<%@ Page Title="" Language="C#" MasterPageFile="~/Master_MR.master" AutoEventWireup="true"
    CodeFile="DashBoard_MR.aspx.cs" Inherits="DashBoard_MR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
      <link rel="stylesheet" href="css/dashboard.css">
    <script src="js/canvasjs.min.js" type="text/javascript"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
     <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="../JsFiles/light.js" type="text/javascript"></script>
    <style type="text/css">
        canvas, .ChartTop
        {
            border-radius: 6px;
        }
        .noti
        {
            border: solid 1px #000000;
        }
        .notification_div
        {
            border-radius: 6px;
            height: 450px;
            border: solid 1px #cccccc;
            padding-left: 50px;
            margin-left: 50px;
        }
        .ChartTop
        {
            height: 180px;
            border: solid 1px #cccccc;
            width: 100%;
        }
        .Chartdown
        {
            border-radius: 6px;
            height: 150px;
            border: solid 1px #cccccc;
        }
        .ddl
        {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-family: Andalus;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
        .huge
        {
            font-size: 20px;
        }
        .panel-green
        {
            border-color: #5cb85c;
        }
        .panel-green > .panel-heading
        {
            border-color: #5cb85c;
            color: white;
            background-color: #5cb85c;
        }
        .panel-green > a
        {
            color: #5cb85c;
        }
        .panel-green > a:hover
        {
            color: #3d8b3d;
        }
        .list-group-item {
            position: relative;
            
            padding: 10px 15px;
            margin-bottom: -1px;
            background-color: #fff;
            border: 1px solid #ddd;
        }
    

#chartdiv1 {
  width: 100%;
  height: 500px;
}	
}
    </style>
    <script language="javascript" type="text/javascript">
        function popUp() {
            var sf_code = '<%= Session["sf_code"] %>';
     
            strOpen = "MIS Reports/rpt_Total_Order_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
                  "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sf_code + "&Sub_Div='0'&Date=" + todate
            window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=400,height=600,left = 0,top = 0');
        }
    </script>
    <div class="col-lg-8" style="padding: 0px;">
        <div class="col-md-4">
            <div class="sm-st clearfix">
                <span class="sm-st-icon st-red"><i class="fa fa-user"></i></span>
                <div class="sm-st-info">
                    <span><asp:Label ID="retailer" runat="server" Text="Label"></asp:Label></span> Retailer
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="sm-st clearfix">
                <span class="sm-st-icon st-violet"><i class="fa fa-user"></i></span>
                <div class="sm-st-info">
                    <span><asp:Label ID="Dist_cou" runat="server"></asp:Label></span> Distributor</div>
            </div>
        </div>
        <div class="col-md-4">
           
           <button id="Btn_order" type="button" runat="server" onserverclick="Submit_Click" class="sm-st clearfix" style="width:210px;height:93px;border-bottom-width:0px;border-right-width:0px;border-color:White;"><a class="sm-st-icon st-blue"><i class="glyphicon glyphicon-briefcase"></i>
                </a>
                <div class="sm-st-info">
                    <span><asp:Label ID="ordercount" runat="server" ></asp:Label></span><asp:Label ID="Order_val" runat="server" Font-Size="Small" ></asp:Label></div>
            </button>
        </div>
        <div class="col-lg-12" style="padding: 0px;">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Primary Vs Secondary
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div id="ChrtPrimSec" class="Chartdown">
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
        </div>
        <div class="col-lg-12" style="padding: 0px;">
            <div id="exTab3" class="container">
                <ul class="nav nav-pills">
                    <li class="active"><a href="#1b" data-toggle="tab">Orders</a> </li>
                    <li><a href="#2b" data-toggle="tab">Sales</a> </li>
                </ul>
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1b">
                    <div class="col-lg-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-bar-chart-o fa-fw"></i>Order - 2018 (In Values)
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="row" style="padding: 0px;">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="T5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="B5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="T5brand" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="B5brand" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="T5Pro" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="B5Pro" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="2b">
                    <div class="col-lg-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-bar-chart-o fa-fw"></i>Sales - 2018 (In Values)
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <!-- /.col-lg-4 (nested) -->
                                <div class="row" style="padding: 0px;">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="ST5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="SB5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="ST5brand" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="SB5brand" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="ST5Pro" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="SB5Pro" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.col-lg-8 (nested) -->
                            <!-- /.row -->
                            <!-- /.panel-body -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
 <div class="row">
     <asp:Panel ID="Panel1" runat="server">
     
        <div class="col-lg-4 col-md-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-user fa-5x"></i>
                            <div class="huge">
                                <asp:Label ID="Lbl_Tot_User" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Total Users!</div>
                        </div>
                        <div class="col-xs-3 text-right">
                            <div class="huge">
                                <asp:Label ID="Lbl_Reg_User" runat="server" Text="0"></asp:Label></div>
                            <div style="width: 67px;">
                                In-Market!</div>
                            <div class="huge" style="margin-top: 40px;">
                                <asp:Label ID="Lbl_Lea" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Leave!</div>
                        </div>
                        <div class="col-xs-6 text-right">
                            <div class="huge">
                                <asp:Label ID="Lbl_Oth" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Other Works!</div>
                            <div class="huge" style="margin-top: 40px;color:#ff9898;">
                                <asp:Label ID="Lbl_Inact_User" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Inactive Users!</div>
                        </div>
                    </div>
                </div>
                <a href="#">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div>
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-tasks fa-5x"></i>
                                <div class="huge">
                                    <asp:Label ID="Lbl_Outlets" runat="server" Text="0"></asp:Label></div>
                                <div>
                                   Visited Outlets</div>
                            </div>
                            <div class="col-xs-3 text-right">
                            <div class="huge">
                                    <asp:Label ID="Lbl_Sch_Call" runat="server" Text=" "></asp:Label></div>
                                <div>
                                 &nbsp
                                    </div>
                                <div class="huge" style="margin-top: 70px;">
                                    <asp:Label ID="Lbl_Prod" runat="server" Text="0"></asp:Label></div>
                                <div>
                                    Productive!</div>
                               
                            </div>
                            <div class="col-xs-6 text-right">
                                 
                                <div class="huge">
                                    <asp:Label ID="Lbl_Prod_Outlet" runat="server" Text="23.1%"></asp:Label></div>
                                <div>
                                    Productivity!</div>

                                    <div class="huge" style="margin-top: 40px;">
                                    <asp:Label ID="Lbl_Vist_Outlet" runat="server" Text="0"></asp:Label></div>
                                <div>
                                    Un-Productive!</div>
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                            </i></span>
                            <div class="clearfix">
                            </div>
                        </div>
                    </a>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-bell fa-fw"></i>Notifications Panel
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="list-group">
                            <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" class="list-group-item" OnItemDataBound="Item_Bound" > <ItemTemplate><i class="fa fa-comment fa-fw"></i><asp:Label ID="lblInput" runat="server" Width="255px" style="padding-top:10px;padding-left:5px;"  Text='<%# Eval("comment") %>'></asp:Label><span
                                class="pull-right text-muted small"><em><asp:Label ID="daytime" runat="server" Text='<%# Eval("timee") %>' style="font-style:bold;font-size:10px;color:#a8b0b3;" ></asp:Label><i class="fa fa-clock-o" style="color:#a6aeb1;"></i></em> </span>  <asp:Label ID="cmttype" runat="server" Text='<%# Eval("Comment_Type") %>' Visible="false"></asp:Label> </ItemTemplate>
                             </asp:DataList>

                                                       
                        </div>
                        <!-- /.list-group -->
                        <a href="../../../../MIS Reports/Notification_inpu.aspx" class="btn btn-default btn-block">Set Alerts</a>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-bar-chart-o fa-fw"></i>In-Time Statistics
                    </div>
                    <div class="panel-body">
                      <div id="chartContainer" style="height: 200px; width: 100%;"></div>
                        <a href="#" class="btn btn-default btn-block">View Details</a>
                    </div>
                    <!-- /.panel-body -->
                </div>
            </div>
        </div>
        </asp:Panel>
    </div>
    <script type="text/javascript">
        function gen1(Obj, value, title) {
            AmCharts.addInitHandler(function (chart) {
         for(var i = 0; i < chart.graphs.length; i++) {
    var graph = chart.graphs[i];
    if (graph.autoColor !== true)
      continue;
    var colorKey = "autoColor-"+i;
    graph.lineColorField = colorKey;
    graph.fillColorsField = colorKey;
    for(var x = 0; x < chart.dataProvider.length; x++) {
      var color = chart.colors[x]
      chart.dataProvider[x][colorKey] = color;
    }
  }
  
}, ["serial"]);

var chart = AmCharts.makeChart(Obj, {
                "type": "serial",
                "theme": "light",
                "type": "serial",
                "startDuration": 2,
                "fontSize": 9,
                "dataProvider": value,
                "graphs": [{
                    "balloonText": "[[category]]: <b>[[value]]</b>",
                    "fillColorsField": "color",
                    "fillAlphas": 1,
                    "lineAlpha": 0.1,
                    "type": "column",
                    "valueField": "y",
                    "autoColor": true
                }],
                "depth3D": 20,
                "angle": 30,
                "chartCursor": {
                    "categoryBalloonEnabled": false,
                    "cursorAlpha": 0,
                    "zoomable": false
                },
                "categoryField": "label",
                "categoryAxis": {
                    "gridPosition": "start",
                    "inside": true,
                    "labelRotation": 90
                },
                "export": {
                    "enabled": false
                }

            });

        }
        function gen2(Obj, value, title) {
            var chart = new CanvasJS.Chart(Obj,{
                exportEnabled: false,
                animationEnabled: true,

                legend: {
                    cursor: "pointer",
                    itemclick: explodePie
                },
                data: [{
                    type: "pie",
                    showInLegend: false,
                    toolTipContent: "{label}: <strong>{y}</strong>",
                    indexLabel: "{label} - {y}",
                    exploded: true,
                    dataPoints: value

                    
                }]
            });
            chart.render();
        }

function explodePie (e) {
	if(typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
		e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
	} else {
		e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
	}
	e.chart.render();

}
      
        function genChart1(Obj, arrDta, arr1, title) {
//            var yourSessionValue = '<%= Session["sf_code"] %>';
//            alert(yourSessionValue);
            //var date = new Date(); alert((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
            
            var chart = AmCharts.makeChart(Obj, {
                "theme": "light",
                "type": "serial",
                "startDuration": 0,
                "dataProvider": arrDta,
                "valueAxes": [{
                    "axisAlpha": 0,
                    "position": "left"
                }],
                "graphs": [{
                    "id": "g1",
                    "balloonText": "[[label]]<br><b><span style='font-size:14px;'>[[y]]</span></b>",
                    "bullet": "round",
                    "bulletSize": 8,
                    "lineColor": "#428bca",
                    "lineThickness": 2,
                    "negativeLineColor": "#637bb6",
                    "type": "smoothedLine",
                    "valueField": "y"
                },{
                    "id": "g2",
                    "balloonText": "[[label]]<br><b><span style='font-size:14px;'>[[s]]</span></b>",
                    "bullet": "round",
                    "bulletSize": 8,
                    "lineColor": "#5cb85c",
                    "lineThickness": 2,
                    "negativeLineColor": "#d1655d",
                    "type": "smoothedLine",
                    "valueField": "s"
                }],
                "dataDateFormat": "mmm-YY",
                "categoryField": "label",
                "categoryAxis": {
                    "gridPosition": "start",
                    "labelRotation": 0
                },
                "export": {
                    "enabled": false
                }

            });
          
        }

    </script>

    </form>
</asp:Content>
