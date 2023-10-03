<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SF_MGR_Reporting.aspx.cs" Inherits="MasterFiles_SF_MGR_Reporting" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Manager - Reporting Structure</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
            <br />
           <table width="100%" align="center">
            <tbody>               
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSalesForce" runat="server" Width="30%" HorizontalAlign="Center" 
                            onrowdatabound="grdSalesForce_RowDataBound"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>                
                                <asp:TemplateField HeaderText="S.No">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                    <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfName"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Reporting To">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlReporting_To" SkinID="ddlRequired" runat="server" DataSource ="<%# Fill_Reporting_To() %>" DataTextField="Sf_Name" DataValueField="sf_code">                                           
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>                                       
                            </Columns>
                        </asp:GridView>
                    </td> 
                </tr> 
            </tbody>
        </table>
        <center>
            <asp:Button ID="btnSubmit" runat="server" Text="Map Reporting Struture" CssClass="BUTTON"
                onclick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BUTTON"
                onclick="btnCancel_Click" />
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
