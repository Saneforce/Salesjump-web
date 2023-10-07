<%@ Page Title="Expense Approval" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MGR.master" CodeFile="Leave_Approval.aspx.cs" Inherits="MasterFiles_MGR_Leave_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Expense Approval</title>
        <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%--<ucl:Menu ID="menu1" runat="server" />--%> 
        <br />
          <center>
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblcrm" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                            ForeColor="Chocolate">Expense</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="mGridImg"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Field Force Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Name" Width="160px" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Width="80px" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="App" DataNavigateUrlFormatString="../MGR/WrkTypeWise_Allowance.aspx?Month={0}&Year={1}&sf_code={2}&sf_name={3}"
                                    DataNavigateUrlFields="Month,Year,sf_code,sf_name" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <br />
        <center>
     <%-- <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>--%>
    </div>
    </form>
</body>
</html>
</asp:Content>