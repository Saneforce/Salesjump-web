<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChemistCreation.aspx.cs" Inherits="MasterFiles_MR_Chemist_ChemistCreation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chemist Creation</title>
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
     <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
   <%-- <script type="text/javascript">
        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= grdChemist.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                for (i = 2; i < Inputs.length; i++) {
                    if (Inputs[i].type == 'text') {
                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }
                        var chemistName = document.getElementById('grdChemist_ctl' + index + '_Chemists_Name');

                        var Address = document.getElementById('grdChemist_ctl' + index + '_Chemists_Address1');
                        var Contact = document.getElementById('grdChemist_ctl' + index + '_Chemists_Contact');
                        var phoneNo = document.getElementById('grdChemist_ctl' + index + '_Chemists_Phone');
                        var territory = document.getElementById('grdChemist_ctl' + index + '_ddlTerr');
                        if (chemistName.value != '' || Address.value != '' || Contact.value != '' || phoneNo.value != '' || territory.value != '0') {
                            isEmpty = true;
                        }
                        if (chemistName.value != '') {
                            if (Address.value == '') {
                                alert('Enter Address');
                                Address.focus();
                                return false;
                            }
                            if (Contact.value == '') {
                                alert('Enter Contact Name')
                                Contact.focus();
                                return false;
                            }
                            if (phoneNo.value == '') {
                                alert('Enter Phone no')
                                phoneNo.focus();
                                return false;
                            }
                            if (territory.value == '0') {
                                alert('Select territory')
                                territory.focus();
                                return false;
                            }
                        }
                        if (Address.value != '') {
                            if (chemistName.value == '') {
                                alert('Enter Name')
                                chemistName.focus();
                                return false;
                            }
                            if (Contact.value == '') {
                                alert('Enter Contact Name')
                                Contact.focus();
                                return false;
                            }
                            if (phoneNo.value == '') {
                                alert('Enter Phone no')
                                phoneNo.focus();
                                return false;
                            }
                            if (territory.value == '0') {
                                alert('Select territory')
                                territory.focus();
                                return false;
                            }
                        }
                        if (Contact.value != '') {
                            if (chemistName.value == '') {
                                alert('Enter Name')
                                chemistName.focus();
                                return false;
                            }
                            if (Address.value == '') {
                                alert('Enter Address')
                                Address.focus();
                                return false;
                            }
                            if (phoneNo.value == '') {
                                alert('Enter Phone no')
                                phoneNo.focus();
                                return false;
                            }
                            if (territory.value == '0') {
                                alert('Select territory')
                                territory.focus();
                                return false;
                            }
                        }
                        if (phoneNo.value != '') {
                            if (chemistName.value == '') {
                                alert('Enter Name')
                                chemistName.focus();
                                return false;
                            }
                            if (Contact.value == '') {
                                alert('Enter Contact Name')
                                Contact.focus();
                                return false;
                            }
                            if (Address.value == '') {
                                alert('Enter Address')
                                Address.focus();
                                return false;
                            }
                            if (territory.value == '0') {
                                alert('Select territory')
                                territory.focus();
                                return false;
                            }
                        }
                        if (territory.value != '0') {
                            if (chemistName.value == '') {
                                alert('Enter Name')
                                chemistName.focus();
                                return false;
                            }
                            if (Contact.value == '') {
                                alert('Enter Contact Name')
                                Contact.focus();
                                return false;
                            }
                            if (Address.value == '') {
                                alert('Enter Address')
                                Address.focus();
                                return false;
                            }
                            if (phoneNo.value == '') {
                                alert('Enter Phone no')
                                phoneNo.focus();
                                return false;
                            }
                        }

                    }
                }
                    if (isEmpty) {
                        alert('Enter any one Entry')
                        return true;
                    }
                    else
                        
                        return true;
                
               
            }
        }
</script>--%>

 <script type="text/javascript">
     function ValidateEmptyValue() {
         var grid = document.getElementById('<%= grdChemist.ClientID %>');
         if (grid != null) {

             var Inputs = grid.getElementsByTagName("input");
             var cnt = 0;
             var index = '';
             var isEntry = false;
             for (i = 2; i < Inputs.length; i++) {
                 if (Inputs[i].type == 'text') {
                     if (i.toString().length == 1) {
                         index = cnt.toString() + i.toString();
                     }
                     else {
                         index = i.toString();
                     }
                     var isEmpty = false;
                     var chemistName = document.getElementById('grdChemist_ctl' + index + '_Chemists_Name');
                     var territory = document.getElementById('grdChemist_ctl' + index + '_ddlTerr');
                     if (chemistName.value != '' && territory.value != '0') {
                         isEntry = true;
                     }
                     if (chemistName.value == '' && territory.value == '0') {
                         isEmpty = true;
                     }
                     if ((isEntry == false) || (isEmpty == false)) {
                         if (chemistName.value == '') {
                             alert('Enter Name');
                             chemistName.focus();
                             return false;
                         }                       
                         else if (territory.value == '0') {
                             alert('Select territory')
                             territory.focus();
                             return false;
                         }
                     }
                 }
             }

         }
         if (isEntry) {
             return true;
         }
     }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="Divid" runat="server">
        </div>   
       <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>       
           <table id="Table1" runat="server" width="90%">
       
            <tr>
                 <td align="right" width="30%">
                   <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
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

        <table runat="server" id="tblChemist" width="80%">
            <tr>
                <td valign="top" style="width:60%;" align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="grdChemist" runat="server" Width="80%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowCreated="grdChemist_RowCreated"
                                    GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                                    onrowdatabound="grdChemist_RowDataBound"  EmptyDataText="No Records Found" 
                                    AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood"/>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>                
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Chemists_Name" runat="server" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="250" Width="250px" Text='<%#Eval("Chemists_Name")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Chemists_Address1" runat="server" SkinID="TxtBxAllowSymb" MaxLength="250" Width="250px" Text='<%#Eval("Chemists_Address1")%>' onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Chemists_Contact" runat="server" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="250" Width="180px" Text='<%#Eval("Chemists_Contact")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Chemists_Phone" runat="server" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="250" Width="150px" Text='<%#Eval("Chemists_Phone")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCat" runat="server" SkinID="ddlRequired"  DataSource="<%# FillCategory() %>"
                                            DataTextField="Chem_Cat_SName" DataValueField="Cat_Code" >      
                                               <%-- <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>                                     
                                                <asp:ListItem Text="A" Value="0"></asp:ListItem>  
                                                <asp:ListItem Text="B" Value="1"></asp:ListItem>  
                                                <asp:ListItem Text="C" Value="2"></asp:ListItem>  
                                                <asp:ListItem Text="D" Value="3"></asp:ListItem>  
                                                <asp:ListItem Text="A+" Value="4"></asp:ListItem>  
                                                <asp:ListItem Text="A++" Value="5"></asp:ListItem> --%> 
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>    
                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr" runat="server" SkinID="ddlRequired" DataSource ="<%# FillTerritory() %>" DataTextField="Territory_Name" Width="180px"  DataValueField="Territory_Code">                                           
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>    

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Save" OnClientClick="return ValidateEmptyValue()" 
                                    onclick="btnSave_Click" />
                               
                                <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" 
                                    onclick="btnClear_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:10%;"></td>
                <td style="width:30%;">                    
                    <asp:GridView ID="grdTerr" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" 
                        GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                            
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Territory_Name" ShowHeader="true" HeaderText="Territory Name"  ItemStyle-Width="80%"  />
                            <asp:BoundField DataField="Territory_Cat" ShowHeader="true" HeaderText="Type" ItemStyle-Width="20%" />                            
                        </Columns>
                         <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                    </asp:GridView>
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
