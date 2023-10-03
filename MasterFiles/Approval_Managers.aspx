<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval_Managers.aspx.cs" Inherits="MasterFiles_Approval_Managers" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval Managers</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
        <br />
         <%-- <table width="50%">  
         <tr>
         <td style="width:15%" />
               <td colspan="2" align="center">  
               <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="70%" HorizontalAlign="left">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAlpha" runat="server" CommandArgument = '<%#bind("sf_name") %>' text = '<%#bind("sf_name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>   
            </table> --%>
        <table width="83%" align="center" border="0">
            <tr>
            <td style="width:15%" />
               <td colspan="2" align="center">  
               <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="160%" HorizontalAlign="left">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAlpha" runat="server" CommandArgument = '<%#bind("sf_name") %>' text = '<%#bind("sf_name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td colspan="2" align="right">
                    <asp:Label ID="lblFilter" runat="server" Text="Filter By"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlFilter" runat="server" Width="200"
                        onselectedindexchanged="ddlFilter_SelectedIndexChanged"></asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" onclick="btnGo_Click" />
                </td>
            </tr>
              
        </table>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      
    <center> 
    <table runat="server" id="tblSalesForce" >
            <tr>
                <td>
                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="10" 
                        onpageindexchanging="grdSalesForce_PageIndexChanging" onrowdatabound="grdSalesForce_RowDataBound"
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle Wrap="True" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            
                              <asp:TemplateField HeaderText="SF_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Bind("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="FieldForce" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                     <asp:Label ID="lblSf_Name" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Sf_Name") %>'></asp:Label> 
                            
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="DCR Approval" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlReporting_To" runat="server" Width="150px" DataSource ="<%#  Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="Sf_Code" SkinID="ddlRequired" Font-Size="XX-Large">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TP Approval" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Sf_Code1" runat="server" Width="150px" DataSource ="<%# Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="Sf_Code" SkinID="ddlRequired">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Listed Dr Add/Delete Approval" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Sf_Code2" runat="server" Width="180px" DataSource ="<%# Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="Sf_Code" SkinID="ddlRequired">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Approval" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Sf_Code3" runat="server" Width="150px" DataSource ="<%# Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="Sf_Code" SkinID="ddlRequired">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Secondary Sales Approval" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Sf_Code4" runat="server" Width="150px" DataSource ="<%# Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="Sf_Code" SkinID="ddlRequired">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other Approval" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Sf_Code5" runat="server" Width="150px" DataSource ="<%# Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="Sf_Code" SkinID="ddlRequired">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnApproval" CssClass="BUTTON" runat="server" Text="Approval Managers" 
                        onclick="btnApproval_Click" />
                </td>
            </tr>
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
