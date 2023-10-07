<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view_ffsummary_pcall.aspx.cs" Inherits="MIS_Reports_view_ffsummary_pcall" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Productivity Calls Data</title>
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
                   <thead></thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                  </table>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <script type="text/javascript">
        var tbplan = [], dcrcalls = [], stendtimes = [], rtscount = [], sfusers = [], tpdates = [], dayOrders = [], newRPOB = [];
        var cat = []; var box = [];
        function doit(type, fn, dl) {
            var elt = document.getElementById('OrderList');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "ProductivityCalls_Data" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ((type || 'xlsx')));
        }
        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_ffsummary_pcall.aspx/getSFdets",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfusers = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getDayPlan() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_ffsummary_pcall.aspx/getDayPlan",
                dataType: "json",
                success: function (data) {
                    tbplan = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function prodcat() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_ffsummary_pcall.aspx/getprodcat",
                dataType: "json",
                success: function (data) {
                    cat = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function prodcatbox() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "view_ffsummary_pcall.aspx/getprodcatbox",
                dataType: "json",
                success: function (data) {
                    box = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
            $.when(getUsers(), getDayPlan(), prodcat(), prodcatbox()).then(function () {
                ReloadTable();
            });
        }



        function ReloadTable() {
            $('#OrderList thead').html('');
            $('#OrderList tbody').html('');
            $('#OrderList').show();
            PgRecords = $("#pglim").val();
          
            var sth = '';
            var str = '';
            var bsth = '';
            var csf = '';
            let text = "";
         
            sth += "<tr><th rowspan=2>S.NO</th><th rowspan=2>Date</th><th rowspan=2>Zone Name</th><th rowspan=2>TSI Name</th><th rowspan=2>Beat Name</th><th rowspan=2>Total Calls</th><th rowspan=2>Productivity Calls</th>";

            for (var k = 0; k < cat.length; k++) {
                sth += '<th colspan=2>' + cat[k].Product_Cat_Name + '</th>';
             }
            sth += "</tr>";
            sth += "<tr>";
            for (var k = 0; k < cat.length; k++) {
                sth += "<th>NO Of Shops</th><th>Quantity in Box</th>";
            }
            for (var j = 0; j < sfusers.length; j++) {
                let callsmade = tbplan.filter(function (a) {
                    return a.ActDate == sfusers[j].dt && a.Sf_Code == sfusers[j].sf_Code;
                });
               
             str += '<tr><td>' + (j + 1) + '</td><td>' + sfusers[j].dt + '</td><td>' + sfusers[j].zone + '</td><td>' + sfusers[j].SF_Name + '</td><td>' + sfusers[j].ClstrName + '</td><td>' + ((callsmade.length > 0) ? callsmade[0].TCall : '') + '</td><td>'+ ((callsmade.length > 0) ? callsmade[0].EfCall : '') + '</td>';
                for (var n = 0; n < cat.length; n++) {
                    let boxqty = box.filter(function (a) {
                        return a.Order_date == sfusers[j].dt && a.Sf_Code == sfusers[j].sf_Code && a.Product_Cat_Code == cat[n].Product_Cat_Code;
                    });
                    str += '<td>' + ((boxqty.length > 0) ? boxqty[0].shp : '') + '</td><td>' + ((boxqty.length > 0) ? boxqty[0].box.toFixed(2) : '') + '</td>';
                }
            }
               

            sth += "</tr>";
            str += "</tr>";

            $('#OrderList thead').append(sth);
            $('#OrderList tbody').append(str);
         
        }
      
        $(document).ready(function () {
            $('#btnExportrpt').click(function () {
                doit('biff8', 'ProductivityCalls_Data.xls');
            });
            $('#loadover').show();
            var i = 0;
            getUsers();
            setTimeout(function () {
                loadData();
            }, 1000);
           $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
           
        });
   
       
    </script>
</body>
</html>
