<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Details_Basedonfield_Level3.aspx.cs" Inherits="MIS_Reports_Visit_Details_Field3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Visit Details Field Report</title>
     <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />

     <script type = "text/javascript">
         var popUpObj;
         function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, cmode, Type) {
             popUpObj = window.open("Visit_Details_Basedonfield_Level4.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&cMode=" + cmode + "&Type=" + Type,
               "_blank",
        "ModalPopUp_Level1," +
         "0," +      
        "toolbar=no," +
        "scrollbars=yes," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "addressbar=no," +
        "resizable=yes," +
        "width=600," +
        "height=600," +
        "left = 0," +
        "top=0"
        );
             popUpObj.focus();
             //LoadModalDiv();
         }

    </script>

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
                                        onclick="btnPrint_Click"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnExcel_Click"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnPDF_Click"
                                        />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent()"
                                        />
                                </td>
                            </tr>

                          
                        </table>
                    </td>
                </tr>
            </table>
            <br />
    
    <br />
     <asp:Panel ID="pnlContents" runat="server">
    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Visit Customer Field for the month of " Font-Underline="True"
                            Font-Bold="True" Font-Size="8pt"></asp:Label>
                   </div>
            <br />
            
    <asp:Table ID="tbl" Font-Size="8pt" runat="server" BorderStyle="Solid" BorderColor="Black" style="border-collapse: collapse;  border: solid 1px Black;" BorderWidth="1" GridLines="Both" Width = "95%">
    </asp:Table>
    </asp:Panel>
     </center>    
    </div>
    </form>
</body>
</html>
