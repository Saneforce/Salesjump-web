<%@ Page Title="Quick Add Retailer Creation" Language="C#" AutoEventWireup="true" CodeFile="ListedDRCreation1.aspx.cs" MasterPageFile="~/Master.master"
    Inherits="MasterFiles_MR_ListedDRCreation" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu1" TagPrefix="ucl1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Listed Customer Creation</title>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
            margin-right: 35px;
        }
        .normal
        {
            background-color: white;
        }
        .highlight_clr
        {
            background-color: LightBlue;
        }
        .clp
        {
            border-collapse: collapse;
            background-color: White;
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
    </script>
    <script type="text/javascript">
        function ValidateEmptyValue() {

            var grid = document.getElementById('<%= grdListedDR.ClientID %>');

            if (grid != null) {

                var Inputs = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                var isEntry = false;
                for (i = 2; i < Inputs.length; i++) {
                    if (Inputs[i].type == 'text') {
                        if (i.toString().length == 1) {

                            index = cnt.toString() + i.toString();
                        }
                        else {

                            index = i.toString();
                        }
                        var isEmpty = false;
                        var DoctorName = document.getElementById('grdListedDR_ctl' + index + '_ListedDR_Name');
                        var Address = document.getElementById('grdListedDR_ctl' + index + '_ListedDR_Address1');
                        var Category = document.getElementById('grdListedDR_ctl' + index + '_ddlCatg');
                        var Speciality = document.getElementById('grdListedDR_ctl' + index + '_ddlspcl');
                        var Qualification = document.getElementById('grdListedDR_ctl' + index + '_ddlQual');
                        var Class = document.getElementById('grdListedDR_ctl' + index + '_ddlClass');
                        var Territory = document.getElementById('grdListedDR_ctl' + index + '_ddlTerr');
                        if (DoctorName.value != '' && Address.value != '' && Category.value != '0' && Speciality.value != '0' && Qualification.value != '0' && Class.value != '0' && Territory.value != '0') {
                            isEntry = true;
                        }
                        if (DoctorName.value == '' && Address.value == '' && Category.value == '0' && Speciality.value == '0' && Qualification.value == '0' && Class.value == '0' && Territory.value == '0') {
                            isEmpty = true;
                        }
                        if ((isEntry == false) || (isEmpty == false)) {
                            if (DoctorName.value == '') {
                                alert('Enter Listed Doctor Name');
                                DoctorName.focus();
                                return false;
                            }
                            else if (Address.value == '') {
                                alert('Enter Address');
                                Address.focus();
                                return false;
                            }
                            else if (Category.value == '0') {
                                alert('Select Category');
                                Category.focus();
                                return false;
                            }
                            else if (Speciality.value == '0') {
                                alert('Select Speciality');
                                Speciality.focus();
                                return false;
                            }
                            else if (Qualification.value == '0') {
                                alert('Select Qualification');
                                Qualification.focus();
                                return false;
                            }
                            else if (Class.value == '0') {
                                alert('Select Class');
                                Class.focus();
                                return false;
                            }
                            else if (Territory.value == '0') {
                                alert('Select Territory');
                                Territory.focus();
                                return false;
                            }
                        }
                    }
                }

            }
            if (isEntry) {
                return true;
            }
        }
    </script>
    <style type="text/css">
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: 10px;
            overflow-y: scroll;
            height: 200px;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Black;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 150px !important;
        }
        #divwidth div
        {
            width: 150px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" name="frmquickproduct_entry" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div id="Divid" runat="server">
        </div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">
            <asp:Button ID="btnBack" CssClass="BUTTON" Height="25px" Width="60px" PostBackUrl="~/MasterFiles/MR/ListedDoctor/LstDoctorList.aspx"
                Text="Back" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
            <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <center>
            <asp:Table ID="Table1" runat="server" BorderStyle="Solid" Width="850px" BorderWidth="1"
                CssClass="clp" CellSpacing="3" CellPadding="3">
              
                <asp:TableRow>
                    <asp:TableCell BorderWidth="1" HorizontalAlign="Left">
                        <asp:Label ID="lblListed" runat="server" Text="No of Listed Customers - " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lblDrcount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" HorizontalAlign="Left">
                        <asp:Label ID="lblappcnt" runat="server" Text="Listed Customers Approval Pending - "
                            SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lblapp" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></asp:TableCell>
                    <asp:TableCell BorderWidth="1" HorizontalAlign="Left">
                        <asp:Label ID="lblListeddr" runat="server" Text="Lst Customers Deactivation Pending - "
                            SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lbldeact" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" HorizontalAlign="Left">
                        <asp:Label ID="lblListeddr1" runat="server" Text="Add Against Deactivation Pending - "
                            SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lbladddeact" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>
        <br />
        <center>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="grdListedDR" runat="server" Width="85%" HorizontalAlign="Center"
                            OnRowCreated="grdListedDR_RowCreated" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10" GridLines="None" OnRowDataBound="grdListedDR_RowDataBound" OnPageIndexChanging="grdListedDR_PageIndexChanging"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       
                                        <asp:TextBox ID="code" runat="server" SkinID="TxtBxAllowSymb" Width="30px"></asp:TextBox>
                                    <%--    <asp:HiddenField ID="hdnTerritoryId" runat="server"></asp:HiddenField>--%>
                                                                             
                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Customer Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="160px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="ListedDR_Name" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                            runat="server" Width="180px" Text='<%#Eval("ListedDR_Name")%>'></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="AutoCompleteAjaxRequest"
                                            ServicePath="~/MasterFiles/MR/ListedDoctor/Webservice/AutoComplete.asmx" MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="ListedDR_Name"
                                            FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ListedDR_Address1" onkeypress="AlphaNumric(event);" runat="server" 
                                            SkinID="TxtBxAllowSymb" MaxLength="240" Width="250px" Text='<%#Eval("ListedDR_Address1")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address2" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ListedDR_Address2" onkeypress="AlphaNumeric(event);" runat="server"
                                            SkinID="TxtBxAllowSymb" MaxLength="240" Width="250px" Text='<%#Eval("ListedDR_Address2")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Mobileno" onkeypress="AlphaNumeric(event);" runat="server"
                                            SkinID="TxtBxAllowSymb" MaxLength="240" Width="100px" Text='<%#Eval("Mobile")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Contact Person Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Contactperso" onkeypress="AlphaNumeric(event);" runat="server"
                                            SkinID="TxtBxAllowSymb" MaxLength="240" Width="100px" Text='<%#Eval("Contact_Person")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="credi" onkeypress="AlphaNumeric(event);" runat="server"
                                            SkinID="TxtBxAllowSymb" MaxLength="240" Width="100px" Text='<%#Eval("Credit_Days")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                               <%-- <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired" DataSource="<%# FillCategory() %>"
                                            DataTextField="Doc_Cat_SName" DataValueField="Doc_Cat_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Customer Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlspcl" runat="server" SkinID="ddlRequired" DataSource="<%# FillSpeciality() %>" 
                                            DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   
                                <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired" DataSource="<%# FillClass() %>"
                                            DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                       
                                <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlQual" runat="server" SkinID="ddlRequired" DataSource="<%# FillQualification() %>"
                                            DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
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
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" />
                        <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Clear" OnClick="btnClear_Click" />
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
</asp:Content>