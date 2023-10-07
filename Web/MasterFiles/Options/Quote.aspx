<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Quote.aspx.cs" Inherits="MasterFiles_Options_Quote" %>
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Quote for the Week</title>
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
              $('#btnSubmit').click(function () {


                  if ($("#txtQuote").val() == "") { alert("Enter Quote."); $('#txtQuote').focus(); return false; }

              });
          }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>
        <br /> 
        <center>
        <table>
            <tr style="height:25px;">
                <td valign="middle" style="vertical-align:middle;">
                    <asp:Label ID="lblQuote" runat="server" Text="Quote"  ></asp:Label>                                        
                </td>
                <td valign="top">
                    <asp:TextBox ID="txtQuote" runat="server" TextMode="MultiLine" Height="100px" 
                        Width="600px"></asp:TextBox>
                </td>      
             </tr>
             <tr>
             <td></td>
             <td align="left">
                  <asp:CheckBox ID="chkback" runat="server" Text="Set as Home Page" />
                  </td>
             </tr>
        </table> 
      
        <br />
        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Save" onclick="btnSubmit_Click" />
        &nbsp;&nbsp;        
        <asp:Button ID="btnDelete" runat="server" CssClass="BUTTON" Text="Delete-Add Quote" onclick="btnDelete_Click" />
        </center>     
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