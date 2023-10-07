<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="order_edit.aspx.cs" Inherits="MasterFiles_order_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <link href="../css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <input id="bac" type="button" class="btn btn-primary" style="margin-left: 90%; margin-top: -1%;" value="Back" onclick="history.back(-2)">
    <div class="card">
        <div class="card-body table-responsive">
            <input type="hidden" id="ordcode" />
            <input type="hidden" id="sfcode" />
            <input type="hidden" id="ordate" />
            <input type="hidden" id="stkcd" />
                         <div class="row">
                        <div class="col-sm-12" style="padding: 15px">
                                <table class="table table-hover" id="editdets">
                                    <thead class="text-warning">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Product</th>
                                            <th>Unit</th>
                                            <th>Qty</th>
                                            <th>Rate</th>
                                            <th>Free</th>
                                            <th>Discount</th>
                                            <th>Total</th>
                                            <th>Tax</th>
                                            <th>Gross_Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                        </div>
                    </div>
             </div>
			 
			  <div style="text-align: left" colspan="9">
              &nbsp;&nbsp;&nbsp;&nbsp;<button type="button" class="btn btn-primary" onclick="AddRow()">+ Add</button>
              &nbsp;<button type="button" class="btn btn-primary" onclick="DelRow()">- Remove</button>
                            </div>
        <br />
                    </div>
    </br>
    </br>
    <div align="center"><button type="button" class="btn btn-primary" id="svorders">Save</button></div>
                    
      <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
    <link href="../css/select2.min.css" rel="stylesheet" />
    <script src="../js/select2.full.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders2 = [], NewOrd = [], Allrate = [], All_Tax=[];
        var All_unit = [], All_Product_Details = [], Product_Details = [], Prds = "", ret,ret1, orderno, sfname, orddate, stockistcd;
        $(document).ready(function () {
            $('#ordcode').val('<%=orderno%>');
            $('#sfcode').val('<%=sfcode%>');
            $('#ordate').val('<%=ordate%>');
            $('#stkcd').val('<%=stkcode%>');
            orderno = $('#ordcode').val();
            sfname = $('#sfcode').val();
            orddate = $('#ordate').val();
            stockistcd = $('#stkcd').val();
            loaddata(orderno, sfname, stockistcd);
            $(document).on('click', '#svorders', function () {
                svOrder();
            });
            
        });
        
        function loaddata(orderno, sfname, stockistcd) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "order_edit.aspx/Get_Product_unit",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sfname + "'}",
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
                url: "order_edit.aspx/GetProducts",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    All_Product_Details = JSON.parse(data.d) || [];
                    Product_Details = All_Product_Details;
                    Prds += "<option  selected='selected' value='0'>Select Product</option>";
                    for ($k = 0; $k < Product_Details.length; $k++) {
                        Prds += "<option value='" + Product_Details[$k].Product_Detail_Code + "'>" + Product_Details[$k].Product_Detail_Name + "</option>";
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
                url: "order_edit.aspx/getratenew",
                data: "{'Div_Code':'<%=Session["div_code"]%>','sf_Code':'" + sfname + "','stk_code':'" + stockistcd+"'}",
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
                url: "MyOrder_Edit1.aspx/Get_Product_Tax",
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
                url: "order_edit.aspx/get_product_fedit",
                data: "{'divcode':'<%=Session["div_code"]%>','trans_slno':'" + orderno + "','sfcode':'" + sfname + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders2 = JSON.parse(data.d) || [];
                        ReloadTable();
                    }
            });
        }
        svOrder = function () {
		var tests = 0;
            $('.ddlprod').each(function () {
                itr = $(this).closest('tr');
                if (itr.find('.ddlprod').find('option:selected').val() == "0") {
                    alert("Please Select One Product or Remove Unwanted Rows...");
                    tests = 1;
                    return false;
                }
            });

            $('.txtQty').each(function () {
                itr = $(this).closest('tr');
                if ($(itr).find('.txtQty').val() == "") {
                    alert("Please Enter Quantity for " + itr.find('.ddlprod').find('option:selected').text());
                    tests = 2;
                    return false;
                }
				if($(itr).find('.txtQty').val() == "0"){
				alert("Zero Quantity Can't be Submit...");
				tests = 2;
				return false;
				}
            });
			if($("#editdets TBODY").find('tr').length!=0)
			{
			if (tests != 1 && tests != 2) {
            var netwt = 0;
            for (var i = 0; i < NewOrd.length; i++) {
                netwt += parseFloat(NewOrd[i].Qty);
            }
            tot = 0;
            $('.tdtotal').each(function () {
                v = parseFloat($(this).text()); if (isNaN(v)) v = 0;
                tot += v;
            });
            tax_value = 0;
			for (var i = 0; i < NewOrd.length; i++) {
                tax_value += parseFloat(NewOrd[i].Tax_value);
            }
            //$('.tval_hidd').each(function () {
            //    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
            //    tax_value += k;
            //});
            dis = 0;
            $('.tddis_val').each(function () {
                z = parseFloat($(this).text()); if (isNaN(z)) z = 0;
                dis += z;
            });
            gross = 0;
            $('.tdAmt').each(function () {
                i = parseFloat($(this).text()); if (isNaN(i)) i = 0;
                gross += i;
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "order_edit.aspx/saveorders",
                data: "{'NewOrd':'" + JSON.stringify(NewOrd) + "','Ordrval':'" + gross.toFixed(2) + "','RecDate':'" + orddate + "','Ntwt':'" + netwt + "','Type':'0','ref_order':'','sub_total':'" + tot + "','dis_total':'" + dis + "','tax_total':'" + tax_value + "','Ord_id':'" + orderno + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d == "Success") {
                        alert("Order saved successfully...");
                        loaddata(orderno, sfname, stockistcd);
                    }
                }
                });
				}
				}
				else
				{
				alert("Can't Submit Empty Order...");
				}
        }
        $(document).on('change', '.ddlunit', function () {
            itr = $(this).closest('tr');
            idx = $(itr).index();
            unit = $(this).text();
            unit1 = itr.find('.ddlunit').find('option:selected').text();
            var uom = itr.find('.ddlunit').find('option:selected').val();
            var pc = itr.find('.ddlprod').find('option:selected').val();
			qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
            var Prod1 = Allrate.filter(function (a) {
                return (a.Product_Detail_Code == pc && a.Move_MailFolder_Id == uom);
            });
            if(Prod1.length>0) {
                if (Prod1[0].Move_MailFolder_Id == Prod1[0].Base_Unit_code) {
                    NewOrd[idx].Rate = Prod1[0].RP_Base_Rate;
                    $(itr).find('.tdRate').text(parseFloat(NewOrd[idx].Rate).toFixed(2));
					if (Prod1[0].scheme != '') {
                        NewOrd[idx].Free = (Math.trunc(qt / Prod1[0].scheme)) * Prod1[0].Free;
                        $(itr).find('.fre').html(Math.trunc(NewOrd[idx].Free));
                        NewOrd[idx].of_Pro_Code = Prod1[0].Offer_Product;
                        NewOrd[idx].of_Pro_Name = Prod1[0].Offer_Product_Name;
                    }
                }
                else {
                    NewOrd[idx].Rate = Prod1[0].RP_Base_Rate * Prod1[0].Sample_Erp_Code;
                    $(itr).find('.tdRate').text(parseFloat(NewOrd[idx].Rate).toFixed(2));
					if (Prod1[0].scheme != '') {
                        NewOrd[idx].Free = ((Math.trunc(qt / Prod1[0].scheme)) * Prod1[0].Free) * Prod1[0].Sample_Erp_Code;
                        $(itr).find('.fre').html(Math.trunc(NewOrd[idx].Free));
                        NewOrd[idx].of_Pro_Code = Prod1[0].Offer_Product;
                        NewOrd[idx].of_Pro_Name = Prod1[0].Offer_Product_Name;
                    }
                }
            }
            NewOrd[idx].Unit = Prod1[0].Move_MailFolder_Name;
            NewOrd[idx].umo_unit = Prod1[0].Move_MailFolder_Id;
            NewOrd[idx].Tax = Prod1[0].Tax_Name;
            NewOrd[idx].Discount = parseFloat(Prod1[0].Discount);
            calculate_amount($(itr));
        });
        $(document).on('change', '.ddlprod', function () {
            itr = $(this).closest('tr');
            idx = $(itr).index();
            sPCd = $(this).val();
			if (sPCd == '0') {
			$(itr).find('#Td1').html("<select class='ddlunit' name='unitcd'></select>");
                $(itr).find('#Td2').html("<input type='text' id='txtQty' class='txtQty validate' style='width: 60px' value='0'>");
                $(itr).find('.tdRate').text('0.00');
                $(itr).find('.fre').html('0');
                $(itr).find('.tddis_val').html('0');
                $(itr).find('.tdtotal').html('0');
                $(itr).find('.tdtax').html('0');
                $(itr).find('.tdAmt').html('0.00');

            }
			else
			{
            $(this).closest("tr").attr('class', $(this).val());
            var P_Name = itr.find('.ddlprod').find('option:selected').text();
            Prod = Allrate.filter(function (a) {
                return (a.Product_Detail_Code == sPCd);
            });
            rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0;
            qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
            NewOrd[idx].PCd = sPCd;
            NewOrd[idx].sPCd = $(this).val();
            NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
            NewOrd[idx].Unit = Prod[0].product_unit;
            NewOrd[idx].PName = P_Name;
            NewOrd[idx].Qty = qt;
            NewOrd[idx].Qty_c = qt;
            NewOrd[idx].Free = 0;
            NewOrd[idx].Total_Tax = Prod[0].Tax_Val;
            NewOrd[idx].Tax = Prod[0].Tax_Name;
            NewOrd[idx].Discount = parseFloat(Prod[0].Discount);
			if (Prod[0].scheme != '') {
                NewOrd[idx].Free = (Math.trunc(qt / Prod[0].scheme)) * Prod[0].Free;
                $(itr).find('.fre').html(Math.trunc(NewOrd[idx].Free));
                NewOrd[idx].of_Pro_Code = Prod[0].Offer_Product;
                NewOrd[idx].of_Pro_Name = Prod[0].Offer_Product_Name;
            }
            var unit = Prod[0].Product_Sale_Unit;
            var filter_unit = []; units = ""; units1 = "";
            filter_unit = All_unit.filter(function (w) {
                return (sPCd == w.Product_Detail_Code);
            });
            if (filter_unit.length > 0) {

                for (var z = 0; z < filter_unit.length; z++) {
                    if (filter_unit[z].Move_MailFolder_Id == Prod[0].Unit_code) {
                        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                    }

                    else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                    }
                }
            }
            $(document).find('.' + sPCd).each(function () {

                var rowww = $(this).closest('tr');
                var indxx = $(rowww).index();
                $(itr).find('#Td1').html("<select class='ddlunit' name='unitcd'>" + units1 + "</select>");
                $(editdets).find('tr').eq(indxx + 1).find('select[name="unitcd"]').find('option').each(function () {
                    ret1 = unit;
                    if ($(this).text() == ret1) {
                        $(this).prop('selected', true);
                        return false;
                    }
                });
                $(itr).find('#Td2').html("<input type='text' id='txtQty' class='txtQty validate' style='width: 60px' value='" + ((qt == 0) ? '' : qt) + "'>");
                var unt_rate = Prod[0].RP_Base_Rate;
                $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                NewOrd[idx].Rate = unt_rate;
                //$(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                //NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                NewOrd[idx].umo_unit = Prod[0].Unit_code;

                calculate_amount($(itr));
            });
			}
        });
        $(document).on('keyup', '.txtQty', function (e) {
            itr = $(this).closest('tr');
            idx = $(itr).index();
            eqty = $(this).val();
            var P_Code = itr.find('.ddlprod').find('option:selected').val();
			var uom = itr.find('.ddlunit').find('option:selected').val();
            //var P_Rate = itr.find('.tdRate').text();
            Prod = Allrate.filter(function (a) {
                return (a.Product_Detail_Code == P_Code);
            });
            NewOrd[idx].Tax = Prod[0].Tax_Name;
            NewOrd[idx].Discount = parseFloat(Prod[0].Discount);
            //Prod[0].RP_Base_Rate;
			var Prod1 = Prod.filter(function (a) {
                return (a.Move_MailFolder_Id == uom);
            });
            if (eqty != "") {
                if (Prod1[0].scheme != '') {
                    if (Prod1[0].Move_MailFolder_Id == Prod1[0].Base_Unit_code) {
                        NewOrd[idx].Free = (Math.trunc(eqty / Prod1[0].scheme)) * Prod1[0].Free;
                        $(itr).find('.fre').html(Math.trunc(NewOrd[idx].Free));
                        NewOrd[idx].of_Pro_Code = Prod1[0].Offer_Product;
                        NewOrd[idx].of_Pro_Name = Prod1[0].Offer_Product_Name;
                    }
                    else {
                        NewOrd[idx].Free = ((Math.trunc(eqty / Prod1[0].scheme)) * Prod1[0].Free) * Prod1[0].Sample_Erp_Code;
                        $(itr).find('.fre').html(Math.trunc(NewOrd[idx].Free));
                        NewOrd[idx].of_Pro_Code = Prod1[0].Offer_Product;
                        NewOrd[idx].of_Pro_Name = Prod1[0].Offer_Product_Name;
                    }
                    
                }
                calculate_amount($(itr));
            }
        });
        function calculate_amount(x) {

            itr = $(x).closest('tr');
            idx = $(itr).index();
            rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0;
            qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
            tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;
            NewOrd[idx].Qty = qt;
            NewOrd[idx].Qty_c = qt;
            NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);
            //if ($(itr).find('.dis_per').text() == "") { var dis_per = 0; }
            NewOrd[idx].Dis_value = (parseFloat(NewOrd[idx].Discount) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            
            $(itr).find('.tddis_val').html(NewOrd[idx].Dis_value);

            NewOrd[idx].Sub_Total = (parseFloat(NewOrd[idx].Sub_Total) - parseFloat(NewOrd[idx].Dis_value)).toFixed(2);
            $(itr).find('.tdtotal').html(NewOrd[idx].Sub_Total);

            var chk = NewOrd[idx].Tax.split(" ");
            if (chk[1] == "Rs") {
                NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax)  + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            }
            else {
                NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            }
            NewOrd[idx].Tax_value = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            $(itr).find('.tdtax').html(NewOrd[idx].Tax);
            $(itr).find('.tdAmt').html((NewOrd[idx].Gross_Amt));
        }
        function add_new() {

            itm = {}
            itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0"; itm.Qty = 0; itm.Qty_c = 0; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0";
            itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = '';
            itm.Tax_details = '', itm.dbrate = '', itm.trans_ordno='';
            NewOrd.push(itm);
        }
		
		function AddRow() {
            var test = 0;
            $('.ddlprod').each(function () {
                itr = $(this).closest('tr');
                if (itr.find('.ddlprod').find('option:selected').val() == "0") {
                    alert("Please Select One Product...");
                    test = 1;
                    return false;
                }
            });

            $('.txtQty').each(function () {
                itr = $(this).closest('tr');
                if ($(itr).find('.txtQty').val() == "") {
                    alert("Please Enter Quantity for " + itr.find('.ddlprod').find('option:selected').text());
                    test = 2;
                    return false;
                }
            });

            if (test != 1 && test != 2) {


            add_new();
                var prod=[], prd='';
				prod = Product_Details;
            $('.ddlprod').each(function () {
                itr = $(this).closest('tr');
                var pval = itr.find('.ddlprod').find('option:selected').val();
                prod = prod.filter(function (a) {
                    return (a.Product_Detail_Code != pval);
                });
            });

            prd += "<option  selected='selected' value='0'>Select Product</option>";
			if (prod.length <= 0) {
                    prod = Product_Details;
                }
            for ($k = 0; $k < prod.length; $k++) {
                prd += "<option value='" + prod[$k].Product_Detail_Code + "'>" + prod[$k].Product_Detail_Name + "</option>";
            }

            slno = $('#editdets tr').length;
            tr = $("<tr class='subRow'></tr>");
            $(tr).html("<td><input type='checkbox' class='rowCheckbox' id='SelectedCheckbox'/></td><td><select class='ddlprod' name='prodcd'>" + prd + "</select></td><td id='Td1'><select class='ddlunit' name='unitcd'></select></td><td id='Td2'><input type='text' id='txtQty' class='txtQty validate' style='width: 60px' value='0'></td>" +
                "<td class='tdRate'>0</td > <td class='fre'>0</td> <td class='tddis_val'>0</td> <td class='tdtotal'>0"+
                "</td><td class='tdtax'><input type='hidden' class='tval_hidd' value='0'>0</td><td class='tdAmt'>0.00</td>");
                $("#editdets > TBODY").append(tr);

            }
        }

        function DelRow() {
		 var ckh = 0;
            $('.rowCheckbox:checkbox:checked').each(function () {
                
                NewOrd.splice($(this).closest('tr').index(), 1);
				$(this).closest('tr').remove();
                ckh = 1;
            });
            if (ckh == 0) {
                alert("Please Tick checkbox which Product to remove...");
            }
            //$('#editdets tr:last').remove();
            //NewOrd.splice($('#editdets tr:last').length, 1);
        }
		
        function ReloadTable() {
            
            $('#editdets TBODY').html("");
            var slno = 0;
            var row = $(this).closest('tr');
            var idx = $(row).index();
            var idx1 = $(row).index();
            for ($i = 0; $i < AllOrders2.length; $i++) {
                add_new();
                var filter_unit = []; units = ""; units1 = "";
                var prodnam = AllOrders2[$i].Product_Detail_Name;
                var unit = AllOrders2[$i].Unit_Name;
                filter_unit = All_unit.filter(function (w) {
                    return (AllOrders2[$i].Product_Code == w.Product_Detail_Code);
                });

                if (filter_unit.length > 0) {

                    for (var z = 0; z < filter_unit.length; z++) {
                        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                    }
                }
                if (AllOrders2.length > 0) {
                    slno += 1;
                    tr = $("<tr class='" + AllOrders2[$i].Product_Detail_Code + "'></tr>");
                    $(tr).html("<td><input type='checkbox' class='rowCheckbox' id='SelectedCheckbox'/></td><td><select class='ddlprod' name='prodcd'>" + Prds + "</select></td><td id='Td1'><select class='ddlunit' name='unitcd'>" + units1 + "</select></td><td id='Td2'><input type='text' id='txtQty' class='txtQty validate' style='width: 60px' value='" + AllOrders2[$i].Quantity + "'></td>" + /*<td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'><option value='1'>Select</option></select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + AllOrders2[$i].umo + "></div><div class='Spinner-Value'>" + AllOrders2[$i].Unit_Name + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div></td>*//*<td>" + AllOrders2[$i].Quantity +*/
                        "<td class='tdRate'>" + AllOrders2[$i].Rate + "</td > <td class='fre'>" + AllOrders2[$i].free + "</td> <td class='tddis_val'>" + AllOrders2[$i].discount_price + "</td> <td class='tdtotal'>" + AllOrders2[$i].total +
                        "</td><td class='tdtax'><input type='hidden' class='tval_hidd' value='" + AllOrders2[$i].Txval + "'>" + AllOrders2[$i].Tax_val + "</td><td class='tdAmt'>" + (AllOrders2[$i].value).toFixed(2)+ "</td>");
                    $("#editdets TBODY").append(tr);
                }
                idx++;
                $(editdets).find('tr').eq(idx + 1).find('select[name="prodcd"]').find('option').each(function () {
                    ret = prodnam;
                    if ($(this).text() == ret) {
                        $(this).prop('selected', true);
                        return false;
                    }
                });
                idx1++;
                $(editdets).find('tr').eq(idx1 + 1).find('select[name="unitcd"]').find('option').each(function () {
                    ret1 = unit;
                    if ($(this).text() == ret1) {
                        $(this).prop('selected', true);
                        return false;
                    }
                });
                NewOrd[$i].PCd = AllOrders2[$i].Product_Code;
                NewOrd[$i].sPCd = AllOrders2[$i].Product_Code;
                NewOrd[$i].s_pcode = AllOrders2[$i].Sale_Erp_Code;
                NewOrd[$i].Rate = AllOrders2[$i].Rate;
                NewOrd[$i].Unit = AllOrders2[$i].Unit_Name;
                NewOrd[$i].PName = AllOrders2[$i].Product_Detail_Name;
                NewOrd[$i].Qty = AllOrders2[$i].Quantity;
                NewOrd[$i].Free = 0;
                NewOrd[$i].con_fac = AllOrders2[$i].Sample_Erp_Code;
                NewOrd[$i].Qty_c = AllOrders2[$i].qty;
                NewOrd[$i].umo_unit = AllOrders2[$i].Uom_Id;
                NewOrd[$i].Free = AllOrders2[$i].free;
                NewOrd[$i].Discount = AllOrders2[$i].discount;
                NewOrd[$i].Dis_value = AllOrders2[$i].discount_price;
                NewOrd[$i].Total_Tax = AllOrders2[$i].Txval;
				NewOrd[$i].Tax_value = AllOrders2[$i].Tax_value;
                NewOrd[$i].dbrate = AllOrders2[$i].DP_Base_Rate;
                NewOrd[$i].trans_ordno = AllOrders2[$i].Trans_Order_No;
                NewOrd[idx].Sub_Total = (AllOrders2[$i].Quantity * AllOrders2[$i].Rate).toFixed(2);
                NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            }
        }
    </script> 
</asp:Content>

