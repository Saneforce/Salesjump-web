<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quiz_Result_Daywise.aspx.cs" Inherits="MIS_Reports_Quiz_Result_Daywise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quiz Result Daywise</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

    <script type="text/javascript">
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

            $(document).on('click', ".retpro", function () {
                var sfn = $(this).attr('sfname');
                var dtt = $(this).attr('dt');
                dtt = dtt.split(' ');
                var sfc = $(this).attr('sfcode');
                //var Order_date = document.getElementById("Label2").innerHTML.split(':')[1].trim().split('-').reverse().join('-');

                strOpen = "Rpt_Quiz_Results.aspx?QDate=" + dtt[0] + "&SF=" + sfc + "&SName=" + sfn
                window.open(strOpen, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
            })

        });
    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>

                        <td width="60%" align="center">
                            <asp:Label ID="lblHead" Text="Primary Order View" Font-Bold="true" Style="color: #3F51B5; padding-right: 217PX;" Font-Size="Large" Font-Underline="true"
                                runat="server"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click" />
                                        <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf" Visible="false" />
                                        <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                            OnClick="btnExcel_Click" />
                                        <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%" Style="margin-left: 35px">
            <table border="0" id='1' style="margin: auto" width="100%">
                <tr>
                    <td>&nbsp;&nbsp;</td>
                </tr>
                <tr align="left">
                    <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus; padding-left: 180px;">FieldForce:
                   <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" Style="font-family: Andalus; color: Blue;"></asp:Label></td>
                </tr>
                <tr>
                    <td width="100%">
                        <asp:GridView ID="gdprimary" runat="server" ShowHeader="false"
                            HorizontalAlign="Center" Width="90%" Font-Names="andalus" OnRowCreated="OnRowCreated" OnDataBound="OnDataBound"
                            BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                            AutoGenerateColumns="true" class="newStly"
                            HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                            <Columns>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>