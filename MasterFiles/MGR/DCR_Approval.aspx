﻿<%@ Page Language="C#" Title="DCR - Approval" AutoEventWireup="true" CodeFile="DCR_Approval.aspx.cs" MasterPageFile="~/Master_MGR.master" Inherits="MasterFiles_MGR_DCR_Approval" %>
<%--<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>DCR - Approval</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <%--<ucl:Menu ID="menu1" runat="server" />--%> 
        <br />
        <center>
       <table width="75%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                   <asp:GridView ID="grdDCR" runat="server" Width="60%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Data found for Approval's"   
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Name" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Designation" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Month" Visible ="false">
                                <ItemTemplate> 
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Mon")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
<%--                            <asp:HyperLinkField HeaderText="Month - Year" 
                                       DataNavigateUrlFormatString="~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                       DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-HorizontalAlign="Center">
                            </asp:HyperLinkField>                           
--%>
                            <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" 
                                DataNavigateUrlFormatString="DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>


                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                  
                </td> 
            </tr> 
        </tbody>
    </table>
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
</asp:Content>