<%@ Page Title="Order Entry" Language="C#" AutoEventWireup="true" CodeFile="Sec_Entry.aspx.cs" MasterPageFile="~/Master.master"
    Inherits="MasterFiles_Sec_Entry" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
    <%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>--%>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Sec Entry</title>

        <link href="../css/font-awesome.css" rel="stylesheet" type="text/css" />
        <script src="../JsFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
        <script src="../JsFiles/DCR_Entry.js" type="text/javascript"></script>
        <link rel="stylesheet" type="text/css" href="ui/css/jquery.timeentry.css" />
        <script type="text/javascript" src="ui/js/jquery.mousewheel.js"></script>
        <script type="text/javascript" src="ui/js/jquery.plugin.js"></script>
        <script type="text/javascript" src="ui/js/jquery.timeentry.js"></script>



        <style type="text/css">
            thead {
                display: block;
                vertical-align: middle;
                border-color: inherit;
            }

            #chkboxLocation label {
                margin-bottom: 0px;
            }

            table td, table th {
                margin-bottom: 0px;
                padding: 0px;
            }


            .HDBg1 {
                color: #41505b;
                font-weight: normal;
                text-shadow: 0 2px #c2d0db;
                background: #d1d3dc;
                border-color: #909eab #768791 #50606e;
                background-image: -webkit-linear-gradient(top, #E1E1E2, #B1BDC7 70%, #CDD6DB);
                background-image: -moz-linear-gradient(top, #E1E1E2, #B1BDC7 70%, #CDD6DB);
                background-image: -o-linear-gradient(top, #E1E1E2, #B1BDC7 70%, #CDD6DB);
                background-image: linear-gradient(to bottom, #E1E1E2, #B1BDC7 70%, #CDD6DB);
                -webkit-box-shadow: inset 0 1px #d0dae2, inset 0 0 0 1px #99b5ce, 0 1px #50606e, 0 1px #627786, 0 1px #50606e, 0 1px 1px rgba(0, 0, 0, 0.4);
                box-shadow: inset 0 1px #d0dae2, inset 0 0 0 1px #99b5ce, 0 1px #50606e, 0 1px #627786, 0 1px #50606e, 0 1px 1px rgba(0, 0, 0, 0.4);
            }

            .pad1 {
                display: block;
                padding: 5px;
                height: 100px;
            }

            .column {
                float: left;
                width: 30%;
                padding: 10px;
                margin-top: -8px;
                margin-left: -10px;
                /* Should be removed. Only for demonstration */
            }

            .center {
                margin: auto;
                width: 60%;
                padding: 20px;
                box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            }

            .hideform {
                display: none;
            }

            .hCap {
                padding: 5px 10px;
            }

            .highlightor {
                color: #0063ff;
                font-weight: bold;
                /* background: rgba(28,99,4,.75); */
                padding: 2px 5px;
            }

            .dtDisp {
                padding: 2px 8px;
                color: #20356E;
                font-weight: bolder;
                background: #89C15A;
                background: rgba(137, 193, 90,1);
            }

            #SFInf, .dtDisp {
                display: inline-block;
            }

            .ddlBox {
                display: block;
                width: 90%;
                padding: 3px;
                background: rgb(191,210,85);
                background: -moz-linear-gradient(top, rgba(191,210,85,1) 0%, rgba(142,185,42,1) 50%, rgba(114,170,0,1) 51%, rgba(158,203,45,1) 100%);
                background: -webkit-linear-gradient(top, rgba(191,210,85,1) 0%,rgba(142,185,42,1) 50%,rgba(114,170,0,1) 51%,rgba(158,203,45,1) 100%);
                background: linear-gradient(to bottom, rgba(191,210,85,1) 0%,rgba(142,185,42,1) 50%,rgba(114,170,0,1) 51%,rgba(158,203,45,1) 100%);
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#bfd255', endColorstr='#9ecb2d',GradientType=0 );
                border: 1px solid;
                border-color: #63B5FF;
                box-shadow: inset 0 1px #d0dae2, inset 0 0 0 1px #99b5ce, 0 1px #50606e, 0 1px #627786, 0 1px #50606e, 0 1px 1px rgba(0, 0, 0, 0.4);
            }

            .scrollbar {
                float: left;
                background: #F5F5F5;
                height: 300px;
                overflow-y: auto;
                overflow-x: hidden;
                margin-bottom: 25px;
            }



            button.btn.btn-primary {
                width: 100%;
                margin-left: 1%;
            }

            button.btn.btn-success {
                width: 100%;
                margin-left: 1%;
            }

            button.btn.btn-primary1 {
                margin-left: 45%;
                margin-top: 1%;
            }

            .col-sm-4 {
                border-left: 2px solid #d2c8c8;
            }

            .h2color {
                color: orange;
                font-size: 13px;
            }

            .h3color {
                color: purple;
                font-size: 12px;
            }

            .h4color {
                font-size: 12px;
                width: 100%;
                margin-bottom: 3%;
                height: 35px;
                border: 1px solid red;
            }
            /* Popup box BEGIN */
            .hover_bkgr_fricc {
                background: rgba(0,0,0,.4);
                cursor: pointer;
                display: none;
                height: 100%;
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                z-index: 10000;
            }

                .hover_bkgr_fricc .helper {
                    display: inline-block;
                    height: 100%;
                    vertical-align: middle;
                }

                .hover_bkgr_fricc > div {
                    background-color: #fff;
                    box-shadow: 10px 10px 60px #555;
                    display: inline-block;
                    height: auto;
                    vertical-align: middle;
                    width: 80%;
                    position: relative;
                    border-radius: 8px;
                    padding: 15px 5%;
                    left: 50%;
                    top: 50%;
                    transform: translate(-50%,-50%);
                }

            .popupCloseButton {
                background-color: #fff;
                border: 3px solid #999;
                border-radius: 50px;
                cursor: pointer;
                display: inline-block;
                font-family: arial;
                font-weight: bold;
                position: absolute;
                top: -20px;
                right: -20px;
                font-size: 25px;
                line-height: 30px;
                width: 30px;
                height: 30px;
                text-align: center;
            }

                .popupCloseButton:hover {
                    background-color: #ccc;
                }

            .trigger_popup_fricc {
                cursor: pointer;
                font-size: 20px;
                margin: 20px;
                display: inline-block;
                font-weight: bold;
            }

            /* Popup box BEGIN */
            .hover_bkgr_fricc1 {
                background: rgba(0,0,0,.4);
                cursor: pointer;
                display: none;
                height: 100%;
                position: fixed;
                top: 0;
                left: 0;
                width: 109%;
                z-index: 10000;
            }

                .hover_bkgr_fricc1 .helper {
                    display: inline-block;
                    height: 100%;
                    vertical-align: middle;
                }

                .hover_bkgr_fricc1 > div {
                    background-color: #fff;
                    box-shadow: 10px 10px 60px #555;
                    display: inline-block;
                    height: auto;
                    vertical-align: middle;
                    width: 60%;
                    position: relative;
                    border-radius: 8px;
                    padding: 15px 5%;
                    left: 50%;
                    top: 50%;
                    transform: translate(-50%,-50%);
                }

            .popupCloseButton1 {
                background-color: #fff;
                border: 3px solid #999;
                border-radius: 50px;
                cursor: pointer;
                display: inline-block;
                font-family: arial;
                font-weight: bold;
                position: absolute;
                top: -20px;
                right: -20px;
                font-size: 25px;
                line-height: 30px;
                width: 30px;
                height: 30px;
                text-align: center;
            }

                .popupCloseButton1:hover {
                    background-color: #ccc;
                }

            .trigger_popup_fricc1 {
                cursor: pointer;
                font-size: 20px;
                margin: 20px;
                display: inline-block;
                font-weight: bold;
            }

            .table {
                width: 100%;
                /* margin-bottom: 20px; */
            }

            .pb-cmnt-container {
                font-family: Lato;
            }

            .pb-cmnt-textarea {
                resize: none;
                padding: 20px;
                height: 130px;
                width: 100%;
                border: 1px solid #F2F2F2;
            }
            /* Popup box BEGIN */
        </style>
        <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">

            var sRetailers = [];
            var selRetInd = [];
            var sRetailers1 = [];
            var selRetInd1 = [];
            var objMain = {};
            $(document).ready(function () {

                $('input:text:first').focus();
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

                $(document).ready(function () {


                    $(document).on('click', '#show', function (e) {
                        $('.hover_bkgr_fricc').attr("indexid", $(this).attr("indexid"));
                        BindGridView();
                        //$("[id*=sub_tot]").val(0);
                        $('.hover_bkgr_fricc').show();

                        //   $(this).hide();
                    })

                    $('.popupCloseButton').on('click', function () {
                        $('#show').show();
                        $('.hover_bkgr_fricc').hide();

                    })

                    $(document).on('click', '#show1', function (e) {
                        $('.hover_bkgr_fricc1').attr("indexid", $(this).attr("indexid"));
                        BindGridView1();
                        //$("[id*=sub_tot1]").val(0);
                        $('.hover_bkgr_fricc1').show();
                        //$(this).hide();
                    })

                    $('.popupCloseButton1').on('click', function () {
                        $('#show1').show();
                        $('.hover_bkgr_fricc1').hide();

                    })

                    $(document).on('keyup', '[id*=txtQty]', function () {

                        if (!jQuery.trim($(this).val()) == '') {
                            if (!isNaN(parseFloat($(this).val()))) {
                                var row = $(this).closest("tr");
                                var dis = row.find('[id*=txtDis]').val();
                                row.find('[id*=txtVal]').val(row.find('[id*=txtRate]').val() * row.find('[id*=txtQty]').val() - (parseInt(dis)));
                                //row.find('[id*=txtVal]').val(((row.find('[id*=txtRate]').val() * row.find('[id*=txtQty]').val())).toFixed(2));
                            }
                        } else {
                            $(this).val('');
                        }
                        var grandTotal = 0;
                        $("[id*=txtVal]").each(function () {
                            grandTotal = (grandTotal + parseFloat($(this).val()));
                        });
                        $("[id*=sub_tot]").val(grandTotal.toFixed(2));


                    });

                    //dis_count
                    $(document).on('keyup', '[id*=txtDis]', function () {

                        if (!jQuery.trim($(this).val()) == '') {
                            if (!isNaN(parseFloat($(this).val()))) {
                                var row = $(this).closest("tr");

                                var amd = row.find('[id*=txtRate]').val();
                                var dis = row.find('[id*=txtDis]').val();
                                if (dis != '' && amd != '') {

                                    Val = (row.find('[id*=txtRate]').val() * row.find('[id*=txtQty]').val()) * (dis / 100);
                                    row.find('[id*=txtVal]').val(((row.find('[id*=txtRate]').val() * row.find('[id*=txtQty]').val()) - Val).toFixed(2));

                                }

                            }
                        } else {
                            $(this).val('');
                        }

                        var grandTotal = 0;
                        $("[id*=txtVal]").each(function () {
                            grandTotal = (grandTotal + parseFloat($(this).val()));
                        });
                        $("[id*=sub_tot]").val(grandTotal.toFixed(2));

                    });

                    $(document).on('keyup', '[id*=txtcqty]', function () {

                        if (!jQuery.trim($(this).val()) == '') {
                            if (!isNaN(parseFloat($(this).val()))) {
                                var row = $(this).closest("tr");
                                var dis = row.find('[id*=txtpqty]').val();
                                var unit = row.find('[id*=txtunit1]').val();
                                row.find('[id*=txtVal1]').val((row.find('[id*=txtNw1]').val() * (row.find('[id*=txtcqty]').val()) + row.find('[id*=txtRate1]').val() * (row.find('[id*=txtpqty]').val())));
                                //row.find('[id*=txtVal]').val(((row.find('[id*=txtRate]').val() * row.find('[id*=txtQty]').val())).toFixed(2));
                            }
                        } else {
                            $(this).val('');
                        }
                        var grandTotal = 0;
                        $("[id*=txtVal1]").each(function () {
                            grandTotal = (grandTotal + parseFloat($(this).val()));
                        });
                        $("[id*=sub_tot1]").val(grandTotal.toFixed(2));


                    });

                    $(document).on('keyup', '[id*=txtpqty]', function () {
                        var row = $(this).closest("tr");
                        var pq = row.find('[id*=txtpqty]').val();
                        var u = row.find('[id*=txtunit1]').val();

                        if (pq < u) {


                            if (!jQuery.trim($(this).val()) == '') {
                                if (!isNaN(parseFloat($(this).val()))) {

                                    var dis = row.find('[id*=txtcqty]').val();
                                    var unit = row.find('[id*=txtunit1]').val();
                                    row.find('[id*=txtVal1]').val((row.find('[id*=txtNw1]').val() * (row.find('[id*=txtcqty]').val()) + row.find('[id*=txtRate1]').val() * (row.find('[id*=txtpqty]').val())));
                                    //row.find('[id*=txtVal]').val(((row.find('[id*=txtRate]').val() * row.find('[id*=txtQty]').val())).toFixed(2));
                                }
                            } else {
                                $(this).val('');
                            }
                            var grandTotal = 0;
                            $("[id*=txtVal1]").each(function () {
                                grandTotal = (grandTotal + parseFloat($(this).val()));
                            });
                            $("[id*=sub_tot1]").val(grandTotal.toFixed(2));
                        }
                        else {
                            alert('Conversion factor Value: ' + u + ' You Enter Below This Value');
                            row.find('[id*=txtpqty]').val(0);
                        }


                    });


                });





                function BindGridView() {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Sec_Entry.aspx/GetProduct",
                        data: "{'TransSlNo':'" + $("input[name='chklistitem']:checked").val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
                            var str = "";
                            idex = $('.hover_bkgr_fricc').attr("indexid");
                            productArr = sRetailers[idex]["Product"] || [];
                            $('#tblCustomers >tbody').html('');
                            for (var i = 0; i < data.d.length; i++) {
                                itm = productArr.filter(function (a) {
                                    return data.d[i].pCode == a.pCode
                                });
                                str = "<td>" + (i + 1) + "</td><td> <input type='hidden'name='pCode' value='" + data.d[i].pCode + "'/>" + data.d[i].pName + "</td><td><input type='text' name='txtRate' id='txtRate' value='" + data.d[i].pUOM + "' /></td>";

                                if (itm.length > 0) {
                                    $('#<%=ddl_dis.ClientID %>').val(itm[0].DisCd);


                                    str += "<td><input type='text' name='txtDis' value='" + itm[0].disco + "' id='txtDis' /></td><td><input type='text' name='txtFree' value='" + itm[0].free + "' id='txtFree' /></td><td><input type='text' name='txtQty' value='" + itm[0].pqty + "' id='txtQty' /></td><td><input type='text' name='txtVal' id='txtVal' value='" + itm[0].pval + "' disabled/>";
                                    $('#sub_tot').val(itm[0].tot);
                                } else {
                                    str += "<td><input type='text' name='txtDis' value='0' id='txtDis' /><td><input type='text' name='txtFree' value='0' id='txtFree' /></td><td><input type='text' name='txtQty' value='0' id='txtQty' /></td><td><input type='text' name='txtVal' id='txtVal' value='0' disabled/>";
                                    //$('#sub_tot').val(0);
                                }
                                str += "<input type='hidden' name='txtNw' value='" + data.d[i].pUOM_Name + "' /><input type='hidden' name='txtunit' value='" + data.d[i].Con_unit + "' /></td>";

                                $('#tblCustomers >tbody').append('<tr>' + str + ' </tr>');
                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });

                }

                //primary order

                function BindGridView1() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Sec_Entry.aspx/GetProduct1",
                        data: "{'TransSlNo':'" + $("input[name='chklistitem']:checked").val() + "'}",
                        dataType: "json",
                        success: function (data) {

                            var str = "";
                            idex = $('.hover_bkgr_fricc1').attr("indexid");
                            productArr1 = sRetailers1[idex]["product1"] || [];
                            $('#tblCustomers1 >tbody').html('');
                            for (var p = 0; p < data.d.length; p++) {
                                itm1 = productArr1.filter(function (a) {
                                    return data.d[p].pCode1 == a.pCode12
                                });
                                str = "<td>" + (p + 1) + "</td><td> <input type='hidden'name='pCode1' value='" + data.d[p].pCode1 + "'/>" + data.d[p].pName1 + "</td><td><input type='text' name='txtRate1' id='txtRate1'  value='" + data.d[p].pUOM1 + "' disabled /></td>";
                                if (itm1.length > 0) {
                                    $('#<%=ddlTerritoryName.ClientID %>').val(itm1[0].TwnCd);
                                    str += "<td><input type='text' name='txtcqty' value='" + itm1[0].cqty + "' id='txtcqty' /><td><input type='text' name='txtpqty' value='" + itm1[0].pqty + "' id='txtpqty' /></td><td><input type='text' name='txtVal1' id='txtVal1' value='" + itm1[0].pval + "' disabled/>";
                                    $('#sub_tot1').val(itm1[0].tot1);
                                }
                                else {

                                    str += "<td><input type='text' name='txtcqty' value='0' id='txtcqty' /><td><input type='text' name='txtpqty' value='0' id='txtpqty' /></td><td><input type='text' name='txtVal1' id='txtVal1' value='0' disabled/>";

                                }
                                str += "<input type='hidden' name='txtNw1' id='txtNw1' value='" + data.d[p].pUOM_Name1 + "' /><input type='hidden' name='txtunit1' id='txtunit1' value='" + data.d[p].Con_unit1 + "' /></td>";

                                $('#tblCustomers1 >tbody').append('<tr>' + str + ' </tr>');


                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });

                }
                //sec
                $(document).on('click', '#btnSave1', function () {
                    console.log($('.hover_bkgr_fricc'));
                    idex = $('.hover_bkgr_fricc').attr("indexid");

                    var stockistFrom = $('#<%=ddl_dis.ClientID %>').val();
                    if (stockistFrom == 0) { alert('Select Supplier Name!!'); $('#<%=ddl_dis.ClientID %>').focus(); return false; }
                    //var rem = document.getElementById("Remark").value;


                    var countr = 0;

                    var productArr = [];
                    $('#tblCustomers >tbody >tr').each(function () {
                        if ($(this).find("input[name=txtQty]").val() > 0) {
                            //var oqty = $(this).find("input[name=txtQty]").val();
                            productArr.push({
                                pCode: $(this).find("input[name=pCode]").val(),
                                pName: $(this).find('td').eq(1).text(),
                                prate: $(this).find("input[name=txtRate]").val(),
                                disco: $(this).find("input[name=txtDis]").val(),
                                free: $(this).find("input[name=txtFree]").val(),
                                pqty: $(this).find("input[name=txtQty]").val(),
                                pval: $(this).find("input[name=txtVal]").val(),
                                Nval: $(this).find("input[name=txtNw]").val(),
                                tot: $('#sub_tot').val(),
                                TwnCd: $('#<%=ddlTerritoryName.ClientID %>').val(),
                                TwnNw: $('#<%=ddlTerritoryName.ClientID %> option:selected').text(),
                                DisNm: $('#<%=ddl_dis.ClientID %> option:selected').text(),
                                DisCd: $('#<%=ddl_dis.ClientID %>').val()
                            });
                            countr++;
                        }
                    });

                    if (countr <= 0) {
                        alert('Enter Atleast One Row!!!');
                        $('#tblCustomers').focus();
                        return false;
                    }



                    sRetailers[idex]["Product"] = productArr;
                    console.log(sRetailers);
                    $('.hover_bkgr_fricc').hide();

                });

                //primary
                $(document).on('click', '#btnSave2', function () {
                    idex1 = $('.hover_bkgr_fricc1').attr("indexid");


                    var countr = 0;

                    var productArr1 = [];
                    $('#tblCustomers1 >tbody >tr').each(function () {
                        if ($(this).find("input[name=txtcqty]").val() > 0) {
                            //var oqty = $(this).find("input[name=txtQty]").val();
                            productArr1.push({
                                pCode12: $(this).find("input[name=pCode1]").val(),
                                pName12: $(this).find('td').eq(1).text(),
                                prate12: $(this).find("input[name=txtRate1]").val(),
                                cqty: $(this).find("input[name=txtcqty]").val(),
                                pqty: $(this).find("input[name=txtpqty]").val(),
                                pval: $(this).find("input[name=txtVal1]").val(),
                                Nval: $(this).find("input[name=txtNw1]").val(),
                                tot1: $('#sub_tot1').val(),
                                TwnNw: $('#<%=ddlTerritoryName.ClientID %> option:selected').text(),
                                TwnCd: $('#<%=ddlTerritoryName.ClientID %>').val()
                            });
                            countr++;
                        }
                    });

                    if (countr <= 0) {
                        alert('Enter Atleast One Row!!!');
                        $('#tblCustomers1').focus();
                        return false;
                    }



                    sRetailers1[idex1]["product1"] = productArr1;
                    console.log(sRetailers1);
                    $('.hover_bkgr_fricc1').hide();

                });

                //final save
                var objMain = {};
                $(document).on('click', '#btnSave', function () {

                    var TerrFrom = $('#<%=ddlTerritoryName.ClientID %>').val();
                    if (TerrFrom == 0) { alert('Select Rount Name!!'); $('#<%=ddlTerritoryName.ClientID %>').focus(); return false; }


                  

                        if (confirm('Are you sure you want to Save this Order?')) {




                            var selact = [];
                            for (i = 0; i < selRetInd.length; i++) {

                                var Remark = $('#txtRemItem' + i).val();
                                sRetailers[i].Remark = Remark;
                                selact.push(sRetailers[selRetInd[i]]);
                            }

                            var selact1 = [];
                            for (i = 0; i < selRetInd1.length; i++) {

                                var Remark1 = $('#txtRemItem1' + i).val();
                                sRetailers1[i].Remark1 = Remark1;
                                selact1.push(sRetailers1[selRetInd1[i]]);
                            }
                            objMain['TransS'] = selact;
                            objMain['TransP'] = selact1;
                            console.log(objMain);


                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "Sec_Entry.aspx/SaveDate",
                                data: "{'data':'" + JSON.stringify(objMain) + "'}",
                                dataType: "json",
                                success: function (data) {
                                    //alert(data.d);

                                    var url = "Sec_Entry.aspx";
                                    window.location = url;
                                    alert("Order has been updated successfully!!!");
                                },
                                error: function (data) {
                                    alert(JSON.stringify(data));
                                }
                            });
                        }
                        else {
                            alert('Order Not Save as You pressed Cancel !');
                        }
                    


                });

                //remark
                $(document).on('click', '#btn_Rmk', function () {



                    var Rmk = $('#<%=ddl_worktype.ClientID %>').val();
                    if (Rmk == "F") { alert('Select WorkType Name!!'); $('#<%=ddl_worktype.ClientID %>').focus(); return false; }

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Sec_Entry.aspx/RemarkDate",
                        data: "{'data':'" + $('textarea#message').val() + "','fwlg':'" + $('#<%=ddl_worktype.ClientID %> option:selected').text() + "'}",
                        dataType: "json",
                        success: function (data) {
                            //alert(data.d);

                            var url = "Sec_Entry.aspx";
                            window.location = url;
                            alert("Remarks has been updated successfully!!!");
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });



                });



            });
        </script>
        <script type="text/javascript" language="javascript">
            function PopulateCheckBoxList() {
                $.ajax({
                    type: "POST",
                    url: "Sec_Entry.aspx/GetCheckBoxDetails",
                    contentType: "application/json; charset=utf-8",
                    data: "{'TransSlNo':'" + $('#<%=ddlTerritoryName.ClientID %>').val() + "'}",
                    dataType: "json",
                    success: AjaxSucceeded,
                    error: AjaxFailed
                });
            }
            function AjaxSucceeded(result) {
                BindCheckBoxList(result);
                document.getElementById("btnSave").style.display = "block";
            }
            function AjaxFailed(result) {
                alert('Failed to load checkbox list');
            }
            function BindCheckBoxList(result) {

                sRetailers = JSON.parse(result.d) || [];
                CreateCheckBoxList(sRetailers);
            }
            function CreateCheckBoxList(checkboxlistItems) {
                $('#dvCheckBoxListControl').find('div').remove();
                var table = $('<div class="col-md-4 offset-md-3" style="display: grid; grid-template-columns: 300px 300px 300px;"></div>');
                var counter = 0;
                $(checkboxlistItems).each(function () {
                    table.append($('<ul class="list-group list-group-flush"></ul>').append($('<li class="list-group-item"></li>').append($('<input>').attr({
                        type: 'checkbox', name: 'chklistitem', value: this.Value, id: 'chklistitem' + counter, indexid: counter
                    }).on("change", function () { AddSelRetailer(this) })).append(
                    $('<label>').attr({
                        'for': 'chklistitem' + counter
                    }).text(this.Name)).append($('<div></div>')).append(
                    $('<label>').attr({
                        'class': 'h2color', 'for': 'chklistitem' + counter
                    }).text('Address : ' + this.address)).append($('<div ></div>')).append(
                    $('<label>').attr({
                        'class': 'h3color', 'for': 'chklistitem' + counter
                    }).text('Mobile No : ' + this.Mobile)).append($('<div ></div>')).append(
                    $('<input>').attr({
                        'class': 'h4color', 'placeholder': ' Enter Remark ', 'id': 'txtRemItem' + counter
                    }).text(this.Remark)).append($('<div></div>')).append($('<div><button id="show"  type="button" indexid="' + counter + '" class="btn btn-success" disabled="true" >SECONDARY ORDERS</button></div>'))));
                    counter++;
                });

                $('#dvCheckBoxListControl').append(table);






            }
            function AddSelRetailer(x) {
                indx = $(x).attr('indexid');
                if ($(x).attr('checked')) {

                    selRetInd.push(indx);
                    svbtn = $('.btn-success');
                    $(svbtn[indx]).attr('disabled', false);
                }
                else {

                    selRetInd.splice(selRetInd.indexOf(indx), 1);
                    svbtn = $('.btn-success');
                    $(svbtn[indx]).attr('disabled', true);
                    //document.getElementById("show").disabled = true;
                }
                //console.log(selRetInd);


                /*if (selRetInd.filter(function (a) {
                    return a == indx;
                })<1)*/
            }
            //primary
            function PopulateCheckBoxList1() {
                $.ajax({
                    type: "POST",
                    url: "Sec_Entry.aspx/GetCheckBoxDetails1",
                    contentType: "application/json; charset=utf-8",
                    data: "{'TransSlNo':'" + $('#<%=ddlTerritoryName.ClientID %>').val() + "'}",
                    dataType: "json",
                    success: AjaxSucceeded1,
                    error: AjaxFailed
                });
            }
            function AjaxSucceeded1(result) {
                BindCheckBoxList1(result);
                document.getElementById("btnSave").style.display = "block";
            }
            function AjaxFailed1(result) {
                alert('Failed to load checkbox list');
            }
            function BindCheckBoxList1(result) {


                sRetailers1 = JSON.parse(result.d) || [];
                CreateCheckBoxList1(sRetailers1);
            }
            function CreateCheckBoxList1(checkboxlistItems1) {
                $('#dvCheckBoxListControl1').find('div').remove();
                var table = $('<div class="col-md-4 offset-md-3" style="display: grid; grid-template-columns: 300px 300px 300px;"></div>');
                var counter = 0;
                $(checkboxlistItems1).each(function () {
                    table.append($('<ul class="list-group list-group-flush"></ul>').append($('<li class="list-group-item"></li>').append($('<input>').attr({
                        type: 'checkbox', name: 'chklistitem1', class: 'keys', value: this.Value1, id: 'chklistitem1' + counter, indexid: counter
                    }).on("change", function () { AddSelRetailer1(this) })).append(
                    $('<label>').attr({
                        'for': 'chklistitem1' + counter
                    }).text(this.Name1)).append($('<div></div>')).append(
                    $('<label>').attr({
                        'class': 'h2color', 'for': 'chklistitem1' + counter
                    }).text('Address : ' + this.address1)).append($('<div ></div>')).append(
                    $('<label>').attr({
                        'class': 'h3color', 'for': 'chklistitem1' + counter
                    }).text('Mobile No : ' + this.Mobile1)).append(
                    $('<input>').attr({
                        'class': 'h4color', 'placeholder': ' Enter Remark ', 'for': 'txtRemItem1' + counter
                    }).text(this.Remark1)).append($('<div></div>')).append($('<div><button id="show1" indexid="' + counter + '" type="button" disabled="true" class="btn btn-primary" >PRIMARY ORDERS</button></div>'))));
                    counter++;
                });

                $('#dvCheckBoxListControl1').append(table);
            }

            function AddSelRetailer1(x1) {
                indx1 = $(x1).attr('indexid');
                if ($(x1).attr('checked')) {

                    selRetInd1.push(indx1);
                    svbtn1 = $('.btn-primary');
                    $(svbtn1[indx1]).attr('disabled', false);
                }
                else {

                    selRetInd1.splice(selRetInd1.indexOf(indx1), 1);
                    svbtn1 = $('.btn-primary');
                    $(svbtn1[indx1]).attr('disabled', true);
                    //document.getElementById("show").disabled = true;
                }
            }



        </script>

    </head>
    <body>
        <form id="form1" runat="server">
            <asp:HiddenField ID="hdnSfcode" runat="server" />
            <asp:HiddenField ID="HdnDate" runat="server" />
            <div class="pad1 HDBg1">
                <div class='hCap'>
                    Daily Calls Report For :
                <div runat="server" clientidmode="Static" class="highlightor" id="SFInf"></div>
                    Date -
                <div id="DtInf" runat="server" clientidmode="Static" class="dtDisp"></div>
                </div>




                <div class="row" style="padding: 0px 25px;">

                    <div class="column">
                        <div runat="server" class="plnPlholder" id="Div2">
                            <span class="lblCap">Work Type</span>
                            <asp:DropDownList ID="ddl_worktype" runat="server" class="ddlBox"
                                onkeypress="AlphaNumeric_NoSpecialChars(event);" OnSelectedIndexChanged="ddl_worktype_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="column" runat="server" id="theDiv">
                        <div runat="server" class="plnPlholder" id="Div3">
                            <span class="lblCap">Head Quarter</span>
                            <asp:DropDownList ID="ddl_HQ" runat="server" class="ddlBox"
                                onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                                onselectedindexchanged="ddl_HQ_SelectedIndexChanged" AutoPostBack="true" Visible="false" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="column">
                        <div runat="server" class="plnPlholder" id="Plchold_HQ">
                            <span id="ter" runat="server" class="lblCap">Route</span>
                            <asp:DropDownList ID="ddlTerritoryName" runat="server" class="ddlBox"
                                onchange="PopulateCheckBoxList(),PopulateCheckBoxList1();" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>

            </div>



            <div>


                <br />
                <asp:Panel ID="Panel2" runat="server" Visible="false">
                    <div class="container pb-cmnt-container">
                        <div class="row">
                            <div class="col-md-11">
                                <div class="panel panel-info">
                                    <div class="panel-body">
                                        <textarea cols="60" rows="5" id="message" name="message" placeholder="Write your Remarks here!" class="pb-cmnt-textarea"></textarea>
                                        <button id="btn_Rmk" class="btn btn-primary1" type="button">Submit</button>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">

                    <div class="col-md-12" style="padding: 0px;">
                        <div id="exTab3" class="container">
                            <ul class="nav nav-pills">
                                <li class="active"><a href="#1b" id="TabSec" data-toggle="tab">Secondary</a> </li>
                                <li><a href="#2b" id="TabPri" data-toggle="tab">Primary</a> </li>

                            </ul>
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1b">
                                <div class="col-md-12" style="padding: 0px;">
                                    <div class="panel panel-default">
                                        <%--  <div class="panel-heading">
                                <i class="fa fa-bar-chart-o fa-fw"></i>Category Wise
                            </div>--%>
                                        <!-- /.panel-heading -->
                                        <div class="panel-body">

                                            <div id="dvCheckBoxListControl">
                                                <div class="card-header">Retailers </div>


                                            </div>


                                        </div>
                                        <!-- /.panel-body -->
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="2b">
                                <div class="col-md-12" style="padding: 0px;">
                                    <div class="panel panel-default">

                                        <div class="panel-body">

                                            <div id="dvCheckBoxListControl1">
                                                <div class="card-header">Distributor </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </div>



                    </div>
                </asp:Panel>
                <%--     <a class="trigger_popup_fricc">Click here to show the popup</a>

<div class="hover_bkgr_fricc">
    <span class="helper"></span>
    <div>
        <div class="popupCloseButton">X</div>
        <p>Add any HTML content<br />inside the popup box!</p>
    </div>
</div>--%>
                <br />

                <div class="hover_bkgr_fricc">
                    <div>
                        <div class="popupCloseButton">X</div>
                        <form action="/action_page.php">
                            <div class="row" style="padding: 0px 25px;">
                                <div runat="server" class="plnPlholder" id="Div1">
                                    <span class="lblCap">Supplier By</span>
                                    <asp:DropDownList ID="ddl_dis" runat="server" class="ddlBox"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <table id="tblCustomers" class="table table-responsive">
                                <thead>
                                    <tr>
                                        <th style="width: 50px">Sl.No.
                                        </th>
                                        <th style="width: 125px">Product Name
                                        </th>
                                        <th style="width: 150px">Rate
                                        </th>
                                        <th style="width: 150px">Discount(%)
                                        </th>
                                        <th style="width: 150px">Free
                                        </th>
                                        <th style="width: 150px">QTY
                                        </th>
                                        <th style="width: 150px">Value
                                        </th>

                                    </tr>
                                </thead>
                                <tbody class="scrollbar">
                                </tbody>
                            </table>

                            <div class="row">

                                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                    <div class="form-group form-inline">
                                        <label>
                                            Total: &nbsp;</label>
                                        <div class="input-group">
                                            <div class="input-group-addon currency">
                                                <i class="fa fa-inr"></i>
                                            </div>
                                            <input data-cell="G1" id="sub_tot" name="sub_tot" data-format="0,0.00" class="form-control" readonly />
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-sm-12" style="text-align: center">
                                    <a id="btnSave1" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                                        <span>Save</span></a>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>

                <div class="hover_bkgr_fricc1">
                    <div>
                        <div class="popupCloseButton1">X</div>
                        <form action="/action_page.php">
                            <table id="tblCustomers1" class="table table-responsive">
                                <thead>
                                    <tr>
                                        <th style="width: 50px">Sl.No.
                                        </th>
                                        <th style="width: 150px">Product Name
                                        </th>
                                        <th style="width: 65px">Rate
                                        </th>
                                        <th style="width: 150px">C.QTY
                                        </th>
                                        <th style="width: 150px">P.QTY
                                        </th>
                                        <th style="width: 150px">Value
                                        </th>

                                    </tr>
                                </thead>
                                <tbody class="scrollbar">
                                </tbody>

                            </table>

                            <div class="row">

                                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                    <div class="form-group form-inline">
                                        <label>
                                            Total: &nbsp;</label>
                                        <div class="input-group">
                                            <div class="input-group-addon currency">
                                                <i class="fa fa-inr"></i>
                                            </div>
                                            <input data-cell="G1" id="sub_tot1" name="sub_tot1" data-format="0,0.00" class="form-control" readonly />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12" style="text-align: center">
                                    <a id="btnSave2" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                                        <span>Save</span></a>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>

                <center>
                    <table cellpadding="5" cellspacing="10" width="8%">
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                                    Text="Save" OnClick="btnSave_Click" />--%>
                                <button id="btnSave" type="button" class="btn btn-info" style="display: none;">Save</button>
                            </td>
                            <td>
                                <%--<asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                                    Text="Clear" OnClick="btnClear_Click" />--%>
                                <button id="btnClear" type="button" class="btn btn-danger" style="display: none;">Clear</button>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </form>
    </body>


    </html>
</asp:Content>
