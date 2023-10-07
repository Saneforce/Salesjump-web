<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Invoice_Order_List.aspx.cs" Inherits="SuperStockist_Sales_Invoice_SS_Invoice_Order_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <form id="frm1" runat="server">
        <asp:HiddenField ID="HiddenField1" runat="Server" />
        <div class="row">
            <div class="col-lg-12 sub-header">
                Sales Invoice<span style="float: right"><a href="../Invoice/SS_Invoice_Entry.aspx" class="btn btn-primary btn" id="newsf">Add New</a></span><span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                    </div>
                </span>
            </div>
        </div>
        <br />
        <div class=" card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                    <label style="float: right">
                        Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning" style="white-space: nowrap;">
                        <tr>
                            <th style="text-align: left;">SL NO</th>
                            <th style="text-align: left;">Invoice No</th>
                            <th style="text-align: left;">Order No</th>
                            <th style="text-align: left;">Customer Name</th>
                            <th style="text-align: left">Invoice Date</th>
                            <th style="text-align: left">Status</th>
                            <th style="text-align: right">Total</th>
                           <%-- <th style="text-align: right">Edit</th> --%>
                            <th style="text-align: right">View</th>
                            <th style="text-align: right">Print</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot></tfoot>
                </table>
                <div class="row" style="padding: 5px 0px">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="div_Print" style="display: none; padding-right: 10px; padding-top: 10px; padding-left: 10px;min-height:842px;position:relative">
        </div>


        <div id="divid">
        </div>

    </form>
    <style type="text/css">
        .green {
            color: #28a745;
        }

        .yellow {
            color: #e8a11e;
        }

        .red {
            color: #ff8080;
        }

        .blue {
            color: #80bfff;
        }
        .edit_class {
            pointer-events: none;
            cursor: default;
            text-decoration: line-through;
            color: black;
        }

    </style>
    <script type="text/javascript" src="../../../js/lib/slip.js"></script>
    <script type="text/javascript" src="../../../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../../../js/plugins/datatables/dataTables.bootstrap.js"></script>

    <script language="javascript" type="text/javascript">

        var AllOrders = []; var fdt = ''; var tdt = ''; var divcode; filtrkey = '0'; var AllOrdersdetails = []; var AllOrderspro = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Trans_Inv_Slno,Cus_Name,Total_Amount,Status,Total";
        pgNo_Print = 1; PgRecords_print = 30; TotalPg_print = 0;
        var netval = 0; var cgst = 0; var sgst = 0; var totlcase = 0; var totalpie = 0; var cashdis = 0.00; var grossvalue = 0; var totalGst = 0.00; var roundoff = 0.00; var netvalue = 0.00;
        var rndoff = 0.00; var netwrd = ''; var netwrd1 = ''; var netwrd2 = ''; var cntu = 10; pagenumber = 1;
        var subdiv = ("<%=Session["subdivision_code"]%>");
        var path = '';
        var page = ''; var hiden = ''; var imsgename = '';
        path = window.location.pathname;
        page = path.split("/").pop();
       
        var hiden = $('#<%= HiddenField1.ClientID %>').val();
        var imsgename=''+hiden+'_logo.png';
      

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );

        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            loadData();
        });

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg)) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
            }
            );
        }

        function ReloadTable() {
           //DoesParentExists();
            var tota = 0;
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1); slno = 0;

            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Trans_Inv_Slno + "</td><td>" + Orders[$i].Order_No + "</td><td>" + Orders[$i].Cus_Name + "</td><td>" + Orders[$i].Invoice_Date +
                        "</td><td>" + Orders[$i].Status + "</td><td style='text-align: right'>" + parseFloat(Orders[$i].Total).toFixed(2) + "</td><td style='text-align: right'><a href='SS_Invoice_Order_View.aspx?Order_No=" + Orders[$i].Trans_Inv_Slno + "&Stockist_Code=" + stockist_Code + "&Div_Code=" + Div_Code + "&Status=" + Orders[$i].Status + "'><span class='glyphicon glyphicon-eye-open'></span></a></td><td style='text-align: right'><a href='#'  onClick='popup(\"" + Orders[$i].Trans_Inv_Slno + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Cus_Code + "\")'><span class='glyphicon glyphicon-print'></span></td>");
                    /*<td style='text-align:right; cursor: pointer;'><a href='#' id='Btn_Edit' onclick='Edit_Screen(\"" + Orders[$i].Trans_Inv_Slno + "\",\"" + Orders[$i].Cus_Code + "\" ,\"" + Orders[$i].Order_Date + "\",\"" + Orders[$i].Cus_Name + "\" )'</a>Edit</td >*/
                    $("#OrderList TBODY").append(tr);

                    tota = parseFloat(Orders[$i].Total || 0) + parseFloat(tota)

                    if (Orders[$i].Status == 'Invoiced') {
                        $("td:contains('Invoiced')").addClass('green');
                    }
                    else if (Orders[$i].Status == 'Partially Paid') {
                        $("td:contains('Partially Paid')").addClass('yellow');
                    }
                    else if (Orders[$i].Status == 'Pending') {
                        $("td:contains('Pending')").addClass('red');
                    }
                }
            }
            $("#OrderList TFOOT").html("<tr><td colspan='6' style='font-weight: bold;'>Total</td><td style='text-align: right; font-weight: bold;'>" + tota.toFixed(2) + "</td></tr>");
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }

        function Edit_Screen(id, cust, order_date, cusname) {

            var url = "../Stockist/HFS_SalesInvoice_Edit.aspx?Invoice_No=" + id + "&Rettailer_Code=" + cust + "&Order_Date=" + order_date + "&Cust_Name=" + cusname + ""
            window.open(url, '_self');

        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
            ReloadTable();
        })

        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Invoice_Order_List.aspx/GetInvoiceDetails",
                data: "{'FDT':'" + fdt + "','TDT':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
        var Div_Code = ("<%=Session["div_code"].ToString()%>");

        loadData();

        function popup(order_id, stk, div, cust_code) {
            if (stk != "5721") {
                PrintData(order_id, stk, div, cust_code);
            }
            else {
                var type = 1;
                slipfun(order_id, type);
                $('#divid').hide();
            }
        }

        function PrintData(order_id, stk, div, cust_code) {

            $('#div_Print').html('');
            netval = 0; cgst = 0; sgst = 0; totlcase = 0; totalpie = 0; cashdis = 0.00; grossvalue = 0; totalGst = 0.00; roundoff = 0.00; netvalue = 0.00;
            rndoff = 0.00; netwrd = ''; netwrd1 = ''; netwrd2 = ''; cntu = 10; pagenumber = 1; amt_without_tax = 0;
            loadDataPrint(order_id, stk, div, cust_code);
            print();
        }

        function loadDataPrint(order_id, stk, div, cust_code) {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "SS_Invoice_Order_List.aspx/GetProductdetails",
                data: "{'Order_Id':'" + order_id + "'}",
                dataType: "json",
                async: false,
                success: function (data) {
                    AllOrderspro = JSON.parse(data.d) || [];
                    Orders = AllOrderspro;
                    ReloadTablePrint(order_id, stk, div, cust_code);
                },
                error: function (result) {
                }
            });
        }

        function ReloadTablePrint(order_id, stk, div, cust_code) {

            $('#div').empty(); var tot_gross = 0; var rowcount = 0; pgNo = 1;
            Multipage = '<div class="fullpage" style="padding-right: 10px;padding-top:40px;background-color:#f1f2f7;"><page size="A4" layout="portrait" class="Printarea" ><div  class="page" style="font-family: "Times New Roman", Times, serif;page-break-before:always;">';
            singlepg = '<div class="fullpage" style="padding-right: 10px;padding-top:40px;background-color:#f1f2f7;"><div  class="page" style="font-family: "Times New Roman", Times, serif;background-color:#f1f2f7;">';

            for (x = 0; x < (Orders.length / PgRecords_print); x++) {
                str = '';
                st = PgRecords_print * (pgNo_Print - 1); slno = 0;
                if (PgRecords > Orders.length) {
                    str += singlepg;
                }
                else
                    str += Multipage;

                loadDistributor(order_id, stk, div, cust_code);
                productheader();
                for ($i = st; $i < st + PgRecords_print; $i++) {
                    if ($i > Orders.length) {
                        str += "<tr><td class='Sh' style='padding-top:0px;padding-bottom:0px;height:14px;'></td></tr>";
                    }
                    if ($i < Orders.length) {

                        var caseval = "";
                        var pc = "";
                        var amtbyCase = 0.00;
						var peice = 0;
                        var tax = Orders[$i].Tax / 2;
                        var qty_in_case = Orders[$i].Quantity / Orders[$i].Sample_Erp_Code
                        if (qty_in_case >= 1) {
                            qty_in_case = parseInt(qty_in_case);
                            peice = Orders[$i].Quantity - (Orders[$i].Sample_Erp_Code * qty_in_case)
                        }
                        else {
                            qty_in_case = 0;
                            peice = Orders[$i].Quantity;
                        }	
                        var amount = Orders[$i].Amount;
                        var discnt = Orders[$i].Discount;
                        var cgstv = tax;
                        var sgstv = tax;

                        //   var grossamnt = amount + Orders[$i].Tax;
                        // if (Orders[$i].qty == 0) {
                        //   amtbyCase = grossamnt;
                        //} else {
                        //  amtbyCase = grossamnt / Orders[$i].qty;
                        //}                  
                        rowcount = rowcount + 1;

                        str += "<tr><td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + ($i + 1) + "</b></td>";
                        str += "<td class='Sh' align='left'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + Orders[$i].Product_Name + "</b></td>";
                        str += "<td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].HSN_Code + "</td>";
                        str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Quantity + "</td>";
                        str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Unit  + "</td>";
                        /* str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].MRP_Price + "</td>";*/
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Rate + "</td>";
                        str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Free + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + discnt.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Tax + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + (amount ).toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;display:none;'>" + (amount).toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + (amount ).toFixed(2) + "</td></tr>";

                        cashdis += Orders[$i].Discount;
                        totlcase += qty_in_case;
                        totalpie += Orders[$i].Quantity;
                        cgst += cgstv;
                        sgst += sgstv;
                        grossvalue += (Orders[$i].Amount + Orders[$i].Tax);
                        /*amt_without_tax += (Orders[$i].Amount - Orders[$i].Tax);*/
                        /* amt_without_tax += (Orders[$i].Amount - Orders[$i].Tax);*/
                        amt_without_tax += Orders[$i].Amount;
                        totalGst += Orders[$i].Tax;
                    }
                }
                pagenumber = pgNo;
                if (Orders.length > PgRecords_print) {
                    str += "<tr><td class='Sh' align='right' colspan='13' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>Continue</b></td></tr>";
                    str += '</tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:right">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div><div style="break-after:page"></div>';
                    str += '</div></page></div>';
                }
                else {
                    str += '</div>';
                }
                $('#div_Print').append(str);
                pgNo++;
            }
            gstcalculi();

        }
        function loaddivision() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Invoice_Order_List.aspx/GetDivision",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {                    
                    Alldivision = JSON.parse(data.d) || [];
                }
            });
        }

        function loadDistributor(order_id, stk, div, cust_code) {
            loaddivision();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Invoice_Order_List.aspx/GetDistributor",
                data: "{'Order_Id':'" + order_id + "','Stockist':'" + stk + "','Division':'" + div + "','Cust_code':'" + cust_code + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrdersdetails = JSON.parse(data.d) || [];

                    var str1 = '<div class="head" style="font-family: "Times New Roman", Times, serif;"><table border="0" class="headtable" align="left" style="width: 35%;margin-top:75px; border-collapse: collapse;"><tbody>';
                    var str2 = '<table border="0" align="left" class="middletable" style="width: 35%; border-collapse: collapse;font-size:12px;"><tbody>';
                    var str3 = '<table border="0" align="left" class="lasttable" style="width: 30%; border-collapse: collapse;font-size:12px;margin-top:75px"><tbody>';

                    var a = 0;
                  <%--  $('#<%=hid_stockist_name.ClientID%>').val(AllOrdersdetails[a].Stockist_Name);--%>


                    str1 += '<tr style="font-size:12px"><td  align="left" style="padding-right:0px; padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; TimesNewRomen;padding-left:10px;"></td></tr>';                 
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px; ">RET State Name:-' + AllOrdersdetails[a].Retstate + '</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;">&nbsp;&nbsp;</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font: bold 14px TimesNewRomen;padding-left:10px;"><b>' + AllOrdersdetails[a].Stockist_Name + '</b></td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;width:275px;">' + AllOrdersdetails[a].Stockist_Address + '</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;">Retailer FSSI No :-' + AllOrdersdetails[a].RetFssi_No + '</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;">Retailer Ph NO:- ' + AllOrdersdetails[a].RetMobile + '</td></tr></tbody></table>';

                    
                    str2 += '<tr align="center"><td ><img style="width:100px;height:100px;margin-top:-25px;" src="http://fmcg.sanfmcg.com/limg/'+imsgename+'" /></td></tr>';
                    str2 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>' + AllOrdersdetails[a].S_Name + '</b></td></tr>';
                    str2 += '<tr><td align="left" style="font: bold 12px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>(Authorised Disributor for '+Alldivision[0].Division_Name+'  ) </b></td></tr>';
                    str2 += '<tr><td align="left" style="width:250px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + AllOrdersdetails[a].Stockist_Address + '</td></tr>';
                    str2 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Dist FSSI No :-' + AllOrdersdetails[a].DistFssi_No + '</td></tr>';
                    str2 += '<tr><td align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">PH NO:&nbsp;&nbsp;' + AllOrdersdetails[a].Mobile + '</b></td></tr>';
                    str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr>';
                    str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr></tbody></table>';

                    str3 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>GST INVOICE</b></td></tr>';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Dist GSTIN:-' + AllOrdersdetails[a].DistgstTin + '</td></tr >';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill Date :-' + AllOrdersdetails[a].billdate + '</td></tr>';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill No:-' + AllOrdersdetails[a].billno + '</td></tr>';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Name:-' + AllOrdersdetails[a].Diststate + '</td></tr> ';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Code:-' + AllOrdersdetails[a].DistStatecd + '</td></tr></tbody></table></div>';

                    str += str1 + str2 + str3;

                }
            });
        }

        function productheader() {
            str += '<div class="product" style="font:12px TimesNewRomen;"><table class="rptOrders" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;">';
            str += '<thead><tr style="border-top:thin dashed;border-bottom:thin dashed;font:12px TimesNewRomen;"><td class="Sh1" align="center" rowspan="2"><font face="calibri"/><b>Sl.No</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>DESCRIPTION</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>HSN Code</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Qty</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Uom</b></td>';
            /*str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>MRP</b></td>*/
             str+=' <td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Rate</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>FrQty</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>S.Disc</b></td></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Tax</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Amt/Case</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Amount</b></td></tr></thead>';
            str += '<tbody>';
        }

        function numberToWords(number) {
            var digit = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
            var elevenSeries = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
            var countingByTens = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
            var shortScale = ['', 'thousand', 'million', 'billion', 'trillion'];

            number = number.toString(); number = number.replace(/[\, ]/g, ''); if (number != parseFloat(number)) return 'not a number'; var x = number.indexOf('.'); if (x == -1) x = number.length; if (x > 15) return 'too big'; var n = number.split(''); var str = ''; var sk = 0; for (var i = 0; i < x; i++) { if ((x - i) % 3 == 2) { if (n[i] == '1') { str += elevenSeries[Number(n[i + 1])] + ' '; i++; sk = 1; } else if (n[i] != 0) { str += countingByTens[n[i] - 2] + ' '; sk = 1; } } else if (n[i] != 0) { str += digit[n[i]] + ' '; if ((x - i) % 3 == 0) str += 'hundred '; sk = 1; } if ((x - i) % 3 == 1) { if (sk) str += shortScale[(x - i - 1) / 3] + ' '; sk = 0; } } if (x != number.length) { var y = number.length; str += 'point '; for (var i = x + 1; i < y; i++) str += digit[n[i]] + ' '; } str = str.replace(/\number+/g, ' '); return str.trim() + ".";

        }

        function gstcalculi() {
            netval = amt_without_tax  - cashdis;
            netvalue = Math.round(netval);
            rndoff = netvalue - netval;
            intpart = netvalue.toString().split(".")[0];
            floatpart = netvalue.toString().split(".")[1];

            if (floatpart == undefined) {
                netwrd1 = "Zero"
            } else if (floatpart.length != 2) {
                floatpart += "0";
                netno1 = numberToWords(parseInt(floatpart)).replace(".", "");
                netwrd1 = netno1.replace(/^(.)|\s+(.)/g, c => c.toUpperCase());
            }
            netno = numberToWords(parseInt(intpart)).replace(".", "");
            netwrd = netno.replace(/^(.)|\s+(.)/g, c => c.toUpperCase());

            var distributor = ("<%=Session["Sf_Name"]%>");

            str = '<div style="padding-bottom: -5px;background-color:#f1f2f7;bottom:90px;position: absolute;width:100%" class="total"><table border="0" clabottom: opx;ss="rpttotal" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;"><tbody>';
            str += '<tr style="border-top:thin dashed;font:12px TimesNewRomen;"> <td></td><td></td> <td colspan="12" ><b></b></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;padding-left:10px;" colspan="2"><b></b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b></b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b></b></td><td style="padding-top: 0px;padding-bottom: 0px;" colspan="2"><b></b></td><td  colspan="4" style="padding-top:0px;padding-bottom:0px;"></td><td style="padding-top:0px;padding-bottom:0px;"><b>Gross Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;"><b>' + amt_without_tax.toFixed(2) + '</b></td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;padding-left:10px;" colspan="2"><b></b></td><td style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top:0px;padding-bottom:0px;"></td><td style="padding-top:0px;padding-bottom:0px;" colspan="3"></td><td  colspan="3" style="padding-top:0px;padding-bottom:0px;"></td><td style="padding-top:0px;padding-bottom:0px;"><b>Scheme Amt :</b></td><td style="float:right;padding-top:0px;padding-bottom:0px;"><b>0.00</b></td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;padding-left:10px;" colspan="2"><b></b></td><td style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top:0px;padding-bottom:0px;"></td><td style="padding-top:0px;padding-bottom:0px;" colspan="6"></td><td style="padding-top:0px;padding-bottom:0px;"><b>Cash Disc :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;"><b>' + cashdis.toFixed(2) + '</b></td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>Tax Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-bottom: 0px;padding-top: 0px;"><b>' + totalGst.toFixed(2) + '</b></td><td></td></tr>';
            str += '<tr style="border-bottom:thin dashed;font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>Round Off(+/-) :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top: 0px;padding-bottom: 0px;"><b>' + rndoff.toFixed(2) + '</b></td><td></td></tr>';
            str += '<tr style="border-bottom:thin dashed; padding-left:20px;font-family:monospace;font-size:14px;padding-left:10px;"><td  colspan="8">' + netwrd + ' Rupees  and ' + netwrd1 + ' Paise</td> </td><td></td><td></td><td><b>Net Value :</b>&nbsp;&nbsp;</td><td style="float:right; padding-bottom: 0px;"><b>' + netvalue.toFixed(2) + '</b></td><td></td></tr></tbody></table ></div>';
            str += '<table border="0" class="rpttotal1" style="width: 100%; border-collapse: collapse;background-color:#f1f2f7;bottom: 0px;position: absolute;"><tbody><tr style="font:12px TimesNewRomen;"><td style="padding-left:10px;width:450px;">We hereby Ceritify that Goods mentioned in this invoice are warranted to be of nature quality which these are purposed to be. </td><td style="padding-top: 20px;font:bold 12px TimesNewRomen;float:right;margin-right: 10px;"><b>For ' + distributor + '</b></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-left:20px;padding-top: 10px;">Buyers Sign</td><td style="float:right;margin-right: 10px;padding-top: 10px;">Authorized Signatory</td></tr></tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:center">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div></div><div style="break-after:page"></div>';

            $('#div_Print').append(str);
        }

        function print() {
            var contents = $("#div_Print").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>Invoice Print</title>');
            frameDoc.document.write('</head><body style="position:relative;">');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
            //var myWindow = window.open();
            //var doc = myWindow.document;
            //doc.open();
            //doc.write(contents);
           // doc.close();
        }

    </script>
    <script type="text/javascript">
	    /*
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);

        });
		*/

        $(function () {

            var Get_localData = localStorage.getItem("Date_Details");

            Get_localData = JSON.parse(Get_localData);

            if (Get_localData != "" && Get_localData != null) {

                var Dates = Get_localData[0].split('-');

                var fdj = Dates[2].trim() + '-' + Dates[1] + '-' + Dates[0] + ' ' + ' 00:00:00';
                var nfgresd = Dates[5] + '-' + Dates[4] + '-' + Dates[3].trim() + ' ' + ' 00:00:00';

                pgNo = parseFloat(Get_localData[1]); PgRecords = parseFloat(Get_localData[2]); flag = '1';

                const utcDate = new Date(fdj);
                const utcDate1 = new Date(nfgresd);

                var start = utcDate;
                var end = utcDate1;

            }
            else {
                var start = moment();
                var end = moment();
                flag = '0';
            }

            function cb(start, end, flag) {

                if (flag == '1') {

                    var F_dat = start.getDate();
                    var F_dat1 = start.getMonth() + 1;
                    var F_dat2 = start.getFullYear();
                    var f_date3 = F_dat + '-' + F_dat1 + '-' + F_dat2;

                    var E_dat = end.getDate();
                    var E_dat1 = end.getMonth() + 1;
                    var E_dat2 = end.getFullYear();
                    var E_date3 = E_dat + '-' + E_dat1 + '-' + E_dat2;

                    $('#reportrange span').html(f_date3 + ' - ' + E_date3);
                }
                else {
                    pgNo = 1;
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                }

                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                namesArr.push(page);
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);
            cb(start, end, flag);
        });

    </script>
</asp:Content>

