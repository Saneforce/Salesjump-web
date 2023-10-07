<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisitDocList.aspx.cs" Inherits="MIS_Reports_VisitDocList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Customer List</title>
    <link type="text/css" rel="stylesheet" href="../css/Report.css" />
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
            
            <br />
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
                                        onclick="btnPrint_Click"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnExcel_Click"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnPDF_Click"
                                        />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent()"
                                        />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Visit Customer List for the month of "
                            Font-Underline="True" Font-Bold="True" Font-Size="Small"></asp:Label>
                   <br />
                            <asp:Label ID="lblsubhead" runat="server" Font-Underline="True" Visible="false" Font-Bold="True"></asp:Label>
                       </div>
                       <br />
                       <div>
                            <asp:GridView ID="grdDoctor" runat="server" Width="65%" HorizontalAlign="Center"
                                OnRowDataBound="grdDoctor_RowDataBound" AutoGenerateColumns="false" GridLines="None"
                                CssClass="mGrid">
                                <HeaderStyle BorderWidth="1" />
                                <RowStyle BorderWidth="1" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Listed Customer Code" ItemStyle-HorizontalAlign="Left"
                                        Visible="false">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDRCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qual" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit Count" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVisitCount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit Date" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="40%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVisitDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
            <br />
            <%--<asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width = "95%">
    </asp:Table>
    
     <tr>
                <td align="center">--%>
            <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Title1" />
                </Titles>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                        LegendStyle="Row" />
                </Legends>
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderWidth="0">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            </asp:Panel>
            <%--   </td>
            </tr> --%>
        </center>
        
    </div>
    </form>
</body>
</html>
