<%@ Page Title="TPEdit" Language="C#" AutoEventWireup="true" CodeFile="TPEdit.aspx.cs" MasterPageFile="~/Master.master" Inherits="MasterFiles_Options_TPEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TP Edit</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
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
            $('#<%=btnGo.ClientID%>').click(function () {

                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }
                var FDay = $('#<%=ddlDay.ClientID%> :selected').text();
                if (FDay == "---Select---") { alert("Select Day."); $('#<%=ddlDay.ClientID%>').focus(); return false; }


            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <br />
        <center>
            <table align="center">
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlMonth_SelectedIndex">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="01" Text="January"></asp:ListItem>
                            <asp:ListItem Value="02" Text="February"></asp:ListItem>
                            <asp:ListItem Value="03" Text="March"></asp:ListItem>
                            <asp:ListItem Value="04" Text="April"></asp:ListItem>
                            <asp:ListItem Value="05" Text="May"></asp:ListItem>
                            <asp:ListItem Value="06" Text="June"></asp:ListItem>
                            <asp:ListItem Value="07" Text="July"></asp:ListItem>
                            <asp:ListItem Value="08" Text="August"></asp:ListItem>
                            <asp:ListItem Value="09" Text="September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="December"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label2" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired" Width="90px">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired" Width="50px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Day"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDay" runat="server" SkinID="ddlRequired" AutoPostBack="false"
                            Width="90px">
                            <asp:ListItem Value="-1" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnGo" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" runat="server"
                OnClick="btnGo_Click" />
        </center>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblDate" runat="server" Text="Selective Date's" Font-Underline="True"
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:TextBox ID="txtDate" runat="server" TextMode="MultiLine" Height="100px" Width="600px"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="80px" Height="25px" Text="TP Edit" Visible="false" OnClientClick="return confirm('Do you want to edit the Tour Plan');"
                        OnClick="btnSubmit_Click" />
                </td>
            </tr>
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