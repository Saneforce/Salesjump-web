<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Primary_Upload.aspx.cs" Inherits="MasterFiles_Options_Primary_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Upload</title>
    <style type="text/css">
        td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ucl:Menu ID="menu1" runat="server" />
    <br />
    <div>
        <center>
            <asp:Panel ID="pnlSalesForce" Width="90%" runat="server">
                <table cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color: White">
                    <tr>
                        <td align="center" class="stylespc">
                            <table align="center" width="200px" >
                                <tr>
                                    <td  align="center" class="stylespc" >
                                        <asp:Label ID="lblMoth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                                  
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
                                    <td  class="stylespc" align="center"  >
                                        <asp:Label ID="lblToYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                                
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblExcel" runat="server" SkinID="lblMand" Font-Size="Small" Font-Names="Verdana">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" runat="server" />
                            </td>
                          
                        </tr>
                        <tr>
                            <%--<td style="padding-left: 80px">
                            <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                Text="Deactivate Existing Salesforce List ( if Yes then Check this Option )" OnCheckedChanged="chkDeact_CheckedChanged" />
                        </td>--%>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button1" runat="server" Width="70px" Height="25px" BackColor="BurlyWood"
                                    Font-Size="Medium" Text="Upload" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid Black; padding-left: 80px;">
                                <asp:Label ID="lblIns" runat="server" Text="Note:" ForeColor="Red"></asp:Label>
                                &nbsp; &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="11px" Font-Names="Verdana" Width="280px"
                                    Text="1) Sheet Name Must be 'UPL_Primary_Master'"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblExc" runat="server" Text="Excel Format File" Font-Size="Medium"></asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana"
                                    Text="Download Here" OnClick="lnkDownload_Click"> 
                                </asp:LinkButton>
                            </td>
                        </tr>
                        </table> </td>
                    </tr>
                </table>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
