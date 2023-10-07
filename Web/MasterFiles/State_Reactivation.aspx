<%@ Page Language="C#" AutoEventWireup="true" CodeFile="State_Reactivation.aspx.cs"
    Inherits="MasterFiles_State_Reactivation" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State-Location Reactivation</title>
    <style type="text/css">
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
            z-index: 999;</style>
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
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <center>
        <br />
         <asp:GridView ID="grdState" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found" 
                    OnRowCommand = "grdState_RowCommand" 
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                    AlternatingRowStyle-CssClass="alt" AllowSorting="True" >
            <SelectedRowStyle BackColor="BurlyWood" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State_Code" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("ShortName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reactivate" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                    </ControlStyle>
                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("State_Code") %>'
                            CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the State');">Reactivate
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
              <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:GridView>
        </center>
    </div>
    </form>
</body>
</html>
