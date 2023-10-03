<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayList_SlNo.aspx.cs"
    Inherits="MasterFiles_HolidayList_SlNo" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday SlNo Generation</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
<br/>
        <center>
        <table>
            <tr>
                <td >
                    <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand">Division Name</asp:Label>
                    <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </center>
        <table align="center" style="width: 100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdHoliday" runat="server" Width="85%" HorizontalAlign="Center"
                        PageSize="40" EmptyDataText="No Records Found" AllowSorting="True" AutoGenerateColumns="false"
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="gridview1">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNumber" runat="server" Text='<%#  (grdHoliday.PageIndex * grdHoliday.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Holiday_Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHoliday_Id" runat="server" Text='<%#Bind("Holiday_Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Holiday Name"  HeaderStyle-ForeColor="white"
                                ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtHolidayName" SkinID="TxtBxAllowSymb" Width="160px" runat="server"
                                        MaxLength="70" onkeypress="CharactersOnly(event);" Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--   <asp:TemplateField HeaderText="Fixed Date" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Fixed_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Existing S.No" HeaderStyle-ForeColor="white" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" HeaderStyle-ForeColor="white"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="3"
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
        </table>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" Text="Generate - Sl No" Width="110px" Height="25px"
                CssClass="BUTTON" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear"
                CssClass="BUTTON" onclick="btnClear_Click"  />
        </center>
    </div>
    </form>
</body>
</html>
