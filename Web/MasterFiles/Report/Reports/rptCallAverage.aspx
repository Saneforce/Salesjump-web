<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCallAverage.aspx.cs" Inherits="Reports_rptCallAverage" %>
<%--<%@ Register Src ="~/UserControl/ExportData.ascx" TagName ="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Call Average</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <%-- <div>
      <ucl2:Menu2 ID="menu1" runat="server" />
    </div>--%>
    <div>
    <%--<asp:HyperLink Target="_parent"--%>

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
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <div align="center">
            <asp:Label ID="lblHead" Text="Call Average Sheet" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>
        </div>
        <div>
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left" width="45%">
                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                    </tr>
                </table>
           </div>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>
                        </td>
                    </tr>
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
    </form>
</body>
</html>
