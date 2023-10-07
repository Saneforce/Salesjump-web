﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MR_TP_Menu.ascx.cs" Inherits="UserControl_MR_TP_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href='<%=ResolveClientUrl("../css/MR.css")%>' rel="stylesheet" type="text/css" />
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
    .style4
    {
        width: 47%;
    }
    .style5
    {
        width: 45%;
    }
     .under
{
   margin-top:2px;
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
          position:absolute;
            
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
     .modalBackground1
        {
            background-color: #3399FF;
            filter: alpha(opacity=45);
            opacity: 0.5;
        }
         .popup tr
        {
            background-color: LightBlue;
        }
        .popup 
        {
            background-color: #6699FF;
           
            position: absolute;
        
            top:0px;
            border: Gray 2px inset;
         
        }
        .mar_right
        {
            margin-right:10px;
            
        }
          .modalBackgroundNew
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopupNew
        {
            background-color: #FFFFFF;
            width: 350px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        .modalPopupNew .header
        {
            background-color: #2FBDF1;
            height: 20px;
            color: White;
            line-height: 20px;
            text-align: center;
            font-weight: bold;
            font-size:14px;
            font-family:Verdana;
        }
        .modalPopupNew .body
        {
            min-height: 120px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            padding:5px;
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
<%--<script type="text/javascript" language="javascript">
    window.onload = function () {
        noBack();
    }
    function noBack() {
        window.history.forward();
    }
</script>--%>
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
            Do you want to Continue?
               <span id="secondsIdle" style="visibility:hidden"></span>
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
    <table width="100%" >
        <tr>
            <td class="style1">
                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                    font-size: 14px; text-align: left; margin-top:0px" ForeColor="Blue" Font-Bold="True" Font-Names="Verdana" >
                </asp:Label>
            </td>
            <td>
            <asp:Panel ID="pnlQueries" runat="server" HorizontalAlign="Right"  CssClass="mar_right" >
 <asp:LinkButton ID="lnkQueries" runat="server" Text="Queries" ForeColor="Red" Font-Size="Medium" Font-Bold="true" ></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1"  runat="server" CancelControlID="btnCancel"
        PopupControlID="Panel2" TargetControlID="lnkQueries" BackgroundCssClass="modalBackgroundNew"></asp:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopupNew"  Style="display: none">
        <div class="header">
           Queries
            <asp:ImageButton ID="imgbtnclosegift" runat="server" ImageUrl="~/Images/Close.gif" ImageAlign="Right" />
        </div>
        <div class="body" >
        <table>
        <tr>
        <td>
        <asp:Label ID="lblprob" runat="server" SkinID="lblMand" Text="Queries File"></asp:Label>
      
           <asp:DropDownList ID="ddlProb" runat="server" SkinID="ddlRequired">
             <asp:ListItem Text="--Select the Problem--" Value="-1"></asp:ListItem>
            <asp:ListItem Text="DCR" Value="0">
            </asp:ListItem>
            <asp:ListItem Text="TP" Value="1"></asp:ListItem>            
            <asp:ListItem Text="Others" Value="2"></asp:ListItem>
            </asp:DropDownList>
            </td>
            </tr>
            <tr>
    
            <td >
                <asp:TextBox ID="txtQuery" runat="server" Width="330px" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right">
            <asp:Button ID="btnSend" runat="server" BackColor="LightBlue" Text="Send" OnClick="btnSend_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" BackColor="LightBlue"   Text="Cancel" />
            </td>
            </tr>
            </table>
        </div>
    </asp:Panel>

         </asp:Panel>
            </td>
        </tr>
    </table>
   </asp:Panel>


	<ul id="menu">
		<li><a href="../../../../Default_MR.aspx" onclick="ShowProgress()";>Home</a></li>
		<li><a href="#">Information &raquo;</a>
			<ul>
				<li><a href="../../../../MasterFiles/MR/ProductRate.aspx" onclick="ShowProgress();">Product Information</a></li>                
                <li><a href="../../../../MasterFiles/MR/Territory/Territory.aspx" onclick="ShowProgress();"><asp:Label ID="lblTerritory" Text="Territory" runat="server"></asp:Label></a></li>
                <li><a href="#">Listed Customer Details &raquo;</a>
                <ul>                
                <li><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" onclick="ShowProgress();">Entry</a></li>
                <li><a href="../../../../MasterFiles/MR/ListedDoctor/LstDoctorView.aspx" onclick="ShowProgress();">View</a></li>
                </ul>
                </li>
                <li><a href="#">Chemist Details &raquo;</a>
                <ul>
                <li><a href="../../../../MasterFiles/MR/Chemist/ChemistList.aspx" onclick="ShowProgress();">Entry</a></li>
                 <li><a href="../../../../MasterFiles/MR/Chemist/Chemist_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                </ul>
                </li>
                <li><a href="#">Hospital Details &raquo;</a>
                <ul>
                <li><a href="../../../../MasterFiles/MR/Hospital/HospitalList.aspx" onclick="ShowProgress();">Entry</a>               
                </li>
                 <li><a href="../../../../MasterFiles/MR/Hospital/Hospital_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                </ul>                
                </li>
                <li><a href="#">Unlisted Customer Details &raquo;</a>
                <ul>
                <li><a href="../../../../MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx" onclick="ShowProgress();">Entry</a></li>
                 <li><a href="../../../../MasterFiles/MR/UnListedDoctor/Unlisteddr_Terr_View.aspx" onclick="ShowProgress();">View</a></li>
                </ul>
                </li>
                  <li><a href="../../../../MasterFiles/MR/Doctor_Spec_List.aspx">Customer - Speciality List</a></li>
                <li><a href="../../../../MasterFiles/MR/HolidayList_MR.aspx" onclick="ShowProgress();">Holiday List</a></li>
                <li><a href="../../../../MasterFiles/MR/ListedDr_Approval_Pending.aspx" onclick="ShowProgress();">Lst.Customer - Approval Pending View</a></li>
    		</ul>
		</li>
		<li><a href="#">Activities &raquo;</a>
			<ul>
            <li><a href="#"><asp:Label ID="lblTerritory1" Text="Territory" runat="server"></asp:Label>  &raquo;</a>
					<ul >
                <li><a href="../../../../MasterFiles/MR/ListedDoctor/RoutePlan.aspx" onclick="ShowProgress();">Normal</a></li>
                <li><a href="../../../../MasterFiles/MR/ListedDoctor/RoutePlan_Catgwise.aspx">Classic (Categorywise)</a></li>
                  <li><a href="../../MasterFiles/Reports/rptRoutePlan.aspx" onclick="ShowProgress();">View</a></li>
                 <li><a href="../../MasterFiles/Reports/RoutePlan_Status.aspx" onclick="ShowProgress();">Status</a></li>
                
                </ul>
                </li>
 				<li><a href="#">Tour Plan &raquo;</a>
					<ul>
                        <li><a href="../../../../MasterFiles/MR/TourPlan.aspx" onclick="ShowProgress();">Entry</a></li>
						<li><a href="../../../../MasterFiles/MR/TourPlan.aspx" onclick="ShowProgress();">Edit</a></li>
						<li><a href="../../../../MasterFiles/Report/TP_View_Report.aspx" onclick="ShowProgress();">View</a></li>
                        <li><a href="../../../../MasterFiles/Report/TP_Status_Report.aspx" onclick="ShowProgress();">Status</a></li>
                        	<%--<li><a href="#">Status</a></li>--%>
                            <%--<li><a href="#">Deviation</a></li>--%>
                        
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
                <li>
                    <a href="#">Secondary Sales&raquo;</a>
					<ul>
                        <li>
                            <a href="../../../../MasterFiles/MR/SecSales/SecSalesEntry.aspx" onclick="ShowProgress();">Entry</a>
                        </li>                
                    </ul>
                </li>
                 <li><a href="../../../../MasterFiles/MR/RptAutoExpense.aspx" onclick="ShowProgress();">Expense Statement</a></li>
			</ul>				
		</li>
		<li><a href="#">MIS Reports &raquo;</a>
            <ul>
            <%--    <li><a href="../MasterFiles/MR_DCRAnalysis.aspx" onclick="ShowProgress();">DCR Analysis</a></li>--%>
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
                        <li><a href="../../../../MIS Reports/Visit_Details_Consolidated.aspx"
                            onclick="ShowProgress();">Consolidated</a></li>
                        <li><a href="../../../../MIS Reports/Visit_Details_BasedonVisit.aspx"
                            onclick="ShowProgress();">Based on Visit</a></li>
                        <li><a href="../../../../MIS Reports/Visit_Details_Basedonfield.aspx"
                            onclick="ShowProgress();">Based on Field</a></li>
                        <%--   <li><a href="#">Modewise</a></li>
                        <li><a href="#">Productwise</a></li>--%>
                    </ul>
                </li>
            </ul>

        </li>
		<li><a href="#">Options &raquo;</a>
            <ul>
                <li><a href="../../../../MasterFiles/Options/ChangePassword.aspx" onclick="ShowProgress();">Change Password</a></li>
               
                <li><a href="../../../../MasterFiles/Mails/Mail_Head.aspx" onclick="ShowProgress();">Mail Box</a></li>   
                  <li><a href="../../../../MasterFiles/MR/FileUpload_MR.aspx" onclick="ShowProgress();">Files Download</a></li>               
                  <li><a href="#">Leave &raquo;</a>
                  <ul>
                  <li><a href="../../../../MasterFiles/MR/LeaveForm.aspx" onclick="ShowProgress();">Entry</a></li>               
                  <li><a href="../../../../MasterFiles/MR/Leave_Status.aspx" onclick="ShowProgress();">Status</a></li>               
                         </ul>
                  </li>
                    <li><a href="../../../../MasterFiles/MR/Doctor-SubCategory-Map.aspx" onclick="ShowProgress();">Customer - Campaign Map</a></li>               
            </ul>
        </li>
		<li><a href="../../../E-Report_DotNet/Index.aspx" class="first" onclick="ShowProgress();">Logout</a></li>
	</ul>

 
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
                            <td align="center" class="style5" style="width:30%">
                            <asp:Label ID="lblHeading" runat="server" style="text-transform: capitalize;
                                font-size:15px; text-align: center;" ForeColor="Blue" 
                                Font-Bold="True" Font-Names="Verdana" CssClass="under">
                            </asp:Label>
                        </td>
                        
                        
                        
                            <td align ="right" class="style3" style="width:35%"  >                            
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

           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
           <img id="Img1" src="../Images/loader.gif" alt="" runat="server" />
        </div>   
</div>
<!--end wrapper-->
