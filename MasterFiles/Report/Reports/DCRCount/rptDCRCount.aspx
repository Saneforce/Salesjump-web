<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRCount.aspx.cs" Inherits="Reports_DCRCount_rptDCRCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Count</title>
    <link type="text/css" rel="Stylesheet" href="../../../css/rptMissCall.css" />
    <style>
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
    </style>
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
            <asp:Panel ID="pnlContents" runat="server">
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="DCR Count Report for the Period " Font-Underline="True"
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                    <br />
                    <asp:Label ID="LblForceName" runat="server" Font-Underline="True" Font-Bold="True"
                        Font-Size="Small"></asp:Label>
                    <br />
                    <asp:Label ID="lblDcrCount" Style="color: red" Font-Underline="True" runat="server"
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </div>
                <br />
                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                    Width="95%">
                </asp:Table>
                <asp:GridView ID="GvDcrCount" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                    OnRowDataBound="GvDcrCount_RowDataBound">
                    <HeaderStyle BorderWidth="1" Font-Names="Verdana" Font-Size="8pt" />
                    <RowStyle BorderWidth="1" Font-Names="Verdana" Font-Size="8pt" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                    HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                    <ControlStyle Width="90%"></ControlStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblMonth" ForeColor="Red" runat="server" Text='<%# Bind("Date1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNextMonth" ForeColor="Red" runat="server" Text='<%# Bind("Date2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                            Visible="false" HeaderStyle-CssClass="" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSfcode" runat="server" Visible="false" Text='<%# Bind("sf_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="220px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last DCR Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblLast_DCR_Date" runat="server" Text='<%# Bind("activity_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DCR Count" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40"
                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDCR_Count" runat="server" Text='<%# Bind("Count_value") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval Pending Dates" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-Width="550" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblApproval_Pending_Dates" runat="server" Text='<%# Bind("Pendind_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="desig_color" ItemStyle-HorizontalAlign="Left" Visible="false"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="90%"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lbldesig_color" Visible="false" Text='<%# Bind("desig_color") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reporting_To_SF" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-Width="40" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                            <ControlStyle Width="220px"></ControlStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblReporting" Text='<%# Bind("Reporting_To_SF") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </center>
        <table width="100%" style="margin-left: 26px">
            <tr style="margin-left: 120px">
                <td width="100%">
                    <asp:Label ID="lblExplain" Text="(LP) - Leave Apporoval Pending  ,   (DAP) - DCR Approval Pending "
                        Font-Underline="True" Font-Bold="True" Font-Size="Small" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
