<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptPrimvsSec.aspx.cs" Inherits="MIS_Reports_rptPrimvsSec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Vs Secondary</title>
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />

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
            text-align: right;
        }
        
        #Product_Table tbody tr td:nth-child(1)
        {
            text-align: left;
        }
        #Product_Table tfoot tr th:nth-child(1)
        {
            text-align: center;
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
            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }


            dProd = []; dPQV = []; dPQVp = []; pro_1 = []; pro_2 = [];
            //console.log(dPQV_P);
            //console.log(dPQVp_P);
           // var cnt =0;
            genReport = function () {

                if (dPQV.length > 0 && dPQVp.length > 0 && pro_1.length > 0 && pro_2.length > 0) {

                 //console.log(dPQV);
                 //console.log(dPQVp);
                 console.log(pro_1);
                 //console.log(pro_2);


                    var DDts = [];
                    for (var i = 0; i < dPQV.length; i++) {
                        DDts.push({ oDate: dPQV[i].dy, route: dPQV[i].SDP, subDt: dPQV[i].Sub_Date, wType: dPQV[i].wrk_Name, wWith: dPQV[i].wrk_With });
                    }

                    for (var j = 0; j < pro_1.length; j++) {
                        DDts.push({ oDate: pro_1[j].dy_pro, route: pro_1[j].SDP_pro, subDt: pro_1[j].Sub_Date_pro, wType: pro_1[j].wrk_Name_pro, wWith: '' });
                    }

                    ArrayArray = DDts.reduce(function (item, e1) {
                        var matches = item.filter(function (e2)
                        { return e1.oDate == e2.oDate });
                        if (matches.length == 0) {
                            item.push(e1);
                        }
                        return item;
                    }, []);
                     console.log(ArrayArray);
                   ArrayArray.sort((a,b) => ( Number(a.oDate) > Number(b.oDate)) ? 1 : ((Number(b.oDate) > Number(a.oDate)) ? -1 : 0)); 
                   console.log(ArrayArray);
                    for (var i = 1; i < ArrayArray.length; i++) {
                      //  console.log( i +': '+ ArrayArray[i].oDate );
                        //                        console.log(dPQVp);
                       
                        var tc = 0, ec = 0; var tc_p = 0, ec_p = 0; r_cou = 0;
                        var TotVal = 0; var TotVal_p = 0; d_cou = 0;
                        fP = dPQV.filter(function (a) { return (a.dy == ArrayArray[i].oDate); });
                        // console.log('Mano');
                        // console.log(fP);

                        if (fP.length > 0) {
                            tc = fP[0].TC_Count, ec = fP[0].EC_Count;
                            TotVal = fP[0].order; r_cou = fP[0].rt_visit;
                        }
                        var fP1 = pro_1.filter(function (a) { return (a.dy_pro == ArrayArray[i].oDate); });

                        if (fP1.length > 0) {
                            tc_p = fP1[0].TC_Count_pro, ec_p = fP1[0].EC_Count_pro;
                            TotVal_p = fP1[0].order_pro; d_cou = fP1[0].dis_visit_pro;
                        }

                        str = '<td><p name="day" style="margin: 0 0 0px;">' + ArrayArray[i].oDate + '</p> </td><td><p name="Route" style="margin: 0 0 0px;">' + ArrayArray[i].route + '</p> </td><td><p name="Sub_Date" style="margin: 0 0 0px;">' + ArrayArray[i].subDt + '</p> </td><td><p name="wrk_Name" style="margin: 0 0 0px;">' + ArrayArray[i].wType + '</p> </td><td><p name="wrk_With" style="margin: 0 0 0px;">' + ArrayArray[i].wWith + '</p> </td><td><p name="rt_visit" style="margin: 0 0 0px;">' + d_cou + '</p> </td><td><p name="rt_visit1" style="margin: 0 0 0px;">' + r_cou + '</p> </td>';
                        for (var j = 0; j < dProd.length; j++) {
                            //   console.log(i + ":" + ArrayArray[i].oDate);
                            var pv = "", v = "";
                            var fP = dPQVp.filter(function (a) { return (a.proName == dProd[j].product_id && a.dy_p == ArrayArray[i].oDate); });
                            var fP1 = pro_2.filter(function (a) { return (a.proName_pro == dProd[j].product_id && a.dy_p_pro == ArrayArray[i].oDate); });
                            if (fP.length > 0) {
                                v = fP[0].Proqty;
                                //TotVal += Number(fP[0].amount);


                            }
                            if (fP1.length > 0) {
                                pv = fP1[0].Proqty_pro;

                            }

                            str += '<td>' + ((pv != "") ? parseFloat(pv) : "") + '</td><td>' + ((v != "") ? parseFloat(v) : "") + '</td>';
                        }
                        //                        console.log(parseFloat(TotVal));
                        str += '<td>' + parseFloat(TotVal_p) + '</td><td>' + parseFloat(TotVal) + '</td><td>' + tc_p + '</td><td>' + tc + '</td><td>' + ec_p + '</td><td>' + ec + '</td>';
                        $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                    }
                }
                
            }
            




            $('#Product_Table tr').remove();
            var len = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptPrimvsSec.aspx/getdata",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dProd = data.d;
                    genReport();
                    if (data.d.length > 0) {
                        str = '<th  style="min-width:50px;" rowspan="2"> <p style="margin: 0 0 0px;">Date</p> </th><th  style="min-width:200px;" rowspan="2"> <p style="margin: 0 0 0px;">Route</p> </th><th  style="min-width:100px;" rowspan="2"> <p style="margin: 0 0 0px;">Sub.Date</p> </th><th  style="min-width:100px;" rowspan="2"> <p style="margin: 0 0 0px;">Work Type</p> </th><th  style="min-width:100px;" rowspan="2"> <p style="margin: 0 0 0px;" rowspan="2">Worked With</p> </th><th  style="min-width:250px;" colspan="2"> <p style="margin: 0 0 0px;">visited</p> </th>';
                        str1 = '<th>Distributor</th><th>Retailer</th>';
                        strff = '<th style="min-width:250px;" colspan="5"> <p style="margin: 0 0 0px;">Total</p> </th>';

                        for (var i = 0; i < data.d.length; i++) {

                            str += '<th style="min-width:150px; " colspan="2"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].product_name + '</p> </th>';
                            str1 += '<th>PriQty</th><th>SecQty</th>';
                            strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';

                        }

                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="6">Order Value</th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>PriValues</th><th>SecValues</th><th>PriTC</th><th>SecTC</th><th>PriEC</th><th>SecEC</th></tr>');
                        $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr>');
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
                url: "rptPrimvsSec.aspx/getIssuData1",
                data: "{'SF_Code':'" + sf_code + "', 'FDate':'" + Fyear + "', 'TDate':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    pro_1 = data.d;
                    genReport();
                    //genReport();
                    $('#btnexcel').show();
                },
                error: function (error) {
                    console.log(error);
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptPrimvsSec.aspx/getIssuData_pro1",
                data: "{'SF_Code':'" + sf_code + "', 'FDate':'" + Fyear + "', 'TDate':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    pro_2 = data.d;
                  //  console.log(pro_2);
                    //genReport();
                    $('#btnexcel').show();
                },
                error: function (error) {
                    console.log(error);
                }

            });




            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptPrimvsSec.aspx/getIssuData",
                data: "{'SF_Code':'" + sf_code + "', 'FDate':'" + Fyear + "', 'TDate':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    dPQV = data.d;
                   // console.log(dPQV);
                    genReport();
                    $('#btnexcel').show();
                },
                error: function (error) {
                    console.log(error);
                }
                //                error: function (jqXHR, exception) {
                //                    //alert(JSON.stringify(result));
                //                    var msg = '';
                //                    if (jqXHR.status === 0) {
                //                        msg = 'Not connect.\n Verify Network.';
                //                    } else if (jqXHR.status == 404) {
                //                        msg = 'Requested page not found. [404]';
                //                    } else if (jqXHR.status == 500) {
                //                        msg = 'Internal Server Error [500].';
                //                    } else if (exception === 'parsererror') {
                //                        msg = 'Requested JSON parse failed.';
                //                    } else if (exception === 'timeout') {
                //                        msg = 'Time out error.';
                //                    } else if (exception === 'abort') {
                //                        msg = 'Ajax request aborted.';
                //                    } else {
                //                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                //                    }
                //                    alert(msg);
                //                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptPrimvsSec.aspx/getIssuData_pro",
                data: "{'SF_Code':'" + sf_code + "', 'FDate':'" + Fyear + "', 'TDate':'" + FMonth + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    dPQVp = data.d;
                    genReport();
                    $('#btnexcel').show();
                },
                error: function (error) {
                    console.log(error);
                }
                //                error: function (jqXHR, exception) {
                //                    //alert(JSON.stringify(result));
                //                    var msg = '';
                //                    if (jqXHR.status === 0) {
                //                        msg = 'Not connect.\n Verify Network.';
                //                    } else if (jqXHR.status == 404) {
                //                        msg = 'Requested page not found. [404]';
                //                    } else if (jqXHR.status == 500) {
                //                        msg = 'Internal Server Error [500].';
                //                    } else if (exception === 'parsererror') {
                //                        msg = 'Requested JSON parse failed.';
                //                    } else if (exception === 'timeout') {
                //                        msg = 'Time out error.';
                //                    } else if (exception === 'abort') {
                //                        msg = 'Ajax request aborted.';
                //                    } else {
                //                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                //                    }
                //                    alert(msg);
                //                }
            });

            // console.log(subDiv);
            //          

            var arr = [];
            $('#Product_Table tbody tr').each(function () {
                var i = 0;
                //console.log($(this).find('td').length);
                $(this).find('td').each(function () {
                    if (i > 2) {
                        arr[i - 5] = Number(arr[i - 5] || 0) + Number($(this).text() || 0);
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
                    //                   $(this).hide();
                    //                    tds.hide();
                    //                    $('#Product_Table tfoot tr th').eq($(this).index() + 1).hide();
                    ////                      if (i > 0) {
                    //                    if (i % 3 == 0) {
                    //                        $('.mainhead th').eq((i / 3) + 1).hide();
                    //                    }
                    //                    }
                }
            });
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
                a.download = 'Primary_Vs_Secondary' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

        });


//        function sortTable() {
//            var table, rows, switching, i, x, y, shouldSwitch;
//            table = document.getElementById("Product_Table");
//            switching = true;
//            /*Make a loop that will continue until
//            no switching has been done:*/
//            while (switching) {
//                //start by saying: no switching is done:
//                switching = false;
//                rows = table.rows;
//                /*Loop through all table rows (except the
//                first, which contains table headers):*/
//                for (i = 1; i < (rows.length - 1); i++) {
//                    //start by saying there should be no switching:
//                    shouldSwitch = false;
//                    /*Get the two elements you want to compare,
//                    one from current row and one from the next:*/
//                    var colLen = rows[i].cells.length;
//                    x = rows[i].getElementsByTagName("TD")[colLen - 3] || 0;
//                    y = rows[i + 1].getElementsByTagName("TD")[colLen - 3] || 0;
//                    //check if the two rows should switch place:
//                    if (Number(x.innerHTML) < Number(y.innerHTML)) {
//                        //if so, mark as a switch and break the loop:
//                        shouldSwitch = true;
//                        break;
//                    }
//                }
//                if (shouldSwitch) {
//                    /*If a switch has been marked, make the switch
//                    and mark that a switch has been done:*/
//                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
//                    switching = true;
//                }
//            }
     //   }
//       
       
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
            <asp:Label ID="Label2" runat="server" Text="Primary Vs Secondary Details" Style="font-weight: bold;
                font-size: x-large"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
            <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();"
                class="btn btnClose"></a>
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
