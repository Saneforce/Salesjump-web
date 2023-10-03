<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="InshopRetailer_Sale_Report.aspx.cs" Inherits="MIS_Reports_InshopRetailer_Sale_Report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="Stylesheet" href="../css/kendo.common.min.css" />
    <link rel="Stylesheet" href="../css/kendo.default.min.css" />
    <style>
        .segment1 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
            /* border: 1px solid  #66a454   #d3252a  #f59303;*/
        }

            .segment1 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment1 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                }

                .segment1 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment1 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }

        [data-val='Rejected'] {
            color: red;
        }

        [data-val='Approved'] {
            color: green;
        }

        [data-val='Pending'] {
            color: blue;
        }

        th {
            white-space: nowrap;
            cursor: pointer;
        }

        #txtfilter_chzn {
            width: 300px !important;
            top: 10px;
        }

    </style>
    <form runat="server" id="frm1">
        <asp:HiddenField ID="hfilter" runat="server" />
        <asp:HiddenField ID="hffilter" runat="server" />
        <asp:HiddenField ID="hsfhq" runat="server" />
        <div class="row">
            <div class="col-lg-12 sub-header">
                Inshop Sales Activity
                <span style="float: right; margin-right: 80px;">
                    <img src="../img/Excel-icon.png" style="height: 50px; width: 50px; border-width: 0px; position: absolute; right: 15px;" onclick="exportToExcel()" />
                </span>
            </div>

            <div class="card" style="padding: 0px 16px">
                <div class="card-body table-responsive">
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                     
                                <tr>
                                    <th>SlNo</th>
                                    <th style="width:100px">Entry Date</th>
                                    <th>Field Force</th>
                                    <th>Retailer Name</th>                                    
                                    <th>Product Name</th>                                    
                                    <th>Opening Stock</th>
                                    <th>Sales order</th>                                    
                                    <th>Closing Stock</th>
				    <th>Order Value</th>
                                </tr>
                           
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>

    </form>
    <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var sftyp ='<%=Session["sf_type"]%>';
        var SFHQs = [];
        var SFMGRs = [];
        var Arrsum = [];
        var AllOrders2 = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;//
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0;
         $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );

        function loadData() {
            dt = new Date();
            $m = dt.getMonth() + 1, $y = dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "InshopRetailer_Sale_Report.aspx/GetDetails",
                data: "{'SF':'<%=sfo%>','Sl_No':'<%=Sl_No%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function loadPgNos() {
            for (il = 0; il < Orders.length; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (fffilterkey != "AllFF") {
                Orders = Orders.filter(function (a) {
                    return a.Approved_By == fffilterkey;
                })
            }
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.dvMin == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    //,WrkDate,DistName,Rut_Name,Ret_Name,Promotor,Start_Time,End_Time,Value,Rmks
                    tr = $("<tr class='trclick'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Entry_Date + "</td><td>" + Orders[$i].Sf_Name + "</td><td>" + Orders[$i].Retailer_Name + "</td><td>" + Orders[$i].product_Name + "</td><td>" + Orders[$i].Opening_Stock + "</td><td>" + Orders[$i].Order_sale + "</td><td>" + Orders[$i].Closing + "</td><td>" + Orders[$i].Amt + "</td>");
                    $("#OrderList TBODY").append(tr);
                }
            }
            //$("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            //loadPgNos();
        }

        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            $(".header").hide();
            $(".left-side").hide();
            $(".right-side").css("margin-left", "0px");

            loadData();
            setTimeout(function () {
                $("#fc_frame").hide();
            }
                , 500);

        });
          function exportToExcel() {
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
            htmls = document.getElementById("OrderList").innerHTML;

            var ctx = {
                worksheet: 'Inshop_sale_Activity',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_Inshop_sale_Activity' + '.xls';

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }

    </script>

</asp:Content>



