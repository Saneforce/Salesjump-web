<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorQualificationList.aspx.cs" Inherits="MasterFiles_DoctorQualificationList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer-Qualification</title>

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
               <asp:Button ID="btnTransfer_Qua" runat="server" CssClass="BUTTON"  Width="150px" Height="25px" Text="Transfer Qualification" onClick="btnTransfer_Qua_Click" />&nbsp;
                <asp:Button ID="btnReactivate" runat="server" CssClass="BUTTON"  Width="120px" Height="25px" Text="Reactivation" OnClick="btnReactivate_Onclick" />
            </td>
            <td></td>
        </tr>
      </table>
      <br />
        <table align="center" style="width: 100%">
        <tbody>
        <tr>
               <td colspan="2" align="center">
               <asp:GridView ID="grdDocQua" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                     onrowupdating="grdDocQua_RowUpdating" onrowediting="grdDocQua_RowEditing" 
                     onrowdeleting="grdDocQua_RowDeleting" onrowcommand="grdDocQua_RowCommand"      
                     onpageindexchanging="grdDocQua_PageIndexChanging" OnRowCreated="grdDocQua_RowCreated"
                     onrowcancelingedit="grdDocQua_RowCancelingEdit"   
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDocQua_Sorting">
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="gridview1"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDocQua.PageIndex * grdDocQua.PageSize) +  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qualification Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocQuaCode" runat="server" Text='<%#Eval("Doc_QuaCode")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:TemplateField SortExpression="Doc_QuaSName" HeaderStyle-ForeColor="white" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_Qua_SName" runat="server" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Doc_QuaSName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Qua_SName" runat="server" Text='<%# Bind("Doc_QuaSName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField SortExpression="Doc_QuaName" HeaderStyle-ForeColor="white" HeaderText="Qualification Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocQuaName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="100" Text='<%# Bind("Doc_QuaName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocQuaName" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="No of Customers">
                        <ItemTemplate>
                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Qua_Count") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>
                         <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DoctorQualification.aspx?Doc_QuaCode={0}"
                                DataNavigateUrlFields="Doc_QuaCode">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>
                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_QuaCode") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Qualification');">Deactivate
                                    </asp:LinkButton>
                                       <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="This Qualification Exists in Doctor" />
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
