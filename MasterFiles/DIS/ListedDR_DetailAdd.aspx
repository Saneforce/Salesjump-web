<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDR_DetailAdd.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_ListedDR_DetailAdd" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/DIS_Menu.ascx" TagName="Menu2" TagPrefix="ucl3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Retailer Detail Add</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
        
        
        .space
        {
            padding: 3px 3px;
        }
        .sp
        {
            padding-left: 11px;
        }
        
        .marRight
        {
            margin-right: 35px;
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
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
                            $('#btnSave').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSave').click(function () {
                if ($("#txtName").val() == "") { alert("Enter Retailer Name."); $('#txtName').focus(); return false; }
                if ($("#txtMobile").val() == "") { alert("Enter Mobile No."); $('#txtMobile').focus(); return false; }
                if ($("#Txt_id").val() == "") { alert("Enter Retailer Code."); $('#Txt_id').focus(); return false; }
                if ($("#txtDOW").val() == "") { alert("Enter Contact Person Name."); $('#txtDOW').focus(); return false; }
                if ($("#salestaxno").val() == "") { alert("Enter Contact Person Name."); $('#salestaxno').focus(); return false; }

                var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Channel."); $('#ddlSpec').focus(); return false; }

                var clas = $('#<%=ddlClass.ClientID%> :selected').text();
                if (clas == "---Select---") { alert("Select Class."); $('#ddlClass').focus(); return false; }


            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
            <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <table id="Table1" runat="server" width="90%">
            <tr>
                <td align="right" width="30%">
                    <%--   <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana"
                        Visible="true"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <br />
        <center>
            <table width="62%" bgcolor="#D6E9C6">
                <tr>
                    <td colspan="4" align="left" style="border: 1px solid; background-color: #7AA3CC;">
                        <asp:Label ID="lblHead" runat="server" Text="  Personal Profile" Font-Size="14px"
                            Font-Names="Verdana" ForeColor="White" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblName" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Name of Retailer</asp:Label>
                        &nbsp;
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtName" runat="server" Width="170px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event)" onblur="this.style.backgroundColor='White'">
                        </asp:TextBox>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblDOB" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Mobile No</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtMobile" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblQual" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Retailer Code</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="Txt_id" runat="server"></asp:TextBox>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblDOW" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Contact Person Name</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <%-- <asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'" ReadOnly="true"
                    onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>   
               <asp:CalendarExtender ID="Caldow" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOW"> </asp:CalendarExtender>--%>
                        <asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblSpec" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Channel </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:DropDownList ID="ddlSpec" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="Label3" SkinID="lblMand" runat="server" Text="Tin No " Font-Bold="True"></asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="salestaxno" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblCatg" runat="server" SkinID="lblMand" Font-Bold="True">Sales TaxNo  </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="TinNO" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblTerritory" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Route</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:DropDownList ID="ddlTerritory" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="Label4" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Credit Days</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="creditdays" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblClass" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Class </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="Advanceamount" SkinID="lblMand" runat="server" Font-Bold="True">Advance Amount </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="Txt_advanceamt" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left" style="border: 1px solid; background-color: #7AA3CC;">
                        <asp:Label ID="lblHeadAddress" runat="server" ForeColor="White" Text=" Address  "
                            Font-Size="14px" Font-Names="Verdana" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <span style="color: Red">*</span><asp:Label ID="Label2" runat="server" ForeColor="White"
                            Text=" Address 1" Style="border: 1px solid; background-color: #7AA3CC;" Font-Size="12px"
                            Font-Names="Verdana" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtAddress" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="211px" Height="200px"></asp:TextBox>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="Label10" runat="server" ForeColor="White" Text=" Address 2" Style="border: 1px solid;
                            background-color: #7AA3CC;" Font-Size="12px" Font-Names="Verdana" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtStreet" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="211px" Height="200px" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Width="60px" Height="25px" Text="Save" OnClick="btnSave_Click"
                            CssClass="BUTTON" />&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear"
                            OnClick="btnClear_Click" CssClass="BUTTON" />
                    </td>
                </tr>
            </table>
        </center>
        <div class="loading" align="center">
            Loading. Please wait. Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
