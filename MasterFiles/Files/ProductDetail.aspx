<%@ Page Title="Product Detail Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="ProductDetail.aspx.cs" Inherits="MasterFiles_ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Product Detail</title>
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }

            .loading {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }

            #divdr {
                font-family: Verdana;
                font-size: small;
                font-weight: bold;
                padding: 2px;
                border: solid 1px;
                width: 130px;
            }

            #detail {
                font-family: Verdana;
                font-size: Smaller;
                border: solid 1px;
                width: 130px;
                padding: 2px;
            }

            #divcat {
                font-family: Verdana;
                font-size: small;
                font-weight: bold;
                padding: 2px;
                border: solid 1px;
                width: 90px;
            }

            #detailcat {
                font-family: Verdana;
                font-size: Smaller;
                border: solid 1px;
                width: 90px;
                padding: 2px;
            }

            #divTerr {
                font-family: Verdana;
                font-size: small;
                font-weight: bold;
                padding: 2px;
                border: solid 1px;
                width: 120px;
            }

            #detailTerr {
                font-family: Verdana;
                font-size: Smaller;
                border: solid 1px;
                width: 120px;
                padding: 2px;
            }

            #divcls {
                font-family: Verdana;
                font-size: small;
                font-weight: bold;
                padding: 2px;
                border: solid 1px;
                width: 120px;
            }

            #detailCls {
                font-family: Verdana;
                font-size: Smaller;
                border: solid 1px;
                width: 120px;
                padding: 2px;
            }

            .btn-choose {
                background-color: #19a4c6;
                color: #fff;
                border: .1ex solid #4caf50;
                flex: 0 1 auto;
                flex: 0 1 auto;
                display: inline-block;
                align-items: center;
                justify-content: center;
                padding: 1ex 2ex;
                border-radius: .5rem;
                text-decoration: none;
                cursor: pointer;
                text-align: center;
                white-space: nowrap;
            }


            .ms-options-wrap.ms-has-selections > button {
                color: #333;
                width: 341px;
                overflow: hidden;
            }

            .ms-options {
                min-height: 100px;
                max-width: 321px;
                max-height: 332.938px;
            }
        </style>

        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
        <%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
        <script type="text/javascript">
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
            $('form').live("submit", function () {
                ShowProgress();
            });
        </script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script src="multiselect.min.js"></script>
        <link href="../css/jquery.multiselect.css" rel="stylesheet" />
        <%--    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>--%>
        <script type="text/javascript">
            var x = 0;
            var pcode = '<%=ProdCode%>';
            var Prds = '';
            var txt;
            var txtval;
            var NewOrd = [];
            var arr = [];
            var state = [];
            var Tax = [];
            var taDets = [];
            var alltax = [];
            var newtax = [];
            function AddRow() {
                x = 1;
                var trx = $("#OrderEntry > TBODY>tr");
                tr = $("<tr class='subRow'></tr>");
                $(tr).html("<td><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + x + "</span></label></td><td><select class='ddlProd' style='margin-top:-3px;height:25px;'>" + Prds + "</select></td><td style='width:70px;'><input type='text' class='txtQty' style='margin-top:-3px;height:25px;' value='' /></td><td><input type='radio' name='dUOM' class='deuom'></td>");
                $("#OrderEntry > TBODY").append(tr); resetSL();
            }
            function DelRow() {
                $(".slitm:checked").each(function () {
                    itr = $(this).closest('tr');
                    idx = $(itr).index();
                    $(this).closest('tr').remove();
                }); resetSL();
            }
            clearOrder = function () {
                $("#OrderEntry > TBODY").html("");
                arr = [];
            }
            resetSL = function () {
                $(".rwsl").each(function () {
                    $(this).text($(this).closest('tr').index() + 1);
                });
            }

            function btnsave() {


                $(document).find('.subRow').each(function () {
                    var dfuom = (($(this).find('.deuom').prop("checked") == true) ? "0" : "1");
                    arr.push({
                        baseuom: $(this).find('.ddlProd').val(),
                        qty: $(this).find('.txtQty').val(),
                        defaultuom: dfuom
                    });
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/saveUOMPopup",
                    data: "{'Data':'" + JSON.stringify(arr) + "','ddlbaseunit':'" + $('#<%=ddlbaseunit.ClientID%> :selected').val() + "','ProdDetailCode':'" + pcode + "'}",
                    dataType: "json",
                    success: function (data) {
                        var res = data.d;
                        clearOrder();
                        alert(res);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }
            function loadproduct() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/GetUOMpro",
                    data: "{'procode':'" + pcode + "','UOT':'" + $('#<%=ddlbaseunit.ClientID%> :selected').val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        var UOMpro = JSON.parse(data.d) || [];
                        //  var filt = [];
                        // filt = UOMddl.filter(function (a) {
                        //     return a.UOF != txtval && a.Move_MailFolder_Name != '';
                        //});
                        $("#OrderEntry > TBODY").html("");
                        if (UOMpro.length > 0) {
                            for ($k = 0; $k < UOMpro.length; $k++) {
                                x = $k + 1;
                                tr = $("<tr class='subRow'></tr>");
                                $(tr).html("<td><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + x + "</span></label></td><td><select class='ddlProd' id='ddl" + x + "' style='margin-top:-3px;height:25px;'>" + Prds + "</select></td><td style='width:70px;'><input type='text' id='txtqny" + x + "' class='txtQty' style='margin-top:-3px;height:25px;' /></td><td><input type='radio' name='dUOM' class='deuom chk" + x + "'></td>");
                                $("#OrderEntry > TBODY").append(tr);
                                $('#ddl' + x).val(UOMpro[$k].UOF);
                                if (UOMpro[$k].Default_UOM == 1) {
                                    $('.chk' + x).prop("checked", true);
                                }
                                $("#txtqny" + x).val(UOMpro[$k].Qnty)
                            }
                        }
                    },
                    error: function (result) {
                    }


                });
            }
            function loaduom() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/GetUOMddl",
                    dataType: "json",
                    success: function (data) {
                        var UOMddl = JSON.parse(data.d) || [];
                        var filt = [];
                        filt = UOMddl.filter(function (a) {
                            return a.Move_MailFolder_Id != txtval && a.Move_MailFolder_Name != '';
                        });
                        for ($k = 0; $k < filt.length; $k++) {
                            Prds += "<option value='" + filt[$k].Move_MailFolder_Id + "'>" + filt[$k].Move_MailFolder_Name + "</option>";
                        }
                    },
                    error: function (result) {
                    }


                });
            }



            $(document).ready(function () {





                $('input[id*=txtEffect_To]').datepicker({
                    dateFormat: 'dd/mm/yy'
                });
                $('input[id*=txtEffect_from]').datepicker({
                    dateFormat: 'dd/mm/yy'
                });
                txt = $('#<%=ddlbaseunit.ClientID%> :selected').text();
                txtval = $('#<%=ddlbaseunit.ClientID%> :selected').val();
                loaduom();
                $(document).on('click', '#btnbaseUOM', function () {
                    $('#exampleModal').modal('toggle');
                    x = 0;
                    $('#txtth').html(txt);

                    loadproduct();
                    //clearOrder();
                });


                $('#<%=chkSubdiv.ClientID%> input:checkbox').click(function () {
                    var $inputs = $('#<%=chkSubdiv.ClientID%> input:checkbox')
                    if ($(this).is(':checked')) {
                        $inputs.not(this).prop('disabled', false); // <-- disable all but checked one
                    } else {
                        $inputs.prop('disabled', false); // <--
                    }
                });
                //   $('input:text:first').focus();
                $('input:text').bind("keydown", function (e) {
                    var n = $("input:text").length;
                    if (e.which == 13) { //Enter key
                        e.preventDefault(); //to skip default behavior of the enter key
                        var curIndex = $('input:text').index(this);
                        if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                            $('input:text')[curIndex].focus();
                        }
                        else {
                            var nextIndex = $('input:text').index(this) + 1;

                            if (nextIndex < n) {
                                e.preventDefault();
                                $('input:text')[nextIndex].focus();
                            }
                            else {
                                $('input:text')[nextIndex - 1].blur();
                                $('#btnSubmit').focus();
                            }
                        }
                    }
                });
                $("input:text").on("keypress", function (e) {
                    if (e.which === 32 && !this.value.length)
                        e.preventDefault();
                });
                $('#<%=btnSubmit.ClientID%>').click(function () {
                    if ($('#<%=txtProdDetailCode.ClientID%>').val() == "") { alert("Enter Product Code."); $('#<%=txtProdDetailCode.ClientID%>').focus(); return false; }
                    if ($('#<%=txtProdDetailName.ClientID%>').val() == "") { alert("Enter Product Name."); $('#<%=txtProdDetailName.ClientID%>').focus(); return false; }
                    if ($('#<%=txtProdShortName.ClientID%>').val() == "") { alert("Enter Product Short Name."); $('#<%=txtProdShortName.ClientID%>').focus(); return false; }
                    var cat = $('#<%=ddlbaseunit.ClientID%>:selected').text();
                    if (cat == "---Select---") { alert("Select Base Unit."); $('#<%=ddlbaseunit.ClientID%>').focus(); return false; }
                    var cat = $('#<%=ddlunit.ClientID%>:selected').text();
                    if (cat == "---Select---") { alert("Select UOM."); $('#<%=ddlunit.ClientID%>').focus(); return false; }
                    var cat = $('#<%=ddlCat.ClientID%>  :selected').text();
                    if ($('#<%=Txt_UOM.ClientID%> ').val() == "") { alert("Enter UOM Value."); $('#<%=Txt_UOM.ClientID%>').focus(); return false; }
                    if (cat == "---Select---") { alert("Select Category."); $('#<%=ddlCat.ClientID%>').focus(); return false; }
                    var grp = $('#<%=ddlBrand.ClientID%>  :selected').text();
                    if (grp == "---Select---") { alert("Select Brand."); $('#<%=ddlBrand.ClientID%>').focus(); return false; }
                    if ($('#<%=txtProdDesc.ClientID%> ').val() == "") { alert("Enter Description."); $('#<%=txtProdDesc.ClientID%>').focus(); return false; }
                    if ($('#<%=chkSubdiv.ClientID%> input:checked').length > 0) { } else { alert('Select Division'); return false; }
                    if ($('#<%=chkboxLocation.ClientID%> input:checked').length > 0) { } else { alert('Select Sate'); return false; }
                    <%--if ($('#<%=Txtunitwg.ClientID%>').val() == "") {alert("Enter Product Unit Weight."); $('#<%=Txtunitwg.ClientID%>').focus(); return false;--%> 

                    //if ($('#cntry1 option:selected').val() != '' && $('#state1 option:selected').val() != null) {
                    //    $('.tbltax>tbody>tr').each(function () {

                    //        tname = $('select[name="cntry' + icnt + '"]').val();
                    //        // sname = $('select[name="state' + icnt + '"]').val();
                    //        if (tname == 0) {
                    //            //alert('select The tax values');
                    //            return false;
                    //        }
                    //        sname = $('select[name="state' + icnt + '"]').val();
                    //        if (sname == null) {
                    //            //alert('select The states');
                    //            return false;
                    //        }

                    //        alltax.push({
                    //            tname: tname,
                    //            sname: sname

                    //        })

                    //        icnt--;
                    //    });
                    //    if (tname == 0 || sname == null) {
                    //        alert('select The tax and states');
                    //        return false;
                    //    }
                    //}



                    $utr = $(".tbltax > tbody").find("tr");
                    alltax = [];
                    for (i = 1; i <= $utr.length; i++) {
                        tax = {};
                        tr = $utr[i - 1];

                        tname = $(tr).find(".taxval").val();
                        if (tname != 0) {
                            tax["tname"] = tname;
                        }

                        sname = $(tr).find(".state").val();
                        if (sname != null) {
                            var ttacitems = sname.toString();
                            tax["sname"] = ttacitems;
                        }
                        if (sname != null && tname != 0) {
                            alltax.push(tax);
                        }

                    }
                    savetaxdet();

                });
                $('#<%=Button1.ClientID%>').click(function () {
                    if ($('#<%=txtProdDetailCode.ClientID%>').val() == "") { alert("Enter Product Code."); $('#<%=txtProdDetailCode.ClientID%>').focus(); return false; }

                });
                gettaxdetails();


            });

            function loadtaxes() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/gettax",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        Tax = JSON.parse(data.d);
                        if (Tax.length > 0) {
                            var Tax_Name = '';
                            Tax_N = "";
                            Tax_Name = $("#cntry1").empty();
                            for (var i = 0; i < Tax.length; i++) {
                                Tax_N += '<option value="' + Tax[i].Tax_Id + '">' + Tax[i].Tax_Name + '</option>';
                                Tax_Name.append($('<option value="' + Tax[i].Tax_Id + '">' + Tax[i].Tax_Name + '</option>'))
                            }

                        }
                    },
                });


            }
            function loadstates() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/getstates",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        state = JSON.parse(data.d);
                        if (state.length > 0) {
                            var StateName = '';
                            Sta_na = '';
                            StateName = $("#state1").empty();

                            for (var i = 0; i < state.length; i++) {
                                Sta_na += '<option value="' + state[i].State_Code + '">' + state[i].StateName + '</option>';
                                StateName.append($('<option value="' + state[i].State_Code + '">' + state[i].StateName + '</option>'))
                            }
                            $("#state1").multiselect({
                                columns: 3,
                                placeholder: 'Select States',
                                search: true,
                                searchOptions: {
                                    'default': 'Search State'
                                },
                                selectAll: true
                            }).multiselect('reload');
                            $('.ms-options ul').css('column-count', '2');
                        }
                    },
                });


            }
            var tname = '';
            var sname = '';
            function savetaxdet() {


                for (var i = 0; i < alltax.length; i++) {
                    var statess = alltax[i].sname.toString();
                    newtax.push({
                        tname: alltax[i].tname,
                        sname: statess
                    })
                }

                saveupdt();
                var Product_code = $('#<%=txtProdDetailCode.ClientID%>').val();
                if (newtax.length > 0) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ProductDetail.aspx/saveprotax",
                        data: "{'Product_code':'" + Product_code + "','newtax':'" + JSON.stringify(newtax) + "'}",
                        dataType: "json",
                        success: function (data) {
                            var successtext = data.d;
                            if (successtext == "Success") {

                            }
                        },
                        error: function (result) {
                            alert("Error Occured, Try Again");
                        }
                    });
                }




            }
            var icnt = 1;
            function addtax() {
                $('#state').multiselect({
                    includeSelectAllOption: true
                });

                var tname = $('select[name="cntry' + icnt + '"]').val();
                //if (tname == 0) {
                //    alert('select The tax values');
                //    return false;
                //}
                var sname = $('select[name="state' + icnt + '"]').val();
                //if (sname == null) {
                //    alert('select The States');
                //    return false;
                //}
                icnt = icnt + 1;
                var txtboxx = '<tr class="taxdet"><td align="left" Style="width:129px"><select name="cntry' + icnt + '" class="col-2 taxval" id="cntry' + icnt + '"  style="width: 100px;margin-left: 18px;">' + Tax_N + '</select></td><td align="left"style="width: 312px;"><select id="state' + icnt + '" name="state' + icnt + '" class="col-6 state" multiple>' + Sta_na + '</select></td><td align="left" class=""><input type="image" style="margin-left:3px;margin-top:0px;width:73%;" id="remobtn"  src="https://img.icons8.com/fluency/344/delete-sign.png" class="btnDelet"/></td></tr>';

                $("#tblee1").on('click', '.btnDelet', function () {
                    $(this).closest('tr').remove();
                    icnt--;
                });



                $('#tblee1').append(txtboxx);
                $(".state").multiselect({
                    columns: 3,
                    placeholder: 'Select States',
                    search: true,
                    searchOptions: {
                        'default': 'Search State'
                    },
                    selectAll: true
                }).multiselect('reload');
                $('.ms-options ul').css('column-count', '2');

            }


            function saveupdt() {

                var Product_code = $('#<%=txtProdDetailCode.ClientID%>').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/deletedata",
                    data: "{'Product_code':'" + Product_code + "'}",
                    success: function (data) {

                        var obj = data.d;
                        if (obj == 'true') {


                        }
                    },

                    error: function (result) {
                        alert("Error Occured, Try Again");
                    }
                });
            }
            function gettaxdetails() {
                loadtaxes();
                loadstates();
                var Product_code =  '<%=ProdCode%>';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductDetail.aspx/gettaxesdets",
                    data: "{'Product_Code':'" + Product_code + "','div_code':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var taDets = JSON.parse(data.d) || [];

                        if (taDets.length > 0) {
                            Chkstr = ''; cnt = 0;
                            for (var i = 0; i < taDets.length; i++) {
                                taDets.sort((a, b) => b.Tax_Id - a.Tax_Id);
                                if (taDets[i].Tax_Id != Chkstr) {
                                    cnt = cnt + 1;
                                    var taxlist = taDets.filter(function (taxDe) {
                                        return taxDe.Tax_Id == taDets[i].Tax_Id;
                                    });
                                    Chkstr = taDets[i].Tax_Id;
                                    $("#cntry" + cnt + ' option[value="' + taxlist[0].Tax_Id + '"]').attr('selected', true);
                                    for (var j = 0; j < taxlist.length; j++) {
                                        $('#state' + cnt + ' option[value="' + taxlist[j].State_code + '"]').attr('selected', true);
                                    }
                                    addtax();
                                }

                            }
                            deleterow(tblee1);
                        }


                    },
                });

            }
            function deleterow(tblee1) {
                icnt--;
                var table = document.getElementById('tblee1');
                var rowCount = table.rows.length;
                table.deleteRow(rowCount - 1);
            }

        </script>
        <script type="text/javascript">
            function ValidateCheckBoxList() {

                var listItems = document.getElementById("chkboxLocation").getElementsByTagName("input");
                var itemcount = listItems.length;
                var iCount = 0;
                var isItemSelected = false;
                for (iCount = 0; iCount < itemcount; iCount++) {
                    if (listItems[iCount].checked) {
                        isItemSelected = true;
                        break;
                    }
                }



                if (!isItemSelected) {
                    alert("Please select State");
                }
                else {
                    return true;
                }
                return false;
            }
        </script>
        <script type="text/javascript">

            function checkAll(obj1) {
                var checkboxCollection = document.getElementById('<%=chkboxLocation.ClientID %>').getElementsByTagName('input');
                for (var i = 0; i < checkboxCollection.length; i++) {
                    if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {

                        checkboxCollection[i].checked = obj1.checked;

                    }
                }
            }

        </script>
        <script type="text/javascript">

</script>
        <%-- <script type="text/javascript">

        function ChkBoxAll(obj2) {
            var checkboxCollection1 = document.getElementById('<%=ChkBox_Multiunit.ClientID %>').getElementsByTagName('input');
            for (var i = 0; i < checkboxCollection1.length; i++) {
                if (checkboxCollection1[i].type.toString().toLowerCase() == "checkbox") {

                    checkboxCollection1[i].checked = obj1.checked;

                }
            }
        }  
      
    </script>--%>
        <script type="text/javascript">

            $(function () {

                $("[id*=ChkAll]").bind("click", function () {

                    if ($(this).is(":checked")) {

                        $("[id*=chkboxLocation] input").attr("checked", "checked");

                    } else {

                        $("[id*=chkboxLocation] input").removeAttr("checked");

                    }

                });

                $("[id*=chkboxLocation] input").bind("click", function () {

                    if ($("[id*=chkboxLocation] input:checked").length == $("[id*=chkboxLocation] input").length) {

                        $("[id*=ChkAll]").attr("checked", "checked");

                    } else {

                        $("[id*=ChkAll]").removeAttr("checked");

                    }

                });

            });

        </script>
        <%-- <script type="text/javascript">

        $(function () {

            $("[id*=ChkBoxAll]").bind("click", function () {

                if ($(this).is(":checked")) {

                    $("[id*=ChkBox_Multiunit] input").attr("checked", "checked");


                } else {

                    $("[id*=ChkBox_Multiunit] input").removeAttr("checked");

                }

            });

            $("[id*=ChkBox_Multiunit] input").bind("click", function () {

                if ($("[id*=ChkBox_Multiunit] input:checked").length == $("[id*=ChkBox_Multiunit] input").length) {

                    $("[id*=ChkBoxAll]").attr("checked", "checked");


                } else {

                    $("[id*=ChkBoxAll]").removeAttr("checked");

                }

            });

        });

    </script>--%>
        <%--  <script type="text/javascript">
    function check(){
        var chkNil = document.getElementById("chkNil");
        var chkSubdiv = document.getElementById("chkSubdiv");
    if (chkNil.checked == true)
    {
        chkSubdiv.checked == false;
    } 
    else if(chkSubdiv.checked == true)
    {
        chkNil.checked == false;
    }
}
</script>--%>
        <%--   <script type="text/javascript">

        function checkNIL(obj1) {
            var checkSubCollection = document.getElementById('<%=chkSubdiv.ClientID %>').getElementsByTagName('input');
            
            var checkNilCollection = document.getElementById('<%=chkNil.ClientID %>').getElementsByTagName('input2');

            for (var i = 0; i < checkSubCollection.length; i++) {
                if (obj1.checked) {
                    checkSubCollection[i].checked = false;
                }
                else {
                    checkSubCollection[i].checked = true;
                }             



            }


        }  
      
    </script>--%>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto; background-color: rgba(0, 0, 0, 0.06);" tabindex="0" aria-hidden="true">
                    <div class="modal-dialog" role="document" style="width: 618px !important">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Base UOM Entry</h5>
                                <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">

                                <div class="row">
                                    <div class="col-sm-12" style="padding: 15px">
                                        <table class="table table-hover orden" id="OrderEntry" c>
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="text-align: left">#</th>
                                                    <th style="text-align: left">UOM</th>
                                                    <th style="text-align: left" id="txtth"></th>
                                                    <th style="text-align: left">Default</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                            <tfoot>
                                                <th style="text-align: left" colspan="4">
                                                    <button type="button" class="btn btn-success" onclick="AddRow()" style="font-size: 12px">+ Add New </button>
                                                    <button type="button" class="btn btn-danger" onclick="DelRow()" style="font-size: 12px">- Remove </button>
                                                </th>
                                            </tfoot>
                                        </table>
                                    </div>

                                </div>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                <button type="button" class="btn btn-primary" onclick="btnsave()" id="svorders">Save</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <ucl:Menu ID="menu1" runat="server" />--%>

                <br />
                <center>
                    <div class="col-sm-8 card" style="width: 740px; left: 0px; top: -30px">

                        <br />

                        <table runat="server" cellpadding="5" cellspacing="5" align="center">
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="lblDetailCode" runat="server" Width="150px" SkinID="lblMand"><span style="color:Red">*</span>Product Code</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtProdDetailCode" runat="server" CssClass="TEXTAREA" MaxLength="50"
                                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label2" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Product Name</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtProdDetailName" runat="server" CssClass="TEXTAREA" TabIndex="2"
                                        AutoCompleteType="Search" MaxLength="150" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'"
                                        Width="288px" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label13" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Product Short Name</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtProdShortName" runat="server" CssClass="TEXTAREA" TabIndex="2"
                                        AutoCompleteType="Search" MaxLength="150" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'"
                                        Width="288px" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label6" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Base UOM</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="1">
                                    <asp:DropDownList ID="ddlbaseunit" runat="server" EnableViewState="true" CssClass="DropDownList"
                                        AutoPostBack="true" SkinID="ddlRequired" TabIndex="3" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                        OnSelectedIndexChanged="ddlbaseunit_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                    <button type="button" id="btnbaseUOM">>></button>
                                </td>
                                <td rowspan="4">
                                    <asp:Image ID="prodImg" runat="server" Style="width: 111px; height: 123px;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label4" runat="server" SkinID="lblMand"><span style="color:Red">*</span>UOM</asp:Label>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:DropDownList ID="ddlunit" runat="server" EnableViewState="true" CssClass="DropDownList"
                                        AutoPostBack="true" SkinID="ddlRequired" TabIndex="4" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                        OnSelectedIndexChanged="ddlunit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label16" runat="server" SkinID="lblMand"><span style="margin-left:-275px">Product Image</span></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label8" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Conversion factor</asp:Label>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:TextBox ID="Txt_UOM" runat="server" CssClass="TEXTAREA" TabIndex="5" AutoCompleteType="Search"
                                        MaxLength="50" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                        onkeypress="CheckNumeric(event);" Width="100px" SkinID="MandTxtBox" onpaste="return false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label3" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Category</asp:Label>
                                </td>
                                <td align="left" class="stylespc">
                                    <asp:DropDownList ID="ddlCat" runat="server" EnableViewState="true" CssClass="DropDownList"
                                        AutoPostBack="false" SkinID="ddlRequired" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label1" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Product Group</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:DropDownList ID="ddlGroup" runat="server" EnableViewState="true" CssClass="DropDownList"
                                        AutoPostBack="false" TabIndex="5" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        SkinID="ddlRequired" onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                        <asp:ListItem Value="0">--Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td style="font-size: Medium; height: 38px; width: 301px; padding-top: 12px;">
                                    <div style="margin-left: -180px">
                                        <asp:FileUpload ID="fileup2" runat="server" Font-Size="8pt" COLOR="black" FONT-FAMILY="Verdana" Height="38px" Width="301px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="lblbrd" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Brand</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:DropDownList ID="ddlBrand" runat="server" EnableViewState="true" CssClass="DropDownList"
                                        AutoPostBack="false" TabIndex="5" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        SkinID="ddlRequired" onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;</td>
                                <td style="padding-top: 0px; padding-bottom: 08px;">
                                    <div style="margin-left: -150px">
                                        <asp:Button ID="Button2" CssClass="btn-choose" runat="server" Text="Upload" OnClick="Button2_Click" />
                                        <asp:Label ID="pimage" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Width="60px" SkinID="lblMand"> HSN Code</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Hsn" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label7" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Type</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:RadioButtonList ID="RblType" CssClass="Radio" runat="server" RepeatColumns="3"
                                        Font-Names="Verdana" Font-Size="X-Small">
                                        <asp:ListItem Value="R" Selected="True">Regular Product &nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="P">Promoted Product &nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="O">Offer Product</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <%-- <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label8" runat="server" Text="Mode of Product" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Text="Sale" Value="Sale"></asp:ListItem>
                            <asp:ListItem Text="Sample" Value="Sample"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label5" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Description</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtProdDesc" runat="server" CssClass="TEXTAREA" TabIndex="6" MaxLength="120"
                                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'" Width="400px" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label11" runat="server" SkinID="lblMand"><span style="color:Red"></span>Pack Size</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtPacksize" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric(event);"
                                        TabIndex="7" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label9" runat="server" Width="150px" SkinID="lblMand"><span style="color:Red"></span>Order Conversion Qty</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtGrosswt" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric_dot(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label14" runat="server" Width="150px" SkinID="lblMand"><span style="color:Red"></span>Base Value(tonnage)</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtNetwt" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric_dot(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="Label10" runat="server" Width="150px" SkinID="lblMand">ERP Code</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txtsale" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="lbltarget" runat="server" Width="150px" SkinID="lblMand">Target</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="txttarget" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="lblunitwg" runat="server" Width="150px" SkinID="lblMand">Unit Weight</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="Txtunitwg" runat="server" CssClass="TEXTAREA" MaxLength="15" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric_dot(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="stylespc">
                                    <asp:Label ID="lblprovalid" runat="server" Width="150px" SkinID="lblMand">Product Validity</asp:Label>
                                </td>
                                <td align="left" class="stylespc" colspan="2">
                                    <asp:TextBox ID="Txtprovalid" runat="server" CssClass="TEXTAREA" MaxLength="5" onfocus="this.style.backgroundColor='LavenderBlush'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="CheckNumeric(event);"
                                        TabIndex="1" SkinID="MandTxtBox"></asp:TextBox>
                                </td>
                            </tr>

                        </table>

                        <br />
                    </div>


                    <div style="height: auto; width: 44%; float: right; margin-top: 0px" class="col-sm-8 card">
                        <table id="tbl1" style="margin-top: 15px" border="1">
                            <tr>
                                <td>
                                    <asp:Label ID="lblExcel" runat="server" class="pad HDBg" Style="position: relative; color: #41505b; font-family: Verdana; font-weight: bold; text-transform: capitalize; margin-left: 95px; font-size: 15px; padding: 5px;">   Detailing Files(Audio,Video and Image)</asp:Label>

                                </td>
                            </tr>
                            <%-- <tr>
                <td>
                </td>
            </tr>--%>
                            <tr>

                                <td>
                                    <br />
                                    <asp:FileUpload ID="fileUpload1" runat="server" Style="padding-left: 20px;" AllowMultiple="true" Font-Bold="true" />
                                    <br />
                                </td>
                            </tr>
                            <%-- <tr>
                <td>
                </td>
            </tr>--%>
                            <tr style="width: auto; height: auto">

                                <td>

                                    <%-- Effect From:<asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                   
                        Effect To:<asp:TextBox ID="txtTo" runat="server"></asp:TextBox> --%>
                    Effect From:<input name="txtFrom" type="date" cssclass="TEXTAREA" maxlength="5"
                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                        onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="1" skinid="MandTxtBox" />

                                    Effect To:<input name="txtTo" type="date" cssclass="TEXTAREA" maxlength="5"
                                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="2" skinid="MandTxtBox" />





                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="state" style="font-size: small; margin-left: 0px;" class="form-label">Save Type</label>
                                    <asp:DropDownList ID="fold" runat="server" EnableViewState="true" Width="150px" Font-Size="medium"
                                        AutoPostBack="false" TabIndex="5" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                        <asp:ListItem Value="1">Detailing</asp:ListItem>
                                        <asp:ListItem Value="2">Others</asp:ListItem>
                                        <asp:ListItem Value="3">Product Launch</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:Label ID="label12" runat="server" ForeColor="Green" Font-Size="XX-Small" Font-Bold="true"></asp:Label><br />
                                </td>
                            </tr>
                            <%--<tr>
                <td>
                </td>
            </tr>--%>
                            <tr>
                                <td align="center">

                                    <asp:ImageButton ID="Button1" runat="server" Width="90px" Height="30px" ImageUrl="~/Images/but_upload.png" Text="Upload" OnClick="OnLnkUpload_Click" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Panel runat="server" ID="FriendDropDown"
                                            Style="max-height: 300px; overflow: scroll; visibility: visible;">
                                            <asp:GridView ID="gvDetails" runat="server" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowEditing="gvDetails_OnRowEditing"
                                                OnRowDeleting="gvDetails_OnRowDeleting" OnRowUpdating="gvDetails_OnRowUpdating"
                                                OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                                                AlternatingRowStyle-CssClass="alt" AllowSorting="True" DataKeyNames="ID" ShowHeaderWhenEmpty="True">
                                                <HeaderStyle Font-Bold="False" />
                                                <PagerStyle CssClass="gridview1"></PagerStyle>
                                                <SelectedRowStyle BackColor="BurlyWood" />
                                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (gvDetails.PageIndex * gvDetails.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="270" HeaderStyle-BorderWidth="1" Visible="false"
                                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbleid" runat="server" Font-Size="9pt" Text='<%#Eval("ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="filename" runat="server" Font-Size="9pt" Text='<%#Eval("File_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%-- <EditItemTemplate>  
                           <asp:FileUpload ID="fileUpload11" runat="server" />
                    </EditItemTemplate>--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effect From" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffect_From" runat="server" Font-Size="9pt" Text='<%#Eval("Effect_From")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEffect_from" runat="server" Style="width: 70px;" DataFormatString="{dd/MM/yyyy}" Text='<%#Eval("Effect_From") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effect To" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Effectto" runat="server" Font-Size="9pt" Text='<%#Eval("Effect_To")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEffect_To" runat="server" Style="width: 70px;" DataFormatString="{dd/MM/yyyy}" Text='<%#Eval("Effect_To") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Button ID="btnupdate" runat="server" Text="Update" CommandName="Update" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName="Delete" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />

                    </div>
                    <div style="max-height: 330px; min-height: 200px; width: 44%; left: 8px; top: -40px; overflow: auto" class="col-sm-8 card">
                        <table id="tblee1" border="1" style="margin-top: 10px" class="tbltax">
                            <thead>
                                <tr>
                                    <th colspan="3">
                                        <label for="cntry" style="font-size: large; margin-left: 168px" class="form-label">TaxAllocation </label>
                                    </th>
                                </tr>
                                <tr>
                                    <th align="center" class="stylespc">
                                        <label for="cntry" style="font-size: medium; margin-left: 50px" class="form-label">Tax </label>
                                    </th>
                                    <th>
                                        <label for="state" style="font-size: medium; margin-left: 122px;" class="form-label">States</label></th>
                                    <th align="left" class="" style="width: 32px;">
                                        <label for="add" style="font-size: medium;" class="form-label">Add</label></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="taxdet">
                                    <td align="left" style="width: 129px">
                                        <select name="cntry1" class="col-2 taxval" id="cntry1" style="width: 100px; margin-left: 18px;"></select></td>
                                    <td align="left" style="width: 312px;">
                                        <select id="state1" name="state1" class="col-6 state" multiple></select></td>
                                    <td align="left"><i class="fa fa-plus-circle col-2 addrows" style="font-size: 25px; color: green" id="addcountry" onclick="addtax()"></i></td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <%-- <button type="button" style="margin-left: 200px" class="btn btn-default" onclick="savetaxdet()">Save</button>--%>
                    </div>





                    <div class="col-sm-4 card" style="margin-top: -50px">
                        <table cellpadding="3" cellspacing="3">
                            <tr>
                                <td></td>
                                <td align="center">
                                    <asp:Label ID="lblTitle_LocationDtls" runat="server" Visible="true" Width="210px" Text="Select the State/Location"
                                        TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="Small" ForeColor="#8A2EE6">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="center">
                                    <asp:CheckBox ID="ChkAll" runat="server" Visible="false" Text=" All" OnCheckedChanged="CheckBox1_CheckedChanged"
                                        AutoPostBack="true" />
                                    <asp:CheckBoxList ID="chkboxLocation" runat="server" Visible="true" CssClass="chkboxLocation" DataTextField="State_Name"
                                        DataValueField="State_Code" RepeatDirection="Vertical" RepeatColumns="4" Width="650px"
                                        TabIndex="7" Style="font-size: x-small; color: black; font-family: Verdana;">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="3" cellspacing="3" style="margin-top: 26px; margin-left: 10px;">
                            <tr>
                                <td></td>
                                <td rowspan="" align="center">
                                    <asp:Label ID="lbldivision" runat="server" Width="210px" Text="Division" TabIndex="6"
                                        BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="Small" ForeColor="#8A2EE6">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="center">
                                    <%-- <asp:CheckBox ID="chkNil" runat="server" Checked="true" Text="NIL" onclick="checkNIL(this);"
                            OnClientClick="return check()" OnCheckedChanged="chkNil_CheckedChanged" />--%>
                                    <asp:CheckBoxList ID="chkSubdiv" runat="server" DataTextField="subdivision_name"
                                        CssClass="chkboxLocation" DataValueField="subdivision_code" RepeatDirection="Vertical"
                                        RepeatColumns="4" Width="650px" TabIndex="7" Style="font-size: x-small; color: black; font-family: Verdana;">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                        <table align="center" width="70%" cellpadding="10" cellspacing="10">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md"
                                        Text="Save" OnClick="Submit_Click" OnClientClick="return ValidateCheckBoxList()" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </center>
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../Images/loader.gif" alt="" />
                </div>
            </div>
        </form>


    </body>

    </html>
</asp:Content>
