<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hospital_View.aspx.cs" Inherits="MasterFiles_MR_Hospital_Hospital_View" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital View</title>
     <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
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
        <div id="Divid" runat="server"></div> 
          <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
          <table id="Table1" runat="server" width="90%">
       
            <tr>
                 <td align="right" width="30%">
                  <%--  <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
            </tr>
              <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>
            </tr>
            </table>  
            <br />     
        <center>

        <table border="1" >
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblName" SkinID="lblMand" runat="server" Text="Hospital Name"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtName" runat="server" Width="200px"  Enabled="False" Text='<%#Eval("Chemists_Name")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
               <td align="left" class="stylespc">
                    <asp:Label ID="lblAddress" SkinID="lblMand" runat="server" Text="Hospital Address"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtAddress" runat="server" Width="200px"  Enabled="False" Text='<%#Eval("Chemists_Address1")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="left" class="stylespc">
                    <asp:Label ID="lblContact" SkinID="lblMand" runat="server" Text="Contact"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtContact" runat="server" Width="160px"  Enabled="false" Text='<%#Eval("Chemists_Contact")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="left" class="stylespc">
                    <asp:Label ID="lblPhone" SkinID="lblMand" runat="server" Text="Hospital Phone"></asp:Label>
                </td>
                 <td align="left" class="stylespc">
                    <asp:TextBox ID="txtPhone" runat="server" Width="160px"  Enabled="false" Text='<%#Eval("Chemists_Phone")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblTerritory" SkinID="lblMand" runat="server" Text="Territory"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:TextBox ID="txtTerritory" runat="server"   Enabled="false" Text='<%#Eval("Territory_Name")%>'></asp:TextBox>
                </td>
            </tr>
         </table> 
         
         </center>     
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>    
    </div>
    </form>
</body>
</html>
