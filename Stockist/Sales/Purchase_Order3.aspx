<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Purchase_Order3.aspx.cs" Inherits="Stockist_Purchase_Order3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="frm1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div style="padding-top: -2px;">
            <div class="row">
                <div class="col-md-4">
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
                    <textarea style="width: 330px;" rows="4" class="form-control" id="txt_Ship_add"></textarea>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-6">
                <label>Purchase Date</label>
                <input type="date" style="width: 270px; margin-left: 20px;" class="control-group" id="txt_Purchase_Date" readonly="readonly" />
            </div>
            <div class="col-sm-6" style="padding: 0px 0px 0px 143px;">
                <label>Expected Date</label>

                <input type="date" style="width: 270px; margin-left: 20px;" class="control-group" id="txt_Expected_Date" />
            </div>
        </div>

        <br />

        <div class="row" style="text-align: center;">
            <div id="myDIV">
            </div>
        </div>
        <br />
        <div class="row" style="background: #fff;">
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
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    Subtotal :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    Dis Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    Tax Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 120px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    TCS Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="tot_tcs" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal div_cgst" style="margin-top: 80px; display: none;">
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    CGST Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <%--    <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />--%>
                        <input data-cell="G1" id="txt_cgst" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal div_sgst" style="margin-top: 123px; display: none;">
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    SGST Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="txt_sgst" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal div_gross" style="margin-top: 160px;">
                <label class=" col-xs-2 control-label" style="font-size: 12px;padding:0px">
                    Gross Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="gross" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>
            <div style="left: 0; position: absolute; padding: 61px 0px 0px 1009px;">
                <input type="button" id="svorders" class="btn btn-lg btn-primary ews" value="Save" style="float: right; width: 160%;">
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
    </style>

    <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        var Orders = [];
        var Selected_Com_Name = ''; var Collected_data = []; var gTot = 0; var AllOrders = []; var detailsdata = []; var arr = []; Allrate = []; var calc_all_tac = 0; var calc_all_dis = 0; var calc_all_tot = 0;
        ArrPriOrd = []; var CQ = ''; var headdatas = []; var divcode; filtrkey = '0'; var ComDetails = []; var a = 0; var scheme = []; var add = []; var set = 0; var click_text = '';
        pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sale_Erp_Code,Product_Detail_Name,price"; var All_unit = []; var purchase_order = []; var sr = ''; var row_index = '';
        var z = 1; var jkl = 0; var sap_code = ''; var Pri_order_id = ''; var Pri_order_id1 = ''; var All_Tax = []; NewOrd = []; Prds = "";
        var Retailer_State = 0; var Cat_Details = [];

        var Dist_state_code = ("<%=Session["State"].ToString()%>");

        $(document).ready(function () {

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
                url: "Purchase_Order3.aspx/DisplayCompanyDetails",
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
                url: "Purchase_Order3.aspx/Dis_stk_sstk_Details",
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

            function bind_category() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order3.aspx/Get_Category",
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
                url: "Purchase_Order3.aspx/GetProducts",
                data: "{'div':'" + Div_Code + "'}",
                dataType: "json",
                success: function (data) {
                    itms = JSON.parse(data.d) || [];
                    for ($k = 0; $k < itms.length; $k++) {
                        Prds += "<option value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
                    }
                    AddRow(0);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $(document).on("change", "#To_Address", function () {

                var comp = [];
                var compVal = $('#To_Address option:selected').val();

                comp = ComDetails.filter(function (w) {
                    return (Dist_state_code == w.State_Code && w.HO_ID == compVal);
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
                    alert("Please Select")
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
                        return (a.Code ==  "<%=Session["Sf_Code"].ToString()%>")
                    });

                    $("#txt_Ship_add").val(d[0].Address);

                } else {
                    $("#txt_Ship_add").attr("readonly", false);
                }
            });

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Purchase_Order3.aspx/getscheme",
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
                    url: "Purchase_Order3.aspx/DisplayProduct",
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
                url: "Purchase_Order3.aspx/GetProducts",
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
                url: "Purchase_Order3.aspx/getratenew",
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
                url: "Purchase_Order3.aspx/Get_Product_unit",
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
                url: "Purchase_Order3.aspx/Get_Product_Tax",
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
                url: "Purchase_Order3.aspx/Get_Product_Cat_Details",
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

            function AddRow(type) {

                itm = {}
                itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0"; itm.Qty = 1; itm.Qty_c = 1; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0"; itm.Total_Tax = 0;
                itm.Tax_details = ''; itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = ''; itm.TCS = ''; itm.TDS = '';
                NewOrd.push(itm);

                tr = $("<tr class='subRow'></tr>");
                $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + ($("#OrderEntry > TBODY > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:27%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;'><option value='0'>--select--</option>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11.5px;padding: 5px 0px 0px 3px;'></div></td><td style='display:none;' ><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'></select><div class='Spinner-Input'><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>Select</ul></div></div><input type='text' name='txqty' id='txqty' class='txtQty validate' pval='0.00' value   style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>0.00</td><td class='fre' style='text-align:right; padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>0</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>0.00</td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>0.00</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td style='display:none;'><input type='hidden' class='tdtcs' id='tdtcs' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>0.00</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td>");
                $("#OrderEntry > TBODY").append(tr); OrderEntry
                resetSL();
                $('.ddlProd').chosen();

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
                    $(this).trigger("chosen:updated");

                });

                if (type == 1) {
                    event.stopPropagation();
                    $('#OrderEntry tr:last').find(".ddlProd").trigger('chosen:open');
                }

                $(".ddlProd").on("change", function () {

                    itr = $(this).closest('tr');
                    idx = $(itr).index();
                    var pro_filter = [];

                    var selected_company_code = $('#To_Address option:selected').val();

                    if (selected_company_code == '' || selected_company_code == 0) {
                        alert('Please select company');
                        $('.ddlProd').val('');
                        $('.ddlProd ').chosen("destroy");
                        $('.ddlProd').chosen();
                        return false;
                    }

                    $('.second_row_div').show();
                    sPCd = $(this).val();
                    $(this).closest("tr").attr('class', $(this).val());
                    var P_Name = itr.find('.ddlProd').find('option:selected').text();

                    pro_filter = NewOrd.filter(function (s, key) {
                        return (s.PCd == sPCd && key != idx);
                    });

                    if (pro_filter.length > 0) {

                        $(itr).find('.ddlProd').val('');
                        $(itr).find('.ddlProd ').chosen("destroy");
                        $(itr).find('.ddlProd').chosen();
                        $(itr).find('.Spinner-Value').text('Select');
                        $(itr).find('.tdRate').text("0.00");
                        $(itr).find('.txtQty').val('');
                        NewOrd[idx].Unit = 'Select';
                        NewOrd[idx].umo_unit = '';
                        $(itr).find('.second_row_div').text('');
                        CalcAmt(itr);
                        alert('Product Already Selected');
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

                    var unt_rate = Prod[0].PTS;
                    //var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                    if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }
                    $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                    NewOrd[idx].Rate = unt_rate;


                    var tax_filter = []; var tax_arr = [];

                    var ddlUnit = itr.find('.ispinner');
                    ddlUnit.empty();

                    $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + Prod[0].product_unit + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + Prod[0].product_unit + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");

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
                        return (r.Product_Detail_Code == sPCd && (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
                    });
                    $(itr).find('.second_row_div').text('');
                    $(itr).find('.second_row_div').append("<label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

                    var append = ''; var total_tax_per = 0;

                    if (tax_filter.length > 0) {

                        for (var z = 0; z < tax_filter.length; z++) {
                            append += "<label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
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
                    alert('Please Select UOM');
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
                CalcAmt(tr);

            });

            function CalcAmt(x) {

                itr = $(x).closest('tr');
                idx = $(itr).index();
                rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
                tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;

                NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);

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
                svOrder();
            });

            var approve = 0;
            svOrder = function () {

                approve += 1; var Tax_Array = [];
                if (approve == "1") {

                    var selectedvalue = $('#To_Address').val();
                    var selectdtext = $('#To_Address :selected').text();
                    if (selectedvalue == "0") { button_Click = 0; alert("Please Select To "); return false }

                    itm1 = {}

                    itm1.bill_add = $('#lbl_Bill_Address').text();
                    itm1.ship_add = $('#txt_Ship_add').val();
                    itm1.order_date = $('#txt_Purchase_Date').val();
                    if ($('#txt_Expected_Date').val() == "") {
                        itm1.exp_date = $('#txt_Purchase_Date').val();
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
                        alert('Atleast select a Product');
                        return false;
                    }
                    for (var i = 0; i < NewOrd.length; i++) {

                        if (NewOrd[i].PName.indexOf('&') > -1) {
                            NewOrd[i].PName = NewOrd[i].PName.replace(/&/g, "&amp;");
                        }

                        if (NewOrd[i].sPCd == '') {
                            approve = 0;
                            alert('Select a Product');
                            return false;
                        }
                        if (NewOrd[i].Qty == '') {
                            approve = 0;
                            alert('Remove the Product or Enter the Quantity');
                            return false;
                        }

                        for (var f = 0; f < NewOrd[i].Tax_details.length; f++) {
                            NewOrd[i].Tax_details[f]["umo_code"] = NewOrd[i].umo_unit;
                            Tax_Array.push(NewOrd[i].Tax_details[f]);
                        }                    
                    }

                    if (orderval > 0) {

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Purchase_Order3.aspx/SavePurchaseOrder",
                            data: "{'HeadData':'" + JSON.stringify(headdatas) + "','DetailsData':'" + JSON.stringify(NewOrd) + "','TaxData':'" + JSON.stringify(Tax_Array) + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    alert('Purchase Ordered Successfully');

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
                                    window.location.href = "../Stockist/Purchase_Order_List.aspx";
                                }
                                else {
                                    alert('Error');
                                }
                            },
                            error: function (result) {
                            }
                        });
                    } else {
                        approve = 0;
                        alert("Order Minimum Value to create a Order");
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
                    url: "Purchase_Order3.aspx/getPrimaryOrderValue",
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

