<%@ Page Title="Expense Report" Language="C#" AutoEventWireup="true" CodeFile="rpt_Expense_Entry.aspx.cs"
    Inherits="MasterFiles_rpt_Expense_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style type="text/css">
        @page
        {
            size: auto; /* auto is the initial value */
            margin: 0mm; /* this affects the margin in the printer settings */
        }
        
        html
        {
            background-color: #FFFFFF;
            margin: 0px; /* this affects the margin on the html before sending to printer */
        }
        
        body
        {
            margin: 10mm 15mm 10mm 15mm; /* margin you want for the content */
        }
        #Expense_Table, #mon_Table1
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #Expense_Table td, #Expense_Table th, #mon_Table1 td, #mon_Table1 th
        {
            border: 1px solid #ddd;
            padding: 8px;
        }
        
        #Expense_Table tr:nth-child(even), #mon_Table1 tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #Expense_Table tr:hover, #mon_Table1 tr:hover
        {
            background-color: #ddd;
        }
        
        #Expense_Table th, #mon_Table1 th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #475677;
            color: white;
        }
        input[type=text]
        {
            text-align: right;
        }
        tfoot label
        {
            width: 100%;
            margin-bottom: 0px;
        }
        
        #Expense_Table tfoot th, #Expense_Table tfoot td, #mon_Table1 tfoot th, #mon_Table1 tfoot td
        {
            background-color: #c1c1c1;
            color: #252525;
        }
        .css_sum, .css_tot, tfoot label
        {
            text-align: right;
        }
        #Expense_Table td:nth-child(5)
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
            alldataload();

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

                // $("#over_tot").html(Mtot); 

                $(tt).html(stot);
                $(cTC).html(ctot);
            }
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


            function alldataload() {
                var tot_arr = [];
                arr = [];
                var len = 0;
                var mon_len = 0;
                selmonth = $('#<%=hdnMonth.ClientID%>').val();
                selyear = $('#<%=hdnYear.ClientID%>').val();
                sf_code = $('#<%=hdnsfcode.ClientID%>').val();


                var str = "<th style='width:100px' >Date</th><th style='width:150px'>Work Type</th><th style='width:300px'>Worked Place</th><th>Al Type</th><th>Distance</th><th>Fare</th><th>DA</th>";
                var str_foot = "<th colspan='3'>Total</th><th><label/></th><th><label/></th><th><label/></th><th><label/></th>";

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetVisitDetails",
                    data: "{'sf_code':'" + sf_code + "','years':'" + selyear + "','months':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        VDetail = data;
                        // alert(JSON.stringify(VDetail.d));
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetDistanceDetails",
                    data: "{'sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        DDetail = data;
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetfAREDetails",
                    data: "{'sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        FareDet = data;
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetTerr_Code",
                    data: "{'sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        terr_code = data.d;
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });




                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetAllType",
                    dataType: "json",
                    success: function (data) {


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
                                        mon_str += '<td><input type="hidden" name="Alw_Code" value="' + data.d[i].ALW_code + '"/><input type="hidden" name="user_enter"  value="' + data.d[i].user_enter + '"/>' + data.d[i].ALW_name + '</t><td class="css_sum"></td>';
                                        $('#mon_Table1 tbody').append("<tr>" + mon_str + "</tr>");
                                    }

                                }

                            }
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });



                $('#Expense_Table thead').append("<tr>" + str + "<th class='adddetals' style='width:300px;'>Additional</th><th style='min-width: 100px;'>Total</th></tr>");
                $('#Expense_Table tfoot').append("<tr>" + str_foot + "<th><label id='lbltot'/></th><th><label id='lblTtot'/></th></tr>");


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetExpanceDetails",
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
                    $(this).find('td:last').before('<td style="width:300px;"><table name="addtable" style="width:100%"><tbody><tr class="sel_row"><td> </td><td style="width:80px; text-align: right;" ></td></tr></tbody><tfoot><tr><td>Total</td><td class="css_sum"></td></tr></tfoot></table></td>');
                });
                loadddl(arr);

                var dtls_tab = document.getElementById("Expense_Table");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;




                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetExpanceValues",
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
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                expDets = [];
                console.log(VDetail.d);
                console.log(DDetail.d);
                console.log(AllwDet);
                console.log(FareDet.d);
                Dis = 0; cAty = ''; fare = 0; Allw = 0;
                for (var v = 0; v < VDetail.d.length; v++) {
                    itm = VDetail.d[v]; nitm = VDetail.d[v + 1];

                    dtd = DDetail.d.filter(function (a) {
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
                        } else {
                            cAty = Aty
                        }
                    }
                    else
                        cAty = itm.vtype.replace(/-EX/g, '');

                    if (nitm == undefined || itm.vdate != nitm.vdate) {
                        fDA = AllwDet.filter(function (a) {
                            return (a.Allowance_Code == cAty + '1');
                        });
                        Allw = (fDA.length > 0) ? fDA[0].Allowance_Value : 0;
                        fare = (FareDet.d.length > 0) ? FareDet.d[0].Fare : 0;

                        expDTDet = {};
                        expDTDet.vDt = itm.vdate;
                        expDTDet.Aty = cAty;
                        expDTDet.Dist = Dis * ((cAty != 'OS') ? 2 : 1);
                        expDTDet.fare = Dis * ((cAty != 'OS') ? 2 : 1) * fare;
                        expDTDet.Allw = Allw;

                        expDets.push(expDTDet);
                        Dis = 0; cAty = ''; fare = 0; Allw = 0;
                    }
                    Pdt = itm.vdate;
                }
                console.log(expDets);

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

                var dtot_sum = 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry.aspx/GetDate",
                    data: "{'SF_Code':'" + sf_code + "','ExpYear':'" + selyear + "','ExpMonth':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(JSON.parse(data.d));
                        var obj = JSON.parse(data.d);
                        console.log(obj.MTED.length);
                        if (obj.MTED.length > 0) {
                            console.log(obj.MTED[0].exp_amt);
                            $('#over_tot').html(obj.MTED[0].exp_amt || 0);
                        }
                        $('#Expense_Table > tbody > tr').each(function () {
                            var sum = 0;
                            var chaddls = [];
                            for (var i = 0; i < obj.TED.length; i++) {
                                if ($(this).find("td").eq(0).text() == obj.TED[i].exp_dt) {
                                  
                                    $(this).find("td").eq(4).html(obj.TED[i].distance);
                                    $(this).find("td").eq(5).html(obj.TED[i].fare);
                                    $(this).find("td").eq(6).html(obj.TED[i].exp_da);
                                   
                                    for (var j = 0; j < obj.TED[i].adddtls.length; j++) {
                                        if (obj.TED[i].adddtls[j].user_enter == 1) {
                                            console.log(obj.TED[i].adddtls[j]);
                                            if (sum == 0) {
                                                console.log("Mano" + obj.TED[i].adddtls[j].alw_name);
                                                $(this).find('table[name=addtable] tbody tr:last').find('td').eq(0).text(obj.TED[i].adddtls[j].alw_name);
                                                $(this).find('table[name=addtable] tbody tr:last').find('td').eq(1).text(obj.TED[i].adddtls[j].value);
                                            }
                                            else {
                                                $(this).find('table[name=addtable] tbody tr:last').before().find('a[name=btnadd]').text('-');
                                                $(this).find('table[name=addtable] tbody tr:last').after('<tr class="sel_row"><td style="width:100px;">' + obj.TED[i].adddtls[j].alw_name + '</td><td style="width:80px; text-align: right;" > ' + obj.TED[i].adddtls[j].value + ' </td></tr>');

                                            }
                                            sum += Number(obj.TED[i].adddtls[j].value);
                                            chaddls.push({ alw_code: obj.TED[i].adddtls[j].alw_code });
                                        }
                                        else {
                                            //  console.log('Mano : ' + $(this).closest('table').find('th').eq(j + 7).find('input[name=Alw_Code]').val());
                                            if ($(this).closest('table').find('th').eq(j + 7).find('input[name=Alw_Code]').val() == obj.TED[i].adddtls[j].alw_code) {
                                                $(this).closest('tr').find('td').eq(j + 7).html(obj.TED[i].adddtls[j].value);
                                                //  console.log('Mano : ' + $(this).closest('tr').find('td').eq(j + 7).text() + ":" + obj.TED[i].adddtls[j].value);
                                            }
                                        }

                                    }                                    
                                    dtot_sum += sum;
                                    $(this).find('table[name=addtable] tfoot tr').find('.css_sum').html(sum);

                                }
                            }
                        });

                        $('#mon_Table1 tbody tr').each(function () {
                            for (var i = 0; i < obj.MED.length; i++) {
                                if ($(this).find("td").eq(0).find('input[name=Alw_Code]').val() == obj.MED[i].alw_code) {
                                    console.log(obj.MED[i].value);
                                    $(this).find("td").eq(1).html(obj.MED[i].value);
                                }
                            }

                        });


                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
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
                    //console.log("Mano" + $('#Expense_Table tfoot tr').find('th').eq(j).text());
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

            $(document).on('click', "#btnprint", function () {

                var originalContents = $("body").html();
                var printContents = $("#maindiv").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;


                //                var printContents = document.getElementById("maindiv").innerHTML;
                //                var originalContents = document.body.innerHTML;               
                //                document.body.innerHTML = printContents;
                //                window.print();
                //                document.body.innerHTML = originalContents;
            });

            $(document).on('click', "#btnclose", function () {
                window.close();
            });

            //            var originalContents = $("body").html();
            //            var printContents = $("#maindiv").html();
            //            $("body").html(printContents);
            //            window.print();
            //            $("body").html(originalContents);
            //            return false;

        });



        function loadddl(arr, tr) {
            var ddlCustomers;
            if (!tr) {
                ddlCustomers = $("select[name=addetype]");
                console.log(tr);
            } else {
                ddlCustomers = $(tr).find("select[name=addetype]:last");
            }
            $(arr).each(function () {
                var option = $("<option />");
                //Set Customer Name in Text part.
                option.html(this.alw_name);
                //Set Customer CustomerId in Value pat.
                option.val(this.alw_code);
                //Add the Option element to DropDownList.
                ddlCustomers.append(option);
            });

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnMonth" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
    <asp:HiddenField ID="hdnsfcode" runat="server" />
    <div style="text-align: center">
        <a id="btnprint" class="btn btn-primary" style="vertical-align: middle; font-size: 14px;">
            <span>Print</span></a> <a id="btnclose" class="btn btn-primary" style="vertical-align: middle;
                font-size: 14px;"><span>Close</span></a>
    </div>
    <br />
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div id="maindiv" style="width: 90%;">
            <div id="container" class="container" style="width: 100%">
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
                                    <th>
                                        Name
                                    </th>
                                    <th style="min-width: 150px;">
                                        Values
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        Total
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
        </div>
    </asp:Panel>
    </form>
</body>
</html>
