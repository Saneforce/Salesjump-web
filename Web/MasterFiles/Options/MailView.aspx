<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailView.aspx.cs" Inherits="MasterFiles_Options_MailView" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail Delete</title>

    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <ucl:Menu ID="menu1" runat="server" /> 
        <center>
        <br />
       <table id="tblTpRpt">
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack = "true" SkinID="ddlRequired">
                     <asp:ListItem Selected="True" Value="-1" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>                        
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                </td>
               
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                               
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" SkinID="lblMand" runat="server" Text="Search by"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="1" Text="Inbox"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Viewed"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Sent"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" class="stylespc">
                    <asp:Label ID="Label2" runat="server" Text="Search" SkinID="lblMand"></asp:Label>
                </td>
                <td colspan="3" align="left" class="stylespc">
                    <asp:TextBox ID="txtSearch" runat="server" SkinID="MandTxtBox" Width="100"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="BUTTON"
                        onclick="btnSearch_Click"/>
                </td>
            </tr>
        </table>

        <br />

        <table width="80%" align="center">
            <tr>
                <td height="50%" valign="top">
                        <asp:GridView ID="gv1" runat="server" Width="85%" HorizontalAlign="Center" 
                            AutoGenerateColumns="false" 
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                            AlternatingRowStyle-CssClass="alt">
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <RowStyle HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="From_Name" ItemStyle-HorizontalAlign="Left" HeaderText="From"
                                HeaderStyle-Width='30%' ItemStyle-CssClass="print" SortExpression="From_Name" />
                            <asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderText="Subject"
                                HeaderStyle-Width='30%' ItemStyle-CssClass="print" SortExpression="Mail_Subject" />
                            <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width='15%' ItemStyle-CssClass="print" SortExpression="Mail_Sent_Time" />
                            <asp:BoundField DataField="Mail_Attachement" ItemStyle-HorizontalAlign="center" HeaderStyle-Width='7%'
                                HeaderText="Attachment" ItemStyle-CssClass="print" />
                            <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                ItemStyle-CssClass="print" Visible="false"/>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnslNo" runat="server"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#DDEEFF" />
                    </asp:GridView>
                </td>
            </tr>
        </table>

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
