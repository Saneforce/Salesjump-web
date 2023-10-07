<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptattendancefinal.aspx.cs"
    Inherits="MIS_Reports_rptattendancefinal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance View</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <%-- <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        
        .remove
        {
            text-decoration: none;
        }
        .ttable tr:nth-child(odd)
        {
            background-color: #dbe2f9;
        }
        .ttable td
        {
            padding: 5px 2px;
            width: 14px;
            text-align: justify;
            border: solid 1px black;
        }
        
        .ttable th
        {
            padding: 4px 2px;
            color: #fff;
            background: #819DFB url(Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }
        
        
        .ttable table
        {
            overflow: hidden;
        }
        
        .ttable tr:hover
        {
            background-color: #ffa;
        }
        
        .ttable td, th
        {
            position: relative;
        }
        .ttable td:hover::after, .ttable th:hover::after
        {
            content: "";
            background-color: #ffa;
            left: 0;
            top: -5000px;
            height: 10000px;
            width: 100%;
            z-index: -1;
        }
    </style>--%>
    <script type="text/javascript">
        $(document).on('click', "#btnExcel", function (e) {

            //creating a temporary HTML link element (they support setting file names)
            //            var a = document.createElement('a');
            //            //getting data from our div that contains the HTML table
            //            var data_type = 'data:application/vnd.ms-excel';
            //            $('.hillbillyForm').remove();
            //            var table_div = document.getElementById('pnlContents');
            //            var table_html = table_div.outerHTML.replace(/ /g, '%20');
            //            a.href = data_type + ', ' + table_html;
            //            //setting the file name
            //            a.download = 'AttendanceReport.xls';
            //            //triggering the function
            //            a.click();
            //            //just in case, prevent default behaviour
            //            e.preventDefault();

            //  window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=pnlContents]').html()));
            //  e.preventDefault();



//            var a = document.createElement('a');
//            var data_type = 'data:application/vnd.ms-excel';
//            a.href = data_type + ', ' + encodeURIComponent($('div[id$=pnlContents]').html());
//            a.download = 'AttendanceReport.xls';
//            a.click();
//            e.preventDefault();


            var a = document.createElement('a');

            var fileName = 'Test file.xls';
            var blob = new Blob([$('div[id$=pnlContents]').html()], {
                type: "application/csv;charset=utf-8;"
            });
            document.body.appendChild(a);
            a.href = URL.createObjectURL(blob);
            a.download = 'AttendanceReport.xls';
            a.click();
            e.preventDefault();


        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var tcData = [];



            var Fyear = $("#<%=hdnYear.ClientID%>").val();
            var FMonth = $("#<%=hdnMonth.ClientID%>").val();
            var hdnDiv_Code = $("#<%=hdnDiv.ClientID%>").val();
            var hty = $("#<%=htype.ClientID%>").val();


            var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var stw = '';
            var len = $('#gvtotalorder tr:first th').length;
            var exta = 0;
            if (hdnDiv_Code == "20") {
                exta = 2;
            }

            var cn = 4;
            if (hty == 'Maximised') {
                cn = 5;
            }

            $('#gvtotalorder tr:first th').each(function (n) {
                if ($(this).index() > cn && $(this).index() < len - exta) {
                    //str = FMonth +'/'
                    //'08/11/2015'
                    var a = new Date('' + FMonth + '/' + ($(this).index() - cn) + '/' + Fyear + '');

                    //   console.log($(this).text() + ':' + a + ":" + weekday[a.getDay()]);
                    stw += '<th>' + weekday[a.getDay()].substring(0, 3); +'</th>';
                }
            });

            $('#gvtotalorder tr:first').after('<tr>' + stw + '</tr>');

            for (var ll = 0; ll < cn + 1; ll++) {
                $('#gvtotalorder tr:first').find('th').eq(ll).attr('rowspan', 2)
            }




            if (hdnDiv_Code == "20") {
                $('#gvtotalorder tr:first').find('th').eq(len - 1).attr('rowspan', 2)
                $('#gvtotalorder tr:first').find('th').eq(len - 2).attr('rowspan', 2)
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptattendancefinal.aspx/GetTcEcDetails",
                    data: "{'FYear':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        // console.log(data.d);
                        tcData = JSON.parse(data.d);
                        //genReport();
                    },
                    error: function (err) {
                        console.log(err);
                    }
                    //            error: function (jqXHR, exception) {
                    //                console.log(jqXHR);
                    //                console.log(exception);
                    //                var msg = '';
                    //                if (jqXHR.status === 0) {
                    //                    msg = 'Not connect.\n Verify Network.';
                    //                } else if (jqXHR.status == 404) {
                    //                    msg = 'Requested page not found. [404]';
                    //                } else if (jqXHR.status == 500) {
                    //                    msg = 'Internal Server Error [500].';
                    //                } else if (exception === 'parsererror') {
                    //                    msg = 'Requested JSON parse failed.';
                    //                } else if (exception === 'timeout') {
                    //                    msg = 'Time out error.';
                    //                } else if (exception === 'abort') {
                    //                    msg = 'Ajax request aborted.';
                    //                } else {
                    //                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    //                }
                    //                alert(msg);
                    //            }
                });

                $('#gvtotalorder tr').each(function (n) {

                    if ($(this).index() > 1) {

                        var cRow = $(this).find('td').length;
                        var toCol = $('#gvtotalorder tr:first').find('th').length;
                        //  console.log(cRow);
                        if (Number(cRow) == toCol) {

                            var sfCodes = $(this).find('td').eq(0).text() || '0';

                            for (var i = 0; i < tcData.length; i++) {

                                if (sfCodes == tcData[i].sf_code) {
                                    //  console.log(sfCodes + ":" + tcData[i].sf_code);
                                    //  console.log($(this).find('td:first').attr('rowspan') || 0);
                                    var rowL = ($(this).find('td:first').attr('rowspan') || 0);
                                    if (Number(rowL) > 0) {
                                        var tot = 0;
                                        var str = '<td>DA</td><td></td><td></td>';
                                        // console.log(Object.keys(tcData[i]).length);
                                        for (var j = 0; j < Object.keys(tcData[i]).length - 3; j++) {
                                            //stk ='.' + (Number(j) + [1]);
                                            str += '<td>' + (tcData[i]["" + (j + 1) + ""] || 0) + '</td>';
                                            tot += Number((tcData[i]["" + (j + 1) + ""] || 0));

                                        }
                                        if (Number(rowL) == 1) {
                                            $(this).after('<tr>' + str + '<td>' + tcData[i].monthAllowance + '</td><td>' + tcData[i].SplAllowance + '</td></tr>');

                                        }
                                        else if (Number(rowL) == 2) {
                                            $(this).next('tr').after('<tr>' + str + '<td>' + tcData[i].monthAllowance + '</td><td>' + tcData[i].SplAllowance + '</td></tr>');

                                        }
                                        else if (Number(rowL) == 3) {
                                            $(this).next('tr').next('tr').after('<tr>' + str + '<td>' + tcData[i].monthAllowance + '</td><td>' + tcData[i].SplAllowance + '</td></tr>');

                                        }
                                        else if (Number(rowL) == 4) {

                                            $(this).next('tr').next('tr').next('tr').after('<tr>' + str + '<td>' + tcData[i].monthAllowance + '</td><td>' + tcData[i].SplAllowance + '</td></tr>');

                                        }

                                        $(this).find('td:first').attr('rowspan', Number(rowL) + 1)
                                        $(this).find('td').eq(1).attr('rowspan', Number(rowL) + 1)
                                        $(this).find('td').eq(2).attr('rowspan', Number(rowL) + 1)

                                    }
                                }
                            }
                        }
                    }
                });

            }
            var rows = $('#gvtotalorder tr:first').length;
            var columns = $('#gvtotalorder  tr:first th').length;
            var table = document.getElementById('<%=gvtotalorder.ClientID %>');
            //            alert(table.columns.length);

            var row = table.insertRow(table.rows.length);



            $('#gvtotalorder tr:first th').each(function (n) {


                //                alert(($('#Viewalldcrdates1 tr').length - 1));
                var remove = 0;
                //select all tds in this column
                var tds1 = $(this).parents('table').find('tr td:nth-child(' + (n + 1) + ')');
                var tds2 = $(this).parents('table').find('tr td:nth-child(' + (n + 1) + ') .uu');
                tds2.each(function (k) {


                    $(this).text(sum);

                });
                //  
                //                alert(tds1.length);
                //                tds[tds1.length].text(sum);
                //                 console.log(tds1.text());

                var count = 0;
                var sum = 0;
                tds1.each(function (j) {
                    count = count + 1;
                    //                    var val = $(this).find('.total');
                    //                                        if ($(this).find('.total')) {
                    //                                            $(this).css('background-color', 'green');
                    //                                            count = count + 1;
                    //                                            //                        alert('ee');
                    //                                        }
                    //                                        var ratingTdText = $(this).parents('table').find('tr td.uu').text();
                    //                                        if (ratingTdText == "") {
                    //                                            $(this).css('background-color', 'green');
                    //                                        }
                    if ($(this).html().match(/^\s*\d[\d,\.]*\s*$/)) {

                        //                        alert($(this).find('td:last-child .uu').css('background-color', 'green').text());
                        //                        $(this).find('.uu').css('background-color', 'green');
                       if ($(this).closest('tr').text().indexOf('value') > -1) {
                            sum += parseFloat($(this).html());
                        }

                        //                        console.log(j);
                        // TODO: something cool
                    }

                    //                    tds1.append('<td>'+sum+'</td>');

                    //                    alert(n);
                    //                   


                });
                if (n == '1') {
                    var title = 'Total';

                    var cell = row.insertCell(0);
                    cell.style.textAlign = 'Center'
                    cell.style.fontWeight = 'bold'
                    cell.setAttribute("colspan", 5);
                    var dtitle = document.createTextNode(title);
                    cell.appendChild(dtitle);
                }
                else if (n == '0' || n == columns - 1 || n == columns - 2 || n == columns - 3) {
                    var cell3 = row.insertCell(n);
                    cell3.style.textAlign = 'Center'
                    cell3.style.fontWeight = 'bold'
                    cell3.setAttribute('class', 'hillbillyForm');
                    var debitsum = document.createTextNode(sum);
                    cell3.appendChild(debitsum);
                    cell3.style.display = "none";
                }
                else {

                    //  console.log(n + 'hh' + sum);
                    var cell3 = row.insertCell(n);
                    cell3.style.textAlign = 'Center'
                    cell3.style.fontWeight = 'bold'
                    cell3.setAttribute('class', 'uu');
                    var debitsum = document.createTextNode(sum.toFixed(2));
                    cell3.appendChild(debitsum);
                }
                //                cell3.appendChild(sum);
                //                tds2[n+1].innerhtml=parseInt(sum);

                //                alert($(this).find(':last-child').text());

                //                }

                //                console.log(sum);
                sum = 0;


            });

            $('#gvtotalorder tr').each(function (n) {

                var rsum = 0;
                $(this).find("td").each(function (colindex) {

                    if (colindex > 6) {
                        if ($(this).html().match(/^\s*\d[\d,\.]*\s*$/)) {
                            rsum += parseFloat($(this).html());
                        }
                    }
                });
                if (n == 0) {
                    $(this).append('<th rowspan="2">' + 'Total' + '</th>');
                    // $(this).css('background-color', 'Orange');
                }
                else {
                    if ($(this).index() > 1) {
                        $(this).append('<td style="fontWeight:bold;">' + (rsum.toFixed(2)) + '</td>');
                    }
                }

            });

            //            alert(rows + 'hg' + columns);
            //                tds.innerHTML = tds.innerHTML.replace(/&nbsp;/g, '');

            //check if all the cells in this column are empty
            //                if (tds1.length == tds1.filter(':empty').length) {
            //                    //hide header
            //                    $(this).hide();
            //                    //hide cells
            //                    tds1.hide();
            //                }







        });
           
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnYear" runat="server" />
    <asp:HiddenField ID="hdnMonth" runat="server" />
    <asp:HiddenField ID="hdnDiv" runat="server" />
    <asp:HiddenField ID="htype" runat="server" />
    <br />
    <asp:Panel ID="pnlbutton" runat="server">
        <div class="container" style="max-width: 100%; width: 90%">
            <div class="row" style="text-align: right">
                <asp:Label ID="lbltot" runat="server" />
                <asp:LinkButton ID="btnPrint" runat="server" OnClick="btnPrint_Click" class="btn btnPrint" />
                <asp:LinkButton ID="btnExcel" runat="server" class="btn btnExcel" />
                <asp:LinkButton ID="btnClose" runat="server" href="javascript:window.open('','_self').close();"
                    class="btn btnClose" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlContents" runat="server">
        <div class="container" style="max-width: 100%; width: 90%">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblHead" SkinID="lblMand" Style="font-weight: bold; font-size: 16pt;
                        color: black; font-family: Arial; float: left; padding: 5px;" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true" Font-Underline="true"
                        ForeColor="#476eec" runat="server"></asp:Label>
                    <asp:Label ID="lblsf_name" runat="server" Font-Underline="true" SkinID="lblMand"></asp:Label>
                    <asp:Image ID="logoo" runat="server" Style="width: 28%; border-width: 0px; height: 39px;
                        float: right"></asp:Image>
                </div>
            </div>
        </div>
        <div class="container" style="max-width: 100%; width: 90%">
            <asp:GridView ID="gvtotalorder" runat="server" Width="100%" Font-Size="10pt" OnPreRender="gridView_PreRender"
                CssClass="newStly" HorizontalAlign="Center" ShowFooter="true" BorderWidth="1px"
                CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="true"
                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid"
                OnRowDataBound="gvtotalorder_RowDataBound">
                <Columns>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
            <%-- <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>--%>
            <asp:GridView ID="GridView1" runat="server" Width="100%" Font-Size="10pt" OnPreRender="gridView_PreRender"
                CssClass="ttable" HorizontalAlign="Center" HeaderStyle-BackColor="#819dfb" ShowFooter="true"
                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="true"
                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid"
                OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
            <%-- <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>--%>
        </div>
    </asp:Panel>
    <asp:Label ID="lbl12" runat="server"></asp:Label>
    </form>
</body>
</html>
