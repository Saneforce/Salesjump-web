<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="InshopActivity_Orders_Report.aspx.cs" Inherits="MIS_Reports_InshopActivity_Orders_Report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

        [data-val='Rejected']{
            color:red;
        }
        [data-val='Approved']{
            color:green;
        }
        [data-val='Pending'] {
            color: blue;
        }

        th
        {
            white-space:nowrap;  
            cursor:pointer; 
        }
        #txtfilter_chzn{
            width:300px !important;
            top:10px;
        }
    </style>
    <form runat="server" id="frm1">
        <asp:HiddenField ID="hfilter" runat="server" />
        <asp:HiddenField ID="hffilter" runat="server" />
        <asp:HiddenField ID="hsfhq" runat="server" />
    <div class="row">
        <div class="row">
           <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;"  />
        </div>
        <div class="col-lg-12 sub-header">Inshop Activity Orders
    </div>

    <div class="card" style="padding:0px 16px">
        <div class="card-body table-responsive">
            </div>
             <table class="table table-hover" id="OrderList" style="font-size:12px">
                <thead class="text-warning">
                    <tr>                          
                        <th style="text-align:left">Sl.No</th>
                        <th id="SF_Name" style="text-align:left">Retailer Name</th>
                        <th id="WrkDate" style="text-align:left">Date</th>
                        <th id="DistName" style="text-align:left;">Product</th>
                        <th id="Rmks" style="text-align:left">Qty</th>
                        <th id="Value" style="text-align:left">Amount</th>
						
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
        var sftyp='<%=Session["sf_type"]%>';
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
        var asc=true;//
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Name,WrkDate,DistName,Rut_Name,Ret_Name,Promotor,Start_Time,End_Time,Value,Rmks";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
       $('th').on('click', function () {
            sortid = this.id;
            asc = $(this).attr('asc');
            if(asc==undefined) asc='true';
            Orders.sort(function (a, b) {
                if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true') return -1;
                if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true') return 1;
                
                if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false') return -1;
                if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false') return 1;
                return 0;
            });
            
            $(this).attr('asc',((asc=='true')?'false':'true'));
            ReloadTable();
        });
         function loadPgNos() {
			for (il = 0; il < Orders.length; il++)
			{
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
        
        $('#eedit').on('click', function () {
            $('#shftname').hide();
            $('#sftname').show();
            $('#btnupdate').show();
        });
        $('#btnupdate').on('click', function () {
            var selsft = $('#sftname').val();
            var ssfttext = $('#sftname :selected').text();
            var presft = $('#ShiftSelected_Id').val();
            var logdt = $('#tdt').html();
            logdt = logdt.split('/');
            ldt = logdt[2] + '-' + logdt[1] + '-' + logdt[0];
            var hsf = $('#hidsf').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Extended_Shift_Report.aspx/shiftUpdate",
                data: "{'SF':'" + hsf + "','logdt':'" + ldt + "','nsftid':'" + selsft + "','psftid':'" + presft + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d = 'Success') {
                        alert('Shift Changed Successfully');
                        $('#shftname').show();
                        $('#sftname').hide();
                        $('#btnupdate').hide();
                        $('#shftname').html(':' + ssfttext);
                        $('#sftname').val(selsft);
                    }
                    else {
                        alert('Shift Change Unsuccessful');
                    }
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        })

        $(".segment1>li").on("click", function () {
            $(".segment1>li").removeClass('active');
            $(this).addClass('active');
            fffilterkey = $(this).attr('data-va');
            $("#<%=hffilter.ClientID%>").val(fffilterkey);
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            $("#<%=hfilter.ClientID%>").val(filtrkey);
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });

        
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
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Retailer_Name + "</td><td>" + Orders[$i].Dt + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + Orders[$i].Qty + "</td><td>" + Orders[$i].Amt + "</td>");
                    $("#OrderList TBODY").append(tr);
                }
            }
            //$("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            //loadPgNos();
        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                       // if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                        if (val != null && val.toString().toLowerCase().substring(0, shText.length) == shText && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
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
            dt=new Date();
            $m=dt.getMonth()+1,$y=dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "InshopActivity_Orders_Report.aspx/GetDetails",
                data: "{'SF':'<%=sfo%>','Ret':'<%=RetNm%>','dt':'<%=Dt%>'}",
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
	  
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
			$(".header").hide();
			$(".left-side").hide();
			$(".right-side").css("margin-left","0px");
			
            loadData();
			setTimeout(function(){ 
				$("#fc_frame").hide();
			}
			,500);
			
        });
       
    </script>
    <script type="text/javascript">
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
    </script>
</asp:Content>



