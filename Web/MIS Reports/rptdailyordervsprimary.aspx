<%@  Language="C#"  EnableEventValidation="false"   AutoEventWireup="true" CodeFile="rptdailyordervsprimary.aspx.cs" Inherits="MIS_Reports_rptdailyordervsprimary" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Order Vs Primary</title>


    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
             //window.opener.document.getElementById('form1').click();
             window.close();
         }
         function fixit(selector) {
             selector.each(function () {
                 var values = $(this).find("tr>td:first-of-type")
                 var run = 1
                 for (var i = values.length - 1; i > -1; i--) {
                     if (values.eq(i).text() === values.eq(i - 1).text()) {
                         values.eq(i).remove()
                         run++
                     } else {
                         values.eq(i).attr("rowspan", run)
                         run = 1
                     }
                 }
             })
         }
         fixit($("tbl"))
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
                    <asp:Label ID="lblHead" Text="Daily Order Vs Primary" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
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
   
       
   
     
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            &nbsp;</td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand" 
                                Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand" 
                                Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            &nbsp;</td>
                       
                        <td align="center" >
                            <asp:Label ID="Label1" Text="SalesMan Name  :" runat="server"  ForeColor="#0099CC"
                                Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                            <asp:Label ID="distname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                        </td>
                        <td></td>
                     
                        
                    </tr>
                </table>
           
           <br> </br>
        <div id="norecordfound" runat="server" align="center" 
            style="border: 1px solid #99FFCC; background-color: #CCFFFF;">No Record Found</div>
<asp:Panel ID="pnlContents" runat="server" Width="100%">
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>

                    </tr>
                      </tbody>
            </table> 
</asp:Panel>
    
            <br>  
            </br>
       
        <br></br>
        <br></br>
       
        
       
              
    
    </form>
</body>
</html>

