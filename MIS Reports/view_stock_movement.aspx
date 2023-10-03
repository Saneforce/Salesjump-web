<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view_stock_movement.aspx.cs" Inherits="MIS_Reports_view_stock_movement" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Return Entry</title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 39px; top: 8px;" src="/img/excel.png" alt="" width="40" height="40" id="btnExportrpt" />
        </div>

        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="lblHead" runat="server" Text="" Style="margin-left: 10px; font-size: x-large"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Distributor Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
               
                <table id="OrderList" class="newStly" style="border-collapse: collapse;">
                   <thead>
                        <tr>
                        <th>Sl.No</th>
                        <th>Product Name</th>
                        <th>Date of Purchase</th>
                        <th>Date of order</th>
                        <th>Stock idle days</th>
                        </tr>
                   </thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                  </table>
            </div>
        </div>
       </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <script type="text/javascript">
        var prod = []; var secord = []; var priord = [];

        function doit(type, fn, dl) {
            var elt = document.getElementById('OrderList');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "Stock_Movement" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ((type || 'xlsx')));
        }
        function getproduct() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_stock_movement.aspx/getproduct",
                     dataType: "json",
                     success: function (data) {
                         prod = JSON.parse(data.d) || [];
                        
                     },
                     error: function (result) {
                         alert(JSON.stringify(result));
                     }
                 });
        } 
        function getsecdtl() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_stock_movement.aspx/getsecdtl",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    secord = JSON.parse(data.d) || [];
                   },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getpridtl() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_stock_movement.aspx/getpridtl",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    priord = JSON.parse(data.d) || [];
                    ReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $('#OrderList tbody').html('');
            $('#OrderList').show();
            var str = '';
            for (var j = 0; j < prod.length; j++) {
                let filsec = secord.filter(function (a) {
                    return  a.Product_Detail_Code == prod[j].Product_Detail_Code;
                });
                let filpri = priord.filter(function (a) {
                    return a.Product_Detail_Code == prod[j].Product_Detail_Code;
                });
                str += '<tr><td>' + (j + 1) + '</td><td>' + prod[j].Product_Detail_Name + '</td><td>' + (filpri.length > 0 ? filpri[0].dtofpurch : '') + '</td><td>' + (filsec.length > 0 ? filsec[0].dateoforder : '') + '</td><td>' + (filsec.length > 0 ? (filsec[0].DateDiff == 0 ? '' :filsec[0].DateDiff) : '') + '</td>';
            
            }
             str += "</tr>";
         
            $('#OrderList tbody').append(str);

        }
      
        $(document).ready(function () {
            $('#btnExportrpt').click(function () {
                doit('biff8', 'Stock_Movement.xls');
            });
            getproduct();
            getsecdtl();
            getpridtl();
          });
   
       
    </script>
</body>
</html>
