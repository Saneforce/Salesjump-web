<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoolName_Creation.aspx.cs"
    Inherits="MasterFiles_PoolName_Creation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pool Area Name Creation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
            z-index: 999;</style>
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
            $('#btnSubmit').click(function () {
                if ($("#txtPool_Sname").val() == "") { alert("Enter Short Name."); $('#txtPool_Sname').focus(); return false; }
                if ($("#txtPool_Name").val() == "") { alert("Enter Pool Area Name."); $('#txtPool_Name').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDivisionDtls" align="center"
               >
                <tr>
                    <td align="left">
                        <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px"
                            Width="120px"><span style="color:Red">*</span>Short Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPool_Sname" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="1" runat="server" MaxLength="10"
                            onkeypress="CharactersOnly(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblPoolName" runat="server" SkinID="lblMand" 
                            Height="18px" Width="120px"><span style="color:Red">*</span>Pool Area Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPool_Name" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="120" onkeypress="CharactersOnly(event);">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text="Save" OnClick="btnSubmit_Click" />
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
