<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTPView.aspx.cs" Inherits="Reports_rptTPView" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TP View Report</title>
    
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" 
                                    Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" 
                                    BorderWidth="1" Height="25px" Width="60px" />    
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" 
                                    Font-Names="Verdana" Font-Size="10px" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" 
                                    Height="25px" Width="60px"     />    
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF"
                                 Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                 BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />    
                            </td>
                             <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" 
                                Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />

        <table border="0" width="90%">
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblHead" runat="server" Text="TP Consolidated View for the month of " Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>                    
                </td>
            </tr>
        </table>
        <br />
        <br />
        <center>
       <table width="90%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" onrowdatabound="grdTP_RowDataBound" GridLines="None" CssClass="mGrid"  
                        >
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Font-Size="Small" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State" ItemStyle-Width="70" >
                                <ItemTemplate>
                                    <asp:Label ID="lblState" runat="server" Font-Size="Small" Text='<%#Eval("StateName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Zone" ItemStyle-Width="70" >
                                <ItemTemplate>
                                    <asp:Label ID="lblZone" runat="server" Font-Size="Small" Text='<%#Eval("Zone_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Area" ItemStyle-Width="70" >
                                <ItemTemplate>
                                    <asp:Label ID="lblArea" runat="server" Font-Size="Small" Text='<%#Eval("Area_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="HQ" ItemStyle-Width="70" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Font-Size="Small" Text='<%#Eval("HQ_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="70" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTourPlan" runat="server" Font-Size="Small" Text='<%#Eval("tour_date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="DB" ItemStyle-Width="70" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDB" runat="server" Font-Size="Small" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Beat" ItemStyle-Width="100" >
                                <ItemTemplate>
                                    <asp:Label ID="lblBeat" runat="server" Font-Size="Small" Text='<%#Eval("Territory")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-Width="200" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFieldForceName" runat="server" Font-Size="Small" Text='<%#Eval("Tour_Schedule")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory Planned" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblterr" runat="server" Font-Size="Small" Text='<%# Bind("Worked_With_SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Objective" ItemStyle-Width="300">
                                <ItemTemplate>
                                    <asp:Label ID="lblObjective" runat="server" Font-Size="Small" Text='<%#Eval("objective")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td> 
            </tr> 
            <tr>
                <td>
                    <asp:Table ID="tbl" runat="server" Width="100%" align="center">
                    </asp:Table>                
                </td>
            </tr>
        </tbody>
    </table>
    </center>
    </div>
    </form>
</body>
</html>
