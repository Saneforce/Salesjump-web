<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicMaster.aspx.cs" Inherits="BasicMaster" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menumas" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Reporting Sales & Analysis</title>
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
    <link type="text/css" rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menumas ID="menumas" runat="server" />
        <center>
            <table>
                <tr>
                    <td align="center" width="90%">
                        <%--<a href="../../MGR_Home.aspx" style="text-decoration:none; color:white" >--%>
                        <asp:Label ID="lblApproval" runat="server" Text="Approvals" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <%--   </a>--%>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lbldcr" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                            ForeColor="Chocolate">DCR</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdDCR" runat="server" Width="65%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="mGridImg"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Mon")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                            <asp:HyperLinkField HeaderText="Month - Year" 
                                       DataNavigateUrlFormatString="~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                       DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-HorizontalAlign="Center">
                            </asp:HyperLinkField>                           
                                --%>
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                    DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <%--             <br />--%>
        <center>
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lbltp" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                            ForeColor="Chocolate">Tour Plan</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdTP" runat="server" Width="65%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="mGridImg"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Field Force Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Name" Width="160px" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDG" Width="100px" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_HQ" Width="120px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Width="80px" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                        <center>
                            <table width="100%" align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblcrm" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                                            ForeColor="Chocolate">Expense</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="GridView1" runat="server" Width="60%" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Field Force Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Name" Width="160px" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMonth" Width="80px" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:HyperLinkField HeaderText="Approve" DataTextField="App" DataNavigateUrlFormatString="../../../../MasterFiles/Exp_Allowance.aspx?Month={0}&Year={1}&sf_code={2}&sf_name={3}"
                                                    DataNavigateUrlFields="Month,Year,sf_code,sf_name" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                                    </ControlStyle>
                                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                </asp:HyperLinkField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <asp:GridView ID="grdTP_Calander" runat="server" Width="65%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" OnRowDataBound="grdTP_RowDataBound" EmptyDataText="TP No found for Approval"
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_HQ" Width="80px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="~/MasterFiles/MR/TourPlan.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataTextField="Month" ShowHeader="false" DataNavigateUrlFormatString="~/MasterFiles/MGR/TourPlan_Calen.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblAdd" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                                ForeColor="Chocolate">Listed Customer Addition</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdListedDR" runat="server" Width="65%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                                CssClass="mGridImg" PagerStyle-CssClass="pgr" RowStyle-Font-Size="Smaller" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" Font-Size="Smaller" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Approve">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutApprove" runat="server" CommandArgument='<%# Eval("Sf_Code") %>'
                                        CommandName="Approve" OnClientClick="return confirm('Do you want to Approve');">Approve
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="~/MasterFiles/MGR/Listeddr_admin_Approval.aspx?sfcode={0}"
                                        DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    </asp:HyperLinkField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <%--     <br />--%>
            <center>
                <table width="100%" align="center">
                </table>
            </center>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                                ForeColor="Chocolate">Listed Customer Deactivation</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdListedDR_Dea" runat="server" Width="65%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--             <asp:TemplateField HeaderText="Click Here">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutApprove" runat="server" CommandArgument='<%# Eval("Sf_Code") %>'
                                        CommandName="Approve" OnClientClick="ListedDR_DeActivate_Approval.aspx">Approve
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="~/MasterFiles/MGR/Listeddr_adm_deAct_Approval.aspx?sfcode={0}"
                                        DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    </asp:HyperLinkField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
        </center>
        <br />
        <%-- <br />--%>
        <center>
            <%--<table width="100%" align="center">
        <tr>
        <td align="center">
        <asp:Label ID="Lbladddeact" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">Add Against Deactivated Doctor Approval</asp:Label>
        </td>
        </tr>
        <tr>
        <td colspan="2" align="center">
        <asp:GridView ID="grdadddeactivate" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
        EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <HeaderStyle Font-Bold="false" />
        <SelectedRowStyle BackColor="BurlyWood" />
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
             <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Designation" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="AddAgainstDeact_App.aspx?sfcode={0}"
                                        DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    </asp:HyperLinkField>
        </Columns>
             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:GridView>
        </td>
        </tr>
        </table>
        </center>
        <br />
        <br />  --%>
            <center>
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblleave" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                                ForeColor="Chocolate">Leave</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdLeave" runat="server" Width="65%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="mGridImg"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveCode" runat="server" Text='<%#Eval("Leave_Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designaion" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp.Code" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemp" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Date" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("From_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("To_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Days" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldays" runat="server" Text='<%#Eval("No_of_Days")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="~/MasterFiles/MR/LeaveForm.aspx?sfcode={0}&amp;Leave_Id={1}"
                                        DataNavigateUrlFields="SF_Code,Leave_Id" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    </asp:HyperLinkField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </center>
    </div>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="Images/loader.gif" alt="" />
    </div>
    </form>
</body>
</html>
