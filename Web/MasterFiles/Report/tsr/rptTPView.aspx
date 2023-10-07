<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTPView.aspx.cs" Inherits="MasterFiles_Temp_rptTPView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TP View Report</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <link href="../../../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    


    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if(<%=div_code%>!="32"){
                $(document).find('.hidecl').hide();
                $(document).find('.hidecll').closest('td').hide();
                $(document).find('.hidecpob').closest('td').hide();
                $(document).find('.hidecsob').closest('td').hide();
                $('#etbl').find('.hidetcl').hide();
                $('#poblbl').hide();
                $('#soblbl').hide();
                $('#totlbl').hide();
            }
            var tpob=0;
            var tsob=0;
            $('.hidecpob').each(function(){
                tpob+=isNaN(parseFloat($(this).text()))?0:parseFloat($(this).text());
            });
            $('.hidecsob').each(function(){
                tsob+=isNaN(parseFloat($(this).text()))?0:parseFloat($(this).text());
            });
            var totlbl=tpob+tsob;
            $('#poblbl').text(":"+tpob);
            $('#soblbl').text(":"+tsob);
            $('#totlbl').text(":"+totlbl);          
            var indx=1;
            if(<%=div_code%>=="32"){loadpobsob();}
            function loadpobsob() {
                $('#grdTP tr').each(function(){
                    var tdpob = parseFloat($(this).find('td').eq(7).text().trim());
                    var tdsob = parseFloat($(this).find('td').eq(8).text().trim());
                    if (isNaN(tdpob)) tdpob = 0;
                    if (isNaN(tdsob)) tdsob = 0;
                    var dadd = parseFloat(tdpob+tdsob);
                    $($(this).find('td')[9]).text(dadd); indx++;
                })
            }
        });
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
            <div id="tblStatus" style="padding-left: 50px;display: none;" runat="server" width="90%" class="common"
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
            <div class="row hidden" style="display: flex;">
                        <table id="etbl" style="width: 25%;white-space:nowrap;margin:0px 0px 0px 74px;">
                            <tbody>
                                <tr>
                                    <td>Employee ID</td>
                                    <td style="padding-left: 29px;"><asp:Label ID="lbleid" runat="server"></asp:Label></td>
                                    <td class="hidetcl" style="padding-left: 40px;">Total POB</td>
                                    <td style="padding-left: 68px;"><label id="poblbl"></label></td></tr>
                                <tr>
                                    <td>Employee Name</td>
                                    <td style="padding-left: 30px;"><asp:Label ID="lblename" runat="server"></asp:Label></td>
                                    <td class="hidetcl" style="padding-left: 40px;">Total SOB</td>
                                    <td style="padding-left: 68px;"><label id="soblbl"></label></td>
                                </tr>
                                <tr>
                                    <td>HeadQuarters</td>
                                    <td style="padding-left: 30px;"><asp:Label ID="lblhqq" runat="server"></asp:Label></td>
                                    <td class="hidetcl" style="padding-left: 40px;">Target</td>
                                    <td style="padding-left: 68px;"><label id="totlbl"></label></td>
                                </tr>
                    <tr>                       
                        <td>
                            <asp:Label ID="lblCompleted" runat="server" Text="Completed Date/Time"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td style="padding-left: 30px;">
                            <asp:Label ID="lblCompletedValue" Font-Bold="true" runat="server" CssClass="TPFontSize"></asp:Label>
                        </td>                        
                        <td style="padding-left: 40px;">
                            <asp:Label ID="lblConfirmed" runat="server" Text="Confirmed Date/Time"
                                CssClass="TPFontSize"></asp:Label>
                        </td>
                        <td style="padding-left: 69px;">
                            <asp:Label ID="lblConfirmedValue" Font-Bold="true" runat="server" Width="140px" CssClass="TPFontSize"></asp:Label>
                        </td>
                    </tr>
                            </tbody>
                        </table>                        
            </div>
            <div style="padding-left: 57px" runat="server" width="25%" class="common" align="left">
                <table align="center">
                </table>
            </div>
            <div align="center" style="width:100%">
                <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" BorderWidth="1"
                    CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                    OnRowDataBound="grdTP_RowDataBound" CssClass="mGrid">
                    <HeaderStyle Font-Bold="False" />
                    <SelectedRowStyle BackColor="BurlyWood" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <HeaderStyle BorderWidth="1" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="20" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="State" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Font-Size="9pt" Text='<%#Eval("StateName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Zone" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblZone" runat="server" Font-Size="9pt" Text='<%#Eval("Zone_name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Area" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblArea" runat="server" Font-Size="9pt" Text='<%#Eval("Area_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblUser" runat="server" Font-Size="9pt" Text='<%#Eval("SF_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HQ" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblHQ" runat="server" Font-Size="9pt" Text='<%#Eval("HQ_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblTourPlan" runat="server" Font-Size="9pt" Text='<%#Eval("tour_date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Distributor name" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="9pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkWithSFName" runat="server" Font-Size="9pt" Text='<%# Bind("Worked_With_SF_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Beat" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblBeat" runat="server" Font-Size="9pt" Text='<%#Eval("Territory")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkType" runat="server" Text='<%# Bind("Worktype_Name_B") %>'
                                    Font-Size="9pt"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Territory Planned1" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblterr1" runat="server" Font-Size="9pt" Text='<%# Bind("Tour_Schedule1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkType" runat="server" Text='<%# Bind("Worktype_Name_B1") %>'
                                    Font-Size="9pt"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Territory Planned2" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblterr2" runat="server" Font-Size="9pt" Text='<%# Bind("Tour_Schedule2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Type" ItemStyle-Width="100" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkType" runat="server" Text='<%# Bind("Worktype_Name_B2") %>'
                                    Font-Size="9pt"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Territory Planned3" ItemStyle-Width="200" HeaderStyle-BorderWidth="1"
                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblterr3" runat="server" Font-Size="9pt" Text='<%# Bind("Tour_Schedule3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="300" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblObjective" runat="server" Font-Size="9pt" Text='<%#Eval("objective")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="hidecl" HeaderText="Retailer Name" ItemStyle-Width="300" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRetailer" CssClass="hidecll" runat="server" Font-Size="9pt" Text='<%#Eval("Retailer_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="hidecl"  HeaderText="POB" ItemStyle-Width="300" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPOB" CssClass="hidecpob" runat="server" Font-Size="9pt" Text='<%#Eval("TPOB")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="hidecl"  HeaderText="SOB" ItemStyle-Width="300" HeaderStyle-BorderWidth="1"
                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSOB" CssClass="hidecsob" runat="server" Font-Size="9pt" Text='<%#Eval("TSOB")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="hidecl" HeaderText="Daywise Total" HeaderStyle-Width="200px" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID='dtotal' CssClass="hidecsob" runat="server" Text='' Width="200"></asp:Label>
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
