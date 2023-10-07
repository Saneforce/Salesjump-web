<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Purchase_Order.aspx.cs" Inherits="SuperStockist_Purchase_Order_SS_Purchase_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <%--  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>--%>
    <%-- <link href="../../../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <script src="../../../alertstyle/jquery-confirm.min.js"></script>--%>
    <link href="../../../ownjs_custom/build/toastr.min.css" rel="stylesheet" />
    <script src="../../../ownjs_custom/build/toastr.min.js"></script>
    <a href="../../../ownjs_custom/build/toastr.js.map"></a>
    <style>
        .chosen-container {
            width: 360px !important;
        }

        input[type='text'], select {
            border: 1.5px solid #19a4c6a3 !important;
            background: aliceblue;
        }

        .table > thead > tr > th {
            vertical-align: bottom;
            border-bottom: 2px solid #19a4c6a3;
        }

        .txtblue {
            border: 1.5px solid #19a4c6a3 !important;
            background: aliceblue;
            /*border: 1px solid #19a4c6!important;*/
        }

        .chosen-container-single .chosen-single {
            border: 1.5px solid #19a4c6a3 !important;
            height: 30px;
            background: aliceblue !important;
        }

        .card {
            border: 1.5px solid #b2ebf9b5 !important;
        }

        .select2-container--default .select2-selection--single {
            border: 1.5px solid #19a4c6a3 !important;
            background: aliceblue !important;
        }

        .ms-options-wrap > button:focus, .ms-options-wrap > button {
            border: 1.5px solid #19a4c6a3 !important;
        }

        .stockClass {
            font-size: 12px;
            font-weight: bolder;
        }

        .fa-stack-overflow:before {
            content: "\f16c";
            margin-right: 6px;
            margin-left: 6px;
            color: #00bcd4;
        }

        .fa-tags:before {
            content: "\f02c";
            color: #27c24c;
            margin-right: 6px;
        }

        .fa-code:before {
            content: "\f121";
            margin-left: 6px;
            margin-right: 6px;
            color: #0075ff;
            font-size: 13px;
            font-weight: bolder;
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

        .modalmul {
            display: none;
            position: fixed;
            top: 0;
            right: -40%;
            width: 40%;
            height: 100%;
            background-color: white;
            overflow-x: hidden;
            transition: 0.5s;
        }

        .modalmul-content {
            padding: 20px;
        }

        .slide-in {
            right: 0;
            display: block;
        }

.card-container {
 /*    display: flex;
    flex-wrap: wrap; 
    justify-content: space-between; 
    max-height: 500px; 
    overflow-y: auto;*/
    display: flex;
    flex-wrap: wrap; 
    justify-content: space-between; 
    border: 1px solid #67c2db;
    padding: 8px;
    border-radius: 5px;
    margin-top: 8px;
    max-height: 339px;
    color: #757575;
    overflow-y: scroll;
}
#productContainer{
    border: 1px solid #67c2db;
    padding: 8px;
    border-radius: 5px;
    margin-top: 8px;
    max-height: 339px;
    color: #757575;
    overflow-y: scroll;
}
.product-card {
    /*width: calc(50% - 20px);*/ 
    
}


.product-name {
    font-size: 16px;
    font-weight: bold;
}


.product-checkbox {
    margin-right: 5px;
}
    </style>
    <link href="../../../fontawesome-free-6.2.1-web/css/all.min.css" rel="stylesheet" />
    <script src="../../../fontawesome-free-6.2.1-web/js/all.min.js"></script>
    <form id="frm1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="spinnner_div" style="display: none;">
            <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                <div class="rect1" style="background: #1a60d3;"></div>
                <div class="rect2" style="background: #DB4437;"></div>
                <div class="rect3" style="background: #F4B400;"></div>
                <div class="rect4" style="background: #0F9D58;"></div>
                <div class="rect5" style="background: orangered;"></div>
            </div>
        </div>
        <div class="col-lg-12 sub-header" style="padding-bottom: 10px;">
            Purchase Request
        </div>
        <div style="padding: 8px; margin-top: 0;" class="card">
            <div class="row">
                <div class="col-md-4">
                    <label class="control-label">Company Name</label>
                    <select style="width: 265px; margin-left: 13px;" class="form-group" id="To_Address">
                        <option value="0">---Select---</option>
                    </select>
                    <label style="margin-left: 12px; font-weight: 100; padding-top: 20px;" id="lbl_To_Address" class="control-label"></label>
                </div>
                <div class="col-md-4">
                    <label class="control-label">Billing Address</label>
                    <div style="font-weight: 100;" id="lbl_Bill_Address" class="control-label"></div>
                </div>
                <div class="col-md-1">
                    <div style="width: 300px;">
                        <label style="width: 131px;" class="control-label">Shipping Address</label>&nbsp;&nbsp;
                        <input type="checkbox" id="chck" />Same As Billing Address
                    </div>
                    <textarea style="width: 330px; background-color: aliceblue" rows="4" class="txtblue form-control" id="txt_Ship_add"></textarea>
                </div>
            </div>
            <div class="row" style="margin-top: 22px; padding-right: 4px;">
                <div class="col-sm-6">
                    <label>Purchase Date</label>
                    <input type="date" style="width: 270px; margin-left: 20px;" class="control-group txtblue" id="txt_Purchase_Date" readonly="readonly" />
                </div>
                <div class="col-sm-6" style="padding: 0px 0px 0px 143px;">
                    <label>Expected Date</label>

                    <input type="date" style="width: 270px; margin-left: 20px;" class="control-group txtblue" id="txt_Expected_Date" />
                </div>
            </div>
        </div>

        <div class="row" style="text-align: center;">
            <div id="myDIV">
            </div>
        </div>
        <br />
        <div class="row card" style="background: #fff; margin: 0;">
            <div class="col-sm-12" style="padding: 15px">
                <div class="tableBodyScroll">
                    <table class="table table-hover" id="OrderEntry">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left">S.No</th>
                                <th style="text-align: left">Product</th>
                                <th style="text-align: left; word-spacing: 41px; padding: 0px 0px 8px 50px;">Unit Qty</th>
                                <th style="text-align: right">Rate</th>
                                <th style="text-align: right">Free</th>
                                <th style="text-align: right">Dis Val</th>
                                <th style="text-align: right">Total</th>
                                <th style="text-align: right">Tax</th>
                                <th style="text-align: right">Gross_Total</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
        <br />
        <div class="row" style="margin-left: 0%;">
            <div>
                <div style="text-align: left" colspan="9">
                    <button type="button" class="btn btn-success" id="btnAddmulti" style="font-size: 12px">+ Add Multiple Product </button>
                    <button type="button" class="btn btn-success" id="btnAdd" style="font-size: 12px">+ Add Product </button>
                    <button type="button" class="btn btn-danger" id="btnDelRow" style="font-size: 12px">- Remove Product</button>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-8">
                <div class="card freecard">
                    <div class="card-body table-responsive">
                        <div style="white-space: nowrap; padding: 0px 0px 7px 7px; font-weight: 900;">View Offer Products</div>
                        <div class="tddd">
                            <table id="free_table" class="table table-hover">
                                <thead class="text-warning">
                                    <tr>
                                        <th style="width: 14%;">SlNo</th>
                                        <th style="width: 22%;">Product Code </th>
                                        <th style="width: 37%;">Product Name</th>
                                        <th style="width: 14%;">Free</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                                <tfoot>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-offset-8 form-horizontal">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    Subtotal :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    Dis Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    Tax Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 120px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    TCS Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="tot_tcs" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal div_cgst" style="margin-top: 80px; display: none;">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    CGST Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <%--    <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />--%>
                        <input data-cell="G1" id="txt_cgst" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal div_sgst" style="margin-top: 123px; display: none;">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    SGST Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="txt_sgst" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal div_gross" style="margin-top: 160px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                    Gross Total
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="gross" data-format="0,0.00" class="form-control txtblue" readonly style="background-color: aliceblue;" />
                    </div>
                </div>
            </div>
            <div style="left: 0; position: absolute; padding: 61px 0px 0px 1009px;">
                <input type="button" id="svorders" class="btn btn-lg btn-primary ews" value="Save" style="float: right; width: 160%;">
            </div>
        </div>
        <div id="multiProductModal" class="modalmul">
            <div class="modalmul-content" style="width: 100%; margin-top: 20px;">
                <div class="modal-header">
                    <h4>Select Products to Add</h4>
                </div>
                <div class="modal-body">

                    <select id="brandDropdown">
                    </select>
                    <div class="row">
    <div class="col-lg-7">
     </div>
    <div class="col-lg-5 offset-lg-2">
      <input type="text" id="productSearch" placeholder="Search Products" class="form-control" style="margin-top: 10px;">
    </div>
</div>
                    <div id="productContainer">
                     
                      </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="submitBtn" class="btn btn-success">Submit</button>
                    <button type="button" id="closeBtn" class="btn btn-danger">Close</button>
                </div>
            </div>
        </div>


    </form>
    <style type="text/css">
        .chosen-container {
            width: 100% !important;
        }

        .ui-menu-item > a {
            display: block;
        }

        .fixed {
            position: fixed;
            width: 97%;
            bottom: 10px;
        }

        .ms-options {
            cursor: pointer;
        }

        .example {
            display: contents;
            position: fixed;
            margin-left: 156px;
        }

        .focus {
            background-color: #ff6666;
        }

        .focus1 {
            background-color: white;
        }

        .tr_class {
            background-color: #9fdfbe;
        }

        .category {
            border: none;
            outline: none;
            padding: 10px 16px;
            background-color: #f1f1f1;
            cursor: pointer;
            font-size: 15px;
        }

        .active, .btn:hover {
            background-color: #666;
            color: white;
        }

        .Spinner-Input {
            width: 43% !important;
        }
    </style>
    <script src="../../../js/lib/bootstrap-select.min.js"></script>
    <link href="../../../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/select2.min.css" rel="stylesheet" />
    <script src="../../../js/select2.full.min.js"></script>
    <script src="../../../js/daterangepicker-3.0.5.min.js"></script>
    <script src="../../../js/jquery.multiselect.js"></script>
    <script src="../../../js/moment-2.24.0.min.js"></script>
    <script type="text/javascript">

        var Orders = [];
        var Selected_Com_Name = ''; var Collected_data = []; var gTot = 0; var AllOrders = []; var detailsdata = []; var arr = []; Allrate = []; var calc_all_tac = 0; var calc_all_dis = 0; var calc_all_tot = 0;
        ArrPriOrd = []; var CQ = ''; var headdatas = []; var divcode; filtrkey = '0'; var ComDetails = []; var a = 0; var scheme = []; var add = []; var set = 0; var click_text = '';
        pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sale_Erp_Code,Product_Detail_Name,price"; var All_unit = []; var purchase_order = []; var sr = ''; var row_index = '';
        var z = 1; var jkl = 0; var sap_code = ''; var Pri_order_id = ''; var Pri_order_id1 = ''; var All_Tax = []; NewOrd = []; Prds = "";
        var Retailer_State = 0; var Cat_Details = [];

        var Dist_state_code = ("<%=Session["State"].ToString()%>");

        $(document).ready(function () {

            $("#submitBtn").click(function () {
               var selectedProducts = [];

                $(".product-checkbox:checked").each(function () {
                    var productValue = $(this).val();
                    selectedProducts.push(productValue);
                });

                 $.each(selectedProducts, function (index, productValue) {
                     AddRow(0);
                     $('#OrderEntry tr:last').find(".ddlProd").val(productValue).trigger('chosen:updated');
                     $('#OrderEntry tr:last').find('.ddlProd').trigger('change');
                });

                $("#multiProductModal").modal("hide");
                $("#multiProductModal").removeClass("slide-in");
            });

            $("#productSearch").keypress(function (e) {
                if (e.which === 13) { // Check if Enter key is pressed
                    handleSearch();
                }
            });
            $("#brandDropdown").change(function () {
                var selectedValue = $(this).val(); // Get the selected value from the dropdown

                // Show all checkboxes initially
                $(".product-card").show();

                if (selectedValue !== "-1") {
                    // If a specific category is selected (not "ALL"), hide checkboxes that don't match the selected value
                    $(".product-card").each(function () {
                        var catId = $(this).find(".product-checkbox").attr("cat_id");
                        if (catId !== selectedValue) {
                            $(this).hide();
                        }
                    });
                }
            });

            $("#btnAddmulti").click(function () {
                for (var g = 0; g < Cat_Details.length; g++) {


                    $('#brandDropdown').append('<option class="category" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</option>');

                }
                $('#brandDropdown').chosen();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SS_Purchase_Order.aspx/GetProducts",
                    data: "{'div':'" + Div_Code + "'}",
                    dataType: "json",
                    success: function (data) {
                        var itms = JSON.parse(data.d) || [];
                        var productContainer = $('#productContainer');

                        // Clear previous products
                        productContainer.empty();

                        for (var i = 0; i < itms.length; i++) {
                            var product_code = itms[i].Product_Detail_Code;
                            var Product_Detail_Name = itms[i].Product_Detail_Name;

                            // Create a product card with a checkbox
                            var productCard = $('<div class="product-card">');
                            var checkbox = $('<input type="checkbox" class="product-checkbox" id=' + product_code + ' value=' + product_code + ' cat_id=' + itms[i].Product_Cat_Code +'>');
                            var productName = $('<label>' + Product_Detail_Name + '</label>');

                            // Append the checkbox and product name to the card
                            productCard.append(checkbox);
                            productCard.append(productName);

                            // Append the product card to the container
                            productContainer.append(productCard);
                        }
                       
                        
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $("#multiProductModal").addClass("slide-in");
            });

            $("#closeBtn").click(function () {
                $("#multiProductModal").removeClass("slide-in");
            });
            toastr.options = {
                positionClass: "toast-bottom-right",
                showDuration: 300,
                hideDuration: 500

            }
            resetSL = function () {
                $(".rwsl").each(function () {
                    $(this).text($(this).closest('tr').index() + 1);
                });
            }

            $("#full_div").click(function (e) {
                e.stopPropagation();
                return false;
            });

            var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
            var Div_Code = ("<%=Session["div_code"].ToString()%>");

            $("#txt_Purchase_Date").attr('readonly', true);
            $('#chck').prop('checked', true);

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            today = yyyy + '-' + mm + '-' + dd;

            $("#txt_Purchase_Date").val(today);
            $('#txt_Expected_Date').val(today);
            $('#txt_Expected_Date').attr('min', today);
            $('#txt_Purchase_Date').attr('readonly', true);

            //$(document).on("click", ".Spinner-Input", function (e) {
            //    e.stopPropagation();
            //    $ix = $(this).find(".Spinner-Modal");
            //    $dsp = $($ix).hasClass("open");
            //    $(".Spinner-Modal").removeClass("open");
            //    if ($dsp == false)
            //        $($ix).addClass("open");
            //})

            $(document).on('keypress', '.txtQty ', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $(document).on("click", "#btnAdd", function () {
                AddRow(1);
            })

            $(document).on("click", "#btnDelRow", function () {
                DelRow();
            })

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/DisplayCompanyDetails",
                dataType: "json",
                success: function (data) {
                    ComDetails = data.d;
                    if (data.d.length > 0) {
                        $.each(data.d, function () {
                            $('#To_Address').append($("<option></option>").val(this['HO_ID']).html(this['Name'])).trigger('chosen:updated').css("width", "100%");;;

                        });
                    }
                },
                error: function (result) {
                }
            });
            $('#To_Address').chosen();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/Dis_stk_sstk_Details",
                dataType: "json",
                success: function (data) {
                    add = JSON.parse(data.d);
                    for (var a = 0; a < add.length; a++) {
                        $('#lbl_Bill_Address').text(add[a].Address);
                        $('#txt_Ship_add').text(add[a].Address);
                        $('#txt_Ship_add').attr('readonly', true);
                    }
                },
                error: function (result) {
                }
            });
            //function handleSearch() {
            //    var searchText = $("#productSearch").val().toLowerCase();
            //    var selectedValue = $("#brandDropdown").val();

            //    // $(".product-card").hide();

            //    if (searchText !== "" || selectedValue !== "-1") {
            //        $(".product-card").hide();
            //        $(".product-card").each(function () {
            //            var productLabel = $(this).find("label").text().toLowerCase();
            //            var catId = $(this).find(".product-checkbox").attr("cat_id");
            //            if ((searchText === "" || productLabel.indexOf(searchText) !== -1) &&
            //                (selectedValue === "-1" || catId === selectedValue)) {
            //                $(this).show();
            //            }
            //        });
            //    }
            //}
            function handleSearch() {
                var searchText = $("#productSearch").val().toLowerCase();
                var selectedValue = $("#brandDropdown").val();

                $(".product-card").hide();

                if (searchText === "" && selectedValue !== "-1") {
                    $(".product-card").each(function () {
                        var catId = $(this).find(".product-checkbox").attr("cat_id");
                        if (catId === selectedValue) {
                            $(this).show();
                        }
                    });
                } else if (searchText !== "" || selectedValue !== "-1") {
                     $(".product-card").each(function () {
                        var productLabel = $(this).find("label").text().toLowerCase();
                        var catId = $(this).find(".product-checkbox").attr("cat_id");
                        if ((searchText === "" || productLabel.indexOf(searchText) !== -1) &&
                            (selectedValue === "-1" || catId === selectedValue)) {
                            $(this).show();
                        }
                    });
                }
            }

            function populateProductCards(brand) {
                var productContainer = $('#productContainer');
                productContainer.empty(); 

                if (productData.hasOwnProperty(brand)) {
                    $.each(productData[brand], function (index, product) {
                        var productCard = $('<div class="product-card">');
                        var checkbox = $('<input type="checkbox" class="product-checkbox">');
                        var productName = $('<label>' + product + '</label>');

                        productCard.append(checkbox);
                        productCard.append(productName);
                        productContainer.append(productCard);
                    });
                }
            }
            function bind_category() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SS_Purchase_Order.aspx/Get_Category",
                    dataType: "json",
                    success: function (data) {
                        cat = JSON.parse(data.d);
                        $.each(cat, function () {
                            $('#txtfilter').append($("<option></option>").val(this['Product_Cat_Code']).html(this['Product_Cat_Name'])).trigger('chosen:updated');
                        });
                    },
                });
                $("#txtfilter").chosen({ allow_single_deselect: true, width: '50%' });
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_myOrders.aspx/Get_Product_unit",
                dataType: "json",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/GetProducts",
                data: "{'div':'" + Div_Code + "'}",
                dataType: "json",
                success: function (data) {
                    itms = JSON.parse(data.d) || [];
                    for ($k = 0; $k < itms.length; $k++) {
                        Prds += "<option value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
                    }
                   // AddRow(0);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $(document).on("change", "#To_Address", function () {

                var comp = [];
                var compVal = $('#To_Address option:selected').val();

                comp = ComDetails.filter(function (w) {
                    return (w.HO_ID == compVal && w.State_Code.indexOf(Dist_state_code) > -1);
                });

                if (comp.length <= 0) {
                    clear();
                }

                Selected_Com_Name = $(this).val();
                if (Selected_Com_Name != "0") {
                    var h = ComDetails.filter(function (a) {
                        return (a.HO_ID == Selected_Com_Name)
                    });
                    $('#lbl_To_Address').text(h[0].Division_Add1);
                }
                else {
                    $('#lbl_To_Address').html('');
                    toastr.info('Alert', 'Please Select Company!');

                    //$.confirm({
                    //    title: 'Confirm!',
                    //    content: 'Please Select Company!',
                    //    type: 'red',
                    //    typeAnimated: true,
                    //    autoClose: 'action|8000',
                    //    icon: 'fa fa-warning ',
                    //    buttons: {
                    //        tryAgain: {
                    //            text: 'OK',
                    //            btnClass: 'btn-red',
                    //            action: function () {

                    //            }
                    //        }
                    //    }
                    //});

                }
            });

            function clear() {
                $('#OrderEntry').find('tbody tr').remove();
                $('#free_table').find('tbody tr').remove();
                $("[id*=sub_tot]").val('');
                $("[id*=txt_cgst]").val('');
                $("[id*=txt_sgst]").val('');
                $("[id*=Txt_Dis_tot]").val('');
                $("[id*=gross").val('');
                NewOrd = [];
            }

            $(document).on("click", "#chck", function () {

                if ($("#chck").is(":checked")) {
                    $("#txt_Ship_add").attr("readonly", true);

                    var d = [];

                    var d = add.filter(function (a) {
                        return (a.Code == "<%=Session["Sf_Code"].ToString()%>")
                    });

                    $("#txt_Ship_add").val(d[0].Address);

                } else {
                    $("#txt_Ship_add").attr("readonly", false);
                }
            });

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "SS_Purchase_Order.aspx/getscheme",
                data: "{'date':'" + today + "','Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                dataType: "json",
                async: false,
                success: function (data) {
                    scheme = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SS_Purchase_Order.aspx/DisplayProduct",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        AddRow(1);
                    },
                    error: function (result) {
                    }
                });
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/GetProducts",
                data: "{'div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    itms = JSON.parse(data.d) || [];
                    for ($k = 0; $k < itms.length; $k++) {
                        Prds += "<option value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                url: "SS_Purchase_Order.aspx/getratenew",
                dataType: "json",
                success: function (data) {
                    Allrate = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/Get_Product_unit",
                dataType: "json",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/Get_Product_Tax",
                dataType: "json",
                success: function (data) {
                    All_Tax = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order.aspx/Get_Product_Cat_Details",
                dataType: "json",
                success: function (data) {
                    Cat_Details = JSON.parse(data.d) || [];
                    $('#myDIV').html('');
                    for (var g = 0; g < Cat_Details.length; g++) {

                        if (Cat_Details[g].Product_Cat_Code == -1) {
                            $('#myDIV').append('<button class="btn category active" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</button>');
                        }
                        else {
                            $('#myDIV').append('<button class="btn category" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</button>');
                        }
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            function Get_unit(code) {

                var filter_unit = []; units = ""; units1 = "";

                filter_unit = All_unit.filter(function (w) {
                    return (code == w.Product_Detail_Code);
                });

                if (filter_unit.length > 0) {

                    for (var z = 0; z < filter_unit.length; z++) {
                        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                    }
                }
                return units
            }

            function Get_Tax(pro_code, itr) {

                var tax_filter = [];

                tax_filter = All_Tax.filter(function (r) {
                    return (r.Product_Detail_Code == pro_code)
                });

                for (var z = 0; z < tax_filter.length; z++) {

                    if (tax_filter[z].Tax_Type == 'SGST') {

                        $(itr).find('.sgst_taxname').text(tax_filter[z].Tax_name);
                        $(itr).find('.hid_sgst').val(tax_filter[z].Tax_Val);

                    }
                    else if (tax_filter[z].Tax_Type == 'CGST') {

                        $(itr).find('.cgst_taxname').text(tax_filter[z].Tax_name);
                        $(itr).find('.hid_cgst').val(tax_filter[z].Tax_Val);
                    }
                    else if (tax_filter[z].Tax_Type == 'IGST') {

                    }
                    else {

                        $(itr).find('.sgst_taxname').text(tax_filter[z].Tax_name);
                        $(itr).find('.hid_sgst').val(tax_filter[z].Tax_Val);
                        $(itr).find('.cgst_taxname').text(tax_filter[z].Tax_name);
                        $(itr).find('.hid_cgst').val(tax_filter[z].Tax_Val);
                    }
                }

            }


            $(document).on("click", ".category", function (e) {
                e.preventDefault();
                $('.category').removeClass('active');
                $('.category').css('color', 'black');
                $(this).addClass('active');
                $(this).css('color', 'white');

            });
            $(document).on("click", ".Spinner-Input", function () {
                $ix = $(this).find(".Spinner-Modal");
                $dsp = $($ix).hasClass("open");
                $(".Spinner-Modal").removeClass("open");
                if ($dsp == false)
                    $($ix).addClass("open");
            })

            $(document).on("click", ".Spinner-Modal>ul>li", function () {
                $vl = $(this).attr('val');
				 itr = $(this).closest('tr');
                idx = $(itr).index();
                CalcAmt(itr);
                $(this).closest(".Spinner-Input").find(".Spinner-Value").html($(this).html())
               
                $(itr).find('.txtQty').val(0);
                $(itr).find('.tdAmt').text(0.00);
                $(itr).find('.tdtotal').text(0.00);
                var prdcode = $(this).closest('tr').attr('class');
                var unitval = $(this).attr('id');
                var umoval = $(this).val();
                var filterunit = [];
                filterunit = NewOrd.filter(function (s, key) {
                    return (s.PCd == prdcode && key != idx && s.umo_unit == umoval)
                });
                if (filterunit.length > 0) {
                    $(itr).find('.tdRate').text(0);
                    toastr.warning('Alert', 'Product Units Already Selected!');
                    //$.confirm({
                    //    title: 'Error!',
                    //    content: 'Product Units Already Selected',
                    //    type: 'red',
                    //    typeAnimated: true,
                    //    autoClose: 'action|8000',
                    //    icon: 'fa fa-warning ',
                    //    buttons: {
                    //        tryAgain: {
                    //            text: 'OK',
                    //            btnClass: 'btn-red',
                    //            action: function () {

                    //            }
                    //        }
                    //    }
                    //});
                    return false;
                }
                ans = Allrate.filter(function (t) {
                    return (t.Product_Detail_Code == prdcode && t.Unit_code == umoval.toString());
                });
                if (ans.length > 0) {
                    $(itr).find('.Con_fac').text(ans[0].Sample_Erp_Code);
                }
                else {
                    $(itr).find('.Con_fac').text(1);
                }
                NewOrd[idx].umo_unit = umoval;
                NewOrd[idx].Rate = parseFloat(unitval).toFixed(2);
                $(itr).find('.tdRate').text(parseFloat(unitval).toFixed(2));

            })

            $(document).on('keypress', '.txtQty', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            function AddRow(type) {

                itm = {}
                itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0"; itm.Qty = 1; itm.Qty_c = 1; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0"; itm.Total_Tax = 0; itm.produnit = 0; itm.eQty = 0;
                itm.Tax_details = ''; itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = ''; itm.TCS = ''; itm.TDS = '';
                NewOrd.push(itm);

                tr = $("<tr class='subRow'></tr>");
                $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + ($("#OrderEntry > TBODY > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:27%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;width:250px!important'><option value='0'>--select--</option>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11px;padding: 5px 0px 0px 3px;margin-left: -28px;'></div></td><td style='display:none;' ><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'></select><div class='Spinner-Input'><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>Select</ul></div></div><input type='text' name='txqty' id='txqty' class='txtQty validate' pval='0.00' value   style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>0.00</td><td class='fre' style='text-align:right; padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>0</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>0.00</td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>0.00</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td style='display:none;'><input type='hidden' class='tdtcs' id='tdtcs' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>0.00</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td>");
                $("#OrderEntry > TBODY").append(tr); OrderEntry
                resetSL();
                $('.ddlProd').chosen();
                //$('.ddlProd').select2();

                //$('.ddlProd').on('chosen:open', function () {
                $('.ddlProd').on('chosen:showing_dropdown', function () {
                    var selected_cat_code = $('.btn.active').val();
                    var selected_cat_Name = $('.btn.active').text();
                    var filtered_prd = [];

                    filtered_prd = itms.filter(function (k) {
                        return (k.Product_Cat_Code == selected_cat_code);
                    });

                    $(this).html('');
                    filtered_prd = selected_cat_code == -1 ? itms : filtered_prd;


                    let str = '<option value=-1>--select--</option>';
                    for (var h = 0; h < filtered_prd.length; h++) {
                        str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                    }
                    $(this).html(str);
                    //  $(this).trigger("select2:updated");
                    $(this).trigger("chosen:updated");

                });

                if (type == 1) {
                    event.stopPropagation();
                    //$('#OrderEntry tr:last').find(".ddlProd").trigger('select2:open');
                    $('#OrderEntry tr:last').find(".ddlProd").trigger('chosen:open');
                }

                $(".ddlProd").on("change", function () {

                    itr = $(this).closest('tr');
                    idx = $(itr).index();
                    $(itr).find('.txtQty').val(0);
                    var pro_filter = [];

                    var selected_company_code = $('#To_Address option:selected').val();

                    if (selected_company_code == '' || selected_company_code == 0) {

                        $('.ddlProd').val('');
                        //$('.ddlProd ').select2("destroy");
                        $('.ddlProd ').chosen("destroy");
                        //$('.ddlProd').select2();
                        $('.ddlProd').chosen();
                        toastr.warning('Warning', 'Please select company');

                        return false;
                    }

                    $('.second_row_div').show();
                    sPCd = $(this).val();
                    $(this).closest("tr").attr('class', $(this).val());
                    var P_Name = itr.find('.ddlProd').find('option:selected').text();

                    
                    if (pro_filter.length > 0) {

                        $(itr).find('.ddlProd').val('');
                        // $(itr).find('.ddlProd ').select2("destroy");
                        $(itr).find('.ddlProd ').chosen("destroy");
                        // $(itr).find('.ddlProd').select2();
                        $(itr).find('.ddlProd').chosen();
                        $(itr).find('.Spinner-Value').text('Select');
                        $(itr).find('.tdRate').text("0.00");
                        $(itr).find('.txtQty').val('');
                        NewOrd[idx].Unit = 'Select';
                        NewOrd[idx].umo_unit = '';
                        $(itr).find('.second_row_div').text('');
                        CalcAmt(itr);
                        toastr.warning('Warning', 'Product Already Selected');
                      

                        return false;
                    }

                    Prod = Allrate.filter(function (a) {
                        return (a.Product_Detail_Code == sPCd);
                    })

                    rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0; //tax = parseFloat(Prod[0].Tax_Val || 0);

                    NewOrd[idx].PCd = sPCd;
                    NewOrd[idx].sPCd = $(this).val();
                    NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
                    NewOrd[idx].Unit = Prod[0].product_unit;
                    NewOrd[idx].PName = P_Name;
                    NewOrd[idx].umo_unit = Prod[0].Unit_code;
                    NewOrd[idx].Qty = qt;
                    NewOrd[idx].Free = 0;
                    NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;

                    //var unt_rate = Prod[0].PTS;
                    var unt_rate = 0;
                    //var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                    if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }
                    $(itr).find('.tdRate').text(0);
                    //$(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                    NewOrd[idx].Rate = unt_rate;


                    var tax_filter = []; var tax_arr = []; var filter_unit = []; units = ""; units1 = "";
                    filter_unit = All_unit.filter(function (w) {
                        return (sPCd == w.Product_Detail_Code);
                    });
                    
                    if (filter_unit.length > 0) {
                        if (Prod.length > 0) {
                            
                            var unitrate = 0;
                            var unitconfac = 0;
                            for (var z = 0; z < filter_unit.length; z++) {
                                if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                    unitrate = Prod[0].PTS;
                                    unitconfac = filter_unit[z].Sample_Erp_Code;
                                    units += "<li id=" + unitrate + " conf=" + unitconfac + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " conf=" + unitconfac + "  value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                }
                                 else {
                                    unitconfac = filter_unit[z].Sample_Erp_Code;
                                    unitrate = Prod[0].PTS * filter_unit[z].Sample_Erp_Code
                                    units += "<li id=" + unitrate + " name='itms' conf=" + unitconfac + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " conf=" + unitconfac + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                }
                            }
                        }
                        else {
                            for (var z = 0; z < filter_unit.length; z++) {
                                units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";


                            }
                        }
                    }
                    var ddlUnit = itr.find('.ispinner');
                    ddlUnit.empty();

                    // $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + Prod[0].product_unit + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + Prod[0].product_unit + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                    $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value='0'></div><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                    setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == sPCd)
                    });

                    var comp = [];
                    var compVal = $('#To_Address option:selected').val();

                    comp = ComDetails.filter(function (w) {
                        return (Dist_state_code == w.State_Code && w.HO_ID == compVal);
                    });

                    if (comp.length <= 0) { var Retailer_State = undefined; } else { var Retailer_State = comp[0].State_Code; }
                    if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == sPCd)
                        ////&& (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
                    });
                    $(itr).find('.second_row_div').text('');
                    //$(itr).find('.second_row_div').append("<i class='fa fa-tags'></i><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<i class='fa fa-stack-overflow' ><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<i class='fa fa-code' ></i><label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");
                    $(itr).find('.second_row_div').append("<span style='color:#27c24c;padding: 5px;'><i class='fa-solid fa-tags'></i></span><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<span style='color: Dodgerblue;padding: 5px;'><i class='fa-solid fa-boxes-stacked'></i></span><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<span style='color: #f29900;padding: 5px;'><i class='fa-solid fa-code-compare'></i></span><label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

                    var append = ''; var total_tax_per = 0;

                    if (tax_filter.length > 0) {

                        for (var z = 0; z < tax_filter.length; z++) {
                            //append += "<label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                            append += " <span style='color: red;padding: 5px;'><i class='fa-solid fa-percent'></i></span><label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                            var Push_data = tax_filter[z].Tax_Type;
                            total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                            NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

                            tax_arr.push({
                                pro_code: sPCd,
                                Tax_Code: tax_filter[z].Tax_Id,
                                Tax_Name: tax_filter[z].Tax_Type,
                                Tax_Amt: 0,
                                Tax_Per: tax_filter[z].Tax_Val,
                                umo_code: 0
                            });
                        }
                        NewOrd[idx].Tax_details = tax_arr;
                        NewOrd[idx].Total_Tax = total_tax_per;
                        $(itr).find('.second_row_div').append(append);
                    }
                    $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                    if (parseInt(Prod[0].TotalStock) > 0) {
                        $(itr).find('.stockClass').css("color", "rgb(39 194 76)");
                    }
                    else {
                        $(itr).find('.stockClass').css("color", "red");
                    }
                    $(itr).find('.stockClass').text(Prod[0].TotalStock);
                    $(itr).find('.stockClass').val(Prod[0].TotalStock);
                    $(itr).find('.dis_per').text("0");
                    $(itr).find('.sale_code').html(Prod[0].Sale_Erp_Code);
                    $(itr).find('.tdAmt').html((qt * rt).toFixed(2));
                    $(itr).find('.erp_code').val(Prod[0].Sample_Erp_Code);
                    $(itr).find('.dis_val_class').text(0);
                    $(itr).find('.fre').attr("id", sPCd);
                    $(itr).find('.fre1').attr("id", sPCd);
                    $(itr).find('.tddis_val').text('0.00');
                    $(document).find('.' + sPCd).each(function () {

                        var rowww = $(this).closest('tr');
                        var indxx = $(rowww).index();

                        var Unit_length = Prod.length;
                        var row_length = $('#OrderEntry tbody').find('.' + sPCd).length;

                        var Pre_pro = $(this).closest("tr").attr('Prev_Prod_Code');

                        if (Pre_pro == sPCd) {
                            var row_unit = rowww.find('.Spinner-Value').text();
                            var row_unit_Code = rowww.find('.spinner-Value_val').attr('value');
                        }
                        else {
                            var row_unit = "";
                            var row_unit_Code = "";
                        }
                        $(itr).closest("tr").attr('Prev_Prod_Code', sPCd);
                        if (filter_unit.length >= row_length) {

                            if (row_unit_Code == "" || row_unit_Code == undefined) {

                                $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value' umo='" + Prod[0].Unit_code + "'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                                if (Prod[0].Unit_code == Prod[0].UnitCode)
                                    var unt_rate = Prod[0].PTS;
                                else
                                    var unt_rate = Prod[0].PTS * Prod[0].Sample_Erp_Code;
                                if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                NewOrd[idx].Rate = unt_rate;
                                NewOrd[idx].mrp = Prod[0].MRP_Rate;
                                $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                                NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                                NewOrd[idx].umo_unit = Prod[0].Unit_code;

                            }
                            else {
                                for (var z = 0; z < filter_unit.length; z++) {
                                    if (filter_unit[z].Move_MailFolder_Id == row_unit_Code) {
                                        $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value='0'></div><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                    }
                                }
                            }
                           
                            set = 0;
                        }
                        else {
                            $(itr).find('.ddlProd').val('');
                            $(itr).find('.ddlProd ').chosen("destroy");
                            //$(itr).find('.ddlProd ').select2("destroy");
                            $(itr).find('.ddlProd').chosen();
                            // $(itr).find('.ddlProd').select2();
                            $(itr).find('.Spinner-Value').text('Select');
                            $(itr).find('.tdRate').text("0.00");
                            $(itr).find('.txtQty').val('');
                            $(itr).find('.second_row_div').text('');
                            NewOrd[idx].Unit = 'Select';
                            NewOrd[idx].umo_unit = '';
                            CalcAmt($(itr));
                            toastr.info('Alert', 'Product Units Already Selected');
                            //rjalert('error!', 'Product Units Already Selected', 'error');
                            //alert('Product Units Already Selected');
                            set = 1;
                            return false;
                        }
                    });


                    //getscheme(Prod[0].Sale_Erp_Code, '', '', $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), $(itr).find('.pcode').text(), idx, $(this).find('option:selected').text());
                    $(itr).find('.pcode').html(sPCd);
                    NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
                    CalcAmt(itr);
                })
            }

            function DelRow() {

                $(".slitm:checked").each(function () {

                    itr = $(this).closest('tr');
                    idx = $(itr).index();
                    var prod = itr.find('.ddlProd').val();
                    //getscheme($(itr).find('.sale_code').text(), 0, $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), '', $(itr).find('.pcode').text());
                    $(this).closest('tr').remove();
                    NewOrd.splice(idx, 1);
					idx = -1;
                    if (prod != "") {
                        $(document).find('.' + prod).each(function () {
                            // calc_stock($(this).closest('tr'));
                        });
                    }

                }); resetSL(); ReCalc();
            }

            $(document).on('keyup', '.txtQty', function (e) {

                tr = $(this).closest("tr");
                idx = $(tr).index();
                NewOrd[idx].PCd = tr.closest('tr').find('.ddlProd option:selected ').val();
                NewOrd[idx].PName = tr.closest('tr').find('.ddlProd option:selected ').text();
                NewOrd[idx].Unit = tr.closest('tr').find('.Spinner-Value').text();

                if (e.keyCode == 13) {
                    if ($(this).val() != '' || isNaN($(this).val())) {
                        AddRow(1);
                        return false;
                    }
                }

                un = $(tr).find('.Spinner-Value').text();

                if (un == 'Select') {
                    toastr.info('Alert', 'Please Select UOM');
                    //$.confirm({
                    //    title: 'Error!',
                    //    content: 'Please Select UOM',
                    //    type: 'red',
                    //    typeAnimated: true,
                    //    autoClose: 'action|8000',
                    //    icon: 'fa fa-warning ',
                    //    buttons: {
                    //        tryAgain: {
                    //            text: 'OK',
                    //            btnClass: 'btn-red',
                    //            action: function () {

                    //            }
                    //        }
                    //    }
                    //});

                    $(tr).find('.txtQty').val('');
                    return false;
                }

                var erpcode = $(tr).find('.erp_code').val();

                CQ = $(this).val();
                opcode = $(tr).find(".ddlProd :selected").val();

                var disrate = $(this).closest("tr").find('.tdRate').text();
                result = (CQ * disrate);

                pCode = $(tr).find(".sale_code").text();
                pname = $(tr).find(".ddlProd :selected").text();
                ff = $(tr).find(".fre1").text();

                // getscheme(pCode, CQ, un, tr, ff, '', opcode);

                NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                NewOrd[idx].Qty_c = $(this).val();
                NewOrd[idx].Qty = parseFloat($(this).val()) * $(tr).find('.Con_fac').text();
                NewOrd[idx].Discount = $(tr).find(".dis_val_class").text() || 0;
                NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;
                NewOrd[idx].eQty = $(this).val();
                NewOrd[idx].produnit = $(tr).find('.Con_fac').text();
                CalcAmt(tr);

            });

            function CalcAmt(x) {

                itr = $(x).closest('tr');
                idx = $(itr).index();
                rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
                tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;
				
				if (NewOrd[idx].Sub_Total !== 0) {
						NewOrd[idx].Sub_Total = parseFloat(NewOrd[idx].Sub_Total).toFixed(2);
				} else {
						NewOrd[idx].Sub_Total = "0.00"; // Or any other appropriate default value
				}
                //NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);

                if ($(itr).find('.dis_per').text() == "") { var dis_per = 0; }
                if (NewOrd[idx].Total_Tax == '' || isNaN(NewOrd[idx].Total_Tax)) { NewOrd[idx].Total_Tax = 0; }

                NewOrd[idx].Dis_value = (parseFloat(dis_per) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                NewOrd[idx].Sub_Total = (parseFloat(NewOrd[idx].Sub_Total) - parseFloat(NewOrd[idx].Dis_value)).toFixed(2);

                for (var g = 0; g < NewOrd[idx].Tax_details.length; g++) {
                    NewOrd[idx].Tax_details[g].Tax_Amt = parseFloat(NewOrd[idx].Tax_details[g].Tax_Per / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    if (NewOrd[idx].Tax_details[g].Tax_Name.toLowerCase() == 'tcs') { $(itr).find('.tdtcs').html(NewOrd[idx].Tax_details[g].Tax_Amt); }
                }

                $(itr).find('.tdtotal').html(NewOrd[idx].Sub_Total);
                $(itr).find('.disc_value').html(NewOrd[idx].Dis_value);
                $(itr).find('.tddis_val').html(NewOrd[idx].Dis_value);


                $(itr).find('.tdtax').html((parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2));
                NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                NewOrd[idx].Tax_value = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                $(itr).find('.tdAmt').html(NewOrd[idx].Gross_Amt);
                ReCalc();
            }

            function ReCalc() {

                tv = 0;
                $('.tdtotal').each(function () {
                    v = parseFloat($(this).text()); if (isNaN(v)) v = 0;
                    tv += v;
                })
                $('#sub_tot').val(tv.toFixed(2));

                tac = 0;
                $('.tdtax').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    tac += k;
                })
                $('#Tax_GST').val(tac.toFixed(2));


                tcss = 0;
                $('.tdtcs').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    tcss += k;
                })
                $('#tot_tcs').val(tcss.toFixed(2));

                /*
                cgst_tax = 0;
                $('.tdcgst').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    cgst_tax += k;
                })
                $('#txt_cgst').val(cgst_tax.toFixed(2));
 
                sgst_tax = 0;
                $('.tdsgst').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    sgst_tax += k;
                })
                $('#txt_sgst').val(sgst_tax.toFixed(2));
                */

                //igst_tax = 0;
                //$('.tdigst').each(function () {
                //    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                //    igst_tax += k;
                //})
                //$('#txt_igst').val(igst_tax.toFixed(2));

                dis = 0;
                $('.tddis').each(function () {
                    z = parseFloat($(this).text()); if (isNaN(z)) z = 0;
                    dis += z;
                })
                $('#Txt_Dis_tot').val(dis.toFixed(2));

                gross = 0;
                $('.tdAmt').each(function () {
                    i = parseFloat($(this).text()); if (isNaN(i)) i = 0;
                    gross += i;
                })
                $('#gross').val(gross.toFixed(2));

            }

            $(document).on('keypress', 'input[name=CQ]', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $(document).on('click', '#svorders', function () {
                $('body,html').animate({
                    scrollTop: 0
                }, 800);
                $('.spinnner_div').show();
                setTimeout(svOrder, 2000);
            });

            var approve = 0;
            svOrder = function () {

                approve += 1; var Tax_Array = [];
                if (approve == "1") {

                    var selectedvalue = $('#To_Address').val();
                    var selectdtext = $('#To_Address :selected').text();
                    if (selectedvalue == "0") {
                        button_Click = 0;
                        toastr.error('Error', 'Please Select To');
                        //$.confirm({
                        //    title: 'Error!',
                        //    content: 'Please Select To',
                        //    type: 'red',
                        //    typeAnimated: true,
                        //    autoClose: 'action|8000',
                        //    icon: 'fa fa-warning ',
                        //    buttons: {
                        //        tryAgain: {
                        //            text: 'OK',
                        //            btnClass: 'btn-red',
                        //            action: function () {

                        //            }
                        //        }
                        //    }
                        //});
                        $('.spinnner_div').hide();
                        return false;
                    }

                    itm1 = {}

                    itm1.bill_add = $('#lbl_Bill_Address').text();
                    itm1.ship_add = $('#txt_Ship_add').val();

                    if ($('#txt_Purchase_Date').val() == "") {
                        toastr.info('Alert', 'Please Select Purchase Date');
                        //$.confirm({
                        //    title: 'Error!',
                        //    content: 'Please Select Purchase Date',
                        //    type: 'red',
                        //    typeAnimated: true,
                        //    autoClose: 'action|8000',
                        //    icon: 'fa fa-warning ',
                        //    buttons: {
                        //        tryAgain: {
                        //            text: 'OK',
                        //            btnClass: 'btn-red',
                        //            action: function () {

                        //            }
                        //        }
                        //    }
                        //});
                        approve = 0;
                        $('.spinnner_div').hide();
                        return false;
                    }
                    else {
                        itm1.order_date = $('#txt_Purchase_Date').val();
                    }
                    if ($('#txt_Expected_Date').val() == "") {
                        toastr.info('Alert', 'Please Select Expected Date');
                        //$.confirm({
                        //    title: 'Error!',
                        //    content: 'Please Select Expected Date',
                        //    type: 'red',
                        //    typeAnimated: true,
                        //    autoClose: 'action|8000',
                        //    icon: 'fa fa-warning ',
                        //    buttons: {
                        //        tryAgain: {
                        //            text: 'OK',
                        //            btnClass: 'btn-red',
                        //            action: function () {

                        //            }
                        //        }
                        //    }
                        //});
                        approve = 0;
                        $('.spinnner_div').hide();
                        return false;

                    }
                    else {
                        itm1.exp_date = $('#txt_Expected_Date').val();
                    }
                    itm1.div_code = Div_Code;

                    itm1.sup_no = selectedvalue;
                    itm1.sup_name = selectdtext;
                    itm1.sf_code = stockist_Code;
                    itm1.stk_code = stockist_Code;
                    itm1.com_add = $('#lbl_To_Address').text();
                    itm1.sub_tot = $(document).find('#sub_tot').val();
                    itm1.Tax_Total = $(document).find('#Tax_GST').val();
                    itm1.Gross_tot = $(document).find('#gross').val();
                    headdatas.push(itm1);

                    var netwt = 0;
                    for (var i = 0; i < NewOrd.length; i++) {
                        netwt += parseFloat(NewOrd[i].Qty);
                    }

                    var orderval = $('#gross').val();

                    if (NewOrd.length == 0) {
                        approve = 0;
                        toastr.info('info', 'Please Select Expected Date');
                        //$.confirm({
                        //    title: 'Error!',
                        //    content: 'Atleast select a Product',
                        //    type: 'red',
                        //    typeAnimated: true,
                        //    autoClose: 'action|8000',
                        //    icon: 'fa fa-warning ',
                        //    buttons: {
                        //        tryAgain: {
                        //            text: 'OK',
                        //            btnClass: 'btn-red',
                        //            action: function () {

                        //            }
                        //        }
                        //    }
                        //});

                        headdatas = [];
                        $('.spinnner_div').hide();
                        return false;
                    }
                    for (var i = 0; i < NewOrd.length; i++) {

                        if (NewOrd[i].PName.indexOf('&') > -1) {
                            NewOrd[i].PName = NewOrd[i].PName.replace(/&/g, "&amp;");
                        }

                        if (NewOrd[i].sPCd == '') {
                            approve = 0;
                            toastr.warning('warning', 'Please Select a Product');
                            //$.confirm({
                            //    title: 'Error!',
                            //    content: 'Select a Product',
                            //    type: 'red',
                            //    typeAnimated: true,
                            //    autoClose: 'action|8000',
                            //    icon: 'fa fa-warning ',
                            //    buttons: {
                            //        tryAgain: {
                            //            text: 'OK',
                            //            btnClass: 'btn-red',
                            //            action: function () {

                            //            }
                            //        }
                            //    }
                            //});

                            headdatas = [];
                            $('.spinnner_div').hide();
                            return false;
                        }
                        if (NewOrd[i].Qty == '') {
                            approve = 0;
                            //$.confirm({
                            //    title: 'Error!',
                            //    content: 'Remove the Product or Enter the Quantity',
                            //    type: 'red',
                            //    typeAnimated: true,
                            //    autoClose: 'action|8000',
                            //    icon: 'fa fa-warning ',
                            //    buttons: {
                            //        tryAgain: {
                            //            text: 'OK',
                            //            btnClass: 'btn-red',
                            //            action: function () {

                            //            }
                            //        }
                            //    }
                            //});
                            toastr.error('Error', 'Remove the Product or Enter the Quantity');
                            headdatas = [];
                            $('.spinnner_div').hide();
                            return false;
                        }

                        for (var f = 0; f < NewOrd[i].Tax_details.length; f++) {
                            NewOrd[i].Tax_details[f]["umo_code"] = NewOrd[i].produnit;
                            Tax_Array.push(NewOrd[i].Tax_details[f]);
                        }
                    }

                    if (orderval > 0) {

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "SS_Purchase_Order.aspx/SavePurchaseOrder",
                            data: "{'HeadData':'" + JSON.stringify(headdatas) + "','DetailsData':'" + JSON.stringify(NewOrd) + "','TaxData':'" + JSON.stringify(Tax_Array) + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    toastr.options = {
                                        "positionClass": "toast-top-right",
                                        "onHidden": function () { window.location.href = "../Order/SS_Purchase_Order_List.aspx"; }
                                    }

                                    toastr.success('WOw', 'Purchase Ordered Successfully!');
                                    $('.spinnner_div').hide();
                                    //toastr.error('success', 'Purchase Ordered Successfully!');
                                    //$('.spinnner_div').hide();
                                    //window.location.href = "../Order/SS_Purchase_Order_List.aspx";
                                    //$.confirm({
                                    //    title: 'Success!',
                                    //    content: 'Purchase Ordered Successfully!',
                                    //    type: 'green',
                                    //    typeAnimated: true,
                                    //    autoClose: 'action|8000',
                                    //    icon: 'fa fa-check fa-2x',
                                    //    buttons: {
                                    //        tryAgain: {
                                    //            text: 'OK',
                                    //            btnClass: 'btn-green',
                                    //            action: function () {
                                    //                $('.spinnner_div').hide();
                                    //                window.location.href = "../Order/SS_Purchase_Order_List.aspx";
                                    //            }
                                    //        }
                                    //    }
                                    //});
                                    // Pri_order_id1 = data.d;
                                    //Pri_order_id = "'" + data.d + "'" + ',';

                                    //$.ajax({
                                    //    type: "POST",
                                    //    contentType: "application/json; charset=utf-8",
                                    //    async: false,
                                    //    data: '{"Order_id":"' + Pri_order_id + '"}',
                                    //    url: "Purchase_Order3.aspx/getorder",
                                    //    dataType: "json",
                                    //    success: function (data) {
                                    //        var PriOrdered_Data = data.d;
                                    //        call_api(PriOrdered_Data);
                                    //    },
                                    //    error: function (result) {
                                    //        button_Click = 0
                                    //        alert(JSON.stringify(result));
                                    //    }
                                    //});
                                    //window.location.href = "../Stockist/Purchase_Order_List.aspx";
                                }
                                else {
                                    $.confirm({
                                        title: 'Error!',
                                        content: 'Something Went Wrong',
                                        type: 'red',
                                        typeAnimated: true,
                                        autoClose: 'action|8000',
                                        icon: 'fa fa-warning ',
                                        buttons: {
                                            tryAgain: {
                                                text: 'OK',
                                                btnClass: 'btn-red',
                                                action: function () {
                                                    $('.spinnner_div').hide();
                                                    window.location.href = "../Order/SS_Purchase_Order_List.aspx";
                                                }
                                            }
                                        }
                                    });
                                }
                            },
                            error: function (result) {
                                $('.spinnner_div').hide();
                            }
                        });
                    } else {
                        approve = 0;
                        toastr.error('Error', 'Order Minimum Value to create a Order')

                        //$.confirm({
                        //    title: 'Error!',
                        //    content: 'Order Minimum Value to create a Order',
                        //    type: 'red',
                        //    typeAnimated: true,
                        //    autoClose: 'action|8000',
                        //    icon: 'fa fa-warning ',
                        //    buttons: {
                        //        tryAgain: {
                        //            text: 'OK',
                        //            btnClass: 'btn-red',
                        //            action: function () {

                        //            }
                        //        }
                        //    }
                        //});
                        $('.spinnner_div').hide();
                        return false;
                    }
                }
            }

            function call_api(PriOrdered_Data) {
                var url = JSON.parse(PriOrdered_Data);
                var trans_sl_no = url.mainOrder[0].Trans_sl_no;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'urlparam':'" + JSON.stringify(url) + "','trans_sl_no':'" + trans_sl_no + "'}",
                    url: "SS_Purchase_Order.aspx/getPrimaryOrderValue",
                    dataType: "json",
                    success: function (data) {
                        var PriOrdered_Data = JSON.parse(data.d);
                        sap_code = PriOrdered_Data[0].result;
                        if ($.isNumeric(sap_code) == true)
                            alert("success");
                        else
                            alert(sap_code);
                    },
                    error: function (result) {
                        //alert(PriOrdered_Data);                        
                        alert(JSON.stringify(result));
                    }
                });
            }
        });

    </script>
</asp:Content>

