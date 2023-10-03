<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorCampaignList.aspx.cs" Inherits="MasterFiles_DoctorCampaignList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer - Campaign</title>

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
      <table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                <asp:Button ID="btnNew" runat="server" CssClass="BUTTON"  Width="60px" Height="25px" Text="Add" onClick="btnNew_Click" />&nbsp;               
                <asp:Button ID="btnBulkEdit" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="Bulk Edit" onClick="btnBulkEdit_Click" />&nbsp;              
                <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="S.No Gen" onClick="btnSlNo_Gen_Click" />&nbsp;
            </td>
            <td></td>
        </tr>
      </table>
      <br />
     <table align="center" style="width: 100%">
     <tbody>
        <tr>
                <td colspan="2" align="center">
                <asp:GridView ID="grdDocSubCat" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found" 
                     onrowupdating="grdDocSubCat_RowUpdating" onrowediting="grdDocSubCat_RowEditing"
                     onrowdeleting="grdDocSubCat_RowDeleting"
                     onpageindexchanging="grdDocSubCat_PageIndexChanging" OnRowCreated="grdDocSubCat_RowCreated"
                     onrowcancelingedit="grdDocSubCat_RowCancelingEdit"  onrowcommand="grdDocSubCat_RowCommand"                 
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDocSubCat_Sorting">
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campaign Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocSubCatCode" runat="server" Text='<%#Eval("Doc_SubCatCode")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Doc_SubCatSName" HeaderStyle-Width="120px" HeaderStyle-ForeColor="white" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_SubCat_SName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="12" Text='<%# Bind("Doc_SubCatSName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_SubCat_SName" runat="server" Text='<%# Bind("Doc_SubCatSName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Doc_SubCatName" HeaderStyle-Width="300px" HeaderText="Campaign Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocSubCatName"  SkinID="TxtBxAllowSymb" Width="280px" runat="server" MaxLength="100" Text='<%# Bind("Doc_SubCatName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocSubCatName" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-Width="150px" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DoctorCampaign.aspx?Doc_SubCatCode={0}"
                                DataNavigateUrlFields="Doc_SubCatCode">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>            

                        <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_SubCatCode") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Sub-Category');">Deactivate
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                      <%--  <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Doc_SubCatCode") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Doctor Sub-Category');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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
