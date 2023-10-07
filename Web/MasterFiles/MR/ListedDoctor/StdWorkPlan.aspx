<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StdWorkPlan.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_StdWorkPlan" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route Plan</title>

    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
  
        <table style="width:100%">
            <tr>
                <td style="width:20%;">
                    <asp:Panel ID="pnlGiftUnlst" runat="server" Width="12%" style="left:20px; top:150px; height:500px; position:absolute;" BorderStyle="Solid" BorderWidth="1" ScrollBars="Vertical">
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdTerritory" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" 
                                        AutoGenerateColumns="false" OnRowCommand="grdTerritory_RowCommand" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg" >
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood"/>
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>                
                                            <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Route Plan List" HeaderStyle-Width="200px">
                                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                                </ControlStyle>
                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                <ItemTemplate>
                                                    &nbsp;
                                                    <asp:LinkButton ID="lnkbutTerr" runat="server" CommandArgument='<%# Eval("Territory_Code") %>'
                                                        CommandName="Territory" Text ='<%#Eval("Territory_Name") %>' Font-Names="Tahoma" Font-Size="12px">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>

                <td style="width:60%;">
                    <asp:Panel ID="pnlDoctor" runat="server" BorderStyle="Solid" BorderWidth="1" visible="false" style="left:210px; top:150px; width:53%; position:absolute;">
                        <table>
                            <tr>
                            <td align = "center" >
                                 <asp:Label ID="lblrt" Text ="Route Plan Add/Delete/View" Font-Bold = "true"  ForeColor = "Navy"   Font-Size ="Small" runat="server"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td>
                            </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdDoctor" runat="server" Width="108%" Height="100%" HorizontalAlign="Center" 
                                        AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                        onrowdatabound="grdDoctor_RowDataBound">
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood"/>
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>                
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRemove" runat="server"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="300" HeaderText="Listed Doctor Name" >
                                                <ItemTemplate>
                                                    &nbsp;
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Name" >
                                                <ItemTemplate>
                                                    &nbsp;
                                                    <asp:Label ID="lblTerritoryName" runat="server" Text='<%#Eval("Territory_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Code" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritoryCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Code" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlanNo" runat="server" Text='<%#Eval("Plan_No")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200" HeaderText="Color" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblColor" runat="server" Text='<%#Eval("color")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Update" 
                                        onclick="btnSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="width:15%;">
                    <asp:Panel ID="pnlImgCopyMove" runat="server" BorderStyle="Solid" BorderWidth="1" Visible="false" style="left:850px; top:150px; 
                        position:absolute;">
<%--                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgCopyMove" runat="server"  ImageAlign="Middle" 
                                        ImageUrl="~/Images/arrowIcon.jpg" Width="25" onclick="imgCopyMove_Click" />
                                </td>
                            </tr>
                        </table>
--%>                    </asp:Panel>
                </td>
                <td style="width:45%;">
                    <asp:Panel ID="pnlMove" runat="server" BorderStyle="Solid" BorderWidth="1" Visible="false" style="left:950px; top:150px; 
                        position:absolute;">
                        <table>

                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblTerr" runat="server" Text="Route Plan" ></asp:Label>
                                    <asp:DropDownList ID="ddlTerritory" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                        onselectedindexchanged="ddlTerritory_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdCopyMove" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" 
                                        AutoGenerateColumns="false" RowStyle-Height="20px" GridLines="None" CssClass="mGridImg"
                                        >
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood"/>
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>                
                                            <asp:TemplateField HeaderText="Select" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCopyMove" runat="server" AutoPostBack="true" oncheckedchanged="chkCopyMove_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblDocCode_CopyMove" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="300" HeaderText="Listed Doctor Name" >
                                                <ItemTemplate>
                                                    &nbsp;
                                                    <asp:Label ID="lblDocName_CopyMove" runat="server" Text='<%#Eval("ListedDr_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Name" >
                                                <ItemTemplate>
                                                    &nbsp;
                                                    <asp:Label ID="lblTerritoryName_CopyMove" runat="server" Text='<%#Eval("Territory_Name")%>' Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="200" HeaderText="Plan Code" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlanNo_CopyMove" runat="server" Text='<%#Eval("Plan_No")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
