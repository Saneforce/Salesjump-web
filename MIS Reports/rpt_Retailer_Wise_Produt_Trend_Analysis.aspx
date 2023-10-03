<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Retailer_Wise_Produt_Trend_Analysis.aspx.cs" Inherits="MIS_Reports_rpt_Retailer_Wise_Produt_Trend_Analysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retailer Wise Product Trend Analysis</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>

    <style type="text/css">
        body {
            padding: 10px 25px;
        }

        #Product_Table tbody tr td, #Product_Table tfoot tr th {
            text-align: right;
        }

            #Product_Table tbody tr td:nth-child(1) {
                text-align: left;
            }

            #Product_Table tfoot tr th:nth-child(1) {
                text-align: center;
            }
    </style>
    <script type="text/javascript">


        $(document).ready(function () {

            dProd = []; dPQV = []; dLD = []; dData = []; fP = []; mn = []; mmmm = [];


            var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
            var fYear = $('#<%=hFYear.ClientID%>').val();
            var fMonth = $('#<%=hFMonth.ClientID%>').val();
            var tYear = $('#<%=hTYear.ClientID%>').val();
            var tMonth = $('#<%=hTMonth.ClientID%>').val();
            var SubDiv = $('#<%=subDiv.ClientID%>').val();

            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";

            var fd1 = new Date(fYear + '/' + fMonth + '/' + "01/");
            var td1 = new Date(tYear + '/' + tMonth + '/' + "01/");
            cd1 = fd1;
            var dat1 = 0;

            while (cd1 <= td1) {
                mmmm.push({
                    mnth: month[cd1.getMonth()]
                })
                cd1.setMonth(cd1.getMonth() + 1);
                dat1++;
            }
            console.log(mn);
            console.log(mmmm);


            strbind = '';
            genReport = function () {
                if (dLD.length > 0 && dProd.length > 0 && dData.length > 0) {
                    for (var i = 0; i < dLD.length; i++) {
                        strbind += '<tr><td><label name="sfcode" value="' + dLD[i].ListedDrCode + '"> ' + dLD[i].ListedDr_Name + ' </label></td>';

                        var TotVal = 0;
                        var TotNetW = 0;
                        var TotQty = 0;
                        for (var j = 0; j < dProd.length; j++) {


                            for (var z = 0; z < mmmm.length; z++) {
                                fP = dData.filter(function (a) {
                                    return (a.ListedDrCode == dLD[i].ListedDrCode && a.Mnth == mmmm[z].mnth && a.Product_Code == dProd[j].product_id);
                                });
                                if (fP.length > 0) {
                                    var q = "", v = "", n = "";
                                    strbind += '<td class=' + mmmm[z].mnth + '>' + fP[0].Qty + '</td><td class=' + mmmm[z].mnth + '>' + fP[0].Val + '</td>';

                                    q = fP[0].Qty, v = fP[0].Val;
                                    TotVal += Number(fP[0].Val);
                                    TotQty += Number(fP[0].Qty);
                                    // TotNetW += Number(0);
                                }
                                else {
                                    strbind += '<td class=' + mmmm[z].mnth + '>' + 0 + '</td><td class=' + mmmm[z].mnth + '>' + 0 + '</td>';
                                }
                            }
                        }
                        strbind += '<td>' + TotQty + '</td><td>' + ((TotVal != "") ? parseFloat(TotVal).toFixed(2) : "") + '</td>';
                    }
                    //  strbind += '<td>' + parseFloat(TotQty).toFixed(0) + '</td><td>' + parseFloat(TotVal).toFixed(2) + '</td></tr>';
                    $('#Product_Table tbody').append(strbind);
                }

                $('#btnexcel').show();
            }


            $('#Product_Table tr').remove();
            var len = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Retailer_Wise_Produt_Trend_Analysis.aspx/getdata",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dProd = data.d;
                    var fd = new Date(fYear + '/' + fMonth + '/' + "01/");
                    var td = new Date(tYear + '/' + tMonth + '/' + "01/");
                    cd = fd;
                    genReport();
                    str2 = '';
                    if (data.d.length > 0) {

                        str = '<th style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Product Name</p></th>';
                        str1 = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Retailer Name</p> </th>';
                        strff = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Total</p> </th>';
                        for (var i = 0; i < data.d.length; i++) {
                            var fd = new Date(fYear + '/' + fMonth + '/' + "01/");
                            var td = new Date(tYear + '/' + tMonth + '/' + "01/");
                            cd = fd;
                            var dat = 0;
                            while (cd <= td) {
                                str2 += '<th colspan="2"> ' + month[cd.getMonth()] + ' - ' + cd.getFullYear() + ' </th>';
                                str1 += '<th>QTY</th><th>Value</th>';
                                strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';
                                cd.setMonth(cd.getMonth() + 1);
                                dat++;
                            }
                            str += '<th style="min-width:130px; " colspan=' + (dat * 2) + '><input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].product_name + '</p> </th>';

                        }
                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="2">Total</th></tr>');
                        $('#Product_Table thead').append('<tr class="mainhead1">' + str2 + '<th></th><th></th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>QTY</th><th>Values</th></tr>');
                        $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th></tr>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Retailer_Wise_Produt_Trend_Analysis.aspx/getretailer",
                data: "{'SF_Code':'" + sfCode + "','FY':'" + fYear + "','FMON':'" + fMonth + "','TY':'" + tYear + "','TMON':'" + tMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dLD = data.d;
                    console.log(data.d);
                    genReport();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Retailer_Wise_Produt_Trend_Analysis.aspx/GEt_Retailer_wise_product_analysis",
                data: "{'SF_Code':'" + sfCode + "','FY':'" + fYear + "','FMON':'" + fMonth + "','TY':'" + tYear + "','TMON':'" + tMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dData = JSON.parse(data.d) || [];
                    console.log(data.d);
                    genReport();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });



            var arr = [];
            $('#Product_Table tbody tr').each(function () {
                var i = 0;
                console.log($(this).find('td').length);
                $(this).find('td').each(function () {
                    if (i != 0) {
                        arr[i - 1] = Number(arr[i - 1] || 0) + Number($(this).text() || 0);
                    }
                    i++;
                });
            });

            // console.log(arr);


            var i = 0;
            var leng = $('.trfoot th').length;
			 if ($('#Product_Table tbody tr').length > 0) {
            $('.trfoot th').each(function () {
                if (i != 0) {

                    if (i % 2 == 0) {
                        if (leng - 3 == i) {
                            $(this).text(parseFloat(arr[i - 1]).toFixed(2));
                        }
                        else if (leng - 2 == i) {
                            $(this).text(parseFloat(arr[i - 1]));
                        }
                        else if (leng - 1 == i) {
                            $(this).text(parseFloat(arr[i - 1]).toFixed(2));
                        }
                        else {
                            $(this).text(parseFloat(arr[i - 1]).toFixed(2));
                        }

                    }
                    else {
                        if (leng - 3 == i) {
                            $(this).text(parseFloat(arr[i - 1]).toFixed(2));
                        }
                        else if (leng - 2 == i) {
                            $(this).text(parseFloat(arr[i - 1]));
                        }
                        else if (leng - 1 == i) {
                            $(this).text(parseFloat(arr[i - 1]));
                        }
                        else {

                            if (Number(leng) - 4 == Number(i)) {

                                $(this).text(parseFloat(arr[i - 1]).toFixed(2));
                            }
                            else {
                                $(this).text(parseFloat(arr[i - 1]));
                            }
                        }
                    }

                }
                i++;
            });
			}
			 else {
                $('.trfoot th').each(function () {
                    if (i != 0) {
                        if (i % 2 == 0) {
                            $(this).text("0.00");
                        }
                        else {
                            $(this).text("0");
                        }
                    }
                    i++;
                });
            }



            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('content');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'Retailer_Wise_Product_Trend_Analysis' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

        });



            //sortTable();

    </script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8" style="padding-left: 5px;">
<%--                <asp:Label ID="Label2" runat="server" Style="font-weight: bold; font-size: x-large"></asp:Label>--%>
                 <asp:Label ID="Label1" runat="server" Text="Label" Style="padding-left: 5px; font-size:x-large"></asp:Label>

            </div>

            <div class="col-sm-4" style="text-align: right">
                <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
                <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
            </div>
        </div>
        <div class="row" style="padding: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size:larger"></asp:Label> 
                <asp:Label ID="lblsf_name" runat="server" Style="font-size:larger"></asp:Label>
        </div>
        <div class="row">
            <br />
           
            <br />
            <div id="content">
                <table id="Product_Table" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <%-- <img src="../../../Images/loader.gif" alt="" />--%>
        </div>
    </form>
</body>
</html>
