<%@ Page Title="Distributor List" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="DistributorList.aspx.cs" Inherits="MasterFiles_DistributorList" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Distributor List</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
 	<script language="javascript" type="text/javascript">
        function popUp(Stockist_Code, Stockist_Name) {
            strOpen = "rptDis_Terr.aspx?Stockist_Code=" + Stockist_Code + "&Stockist_Name=" + Stockist_Name
            window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=400,height=600,left = 0,top = 0');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <table width="100%">
            <tr>
                <td style="width: 7.4%" />
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary btn-md"
                        Text="Add" OnClick="btnNew_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
         <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Height="40px" Width="40px" OnClick="ExportToExcel" />
            <br />
     
                        <div align="left">
                           
                            <asp:TextBox ID="txtSearch1" runat="server" Width="100px" Height="25px" Visible="false" />
                            <asp:Button ID="Button1" Text="Search" runat="server" OnClick="Search" Visible="false" />
                         
                        </div>
                  
            <br />
            <div style="width:100%;  text-align:center; margin:0%; padding:0% 7.5%">
        <table width="100%" align="center" >
            <tbody>
                <tr style="padding:5px 0px">
                 
                    <td align="left">
                        <asp:Label ID="SearchBy" runat="server" Text="SearchBy" Visible="true">
                        </asp:Label>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlFields" runat="server" SkinID="ddlRequired" CssClass="DropDownList" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                            <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                            <asp:ListItem Value="Stockist_Name">Distributor Name</asp:ListItem>
                               <asp:ListItem Value="Territory">Territory Name</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtsearch" runat="server" CssClass="TEXTAREA" SkinID="MandTxtBox" Visible="true"></asp:TextBox>
 						<asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'"
                                         SkinID="ddlRequired" TabIndex="4">
                                    </asp:DropDownList>
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server"  Text="Go" CssClass="btn btn-primary btn-md" Visible="true"></asp:Button>
                        <asp:Button ID="btnclr" OnClick="btnClear_Click" runat="server" 
                            Text="Clear" CssClass="btn btn-danger btn-md" Visible="true"></asp:Button>
                    </td>
                    <td align="right">
                    <asp:Label ID="lblFilter" runat="server" Font-Bold="true" ForeColor="Purple" Text="Filter By FO Name"></asp:Label>&nbsp;&nbsp;
                                    
                                    <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server" Visible="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                    </asp:DropDownList> &nbsp;
                                    <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary btn-md"
                                        Text="Go" OnClick="btnGo_Click" />
            </td>
                </tr>
                <tr align="center">
                    <td colspan="2" style="width: 100%" align="center">
                        <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                            runat="server" HorizontalAlign="Center" Visible="true">
                            <SeparatorTemplate>
                            </SeparatorTemplate>
                            <ItemTemplate>
                                &nbsp
                                <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px" Visible="true"
                                    ForeColor="#8A2EE6" CommandArgument='<%#bind("stockist_name") %>' Text='<%#bind("stockist_name") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </tbody>
        </table>
        </div>
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdStockist" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" OnRowUpdating="grdStockist_RowUpdating" OnRowDataBound="grdStockist_RowDataBound"
                            OnRowEditing="grdStockist_RowEditing" OnRowCreated="grdStockist_RowCreated" OnRowCancelingEdit="grdStockist_RowCancelingEdit"
                            EmptyDataText="No Records Found" OnRowCommand="grdStockist_RowCommand" OnPageIndexChanging="grdStockist_PageIndexChanging"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowPaging="True" PageSize="10" AllowSorting="True" OnSorting="grdStockist_Sorting">
                            <HeaderStyle Font-Bold="false" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdStockist.PageIndex * grdStockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distributor Code" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Code" runat="server" Visible="true" Text='<%# Eval("User_Entry_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Distributor Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="white">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStockist_Name" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150"
                                            Text='<%# Bind("Stockist_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStockist_ContactPerson" runat="server" SkinID="TxtBxAllowSymb"
                                            MaxLength="150" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_ContactPerson" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStockist_Mobile" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"
                                            Text='<%# Bind("Stockist_Mobile") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Field Officer" Visible="true">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Field_Officer" runat="server" Text='<%# Bind("Field_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser_Name" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Password" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPass_word" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" Visible="true">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"
                                            Text='<%# Bind("Territory") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								
                                <asp:TemplateField HeaderText="Count" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="DSM" runat="server" Text='<%# Bind("Stockist_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DSM Count" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkDSM" runat="server" Text='<%# Bind("Sub_Coun") %>' OnClick="Link_DSM"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
 								<asp:TemplateField HeaderText="Route Count" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblSubDiv_count" runat="server" CausesValidation="False" Text='<%# Eval("Sub_Couns") %>'
                                         OnClientClick='<%# "return popUp(\"" + Eval("Stockist_Code") + "\",\"" + Eval("Stockist_Name") + "\");"%>' >
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Fieldforce Name">             
             <ItemTemplate> 
                 <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("SF_Code") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>--%>
                                <%--<asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>--%>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="Distributor_Creation.aspx?stockist_code={0}"
                                    DataNavigateUrlFields="stockist_code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="Deactivate">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Stockist_Code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Stockist');">Deactivate
                                        </asp:LinkButton>
 										<asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      	<img src="../Images/deact1.png" alt="" width="55px" title="This Territory Name Exists in Product or Fieldforce" />
                                        </asp:Label>
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
        <%--<div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>--%>
    </div>
    </form>
</body>
</html>
</asp:Content>