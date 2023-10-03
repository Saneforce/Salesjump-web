<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDR_SlNo_Gen.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_ListedDR_SlNo_Gen" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer Serial No Generation</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <style type="text/css">
        .marRight
        {
            margin-right: 35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <%-- <ucl:Menu ID="menu1" runat="server" />--%>
    </div>
    <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
        <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
    </asp:Panel>
    <table id="Table1" runat="server" width="90%">
        <tr>
            <td align="right" width="30%">
                <%--    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" align="center">
        <tbody>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                        EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound"
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        AllowSorting="True" OnSorting="grdDoctor_Sorting">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Customer Name"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Doc_Cat_ShortName" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Category" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Doc_Spec_ShortName" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Channel" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Doc_Qua_Name" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Qualification" HeaderStyle-ForeColor="White" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Doc_Class_ShortName" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Class" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Territory" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Existing S.No" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlSlNo" runat="server" DataSource="<%# Get_SlNo() %>" DataTextField="SlNO"
                                        DataValueField="SlNO" SkinID="ddlRequired">
                                    </asp:DropDownList>
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
    <br />
    <center>
        <asp:Button ID="btnSubmit" runat="server" Width="60px" Height="25px" Text="Save"
            CssClass="BUTTON" OnClick="btnSubmit_Click" />
        &nbsp;
        <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear"
            CssClass="BUTTON" OnClick="btnClear_Click" />
    </center>
    </form>
</body>
</html>
