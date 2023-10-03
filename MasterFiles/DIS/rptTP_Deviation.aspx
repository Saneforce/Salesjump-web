<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTP_Deviation.aspx.cs" Inherits="MIS_Reports_rptTP_Deviation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP - Deviation</title>
<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
<link type="text/css" rel="stylesheet" href="../css/Report.css" />
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        
        .remove  
  {
    text-decoration:none;
  }
  
    </style>
</head>
<body>
   <form id="form1" runat="server">
    <div>
    
    <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="TP - Deviation" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
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
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <br />

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <div align="center">
            <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
        </div>
        <div>
                <table width="60%" align="center">
                    <tr>
                    <td width="40%"></td>
                        <td align="left">
                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" SkinID="lblMand" Font-Bold="true" ></asp:Label>
                        </td>
                       <%--
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>--%>
                        
                    </tr>
                </table>
           </div>
            <br />
            <br />
            
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdTP" runat="server" Width="65%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdTP_RowDataBound" 
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  HeaderText="Territory_Code" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_Code" runat="server" Width="120px" Text='<%#Eval("Territory_Code1")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="ASper_Dcr" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblASper_Dcr" runat="server" Width="120px" Text='<%#Eval("ASper_Dcr")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="Date" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Width="80px" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField  HeaderText="Day" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate_Name" runat="server" Width="80px" Text='<%#Eval("Date_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="As Per TP" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsper_Tp" runat="server" Text='<%# Bind("Asper_Tp") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="As Per DCR" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblASper_Dcr_match" runat="server" Text='<%# Bind("ASper_Dcr_match") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField  HeaderText="WorkType_Name" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblworkDCR" runat="server" Width="120px" Text='<%#Eval("WorkType_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="Worktype_Name_B" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblworkTP" runat="server" Width="120px" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField  HeaderText="trans_slno" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrans_sl" runat="server" Width="120px" Text='<%#Eval("trans_slno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                                                         
                            </Columns>
                               <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
    </form>
</body>
</html>


