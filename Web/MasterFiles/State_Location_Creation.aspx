<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="State_Location_Creation.aspx.cs" Inherits="MasterFiles_State_Location_Creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--#include file="../JsFiles/common.js"-->
    <!--#include file="../JsFiles/CommanJScript.js"-->
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table id="tblMandatory" align="center" border="0" cellpadding="0" cellspacing="0" style="width: 80%">
        <tr>
            <td align="right">
                <asp:Label ID="lblMandatory" runat="server" SkinID="lblRequired" Font-Bold="True" Font-Size="X-Small" Text="* Required field"> </asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" id="tblDivisionDtls" align="center">
       <tr>
                <td  align="center">
                    <asp:Label ID="lblStateName" runat="server" SkinID="lblMand" Text="*State Name" Height="19px"></asp:Label>
                    <asp:TextBox ID="txtStateName"  SkinID="MandTxtBox" onfocus="if (fl==0){this.style.backgroundColor='LavenderBlush';}else{this.style.backgroundColor='#E0EE9D';}fl=0;"
                        onblur="this.style.backgroundColor='White'"  TabIndex="1"
                       runat="server" Width="200px" MaxLength="50" CssClass="TEXTAREA"></asp:TextBox>
                    &nbsp; &nbsp;
                    <asp:Label ID="lblShtName" runat="server" SkinID="lblMand" Text="*Short Name" Height="18px"></asp:Label>
                    <asp:TextBox ID="txtShortName"  SkinID="MandTxtBox" onfocus="if (fl==0){this.style.backgroundColor='LavenderBlush';}else{this.style.backgroundColor='#E0EE9D';}fl=0;"
                        onblur="this.style.backgroundColor='White'" TabIndex="2"
                        runat="server"  MaxLength="2"
                        CssClass="TEXTAREA"></asp:TextBox>           
                </td>
            </tr>
    </table> 
    <br />
    <center>
        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text="Save" onClick="btnSubmit_Click" />
    </center>
    <br />
    <asp:GridView ID="grdState" runat="server" Width="85%" SkinID="GV" AllowPaging="True" PageSize="10" 
         onrowupdating="grdState_RowUpdating" onrowediting="grdState_RowEditing" 
         onpageindexchanging="grdState_PageIndexChanging" OnRowCreated="grdState_RowCreated"
         onrowcancelingedit="grdState_RowCancelingEdit"            
         GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
         <SelectedRowStyle BackColor="BurlyWood" />
           <Columns>                
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State_Code" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                <EditItemTemplate> 
                    <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("ShortName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("ShortName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State Name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtStateName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="3" Text='<%# Bind("StateName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                    ShowEditButton="True">
                <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" BorderColor="Black"
                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                </ItemStyle>
            </asp:CommandField>
            <asp:TemplateField HeaderText="Delete">
                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                </ControlStyle>
                <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" BorderColor="Black"
                    Font-Bold="False"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("ShortName") %>'
                        CommandName="Delete" OnClientClick="return confirm('Do you want to delete the State');">Delete
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
          </Columns>
    </asp:GridView>
</asp:Content>

