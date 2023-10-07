<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="POB_Report.aspx.cs" Inherits="MIS_Reports_POB_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <form runat="server" style="height:550px;">
        <div class="row">
            <div class="col-lg-12 sub-header">
                HQwise POB Report
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Division</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select id="ddldiv"></select>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">State</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" data-size="7" id="ddlstates"></select>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Month</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select class="selectpicker" data-dropup-auto="false" data-size="6" id="ddlmonth">
                        <option value="">Select Month</option>
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
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Year</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" id="fltpyr"></select>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color: #1a73e8; color: white;">View</button>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var AllDiv = [], AllBrand = [], AllFF = [];
        var Div = [], Brands = [], SFF = [];
        $(document).ready(function () {
            loadDivision();
            loadStates();
            fillTpYR();
            $('#btnview').on('click', function () {
                var stcode = $('#ddlstates').val();
                var stname = $('#ddlstates :selected').text();
                var subdiv = $('#ddldiv').val() || 0;
                var mnth = $('#ddlmonth').val() || 0;
                var mnthname = $('#ddlmonth :selected').text();
                var ddlyr = $('#fltpyr').val();
                if (stcode == '') {
                    alert('Select State');
                    return false;
                }
                if (mnth == '') {
                    alert('Select The Month');
                    return false;
                }
                var url = '';
                window.location.href = "Rpt_HQwisePOB.aspx?stcode=" + stcode + "&stname=" + stname + "&subdiv=" + subdiv + "&mnth=" + mnth + "&mnthname=" + mnthname + "&ddlyr=" + ddlyr;
            });
        });
        function fillTpYR() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "POB_Report.aspx/BindDate",
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
                $('#fltpyr').selectpicker({
                    liveSearch: true
                });
            }
            function loadDivision(ssdiv) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "POB_Report.aspx/getDivision",
                    data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    AllDiv = JSON.parse(data.d) || [];
                    Div = AllDiv;
                    if (Div.length > 0) {
                        var dept = $("#ddldiv");
                        dept.empty().append('<option selected="selected" value="">Select Division</option>');
                        for (var i = 0; i < Div.length; i++) {
                            dept.append($('<option value="' + Div[i].subdivision_code + '">' + Div[i].subdivision_name + '</option>'))
                        }
                    }
                }
            });
            $('#ddldiv').selectpicker({
                liveSearch: true
            });
        }
        function loadStates() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "POB_Report.aspx/getStates",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    AllState = JSON.parse(data.d) || [];
                    States = AllState;
                    if (States.length > 0) {
                        var dept = $("#ddlstates");
                        dept.empty().append('<option selected="selected" value="">Select State</option>');
                        for (var i = 0; i < States.length; i++) {
                            dept.append($('<option value="' + States[i].scode + '">' + States[i].sname + '</option>'))
                        }
                    }
                }
            });
            $('#ddlstates').selectpicker({
                liveSearch: true
            });
        }
    </script>
</asp:Content>
