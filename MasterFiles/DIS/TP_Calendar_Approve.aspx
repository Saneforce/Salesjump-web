<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Calendar_Approve.aspx.cs"
    Inherits="MasterFiles_MGR_TP_Calendar_Approve" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tour Plan Approval</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
      <ucl:Menu ID="menu1" runat="server" />
    <div>
    <center>
        <asp:Panel ID="pnlCalender" runat="server">
            <table width="75%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdTP_Calander1" runat="server" Width="65%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" OnRowDataBound="grdTP_RowDataBound" EmptyDataText="TP No found for Approval"
                                GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Force Name" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_HQ" Width="80px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tour Month" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tour Year" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="TourPlan_Calen.aspx?refer={0}"
                                        DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
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
                     <tr>
                        <td align="center">
                             <asp:GridView ID="grdTP" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                     EmptyDataText="No Data found for Approval's"
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
                                        <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" 
                                            DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}"
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
                                GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                               AlternatingRowStyle-CssClass="alt" 
                               >
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Designation" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_HQ" Width="80px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tour Month">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tour Year" >
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
                                     <asp:HyperLinkField DataTextField="Month" ShowHeader="false"
                                            DataNavigateUrlFormatString="TourPlan_Calen.aspx?refer={0}"
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
                </tbody>
            </table>
        </asp:Panel>
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
