<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Master_MR.master" AutoEventWireup="true"
    CodeFile="Default1.aspx.cs" Inherits="Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <style type="text/css">
                canvas, .ChartTop {
                    border-radius: 6px;
                }


                .noti {
                    border: solid 1px #000000;
                }


                .notification_div {
                    border-radius: 6px;
                    height: 450px;
                    border: solid 1px #cccccc;
                    padding-left: 50px;
                    margin-left: 50px;
                }


                .ChartTop {
                    height: 120px;
                    border: solid 1px #cccccc;
                }


                .Chartdown {
                    border-radius: 6px;
                    height: 150px;
                    border: solid 1px #cccccc;
                }


                .ddl {
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
            </style>
            <script type="text/javascript">
                function genChart(Obj, arrDta, title) {
                    var chart = new CanvasJS.Chart(Obj, {
                        theme: "theme3",
                        axisX: {
                            labelFormatter: function (e) {
                                return " ";
                            },
                            tickLength: 0
                            , lineThickness: 0
                            //,margin:-6
                        },
                        axisY: {

                            valueFormatString: " ",
                            tickLength: 0,
                            lineThickness: 0
                        },
                        title: {
                            text: title
                        },
                        animationEnabled: true,
                        data: [{
                            type: "column",       // Change type to "bar", "area", "spline", "pie",etc.
                            dataPoints: arrDta
                        }]
                    });
                    chart.render();
                }

                function genChart1(Obj, arrDta, title) {
                    var chart1 = new CanvasJS.Chart(Obj,
                        {
                            theme: "theme2",
                            title: {
                                text: "Primary Vs Secondary - per month"
                            },
                            animationEnabled: true,
                            axisX: {
                                valueFormatString: "MMM",
                                interval: 1,
                                intervalType: "month"

                            },
                            axisY: {
                                includeZero: false

                            },
                            data: [
                                {
                                    type: "spline",
                                    //lineThickness: 3,        
                                    dataPoints: [


                                    ]
                                }, {
                                    type: "spline",
                                    //lineThickness: 3,        
                                    dataPoints: [


                                    ]
                                }
                            ]
                        });

                    chart1.render();
                }
            </script>
            <script src="js/canvasjs.min.js" type="text/javascript"></script>
        </head>
        <body>
            <form runat="server" id="form2">
                <div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-3">
                        <div class="sm-st clearfix">
                            <span class="sm-st-icon st-red"><i class="fa fa-user"></i></span>
                            <div class="sm-st-info">
                                <span>
                                    <asp:Label ID="retailer" runat="server" ></asp:Label>
                                </span> Retailer
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="sm-st clearfix">
                            <span class="sm-st-icon st-violet"><i class="fa fa-envelope-o"></i></span>
                            <div class="sm-st-info">
                                <span>0</span> Mail
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="sm-st clearfix">
                            <span class="sm-st-icon st-blue"><i class="glyphicon glyphicon-shopping-cart"></i></span>
                            <div class="sm-st-info">
                                <span><asp:Label ID="ordercount" runat="server" ></asp:Label></span>Orders</div>
                        </div>
                    </div>
                    
                    <div class="col-md-3">
                        <div class="sm-st clearfix">
                            <span class="sm-st-icon st-green"><i class="fa fa-paperclip"></i></span>
                            <div class="sm-st-info">
                                <span>0</span> Total Documents
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8" style="background: white;">
                        <asp:Panel runat="server" CssClass="panel">
                            <form id="form1" runat="server">
                                <div class="row">
                                    <header class="panel-heading" style="background-color:#19a4c6;;color:White; font-size:10px; font-family:Sans-Serif; padding-left:400px; font-weight: bolder;" >charts</header>
                                    <br/>
                                    <div class="col-md-4">
                                        <div id="T10brand" class="ChartTop">

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div id="T10Cate" class="ChartTop">

                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4">
                                        <div id="T10Pro" class="ChartTop">

                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" >
                                    <div class="col-md-4">
                                        <div id="saleT10Cate" class="ChartTop">

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div id="saleT10brand" class="ChartTop">

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div id="saleT10Pro" class="ChartTop">

                                        </div>
                                    </div>

                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="RetailerT10Cate" class="Chartdown">

                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <div id="RetailerT10brand" class="ChartTop">

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div id="RetailerT10Pro" class="ChartTop">

                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div id="RetailerT10" class="ChartTop">

                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-4">
                                        <!--chat start-->
                                        <div class="slimScrollBar">
                                            <section class="panel">
                                                <header class="panel-heading" style="background-color:#ff865c;color:White; font-size:14px; font-family:Sans-Serif">
                                                    Notice Board
                                                </header>
                                                <div class="panel-body" id="noti-box">
                                                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" CssClass="table"    OnItemDataBound="Item_Bound" >
                                                        <ItemTemplate>
                                                            <div class="desc">
                                                                <asp:Label ID="lblInput" runat="server" Width="255px" style="padding-top:10px;padding-left:5px;"  Text='<%# Eval("comment") %>'></asp:Label><br/>
                                                                <div class="thumb" style="height:40px;padding-top:15px;padding-left:117px;">
                                                                    <span class="badge bg-theme" style="background-color:transparent;"></span>
                                                                    <asp:Label ID="daytime" runat="server" Text='<%# Eval("timee") %>' style="font-style:normal;font-size:10px;color:#a8b0b3;" ></asp:Label>
                                                                    <i class="fa fa-clock-o" style="color:#a6aeb1;"></i>
                                                                </div>
                                                            </div>
                                                            <asp:Label ID="cmttype" runat="server" Text='<%# Eval("Comment_Type") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </asp:Panel>
                    </div>
                </div>

            </form>
            <script type="text/javascript">
                $(function () {
                    "use strict";
                    //BAR CHART
                    var data = {
                        labels: ["January", "February", "March", "April", "May", "June", "July"],
                        datasets: [
                            {
                                label: "My First dataset",
                                fillColor: "rgba(220,220,220,0.2)",
                                strokeColor: "rgba(220,220,220,1)",
                                pointColor: "rgba(220,220,220,1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(220,220,220,1)",
                                data: [65, 59, 80, 81, 56, 55, 40]
                            },
                            {
                                label: "My Second dataset",
                                fillColor: "rgba(151,187,205,0.2)",
                                strokeColor: "rgba(151,187,205,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStrok
                            e: "rgba(151,187,205,1)",
                                data: [28, 48, 40, 19, 86, 27, 90]
                            }
                        ]
                    };
                    new Chart(document.getElementById("linechart").getContext("2d")).Line(data, {
                        responsive: true,
                        maintainAspectRatio: false,
                    });

                });
            // Chart.defaults.global.responsive = true;
            </script>
        </body>
    </html>  
</asp:Content>