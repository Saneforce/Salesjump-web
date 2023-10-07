<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/all_rpts.master"  CodeFile="new_Sec_filter.aspx.cs" Inherits="MIS_Reports_new_Sec_filter" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
   <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" />
    <link href="../../css/jquery.multiselect.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        body {
  font-family: "Lato", sans-serif;
}

.sidenav {
  height: 100%;
  width: 0;
  position: fixed;
  z-index: 1;
  top: 0;
  right: 0;
  background-color: aliceblue;
  overflow-x: hidden;
  transition: 0.5s;
  padding-top: 60px;
}

        .sidenav a {
            padding: 8px 8px 8px 32px;
            text-decoration: none;
            font-size: 25px;
            color: #818181;
            display: block;
            transition: 0.3s;
        }

.sidenav a:hover {
  color: #f1f1f1;
}

.sidenav .closebtn {
  float: right;
  right: 25px;
  font-size: 36px;
  margin-top: -454px;
}

#main {
  transition: margin-left .5s;
  padding: 16px;
  float: right;
}

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
            }

                .sidenav a {
                    font-size: 18px;
                }
        }

        #txtfilter_chzn {
            width: 328px !important;
            position: absolute;
        }

        li {
            font-weight: 100;
        }

        .bs-example {
            margin: 20px;
        }

        span.b {
            white-space: nowrap;
            width: 76px;
            overflow: hidden;
            text-overflow: ellipsis;
            display: block;
            position: static;
        }

        .tooltip {
            position: relative;
            display: inline-block;
            color: #006080;
            opacity: 1;
        }

        .tooltiptext {
            visibility: hidden;
            background-color: black;
            color: #fff;
            text-align: center;
            border-radius: 6px;
            padding: 5px 0;
            position: absolute;
            z-index: 100000;
        }

        th:hover .tooltiptext {
            visibility: visible;
        }
    .rw{
    white-space: nowrap;
    width: 110px;
    overflow: hidden;
    text-overflow: ellipsis;
    }
        .btnmd {
            background-color: DodgerBlue;
            border: none;
            color: white;
            padding: 8px 10px;
            font-size: 16px;
            cursor: pointer;
            margin-right: 19px;
        }

.btnmd:hover {
  background-color: RoyalBlue;
}
  
    #sortable1 {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 100%;
        }

        #sortable1 li {
            margin: 0 3px 3px 3px;
            padding: 0.4em;
            padding-left: 1.5em;
            font-size: 0.9em;
            height: 35px;
        }

        #sortable1 li span {
            position: absolute;
            margin-left: -1.3em;
        }
 
    </style>
    <form id="frm1" runat="server" style="padding-left: 248px;">
           <div id="mySidenav" class="sidenav">
   <button type="button" class="btn btn-warning btn-circle btn-lg" id="bcir">+</button>      
   <div id="d1" style="overflow: auto;height:400px; font-size: inherit;overflow-x: hidden;">
    </div>
              <input type="button" id="btnClosePopup" value="Clear" class="btn btn-danger" data-dismiss="modal" onclick="location.reload();" />     
                         <input type="button" id="btnsave" value="Apply" class="btn btn-success" />
          <a href="javascript:void(0)"  class="closebtn" onclick="closeNav()">&times;</a>
   </div>
    <div class="modal fade" id="AddFieldsModal" tabindex="-1" style="z-index: 1000000; background: transparent; overflow: auto;" role="dialog" aria-labelledby="AddFieldsModal" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width: 30%;">
                    <div class="modal-content" style="overflow-y: scroll">
                        <div class="modal-header" style="background-color: #bfefff;">
                            <h4>Field setting</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="margin-bottom: 1rem!important;">
                               <div class="col-xs-12 col-sm-12" style="padding-left: 35px;padding-bottom: 5px;">
                                    <button type="button" class="btn btn-secondary"   id="slct_all">select All</button>
                                    <button type="button" class="btn btn-secondary" id="dslct_all">Deselect All</button>
                                </div>
                                <div class="col-xs-12 col-sm-12">
                                    <ul id="sortable1" style="padding-left: 20px;">                                        
                                    </ul>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal" id="closefields1">Cancel</button>
                                <button type="button" style="background-color: #1a73e8;" class="btn btn-primary" data-dismiss="modal" id="svfields1">Apply</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>          
     <div class="row">
         </div>
        <div class="row">
            <div class="col-lg-12 sub-header"  style="margin-top: 4px";>
                Secondary Order <div style="float: right; padding-top: 3px;">
                  
                     <div class="col-lg-12 sub-header">
                          <a href="#" class="btnmd" id="newsf" onclick="openNav()" title="Filter"><i class="fa fa-bars" style="margin-top: 10px;"></i></a> 
          <span style="float: right; margin-right: 15px;">
             <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
            </span>
         </div>
                </div>
            </div>
        </div>
     <div class="card" style="overflow: auto;">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                   <label style="float: left">
                        Show
                        <select class="data-table-basic_length" id="pglim" aria-controls="data-table-basic">
                            <option value="50">50</option>
                            <option value="100">100</option>
                            <option value="150">150</option>
                            <option value="200">200</option>
                            <option value="250">250</option>
                        </select>
                        entries</label>
                     <span style="float: right; margin-right: 15px;">
                         
                                  <script type="text/javascript">
                                      $(document).ready(function () {
                                          $('#option-droup-demo').multiselect({
                                              enableClickableOptGroups: true
                                          });
                                      });
                    </script>
                    <a href="#" class='dropdown-toggle' id="setif" title="Add Fields"><img src="/MasterFiles/icon/settings.png" style="height: 21px;"></a>
             </span>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 11px;text-align: center;">
                    <thead class="text-warning">
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <div class="row">
                    
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
    <script type="text/javascript" src="http://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        pgNo = 1; PgRecords = 50; var prodcount = []; TotalPg = 0;
        var oq = []; var mq = []; var dq = []; var HQ = []; var templatelist = ''; var selected = []; var fields = []; var tempData = []; var listobj = []; var array = []; var arrayp = [];

        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            $('#txtfilter').val(0);
            $('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");

        });

        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
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
        $(document).ready(function () {
            fillcom();
            countdtl();
            selectfields();
            fillqty();
            fillprod();
           ReloadTable();

            $('[data-toggle="tooltip"]').tooltip();
            $('#pglim').on('change', function () {
                selectfields();
                fillqty();
                fillprod();
                ReloadTable();
            });
            $("#btnClosePopup").click(function () {
                $("#MyPopup").modal("hide");
            });
            $("#btnClosePopup1").click(function () {
                $("#MyPopup1").modal("hide");
            });
            $('#slct_all').on('click', function () {
                $('#sortable1').find('input[type="checkbox"]').prop('checked', true);
            });

            $('#dslct_all').on('click', function () {
                $('#sortable1').find('input[type="checkbox"]').prop('checked', false);
            });
            $('#reportrange').on('apply.daterangepicker', function () {
                PgRecords = $("#pglim").val();
                countdtl();
                selectfields();
                fillqty();
                fillprod();
                ReloadTable();
            });
            function loadPgNos() {
                prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                $(".pagination").html("");
                TotalPg = parseInt(parseInt(prodcount.length / PgRecords) + ((prodcount.length % PgRecords) ? 1 : 0)); selpg = 1;
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
                    pgNo = parseInt($(this).attr("data-dt-idx"));
                    selectfields();
                    fillqty();
                    fillprod();
                    ReloadTable();
                }
                );
            }

            $(".paginate_button > a").on("click", function () {
                selectfields();
                fillqty();
                fillprod();
                ReloadTable();
            }
            );

            function fillcom() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_Sec_filter.aspx/Binddivname",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        prods = JSON.parse(data.d);
                        str = '';
                        for (var i = 0; i < prods.length; i++) {
                            str += "<span style='color: white;line-height: 20px;font-size: 20px;font-weight: bold;'>" + prods[i].Division_Name + "</span>";
                        }
                        $('#cn').append(str);
                    }
                });
            }
            function countdtl() {
                var plim = $("#pglim option:selected").val();
                var plimt = ((pgNo - 1) * plim);
                seloval = $('#txtfilter1').val();
                seldval = $('#txtfilter2').val();
                selorbval = $('#txtfilter3').val();
                selmval = $('#txtfilter4').val();
                selerp = $('#txtfilter5').val();
                selrut = $('#txtfilter6').val();
                selrtl = $('#txtfilter7').val();
                selchnl = $('#txtfilter8').val();
                odat = '';
                $('#mselect1  > option:selected').each(function () {
                    odat += ',' + $(this).val();
                });
                disv = '';
                $('#mselect2  > option:selected').each(function () {
                    disv += ',' + $(this).val();
                });
                ordby = '';
                $('#mselect3  > option:selected').each(function () {
                    ordby += ',' + $(this).val();
                });
                mgr = '';
                $('#mselect4  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                erp = '';
                $('#mselect5  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                rut = '';
                $('#mselect6  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                retail = '';
                $('#mselect7  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                chnl = '';
                $('#mselect8  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                arrayp = array.toString();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_Sec_filter.aspx/totalcnt",
                    data: "{'divcode':'<%=Session["div_code"]%>','fdate':'" + fdt + "','tdate':'" + tdt + "','odatv':'" + odat + "','distv':'" + disv + "','ordbyv':'" + ordby + "','mgrv':'" + mgr + "','erpv':'" + erp + "','rutv':'" + rut + "','retailv':'" + retail + "','chnlv':'" + chnl + "','selovals':'" + seloval + "','selorbvals':'" + selorbval + "','seldvals':'" + seldval + "','selmvals':'" + selmval + "','selerps':'" + selerp + "','selruts':'" + selrut + "','selrtls':'" + selrtl + "','selchnls':'" + selchnl + "','arrayval':'" + arrayp + "'}",

                    dataType: "json",
                    success: function (data) {
                        prodcount = JSON.parse(data.d);

                    }
                });

            }
            function fillqty() {
                var plim = $("#pglim option:selected").val();
                var plimt = ((pgNo - 1) * plim);
                seloval = $('#txtfilter1').val();
                seldval = $('#txtfilter2').val();
                selorbval = $('#txtfilter3').val();
                selmval = $('#txtfilter4').val();
                selerp = $('#txtfilter5').val();
                selrut = $('#txtfilter6').val();
                selrtl = $('#txtfilter7').val();
                selchnl = $('#txtfilter8').val();
                odat = '';
                $('#mselect1  > option:selected').each(function () {
                    odat += ',' + $(this).val();
                });
                disv = '';
                $('#mselect2  > option:selected').each(function () {
                    disv += ',' + $(this).val();
                });
                ordby = '';
                $('#mselect3  > option:selected').each(function () {
                    ordby += ',' + $(this).val();
                });
                mgr = '';
                $('#mselect4  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                erp = '';
                $('#mselect5  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                rut = '';
                $('#mselect6  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                retail = '';
                $('#mselect7  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                chnl = '';
                $('#mselect8  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                arrayp = array.toString();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_Sec_filter.aspx/Bindfillqty",
                    data: "{'divcode':'<%=Session["div_code"]%>','pageindex':'" + plimt + "','plimt':'" + plim + "','fdate':'" + fdt + "','tdate':'" + tdt + "','odatv':'" + odat + "','distv':'" + disv + "','ordbyv':'" + ordby + "','mgrv':'" + mgr + "','erpv':'" + erp + "','rutv':'" + rut + "','retailv':'" + retail + "','chnlv':'" + chnl + "','selovals':'" + seloval + "','selorbvals':'" + selorbval + "','seldvals':'" + seldval + "','selmvals':'" + selmval + "','selerps':'" + selerp + "','selruts':'" + selrut + "','selrtls':'" + selrtl + "','selchnls':'" + selchnl + "','arrayval':'" + arrayp + "'}", 
                    dataType: "json",
                    success: function (data) {
                        proddtlqty = JSON.parse(data.d);

                    }
                });
            }

            function fillprod() {
                var plim = $("#pglim option:selected").val();
                var plimt = ((pgNo - 1) * plim) ;
                seloval = $('#txtfilter1').val();
                seldval = $('#txtfilter2').val();
                selorbval = $('#txtfilter3').val();
                selmval = $('#txtfilter4').val();
                selerp = $('#txtfilter5').val();
                selrut = $('#txtfilter6').val();
                selrtl = $('#txtfilter7').val();
                selchnl = $('#txtfilter8').val();
                odat = '';
                $('#mselect1  > option:selected').each(function () {
                    odat += ',' + $(this).val();
                });
                disv = '';
                $('#mselect2  > option:selected').each(function () {
                    disv += ',' + $(this).val();
                });
                ordby = '';
                $('#mselect3  > option:selected').each(function () {
                    ordby += ',' + $(this).val();
                });
                mgr = '';
                $('#mselect4  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                erp = '';
                $('#mselect5  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                rut = '';
                $('#mselect6  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                retail = '';
                $('#mselect7  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                chnl = '';
                $('#mselect8  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                arrayp = array.toString();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_Sec_filter.aspx/Bindfillqty_view",
                    data: "{'divcode':'<%=Session["div_code"]%>','pageindex':'" + plimt + "','plimt':'" + plim + "','fdate':'" + fdt + "','tdate':'" + tdt + "','odatv':'" + odat + "','distv':'" + disv + "','ordbyv':'" + ordby + "','mgrv':'" + mgr + "','erpv':'" + erp + "','rutv':'" + rut + "','retailv':'" + retail + "','chnlv':'" + chnl + "','selovals':'" + seloval + "','selorbvals':'" + selorbval + "','seldvals':'" + seldval + "','selmvals':'" + selmval + "','selerps':'" + selerp + "','selruts':'" + selrut + "','selrtls':'" + selrtl + "','selchnls':'" + selchnl + "','arrayval':'" + arrayp + "'}",
                    dataType: "json",
                    success: function (data) {
                        proddtl = JSON.parse(data.d);
                        Orders = proddtl;
                    }
                });
            }
            function selectfields() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_Sec_filter.aspx/get_selectfields",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        tempData = JSON.parse(data.d) || [];
                        if (tempData.length > 0) {
                            listobj = JSON.parse(tempData[0].Fields) || [];
                            array = Object.keys(listobj);
                            arrayp = array.toString();
                        }

                    }
                });
            }


            function ReloadTable() {
                $('#OrderList thead').html('');
                $('#OrderList tbody').html('');
                $('#OrderList').show();
                PgRecords = $("#pglim").val();
                st = PgRecords * (pgNo - 1); slno = 0;
                var sth = '';
                var str = '';
                var bsth = '';
                var csf = '';
                let text = "";
                if (tempData.length > 0) {
                    listobj = JSON.parse(tempData[0].Fields) || [];
                    sth += "<tr><th>S.NO</th>";
                    for (var key in listobj) {
                        sth += "<th>" + listobj[key] + "</th>";
                    }
                }
                else {
                    sth += "<tr><th>S.NO</th><th  class='rw'>Order Date</th><th  class='rw'>Distributor Name</th><th  class='rw'>ERP Code</th><th  class='rw'>Order Taken By</th><th  class='rw'>Reporting Manager</th><th  class='rw'>Route</th><th  class='rw'>Retailer Name</th><th  class='rw'>Channel</th>";
                }
                let oprods = [];
                let mymap = new Map();
                let ordarr = [];
                var filtt = [];
                oprods = proddtlqty.filter(function (el) {
                    const val = mymap.get(el.Product_Code);
                    if (val) {
                        return false;
                    }
                    mymap.set(el.Product_Code, el.Product_Detail_Name);
                    return true;
                }).map(function (a) { return a; }).sort();

                for (var j = 0; j < oprods.length; j++) {
                    sth += '<th><span class="tooltip b">' + oprods[j].Product_Detail_Name + '</span><div class="tooltiptext">' + oprods[j].Product_Detail_Name + '</div></th>';
                }
                sth += '<th>Net weight</th><th>Order Value</th>';
                for (i = 0; i <= st + PgRecords; i++) {
                    if (i < proddtl.length) {
                        slno = st + i + 1;
                        if (tempData.length > 0) {
                            str += "<tr style='text-align: left;'><td>" + slno + "</td>";
                            for (var key in listobj) {
                                if (proddtl[i].hasOwnProperty(key)) {
                                    str += "<td>" + proddtl[i][key] + "</td>";
                                }
                            }
                            let todayarr = proddtlqty;
                            for (var x = 0; x < array.length; x++) {
                                todayarr = todayarr.filter(function (a) { return (a[array[x]] == proddtl[i][array[x]] ) });
                            }
                           for (var $j = 0; $j < oprods.length; $j++) {
                                let ordarr = todayarr.filter(function (a) { return (a.Product_Code == oprods[$j].Product_Code) });
                                str += "<td>" + (ordarr.length > 0 ? ordarr[0].Quantity : '') + "</td>";

                            }
                        }
                        else {
                            str += "<tr style='text-align: left;'><td>" + slno + "</td><td>" + proddtl[i].Order_Date + "</td><td>" + proddtl[i].Stockist_name + "</td><td>" + proddtl[i].ERP_Code + "</td><td>" + proddtl[i].SF_Name + "</td><td>" + proddtl[i].RSF + "</td><td>" + proddtl[i].routename + "</td><td>" + proddtl[i].retailername + "</td><td>" + proddtl[i].channel + "</td>";
                            let todayarr = proddtlqty.filter(function (a) { return (a.Trans_Sl_No == proddtl[i].Trans_Sl_No) });
                            for (var $j = 0; $j < oprods.length; $j++) {
                                let ordarr = todayarr.filter(function (a) { return (a.Product_Code == oprods[$j].Product_Code) });
                                str += "<td>" + (ordarr.length > 0 ? ordarr[0].Quantity : '') + "</td>";

                            }
                        }

                        str += "<td>" + proddtl[i].net_weight_value + "</td><td>" + proddtl[i].Order_value + "</td>";
                    }
                }

                sth += "</tr>";
                str += "</tr>";

                $('#OrderList thead').append(sth);
                $('#OrderList tbody').append(str);
                loadPgNos();
            }

            $('#setif').on('click', function () {
                $('#sortable1').html('');
                var fld = '';
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='Order_Date' value='Order_Date' name='Order Date'/> " +
                    "<label class= 'tgl' for='OrderDate' style='height: 1px; '>Order Date</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='Stockist_name' value='Stockist_name' name='Distributor Name'/> " +
                    "<label class= 'tgl' for='Stockist_name' style='height: 1px; '>Distributor Name</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='SF_Name'  value='SF_Name' name='Order Taken By'/> " +
                    "<label class= 'tgl' for='Order Taken By' style='height: 1px; '>Order Taken By</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='RSF' value='RSF' name='Reporting Manager'/> " +
                    "<label class= 'tgl' for='Reporting Manager' style='height: 1px; '>Reporting Manager</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='ERP_Code' value='ERP_Code' name='Distributor ERP Code'/> " +
                    "<label class= 'tgl' for='ERP_Code' style='height: 1px; '>Distributor ERP Code</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='routename' value='routename' name='Route'/> " +
                    "<label class= 'tgl' for='Territory_Name' style='height: 1px; '>Route</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='retailername' value='retailername' name='Retailer Name'/> " +
                    "<label class= 'tgl' for='ListedDr_Name' style='height: 1px; '>Retailer Name</label></li>";
                fld += "<li class='ui-state-default'><input class='tgl tgl-skewed' type='checkbox' id='channel' value='channel' name='Channel'/> " +
                    "<label class= 'tgl' for='Doc_Spec_ShortName' style='height: 1px; '>Channel</label></li>";
                $("#sortable1").append(fld);
                $('#AddFieldsModal').modal('toggle');
                for (var a = 0; a < array.length; a++) {
                    document.getElementById(array[a]).checked = true;
                }
            });

            function clearFields() {
                $('#sortable1').find('input[type="checkbox"]').prop('checked', false);
            }

            $('#closefields1').on('click', function () {
                clearFields();
            });
            $(document).on('click', '.btnmd', function () {
                $("#MyPopup").modal("show");
            });
            var counter = 1; var iCnt = 0;
            $("#bcir").click(function () {
                if (iCnt <= 7) {
                    iCnt = iCnt + 1;
                    var str = '';
                    var dd = `${iCnt}`;
                    str += `<div class=row>`;
                    str += `<div class='col-xs-6' id="${counter}" style='width: 224px; height: 52px; padding-top: 4px; margin-left: 22px; padding: 0px;'>`;
                    str += `<label style='white-space: nowrap; margin-left: 57px;'></label><select id="txtfilter${iCnt}" name='ddfilter' style='width: 207px; margin-top: 11px;'> +`
                        + `<option value='0'>Select Fields</option><option value='Order_Date'>Order Date</option><option value='stockist_code'>Distributor Name</option> +`
                        + `<option value='mr'>Order Taken By</option><option value='rsfc'>Reporting Manager</option><option value='ERP_Code'>ERP Code</option>+`
                        + `<option value='Territory_code'>Route</option><option value='ListedDrCode'>Retailer Name</option><option value='Doc_Special_Code'>Channel</option></select>`;
                    str += `</div></br></br>`;
                    $('#d1').append(str);

                    var str2 = '';

                    str2 += `<div class='col-xs-6' style='line-height: normal; width: 139%;margin-left:225px;margin-top: -60px;'>`;
                    str2 += `<div class='col-sm-5' style='margin-top: 22px;width: 39.666667%;'>`;
                    str2 += `<select id='mselect${iCnt}' multiple></select>`;
                    str2 += `</div>`;
                    str2 += `</div></div>`;
                    $('#d1').append(str2);

                    $('#txtfilter1').on('change', function () {
                        let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        var sf = $("#mselect1");
                        sf.empty();
                        if ($('#txtfilter1').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter1').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter1').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter1').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter1').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter1').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter1').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter1').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect1').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');

                    });


                    $('#txtfilter2').on('change', function () {
                        var sf = $("#mselect2"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter2').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter2').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter2').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter2').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter2').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter2').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter2').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter2').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect2').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });

                    $('#txtfilter3').on('change', function () {
                        var sf = $("#mselect3"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter3').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter3').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter3').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter3').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter3').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter3').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter3').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter3').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect3').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });

                    $('#txtfilter4').on('change', function () {
                        var sf = $("#mselect4"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter4').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter4').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter4').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter4').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter4').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter4').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter4').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter4').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect4').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });
                    $('#txtfilter5').on('change', function () {
                        var sf = $("#mselect5"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter5').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter5').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter5').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter5').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter5').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter5').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter5').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter5').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect5').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });
                    $('#txtfilter6').on('change', function () {
                        var sf = $("#mselect6"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter6').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter6').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter6').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter6').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter6').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter6').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter6').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter6').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect6').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });

                    $('#txtfilter7').on('change', function () {
                        var sf = $("#mselect7"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter7').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter7').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter7').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter7').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter7').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter7').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter7').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter7').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect7').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });
                    $('#txtfilter8').on('change', function () {
                        var sf = $("#mselect8"); let mymap = new Map(); let arrayod = []; let arrayst = []; let arrayob = []; let arrayrpt = []; let arrayerp = []; let arrayrt = []; let arrayretail = []; let arraychnl = [];
                        sf.empty();
                        if ($('#txtfilter8').val() == 'Order_Date') {
                            orderdateval();
                            arrayod = HQ.filter(function (el) {
                                const val = mymap.get(el.Order_Date);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Order_Date, el.Order_Date);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var j = 0; j < arrayod.length; j++) {
                                sf.append($('<option value="' + arrayod[j].Order_Date + '">' + arrayod[j].Order_Date + '</option>'));
                            }

                        }
                        if ($('#txtfilter8').val() == 'stockist_code') {
                            orderdateval();
                            arrayst = HQ.filter(function (el) {
                                const val = mymap.get(el.stockist_code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.stockist_code, el.Stockist_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayst.length; i++) {
                                sf.append($('<option value="' + arrayst[i].stockist_code + '">' + arrayst[i].Stockist_Name + '</option>'));
                            }

                        }
                        if ($('#txtfilter8').val() == 'mr') {
                            orderdateval();
                            arrayob = HQ.filter(function (el) {
                                const val = mymap.get(el.mr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mr, el.mrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayob.length; i++) {
                                sf.append($('<option value="' + arrayob[i].mr + '">' + arrayob[i].mrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter8').val() == 'rsfc') {
                            orderdateval();
                            arrayrpt = HQ.filter(function (el) {
                                const val = mymap.get(el.mgr);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.mgr, el.mgrn);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrpt.length; i++) {
                                sf.append($('<option value="' + arrayrpt[i].mgr + '">' + arrayrpt[i].mgrn + '</option>'));
                            }

                        }
                        if ($('#txtfilter8').val() == 'ERP_Code') {
                            orderdateval();
                            arrayerp = HQ.filter(function (el) {
                                const val = mymap.get(el.ERP_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ERP_Code, el.ERP_Code);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayerp.length; i++) {
                                sf.append($('<option value="' + arrayerp[i].ERP_Code + '">' + arrayerp[i].ERP_Code + '</option>'));
                            }
                        }
                        if ($('#txtfilter8').val() == 'Territory_code') {
                            orderdateval();
                            arrayrt = HQ.filter(function (el) {
                                const val = mymap.get(el.Territory_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Territory_Code, el.Territory_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayrt.length; i++) {
                                sf.append($('<option value="' + arrayrt[i].Territory_Code + '">' + arrayrt[i].Territory_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter8').val() == 'ListedDrCode') {
                            orderdateval();
                            arrayretail = HQ.filter(function (el) {
                                const val = mymap.get(el.ListedDrCode);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.ListedDrCode, el.ListedDr_Name);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arrayretail.length; i++) {
                                sf.append($('<option value="' + arrayretail[i].ListedDrCode + '">' + arrayretail[i].ListedDr_Name + '</option>'));
                            }
                        }
                        if ($('#txtfilter8').val() == 'Doc_Special_Code') {
                            orderdateval();
                            arraychnl = HQ.filter(function (el) {
                                const val = mymap.get(el.Doc_Special_Code);
                                if (val) {
                                    return false;
                                }
                                mymap.set(el.Doc_Special_Code, el.Doc_Spec_ShortName);
                                return true;
                            }).map(function (a) { return a; }).sort();
                            for (var i = 0; i < arraychnl.length; i++) {
                                sf.append($('<option value="' + arraychnl[i].Doc_Special_Code + '">' + arraychnl[i].Doc_Spec_ShortName + '</option>'));
                            }
                        }
                        $('#mselect8').multiselect({
                            columns: 1,
                            placeholder: 'Select Value',
                            search: true,
                            searchOptions: {
                                'default': 'Search Value'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');
                    });
                }
            });

            function orderdateval() {
                seloval = $('#txtfilter1').val();
                seldval = $('#txtfilter2').val();
                selorbval = $('#txtfilter3').val();
                selmval = $('#txtfilter4').val();
                selerp = $('#txtfilter5').val();
                selrut = $('#txtfilter6').val();
                selrtl = $('#txtfilter7').val();
                selchnl = $('#txtfilter8').val();
                odat = '';
                $('#mselect1  > option:selected').each(function () {
                    odat += ',' + $(this).val();
                });
                disv = '';
                $('#mselect2  > option:selected').each(function () {
                    disv += ',' + $(this).val();
                });
                ordby = '';
                $('#mselect3  > option:selected').each(function () {
                    ordby += ',' + $(this).val();
                });
                mgr = '';
                $('#mselect4  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                erp = '';
                $('#mselect5  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                rut = '';
                $('#mselect6  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                retail = '';
                $('#mselect7  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                chnl = '';
                $('#mselect8  > option:selected').each(function () {
                    mgr += ',' + $(this).val();
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_Sec_filter.aspx/Getorderdateval",
                    data: "{'divcode':'<%=Session["div_code"]%>','fdate':'" + fdt + "','tdate':'" + tdt + "','odatv':'" + odat + "','distv':'" + disv + "','ordbyv':'" + ordby + "','mgrv':'" + mgr + "','erpv':'" + erp + "','rutv':'" + rut + "','retailv':'" + retail + "','chnlv':'" + chnl + "','selovals':'" + seloval + "','selorbvals':'" + selorbval + "','seldvals':'" + seldval + "','selmvals':'" + selmval + "','selerps':'" + selerp + "','selruts':'" + selrut + "','selrtls':'" + selrtl + "','selchnls':'" + selchnl + "'}",
                    dataType: "json",
                    success: function (data) {
                        HQ = JSON.parse(data.d) || [];
                    }
                });
            }


            $(document).on("click", "a.delete", function () {
                $(this).parent().remove();
            });
            $(document).on("click", "a.deletes", function () {
                $(this).parent().remove();
            });
            $('#btnsave').on('click', function () {
                closeNav();
                countdtl();
                fillqty();
                fillprod();
                ReloadTable();
            });

            $('#svfields1').on('click', function () {
                templatelist = '';
                $('.tgl').each(function () {
                    if ($(this).prop("checked") == true)
                        templatelist += '"' + $(this).val() + '":"' + $(this).next('label').text() + '",';
                });
                templatelist = '{' + templatelist.substring(0, templatelist.length - 1) + '}';
                saveTemplateList(templatelist);
            });
            function saveTemplateList(templatelist) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_sec_filter.aspx/savetemplate",
                    data: "{'divcode':'<%=Session["div_code"]%>','tplist':'" + templatelist + "'}",
                    dataType: "json",
                    success: function (data) {
                       selectfields();
                        fillqty();
                        fillprod();
                        ReloadTable();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                clearFields();

            }
        });
        function openNav() {
            document.getElementById("mySidenav").style.width = "500px";

        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";

        }

</script>
</asp:Content>
