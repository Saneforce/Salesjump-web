<%@ Page Title="Admin Setup" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="AdminSetup.aspx.cs" Inherits="MasterFiles_AdminSetup" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        .spc
        {
            padding-left: 5%;
        }
        .spc1
        {
            padding-left: 10%;
        }
        
         .box{
    background:#FFFFFF;
    border:5px solid #427BD6;
    border-radius:8px;
}

.tableHead{
    background:#e0f3ff;
    color:black;
    border-style:solid; 
    border-width: 1px;
    border-color: #427BD6;

}
.break  {
  height: 10px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>

         <center >
          <table align ="center" >
           <tr align="center"> 
             <td align ="center">
                <span runat="server" id="levelset" style="font-weight:bold; text-decoration:underline; border-style:none; font-family:Verdana; font-size:14px; border-color:#E0E0E0; color :#8A2EE6">Base Level S<asp:LinkButton ID="lnk" ForeColor="#8A2EE6" runat="server" Text="e" onclick="lnk_Click"></asp:LinkButton>tup</span>
             </td>
           </tr>
          </table>
          </center> 
         <br />
        <br />
        <table border="1" width="90%" align="center" style="margin-left: 7%" class="box">
            <tr>
                <td width="48%" class ="tableHead">
                <br />
                    <table border="1" width="100%">
                        <tr>
                            <td>
                               <asp:Label ID="Label1" runat="server" Text="PLAN SETUP" Font-Underline="True" Width ="48%" BackColor="skyblue" ForeColor="Red"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="14px"> </asp:Label>
                            </td>
                        </tr>
                        <tr class="spc">
                            <td class="spc">
                                <asp:Label ID="Label2" runat="server" Text="Customer with Multiple Plan(s) Allowed "
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoMultiDRYes" Enabled="false" runat="server" Text="Yes" Font-Bold="true" GroupName="MultiDR" />
                                <asp:RadioButton ID="rdoMultiDRNo" Enabled="false" runat="server" Text="No" Font-Bold="true" GroupName="MultiDR" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblWorkAreaName" runat="server" Text="Working Area Name" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWorkingAreaList" SkinID="ddlRequired" runat="server">
                                          <asp:ListItem Value="0">---Select---</asp:ListItem>
                                <asp:ListItem Value="1">Std. Daywise Plan</asp:ListItem>
                                <asp:ListItem Value="2">Std. Work Plan</asp:ListItem>
                                <asp:ListItem Value="3">Route Plan</asp:ListItem>
                                <asp:ListItem Value="4">Patches</asp:ListItem>
                                <asp:ListItem Value="5">Territory</asp:ListItem>
                                <asp:ListItem Value="6">Clusters</asp:ListItem>
                                <asp:ListItem Value="7">Sub Area</asp:ListItem>
                                <asp:ListItem Value="8">Permanent Journey Plan</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblNoofTourPlan" runat="server" Text="No.of Tour Plan view" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoofTourPlan" runat="server" MaxLength="3" Width="40" 
                                    SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                           <td class="break"></td>
                        </tr>
                        <tr >
                        <td >
                        </td>
                        </tr>
                        <tr>
                            <td>
                                <span runat="server" id="Span1" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label3" runat="server" Text="DCR SETUP" Font-Underline="True" Font-Bold="True" Width="48%" BackColor="SkyBlue" 
                                    Font-Names="Arial" Font-Size="14px"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:RadioButton ID="rdoDCRTP" runat="server" Text="Tour Plan Based DCR" AutoPostBack="true" GroupName="TP" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <asp:UpdatePanel ID="updpnlDesign" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="False" Width="55%"
                                            CellPadding="2" CellSpacing="2" GridLines="None" Font-Names="Arial">
                                            <Columns>
                                            <asp:TemplateField >
                                                 <ItemTemplate >
                                                    <asp:Label ID="appr" runat ="server" text ="Approval Needed for"  Font-Names="Arial" ></asp:Label>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" ForeColor ="#8B0000"  Text='<% #Eval("Designation_Short_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" Text="  Yes"  Font-Bold="true"  Width="40px" runat="server" Style="margin-left: 40px"
                                                            AutoPostBack="true" OnCheckedChanged="chkId_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkNo" Text="  No"  Font-Bold="true"  Width="40px" runat="server" AutoPostBack="true"
                                                            OnCheckedChanged="chkNo_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:RadioButton ID="rdoDCRWTP" runat="server" AutoPostBack="true" Text="Without Tour Plan Based DCR"
                                    GroupName="TP" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label4" runat="server" Text="No.of Listed Customers Allowed For DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDRAllow" runat="server" MaxLength="3" Width="40" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label5" runat="server" Text="No.of Chemists Allowed For DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChemAllow" runat="server" MaxLength="3" Width="40" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label24" runat="server" Text="No.of Stockists Allowed For DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStkAllow" runat="server" MaxLength="3" Width="40" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label6" runat="server" Text="No.of UnListed Customers Allowed For DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUNLAllow" runat="server" MaxLength="3" Width="40" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label15" runat="server" Text="No.of Hospitals Allowed For DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHosAllow" runat="server" MaxLength="3" Width="40" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label7" runat="server" Text="Customers listing display in DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoDCRNone" runat="server" Text="None" GroupName="DCREntry"
                                    Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoDCRSVLNo" runat="server" Text="SVL No" GroupName="DCREntry" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoDCRSpeciality" runat="server" Text="Speciality" GroupName="DCREntry" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoDCRCategory" runat="server" Text="Category" GroupName="DCREntry" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoClass" runat="server" Text="Class" GroupName="DCREntry" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoCampaign" runat="server" Visible="false" Text="Campaign"
                                    GroupName="DCREntry" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <span runat="server" id="Span2" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label33" runat="server" Text="DCR Approval System" Font-Underline="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="12px" Width ="48%" BackColor="skyblue"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:RadioButton ID="rdoAprYes" runat="server" Text="Needed" GroupName="APRVL" />
                                <asp:RadioButton ID="rdoAprNo" runat="server" Text="Not Needed" GroupName="APRVL" />
                            </td>
                        </tr>
                         <tr>
                          <td class="break"></td>
                        </tr>
                        <tr>
                            <td>
                                 <span runat="server" id="Span3" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label27" runat="server" Text="DCR Delayed System" Font-Underline="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="12px" Width ="48%" BackColor="skyblue"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:RadioButton ID="rdoDlyYes" runat="server" Text="Needed" GroupName="DLY" />
                                <asp:RadioButton ID="rdoDlyNo" runat="server" Text="Not Needed" GroupName="DLY" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label29" runat="server" Text="Week Off / Holiday Calculated in Delayed" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoDlyHoliday" runat="server" Font-Bold="true" Text="Yes" GroupName="DLYHLD" />
                                <asp:RadioButton ID="rdoDlyHolidayNo" runat="server" Font-Bold="true" Text="No" GroupName="DLYHLD" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label28" runat="server" Text="No.of Days Allowed For Delay" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoDaysDly" runat="server" MaxLength="3" Width="40" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                          <td class="break"></td>
                        </tr>
                        <tr>
                            <td>
                                 <span runat="server" id="Span4" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label30" runat="server" Text="DCR Auto Post" Font-Underline="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="12px" Width ="48%" BackColor="skyblue"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label31" runat="server" Text="Holiday" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoAutoHldYes" runat="server" Font-Bold="true" Text="Yes" GroupName="APHLD" />
                                <asp:RadioButton ID="rdoAutoHldNo" runat="server" Font-Bold="true" Text="No" GroupName="APHLD" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label32" runat="server" Text="Week Off" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoAutoSunYes" runat="server" Font-Bold="true" Text="Yes" GroupName="APSun" />
                                <asp:RadioButton ID="rdoAutoSunNo" runat="server" Font-Bold="true" Text="No" GroupName="APSun" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tableHead">
                <br />
                    <table border="1" width="70%">
                        <tr>
                            <td>
                                <span runat="server" id="Span5" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label8" runat="server" Text="DCR - ENTRY SETUP" Font-Bold="true"
                                    Font-Underline="True" Font-Names="Arial" Font-Size="14px" Width ="60%" BackColor="skyblue"> </asp:Label> 
                                  </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label9" runat="server" Text="No.of Characters Allowed For Remarks"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFFRemarks" runat="server" SkinID="MandTxtBox" Width="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label10" runat="server" Text="Maximum Product Selection Allowed For DCR Entry"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFFProd" runat="server" SkinID="MandTxtBox" Width="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label26" runat="server" Text="No. of Products Selection Mandatory For DCR Entry" Width="300px"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaxProd" runat="server" SkinID="MandTxtBox" Width="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label11" runat="server" Text="Remove Session in DCR Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoFFDCRYes" runat="server" Font-Bold="true" Text="Yes" GroupName="DCREntryFF" />
                                <asp:RadioButton ID="rdoFFDCRNo" runat="server" Font-Bold="true" Text="No" GroupName="DCREntryFF" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label12" runat="server" Text="Remove Time in DCR Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoFFDCRTimeYes" runat="server" Font-Bold="true" Text="Yes" GroupName="DCREntryFFTime" />
                                <asp:RadioButton ID="rdoFFDCRTimeNo" runat="server" Font-Bold="true" Text="No" GroupName="DCREntryFFTime" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label17" runat="server" Text="Is Session Mandatory in DCR Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoSessMYes" runat="server" Font-Bold="true" Text="Yes" GroupName="DCREntrySessM" />
                                <asp:RadioButton ID="rdoSessMNo" runat="server" Font-Bold="true" Text="No" GroupName="DCREntrySessM" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label25" runat="server" Text="Is Time Mandatory in DCR Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoTimeMYes" runat="server" Font-Bold="true" Text="Yes" GroupName="DCREntryTimeM" />
                                <asp:RadioButton ID="rdoTimeMNo" runat="server" Font-Bold="true" Text="No" GroupName="DCREntryTimeM" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label13" runat="server" Text="After Product Selection Qty Value 0"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoFFDCRQtyYes" runat="server" Font-Bold="true" Text="Yes" GroupName="DCREntryFFQty" />
                                <asp:RadioButton ID="rdoFFDCRQtyNo" runat="server" Font-Bold="true" Text="No" GroupName="DCREntryFFQty" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label14" runat="server" Text="UnListed Customer Entry Needed" Font-Names="Arial" Visible="false"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoFFUNLYes" runat="server" Text="Yes" Font-Bold="true" Visible="false" GroupName="DCREntryFFUNL" />
                                <asp:RadioButton ID="rdoFFUNLNo" runat="server" Text="No" Font-Bold="true" Visible ="false" GroupName="DCREntryFFUNL" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label34" runat="server" Text="Is Customer-wise Remarks Mandatory" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoDRYes" runat="server" Text="Yes" Font-Bold="true" GroupName="DCREntryDRYes" />
                                <asp:RadioButton ID="rdoDRNo" runat="server" Text="No" Font-Bold="true" GroupName="DCREntryDRYes" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label35" runat="server" Text="Allow to Enter New Chemist in DCR Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoCheYes" runat="server" Text="Yes" Font-Bold="true"  GroupName="DCREntryCheYes" />
                                <asp:RadioButton ID="rdoCheNo" runat="server" Text="No" Font-Bold="true"  GroupName="DCREntryCheYes" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label36" runat="server" Text="Allow to Enter New Un-Listed Customer in DCR Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoUnYes" runat="server" Text="Yes" Font-Bold ="true" GroupName="DCREntryUnYes" />
                                <asp:RadioButton ID="rdoUnNo" runat="server" Text="No" Font-Bold ="true" GroupName="DCREntryUnYes" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label151" runat="server" Text="POB Entry Option Needed" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoFFPOBYes" runat="server" Font-Bold ="true" Text="Yes" GroupName="DCREntryFFPOB" />
                                <asp:RadioButton ID="rdoFFPOBNo" runat="server" Font-Bold ="true" Text="No" GroupName="DCREntryFFPOB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc1">
                                <asp:RadioButton ID="rdopobprod" runat="server" Text="Productwise  POB" GroupName="grpPOB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc1">
                                <asp:RadioButton ID="rdopobdoc" runat="server" Text="Customerwise  POB" GroupName="grpPOB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc1">
                                <asp:RadioButton ID="rdopobdocrx" runat="server" Text="Productwise  Rx" GroupName="grpPOB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label16" runat="server" Text="Half day Work Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoFFDayYes" runat="server" Text="Yes" Font-Bold ="true" GroupName="DCREntryFFDay" />
                                <asp:RadioButton ID="rdoFFDayNo" runat="server" Text="No" Font-Bold ="true" GroupName="DCREntryFFDay" />
                            </td>
                        </tr>

                        <tr>
                          <td class="break">
                          </td>
                        </tr>
                        <tr>
                          <td class="break">
                          </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label17" runat="server" Text="Others Entry Needed" Font-Names="Arial" Font-Size="12px">
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoFFOtherYes" runat="server" Text="Yes" GroupName="DCREntryFFOther" />
                                <asp:RadioButton ID="rdoFFOtherNo" runat="server" Text="No" GroupName="DCREntryFFOther" />
                            </td>
                        </tr>
                        --%>
                        <tr>
                            <td>
                                 <span runat="server" id="Span6" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label18" runat="server" Text="RETAILER SETUP" Font-Underline="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="14px" Width ="60%" BackColor="skyblue"> </asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label19" runat="server" Text="No.of Listed Customers Allowed For Entry in Master"
                                    Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDRAllowed" runat="server" SkinID="MandTxtBox" Width="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label37" runat="server" Text="Listed Customer Approval Needed" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdodocYes" runat="server" Font-Bold="true"  Text="Yes" GroupName="DocApp" />
                                <asp:RadioButton ID="rdodocNo" runat="server" Font-Bold="true"  Text="No" GroupName="DocApp" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lbldeact" runat="server" Text="Listed Customer Deactivation Approval Needed"
                                    Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdodeactYes" runat="server" Text="Yes" Font-Bold="true"  GroupName="DocDeactApp" />
                                <asp:RadioButton ID="rdodeactNo" runat="server" Text="No" Font-Bold="true"  GroupName="DocDeactApp" />
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lbladddeact" runat="server" Text="Listed Customer Add against Deact Approval Needed"
                                    Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoadddeaYes" runat="server" Text="Yes" Font-Bold="true" GroupName="DocAddDeact" />
                                <asp:RadioButton ID="rdoadddeaNo" runat="server" Text="No" Font-Bold="true" GroupName="DocAddDeact" />
                            </td>
                        </tr>

                        <tr>
                          <td class="break">
                          </td>
                        </tr>
                        <tr>
                        <td class="break"></td>
                        </tr>
                        <tr>
                            <td>
                                <span runat="server" id="Span7" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label20" runat="server" Text="CHEMISTS SETUP" Font-Underline="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="14px" Width ="60%" BackColor="skyblue"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label21" runat="server" Text="No.of Chemists Allowed For Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChemAllowed" runat="server" SkinID="MandTxtBox" Width="40"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                         <td class="break"></td>
                        </tr>
                        <tr>
                            <td>
                                <span runat="server" id="Span8" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label22" runat="server" Text="STOCKISTS SETUP" Font-Underline="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="14px" Width ="60%" BackColor="skyblue"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="Label23" runat="server" Text="No.of Stockists Allowed For Entry" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStkAllowed" runat="server" SkinID="MandTxtBox" Width="40"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                         <td class="break"></td>
                        </tr>
                      <%--  <tr>
                            <td>
                            <span runat="server" id="Span10" style="font-weight:bold;  border-color:#FF0000; color :Red">
                                <asp:Label ID="Label42" runat="server" Text="TP APPROVAL SYSTEM" Font-Underline="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="14px" Width ="60%" BackColor="skyblue"> </asp:Label>
                                </span>
                                
                            </td>
                        </tr>--%>
                       
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px"
                Text="Save" OnClick="btnSubmit_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
                Text="Clear" />
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>