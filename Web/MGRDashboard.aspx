<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGRDashboard.aspx.cs" Inherits="MGRDashboard" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
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
    </style>

</head>
<body>

    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        </div>

        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Manager Dashboard" Style="margin-left: 10px; font-size: x-large"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger;display:none;"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger; display: none;"></asp:Label>
        </div>
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        let outletsDay = [], outletsMonth = [], outletsYear = [], categoryDay = [], categoryMonth = [], categoryYear = [], mCategory = [], reps = [];
        let div_code = '<%=Session["div_code"]%>';
        let sf_code = '<%=Session["sf_code"]%>';
        let sf_name = '<%=sfname%>';
        function getMGRDay() {
            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getMGRDay",
                async: true,
                data: "{'sf_code':'" + sf_code + "','div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    outletsDay = JSON.parse(data.d);
                }
            })
        }
        function getReporting() {
            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getDrReporting",
                async: true,
                data: "{'sf_code':'" + sf_code + "'}",
                dataType: "json",
                success: function (data) {
                    reps = JSON.parse(data.d);
                }
            })
        }
        function getCategory() {
            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getCategory",
                async: true,
                data: "{'div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    mCategory = JSON.parse(data.d);
                }
            })
        }
        function getMGRMonth() {

            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getMGRMonth",
                async: true,
                data: "{'sf_code':'" + sf_code + "','div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    outletsMonth = JSON.parse(data.d);
                }
            })
        }
        function getMGRYear() {

            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getMGRYear",
                async: true,
                data: "{'sf_code':'" + sf_code + "','div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    outletsYear = JSON.parse(data.d);
                }
            })
        }
        function getMGRCatDay() {

            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getMGRCatDay",
                async: true,
                data: "{'sf_code':'" + sf_code + "','div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    categoryDay = JSON.parse(data.d);
                }
            })
        }
        function getMGRCatMonth() {

            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getMGRCatMonth",
                async: true,
                data: "{'sf_code':'" + sf_code + "','div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    categoryMonth = JSON.parse(data.d);
                }
            })
        }
        function getMGRCatYear() {

            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "MGRDashboard.aspx/getMGRCatYear",
                async: true,
                data: "{'sf_code':'" + sf_code + "','div':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    categoryYear = JSON.parse(data.d);
                }
            })
        }
        function ReloadTable() {
            var divhtml = $(`<div style="display: flex"></div>`);
            var reptbl = '';
            var reptblbody = '';
            var performancetbl = '';
            var performancebody = '';
            reptbl = $(`<table border="1" class="newStly" style="border-collapse: collapse; margin: 1rem 0rem 4rem 2rem;"><thead><tr><th>Designation</th><th>Name</th></tr></thead></table>`);
            reptblbody = $('<tbody></tbody>');
            performancetbl = $(`<table border="1" class="newStly" style="border-collapse: collapse; margin: 1rem 0rem 4rem 0rem;"><thead><tr><th colspan="4">${sf_name}</th></tr><tr><th>Sales Performance Index</th><th>Today</th><th>MTD</th><th>YTD</th></tr></thead></table>`);
            performancebody = $('<tbody></tbody>');
            for ($j = 0; $j < reps.length; $j++) {
                let eRw = `<tr style="cursor: pointer;" class="mgrclick" id="${reps[$j].Sf_Code}"><td>${reps[$j].Desig}</td><td class="sfnm">${reps[$j].SF}</td></tr>`;
                $(reptblbody).append(eRw);
            }
            let tsales = 0, msales = 0, ysales = 0;
            let tvisit = 0, mvisit = 0, yvisit = 0;
            let tprod = 0, mprod = 0, yprod = 0;
            if (outletsDay.length > 0) {
                tvisit = outletsDay[0].Visited;
                tprod = outletsDay[0].Productive;
                tsales = outletsDay[0].POB;
            }
            if (outletsMonth.length > 0) {
                mvisit = outletsMonth[0].Visited;
                mprod = outletsMonth[0].Productive;
                msales = outletsMonth[0].POB;
            }
            if (outletsYear.length > 0) {
                yvisit = outletsYear[0].Visited;
                yprod = outletsYear[0].Productive;
                ysales = outletsYear[0].POB;
            }
            let rw1 = `<tr><td>Secondary sales-Total Value</td><td>${+tsales}</td><td>${+msales}</td><td>${+ysales}</td></tr>`;
            let rw2 = `<tr><td>Visited Outlets-Total No</td><td>${+tvisit}</td><td>${+mvisit}</td><td>${+yvisit}</td></tr>`;
            let rw3 = `<tr><td>Productive Outlets-Total No</td><td>${+tprod}</td><td>${+mprod}</td><td>${+yprod}</td></tr>`;
            $(performancebody).append(rw1);
            $(performancebody).append(rw2);
            $(performancebody).append(rw3);
            for ($i = 0; $i < mCategory.length; $i++) {
                let filtcday = categoryDay.filter(function (a) {
                    return a.Product_Cat_Code == mCategory[$i].Product_Cat_Code
                }).map(function (el) {
                    return el.Qty
                }).toString();
                let filtcMnth = categoryMonth.filter(function (a) {
                    return a.Product_Cat_Code == mCategory[$i].Product_Cat_Code
                }).map(function (el) {
                    return el.Qty
                }).toString();
                let filtcYr = categoryYear.filter(function (a) {
                    return a.Product_Cat_Code == mCategory[$i].Product_Cat_Code
                }).map(function (el) {
                    return el.Qty
                }).toString();
                let brw1 = `<tr><td>${mCategory[$i].Product_Cat_Name}</td><td>${+filtcday}</td><td>${+filtcMnth}</td><td>${+filtcYr}</td></tr>`;
                $(performancebody).append(brw1);
            }
            $(reptbl).append(reptblbody)
            $(performancetbl).append(performancebody);
            $(divhtml).append(performancetbl);
            $(divhtml).append(reptbl);
            $('#content').append(divhtml)
        }
        function loadData() {
            $.when(getMGRCatYear(), getMGRDay(), getMGRMonth(), getCategory(), getMGRYear(), getMGRCatDay(), getMGRCatMonth(), getReporting()).then(function () {
                ReloadTable();
            });
        }
        $(document).on('click', '.mgrclick', function () {
            sf_code = $(this).attr("id");
            sf_name = $(this).find('.sfnm').text();
            $('#loadover').show();
            setTimeout(function () {
                loadData();
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
        })
        $(document).ready(function () {
            $('#loadover').show();
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
