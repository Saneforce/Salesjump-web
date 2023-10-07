<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Quiz_Category_List.aspx.cs"
    Inherits="MasterFiles_Options_Quiz_Category_List" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">--%>
<%--<head id="Head1" runat="server">--%>
    <%--<title>Online Quiz - Category List</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .mGrid
{
    width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
    background: white;
}
.mGrid td
{
  /*  padding: 2px;
    border: solid 1px Black;
    border-color: Black;
    border-left: solid 1px Black;
    border-right: solid 1px Black;
    border-top: solid 1px Black;
    font-size: small;
    font-family: Calibri; */
    border: 1px solid #ddd;
    padding: 8px;
}


.mGrid th
{
  /*  padding: 4px 2px;
    color: white;
    background: #39435C;
    border-color: Black;
    border-left: solid 1px Black;
    border-right: solid 1px Black;
    border-top: solid 1px Black;
    border-bottom: solid 1px Black;
    font-weight: normal;
    font-size: small;
    font-family: Calibri; */
    padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #496a9a;
            color: white;
}

.mGrid .pgr
{
    background: #A6A6D2;
}
.mGrid .pgr table
{
    margin: 5px 0;
}
.mGrid .pgr td
{
    border-width: 0;
    padding: 0 6px;
    text-align: left;
    border-left: solid 1px #666;
    font-weight: bold;
    color: White;
    line-height: 12px;
}
.mGrid .pgr th
{
    background: #A6A6D2;
}
.mGrid .pgr a
{
    color: #666;
    text-decoration: none;
}
.mGrid .pgr a:hover
{
    color: #000;
    text-decoration: none;
}
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
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script src="../../JsFiles/CommonValidation.js" type="text/javascript"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
<%--</head>
<body>--%>
    <form id="form1" runat="server">
      <div class="row">
        <div class="col-lg-12 sub-header">
            Quiz Category List 
        </div>
    </div>
    <div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <br />
        <table width="80%">
            <tr>
                <td style="width: 9.2%" />
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Text="Add" Width="60px"
                        Height="25px" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="gvCategoryList" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            OnRowEditing="gvCategoryList_RowEditing" OnRowUpdating="gvCategoryList_RowUpdating"
                            OnRowCancelingEdit="gvCategoryList_RowCancelingEdit" OnRowCreated="gvCategoryList_RowCreated"
                            OnRowCommand="gvCategoryList_RowCommand" ShowHeaderWhenEmpty="true" AllowSorting="True"
                            OnSorting="gvCategory_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (gvCategoryList.PageIndex * gvCategoryList.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryId" runat="server" Text='<%#Eval("Category_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Category_ShortName" ItemStyle-Width="130px" HeaderText="Short Name"
                                    HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <%-- <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="20" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("subdivision_sname") %>'></asp:TextBox>--%>
                                        <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="10"
                                            Text='<%# Bind("Category_ShortName") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Category_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Category_Name" HeaderText="Category Name" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCategoryName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="100"
                                            onkeypress="CharactersOnly(event);" Text='<%# Bind("Category_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" ItemStyle-Width="140px"
                                    HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                    DataNavigateUrlFormatString="Quiz_Category_Creation.aspx?Category_Id={0}" DataNavigateUrlFields="Category_Id">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Category_Id") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the  Category');">Deactivate
                                        </asp:LinkButton>
                                        <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../../Images/deact1.png" alt="" width="55px" title="This Category Exists in Category" />
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" />
        </div>
    </div>
    </form>
<%--</body>
</html>--%>
</asp:Content>