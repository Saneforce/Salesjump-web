<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Usermanual_Upload.aspx.cs" Inherits="MasterFiles_Options_Usermanual_Upload" %>
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
 <title>User Manual Upload</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <%-- <ucl:Menu ID="menu1" runat="server" />--%>
        <br /> 
        
        <center>
            <table cellpadding="5" cellspacing="5">
                <tr>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="File Upload: "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="fileUpload1" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Subject: "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFileSubject" SkinID="MandTxtBox" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnUpload" runat="server" CssClass="BUTTON" Text="Upload" Width="70px" Height="25px" OnClick="Upload" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="70%">
                <tr>
                    <td>                        
                        <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Records Found" OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting"   
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" Visible="false"/>
                         <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FileSubject" HeaderText="Subject" ItemStyle-HorizontalAlign="Left"/>                        
                       <%-- <asp:TemplateField HeaderText="Uploaded File" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFileName" runat="server" Text='<%# Eval("FileName") %>' OnClick="DownloadFile" CommandArgument='<%# Eval("FileName") %>' >
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                         <asp:BoundField DataField="FileName" HeaderText="File Name" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Download">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                        
                        <asp:BoundField DataField="Update_dtm" HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left" />

                       <%-- <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                       <asp:LinkButton ID="lnkbutDel" runat="server" OnClientClick="return confirm('Do you want to delete the Image');">Delete
                                </asp:LinkButton>
                                </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDel" Font-Size="11px" Font-Names="Verdana" runat="server" CommandArgument='<%# Eval("ID") %>'
                                            CommandName="Delete" OnClientClick="return confirm('Do you want to delete the File');">Delete
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>                    
                    </td>
                </tr>
            </table>
        </center>
  
    </div>
    </form>
</body>
</html>
</asp:Content>