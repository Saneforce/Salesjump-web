<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Salesforce_Approval_Changes.aspx.cs" Inherits="MasterFiles_Salesforce_Approval_Changes" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval Changes</title>
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
    #tblModeDtls
    {
        margin-left:12%;
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
<script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JsFiles/jquery.tooltip.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {

                var mode = $('#<%=ddlMode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlMode').focus(); return false; }
                var type = $('#<%=ddlFilter.ClientID%> :selected').text();
                if (type == "---Select Clear---") { alert("Select Manager."); $('#ddlFilter').focus(); return false; }


            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    </div>
    <br /> 
    <center> 
    <table border="0" id="tblModeDtls" align= "center" style="width: 32%"> 
    <tr style="height:30px"> 
    <td> 
    <asp:Label ID="lblMode" runat="server" Text="Select the Mode"></asp:Label>
    </td>
    <td>
                    <asp:DropDownList ID="ddlMode" SkinID="ddlRequired" runat="server">
                         <asp:ListItem Text = "---Select---" Value="-1" Selected="True"></asp:ListItem>
                         <asp:ListItem Text = "DCR" Value="DCR"></asp:ListItem>
                         <asp:ListItem Text = "TP" Value="TP"></asp:ListItem>
                         <asp:ListItem Text = "Listed Customer" Value="LstDr"></asp:ListItem>
                         <asp:ListItem Text= "Leave " Value="Leave"></asp:ListItem>
                         <asp:ListItem Text="Secondary Sales" Value="SS"></asp:ListItem>
                         <asp:ListItem Text="Expense" Value="Expense"></asp:ListItem>
                         <asp:ListItem Text="Other" Value="Otr"></asp:ListItem>
                    </asp:DropDownList>
                </td>
</tr>
<tr>
   
    <td>
    <asp:Label ID="lblFilter" runat="server" Text="Select the Manager"></asp:Label>
    </td>
    <td>
                    <asp:DropDownList ID="ddlFilter" runat="server" SkinID="ddlRequired" 
                        onselectedindexchanged="ddlFilter_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                      </asp:DropDownList>
                    </td>                   
                    </tr>
                    </table>
                   <br />
                   <center>
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" onclick="btnGo_Click" CssClass="BUTTON" />
                      </center> 
    </center>
    <br />
    <br />
    <center>
        <table width="100%" align="center">
        <tbody>
        <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdSalesForce" runat="server" Width="80%" HorizontalAlign="Center" 
                        AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound = "grdSalesForce_RowDataBound"
                        onpageindexchanging="grdSalesForce_PageIndexChanging"                       
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle Wrap="true" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="FieldForce Name">
                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblsfName"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            
                             <asp:TemplateField HeaderText="Designation" >
                                               
                                  <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="100px" BorderColor="Black" HorizontalAlign="Left">
                                  </ItemStyle>
                                  <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                  <ItemTemplate>
                                     <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                  </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField  HeaderText="HQ">
                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField  HeaderText="Reporting_To">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>                  
                            <asp:TemplateField HeaderText="Approved By" >
                             <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate> 
                                 <asp:DropDownList ID="ddlNew" runat="server" DataSource ="<%# Fill_Approved_By() %>" DataTextField="sf_name" DataValueField="sf_code" SkinID="ddlRequired">                                           
                                    </asp:DropDownList> 
                                </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
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
                    <asp:Button ID="btnApproval" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Update" Visible="false"
                        onclick="btnApproval_Click" />
                </td>
            </tr>
        </tbody>
    </table>
    </center>
      <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>
    </form>
</body>
</html>
