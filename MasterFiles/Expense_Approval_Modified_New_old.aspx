<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Expense_Approval_Modified_New.aspx.cs" Inherits="MasterFiles_Expense_Approval_Modified_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #FieldForce {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #FieldForce td {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #FieldForce tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #FieldForce tr:hover {
                background-color: #ddd;
            }

            #FieldForce th {
                padding-top: 12px;
                padding-bottom: 12px;
                padding-left: 5px;
                padding-right: 5px;
                text-align: center;
                background-color: #496a9a;
                color: white;
            }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	 <a href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.js.map"></a>
        <link href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.css" rel="stylesheet" />
        <script src="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.js"></script>
    <script type="text/javascript">
        var dt = new Date();
        var pgYR = dt.getFullYear();
        var pgMNTH = dt.getMonth()+1;
        var sfexp = [];
        var Orders = [];
        var filsfexp = [];
        var AllOrders = []; 
		var period = '';
	var FilterOrders=[];var loc=[];
        searchKeys = "exstatus";						
        function getApprovalDets(Yr, Mnth) {
            pgYR = Yr; pgMNTH = Mnth;	    
            if (Mnth == "0") {
                alert("Please Select Month");
            }
            else {
                Get_FieldForce('',Yr,Mnth);
            }
        }
        function Get_FieldForce(sfcode,Yr,Mnth){
            $.ajax({
					type: "POST",
					contentType: "application/json; charset=utf-8",
					async: false,
					url: "Expense_Approval_Modified_New.aspx/Get_FieldForce",
					data: "{'exp_years':'" + Yr + "','exp_month':'" + Mnth + "','sfcode':'"+sfcode+"'}",
					dataType: "json",
					success: function (data) {
						period = '<td>' + ($('#dll_month :selected').attr('label')) + ',' + ($('#ddl_year :selected').text()) + '</td>';
						sfexp = JSON.parse(data.d) || [];
						Orders = sfexp;
						ReloadTable(Orders);		
					},
					error: function (result) {
						alert(JSON.stringify(result));
					}
				});

        }
        function ReloadTable(Orders) {
            $('#FieldForce tbody tr').remove();
            if (Orders.length > 0) {
                for (var i = 0; i < Orders.length; i++) {
                    var sty = '';
                    sty = '<td>' + Orders[i].exstatus + '</td>';
                    var strFF = "<td ><input type='hidden' name='sf_Code' value='" + Orders[i].sf_code + "'/>" + Orders[i].sfname + "</td>" + period + sty + "<td><a style='display:none' class='btn btn-info' href='Expense_Entry.aspx?Mode=1&SF_Code=" + Orders[i].sf_code + "&SF_EmpID=" + Orders[i].EmpID + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "'>Edit</a>";
                    strFF += " <a class='btn btn-info AAA' href='rpt_Expense_Entry_Modified.aspx?SF_Code=" + Orders[i].sf_code + "&SF_name=" + Orders[i].sfname + "&SF_EmpID=" + Orders[i].EmpID + "&SF_DesgID=" + Orders[i].Designation_Code + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "'>View</a>";
                    strFF += "</td>";
                    $("#FieldForce tbody").append("<tr class='gvRow'>" + strFF + "</tr>");
                }
            }
            else {
                $("#FieldForce tbody").append("<tr class='gvRow'><td colspan='3'>No Record Found.!.</td></tr>");
            }
        }
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New.aspx/Get_Year",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddl_year");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['years']).html(this['years']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New.aspx/FillMRManagers",
                dataType: "json",
                success: function (data) {
                    var sfmgr = JSON.parse(data.d);
                    var ddlCustomers = $("#ddlmgrs");
                    ddlCustomers.empty().append('<option value="0">Select Manager</option>')
                    for (var i = 0; i < sfmgr.length; i++) {
                        ddlCustomers.append('<option value="' + sfmgr[i].Sf_Code + '">' + sfmgr[i].Sf_Name + '</option>')
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            //$("#ddl_year").val(pgYR);
            //$("#dll_month").val(pgMNTH);
            //getApprovalDets(pgYR, pgMNTH);
	if (localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, ''))) {
                loc = JSON.parse(localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, '')));
                if (loc.length > 0) {
                    $("#ddl_year").val(loc[0].year);
                    $("#dll_month").val(loc[0].month);
                    $('#ddlmgrs > option[value="' + loc[0].sfcode + '"]').prop('selected', true);
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Expense_Approval_Modified_New.aspx/Get_FieldForce",
                        data: "{'exp_years':'" + loc[0].year + "','exp_month':'" + loc[0].month + "','sfcode':''}",
                        dataType: "json",
                        success: function (data) {
                            period = '<td>' + ($('#dll_month :selected').attr('label')) + ',' + ($('#ddl_year :selected').text()) + '</td>';
                            sfexp = JSON.parse(data.d) || []; 
                            if (sfexp.length=='1') {
                                $('#ddlmgrs > option[value="' + sfexp[0].Reporting_To_SF + '"]').prop('selected', true);
                            }
							pgMNTH=loc[0].month;
							pgYR=loc[0].year;
                            Orders = sfexp;
                            ReloadTable(Orders);
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
            }
            else {
                $("#ddl_year").val(pgYR);
                $("#dll_month").val(pgMNTH);
                $('#ddlmgrs > option[value="0"]').prop('selected', true);
                getApprovalDets(pgYR,pgMNTH);
            }
            $(document).on('click', '#btnGo', function () {
                $('#ddlmgrs').val('0');
                $('#ddlsts').val('0');
                var selyear = $("#ddl_year").val();
                if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                var selmonth = $("#dll_month").val();
                if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }
                if (selyear <= 2022 && selmonth <= 11 && '<%=Session["div_code"]%>' == '100') {
                    toastr.error('Expense Can\'t be process','Alert!!!');
                }
				else if (selyear <= 2023 && ((selyear <= 2022 ) ? selmonth = selmonth : selmonth <= 4) && '<%=Session["div_code"]%>' == '109') {
					location.href = "Expense_Approval.aspx";
                    //toastr.error('Expense Can\'t be process','Alert!!!');
                }
                else {
                    getApprovalDets(selyear, selmonth);
                }
            });


            $(document).on('click', '.AAA', function (event) {
                event.preventDefault();
                event.stopPropagation();
                var url = $(this).attr('href');
                window.open(url, 'poprpExpense1', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

            });
            $("#ddlmgrs").on('change', function () {
                $('#FieldForce tbody tr').remove();
                if ($("#ddlmgrs").val()!='admin') {
                    //Get_FieldForce($("#ddlmgrs").val(), pgYR, pgMNTH);
                    FilterOrders= sfexp.filter(function (a) {
                        return (a.Reporting_To_SF == $("#ddlmgrs").val() || a.sf_code == $("#ddlmgrs").val() || a.Appr_By == $("#ddlmgrs").val());
                    });
					ReloadTable(FilterOrders);
                }
				else if($("#ddlmgrs").val()!='') {
					Orders = sfexp;
					ReloadTable(Orders);
				}
                else {
                    $("#FieldForce tbody").append("<tr class='gvRow'><td colspan='3'>No Record Found.!.</td></tr>");
                }
		$('#ddlsts').val('0');
            });
            $("#ddlsts").on('change', function () {
                if ($("#ddlmgrs").val() != '0' && $("#ddlmgrs").val()!='admin') { 
                    shText = $(this).val().toLowerCase();
                    FiltrOrd = FilterOrders.filter(function (a) {
                        return (a.exstatus.toLowerCase().indexOf(shText) > -1)
                    });
                    ReloadTable(FiltrOrd);
                }		
                else if ($(this).val() != "" && $(this).val() != "0") {
                    shText = $(this).val().toLowerCase();
                    Orders = sfexp.filter(function (a) {
                        return (a.exstatus.toLowerCase().indexOf(shText) > -1)
                    });
                    ReloadTable(Orders);
                }
                else {
                    Orders = sfexp
                    ReloadTable(Orders);
                }

            });
        });
    </script>
    <form id="Allowancefrm" runat="server">
        <div class="container" style="max-width: 100%; width: 55%">
            <div class="row">
                <asp:Label ID="lblFMonth" runat="server" Style="text-align: left; padding: 8px 4px;"
                    SkinID="lblMand" Text="Year" CssClass="col-md-1"></asp:Label>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <select id="ddl_year" name="txt_year" class="form-control">
                            <option>--- Select ---</option>
                        </select>
                    </div>
                </div>
                <asp:Label ID="Label3" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                    Text="Month" CssClass="col-md-1"></asp:Label>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <select id="dll_month" name="dll_month" class="form-control">
                            <option value="0" label="--- Select---"></option>
                            <option value="1" label="JAN"></option>
                            <option value="2" label="FEB"></option>
                            <option value="3" label="MAR"></option>
                            <option value="4" label="APR"></option>
                            <option value="5" label="MAY"></option>
                            <option value="6" label="JUN"></option>
                            <option value="7" label="JUL"></option>
                            <option value="8" label="AUG"></option>
                            <option value="9" label="SEP"></option>
                            <option value="10" label="OCT"></option>
                            <option value="11" label="NOV"></option>
                            <option value="12" label="DEC"></option>
                        </select>
                    </div>
                </div>
                    <a id="btnGo" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                        <span>Go</span></a>
            </div>
            <br />
            <div class="row" style="text-align: center">
                <div class="col-sm-10 inputGroupContainer">
                </div>
            </div>
        </div>
        <br />        
            <div class="row col-md-10 col-md-offset-1" style="margin-top:8px;">
                <asp:Label ID="Label2" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                    Text="Filter By Manager" CssClass="col-md-2"></asp:Label>
                <div class="col-md-10">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <select class="form-control" id="ddlmgrs"></select>
                    </div>
                </div>
            </div>
        <br />
		 <div class="row col-md-10 col-md-offset-1" style="margin-top:8px;">
                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                    Text="Filter By status" CssClass="col-md-2"></asp:Label>
                <div class="col-md-10">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <select class="form-control" id="ddlsts">
                            <option value="0">Select Status</option>
                            <option value="">All</option>
                            <option value="Approved">Approved</option>
							<option value="Need approval">Need approval</option>
                            <option value="Expense not submitted">Not submitted</option>
                            <option value="Not submit for approval">Not submit for approval</option>
                            <option  value="Pending">Pending</option>
                            <option  value="Rejected">Rejected</option>
                        </select>
                        
                    </div>
                </div>
            </div>		  
        <div class="container" style="max-width: 90%; width: 90%">
            <div class="row">
                <div class="col-sm-12 inputGroupContainer">
                    <table id="FieldForce" class="gvHeader card" style="display:table;box-shadow: 0px 5px 20px 4px grey;">
                        <thead>
                            <tr>
                                <th class="col-xs-8">Name</th>
                                <th >Period</th>
                                <th >Status</th>
                                <th ><img style="width:30px;" src='data:image/png;base64,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'/></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</asp:Content>