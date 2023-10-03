﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail_Folder_Creation.aspx.cs" Inherits="MasterFiles_Options_Mail_Folder_Creation" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mail Folder Creation</title>
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
         td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
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

   <%-- <script type="text/javascript">
           function GetGridFooterRowvalues() {
               var fuid = document.getElementById('<%=((TextBox)grdmail.FooterRow.FindControl("txt_Name")).ClientID %>');
               if (fuid.value == '') {
               alert('Please Enter Mail Folder Name')
               document.getElementById('<%=((TextBox)grdmail.FooterRow.FindControl("txt_Name")).ClientID %>').focus();
               return false;

         }
      }
</script>--%>

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <ucl:Menu ID="menu1" runat="server" />
       <br />
       <br />
       <br />
        <table width="78%">
           <tr align="center" >
              <td>
                 <asp:Button ID="btnTransfer_mail" runat="server" CssClass="BUTTON"  Width="90px" Height="25px" Text="Transfer Mail" onClick="btnTransfer_mail_Click" />
              </td>
            </tr>
        </table>
        <br />
        
         <center >
            <table width="100%" align="center">
            
              <tr >
                  <td colspan="2" align="center">
                        <asp:GridView ID="grdmail" runat="server" Width="20%" HorizontalAlign="Center" OnRowCreated ="grdmail_RowCreated" 
                        AutoGenerateColumns="false"  GridLines="None" OnPageIndexChanging="grdmail_PageIndexChanging" ShowFooter="True" onrowcommand="grdmail_RowCommand" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" OnSelectedIndexChanging="grdmail_SelectedIndexChanging">
                        <HeaderStyle Font-Bold="false" />
                        <PagerStyle CssClass ="pgr" />
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt" />
                        
                           <Columns>

                           <asp:TemplateField HeaderText="More" HeaderStyle-Width="40px">
                             <FooterTemplate>
                             <asp:Button ID="btnadd" runat="server" CssClass="BUTTON" CausesValidation ="true"  Width="90px" Height="25px" CommandName ="Select" Text="Add Folder" OnClientClick="GetGridFooterRowvalues()" />
                             <%--<asp:LinkButton ID="LkB1" runat="server" CommandName="Select">Add Folder</asp:LinkButton>--%>
                              </FooterTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                 <ItemTemplate >
                                   <asp:Label ID="lblSNo" runat="server" Text ='<%# (grdmail.PageIndex * grdmail.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>    
                                 </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText ="Move_MailFolder_Id" Visible="false" >
                                 <ItemTemplate >
                                    <asp:Label ID="lblfolder_id" runat="server" Text='<%#Eval("Move_MailFolder_Id")%>'></asp:Label>
                                 </ItemTemplate>
                              
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Mail Folder Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                 <ItemTemplate >
                                   <asp:TextBox ID="txtfolder_name" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" runat="server" SkinID="TxtBxNumOnly" Width="160px" Text='<%# Bind("Move_MailFolder_Name") %>'></asp:TextBox>  
                                 </ItemTemplate>
                                 <FooterTemplate>
                               <asp:TextBox ID="txt_Name" runat="server" SkinID="TxtBxNumOnly" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" Width="160px" align="left"></asp:TextBox>
                             </FooterTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width = "15%" HeaderText="Mail Count" HeaderStyle-ForeColor="white">
                           
                                   <ItemTemplate>
                                   <asp:Label ID="lblmailCount"  runat="server" Text='<%# Bind("mail_count") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>

                               <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                <%--<asp:Button ID="btnDelete" runat ="server" CssClass="BUTTON" Width="50px" Height="20px" CommandArgument='<%# Eval("Move_MailFolder_Id") %>' CommandName ="Delete" Text="Delete" OnClientClick="return confirm('Do you want to Delete the Folder Name');" />--%>
                                    <asp:LinkButton ID="lnkbutDelete" runat="server" CommandArgument='<%# Eval("Move_MailFolder_Id") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Delete the Folder Name');">Delete
                                    </asp:LinkButton>
                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../Images/dele.PNG" alt="" width="40px" height="15px" title="Already Some Mails are in this folder kindly click Transfer Mail" />
                                        </asp:Label>                          
                              </ItemTemplate>
                              </asp:TemplateField>
                           </Columns>
                         <%--<EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />--%>                              
                             
                        </asp:GridView>  
                           <asp:Button ID="btnaddnew" runat="server" CssClass="BUTTON" Width="90px" Height="25px" Text="Add Folder" OnClick="btnaddnew_Click" />&nbsp
                        <asp:TextBox ID="txtaddnew" runat="server" SkinID="TxtBxNumOnly" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" Width="160px"></asp:TextBox>
                  </td>
              </tr>
             
            
            </table>
            <%--<table >
               <tr>
              <td align="center" >
                 <asp:Button ID="btnnew" runat="server" CssClass="BUTTON" Width="70px" 
                      Height="25px" Text ="Add" onclick="btnnew_Click" />
              </td>
              <td align="center"> 
                 <asp:TextBox ID="txtnew" runat="server" SkinID="TxtBxAllowSymb" Width="160px" ></asp:TextBox>
              </td>

              </tr>
            
            </table>--%>
            <br />
            <br />          

              <asp:Button ID="btnUpdate" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Save" OnClick="btnUpdate_Click" />

         
         </center>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
