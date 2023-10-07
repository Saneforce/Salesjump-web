<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="TargetFixationProduct.aspx.cs" Inherits="TargetFixationProduct" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>


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
<script type="text/javascript">
    function Validate() {
        var ddlFieldForce = document.getElementById('<%=ddlFieldForce.ClientID%>');
        var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');

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
    }
</script>
   <%-- <ucl:Menu ID="menu" runat="server" />--%>
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <center>
        <table>
            <tr>
                <td align="center" style="color: #8A2EE6; font-family: Verdana; font-weight: bold;
                    text-transform: capitalize; font-size: 14px; text-align: center;">
                    <asp:Label ID="lblHead" runat="server" Text="Target Fixation Productwise" Font-Underline="True"
                        Font-Bold="True" ></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <table width="85%" >
        <tr>
            <td align="center">
                <table >
                 <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Financial Year" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="2015" Text="2015 - 2016"></asp:ListItem>
                        <asp:ListItem Value="2016" Text="2016 - 2017"></asp:ListItem>
                        <asp:ListItem Value="2017" Text="2017 - 2018"></asp:ListItem>
                        <asp:ListItem Value="2018" Text="2018 - 2019"></asp:ListItem>
                        <asp:ListItem Value="2019" Text="2019 - 2020"></asp:ListItem>
                        <asp:ListItem Value="2020" Text="2020 -2021"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="CmdGo" runat="server" Text="Go" OnClick="CmdGo_Click" OnClientClick="return Validate();" CssClass="BUTTON" />
                </td>
                <td align="left">
                </td>
            </tr>
                </table>
            </td>
        </tr>
           
            <tr>
                <td >
                    <asp:GridView ID="gvTarget" runat="server" AutoGenerateColumns="false" CssClass="mGrid">
                        <HeaderStyle ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Product" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("ProductName") %>' Width="150px">
                                    </asp:Label>
                                    <asp:HiddenField ID="hdnProdCode" runat="server" Value='<%# Eval("ProductCode") %>'>
                                    </asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Pack" HeaderText="Pack" HeaderStyle-ForeColor="White"/>
                            <asp:TemplateField HeaderText="Apr" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth4" runat="server" Width="50px" Text='<%# Eval("Apr") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="May" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth5" runat="server" Width="50px" Text='<%# Eval("May") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jun" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth6" runat="server" Width="50px" Text='<%# Eval("Jun") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jul" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth7" runat="server" Width="50px" Text='<%# Eval("Jul") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Aug" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth8" runat="server" Width="50px" Text='<%# Eval("Aug") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sep" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth9" runat="server" Width="50px" Text='<%# Eval("Sep") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Oct" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth10" runat="server" Width="50px" Text='<%# Eval("Oct") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nov" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth11" runat="server" Width="50px" Text='<%# Eval("Nov") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dec" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth12" runat="server" Width="50px" Text='<%# Eval("Dec") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jan" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth1" runat="server" Width="50px" Text='<%# Eval("Jan") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feb" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth2" runat="server" Width="50px" Text='<%# Eval("Feb") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mar" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonth3" runat="server" Width="50px" Text='<%# Eval("Mar") %>'
                                        Style="text-align: right;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:HiddenField ID="hdnTransSlNo" runat="server" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="BUTTON" Visible="false" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
