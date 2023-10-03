<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_View.aspx.cs" Inherits="MasterFiles_MR_RptAutoExpense" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
   
 <style type="text/css">
table {
    border-collapse: collapse;
}
.mainDiv
{
   background-color:White;
}
</style>
 
</head>
<body class="bodycss">
    <form class="mainDiv" id="form1" runat="server">
    <div class="mainDiv" id="mainDiv" runat="server" >
     <center><u><font class="print" style="color:blue;cursor:pointer;font-weight:bold" size="2">Territory View For The HQ : </font><font style="color:red;cursor:pointer;font-weight:bold" size="3">-<span id="hqId" runat="server"></span></font></u></center>
 <br />
     <table  width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdExpMain" runat="server" Width="50%" Font-Size="8pt" 
                            HeaderStyle-BackColor="#FFEFD5" HeaderStyle-CssClass="mainGrid" HeaderStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="black"  HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" cellpadding="10" cellspacing="5">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdExpMain.PageIndex * grdExpMain.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TerritoryName" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                         <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ADate" runat="server" Text='<%# Bind("territory_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDayName" runat="server" Text='<%# Bind("Town_Cat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                </Columns>
                                <EmptyDataRowStyle ForeColor="#9AA3A9" Height="5px" BorderColor="#9AA3A9"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
           
    </div>
    </form>
</body>
</html>
