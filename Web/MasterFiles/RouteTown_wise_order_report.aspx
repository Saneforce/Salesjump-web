<%@ Page Title="" Language="C#"  MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RouteTown_wise_order_report.aspx.cs" Inherits="MasterFiles_RouteTown_wise_order_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>RouteTown wise Report</title>
    <link type="text/css" rel="Stylesheet" href="../css/Report.css" />
    <%--<link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />--%>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

    <style type="text/css">        
        .newStly td
        {
            padding-top: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 12px;
        }
        
        .num2
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">       
        <div class="row">
                <label id="Label2" class="col-md-1  col-md-offset-4  control-label">
                    Division</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            Width="150">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="ddlFF" class="col-md-1 col-md-offset-4 control-label">
                    Field Force</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        <div class="row" id="divyear" style="margin-top: 1rem;">
             <label for="lblyear" class="col-md-1 col-md-offset-4 control-label">
                    Year</label>
            <div class="col-md-6 inputGroupContainer">
                <div class="input-group">
                    <select name="ddlyear" id="ddlyear">
                        <option selected="selected" value="0">--Select--</option>
                    </select>
                </div>                            
            </div>                        
        </div>
        <div class="row" style="margin-top: 1rem; padding: 0px 0px 0px 550px;">
            <button type="button" class="btn btn-primary btnsaveClass" id="btnView">View</button>            
        </div>
        <div class="container" id="btnexcelimg" style="max-width: 100%; width: 95%; text-align: right;display:none;">
            <img style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 39px; /*top: 150px;*/" src="../img/excel.png" alt="" width="40" height="40" id="btnExcel" />
        </div>
        
        <div class="row" style="margin: 25px 0px 0px 5px;width:100%;height:100%;overflow:scroll;">
            <br />
            <div id="content">               
                <%--<table id="OrderList" class="newStly" style="border-collapse: collapse;width:80%;overflow:scroll;">--%>
                <table id="OrderList" border="1" class="newStly" style="margin-left: 15px;width: 95%;">
                   <thead></thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                  </table>
            </div>
        </div>
   <%-- </div>--%>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <script src="../js/jquery.table2excel.js"></script>
    <script src="../js/jquery.table2excel.min.js"></script>
    <script type="text/javascript">

        function doit(type, fn, dl) {
            var elt = document.getElementById('OrderList');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "RouteTown_wise_order_report" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ((type || 'xlsx')));
        }
        var routTown = []; var Orderval = [];var planDate = [];
        var routTownWtype = []; var OrdervalWtype = []; var planDateWtype = [];
        var monthWD = [];
        var monthPOB = [];
        totalsdp = 0;
        
        $(document).ready(function () {

            var todaydate = new Date();
            var year = todaydate.getFullYear();

            for (var i = year - 2; i < year; i++) {
                $('#ddlyear').append('<option value="' + i + '">' + i + '</option>');               
            }
            for (var i = year; i < year + 3 ; i++) {
                $('#ddlyear').append('<option value="' + i + '">' + i + '</option>');               
            }
            $('#btnView').on('click', function (e) {
                var sf_code = $('#<%=ddlFieldForce.ClientID%>').val();
                var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                var sub_div_code = $('#<%=subdiv.ClientID%>').val();
                var selectedyear = $('#ddlyear option:selected').val();
                var selectedyearval = $('#ddlyear option:selected').text();
                //$("#ddlDiv option:selected")
                if (ddlfo_Name == '---Select Field Force---') { alert('Select Field Force'); focus($('#ddlFieldForce')); return false;}
                if (selectedyearval == '--Select--') { alert('Select year'); focus($('#ddlyear')); return false;}
                getroutTown(sf_code);
                getroutTownOrderval(sf_code,selectedyear);
                getroutTownplanDate(sf_code,selectedyear);                
                getroutTownplanDateWtype(sf_code,selectedyear);
                ReloadTable();
                $('#btnexcelimg').show();
            });
                        
            $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#content').html());
                var a = document.createElement('a');
                a.href = data_type;

                a.download = 'RouteTown_Wise_Order_Report.xls';               
                a.click();
                e.preventDefault();
            });
        });

        function getroutTown(sf_code) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RouteTown_wise_order_report.aspx/getroutTown",
                data: "{'sf_code':'"+ sf_code +"'}",
                dataType: "json",
                success: function (data) {
                    routTown = JSON.parse(data.d) || [];                    
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getroutTownOrderval(sf_code,year) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RouteTown_wise_order_report.aspx/getroutTownOrderval",
                data: "{'sf_code':'"+ sf_code +"','year':'"+ year +"'}",
                dataType: "json",
                success: function (data) {
                    Orderval = JSON.parse(data.d) || [];                   
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getroutTownplanDate(sf_code,year) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RouteTown_wise_order_report.aspx/getroutTownplanDate",
               // data: "{'year':'"+ year +"'}",
                data: "{'sf_code':'"+ sf_code +"','year':'"+ year +"'}",
                dataType: "json",
                success: function (data) {
                    planDate = JSON.parse(data.d) || [];                    
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        
        function getroutTownplanDateWtype(sf_code,year) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RouteTown_wise_order_report.aspx/getroutTownplanDateWtype",
                //data: "{'year':'"+ year +"'}",
                 data: "{'sf_code':'"+ sf_code +"','year':'"+ year +"'}",
                dataType: "json",
                success: function (data) {
                    planDateWtype = JSON.parse(data.d) || [];                    
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
              
        function ReloadTable() {
            $('#OrderList thead').html('');
            $('#OrderList tbody').html('');
            $('#OrderList').show();
            var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            var selectedyear = $('#ddlyear option:selected').val();
            subyear = selectedyear.substring(2, 4);
            var strhead = "<tr ><th rowspan='2'>Sl.No</th rowspan='2'><th rowspan='2'>STATE</th><th rowspan='2'>BLOCK</th><th rowspan='2'>HQ</th><th rowspan='2'>TOWN</th><th rowspan='2'>SDP</th>";
            for (var k = 0; k < month.length; k++) {
                strhead += "<th colspan='2'>" + month[k] + "-" + subyear + "</th>";                
            }
            strhead += "</tr><tr>";
            for (var ii = 0; ii < 12; ii++) {
                strhead += '<th>W.DAY</th><th>POB</th>';
            }
            strhead += '</tr>';
            $('#OrderList thead').append(strhead);
            
            var str = '';
            var daytot = 0; var valtot = 0;
            totalsdp = 0;
            //monthWD = [];monthPOB = [];
            if (routTown.length > 0) {
                for (i = 0; i < routTown.length; i++) {
                    str += '<tr><td>' + (i + 1) + '</td><td>' + routTown[i].StateName + '</td><td>' + routTown[i].StateName + '</td><td>' + routTown[i].Sf_HQ + '</td><td>' + routTown[i].TownName+ '</td><td>' + routTown[i].territoryCount + '</td>';
                    totalsdp += routTown[i].territoryCount;
                    monthWD = [];monthPOB = [];
                    for (var $j = 0; $j < month.length; $j++) {
                        var arrval = $j;
                        var planDatefiltr = planDate.filter(function (a) {
                            return a.TownCode == routTown[i].TownCode && a.plan_monthName == month[$j];
                        });
                        var ordervalfiltr = Orderval.filter(function (a) {
                            return a.TownCode == routTown[i].TownCode && a.Order_monthName == month[$j];
                        });
                        if (planDatefiltr.length > 0) {
                            str += '<td>' + planDatefiltr[0].plantdatecount + '</td>';
                            if (monthWD.length > 0) {
                                if (arrval > monthWD.length - 1) {
                                    monthWD[arrval] = planDatefiltr[0].plantdatecount;
                                } else
                                    monthWD[arrval] = monthWD[arrval] + planDatefiltr[0].plantdatecount;
                            }
                            else
                                monthWD[arrval] = planDatefiltr[0].plantdatecount;
                        }
                        else {
                            str += '<td>0</td>';
                            monthWD[arrval] = 0;
                        }
                        if (ordervalfiltr.length > 0) {
                            str += '<td>' + ordervalfiltr[0].orderval + '</td>';
                            if (monthPOB.length > 0) {
                                if (arrval > monthPOB.length - 1) {
                                    monthPOB[arrval] = ordervalfiltr[0].orderval;
                                } else
                                    monthPOB[arrval] = monthPOB[arrval] + ordervalfiltr[0].orderval;
                            } else
                                monthPOB[arrval] = ordervalfiltr[0].orderval;
                        }
                        else {
                            str += '<td>0</td>';
                            monthPOB[arrval] = 0;
                        }                           
                    }
                    str += "</tr>";
                }                
            }
            if (planDateWtype.length > 0) {
                for (var i = 0; i < planDateWtype.length; i++) {
                    str += '<tr><td>' + (routTown.length + 1) + '</td><td>' + planDateWtype[0].StateName + '</td><td>' + planDateWtype[0].StateName + '</td><td>' + planDateWtype[0].Sf_HQ + '</td><td>' + planDateWtype[i].Wtype+ '</td><td></td>';
                   
                    for (var $j = 0; $j < month.length; $j++) {
                        var arrval = $j;
                        var planDatefiltr = planDateWtype.filter(function (a) {
                            return a.plan_monthName == month[$j];
                        });                        
                        if (planDatefiltr.length > 0) {
                            str += '<td>' + planDatefiltr[0].Wplancount + '</td>';
                            monthWD[arrval] = monthWD[arrval] + planDatefiltr[0].Wplancount;
                        }
                        else {
                            str += '<td>0</td>';       
                        }                          
                        str += '<td>0</td>';                        
                    }
                    str += "</tr>";
                }
                $('#OrderList tbody').append(str);
                
            }
            $('#OrderList tfoot').html("<tr><td colspan=5 style='text-align: center;font-weight: bold'>Total</td><td>" + totalsdp + "</td><td>" + monthWD[0] + "</td><td>" + monthPOB[0] +  "</td><td>" + monthWD[1] + "</td><td>" + monthPOB[1] + "</td><td>" + monthWD[2] + "</td><td>" + monthPOB[2] + "</td><td>" + monthWD[3] + "</td><td>" + monthPOB[3] + "</td><td>" + monthWD[4] + "</td><td>" + monthPOB[4] + "</td><td>" + monthWD[5] + "</td><td>" + monthPOB[5] + "</td><td>" + monthWD[6] + "</td><td>" + monthPOB[6] + "</td><td>" + monthWD[7] + "</td><td>" + monthPOB[7] + "</td><td>" + monthWD[8] + "</td><td>" + monthPOB[8] + "</td><td>" + monthWD[9] + "</td><td>" + monthPOB[9] + "</td><td>" + monthWD[10] + "</td><td>" + monthPOB[10] + "</td><td>" + monthWD[11] + "</td><td>" + monthPOB[11] + /*"</td><td>" + monthWD[12] + "</td><td>" + monthPOB[12] +*/ "</td></tr>");
            
        }
    </script>
     <script type="text/javascript">
        var array1 = new Array();
        var array2 = new Array();
        var n = 2; //Total table
        for (var x = 1; x <= n; x++) {
            array1[x - 1] = x;
            array2[x - 1] = x + 'th';
        }

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
        , templateend = '</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'
        , body = '<body>'
        , tablevar = '<table>{table'
        , tablevarend = '}</table>'
        , bodyend = '</body></html>'
        , worksheet = '<x:ExcelWorksheet><x:Name>'
        , worksheetend = '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>'
        , worksheetvar = '{worksheet'
        , worksheetvarend = '}'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        , wstemplate = ''
        , tabletemplate = '';

            return function (table, name, filename) {
                var tables = table;

                for (var i = 0; i < tables.length; ++i) {
                    wstemplate += worksheet + worksheetvar + i + worksheetvarend + worksheetend;
                    tabletemplate += tablevar + i + tablevarend;
                }

                var allTemplate = template + wstemplate + templateend;
                var allWorksheet = body + tabletemplate + bodyend;
                var allOfIt = allTemplate + allWorksheet;

                var ctx = {};
                for (var j = 0; j < tables.length; ++j) {
                    ctx['worksheet' + j] = name[j];
                }

                for (var k = 0; k < tables.length; ++k) {
                    var exceltable;
                    if (!tables[k].nodeType) exceltable = document.getElementById(tables[k]);
                    ctx['table' + k] = exceltable.innerHTML;
                }

               
                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })();
        // onclick="tablesToExcel(array1, array2, 'myfile.xls')"
    </script>    
</body>
</html>
</asp:Content>

