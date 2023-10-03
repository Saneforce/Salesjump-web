﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="OrderVSale.aspx.cs" Inherits="MIS_Reports_OrderVSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <form runat="server" style="height:550px;">
        <div class="row">
            <div class="col-lg-12 sub-header">
                Invoice Upload Report
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
            <label class="col-md-2  col-md-offset-3  control-label">FieldForce</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" id="ddlff"></select>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="fdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="tdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color:#1a73e8;color:white;">View</button>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var AllDiv = [], AllFF = [];
        var Div = [], SFF = [];
        $(document).ready(function () {
            loadDivision();
            loadFieldForce();
            $('#ddldiv').on('change', function () {
                FFilter();
            });
            $('#btnview').on('click', function () {
                var sfcode = $('#ddlff').val();
                var sfname = $('#ddlff :selected').text();
                var subdiv = $('#ddldiv').val() || 0;
                if (sfcode == '') {
                    alert('Select FieldForce');
                    return false;
                }
                var fdate = $('#fdate').val();
                if (fdate == '') {
                    alert('Select the From Date');
                    return false;
                }
                var tdate = $('#tdate').val();
                if (tdate == '') {
                    alert('Select the To Date');
                    return false;
                }
                window.location.href = `Rpt_OrderVSales.aspx?sf=${sfcode}&sfnm=${sfname}&fdt=${fdate}&tdt=${tdate}&subdiv=0&state=0`;
            });
        });
        function FFilter() {
            var cstate = 0;
            var cdiv = $("#ddldiv").val() || 0;
            $('#ddlff').selectpicker('destroy');
            var filsf = [];
            if (cdiv != 0) {
                filsf = AllFF.filter(function (a) {
                    return ((',' + (a.subdivision_code)).indexOf(',' + cdiv + ',')) > -1
                });
            }
            else {
                filsf = AllFF.filter(function (a) {
                    return (a.Sf_Code != 'admin');
                });
            }
            var dept = $("#ddlff");
            dept.empty().append('<option selected="selected" value="">Select FieldForce</option>');
            dept.append('<option value="admin">Admin</option>');
            if (filsf.length > 0) {
                for (var i = 0; i < filsf.length; i++) {
                    dept.append($('<option value="' + filsf[i].Sf_Code + '">' + filsf[i].Sf_Name + '</option>'))
                }
            }
            $('#ddlff').selectpicker({
                liveSearch: true
            });
        }
        function loadDivision(ssdiv) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "OrderVSale.aspx/getDivision",
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
        function loadFieldForce() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "OrderVSale.aspx/getFieldForce",
                data: '{"divcode":"<%=Session["div_code"]%>","sfcode":"<%=Session["Sf_Code"]%>"}',
                dataType: "json",
                success: function (data) {
                    AllFF = JSON.parse(data.d) || [];
                    SFF = AllFF;
                    if (SFF.length > 0) {
                        var dept = $("#ddlff");
                        dept.empty().append('<option selected="selected" value="">Select FieldForce</option>');
                        for (var i = 0; i < SFF.length; i++) {
                            dept.append($('<option value="' + SFF[i].Sf_Code + '">' + SFF[i].Sf_Name + '</option>'))
                        }
                    }
                }
            });
            $('#ddlff').selectpicker({
                liveSearch: true
            });
        }
    </script>
</asp:Content>