<%@ Page Title="DSM Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DSMCreation.aspx.cs" Inherits="MasterFiles_DSMCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>DSM Master</title>
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

                    if ($('input:text')[curIndex].value == '') {
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
            $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=txtSubDivision_Sname.ClientID%>').val() == "") { alert("Please Enter DSM Code."); $('#<%=txtSubDivision_Sname.ClientID%>').focus(); return false; }
                if ($('#<%=txtSubDivision_Name.ClientID%>').val() == "") { alert("Please Enter DSM Name."); $('#<%=txtSubDivision_Name.ClientID%>').focus(); return false; }
                var SName = $('#<%=salesforcelist.ClientID%> :selected').text();
                if (SName == "--Select--") { alert("Please Select FieldForce Name."); $('#<%=salesforcelist.ClientID%>').focus(); return false; }
                var SName = $('#<%=ddlState.ClientID%> :selected').text();
                if (SName == "---ALL---") { alert("Please Select Distributor."); $('#<%=ddlState.ClientID%>').focus(); return false; }
                var SName = $('#<%=ddlTown_Name.ClientID%> :selected').text();
                if (SName == "--Select--") { alert("Please Select Town."); $('#<%=ddlTown_Name.ClientID%>').focus(); return false; }
                if ($('#<%=txtUserName.ClientID%>').val() == "") { alert("Enter UserName."); $('#<%=txtUserName.ClientID%>').focus(); return false; }
                if ($('#<%=txtPassword.ClientID%>').val() == "") { alert("Enter Password."); $('#<%=txtPassword.ClientID%>').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:TextBox ID="fakeusernameremembered1" runat="server" style="display:none">
                        </asp:TextBox>
						<asp:TextBox ID="fakepasswordremembered1" runat="server" style="width: 0px;height: 0px;position: absolute;top: -35px;" TextMode="Password">
                        </asp:TextBox>
    <div>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDocCatDtls">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px" Width="120px"><span style="Color:Red">*</span>DSM Code</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSubDivision_Sname" TabIndex="1" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server"  onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivisionName" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>DSM Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSubDivision_Name" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="50" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
				 <tr>
                 <td> 
                 <asp:Label ID="Label2" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Select Sales Executive Officer</asp:Label></td><td align="left">
                   <asp:DropDownList ID="salesforcelist" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        CssClass="ddl" Width="210px" 
                             onselectedindexchanged="salesforcelist_SelectedIndexChanged">
                   </asp:DropDownList>
               </td>
               </tr> 
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Lbl_Area" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Distributor Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired" TabIndex="3">
                        </asp:DropDownList>
                    </td>
                      <td align="left">
                        <asp:Label ID="lbl_Town_Name" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Town Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTown_Name" runat="server" SkinID="ddlRequired" TabIndex="4">
                            <asp:ListItem Text="--Select--" Selected="True" Value="-1">           
                            </asp:ListItem>
                           
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="stylespc" align="left">
                        <asp:Label ID="lblUserName" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>User Name</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txtUserName" runat="server" SkinID="MandTxtBox" Width="144px" MaxLength="100"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            TabIndex="5" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            Height="16px"></asp:TextBox>
                    </td>
                   
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblPassword" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Password</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txtPassword" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"
                            TextMode="Password" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="163px" TabIndex="6" CssClass="TEXTAREA" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
                Text="Save" OnClick="btnSubmit_Click" />
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
</asp:Content>