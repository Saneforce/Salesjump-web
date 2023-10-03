<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="InputDespatchHQView.aspx.cs" Inherits="InputDespatchHQView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
      td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        

        function Validate() {
            var ddlFieldForce = document.getElementById('<%=ddlFieldForce.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
           
            
            var rdoSelected = '';
            if (ddlFieldForce.selectedIndex == 0) {
                alert("Please select Field Force!!");
                ddlFieldForce.focus();
                return false;
            }
            else if (ddlYear.selectedIndex == 0) {
                alert("Please select Year!!");
                ddlYear.focus();
                return false;
            }
            else if (ddlMonth.selectedIndex == 0) {
                alert("Please select Month!!");
                ddlMonth.focus();
                return false;
            }
        }

        
    
    </script>
    <ucl:Menu ID="menu" runat="server" />
   
    <center>
        <table>
            <tr>
                <td align="center" style="color: #8A2EE6; font-family: Verdana; font-weight: bold;
                    text-transform: capitalize; font-size: 14px; text-align: center;">
                    <asp:Label ID="lblHead" runat="server" Text="Input Despatch from HQ View" Font-Underline="True"
                        Font-Bold="True" ></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <table >
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                   <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                        <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                        <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="BUTTON" 
                         onclick="btnGo_Click" OnClientClick="return Validate();" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="gvInputDespatch" runat="server" AutoGenerateColumns="false" 
                        CssClass="mGrid" Width="75%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Input" HeaderStyle-ForeColor="White" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("GiftName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnProdCode" runat="server" Value='<%# Eval("Gift_Code") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Gift_Type" HeaderText="Gift Type" HeaderStyle-ForeColor="White" ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="Despatch_Qty" HeaderText="Pre Despatch Qty" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                            <asp:TemplateField HeaderText="Despatch Qty" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDespatchQty" runat="server" Text='<%# Eval("Despatch_Qty") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="New Despatch Qty" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNewDespatchQty" runat="server" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="BUTTON" 
                        onclick="btnUpdate_Click" Visible="false" />
                    <asp:HiddenField ID="hdnTransNo" runat="server"></asp:HiddenField>
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="BUTTON" 
                        OnClientClick="return confirm('Are you sure to delete records?');" 
                        onclick="btnDelete_Click" Visible="false"/>
                </td>
            </tr>
        </table>
    </center>
   
</asp:Content>
