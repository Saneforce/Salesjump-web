<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSales_Edit.aspx.cs" Inherits="MasterFiles_Options_SecSales_Edit" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Secondary Sales Edit</title>
       <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <ucl:Menu ID="menu1" runat="server" />
       <center>
        <br />
            <table >
                <tr>
                    <td>
                        <asp:Label ID="lblSF" runat="server" Text="Field Force " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFMonth" runat="server" Text="Month " SkinID="lblMand"></asp:Label>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFYear" runat="server" Text="Year " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>                   
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="BUTTON" 
                            onclick="btnGo_Click"/>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdSecSales" runat="server" Width="40%" HorizontalAlign="Center" GridLines="None" 
                EmptyDataText="No Records Found"   
                AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
                <HeaderStyle Font-Bold="False" />
                <SelectedRowStyle BackColor="BurlyWood" />
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stockiest Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStockiestCode" runat="server" Text='<%#Eval("Stockiest_Code")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stockiest Name" ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblStockiestName" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Allow Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSaleEntry" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
            </asp:GridView>
        <br />
        <table>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Update" CssClass="BUTTON" Visible="false"
                    OnClientClick="return confirm('Do you want to allow Sec Sales Edit for the selected stockiest(s)');" 
                    onclick="btnSubmit_Click"/>
            </td>
        </tr>
    </table>
    </center>    
    </div>

    </form>
</body>
</html>
