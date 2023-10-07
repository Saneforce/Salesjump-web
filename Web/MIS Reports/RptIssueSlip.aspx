<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="RptIssueSlip.aspx.cs" Inherits="MIS_Reports_RptIssueSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Fromdate').datepicker({ dateFormat: 'dd/mm/yy' });

            $(document).on('click', '.btnview', function () {

                var fromDt = $('#Fromdate').val();
                if (fromDt.length <= 0) {
                    alert('Enter Date');
                    $('#Fromdate').focus();
                    return false;
                }


                $('#Product_Table tr').remove();
                var len = 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RptIssueSlip.aspx/getdata",
                    dataType: "json",
                    success: function (data) {
                        len = data.d.length;
                        if (data.d.length > 0) {
                            str = '<th>Print</th><th  style="min-width:250px;"> <p>Field Force</p> </th>';
                            str1 = '<th></th><th></th>'
                            for (var i = 0; i < data.d.length; i++) {
                                str += '<th style="min-width:150px" colspan="2"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname">' + data.d[i].product_name + '</p> </th>';
                                str1 += '<th>Case</th><th>Piece</th>'
                            }
                            $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');
                            $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '</tr>');
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RptIssueSlip.aspx/getIssuData",
                    data: "{'fromDate':'" + $('#Fromdate').val() + "'}",
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
                                    str = '<td><a name="btnprint" type="button" class="btn btn-primary btnprint"  href="printIssueSlip.aspx?SF_Code=' + data.d[i].sfCode + '&fromDate=' + $('#Fromdate').val() + '" style="width: 100px">Print</a> </td><td><input type="hidden" name="sfcode" value="' + data.d[i].sfCode + '"/> <p name="sfname">' + data.d[i].sfName + '</p> </td>';
                                    for (var j = 0; j < len; j++) {
                                        str += '<td></td><td></td>';
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

                                    for (var col = 0; col < Ncols - 1; col++) {
                                        fd = col + 2;
                                        if ($(this).children('td').eq(1).find('input[name=sfcode]').val().toLowerCase().toString() == data.d[i].sfCode.toLowerCase().toString()) {

                                            //console.log("|" + $(this).closest('table').find('.mainhead').find('th').eq(fd).find('input[name=pcode]').val() + '==' + data.d[i].proCode + "|");

                                            if ($(this).closest('table').find('.mainhead').find('th').eq(fd).find('input[name=pcode]').val() == data.d[i].proCode) {
                                                console.log($(this).children('td').eq(fd));
                                                $(this).children('td').eq((fd * 2) - 2).text(data.d[i].caseRate);
                                                $(this).children('td').eq((fd * 2)-1).text(data.d[i].piceRate);
                                            }

                                        }
                                    }

                                });
                            }


                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            });

			 $(document).on('click', '.btnprint', function () {              
                event.preventDefault();
                window.open($(this).attr("href"), "popupWindow", "width=800,height=600,scrollbars=yes");

            });
			
        });
    </script>
    <form id="form1" runat="server">
    <div class="container" style="width: 100%;">
        <div class="row">
            <label for="Fromdate" class="col-md-1 control-label">
                Date</label>
            <div class="col-md-2 inputGroupContainer">
                <input type="text" name="Fromdate" id="Fromdate" class="form-control datetimepicker" />
            </div>
            <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">
                View</a>


                


        </div>
        <div class="row">
            <div style="overflow-x: auto;">
                <table id="Product_Table" class="newStly">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
