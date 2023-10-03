<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="rpt_Secondary_Sales_View.aspx.cs" Inherits="MIS_Reports_rpt_Secondary_Sales_View"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary_Sales View</title>
     <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
     <link href="../css/style.css" rel="stylesheet" /> 
     <script language="Javascript">
         function RefreshParent() {
             //window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(Brandcode, Brandname, cyear, cmonth, sCurrentDate, type) {
            popUpObj = window.open("rptPurchas_Register_Brandwise_view.aspx?Brand_Code=" + Brandcode + "&Brand_name=" + Brandname + " &Year=" + cyear + "&Month=" + cmonth + "&sCurrentDate=" + sCurrentDate + "&Type=" + type,
     "_blank",
    "ModalPopUp," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=700," +
    "height=500," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }
    

   
    </script>
      <style type="text/css">
        #DGVFFO th
        {
            text-align: center;
        }
        #DGVFFO td
        {
            padding: 4px 4px;
        }
        #DGVFFO td:nth-child(3), #DGVFFO td:nth-child(4)
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Secondary Sales View" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
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
<td> <asp:Button ID="btnExport" runat="server" Text="PDF"  Font-Names="Verdana" Font-Size="10px"  BorderWidth="1" Height="25px" Width="60px"
                                        BorderColor="Black" BorderStyle="Solid" onclick="btnExport_Click" /></td>
                               
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
         <asp:Label ID="lblRegionName" runat="server" Text="Label"></asp:Label> 
        </div>
        <div>
             <center>
                <table  align="center">
                    <tr>                   
                       
                        <td width="500px">
            <asp:Label ID="DistributorName" runat="server" Text="Distributor Name"  
                Font-Bold="True" Font-Names="Andalus"></asp:Label>
                <asp:Label ID="dist" runat="server" Font-Size="12px" Font-Underline="True"></asp:Label></td>
                        <td align="left">
                            <asp:Label ID="lblIDFieldforce"  Font-Bold="True" Font-Names="Andalus" Text="Fieldforcename :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblfield" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
                    </center>
            </div>
            <br />

            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="DGVFFO" runat="server" Style="border-collapse: collapse; border: solid 1px Black;"
                            Width="95%" CssClass="newStly">
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td>
                        &nbsp;</td></tr>

                </tbody>
            </table> 


            </div>      
    </asp:Panel>
    </form>
</body>
</html>
