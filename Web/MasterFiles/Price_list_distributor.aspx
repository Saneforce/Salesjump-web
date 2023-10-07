<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Price_list_distributor.aspx.cs" Inherits="MasterFiles_Price_list_distributor" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="../css/chosen.css" rel='stylesheet' type='text/css' />
    <style type="text/css">
        .newStly td {
            padding-top: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 12px;
        }

        .num2 {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../js/jquery.table2excel.js"></script>
    <script src="../js/jquery.table2excel.min.js"></script>
    <script type="text/javascript">
        bindArray = [];
        ratePeriod = [];

        $(document).ready(function () {
            getproducts();
        });
        function getproducts() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Price_list_distributor.aspx/Get_Product_list",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    DCR_ProdDts = JSON.parse(data.d) || [];
                    bindArray = DCR_ProdDts;
                    ReloadTable();
                    $('#OrderList').show();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            for ($i = 0; $i < bindArray.length; $i++) {
                var slno = $i + 1;
                var rwStr = "";
                if (bindArray[0].CTYPE == 'DIST') {
                    rwStr += "<tr><td>" + slno + "</td><td style='display:none'>" + bindArray[$i].Stockist_Code + "</td><td>" + bindArray[$i].Stockist_Name + "</td></tr>";
                    $("#ctypename").html('Distributor Name');
                }
                else {
                    rwStr += "<tr><td>" + slno + "</td><td style='display:none'>" + bindArray[$i].S_No + "</td><td>" + bindArray[$i].S_Name + "</td></tr>";
                    $("#ctypename").html('Super Stockist Name');
                }
                $("#OrderList tbody").append(rwStr);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row" style="max-width: 100%; width: 98%">
            <br />
            <div class="col-lg-12 sub-header" id="dist_head" style="font-size: 16px; text-align: center; font-weight: bolder;">
                 List For RateCard - 
            
                <asp:Label ID="ratelbl" runat="server" Text="label"></asp:Label>
            </div>
        </div>
        <div id="divtable" style="padding-top: 20px;">

            <table id="OrderList" border="1" class="newStly" style="margin-left: 15px; width: 95%; display: none;">
                <thead>
                    <tr>
                        <th>Sl.No</th>
                        <th style="display: none">Distributor Code</th>
                        <th id="ctypename">Distributor Name</th>
                        <th style="display: none">Valid From-To</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>

