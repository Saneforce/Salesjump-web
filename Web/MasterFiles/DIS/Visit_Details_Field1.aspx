<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Details_Field1.aspx.cs" Inherits="MIS_Reports_Visit_Details_Field1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Details Field Report</title>
    <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />

    <script type = "text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, cmode) {
            popUpObj = window.open("Visit_Details_Field2.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&cMode=" + cmode,
        "ModalPopUp_Level1",
        "toolbar=no," +
        "scrollbars=yes," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "addressbar=no," +
        "resizable=0," +
        "width=1000," +
        "height=800," +
        "left = 150," +
        "top=150"
        );
            popUpObj.focus();
            LoadModalDiv();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
    
            <table>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Visit Customer Field for the month of " Font-Underline="True"
                            Font-Bold="True" Font-Size="Small"></asp:Label>
                    </td> 
                </tr>

                 
            </table>
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
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        />
                                </td>
                            </tr>

                          
                        </table>
                    </td>
                </tr>
            </table>
            <br />
    
    <br />
    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width = "95%">
    </asp:Table>

     </center>          
    

    
    
    </div>
    </form>
</body>
</html>
