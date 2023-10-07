<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="ChangePassword.aspx.cs" Inherits="MasterFiles_Options_ChangePassword" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl1" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Password Maintenance</title>
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
              $('#btnGo').click(function () {
              
             
                  var Name1 = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                  if (Name == "") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                  if ($("#txtOldPwd").val() == "") { alert("Enter Old Password."); $('#txtOldPwd').focus(); return false; }
                  if ($("#txtNewPwd").val() == "") { alert("Enter New Password."); $('#txtNewPwd').focus(); return false; }
                  if ($("#txtConfirmPwd").val() == "") { alert("Enter Confirm Password."); $('#txtConfirmPwd').focus(); return false; }
              });
          }); 
    </script>
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
</head>
  
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
         <div id="Divid" runat="server"></div>
    <br />  
        <center>
        <table align="center" cellpadding="3" cellspacing="3">
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                 
                </td>
             <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                </td>
            </tr>

            <tr style="height:25px;">
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" runat="server" Text="Old Password" SkinID="lblMand"></asp:Label>                                        
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtOldPwd" runat="server" SkinID="MandTxtBox" MaxLength="15" 
                        TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr style="height:25px;" class="stylespc">
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Text="New Password" SkinID="lblMand"></asp:Label>                                        
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtNewPwd" runat="server" SkinID="MandTxtBox" MaxLength="15" 
                         TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr style="height:25px;" class="stylespc">
                <td align="left">
                    <asp:Label ID="Label3" runat="server" Text="Confirm Password" SkinID="lblMand"></asp:Label>                                        
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtConfirmPwd" runat="server" SkinID="MandTxtBox" 
                        MaxLength="15"  TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            </table>
            </center>
            <br />
            <center>
            <table align="center">
            <tr>
                <td>
                    <asp:Button ID="btnGo" runat="server" Width="70px" Height="25px" Text="Save" CssClass="BUTTON" onclick="btnGo_Click" />
                    &nbsp;
                    <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text="Clear" 
                        onclick="btnClear_Click" />
                </td>
            </tr>
        </table>
        </center> 
        <br />
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