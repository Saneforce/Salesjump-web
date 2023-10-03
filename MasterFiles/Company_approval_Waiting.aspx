<%@ Page Title="" Language="C#" MasterPageFile="~/Billing.master" AutoEventWireup="true" CodeFile="Company_approval_Waiting.aspx.cs" Inherits="MasterFiles_Company_approval_Waiting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }
    </style>
    <form id="form1" runat="server">

        <div class="row">
            <div class="col-lg-12 sub-header">
                Company Request List
            </div>
        </div>


        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <label style="float: right">
                        Show
                       
                            <select class="data-table-basic_length" aria-controls="data-table-basic">
                                <option value="10">10</option>
                                <option value="25">25</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select>
                        entries</label>
                </div>
                <div>
                    <table class="table table-hover" id="OrderList" style="font-size: 12px; align-items: center; border: 1px solid;width:100%">
                        <thead class="text-warning">
                            <tr style="white-space: nowrap;">
                                <th style="text-align: center; background-color: #7ac2ff;">Comp Id</th>
                                <th style="background-color: #7ac2ff; text-align: center">Company Code</th>
                                <th style="background-color: #7ac2ff; text-align: center">Company Name</th>
                                <th style="background-color: #7ac2ff; text-align: center">Country</th>
                                <th style="background-color: #7ac2ff; text-align: center">Edit</th>

                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="row">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript">
        var menurights = '', rsfc = '';
        var AllCOM = [];
        var filtrkey = '0';
        var compnme = []; pgNo = 1; PgRecords = 100; TotalPg = 0; searchKeys = "Comp_Name,Comp_Code,CCountry,";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();

            }
        );
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(compnme.length / PgRecords) + ((compnme.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg)) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                if ('<%=Session["div_code"]%>' == '4' ||'<%=Session["div_code"].ToString()%>' == '106' ||'<%=Session["div_code"].ToString()%>' == '107' ||'<%=Session["div_code"].ToString()%>' == '109') {
                    $('.hrts').show();
                    $('#OrderList>tbody>tr>.rts').show();
                }
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );
        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                compnme = AllCOM.filter(function (a) {
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
                compnme = AllCOM
            ReloadTable();

        })

        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Company_approval_Waiting.aspx/GetList",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllCOM = JSON.parse(data.d) || [];
                    compnme = AllCOM; ReloadTable();

                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");

            st = PgRecords * (pgNo - 1); slno = 0;
            var Company = compnme.filter(function (x) {
                return x.Comp_id != '0';
            });
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Company.length) {
                    tr = $("<tr></tr>");

                    $(tr).html('<td  align="left">' + Company[$i].Comp_id + '</td><td  align="left">' + Company[$i].Comp_Code + '</td><td  align="left">' + Company[$i].Comp_Name +
                        '</td><td  align="left">' + Company[$i].CCountry + '</td><td id=' + Company[$i].Comp_id + ' class="cview"  align="center"><a href="#">VIEW</a></td>');

                    $("#OrderList TBODY").append(tr);

                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Company.length) ? (st + PgRecords) : Company.length) + " of " + Company.length + " entries")
            loadPgNos();
        }

        $(document).ready(function () {

            loadData();

            $(document).on('click', '.cview', function () {
                x = this.id;
                window.location.href = "Company_Approval_form.aspx?Comp_id=" + x;
            });
        });

    </script>

</asp:Content>

