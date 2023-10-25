<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExp_Status_RepView.aspx.cs"
    Inherits="MIS_Reports_rptExp_Status_RepView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" style="overflow-x: auto!important;">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="../css/style.css" rel="stylesheet" />
    <title>Manufacturing Status Report View</title>
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var rshq = [];
            var dRSF = [];
            var dDist = [];
            var dDist_no = [];
            var Dtls = [];
            var CatVal = [];
            var catH = [];
            var lvl = 0;
            genReport = function () {
                if (dDist.length > 0) {
                    //  console.log('en');
                    var ldcode = 0;
                    for (var i = 0; i < dDist.length; i++) {
                        str = '<td class="num2">' + (i + 1) + '</td><td>' + dDist[i].StateName + '</td><td>' + dDist[i].stockist_name + '</td>';
                        //console.log(dDist[i].RSFs);
                        //  console.log(rshq);
                        str += '<td>' + (dDist[i].Entered_Month || '') + '</td> <td><input type="hidden" name="sfcode" value="' + dDist[i].sf_code + '"/>' + dDist[i].Sf_Name + '</td> <td>' + (dDist[i].SDP_Name || '') + '</td> <td>' + (dDist[i].ListedDr_Name || '') + '</td><td>' + (dDist[i].Product_Detail_Name || '') + '</td><td>' + (dDist[i].Mf_Date || '') + '</td><td>' + (dDist[i].Quantity || '') + '</td><td>' + (dDist[i].Rate || '') + '</td><td>' + (dDist[i].value || '') + '</td><td>' + (dDist[i].net_weight || '') + '</td>';
                       
                        
                        $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                     


                    }
                    //str += '<td class="num2">' + TotQTY + '</td><td class="num2">' + TotVal.toFixed(2) + '</td>' + str2;

                }
            }



            //$('#ProductTable tr').remove();



            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=SubDivCode.ClientID%>").val();
            var hdnDt = $("#<%=hdnDate.ClientID%>").val();

            //head
            var ReasonArray = [];


            $('#Product_Table tr').remove();
            var str = '<th style="min-width:70px;">SLNo.</th><th style="min-width:150px;">State</th>';
            str += '<th style="min-width:250px;">Distributor Name</th>  <th style="min-width:100px;">Entered Month</th>  <th style="min-width:250px;">Name of SO</th> <th style="min-width:250px;">Route/Area Name</th> <th style="min-width:250px;">Name of Retailer</th> <th style="min-width:250px;">Product Name</th><th style="min-width:100px;">Manufacturing.date</th><th style="min-width:100px;">Quantity</th><th style="min-width:100px;">Rate</th> <th style="min-width:100px;">Value</th>';
            str += '<th style="min-width:100px;">Net value(Kilo Grams)</th>';
            $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');





            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptExp_Status_RepView.aspx/GetData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "', 'ModeDt':'" + hdnDt + "'}",
                dataType: "json",
                success: function (data) {
                    dDist = data.d;
                    genReport();

                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
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
                a.download = 'Manufacturing_Status_' + postfix + '.xls';
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
            <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large;
                padding: 0px 20px;" Text="Product Wise Secondary Order Report"></asp:Label>
        </div>
         <div class="col-sm-4" style="text-align: right">
         <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
			<a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
        </div>
        <div>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <div id="content">
                <table id="Product_Table" class="newStly table table-responsive">
                    <thead>
                      
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
      
 
    </form>
</body>
</html>
