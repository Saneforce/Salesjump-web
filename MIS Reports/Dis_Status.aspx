<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Dis_Status.aspx.cs" Inherits="MasterFiles_Dis_Status" %>

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
        
        #ProductTable td:nth-child(8), #ProductTable td:nth-child(13), #ProductTable td:nth-child(14)
        {
            font-weight: bold;
        }
    </style>
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btnExcel").hide();
            $('.datePicker').datepicker({ dateFormat: 'yy-mm-dd' });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Dis_Status.aspx/GetDistributor",
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
            $(document).on('click', '.btnsave', function () {

               
                var ddlcust = $('#ddldistributor').val();
                if (ddlcust == 0) { alert('Select distributor..!'); $('#ddldistributor').focus(); return false; };
                var fromdate = $('#fromDate').val();
                if (fromdate.length <= 0) { alert('Enter From Date..!'); $('#fromDate').focus(); return false; };
                var todate = $('#toDate').val();
                if (todate.length <= 0) { alert('Enter To Date..!'); $('#toDate').focus(); return false; };


                $('#Product_Table tr').remove();
                var len = 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Dis_Status.aspx/getdata",
                    dataType: "json",
                    success: function (data) {
                        len = data.d.length;
                        if (data.d.length > 0) {
                            str = '<th  style="min-width:250px; " " rowspan="2"> <p style="margin: 0 0 0px;">Field Force</p> </th>';
                            // str1 = ''
                            strff = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Total</p> </th>';
                            for (var i = 0; i < data.d.length; i++) {
                                str += '<th style="min-width:150px"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].product_name + '</p> </th>';
                                //  str1 += '<th>Free</th>';
                                strff += '<th style="text-align: right" ></th>';
                            }
                            $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');
                            // $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '</tr>');
                            $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '</tr>');
                            $(".btnExcel").show();
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });



                var Fyear = $('#fromDate').val();
                var FMonth = $('#toDate').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Dis_Status.aspx/getIssuData",
                    data: "{'SF_Code':'" + ddlcust + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(JSON.stringify(data.d));
                        if (data.d.length > 0) {

                            var sfCode = '';
                            for (var i = 0; i < data.d.length; i++) {

                                var str = '';
                                if (sfCode == data.d[i].sfCode) {

                                }
                                else {
                                    str = '<td><input type="hidden" name="sfcode" value="' + data.d[i].sfCode + '"/> <p name="sfname" style="margin: 0 0 0px;">' + data.d[i].sfName + '</p> </td>';
                                    for (var j = 0; j < len; j++) {
                                        str += '<td></td>';
                                    }

                                    $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                                }
                                sfCode = data.d[i].sfCode;
                            }


                            var dtls_tab = document.getElementById("Product_Table");
                            var nrows1 = dtls_tab.rows.length;
                            var Ncols = dtls_tab.rows[0].cells.length;
                            console.log(Ncols);
                            for (var i = 0; i < data.d.length; i++) {
                                $('#Product_Table tbody tr').each(function () {
                                    fd = 0;
                                    curr = 0;
                                    for (var col = 0; col < Ncols - 2; col++) {
                                        fd = col + 1;
                                        if ($(this).children('td').eq(0).find('input[name=sfcode]').val() == data.d[i].sfCode) {
                                            curr = i;
                                            if ($(this).closest('table').find('.mainhead').find('th').eq(fd).find('input[name=pcode]').val() == data.d[i].proCode) {
                                                $(this).children('td').eq((fd)).text(data.d[i].amount);
                                            }


                                        }
                                    }
                                    fd++;


                                });
                            }


                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                var arr = [];
                $('#Product_Table tbody tr').each(function () {
                    var i = 0;
                    $(this).find('td').each(function () {
                        if (i != 0) {
                            arr[i - 1] = Number(arr[i - 1] || 0) + Number($(this).text() || 0);
                        }
                        i++;
                    });
                });
                // console.log(arr);

                var i = 0;
                $('.trfoot th').each(function () {
                    if (i != 0) {
                        //console.log($(this));

                        $(this).text(arr[i - 1]);

                    }
                    i++;
                });


                $('.mainhead th').each(function (i) {
                    var remove = 0;

                    var tds = $(this).parents('table').find('tr td:nth-child(' + (i + 1) + ')')


                    tds.each(function (j) {
                        if (this.innerHTML == '' || this.innerHTML == 0) remove++;
                    });
                    if (remove == ($('#Product_Table tbody tr').length)) {
                        $(this).hide();
                        tds.hide();
                        $('#Product_Table tfoot tr th').eq($(this).index()).hide();
                        //  if (i > 0) {
                        // if (i % 2 == 0) {
                        // $('.mainhead th').eq($(this).index() + 1).hide();
                        // }
                        //}
                    }
                });


            });

            $(document).on('click', ".btnExcel", function () {
                var printableArea = $('#printableArea').html();
                window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#printableArea').html()));
                e.preventDefault();
            });






        });
    </script>
    <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <div class="form-group">
                <label for="location" class="control-label col-sm-2 col-sm-offset-3" style="font-weight: normal">
                    Distributor
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
    <div class="row">
        <div style="overflow-x: auto;">
            <div id="printableArea" class="page">
                <table id="Product_Table" class="newStly">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>
    <div class="row" style="text-align: center">
        <a name="btnExcel" type="button" class="btn btn-primary btnExcel" style="width: 100px">
            Excel</a>
    </div>
</asp:Content>
