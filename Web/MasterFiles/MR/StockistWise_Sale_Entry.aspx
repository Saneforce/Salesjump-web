<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockistWise_Sale_Entry.aspx.cs" Inherits="MasterFiles_MR_StockistWise_Sale_Entry" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>StockistWise Product Sale Entry</title>
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
       <style type="text/css">
             #tblddlDetails
        {
            margin-left: 350px;
        }
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
          <br />
    <br />
    <table width="757px" border="0" cellpadding="3" cellspacing="3" 
            id="tblddlDetails" align="center"> 
    <tr style="height:30px">
     
                <td width="80">
                
                    <asp:Label ID="lblStockistName" runat="server" Text="Stockist Name "></asp:Label>
                </td>
                <td width="180">
                    <asp:DropDownList ID="ddlStockist" runat="server" AutoPostBack="true" SkinID="ddlRequired" DataNavigateUrlFormatString="Stockist_Sale.aspx?stockist_code={0}" 
                       DataNavigateUrlFields="stockist_code">     
                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>               
                    </asp:DropDownList>
                </td>
                <td width="40">
                    <asp:Label ID="lblMonth" runat="server" Text="Month "></asp:Label>
                </td>
                <td width="130">
                     <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" >
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="40">
                    <asp:Label ID="lblYear" runat="server" Text="Year "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" SkinID="ddlRequired" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlYear_SelectedIndexChanged">
                         <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                         <asp:ListItem Text = "2012" ></asp:ListItem>
                         <asp:ListItem Text = "2013" ></asp:ListItem>
                         <asp:ListItem Text = "2014" ></asp:ListItem>                         
                    </asp:DropDownList>
                </td>
            </tr>
            </table>
            <table id="tblRate" runat="server" width="100%" align="center" >
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdProdRate" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" 
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
                            <asp:TemplateField HeaderText="Prod_Code" Visible="false">
                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProd_Code" runat="server" Text='<%#   Bind("Product_Detail_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                   <asp:Label ID="lblDP" runat="server" Text='<%#   Bind("Distributor_Price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField   SortExpression="Product_Detail_Name" HeaderText="Product Name">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProdName"  runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="UOM">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("Product_Sale_Unit")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OB Qty">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Right"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMRP" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("MRP_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Receipt from SS">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRP" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("Retailor_Price"))%>'  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                             <asp:TemplateField HeaderText="Sale Return from field to stock">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNSR" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("NSR_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sale">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTarg" Width="60px" style="text-align:right;" MaxLength="8"
                                     CssClass="TEXTAREA" Text='<%#(Eval("Target_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Qty">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Right"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMRP" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("MRP_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="CB">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTarg" Width="60px" style="text-align:right;" MaxLength="8"
                                     CssClass="TEXTAREA" Text='<%#(Eval("Target_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </td> 
            </tr> 
        </tbody>
    </table>
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="btnSubmit" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Save" 
                    onclick="btnSubmit_Click" />
            </td>
        </tr>
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
