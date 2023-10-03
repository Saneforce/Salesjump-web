<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delayed_Status.aspx.cs" Inherits="Delayed_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delayed Status</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
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
 
    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
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
        <br />
        <center>
            <table>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Delayed Status for the month of " Font-Underline="True"
                            Font-Bold="True" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <table cellpadding="6" cellspacing="6" >
                <tr>
                    <td align="left">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="160px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
              <tr style="height:25px;">
                <td align="left" >
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>                    
                 </td>
                 <td align="left" > 
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
                    <tr>
                        <td align="left" >
                            <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                </tr>
            </table>
            <center>
                <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" />
            </center>
            <br />
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdDelayed" runat="server" Width="100%" HorizontalAlign="Center"
                BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                CssClass="mGrid">
                <HeaderStyle Font-Bold="False" />
                <SelectedRowStyle BackColor="BurlyWood" />
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <HeaderStyle BorderWidth="1" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="250px" />
                        <ItemTemplate>
                            <asp:Label ID="lblFieldForce" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="120px" />
                        <ItemTemplate>
                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="130px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPlace" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTourPlan" runat="server" Font-Size="9pt" Text='<%#Eval("tour_date")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    Width="80%" VerticalAlign="Middle" />
            </asp:GridView>
       
        <asp:Table ID="tbl" CellSpacing="2" CellPadding="2" runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>
                             </center>
    </div>
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
