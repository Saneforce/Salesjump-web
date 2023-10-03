<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_stk_wise.aspx.cs" Inherits="MIS_Reports_rpt_stk_wise" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Stock View</title>
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" /> 
         <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }

         $(document).ready(function () {
             var div_code = '<%=div_code%>';

//             $("#btngo").on("click", function () {
//                 var sfcode = $("#ddlff").val();
//                 month = $("#ddlmnth").val();
//                 if (month == "0") {
//                     alert("Select the Month");
//                     return false;
//                 }
//                 year = $("#ddlYR").val();
//                 strOpen = "rpt_stk_wise.aspx?sf_code=" + sfcode + "&div_code=" + div_code + "&Year=" + year + "&SfR=" + sf + "&Mnth=" + month;
//                 window.open(strOpen, '_self', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

//             });
             //             $("#ddlmnth").change(function () {
             //                 var sfcode = $("#ddlff").val();
             //                 strOpen = "rpt_stk_wise.aspx?sf_code=" + sfcode + "&div_code=" + div_code + "&Year=" + year + "&SfR=" + sf + "&Mnth=" + month;
             //                 window.open(strOpen, '_self', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

             //             });
             //             $("#ddlYR").change(function () {
             //                 var sfcode = $("#ddlff").val();
             //                 strOpen = "rpt_stk_wise.aspx?sf_code=" + sfcode + "&div_code=" + div_code + "&Year=" + year + "&SfR=" + sf + "&Mnth=" + month;
             //                 window.open(strOpen, '_self', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

             //             });
             $(document).on('click', '#btnExcel', function (e) {
                 var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divexcel').html());
                 var a = document.createElement('a');
                 a.href = data_type;
                 a.download = 'ClosingStockdetails xls';
                 a.click();
                 e.preventDefault();

             });

         });
            /* $(document).ready(function () {
                 $('#tbl').each(function () {
                     // tds1 = $(this).find("td");
                     //var tbl = $('#tbl');
                     //  var tds = parseFloat($(this).find("td"));

                     tds = $(this).find("td");

                     var add, i, total = 0;
                     // var tbl1 = $(tbl).find('tbody td').val();
                     
                      //   for (i = 4; i <10; i++) {
                             // for (j = 4; j < parseFloat($(tds[j]).text()); i++) {
                              // if (($(tds.length)) > 0) {
                             add += parseInt(($(tds[i])).val());
                             if (!isNaN(add)) {
                                 total += add;
                             }
                         //  }
                         //}
                         
                     $(tbl).find('tfooter').append('<tr style="color:blue;background-color: #99FFFF;"> <td  colspan="2">Total</td> <td>' + total + ' </td> </tr > ');
                     
                     i++;              
                     });
                 
                 });
                    // $('table tfoot td').each(function (index) {
                       //  var total = 0;
                       //  $('tbody tr').each(function () {
                           //  total += +$('td', this).eq(index).text(); //+ will convert string to number
                         //});
                        // $(this).text(total);
                     //});
            */

           
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
                <td width="30%"></td>
                    <td width="70%" align="Center" >
<br></br>

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
                                       />
                                </td>
                                  <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                         OnClientClick="javascript:window.open('','_self').close();"/>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
               </asp:Panel>
                 
          <div id="divexcel">
              <asp:Panel ID="pnlContents" runat="server" Width="100%">
<table>
<tr><td></td><td></td><td></td><td align="right">
     
                    <asp:Label ID="lblHead"  SkinID="lblMand" Font-Bold="true" width="290px"  Font-Underline="true"
                runat="server"></asp:Label></td></tr></table>
       

       
   
        
           <table width="80%" align="center">
                <tbody>
                    <tr><td>
                        <table><tr><td><asp:Label id="lbl" runat="server" Text="Closing Stock View" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                <td align="left" style="padding-left:30px"><asp:DropDownList ID="ddlff" runat="server" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'"  SkinID="ddlRequired" TabIndex="6"
                                    Visible="true">
                                    <%--  <asp:ListItem Selected="true" Value="">---Select Mgr---</asp:ListItem> --%>
                                </asp:DropDownList></td>
                            <td align="left" style="padding-left:30px"><asp:DropDownList ID="ddlmnth" runat="server" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'"  SkinID="ddlRequired" TabIndex="6">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem></asp:DropDownList></td>
                            <td align="left" style="padding-left:30px"><asp:DropDownList ID="ddlYR" runat="server" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'"  SkinID="ddlRequired" TabIndex="6"></asp:DropDownList></td>
                                    <td align="left" style="padding-left:30px">
                                    <asp:Button ID="btngo" runat="server" Text="Go" Font-Names="Verdana" Font-Size="10px" OnClick="btngoclick"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px "/></td>
                               </tr></table>
                    </td></tr>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="78%">
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td>
                        &nbsp;</td></tr>
                </tbody>
            </table>    
          
     
           </asp:Panel>
             </div>
                 </div>
    </form>
</body>
</html>
