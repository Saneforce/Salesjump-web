<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuUserControl_TP.ascx.cs"
    Inherits="UserControl_MenuUserControl_TP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href='<%=ResolveClientUrl("../css/Master.css")%>' rel="stylesheet" type="text/css" />
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
<div class="Container">
    <div id="wrapper">
        <asp:Panel ID="pnldiv" runat="server" CssClass="spc">
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
                    </asp:Label>--%>
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:Label ID="Label1" runat="server" Style="text-transform: capitalize; font-size: 14px;
                            margin-top: 0px" ForeColor="#8A2EE6" Font-Bold="True" Font-Names="Verdana">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ul id="menu">
            <li><a href="../../../E-Report_DotNet/Default.aspx" class="first" onclick="ShowProgress();">
                Home</a></li>
            <li><a href="#">Master &raquo;</a>
                <ul>
                    <li><a href="#">SubDivision &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/SubDivisionList.aspx" onclick="ShowProgress();">
                                Entry</a></li>
                            <li><a href="../../../../MasterFiles/Subdiv_Productwise.aspx" onclick="ShowProgress();">
                                View - Productwise</a></li>
                            <li><a href="../../../../MasterFiles/Subdiv_Salesforcewise.aspx" onclick="ShowProgress();">
                                View - Field Forcewise</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Product &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/ProductCategoryList.aspx" onclick="ShowProgress();">
                                Category</a></li>
                            <li><a href="../../../../MasterFiles/ProductGroupList.aspx" onclick="ShowProgress();">
                                Group</a></li>
                            <li><a href="../../../../MasterFiles/ProductBrandList.aspx" onclick="ShowProgress();">
                                Brand</a></li>
                            <li><a href="../../../../MasterFiles/ProductList.aspx" onclick="ShowProgress();">Product
                                Detail</a></li>
                            <li><a href="../../../../MasterFiles/ProductRate.aspx" onclick="ShowProgress();">Statewise
                                - Rate Fixation</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../MasterFiles/SalesForceList.aspx" onclick="ShowProgress();">
                        Field Force</a></li>
                    <li><a href="#">Doctor &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/DoctorCategoryList.aspx" onclick="ShowProgress();">
                                Category</a></li>
                            <li><a href="../../../../MasterFiles/DoctorSpecialityList.aspx" onclick="ShowProgress();">
                                Speciality</a></li>
                            <li><a href="../../../../MasterFiles/DoctorClassList.aspx" onclick="ShowProgress();">
                                Class</a></li>
                            <li><a href="../../../../MasterFiles/DoctorCampaignList.aspx" onclick="ShowProgress();">
                                Campaign</a></li>
                            <li><a href="../../../../MasterFiles/DoctorQualificationList.aspx" onclick="ShowProgress();">
                                Qualification</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../MasterFiles/ProductReminderList.aspx" onclick="ShowProgress();">
                        Input</a></li>
                    <li><a href="#">Field Force Entries &raquo;</a>
                        <ul>
                            <%--  <li><a href="../../../../MasterFiles/MR/Territory/Territory.aspx"
                                onclick="ShowProgress();">Territory</a></li>--%>
                            <li><a href="../../../../MasterFiles/MR/Territory/Territory.aspx" onclick="ShowProgress();">
                                <asp:Label ID="lblTerritory" Text="Territory" runat="server"></asp:Label></a></li>
                            <li><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" onclick="ShowProgress();">
                                Listed Customer</a></li>
                            <li><a href="../../../../MasterFiles/MR/Chemist/ChemistList.aspx" onclick="ShowProgress();">
                                Chemist</a></li>
                            <li><a href="../../../../MasterFiles/MR/Hospital/HospitalList.aspx" onclick="ShowProgress();">
                                Hospital</a></li>
                            <li><a href="../../../../MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx" onclick="ShowProgress();">
                                Unlisted Customer</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../MasterFiles/HolidayList.aspx" onclick="ShowProgress();">Statewise
                        - Holiday Fixation</a></li>
                    <li><a href="#">Stockist Details &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/StockistList.aspx" onclick="ShowProgress();">Add/Edit/DeActivate</a></li>
                            <li><a href="../../../../MasterFiles/Stockist_Sale.aspx" onclick="ShowProgress();">FieldForce
                                Stockist Entry</a></li>
                            <li><a href="../../../../MasterFiles/Stockist_View.aspx" onclick="ShowProgress();">Stockist
                                View</a></li>
                            <li><a href="../../../../MasterFiles/Stockist_Sale_Status.aspx" onclick="ShowProgress();">
                                Entry Status</a></li>
                            <%--   <li><a href="../../../../MasterFiles/Stockist_HQ_Map.aspx" onclick="ShowProgress();">
                                HQ Map</a></li>--%>
                            <li><a href="../../../../MasterFiles/PoolName_List.aspx" onclick="ShowProgress();">Pool
                                Area Name Creation</a></li>
                        </ul>
                    </li>
                    <%--<li><a href="#">Expense &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Distance_Fixation.aspx" onclick="ShowProgress();">
                                SFC Updation</a></li>
                            <li><a href="#" onclick="ShowProgress();">SFC View</a></li>
                            <li><a href=".../../../../MasterFiles/AllowanceFixation.aspx" onclick="ShowProgress();">
                                Allowance Fixation</a></li>
                            <li><a href="../../../../MasterFiles/WrkTypeWise_Allowance.aspx"
                                onclick="ShowProgress();">Wrk Type Wise - Allowance Fix</a></li>
                            <li><a href="../../../../MasterFiles/FVExpense_Parameter.aspx" onclick="ShowProgress();">
                                Fixed/Variable Expense Parameter</a></li>
                        </ul>
                    </li>--%>
                </ul>
            </li>
            <li><a href="#">Activities &raquo;</a>
                <ul>
                    <%-- <li><a href="#">Std. Daywise Plan &raquo;</a>
                    <ul>
                        <li><a href="../../../../MasterFiles/MR_StdDayPlan.aspx" onclick="ShowProgress();">
                            Entry</a></li>
                        <li><a href="#">View</a></li>
                    </ul>
                </li>--%>
                    <li><a href="#">Approvals &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/MGR/Listeddr_admin_Approve.aspx" onclick="ShowProgress();">
                                Listed Customer Addition</a></li>
                            <li><a href="../../../../MasterFiles/MGR/Listeddr_adm_deAct_Approve.aspx" onclick="ShowProgress();">
                                Listed Customer Deactivation</a></li>
                            <li><a href="../../../../MasterFiles/MGR/TP_Calendar_Approve.aspx" onclick="ShowProgress();">
                                TP</a></li>
                            <li><a href="../../../../MasterFiles/DCR_Admin_Approval.aspx" onclick="ShowProgress();">
                                DCR</a></li>
                            <li><a href="../../../../MasterFiles/Leave_Admin_Approval.aspx" onclick="ShowProgress();">
                                Leave</a></li>
                        </ul>
                    </li>
                    <%-- <li><a href="#">Sample Despatch &raquo;</a>
                    <ul>
                        <li><a href="#">Entry</a></li>
                        <li><a href="#">Status</a></li>
                        <li><a href="#">View</a></li>
                    </ul>
                </li>
                <li><a href="#">Gift Despatch &raquo;</a>
                    <ul>
                        <li><a href="#">Entry</a></li>
                        <li><a href="#">Status</a></li>
                        <li><a href="#">View</a></li>
                    </ul>
                </li>--%>
                    <%--<li><a href="#">Target &raquo;</a>
                    <ul>
                        <li><a href="#">Entry</a></li>
                        <li><a href="#">Status</a></li>
                        <li><a href="#">View</a></li>
                    </ul>
                </li>--%>
                </ul>
            </li>
            <li><a href="#">Activity Reports</a>
                <ul>
                    <%-- <li><a href="../../../../MasterFiles/User_List.aspx" onclick="ShowProgress();">
                    User List</a></li>--%>
                    <li><a href="#">
                        <asp:Label ID="lblRoute" Text="Route Plan" runat="server"></asp:Label>
                        &raquo;</a>
                        <ul>
                            <li><a href="../../MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">
                                Status</a></li>
                        </ul>
                    </li>
                    <li><a href="#">TP &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../../../MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">
                                Status</a></li>
                        </ul>
                    </li>
                    <li><a href="#">DCR &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Reports/DCR_View.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../../../MasterFiles/Reports/DCR_Status.aspx" onclick="ShowProgress();">
                                Status</a></li>
                            <li><a href="../../../../MasterFiles/Reports/DCR_NotApprove.aspx" onclick="ShowProgress();">
                                Not Approved</a></li>
                            <li><a href="../../../../MasterFiles/Reports/DCR_NotSubmit.aspx" onclick="ShowProgress();">
                                Not Submitted</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../MasterFiles/DashBoard/TreeView.aspx" onclick="ShowProgress();">
                        Dash Board</a></li>
                    <%--<li><a href="#">Expense Statement &raquo;</a>
                    <ul>
                        <li><a href="#">View/Approval</a></li>
                        <li><a href="#">Status</a></li>
                    </ul>
                </li>--%>
                    <%-- <li><a href="#">Doctor Details &raquo;</a>
                    <ul>
                        <li><a href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">
                            View</a></li>
                    </ul>
                </li>
                <li><a href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">
                    Fieldforce Status</a></li>--%>
                </ul>
            </li>
            <li><a href="#">MIS Reports</a>
                <ul>
                    <%--<li><a href="#">DCR &raquo;</a>
                    <ul>
                        <li><a href="#">Analysis</a></li>
                        <li><a href="#">Joint Work</a></li>
                        <li><a href="#">Manager Analysis</a></li>
                        <li><a href="#">Coverage Analysis</a></li>
                    </ul>
                </li>--%>
                    <li><a href="../../../../MIS Reports/MissedCallReport.aspx" onclick="ShowProgress();">
                        Missed Call</a></li>
                    <%--<ul>
                        <li><a href="#">Listed Doctorwise</a></li>
                    </ul>--%>
                    <li><a href="#">Visit Details &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Visit_Details_Report.aspx" onclick="ShowProgress();">
                                Listed Doctorwise</a></li>
                            <li><a href="../../../../MIS Reports/Visit_Details_Consolidated.aspx" onclick="ShowProgress();">
                                Consolidated</a></li>
                            <li><a href="../../../../MIS Reports/Visit_Details_BasedonVisit.aspx" onclick="ShowProgress();">
                                Based on Visit</a></li>
                            <li><a href="../../../../MIS Reports/Visit_Details_Basedonfield.aspx" onclick="ShowProgress();">
                                Based on Field</a></li>
                            <%--   <li><a href="#">Modewise</a></li>
                        <li><a href="#">Productwise</a></li>--%>
                        </ul>
                    </li>
                    <%--<li><a href="#">Product Exposure &raquo;</a>
                    <ul>
                        <li><a href="#">Detailed View</a></li>
                        <li><a href="#">Speciality/Category Wise</a></li>
                    </ul>
                </li>--%>
                    <li><a href="#">Campaign &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Reports/Campaign_View.aspx" onclick="ShowProgress();">
                                View</a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li><a href="#">Options</a>
                <ul>
                    <li><a href="../../../MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">
                        Change Password</a></li>
                    <li><a href="../../../MasterFiles/Options/Vacant_MR_Access.aspx" onclick="ShowProgress();">
                        Vacant MR Login - Access</a></li>
                    <%-- <li><a href="#">Vacant MR Login - Access</a></li>--%>
                    <li><a href="#">Update/Delete &raquo;</a>
                        <ul>
                            <li><a href="../../../MasterFiles/Options/TPEdit.aspx" onclick="ShowProgress();">TP
                                Edit</a></li>
                            <li><a href="../../../MasterFiles/Options/TPDelete.aspx" onclick="ShowProgress();">TP
                                Delete</a></li>
                            <li><a href="../../../MasterFiles/Options/DCREdit.aspx" onclick="ShowProgress();">DCR
                                Edit</a></li>
                            <%-- <li><a href="#">DCR Delete</a></li>--%>
                            <li><a href="../../../MasterFiles/Options/MailView.aspx" onclick="ShowProgress();">Mail
                                Delete</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Setup &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Options/Permission_MR.aspx" tabindex="-1">Permission
                                For Mgrs -
                                <br />
                                To Access Vacant MR</a></li>
                            <%-- <li><a href="../../../MasterFiles/AdminSetup.aspx" onclick="ShowProgress();">
                            TP/DCR</a></li>--%>
                            <li><a href="../../../../MasterFiles/Options/SetupScreen.aspx" onclick="ShowProgress();">
                                Screen Access Rights</a></li>
                            <li><a href="../../../../MasterFiles/Options/AdminSetup.aspx" onclick="ShowProgress();">
                                Base Level</a></li>
                            <li><a href="../../../../MasterFiles/Options/AdminSetupMGR.aspx" onclick="ShowProgress();">
                                Managers</a></li>
                            <li><a href="../../../../MasterFiles/Options/Mgrwise_Core_Doc_Map.aspx" onclick="ShowProgress();">
                                Managerwise Core Customer Map</a></li>
                            <li><a href="../../../../MasterFiles/Options/Screenwise_Lock.aspx" onclick="ShowProgress();">
                                Screenwise Access</a></li>
                            <li><a href="../../../../MasterFiles/Mail_Folder_Creation.aspx" onclick="ShowProgress();">
                                Mail Folder Creation</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../MasterFiles/Options/FileUpload.aspx" onclick="ShowProgress();">
                        File Upload</a></li>
                    <li><a href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">
                        Mail Box</a></li>
                    <li><a href="../../../../MasterFiles/Options/NoticeBoard.aspx" onclick="ShowProgress();">
                        Notice Board</a></li>
                    <li><a href="../../../../MasterFiles/Options/FlashNews.aspx" onclick="ShowProgress();">
                        Flash News</a></li>
                    <li><a href="../../../../MasterFiles/Options/Quote.aspx" onclick="ShowProgress();">Quote
                        for the Week</a></li>
                    <li><a href="../../../../MasterFiles/Options/TalktoUs.aspx" onclick="ShowProgress();">
                        Talk to Us</a></li>
                    <li><a href="../../../../MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">
                        Leave Status</a></li>
                    <%--   <li><a href="../MasterFiles/Quote_Design.aspx">Design</a></li>--%>
                    <li><a href="#">Transfers &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Options/MR_MR_Transfer.aspx" onclick="ShowProgress();">
                                Transfer - Master Details</a></li>
                            <%-- <li><a href="../../../MasterFiles/Options/testing.aspx"
                            onclick="ShowProgrss();">Transfer - Master Details</a></li>--%>
                            <%-- <li><a href="#">Stockist With Sale</a></li>--%>
                            <li><a href="../../../../MasterFiles/MGR/Convert_Unlistto_Listeddr.aspx" onclick="ShowProgress();">
                                Convert Unlisted Customers - Listed Customers</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Upload &raquo;</a>
                        <ul>
                            <li><a href="../../../../MasterFiles/Options/ListedDr_Upload.aspx" onclick="ShowProgress();">
                                Listed Customer </a></li>
                            <li><a href="../../../../MasterFiles/Options/Listeddr_BulkUpload.aspx" onclick="ShowProgress();">
                                Listed Customer - Bulk</a></li>
                            <li><a href="../../../../MasterFiles/Options/Chemists_Upload.aspx" onclick="ShowProgress();">
                                Chemist</a></li>
                            <li><a href="../../../../MasterFiles/Options/Chemists_BulkUpload.aspx" onclick="ShowProgress();">
                                Chemist - Bulk</a></li>
                            <li><a href="../../../../MasterFiles/Options/Salesforce_Upload.aspx" onclick="ShowProgress();">
                                Field Force</a></li>
                            <%--   <li><a href="#">Unlisted Doctor</a></li>--%>
                        </ul>
                    </li>
                    <li><a href="#">Image Upload &raquo;</a>
                        <ul>
                            <%--     <li><a href="../../../../MasterFiles/Options/Loginpage_ImgUpload.aspx"
                            onclick="ShowProgress();">Login Page</a></li> --%>
                            <li><a href="../../../../MasterFiles/Options/Homepage_ImgUpload.aspx" onclick="ShowProgress();">
                                Home Page(Common For All)</a></li>
                            <li><a href="../../../../MasterFiles/Options/HomePage_FieldForcewise.aspx">Home Page(FieldForcewise)</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../MasterFiles/Options/Delayed_Release.aspx" onclick="ShowProgress();">
                        Delayed Release</a></li>
                    <%-- <li><a href="#">Release &raquo;</a>
                    <ul>
                        <li><a href="#">DCR Lock</a></li>
                        <li><a href="#">TP Lock</a></li>
                        <li><a href="#">Std.Daywise Lock</a></li>
                    </ul>
                </li>--%>
                    <%--  <li><a href="#">Setup</a></li>--%>
                </ul>
            </li>
            <li><a href="../../../E-Report_DotNet/Index.aspx" class="first" onclick="ShowProgress();">Logout</a>
            </li>
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
                                        Text="Back" OnClick="btnBack_Click" />
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
        <img id="Img1" src="../Images/loader.gif" runat="server" alt="" />
    </div>
</div>
