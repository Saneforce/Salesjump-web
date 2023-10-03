<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDSRCallsDetails.aspx.cs" Inherits="MIS_Reports_rptDSRCallsDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script language="Javascript">
        $(document).ready(function () {

            $(document).on('click', "#btnExcel", function (e) {
                var a = document.createElement('a');
                var data_type = 'data:application/vnd.ms-excel';
                a.href = data_type + ', ' + encodeURIComponent($('div[id$=excelDiv]').html());
                document.body.appendChild(a);
                a.download = 'DSR_Monitoring_Report.xls';
                a.click();
                e.preventDefault();
            });
        });
            </script>
</head>
<body>
     <form id="form1" runat="server">
        <br />
        <div class="container" style="max-width: 100%; width: 100%; text-align:right; padding-right:50px"  ">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px; display:none;" class="btn btnPrint" />
            <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px; display:none" class="btn btnPdf"  />
            <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>       
         
         <div id="excelDiv" class="container" style="max-width: 100%; width: 65%">
         <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large"></asp:Label>
         <br />
         <asp:Label ID="lblhead" runat="server" ></asp:Label>         
             <asp:GridView ID="dgvRetailers" runat="server"  class="newStly" ></asp:GridView>
         </div>

    </form>
</body>
</html>
