<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEdit_Chemists.aspx.cs" Inherits="MasterFiles_MR_Chemist_BulkEdit_Chemists" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Chemist</title>
     <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
   <script type="text/javascript" language="javascript">
function checkEmail() {
    var email = document.getElementById('Chemists_EMail');
var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
if (!filter.test(email.value)) {
alert('Please provide a valid email address');
email.focus;
return false;
}
}
</script>
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
      <div id="Divid" runat="server">
        </div>
    <div>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>
         <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        
          <table id="Table1" runat="server" width="90%">
       
            <tr>
                 <td align="right" width="30%">
                 <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
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
                <asp:Label ID="lblTitle" runat="server" Width="180px" Text="Select the Fields to Edit"
                    TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                    Font-Size="Small" ForeColor="BlueViolet">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" >
               
                <asp:CheckBoxList ID="CblChemistsCode" CssClass="Checkbox" runat="server"  style="margin-left:250px"
                    RepeatColumns="4" RepeatDirection="Horizontal" Width="600px">
                        <asp:ListItem Value="Chemists_Name">&nbsp;Chemists Name</asp:ListItem>  
                        <asp:ListItem Value="Chemists_Contact">&nbsp;Contact Person</asp:ListItem>                      
                        <asp:ListItem Value="Chemists_Address1">&nbsp;Address</asp:ListItem>
                        <asp:ListItem Value="Chemists_Phone">&nbsp;Phone</asp:ListItem>
                        <asp:ListItem Value="Chemists_Fax">&nbsp;Fax</asp:ListItem>                        
                        <asp:ListItem Value="Chemists_EMail">&nbsp;EMail ID</asp:ListItem>
                        <asp:ListItem Value="Chemists_Mobile">&nbsp;Mobile No</asp:ListItem>                        
                      <%--  <asp:ListItem Value="Territory_Code">&nbsp;Territory</asp:ListItem>--%>
                </asp:CheckBoxList>
            </td>
        </tr>
     </table>
      <br />
        <table>            
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" CssClass="BUTTON" Text="Ok" Width="35px" Height="25px" 
                        onclick="btnOk_Click" />
                   
                    <asp:Button ID="btnClr" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" 
                        onclick="btnClr_Click" />
                </td>
            </tr>
        </table>
        <br />
    <center>
        <table runat="server" id="tblDoctor" visible="false" width="80%" align="center" style="margin-left:200px">
            <tr>
                <td>
                    <asp:GridView ID="grdChemists" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" EmptyDataText="No Records Found" onrowdatabound="grdChemist_RowDataBound"  
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />                        
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chemists_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Chemists_Code" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                 <ItemStyle Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Chemists_Name" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Chemists_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">                              
                                <ItemTemplate>
                                    <asp:TextBox ID="Chemists_Name" Width="200px" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Chemists_Name") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Chemists_Contact" runat="server" Width="200px" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Chemists_Contact") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="160px" />
                                <ItemTemplate>                                
                                    <asp:TextBox ID="Chemists_Address1" runat="server" onkeypress="AlphaNumeric(event);" SkinID="TxtBxAllowSymb" Width="200px" MaxLength="150" Text='<%# Bind("Chemists_Address1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Chemists_Phone" runat="server" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Chemists_Phone") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fax" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Chemists_Fax" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Chemists_Fax") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMail ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Chemists_EMail" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Chemists_EMail") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Chemists_Mobile" onkeypress="CheckNumeric(event);" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Chemists_Mobile") %>'></asp:TextBox>
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
                       <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
             </table>
             <br />
               <table align="center">
            <tr>
                <td align="center">
                    <asp:Button ID="btnUpdate" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Update" Visible="false"
                        onclick="btnUpdate_Click"  />
                </td>
            </tr>
        </table>
        </center>
        </center>                         
    </div>    
    <div class="div_fixed">
         <asp:Button ID="btnSave" runat="server" Text="Update" Width="70px" Height="25px" CssClass="BUTTON" Visible="false"
            onclick="btnSave_Click" />
    </div>    
     <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
