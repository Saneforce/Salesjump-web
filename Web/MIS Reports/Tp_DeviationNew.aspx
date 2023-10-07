<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Tp_DeviationNew.aspx.cs" Inherits="MIS_Reports_Tp_DeviationNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
<style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 0px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }

            var today = mm + '/' + dd + '/' + yyyy;
            $('.datetimepicker').datepicker({ dateFormat: 'mm/dd/yy' });
            $('.datetimepicker').val(today);
        });
        </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('btnExport').style.visibility = 'hidden';

            $(document).on('click', '.btnview', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") {
                    alert('Select Field Force');
                    $("#<%=ddlFieldForce.ClientID%>").focus();
                    return false;
                }
                $('#datwise_Tp tr').remove();
                document.getElementById('btnExport').style.visibility = 'visible';
                str = '<th style="min-width:100px;><p style="margin: 0 0 0px;">Date</p></th><th style="min-width:100px;><p style="margin: 0 0 0px;">Field Force</p></th><th style="min-width:100px;><p style="margin: 0 0 0px;">Tour Plan</p></th><th style="min-width:100px;><p style="margin: 0 0 0px;">Actual Plan</p></th>';
                str += '<th style="min-width:100px;><p style="margin: 0 0 0px;">Deviation (Yes/No)</p></th><th style="min-width:100px;><p style="margin: 0 0 0px;">TC</p></th><th style="min-width:100px;><p style="margin: 0 0 0px;">PC</p></th>';
                str += '<th style="min-width:100px;> <p style="margin: 0 0 0px;">Order Value</p></th>';
                $('#datwise_Tp thead').append('<tr class="mainhead">' + str + '</tr>');
                var Dt = $("#<%=txtFromDate.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'SF_Code':'" + sf_code + "', 'date':'" + Dt + "'}",
                    url: "Tp_DeviationNew.aspx/getdata",
                    dataType: "json",
                    success: function (data) {
                        var res = JSON.parse(data.d) || [];
                        if (res.length > 0) {
                            for (var i = 0; i < res.length; i++) {
                                str = '<td>' + Dt+'</td><td><input class="AAA" type="hidden" name="sfcode" value="' + res[i].sf_code + '"/>' + res[i].sf_name + ' </td><td>' + res[i].Territory_Code1 + '</td><td>' + res[i].worked_area_name + '</td><td style="text-align:center;">' + res[i].Deviation + '</td><td style="text-align:center;"style="text-align:center;">' + res[i].TCall + '</td><td style="text-align:center;">' + res[i].EfCall + '</td><td style="text-align:center;">' + res[i].Order_Value.toFixed(2) + '</td>';

                                $('#datwise_Tp tbody').append('<tr>' + str + '</tr>');
                            }
                        }
                        else {
                            str = '<td>No Records Found...</td>';
                            $('#datwise_Tp tbody').append('<tr>' + str + '</tr>');
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });

        });
        
        function exportToExcel() {
            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("datwise_Tp").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'TP_Deviation-' + $("#<%=txtFromDate.ClientID%>").val()+'.xls';  

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
        </script>
    <form id="form1" runat="server">
    <div class="container" style="width: 100%;">
         <div class="row">
            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                Field Force</label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control" Width="350">
                    </asp:DropDownList>
                </div>
            </div>
             </div>
        <br />
        <div class="row">
                    <label for="txtFromDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                        Date</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group" id="Div1" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker" autocomplete="off" style="width: 350px;" />

                        </div>
                    </div>
                </div>
        <br />
        <div class="row">
            <div class="col-md-6  col-md-offset-5">
                <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">
                    View</a>
            </div>
        </div>
        </div>
        <br />
        <div class="m-0">
                    <div style="margin-right:50px;position:inherit;" class="col-3">
                        <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" onclick="exportToExcel()" width="40" height="40" id="btnExport" />                        
                    </div>
                </div>
        <div class="row">
            <div class="col-md-12">
                <div id="printableArea" class="page">
                    <table id="datwise_Tp" class="newStly" style="width:90%;margin: auto">
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
        
    </form>
</asp:Content>

