<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_View.aspx.cs" Inherits="Reports_Rpt_DCR_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR View Report</title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    
    <style type="text/css">
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
        .tbldetail_main
        {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }
        .tbldetail_Data
        {
            height: 18px;
        }
        .Holiday
        {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
        .NoRecord
        {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
    </style>
    <script language="Javascript" type="text/javascript">
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
                                            OnClientClick="RefreshParent();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for " Font-Underline="True"
                                Font-Size="Small" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%--<td align="center">
                            <span style="font-family: Verdana">Field Force Name :</span>
                            <asp:Label ID="lblFieldForceName" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvMyDayPlan" runat="server" Width="100%" HorizontalAlign="Center"
                                BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                CssClass="mGrid">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity_Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Pln_Date" runat="server" Font-Size="9pt" Text='<%#Eval("Pln_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Pln_Time" runat="server" Font-Size="9pt" Text='<%#Eval("Pln_Time")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cluster name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtClustername" runat="server" Font-Size="9pt" Text='<%#Eval("ClstrName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WorkType Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWorkTypeName" runat="server" Font-Size="9pt" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                        ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtremarks" runat="server" Font-Size="9pt" Text='<%#Eval("remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
        </center>
    </div>
    </form>
</body>
</html>
