<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTPView.aspx.cs" Inherits="MasterFiles_Temp_rptTPView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TP View Report</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <link href="../../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    


    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnPrint" runat="server"  class="btn btnPrint"
                                    OnClick="btnPrint_Click" />
                            </td>
                            <td>
                                <asp:LinkButton ID="btnExcel" runat="server" class="btn btnExcel"
                                    OnClick="btnExcel_Click" />
                            </td>
                            <td>
                                <asp:LinkButton ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPDF_Click" />
                            </td>
                            <td>
                               <%-- <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent()" />--%>
                                     <asp:LinkButton ID="btnClose" runat="server" type="button" Style="padding: 0px 20px;" 
                                         href="javascript:window.open('','_self').close();" class="btn btnClose" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <center>
            <div align="center">
                <asp:Label ID="lblHead" runat="server" Text="TP View"
                    Font-Underline="True" Style="font-family: Verdana; font-size: 9pt" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblHq" runat="server" Font-Underline="True" Font-Size="9pt" Font-Bold="True"></asp:Label></div>
            <br />
            <br />
            <div id="tblStatus" style="padding-left: 50px" runat="server" width="90%" class="common"
                align="left">
                <%--<ul>
                    <li>
                        <asp:Label ID="lblFieldForce" runat="server" Font-Size="9pt" Text="FieldForce Name"
                            CssClass="TPFontSize"></asp:Label>
                        <asp:Label ID="lblFieldForceValue" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label></li>
                    <li>
                        <asp:Label ID="lblValHQ" Text="HQ" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                        <asp:Label ID="lblHQValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label></li>
                    <li>
                        <asp:Label ID="lblDesgn" Text="Designation" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                        <asp:Label ID="lblDesgnValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label></li>
                </ul>  --%>
                <table align="center" width="90%">
                    <tr>
                        <td width="15%">
                            <asp:Label ID="lblFieldForce" runat="server" Font-Size="9pt" Text="FieldForce Name"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblFieldForceValue" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                       
                        <td width="15%">
                            <asp:Label ID="lblValHQ" Text="HQ" Font-Size="9pt" runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblHQValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label>
                        </td>
                        
                        <td width="10%">
                        
                        </td>
                        <td width="10%">
                            <asp:Label ID="lblDesgn" Text="Designation" Font-Size="9pt"  runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblDesgnValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-left: 50px" runat="server" width="90%" class="common" align="left">
                <table align="center" width="90%">
                    <tr>
                       
                        <td width="15%">
                            <asp:Label ID="lblCompleted" runat="server" Font-Size="9pt" Text="Completed Date/Time"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblCompletedValue" runat="server" Font-Size="9pt" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                        
                        <td width="15%">
                            <asp:Label ID="lblConfirmed" runat="server" Font-Size="9pt" Text="Confirmed Date/Time"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="15%">
                            <asp:Label ID="lblConfirmedValue" runat="server" Font-Size="9pt" Width="140px" CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                        <td width="15%">
                        </td>
                        <td width="15%">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <%--<table width="90%">
                <tr>
                    <td style="width: 45%;" align="left">
                        
                    </td>
                    <td style="width: 30%;" align="left">
                       
                    </td>
                    <td style="width: 35%;" align="left">
                    </td>
                </tr>
            </table>--%>
            <div align="center" width="100%">
                <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" BorderWidth="1"
                    CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                    OnRowDataBound="grdTP_RowDataBound" CssClass="mGrid">
                    <HeaderStyle Font-Bold="False" />
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <HeaderStyle BorderWidth="1" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTourPlan" runat="server" Font-Size="9pt" Text='<%#Eval("tour_date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkType" runat="server" Text='<%# Bind("Worktype_Name_B") %>'
                                    Font-Size="9pt"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Distributor name" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="9pt" ItemStyle-BorderWidth="1"  ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkWithSFName" runat="server" Font-Size="9pt" Text='<%# Bind("Worked_With_SF_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Worked With" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lbljointWork" runat="server" Text='<%# Bind("JointWork_Name") %>'
                                    Font-Size="9pt"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Route" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1"  ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblterr1" runat="server" Font-Size="9pt" Text='<%# Bind("Tour_Schedule1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Territory Planned2" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblterr2" runat="server" Font-Size="9pt" Text='<%# Bind("Tour_Schedule2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkType" runat="server" Text='<%# Bind("Worktype_Name_B2") %>'
                                    Font-Size="9pt"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Territory Planned3" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblterr3" runat="server" Font-Size="9pt" Text='<%# Bind("Tour_Schedule3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Objective" ItemStyle-Width="300" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblObjective" runat="server" Font-Size="9pt" Text='<%#Eval("objective")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                        Width="80%" VerticalAlign="Middle" />
                </asp:GridView>
                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                    Width="80%" align="center">
                </asp:Table>
            </div>
        </center>
    </asp:Panel>
    </form>
</body>
</html>
