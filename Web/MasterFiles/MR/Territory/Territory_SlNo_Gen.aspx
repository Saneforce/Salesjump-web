<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_SlNo_Gen.aspx.cs" Inherits="MasterFiles_Territory_SlNo_Gen" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Territory Serial No Generation</title>    
      <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
         <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
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
           .marRight
        {
            margin-right:35px;
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
    <div id="Divid" runat="server">
        </div>
     <%--<ucl:Menu ID="menu1" runat="server" /> --%>
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <table width="90%">
      
        <tr> 
          <td align="right" width="30%">
                 <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" SkinID="lblMand" Visible="true"></asp:Label>--%>
                </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>                    
     </tr>
     </table>
  
      <table align="center" width="100%">            
            <tr>
             <td colspan="2" align="center">
        <asp:GridView ID="grdTerr" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                            GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" AllowSorting="true" onsorting="grdTerr_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>                              
                                  <asp:TemplateField HeaderText="Territory Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="Route_Code" ShowHeader="true" ItemStyle-HorizontalAlign="Left" 
                                    HeaderText="Route Code" ItemStyle-Width="15%" HeaderStyle-ForeColor="White" />
                                <asp:BoundField DataField="Territory_Name" ShowHeader="true" ItemStyle-HorizontalAlign="Left" SortExpression="Territory_Name" 
                                    HeaderText="Name" ItemStyle-Width="30%" HeaderStyle-ForeColor="White" />
                                

                                 <asp:TemplateField HeaderText="Existing S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" MaxLength="3" runat="server" Width="50%" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" Width="80%"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />                        
                        </asp:GridView>
        </td>
        </tr>
        </table>
       
        <br />
         <center>
            <asp:Button ID="btnSubmit" runat="server" Text="Generate - Sl No" CssClass="BUTTON" Width="110px" Height="25px"
                onclick="btnSubmit_Click" />&nbsp;
        
            <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" CssClass="BUTTON"
                onclick="btnClear_Click" />
        </center>
    </div>
    <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
