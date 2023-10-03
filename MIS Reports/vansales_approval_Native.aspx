<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="vansales_approval_Native.aspx.cs" Inherits="MIS_Reports_vansales_approval_Native" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
             <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
        }

        th {
           
            top: 0;
            background: #6c7ae0;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        .a {
            line-height: 22px;
            padding: 3px 4px;
            border-radius: 7px;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }
    </style>
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
	</head>

	<body>
    <form runat="server" style="height:550px;">
        <div class="row">
            <div class="col-lg-12 sub-header">
                Begin Inventory Approval
            </div>
        </div>
		
		<div class="row hid" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Manager</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" data-size="8"  id="ddlmgr"></select>
                </div>
            </div>
        </div>
      
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">FieldForce<span style="color:red;font:bold;">  *</span></label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" data-size="8"  id="ddlff"></select>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="fdate" />
                </div>
            </div>
        </div>
       
        <div class="row" style="margin-top: 5px">
            <div class="col-md-2  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color:#1a73e8;color:white;">View</button>
            </div>
          </div><br /><br />
        <div class="card" id="tblcrd">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                    <div id="d1"></div>
         <table class="auto-index" id="grid" style='display: none;margin: auto; '>
                <thead>
                   </thead>
             <tbody></tbody>
             </table></div></div></div>
          <a id='btnApp' class='btn btn-primary'>Approval</a> <a id='btnRej' class='btn btn-danger'>Reject</a>
    </form>
    <script type="text/javascript">
	
        var AllDiv = [], AllState = [], AllFF = [];
        var Div = [], States = [], SFF = []; var cspc = []; var clos = []; var bnd = '';var tblslno='';
		 var sysdatetime = "";var retmsg="";
		 
        $(document).ready(function () {
            document.getElementById('fdate').valueAsDate = new Date();
            loadDivision();
            //loadFieldForce();
			if ('<%=Session["sf_type"]%>'== '2') {
                loadFieldForce('<%=Session["Sf_Code"]%>');
                $(".hid").hide();
            }
            else {
                loadMGR();
                
                $("#ddlff").empty().append('<option selected="selected" value="">Select FieldForce</option>');
                $('#ddlff').selectpicker({
                    liveSearch: true
                });
				loadFieldForce('admin');
            }
            $('#ddldiv').on('change', function () {
                FFilter();
            });
			$("#btnApp").hide(); $("#btnRej").hide();
            $('#tblcrd').hide();
        
        });
        $('#btnview').on('click', function () {
            var sfcode = $('#ddlff').val();
            var sfname = $('#ddlff :selected').text();
				
			

            if (sfcode == '') {
                alert('Select FieldForce');
                return false;
            }
            var fdate = $('#fdate').val();
            if (fdate == '') {
                alert('Select the From Date');
                return false;
            }

            $('#tblcrd').show();
            $("#grid").show();
            $('#grid tbody').html(''); 
            $('#grid thead').html(''); 
			
            Fillprod();
         });
		 $("#ddlmgr").change(function () {
            var mgr = $('#ddlmgr').val();
			if(mgr=='')
			loadFieldForce('admin');
			else
            loadFieldForce(mgr);
        });
        function Fillprod() {
            var sfcode = $('#ddlff').val();
            var fdate = $('#fdate').val();

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "vansales_approval_Native.aspx/getvanprod",
                data: "{'divcode':'<%=Session["div_code"]%>','ffc':'" + sfcode + "','fdt':'" + fdate + "','tdt':'" + fdate + "'}",
                    dataType: "json",
                    success: function (data) {
                        clos = JSON.parse(data.d);
                        Fillcasepc();
                    }
                });
        }
        function Fillcasepc() {
            var sfcode = $('#ddlff').val();
            var fdate = $('#fdate').val();

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "vansales_approval_Native.aspx/getcasepc",
                data: "{'ffc':'" + sfcode + "','fdt':'" + fdate + "'}",
                dataType: "json",
                success: function (data) {
                    cspc = JSON.parse(data.d);
                     if (cspc[0].Data != "Already Approved" && cspc[0].Data != "No Records Found...") {
					 $('#d1').empty();
                        Reloadtable();
                    }
                    else {
                        
						if(cspc[0].Data == "No Records Found...")
						{
						$('#d1').empty().append("No Data Found...");
						$("#btnApp").hide(); $("#btnRej").hide();
						}
						else
						alert(cspc[0].Data);
                    }
                }
            });
        } 
         
        function Reloadtable() {
           
                str = ''; str1 = '';
				var slno=0;
                for (var i = 0; i < clos.length; i++) {
                    var flit = cspc.filter(function (a) {
                        return a.Prod_Code == clos[i].Product_Detail_Code;
                    });
					if(flit.length>0){
                    str1 = "<tr><th>S.No</th><th>Product</th><th>Case</th><th>Piece</th></tr>";
                    str += "<tr id='" + clos[i].Product_Detail_Code + "'><td>" + (++slno) + "</td><td><input type='hidden' name='pCode' value='" + clos[i].Product_Detail_Code + "'/><span>" + clos[i].Product_Detail_Name + "</span></td>";
                    str += "<td>" + (flit.length > 0 ? flit[0].CaseQty : 0) + "</td><td>" + (flit.length > 0 ? flit[0].PiceQty : 0) + "</td></tr>";
					tblslno += flit[0].SLNo+',';
					}
                }
                $('#grid thead').empty().append(str1);
                $('#grid tbody').empty().append(str);
				$("#btnApp").show(); $("#btnRej").show();
            
        }
    
        function FFilter() {
           
            var cdiv = $("#ddldiv").val() || 0;
            $('#ddlff').selectpicker('destroy');
            var filsf = [];
         
            var dept = $("#ddlff");
            dept.empty().append('<option selected="selected" value="">Select FieldForce</option>');
            if ("<%=sf_type%>" != "2") {
                dept.append('<option value="admin">Admin</option>');
            }
            if (filsf.length > 0) {
                for (var i = 0; i < filsf.length; i++) {
                    dept.append($('<option value="' + filsf[i].Sf_Code + '">' + filsf[i].Sf_Name + '</option>'))
                }
            }
            $('#ddlff').selectpicker({
                liveSearch: true
            });
        }
      
        function loadDivision(ssdiv) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "vansales_approval_Native.aspx/getDivision",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    AllDiv = JSON.parse(data.d) || [];
                    Div = AllDiv;
                    if (Div.length > 0) {
                        var dept = $("#ddldiv");
                        dept.empty().append('<option selected="selected" value="">Select Division</option>');
                        for (var i = 0; i < Div.length; i++) {
                            dept.append($('<option value="' + Div[i].subdivision_code + '">' + Div[i].subdivision_name + '</option>'))
                        }
                    }
                }
            });
            $('#ddldiv').selectpicker({
                liveSearch: true
            });
        }
        function chkinp() {
            var valid = true;
            var sfcode = $('#ddlff').val();
            var sfname = $('#ddlff :selected').text();
            var subdiv = $('#ddldiv').val() || 0;
            if (sfcode == '') {
                alert('Select FieldForce');
                valid = false;
                return false;
            }
            var fdate = $('#fdate').val();
            if (fdate == '') {
                alert('Select the From Date');
                valid = false;
                return false;
            }
            var tdate = $('#tdate').val();
            if (tdate == '') {
                alert('Select the To Date');
                valid = false;
                return false;
            }
           
            return valid;
        }
		function loadMGR() {
            var subdiv ='<%=Session["sub_division"]%>';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "vansales_approval_Native.aspx/SalesForceListMgrGet_MgrOnly",
                data: '{}',
                dataType: "json",
                success: function (data) {
                    AllMGR = JSON.parse(data.d) || [];
                    MGR = AllMGR;
                    if (MGR.length > 0) {
                        var dept = $("#ddlmgr");
                        dept.empty().append('<option selected="selected" value="">Select Manager</option>');
                        for (var i = 0; i < MGR.length; i++) {
                            dept.append($('<option value="' + MGR[i].Sf_Code + '">' + MGR[i].Sf_Name + '</option>'))
                        }
                    }
                }
            });
            $('#ddlmgr').selectpicker({
                liveSearch: true
            });
        }
        function loadFieldForce(mgr) {
		var subdiv='<%=Session["sub_division"]%>';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "vansales_approval_Native.aspx/getFieldForce",
                data: '{"divcode":"<%=Session["div_code"]%>","sfcode":"'+mgr+'"}',
                dataType: "json",
                success: function (data) {
                    AllFF = JSON.parse(data.d) || [];
                    SFF = AllFF;
					$("#ddlff").selectpicker("destroy");
                    var dept = $("#ddlff");
                    dept.empty().append('<option selected="selected" value="">Select FieldForce</option>');
                    if (SFF.length > 0) {
                        for (var i = 0; i < SFF.length; i++) {
                            dept.append($('<option value="' + SFF[i].Sf_Code + '">' + SFF[i].Sf_Name + '</option>'))
                        }
                    }
                }
            });
            $('#ddlff').selectpicker({
                liveSearch: true
            });
        }
		function generateDatabaseDateTime(date) {
            const p = new Intl.DateTimeFormat('en', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false
            }).formatToParts(date).reduce((acc, part) => {
                acc[part.type] = part.value;
                return acc;
            }, {});

            return `${p.year}-${p.month}-${p.day} ${p.hour}:${p.minute}:${p.second}`;
        }
		function check_alert(sfcode, fdate) {
		
            $.ajax({
                contentType: "application/json; charset=utf-8",
                async: true,
                url: 'vansales_approval_Native.aspx/chkAlready_apprej',
                type: "POST",
                data: "{'SF_Code':'" + sfcode + "', 'dates':'" + fdate + "','tblslno':'" + tblslno + "'}",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
					retmsg=data.d;
					//return retmsg;
                },
                error: function (erorr) {
                    console.log(erorr);
                }
            });
			if(retmsg=='Data')
            return retmsg;
        }
        $(document).on('click', '[id*=btnApp]', function () {
		var sfcode = $('#ddlff').val();
		var Apprby = '<%= Session["sf_code"] %>';//$('#ddlmgr').val();
		var fdate = $('#fdate').val();
			$.ajax({
                contentType: "application/json; charset=utf-8",
                async: true,
                url: 'vansales_approval_Native.aspx/chkAlready_apprej',
                type: "POST",
                data: "{'SF_Code':'" + sfcode + "', 'dates':'" + fdate + "','tblslno':'" + tblslno + "'}",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
					retmsg=data.d;
					if (retmsg == 'Data') {
					if (confirm('Are you sure you want to Approval ?')) {
            
			sysdatetime = generateDatabaseDateTime(new Date);

                console.log(sysdatetime);
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'vansales_approval_Native.aspx/Approvaldata',
                    type: "POST",
                    data: "{'SF_Code':'" + sfcode + "', 'dates':'" + fdate + "','Apprby':'" + Apprby + "','tblslno':'" + tblslno+"','systime':'" + sysdatetime+"'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        if (data.d == "Sucess" ){
                            alert("Approved Sucessfully");
                        }
                        location.reload();
                    },
                    error: function (erorr) {
                        console.log(erorr);
                    }
                });
            }
			}
						else{
			alert(retmsg);
			location.reload();}
                },
                error: function (erorr) {
                    console.log(erorr);
                }
            });
            
        });
        $(document).on('click', '[id*=btnRej]', function () {
          var sfcode = $('#ddlff').val();
                var fdate = $('#fdate').val();
				$.ajax({
                contentType: "application/json; charset=utf-8",
                async: true,
                url: 'vansales_approval_Native.aspx/chkAlready_apprej',
                type: "POST",
                data: "{'SF_Code':'" + sfcode + "', 'dates':'" + fdate + "','tblslno':'" + tblslno + "'}",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
					retmsg=data.d;
            if (retmsg == 'Data') {
            if (confirm('Are you sure you want to Reject ?')) {
			var reasonTxt = prompt('Enter Reject Reason here');
			sysdatetime = generateDatabaseDateTime(new Date);
                console.log(sysdatetime);
				if (reasonTxt != null && reasonTxt != "") {
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'vansales_approval_Native.aspx/RejectData',
                    type: "POST",
                    data: "{'SF_Code':'" + sfcode + "', 'dates':'" + fdate + "','Rejby':'<%= Session["sf_code"] %>','reason':'" + reasonTxt + "','tblslno':'" + tblslno+"','systime':'" + sysdatetime+"'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        if (data.d == "Sucess" ){
                                alert("Rejected Sucessfully");
                            }
                        location.reload();
                    },
                    error: function (erorr) {
                        console.log(erorr);
                       
                    }
                });
				}
				else
                    alert("You Cancelled Rejection Reason...");
            }
			
			}
			else{
			alert(retmsg);
			location.reload();}
                },
                error: function (erorr) {
                    console.log(erorr);
                }
            });
               
       });
    </script>
	</body>
	</html>
</asp:Content>

