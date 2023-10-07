<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FVExpense_Parameter.aspx.cs"
    Inherits="MasterFiles_FVExpense_Parameter" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fixed/Variable Expense</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
        
       
        <table width="80%">
            <tr>
                <td style="width: 19.2%" />
               
                <td align="left" width="22%">
                    <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
                        Text="Add" OnClick="btnNew_Click" />
                </td>
                <td align="left" style="margin-left: 75%">
                    <asp:Label ID="lblLevel" runat="server" SkinID="lblMand" Text="Level" Height="19px"
                        ></asp:Label>
               
                    <asp:DropDownList ID="ddlLevel" runat="server" Width="140px" AutoPostBack="true"
                        SkinID="ddlRequired" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Line Managers" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td height="15px"></td></tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Label ID="lblSelect" Text="Select the Designation" ForeColor="Red" Font-Size="Large"
                        runat="server">
                    </asp:Label>
                </td>
            </tr>
        </table>
        </center>
        
        <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table align="center" style="width: 100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdFVeExpParameter" runat="server" Width="50%" HorizontalAlign="Center"
                        OnRowUpdating="grdFVeExpParameter_RowUpdating" OnRowEditing="grdFVeExpParameter_RowEditing"
                        OnRowDeleting="grdFVeExpParameter_RowDeleting" EmptyDataText="No Records Found"
                        OnRowCreated="grdFVeExpParameter_RowCreated" OnRowDataBound="grdFVeExpParameter_RowDataBound"
                        OnRowCancelingEdit="grdFVeExpParameter_RowCancelingEdit" AutoGenerateColumns="false"
                        GridLines="None" CssClass="mGrid">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parameter Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpParameter_Code" runat="server" Text='<%#Eval("Expense_Parameter_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parameter Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="140px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtExpParameter_Name" runat="server" SkinID="TxtBxAllowSymb" MaxLength="100"
                                         Text='<%# Bind("Expense_Parameter_Name") %>'></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpParameter_Name"
                                    ErrorMessage="Enter Parameter Name."></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblExpParameter_Name" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Expense Type" HeaderStyle-ForeColor="white"
                                HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseType" runat="server" Text='<%# Bind("Param_type") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlExpenseType" runat="server" SkinID="ddlRequired">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                        <asp:ListItem Value="F" Text="Fixed"></asp:ListItem>
                                        <asp:ListItem Value="V" Text="Variable"></asp:ListItem>
                                        <asp:ListItem Text="Variable with Limit" Value="L"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="Expense Type" HeaderStyle-ForeColor="white"
                                HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseType" Visible="false" runat="server" Text='<%# Bind("Fixed_Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLimit" Visible="false" Text='<%# Bind("Fixed_Amount") %>' runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPrice" ControlToValidate="txtLimit" runat="server" 
                                     ErrorMessage="Enter Fixed amount." ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white"
                                HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"
                                HeaderStyle-Width="80px">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ItemStyle>
                            </asp:CommandField>
                            <%-- <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-ForeColor="white" DataNavigateUrlFormatString="DesignationCreation.aspx?Designation_Code={0}"
                                DataNavigateUrlFields="Designation_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>--%>
                            <asp:TemplateField HeaderText="Delete" Visible="false" HeaderStyle-ForeColor="white" HeaderStyle-Width="50px">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Expense_Parameter_Code") %>'
                                        CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Designation');">Delete
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
         </ContentTemplate>
        </asp:UpdatePanel>
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
