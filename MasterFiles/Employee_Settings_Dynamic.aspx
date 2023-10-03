<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Employee_Settings_Dynamic.aspx.cs" Inherits="Menu_Creation_Employee_Settings_Dynamic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <link href="../css/style1.css" rel="stylesheet" type="text/css" />
        <script src="../JsFiles/ScrollableGrid.js" type="text/javascript"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link href="CSS/GridviewScroll.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link href="../css/ScrollTabla.css" rel="stylesheet" />
        <script type="text/javascript">
            var AllData = [], Data = [], Sf = [], FrmChk = [];
            var Data_arr = [];
            var str = ''; var cnt = 1, OptionStr = '', optn = '';
            var FilterData = ['State', 'Region', 'Zone'];
            var DuplicateFltr = FilterData;
            var len = FilterData.length;
            $(document).ready(function () {

                var div_code = '<%= Session["div_code"] %>';
                var sf_code = '<%= Session["sf_code"] %>';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Employee_Settings_Dynamic.aspx/getsf",
                    data: "{'div_code':'" + div_code + "','sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        Sf = JSON.parse(data.d) || [];
                    },
                    error: function (res) {

                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Employee_Settings_Dynamic.aspx/getheader_Chk",
                    dataType: "json",
                    success: function (data) {
                        FrmChk = JSON.parse(data.d) || [];
                    },
                    error: function (res) {

                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Employee_Settings_Dynamic.aspx/getdata",
                    data: "{'div_code':'" + div_code + "','sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllData = JSON.parse(data.d) || [];
                        Data = AllData;
                    },
                    error: function (res) {

                    }
                });
				//&& FrmChk.length > 0 
                if (Data.length > 0 && Sf.length > 0) {
                    Data_arr = Data;
                    $.each(FrmChk, function (key, val) {
                        (val.EmpChk == 1) ? LoadHeader(val.Field_Name, val.SetUp_ID) : '';
                    });
                    ReloadTable(Sf);
                }


                $('#btn_rmv').on('click', function () {
                    $('#div' + cnt).remove();
                    cnt -= 1;
                });

                $('#btn_Add').on('click', function () {
                    if (cnt <= len) {
                        cnt += 1;
                        OptionStr = '';
                        var str = "<div id='div" + cnt + "' style='margin: 5px;'>\
                            <select id='Cmb_"+ cnt + "' onchange=\"FilterDataChng(\'\Cmb_" + cnt + "'\,\'FltrCmb_" + cnt + "'\)\" class='col-lg-offset-1' style='width: 100px;'></select>\
                            <label class='col-lg-offset-2' style='text-align: center;padding: 10px;'>=</label>\
                            <select id='FltrCmb_"+ cnt + "' onchange=\"FltrData(\'\FltrCmb_" + cnt + "'\,\'Cmb_" + cnt + "'\)\" class='col-lg-offset-2' style='text-align: center;width: 100px;'></select>\
                          </div>";
                        $('.Filtermodal').append(str);
                        OptionStr = "<option selected disabled>--select--</option>"
                        for (var i = 0; i < DuplicateFltr.length; i++) {
                            if (DuplicateFltr[i] != $("#Cmb_" + (cnt - 1)).val() && DuplicateFltr[i] != $("#Cmb_" + (cnt - 2)).val() && DuplicateFltr[i] != $("#Cmb_" + (cnt - 3)).val())
                                OptionStr += "<option value='" + DuplicateFltr[i] + "'>" + DuplicateFltr[i] + "</option>";
                        }
                        $('#Cmb_' + cnt).append(OptionStr);
                    }
                });

                $('#filter_chklst').on('click', function () {
                    $('#FilterChklst').modal('toggle');
                    str = '';
                    $('.Filter_Chklst').html("");
                    $.each(FrmChk, function (key, val) {
                        str += "<div><a><label>" + val.Setup_Name + "<input type='checkbox' " + ((val.EmpChk == 1) ? 'checked=checked' : '') + " onchange='\(HeaderChng\(" + val.SetUp_ID + ")\)\' style='position:absolute;right: 30px;line-height: 20px' id='" + val.SetUp_ID + "'></label></a></div>";
                    });
                    $('.Filter_Chklst').append(str);
                });

                $('#txtser').keyup(function (e) {
                    var $input = $(this);
                    inputContent = $input.val().toLowerCase();
                    $panel = $input.parents('.filterable'),
                        column = $panel.find('.filters th').index($input.parents('th')),
                        $table = $panel.find('.tbl'),
                        $rows = $table.find('tbody tr');
                    var $filteredRows = $rows.filter(function () {
                        var value = $(this).find('td').eq(0).text().toLowerCase();
                        return value.indexOf(inputContent) == -1;
                    });
                    $table.find('tbody .no-result').remove();
                    $rows.show();
                    $filteredRows.hide();
                    if ($filteredRows.length === $rows.length) {
                        $table.find('tbody').prepend($('<tr class="no-result text-center"><td>No result found</td></tr>'));
                    }
                });
            });

            function FilterDataChng(Cmb_ID, optnID) {
                var Chk = '', str = '', optn = '';
                $("#" + optnID).html("");
                var selectval = $('#' + Cmb_ID).val();
                optn = selectval;
                if (selectval != '') {
                    if (selectval == 'State') {
                        Sf.sort((a, b) => b.State_Code - a.State_Code);
                        str += "<option selected disabled>-Nothing Select-</option>";
                        for (var i = 0; i < Sf.length; i++) {
                            if (Sf[i].StName != Chk) {
                                Chk = Sf[i].StName;
                                str += "<option value='" + Sf[i].State_Code + "'>" + Sf[i].StName + "</option>";
                            }
                        }
                    }
                    else if (selectval == 'Region') {
                        Sf.sort((a, b) => b.Territory_Code - a.Territory_Code);
                        str += "<option selected disabled>-Nothing Select-</option>";
                        for (var i = 0; i < Sf.length; i++) {
                            if (Sf[i].Territory != '') {
                                if (Sf[i].Territory != Chk) {
                                    Chk = Sf[i].Territory;
                                    str += "<option value='" + Sf[i].Territory_Code + "'>" + Sf[i].Territory + "</option>";
                                }
                            }
                        }
                    }
                    else if (selectval == 'Zone') {
                        Sf.sort((a, b) => b.Hq_Code - a.Hq_Code);
                        str += "<option selected disabled>-Nothing Select-</option>";
                        for (var i = 0; i < Sf.length; i++) {
                            if (Sf[i].Sf_HQ != '') {
                                if (Sf[i].Sf_HQ != Chk) {
                                    Chk = Sf[i].Sf_HQ;
                                    str += "<option value='" + Sf[i].Hq_Code + "'>" + Sf[i].Sf_HQ + "</option>";
                                }
                            }
                        }
                    }
                    $("#" + optnID).append(str);
                }
            }

            function FltrData(Cmb_ID,FltrID) {
                var $selectval = $('#' + Cmb_ID).val();
                var FilterSf = [];
                if ($selectval != '') {
                    if ($("#" + FltrID).val() == 'State') {
                        FilterSf = Sf.filter(function (a) {
                            return a.State_Code == $selectval;
                        });
                    }
                    else if ($("#" + FltrID).val() == 'Region') {
                        FilterSf = Sf.filter(function (a) {
                            return a.Territory_Code == $selectval;
                        });
                    }
                    else if ($("#" + FltrID).val() == 'Zone') {
                        FilterSf = Sf.filter(function (a) {
                            return a.Hq_Code == $selectval;
                        });
                    }
                    if (FilterSf.length > 0)
                        ReloadTable(FilterSf);
                    else
                        alert('No Data');
                }
            }

            function chngneed(ID, Name) {
                var cb = $("#" + ID);         //checkbox that was changed
                th = cb.parent();      //get parent th
                col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", cb[0].checked);

                for (var i = 0; i < Data_arr.length; i++) {
                    if (Name == 'GeoNeed')
                        Data_arr[i]['GeoNeed'] = (ID.checked == true) ? 0 : 1;
                    else if (Name == 'Geo_Track')
                        Data_arr[i]['Geo_Track'] = (ID.checked == true) ? 1 : 0;
                    else if (Name == 'Geo_Fencing')
                        Data_arr[i]['Geo_Fencing'] = (ID.checked == true) ? 1 : 0;
                    else if (Name == 'Eddy_Sumry')
                        Data_arr[i]['Eddy_Sumry'] = (ID.checked == true) ? 1 : 0;
                    else if (Name == 'Van_Sales')
                        Data_arr[i]['Van_Sales'] = (ID.checked == true) ? 1 : 0;
                    else if (Name == 'DCR_Summary_ND')
                        Data_arr[i]['DCR_Summary_ND'] = (ID.checked == true) ? 1 : 0;
                }
            }

            function checkhead(Field_Name, SetUp_ID) {
                var HeadNm = (Field_Name == 'GeoNeed') ? 'g' + SetUp_ID : (Field_Name == 'Geo_Track') ? 't' + SetUp_ID : (Field_Name == 'Geo_Fencing') ? 'f' + SetUp_ID : (Field_Name == 'Eddy_Sumry') ? 'e' + SetUp_ID : (Field_Name == 'Van_Sales') ? 'v' + SetUp_ID : (Field_Name == 'DCR_Summary_ND') ? 'en' + SetUp_ID : '';
                if (HeadNm != '') {
                    if ($(".Td_" + SetUp_ID + " input:checkbox").length == $(".Td_" + SetUp_ID + " input:checkbox:checked").length) {
                        $("#" + HeadNm).attr("checked", "checked");
                    } else {
                        $("#" + HeadNm).removeAttr("checked");
                    }
                }
            }

            function HeaderChng(ID) {
                var chngval = '';
                chngval = $('#' + ID).attr("checked") ? 1 : 0;
                filterval = FrmChk.filter(function (a) {
                    return a.SetUp_ID == ID;
                });
                if (filterval[0].SetUp_ID == ID) {
                    filterval[0]['EmpChk'] = chngval;
                }
                const Arr = JSON.stringify(filterval);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Employee_Settings_Dynamic.aspx/HeaderChng",
                    data: "{'ChngVal':'" + Arr + "'}",
                    dataType: "json",
                    success: function (data) {
                        GetUpdateArr = JSON.parse(data.d) || [];
                        (GetUpdateArr[0].EmpChk == 1) ? LoadHeader(GetUpdateArr[0].Field_Name, GetUpdateArr[0].SetUp_ID) : TH_TD(GetUpdateArr[0].SetUp_ID);
                        LoadChngData(GetUpdateArr)
                    },
                    error: function (res) {

                    }
                });
            }

            function TH_TD(SetUp_ID) {
                $('.' + SetUp_ID).remove();
                $('.Td_' + SetUp_ID).remove();
            }
            function LoadChngData(Arr) {
                for (var $i = 0; $i < Sf.length; $i++) {
                    if (Sf[$i].Sf_Code != 'admin') {
                        FilterSF_Data = Data.filter(function (a) {
                            return a.Sf_code == Sf[$i].Sf_Code;
                        });
                        str = '';
                        loadTable(Arr, Sf[$i].Sf_Code);
                    }
                }
            }

            function EditArr(FieldNm, ID, sfcode, cls, HeaderID) {
                if ($("." + cls + " input:checkbox").length == $("." + cls + " input:checkbox:checked").length) {
                    $("#" + HeaderID).attr("checked", "checked");
                } else {
                    $("#" + HeaderID).removeAttr("checked");
                }
                var Chk = '';
                if (FieldNm == 'GeoNeed') {
                    Chk = $('#' + ID).attr("checked") ? 0 : 1;
                }
                else {
                    Chk = $('#' + ID).attr("checked") ? 1 : 0;
                }
                for (var i = 0; i < Data_arr.length; i++) {
                    if (FieldNm == 'GeoNeed' && Data_arr[i].Sf_code == sfcode)
                        Data_arr[i]['GeoNeed'] = Chk;
                    else if (FieldNm == 'Geo_Track' && Data_arr[i].Sf_code == sfcode)
                        Data_arr[i]['Geo_Track'] = Chk;
                    else if (FieldNm == 'Geo_Fencing' && Data_arr[i].Sf_code == sfcode)
                        Data_arr[i]['Geo_Fencing'] = Chk;
                    else if (FieldNm == 'Eddy_Sumry' && Data_arr[i].Sf_code == sfcode)
                        Data_arr[i]['Eddy_Sumry'] = Chk;
                    else if (FieldNm == 'Van_Sales' && Data_arr[i].Sf_code == sfcode)
                        Data_arr[i]['Van_Sales'] = Chk;
                    else if (FieldNm == 'DCR_Summary_ND' && Data_arr[i].Sf_code == sfcode)
                        Data_arr[i]['DCR_Summary_ND'] = Chk;
                }
            }

            function loadTable(ChngArr, sfcode) {
				str='';
                for (var $j = 0; $j < ChngArr.length; $j++) {
                    checkhead(ChngArr[$j].Field_Name, ChngArr[$j].SetUp_ID);
                    for (var i = 0; i < FilterSF_Data.length; i++) {
                        if (ChngArr[$j].Field_Name == 'GeoNeed' && ChngArr[$j].EmpChk == 1) {
                            if (FilterSF_Data[i].GeoNeed == 1) {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'g" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='g" + FilterSF_Data[i].Sf_code + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='g" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                            else {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'g" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='g" + FilterSF_Data[i].Sf_code + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='g" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                        }
                        if (ChngArr[$j].Field_Name == 'Geo_Track' && ChngArr[$j].EmpChk == 1) {
                            if (FilterSF_Data[i].Geo_Track == 0) {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'t" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='t" + FilterSF_Data[i].Sf_code + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='t" + FilterSF_Data[i].Sf_code + "'></label></td>";
                            }
                            else {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'t" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='t" + FilterSF_Data[i].Sf_code + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='t" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                        }
                        if (ChngArr[$j].Field_Name == 'Geo_Fencing' && ChngArr[$j].EmpChk == 1) {

                            if (FilterSF_Data[i].Geo_Fencing == 0) {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'f" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='f" + FilterSF_Data[i].Sf_code + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='f" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                            else {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'f" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='f" + FilterSF_Data[i].Sf_code + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='f" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                        }
                        if (ChngArr[$j].Field_Name == 'Eddy_Sumry' && ChngArr[$j].EmpChk == 1) {

                            if (FilterSF_Data[i].Eddy_Sumry == 0) {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'e" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='e" + FilterSF_Data[i].Sf_code + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='e" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                            else {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'e" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='e" + FilterSF_Data[i].Sf_code + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='e" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                        }
                        if (ChngArr[$j].Field_Name == 'Van_Sales' && ChngArr[$j].EmpChk == 1) {

                            if (FilterSF_Data[i].Van_Sales == 0) {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'v" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='v" + FilterSF_Data[i].Sf_code + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='v" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                            else {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'v" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='v" + FilterSF_Data[i].Sf_code + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='v" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                        }
                        if (ChngArr[$j].Field_Name == 'DCR_Summary_ND' && ChngArr[$j].EmpChk == 1) {

                            if (FilterSF_Data[i].DCR_Summary_ND == 0) {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' id='chnl" + FilterSF_Data[i].Sf_code + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed hidecl' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'en" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='en" + FilterSF_Data[i].Sf_code + "' disabled> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='en" + FilterSF_Data[i].Sf_code + "'></label></td> ";
                            }
                            else {
                                str += "<td class='col-xs-2 Td_" + ChngArr[$j].SetUp_ID + "' id='chnl" + FilterSF_Data[i].Sf_code + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed hidecl' onchange=\"EditArr(\'" + ChngArr[$j].Field_Name + "'\,\'g" + FilterSF_Data[i].Sf_code + "'\,\'" + sfcode + "'\,\'Td_" + ChngArr[$j].SetUp_ID + "'\,\'en" + ChngArr[$j].SetUp_ID + "'\)\" type='checkbox' id='en" + FilterSF_Data[i].Sf_code + "' disabled checked></input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='en" + FilterSF_Data[i].Sf_code + "' ></label></td> ";
                            }
                        }

                    }
                }
				//if($('#row_' + sfcode)) $('#table1 tbody').append(str)
                (ChngArr.length == 0) ? "" : $('#row_' + sfcode).append(str);
                if ('<%=Session["div_code"]%>' != '98') {
                    $('#chnl' + sfcode).hide();
                }
                if ('<%=Session["div_code"]%>' == '98') {
                    $('#chechenen').prop('disabled', false);
                    $('#cen').show();
                    var cells = document.getElementsByClassName("hidecl");
                    for (var i = 0; i < cells.length; i++) {
                        cells[i].disabled = false;
                    }
                }
                else {

                    $('#cen').hide();
                }
            }


            function LoadHeader(HeaderChk, ID) {
                var Header = '';

                $('.' + ID).remove();
                var cls = 'class=" align-middle ' + ID + '" style="text-align:-webkit-center"';
                var input = '';
                if (HeaderChk == 'GeoNeed') {
                    Header = "<th " + cls + ">Location<input class='tgl tgl-skewed 'g" + ID + "'' type='checkbox' onchange=\"chngneed('g" + ID + "',\'" + HeaderChk + "'\)\" id='g" + ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='g" + ID + "'></label></th>";
                }
                else if (HeaderChk == 'Geo_Track') {
                    Header = "<th " + cls + ">Tracking<input class='tgl tgl-skewed' type='checkbox' onchange=\"chngneed(\'t" + ID + "'\,\'" + HeaderChk + "'\)\" id='t" + ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='t" + ID + "'></label></th>";
                }
                else if (HeaderChk == 'Geo_Fencing') {
                    Header = "<th " + cls + ">Fencing<input class='tgl tgl-skewed' type='checkbox' onchange=\"chngneed(\'f" + ID + "'\,\'" + HeaderChk + "'\)\" id='f" + ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='f" + ID + "'></label></th>";
                }
                else if (HeaderChk == 'Eddy_Sumry') {
                    Header = "<th " + cls + ">Edit Day Summary<input class='tgl tgl-skewed' type='checkbox' onchange=\"chngneed(\'e" + ID + "'\,\'" + HeaderChk + "'\)\" id='e" + ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='e" + ID + "'></label></th>";
                }
                else if (HeaderChk == 'Van_Sales') {
                    Header = "<th " + cls + ">Van Sales<input class='tgl tgl-skewed' type='checkbox' onchange=\"chngneed(\'v" + ID + "'\,\'" + HeaderChk + "'\)\" id='v" + ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='v" + ID + "'></label></th>";
                }
                else if (HeaderChk == 'DCR_Summary_ND') {
                    Header = "<th " + cls + " id='cen'>Channel Entry<input class='tgl tgl-skewed' type='checkbox' onchange=\"chngneed(\'en" + ID + "'\,\'" + HeaderChk + "'\)\" id='en" + ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='en" + ID + "'></label></th>";
                }
                $('.filters').append(Header);
            }
            function ReloadTable(SfArr) {
                $('#table1 tbody').html("");
                str = ''; var FrmChk_Nm = '';
                if (SfArr.length > 0) {
                    for (var $i = 0; $i < SfArr.length; $i++) {
                        if (SfArr[$i].Sf_Code != 'admin') {
                            str = "<tr  id='row_" + SfArr[$i].Sf_Code + "'><td style='height:80px;left: 15px;position:sticky;background:white;' class='TotalHead col-xs-2'><input class='rsf' type='hidden' name='hsfcode' value=" + SfArr[$i].Reporting_To_SF + "><input class='sfc' type='hidden' name='hsfcode' value=" + SfArr[$i].Sf_Code + "> <input class='sft' type='hidden' name='sftype' value=" + SfArr[$i].sf_type + ">" + SfArr[$i].Sf_Name + "</td>";
							$('#table1 tbody').append(str);
                            FilterSF_Data = Data.filter(function (a) {
                                return a.Sf_code == SfArr[$i].Sf_Code;
                            });
                            loadTable(FrmChk, SfArr[$i].Sf_Code);
                        }
                    }
                }
            }

            function fun() {
                $('#loadover').show();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Employee_Settings_Dynamic.aspx/savedata",
                    data: "{'data':'" + JSON.stringify(Data_arr) + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#loadover').hide();
                        alert("Employee Setting has been updated successfully!!!");
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            }

            function Filter() {
                $('#FilterModal').modal('toggle');
                //$('#myModal').modal('toggle');

            }
        </script>
        <link href="../css/ScrollTabla.css" rel="stylesheet" />
        <style>
            .TotalHead {
                /*background:#777;
             color:#FFF;*/
                border-right: 1px solid #999;
                position: sticky;
                top: 0;
                z-index: 4;
            }


            #loader {
                position: absolute;
                left: 50%;
                top: 50%;
                z-index: 1;
                width: 120px;
                height: 120px;
                margin: -76px 0 0 -76px;
                border: 16px solid #f3f3f3;
                border-radius: 50%;
                border-top: 16px solid #3498db;
                -webkit-animation: spin 2s linear infinite;
                animation: spin 2s linear infinite;
            }

            .overlay {
                background-color: #EFEFEF;
                position: fixed;
                width: 100%;
                height: 100%;
                z-index: 1000;
                top: 0px;
                left: 0px;
                opacity: .5; /* in FireFox */
                filter: alpha(opacity=50); /* in IE */
            }

            .tg-list-item {
                margin: 0 2em;
            }


            .tgl {
                display: none;
            }

                .tgl, .tgl:after, .tgl:before, .tgl *, .tgl *:after, .tgl *:before, .tgl + .tgl-btn {
                    box-sizing: border-box;
                }

                    .tgl::-moz-selection, .tgl:after::-moz-selection, .tgl:before::-moz-selection, .tgl *::-moz-selection, .tgl *:after::-moz-selection, .tgl *:before::-moz-selection, .tgl + .tgl-btn::-moz-selection {
                        background: none;
                    }

                    .tgl::selection, .tgl:after::selection, .tgl:before::selection, .tgl *::selection, .tgl *:after::selection, .tgl *:before::selection, .tgl + .tgl-btn::selection {
                        background: none;
                    }

                    .tgl + .tgl-btn {
                        outline: 0;
                        display: block;
                        width: 4em;
                        height: 2em;
                        position: relative;
                        cursor: pointer;
                        -webkit-user-select: none;
                        -moz-user-select: none;
                        -ms-user-select: none;
                        user-select: none;
                    }


            .tgl-skewed + .tgl-btn {
                overflow: hidden;
                -webkit-transform: skew(-10deg);
                transform: skew(-10deg);
                -webkit-backface-visibility: hidden;
                backface-visibility: hidden;
                -webkit-transition: all .2s ease;
                transition: all .2s ease;
                font-family: sans-serif;
                background: #888;
            }

                .tgl-skewed + .tgl-btn:after, .tgl-skewed + .tgl-btn:before {
                    -webkit-transform: skew(10deg);
                    transform: skew(10deg);
                    display: inline-block;
                    -webkit-transition: all .2s ease;
                    transition: all .2s ease;
                    width: 100%;
                    text-align: center;
                    position: absolute;
                    line-height: 2em;
                    font-weight: bold;
                    color: #fff;
                    text-shadow: 0 1px 0 rgba(0, 0, 0, 0.4);
                }

                .tgl-skewed + .tgl-btn:after {
                    left: 100%;
                    content: attr(data-tg-on);
                }

                .tgl-skewed + .tgl-btn:before {
                    left: 0;
                    content: attr(data-tg-off);
                }

                .tgl-skewed + .tgl-btn:active {
                    background: #888;
                }

                    .tgl-skewed + .tgl-btn:active:before {
                        left: -10%;
                    }

            .tgl-skewed:checked + .tgl-btn {
                background: #86d993;
            }

                .tgl-skewed:checked + .tgl-btn:before {
                    left: -100%;
                }

                .tgl-skewed:checked + .tgl-btn:after {
                    left: 0;
                }

                .tgl-skewed:checked + .tgl-btn:active:after {
                    left: 10%;
                }

            .results tr[visible='false'], .no-result {
                display: none;
            }

            .results tr[visible='true'] {
                display: table-row;
            }

            .counter {
                padding: 8px;
                color: #ccc;
            }


            .table-fixed thead {
                width: 97%;
            }

            .table-fixed tbody {
                height: 230px;
                overflow-y: auto;
                width: 100%;
            }

                /*.table-fixed thead, .table-fixed tbody, .table-fixed tr, .table-fixed td, .table-fixed th {
                display: block;
            }*/

                .table-fixed tbody td, .table-fixed thead > tr > th {
                    float: left;
                    border-bottom-width: 0;
                }

            .table thead {
                width: 100%;
            }

                /*.table tbody {
                overflow-x: auto;
                width: 100%;
            }*/

                .table thead tr {
                    width: 99%;
                }

            .table tr {
                width: 100%;
            }

            /*.table thead, .table tbody, .table tr, .table td, .table th {
                display: inline-block;
            }*/

            .table thead {
                background: #4697ce;
                color: #fff;
            }

                .table tbody td, .table thead > tr > th {
                    float: left;
                    border-bottom-width: 0;
                }

            .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
                padding: 8px;
                height: 75px;
                text-align: center;
                /*line-height: 32px;*/
            }

            .filterable {
                margin-top: 15px;
            }

                .filterable .panel-heading .pull-right {
                    margin-top: -20px;
                }

                .filterable .filters input[disabled] {
                    background-color: transparent;
                    border: none;
                    cursor: auto;
                    box-shadow: none;
                    padding: 0;
                    height: auto;
                }

                    .filterable .filters input[disabled]::-webkit-input-placeholder {
                        color: #333;
                    }

                    .filterable .filters input[disabled]::-moz-placeholder {
                        color: #333;
                    }

                    .filterable .filters input[disabled]:-ms-input-placeholder {
                        color: #333;
                    }

            .modal {
                position: fixed;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: gray;
                z-index: 10000000;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }
        </style>
        <link href="../css/font-awesome.min.css" rel="stylesheet" />
        <link href="../css/font-awesome.css" rel="stylesheet" />
    </head>
    <body>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        <div style="text-align: right;">
            <label class="" style="cursor:pointer;text-align: right;" id="filter_chklst"><i style="padding: 5px;" class="fa fa-plus"></i>Add Settings</label>
            <label onclick="Filter()" style="cursor:pointer;"><i style="padding: 5px;" class="fa fa-filter"></i>Filter</label>
        </div>
        <div>
            <h3 style="margin-top: 0px; margin-bottom: 0px; margin-inline: 20px;">Employee Setting</h3>

        </div>
        <div class="container">
            <div class="row" style="max-width: 100%;">
                <div class="panel panel-primary filterable" style="overflow-y: auto">
                    <table id="table1" class="tbl">
                        <thead style="background: #19a4c6;" class="TotalHead">
                            <tr class="filters" style="height: 80px; text-align: left">
                                <th style="position: sticky; z-index: 4; max-width: 300px; min-width: 300px; padding: 15px; left: 15px; background: #19a4c6;">Employee Name
                                <div colspan="3" style="width: 100px;">
                                    <input type="text" id="txtser" class="form-control" style="height: 20px; width: 25px;" />
                                </div>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>

                </div>
                <div class="modal fade" id="FilterModal" role="dialog" tabindex="-1" aria-hidden="false" aria-labelledby="FilterModal" style="display: none; z-index: 10000000; background: transparent;">
                    <div class="modal-dialog" role="document" style="width: 30%;">
                        <div class="modal-content">
                            <div class="modal-body Filtermodal">
                                <button type="button" id="btnClose" class="close" style="margin-top: -12px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <div style="margin-top: 10px;">
                                    <button type="button" class="btn btn-primary col-lg-8 btn-circle" id="btn_Add"><i class="fa fa-plus"></i></button>
                                    <button type="button" class="btn btn-danger col-lg-offset-10 btn-circle" id="btn_rmv"><i class="fa fa-trash-o"></i></button>
                                </div>

                            </div>
                            <div style="text-align: center; margin-bottom: 18px;">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="FilterChklst" tabindex="-1" style="display: none; z-index: 10000000; background: transparent;" role="dialog" aria-labelledby="FilterChklst" aria-hidden="false">
                    <div class="modal-dialog" role="document" style="width: 30%;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5>Setting Check List</h5>
                                <button type="button" id="btntimesClose" class="close" style="margin-top: -30px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            </div>
                            <div class="modal-body Filter_Chklst">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center;">
                <div class="col-md-12">
                    <button type="button" id="btnsave" onclick="fun()" class="btn btn-primary" style="width: 100px;">
                        Save               
                    </button>
                </div>
            </div>
        </div>
    </body>
    </html>
</asp:Content>

