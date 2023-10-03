<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fieldforcecard.aspx.cs" Inherits="MIS_Reports_fieldforcecard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fieldforce card</title>
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
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Filed Force Score Card" Style="margin-left: 10px; font-size: x-large"></asp:Label>

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
                    <thead>
                    </thead>
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
       
        var tcpob = []; var sfusers = []; var newstr = []; var tlsdcnt = [];   
        var div_code = '<%=Session["div_code"]%>';
        $(document).ready(function () {
            $('#loadover').show();
            setTimeout(function () {
                $.when(gettcpobval(), getnewstore(), getunbilledstore(), getUsers()).then(function () {
                    ReloadTable();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
        });

        hidn_sf_code.Value = sf;
        hFYear.Value = yr;
        hFMonth.Value = mn;
        subDiv.Value = sdv;

        var sf = $('#<%=hidn_sf_code.ClientID%>').val();
        var mn = $('#<%=hFMonth.ClientID%>').val();
        var yr = $('#<%=hFYear.ClientID%>').val();
        var sdv = $('#<%=subDiv.ClientID%>').val();
        function gettcpobval() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "fieldforcecard.aspx/gettcpob",
                data: "{'divcode':'" + div_code + "','sfc':'" + sf + "','mnth':'" + mn + "','year':'" + yr + "','subd':'" + sdv + "'}",
                dataType: "json",
                success: function (data) {
                    tcpob = JSON.parse(data.d);
                }
            });
        }
        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "fieldforcecard.aspx/getSFdets",
                data: "{'divcode':'" + div_code + "','sfc':'" + sf + "','mnth':'" + mn + "','year':'" + yr + "','subd':'" + sdv + "'}",
                dataType: "json",
                success: function (data) {
                   
                    sfusers = JSON.parse(data.d);
                 }
             });
        }
       
        function getnewstore() {
                $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                    async: false,
                url: "fieldforcecard.aspx/getnewstor",
                    data: "{'divcode':'" + div_code + "','sfc':'" + sf + "','mnth':'" + mn + "','year':'" + yr + "','subd':'" + sdv + "'}",
                dataType: "json",
                success: function (data) {
                    newstr = JSON.parse(data.d);
                }
            }); 
        }
        function getunbilledstore() {
                $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                    url: "fieldforcecard.aspx/gettlsdcount",
                    data: "{'divcode':'" + div_code + "','sfc':'" + sf + "','mnth':'" + mn + "','year':'" + yr + "','subd':'" + sdv + "'}",
                dataType: "json",
                success: function (data) {
                    tlsdcnt = JSON.parse(data.d);
                }
            });
        }
        function ReloadTable() {
            $('#grid tbody').html('');
            var sth = '';
            var str = '';
            sth += "<tr><th rowspan='2'>S.NO</th><th rowspan='2'>So/Fo Name</th><th rowspan='2'>Reporting Manager</th><th rowspan='2'>No of Days Avilable</th><th rowspan='2'>No of Days Worked</th><th rowspan='2'>AC</th>";
            sth += "<th rowspan='2'>TC</th><th rowspan='2'>PC</th><th rowspan='2'>%</th><th rowspan='2'>Value</th>";
            sth += "<th rowspan='2'>Unbilled Stores</th><th rowspan='2'>% To AC</th><th rowspan='2'>new Stores</th><th rowspan='2'>Value</th><th rowspan='2'>TLSD</th><th rowspan='2'>No of Ws</th><th rowspan='2'>Value</th><th colspan='5'>No of Stores</th><th colspan='5'>Value</th>";
            sth += "</tr>";
            sth += "<tr><th>>10k</th><th>>5k</th><th>>3k</th><th>>1k</th><th><1k</th><th>>10k</th><th>>5k</th><th>>3k</th><th>>1k</th><th><1k</th></tr>";
            $('#grid thead').append(sth);
            for (var i = 0; i < sfusers.length; i++) {
                let pctcper = 0;
                let lenns = [];
                let totnsval = 0;
                let unbilva = 0;
                let nows = [];
                var totwsval = 0;
                var totshpten = 0;
                var totshpfv = 0;
                var totshpthr = 0;
                var totshpon = 0;
                var totshponle = 0;
                var pertoac = 0;
                str += "<tr colspan=2><td>" + (i + 1) + "</td><td>" + sfusers[i].SF_Name + "</td><td>" + sfusers[i].rsf + "</td><td>" + sfusers[i].Total_Days + "</td><td>" + sfusers[i].wdays + "</td><td>" + sfusers[i].ac + "</td>";
               
                let filtarr = tcpob.filter(function (a) {
                        return a.Sf_Code == sfusers[i].SF_Code;
                    });
                pctcper = (filtarr.length > 0 ? ((filtarr[0].PC / filtarr[0].TC) * 100).toFixed(2) : 0);

                let filtlsd = tlsdcnt.filter(function (a) {
                    return a.Sf_Code == sfusers[i].SF_Code;
                });
                    

                 str += '<td>' + (filtarr.length > 0 ? filtarr[0].TC : 0) + '</td><td>' + (filtarr.length > 0 ? filtarr[0].PC : 0) + '</td><td>' + pctcper +'</td><td>' + (filtarr.length > 0 ? filtarr[0].POB_Value : 0) +'</td>';
                 
                let filtns = newstr.filter(function (a) {
                    return a.Sf_Code == sfusers[i].SF_Code;
                });
               
                
                let shpten = filtns.filter(function (a) {
                    return a.val >= 10000;
                });
                for (var a = 0; a < shpten.length; a++) {
                    totshpten += parseFloat(shpten[a].val);
                }
                let shpfv = filtns.filter(function (a) {
                    return a.val >= 5000 && a.val < 10000;
                });
                for (var b = 0; b < shpfv.length; b++) {
                    totshpfv += parseFloat(shpfv[b].val);
                }
                let shpthr = filtns.filter(function (a) {
                    return a.val >= 3000 && a.val < 5000 ;
                });
                for (var c = 0; c < shpthr.length; c++) {
                    totshpthr += parseFloat(shpthr[c].val);
                }
                let shpon = filtns.filter(function (a) {
                    return a.val >= 1000 && a.val < 3000 ;
                });
                for (var d = 0; d < shpon.length; d++) {
                    totshpon += parseFloat(shpon[d].val);
                }
                let shponle = filtns.filter(function (a) {
                    return a.val < 1000;
                });
                for (var e = 0; e < shponle.length; e++) {
                    totshponle += parseFloat(shponle[e].val);
                }
                 
                unbilva = (sfusers[i].ac - (filtarr.length > 0 ? filtarr[0].PC : 0));
                pertoac = (filtarr.length > 0 ? ((unbilva / sfusers[i].ac) * 100).toFixed(2) : 0);
                nows = filtns.filter(function (a) {
                    return a.Doc_Spec_ShortName == "wholesaler";
                });                
                let mymap = new Map();
                var varea = nows.filter(function (el) {
                    const val = mymap.get(el.Cust_Code);
                    if (val) {
                        return false;
                    }
                    mymap.set(el.Cust_Code, el.Sf_Code);
                    return true;
                }).map(function (a) { return a; });

                lenns = filtns.filter(function (a) {
                    return a.newrt == 1;
                });
                let mymaps = new Map();
                var varnews = lenns.filter(function (el) {
                    const val = mymaps.get(el.Cust_Code);
                    if (val) {
                        return false;
                    }
                    mymaps.set(el.Cust_Code, el.Sf_Code);
                    return true;
                }).map(function (a) { return a; });
                str += '<td>' + unbilva + '</td><td>' + pertoac + '</td><td>' + (varnews.length > 0 ? varnews.length : 0) + '</td>';
                for (var n = 0; n < lenns.length; n++) {
                    totnsval += parseFloat(lenns[n].val);
                }
                for (var s = 0; s < nows.length; s++) {
                    totwsval += parseFloat(nows[s].val);
                }
                str += '<td>' + (totnsval.toFixed(2)) + '</td><td>' + (filtlsd.length > 0 ? filtlsd[0].cnt : 0) + '</td><td>' + (varea.length) + '</td><td>' + (totwsval.toFixed(2)) + '</td>';
                str += '<td>' + (shpten.length) + '</td><td>' + (shpfv.length) + '</td><td>' + (shpthr.length) + '</td><td>' + (shpon.length) + '</td><td>' + (shponle.length) + '</td>';
                str += '<td>' + (totshpten.toFixed(2)) + '</td><td>' + (totshpfv.toFixed(2)) + '</td><td>' + (totshpthr.toFixed(2)) + '</td><td>' + (totshpon.toFixed(2)) + '</td><td>' + (totshponle.toFixed(2)) + '</td>';
                str += "</tr>";
                
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
                var tets = 'fieldforcecard' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });


</script>
</body>
</html>
