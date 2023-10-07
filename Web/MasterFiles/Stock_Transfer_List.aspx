<%@ Page Title="Stock Transfer List" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Stock_Transfer_List.aspx.cs" Inherits="MasterFiles_Stock_Transfer_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #StockTransferList
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #StockTransferList td
        {
            border: 1px solid #ddd;
            padding: 8px;
        }
        
        #StockTransferList tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #StockTransferList tr:hover
        {
            background-color: #ddd;
        }
        
        #StockTransferList th
        {
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
        $(document).ready(function () {
            var today = new Date();
            var yyyy = today.getFullYear();
            var ddlCustomers = $("#ddl_year");
            ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
            for (var i = yyyy - 3; i <= yyyy + 1; i++) {
                ddlCustomers.append($("<option></option>").val(i).html(i));
            }
            $("#ddl_year").val(yyyy);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Stock_Transfer_List.aspx/GetDistributor",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    var ddlCustomers = $("#ddlFilter");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        if (this['wType'] == 'Warehouse') {
                            ddlCustomers.append($("<option></option>").val(this['disCode']).html(this['disName']));
                        }
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $("#StockTransferList tbody").append("<tr class='gvRow'><td colspan='7'> No Records Found..!</td></tr>");

            $(document).on('click', '#btnGo', function () {

                var selyear = $("#ddl_year").val();
                if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                var selmonth = $("#dll_month").val();
                if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }

                $('#StockTransferList tbody tr').remove();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Stock_Transfer_List.aspx/Get_Stock_Transfer_List",
                    data: "{'years':'" + selyear + "','months':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                var strFF = "<td>" + (i + 1) + "</td><td>" + data.d[i].Transfer_No + "</td><td>" + data.d[i].Transfer_Date + "</td><td><input type='hidden' name='supp_Code' value='" + data.d[i].Transfer_From + "'/>" + data.d[i].Transfer_From_Nm + "</td><td><input type='hidden' name='supp_Code' value='" + data.d[i].Transfer_To + "'/>" + data.d[i].Transfer_To_Nm + "</td><td><a class='btn btn-primary btn-md' href='Stock_Transfer.aspx?Mode=1&Code=" + data.d[i].Trans_SlNo + "'><span class='glyphicon glyphicon-edit'> Edit</span></a></td><td><a class='btn btn-primary btn-md' href='Stock_Transfer_View.aspx?Mode=1&Code=" + data.d[i].Trans_SlNo + "'><span class='glyphicon glyphicon-eye-open'> View</span></a></td>";
                                $("#StockTransferList tbody").append("<tr class='gvRow'>" + strFF + "</tr>");
                            }
                        }
                        else {

                            $("#StockTransferList tbody").append("<tr class='gvRow'><td colspan='7'> No Records Found..!</td></tr>");
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            });


            $(document).on('change', '#ddlFilter', function () {
                $("#GoodRecivedList > tbody > tr").each(function () {
                    var Supp_Code = $(this).find("input[name=supp_Code]").val().toLowerCase().toString()
                    var lnkText = ',' + $("#ddlFilter").val().toLowerCase() + ',';
                    $(this).css('display', 'table-row');
                    if (lnkText != ',0,') {
                        if (lnkText.toLowerCase().indexOf(',' + Supp_Code + ',') > -1) {
                            $(this).css('display', 'tabel-row');
                        }
                        else {
                            $(this).css('display', 'none');
                        }
                    }

                });
            });

        });
    </script>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <a class='btn btn-success btn-md' href='Stock_Transfer.aspx?Mode=0'>New Stock Transfer</a>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-8">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Label ID="lblFMonth" runat="server" Text="Year"></asp:Label>
                        <select id="ddl_year" name="txt_year" class="form-control" style="width: 130px">
                            <option>--- Select ---</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>
                        <select id="dll_month" name="dll_month" class="form-control" style="width: 130px">
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
                    <a id="btnGo" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                        <span>Go</span></a>
                </div>
            </div>
            <div class="col-sm-4" style="text-align: right">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Transfer From"></asp:Label>
                        <select id="ddlFilter" name="ddlFilter" class="form-control">
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-12 inputGroupContainer">
                <table class="table table-responsive" id="StockTransferList">
                    <thead>
                        <tr>
                            <th class='col-xs-1'>
                                Sl.No.
                            </th>
                            <th class='col-xs-1'>
                                Transfer No.
                            </th>
                            <th class='col-xs-2'>
                                Transfer Date
                            </th>
                            <th class='col-xs-3'>
                                Transfer From
                            </th>
                            <th class='col-xs-3'>
                                Transfer TO
                            </th>
                            <th class='col-xs-1'>
                            </th>
                            <th class='col-xs-1'>
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
