<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptNewOutletPenetration.aspx.cs" Inherits="MIS_Reports_rptNewOutletPenetration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NEW OUTLET PENETRATION</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
             $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('excelDiv');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'Reason_Analysis_' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="HSFCode" runat="server" />
        <br />
        <div class="container" style="max-width: 100%; width: 98%">
            <div class="row">
                <div class="col-md-8">
                </div>
                <div class="col-md-4" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none" href="#" class="btn btnPrint"></a>
                    <a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px; display: none" href="#" class="btn btnPdf"></a>
                    <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>

                </div>
            </div>
        </div>
        <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
            <asp:Label runat="server" ID="lblHead" Style="font-size: x-large"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblsfname"></asp:Label>
            <asp:GridView ID="GVData" runat="server" class="newStly"></asp:GridView>
            <table id="FFTable" class="newStly" runat="server" style="border-collapse: collapse;">
            </table>

            <asp:Table ID="DGVFFO" runat="server" Style="border-collapse: collapse; border: solid 1px Black;"
                Width="95%" CssClass="newStly">
            </asp:Table>
        </div>
    </form>
</body>
</html>
