<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMgr_Coverage.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rptMgr_Coverage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAN eReport</title>
    <script type="text/javascript" >
        function onReady(callback) {
            var intervalID = window.setInterval(checkReady, 1000);

            function checkReady() {
                if (document.getElementsByTagName('body')[0] !== undefined) {
                    window.clearInterval(intervalID);
                    callback.call(this);
                }
            }
        }

        function show(id, value) {
            document.getElementById(id).style.display = value ? 'block' : 'none';
        }

        onReady(function () {
            show('page', true);
            show('loading', false);
        });
    </script>
</head>

<style type="text/css">
   
   
     .tblCellFont
        {
            font-size:9pt;
            font-family:Calibri;
        }
        #loading {
    display: block;
    position: absolute;
    top: 0;
    left: 0;
    z-index: 100;
    width: 100vw;
    height: 100vh;
    background-color: rgba(192, 192, 192, 0.5);
    background-image: url("../../Images/loader.gif");
    background-repeat: no-repeat;
    background-position: center;
}
#page {
    display: none;
}
</style>
<script language="Javascript" type="text/javascript">
    function RefreshParent() {
        window.opener.document.getElementById('form1').click();
        window.close();
    }
</script>


<body>

    <form id="form1" runat="server">
    <div id="page">
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
            <center>
                <asp:Panel ID="pnlContents" runat="server">
                    <div align="center">
                    
                        <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " Font-Underline="True"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                    </div>
                    <br />
                    <table width="100%" align="center">
                        <tr>
                            <td>
                                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                    Width="95%">
                                </asp:Table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Table ID="tblhq" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                    Width="95%">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </center>
         
        </center>
    </div>
       <div id="loading"></div>
    </form>
</body>
</html>
