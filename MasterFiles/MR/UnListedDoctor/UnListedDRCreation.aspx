<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnListedDRCreation.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnListedDRCreation" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Un Listed Customer Creation</title>    
      <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />

    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
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
         .marRight
        {
            margin-right:35px;
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
           function ValidateEmptyValue() {
               var grid = document.getElementById('<%= grdListedDR.ClientID %>');
               if (grid != null) {


                   var isEmpty = false;
                   var Inputs = grid.getElementsByTagName("input");
                   var cnt = 0;
                   var index = '';
                   for (i = 2; i < Inputs.length; i++) {
                       if (Inputs[i].type == 'text') {
                           if (i.toString().length == 1) {
                               index = cnt.toString() + i.toString();
                           }
                           else {
                               index = i.toString();
                           }
                           var DoctorName = document.getElementById('grdListedDR_ctl' + index + '_ListedDR_Name');
                           var Address = document.getElementById('grdListedDR_ctl' + index + '_ListedDR_Address1');
                           var Category = document.getElementById('grdListedDR_ctl' + index + '_ddlCatg');
                           var Speciality = document.getElementById('grdListedDR_ctl' + index + '_ddlspcl');
                           var Qualification = document.getElementById('grdListedDR_ctl' + index + '_ddlQual');
                           var Class = document.getElementById('grdListedDR_ctl' + index + '_ddlClass');
                           var Territory = document.getElementById('grdListedDR_ctl' + index + '_ddlTerr');
                           if (DoctorName.value != '' || Address.value != '' || Category.value != '0' || Speciality.value != '0' || Qualification.value != '0' || Class.value != '0' || Territory.value != '0') {
                               isEmpty = true;
                           }
                           if (DoctorName.value != '') {
                               if (Address.value == '') {
                                   alert('Enter Address')
                                   Address.focus();
                                   return false;
                               }
                               if (Category.value == '0') {
                                   alert('Select Category');
                                   Category.focus();
                                   return false;
                               }
                               if (Speciality.value == '0') {
                                   alert('Select Speciality')
                                   Speciality.focus();
                                   return false;
                               }
                               if (Qualification.value == '0') {
                                   alert('Select Qualification')
                                   Qualification.focus();
                                   return false;
                               }
                               if (Class.value == '0') {
                                   alert('Select Class')
                                   Class.focus();
                                   return false;
                               }
                               if (Territory.value == '0') {
                                   alert('Select Territory')
                                   Territory.focus();
                                   return false;
                               }

                           }
                           if (Address.value != '') {
                               if (DoctorName.value == '') {
                                   alert('Enter Doctor Name')
                                   DoctorName.focus();
                                   return false;
                               }
                               if (Category.value == '0') {
                                   alert('Select Category');
                                   Category.focus();
                                   return false;
                               }
                               if (Speciality.value == '0') {
                                   alert('Select Speciality')
                                   Speciality.focus();
                                   return false;
                               }
                               if (Qualification.value == '0') {
                                   alert('Select Qualification')
                                   Qualification.focus();
                                   return false;
                               }
                               if (Class.value == '0') {
                                   alert('Select Class')
                                   Class.focus();
                                   return false;
                               }
                               if (Territory.value == '0') {
                                   alert('Select Territory')
                                   Territory.focus();
                                   return false;
                               }
                           }
                           if (Category.value != '0') {
                               if (DoctorName.value == '') {
                                   alert('Enter Doctor Name')
                                   DoctorName.focus();
                                   return false;
                               }
                               if (Address.value == '') {
                                   alert('Enter Address')
                                   Address.focus();
                                   return false;
                               }
                               if (Speciality.value == '0') {
                                   alert('Select Speciality')
                                   Speciality.focus();
                                   return false;
                               }
                               if (Qualification.value == '0') {
                                   alert('Select Qualification')
                                   Qualification.focus();
                                   return false;
                               }
                               if (Class.value == '0') {
                                   alert('Select Class')
                                   Class.focus();
                                   return false;
                               }
                               if (Territory.value == '0') {
                                   alert('Select Territory')
                                   Territory.focus();
                                   return false;
                               }
                           }
                           if (Speciality.value != '0') {
                               if (DoctorName.value == '') {
                                   alert('Enter Doctor Name')
                                   DoctorName.focus();
                                   return false;
                               }
                               if (Address.value == '') {
                                   alert('Enter Address')
                                   Address.focus();
                                   return false;
                               }
                               if (Category.value == '0') {
                                   alert('Select Category')
                                   Description.focus();
                                   return false;
                               }
                               if (Qualification.value == '0') {
                                   alert('Select Qualification')
                                   Qualification.focus();
                                   return false;
                               }
                               if (Class.value == '0') {
                                   alert('Select Class')
                                   Class.focus();
                                   return false;
                               }
                               if (Territory.value == '0') {
                                   alert('Select Territory')
                                   Territory.focus();
                                   return false;
                               }
                           }
                           if (Qualification.value != '0') {
                               if (DoctorName.value == '') {
                                   alert('Enter Doctor Name')
                                   DoctorName.focus();
                                   return false;
                               }
                               if (Address.value == '') {
                                   alert('Enter Address')
                                   Address.focus();
                                   return false;
                               }

                               if (Category.value == '0') {
                                   alert('Select Category')
                                   Category.focus();
                                   return false;
                               }
                               if (Speciality.value == '0') {
                                   alert('Enter Speciality')
                                   Speciality.focus();
                                   return false;
                               }
                               if (Class.value == '0') {
                                   alert('Select Class')
                                   Class.focus();
                                   return false;
                               }
                               if (Territory.value == '0') {
                                   alert('Select Territory')
                                   Territory.focus();
                                   return false;
                               }
                           }
                           if (Class.value != '0') {
                               if (DoctorName.value == '') {
                                   alert('Enter Doctor Name')
                                   DoctorName.focus();
                                   return false;
                               }
                               if (Address.value == '') {
                                   alert('Enter Address')
                                   Address.focus();
                                   return false;
                               }
                               if (Category.value == '0') {
                                   alert('Select Category')
                                   Category.focus();
                                   return false;
                               }
                               if (Speciality.value == '0') {
                                   alert('Enter Speciality')
                                   Speciality.focus();
                                   return false;
                               }
                               if (Qualification.value == '0') {
                                   alert('Select Qualification')
                                   Qualification.focus();
                                   return false;
                               }
                               if (Territory.value == '0') {
                                   alert('Select Territory')
                                   Territory.focus();
                                   return false;
                               }
                           }
                           if (Territory.value != '0') {
                               if (DoctorName.value == '') {
                                   alert('Enter Doctor Name')
                                   DoctorName.focus();
                                   return false;
                               }
                               if (Address.value == '') {
                                   alert('Enter Address')
                                   Address.focus();
                                   return false;
                               }
                               if (Category.value == '0') {
                                   alert('Select Category')
                                   Category.focus();
                                   return false;
                               }
                               if (Speciality.value == '0') {
                                   alert('Select Speciality')
                                   Speciality.focus();
                                   return false;
                               }
                               if (Qualification.value == '0') {
                                   alert('Select Qualification')
                                   Qualification.focus();
                                   return false;
                               }
                               if (Class.value == '0') {
                                   alert('Select Class')
                                   Class.focus();
                                   return false;
                               }
                           }
                       }
                   }
                   if (isEmpty) {
                       alert('Enter any one Entry')
                       return true;
                   }
                   else
                       return true;
               }
           }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="Divid" runat="server"></div>
    </div>
    <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
    <div>  
       
          <table id="Table1" runat="server" width="90%">      
            <tr>
                 <td align="right" width="30%">
                   <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
            </tr>
              <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>
            </tr>
            </table>  
            <br />     
        <center>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="grdListedDR" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" GridLines="None"
                            OnRowDataBound="grdListedDR_RowDataBound" OnRowCreated="grdListedDR_RowCreated"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Initial" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIni" runat="server" Text="Dr."></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UnListed Customer Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ListedDR_Name" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event)"
                                            runat="server" Width="130px" Text='<%#Eval("UnListedDR_Name")%>'></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                            ServiceMethod="AutoCompleteAjaxUnlistedDr_Name" 
                                            ServicePath="~/MasterFiles/MR/ListedDoctor/Webservice/AutoComplete.asmx"
                                            MinimumPrefixLength="1" CompletionInterval="100"                                            
                                            EnableCaching="false" CompletionSetCount="10" 
                                            TargetControlID="ListedDR_Name" 
                                            FirstRowSelected="false">
                                       </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ListedDR_Address1" runat="server" onkeypress="AlphaNumeric(event)"
                                            SkinID="TxtBxAllowSymb" MaxLength="250" Width="250px" Text='<%#Eval("UnListedDR_Address1")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired" DataSource="<%# FillCategory() %>"
                                            DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Channel" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlspcl" runat="server" SkinID="ddlRequired" DataSource="<%# FillSpeciality() %>"
                                            DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlQual" runat="server" SkinID="ddlRequired" DataSource="<%# FillQualification() %>"
                                            DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired" DataSource="<%# FillClass() %>"
                                            DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTerr" runat="server" Width="150px" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" />
                        <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </center>
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
