<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="rpt_Order_Detail_View1.aspx.cs"
    Inherits="MIS_Reports_rpt_Order_Detail_View1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Order Detail View</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script language="Javascript">
        function RefreshParent() {
         //   window.opener.document.getElementById('form1').click();
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
        .rptCellBorder
        {
            border: 1px solid;
            border-color: white;
            width: 10px;
        }
        tbody {
    display: table-row-group;
    vertical-align: top;
    border-color: inherit;
}
        
        .remove
        {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="80%" align="center">
                        <asp:Label ID="lblHead" Text="Order Detail View" SkinID="lblMand" Font-Bold="true"
                            Font-Underline="true" runat="server"/>
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
									
								<!-- <td>
                                    <asp:Button ID="btnExport" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderWidth="1" Height="25px" Width="60px" BorderColor="Black" BorderStyle="Solid"
                                        OnClick="btnExport_Click" />
                                </td> -->

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
                <table width="90%" align="center">
                <tr>
                       <%-- <td>
                            <asp:Label ID="Lbl_Route_name" Text="" SkinID="lblMand" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Lbl_Retailer" Text="" SkinID="lblMand" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Lbl_Date" runat="server" Font-Bold="true" SkinID="lblMand" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_order_no" runat="server" Font-Bold="true" SkinID="lblMand" Text=""></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td width="2.5%">
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                             <asp:Label ID="Lbl_Route_name" Text="" SkinID="lblMand" Font-Bold="true" runat="server"></asp:Label>
                             <br />
                            <asp:Label ID="Lbl_Retailer" Text="" SkinID="lblMand" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="Lbl_Date" runat="server" Font-Bold="true" SkinID="lblMand" Text=""></asp:Label>
                            <br />
                             <asp:Label ID="lbl_order_no" runat="server" Font-Bold="true" SkinID="lblMand" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <table width="100%" align="center">
                <tbody align="center">
                 
                    <tr>
                         <td align="center">
                            <asp:Table ID="tbl" runat="server" Style="border-collapse: collapse; border: solid 1px white;
                                font-family: Calibri" Font-Size="10pt" GridLines="Both" Width="100%"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="center">                           
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
