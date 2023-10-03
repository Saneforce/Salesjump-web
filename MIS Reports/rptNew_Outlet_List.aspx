<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptNew_Outlet_List.aspx.cs"
    Inherits="MIS_Reports_rptNew_Outlet_List" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server" style="overflow-x: auto!important;">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../css/style.css" rel="stylesheet" />
    <title>New Outlets</title>
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
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
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        let users = [], stks = [];
        var dDist = [];
        let sf_code, Fyear, FMonth, SubDivCode, hdnDt;
        function getSFName() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptNew_Outlet_List.aspx/getSalesforce",
                dataType: "json",
                success: function (data) {
                    users = JSON.parse(data.d);
                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getStockistName() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptNew_Outlet_List.aspx/getStockist",
                dataType: "json",
                success: function (data) {
                    stks = JSON.parse(data.d);
                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
           return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptNew_Outlet_List.aspx/GetData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "', 'ModeDt':'" + hdnDt + "'}",
                dataType: "json",
                success: function (data) {
                    dDist = data.d;
                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function genReport() {
            if (dDist.length > 0) {
                for (var i = 0; i < dDist.length; i++) {
                    let sfname = users.filter(function (a) {
                        return ((',' + dDist[i].Sf_Code + ',').indexOf(a.Sf_Code) > 0)
                    }).map(function (el) {
                        return el.Sf_Name
                    }).join(',');
                    let stkname = stks.filter(function (a) {
                        return ((',' + dDist[i].Dist_Name + ',').indexOf(a.Stockist_Code) > 0)
                    }).map(function (el) {
                        return el.Stockist_Name
                    }).join(',');
                    str = '<td class="num2">' + (i + 1) + '</td>';
                    str += '<td>' + dDist[i].ListedDrCode + '</td> <td><input type="hidden" name="sfcode" value="' + dDist[i].ListedDrCode + '"/>' + dDist[i].ListedDr_Name + '</td> <td>' + dDist[i].RouteName + '</td> <td>' + sfname + '</td> <td>' + dDist[i].Retailer_Channel + '</td>';
                    str += '<td>' + dDist[i].Mobile_No + '</td> <td>' + dDist[i].Retailer_Class + '</td> <td>' + stkname + '</td> <td>' + dDist[i].ContactPerson + '</td><td>' + dDist[i].GSTNO + '</td><td>' + dDist[i].ListedDr_Email + '</td><td>' + dDist[i].Address + '</td><td>' + dDist[i].City + '</td><td>' + dDist[i].PinCode + '</td><td>' + dDist[i].order_val + '</td>';
                    $('#ProductTable tbody').append('<tr>' + str + '</tr>');
                }
            }
        }
        $(document).ready(function () {
            $('#loadover').show();
            sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            Fyear = $("#<%=ddlFYear.ClientID%>").val();
            FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            SubDivCode = $("#<%=SubDivCode.ClientID%>").val();
            hdnDt = $("#<%=hdnDate.ClientID%>").val();

           
            $('#ProductTable tr').remove();
            var str = '<th style="min-width:70px;">SLNo.</th><th style="min-width:150px;">Retailer Code</th>';
            str += '<th style="min-width:150px;">Retailer Name</th> <th style="min-width:150px;">Route</th>   <th style="min-width:450px;">FieldForce</th><th style="min-width:150px;">Retailer Channel</th> <th style="min-width:150px;">Mobile No</th> <th style="min-width:150px;">Retailer Class</th>';
            str += '<th style="min-width:300px;">Distributor Name</th> <th style="min-width:150px;">Owner</th>';
            str += '<th style="min-width:150px;">Owners Email</th> <th style="min-width:150px;">GSTIN</th> <th style="min-width:300px;">Address</th> <th style="min-width:150px;">Market Area City Tehsil</th> <th style="min-width:150px;">Pin Code</th><th style="min-width:150px;">Value</th>';

            $('#ProductTable thead').append('<tr class="mainhead">' + str + '</tr>');

            var ReasonArray = [];
            $.when(loadData(), getSFName(), getStockistName()).then(function () {
                genReport();
            });
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
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
                a.download = 'New_Outlet_List_' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });


        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
    <asp:HiddenField ID="hdnDate" runat="server" />
    <div class="row">
        <div class="col-sm-8">
            <asp:Label ID="Label2" runat="server" Text="Today New Outlet List" Style="font-weight: bold;
                font-size: x-large"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                    display: none" href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                        style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                            type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
        </div>
        <div>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <div id="content">
                <table id="ProductTable" class="newStly table table-responsive">
                    <thead>
                      
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
</body>
</html>
