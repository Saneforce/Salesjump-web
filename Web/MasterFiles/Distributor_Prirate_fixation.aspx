<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Distributor_Prirate_fixation.aspx.cs" Inherits="MasterFiles_Distributor_Prirate_fixation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="frm1" runat="server">
        <div class="row">
            <div class="col-lg-4 sub-header">Customers </div>
            <div class="col-lg-8 sub-header">Effective Date  <span style="float: right;"><a href="#" class="btn btn-primary btn-update" id="save">SAVE</a></span></div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-4" style="min-width: 338px !important;">
                <select id="ddl_dist" class="ddrt" name="route"></select>
            </div>
            <div class="col-lg-4" style="min-width: 338px !important; max-width: 340px !important">
                <input type="date" id="dt_dist" class="form-control" style="height: 25px !important" name="route"></input>
            </div>
            <button class="btn btn-primary go" type="button" style="display: none;">Go</button>
        </div>
       <%-- <div class="col-lg-3">
            <div class="form-group">
                <input id="fileupload" type="file" name="files[]">
            </div>
        </div>
        <div class="col-lg-3">
            <div class="form-group">
                <button class="exportToExcel">Export to XLS</button>
            </div>
        </div>--%>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="float: right">
                        Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="100">100</option>
                            <option value="250">250</option>
                            <option value="500">500</option>
                            <option value="1000">1000</option>
                        </select>
                        entries</label>

                </div>
                <table class="table table-hover table-bordered  " id="OrderListexcel" style="display: none">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl NO.</th>
                            <th style="text-align: left">Customer_Code</th>
                            <th style="text-align: left">Product_Code</th>
                            <th style="text-align: left">Product_Name</th>
                            <th style="text-align: left">MRP</th>
                            <th style="text-align: left">Rate</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl NO.</th>
                            <th style="text-align: left">Customer Code</th>
                            <th style="text-align: left">Product Code</th>
                            <th style="text-align: left">Product Name</th>
                            <th style="text-align: left">MRP</th>
                            <th style="text-align: left">Rate</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="row" style="padding: 5px 0px">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous " id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <script src="../js/jquery.table2excel.js"></script>
        <script>
            $(function () {
                $(".exportToExcel").click(function (e) {
                    $("#OrderListexcel").table2excel({

                        // exclude CSS class
                        exclude: ".noExl",
                        name: "Worksheet Name",
                        filename: "SomeFile",//do not include extension
                        fileext: ".xls" // file extension
                    });

                });
            });
				
		</script>
        <script type="text/javascript">
            var AllOrders = []; Allrate = [];
            var idx;
            var sf, distcode, prodcode, mrp, pts;
            var divcode; filtrkey = '0';
            var Orders = []; pgNo = 1; PgRecords = 1000; TotalPg = 0; searchKeys = "Dist_Code,Product_Detail_Code,Product_Detail_Name,";
            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    ReloadTable();
                }
            );

            //excel import
            var ExcelToJSON = function () {
                this.parseExcel = function (file) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        var data = e.target.result;
                        var workbook = XLSX.read(data, {
                            type: 'binary'
                        });
                        workbook.SheetNames.forEach(function (sheetName) {
                            var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                            var productList = JSON.parse(JSON.stringify(XL_row_object));

                            $('#OrderList tbody').html();
                            var rows = $('#OrderList tbody');
                            console.log(productList)
                            for (i = 0; i < productList.length; i++) {
                                var columns = Object.values(productList[i])
                                rows.append(`
                        <tr>
                            <td>${i + 1}</td>    
                            <td>${productList[i].Customer_Code}</td>
                            <td>${productList[i].Product_Code}</td>
                            <td>${productList[i].product_name}</td>
                            <td>${productList[i].MRP}</td>
                            <td>${productList[i].Rate}</td>
                        </tr>
                    `);
                            }

                        })
                    };
                    reader.onerror = function (ex) {
                        console.log(ex);
                    };

                    reader.readAsBinaryString(file);
                };
            };

            function handleFileSelect(evt) {
                var files = evt.target.files; // FileList object
                var xl2json = new ExcelToJSON();
                xl2json.parseExcel(files[0]);
            }

            //document.getElementById('fileupload').addEventListener('change', handleFileSelect, false);
            //excel import
            $("#download_excel").on("click",
                function () {
                    exportTableToExcel('OrderListexcel', 'excelrate')
                });
            $("#save").on("click",
                function () {
                    if ($("#dt_dist").val() != '') {
                        AllOrders = [];
                        var divcode = '<%= Session["div_code"] %>';
                        //pgNo = 1;
                        //PgRecords = 1000;
                        //ReloadTable();
                        $('#OrderList > tbody  > tr').each(function (index, tr) {
                            itm = {}
                            itm.distcode = $(this).find('td').eq(1).html();
                            itm.prodcode = $(this).find('td').eq(2).html();
                            itm.mrp = $(this).find('td').eq(4).find('input').val();
                            itm.pts = $(this).find('td').eq(5).find('input').val();
                            Allrate.push(itm);
                        });
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Distributor_Prirate_fixation.aspx/save_ratedetails",
                            data: "{'div_code':'" + divcode + "','sfcode':'" + Allrate[0].distcode + "','dt':'" + $("#dt_dist").val() + "','proddetails':'" + JSON.stringify(Allrate) + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    var res = JSON.parse(data.d);
                                    if (res[0].RESULT == "UPDATED") {
                                        alert('Updated Successfully'); location.reload();
                                    }
                                    else if (res[0].RESULT == "FAILED") {
                                        alert('something went wrong'); return false;
                                    }
                                    else if (res[0].RESULT == "ALREADY_EXIST") {
                                        alert('alreadly exist for this effective date'); return false;
                                    }
                                }
                                else {
                                    alert(data.d);
                                }
                            },
                            error: function (result) {
                                approve = 0;
                            }
                        });

                    }
                    else {

                        alert('select Effective Date.');
                        $("#dt_dist").focus();
                       // exportTableToExcel('OrderListexcel', 'excelrate')
                        return false;
                    }
                }
            );
            $(document).ready(function () {
                var div_code = '<%= Session["div_code"] %>';
				var now = new Date();
                var day = ("0" + now.getDate()).slice(-2);
                var month = ("0" + (now.getMonth() + 1)).slice(-2);
                var today = now.getFullYear() + "-" + (month) + "-" + (day);
                $('#dt_dist').attr("min", today);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Distributor_Prirate_fixation.aspx/getdata",
                    dataType: "json",
                    success: function (data) {

                        var sf = $("[id*=ddl_dist]");
                        sf.empty().append('<option selected="selected" value="0">Select Distributor</option>');
                        for (var i = 0; i < data.d.length; i++) {
                            sf.append($('<option value="' + data.d[i].Distcode + '">' + data.d[i].Distname + '</option>')).trigger('chosen:updated').css("width", "100%");;;
                        };

                        $('#ddl_dist').chosen();
                    },
                    error: function (sfcode) {
                        alert(JSON.stringify(result));
                    }
                });

                $('#ddl_dist').on('change', function () {
                    if ($('#ddl_dist :selected').val() != "0")
                        loadData($('#ddl_dist :selected').val());
                    else
                        return false;
                });

            });
            function loadData(sf) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Distributor_Prirate_fixation.aspx/getDistRate",
                    data: "{'sfcode':'" + sf + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        ReloadTable();
                    },
                    error: function (result) {
                    }
                });
            }

            function ReloadTable() {
                $("#OrderList TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;
                for ($i = st; $i < st + PgRecords; $i++) {
                    //var filtered = Orders.filter(function (x) {
                    //    return x.Sf_Code != 'admin';
                    //})
                    if ($i < Orders.length) {
                        tr1 = $("<tr></tr>");
                        tr = $("<tr></tr>");
                        //var hq = filtered[$i].Sf_Name.split('-');
                        slno = $i + 1;
                        $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].Dist_Code + '</td><td>' + Orders[$i].Product_Detail_Code + '</td><td>' + Orders[$i].Product_Detail_Name +
                            '</td><td><input type="text" value=' + Orders[$i].MRP + '></td><td ><input type="text" value=' + Orders[$i].PTS +
                            '></td>');
                        $(tr1).html('<td>' + slno + '</td><td>' + Orders[$i].Dist_Code + '</td><td>' + Orders[$i].Product_Detail_Code + '</td><td>' + Orders[$i].Product_Detail_Name +
                            '</td><td> ' + Orders[$i].MRP + '</td><td>' + Orders[$i].PTS +
                            '</td>');
                        $("#OrderList TBODY").append(tr);
                        $("#OrderListexcel TBODY").append(tr1);
                  <%--  if(<%=Session["sf_type"]%>==4){
                        $('.sfedit').css('display','none');
                    }--%>
                        hq = [];
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")


                loadPgNos();
            }
            $("#tSearchOrd").on("keyup", function () {
                if ($(this).val() != "") {
                    shText = $(this).val().toLowerCase();
                    Orders = AllOrders.filter(function (a) {
                        chk = false;
                        $.each(a, function (key, val) {
                            if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                                chk = true;
                            }
                        })
                        return chk;
                    })
                }
                else
                    Orders = AllOrders
                pgNo = 1;
                ReloadTable();
            })
            function loadPgNos() {
                prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                $(".pagination").html("");
                TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
                if (isNaN(prepg)) prepg = 0;
                if (isNaN(Nxtpg)) Nxtpg = 2;
                //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
                selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
                if ((Nxtpg) == pgNo) {
                    selpg = (parseInt(TotalPg)) - 7;
                    selpg = (selpg > 1) ? selpg : 1;
                }
                spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
                for (il = selpg - 1; il < selpg + 7; il++) {
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

            function exportTableToExcel(tableId, filename) {
                let dataType = 'application/vnd.ms-excel';
                let extension = '.xls';

                let base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };

                let template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                let render = function (template, content) {
                    return template.replace(/{(\w+)}/g, function (m, p) { return content[p]; });
                };

                let tableElement = document.getElementById(tableId);

                let tableExcel = render(template, {
                    worksheet: filename,
                    table: tableElement.innerHTML
                });

                filename = filename + extension;

                if (navigator.msSaveOrOpenBlob) {
                    let blob = new Blob(
                        ['\ufeff', tableExcel],
                        { type: dataType }
                    );

                    navigator.msSaveOrOpenBlob(blob, filename);
                } else {
                    let downloadLink = document.createElement("a");

                    document.body.appendChild(downloadLink);

                    downloadLink.href = 'data:' + dataType + ';base64,' + base64(tableExcel);

                    downloadLink.download = filename;

                    downloadLink.click();
                }
            }
        </script>
    </form>
</asp:Content>

