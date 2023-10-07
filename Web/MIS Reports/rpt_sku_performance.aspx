<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_sku_performance.aspx.cs"
    EnableEventValidation="false" Inherits="MIS_Reports_rpt_sku_performance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SKU PERFORMANCE REPORT</title>
       <link type="text/css" rel="stylesheet" href="../css/style.css" />
       <script type="text/javascript">
           $(document).ready(function () {
               alert('hi');
           });
       </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="Dgv_SKU" runat="server" Width="95%" HorizontalAlign="Center" BorderWidth="1"
            GridLines="Both" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="Dgv_SKU_RowDataBound" ItemStyle-HorizontalAlign="Center">
        <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
