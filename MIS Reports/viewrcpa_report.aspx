<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewrcpa_report.aspx.cs" Inherits="MIS_Reports_viewrcpa_report" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml"><head runat="server">
    <title>Competitor Analysis</title>
     <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet"/>
     <link href="../css/style.css" rel="stylesheet" />   
    <style type="text/css">
      #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;     
        }

        th {
          
            top: 0;
            background: #6c7ae0;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

         table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
            font-size: 12px;
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
       
           <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="FieldForce Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>	
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport"><br /> 
      <table class="grids" id="grid"> 
                <thead></thead>
                <tbody></tbody>
         <tfoot></tfoot>
      </table>
    </div>
    </form>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
          var str = "";
          var arr = [];
        var prods = []; var prodqty = [];
          $(document).ready(function () {
              FillProductqty();
              FillProduct();
          });
          function FillProduct() {
             
              $.ajax({
                  type: "Post",
                  contentType: "application/json; charset=utf-8",
                  url: "viewrcpa_report.aspx/Filldtl",
                  async: false,
                  dataType: "json",
                  success: function (data) {
                      $('#grid tbody').html('');
                      prods = JSON.parse(data.d);
                      let oprods = []; let mymap = new Map(); let tt = []; let ss=[];
                      sth = '';
                      sth += "<tr><th>S.NO</th><th>Order Date</th><th>Field Force Name</th><th>Distributor Name</th><th>Route</th><th>Retailer</th><th>Retailer Type</th><th>Product</th><th>Competitor Name</th><th>Competitor ProductName</th><th>Qty</th><th>Order Value</th><th>Rate</th>";
                      str = '';
                      oprods = prodqty.filter(function (el) {
                          const val = mymap.get(el.Product_Code);
                          if (val) {
                              return false;
                          }
                          mymap.set(el.Product_Code, el.Name);
                          return true;
                      }).map(function (a) { return a; }).sort();
                      for (var j = 0; j < oprods.length; j++) {
                          sth += "<th>" + oprods[j].Name + "</th><th>Order Value</th><th>Rate</th>";
                     
                      }
                      sth += "</tr>";
                      for (var i = 0; i < prods.length; i++) {
                          str += "<tr><td>" + (i + 1) + "</td><td>" + prods[i].Activity_Date + "</td><td>" + prods[i].sf_name + "</td><td>" + prods[i].stockist_name + "</td><td>" + prods[i].SDP_Name + "</td><td>" + prods[i].Trans_Detail_Name + "</td><td>" + prods[i].Doc_Spec_ShortName + "</td><td>" + prods[i].Product_Name + "</td><td>" + prods[i].CmptrName + "</td><td>" + prods[i].CmptrBrnd + "</td><td>" + prods[i].Qty + "</td><td>" + prods[i].POB_Value + "</td><td>" + prods[i].Rate + "</td>";
                          for (var k = 0; k < oprods.length; k++) {
                          tt = prodqty.filter(function (a) {
                              return a.Cat_Id == prods[i].Product_Code && a.Product_Code == oprods[k].Product_Code && a.Trans_Sl_No == prods[i].Order_No && a.Order_Date == prods[i].Activity_Date ;
                          });
                        
                              str += "<td>" + (tt.length > 0 ? tt[0].Qty : '') + "</td><td>" + (tt.length > 0 ? tt[0].order_val : '') + "</td><td>" + (tt.length > 0 ? tt[0].Rate : '') + "</td>";
                          }
                             str += "</tr>";
                          }

                      $('#grid thead').append(sth);
                      $('#grid tbody').append(str);
                     }
              });
        }
        function FillProductqty() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "viewrcpa_report.aspx/Fillqtydtl",
                async: false,
                dataType: "json",
                success: function (data) {
                    prodqty = JSON.parse(data.d);
                   
                }
            });
        }
      
          $('#btnExport').click(function () {

              var htmls = "";
              var uri = 'data:application/vnd.ms-excel;base64,';
              var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
              var base64 = function (s) {
                  return window.btoa(unescape(encodeURIComponent(s)))
              };
              var format = function (s, c) {
                  return s.replace(/{(\w+)}/g, function (m, p) {
                      return c[p];
                  })
              };
              htmls = document.getElementById("grid").innerHTML;


              var ctx = {
                  worksheet: 'Worksheet',
                  table: htmls
              }
              var link = document.createElement("a");
              var tets = 'Competitor_Analysis' + '.xls';   //create fname

              link.download = tets;
              link.href = uri + base64(format(template, ctx));
              link.click();
          });
      </script>
</body>
</html>