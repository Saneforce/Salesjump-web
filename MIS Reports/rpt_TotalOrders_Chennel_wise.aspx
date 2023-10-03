<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_TotalOrders_Chennel_wise.aspx.cs" Inherits="MIS_Reports_rpt_TotalOrders_Chennel_wise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Channel wise Order Details</title>
   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
	
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }


		/*#Product_Table tbody tr td, #Product_Table tfoot tr th
		{
			text-align:left;
		}

		#Product_Table tbody tr td:nth-child(1)
		{
			text-align:left;
		}
		#Product_Table tfoot tr th:nth-child(1)
		{
			text-align:center;
		}*/
    </style>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        //$('form').live("submit", function () {
        //    ShowProgress();
        //});
    </script>
    <script type="text/javascript">


        $(document).ready(function () {


            dProd = []; dPQV = []; lstdrP = [];
            genReport = function () {
                //if (dProd.length > 0 && dPQV.length > 0) {

                str = ''
                for (var i = 0; i < lstdrP.length; i++) {
                    str += '<tr><td><input type="hidden" name="Channel" value="' + lstdrP[i].ListedDrCode + '"/> ' + lstdrP[i].ListedDr_Name + '</td><td>' + lstdrP[i].Territory_Name + ' </td><td>' + lstdrP[i].DistName + '</td><td>' + lstdrP[i].SFName + '</td>'; //<p name="sfname" style="margin: 0 0 0px;">'</p>

                    var TotVal = 0;
                    var TotNetW = 0;
                    var TotQty = 0;
                    //  console.log(dSF[i].sfCode);
                    for (var j = 0; j < dProd.length; j++) {
                        var q = "", v = "", n = "";
                        fP = dPQV.filter(function (a) { return (a.Product_Code == dProd[j].Product_Code && a.ListedDrCode == lstdrP[i].ListedDrCode && a.SFCode == lstdrP[i].SFCode && a.DistCode == lstdrP[i].DistCode); });
                        // console.log(fP);
                        if (fP.length > 0) {
                            q = fP[0].quantity, v = fP[0].value, n = fP[0].net_weight_value;
                            TotVal += Number(fP[0].value);
                            TotQty += Number(fP[0].quantity);
                            TotNetW += Number(fP[0].net_weight_value);
                        }
                        str += '<td align="right">' + q + '</td><td align="right">' + ((v != "") ? parseFloat(v).toFixed(2) : "") + '</td><td align="right">' + ((n != "") ? parseFloat(n).toFixed(2) : "") + '</td>';
                    }
                    // console.log(parseFloat(TotVal).toFixed(2));
                    str += '<td align="right">' + parseFloat(TotQty).toFixed(0) + '</td><td align="right">' + parseFloat(TotVal).toFixed(2) + '</td><td align="right">' + parseFloat(TotNetW).toFixed(2) + '</td></tr>';
                    // }
                }

                $('#btnexcel').show();
                $('#Product_Table tbody').append(str );
            }



            $('#Product_Table tr').remove();
            var len = 0;
            /* $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "rpt_TotalOrders_Chennel_wise.aspx/getdata",
            dataType: "json",
            success: function (data) {
            len = data.d.length;
            dProd = data.d;
            genReport();
            if (data.d.length > 0) {
            str = '<th style="min-width:200px;" rowspan="2"> <p style="margin: 0 0 0px;">Retailer Name</p> </th><th style="min-width:200px;" rowspan="2"> <p style="margin: 0 0 0px;">Route Name</p></th> <th style = "min-width:200px;" rowspan = "2" > <p style="margin: 0 0 0px;">Contact Name</p> </th >';
            str1 = '';
            strff = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Total</p> </th>';
            for (var i = 0; i < data.d.length; i++) {

            str += '<th style="min-width:100px; " colspan="2"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].product_name + '</p> </th>';
            str1 += '<th>Quantity</th><th>Value</th><th>Net Weight</th>';
            strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';

            }

            $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="5">Total</th></tr>');
            $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>QTY</th><th>Values</th><th>Net Weight</th>');
            $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th><th></th><th></th><th></th></tr>');
            }
            },
            error: function (jqXHR, exception) {

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
            */

            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var chl = $("#<%=Channal.ClientID%>").val();



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_TotalOrders_Chennel_wise.aspx/getIssuData",
                data: "{'sf_code':'" +sf_code+ "','FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'chl':'" + chl + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));ListedDrCode
                    dPQV = data.d;
                    pc = ''; lstr = '';
                    dProd = dPQV.filter(function (a) {
                        if ((',' + pc).indexOf(',' + a.Product_Code + ',') < 0)
                        { pc += a.Product_Code + ','; return true } else { return false }
                    });
                    lstdrP = dPQV.filter(function (a) {
                        if ((',' + lstr ).indexOf(',' + a.ListedDrCode+'-'+ a.SFCode +'-'+ a.DistCode + ',') < 0)
                        { lstr += a.ListedDrCode + '-' + a.SFCode + '-' + a.DistCode + ','; return true } else { return false }
                    });
                    if (dProd.length > 0) {
                        str = '<th style="min-width:200px;" rowspan="2"> <p style="margin: 0 0 0px;">Retailer Name</p> </th><th style="min-width:200px;" rowspan="2"> <p style="margin: 0 0 0px;">Route Name</p></th> <th style = "min-width:200px;" rowspan = "2" > <p style="margin: 0 0 0px;">Distributor</p> </th ><th rowspan="2" style="min-width:200px;">Order Taken By</th>';
                        str1 = '';
                        strff = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Total</p> </th>';
                        for (var i = 0; i < dProd.length; i++) {

                            str += '<th style="min-width:100px;" colspan="3"><input type="hidden" name="pcode" value="' + dProd[i].Product_Code + '"/> <p name="pname" style="margin: 0 0 0px;">' + dProd[i].Product_Name + '</p> </th>';
                            str1 += '<th>Quantity</th><th>Value</th><th>Net Weight</th>';
                            strff += '<th style="text-align: right" ></th><th style="text-align: right"></th><th style="text-align: right"></th>';

                        }

                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="5">Total</th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>QTY</th><th>Values</th><th>Net Weight</th>');
                        // $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th><th></th><th></th><th></th></tr>');
                    }
                    genReport();

                },
                error: function (jqXHR, exception) {
                    //alert(JSON.stringify(result));
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
                            $(this).text(parseFloat(arr[i - 1]));
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

            /*$('.secondhead th').each(function (i) {
            var remove = 0;

            var tds = $(this).parents('table').find('tr td:nth-child(' + (i + 2) + ')')


            tds.each(function (j) {
            if (this.innerHTML == '') remove++;
            });
            if (remove == ($('#Product_Table tbody tr').length)) {
            $(this).hide();
            tds.hide();
            $('#Product_Table tfoot tr th').eq($(this).index() + 1).hide();
            //  if (i > 0) {
            if (i % 2 == 0) {
            $('.mainhead th').eq((i / 2) + 1).hide();
            }
            //}
            }
            });*/
            //sortTable();





            $(document).on('click', "#btnClose", function () {
                window.close();
            });
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
                a.download = 'Employee_wise_Order_Details_' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

        });


        function sortTable() {
            var table, rows, switching, i, x, y, shouldSwitch;
            table = document.getElementById("Product_Table");
            switching = true;
            /*Make a loop that will continue until
            no switching has been done:*/
            while (switching) {
                //start by saying: no switching is done:
                switching = false;
                rows = table.rows;
                /*Loop through all table rows (except the
                first, which contains table headers):*/
                for (i = 1; i < (rows.length - 1); i++) {
                    //start by saying there should be no switching:
                    shouldSwitch = false;
                    /*Get the two elements you want to compare,
                    one from current row and one from the next:*/
                    var colLen = rows[i].cells.length;
                    x = rows[i].getElementsByTagName("TD")[colLen - 3] || 0;
                    y = rows[i + 1].getElementsByTagName("TD")[colLen - 3] || 0;
                    //check if the two rows should switch place:
                    if (Number(x.innerHTML) < Number(y.innerHTML)) {
                        //if so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
                if (shouldSwitch) {
                    /*If a switch has been marked, make the switch
                    and mark that a switch has been done:*/
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                }
            }
        }


    </script>
</head>
<body>
    <form id="form" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="Channal" runat="server" />
    <div class="row">
        <div class="col-sm-8" style="padding-left: 5px;">
            <asp:Label ID="Label2" runat="server" Text="Employee wise Order Details " Style="font-weight: bold;
                font-size: x-large"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
			<a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
    </div>
    <div class="row">
        <br />
        <asp:Label ID="Label1" runat="server" Text="" Style="padding-left: 5px;"></asp:Label>
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
