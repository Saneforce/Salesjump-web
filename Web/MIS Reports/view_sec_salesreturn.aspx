<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view_sec_salesreturn.aspx.cs" Inherits="MIS_Reports_view_sec_salesreturn" %>


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
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
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
                        <th>Field Force Name</th>
                        <th>HQ</th>
                        <th>Retailer Name</th>
                        <th>Distributor Name</th>
                        <th>Product Name</th>
                        <th>Batch no</th>
                        <th>Date</th>
                        <th>Return Type</th>
                        <th>Claim Type</th>
                        <th>Reason</th>
                        <th>Return Qty</th>
                        <th>Return Value</th>
                        </tr>
                   </thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                  </table>
            </div>
        </div>
       
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <script type="text/javascript">
        var tbplan = [], dcrcalls = [], stendtimes = [], rtscount = [], sfusers = [], tpdates = [], dayOrders = [], newRPOB = [];
        var cat = []; var box = [];
        function doit(type, fn, dl) {
            var elt = document.getElementById('OrderList');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "SalesReturnEntry_Primaryorder" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ((type || 'xlsx')));
        }
        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_sec_salesreturn.aspx/getSFdets",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfusers = JSON.parse(data.d) || [];
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
            var qtytot = 0; var valtot = 0;
            for (var j = 0; j < sfusers.length; j++) {
                str += '<tr><td>' + (j + 1) + '</td><td>' + sfusers[j].SF_Name + '</td><td>' + sfusers[j].sf_hq + '</td><td>' + sfusers[j].ListedDr_Name + '</td><td>' + sfusers[j].Stockist_Name + '</td>' +
                    '<td>' + sfusers[j].Product_Detail_Name + '</td><td>' + sfusers[j].damagebatch + '</td><td>' + sfusers[j].damageorderdate + '</td><td>' + sfusers[j].QType + '</td><td>' + sfusers[j].Claim + '</td> ' +
                    '<td>' + sfusers[j].Rem + '</td><td>' + sfusers[j].Ret_Qty + '</td><td>' + sfusers[j].RetValue + '</td>'; 
                qtytot += sfusers[j].Ret_Qty;
                valtot += sfusers[j].RetValue; 
            }
             str += "</tr>";
         
            $('#OrderList tbody').append(str);
            $('#OrderList tfoot').html("<tr><td colspan=11 style='text-align: center;font-weight: bold'>Total  Value</td><td>" + qtytot.toFixed(2) + "</td><td>" + valtot.toFixed(2) + "</td></tr>");

        }
      
        $(document).ready(function () {
            $('#btnExportrpt').click(function () {
                doit('biff8', 'SalesReturnEntry_secondary.xls');
            });
       
            getUsers();
          
          });
   
       
    </script>
</body>
</html>