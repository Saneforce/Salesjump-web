<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAgainstDeactivatedDR.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_AddAgainstDeactivatedDR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add against Deactivated Customer</title>
   
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
     <script type="text/javascript">
         function btnback_click() {
             var url = "LstDoctorList.aspx"
             $(location).attr('href', url);

             return false;
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <div id="Divid" runat="server">
        </div>
        <asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">
          <asp:Button ID="btnBack" CssClass="BUTTON"  Height="25px" Width="60px" OnClientClick= "return btnback_click();" Text="Back" runat="server" 
                     />
        </asp:Panel>
           <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel> 
         
            <br />     
        <center>

        <table align="center" width="90%">
            <tr>
                <td>
                    <asp:GridView ID="grdListedDR" runat="server" Width="90%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  OnRowDataBound="grdListedDR_RowDataBound" 
                        GridLines="None" CssClass="mGridImg" 
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />                        
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="Listed Customer Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180px">
                                <ItemTemplate>
                                    <asp:TextBox ID="ListedDR_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"  runat="server" Width="180px" Text='<%#Eval("ListedDR_Name")%>'></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="rfvDoc" runat="server"  setfocusonerror="true" ControlToValidate="ListedDR_Name"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:TextBox ID="ListedDR_Address1" runat="server" onkeypress="AlphaNumeric(event);" SkinID="TxtBxAllowSymb" MaxLength="200" Width="200px" Text='<%#Eval("ListedDR_Address1")%>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired" DataSource ="<%# FillCategory() %>" DataTextField="Doc_Cat_SName" DataValueField="Doc_Cat_Code">                                           
                                    </asp:DropDownList>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlspcl" runat="server"  SkinID="ddlRequired" DataSource ="<%# FillSpeciality() %>" DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">                                           
                                    </asp:DropDownList>
                                 
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlQual" runat="server"  SkinID="ddlRequired" DataSource ="<%# FillQualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">                                          
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired" DataSource ="<%# FillClass() %>" DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">                                           
                                    </asp:DropDownList>
                                      
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTerr" runat="server" SkinID="ddlRequired" DataSource ="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
                                    </asp:DropDownList>
                                        
                                </ItemTemplate>
                            </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                               
                                        <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxAllowSymb" Width="200px"></asp:TextBox>
                                        <asp:HiddenField ID="hdnTerritoryId" runat="server"></asp:HiddenField>
                                        <asp:PopupControlExtender ID="txtTerritory_PopupControlExtender" runat="server" Enabled="True"
                                            ExtenderControlID="" TargetControlID="txtTerritory" PopupControlID="Panel2" OffsetY="22">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel2" runat="server" Height="116px" Width="200px" BorderStyle="Solid"
                                            BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                            Style="display: none">
                                            <asp:CheckBoxList ID="ChkTerritory" runat="server" Width="180px"  
                                                DataTextField="Territory_Name" DataValueField="Territory_Code" AutoPostBack="True"
                                                OnSelectedIndexChanged="ChkTerritory_SelectedIndexChanged" onclick="checkAll(this);">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
 
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="grdOrgDR" runat="server" Width="90%" 
                        HorizontalAlign="Center" ShowHeader="false"
                        AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" 
                        AlternatingRowStyle-CssClass="alt" BorderStyle="None">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>   
                            <asp:BoundField DataField = "ListedDR_Name" ItemStyle-Width="180px" />
                            <asp:BoundField DataField = "ListedDr_Address1" ItemStyle-Width="200px"/>
                            <asp:BoundField DataField = "Doc_Cat_Name" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField = "Doc_Special_Name" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField = "Doc_QuaName" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField = "Doc_ClsName" ItemStyle-Width="150px"/>
                            <asp:BoundField DataField = "territory_Name" ItemStyle-Width="150px"/>
                        </Columns>
                    </asp:GridView>

                </td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Save" 
                        onclick="btnSave_Click" />
               
                    <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" 
                        onclick="btnClear_Click" />
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
