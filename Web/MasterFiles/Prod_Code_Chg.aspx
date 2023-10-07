<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prod_Code_Chg.aspx.cs" Inherits="MasterFiles_Prod_Code_Chg" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Code Change</title>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdProduct" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                            CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Existing Code" HeaderStyle-ForeColor="white"   
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCode" runat="server" Width="120px" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Code" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProd" MaxLength="20" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProName" runat="server" Width="150px" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Group" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgr" runat="server" Width="130px" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Category" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcar" runat="server" Width="130px" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Brand" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrd" runat="server" Width="130px" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <center>
            <asp:Button ID="btnSave" runat="server" Text="Update" Width="90px" Height="25px" BackColor="LightBlue" 
                onclick="btnSave_Click" />
        </center>
    </div>
    </form>
</body>
</html>
