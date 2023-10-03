<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptEmployeewiseCategoryDetails.aspx.cs" Inherits="MIS_Reports_rptEmployeewiseCategoryDetails" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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


		#Product_Table tbody tr td, #Product_Table tfoot tr th
		{
			text-align:right;
		}

		#Product_Table tbody tr td:nth-child(1)
		{
			text-align:left;
		}
		#Product_Table tfoot tr th:nth-child(1)
		{
			text-align:center;
		}
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        $('form').live("submit", function () {
            ShowProgress();
        });       
    </script>
    <script type="text/javascript">


        $(document).ready(function () {


            dProd = []; dPQV = []; dSF = []; dTcEc = [];
            genReport = function () {
                if (dSF.length > 0 && dProd.length > 0 && dPQV.length > 0) {
                    console.log(dPQV);
                    for (var i = 0; i < dSF.length; i++) {
                        str = '<td><input type="hidden" name="sfcode" value="' + dSF[i].sfCode + '"/> ' + dSF[i].sfName + ' </td><td> ' + dSF[i].sf_emp_id + ' </td>'; //<p name="sfname" style="margin: 0 0 0px;">'</p>
                        //var tc = 0, ec = 0;
                        //fP = dTcEc.filter(function (a) { return (a.sfCode == dSF[i].sfCode); });
                        //if (fP.length > 0) {
                        //    tc = fP[0].TC_Count, ec = fP[0].EC_Count;
                        //}
                        var TotVal = 0;
                        var TotNetW = 0;
                        var TotQty = 0;
                      //  console.log(dSF[i].sfCode);
                        for (var j = 0; j < dProd.length; j++) {
                            var q = "", v = "", n = "";
                            fP = dPQV.filter(function (a) { return (a.sfCode == dSF[i].sfCode && a.categorycode == dProd[j].categoryId); });
                           // console.log(fP);
                            if (fP.length > 0) {
                                q = fP[0].Quantity, v = fP[0].amount, n = fP[0].netWeight;
                                TotVal += Number(fP[0].amount);
                                TotQty += Number(fP[0].Quantity);
                                TotNetW += Number(fP[0].netWeight);
                            }
                            str += '<td>' + q + '</td><td>' + ((v != "") ? parseFloat(v).toFixed(2) : "") + '</td>';
                        }
                        // console.log(parseFloat(TotVal).toFixed(2));
                        str += '<td>' + parseFloat(TotQty).toFixed(0) + '</td><td>' + parseFloat(TotNetW).toFixed(2) + '</td><td>' + parseFloat(TotVal).toFixed(2) + '</td>';
                        $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                    }
                    $('#btnexcel').show();
                }
            }

            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }


            $('#Product_Table tr').remove();
            var len = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptEmployeewiseCategoryDetails.aspx/getcategorys",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dProd = data.d;
                    genReport();
                    if (data.d.length > 0) {
                        str = '<th style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Field Force</p> </th><th style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Employee ID</p> </th>';
                        str1 = '';
                        strff = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Total</p> </th><th></th>';
                        for (var i = 0; i < data.d.length; i++) {

                            str += '<th style="min-width:100px; " colspan="2"> <input type="hidden" name="pcode" value="' + data.d[i].categoryId + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].categoryName + '</p> </th>';
                            str1 += '<th>Quantity</th><th>Value</th>';
                            strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';

                        }

                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="3">Total</th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>QTY</th><th>Net Weight</th><th>Values</th></tr>');
                        $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th><th></th></tr>');
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



            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var subDiv = $("#<%=SubDivCode.ClientID%>").val();



            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    async: false,
            //    url: "rptEmployeewiseorderDetails.aspx/getIssuDataTcEc",
            //    data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
            //    dataType: "json",
            //    success: function (data) {
            //        dTcEc = data.d;
            //        console.log(dTcEc);
            //    },
            //    error: function (jqXHR, exception) {
            //        //alert(JSON.stringify(result));
            //        var msg = '';
            //        if (jqXHR.status === 0) {
            //            msg = 'Not connect.\n Verify Network.';
            //        } else if (jqXHR.status == 404) {
            //            msg = 'Requested page not found. [404]';
            //        } else if (jqXHR.status == 500) {
            //            msg = 'Internal Server Error [500].';
            //        } else if (exception === 'parsererror') {
            //            msg = 'Requested JSON parse failed.';
            //        } else if (exception === 'timeout') {
            //            msg = 'Time out error.';
            //        } else if (exception === 'abort') {
            //            msg = 'Ajax request aborted.';
            //        } else {
            //            msg = 'Uncaught Error.\n' + jqXHR.responseText;
            //        }
            //        alert(msg);
            //    }
            //});


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptEmployeewiseCategoryDetails.aspx/getSalesforce",
                data: "{'SF_Code':'" + sf_code + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    dSF = data.d;
                   
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




            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptEmployeewiseCategoryDetails.aspx/getcetegoryorderData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    dPQV = data.d;
                    genReport();

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



            var arr = [];
            $('#Product_Table tbody tr').each(function () {
                var i = 0;
                console.log($(this).find('td').length);
                $(this).find('td').each(function () {
                    if (i != 0 && i!=1) {
                        arr[i - 1] = Number(arr[i - 1] || 0) + Number($(this).text() || 0);
                    }
                    i++;
                });
            });
             console.log(arr);

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
                                $(this).text(isNaN((parseFloat(arr[i - 1]))) ? "" : parseFloat(arr[i - 1]));
                            }
                        }
                    }

                }
                i++;
            });

            $('.secondhead th').each(function (i) {
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
            });
            sortTable();





            $(document).on('click', "#btnClose", function () {
                window.close();
            });




            $(document).on('click', "#btnExcel", function (e) {

                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };

                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };

                htmls = document.getElementById("content").innerHTML;

                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }


                var link = document.createElement("a");


                var tets = 'Employee_wise_category_Details_' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                

                //var dt = new Date();
                //var day = dt.getDate();
                //var month = dt.getMonth() + 1;
                //var year = dt.getFullYear();
                //var postfix = day + "_" + month + "_" + year;
                ////creating a temporary HTML link element (they support setting file names)
                //var a = document.createElement('a');
                ////getting data from our div that contains the HTML table
                //var data_type = 'data:application/vnd.ms-excel';
                //var table_div = document.getElementById('content');
                //var table_html = table_div.outerHTML.replace(/ /g, '%20');
                //a.href = data_type + ', ' + table_html;
                ////setting the file name
                //a.download = 'Employee_wise_category_Details_' + postfix + '.xls';
                ////triggering the function
                //a.click();
                //just in case, prevent default behaviour
                //e.preventDefault();
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
                for (i = 1; i < (rows.length - 1) ; i++) {
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
    <asp:HiddenField ID="SubDivCode" runat="server" />
         <div class="row">
             <div class="col-sm-8" style="padding-left: 5px;"></div>
               <div class="col-sm-4" style="text-align: right">
              <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
                         <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                     </div>

         </div>

        <div  id="content">
		<div class="row">		
			<div class="col-sm-8" style="padding-left: 5px;">
				<asp:Label ID="Label2" runat="server" Text="Employee wise category Details " Style="font-weight: bold;
					font-size: x-large"></asp:Label>
			</div>
		</div>
		<div class="row">
			<br />
			<asp:Label ID="Label4" runat="server" Text="Label" Style="padding-left: 5px;"></asp:Label>
			<br />
			<div>
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
	</div>

    </form>
</body>
</html>
