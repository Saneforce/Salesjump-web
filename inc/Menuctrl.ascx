<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menuctrl.ascx.cs" Inherits="Menuctrl" %>

<link href='<%=ResolveClientUrl("../css/Menu3.css")%>'  rel="stylesheet" type="text/css" />

<div id="header" align="center" >    
    <table width="100%" cellpadding ="0" cellspacing ="0" align="center">
        <tr>
            <td  align="center">
                <asp:Menu ID="Menu1" runat="server" Orientation ="Horizontal" CssClass ="menu" Font-Names="Verdana"  Width="800px">
                    <StaticMenuItemStyle CssClass="menu" Height="40px" HorizontalPadding ="5px" />
                    <DynamicMenuItemStyle CssClass ="menu" Height="40px" HorizontalPadding="25px"  />
                    <dynamichoverstyle  CssClass ="menuhover" />
                    <StaticHoverStyle CssClass ="menuhover" />
                    <Items>
                        <asp:MenuItem Text="Master">
                            <asp:MenuItem Text ="Division" Enabled ="true"  NavigateUrl="~/MasterFiles/DivisionList.aspx" ></asp:MenuItem>
                            <asp:MenuItem Text="State/Location" Enabled ="true" NavigateUrl ="~/MasterFiles/State_Location_Creation.aspx"></asp:MenuItem>
                        </asp:MenuItem>
                
                        <asp:MenuItem Text="Reports" >
                            <asp:MenuItem Text="User List" Enabled ="true"></asp:MenuItem>
                            <asp:MenuItem Text="Listed Doctor Category/SPeciality" Enabled ="true" ></asp:MenuItem>   
                        </asp:MenuItem>
    
                        <asp:MenuItem Text="Option">
                            <asp:MenuItem Text="Mailbox"></asp:MenuItem>
                        </asp:MenuItem>
   
                        <asp:MenuItem Text="Change Division">
                        </asp:MenuItem>
   
                    </Items>
            </asp:Menu>
        </td>
</tr>
</table>
</div>