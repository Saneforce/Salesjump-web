<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_Monthly_Order_Count_Details.aspx.cs" Inherits="MIS_Reports_View_Monthly_Order_Count_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary OrderWise Count Details</title>
        <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
        <script type="text/javascript">
            function RefreshParent() {
                // window.opener.document.getElementById('form1').click();
                window.close();
            }
            function generate() {

                var doc = new jsPDF('l', 'pt');

                var res = doc.autoTableHtmlToJson(document.getElementById("Order_Count_Details"));



                var header = function (data) {
                    doc.setFontSize(10);
                    doc.setTextColor(40);
                    doc.setFontStyle('normal');
                    //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                    doc.text("View_Monthly_Order_Count_Details.aspx", data.settings.margin.top, 70);
                };
                var options = {
                    beforePageContent: header,
                    margin: {
                        top: 80
                    },
                    startY: doc.autoTableEndPosY() + 20
                };
                doc.autoTable(res.columns, res.data, options);




                doc.save("RetailerOfftake.pdf");
            }</script>
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        td {
            padding: 2px 5px;
        }

        .subTot {
            Font-size: 11pt;
            font-weight: bold;
        }

        .GrndTot {
            Font-size: 13pt;
            font-weight: bold;
        }

        .remove {
            text-decoration: none;
        }

        .TopButton {
            border-color: Black;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
            height: 25px;
            width: 60px;
        }

        #Order_Count_Details {
            text-align: center;
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
                    <asp:Label ID="lblHead" Text="Retailer Offtake Report"  Font-Names="Andalus" Font-Bold="true"  Font-Underline="true"              
                       runat="server" Font-Size="Large"></asp:Label>
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

<td><asp:Button ID="btnExport" runat="server" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Text="Excel" OnClick="btnExport_Click" /></td>
                               
                                </td>
                                <td> 
 <input id="pdfexport" class="TopButton" type="button" value="PDF" height="35px"  onclick="generate()"" >   


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

        
              <asp:Panel id="pnlContents"  runat="server" Width="100%">
                <table border="0" id='1'   width="100%">                 
                                                   
          <tr align="right"><td align="left" style="font-size: small; PADDING: 0PX 52PX; font-weight: bold;font-family: Andalus;">FieldForce Name:
                   <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                     <tr> 
                        <td width="100%" style="TEXT-ALIGN: -webkit-center;">

                           <asp:GridView ID="Order_Count_Details" runat="server" Width="95%"   ShowHeader="false"
                                HorizontalAlign="Center" 
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                                AutoGenerateColumns="true" CssClass="newStly" Font-Size="Small" OnRowCreated="Order_Count_Details_RowCreated"
                                HeaderStyle-HorizontalAlign="Center" >                               
                                <Columns>
                   <%--                <asp:TemplateField HeaderText="Sl No">
                                       <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text="<%#Container.DataItemIndex + 1%>"></asp:Label>
                                           </ItemTemplate>
                                   </asp:TemplateField >
                                      <asp:TemplateField HeaderText="Sl No">
                                          <ItemTemplate>
                                               
                                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                              </ItemTemplate>
                                   </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Stockist Name">
                                          <ItemTemplate>
                                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                               </ItemTemplate>
                                   </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Order Date">
                                          <ItemTemplate>
                                       <asp:Label ID="Label4" runat="server" Text='<%# Bind("Order_Date") %>'></asp:Label>
                                               </ItemTemplate>
                                   </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Order Value">
                                          <ItemTemplate>
                                       <asp:Label ID="Label5" runat="server" Text='<%# Bind("Order_Value") %>'></asp:Label>
                                               </ItemTemplate>
                                   </asp:TemplateField>--%>
                               </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                  
    </asp:Panel>
    </form>
</body>
</html>
