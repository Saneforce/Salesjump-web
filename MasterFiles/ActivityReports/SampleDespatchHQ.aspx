<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="SampleDespatchHQ.aspx.cs" EnableViewState="true" Inherits="SampleDespatchHQ" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">--%>
    <style type="text/css">
        #effect
        {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }
        .textbox
        {
            width: 185px;
            height: 14px;
        }
        body
        {
            font-size: 62.5%;
        }
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
   
<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <ucl:Menu ID="menu" runat="server" />--%>
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
    <script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#effect").hide();
            //run the currently selected effect
            function runEffect() {
                //get effect type from
                if (!($('#effect').is(":visible"))) {
                    //run the effect
                    $("#effect").show('blind', 200);
                }
                else {
                    $("#effect").hide('blind', 200);
                }
            };

            //set effect from select menu value
            $("#ddlArrow").click(function () {
                runEffect();
                return false;
            });


            $(document).click(function (e) { if (($('#effect').is(":visible"))) { $("#effect").hide('blind', 1000); } });

            $('#effect').click(function (e) {
                e.stopPropagation();
            });
        });

        function autoCompleteEx_ItemSelected(sender, args) {
            __doPostBack(sender.get_element().name, "");
        }

        function SubmitForm() {

            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');

            if (ddlYear.selectedIndex == 0) {
                alert("Please select Year!!");
                ddlYear.focus();
                return false;
            }
            else if (ddlMonth.selectedIndex == 0) {
                alert("Please select Month!!");
                ddlMonth.focus();
                return false;
            }

            //document.getElementById('ctl00_MainContent_btnHdn').click();
            var clickButton = document.getElementById("<%= btnHdn.ClientID %>");

            var table = document.getElementById("<%= gvSampleProducts.ClientID %>");
            if (table != null) {
                var rowCount = table.rows.length;
                var prodVal = '';
                var prodName = '';
                for (var i = 1; i < rowCount; i++) {
                    var row = table.rows[i];

                    var label = row.getElementsByTagName('span');
                    //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    //alert(txt[1].value);

//                    if (txt[1].value == "") {
//                        alert("Please Enter Quantity!!");
//                        txt[1].focus();
//                        return false;
//                    }

                    if (prodVal == "") {

                        prodVal = txt[0].value + '|' + label[0].innerHTML + '|' + txt[1].value;
                        prodName = label[0].innerHTML + '(' + txt[1].value + ')';
                    }
                    else {
                        prodVal = prodVal + "," + txt[0].value + '|' + label[0].innerHTML + '|' + txt[1].value;
                        prodName = prodName + "," + label[0].innerHTML + '(' + txt[1].value + ')';
                    }
                    //chkbox[0].value = 0;
                    //txt[1].value = "";
                }

                document.getElementById("<%= hdnProdCode.ClientID %>").value = prodVal;
                document.getElementById("<%= hdnProdName.ClientID %>").value = prodName;
                //document.getElementById('lblProd').value = prodVal;

            }
            else {
                var table = document.getElementById("ctl00_MainContent_tbl");
                var rowCount = table.rows.length;

                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('select');
                    //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    var lbl = row.getElementsByTagName('span');
                    //alert(chkbox);
//                    if (txt[0].checked) {
//                        if (txt[1].value == "") {
//                            alert('Please Enter Product Quantity');
//                            txt[1].focus();
//                            return false;
//                        }
//                    }
                }
            }

            clickButton.click();
        }

        function Validate() {
            var lstFieldForce = document.getElementById('<%=lstFieldForce.ClientID%>');
            var lstBaseLevel = document.getElementById('<%=lstBaseLevel.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
            var rdoProduct = document.getElementsByName('<%=rdoProduct.ClientID%>');
            //var divProducts = document.getElementById('<%=divProducts.ClientID%>');
            //var btnSave = document.getElementById('<%=btnSave.ClientID%>');
            var rdoSelected = '';

            if (lstFieldForce.selectedIndex == -1) {
                alert("Please select FieldForce!!");
                lstFieldForce.focus();
                return false;
            }
            else if (lstBaseLevel.selectedIndex == -1) {
                alert("Please select BaseLevel!!");
                lstBaseLevel.focus();
                return false;
            }
            else if (ddlYear.selectedIndex == 0) {
                alert("Please select Year!!");
                ddlYear.focus();
                return false;
            }
            else if (ddlMonth.selectedIndex == 0) {
                alert("Please select Month!!");
                ddlMonth.focus();
                return false;
            }

            //if (divProducts.length > 0) {
            //divProducts.style.display = 'block';
            //}

            //btnSave.style.display = 'block';
            //this.GetAllProducts();
            //return false;
        }

        function GetAllProducts() {
            var list = document.getElementById("<% =rdoProduct.ClientID %>"); //Cleint ID of RadioButtonList
            var rdbtnLstValues = list.getElementsByTagName("input");

            var divProducts = document.getElementById('<%=divProducts.ClientID%>');

            //if (divProducts.length > 0) {
            divProducts.style.display = 'block';
            //}

            var Checkdvalue = '';
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[i].checked) {
                    Checkdvalue = rdbtnLstValues[i].value;
                    break;
                }
            }
        }

        function addMultipleRows(products) {
            var table = document.getElementById("<%= tblProductList.ClientID %>");
            var prodArray = products.split(',');

            var totalLength = table.rows.length - 1;
            this.deleteRow();
            //if (totalLength != prodArray.length) {
            for (var i = 0; i < prodArray.length; i++) {

                var prodVal = prodArray[i].split('-')
                var prodQty = '';
                var giftType = '';
                if (prodVal.length > 1) {
                    prodQty = prodVal[1];
                }

                if (prodVal.length > 2) {
                    giftType = prodVal[2];
                }
                addRow(prodVal[0], prodQty, giftType);
            }
            table.deleteRow(1);
            //}

        }

        function addRow(product, prodcount, newgiftType) {
            var table = document.getElementById("<%= tblProductList.ClientID %>");

            var rowCount = table.rows.length;
            var row = table.insertRow(rowCount);
            var colCount = table.rows[1].cells.length;
            for (var i = 0; i < colCount; i++) {
                var newcell = row.insertCell(i);
                newcell.innerHTML = table.rows[1].cells[i].innerHTML;
                //alert(newcell.childNodes);
                if (i == 0) {
                    newcell.childNodes[1].innerHTML = product;
                }
                else if (i == 1) {
                    newcell.childNodes[1].innerHTML = newgiftType;
                }
                else if (i == 2) {
                    newcell.childNodes[1].innerHTML = prodcount;
                }
            }
        }

        function deleteRow() {
            try {
                var table = document.getElementById("<%= tblProductList.ClientID %>");
                var rowCount = table.rows.length;
                for (var i = 2; i < rowCount; i++) {
                    var row = table.rows[i];

                    if (rowCount <= 1)
                    { break; }
                    table.deleteRow(i);
                    rowCount--;
                    i--;

                }
            } catch (e) {
                alert(e);
            }
        }
    
    </script>
    <center>
        <table>
            <tr>
                <td align="center" style="color: #8A2EE6; font-family: Verdana; font-weight: bold;
                    text-transform: capitalize; font-size: 14px; text-align: center;">
                    <asp:Label ID="lblHead" runat="server" Text="Sample Despatch from HQ" Font-Underline="True"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <select id="lstFieldForce" multiple="true" runat="server">
                    </select>
                    <asp:Button ID="btnFieldForce" Text="Submit" runat="server" OnClick="Submit" Style="display: none;" />
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" runat="server" Text="Base Level" SkinID="lblMand"></asp:Label>
                </td>
                <td class="stylespc">
                    <asp:ListBox ID="lstBaseLevel" runat="server" SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProd" runat="server" Text="Product" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="rdoProduct" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Single" Text="Single Product wise" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="All" Text="All Product wise"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                    <asp:Button ID="btnGo" runat="server" Text="Go" Width="30px" Height="25px" CssClass="BUTTON"
                        OnClick="btnGo_Click" OnClientClick="return Validate();" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="gvSampleDespatch" runat="server" AutoGenerateColumns="false" OnRowCommand="gvSampleDespatch_RowCommand"
                        OnRowDataBound="gvSampleDespatch_RowDataBound" OnRowDeleting="gvSampleDespatch_RowDeleting"
                        CssClass="mGrid" Width="65%">
                        <Columns>
                            <asp:TemplateField HeaderText="Products" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlProducts" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Despatch Qty" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProducts" runat="server" Text='<%# Eval("DespatchQty") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <div id="divProducts" runat="server">
                        <table id="tblProducts" runat="server" width="60%" border="1" visible="false">
                            <tr>
                                <td align="left">
                                    <div class="demo">
                                        <div class="toggler">
                                            <span>
                                                <asp:TextBox ID="txtProducts" runat="server" CssClass="textbox"></asp:TextBox>
                                                <img id="ddlArrow" src="../../Images/down_arrow.jpg" style="margin-left: -23px; margin-bottom: -4px" />
                                            </span>
                                            <div id="effect" class="ui-widget-content">
                                                <asp:Panel ID="pnlList" runat="server" Style="white-space: nowrap;" />
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table id="tblProductList" runat="server" width="40%" border="1" class="mGrid">
                                        <tr>
                                            <td>
                                                Products
                                            </td>
                                            <td>
                                                Pack
                                            </td>
                                            <td>
                                                Despatch Qty
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span id="lblProduct"></span>&nbsp;
                                            </td>
                                            <td>
                                                <span id="lblPack"></span>
                                            </td>
                                            <td>
                                                <span id="lblProductCount"></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSampleProducts" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                        Width="100%" Visible="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("Product_Detail_Name") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnProducts" runat="server" Value='<%# Eval("Product_Detail_Code") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Product_Sale_Unit" HeaderText="Pack" HeaderStyle-ForeColor="White"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Despatch Quantity" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="SubmitForm();return false;"
                        CssClass="BUTTON" Visible="false" />
                    <asp:Button ID="btnHdn" runat="server" Text="Hidden" Style="display: none;" OnClick="btnSave_Click" />
                    <asp:HiddenField ID="hdnProdCode" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnProdName" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnProducts" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnProductPack" runat="server"></asp:HiddenField>
                </td>
            </tr>
        </table>
    </center>
    <link type="text/css" href="css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="JsFiles/multiple-select.js"></script>
    <script type="text/javascript">
        $('[id*=lstFieldForce]').multipleSelect({
            onClose: function (event) {
                document.getElementById("<%= btnFieldForce.ClientID %>").click();
            }
        });

        $('[id*=lstBaseLevel]').multipleSelect();
    </script>
    <script language="javascript" type="text/javascript">
        function GetSelectedValue() {

        }

        function ControlVisibility(control) {

            var chk = document.getElementById("ctl00_MainContent_chkNew" + control);
            var txt = document.getElementById("ctl00_MainContent_txtNew" + control);
            var objTextBox = document.getElementById("<%=txtProducts.ClientID%>");
            var hdnProdCode = document.getElementById("<%=hdnProdCode.ClientID%>");
            var hdnProductPack = document.getElementById("<%=hdnProductPack.ClientID%>");

            if (chk.checked) {
                txt.style.display = "block";

                var table = document.getElementById("ctl00_MainContent_tbl");
                var rowCount = table.rows.length;
                objTextBox.value = "";
                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('select');
                    //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    var lbl = row.getElementsByTagName('span');
                    //alert(chkbox);
                    if (txt[0].checked) {
                        if (objTextBox.value == "") {
                            objTextBox.value = lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProductPack.value = lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                            hdnProdCode.value = txt[2].value + "-" + txt[1].value;
                        }
                        else {
                            objTextBox.value = objTextBox.value + "," + lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProductPack.value = hdnProductPack.value + "," + lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                            hdnProdCode.value = hdnProdCode.value + "," + txt[2].value + "-" + txt[1].value;
                        }
                    }
                    else {
                        objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + "-" + txt[1].value, "");
                        hdnProductPack.value = hdnProductPack.value.replace(lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value, "");
                        hdnProdCode.value = hdnProdCode.value.replace(txt[2].value + "-" + txt[1].value, "");
                    }
                }
                //this.SetValue();
            }
            else {
                txt.style.display = "none";
                var table = document.getElementById("ctl00_MainContent_tbl");
                var row = table.rows[control];
                var lbl = row.getElementsByTagName('span');
                var txt = row.getElementsByTagName('input');
                var txthdnProd = txt[2].value + "-" + txt[1].value;
                var txthdnProdPack = lbl[0].innerHTML + "-" + txt[1].value + "-" + txt[3].value;
                var lblVal = lbl[0].innerHTML + "-" + txt[1].value;
                objTextBox.value = objTextBox.value.replace(lblVal + ",", "");
                objTextBox.value = objTextBox.value.replace("," + lblVal, "");
                objTextBox.value = objTextBox.value.replace(lblVal, "");
                objTextBox.value = objTextBox.value.replace(",,", ",");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd + ",", "");
                hdnProdCode.value = hdnProdCode.value.replace("," + txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(",,", ",");
                hdnProductPack.value = hdnProductPack.value.replace(txthdnProdPack + ",", "");
                hdnProductPack.value = hdnProductPack.value.replace("," + txthdnProdPack, "");
                hdnProductPack.value = hdnProductPack.value.replace(txthdnProdPack, "");
                hdnProductPack.value = hdnProductPack.value.replace(",,", ",");
            }

            this.addMultipleRows(hdnProductPack.value);
        }

        function ValidateProducts() {

            var txtProducts = document.getElementById("<%=txtProducts.ClientID%>");

            if (txtProducts.value == "") {
                alert("Please select Products!!");
                txtProducts.focus();
                return false;
            }
            else {
                var table = document.getElementById("ctl00_MainContent_tbl");
                var rowCount = table.rows.length;

                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    var chkbox = row.getElementsByTagName('select');
                    //alert(chkbox[0].value);
                    var txt = row.getElementsByTagName('input');
                    var lbl = row.getElementsByTagName('span');
                    //alert(chkbox);
//                    if (txt[0].checked) {
//                        if (txt[1].value == "") {
//                            alert('Please Enter Product Quantity');
//                            txt[1].focus();
//                            return false;
//                        }
//                    }
                }
            }

            document.getElementById('ctl00_MainContent_btnHdnProd').click();
        }
    </script>
</asp:Content>
