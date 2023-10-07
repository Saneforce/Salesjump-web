<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSalesSetUp.aspx.cs" Inherits="SecondarySales_SecSalesSetUp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales Setup</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <center>
        <br />
        <table align="center" border="1" >
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Is Total Needed in between + and - : " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoTotalNeeded" runat="server" RepeatDirection="Horizontal" Font-Names="Verdana" Font-Size="8" >
                            <asp:ListItem Value = "1" Text = "Yes"></asp:ListItem>
                            <asp:ListItem Value = "0" Text = "No"></asp:ListItem>
                        </asp:RadioButtonList>                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValue" runat="server" Text="Is Value Needed for Total : " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoValueNeeded" runat="server" RepeatDirection="Horizontal" Font-Names="Verdana" Font-Size="8" >
                            <asp:ListItem Value = "1" Text = "Yes"></asp:ListItem>
                            <asp:ListItem Value = "0" Text = "No"></asp:ListItem>
                        </asp:RadioButtonList>                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblApproval" runat="server" Text="Approval System Needed?" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoApproval" runat="server" RepeatDirection = "Horizontal" Font-Names="Verdana" Font-Size="8" >
                            <asp:ListItem Value = "0" Text = "Yes"></asp:ListItem>
                            <asp:ListItem Value = "1" Text = "No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>                    
                        <asp:Label ID="lblSale" runat="server" Text="Sale calculated rate based on: " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoSale" runat="server" RepeatDirection = "Horizontal" RepeatColumns="3" Font-Names="Verdana" Font-Size="8" >
                            <asp:ListItem Value = "M" Text = "MRP Price"></asp:ListItem>
                            <asp:ListItem Value = "R" Text = "Retailor Price"></asp:ListItem>
                            <asp:ListItem Value = "D" Text = "Distributor Price"></asp:ListItem>
                            <asp:ListItem Value = "T" Text = "Target Price"></asp:ListItem>
                            <asp:ListItem Value = "N" Text = "NSR Price"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>                    
                        <asp:Label ID="lblSecSale" runat="server" Text="Reporting Field: " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBoxList ID="chklstSecSale" runat="server" RepeatDirection = "Horizontal" RepeatColumns="3" Font-Names="Verdana" Font-Size="8" >                        
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>                    
                        <asp:Label ID="lblProd" runat="server" Text="Product Grouping: " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoProd" runat="server" RepeatDirection = "Horizontal" RepeatColumns="3" Font-Names="Verdana" Font-Size="8" >
                            <asp:ListItem Value = "0" Text = "Not Required"></asp:ListItem>
                            <asp:ListItem Value = "C" Text = "Category"></asp:ListItem>
                            <asp:ListItem Value = "G" Text = "Group"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </tbody>
        </table>

        <br />


        <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save" 
                CssClass="BUTTON" onclick="btnSubmit_Click"/>
        &nbsp;
        <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" 
                Height="25px" Text="Clear" onclick="btnClear_Click" />

        </center>


        


    </div>
    </form>
</body>
</html>
