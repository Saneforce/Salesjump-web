<%@ Page Title="Settings for Employees" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Settings_for_Employees.aspx.cs" EnableEventValidation="false" Inherits="MasterFiles_Settings_for_Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "https://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="https://www.w3.org/1999/xhtml">
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
        <script src="Scripts/gridviewScroll.min.js" type="text/javascript"></script>
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

            $(document).ready(function () {

                var div_code = '<%= Session["div_code"] %>';
                var sf_code = '<%= Session["sf_code"] %>';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Settings_for_Employees.aspx/getdata",
                    data: "{'div_code':'" + div_code + "','sf_code':'" + sf_code + "'}",
                    dataType: "json",
                    success: function (data) {

                        for (var i = 0; i < data.d.length; i++) {
                            var geon = "";
                            var tra = "";
                            var fen = "";
                            var eds = "";
                           var chen = "";
                            if (data.d[i].geoneed == 1) {
                                geon = "<input class='tgl tgl-skewed'  type='checkbox' id='g" + data.d[i].sfcode + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='g" + data.d[i].sfcode + "'></label> ";
                            }
                            else {
                                geon = "<input class='tgl tgl-skewed'  type='checkbox' id='g" + data.d[i].sfcode + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='g" + data.d[i].sfcode + "'></label> ";
                            }

                            if (data.d[i].Geo_Track == 0) {
                                tra = "<input class='tgl tgl-skewed'  type='checkbox' id='t" + data.d[i].sfcode + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='t" + data.d[i].sfcode + "'></label> ";
                            }
                            else {
                                tra = "<input class='tgl tgl-skewed'  type='checkbox' id='t" + data.d[i].sfcode + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='t" + data.d[i].sfcode + "'></label> ";
                            }

                            if (data.d[i].Geo_Fencing == 0) {
                                fen = "<input class='tgl tgl-skewed'  type='checkbox' id='f" + data.d[i].sfcode + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='f" + data.d[i].sfcode + "'></label> ";
                            }
                            else {
                                fen = "<input class='tgl tgl-skewed'  type='checkbox' id='f" + data.d[i].sfcode + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='f" + data.d[i].sfcode + "'></label> ";
                            }

                            if (data.d[i].Eddy_Sumry == 0) {
                                eds = "<input class='tgl tgl-skewed'  type='checkbox' id='e" + data.d[i].sfcode + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='e" + data.d[i].sfcode + "'></label> ";
                            }
                            else {
                                eds = "<input class='tgl tgl-skewed'  type='checkbox' id='e" + data.d[i].sfcode + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='e" + data.d[i].sfcode + "'></label> ";
                            }
							if (data.d[i].Van_Sales == 0) {
                                vs = "<input class='tgl tgl-skewed'  type='checkbox' id='v" + data.d[i].sfcode + "' > </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='v" + data.d[i].sfcode + "'></label> ";
                            }
                            else {
                                vs = "<input class='tgl tgl-skewed'  type='checkbox' id='v" + data.d[i].sfcode + "' checked> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='v" + data.d[i].sfcode + "'></label> ";
                            }
							 if (data.d[i].DCR_Summary_ND == 0) {
                                chen = "<input class='tgl tgl-skewed hidecl'  type='checkbox' id='en" + data.d[i].sfcode + "' disabled> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='en" + data.d[i].sfcode + "'></label> ";
                            }
                            else {
                                chen = "<input class='tgl tgl-skewed hidecl'  type='checkbox' id='en" + data.d[i].sfcode + "' disabled checked></input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='en" + data.d[i].sfcode + "' ></label> ";
                            }

                            fcs = "<input class='tgl tgl-skewed'  type='checkbox' id='fcs" + data.d[i].sfcode + "'" + ((data.d[i].fcs == 1)?" checked":"") + "> </input> <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='fcs" + data.d[i].sfcode + "'></label> ";
                            
							
                            $('#table1 tbody').append("<tr style='display: flex;'><td class='col-xs-2' style='text-align: left'> <input class='rsf' type='hidden' name='hsfcode' value=" + data.d[i].Reporting_To_SF + ">   <input class='sfc' type='hidden' name='hsfcode' value=" + data.d[i].sfcode + "> <input class='sft' type='hidden' name='sftype' value=" + data.d[i].sf_type + ">" + data.d[i].Sf_name + "</td> " +
                            "<td class='col-xs-2' style='text-align: -webkit-center' >" + geon + "</td>" +
                            "<td class='col-xs-2' style='text-align: -webkit-center' >" + tra + "</td>" +
                            "<td class='col-xs-2' style='text-align: -webkit-center;' >" + fen + "</td>" +
                            "<td class='col-xs-2' style='text-align: -webkit-center' >" + eds + "</td>" +
							"<td class='col-xs-2' style='text-align: -webkit-center' >" + vs + "</td>" +
							"<td class='col-xs-2 ' id='chnl" + data.d[i].sfcode + "' style='text-align: -webkit-center' >" + chen + "</td>" +
							"<td class='col-xs-2' style='text-align: -webkit-center' >" + fcs + "</td></tr > ");
							  if ('<%=Session["div_code"]%>' != '98') {
                                $('#chnl' + data.d[i].sfcode).hide();
                            }
                        }
						 if ('<%=Session["div_code"]%>' == '98') {
                            $('#chechenen').prop('disabled', false);
							 $('#cen').show();
							var cells = document.getElementsByClassName("hidecl"); 
							for (var i = 0; i < cells.length; i++) { 
								cells[i].disabled = false;
							}
                        }
						 else {
                           
                            $('#cen').hide();
                        }
						
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $("#btnsave").click(function () {
                    fun();
                });


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



                $("#ddlFieldForce").change(function (e) {


                    var lnkText = ',' + $("#ddlFieldForce").val().toLowerCase() + ',';
                    var iCounter = 0;
                    var ch = 0;
                    var $input = $(this);
                    inputContent = $input.val().toLowerCase();

                    $panel = $input.parents('.filterable'),
                                          column = $panel.find('.filters th').index($input.parents('th')),
                                          $table = $panel.find('.table'),
                                          $rows = $table.find('tbody tr');

                    funfilter(lnkText, iCounter);

                    checkhead();

                    e.preventDefault();
                });


                $.ajax({
                    type: "POST",
                    url: "Settings_for_Employees.aspx/GetFieldforce",
                    data: "{'div_code':'" + div_code + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlFieldForce]");
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });


                $("#chgneed").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                });

                $("#chtrack").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                });

                $("#chfencing").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                });

                $("#chedsum").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                });

                $("#chevsal").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                }); 
                 $("#chechenen").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                });
                 $("#chefcs").change(function (e) {
                    var cb = $(this);          //checkbox that was changed
                    th = cb.parent();      //get parent th
                    col = th.index() + 1;  //get column index. note nth-child starts at 1, not zero
                    $("#table1 tbody tr:not([style*='display: none']) td:nth-child(" + col + ") input").prop("checked", this.checked);
                });
				
                $("#table1 tbody tr td:nth-child(2) input:checkbox").click(function () {
                    // alert($("#table1 tbody tr:not([style*='display: none']) td:nth-child(2) input:checkbox:checked").length);
                    //  alert($("#table1 tbody tr td:nth-child(2) input:checkbox:checked").length);
                    if ($("#table1 tbody tr td:nth-child(2) input:checkbox").length == $("#table1 tbody tr td:nth-child(2) input:checkbox:checked").length) {
                        $("#chgneed").attr("checked", "checked");
                    } else {
                        $("#chgneed").removeAttr("checked");
                    }
                });

                $("#table1 tbody tr td:nth-child(3) input:checkbox").click(function () {
                    // alert($("#table1 tbody tr td:nth-child(3) input:checkbox").length);
                    // alert($("#table1 tbody tr td:nth-child(3) input:checkbox:checked").length);
                    if ($("#table1 tbody tr td:nth-child(3) input:checkbox").length == $("#table1 tbody tr td:nth-child(3) input:checkbox:checked").length) {
                        $("#chtrack").attr("checked", "checked");
                    } else {
                        $("#chtrack").removeAttr("checked");
                    }

                });


                $("#table1 tbody tr td:nth-child(4) input:checkbox").click(function () {
                    // alert($("#table1 tbody tr td:nth-child(4) input:checkbox").length);
                    //  alert($("#table1 tbody tr td:nth-child(4) input:checkbox:checked").length);
                    if ($("#table1 tbody tr td:nth-child(4) input:checkbox").length == $("#table1 tbody tr td:nth-child(4) input:checkbox:checked").length) {
                        $("#chfencing").attr("checked", "checked");
                    } else {
                        $("#chfencing").removeAttr("checked");
                    }

                });

                $("#table1 tbody tr td:nth-child(5) input:checkbox").click(function () {
                    // alert($("#table1 tbody tr td:nth-child(5) input:checkbox").length);
                    //  alert($("#table1 tbody tr td:nth-child(5) input:checkbox:checked").length);
                    if ($("#table1 tbody tr td:nth-child(5) input:checkbox").length == $("#table1 tbody tr td:nth-child(5) input:checkbox:checked").length) {
                        $("#chedsum").attr("checked", "checked");
                    } else {
                        $("#chedsum").removeAttr("checked");
                    }

                });
				
			          $("#table1 tbody tr td:nth-child(6) input:checkbox").click(function () {
                    if ($("#table1 tbody tr td:nth-child(6) input:checkbox").length == $("#table1 tbody tr td:nth-child(6) input:checkbox:checked").length) {
                        $("#chevsal").attr("checked", "checked");
                    } else {
                        $("#chevsal").removeAttr("checked");
                    }

                });

                $("#table1 tbody tr td:nth-child(7) input:checkbox").click(function () {
                    if ($("#table1 tbody tr td:nth-child(7) input:checkbox").length == $("#table1 tbody tr td:nth-child(7) input:checkbox:checked").length) {
                        $("#chechenen").attr("checked", "checked");
                    } else {
                        $("#chechenen").removeAttr("checked");
                    }

                });
			
                $("#table1 tbody tr td:nth-child(8) input:checkbox").click(function () {
                    if ($("#table1 tbody tr td:nth-child(8) input:checkbox").length == $("#table1 tbody tr td:nth-child(8) input:checkbox:checked").length) {
                        $("#chefcs").attr("checked", "checked");
                    } else {
                        $("#chefcs").removeAttr("checked");
                    }

                });
				
                checkhead();

            });

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
                if ($("#table1 tbody tr td:nth-child(8) input:checkbox").length == $("#table1 tbody tr td:nth-child(8) input:checkbox:checked").length) {
                    $("#chefcs").attr("checked", "checked");
                } else {
                    $("#chefcs").removeAttr("checked");
                }
            }
            function fun() {
                var arr = [];
                $('.table tbody tr:not([style*="display: none"])').each(function () {
                    arr.push({
                        sf_code: $(this).find(".sfc").val().toLowerCase().toString(),
                        sf_name: $(this).find('td').eq(0).text().toLowerCase(),
                        geoneed: $(this).find('td').eq(1).find('input[type="checkbox"]').prop('checked') ? 0 : 1,
                        geotrack: $(this).find('td').eq(2).find('input[type="checkbox"]').prop('checked') ? 1 : 0,
                        geofencing: $(this).find('td').eq(3).find('input[type="checkbox"]').prop('checked') ? 1 : 0,
                        eddysumry: $(this).find('td').eq(4).find('input[type="checkbox"]').prop('checked') ? 1 : 0,
						vsal: $(this).find('td').eq(5).find('input[type="checkbox"]').prop('checked') ? 1 : 0,
						chnen: $(this).find('td').eq(6).find('input[type="checkbox"]').prop('checked') ? 1 : 0,
						fcs: $(this).find('td').eq(7).find('input[type="checkbox"]').prop('checked') ? 1 : 0,
                        sf_type: $(this).find(".sft").val().toLowerCase().toString()
                    });
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Settings_for_Employees.aspx/savedata",
                    data: "{'data':'" + JSON.stringify(arr) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert("Employee Setting has been updated successfully!!!");
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
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

              

        </script>
        <link href="CSS/GridviewScroll.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .results tr[visible='false'], .no-result
            {
                display: none;
            }
            
            .results tr[visible='true']
            {
                display: table-row;
            }
            
            .counter
            {
                padding: 8px;
                color: #ccc;
            }
            
            
            .table-fixed thead
            {
                width: 97%;
            }
            .table-fixed tbody
            {
                height: 230px;
                overflow-y: auto;
                width: 100%;
            }
            .table-fixed thead, .table-fixed tbody, .table-fixed tr, .table-fixed td, .table-fixed th
            {
                display: block;
            }
            .table-fixed tbody td, .table-fixed thead > tr > th
            {
                float: left;
                border-bottom-width: 0;
            }
            
            .table thead
            {
                width: 100%;
            }
            
            .table tbody
            {
                height: 562px;
                overflow-y: auto;
                width: 100%;
            }
            
            .table thead tr
            {
                width: 99%;
            }
            
            .table tr
            {
                width: 100%;
            }
            
            .table thead, .table tbody, .table tr, .table td, .table th
            {
                display: inline-block;
            }
            
            .table thead
            {
                background: #4697ce;
                color: #fff;
            }
            
            .table tbody td, .table thead > tr > th
            {
                float: left;
                border-bottom-width: 0;
            }
            
            .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th
            {
                padding: 8px;
                height: 75px;
                text-align: center;
                line-height: 32px;
            }
            
            .odd
            {
                background: #ffffff;
                color: #000;
            }
            
            .even
            {
                background: #efefef;
                color: #000;
            }
            
            .points_table_scrollbar
            {
                height: 612px;
                overflow-y: scroll;
            }
            
            .points_table_scrollbar::-webkit-scrollbar-track
            {
                -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.9);
                border-radius: 10px;
                background-color: #444444;
            }
            
            .points_table_scrollbar::-webkit-scrollbar
            {
                width: 1%;
                min-width: 5px;
                background-color: #F5F5F5;
            }
            
            .points_table_scrollbar::-webkit-scrollbar-thumb
            {
                border-radius: 10px;
                background-color: #D62929;
                background-image: -webkit-linear-gradient(90deg, transparent, rgba(0, 0, 0, 0.4) 50%, transparent, transparent);
            }
            
            
            
            
            .tg-list-item
            {
                margin: 0 2em;
            }
            
            
            .tgl
            {
                display: none;
            }
            .tgl, .tgl:after, .tgl:before, .tgl *, .tgl *:after, .tgl *:before, .tgl + .tgl-btn
            {
                box-sizing: border-box;
            }
            .tgl::-moz-selection, .tgl:after::-moz-selection, .tgl:before::-moz-selection, .tgl *::-moz-selection, .tgl *:after::-moz-selection, .tgl *:before::-moz-selection, .tgl + .tgl-btn::-moz-selection
            {
                background: none;
            }
            .tgl::selection, .tgl:after::selection, .tgl:before::selection, .tgl *::selection, .tgl *:after::selection, .tgl *:before::selection, .tgl + .tgl-btn::selection
            {
                background: none;
            }
            .tgl + .tgl-btn
            {
                outline: 0;
                display: block;
                width: 4em;
                height: 2em;
                position: relative;
                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }
            
            
            .tgl-skewed + .tgl-btn
            {
                overflow: hidden;
                -webkit-transform: skew(-10deg);
                transform: skew(-10deg);
                -webkit-backface-visibility: hidden;
                backface-visibility: hidden;
                -webkit-transition: all .2s ease;
                transition: all .2s ease;
                font-family: sans-serif;
                background: #888;
            }
            .tgl-skewed + .tgl-btn:after, .tgl-skewed + .tgl-btn:before
            {
                -webkit-transform: skew(10deg);
                transform: skew(10deg);
                display: inline-block;
                -webkit-transition: all .2s ease;
                transition: all .2s ease;
                width: 100%;
                text-align: center;
                position: absolute;
                line-height: 2em;
                font-weight: bold;
                color: #fff;
                text-shadow: 0 1px 0 rgba(0, 0, 0, 0.4);
            }
            .tgl-skewed + .tgl-btn:after
            {
                left: 100%;
                content: attr(data-tg-on);
            }
            .tgl-skewed + .tgl-btn:before
            {
                left: 0;
                content: attr(data-tg-off);
            }
            .tgl-skewed + .tgl-btn:active
            {
                background: #888;
            }
            .tgl-skewed + .tgl-btn:active:before
            {
                left: -10%;
            }
            .tgl-skewed:checked + .tgl-btn
            {
                background: #86d993;
            }
            .tgl-skewed:checked + .tgl-btn:before
            {
                left: -100%;
            }
            .tgl-skewed:checked + .tgl-btn:after
            {
                left: 0;
            }
            .tgl-skewed:checked + .tgl-btn:active:after
            {
                left: 10%;
            }
            
            .filterable
            {
                margin-top: 15px;
            }
            .filterable .panel-heading .pull-right
            {
                margin-top: -20px;
            }
            .filterable .filters input[disabled]
            {
                background-color: transparent;
                border: none;
                cursor: auto;
                box-shadow: none;
                padding: 0;
                height: auto;
            }
            .filterable .filters input[disabled]::-webkit-input-placeholder
            {
                color: #333;
            }
            .filterable .filters input[disabled]::-moz-placeholder
            {
                color: #333;
            }
            .filterable .filters input[disabled]:-ms-input-placeholder
            {
                color: #333;
            }
            
            .modal
            {
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
            .loading
            {
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
            select
            {
                padding: 0 4px;
            }
            #table1 tr td :nth-child(4), #table1 tr th :nth-child(4)
            {
                display: none;
            }
			.pad
			{
			display: block;
            padding: 5px;
            height: 32px;
	            }
          </style>
    </head>
    <body>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container">
            <div class="row">
                <div class="panel panel-primary filterable">
                    <div style="display: flex; padding: 10px;">
                        <div class="col-md-2" style="padding: 7px;">
                            Search By :</div>
                        <div class="col-md-3">
                            <input type="text" id="txtser" class="form-control" style="height: 33px" /></div>
                        <div class="col-md-2" style="padding: 7px;">
                            Filter :</div>
                        <div class="col-md-4">
                            <select id="ddlFieldForce" class="form-control" style="height: 33px">
                            </select></div>
                    </div>
                    <table id="table1" class="table">
                        <thead>
                            <tr class="filters" style="height: 80px;display: flex;">
                                <th class="col-xs-2" style="padding: 26px 0px;">
                                    Employee Name
                                </th>
                                <th class="col-xs-2 align-middle" style="text-align: -webkit-center">
                                    Location
                                    <input class='tgl tgl-skewed' type='checkbox' id='chgneed'> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chgneed'>
                                    </label>
                                </th>
                                <th class="col-xs-2" style="text-align: -webkit-center; ">
                                    Tracking
                                    <input class='tgl tgl-skewed' type='checkbox' id='chtrack'> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chtrack'>
                                    </label>
                                </th>
                                <th class="col-xs-2" style="text-align: -webkit-center;">
                                    Fencing
                                    <input class='tgl tgl-skewed' type='checkbox' id='chfencing'> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chfencing'>
                                    </label>
                                </th>
                                <th class="col-xs-2" style="text-align: -webkit-center">
                                    Edit Day Summary
                                    <input class='tgl tgl-skewed' type='checkbox' id='chedsum'> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chedsum'>
                                    </label>
                                </th>
								    <th class="col-xs-2" style="text-align: -webkit-center">
                                    Van Sales
                                    <input class='tgl tgl-skewed' type='checkbox' id='chevsal'> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chevsal'>
                                    </label>
                                </th>
								<th class="col-xs-2 hidecl" id="cen" style="text-align: -webkit-center">
                                   Channel Entry
                                    <input class='tgl tgl-skewed' type='checkbox' id='chechenen' disabled> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chechenen'>
                                    </label>
                                </th>
								<th class="col-xs-2" id="fcs" style="text-align: -webkit-center">
                                   First Call Selfie
                                    <input class='tgl tgl-skewed' type='checkbox' id='chefcs'> </input>
                                    <label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for='chefcs'>
                                    </label>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div style="text-align: center">
            <div class="col-md-12">
                <button type="button" id="btnsave" class="btn btn-primary" style="width: 100px;">
                    Save
                </button>
                <button type="button" id="Cancel" class="btn btn-primary" style="width: 100px;">
                    Cancel
                </button>
            </div>
        </div>
        </div> </div>
        </form>
    </body>
    </html>
</asp:Content>
