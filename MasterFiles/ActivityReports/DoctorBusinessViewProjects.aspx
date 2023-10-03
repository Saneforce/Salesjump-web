<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorBusinessViewProjects.aspx.cs"
    Inherits="DoctorBusinessViewProjects" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Business View Projects</title>
    <link type="text/css" href="css/Report.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:GridView ID="gvProjects" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                EmptyDataText="No Records Found" Width="90%">
                <Columns>
                    <asp:BoundField HeaderText="Product Name" DataField="Product_Detail_Name" />
                    <asp:BoundField HeaderText="Pack" DataField="Pack" />
                    <asp:BoundField HeaderText="Business Quantity" DataField="Product_Quantity" />
                </Columns>
            </asp:GridView>
        </center>
    </div>
    </form>
</body>
</html>
