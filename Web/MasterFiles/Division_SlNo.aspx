<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Division_SlNo.aspx.cs" Inherits="MasterFiles_Division_SlNo" %>
<%@ Register Src ="~/UserControl/pnlMenu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Divisionwise Serial No - Generation</title>
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
<body >

    <form id="form1" runat="server">

    <div>
    <ucl:Menu ID="menu1" runat="server" /> 

      <%--<table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td class="style3">
                <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Text="Add" Width="60px" Height="25px" onClick="btnNew_Click" />
            </td>
            <td></td>
        </tr>
      </table>--%>
      <br />
       <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                     <asp:GridView ID="grdDivision" runat="server" Width="55%" HorizontalAlign="Center" EmptyDataText="No Records Found"  
                            AutoGenerateColumns="false" AllowSorting="True" OnSorting="grdDivision_Sorting"
                            GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                         
                            <asp:TemplateField HeaderText="Div_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDivCode" runat="server" Text='<%#Eval("Division_Code")%>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Division_Name" ItemStyle-Width="20%" HeaderStyle-ForeColor="white" HeaderText="Division Name" ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:TextBox ID="txtDiv" runat="server"  SkinID="TxtBxAllowSymb" MaxLength="100" Text='<%# Bind("Division_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDiv" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                </ItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Alias_Name" ItemStyle-Width="10%" HeaderStyle-ForeColor="white"  HeaderText="Alias Name" ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAlName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="8" Text='<%# Bind("Alias_Name") %>' onkeypress="CharactersOnly(event);"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAlName" runat="server" Text='<%# Bind("Alias_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Existing S.No"  HeaderStyle-ForeColor="white" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="3" Width="50%" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
    <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" Text="Generate - Sl No" Width="110px" Height="25px" CssClass="BUTTON" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" CssClass="BUTTON" OnClick="btnClear_Click" />
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
