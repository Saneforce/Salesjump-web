<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Delivery_Order.aspx.cs" Inherits="SuperStockist_Purchase_Invoice_SS_Delivery_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <html xmlns="https://www.w3.org/1999/xhtml">
    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />
    <head>
        <link href="../../../css/style.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
font.head {
    font-style: verdana;
	font-size: 15px;
    
}
table td, table th
{
    margin-left: 40px;
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
td{font-size:12px}
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
#header { height: 15px; width: 100%; background: #19a4c6; text-align: center; color: white; font: bold 15px Helvetica, Sans-Serif; text-decoration: uppercase; letter-spacing: 20px; padding: 8px 0px; }
textarea { border: 0; font: 14px Georgia, Serif; overflow: hidden; resize: none; }
    .auto-style1 {
        height: 24px;
    }
    .auto-style2 {
        height: 50px;
    }


</style>

          
        <script type="text/javascript">

            $(document).ready(function () {

                var AllOrdersdetails = [];
                var AllOrderspro = [];
                var Order = "<%=orderid%>";
                var div = "<%=div%>";

                var sl_no;
                var Orders = [];
                pgNo = 1; PgRecords = 15; TotalPg = 0;
                var AllOrdertotal = [];
                var str = '';
                var calquan = 0;
                var sub = 0;
                var dis = 0;
                var tax = 0;
                var net = 0;
                var free = 0;
                var $i;
                var hiden = $('#<%= HiddenField1.ClientID %>').val();
                var imsgename=''+hiden+'_logo.png';
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "SS_Delivery_Order.aspx/BindgrnOrderDetails",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        AllOrdersdetails = JSON.parse(data.d) || [];
                        for (var a = 0; a < AllOrdersdetails.length; a++) {
                            sl_no = AllOrdersdetails[a].Trans_Sl_No;
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }

                });
            
                loadData();

                function orderdetails() {

                    var strhead = '<div id="head"><table border="0" id="headtable" align="center" style="width: 100%; border-collapse: collapse;"><tbody>';

                    var a = 0;               

                    strhead += '<tr><td class="ShT ShL ShB" style="font-weight:bold;" align="left" colspan="10">' + AllOrdersdetails[a].GRN_Date + '</td><td class="ShT ShR ShB" style="font-weight:bold;">Print DO</td></tr>';

                    strhead += '<tr><td class="ShL ShR ShB" align="center" colspan="2" rowspan="8" style="width: 100px;"><img id="imglog" src="http://fmcg.sanfmcg.com/limg/'+imsgename+'" /></td></tr>';
                    strhead += '<tr><td class="ShL ShR" align="left" colspan="6"><b>' + AllOrdersdetails[a].Supp_Name + ',</b></td><td  class="ShL ShR" align="left" colspan="7" style="font-size:large;" class="auto-style1"><b>DELIVERY ORDER</b></td></tr>';
                    strhead += '<tr><td class="ShL ShR" align="left" colspan="6" >' + AllOrdersdetails[a].address + '.</td><td  class="ShL ShR" align="left" colspan="7" class="auto-style1"><b>REF #:</b>&nbsp;&nbsp;' + AllOrdersdetails[a].Po_No + '</td></tr>';
                    strhead += '<tr  class="ShB"><td class="ShL ShR" align="left" colspan="6" ></td><td  class="ShL ShR" align="left" colspan="7" class="auto-style1"><b>Invoice Date:</b>&nbsp;&nbsp;' + AllOrdersdetails[a].GRN_Date + '</td></tr>';
                    //strhead += '<tr><td class="ShL ShR" align="left" colspan="6" ><b>Ph No : ' + AllOrdersdetails[a].Stockist_Mobile + '</b>&nbsp;&nbsp;</td><td class="ShL ShR" align="left" colspan="7"><b>Invoice Status:</b>&nbsp;&nbsp;<Label></td></tr>';
                    //strhead += '<tr><td class="ShL ShR ShB" align="left" colspan="6"><b>Email:  ' + AllOrdersdetails[a].Sf_Email + '.</b></td><td class="ShL ShR ShB" align="left" colspan="7"><b>Delivery Date:</b>&nbsp;&nbsp;' + AllOrdersdetails[a].Dispatch_Date + '</td></tr></tbody></table></div>';
                    str += strhead;
 
                    var strmiddle = '<div id="middle"><table border="0" align="center" id="middletable" style="width: 100%; border-collapse: collapse;"><tbody>';
                    strmiddle += '<tr><td  class="ShL ShR" align="left"><b>Customer :</b>' + AllOrdersdetails[a].Stockist_Name + '</td></tr>';
                    strmiddle += '<tr><td  class="ShL ShR" align="left"><b>Address :</b>' + AllOrdersdetails[a].Stockist_Address + '</td></tr>';
                    strmiddle += '<tr><td  class="ShL ShR" align="left"><b>Phone No :</b>' + AllOrdersdetails[a].Stockist_Mobile + '</td></tr>';
                    strmiddle += '<tr><td  class="ShL ShR" align="left"><b>Email :</b>' + AllOrdersdetails[a].Sf_Email + '</td></tr></tbody></table></div>';
                    str += strmiddle;
                                  
                   // str += '</page>';
                   // $('#div').append(str);
                }

                function ReloadTable() {
                                    
                    for (x = 0; x < (Orders.length / PgRecords); x++) {
                        str = '';
                        st = PgRecords * (pgNo - 1); slno = 0;
                        str += '<page size="A4" layout="Landscape"><div style="padding:10px;margin-right:45px" id="printableArea' + x + '" class="page">';
                        orderdetails();

                        str += '<div id="product" ><table border="0" id="rptOrders" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;">';
                        str += '<thead><tr><td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Sl.No</b></td><td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Product Name</b></td>';
                        str += '<td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Qty</b></td><td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Check</b></td>';
                       // str += '<td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Total Amount</b></td><td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Free</b></td>';
                       // str += '<td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Discount</b></td><td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Tax</b></td>';
                       // str += '<td class="Sh" align="center" rowspan="2"><font face="calibri"/><b>Taxable Amount</b></td></tr></thead>';
                        str += '</tr></thead><tbody>';

                        for ($i = st; $i < st + PgRecords; $i++) {

                            if ($i > Orders.length) {
                                str += "<tr><td class='ShL' style='height:20px;' align='center'></td><td></td><td></td><td class='ShR'></td>";
                            } 
                            if ($i < Orders.length) {

                                calquan += parseFloat(Orders[$i].Good);
                                sub += parseFloat(Orders[$i].Net_Value);
                                free += parseFloat(Orders[$i].Free) || 0;
                                dis += parseFloat(Orders[$i].Discount)|| 0;
                                tax += parseFloat(Orders[$i].TaxAmount) || 0;
                                net += parseFloat(Orders[$i].Gross_Value);

                                str += "<tr><td class='Sh' align='center'>" + ($i + 1) + "</td>";
                                str += "<td class='Sh' align='left'>" + Orders[$i].PDetails + "</td>";
                                str += "<td class='Sh' align='right'>" + Orders[$i].Good + "</td>";
                                str += "<td class='Sh' align='right'></td></tr>";
                                //str += "<td class='Sh' align='right'>" + Orders[$i].Net_Value.toFixed(2) + "</td>";
                                //str += "<td class='Sh' align='right'>" + Orders[$i].Damaged + "</td>";
                                //str += "<td class='Sh' align='right'>" + Orders[$i].Damaged.toFixed(2) + "</td>";
                                //str += "<td class='Sh' align='right'>" + Orders[$i].Damaged.toFixed(2) + "</td>";    
                                //str += "<td class='Sh' align='right'>" + Orders[$i].Gross_Value.toFixed(2) + "</td></tr>";

                            }                       
                        }
                        str += '</tbody><tfoot id="tfoot" class="ShT ShB"><tr><td align="center" class="ShL">Total</td><td class="ShT ShB ShR"></td><td align="right" style="visible:hidden;" class="ShT ShB ShR">' + calquan + '</td><td align="right" class="ShT ShB ShR"></td></tr ></tfoot ></table ></div >';
                            //<td align="right" class="ShT ShB ShR">' + sub.toFixed(2) + '</td>< td align = "right" class="ShT ShB ShR" > ' + free + '</td > <td align="right" class="ShT ShB ShR">' + dis.toFixed(2) + '</td> <td align="rig/*ht" class="ShT ShB ShR">' + tax.toFixed(2) + '</td> <td class="ShR" align="right"></td></tr ></tfoot ></table ></div >';                       
                       // totaltable();   
                        str += '<div style="padding:15px 0px 0px 0px;"><table id="dis" style="width: 200%;"><tbody><tr><td>Thank you for your Business !</td></tr><tr><td>Issued by:</td><td></td><td>Accepted by :</td></tr>';
                        str += '<tr><td><img id="imglog" src=".../limg/"' + AllOrdersdetails[0].Supp_Name + '"_logo.png" /></td><td></td></tr><tr><td class="ShB" style="width: 20%;padding: 2%;"></td><td style="width: 20%;"></td><td class="ShB" style="width: 20%;"></td></tr></tbody></table></div>';
                        str += '</div></page>';
                        $('#div').append(str);
                        pgNo++;
                    }
                }


                
                function loadData() {

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "SS_Delivery_Order.aspx/BindgrnProductdetails",
                        data: "{'Order_ID':'" + sl_no + "'}",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            AllOrderspro = JSON.parse(data.d) || [];
                            Orders = AllOrderspro; ReloadTable();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }



            });
            function webprint() {
                $("#buton").hide();
                  //var originalContents = $("#printableArea0").html();
                  //console.log(originalContents)
            //var printContents = $("#printableArea0").html();
           // $("#printableArea0").html(printContents);
           window.print();
            //$("#printableArea0").html(originalContents);
            return false;
        }

         </script>



        </head>
                <body>

                    <form id="frm" runat="server">
             <asp:HiddenField ID="HiddenField1" runat="Server" />
                        
        <asp:HiddenField ID="hid_order_id" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />
        <asp:HiddenField ID="hid_cust" runat="server" />
                        <div id="buton">
                        <button id="btn_print" class="btn btn primary" style="background-color: #19a4c6;" onclick="webprint()"> Print</button>
                         <button id="btn_back" class="btn btn primary" style="background-color: #ffffff;"><a href="Goods_Received_List.aspx"> Back</a></button>
                        </div>

         <div id="div">
         </div>
                    </form>
                            
                </body>

                </html>
</asp:Content>

