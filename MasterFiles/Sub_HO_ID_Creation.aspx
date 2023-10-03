<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sub_HO_ID_Creation.aspx.cs" Inherits="MasterFiles_Sub_HO_ID_Creation" %>
<%@ Register Src ="~/UserControl/pnlMenu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HO ID Creation</title>
      <style type="text/css">
    td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
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
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

</head>
 <script type="text/javascript">
     $(document).ready(function () {
//         $('input:text:first').focus();
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


             if ($("#txtName").val() == "") { alert("Please Enter Name."); $('#txtName').focus(); return false; }
             if ($("#txtUserName").val() == "") { alert("Please Enter User Name."); $('#txtUserName').focus(); return false; }
             if ($("#txtPassword").val() == "") { alert("Please Enter Password."); $('#txtPassword').focus(); return false; }
             if ($('#chkDivision input:checked').length > 0) { return true; } else { alert('Please Select Division'); return false; }

         });
     }); 
    </script>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    
    <br />

    <center>
        <table>
           <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblName" runat="server" Text="Name" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtName" runat="server" TabIndex="1" SkinID="MandTxtBox" Width="250"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:100px;" align="left" class="stylespc">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtUserName" runat="server" TabIndex="2" SkinID="MandTxtBox" Width="170"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblPassword" runat="server" Text="Password" SkinID="lblMand"></asp:Label>
                </td>
               <td align="left" class="stylespc">
                    <asp:TextBox ID="txtPassword" runat="server" TabIndex="3" SkinID="MandTxtBox" MaxLength="15" TextMode="Password" Width="170"></asp:TextBox>
                </td>
            </tr>
    
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblDivision" runat="server" Text="Division Name" SkinID="lblMand"></asp:Label>
                </td>
               <td align="left" class="stylespc">
                    <asp:CheckBoxList ID="chkDivision" TabIndex="5" runat="server" DataTextField="division_name" DataValueField="division_code"
                     Font-Names="Verdana" Font-Size="X-Small" RepeatColumns="3"
                     RepeatDirection="vertical" Width="350px">
                    </asp:CheckBoxList>
                </td>
            </tr>
       
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Width="70px" CssClass="btnnew" TabIndex="7" Height="25px" Text="Submit" 
            onclick="btnSubmit_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnReset" runat="server" TabIndex="8" Width="70px" Height="25px" CssClass="btnnew" Text="Reset" 
            onclick="btnReset_Click" />
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