<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEdit_Hospital.aspx.cs" Inherits="MasterFiles_MR_Hospital_BulkEdit_Hospital" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Hospital</title>
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
    <div id="Divid" runat="server"></div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
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
            
        <center>
        <table border="0" cellpadding="3" cellspacing="3" id="tblLocationDtls" align="center" width ="80%">
        <tr>
            <td rowspan="" align="center">
                <asp:Label ID="lblTitle" runat="server" Width="210px" Text="Select the Fields to Edit"
                    TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                    Font-Size="Small" ForeColor="BlueViolet">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" >
                
                <asp:CheckBoxList ID="CblHospitalCode" CssClass="Checkbox" runat="server" style="margin-left:300px" 
                    RepeatColumns="4" RepeatDirection="Horizontal" Width="500px">
                        <asp:ListItem Value="Hospital_Name">&nbsp;Hospital Name</asp:ListItem>  
                        <asp:ListItem Value="Hospital_Contact">&nbsp;Contact Person</asp:ListItem>                      
                        <asp:ListItem Value="Hospital_Address1">&nbsp;Address</asp:ListItem>
                        <asp:ListItem Value="Hospital_Phone">&nbsp;Phone</asp:ListItem>
                        <asp:ListItem Value="Hospital_Fax">&nbsp;Fax</asp:ListItem>                        
                        <asp:ListItem Value="Hospital_EMail">&nbsp;EMail ID</asp:ListItem>
                        <asp:ListItem Value="Hospital_Mobile">&nbsp;Mobile No</asp:ListItem>                        
                       <%-- <asp:ListItem Value="Territory_Code">&nbsp;Territory</asp:ListItem>--%>
                </asp:CheckBoxList>
            </td>
        </tr>
     </table>
      <br />
        <table>            
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" CssClass="BUTTON" Text="Ok" Width="30px" Height="25px" 
                        onclick="btnOk_Click" />
                 
                    <asp:Button ID="btnClr" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" 
                        onclick="btnClr_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table runat="server" id="tblDoctor" visible="false">
            <tr>
                <td>
                    <asp:GridView ID="grdHospital" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"   OnRowDataBound="grdHospital_RowDataBound" 
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />                        
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hospital_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Hospital_Code" runat="server" Text='<%#Eval("Hospital_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hospital Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">                           
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Hospital_Name" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Hospital_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hospital Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                 <ItemStyle Width="170px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);"
 runat="server" Width="170px" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_Name") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_Contact" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);"
 SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_Contact") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="160px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_Address1" runat="server" onkeypress="AlphaNumeric(event);" Width="160px" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_Address1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_Phone" runat="server" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_Phone") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fax" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_Fax" runat="server" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_Fax") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMail ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_EMail" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_EMail") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Hospital_Mobile" runat="server" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Hospital_Mobile") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Territory_Code" runat="server" SkinID="ddlRequired" DataSource ="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
            <td><br /></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnUpdate" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Update" 
                        onclick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
        </center>                     
    
    </div>
    <div class="div_fixed">
         <asp:Button ID="btnSave" runat="server" Text="Update" Width="70px" Height="25px" CssClass="BUTTON"
            onclick="btnSave_Click" Visible="false" />
    </div>    
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
