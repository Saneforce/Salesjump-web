﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemistCategoryList.aspx.cs" Inherits="MasterFiles_ChemistCategoryList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemists-Category</title>

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
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
         <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" />
      <br />
        <table width="80%">
          <tr>
           <td style="width:9.2%" />
           <td>
              <asp:Button ID="btnNew" runat="server" CssClass="BUTTON"  Width="60px" Height="25px" Text="Add" onClick="btnNew_Click" />&nbsp; 
              <asp:Button ID="btnBulkEdit" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="Bulk Edit" onClick="btnBulkEdit_Click" />&nbsp;                             
              <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="S.No Gen" onClick="btnSlNo_Gen_Click" />&nbsp;
              <asp:Button ID="btnTransfer_Cat" runat="server" CssClass="BUTTON"  Width="120px" Height="25px" Text="Transfer Category" onClick="btnTransfer_Cat_Click" />&nbsp;
              <asp:Button ID="btnReactivate" runat="server" CssClass="BUTTON"  Width="100px" Height="25px" Text="Reactivation" OnClick="btnReactivate_Onclick" />
           </td>
           <td></td>
          </tr>
        </table>
        <br />

        <table align="center" style="width:100%" >
          <tbody>
            <tr>
              <td colspan="2" align="center" >
                <asp:GridView ID="grdChemCat" runat="server" Width="85%" HorizontalAlign="Center"
                 AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                 onrowupdating="grdChemCat_RowUpdating" onrowediting="grdChemCat_RowEditing" 
                 onrowdeleting="grdChemCat_RowDeleting" EmptyDataText="No Records Found"
                 onpageindexchanging="grdChemCat_PageIndexChanging" OnRowCreated="grdChemCat_RowCreated"
                 onrowcancelingedit="grdChemCat_RowCancelingEdit" onrowcommand="grdChemCat_RowCommand" 
                 GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                  AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdChemCat_Sorting"  >
                  <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                     <Columns >
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chemist Code" Visible="false" >
                           <ItemTemplate >
                             <asp:Label ID="lblChemCatCode" runat="server" Text='<%#Eval("Cat_Code")%>'></asp:Label>
                           </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField SortExpression="Chem_Cat_SName" HeaderStyle-Width="120px" HeaderStyle-ForeColor="white" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtChem_Cat_SName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="12"  Text='<%# Bind("Chem_Cat_SName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblChem_Cat_SName" runat="server" Text='<%# Bind("Chem_Cat_SName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField SortExpression="Chem_Cat_Name" HeaderStyle-Width="260px" HeaderStyle-ForeColor="white" HeaderText="Category Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtChemCatName"  SkinID="TxtBxAllowSymb"  runat="server" Width="200px" MaxLength="100" Text='<%# Bind("Chem_Cat_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblChemCatName" runat="server" Text='<%# Bind("Chem_Cat_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="No of Chemists"  HeaderStyle-Width="160px">
                          <ItemTemplate>
                            <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Cat_Count") %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>  

                         <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="ChemistCategory.aspx?Cat_Code={0}"
                                DataNavigateUrlFields="Cat_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Cat_Code") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Chemists Category');">Deactivate
                                    </asp:LinkButton>
                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="This Category Exists in Chemists" />
                                        </asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                     </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
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
