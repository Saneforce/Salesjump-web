<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_Form_Mgr.aspx.cs" Inherits="MasterFiles_MGR_Leave_Form_Mgr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Leave Application Form</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        table, th
        {
            border: 1px solid black;
            border-collapse: collapse;
        }
        
        .padding
        {
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
            padding-bottom: 3px;
        }
            .chkboxLocation label 
{  
    padding-right: 5px; 
}

    </style>
   
</head>
<body>
    <form id="form1" runat="server">
     <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
                            $('#btnApprove').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnApprove').click(function () {
                var divi = $('#<%=ddltype.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Type of Leave."); $('#ddltype').focus(); return false; }
                if ($("#txtLeave").val() == "") { alert("Enter Leave From Date."); $('#txtLeave').focus(); return false; }
                if ($("#txtLeaveto").val() == "") { alert("Enter Leave To Date."); $('#txtLeaveto').focus(); return false; }
                if ($("#txtreason").val() == "") { alert("Enter Reason for Leave."); $('#txtreason').focus(); return false; }
                if (confirm('Do you want to Submit?')) {
                    return true;
                }
                else {
                    return false;
                }
             
            });
        });
    </script>
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
      <center>
            <asp:Table ID="tblLeave" BorderStyle="Solid" BackColor="White"  BorderWidth="1"  Width="70%" 
                CellSpacing="5" CellPadding="5" runat="server">
                 <asp:TableHeaderRow HorizontalAlign="Center" Font-Size="14px" Font-Names="Verdana">
                <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle" ColumnSpan="2">Leave Form</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow CssClass="padding" HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="padding" VerticalAlign="Middle">
                        <asp:Label ID="lblName" runat="server" Width="140px" Text=" Name Of the Employee " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="lblcol" runat="server" >:</asp:Label>
                        <asp:Label ID="lblemp" runat="server" SkinID="lblMand"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" CssClass="padding" VerticalAlign="Middle">
                        <asp:Label ID="lblHq" runat="server" Width="120px" Text="HQ " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="Label1" runat="server" >: </asp:Label>
                        <asp:Label ID="lblSfhq" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                        <asp:Label ID="lblCode" runat="server" Width="140px" Text="Emp Code " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="Label2" runat="server" >: </asp:Label>
                        <asp:Label ID="lblempcode" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                        <asp:Label ID="lblDesign" runat="server" Width="120px" Text="Designation " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="Label3" runat="server" >: </asp:Label>
                        <asp:Label ID="lbldesig" runat="server" SkinID="lblMand"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                        <asp:Label ID="lblDivision" SkinID="lblMand" Width="140px" runat="server" Text="Division Name "></asp:Label>
                          <asp:Label ID="Label4" runat="server" >: </asp:Label>
                        <asp:Label ID="lbldivi" runat="server" SkinID="lblMand"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lbltype" runat="server" Width="140px" Text="Type of Leave " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="Label5" runat="server" >: </asp:Label>
                        <asp:DropDownList ID="ddltype" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="lblLeave" runat="server" Width="140px" SkinID="lblMand" Text="Leave From Date "></asp:Label>
                          <asp:Label ID="Label6" runat="server" >: </asp:Label>
                        <asp:TextBox ID="txtLeave" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);" Width="100px" AutoPostBack="true" OnTextChanged="txtLeave_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom"
                            runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtLeave"
                            PopupButtonID="imgPopup" runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="lblLeaveto" runat="server" Width="140px" SkinID="lblMand" Text="Leave To Date "></asp:Label>
                          <asp:Label ID="Label7" runat="server" >: </asp:Label>
                        <asp:TextBox ID="txtLeaveto" SkinID="MandTxtBox" runat="server" onkeypress="Calendar_enter(event);" AutoPostBack="true" Width="100px" OnTextChanged="txtLeaveto_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="imgPop" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom"
                            runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtLeaveto"
                            PopupButtonID="imgPop" runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="lblDays" runat="server" Width="140px" SkinID="lblMand" Text="Number Of Days "></asp:Label>
                          <asp:Label ID="Label8" runat="server" >: </asp:Label>
                        <asp:Label ID="lblDaysCount" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell CssClass="padding">
                        <asp:Label ID="lblreason" Width="120px" runat="server" Text="Reason For Leave " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="Label9" runat="server" >: </asp:Label>
                        <br />
                        <asp:TextBox ID="txtreason" runat="server" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" Height="70px" Width="350px"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblAddr" runat="server" Width="120px" Text="Address On Leave " SkinID="lblMand"></asp:Label>
                          <asp:Label ID="Label10" runat="server" >: </asp:Label>
                        <br />
                        <asp:TextBox ID="txtAddr" runat="server" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" Height="70px" Width="350px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow >
                    <asp:TableCell CssClass="padding" BorderWidth="1" HorizontalAlign="Left" ColumnSpan="2">                    
                        <asp:Label ID="lblInform" runat="server" SkinID="lblMand" Text="Informed Manager : "></asp:Label>
                        <asp:CheckBoxList ID="chkmanager" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" CssClass="chkboxLocation"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text=" Phone"></asp:ListItem>
                            <asp:ListItem Value="2" Text=" E-Mail"></asp:ListItem>
                            <asp:ListItem Value="3" Text=" SMS"></asp:ListItem>
                        </asp:CheckBoxList>
                     
                        <asp:Label ID="lblho" runat="server" SkinID="lblMand" Text="Informed HO : "></asp:Label>
                        <asp:CheckBoxList ID="chkho" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" CssClass="chkboxLocation" 
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text=" Phone"></asp:ListItem>
                            <asp:ListItem Value="2" Text=" E-Mail"></asp:ListItem>
                            <asp:ListItem Value="3" Text=" SMS"></asp:ListItem>
                        </asp:CheckBoxList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="padding" HorizontalAlign="Left" ColumnSpan="2">
                     <span style="vertical-align:top">
                        <asp:Label ID="lblValid" runat="server" SkinID="lblMand" Text="If no Phone / E-Mail /SMS ,Valid Reason : "></asp:Label></span>
                            <asp:TextBox ID="lblValidreason" Width="450px"  BorderStyle="Solid" BorderColor="Gray" Height="40px" runat="server" TextMode="MultiLine" SkinID="lblMand"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>
        <br />
        <center>
            <asp:Panel ID="pnlmr" runat="server">
                <asp:Button ID="btnApprove" runat="server" Text="Submit for Approval" CssClass="BUTTON" Width="140px" Height="25px" 
                    OnClick="btnApprove_Click" />
            </asp:Panel>
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
