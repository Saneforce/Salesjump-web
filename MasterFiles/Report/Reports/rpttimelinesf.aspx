<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpttimelinesf.aspx.cs" Inherits="MasterFiles_Reports_rpttimelinesf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" onclick="tablesToExcel(['sflog'], 'VirtueleMachineInfo.xls', 'Excel')" />
        </div>

        <%-- <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />--%>
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Brandwise Sales" Style="margin-left: 10px; font-size: x-large"></asp:Label>

            </div>

        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div id="content">
            <table id="sflog" border="1" class="newStly" style="border-collapse: collapse;">
                <thead>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>

            </table>
            <br />
            <br />
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var stcode =<%=stcode%>;
            if (stcode == 2) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpttimelinesf.aspx/getdistsfdet",
                    data: "{'Div':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        disthunt = JSON.parse(data.d) || [];
                        disReloadTable();

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }

                });
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpttimelinesf.aspx/getretailsfdet",
                    data: "{'Div':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        disthunt = JSON.parse(data.d) || [];
                        RetReloadTable();

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }

                });
            }



        });
        function disReloadTable() {

            $('#sflog thead').html('');
            $('#sflog tbody').html('');
            var hstr = '<tr><th>Date</th><th>FiedForce Name</th><th>Distributor Name</th><th>Order value</th><th>Remarks</th></tr>';

            $('#sflog thead').append(hstr);
            var bstr = '';

            for (var i = 0; i < disthunt.length; i++) {
                bstr += '<tr><td>' + disthunt[i].order_date + '</td><td>' + disthunt[i].Sf_Name + '</td><td>' + disthunt[i].Stockist_Name + '</td><td>' + disthunt[i].Order_value + '</td><td>' + disthunt[i].Remarks + '</td></tr>';
            }
            $('#sflog tbody').append(bstr);
        }
        function RetReloadTable() {

            $('#sflog thead').html('');
            $('#sflog tbody').html('');
            var hstr = '<tr><th>Date</th><th>FiedForce Name</th><th>Retailer Name</th><th>Order value</th><th>Remarks</th></tr>';

            $('#sflog thead').append(hstr);
            var bstr = '';

            for (var i = 0; i < disthunt.length; i++) {
                bstr += '<tr><td>' + disthunt[i].Activity_Time + '</td><td>' + disthunt[i].Sf_Name + '</td><td>' + disthunt[i].Retailer_name + '</td><td>' + disthunt[i].Order_value + '</td><td>' + disthunt[i].Activity_Remarks + '</td></tr>';
            }
            $('#sflog tbody').append(bstr);
        }
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
    </script>
</body>
</html>
