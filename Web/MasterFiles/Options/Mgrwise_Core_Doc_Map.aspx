<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mgrwise_Core_Doc_Map.aspx.cs" Inherits="MasterFiles_Options_Mgrwise_Core_Doc_Map" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Managerwise-Core Customer Map</title>
    <link type="text/css" rel="stylesheet" href="../../css/Style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br /> 
        <center>
            <table width="90%" border="1" bgcolor="#A6A6D2" style="border-style: solid; border-width: thin">
                <tr>
                    <td align="left" width="40%">
                        <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text="Save" onclick="btnSave_Click"/>
                        &nbsp;
                        <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text = "Clear" />
                    </td>
                    <td align="center">
                        <asp:Label ID="lblHead" runat="server" SkinID="lblMand" Text="Core Customer Map" ForeColor="White" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblMR" runat="server" Text="MR : " SkinID="lblMand" ForeColor="White"></asp:Label>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" SkinID="ddlRequired" 
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlMR" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            onselectedindexchanged="ddlMR_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table width="90%">
                <tr>
                    <td>                        
                        <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Records Found"   OnRowDataBound="gvDetails_RowDataBound"
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doc Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doctor" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Specialty" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecName" runat="server" Text='<%#Eval("Doc_Special_SName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("Doc_Cat_SName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Territory" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerrName" runat="server" Text='<%#Eval("territory_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Level1" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Level2" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel2" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Level3" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel3" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Level4" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel4" runat="server" Checked="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        </asp:GridView>                    
                    </td>
                </tr>
            </table>
        </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
