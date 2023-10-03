<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="HolidayMaster.aspx.cs" Inherits="MasterFiles_HolidayMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <link type="text/css" href="../css/calendar_by_year.css" rel="stylesheet" />
            <script type="text/javascript" src="../js/jquery.min.1.11.1.js"></script>
            <script type="text/javascript" src="../js/calendar_by_year.js"></script>
            <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
            <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
            <script type="text/javascript">
                jQuery(document).ready(function ($) {
                    $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();


                    buildYearCalendar(yyyy);


                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    var today = dd + '/' + mm + '/' + yyyy;

                    $('#datefrom').val(today);
                    $('#dateto').val(today);

                    $('#holiID').val(0);
                    $(document).on('click', '#year', function () {
                        getdata();
                        $('#year-calendar .day').each(function () {
                            $(this).removeClass('sel');
                        });

                    });

                    $(document).on('change', '#ddlState', function () {
                        clr();
                        buildYearCalendar(yyyy);
                        getdata();
                        $('#year-calendar .day').each(function () {
                            $(this).removeClass('sel');
                        });
                    });

                    var Fyear = $('.selected-year').text();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "HolidayMaster.aspx/GetState",
                        dataType: "json",
                        success: function (data) {
                            var ddlCustomers = $("#ddlState");
                            ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                            $.each(data.d, function () {
                                ddlCustomers.append($("<option></option>").val(this['stCode']).html(this['stName']));
                            });
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });


                    $('#THoliday').removeClass(SELECTED);
                    $('#THoliday').removeClass(SELSTART);
                    $('#THoliday').removeClass(SELEND);


                    $(document).on('click', '.day', function () {
                        $('.selected').each(function (index, element) {
                            if ($(this).attr('selDt') == "true" && index != 0) {
                                $('.selected').removeClass(SELECTED);
                                $('.select-start').removeClass(SELSTART);
                                $('.select-end').removeClass(SELEND);
                            }
                            else if ($(this).attr('selDt') == "true" && index == 0) {
                                $('#tags').val($(this).attr('hod'));
                                $('#Remarks').val($(this).attr('rem'));
                                $('#holiID').val($(this).attr('hodid'));
                            }
                            else {
                                $('#tags').val('');
                                $('#Remarks').val('');
                                $('#holiID').val('0');
                            }

                        });
                        //$('#tags').focus();
                    });

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "HolidayMaster.aspx/GetHolidays",
                        dataType: "json",
                        success: function (data) {
                            dts = data.d;
                            if (dts.length > 0) {
                                $("#tags").autocomplete({
                                    source: dts,
                                    change: function (event, ui) {
                                        if (ui.item != null) {
                                            $('#holiID').val(ui.item.id);
                                        }
                                        else {
                                            $('#holiID').val(0);
                                        }
                                    }

                                });
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });

                    getdata();

                    function getdata() {
                        var Fyear = $('.selected-year').text();
                        var stateCode = $('#ddlState').val();
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "HolidayMaster.aspx/GetDate",
                            data: "{'FYear':'" + Fyear + "','stateCode':'" + stateCode + "'}",
                            dataType: "json",
                            success: function (data) {
                                tDts = data.d;
                                getTable(tDts);
                                filldata();
                            },
                            error: function (data) {
                                alert(JSON.stringify(data));
                            }
                        });
                    }

                    function getTable(tDts) {
                        var stateCode = $('#ddlState').val();
                        $('#THoliday').empty();
                        var str = '<tr><th>SlNo</th><th>Date</th><th style="width: 250px">holiday Name</th><th></th><th></th></tr>';
                        $('#THoliday').append(str);
                        if (tDts.length > 0) {
                            for (var i = 0; i < tDts.length; i++) {
                                // $('#THoliday').append('<tr><td>' + (i + 1) + '</td> <td>' + tDts[i].HolidayDate + '</td> <td>' + tDts[i].HolidayName + '</td> <td><a class="btn btn-primary btnedit" hdidv=' + tDts[i].HolidayID + ' hdremarks=' + tDts[i].HolidayRemarks + '>Edit</a></td>  </tr>');
                                $('#THoliday').append('<tr><td>' + (i + 1) + '</td> <td>' + tDts[i].HolidayDate + '</td> <td>' + tDts[i].HolidayName + '</td> <td><a class="btn btn-primary btnedit" hdidv=' + tDts[i].HolidayID + ' hdremarks=' + tDts[i].HolidayRemarks + '>Edit</a></td> <td><a class="btn btn-primary btndelete" hdeldidv=' + tDts[i].HolidayID + ' hdeldate=' + tDts[i].HolidayDate + ' hdelstcode=' + stateCode + '  hddelremarks=' + tDts[i].HolidayRemarks + '>Delete</a></td>  </tr>');
                                //$('#THoliday').append('<tr><td>' + (i + 1) + '</td> <td>' + tDts[i].HolidayDate + '</td> <td>' + tDts[i].HolidayName + '</td> <td><a class="btn btn-primary btnedit" hdidv=' + tDts[i].HolidayID + '  hdremarks=' + tDts[i].HolidayRemarks + '>Edit</a></td> <td><a class="btn btn-primary btndelete" hdeldidv=' + tDts[i].HolidayID + ' hdeldate=' + tDts[i].HolidayDate + ' hdelstcode=' + stateCode+'  hddelremarks=' + tDts[i].HolidayRemarks + '>Delete</a></td>  </tr>');
                            }
                        }
                        else {
                            $('#THoliday').append('<tr><td colspan="4" style="color:red">No Records Found..!</td></tr>');
                        }

                    }

                    function filldata() {
                        $('#year-calendar .day').each(function () {
                            for (var i = 0; i < tDts.length; i++) {
                                if ($(this).attr('data-yc-value') == tDts[i].odFormat) {
                                    $(this).addClass('sel');
                                    $(this).attr('dtval', tDts[i].HolidayDate);
                                    $(this).attr('selDt', 'true');
                                    $(this).attr('rem', tDts[i].HolidayRemarks);
                                    $(this).attr('hod', tDts[i].HolidayName);
                                    $(this).attr('hodid', tDts[i].HolidayID);
                                }
                            }
                        });
                    }

                    $(document).on('change', '#tags', function () {
                        if (tDts.length > 0) {
                            filldata();
                        }
                    });

                    $(document).on('click', '#btnclear', function () {
                        clr();
                    });

                    function clr() {
                        $('#tags').val('');
                        $('#Remarks').val('');
                        $('#holiID').val('0');
                        $('#datefrom').val('');
                        $('#dateto').val('');
                        $('#tags').focus();
                        $('#year-calendar .day').removeClass('selected');
                    }


                    $(document).on('click', '.btndelete', function () {

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "HolidayMaster.aspx/Deletehoilday",

                            data: "{'holiID':'" + $(this).attr('hdeldidv') + "','holidate':'" + $(this).attr('hdeldate') + "','statecode':'" + $(this).attr('hdelstcode') + "'}",
                            dataType: "json",
                            success: function (data) {
                                var i = data.d;
                                if (i >= 1) {
                                    alert('Deleted Successfully');
                                    getdata();
                                }
                                else {
                                    alert('Deleted Failed');
                                }
                            },
                            error: function (rs) {
                                alert(JSON.stringify(result));
                            }
                        });
                    });

                    $(document).on('click', '.btnedit', function () {
                        $('#tags').val($(this).closest('tr').find('td').eq(2).text());
                        $('#Remarks').val($(this).attr('hdremarks'));
                        $('#holiID').val($(this).attr('hdidv'));
                        $('#datefrom').val($(this).closest('tr').find('td').eq(1).text());
                        $('#dateto').val($(this).closest('tr').find('td').eq(1).text());
                    });

                    $(document).on('click', '#btnsave', function () {
                        var hoid = $('#holiID').val();
                        var stateCode = $('#ddlState').val();

                        if (stateCode == 0) { alert('Select State'); $('#ddlState').focus(); return false; }

                        var hoName = $('#tags').val();
                        if (hoName == '') { alert('Type Holiday'); $('#tags').focus(); return false; }
                        var hoRemarks = $('#Remarks').val();
                        if (hoRemarks == '') { alert('Type Remarks'); $('#Remarks').focus(); return false; }

                        var Fyear = $('.selected-year').text();
                        var fDate = $('#datefrom').val();
                        if (fDate == '') { alert('Select Date'); return false; }
                        var tDate = $('#dateto').val();


                        var fd = new Date(toDate(fDate));
                        var td = new Date(toDate(tDate));


                        days = (td - fd) / (1000 * 60 * 60 * 24);

                        if (Math.round(days) > 5) {
                            alert('Select Maximum 5 Days');
                            return false;
                        }
                        else {

                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "HolidayMaster.aspx/InsertHoliday",

                                data: "{'FYear':'" + Fyear + "','FDate':'" + fDate + "','TDate':'" + tDate + "','HolidayId':'" + hoid + "','HolidayName':'" + hoName + "','HolidayRemarks':'" + hoRemarks + "','stateCode':'" + stateCode + "'}",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d == 'Sucess') {
                                        alert('Record Successfully Add (or) Update');
                                        clr();
                                        getdata();
                                    }
                                    else {
                                        alert('Record Unsuccessfully Add (or) Update');
                                    }
                                },
                                error: function (data) {
                                    alert(JSON.stringify(data));
                                }
                            });
                        }
                    });



                    $(document).on('click', '#btnsave', function () {
                        var hoid = $('#holiID').val();
                        var stateCode = $('#ddlState').val();

                        if (stateCode == 0) { alert('Select State'); $('#ddlState').focus(); return false; }

                        var hoName = $('#tags').val();
                        if (hoName == '') { alert('Type Holiday'); $('#tags').focus(); return false; }
                        var hoRemarks = $('#Remarks').val();
                        if (hoRemarks == '') { alert('Type Remarks'); $('#Remarks').focus(); return false; }

                        var Fyear = $('.selected-year').text();
                        var fDate = $('#datefrom').val();
                        if (fDate == '') { alert('Select Date'); return false; }
                        var tDate = $('#dateto').val();


                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "HolidayMaster.aspx/InsertHoliday",

                            data: "{'FYear':'" + Fyear + "','FDate':'" + fDate + "','TDate':'" + tDate + "','HolidayId':'" + hoid + "','HolidayName':'" + hoName + "','HolidayRemarks':'" + hoRemarks + "','stateCode':'" + stateCode + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Sucess') {
                                    alert('Record Successfully Add (or) Update');
                                    clr();
                                }
                                else {
                                    alert('Record Unsuccessfully Add (or) Update');
                                }
                            },
                            error: function (data) {
                                alert(JSON.stringify(data));
                            }
                        });


                    });
                }); tags

                function monthDiff(d1, d2) {
                    var months;
                    months = (d2.getFullYear() - d1.getFullYear()) * 12;
                    months -= d1.getMonth();
                    months += d2.getMonth();
                    return months <= 0 ? 0 : months;
                }

                function toDate(selector) {
                    var from = selector.split("/")
                    return new Date(from[2], from[1] - 1, from[0])
                }
            </script>
            
            <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" type="text/css" />
            <link rel="stylesheet" href="/resources/demos/style.css" type="text/css" />

        </head>
        <body>
            <form id="form1" runat="server">
                <div class="container" style="min-width: 100%; margin: 0px">
                    <div class="row" style="background-color: #2a9bb7;">
                        <label for="ddlState" class="control-label col-md-9" style="color: #fff; text-align: left; padding-top: 8px; font-size: x-large; color: #fff;">Holiday Master   </label>
                        <label for="ddlState" class="control-label col-md-1" style="color: #fff; padding-top: 14px; text-align: right">State</label>
                        <div class="col-md-2 inputGroupContainer" style="padding-top: 10px;">
                            <select id="ddlState" class="form-control" data-style="btn-success" style="width: 100px;margin-left: -7px;">

                            </select>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3" style="text-align: left;">
                            <label for="date-range">Dates</label>
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <label for="tags">Holiday </label>
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <label for="Remarks">Remarks </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3" style="text-align: left;">
                            <input id="datefrom" type="text" style="width: 45%" class="datetimepicker" />To
                            <input id="dateto" style="width: 45%" type="text" class="datetimepicker" />
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <input id="tags" type="text" class="form-control" />
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <input id="Remarks" type="text" class="form-control"  />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6" style="text-align: center;">
                            <a class="btn btn-primary" id="btnsave">Save</a>
                            <a class="btn btn-primary" id="btnclear">Clear</a>
                            <input id="holiID" type="hidden" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-7">
                            <div id="year-calendar"></div>
                        </div>
                        <div class="col-md-5">
                            <table id="THoliday" class="newStly" style="width:100%"> 

                            </table>
                        </div>
                    </div>
                </div>
            </form>
            <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
            <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
            <script type="text/javascript">
              
                $("input[type='text']").on("keypress", function (e) {
                    var val = $(this).val();
                    var regex = /(<([^>]+)>)/ig;
                    var result = val.replace(regex, "");
                    $(this).val(result);
                });


                //jQuery(document).ready(function ($) {
                //    //you can now use $ as your jQuery object.
                //    $(function () {
                //        var availableTags = [
                //            "ActionScript",
                //            "AppleScript",
                //            "Asp",
                //            "BASIC",
                //            "C",
                //            "C++",
                //            "Clojure",
                //            "COBOL",
                //            "ColdFusion",
                //            "Erlang",
                //            "Fortran",
                //            "Groovy",
                //            "Haskell",
                //            "Java",
                //            "JavaScript",
                //            "Lisp",
                //            "Perl",
                //            "PHP",
                //            "Python",
                //            "Ruby",
                //            "Scala",
                //            "Scheme"
                //        ];
                //        $("#tags").autocomplete({
                //            source: availableTags,
                //        });
                //    });
                //});

            </script>
        </body>
    </html>    
</asp:Content>
