<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCustomerPaymentDetailList.aspx.cs" Inherits="MIS_Reports_rptCustomerPaymentDetailList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var dtls = [];


            var custCode = $("#<%=HCustCode.ClientID%>").val();
            var FDate = $("#<%=hFYear.ClientID%>").val();
            var TDate = $("#<%=hFMonth.ClientID%>").val();
            var sfName = $("#<%=hsfName.ClientID%>").val();

            var getTable = () => {
            var Tot =0;
                var tbl = $('#tbl');

                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();
                $(tbl).find('tfoot tr').remove();

                var str = `<th>SlNo</th><th>Retailer Name</th><th>Route Name</th><th>Distributor Name</th><th>Amount</th><th>Pay Date</th><th>Pay Mode</th><th>Remark</th> `;

                $(tbl).find('thead').append(`<tr>${str}</tr>`);

                if (dtls.length > 0) {
                    dtls.forEach((item, index, list) => {
                    Tot+= Number(item.Amount || 0);
                        str = `<td>${(index + 1)}</td><td>${item.custName}</td><td>${item.RouteName}</td><td>${item.Distname}</td><td>${item.Amount} </td><td>${item.Pay_Mode} </td><td>${item.Pay_Date} </td><td>${item.Remarks} </td>`;
                        $(tbl).find('tbody').append(`<tr>${str}</tr>`);
                    });
                    str=`<th colspan="2">Total</th><th></th><th></th><th>${Tot}</th><th></th><th></th><th></th>`;
                    $(tbl).find('tfoot').append(`<tr>${str}</tr>`);
                }
                else {
                str=`<td colspan="8"  style="color:red;text-align: center;    font-weight: bold;">No Record Found..!</td>`;
                   $(tbl).find('tbody').append(`<tr>${str}</tr>`);
                }
            };


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptCustomerPaymentDetailList.aspx/GetDetails",
                data: "{'custCode':'" + custCode + "','FDate':'" + FDate + "','TDate':'" + TDate + "'}",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    dtls = data.d;
                    getTable();
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });
             $(document).on('click', "#btnExcel", function (e) {
              //  window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=divExcel]').html()));
               // e.preventDefault();

                var a = document.createElement('a');

                var fileName = 'Test file.xls';
                var blob = new Blob([$('div[id$=divExcel]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'Customerwise_Sales_Analysis.xls';
                a.click();
                e.preventDefault();

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hSubDiv" runat="server" />
        <asp:HiddenField ID="HCustCode" runat="server" />
        <asp:HiddenField ID="hsfName" runat="server" />
             <div class="container" style="max-width: 100%; width: 100%">
         <table width="100%">
                <tr>
                    <td width="60%" >
                        <asp:Label ID="lblHead" Text="" style="font-size:larger"
                            Font-Bold="true"  runat="server" />
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                                                     
                                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
                                    <%--  OnClick="btnExcel_Click"--%>
                                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                        class="btn btnClose"></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <div id="divExcel" class="container" style="max-width: 100%; width: 100%">
        <asp:Label ID="Label1" Text="" style="font-size:medium"
                              runat="server" />
            <table id="tbl" class="newStly" border="1" style="width: 100%">
                <thead></thead>
                <tbody></tbody>
                <tfoot></tfoot>
            </table>
        </div>
    </form>
</body>
</html>
