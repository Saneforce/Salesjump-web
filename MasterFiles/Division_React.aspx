<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Division_React.aspx.cs" Inherits="MasterFiles_Division_React" %>
<%@ Register Src ="~/UserControl/pnlMenu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Division - Reactivation</title>
 <link type="text/css" rel="stylesheet" href="../css/style.css" />
   <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
<%--    <script type="text/javascript">

  window.onload=function(){
      document.getElementById('btnNew').focus();
  }

    </script>--%>
      <style type="text/css">
        #tblDivisionDtls
        {
            margin-left: 300px;
        }
        #tblLocationDtls
        {
            margin-left: 300px;
        }
        .style2
        {
            width: 92px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
         .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: gray;
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
    <ucl:Menu ID ="menu1" runat ="server" />
    <br />
    <table width="100%" align="center" >
       <tbody >
         <tr>
           <td colspan ="2" align="center"> 
              <asp:GridView ID ="grdDivision" runat ="server" Width="85%" HorizontalAlign ="Center" 
              AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="10" EmptyDataText ="No Records Found"
               OnRowCommand ="grdDivision_RowCommand" OnPageIndexChanging ="grdDivision_PageIndexChanging" 
               GridLines ="None" CssClass ="mGrid" PagerStyle-CssClass ="pgr" AlternatingRowStyle-CssClass ="alt" 
               AllowSorting="true" >
               <HeaderStyle Font-Bold ="false" />
               <PagerStyle CssClass ="pgr" />
               <SelectedRowStyle BackColor ="BurlyWood" />
               <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                   <Columns>
                        <asp:TemplateField HeaderText ="S.No" HeaderStyle-ForeColor="white" >
                          <ItemTemplate >
                              <asp:Label ID ="lblSNo" runat ="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText ="Div_Code" Visible="false">
                          <ItemTemplate >
                            <asp:Label ID ="lblDivCode" runat="server" Text='<%#Eval("Division_Code")%>'></asp:Label> 
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor ="White" HeaderText ="Division Name" ItemStyle-HorizontalAlign ="Left">
                          <%--<EditItemTemplate> 
                            <asp:TextBox ID="txtDiv" runat="server" SkinID="TxtBxAllowSymb" MaxLength="100" Text='<%# Bind("Division_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                         </EditItemTemplate>--%>
                          <ItemTemplate >
                           <asp:Label ID="lblDiv" runat ="server"  Text='<%# Bind("Division_Name") %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText ="Alias Name" ItemStyle-HorizontalAlign ="Left" >
                         <%--<EditItemTemplate>
                           <asp:TextBox ID="txtAlName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="8" Text='<%# Bind("Alias_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                         </EditItemTemplate>--%>
                         <ItemTemplate >
                          <asp:Label ID="lblAlName" runat="server" Text='<%# Bind("Alias_Name") %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText ="City" ItemStyle-HorizontalAlign="Left" >
                          <%--<EditItemTemplate >
                            <asp:TextBox ID ="txtCity" runat="server" SkinID="TxtBxChrOnly" MaxLength="20" Text='<%# Bind("Division_City") %>' onkeypress="CharactersOnly(event);"></asp:TextBox> 
                          </EditItemTemplate>--%>
                          <ItemTemplate >
                           <asp:Label ID ="lblCity" runat="server" Text='<%# Bind("Division_City") %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText ="Reactivate" HeaderStyle-ForeColor="white">
                          <ControlStyle ForeColor ="DarkBlue" Font-Size ="XX-Small" Font-Names ="Verdana" Font-Bold ="true" />
                          <ItemStyle ForeColor="DarkBlue" Font-Bold ="false" />
                          <ItemTemplate >
                          <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Division_Code") %>'
                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Division');">Reactivate
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
