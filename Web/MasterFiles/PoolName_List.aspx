<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoolName_List.aspx.cs" Inherits="MasterFiles_PoolName_List" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pool Area Name List</title>
</head>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
       <style type="text/css">
            .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: gray;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    </style>
       <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
<body>
    <form id="form1" runat="server">
     <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <table width="80%">
            <tr>
                <td style="width: 9.2%" />
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text="Add" OnClick="btnNew_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdPoolName" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found" 
                            OnRowCancelingEdit="grdPoolName_RowCancelingEdit" OnRowEditing="grdPoolName_RowEditing" 
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" >
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pool Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoolCode" runat="server" Text='<%#Eval("Pool_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                       <%-- <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="20" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("subdivision_sname") %>'></asp:TextBox>--%>
                                                <asp:TextBox ID="txtShortName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="10" Text='<%# Bind("Pool_SName") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Pool_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pool Area Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPoolName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="100" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("Pool_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoolName" runat="server" Text='<%# Bind("Pool_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="Deactivate">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Pool_Id") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the sub division');">Deactivate
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
         <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>
    </div>
    </form>
</body>
</html>
