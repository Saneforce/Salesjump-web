<%@ Page Title="Holiday List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master"
    CodeFile="HolidayList_MR.aspx.cs" Inherits="MasterFiles_MR_HolidayList_MR" %>

<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Holiday List</title>
        <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
            <br />
            <div class="container" style="max-width: 100%; margin: 0px auto; width: 85%;">
                <div class="row form-inline " style="text-align: center">
                    <asp:Label ID="lblYear" runat="server" Text="Year" class="control-label" Style="text-align: right;
                        padding-top: 5px;" Height="20px"></asp:Label>
                    <asp:DropDownList ID="ddlYear" runat="server" class="selectpicker form-control" Width="100"
                        data-style="btn-success">
                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click"
                        Style="width: 120px;"></asp:LinkButton>
                </div>
            </div>
            <table align="center" style="width: 100%">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdHoliday" runat="server" Width="85%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" AllowPaging="True" PageSize="15" EmptyDataText="No Records Found"
                                OnPageIndexChanging="grdHoliday_PageIndexChanging" OnRowCreated="grdHoliday_RowCreated"
                                GridLines="None" CssClass="newStly" OnRowDataBound="grdHoliday_RowDataBound"
                                AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdHoliday_Sorting">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="HSlno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHSlno" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="StateCode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("stateCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField SortExpression="Academic_Year" HeaderText="Academic Year" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Academic_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <%--<asp:TemplateField SortExpression="State_Name" HeaderText="State" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%#Eval("State_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField SortExpression="Holiday_Name" HeaderText="Holiday Name" HeaderStyle-ForeColor="white"
                                        ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtHolidayeName" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("HolidayName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("HolidayName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Holiday_Date" HeaderText="Holiday Date" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-ForeColor="white">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDate" runat="server" onkeypress="Calendar_enter(event);" SkinID="TxtBxAllowSymb"
                                                Text='<%# Bind("HolidayDate") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("HolidayDate") %>'></asp:Label>
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
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
