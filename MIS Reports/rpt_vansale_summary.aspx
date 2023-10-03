<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_vansale_summary.aspx.cs" Inherits="MIS_Reports_rpt_vansale_summary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }


		#van_tbl tbody tr td, #van_tbl tfoot tr th
		{
			text-align:right;
		}

		#van_tbl tbody tr td:nth-child(1)
		{
			text-align:left;
		}
		#van_tbl tbody tr td:nth-child(2)
		{
			text-align:left;
		}
		#van_tbl tbody tr td:nth-child(3)
		{
			text-align:left;
		}
		#van_tbl tfoot tr th:nth-child(1)
		{
			text-align:center;
		}
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });       
    </script>
    <script type="text/javascript">
        //sp_van_collection
        var ptyp = [];
        $(document).ready(function () {
            $(document).on('click', "#btnClose", function () {
                window.close();
            });

            genReport = function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_vansale_summary.aspx/GetDetail",
                    data: "{}",
                    dataType: "json",
                    success: function (data) {
                        Alldata = JSON.parse(data.d) || [];
                        var slno = 1,billamt=0,revamt=0,penamt=0;
                        var strff = '<th style="min-width:250px;" colspan="5"> <p style="margin: 0 0 0px;">Total</p> </th>';
                        for (var i = 0; i < Alldata.length; i++) {
                            str = '<td><p>' + (slno++) + '</p></td><td><p>' + Alldata[i].bill_date + '</p></td><td><p>' + Alldata[i].bill_no + '</p> </td><td><p>' + Alldata[i].Cus_Name + '</p></td><td><p>' + Alldata[i].Territory_Name + '</p></td><td><p>' + Alldata[i].Value + '</p></td><td><p>' + Alldata[i].free + '</p></td>';
                            //str1 = $("<td>").text(Alldata[i]);
                            billamt = billamt+parseFloat(Alldata[i].Value);
                            for (var key in Alldata[i]) {
                                for (var j = 0; j < ptyp.length; j++) {
                                    if (key == ptyp[j].Name) {
                                        str += '<td><p>' + (Alldata[i][key] == null ? '0' : Alldata[i][key]) + '</p></td>';

                                    }
                                }
                            }
                            str += '<td><p>' + parseFloat(Alldata[i].paid_amt).toFixed(2) + '</p></td><td><p>' + parseFloat(Alldata[i].Pending).toFixed(2) + '</p></td>';
                            revamt = revamt + parseFloat(Alldata[i].paid_amt);
                            penamt = penamt + parseFloat(Alldata[i].Pending);
                            $('#van_tbl tbody').append('<tr>' + str + '</tr>');
                        }
                        strff += '<th style="text-align: right" >' + billamt.toFixed(2) + '</th><th></th>';
                        for (var j = 0; j < ptyp.length; j++) {
                            strff +='<th></th>';
                        }
                        strff += '<th style="text-align: right" >' + revamt.toFixed(2) + '</th><th style="text-align: right" >' + penamt.toFixed(2) + '</th>';
                        $('#van_tbl tfoot').append('<tr class="trfoot">' + strff + '</tr>');
                    },
                });
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_vansale_summary.aspx/getdata",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    ptyp = JSON.parse(data.d) || [];
                    if (ptyp.length > 0) {
                        str = '<th  style="min-width:50px;" rowspan="2"><p style="margin: 0 0 0px;">Sl.No</p> </th><th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Bill Date</p></th><th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Bill Number</p></th><th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Shop Name</p></th><th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Route</p></th><th style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Bill Amount</p></th><th style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Free (Pcs)</p></th>';
                        console.log(len);
                        str1 = '';
                        
                        str += '<th style="min-width:150px; " colspan="' + ptyp.length+'"> <p name="pname" style="margin: 0 0 0px;">Payment Type</p> </th>';
                        for (var i = 0; i < ptyp.length; i++) {

                            str1 += '<th>' + ptyp[i].Name+'</th>';
                            //strff += '<th style="text-align: right" ></th><th style="text-align: right"></th></th>';

                        }
                        str +='<th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Received Amount</p> </th><th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Balance Amount</p> </th>';

                        $('#van_tbl thead').append('<tr class="mainhead">' + str + '</tr>');
                        $('#van_tbl thead').append('<tr class="secondhead">' + str1 + '</tr>');
                        
                    }
                },
                error: function (jqXHR, exception) {

                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }
            });
            genReport();
        });
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { workbook: 'VanSale&Collections' || 'Workbook',worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
        <div class="col-sm-8" style="padding-left: 5px;">
            <asp:Label ID="Label2" runat="server" Text="Van Sales&Collection Report " Style="font-weight: bold; font-size: x-large"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
           <a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" onclick="tableToExcel('van_tbl', 'Van Sales Report')" />
			<a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
    </div>
             <div class="row">
        <br />
        <asp:Label ID="Label1" runat="server" Text="" Style="padding-left: 5px;"></asp:Label>
        <br />
        <div id="content">
            <table id="van_tbl" border="1" class="newStly" style="border-collapse: collapse;">
                <thead>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
        </div>
    </div>
        </div>
    </form>
</body>
</html>
