﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FOwise_secorder_rpt.aspx.cs" Inherits="MIS_Reports_FOwise_secorder_rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
            #loader {
                position: absolute;
                left: 50%;
                top: 50%;
                z-index: 1;
                width: 120px;
                height: 120px;
                margin: -76px 0 0 -76px;
                border: 16px solid #f3f3f3;
                border-radius: 50%;
                border-top: 16px solid #3498db;
                -webkit-animation: spin 2s linear infinite;
                animation: spin 2s linear infinite;
            }

            .overlay {
                background-color: #EFEFEF;
                position: fixed;
                width: 100%;
                height: 100%;
                z-index: 1000;
                top: 0px;
                left: 0px;
                opacity: .5;
                filter: alpha(opacity=50);
            }
            #emp_table{
  border-collapse: separate;
  border-spacing: 0;
  border: 1px solid #D3D3D3;
            }
         #emp_table th {
             position: sticky;
             top: 0;
             background-color: #496a9a;
         }
             #emp_table td {
                 padding:5px;
                 border: 0.5px solid #D3D3D3;
             }
             #emp_table td:nth-child() {
                 padding: 5px;
                 border: 1px solid #000
             }
             #emp_table tbody tr:nth-child(odd) {
                 background-color: none;
             }

             #emp_table tbody tr:nth-child(even) {
                 background-color: white;
             }
     </style>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <form id="Form1" runat="server">
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        <div>
            <h3>EmployeeWise OrderAmount</h3>
        </div>
		</br>
    <div class="row">
          <div class="col-lg-12 " >
		  <span style="float: right; margin-left: 15px;">
		  <label>Employee Name :</label>
                <select id="ddlsf" style="max-width: 150px; min-width: 150px;"></select> &nbsp&nbsp;&nbsp;&nbsp;&nbsp
            <button id="btnview" type="button" class="btn btn-primary" style="vertical-align: middle; background-color: #496a9a">View</button>&nbsp;&nbsp;&nbsp
			<img style="float: right; margin-right: 15px; cursor: pointer; width: 40px; height: 40px; float: right;" alt="" onclick="ExportToExcel()" src="../img/Excel-icon.png" /></span>&nbsp&nbsp&nbsp
                <span style="float: left; margin-left: 15px;">
                    <div id="reportrange"  class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer;  border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
                      <span id="ordDate" STYLE="font-weight:bold"></span><i class="fa fa-caret-down"></i>
                    </div>
                    </span>
            
                
                            
            </div>
        </div>
        </br>
        </br>
        <main class="main">
		<div class="ContenedorTabla" class="card-body table-responsive" style="max-width:100%; width:95%"></div></main>
       <script type="text/javascript" src="../js/jquery.CongelarFilaColumna.js"></script>
        
          <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

        <script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.2/xlsx.full.min.js"></script>
<script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/1.3.8/FileSaver.min.js"></script>
        <%--<script type="text/javascript" src="../js/jquery.table2excel.js"></script>--%>
        <script src="https://cdn.jsdelivr.net/gh/linways/table-to-excel@v1.0.4/dist/tableToExcel.js"></script>
    <script type="text/javascript">
        var FDT = ''; var TDT = '';
        $(document).ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDT = id[2].trim() + '-' + id[1] + '-' + id[0];
                TDT = id[5] + '-' + id[4] + '-' + id[3].trim();
                var Fdate = new Date(FDT);
                var fyear = Fdate.getFullYear();
                var Tdate = new Date(TDT);
                var tyear = Tdate.getFullYear();
            });
            fillsf();
            $(document).on('click', '#btnview', function () {
                var sf_code = $('#ddlsf').val();
                if (sf_code == '') {
                    alert("Please Select Employee...");
                }
				else{
                var di = $('.ContenedorTabla');
                $(di).empty();
                var htm = '<table class="table table-hover" cellspacing="1" align="Center" id="emp_table" style="font-size: 12px" width:85%;border-collapse:collapse;"> <thead class="text-warning"></thead><tbody></tbody> </table>';
                $(di).append(htm);
                var Fdate = new Date(FDT);
                var fyear = Fdate.getFullYear().toString();
                var Tdate = new Date(TDT);
                var tyear = Tdate.getFullYear().toString();
                var fmonth = Fdate.getMonth();
                var tmonth = Tdate.getMonth();
                if (fyear != tyear) {
                    //alert("Please select from to date with same year...");
                    var lstdate = fyear + "-" + "12-31";
                    var fstdate = tyear + "-" + "01-01";
                    var lsdat = new Date(lstdate);
                    var fsdat = new Date(fstdate);
                    var lstmth = lsdat.getMonth();
                    var fstmth = fsdat.getMonth();
                    var res, str = "";

                    if (monthDiff(Fdate, Tdate) <= 6) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "FOwise_secorder_rpt.aspx/getdatalist",
                            data: "{'Div':'<%=Session["div_Code"]%>','fdate':'" + FDT + "','tdate':'" + TDT + "','sfcode':'" + sf_code + "'}",
                            dataType: "json",
                            success: function (data) {
                                res = JSON.parse(data.d);
                                var thre = [];
                                var one = res.filter(function (a) {
                                    return a.Years == '2021';
                                });
                                var two = res.filter(function (a) {
                                    return a.Years == '2022';
                                });
                                for (var k = 0; k < two.length; k++) {
                                    var tt = 0;
                                    for (var h = 0; h < one.length; h++) {
                                        if (two[k].sf_code == one[h].sf_code) {
                                            tt = 1;
                                            break;
                                        }
                                    }
                                    if (tt == 0)
                                        thre.push(two[k]);
                                }
                                var my_columns = [], my_columns1 = [], val = 0, val1 = 0, ndmthcl = [], ndmthcl1 = [];
                                var str1 = "";

                                for (var j = fmonth; j <= lstmth; j++) {
                                    var item = {};
                                    item.data = j;
                                    item.value = fyear;
                                    ndmthcl.push(item);
                                }
                                for (var j = fstmth; j <= tmonth; j++) {
                                    var item = {};
                                    item.data = j;
                                    item.value = tyear;
                                    ndmthcl1.push(item);
                                }
                                if (res.length > 0) {
                                    $.each(res[0], function (key, value) {
                                        var my_item = {};
                                        my_item.data = key;
                                        my_item.title = val;
                                        if (my_item.title >= 3) {
                                            for (var l = 0; l < ndmthcl.length; l++) {
                                                if (ndmthcl[l].data + 3 == my_item.title) {
                                                    my_item.year = ndmthcl[l].value;
                                                    my_columns.push(my_item);
                                                }
                                            }
                                        }
                                        val++;
                                    });
                                    str = '<th class="celda_encabezado_general" style="text-align: left;color:White;width:15%;" ><p class="phh">SI.No.</p></th><th class="celda_encabezado_general" style="text-align: left;color:White;width:15%;" > <p class="phh">FieldForce</p> </th>';
                                    bindHeader(my_columns, fyear);
                                    $.each(res[0], function (key, value) {
                                        var my_item = {};
                                        my_item.data = key;
                                        my_item.title = val1;
                                        if (my_item.title >= 3) {
                                            for (var l = 0; l < ndmthcl1.length; l++) {
                                                if (ndmthcl1[l].data + 3 == my_item.title) {
                                                    my_item.year = ndmthcl1[l].value;
                                                    my_columns1.push(my_item);
                                                }
                                            }
                                        }
                                        val1++;
                                    });
                                    bindHeader(my_columns1, tyear);
                                    var slno = 1;
                                    for (var k = 0; k < one.length; k++) {
                                        var vale = 0, vale1 = 0, vale2 = 0, my_rows = [], my_rows1 = [], total = 0;
                                        str1 = '<td  class="celda_normal" style="min-width: 100px;" >' + (slno++) + '</td><td  class="celda_normal" style="min-width: 100px;" > <input type="hidden" name="stcode" value="' + one[k].sf_code + '"/> <p class="phh" name="sfname">' + one[k].SF_Name + '</p> </td>';
                                        $.each(one[k], function (key, value) {
                                            var item = {};
                                            item.data = key;
                                            item.title = vale;
                                            item.val = value;
                                            if (item.title >= 3) {
                                                for (var l = 0; l < ndmthcl.length; l++) {
                                                    if (ndmthcl[l].data + 3 == item.title) {
                                                        my_rows.push(item);
                                                        str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + my_rows[l].val + '</td>';
                                                        total = total + my_rows[l].val;
                                                        break;
                                                    }
                                                }
                                            }
                                            vale++;
                                        });
                                        var test = 0;
                                        for (var p = 0; p < two.length; p++) {
                                            if (one[k].sf_code == two[p].sf_code) {
                                                test = 1;
                                                $.each(two[p], function (key, value) {
                                                    var item = {};
                                                    item.data = key;
                                                    item.title = vale1;
                                                    item.val = value;
                                                    if (item.title >= 3) {
                                                        for (var l = 0; l < ndmthcl1.length; l++) {
                                                            if (ndmthcl1[l].data + 3 == item.title) {
                                                                my_rows1.push(item);

                                                                str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + my_rows1[l].val + '</td>';
                                                                total = total + my_rows1[l].val;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    vale1++;
                                                });
                                            }
                                        }
                                        if (test == 0) {
                                            for (var l = 0; l < ndmthcl1.length; l++) {
                                                str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + test + '</td>';
                                            }
                                        }
                                        str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + total.toFixed(2) + '</td>'
                                        $('#emp_table tbody').append('<tr>' + str1 + '</tr>');
                                        str1 = "";
                                    }

                                    for (var h = 0; h < thre.length; h++) {
                                        var my_rows2 = [];
                                        str1 = '<td  class="celda_normal" style="min-width: 100px;" >' + (slno++) + '</td><td  class="celda_normal" style="min-width: 100px;" > <input type="hidden" name="stcode" value="' + thre[h].sf_code + '"/> <p class="phh" name="sfname">' + thre[h].SF_Name + '</p> </td>';
                                        for (var l = 0; l < ndmthcl.length; l++) {
                                            str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. 0</td>';
                                        }
                                        $.each(thre[h], function (key, value) {
                                            var item = {};
                                            item.data = key;
                                            item.title = vale2;
                                            item.val = value;
                                            if (item.title >= 3) {
                                                for (var l = 0; l < ndmthcl1.length; l++) {
                                                    if (ndmthcl1[l].data + 3 == item.title) {
                                                        my_rows2.push(item);
                                                        str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + my_rows2[l].val + '</td>';
                                                        total = total + my_rows2[l].val;
                                                        break;
                                                    }
                                                }
                                            }
                                            vale2++;
                                        });
                                    }
                                    str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + total.toFixed(2) + '</td>'
                                    $('#emp_table tbody').append('<tr>' + str1 + '</tr>');
                                    str1 = "";
                                }
                                else {
                                    $('#emp_table tbody').append('<td style="font-family:courier;color:red">No Records Found...</td>');
                                }
                            },
                        });
                    }
                    else {
                        alert("Please Select within six months only...");
                    }
                }
                else {
                    var str = 0, str1 = "";
                    if (monthDiff(Fdate, Tdate) <= 6) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "FOwise_secorder_rpt.aspx/getdatalist",
                            data: "{'Div':'<%=Session["div_Code"]%>','fdate':'" + FDT + "','tdate':'" + TDT + "','sfcode':'" + sf_code + "'}",
                            dataType: "json",
                            success: function (data) {
                                var res = JSON.parse(data.d);
                                var my_columns = [], val = 0, ndmthcl = [];


                                for (var j = fmonth; j <= tmonth; j++) {
                                    var item = {};
                                    item.data = j;
                                    item.value = tyear;
                                    ndmthcl.push(item);
                                }
                                if (res.length > 0) {
                                    $.each(res[0], function (key, value) {
                                        var my_item = {};
                                        my_item.data = key;
                                        my_item.title = val;
                                        if (my_item.title >= 3) {
                                            for (var l = 0; l < ndmthcl.length; l++) {
                                                if (ndmthcl[l].data + 3 == my_item.title) {
                                                    my_item.year = ndmthcl[l].value;
                                                    my_columns.push(my_item);
                                                }
                                            }
                                        }
                                        val++;
                                    });
                                    str = '<th class="celda_encabezado_general" style="text-align: left;color:White;width:15%;" ><p class="phh">SI.No.</p></th><th class="celda_encabezado_general" style="text-align: left;color:White;width:15%;" > <p class="phh">FieldForce</p> </th>';
                                    bindHeader(my_columns, tyear);
                                    var slno = 1;
                                    for (var k = 0; k < res.length; k++) {
                                        var vale = 0, my_rows = [], total = 0;
                                        str1 = '<td  class="celda_normal" style="min-width: 100px;" >' + (slno++) + '</td><td  class="celda_normal" style="min-width: 100px;" > <input type="hidden" name="stcode" value="' + res[k].sf_code + '"/> <p class="phh" name="sfname">' + res[k].SF_Name + '</p> </td>';
                                        $.each(res[k], function (key, value) {
                                            var item = {};
                                            item.data = key;
                                            item.title = vale;
                                            item.val = value;
                                            if (item.title >= 3) {
                                                for (var l = 0; l < ndmthcl.length; l++) {
                                                    if (ndmthcl[l].data + 3 == item.title) {
                                                        my_rows.push(item);
                                                        str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + my_rows[l].val + '</td>';
                                                        total = total + my_rows[l].val;
                                                        break;
                                                    }
                                                }
                                            }
                                            vale++;
                                        });
                                        str1 += '<td  class="celda_normal" style="min-width: 100px; ">Rs. ' + total.toFixed(2) + '</td>'
                                        $('#emp_table tbody').append('<tr>' + str1 + '</tr>');
                                        str1 = "";
                                    }
                                }
                                else {
                                    $('#emp_table tbody').append('<td style="font-family:courier;color:red">No Records Found...</td>');
                                }
                            },
                            error: function (result) {
                            }
                        });
                    }
                    else {
                        alert("Please Select within six months only...");
                    }
                }
                function bindHeader(arr,year) {
                    
                    if (arr.length > 0) {
                        
                        for (var i = 0; i < arr.length; i++) {
                            str += '<th class="celda_encabezado_general" style="text-align: left;color:White;width:15%;align:center"> <input type="hidden" class="mono" name="mono" value="' + arr[i].year + '"/> <p class="phh" name="pname">' + arr[i].data + '-' + year+'</p> </th>';

                        }
                    }
                    
                }
                if (monthDiff(Fdate, Tdate) <= 6) {
                    str += '<th class="celda_encabezado_general" style="text-align: left;color:White;width:10%;" ><p class="phh">Total</p></th>';
                    $('#emp_table thead').append('<tr>' + str + '</tr>');
                }
				}
            });
            
        });
        function monthDiff(d1, d2) {
            var months;
            months = (d2.getFullYear() - d1.getFullYear()) * 12;
            months -= d1.getMonth();
            months += d2.getMonth();
            return months <= 0 ? 0 : months;
        }
        function fillsf() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "FOwise_secorder_rpt.aspx/getFieldForce",
                data: "{'Div':'<%=Session["div_Code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var sfdets = JSON.parse(data.d) || [];
                        var ddsf = $('#ddlsf');
                        $('#ddlff').selectpicker('destroy');
                        ddsf.empty().append('<option value="">Select Employee</option>');
                        //ddsf.append('<option value="admin">All</option>');
                        for (var i = 0; i < sfdets.length; i++) {
                            ddsf.append($('<option value="' + sfdets[i].Sf_Code + '">' + sfdets[i].Sf_Name + '</option>'));
                        }
                    },
                    error: function (result) {
                    }
                });
            //$('#ddlsf').selectpicker({
            //    liveSearch: true
            //});
        }
        function ExportToExcel() {
            var table = document.getElementById("emp_table"); 
            TableToExcel.convert(table, {
                name: `EmployeeWise_OrderValue.xlsx`, 
                sheet: {
                    name: FDT+'-'+TDT 
                }
            });

        }
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));

            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);
        });
    </script>
        </form>
</asp:Content>

