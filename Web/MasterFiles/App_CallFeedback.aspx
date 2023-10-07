<%@ Page Title="Call Feedback Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="App_CallFeedback.aspx.cs" Inherits="MasterFiles_App_CallFeedback" %>
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Call Feedback Creation</title>
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

  <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //  $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnaddnew').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#<%=btnaddnew.ClientID%>').click(function () {
                if ($('#<%=txtaddnew.ClientID%>').val() == "") { alert("Enter Feedback."); $('#<%=txtaddnew.ClientID%>').focus(); return false; }
               
            });
        });
    </script>

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%--<ucl:Menu ID="menu1" runat="server" />--%>
       <br />
       <br />
       <br />
     <center >
            <table width="100%" align="center">
            
              <tr >
                  <td colspan="2" align="center">
                        <asp:GridView ID="grdFeedback" runat="server" Width="20%" HorizontalAlign="Center" OnRowCreated ="grdFeedback_RowCreated" 
                        AutoGenerateColumns="false"  GridLines="None" OnPageIndexChanging="grdFeedback_PageIndexChanging" ShowFooter="True" onrowcommand="grdFeedback_RowCommand" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr" OnSelectedIndexChanging="grdFeedback_SelectedIndexChanging">
                        <HeaderStyle Font-Bold="false" />
                        <PagerStyle CssClass ="pgr" />
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt" />

                           <Columns>

                           <asp:TemplateField HeaderText="More" HeaderStyle-Width="40px">
                             <FooterTemplate>
                             <asp:Button ID="btnadd" runat="server" CssClass="BUTTON" CausesValidation ="true"  Width="90px" Height="25px" CommandName ="Select" Text="Add Feedback" OnClientClick="GetGridFooterRowvalues()" />
                             <%--<asp:LinkButton ID="LkB1" runat="server" CommandName="Select">Add Folder</asp:LinkButton>--%>
                              </FooterTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                 <ItemTemplate >
                                   <asp:Label ID="lblSNo" runat="server" Text ='<%# (grdFeedback.PageIndex * grdFeedback.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>    
                                 </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText ="Feedback_Id" Visible="false" >
                                 <ItemTemplate >
                                    <asp:Label ID="lblfeedback_id" runat="server" Text='<%#Eval("Feedback_Id")%>'></asp:Label>
                                 </ItemTemplate>
                              
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                 <ItemTemplate >
                                   <asp:TextBox ID="txtfeedback_name" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" runat="server" SkinID="TxtBxNumOnly" Width="160px" Text='<%# Bind("Feedback_Content") %>'></asp:TextBox>  
                                 </ItemTemplate>
                                 <FooterTemplate>
                               <asp:TextBox ID="txt_Name" runat="server" SkinID="TxtBxNumOnly" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" Width="160px" align="left"></asp:TextBox>
                              </FooterTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Delete" HeaderStyle-ForeColor="White" HeaderStyle-Width="120px">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue"  HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Feedback_Id") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Delete Call Feedback');">Delete
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                           </Columns>
                         <%--<EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />--%>                              
                             
                        </asp:GridView>  
                        <asp:Button ID="btnaddnew" runat="server" CssClass="BUTTON" Width="90px" Height="25px" Text="Add Feedback" OnClick="btnaddnew_Click" />&nbsp
                        <asp:TextBox ID="txtaddnew" runat="server" SkinID="TxtBxNumOnly" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" Width="160px"></asp:TextBox>
                  </td>
              </tr>
             
            
            </table>
           
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
</asp:Content>