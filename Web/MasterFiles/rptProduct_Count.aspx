<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProduct_Count.aspx.cs" Inherits="MasterFiles_rptProduct_Count" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details</title>
      <link type="text/css" rel="stylesheet" href="../css/Report.css" />
        
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <center>
    <asp:Label ID="lblProd" runat="server" Text="Product Details" SkinID="lblMand" ></asp:Label>
    </center>

    <asp:Panel runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdProduct" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" BackColor="#0097AC" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCode" runat="server" Width="120px" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblProName" runat="server" Width="150px" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Description" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left"> 
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleUn" Width="80px" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <%--   <asp:TemplateField HeaderText="Sample Unit" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblsam1" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:Label>--%>
                                        <%-- </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit2">
                                <ItemTemplate>--%>
                                     <%--   <asp:Label ID="lblsam2" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:Label>--%>
                                        <%--</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit3">
                                <ItemTemplate>--%>
                                     <%--   <asp:Label ID="lblsam3" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField  HeaderText="Product Group" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgr" runat="server" Width="130px" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField  HeaderText="Product Category" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblcar" runat="server" Width="130px" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="Product Brand" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <%-- <asp:TemplateField  HeaderText="Product Brand" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="Sub Division" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsd" runat="server" Width="130px" Text='<%# Bind("subdivision_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
