<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Fieldforce_sales_analysis_days.aspx.cs" Inherits="MIS_Reports_rpt_Fieldforce_sales_analysis_days" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer </title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var vsDtl = [];
            var rpDtl = [];
            var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
            var fYears = $('#<%=hidnYears.ClientID%>').val();
            var fMonths = $('#<%=hidnMonths.ClientID%>').val();


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Fieldforce_sales_analysis_days.aspx/GetVisitedDtls",
                data: "{'SF_Code':'" + sfCode + "', 'FYears':'" + fYears + "', 'FMonths':'" + fMonths + "'}",
                dataType: "json",
                success: function (data) {
                    //data.d;
                    // console.log(data.d);


                    obj = JSON.parse(data.d);

                    vsDtl = obj.rVisit;
                    rpDtl = obj.rOrder;

                    
                    setVisitedDtls();

                    //                    for (var i = 0; i < data.d.length; i++) {
                    //                        $('#<%=GV_DATA.ClientID%> tr').each(function () {
                    //                            // console.log(data.d[i].custCode + ':' + $(this).find('td').eq(0).text());

                    //                            if (data.d[i].custCode == $(this).find('td').eq(0).text()) {
                    //                                $(this).find('td').eq(4).text('Yes');
                    //                                if (Number($(this).find('td').eq(Number(data.d[i].months) + 4).text()) <= 0) {
                    //                                    $(this).find('td').eq(Number(data.d[i].months) + 4).css('background-color', '#8ebf8e');
                    //                                }

                    //                            }
                    //                        });
                    //                    }
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


            function setVisitedDtls() {
                if (vsDtl.length > 0) {

                    $('#<%=GV_DATA.ClientID%> tr').each(function () {
                        var mRow = $(this);
                        // console.log(data.d[i].custCode + ':' + $(this).find('td').eq(0).text());
                        var cCode = $(this).find('td').eq(0).text();
                        vD = vsDtl.filter(function (a) {
                            return (a.custCode == cCode)
                        });
                       
                        if (vD.length > 0) {
                            $(vD).each(function (index, D) {
                                if (vD.length == 1) {
                                    $(mRow).find('td').eq(5).text('OV');
                                }
                                else if (vD.length > 1) {
                                    $(mRow).find('td').eq(5).text('RV');
                                }
                                if (Number($(mRow).find('td').eq(Number(D.months) + 5).text()) <= 0) {
                                    $(mRow).find('td').eq(Number(D.months) + 5).css('background-color', '#8ebf8e');
                                    $(mRow).find('td').eq(Number(D.months) + 5).text(Number($(mRow).find('td').eq(Number(D.months) + 5).text()).toFixed(2));
                                }
                            });
                        }

                        rD = rpDtl.filter(function (a) {
                            return (a.custCode == cCode)
                        });

                        if (rD.length > 0) {
                            if (rD.length == 1) {
                                $(mRow).find('td').eq(5).text('OR');
                            }
                            else if (rD.length > 1) {
                                $(mRow).find('td').eq(5).text('RO');
                            }

                            $(rD).each(function (index, D) {
                                
                                if (Number(D.counts) > 1) {
                                    $(mRow).find('td').eq(Number(D.months) + 5).css('background-color', '#a4bef9');
                                    $(mRow).find('td').eq(Number(D.months) + 5).text(Number($(mRow).find('td').eq(Number(D.months) + 5).text()).toFixed(2));
                                }
                            });
                        }



                    });

                }
            }

            $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divExcel').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'Customer_Sales_Analysis_Month.xls';
                a.click();
                e.preventDefault();

            });

        });


        function generate() {
            var doc = new jsPDF('l', 'mm', [500, 200]);
            var res = doc.autoTableHtmlToJson(document.getElementById("GV_DATA"));
            var el = document.getElementById("lblsf_name");
            var sfName = (el.innerText || el.textContent);
            el = document.getElementById("lblyear");
            var Years = (el.innerText || el.textContent);
            doc.text("Customerwise Sales Analysis for " + sfName + " in Year of - " + Years, 50, 10);
            doc.autoTable(res.columns, res.data, {
                margin: { top: 15 },
                addPageContent: function (data) {
                }
            });

            var pageCount = doc.internal.getNumberOfPages();
            for (i = 0; i < pageCount; i++) {
                doc.setPage(i);
                doc.text(15, 10, doc.internal.getCurrentPageInfo().pageNumber + "/" + pageCount);
            }
            doc.save("Customer_Sales_Analysis_Month.pdf");
        }
         
    </script>
    <style type="text/css">
        body
        {
            padding: 10px;
        }
        .mGrid td, .mGrid th
        {
           
            padding: 2px 8px;
        }
        #GV_DATA tr td:first-child, #GV_DATA tr th:first-child
        {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 95%; border-collapse: collapse; margin: 0px auto;">
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="60%" align="center">
                        <asp:Label ID="lblHead" Text="Purchase Register-Distributor Wise" SkinID="lblMand"
                            Font-Bold="true" Visible="false" Font-Underline="true" runat="server" />
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                                        OnClick="btnPrint_Click" />
                                    <a id="btnExport" style="padding: 0px 20px;" class="btn btnPdf" onclick="generate()" />
                                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
                                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                        class="btn btnClose"></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div id="divExcel">
            <center>
                <br />
                <div>
                    FIELDFORCE WISE SALES ANALYSIS REPORT of - <b>
                        <asp:Label ID="lblyear" runat="server"></asp:Label></b></div>
                <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidn_sf_code" runat="server" />
                        <asp:HiddenField ID="hidnYears" runat="server" />
                        <asp:HiddenField ID="hidnMonths" runat="server" />
                    </b>
                </div>
                <div>
                </div>
                <div>
                    <asp:GridView ID="GV_DATA" runat="server" Width="95%" HorizontalAlign="Center" BorderWidth="1"
                        GridLines="Both" class="newStly" OnRowDataBound="Dgv_SKU_RowDataBound" ItemStyle-HorizontalAlign="Right">
                        <RowStyle HorizontalAlign="Right" />
                    </asp:GridView>
                </div>
            </center>
        </div>
    </asp:Panel>
    </form>
</body>
</html>