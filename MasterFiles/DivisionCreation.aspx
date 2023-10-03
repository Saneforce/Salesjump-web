<%@ Page Title="Company Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DivisionCreation.aspx.cs"
    Inherits="MasterFiles_DivisionCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Company Creation</title>
    <style type="text/css">
        #tblDivisionDtls
        {
            margin-left: 300px;
        }
        #tblLocationDtls
        {
            margin-left: 300px;
        }
        .style2
        {
            width: 92px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
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
        .padding
        {
            padding:3px;
        }
        td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
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
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=txtDivision_Sname.ClientID%>').val() == "") { alert("Enter Code."); $('#<%=txtDivision_Sname.ClientID%>').focus(); return false; }
                if ($('#<%=txtDivision_Name.ClientID%>').val() == "") { alert("Enter Company Name."); $('#<%=txtDivision_Name.ClientID%>').focus(); return false; }
                if ($('#<%=txtDivision_Add1.ClientID%>').val() == "") { alert("Enter Address1."); $('#<%=txtDivision_Add1.ClientID%>').focus(); return false; }
                if ($('#<%=txtCity.ClientID%>').val() == "") { alert("Enter City."); $('#<%=txtCity.ClientID%>').focus(); return false; }
                if ($('#<%=txtPincode.ClientID%>').val() == "") { alert("Enter Pincode."); $('#<%=txtPincode.ClientID%>').focus(); return false; }
                if ($('#<%=txtAlias.ClientID%>').val() == "") { alert("Enter Alias Name."); $('#<%=txtAlias.ClientID%>').focus(); return false; }
                if ($('#<%=txtYear.ClientID%>').val() == "") { alert("Enter Year."); $('#<%=txtYear.ClientID%>').focus(); return false; }
                if ($('#<%=txtWeekOff.ClientID%>').val() == "") { alert("Select Weak Off."); $('#<%=txtWeekOff.ClientID%>').focus(); return false; }
				var SName = $('#<%=ddlInd_type.ClientID%> :selected').text();
                if (SName == "--Select--") { alert("Please Select Industrial Type.");
				$('#<%=ddlInd_type.ClientID%>').focus(); return false; }
				if ($('#<%=txtGST.ClientID%>').val() == "") { alert("Enter GST No"); $('#<%=txtGST.ClientID%>').focus(); return false; }
                if ($('#<%=chkboxLocation.ClientID%> input:checked').length > 0) { return true; } else { alert('Select State'); return false; }
				//OnClientClick="return confirm('Do you want to Update State in this Master');"
				return confirm('Do you want to Update State in this Master');
            });
        });
    </script>
    <%--<script type="text/javascript">
        function ValidateCheckBoxList() {
            var listItems = document.getElementById("chkboxLocation").getElementsByTagName("input");
            var itemcount = listItems.length;
            var iCount = 0;
            var isItemSelected = false;
            for (iCount = 0; iCount < itemcount; iCount++) {
                if (listItems[iCount].checked) {
                    isItemSelected = true;
                    break;
                }
            }
            if (!isItemSelected) {
                alert("Please select State");
            }
            else {
                return true;
            }
            return false;
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    <input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" onclick="history.back(-2)" />
        <center>
            <br />
            <table border="0" cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td style="width: 80px" align="left" class="stylespc">
                        <asp:Label ID="lblShortName" runat="server" Width="86px" SkinID="lblMand"><span style="color:Red">*</span>Company Code</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtDivision_Sname" SkinID="MandTxtBox" runat="server" Width="114px" 
                            TabIndex="1" MaxLength="3" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            onkeypress="CharactersOnly(event);">                      
                        </asp:TextBox>
                        <asp:Label ID="lblCap" runat="server" Font-Size="XX-Small" Font-Names="Arial" Text="(For ID Creation)">
                        </asp:Label>
                    </td>
                    <td style="width:30px" >
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivisionName" runat="server" Width="110px"
                            SkinID="lblMand"><span style="color:Red">*</span>Company Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtDivision_Name" runat="server" Width="297px" TabIndex="2" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric(event);"
                            SkinID="MandTxtBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="stylespc" align="left" >
                        <asp:Label ID="lblAddress1" runat="server" Width="91px" SkinID="lblMand"><span style="color:Red">*</span>Address 1</asp:Label>
                    </td>
                    <td class="stylespc">
                        <asp:TextBox ID="txtDivision_Add1" runat="server" SkinID="TxtBxAllowSymb" Width="224px"
                            TabIndex="3" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            MaxLength="150" onkeypress="AlphaNumeric(event);">
                        </asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:Label ID="lblAddress2" runat="server" Text="Address 2" Width="91px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtDivision_Add2" runat="server" Width="252px" TabIndex="4" MaxLength="150"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_SpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px" align="left" class="stylespc">
                        <asp:Label ID="lblCity" runat="server" Width="87px" SkinID="lblMand"><span style="color:Red">*</span>City</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtCity" runat="server" SkinID="TxtBxChrOnly" CssClass="TEXTAREA"
                            TabIndex="5" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            MaxLength="20" onkeypress="CharactersOnly(event);">
                        </asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblPincode" runat="server" Width="99px" SkinID="lblMand"><span style="color:Red">*</span>Pin Code</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtPincode" runat="server" SkinID="TxtBxNumOnly" TabIndex="6" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="6" Width="88px" onkeypress="CheckNumeric(event);">
                        </asp:TextBox>
                        <asp:Label ID="lblAlias" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Alias Name</asp:Label>
                        <asp:TextBox ID="txtAlias" runat="server" Width="80" SkinID="TxtBxChrOnly" CssClass="TEXTAREA"
                            TabIndex="7" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            MaxLength="8" onkeypress="CharactersOnly(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px" align="left" class="stylespc">
                        <asp:Label ID="lblYear" runat="server" Width="87px" SkinID="lblMand"><span style="color:Red">*</span>Year</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="40px" SkinID="TxtBxNumOnly"
                            CssClass="TEXTAREA" TabIndex="8" onkeypress="CheckNumeric(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'">
                        </asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblWeekOff" runat="server" Width="99px" SkinID="lblMand"><span style="color:Red">*</span>Week Off</asp:Label>
                        <%--<asp:DropDownList ID="ddlWeek" runat="server">
                        <asp:ListItem Text="Select" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                    </asp:DropDownList>--%>
                    </td>
                    <td align="left">
                        <asp:UpdatePanel>
                            <ContentTemplate>
                                <asp:TextBox ID="txtWeekOff" ReadOnly="true" SkinID="TxtBoxStyle" Width="150px" runat="server"></asp:TextBox>
                                <asp:PopupControlExtender ID="txtWeek_PopupControlExtender" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="txtWeekOff" PopupControlID="Panel1"
                                    OffsetY="22">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="Panel1" runat="server" Height="116px" Width="150px" BorderStyle="Solid"
                                    BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                    Style="display: none">
                                    <asp:CheckBoxList ID="Chkweek" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Chkweek_SelectedIndexChanged">
                                        <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                     <td style="width: 92px" align="left" class="stylespc">
                    
                    <asp:Label ID="LblInd_type" runat="server" SkinID="lblMand" 
                            Height="18px" Width="100px"><span style="Color:Red">*</span>Industrial Types</asp:Label>
                            
                   </td>
                    
                    <td>
                     <asp:DropDownList ID="ddlInd_type" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
<td>
</td>
<td style="width: 92px" align="left" class="stylespc">
					<asp:Label ID="Lblgst" runat="server" SkinID="lblMand" 
                            Height="18px" Width="100px"><span style="Color:Red">*</span>GST No</asp:Label>                            
                   </td>

					<td align="left" class="stylespc">
                        <asp:TextBox ID="txtGST" runat="server" MaxLength="16" Width="100px" SkinID="TxtBxNumOnly"
                            CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);" onkeyup="return event.charCode!= 32" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" >
                        </asp:TextBox>
                    </td>

                </tr>
               
            </table>
           
            <table   align="center" >
                <tr>
                    <td rowspan="" class="padding" align="center">
                        <asp:Label ID="lblTitle_LocationDtls" runat="server"  Text="Select the State/Location" Visible="false" 
                            TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Underline="true" Font-Names="Verdana"
                            Font-Size="Small" ForeColor="#336277">
                        </asp:Label>
                         <span runat="server" id="spState" style="font-weight:bold; text-decoration:underline; border-style:none; font-family:Verdana; border-color:#E0E0E0; color :#336277">Select the<asp:LinkButton ID="lnk" runat="server" Text=" " onclick="lnk_Click"></asp:LinkButton>State/Location</span>

                    </td>
                </tr>
              <tr style="height: 5px">
                    <td style="width: 92px; height: 5px">
                    </td>
                </tr>
                <tr class="padding">
                    <td style="width: 450px; height: 10px" align="left" >
                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="State_Name" DataValueField="State_Code"
                            Font-Names="Verdana" Font-Size="11px"  RepeatColumns="4" RepeatDirection="vertical"
                            Width="810px" TabIndex="7">
                        </asp:CheckBoxList>
                        <asp:HiddenField ID="HidDivCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md"
                            Text="Save" OnClick="btnSubmit_Click"  />
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
        <asp:Panel ID="pnlNew" runat="server" Visible="false" >
            <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px #347C17;
                border-collapse: collapse" >
                <tr>
                    <td style="background-color: #98AFC7; color: White; font-weight: bold; font-family: Arial;"
                        align="center">
                        Assign the New Company
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"
                            BackColor="#FEFCFF" ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="600px"
                            Font-Names="Calibri">
                            <asp:ListItem Value="0" Text="Product Category"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Product Group"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Product Detail"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Doctor Category"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Doctor Specialty"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Doctor Qualification"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Doctor Class"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Setup"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Flash News"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Designation"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Holiday"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="background: white">
                        <asp:Button ID="btnProcess" runat="server" Text="Process" Font-Names="Arial" Font-Bold="true"
                            BackColor="#98AFC7" ForeColor="White" onclick="btnProcess_Click" />
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
        <br />
    </div>
    </form>
</body>
</html>
</asp:Content>
