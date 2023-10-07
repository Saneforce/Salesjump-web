<%@ Page Title="Channel List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DoctorSpecialityList.aspx.cs" Inherits="MasterFiles_DoctorSpecialityList" %>
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Customer-Channel</title>

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
    <%--<ucl:Menu ID="menu1" runat="server" /> --%>

     <br />
      <table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                <asp:Button ID="btnNew" runat="server" CssClass="BUTTON"  Width="60px" Height="25px" Text="Add" onClick="btnNew_Click" />&nbsp;               
                <asp:Button ID="btnBulkEdit" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="Bulk Edit" onClick="btnBulkEdit_Click" Visible="false" />&nbsp;                
                <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="BUTTON"  Width="80px" Height="25px" Text="S.No Gen" onClick="btnSlNo_Gen_Click" Visible="false" />&nbsp;
           <asp:Button ID="btnTransfer" runat="server" CssClass="BUTTON"  Width="130px" Height="25px" Text="Transfer Outlet" onClick="btnTransfer_Spec_Click" Visible="false" />&nbsp;
           <asp:Button ID="btnReactivate" runat="server" CssClass="BUTTON"  Width="120px" Height="25px" Text="Reactivation" OnClick="btnReactivate_Click" Visible="false" />
            </td>
            <td></td>
        </tr>
      </table>
      <br />
     <table align="center" style="width: 100%">
    <tbody>
        <tr>
               <td colspan="2" align="center">
               <asp:GridView ID="grdDocSpe" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                     onrowupdating="grdDocSpe_RowUpdating" onrowediting="grdDocSpe_RowEditing"
                     onrowdeleting="grdDocSpe_RowDeleting" EmptyDataText="No Records Found" 
                     onpageindexchanging="grdDocSpe_PageIndexChanging" OnRowCreated="grdDocSpe_RowCreated"
                     onrowcancelingedit="grdDocSpe_RowCancelingEdit"  onrowcommand="grdDocSpe_RowCommand"                 
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDocSpe_Sorting">
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="gridview1"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                  <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDocSpe.PageIndex * grdDocSpe.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Speciality Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocSpeCode" runat="server" Text='<%#Eval("Doc_Special_Code")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-Width="120px" HeaderStyle-ForeColor="white" HeaderText="Outlet Type Code" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_Spe_SName" runat="server" SkinID="TxtBxAllowSymb"  MaxLength="6" Text='<%# Bind("Doc_Special_SName") %>'></asp:TextBox>
								<asp:RequiredFieldValidator ID="rfvsn" ControlToValidate="txtDoc_Spe_SName" runat="server"
                                            ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Spe_SName" runat="server"  Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
								
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField  ItemStyle-Width="260px" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left" HeaderText="Outlet Type">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocSpeName"  SkinID="TxtBxAllowSymb" Width="200px"  runat="server" MaxLength="100" Text='<%# Bind("Doc_Special_Name") %>'></asp:TextBox>
								 <asp:RequiredFieldValidator ID="rfvn" ControlToValidate="txtDocSpeName" runat="server"
                                  ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocSpeName" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="No of Customers">
                        <ItemTemplate>
                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Spec_Count") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" ItemStyle-Width="150px"  HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>
                          <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="130px"  ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DoctorSpeciality.aspx?Doc_Special_Code={0}"
                                DataNavigateUrlFields="Doc_Special_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>
                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px" >
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_Special_Code") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Speciality');">Deactivate
                                    </asp:LinkButton>
                                       <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="This Speciality Exists in Doctor" />
                                        </asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Doc_Special_Code") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Doctor Speciality');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
--%>       
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
</asp:Content>