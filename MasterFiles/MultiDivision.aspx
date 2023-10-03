<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiDivision.aspx.cs" Inherits="MasterFiles_MultiDivision" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi Division Selection</title>
      <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
        <br />
        <center>
              <br />
              <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td style="width:7.3%" />
                        <td align="left">
                            <asp:Label id="SearchBy" SkinID="lblMand" runat="server" Text="SearchBy">
                            </asp:Label> 
                             <asp:DropDownList id="ddlFields" TabIndex="1" SkinID="ddlRequired" runat="server" CssClass="DropDownList" AutoPostBack="true"  OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                                <asp:ListItem Value="Sf_UserName">User Name</asp:ListItem>
                                <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                                <asp:ListItem Value="StateName">State</asp:ListItem>
                                <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                            </asp:DropDownList> 
                            <asp:TextBox id="txtsearch" TabIndex="2" runat="server" SkinID="MandTxtBox" Width="150px" CssClass="TEXTAREA" Visible="false"></asp:TextBox> 
                              <asp:DropDownList ID="ddlSrc" runat="server"  Visible ="false" SkinID="ddlRequired" TabIndex="4" >                    
                                        </asp:DropDownList>   
                            <asp:Button id="btnSearch" onclick="btnSearch_Click" Width="30px" Height="25px" runat="server" Text="Go" Visible="false"></asp:Button>
                        </td>
                
<%--                    <td width="45%">
                        <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager"></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server"></asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnGo" runat="server" Text=">" onclick="btnGo_Click" />
                    </td>
--%>
                    </tr>
                </tbody>
            </table>
            <br />
         <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" EmptyDataText="No Records Found" 
                        OnRowCreated="grdSalesForce_RowCreated" OnRowDataBound="grdSalesForce_RowDataBound" 
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr" ></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Sf_UserName" HeaderText="User Name" HeaderStyle-ForeColor="white">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUsrName" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Sf_UserName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtsfName" SkinID="MandTxtBox"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblsfName"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
<%--                            <asp:TemplateField HeaderText="Type">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("SF_Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
--%>                            <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ" HeaderStyle-ForeColor="white">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtHQ" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>                   
                            <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle  Width="170px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updatepanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Width="170px" SkinID="MandTxtBox"></asp:TextBox>
                                                <asp:HiddenField ID="hdnDivisionId" runat="server"></asp:HiddenField>
                                                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True"
                                                    ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22">
                                                </asp:PopupControlExtender>
                                                <asp:Panel ID="Panel1" runat="server" Height="116px" Width="170px" BorderStyle="Solid"
                                                    BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                    Style="display: none">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                                        DataTextField="Division_Name" DataValueField="Division_Code" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" Width="150px">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>                          

                            <asp:HyperLinkField HeaderText="Select Audit Team" Text="Select Audit Team" DataNavigateUrlFormatString="~/MasterFiles/MultiDiv_AuditTeam.aspx?sfcode={0}" DataNavigateUrlFields="SF_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            </asp:HyperLinkField>    
                            <asp:HyperLinkField HeaderText="Select Reporting Team" Text="Select Reporting Team" DataNavigateUrlFormatString="~/MasterFiles/MultiDiv_Reporting_Team.aspx?sfcode={0}" DataNavigateUrlFields="SF_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                            </asp:HyperLinkField>   
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
        </tbody>

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
