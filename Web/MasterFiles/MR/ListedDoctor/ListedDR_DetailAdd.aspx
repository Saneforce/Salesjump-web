<%@ Page Title="Listed Retailer Detail Add" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="ListedDR_DetailAdd.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_ListedDR_DetailAdd" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>--%>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Listed Retailer Detail Add</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
        <link href="/css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <style type="text/css">
 .ms-options{
            width:19% !important;
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
        
        
        .space
        {
            padding: 3px 3px;
        }
        .sp
        {
            padding-left: 11px;
        }
        
        .marRight
        {
            margin-right: 35px;
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
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
 $('#example-multiple-selected').hide()
            $('#<%=lblDMP.ClientID%>').hide();
            $('#<%=txtDMP.ClientID%>').hide()
            $('#<%=lblCC.ClientID%>').hide();
            $('#<%=ddlCC.ClientID%>').hide()
            $('#<%=lblmonA.ClientID%>').hide()
            $('#<%=txtmonA.ClientID%>').hide()  
            $('#<%=lblMCL.ClientID%>').hide()
            $('#<%=txtMCL.ClientID%>').hide()
            $('#<%=lblMFPM.ClientID%>').hide()
            $('#<%=txtMFPM.ClientID%>').hide()            
            $('#<%=lblfzy.ClientID%>').hide()
            $('#<%=ddlfzy.ClientID%>').hide()
            $('#<%=Label4.ClientID%>').hide()
            $('#<%=creditdays.ClientID%>').hide()
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

              //  var categ = $('#<%=DDL_category.ClientID%> :selected').text();
               // if (categ == "---Select---") { alert("Select Category..!"); $('#<%=DDL_category.ClientID%>').focus(); return false; }


             
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

                var charCode = (evt.which) ? evt.which : event.keyCode

                if (
                        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // �.� CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

  if ($('#<%=divcode.ClientID%>').val() == 70) {
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "ListedDR_DetailAdd.aspx/Fillddlbreed",
                    //   data: "{'Retailer_ID':'" + retailer_ID + "'}",
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
                    $('#<%=Label4.ClientID%>').show()
                    $('#<%=creditdays.ClientID%>').show()
                    $('#<%=lblDMP.ClientID%>').show()
                    $('#<%=txtDMP.ClientID%>').show()
                    $('#<%=lblMCL.ClientID%>').hide()
                    $('#<%=txtMCL.ClientID%>').hide()
                    $('#<%=lblMFPM.ClientID%>').hide()
                    $('#<%=txtMFPM.ClientID%>').hide()
                    $('#<%=lblmonA.ClientID%>').hide()
                    $('#<%=txtmonA.ClientID%>').hide()
                    $('#<%=lblCatg.ClientID%>').text('*Breed')
                    $('#<%=lblCatg.ClientID%>').show()
                   //   $('#example-multiple-selected').show()
                  <%--  $('#<%=ddlCatg.ClientID%>').multiselect({
                        includeSelectAllOption: true
                    });--%>

                  }
                   else {
                    $('#<%=Label4.ClientID%>').hide()
                    $('#<%=creditdays.ClientID%>').hide()
                    $('#<%=lblDMP.ClientID%>').hide();
                    $('#<%=txtDMP.ClientID%>').hide()
                    }
                    if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'AIT') {
                        $('#<%=lblmonA.ClientID%>').show()
                        $('#<%=txtmonA.ClientID%>').show()
                        $('#<%=lblMCL.ClientID%>').hide()
                        $('#<%=txtMCL.ClientID%>').hide()
                        $('#<%=lblMFPM.ClientID%>').hide()
                        $('#<%=txtMFPM.ClientID%>').hide()
                        $('#<%=lblDMP.ClientID%>').hide()
                        $('#<%=txtDMP.ClientID%>').hide()
                        $('#<%=Label4.ClientID%>').hide()
                        $('#<%=creditdays.ClientID%>').hide()
                      //  $('#example-multiple-selected').hide()
                    }
                    else {
                        $('#<%=lblmonA.ClientID%>').hide()
                        $('#<%=txtmonA.ClientID%>').hide()
                    }
                    if ($('#<%=ddlSpec.ClientID%> option:selected').text() == 'MCC') {
                        $('#<%=lblMCL.ClientID%>').show()
                        $('#<%=txtMCL.ClientID%>').show()
                        $('#<%=lblMFPM.ClientID%>').show()
                        $('#<%=txtMFPM.ClientID%>').show()
                        $('#<%=lblmonA.ClientID%>').hide()
                        $('#<%=txtmonA.ClientID%>').hide()
                        $('#<%=lblDMP.ClientID%>').hide()
                        $('#<%=txtDMP.ClientID%>').hide()
                        $('#<%=Label4.ClientID%>').hide()
                        $('#<%=creditdays.ClientID%>').hide()
                        //$('#example-multiple-selected').hide()
                    }
                    else {
                        $('#<%=lblMCL.ClientID%>').hide()
                        $('#<%=txtMCL.ClientID%>').hide()
                        $('#<%=lblMFPM.ClientID%>').hide()
                        $('#<%=txtMFPM.ClientID%>').hide()
                    }
                });
              
            }


        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
	 <asp:ScriptManager ID="scriptmanager1" runat="server">
            </asp:ScriptManager>
    <div>
 <asp:HiddenField ID="hdnukey" runat="server"  value="" />
           <asp:HiddenField ID="hdnbreedname" runat="server"  value="" />
        <asp:HiddenField ID="divcode" runat="server"  value="" />
 <%--                <input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" onclick="history.back(-2)" />--%>
                <button id="bac" type="button" class="btn btn-warning" style="margin-left: 90%; margin-top: -1%;"><a href="../Retailer_Details.aspx">BACK</a></button>
<table width="95%" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td>
                    <table id="Table2" runat="server" width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                    text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" style="width: 45%">
                                <asp:Label ID="lblHeading" Text="Listed Retailer Detail Add" runat="server" Visible="false" CssClass="under" Style="text-transform: capitalize;
                                    font-size: 14px; text-align: center;" ForeColor="#336277" Font-Bold="True" Font-Names="Verdana">
                                </asp:Label>
                            </td>
                            <td align="right" class="style3" style="width: 55%">
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Visible="false" Height="25px" Width="60px"
                                    Text="Back"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="Divid" runat="server">
        </div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
            <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <table id="Table1" runat="server" width="90%">
            <tr>
                <td align="right" width="30%">
                    <%--   <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana"
                        Visible="true"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack1" CssClass="BUTTON" Text="Back" Visible="false" runat="server" OnClick="btnBack_Click" />

                </td>
            </tr>
        </table>
        <br />
        <center>
		<asp:UpdatePanel ID="updatepnl" runat="server">
                        <ContentTemplate>
           <table width="62%" bgcolor="#D6E9C6">
                <tr>
                    <td colspan="4" align="left" style="border: 1px solid; background-color: #7AA3CC;">
                        <asp:Label ID="lblHead" runat="server" Text="  Personal Profile" Font-Size="14px"
                            Font-Names="Verdana" ForeColor="White" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                   <td class="space" align="left">
                        <asp:Label ID="lblQual" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Retailer Code</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="Txt_id" runat="server"></asp:TextBox>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblDOB" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red"></span>Mobile No</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtMobile" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
 					<td class="space" align="left">
                        <asp:Label ID="lblName" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Name of Retailer</asp:Label>
                        &nbsp;
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtName" runat="server" Width="170px" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event)" onblur="this.style.backgroundColor='White'">
                        </asp:TextBox>
                    </td>
                    
                    <td class="space" align="left">
                        <asp:Label ID="lblDOW" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red"></span>Contact Person Name</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <%-- <asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'" ReadOnly="true"
                    onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>   
               <asp:CalendarExtender ID="Caldow" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOW"> </asp:CalendarExtender>--%>
                        <asp:TextBox ID="txtDOW" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblSpec" runat="server" SkinID="lblMand" Font-Bold="True"><span style="Color:Red">*</span>Channel </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:DropDownList ID="ddlSpec" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="Label3" SkinID="lblMand" runat="server" Text="GST No " Font-Bold="True"></asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="salestaxno" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblCatg" runat="server" SkinID="lblMand" Font-Bold="True">Sales TaxNo  </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="TinNO" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                      <%--  <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired" >                          
                        </asp:DropDownList>--%>
               <%--  <asp:ListBox ID="ddlCatg" runat="server" SelectionMode="Multiple">  </asp:ListBox>--%>
                         <select class="form-control poiinter" id="example-multiple-selected" multiple>
                                    <%--          <option value="0">---Select All--</option>--%>
                                </select>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblTerritory" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Route</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:DropDownList ID="ddlTerritory" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                    <td class="space" align="left" style="display:none">
                        <asp:DropDownList ID="terri" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="Label4" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>Credit Days</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="creditdays" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                   
                    <td class="space" align="left">
                        <asp:Label ID="lblClass" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red">*</span>Class </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>

 <tr><td class="space" align="left">
                        <asp:Label ID="lbloutstanding" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>Outstanding</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtoutstanding" runat="server" SkinID="MandTxtBox" CssClass="numberVal" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
             
                <td class="space" align="left">
                        <asp:Label ID="lblcreditlimit" SkinID="lblMand" runat="server" Font-Bold="True"><span style="Color:Red"></span>Credit Limit</asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtcreditlimit" runat="server" SkinID="MandTxtBox" CssClass="numberVal" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
   </tr>

  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="Advanceamount" SkinID="lblMand" runat="server" Font-Bold="True">Advance Amount </asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="Txt_advanceamt" CssClass="numberVal" runat="server" SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'"></asp:TextBox>
                    </td>
                    <td class="space" align="left">
                        
						 <asp:Label ID="Lab_Type" SkinID="lblMand" runat="server" Font-Bold="True" Visible="true"><span style="Color:Red"></span>Retailer Type</asp:Label>
                    </td>
                    <td class="space" align="left">
                         <asp:DropDownList ID="DDL_Re_Type" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
				<tr>
					<td class="space" align="left">
                        <asp:Label ID="Lab_Milk_Potential" SkinID="lblMand" runat="server" Font-Bold="True" > Potential</asp:Label>
					
                    </td>
					 <td class="space" align="left">
                        <asp:TextBox ID="Txt_Mil_Pot" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal"></asp:TextBox>
						 
                    </td>

					<td class="space" align="left">
                        <asp:Label ID="Lab_category" SkinID="lblMand" runat="server" Font-Bold="True" > Category</asp:Label>
					
                    </td>
					 <td class="space" align="left">
                         <asp:DropDownList ID="DDL_category" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
						 
                    </td>
				 </tr>
                <tr>
                <td class="space" align="left">
                        <asp:Label ID="lblDMP" SkinID="lblMand" runat="server" Font-Bold="True" > Daily Milk Production (In Ltrs) </asp:Label>
					
                    </td>

					 <td class="space" align="left">
                        <asp:TextBox ID="txtDMP" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal"></asp:TextBox>
						 
                    </td>
 <td class="space" align="left">
                        <asp:Label ID="lblCC" SkinID="lblMand" runat="server" Font-Bold="True" >Current Competitor </asp:Label>
					
                    </td>
					 <td class="space" align="left">
                       <asp:DropDownList ID="ddlCC" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td class="space" align="left">
                        <asp:Label ID="lblmonA" SkinID="lblMand" runat="server" Font-Bold="True" >   Monthly Ais  </asp:Label>
					
                    </td>

					 <td class="space" align="left">
                        <asp:TextBox ID="txtmonA" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal"></asp:TextBox>
						 
                    </td>
 <td class="space" align="left">
                        <asp:Label ID="lblfzy" SkinID="lblMand" runat="server" Font-Bold="True" > Frequency </asp:Label>
					
                    </td>
					 <td class="space" align="left">
                        <asp:DropDownList ID="ddlfzy" runat="server" SkinID="ddlRequired">                           
                        </asp:DropDownList>                        
                    </td>
                </tr>
                <tr>
                <td class="space" align="left">
                        <asp:Label ID="lblMCL" SkinID="lblMand" runat="server" Font-Bold="True" >  Milk Collection Ltrs (Daily)  </asp:Label>
					
                    </td>

					 <td class="space" align="left">
                        <asp:TextBox ID="txtMCL" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal"></asp:TextBox>
						 
                    </td>
 <td class="space" align="left">
                        <asp:Label ID="lblMFPM" SkinID="lblMand" runat="server" Font-Bold="True" > Number of Farmers Pouring Milk  </asp:Label>
					
                    </td>
					 <td class="space" align="left">
                        <asp:TextBox ID="txtMFPM" runat="server"  SkinID="MandTxtBox" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='LavenderBlush'" CssClass="numberVal"></asp:TextBox>
						 
                    </td>
                </tr>
 				<tr>
                <td class="space" align="left">
                        <asp:Label ID="Lab_UOM" SkinID="lblMand" runat="server" Visible="false" Font-Bold="True"><span style="Color:Red">*</span>UOM</asp:Label>
                        <asp:Label ID="lblERBCode" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">ERP Code</asp:Label>
                    </td>
                   <td class="space" align="left">
                        <asp:DropDownList ID="ddl_uom" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtERBCode" runat="server"></asp:TextBox>
                    </td>
					 <td class="space" align="left">
                        <asp:Label ID="Lab_Alt" SkinID="lblMand" runat="server"  Font-Bold="True"><span style="Color:black">CustomerWise Alter</span></asp:Label>
                    </td>
                   <td class="space" align="left">
                         <asp:RadioButtonList ID="RblAlt" CssClass="Radio" runat="server" RepeatColumns="3"
                            Font-Names="Verdana" Font-Size="X-Small">
                            <asp:ListItem Value="1" Selected="True">ON &nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">OFF &nbsp;&nbsp;</asp:ListItem>
                            <%--  <asp:ListItem Value="O">Others</asp:ListItem>--%>
                        </asp:RadioButtonList>
					
                    </td>
                  <td>
	<asp:Label ID="Lab_Alt_Msg" SkinID="lblMand" runat="server"  Font-Bold="True"><span style="Color:Red">(Note : More Then 3 Bill Show Alter)</span></asp:Label>
</td>
                </tr>
<tr>
                        <td class="space" align="left">
                        <asp:Label ID="lbllat" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">Latitude</asp:Label>
                         </td>
                     <td class="space" align="left">
                           <asp:TextBox ID="txtlat" runat="server"></asp:TextBox>
                         </td>
                     <td class="space" align="left">
                        <asp:Label ID="lbllong" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">Longitude</asp:Label>
                         </td>
                     <td class="space" align="left">
                           <asp:TextBox ID="txtlong" runat="server"></asp:TextBox>
                         </td>
                </tr>
               <tr>
                        <td class="space" align="left">
                        <asp:Label ID="Label1" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True">Email</asp:Label>
                         </td>
                     <td class="space" align="left">
                           <asp:TextBox ID="txtmail" runat="server"></asp:TextBox>
                         </td>
               </tr>
                <tr>
                    <td colspan="4" align="left" style="border: 1px solid; background-color: #7AA3CC;">
                        <asp:Label ID="lblHeadAddress" runat="server" ForeColor="White" Text=" Address  "
                            Font-Size="14px" Font-Names="Verdana" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="space" align="left">
                        <span style="color: Red">*</span><asp:Label ID="Label2" runat="server" ForeColor="White"
                            Text=" Billing Address" Style="border: 1px solid; background-color: #7AA3CC;" Font-Size="12px"
                            Font-Names="Verdana" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtAddress" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="211px" Height="200px"></asp:TextBox>
                    </td>
                    <td class="space" align="left">
                        <asp:Label ID="Label10" runat="server" ForeColor="White" Text=" Shipping Address" Style="border: 1px solid;
                            background-color: #7AA3CC;" Font-Size="12px" Font-Names="Verdana" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="space" align="left">
                        <asp:TextBox ID="txtStreet" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White'" Width="211px" Height="200px" ></asp:TextBox>
                    </td>
                </tr>
            </table>
			</ContentTemplate>
                    </asp:UpdatePanel>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Width="60px" Height="25px" Text="Save" OnClick="btnSave_Click"
                            CssClass="BUTTON" />&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear"
                            OnClick="btnClear_Click" CssClass="BUTTON" />
                    </td>
                </tr>
            </table>
        </center>
        <div class="loading" align="center">
            Loading. Please wait. Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>