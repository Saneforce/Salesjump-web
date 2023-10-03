<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesForce_React.aspx.cs" Inherits="MasterFiles_SalesForce_React" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Field Force Reactivation</title>
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>            
    <link type="text/css" rel="stylesheet" href="../css/style.css"/>
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
    <style type="text/css">               
        #tooltip
        {
            position: absolute;
            z-index: 3000;
            border: 1px solid #111;
            background-color: #FEE18D;
            padding: 5px;
            opacity: 0.85;
        }                                
        #tooltip h3, #tooltip div
        {
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID ="menu1" runat ="server" />
    <br />
    <br />
       <table width ="100%" align="center" >
       
          <tr >
             <td colspan="2" align="center" >

                <asp:GridView ID="grdSalesForce" runat ="server" Width="85%" HorizontalAlign="Center" 
                AutoGenerateColumns="false"  EmptyDataText="No Records Found" OnRowDataBound="grdSalesForce_RowDataBound"
                OnRowCommand="grdSalesForce_RowCommand" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                AlternatingRowStyle-CssClass="alt" AllowSorting="true">
                <SelectedRowStyle BackColor="BurlyWood" />
                
                   <Columns >
                     <asp:TemplateField HeaderText="S No" HeaderStyle-ForeColor ="White">
                        <ItemTemplate >
                          <asp:Label ID ="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>                     
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText ="Sf_Code" Visible ="false" >
                        <ItemTemplate >
                           <asp:Label ID="lblSF_Code" runat="server" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                        </ItemTemplate>                 
                     </asp:TemplateField> 

                     <asp:TemplateField HeaderText="User Name" Visible ="false" >
                       <ItemTemplate>
                          <asp:Label ID="lblUsrName" runat ="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label> 
                       </ItemTemplate>                    
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor ="White" >
                       <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                          <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label> 
                        </ItemTemplate>                     
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor ="White">
                       <ItemStyle HorizontalAlign="Left" />
                         <ItemTemplate>
                           <asp:Label ID ="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                         </ItemTemplate>               
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor ="White">
                       <ItemStyle HorizontalAlign="Left" />
                         <ItemTemplate>
                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="State" HeaderStyle-ForeColor ="White">
                       <ItemStyle HorizontalAlign ="Left" />
                        <ItemTemplate>
                           <asp:Label ID ="lblstateName" runat ="server" Text='<%# Bind("StateName") %>'></asp:Label> 
                        </ItemTemplate>
                     </asp:TemplateField>

                      <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="90px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblS" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                     <asp:TemplateField HeaderText ="Reactivate" HeaderStyle-ForeColor ="White" >
                          <ControlStyle ForeColor ="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="true" />
                          <ItemStyle ForeColor ="DarkBlue" Font-Bold ="false" />
                          <ItemTemplate >
                              <asp:LinkButton ID="lnkbutDeactivate" runat ="server" CommandArgument='<%# Eval("SF_Code") %>' 
                              CommandName ="Reactivate" OnClientClick ="return confirm('Do you want to Reactivate the Fieldforce');">Reactivate 
                              </asp:LinkButton>
                          </ItemTemplate>                      
                        </asp:TemplateField>
                                           
                   </Columns>
                   <EmptyDataRowStyle ForeColor ="Black" BackColor ="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth ="2" Font-Bold ="true" HorizontalAlign ="Center" VerticalAlign ="Middle" />
                </asp:GridView>                 
             </td>          
          </tr>      
       </table>   
    </div>
    </form>
</body>
</html>
