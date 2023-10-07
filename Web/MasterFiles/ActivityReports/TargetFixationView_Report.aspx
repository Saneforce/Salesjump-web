<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetFixationView_Report.aspx.cs"
    Inherits="TargetFixationView_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Target Fixation Report</title>
    <link type="text/css" href="css/Report.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div style="width: 90%;text-align:center">
    <u><asp:Label ID="lblHeader" runat="server"></asp:Label></u><br />
    Field Force Name : <asp:Label ID="lblFieldForce" runat="server"></asp:Label>
    </div>
        <div style="width: 90%;text-align:left">
            <asp:GridView ID="gvTargetReport" runat="server" AutoGenerateColumns="true" CssClass="mGrid"
                EmptyDataText="No Records Found">
            </asp:GridView>
        </div>
    </center>
    </form>
</body>
</html>
