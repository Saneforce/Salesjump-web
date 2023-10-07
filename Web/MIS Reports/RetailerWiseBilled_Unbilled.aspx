<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetailerWiseBilled_Unbilled.aspx.cs" Inherits="MIS_Reports_RetailerWiseBilled_Unbilled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
            overflow: scroll;
        }

        th {
            position: sticky;
            top: 0;
            background: #6c7ae0;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        #grid td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }
    </style>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script type="text/javascript">
        var Cntdetails = []; var str = '';
        $(document).ready(function () {
            $('#loadover').show();
            setTimeout(function () {
                setTimeout(GetRetailersBilled_Unbilled(), 500);
            }, 500);
        });

        function GetRetailersBilled_Unbilled() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "RetailerWiseBilled_Unbilled.aspx/GetRetailersBilled_Unbilled",
                async: false,
                dataType: "json",
                success: function (data) {
                    Cntdetails = JSON.parse(data.d) || [];
                    if (Cntdetails.length > 0) {
                        ReloadTable(Cntdetails);
                        $('#loadover').hide();
                    }
                },
                error: function (result) {
                    alert(result);
                }
            });
        }

        function TypeChng() {
            var chngtyp = $('#ddlType option:selected').val();
            if (chngtyp != 'All') {
                FilterCnt = Cntdetails.filter(function (a) {
                    return a.Stat == chngtyp;
                });
                ReloadTable(FilterCnt)
                //(FilterCnt.length > 0) ? ReloadTable(FilterCnt) : $('#cnt_retailerdet TBODY').append('No Data');
                $('#loadover').hide();
            }
            else {
                ReloadTable(Cntdetails);
            }
        }

        function ReloadTable(LoadArr) {
            var slno = 0; str = '';
            $('#cnt_retailerdet TBODY').html("");
            if (LoadArr.length > 0) {
                for (var i = 0; i < LoadArr.length; i++) {
                    slno = i + 1;
                    str += "<tr><td>" + slno + "</td><td>" + LoadArr[i].SFNA + "</td><td>" + LoadArr[i].ListedDrCode + "</td><td>" + LoadArr[i].ListedDr_Name + "</td><td style='width:10px !important;'>" + LoadArr[i].Doc_Class_ShortName + "</td><td>" + LoadArr[i].Territory_Name + "</td><td>" + LoadArr[i].Stat + "</td></tr>";
                }
            }
            else {
                str = 'No Data';
            }
            $('#cnt_retailerdet TBODY').append(str);
        }

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
            htmls = document.getElementById("cnt_retailerdet").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Retailer_Visit_Status' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }

    </script>
    <form id="form1" runat="server">
        <div>
            <div class="container-fuild ">
                <div class="m-0">
                    <h3>Retailer Wise Report</h3>
                    <div style="margin-right: 50px; position: inherit;text-align: end;" class="col-3">
                        <label>Type  :</label>
                        <select id="ddlType" onchange="TypeChng()">
                            <option selected="selected" disabled="disabled">Nothing Select</option>
                            <option value="">All</option>
                            <option value="Non Visited">Non Visited</option>
                            <option value="Not Billed">Not Billed</option>
                            <option value="Billed">Billed</option>
                        </select>
                        <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" onclick="exportToExcel()" width="40" height="40" id="btnExport" />
                    </div>
                </div>
                <div class="row m-0">
                    <div class="table-responsive col-10" style="max-width: 90%; max-height: 800px; margin: auto;">
                        <table id="cnt_retailerdet" class="table table-bordered table-hover grids">
                            <thead>
                                <tr>
                                    <th>SlNo</th>
                                    <th>Field Force</th>
                                    <th>Retailer Code</th>                                    
                                    <th>Retailer Name</th>                                    
                                    <th>Category</th>  
                                    <th>Route Name</th>
                                    <th>Visited Details</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot></tfoot>
                        </table>
                    </div>
                </div>
                <div class="col-2">

                    <div class="overlay" id="loadover" style="display: none;">
                        <div id="loader">Please Wait Loading...</div>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
