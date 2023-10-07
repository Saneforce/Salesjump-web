<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Allowance_Entry_New.aspx.cs" Inherits="MasterFiles_Allowance_Entry_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        /*.gvHeader th {
            padding: 4px 4px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }

        .gvRow td {
            padding: 4px 4px;
            background-color: #ffffff;
            border: 1px solid #bbb;
            text-align: left;
        }       

        .gvAltRow td {
            padding: 4px 4px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
            text-align: left;
        }*/
        tbody > tr:hover {
            background-color: #83e6c3;
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

        input[type='text'] {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var Hq_Typ = ['Metro', 'Major', 'Others'];
        var Typeval = 0;
        var TypevalSF = 0;
        var PrvsAllowDet = [];
        $(document).ready(function () {
            let edt = new Date();
            var dd = edt.getDate();
            var mm = edt.getMonth() + 1;
            var yyyy = edt.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            document.getElementById("recpt-dt").setAttribute("min", yyyy + '-' + mm + '-' + dd);
            alowanceLoad();
            function alowanceLoad() {
                $('#loadover').show();
                var len = 0;
                var mnth_cnt = 0;
                var dly_cnt = 0;
                $('#Designation tr').remove();
                $("#FieldForce tr").remove();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry_New.aspx/GetAllType",
                    dataType: "json",
                    success: function (data) {

                        len = data.d.length;
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                if (data.d[i].ALW_type == 1) {
                                    dly_cnt++;
                                }
                                if (data.d[i].ALW_type == 2) {
                                    mnth_cnt++;
                                }
                            }
                        }
                        var rowsp = (mnth_cnt) == 2 ? 0 : 1;
                        appendDesg(data.d, dly_cnt, mnth_cnt, rowsp);

                        var str = "<tr><th rowspan='4' >S.No </th><th rowspan='4' class='col-xs-2'>FieldForce</th><th  rowspan='4' class='col-xs-2'>HeadQuarters</th><th  colspan='" + (dly_cnt + 9) + "'>Daily</th>";
                        if (mnth_cnt > 0) str += "<th rowspan='" + (mnth_cnt + rowsp) + "' colspan='" + mnth_cnt + "'>Monthly</th>";
                        str += "</tr>";
                        $.each(Hq_Typ, function (key, val) {
                            str += "<th colspan='3'>" + val + "</th>";
                        });
                        str += "<tr class='alcode'>";
                        $.each(Hq_Typ, function (key, val) {
                            str += "<th><input type='hidden' name='Alw_Code' value='HQ_" + val.slice(0, 2) + "'/>HQ</th><th> <input type='hidden' name='Alw_Code' value='EX_" + val.slice(0, 2) + "'/>EX</th><th> <input type='hidden' name='Alw_Code' value='OS_" + val.slice(0, 2) + "'/>OS</th>";
                        });
                        //$('#FieldForce thead').append(str);
                        //var str1 = "<th><input type='hidden' name='Alw_Code' value='HQ1'/>HQ</th><th><input type='hidden' name='Alw_Code' value='EX1'/>EX</th><th > <input type='hidden' name='Alw_Code' value='OS1'/>OS</th>";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<th > <input type='hidden' name='Alw_Code' value='" + data.d[i].ALW_code + "'/>" + data.d[i].ALW_name + "</th>";
                        }
                        $("#FieldForce thead").append('<tr class="alcode">' + str + '</tr>');
                        $('#loadover').hide();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                if ('<%=DBase_EReport.Global.AllowanceType%>' == '0') {
                    $(".FieldForce").css("display", "block");
                    $("#Designation").css('display', 'block');
                }
                else if ('<%=DBase_EReport.Global.AllowanceType%>' == '1') {
                    $("#Designation").css('display', 'block');
                    $(".FieldForce").css('display', 'none');
                }
                var strDs = "";
                $("#Designation").append(strDs);
                strDs = "";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry_New.aspx/Get_Details",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strDs = "<td class='col-xs-2'> <input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].Designation_Short_Name + "</td> ";
                            $.each(Hq_Typ, function (key, val) {
                                strDs += "<td><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval + 1) + ")'/> </td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval + 2) + ")'/></td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval + 3) + ")'/></td>";
                                Typeval += 3;
                            });
                            Typeval++;
                            for (var j = 0; j < len; j++) {
                                strDs += "<td> <input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (j + Typeval) + ")'/> </td>";
                            }
                            strDs += "<td class='prvealw'><a class='viewdet' style='cursor: pointer;'>View</a></td>";
                            $("#Designation tbody").append("<tr class='gvRow'>" + strDs + "</tr>");
                            Typeval = 0;
                        }
                        if ('<%= Session["div_code"] %>' != '109')
                            $(".prvealw").hide();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                var strFF = "";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry_New.aspx/Get_FieldForce",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            //strFF = "<td>" + (i + 1) + " </td><td class='col-xs-2'>" + data.d[i].sf_name + "</td><td class='col-xs-2'><input type='hidden' name='sf_Code' value='" + data.d[i].sf_code + "'/><input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].sf_HQ + "</td><td > <input type='text' class='form-control' style='height:33px;' name='Alw_value'/> </td><td ><input type='text' class='form-control' style='height:33px; 'name='Alw_value'/></td><td ><input type='text' class='form-control' style='height:33px; ' name='Alw_value'/></td>";
                            strFF = "<td>" + (i + 1) + " </td><td class='col-xs-2'>" + data.d[i].sf_name + "</td><td class='col-xs-2'><input type='hidden' name='sf_Code' value='" + data.d[i].sf_code + "'/><input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].sf_HQ + "</td>";//<td > <input type='text' class='form-control' style='height:33px;' name='Alw_value'/> </td><td ><input type='text' class='form-control' style='height:33px; 'name='Alw_value'/></td><td ><input type='text' class='form-control' style='height:33px; ' name='Alw_value'/></td>
                            $.each(Hq_Typ, function (key, val) {
                                strFF += "<td><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (TypevalSF + 1) + ")'/> </td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (TypevalSF + 2) + ")'/></td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (TypevalSF + 3) + ")'/></td>";
                                TypevalSF += 3;
                            });
                            TypevalSF++;
                            for (var j = 0; j < len; j++) {
                                strFF += "<td ><input type='text'  name='Alw_value' class='form-control' style='height:33px;'/></td>";
                            }
                            $("#FieldForce tbody").append("<tr class='gvRow'>" + strFF + "</tr>");
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry_New.aspx/GetAllow_Values",
                    dataType: "json",
                    success: function (data) {
                        Filldata(data);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry_New.aspx/GetAllow_Values_FF",
                    dataType: "json",
                    success: function (data) {
                        Filldata_FF(data);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $("input").live("keypress", function (e) {
                    var num = e.keyCode;
                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                        return false;
                    }
                });
            }
            $(".viewdet").on('click', function () {
                var desgcode = $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString();
                var Allstr = '';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Entry_New.aspx/getPrvsDet",
                    data: "{'DesgCode':'" + desgcode + "'}",
                    asyc: false,
                    dataType: "json",
                    success: function (data) {
                        $("#loader").show();
                        PrvsAllowDet = JSON.parse(data.d) || [];
                        if (PrvsAllowDet.length > 0) {
                            $("#tblAlw tbody").html('');
                            Allstr = '';
                            $.each(PrvsAllowDet, function (key, val) {
                                Allstr += "<tr><td>" + (Number(key) + 1) + "</td><td>" + val.SfName + "</td><td>" + val.Created_Date + "</td><td>" + val.Effective_Date + "</td><td>" + val.Allowance_Value + "</td><td>" + val.Allowance_Name + "</td></tr>";
                            })
                            $("#tblAlw tbody").append(Allstr);
                            $("#MyPopup").modal('toggle');
                        }
                    },
                    error: function (data) {
                        $("#loader").hide();
                        alert(JSON.stringify(data));
                    }
                });
                $("#loader").hide();
            })
            $(document).on('click', '.btnsave', function () {
                if ($("#recpt-dt").val() == '') {
                    alert("Select Effective Date!!!")
                    $("#recpt-dt").focus();
                    return;
                }
                Typeval = 0;
                $('#loadover').show();
                var dtls_tab = document.getElementById("Designation");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[3].cells.length;
                var arr = [];
                var btndate = $('#recpt-dt').val();
                var fnldate = btndate;
                $('#Designation tbody tr').each(function () {
                    var fd = 1;
                    for (var i = 1; i < Ncols; i++) {
                        //console.log($(this).closest('table').find('.alcode').find('th'));
                        arr.push({
                            Des_code: $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString(),
                            //SF_Code: $(this).closest('tr').find('input[name=sf_Code]').val().toLowerCase().toString(),
                            Alw_code: $(this).closest('table').find('.alcode').find('th').eq(i - 1).find('input[name=Alw_Code]').val(),
                            values: (($(this).children('td').eq(i).find('input[name=Alw_value]').val()) == "") ? "" : ($(this).children('td').eq(i).find('input[name=Alw_value]').val())
                        });
                    }
                });

                var dtls_tab = document.getElementById("FieldForce");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[3].cells.length;
                var Fieldforce = [];
                var btndate = $('#recpt-dt').val();
                var fnldate = btndate;
                $('#FieldForce tbody tr').each(function () {
                    var fd = 1;
                    for (var i = 3; i < Ncols + 3; i++) {
                        console.log($(this).closest('table').find('.alcode').find('th'));
                        Fieldforce.push({
                            Des_code: $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString(),
                            SF_Code: $(this).closest('tr').find('input[name=sf_Code]').val().toLowerCase().toString(),
                            Alw_code: $(this).closest('table').find('.alcode').find('th').eq(i - 3).find('input[name=Alw_Code]').val(),
                            values: (($(this).children('td').eq(i).find('input[name=Alw_value]').val()) == "") ? "" : ($(this).children('td').eq(i).find('input[name=Alw_value]').val())

                        });
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Entry_New.aspx/savedata",
                    data: "{'data':'" + JSON.stringify(arr) + "',FieldforceData:'" + JSON.stringify(Fieldforce) + "','date':'" + fnldate + "'}",
                    dataType: "json",
                    success: function (data) {
                        $("#loader").hide();
                        alert("Allowance has been updated successfully!!!");
                        alowanceLoad();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
        });

        function appendDesg(data, dly_cnt, mnth_cnt, rowsp) {
            var str = "<tr><th rowspan='4' class='col-xs-2'><p>Designation</p></th><th colspan='" + (dly_cnt + 9) + "'>Daily</th>";

            if (mnth_cnt > 0) str += "<th rowspan='" + (mnth_cnt + rowsp) + "' colspan='" + mnth_cnt + "'>Monthly</th>";
            str += "<th rowspan='4' class='prvealw' >Previous Allowance Details</th></tr>";
            $('#Designation thead').append(str);
            if ('<%= Session["div_code"] %>' != '109')
                $(".prvealw").hide();
            str = "";
            //str += '<tr>';
            $.each(Hq_Typ, function (key, val) {
                str += "<th colspan='3'>" + val + "</th>";
            })
            //str += "</tr>";
            str += "<tr class='alcode'>";
            $.each(Hq_Typ, function (key, val) {
                str += "<th><input type='hidden' name='Alw_Code' value='HQ_" + val.slice(0, 2) + "'/>HQ</th><th> <input type='hidden' name='Alw_Code' value='EX_" + val.slice(0, 2) + "'/>EX</th><th> <input type='hidden' name='Alw_Code' value='OS_" + val.slice(0, 2) + "'/>OS</th>";
            });

            for (var i = 0; i < data.length; i++) {
                str += "<th rowspan='2'><input type='hidden' name='Alw_Code' value='" + data[i].ALW_code + "'/>" + data[i].ALW_name + "</th>";
            }
            $('#Designation thead').append('<tr>' + str + '</tr>');
        }

        function Filldata_FF(data) {
            var dtls_tab = document.getElementById("FieldForce");
            var nrows1 = dtls_tab.rows.length;
            var Ncols = dtls_tab.rows[3].cells.length;
            //  alert(JSON.stringify(data.d));
            if (data.d.length > 0) {
                $('#FieldForce tbody tr').each(function () {
                    var sfCode = $(this).closest('tr').find("[name='sf_Code']").val();
                    dtd = data.d.filter(function (a) {
                        return (a.SF_Code.toLowerCase() == sfCode.toLowerCase());
                    });
                    for (var i = 0; i < dtd.length; i++) {
                        var fd = 1;
                        for (var col = 3; col < Ncols + 3; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == dtd[i].Des_code) {
                                if ($(this).closest('table').find('.alcode').find('th').eq(col - 3).find('input[name=Alw_Code]').val() == dtd[i].Alw_code) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(dtd[i].values);
                                    // console.log(data.d[i].Alw_code + ':' + data.d[i].values);
                                }
                            }
                        }
                    }
                });
            }
        }
        function Filldata(data) {
            var dtls_tab = document.getElementById("Designation");
            var nrows1 = dtls_tab.rows.length;
            var Ncols = dtls_tab.rows[3].cells.length;
            if (data.d.length > 0) {
                document.getElementById("recpt-dt").setAttribute("min", data.d[0].EffDt);
                $("#recpt-dt").val(data.d[0].EffDt);
                for (var i = 0; i < data.d.length; i++) {
                    $('#Designation tbody tr').each(function () {
                        var fd = 1;
                        for (var col = 1; col < Ncols; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == data.d[i].Des_code) {
                                if ($(this).closest('table').find('.alcode').find('th').eq(col - 1).find('input[name=Alw_Code]').val() == data.d[i].Alw_code) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(data.d[i].values);
                                }
                            }
                        }

                    });
                }
            }

        }

        function FetchData(button, idx) {
            var fareval = $(button).val();
            var id = '#' + button;
            var ff = $(button).closest("tr").find("input:hidden").val();
            $('#FieldForce').find('tr:has(td)').each(function () {
                var sf_code = $(this).find("td:eq(2) :input[name=Des_Code]").val();
                if (sf_code == ff) {
                    //if($(this).find("td:eq(" + (idx + 2) + ") :input").val()=='')
                    $(this).find("td:eq(" + (idx + 2) + ") :input").val(fareval);
                }
            });
        }
    </script>
    <form id="Allowancefrm" runat="server">
        <div id="MyPopup" class="modal fade" role="dialog" style="z-index: 100000;">
            <div class="modal-dialog" style="width: 80%; text-align: center; margin: auto; line-height: 0px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" style="font-weight: bold; color: #ee3939;">Allowance Details</h4>
                    </div>
                    <div id="d1" style="overflow: auto; height: 558px; font-size: inherit;text-align: left;" class="card table-responsive">
                        <table id="tblAlw" class="table">
                            <thead>
                                <tr>
                                    <th>Sl No</th>
                                    <th>Sales Force Name</th>
                                    <th>Created Date</th>
                                    <th>Effective Date</th>
                                    <th>Allowance Value</th>
                                    <th>Allowance Name</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <input type="button" id="btnClosePopup" value="Close" class="btn btn-danger" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        <div class="row" style="margin-bottom: 1rem;">
            <div class="col-lg-12">
                <div class="col-sm-6">
                    <h2>Designation Allowance Entry</h2>
                </div>
                <div class="col-sm-3">
                    <label style="float: right;">Effective From</label>
                </div>
                <div class="col-sm-3">
                    <input type="date" autocomplete="off" class="form-control" id="recpt-dt" min="2021-09-08">
                </div>
            </div>
        </div>
        <div class="container" style="width: 100%; padding: 8px;">
            <div class="card" style="border: 1px solid #c1c1c1; margin-top: 0px;">
                <table id="Designation" class="table table-bordered" style="margin-bottom: 0px;">
                    <thead style="text-align: center;">
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="FieldForce card" style="overflow: scroll; display: none; height: 500px;">
                <table id="FieldForce" class="gvHeader">
                    <thead style="position: sticky; top: 0px; background-color: slategray; z-index: 5;">
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="row" style="text-align: center">
                <div class="col-sm-12 inputGroupContainer" style="margin-bottom: 5px;">
                    <a name="btnsave" id="btnsave" class="btn btn-primary btnsave" style="width: 100px">Save</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>


