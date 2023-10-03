<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateLocationCreation.aspx.cs" Inherits="MasterFiles_StateLocationCreation" %>
<%@ Register Src ="~/UserControl/pnlMenu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State-Location</title>
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
       td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    
</style>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
   <script type="text/javascript">
       $(document).ready(function () {
           $('input:text:first').focus();
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

           $('#btnSubmit').click(function () {
               if ($("#txtShortName").val() == "") { alert("Enter Short Name."); $('#txtShortName').focus(); return false; }
               if ($("#txtStateName").val() == "") { alert("Enter State Name."); $('#txtStateName').focus(); return false; }

           });
       });

    </script>
   <script language="javascript" type="text/javascript">

       function ToUpper(ctrl) {

           var t = ctrl.value;

           ctrl.value = t.toUpperCase();

       }

       function ToLower(ctrl) {

           var t = ctrl.value;

           ctrl.value = t.toLowerCase();

       }

    </script>
    <script type="text/JavaScript">
        function valid(f) {
            !(/^[A-z&#209;&#241;0-9]*$/i).test(f.value) ? f.value = f.value.replace(/[^A-z&#209;&#241;0-9]/ig, '') : null;
        } 
</script>

    </head>
<body>
    <form id="form1" runat="server" method="post">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    
    <br />
     <center>
    <table border="0" cellpadding="3" cellspacing="3" id="tblStateDtls" align="center"
             >
       <tr>
                <td class="stylespc">     
                    <asp:Label ID="lblShtName" runat="server" SkinID="lblMand" Width="90px"><span style="color:Red">*</span>Short Name</asp:Label>
                    </td>
                   <td align="left" class="stylespc">     
                    <asp:TextBox ID="txtShortName"  runat="server" SkinID="MandTxtBox" 
                          onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'; valid(this)" 
                          TabIndex="1" MaxLength="2"  onkeyup="ToUpper(this); valid(this)"  onkeypress="CharactersOnly(event);">
                    </asp:TextBox>           
                </td>
            </tr>
            <tr>
             <td  class="stylespc">     
                    <asp:Label ID="lblStateName" runat="server" SkinID="lblMand" ><span style="color:Red">*</span>State Name</asp:Label>
                    </td>
                  <td align="left" class="stylespc">     
                    <asp:TextBox ID="txtStateName"  SkinID="MandTxtBox" 
                         onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'; valid(this)" 
                         TabIndex="2" runat="server" Width="200px" MaxLength="120"  onkeypress="CharactersOnly(event);">
                    </asp:TextBox>
                </td>
            </tr>
    </table> 
        </center>   
    <br />
    <center>
        <asp:Button ID="btnSubmit" runat="server" CssClass="btnnew" Width="60px" Height="25px" Text="Save" onClick="btnSubmit_Click" />
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
