<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pnlMenu.ascx.cs" Inherits="UserControl_pnlMenu" %>
<link href='<%=ResolveClientUrl("../css/AdminMenuStyle.css")%>' rel="stylesheet"
    type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
     .style1
    {
        width: 381px;
        margin-top:0%;
    }
     .BUTTON:hover {

background-color:#336277;
-webkit-border-radius: 6px;
-moz-border-radius: 6px;
border-radius: 6px;
color:White;

}
.under
{   
    text-decoration:underline;
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
          body
        {
            font-family: Arial;
            font-size: 10pt;
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
           .spc
    {
        padding-top:5px;
        padding-bottom:5px;
        padding-left:5px;
    }
jQuery(document).ready(function(){ 


    if (jQuery(window).width() < 900) { 


        jQuery(".AdminMenuStyle").css("display", "none"); 


    }   


}); 


jQuery(window).resize(function () { 


        if (jQuery(window).width() < 900) { 


            jQuery(".top-menu").css("display", "none"); 


        } 


});  

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
        //Redirect to refresh Session.
        window.location = window.location.href;
    }
    function ExSession() {
        window.location.replace("http://trial.sanffa.info/");
    }
</script>
<div id="wrapper">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Panel ID="pnldiv" runat="server" CssClass="spc">
    <table width="100%" border="0">
        <tr>
            <td class="style1">
                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                    font-size: 14px; text-align: left; margin-top: 0px" ForeColor="Maroon" Font-Bold="True"
                    Font-Names="Verdana">
                </asp:Label>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <ul id="menu">
        <li><a href="../../../Default2.aspx" class="first" onclick="ShowProgress();">
            Home</a></li>
        <li><a href="#">Master &raquo;</a>
            <ul>
                <li id="LiDiv" runat="server"><a href="../../../MasterFiles/DivisionList.aspx" onclick="ShowProgress();">
                    Company</a> </li>
                <li id="LiMnulst" runat="server"><a href="../../../MasterFiles/MenuList.aspx" onclick="ShowProgress();">
                    Menu List</a> </li>
                <li id="Listate" runat="server"><a href="../../../MasterFiles/StateLocationList.aspx" onclick="ShowProgress();">
                    State/Location</a></li>
                <li id="lides" runat="server"><a href="../../../MasterFiles/Designation.aspx" onclick="ShowProgress();">
                    Designation</a></li>
                <li id="liholi" runat="server"><a href="../../../MasterFiles/Holiday_List.aspx" onclick="ShowProgress();">
                    Holiday Master</a></li>
                <li ><a href="../../../MasterFiles/HO_ID_View.aspx" onclick="ShowProgress();">
                    <asp:Label ID="lblRoute" Text="HO ID Creation" runat="server"></asp:Label> </a></li>
    <li id="SubHo" runat="server"><a href="../../../MasterFiles/Sub_HO_ID_View.aspx" onclick="ShowProgress();">
                    HO ID Creation </a></li>
                <li id="Lique" runat="server">
                    <a href="../../../MasterFiles/Query_Box_List.aspx" onclick="ShowProgress();">Query</a>
                    </li>
            </ul>
        </li>
        <li><a href="#">Reports &raquo;</a>
            <%-- <ul>
				                            <li><a href="../../../MasterFiles/Reports/FieldForceReport.aspx" onclick="ShowProgress();">Field Force</a></li>
                                            <li><a href="../../../MasterFiles/Reports/rptDoctorDetails.aspx" onclick="ShowProgress();">Listed Doctor</a></li>
                                            <li><a href="../../../MasterFiles/Reports/UserList.aspx" onclick="ShowProgress();">User List</a></li>
    		                            </ul>--%>
            <ul id="lireports" runat="server">
                <li><a href="../../../../MasterFiles/User_List.aspx" onclick="ShowProgress();">
                    User List</a></li>
                <%-- <li><a href="#">Customer Details &raquo;</a>
                    <ul>
                        <li><a href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">
                            View</a></li>
                         <li><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">
                        Territory view</a></li>
                    </ul>
                </li>
                <li><a href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">
                    Fieldforce Status</a></li>
                <li><a href="../../../../MasterFiles/Reports/CallAverage.aspx" onclick="ShowProgress();">
                    Call Average View</a></li>
                <li><a href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx" onclick="ShowProgress();">
                    Work Type View Status </a></li> --%>
                <li><a href="../../../../MasterFiles/Reports/DCRCount/frmDcrCount.aspx" onclick="ShowProgress();">
                    DCR Count View </a></li>
            </ul>
        </li>
          <li><a href="#">Options &raquo;</a>
        <ul>
         <li id="Limail" runat="server"><a href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">Mail Box</a></li>   
          <%--<li id="Li1" runat="server"><a href="../../../../MIS Reports/Statusview_Prd_Camp.aspx" onclick="ShowProgress();">Status View</a></li>--%>   
         </ul>
        </li>
        <li><a href="#">
            Dash Board &raquo;</a>
            <ul>
              <li id="lidash" runat="server"><a href="../../../../MasterFiles/DashBoard/DashBoard.aspx" onclick="ShowProgress();">Dash Board</a></li>
            </ul>
            </li>
        <li><a href="../../../Index.aspx" class="first" onclick="ShowProgress();">
            Logout</a></li>
    </ul>
    <br />
    <asp:Panel ID="pnlHeader" runat="server" BorderStyle="Solid" Width="95%" align="center">
        <table width="95%" cellpadding="0" cellspacing="0" align="center" frame="box">
            <tr>
                <td>
                    <table id="Table2" runat="server" width="100%" border="1">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                    text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" style="width: 45%">
                                <asp:Label ID="lblHeading" runat="server" CssClass="under" Style="text-transform: capitalize;
                                    font-size: 14px; text-align: center;" ForeColor="#336277" Font-Bold="True" Font-Names="Verdana">
                                </asp:Label>
                            </td>
                            <td align="right" class="style3" style="width: 55%">
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Height="25px" Width="60px"
                                    Text="Back" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" runat="server" alt="" />
        </div>
    </asp:Panel>
    <%--    <asp:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" BorderColor="DarkSlateGray"
        Color="DarkSlateGray" Radius="2" TargetControlID="pnlHeader">
    </asp:RoundedCornersExtender>--%>
</div>