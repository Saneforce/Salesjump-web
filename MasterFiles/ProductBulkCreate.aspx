<%@ Page Title="Quick Add - Product Detail" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="ProductBulkCreate.aspx.cs"
    Inherits="MasterFiles_ProductBulkCreate" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Quick Add - Product Detail</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
        .collp
        {
            border-collapse: collapse;
            text-align: left;
        }
        .normal
        {
            background-color: white;
        }
        .highlight_clr
        {
            background-color: Lightblue;
        }
        .closeLoginPanel
        {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }
        
        .closeLoginPanel a
        {
            background-color: Yellow;
            cursor: pointer;
            color: Black;
            text-align: center;
            text-decoration: none;
            padding: 3px;
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
        function dynInput(cbox) {
            if (cbox.checked) {
                var input = document.createElement("input");
                input.type = "text";
                var div = document.createElement("div");
                div.id = cbox.name;
                div.innerHTML = "Text to display for " + cbox.name;
                div.appendChild(input);
                document.getElementById("insertinputs").appendChild(div);
            } else {
                document.getElementById(cbox.name).remove();
            }
        }
    </script>
    <script type="text/javascript">

        function checkAll(obj1) {


            var Chkstate = document.getElementById('<%=Chkstate.ClientID %>').getElementsByTagName('input');

            Chkstate.checked = true;

        }  
      
    
      
   
      
    </script>
    
    <script language="javascript" type="text/javascript">

        function validateTextBox() {

            var TargetBaseControl = document.getElementById('<%=this.grdProduct.ClientID%>');
            var TargetChildControl1 = "Product_Detail_Code";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].type == 'text' && Inputs[n].id.indexOf(TargetChildControl1, 0) >= 0) {

                    if (Inputs[n].value != "")

                        return true; alert('Product_Detail_Code');
                    return false;

                }
            }
        }
 
         
    </script>
    <script type="text/javascript">
        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= grdProduct.ClientID %>');
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
                        var productCode = document.getElementById('grdProduct_ctl' + index + '_Product_Detail_Code');
                        var productName = document.getElementById('grdProduct_ctl' + index + '_Product_Detail_Name');
                        var Description = document.getElementById('grdProduct_ctl' + index + '_Product_Description');
                        var Sale = document.getElementById('grdProduct_ctl' + index + '_Product_Sale_Unit');
                        var group = document.getElementById('grdProduct_ctl' + index + '_Product_Group');
                        var category = document.getElementById('grdProduct_ctl' + index + '_Product_Cat_Code');
                        var brand = document.getElementById('grdProduct_ctl' + index + '_Product_Brd_Code');


                        if (productCode.value != '' || productName.value != '' || Description.value != '' || Sale.value != '' || group.value != '0' || category.value != '0' || brand.value != '0') {
                            isEmpty = true;
                        }
                        if (productCode.value != '') {
                            if (productName.value == '') {
                                alert('Enter Product Name')
                                productName.focus();
                                return false;
                            }
                            if (Description.value == '') {
                                alert('Enter Description');
                                Description.focus();
                                return false;
                            }
                            if (Sale.value == '') {
                                alert('Enter Sale Unit')
                                Sale.focus();
                                return false;
                            }
                            if (group.value == '0') {
                                alert('Select Group')
                                group.focus();
                                return false;
                            }
                            if (category.value == '0') {
                                alert('Select Category')
                                category.focus();
                                return false;
                            }

                            if (brand.value == '0') {
                                alert('Select Brand')
                                brand.focus();
                                return false;
                            }


                        }
                        if (productName.value != '') {
                            if (productCode.value == '') {
                                alert('Enter Product Code')
                                productCode.focus();
                                return false;
                            }
                            if (Description.value == '') {
                                alert('Enter Description');
                                Description.focus();
                                return false;
                            }
                            if (Sale.value == '') {
                                alert('Enter Sale Unit')
                                Sale.focus();
                                return false;
                            }
                            if (group.value == '0') {
                                alert('Select Group')
                                group.focus();
                                return false;
                            }
                            if (category.value == '0') {
                                alert('Select Category')
                                category.focus();
                                return false;
                            }
                            if (brand.value == '0') {
                                alert('Select Brand')
                                brand.focus();
                                return false;
                            }


                        }
                        if (Description.value != '') {
                            if (productCode.value == '') {
                                alert('Enter Product Code')
                                productCode.focus();
                                return false;
                            }
                            if (productName.value == '') {
                                alert('Enter Product Name')
                                productName.focus();
                                return false;
                            }
                            if (Sale.value == '') {
                                alert('Enter Sale Unit')
                                Sale.focus();
                                return false;
                            }
                            if (group.value == '0') {
                                alert('Select Group')
                                group.focus();
                                return false;
                            }
                            if (category.value == '0') {
                                alert('Select Category')
                                category.focus();
                                return false;
                            }
                            if (brand.value == '0') {
                                alert('Select Brand')
                                brand.focus();
                                return false;
                            }


                        }
                        if (Sale.value != '') {
                            if (productCode.value == '') {
                                alert('Enter Product Code')
                                productCode.focus();
                                return false;
                            }
                            if (productName.value == '') {
                                alert('Enter Product Name')
                                productName.focus();
                                return false;
                            }
                            if (Description.value == '') {
                                alert('Enter Description')
                                Description.focus();
                                return false;
                            }
                            if (group.value == '0') {
                                alert('Select Group')
                                group.focus();
                                return false;
                            }
                            if (category.value == '0') {
                                alert('Select Category')
                                category.focus();
                                return false;
                            }
                            if (brand.value == '0') {
                                alert('Select Brand')
                                brand.focus();
                                return false;
                            }

                        }
                        if (group.value != '0') {
                            if (productCode.value == '') {
                                alert('Enter Product Code')
                                productCode.focus();
                                return false;
                            }
                            if (productName.value == '') {
                                alert('Enter Product Name')
                                productName.focus();
                                return false;
                            }

                            if (Description.value == '') {
                                alert('Enter Description')
                                Description.focus();
                                return false;
                            }
                            if (Sale.value == '') {
                                alert('Enter Sale Unit')
                                Sale.focus();
                                return false;
                            }
                            if (category.value == '0') {
                                alert('Select Category')
                                category.focus();
                                return false;
                            }
                            if (brand.value == '0') {
                                alert('Select Brand')
                                brand.focus();
                                return false;
                            }

                        }
                        if (category.value != '0') {
                            if (productCode.value == '') {
                                alert('Enter Product Code')
                                productCode.focus();
                                return false;
                            }
                            if (productName.value == '') {
                                alert('Enter Product Name')
                                productName.focus();
                                return false;
                            }
                            if (Description.value == '') {
                                alert('Enter Description')
                                Description.focus();
                                return false;
                            }
                            if (Sale.value == '') {
                                alert('Enter Sale Unit')
                                Sale.focus();
                                return false;
                            }
                            if (group.value == '0') {
                                alert('Select Group')
                                group.focus();
                                return false;
                            }
                            if (brand.value == '0') {
                                alert('Select Brand')
                                brand.focus();
                                return false;
                            }

                        }

                        if (brand.value != '0') {
                            if (productCode.value == '') {
                                alert('Enter Product Code')
                                productCode.focus();
                                return false;
                            }
                            if (productName.value == '') {
                                alert('Enter Product Name')
                                productName.focus();
                                return false;
                            }
                            if (Description.value == '') {
                                alert('Enter Description')
                                Description.focus();
                                return false;
                            }
                            if (Sale.value == '') {
                                alert('Enter Sale Unit')
                                Sale.focus();
                                return false;
                            }
                            if (group.value == '0') {
                                alert('Select Group')
                                group.focus();
                                return false;
                            }
                            if (category.value == '0') {
                                alert('Select Catergory')
                                category.focus();
                                return false;
                            }

                        }

                    }
                }
                if (isEmpty) {
                    alert('Please enter value')
                    return true;
                }
                else
                    return true;
            }
        }
    </script>
    <script type="text/javascript">
        function HidePopup() {

            var mpu = $find('txtUOM_PopupControlExtender');
            mpu.hide();
        }
    </script>
    <script type="text/javascript">
        function HidePopup() {

            var mpu = $find('txtstate_PopupControlExtender');
            mpu.hide();
        }
    </script>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('TextBox1_PopupControlExtender');
            popup.hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" name="frmquickproduct_entry" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>
        <br />
        <center>
            <table runat="server" width="100%" id="tblProduct">
                <tr>
                    <td>
                        <asp:GridView ID="grdProduct" runat="server" Width="80%" HorizontalAlign="Center"
                            OnRowCreated="grdProduct_RowCreated" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="grdProduct_RowDataBound">
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
                                <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Product_Detail_Code" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                            SkinID="TxtBxAllowSymb" MaxLength="20" Width="80px" Text='<%#Eval("code")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Product_Detail_Name" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                            SkinID="TxtBxAllowSymb" MaxLength="50" Width="190px" Text='<%#Eval("name")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Description" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Product_Description" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                            runat="server" Width="230px" Text='<%#Eval("descr")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base UOM" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                      <asp:DropDownList ID="Product_Base_uom" runat="server" Width="120px" SkinID="ddlRequired" AutoPostBack="true"
                                            DataSource="<%# FillUOM() %>" OnSelectedIndexChanged="Product_Base_uom_SelectedIndexChanged" DataTextField="Move_MailFolder_Name" DataValueField="Move_MailFolder_Id">
                                        </asp:DropDownList>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                         <asp:DropDownList ID="Product_uom" runat="server" Width="120px" SkinID="ddlRequired"
                                            DataSource="<%# FillUOM() %>" DataTextField="Move_MailFolder_Name" DataValueField="Move_MailFolder_Id">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="UOM Value" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="UOM_Value" SkinID="TxtBxAllowSymb" onkeypress="CheckNumeric(event);"
                                            runat="server" Width="80px" Text='<%#Eval("sale_unit")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Product_Group" runat="server" Width="120px" SkinID="ddlRequired"
                                            DataSource="<%# FillGroup() %>" DataTextField="Product_Grp_Name" DataValueField="Product_Grp_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Product_Cat_Code" runat="server" Width="120px" SkinID="ddlRequired"
                                            DataSource="<%# FillCategory() %>" DataTextField="Product_Cat_Name" DataValueField="Product_Cat_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Product_Brd_Code" runat="server" Width="120px" SkinID="ddlRequired"
                                            DataSource="<%# FillBrand() %>" DataTextField="Product_Brd_Name" DataValueField="Product_Brd_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updatepanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtstate" runat="server" Text="ALL" SkinID="TxtBxAllowSymb" Width="190px"></asp:TextBox>
                                                <asp:HiddenField ID="hdnStateId" runat="server"></asp:HiddenField>
                                                <asp:PopupControlExtender ID="txtstate_PopupControlExtender" runat="server" Enabled="True"
                                                    ExtenderControlID="" TargetControlID="txtstate" PopupControlID="Panel2" OffsetY="22">
                                                </asp:PopupControlExtender>
                                                <asp:Panel ID="Panel2" runat="server" Height="230px" Width="200px" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="collp" Direction="LeftToRight" ScrollBars="Auto"
                                                    BackColor="#CCCCCC" Style="display: none">
                                                    <%-- <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="Button2" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color:Yellow; 
                                            color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>--%>
                                                    <div style="height: 17px; position: relative; background-color: #4682B4; text-transform: capitalize;
                                                        width: 100%; float: left" align="right">
                                                        <div class="closeLoginPanel">
                                                            <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                title="Close">X</a>
                                                        </div>
                                                    </div>
                                                    <asp:CheckBoxList ID="Chkstate" runat="server" Width="180px" BorderStyle="None" CssClass="collp"
                                                        DataTextField="StateName" DataValueField="State_Code" AutoPostBack="True" OnSelectedIndexChanged="Chkstate_SelectedIndexChanged"
                                                        onclick="checkAll(this);">
                                                    </asp:CheckBoxList>
                                                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Division" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:UpdatePanel>
                                            <ContentTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text="ALL" SkinID="TxtBxAllowSymb" Width="145px"></asp:TextBox>
                                                <asp:HiddenField ID="hdnSubDivisionId" runat="server"></asp:HiddenField>
                                                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True"
                                                    ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22">
                                                </asp:PopupControlExtender>
                                                <asp:Panel ID="Panel1" runat="server" Height="116px" Width="155px" BorderStyle="Solid"
                                                    BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                    Style="display: none">
                                                    <%--   <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="btnsubdiv" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color: Yellow; 
                                            Color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>--%>
                                                    <div style="height: 17px; position: relative; background-color: #4682B4; text-transform: capitalize;
                                                        width: 100%; float: left" align="right">
                                                        <div class="closeLoginPanel">
                                                            <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                title="Close">X</a>
                                                        </div>
                                                    </div>
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="155px" CssClass="collp"
                                                        DataTextField="subdivision_name" DataValueField="subdivision_code" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" onclick="checkAll1(this);">
                                                    </asp:CheckBoxList>
                                                    <%--   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [subdivision_code],[subdivision_name] FROM [mas_subdivision]"></asp:SqlDataSource>--%>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
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
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>