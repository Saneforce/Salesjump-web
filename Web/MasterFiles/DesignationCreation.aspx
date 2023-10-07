<%@ Page Title="Designation Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DesignationCreation.aspx.cs"
    Inherits="MasterFiles_DesignationCreation" %>

<%@ Register Assembly="obout_ColorPicker" Namespace="OboutInc.ColorPicker" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Designation</title>
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
</head>
<body>
    <form id="form1" runat="server" method="post">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
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
             $('#bac').click(function () {
                 window.location.href = '../MasterFiles/Designation.aspx?&menuid=2&id=8 ';
             });

             $("input:text").on("keypress", function (e) {
                 if (e.which == 32 && !this.value.length)
                     e.preventDefault();
             });


             $('#<%=btnSubmit.ClientID%>').click(function () {
                 if ($('#<%=txtShortName.ClientID%>').val() == "") { alert("Enter Short Name."); $('#<%=txtShortName.ClientID%>').focus(); return false; }
                 if ($('#<%=txtDesignation.ClientID%>').val() == "") { alert("Enter Designation."); $('#<%=txtDesignation.ClientID%>').focus(); return false; }
                 var cat = $('#<%=ddlDesType.ClientID%> :selected').text();
                 if (cat == "--Select--") { alert("Enter Type."); $('#<%=ddlDesType.ClientID%>').focus(); return false; }

                 if ($('#<%=txtColor.ClientID%>').val() == "") { alert("Select Color."); $('#<%=txtColor.ClientID%>').focus(); return false; }
             });
         });

    </script>
     <script type="text/javascript">
         function onBGColorPicked(sender) {
           
             var hiddenField = document.getElementById('<%=bgColor.ClientID %>');
             hiddenField.value = sender.getColor();
             var color = hiddenField.value.replace('#', '');

             document.getElementById('<%=txtColor.ClientID %>').value = color;


         }

        
</script> 
       <input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" <%--onclick="history.back(-2)"--%>  />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblStateDtls" align="center"
             >
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDesName" runat="server" SkinID="lblMand" Width="90px"
                            Height="18px"><span style="color:Red">*</span>Short Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtShortName" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="1" MaxLength="10" onkeypress="CharactersOnly(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDesignation" runat="server" SkinID="lblMand" 
                            Height="19px" ><span style="color:Red">*</span>Designation</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtDesignation" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="50" onkeypress="CharactersOnly(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                 <tr>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblDesType" runat="server" SkinID="lblMand" ><span style="color:Red">*</span>Type</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDesType" runat="server" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" TabIndex="2">
                            <asp:ListItem Selected="True" Text="--Select--" Value=""></asp:ListItem>
                            <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display:none;">
                <td align="left" class="stylespc" >
                        <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand" ><span style="color:Red">*</span>Division Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                           <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                            AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                       <asp:HiddenField runat="server" ID="bgColor" />
                       
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblColor" runat="server" SkinID="lblMand" Height="19px"
                            ><span style="color:Red">*</span>BackColor</asp:Label>
                          
                    </td>
                     <td align="left" class="stylespc">
                       <%-- <asp:TextBox ID="txtColor" SkinID="MandTxtBox" AutoCompleteType="None" runat="server" />
                        &nbsp;<asp:ImageButton ID="Imgpick" ImageUrl="~/Images/color.png" runat="server" />
                    
                        
                        <asp:ColorPickerExtender ID="ColorPickerExtender1" TargetControlID="txtColor" PopupButtonID="Imgpick"
                            PopupPosition="TopRight" SampleControlID="PreviewColor" Enabled="True" runat="server">
                        </asp:ColorPickerExtender>--%>
                 
                          <asp:TextBox ID="txtColor" SkinID="MandTxtBox" onkeydown="return false;"  AutoCompleteType="None" runat="server" />

                           <obout:ColorPicker ID="cpBGColor" runat="server"  
                              PickButton="False" TargetProperty="style.backgroundColor" OnClientPicked="onBGColorPicked">
                           </obout:ColorPicker>
                     

                    </td>
                </tr>
                <tr>
                <td colspan="1"></td>
               <td align="left">
             <%--     <div id="PreviewColor" style="width: 20px; height: 20px; border: 1px solid Gray">
                        </div>--%>
               </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
        
        </center>
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md" Text="Save" 
                 OnClick="btnSubmit_Click" />
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