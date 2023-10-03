<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BasicMenu.ascx.cs" Inherits="UserControl_BasicMenu" %>
<style type="text/css">
#horizontalmenu ul {
padding:0; margin:1; list-style:none; 
}
#horizontalmenu li {
float:left; position:relative; padding-right:100; display:block;
border:2px solid LightGray; 
background:LightGray;
width:13em;
}
#horizontalmenu li ul {
    display:none;
position:absolute;
}
#horizontalmenu li:hover ul{
    display:block;            
}
#horizontalmenu li ul li{
    clear:both;
    border-style:none;
    background-color:LightGray;
    width:10em;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
    }
#horizontalmenu li ul li a
{
    font-family:Times New Roman;
    font-size:10pt;
    font-style:normal;
    font-weight:normal;    
    padding-left:1em;
    color:Black;
}
#horizontalmenu li a
{    
    font-family:Times New Roman;
    font-size:11pt;
    font-style:normal;
    font-weight:normal;
    text-decoration:none;
    color:Black;    
    
}
</style>

<table width="95%">
    <tr>
        <td class="style1" >
            <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                    font-size: 12px; text-align: left;" ForeColor="#0000FF"
                    Font-Bold="True" Font-Names="Verdana">
            </asp:Label>
        </td>
            <td align="left" >
            <asp:Label ID="LblDiv" runat="server" Text="DivName" Style="text-transform: capitalize;
                    font-size: 12px; " ForeColor="#0000FF"
                    Font-Bold="True" Font-Names="Verdana">
            </asp:Label>
        </td>
        <td align="right">
                <asp:LinkButton ID="LBLogout" runat="server" CausesValidation="false" ToolTip="Logout" ForeColor="#0000FF" 
                    Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Small" onclick="lnkLogout_Click">Log Out</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">&nbsp;</td> 
    </tr>
    <tr>
        <td colspan="3">

            <div id="horizontalmenu" >
                    <ul> 
                        <li>
                            <a href="~/Default.aspx">Home</a>
                        </li>
                        <li>
                            <a href="#" style="border-style:inherit; border-bottom-width:thin; border-left-width:thin;border-top-width:thin;border-right-width:thin;">Master&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                            <ul> 
                                <li><a href="E-Report_DotNet/E-Report_DotNet/MasterFiles/MR/ProductRate.aspx">Product Rate</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR/Territory/Territory.aspx">Territory</a></li>
                                <li><a href="E-Report_DotNet/MasterFiles/MR/ListedDoctor/LstDoctorList.aspx">Listed Doctor Details</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_ChemistDetails.aspx">Chemist Details</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_StockistDetails.aspx">Stockist Details</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_HospitalHDetails.aspx">Hospital Details</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_UnListedDoctor.aspx">Unlisted Doctor</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_HolidayList.aspx">Holiday List</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_ListedDR_Pending.aspx">Listed Doctor - Approval Pending View</a></li> 
                            </ul>
                        </li>
                        <li>
                            <a href="#" style="border-style:inherit; border-bottom-width:thin; border-left-width:thin;border-top-width:thin;border-right-width:thin;">Activities&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                            <ul> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_StdDayPlan.aspx">Std. Daywise Plan</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_TourPlan.aspx">Tour Plan</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_DCR.aspx">DCR</a></li> 
                            </ul>
                        </li>
                        <li>
                            <a href="#" style="border-style:inherit; border-bottom-width:thin; border-left-width:thin;border-top-width:thin;border-right-width:thin;">MIS Reports&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                            <ul> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_DCRAnalysis.aspx">DCR Analysis</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_MissedCalls.aspx">Missed Calls</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_VisitDetails.aspx">Visit Details</a></li> 
                            </ul>
                        </li>
                        <li>
                            <a href="#">Options</a>
                            <ul> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_ChangePassword.aspx">Change Password</a></li> 
                                <li><a href="E-Report_DotNet/MasterFiles/MR_MailBox.aspx">Mail Box</a></li>                     
                            </ul>
                        </li>
                        <li>
                            <a href="~/Default.aspx">LogOut</a>
                        </li>

                    </ul> 
            </div>
        </td>
       </tr>
    </table>
    <br />
    <br />
    <asp:Panel ID="pnlHeader" runat="server"  
        BorderStyle="None" BackColor="#F0F8FF" Width="90%">
        <table width="100%" cellpadding ="0" cellspacing ="0" align="center">
            <tr>
                <td>               
                    <table id="tblpanel" runat="server" width="100%">
                        <tr>                           
                            <td valign="middle"  align="left" style="width:40%;">
                                <asp:Label ID="lblHeading" runat="server" CssClass="Headinglbl" 
                                    ForeColor="#0033CC"></asp:Label>
                            </td>    
                             <td valign="middle"  >
                                 <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" 
                                  ForeColor="#0033CC"></asp:Label>
                            </td>
                            
                            <td align ="right" >                            
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Text="Back" onClick="btnBack_Click"/>
                            </td>       
                        </tr>                       
                    </table>
                </td>
            </tr>
         </table>
           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </asp:Panel>
