<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCampaign_View.aspx.cs"
    Inherits="Reports_rptCampaign_View" %>

<%--
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Campaign View</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        table
        {
            border-collapse: collapse;
        }
    </style>
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
        <center>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Campaign Listed Doctors View" Font-Underline="True"
                        Font-Bold="True"></asp:Label>
                </div>
                <br />
                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                    Width="100%">
                </asp:Table>
            </asp:Panel>
        </center>
        <%--    <br />
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" CssClass="mGrid" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Force Name"  ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Name"  ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qual." ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                       
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </center>--%>
    </div>
    </form>
</body>
</html>
