<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Secondary_Order.aspx.cs" Inherits="MIS_Reports_Rpt_Secondary_Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Order View</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link type="text/css" href="../css/Stockist.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="70%" align="center">
                            <asp:Label ID="lblHead" SkinID="lblMand" Font-Bold="true" Font-Underline="true"
                                runat="server" Font-Size="Medium"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" />
                            <button class="btn btnExcel" id="btnExport" type="button"></button>
                            <input id="pdfexport" type="linkbutton" style="padding: 0px 20px;display:none;" class="btn btnPdf" value="PDF" height="35px" onclick="generate()">
                            <asp:LinkButton ID="LinkButton1" runat="Server" Style="padding: 0px 20px;" class="btn btnClose" OnClientClick="javascript:window.open('','_self').close();" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%">
            <table border="0" id='1' width="90%" style="margin: auto;">
                <tr>
                    <td width="100%" colspan="2">
                        <asp:HiddenField ID="HiddenField1" runat="server" />

                        <table id="ordertbl" class="table table-bordered table-hover table-sm newStly">
                            <thead></thead>
                            <tbody></tbody>
                            <tfoot></tfoot>
                        </table>
                    </td>
                </tr>
            </table>
            <table id='2' style="width: 90%; margin: auto;">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td width="50%" valign="top">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Bold="True" Font-Underline="true" Text="Categorywise Order"></asp:Label>
                        <asp:GridView ID="GridViewcat" runat="server" Width="100%" CssClass="newStly" OnDataBound="catOnDataBound"
                            HorizontalAlign="Center"
                            BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                            AutoGenerateColumns="false" HeaderStyle-BackColor="#33CCCC"
                            HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                    HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo1" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Category" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                    HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Cat_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                    HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lnetweight" runat="server" Font-Size="9pt" Text='<%#Eval("net_weight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                    HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lquantity" runat="server" Font-Size="9pt" Text='<%#Eval("quantity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                    HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="cval" runat="server" Font-Size="9pt" Text='<%#Eval("value")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                    <td align="right" valign="top">
                        <table id="Rtsummary">
                            <thead>
                                <tr>
                                    <td>
                                        <label>Retail Summary</label></td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <label id="totret" style="font-weight: 100;">Total Retailers Visited ( TC ): </label>
                                    </td>
                                    <td>
                                        <label id="ordret" style="font-weight: 100;">Ordered Retailers ( EC ): </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="newret" style="font-weight: 100;">New Outlets Created: </label>
                                        <td>
                                            <label id="neworet" style="font-weight: 100;">Newly Order Given Outlet: </label>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="totoval" style="font-weight: 100;">Total Order Value: </label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
           <div id="chartContainer" style="text-align:center;height:180px; width: 95%;"></div>
    <div class="table-responsive">
    </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script src="../JsFiles/canvasjs.min.js"></script>
    <script type="text/javascript" src="../js/plugins/table2excel.js"></script>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script type="text/javascript">
        var AllOrderDets = [], OrderDets = [], AllOrderHead = [], OrderHead = [], AllProdGroup = [], ProdGroup = [], AllProducts = [], Products = [], AllUOM = [], UOM = [];
        function getUOM() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Secondary_Order.aspx/GetUOM",
                data: "{'divcode':'<%=divcode%>','SF':'<%=Sf_Code%>','FDt':'<%=FDate%>','TDt':'<%=TDate%>','subdiv':'<%=subdiv_code%>'}",
                dataType: "json",
                success: function (data) {
                    AllUOM = JSON.parse(data.d);
                    UOM = AllUOM;
                },
                error: function (rs) {

                }
            });
        }
        function getOrderDets() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Secondary_Order.aspx/GetOrderQty",
                data: "{'divcode':'<%=divcode%>','SF':'<%=Sf_Code%>','FDt':'<%=FDate%>','TDt':'<%=TDate%>','subdiv':'<%=subdiv_code%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrderDets = JSON.parse(data.d);
                    OrderDets = AllOrderDets;
                },
                error: function (rs) {

                }
            });
        }
        function getOrderHead() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Secondary_Order.aspx/GetOrderHead",
                data: "{'divcode':'<%=divcode%>','SF':'<%=Sf_Code%>','FDt':'<%=FDate%>','TDt':'<%=TDate%>','subdiv':'<%=subdiv_code%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrderHead = JSON.parse(data.d);
                    OrderHead = AllOrderHead;
                },
                error: function (rs) {

                }
            });
        }
        function getPGroup() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Secondary_Order.aspx/GetOrderGroup",
                data: "{'divcode':'<%=divcode%>','SF':'<%=Sf_Code%>','FDt':'<%=FDate%>','TDt':'<%=TDate%>','subdiv':'<%=subdiv_code%>'}",
                dataType: "json",
                success: function (data) {
                    AllProdGroup = JSON.parse(data.d);
                    ProdGroup = AllProdGroup;
                },
                error: function (rs) {

                }
            });
        }
        function getProducts() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Secondary_Order.aspx/GetOrderProducts",
                data: "{'divcode':'<%=divcode%>','SF':'<%=Sf_Code%>','FDt':'<%=FDate%>','TDt':'<%=TDate%>','subdiv':'<%=subdiv_code%>'}",
                dataType: "json",
                success: function (data) {
                    AllProducts = JSON.parse(data.d);
                    Products = AllProducts;
                },
                error: function (rs) {

                }
            });
        }
        function loadData() {
            getPGroup(); getProducts(); getOrderHead(); getUOM(); getOrderDets(); ReloadTable();
        }
        function ReloadTable() {
            $('#ordertbl thead').html('');
            $('#ordertbl tbody').html('');
            $('#ordertbl tfoot').html('');
            $htr = '<tr><th rowspan="3">Order Date</th><th rowspan="3">Distributor Name</th><th rowspan="3">Distributor ERP Code</th><th rowspan="3">Order Taken By</th><th rowspan="3">Reporting Manager</th><th rowspan="3">Route</th><th rowspan="3">Retailer Name</th>';
            $htr2 = '<tr>';
            $htr3 = '<tr>';
            $htr4 = '<tr>';
            for (var i = 0; i < ProdGroup.length; i++) {
                $htr += '<th style="text-align:center;" colspan="' + ((ProdGroup[i].t) * (UOM.length)) + '">' + ProdGroup[i].Product_Grp_Name + '</th>';
                var filtpro = Products.filter(function (a) {
                    return a.Product_Grp_Code == ProdGroup[i].Product_Grp_Code
                })
                for (var j = 0; j < filtpro.length; j++) {
                    if (filtpro.length > 0) {
                        $htr2 += '<th colspan="' + UOM.length + '">' + filtpro[j].Product_Detail_Name + '</th>';
                        for (var k = 0; k < UOM.length; k++) {
                            $htr3 += '<th>' + UOM[k].Move_MailFolder_Name + '</th>';
                        }
                    }
                    else
                        $htr2 += '<td></td>';
                }
            }
            $htr += '<th rowspan="3">Net Weight</th><th rowspan="3">Order Value</th></tr>';
            $htr2 += '</tr>';
            $htr3 += '</tr>';
            $('#ordertbl thead').append($htr);
            $('#ordertbl thead').append($htr2);
            $('#ordertbl thead').append($htr3); $gtot = 0; $netw = 0;
            $htr4 += '<tr><td colspan="7">Total</td>';
            var totarr = [];
            for (var x = 0; x < OrderHead.length; x++) {
                ar = 0;
                $btr = '<tr><td>' + OrderHead[x].OrderDate + '</td><td>' + OrderHead[x].Stockist_name + '</td><td>' + OrderHead[x].ERP_Code + '</td><td>' + OrderHead[x].SF_Name + '</td><td>' + OrderHead[x].RSF + '</td><td>' + OrderHead[x].routename + '</td><td>' + OrderHead[x].retailername + '</td>';
                for (var i = 0; i < ProdGroup.length; i++) {
                    var filtpro = Products.filter(function (a) {
                        return a.Product_Grp_Code == ProdGroup[i].Product_Grp_Code
                    })
                    for (var j = 0; j < filtpro.length; j++) {
                        for (var k = 0; k < UOM.length; k++) {
                            var filtqty = OrderDets.filter(function (a) {
                                return (a.Trans_Sl_No == OrderHead[x].Trans_Sl_No)
                                    && (a.Product_Code == filtpro[j].Product_Detail_Code) && (a.Uom_Id == UOM[k].Move_MailFolder_Id)
                            });
                            $btr += '<td>' + ((filtqty[0] != undefined) ? filtqty[0].Con_Qty : "") + '</td>';
                            totarr[ar] = ((filtqty[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + filtqty[0].Con_Qty) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                            ar++;
                        }
                    }
                }
                $btr += '<td>' + OrderHead[x].net_weight_value + '</td><td>' + parseFloat(OrderHead[x].Order_value).toFixed(2) + '</td></tr>';
                $netw += parseFloat(OrderHead[x].net_weight_value);
                $gtot += parseFloat(OrderHead[x].Order_value);
                $('#ordertbl tbody').append($btr);
            }
            for (var i = 0; i < totarr.length; i++) {
                $htr4 += '<td>' + totarr[i] + '</td>';
            }
            $htr4 += '<td>' + $netw.toFixed(2) + '</td><td>' + $gtot.toFixed(2) + '</td></tr>'
            $('#ordertbl tfoot').append($htr4);
        }
        $(document).ready(function () {
            getretdetails();
            loadData();
            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#pnlContents").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
            $("#btnExport").click(function (e) {
                window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id=pnlContents]').html()));
                e.preventDefault();
            });
        });
        function generate() {
            // var doc = new jsPDF('l', 'pt');
            //var doc = new jsPDF(l, '', '', '');
            var doc = new jsPDF('l', 'mm', [1000, 1414]);
            var res = doc.autoTableHtmlToJson(document.getElementById("gvtotalorder"));
            var res1 = doc.autoTableHtmlToJson(document.getElementById("GridViewcat"));
            var header = function (data) {
                doc.setFontSize(10);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("TotalOrderView", data.settings.margin.top, 30);
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
                doc.setFontSize(13);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("Categorywise", data.settings.margin.left, doc.autoTableEndPosY() + 30);
            };
            var options1 = {
                beforePageContent: header1,
                margin: {
                    top: 40
                },
                startY: doc.autoTableEndPosY() + 50
            };
            doc.autoTable(res1.columns, res1.data, options1);
            doc.save("TotalOrderView.pdf");
        }
        function genChart(arrDta) {

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "theme2", //theme1
                title: {
                    text: "Categorywise Order"
                },
                animationEnabled: true,   // change to true
                data: [{
                    type: "pie",       // Change type to "bar", "area", "spline", "pie",etc.
                    dataPoints: arrDta
                }]
            });
            chart.render();
        }
        function getretdetails() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false, url: "Rpt_Secondary_Order.aspx/GetRetDetails",
                dataType: "json",
                success: function (data) {
                    var gret = [];
                    gret = JSON.parse(data.d);
                    if (gret.length > 0) {
                        $('#totret').html('Total Retailers Visited ( TC ) : <b>' + gret[0].Total_call + "</b>")
                        $('#ordret').html('Ordered Retailers ( EC ) : <b>' + gret[0].Productive + "</b>")
                        $('#newret').html('New Outlets Created : <b>' + gret[0].drcount + "</b>")
                        $('#neworet').html('Newly Order Given Outlet : <b>' + gret[0].pdrcount + "</b>")
                        $('#totoval').html('Total Order Value : <b>' + gret[0].Order_Value + "</b>")
                    }
                    else {
                        $('#Rtsummary').hide();
                    }
                },

                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
    </script>
</body>
</html>
