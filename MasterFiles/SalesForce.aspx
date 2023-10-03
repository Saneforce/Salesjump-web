<%@ Page Title="Field Force Master" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="SalesForce.aspx.cs" Inherits="MasterFiles_SalesForce" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Sales Force Master</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .div_fixed
        {
            position: fixed;
            top: 400px;
            right: 5px;
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
        .chkboxLocation label
        {
            padding-left: 5px;
        }
        
        .height
        {
            height: 20px;
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        function Vacant() {
            if (confirm('Do you want to Vacant this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function Promote() {
            if (confirm('Do you want to Promote this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function BaseLevelPromote() {
            if (confirm('Do you want to Promote this user?')) {
                return true;
            }

            else {
                return false;
            }
        }
        function DePromote() {
            if (confirm('Do you want to De-Promote this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function Block() {
            if (confirm('Do you want to Block this user?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

    </script>
    <style type="text/css">
        .TEXTAREA
        {
            margin-left: 0px;
        }
        .style9
        {
            height: 27px;
            width: 173px;
        }
        .style26
        {
            width: 173px;
        }
        .style38
        {
            width: 173px;
            height: 31px;
        }
        #tblpanel
        {
            height: 14px;
            width: 77%;
            margin-left: 0px;
        }
        .style57
        {
            width: 620px;
        }
        .Radio
        {
            margin-left: 0px;
        }
        #Table1
        {
            width: 78%;
            //margin-left: 0px;
        }
        .style65
        {
            width: 236px;
            height: 21px;
        }
        .style66
        {
            height: 31px;
            width: 219px;
        }
        .style67
        {
            width: 219px;
        }
        .style68
        {
            height: 27px;
            width: 219px;
        }
        .style70
        {
            width: 350px;
        }
        .style71
        {
            width: 270px;
        }
        .style72
        {
            height: 31px;
            width: 1367px;
        }
        .style73
        {
            width: 1367px;
        }
        .style75
        {
            width: 169px;
        }
        .style76
        {
            height: 27px;
            width: 1588px;
        }
        .style77
        {
            height: 27px;
            width: 1367px;
        }
        
        .style78
        {
            height: 19px;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
        function validateRadio() {
            var RB1 = document.getElementById("<%=rdoMode.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked) {
                alert("Select Mode");
            }
            return isChecked;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            if ($('#<%=FFType.ClientID%>').val() == "2") {
                $('#<%=txtcnfdate.ClientID%>').css('visibility', 'visible');
                $('#<%=lblcnfdate.ClientID%>').css('visibility', 'visible');
            }
            else {
                $('#<%=txtcnfdate.ClientID%>').css('visibility', 'hidden');
                $('#<%=lblcnfdate.ClientID%>').css('visibility', 'hidden');
            }

            $('input:checkbox').click(function () {
                var $inputs = $('input:checkbox')
                if ($(this).is(':checked')) {
                    $inputs.not(this).prop('disabled', false); // <-- disable all but checked one
                } else {
                    $inputs.prop('disabled', false); // <--
                }
            });
            //$('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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



            $('#<%=FFType.ClientID%>').change(function () {
                if ($(this).val() == 1) {
                    $('#<%=txtcnfdate.ClientID%>').css('visibility', 'hidden');
                    $('#<%=lblcnfdate.ClientID%>').css('visibility', 'hidden');

                }
                else {
                    $('#<%=txtcnfdate.ClientID%>').css('visibility', 'visible');
                    $('#<%=lblcnfdate.ClientID%>').css('visibility', 'visible');
                }
            });

            $('#<%=btnSubmit.ClientID%>').click(function () {

                if ($('#<%=FFType.ClientID%>').val() == "2") {
                    if ($('#<%=txtcnfdate.ClientID%>').val() == "") {
                        alert('Enter Confirm Date..!');

                        $('#<%=txtcnfdate.ClientID%>').focus();
                        return false;
                    }
                }

                if ($('#<%=txtFieldForceName.ClientID%>').val() == "") { alert("Enter FieldForce Name."); $('#<%=txtFieldForceName.ClientID%>').focus(); return false; }
                var type = $('#<%=ddlFieldForceType.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Type."); $('#<%=ddlFieldForceType.ClientID%>').focus(); return false; }
                var sta = $('#<%=ddlState.ClientID%> :selected').text();
                if (sta == "---Select---") { alert("Select State."); $('#<%=ddlState.ClientID%>').focus(); return false; }

                var type = $('#<%=ddlReporting.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Reporting To."); $('#<%=ddlReporting.ClientID%>').focus(); return false; }
                var Des = $('#<%=txtDesignation.ClientID%> :selected').text();
                if (Des == "---Select---") { alert("Select Designation."); $('#<%=txtDesignation.ClientID%>').focus(); return false; }
                if ($('#<%=txtUserName.ClientID%>').val() == "") { alert("Enter User Name."); $('#<%=txtUserName.ClientID%>').focus(); return false; }
                if ($('#<%=txtPassword.ClientID%>').val() == "") { alert("Enter Password."); $('#<%=txtPassword.ClientID%>').focus(); return false; }
                if ($('#<%=txtJoingingDate.ClientID%>').val() == "") { alert("Enter Joining Date."); $('#<%=txtJoingingDate.ClientID%>').focus(); return false; }
                if ($('#<%=txtTPDCRStartDate.ClientID%>').val() == "") { alert("Enter Reporting Start Date."); $('#<%=txtTPDCRStartDate.ClientID%>').focus(); return false; }
                if ($('#<%=txtReason.ClientID%>').val() == "") { alert("Enter Reason for Blocking the ID."); $('#<%=txtReason.ClientID%>').focus(); return false; }
                if ($('#<%=txteffe.ClientID%>').val() == "") { alert("Enter Effective Date."); $('#<%=txteffe.ClientID%>').focus(); return false; }
                if ($('#<%=UsrDfd_UserName.ClientID%>').val() == "") { alert("Enter UserDefined UserName."); $('#<%=UsrDfd_UserName.ClientID%>').focus(); return false; }
                var sta = $('#<%=ddlTerritoryName.ClientID%> :selected').text();
                if (sta == "--Select--") { alert("Select Territory."); $('#<%=ddlTerritoryName.ClientID%>').focus(); return false; }
                if ($('#<%=txtMobile.ClientID%>').val() == "") { alert("Enter Your Mobile No."); $('#<%=txtMobile.ClientID%>').focus(); return false; }

                if ($('#<%=chkboxLocation.ClientID%> input:checked').length > 0) { return true; } else { alert('Select Division'); return false; }

            });
            $('#<%=btnSave.ClientID%>').click(function () {

                if ($('#<%=FFType.ClientID%>').val() == "2") {
                    if ($('#<%=txtcnfdate.ClientID%>').val() == "") {
                        alert('Enter Confirm Date..!');

                        $('#<%=txtcnfdate.ClientID%>').focus();
                        return false;
                    }
                }
                if ($('#<%=txtFieldForceName.ClientID%>').val() == "") { alert("Enter FieldForce Name."); $('#<%=txtFieldForceName.ClientID%>').focus(); return false; }
                var type = $('#<%=ddlFieldForceType.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Type."); $('#<%=ddlFieldForceType.ClientID%>').focus(); return false; }
                var sta = $('#<%=ddlState.ClientID%> :selected').text();
                if (sta == "---Select---") { alert("Select State."); $('#<%=ddlState.ClientID%>').focus(); return false; }

                var type = $('#<%=ddlReporting.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Reporting To."); $('#<%=ddlReporting.ClientID%>').focus(); return false; }
                var Des = $('#<%=txtDesignation.ClientID%> :selected').text();
                if (Des == "---Select---") { alert("Select Designation."); $('#<%=txtDesignation.ClientID%>').focus(); return false; }
                if ($('#<%=txtUserName.ClientID%>').val() == "") { alert("Enter User Name."); $('#<%=txtUserName.ClientID%>').focus(); return false; }
                if ($('#<%=txtPassword.ClientID%>').val() == "") { alert("Enter Password."); $('#<%=txtPassword.ClientID%>').focus(); return false; }
                if ($('#<%=txtJoingingDate.ClientID%>').val() == "") { alert("Enter Joining Date."); $('#<%=txtJoingingDate.ClientID%>').focus(); return false; }
                if ($('#<%=txtTPDCRStartDate.ClientID%>').val() == "") { alert("Enter Reporting Start Date."); $('#<%=txtTPDCRStartDate.ClientID%>').focus(); return false; }
                if ($('#<%=txtReason.ClientID%>').val() == "") { alert("Enter Reason for Blocking the ID."); $('#<%=txtReason.ClientID%>').focus(); return false; }
                if ($('#<%=txteffe.ClientID%>').val() == "") { alert("Enter Effective Date."); $('#<%=txteffe.ClientID%>').focus(); return false; }
                if ($('#<%=UsrDfd_UserName.ClientID%>').val() == "") { alert("Enter UserDefined UserName."); $('#<%=UsrDfd_UserName.ClientID%>').focus(); return false; }
                var sta = $('#<%=ddlTerritoryName.ClientID%> :selected').text();
                if (sta == "--Select--") { alert("Select Territory."); $('#<%=ddlTerritoryName.ClientID%>').focus(); return false; }
                if ($('#<%=txtMobile.ClientID%>').val() == "") { alert("Enter Your Mobile No."); $('#<%=txtMobile.ClientID%>').focus(); return false; }
                if ($('#<%=chkboxLocation.ClientID%> input:checked').length > 0) { return true; } else { alert('Select Division'); return false; }

            });
        }); 
    	</script>
		<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
		
    <script type="text/javascript">
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
                alert("Please select Territory");
            }
            else {
                return true;
            }
            return false;
        }
    </script>
    <script type="text/javascript">

        function checkAll(obj1) {
            var checkboxCollection = document.getElementById('<%=chkboxLocation.ClientID %>').getElementsByTagName('input');
            for (var i = 0; i < checkboxCollection.length; i++) {
                if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {

                    checkboxCollection[i].checked = obj1.checked;

                }
            }
        }  
      
    </script>
    <form id="form1" runat="server">
   <asp:TextBox ID="fakeusernameremembered1" runat="server" style="display:none">
                        </asp:TextBox>
						<asp:TextBox ID="fakepasswordremembered1" runat="server" style="width: 0px;height: 0px;position: absolute;top: -35px;" TextMode="Password">
                        </asp:TextBox>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>

        <center>
            <br />
            <table border="1" cellpadding="0" cellspacing="0" id="Table2" align="center" style="width: 67%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white">
                        &nbsp; Login & Joining Details&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table align="center" border="0" cellpadding="3" style="height: 230px; width: 60%;">
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblFieldForceName" runat="server" SkinID="lblMand" Width="120px"><span style="Color:Red">*</span>FieldForce Name</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtFieldForceName" runat="server" SkinID="MandTxtBox" MaxLength="50"
                                    TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    Width="268px" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                    Height="21px"></asp:TextBox>
                            </td>
                            <td align="left">
                            </td>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblFieldForceType" runat="server" SkinID="lblMand" Width="130px"><span style="Color:Red">*</span>Type</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:DropDownList ID="ddlFieldForceType" runat="server" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" AutoPostBack="true"
                                    TabIndex="2" OnSelectedIndexChanged="ddlFieldForceType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblStateName" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>State Name</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" MaxLength="15"
                                    onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"
                                    SkinID="ddlRequired" TabIndex="3" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblHQ" runat="server" SkinID="lblMand"><span style="Color:Red"></span>HQ</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtHQ" runat="server"  SkinID="TxtBxNumOnly" Width="164px" MaxLength="30"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    TabIndex="4" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialCharshq(event);"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <%--<td align="left" class="stylespc">
                                <asp:Label ID="lblHQ" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>HQ</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtHQ" runat="server" SkinID="TxtBxNumOnly" Width="164px" MaxLength="30"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    TabIndex="4" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialCharshq(event);"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblDesignation" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Designation</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <%--      <asp:TextBox ID="txtDesignation" runat="server" SkinID="TxtBxNumOnly" MaxLength="100"
                                onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'" 
                                Width="162px" TabIndex="6" CssClass="TEXTAREA"  onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDesignation" runat="server" AutoPostBack="true" MaxLength="15"
                                    onblur="this.style.backgroundColor='White'" onfocus="this.style.backgroundColor='#E0EE9D'"
                                    SkinID="ddlRequired" TabIndex="5" OnSelectedIndexChanged="txtDesignation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblReporting" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Reporting To</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:DropDownList ID="ddlReporting" runat="server" AutoPostBack="true" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" TabIndex="6">
                                    <%--  <asp:ListItem Selected="true" Value="">---Select Mgr---</asp:ListItem> --%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblUserName" runat="server" SkinID="lblMand" Visible="True">Field Force Type</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtUserName" runat="server" Visible="false" SkinID="MandTxtBox" Width="144px" MaxLength="100"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    TabIndex="7" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                    Height="16px"></asp:TextBox>
                                      <asp:DropDownList ID="FFType" runat="server"  onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" TabIndex="6">
                                    <asp:ListItem Selected="true" Value="1" >Probationary</asp:ListItem> 
                                    <asp:ListItem Value="2">Confirm</asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                             <td class="stylespc" align="left">
                                <asp:Label ID="lblcnfdate" runat="server" SkinID="lblMand" Visible="True"><span style="Color:Red">*</span>Confirm Date</asp:Label>
                            </td>
                             <td class="stylespc" align="left">
                                <asp:TextBox ID="txtcnfdate" runat="server" Height="25px" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);"
                                    SkinID="MandTxtBox" TabIndex="9" Width="145px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" runat="server" TargetControlID="txtcnfdate" />
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblJoingingDate" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Joining Date</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtJoingingDate" runat="server" Height="25px" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);"
                                    SkinID="MandTxtBox" TabIndex="9" Width="145px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtJoingingDate" />
                            </td>
                            <td>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblTPDCRStartDate" runat="server" SkinID="lblMand" Width="140px"><span style="Color:Red">*</span>Report Starting Date</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtTPDCRStartDate" runat="server" Height="25px" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);"
                                    SkinID="TxtBxNumOnly" TabIndex="10" Width="125px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTPDCRStartDate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblEmployeeID" runat="server" SkinID="lblMand"><span style="Color:Red"></span>Employee ID</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtEmployeeID" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    Width="147px" TabIndex="11" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                    Height="16px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTerritory" runat="server" Width="91px" SkinID="lblMand"><span style="color:Red">*</span>Territory</asp:Label>
                            </td>
                            <td align="left">
                               <asp:DropDownList ID="ddlTerritoryName" runat="server"  CssClass="DropDownList"
                                    SkinID="ddlRequired" AutoPostBack="true"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                                    onselectedindexchanged="ddlTerritoryName_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:Label ID="Label7" runat="server" Visible="false" SkinID="lblMand"><span style="Color:Red">*</span>Status</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:RadioButtonList ID="RblSta" CssClass="Radio" runat="server" Visible="false"
                                    RepeatColumns="3" TabIndex="12" Font-Names="Verdana" Font-Size="X-Small">
                                    <asp:ListItem Value="0" Selected="True">&nbsp;Active </asp:ListItem>
                                    <asp:ListItem Value="1">&nbsp;Vacant</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;Block</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblLastDCRDate" runat="server" Visible="False" SkinID="lblMand" ForeColor="#0033CC"><span style="Color:Red">*</span>Last DCR Date</asp:Label>
                            </td>
                             

                            <td class="stylespc" align="left">
                                <%--      <asp:TextBox ID="txtDCRDate" runat="server" Height="18px" onblur="this.style.backgroundColor='White'"
                        onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="MandTxtBox" Visible="false"
                        Width="125px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDCRDate" />  --%>
                                <asp:Label ID="txtDCRDate" runat="server" SkinID="lblMand" Font-Bold="true" Visible="false"></asp:Label>
                                 </td>
                            <td>
                            </td>
                                 <td align="left" class="stylespc">
                                <asp:Label ID="lblPassword" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Password</asp:Label>
                           
                            </td>
                            <td class="stylespc" align="left">
							
                                <asp:TextBox ID="txtPassword" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"
                                    TextMode="Password" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    Width="163px" TabIndex="8" CssClass="TEXTAREA" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                            </td>
                            
                           
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblVacantBlock" runat="server" Text="Vacant Block" Visible="false"
                                    SkinID="lblMand"></asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:RadioButtonList ID="rblVacantBlock" CssClass="Radio" runat="server" Visible="false"
                                    RepeatColumns="2" TabIndex="13" Font-Names="Verdana" Font-Size="X-Small">
                                    <asp:ListItem Value="R" Selected="True">Resigned</asp:ListItem>
                                    <%--     <asp:ListItem Value="P">Promotion</asp:ListItem>--%>
                                    <%--    <asp:ListItem Value="T">Transfered</asp:ListItem> --%>
                                    <%--     <asp:ListItem Value="D">Depromotion</asp:ListItem> --%>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblReason" runat="server" Text="Reason for Blocking the ID:" SkinID="lblMand"
                                    Font-Bold="true" Width="120px" Visible="False" ForeColor="#0033CC"></asp:Label>


							  <asp:Label ID="lblStatus" runat="server" Text="Status" SkinID="lblMand"
                                    Font-Bold="true" Width="120px"  ForeColor="#0033CC"></asp:Label>
                          
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txtReason" runat="server" SkinID="TxtBxNumOnly" Visible="false"
                                    MaxLength="300" TabIndex="14" Placeholder="Type Here" onfocus="this.style.backgroundColor='#E0EE9D'"
                                    onblur="this.style.backgroundColor='White'" TextMode="MultiLine" Width="254px"
                                    CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
							
								<asp:DropDownList ID="ddlStatus" runat="server"  CssClass="DropDownList"
                                    SkinID="ddlRequired" AutoPostBack="true"  onselectedindexchanged="ddlStatus_SelectedIndexChanged" >
								<asp:ListItem Text="Active" Value="0"></asp:ListItem>
								<asp:ListItem Text="Vacant" Value="1"></asp:ListItem>
								<asp:ListItem Text="Deactivate" Value="2"></asp:ListItem>
								<asp:ListItem Text="Block" Value="3"></asp:ListItem>
								<asp:ListItem Text="Independent" Value="4"></asp:ListItem>
                                </asp:DropDownList>
							  
                            </td>
                            <td>


                            </td>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lblUI" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>UserDefined UserName:</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="UsrDfd_UserName" runat="server" SkinID="MandTxtBox" Width="144px"
                                    MaxLength="100" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    TabIndex="15" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                    Height="16px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lbleffective" runat="server" Visible="false" SkinID="lblMand"><span style="Color:Red">*</span>Effective Date</asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:TextBox ID="txteffe" runat="server" Height="25px" onblur="this.style.backgroundColor='White'"
                                    Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'" onkeypress="Calendar_enterBa(event);"
                                    SkinID="MandTxtBox" TabIndex="9" Width="145px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" runat="server" TargetControlID="txteffe" />
                            </td>
                            <td>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lbloldusername" runat="server" Text="&nbsp;Old ID" SkinID="lblMand"
                                    Visible="false"></asp:Label>
                            </td>
                            <td class="stylespc" align="left">
                                <asp:Label ID="lbluserdefi" runat="server" SkinID="lblMand" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        </tr>
                        <tr>
                            <td class="stylespc" align="left">
                                <asp:Label ID="Label4" runat="server" SkinID="lblMand" Visible="false"><span style="Color:Red">*</span>FieldForce Type:</asp:Label>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:DropDownList ID="ddl_fftype" runat="server" SkinID="ddlRequired" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" TabIndex="4" Enabled="false" Visible="false">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Company</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblmode" runat="server" Font-Bold="true" ForeColor="#8B100A" Text="Mode "
                                    Visible="false"></asp:Label>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:RadioButtonList ID="rdoMode" runat="server" AutoPostBack="true" Font-Names="Verdana"
                                    Font-Size="11px" OnSelectedIndexChanged="rdoMode_SelectedIndexChanged" RepeatDirection="Horizontal"
                                    Visible="false">
                                    <asp:ListItem Text="New Id&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Replacement" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:Label ID="lblreplace" runat="server" Font-Bold="true" SkinID="lblMand" Text="Replacement For (Only Vacant Manager ID's)"
                                    Visible="false"></asp:Label>
                            </td>
                            <td align="left" class="stylespc">
                                <asp:DropDownList ID="ddlrepla" runat="server" onblur="this.style.backgroundColor='White'"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" TabIndex="6"
                                    Visible="false">
                                    <%--  <asp:ListItem Selected="true" Value="">---Select Mgr---</asp:ListItem> --%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </tr>
                    </table>
                    <%-- <table border="1" cellpadding="3" cellspacing="3">
                        <tr>
                            <td width="12%">
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTitle_LocationDtls" runat="server" Width="210px" Text="Select the Territory"
                                    TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="Small" ForeColor="#8A2EE6">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="ChkAll" runat="server" Text=" All" OnCheckedChanged="CheckBox1_CheckedChanged"
                                    AutoPostBack="true" />
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="chkboxLocation" DataTextField="Territory_name"
                                    DataValueField="Territory_Code" AutoPostBack="true" RepeatDirection="Vertical" RepeatColumns="4" Width="650px"
                                    TabIndex="7" 
                                    Style="font-size: x-small; color: black; font-family: Verdana;" 
                                    onselectedindexchanged="CheckBoxList1_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 67%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white;">
                        &nbsp; Division&nbsp;
                    </td>
                </tr>
            </table>
            <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 54%;">
                <tr>
                    <td class="style71" align="left">
                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="subdivision_name"
                            CssClass="chkboxLocation" DataValueField="subdivision_code" Font-Names="Verdana"
                            Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="4"
                            RepeatDirection="vertical" Width="753px" TabIndex="29">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" cellpadding="0" cellspacing="0" id="Table3" align="center" style="width: 67%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white;">
                        &nbsp; Contact Details&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <table align="center" border="0" cellpadding="3" style="height: 230px; width: 60%;">
                <tr>
                    <td class="stylespc" align="left">
                        <%--  <table align="center" border="0" cellpadding="3" cellspacing="3" 
                
                        style="height: 272px; width: 60%; margin-left: 173px; margin-right: 220px; margin-top: 2px;">
                     <tr> 
                     <td class="style72" >--%>
                        <asp:Label ID="lblDOB" runat="server" Text="DOB" SkinID="lblMand" Width="40px"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDOB" runat="server" Height="18px" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onkeypress="Calendar_enter(event);"
                            SkinID="TxtBxNumOnly" TabIndex="16" Width="127px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDOB" />
                    </td>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDOW" runat="server" Text="DOW" SkinID="lblMand" Width="20px"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDOW" runat="server" Height="18px" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onkeypress="Calendar_enter(event);"
                            SkinID="TxtBxNumOnly" TabIndex="17" Width="128px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDOW" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblPhone1" runat="server" Text="Phone No" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPhone1" runat="server" SkinID="MandTxtBox" MaxLength="12" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="175px" TabIndex="18" CssClass="TEXTAREA"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                    </td>
                    <td class="style76" align="left">
                    </td>
                    <td align="left">
                        <asp:Label ID="lblMobile" runat="server"  SkinID="lblMand" Width="100px"><span style="Color:Red">*</span>Mobile No</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMobile" runat="server" Width="162px" TabIndex="19" MaxLength="12"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            SkinID="MandTxtBox" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblEMail" runat="server" Text="EMail" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEMail" runat="server" SkinID="MandTxtBox" Width="168px" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" TabIndex="20" CssClass="TEXTAREA"></asp:TextBox>
                    </td>
                    <td class="style76">
                    </td>
                </tr>
                <tr>
                    <td class="height">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="Present Address" Font-Bold="True" Font-Size="Small"
                            Font-Underline="True" Width="130px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="height">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblAddress1" runat="server" Text="Address1" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddress1" runat="server" SkinID="MandTxtBox" MaxLength="150"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            Width="281px" TabIndex="21" CssClass="TEXTAREA" onkeypress="AlphaNumeric(event);"
                            Height="26px"></asp:TextBox>
                    </td>
                    <td class="style76">
                    </td>
                    <td align="left">
                        <asp:Label ID="lblAddress2" runat="server" Text="Address2" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddress2" runat="server" SkinID="TxtBxNumOnly" MaxLength="150"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            Width="251px" TabIndex="22" CssClass="TEXTAREA" onkeypress="AlphaNumeric(event);"
                            Height="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCityPin" runat="server" Text="City / Pincode" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtCityPin" runat="server" SkinID="MandTxtBox" MaxLength="50" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="193px" TabIndex="23" CssClass="TEXTAREA"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="22px"></asp:TextBox>
                    </td>
                    <td class="style76">
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPhone2" runat="server" Text="Phone No" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPhone2" runat="server" Width="162px" TabIndex="24" MaxLength="12"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            SkinID="MandTxtBox" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="height">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" Text="Permanent Address" Font-Bold="True" Font-Size="Small"
                            Font-Underline="True" Width="130px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="height">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblPerAddress1" runat="server" Text="Address1" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPerAddress1" runat="server" SkinID="MandTxtBox" MaxLength="150"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            Width="279px" TabIndex="25" CssClass="TEXTAREA" onkeypress="AlphaNumeric(event);"
                            Height="31px"></asp:TextBox>
                    </td>
                    <td class="style76">
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPerAddress2" runat="server" Text="Address2" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPerAddress2" runat="server" SkinID="TxtBxNumOnly" MaxLength="150"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            Width="249px" TabIndex="26" CssClass="TEXTAREA" onkeypress="AlphaNumeric(event);"
                            Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblPerCityPin" runat="server" Text="City / PinCode" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPerCityPin" runat="server" SkinID="MandTxtBox" MaxLength="50"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            Width="178px" TabIndex="27" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                    </td>
                    <td class="style76">
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone No" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPhone" runat="server" SkinID="TxtBxNumOnly" MaxLength="12" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="163px" TabIndex="28" CssClass="TEXTAREA"
                            onkeypress="CheckNumeric(event);"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <%--    <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 67%;
           ">
            <tr>
                <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white;">
                    &nbsp; Sub Division&nbsp;
                </td>
            </tr>
        </table>
       
        <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 54%;
            margin-bottom: 0px; margin-right: 0px; margin-top:15px;">
            <tr>
                <td class="style71" align="left">
                    <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="subdivision_name" CssClass="chkboxLocation"  
                        DataValueField="subdivision_code" Font-Names="Verdana" Font-Bold ="true" ForeColor ="BlueViolet"    Font-Size="X-Small" RepeatColumns="4"
                        RepeatDirection="vertical" Width="753px" TabIndex="29">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>--%>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server"  Text="Submit"
                OnClick="btnSubmit_Click" OnClientClick="return validateRadio()" TabIndex="26"
                CssClass="btn btn-success btn-md" Style="margin-right: 70px; margin-left: 0px;" />
        </center>
        <%--
               </td>
                      </tr>
                      </table>--%>
        <div class="div_fixed">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-md"
                Text="Submit" OnClick="btnSave_Click" OnClientClick="return validateRadio()" />
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>