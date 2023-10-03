<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="HO_User_Master.aspx.cs" Inherits="MasterFiles_HO_User_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <style>
        .image{
     width: 100%;
  position: relative;
 
}
.ab{
 
  display: flex;
  align-items: center;
  position: absolute;
  height: 100%;
  top: 0;
  right: 0;
  padding-right: 5px;
}
    </style>
<div class="row">
        <div class="col-lg-12 sub-header">HO Users List<span style="float:right;"><a href="#" class="btn btn-primary btn-update" id="nwgrp">New HO User</a></span></div>
    </div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width:250px;" />
            <label style="white-space:nowrap;margin-left: 57px;display:none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width:250px;display:none;"></select></label>
            <label style="float:right">Shows <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            <div style="float:right"><ul class="segment"><li data-va='All'>ALL</li><li data-va='0' class="active">Active</li></ul></div>
            </div>
             <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>                           
                        <th style="text-align:left">Sl.No</th>
                        <th  style='display:none;text-align:left'>ID</th>
                        <th style="text-align:left">Name</th>
                        <th style="text-align:left">User Name</th>
                        <th style="text-align:left">Divisions</th>
                        <th style="text-align:left">Status</th>
                        <th style="text-align:left">Edit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row" style="padding:5px 0px">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination" style="float:right;margin:-11px 0px">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>            
        </div>
    </div>
    <div class="modal fade" id="exampleModal" style="z-index: 10000000;overflow-y:auto;background-color: #0000000f;" tabindex="0" aria-hidden="true">
      <div class="modal-dialog" role="document" style="width:60% !important">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Add New HO User</h5><button type="button" class="close" style="margin-top:-20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
          </div>
			<div class="modal-body">
				<div class="container" style="width:100% !important">
					<div class="row">
                        <div class="col-sm-12">
							<div class="row">
								<div class="col-sm-7">
									<input type="hidden" id="hscode" />
									<div class="row">
										<div class="col-sm-4">
											<label style="padding-top: 4px;">Name</label>
										</div>
										<div class="col-sm-8"> <input type="text" id="txtName" class="form-control" autocomplete="off" /></div>
									</div>
									<div class="row" style="margin-top:15px;">
										<div class="col-sm-4">
											<label style="padding-top: 4px;">User ID</label>
										</div>
										<div class="col-sm-8">
											<input type="text" id="txtUSRID" class="form-control" autocomplete="off" /></div>
									</div>
									<div class="row" style="margin-top:15px;">
										<div class="col-sm-4">
											<label style="padding-top: 4px;">Password</label>
										</div>
										<div class="col-sm-8">
										<div class='image'>
                                            <span class='ab'><i class="fa fa-eye" onclick="myFunction()"></i></span>
											<input type="password" id="txtHOPWD" class="form-control" autocomplete="off" /></div>
											</div>
									</div>
									<div class="row" style="margin-top:15px;">
										<div class="col-sm-4">
											<label style="white-space:nowrap;padding-top: 4px;">Confirm Password</label>
										</div>
										<div class="col-sm-8">
											<input type="text" id="txtCnfPWD" class="form-control" autocomplete="off" />
										</div>
									</div>
								</div>
							
								<div class="col-sm-5">
									<div class="row">
										<div class="col-sm-12 divAll">
											<a href="#" class="list-group-item active llsubdiv" style="width: 100%;">Divisions<input title="toggle all" type="checkbox" class="all pull-right" /></a>
											<div class="list-group" id="llsubdiv" style="border: 1px solid #ddd;overflow-y:auto;max-height:200px;">
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		<div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" id="sgrp">Save</button>
          </div>
        </div>
      </div>
    </div>
    <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 98% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="leaveModalLabel"></h5>
                        <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="card">
                        <table id="leavedets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">SlNO</th>
                                            <th style="text-align: left">Product Code</th>
                                            <th style="text-align: left">Product Name</th>
                                            <th style="text-align: left">Product Description</th>
                                            <th style="text-align: left">UOM</th>
                                            <th style="text-align: left">Product Category</th>
                                            <th style="text-align: left">Product Brand</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                         </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
	 <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var filtrkey = '0';
        optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Deactivate</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Name,User_Name,Div_Names,";
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
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
          //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg =(pgNo > 7)? (parseInt(pgNo) + 1) - 7:1;
            if ((Nxtpg) == pgNo){
                 selpg = (parseInt(TotalPg)) - 7;
                 selpg =(selpg>1)? selpg:1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt( $(this).attr("data-dt-idx")); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }
		 function myFunction() {
            var x = document.getElementById("txtHOPWD");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.HO_Active_flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords ; $i++) {
                //var filtered = Orders.filter(function (x) {
                //    return x.Sf_Code != 'admin';
                //})
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + '</td><td style="display:none">' + Orders[$i].HO_ID + '</td><td>' + Orders[$i].Name + '</td><td>' + Orders[$i].User_Name +
                    '</td><td>' + Orders[$i].Div_Names +
                    '</td><td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown"><span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td><td id=' + Orders[$i].HO_ID +
                    ' class="sfedit"><a href="#">Edit</a></td>' +
                    '');

                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
			$(".ddlStatus>li>a").on("click", function () {
				console.log("sai");
				cStus=$(this).closest("td").find(".aState");
				stus=$(this).attr("v");
				$indx=$(this).closest("tr").index();
				cStusNm=$(this).text();
				if(confirm("Do you want change status "+$(cStus).text()+ " to "+cStusNm+" ?")){
					sf=Orders[$indx].DSM_code;
					$.ajax({
						type: "POST",
						contentType: "application/json; charset=utf-8",
						async: false,
						url: "HO_User_Master.aspx/SetNewStatus",
						data: "{'SF':'" + sf + "','stus':'"+stus+"'}",
						dataType: "json",
						success: function (data) {
							Orders[$indx].Product_Grp_Active_Flag = stus;
							Orders[$indx].Status=cStusNm;
							$(cStus).html(cStusNm);
								
							ReloadTable();
							alert('Status Changed Successfully...');

						},
						error: function (result) {
						}
					});
				}
			});
            loadPgNos();
        }
        
		
		$('.divAll .all').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#llsubdiv').find("[type=checkbox]").prop("checked", true);
                }
                else {
                    $('#llsubdiv').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                }
            });
		
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey=$(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
		
		
        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val();
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
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HO_User_Master.aspx/GetHOUsers",
                data: "{'div':'<%=Session["div_code"]%>'}",
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
        function clear(){
		
					hscode.value='';
					$('#txtName').val('');
					$('#txtUSRID').val('');
					$('#txtHOPWD').val('');
			$('#txtCnfPWD').val('');
			$("#llsubdiv input").each(function (idx, item) {
				$(item).prop("checked", false);
			});
            hscode.value = '';
        }
        function savegrp() {
            var hoName=$('#txtName').val();
            var hoName=$('#txtName').val();
            var hoUID=$('#txtUSRID').val();
            var hoPWD=$('#txtHOPWD').val();
            var hoCPWD=$('#txtCnfPWD').val();
			var hoChkDiv = $("#llsubdiv input:checked");
			hoDivs = '';
			hoChkDiv.each(function (idx, item) {
				hoDivs += $(item).attr('name') + ',';
			});
			
            var hoID = hscode.value;
            if (hoName == '' || hoName == null) {
                alert('Enter the Name');
                return false;
            }
            if (hoUID == '' || hoUID == null) {
                alert('Enter the User ID');
                return false;
            }
            if (hoPWD == '' || hoPWD == null) {
                alert('Enter the Password');
                return false;
            }
            if (hoCPWD == '' || hoCPWD == null) {
                alert('Enter the Confirm Password');
                return false;
            }
			
            if (hoPWD != hoCPWD) {
                alert('Confirm Password not matched..');
                return false;
            }
			
            if (hoDivs == '' || hoDivs == null) {
                alert('select the Divisions');
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HO_User_Master.aspx/saveHOUser",
                data: "{'div':'<%=Session["div_code"]%>','hoID': '"+hoID+"','hoName':'" + hoName + "','hoUID':'" + hoUID + "','hoPWD':'" + hoPWD + "','subDivs':'" + hoDivs + "'}",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                    clear(); loadData();
                    $('#exampleModal').modal('hide');
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function editHOUser(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HO_User_Master.aspx/editHOUser",
                data: "{'div':'<%=Session["div_code"]%>','HOID':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var getegr=JSON.parse(data.d) || [];
					hscode.value=x;
					$('#txtName').val(getegr[0].Name);
					$('#txtUSRID').val(getegr[0].User_Name);
					$('#txtHOPWD').val(getegr[0].Password);
					$('#txtCnfPWD').val(getegr[0].Password);
					$("#llsubdiv input").each(function (idx, item) {
						if((","+getegr[0].sub_div_code+",").indexOf(","+$(item).attr('name') + ",")>-1){
						$(item).prop("checked", true);
						}
					});
					
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            loadData();
			$.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HO_User_Master.aspx/getDivision",
                data: "{'div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    res = JSON.parse(data.d) || [];
					
                    if (res.length > 0) {
                        var div = $('#llsubdiv');
                        for (var i = 0; i < res.length; i++) {
                            str = '<a href="#" class="list-group-item">' + res[i].subdivision_name + '<input type="checkbox" name=' + res[i].subdivision_code + ' class="chk pull-right"/></a>';
                            $(div).append(str);
                        }
                    }
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });
            $(document).on('click', '.sfedit', function () {
                $('#exampleModal').modal('toggle');
                var x = this.id;
                hscode.value = this.id;
                editHOUser(x);
            }); 
            $(document).on('click', '#sgrp', function () {
                savegrp();
            });
            $(document).on('click', '#nwgrp', function () {
                clear();
                $('#exampleModal').modal('toggle');
            });
			$(document).on("click", ".deptsurs", function () {
                let prodc = $(this).attr("cd");
                let prodn = $(this).attr("nm");
              
                $('#leaveModal').modal('toggle');
                $('#leavedets TBODY').html("<tr><td colspan='4'>Loading please wait...</td></tr>");
                $("#leaveModalLabel").html(`${prodn} Details`)
               
                    setTimeout(function () {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Product_Group_Master.aspx/getdataliprod",
                            data: "{'div':'<%=Session["div_code"]%>','prodcode':'" + prodc + "'}",
                            dataType: "json",
                            success: function (data) {
                                let AllOrders = JSON.parse(data.d) || [];
                                $('#leavedets TBODY').html("");
                                var slno = 0;
                                for ($i = 0; $i < AllOrders.length; $i++) {
                                    if (AllOrders.length > 0) {
                                        slno += 1;
                                        tr = $("<tr></tr>");
                                        $(tr).html("<td>" + slno + "</td><td>" + AllOrders[$i].Product_Detail_Code + "</td><td>" + AllOrders[$i].Product_Detail_Name + "</td><td>" + AllOrders[$i].Product_Description + "</td><td>" + AllOrders[$i].Product_Sale_Unit + "</td><td>" + AllOrders[$i].Product_Cat_Name + "</td><td>" + AllOrders[$i].Product_Brd_Name + "</td>");
                                        $("#leavedets TBODY").append(tr);
                                    }
                                }
                            },
                            error: function (resp) {
                                $('#leavedets TBODY').html("<tr><td colspan='4'>Something went wrong. Try again.</td></tr>");
                            }
                        });
                    }, 500);
              
            })
        });

    </script>
</asp:Content>