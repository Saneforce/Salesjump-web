<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expenserpt_view.aspx.cs" Inherits="MIS_Reports_expenserpt_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Report</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 90%;
            font-family: andalus;
            overflow: scroll;
        }

        th {
            position: sticky;
            top: 0;
            color: #fff;
            background-color: #496a9a;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
        }

        #grid td, table th {
            padding: 5px;
            border: 1px solid #ddd;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
		//$(document).ready(function () {
          //  $(document).on('click', "#btnPrint", function () {
            //    var originalContents = $("body").html();
              //  var printContents = $("#content").html();
               // $("body").html(printContents);
                //window.print();
                //$("body").html(originalContents);
                //return false;
            //});
		
        //});
    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12 sub-header">
                <span style="float: right">
            </div>
        </div>
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="60%" align="center">
                            <asp:Label ID="lblHead" Font-Bold="true" Style="color: #3F51B5; padding-right: 217PX;" Font-Size="Large" Font-Underline="true"
                                runat="server"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click" />
                                        <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf" Visible="false" />
                                        <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
                                        <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" runat="server" Width="100%" Style="margin-left: 35px">


            <table id="grid" class="newStly">
                <thead>
                    <tr>
                        <th>S.No</th>
						<th>Employee ID</th>
                        <th>Employee Name</th>
						<th>State</th>
                        <th>Applied Amount</th>
                        <th>Last Submitted Date</th>
                        <th>Expense Month</th>
                        <th>Approved Date</th>
                        <th>HQ DA</th>
                        <th>TA</th>
                        <th>Fare</th>
                        <th>MOB INT</th>
                        <th>Total Additional Amnt</th>
                        <th>ADD Amnt</th>
                        <th>DED Amnt</th>
                        <th>Expense Amt</th>
						<th style="display:none" class="Pri_Nm">Periodic Name</th>						
                        <th>Approved Status</th>
                        <th>Approve Status</th>
                    </tr>
                </thead>
                <tbody></tbody>
                <tfoot></tfoot>
            </table>


        </asp:Panel>
    </form>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            expensedtl();
			if('<%=Session["div_code"]%>'=='98'){
				$(".Pri_Nm").show();
			}
            $('#btnExcel').click(function (event) {

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
                htmls = document.getElementById("grid").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = /*'Expense_Report' + '.xls';   //create fname */"Expense_Report " + mnthname + "-" + year + ".xls"

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                event.preventDefault();
            });
        });
        var mnthname = '<%=mnthname%>';
        var year = '<%=Yr%>';

        function expensedtl() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "expenserpt_view.aspx/expensedtls",
                async: false,
                dataType: "json",
                success: function (data) {
                    $('#grid tbody').html('');
                    prods = JSON.parse(data.d);
                    str = '';
                    for (var i = 0; i < prods.length; i++) {
                        str += "<tr><td>" + (i + 1) + "</td><td>" + prods[i].Employee_Id + "</td><td>" + prods[i].SF_Name + "</td><td>" + prods[i].StateName + "</td><td>" + prods[i].Applied_ExpAmnt + "</td><td>" + prods[i].Submit_Date + "</td><td>" + prods[i].Expense_Month + "</td><td>" + prods[i].Approved_Date + "</td><td>" + prods[i].HQ_DA + "</td><td>" + prods[i].TA + "</td><td>" + prods[i].Fare + "</td><td>" + prods[i].MOB_INT + "</td><td>" + prods[i].Misc + "</td><td>" + prods[i].ADD_Amnt + "</td><td>" + prods[i].DED_Amnt + "</td><td>" + prods[i].Expense_Amt + "</td><td style='display:none;' class='Pri_Nm'>"+ prods[i].Periodic_Name +"</td><td>" + prods[i].ApprovedSts + "</td><td>" + prods[i].Approve_Status + "</td></tr >";
                    }
                    $('#grid tbody').append(str);

                }
            });
        }
    </script>
</body>
</html>
