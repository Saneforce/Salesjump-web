<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="DoctorBusinessStatus.aspx.cs" EnableViewState="true" Inherits="DoctorBusinessStatus" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
       

        function SubmitForm() {

            var ddlFieldForce = document.getElementById('<%=ddlFieldForce.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
            
            if (ddlFieldForce.selectedIndex == 0) {
                alert("Please select FieldForce!!");
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
                <td align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Customer Business Status" Font-Underline="True"
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <table>
            <tr>
                <td align="left">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblYear" runat="server" Text="Year " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left">
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
                <td align="left">
                    <asp:Label ID="lblMonth" runat="server" Text="Month " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left">
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
                <td colspan="4" align="center">
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="BUTTON" OnClientClick="return SubmitForm()" 
                        onclick="btnGo_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvDoctorBusiness" runat="server" AutoGenerateColumns="false" OnRowCommand="gvDoctorBusiness_RowCommand"
                         OnRowEditing="gvDoctorBusiness_RowEditing"
                        CssClass="mGrid" onrowdatabound="gvDoctorBusiness_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblFieldForce" runat="server" Text='<%# Eval("FieldForceName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Eval("HQ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkActivate" runat="server" Text="Activate" CommandName="Edit"></asp:LinkButton>
                                    <asp:HiddenField ID="hdnTransNo" runat="server" Value='<%# Eval("Trans_sl_No") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
