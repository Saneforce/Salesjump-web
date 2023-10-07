<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload_MR.aspx.cs" Inherits="MasterFiles_MR_FileUpload_MR" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />
    <title>Files Download</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <div id="Divid" runat="server">
        </div>
        <br />
        <center>
            <table width="70%">
                <tr>
                    <td>
                        <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                  <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" Visible="false"/>
                         <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:BoundField DataField="FileSubject" HeaderText="Subject" />
                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Download">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                            CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
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
