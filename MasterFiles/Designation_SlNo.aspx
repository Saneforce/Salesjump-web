<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Designation_SlNo.aspx.cs"
    Inherits="MasterFiles_Designation_SlNo" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designation Serial No Generation</title>
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
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
        <table>
            <tr>
                <td align="center">
                    <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand">Division Name</asp:Label>
                    <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </center>
        <table width="100%" align="center">
            <tbody>
                <th style="font-size: medium;">
                    Base Level
                </th>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdBaselevel" runat="server" Width="50%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowSorting="True"
                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <%-- <asp:BoundField DataField="Doc_ClsCode" ShowHeader="true" HeaderText="Class Code"  ItemStyle-Width="7%"  Visible="false"/>--%>
                                <asp:TemplateField HeaderText="Designation_Code" Visible="false">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesign_code" runat="server" Text='<%#Bind("Designation_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Designation_Short_Name" HeaderStyle-ForeColor="white"
                                    ShowHeader="true" ItemStyle-HorizontalAlign="Left" HeaderText="Short Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Designation_Name" HeaderStyle-ForeColor="white" ShowHeader="true"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Designation Name" ItemStyle-Width="20%" />
                                <asp:TemplateField HeaderText="Existing S.No" HeaderStyle-ForeColor="white" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New S.No" HeaderStyle-ForeColor="white" ItemStyle-Width="6%"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBaseSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="2"
                                            Width="50%" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <center>
            <asp:Button ID="btnbase" runat="server" Text="Update" Width="80px" Height="25px"
                OnClick="btnbase_Click" CssClass="BUTTON" />
        </center>
        <br />
        <table width="100%" align="center">
            <tbody>
                <th style="font-size: medium;">
                    Managers
                </th>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdmanager" runat="server" Width="50%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowSorting="True"
                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <%-- <asp:BoundField DataField="Doc_ClsCode" ShowHeader="true" HeaderText="Class Code"  ItemStyle-Width="7%"  Visible="false"/>--%>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Des_Code" runat="server" Text='<%#Bind("Designation_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Designation_Short_Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="white" ShowHeader="true" HeaderText="Short Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Designation_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white"
                                    ShowHeader="true" HeaderText="Designation Name" ItemStyle-Width="20%" />
                                <asp:TemplateField HeaderText="Existing S.No" HeaderStyle-ForeColor="white" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New S.No" HeaderStyle-ForeColor="white" ItemStyle-Width="6%"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtManSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="2"
                                            Width="50%" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <center>
            <asp:Button ID="btnManager" runat="server" Text="Update" Width="80px" Height="25px"
                OnClick="btnManager_Click" CssClass="BUTTON" />
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
