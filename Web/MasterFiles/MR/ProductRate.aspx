<%@ Page Title="Statewise - Product Rate View" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master" CodeFile="ProductRate.aspx.cs" Inherits="MasterFiles_MR_ProductRate" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Statewise - Product Rate View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
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
        .marright
        {
            margin-left: 90%;
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
            $('#btnGo').click(function () {
                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
            });
        }); 
    </script>
    <script type="text/javascript">
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=GrdDoctor.ClientID %>');

            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <div id="Divid" runat="server">
        </div>
        <br />
        <asp:Panel ID="pnldivision" runat="server" Visible="false">
            <center>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblSelect" Text="Please Select the Division Name" runat="server" ForeColor="DarkGreen"
                    Font-Size="Large" Visible="false"></asp:Label>
            </center>
        </asp:Panel>
        <center>
            <asp:Panel ID="pnlstate" runat="server" Visible="false">
                <table border="0" cellpadding="3" cellspacing="3">
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblState" runat="server" SkinID="lblMand">State Name</asp:Label>
                            <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:Button ID="btnstate" runat="server" CssClass="BUTTON" Text="Go" Width="35px"
                                Height="25px" OnClick="btnstate_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </center>
        <asp:Panel ID="pnlprint" runat="server" CssClass="marright">
            <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Print.GIF" ToolTip="Print"
                    Width="20px" Style="border-width: 0px;" />
            </asp:LinkButton>

             <asp:LinkButton ID="LinkButton1" ToolTip="Excel" runat="server" OnClientClick="PrintGridData()">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/excel_Img.png" ToolTip="Excel"
                    Width="20px" Style="border-width: 0px;" />
            </asp:LinkButton>
        </asp:Panel>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="GrdDoctor" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                            CssClass="mGrid" EmptyDataText="No Records Found" GridLines="None" HorizontalAlign="Center"
                            OnRowCreated="GVMissedCall_RowCreated" ShowHeader="false" Width="90%">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt" />
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="40px">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prod_Code" Visible="false">
                                    <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProd_Code" runat="server" Text='<%# Bind("Product_Detail_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="240px" HeaderText="Product Name" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProdName" SkinID="MandTxtBox" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="120px">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("product_unit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM Value" HeaderStyle-Width="90px">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Sample_Erp_Code")%>' CssClass="price"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRP" HeaderStyle-Width="140px">
                                    <ControlStyle Width="60%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Lab_DP_Base" runat="server" Text='<%#(Eval("DP_Base_Rate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distributor Price" HeaderStyle-Width="120px">
                                    <ControlStyle Width="60%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Lab_DP_Case" runat="server" Text='<%#(Eval("DP_Case_Rate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retailer Price" HeaderStyle-Width="120px">
                                    <ControlStyle Width="60%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Lab_RP_Base" runat="server" Text='<%#(Eval("RP_Base_Rate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NSR Price" HeaderStyle-Width="120px">
                                    <ControlStyle Width="60%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Lab_RP_Case" runat="server" Text='<%#(Eval("RP_Case_Rate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target Price" HeaderStyle-Width="120px">
                                    <ControlStyle Width="60%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                    </ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Lab_MRP_Rate" runat="server" Text='<%#(Eval("MRP_Rate"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>