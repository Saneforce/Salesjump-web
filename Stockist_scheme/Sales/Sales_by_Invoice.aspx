<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Sales_by_Invoice.aspx.cs" Inherits="Stockist_Sales_Sales_by_Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link type="text/css" rel="stylesheet" href="../css/style1.css" />--%>
    <%--<link type="text/css" href="../../css/style1.css" rel="stylesheet" />--%>
    <link href="../../css/jquery.multiselect.css" rel="stylesheet" />
    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        var retailer_code = '';

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

        function NewWindow() {
            var fdate = $('#txtfrdate').val();
            if (fdate == '') {
                alert('Select the From Date');
                return false;
            }
            var Tdate = $('#ttxtodate').val();
            if (Tdate == '') {
                alert('Select the To Date');
                return false;
            }

            //$('#ddl_retailer > option:selected').each(function () {
            //    retailer_code += ',' + $(this).val();
            //});
            //if (retailer_code == '') {
            //    alert("select Retailer");
            //    return false;
            //} 
            if (retailer_code == '') {
                alert("No data found");
                return false;
            }
            var sf_code = '<%=Session["Sf_Code"]%>';
            window.open("Sales_by_Invoice_Report.aspx?&Fdate=" + fdate + "&Tdate=" + Tdate + "&stk=" + sf_code + "&retailer=" + retailer_code, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
        }
        $(document).on("change", ".todate", function () {
            var fdate = $('#txtfrdate').val();
            if (fdate == '' || fdate == undefined) {
                alert('Select   From Date');
                return false;
            }
            retrieveRetailer();
            //$('#divret').show();
        });



        function retrieveRetailer() {
            var fdate = $('#txtfrdate').val();
            if (fdate == '' || fdate == undefined) {
                alert('Select the From Date');
                return false;
            }
            var Tdate = $('#ttxtodate').val();
            if (Tdate == '' || Tdate == undefined) {
                alert('Select the To Date');
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Sales_by_Invoice.aspx/getRetailer",
                data: "{'Fdt':'" + fdate + "','Tdt':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    ddlretailer = JSON.parse(data.d) || [];
                    if (ddlretailer.length > 0) {
                        retailer_code = '';
                        for (var i = 0; i < ddlretailer.length; i++) {
                            retailer_code += ',' + ddlretailer[i].ListedDrCode;

                            //$('#ddl_retailer').append('<option selected value="' + ddlretailer[i].ListedDrCode + '">' + ddlretailer[i].ListedDr_Name + '</option>');
                        }
                        //$('#ddl_retailer').multiselect({
                        //    columns: 3,
                        //    placeholder: 'Select Retailer',
                        //    search: true,
                        //    searchOptions: {
                        //        'default': 'Search Retailer'
                        //    },
                        //    selectAll: true
                        //}).multiselect('reload');
                        //$('#ddl_retailer-options ul').css('column-count', '3');
                    }
                    else
                        retailer_code = '';

                    if (retailer_code == '') {
                        alert("No data found");
                        return false;
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
    </script>

    <form id="target_form" runat="server">
        <div class="container" style="width: 100%">
            <div class="row" style="font-size: 22px; text-align: left; font-weight: bolder; color: steelblue;">Sales By Retailer Invoice</div>
            <div class="form-group">
                <div class="row" style="padding-top: 10px;">
                    <label id="Label6" class="col-md-2 col-md-offset-3  control-label">
                        From Date
                    </label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input type="date" id="txtfrdate" class="form-control txtFrom" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label id="Label5" class="col-md-2 col-md-offset-3  control-label">
                        To Date
                    </label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input type="date" id="ttxtodate" class="form-control todate" />
                        </div>
                    </div>
                </div>
                <div class="row" id="divret" style="margin-top: 10px; display: none;">
                    <label id="lblret" class="col-md-2 col-md-offset-3  control-label">
                        Retailer
                    </label>
                    <div class="col-md-4 inputGroupContainer">
                        <div>
                            <select id="ddl_retailer" multiple>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-md-offset-5">
                        <a id="btnGo" class="btn btn-primary" onclick="NewWindow()"
                            style="vertical-align: middle; width: 100px">
                            <span>View</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
