<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printIssueSlip.aspx.cs" Inherits="MIS_Reports_printIssueSlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Issue Slip</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "printIssueSlip.aspx/getIssuData",
                data: "{'fromDate':'" + $('#<%=fromDate.ClientID  %>').val() + "',  'SFCode':'" + $('#<%=hdnsfcode.ClientID  %>').val() + "' }",
                dataType: "json",
                success: function (data) {
                    console.log(JSON.stringify(data.d));
                    var totAmt = 0;
                    var sf_name = '';
                    var issName = '';
                    if (data.d.length > 0) {
                        str = "";
                        for (var i = 0; i < data.d.length; i++) {
                            sf_name = data.d[0].sfName;
                            issName = data.d[0].StkNm;
                            str = '<td>' + (i + 1) + '</td><td>' + data.d[i].proName + '</td><td style="text-align:right">' + Number(data.d[i].rate).toFixed(2) + '</td><td style="text-align:right">' + data.d[i].caseRate + '</td><td style="text-align:right">' + data.d[i].piceRate + '</td><td style="text-align:right">' + Number(data.d[i].amount).toFixed(2) + '</td>';
                            $('#productTable tbody').append('<tr>' + str + '</tr>');
                            console.log(str);

                            totAmt += Number(data.d[i].amount);
                        }
                    }
                    $('#lblfoName').text(sf_name);
                    $('#lblIssuDt').text($('#<%=fromDate.ClientID  %>').val());
                    $('#lblIssuedBy').text(issName);
                    $('#lblTot').text(totAmt.toFixed(2));
                    $('#lblRptDis').text(sf_name);

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "printIssueSlip.aspx/getReportingtoSF",
                data: "{'SFCode':'" + $('#<%=hdnsfcode.ClientID  %>').val() + "' }",
                dataType: "json",
                success: function (data) {
                    console.log(JSON.stringify(data.d));

                    $('#lblRptSF').text(data.d);



                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


            $(document).on('click', "#btnprint", function () {
                var originalContents = $("body").html();
                var printContents = $(".container").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
            $(document).on('click', "#btnclose", function () {
                window.close();
            });

        });
    </script>
    <style type="text/css" media="print">
        .container
        {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: 0px;
            margin-left: 0px;
        }
    </style>
    <style type="text/css">
        @page
        {
            size: auto; /* auto is the initial value */
            margin: 0mm; /* this affects the margin in the printer settings */
        }
        
        html
        {
            background-color: #FFFFFF;
            margin: 0px; /* this affects the margin on the html before sending to printer */
        }
        
        body
        {
            margin: 10mm 15mm 10mm 15mm; /* margin you want for the content */
        }
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnsfcode" runat="server" />
    <asp:HiddenField ID="fromDate" runat="server" />
    <div style="text-align: center">
        <br />
        <a id="btnprint" class="btn btn-primary" style="vertical-align: middle; font-size: 14px;">
            <span>Print</span></a> <a id="btnclose" class="btn btn-primary" style="vertical-align: middle;
                font-size: 14px;"><span>Close</span></a>
    </div>
    <br />
    <div class="container" style="max-width: 100%; margin-left: 0px; margin-right: 0px;">
        <div class="row">
            <label id="foName" class="col-sm-2 control-label">
                Name</label>
            <label id="lblfoName" class="col-sm-4 control-label">
            </label>
            <label id="issDate" class="col-sm-2 control-label">
                Issue Date</label>
            <label id="lblIssuDt" class="col-sm-2 control-label">
            </label>
        </div>
        <div class="row">
            <label id="issedBy" class="col-sm-2 control-label">
                Issued By</label>
            <label id="lblIssuedBy" class="col-sm-2 control-label">
            </label>
        </div>
        <div class="row" style="padding: 5px 10px;">
            <table id="productTable" class="newStly">
                <thead>
                    <tr>
                        <th style="min-width: 50px">
                            SlNo.
                        </th>
                        <th style="min-width: 350px">
                            Product Name
                        </th>
                        <th style="min-width: 150px">
                            Rate
                        </th>
                        <th style="min-width: 150px">
                            Case
                        </th>
                        <th style="min-width: 150px">
                            Piece
                        </th>
                        <th style="min-width: 150px">
                            Amount
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th colspan="2">
                            Total
                        </th>
                        <th colspan="3">
                        </th>
                        <th style="text-align: right">
                            <label id="lblTot">
                                0.00</label>
                        </th>
                    </tr>
                </tfoot>
            </table>
        </div>
        <div class="row" style="padding: 5px 10px;">
            <label id="Label1" class="col-sm-6 control-label" style="text-align: center">
                Received By 
            </label>
            <label id="Label2" class="col-sm-6  control-label" style="text-align: center">
                Approved By
            </label>
        </div>
        <br />
        <div class="row" style="padding: 5px 10px;">
            <label id="lblRptDis" class="col-sm-6 control-label" style="text-align: center">
            </label>
            <label id="lblRptSF" class="col-sm-6 control-label" style="text-align: center">
            </label>
        </div>
    </div>
    </form>
</body>
</html>
