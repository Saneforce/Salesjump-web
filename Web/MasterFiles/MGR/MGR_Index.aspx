<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Index.aspx.cs" Inherits="MasterFiles_MGR_MGR_Index" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager</title>
    <style type="text/css">
        .roundCorner
        {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 150px;
            height: 30px;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                            font-size: 22px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="roundCorner"
                            Text="Goto Home Page" OnClick="btnHome_Click" BackColor="Green" ForeColor="White"
                            Visible="false" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="roundCorner"
                            Text="Logout" OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblhomepage" runat="server" Font-Bold="true" Visible="false" ForeColor="Blue"
                Font-Size="Medium" Font-Names="Verdana">For Getting the
                <asp:Button ID="Button1" runat="server" BackColor="Green" ForeColor="White" Text="Goto Home Page"
                    Enabled="false" />
                Link --> (
                <asp:Label ID="lbltext" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                ) Approval is Mandatory</asp:Label>
            <br />
            <br />
            <table cellpadding="0" cellspacing="0" id="Table2" align="center" style="width: 100%;
                background-color: Gray; color: white">
                <tr>
                    <td align="center" width="90%">
                        <a href="../../MGR_Home.aspx" style="text-decoration: none; color: white">
                            <asp:Label ID="lblApproval" runat="server" Text="Approvals" Font-Size="Medium"></asp:Label>
                        </a>
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
                        <asp:GridView ID="grdDCR" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
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
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
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
        <br />
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
                        <asp:GridView ID="grdTP" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
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
                        <asp:GridView ID="grdTP_Calander" runat="server" Width="60%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" OnRowDataBound="grdTP_RowDataBound" EmptyDataText="TP No found for Approval"
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-ForeColor="Black">
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
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataTextField="Month" ShowHeader="false" DataNavigateUrlFormatString="TourPlan_Calen.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-Width="0px" ItemStyle-HorizontalAlign="Center">
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
        <br />
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
                        <asp:GridView ID="GridView1" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
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
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="App" DataNavigateUrlFormatString="../MGR/WrkTypeWise_Allowance.aspx?Month={0}&Year={1}&sf_code={2}&sf_name={3}"
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
        <br />
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
                            <asp:GridView ID="grdListedDR" runat="server" Width="60%" HorizontalAlign="Center"
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
                                    <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="ListedDR_Add_Approval.aspx?sfcode={0}"
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
            <br />
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
                            <asp:GridView ID="grdListedDR1" runat="server" Width="60%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                                RowStyle-Font-Size="Smaller" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Data found for Approval's">
                                <HeaderStyle Font-Bold="False" Font-Size="Smaller" />
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
                                            <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ">
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
                                    <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="ListedDR_DeActivate_Approval.aspx?sfcode={0}"
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
        <br />
        <center>
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="Lbladddeact" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                            ForeColor="Chocolate">Add Against Deactivated Customer Approval</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdadddeactivate" runat="server" Width="60%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                                <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="AddAgainstDeact_App.aspx?sfcode={0}"
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
            </table>
        </center>
        <br />
        <br />
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
                        <asp:GridView ID="grdLeave" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
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
            <!-- Secondary Sales -->
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblSecSales" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                            ForeColor="Chocolate">Secondary Sales</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdSecSales" runat="server" Width="60%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" OnRowDataBound="grdSecSales_RowDataBound"
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                                        <asp:Label ID="lblDes" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSSMonth" runat="server" Text='<%#Eval("Cur_Month")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSSYear" runat="server" Text='<%#Eval("Cur_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="~/MasterFiles/MR/SecSales/SecSalesEntry.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
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
            <!-- Secondary Sales -->
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
