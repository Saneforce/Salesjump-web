<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listviewof_retailers.aspx.cs" Inherits="MIS_Reports_Listviewof_retailers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />    
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />   
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
             animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" />
        </div>

        <%--        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />--%>

        <div class="row">
            
        </div>

        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="lblHead" Font-Bold="true" runat="server"></asp:Label>
        </div>

        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
                <table id="FFTbl" class="newStly" style="border-collapse: collapse;width:80%;">
                    <thead>
                        <tr>
                            <th>SLNo.</th>
                            <th>Retailer Name</th>                                                       
                            <th>Visited Status</th>
                            
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var Retlist = [];
        $(document).ready(function () {
            loaddata();

        });
        $('#btnExport').click(function () {

            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("content").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Routewise retailers' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();




        });
        function loaddata() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Listviewof_retailers.aspx/getroutecnt",
                dataType: "json",
                success: function (data) {
                    SFdet = JSON.parse(data.d) || [];
                    $('#FFTbl tbody').html('');
                    var slno = 0; var tr = '';
                    for ($i = 0; $i < SFdet.length; $i++) {
                        if (SFdet.length > 0) {
                            slno += 1;
                            if (SFdet[$i].Order_Value > 0) {
                                var value = SFdet[$i].Order_Value;
                                 SFdet[$i].Order_Value;
                                var action = '<td style="font-size:16px;color:black;">' + value + '</td>';
                            }
                            else if (SFdet[$i].Order_Value =='0') {                                                                   
                                var value = "<img style='width:25px;f' src='../css/images/green-check-mark-.png' />";
                                var action ='<td style="font-size:16px;color:green;">Visited</td>';
                            }
                            else if (SFdet[$i].Order_Value == '-1') {
                                var value = "<img style='width:25px;' src='../css/images/close-icon-30.png' />";
                                var action = '<td style="font-size:16px;color:red;">Not Visited</td>';
                            } 

                            tr = '<tr><td style="font-size:16px;">' + slno + '</td><td style="font-size:16px;">' + SFdet[$i].ListedDr_Name + '</td>'+action+'</tr>';/*<td>' + value + '</td>*/
                        }
                        $('#FFTbl tbody').append(tr);
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
    </script>
</body>
</html>
