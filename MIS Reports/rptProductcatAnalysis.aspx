<%@ Page Language="C#" Title="Product wise Analysis" AutoEventWireup="true" CodeFile="rptProductcatAnalysis.aspx.cs"
    Inherits="MIS_Reports_rptProductcatAnalysis" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">      
        
        .num2
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var Fyear = $("#<%=hdnYear.ClientID%>").val();
            var FMonth = $("#<%=hdnMonth.ClientID%>").val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptProductcatAnalysis.aspx/GetProductDtls",
                data: "{'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dPDtls = data.d;
                    //nsole.log(data.d);
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
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

            $('#ProductTable tr').remove();
            var str = '<th style="min-width:70px;">SLNo.</th><th style="min-width:200px;">Product Name</th><th style="min-width:120px;">Qty(Std Unit)</th><th style="min-width:120px;">Value </th> <th style="min-width:120px;">% of Sales</th> <th style="min-width:120px;">Total SR </th> <th style="min-width:120px;">Count Of SR </th> <th style="min-width:120px;">% Of SR</th>';
            str += '<th style="min-width:120px;">Total TC</th><th style="min-width:120px;">Count Of PC</th><th style="min-width:120px;">% of PCs</th><th style="min-width:120px;">UTC</th><th style="min-width:120px;">UPC</th><th style="min-width:120px;">% Of Outlets</th>';
            $('#ProductTable thead').append('<tr >' + str + '</tr>');
            if (dPDtls.length > 0) {
                var nQty = 0;
                var nVal = 0;
                for (var i = 0; i < dPDtls.length; i++) {
                    str = '<td class="num2">' + (i + 1) + '</td><td>' + dPDtls[i].PName + '</td> <td class="num2">' + dPDtls[i].OrdQty + '</td> <td class="num2">' + dPDtls[i].OrdVal + '</td> <td class="num2">' + dPDtls[i].OrdPer + '%' + '</td> <td class="num2">' + dPDtls[i].TSR + '</td> <td class="num2">' + dPDtls[i].CntSR + '</td>  <td class="num2">' + (dPDtls[i].CntSR / dPDtls[i].TSR * 100).toFixed(2) + '%' + '</td> ';
                    str += '<td class="num2">' + dPDtls[i].TPC + '</td><td class="num2">' + dPDtls[i].CntPC + '</td><td class="num2">' + (dPDtls[i].CntPC / dPDtls[i].TPC * 100).toFixed(2) + '%' + '</td>';
                    str += '<td class="num2">' + dPDtls[i].TUPC + '</td><td class="num2">' + dPDtls[i].CntUPC + '</td><td class="num2">' + (dPDtls[i].CntUPC / dPDtls[i].TUPC * 100).toFixed(2) + '%' + '</td>';
                    $('#ProductTable tbody').append('<tr>' + str + '</tr>');
                    nQty += Number(dPDtls[i].OrdQty || 0);
                    nVal += Number(dPDtls[i].OrdVal || 0);
                }
                strF = '<th colspan="2">Total</th><th style="text-align:right" > ' + nQty + ' </th><th style="text-align:right" > ' + nVal.toFixed(2) + ' </th><th >-</th><th >-</th><th  >-</th><th  >-</th>';
                strF += '<th >-</th><th  >-</th><th >-</th><th " >-</th><th >-</th><th >-</th>';
                $('#ProductTable tfoot').append('<tr>' + strF + '</tr>');
                $("#<%=Label2.ClientID%>").hide();
            }
            else {
                var len = $('#ProductTable thead tr td').length;
                $('#ProductTable tbody').append('<tr><td colspan="' + len + '">No Record Fount..!</td></tr>');
                $("#<%=Label2.ClientID%>").hide();
            }

            $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#excelDiv').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'ProductAnalysis.xls';
                a.click();
                e.preventDefault();
            });

        });


       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row" style="max-width: 100%; width: 98%">
        <br />
        <div class="col-sm-8">
            <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large;
                padding: 0px 20px;" Text="Product Analysis"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                Visible="false" />
            <a id="btnExport" runat="Server" style="padding: 0px 20px;" class="btn btnPdf" visible="false">
            </a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" /><a name="btnClose"
                id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                class="btn btnClose"></a>
        </div>
    </div>
    <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
        <asp:HiddenField ID="hdnYear" runat="server" />
        <asp:HiddenField ID="hdnMonth" runat="server" />
        <div class="row">
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label" Style="padding-left: 5px;" Visible="false"></asp:Label>
            <br />
            <div id="content">
                <table id="ProductTable" border="1" class="newStly" style="border-collapse: collapse;">
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
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Label" Style="padding-left: 20px; text-align: center;
        font-size: x-large; color: #0210ff; font-weight: bold"></asp:Label>
    </form>
</body>
</html>
