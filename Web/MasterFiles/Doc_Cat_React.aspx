<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doc_Cat_React.aspx.cs" Inherits="MasterFiles_Doc_Cat_React" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Category Reactivation</title>
         <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" />
    <br />
     <table align="center" style="width: 100%" >
     <tbody>
        <tr>            
            <td colspan="2" align="center">
               <asp:GridView ID="grdDocCat" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"                     
                     onpageindexchanging="grdDocCat_PageIndexChanging" onrowcommand="grdDocCat_RowCommand"                 
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocCatCode" runat="server" Text='<%#Eval("Doc_Cat_Code")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_Cat_SName" runat="server" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Doc_Cat_SName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Cat_SName" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="Category Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocCatName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="100" Text='<%# Bind("Doc_Cat_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocCatName" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Reactivate" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_Cat_Code") %>'
                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Doctor Category');">Reactivate
                                    </asp:LinkButton>
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
