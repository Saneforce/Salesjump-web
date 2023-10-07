<%@ Page Title="Not Submitted DCR Dates" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DCR_NotSubmit.aspx.cs" Inherits="Reports_DCR_NotSubmit" %>
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Not Submitted DCR Dates</title>
      <style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
       
    </style>
    
   
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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

                  var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                  if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }


              });
          }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <%-- <ucl:Menu ID="menu1" runat="server" /> --%>

        <br />
        <center>
        <br />
     
        <table>
            <tr>
                <td align="left" class="stylespc" width="120px">
                    <asp:Label ID="lblDivision" Visible="false" runat="server" SkinID="lblMand" Text="Division"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlDivision" Visible="false" runat="server" SkinID="ddlRequired" Width="350"></asp:DropDownList>
                </td>
            </tr>
            <tr >
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                </td>
                <td align="left"> 
                    <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" Width="250"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" Width="100">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100">
                   
                    </asp:DropDownList>
                </td>
            </tr>
                  
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Save" onclick="btnSubmit_Click"/>
     <%--   <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text="Clear"/>--%>
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