<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuUserControl.ascx.cs"
    Inherits="UserControl_MenuUserControl" %>
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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
        </asp:ToolkitScriptManager>
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
            <li><a href="../../../../E-Report_DotNet/Default2.aspx" class="first" onclick="ShowProgress();">Home</a></li>
            <li><a href="#">Master &raquo;</a>
                <ul>
                    <li><a href="#">Division &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/SubDivisionList.aspx" onclick="ShowProgress();">
                                Entry</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Subdiv_Productwise.aspx" onclick="ShowProgress();">
                                View - Productwise</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Subdiv_Salesforcewise.aspx" onclick="ShowProgress();">
                                View - Field Forcewise</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Product &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ProductCategoryList.aspx" onclick="ShowProgress();">
                                Category</a></li>
                            <%--<li><a href="../../../../E-Report_DotNet/MasterFiles/ProductGroupList.aspx" onclick="ShowProgress();">
                                Group</a></li>--%>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ProductBrandList.aspx" onclick="ShowProgress();">
                                Brand</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Multi_Unit_Entry.aspx" onclick="ShowProgress();">
                                UOM</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ProductList.aspx" onclick="ShowProgress();">Product
                                Detail</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ProductRate.aspx" onclick="ShowProgress();">Statewise
                                - Rate Fixation</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/ProductRate.aspx" onclick="ShowProgress();">
                                Statewise - Product Rate - View</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Geography &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/AreaList.aspx" onclick="ShowProgress();">Area</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ZoneList.aspx" onclick="ShowProgress();">Zone</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/TerritoryList.aspx" onclick="ShowProgress();">Territory</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/DistrictList.aspx" onclick="ShowProgress();">District</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/TownList.aspx" onclick="ShowProgress();">Town</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/SalesForceList.aspx" onclick="ShowProgress();">
                        Field Force</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/StockistList.aspx" onclick="ShowProgress();">Stockist
                        Details</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/DSMList.aspx" onclick="ShowProgress();">DSM Creation</a></li>
                    <%--  <li><a href="../../../../ E-Report_DotNet/MasterFiles/MR/Territory/Territory.aspx"
                                onclick="ShowProgress();">Territory</a></li>--%>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/Territory/Territory.aspx" onclick="ShowProgress();">
                        <asp:Label ID="lblTerritory" Text="Route" runat="server"></asp:Label></a></li>
                    <%-- <li><a href="../../../../ E-Report_DotNet/MasterFiles/MR/Chemist/ChemistList.aspx"
                                onclick="ShowProgress();">Chemist</a></li>
                            <li><a href="../../../../ E-Report_DotNet/MasterFiles/MR/Hospital/HospitalList.aspx"
                                onclick="ShowProgress();">Hospital</a></li>--%>
                    <%--  <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx"
                                onclick="ShowProgress();">Unlisted Customer</a></li>--%>
                    <%-- <li><a href="#">Stockist Details &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/StockistList.aspx" onclick="ShowProgress();">
                                Add/Edit/DeActivate</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Stockist_Sale.aspx" onclick="ShowProgress();">
                                FieldForce Stockist Entry</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Stockist_View.aspx" onclick="ShowProgress();">
                                Stockist View</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/Sec_Sales_Stockiest.aspx"
                                onclick="ShowProgress();">Entry Status</a></li>
                            <li><a href="../../../../ E-Report_DotNet/MasterFiles/Stockist_HQ_Map.aspx" onclick="ShowProgress();">
                                HQ Map</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/PoolName_List.aspx" onclick="ShowProgress();">
                                Pool Area Name Creation</a></li>
                        </ul>
                    </li>--%>
                    <li><a href="#">Retailer &raquo;</a>
                        <ul>
                            <%-- <li><a href="../../../../E-Report_DotNet/MasterFiles/DoctorCategoryList.aspx" onclick="ShowProgress();">
                                Category</a></li>--%>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/DoctorSpecialityList.aspx" onclick="ShowProgress();">
                                Channel</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/DoctorClassList.aspx" onclick="ShowProgress();">
                                Class</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" onclick="ShowProgress();">
                                Retailer Creation</a></li>
                            <%--  <li><a href="../../../../E-Report_DotNet/MasterFiles/DoctorCampaignList.aspx" onclick="ShowProgress();">
                                Campaign</a></li>--%>
                            <%-- <li><a href="../../../../ E-Report_DotNet/MasterFiles/DoctorQualificationList.aspx"
                                onclick="ShowProgress();">Qualification</a></li>
                            <li><a href="../../../../ E-Report_DotNet/MasterFiles/ChemistCategoryList.aspx" onclick="ShowProgress();">
                                Chemists Category</a></li>--%>
                        </ul>
                    </li>
                    <%--  <li><a href="../../../../E-Report_DotNet/MasterFiles/ProductReminderList.aspx" onclick="ShowProgress();">
                        Input</a></li>--%>
                    <%-- <li><a href="../../../../E-Report_DotNet/MasterFiles/HolidayList.aspx" onclick="ShowProgress();">Statewise
                        - Holiday Fixation</a></li>--%>
                     <%--<li><a href="#">Expense &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Distance_Fixation.aspx" onclick="ShowProgress();">
                                SFC Updation</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/Distance_fixation_view.aspx" onclick="ShowProgress();">
                                SFC View</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/AllowanceFixation.aspx" onclick="ShowProgress();">
                                Allowance Fixation</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/WrkTypeWise_Allowance.aspx" onclick="ShowProgress();">
                                Wrk Type Wise - Allowance Fix</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/FVExpense_Parameter.aspx" onclick="ShowProgress();">
                                Fixed/Variable Expense Parameter</a></li>
                        </ul>
                    </li>--%>
                </ul>
            </li>
            <li><a href="#">Activities &raquo;</a>
                <ul>
                    <%-- <li><a href="#">Std. Daywise Plan &raquo;</a>
                    <ul>
                        <li><a href="../../../../ E-Report_DotNet/MasterFiles/MR_StdDayPlan.aspx" onclick="ShowProgress();">
                            Entry</a></li>
                        <li><a href="#">View</a></li>
                    </ul>
                </li>--%>
                    <li><a href="#">Approvals &raquo;</a>
                        <ul>
                             <%--<li><a href="../../../../E-Report_DotNet/MasterFiles/MGR/Listeddr_admin_Approve.aspx" onclick="ShowProgress();">
                                Listed Retailer Addition</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/MGR/Listeddr_adm_deAct_Approve.aspx" onclick="ShowProgress();">
                                Listed Retailer Deactivation</a></li>--%>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/MGR/TP_Calendar_Approve.aspx" onclick="ShowProgress();">
                                TP</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/DCR_Admin_Approval.aspx" onclick="ShowProgress();">
                                DCR</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Leave_Admin_Approval.aspx" onclick="ShowProgress();">
                                Leave</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/rptAutoexpense_Approve.aspx" onclick="ShowProgress();">
                                Expense</a></li>
                        </ul>
                    </li>
                   <%--<li><a href="#">&nbsp;Customer Business &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/DoctorBusinessEntry.aspx" onclick="ShowProgress();">
                                Entry</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/DoctorBusinessView.aspx" onclick="ShowProgress();">
                                View</a></li>
                         <li><a href="../../../../ E-Report_DotNet/MasterFiles/ActivityReports/DoctorBusinessStatus.aspx"
                                onclick="ShowProgress();">Status</a></li>
                        </ul>
                    </li>--%>
                    <li><a href="#">Sample Despatch &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/SampleDespatchHQ.aspx" onclick="ShowProgress();">
                                From HQ</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/SampleDespatchHQView.aspx" onclick="ShowProgress();">
                                View</a> </li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/SampleDespatchHQStatus.aspx"
                                onclick="ShowProgress();">Status</a> </li>
                        </ul>
                    </li>
                    <%--<li><a href="#">Input Despatch &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/InputDespatchHQ.aspx" onclick="ShowProgress();">
                                From HQ</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/InputDespatchHQView.aspx" onclick="ShowProgress();">
                                View</a> </li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/InputDespatchHQStatus.aspx"
                                onclick="ShowProgress();">Status</a> </li>
                        </ul>
                    </li>--%>
                    <li><a href="#">Target Fixation &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/TargetFixationProduct.aspx"
                                onclick="ShowProgress();">Productwise</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/ActivityReports/TargetFixationView.aspx" onclick="ShowProgress();">
                                View</a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li><a href="#">Activity Reports</a>
                <ul>
           <%--<li><a href="../../../../ E-Report_DotNet/MasterFiles/User_List.aspx" onclick="ShowProgress();">
                    User List</a></li>
                     <li><a href="#">
                        <asp:Label ID="lblRoute" Text="Route Plan" runat="server"></asp:Label>
                        &raquo;</a>
                       <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">
                                Status</a></li>
                        </ul>
                    </li>--%>
                    <li><a href="#">TP &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">
                                Status</a></li>
                        </ul>
                    </li>
                    <li><a href="#">DCR &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/DCR_View.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/DCR_Status.aspx" onclick="ShowProgress();">
                                Status</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/DCR_NotApprove.aspx" onclick="ShowProgress();">
                                Not Approved</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/DCR_NotSubmit.aspx" onclick="ShowProgress();">
                                Not Submitted</a></li>
                            <%--<li><a href="../../../../MIS Reports/Delayed_DCR_Status.aspx" onclick="ShowProgress();">
                                Delayed Status</a></li>--%>
                        </ul>
                    </li>
                     <%--<li><a href="../../../../E-Report_DotNet/MasterFiles/DashBoard/TreeView.aspx" onclick="ShowProgress();">
                        Dash Board</a></li>
                   <li><a href="#">Expense Statement &raquo;</a>
                    <ul>
                        <li><a href="#">View/Approval</a></li>
                        <li><a href="#">Status</a></li>
                    </ul>
                </li>
                   <li><a href="#">Doctor Details &raquo;</a>
                    <ul>
                        <li><a href="../../../../ E-Report_DotNet/MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">
                            View</a></li>
                    </ul>
                </li>
                <li><a href="../../../../ E-Report_DotNet/MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">
                    Fieldforce Status</a></li>--%>
                </ul>
            </li>
            <li><a href="#">MIS Reports</a>
                <ul>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/DashBoard/State_Count.aspx" onclick="ShowProgress();">
                        State View - Status</a></li>
                    <li><a href="../../../../MIS Reports/Secondary_Sales_View.aspx" onclick="ShowProgress();">
                        Sec.Sale View</a></li>
                    <li><a href="#">Purchase &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Purchase_Register_Distributor_wise.aspx" onclick="ShowProgress();">
                                Distributor-wise</a></li>
                            <li><a href="../../../../MIS Reports/statewise_purchase_report.aspx" onclick="ShowProgress();">
                                State-wise</a></li>
                            <li><a href="../../../../MIS Reports/Lost_Purchase.aspx" onclick="ShowProgress();">Lost
                                Purchase</a></li>
                            <li><a href="../../../../MIS Reports/Trend_Analysis.aspx" onclick="ShowProgress();">
                                Trend Analysis</a></li>
                            <li><a href="../../../../MIS Reports/Top10_Exception.aspx" onclick="ShowProgress();">
                                Exception</a></li>
                        </ul>
                    </li>
                     <%--<li><a href="#">Analysis &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/DCR_Analysis.aspx" onclick="ShowProgress();">DCR</a>
                            </li>
                        </ul>
                    </li>--%>
                    <li><a href="#">Sale &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Sales_Distributor_Wise.aspx" onclick="ShowProgress();">
                                Distributor-wise</a></li>
                            <li><a href="../../../../MIS Reports/sales_statewise.aspx" onclick="ShowProgress();">
                                State-wise</a></li>
                            <li><a href="../../../../MIS Reports/Sales_lost_purchase.aspx" onclick="ShowProgress();">
                                Lost Sale</a></li>
                            <li><a href="../../../../MIS Reports/sales_trend_analysis.aspx" onclick="ShowProgress();">
                                Trend Analysis</a></li>
                            <li><a href="../../../../MIS Reports/Sales_Top10_Exception.aspx" onclick="ShowProgress();">
                                Exception</a></li>
                        </ul>
                    </li>
  <li><a href="#">Retail &raquo;</a>
                        <ul>
                       <li><a href="../../../../MIS Reports/retail_distributorwise.aspx" onclick="ShowProgress();">
                            Distributor-wise</a></li>
                           <li><a href="../../../../MIS Reports/retail_statewise.aspx" onclick="ShowProgress();">
                               State-wise</a></li>
                            <li><a href="../../../../MIS Reports/retail_lost_purchase.aspx" onclick="ShowProgress();">
                                Lost Retail</a></li>
                            <li><a href="../../../../MIS Reports/retail_trend_analysis.aspx" onclick="ShowProgress();">
                                Trend Analysis</a></li>
                            <li><a href="../../../../MIS Reports/retail_top10_exception.aspx" onclick="ShowProgress();">
                                Exception</a></li>
							<li><a href="../../../../MIS Reports/Secondary_Order_Report.aspx" onclick="ShowProgress();">
                                Secondary Order</a></li>
							<li><a href="../../../../MIS Reports/Route_Productivity_Report.aspx" onclick="ShowProgress();">
                                Route Productivity</a></li>
	<li><a href="../../../../MIS Reports/Distribution_Width.aspx" onclick="ShowProgress();">
                                Distribution Width</a></li>

                        </ul>
                    </li>
                    <li><a href="../../../../MIS Reports/primarysecondaryreport.aspx" onclick="ShowProgress();">
                        Primary Vs Secondary</a></li>
	<li><a href="../../../../MIS Reports/Order_Detail_View.aspx" onclick="ShowProgress();">
                        Order Detail</a></li>
                     <%--<li><a href="#">Manager Analysis &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/AnalysisReports/Mgr_Coverage.aspx" onclick="ShowProgress();">
                                HQ - Coveragewise</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/AnalysisReports/Coverage_Analysis.aspx" onclick="ShowProgress();">
                                Coverage Analysis</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/AnalysisReports/JointWk_Analysis.aspx" onclick="ShowProgress();">
                                Joint Workwise</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/AnalysisReports/FieldWork_Analysis.aspx" onclick="ShowProgress();">
                                FieldWork Manager - Analysis</a></li>
                        </ul>
                    </li>
 				
                    <li><a href="../../../../MIS Reports/MissedCallReport.aspx" onclick="ShowProgress();">
                        Missed Call</a></li>
                    <li><a href="../../../../MIS Reports/VisitDetail_Datewise.aspx" onclick="ShowProgress();">
                        Visit Detail - Datewise</a></li>
                   <ul>
                        <li><a href="#">Listed Doctorwise</a></li>
                    </ul>
                    <li><a href="#">Visit Details &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Visit_Details_Report.aspx" onclick="ShowProgress();">
                                Listed Customerwise</a></li>
                            <li><a href="../../../../MIS Reports/Visit_Details_Consolidated.aspx" onclick="ShowProgress();">
                                Consolidated</a></li>
                            <li><a href="../../../../MIS Reports/Visit_Details_BasedonVisit.aspx" onclick="ShowProgress();">
                                Based on Visit</a></li>
                            <li><a href="../../../../MIS Reports/Visit_Details_Basedonfield.aspx" onclick="ShowProgress();">
                                Based on Field</a></li>
                              <li><a href="#">Modewise</a></li>
                        <li><a href="#">Productwise</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/SecSalesReport.aspx" onclick="ShowProgress();">
                        Secondary Sales</a></li>
                    
                   <li><a href="#">Product Exposure &raquo;</a>
                    <ul>
                        <li><a href="#">Detailed View</a></li>
                        <li><a href="#">Speciality/Category Wise</a></li>
                    </ul>
                </li>
                    <li><a href="#">Campaign &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/ListeddrCamp_View.aspx" onclick="ShowProgress();">
                                View</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Reports/Campaign_View.aspx" onclick="ShowProgress();">
                                Status</a></li>
  <li><a href="../../../../MIS Reports/retail_distributorwise.aspx" onclick="ShowProgress();">
                            Distributor-wise</a></li>
                           <li><a href="../../../../MIS Reports/retail_statewise.aspx" onclick="ShowProgress();">
                               State-wise</a></li>
                        </ul>
                    </li>
                      <li><a href="#">Exception</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/TP_Deviation.aspx" onclick="ShowProgress();">
                               TP</a></li>
                      
                        </ul>
                    </li>
                    <li><a href="#">Product Exposure &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Product_Exp_Detail.aspx" onclick="ShowProgress();">
                                Analysis</a></li>
                            <li><a href="../../../../MIS Reports/Product_Exp_specat.aspx" onclick="ShowProgress();">
                                Speciality/Category Wise</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Sample Details &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/sampleproduct_details.aspx" onclick="ShowProgress();">
                                Fieldforce Wise</a></li>
                        </ul>
                    </li>
                    <li><a href="#">ListedCustomer - Productwise Visit&raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Territory_Format.aspx" onclick="ShowProgress();">
                                View</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Status &raquo;</a>
                        <ul>
                            <li><a href="../../../../MIS Reports/Delayed_DCR_Status.aspx" onclick="ShowProgress();">
                                Delayed</a></li>
                            <li><a href="../../../../MIS Reports/Leave_DCR_Status.aspx" onclick="ShowProgress();">
                                Leave</a></li>
                        </ul>
                    </li>--%>
                </ul>
            </li>
            <li><a href="#">Options</a>
                <ul>
                    <li><a href="../../../E-Report_DotNet/MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">
                        Change Password</a></li>
                     <%-- <li><a href="#">Vacant MR Login &raquo;</a>
                        <ul>
                            <li><a href="../../../E-Report_DotNet/MasterFiles/Options/Vacant_MR_Access.aspx" onclick="ShowProgress();">
                                Access</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Permission_MR.aspx" onclick="ShowProgress();">
                                Permission for Managers </a></li>
                        </ul>
                    </li>
                     <li><a href="#">Vacant MR Login - Access</a></li>
                    <li><a href="#">Update/Delete &raquo;</a>
                        <ul>
                            <li><a href="../../../E-Report_DotNet/MasterFiles/Options/TPEdit.aspx" onclick="ShowProgress();">TP
                                Edit</a></li>
                            <li><a href="../../../E-Report_DotNet/MasterFiles/Options/TPDelete.aspx" onclick="ShowProgress();">TP
                                Delete</a></li>
                            <li><a href="../../../E-Report_DotNet/MasterFiles/Options/DCREdit.aspx" onclick="ShowProgress();">DCR
                                Edit</a></li>
                            <li><a href="../../../E-Report_DotNet/MasterFiles/Options/SecSales_Edit.aspx" onclick="ShowProgress();">
                                SS Edit</a></li>
                             <li><a href="#">DCR Delete</a></li>
                            <li><a href="../../../E-Report_DotNet/MasterFiles/Options/MailView.aspx" onclick="ShowProgress();">Mail
                                Delete</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Basic Setup &raquo;</a>
                        <ul>
                            <li><a href="../../../ E-Report_DotNet/MasterFiles/AdminSetup.aspx" onclick="ShowProgress();">
                            TP/DCR</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/SetupScreen.aspx" onclick="ShowProgress();">
                                Screen Access Rights</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/AdminSetup.aspx" onclick="ShowProgress();">
                                Base Level</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/AdminSetupMGR.aspx" onclick="ShowProgress();">
                                Managers</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/HomePageRest.aspx" onclick="ShowProgress();">
                                Approval Mandatory</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Mgrwise_Core_Doc_Map.aspx" onclick="ShowProgress();">
                                Managerwise Core Customer Map</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Screenwise_Lock.aspx" onclick="ShowProgress();">
                                Screenwise Access</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Mail_Folder_Creation.aspx" onclick="ShowProgress();">
                                Mail Folder Creation</a></li>
                        </ul>
                    </li>--%>
                    <li><a href="#">App Setup &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/App_CallFeedback.aspx" onclick="ShowProgress();">
                                Call Feedback</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/App_CallRemarks.aspx" onclick="ShowProgress();">
                                Call Remarks Templates</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Notification_Message.aspx" onclick="ShowProgress();">
                                Notification Message</a></li>
                        </ul>
                    </li>
                    <%--<li><a href="#">Seconday Sale Setup &raquo;</a>
                        <ul>
                            <li><a href="../../../../SecondarySales/SetUp.aspx" onclick="ShowProgress();">SS Entry
                                - Setup 1</a></li>
                            <li><a href="../../../../SecondarySales/SecSalesSetUp.aspx" onclick="ShowProgress();">
                                SS Entry - Setup 2</a></li>
                            <li><a href="../../../../SecondarySales/CustomizedColumn.aspx" onclick="ShowProgress();">
                                SS Entry - Customized Formula
                                <br />
                                Creation</a></li>
                        </ul>
                    </li>--%>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">
                        Mail Box</a></li>
                    <li><a href="#">Information Upload &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/FlashNews.aspx" onclick="ShowProgress();">
                                Flash News</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/NoticeBoard.aspx" onclick="ShowProgress();">
                                Notice Board</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Quote.aspx" onclick="ShowProgress();">Quote
                                for the Week</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/TalktoUs.aspx" onclick="ShowProgress();">
                                Talk to Us</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/FileUpload.aspx" onclick="ShowProgress();">
                                File Upload</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Usermanual_Upload.aspx" onclick="ShowProgress();">
                                User Manual Upload</a></li>
                        </ul>
                    </li>
                    <%-- <li><a href="#">Upload &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/ListedDr_Upload.aspx" onclick="ShowProgress();">
                                Listed Customer</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Listeddr_BulkUpload.aspx" onclick="ShowProgress();">
                                Listed Customer - Bulk</a></li>
                           <li><a href="../../../../ E-Report_DotNet/MasterFiles/Options/Chemists_Upload.aspx"
                                onclick="ShowProgress();">Chemist</a></li>
                            <li><a href="../../../../ E-Report_DotNet/MasterFiles/Options/Chemists_BulkUpload.aspx"
                                onclick="ShowProgress();">Chemist - Bulk</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Salesforce_Upload.aspx" onclick="ShowProgress();">
                                Field Force</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Stockiest_Upload.aspx" onclick="ShowProgress();">
                                Stockist</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Product_Upload.aspx" onclick="ShowProgress();">
                                Product</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Primary_Upload.aspx" onclick="ShowProgress();">
                                Primary Upload</a></li>
                              <li><a href="#">Unlisted Customer</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Image Upload &raquo;</a>
                        <ul>
                             <li><a href="../../../../ E-Report_DotNet/MasterFiles/Options/Loginpage_ImgUpload.aspx"
                            onclick="ShowProgress();">Login Page</a></li> 
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Homepage_ImgUpload.aspx" onclick="ShowProgress();">
                                Home Page(Common For All)</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/HomePage_FieldForcewise.aspx">Home Page(FieldForcewise)</a></li>
                        </ul>
                    </li>--%>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">
                        Leave Status</a></li>
                  <%--<li><a href="../ E-Report_DotNet/MasterFiles/Quote_Design.aspx">Design</a></li>
                    <li><a href="#">Transfers &raquo;</a>
                        <ul>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/MR_MR_Transfer.aspx" onclick="ShowProgress();">
                                Transfer - Master Details</a></li>
                            <li><a href="../../../ E-Report_DotNet/MasterFiles/Options/testing.aspx"
                            onclick="ShowProgrss();">Transfer - Master Details</a></li>
                            <li><a href="#">Stockist With Sale</a></li>
                            <li><a href="../../../../E-Report_DotNet/MasterFiles/MGR/Convert_Unlistto_Listeddr.aspx" onclick="ShowProgress();">
                                Convert Unlisted Customers - Listed Customers</a></li>
                        </ul>
                    </li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/Options/Delayed_Release.aspx" onclick="ShowProgress();">
                        Release - Missing Dates / Delay </a></li>
                    <li><a href="#">Release &raquo;</a>
                    <ul>
                        <li><a href="#">DCR Lock</a></li>
                        <li><a href="#">TP Lock</a></li>
                        <li><a href="#">Std.Daywise Lock</a></li>
                    </ul>
                </li>
                      <li><a href="#">Setup</a></li>--%>
                </ul>
            </li>
            <li><a href="../../../../E-Report_DotNet/Index.aspx" class="first" onclick="ShowProgress();">Logout</a>
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
         <img src="../Images/loader.gif" runat="server" alt="" />
    </div>
</div>