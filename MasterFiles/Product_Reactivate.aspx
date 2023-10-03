<%@ Page Title="Product Details Reactivation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Product_Reactivate.aspx.cs" Inherits="MasterFiles_Product_Reactivate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Product Details Reactivation</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <%-- <ucl:Menu ID="menu1" runat="server" /> --%>
        <br />
    <table width="100%" align="center">
        <tbody>    
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdProduct" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10"                        
                        onpageindexchanging="grdProduct_PageIndexChanging" EmptyDataText="No Records Found" 
                        onrowcommand="grdProduct_RowCommand"           
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:TextBox ID="txtProName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:TextBox ID="txtProDesc" runat="server" SkinID="TxtBxAllowSymb" MaxLength="250" Text='<%# Bind("Product_Description") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProDesc" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSaleUn"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="3" Text='<%# Bind("Product_Sale_Unit") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="Product_Cat_Name" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Category" ItemStyle-Width="10%"/>
                                <asp:BoundField DataField="Product_Grp_Name" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Group" ItemStyle-Width="10%" />  
                                <asp:BoundField DataField="Product_Brd_Name" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Brand" ItemStyle-Width="10%" />      
                            <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Product_Detail_Code") %>'
                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Product');">Reactivate
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
    </div>
    </form>
</body>
</html>
</asp:Content>