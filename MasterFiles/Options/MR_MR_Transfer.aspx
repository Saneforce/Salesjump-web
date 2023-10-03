<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MR_MR_Transfer.aspx.cs" Inherits="MasterFiles_Options_MR_MR_Transfer" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Master Details</title>
    <style type="text/css">
        table.gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        table.gridtable th
        {
            padding: 5px;
        }
        table.gridtable td
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: #666666;
        }
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {           
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                        
                    }
                }
            }
        }
    </script>
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
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
    </div>
    <center>
        <asp:Panel ID="pnlTrans" runat="server" BorderWidth="1" Width="90%" BackColor="White">
            <table style="border-bottom: none" width="100%">
                <tr style="border: none">
                    <th align="right" style="border: none;" width="30%">
                        <asp:RadioButtonList ID="rdotransfer" BorderStyle="None" AutoPostBack="true" runat="server"
                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rdotransfer_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Listed Doctor &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:ListItem>
                            <asp:ListItem Text="Chemist" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </th>
                    <th align="right" width="20%">
                        <asp:Button ID="btnTran" runat="server" Text="Transfer" ForeColor="White" BackColor="#A6A6D2"
                            OnClick="btnTransfer_Click" Enabled="false" />
                        <asp:Button ID="btnClr" runat="server" Text="Clear All" BackColor="#A6A6D2" ForeColor="White"
                            OnClick="btnClear_Click" Enabled="false" />
                    </th>
                </tr>
            </table>
        </asp:Panel>
        <table class="gridtable" width="90%" style="background-color: White">
            <tr>
                <td style="width: 45%">
                    <table>
                        <tr>
                            <td style="border: none;" align="left">
                                <asp:Label ID="lblFF" Width="160px" runat="server" SkinID="lblMand" Text="Transfer From"></asp:Label>
                            </td>
                            <td style="border: none" align="left">
                                <%--<asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlFromFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                    OnSelectedIndexChanged="ddlFromFieldForce_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none" align="left">
                                <asp:Label ID="lblterrFrom" Width="160px" runat="server" SkinID="lblMand" Text="Transfer From Territory"></asp:Label>
                            </td>
                            <td style="border: none" align="left">
                                <asp:DropDownList ID="ddlFromTerr" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                    OnSelectedIndexChanged="ddlFromTerr_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none">
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none;" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdDoctor" runat="server" HorizontalAlign="Center" EmptyDataText="No Listed Doctor's Found"
                                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                            OnRowDataBound="grdDoctor_RowDataBound" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Customer Name"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Speciality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Territory">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transfer" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true" Text="  Transfer" />
                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkListedDR" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdChem" runat="server" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                            AutoGenerateColumns="false" Visible="false" GridLines="None" CssClass="mGrid"
                                            OnRowDataBound="grdChem_RowDataBound" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdChemist.PageIndex * grdChemist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Chemists_Name" HeaderText="Chemists Name" HeaderStyle-ForeColor="white"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Chemists_Contact" HeaderText="Contact Person"
                                                    HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr_code" runat="server" Text='<%# Bind("Territory_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" HeaderStyle-ForeColor="white"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transfer" ItemStyle-HorizontalAlign="Center">
                                                    <%--<HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true" Text="  Transfer" />
                                    </HeaderTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkChemist" runat="server" AutoPostBack="true" OnCheckedChanged="chkChemist_Changed" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 45%">
                    <table>
                        <tr>
                            <td style="border: none" align="left">
                                <asp:Label ID="lbltoFF" Width="160px" runat="server" SkinID="lblMand" Text="Transfer To"></asp:Label>
                            </td>
                            <td style="border: none" align="left">
                                <asp:DropDownList ID="ddlToFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                    OnSelectedIndexChanged="ddlToFieldForce_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none" align="left">
                                <asp:Label ID="lblterrTo" Width="160px" runat="server" SkinID="lblMand" Text="Transfer To Territory"></asp:Label>
                            </td>
                            <td style="border: none" align="left">
                                <asp:DropDownList ID="ddlToTerr" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                    OnSelectedIndexChanged="ddlToTerr_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none" align="left">
                                <asp:Label ID="lblCount" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                            <td style="border: none" align="left">
                                <asp:Label ID="lblTo" Text="Please Select the Transfer To" runat="server" ForeColor="Red"
                                    Font-Size="Large" Visible="false"></asp:Label>
                                <asp:Label ID="lblToTerr" Text="Select the Transfer To Territory" runat="server"
                                    ForeColor="Red" Font-Size="Large" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none;" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GrdTransfer" runat="server" HorizontalAlign="Center" EmptyDataText="No Listed Doctor's Found"
                                            AutoGenerateColumns="false" OnRowDataBound="GrdTransfer_RowDataBound" GridLines="None"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Customer Name"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Speciality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr_code" runat="server" Text='<%# Bind("Territory_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderText="Territory">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="200" HeaderText="Color" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColor" runat="server" Text='<%#Eval("color")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdChemist" runat="server" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                            AutoGenerateColumns="false" Visible="false" OnRowDataBound="grdChemist_RowDataBound"
                                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdChemist.PageIndex * grdChemist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Chemists_Name" HeaderText="Chemists Name" HeaderStyle-ForeColor="white"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Chemists_Contact" HeaderText="Contact Person"
                                                    HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Chemists_Contact")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" HeaderStyle-ForeColor="white"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="200" HeaderText="Color" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColor" runat="server" Text='<%#Eval("color")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblSelect" Text="Please Select the Transfer From" runat="server" ForeColor="Red"
            Font-Size="Large" Visible="false"></asp:Label>
        <asp:Label ID="lblTerr1" Text="Please Select the Transfer From Territory" runat="server"
            ForeColor="Red" Font-Size="Large" Visible="false"></asp:Label>
        <br />
        <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Visible="false" CssClass="BUTTON"
            OnClick="btnTransfer_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="BUTTON" Visible="false"
            OnClick="btnClear_Click" />
    </center>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../../Images/loader.gif" alt="" />
    </div>
    </form>
</body>
</html>
