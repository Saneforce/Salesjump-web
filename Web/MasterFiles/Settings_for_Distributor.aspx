<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Settings_for_Distributor.aspx.cs" Inherits="MasterFiles_Settings_for_Distributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Settings for Employees</title>
        <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <link href="../css/style1.css" rel="stylesheet" type="text/css" />
        <script src="../JsFiles/ScrollableGrid.js" type="text/javascript"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link href="CSS/GridviewScroll.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../js/plugins/timepicker/bootstrap-clockpicker.min.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>

        <script src="Scripts/gridviewScroll.min.js" type="text/javascript"></script>

        <link href="CSS/GridviewScroll.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .results tr[visible='false'], .no-result {
                display: none;
            }

            .results tr[visible='true'] {
                display: table-row;
            }

            .counter {
                padding: 8px;
                color: #ccc;
            }


            .table-fixed thead {
                width: 97%;
            }

            .table-fixed tbody {
                height: 230px;
                overflow-y: auto;
                width: 100%;
            }

            .table-fixed thead, .table-fixed tbody, .table-fixed tr, .table-fixed td, .table-fixed th {
                display: block;
            }

                .table-fixed tbody td, .table-fixed thead > tr > th {
                    float: left;
                    border-bottom-width: 0;
                }

            .table thead {
                width: 100%;
            }

            .table tbody {
                height: 562px;
                overflow-y: auto;
                width: 100%;
            }

            .table thead tr {
                width: 99%;
            }

            .table tr {
                width: 100%;
            }

            .table thead, .table tbody, .table tr, .table td, .table th {
                display: inline-block;
            }

            .table thead {
                background: #19a4c6;
                color: #fff;
            }

                .table tbody td, .table thead > tr > th {
                    float: left;
                    border-bottom-width: 0;
                }

            .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
                text-align: center;
                line-height: 20px;
            }

            .odd {
                background: #ffffff;
                color: #000;
            }

            .even {
                background: #efefef;
                color: #000;
            }

            .points_table_scrollbar {
                height: 612px;
                overflow-y: scroll;
            }

                .points_table_scrollbar::-webkit-scrollbar-track {
                    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.9);
                    border-radius: 10px;
                    background-color: #444444;
                }

                .points_table_scrollbar::-webkit-scrollbar {
                    width: 1%;
                    min-width: 5px;
                    background-color: #F5F5F5;
                }

                .points_table_scrollbar::-webkit-scrollbar-thumb {
                    border-radius: 10px;
                    background-color: #D62929;
                    background-image: -webkit-linear-gradient(90deg, transparent, rgba(0, 0, 0, 0.4) 50%, transparent, transparent);
                }




            .filterable {
                margin-top: 15px;
            }

                .filterable .panel-heading .pull-right {
                    margin-top: -20px;
                }

                .filterable .filters input[disabled] {
                    background-color: transparent;
                    border: none;
                    cursor: auto;
                    box-shadow: none;
                    padding: 0;
                    height: auto;
                }

                    .filterable .filters input[disabled]::-webkit-input-placeholder {
                        color: #333;
                    }

                    .filterable .filters input[disabled]::-moz-placeholder {
                        color: #333;
                    }

                    .filterable .filters input[disabled]:-ms-input-placeholder {
                        color: #333;
                    }

            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: gray;
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

            select {
                padding: 0 4px;
            }

            #table1 tr td :nth-child(4), #table1 tr th :nth-child(4) {
                display: none;
            }

            .pad {
                display: block;
                padding: 5px;
                height: 32px;
            }

            .new {
                padding: 50px;
            }

            body {
                font-family: sans-serif;
            }

            .container {
                margin-top: 50px;
                margin-left: 20px;
                margin-right: 20px;
            }

            .checkbox {
                width: 100%;
                margin: 15px auto;
                position: relative;
                display: block;
            }

                .checkbox input[type="checkbox"] {
                    width: auto;
                    opacity: 0.00000001;
                    position: absolute;
                    left: 0;
                    margin-left: -20px;
                }

                .checkbox label {
                    position: relative;
                }

                    .checkbox label:before {
                        content: '';
                        position: absolute;
                        left: 0;
                        top: 0;
                        margin: 4px;
                        width: 22px;
                        height: 22px;
                        transition: transform 0.28s ease;
                        border-radius: 3px;
                        border: 2px solid #7bbe72;
                    }

                    .checkbox label:after {
                        content: '';
                        display: block;
                        width: 10px;
                        height: 5px;
                        border-bottom: 2px solid #7bbe72;
                        border-left: 2px solid #7bbe72;
                        -webkit-transform: rotate(-45deg) scale(0);
                        transform: rotate(-45deg) scale(0);
                        transition: transform ease 0.25s;
                        will-change: transform;
                        position: absolute;
                        top: 12px;
                        left: 10px;
                    }

                .checkbox input[type="checkbox"]:checked ~ label::before {
                    color: #7bbe72;
                }

                .checkbox input[type="checkbox"]:checked ~ label::after {
                    -webkit-transform: rotate(-45deg) scale(1);
                    transform: rotate(-45deg) scale(1);
                }

                .checkbox label {
                    min-height: 34px;
                    display: block;
                    padding-left: 40px;
                    margin-bottom: 0;
                    font-weight: normal;
                    cursor: pointer;
                    vertical-align: sub;
                }

                    .checkbox label span {
                        position: absolute;
                        top: 50%;
                        -webkit-transform: translateY(-50%);
                        transform: translateY(-50%);
                    }

                .checkbox input[type="checkbox"]:focus + label::before {
                    outline: 0;
                }

            element.style {
            }

            .form-control {
                box-shadow: none;
                min-width: 100% !important;
            }

            .form-control {
                box-shadow: none;
                min-width: 100% !important;
            }

            .dropdown-menu.dropdown-custom {
                min-width: 273px;
                max-width: 273px;
            }

            a {
                color: #19a4c6;
                font-size: 15px;
                font-weight: 549;
            }

            #table1 tr:hover {
                background-color: #6ecee536;
                /* transform: skew(10deg); */
            }

            .nav > li > a:hover, .nav > li > a:focus {
                text-decoration: none;
                background-color: rgb(255 255 255 / 15%);
                font-weight: 600;
            }

            .dropdown-menu-right > li > a:hover {
                /* background-color: #144ad1; */
                transform: skew(8deg);
                padding: 5px;
                margin: 5px;
                color: #f9f9f9;
            }

            .panel-primary {
                border:1px solid #19a4c6;
            }   
        </style>
    </head>
    <script type="text/javascript">
        $(document).ready(function () {
            var div_code = '<%= Session["div_code"] %>';
            var sf_code = '<%= Session["sf_code"] %>';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Settings_for_Distributor.aspx/getdata",
                data: "{'div_code':'" + div_code + "','sf_code':'" + sf_code + "'}",
                dataType: "json",
                success: function (data) {

                    for (var i = 0; i < data.d.length; i++) {
                        var dis = "";
                        if (data.d[i].Pricetype == 2) {
                            dis = '<ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                                + '<span><span class="aState" data-val="2">Rate Card</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                                '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;"><li><a href="#" v="0">State Wise</a><a href="#" v="1">Retailer Wise</a><a href="#" v="2">Rate Card</a></li></ul></li></ul>';

                        }
                        else if (data.d[i].Pricetype == 1) {
                            dis = '<ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                                + '<span><span class="aState" data-val="1">Retailer Wise</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                                '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;"><li><a href="#" v="0">State Wise</a><a href="#" v="1">Retailer Wise</a><a href="#" v="2">Rate Card</a></li></ul></li></ul>';
                        }
                        else if (data.d[i].Pricetype ==0) {
                            dis = '<ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                                + '<span><span class="aState" data-val="0">State Wise</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                                '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;"><li><a href="#" v="0">State Wise</a><a href="#" v="1">Retailer Wise</a><a href="#" v="2">Rate Card</a></li></ul></li></ul>';
                        }
                        else {
                            dis = '<ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                                + '<span><span class="aState" data-val="-1">select type</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                                '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;"><li><a href="#" v="0">State Wise</a><a href="#" v="1">Retailer Wise</a><a href="#" v="2">Rate Card</a></li></ul></li></ul>';
                        }
                        var btn = "";
                        btn = '<input type="button" class="btn btn-primary" value="Save" onclick="save(' + data.d[i].Distcode + ')"';
                        $('#table1 tbody').append("<tr style='display: flex;transition:0.8s' id='" + data.d[i].Distcode + "'><td class='col-xs-6' style='text-align: -webkit-center' >" + data.d[i].Distname + "</td>" +
                            "<td class='col-xs-3 sfedit' style='text-align: -webkit-center' id='" + data.d[i].Distcode + "' >" + dis + "</td><td class='col-xs-3'></td> </tr>");

                    }


                },
                error: function (sfcode) {
                    alert(JSON.stringify(result));
                }
            });

            $('.pricetype').change(function () {
                var id = $(this).attr('id');
                var value = $(this).find("option:selected").text()
                alert(id + value);
            })
            $('#txtser').keyup(function (e) {
                var $input = $(this);
                inputContent = $input.val().toLowerCase();
                $panel = $input.parents('.filterable'),
                    column = $panel.find('.filters th').index($input.parents('th')),
                    $table = $panel.find('.table'),
                    $rows = $table.find('tbody tr');
                var $filteredRows = $rows.filter(function () {
                    var value = $(this).find('td').eq(0).text().toLowerCase();
                    return value.indexOf(inputContent) == -1;
                });
                $table.find('tbody .no-result').remove();
                $rows.show();
                $filteredRows.hide();
                if ($filteredRows.length === $rows.length) {
                    $table.find('tbody').prepend($('<tr class="no-result text-center"><td>No result found</td></tr>'));
                }
                checkhead();
            });
            $(".ddlStatus>li>a").on("click", function () {
                cStus = $(this).closest("td").find(".aState");
                let hqid = $(this).closest("tr").find(".sfedit").attr("id");
                stus = $(this).attr("v");
                cStusNm = $(this).text();
                if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                    $(this).closest("td").find(".aState").text(cStusNm);
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Settings_for_Distributor.aspx/updatedata",
                        data: "{'distcode':'" + hqid + "','pricetype':'" + stus +"'}",
                        dataType: "json",
                        success: function (data) {
                           alert(data.d);

                        },
                        error: function (result) {
                            alert(data.d);
                        }
                    });
                }
            });

        });
        function save(div_code) {
            alert('Updated Successfully');
            $("#" + div_code).css("transform", "translateX(-99%)");
            setTimeout(function () {
                $("#" + div_code).remove().delay(500);
            }, 1000)
        }


        function funfilter(lnkText, iCounter) {
            $(".table tbody tr").each(function () {
                var sRSF = $(this).find(".rsf").val().toLowerCase().toString()
                var sSF = $(this).find(".sfc").val().toLowerCase().toString()

                if (lnkText != ',0,') {

                    console.log("lnk : " + lnkText + " sf : " + sSF + " rsf : " + sRSF);
                    if (lnkText.toLowerCase().indexOf(',' + sSF + ',') > -1 || lnkText.toLowerCase().indexOf(',' + sRSF + ',') > -1 || $("#ddlFieldForce").val().toLowerCase() == sRSF.toLowerCase() || $("#ddlFieldForce").val().toLowerCase() == sSF.toLowerCase()) {
                        $(this).css('display', 'inline-block');
                        lnkText += sSF + ',';
                        iCounter++;
                    }
                    else {
                        $(this).css('display', 'none');
                    }
                }
                else {
                    $(this).css('display', 'inline-block');
                    iCounter++;
                }

            });

        }
        function checkhead() {
            if ($("#table1 tbody tr td:nth-child(2) input:checkbox").length == $("#table1 tbody tr td:nth-child(2) input:checkbox:checked").length) {
                $("#chgneed").attr("checked", "checked");
            } else {
                $("#chgneed").removeAttr("checked");
            }


            if ($("#table1 tbody tr td:nth-child(3) input:checkbox").length == $("#table1 tbody tr td:nth-child(3) input:checkbox:checked").length) {
                $("#chtrack").attr("checked", "checked");
            } else {
                $("#chtrack").removeAttr("checked");
            }

            if ($("#table1 tbody tr td:nth-child(4) input:checkbox").length == $("#table1 tbody tr td:nth-child(4) input:checkbox:checked").length) {
                $("#chfencing").attr("checked", "checked");
            } else {
                $("#chfencing").removeAttr("checked");
            }

            if ($("#table1 tbody tr td:nth-child(5) input:checkbox").length == $("#table1 tbody tr td:nth-child(5) input:checkbox:checked").length) {
                $("#chedsum").attr("checked", "checked");
            } else {
                $("#chedsum").removeAttr("checked");
            }

            if ($("#table1 tbody tr td:nth-child(6) input:checkbox").length == $("#table1 tbody tr td:nth-child(6) input:checkbox:checked").length) {
                $("#chevsal").attr("checked", "checked");
            } else {
                $("#chevsal").removeAttr("checked");
            }
            if ($("#table1 tbody tr td:nth-child(7) input:checkbox").length == $("#table1 tbody tr td:nth-child(7) input:checkbox:checked").length) {
                $("#chechenen").attr("checked", "checked");
            } else {
                $("#chechenen").removeAttr("checked");
            }
        }
    </script>
    <div class="container">
        <div class="row" style="width: 95%">
            <div class="panel panel-primary filterable">
                <div style="display: flex; padding: 10px;">
                    <div class="col-md-1" style="padding: 7px;">
                        Search By :
                    </div>
                    <div class="col-md-3">
                        <input type="text" id="txtser" class="form-control" style="height: 33px" />
                    </div>

                </div>
                <table id="table1" class="table">
                    <thead>
                        <tr class="filters" style="height: 40px; display: flex;">
                            <th class="col-xs-6" style="padding: 10px 0px;">Distributor Name
                            </th>
                            <th class="col-xs-6 " style="padding: 10px 0px;">Price type
                            </th>

                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

            </div>
        </div>
    </div>
</asp:Content>

