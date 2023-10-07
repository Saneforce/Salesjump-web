<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissedDocList.aspx.cs" Inherits="MIS_Reports_MissedDocList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missed Doctor List</title>
    <link type="text/css" rel="stylesheet" href="../css/Report.css" />
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
            <br />
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
                                        OnClientClick="RefreshParent()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="pnlContents" runat="server">
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Missed Customer List for the month of "
                        Font-Underline="True" Font-Bold="True" Font-Size="9pt"></asp:Label>
                    <br />
                    <asp:Label ID="lblsubhead" runat="server" Font-Underline="True" Visible="false" Font-Bold="True"
                        Font-Size="9pt"></asp:Label>
                </div>
                <br />
                <div>
                    <asp:GridView ID="grdDoctor" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                        BorderWidth="1" HeaderStyle-BackColor="#0097AC" Font-Size="8pt" HorizontalAlign="Center"
                        Width="85%">
                        <HeaderStyle BackColor="#0097AC" ForeColor="White" BorderWidth="1" />
                        <RowStyle BorderWidth="1" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="#0097AC" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Listed Doctor Code" HeaderStyle-BackColor="#0097AC" ItemStyle-HorizontalAlign="Left"
                                Visible="false">
                                <ControlStyle Width="90%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDRCode" HeaderStyle-BackColor="#0097AC" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-BackColor="#0097AC" HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="90%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-BackColor="#0097AC" HeaderText="Qual" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-BackColor="#0097AC" HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-BackColor="#0097AC" HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-BackColor="#0097AC" HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
