<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockiest_Upload.aspx.cs" Inherits="MasterFiles_Options_Stockiest_Upload" %>


<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stockist Upload</title>
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
                        <td style="padding-left: 80px">
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
                            <asp:Label ID="Label1" runat="server" Font-Size="11px" Font-Names="Verdana" Width="280px" Text="1) Sheet Name Must be 'UPL_Stockist_Master'"></asp:Label>
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
                </table>
            </asp:Panel>
        </center>
     
    </div>
    </form>
</body>
</html>
