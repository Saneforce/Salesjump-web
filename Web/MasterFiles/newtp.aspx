<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="newtp.aspx.cs" Inherits="MasterFiles_newtp" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <!DOCTYPE html>
    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <meta charset="utf-8" />
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
            <style type="text/css">
                #grid {
                    border: 1px solid #ddd;
                    border-collapse: collapse;
                }

                th {
                    position: sticky;
                    top: 0;
                    background: #6c7ae0;
                    text-align: center;
                    font-weight: normal;
                    font-size: 15px;
                    color: white;
                }

                .a {
                    line-height: 22px;
                    padding: 3px 4px;
                    border-radius: 7px;
                }

                table td, table th {
                    padding: 5px;
                    border: 1px solid #ddd;
                }
                .gosubmit {
                    display: inline-block;
                    margin: 0;
                    cursor: pointer;
                    border: 1px solid #bbb;
                    overflow: visible;
                    font: bold 13px arial, helvetica, sans-serif;
                    text-decoration: none;
                    white-space: nowrap;
                    color: #555;
                    border-radius: 11px;
                    background-color: #ddd;
                    width: 5%;
                    height: 26px;
                }
                input[type='text'], select {
                    line-height: 22px;
                    padding: 6px 21px;
                    border: solid 1px #bbbaba;
                    border-radius: 7px;
                }
            </style>
        </head>
        <body>
            <form id="formid" class="form-horizontal" runat="server">
                <center>
                    <div id="tblMargin" style="margin-top: 35px;">
                        <div>
                            <label for="inputcomname">Field Force Name</label>
                            <select id="ddlcomname">
                                <option value="0">--Select Field Force Name--</option>
                            </select>
                        </div>
                        <div>
                            <label for="inputdisname">Distributor Name</label>
                            <select id="distname">
                                <option value="0">--Select Distributor Name--</option>
                            </select>
                        </div>
                        <div>
                            <label for="month">Month</label>
                            <select id="fltpmnth" data-size="5" data-dropup-auto="false">
                                <option value="0">Select Month</option>
                                <option value="1">January</option>
                                <option value="2">February</option>
                                <option value="3">March</option>
                                <option value="4">April</option>
                                <option value="5">May</option>
                                <option value="6">June</option>
                                <option value="7">July</option>
                                <option value="8">August</option>
                                <option value="9">September</option>
                                <option value="10">October</option>
                                <option value="11">November</option>
                                <option value="12">December</option>
                            </select>
                            <label for="year">Year</label>
                            <select id="fltpyr" data-size="5" data-dropup-auto="false">
                            </select>
                        </div>
                        <input type="button" class="gosubmit" id="gosubmit" value="GO" />
                    </div>
                    <br />
                    <br />
                    <table class="auto-index" id="grid" style='display: none '>
                        <thead>
                            <tr>
                                <th rowspan="2">S.No</th>
                                <th rowspan="2">Product</th>
                                <th rowspan="2">Price</th>
                                <th colspan="2">OP</th>
                                <th colspan="2">Purchase</th>
                                <th colspan="2">Sales</th>
                                <th colspan="2">Return</th>
                                <th colspan="2">Closing</th>
                            </tr>
                            <tr>
                                <th>Qty</th>
                                <th>Free</th>
                                <th>Qty</th>
                                <th>Free</th>
                                <th>Qty</th>
                                <th>Free</th>
                                <th>Qty</th>
                                <th>Free</th>
                                <th>Qty</th>
                                <th>Free</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                        <tfoot></tfoot>
                    </table>
                </center>
                <br />
                <br />
                <input type="submit" name="btnsave" id="btnsave" class="btn btn-primary btnsave" value="Save" style="width: 100px;display:none;" />
            </form>
            <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
            <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
            <script type="text/javascript">
                var str = "";
        var arr = [];
        var prods = [];
        var clos = [];
                $(document).ready(function () {
                    fillfilter();
                    fillTpYR();


                    $('.gosubmit').on('click', function () {
                        $("#grid").show();
                        $('#grid tbody').html('');
                        $("#btnsave").show();
                        var fieldforce = $("#ddlcomname").val();
                        var disname = $("#distname").val();
                        var month = $("#fltpmnth").val();
                        var year = $("#fltpyr").val();
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: "newtp.aspx/clsdata",
                            data: "{'ffc':'" + fieldforce + "','emonth':'" + month + "','eyear':'" + year + "','distname':'" + disname + "'}",
                            dataType: "json",
                            success: function (data) {
                                clos = JSON.parse(data.d);
                                FillProduct();
                                totalcal();
                            }
                        });

                    });

                });

                function fillfilter() {

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "newtp.aspx/GetMR",
                        dataType: "json",
                        success: function (data) {
                            $.each(data.d, function (data, value) {

                                $('#ddlcomname').append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });


                }
                $('#ddlcomname').change(function () {
                    $('#distname').html('');
                    var fieldforce = $('#ddlcomname :selected').val();
                    //var fieldforce = $("#ddlcomname").val();
                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "newtp.aspx/distname",
                        data: "{ 'fieldforcecode':'" + fieldforce + "'}",
                        dataType: "json",
                        success: function (data) {
                            $.each(data.d, function (data, value) {

                                $('#distname').append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });

                function fillTpYR() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "newtp.aspx/BindDate",
                        data: "{'divcode':'<%=Session["div_code"]%>'}",
                        dataType: "json",
                        success: function (data) {
                            var tpyr = $("#fltpyr");
                            tpyr.empty().append('<option value="0">Select Year</option>');
                            for (var i = 0; i < data.d.length; i++) {
                                tpyr.append($('<option value="' + data.d[i].value + '">' + data.d[i].text + '</option>'));
                            };
                        }
                    });
                }


                function FillProduct() {

                    var fieldforce = $("#ddlcomname").val();
                    var opprice = 0;
                    var purprice = 0;
                    var salprice = 0;
                    var clprice = 0;
                    var reprice = 0;
                    var opfprice = 0;
                    var purfprice = 0;
                    var salfprice = 0;
                    var clfprice = 0;
                    var refprice = 0;
                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "newtp.aspx/FillProd",
                        data: "{'ffc':'" + fieldforce + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#grid tbody').html('');
                            prods = JSON.parse(data.d);
                            str = '';
                            for (var i = 0; i < prods.length; i++) {
                                var flit = clos.filter(function (a) {
                                    return a.Product_Detail_Code == prods[i].Product_Detail_Code;
                                });
                                opprice += flit[0].optpr
                                purprice += flit[0].purtpr
                                salprice += flit[0].satpr
                                clprice += flit[0].cltpr
                                reprice += flit[0].reqpr
                                opfprice += flit[0].ofrpr
                                purfprice += flit[0].pfrpr
                                salfprice += flit[0].sfrpr
                                clfprice += flit[0].cfrpr
                                refprice += flit[0].refrpr
                                str += "<tr id='" + prods[i].Product_Detail_Code + "'><td>" + (i + 1) + "</td><td><input type='hidden' name='pCode' value='" + prods[i].Product_Detail_Code + "'/><span>" + prods[i].Product_Detail_Name + "</span></td><td><input type='hidden' class='prate' name='prate' value='" + flit[0].Distributor_Price + "'/><span>" + flit[0].Distributor_Price + "</span></td><td><input type='hidden' name='ppr' class='opr' value='" + flit[0].optpr + "'/><input type='text'  name='oqty' value='" + flit[0].cqty + "' class='form-control oqty' style='height: 25px;'/></td><td><input type='hidden' name='ofrpr' class='ofrpr' value='" + flit[0].ofrpr + "'/><input type='text' name='ofree' value='" + flit[0].cfree + "' class='form-control ofree' style='height: 25px;'/></td><td><input type='hidden' name='pupr' class='pupr' value='" + flit[0].purtpr + "'/><input type='text'  name='pqty' value='" + flit[0].pqty + "' class='form-control pqty' style='height: 25px;'/></td><td><input type='hidden' name='pfrpr' class='pfrpr' value='" + flit[0].pfrpr + "'/><input type='text' name='pfree' value='" + flit[0].pfree + "' class='form-control pfree' style='height: 25px;'/></td> " +
                                    "<td><input type='hidden' name='spr' class='spr' value='" + flit[0].satpr + "' /><input type='text' name='sqty' value='" + flit[0].sqty + "' class='form-control sqty' style='height: 25px;' /></td > <td><input type='hidden' name='sfrpr' class='sfrpr' value='" + flit[0].sfrpr + "' /><input type='text' name='sfree' value='" + flit[0].sfree + "' class='form-control sfree' style='height: 25px;' /></td><td><input type='hidden' name='rpr' class='rpr' value='" + flit[0].reqpr + "'/><input type='text' name='rqty' class='form-control rqty' value='" + flit[0].re_qty + "' style='height: 25px;background-color: #fff;'/></td><td><input type='hidden' name='rfrpr' class='rfrpr' value='" + flit[0].refrpr + "'/><input type='text'  name='rfree' value='" + flit[0].re_free + "' class='form-control rfree' style='height: 25px;'/></td><td><input type='hidden' name='cpr' class='cpr' value='" + flit[0].cltpr + "' /><input type='text' name='cqty' class='form-control cqty' value='" + flit[0].clqty + "' style='height: 25px;background-color: #fff;' readonly /></td> <td><input type='hidden' name='cfrpr' class='cfrpr' value='" + flit[0].cfrpr + "' /><input type='text' name='cfree' value='" + flit[0].clfree + "' class='form-control cfree' style='height: 25px;background-color: #fff; readonly'/></td></tr>";
                            }
                            $('#grid tbody').append(str);
                            $('#grid tfoot').html("<tr><td colspan=3 style='text-align: center;font-weight: bold'>Total  Value</td><td><label id='otot'>" + opprice + "</label></td><td><label id='oftot' name='oftot'>" + opfprice + "</label></td>><td><label id='ptot' name='ptot'>" + purprice + "</label></td><td><label id='pftot' name='pftot'>" + purfprice + "</label></td><td><label  id='stot' name='stot'>" + salprice + "</label></td><td><label id='sftot' name='sftot'>" + salfprice + "</label></td><td><label id='retot' name='retot'>" + reprice + "</label></td><td><label id='reftot'>" + refprice + "</label></td><td><label id='cltot' name='cltot'>" + clprice + "</label></td><td><label id='clftot'>" + clfprice + "</label></td></tr>");
                        }
                    });
                }
                $(document).on('keyup', '.oqty', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.pqty', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.sqty', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.cqty', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.rqty', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.ofree', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.pfree', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.sfree', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.cfree', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                $(document).on('keyup', '.rfree', function () {
                    var elmnt = $(this)
                    keyuptxt(elmnt);
                    totalcal();
                });
                function keyuptxt($elemmnt) {
                    var row = $($elemmnt).closest('tr');
                    var openq = isNaN(parseFloat(row.find('.oqty').val())) ? 0 : parseFloat(row.find('.oqty').val());
                    var purqty = isNaN(parseFloat(row.find('.pqty').val())) ? 0 : parseFloat(row.find('.pqty').val());
                    var salqty = isNaN(parseFloat(row.find('.sqty').val())) ? 0 : parseFloat(row.find('.sqty').val());
                    var retqty = isNaN(parseFloat(row.find('.rqty').val())) ? 0 : parseFloat(row.find('.rqty').val());
                    var openf = isNaN(parseFloat(row.find('.ofree').val())) ? 0 : parseFloat(row.find('.ofree').val());
                    var purf = isNaN(parseFloat(row.find('.pfree').val())) ? 0 : parseFloat(row.find('.pfree').val());
                    var salf = isNaN(parseFloat(row.find('.sfree').val())) ? 0 : parseFloat(row.find('.sfree').val());
                    var retf = isNaN(parseFloat(row.find('.rfree').val())) ? 0 : parseFloat(row.find('.rfree').val());
                    if ((parseFloat(openq) + parseFloat(purqty)) < parseFloat(salqty)) {
                        alert("Enter valid sales Qty");
                    }
                    else {
                        var newcq = ((parseFloat(openq) + parseFloat(purqty)) - parseFloat(salqty)) - parseFloat(retqty);
                    }
                    if ((parseFloat(openf) + parseFloat(purf)) < parseFloat(salf)) {
                        alert("Enter valid sales free");
                    }
                    else {
                        var newcf = ((parseFloat(openf) + parseFloat(purf)) - parseFloat(salf)) - parseFloat(retf);
                    }

                    var cq = row.find('.cqty').val(newcq);
                    var cf = row.find('.cfree').val(newcf);
                }
                function totalcal() {
                    var opprice = 0;
                    var purprice = 0;
                    var salprice = 0;
                    var clprice = 0;
                    var rprice = 0;
                    var opfprice = 0;
                    var purfprice = 0;
                    var salfprice = 0;
                    var clfprice = 0;
                    var rfprice = 0;
                    $('#grid tr').each(function () {

                        var row = $(this);

                        var price = isNaN(parseFloat(row.find('.prate').val())) ? 0 : parseFloat(row.find('.prate').val());
                        var openq = isNaN(parseFloat(row.find('.oqty').val())) ? 0 : parseFloat(row.find('.oqty').val());
                        var purqty = isNaN(parseFloat(row.find('.pqty').val())) ? 0 : parseFloat(row.find('.pqty').val());
                        var salqty = isNaN(parseFloat(row.find('.sqty').val())) ? 0 : parseFloat(row.find('.sqty').val());
                        var retqty = isNaN(parseFloat(row.find('.rqty').val())) ? 0 : parseFloat(row.find('.rqty').val());
                        var clsqty = isNaN(parseFloat(row.find('.cqty').val())) ? 0 : parseFloat(row.find('.cqty').val());
                        var openf = isNaN(parseFloat(row.find('.ofree').val())) ? 0 : parseFloat(row.find('.ofree').val());
                        var purf = isNaN(parseFloat(row.find('.pfree').val())) ? 0 : parseFloat(row.find('.pfree').val());
                        var salf = isNaN(parseFloat(row.find('.sfree').val())) ? 0 : parseFloat(row.find('.sfree').val());
                        var clsf = isNaN(parseFloat(row.find('.cfree').val())) ? 0 : parseFloat(row.find('.cfree').val());
                        var retf = isNaN(parseFloat(row.find('.rfree').val())) ? 0 : parseFloat(row.find('.rfree').val());
                        opprice += (openq * price);
                        purprice += (purqty * price);
                        salprice += (salqty * price);
                        clprice += (clsqty * price);
                        rprice += (retqty * price);
                        opfprice += (openf * price);
                        purfprice += (purf * price);
                        salfprice += (salf * price);
                        clfprice += (clsf * price);
                        rfprice += (retf * price);
                    });
                    $('#otot').text(opprice);
                    $('#oftot').text(opfprice);
                    $('#ptot').text(purprice);
                    $('#pftot').text(purfprice);
                    $('#stot').text(salprice);
                    $('#sftot').text(salfprice);
                    $('#cltot').text(clprice);
                    $('#clftot').text(clfprice);
                    $('#retot').text(rprice);
                    $('#reftot').text(rfprice);
                };

                $(document).on('click', '.btnsave', function () {
                    var fieldforce = $("#ddlcomname").val();
                    var disname = $("#distname").val();
                    var month = $("#fltpmnth").val();
                    var year = $("#fltpyr").val();
                    $('#grid').find('tbody').find('tr').each(function () {
                        arr.push({
                            pCodes: $(this).find('input[name="pCode"]').val(),
                            oqtys: $(this).find('input[name="oqty"]').val(),
                            ofrees: $(this).find('input[name="ofree"]').val(),
                            pqtys: $(this).find('input[name="pqty"]').val(),
                            pfrees: $(this).find('input[name="pfree"]').val(),
                            sqtys: $(this).find('input[name="sqty"]').val(),
                            sfrees: $(this).find('input[name="sfree"]').val(),
                            cqtys: $(this).find('input[name="cqty"]').val(),
                            cfrees: $(this).find('input[name="cfree"]').val(),
                            rqtys: $(this).find('input[name="rqty"]').val(),
                            rfrees: $(this).find('input[name="rfree"]').val(),
                        });
                    });
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "newtp.aspx/Savestock",
                        data: "{'Data':'" + JSON.stringify(arr) + "','fieldforcecode':'" + fieldforce + "','distname':'" + disname + "','emonth':'" + month + "','eyear':'" + year + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == 'Success') {

                                alert('Product Saved Successfully');

                            }
                            else {

                                alert(data.d);
                            }

                        },
                        error: function (exception) {
                            console.log(exception);
                        }
                    });
                });
            </script>
        </body>
    </html>      
</asp:Content>
