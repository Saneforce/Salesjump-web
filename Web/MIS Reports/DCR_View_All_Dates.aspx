<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_View_All_Dates.aspx.cs" Inherits="MIS_Reports_DCR_View_All_Dates" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style>
        #grdsec{
            margin:43px;
        }
    </style>
    <script type="text/javascript" src="../../js/plugins/table2excel.js"></script>

    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
        $(document).ready(function () {
            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
            $('.retcount').on('click', function () {
                var Fyear = $(this).attr('fyear');
                var Tyear = $(this).attr('tyear');
                var FMn = $(this).attr('fdate');
                var TMn = $(this).attr('tdate');
                var SF = $(this).attr('sf');
                var SFN = $(this).attr('sfn');
                var subdiv = $(this).attr('subdiv');

                strOpen = "../../MIS Reports/rpt_Retailer_Wise_Produt_Trend_Analysis.aspx?SF_Code=" + SF + "&FYear=" + Fyear + "&SF_Name=" + SFN + "&FMonth=" + FMn + "&TYear=" + Tyear + "&TMonth=" + TMn + "&subDiv=" + subdiv;
                window.open(strOpen, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
            });
            $("#btnExcel").click(function () {
                $("#grdsec").table2excel({
                    filename: "Secondary Order Summary.xls"
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large; padding: 0px 20px;"
                        ></asp:Label>
                </div>
                <div class="col-sm-4" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                        href="#" class="btn btnPrint"></a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" /><a name="btnClose"
                            id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
                </div>
                <asp:GridView ID="grdsec" runat="server"
                    HorizontalAlign="Center" Width="90%" Font-Names="andalus"
                    BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                    AutoGenerateColumns="true" class="newStly" OnDataBound="OnDataBound" OnRowCreated="OnRowCreated"
                    HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
