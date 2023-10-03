<%@ Page Title="Supplier Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="SupplierCreation.aspx.cs" Inherits="MasterFiles_SupplierCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Supplier Creation</title>
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
            z-index: 999;</style>
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
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
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
            $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=txtSup_Sname.ClientID%>').val() == "") { alert("Please Enter Supplier Name."); $('#<%=txtSup_Sname.ClientID%>').focus(); return false; }
                if ($('#<%=txtSupCon_Name.ClientID%>').val() == "") { alert("Please Enter Contact Person."); $('#<%=txtSupCon_Name.ClientID%>').focus(); return false; }
                if ($('#<%=txtMob_No.ClientID%>').val() == "") { alert("Enter Mobile No."); $('#<%=txtMob_No.ClientID%>').focus(); return false; }
                if ($('#<%=txt_ERP.ClientID%>').val() == "") { alert("Enter ERP Code."); $('#<%=txt_ERP.ClientID%>').focus(); return false; }
                var type = $('#<%=ddlTerritoryName.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select State"); $('#<%=ddlTerritoryName.ClientID%>').focus(); return false; }
                //if ($('#<%=txtusr.ClientID%>').val() == "") { alert("Enter the Username"); $('#<%=txtusr.ClientID%>').focus(); return false; }
               // if ($('#<%=txtpass.ClientID%>').val() == "") { alert("Enter the Password"); $('#<%=txtpass.ClientID%>').focus(); return false; }
            });
        });
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
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
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
        #txtaddr{
            Height:100px ;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
   <asp:TextBox ID="fakeusernameremembered1" runat="server" style="display:none">
                        </asp:TextBox>
						<asp:TextBox ID="fakepasswordremembered1" runat="server" style="width: 0px;height: 0px;position: absolute;top: -35px;" TextMode="Password">
                        </asp:TextBox>
    <div>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDocCatDtls">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblShortName" runat="server" SkinID="lblMand" Height="19px" Width="120px"><span style="Color:Red">*</span>Supplier Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSup_Sname" TabIndex="1" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" Width="200px"  onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivisionName" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Contact Person</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSupCon_Name" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="50" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>	
                <tr>
                    <td class="stylespc" align="left">
                        <asp:Label ID="lblUserName" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Mobile No</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txtMob_No" runat="server"  Width="144px" MaxLength="10"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            TabIndex="3" CssClass="TEXTAREA" SkinID="TxtBxNumOnly" onkeypress="CheckNumeric(event);"
                            Height="16px"></asp:TextBox>
                    </td>
                   </tr>
                   <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblPassword" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>ERP Code</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txt_ERP" runat="server" SkinID="MandTxtBox" MaxLength="50"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="163px" TabIndex="4" CssClass="TEXTAREA" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                    </td>
                </tr><tr>
                 <td align="left">
                        <asp:Label ID="lblTerritory" runat="server" Width="91px" SkinID="lblMand"><span style="color:Red">*</span>State</asp:Label>
                    </td>
                    <td align="left">
                       
                        <asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="DropDownList"
                            Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:DropDownList>
                    </td>
                    </tr>
                <tr>
                 <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Field Force</asp:Label>
                    </td>
                <td align="left"> 

                         <asp:TextBox ID="TextBox1" SkinID="TxtBxAllowSymb" runat="server" Width="300px" ReadOnly="true" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                        
                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22" >
                </asp:PopupControlExtender>

                <asp:Panel ID="Panel1" runat="server" Height="200px" Width="300px" BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"  ScrollBars="Auto" BackColor="#CCCCCC" >                  
                    <asp:CheckBoxList ID="DDL_FO" runat="server" onclick="Get_Selected_Value();"  >
                    </asp:CheckBoxList>         

                </asp:Panel>
                </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblusr" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Username</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtusr" TabIndex="1" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" Width="200px"  onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblpass" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Password</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtpass" style="background-color: white; border-style: groove; border-radius: 5px;" TextMode="Password"  runat="server" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lbladdr" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Address</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtaddr" onfocus="this.style.backgroundColor='#E0EE9D'" TextMode="MultiLine"
                            onblur="this.style.backgroundColor='White'"  Rows="3" Columns="40" runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md"
                Text="Save" OnClick="btnSubmit_Click" />
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