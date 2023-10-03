﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewalldate_dcr.aspx.cs" Inherits="MasterFiles_Reports_viewalldate_dcr" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR View Report</title>
   <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }

        .Holiday {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }

        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }

        .table td {
            padding: 2px 5px;
            white-space: nowrap;
        }

        .gridviewStyle td {
            border: thin solid #000000;
            text-align: right;
            padding-right: 3px;
            max-width: 300px;
        }

        .gridpager td {
            padding-left: 13px;
            color: cornsilk;
            font-weight: bold;
            text-decoration: none;
            width: 100%;
        }
		 [data-val='HELPLINE REQUIRED']{
         background-color: yellow;
         }
        [data-val='RESOLVED']{
           background-color: #18eb18;
        }
    </style>
   
</head>
<body>

    <form id="form1" runat="server">
         <div class="container" style="max-width: 100%; width: 95%; text-align: right"> 
         <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" onclick="tablesToExcel(['Product_Table', 'Productpri_Table'], 'VirtueleMachineInfo.xls', 'Excel')" />
    </div>
        
        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Brandwise Sales" Style="margin-left: 10px; font-size: x-large"></asp:Label>

            </div>

        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div class="row" style="margin:0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
                <table id="Product_Table" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table><br /><br />
                 <table id="Productpri_Table" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
               </table><br /><br />
                <label style="font-size: 17px;">New Distributor</label>
                <br /><br />
                 <table id="Disthunt_table" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>                     
                </table><br /><br />
                 <table id="ProductRemark_Table" border="1" class="newStly" style="border-collapse: collapse;Display:none;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>                     
                </table>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <%-- <img src="../../../Images/loader.gif" alt="" />--%>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var Pbrands = [], Gorders = [], Gusers = []; SFremarks = []; disthunt = [];
		var prPbrands = [], prGorders = [], prGusers = [];	
		var $indxx = 0;
		function loadaddrs($tr) {
				let sublat = parseFloat($($tr).find('.latlng').attr('lat'));
				let sublng = parseFloat($($tr).find('.latlng').attr('lng'));
				var addrs = '';
				var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + sublng + '&lat=' + sublat + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                    }
                });
                $($tr).find('.latlng').text(addrs);
                $indxx++;
				if($indxx <= $('#Product_Table tbody tr').length){
					setTimeout(function () { loadaddrs($('#Product_Table tbody tr')[$indxx]) }, 10);
				}
            }
        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "viewalldate_dcr.aspx/getBrandwiseSales",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Gusers = JSON.parse(data.d) || [];
                    console.log(data.d);
                    getBrands();
                    getOrders();
                    ReloadTable();
					setTimeout(function () { loadaddrs($('#Product_Table tbody tr')[0]) }, 10);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        } 
        function getRemarks() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "viewalldate_dcr.aspx/getRemarksSF",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFremarks = JSON.parse(data.d) || [];                    
                    remarksReloadTable();
					 
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        
        function getOrders() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "viewalldate_dcr.aspx/getBrandwiseSalesUsr",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Gorders = JSON.parse(data.d) || [];
                    console.log(data.d);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getBrands() {
            
        }
        function ReloadTable() {
            
            $('#Product_Table thead').html('');
            $('#Product_Table tbody').html('');
            $('#Product_Table tfoot').html('');
            //var hstr = '<tr><th>Date</th><th>SR</th><th>Retailer Code</th><th>Retailer Name</th><th>Mobile</th><th>Channel</th><th>Class</th><th>Route</th><th>Distributor</th><th>Order Type</th>';

            var hstr = '';
            if ('<%=Session["div_code"]%>' == '179') {
                hstr = '<tr><th>Date</th><th>SR</th><th>Retailer Code</th><th>Retailer Name</th><th>Mobile</th><th>Channel</th><th>Class</th><th>Route</th><th>Joint Work</th> <th>Distributor</th><th>Order Type</th>';
            }
            else {
                hstr = '<tr><th>Date</th><th>SR</th><th>Retailer Code</th><th>Retailer Name</th><th>Mobile</th><th>Channel</th><th>Class</th><th>Route</th><th>Distributor</th><th>Order Type</th>';
            }

            var hstr2 = '<tr>';
            var hstr3 = '<tr>';
            let oprods = [];
            let mymap = new Map();
            oprods = Gorders.filter(function (el) {
                const val = mymap.get(el.Product_Code);
                if (val) {
                    return false;
                }
                mymap.set(el.Product_Code, el.Product_Name);
                return true;
            }).map(function (a) { return a; }).sort();
            for (var i = 0; i < oprods.length; i++) {
                hstr += '<th>' + oprods[i].Product_Name + '</th>';

            }
			if('<%=Session["div_code"]%>'=='128'){
                hstr += '<th>Order Value</th><th>Visit Purpose</th><th>Remarks</th><th>Submitted Address</th>';
			}
			else{
				hstr += '<th>Order Value</th><th>Remarks</th><th>Submitted Address</th>';
            }          
            hstr += '</tr>';
            hstr2 += '</tr>';

            $('#Product_Table thead').append(hstr);
            $('#Product_Table thead').append(hstr2);
            if ('<%=Session["div_code"]%>' == '179') {
                hstr3 += '<td colspan="11">Total</td>';
            }
            else {
                hstr3 += '<td colspan="10">Total</td>';
            }    

            //hstr3 += '<td colspan="10">Total</td>';
            var bstr = '';
            var totarr = [];
            var totapb = [];
            for (var i = 0; i < Gusers.length; i++) {
				let ordervalue = 0;
                var ar = 0;
                var br = 0;
                var csf = '';
                //bstr += '<tr><td>' + Gusers[i].ActivityDate + '</td><td>' + Gusers[i].SF_Name + '</td><td>' + Gusers[i].Trans_Detail_Info_Code + '</td><td>' + Gusers[i].Trans_Detail_Name + '</td><td>' + Gusers[i].Mobile + '</td><td>'+Gusers[i].Special+'</td><td>'+Gusers[i].Class+'</td><td>' + Gusers[i].SDP_Name + '</td><td>' + Gusers[i].stockist_name + '</td>';

                if ('<%=Session["div_code"]%>' == '179') {

                    bstr += '<tr><td>' + Gusers[i].ActivityDate + '</td><td>' + Gusers[i].SF_Name + '</td><td>' + Gusers[i].Trans_Detail_Info_Code + '</td><td>' + Gusers[i].Trans_Detail_Name + '</td><td>' + Gusers[i].Mobile + '</td><td>' + Gusers[i].Special + '</td><td>' + Gusers[i].Class + '</td><td>' + Gusers[i].SDP_Name + '</td><td>' + Gusers[i].Worked_with_Name + '</td><td>' + Gusers[i].stockist_name + '</td>';
                }
                else {
                    bstr += '<tr><td>' + Gusers[i].ActivityDate + '</td><td>' + Gusers[i].SF_Name + '</td><td>' + Gusers[i].Trans_Detail_Info_Code + '</td><td>' + Gusers[i].Trans_Detail_Name + '</td><td>' + Gusers[i].Mobile + '</td><td>' + Gusers[i].Special + '</td><td>' + Gusers[i].Class + '</td><td>' + Gusers[i].SDP_Name + '</td><td>' + Gusers[i].stockist_name + '</td><td>' + Gusers[i].OrderType + '</td>';
                }

                var filtt = Gorders.filter(function (a) {
                    return a.Sf_Code == Gusers[i].Sf_Code && a.Cust_Code == Gusers[i].Trans_Detail_Info_Code && a.Activity_Date == Gusers[i].Activity_Date;
                });

                ordervalue = filtt.reduce(function (prev, cur) {
                    return prev + cur.value;
                }, 0);

                //bstr += '<td>' + ((filtt.length > 0) ? filtt[0].OrderType : csf) + '</td>';
                for (var j = 0; j < oprods.length; j++) {
                    var filt = filtt.filter(function (a) {
                        return a.Sf_Code == Gusers[i].Sf_Code && a.Product_Code == oprods[j].Product_Code && a.Cust_Code == Gusers[i].Trans_Detail_Info_Code && a.Activity_Date == Gusers[i].Activity_Date; 
                        
                    });
                    totarr[ar] = ((filt[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + filt[0].Quantity) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                    ar++;
                    bstr += '<td>' + ((filt.length > 0) ? filt[0].Quantity : csf) + '</td>';
                   
                   
                }
				bstr += '<td>' + ordervalue + '</td>';
				if('<%=Session["div_code"]%>'=='128'){
					bstr+='<td>'+Gusers[i].visit_name+'</td>';
				}
                bstr += '<td><span data-val="' + Gusers[i].Activity_Remarks  + '">' + Gusers[i].Activity_Remarks + '</td><td style="white-space:nowrap;" class="latlng" lat="'+ Gusers[i].lat +'" lng="'+ Gusers[i].lng +'"></td>';
                totapb[br] = ((Gusers[i] != undefined) ? ((isNaN(totapb[br]) ? 0 : totapb[br]) + ordervalue) : ((isNaN(totapb[br]) ? 0 : totapb[br]) + 0));
                br++;
              
            }
          
            for (var i = 0; i < totarr.length; i++) {
                hstr3 += '<td>' + totarr[i] + '</td>';
            }
            for (var i = 0; i < totapb.length; i++) {
				if('<%=Session["div_code"]%>'=='128'){
					hstr3 += '<td>' + totapb[i].toFixed(2)  + '</td><td></td><td></td><td></td>';
				}
				else{
					hstr3 += '<td>' + totapb[i].toFixed(2)  + '</td><td></td><td></td>';
				}
            }
            hstr3 +='</tr >'
            $('#Product_Table tbody').append(bstr);
            $('#Product_Table tfoot').append(hstr3);
        }


        function getpriUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "viewalldate_dcr.aspx/primgetBrandwiseSales",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    prGusers = JSON.parse(data.d) || [];
                    console.log(data.d);
                    getpriBrands();
                    getpriOrders();
                    primReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function getpriOrders() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "viewalldate_dcr.aspx/primgetBrandwiseSalesUsr",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    prGorders = JSON.parse(data.d) || [];
                    console.log(data.d);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getpriBrands() {
            
        }
        function primReloadTable() {

            $('#Productpri_Table thead').html('');
            $('#Productpri_Table tbody').html('');
            $('#Productpri_Table tfoot').html('');
            var hstr = '<tr><th>Date</th><th>SR</th><th>Stockist Code</th><th>Stockist Name</th><th>Route</th><th>Order Type</th><th>Category</th>';
            var hstr2 = '<tr>';
            var hstr3 = '<tr>';
            let oprods = [];
            let mymap = new Map();
            oprods = prGorders.filter(function (el) {
                const val = mymap.get(el.Product_Code);
                if (val) {
                    return false;
                }
                mymap.set(el.Product_Code, el.Product_Name);
                return true;
            }).map(function (a) { return a; }).sort();
            for (var i = 0; i < oprods.length; i++) {
                hstr += '<th>' + oprods[i].Product_Name + '</th>';
            }
            hstr += '<th>Order Value</th><th>Remarks</th>';

            hstr += '</tr>';
            hstr2 += '</tr>';

            $('#Productpri_Table thead').append(hstr);
            $('#Productpri_Table thead').append(hstr2);
            hstr3 += '<td colspan=7>Total Value</td>';
            var bstr = '';
            var totarr = [];
            var totapb = [];
            for (var i = 0; i < prGusers.length; i++) {
                var ar = 0;
                var br = 0;
                var csf = '';
                bstr += '<tr><td>' + prGusers[i].ActivityDate + '</td><td>' + prGusers[i].SF_Name + '</td><td>' + prGusers[i].Trans_Detail_Info_Code + '</td><td>' + prGusers[i].stockist + '</td><td>' + prGusers[i].SDP_Name + '</td><td>' + prGusers[i].OrderType + '</td><td>' + prGusers[i].catename + '</td>';
                var filtp = prGorders.filter(function (a) {
                    return a.Sf_Code == prGusers[i].Sf_Code && a.Activity_Date == prGusers[i].Activity_Date;

                });
               // bstr += '<td>' + ((filtp.length > 0) ? filtp[0].OrderType : csf) + '</td>';
                for (var j = 0; j < oprods.length; j++) {
                    var filt = filtp.filter(function (a) {
                        return a.Sf_Code == prGusers[i].Sf_Code && a.Product_Code == oprods[j].Product_Code && a.Activity_Date == prGusers[i].Activity_Date && a.Stockist_Code == prGusers[i].Trans_Detail_Info_Code;

                    });
                    totarr[ar] = ((filt[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + filt[0].Quantity) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                    ar++;
                    bstr += '<td>' + ((filt.length > 0) ? filt[0].Quantity : csf) + '</td>';


                }
                bstr += '<td>' + prGusers[i].POB_Value + '</td><td>' + prGusers[i].Activity_Remarks + '</td>';
                totapb[br] = ((prGusers[i] != undefined) ? ((isNaN(totapb[br]) ? 0 : totapb[br]) + prGusers[i].POB_Value) : ((isNaN(totapb[br]) ? 0 : totapb[br]) + 0));
                br++;

            }

            for (var i = 0; i < totarr.length; i++) {
                hstr3 += '<td>' + totarr[i] + '</td>';
            }
            for (var i = 0; i < totapb.length; i++) {
                hstr3 += '<td>' + totapb[i] + '</td><td></td>';
            }
            hstr3 += '</tr >'
            $('#Productpri_Table tbody').append(bstr);
            $('#Productpri_Table tfoot').append(hstr3);
        }

        function remarksReloadTable() {

            $('#ProductRemark_Table thead').html('');
            $('#ProductRemark_Table tbody').html('');
            var hstr = '<tr><th>Date</th><th>FiedForce Name</th><th>Remarks</th></tr>';  

            $('#ProductRemark_Table thead').append(hstr);            
            var bstr = '';
             
            for (var i = 0; i < SFremarks.length; i++) {                 
                bstr += '<tr><td>' + SFremarks[i].Activity_Date + '</td><td>' + SFremarks[i].SF_Name + '</td><td>' + SFremarks[i].Remarks + '</td></tr>';
            }
            $('#ProductRemark_Table tbody').append(bstr);
        }
        function getdisthunt() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "viewalldate_dcr.aspx/getdisthuntdet",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    disthunt = JSON.parse(data.d) || [];                    
                    huntReloadTable();
					 
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function huntReloadTable() {

            $('#Disthunt_table thead').html('');
            $('#Disthunt_table tbody').html('');
            var hstr = '<tr><th>Date</th><th>FiedForce Name</th><th>Distributor Name</th><th>Owner Name</th><th>Mobile No</th><th>Area</th><th>Remarks</th></tr>';  

            $('#Disthunt_table thead').append(hstr);            
            var bstr = '';
             
            for (var i = 0; i < disthunt.length; i++) {                 
                bstr += '<tr><td>' + disthunt[i].Activity_Date + '</td><td>' + disthunt[i].SF_Name + '</td><td>' + disthunt[i].Shop_Name + '</td><td>' + disthunt[i].Contact_Person + '</td><td>' + disthunt[i].Phone_Number + '</td><td>' + disthunt[i].area + '</td><td>' + disthunt[i].Remarks + '</td></tr>';
            }
            $('#Disthunt_table tbody').append(bstr);
        }

        $(document).ready(function () {
            getUsers();
            getpriUsers();
            getdisthunt();
            //getRemarks();

            $('#btnExcel').click(function (event) {

                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };
                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };
                htmls = document.getElementById("Product_Table").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = /*'Expense_Report' + '.xls';   //create fname */"Brandwise_Sales.xls"

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                event.preventDefault();
            });
        });

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , tmplWorkbookXML = '<?xml version="1.0"?><?mso-application progid="Excel.Sheet"?><Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">'
                    + '<DocumentProperties xmlns="urn:schemas-microsoft-com:office:office"><Author>Axel Richter</Author><Created>{created}</Created></DocumentProperties>'
                    + '<Styles>'
                    + '<Style ss:ID="Currency"><NumberFormat ss:Format="Currency"></NumberFormat></Style>'
                    + '<Style ss:ID="Date"><NumberFormat ss:Format="Medium Date"></NumberFormat></Style>'
                    + '</Styles>'
                    + '{worksheets}</Workbook>'
                , tmplWorksheetXML = '<Worksheet ss:Name="{nameWS}"><Table>{rows}</Table></Worksheet>'
                , tmplCellXML = '<Cell{attributeStyleID}{attributeFormula}><Data ss:Type="{nameType}">{data}</Data></Cell>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (wsnames, wbname, appname) {
                var ctx = "";
                var workbookXML = "";
                var worksheetsXML = "";
                var rowsXML = "";
                var tables = $('table');
                for (var i = 0; i < tables.length; i++) {
                    for (var j = 0; j < tables[i].rows.length; j++) {
                        rowsXML += '<Row>'
                        for (var k = 0; k < tables[i].rows[j].cells.length; k++) {
                            var dataType = tables[i].rows[j].cells[k].getAttribute("data-type");
                            var dataStyle = tables[i].rows[j].cells[k].getAttribute("data-style");
                            var dataValue = tables[i].rows[j].cells[k].getAttribute("data-value");
                            dataValue = (dataValue) ? dataValue : tables[i].rows[j].cells[k].innerHTML;
                            var dataFormula = tables[i].rows[j].cells[k].getAttribute("data-formula");
                            dataFormula = (dataFormula) ? dataFormula : (appname == 'Calc' && dataType == 'DateTime') ? dataValue : null;
                            ctx = {
                                attributeStyleID: (dataStyle == 'Currency' || dataStyle == 'Date') ? ' ss:StyleID="' + dataStyle + '"' : ''
                                , nameType: (dataType == 'Number' || dataType == 'DateTime' || dataType == 'Boolean' || dataType == 'Error') ? dataType : 'String'
                                , data: (dataFormula) ? '' : dataValue.replace('<br>', '')
                                , attributeFormula: (dataFormula) ? ' ss:Formula="' + dataFormula + '"' : ''
                            };
                            rowsXML += format(tmplCellXML, ctx);
                        }
                        rowsXML += '</Row>'
                    }
                    ctx = { rows: rowsXML, nameWS: wsnames[i] || 'Sheet' + i };
                    worksheetsXML += format(tmplWorksheetXML, ctx);
                    rowsXML = "";
                }

                ctx = { created: (new Date()).getTime(), worksheets: worksheetsXML };
                workbookXML = format(tmplWorkbookXML, ctx);

                console.log(workbookXML);

                var link = document.createElement("A");
                link.href = uri + base64(workbookXML);
                link.download = wbname || 'alldatedcr.xls';
                link.target = '_blank';
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            }
        })();
        

        //$('#btnExport').click(function () {

        //    var htmls = "";
        //    var uri = 'data:application/vnd.ms-excel;base64,';
        //    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
        //    var base64 = function (s) {
        //        return window.btoa(unescape(encodeURIComponent(s)))
        //    };
        //    var format = function (s, c) {
        //        return s.replace(/{(\w+)}/g, function (m, p) {
        //            return c[p];
        //        })
        //    };
        //    htmls = document.getElementById("Product_Table").innerHTML;


        //    var ctx = {
        //        worksheet: 'Worksheet',
        //        table: htmls
        //    }
        //    var link = document.createElement("a");
        //    var tets = 'alldatedcr' + '.xls';   //create fname

        //    link.download = tets;
        //    link.href = uri + base64(format(template, ctx));
        //    link.click();
        //});

    </script>
</body>
</html>

