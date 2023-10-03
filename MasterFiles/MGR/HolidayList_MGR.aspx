<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayList_MGR.aspx.cs"
    Inherits="MasterFiles_MGR_HolidayList_MGR" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday List</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
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
         <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
         <br />
        
        <table width="90%">
        <tr>
        <td style="width:8%" />
        <td>
        <asp:Button ID="btnNormal" runat="server" Text="Normal View" BackColor="LightPink" Width="110px" 
                onclick="btnNormal_Click" /> &nbsp;
        <asp:Button ID="btnCal" runat="server" Text="Calendar View" BackColor="LightBlue" Width="110px"  
                onclick="btnCal_Click" /> &nbsp;
                  <asp:Button ID="btnConsol" runat="server" Visible="false" Text="Consolidated View" BackColor="LightBlue" Width="130px"  
                onclick="btnConsol_Click" />
        </td>
        </tr>
        </table>
        <center>
        <table id="tblTpRpt" cellpadding="3" cellspacing="3">
           <tr>
               <td align="left" class="stylespc">
                   <asp:Label ID="lblDivision" runat="server" Text="Division Name" SkinID="lblMand"
                            Height="16px"></asp:Label>
               </td>
               <td align="left" class="stylespc">
                   <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
               </td>
           </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblState" runat="server" Text="State Name" SkinID="lblMand" Height="20px"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                   <asp:DropDownList ID="ddlStateName"  runat="server" SkinID="ddlRequired">
                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                   </asp:DropDownList>
                </td>
            </tr>
           <tr >
                <td align="left" class="stylespc">
                     <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand" Height="20px"></asp:Label>
                </td>
               
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server"  SkinID="ddlRequired">
                     <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>                
                    </asp:DropDownList>&nbsp
                     <asp:Button ID="btnGo" runat="server" Width="35px" Height="25px" 
                        CssClass="BUTTON" Text="Go" onclick="btnGo_Click" />
                </td>
                
                   
                        
            </tr>            
        </table>
        <br />

        </center>
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdHoliday" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="15" EmptyDataText="No Records Found"
                            OnPageIndexChanging="grdHoliday_PageIndexChanging" OnRowCreated="grdHoliday_RowCreated"  OnRowDataBound="grdHoliday_RowDataBound"
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" OnSorting="grdHoliday_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HSlno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHSlno" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StateCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Academic_Year" HeaderStyle-ForeColor="White" HeaderText="Academic Year" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Academic_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="State_Name" HeaderStyle-ForeColor="White" HeaderText="State" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%#Eval("State_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Holiday_Name" HeaderStyle-ForeColor="White" HeaderText="Holiday Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtHolidayeName" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Holiday_Date" HeaderStyle-ForeColor="White" HeaderText="Holiday Date" 
                                    ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDate" runat="server" onkeypress="Calendar_enter(event);" SkinID="TxtBxAllowSymb" Text='<%# Bind("Holiday_Date") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Holiday_Date") %>'></asp:Label>
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
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
