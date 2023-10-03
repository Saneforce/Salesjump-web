<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_Event_Captures_View.aspx.cs" Inherits="MasterFiles_Reports_tsr_Rpt_DCR_Event_Captures_View" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Captures Report</title>
    
    <link type="text/css" rel="Stylesheet" href="../../../css/Report.css" />
    <link href="../../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
     <link href="../../../css/style.css" rel="stylesheet" />

    <style type="text/css">
    
    #GridView1 td 
    {
        padding : 5px 5px;
    }
    </style>
       
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
            <asp:Panel ID="pnlbutton" runat="server">
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
                                            OnClick="btnPDF_Click" Visible="false" />
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
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>                 
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333"  class="newStly"  GridLines="Both"  
                                AutoGenerateColumns="false" > 
                                <AlternatingRowStyle BackColor="White" /> 
                                <Columns> 
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1" 
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" visible="false">                    
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("dt")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fieldforce Name">                     
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Sf_Name")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Name" >                    
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("ClstrName")%>'  ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Submission Date" >                    
                                        <ItemTemplate>                                             
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("dt")%>' ></asp:Label> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Code" >                     
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("retailor_code")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Name" >                     
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("retailor_name")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor Name" > 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Distributor")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title" >                    
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("title")%>'></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="Capture Time" >                    
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("InsertedTime")%>'></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Identification" >                     
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Identification")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="POP Count" >                     
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("PopCount")%>' ></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" > 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("remarks")%>' Width="150"></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image URL"> 
                                        <ItemTemplate> 
                                            <a id="shrflink" href='<%# Eval("events","http://tiesar.salesjump.in/photos/{0}") %>' ><%# Eval("events","http://tiesar.salesjump.in/photos/{0}") %> </a>
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">                      
                                        <ItemTemplate>                  
                                            <img alt="" id="img12" width="140" height="140"  src='<%# Eval("events","http://tiesar.salesjump.in/photos/{0}") %>' runat="server" />  
                                            <%--  <asp:Image ID="Image1" runat="server" Width="150" Height="150"  ImageUrl='<%# Eval("events","http://tiesar.salesjump.in/photos/{0}") %>'/>--%> </a>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Image Capture Date">                      
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("Events_Date")%>' Width="150"></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" /> 
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /> 
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /> 
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /> 
                                <RowStyle BackColor="#EFF3FB" height="150" /> 
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> 
                                <SortedAscendingCellStyle BackColor="#F5F7FB" /> 
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" /> 
                                <SortedDescendingCellStyle BackColor="#E9EBEF" /> 
                                <SortedDescendingHeaderStyle BackColor="#4870BE" /> 
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
                <br />
            </center>
        </div>
    </form>
</body>
</html>