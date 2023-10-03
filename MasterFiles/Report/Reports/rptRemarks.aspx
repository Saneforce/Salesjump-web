<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRemarks.aspx.cs" Inherits="Reports_rptRemarks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>rptRemarks</title>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlbutton" runat="server">
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
                                    OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div align="center">
            <asp:Label ID="lblHead" SkinID="lblMand" Font-Underline="true" runat="server"></asp:Label>
        </div>
        <br />
        <asp:GridView ID="grdRemarks" runat="server" Width="50%" HorizontalAlign="Center"
            BorderStyle="Solid" BorderWidth="1" AutoGenerateColumns="false" Font-Size="9pt"
            EmptyDataText="No Data found for View" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
            <HeaderStyle Font-Bold="False" />
            <PagerStyle CssClass="pgr"></PagerStyle>
            <SelectedRowStyle BackColor="BurlyWood" />
            <HeaderStyle BorderStyle="Solid" />
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                <asp:TemplateField HeaderText="S.No"  
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-BackColor="#0097AC"
                    HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Submission_Date") %>' Width="90px"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" 
                    HeaderStyle-BackColor="#0097AC" HeaderStyle-BorderColor="Black"
                    HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Remarks") %>' Width="350px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                Width="80%" VerticalAlign="Middle" />
        </asp:GridView>
    </asp:Panel>
    </form>
</body>
</html>
