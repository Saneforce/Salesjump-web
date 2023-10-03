<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ffagetpwd.aspx.cs" Inherits="UserInfo_ffagetpwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
       <style type="text/css">
       .mGrid { width: 100%; /*background:url(menubg.gif) center center repeat-x;*/ background:white; }
    .mGrid td { padding: 2px; border: solid 1px Black; border-color:Black; border-left: solid 1px Black;border-right: solid 1px Black; border-top: solid 1px Black; font-size:small; font-family:Calibri}   


    .mGrid th { padding: 4px 2px; color:white; background:#A6A6D2; border-color:Black; border-left: solid 1px Black;border-right: solid 1px Black; border-top: solid 1px Black; border-bottom:solid 1px Black; font-weight:normal; font-size: small; font-family:Calibri }

.mGrid .pgr {background: #A6A6D2; }
    .mGrid .pgr table { margin: 5px 0; }
    .mGrid .pgr td { border-width: 0; padding: 0 6px; text-align:left; border-left: solid 1px #666; font-weight: bold; color: White; line-height: 12px; }   
    .mGrid .pgr th { background: #A6A6D2;}
    .mGrid .pgr a { color: #666; text-decoration: none; }
    .mGrid .pgr a:hover { color: #000; text-decoration: none; }
      td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
       </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
        <center>
            <h2>
                Get User Password</h2>
        </center>
  <br />
        <asp:Panel ID="pnlcompl" runat="server" HorizontalAlign="Right" style="margin-right:20px">
        <table align="Right" style="margin-right:20px">
            <tr>
                <td>
                    
                    <asp:Button ID="btnLog" runat="server" Text="Logout" BackColor="Pink" 
                        onclick="btnLog_Click" />
                </td>
            </tr>
        </table>
</asp:Panel>
        <center>
            <table cellpadding="3" cellspacing="3">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblServer" runat="server" SkinID="lblMand" Text="Server Name"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlserver" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Server 1" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDatabase" runat="server" SkinID="lblMand" Text="DataBase Name"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddldatabase" runat="server" SkinID="ddlRequired" 
                            onselectedindexchanged="ddldatabase_SelectedIndexChanged" AutoPostBack="true">
                         
                        
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblcompany" runat="server" SkinID="lblMand" Text="Company Name"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlcompany" runat="server" SkinID="ddlRequired" 
                            AutoPostBack="true" onselectedindexchanged="ddlcompany_SelectedIndexChanged">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="SearchBy" SkinID="lblMand" runat="server" Text="SearchBy">
                        </asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                            <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                            <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                            <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                            <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" Width="150px" CssClass="TEXTAREA"
                            Visible="false"></asp:TextBox> &nbsp;&nbsp;
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="30px" Height="25px" CssClass="BUTTON">
                            </asp:Button>
                    </td>
                </tr>
            </table>
        </center>
        <center>
           <table width="100%" align="center"  >
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="90%" HorizontalAlign="Center"
                                AutoGenerateColumns="false"  EmptyDataText="No Records Found"
                            
                                GridLines="None" CssClass="mGrid"  AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="gridview1"  ></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                    <HeaderTemplate></HeaderTemplate>
                                        <ControlStyle Width="90%"></ControlStyle>                                   
                                                                          
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                        <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="User Name" Visible="false"
                                        >
                                       
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           
                                    <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                      
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        
                                
                                    <asp:TemplateField SortExpression="Designation_Name" HeaderText="Designation" HeaderStyle-ForeColor="white">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ" HeaderStyle-ForeColor="white">
                                     
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="State" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    
                                     
                                    </asp:TemplateField>
                                       <asp:TemplateField  HeaderText="User Name" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                        </ItemTemplate>                                   
                                      </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Password" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpass" runat="server" Text='<%# Bind("Sf_Password") %>'></asp:Label>
                                        </ItemTemplate>                                  
                                     </asp:TemplateField>
                                      <asp:TemplateField  HeaderText="Active" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActive" runat="server" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                        </ItemTemplate>                                    
                                      </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Reporting to" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Bind("Reporting_To") %>'></asp:Label>
                                        </ItemTemplate>                                   
                                     
                                    </asp:TemplateField>
                                     <asp:TemplateField  HeaderText="User Name" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("rep_username") %>'></asp:Label>
                                        </ItemTemplate>                                   
                                     
                                    </asp:TemplateField>
                                       <asp:TemplateField  HeaderText="Password" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("rep_password") %>'></asp:Label>
                                        </ItemTemplate>                                   
                                     
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
