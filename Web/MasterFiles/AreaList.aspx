﻿<%@ Page Title="Area List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="AreaList.aspx.cs" Inherits="MasterFiles_AreaList" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Area</title>
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
            z-index: 999;</style>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);


                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)

                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {
                if ($("#txtSubDivision_Sname").val() == "") { alert("Please Enter Area Code."); $('#txtSubDivision_Sname').focus(); return false; }
                if ($("#txtSubDivision_Name").val() == "") { alert("Please Enter Area Name."); $('#txtSubDivision_Name').focus(); return false; }
               
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <table width="80%">
            <tr>
                <td style="width: 9.2%" />
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary btn-md" Text="Add" 
                         OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
      <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Height="40px" Width="40px" OnClick="ExportToExcel"  />
        <br />

        <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSubDiv" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10" OnRowUpdating="grdSubDiv_RowUpdating" OnRowEditing="grdSubDiv_RowEditing"
                            OnRowCommand="grdSubDiv_RowCommand" OnPageIndexChanging="grdSubDiv_PageIndexChanging"
                            OnRowCreated="grdSubDiv_RowCreated" OnRowCancelingEdit="grdSubDiv_RowCancelingEdit"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" OnSorting="grdSubDiv_Sorting" 
                            onrowdatabound="grdSubDiv_RowDataBound">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSubDiv.PageIndex * grdSubDiv.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Division Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsubdivCode" runat="server" Text='<%#Eval("Area_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Area_sname" ItemStyle-Width="130px" HeaderText="Area Code"
                                    HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <%-- <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="20" onkeypress="CharactersOnly(event);"
                                            Text='<%# Bind("subdivision_sname") %>'></asp:TextBox>--%>
                                        <asp:TextBox ID="txtShortName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="5"
                                            Text='<%# Bind("Area_sname") %>' onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvsn" ControlToValidate="txtShortName" runat="server"
                                            ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Area_sname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Area_name" HeaderText="Area Name" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubDivName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="50"
                                            onkeypress="AlphaNumeric_NoSpecialChars(event);" Text='<%# Bind("Area_name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvn" ControlToValidate="txtSubDivName" runat="server"
                                            ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubDivName" runat="server" Text='<%# Bind("Area_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="State" HeaderText="State" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left" Visible="true">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                                        </asp:DropDownList>
									  <asp:RequiredFieldValidator ID="RFValidator1_ddl" InitialValue="0" runat="server" ErrorMessage="RequiredField" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zone Count" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubDiv_count" runat="server" Text='<%# Bind("Sub_Coun") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" ItemStyle-Width="140px"
                                    HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                    DataNavigateUrlFormatString="AreaCreation.aspx?Subdivision_Code={0}" DataNavigateUrlFields="Area_code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="140px">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Area_code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Area');">Deactivate
                                        </asp:LinkButton>
                                        <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="This Area Name Exists in Product or Fieldforce" />
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
         <%--<div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>--%>
    </div>
    </form>
</body>
</html>
</asp:Content>