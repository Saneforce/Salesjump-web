<%@ Page Title="Expense Entry" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Expense_Entry.aspx.cs" Inherits="MasterFiles_Expense_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #Expense_Table, #mon_Table1 {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #Expense_Table td, #Expense_Table th, #mon_Table1 td, #mon_Table1 th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #Expense_Table tr:nth-child(even), #mon_Table1 tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #Expense_Table tr:hover, #mon_Table1 tr:hover {
                background-color: #ddd;
            }

            #Expense_Table th, #mon_Table1 th {
                padding-top: 12px;
                padding-bottom: 12px;
                padding-left: 5px;
                padding-right: 5px;
                text-align: center;
                background-color: #475677;
                color: white;
            }

        input[type=text] {
            text-align: right;
        }

        tfoot label {
            width: 100%;
            margin-bottom: 0px;
        }

        #Expense_Table tfoot th, #Expense_Table tfoot td, #mon_Table1 tfoot th, #mon_Table1 tfoot td {
            background-color: #c1c1c1;
            color: #252525;
        }

        .css_sum, .css_tot, tfoot label {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var mode = $('#<%=hdnMode.ClientID%>').val();
            var terr_code = 0;
            var arr = [];
            var dayarr = [];
            var VDetail = [];
            var DDetail = [];
            var objmain = {};
            var AllwDet = [];
            var FareDet = [];
            var mon_arr = [];
            var mainarr = [];
            var appData = [];
            var selyear = 0;
            var selmonth = 0;
            var sf_code = 0;
            var CountOption = 0;
            if (mode == 0) {
                $('#btnctrl').show();
                $('#maindiv').hide();
            }
            else {
                $('#btnctrl').hide();
                $('#maindiv').show();
                alldataload();
            }
            $(document).on("keypress", function (e) {
                var x = e.which || e.keyCode;
                if (x == 13) {
                    e.preventDefault();
                    return false;
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Entry.aspx/Get_Year",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddl_year");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['years']).html(this['years']));
                    });
                },
                error: function (jqXHR, exception) {
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }
            });
            $(document).on('click', '#btnGo', function () {
                $('#maindiv').show();
                alldataload();
            });
            calcAddi = function (x) {
                var t = parseFloat($(x).val());
                if (isNaN(t)) t = 0;

                tb = $(x).closest('table');
                tt = $(tb).find('.css_sum');

                mRw = $(tb).closest('tr');
                cTC = $(mRw).find(".css_tot");
                console.log($(cTC).html());

                var pv = parseFloat($(x).attr('pv'));
                if (isNaN(pv)) pv = 0;

                var tot = parseFloat($("#lbltot").text());
                if (isNaN(tot)) tot = 0;

                var Ttot = parseFloat($("#lblTtot").text());
                if (isNaN(Ttot)) Ttot = 0;

                var Mtot = parseFloat($("#over_tot").text());
                if (isNaN(Mtot)) Mtot = 0;

                var stot = parseFloat($(tt).text());
                if (isNaN(stot)) stot = 0;

                var ctot = parseFloat($(cTC).text());
                if (isNaN(ctot)) ctot = 0;

                tot = (tot - pv) + Number(t);
                stot = (stot - pv) + Number(t);
                ctot = (ctot - pv) + Number(t);
                Ttot = (Ttot - pv) + Number(t);
                Mtot = (Mtot - pv) + Number(t);

                $(x).attr('pv', t);
                $("#lbltot").html(tot);
                $("#lblTtot").html(Ttot);
                $("#over_tot").html(Mtot || 0);
                $(tt).html(stot);
                $(cTC).html(ctot);
            }

            $(document).on('keyup', 'input[name=addeamt]', function () {
                calcAddi(this)
            });

            calcMonAddi = function (x) {
                var t = parseFloat($(x).val());
                if (isNaN(t)) t = 0;

                var pv = parseFloat($(x).attr('pv'));
                if (isNaN(pv)) pv = 0;
                $(x).attr('pv', t);

                var Mtot = parseFloat($("#mon_tot").text());
                if (isNaN(Mtot)) Mtot = 0;

                var OMtot = parseFloat($("#over_tot").text());
                if (isNaN(OMtot)) OMtot = 0;

                Mtot = (Mtot - pv) + Number(t);
                OMtot = (OMtot - pv) + Number(t);

                $("#mon_tot").html(Mtot);
                $("#over_tot").html(OMtot || 0);
            }

            $(document).on('keyup', 'input[name=addmamt]', function () {
                calcMonAddi(this);
            });

            $(document).on('click', 'a[name=btnadd]', function () {
                if ($(this).text().toString() == "+") {
                    $(this).text("-");
                    $(this).closest('tr').after('<tr class="sel_row"><td style="width:100px;"><select name="addetype" class="form-control alws_code"/></td><td style="width:80px;" ><input class="form-control row_sum2" type="text" name="addeamt"/></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr>');
                    loadddl(arr, $(this).closest('tbody'))
                }
                else {
                    x = $(this).closest('tr').find('input[name=addeamt]'); $(x).val('0');
                    calcAddi(x);
                    $(this).closest('tr').remove();
                }
            });

            $(document).on('click', '.btnsave', function () {
                var approve = 0;
                if ($(this).attr('id').toString() == "btndaysave") {
                    approve = 0;
                }

                if ($(this).attr('id').toString() == "btnapproval") {
                    approve = 1;
                }

                dayarr = [];
                mainarr = [];
                if (sf_code.length <= 0) {
                    alert('select FieldForce');
                    return false;
                }

                if (selyear.length <= 0) {
                    alert('select Year');
                    return false;
                }

                if (selmonth.length <= 0) {
                    alert('select Year');
                    return false;
                }

                mainarr.push({
                    sf_code: sf_code,
                    exp_year: selyear,
                    exp_month: selmonth,
                    exp_amt: $('#over_tot').text()
                });

                var dtls_tab = document.getElementById("Expense_Table");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;
                if (nrows1 > 1) {
                    var ch = true;
                    var arr = [];
                    $('#Expense_Table > tbody > tr').each(function () {
                        dayarr = [];
                        $(this).find('.row_sum').each(function () {
                            dayarr.push({
                                value: $(this).text() || $(this).val(),
                                alw_code: $(this).attr('alw_code'),
                                alw_name: $(this).attr('alw_name'),
                                user_enter: 0
                            });
                        });
                        $(this).find('.sel_row').each(function () {
                            dayarr.push({
                                value: $(this).find('input').val(),
                                alw_code: $(this).find('select').val(),
                                alw_name: $(this).find('select').text(),
                                user_enter: 1

                            });
                        });

                        arr.push({
                            exp_dt: $(this).children('td').eq(0).text(),
                            work_type: $(this).children('td').eq(1).text(),
                            work_anme: $(this).children('td').eq(1).text(),
                            place_work: $(this).children('td').eq(2).text(),
                            al_type: $(this).children('td').eq(3).text(),
                            distance: $(this).children('td').eq(4).text(),
                            fare: $(this).children('td').eq(5).text(),
                            exp_da: $(this).children('td').eq(6).text(),
                            dly_tot: $(this).children('td:last-child').text(),
                            adddtls: dayarr
                        });
                    });
                }

                var dtls_tab = document.getElementById("Expense_Table");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;
                if (nrows1 > 1) {
                    var ch = true;
                    var mon_arr = [];
                    $('#mon_Table1 > tbody > tr').each(function () {
                        var mon_val = 0;
                        if ($(this).children('td').eq(0).find('input[name=user_enter]').val().toLowerCase().toString() == "0" || $(this).children('td').eq(1).find('input[name=addmamt]').val() == undefined) {
                            mon_val = $(this).children('td').eq(1).text();
                        }
                        else {
                            mon_val = $(this).children('td').eq(1).find('input[name=addmamt]').val().toLowerCase().toString();
                        }

                        mon_arr.push({
                            alw_code: $(this).children('td').eq(0).find('input[name=Alw_Code]').val().toLowerCase().toString(),
                            user_enter: $(this).children('td').eq(0).find('input[name=user_enter]').val().toLowerCase().toString(),
                            value: mon_val
                        });
                    });
                }

                objmain['MTED'] = mainarr
                objmain['TED'] = arr;
                objmain['MED'] = mon_arr;
                if (arr.length <= 0) {
                    alert('No Records Fount');
                    return false;
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Expense_Entry.aspx/SaveDate",
                    data: "{'data':'" + JSON.stringify(objmain) + "','savemode':'" + approve + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });
            });

            function alldataload() {
                var tot_arr = [];
                arr = [];
                var len = 0;
                var mon_len = 0;
                $("#Expense_Table tr").remove();
                $("#mon_Table1 tr").remove();
                if (mode == 0) {
                    selyear = $("#ddl_year").val();
                    if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                    selmonth = $("#dll_month").val();
                    if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }
                    sf_code = '<%= Session["Sf_Code"] %>';
                }
                else {
                    selmonth = $('#<%=hdnMonth.ClientID%>').val();
                    selyear = $('#<%=hdnYear.ClientID%>').val();
                    sf_code = $('#<%=hdnsfcode.ClientID%>').val();
                }

                $("#container").show();
                var str = "<th style='width:100px' >Date</th><th style='width:150px'>Work Type</th><th style='width:300px'>Worked Place</th><th>Al Type</th><th>Distance</th><th>Fare</th><th>DA</th>";
                var str_foot = "<th colspan='3'>Total</th><th><label/></th><th><label/></th><th><label/></th><th><label/></th>";
                $('#mon_Table1 thead').append("<tr><th>Name</th><th style='min-width: 150px;'>Values</th></tr>");
                $('#mon_Table1 tfoot').append("<tr><td>Total</td><td><label id='mon_tot'/></td></tr>");

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetVisitDetails",
                    data: "{'sf_code':'" + sf_code + "','years':'" + selyear + "','months':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        VDetail = data;
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetDistanceDetails",
                    data: "{'sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        DDetail = data;
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetfAREDetails",
                    data: "{'sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        FareDet = data;
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetTerr_Code",
                    data: "{'sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        terr_code = data.d;
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetAllType",
                    dataType: "json",
                    success: function (data) {
                        if (arr.length <= 0) {
                            arr.push({ alw_code: '0', alw_name: '--Select--' });
                        }
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                var mon_str = "";
                                if (data.d[i].ALW_type == 1) {
                                    if (data.d[i].user_enter == 0) {
                                        len++;
                                        str += "<th><input type='hidden' name='Alw_Code' value='" + data.d[i].ALW_code + "'/>" + data.d[i].ALW_name + "</th>";
                                        str_foot += "<th><label/></th>";
                                    }
                                    else {
                                        arr.push({ alw_code: data.d[i].ALW_code, alw_name: data.d[i].ALW_name });
                                    }
                                }
                                else {
                                    if (data.d[i].user_enter == 0) {
                                        mon_str += '<td><input type="hidden" name="Alw_Code" value="' + data.d[i].ALW_code + '"/><input type="hidden" name="user_enter" value="' + data.d[i].user_enter + '"/>' + data.d[i].ALW_name + '</td><td class="css_sum"></td>';
                                        $('#mon_Table1 tbody').append("<tr>" + mon_str + "</tr>");
                                    }
                                    else {
                                        mon_str += '<td><input type="hidden" name="Alw_Code" value="' + data.d[i].ALW_code + '"/><input type="hidden" name="user_enter"  value="' + data.d[i].user_enter + '"/>' + data.d[i].ALW_name + '</t><td  style="width:80px;"><input class="form-control css_sum" type="text"  name="addmamt"/> </td>';
                                        $('#mon_Table1 tbody').append("<tr>" + mon_str + "</tr>");
                                    }
                                }
                            }
                        }
                        CountOption = arr.length;
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $('#Expense_Table thead').append("<tr>" + str + "<th class='adddetals' style='width:300px;'>Additional</th><th style='min-width: 100px;'>Total</th></tr>");
                $('#Expense_Table tfoot').append("<tr>" + str_foot + "<th><label id='lbltot'/></th><th><label id='lblTtot'/></th></tr>");


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetExpanceDetails",
                    data: "{'sf_code':'" + sf_code + "','years':'" + selyear + "','months':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                var str2 = "";
                                str2 = "<td style='width:100px'>" + data.d[i].Activity_Date + "</td><td style='width:150px'>" + data.d[i].WorkType_Name + "</td><td style='width:300px'>" + data.d[i].Route_Name + "</td><td></td><td></td><td class='css_sum '></td><td class='css_sum '></td>";
                                for (var l = 0; l < len; l++) {
                                    str2 += "<td class='css_sum row_sum' alw_code='" + $('#Expense_Table thead tr').find('th').eq(l + 7).find('input[name=Alw_Code]').val() + "' alw_name='" + $('#Expense_Table thead tr').find('th').eq(l + 7).text() + "'></td>";
                                }
                                str2 += "<td class='css_tot'></td>";
                                $('#Expense_Table tbody').append("<tr class='savecss'>" + str2 + "</tr>");
                            }
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $('#Expense_Table:first tbody tr').each(function () {
                    $(this).find('td:last').before('<td style="width:300px;"><table name="addtable" style="width:100%"><tbody><tr class="sel_row"><td><select  name="addetype" class="form-control alws_code"/></td><td style="width:80px;" ><input class="form-control row_sum2" type="text" name="addeamt" /></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr></tbody><tfoot><tr><td>Total</td><td class="css_sum"></td></tr></tfoot></table></td>');
                });

                loadddl(arr);

                var dtls_tab = document.getElementById("Expense_Table");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetExpanceValues",
                    data: "{'sf_code':'" + sf_code + "','years':'" + selyear + "','months':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllwDet = data.d;
                        if (data.d.length > 0) {
                            $('#Expense_Table tbody tr').each(function () {
                                var rowtot = 0;
                                for (var i = 0; i < data.d.length; i++) {
                                    for (var col = 0; col < Ncols - 1; col++) {

                                        if ($(this).closest('table').find('th').eq(col).find('input[name=Alw_Code]').val() == data.d[i].Allowance_Code) {
                                            $(this).find('td').eq(col).html(data.d[i].Allowance_Value);
                                            rowtot += Number(data.d[i].Allowance_Value);
                                        }
                                    }
                                }
                            });
                            $('#mon_Table1 tbody tr').each(function () {
                                var rowtot = 0;
                                for (var i = 0; i < data.d.length; i++) {
                                    if ($(this).find('td').eq(0).find('input[name=Alw_Code]').val() == data.d[i].Allowance_Code) {
                                        $(this).find('td').eq(1).html(data.d[i].Allowance_Value);
                                    }
                                }
                            });
                        }
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                expDets = [];
                Dis = 0; cAty = ''; fare = 0; Allw = 0; $cWT = ''; $FR = ''; ExpRWs = []; //$Dv = 0; $Fl = 0; $RtTwns = ''; $ONo = 1; $DwsSl = 1; $RvNo = 1;
                $Dv = 0, $Plc = '', $RtTwns = '', $PTwn = '', $pTR = '', $Fl = 0, $ONo = 1, $RvNo = 1, $tTFa = 0, $tTAD = 0, $tTBD = 0, $tTDh = 0, $ptTAD = 0, $ptTBD = 0, $DwsSl = 1, $pDwsSl = 1, $OSRT = 0, $StyPls = '', $SPls = '', $Pdt = ''

                function goto(fl) {
                    if (fl == 1)
                        if ($RtTwns.endsWith(',')) $RtTwns = $RtTwns.substring(0, $RtTwns.length - 1);

                    if ($RtTwns.indexOf(',') > -1) {
                        $lst = $RtTwns.split(',');
                        if ($lst.length > 1)
                            $FTw = $lst[$lst.length - 2];
                    } else {
                        $FTw = $RtTwns
                    }
                    $v = parseFloat(FILTER_DISTANCE($FTw, $TTw));
                    if (isNaN($v)) $v = 0;
                    itmd = {}
                    itmd.SF = sf_code;
                    itmd.Adt = $Adt
                    itmd.FT = $TTw;
                    itmd.TT = $FTw;
                    itmd.MTR = -1;
                    itmd.Dis = $v;
                    itmd.ONo = $ONo;
                    itmd.DwsSl = $DwsSl;
                    ExpRWs.push(itmd);
                    $Dv += $v;
                    $ONo += 1;
                    $CTW = $FTw;

                    if ($OSRT == 0 && sf_code == $FTw) $OSRTFL = 1;

                    if ($v > 100) {
                        $tTDh = $v - 100;
                        $tTAD = $tTAD + $tTDh;
                        $tTBD = $tTBD + ($v - $tTDh);
                    } else
                        $tTBD = $tTBD + $v;

                    if (sf_code == $FTw && $cWT != 'OS') {
                        if (($ptTAD + $ptTBD) < ($tTAD + $tTBD)) {
                            $ptTAD = $tTAD, $ptTBD = $tTBD, $pDs = $Dv, $pDwsSl = $DwsSl
                        }
                        $tTAD = 0, $tTBD = 0, $tTDh = 0, $Dv = 0, $DwsSl = $DwsSl + 1
                    }
                    $TTw = $FTw;
                    // if isnull(@NTw,'')='' set $NTw='s'  
                    if (FILTER_ROUTE($FTw, $NTw) == '' && $FTw != '' && $FTw != $NTw) { //&& $NTw!='' 
                        if ($RtTwns.indexOf(',') > -1) {
                            $RtTwns = replace($RtTwns, ',' + $FTw, '');
                            goto(0);
                        }
                    }
                    $FR = $FTw;
                }
                function FILTER_DISTANCE(fa, fb) {
                    dtd = DDetail.d.filter(function (a) {
                        return (a.Frm_Plc_Code == fa && a.To_Plc_Code == fb);
                    });
                    $cWT = (dtd.length > 0) ? dtd[0].Atype : 'HQ';
                    return (dtd.length > 0) ? dtd[0].Distance_KM : 0;
                }
                function FILTER_ROUTE(fa, fb) {
                    dtd = DDetail.d.filter(function (a) {
                        console.log(a.Routes.indexOf(fa));
                        return (a.Routes.indexOf(fa) > -1 && a.Routes.indexOf(fb, a.Routes.indexOf(fa)) > -1); //((a.Frm_Plc_Code == terr_code || a.Frm_Plc_Code == fa) && a.To_Plc_Code == fb);
                    });
                    return (dtd.length > 0) ? dtd[0].Routes.substring(('-' + dtd[0].Routes + '-').indexOf('-' + fa + '-'), ('-' + dtd[0].Routes + '-').indexOf('-' + fb + '-', ('-' + dtd[0].Routes + '-').indexOf('-' + fa + '-') + 1) + fb.length) + '-' : '';
                    // dtd[0].Routes.substring(('-' + dtd[0].Routes).indexOf(fa) + 1, ('-' + dtd[0].Routes).indexOf(fb, ('-' + dtd[0].Routes).indexOf(fa) + 1) + fb.length) 
                }

                for (var v = 0; v < VDetail.d.length; v++) {

                    $v = 0, $OSRTFL = 0;
                    if ($FR == '') $FR = sf_code;
                    if ($RtTwns == '') $RtTwns = sf_code + ',';


                    itm = VDetail.d[v]; nitm = VDetail.d[v + 1];
                    $Adt = itm.vdate;
                    $Twn = itm.route_code;
                    $Ntn = (nitm == undefined) ? '' : nitm.route_code;
                    $nDt = (nitm == undefined) ? '' : nitm.vdate;
                    $MTR = '';

                    //if @tWA<>'HQ' begin  

                    $sTwns = FILTER_ROUTE($FR, $Twn);
                    $sTwns1 = FILTER_ROUTE($Twn, $FR);

                    if ($sTwns1.length > 0) {
                        if ($sTwns.length > $sTwns1.length || $sTwns.length < 1) {
                            $MTR = $RvNo; $sTwns = $sTwns1
                        }
                    }

                    $FTw = $sTwns.substring(0, $sTwns.indexOf('-'));
                    $sTwns = $sTwns.substring($sTwns.indexOf('-') + 1, $sTwns.length);

                    RTF: while (true) {
                        if ($MTR != '') $MTR = $RvNo;
                        if ($sTwns.indexOf('-') > -1) {
                            $TTw = $sTwns.substring(0, $sTwns.indexOf('-'));
                            $sTwns = $sTwns.substring($sTwns.indexOf('-') + 1, $sTwns.length);
                        } else $TTw = $sTwns;

                        $NTw = ($sTwns.indexOf('-') > -1) ? $sTwns.substring(0, $sTwns.indexOf('-')) : ($sTwns != '') ? $sTwns : $Ntn;
                        $nDt = ($sTwns != '') ? $Adt : $Pdt;
                        ///$Pdt = $Adt
                        $v = 0
                        if ($Fl != 1) {
                            $v = parseFloat(FILTER_DISTANCE($FTw, $TTw));
                            if (isNaN($v)) $v = 0;
                            itmd = {}
                            itmd.SF = sf_code;
                            itmd.Adt = $Adt
                            itmd.FT = ($MTR == '') ? $FTw : $TTw;
                            itmd.TT = ($MTR == '') ? $TTw : $FTw;
                            itmd.MTR = $MTR;
                            itmd.Dis = $v;
                            itmd.ONo = $ONo;
                            itmd.DwsSl = $DwsSl;
                            ExpRWs.push(itmd);
                            $Dv += $v;
                            $ONo += ($MTR == '') ? 1 : 0;
                            $RvNo += ($MTR == '') ? 0 : 1;
                            $CTW = $TTw;
                            $StyPls += $FTw; $SPls = $FTw;
                            if ($v > 100) {
                                $tTDh = $v - 100;
                                $tTAD = $tTAD + $tTDh;
                                $tTBD = $tTBD + ($v - $tTDh);
                            } else
                                $tTBD = $tTBD + $v;
                        }
                        $Fl = 0;
                        if ($sTwns != '') {
                            if ($RtTwns.endsWith(',') == false && $RtTwns != '') $RtTwns = $RtTwns + ',';
                            $RtTwns = $RtTwns + $TTw + ',';
                            $FTw = $TTw;
                            continue RTF;
                        }
                        break;
                    }

                    $TTw = $Twn; $NTw = $Ntn;
                    if ($Adt != $nDt && $cWT != 'OS') $NTw = ''   //and @sWT<>'OS'
                    if ($RtTwns.endsWith(',') == false && $RtTwns != '') $RtTwns = $RtTwns + ','
                    $RtTwns = $RtTwns + $TTw + ','
                    if ((FILTER_ROUTE($TTw, $NTw) != '' || FILTER_ROUTE($NTw, $TTw) != '') && $TTw != '' && $NTw != '')
                        $FR = $TTw;
                    else {
                        $RtTwns = FILTER_ROUTE($NTw, $TTw).replace(/-/g, ",");

                        dtd = DDetail.d.filter(function (a) {
                            return ((',' + $RtTwns + ',').indexOf(',' + a.Frm_Plc_Code + ',') > -1 && a.To_Plc_Code == $NTw);
                        });
                        $tFR = (dtd.length > 0) ? dtd[0].Frm_Plc_Code : '';
                        if ($tFR == '') {
                            if (FILTER_ROUTE(sf_code, $TTw) != '') $tFR = sf_code;
                        }
                        if ($tFR != '' && (',' + $RtTwns + ',').indexOf(',' + $NTw + ',') < 0 && $NTw != $TTw) {
                            $v = parseFloat(FILTER_DISTANCE($TTw, $tFR));
                            if (isNaN($v)) $v = 0;
                            if ($v > 0) {
                                itmd = {}
                                itmd.SF = sf_code;
                                itmd.Adt = $Adt
                                itmd.FT = ($MTR == '') ? $FTw : $TTw;
                                itmd.TT = ($MTR == '') ? $TTw : $FTw;
                                itmd.MTR = $MTR;
                                itmd.Dis = $v;
                                itmd.ONo = $ONo;
                                itmd.DwsSl = $DwsSl;
                                ExpRWs.push(itmd);
                                $Dv += $v;
                                $ONo += ($MTR == '') ? 1 : 0;
                                $RvNo += ($MTR == '') ? 0 : 1;
                                $CTW = $TTw;
                                $StyPls += $FTw; $SPls = $FTw;
                                if ($v > 100) {
                                    $tTDh = $v - 100;
                                    $tTAD = $tTAD + $tTDh;
                                    $tTBD = $tTBD + ($v - $tTDh);
                                } else
                                    $tTBD = $tTBD + $v;

                                if (sf_code = $tFR && $WA != 'OS') {
                                    if (($ptTAD + $ptTBD) < ($tTAD + $tTBD)) {
                                        $ptTAD = $tTAD; $ptTBD = $tTBD; $pDs = $Dv; $pDwsSl = $DwsSl
                                    }
                                    $tTAD = 0; $tTBD = 0; $tTDh = 0; $Dv = 0; $DwsSl = $DwsSl + 1;
                                }
                            } else {
                                goto(1);
                            }
                            if ((',' + $RtTwns + ',').indexOf(',' + $NTw + ',') < 2) {
                                $RtTwns = $tFR
                            } else {
                                $RtTwns = $RtTwns.substring(0, (',' + $RtTwns + ',').indexOf(',' + $tFR + ',') + $tFR.length);
                            }
                            $FR = $tFR
                        }
                        else if ($NTw != $TTw) {
                            goto(1)
                        } else {
                            $FR = $FTw
                            $Fl = 1;
                        }
                    }

                    if ($Pdt != $Adt) {
                        $OSRT = $OSRT + 1
                        //if ($WA!='OS') $OSRT=0  
                        if (($ptTAD + $ptTBD) > ($tTAD + $tTBD)) {
                            $tTAD = $ptTAD; $tTBD = $ptTBD; $Dv = $pDs
                        }

                        //delete from tbExpRoutDet where SF_code=@SF and Adt=@Adt and DtSl<>@pDwsSl  
                        //declare @tmpWA varchar(20) 
                        //set @tmpWA=@WA  

                        //if @cWA='HQ' and @WA<>'HQ' and @OSRTFL=1 select @WA='EX',@OSRTFL=0,@OSRT=0  
                        /*if @Ds='mr' begin  
                        set @tTFa=@tTBD*@fa  
                        set @tTFa=@tTFa+(@tTAD*@Afa)  
                        }else [*/
                        $tTFa = $Dv * fare
                        //}  


                        if (nitm != undefined) {
                            Aty = itm.vtype.replace(/-EX/g, '');
                            nAty = nitm.vtype.replace(/-EX/g, '');
                            if (Aty == 'OS' && nAty != 'OS') {
                                cAty = 'EX'
                            }
                            else {
                                cAty = Aty
                            }
                        }
                        else {
                            cAty = itm.vtype.replace(/-EX/g, '');
                        }
                        //if (nitm == undefined || itm.vdate != nitm.vdate) {
                        fDA = AllwDet.filter(function (a) {
                            return (a.Allowance_Code == cAty + '1');
                        });
                        Allw = (fDA.length > 0) ? fDA[0].Allowance_Value : 0;
                        fare = (FareDet.d.length > 0) ? FareDet.d[0].Fare : 0;
                        expDTDet = {};
                        expDTDet.vDt = itm.vdate;
                        expDTDet.Aty = cAty;
                        expDTDet.Dist = $Dv;
                        expDTDet.fare = $Dv * fare;
                        expDTDet.Allw = Allw;
                        expDets.push(expDTDet);
                        $Dv = 0; Dis = 0; cAty = ''; fare = 0; Allw = 0;
                        // }

                        //insert into tExpeDisDet select @SF,@Adt,isnull(@TWna,''),@WA,case when @tmpWA='OS' then 0 else round(isnull(@Dv,0),0) End,case when @tmpWA='OS' then round(isnull(@Dv,0),0) else @tTFa End  
                        $Dv = 0; $Plc = ''; $tTBD = 0; $tTAD = 0; $ptTAD = 0; $ptTBD = 0; $DwsSl = 1; $pDwsSl = 1; $StyPls = ''
                    }
                    $Pdt = itm.vdate;
                }


                console.log(ExpRWs);

                /*dtd = DDetail.d.filter(function (a) {
                return ((a.Frm_Plc_Code == terr_code || a.Frm_Plc_Code == sf_code) && a.To_Plc_Code == VDetail.d[v].route_code);
                });
                if (dtd.length > 0) {
                if (Dis < dtd[0].Distance_KM) Dis = dtd[0].Distance_KM;
                }
                if (nitm != undefined) {
                Aty = itm.vtype.replace(/-EX/g, '');
                nAty = nitm.vtype.replace(/-EX/g, '');
                if (Aty == 'OS' && nAty != 'OS') {
                cAty = 'EX'
                }
                else {
                cAty = Aty
                }
                }
                else {
                cAty = itm.vtype.replace(/-EX/g, '');
                }
                if (nitm == undefined || itm.vdate != nitm.vdate) {
                fDA = AllwDet.filter(function (a) {
                return (a.Allowance_Code == cAty + '1');
                });
                Allw = (fDA.length > 0) ? fDA[0].Allowance_Value : 0;
                fare = (FareDet.d.length > 0) ? FareDet.d[0].Fare : 0;
                expDTDet = {};
                expDTDet.vDt = itm.vdate;
                expDTDet.Aty = cAty;
                expDTDet.Dist = Dis*2;
                expDTDet.fare = (Dis*2) * fare;
                expDTDet.Allw = Allw;
                expDets.push(expDTDet);
                $Dv = 0; Dis = 0; cAty = ''; fare = 0; Allw = 0;
                }*/
                //Pdt = itm.vdate;
                // }

                var dtls_tab = document.getElementById("Expense_Table");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;

                $('#Expense_Table tbody tr').each(function () {
                    var rowtot = 0;
                    for (var i = 0; i < expDets.length; i++) {
                        if ($(this).find("td").eq(0).text() == expDets[i].vDt) {
                            $(this).find('td').eq(3).html(expDets[i].Aty);
                            $(this).find('td').eq(4).html(expDets[i].Dist);
                            $(this).find('td').eq(5).html(expDets[i].fare);
                            $(this).find('td').eq(6).html(expDets[i].Allw);
                        }
                    }
                });

                appData = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetAppDetails",
                    data: "{'SF_Code':'" + sf_code + "','ExpYear':'" + selyear + "','ExpMonth':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        appData = data;
                        console.log(appData);
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                var dtot_sum = 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Entry.aspx/GetDate",
                    data: "{'SF_Code':'" + sf_code + "','ExpYear':'" + selyear + "','ExpMonth':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        var obj = JSON.parse(data.d);
                        if (obj.MTED.length > 0) {
                            $('#over_tot').html(obj.MTED[0].exp_amt || 0);
                        }
                        $('#Expense_Table > tbody > tr').each(function () {
                            var coption = CountOption;
                            var sum = 0;
                            var chaddls = [];
                            var alCode = "";
                            if (obj.MTED.length > 0) {
                                for (var i = 0; i < obj.TED.length; i++) {
                                    if ($(this).find("td").eq(0).text() == obj.TED[i].exp_dt) {
                                        $(this).find("td").eq(4).html(obj.TED[i].distance);
                                        $(this).find("td").eq(5).html(obj.TED[i].fare);
                                        $(this).find("td").eq(6).html(obj.TED[i].exp_da);
                                        for (var j = 0; j < obj.TED[i].adddtls.length; j++) {
                                            if (obj.TED[i].adddtls[j].user_enter == 1) {
                                                console.log('mani');
                                                if (sum == 0) {
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.alws_code').val(obj.TED[i].adddtls[j].alw_code);
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').val(obj.TED[i].adddtls[j].value);
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').attr('pv', obj.TED[i].adddtls[j].value);
                                                    sum += Number(obj.TED[i].adddtls[j].value);
                                                    chaddls.push({ alw_code: obj.TED[i].adddtls[j].alw_code });
                                                    coption = coption - 1;
                                                }
                                                else {
                                                    if (coption != 0) {
                                                        if (alCode != obj.TED[i].adddtls[j].alw_code) {
                                                            $(this).find('table[name=addtable] tbody tr:last').before().find('a[name=btnadd]').text('-');
                                                            $(this).find('table[name=addtable] tbody tr:last').after('<tr class="sel_row"><td style="width:100px;"><select name="addetype" class="form-control alws_code"/></td><td style="width:80px;" ><input class="form-control row_sum2" type="text" name="addeamt"/></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr>');
                                                            loadddl(arr, $(this).find('table[name=addtable] tbody'));
                                                            $(this).find('table[name=addtable] tbody tr:last').find('.alws_code').val(obj.TED[i].adddtls[j].alw_code);
                                                            $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').val(obj.TED[i].adddtls[j].value);
                                                            $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').attr('pv', obj.TED[i].adddtls[j].value);
                                                            sum += Number(obj.TED[i].adddtls[j].value);
                                                            chaddls.push({ alw_code: obj.TED[i].adddtls[j].alw_code });
                                                            coption = coption - 1;
                                                            alCode = obj.TED[i].adddtls[j].alw_code;
                                                            console.log(alCode);
                                                        }
                                                    }
                                                }
                                            }
                                            else {
                                                if ($(this).closest('table').find('th').eq(j + 7).find('input[name=Alw_Code]').val() == obj.TED[i].adddtls[j].alw_code) {
                                                    $(this).closest('tr').find('td').eq(j + 7).html(obj.TED[i].adddtls[j].value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            for (var k = 0; k < appData.d.length; k++) {
                                if ($(this).find("td").eq(0).text() == appData.d[k].eDate) {

                                    if (chaddls.length > 0) {
                                        // console.log('en1 :' + k);
                                        var dtd = chaddls.filter(function (a) {
                                            return (a.alw_code == appData.d[k].expCode);
                                        });
                                        if (dtd.length == 0) {
                                            if (coption != 0) {
                                                if (alCode != appData.d[k].expCode) {
                                                    $(this).find('table[name=addtable] tbody tr:last').before().find('a[name=btnadd]').text('-');
                                                    $(this).find('table[name=addtable] tbody tr:last').after('<tr class="sel_row"><td style="width:100px;"><select name="addetype" class="form-control alws_code"/></td><td style="width:80px;" ><input class="form-control row_sum2" type="text" name="addeamt"/></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr>');
                                                    loadddl(arr, $(this).find('table[name=addtable] tbody'));
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.alws_code').val(appData.d[k].expCode);
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').val(appData.d[k].Amt);
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').attr('pv', appData.d[k].Amt);
                                                    sum += Number(appData.d[k].Amt);
                                                    coption = coption - 1;
                                                    //  console.log('add1');
                                                    alCode = appData.d[k].expCode;
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        // console.log('en2 :' + k);                                       
                                        if (sum == 0) {
                                            $(this).find('table[name=addtable] tbody tr:last').find('.alws_code').val(appData.d[k].expCode);
                                            $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').val(appData.d[k].Amt);
                                            $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').attr('pv', appData.d[k].Amt);
                                            sum += Number(appData.d[k].Amt) || 0;
                                            coption = coption - 1;

                                            // console.log('1 : '+appData.d[k].expCode);

                                        }
                                        else {
                                            if (coption != 0) {
                                                if (alCode != appData.d[k].expCode) {
                                                    $(this).find('table[name=addtable] tbody tr:last').before().find('a[name=btnadd]').text('-');
                                                    $(this).find('table[name=addtable] tbody tr:last').after('<tr class="sel_row"><td style="width:100px;"><select name="addetype" class="form-control alws_code"/></td><td style="width:80px;" ><input class="form-control row_sum2" type="text" name="addeamt"/></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr>');
                                                    loadddl(arr, $(this).find('table[name=addtable] tbody'));
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.alws_code').val(appData.d[k].expCode);
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').val(appData.d[k].Amt);
                                                    $(this).find('table[name=addtable] tbody tr:last').find('.row_sum2').attr('pv', appData.d[k].Amt);
                                                    sum += Number(appData.d[k].Amt);
                                                    coption = coption - 1;
                                                    alCode = appData.d[k].expCode;
                                                    //   console.log('2 : '+appData.d[k].expCode);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            dtot_sum += sum;
                            $(this).find('table[name=addtable] tfoot tr').find('.css_sum').html(sum);
                        });

                        $('#mon_Table1 tbody tr').each(function () {
                            for (var i = 0; i < obj.MED.length; i++) {
                                if ($(this).find("td").eq(0).find('input[name=Alw_Code]').val() == obj.MED[i].alw_code) {
                                    if ($(this).find("td").eq(0).find('input[name=user_enter]').val() == obj.MED[i].user_enter) {
                                        $(this).find("td").eq(1).find('input[name=addmamt]').val(obj.MED[i].value);
                                        $(this).find("td").eq(1).find('input[name=addmamt]').attr('pv', obj.MED[i].value);
                                    }
                                    else {
                                        $(this).find("td").eq(1).find('.css_sum').html(obj.MED[i].value);
                                    }
                                }
                            }
                        });
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $('#Expense_Table tbody tr').each(function () {
                    var sum = 0;
                    $(this).find('.css_sum').each(function () {
                        sum += Number($(this).text());
                    });
                    $(this).find('td').closest('.css_tot').html(sum);
                });

                $('#Expense_Table tbody tr').each(function () {
                    var m = 0;
                    $.each(this.cells, function () {
                        if ($(this).index() > 3) {
                            var mm = $(this).text();
                            tot_arr[m] = (tot_arr[m] || 0) + Number(mm);
                            m++;
                        }
                    });
                });

                for (var j = 0; j < tot_arr.length - 1; j++) {
                    $('#Expense_Table tfoot tr').find('th').eq(j + 2).find('label').html(tot_arr[j]);
                }

                $('#Expense_Table tfoot tr th:last').find('label').html(tot_arr[tot_arr.length - 1]);
                $('#lbltot').html(dtot_sum);
                var tot_sum = tot_arr[tot_arr.length - 1];
                var sum = 0;
                $('#mon_Table1 tbody tr').each(function () {
                    $(this).find('.css_sum').each(function () {
                        sum += Number($(this).text() || $(this).val());
                    });
                });
                $("#mon_tot").html(sum);
                $('#over_tot').html(tot_sum + sum || 0);
            }

            $(document).on('click', '#btnprint', function () {
                if ($('#Expense_Table tbody tr').length <= 0) {
                    alert('No Records Fount');
                    return false;
                }
                var strOpen = "rpt_Expense_Entry_new.aspx?SF_Code=" + sf_code + "&FYear=" + selyear + "&FMonth=" + selmonth
                window.open(strOpen, 'poprpExpense', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
            });

            $('input[type=text').keypress(function (event) {
                return isNumber(event, this)
            });


            function isNumber(evt, element) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (
            (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
        });

        function loadddl(arr, tr) {
            var ddlCustomers;
            if (!tr) {
                ddlCustomers = $("select[name=addetype]");
            }
            else {
                ddlCustomers = $(tr).find("select[name=addetype]:last");
            }
            $(arr).each(function () {
                var option = $("<option />");
                option.html(this.alw_name);
                option.val(this.alw_code);
                ddlCustomers.append(option);
            });
        }
    </script>
    <form id="Allowancefrm" runat="server">
        <asp:HiddenField ID="hdnMode" runat="server" />
        <asp:HiddenField ID="hdnMonth" runat="server" />
        <asp:HiddenField ID="hdnYear" runat="server" />
        <asp:HiddenField ID="hdnsfcode" runat="server" />
        <div class="container" style="width: 30%; max-width: 30%; display: none" id="btnctrl">
            <div class="row">
                <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                    Text="Year" CssClass="col-md-2 control-label"></asp:Label>
                <div class="col-sm-8 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <select id="ddl_year" name="txt_year" class="form-control" style="width: 130px">
                            <option>--- Select ---</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                    Text="Month" CssClass="col-md-2 control-label"></asp:Label>
                <div class="col-sm-8 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <select id="dll_month" name="dll_month" class="form-control" style="width: 130px">
                            <option value="0" label="--- Select---"></option>
                            <option value="1" label="JAN"></option>
                            <option value="2" label="FEB"></option>
                            <option value="3" label="MAR"></option>
                            <option value="4" label="APR"></option>
                            <option value="5" label="MAY"></option>
                            <option value="6" label="JUN"></option>
                            <option value="7" label="JUL"></option>
                            <option value="8" label="AUG"></option>
                            <option value="9" label="SEP"></option>
                            <option value="10" label="OCT"></option>
                            <option value="11" label="NOV"></option>
                            <option value="12" label="DEC"></option>
                        </select>
                    </div>
                </div>
            </div>
            <br />
            <div class="row" style="text-align: center">
                <div class="col-sm-10 inputGroupContainer">
                    <a id="btnGo" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                        <span>Go</span></a>
                </div>
            </div>
        </div>
        <br />
        <div id="maindiv" style="display: none">
            <div id="container" class="container" style="width: 100%">WE ARE WORKING IN THIS SCREEN. UNDER MAINTENANCE.
                <table id="Expense_Table" class="gvHeader" border="1">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
            <br />
            <br />
            <div id="monthly_div" class="container" style="width: 100%;">
                <div class="row">
                    <div class="col-sm-8" style="text-align: left; /* margin-top: 150px; */font-size: 35px;">
                        <div class="row">
                            <div class="col-sm-8" style="text-align: center;">
                                <label id="Label3" style="/* font-size: larger; */">
                                    NetTotal</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-8" style="text-align: center;">
                                <label id="over_tot" style="font-size: larger">
                                    0.00</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <table id="mon_Table1" class="gvHeader" border="1">
                            <thead>
                                <tr>
                                    <th>Name
                                    </th>
                                    <th style="min-width: 150px;">Values
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>Total
                                    </td>
                                    <td>
                                        <label id="mon_tot">
                                        </label>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
            <br />
            <div class="row" style="text-align: center">
                <div class="col-sm-12 inputGroupContainer">
                    <a id="btnapproval" class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;"><span>Approval</span></a>&nbsp&nbsp <a id="btndaysave" class="btn btn-primary btnsave"
                        style="vertical-align: middle; font-size: 17px;"><span>Save</span></a>&nbsp&nbsp<a
                            id="btnprint" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                            <span>Print</span></a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
