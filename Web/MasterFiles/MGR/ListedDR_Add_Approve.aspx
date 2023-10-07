<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDR_Add_Approve.aspx.cs" Inherits="MasterFiles_MGR_ListedDR_Add_Approve" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer Addition - Approval</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
        <br />
        <center>
       <table width="75%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdListedDR" runat="server" Width="65%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Data found for Approval's"   
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Name" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Approve">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutApprove" runat="server" CommandArgument='<%# Eval("Sf_Code") %>'
                                        CommandName="Approve" OnClientClick="return confirm('Do you want to Approve');">Approve
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                               <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="ListedDR_Add_Approval.aspx?sfcode={0}"
                               DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>   
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
    </center>
     
    </div>
    </form>
</body>
</html>
