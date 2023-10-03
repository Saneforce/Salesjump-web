<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="DoctorBusinessEntry.aspx.cs" EnableViewState="true" Inherits="DoctorBusinessEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <ucl:Menu ID="menu" runat="server" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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

            var ddlFieldForce = document.getElementById('<%=ddlFieldForce.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>');
            var txtDoctor = document.getElementsByName('<%=lstDoctor.ClientID%>');

            var rdoSelected = '';

            if (ddlFieldForce.selectedIndex == 0) {
                alert("Please select FieldForce!!");
                ddlFieldForce.focus();
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
            else if (txtDoctor[0].value == "") {
                alert("Please select Doctor!!");
                txtDoctor[0].focus();
                return false;
            }

            //document.getElementById('ctl00_MainContent_btnHdn').click();

            var table = document.getElementById("dataTable");
            var rowCount = table.rows.length;
            var prodVal = '';
            var prodName = '';
            for (var i = 1; i < rowCount; i++) {
                var row = table.rows[i];

                var chkbox = row.getElementsByTagName('select');
                //alert(chkbox[0].value);
                var txt = row.getElementsByTagName('input');
                //alert(txt[1].value);
                if (prodVal == "") {

                    prodVal = chkbox[0].value + '|' + txt[1].value;
                    prodName = chkbox[0].options[chkbox[0].selectedIndex].innerHTML + '(' + txt[1].value + ')';
                }
                else {
                    prodVal = prodVal + "," + chkbox[0].value + '|' + txt[1].value;
                    prodName = prodName + "," + chkbox[0].options[chkbox[0].selectedIndex].innerHTML + '(' + txt[1].value + ')';
                }
                chkbox[0].value = 0;
                txt[1].value = "";
            }

            document.getElementById("<%= hdnProdCode.ClientID %>").value = prodVal;
            document.getElementById("<%= hdnProdName.ClientID %>").value = prodName;
            //document.getElementById('lblProd').value = prodVal;


            //clickButton.click();
        }

        function OnContactSelected(source, eventArgs) {

            var hdnValueID = "<%= hdnValue.ClientID %>";

            document.getElementById(hdnValueID).value = eventArgs.get_value();
            __doPostBack(hdnValueID, "");
        }  
    </script>
    <center>
        <table>
            <tr>
                <td align="center" style="color:#8A2EE6;font-family:Verdana;font-weight:bold;text-transform: capitalize; font-size: 14px;
                                        text-align: center;">
                    <asp:Label ID="lblHead" runat="server" Text="Customer Business Entry" Font-Underline="True"
                        Font-Bold="True" ></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="false"
                        OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Year " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblMonth" runat="server" Text="Month " SkinID="lblMand"></asp:Label>
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
                <td colspan="4" align="center">
                    <asp:Button ID="btnGo" runat="server" Text="Go" Width="30px" Height="25px" CssClass="BUTTON"
                        OnClick="btnGo_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="stylespc">
                    <table class="mGrid" border="0" style="border-collapse: collapse">
                        <tr>
                            <th style="color: White;">
                                Customer Name
                            </th>
                            <th style="color: White;">
                                Channel
                            </th>
                            <th style="color: White;">
                                Category
                            </th>
                            <th style="color: White;">
                                Territory Name
                            </th>
                            <th style="color: White;">
                                Product / QTY
                            </th>
                            <th>
                                &nbsp;
                            </th>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:HiddenField ID="hdnValue" OnValueChanged="hdnValue_ValueChanged" runat="server" />
                                <asp:TextBox ID="txtDoctor" runat="server" Visible="false"></asp:TextBox>
                                <asp:AutoCompleteExtender ServiceMethod="GetDoctorList" MinimumPrefixLength="1" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtDoctor" ID="AutoCompleteExtender1"
                                    runat="server" FirstRowSelected="false" UseContextKey="true" OnClientItemSelected="OnContactSelected">
                                </asp:AutoCompleteExtender>
                                <asp:ListBox ID="lstDoctor" runat="server" SelectionMode="Single"></asp:ListBox>
                                <%--<select id="lstDoctor" multiple="true" runat="server">
                                </select>--%>
                                <asp:Button ID="btnDoctor" Text="Submit" runat="server" OnClick="btnDoctor_Click"
                                    Style="display: none;" />
                            </td>
                            <td>
                                <asp:Label ID="lblDCRSpeciality" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnDCRSpeciality" runat="server"></asp:HiddenField>
                            </td>
                            <td>
                                <asp:Label ID="lblDCRCategory" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnDCRCategory" runat="server"></asp:HiddenField>
                            </td>
                            <td>
                                <asp:Label ID="lblDCRTerritory" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnDCRTerritory" runat="server"></asp:HiddenField>
                            </td>
                            <td>
                                <div class="demo">
                                    <div class="toggler">
                                        <span>
                                            <asp:TextBox ID="txtProducts" runat="server" CssClass="textbox"></asp:TextBox>
                                            <img id="ddlArrow" src="../../Images/down_arrow.jpg" />
                                        </span>
                                        <div id="effect" class="ui-widget-content">
                                            <asp:Panel ID="pnlList" runat="server" Style="white-space: nowrap;" />
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnProdGo" runat="server" Text="Go" CssClass="BUTTON" OnClientClick="ValidateProducts();return false;" />
                                <asp:Button ID="btnHdnProd" runat="server" Text="Hidden" OnClick="btnProdGo_Click"
                                    CssClass="BUTTON" Style="display: none;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <%--<asp:Button ID="btnAdd" runat="server" Text="Add To List" OnClientClick="SubmitForm();return false;"
                        CssClass="BUTTON" />
                    <asp:Button ID="btnHdn" runat="server" Text="Hidden" OnClick="btnAdd_Click" CssClass="BUTTON"
                        Style="display: none;" />--%>
                    <asp:HiddenField ID="hdnProdCode" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnProdName" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnRowID" runat="server"></asp:HiddenField>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvDoctorBusiness" runat="server" AutoGenerateColumns="false" OnRowCommand="gvDoctorBusiness_RowCommand"
                        OnRowDeleting="gvDoctorBusiness_RowDeleting" OnRowEditing="gvDoctorBusiness_RowEditing"
                        CssClass="mGrid">
                        <Columns>
                            <asp:TemplateField HeaderText="Customer Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoctor" runat="server" Text='<%# Eval("DoctorName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnDoctor" runat="server" Value='<%# Eval("DoctorCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpeciality" runat="server" Text='<%# Eval("Speciality") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerritory" runat="server" Text='<%# Eval("Territory") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Products" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("Products") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnProducts" runat="server" Value='<%# Eval("ProductsCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblEdit" runat="server" Text="Edit" CommandName="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                        CommandName="Delete"></asp:LinkButton>
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
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Width="120px" Text="Draft Save" CssClass="BUTTON"
                        OnClick="btnSave_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Width="120px" Text="Final Submit" OnClick="btnSubmit_Click"
                        CssClass="BUTTON" />
                    <asp:HiddenField ID="hdnTransNo" runat="server" />
                </td>
            </tr>
        </table>
    </center>
    <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>
    <script type="text/javascript">
        $('[id*=lstDoctor]').multipleSelect({
            single: true,
            filter: true,
            onClick: function (event) {

                var selectedDoctor = event.label;
                var doctorValue = event.value;

                var doctorValueAll = doctorValue.split('|');

                var category = '';
                var speciality = '';
                var territory = '';
                //alert(doctorValueAll);
                if (doctorValueAll.length > 0) {
                    category = doctorValueAll[1];
                    speciality = doctorValueAll[2];
                    territory = doctorValueAll[3];

                    document.getElementById('<%=lblDCRCategory.ClientID%>').innerHTML = category;
                    document.getElementById('<%=hdnDCRCategory.ClientID%>').value = category;
                    document.getElementById('<%=lblDCRSpeciality.ClientID%>').innerHTML = speciality;
                    document.getElementById('<%=lblDCRTerritory.ClientID%>').innerHTML = territory;

                    document.getElementById('<%=hdnDCRSpeciality.ClientID%>').value = speciality;
                    document.getElementById('<%=hdnDCRTerritory.ClientID%>').value = territory;

                }

                //document.getElementById("<%= btnDoctor.ClientID %>").click();
                //return false;
                //alert(selectedDoctor);
                //$('[id*=lstDoctor]').multipleSelect("setSelects", [selectedDoctor]);
            }
        });
        
    </script>
    <script language="javascript" type="text/javascript">
        function GetSelectedValue() {

        }

        function ControlVisibility(control) {

            var chk = document.getElementById("ctl00_MainContent_chkNew" + control);
            var txt = document.getElementById("ctl00_MainContent_txtNew" + control);
            var objTextBox = document.getElementById("<%=txtProducts.ClientID%>");
            var hdnProdCode = document.getElementById("<%=hdnProdCode.ClientID%>");


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
                            hdnProdCode.value = txt[2].value + "-" + txt[1].value;
                        }
                        else {
                            objTextBox.value = objTextBox.value + "," + lbl[0].innerHTML + "-" + txt[1].value;
                            hdnProdCode.value = hdnProdCode.value + "," + txt[2].value + "-" + txt[1].value;
                        }
                    }
                    else {
                        objTextBox.value = objTextBox.value.replace(lbl[0].innerHTML + "-" + txt[1].value, "");
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
                var lblVal = lbl[0].innerHTML + "-" + txt[1].value;
                objTextBox.value = objTextBox.value.replace(lblVal + ",", "");
                objTextBox.value = objTextBox.value.replace("," + lblVal, "");
                objTextBox.value = objTextBox.value.replace(lblVal, "");
                objTextBox.value = objTextBox.value.replace(",,", ",");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd + ",", "");
                hdnProdCode.value = hdnProdCode.value.replace("," + txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(txthdnProd, "");
                hdnProdCode.value = hdnProdCode.value.replace(",,", ",");
            }
        }

        function ValidateProducts() {
            var txtDoctor = document.getElementById("<%=lstDoctor.ClientID%>");
            var txtProducts = document.getElementById("<%=txtProducts.ClientID%>");

            if (txtDoctor.value == "") {
                alert("Please select Doctor!!");
                txtDoctor.focus();
                return false;
            }
            else if (txtProducts.value == "") {
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
                    if (txt[0].checked) {
                        if (txt[1].value == "") {
                            alert('Please Enter Product Quantity');
                            txt[1].focus();
                            return false;
                        }
                    }
                }
            }

            document.getElementById('ctl00_MainContent_btnHdnProd').click();
        }
    </script>
</asp:Content>
