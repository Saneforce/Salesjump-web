<%@ Page Language="C#" AutoEventWireup="true" CodeFile="labpharma_doa_view.aspx.cs" Inherits="MIS_Reports_labpharma_doa_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>doareport</title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet"/>
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

        .Holiday {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
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
        }

        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }

        .table td {
            padding: 2px 5px;
            white-space: nowrap;
        }

        .gridviewStyle td {
            border: thin solid #000000;
            text-align: right;
            padding-right: 3px;
            max-width: 300px;
        }

        .gridpager td {
            padding-left: 13px;
            color: cornsilk;
            font-weight: bold;
            text-decoration: none;
            width: 100%;
        }

        [data-val='HELPLINE REQUIRED'] {
            background-color: yellow;
        }

        [data-val='RESOLVED'] {
            background-color: #18eb18;
        }
    </style>

</head>

<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        </div>

        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
         <asp:HiddenField ID="type" runat="server" />
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="DOB/DOA Report" Style="margin-left: 10px; font-size: x-large"></asp:Label>

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
                 <table class="auto-index" id="grid">
                    <thead></thead>
                    <tbody></tbody>
                  </table>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
     <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var sfusers = [];
        var stks = []; var users = [];
        var div_code = '<%=Session["div_code"]%>';
        $(document).ready(function () {
            $('#loadover').show();
            setTimeout(function () {
                $.when(getUsers(), getSFName(), getStockistName()).then(function () {
                    ReloadTable();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
        });

        hidn_sf_code.Value = sf;
        hFMonth.Value = mn;
        subDiv.Value = sdv; 
        type.Value = typs;

        var sf = $('#<%=hidn_sf_code.ClientID%>').val();
        var mn = $('#<%=hFMonth.ClientID%>').val();
        var sdv = $('#<%=subDiv.ClientID%>').val();
        var typs = $('#<%=type.ClientID%>').val();

        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "labpharma_doa_view.aspx/getSFdets",
                data: "{'divcode':'" + div_code + "','sfc':'" + sf + "','mnth':'" + mn + "','typs':'" + typs + "'}",
                dataType: "json",
                success: function (data) {
                 sfusers = JSON.parse(data.d);
                }
            });
        }
        function getSFName() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "labpharma_doa_view.aspx/getSalesforce",
                data: "{'divcode':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    users = JSON.parse(data.d);
                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getStockistName() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "labpharma_doa_view.aspx/getStockist",
                data: "{'divcode':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    stks = JSON.parse(data.d);
                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                }
            });
        }
        
         function ReloadTable() {
            $('#grid tbody').html('');
            var sth = '';
            var str = '';
             sth += "<tr><th>S.NO</th><th>Cutomer Name</th><th>Channel</th><th>DOB</th><th>DOA</th><th>State</th><th>Route Name</th><th>Distributor name</th><th>Field force  Name</th></tr>";
             $('#grid thead').append(sth);
             for (var i = 0; i < sfusers.length; i++) {
                 let sfname = users.filter(function (a) {
                     return ((',' + sfusers[i].SF_Code + ',').indexOf(a.Sf_Code) > 0)
                 }).map(function (el) {
                     return el.Sf_Name
                 }).join(',');
                let stkname = stks.filter(function (a) {
                    return ((',' + sfusers[i].Dist_Name + ',').indexOf(a.Stockist_Code) > 0)
                }).map(function (el) {
                    return el.Stockist_Name
                }).join(',');
                 str += "<tr><td>" + (i + 1) + "</td><td>" + sfusers[i].cusname + "</td><td>" + sfusers[i].chnl + "</td><td>" + ((sfusers[i].dob == null) ? "" : sfusers[i].dob) + "</td><td>" + ((sfusers[i].doa == null) ? "" : sfusers[i].doa) + "</td><td>" + sfusers[i].StateName + "</td><td>" + ((sfusers[i].rte == null) ? "" : sfusers[i].rte) + "</td><td>" + stkname + "</td><td>" + sfname + "</td>";

             }
            $('#grid tbody').append(str);
        }
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
            var tets = 'doareport' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });


</script>
</body>
</html>
