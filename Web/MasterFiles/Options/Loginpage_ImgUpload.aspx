<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Loginpage_ImgUpload.aspx.cs"
    Inherits="MasterFiles_Options_Loginpage_ImgUpload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page Image Upload</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblHome" SkinID="lblMand" runat="server">Login Page Image</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:FileUpload ID="fileUpload1" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand">Subject</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSub" runat="server" SkinID="MandTxtBox" Width="160px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnUpload" runat="server" Width="70px" Height="25px" Text="Upload" CssClass="BUTTON" OnClick="btnUpload_Click" />
        </center>
        <br />
        <center>
            <table width="70%">
                <tr>
                    <td>
                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowCommand="gvDetails_RowCommand"
                            OnRowDeleting="gvDetails_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Subject" HeaderText="Subject" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="FileName" HeaderText="FileName" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="FilePath" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Upload_Date" HeaderText="Upload Date and Time" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Image');">Delete
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
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
