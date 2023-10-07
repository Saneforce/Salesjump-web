<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDoctorDetails.aspx.cs" Inherits="Reports_rptDoctorDetails" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Details - Field Force wise</title>
    
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
        <br /><br />
        <center>
        <table>
            <tr>
                <td align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Customer Details - Field Force wise" Font-Underline="True" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblFF" runat="server" Text="Field Force wise"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                        onselectedindexchanged="ddlFieldForce_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
       <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" OnRowCreated="grdSalesForce_RowCreated"
                        OnRowDataBound ="grdSalesForce_RowDataBound"
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" ShowHeader="False">
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    

                        </Columns>
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
     </center>          
    </div>
    </form>
</body>
</html>
