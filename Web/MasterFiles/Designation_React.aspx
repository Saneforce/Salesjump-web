<%@ Page Title="Designation Reactivation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Designation_React.aspx.cs" Inherits="MasterFiles_Designation_React" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Designation Reactivation</title>
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
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
    
	$(document).ready(function() {
		$('#bac').click(function () {
			window.location.href = '../MasterFiles/Designation.aspx?&menuid=2&id=8';
		});
		$('form').live("submit", function () {
			ShowProgress();
		});
	});
	
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" <%--onclick="history.back(-2)"--%> />
           <center>
           <table>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" style="display:none;">
                    <asp:Label ID="Lbldivi" runat="server" SkinID="lblMand">Division Name</asp:Label>
                    <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                        AutoPostBack="true" Height="24px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
           <br />
               <asp:GridView ID="grdDesignation" runat ="server" Width="85%" HorizontalAlign ="Center"
                AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="10" EmptyDataText ="No Records Found" 
                OnRowCommand ="grdDesignation_RowCommand" GridLines ="None" CssClass ="mGrid" PagerStyle-CssClass ="pgr"
                 AlternatingRowStyle-CssClass="alt" AllowSorting ="true" >
                 <SelectedRowStyle BackColor="BurlyWood" />
                    <Columns >
                       <asp:TemplateField HeaderText ="S.No" HeaderStyle-ForeColor ="white" ItemStyle-HorizontalAlign ="Left" >
                            <ItemTemplate >
                               <asp:Label ID="lblSNo" runat ="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                             </ItemTemplate>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderText ="Designation_Code" Visible ="false" >
                            <ItemTemplate >
                               <asp:Label ID="lblDesignationCode" runat="server" Text='<%#Eval("Designation_Code")%>'></asp:Label>
                            </ItemTemplate>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderText ="Short Name" HeaderStyle-ForeColor ="White" ItemStyle-HorizontalAlign ="Left" >
                           <ItemTemplate >
                               <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderText ="Designation" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign ="Left" >
                           <ItemTemplate >
                             <asp:Label ID ="lblDesignationName" runat ="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                        
                        <%--<asp:TemplateField HeaderText ="FieldForce Count" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign ="Left" >
                           <ItemTemplate >
                              <asp:Label ID ="lblSfCount" runat ="server" Text='<%# Bind("sf_count") %>'></asp:Label>
                           </ItemTemplate>
                        
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText ="Reactivate" HeaderStyle-ForeColor ="White" >
                          <ControlStyle ForeColor ="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="true" />
                          <ItemStyle ForeColor ="DarkBlue" Font-Bold ="false" />
                          <ItemTemplate >
                              <asp:LinkButton ID="lnkbutDeactivate" runat ="server" CommandArgument='<%# Eval("Designation_Code") %>' 
                              CommandName ="Reactivate" OnClientClick ="return confirm('Do you want to Reactivate the Designation');">Reactivate 
                              </asp:LinkButton>
                          </ItemTemplate>
                        
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle ForeColor ="Black" BackColor ="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth ="2" Font-Bold ="true" HorizontalAlign ="Center" VerticalAlign ="Middle" />
                 </asp:GridView>                    
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
</asp:Content>