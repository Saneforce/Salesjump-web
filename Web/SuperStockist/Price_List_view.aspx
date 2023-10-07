<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Price_List_view.aspx.cs" Inherits="MasterFiles_Price_List_view" %>
<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="../css/chosen.css" rel='stylesheet' type='text/css' />
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../js/jquery.table2excel.js"></script>
    <script src="../js/jquery.table2excel.min.js"></script>
    <script type="text/javascript">
        bindArray = [];
        ratePeriod = [];

        $(document).ready(function () {
            getdatalist();
            getproducts(0);
            $('#ddlPeriod').on('change', function () {
                var PeriodSl_No = $('#ddlPeriod option:selected').val();
                //if (PeriodSl_No != 0) {
                    getproducts(PeriodSl_No);
                //} else
                   // { alert("Select Period"); $('#ddlPeriod').focus(); return false; }                
            });       
        });
        function getproducts(PeriodSl_No) {
             $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Price_List_view.aspx/Get_Product_list",
               // data: "{}",
                data: "{'PeriodSl_No':'" + PeriodSl_No + "'}",
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
        function getdatalist() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Price_List_view.aspx/GetRatePeriods",
                dataType: "json",
                success: function (data) {
                    ratePeriod = JSON.parse(data.d) || [];
                    $('#ddlPeriod').empty().append('<option value=0>--select--</option>')
                    for (k = 0; k < ratePeriod.length; k++) {
                        if (ratePeriod[k].Effective_From_Date != '') {
                            var perioddata = ratePeriod[k].Effective_From_Date + "- " + ratePeriod[k].Effective_To_Date;
                            $('#ddlPeriod').append('<option value="' + ratePeriod[k].Period_Sl_No + '">' + perioddata + '</option>');
                        }
                    }
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }

        function ReloadTable() {      
            $("#OrderList TBODY").html("");          
            for ($i = 0; $i < bindArray.length; $i++) {
                var slno = $i + 1;
                var rwStr = "";
                rwStr += "<tr><td>" + slno + "</td><td>" + bindArray[$i].Product_Code + "</td><td>" + bindArray[$i].Product_Name+ "</td><td>" + bindArray[$i].UOM + "</td><td>" + bindArray[$i].distributor_price + "</td><td>" + bindArray[$i].retailer_price + "</td><td>"+ bindArray[$i].Effective_From_Date + "- " + bindArray[$i].Effective_To_Date +"</td></tr>";
                $("#OrderList tbody").append(rwStr);
            }
        }
    </script>
 
</head>
<body>
    <form id="form1" runat="server">
    <div class="row" style="max-width: 100%; width: 98%">
        <br />
         <div class="col-lg-12 sub-header" id="dist_head" style="font-size:16px;text-align:center;font-weight: bolder;"> 
             Product Price List
         </div>
        <button id="btnExcel" runat="server" align="right"  style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 105px; top: 43px;display:none" onclick="lnkDownload_Click"><img src="img/excel.png"/></button>        
    </div>
    <div class="row" id="ratePeriod" style="margin-top: 1rem;padding-left:50px;display:none;">
        <label id="lblperiod" class="col-md-2">Period</label>            
        <select style="" name="ddlPeriod" id="ddlPeriod">
            <option selected="selected" value="0">--Select--</option>
        </select>                    
    </div>
    <div id="divtable" style="padding-top: 50px;"> 
        <table id="OrderList" border="1" class="newStly" style="margin-left: 15px;width: 95%;display:none;">
            <thead>
                <tr>
                    <th>Sl.No</th>
                    <th>Product Code</th>
                    <th>Product Name</th>
                    <th>UOM</th>
                    <th>Distributor Price</th>
                    <th>Retailer Price</th>
                    <th>Valid From-To</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>

