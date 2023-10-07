<%@ Page Title="Distributor Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Distributor_Creation.aspx.cs"
    Inherits="MasterFiles_Distributor_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head>
    <title>Stockist Creation</title>
    
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
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
        .height
        {
            height: 20px;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript" language="javascript">
        function disp_confirm() {
            if (confirm("Do you want to Create the ID ?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript"> var mpsf = [];
        function getMappedSF() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Creation.aspx/getMappedSF",
                dataType: "json",
                success: function (data) {
                    mpsf = JSON.parse(data.d);
					if (mpsf[0].stock_maintain == '1') {
                        $('input#ctl00_ContentPlaceHolder1_chckboxstock').prop('checked', true);
                    }
                }
            });
        }
        function stkchk() {
			if ('<%=Session["div_code"]%>' == '52'  || '<%=Session["div_code"]%>'=='100'){
				var cursf = ($('#<%=TextBox1.ClientID%>').val()).split(',');
				var mappedsf = (mpsf[0].sf_name).split(',');

				if (cursf.length > mappedsf.length) {
					if (confirm("Do you want to transfer Routes?")) {
						$.ajax({
							type: "POST",
							contentType: "application/json; charset=utf-8",
							async: false,
							url: "Distributor_Creation.aspx/transferRoute",
							dataType: "json",
							success: function (data) {
								alert("Route Transferred");
							},
							error: function (response) {
								alert(response);
							}
						});
					}
				}
			}
        }
        $(document).ready(function () {
		getMappedSF();
            $('#pgbk').on('click', function () {
                if ('<%=Session["div_code"]%>' == '32' || '<%=Session["div_code"]%>' == '43' || '<%=Session["div_code"]%>' == '48')
				{
                    window.location.href = "DistributorList.aspx";
                }
                else {
                    window.location.href = "Distributor_Master.aspx";
                }
            });
            //            $('input:checkbox').click(function () {
            //                var $inputs = $('input:checkbox')
            //                if ($(this).is(':checked')) {
            //                    $inputs.not(this).prop('disabled', true); // <-- disable all but checked one
            //                } else {
            //                    $inputs.prop('disabled', false); // <--
            //                }
            //            });
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

			//	if ($('#<%=txtgstn.ClientID%>').val() == "") { alert("Please Enter GSTN No."); $('#<%=txtgstn.ClientID%>').focus(); return false; }


                if ($('#<%=txtStockist_Name.ClientID%>').val() == "") { alert("Please Enter Distributor Name."); $('#<%=txtStockist_Name.ClientID%>').focus(); return false; }
                if ($('#<%=txtStockist_Address.ClientID%>').val() == "") { alert("Please Enter Address."); $('#<%=txtStockist_Address.ClientID%>').focus(); return false; }


                if ($('#<%=txtStockist_Mobile.ClientID%>').val() == "") { alert("Please Enter MobileNo."); $('#<%=txtStockist_Mobile.ClientID%>').focus(); return false; }
               // if ($('#<%=Txt_ERP_Code.ClientID%>').val() == "") { alert("Please Enter ERP Code."); $('#<%=Txt_ERP_Code.ClientID%>').focus(); return false; }


                var type = $('#<%=ddlDist_Name.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select District."); $('#<%=ddlDist_Name.ClientID%>').focus(); return false; }
				var type = $('#<%=ddlTerritoryName.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Territory."); $('#<%=ddlTerritoryName.ClientID%>').focus(); return false; }
                if ($('#<%=TextBox1.ClientID%>').val() == "") { alert("Select FieldForce."); $('#<%=TextBox1.ClientID%>').focus(); return false; }
                if ($('#<%=chkboxLocation.ClientID%> input:checked').length > 0) { return true; } else { alert('Select Division'); return false; }
				


            });
            $('#btnSave').click(function () {
                if ($("#txtStockist_Name").val() == "") { alert("Please Enter Stockist Name."); $('#txtStockist_Name').focus(); return false; }
                if ($("#txtStockist_Address").val() == "") { alert("Please Enter Address."); $('#txtStockist_Address').focus(); return false; }
                if ($("#txtStockist_Mobile").val() == "") { alert("Please Enter MobileNo."); $('#txtStockist_Mobile').focus(); return false; }
              //  if ($("#Txt_ERP_Code").val() == "") { alert("Please Enter ERP Code."); $('#Txt_ERP_Code').focus(); return false; }
                var type = $('#<%=ddlDist_Name.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select District."); $('#ddlDist_Name').focus(); return false; }
                var type = $('#<%=ddlTerritoryName.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Territory."); $('#ddlTerritoryName').focus(); return false; }
                alert($("#TextBox1").val());
                if ($("#TextBox1").val() == "") { alert("Select FieldForce."); $('#TextBox1').focus(); return false; }
                if ($('#chkboxLocation input:checked').length > 0) { return true; } else { alert('Select Subdivision'); return false; }

            });
        });
       function CheckAll(thisObject) {
            var listBox = document.getElementById('<%= DDL_FO.ClientID %>');
        var inputItems = listBox.getElementsByTagName("input");
        for (index = 0; index < inputItems.length; index++) {
            if (inputItems[index].type == 'checkbox') {
                inputItems[index].checked = thisObject.checked;
                var checked_checkboxes = $("[id*=DDL_FO] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    var text = $(this).closest("td").find("label").html();
                    message += text;
                    message += ",";
                });

                document.getElementById("<%= TextBox1.ClientID %>").value = message;

            }
        }
    }

 function myFunction() {
            var x = document.getElementById("<%= Txt_Password.ClientID %>");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
      

        function Get_Selected_Value() {


            var checked_checkboxes = $("[id*=DDL_FO] input:checked");
            var message = "";
            checked_checkboxes.each(function () {            
                var text = $(this).closest("td").find("label").html();
                message += text;
                message += ",";
            });
           
            document.getElementById("<%= TextBox1.ClientID %>").value = message;


          
        }





    </script>
    <style type="text/css">
        #tblStockistDetails
        {
            margin-left: 330px;
        }
        #tblSalesforceDtls
        {
            margin-left: 330px;
        }
        #btnSubmit
        {
            margin-right: 330px;
        }
        .div_fixed
        {
            position: fixed;
            top: 400px;
            right: 5px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
	<button type="button" id="pgbk" style="float:right;" class="btn btn-primary">Back</button>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
                        <asp:TextBox ID="fakeusernameremembered1" runat="server" style="display:none">
                        </asp:TextBox>
						<asp:TextBox ID="fakepasswordremembered1" runat="server" style="width: 0px;height: 0px;position: absolute;top: -35px;" TextMode="Password">
                        </asp:TextBox>
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>
        <table id="Table2" runat="server" width="85%">
            <tr>
                <td align="right" width="30%">
                    <%--   <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana"
                        Visible="true"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack1" CssClass="BUTTON" Text="Back" runat="server" OnClick="btnBack_Click" Width="80px" Visible="false" />
                </td>
            </tr>
        </table>
        <br />
        <center>
            <table width="65%" border="0" cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="Lab_Stockist_Code" runat="server" Visible="false" Width="130px" SkinID="lblMand"><span style="color:Red"></span>Distributor Code</asp:Label>
                        <asp:Label ID="Lab_ERP_Code" runat="server" Width="130px" SkinID="lblMand">ERP Code</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Stockist_Code" runat="server" Visible="false" Width="161px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                        <asp:TextBox ID="Txt_ERP_Code" runat="server" Width="161px" TabIndex="7" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStockist_Address" runat="server" Text="Address" Width="99px" SkinID="lblMand"><span style="color:Red">*</span>Address</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Address" runat="server" SkinID="TxtBxAllowSymb" Width="297px"
                            TabIndex="2" onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White' "
                            MaxLength="150" onkeypress="AlphaNumeric(event);"> 
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="lblStockistName" runat="server" Width="130px" SkinID="lblMand"><span style="color:Red">*</span>Distributor Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Name" runat="server" Width="161px" TabIndex="3" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStockist_Designation" runat="server" Text="Designation" Width="91px"
                            SkinID="lblMand"><span style="color:Red"></span>Designation</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Desingation" runat="server" SkinID="TxtBxAllowSymb"
                            Width="150px" TabIndex="4" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White' " MaxLength="15" onkeypress="AlphaNumeric_NoSpecialCharshq(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="lblStockist_ContactPerson" runat="server" Text="Contact Person" Width="91px"
                            SkinID="lblMand"><span style="color:Red"></span>ContactPerson</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_ContactPerson" runat="server" Width="224px" TabIndex="5"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="lblStockist_Mobile" runat="server" Text="Mobile No" Width="87px" SkinID="lblMand"><span style="color:Red">*</span>Mobile No</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStockist_Mobile" runat="server" Width="170px" TabIndex="6" SkinID="TxtBxNumOnly"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="CheckNumeric(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="Label4" SkinID="lblMand" runat="server" > Email</asp:Label>
                    </td>
                    <td align="left">
                    <asp:TextBox ID="txtemail" runat="server"  Width="170px" TabIndex="11" 
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Visible="true"><span style="color:Red"></span>Norm Value</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Norm" runat="server" Width="161px" TabIndex="8" SkinID="TxtBxNumOnly"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="CheckNumeric(event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="Lab_User_Name" runat="server" Text="User Name" Width="87px" SkinID="lblMand"><span style="color:Red"></span>User Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_User_Name" runat="server" Width="170px" TabIndex="10" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb">
                        </asp:TextBox>
                    </td>
                    <td style="width: 92px;" align="left">
                        <asp:Label ID="Lab_Password" runat="server" Text="Password" Width="87px" SkinID="lblMand"><span style="color:Red"></span>Password</asp:Label>
                    </td>
                    <td align="left">
					<div class='image'>
                        <asp:TextBox ID="Txt_Password" runat="server" Width="170px" TabIndex="11" TextMode="Password"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb">
                        </asp:TextBox>
						<span class='ab'><i class="fa fa-eye" onclick="myFunction()"></i></span>
                         </div>
                    </td>
                </tr>
                <tr>
			<asp:UpdatePanel ID="OuterUpdatePanel" runat="server">
                     <ContentTemplate>
                    <td align="left">
                        <asp:Label ID="lblTerritory" runat="server" Width="91px" SkinID="lblMand"><span style="color:Red">*</span>Territory</asp:Label>
                    </td>
                    <td align="left">
                       
                        <asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="DropDownList"
                            Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            OnSelectedIndexChanged="ddlTerritoryName_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
				</ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        <asp:Label ID="Lab_FO" runat="server" Width="91px" SkinID="lblMand"><span style="color:Red">*</span>Field Officer</asp:Label>
                    </td>
                    <td align="left">
                 
<%--                        <asp:DropDownList ID="DDL_FO" runat="server" EnableViewState="true" CssClass="DropDownList"
                            Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event); ">
                        </asp:DropDownList>--%>


                         <asp:TextBox ID="TextBox1" SkinID="TxtBxAllowSymb" runat="server" Width="200px" ReadOnly="true" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                        
                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22" >
                </asp:PopupControlExtender>
                 

                <asp:Panel ID="Panel1" runat="server" Height="200px" Width="200px" BorderStyle="Solid" style="display:none"  BorderWidth="2px" Direction="LeftToRight"  ScrollBars="Auto" BackColor="#CCCCCC" >                  
                    <asp:CheckBox ID="chkBox" runat="server" Text="Select All" OnClick="CheckAll(this);" />
				  <asp:CheckBoxList ID="DDL_FO" runat="server"  onclick="Get_Selected_Value();"  >
                    </asp:CheckBoxList>              

                  </asp:Panel>

        </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl_Dist_Name" runat="server" SkinID="lblMand" Visible="true"><span style="color:Red">*</span>District Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDist_Name" runat="server" SkinID="ddlRequired" Visible="true"
                            TabIndex="9">
                            <asp:ListItem Text="--Select--" Selected="True" Value="-1">           
                            </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                  <td align="left">
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Visible="true"><span style="color:Red"></span>Head Quarters</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txtheadquarters" runat="server" Width="170px" TabIndex="11" 
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb">
                        </asp:TextBox>
                    </td>

                </tr>
				          <tr>
                       <td align="left">
                        <asp:Label ID="Label5" runat="server" SkinID="lblMand" Visible="true">Taluk</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="txttaluk" runat="server" SkinID="ddlRequired" Visible="true"
                            TabIndex="9">
                           <asp:ListItem Text="--Select--" Selected="True" Value="-1">           
                            </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="Lab_category" SkinID="lblMand" runat="server" > Category</asp:Label>
					
                    </td>
					 <td class="space" align="left">
                         <asp:DropDownList ID="DDL_category" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
						 
                    </td>
                </tr>
  				<tr align="left">
							<td><asp:Label ID="Label3" runat="server" SkinID="lblMand" Visible="true"><span style="color:Red">*</span>Type</asp:Label></td><td>   <asp:DropDownList ID="ddltype" runat="server" SkinID="ddlRequired" TabIndex="4">
                            <asp:ListItem Text="--Select--" Selected="True" Value="-1"> </asp:ListItem>
                            <asp:ListItem Text="Warehouse"  Value="0"> </asp:ListItem>
                               <asp:ListItem Text="Stockist"  Value="1">   </asp:ListItem>        
                            </asp:DropDownList></td>
					<td>
						<asp:Label ID="lblgstn" runat="server" SkinID="lblMand" Visible="true">GSTN No.</asp:Label></td>
					<td align="left">
                        <asp:TextBox ID="txtgstn" runat="server" Width="170px" TabIndex="11" 
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                             onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb">
                        </asp:TextBox>                   
					</td>
				</tr>
				<tr align="left">
							<td><asp:Label ID="Label6" runat="server" SkinID="lblMand" Visible="true"><span style="color:Red">*</span>Rate</asp:Label></td>                   
					<td>

                         <asp:DropDownList ID="ddlRate" runat="server" EnableViewState="true" CssClass="DropDownList"
                            Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                             AutoPostBack="true"> 
						<asp:ListItem Value="0">--Select--</asp:ListItem>							 
                        </asp:DropDownList>		

					</td>
					<td class="space" align="left">
                        <asp:Label ID="lbl_stock" SkinID="lblMand" runat="server" >Stock </asp:Label>
                    </td>
					<td class="space" align="left">
                         		<asp:CheckBox ID="chckboxstock" runat="server"	/>			 
                    </td>		
					
				</tr>				

      






            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 67%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white;">
                        &nbsp;Division&nbsp;
                    </td>
                </tr>
            </table>
            <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 54%;
                margin-bottom: 0px;  margin-top: 15px;">
                <tr>
                    <td class="style71" align="left">
                        <asp:CheckBoxList ID="chkboxLocation" runat="server" DataTextField="subdivision_name"
                            CssClass="chkboxLocation" DataValueField="subdivision_code" Font-Names="Verdana"
                            Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="4"
                            RepeatDirection="vertical" Width="753px" TabIndex="29" OnSelectedIndexChanged="chkboxLocation_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            
            <asp:CheckBoxList ID="chkboxSalesforce" runat="server" DataTextField="sf_Name" DataValueField="sf_code"
                RepeatColumns="2" RepeatDirection="Vertical" Width="753px" TabIndex="7" Style="font-size: x-small;
                color: black; font-family: Verdana;">
            </asp:CheckBoxList>
            <asp:HiddenField ID="HidStockistCode" runat="server" />
            <center>
                <table>
                    <tr>
                        
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md"
                                Text="Save"  OnClientClick="stkchk()" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </center>
        <div class="div_fixed">
           
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
    