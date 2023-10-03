<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductView.aspx.cs" Inherits="MasterFiles_ProductView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details - View</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <table width="50%">
            <tr>
                <td style="width: 15%" />
                <td>
                     <asp:Label ID ="lblGiftType" runat ="server" Text ="Search By"></asp:Label>
                <asp:DropDownList ID ="ddlSrch" runat ="server" SkinID ="ddlRequired" AutoPostBack ="true" 
                TabIndex ="1" OnSelectedIndexChanged ="ddlSrch_SelectedIndexChanged">
                <asp:ListItem Text ="All" Value ="1" Selected ="True" ></asp:ListItem>
                <asp:ListItem Text ="Product Name" Value ="2" ></asp:ListItem>
                <asp:ListItem Text ="Product Category" Value ="3"></asp:ListItem>
                <asp:ListItem Text ="Product Group" Value ="4" ></asp:ListItem>
                <asp:ListItem Text ="Product Brand" Value ="5"></asp:ListItem>
                <asp:ListItem Text ="Sub Division" Value ="6"></asp:ListItem>
                <asp:ListItem Text ="State" Value ="7"></asp:ListItem>
                 

                </asp:DropDownList>
                <asp:TextBox ID ="TxtSrch" runat ="server" SkinID ="MandTxtBox" Width ="150px" CssClass ="TEXTAREA" Visible="false" ></asp:TextBox>
                <asp:DropDownList ID ="ddlProCatGrp" runat ="server" AutoPostBack ="false" OnSelectedIndexChanged ="ddlProCatGrp_SelectedIndexChanged" 
                SkinID ="ddlRequired" TabIndex ="4" Visible ="false" ></asp:DropDownList> 
              <asp:Button ID="Btnsrc" runat="server" CssClass="BUTTON" Width="30px" Height="25px"
                        Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                        </td>
            </tr>
        </table>
        <br />
          
        
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdProduct" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" OnSorting="grdProduct_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Product_Detail_Code" HeaderText="Product Code" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCode" runat="server" Width="120px" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Product_Detail_Name" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProName" runat="server" Width="150px" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Description" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleUn" Width="80px" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Base UOM">
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblsam1" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:Label>
                                        <%-- </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit2">
                                <ItemTemplate>
                                        <asp:Label ID="lblsam2" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:Label>
                                        </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Unit3">
                                <ItemTemplate>
                                        <asp:Label ID="lblsam3" runat="server" Width="50px" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField SortExpression="Product_Grp_Name" HeaderText="Product Group" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgr" runat="server" Width="130px" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Product_Cat_Name" HeaderText="Product Category"
                                    HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcar" runat="server" Width="130px" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField SortExpression="Product_Brd_Name" HeaderText="Product Brand"
                                    HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
    </div>
    </form>
</body>
</html>
