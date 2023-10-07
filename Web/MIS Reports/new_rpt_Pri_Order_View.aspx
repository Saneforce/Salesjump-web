<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="rpt_Pri_Order_View.aspx.cs"
    Inherits="MIS_Reports_rpt_Pri_Order_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Primary Order View</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="css/morris/morris.css" rel="stylesheet" type="text/css" />
    <link href="css/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
    <link href="css/datepicker/datepicker3.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-daterangepicker/3.0.5/daterangepicker.css" />
    <link href="css/iCheck/all.css" rel="stylesheet" type="text/css" />
    <link href='https://fonts.googleapis.com/css?family=Lato' rel='stylesheet' type='text/css' />
    <link href="css/Stockist.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/chosen.css" rel='stylesheet' type='text/css' />
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script src="../js/lib/xls.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/xlsx.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/import_data.js" type="text/javascript"></script>
    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">

        function popUp(sfCode, divcode, year) {
            var dtd = new Date();
            var cmnth = dtd.getMonth() + 1;
            var sURL = "rpt_stk_wise.aspx?sf_code=" + sfCode + "&div_code=" + divcode + "&Year=" + year + "&SfR=" + sfCode + "&Mnth=" + cmnth;
            window.open(sURL, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }

        var array1 = new Array();
        var array2 = new Array();
        var n = 2; //Total table
        for (var x = 1; x <= n; x++) {
            array1[x - 1] = x;
            array2[x - 1] = x + 'th';
        }

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
                , templateend = '</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'
                , body = '<body>'
                , tablevar = '<table>{table'
                , tablevarend = '}</table>'
                , bodyend = '</body></html>'
                , worksheet = '<x:ExcelWorksheet><x:Name>'
                , worksheetend = '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>'
                , worksheetvar = '{worksheet'
                , worksheetvarend = '}'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                , wstemplate = ''
                , tabletemplate = '';

            return function (table, name, filename) {
                var tables = table;

                for (var i = 0; i < tables.length; ++i) {
                    wstemplate += worksheet + worksheetvar + i + worksheetvarend + worksheetend;
                    tabletemplate += tablevar + i + tablevarend;
                }

                var allTemplate = template + wstemplate + templateend;
                var allWorksheet = body + tabletemplate + bodyend;
                var allOfIt = allTemplate + allWorksheet;

                var ctx = {};
                for (var j = 0; j < tables.length; ++j) {
                    ctx['worksheet' + j] = name[j];
                }

                for (var k = 0; k < tables.length; ++k) {
                    var exceltable;
                    if (!tables[k].nodeType) exceltable = document.getElementById(tables[k]);
                    ctx['table' + k] = exceltable.innerHTML;
                }

                //document.getElementById("dlink").href = uri + base64(format(template, ctx));
                //document.getElementById("dlink").download = filename;
                //document.getElementById("dlink").click();

                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })();

    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };


    </script>
    <script  type="text/javascript" src="../JsFiles/canvasjs.min.js"></script>
    <script type="text/javascript">

        function genChart(arrDta) {

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "theme2", //theme1
                title: {
                    text: "Categorywise Order"
                },
                animationEnabled: true,   // change to true
                data: [{
                    type: "pie",      // Change type to "bar", "area", "spline", "pie",etc.
                    dataPoints: arrDta
                }]
            });
            chart.render();
        }
    </script>
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        td {
            padding: 2px 5px;
        }

        .subTot {
            font-size: 11pt;
            font-weight: bold;
        }

        .GrndTot {
            font-size: 13pt;
            font-weight: bold;
        }

        .remove {
            text-decoration: none;
        }

        .TopButton {
            border-color: Black;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
            height: 25px;
            width: 60px;
        }
    </style>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../pluginexcel/jquery.table2excel.js"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
            res();
            $('.header').click(function () {
                $(this).nextUntil('tr.header').slideToggle("slow");
            });
            $('.hidecll').closest('td').hide();
            $('.hidecell').hide();

            if ('<%=div_code%>' == '107') {
                $('.hidecell').show();
                $('.hidecll').closest('td').show();
            }

            $(document).on('click', "#btnClose", function () {
                window.close();
            });

            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('content');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'Primary_View' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                location.reload();
                //$("body").html(originalContents);
                return false;
            });


        });

        function res() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Pri_order_view.aspx/Getnewtable",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    var str = "<thead style='background: #87ade5;'>\
                    <tr><th> </th>\
                    <th>Sf code</th>\
                    <th>distributor</th>\
                    <th>Order taken By</th>\
                    <th>Order Id</th>\
                    <th>Pay type</th>\
                    <th>Collection Amount</th>\
                    <th>Status</th>\
                    <th>Order value</th>\
                   </tr></thead><tbody>";
                    $("#1").append(str);
                    var str1 = '';
                    var str2 = '';
                    var str3 = '';
                    var totl = 0;
                    for (i = 0; i < AllOrders.length; i++) {
                        console.log(AllOrders[i]);
                        console.log('next');
                        str3 = '';
                        str2 = '';
                        
                        
                        str3 += '<tr class="info" style="display:none;">';
                            str3 += '<th style="background: bisque;">Sf code</th>';
                            str3 += '<th style="background: bisque;">distributor</th>';
                            str3 += '<th style="background: bisque;">Order taken By</th>';
                            str3 += '<th style="background: bisque;">Order Id</th>';
                            str3 += '<th style="background: bisque;">Pay type</th>';
                            str3 += '<th style="background: bisque;">Collection Amount</th>';
                            str3 += '<th style="background: bisque;">Reference no</th>';
                            str3 += '<th style="background: bisque;">status no</th>';
                            str3 += '<th style="background: bisque;">Order value</th>';
                            str3 += '</tr>';
                        for (j = 0; j < Orders.length; j++) {
                           if (AllOrders[i].Trans_Sl_No == Orders[j].refId) {
                                str2 +='<tr  class="" style="display:none;" data-target="' + AllOrders[i].Sf_Name + '">\
                                    <td style="background: #ffff90;">'+ Orders[j].Sf_Code + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].Stockist_Name + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].Sf_Name + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].Trans_Sl_No + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].Pay_Type + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].Collected_Amount + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].refId + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].status + '</td>\
                                    <td style="background: #ffff90;">'+ Orders[j].Order_Value + '</td></tr>';
                            }
                        }
                        if (str2 != '') {
                            if (AllOrders[i].refId == '') {
                                str1 = '<tr data-toggle="collapse" id="' + AllOrders[i].Trans_Sl_No + '" data-target="' + AllOrders[i].Sf_Name + '" class="header"><td style="color: #87ade5;"><span class="glyphicon glyphicon-plus-sign"></span></td>\
                                <td>'+ AllOrders[i].Sf_Code + '</td>\
                                 <td>'+ AllOrders[i].Stockist_Name + '</td>\
                                <td>'+ AllOrders[i].Sf_Name + '</td>\
                                <td>'+ AllOrders[i].Trans_Sl_No + '</td>\
                                 <td>'+ AllOrders[i].Pay_Type + '</td>\
                                 <td>'+ AllOrders[i].Collected_Amount + '</td>\
                                <td>'+ AllOrders[i].status + '</td>\
                                <td>'+ AllOrders[i].Order_Value + '</td></tr>';
                                totl += parseFloat(AllOrders[i].Order_Value);
                                $("#1").append(str1);
                            }
                            $("#1").append(str3);
                            $("#1").append(str2);
                        }
                        else {
                            if (AllOrders[i].refId == '') {
                            str1 = '<tr data-toggle="collapse" id="' + AllOrders[i].Trans_Sl_No + '" data-target="' + AllOrders[i].Sf_Name + '" class="header"><td style="color: #87ade5;"><span class="glyphicon "></span></td>\
                                <td>'+ AllOrders[i].Sf_Code + '</td>\
                                 <td>'+ AllOrders[i].Stockist_Name + '</td>\
                                <td>'+ AllOrders[i].Sf_Name + '</td>\
                                <td>'+ AllOrders[i].Trans_Sl_No + '</td>\
                                 <td>'+ AllOrders[i].Pay_Type + '</td>\
                                 <td>'+ AllOrders[i].Collected_Amount + '</td>\
                                <td>'+ AllOrders[i].status + '</td>\
                                <td>'+ AllOrders[i].Order_Value + '</td></tr>';
                            totl += parseFloat(AllOrders[i].Order_Value);
                            $("#1").append(str1);
                        }
                        }
                         
                        //$("#ord").append(str3);
                        //$("#ord").append(str2);
                       

                    }
                    $("#1").append('<tr class="header"><td colspan="7"></td><td style="text-align:right;">Total</td><td>'+totl+'</td>');
                    $("#1").append("<tbody></table>");
                },
                error: function (result) {
                }
            });
        }
        function exportexcel() {
            $("#pnlContents").table2excel({

                filename: "TodayOrderView",
                fileext: ".xls"
            });
        };
        $(function() {
				$(".export").click(function(e){
					$("#ord, #GridViewcat").table2excel({

							exclude: ".noExl",
							name: "Excel Document Name",
							filename: "myFileName" + new Date().toISOString().replace(/[\-\:\.]/g, "") + ".xls",
							fileext: ".xls",
							exclude_img: true,
							exclude_links: true,
							exclude_inputs: true,
							preserveColors: true
						});
					
				});
				
			});
    </script>

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="80%" align="center">
                        <a href="javascript:popUp('<%=sfCode%>','<%=divcode%>','<%=year%>')" >
                  <b> Closing Stock  </b> </a> 
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btnPrint" OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                <a id="pdfexport" class="btn btnPdf" type="button" value="PDF" height="35px"  onclick="generate()"" >  </a>
                                    <asp:LinkButton ID="btnPdf" runat="server" class="btn btnPdf" Visible="false" />
                                </td>
                                <td>
                                    <%-- onclick="tablesToExcel(array1, array2, 'myfile.xls')" --%>
                                  <a id="Export" class="btn btnExcel exportToExcel" type="button"  height="35px" onclick="tablesToExcel(array1, array2, 'myfile.xls')" > </a>  
                                    
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnClose" runat="server" type="button" Style="padding: 0px 20px;" 
                                         href="javascript:window.open('','_self').close();" class="btn btnClose" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
   
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div class="container">
	<div class="col-md-12">
    	<%--<div class="panel panel-default">
				<div class="panel-heading">
					Primary Order View
				</div>
        <div class="panel-body">--%>
					<table id="1" class="table table-condensed table-striped">
                        </table>
           <%-- </div>--%>
            </div>

	</div>
            </div>
      <%--  <table border="0" id='1' width="90%">


        <tr><td style=" text-align:center; font-size:x-large;  font-weight: bold;font-family: Andalus;"> <asp:Label ID="lblHead" Text="Retail Lost  Details" runat="server"></asp:Label></td></tr>
            <tr align="right">
                <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus;">
                    FieldForce Name:
                    <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label2"  runat="server" style="float:right" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvtotalorder" runat="server" Width="100%" HorizontalAlign="Center"
                        OnDataBound="OnDataBound" OnRowCreated="OnRowCreated" BorderWidth="1px" CellPadding="2"
                        EmptyDataText="No Data found for View" AutoGenerateColumns="false" class="newStly"
                        Style="border-collapse: collapse;">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Distributor" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="stockist" Style="white-space: nowrap" runat="server" Font-Size="9pt"
                                        Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
							<asp:TemplateField HeaderText="Zone"  HeaderStyle-CssClass="hidecell" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label CssClass="hidecll" ID="zn" Style="white-space: nowrap" runat="server" Font-Size="9pt"
                                        Text='<%#Eval("zone")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Taken By" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--	<asp:TemplateField HeaderText="Route" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Route_name" runat="server" Font-Size="9pt" Text='<%#Eval("routename")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Retailer_name" runat="server" Font-Size="9pt" Text='<%#Eval("retailername")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="netval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "net_weight_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

									<asp:TemplateField HeaderText="Free" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="freept" runat="server" Font-Size="9pt" Text='<%#Eval("free")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                           <%-- <asp:TemplateField HeaderText="Pay Type" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="toval" runat="server" Font-Size="9pt" Text='<%#Eval("Pay_Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Collected Amount" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="discountpri" runat="server" Font-Size="9pt" Text='<%#Eval("Collected_Amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:LinkButton ID="status"  runat="server" ForeColor="Red" Font-Size="9pt" Text='<%#Eval("status")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="orderval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_Value", "{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>--%>
        <table id='2' width="100%">
            <tr>
                <td>
                    <tr>
                        <td align="left" width="50%">
                            <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Bold="True" Font-Names="Andalus"
                                Font-Underline="true" Text="Categorywise Order"></asp:Label>
                            <asp:GridView ID="GridViewcat" runat="server" Width="100%" CssClass="newStly" HorizontalAlign="Center"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                HeaderStyle-BackColor="#33CCCC" HeaderStyle-HorizontalAlign="Center" BorderColor="Black" OnDataBound="catOnDataBound"
                                BorderStyle="Solid">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Category" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo1" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Cat_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" runat="server" Font-Size="9pt" Text='<%#Eval("quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="stval" runat="server" Font-Size="9pt" Text='<%#Eval("value")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                        <td align="right" valign="top">
                            <div id="chartContainer" style="text-align: center; height: 180px; width: 95%; padding-top: 30px;">
                            </div>
                        </td>
                    </tr>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    <script type="text/javascript">
        function PostToNewWindow(str) {
            location.reload();
            return false;
        }
        function generate() {

            //  var doc = new jsPDF('l', 'pt');
            var doc = new jsPDF('l', '', '', '');

            var res = doc.autoTableHtmlToJson(document.getElementById("gvtotalorder"));
            var res1 = doc.autoTableHtmlToJson(document.getElementById("GridViewcat"));



            var header = function (data) {
                doc.setFontSize(15);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("Primary Order View", data.settings.margin.top, 30);
            };
            var options = {
                beforePageContent: header,
                margin: {
                    top: 40
                },
                startY: doc.autoTableEndPosY() + 50
            };
            doc.autoTable(res.columns, res.data, options);
            var header1 = function (data) {
                doc.setFontSize(12);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("Category-wise Order", data.settings.margin.left, doc.autoTableEndPosY() + 30);
            };
            var options1 = {
                beforePageContent: header1,
                margin: {
                    top: 40
                },
                startY: doc.autoTableEndPosY() + 50
            };



            doc.autoTable(res1.columns, res1.data, options1);

            doc.save("PrimaryOrderView.pdf");
        }</script>
    </form>
</body>
</html>
