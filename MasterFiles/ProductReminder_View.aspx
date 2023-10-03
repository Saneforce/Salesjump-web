<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductReminder_View.aspx.cs"
    Inherits="MasterFiles_ProductReminder_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Details View</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
     <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
                            $('btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {
                var st = $('#<%=ddlState.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select State."); $('ddlState').focus(); return false; }
                var FromYear = $('#<%=ddlFromYear.ClientID%> :selected').text();
                if (FromYear == "---Select---") { alert("Select From Year."); $('#ddlFromYear').focus(); return false; }
                var ToYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (ToYear == "---Select---") { alert("Select To Year."); $('#ddlToYear').focus(); return false; }

            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
       
        <center>
            <table id="tblview" runat="server" cellpadding="5" cellspacing="5">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblState" runat="server" SkinID="lblMand" Width="100px"><span style="color:red">*</span>State Name</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlState" runat="server"  SkinID="ddlRequired"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblEff_From" runat="server" SkinID="lblMand" 
                            Width="100px"><span style="color:red">*</span>Effective From</asp:Label>
                    </td>
                    <td align="left">
                       <asp:DropDownList ID="ddlFromYear" runat="server" SkinID="ddlRequired"
                            AutoPostBack="true">
               </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblEff_To" runat="server" SkinID="lblMand" Width="100px"><span style="color:red">*</span>Effective To</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlToYear" runat="server" SkinID="ddlRequired"
                            AutoPostBack="true">
               </asp:DropDownList>
                    </td>
                </tr>                
            </table>
        </center>
        <br />
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Save" OnClick="btnSubmit_Onclick" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
             <table id="tblState" runat="server" width="100%" align="center" visible="false">
            <tr style="height:25px;">               
             
                <td align="center">
                    <asp:Label ID="lblSt" runat="server" Text="State Name: "></asp:Label>                    
                    <asp:Label ID="lblStatename" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr> 
        </table>      
             <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdGift" runat="server" Width="85%" HorizontalAlign="Center" AutoGenerateColumns="false"
                             OnRowDataBound="grdGift_RowDataBound" EmptyDataText="No Records Found"             
                           GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gift_Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftCode" runat="server" Text='<%#Eval("Gift_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-ForeColor="white"
                                    ItemStyle-HorizontalAlign="Left">                             
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftName" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="120px" />                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftSN" runat="server" Width="60px" Text='<%# Bind("Gift_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftType" runat="server" Text='<%# Bind("Gift_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGiftType" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Gift Value">
                                    <ItemStyle Width="70px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGiftVal" runat="server" Text='<%# Bind("Gift_Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                  <asp:TemplateField HeaderText="Effective From" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="140px" />                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrom" runat="server" Text='<%# Bind("Gift_Effective_From") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective To" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="140px" />                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblTo" runat="server" Text='<%# Bind("Gift_Effective_To") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
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
