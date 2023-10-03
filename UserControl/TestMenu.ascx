﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestMenu.ascx.cs" Inherits="UserControl_TestMenu" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href='<%=ResolveClientUrl("../css/Menu3.css")%>'  rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        width: 46%;
    }
    .menu
    {
        margin-top: 0px;
    }
</style>

<link rel="stylesheet" type="text/css" href='<%=ResolveClientUrl("../css/csshorizontalmenu.css")%>' />

<script type="text/javascript" src='<%=ResolveClientUrl("../JsFiles/csshorizontalmenu.js")%>'>
</script>


<div class="horizontalcssmenu" align="center">

  <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
  </asp:ToolkitScriptManager>
    <br />

    <table width="90%" border="0" cellpadding ="0" cellspacing ="0" align="center">
        <tr>
            <td class="style1" >
                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 12px; text-align: left;" ForeColor="Navy" 
                        Font-Bold="True" Font-Names="Verdana">
                </asp:Label>
            </td>
             <td align="left" >
                <asp:Label ID="LblDiv" runat="server" Text="DivName" Style="text-transform: capitalize;
                        font-size: 12px; " ForeColor="Navy"
                        Font-Bold="True" Font-Names="Verdana">
                </asp:Label>
            </td>
            <td align="right">
                 <asp:LinkButton ID="LBLogout" runat="server" CausesValidation="false" ToolTip="Logout" ForeColor="Navy" 
                     Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Small" onclick="lnkLogout_Click">Log Out</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">&nbsp;</td> 
        </tr>
        <tr>
            <td colspan="3">

                <ul id="cssmenu1">
                <li style="border-left: 1px solid #202020;"><a href="../Default.aspx">Home</a></li>
                <li><a href="#" >Master</a>
                    <ul>                    
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/ProductRate.aspx">Product List</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/Territory/Territory.aspx">Territory</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/ListedDoctor/LstDoctorList.aspx">Listed Customer Details</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/Chemist/ChemistList.aspx">Chemist Details</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/Hospital/HospitalList.aspx">Hospital Details</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/UnListedDoctor/UnLstDoctorList.aspx">Unlisted Customer</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/HolidayList_MR.aspx">Holiday List</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR_ListedDR_Pending.aspx">Listed Customer - Approval Pending View</a></li>
                    </ul>
                </li>
                <li><a href="#">Activities</a>
                    <ul>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_StdDayPlan.aspx">Std. Daywise Plan</a></li>
                    <li><a href="../../../../E-Report_DotNet/MasterFiles/MR/TPView.aspx">Tour Plan</a></li>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_DCR.aspx">DCR</a></li>
                    </ul>
                </li>
                <li><a href="#">MIS Reports</a>
                    <ul>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_DCRAnalysis.aspx">DCR Analysis</a></li>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_MissedCalls.aspx">Missed Calls</a></li>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_VisitDetails.aspx">Visit Details</a></li>
                    </ul>
                </li>
                <li><a href="#">Options</a>
                    <ul>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_ChangePassword.aspx">Change Password</a></li>
                    <li><a href="../E-Report_DotNet/MasterFiles/MR_MailBox.aspx">Mail Box</a></li>    
                    </ul>
                </li>
                 </ul>
                <br style="clear: left;" />
            </td>
        </tr>
    </table>
    <br />
    <br />
      
          <asp:Panel ID="pnlHeader" runat="server"  
        BorderStyle="None" Width="85%">
        <table width="100%" cellpadding ="0" cellspacing ="0" align="center">
            <tr>
                <td>               
                    <table id="tblpanel" runat="server" width="100%">
                        <tr>                           
                            <td valign="middle"  align="left" style="width:40%;">
                                <asp:Label ID="lblHeading" runat="server" CssClass="Headinglbl" 
                                    ForeColor="Navy"></asp:Label>
                            </td>    
                             <td valign="middle"  >
                                 <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" 
                                  ForeColor="Navy"></asp:Label>
                            </td>
                            <td align ="right" >                            
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Text="Back" onClick="btnBack_Click"/>
                            </td>       
                        </tr>                       
                    </table>
                </td>
            </tr>
         </table>
          <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BorderColor="Navy"
            Color="Navy" Radius="2" TargetControlID="pnlHeader">
        </asp:RoundedCornersExtender>
    </asp:Panel>
    
</div>

