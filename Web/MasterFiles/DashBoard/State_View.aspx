<%@ Page Language="C#" AutoEventWireup="true" CodeFile="State_View.aspx.cs" Inherits="Customer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Details</title>
    <style type="text/css">
        table
        {
            border-collapse: collapse;
        }
        
        table, td, th
        {
            border: 1px solid;
            border-color: #999999;
            border-bottom: 0.1em solid #bbb;
        }
    </style>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            window.print();
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="right">
        <asp:Panel ID="pnlbutton" runat="server">
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
                                        OnClick="btnPrint_Click" Visible="false" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana"
                                        Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px"
                                        Width="60px" OnClick="btnPDF_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                BorderColor="Black" BorderStyle="Solid" BorderWidth="0" Height="25px" Width="60px"
                OnClientClick="PrintPanel();" />
            <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                Visible="true" OnClick="btnClose_Click" OnClientClick="RefreshParent();" />--%>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div align="center">
            <h1>
                <asp:Label ID="lblHead" Font-Underline="True" Font-Bold="True" runat="server" Font-Size="XX-Large"></asp:Label></h1>
        </div>
        <br />
        <asp:GridView ID="GridView1" runat="server" align="center" AutoGenerateColumns="false"
            HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound"
            EmptyDataText="No Records Found" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:TemplateField HeaderText="S.NO" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="myLabel" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DCR Count" Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Sf_Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DCR Count" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblnum" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <%--<asp:DataList ID="myDataList" runat="server" CellPadding="0" align="center" Font-Size="Medium"
            GridLines="Vertical" RepeatColumns="2" RepeatDirection="Vertical" ShowHeader="true"
            RepeatLayout="Table" OnItemDataBound="myDataList_ItemDataBound" Width="50%">
            <HeaderTemplate>
                <asp:Table ID="Table0" runat="server" GridLines="Both" CellPadding="15">
                    <asp:TableRow ID="trHeader" runat="server">
                        <asp:TableHeaderCell runat="server" ID="a1" Text="S.NO" Font-Size="XX-Large" Width="68px"
                            Height="50px">
           
         
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="a2" runat="server" Text="Field Force Name" Font-Size="XX-Large"
                            Width="950px" Height="50px">
           
         
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="a3" runat="server" Text="DCR Count" Font-Size="XX-Large"
                            Width="75px" Height="50px">
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="b1" runat="server" Text="S.NO" Font-Size="XX-Large" Width="68px"
                            Height="50px">
           
         
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="b2" runat="server" Text="Field Force Name" Font-Size="XX-Large"
                            Width="950px" Height="50px">
           
         
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="b3" runat="server" Text="DCR Count" Font-Size="XX-Large"
                            Width="75px" Height="50px">
                        </asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </HeaderTemplate>
            <ItemTemplate>
                <div id="fontdiv">
                    <asp:Table ID="Table1" runat="server" CellPadding="15" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="myLabel" Width="68px" Height="30px" Font-Size="XX-Large" runat="server"
                                    Text="<%# Container.ItemIndex+1 %>" />
                            </asp:TableCell><asp:TableCell>
                                <asp:Label ID="lbl_name" runat="server" Width="950px" Height="30px" Font-Size="XX-Large"
                                    Text='<%# Eval("Name") %>'></asp:Label>
                            </asp:TableCell><asp:TableCell Visible="false">
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Sf_Code") %>' />
                            </asp:TableCell><asp:TableCell>
                                <asp:Label ID="lblnum" runat="server" Font-Size="XX-Large" Text="" Width="75px" Height="30px"></asp:Label>
                            </asp:TableCell></asp:TableRow>
                    </asp:Table>
              </div>
            </ItemTemplate>
        </asp:DataList> --%>
    </asp:Panel>
    </form>
</body>
</html>
