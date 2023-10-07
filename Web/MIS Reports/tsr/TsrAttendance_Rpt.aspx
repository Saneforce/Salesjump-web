<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="TsrAttendance_Rpt.aspx.cs" Inherits="MIS_Reports_tsr_TsrAttendance_Rpt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <link type="text/css" href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />

    <style type="text/css">
        .select {
            position: relative;
            display: block;
            /*width: 15em;
                height: 2em;*/
            line-height: 3;
            overflow: hidden;
            border-radius: .25em;
            padding-bottom: 10px;
			width: 350px;
        }
		.ranges{
            display:inline-flex;height:200px;width:420px;overflow-x:scroll;
        }
		.daterangepicker .ranges {
			width: 420px;
			text-align: left;
		}
		.daterangepicker.dropdown-menu {
			max-width: none;
			z-index: 3000;
			top: 306px;
		}
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
     

    <script type="text/javascript">
        ddlstate = []; ddlzone = []; ddlso = []; ddlTsr = []; ddldisbutr = []; ddlFieldForce = []; ddlevent = [];
        $(document).ready(function () {
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



            retrievestate(); retrieveZone(); retrieveDistributor(); retrieveIdentification();
            var mod = "TP MY Day Plan";

            $('#ddmodeev').hide();           
            $('#ddb').hide();
            
            $('#ddlstate').on('change', function () {
                var stval = $('#ddlstate').val();
                //showTabledata();
                $('#ddlzone').empty().append('<option value="0">--select--</option>');
                var fddlzone = ddlzone.filter(function (a) {
                    return a.sf_type == '2' && a.State_Code == stval && (a.sf_Designation_Short_Name == 'ASM' || a.sf_Designation_Short_Name == 'SAM' || a.Sf_Code == 'MGR0111');
                })
                for (var i = 0; i < fddlzone.length; i++) {
                    $('#ddlzone').append('<option value="' + fddlzone[i].Sf_Code + '">' + fddlzone[i].Sf_Name + '</option>');
                }
            });
            $('#ddlzone').on('change', function () {
                retrieveSO($('#ddlzone').val());
                ddltsrch();
            });
            $('#ddlso').on('change', function () {
                ddltsrch();
            });
            $('#ddltsr').on('change', function () {
                var selsf = $('#ddltsr').val();
                $('#ddldisbutr').empty().append('<option value="0">--select--</option>');
                var fddldisbutr = ddldisbutr.filter(function (a) {
                    return a.Field_Code == selsf;
                })
                for (var i = 0; i < fddldisbutr.length; i++) {
                    $('#ddldisbutr').append('<option value="' + fddldisbutr[i].Stockist_Code + '">' + fddldisbutr[i].Stockist_Name + '</option>');
                }
            });
            $('#btnsubmit').on('click', function () {
                var id = '';
                if ($('#ordDate').text() == "") { alert("Please select  Date."); $('#ordDate').focus(); return false; }
                var id = $('#ordDate').text();
                id = id.split('-');
                var fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
                var tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
                var stat = $('#ddlstate :selected').val();
                var zon = $('#ddlzone :selected').val();
                var so = $('#ddlso :selected').val();
                var tsr = $('#ddltsr :selected').val();
                var distru = $('#ddldisbutr :selected').val();
                var statn = $('#ddlstate :selected').text();
                var zonnm = $('#ddlzone :selected').text();
                var sonm = $('#ddlso :selected').text();
                var tsrnm = $('#ddltsr :selected').text();
                var distrunm = $('#ddldisbutr :selected').text();
                var ddlevent = $('#ddlevent :selected').val();
                var sf = (tsr == '0') ? ((so == '0') ? ((zon == '0') ? 'admin' : zon) : so) : tsr;
                //var sf = (tsr == '0') ? ((so == '0') ? ((zon == '0') ? 'admin' : zon) : (so +","+ zon)) : (tsr+","+so+","+zon);
                //var sfn = (tsr == '0') ? ((so == '0') ? ((zon == '0') ? 'admin' : zonnm) : (sonm+","+zonnm)) : (tsrnm+","+sonm+","+zonnm);
                // var mod = '<=Mode>';

                var mod = "TP MY Day Plan";

                var sfn = (tsr == '0') ? ((so == '0') ? ((zon == '0') ? 'admin' : zonnm) : sonm) : tsrnm;

                var sURL = "";

                sURL = "/MasterFiles/Reports/tsr/TsrRpt_DCR_View.aspx?sf_code=" + sf + "&div_code=" + <%=Session["div_code"]%> +"";
                sURL += "&Mode= " + mod + "&Sf_Name=" + sfn + "&FDate=" + fdt + "&TDate=" + tdt + "";
                sURL += "&Dst_name=" + distrunm + "&Dst_code=" + distru + "&st_name=" + statn + "&st_code=" + stat + "";

                //alert(sURL);
                window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');               
                
            });
        });
        function ddltsrch() {            
                var sosf = $('#ddlso').val();
                var assf = $('#ddlzone').val();
                $('#ddltsr').empty().append('<option value="0">--select--</option>');
                var fddlTsr = ddlTsr.filter(function (a) {
                    return (a.Reporting_To_SF == assf || a.Reporting_To_SF == sosf) && (a.sf_Designation_Short_Name == 'TSR' || a.sf_Designation_Short_Name == 'BDE' || a.sf_Designation_Short_Name == 'BDE T' || a.sf_Designation_Short_Name == 'ISR');
                });
                for (var i = 0; i < fddlTsr.length; i++) {
                    $('#ddltsr').append('<option value="' + fddlTsr[i].Sf_Code + '">' + fddlTsr[i].Sf_Name + '</option>');
                }
        }
        function retrievestate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrAttendance_Rpt.aspx/getstate",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('#ddlstate').empty().append('<option value="0">--select--</option>');
                    ddlstate = JSON.parse(data.d) || [];
                    for (var i = 0; i < ddlstate.length; i++) {
                        $('#ddlstate').append('<option value="' + ddlstate[i].State_code + '">' + ddlstate[i].StateName + '</option>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function retrieveZone() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrAttendance_Rpt.aspx/getzone",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    ddlzone = JSON.parse(data.d) || [];
                    ddlTsr = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function retrieveSO($mgrid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrAttendance_Rpt.aspx/getSO",
                data: "{'divcode':'<%=Session["div_code"]%>','mgrid':'"+$mgrid+"'}",
                dataType: "json",
                success: function (data) {
                    ddlso = JSON.parse(data.d) || [];
                    $('#ddlso').empty().append('<option value="0">--select--</option>');
                    for (var i = 0; i < ddlso.length; i++) {
                        $('#ddlso').append('<option value="' + ddlso[i].SF_Code + '">' + ddlso[i].SF_Name + '</option>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function retrieveDistributor() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrAttendance_Rpt.aspx/getDistributor",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    ddldisbutr = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function retrieveIdentification() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrAttendance_Rpt.aspx/getIdentification",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    console.log(data);
                    ddlevent = JSON.parse(data.d) || [];
                    $('#ddlevent').empty().append('<option value="0">All</option>');
                    for (var i = 0; i < ddlevent.length; i++) {
                        $('#ddlevent').append('<option value="' + ddlevent[i].title + '">' + ddlevent[i].title + '</option>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
		$(function () {
            var start = moment();
            var end = moment();
            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
            }
            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);
            cb(start, end);
        });



    </script>


    

    <form id="form1" runat="server">
        
        <div class="row" style="overflow-x:clip;">
            <div class="col-xs-12 col-sm-8" id="dstate" style="padding-top: 10px;">
                <label id="lblstate" class="col-md-2 col-md-offset-3  control-label">State </label>
                <select class="select" name="ddlstate" id="ddlstate">
                    <option value="select">--Select--</option>
                </select>
            </div>
            <div class="col-xs-12 col-sm-8" id="dzone" style="padding-top: 10px;">
                <label id="lblzone" class="col-md-2 col-md-offset-3  control-label">Zone </label>
                <select class="select" name="ddlzone" id="ddlzone" >
                    <option value="0">--Select--</option>
                </select>
            </div>
            <div class="col-xs-12 col-sm-8" id="dso" style="padding-top: 10px;">
                <label id="lblso" class="col-md-2 col-md-offset-3  control-label">SO </label>
                <select class="select" name="ddlso" id="ddlso" >
                    <option value="0">--Select--</option>
                </select>
            </div>
            <div class="col-xs-12 col-sm-8" id="dtsr" style="padding-top: 10px;">
                <label id="lbltsr" class="col-md-2 col-md-offset-3  control-label">TSR </label>
                <select class="select" name="ddltsr" id="ddltsr" >
                    <option value="0">--Select--</option>
                </select>
            </div>
            <div class="col-xs-12 col-sm-8  hidden" id="ddb" style="padding-top: 10px;">
                <label id="lbldb" class="col-md-2 col-md-offset-3  control-label">DB </label>
                <select class="select" name="ddldisbutr" id="ddldisbutr" >
                    <option value="0">--Select--</option>
                </select>
            </div>
            <div class="col-xs-12 col-sm-8 hidden ddmodeev" id="ddmodeev" style="padding-top: 10px;">
                <%--<label id="lblmodeev" class="col-md-2 col-md-offset-3  control-label">Mode Identification </label>--%>
                <label id="lblmodeev" class="col-md-2 col-md-offset-3  control-label">Type</label>
                <select class="select" name="ddlevent" id="ddlevent" >
                    <option value="0">All</option>
                   <%-- <option value="Promotion">Promotion</option>--%>
                </select>
            </div>
            <div class="col-xs-12 col-sm-8" id="ddate" style="padding-top: 10px;">
                <label id="lbldt" class="col-md-2 col-md-offset-3  control-label">
                     Date
                </label>
                <div>
                    <div style="float: left; margin-right: 15px;">
                        <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                            <i class="fa fa-calendar"></i>&nbsp;
                            <span id="ordDate"></span>
                            <i class="fa fa-caret-down"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <button type="button" class="btn btn-primary"  style="vertoical-align: middle; width: 100px" id="btnsubmit">Submit</button>         
                        <button type="button" class="btn btn-primary hidden" style="vertical-align: middle; width: 100px" id="btnsubmit1">Submit</button>
                    <%--<a id="btnsubmit" class="btn btn-primary" 
                        <%--onclick="btnSubmit_Click1" 
                        style="vertical-align: middle; width: 100px">
                        <span>Submit</span></a>--%>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
