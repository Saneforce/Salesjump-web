<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdCatgMap.aspx.cs" Inherits="MasterFiles_ProdCatgMap" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Category Map</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
      <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
        <br /><br />
        <table align="center" width="100%">
            <tr>
                <td width="35%"></td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Product Category" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCat" runat="server" SkinID="ddlRequired" EnableViewState="true"  AutoPostBack="true"
                                    CssClass="DropDownList" onselectedindexchanged="ddlCat_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                                      
                    </table>                     
                </td>
            </tr>
        </table> 
        <br />
        <center>
           <%-- <hr width="80%" />--%>
            <br />
              <table border="0" cellpadding="0" cellspacing="0" align="center" id="tblProduct" runat="server" visible="false">
            <tr>
                <td >
                    <asp:TextBox ID="txtTitle_ProductDtls" runat="server" Width="210px" Text="Product List"
                        TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="X-Small" ForeColor="DarkGreen">
                    </asp:TextBox>
                </td>
             </tr>
                 <tr>
                    <td>
                        <asp:CheckBoxList ID="chkProduct" runat="server" 
                            Font-Names="Verdana" Font-Size="X-Small" RepeatColumns="2" RepeatDirection="Horizontal"
                            Width="753px">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Save" onclick="btnSave_Click" 
                             />
                    </td>
                </tr>
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
