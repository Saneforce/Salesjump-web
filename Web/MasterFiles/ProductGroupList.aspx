<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGroupList.aspx.cs"
    Inherits="MasterFiles_ProductGroupList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product-Group</title>
  
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
        .wrap
        {
            /* force the div to properly contain the floated images: */
            position: relative;
            float: left;
            clear: none;
            overflow: hidden;
        }
        .wrap img
        {
            position: relative;
            z-index: 1;
        }
        .wrap #lblimg
        {
            display: block;
            position: absolute;
            width: 100%;
            top: 30%;
            left: 0;
            z-index: 2;
            text-align: center;
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
        function popUp(Product_Grp_Code, Product_Grp_Name) {
            strOpen = "rptProduct_Grp.aspx?Product_Grp_Code=" + Product_Grp_Code + "&Product_Grp_Name=" + Product_Grp_Name
            window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=400,height=600,left = 0,top = 0');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <table width="80%">
            <tr>
                <td style="width: 9.2%" />
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
                        Text="Add" OnClick="btnNew_Click" />&nbsp;
                    <asp:Button ID="btnBulkEdit" runat="server" CssClass="BUTTON" Width="80px" Height="25px"
                        Text="Bulk Edit" OnClick="btnBulkEdit_Click" Visible="false" />&nbsp;
                    <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="BUTTON" Width="80px" Height="25px"
                        Text="S.No Gen" OnClick="btnSlNo_Gen_Click" Visible="false" />&nbsp;
                <asp:Button ID="btnReactivate" runat="server" Width="90px" Height="25px" 
                    CssClass="BUTTON" Text="Reactivation" onclick="btnReactivate_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdProGrp" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdProGrp_RowUpdating"
                            OnRowEditing="grdProGrp_RowEditing" OnRowDeleting="grdProGrp_RowDeleting" EmptyDataText="No Records Found"
                            OnPageIndexChanging="grdProGrp_PageIndexChanging" OnRowCreated="grdProGrp_RowCreated"
                            OnRowCancelingEdit="grdProGrp_RowCancelingEdit" OnRowCommand="grdProGrp_RowCommand"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" OnSorting="grdProGrp_Sorting">
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
                                <asp:TemplateField HeaderText="Group Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProGrpCode" runat="server" Text='<%#Eval("Product_Grp_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Group Code" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProduct_Grp_SName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="6"
                                            Text='<%# Bind("Product_Grp_SName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct_Grp_SName" runat="server" Text='<%# Bind("Product_Grp_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Group Name" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProGrpName" SkinID="TxtBxAllowSymb" runat="server" MaxLength="100"
                                            Text='<%# Bind("Product_Grp_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProGrpName" runat="server" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Products" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("Grp_count") %>'
                                              OnClientClick='<%# "return popUp(\"" + Eval("Product_Grp_Code") + "\",\"" + Eval("Product_Grp_Name")  + "\");" %>' >
                                            </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center"
                                    DataNavigateUrlFormatString="ProductGroup.aspx?Product_Grp_Code={0}" DataNavigateUrlFields="Product_Grp_Code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Product_Grp_Code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Product Group');">Deactivate
                                        </asp:LinkButton>
                                    
                                      <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >
                                        
                                    <img src="../Images/deact1.png" alt="" width="55px" title="This Category Exists in Product" />
                                        </asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--    <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Product_Grp_Code") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Product Group');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                                --%>
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
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
