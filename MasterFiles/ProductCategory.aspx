<%@ Page Title="Product-Category Creation" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="ProductCategory.aspx.cs"
    Inherits="MasterFiles_ProductCategory" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <html xmlns="http://www.w3.org/1999/xhtml">
         <head>
             <title>Product-Category</title>
             <style type="text/css">
                 .modal {
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

                 .loading {
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
             <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
             <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
             <link type="text/css" rel="stylesheet" href="../css/style.css" />
             <script type="text/javascript">
                 $(document).ready(function () {
                     //  $('input:text:first').focus();
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
                         if ($('#<%=txtProduct_Cat_SName.ClientID%>').val() == "") { alert("Enter Category Code."); $('#<%=txtProduct_Cat_SName.ClientID%>').focus(); return false; }
                         if ($('#<%=txtProCatName.ClientID%>').val() == "") { alert("Enter Category Name."); $('#<%=txtProCatName.ClientID%>').focus(); return false; }
                         var SName = $('#<%=ddldiv.ClientID%> :selected').text();
                         if (SName == "--Select--") { alert("Please Select Division."); $('#ddldiv').focus(); return false; }
                     });

                 });
             </script>
         </head>
         <body>
             <form id="form1" runat="server">
                 <div class="card">
                     <%--  <ucl:Menu ID="menu1" runat="server" />--%>
                     <br />
                     <center>
                         <table border="0" cellpadding="3" cellspacing="3" id="tblProCatDtls" align="center">
                             <tr>
                                 <td align="left" class="stylespc">
                                     <asp:Label ID="lblProduct_Cat_SName" runat="server" SkinID="lblMand" Height="19px"
                                         Width="100px"><span style="color:Red">*</span>Category Code</asp:Label>
                                 </td>
                                 <td align="left" class="stylespc">
                                     <asp:TextBox ID="txtProduct_Cat_SName" SkinID="MandTxtBox" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                                         onblur="this.style.backgroundColor='White'" runat="server" MaxLength="12" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                     </asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="left" class="stylespc">
                                     <asp:Label ID="lblProCatName" runat="server" SkinID="lblMand" Height="18px" Width="100px"><span style="color:Red">*</span>Category Name</asp:Label>
                                 </td>
                                 <td align="left" class="stylespc">
                                     <asp:TextBox ID="txtProCatName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                         onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                                         MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                     </asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="left" class="stylespc">
                                     <asp:Label ID="Lbl_div" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Division</asp:Label>
                                 </td>
                                 <td align="left" class="stylespc">
                                     <asp:DropDownList ID="ddldiv" runat="server" SkinID="ddlRequired" TabIndex="3">

                                     </asp:DropDownList>
                                 </td>
                             </tr>
                         </table>
                         <br />
                         <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md" Text="Save" OnClick="btnSubmit_Click" />
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