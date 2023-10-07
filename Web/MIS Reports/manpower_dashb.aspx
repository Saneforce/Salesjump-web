<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manpower_dashb.aspx.cs" Inherits="MIS_Reports_manpower_dashb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dashboard Details</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style>
         #grid, #grid1,#grid2,#grid3 {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
            overflow:scroll;
        }

        th {
            position: sticky;
            top: 0;
            background: #6c7ae0;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }
            td, table th {
                padding: 5px;
                border: 1px solid #ddd;
                
            }
    </style>
    <script type="text/javascript">
        var day = []; let type = ''; var month = []; var year = []; var unimonth = []; var uniyear = []; var jwmonth = []; var jwyear = [];
        var vsday = []; var vsmonth = []; var vsyear = []; var catmonth = []; var catyear = []; var jwday = [];
        $(document).ready(function () {
            $('#btnExport').click(function () {

                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };
                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };
                htmls = document.getElementById("content").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'ManagerDetails' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
        });
        $(document).ready(function () {
            $('#loadover').show();
            setTimeout(function () {
                $.when(Fillactcmonth(), Fillactcyear(), Fillactcday(), uniactcmnth(), uniactcyear()).then(function () {
                    ReloadTable();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
        });
        $(document).ready(function () {
            $('#loadover1').show();
            setTimeout(function () {
                $.when(getjwmonth(), getjwyear(), Filljwday()).then(function () {
                    ReloadTable1();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover1').hide();
            });
        });
        $(document).ready(function () {
            $('#loadover2').show();
            setTimeout(function () {
                $.when(getvsmonth(), getvsyear(), Fillvsday()).then(function () {
                    ReloadTable2();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover2').hide();
            });
        });
        $(document).ready(function () {
            $('#loadover3').show();
            setTimeout(function () {
                $.when(Fillcatmonth(), Fillcatyear(), Fillcatday()).then(function () {
                    ReloadTable3();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover3').hide();
            });
        });
        function Fillactcday() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/actcday",
                async: false,
                dataType: "json",
                success: function (data) {
                    day = JSON.parse(data.d);
                  
                }
               
            })
        }
        function ReloadTable(){
            $('#grid tbody').html('');
            var str = ''; var str1 = ''; var str2 = '';
            for (var i = 0; i < day.length; i++) {
                str += '<tr><td>Day</td><td>' + day[i].TC + '</td><td>' + day[i].PC + '</td><td>' + day[i].POB_Value + '</td><td></td><td></td></tr>';
            }

            for (var j = 0; j < month.length; j++) {
                str += '<tr><td>Month</td><td>' + month[j].TC + '</td><td>' + month[j].PC + '</td><td>' + month[j].POB_Value + '</td>';

            }
            for (var n = 0; n < unimonth.length; n++) {
                str += '<td>' + unimonth[n].tc + '</td><td>' + unimonth[n].PC + '</td></tr>';
            }
            for (var k = 0; k < year.length; k++) {
                str += '<tr><td>Year</td><td>' + year[k].TC + '</td><td>' + year[k].PC + '</td><td>' + year[k].POB_Value + '</td>';
            }
            for (var m = 0; m < uniyear.length; m++) {
                str += '<td>' + uniyear[m].tc + '</td><td>' + uniyear[m].PC + '</td></tr>';
            }

            $('#grid tbody').append(str);
        }
        function Fillactcmonth() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/actcmonth",
                async: false,
                dataType: "json",
                success: function (data) {
                    month = JSON.parse(data.d);
                }
            })
        }
        function Fillactcyear() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/actcyear",
                async: false,
                dataType: "json",
                success: function (data) {
                    year = JSON.parse(data.d);
                }
            })
        }
        function uniactcmnth() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/uniactcmnth",
                async: false,
                dataType: "json",
                success: function (data) {
                    unimonth = JSON.parse(data.d);
                }
            })
        }
        function uniactcyear() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/uniactcyear",
                async: false,
                dataType: "json",
                success: function (data) {
                    uniyear = JSON.parse(data.d);
                }
            })
        }
        function Filljwday() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/jwday",
                async: false,
                dataType: "json",
                success: function (data) {
                    jwday = JSON.parse(data.d);
                  
                }

            })
        }
        function ReloadTable1() {
            $('#grid1 tbody').html('');
            var str = '';
            for (var i = 0; i < jwday.length; i++) {
                var flitm = jwmonth.filter(function (a) {
                    return a.SF_Code == jwday[i].SF_Code;
                });
                var flity = jwyear.filter(function (a) {
                    return a.SF_Code == jwday[i].SF_Code;
                });
                str += '<tr><td>' + jwday[i].SF_Name + '</td><td>' + jwday[i].worked_with_name + '</td><td>' + jwday[i].tc + '</td><td>' + jwday[i].Order_Value + '</td>';
                str += '<td>' + (flitm.length > 0 ? flitm[0].tc : '') + '</td><td>' + (flitm.length > 0 ? flitm[0].Order_Value : '') + '</td><td>' + (flity.length > 0 ? flity[0].tc : '') + '</td><td>' + (flity.length > 0 ? flity[0].Order_Value : '') + '</td></tr>'
            }

            $('#grid1 tbody').append(str);
        }
        function getjwmonth() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/jwmonth",
                async: false,
                dataType: "json",
                success: function (data) {
                   jwmonth = JSON.parse(data.d);
                }
            })
        }
        function getjwyear() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/jwyear",
                async: false,
                dataType: "json",
                success: function (data) {
                   jwyear = JSON.parse(data.d);
                }
            })
        }
        function Fillvsday() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/vsday",
                async: false,
                dataType: "json",
                success: function (data) {
                    vsday = JSON.parse(data.d);
                  }

            })
        }
        function ReloadTable2() {
            $('#grid2 tbody').html('');
            var str = '';

            str += '<tr><td>No Of Vantours</td><td>' + (vsday.length > 0 ? vsday[0].cnt : '') + '</td><td>' + (vsmonth.length > 0 ? vsmonth[0].cnt : '') + '</td><td>' + (vsyear.length > 0 ? vsyear[0].cnt : '') + '</td></tr>';
            str += '<tr><td>Vantour Sales</td><td>' + (vsday.length > 0 ? vsday[0].Order_Value : '') + '</td><td>' + (vsmonth.length > 0 ? vsmonth[0].Order_Value : '') + '</td><td>' + (vsyear.length > 0 ? vsyear[0].Order_Value : '') + '</td></tr>';
            $('#grid2 tbody').append(str);
        }
        function getvsmonth() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/vsmonth",
                async: false,
                dataType: "json",
                success: function (data) {
                    vsmonth = JSON.parse(data.d);
                }
            })
        }
        function getvsyear() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/vsyear",
                async: false,
                dataType: "json",
                success: function (data) {
                    vsyear = JSON.parse(data.d);
                }
            })
        }
        function Fillcatday() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/catday",
                async: false,
                dataType: "json",
                success: function (data) {
                    catday = JSON.parse(data.d);
                 }

            })
        }
        function ReloadTable3() {
            $('#grid3 tbody').html('');
            var str = ''; var str1 = ''; var str2 = '';
            for (var i = 0; i < catyear.length; i++) {
                var flitcd = catday.filter(function (a) {
                    return a.Product_Cat_Code == catyear[i].Product_Cat_Code && a.Product_Sale_Unit == catyear[i].Product_Sale_Unit;
                });
                var flitcm = catmonth.filter(function (a) {
                    return a.Product_Cat_Code == catyear[i].Product_Cat_Code && a.Product_Sale_Unit == catyear[i].Product_Sale_Unit;
                });
                str += '<tr><td>' + catyear[i].Product_Cat_Name + '</td><td>' + catyear[i].Product_Sale_Unit + '</td><td>' + (flitcd.length > 0 ? flitcd[0].quantity : '') + '</td> ' +
                    ' <td>' + (flitcd.length > 0 ? flitcd[0].value : '') + '</td><td>' + (flitcm.length > 0 ? flitcm[0].quantity : '') + '</td><td>' + (flitcm.length > 0 ? flitcm[0].value : '') + '</td>' +
                    '<td>' + catyear[i].quantity + '</td><td>' + catyear[i].value + '</td></tr>';
            }

            $('#grid3 tbody').append(str);
        }
        function Fillcatmonth() {
             $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/catmonth",
                 async: false,
                dataType: "json",
                success: function (data) {
                    catmonth = JSON.parse(data.d);
                }
            })
        }
        function Fillcatyear() {
             $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "manpower_dashb.aspx/catyear",
                 async: false,
                dataType: "json",
                success: function (data) {
                    catyear = JSON.parse(data.d);
                }
            })
        }
   
    </script>
        </head>
<body>
    <form id="form1" runat="server">
         <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
         <div id="content">
        <div class="card">
       <div class="card-body table-responsive" style="overflow-x: auto;">
            <br />
        <asp:Label ID="lblHead" SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 

        <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" Text="Calls" runat="server"></asp:Label>
                       </b>
                </div>
       
         
        <table id="grid" class="grids">
                <thead>
                    <tr>
                        <th></th>
                        <th>Total Calls</th>
                        <th>Productive Calls</th>
                        <th>Order Value</th>
                        <th>UNIQUE  Calls</th>
                        <th>UNIQUE Productive Calls</th>
                       </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table>
             <div class="overlay" id="loadover" style="display: none;">
            <div id="loader">Please Wait...</div>
        </div><br />
                     <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="Label1" Text="Joint Work" runat="server"></asp:Label>
                       </b>
                </div>
                <table id="grid1" class="grids">
                <thead>
                    <tr>
                        <th rowspan="2">Fieldforce Name</th>
                        <th rowspan="2">Worked With name</th>
                        <th colspan="2">Day</th>
                        <th colspan="2">Month</th>
                        <th colspan="2">Year</th>
                        </tr>
                      <tr>
                        <th>Call</th>
                        <th>Ordervalue</th>
                        <th>Call</th>
                        <th>Ordervalue</th>
                        <th>Call</th>
                        <th>Ordervalue</th>
                     </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table> 
           <div class="overlay" id="loadover1" style="display: none;">
            <div id="loader1">Please Wait...</div>
        </div><br />
            <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="Label2" Text="VANTOUR" runat="server"></asp:Label>
                       </b>
                </div>
                <table id="grid2" class="grids">
                <thead>
                    <tr>
                        <th>VTS</th>
                        <th>Today</th>
                        <th>Month</th>
                        <th>Year</th>
                         </tr>
                      
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table>
           <div class="overlay" id="loadover2" style="display: none;">
            <div id="loader2">Please Wait...</div>
        </div><br />
         <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="Label3" Text="PRODUCTS" runat="server"></asp:Label>
                       </b>
                </div>
                <table id="grid3" class="grids">
                <thead>
                    <tr>
                        <th rowspan="2">Category</th>
                        <th rowspan="2">Units</th>
                        <th colspan="2">Day</th>
                        <th colspan="2">Month</th>
                        <th colspan="2">Year</th>
                        </tr>
                      <tr>
                        <th>Qty</th>
                        <th>Value</th>
                        <th>Qty</th>
                        <th>Value</th>
                        <th>Qty</th>
                        <th>Value</th>
                     </tr>
                      
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table><div class="overlay" id="loadover3" style="display: none;">
            <div id="loader3">Please Wait...</div>
        </div><br />

       </div></div></div>
    </form>
</body>
</html>
