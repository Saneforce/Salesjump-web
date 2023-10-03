<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Merchand_Inout.aspx.cs" Inherits="MIS_Reports_Merchand_Inout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../js/daterangepicker-3.0.5.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var sfCode = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fdate = $("#<%=Fdate.ClientID%>").val();
            var Tdate = $("#<%=Tdate.ClientID%>").val();
            var subdiv = $("#<%=SubDivCode.ClientID%>").val();
            var sfname = $("#<%=sfname.ClientID%>").val();
            GetRetailersIn_Out(sfCode, Fdate, Tdate, subdiv);
			$(document).on('click', '.sfcount', function () {
                var rtcd = $(this).closest('tr').attr('rocode');
                var rtnm = $(this).closest('tr').attr('rtname');
                var slno = $(this).closest('tr').find('.pcod_hidd').val();
                window.open("Merchand_Activity.aspx?&slno=" + slno + "&rtCode=" + rtcd + "&rtnm=" + rtnm,
                    "NewTab", 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
        });
        function GetRetailersIn_Out(sfCode, Fdate, Tdate, subdiv) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Merchand_Inout.aspx/GetRetailersIn_Out",
                async: false,
                dataType: "json",
                //data: "{'Fdate':'" + Fdate + "','Tdate':'" + Tdate + "','sfCode':'" + sfCode + "','subdiv':'" + subdiv+"'}",
                success: function (data) {
                    details = JSON.parse(data.d) || [];
                    if (details.length > 0) {
                        var slno = 0, str='';
                        $('#cnt_retailerdet TBODY').html("");
                        for (var i = 0; i < details.length; i++) {
                            slno = i + 1;
                            if (details[i].C_Flag == '1') {
                                str += "<tr rocode='" + details[i].Retailer_Code + "' rtname='" + details[i].Listeddr_Name + "'><td>" + slno + "</td><td>"+details[i].User_Name+"</td><td id='" + details[i].Retailer_Code + "'><input type='hidden' class='pcod_hidd' value='" + details[i].Sl_No + "'>" + details[i].Listeddr_Name + "</td><td>" + details[i].CIN_Date + "</td><td>" + details[i].CIN_Time + "</td><td>------</td><td>------</td><td class='sfcount'><a href='#' >View</a></td></tr>";
                            }
                            else {
                                str += "<tr rocode='" + details[i].Retailer_Code + "' rtname='" + details[i].Listeddr_Name + "'><td>" + slno + "</td><td>"+details[i].User_Name+"</td><td id='" + details[i].Retailer_Code + "'><input type='hidden' class='pcod_hidd' value='" + details[i].Sl_No + "'>" + details[i].Listeddr_Name + "</td><td>" + details[i].CIN_Date + "</td><td>" + details[i].CIN_Time + "</td><td>" + details[i].COUT_Time + "</td><td>" + details[i].Duration + "</td><td class='sfcount'><a href='#' >View</a></td></tr>";
                            }
                        }
                        $('#cnt_retailerdet TBODY').append(str);
                    }
                    else {
                        document.getElementById('ndf').innerHTML = "No Data Found for View...";
                    }
                }
            });
            
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
            var tets = 'MerchandizingActivity_report' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    </script>
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 100%;
            overflow: scroll;
        }

        th {
            position: sticky;
            top: 0;
            background-color: #496a9a;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        #grid td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="Fdate" runat="server" />
    <asp:HiddenField ID="Tdate" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
        <asp:HiddenField ID="sfname" runat="server" />
         <div>
             <div class="container-fuild ">
                  <div class="m-0">
                    <div style="margin-right:50px;position:inherit;" class="col-3">
                        <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" onclick="exportToExcel()" width="40" height="40" id="btnExport" />                        
                    </div>
                </div>
				<table width="100%">
                <tr>
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead" Text="" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                    </tr>
                     </table>
                 <div class="row m-0">
                            </br>
                            </br>
                    <div class="table-responsive col-10" style="max-width: 800px; max-height: 800px; margin: auto;">
                        <table id="cnt_retailerdet" class="table table-bordered table-hover grids" width="100%">
                            <thead>
                                <tr>
                                    <th>SlNo</th>
									<th>FieldForce Name</th>
                                    <th>Retailer Name</th>
                                    <th>Checkin Date</th>
                                    <th>Checkin Time</th>
                                    <th>Checkout Time</th>
                                    <th>Duration</th>
                                    <th>UI Activity</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="ndf" align="center" valign="middle" style="color:Black;background-color:AliceBlue;border-color:Black;border-width:2px;border-style:Solid;font-weight:bold;height:5px;">
                                    <td></td>
                                </tr>
                            </tbody>
                            <tfoot></tfoot>
                        </table>
                    </div>
                </div>
                 </div>
        </div>
    </form>
    
</body>
</html>
