<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_van_stock_ledger.aspx.cs" Inherits="MIS_Reports_rpt_van_stock_ledger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Van Stock Ledger</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    <script type="text/javascript">
        function popUp(PCode, product_name, sfcode, sfname, fdate, tdate) {
            strOpen = "rptvanstockledger.aspx?fieldforceval=" + sfcode + "&Fdate=" + fdate + "&Tdate=" + tdate + "&Feildforce=" + sfname + "&product=" + product_name + "&productval=" + PCode;
            window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server"></asp:Panel>
            <table width="100%">
                <tr>

                    <td width="60%" align="center">
                        <asp:Label ID="lblHead"  Font-Bold="true" Style="color: #3F51B5; padding-right: 80px;" Font-Size="Large" Font-Underline="true"
                            runat="server"></asp:Label>
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"  OnClick="btnPrint_Click" />
                                    <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf" Visible="false" />
                                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click" />
                                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%" Style="margin-left: 35px">
            <table border="0" id='1' style="margin: auto" width="100%">
                <tr>
                    <td>&nbsp;&nbsp;</td>
                </tr>
                <tr align="left">
                    <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus; padding-left: 180px;">
                        <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" Style="font-family: Andalus; color: Blue;"></asp:Label></td>
                </tr>


                <tr>

                </tr>

                <tr>
                    <td width="100%">
                        <asp:GridView ID="gdvan" runat="server"      ShowHeader="true"
                                HorizontalAlign="Center"  Width="100%"  Font-Names="andalus"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"   
                                AutoGenerateColumns="false"  class="newStly"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                            <Columns>
                                <asp:TemplateField HeaderText="Product" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <a href="javascript:popUp('<%#Eval("Product_Detail_Code")%>','<%#Eval("Product_Detail_Name")%>','<%=sf_code%>','<%=sf_name%>','<%=fdate%>','<%=tdate%>')">                                         
                                            <asp:Label ID="lblSNou" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Product_Detail_Name", "{0:N2}")%>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="OB" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblop" runat="server" Font-Size="9pt" Text='<%#Eval("OP")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="In" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblc" runat="server" Font-Size="9pt" Text='<%#Eval("Credit")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Out" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbld" runat="server" Font-Size="9pt" Text='<%#Eval("Debit")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Stock" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcs" runat="server" Font-Size="9pt" Text='<%#Eval("CurrentStock")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
