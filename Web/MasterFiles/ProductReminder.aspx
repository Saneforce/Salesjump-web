<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductReminder.aspx.cs"
    Inherits="MasterFiles_ProductReminder" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Details</title>
    <style type="text/css">
        #Table1
        {
            margin-left: 370px;
        }
        
        #tblLocationDtls
        {
            margin-left: 370px;
        }
        #Submit
        {
          
        }
        
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
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
 
</head>
<body>
   <script type="text/javascript">
       $(document).ready(function () {
           //$('input:text:first').focus();
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
                           $('#Submit').focus();
                       }
                   }
               }
           });
           $('#Submit').click(function () {
               if ($("#txtGift_SName").val() == "") { alert("Enter Short Name."); $('#txtGift_SName').focus(); return false; }
               if ($("#txtGiftName").val() == "") { alert("Enter Name."); $('#txtGiftName').focus(); return false; }

               var cat = $('#<%=ddlGiftType.ClientID%> :selected').text();
               if (cat == "--Select--") { alert("Enter Type."); $('#ddlGiftType').focus(); return false; }
               if ($("#txtGiftValue").val() == "") { alert("Enter Gift Value."); $('#txtGiftValue').focus(); return false; }
               if ($("#txtEffFrom").val() == "") { alert("Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
               if ($("#txtEffTo").val() == "") { alert("Enter Effective To Date."); $('#txtEffTo').focus(); return false; }
               if ($('#chkboxLocation input:checked').length > 0) { return true; } else { alert('Select State'); return false; }
               if ($('#chkSubdiv input:checked').length > 0) { return true; } else { alert('Select Subdivision'); return false; }

           });
       }); 
    </script>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
    
            <br />
            <table runat="server" cellpadding="5" cellspacing="5" style="margin-left:31.5%">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblGift_SName" runat="server" SkinID="lblMand" 
                            Width="130px"><span style="Color:Red">*</span>Short Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtGift_SName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="1" runat="server" MaxLength="15"
                            CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblGiftName" runat="server" SkinID="lblMand" Width="100px"><span style="Color:Red">*</span>Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtGiftName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" MaxLength="80"
                            CssClass="TEXTAREA" Width="200px" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblGiftType" runat="server" SkinID="lblMand" Width="100px"><span style="Color:Red">*</span>Type</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlGiftType" runat="server" SkinID="ddlRequired" TabIndex="4">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>            
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblGiftValue" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Value(in RS)</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtGiftValue" SkinID="MandTxtBox" onkeypress="CheckNumeric(event);"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            TabIndex="5" runat="server" MaxLength="5" CssClass="TEXTAREA">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblEffFrom" runat="server" SkinID="lblMand" 
                            Width="100px"><span style="Color:Red">*</span>Effective From</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffFrom" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblEffTo" runat="server" SkinID="lblMand" Width="100px"><span style="Color:Red">*</span>Effective To</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffTo" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                            TabIndex="7" onblur="this.style.backgroundColor='White'" />
                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo"
                            runat="server" />
                    </td>
                </tr>
            </table>
                <center>
            <table border="1" cellpadding="5" cellspacing="5">
                <tr>
                    <td rowspan="" align="center">
                        <asp:Label ID="lblTitle_LocationDtls" runat="server" Width="210px" Text="Select the State/Location"
                            TabIndex="8" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Underline="true" Font-Names="Verdana"
                            Font-Size="12px" ForeColor="#8A2EE6" ></asp:Label>
                    </td>
                </tr>
                <tr >
                    <td  align="left" style="width: 240px; height: 10px;">
                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="State_Name" DataValueField="State_Code"
                            Font-Names="Verdana" Font-Size="8pt" RepeatColumns="4" RepeatDirection="vertical"
                            Width="500px" TabIndex="9">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table border="1" cellpadding="5" cellspacing="5" >
                <tr>
                    <td rowspan="" align="center">
                        <asp:Label ID="lbldivision" runat="server" Width="210px" Text="Select the Sub Division" TabIndex="6"
                            BorderColor="#E0E0E0" BorderStyle="None" Font-Underline="true" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="12px" ForeColor="#8A2EE6">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left"  style="width: 250px; height: 10px;">                      
                        <asp:CheckBoxList ID="chkSubdiv" runat="server" DataTextField="subdivision_name"
                            DataValueField="subdivision_code" RepeatDirection="Vertical" RepeatColumns="4"
                            Width="500px"  Style="font-size: 8pt;  font-family: Verdana;">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <table >
                <tr>
                    <td >
                        <asp:Button ID="Submit" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
                            Text="Save" OnClick="Submit_Click" />
                    </td>
                </tr>
            </table>
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
