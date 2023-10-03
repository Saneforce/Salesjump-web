<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HospitalCreation.aspx.cs" Inherits="MasterFiles_MR_Hospital_HospitalCreation" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hospital Creation</title>
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
    <script type="text/javascript">
        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= grdHospital.ClientID %>');
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
                        var HospitalName = document.getElementById('grdHospital_ctl' + index + '_Hospital_Name');

                        var Address = document.getElementById('grdHospital_ctl' + index + '_Hospital_Address1');
                        var Contact = document.getElementById('grdHospital_ctl' + index + '_Hospital_Contact');
                        var phoneNo = document.getElementById('grdHospital_ctl' + index + '_Hospital_Phone');
                        var territory = document.getElementById('grdHospital_ctl' + index + '_ddlTerr');
                        if (HospitalName.value != '' || Address.value != '' || Contact.value != '' || phoneNo.value != '' || territory.value != '0') {
                            isEmpty = true;
                        }
                        if (HospitalName.value != '') {
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
                            if (HospitalName.value == '') {
                                alert('Enter Hospital Name')
                                HospitalName.focus();
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
                            if (HospitalName.value == '') {
                                alert('Enter Hospital Name')
                                HospitalName.focus();
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
                            if (HospitalName.value == '') {
                                alert('Enter Hospital Name')
                                HospitalName.focus();
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
                            if (HospitalName.value == '') {
                                alert('Enter Hospital Name')
                                HospitalName.focus();
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
                        return false;
                    }
                    else

                        return true;
                }

            }
     
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

        <table runat="server" id="tblChemist" width="80%">
            <tr>
                <td valign="top" style="width:60%;" align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="grdHospital" runat="server" Width="85%" HorizontalAlign="Center" 
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowCreated="grdHospital_RowCreated"
                                    GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                                    onrowdatabound="grdHospital_RowDataBound"
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
                                        <asp:TemplateField HeaderText="Hospital Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Hospital_Name" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event)" SkinID="TxtBxAllowSymb" MaxLength="250" Width="250px" Text='<%#Eval("Hospital_Name")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Hospital_Address1" runat="server" SkinID="TxtBxAllowSymb" MaxLength="250" Width="250px" Text='<%#Eval("Hospital_Address1")%>' onkeypress="AlphaNumeric(event);"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Hospital_Contact" runat="server" SkinID="TxtBxAllowSymb" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="250" Width="190px" Text='<%#Eval("Hospital_Contact")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Hospital_Phone" runat="server" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="250" Width="150px" Text='<%#Eval("Hospital_Phone")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                          <ItemStyle Width="180px" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr" runat="server" SkinID="ddlRequired" Width="180px" DataSource ="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
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
