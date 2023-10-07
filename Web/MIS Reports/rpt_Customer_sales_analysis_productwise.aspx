<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Customer_sales_analysis_productwise.aspx.cs"
    Inherits="MIS_Reports_rpt_Customer_sales_analysis_productwise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer </title>
     <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function RefreshParent() {
            //  window.opener.document.getElementById('form1').click();
            window.close();
        }
        $(document).ready(function () {
            var Routes = [];
            var Retailers = [];
            var dtd = [];
            var ProdctValues = [];
            // $('#Product_Table tr').remove();
            //            var len = 0;
            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                async: false,
            //                url: "rpt_Customer_sales_analysis_productwise.aspx/getdata",
            //                dataType: "json",
            //                success: function (data) {
            //                    len = data.d.length;
            //                    if (data.d.length > 0) {
            //                        str = '<th  style="min-width:250px;"> <p>Name</p> </th><th  style="min-width:150px;"> <p>Channel</p> </th>';
            //                        for (var i = 0; i < data.d.length; i++) {
            //                            str += '<th style="min-width:150px;"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname">' + data.d[i].product_name + '</p> </th>';
            //                        }
            //                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');
            //                    }
            //                },
            //                error: function (result) {
            //                    alert(JSON.stringify(result));
            //                }
            //            });

            //            var sf_code = $("#<%=hidn_sf_code.ClientID%>").val();
            //            var Fyear = $("#<%=hdnYear.ClientID%>").val();
            //            var FMonth = $("#<%=hdnMonth.ClientID%>").val();

            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                async: false,
            //                url: "rpt_Customer_sales_analysis_productwise.aspx/GetRoutes",
            //                data: "{'SF_Code':'" + sf_code + "'}",
            //                dataType: "json",
            //                success: function (data) {
            //                    //console.log(data.d);
            //                    Routes = data;
            //                },
            //                error: function (result) {
            //                    alert(JSON.stringify(result));
            //                }
            //            });

            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                async: false,
            //                url: "rpt_Customer_sales_analysis_productwise.aspx/GetRetaile",
            //                data: "{'SF_Code':'" + sf_code + "', 'FYear':'" + Fyear + "'}",
            //                dataType: "json",
            //                success: function (data) {
            //                    //console.log(data.d);
            //                    Retailers = data;
            //                },
            //                error: function (result) {
            //                    alert(JSON.stringify(result));
            //                }
            //            });



            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                async: false,
            //                url: "rpt_Customer_sales_analysis_productwise.aspx/GetRetailerProdctValues",
            //                data: "{'SF_Code':'" + sf_code + "', 'FYear':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
            //                dataType: "json",
            //                success: function (data) {
            //                    //console.log(data.d);
            //                    ProdctValues = data;
            //                },
            //                error: function (result) {
            //                    alert(JSON.stringify(result));
            //                }
            //            });

            //            //console.log(Retailers);
            //            //for (var i = 0; i < Routes.d.length; i++) {
            //            //   str = '';
            //            //  str = '<td>Route no:- ' + (i + 1) + ' ' + Routes.d[i].RouteName + '</td><td></td>';
            //            //  for (var m = 0; m < len; m++) {
            //            //     str += '<td></td>';
            //            // }
            //            //  $('#Product_Table tbody').append('<tr class="Routes">' + str + '</tr>');
            //            //  str = '';


            //            //  dtd = Retailers.d.filter(function (a) {
            //            //    return (a.Route == Routes.d[i].RouteCode);
            //            // });
            //            //work
            //            if (Retailers.d.length > 0) {
            //                for (var k = 0; k < Retailers.d.length; k++) {
            //                    str = '<td> <input type="hidden" name="ldrCode" value="' + Retailers.d[k].ListedDrCode + '"/>' + Retailers.d[k].ListedDr_Name + '</td><td>' + Retailers.d[k].Doc_Special_Name + '</td>';
            //                    for (var j = 0; j < len; j++) {
            //                        str += '<td></td>';
            //                    }
            //                    $('#Product_Table tbody').append('<tr>' + str + '</tr>');
            //                }
            //            }
            //            //   }

            //            var proArr = [];
            //            var s = 0;
            //            var dtls_tab = document.getElementById("Product_Table");
            //            var nrows1 = dtls_tab.rows.length;
            //            var Ncols = dtls_tab.rows[0].cells.length;

            //            for (var l = 0; l < ProdctValues.d.length; l++) {
            //                $('#Product_Table tbody tr').each(function () {
            //                    for (var col = 2; col < Ncols; col++) {
            //                        prdVal = 0;
            //                        console.log('en');
            //                        if ($(this).children('td').eq(0).find('input[name=ldrCode]').val() == ProdctValues.d[l].ListedDrCode) {
            //                            if ($(this).closest('table').find('.mainhead').find('th').eq(col).find('input[name=pcode]').val() == ProdctValues.d[l].prd_code) {
            //                                $(this).children('td').eq(col).text(ProdctValues.d[l].ord_val);
            //                                prdVal = Number(ProdctValues.d[l].ord_val || 0);
            //                            }
            //                        }
            //                        proArr[col - 2] = Number(proArr[col - 2] || 0) + Number(prdVal);
            //                    }





            //                });
            //            }


            var GArr = [];
            var gTot = 0;
            console.log($('.newStly tr:not(:first-child'));
            $('.newStly tr:not(:first-child').each(function (i) {
                $(this).find('td').each(function (j) {

                    if (j > 0) {
                        GArr[j - 1] = Number(GArr[j - 1] || 0) + Number($(this).text() || 0)
                    }

                });
            });
            console.log(GArr);

            str = '';
            str = '<td>Order Total</td>'
            for (var h = 0; h < GArr.length; h++) {
                str += '<td style="text-align: right">' + (GArr[h] || 0).toFixed(2) + '</td>';
            }
             $('.newStly tr:last').after('<tr class="overTot">' + str + '</tr>');
            //            console.log($('.newGrds tr:first td'));
            $('.newStly tr:first td').each(function (i) {
                var remove = 0;
                var tds = $(this).parents('table').find('tr td:nth-child(' + (i + 1) + ')')

                tds.each(function (j) {
                    if (this.innerHTML == '' || this.innerHTML == '0' || this.innerHTML == '0.00') remove++;
                });
                console.log($('.newStly tr:not(:first-child').length);

                console.log(remove);
                if (remove == ($('.newStly tr:not(:first-child').length)) {
                    //  $(this).hide();
                    tds.hide();
                }
            });
        });
    </script>
    <style type="text/css">
        body
        {
            padding: 10px;
        }
        
        .mGrid td, .mGrid th
        {
            padding: 2px 8px;
        }
        
        .Routes
        {
            background-color: #39435C;
            color: #fff;
        }
        
        .subTot
        {
            background-color: #99FFFF;
        }
        
        .overTot
        {
            background-color: #6b7794;
            color: #003ff7;
		
           
        }
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
       
</style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <center>
            <br />
            <div>
                CUSTOMER WISE SALES ANALYSIS REPORT for - <b>
                    <asp:Label ID="lblyear" runat="server"></asp:Label></b>
            </div>
            <div style="text-align: left; padding: 2px 50px;">
                <b>
                    <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                    <asp:HiddenField ID="hidn_sf_code" runat="server" />
                    <asp:HiddenField ID="hdnYear" runat="server" />
                    <asp:HiddenField ID="hdnMonth" runat="server" />
                </b>
            </div>
            <div>
            </div>
            <div>
                <div class="row" style="padding: 5px 15px;">
                    
                        <div id="printableArea" class="page">
                            <table id="Product_Table" class="mGrid" style="border-collapse: collapse">
                                <thead>
                                </thead>
                                <tbody>
                                </tbody>
                                <tfoot>
                                </tfoot>
                            </table>
                            <asp:Table ID="tbl" runat="server" class="newStly" GridLines="Both"
                                Width="95%">
                            </asp:Table>
                        </div>
                    
                </div>
            </div>
        </center>
    </asp:Panel>
    </form>
</body>
</html>
