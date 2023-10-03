<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MGR_TP_Menu.ascx.cs" Inherits="UserControl_MGR_TP_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href='<%=ResolveClientUrl("../css/MGR.css")%>' rel="stylesheet" type="text/css" />

<!-- IE6-8 support of HTML5 elements -->
<!--[if lt IE 9]>
	<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->

<style type="text/css">
    .style1
    {
        width: 46%;
    }
    .menu
    {
        margin-top: 0px;
    }
    .BUTTON
    {
    }
    .style3
    {
        width: 99px;
    }
    .style5
    {
        width: 45%;
    }
    .under
    {
        margin-top: 2px;
        text-decoration: underline;
    }
    .spc
    {
        padding-top:5px;
        padding-bottom:5px;
        padding-left:5px;
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
</style>


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
    
    <asp:Panel ID="pnldiv" runat="server" CssClass="spc">
    <table width="90%" border="0">
        <tr>
            <td class="style1">
                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                    font-size: 14px; text-align: left;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana">
                </asp:Label>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <nav id="nav">
	<ul id="menu">
		<li><a href="../../../../MGR_Home.aspx" class="first" onclick="ShowProgress();">Home</a></li>
		<li><a href="#">Information &raquo;</a>
			<ul>
				<li><a href="../../../../MasterFiles/MR/ProductRate.aspx" onclick="ShowProgress();">Product Information</a></li>
<%--                <li><a href="../../../../MasterFiles/MGR/Territory/Territory.aspx">Territory</a></li>
                <li><a href="../../../../MasterFiles/MGR/ListedDoctor/LstDoctorList.aspx">Listed Doctor Details</a></li>
                <li><a href="../../../../MasterFiles/MGR/Chemist/ChemistList.aspx">Chemist Details</a></li>
                <li><a href="../../../../MasterFiles/MGR/Hospital/HospitalList.aspx">Hospital Details</a></li>
                <li><a href="../../../../MasterFiles/MGR/UnListedDoctor/UnLstDoctorList.aspx">Unlisted Doctor</a></li>--%>
                <li><a href="../../../../MasterFiles/User_List.aspx" onclick="ShowProgress();">Subordinate Detail</a></li>      
                <li><a href="#">Master View &raquo;</a>
                <ul>
                <li><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">Listed Customer</a></li>
                <li><a href="../../../../MasterFiles/MR/Chemist/Chemist_Terr_View.aspx" onclick="ShowProgress();">Chemist</a></li>
                     <li><a href="../../../../MasterFiles/MR/Hospital/Hospital_Terr_View.aspx" onclick="ShowProgress();">Hospital</a></li>
                <li><a href="../../../../MasterFiles/MR/UnListedDoctor/Unlisteddr_Terr_View.aspx" onclick="ShowProgress();">Unlisted Customer</a></li>
           
                </ul>
                </li>         
 				<li><a href="#">Approvals &raquo;</a>
					<ul>
						<li><a href="../../../../MasterFiles/MGR/ListedDR_Add_Approve.aspx" onclick="ShowProgress();">
                            Listed Customer Addition</a></li>
						<li><a href="../../../../MasterFiles/MGR/ListedDR_DeActivate_Approve.aspx" onclick="ShowProgress();">Listed Customer DeActivation</a></li>
                        <li><a href="../../../../MasterFiles/MGR/TP_Approve.aspx" onclick="ShowProgress();">TP Approval</a></li>
                        <li><a href="../../../../MasterFiles/MGR/DCR_Approval.aspx" onclick="ShowProgress();">DCR Approval</a></li>
                        <li><a href="../../../../MasterFiles/MGR/Leave_Approval.aspx" onclick="ShowProgress();">Leave Approval</a></li>
                        
					</ul>						
				</li>
                <li><a href="../../../../MasterFiles/MR/Doctor_Spec_List.aspx" onclick="ShowProgress();">Doctor - Speciality List</a></li>
                 <li><a href="../../../../MasterFiles/MGR/HolidayList_MGR.aspx" onclick="ShowProgress();">Holiday List</a></li>
                 <li><a href="../../../../MasterFiles/MGR/Listeddr_App_Status.aspx" onclick="ShowProgress();">Listed Customer Approval Status</a></li>
                  <li><a href="../../../../MasterFiles/MGR/Convert_Unlistto_Listeddr.aspx" onclick="ShowProgress();">
                      Unlisted Customer Convert to Listed Customer</a></li>  
    		</ul>
		</li>
		<li><a href="#">Activities &raquo;</a>
			<ul>
                <li><a href="#">Route Plan &raquo;</a>
					<ul >
              
              <li><a href="../../../../MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">View</a></li>
                  <li><a href="../../../../MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">Status</a></li>
                
                </ul>
                </li>
 				<li><a href="#">Tour Plan &raquo;</a>
				<ul>
                    		
                    <li><a href="../../../../MasterFiles/MGR/TourPlan_Calen.aspx" onclick="ShowProgress();">Entry</a></li>
						<li><a href="../../../../MasterFiles/MGR/TP_Calendar_Edit.aspx" onclick="ShowProgress();">Edit</a></li>
						<li><a href="../../../../MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">View</a></li>
				       <li><a href="../../../../MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">Status</a></li>
                   
                        
                       <%-- <li><a href="../../../../MasterFiles/MGR/TP_Approval.aspx">Calendar View - Approval</a></li>--%>
					</ul>						
				</li>
                <li><a href="#">DCR &raquo;</a>
				<ul>
                <li><a href="../../../../DCR/DCR_Entry.aspx" onclick="ShowProgress();">Entry</a></li>
                 <li><a href="../../../../MasterFiles/Reports/DCR_View.aspx" onclick="ShowProgress();">
                            View</a></li>
                        <li><a href="../../../../MasterFiles/Reports/DCR_Status.aspx" onclick="ShowProgress();">
                            Status</a></li>
                </ul>
                </li>
			</ul>				
		</li>
		<li><a href="#">MIS Reports &raquo;</a>
            <ul>
  <%--              <li><a href="../MasterFiles/MR_DCRAnalysis.aspx">DCR Analysis</a></li>--%>
   <li><a href="#">Customer Details &raquo;</a>
                    <ul>
                        <li><a href="../../../../MasterFiles/Reports/DoctorList_Reportaspx.aspx" onclick="ShowProgress();">
                            View</a></li>
                    </ul>
                </li>
                <li><a href="../../../../MasterFiles/Reports/MR_Status_Report.aspx" onclick="ShowProgress();">
                    Fieldforce Status</a></li>
                <li><a href="../../../../MasterFiles/Reports/CallAverage.aspx" onclick="ShowProgress();">
                    Call Average View</a></li>
                <li><a href="../../../../MasterFiles/Reports/frmWorkTypeStatusView.aspx" onclick="ShowProgress();">
                    Work Type View Status </a></li>
                 <li><a href="../../../../MIS Reports/MissedCallReport.aspx" onclick="ShowProgress();">Missed Calls</a></li>
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
        </ul>
                  </li>
            </ul>
        </li>
		<li><a href="#">Options &raquo;</a>
            <ul>
                <li><a href="../../../../MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">Change Password</a></li>
                <li><a href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">Mail Box</a></li>   
                      <li><a href="../../../../MasterFiles/MR/FileUpload_MR.aspx">Files Download</a></li>
                      <li><a href="../../../../MasterFiles/MGR/Vacant_MR_Login.aspx">Vacant - MR Login Access</a></li>   
                        <li><a href="#">Leave &raquo;</a>
                  <ul>
                  <li><a href="../../../../MasterFiles/MGR/Leave_Form_Mgr.aspx" onclick="ShowProgress();">Entry</a></li>               
                  <li><a href="../../../../MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">Status</a></li>               
                  </ul>
                  </li>
            </ul>
        </li>
		<li><a href="../../../E-Report_DotNet/Index.aspx" class="first" onclick="ShowProgress();">Logout</a></li>
	</ul>
   
    <br /> 
  
       <asp:Panel ID="pnlHeader" runat="server" Width="100%" align="center">
        <table width="95%" cellpadding ="0" cellspacing ="0" align="center">
            <tr>
                <td>                   
                    <table id="tblpanel" runat="server" width="100%" border="1">
                        <tr>    
                          <td  style="width:35%" >
                                 <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" 
                                  ForeColor="Black" style="font-size:13px; text-align: center;" 
                                Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>                      
                         <td class="style5" align="center" style="width:30%">
                            <asp:Label ID="lblHeading" runat="server" style="text-transform: capitalize;
                                font-size:15px; text-align: center;" ForeColor="DarkGreen" CssClass="under" 
                                Font-Bold="True" Font-Names="Verdana">
                            </asp:Label>
                        </td>                             
                           
                            <td align ="right" class="style3" style="width:35%" >                           
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Height="25px" Width="70px" Text="Back" 
                                    onClick="btnBack_Click" />
                            </td>       
                        </tr>                       
                    </table>
                </td>
            </tr>
         </table>
         </asp:Panel> 
            <%--<asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BorderColor="DarkSlateGray" 
            Color="DarkSlateGray"       Radius="2" TargetControlID="pnlHeader">
        </asp:RoundedCornersExtender>--%>
         </nav>
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img id="Img1" src="../Images/loader.gif" alt="" runat="server" />
        </div>
        
</div>
<!--end wrapper-->
