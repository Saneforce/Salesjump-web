<%@ Page Title="Rate Statewise Generation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="ProductRate_New.aspx.cs" Inherits="MasterFiles_ProductRate_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Product Rate</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=ddlState.ClientID%>').val('--Select--');
            $("INPUT[type='checkbox']").each(function () {
                $(this).prop('disabled', false);
                $(this).prop('checked', false);
            });
            //  alert($('input[name="chkbox_states"]:checked').length);
            //   alert($('#<%=chkbox_states.ClientID%> :not(":checked")').length);
            //  $('#<%=chkbox_states.ClientID %> option').each(function (index, item) {
            //      alert($(item).val());
            //   });

            $(document).on('change', '#<%=ddlState.ClientID%>', function () {
                var i = $('option:selected', this).index() - 1;
                var c = 0;
                $("INPUT[type='checkbox']").each(function () {
                    if (i == c) {
                        $(this).prop('checked', true);
                        $(this).prop('disabled', true);
                    }
                    else {
                        $(this).prop('disabled', false);
                        $(this).prop('checked', false);
                    }
                    c++;
                });

            });

            $('#<%=btnSubmit.ClientID%>').click(function () {
                var SName = $('#<%=ddlState.ClientID%> :selected').text();
                if (SName == "--Select--") { alert("Please Select State Name."); $('#<%=ddlState.ClientID%>').focus(); return false; }
                if ($('#<%=chkbox_states.ClientID%> input:checked').length > 1) { return true; } else { alert('Select Update State'); return false; }
                if ($("#txtEffFrom").val() == "") { alert("Please Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
            });
        });

        function Get_Selected_Value() {
            var checked_checkboxes = $("[id*=chkbox_states] input:checked");
            var message = "";
            checked_checkboxes.each(function () {
                var text = $(this).closest("td").find("label").html();
                message += text;
                message += ",";
            });
            document.getElementById("<%= TextBox1.ClientID %>").value = message;
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblState" runat="server" SkinID="lblMand" Width="100px"><span style="color:Red">*</span>State Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
					  <asp:RequiredFieldValidator ID="RFValidator1_ddl" InitialValue="0" runat="server" ErrorMessage="RequiredField" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblEffFrom" runat="server" SkinID="lblMand" Width="100px" Visible="true"><span style="color:Red">*</span>Effective From</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffFrom" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="106px" TabIndex="6" Visible="true"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                 <td align="left">
                        <asp:Label ID="Lab_FO" runat="server" Width="91px" SkinID="lblMand"><span style="color:Red">*</span>Update State</asp:Label>
                    </td>
                    <td align="left">
                 
<%--                        <asp:DropDownList ID="DDL_FO" runat="server" EnableViewState="true" CssClass="DropDownList"
                            Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event); ">
                        </asp:DropDownList>--%>


                         <asp:TextBox ID="TextBox1" SkinID="TxtBxAllowSymb" runat="server" Width="200px" ReadOnly="true" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                        
                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22" >
                </asp:PopupControlExtender>

                <asp:Panel ID="Panel1" runat="server" Height="200px" Width="200px" BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"  ScrollBars="Auto" BackColor="#CCCCCC" >                  
                    <asp:CheckBoxList ID="chkbox_states" runat="server" onclick="Get_Selected_Value();">
                    </asp:CheckBoxList>  

                </asp:Panel>
                

                    </td>
                </tr>


            </table>          
            <%--<br />             
         <table border="0" cellpadding="0" cellspacing="0" id="Table1" align="center" style="width: 67%;">
                <tr>
                    <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white;">
                        &nbsp; State Names&nbsp;
                    </td>
                </tr>
            </table>
            <table align="center" border="1" cellpadding="0" cellspacing="0" style="width: 54%;
                margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                <tr>
                    <td class="style71" align="left">
                        <asp:CheckBoxList ID="chkbox_states" runat="server" DataTextField="statename"
                            CssClass="chkboxLocation" DataValueField="state_code" Font-Names="Verdana"
                            Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="4"
                            RepeatDirection="vertical" Width="753px" TabIndex="29">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <br />--%>
            
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" CssClass="BUTTON" runat="server" Width="70px" Height="25px"
                            Text="Save"  OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>   
            </center>     
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