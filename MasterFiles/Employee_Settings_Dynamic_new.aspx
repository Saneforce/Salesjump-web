<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Employee_Settings_Dynamic_new.aspx.cs" Inherits="Menu_Creation_Employee_Settings_Dynamic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <link type="text/css" href="../css/style.css" rel="stylesheet" />
        <link type="text/css" href="../css/style1.css" rel="stylesheet" />
        <link type="text/css" href="../CSS/GridviewScroll.css" rel="stylesheet" />
        <link type="text/css" href="../css/ScrollTabla.css" rel="stylesheet" />


        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

        <script type="text/javascript">
            $finlSettings = []; $EmpList = null; $settList = null; $AllData = []; $Data_ColRow = null; var $finallist = null;
            var cnt = 0;

            $div = '<%= Session["div_code"] %>';
            $sf = '<%= Session["sf_code"] %>';

            $(document).ready(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Employee_Settings_Dynamic_new.aspx/getdata",
                    data: "{'div_code':'" + $div + "','sf_code':'" + $sf + "'}",
                    dataType: "json",
                    success: function (data) {
                        $AllData = JSON.parse(data.d) || []; genSettingsGrid($EmpList);
                    },
                    error: function (res) {

                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Employee_Settings_Dynamic_new.aspx/getsf",
                    data: "{'div_code':'" + $div + "','sf_code':'" + $sf + "'}",
                    dataType: "json",
                    success: function (data) {
                        $EmpList = JSON.parse(data.d) || []; genSettingsGrid($EmpList);
                    },
                    error: function (res) {
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Employee_Settings_Dynamic_new.aspx/getSettingsList",
                    data: "{'div_code':'" + $div + "','sf_code':'" + $sf + "'}",
                    dataType: "json",
                    success: function (data) {
                        $settList = JSON.parse(data.d) || [];
                        unpivot()
                        genSettingsGrid($EmpList);
                    },
                    error: function (res) {

                    }
                });

                $('#filter_chklst').on('click', function () {
                    $('#FilterChklst').modal('toggle');
                    str = '';
                    $('.Filter_Chklst').html("");
                    $.each($settList, function (key, val) {
                        str += "<div><a><label>" + val.Setup_Name + "<input type='checkbox' " + ((val.EmpChk == 1) ? 'checked=checked' : '') + " onchange='\(HeaderChng\(" + val.SetUp_ID + ")\)\' style='position:absolute;right: 30px;line-height: 20px' id='" + val.SetUp_ID + "'></label></a></div>";
                    });
                    $('.Filter_Chklst').append(str);
                });
                $('#btn_Add').on('click', function () {
                    if (cnt <= 4) {
                        cnt += 1;
                        var str = "<div id='div" + cnt + "' style='margin: 2px;'>\
                            <select id='Cmb_"+ cnt + "' onchange=\"GetFilterData(\'\Cmb_" + cnt + "'\,\'FltrCmb_" + cnt + "'\)\"  style='width: 100px;'>\
                            <option value='' selected disabled>Select Value</option><option value='Sf_Name'>Employe Name</option><option value='StName'>State Name</option><option value='Territory_Code'>Territory</option><option value='Designation'>Designation</option>\
                            <option value='Reporting_To_SF'>Reporting Manager</option></select>\
                            <label class='col-lg-offset-2' style='text-align: center;padding: 10px;'>=</label>\
                            <select id='FltrCmb_"+ cnt + "' onchange=\"FltrData(\'\FltrCmb_" + cnt + "'\,\'Cmb_" + cnt + "'\)\" class='col-lg-offset-2' style='text-align: center;width: 100px;'></select>\
                          </div>";
                        $('.Filtermodal').append(str);
                    }
                });
                $('#btn_rmv').on('click', function () {
                    $('.Filtermodal').html("");
                    cnt = 0;
                    if (cnt == 0)
                        genSettingsGrid($EmpList);
                });
				 $(document).on('keyup', '#txtser', function () {
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
                    chk = $EmpList.filter(function (a) {
                        return a.Sf_Name.toLowerCase().indexOf(inputContent) > -1;
                    });
                    HeadChk_srch(chk)
                });
            });
            function HeadChk_srch(filteredRows) {
                for (var r = 0; r < filteredRows.length; r++) {                    
                    headchk = $finallist.filter(function (a) {
                        return a.sf_code == filteredRows[r].Sf_Code;
                    });
                    for ($j = 0; $j < $settList.length; $j++) {
                        for (var i = 0; i < headchk.length; i++) {
                            if ($settList[$j].Field_Name == headchk[i].Field_Name) {
                                if ($("#" + headchk[i].Field_Name+headchk[i].sf_code).is(':checked')) {
                                    $("#g" + $settList[$j].SetUp_ID).attr("checked", "checked");
                                } else {
                                    $("#g" + $settList[$j].SetUp_ID).removeAttr("checked");
                                }
                            }
                        }
                    }
                }
            }
            function unpivot() {
                var Qry = '', castvar = '';
                for (var $i = 0; $i < $settList.length; $i++) {
                    Qry += $settList[$i].Field_Name + ",";
                }
                Qry = Qry.slice(0, -1);
                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Employee_Settings_Dynamic_new.aspx/GetData_colrow",
                    dataType: "json",
                    data: "{'Unpivot':'" + Qry + "'}",
                    success: function (data) {
                        $Data_ColRow = JSON.parse(data.d) || [];
                        if ($Data_ColRow.length > 0) {
                            $finallist = []; Items = {};
                            for (var $j = 0; $j < $EmpList.length; $j++) {
                                $Value = $Data_ColRow.filter(function (a) {
                                    return (a.sf_code == $EmpList[$j].Sf_Code);
                                });
                                for (var $k = 0; $k < $Value.length; $k++) {
                                    $finallist.push($Value[$k]);
                                }
                            }
                            genSettingsGrid($EmpList);
                        }
                    },
                    Error: function (res) {
                        alert(res);
                    }
                });
            }

            function FltrData(FltrCmb_ID, Cmb_ID) {
                var val = $('#' + FltrCmb_ID).val();
                var $FiltrData = null;
                if ($('#' + Cmb_ID).val() == 'Territory_Code')
                    $FiltrData = $EmpList.filter(function (b) {
                        return b.Territory_Code == val;
                    });
                else if ($('#' + Cmb_ID).val() == 'Sf_Name')
                    $FiltrData = $EmpList.filter(function (b) {
                        return b.Sf_Code == val;
                    });
                else if ($('#' + Cmb_ID).val() == 'StName')
                    $FiltrData = $EmpList.filter(function (b) {
                        return b.State_Code == val;
                    });
                else if ($('#' + Cmb_ID).val() == 'Designation')
                    $FiltrData = $EmpList.filter(function (b) {
                        return b.Designation == val;
                    });
                else if ($('#' + Cmb_ID).val() == 'Reporting_To_SF')
                    $FiltrData = $EmpList.filter(function (b) {
                        return b.Reporting_To_SF == val;
                    });
                ($FiltrData.length > 0) ? genSettingsGrid($FiltrData) : alert("No Data Available!!!");
                $("#FilterModal").modal('hide');
            }
            function GetFilterData(Cmb_ID, optnID) {
                //if ($('#' + Cmb_ID).val() == 'Territory_code') {
                var $arrayrt = []; str = ''; let mymap = new Map();
                $arrayrt = $EmpList.filter(function (el) {
                    const val = ($('#' + Cmb_ID).val() == 'Territory_Code') ? mymap.get(el.Territory_Code) : ($('#' + Cmb_ID).val() == 'Sf_Name') ? mymap.get(el.Sf_Code) : ($('#' + Cmb_ID).val() == 'StName') ? mymap.get(el.State_Code) : ($('#' + Cmb_ID).val() == 'Designation') ? mymap.get(el.Designation) : ($('#' + Cmb_ID).val() == 'Reporting_To_SF') ? mymap.get(el.Reporting_To_SF) : '';
                    if (val) {
                        return false;
                    }
                    ((($('#' + Cmb_ID).val() == 'Territory_Code')&& el.Territory_Code!="") ? mymap.set(el.Territory_Code, el.Territory) : ($('#' + Cmb_ID).val() == 'Sf_Name') ? mymap.set(el.Sf_Code, el.Sf_Name) : ($('#' + Cmb_ID).val() == 'StName') ? mymap.set(el.State_Code, el.StName) : ($('#' + Cmb_ID).val() == 'Designation') ? mymap.set(el.Designation,el.Designation) : ($('#' + Cmb_ID).val() == 'Reporting_To_SF') ? mymap.set(el.Reporting_To_SF,el.Reporting_To_SF) : mymap.set());
                    return true;
                }).map(function (a) { return a; }).sort();
                $('#' + optnID).empty().append("<option value='' selected disabled>Select Value</option>");
                for (var i = 0; i < $arrayrt.length; i++) {
                    if ($('#' + Cmb_ID).val() == 'Territory_Code') {
                        if ($arrayrt[i].Territory_Code != '' && $arrayrt[i].Territory_Code != 0)
                            str += '<option value="' + $arrayrt[i].Territory_Code + '">' + $arrayrt[i].Territory + '</option>';
                    }
                    else if ($('#' + Cmb_ID).val() == 'Sf_Name') {
                        if ($arrayrt[i].Sf_Code != 'admin')
                            str += '<option value="' + $arrayrt[i].Sf_Code + '">' + $arrayrt[i].Sf_Name + '</option>';
                    }
                    else if ($('#' + Cmb_ID).val() == 'StName')
                        str += '<option value="' + $arrayrt[i].State_Code + '">' + $arrayrt[i].StName + '</option>';
                    else if ($('#' + Cmb_ID).val() == 'Designation')
                        str += '<option value="' + $arrayrt[i].Designation + '">' + $arrayrt[i].Designation + '</option>';
                    else if ($('#' + Cmb_ID).val() == 'Reporting_To_SF') {
                        if ($arrayrt[i].Reporting_To_SF != '')
                            str += '<option value="' + $arrayrt[i].Reporting_To_SF + '">' + $arrayrt[i].Reporting_To_SF + '</option>';
                    }
                }
                $('#' + optnID).append(str);
                //}
            }



            function genSettingsGrid($Emp) {
                if ($Emp != null && $settList != null && $AllData != null && $finallist != null) {
                    str = '';
                    $('#table1 tbody').html(""); $finlSettings = [];
                    for ($i = 0; $i < $Emp.length; $i++) {
                        if ($Emp[$i].Sf_Code != 'admin') {
                            
                            $item = {}; $SettArr = {};
                            $GetVal = $AllData.filter(function (a) {
                                return a.sf_code == $Emp[$i].Sf_Code;
                            })
							if($GetVal.length>0){
								str += "<tr id='rw_" + $Emp[$i].Sf_Code + "'><td style='height:80px;left: 15px;position:sticky;background:white;' class='TotalHead col-xs-4'>" + $Emp[$i].Sf_Name + "</td>";
								$item["SF"] = $Emp[$i].Sf_Code;
								$SettArr = $GetVal[0];
								for ($j = 0; $j < $settList.length; $j++) {
									if ($settList[$j].EmpChk == 1)
										//$item["Field_Name"]=$settList[$j].Field_Name;
										$item[$settList[$j].Field_Name] = $SettArr[$settList[$j].Field_Name];
								}
								$finlSettings.push($item);
							}
                        }
                    }
                    str += "</tr>";
                    $('#table1 tbody').append(str)
                    genTable();
                }
            }
            function genTable() {
                shdStr = $("#thHead").html();
                SF = ""; HeadId = ''; EmpID = '';
                str = ""; $sHead = shdStr.substring(0, shdStr.indexOf("</th>"));
                for ($i = 0; $i < $finlSettings.length; $i++) {
                    $emp = $EmpList.filter(function (a) {
                        return a.Sf_Code == $finlSettings[$i].SF;
                    })
                    //if($EmpList[$i].Sf_Code!='admin'){
                    $item = $finlSettings[$i];
                    SF = $finlSettings[$i].SF;
                    //str+="<tr id='rw_"+$item.SF+"'><td>"+$emp[0].Sf_Name+"</td>";
                    $.each($item, function (key, val) {
						chkdata = '';
                        $sett = $settList.filter(function (a) {
                            return a.Field_Name == key;
                        })
                        if ($sett.length > 0) {
                            var valarr = $sett[0].Setup_Values;
                            var valuessetup = valarr.slice(0, -1).split(/,/);
                            var Listtxt_Val = [], TxtArr = [];
                            $.each(valuessetup, function (key, val) {
                                Listtxt_Val = val.split('#');
                                TxtArr.push({
                                    id: Listtxt_Val[1],
                                    value: Listtxt_Val[0]
                                });
                            });
                            for (var $m = 0; $m < TxtArr.length; $m++) {
                                if ((TxtArr[$m].value == 'ON' || TxtArr[$m].value == 'Yes') && $finlSettings[$i][key] == TxtArr[$m].id)
                                    chkdata = 'checked=checked';
                            }
							//if($finlSettings.length!=2){
								if ($i == 1) {
									$sHead += "<th class='align-middle Th_" + $sett[0].SetUp_ID + "' style='text-align:-webkit-center;padding-left:8px;padding-right:8px;font-size: smaller;'>" + $sett[0].Setup_Name + "<input class='tgl tgl-skewed g" + $sett[0].SetUp_ID + "' type='checkbox' onchange=\"chngneed('g" + $sett[0].SetUp_ID + "',\'" + key + "'\)\" id='g" + $sett[0].SetUp_ID + "'></input><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='g" + $sett[0].SetUp_ID + "'></label></th>";
									$("#thHead").html($sHead);
								}
							//}
							
                            str = "<td class='col-xs-4 Td_" + $sett[0].SetUp_ID + "' style='text-align: -webkit-center' ><input class='tgl tgl-skewed' value='" + val + "' onchange=\"EditArr(\'" + key + "'\,\'" + key + "" + $item.SF + "'\,\'" + $item.SF + "'\,\'Td_" + $sett[0].SetUp_ID + "'\,\'" + $sett[0].SetUp_ID + "'\)\" type='checkbox' id='" + key + "" + $item.SF + "' " + chkdata + " > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='" + key + "" + $item.SF + "'></label></td> ";
                            $('#rw_' + $item.SF).append(str);
                            if ($finlSettings.length > 1)
                                checkhead($sett[0].SetUp_ID, key);
                            else
                                singleValChk($sett[0].SetUp_ID, key + $item.SF);
                        }
                        //(val.EmpChk == 1) ? LoadHeader(val.Field_Name, val.SetUp_ID) : '';
                    });                                                              
                    str = ""
                    ///}
                }

                $('#rw_' + $item.SF).append(str)
                console.log("API Excecuted..." + JSON.stringify($finlSettings));


            }
            function EditArr(FieldNm, ID, sfcode, cls, HeaderID) {
                if ($("." + cls + " input:checkbox").length == $("." + cls + " input:checkbox:checked").length) {
                    $("#g" + HeaderID).attr("checked", "checked");
                }
                else {
                    $("#g" + HeaderID).removeAttr("checked");
                }
                $EditArr = $finlSettings.filter(function (a) {
                    return a.SF == sfcode;
                })
                $item = $EditArr[0];
                for (var i = 0; i < $finallist.length; i++) {
                    if ($finallist[i].Field_Name == FieldNm && $finallist[i].sf_code == sfcode) {
                        for (var $j = 0; $j < $settList.length; $j++) {
                            if ($settList[$j].Field_Name == FieldNm) {
                                var valarr = $settList[$j].Setup_Values;
                                var valuessetup = valarr.slice(0, -1).split(/,/);
                                var Listtxt_Val = [], TxtArr = [];
                                $.each(valuessetup, function (key, val) {
                                    Listtxt_Val = val.split('#');
                                    TxtArr.push({
                                        value: Listtxt_Val[1],
                                        btnnm: Listtxt_Val[0]
                                    });
                                });
                                for (var $k = 0; $k < TxtArr.length; $k++) {
                                    if ($('#' + ID).is(':checked') && TxtArr[$k].btnnm == 'ON') {
                                        $finallist[i]['Value'] = TxtArr[$k].value;
                                        $('#' + ID).val(TxtArr[$k].value);
                                    }
                                    else if (!$('#' + ID).is(':checked') && TxtArr[$k].btnnm == 'OFF') {
                                        $finallist[i]['Value'] = TxtArr[$k].value;
                                        $('#' + ID).val(TxtArr[$k].value);
                                    }
                                }
                                //if ($finallist[i].Field_Name == FieldNm && $finallist[i].sf_code == sfcode) {
                                //    ($('#' + ID).attr("checked")) ? $finallist[i]['Value'] = 0 : $finallist[i]['Value'] = 1;
                                //    var val = ($finallist[i]['Value'] == 0) ? 0 : 1;
                                //    $('#' + ID).val(val);
                                //}
                            }
                        }
                    }

                }
            }
            function singleValChk(HeaderID, Emp_FieldID) {
                if ($('#' + Emp_FieldID).is(':checked')) {
                    $("#g" + HeaderID).attr("checked", "checked");
                }
                else {
                    $("#g" + HeaderID).removeAttr("checked");
                }
            }
            function checkhead(SetUp_ID, Field_Name) {

                if ($(".Td_" + SetUp_ID + " input:checkbox").length == $(".Td_" + SetUp_ID + " input:checkbox:checked").length) {
                    $("#g" + SetUp_ID).attr("checked", "checked");
                } else {
                    $("#g" + SetUp_ID).removeAttr("checked");
                }

            }
            function chngneed(ID, Name) {
                var cb = $("#" + ID);         //checkbox that was changed
                th = cb.parent();      //get parent th
                col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", cb[0].checked);

                for (var $j = 0; $j < $settList.length; $j++) {
                    if ($settList[$j].Field_Name == Name) {
                        var valarr = $settList[$j].Setup_Values;
                        var valuessetup = valarr.slice(0, -1).split(/,/);
                        var Listtxt_Val = [], TxtArr = [];
                        $.each(valuessetup, function (key, val) {
                            Listtxt_Val = val.split('#');
                            TxtArr.push({
                                value: Listtxt_Val[1],
                                btnnm: Listtxt_Val[0]
                            });
                        });
                        for (var $k = 0; $k < TxtArr.length; $k++) {
                            if (cb[0].checked == true) {
                                for ($i = 0; $i < $finallist.length; $i++) {
                                    if ($finallist[$i].sf_code != 'admin') {
                                        if (TxtArr[$k].btnnm == 'ON' && $finallist[$i].Field_Name==Name)
                                            $finallist[$i]['Value'] = TxtArr[$k].value;
                                    }
                                }
                            }
                            else {
                                for ($i = 0; $i < $finallist.length; $i++) {
                                    if ($finallist[$i].sf_code != 'admin') {
                                        if (TxtArr[$k].btnnm == 'OFF' && $finallist[$i].Field_Name==Name)
                                            $finallist[$i]['Value'] = TxtArr[$k].value;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            function HeaderChng(ID) {
                var chngval = '';
                chngval = $('#' + ID).attr("checked") ? 1 : 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Employee_Settings_Dynamic_new.aspx/HeaderChng",
                    data: "{'ChngVal':'" + chngval + "','ID':'" + ID + "'}",
                    dataType: "json",
                    success: function (data) {
                        $settList = null;
                        $settList = JSON.parse(data.d) || [];
                        for ($i = 0; $i < $settList.length; $i++) {
                            ($settList[$i].EmpChk == 1) ? LoadHeader($settList[$i].Setup_Name, $settList[$i].SetUp_ID) : TH_TD($settList[$i].SetUp_ID);
                        }
                        genSettingsGrid($EmpList)
                    },
                    error: function (res) {

                    }
                });
            }
            function LoadHeader(HeaderNm, ID) {
                $strHead = "<th class='Th_" + ID + "' style='text-align:center;padding-left:8px;padding-right:8px'>" + HeaderNm + "</th>";
                $("#thHead").append($strHead);
            }

            function TH_TD(SetUp_ID) {
                $('.Th_' + SetUp_ID).remove();
                $('.Td_' + SetUp_ID).remove();
            }
            function fun() {
                $('#loadover').show();
				if($finallist.length>0){
					$.ajax({
						type: "POST",
						contentType: "application/json; charset=utf-8",
						url: "Employee_Settings_Dynamic_new.aspx/savedata",
						data: "{'data':'" + JSON.stringify($finallist) + "'}",
						dataType: "json",
						success: function (data) {
							$('#loadover').hide();
							if (data.d == 'Sucess')
								alert("Employee Setting has been updated successfully!!!");                        
						},
						error: function (data) {
							alert(JSON.stringify(data));
						}
					});
				}
				else{
					window.location.reload();
				}
				
            }
            function Filter() {
                $('#FilterModal').modal('toggle');                
            }
        </script>
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
            <label class="" style="cursor: pointer; text-align: right;" id="filter_chklst"><i style="padding: 5px;" class="fa fa-plus"></i>Add Settings</label>
            <label onclick="Filter()" style="cursor: pointer;"><i style="padding: 5px;" class="fa fa-filter"></i>Filter</label>
        </div>
        <div>
            <h3 style="margin-top: 0px; margin-bottom: 0px; margin-inline: 20px;">Employee Setting</h3>

        </div>
        <div class="container">
            <div class="row" >
                <div class="panel panel-primary filterable" style="overflow-y: auto;max-height: 500px;">
                    <table id="table1" class="tbl">
                        <thead style="background: #19a4c6;position: sticky;z-index: 6;" class="TotalHead">
                            <tr class="filters" id="thHead" style="height: 80px; text-align: left">
                                <th style="position: sticky; left:13px;z-index: 4; max-width: 300px; min-width: 300px; padding: 15px; background: #19a4c6;">Employee Name
                                <div colspan="3" style="width: 150px;">
                                    <input placeholder="Search" type="text" id="txtser" style="height: 25px;">
                                        <span><i class="glyphicon glyphicon-search"></i></span>
                                    </input>
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
                            <div class="modal-header">
                                <button type="button" id="btnClose" class="close" style="margin-top: -12px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>                                    
                                <div style="margin-top: 10px;">
                                        <button type="button" class="btn btn-primary col-lg-8 btn-circle" id="btn_Add"><i class="fa fa-plus"></i></button>
                                        <button type="button" class="btn btn-danger col-lg-offset-10 btn-circle" id="btn_rmv"><i class="fa fa-trash-o"></i></button>
                                    </div>
                                </div>
                                <div class="modal-body Filtermodal">
                                    

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

