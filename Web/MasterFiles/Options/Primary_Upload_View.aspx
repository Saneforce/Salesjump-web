<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Primary_Upload_View.aspx.cs" Inherits="MasterFiles_Options_Primary_Upload_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="frm1" runat="server">
        <div class="row">
            <div class="row hidden">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            </div>
            <div class="col-lg-12 sub-header">
                Primary Sales Upload View
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <asp:Label ID="Label1" CssClass="col-md-2  col-md-offset-3  control-label" Text="Month" runat="server">
            </asp:Label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlmnth" CssClass="form-control"  runat="server">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <asp:Label ID="Label2" CssClass="col-md-2  col-md-offset-3  control-label" Text="Year" runat="server"></asp:Label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlyr" CssClass="form-control"  runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button id="btnview" type="button" class="btn btn-primary">View</button>
            </div>
        </div>
        
        <div class="card hidden">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <label style="float: right">Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th style="text-align: left">ERP_Code</th>
                            <th style="text-align: left">Distributor Name</th>
                            <th style="text-align: right">Sale Value</th>
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
                            <ul class="pagination" style="float: right; margin: -11px 0px">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
        <script language="javascript" type="text/javascript">
            var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Vendor_Code,Distributor_Name,Primary_Sale_Value,ERP_Code,"
            var AllOrders = [];
            $(".data-table-basic_length").on("change", function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();

            });

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
                ReloadTable();
            });

            function loadPgNos() {
                prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                $(".pagination").html("");
                TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
                if (isNaN(prepg)) prepg = 0;
                if (isNaN(Nxtpg)) Nxtpg = 2;
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
                });
            }


            function ReloadTable() {
                $("#OrderList TBODY").html("");
                st = PgRecords * (pgNo - 1);
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].ERP_Code + "</td><td>" + Orders[$i].Distributor_Name + "</td><td style='text-align: right;'>" + Orders[$i].Primary_Sale_Value + "</td>"); //<td>" + Orders[$i].Rmks+"</td>
                        $("#OrderList TBODY").append(tr);
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
            }


            function loadData(mn, yr) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_Upload_View.aspx/GetDetails",
                    data: "{'Div':'<%=Session["Division_Code"]%>','Mn':'" + mn + "','Yr':'" + yr + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }

            $('#btnview').on('click', function () {
                var mn = $('#<%=ddlmnth.ClientID%>').val();
                var yr = $('#<%=ddlyr.ClientID%>').val();

                if (mn == 0) {
                    alert('Select Month');
                    return false;
                }
                                
                var str = 'rptPrimaryUploadView.aspx?&FMonth=' + mn + '&FYear=' + yr;
                window.open(str, "PrimaryUploadView", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');

                //loadData(mn, yr);
            });
        </script>
    </form>    
</asp:Content>

