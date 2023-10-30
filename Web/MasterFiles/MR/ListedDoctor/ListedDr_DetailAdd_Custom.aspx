<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"  CodeFile="ListedDr_DetailAdd_Custom.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_ListedDr_DetailAdd_Custom" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title>Listed Retailer Detail Add</title>

            <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
            <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
            <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
            <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
            <link href="/css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
                       
            <style type="text/css">
                #chkboxLocation label {
                    margin-bottom: 0px;
                }
                
                select {
                    width: 100%;
                    border: 1px solid #D5D5D5 !important;
                    padding: 6px 6px 7px !important;
                }
                select:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }
                .frmddl {
                    width: 200px !important;
                    height: 34px !important;
                    display: block;
                    padding: 6px 6px;
                    font-size: 14px;
                    line-height: 1.42857143;
                    color: #555;
                    background-color: #fff;
                    background-image: none;
                    border: 1px solid #ccc;  
                    border-radius: 4px;
                    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
                    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
                    -webkit-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
                    transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
                    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
                    /*padding:5px !important;*/
                }
               
                .ChbControl{
                    height:100px !important;
                    width:100% !important;
                    overflow-x:scroll;
                    overflow-y:scroll;
                }
                .text-wrap {
                    word-break: break-all;
                    width:100px !important;
                }
                .ms-options {
                    width: 19% !important;
                }

                .modal {
                    position: fixed;
                    top: 0;
                    left: 0;
                    background-color: black;
                    z-index: 99;
                    opacity: 0.8;
                   /* filter: alpha(opacity=80);
                    -moz-opacity: 0.8;*/
                    min-height: 100%;
                    width: 100%;
                }

                .loading {
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

                .space {
                    padding: 3px 3px;
                }

                .sp {
                    padding-left: 11px;
                }

                .marRight {
                    margin-right: 35px;
                }
            </style>    
        </head>
        <body>
            <form id="form1" runat="server">
                <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6 sub-header" style="float:left">
                                    <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana"
                                        Visible="true"></asp:Label>
                                    <table id="Table4" runat="server">                                        
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="btnBack" CssClass="button" Text="Back" Visible="false" runat="server" OnClick="btnBack_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div class="col-md-6 sub-header" style="float:right">
                                     <div class="col-md-2"  style="float: right;">
                                         <button type="reset" id="btngoback" class="btn btn-primary" value="Back">Back</button>
                                       <%-- <i class="fa fa-arrow-left btn btn-circle" id="btngoback" style="color: #3f7b96; box-shadow: 1px 1px 3px 2px grey;"></i> --%>                                                                                                 
                                    </div>   
                                     <div class="col-md-2" style="float: right;">
                                        <asp:Button ID="btnClear" CssClass="btn btn-primary btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                    </div>
                                    <div class="col-md-2" style="float: right;">
                                        <button type="button" class="btn btn-primary btnSaved" id="btnSaved">Save</button>
                                        <asp:Button ID="btnSave"  CssClass="btn btn-primary" runat="server" Text="Save" Visible="false" />
                                    </div>                                                         
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card-body" id="AddRetailer" style="height:600px;overflow-y:scroll">
                        <div class="row" style=" width:100% !important">
                            <div class="col-lg-12 sub-header">
                                <%--Retailer Add --%>                                
                                <%--<input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" onclick="../Retailer_Details.aspx" />--%>
                                <asp:HiddenField ID="hdnukey" runat="server"  value="" />
                                <asp:HiddenField ID="hdnbreedname" runat="server"  value="" />
                                <asp:HiddenField ID="divcode" runat="server"  value="" />
                            </div>
                            <br />
                            <div class="col-lg-12">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table id="Table3" runat="server" width="100%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="Label5" runat="server" CssClass="Statuslbl" 
                                                            ForeColor="Black" Style="font-size: 13px;text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width: 45%">
                                                        <asp:Label ID="Label6" Text="Listed Retailer Detail Add" runat="server" Visible="false" CssClass="under" 
                                                            Style="text-transform: capitalize;font-size: 14px; text-align: center;" ForeColor="#336277" Font-Bold="True" Font-Names="Verdana">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="right" class="style3" style="width: 55%">
                                                        <asp:Button ID="btnBack1" CssClass="BUTTON" Text="Back" Visible="false" runat="server" OnClick="btnBack_Click" />
                                                        <%--<asp:Button ID="Button1" runat="server" CssClass="BUTTON" Visible="false" Height="25px" Width="60px"
                                                            Text="Back"  />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <div id="Div1" runat="server" />
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Right" CssClass="marRight">
                                    <asp:Label ID="Label7" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
                                </asp:Panel>
                            </div>
                            <div class="col-lg-12" style="margin-top:-17px !important;">
                                <div class="col-lg-9" style="float:left;">                                   
                                     <table border="0" cellpadding="0" cellspacing="0" id="tblad" style="width:100%;">
                                            <tr>
                                                <td rowspan="" class="style65" align="left" style="background-color: #19a4c6; color: white;padding: 5px;font-weight:bold;">
                                                    &nbsp;Personal Profile&nbsp;                            
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table border="0"  id="tblDocCatDtls" style="width:100%;">
                                            <tbody>
                                               <tr>
                                                   <td>
                                                       <asp:Label ID="lblQual" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Retailer Code</asp:Label>
                                                   </td>
                                                   <td>
                                                       <asp:TextBox ID="Txt_id" runat="server" CssClass=" frmddl"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblDOB" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red"></span>Mobile No</asp:Label>
                                                   </td>
                                                   <td>
                                                       <asp:TextBox ID="txtMobile" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                           onfocus="this.style.backgroundColor='LavenderBlush'" CssClass=" frmddl"></asp:TextBox>
                                                   </td>
                                               </tr>

                                               <tr>
                                                   <td>
                                                       <asp:Label ID="lblName" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Name of Retailer</asp:Label>&nbsp;
                                                   </td>
                                                   <td>
                                                       <asp:TextBox ID="txtName" runat="server" Width="170px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                            onblur="this.style.backgroundColor='White'" CssClass=" frmddl">
                                                       </asp:TextBox>
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblDOW" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red"></span>Contact Person Name</asp:Label>
                                                   </td>
                                                   <td>
                                                       <%--<asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'" ReadOnly="true"
                                                           onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>   
                                                       <asp:CalendarExtender ID="Caldow" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOW"></asp:CalendarExtender>--%>
                                                       
                                                       <asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                           onfocus="this.style.backgroundColor='LavenderBlush'"  CssClass=" frmddl"></asp:TextBox>
                                                   </td>
                                               </tr>

                                               <tr>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="lblSpec" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Channel </asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:DropDownList ID="ddlSpec" runat="server" SkinID="ddlRequired" CssClass=" frmddl"></asp:DropDownList>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="Label3" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>GST No</asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:TextBox ID="salestaxno" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'" 
                                                           onfocus="this.style.backgroundColor='LavenderBlush'" CssClass=" frmddl"></asp:TextBox>
                                                   </td>
                                               </tr>

                                               <tr>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="lblCatg" runat="server" SkinID="lblMand" Font-Bold="True">Sales TaxNo</asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:TextBox ID="TinNO" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                           onfocus="this.style.backgroundColor='LavenderBlush'" CssClass=" frmddl"></asp:TextBox>
                                                       <%--<asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired"></asp:DropDownList>--%>
                                                       <%--<asp:ListBox ID="ddlCatg" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>
                                                       <select class=" poiinter frmddl" id="example-multiple-selected" multiple="multiple">
                                                           <%--<option value="0">---Select All--</option>--%>
                                                       </select>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="lblTerritory" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Route</asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:DropDownList ID="ddlTerritory" runat="server" SkinID="ddlRequired" CssClass=" frmddl"></asp:DropDownList>
                                                   </td>
                                               </tr>

                                               <tr>
                                                   <td class="stylespc" align="left">
                                                        <asp:Label ID="Label4" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>Credit Days</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="creditdays" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'"  CssClass=" frmddl"></asp:TextBox>
                                                    </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="lblClass" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Class </asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired" CssClass=" frmddl"></asp:DropDownList>
                                                   </td>
                                               </tr>
                                               
                                                <tr>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lbloutstanding" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>Outstanding</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtoutstanding" runat="server" SkinID="MandTxtBox"  onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal  frmddl"></asp:TextBox>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblcreditlimit" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>Credit Limit</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtcreditlimit" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass=" frmddl"></asp:TextBox>
                                                    </td>
                                                </tr>

                                               <tr>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="Advanceamount" SkinID="lblMand" runat="server" Font-Bold="True">Advance Amount </asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:TextBox ID="Txt_advanceamt" CssClass="numberVal  frmddl" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                           onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="Lab_Type" SkinID="lblMand" runat="server" Font-Bold="True" Visible="true"><span style="Color:Red"></span>Retailer Type</asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:DropDownList ID="DDL_Re_Type" runat="server" SkinID="ddlRequired" CssClass=" frmddl"></asp:DropDownList>
                                                   </td>
                                               </tr>
                                               
                                               <tr>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="Lab_Milk_Potential" SkinID="lblMand" runat="server" Font-Bold="True" >Potential</asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:TextBox ID="Txt_Mil_Pot" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                           onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal  frmddl"></asp:TextBox>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:Label ID="Lab_category" SkinID="lblMand" runat="server" Font-Bold="True" >Category</asp:Label>
                                                   </td>
                                                   <td class="stylespc" align="left">
                                                       <asp:DropDownList ID="DDL_category" runat="server" SkinID="ddlRequired" CssClass=" frmddl"></asp:DropDownList>
                                                   </td>
                                               </tr>

                                                <tr>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblDMP" SkinID="lblMand" runat="server" Font-Bold="True" > Daily Milk Production (In Ltrs) </asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtDMP" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal  frmddl"></asp:TextBox>
						                            </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblCC" SkinID="lblMand" runat="server" Font-Bold="True" >Current Competitor </asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:DropDownList ID="ddlCC" runat="server" SkinID="ddlRequired" CssClass=" frmddl">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblmonA" SkinID="lblMand" runat="server" Font-Bold="True">Monthly Ais</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtmonA" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal  frmddl"></asp:TextBox>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblfzy" SkinID="lblMand" runat="server" Font-Bold="True">Frequency</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:DropDownList ID="ddlfzy" runat="server" SkinID="ddlRequired" CssClass=" frmddl"></asp:DropDownList>                        
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblMCL" SkinID="lblMand" runat="server" Font-Bold="True" >  Milk Collection Ltrs (Daily)  </asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtMCL" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal  frmddl"></asp:TextBox>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lblMFPM" SkinID="lblMand" runat="server" Font-Bold="True" > Number of Farmers Pouring Milk  </asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtMFPM" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                                                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal  frmddl"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="Lab_UOM" SkinID="lblMand" runat="server" Visible="false" Font-Bold="True"><span style="Color:Red">*</span>UOM</asp:Label>
                                                        <asp:Label ID="lblERBCode" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">ERP Code</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:DropDownList ID="ddl_uom" runat="server" SkinID="ddlRequired" Visible="false"  CssClass=" frmddl"></asp:DropDownList>
                                                        <asp:TextBox ID="txtERBCode" runat="server"  CssClass=" frmddl"></asp:TextBox>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="Lab_Alt" SkinID="lblMand" runat="server"  Font-Bold="True"><span style="Color:black">CustomerWise Alter</span></asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:RadioButtonList ID="RblAlt" CssClass="Radio frmddl" runat="server" RepeatColumns="3"
                                                            Font-Names="Verdana" Font-Size="X-Small">
                                                            <asp:ListItem Value="1" Selected="True">ON &nbsp;&nbsp;</asp:ListItem>
                                                            <asp:ListItem Value="0">OFF &nbsp;&nbsp;</asp:ListItem>
                                                            <%--<asp:ListItem Value="O">Others</asp:ListItem>--%>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Lab_Alt_Msg" SkinID="lblMand" runat="server"  Font-Bold="True"><span style="Color:Red">(Note : More Then 3 Bill Show Alter)</span></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lbllat" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">Latitude</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtlat" runat="server" CssClass=" frmddl"></asp:TextBox>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:Label ID="lbllong" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">Longitude</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtlong" runat="server" CssClass=" frmddl"></asp:TextBox>
                                                    </td>
                                                </tr>                                               
                                                 <tr>
                                                     <td class="stylespc" align="left">
                                                        <asp:Label ID="Label1" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">Email</asp:Label>
                                                    </td>
                                                    <td class="stylespc" align="left">
                                                        <asp:TextBox ID="txtmail" runat="server" CssClass="frmddl"></asp:TextBox>
                                                    </td>                                                   
                                                </tr>
                                            </tbody>
                                        </table>
                                </div>
                                       
                                <div class="col-lg-3"  style="float:right">
                                    <table border="0" cellpadding="0" cellspacing="0" id="tbladd" style="width:  100%;">
                                        <tr>
                                            <td rowspan="" class="style65" align="left" style="background-color: #19a4c6; color: white;padding: 5px;font-weight:bold;">
                                                &nbsp;Address&nbsp;                            
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%" style="background-color:#D6E9C6">
                                        <tr>
                                            <td class="stylespc">
                                                <span style="color: Red">*</span><asp:Label ID="Label2" runat="server" 
                                                    Text="Address 1" Font-Bold="True" ></asp:Label>
                                            </td>
                                            <td class="stylespc" align="left">
                                                <asp:TextBox ID="txtAddress" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                    onblur="this.style.backgroundColor='White'" CssClass=" frmddl"></asp:TextBox>
                                            </td>
                                           
                                        </tr>  
                                        <tr>
                                             <td class="stylespc" align="left">
                                                <asp:Label ID="Label10" runat="server"  Text="Address 2" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td class="stylespc" align="left">
                                                <asp:TextBox ID="txtStreet" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                    onblur="this.style.backgroundColor='White'" CssClass=" frmddl"></asp:TextBox>
                                            </td>   
                                        </tr>
                                    </table>
                                </div>
                            </div> 
                            

                            <div class="col-lg-12" style="width:100%;">
                                <div class="col-lg-12">
                                    <table style="width:100%;">                                       
                                        <tr>
                                            <td>
                                                <asp:GridView ID="fugv" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Field Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFname" runat="server" Text='<%#Bind("Field_Name") %>'></asp:Label>
                                                                <asp:Label ID="LabelFC" runat="server" Text='<%#Bind("Field_Col") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Files Upload">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="flupslip" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td>
                                                 <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                            </td>
                                             <td>
                                                  <asp:Label ID="lblError" runat="server" Font-Bold="true"  ForeColor="#006600" Width="150"></asp:Label><br />
                                            </td>
                                        </tr>
                                    </table>                   
                                   
                                </div>
                                <div class="col-lg-12 labelnames" style="width:100%;">

                                </div>
                            </div>
                            
                            <br />
                            <%-- <div class="col-lg-12">
                                 <asp:Panel ID="pnl2" runat="server">
                                     <table border="0" cellpadding="0" cellspacing="0" id="tbladdtional" style="width: 100%;">
                                         <tr>
                                             <td rowspan="" class="style65" align="left" style="background-color: #19a4c6; color: white;font-weight:bold; padding: 6px;">
                                                 &nbsp;Additional Fields&nbsp;                            
                                             </td>
                                         </tr>
                                     </table>
                                     <br />
                                     
                                     <br />
                                     <table border="0" cellpadding="2" cellspacing="2" id="RetaileradditionalField" style="width: 100%;margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                         <tbody></tbody>    
                                     </table>   
                                 </asp:Panel>
                             </div>   --%>        
                        </div>
                    </div>

                    <div class="card-footer text-center" style="margin-top:-10px;">
                        
                    </div>
                    <div class="loading" style="align-content:center">
                        Loading. Please wait. Loading. Please wait.<br />
                        <br />
                        <img src="../../../Images/loader.gif" alt="" />
                    </div>
                </div>
            </form>

          
            <script type="text/javascript">
                var listeddrcode = 0; var CFBindData = []; var MasFrms = []; var MasFrmsGroup = [];
                var rbm; var cbm; var smdropdown; var CMfiltmgr = []; var RMfiltmgr = [];
                var SSMList = []; var ssmtablename = ''; var scontrolId = '';
                var mdcontrolId = '';

                $(document).ready(function () {

                    var pageURL = window.location.search.substring(2);
                    if (pageURL != "") {
                        var urlQS = pageURL.split('&');

                        if (urlQS.length > 0) {
                            for (var i = 0; i < urlQS.length; i++) {
                                var paramName = urlQS[i].split('=');
                                listeddrcode = paramName[1];
                            }
                        }
                    }
                    else { listeddrcode = 0; }

                    //console.log(listeddrcode);
                    GetCustomFormsFieldsGroup();
                    GetCustomFormsFields();

                    if (listeddrcode > 0) {
                        BindCustomFieldData(listeddrcode);
                    }

                    $('#example-multiple-selected').hide()
                    $('#<%=lblDMP.ClientID%>').hide();
                    $('#<%=txtDMP.ClientID%>').hide()
                    $('#<%=lblCC.ClientID%>').hide();
                    $('#<%=ddlCC.ClientID%>').hide();
                    $('#<%=lblmonA.ClientID%>').hide();
                    $('#<%=txtmonA.ClientID%>').hide();
                    $('#<%=lblMCL.ClientID%>').hide();
                    $('#<%=txtMCL.ClientID%>').hide();
                    $('#<%=lblMFPM.ClientID%>').hide();
                    $('#<%=txtMFPM.ClientID%>').hide();
                    $('#<%=lblfzy.ClientID%>').hide();
                    $('#<%=ddlfzy.ClientID%>').hide();
                    $('#<%=Label4.ClientID%>').hide();
                    $('#<%=creditdays.ClientID%>').hide();

                    $('input:text:first').focus();

                    $('input:text').bind("keydown", function (e) {
                        var n = $("input:text").length;
                        if (e.which == 13) { //Enter key
                            e.preventDefault(); //to skip default behavior of the enter key
                            var curIndex = $('input:text').index(this);

                            if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
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
                                    $('#btnSave').focus();
                                }
                            }
                        }
                    });

                    $("input:text").on("keypress", function (e) {
                        if (e.which === 32 && !this.value.length)
                            e.preventDefault();
                    });

                    $('#<%=btnSave.ClientID%>').click(function () {

                        if ($('#<%=Txt_id.ClientID%>').val() == "") { alert("Enter Retailer Code."); $('#<%=Txt_id.ClientID%>').focus(); return false; }
                        if ($('#<%=txtName.ClientID%>').val() == "") { alert("Enter Retailer Name."); $('#<%=txtName.ClientID%>').focus(); return false; }

                        var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                        if (spec == "---Select---") { alert("Select Channel."); $('#<%=ddlSpec.ClientID%>').focus(); return false; }

                        var clas = $('#<%=ddlClass.ClientID%> :selected').text();
                        if (clas == "---Select---") { alert("Select Class."); $('#<%=ddlClass.ClientID%>').focus(); return false; }

                        var Rou = $('#<%=ddlTerritory.ClientID%> :selected').text();
                        if (Rou == "---Select---") { alert("Select Route."); $('#<%=ddlTerritory.ClientID%>').focus(); return false; }

                        if ($('#<%=txtAddress.ClientID%>').val() == "") { alert("Enter Address."); $('#<%=txtAddress.ClientID%>').focus(); return false; }

                        var sbreed = '';
                        $('#example-multiple-selected  > option:selected').each(function () {
                            sbreed += $(this).text() + ',';
                        });
                        $('#<%=hdnbreedname.ClientID%>').val(sbreed)

                    });

                    $('.numberVal').keypress(function (event) {

                        return isNumber(event, this)
                    });

                    function isNumber(evt, element) {

                        var charCode = (evt.which) ? evt.which : evt.keyCode

                        if ((charCode != 46 || $(element).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57)) { return false; }
                        else { return true; }
                    }

                    if ($('#<%=divcode.ClientID%>').val() == 70) {
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: "ListedDr_DetailAdd_Custom.aspx/Fillddlbreed",
                            data: {},
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                var Datas = data.d;
                                if (Datas.length > 0) {
                                    $.each(data.d, function () {
                                        $('#example-multiple-selected').append($("<option></option>").val(this['id']).html(this['name']));
                                    });
                                    $('#example-multiple-selected').multiselect({
                                        columns: 3,
                                        placeholder: 'Select Breed',
                                        search: true,
                                        searchOptions: {
                                            'default': 'Search Breed'
                                        },
                                        selectAll: true
                                    }).multiselect('reload');
                                    //$('.ms-options ul').css('column-count', '3');
                                }
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                        var ddlbreed = $('#<%=hdnbreedname.ClientID%>').val();
                        if (ddlbreed != "") {
                            var breedname = ddlbreed.split(",")
                            $('#example-multiple-selected  > option').each(function () {
                                for (var i = 0; i < breedname.length; i++) {
                                    if (breedname[i] == $(this).text()) { $(this).prop('selected', true); $('#example-multiple-selected').multiselect('reload') }
                                }
                            });
                        }
                        $('#<%=lblQual.ClientID%>').text('*Customer Code')
                        $('#<%=lblName.ClientID%>').text('*Name Of Customer')
                        $('#<%=lblSpec.ClientID%>').text('*Category')
                        $('#<%=Lab_Type.ClientID%>').text('Customer Type')
                        $('#<%=TinNO.ClientID%>').hide()
                        $('#<%=lblCC.ClientID%>').show();
                        $('#<%=ddlCC.ClientID%>').show()
                        $('#<%=lblfzy.ClientID%>').show()
                        $('#<%=ddlfzy.ClientID%>').show()
                        $('#<%=lblCatg.ClientID%>').text('*Breed')
                        // $('#<%=lblCatg.ClientID%>').hide()
                        // $('#example-multiple-selected').hide()
                        $('#<%=Label4.ClientID%>').text('No of Animals')
                        if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'AIT') {
                            $('#<%=lblmonA.ClientID%>').show()
                                $('#<%=txtmonA.ClientID%>').show()
                            //  $('#<%=lblCatg.ClientID%>').text('*Breed')
                            //   $('#<%=lblCatg.ClientID%>').show()  
                            //   $('#example-multiple-selected').show()

                        }
                        if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'DF') {
                            $('#<%=Label4.ClientID%>').show()
                                $('#<%=creditdays.ClientID%>').show()
                                $('#<%=lblDMP.ClientID%>').show()
                                $('#<%=txtDMP.ClientID%>').show()
                            // $('#<%=lblCatg.ClientID%>').text('*Breed')
                            // $('#<%=lblCatg.ClientID%>').show()
                            //$('#example-multiple-selected').show()
                        }
                        if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'MCC') {
                            $('#<%=lblMCL.ClientID%>').show()
                                $('#<%=txtMCL.ClientID%>').show()
                                $('#<%=lblMFPM.ClientID%>').show()
                                $('#<%=txtMFPM.ClientID%>').show()
                                // $('#example-multiple-selected').hide()
                                $('#<%=creditdays.ClientID%>').hide()
                        }
                        $('#<%=ddlSpec.ClientID%>').on('change', function () {
                            if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'DF') {
                                    $('#<%=Label4.ClientID%>').show();
                                    $('#<%=creditdays.ClientID%>').show();
                                    $('#<%=lblDMP.ClientID%>').show();
                                    $('#<%=txtDMP.ClientID%>').show();
                                    $('#<%=lblMCL.ClientID%>').hide();
                                    $('#<%=txtMCL.ClientID%>').hide();
                                    $('#<%=lblMFPM.ClientID%>').hide();
                                    $('#<%=txtMFPM.ClientID%>').hide();
                                    $('#<%=lblmonA.ClientID%>').hide();
                                    $('#<%=txtmonA.ClientID%>').hide();
                                    $('#<%=lblCatg.ClientID%>').text('*Breed');
                                    $('#<%=lblCatg.ClientID%>').show();
                                //$('#example-multiple-selected').show()
                                <%--$('#<%=ddlCatg.ClientID%>').multiselect(
                                 {
                                    includeSelectAllOption: true
                                });--%>
                                }
                                else {
                                    $('#<%=Label4.ClientID%>').hide();
                                    $('#<%=creditdays.ClientID%>').hide();
                                    $('#<%=lblDMP.ClientID%>').hide();
                                    $('#<%=txtDMP.ClientID%>').hide();
                                }
                                if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'AIT') {
                                    $('#<%=lblmonA.ClientID%>').show();
                                    $('#<%=txtmonA.ClientID%>').show();
                                    $('#<%=lblMCL.ClientID%>').hide();
                                    $('#<%=txtMCL.ClientID%>').hide();
                                    $('#<%=lblMFPM.ClientID%>').hide();
                                    $('#<%=txtMFPM.ClientID%>').hide();
                                    $('#<%=lblDMP.ClientID%>').hide();
                                    $('#<%=txtDMP.ClientID%>').hide();
                                $('#<%=Label4.ClientID%>').hide()
                                $('#<%=creditdays.ClientID%>').hide();
                                //$('#example-multiple-selected').hide()
                            }
                            else {
                                $('#<%=lblmonA.ClientID%>').hide();
                                $('#<%=txtmonA.ClientID%>').hide();
                                }
                                if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'MCC') {
                                    $('#<%=lblMCL.ClientID%>').show();
                                $('#<%=txtMCL.ClientID%>').show();
                                $('#<%=lblMFPM.ClientID%>').show();
                                $('#<%=txtMFPM.ClientID%>').show();
                                $('#<%=lblmonA.ClientID%>').hide();
                                $('#<%=txtmonA.ClientID%>').hide();
                                $('#<%=lblDMP.ClientID%>').hide();
                                $('#<%=txtDMP.ClientID%>').hide();
                                $('#<%=Label4.ClientID%>').hide();
                                $('#<%=creditdays.ClientID%>').hide();
                                //$('#example-multiple-selected').hide()
                            }
                            else {
                                $('#<%=lblMCL.ClientID%>').hide();
                                $('#<%=txtMCL.ClientID%>').hide();
                                $('#<%=lblMFPM.ClientID%>').hide();
                                $('#<%=txtMFPM.ClientID%>').hide();
                            }
                        });
                    }

                   
                });

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

                function GetCustomFormsFieldsGroup() {
                    var furl = "ListedDr_DetailAdd_Custom.aspx/GetCustomFormsFieldsGroups";
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,                       
                        url: furl,
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'3'}",
                        dataType: "json",
                        success: function (data) {
                            $(".labelnames").html('');
                            MasFrmsGroup = JSON.parse(data.d) || [];
                            if (MasFrmsGroup.length > 0) {
                                var str = "";
                                var str1 = "";
                                j = 0;
                                GetCustomFormsFields();
                                //console.log(MasFrms);
                                for (var j = 0; j < MasFrmsGroup.length; j++) {

                                    var GroupName = MasFrmsGroup[j].FGroupName;
                                    var FGTableName = MasFrmsGroup[j].FGTableName;

                                    let filtered = MasFrms.filter(function (a) {
                                        return a.FGroupName == GroupName;
                                    });

                                    str += "<div class='dynamic' style='width:100%;'><h4 class='" + FGTableName + "' role='heading' aria-level='1'  style='background-color:#19a4c6;color:white;padding:5px;font-weight:bold;'>" + GroupName + "</h4>";

                                    str += "<table id='" + FGTableName + "' style='width:100%;' class='table-responsive  " + FGTableName + "'>";

                                    str += "<tr>";
                                    var m = 0;
                                    for (var k = 0; k < filtered.length; k++) {

                                        var FldType = filtered[k].Fld_Type;
                                        var Mandate = filtered[k].Mandate;
                                        if (FldType == "FS" || FldType == "FC" || FldType == "FSC") { }
                                        else { str += "<td class='space' align='left'><label for='" + filtered[k].Field_Col + "' value='" + filtered[k].Field_Name + "'>" + ((Mandate == "Yes") ? "<span class='fldm' style='Color:Red'>*</span>" : "<span />") + filtered[k].Field_Name + "</label></td>"; }
                                        
                                        switch (FldType) {
                                            case 'TA':
                                                if ((filtered[k].Mandate == "Yes")) { str += "<td class='stylespc' align='left'><input type='text' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                else { str += "<td class='stylespc' align='left'><input type='text' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                break;
                                            case 'TAS':
                                                if ((filtered[k].Mandate == "Yes")) { str += "<td class='stylespc' align='left'><input type='text' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                else { str += "<td class='stylespc' align='left'><input type='text' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                break;
                                            case 'TAM':
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><textarea type='text' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control required' maxLength='" + filtered[k].Fld_Length + "'></textarea></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><textarea type='text' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "'></textarea></td>"; }
                                                break;
                                            case 'NC':
                                                str += "<td class='stylespc' align='left'>";
                                                str += "<div class='row'>";
                                                str += "<div class='col-sm-6'>";
                                                str += "<div class='input-group input-group-sm mb-3' style='display: flex'>";
                                                str += "<div class='input-group-prepend'>";
                                                str += "<div class='input-group-text' style='width:50px; padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px;' id='NCS'>" + filtered[k].Fld_Symbol + "</div>";
                                                str += "</div>";
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<input type='number' onfocus='this.style.backgroundColor='LavenderBlush'' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control required' maxLength='" + filtered[k].Fld_Length + "' />";
                                                }
                                                else {
                                                    str += "<input type='number' onfocus='this.style.backgroundColor='LavenderBlush'' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' />";
                                                }
                                                str += "</div>";
                                                str += "</div>";
                                                str += "</div>";
                                                str += "</td>";
                                                break;
                                            case 'NP':
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><input type='number' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='number' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                break;
                                            case 'N':
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><input type='number' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'><input type='number' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                break;
                                            case 'DR':

                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><input type='date' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control required' /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='date' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired'  /></td>"; }
                                                break;
                                            case 'D':
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><input type='date' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='date' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                break;
                                            case 'TR':
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><input type='time' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='time' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>"; }
                                                break;
                                            case 'T':
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><input type='time' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control required' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'><input type='time' id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "'  class='form-control notrequired' maxLength='" + filtered[k].Fld_Length + "' /></td>";
                                                }
                                                break;
                                            case 'SSM':
                                                ssmtablename = filtered[k].Fld_Src_Name;
                                                scontrolId = filtered[k].Field_Col;

                                                BindDropdown(ssmtablename);

                                                if ((filtered[k].Mandate == "Yes")) {

                                                    str += "<td class='stylespc' align='left'>";

                                                    str += "<select name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='form-control required'>";
                                                    str += "<option value='0'>Select</option>";
                                                    //$('.SSMDetails').append('<option value="0">Select</option>');
                                                    for (var j = 0; j < SSMList.length; j++) {
                                                        str += "<option value='" + SSMList[j].IDCol + "'>" + SSMList[j].TextVal + "</option>";
                                                        //$('.SSMDetails').append('<option value="' + SSMList[j].IDCol + '">' + SSMList[j].TextVal + '</option>');
                                                    }
                                                    str += "</select>";

                                                    str += "</td>";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'>";

                                                    str += "<select name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='form-control notrequired'>";
                                                    str += "<option value='0'>Select</option>";
                                                    //$('.SSMDetails').append('<option value="0">Select</option>');
                                                    for (var j = 0; j < SSMList.length; j++) {
                                                        str += "<option value='" + SSMList[j].IDCol + "'>" + SSMList[j].TextVal + "</option>";
                                                        //$('.SSMDetails').append('<option value="' + SSMList[j].IDCol + '">' + SSMList[j].TextVal + '</option>');
                                                    }
                                                    str += "</select>";

                                                    str += "</td>";
                                                }

                                                break;
                                            case 'SSO':
                                                const SSOArray = filtered[k].Fld_Src_Field.split(",");
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    if (SSOArray.length > 0) {

                                                        if ((SSOArray.length = 2)) {
                                                            str += "<td class='stylespc' align='left'>";

                                                            str += "<select name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='form-control required'>";
                                                            str += "<option value='0'>Select</option>";
                                                            //$('.SSMDetails').append('<option value="0">Select</option>');
                                                            for (var j = 0; j < SSOArray.length; j++) {
                                                                str += "<option value='" + SSOArray[j] + "'>" + SSOArray[j] + "</option>";
                                                                //$('.SSMDetails').append('<option value="' + SSMList[j].IDCol + '">' + SSMList[j].TextVal + '</option>');
                                                            }
                                                            str += "</select>";

                                                            str += "</td>";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                            for (var ro = 0; ro < SSOArray.length; ro++) {
                                                                str += "<input type='checkbox' id='" + SSOArray[ro] + "' value='" + SSOArray[ro] + "' />&nbsp;&nbsp;<label>" + SSOArray[ro] + "<lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                        str += "<input type='checkbox' id='" + SSOArray[0] + "' value='" + SSOArray[0] + "' />&nbsp;<label>" + SSOArray[0] + "<lable>&nbsp;";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                else {
                                                    if (SSOArray.length > 0) {

                                                        if ((SSOArray.length = 2)) {
                                                            str += "<td class='stylespc' align='left'>";

                                                            str += "<select name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='form-control notrequired'>";
                                                            str += "<option value='0'>Select</option>";
                                                            //$('.SSMDetails').append('<option value="0">Select</option>');
                                                            for (var j = 0; j < SSOArray.length; j++) {
                                                                str += "<option value='" + SSOArray[j] + "'>" + SSOArray[j] + "</option>";
                                                                //$('.SSMDetails').append('<option value="' + SSMList[j].IDCol + '">' + SSMList[j].TextVal + '</option>');
                                                            }
                                                            str += "</select>";

                                                            str += "</td>";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                            for (var ro = 0; ro < SSOArray.length; ro++) {
                                                                str += "<input type='checkbox' id='" + SSOArray[ro] + "' value='" + SSOArray[ro] + "' />&nbsp;&nbsp;<label>" + SSOArray[ro] + "<lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                        str += "<input type='checkbox' id='" + SSOArray[0] + "' value='" + SSOArray[0] + "' />&nbsp;<label>" + SSOArray[0] + "<lable>&nbsp;";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                break;
                                            case 'SMM':

                                                if ((filtered[k].Mandate == "Yes")) {
                                                    var crmn = filtered[k].Fld_Src_Name;
                                                    BindCheckboxs(crmn);
                                                    str += "<td class='stylespc' align='left'><br \>";
                                                    str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";
                                                    if (CMfiltmgr.length > 0) {
                                                        str += "<table id='tblsmm" + k + "' class='table-responsive'>";

                                                        str += "<tr>";
                                                        var g = 0;
                                                        for (var f = 0; f < CMfiltmgr.length; f++) {

                                                            str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + CMfiltmgr[f].IDCol + "'";
                                                            str += "name='" + filtered[k].Field_Col + "' class='chkfnRow' value='" + CMfiltmgr[f].IDCol + "' />&nbsp;&nbsp;<label class='text-wrap' for='" + CMfiltmgr[f].IDCol + "'>" + CMfiltmgr[f].TextVal + " </label></td>";

                                                            g++;
                                                            if (g == 2) {
                                                                str += "</tr><tr>";
                                                                g = 0;
                                                            }
                                                        }
                                                        str += "</tr>";
                                                        str += "</table>";
                                                    }

                                                    str += "</div>";

                                                    str += "</td>";

                                                }
                                                else {
                                                    var crmn = filtered[k].Fld_Src_Name;
                                                    BindCheckboxs(crmn);
                                                    str += "<td class='stylespc' align='left'>";
                                                    str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";
                                                    if (CMfiltmgr.length > 0) {
                                                        str += "<table id='tblsmm" + k + "' class='table-responsive'>";

                                                        str += "<tr>";
                                                        var g = 0;
                                                        for (var f = 0; f < CMfiltmgr.length; f++) {

                                                            str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + CMfiltmgr[f].IDCol + "'";
                                                            str += "name='" + filtered[k].Field_Col + "' class='chkfnRow' value='" + CMfiltmgr[f].IDCol + "' />&nbsp;<label class='text-wrap' for='" + CMfiltmgr[f].IDCol + "'>" + CMfiltmgr[f].TextVal + " </label></td>";
                                                            g++;
                                                            if (g == 2) {
                                                                str += "</tr><tr>";
                                                                g = 0;
                                                            }
                                                        }
                                                        str += "</tr>";
                                                        str += '</table>';
                                                    }

                                                    str += "</div>";
                                                    str += "</td>";
                                                }

                                                break;
                                            case 'SMO':
                                                const SMOArray = filtered[k].Fld_Src_Field.split(",");
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    if (SMOArray.length > 0) {


                                                        if ((SMOArray.length == 2)) {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                            for (var ro = 0; ro < SMOArray.length; ro++) {
                                                                str += "<input type='checkbox' name='" + filtered[k].Field_Col + "' id='" + SMOArray[ro] + "' class='rbnsnRow' value='" + SMOArray[ro] + "' />&nbsp;&nbsp;<label>" + SMOArray[ro] + "</lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'><br \>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";

                                                            str += "<table id='tblsmo" + k + "'  class='table-responsive'>";

                                                            str += "<tr>";
                                                            var g = 0;
                                                            for (var f = 0; f < SMOArray.length; f++) {

                                                                str += "<td style='font-weight:bold; font-size:10px;'><input type='checkbox' id='" + SMOArray[f] + "'";
                                                                str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + SMOArray[f] + "' />&nbsp;<label class='text-wrap' for='" + SMOArray[f] + "'>" + SMOArray[f] + " </label></td>";

                                                                g++;
                                                                if (g == 2) {
                                                                    str += "</tr><tr>";
                                                                    g = 0;
                                                                }
                                                            }
                                                            str += "</tr>";
                                                            str += "</table>";
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }

                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                        str += "<input type='checkbox' id='" + SMOArray[0] + "' value='" + SMOArray[0] + "' />&nbsp<label>" + SMOArray[0] + "</lable>";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                else {
                                                    if (SMOArray.length > 0) {
                                                        if ((SMOArray.length == 2)) {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                            for (var ro = 0; ro < SMOArray.length; ro++) {
                                                                str += "<input type='checkbox' name='" + filtered[k].Field_Col + "' id='" + SMOArray[ro] + "' class='rbnsnRow' value='" + SMOArray[ro] + "' />&nbsp;&nbsp;<label>" + SMOArray[ro] + "</lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'><br \>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";

                                                            str += "<table id='tblsmo" + k + "'  class='table-responsive'>";

                                                            str += "<tr>";
                                                            var g = 0;
                                                            for (var f = 0; f < SMOArray.length; f++) {

                                                                str += "<td style='font-weight:bold; font-size:10px;'><input type='checkbox' id='" + SMOArray[f] + "'";
                                                                str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + SMOArray[f] + "' />&nbsp;<label class='text-wrap' for='" + SMOArray[f] + "'>" + SMOArray[f] + " </label></td>";

                                                                g++;
                                                                if (g == 2) {
                                                                    str += "</tr><tr>";
                                                                    g = 0;
                                                                }
                                                            }
                                                            str += "</tr>";
                                                            str += "</table>";
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }

                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                        str += "<input type='checkbox' id='" + SMOArray[0] + "' value='" + SMOArray[0] + "' />&nbsp<label>" + SMOArray[0] + "</lable>";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                break;
                                            case 'CO':
                                                const COArray = filtered[k].Fld_Src_Field.split(",");
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    if ((COArray.length > 0)) {

                                                        if ((COArray.length == 2)) {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                            for (var ro = 0; ro < COArray.length; ro++) {
                                                                str += "<input type='checkbox' name='" + filtered[k].Field_Col + "' id='" + COArray[ro] + "' class='rbnsnRow' value='" + COArray[ro] + "' />&nbsp;&nbsp;<label>" + COArray[ro] + "</lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'><br \>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";

                                                            str += "<table id='tblco" + k + "' class='table-responsive'>";

                                                            str += "<tr>";
                                                            var g = 0;
                                                            for (var f = 0; f < COArray.length; f++) {

                                                                str += "<td style='font-weight:bold; font-size:10px;'><input type='checkbox' id='" + COArray[f] + "'";
                                                                str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + COArray[f] + "' />&nbsp;<label class='text-wrap' for='" + COArray[f] + "'>" + COArray[f] + " </label></td>";

                                                                g++;
                                                                if (g == 2) {
                                                                    str += "</tr><tr>";
                                                                    g = 0;
                                                                }
                                                            }
                                                            str += "</tr>";
                                                            str += "</table>";
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                        str += "<input type='radio' id='" + COArray[0] + "' value='" + COArray[0] + "' />&nbps<label>" + COArray[0] + "</lable></td>";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                else {
                                                    if ((COArray.length > 0)) {
                                                        if ((COArray.length == 2)) {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                            for (var ro = 0; ro < COArray.length; ro++) {
                                                                str += "<input type='checkbox' name='" + filtered[k].Field_Col + "' id='" + COArray[ro] + "' class='rbnsnRow' value='" + COArray[ro] + "' />&nbsp;&nbsp;<label>" + COArray[ro] + "</lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'><br \>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";

                                                            str += "<table id='tblco" + k + "'  class='table-responsive'>";

                                                            str += "<tr>";
                                                            var g = 0;
                                                            for (var f = 0; f < COArray.length; f++) {

                                                                str += "<td style='font-weight:bold; font-size:10px;'><input type='checkbox' id='" + COArray[f] + "'";
                                                                str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + COArray[f] + "' />&nbsp;<label class='text-wrap' for='" + COArray[f] + "'>" + COArray[f] + " </label></td>";

                                                                g++;
                                                                if (g == 2) {
                                                                    str += "</tr><tr>";
                                                                    g = 0;
                                                                }
                                                            }
                                                            str += "</tr>";
                                                            str += "</table>";
                                                            str += "</div>";
                                                            str += "</td>";
                                                        }
                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                        str += "<input type='radio' id='" + COArray[0] + "' value='" + COArray[0] + "' />&nbps<label>" + COArray[0] + "</lable>";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                break;
                                            case 'CM':
                                                var crmn = filtered[k].Fld_Src_Name;
                                                BindCheckboxs(crmn);

                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><br />";
                                                    str += "<div name='" + filtered[k].Field_Col + "' id=" + filtered[k].Field_Col + " class='required'  style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";
                                                    if (CMfiltmgr.length > 0) {
                                                        str += "<table id='tblcm" + k + "' class='tChblControl table-responsive'>";

                                                        str += "<tr>";
                                                        var g = 0;
                                                        for (var f = 0; f < CMfiltmgr.length; f++) {

                                                            str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + CMfiltmgr[f].IDCol + "''";
                                                            str += "name='" + filtered[k].Field_Col + "'  class='chkfnRow' value='" + CMfiltmgr[f].IDCol + "' />&nbsp;<label class='text-wrap' for='" + CMfiltmgr[f].IDCol + "'>" + CMfiltmgr[f].TextVal + " </label></td>";

                                                            g++;
                                                            if (g == 2) {
                                                                str += "</tr><tr>";
                                                                g = 0;
                                                            }
                                                        }
                                                        str += "</tr>";
                                                        str += '</table>';
                                                    }

                                                    str += "</div>";
                                                    str += "</td> ";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'><br />";
                                                    str += "<div name='" + filtered[k].Field_Col + "' id=" + filtered[k].Field_Col + " class='notrequired' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";
                                                    if (CMfiltmgr.length > 0) {
                                                        str += "<table id='tblcm" + k + "'  class='tChblControl table-responsive'>";

                                                        str += "<tr>";
                                                        var g = 0;
                                                        for (var f = 0; f < CMfiltmgr.length; f++) {

                                                            str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + CMfiltmgr[f].IDCol + "''";
                                                            str += "name='" + filtered[k].Field_Col + "'  class='chkfnRow' value='" + CMfiltmgr[f].IDCol + "' />&nbsp;<label class='text-wrap' for='" + CMfiltmgr[f].IDCol + "'>" + CMfiltmgr[f].TextVal + " </label></td>";
                                                            //str += "<td></td>";

                                                            g++;
                                                            if (g == 2) {
                                                                str += "</tr><tr>";
                                                                g = 0;
                                                            }
                                                        }
                                                        str += "</tr>";
                                                        str += '</table>';
                                                    }

                                                    str += "</div>";
                                                    str += "</td> ";
                                                }
                                                break;
                                            case 'RM':
                                                var rbm = filtered[k].Fld_Src_Name;
                                                //console.log(rbm)
                                                BindRadiobutton(rbm);
                                                //console.log(RMfiltmgr);
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    str += "<td class='stylespc' align='left'><br/>";
                                                    str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";
                                                    if (RMfiltmgr.length > 0) {
                                                        str += "<table id='tblrm" + k + "' class='RbtnlControl table-responsive'>";

                                                        str += "<tr>";
                                                        var g = 0;
                                                        for (var f = 0; f < RMfiltmgr.length; f++) {

                                                            str += "<td style='font-weight:bold; font-size:10px;'><input type='radio' id='" + RMfiltmgr[f].IDCol + "'";
                                                            str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + RMfiltmgr[f].IDCol + "' />&nbsp;<label class='text-wrap' for='" + RMfiltmgr[f].IDCol + "'>" + RMfiltmgr[f].TextVal + " </label></td>";
                                                            //str += "<td><label class='text-wrap' for='" + RMfiltmgr[f].IDCol + "'>" + RMfiltmgr[f].TextVal + " </label></td>";

                                                            g++;
                                                            if (g == 2) {
                                                                str += "</tr><tr>";
                                                                g = 0;
                                                            }
                                                        }
                                                        str += "</tr>";
                                                        str += '</table>';
                                                    }
                                                    str += "</div>";
                                                    str += "</td> ";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'><br />";
                                                    str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";
                                                    if (RMfiltmgr.length > 0) {
                                                        str += "<table id='tblrm" + k + "'  class='table-responsive'>";

                                                        str += "<tr>";
                                                        var g = 0;
                                                        for (var f = 0; f < RMfiltmgr.length; f++) {

                                                            str += "<td style='font-weight:bold; font-size:10px;'><input type='radio' id='" + RMfiltmgr[f].IDCol + "'";
                                                            str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + RMfiltmgr[f].IDCol + "' />&nbsp;<label class='text-wrap' for='" + RMfiltmgr[f].IDCol + "'>" + RMfiltmgr[f].TextVal + " </label></td>";

                                                            g++;
                                                            if (g == 2) {
                                                                str += "</tr><tr>";
                                                                g = 0;
                                                            }
                                                        }
                                                        str += "</tr>";
                                                        str += "</table>";
                                                    }
                                                    str += "</div>";
                                                    str += "</td>";
                                                }
                                                break;
                                            case 'RO':
                                                const ROArray = filtered[k].Fld_Src_Field.split(",");
                                                if ((filtered[k].Mandate == "Yes")) {
                                                    if ((ROArray.length > 0)) {
                                                        if ((ROArray.length == 2)) {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                            for (var ro = 0; ro < ROArray.length; ro++) {
                                                                str += "<input type='radio' name='" + filtered[k].Field_Col + "' id='" + ROArray[ro] + "' class='rbnsnRow' value='" + ROArray[ro] + "' />&nbsp;&nbsp;<label>" + ROArray[ro] + "</lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td> ";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'><br />";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";

                                                            str += "<table id='tblro" + k + "' class='table-responsive'>";

                                                            str += "<tr>";
                                                            var g = 0;
                                                            for (var f = 0; f < ROArray.length; f++) {

                                                                str += "<td style='font-weight:bold; font-size:10px;'><input type='radio' id='" + ROArray[f] + "'";
                                                                str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + ROArray[f] + "' />&nbsp;<label class='text-wrap' for='" + ROArray[f] + "'>" + ROArray[f] + " </label></td>";

                                                                g++;
                                                                if (g == 2) {
                                                                    str += "</tr><tr>";
                                                                    g = 0;
                                                                }
                                                            }
                                                            str += "</tr>";
                                                            str += '</table>';
                                                            str += "</div>";
                                                            str += "</td> ";
                                                        }
                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='required'>";
                                                        str += "<input type='radio' name='" + filtered[k].Field_Col + "' id='" + ROArray[0] + "' value='" + ROArray[0] + "' />&nbps<label>" + ROArray[0] + "</lable>";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                else {
                                                    if ((ROArray.length > 0)) {
                                                        if ((ROArray.length == 2)) {
                                                            str += "<td class='stylespc' align='left'>";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                            for (var ro = 0; ro < ROArray.length; ro++) {
                                                                str += "<input type='radio' name='" + filtered[k].Field_Col + "' id='" + ROArray[ro] + "' class='rbnsnRow' value='" + ROArray[ro] + "' />&nbsp;&nbsp;<label>" + ROArray[ro] + "</lable>&nbsp;&nbsp;";
                                                            }
                                                            str += "</div>";
                                                            str += "</td> ";
                                                        }
                                                        else {
                                                            str += "<td class='stylespc' align='left'><br />";
                                                            str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired' style='width:350px;height:auto;max-height:200px;overflow-y:scroll;'>";

                                                            str += "<table id='tblro" + k + "' class='table-responsive'>";

                                                            str += "<tr>";
                                                            var g = 0;
                                                            for (var f = 0; f < ROArray.length; f++) {

                                                                str += "<td style='font-weight:bold; font-size:10px;'><input type='radio' id='" + ROArray[f] + "'";
                                                                str += "name='" + filtered[k].Field_Col + "' class='rbnmsRow' value='" + ROArray[f] + "' />&nbsp;<label class='text-wrap' for='" + ROArray[f] + "'>" + ROArray[f] + " </label></td>";

                                                                g++;
                                                                if (g == 2) {
                                                                    str += "</tr><tr>";
                                                                    g = 0;
                                                                }
                                                            }
                                                            str += "</tr>";
                                                            str += '</table>';

                                                            str += "</div>";
                                                            str += "</td> ";
                                                        }
                                                    }
                                                    else {
                                                        str += "<td class='stylespc' align='left'>";
                                                        str += "<div name='" + filtered[k].Field_Col + "' id='" + filtered[k].Field_Col + "' class='notrequired'>";
                                                        str += "<input type='radio' name='" + filtered[k].Field_Col + "' id='" + ROArray[0] + "' value='" + ROArray[0] + "' />&nbps<label>" + ROArray[0] + "</lable>&nbps";
                                                        str += "</div>";
                                                        str += "</td>";
                                                    }
                                                }
                                                break;
                                            //case 'FS':
                                             

                                            //    str += "<td class='stylespc' align='left'>";
                                             
                                            //    //str += "<input name='photo' type='file' accept='image/png, image/jpeg, image/jpg' onchange='document.getElementById('" + filtered[k].Field_Col + "').src = window.URL.createObjectURL(this.files[0])'>";
                                            //    str += "<input type='file' onchange='getFile(this)'  id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='fsfiles notrequired'  />";
                                            //    str += "<input type='button' onclick='fnADD()' />";

                                            //    str += "<div width='100' height='100' class='" + filtered[k].Field_Col + "' /></td>";

                                            //    break;
                                            //case 'FSC':                                             

                                                
                                            //    str += "<td class='stylespc' align='left'>";
                                                
                                            //    //str += "<input name='photo' type='file' accept='image/png, image/jpeg, image/jpg' onchange='document.getElementById('" + filtered[k].Field_Col + "').src = window.URL.createObjectURL(this.files[0])'>";
                                            //    str += "<input type='file' onchange='getFile(this)'  id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='fsfiles notrequired'  />";
                                            //    str += "<input type='button' onclick='fnADD()' />";
                                            //    str += "<div width='100' height='100'  class='" + filtered[k].Field_Col + "' /></td>";

                                            //    break;

                                            //case 'FC':
                                            //    str += "<td class='stylespc' align='left'>";
                                                
                                            //    //str += "<input name='photo' type='file' accept='image/png, image/jpeg, image/jpg' onchange='document.getElementById('" + filtered[k].Field_Col + "').src = window.URL.createObjectURL(this.files[0])'>";
                                            //    str += "<input type='file' onchange='getFile(this)'  id='" + filtered[k].Field_Col + "' name='" + filtered[k].Field_Col + "' class='fsfiles notrequired'  />";
                                            //    str += "<input type='button' onclick='fnADD()' />";
                                            //    str += "<div width='100' height='100' class='" + filtered[k].Field_Col + "' /></td>";

                                            //    break;
                                          
                                            default:
                                                break
                                        }

                                        m++;
                                        if (m == 2) {
                                            str += "</tr><tr>";
                                            m = 0;
                                        }




                                    }

                                    str += "</tr>";
                                    str += '</table>';

                                    str += "</div>";

                                }
                                $(".labelnames").append(str);

                            }
                        }
                    });
                }

                function GetCustomFormsFields() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/GetCustomFormsFieldsList",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'3'}",
                        dataType: "json",
                        success: function (data) {
                            MasFrms = JSON.parse(data.d) || [];                         
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }

                    });
                }

                function BindCheckboxs(tablename) {
                    CMfiltmgr = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/GetCustomFormsSeclectionMastesList",
                        data: "{'TableName':'" + tablename + "'}",
                        dataType: "json",
                        success: function (data) {
                            CMfiltmgr = JSON.parse(data.d) || [];                            
                        }
                    });
                }

                function BindRadiobutton(tablename) {
                    RMfiltmgr = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/GetCustomFormsSeclectionMastesList",
                        data: "{'TableName':'" + tablename + "'}",
                        dataType: "json",
                        success: function (data) {
                            RMfiltmgr = JSON.parse(data.d) || [];                           
                        }
                    });
                }

                function BindDropdown(tablename) {
                    SSMList = [];

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/GetCustomFormsSeclectionMastesList",
                        data: "{'TableName':'" + tablename + "'}",
                        dataType: "json",
                        success: function (data) {
                            SSMList = JSON.parse(data.d) || [];
                            //$('.SSMDetails').empty();
                            //$('.SSMDetails').append('<option value="0">Select</option>');
                            //for (var j = 0; j < SSMList.length; j++) {
                            //    $('.SSMDetails').append('<option value="' + SSMList[j].IDCol + '">' + SSMList[j].TextVal + '</option>');
                            //}

                        }
                    });
                }                              
                                              
                $('#btnSaved').click(function () {                    
                    SaveCustomFielsValues();                    
                });                              
                                
                var additionalfud = new Array();              

                function fnADD() {
                    var input = document.getElementById('file_TicketManageMent_AttachFile');
                    var files = input.files;
                    var formData = new FormData();

                    for (var i = 0; i != files.length; i++) {
                        formData.append("Attachments", files[i]);
                    }

                    formData.append("FirstName", 'Title');
                    formData.append("LastName", 'Short Description');

                    $.ajax({
                        cache: false,
                        type: 'Post',
                        data: formData,
                        url: '{your_url_here}',
                        processData: false,
                        contentType: false,
                        success: function (xhr, ajaxOptions, thrownError) {

                        }
                    });
                }
                   
                function getFile(elm) {
                    var Attachments = new Array();
                  
                    var id = $(elm).attr("id");
                    var file = "";
                    var input = document.getElementById(id);
                    var files = input.files;
                    console.log(files);
                    var formData = new FormData();

                    for (var i = 0; i < files.length; i++) {
                        formData.append("Attachments", files[i].name);
                    }
                    console.log(formData);
                    //var name = '';
                    //var div = '';
                    //var fileUpload = $(elm);
                    //var Context = fileUpload.context.files;

                    for (var i = 0; i < files.length; i++) {

                        var adDetail = {};

                        file = files[i].name;  

                        div = "<div> " + file + " <br /></div>";

                        adDetail.fId = id;
                        adDetail.fName = file;
                        Attachments.push(adDetail);
                    }

                    var filename = {
                        "fId": id,
                        "fName": file
                    }

                    console.log(filename);

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/SaveFileS3Bucket",
                        data: "{'filename':'" + JSON.stringify(filename) + "'}",
                        dataType: "json",
                        success: function (msg) {
                            alert(msg.d);
                        }
                    });

                    $('.' + id + '').append(div);                  
                    
                }

                console.log(additionalfud);

                function SaveCustomFielsValues() {

                 

                    var vflag = true;

                    if ($('#<%=Txt_id.ClientID%>').val() == "") { alert("Enter Retailer Code."); $('#<%=Txt_id.ClientID%>').focus(); vflag = false; return vflag; }

                    if ($('#<%=txtName.ClientID%>').val() == "") { alert("Enter Retailer Name."); $('#<%=txtName.ClientID%>').focus(); vflag = false; return vflag; }

                    var spec = $('#<%=ddlSpec.ClientID%> :selected').text();
                    if (spec == "---Select---") { alert("Select Channel."); $('#<%=ddlSpec.ClientID%>').focus(); vflag = false; return vflag; }

                    var clas = $('#<%=ddlClass.ClientID%> :selected').text();
                    if (clas == "---Select---") { alert("Select Class."); $('#<%=ddlClass.ClientID%>').focus(); vflag = false; return vflag; }

                    var Rou = $('#<%=ddlTerritory.ClientID%> :selected').text();
                    if (Rou == "---Select---") { alert("Select Route."); $('#<%=ddlTerritory.ClientID%>').focus(); vflag = false; return vflag; }

                    if ($('#<%=txtAddress.ClientID%>').val() == "") { alert("Enter Address."); $('#<%=txtAddress.ClientID%>').focus(); vflag = false; return vflag; }

                    var sbreed = '';
                    $('#example-multiple-selected  > option:selected').each(function () {
                        sbreed += $(this).text() + ',';
                    });

                    $('#<%=hdnbreedname.ClientID%>').val(sbreed);

                    var addfields = new Array();

                    $('.required').each(function () {
                        //alert('hi');
                        var adDetail = {};
                        var fval = $(this).val();
                        var fid = `${this.id}`;
                        values = ""; fields = "";
                        fields = fid;

                        //var $label = $("label[for='" + fid + "']");
                        //var msg = "Please Fill The " + $label.text() + "";
                        //if ((fval == null || fval == "" || fval == "0")) {
                        //    alert(msg);
                        //    $(fid).focus();
                        //    vflag = false;
                        //    return vflag;
                        //}                                               

                        $(this).find('input[type="checkbox"]:checked').each(function () {
                            fval += this.value + ",";
                        });

                        var rbval = '';
                        $(this).find('input[type="radio"]:checked').each(function () {
                            fval += this.value + ",";
                        });

                        values = fval;
                        adDetail.Fields = fields;
                        adDetail.Values = values;
                        addfields.push(adDetail);
                    });

                    $('.notrequired').each(function () {

                        var adDetail = {};
                        var fval = $(this).val();
                        var fid = `${this.id}`;
                        fields = "";
                        fields = fid;

                        $(this).find('input[type="checkbox"]:checked').each(function () {
                            //console.log(this.value);
                            fval += this.value + ",";
                            //console.log(fval);
                        });

                        $(this).find('input[type="radio"]:checked').each(function () {
                            //console.log(this.value);
                            fval += this.value + ",";
                        });

                        values = fval;
                        adDetail.Fields = fields;
                        adDetail.Values = values;
                        addfields.push(adDetail);
                    });

                    if (Attachments.length > 0) {
                        var adDetail = {};
                        adDetail.Attachments = Attachments;
                        addfields.push(adDetail);
                    }
                    else {
                        var adDetail = {};
                        adDetail.Attachments = "";
                        addfields.push(adDetail);
                    }

                    console.log(addfields);

                    var fdata = {
                        "DR_Name": $('#<%=txtName.ClientID%>').val(),
                        "Mobile_No": $('#<%=txtMobile.ClientID%>').val(),
                        "retail_code": $('#<%=Txt_id.ClientID%>').val(),
                        "ERBCode": $('#<%=txtERBCode.ClientID%>').val(),
                        "advance_amount": $('#<%=txtDOW.ClientID%>').val(),
                        "DR_Spec": $('#<%=ddlSpec.ClientID%> option:selected').val(),
                        "dr_spec_name": $('#<%=ddlSpec.ClientID%> option:selected').text(),
                        "sales_Tax": $('#<%=salestaxno.ClientID%>').val(),
                        "Tinno": $('#<%=TinNO.ClientID%>').val(),
                        "DR_Terr": $('#<%=ddlTerritory.ClientID%> option:selected').val(),
                        "credit_days": $('#<%=creditdays.ClientID%> option:selected').val(),

                        "DR_Class": $('#<%=ddlClass.ClientID%> option:selected').val(),
                        "drcategory": $('#<%=DDL_category.ClientID%> option:selected').val(),
                        <%--"dscategoryName":$('#<%=DDL_category.ClientID%>').text(),--%>
                        "dscategoryName": $('#<%=DDL_category.ClientID%> option:selected').text(),
                        <%--"dr_class_name":$('#<%=ddlClass.ClientID%>').text(),--%>
                        "dr_class_name": $('#<%=ddlClass.ClientID %> option:selected').text(),

                        "ad": $('#<%=Txt_advanceamt.ClientID%>').val(),
                        "DR_Address1": $('#<%=txtAddress.ClientID%>').val(),
                        "DR_Address2": $('#<%=txtStreet.ClientID%>').val(),

                        "Milk_pon": $('#<%=Txt_Mil_Pot.ClientID%>').val(),
                        "UOM_Name": $('#<%=ddl_uom.ClientID%> option:selected').text(),
                        "UOM": $('#<%=ddl_uom.ClientID%> option:selected').val(),

                        "outstandng": $('#<%=txtoutstanding.ClientID%>').val(),
                        "creditlmt": $('#<%=txtcreditlimit.ClientID%>').val(),
                        "Cus_Alter": ($('input[type="radio"]').prop('checked')) ? 1 : 0,
                        "latitude": $('#<%=txtlat.ClientID%>').val(),
                        "longitude": $('#<%=txtlong.ClientID%>').val(),
                        "DDL_Re_Type": $('#<%=DDL_Re_Type.ClientID%> option:selected').val(),
                        "DFDairyMP": $('#<%=txtDMP.ClientID%>').val(),
                        "MonthlyAI": $('#<%=txtmonA.ClientID%>').val(),
                        "MCCNFPM": $('#<%=txtMFPM.ClientID%>').val(),
                        "MCCMilkColDaily": $('#<%=txtMCL.ClientID%>').val(),
                        "FrequencyOfVisit": $('#<%=ddlfzy.ClientID%>').val(),
                        "Breed": $('#<%=hdnbreedname.ClientID%>').val(),
                        "ukeys": $('#<%=hdnukey.ClientID%>').val(),
                        "curentCom": $('#<%=ddlCC.ClientID%> option:selected').val(),
                        "curentCompitat": $('#<%=ddlCC.ClientID%> option:selected').text(),
                        "Email": $('#<%=txtmail.ClientID%>').val(),
                        "Additionsfld": addfields
                    };

                    console.log(JSON.stringify(fdata));

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/SaveAdditionalField",
                        /* data: JSON.stringify({ retailermainflds }),*/
                        data: "{'fdata':'" + JSON.stringify(fdata) + "'}",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "Updated Successfully") {
                                alert(msg.d);

                                ClearControls();

                                window.location.href = '../Retailer_Details_New.aspx';
                            }
                            else if (msg.d == "Created Successfully") {
                                alert(msg.d);

                                ClearControls();

                                window.location.href = '../Retailer_Details_New.aspx';
                            }
                            else if (msg.d == "Name Already Exist") {
                                alert(msg.d);

                                ClearControls();

                                window.location.href = '../Retailer_Details_New.aspx';
                            }
                            else if (msg.d == "Code Already Exist") {
                                alert(msg.d);

                                ClearControls();

                                window.location.href = '../Retailer_Details_New.aspx';
                            }
                            else if (msg.d == "ERP Code Already Exist") {
                                alert(msg.d);

                                ClearControls();

                                window.location.href = '../Retailer_Details_New.aspx';
                            }
                            else {
                                //alert(msg.d);                                    
                                // window.location.href = '../Retailer_Details.aspx';
                                //ClearControls();
                            }
                        }
                    });
                }

                function GetCutomRetailerData(listeddrcode, columnName) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ListedDr_DetailAdd_Custom.aspx/GetBindCustomFieldData",
                        data: "{'listeddrcode':'" + listeddrcode + "','columnName':'" + columnName + "'}",
                        dataType: "json",
                        success: function (data) {
                            CFBindData = JSON.parse(data.d) || [];
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }                  
                              
                function BindCustomFieldData(listeddrcode) {

                    if (MasFrms.length > 0) {
                        for (var k = 0; k < MasFrms.length; k++) {
                            var $fldnm = MasFrms[k].Field_Col;
                            var fldtyp = MasFrms[k].Fld_Type;
                            GetCutomRetailerData(listeddrcode, $fldnm);
                            if (CFBindData.length > 0) {
                                for (var j = 0; j < CFBindData.length; j++) {

                                    var $cval = CFBindData[j][$fldnm];

                                    switch (fldtyp) {
                                        case 'TA':
                                            $("input[type='text']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    $(this).val($cval);
                                                }
                                            });
                                            break;
                                        case 'TAS':
                                            $("input[type='text']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    $(this).val($cval);
                                                }
                                            });
                                            break;
                                        case 'TAM':
                                            $("textarea").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    $(this).val($cval);
                                                    $(this).value = $cval;
                                                }
                                            });
                                            break;
                                        case 'NC':
                                            $("input[type='number']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    //alert($cval);

                                                    $(this).val($cval);
                                                }
                                            });
                                            break;
                                        case 'NP':
                                            $("input[type='number']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    //alert($cval);
                                                    $(this).val($cval);
                                                }
                                            });
                                            break;
                                        case 'N':
                                            $("input[type='number']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    //alert($cval);
                                                    $(this).val($cval);
                                                }
                                            });
                                            break;
                                        case 'D':
                                            $("input[type='date']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {

                                                    var date = new Date($cval);

                                                    var day = date.getDate();
                                                    var month = date.getMonth() + 1;
                                                    var year = date.getFullYear();

                                                    if (month < 10) month = "0" + month;
                                                    if (day < 10) day = "0" + day;

                                                    var today = year + "-" + month + "-" + day;
                                                    $(this).attr("value", today);

                                                }
                                            });
                                            break;
                                        case 'DR':
                                            $("input[type='date']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    var date = new Date($cval);

                                                    var day = date.getDate();
                                                    var month = date.getMonth() + 1;
                                                    var year = date.getFullYear();

                                                    if (month < 10) month = "0" + month;
                                                    if (day < 10) day = "0" + day;

                                                    var today = year + "-" + month + "-" + day;
                                                    $(this).attr("value", today);

                                                }
                                            });
                                            break;
                                        case 'T':
                                            $("input[type='time']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    var tv = getFormattedTime($cval);

                                                    $(this).val($cval);
                                                }
                                            });
                                            break;
                                        case 'TR':
                                            $("input[type='time']").each(function () {
                                                var id = $(this).attr("id");
                                                if (id == $fldnm) {
                                                    var tv = getFormattedTime($cval);

                                                    $(this).val(tv);
                                                }
                                            });
                                            break;
                                        case 'SSM':
                                            if (($cval == "" || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                $('#' + $fldnm + '').find('option').each(function () {
                                                    var sval = $(this).val();

                                                    if (sval == $cval) {
                                                        $('#' + $fldnm + '').val($cval);
                                                    }
                                                });
                                            }

                                        case 'SSO': console.log($cval);
                                            if (($cval == "" || $cval == '' || $cval == "")) {
                                                
                                            }
                                            else {
                                                $('#' + $fldnm + '').find('option').each(function () {
                                                    //alert($(this).val());
                                                    var sval = $(this).val();

                                                    if (sval == $cval) {
                                                        $('#' + $fldnm + '').val($cval);
                                                    }
                                                });
                                            }
                                            //var id = $(this).attr("id");
                                            //if (id == $fldnm) {
                                            //    $(id).val($cval);
                                            //}
                                            break;
                                        case 'SMM':
                                            console.log($cval);
                                            if (($cval == null || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                var arr = $cval.split(',');

                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];

                                                    $("input[type='checkbox']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                        case 'SMO':
                                            console.log($cval);
                                            if (($cval == null || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                var arr = $cval.split(',');

                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];

                                                    $("input[type='checkbox']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                            break;
                                        case 'CM':
                                            console.log($cval);
                                            if (($cval == null || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                var arr = $cval.split(',');

                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];

                                                    $("input[type='checkbox']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                        case 'CO':
                                            console.log($cval);
                                            if (($cval == null || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                var arr = $cval.split(',');

                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];

                                                    $("input[type='checkbox']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                            break;
                                        case 'RM':
                                            console.log($cval);
                                            if (($cval == null || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                var arr = $cval.split(',');

                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];

                                                    $("input[type='radio']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                        case 'RO':
                                            console.log($cval);
                                            if (($cval == null || $cval == '' || $cval == "")) {

                                            }
                                            else {
                                                var arr = $cval.split(',');

                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];

                                                    $("input[type='radio']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                            break;
                                        //case 'FS':
                                        //    if (($cval == null || $cval == '' || $cval == "")) {

                                        //    }
                                        //    else {
                                        //        $("input[type='file']").each(function () {
                                        //            var id = $(this).attr("id");
                                        //            if (id == $fldnm) {
                                        //                $("." + id + "").empty();
                                        //                var div = "<div> " + $cval + " <br /></div>";
                                        //                //console.log(div);
                                        //                $("." + id + "").append(div);
                                        //            }
                                        //        });                                               
                                        //    }
                                                                                 
                                        //    break;
                                        //case 'FSC':
                                        //    if (($cval == null || $cval == '' || $cval == "")) {

                                        //    }
                                        //    else {
                                        //        $("input[type='file']").each(function () {
                                        //            var id = $(this).attr("id");
                                        //            if (id == $fldnm) {
                                        //                $("." + id + "").empty();
                                        //                var div = "<div> " + $cval + " <br /></div>";
                                        //                //console.log(div);
                                        //                $("." + id + "").append(div);
                                        //            }
                                        //        });      
                                        //    }                                   
                                        //    break;
                                        //case 'FC':
                                        //    if (($cval == null || $cval == '' || $cval == "")) {

                                        //    }
                                        //    else {

                                        //        $("input[type='file']").each(function () {
                                        //            var id = $(this).attr("id");
                                        //            if (id == $fldnm) {
                                        //                $("." + id + "").empty();
                                        //                var div = "<div> " + $cval + " <br /></div>";
                                        //                //console.log(div);
                                        //                $("." + id + "").append(div);
                                        //            }
                                        //        });      
                                        //    }
                                        //    break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }                    
                }

                function getFormattedDate(datev) {

                    var date = new Date(datev);

                    var day = date.getDate();
                    var month = date.getMonth() + 1;
                    var year = date.getFullYear();

                    if (month < 10) month = "0" + month;
                    if (day < 10) day = "0" + day;

                    var today = year + "-" + month + "-" + day;   
         
                    return today;
                }

                function getFormattedTime(time) {

                    var dat = new Date(time);

                    var dd = dat.getDate();
                    var mm = 1 + dat.getMonth();
                    var yy = dat.getFullYear();

                    if (dd < 10) dd = '0' + dd;
                    if (mm < 10) mm = '0' + mm;
                    
                    var dval = dd + '-' + mm + '-' + yy;
                                        
                    var h = dat.getHours();
                    var m = dat.getMinutes();
                    var s = dat.getSeconds();

                    if (h < 10) h = '0' + h;
                    if (m < 10) m = '0' + m;
                    if (s < 10) s = '0' + s;                    

                    var tval = h + ':' + m + ':' + s;

                    return tval;
                }

                function ClearControls() {
                    $('#<%=txtName.ClientID%>').val("");
                    $('#<%=txtMobile.ClientID%>').val("");
                    $('#<%=Txt_id.ClientID%>').val("");
                    $('#<%=txtERBCode.ClientID%>').val("");
                    $('#<%=txtDOW.ClientID%>').val("");
                    $('#<%=salestaxno.ClientID%>').val("");
                    $('#<%=TinNO.ClientID%>').val("");
                    $('#<%=creditdays.ClientID%>').val("");
                    $('#<%=Txt_advanceamt.ClientID%>').val("");
                    $('#<%=txtAddress.ClientID%>').val("");
                    $('#<%=txtStreet.ClientID%>').val("");
                    $('#<%=Txt_Mil_Pot.ClientID%>').val("");
                    $('#<%=txtlat.ClientID%>').val("");
                    $('#<%=txtlong.ClientID%>').val("");
                }

                $('#btngoback').on('click', function () {
                    window.location.href = '../Retailer_Details_New.aspx';
                });
            </script>
        </body>
    </html>
</asp:Content>
