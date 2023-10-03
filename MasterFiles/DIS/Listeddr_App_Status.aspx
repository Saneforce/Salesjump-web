<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_App_Status.aspx.cs" Inherits="MasterFiles_MGR_Listeddr_App_Status" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer Approval - Status</title>
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
            .bgClr{        
        background-color:Green;
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
        <ucl:Menu ID="menu1" runat="server" />     
    <br />
    <center>
      <table>
                <tr>
                    <td>
                        <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" 
                            AutoPostBack="true" onselectedindexchanged="ddlFieldForce_SelectedIndexChanged"
                           >
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
             <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
    </center>
    <br />
         <center>
        <table border="1" >
        <tr>
        <td>
        <asp:Button ID="btnAdd_Pending" runat="server" Visible="false"
                Text="Listed Customer Addition - Approval Pending" 
                onclick="btnAdd_Pending_Click" />&nbsp;&nbsp;
        </td>
        <td>
        <asp:Button ID="btnDeact_Pending" runat="server" Visible="false"
                Text="Listed Customer Deactivation - Approval Pending" 
                onclick="btnDeact_Pending_Click" />&nbsp;&nbsp;
        </td>
        <td>
        <asp:Button ID="btnReject" runat="server" Text="Approval - Rejection List" Visible="false"
                onclick="btnReject_Click" />&nbsp;&nbsp;
        </td>
        </tr>
        </table>
      </center>
      <br />
      <asp:Panel ID="pnlList" runat="server" HorizontalAlign="Center">
      <asp:Label ID="lblADD" runat="server" Text="Listed Customer Additional Approval Pending List" Font-Size="14px" Font-Names="Verdana" Font-Bold="true" ForeColor="DarkGreen" Visible="false" Font-Underline="true"></asp:Label>
        <asp:Label ID="lblDeAct" runat="server" Text="Listed Customer Deactivation Approval Pending List" Font-Size="14px" Font-Bold="true" Font-Names="Verdana" ForeColor="DarkGreen" Visible="false" Font-Underline="true"></asp:Label>
        <asp:Label ID="lblReject" runat="server" Text="Approval - Rejection List" Font-Size="14px" Font-Names="Verdana" Font-Bold="true" ForeColor="DarkGreen" Visible="false" Font-Underline="true"></asp:Label>
      </asp:Panel>
        <table width="100%" align="center" style="margin-top:10px">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound"                                            
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" >
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDocName" SkinID="MandTxtBox" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                     <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate> 
                                    <EditItemTemplate>
                                     <asp:DropDownList ID="ddlDocCat" runat="server" SkinID="ddlRequired"  DataSource ="<%# Doc_Category() %>" DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">                                           
                                    </asp:DropDownList>
                                    </EditItemTemplate>                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                   <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                    </ItemTemplate>  
                                    <EditItemTemplate>
                                     <asp:DropDownList ID="ddlDocSpec" runat="server" SkinID="ddlRequired"  DataSource ="<%# Doc_Speciality() %>" DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">                                           
                                    </asp:DropDownList>
                                    </EditItemTemplate>                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                    </ItemTemplate>     
                                    <EditItemTemplate>
                                     <asp:DropDownList ID="ddlDocQua" runat="server" SkinID="ddlRequired" DataSource ="<%# Doc_Qualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">                                           
                                    </asp:DropDownList>
                                    </EditItemTemplate>                               
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                    </ItemTemplate>     
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDocClass" runat="server" SkinID="ddlRequired" DataSource ="<%# Doc_Class() %>" DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">                                           
                                    </asp:DropDownList>
                                    </EditItemTemplate>                              
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>      
                                    <EditItemTemplate>
                                       <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource ="<%# Doc_Territory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
                                    </asp:DropDownList>
                                    </EditItemTemplate>                             
                                </asp:TemplateField>     
                                                         
                            </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
      
    </div>
    </form>
</body>
</html>
