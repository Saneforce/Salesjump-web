<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMRStatus.aspx.cs" Inherits="Reports_rptMRStatus" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fieldforce Status Report</title>
   
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
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
        <center>
            <asp:Panel ID="pnlbutton" runat="server">
                <br />
                <table>
                    <tr>
                        <td align="center">
                        </td>
                    </tr>
                </table>
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
            </asp:Panel>
           
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="FieldForce Status Report" Font-Underline="True"
                        Font-Bold="True" Font-Size="9pt"></asp:Label>
                </div>
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblActiveHeader" runat="server" Font-Size="9pt" Font-Bold="true"
                                    Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdTerritory" runat="server" Width="50%" HorizontalAlign="Center"
                                    BorderWidth="1" AutoGenerateColumns="false" OnRowDataBound="grdTerritory_RowDataBound" HeaderStyle-Font-Size="8pt"
                                    CssClass="mGrid">
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory Code" Visible="false" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory_Code" runat="server" Text='<%# Bind("Territory_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White" Visible="false">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory_SName" runat="server" Text='<%# Bind("Territory_SName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ListedDR_Count" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblListedDR_Count" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Chemists_Count" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemists_Count" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="UnListedDR_Count" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnListedDR_Count" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                                    BorderWidth="1" AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound" HeaderStyle-Font-Size="8pt"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">

                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <RowStyle HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left"
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
                                                <asp:Label ID="lblDOB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOW","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
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

                                <asp:GridView ID="grdNonDR" runat="server" Width="85%" HorizontalAlign="Center" BorderWidth="1"
                                    AutoGenerateColumns="false" OnRowDataBound="grdNonDR_RowDataBound" HeaderStyle-Font-Size="8pt"
                                    CssClass="mGrid">
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UnListed Customer Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnDRName" runat="server" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qual." ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnSpeciality" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnCategory" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnClass" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Root Plan" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnMobile" runat="server" Text='<%# Bind("UnListedDr_DOB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnMobile" runat="server" Text='<%# Bind("UnListedDr_DOW") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnMobile" runat="server" Text='<%# Bind("UnListedDr_Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnEMail" runat="server" Text='<%# Bind("UnListedDr_Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="grdChem" runat="server" Width="85%" HorizontalAlign="Center" BorderWidth="1"
                                    AutoGenerateColumns="false" OnRowDataBound="grdChem_RowDataBound" HeaderStyle-Font-Size="8pt"
                                    CssClass="mGrid">
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="chemists_code" ItemStyle-HorizontalAlign="Left" Visible="false"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblchemists_code" runat="server" Text='<%# Bind("chemists_code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemists_Name" runat="server" Text='<%# Bind("Chemists_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemists_Contact" runat="server" Text='<%# Bind("Chemists_Contact") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemists_Phone" runat="server" Text='<%# Bind("Chemists_Phone") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Root Plan" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChemTerritory_Name" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="grdStok" runat="server" Width="85%" HorizontalAlign="Center" BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                    AutoGenerateColumns="false" CssClass="mGrid">
                                    <HeaderStyle BorderWidth="1" />
                                    <RowStyle BorderWidth="1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stockist_Code" ItemStyle-HorizontalAlign="Left" Visible="false"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockist_Code" runat="server" Text='<%# Bind("Stockist_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Stockist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockist_ContactPerson" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
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

           