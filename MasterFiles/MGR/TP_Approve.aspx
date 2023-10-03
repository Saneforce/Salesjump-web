<%@ Page Title="Tour Plan Approval" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MGR.master" CodeFile="TP_Approve.aspx.cs" Inherits="MasterFiles_MGR_TP_Approve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Tour Plan Approval</title>
    
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
      
    <div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        
        <br />
   
        <center>                
            <asp:Panel ID="pnlTP_Date" runat="server">
             <table width="75%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                    OnRowDataBound="grdTP_RowDataBound" EmptyDataText="TP No found for Approval"
                                    GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                                                <asp:Label ID="lblSF_Name" Width="220px" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF_HQ" Width="120px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tour Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonth" Width="80px" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tour Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" 
                                            DataNavigateUrlFormatString="~/MasterFiles/MR/TourPlan.aspx?refer={0}"
                                            DataNavigateUrlFields="key_field" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
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
                    </tbody>
                </table>
            </asp:Panel>
        </center>
       
    </div>
    </form>
</body>
</html>
</asp:Content>