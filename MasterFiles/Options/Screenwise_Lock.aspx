<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Screenwise_Lock.aspx.cs" Inherits="MasterFiles_Options_Screenwise_Lock" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Screenwise Lock</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br /> 
        <center>
            <table align="center">
                <tr>
                <td>
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                   </td>
                   <td>
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                </td>
            </tr>
          </table>
          <br />
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" 
                onclick="btnGo_Click" />
            <br /><br />
            <table width="70%">
                <tr>
                    <td>                        
                        <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Records Found" OnRowDataBound="gvDetails_RowDataBound" 
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("SF_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldForce" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblsf_hq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DCR Lock" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TP Lock" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel2" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SDP Lock" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel3" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Campaign Lock" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel4" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Map Lock" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel5" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>                    
                    </td>
                </tr>
            </table>
          <br />
            <asp:Button ID="btnSumbit" runat="server" Width="70px" Height="25px" Text="Save" CssClass="BUTTON" Visible="false" onclick="btnSumbit_Click" 
                />

        </center>    
           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
