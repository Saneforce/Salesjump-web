<%@ Page Title="AssetLocation" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AssetLocationMaster.aspx.cs" Inherits="MasterFiles_AssetLocationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        #ddlcntry_chzn {
    width: 300px !important;
    top: 10px;
}
        #ddlstate_chzn {
    width: 300px !important;
    top: 10px;
}
        th {
            white-space: nowrap;
            cursor: pointer;
        }
    </style>
     <form runat="server">
           <div>
             <div class="col-lg-12 sub-header">Location Master
            <div class="row">
            <div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> 
               <%-- <div style="float: right;padding-top: 3px;">
                    
            <a href="AssetCategory.aspx" class="btn btn-primary" id="bulkcat">Bulk Upload</a></div>--%>
                <div style="float: right;padding-top: 3px; width: 136px;">
                    <button class="btn btn-primary" id="newloc" data-toggle="modal" data-target="#addlocation">Add Location</button>
            
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
                <table class="table table-hover" id="altable" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th style="text-align: left">Location Name</th>
                            <th style="text-align: left">Contact Person Name</th>
                            <th style="text-align: left">Contact Number</th>
                            <th style="text-align: left;">No.of Assets</th>
                            <th style="text-align: left">State</th>
                            <th style="text-align: left">City</th>
                            <%--<th style="text-align: left">Created By</th>
                            <th style="text-align: left">Modifed By</th>--%>
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
              <div id="addlocation" class="modal fade" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                        <div class="modal-dialog" role="document" style="width: 80% !important">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Add Location</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hgrpcode" />
                                    <div class="col-md-12">
                                        <div class="col-lg-6">

                                            <div class="row">
                                                  <div class="col-md-3">
                                                     <label>Location Name<span style="color:red;font:bold;">  *</span></label>
                                            </div>
                                           <div class="col-md-5">
                                             <input type="text" id="txtcat" class="form-control" autocomplete="off" onkeypress="return (event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)" />
                                           </div>
                                       </div>
                                      <div class="row" style="margin-top: 15px;">
                                        <div class="col-md-3">
                                          <label>Address Line1<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                       <div class="col-md-5">
                                          <input type="text" id="txtadd1" class="form-control" autocomplete="off" />
                                       </div>
                                    </div>
                                    <div class="row" style="margin-top: 15px;">
                                        <div class="col-md-3">
                                           <label>Address Line2</label>
                                       </div>
                                        <div class="col-md-5">
                                            <input type="text" id="txtadd2" class="form-control" autocomplete="off" />
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
                                            <input type="text" id="txtcity" class="form-control" autocomplete="off" />
                                            <%--<select id="ddlcity" class="form-control">
                                                <option value="0">Select City</option>
                                            </select> --%>
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
        <input type="text" id="txtcontnm" class="form-control" autocomplete="off" />
    </div>
</div>
                                                      <div class="row" style="margin-top: 15px;">
    <div class="col-md-3">
       <label>Mob.No</label>
   </div>
    <div class="col-md-5">
        <input type="text" id="txtmob" class="form-control" autocomplete="off" />
    </div>
                                                          </div>
                                                          <div class="row" style="margin-top: 15px;">
    <div class="col-md-3">
       <label>E-Mail</label>
   </div>
    <div class="col-md-5">
        <input type="text" id="txtemail" class="form-control" autocomplete="off" />
    </div>
</div>
            <%--<table id="contact_tbl" class="table table-hover">
                <thead>
    <tr>
        <th style="text-align:center;">Name</th>
        <th style="text-align:center;">Mob.No</th>
        <th style="text-align:center;">Email</th>
    </tr>
               </thead>
                <tbody>
                    <tr><td><input type="text" id="txtnm" class="form-control" autocomplete="off" /></td>
                        <td><input type="text" id="txtnum" class="form-control" autocomplete="off" /></td>
                        <td><input type="text" id="txtemail" class="form-control" autocomplete="off" /></td>
                        <td><a name="btnadd" class="btn btn-primary"><span>+</span></a></td>
                    </tr> 
                </tbody>
                <tfoot>
                </tfoot>
            </table>--%>
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
        <input type="text" id="txtzipcode" placeholder="Pin Code" class="form-control" autocomplete="off" onkeydown="validateNum(event, this)"/>  <%--"if(!(/(^\d{6}$)|(^\d{6}-\d{5}$)/.test(this.value))){this.value='';return false}"--%>
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
    var AllOrders = [], Orders = [], cnty = [], state=[];
    var filtrkey = '0';
    var pgNo = 1; PgRecords = 10; TotalPg = 0, searchKeys = "Location_Name,Contact_Name,Contact_Number,StateName,Location_City,";
    $(document).ready(function () {
        loadData();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetLocationMaster.aspx/Getcntry",
            data: "{}",
            dataType: "json",
            success: function (data) {
                cnty = JSON.parse(data.d) || [];
                var ctyp = $("#ddlcntry");
                ctyp.empty().append('<option value="0">Select Country</option>');
                for (var i = 0; i < cnty.length; i++) {
                    ctyp.append($('<option value=' + cnty[i].Country_code + '>' + cnty[i].Country_name + '</option>'));
                }
                //$('#ddlcntry').chosen();
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
            url: "AssetLocationMaster.aspx/Getstate",
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
    $('#btnsave').on('click', function () {
        var locnam = $("#txtmod").val();
    });
    $('#newloc').on('click', function () {
        
    });
    $('#ddlcntry').on('change', function () {
        var cntrycd = $("#ddlcntry option:selected").val();
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
    });
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
        $("#altable TBODY").html("");
        if (filtrkey != "All") {
            Orders = Orders.filter(function (a) {
                return a.Location_Active_Flag == filtrkey;
            })
        }
        st = PgRecords * (pgNo - 1);
        for ($i = st; $i < st + Number(PgRecords); $i++) {
            if ($i < Orders.length) {

                tr = $("<tr rname='" + Orders[$i].Location_Name + "'  rocode='" + Orders[$i].Location_Id + "'></tr>");
                $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Location_Name + "</td><td>" + Orders[$i].Contact_Name + "</td><td>" + Orders[$i].Contact_Number + "</td><td  class='rocount'><a href='#'>" + Orders[$i].Assets + "</a></td><td>" + Orders[$i].StateName + "</td><td>" + Orders[$i].Location_City + "</td><td id='" + Orders[$i].Location_Id + "' class='roedit'><a href='#'>Edit</a></td><td><ul class='nav' style='margin: 0px'><li class='dropdown'><a href='#' style='padding: 0px' class='dropdown - toggle' data-toggle='dropdown'>"
                    + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                    '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + ((Orders[$i].Status == "Active") ? '<li><a href="#" v="1">Deactivate</a></li>' : '<li><a href="#" v="0">Active</a></li>') + '</ul></li></ul></td>');

                $("#altable TBODY").append(tr);
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
            url: "AssetLocationMaster.aspx/GetDetails",
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
    function validateNum(event, input) {
        event.preventDefault();
        var currVal = (input.value && input.value!=" ") ? input.value : "";
        // Checks if the key pressed is a number and the right length and not an space
        if (!isNaN(event.key) && currVal.length < 6) {
            input.value = currVal + event.key;
        }
        // Backspace functionality
        else if (event.keyCode == 8 && currVal > 0) {
            input.value = input.value.slice(0, -1);
        }
    }
    $(document).on('click', 'a[name=btnadd]', function () {
    if ($(this).text().toString() == "+") {
        $(this).text("-");
        tr = $("<tr></tr>");
        $(tr).html('<td><input type="text" id="txtnm" class="form-control" autocomplete="off" /></td><td><input type="text" id="txtnum" class="form-control" autocomplete="off" /></td><td><input type="text" id="txtemail" class="form-control" autocomplete="off" /></td><td><a name="btnadd" class="btn btn-primary"><span>+</span></a></td>');
        $("#contact_tbl > TBODY").append(tr);
    }
    else {
        //x = $(this).closest('tr').find('input[name=addeamt]'); $(x).val('0');
        //calcAddi(x);
        $(this).closest('tr').remove();
    }
    });
</script>

</asp:Content>

