<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTotalRetailSal.aspx.cs" Inherits="MIS_Reports_rptTotalRetailSal" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var CDtls = [];
            var diDtls = [];
            var sfCode = $('#<%=hsfcode.ClientID%>').val();
            var fyear = $('#<%=hfyear.ClientID%>').val();
            var fmonth = $('#<%=hfmonth.ClientID%>').val();
            var subDiv = $('#<%=hsubdiv.ClientID%>').val();
           $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptTotalRetailSal.aspx/getdistributr",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    diDtls = JSON.parse(data.d);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptTotalRetailSal.aspx/getOrderDetails",
                data: "{'SF_Code':'" + sfCode + "', 'FYear':'" + fyear + "' ,'FMonth':'" + fmonth + "' ,'SubDiv':'" + subDiv + "' }",
                dataType: "json",
                success: function (data) {
                    $('#tblHQSale tbody').html('');
                    CDtls = data.d;
                    console.log(CDtls);
                    str = '';
                    for (var i = 0; i < CDtls.length; i++) {
                        var flit = diDtls.filter(function (a) {
                            return a.Stockist_Code == CDtls[i].disname;
                        });
						let Stockist_Name=(flit.length>0)?(flit[0].Stockist_Name):"";
                        str += "<tr><td>" + (i + 1) + "</td><td>" + CDtls[i].ListedDrCode + "</td><td>" + CDtls[i].custName + "</td><td>" + CDtls[i].CrDate + "</td><td>" + CDtls[i].CustCode + "</td><td>" + CDtls[i].rCode + "</td><td>" + CDtls[i].disname + "</td><td>" + CDtls[i].lastordDate + "</td><td>" + CDtls[i].Doc_Spec_ShortName + "</td><td>" + CDtls[i].Doc_Class_ShortName + "</td><td>" + CDtls[i].orderVal + "</td><td>" + CDtls[i].Activity_Date + "</td><td>" + CDtls[i].visitcount + "</td><td>" + CDtls[i].visitdate + "</td></tr >";
                    }
                    $('#tblHQSale tbody').append(str);
                },
                error: function (rs) {
                    console.log(rs);
                }
            });

        });
        function fnExcelReport() {
            var a = document.createElement('a');
            var fileName = 'Test file.xls';
            var blob = new Blob([$('div[id$=excelDiv]').html()], {
                type: "application/csv;charset=utf-8;"
            });
            document.body.appendChild(a);
            a.href = URL.createObjectURL(blob);
            a.download = 'SummaryReport.xls';
            a.click();
            e.preventDefault();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <a class="btn btnExcel" onclick="fnExcelReport()"></a>
            <a class="btn btnClose" href="javascript:window.open('','_self').close();"></a>
        </div>
        <div id="excelDiv" class="container" style="max-width: 100%; width: 95%;">
            <asp:HiddenField ID="hsfcode" runat="server" />
            <asp:HiddenField ID="hfyear" runat="server" />
            <asp:HiddenField ID="hfmonth" runat="server" />
            <asp:HiddenField ID="hsubdiv" runat="server" />
            <asp:Label ID="lblhead" runat="server" Style="font-size: x-large"></asp:Label>
            <br />
            <asp:Label ID="subhead" runat="server"></asp:Label>
           <table id="tblHQSale" class="table table-responsive newStly" style="width: 100%;" border="1">
                <thead>
                    <tr>
						<th>S.NO</th>
						<th>Retailer Code</th>
						<th>Retailer Name</th>
						<th>Created Date</th>
						<th>Route Name</th>
						<th>Field Force</th>
						<th>Distributor</th>
						<th>Phone Number</th>
						<th>Category</th>
						<th>Class</th>
						<th>Order Value</th>
						<th>Last Visit Date</th>
                                                <th>Retailer VisitCount</th>
                                                <th>Retailer VisitDates</th>
                    </tr>
                </thead>
                <tbody>
               </tbody>
            </table>
        </div>
    </form>
</body>
</html>
