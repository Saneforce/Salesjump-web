<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="rpt_Retailer_Distributor_Wise.aspx.cs"
    Inherits="MIS_Reports_rpt_Retailer_Distributor_Wise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Values</title>
     <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
     <link href="../css/style.css" rel="stylesheet" />   
    <style type="text/css">
        #DGVFFO th
        {
            text-align: center;
        }
        #DGVFFO td
        {
            padding: 4px 4px;
        }
        #DGVFFO td:nth-child(3), #DGVFFO td:nth-child(4)
        {
            text-align: left;
        }
    </style>
<script language="Javascript">
         function RefreshParent() {
           
             window.close();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <br />
        <asp:Label ID="lblHead" Text="Retailer Field Force Wise" SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 
<div style="text-align:right; width:95%">

 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
       <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                            

                       
                                  <%--  <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                               
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                               
<asp:Button ID="btnExport" runat="server" Text="PDF"  Font-Names="Verdana" Font-Size="10px"  BorderWidth="1" Height="25px" Width="60px"
                                        BorderColor="Black" BorderStyle="Solid" onclick="btnExport_Click" />

                               
                                
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" />--%>
                           

</div>
       
        <asp:GridView ID="DGVFFO" runat="server" Style="border-collapse: collapse; border: solid 1px Black;"
           Width="95%" CssClass="newStly">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
