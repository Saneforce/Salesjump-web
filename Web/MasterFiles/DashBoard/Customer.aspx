<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Customer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State Master View Details</title>
    <style type="text/css">
        table
        {
            border-collapse: collapse;
        }
        
        table, td, th
        {
            border: 1px solid;
            border-color: #999999;
            border-bottom: 0.1em solid #bbb;
        }
    </style>
    <script type="text/javascript">
        function RefreshParent() {
           // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            window.print();
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server" BorderColor="#009999" 
            BorderStyle="Solid" BorderWidth="1px">
            <table width="100%">
                <tr>
              
                    <td width="90%" align="center" style="border: thin dotted #00CCFF" >
                    <asp:Label ID="Label1" Text="State Master View Details" 
                            Font-Bold="True"  Font-Underline="True"
                runat="server" Font-Names="Andalus" Font-Size="Large"></asp:Label>
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
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div align="left">
            <h1>
                <asp:Label ID="lblHead" Font-Underline="True" Font-Bold="True" runat="server" 
                    Font-Size="Large"></asp:Label></h1>
        </div>
        <br />
        <asp:GridView ID="GridView1" runat="server" align="center"  
        OnDataBound="OnDataBound" Width="98%"
            HorizontalAlign="Center" 
            EmptyDataText="No Records Found" CssClass="mGrid" 
        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
        AlternatingRowStyle-BackColor="#d6e9c6" HeaderStyle-BackColor="#0097AC" 
        BorderColor="#009999" BorderStyle="Solid" BorderWidth="2px">
            
<AlternatingRowStyle BackColor="#D6E9C6" CssClass="alt"></AlternatingRowStyle>

<HeaderStyle BackColor="#0097AC"></HeaderStyle>

<PagerStyle CssClass="pgr"></PagerStyle>
            
        </asp:GridView>
        
    </asp:Panel>
    </form>
</body>
</html>
