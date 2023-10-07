<%@ Page Title="Product Category Reactivation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Pro_Cat_React.aspx.cs" Inherits="MasterFiles_Pro_Cat_React" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Product Category Reactivation</title>
<link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdProCat" runat="server" Width="50%" HorizontalAlign="Center" EmptyDataText="No Records Found"  
                            AutoGenerateColumns="false"  OnRowCommand="grdProCat_RowCommand"
                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                            <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product_Cat_Code" Visible="false">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Product_Cat_Code" runat="server" Text='<%#   Bind("Product_Cat_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:BoundField DataField="Doc_Cat_Code" ShowHeader="true" HeaderText="DR Cat Code"  ItemStyle-Width="7%"  />--%>
                                <asp:TemplateField  HeaderText="Category Code" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <%--<ItemStyle Width="10%" />--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct_Cat_SName" runat="server" Text='<%# Bind("Product_Cat_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                   <%-- <ItemStyle Width="20%" />--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProCatName" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="Product_Cat_SName" SortExpression="Product_Cat_SName" ShowHeader="true" HeaderText="Short Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Product_Cat_Name" SortExpression="Product_Cat_Name" ShowHeader="true" HeaderText="Category Name"  ItemStyle-Width="20%"/>--%>
                           <asp:TemplateField HeaderText="Reactivate">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Product_Cat_Code") %>'
                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Product Category');">Reactivate
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
</asp:Content>
