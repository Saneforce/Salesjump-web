<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Allowance_Entry_New_Period.aspx.cs" Inherits="MasterFiles_Allowance_Entry_New_Period" %>


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
		th {
			text-align: center;
			color: #333;
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
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <script src="../alertstyle/jquery-confirm.min.js"></script>
    <script type="text/javascript">
        function rjalert(titles, contents, types) {
            if (types == 'error') {
                var tp = 'red';
                var icons = 'fa fa-warning';
                var btn = 'btn-red';
            }
            else {
                var tp = 'green';
                var icons = 'fa fa-check fa-2x';
                var btn = 'btn-green';
            }
            $.confirm({
                title: '' + titles + '',
                content: '' + contents + '',
                type: '' + tp + '',
                typeAnimated: true,
                autoClose: 'action|8000',
                icon: '' + icons + '',
                buttons: {
                    tryAgain: {
                        text: 'OK',
                        btnClass: '' + btn + '',
                        action: function () {

                        }
                    }
                }
            });
        }
        var Hq_Typ = ['Metro', 'Major', 'Others'];
		var Typeval = 0;
		var TypevalSF=0;
        $(document).ready(function () {
			if (JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Web_Auto == 1 && JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Process_Type==0) {
				 window.location.href = "Allowance_Entry_New.aspx";
			}
            let edt = new Date();
            var dd = edt.getDate();
            var mm = edt.getMonth() + 1;
            var yyyy = edt.getFullYear();
            var Periodic = [];
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Allowance_Entry_New_Period.aspx/FillPeriodic",
                dataType: "json",
                success: function (data) {
                    Periodic = JSON.parse(data.d);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
			 
            if ('<%=DBase_EReport.Global.AllowanceType%>' == '0') {
                $("#FieldForce").css("display", "block");
                $("#Designation").css('display', 'block');
            }
            else if ('<%=DBase_EReport.Global.AllowanceType%>' == '1') {
                $("#Designation").css('display', 'block');
                $("#FieldForce").css('display', 'none');
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
                    url: "Allowance_Entry_New_Period.aspx/GetAllType",
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
                        if (mnth_cnt > 0) str += "<th rowspan='" + (mnth_cnt + rowsp) + "' colspan='" + mnth_cnt + "'>Monthly</th><th rowspan='4'>Period List</th>";
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

                var strDs = "", Pstr = "";
                $("#Designation").append(strDs);
                strDs = "";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry_New_Period.aspx/Get_Details",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strDs = "<td class='col-xs-2'> <input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].Designation_Short_Name + "</td> ";
                            $.each(Hq_Typ, function (key, val) {
                                strDs += "<td><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval+1) + ")'/> </td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval+2) + ")'/></td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval+3) + ")'/></td>";
								Typeval += 3;
                            });
							Typeval++;
                            for (var j = 0; j < len; j++) {

                                strDs += "<td> <input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (j + Typeval) + ")'/> </td>";

                            }
                            Pstr = "";
                            Pstr = "<option value='0'>Nothing Select</option>";
                            for (var k = 0; k < Periodic.length; k++) {
                                Pstr += "<option value=" + Periodic[k].Period_Id + ">" + Periodic[k].Period_Name + "</option>";
                            }
                            strDs += "<td><select class='cls'>" + Pstr + "</select></td>";
                            $("#Designation tbody").append("<tr class='gvRow'>" + strDs + "</tr>");
							Typeval = 0;
                            $(".cls").selectpicker({
                                liveSearch: true
                            });
                        }

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
                    url: "Allowance_Entry_New_Period.aspx/Get_FieldForce",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strFF = "<td>" + (i + 1) + " </td><td class='col-xs-2'>" + data.d[i].sf_name + "</td><td class='col-xs-2'><input type='hidden' name='sf_Code' value='" + data.d[i].sf_code + "'/><input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].sf_HQ + "</td>";//<td > <input type='text' class='form-control' style='height:33px;' name='Alw_value'/> </td><td ><input type='text' class='form-control' style='height:33px; 'name='Alw_value'/></td><td ><input type='text' class='form-control' style='height:33px; ' name='Alw_value'/></td>
                            $.each(Hq_Typ, function (key, val) {
                                strFF += "<td><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (TypevalSF+1) + ")'/> </td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (TypevalSF+2) + ")'/></td><td ><input type='text' class='form-control' style='height:33px;' name='Alw_value' onkeyup='FetchData(this," + (Typeval+3) + ")'/></td>";
								TypevalSF+=3;
                            });
							TypevalSF++;
                            for (var j = 0; j < len; j++) {
                                strFF += "<td ><input type='text'  name='Alw_value' class='form-control' style='height:33px;'/></td>";
                            }
							Pstr = "";
                            Pstr = "<option value='0'>Nothing Select</option>";
                            for (var k = 0; k < Periodic.length; k++) {
                                Pstr += "<option value=" + Periodic[k].Period_Id + ">" + Periodic[k].Period_Name + "</option>";
                            }
                            strFF += "<td><select class='cls'>" + Pstr + "</select></td>";
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
                    url: "Allowance_Entry_New_Period.aspx/GetAllow_Values",
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
                    url: "Allowance_Entry_New_Period.aspx/GetAllow_Values_FF",
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
                nrows1 = dtls_tab.rows.length - 3;
                var Ncols = dtls_tab.rows[3].cells.length;
                var arr = [];
                var btndate = $('#recpt-dt').val();
                var fnldate = btndate;
                $('#Designation tbody tr').each(function (key) {
                    var fd = 1;
                    for (var i = 1; i < Ncols-1; i++) {
                        //console.log($(this).closest('table').find('.alcode').find('th'));
                        //var val = (i == 1) ? nrows1 : 3 + (i - 1);
						Alwcode = $(this).closest('table').find('.alcode').find('th').eq(i - 1).find('input[name=Alw_Code]').val();
                        arr.push({
                            Des_code: $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString(),
                            //SF_Code: $(this).closest('tr').find('input[name=sf_Code]').val().toLowerCase().toString(),
                            Alw_code: Alwcode,
                            values: (($(this).children('td').eq(i).find('input[name=Alw_value]').val()) == "") ? 0 : ($(this).children('td').eq(i).find('input[name=Alw_value]').val()),
							period: ((Alwcode == "HQ_Me") ? "" : (Alwcode == "EX_Me") ? "" : (Alwcode == "OS_Me") ? "" : (Alwcode == "HQ_Ma") ? "" : (Alwcode == "EX_Ma") ? "" : (Alwcode == "OS_Ma") ? "" : (Alwcode == "HQ_Ot") ? "" : (Alwcode == "EX_Ot") ? "" : (Alwcode == "OS_Ot") ? "" : $('tr:eq("' + (key + 3) + '")').find('td:eq("' + (Ncols - 1) + '")').find('.cls option:selected').val())
                            //period: $('tr:eq("' + (key + 3) + '")').find('td:eq("' + (Ncols - 1) + '")').find('.cls option:selected').val()
                        });
                    }
                });

                var dtls_tab = document.getElementById("FieldForce");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[3].cells.length;
                var Fieldforce = [];
                var btndate = $('#recpt-dt').val();
                var fnldate = (btndate != '1900-01-01' || btndate != '') ? btndate : rjalert('Alert!!!', 'Select Effective Date', 'error');
                $('#FieldForce tbody tr').each(function () {
                    var fd = 1;
                    //Ncols += 3;
                    for (var i = 3; i < Ncols + 3; i++) {
                        console.log($(this).closest('table').find('.alcode').find('th'));
						Alwcode = $(this).closest('table').find('.alcode').find('th').eq(i - 3).find('input[name=Alw_Code]').val();
                        Fieldforce.push({
                            Des_code: $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString(),
                            SF_Code: $(this).closest('tr').find('input[name=sf_Code]').val().toLowerCase().toString(),
                            Alw_code: Alwcode,//$(this).closest('table').find('.alcode').find('th').eq(i - 3).find('input[name=Alw_Code]').val(),
                            values: (($(this).children('td').eq(i).find('input[name=Alw_value]').val()) == "") ? 0 : ($(this).children('td').eq(i).find('input[name=Alw_value]').val()),
							period: ((Alwcode == "HQ_Me") ? "" : (Alwcode == "EX_Me") ? "" : (Alwcode == "OS_Me") ? "" : (Alwcode == "HQ_Ma") ? "" : (Alwcode == "EX_Ma") ? "" : (Alwcode == "OS_Ma") ? "" : (Alwcode == "HQ_Ot") ? "" : (Alwcode == "EX_Ot") ? "" : (Alwcode == "OS_Ot") ? "" : $(this).closest('table').find('tr:eq("' + (key + 4) + '")').find('td:eq("' + (Ncols + 3) + '")').find('.cls option:selected').val())
                        });
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Entry_New_Period.aspx/savedata",
                    data: "{'data':'" + JSON.stringify(arr) + "',FieldforceData:'" + JSON.stringify(Fieldforce) + "','date':'" + fnldate + "'}",
                    dataType: "json",
                    success: function (data) {
                        $("#loader").hide();
			if(data.d=='')
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

            if (mnth_cnt > 0) str += "<th rowspan='" + (mnth_cnt + rowsp) + "' colspan='" + mnth_cnt + "'>Monthly</th><th rowspan='" + (mnth_cnt + rowsp) + 1 + "'>Period List</th>";
            str += "</tr>";
            $('#Designation thead').append(str);
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
            str += "</tr>";
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
                    $('#Designation tbody tr').each(function (Key) {
                        var fd = 1;
                        for (var col = 1; col < Ncols; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == data.d[i].Des_code) {
                                if ($(this).closest('table').find('.alcode').find('th').eq(col - 1).find('input[name=Alw_Code]').val() == data.d[i].Alw_code) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(data.d[i].values);
                                    $(this).find('td:eq("' + (Ncols - 1) + '")').find('.cls').val(data.d[i].period).selectpicker('refresh');
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
            <div class="card" style="border: 0px; margin-top: 0px;">
                <table id="Designation" class="table table-bordered" style="margin-bottom: 0px;">
                    <thead <%--style="background-color: #8bd1e2;"--%>>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <table id="FieldForce" style="display: none;" class="gvHeader">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row" style="text-align: center">
                <div class="col-sm-12 inputGroupContainer" style="margin-bottom: 5px;">
                    <a name="btnsave" id="btnsave" class="btn btn-primary btnsave" style="width: 100px">Save</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>




