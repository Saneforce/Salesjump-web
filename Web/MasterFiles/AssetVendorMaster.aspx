<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AssetVendorMaster.aspx.cs" Inherits="MasterFiles_AssetVendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
              <link href="../css/jquery.multiselect.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        </head>
  
<style>
    th {
        white-space: nowrap;
        cursor: pointer;
    }
</style>
        <body>
         <form runat="server">
           <div>
             <div class="col-lg-12 sub-header">Vendor Master
            <div class="row">
            <div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> 
                <div style="float: right;padding-top: 3px; width: 136px;">
                    <button class="btn btn-primary" id="newvend" data-toggle="modal" data-target="#addvendor">Add Vendor</button>
            
            </div>
        </div>
            </div>
        </div>
          <br />
        <br />
        <br />

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
                <table class="table table-hover" id="vendtable" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th style="text-align: left">Vendor Name</th>
                            <th style="text-align: left">Contact Person Name</th>
                            <th style="text-align: left">Email</th>
                            <th style="text-align: left;">GSTIN.No</th>
                            <th style="text-align: left">Phone.No</th>
                            <th style="text-align: left">Edit</th>
                            <th style="text-align: left">Status</th>
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
              <div id="addvendor" class="modal fade" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                        <div class="modal-dialog" role="document" style="width: 80% !important">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Add Vendor</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hvencode" />
                                    <div class="col-md-12">
                                        <div class="col-lg-6">
                                            <div class="row">
                                                  <div class="col-md-3">
                                                     <label>Vendor Name<span style="color:red;font:bold;">  *</span></label>
                                            </div>
                                           <div class="col-md-5">
                                             <input type="text" id="txtvennm" class="form-control" />
                                           </div>
                                       </div>
                                      <div class="row" style="margin-top: 15px;">
                                        <div class="col-md-3">
                                          <label>Address Line1<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                       <div class="col-md-5">
                                          <input type="text" id="txtadd1" class="form-control" />
                                       </div>
                                    </div>
                                   <div class="row" style="margin-top: 15px;">
                                        <div class="col-md-3">
                                           <label>Address Line2</label>
                                       </div>
                                        <div class="col-md-5">
                                            <input type="text" id="txtadd2" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 15px;">
                                        <div class="col-md-3">
                                           <label>GSTIN Number</label>
                                       </div>
                                        <div class="col-md-5">
                                            <input type="text" id="txtgstnum" class="form-control" />
                                        </div>
                                    </div>
                                     <div class="row" style="margin-top: 15px;">
                                         <div class="col-sm-3">
                                            <label>Country<span style="color:red;font:bold;">  *</span></label>
                                         </div>
                                         <div class="col-sm-5">
                                               <select id="ddlcntry" class="form-control">
                                                     <option value="0">Select Country</option>
                                               </select> 
                                         </div>
                                     </div>
                                     <div class="row" style="margin-top: 15px;">
                                         <div class="col-sm-3">
                                            <label>City<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtcity" class="form-control" />
                                        </div>
                                    </div>

                                        </div>
                                          <div class="col-lg-6">
                                                  <div class="card" style="padding: 8px; margin-top: 0px;">
                                                      <h5 style="color:blue;font:bold;text-align:center;">Contact Details</h5>
                                                      <div class="row" style="margin-top: 15px;">
                                                            <div class="col-md-3">
                                                               <label>Name</label>
                                                           </div>
                                                            <div class="col-md-5">
                                                                <input type="text" id="txtcontnm" class="form-control" />
                                                            </div>
                                                        </div>
                                                      <div class="row" style="margin-top: 15px;">
                                                            <div class="col-md-3">
                                                               <label>Mob.No</label>
                                                           </div>
                                                            <div class="col-md-5">
                                                                <input type="text" id="txtmob" class="form-control"  />
                                                            </div>
                                                          </div>
                                                          <div class="row" style="margin-top: 15px;">
                                                            <div class="col-md-3">
                                                               <label>E-Mail</label>
                                                           </div>
                                                            <div class="col-md-5">
                                                                <input type="text" id="txtemail" class="form-control"  />
                                                            </div>
                                                            </div>
                                          </div>
                                              <div class="row" style="margin-top: 15px;">
                                                        <div class="col-sm-3">
                                                           <label>State<span style="color:red;font:bold;">  *</span></label>
                                                        </div>
                                                        <div class="col-sm-5">
                                                              <select id="ddlstate" class="form-control" name="ddlstate">
                                                                    <option value="0">Select State</option>
                                                              </select> 
                                                        </div>
                                                    </div>
                                              <div class="row" style="margin-top: 15px;">
                                                    <div class="col-md-3">
                                                       <label>ZipCode</label>
                                                   </div>
                                                    <div class="col-md-5">
                                                        <input type="text" id="txtzipcode" placeholder="Pin Code" class="form-control" />  <%--"if(!(/(^\d{6}$)|(^\d{6}-\d{5}$)/.test(this.value))){this.value='';return false}"--%>
                                                    </div>
                                                </div>
                                          </div> 
                                    </div>
                                </div>
                                <div class="modal-footer" style="margin-top:-40px !important;">
                                    <button type="button" id="btnsave" class="btn btn-success">Save</button>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
              </div>
          </form>
            <script type="text/javascript" src="../js/plugins/timepicker/bootstrap-clockpicker.min.js"></script>
<script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
<script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
<script lang="javascript" type="text/javascript">
    var AllOrders = [], Orders = [], cnty = [], state = [];
    var filtrkey = '0';
    var pgNo = 1; PgRecords = 10; TotalPg = 0, searchKeys = "";
    $(document).ready(function () {
        loadData();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetVendorMaster.aspx/Getcntry",
            data: "{}",
            dataType: "json",
            success: function (data) {
                cnty = JSON.parse(data.d) || [];
                var ctyp = $("#ddlcntry");
                ctyp.empty().append('<option value="0">Select Country</option>');
                for (var i = 0; i < cnty.length; i++) {
                    ctyp.append($('<option value=' + cnty[i].Country_code + '>' + cnty[i].Country_name + '</option>'));
                }
                $('#ddlcntry').selectpicker({
                    liveSearch: true
                });
            },
            error: function (result) {
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetVendorMaster.aspx/Getstate",
            data: "{}",
            dataType: "json",
            success: function (data) {
                state = JSON.parse(data.d) || [];
                $('#ddlstate').selectpicker({
                    liveSearch: true
                });
            },
            error: function (result) {
            }
        });
    });
    $('#ddlcntry').on('change', function () {
        var cntrycd = $("#ddlcntry option:selected").val();
        loadState(cntrycd);
    });
    function loadState(cntrycd) {
        var stat = state.filter(function (a) {
            return a.Country_code == cntrycd;
        });
        $('#ddlstate').selectpicker('destroy');
        var st = $("#ddlstate");
        st.empty().append('<option value="0">Select State</option>');
        for (var i = 0; i < stat.length; i++) {
            st.append($('<option value=' + stat[i].State_Code + '>' + stat[i].StateName + '</option>'));
        }
        $('#ddlstate').selectpicker({
            liveSearch: true
        });
    }
    $(".segment>li").on("click", function () {
        $(".segment>li").removeClass('active');
        $(this).addClass('active');
        filtrkey = $(this).attr('data-va');
        Orders = AllOrders;
        $("#tSearchOrd").val('');
        ReloadTable();
    });
    $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
    );
    $(document).on('click', ".ddlStatus>li>a", function () {
        cStus = $(this).closest("td").find(".aState");
        let venid = $(this).closest("tr").find(".roedit").attr("id");
        stus = $(this).attr("v");
        $indx = Orders.findIndex(x => x.Vendor_Id == venid);
        cStusNm = $(this).text();
        if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
            id = Orders[$indx].Vendor_Id;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "AssetVendorMaster.aspx/SetNewStatus",
                data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                dataType: "json",
                success: function (data) {
                    Orders[$indx].Vendor_Active_Flag = stus;
                    Orders[$indx].Status = cStusNm;
                    $(cStus).html(cStusNm);

                    ReloadTable();
                    alert('Status Changed Successfully...');

                },
                error: function (result) {
                }
            });
        }
        loadPgNos();
    });
    $(document).on('click', '.roedit', function () {
        x = this.id;
        $("#hvencode").val(x);
        $("#addvendor").modal("show");
        fillVendor(x);
    });
    function fillVendor(vcode) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetVendorMaster.aspx/getAssetVend_Id",
            data: "{'vcode':'" + vcode + "'}",
            dataType: "json",
            success: function (data) {
                var astven = JSON.parse(data.d) || [];
                $("#txtvennm").val(astven[0].Vendor_Name);
                $("#txtadd1").val(astven[0].Address_Line1);
                $("#txtadd2").val(astven[0].Address_Line2);
                $('#ddlcntry').selectpicker('val', astven[0].Vendor_Country);
                loadState(astven[0].Vendor_Country);
                $("#txtgstnum").val(astven[0].GSTIN_No);
                $("#txtcity").val(astven[0].Vendor_City);
                $("#ddlstate").selectpicker('val', astven[0].Vendor_State);
                $("#txtzipcode").val(astven[0].Vendor_pincode);
                $("#txtcontnm").val(astven[0].Contact_Name);
                $("#txtmob").val(astven[0].Contact_Number);
                $("#txtemail").val(astven[0].Contact_Email);
            },
            error: function (data) {
                //alert(JSON.stringify(data));
            }
        });
    }
    $('#newvend').on('click', function () {
        $("#hvencode").val('');
        $("#txtvennm").val('');
        $("#txtadd1").val('');
        $("#txtadd2").val('');
        $("#txtgstnum").val('');
        $('#ddlcntry').selectpicker('val', '0');
        loadState(0);
        $("#txtcity").val('');
        $("#ddlstate").selectpicker('val', '0');
        $("#txtzipcode").val('');
        $("#txtcontnm").val('');
        $("#txtmob").val('');
        $("#txtemail").val('');
    });
    $('#btnsave').on('click', function () {
        var hiddenid = $("#hvencode").val();
        var locnam = $("#txtvennm").val();
        if (locnam == '') {
            alert("Please Enter Vendor Name");
            $("#txtvennm").focus();
            return false;
        }
        var addln1 = $("#txtadd1").val();
        if (addln1 == '') {
            alert("Please Fill AddressLine1");
            $("#txtadd1").focus();
            return false;
        }
        var addln2 = $("#txtadd2").val();
        var cntrycd = $("#ddlcntry option:selected").val();
        if (cntrycd == '0') {
            alert("Please Select Country");
            $("#ddlcntry").focus();
            return false;
        }
        var city = $("#txtcity").val();
        if (city == '') {
            alert("Please Enter City.");
            $("#txtcity").focus();
            return false;
        }
        var statcd = $("#ddlstate option:selected").val();
        if (statcd == '0') {
            alert("Please select State");
            $("#ddlstate").focus();
            return false;
        }
        var zipcode = $("#txtzipcode").val();
        if (zipcode == '') {
            alert("Please Enter Zipcode");
            $("#txtzipcode").focus();
            return false;
        }
        var gstno = $("#txtgstnum").val();
        if (gstno == '') {
            alert("Please Enter GSTIN Number");
            $("#txtgstnum").focus();
            return false;
        }
        var contactnm = $("#txtcontnm").val();
        var contactnum = $("#txtmob").val();
        var contactemail = $("#txtemail").val();

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetVendorMaster.aspx/Save_Vendor",
            data: "{'locnam':'" + locnam + "','addln1':'" + addln1 + "','addln2':'" + addln2 + "','cntrycd':'" + cntrycd + "','city':'" + city + "','statcd':'" + statcd + "','zipcode':'" + zipcode + "','contactnm':'" + contactnm + "','contactnum':'" + contactnum + "','contactemail':'" + contactemail + "','hiddenid':'" + hiddenid + "','gstno':'" + gstno +"'}",
            dataType: "json",
            success: function (data) {
                alert(data.d);
                $("#addvendor").modal("hide");
                loadData();
            },
            error: function (result) {
            }
        });

    });
    function loadPgNos() {
        prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
        Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
        $(".pagination").html("");
        TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0));
        selpg = 1;
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
    function ReloadTable() {

        var tr = '';
        $("#vendtable TBODY").html("");
        if (filtrkey != "All") {
            Orders = Orders.filter(function (a) {
                return a.Vendor_Active_Flag == filtrkey;
            })
        }
        st = PgRecords * (pgNo - 1);
        for ($i = st; $i < st + Number(PgRecords); $i++) {
            if ($i < Orders.length) {

                tr = $("<tr rname='" + Orders[$i].Vendor_Name + "'  rocode='" + Orders[$i].Vendor_Id + "'></tr>");
                $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Vendor_Name + "</td><td>" + Orders[$i].Contact_Name + "</td><td>" + Orders[$i].Contact_Email + "</td><td>" + Orders[$i].GSTIN_No + "</td><td>" + Orders[$i].Contact_Number + "</td><td id='" + Orders[$i].Vendor_Id + "' class='roedit'><a href='#'>Edit</a></td><td><ul class='nav' style='margin: 0px'><li class='dropdown'><a href='#' style='padding: 0px' class='dropdown - toggle' data-toggle='dropdown'>" /*< td  class= 'rocount' > <a href='#'>" + Orders[$i].Assets + "</a></td >*/
                    + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                    '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + ((Orders[$i].Status == "Active") ? '<li><a href="#" v="1">Deactivate</a></li>' : '<li><a href="#" v="0">Active</a></li>') + '</ul></li></ul></td>');

                $("#vendtable TBODY").append(tr);
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
            Orders = AllOrders;
        ReloadTable();
    });
    function loadData() {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetVendorMaster.aspx/GetDetails",
            data: "{}",
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
    function validatePin(event, input) {
        event.preventDefault();
        var currVal = (input.value && input.value != " ") ? input.value : "";
        // Checks if the key pressed is a number and the right length and not an space
        if (!isNaN(event.key) && currVal.length < 6) {
            input.value = currVal + event.key;
        }
        // Backspace functionality
        else if (event.keyCode == 8 && currVal > 0) {
            input.value = input.value.slice(0, -1);
        }
    }
</script>
            </body>
    </html>
</asp:Content>

