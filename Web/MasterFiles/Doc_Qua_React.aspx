<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doc_Qua_React.aspx.cs" Inherits="MasterFiles_Doc_Qua_React" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Qualification Reactivation</title>

    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    <br />
     <table align="center" style="width: 100%">
        <tbody>
        <tr>
               <td colspan="2" align="center">
               <asp:GridView ID="grdDocQua" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                     onrowcommand="grdDocQua_RowCommand" EmptyDataText="No Records Found"  
                     onpageindexchanging="grdDocQua_PageIndexChanging" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qualification Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocQuaCode" runat="server" Text='<%#Eval("Doc_QuaCode")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_Qua_SName" runat="server" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Doc_QuaSName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Qua_SName" runat="server" Text='<%# Bind("Doc_QuaSName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Qualification Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocQuaName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="100" Text='<%# Bind("Doc_QuaName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocQuaName" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Reactivate">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Doc_QuaCode") %>'
                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Doctor Qualification');">Reactivate
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
