<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sub_HO_ID_View.aspx.cs" Inherits="MasterFiles_Sub_HO_ID_View" %>
<%@ Register Src ="~/UserControl/pnlMenu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HO-ID-View</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <br />

          <table align="center" width="100%">
             <tr>
             <td style="width:7.4%" />
               <td>
                  <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Width="60px" Height="25px" Text ="Add" OnClick ="btnNew_Click" />&nbsp;  
               </td>
             </tr>
          </table>
          <br />

          <table align="center" style="width: 100%">
            <tbody >
              <tr>
                <td colspan="2" align="center" >
                     <asp:GridView ID="grdSubHoID" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowCommand ="grdSubHoID_RowCommand" 
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                     AlternatingRowStyle-CssClass="alt">
                     <HeaderStyle Font-Bold="false" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                     <Columns >
                       <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HO ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHOID" runat="server" Text='<%#Eval("HO_ID")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  HeaderStyle-ForeColor="white" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField  HeaderStyle-ForeColor="white" HeaderText="User Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UsrName" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField  HeaderStyle-ForeColor="white" HeaderText="Password" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Pwd" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="center" DataNavigateUrlFormatString="Sub_HO_ID_Creation.aspx?HO_ID={0} & division_code={0}"
                                DataNavigateUrlFields="HO_ID">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                        <asp:TemplateField HeaderText="Deactivate" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("HO_ID") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate');">Deactivate
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                   
                      </Columns>
                         <EmptyDataRowStyle ForeColor ="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderWidth="2" Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
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
