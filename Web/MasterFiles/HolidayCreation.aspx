<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayCreation.aspx.cs"
    Inherits="MasterFiles_HolidayCreation" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday Creation</title>
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
         td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
            // $('input:text:first').focus();
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

            //            $('input:text').keyup(function () {
            //                str = $(this).val()
            //                str = str.replace(/\s/g, '')
            //                $(this).val(str)
            //            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

        });

    </script>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($("#txtHolidayName").val() == "") { alert("Enter Holiday Name."); $('#txtHolidayName').focus(); return false; }
                var multiple = $('#<%=ddlMulti.ClientID%> :selected').text();
                if (multiple == "--Select--") { alert("Select Multiple Dates."); $('#ddlMulti').focus(); return false; }
                var month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (month == "--Select--") { alert("Select month."); $('#ddlMonth').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript">
        var myDropDown = document.getElementById("ddlMulti"); var length = myDropDown.options.length; //open dropdown myDropDown.size = length; //close dropdown myDropDown.size = 0;
    </script>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <asp:HiddenField ID="hdnHolidayID" runat="server" />
            <table border="0" cellpadding="3" cellspacing="3" id="tblStateDtls" align="center"
                >
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblHolidayName" runat="server" SkinID="lblMand" 
                            Height="19px" Width="100px"><span style="Color:Red">*</span>Holiday Name</asp:Label>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:TextBox ID="txtHolidayName" SkinID="TxtBoxStyle" 
                            onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="1" runat="server" Width="200px"
                            MaxLength="50" onkeypress="CharactersOnly(event);"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                   <td align="left" class="stylespc">
                        <asp:Label ID="lblMulti" runat="server" SkinID="lblMand"  Height="19px"
                            Width="100px"><span style="Color:Red">*</span>Multiple Dates</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMulti" TabIndex="2" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="-1">--Select--</asp:ListItem>
                            <asp:ListItem Value="0">Yes</asp:ListItem>
                            <asp:ListItem Value="1">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                   <td align="left" class="stylespc">
                        <asp:Label ID="lblFix_Date" runat="server" SkinID="lblMand" Text="Fixed Date" Width="100px"></asp:Label>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:TextBox ID="txtFix_Date" runat="server" onkeypress="Calendar_enter(event);" SkinID="MandTxtBox"  onfocus="this.style.backgroundColor='LavenderBlush'" 
                            onblur="this.style.backgroundColor='White'" TabIndex="3"></asp:TextBox>
                        <asp:CalendarExtender ID="calFix_Date" Format="dd/MM/yyyy" TargetControlID="txtFix_Date"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                   <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"  Width="100px"><span style="Color:Red">*</span>Month</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" TabIndex="4" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand" ><span style="color:Red">*</span>Division Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                           <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btnnew" Width="60px" Height="25px" Text="Save" 
                OnClick="btnSubmit_Click" />
        </center>
        <div class="loading" align="center">
            Loading. Please wait. <br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
