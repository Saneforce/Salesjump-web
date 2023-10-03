<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="rpt_Rout_Names.aspx.cs" Inherits="MasterFiles_rpt_Rout_Names" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retailer Details</title>
    <style type="text/css">
    table.blueTable {
  border: 1px solid #1C6EA4;
  background-color: #EEEEEE;
  width: 100%;
  text-align: left;
  border-collapse: collapse;
}
table.blueTable th
{
    text-align:center;
}
table.blueTable td, table.blueTable th {
  border: 1px solid #AAAAAA;
  padding: 5px 5px;
}

table.blueTable tr:nth-child(even) {
  background: #D0E4F5;
}
table.blueTable thead {
  background: #1C6EA4;
  background: -moz-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  background: -webkit-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  background: linear-gradient(to bottom, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  border-bottom: 2px solid #444444;
}
table.blueTable thead th {
  font-size: 15px;
  font-weight: bold;
  color: #FFFFFF;
  border-left: 2px solid #D0E4F5;
}
table.blueTable thead th:first-child {
  border-left: none;
}

table.blueTable tfoot {
  font-size: 14px;
  font-weight: bold;
  color: #FFFFFF;
  background: #D0E4F5;
  background: -moz-linear-gradient(top, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
  background: -webkit-linear-gradient(top, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
  background: linear-gradient(to bottom, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
  border-top: 2px solid #444444;
}
table.blueTable tfoot td {
  font-size: 14px;
}
table.blueTable tfoot .links {
  text-align: right;
}
table.blueTable tfoot .links a{
  display: inline-block;
  background: #1C6EA4;
  color: #FFFFFF;
  padding: 2px 8px;
  border-radius: 5px;
}

#lblProd
{
    font-size:large;
   
}
    </style>
        
</head>
<body>
    <form id="form1" runat="server">
     <div>
     <br />
    <center>
    <asp:Label ID="lblProd" runat="server" Text="Retailer Details" ></asp:Label>        
    </center>
    <br />
    <asp:Panel ID="Panel1" runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdProduct" runat="server" Width="60%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="blueTable" 
                            AllowSorting="True"  >
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Retailer Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCode" runat="server" Width="120px" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retailer  Name" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblProName" runat="server" Width="150px" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                           
                            </Columns>
                               <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
