<%@ Page Title="Class List" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="DoctorClassList.aspx.cs" Inherits="MasterFiles_DoctorClassList" %>
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
  <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Customer-Class</title>

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
<%--    <ucl:Menu ID="menu1" runat="server" /> --%>

    <br />
     <table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                <asp:Button ID="btnNew" runat="server" CssClass="BUTTON"  Width="60px" Height="25px" Text="Add" onClick="btnNew_Click" />&nbsp;               
                <asp:Button ID="btnBulkEdit" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="Bulk Edit" onClick="btnBulkEdit_Click" Visible="false" />&nbsp;                
                <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="S.No Gen" onClick="btnSlNo_Gen_Click" Visible="false" />&nbsp;
                <asp:Button ID="btnDoc_Class_Trans" runat="server" CssClass="BUTTON"  Width="120px" Height="25px" Text="Transfer Class" OnClick="btnDoc_Class_Trans_Click" Visible="false" />&nbsp;
                 <asp:Button ID="btnReactivate" runat="server" CssClass="BUTTON"  Width="120px" Height="25px" Text="Reactivation" OnClick="btnReactivate_Onclick" Visible="false" />
            </td>
            <td></td>
        </tr>
      </table>
      <br />
        <table align="center" style="width: 100%">
        <tbody>
        <tr>
               <td colspan="2" align="center">
               <asp:GridView ID="grdDocCls" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                     onrowupdating="grdDocCls_RowUpdating" onrowediting="grdDocCls_RowEditing"
                     onrowdeleting="grdDocCls_RowDeleting" EmptyDataText="No Records Found" 
                     onpageindexchanging="grdDocCls_PageIndexChanging" OnRowCreated="grdDocCls_RowCreated"
                     onrowcancelingedit="grdDocCls_RowCancelingEdit"  onrowcommand="grdDocCls_RowCommand"                 
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDocCls_Sorting">
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocClsCode" runat="server" Text='<%#Eval("Doc_ClsCode")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Doc_ClsSName" ItemStyle-Width="120px" HeaderStyle-ForeColor="white" HeaderText="Class Code" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_Cls_SName" MaxLength="6" runat="server" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Doc_ClsSName") %>'></asp:TextBox>
								  <asp:RequiredFieldValidator ID="rfvsn" ControlToValidate="txtDoc_Cls_SName" runat="server"
                                            ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Cls_SName" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Doc_ClsName" ItemStyle-Width="250px" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left" HeaderText="Class Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocClsName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="100" Text='<%# Bind("Doc_ClsName") %>'></asp:TextBox>
								 <asp:RequiredFieldValidator ID="rfvn" ControlToValidate="txtDocClsName" runat="server"
                                            ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocClsName" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="No of Customers"  ItemStyle-Width="120px">
                        <ItemTemplate>
                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Cls_Count") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:CommandField ShowHeader="True" EditText="Inline Edit"  ItemStyle-Width="140px" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>
                         <asp:HyperLinkField HeaderText="Edit" Text="Edit"  ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DoctorClass.aspx?Doc_ClsCode={0}"
                                DataNavigateUrlFields="Doc_ClsCode">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>
                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="140px">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_ClsCode") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Class');">Deactivate
                                    </asp:LinkButton>
                                       <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="This Class Exists in Doctor" />
                                        </asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                     <%--   <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Doc_ClsCode") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Doctor Class');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
--%>                      </Columns>
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
</asp:Content>