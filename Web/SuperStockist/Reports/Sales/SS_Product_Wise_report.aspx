<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Product_Wise_report.aspx.cs" Inherits="SuperStockist_Reports_Sales_SS_Product_Wise_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <title>Product Wise Report </title>
        <%--   <!-- Bootstrap 4 CSS -->
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />--%>

        <!-- jQuery -->
        <%--<script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>--%>

        <%--        <!-- Bootstrap 4 JS -->
        <script type="text/javascript"  src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>--%>
        <style type="text/css">
            .form-group {
                position: relative;
                margin-bottom: 30px;
            }

            label {
                position: absolute;
                top: 10;
                left: 0;
                font-size: 16px;
                color: #888;
                transform-origin: left top;
                /*pointer-events: none;*/
                transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
            }

            .form-controls {
                border: none !important;
                border-radius: 0 !important;
                box-shadow: none !important;
                background-color: transparent !important;
                font-size: 18px !important;
                padding: 0 !important;
                /*padding-bottom: 10px !important;*/
                margin-top: 30px !important;
                width: 100% !important;
                height: auto !important;
                transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1) !important;
            }

            .underline {
                position: absolute;
                bottom: 0;
                left: 0;
                height: 1px;
                width: 100%;
                background-color: #888;
                transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
            }

            .form-control:focus {
                outline: none;
            }

                .form-control:focus ~ label,
                .form-control:valid ~ label {
                    transform: translateY(-40%) scale(0.8);
                    color: #333;
                }

                .form-control:focus ~ .underline,
                .form-control:valid ~ .underline {
                    transform: scaleX(1);
                    background-color: #333;
                }

            /* Animation for label */
            label.active {
                transform: translateY(-40%) scale(0.8);
                color: #333;
            }

            /* Animation for underline */
            .underline.active {
                transform: scaleX(1);
                background-color: #333;
            }

            .rowdiv {
                border: 1px solid lightgray;
                border-radius: 10px;
                background: azure;
                padding-top: 9px;
            }
            .export-button {
                width: 35px;
                height: 42px;
                //background-color: #4CAF50; /* Green */
                border: none;
                color: transparent; /* Make the text color transparent */
                padding: 15px 32px;
                text-align: center;
                text-decoration: none;
                display: inline-block;
                font-size: 16px;
                margin: 4px 2px;
                cursor: pointer;
                float: right;
                margin-right: 10px;
                background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAACXBIWXMAAAsTAAALEwEAmpwYAAACcklEQVR4nGNgGAWjYBSMgmEJDOsNpfRqDX11aw0bdGsNN+vWGj5jGIzAvt6eRb9GX1uvxiBOt8Zwol6t0W69WsM3erVG/9HxQLuVQb3Uilev2thGt9YoDeRY3RrDI3o1ht+xOXbAPaBboSsIdmyNUb5ujdEi3Rqjq3o1Rn+JdeyAe0CPAofiwj7rA8jC3uv9jwxpD/isDyA99kY9UDsaA/8HXRJCBjCH4QOjHtBDCr3Obd0ooVO+qgpF3q7d6f+XH1/g8j07+gZXDBjWm/6/9eI23PC7r+7+N6gzgcsvPb4cLnfn5Z3/Rg1mg68YTZmfjhJCuUsKwOIevT7/f/7+CRb79+/f/+R5aYO3Hth7bT/cA5ceXwaLbbmwFS62+cKWwZ2Jvfp8//+AhjYIlKws///3318w+9P3T/8dO10Gtwf0ao3+zzowB27Jx28f4ez2LZ1Doxg1b7b+//LjSxTLrj+78d+gHpGpB20e0Ks1+m/VaofhgWfvn/03a7IaGh5YfXot3OGvP7+Bs2funz34k1Di3BRwUQkC3399/5+3tBBuIago9Z0QOHg9YNJo8f/uq3twC+YcnAcWP3r7GFzs2J0Tg9cDMw/MRil9bNscwOKhUyPgRSkIlK6sGHx5IGhy6P9ff37hbOvsvLwLJV+AMvqg8YBBncn/8w8vwB0IKoFMmyxRHTch4P+fv3/gahYfWzr4kpAehXjUAwMdA4OmItMb9UDAaAwMqSTkvd7/MMOIG9zFNbyuW2tkDJsLGNTD6+RMcOjVGnWAZmMG7QQHCYBRq95QRbfOOFS31rBNr8Zom16N4XNSDBgFo2AUjAKGIQEAkqNB3aFhJ4wAAAAASUVORK5CYII=);
                background-size: contain; /* Scale the icon to fit the button */
                background-repeat: no-repeat; /* Do not repeat the icon */
                background-position: center; /* Center the icon within the button */
                transition: transform 0.2s; /* Add a transition effect */
            }

                .export-button:hover {
                    transform: scale(1.2); /* Scale up the button on hover */
                }

              .spinner {
                margin: 100px auto;
                width: 50px;
                height: 40px;
                text-align: center;
                font-size: 10px;
            }

                .spinner > div {
                    background-color: #333;
                    height: 100%;
                    width: 6px;
                    display: inline-block;
                    -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                    animation: sk-stretchdelay 1.2s infinite ease-in-out;
                }

                .spinner .rect2 {
                    -webkit-animation-delay: -1.1s;
                    animation-delay: -1.1s;
                }

                .spinner .rect3 {
                    -webkit-animation-delay: -1.0s;
                    animation-delay: -1.0s;
                }

                .spinner .rect4 {
                    -webkit-animation-delay: -0.9s;
                    animation-delay: -0.9s;
                }

                .spinner .rect5 {
                    -webkit-animation-delay: -0.8s;
                    animation-delay: -0.8s;
                }

            @-webkit-keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    -webkit-transform: scaleY(1.0);
                }
            }

            @keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    transform: scaleY(0.4);
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    transform: scaleY(1.0);
                    -webkit-transform: scaleY(1.0);
                }
            }

            .spinnner_div {
                width: 1200px;
                height: 1000px;
                background: rgba(255, 255, 255, 0.1);
                backdrop-filter: blur(2px);
                position: absolute;
                z-index: 100;
                overflow-y: hidden;
            }
        </style>
    </head>

    <body>
        <script type="text/javascript">

    var Details_array = [];


            $(document).ready(function () {

                $('.form-control').on('focus', function () {
                    $(this).siblings('label').addClass('active');
                    $(this).siblings('.underline').addClass('active');
                }).on('blur', function () {
                    if (!this.value) {
                        $(this).siblings('label').removeClass('active');
                        $(this).siblings('.underline').removeClass('active');
                    }
                });
            });
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (Details_array.length > 0) {
                for (var $i = 0; $i < Details_array.length; $i++) {
                    slno = $i + 1;
                    var rwStr = "";
                    rwStr += "<tr><td>" + slno + "</td><td>" + Details_array[$i].Trans_Inv_Slno + "</td><td>" + Details_array[$i].Invoice_Date + "</td><td>" + Details_array[$i].Stockist_Name + "</td><td>" + Details_array[$i].ListedDr_Name + "</td><td>" + Details_array[$i].Product_Code + "</td><td>" + Details_array[$i].Product_Name + "</td><td>" + Details_array[$i].Unit + "</td><td>" + parseFloat(Details_array[$i].Price).toFixed(2) + "</td><td>" + Details_array[$i].Quantity + "</td><td>" + parseFloat(Details_array[$i].Price * Details_array[$i].Quantity).toFixed(2) + "</td><td>" + Details_array[$i].discount + "</td><td>" + Details_array[$i].Tax + "</td><td>" + Details_array[$i].Amount + "</td></tr>";
                    $("#OrderList tbody").append(rwStr);
                }
            }
        }

        $(document).on("click", ".btnview", function () {
            $('.spinnner_div').show();
            setTimeout(loadfunction, 500);

        });

        function loadfunction() {
            var From_Date = $('#txtfrdate').val();
            var To_Date = $('#ttxtodate').val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Product_Wise_report.aspx/Get_Report_Details",
                data: "{'FromDate':'" + From_Date + "','ToDate':'" + To_Date + "'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').show();
                    Details_array = JSON.parse(data.d) || [];
                    ReloadTable();
                    if (Details_array.length > 0) {
                        $("#div_id").show();
                        $(".export-button").show();
                        //exportToExcel();
                    }
                    else {
                        $("#div_id").hide();
                        $(".export-button").hide();
                        alert('No Data Available');
                        return false;
                    }
                    $('.spinnner_div').hide();
                },
                error: function (result) {
                    $('.spinnner_div').hide();
                }
            });
        }

    function exportToExcel() {
        $('.spinnner_div').show();
        var htmls = "";
        var uri = 'data:application/vnd.ms-excel;base64,';
        var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
        var base64 = function (s) {
            return window.btoa(unescape(encodeURIComponent(s)))
        };
        var format = function (s, c) {
            return s.replace(/{(\w+)}/g, function (m, p) {
                return c[p];
            })
        };
        htmls = document.getElementById("div_id").innerHTML;

        var ctx = {
            worksheet: 'Worksheet',
            table: htmls
        }
        var link = document.createElement("a");
        var tets = 'Product_Wise_Secondary_Report' + '.xls';   //create fname

        link.download = tets;
        link.href = uri + base64(format(template, ctx));
        $('.spinnner_div').hide();
        link.click();
    }


        </script>
       
        <%-- <div >
            <p> Product Wise Report </p>
        </div>--%>
         <%-- loading div --%>
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left:438px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <%-- loading div --%>
        <fieldset>
            <div class="row rowdiv">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div class="form-group">
                        <label for="inputField">From Date</label>
                        <input type="date" class="form-control form-controls" id="txtfrdate" autocomplete="off" />
                        <div class="underline"></div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div class="form-group">
                        <label for="inputField">To Date</label>
                        <input type="date" class="form-control form-controls" id="ttxtodate" autocomplete="off" />
                        <div class="underline"></div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">

                    <input type="button" style="margin-top: 31px;" class="form-control btn-primary btnview" id="btn" value="Search" />

                </div>

            </div>
            <legend>Product Wise Report </legend>
        </fieldset>
         <div class="row">
          <button class="export-button" onclick="exportToExcel()" style="display:none;">Export to Excel</button>
         </div>
        <div id="div_id" style="display: none;margin-right: -15px;margin-left: -15px;margin-top:10px;background:white;border: 1px solid lightgrey; background: white;border-radius: 10px;padding:5px;">
         
            <table id="OrderList">
                <thead style="border-bottom: 2px solid #428bca;white-space: pre;">
                    <tr>
                        <th>Sl no</th>
                        <th>Order ID</th>
                        <th>Order Date</th>
                        <th>Super Stockist</th>
                        <th>Distributor</th>
                        <th>Product Code</th>
                        <th>Product Name</th>
                        <th>Unit</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Gross</th>
                        <th>Discount</th>
                        <th>Tax</th>
                        <th>Net</th>

                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </body>
    </html>
</asp:Content>

