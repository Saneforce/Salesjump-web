<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/DashBoard.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DashBoard</title>
    <style type="text/css">
		canvas,.ChartTop
        {
            border-radius: 6px;
        }
        .ChartTop
        {
            display: inline-block;
            width: 300px;
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
    </style>
    <script type="text/javascript">
        function genChart(Obj, arrDta, title) {
            var chart = new CanvasJS.Chart(Obj, {
				theme: "theme3",
                axisX: {
                    labelFormatter: function(e){
						return " ";
					},
                    tickLength: 0
					,lineThickness:0
					//,margin:-6
                },
                axisY: {

                    valueFormatString: " ",
                    tickLength: 0,
					lineThickness:0
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

    </script>
    <script src="../../JsFiles/canvasjs.min.js" type="text/javascript"></script>
</head>
<body style="background-color: #d6e9c6" class="home-one">
    <form id="form1" runat="server">
    <center>
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="Lbl_Area" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Year</asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="viewdrop" runat="server" SkinID="ddlRequired"
                        CssClass="ddl" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div id="T10brand" class="ChartTop">
        </div>
        <div id="T10Cate" class="ChartTop">
        </div>
        <div id="T10Pro" class="ChartTop">
        </div>
        <br />
        <div id="saleT10Cate" class="ChartTop">
        </div>
        <div id="saleT10brand" class="ChartTop">
        </div>
        <div id="saleT10Pro" class="ChartTop">
        </div>
        <br />
        <div id="RetailerT10Cate" class="ChartTop">
        </div>
        <div id="RetailerT10brand" class="ChartTop">
        </div>
        <div id="RetailerT10Pro" class="ChartTop">
        </div>
    </center>
    </form>
</body>
</html>
