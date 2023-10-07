<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="New_Distributor_creation.aspx.cs" Inherits="Stockist_New_Distributor_creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title></title>
        <link href="../../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-daterangepicker/3.0.5/daterangepicker.css" />
         <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.1/popper.min.js"></script>
          <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.24.0/moment.min.js"></script>
         <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-daterangepicker/3.0.5/daterangepicker.min.js"></script>--%>
        <!-- CSS files -->
        <%--k<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.14.0/css/bootstrap-select.min.css" />--%>

        <!-- JS files -->
        <%--<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>--%>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/2.9.3/umd/popper.min.js" ></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.14.0/js/bootstrap-select.min.js"></script>

        <style type="text/css">
            table {
                border-collapse: separate;
                border-spacing: 9px;
                width: 100%;
            }

            .is-invalid {
                outline: none;
                box-shadow: 0 0 0 3px rgba(255, 0, 0, 0.4);
            }

            .scrldiv, .scrldiv2 {
                width: 545px;
                height: 116px;
            }

                .scrldiv input {
                    margin-top: 10px;
                }

                .scrldiv2 input {
                    margin-top: 10px;
                }

            label {
                font-size: 13px;
                font-family: 'Open Sans','arial';
                font-weight: 700;
                margin: 0px;
            }

            .panel-default {
                border-color: #ddd !important;
                border: 1px solid transparent;
            }

            input {
                width: 100%;
                border: 1px solid #d8d8d8 !important;
                padding: 6px 6px 7px !important;
                border-radius: 3px !important;
                line-height: inherit !important;
                background: #fff;
                box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
                transition: all 0.3s ease;
                /*border: 1px solid #00809D;*/
            }

                input:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            select {
                width: 100%;
                border: 1px solid #D5D5D5 !important;
                padding: 6px 6px 7px !important;
            }

                select:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }

            .bootstrap-select:not([class*=col-]):not([class*=form-control]):not(.input-group-btn) {
                width: 294px !important;
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
                    -webkit-transform: scaleY(0.4)
                }

                20% {
                    -webkit-transform: scaleY(1.0)
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
        <div class="row">
            <div class="col-lg-12 sub-header" style="position: relative;">
                Distributor Creation
                <button style="float: right;" type="button" id="btnsubmit" class="btn btn-primary">Submit</button>
            </div>
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <form style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px; margin-top: 5px;">
                <div class="container">
                    <div class="col-md-6">
                        <table id="upltbl" style="border-collapse: collapse; margin-top: 9px;">
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>ERP Code</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="Txt_ERP_Code" type="text" autocomplete="off" required />
                                    <input class="col-xs-9" id="Txt_stock_Code" type="text" autocomplete="off" required style="display: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Distributor Name</label>
                                    <span style="color: red;">*</span>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="txtStockist_Name" type="text" autocomplete="off" required />
                                    <input class="col-xs-9" id="Txt_stockCode" type="text" autocomplete="off" value="0" required style="display: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Contact Person</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="txtStockist_ContactPerson" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Email</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="txtemail" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label>UserName</label>
                                    <span style="color: red;">*</span>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="input-group input-group-sm mb-3" style="display: flex; width: 153PX;">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text" style="padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px; width: 69PX; height: 30px;" id="fixUsrn">SWDF</div>
                                                </div>
                                                <input type="text" class="form-control" id="sfusrname" style="border-radius: 0px 4px 4px 0px !important;" aria-label="Small" aria-describedby="inputGroup-sizing-sm">
                                            </div>
                                        </div>
                                        <div class="col-sm-6" style="padding-left: 30px" id="user">
                                            <input type="checkbox" id="usrauto" name="userauto" value="Yes" style="width: auto; margin: 8px 0px 8px 8px;" />
                                            <span style="font-size: 13px;">Auto-generate</span>
                                        </div>
                                    </div>


                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Territory</label>
                                    <span style="color: red;">*</span>
                                </td>

                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <select id="ddlTerritory" style="width: 62% !important;">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>District Name</label>
                                    <span style="color: red;">*</span>
                                </td>

                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <select id="ddlDistrict" style="width: 62% !important;">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Taluk</label></td>

                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <select id="ddlTaluk" style="width: 62% !important;">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Type</label>
                                    <span style="color: red;">*</span>
                                </td>

                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <select id="ddlType" style="width: 62% !important;">
                                        <option value="-1">Select Type</option>
                                        <option value="Warehouse">Warehouse</option>
                                        <option value="Stockist">Stockist</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <label>Rate</label>
                                    <span style="color: red;">*</span>
                                </td>

                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <select id="ddlRate" style="width: 62% !important;">
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-6">
                        <table>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Address</label>
                                    <span style="color: red;">*</span>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfAddress" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Designation</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfDesignation" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Mobile</label>
                                    <span style="color: red;">*</span>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfMobile" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Norm Value</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfNorm" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Password</label>
                                    <span style="color: red;">*</span>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfpassword" type="password" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Field Officer</label>
                                    <span style="color: red;">*</span>
                                </td>
                                <td>
                                    <select id="ddlField" style="width: 62% !important;" multiple>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Head Quarters</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfHead" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>Category</label>
                                </td>

                                <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                    <select id="ddlCategory" style="width: 62% !important;">
                                    </select>

                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px">
                                    <label>GSTN No</label>
                                </td>
                                <td>
                                    <input class="col-xs-9" id="sfGSTN" type="text" autocomplete="off" required />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="50px">
                                    <label>Stock</label>
                                </td>
                                <td align="left">
                                    <input type="checkbox" name="stock" value="YES" style="width: 15px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-9">
                        <h3 align="center" style="font-weight: normal; font-family: 'Open Sans','arial';">Division</h3>
                    </div>
                    <div class="col-md-11" id="division_div" style="margin: 10px; padding: 10px; border: 1px solid lightgray; border-radius: 10px;">
                    </div>
                </div>
            </form>
    </body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.spinnner_div').show().queue(function () {
                getdistnum();
                loaddiv();
                loadTerritory();
                loadDist();
                loadTaluk();
                loadRate();
                loadCatagory();
                getDivisname(0);

            })
            $("#sfAddress").keypress(function (event) {
                if (event.which === 39) { // 39 is the code for a single quote
                    event.preventDefault();
                }
            });
            $('.spinnner_div').show();
            //getdistnum();
            //loaddiv();
            //loadTerritory();
            //loadDist();
            //loadTaluk();
            //loadRate();
            //loadCatagory();
            //getDivisname(0);
            var sfcode = '<%=stockist_code%>';
            if (sfcode != '')
                loaddistval(sfcode);
            $("#ddlTerritory").change(function () {
                var ter = $('#ddlTerritory').val();
                loadfoname(ter);
            })
            $('#ddlType').selectpicker({
                liveSearch: true
            });
            $('#usrauto').on('change', function () {
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#sfusrname').prop('readonly', true)
                    usratuogenerate = true; getDivisname(1);
                }
                else {
                    $('#sfusrname').prop('readonly', false)
                    usratuogenerate = false;
                }
            })
            $('.spinnner_div').css("display", "none");
        });
        var cc = document.querySelector('#sfNorm');

        cc.addEventListener('keydown', function (e) {

            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 105) || charCode == 8 || charCode == 190 || charCode == 110 || charCode == 46) {
                return true;
            }
            e.preventDefault();
            return false;
        }, true);

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
        function getdistnum() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getdistnum",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    SFDivnum = JSON.parse(data.d);
                    $("#Txt_stock_Code").val(SFDivnum[0].Num);
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
        }
        function loaddiv() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getdivision",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    SFDivisname = JSON.parse(data.d);
                    var str = '';
                    for (var i = 0; i < SFDivisname.length; i++) {
                        str += '<div class="col-xs-3"><input type="checkbox" name="sub_div" id="' + SFDivisname[i].subdivision_code + '"style="width: 15px;"/><label>' + SFDivisname[i].subdivision_name + '</label></div>'
                    }
                    $("#division_div").html(str);
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
        }
        function loadTerritory() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getTerritory",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlTerritory").html('');
                        var tert = $("#ddlTerritory");
                        tert.empty().append('<option selected="selected" value="0">Select Territory </option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Territory_code + '">' + SFTer[i].Territory_name + '</option>'))
                        }
                    }
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
            $('#ddlTerritory').selectpicker({
                liveSearch: true
            });
        }
        function loadfoname(ter) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getFOName",
                data: "{'terr':'" + ter + "','Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlField").html('');
                        var tert = $("#ddlField");
                        tert.empty().append('<option  value="0">Select Field Officer</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Sf_Code + '">' + SFTer[i].Sf_Name + '</option>'))
                        }
                    }
                },
                error: function (rs) {
                    alert(rs);
                }
            });
            $('#ddlField').selectpicker({
                liveSearch: true
            });
            $('#ddlField').selectpicker('refresh');
        }
        function loadDist() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getdist",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlDistrict").html('');
                        var tert = $("#ddlDistrict");
                        tert.empty().append('<option selected="selected" value="0">Select District</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Dist_code + '">' + SFTer[i].Dist_name + '</option>'))
                        }
                    }
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
            $('#ddlDistrict').selectpicker({
                liveSearch: true
            });
        }
        function loadTaluk() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getTaluk",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlTaluk").html('');
                        var tert = $("#ddlTaluk");
                        tert.empty().append('<option selected="selected" value="0">Select Taluk</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Town_code + '">' + SFTer[i].Town_name + '</option>'))
                        }
                    }
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
            $('#ddlTaluk').selectpicker({
                liveSearch: true
            });
        }
        function loadRate() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getRatecard",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlRate").html('');
                        var tert = $("#ddlRate");
                        tert.empty().append('<option selected="selected" value="0">Select Rate</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Price_list_Sl_No + '">' + SFTer[i].Price_list_Name + '</option>'))
                        }
                    }
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
            $('#ddlRate').selectpicker({
                liveSearch: true
            });
        }
        function loadCatagory() {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getCatagory",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    var SFTer = JSON.parse(data.d) || [];
                    if (SFTer.length > 0) {
                        $("#ddlCategory").html('');
                        var tert = $("#ddlCategory");
                        tert.empty().append('<option selected="selected" value="0">Select Category</option>');
                        for (var i = 0; i < SFTer.length; i++) {
                            tert.append($('<option value="' + SFTer[i].Doc_Cat_Code + '">' + SFTer[i].Doc_Cat_Name + '</option>'))
                        }
                    }
                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
            $('#ddlCategory').selectpicker({
                liveSearch: true
            });
        }
        function getDivisname(fl) {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getusername",
                data: "{'Div_Code':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    SFDivisname = JSON.parse(data.d);
                    $('#fixUsrn').text(SFDivisname[0].Division_SName + "-");
                    if (fl == 1) $('#sfusrname').val(SFDivisname[0].uname);
                }
            });
            $('.spinnner_div').css("display", "none");
        }
        $('#btnsubmit').on('click', function () {
            $('.spinnner_div').css("display", "block");
            var erp_code = $('#Txt_ERP_Code').val().trim();
            var userentry = $('#Txt_stock_Code').val().trim();
            var stockist_code = getParameterByName('stockist_code', '')
            var ContactPerson = $('#txtStockist_ContactPerson').val().trim();
            var Stockist_Name = $('#txtStockist_Name').val().trim();
            if (Stockist_Name == "") {
                alert('Enter the Distributor Name');
                $('#txtStockist_Name').focus();
                return false;
            }
            var sfusrname = $('#sfusrname').val();
            if (stockist_code != "" || stockist_code != null) {
                if (sfusrname == '') {
                    sfusrname = "";
                }
                else {
                    sfusrname = $('#fixUsrn').text() + sfusrname;
                }


            }
            var sfterr = $('#ddlTerritory').val().trim();
            var sfterrname = $('#ddlTerritory :selected').text();
            if (sfterr == '' || sfterr == "0") {
                alert('select Territory');
                $('#ddlTerritory').focus();
                return false;
            }
            var sfDistrict = $('#ddlDistrict').val().trim();
            var sfDistrictname = $('#ddlDistrict :selected').text();
            var stockcode = $('#Txt_stockCode').val();
            if (sfDistrict == '' || sfDistrict == "0") {
                alert('select District');
                $('#ddlDistrict').focus();
                return false;
            }
            //var sfddlTaluk = $('#ddlTaluk').val().trim();
            var sfddlTaluk = $("#ddlTaluk").val() == undefined ? '' : $("#ddlTaluk").val().trim();
            var sfddlTalukname = $("#ddlTaluk :selected").text();
            var sfType = $('#ddlType').val().trim();
            if (sfType == '' || sfType == "-1") {
                alert('select Type ');
                $('#ddlType').focus();
                return false;
            }
            var sfRate = $('#ddlRate').val() == undefined ? '' : $("#ddlRate").val().trim();
            if (sfRate == '' || sfRate == "0") {
                // alert('select Rate');
                // $('#ddlRate').focus();
                // return false;
            }
            var sfAddress = $('#sfAddress').val().trim();
            if (sfAddress == '' || sfAddress == "0") {
                alert('Enter Address');
                $('#sfAddress').focus();
                return false;
            }
            var sfDesignation = $('#sfDesignation').val().trim();
            var sfMobile = $('#sfMobile').val().trim();
            var sfNorm = $('#sfNorm').val() == undefined ? 0 : $('#sfNorm').val() == '' ? 0 : $('#sfNorm').val();
            var sfemail = $('#txtemail').val().trim();
            var sfpwd = $('#sfpassword').val();
            if (sfpwd == '') {
                //alert('Enter the Password');
                //$('#sfpassword').focus();
                //return false;
            }

            var ddlFieldcode = $('#ddlField').val();
            var ddlFieldname = '';
            var ddlField = '';
            for (var y = 0; y < ddlFieldcode.length; y++) {
                ddlField += ddlFieldcode[y] + ',';
                ddlFieldname += $("#ddlField option[value='" + ddlFieldcode[y] + "']").text() + ',';
            }
            if (ddlField == '') {
                alert("select ddlField");
                $('#ddlField').focus();
                return false;
            }
            var sfHead = $('#sfHead').val().trim();
            var sfCategory = $('#ddlCategory').val() == undefined ? '' : $("#ddlCategory").val().trim();
            if (sfCategory == '') {
                //alert("select the Category");
                //$('#ddlCategory').focus();
                // return false;
            }
            var sfGSTN = $('#sfGSTN').val();
            var stock = $('input[name="stock"]:checked').val();
            var sub_div = '';
            $('input[name="sub_div"]:checked').each(function () {
                sub_div += $(this).attr('id') + ',';
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/submit_distributor",
                data: "{'Div_Code':'<%=Session["div_code"]%>','Stock_code':'" + stockcode + "','userentry':'" + userentry + "','stockist_code':'" + stockist_code + "','erp_code':'" + erp_code + "','ContactPerson':'" + ContactPerson + "','Stockist_Name':'" + Stockist_Name + "','sfusrname':'" + sfusrname + "'\
                        ,'sfterr':'" + sfterr + "','sfterrname':'" + sfterrname + "','sfDistrict':'" + sfDistrict + "','sfDistrictname':'" + sfDistrictname + "','sfddlTaluk':'" + sfddlTaluk + "','sfddlTalukname':'" + sfddlTalukname + "','sfType':'" + sfType + "'\
                        ,'sfRate':'" + sfRate + "','sfAddress':'" + sfAddress + "','sfemail':'" + sfemail + "','sfDesignation':'" + sfDesignation + "','sfMobile':'" + sfMobile + "','sfNorm':'" + sfNorm + "','sfpwd':'" + sfpwd + "','ddlField':'" + ddlField + "'\
                         ,'ddlFieldname':'" + ddlFieldname + "' ,'sfHead':'" + sfHead + "' ,'sfCategory':'" + sfCategory + "' ,'sfGSTN':'" + sfGSTN + "' ,'stock':'" + stock + "' ,'sub_div':'" + sub_div + "'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    var divcode = '<%=Session["div_code"]%>';
                    if (data.d.length > 0) {
                        if (data.d == "Created_Successfully") {
                            alert('Created_Successfully');
                            location.reload();
                        }
                        else if (data.d == "Updated_Successfully") {
                            if (divcode == "32" || divcode == "43" || divcode == "48") {
                                alert("Updated Successfully");
                                window.location = 'Distributor_Creation.aspx';
                            }
                            else {
                                alert("Updated Successfully");
                                window.location = 'New_Distributor_Master.aspx';
                            }
                        }
                        else {
                            alert(data.d);
                            $('.spinnner_div').css("display", "none");
                            return false;
                        }
                    }


                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");
                }
            });
            $('.spinnner_div').css("display", "none");
        });
        function loaddistval(sfcode) {
            $('.spinnner_div').css("display", "block");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_Distributor_creation.aspx/getdistdetail",
                data: "{'Div_Code':'<%=Session["div_code"]%>','stockist_code':'" + sfcode + "'}",
                dataType: "json",
                success: function (data) {
                    $('.spinnner_div').css("display", "block");
                    SfDetails = JSON.parse(data.d);
                    loadfoname(SfDetails[0].Territory_Code)
                    Itype = '1';
                    $('#txtemail').val(SfDetails[0].Stockist_Email);
                    if (SfDetails[0].Username.indexOf("-") > -1) {
                        var sfausrname = SfDetails[0].Username.split("-");
                        $('#fixUsrn').text(sfausrname[0] + "-");
                        $('#sfusrname').val(sfausrname[1]);
                        $("#user").hide();
                    }
                    else if (SfDetails[0].Username == "") {
                        $('#sfusrname').val(SfDetails[0].Username)
                    }
                    else {
                        $('#fixUsrn').text("");
                        $('#sfusrname').val(SfDetails[0].Username);
                        $("#user").hide();
                    }
                    $('#sfpassword').val(SfDetails[0].Password);
                    $('#sfNorm').val(SfDetails[0].Norm_Val);
                    $('#sfMobile').val(SfDetails[0].Stockist_Mobile);
                    $('#sfDesignation').val(SfDetails[0].Stockist_Designation);
                    $('#sfAddress').val(SfDetails[0].Stockist_Address);
                    $('#Txt_stock_Code').val(SfDetails[0].ERP_Code);
                    $('#Txt_ERP_Code').val(SfDetails[0].ERP_Code);
                    $('#txtStockist_Name').val(SfDetails[0].Stockist_Name);
                    $('#sfHead').val(SfDetails[0].Head_Quaters);
                    $('#sfGSTN').val(SfDetails[0].gstn);
                    $('#txtStockist_ContactPerson').val(SfDetails[0].Stockist_ContactPerson);
                    $('#ddlTerritory').selectpicker('val', SfDetails[0].Territory_Code);
                    $('#ddlRate').selectpicker('val', SfDetails[0].Price_list_Name);
                    $('#ddlDistrict').selectpicker('val', SfDetails[0].Dist_Code);
                    $('#ddlField').selectpicker('val', SfDetails[0].Field_Code);
                    $('#ddlCategory').selectpicker('val', SfDetails[0].Dis_Cat_Code);
                    $('#ddlType').selectpicker('val', SfDetails[0].Type);
                    var sub = (SfDetails[0].subdivision_code).split(',');
                    for (var k = 0; k < sub.length; k++) {
                        $('input[id="' + sub[k] + '"]').prop('checked', true);
                    }
                    var splitsdiv = SfDetails[0].Field_Code.split(',') || [];
                    if (splitsdiv.length > 0) {
                        for (var i = 0; i < splitsdiv.length; i++)
                            $('#ddlField option[value=' + splitsdiv[i] + ']').attr('selected', true);
                    }
                    $('#ddlField').selectpicker('refresh');
                    $("#user").hide();


                },
                error: function (rs) {
                    alert(rs);
                    $('.spinnner_div').css("display", "none");

                }
            });
            $('.spinnner_div').css("display", "none");
        }
    </script>
    </html>
</asp:Content>

