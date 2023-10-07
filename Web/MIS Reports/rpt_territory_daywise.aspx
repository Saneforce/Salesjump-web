<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="rpt_territory_daywise.aspx.cs" Inherits="MIS_Reports_rpt_territory_daywise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Territorywise Product Detail Daywise</title>
 <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />


     <script language="Javascript">
         function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
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
               
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Territorywise Product Detail Daywise" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                   <td width="20%" align="right">
                      <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
                                                                        <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
   
								  <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
<asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>




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
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            &nbsp;</td>
                       
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
                            <asp:Table ID="tbl"  runat="server" class="newStly"  GridLines="Both" Width="95%">
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
