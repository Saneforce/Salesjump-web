<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesForce_Transfer.aspx.cs" Inherits="MasterFiles_SalesForce_Transfer" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            $('#btnGo').click(function () {
                var type = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
                var type = $('#<%=ddlrepla.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Replacement For."); $('#ddlrepla').focus(); return false; }
                if ($("#txteffe").val() == "") { alert("Enter Effective Date."); $('#txteffe').focus(); return false; }
              

            });
        });
    </script>
    <form id="form1" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" />
      <br />
      <center >
       <asp:Label ID="lblheader" runat ="server"  style="font-weight:bold; text-decoration:underline; border-style:none; font-family:Verdana; font-size:14px; border-color:#E0E0E0; color :#8A2EE6" Text="Selected Transfer Area for" SkinID ="lblMand" ></asp:Label>
       <br />
       <br />
         <table align="center">
           <tr>
                    <td align="left" class="lblSpace">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                  <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true" onselectedindexchanged="ddlFieldForce_SelectedIndexChanged"                          
                            >
                        </asp:DropDownList>
                    </td>
           </tr>

            <tr>
                   <td align="left">
                        <asp:Label ID="lblmode" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rdoMode" runat="server" RepeatDirection="Horizontal" Font-Names="Verdana" Font-Size="11px"
                            AutoPostBack="true" OnSelectedIndexChanged="rdoMode_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="New Id&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Replacement"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>

                 <tr>
                    <td align="left" class="lblSpace">
                        <asp:Label ID="lblreplace" runat="server" Text="Replacement For " SkinID="lblMand" Visible ="false" ></asp:Label>
                    </td>
                 <td class="stylespc" align="left">
                    <asp:DropDownList ID="ddlrepla" runat="server" Visible="false"  
                        onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"
                        SkinID="ddlRequired" TabIndex="6">
                        <%--  <asp:ListItem Selected="true" Value="">---Select Mgr---</asp:ListItem> --%>
                    </asp:DropDownList>
                </td>
           </tr>

             <tr>
                <td class="stylespc" align="left">
                    <asp:Label ID="lbleffective" runat="server"  SkinID="lblMand">Effective Date</asp:Label>
                </td>
                <td class="stylespc" align="left">
                    <asp:TextBox ID="txteffe" runat="server" Height="25px" onblur="this.style.backgroundColor='White'"
                        onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);" SkinID="MandTxtBox" TabIndex="9"
                        Width="145px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" runat="server" TargetControlID="txteffe" />
                </td>               
            </tr>
         </table>
      </center>
      <br />
       <center>
             <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
                OnClick="btnGo_Click" />
        </center>

    </div>
    </form>
</body>
</html>
