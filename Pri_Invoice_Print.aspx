<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Pri_Invoice_Print.aspx.cs" Inherits="Pri_Invoice_Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />
    <head>
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
        <style type="text/css">label {
   width: 200px;
   display: inline-block;
   text-align: right;
}
font.head {
    font-style: verdana;
	font-size: 15px;
    
}

font.print {
   font-family:arial;
	font-size:0.5cm;
    
}
font.print1 {
   font-family:arial;
	font-size:0.4cm;
    
}
input[type="text"]
{
     font-family:arial;
	font-size:0.6cm;
}
#pg{
	display:block;
	background:#ffffff;
	height:30.54cm;
	width:31.89cm;	
	padding:0.5cm 1.7cm;
	text-align:left;
	overflow:hidden;
	margin-top: 0px; margin-bottom: 0px; margin-left: 0px; margin-right: 0px;
	
}
#footer
  {position: fixed;  bottom: 0px; width: 80%;margin: auto;}
p {
    -webkit-transform-origin: 0 0;
    -moz-transform-origin: 0 0;
}
p.doubleWidth {
    -webkit-transform: scaleX(2);
    -moz-transform: scaleX(2);
}
p.doubleHeight {
    -webkit-transform: scaleY(2);
    -moz-transform: scaleY(2);
}
p.doubleWidthandHeight {
    -webkit-transform: scaleX(1) scaleY(1);
    -moz-transform: scaleX(1) scaleY(1);
}
THEAD{display: table-header-group}
td{font-size:14px}
<%--SPAN{
    cOunter-increment: page;
}
SPAN:after{
    content: counter(page);
}--%>
@page :right {
  @top-right {
    content: string(header, first); 
  }
}


.Sh{border:solid 1px black;}
.ShR{border-right:solid 1px black;}
.ShL{border-left:solid 1px black;}
.ShT{border-top:solid 1px black;}
.ShB{border-bottom:solid 1px black;}
body {
  background: rgb(204,204,204); 
}
page {
  background: white;
  display: block;
  margin: 0 auto;
  margin-bottom: 0.5cm;
  box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
}
page[size="A4"] {  
  width: 20cm;
  height: 29.7cm; 
}
page[size="A4"][layout="portrait"] {
  width: 29.7cm;
	width: 27cm;
  height: 21cm;  
}
page[size="A3"] {
  width: 29.7cm;
  height: 42cm;
}
page[size="A3"][layout="portrait"] {
  width: 42cm;
  height: 29.7cm;  
}
page[size="A5"] {
  width: 14.8cm;
  height: 21cm;
}
page[size="A5"][layout="portrait"] {
  width: 21cm;
  height: 14.8cm;  
}
@media print {
  body, page {
    margin: 0;
    box-shadow: 0;
  }
}
div.page
      {
        page-break-after: always;
        page-break-inside: avoid;
      }
#header { height: 15px; width: 100%; margin: 20px 0; background: #222; text-align: center; color: white; font: bold 15px Helvetica, Sans-Serif; text-decoration: uppercase; letter-spacing: 20px; padding: 8px 0px; }
textarea { border: 0; font: 14px Georgia, Serif; overflow: hidden; resize: none; }
</style>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            $(document).on('click', "#btnprint", function () {
                var originalContents = $("body").html();
                var printContents = $("#printableArea").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;

            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <a id="btnprint" class="btn btn-primary" style="vertical-align: middle; font-size: 14px;">
            <span>Print</span></a>

        <page size="A4" layout="portrait" />

        <div id="printableArea" class="page">
            <table border="0" align="center" style="width: 98%; border-collapse: collapse; bordercolor=blue;">
                <tr>
                    <td class="Sh" align="center" colspan="15">
                        <textarea id="header" style="height: 32px;">INVOICE</textarea>
                    </td>
                </tr>
                <tr>
                    <td class="ShB ShL" align="center" colspan="2" rowspan="8" style="width:100px;">
                        <img src="limg/taurus_logo.png" />
                    </td>
                   
                </tr>
                <tr>
                    <td class="ShL" align="left" colspan="6">
                        <%--<b><font size="4" face="calibri">Address of the Consignor</font></b>--%>
                        <asp:Label ID="Lab_cmp_name" runat="server" Text="" Style="font-size: 18px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="7">
                        <b><font face="calibri">&nbsp;</font></b>
                    </td>
                </tr>
                <%--	<tr><td class="ShL" align="left" colspan="6"><b><font face="calibri">CIN&nbsp;:&nbsp;U24231TN1938PLC002883</font></b></td><td class="ShL ShR" align="left" colspan="7"><b><font face="calibri">&nbsp;</font></b></td></tr>
		<tr><td class="ShL" align="left" colspan="6"><b><font face="calibri">PAN&nbsp;:&nbsp;AAACT7987L</font></b></td><td class="ShL ShR" align="left" colspan="7"><b><font face="calibri">Transport&nbsp;:&nbsp;FIRST FLIGHT</font></b></td></tr>
		<tr><td class="ShL" align="left" colspan="6"><b><font face="calibri">GSTIN of Consignor&nbsp;:&nbsp;33AAACT7987L1ZP</font></b></td><td class="ShL ShR" align="left" colspan="7"><b><font face="calibri">Destination&nbsp;:&nbsp;AHMEDABAD</font></b></td></tr>--%>
                <tr>
                    <td class="ShL" align="left" colspan="6">
                        <b><font face="calibri">Address of the Consignor</font></b>
                    </td>
                    <td class="ShL ShR" align="left" colspan="7">
                        <%-- <b><font face="calibri">Invoice No&nbsp;:&nbsp;INV00001</font></b>--%>
                        <asp:Label ID="Lab_inv" runat="server" Text="Invoice No:" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_invno" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ShL" align="left" colspan="6" style="width:395px;">
                        <%-- <b><font face="calibri">Unit 4f3 Pearl Plaza Building, Pasig, 1600,Philipines-678</font></b>--%>
                        <asp:Label ID="Lab_add" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="7">
                        <%--<b><font face="calibri">Date&nbsp;:&nbsp;10/05/2018</font></b>--%>
                        <asp:Label ID="Lab_to_date" runat="server" Text="Date:" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_date" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ShL" align="left" colspan="6">
                        <%--<b><font face="calibri">GST NO.33AAACT7987L1ZP-</font></b>--%>
                        <asp:Label ID="Lab_gst" runat="server" Text="GST NO:" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_gstno" runat="server" Text="33AAACT7987L1ZP" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="7">
                        <%--<b><font face="calibri">Inv. Date&nbsp;:&nbsp;10/05/2018</font></b>--%>
                        <asp:Label ID="Lab_inv_d" runat="server" Text="Inv.Date:" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_inv_date" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ShL" align="left" colspan="6">
                        <%--<b><font face="calibri">Philipines</font></b>--%>
                        <asp:Label ID="Lab_city" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="7">
                        <%--<b><font face="calibri">Order Date&nbsp;:&nbsp;10/05/2018</font></b>--%>
                        <asp:Label ID="Lab_Order_Date" runat="server" Text="Order Date:" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_or_date" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <%-- <td class="ShL" align="left" colspan="6">
                        <%--<b><font face="calibri">Lic No&nbsp;:&nbsp;2666/MZ1/20B, 2614/MZ1/21B</font></b>
                        <asp:Label ID="Lab_lic" runat="server" Text="Lic No&nbsp;:&nbsp;" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_Licno" runat="server" Text="2666/MZ1/20B, 2614/MZ1/21B" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                    </td>--%>
                    <td class="ShL ShR" align="left" colspan="7">
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShB" align="left" colspan="6">
                    </td>
                    <td class="ShL ShR ShB" align="left" colspan="7">
                        <b><font face="calibri">&nbsp;</font></b>
                    </td>
                </tr>
            </table>
            <table border="0" align="center" style="width: 98%; border-collapse: collapse; bordercolor=blue;">
                <tr>
                    <td class="ShL ShR ShT" align="left" colspan="2">
                        <%--<font face="calibri">Invoice To&nbsp;:&nbsp;SHARMA RAJESHKUMAR KAMALESHBHAI--%>
                        <asp:Label ID="inv_t" runat="server" Text="Invoice To:" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                        <asp:Label ID="inv_to" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left">
                        <%-- <font face="calibri">Ship To&nbsp;:&nbsp;SHARMA RAJESHKUMAR KAMALESHBHAI--%>
                        <asp:Label ID="ship_t" runat="server" Text="Ship To:" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                        <asp:Label ID="ship_to" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="2">
                        <font face="calibri">
                    </td>
                    <td class="ShL ShR" align="left">
                        <font face="calibri">
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="2">
                        <%-- <font face="calibri">31 KALGI PARK SOCIETY--%>
                        <asp:Label ID="Lab_i_add" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="2">
                        <%--<font face="calibri">31 KALGI PARK SOCIETY--%>
                        <asp:Label ID="Lab_s_add" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="2">
                        <%-- <b><font face="calibri">SalesPerson Name&nbsp;:&nbsp;YADHAVI PET SHOP( MR MURALI
                        YADAV)</b>--%>
                        <asp:Label ID="Lab_sal_name" runat="server" Text="SalesPerson Name:" Style="font-size: 14px;
                            font-family: calibri; font-weight: bold;"></asp:Label>
                        <asp:Label ID="Lab_saleman" runat="server" Text="" Style="font-size: 14px; font-family: calibri;
                            font-weight: bold;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="2">
                        <%-- <font face="calibri"><font face="calibri">Payment Term&nbsp;:&nbsp;credit and debit
                            card payments&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Payment Due&nbsp;:&nbsp;30/05/2018--%>
                        <asp:Label ID="Lab_pt" runat="server" Text="Payment Term:" Style="font-size: 14px;
                            font-family: calibri;"></asp:Label>
                        <asp:Label ID="Lab_pay_term" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Lab_pd" runat="server" Text="Payment Due:" Style="font-size: 14px;
                            font-family: calibri;"></asp:Label>
                        <asp:Label ID="Lab_pay_due" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="2">
                        <%--<font
                                face="calibri">Shipping Method&nbsp;:&nbsp;Van&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <font face="calibri">Shipping Term&nbsp;:&nbsp;1 Month&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Delivery
                        Date:&nbsp;:&nbsp;30/06/2018--%>
                        <asp:Label ID="Lab_sm" runat="server" Text="Shipping Method:" Style="font-size: 14px;
                            font-family: calibri;"></asp:Label>
                        <asp:Label ID="Lab_Sh_mt" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Lab_st" runat="server" Text="Shipping Term:" Style="font-size: 14px;
                            font-family: calibri;"></asp:Label>
                        <asp:Label ID="Lab_sh_term" runat="server" Text="" Style="font-size: 14px; font-family: calibri;"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Lab_del_date" runat="server" Text="Delivery Date:" Style="font-size: 14px;
                            font-family: calibri;"></asp:Label>
                        <asp:Label ID="Lab_delivery_date" runat="server" Text="" Style="font-size: 14px;
                            font-family: calibri;"></asp:Label>
                    </td>
                    <td class="ShL ShR" align="left" colspan="2">
                        <font face="calibri">
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR ShB" align="left" colspan="2">
                    </td>
                    <td class="ShL ShR ShB" align="left" colspan="2">
                        <font face="calibri">
                    </td>
                </tr>
            </table>
            <br>
            <table border="0" align="center" bordercolor="blue" style="width: 98%; border-collapse: collapse">
                <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptOrders_ItemDataBound1">
                    <HeaderTemplate>
                        <tr>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>S.No</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Item </b>
                            </td>
                            <%--<td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Description</b>
                            </td>--%>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Unit Price</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Qty(Pcs.)</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Free</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Gross Value</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Tax</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Discount</b>
                            </td>
                            <td class="Sh" align="center" rowspan="2">
                                <font face="calibri"><b>Net Value</b>
                            </td>
                            <tr>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Sh" align="center">
                                <font face="calibri">
                                    <%# Container.ItemIndex + 1 %>
                            </td>
                            <td class="Sh" align="left">
                                <font face="calibri">
                                    <%# Eval("Product_Name")%>
                            </td>
                          <%--  <td class="Sh" align="left">
                                <font face="calibri">
                                    <%# Eval("Product_Description")%>
                            </td>--%>
                            <td class="Sh" align="left">
                                <font face="calibri">
                                    <%# Eval("Price")%>
                            </td>
                            <td class="Sh" align="left">
                                <font face="calibri">
                                    <%# Eval("Quantity")%>
                            </td>
                            <td class="Sh" align="left">
                                <font face="calibri">
                                    <%# Eval("Free")%>
                            </td>
                            <td class="Sh" align="right">
                                <font face="calibri">
                                    <%# Eval("Amount")%>
                            </td>
                            <td class="Sh" align="right">
                                <font face="calibri">
                                    <%# Eval("TaxAmount")%>
                            </td>
                            <td class="Sh" align="right">
                                <font face="calibri">
                                    <%# Eval("Discount")%>
                            </td>
                            <%--<td class="Sh" align="right"><font face="calibri">0</td>--%>
                            <%--<td class="Sh" align="right"><font face="calibri"></td>--%>
                            <%--<td class="Sh" align="right"><font face="calibri"></td>--%>
                            <td class="Sh" align="right">
                                <font face="calibri">
                                    <%# Eval("net_val")%>
                            </td>
                            <%--<td class="Sh" align="right"><font face="calibri">0.00</td>--%>
                        </tr>
                        <asp:Repeater ID="rptnext" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="Sh" align="left" colspan="4">
                                    </td>
                                    <td class="Sh" align="left" colspan="4">
                                        Sub Total
                                    </td>
                                    <td class="Sh" align="right">
                                        <%# Eval("Sub_Total")%>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="Sh" align="left" colspan="4">
                                    &nbsp;
                                </td>
                                <td class="Sh" align="left" colspan="5">
                                    CGST Total
                                </td>
                                <td class="Sh" align="right">
                                    0.00
                                </td>
                            </tr>
                            <tr>
                                <td class="Sh" align="left" colspan="4">
                                    &nbsp;
                                </td>
                                <td class="Sh" align="left" colspan="5">
                                    SGST/UTGST Total
                                </td>
                                <td class="Sh" align="right">
                                    0.00
                                </td>
                            </tr>
                            <tr>
                                <td class="Sh" align="left" colspan="4">
                                    &nbsp;
                                </td>
                                <td class="Sh" align="left" colspan="5">
                                    IGST Total
                                </td>
                                <td class="Sh" align="right">
                                    0.00
                                </td>
                            </tr>--%>
                                <tr>
                                    <td class="Sh" align="left" colspan="4">
                                        &nbsp;
                                    </td>
                                    <td class="Sh" align="left" colspan="4">
                                        Tax Total
                                    </td>
                                    <td class="Sh" align="right">
                                        <%# Eval("Tax_Total")%>
                                    </td>
                                </tr>
                            <%--    <tr>
                                    <td class="Sh" align="left" colspan="4">
                                        &nbsp;
                                    </td>
                                    <td class="Sh" align="left" colspan="4">
                                        Discount
                                    </td>
                                    <td class="Sh" align="right">
                                        <%# Eval("Dis_total")%>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="Sh" align="left" colspan="4">
                                        &nbsp;
                                    </td>
                                    <td class="Sh" align="left" colspan="4">
                                        <b>Total</b>
                                    </td>
                                    <td class="Sh" align="right">
                                        <%# Eval("Total")%>
                                    </td>
                                    <tr>
                                        <td class="Sh" align="left" colspan="4">
                                            &nbsp;
                                        </td>
                                        <td class="Sh" align="left" colspan="4">
                                            <b>Advanced Paid</b>
                                        </td>
                                        <td class="Sh" align="right">
                                            <%# Eval("Adv_Paid")%>
                                        </td>
                                        <tr>
                                            <td class="Sh" align="left" colspan="4">
                                                &nbsp;
                                            </td>
                                            <td class="Sh" align="left" colspan="4">
                                                <b>Net Amount</b>
                                            </td>
                                            <td class="Sh" align="right">
                                                <%# Eval("Amt_Due")%>
                                            </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <br>
            <table border="0" align="center" bordercolor="blue" style="width: 98%; border-collapse: collapse">
                <tr>
                    <td class="ShL ShT ShR" align="left" colspan="15">
                        Remarks&nbsp;:&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShB ShR" align="center" colspan="15">
                        <font face="calibri"><b><b>
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR ShT" align="left" colspan="7">
                        TERMS (If any)
                    </td>
                    <td class="ShL ShR" align="right" colspan="8">
                        <b></b>
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="7">
                        &nbsp;
                    </td>
                    <td class="ShL ShR" align="left" colspan="8">
                        <br>
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR" align="left" colspan="7">
                        &nbsp;
                    </td>
                    <td class="ShL ShR" align="left" colspan="8">
                        <br>
                    </td>
                </tr>
                <tr>
                    <td class="ShL ShR ShB" align="left" colspan="7">
                        <font size="1">Whether tax is payable on reverse charge basis - "NO"<br>
                            Amount of tax to be paid under reverse charge - "NOT APPLICABLE"</font>
                    </td>
                    <td class="ShL ShR ShB" align="right" colspan="8">
                        <b>Authorised Signatory</b>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </div>
        </page>
        </form>
    </body>
    </html>
</asp:Content>
        
       
