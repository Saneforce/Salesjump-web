<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval_View.aspx.cs" Inherits="MasterFiles_Approval_View" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Approval Managers</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    </div>
    <br /> 
    <table border="0" id="tblMgrDtls" align= "right" style="width: 30%"> 
    <tr style="height:30px"> 
    <td>
    <asp:Label ID="lblFilter" runat="server" Text="Select the Manager"></asp:Label>
    &nbsp;
    
                    <asp:DropDownList ID="ddlFilter" runat="server" Width="165" 
                        onselectedindexchanged="ddlFilter_SelectedIndexChanged"></asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" onclick="btnGo_Click" />
                    </td>   
                                  
                    </tr>
                    </table>
                    <table>
                    <tr>
                    
                    </tr>
                    </table>


                    <table width="100%" align="center">
        <tbody>
        <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdSalesForce" runat="server" Width="80%" HorizontalAlign="Center" 
                        AutoGenerateColumns="False" AllowPaging="True" 
                        onpageindexchanging="grdSalesForce_PageIndexChanging"  OnPreRender="grdSalesForce_PreRender"  OnRowCreated="grdSalesForce_RowCreated"                            
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" ShowHeader="False" >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle Wrap="true" />
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
                            
                            <asp:TemplateField HeaderText="FieldForce Name">
                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblsfName"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField  HeaderText="HQ">
                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField  HeaderText="Reporting_To">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>                  
                            <asp:TemplateField  HeaderText="DCR_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("DCR_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="TP_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("TP_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="LstDr_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("LstDr_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Leave_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Leave_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="SS_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("SS_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Expense_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Expense_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Otr_AM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Otr_AM") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
