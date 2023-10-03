<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Audit_Team_Selection.aspx.cs" Inherits="MasterFiles_Audit_Team_Selection" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Audit Team Selection</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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
    <form id="form2" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" /> 
      <br />
     <center>
      <br />     
    <asp:UpdatePanel ID="Upl" runat="server">
    <ContentTemplate>
      <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false" GridLines="None"
                            OnRowDataBound="grdSalesForce_RowDataBound" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" 
                           >
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" Visible ="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chksf" runat="server"  AutoPostBack="true" oncheckedchanged="chksf_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sf Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfCode" runat="server" Text='<%#Eval("sf_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Field Force"
                                    HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSfName" runat="server" Text='<%#Eval("sf_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="HQ"
                                    HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Reporting Manager" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBackColor" runat="server" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFType" runat="server" Text='<%# Bind("sf_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel> 
</center>
    </div>
    <div class="div_fixed">
        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Update" Visible = "false" onclick="btnSubmit_Click"/>
    </div>
     <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
