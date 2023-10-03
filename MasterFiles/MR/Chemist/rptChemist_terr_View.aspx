<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptChemist_terr_View.aspx.cs"
    Inherits="MasterFiles_MR_Chemist_rptChemist_terr_View" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemist View</title>
    <link type="text/css" rel="stylesheet" href="../../../css/Report.css" />
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
            <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <div>
                    <asp:Label ID="lblHead" runat="server" Text="Chemist Details" Font-Underline="True"
                        Font-Bold="True"></asp:Label>
                </div>
                <br />
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdChemist" runat="server" Width="85%" HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" AutoGenerateColumns="false" CssClass="mGrid"
                                    OnRowDataBound="grdChemist_RowDataBound"  
                                    AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdChemist.PageIndex * grdChemist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("Chemists_Address1")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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
