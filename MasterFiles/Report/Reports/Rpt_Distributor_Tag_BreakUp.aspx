<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Distributor_Tag_BreakUp.aspx.cs" Inherits="MasterFiles_Reports_Rpt_Distributor_Tag_BreakUp" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style>
        #gvMyDayPlan{
            margin:30px;
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
            $("#btnExcel").click(function () {
                $("#gvMyDayPlan").table2excel({
                    filename: "Distributor Tag BreakUp.xls"
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
                    <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: 12pt; padding: 0px 20px;">Distributor Tag Status</asp:Label>
                </div>
                <div class="col-sm-4" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                        href="#" class="btn btnPrint"></a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" /><a name="btnClose"
                            id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
                </div>
                <asp:GridView ID="gvMyDayPlan" runat="server" Width="100%" HorizontalAlign="Center"
                                    BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View"
                                    AutoGenerateColumns="true" CssClass="newStly" >
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>