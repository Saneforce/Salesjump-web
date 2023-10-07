<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSalesReturnNew.aspx.cs"
    Inherits="MIS_Reports_rptSalesReturnNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <%-- <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>--%>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="  https://cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.js"></script>
    <script type="text/javascript">
        jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();
    </script>
    <script type="text/javascript" type="text/javascript">
        $(document).ready(function () {

            var sfCode = $('#<%=hsfcode.ClientID%>').val();
            var FYear = $('#<%=hfyear.ClientID%>').val();
            var FMonth = $('#<%=hfmonth.ClientID%>').val();
            var pData = [];
            var hDate = [];
            var dtls = [];


            genTbale = function () {
                if (hDate.length > 0 && pData.length > 0) {
                    tbl = $('#SalTable');
                    str = '<th rowspan="2">SlNo.</th><th rowspan="2">Return Date</th><th rowspan="2">Retailer Name</th><th rowspan="2">Route Name</th><th rowspan="2">Distributor Name</th>';
                    str1 = '';
                    for (var i = 0; i < pData.length; i++) {
                        str += '<th colspan="2">' + pData[i].pName + '</th>';
                        str1 += '<th>Case QTY</th><th>Piece QTY</th>';
                    }
                    $(tbl).find('thead').append('<tr>' + str + '</tr>');
                    $(tbl).find('thead').append('<tr>' + str1 + '</tr>');

                    for (var i = 0; i < hDate.length; i++) {
                        str = '<td>' + (i + 1) + '</td><td>' + hDate[i].ReturnDate + '</td><td><input type="hidden" name="rtCode" value="' + hDate[i].RtlCode + '">' + hDate[i].RtlName + '</td><td>' + hDate[i].RouteName + '</td><td><input type="hidden" name="distCode" value="' + hDate[i].DisCode + '">' + hDate[i].DisName + '</td>';
                        for (var j = 0; j < pData.length; j++) {
                            if (pData[j].pCode == hDate[i].ProCode) {
                                str += '<td>' + hDate[i].CaseQty + '</td><td>' + hDate[i].PecQty + '</td>';
                            }
                            else {
                                str += '<td></td><td></td>';
                            }
                        }
                        $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                    }
                }
            }




            $(document).on('click', '#SalTable tbody tr', function () {
                var rtCode = $(this).find('input[name="rtCode"]').val();
                var distCode = $(this).find('input[name="distCode"]').val();
                var cDate = $(this).find('td').eq(1).text();

                var rtName = $(this).find('td').eq(2).text();
                var routeName = $(this).find('td').eq(3).text();
                var distName = $(this).find('td').eq(4).text();

                tbl = $("#Table1");
                $(tbl).find('tbody tr').remove();
                $(tbl).find('thead tr').remove();

                $('#lblrtname').text(rtName);
                $('#distName').text(distName);
                $('#rtDate').text(cDate);
                $('#routenames').text(routeName);
                

                str = '<th >SlNo.</th><th >Order Number</th><th >Product Name</th><th >Case QTY </th><th >Piece QTY</th><th>Type Name</th><th>Batch Number</th><th>Batch Date</th> <th >Remarks</th>';
                $(tbl).find('thead').append('<tr>' + str + '</tr>');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptSalesReturnNew.aspx/getReturnDetailsVal",
                    data: "{'rtCode':'" + rtCode + "','distCode':'" + distCode + "','cDate':'" + cDate + "'}",
                    dataType: "json",
                    success: function (data) {
                        dtls = data.d;
                        for (var k = 0; k < dtls.length; k++) {

                            stk = '<td>' + (k + 1) + '</td><td>' + dtls[k].orderNum + '</td><td>' + dtls[k].proName + '</td><td>' + dtls[k].CaseQty + '</td><td>' + dtls[k].PecQty + '</td><td>' + dtls[k].typeName + '</td><td>' + dtls[k].batchNum + '</td><td>' + dtls[k].batchDate + '</td><td>' + dtls[k].remarks + '</td>';
                            $(tbl).find('tbody').append('<tr>' + stk + '</tr>');
                        }
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });

                $("#help").show();
                $('#SalTable').block({ message: null });


                //                $("#help").slideToggle(1000, function () {
                //                    if ($("#help_button").val() == "close") {
                //                        $("#help_button").val("show table");
                //                    }
                //                    else {
                //                        $("#help_button").val("close");
                //                    }
                //                });






            });
            $(document).on('click', '#btnclose', function () {
                $("#help").hide();
                $('#SalTable').unblock();
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptSalesReturnNew.aspx/getProducts",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    pData = data.d;
                    genTbale();
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptSalesReturnNew.aspx/getReturnHeaderVal",
                data: "{'SFCode':'" + sfCode + "','FYear':'" + FYear + "','FMonth':'" + FMonth + "'}",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    hDate = data.d;
                    genTbale();
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                

                var a = document.createElement('a');
                var blob = new Blob([$('div[id$=excelDiv]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'Sales_Return.xls';
                a.click();
                e.preventDefault();
            });


        }); 
    </script>
    <style type="text/css">
        .ui-dialog
        {
            width: 500px !important;
        }
        #help
        {
            background: #cfd2d6;
            width: 50%;
            height: 300px;
            display: none;
            position: fixed;
            top: 50%;
            left: 30%;
            margin: -150px 0 0 -150px;
            z-index: 10000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hsfcode" runat="server" />
    <asp:HiddenField ID="hfyear" runat="server" />
    <asp:HiddenField ID="hfmonth" runat="server" />
    <br />
    <div class="container" id="excelDiv" style="max-width: 100%; width: 95%">
        <div class="row">
            <div class="col-sm-8" style="padding-left: 5px;">
                <asp:Label ID="Label2" runat="server" Text="Sales Return Details " Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
            <div class="col-sm-4" style="text-align: right">
                <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
                <a name="btnClose" id="A1" type="button" href="javascript:window.open('','_self').close();"
                    class="btn btnClose"></a>
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="" Style="padding-left: 5px;"></asp:Label>
        <br />
        <table id="SalTable" class="newStly " border="1" style="border-collapse: collapse;
            width: 100%">
            <thead>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div id="help">
        <div style="width: 100%; text-align: right">
            <a class="btn btn-primary" id="btnclose">Close</a>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>
                    Retailer Name :</label>
                    <label id="lblrtname">
                </label>
            </div>
            <div class="col-md-6">
                <label>
                    Date :
                </label>
                <label id="rtDate">
                </label>
            </div>            
        </div>
        <div class="row">
        <div class="col-md-6">
                <label>
                    Route Name :</label>
                    <label id="routenames">
                </label>
            </div>
            <div class="col-md-6">
                <label>
                    Distributor Name :</label>
                    <label id="distName">
                </label>
            </div>
        </div>
        <table id="Table1" class="newStly " border="1" style="border-collapse: collapse;
            width: 100%">
            <thead>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div id="dialog-message" title="Download complete" style="display: none; width: 50%">
        <p>
            <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
            </span>Your files have downloaded successfully into the My Downloads folder.
        </p>
        <p>
            Currently using <b>36% of your storage space</b>.
        </p>
    </div>
    </form>
</body>
</html>
