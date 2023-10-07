<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSalesReport.aspx.cs" Inherits="MasterFiles_Reports_SecSalesReport" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Sales & Stock statement </title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, stockcode, type) {
            popUpObj = window.open("rptSecSales.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&stockcode=" + stockcode + "&type=" + type,
                                    "ModalPopUp",
                                    "toolbar=no," +
                                    "scrollbars=yes," +
                                    "location=no," +
                                    "statusbar=no," +
                                    "menubar=no," +
                                    "addressbar=no," +
                                    "resizable=yes," +
                                    "width=800," +
                                    "height=600," +
                                    "left = 0," +
                                    "top=0"
                                    );
            popUpObj.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
     <div id="Divid" runat="server">
    </div>
    <br />
       <center>
        <br />
            <table >
                <tr>
                    <td>
                        <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack ="true"  SkinID="ddlRequired" onselectedindexchanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStockiest" runat="server" Text="Stockiest" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStockiest" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>                   
                <tr>
                    <td>
                        <asp:Label ID="lblFMonth" runat="server" Text="From Month " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Label ID="lblFYear" runat="server" Text="From Year " SkinID="lblMand"></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>                   
                <tr>
                    <td>
                        <asp:Label ID="lblTMonth" runat="server" Text="To Month " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Label ID="lblTYear" runat="server" Text="To Year " SkinID="lblMand"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblrptType" runat="server" Text="Report Type" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdolstrpt" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text = "Consolidated&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text = "Monthwise"></asp:ListItem>
                        </asp:RadioButtonList>                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="BUTTON" 
                            onclick="btnGo_Click"/>
                    </td>
                </tr>
            </table>

        </center>    
    
    </form>
</body>
</html>
