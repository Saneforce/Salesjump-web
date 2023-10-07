<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TPMGRApproval.aspx.cs" Inherits="MasterFiles_MGR_TPMGRApproval" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tour Plan Approval System</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        </div>
        <div>
        <center>
            <table cellpadding="8" width="85%" cellspacing="8">
                <tr align="right">
                    <td>
                        <asp:HyperLink ID="hypLinkApproval" runat="server" Text="Confirm List View" style="font-size:10pt;  font-family:Verdana; font-weight:bold"  NavigateUrl="~/MasterFiles/MGR/TP_Approve.aspx"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    <asp:Label ID="lblNextMonthTitle" Text="You Can't Enter the Tour Plan For the Month Of" style="font-size:10pt; font-family:Verdana;color:#004A93;"  Font-Bold="true" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </center>
                <br />
                <center>

               <%-- <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" AutoGenerateColumns="false" 
                                    GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns> 
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
                                       <asp:HyperLinkField DataTextField="Sf_Code" 
                                            DataNavigateUrlFormatString="TP_Approval.aspx?refer={0}"
                                            DataNavigateUrlFields="Sf_Code" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
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
                </table>  --%>
                
                 <asp:Table ID="tbl" CellSpacing="2" CellPadding="2" runat="server" Style="border-collapse:collapse;  border: solid 1px Black;
                                 font-family: Calibri; margin-top:0px;" Font-Size="8pt" GridLines="Both" Width="75%">
                            </asp:Table>         
        </center>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
