<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptFieldwork_Analysis.aspx.cs" Inherits="MasterFiles_AnalysisReports_rptFieldwork_Analysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
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
            border-style: solid;
        }
    </style>
      <style type="text/css">
    
.ball {
    background-color: rgba(0,0,0,0);
    border: 5px solid rgba(0,183,229,0.9);
    opacity: .9;
    border-top: 5px solid rgba(0,0,0,0);
    border-left: 5px solid rgba(0,0,0,0);
    border-radius: 50px;
    box-shadow: 0 0 35px #2187e7;
    width: 50px;
    height: 50px;
    margin: 0 auto;
    -moz-animation: spin .5s infinite linear;
    -webkit-animation: spin .5s infinite linear;
}

.ball1 {
    background-color: rgba(0,0,0,0);
    border: 5px solid rgba(0,183,229,0.9);
    opacity: .9;
    border-top: 5px solid rgba(0,0,0,0);
    border-left: 5px solid rgba(0,0,0,0);
    border-radius: 50px;
    box-shadow: 0 0 15px #2187e7;
    width: 30px;
    height: 30px;
    margin: 0 auto;
    position: relative;
    top: -50px;
    -moz-animation: spinoff .5s infinite linear;
    -webkit-animation: spinoff .5s infinite linear;
}

.divProgress
{
    margin-top:40%;
}

@-moz-keyframes spin {
    0% {
        -moz-transform: rotate(0deg);
    }

    100% {
        -moz-transform: rotate(360deg);
    };
}

@-moz-keyframes spinoff {
    0% {
        -moz-transform: rotate(0deg);
    }

    100% {
        -moz-transform: rotate(-360deg);
    };
}

@-webkit-keyframes spin {
    0% {
        -webkit-transform: rotate(0deg);
    }

    100% {
        -webkit-transform: rotate(360deg);
    };
}

@-webkit-keyframes spinoff {
    0% {
        -webkit-transform: rotate(0deg);
    }

    100% {
        -webkit-transform: rotate(-360deg);
    };
}
</style>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body id="PageId" runat="server">
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
              <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>

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
                                    <asp:Button ID="btnExcel" runat="server"  Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Visible="false" Text="PDF" Font-Names="Verdana" Font-Size="10px"
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
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Campaign Drs - View" Font-Underline="True"
                        Font-Names="Calibri" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </div>
            </center>
            <br />
             <center>
            <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Calibri"
                Font-Size="9pt"></asp:Label>
           </center>
           <br />
           &nbsp;&nbsp;
           <asp:Label ID="lbljw" runat="server"  Font-Names="Calibri" ForeColor="Red" Font-Size="9pt">
            * Joint Work Visits based on 'MR' DCR - Entry 
           </asp:Label>
            <center>
            <asp:Panel ID="pnlContents" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td>
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="100%">
                            </asp:Table>
                              
                        </td>
                    </tr>
                </table>
            </asp:Panel>
     </center>
         <%--  <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="200">
 </asp:Timer>
         <div class="divProgress" id="ShowProgressDiv" runat="server">
                    <div class="ball" id="ball" runat="server">
                    </div>
                    <div class="ball1" id="ball1" runat="server">
                    </div>
                </div>--%>
    </div>
    </form>
</body>
</html>
