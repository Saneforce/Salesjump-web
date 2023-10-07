<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorList_Reportaspx.aspx.cs"
    Inherits="Reports_DoctorList_Reportaspx" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Details - View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
        $(document).ready(function () {

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
            $('#btnSubmit').click(function () {

                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }

            });
        }); 
    </script>
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
    <style type="text/css">
        #tblDocRpt
        {
        }
        #tbl
        {
            border-collapse: collapse;
        }
        table, td, th
        {
            border: 1px solid black;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <center>
            <br />
            <table cellpadding="3" cellspacing="3">
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblView" runat="server" Text="View By" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:RadioButtonList ID="rdoMGRState" Font-Names="Verdana" Font-Size="11px" runat="server"
                            RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoMGRState_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="FieldForce-wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="State-wise"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblType" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="rdoType" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="-1" Text="--Select--" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Category"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Speciality"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                           <%-- <asp:ListItem Value="3" Text="Qualification"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                CssClass="btnnew" OnClick="btnSubmit_Click" />
            <br />
            <br />
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="95%">
            </asp:Table>
            <asp:GridView ID="GrdDoctor" runat="server" Width="45%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  PageSize="10" EmptyDataText="No Records Found"
                        GridLines="None" CssClass="mGrid" 
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </center>
    </div>
    </form>
</body>
</html>
