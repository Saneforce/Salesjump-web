<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Gift_Claim_Master.aspx.cs" Inherits="MasterFiles_Gift_Claim_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="../css/jquery.multiselect.css" rel="stylesheet" />
	<style>
        .segment2 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
            /* border: 1px solid  #66a454   #d3252a  #f59303;*/
        }

            .segment2 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment2 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                }

                .segment2 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment2 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }
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
    </style>
    <form id="frm" runat="server">
        <div class="container col-lg-12">
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" id="gslab" href="#home">Gift Slab</a></li>
                <li><a data-toggle="tab" class="rslab" href="#menu1">Retailer Business Slab</a></li>
                <li><a data-toggle="tab" class="gpslab" href="#menu2">Gift Products</a></li>
            </ul>

            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                    <button class="btn btn-primary" type="button" data-toggle="modal" data-target="#MyPopup" id="giftbtn" style="float: right;">Create</button>
                    <div class="card">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap">
                                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" autocomplete="off" style="width: 250px;" />
                                <label style="float: right">
                                    Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                                    entries</label>
                                <div style="float: right; padding-top: 4px;">
                                    <ul class="segment1">
                                        <li data-va='All'>ALL</li>
                                        <li data-va='0' class="active">Active</li>
                                    </ul>
                                </div>
                            </div>
                            <table class="table table-hover" id="OrderList">
                                <thead class="text-warning">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th>Min Value</th>
                                        <th>Max Value</th>
                                        <th>Mapped Retailer Business Slab</th>
                                        <th>Edit</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                        <ul class="pagination">
                                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="MyPopup" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Gift Slab</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hbcode" />
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <label>Name</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="text" id="txtbname" class="form-control" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Description</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="text" id="txtbdesc" class="form-control" autocomplete="off" maxlength="200" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Min Value</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="number" id="bminvalue" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Max Value</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="number" id="bmaxvalue" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Retailer Business Slab</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <select class="form-control" id="rbsddl"></select>
                                        </div>
                                    </div>
									<div class="row" style="margin-top: 10px;">
                                  <div class="col-sm-2"><label>HQ</label></div>
                                  <div class="col-sm-10">
                                   <select  id="mselect" multiple>
                                    </select>
                                    </div>
                                  </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Duration of Slab</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <label>From</label>&nbsp<input type="date" id="fdt" class="form-control" />
                                            &nbsp
                           <label>To</label>&nbsp<input type="date" id="tdt" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label style="white-space: nowrap;">Type</label>
                                        </div>
                                        <div class="col-sm-10" style="display: flex;">
                                            <div class="col-xs-6" style="margin-left: -15px;">
                                                <select class="msl form-control" id="ddlsl">
                                                    <option value="2">--Select--</option>
                                                    <option value="0">Product Billed Slab</option>
                                                    <option value="1">Slab Gift</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnsave" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <button class="btn btn-primary" id="rbsbtn" type="button" data-toggle="modal" data-target="#RetailPopup" style="float: right;">Create</button>
                    <div class="card">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap">
                                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd2" autocomplete="off" style="width: 250px;" />
                                <label style="float: right">
                                    Show
                                        <select class="data-table-basic_length2" aria-controls="data-table-basic">
                                            <option value="10">10</option>
                                            <option value="25">25</option>
                                            <option value="50">50</option>
                                            <option value="100">100</option>
                                        </select>
                                    entries</label>
                                <div style="float: right">
                                    <ul class="segment">
                                        <li data-va='All'>ALL</li>
                                        <li data-va='0' class="active">Active</li>
                                    </ul>
                                </div>
                            </div>
                            <table class="table table-hover" id="retailbslab">
                                <thead class="text-warning">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th>Min Value</th>
                                        <th>Max Value</th>
                                        <th>Gift Slab Mapped Count</th>
                                        <th>Edit</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="dataTables_info" id="retailbslab_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="dataTables_paginate paging_simple_numbers" id="example_paginate">
                                        <ul class="pagination">
                                            <li class="paginate_button previous disabled" id="example_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                            <li class="paginate_button next" id="example_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="RetailPopup" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Retail Business Slab</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hrcode" />
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <label>Name</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="text" id="txtrname" class="form-control" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Description</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="text" id="txtrdesc" class="form-control" autocomplete="off" maxlength="200" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Min Value</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="number" id="rminvalue" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Max Value</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="number" id="rmaxvalue" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Status</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <select id="ddlrstatus" class="form-control">
                                                <option value="2">--Select--</option>
                                                <option value="0">Active</option>
                                                <option value="1">Inactive</option>
                                            </select>
                                        </div>
                                    </div>
									<div class="row" style="margin-top: 10px;">
                                  <div class="col-sm-2"><label>HQ</label></div>
                                  <div class="col-sm-10">
                                   <select  id="mselect2" multiple>
                                    </select>
                                    </div>
                                  </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Duration of Slab</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <label>From</label>&nbsp<input type="date" id="rfdt" class="form-control" />
                                            &nbsp
                                            <label>To</label>&nbsp<input type="date" id="rtdt" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Claim End Date</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input type="date" id="dedt" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnrsave" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="menu2" class="tab-pane fade">
                    <button class="btn btn-primary" type="button" data-toggle="modal" data-target="#MyGPPopup" id="giftpbtn" style="float: right;">Create</button>
                    <div class="card">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap">
                                Search&nbsp;&nbsp;<input type="text" id="t2SearchOrd" autocomplete="off" style="width: 250px;" />
                                <label style="float: right">
                                    Show
                    <select class="data-table-basic_length3" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                                    entries</label>
                                <div style="float: right; padding-top: 4px;">
                                    <ul class="segment2">
                                        <li data-va='All'>ALL</li>
                                        <li data-va='0' class="active">Active</li>
                                    </ul>
                                </div>
                            </div>
                            <table class="table table-hover" id="giftpList">
                                <thead class="text-warning">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>Edit</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="dataTables_info" id="gporders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                                </div>
                                <div class="col-sm-7">
                                    <div class="dataTables_paginate paging_simple_numbers">
                                        <ul class="pagination">
                                            <li class="paginate_button previous disabled"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                            <li class="paginate_button next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="MyGPPopup" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Gift Products</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hbgpcode" />
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <label>Gift Name</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <input list="products" name="gpproducts" class="form-control autoc ui-autocomplete-input" id="txttype" style="width: 189px;" />
                                            <datalist id="products">
                                            </datalist>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Status</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <select id="ddlgpstatus" class="form-control">
                                                <option value="2">--Select--</option>
                                                <option value="0">Active</option>
                                                <option value="1">Inactive</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-2">
                                            <label>Duration</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <label>From</label>&nbsp<input type="date" id="gpfdt" class="form-control" />
                                            &nbsp
                                            <label>To</label>&nbsp<input type="date" id="gptdt" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btngpsave" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
          var divcode;
        var GPAllOrders = [];
          var AllOrders = [];
          var AllOrders2 = [];
          var filtrkey = '0';
          var filtrkey2 = '0';
          var filtrkey3 = '0';
          var GPOrders = [];
          var Orders2 = [];
          optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Deactivate</a></li>"
          optStatus3 = "<li><a href='#' v2='0'>Active</a><a href='#' v2='1'>Deactivate</a></li>"
          optStatus2 = "<li><a href='#' v1='0'>Active</a><a href='#' v1='1'>Deactivate</a></li>"
          var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Giftcnt,ID,RBDesc,RBMax,RBMin,RBName,Status"; searchKeys3 = "ID,Product_Code,PName,Status"; searchKeys2 = "ID,SlabName,GiftDesc,GMax,GMin,GName,Status";
          $(".data-table-basic_length2").on("change",
          function () {
              pgNo = 1;
              PgRecords = $(this).val();
              ReloadTable();
          }
          );
          $(".data-table-basic_length3").on("change",
          function () {
              pgNo = 1;
              PgRecords = $(this).val();
              ReloadTable3();
          }
          );
          $(".data-table-basic_length").on("change",
          function () {
              pgNo = 1;
              PgRecords = $(this).val();
              ReloadTable2();
          }
          );
          $("#tSearchOrd2").on("keyup", function () {
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
          })
          function fillRetailBusinessID(divcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getRetailBusinessID",
                  data: "{'divcode':" + divcode + "}",
                  dataType: "json",
                  success: function (data) {
                      var dts = JSON.parse(data.d) || [];
                      $('#hrcode').val(dts[0].ID);
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }
          function fillGiftProductsID(divcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getGiftProductsID",
                  data: "{'divcode':" + divcode + "}",
                  dataType: "json",
                  success: function (data) {
                      var dets = JSON.parse(data.d) || [];
                      $('#hbgpcode').val(dets[0].ID);
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }
          function fillGiftID(divcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getGiftID",
                  data: "{'divcode':" + divcode + "}",
                  dataType: "json",
                  success: function (data) {
                      var dets = JSON.parse(data.d) || [];
                      $('#hbcode').val(dets[0].ID);
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }

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
                  pgNo = parseInt($(this).attr("data-dt-idx"));
                  ReloadTable(); 
              }
             );
          }

          function loadPgNos2() {
              prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
              Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
              $(".pagination").html("");
              TotalPg = parseInt(parseInt(Orders2.length / PgRecords) + ((Orders2.length % PgRecords) ? 1 : 0)); selpg = 1;
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
                  pgNo = parseInt($(this).attr("data-dt-idx"));
                   ReloadTable2();
              }
             );
          }

          function loadPgNos3() {
              prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
              Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
              $(".pagination").html("");
              TotalPg = parseInt(parseInt(GPOrders.length / PgRecords) + ((GPOrders.length % PgRecords) ? 1 : 0)); selpg = 1;
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
                  pgNo = parseInt($(this).attr("data-dt-idx"));
                   ReloadTable3();
              }
             );
          }
          $(".segment>li").on("click", function () {
              $(".segment>li").removeClass('active');
              $(this).addClass('active');
              filtrkey = $(this).attr('data-va');
              Orders = AllOrders;
              $("#tSearchOrd").val('');
              ReloadTable();
          });
          $("#t2SearchOrd").on("keyup", function () {
              if ($(this).val() != "") {
                  shText = $(this).val().toLowerCase();
                  GPOrders = GPAllOrders.filter(function (a) {
                      chk = false;
                      $.each(a, function (key, val) {
                          if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys3).indexOf(',' + key + ',') > -1) {
                              chk = true;
                          }
                      })
                      return chk;
                  })
              }
              else
                  GPOrders = GPAllOrders
              ReloadTable3();
          });
          $("#tSearchOrd").on("keyup", function () {
              if ($(this).val() != "") {
                  shText = $(this).val().toLowerCase();
                  Orders2 = AllOrders2.filter(function (a) {
                      chk = false;
                      $.each(a, function (key, val) {
                          if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys2).indexOf(',' + key + ',') > -1) {
                              chk = true;
                          }
                      })
                      return chk;
                  })
              }
              else
                  Orders2 = AllOrders2
              ReloadTable2();
          });
          function ReloadTable3() {
              $("#giftpList TBODY").html("");
              if (filtrkey3 != "All") {
                  GPOrders = GPOrders.filter(function (a) {
                      return a.Active_Flag == filtrkey3;
                  })
              }
              st = PgRecords * (pgNo - 1); slno = 0;
              for ($i = st; $i < st + PgRecords ; $i++) {
                  if ($i < GPOrders.length) {
                      tr = $("<tr></tr>");
                      slno = $i + 1;
                      $(tr).html('<td>' + slno + '</td><td>' + GPOrders[$i].PName + '</td><td id=' + GPOrders[$i].ID +
                      ' class="sfedit3"><a href="#">Edit</a></td>' +
                      '<td>' + GPOrders[$i].Status + '</td>');
                      $("#giftpList TBODY").append(tr);
                  }
              }
              $("#gporders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < GPOrders.length) ? (st + PgRecords) : GPOrders.length) + " of " + GPOrders.length + " entries")
              loadPgNos3();
          }
          function ReloadTable2() {
              $("#OrderList TBODY").html("");
              if (filtrkey2 != "All") {
                  Orders2 = Orders2.filter(function (a) {
                      return a.Active_Flag == filtrkey2;
                  })
              }
              st = PgRecords * (pgNo - 1); slno = 0;
              for ($i = st; $i < st + PgRecords ; $i++) {
                  if ($i < Orders2.length) {
                      tr = $("<tr></tr>");
                      slno = $i + 1;
                      $(tr).html('<td>' + slno + '</td><td>' + Orders2[$i].GName + '</td><td>' + Orders2[$i].GiftDesc + '</td><td>' + Orders2[$i].GMin +
                      '</td><td>' + Orders2[$i].GMax + '</td><td>' + Orders2[$i].SlabName + '</td><td id=' + Orders2[$i].ID +
                      ' class="sfedit2"><a href="#">Edit</a></td>' +
                      '<td>' + Orders2[$i].Status + '</td>');

                      $("#OrderList TBODY").append(tr);
                  }
              }
              $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders2.length) ? (st + PgRecords) : Orders2.length) + " of " + Orders2.length + " entries")
              loadPgNos2();
          }
          function ReloadTable() {
              $("#retailbslab TBODY").html("");
              if (filtrkey != "All") {
                  Orders = Orders.filter(function (a) {
                      return a.Active_Flag == filtrkey;
                  })
              }
              st = PgRecords * (pgNo - 1); slno = 0;
              for ($i = st; $i < st + PgRecords ; $i++) {
                  if ($i < Orders.length) {
                      tr = $("<tr></tr>");
                      slno = $i + 1;
                      $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].RBName + '</td><td>' + Orders[$i].RBDesc + '</td><td>' + Orders[$i].RBMin +
                      '</td><td>' + Orders[$i].RBMax + '</td><td>' + Orders[$i].Giftcnt + '</td><td id=' + Orders[$i].ID +
                      ' class="sfedit"><input type="hidden"  value="' + Orders[$i].Giftcnt + '"><a href="#">Edit</a></td>' +
                      '<td>' + Orders[$i].Status + '</td>');

                      $("#retailbslab TBODY").append(tr);
                  }
              }
              $("#retailbslab_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
              loadPgNos();
          }
          function clear() {
              $('#hrcode').val('');
              $('#txtrname').val('');
              $('#txtrdesc').val('');
              $('#rminvalue').val('');
              $('#rmaxvalue').val('');
              $('#ddlrstatus').val(2);
			  $('#mselect2').val('');
              fillRetailBusinessID(divcode);
              pgNo = 1; PgRecords = 10; TotalPg = 0; filtrkey = '0';
              ReloadTable();
          }
          function clear3() {
              $('#hbgpcode').val('');
              $('#txttype').val('');
              $('#ddlgpstatus').val(2);
              $('#gpfdt').val('');
              $('#gptdt').val('');
              fillGiftProductsID(divcode);
              pgNo = 1; PgRecords = 10; TotalPg = 0; filtrkey3 = '0';
              ReloadTable3();
          }
          function clear2() {
              $('#hbcode').val('');
              $('#txtbname').val('');
              $('#txtbdesc').val('');
              $('#bminvalue').val('');
              $('#bmaxvalue').val('');
              $('#fdt').val('');
              $('#tdt').val('');
              $('#rbsddl').val(0);
			  $('#mselect').val('');
              $('#ddlsl').val(2);
              pgNo = 1; PgRecords = 10; TotalPg = 0; filtrkey2 = '0';
              ReloadTable2();
              fillGiftID(divcode);
          }
          function loadData(divcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getRetailBusiness",
                  data: "{'divcode':'" + divcode + "'}",
                  dataType: "json",
                  success: function (data) {
                      AllOrders = JSON.parse(data.d) || [];
                      Orders = AllOrders;
                      ReloadTable();
                      var activ = [];
                      activ = AllOrders.filter(function (a) {
                          return a.Active_Flag == 0;
                      })
                      var rbsddl = $('#rbsddl');
                      rbsddl.empty().append($('<option value="0">--Select--</option>'))
                      for (var i = 0; i < activ.length; i++) {
                          rbsddl.append($('<option value="' + activ[i].ID + '">' + activ[i].RBName + ' - ' + activ[i].RBDesc + '</option>'));
                      }
                  },
                  error: function (result) {
                  }
              });
          }
		  function fillhq(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Gift_Claim_Master.aspx/getsHQ",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    var HQ = JSON.parse(data.d) || [];
                    var sf = $("#mselect");
                    sf.empty();
                    for (var i = 0; i < HQ.length; i++) {
                        sf.append($('<option value="' + HQ[i].HQ_ID + '">' + HQ[i].HQ_Name + '</option>'));
                    }
                    $('#mselect').multiselect({
                        columns: 3,
                        placeholder: 'Select Value',
                        search: true,
                        searchOptions: {
                            'default': 'Search Value'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                },
                error: function (data) {
                }
            });
        }
		  function fillhq2(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Gift_Claim_Master.aspx/getsHQ",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    var HQ = JSON.parse(data.d) || [];
                    var sf = $("#mselect2");
                    sf.empty();
                    for (var i = 0; i < HQ.length; i++) {
                        sf.append($('<option value="' + HQ[i].HQ_ID + '">' + HQ[i].HQ_Name + '</option>'));
                    }
                    $('#mselect2').multiselect({
                        columns: 3,
                        placeholder: 'Select Value',
                        search: true,
                        searchOptions: {
                            'default': 'Search Value'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                },
                error: function (data) {
                }
            });
        }
          function loadData3(divcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getAllGiftProducts",
                  data: "{'divcode':'" + divcode + "'}",
                  dataType: "json",
                  success: function (data) {
                      GPAllOrders = JSON.parse(data.d) || [];
                      GPOrders = GPAllOrders;
                      ReloadTable3();
                  },
                  error: function (result) {
                  }
              });
          }
          $('#gslab').on('click', function () {
              clear2();
          });
          function loadData2(divcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getGiftDets",
                  data: "{'divcode':'" + divcode + "'}",
                  dataType: "json",
                  success: function (data) {
                      AllOrders2 = JSON.parse(data.d) || [];
                      Orders2 = AllOrders2;
                      ReloadTable2();
                  },
                  error: function (result) {
                  }
              });
          }
          function fillRetailPop(scode, gcount) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getRetailUpdt",
                  data: "{'divcode':" + divcode + ",'scode':" + scode + "}",
                  dataType: "json",
                  success: function (data) {
                      var rpdts = JSON.parse(data.d) || [];
                      var dt = new Date();
                      var dtt = dt.getFullYear() + '-' + (dt.getMonth() + 1) + '-' + dt.getDate();
                      var cdldt=(rpdts[0].Claim_deadline!=null )?rpdts[0].Claim_deadline.split('T'):"";
                      $('#hrcode').val(rpdts[0].RetSlabID);
                      $('#txtrname').val(rpdts[0].SlabName);
                      $('#txtrdesc').val(rpdts[0].SlabDesc);
                      $('#rminvalue').val(rpdts[0].SlabMinVal);
                      $('#rmaxvalue').val(rpdts[0].SlabMaxVal);
                      $('#ddlrstatus').val(rpdts[0].ActiveFlag);
                      var rbfd = (rpdts[0].From_dt).split('T');
                      var rbtfd = (rpdts[0].To_Dt).split('T');
                      $('#rfdt').val(rbfd[0]);
                      $('#rtdt').val(rbtfd[0]);
                      $('#dedt').val(cdldt);
					  var hq2 = rpdts[0].Sf_HQ;
                      hq2 = hq2.split(',');
                      $('#mselect2  > option').each(function () {
                          for (var i = 0; i < hq2.length; i++) {
                              if (hq2[i] == $(this).val()) { $(this).prop('selected', true); }
                          }
                      }); $('#mselect2').multiselect('reload')
                      if (gcount > 0) {
                          $('#ddlrstatus').val(0);
                          $('#ddlrstatus').prop("disabled", true)
                      }
                      else{
                          $('#ddlrstatus').prop("disabled", false)
                      }
                      if(dtt>rbtfd[0]){
                          $('#ddlrstatus').prop("disabled", false)
                      }
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }
          function getdatalist() {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  url: "Gift_Claim_Master.aspx/GetDesgnName",
                  dataType: "json",
                  success: function (data) {
                      dts = data.d;
                      if (dts.length > 0) {
                          var st = $('#products');
                          for (var i = 0; i < dts.length; i++) {
                              st.append($('<option value="' + dts[i].label + '">'));
                          }
                      }
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }
          function fillGiftProdPop(gpcode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getGiftProdUpdt",
                  data: "{'divcode':" + divcode + ",'scode':" + gpcode + "}",
                  dataType: "json",
                  success: function (data) {
                      var gbdts = JSON.parse(data.d) || [];
                      $('#hbgpcode').val(gbdts[0].sl_no);
                      $('#txttype').val(gbdts[0].Product_Name);
                      $('#ddlgpstatus').val(gbdts[0].Active_Flag);
                      var rfd = (gbdts[0].From_dt).split('T');
                      var tfd = (gbdts[0].To_dt).split('T');
                      $('#gpfdt').val(rfd[0]);
                      $('#gptdt').val(tfd[0]);
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }
          function fillGiftPop(scode) {
              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  async: false,
                  url: "Gift_Claim_Master.aspx/getGiftUpdt",
                  data: "{'divcode':" + divcode + ",'scode':" + scode + "}",
                  dataType: "json",
                  success: function (data) {
                      var gbdts = JSON.parse(data.d) || [];
                      $('#hbcode').val(gbdts[0].GiftSlabID);
                      $('#txtbname').val(gbdts[0].GiftName);
                      $('#txtbdesc').val(gbdts[0].GiftDesc);
                      $('#bminvalue').val(gbdts[0].GiftMinVal);
                      $('#bmaxvalue').val(gbdts[0].GiftMaxVal);
                      var rfd = (gbdts[0].From_Date).split('T');
                      var tfd = (gbdts[0].To_Date).split('T');
                      $('#fdt').val(rfd[0]);
                      $('#tdt').val(tfd[0]);
					  var hq1 = gbdts[0].Sf_HQ;
                      hq1 = hq1.split(',');
                      $('#mselect  > option').each(function () {
                          for (var i = 0; i < hq1.length; i++) {
                              if (hq1[i] == $(this).val()) { $(this).prop('selected', true); }
                          }
                      }); $('#mselect').multiselect('reload')
                      $('#rbsddl').val(gbdts[0].RetailSlabID);
                      $('#ddlsl').val(gbdts[0].GiftType);
                  },
                  error: function (data) {
                      alert(JSON.stringify(data));
                  }
              });
          }
          $(document).ready(function () {
              divcode = Number(<%=Session["div_code"]%>);
            loadData(divcode);fillhq(divcode); fillhq2(divcode); getdatalist();
            $('.rslab').on('click', function () {
                clear();
            });
            $('.gpslab').on('click', function () {
                clear3();
            });
            loadData2(divcode);
            loadData3(divcode);
            fillRetailBusinessID(divcode); fillGiftID(divcode); fillGiftProductsID(divcode);
            //$('#rbsopen').on('click', function () {
            //    $("#MyPopup").modal("hide");
            //    fillRetailBusinessID(divcode);
            //    //$('#RetailPopup').delay(3000).modal('show')
            //});
            $('#MyPopup').on('show.bs.modal', function () {
                clear2();
            });
            $('#RetailPopup').on('show.bs.modal', function () {
                clear();
                $('#ddlrstatus').prop("disabled", false)
            });
            $('#MyGPPopup').on('show.bs.modal', function () {
                clear3();
            });
            $(".segment1>li").on("click", function () {
                $(".segment1>li").removeClass('active');
                $(this).addClass('active');
                filtrkey2 = $(this).attr('data-va');
                Orders2 = AllOrders2;
                $("#tSearchOrd").val('');
                ReloadTable2();
            });
            $(".segment2>li").on("click", function () {
                $(".segment2>li").removeClass('active');
                $(this).addClass('active');
                filtrkey3 = $(this).attr('data-va');
                GPOrders = GPAllOrders;
                $("#t2SearchOrd").val('');
                ReloadTable3();
            });
            $('#btnsave').on('click', function () {
                var gcode = $('#hbcode').val();
                var gname = $('#txtbname').val();
                if (gname == '' || gname == null) {
                    alert('Enter the Gift Name');
                    return false;
                }
                var gdesc = $('#txtbdesc').val();
                var gminvalue = $('#bminvalue').val();
                if (gminvalue == '' || gminvalue == null) {
                    alert('Enter the Min Value');
                    return false;
                }
                var gmaxvalue = $('#bmaxvalue').val();
                if (gmaxvalue == '' || gmaxvalue == null) {
                    alert('Enter the Max Value');
                    return false;
                }
                var grbslab = $('#rbsddl').val();
                if (grbslab == '0') {
                    alert('Select the Retail Business Slab');
                    return false;
                }
				var hq = '';
                $('#mselect  > option:selected').each(function () {
                    hq += ',' + $(this).val();
                });
                var gtype = $('#ddlsl').val();
                if (gtype == '2') {
                    alert('Select the Type');
                    return false;
                }
                var gfdt = $('#fdt').val();
                if (gfdt == '') {
                    alert('Select From Date');
                    return false;
                }
                var gtdt = $('#tdt').val();
                if (gtdt == '') {
                    alert('Select To Date');
                    return false;
                }
                data = { "Divcode": divcode, "GCode": gcode, "GName": gname, "GDesc": gdesc, "GMinValue": gminvalue, "GMaxValue": gmaxvalue, "Gtype": gtype, "FDT": gfdt, "TDT": gtdt, "RbSlab": grbslab,"hqdtl": hq }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Gift_Claim_Master.aspx/saveGift",
                    data: "{'data':'" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        $("#MyPopup").modal("hide");
                        clear2();
                        loadData2(divcode);
						fillhq(divcode);
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
            
            $('#btngpsave').on('click', function () {
                var gpscode = $('#hbgpcode').val();
                var gpPname = $('#txttype').val();
                if (gpPname == '' || gpPname == null) {
                    alert('Enter the Gift Product Name');
                    return false;
                }
                var gpsstatus = $('#ddlgpstatus').val();
                if (gpsstatus == '2') {
                    alert('Select the Status');
                    return false;
                }
                var gpfdt = $('#gpfdt').val();
                if (gpfdt == '') {
                    alert('Select From Date');
                    return false;
                }
                var gptdt = $('#gptdt').val();
                if (gptdt == '') {
                    alert('Select To Date');
                    return false;
                }
                data = { "Divcode": divcode, "GPSCode": gpscode, "GPSName": gpPname, "GPSStatus": gpsstatus, "GPfdt": gpfdt, "GPtdt": gptdt }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Gift_Claim_Master.aspx/saveGiftProduct",
                    data: "{'data':'" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        $("#MyGPPopup").modal("hide");
                        clear3();
                        loadData3(divcode);
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
            $('#btnrsave').on('click', function () {
                var rbscode = $('#hrcode').val();
                var rbsname = $('#txtrname').val();
                if (rbsname == '' || rbsname == null) {
                    alert('Enter the Retail Business Slab Name');
                    return false;
                }
                var rbsdesc = $('#txtrdesc').val();
                var rbsminvalue = $('#rminvalue').val();
                if (rbsminvalue == '' || rbsminvalue == null) {
                    alert('Enter the Min Value');
                    return false;
                }
                var rbsmaxvalue = $('#rmaxvalue').val();
                if (rbsmaxvalue == '' || rbsmaxvalue == null) {
                    alert('Enter the Max Value');
                    return false;
                }
                var rbsstatus = $('#ddlrstatus').val();
                if (rbsstatus == '2') {
                    alert('Select the Status');
                    return false;
                }
				var hqid = '';
                $('#mselect2  > option:selected').each(function () {
                    hqid += ',' + $(this).val();
                });
                var rbfdt = $('#rfdt').val();
                if (rbfdt == '') {
                    alert('Select From Date');
                    return false;
                }
                var rbtdt = $('#rtdt').val();
                if (rbtdt == '') {
                    alert('Select To Date');
                    return false;
                }
                var rdedt = $('#dedt').val();
                data = { "Divcode": divcode, "RBSCode": rbscode, "RBSName": rbsname, "RBSDesc": rbsdesc, "RMinValue": rbsminvalue, "RMaxValue": rbsmaxvalue, "RBSStatus": rbsstatus, "Rbfdt": rbfdt, "Rbtdt": rbtdt, "Rdedt": rdedt, "hq": hqid}
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Gift_Claim_Master.aspx/saveRetailBusiness",
                    data: "{'data':'" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        $("#RetailPopup").modal("hide");
                        clear();
                        loadData(divcode);
						fillhq2(divcode);
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
            $(document).on('click', '.sfedit', function () {
                x = this.id;
                var gcount = this.firstChild.value
                $("#RetailPopup").modal("show");
                fillRetailPop(x, gcount);
            });
            $(document).on('click', '.sfedit3', function () {
                x = this.id;
                $("#MyGPPopup").modal("show");
                fillGiftProdPop(x);
            });
            $(document).on('click', '.sfedit2', function () {
                x = this.id;
                $("#MyPopup").modal("show");
                fillGiftPop(x);
            });
        })
    </script>
</asp:Content>

