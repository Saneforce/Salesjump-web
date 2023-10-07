<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptListedDr_View.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_rptListedDr_View" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer View</title>
    <link type="text/css" rel="stylesheet" href="../../../css/Report.css" />
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
          
            <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </center>
            <center>
            <asp:Panel ID="pnlContents" runat="server" Width="100%"  >

                <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Listed Customer Details" Font-Underline="True"
                            Font-Bold="True" ></asp:Label>
                            <br />
                             <asp:Label ID="Label1" runat="server"  Font-Underline="True"
                            Font-Bold="True" ></asp:Label>
                 </div>
            
            <br />
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" CssClass="mGridImg" OnRowDataBound="grdDoctor_RowDataBound">
                                    <HeaderStyle BackColor="#0097AC" CssClass="grdDoc_View_Column" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Customer Name" ItemStyle-CssClass="grdDoc_View_Row" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qual." ItemStyle-CssClass="grdDoc_View_Row" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-CssClass="grdDoc_View_Row" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Channel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grdDoc_View_Row" >
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB/DOW" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOB","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grdDoc_View_Row">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("ListedDr_Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
