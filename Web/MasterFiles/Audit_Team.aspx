<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Audit_Team.aspx.cs" Inherits="MasterFiles_Audit_Team" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Audit-ID Creation</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
        <br />
        <center>
              <br />
         <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" EmptyDataText="No Records Found" 
                        OnRowCreated="grdSalesForce_RowCreated" 
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr" ></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Sf_UserName" HeaderText="User Name" HeaderStyle-ForeColor="white">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUsrName" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Sf_UserName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtsfName" SkinID="MandTxtBox"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblsfName"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
<%--                            <asp:TemplateField HeaderText="Type">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("SF_Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
--%>                            <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ" HeaderStyle-ForeColor="white">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtHQ" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>                   
                            <asp:HyperLinkField HeaderText="Select Audit Team" Text="Select Audit Team" DataNavigateUrlFormatString="~/MasterFiles/Audit_Team_Selection.aspx?sfcode={0}" DataNavigateUrlFields="SF_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            </asp:HyperLinkField>    
                         </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td> 
            </tr>
        </tbody>

    </table>
          </center>     
             <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
