<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptEmp_Month_call.aspx.cs"
    Inherits="MIS_Reports_rptEmp_Month_call" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Monthly call Details</title>
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
		#Product_Table tbody tr td:nth-child(2)
		{
			text-align:left;
		}
		#Product_Table tbody tr td:nth-child(3)
		{
			text-align:left;
		}
		#Product_Table tfoot tr th:nth-child(1)
		{
			text-align:center;
		}
    </style>
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
        $('form').live("submit", function () {
            ShowProgress();
        });       
    </script>
    <script type="text/javascript">


        $(document).ready(function () {

            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var subDiv = $("#<%=SubDivCode.ClientID%>").val();


            dProd = []; dPQV = []; dSF = []; dPQVp = []
            genReport = function () {
                if (dSF.length > 0 && dProd.length > 0 && dPQV.length > 0){// && dPQVp.length > 0) {
                   

                    for (var i = 0; i < dSF.length; i++) {
                        str = '<td><p name="sfcode">' + dSF[i].empid + '</p> </td><td><p name="sfname">' + dSF[i].sfName + '</p> </td><td><p name="sfhq">' + dSF[i].sfhq + '</p> </td>';
                       
                        var TotVal_P = 0; var TotVal_Sec = 0; 
                        for (var j = 0; j < dProd.length; j++) {
                            var pv = "", v = ""; var tc = 0, ec = 0;
                            fP = dPQV.filter(function (a) { return (a.sfCode == dSF[i].sfCode && a.proCode == dProd[j].Day_id); });
                            fP1 = dPQVp.filter(function (a) { return (a.sfCode_p == dSF[i].sfCode && a.dy_p == dProd[j].Day_id); });
                            if (fP.length > 0) {
                                v = fP[0].TC_Count;

                                TotVal_Sec += Number(fP[0].TC_Count);
                            }
                            if (fP1.length > 0) {
                                pv = fP1[0].amount_p;

                                TotVal_P += Number(fP1[0].amount_p);
                            }

                            str += '<td>' + ((pv != "") ? parseFloat(pv) : "") + '</td><td>' + ((v != "") ? parseFloat(v) : "") + '</td>';
                        }
                        console.log(parseFloat(TotVal_P));
                        str += '<td>' + parseFloat(TotVal_P) + '</td><td>' + parseFloat(TotVal_Sec) + '</td>';
                        $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                    }
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
                url: "rptEmp_Month_call.aspx/getdata",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dProd = data.d;
                    //genReport();
                    if (data.d.length > 0) {
                        str = '<th  style="min-width:100px;" rowspan="2"><p style="margin: 0 0 0px;">Employee Code</p> </th><th  style="min-width:250px;" rowspan="2"><p style="margin: 0 0 0px;">Name Of Employee</p></th><th  style="min-width:200px;" rowspan="2"><p style="margin: 0 0 0px;">Head Quarter</p></th>';
                        console.log(len);
                        str1 = '';
                        strff = '<th style="min-width:250px;" colspan="3"> <p style="margin: 0 0 0px;">Total</p> </th>';
                        for (var i = 0; i < data.d.length; i++) {

                            str += '<th style="min-width:150px; " colspan="2"> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].Day_id + '</p> </th>';
                            str1 += '<th>Primary</th><th>Secondary</th>';
                            strff += '<th style="text-align: right" ></th><th style="text-align: right"></th></th>';

                        }

                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="2">Total</th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>Primary</th><th>Secondary</th></tr>');
                        $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th></tr>');
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





            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptEmp_Month_call.aspx/getIssuData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    dPQV = data.d;
                    //genReport();
                    $('#btnexcel').show();
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
                url: "rptEmp_Month_call.aspx/getSalesforce",
                data: "{'SF_Code':'" + sf_code + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    dSF = data.d;
                    // console.log(data.d);
                   // genReport();
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
                url: "rptEmp_Month_call.aspx/getIssuData_pro",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    dPQVp = data.d;
                    //genReport();
                    $('#btnexcel').show();
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

            // console.log(subDiv);
			genReport();

            var arr = [];
            $('#Product_Table tbody tr').each(function () {
                var i = 0;
                console.log($(this).find('td').length);
                $(this).find('td').each(function () {
                    if (i > 2) {
                        arr[i - 3] = Number(arr[i - 3] || 0) + Number($(this).text() || 0);
                    }
                    i++;
                });
            });
            // console.log(arr);

            var i = 0;
            var leng = $('.trfoot th').length;
            $('.trfoot th').each(function () {
                if (i > 0) {
                    $(this).text(parseFloat(arr[i - 1]));
                    //                    if (i % 2 == 0) {
                    //                        if (leng - 3 == i) {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                        else if (leng - 2 == i) {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                        else if (leng - 1 == i) {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                        else {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }

                    //                    }
                    //                    else {
                    //                        if (leng - 3 == i) {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                        else if (leng - 2 == i) {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                        else if (leng - 1 == i) {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                        else {
                    //                            $(this).text(parseFloat(arr[i - 1]));
                    //                        }
                    //                    }

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
                    //                    $(this).hide();
                    //                    tds.hide();
                    //                    $('#Product_Table tfoot tr th').eq($(this).index() + 1).hide();
                    ////                      if (i > 0) {
                    //                    if (i % 3 == 0) {
                    //                        $('.mainhead th').eq((i / 3) + 1).hide();
                    //                    }
                    //                    }
                }
            });
            sortTable();





            $(document).on('click', "#btnClose", function () {
                window.close();
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
      x = rows[i].getElementsByTagName("TD")[colLen-3] || 0;
      y = rows[i + 1].getElementsByTagName("TD")[colLen-3] || 0;
      //check if the two rows should switch place:
      if ( Number(x.innerHTML) < Number( y.innerHTML )) {
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
var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})()
       
       
    </script>
</head>
<body>
    <form id="form" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
    <div class="row">
        <div class="col-sm-8" style="padding-left: 5px;">
            <asp:Label ID="Label2" runat="server" Text="Employee Monthly call Details " Style="font-weight: bold;
                font-size: x-large"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
           <a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" onclick="tableToExcel('Product_Table', 'Employee Monthly call Details')" />
			<a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
    </div>
    <div class="row">
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label" Style="padding-left: 5px;"></asp:Label>
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
