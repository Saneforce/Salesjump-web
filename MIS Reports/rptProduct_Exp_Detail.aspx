<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProduct_Exp_Detail.aspx.cs" Inherits="MIS_Reports_rptProduct_Exp_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Exposure Analysis</title>
 <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />

  <script type="text/javascript">
      var popUpObj;
      function showModalPopUp(sfcode, sf_name, cyear, cmonth, Prod_Name, Prod, sCurrentDate) {
          popUpObj = window.open("rptProduct_Exp_Detail1.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate,
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

      function showModal(sfcode, sf_name, Prod_Name, Prod) {
          popUpObj = window.open("rptProduct_Exp_Detail1.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod,
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
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        
        .remove  
  {
    text-decoration:none;
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
                    <asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
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
            <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
        </div>
        <div>
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" SkinID="lblMand" Font-Bold="true" ></asp:Label>
                        </td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                        <asp:Label ID="lblprd_name" Text="Product Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblname" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
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