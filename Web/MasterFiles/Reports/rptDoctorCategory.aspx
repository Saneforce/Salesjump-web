<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDoctorCategory.aspx.cs"
    Inherits="Reports_rptDoctorCategory" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server">Listed Customer Details</title>
 
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
    </style>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .bar
        {
            text-align: center;
        }
    </style>
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
            <br />
            <center>
            <table width="100%" align="center">               
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Listed Doctor Details" Font-Underline="True"
                                Font-Bold="True" Font-Size="9pt"></asp:Label></div>
                        <tr>
                            <td style="height: 30px">
                            </td>
                        </tr>
                      
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                                    BorderWidth="1" AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound" HeaderStyle-Font-Size="8pt"
                                    GridLines="None" CssClass="mGrid">
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSfName" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="txtHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qual." ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Root Plan" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOB","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOW" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOW" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOW","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("ListedDr_Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                    
                    </td>
                    </tr>
                    </asp:Panel>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblCatg" runat="server" Font-Bold="True" Font-Size="Small" Font-Underline="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="60%" align="center">
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:BarChart ID="BarChart1" runat="server" ChartHeight="350" ChartWidth="250" ChartType="Column"
                                Visible="false" ChartTitle="" Font-Names="Verdana" Font-Size="Small">
                            </asp:BarChart>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
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
                        </td>
                    </tr>
              
            </table>
        </center>
    </div>
    </form>
</body>
</html>
