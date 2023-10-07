<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditSalesForce.aspx.cs"
    Inherits="MasterFiles_BulkEditSalesForce" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FieldForce - Bulk Edit</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script src="../JsFiles/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../JsFiles/ScrollableGridPlugin.js" type="text/javascript"></script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
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
        function myDropDown() {
            var myDropDown = document.getElementById("ddlFilter");
            var length = myDropDown.options.length;
            //open dropdown
            myDropDown.size = length;
            //close dropdown
            myDropDown.size = 0;
        }
    </script>
     <script type="text/javascript">
         function HidePopup() {

             var popup = $find('TextBox1_PopupControlExtender');
             popup.hide();
         }
        </script>
        <center>
            <table border="0" cellpadding="0" cellspacing="0" id="tblLocationDtls" width="80%">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Width="180px" Text="Select the Fields to Edit"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#8A2EE6">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr >
                    <td align="left">
                        <asp:CheckBoxList ID="CblSFCode" RepeatColumns="4" CssClass="Checkbox" runat="server" style="margin-left:280px"
                            RepeatDirection="Horizontal" Width="550px">
                            <asp:ListItem Value="Sf_Name">&nbsp;FieldForce Name</asp:ListItem>
                            <asp:ListItem Value="UsrDfd_UserName">&nbsp;User Name</asp:ListItem>
                            <asp:ListItem Value="Sf_Password">&nbsp;Password</asp:ListItem>
                            <asp:ListItem Value="Sf_HQ">&nbsp;HQ</asp:ListItem>
                            <asp:ListItem Value="State_Code">&nbsp;State</asp:ListItem>
                            <asp:ListItem Value="sf_emp_id">&nbsp;Employee ID</asp:ListItem>
                            <asp:ListItem Value="Designation_Code">&nbsp;Designation</asp:ListItem>
                            <asp:ListItem Value="Sf_Joining_Date">&nbsp;Joining Date</asp:ListItem>
                            <asp:ListItem Value="subdivision_code">&nbsp;Sub Division</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnOk" runat="server" CssClass="BUTTON" Text="Ok" Width="35px" Height="25px" OnClick="btnOk_Click" />
                        <asp:Button ID="btnClr" CssClass="BUTTON" runat="server" Text="Clear"  Width="60px" Height="25px"
                            OnClick="btnClr_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="width: 8%" />
                        <td align="left">
                            <asp:Label ID="SearchBy" runat="server" Text="SearchBy" Visible="false">
                            </asp:Label>
                            <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList" AutoPostBack="true"
                                Visible="false" onselectedindexchanged="ddlFields_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Value="">Select</asp:ListItem>
                                <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                                 <asp:ListItem Value="StateName">State</asp:ListItem>
                               <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="TEXTAREA" SkinID="MandTxtBox" Width="150px" Visible="false"></asp:TextBox>
                               <asp:DropDownList ID="ddlSrc" runat="server" AutoPostBack="true"  Visible ="false" OnSelectedIndexChanged= "ddlSrc_SelectedIndexChanged"  
                                    SkinID="ddlRequired" TabIndex="4" >                    
                                </asp:DropDownList>   
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="30px" Height="25px" Text="Go" Visible="false"
                                CssClass="BUTTON"></asp:Button>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager" Visible="false"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="ddlFilter" runat="server" Visible="false" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" OnClick="btnGo_Click"
                                Visible="false" />
                        </td>
                        <td style="width: 8%" />
                    </tr>
                </tbody>
            </table>
           <%-- </ContentTemplate>
            </asp:UpdatePanel>
      --%>
            <br />
            <table width="100%">
            <tr>
            <td align="center">
            <asp:Label ID="lblSelect" Text="Please Select either Search by or Filter By Manager" runat="server" ForeColor="Red" Font-Size="Medium" visible="false"></asp:Label>
            </td>
            </tr>
            </table>
            <table runat="server" id="tblSalesForce" visible="false" width="85%">
                <tr>
                    <td>
                        <asp:GridView ID="grdSalesForce" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" OnRowCreated="grdSalesForce_RowCreated" OnRowDataBound="grdSalesForce_RowDataBound"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF_Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSf_Name" runat="server" SkinID="TxtBxAllowSymb" widht="220px" MaxLength="150"
                                            Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                                </asp:TemplateField>      
                                 <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesignation" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                                </asp:TemplateField>                         
                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSf_HQ" runat="server" SkinID="TxtBxAllowSymb" MaxLength="150" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Sf_Name" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Sf_Name") %>'
                                            Width="240px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="UsrDfd_UserName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="70"
                                            Text='<%# Bind("UsrDfd_UserName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Sf_Password" runat="server" SkinID="TxtBxAllowSymb" Width="100px"
                                            MaxLength="15" Text='<%# Bind("Sf_Password") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Sf_HQ" runat="server" SkinID="TxtBxAllowSymb" Width="100px" MaxLength="150"
                                            Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="State_Code" runat="server" Width="180px" DataSource="<%# FillState() %>"
                                            DataTextField="StateName" DataValueField="State_Code" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="sf_emp_id" runat="server" SkinID="TxtBxAllowSymb" MaxLength="10"
                                            Width="50px" Text='<%# Bind("sf_emp_id") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <%-- <asp:TextBox ID="ddlDesignation" runat="server" SkinID="TxtBxAllowSymb" Width="200px" MaxLength="150"
                                            Text='<%# Bind("Designation_Code") %>'></asp:TextBox>--%>
                                        <asp:DropDownList ID="Designation_Code" runat="server" Width="200px" DataSource="<%# FillDesignation() %>"
                                            DataTextField="Designation_Name" DataValueField="Designation_Code" SkinID="ddlRequired">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:TemplateField>
                              <%--  <asp:TemplateField HeaderText="Joining Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="Sf_Joining_Date" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Sf_Joining_Date") %>'></asp:TextBox>
                                   
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="Sf_Joining_Date"
                                            runat="server" />
                      </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="Joining Date">                                    
                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                     <ItemTemplate>
                                        <asp:TextBox ID="Sf_Joining_Date" onkeypress="Calendar_enter(event);" runat="server" Text='<%# Bind("Sf_Joining_Date") %>' SkinID="MandTxtBox"></asp:TextBox>
                                        <asp:CalendarExtender   
                                            ID="CalendarExtender1" Format="dd/MM/yyyy"  
                                            TargetControlID="Sf_Joining_Date"                                               
                                            runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Sub Division" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updatepanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" SkinID="TxtBxAllowSymb" Width="145px"></asp:TextBox>
                                                <asp:HiddenField ID="hdnSubDivisionId" runat="server"></asp:HiddenField>
                                                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True" 
                                                    ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22">
                                                </asp:PopupControlExtender>
                                                <asp:Panel ID="Panel1" runat="server" Height="116px" Width="155px" BorderStyle="Solid"
                                                    BorderWidth="1px"  Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                    Style="display: none">
                                                     <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="btnsubdiv" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color: Yellow; 
                                            Color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>
                                                    <asp:CheckBoxList ID="subdivision_code" runat="server" Width="155px" CssClass="collp" DataSource="<%# FillCheckBoxList() %>"
                                                        DataTextField="subdivision_name" DataValueField="subdivision_code" AutoPostBack="True"
                                                        OnSelectedIndexChanged="subdivision_code_SelectedIndexChanged" onclick="checkAll1(this);">
                                                    </asp:CheckBoxList>
                                                <%--   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [subdivision_code],[subdivision_name] FROM [mas_subdivision]"></asp:SqlDataSource>--%>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
               <tr>
               <td>
               <br />
               </td>
               </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnUpdate" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Update" OnClick="btnUpdate_Click" />
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
