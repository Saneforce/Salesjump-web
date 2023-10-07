<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoard.ascx.cs" Inherits="DashBoard_MenuUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href='<%=ResolveClientUrl("../css/Master.css")%>' rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/bootstrap.css")%>">
<!-- stylesheet -->
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/style.css")%>" title="style" />
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/responsive.css")%>">
<!-- font-awesome -->
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/font-awesome.min.css")%>" type="text/css" />
<!-- crousel css -->
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/owl.carousel.css")%>" type="text/css" />
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/owl.theme.css")%>" type="text/css" />
<link rel="stylesheet" href="<%=ResolveClientUrl("../lcss/headerstyle.css")%>"/>
<!-- IE6-8 support of HTML5 elements -->
<!-- [if lt IE 9]>
	<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif] -->
<style type="text/css">
    .menu
    {
        margin-top: 0px;
    }
    .BUTTON : hover
    {
        background-color: #A6A6D2;
    }
    .style3
    {
        width: 99px;
    }
    .under
    {
        margin-top: 2px;
        text-decoration: underline;
    }
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
        z-index: 999;
    }
    .size
    {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 50px;
    }
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .spc
    {
        padding-top: 5px;
        padding-bottom: 5px;
        padding-left: 5px;
    }
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 300px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding: 0;
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        padding: 10px;
        min-height: 50px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
    .WebContainer
    {
        width: 100%;
        height: auto;
    }
    .dropdown-menu
    {
        float: right;
    }
    #menu {
    width: 100%;
    margin: 0px auto;
    border: 1px solid #666699;
    background-color: #19a4c6;
    display: table;
    border-collapse: collapse;
    border: none;
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
<asp:LinkButton ID="lnkFake" runat="server" />
<asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup"
    TargetControlID="lnkFake" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground"
    OnOkScript="ResetSession()" OnCancelScript="ExSession()">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    <div class="header">
        Session Expiring!
    </div>
    <div class="body">
        Your Session will expire in&nbsp;<span id="seconds"></span>&nbsp;seconds.<br />
        Do you want to Continue? <span id="secondsIdle" style="visibility: hidden"></span>
    </div>
    <div class="footer" align="right">
        <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="yes" />
        <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
    </div>
</asp:Panel>
<script type="text/javascript">
    function SessionExpireAlert(timeout) {
        var seconds = timeout / 1000;
        document.getElementsByName("secondsIdle").innerHTML = seconds;
        document.getElementsByName("seconds").innerHTML = seconds;
        setInterval(function () {
            seconds--;
            document.getElementById("seconds").innerHTML = seconds;
            document.getElementById("secondsIdle").innerHTML = seconds;
        }, 1000);
        setTimeout(function () {
            //Show Popup before 20 seconds of timeout.
            $find("mpeTimeout").show();
        }, timeout - 120 * 1000);
        setTimeout(function () {
            window.location.replace("http://trial.sanffa.info/");
        }, timeout);
    };
    function ResetSession() {
        debugger;
        //Redirect to refresh Session.
        window.location = window.location.href;
    }
    function ExSession() {
        debugger;
        window.location.replace("http://trial.sanffa.info/");
    }
</script>
<style>
    #menu li
    {
        background-color: #19a4c6;
        border-color:#19a4c6;
        float: left;
        
        box-shadow: 1px 0 0;
        position: relative;
        width: 14.2%;
        display: inline;
        margin: 0;
    }
    #menu li ul li a {
    width: 680%;
    background-color:#19a4c6;
}
</style>
<section class="top-bar">
    <div class="container">
    <h2 class="hidden">topbar</h2>
        <div class="top-bar-left pull-left">

            <div class="social clearfix">
            </div>            
        </div>
        <div class="top-bar-right pull-right">          
            <div class="top-info">
                <ul>
                    <li><a href="#">User Name</a></li>
            </ul></div>            
        </div>
    </div>
</section>
<div class="Container">
    <div id="wrapper">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
        </asp:ToolkitScriptManager>
        <%--<asp:Panel ID="pnldiv" runat="server" CssClass="spc">
            <table width="100%" border="0">
                <tr>
                    <td>
                        <asp:Label ID="lblSessionTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 35%">
                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                            font-size: 14px; text-align: left; margin-top: 0px" ForeColor="#8A2EE6" Font-Bold="True"
                            Font-Names="Verdana">
                        </asp:Label>
                    </td>
                    <td align="center" style="width: 30%">
                        <%--<asp:Label ID="LblDiv" runat="server" Text="DivName" Style="text-transform: capitalize;
                        font-size: 14px; margin-top: 0px" ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana">
                    </asp:Label>
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:Label ID="Label1" runat="server" Style="text-transform: capitalize; font-size: 14px;
                            margin-top: 0px" ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>--%>
      <ul id="menu">
        <li></li>
            <li><a href="../../../../Default.aspx" class="first" onclick="ShowProgress();">
                Home</a></li>
              
            <li><a href="../../../../Index.aspx" class="first" onclick="ShowProgress();">
                Logout</a> </li>
       </ul>
        <br />
        <asp:Panel ID="pnlHeader" runat="server" Width="100%" align="center">
            <table width="95%" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <table id="Table1" runat="server" width="100%">
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                        text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                </td>
                                <td align="center" style="width: 30%">
                                    <asp:Label ID="lblHeading" runat="server" Style="text-transform: capitalize; font-size: 14px;
                                        text-align: center;" ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana"
                                        CssClass="under">
                                    </asp:Label>
                                </td>
                                <td align="right" class="style3" style="width: 35%">
                                    <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Height="25px" Width="70px"
                                        Text="Back" OnClick="btnBack_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--<asp:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" BorderColor="DarkSlateGray"
        Color="DarkSlateGray" Radius="2" TargetControlID="pnlHeader">
    </asp:RoundedCornersExtender>--%>
    </div>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../Images/loader.gif" runat="server" alt="" />
    </div>
</div>
