<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_BulkEdit.aspx.cs"
    Inherits="MasterFiles_MR_Territory_BulkEdit" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory - Bulk Edit</title>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
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
        .marRight
        {
            margin-right: 35px;
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
    <script language="javascript" type="text/javascript">
        function gvValidate() {

            var f = document.getElementById("grdTerritory");
            if (f != null) {
                var TargetChildTerrname = "txtTerritory_Name";



                var Inputs = f.getElementsByTagName("input");
                for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                    if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildTerrname, 0) >= 0) {
                        if (Inputs[i].value == "") {
                            alert("Enter Territory Name");
                            f.getElementsByTagName("input").item(i).focus();
                            return false;
                        }
                    }

                }

            }

        }

         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
            <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <table width="90%">
            <tr>
                <td align="right" width="30%">
                    <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <table align="center" width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdTerritory" runat="server" Width="40%" HorizontalAlign="Center"
                        EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                        CssClass="mGridImg" PagerStyle-CssClass="pgr" OnRowDataBound="grdTerritory_RowDataBound"
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Short Name" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_SName" MaxLength="3" SkinID="TxtBxAllowSymb" runat="server"
                                                    Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event)" Text='<%#Eval("Territory_SName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Route Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Territory_Code" runat="server" Text='<%#Eval("Route_Code")%>'></asp:Label>
                                   
                                    <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Route Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Territory_Name" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                        Width="150px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" Text='<%#Eval("Territory_Name")%>'></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Distributor Name" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Territory_DisName" runat="server" SkinID="ddlRequired">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="Territory_Name3" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                                    Width="150px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" Text='<%#Eval("Territory_Cat")%>'></asp:TextBox>--%>
                                    <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Route Target" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Territory_Target" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                        Width="80px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" Text='<%#Eval("Target")%>'></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Min Prod%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Territory_MinProd" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                        Width="50px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" Text='<%#Eval("Min_Prod")%>'></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Territory_Type" runat="server" SkinID="ddlRequired">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            Width="100%" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Update"
                CssClass="BUTTON" OnClick="btnSubmit_Click" OnClientClick="return gvValidate()" />
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
