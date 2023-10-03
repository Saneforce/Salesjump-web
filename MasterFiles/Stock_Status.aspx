<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Stock_Status.aspx.cs" Inherits="MasterFiles_Stock_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }

		#ProductTable td:nth-child(8),#ProductTable td:nth-child(13),#ProductTable td:nth-child(14)
		{	
			    font-weight: bold;
		}

    </style>
     <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({ dateFormat: 'dd/mm/yy' });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Stock_Status.aspx/GetDistributor",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddldistributor");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['disCode']).html(this['disName']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


            $(document).on('click', '#btndaysave', function () {
                $('#ProductTable >tbody >tr').remove();
                var ddlcust = $('#ddldistributor').val();
                if (ddlcust == 0) { alert('Select Location..!'); $('#ddldistributor').focus(); return false; };
                var fromdate = $('#fromDate').val();
                if (fromdate.length <= 0) { alert('Enter From Date..!'); $('#fromDate').focus(); return false; };
                var todate = $('#toDate').val();
                if (todate.length <= 0) { alert('Enter From Date..!'); $('#toDate').focus(); return false; };

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Stock_Status.aspx/GetProduct",
                    data: "{'Cust_Code':'" + ddlcust + "','FromDate':'" + fromdate + "','ToDate':'" + todate + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        var str = "";
                        for (var i = 0; i < data.d.length; i++) {
                            str = "<td>" + (i + 1) + "</td><td><input type='hidden' name='pcode'value='" + data.d[i].pCode + "'/>" + data.d[i].pName + "</td>";
                            str += "<td>" + (data.d[i].opning || '') + "</td><td>" + (data.d[i].ppurch || '') + "</td><td>" + (data.d[i].preturn || '') + "</td><td>" + (data.d[i].trsIn || '') + "</td><td>" + (data.d[i].adjIn || '') + "</td><td>" + (data.d[i].totadd || '') + "</td><td>" + (data.d[i].psal || '') + "</td><td>" + (data.d[i].piss || '') + "</td><td>" + (data.d[i].trsOut || '') + "</td><td>" + (data.d[i].adjOut || '') + "</td><td>" + (data.d[i].totdet || '') + "</td><td>" + (data.d[i].closing || '') + "</td>";
                            $('#ProductTable >tbody').append('<tr>' + str + ' </tr>');
                        }
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });
            });

        });
    </script>
    <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <div class="form-group">
                <label for="location" class="control-label col-sm-2 col-sm-offset-3" style="font-weight: normal">
                    Location
                </label>
                <div class="col-sm-3">
                    <select id="ddldistributor" name="ddldistributor" class="form-control">
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
        <label for="fromDate" class="col-md-2 col-sm-offset-3 control-label" style="font-weight: normal">
                From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <input type="text" name="fromDate" id="fromDate" class="form-control datePicker" />
            </div>
            </div>
            <div class="row">
            <label for="toDate" class="col-md-2 col-sm-offset-3 control-label" style="font-weight: normal">
                To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <input type="text" name="toDate" id="toDate" class="form-control datePicker" />
            </div>
            </div>
        
        <div class="row" style="text-align: center">
            <div class="col-md-12 inputGroupContainer">
                <a id="btndaysave" class="btn btn-primary btnsave" style="vertical-align: middle;
                    font-size: 17px;"><span>View</span></a></div>
        </div>
        </div>
        <br />
        <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <div class="col-sm-12">
                <table id="ProductTable" class="newStly table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                SlNo.
                            </th>
                            <th>
                                Product Name
                            </th>
                            <th>
                                OP
                            </th>
                            <th>
                                Purchase
                            </th>
                            <th>
                                Return
                            </th>

							<th>
                                Trans In
                            </th>
                            <th>
                                Adj In
                            </th>                          
		
                            <th>
                                Tot Add
                            </th>
                            <th>
                                Sales
                            </th>
                            <th>
                                Issue
                            </th>
							<th>
                                Trans Out
                            </th>
                            <th>
                                Adj Out
                            </th>
                            <th>
                                Tot Det
                            </th>
                            <th>
                                Cl.Stock
                            </th>
                           
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
