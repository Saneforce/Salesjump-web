<%@ Page Title="Expense Approval" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Expense_Approval_new.aspx.cs" Inherits="MasterFiles_Expense_Approval" %>

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
    <script type="text/javascript">
        var dt = new Date();
         var pgYR = '<%=Session["div_code"]%>' == '100' ? 2022 : dt.getFullYear();
        var pgMNTH = '<%=Session["div_code"]%>' == '100' ? 11 : dt.getMonth() + 1;
        var sfexp = [];
        var Orders = [];
        var filsfexp = [];
        var AllOrders = []; 
        searchKeys = "exstatus";						
        function getApprovalDets(Yr, Mnth) {
            pgYR = Yr; pgMNTH = Mnth;
            var period = '<td>' + ($('#dll_month :selected').attr('label')) + ',' + ($('#ddl_year :selected').text()) + '</td>';
			if(Mnth=="0"){
				alert("Please Select Month");
			}
			else{
				$.ajax({
					type: "POST",
					contentType: "application/json; charset=utf-8",
					async: false,
					url: "Expense_Approval_new.aspx/Get_FieldForce",
					data: "{'exp_years':'" + Yr + "','exp_month':'" + Mnth + "'}",
					dataType: "json",
					success: function (data) {
						sfexp = data.d;
						Orders = sfexp;
						ReloadTable();		
					},
					error: function (result) {
						alert(JSON.stringify(result));
					}
				});
			}
        }
        function ReloadTable() {
            $('#FieldForce tbody tr').remove();
            var period = '<td>' + ($('#dll_month :selected').attr('label')) + ',' + ($('#ddl_year :selected').text()) + '</td>';
            if (Orders.length > 0) {
                for (var i = 0; i < Orders.length; i++) {
                    var sty = '';
                    sty = '<td>' + Orders[i].exstatus + '</td>';
                    var strFF = "<td class='col-xs-2'><input type='hidden' name='sf_Code' value='" + Orders[i].sf_code + "'/>" + Orders[i].sf_name + "</td>" + period + sty + "<td><a style='display:none' class='btn btn-info' href='Expense_Entry.aspx?Mode=1&SF_Code=" + Orders[i].sf_code + "&SF_EmpID=" + Orders[i].EmpID + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "'>Edit</a>";
                    strFF += " <a class='btn btn-info AAA' href='rpt_Expense_Entry_new.aspx?SF_Code=" + Orders[i].sf_code + "&SF_name=" + Orders[i].sf_name + "&SF_EmpID=" + Orders[i].EmpID + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "'>View</a>";
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
                url: "Expense_Approval_new.aspx/Get_Year",
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
                url: "Expense_Approval_new.aspx/FillMRManagers",
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
            $("#ddl_year").val(pgYR);
            $("#dll_month").val(pgMNTH);
            getApprovalDets(pgYR, pgMNTH);
            $(document).on('click', '#btnGo', function () {
                var selyear = $("#ddl_year").val();
                if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                var selmonth = $("#dll_month").val();
                if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }
                getApprovalDets(selyear, selmonth);
            });


            $(document).on('click', '.AAA', function (event) {
                event.preventDefault();
                event.stopPropagation();
                var url = $(this).attr('href');
                window.open(url, 'poprpExpense1', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

            });
            $("#ddlmgrs").on('change', function () {
                if ($("#ddlmgrs").val()!='admin')
                    Orders = sfexp.filter(function (a) {
                        return (a.Reporting_To_SF == $("#ddlmgrs").val()) || (a.sf_code == $("#ddlmgrs").val());
                    });
                else
                    Orders = sfexp
                ReloadTable();
            });
            $("#ddlsts").on('change', function () {
                if ($(this).val() != "" && $(this).val() != "0") {
                    shText = $(this).val().toLowerCase();
                    Orders = sfexp.filter(function (a) {
                        return (a.exstatus.toLowerCase().indexOf(shText) > -1)
                    });
				}
                else
                     Orders = sfexp
                ReloadTable();
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
							<option value="Approval">Need Approval</option>
                            <option value="Not submitted">Not submitted</option>
                            <option  value="Pending">Pending </option>
                        </select>
                        
                    </div>
                </div>
            </div>		  
        <div class="container" style="max-width: 90%; width: 90%">
            <div class="row">
                <div class="col-sm-12 inputGroupContainer">
                    <table id="FieldForce" class="gvHeader">
                        <thead>
                            <tr>
                                <th style="min-width: 100px; width: 50%">Name
                                </th>
                                <th style="min-width: 100px; width: 20%">Period
                                </th>
                                <th style="min-width: 100px; width: 20%">Status
                                </th>
                                <th style="width: 30%; min-width: 100px;"></th>
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
