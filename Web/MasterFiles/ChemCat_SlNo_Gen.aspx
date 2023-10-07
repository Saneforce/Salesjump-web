<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemCat_SlNo_Gen.aspx.cs" Inherits="MasterFiles_ChemCat_SlNo_Gen" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemist Category Serial No Generation</title>
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
      <br />
        <table width="100%" align="center" >
          <tbody>
            <tr>
              <td colspan="2" align="center">
                    <asp:GridView ID="grdChemCat" runat="server" Width="50%" HorizontalAlign="Center" EmptyDataText ="No Records Found" 
                            AutoGenerateColumns="false" AllowSorting="true" OnSorting="grdChemCat_Sorting" 
                            GridLines="None" CssClass ="mGrid"  AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                            <Columns >
                               <asp:TemplateField HeaderText="Chem_Cat_Code" Visible="false">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Chem_Cat_Code" runat="server" Text='<%#   Bind("Cat_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Chem_Cat_SName" SortExpression="Chem_Cat_SName" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white" ShowHeader="true" HeaderText="Short Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Chem_Cat_Name" SortExpression="Chem_Cat_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white" ShowHeader="true" HeaderText="Category Name"  ItemStyle-Width="20%"/>

                                <asp:TemplateField HeaderText="Existing S.No" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSlNo" runat="server" onkeypress="CheckNumeric(event);"  MaxLength="3" Width="50%" SkinID="MandTxtBox"></asp:TextBox>
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
            <asp:Button ID="btnSubmit" runat="server" Width="110px" Height="25px" Text="Generate - Sl No" CssClass="BUTTON"
                onclick="btnSubmit_Click" />
        
            <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" CssClass="BUTTON"
                onclick="btnClear_Click" />
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
