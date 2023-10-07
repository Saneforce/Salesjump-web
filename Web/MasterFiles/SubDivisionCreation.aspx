<%@ Page Title="Division Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="SubDivisionCreation.aspx.cs"
    Inherits="MasterFiles_SubDivisionCreation" %>
     <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Sub-Division</title>
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
            z-index: 999;</style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
	 function fillslot(scode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SubDivisionCreation.aspx/edit_subdiv",
                data: "{'divcode':'" + divcode + "','scode':'" + scode + "'}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val(dts[0].subdivision_code);
                    $('input#ctl00_ContentPlaceHolder1_txtSubDivision_Sname').val(dts[0].subdivision_sname);
                    $('input#ctl00_ContentPlaceHolder1_txtSubDivision_Name').val(dts[0].subdivision_name);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }
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
            // $('input:text:first').focus();
			
            divcode =<%=Session["div_code"]%>;
            $('#hscode').val(<%=hqcode%>);
            fillslot($('#hscode').val());
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
			var hqcode = $('#hscode').val();
                if ($('#<%=txtSubDivision_Sname.ClientID%>').val() == "") { alert("Please Enter Division sname."); $('#<%=txtSubDivision_Sname.ClientID%>').focus(); return false; }
                if ($('#<%=txtSubDivision_Name.ClientID%>').val() == "") { alert("Please Enter Division Name."); $('#<%=txtSubDivision_Name.ClientID%>').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
       <input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" onclick="history.back(-2)" />
	   <input type="hidden" id="hscode" />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDocCatDtls">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px"
                            Width="120px"><span style="Color:Red">*</span>Division sname</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSubDivision_Sname" TabIndex="1" SkinID="MandTxtBox" 
                            onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" MaxLength="10"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                  <td align="left" class="stylespc">
                        <asp:Label ID="lblDivisionName" runat="server" SkinID="lblMand" 
                            Height="18px" Width="120px"><span style="Color:Red">*</span>Division Name</asp:Label>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSubDivision_Name" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="50" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
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