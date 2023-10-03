<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="rpt_Retailer_Score_Card.aspx.cs" Inherits="rpt_Retailer_Score_Card" %>

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
<html>
<head>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <body>
        <form id="frm1" runat="server">

            <br />
            <div>
                <asp:Label ID="lblHead" Text="Areawise Product Detail Daywise" SkinID="lblMand" Font-Bold="true" Font-Underline="true" CssClass="yourclass"
                    runat="server"></asp:Label>
            </div>
            <br />
            <br />
            <table class="table table-striped" id="Retailer_score_card">
                <thead>
                    <tr>
                        <th>SL No</th>
                        <th>Field Force Name</th>
                        <th>Distributor Name</th>
                        <th>Outlet Name</th>
                        <th>Month Sale Value</th>
                        <th>No of Sku Sold</th>
                        <th>No of Visit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </form>
    </body>
</head>

<style>
    .yourclass {
        font-size: 50px;
    }
</style>

<script type="text/javascript">

    var Retailer_Score_Card_Details = []; var Retailer_Score_Card_Details_Visit = [];

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        url: "rpt_Retailer_Score_Card.aspx/Retailer_Score_Card_Visit",
        dataType: "json",
        success: function (data) {
            Retailer_Score_Card_Details_Visit = JSON.parse(data.d);
        },
        error: function (result) {
        }
    });


    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        url: "rpt_Retailer_Score_Card.aspx/Retailer_Score_Card",
        dataType: "json",
        success: function (data) {
            Retailer_Score_Card_Details = JSON.parse(data.d);
            if (Retailer_Score_Card_Details.length > 0) {
                $('#Retailer_score_card tbody').html('');
                for (var t = 0; t < Retailer_Score_Card_Details.length; t++) {

                    var filtered_Visit = Retailer_Score_Card_Details_Visit.filter(function (u) {
                        return (u.Trans_Detail_Info_Code == Retailer_Score_Card_Details[t].Cust_Code);
                    });
                    slno = t + 1;
                    $("#Retailer_score_card tbody").append("<tr><td>" + slno + "</td><td>" + Retailer_Score_Card_Details[t].Sf_Name + "</td><td>" + Retailer_Score_Card_Details[t].DistributorName + "</td><td>" + Retailer_Score_Card_Details[t].Retailer_Name + "</td><td>" + parseFloat(Retailer_Score_Card_Details[t].Month_sales).toFixed(2) + "</td><td>" + Retailer_Score_Card_Details[t].sku + "</td><td>" + ((filtered_Visit.length > 0) ? filtered_Visit[0].Visit : '0') + "</td></tr>");                }
            }
            else {
                $("#Retailer_score_card").append('<tfoot><tr><th colspan="5"></th></tr></tfoot>');
            }
        },
        error: function (result) {
        }
    });

</script>
</html>

