<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reactivate_Gifts.aspx.cs" Inherits="MasterFiles_Reactivate_Gifts" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reactivate Inputs</title>
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
   <script type="text/javascript">
       $(document).ready(function () {
           //   $('input:text:first').focus();
           $('input:text').bind("keydown", function (e) {
               var n = $("input:text").length;
               if (e.which == 13) { //Enter key
                   e.preventDefault(); //to skip default behavior of the enter key
                   var curIndex = $('input:text').index(this);
                   if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
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
                           $('#btnSubmit').focus();
                       }
                   }
               }
           });
           $("input:text").on("keypress", function (e) {
               if (e.which === 32 && !this.value.length)
                   e.preventDefault();
           });
           $('#Btnsrc').click(function () {

               var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
               var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
               if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
               if ($("#txtsearch").val() == "") { alert("Enter Gift Name."); $('#txtsearch').focus(); return false; }

           });
       }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
      <%--  <table width="90%">
            <tr>
                <td style="width: 8.4%" />
                <td align="left">
                    <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Text="Add" OnClick="btnNew_Click" />
                </td>
                <td>
                    <asp:Button ID="btnView" runat="server" CssClass="BUTTON" Text="View" OnClick="btnView_Click" />
                </td>
                 <td>
                    <asp:Button ID="btnReactivate" runat="server" CssClass="BUTTON" Text="Reactivate Gifts" OnClick="btnReactivate_Click" />
                </td>
                <td style="width: 50%" />
                <td align="right">
                    <asp:Label ID="lblEffDt" runat="server" SkinID="lblMand" Text="Effective From Date"></asp:Label>
                </td>
                <td align="right">
                    <asp:DropDownList ID="Ddl_EffDate" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                        OnSelectedIndexChanged="Ddl_EffDate_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />--%>
        <table width="90%">
            <tr>
                <td style="width: 6.2%" />
                <td width="30%">
                    <%--<asp:Label ID="lblView" runat="server" SkinID="lblMand" Text="Search by"></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                        TabIndex="1">
                        <asp:ListItem Value="1" Text="All"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Gift Name"></asp:ListItem>
                        <asp:ListItem Value="3" Text="State Name"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlProGiftSta" runat="server" AutoPostBack="true" Width="174px"
                        Height="16px" SkinID="ddlRequired" TabIndex="4">
                    </asp:DropDownList>--%>
                      <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By" ></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"    
                        TabIndex="1" onselectedindexchanged= "ddlSrch_SelectedIndexChanged" >                    
                                    <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Gift Name" Value="2" ></asp:ListItem>
                                    <asp:ListItem Text="State" Value="3"></asp:ListItem>                                   
                    </asp:DropDownList>
                    <asp:TextBox id="txtsearch" runat="server" CssClass="TEXTAREA" Visible= "false" ></asp:TextBox> 
                    <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="true"  Visible ="false" onselectedindexchanged= "ddlSrc2_SelectedIndexChanged"  
                                    SkinID="ddlRequired" TabIndex="4">                    
                                </asp:DropDownList>       
                    <asp:Button ID="Btnsrc" runat="server" CssClass="BUTTON" 
                    Text="Go" onclick="Btnsrc_Click" Visible = "false" />
                </td>
           
           <%--     <td width="40%" align="center">
                    <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                        runat="server" Width="55%" HorizontalAlign="center">
                        <SeparatorTemplate>
                        </SeparatorTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAlpha" ForeColor="#8A2EE6" runat="server" CommandArgument='<%#bind("Gift_Name") %>'
                                Text='<%#bind("Gift_Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>--%>
               <td align="right" width="40%">
                    <asp:Label ID="lblEffDt" runat="server" SkinID="lblMand" Text="Effective From Date"></asp:Label>
                
                    <asp:DropDownList ID="Ddl_EffDate" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                        OnSelectedIndexChanged="Ddl_EffDate_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdGift" runat="server" Width="85%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            AllowPaging="True" PageSize="10" OnRowUpdating="grdGift_RowUpdating" OnRowEditing="grdGift_RowEditing"
                            OnPageIndexChanging="grdGift_PageIndexChanging" OnRowCreated="grdGift_RowCreated"
                            OnRowCancelingEdit="grdGift_RowCancelingEdit" OnRowCommand="grdGift_RowCommand" EmptyDataText="No Records Found"
                            OnRowDataBound="grdGift_RowDataBound" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdGift_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdGift.PageIndex * grdGift.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gift_Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftCode" runat="server" Text='<%#Eval("Gift_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Gift_Name" HeaderText="Name" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="GiftName" runat="server" Width="180px" SkinID="TxtBxAllowSymb" MaxLength="150"
                                            Text='<%# Bind("Gift_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftName" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Gift_SName" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <ItemStyle Width="120px" />
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGiftSN" SkinID="TxtBxAllowSymb" runat="server" MaxLength="15"
                                            Text='<%# Bind("Gift_SName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftSN" runat="server" Width="60px" Text='<%# Bind("Gift_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Gift_Type" HeaderText="Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftType" runat="server" Text='<%# Bind("Gift_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGiftType" runat="server" SkinID="ddlRequired" >
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemStyle Width="70px" />
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGiftval" SkinID="TxtBxAllowSymb" Width="60px" runat="server"
                                            MaxLength="5" Text='<%# Bind("Gift_Value") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftVal" runat="server" Text='<%# Bind("Gift_Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="ProductReminder.aspx?Gift_Code={0}"
                                    DataNavigateUrlFields="Gift_Code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                               <%-- <asp:TemplateField HeaderText="Reactivate">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Gift_Code") %>'
                                            CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Product');">Reactivate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
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
