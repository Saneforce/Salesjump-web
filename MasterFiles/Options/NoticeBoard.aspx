<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="NoticeBoard.aspx.cs" Inherits="MasterFiles_Options_NoticeBoard" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Notice Board</title>
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


                  if ($("#txtNotice1").val() == "") { alert("Enter Content1."); $('#txtNotice1').focus(); return false; }
                  if ($("#txtStartDate").val() == "") { alert("Enter Start Date."); $('#txtStartDate').focus(); return false; }
                  if ($("#txtEndDate").val() == "") { alert("Enter End Date."); $('#txtEndDate').focus(); return false; }
              });
          }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>
        <br />
         
        <center>
         
            <table style="width: 70%; vertical-align:top">
                <tr style="height: 25px;">                
                <td style="width: 80%">
                <table >
                <tr>
                    <td valign="middle" style="vertical-align: middle;" align="left" class="stylespc">
                        <asp:Label ID="lblNotice1" runat="server" Text="Content1" Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td valign="top" align="left" class="stylespc">
                        <asp:TextBox ID="txtNotice1" runat="server" TextMode="MultiLine" Height="100px" Width="600px"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr style="height: 25px;" >
                    <td valign="middle" style="vertical-align: middle;" align="left" class="stylespc">
                        <asp:Label ID="lblNotice2" runat="server" Text="Content2" SkinID="lblMand"></asp:Label>
                    </td>
                    <td valign="top" align="left" class="stylespc">
                        <asp:TextBox ID="txtNotice2" runat="server" TextMode="MultiLine" Height="100px" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 25px;" class="stylespc">
                    <td valign="middle" style="vertical-align: middle;" align="left">
                        <asp:Label ID="lblNotice3" runat="server" Text="Content3" SkinID="lblMand"></asp:Label>
                    </td>
                    <td valign="top" align="left" class="stylespc">
                        <asp:TextBox ID="txtNotice3" runat="server" TextMode="MultiLine" Height="100px" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 25px;" >
                    <td valign="middle" style="vertical-align: middle;" align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Text="Start Date" SkinID="lblMand"></asp:Label>
                    </td>
                    <td valign="top" align="left" class="stylespc">
                        <asp:TextBox ID="txtStartDate" runat="server" onkeypress="Calendar_enter(event);" SkinID="MandTxtBox"></asp:TextBox>
                        <asp:CalendarExtender ID="CalStartDate" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                            runat="server" />
                    </td>
                </tr>
                <tr style="height: 25px;">
                    <td valign="middle" style="vertical-align: middle;" align="left" class="stylespc">
                        <asp:Label ID="Label2" runat="server" Text="End Date" SkinID="lblMand"></asp:Label>
                    </td>
                    <td valign="top" align="left" class="stylespc">
                        <asp:TextBox ID="txtEndDate" onkeypress="Calendar_enter(event);" runat="server" SkinID="MandTxtBox"></asp:TextBox>
                        <asp:CalendarExtender ID="CalEndDate" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                <td></td>
                    <td align="left">
                        <asp:CheckBox ID="chkback" runat="server" Text="Set as Home Page" Font-Names="Verdana" Font-Size="12px"/>
                    </td>
                </tr>
                </table>
                </td>
                  <td style="vertical-align:top">
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" Font-Size="Medium" OnClick="lnkEdit_Click"></asp:LinkButton>
                   </td>
                   <td style="vertical-align:top">
                        <asp:DropDownList ID="ddlEdit" runat="server" SkinID="ddlRequired" 
                            Visible="false" onselectedindexchanged="ddlEdit_SelectedIndexChanged" >
                        </asp:DropDownList>
                      
                    </td>
                    <td style="vertical-align:top">
                    
                     <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="Go" CssClass="BUTTON" onclick="btnGo_Click" Visible="false"/>
                    </td>
               
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save" CssClass="BUTTON" OnClick="btnSubmit_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" CssClass="BUTTON" OnClick="btnClear_Click" />
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