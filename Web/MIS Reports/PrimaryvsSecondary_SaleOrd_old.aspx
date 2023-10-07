<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="PrimaryvsSecondary_SaleOrd.aspx.cs" Inherits="MIS_Reports_PrimaryvsSecondary_SaleOrd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        .dropdown-menu {
            max-width: 380px;
        }
    </style>
    <div class="col-lg-10">
        <h4 style="font-family: system-ui;">Primary Vs Secondary Report</h4>
    </div>
    <div class="col-lg-2">
        <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
            <i class="fa fa-calendar"></i>&nbsp;
               
                        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
        </div>
    </div>
    <div class="card">
        <div class="col-lg-12" style="margin: 5px; text-align: end">
            <label><i class="fa fa-filter" style="padding-right: 6px;"></i>Sort By :</label>
            <select id="srt">
                <option selected value="0">Nothing Select</option>
                <%-- <option value="ddlst">State</option>--%>
                <option value="ddlprd_nm">Product Name</option>
                <option value="ddlprd_cat">Product Category</option>
                <option value="ddlprd_grp">Product Group</option>
            </select>
            <button class="btn btn-circle btn-info" style="margin: 5px;" id="btnplus">+</button>
        </div>
        <div class="col-lg-12" style="margin: 5px;">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <label>Division</label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlsubdiv" data-live-search="true"></select>
                </div>
            </div>
            <div class="col-lg-6 ddlst">
                <div class="col-lg-3">
                    <label>State</label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlst" data-live-search="true"></select>
                </div>
            </div>

        </div>
        <div class="col-lg-12" style="margin: 5px;">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <label>Manager</label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlmgr" data-live-search="true"></select>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <label>Sales Officer</label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlso" data-live-search="true"></select>
                </div>
            </div>

        </div>
        <div class="col-lg-12" style="margin: 5px;">
            <div class="col-lg-6 ddlprd_cat" style="display: none;">
                <div class="col-lg-3">
                    <label>Product Category </label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlprd_cat" multiple data-live-search="true"></select>
                </div>
            </div>
            <div class="col-lg-6 ddlprd_nm" style="display: none;">
                <div class="col-lg-3">
                    <label>Product Name</label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlprd_nm" multiple data-live-search="true"></select>
                </div>
            </div>

        </div>

        <div class="col-lg-12" style="margin: 5px;">
            <div class="col-lg-6 ddlprd_grp" style="display: none;">
                <div class="col-lg-3">
                    <label>Product Group </label>
                </div>
                <div class="col-lg-3">
                    <select id="ddlprd_grp" multiple data-live-search="true"></select>
                </div>
            </div>
            <div class="col-lg-6">
            </div>
        </div>
        <div class="col-lg-12" style="margin: 5px; display: none;">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <label>To Date</label>
                </div>
                <div class="col-lg-3">
                    <input type="date" id="frmdt" style="width: 220px; height: 25px;" />
                </div>
            </div>
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <label>To Date</label>
                </div>
                <div class="col-lg-3">
                    <input type="date" id="todt" style="width: 220px; height: 25px;" />
                </div>
            </div>
        </div>
        <div class="col-lg-12" style="margin: 5px; text-align: end;">
            <input type="button" id="savBtn" class="btn btn-primary" value="View" />
        </div>
        <div class="col-lg-12" style="margin: 5px;">
        </div>
    </div>

    <script type="text/javascript">
        var SubDiv = [], Sts = [], Prd = [], SFDets = [], MGR_lst = [], Prdcat = [], PrdGrp = [];
        var str = '';
        $("document").ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDT = id[2].trim() + '-' + id[1] + '-' + id[0];
                TDT = id[5] + '-' + id[4] + '-' + id[3].trim();
                $('#loadover').show();
                frmdt = FDT;
                todt = TDT;
                //setTimeout(function () {
                //    setTimeout(loadData(), 500);
                //}, 500);
            });

            $("#srt").selectpicker({
                liveSearch: true
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/Subdivision_list",
                dataType: "json",
                success: function (data) {
                    SubDiv = JSON.parse(data.d) || [];
                    if (SubDiv.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < SubDiv.length; i++) {
                            str += "<option value=" + SubDiv[i].subdivision_code + ">" + SubDiv[i].subdivision_name + "</option>";
                        }
                        $("#ddlsubdiv").append(str);
                        $("#ddlsubdiv").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/state",
                dataType: "json",
                success: function (data) {
                    Sts = JSON.parse(data.d) || [];
                    if (Sts.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < Sts.length; i++) {
                            str += "<option value=" + Sts[i].State_Code + ">" + Sts[i].StateName + "</option>";
                        }
                        $("#ddlst").append(str);
                        $("#ddlst").selectpicker({
                            liveSearch: true
                        });
                    }

                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/MGR",
                dataType: "json",
                success: function (data) {
                    MGR_lst = JSON.parse(data.d) || [];
                    if (MGR_lst.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < MGR_lst.length; i++) {
                            str += "<option value=" + MGR_lst[i].Sf_Code + ">" + MGR_lst[i].Sf_Name + "</option>";
                        }
                        $("#ddlmgr").append(str);
                        $("#ddlmgr").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/SalesForceList",
                dataType: "json",
                success: function (data) {
                    SFDets = JSON.parse(data.d) || [];
                    if (SFDets.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < SFDets.length; i++) {
                            str += "<option value=" + SFDets[i].Sf_Code + ">" + SFDets[i].Sf_Name + "</option>";
                        }
                        $("#ddlso").append(str);
                        $("#ddlso").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/Product",
                dataType: "json",
                success: function (data) {
                    Prd = JSON.parse(data.d) || [];
                    if (Prd.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < Prd.length; i++) {
                            str += "<option value=" + Prd[i].Product_Detail_Code + ">" + Prd[i].Product_Detail_Name + "</option>";
                        }
                        $("#ddlprd_nm").append(str);
                        $("#ddlprd_nm").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/ProductGrp",
                dataType: "json",
                success: function (data) {
                    PrdGrp = JSON.parse(data.d) || [];
                    if (PrdGrp.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < PrdGrp.length; i++) {
                            str += "<option value=" + PrdGrp[i].Product_Grp_Code + ">" + PrdGrp[i].Product_Grp_Name + "</option>";
                        }
                        $("#ddlprd_grp").append(str);
                        $("#ddlprd_grp").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "PrimaryvsSecondary_SaleOrd.aspx/ProductCat",
                dataType: "json",
                success: function (data) {
                    Prdcat = JSON.parse(data.d) || [];
                    if (Prdcat.length > 0) {
                        str = '';
                        for (var i = 0; i < Prdcat.length; i++) {
                            str += "<option value=" + Prdcat[i].Product_Cat_Code + ">" + Prdcat[i].Product_Cat_Name + "</option>";
                        }
                        $("#ddlprd_cat").append(str);
                        $("#ddlprd_cat").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
        });
        $("#savBtn").on('click', function () {
            var Subdiv = $("#ddlsubdiv").val();
            //var state = $("#ddlst option:selected").text()
            var state = $("#ddlst").val();
            var MGR = $("#ddlmgr").val();
            var SF = $("#ddlso").val();
            var Prd_Nm = $("#ddlprd_nm").val();
            var Prd_Grp = $("#ddlprd_grp").val();
            var Prd_Cat = $("#ddlprd_cat").val();
            var frmdt = $("#frmdt").val(); var todt = $("#todt").val();
            var URL = "rpt_pri_VS_sec_saleorder.aspx?sfcode=" + SF + "&RSf=" + MGR + "&state=" + state + "&Subdiv=" + Subdiv + "&PrdNm=" + Prd_Nm + "&PrdGrp=" + Prd_Grp + "&PrdCat=" + Prd_Cat + "&Frm_Dt=" + FDT + "&To_Dt=" + TDT + "";
            window.open(URL, "ModalPopUp",
                "toolbar=no," +
                "scrollbars=yes," +
                "location=no," +
                "statusbar=no," +
                "menubar=no," +
                "addressbar=no," +
                "resizable=yes," +
                "width=900," +
                "height=600," +
                "left = 0," +
                "top=0"
            );
            //URL.focus();//, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
        });
        $("#ddlst").on('change', function () {
            var state = $("#ddlst").val();
            Mgrfilter = MGR_lst.filter(function (a) {
                return a.State_Code == state;
            });
            SFFilter = SFDets.filter(function (a) {
                return a.State_Code == state;
            });
            if (Mgrfilter.length > 0) {
                $("#ddlmgr").empty();
                str = "";
                str = "<option value=''>Nothing Select</option>";
                for (var i = 0; i < Mgrfilter.length; i++) {
                    str += "<option value=" + Mgrfilter[i].Sf_Code + ">" + Mgrfilter[i].SFNA + "</option>";
                }
                $("#ddlmgr").append(str);
                $("#ddlmgr").selectpicker("refresh");
            }
            if (SFFilter.length > 0) {
                $("#ddlso").empty();
                str = "";
                str = "<option value=''>Nothing Select</option>";
                for (var i = 0; i < SFFilter.length; i++) {
                    if (SFFilter[i].Sf_Name != 'admin')
                        str += "<option value=" + SFFilter[i].Sf_Code + ">" + SFFilter[i].Sf_Name + "</option>";
                }
                $("#ddlso").append(str);
                $("#ddlso").selectpicker("refresh");
            }

        })
        $("#btnplus").on('click', function () {
            var srtval = $("#srt").val();
            if (srtval != '0')
                $("." + srtval).show();
            else
                alert('Choose Any Value!!!');
        });
        $("#ddlmgr").on('change', function () {
            var mgr = $("#ddlmgr").val();
            SFFilter = SFDets.filter(function (a) {
                return a.Reporting_To_SF == mgr;
            })
            if (SFFilter.length > 0) {
                $("#ddlso").empty();
                str = "";
                str = "<option value=''>Nothing Select</option>";
                for (var i = 0; i < SFFilter.length; i++) {
                    if (SFFilter[i].Sf_Name != 'admin')
                        str += "<option value=" + SFFilter[i].Sf_Code + ">" + SFFilter[i].Sf_Name + "</option>";
                }
                $("#ddlso").append(str);
                $("#ddlso").selectpicker("refresh");
            }
        })
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                //$('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);
        });
    </script>

</asp:Content>

