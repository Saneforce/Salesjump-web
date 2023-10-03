<%@ Page Title="SKU Analysis" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    EnableEventValidation="false" CodeFile="SKU_Analysis.aspx.cs" Inherits="MasterFiles_SKU_Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <title>Area</title>
    <style type="text/css">
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
        #table1_dyn tr,#table_growth tr,#table_comp tr,#table_outlet tr,#table_repoutlet tr
        {
            background-color: #D0E4F5;
            color: #333;
        }
        #table1_dyn tr:first-child,  #table_growth tr:first-child ,#table_comp tr:first-child,#table_outlet tr:first-child,#table_repoutlet tr:first-child
        {
            background-color: #1C6EA4;
            color: #ffffff;
        }
        label
        {
            font-weight: normal;
            margin: 0px;
            padding: 5px 5px;
        }
        tr
        {
            padding: 5px 5px;
            margin: 0px;
        }
        #chartdiv, #chartdiv1, #chartdiv2,#state_chart
        {
            height: 250px;
            font-size: 11px;
        }

        #state_chart
        {
             width: 100%;
             height: 300px;
        }
        .amcharts-legend-div {
            overflow-y: auto!important;
            max-height: 300px;
         }
        .sku_span
        {
             DISPLAY: BLOCK;
    font-weight: bold;
    BACKGROUND: #d2dce485;
    PADDING: 5px;
    COLOR: #6c941d;

        }
        #table1_dyn TD, #table_growth TD,#table_comp TD {
    PADDING: 3px 10px;
}
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   
    <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="../JsFiles/light.js" type="text/javascript"></script>
  
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

        $(document).ready(function () {
            $(".container").hide();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SKU_Analysis.aspx/Get_Product_Name",
                // data: "{'div_code':'" + div_code + "','sf_code':'" + sf_code + "'}",
                // data: "{'div_code':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#DDL_Product");
                    ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['Product_code']).html(this['Product_Name']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


            $(document).on('change', '#DDL_Product', function () {
                //alert($("#DDL_Product option:selected").val());
                var selval = $("#DDL_Product").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_Product_Year",
                    data: "{'pro_code':'" + selval + "'}",
                    dataType: "json",
                    success: function (data) {

                        var ddlCustomers = $("#DDL_Year");
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(data.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['years']).html(this['years']));
                        });
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });
        });

        $(document).ready(function () {
            $('.button').click(function () {
                var selval = $("#DDL_Product").val();
                if (selval == 0) { alert("Please select Product"); $("#DDL_Product").focus(); return false; }
                var selyear = $("#DDL_Year").val();
                if (selyear == 0) { alert("Please Select Yaer"); $("#DDL_Year").focus(); return false; }
                $(".container").show();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_Product_Details",
                    // data: "{'pro_code':'" + selval + "'}",
                    data: "{'pro_code':'" + selval + "','year':'" + selyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        // alert(JSON.stringify(data.d));
                        $('#pro_name').text(": " + data.d[0].Product_Name);
                        $('#pro_sub_name').text(": " + data.d[0].Product_Short_Name);
                        $('#create_date').text(": " + data.d[0].Create_date);
                        $('#uom').text(": " + data.d[0].UOM);
                        $('#base_uom').text(": " + data.d[0].Base_UOM);
                        $('#pro_img').attr("src", data.d[0].pro_img);
                        $('#pro_cat').text(": " + data.d[0].product_cat);
                        $('#pro_brand').text(": " + data.d[0].product_brand);
                        $('#pro_target').text(": " + data.d[0].product_target);
                        //$('#pro_achieved').text(": " + data.d[0].product_achieved);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_Product_Data",
                    //    data: "{'Product_Code':'" + selval + "'}",
                    data: "{'Product_Code':'" + selval + "','year':'" + selyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        var list = data.d;
                        var pluginArrayArg = new Array();
                        for (var i = 0; i < data.d.length; i++) {
                            pluginArrayArg[i] = new Array();
                            pluginArrayArg[i] = {};
                            pluginArrayArg[i].category = data.d[i].years;
                            pluginArrayArg[i].year2004 = data.d[i].tot_val.toFixed(2);
                            pluginArrayArg[i].year2005 = data.d[i].sig_val.toFixed(2);
                            if (data.d[i].years == selyear) {
                                $('#pro_achieved').text(": " + data.d[i].sig_val.toFixed(2));
                            }

                        }

                        // alert(JSON.stringify(pluginArrayArg));

                        barchart(pluginArrayArg);
                        linechart(pluginArrayArg);

                        $('#table1_dyn tr').remove();
                        $('#table_growth tr').remove();
                        var str = "<td align='left' style='width:20%'> Year </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].years + "</td>";
                        }
                        $('#table1_dyn tbody').append("<tr> " + str + " </tr>");
                        $('#table_growth tbody').append("<tr> " + str + " </tr>");
                        var str = "<td align='left'>All Product </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].tot_val.toFixed(2) + "</td>";
                        }
                        $('#table1_dyn tbody').append("<tr> " + str + " </tr>");
                        var str = "<td align='left' >This Product </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            var tot = 0;
                            if (Number(data.d[i].tot_val) > 0) {
                                tot = (data.d[i].sig_val / data.d[i].tot_val * 100).toFixed(2);
                            }
                            else {
                                tot = 0;
                            }
                            str += "<td>" + data.d[i].sig_val.toFixed(2) + " (" +tot +" %)" + "</td>";
                        }
                        $('#table1_dyn tbody').append("<tr> " + str + " </tr>");
                        $('#table_growth tbody').append("<tr> " + str + " </tr>");

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_Product_Growth",

                    data: "{'Product_Code':'" + selval + "','year':'" + selyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        //alert(JSON.stringify(data.d));
                        // console.log(JSON.stringify(data.d));
                        var list = data.d;
                        var promonth = new Array();
                        for (var i = 0; i < data.d.length; i++) {
                            promonth[i] = new Array();
                            promonth[i] = {};
                            promonth[i].category = data.d[i].years;
                            promonth[i].year2004 = data.d[i].tot_val.toFixed(2);
                            promonth[i].year2005 = data.d[i].sig_val.toFixed(2);
                        }
                        //  console.log(JSON.stringify(promonth));

                        $('#table_comp tr').remove();
                        var str = "<td align='left' style='width:20%'> Year </td> ";
                        //  var str_grow = "<td align='left' style='width:20%'> Year </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].years + "</td>";
                            //  str_grow += "<td>" + data.d[i].years + "</td>";
                        }
                        $('#table_comp tbody').append("<tr> " + str + " </tr>");
                        var str = "<td align='left'>" + selyear + " </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].tot_val.toFixed(2) + "</td>";
                        }
                        $('#table_comp tbody').append("<tr> " + str + " </tr>");

                        var str = "<td align='left' >" + (Number(selyear) - Number(1)) + "</td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].sig_val.toFixed(2) + "</td>";
                        }
                        $('#table_comp tbody').append("<tr> " + str + " </tr>");
                        linechart2(promonth, Number(selyear) - 1, selyear);


                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                  $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_Product_shopcnt",

                    data: "{'Product_Code':'" + selval + "','year':'" + selyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        //alert(JSON.stringify(data.d));
                        // console.log(JSON.stringify(data.d));
                        list = data.d;
                        var promonth = new Array();
                        for (var i = 0; i < data.d.length; i++) {
                            promonth[i] = new Array();
                            promonth[i] = {};
                            promonth[i].category = data.d[i].years;
                            promonth[i].year2004 = data.d[i].tot_cnt;
                            promonth[i].year2005 = data.d[i].sig_cnt;
                        }
                        //  console.log(JSON.stringify(promonth));

                        $('#table_outlet tr').remove();
                        var str = "<td align='left' style='width:20%'> Year </td> ";
                        //  var str_grow = "<td align='left' style='width:20%'> Year </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].years + "</td>";
                            //  str_grow += "<td>" + data.d[i].years + "</td>";
                        }
                        $('#table_outlet tbody').append("<tr> " + str + " </tr>");
                        var str = "<td align='left'>" + selyear + " </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td><a href='view_countofshop.aspx?productcode=" + selval + "&year=" + selyear + "&month=" + data.d[i].years +"'   id='view_dtl' id='view_dtl'>" + data.d[i].tot_cnt + "</a></td>";
                        }
                        $('#table_outlet tbody').append("<tr> " + str + " </tr>");

                        var str = "<td align='left' >" + (Number(selyear) - Number(1)) + "</td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td><a href='view_countofshop.aspx?productcode=" + selval + "&year=" + (Number(selyear) - Number(1)) + "&month=" + data.d[i].years +"'   id='view_dtl' id='view_dtl'>" + data.d[i].sig_cnt + "</a></td>";
                        }
                        $('#table_outlet tbody').append("<tr> " + str + " </tr>");
                        linechart2(promonth, Number(selyear) - 1, selyear);


                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_Product_shoprepeat",

                    data: "{'Product_Code':'" + selval + "','year':'" + selyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        //alert(JSON.stringify(data.d));
                        // console.log(JSON.stringify(data.d));
                        list = data.d;
                        var promonth = new Array();
                        for (var i = 0; i < data.d.length; i++) {
                            promonth[i] = new Array();
                            promonth[i] = {};
                            promonth[i].category = data.d[i].years;
                            promonth[i].year2004 = data.d[i].tot_rpt;
                            promonth[i].year2005 = data.d[i].sig_rpt;
                        }
                        //  console.log(JSON.stringify(promonth));

                        $('#table_repoutlet tr').remove();
                        var str = "<td align='left' style='width:20%'> Year </td> ";
                        //  var str_grow = "<td align='left' style='width:20%'> Year </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td>" + data.d[i].years + "</td>";
                            //  str_grow += "<td>" + data.d[i].years + "</td>";
                        }
                        $('#table_repoutlet tbody').append("<tr> " + str + " </tr>");
                        var str = "<td align='left'>" + selyear + " </td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td><a href='view_countofrepeatshop.aspx?productcode=" + selval + "&year=" + selyear + "&month=" + data.d[i].years +"'   id='view_dtl'>" + data.d[i].tot_rpt + "</a></td>";
                        }
                        $('#table_repoutlet tbody').append("<tr> " + str + " </tr>");

                        var str = "<td align='left' >" + (Number(selyear) - Number(1)) + "</td> ";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<td><a href='view_countofrepeatshop.aspx?productcode=" + selval + "&year=" + (Number(selyear) - Number(1)) + "&month=" + data.d[i].years +"' id='view_dtl'>" + data.d[i].sig_rpt + "</a></td>";
                        }
                        $('#table_repoutlet tbody').append("<tr> " + str + " </tr>");
                        linechart2(promonth, Number(selyear) - 1, selyear);


                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SKU_Analysis.aspx/Get_StateWise",
                    //data: "{'pro_code':'" + selval + "'}",
                    data: "{'pro_code':'" + selval + "','year':'" + selyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        // alert(JSON.stringify(data.d));
                        // console.log(JSON.stringify(data.d));

                        var list = data.d;
                        var pluginArrayArg = new Array();
                        var str = "";
                        var arnew = "[";
                        for (var i = 0; i < data.d.length; i++) {

                            if (str != data.d[i].Years) {

                                if (i == 0) {
                                    arnew += '{"Year" : "' + data.d[i].Years + '","';
                                }
                                else if (i != 0) {
                                    arnew = arnew.substring(0, arnew.length - 2);
                                    arnew += '},{"Year" : "' + data.d[i].Years + '","';
                                }
                                str = data.d[i].Years;
                            }
                            arnew += data.d[i].State_Name + '" : "' + data.d[i].values.toFixed(2) + '","';
                        }
                        arnew = arnew.substring(0, arnew.length - 2);
                        arnew += "}]";
                        //console.log(arnew);


                        state_chart(arnew);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            });
        });



        function barchart(pluginArrayArg) {

            var chart = AmCharts.makeChart("chartdiv", {
                "theme": "light",
                "type": "serial",
                "dataProvider": pluginArrayArg,
				"depth3D": 40,
			    "angle": 30,
                "startDuration": 1,
                "graphs": [{
                    "balloonText": "[[category]] (All Product): <b>[[value]]</b>",
                    "fillAlphas": 0.2,
                    "lineAlpha": 0.9,
                    "title": "All Product",
                    "type": "column",

					"topRadius":1,
                    "labelText": "[[value]]",
                    "columnWidth": 0.2,
                    "valueField": "year2004"
                }, {
                    "balloonText": "[[category]] (This Product): <b>[[value]]</b>",
                    "fillAlphas": 0.9,
                    "lineAlpha": 0.2,
                    "title": "This Product",
                    "type": "column",

					"topRadius":1,
                    "labelText": "[[value]]",
                    "clustered": false,
                    "columnWidth": 0.2,
                    "valueField": "year2005"
                }],

                "plotAreaFillAlphas": 0.1,
                "categoryField": "category",
                "categoryAxis": {
                    "gridPosition": "start"
                },
                "export": {
                    "enabled": true
                }
            });
        }

        function linechart(pluginArrayArg) {
            var chart = AmCharts.makeChart("chartdiv1", {
                "type": "serial",
                "theme": "light",
                "marginRight": 70,
                "autoMarginOffset": 20,
                "dataProvider": pluginArrayArg,
                "graphs": [{
                    "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]] </span></b>",
                    "bullet": "round",
                    "bulletSize": 6,
                    "connect": false,
                    "lineColor": "#b6d278",
                    "lineThickness": 2,
                    "negativeLineColor": "#487dac",
                    "valueField": "year2005"
                }],
                "chartCursor": {
                    "categoryBalloonDateFormat": "YYYY",
                    "cursorAlpha": 0.1,
                    "cursorColor": "#000000",
                    "fullWidth": true,
                    "graphBulletSize": 2
                },
                "dataDateFormat": "YYYY",
                "categoryField": "category",
                "categoryAxis": {
                    "minPeriod": "YYYY",
                    "parseDates": true,
                    "minorGridEnabled": true
                },
                "export": {
                    "enabled": true
                }

            });
        }

        function linechart2(promonth,lr,cur) {

            var chart = AmCharts.makeChart("chartdiv2", {
                "type": "serial",
                "theme": "light",
                "dataProvider": promonth,
                "gridAboveGraphs": true,
                "startDuration": 1,
                "graphs": [{
                    "title": cur,
                    "balloonText": cur + " : [[category]]: <b>[[value]]</b>",
                    "bullet": "round",
                    "bulletSize": 10,
                    "bulletBorderColor": "#ffffff",
                    "lineColor": "#b6d278",
                    "bulletBorderAlpha": 1,
                    "bulletBorderThickness": 2,                    
                    "valueField": "year2004"
                }, {
                    "title": lr,
                    "balloonText": lr + " : [[category]]: <b>[[value]]</b>",
                    "bullet": "round",
                    "bulletSize": 10,
                    "bulletBorderColor": "#ffffff",
                    "bulletBorderAlpha": 1,
                    "bulletBorderThickness": 2,                    
                    "valueField": "year2005"
                }],
                "chartCursor": {
                    "categoryBalloonEnabled": false,
                    "cursorAlpha": 0,
                    "zoomable": false
                },
                "categoryField": "category",
                "categoryAxis": {
                    "dashLength": 1,
                    "minorGridEnabled": true
                },
                "legend": { "position": "right",
                    "valueWidth": 100,
                    "marginRight": 10,
                    "valueAlign": "right",
                    "autoMargins": false
                }
            });
        }



        function state_chart(promonth) {
            var lr = 2017;
            var array = JSON.parse(promonth);


            var st_arr = new Array();
            var k = 0;
            for (var key in array[0]) {
                if (k>0){
                    //var key = Object.keys(array[k])[0];
                    grpStat = {};
                    grpStat.title = key;
                    grpStat.balloonText = key + " : [[Year]]: <b>[[value]]</b>";
                    grpStat.fillAlphas = "0.8";
                    grpStat.lineAlpha = "0.3";
                    grpStat.type = "column";
                    grpStat.columnWidth = "0.5",
                    grpStat.bulletBorderThickness = "2";
                    grpStat.valueField = key;
                    st_arr.push(grpStat);
                    console.log(st_arr);
                }
                k++;

            }


////          

          var chart = AmCharts.makeChart("state_chart", {
              "type": "serial",
              "theme": "light",
              "dataProvider": array,
              "gridAboveGraphs": true,
              "startDuration": 1,
              "graphs": st_arr,
              "chartCursor": {
                  "categoryBalloonEnabled": false,
                  "cursorAlpha": 0,
                  "zoomable": false
              },
              "categoryField": "Year",
              "categoryAxis": {
                  "dashLength": 1,
                  "minorGridEnabled": true
              },
              "legend": {
                  "position": "right",
                  "marginRight": 100,
                  "valueWidth": 100,
                  "valueAlign": "right",
                  "autoMargins": false
              }
            });
        }

        
    </script>
    <style type="text/css">
        .button
        {
            display: inline-block;
            border-radius: 4px;
            background-color: #6495ED;
            border: none;
            color: #FFFFFF;
            text-align: center;
            font-bold: true;
            width: 75px;
            height: 29px;
            transition: all 0.5s;
            cursor: pointer;
            margin: 5px;
        }
        
        .button span
        {
            cursor: pointer;
            display: inline-block;
            position: relative;
            transition: 0.5s;
        }
        
        
        
        .button span:after
        {
            content: '»';
            position: absolute;
            opacity: 0;
            top: 0;
            right: -20px;
            transition: 0.5s;
        }
        
        .button:hover span
        {
            padding-right: 25px;
        }
        
        .button:hover span:after
        {
            opacity: 1;
            right: 0;
        }
        .ddl
        {
            border: 1px solid #1E90FF;
            margin: 2px;
            border-radius: 4px;
            font-family: Andalus;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
        .ddl1
        {
            border: 1px solid #1E90FF;
            margin: 2px;
            border-radius: 4px;
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; 
        }
    </style>
    <div id="div_main"  text-align: center; margin: 0; padding: 0;">
        <div id="div_head" style="width: 100%; text-align: center; margin: 0 auto;">
            <table id="table_head" style="width: 100%;">
                <tr>
                    <td style="width: 50%">
                        Product Name : 
                        <select id="DDL_Product" skinid="ddlRequired" class="ddl">
                        </select>
                        Year : 
                        <select id="DDL_Year" skinid="ddlRequired" class="ddl">
                        </select>
                        
                        <button id="btnView" class="button" runat="server" style="vertical-align: middle">
                            <span>View</span></button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div class="container" style="width: 100%;">
        <div class="row">
      
            <div class="col-xs-6">
              <span style="font-weight: bold"  class="sku_span">Profile</span>
              <div class="row">
              <div class="col-xs-4">
              <div class="row">
              <div class="col-xs-12"></div>
              <div id="proimg"></div>
              </div>
              </div>
              <div class="col-xs-8">
                <div class="row">
                    <div class="col-xs-6">
                        Product Name
                    </div>
                    <div class="col-xs-6">
                        <label id="pro_name">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Product Short Name</div>
                    <div class="col-xs-6">
                        <label id="pro_sub_name">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Create On</div>
                    <div class="col-xs-6">
                        <label id="create_date">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        UOM</div>
                    <div class="col-xs-6">
                        <label id="uom">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Base UOM</div>
                    <div class="col-xs-6">
                        <label id="base_uom">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Category</div>
                    <div class="col-xs-6">
                        <label id="pro_cat">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Brand Name </div>
                    <div class="col-xs-6">
                        <label id="pro_brand">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Target</div>
                    <div class="col-xs-6">
                        <label id="pro_target">
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        Achieved </div>
                    <div class="col-xs-6">
                        <label id="pro_achieved">
                        </label>
                    </div>
                </div>
                </div>
                </div>
            </div>
            <div class="col-xs-6">
                <span style="font-weight: bold"  class="sku_span">Sales & Contribution</span>
                <div class="row">
                    <div class="col-xs-12">
                        <div id="chartdiv">
                        </div>
                        <table id="table1_dyn" style="width:100%">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-6">
                <span style="font-weight: bold" class="sku_span" >Growth Monitor</span>
                <div id="chartdiv1">
                </div>
                 <table id="table_growth" style="width:100%">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
            </div>
            <div class="col-xs-6">
                <span style="font-weight: bold"  class="sku_span">Last Year comparison</span>
                <div id="chartdiv2">
                </div>
                   <table id="table_comp" style="width:100%">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
            </div>
        </div>
		<div class="row">
           <div class="col-xs-6">
                <span style="font-weight: bold"  class="sku_span">Billed Outlet Count</span>
               
                   <table id="table_outlet" style="width:100%">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
            </div>
                  <div class="col-xs-6">
                <span style="font-weight: bold"  class="sku_span">Repeated Billed Outlet Count</span>
               
                   <table id="table_repoutlet" style="width:100%">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
            </div>
             </div>
          <br />
        <div class="row">
        <div class="col-xs-12">
         <span style="font-weight: bold" class="sku_span" >Statewise Performance</span>
        <div id="state_chart"></div>       
         
        </div>
        

        </div>
    </div>      
     
</asp:Content>
