<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Rpt_Productwise_Secondary_Report_Qty.aspx.cs" Inherits="MasterFiles_Reports_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/lib/xls.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/xlsx.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/import_data.js" type="text/javascript"></script>
    <script src="../js/jquery.table2excel.js"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <style>
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }
    </style>
    <%--<div class="overlay" id="loadover" style="display: none;">
        <div id="loader"></div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12 sub-header">
            Productwise Secondary Report Quantity<span style="float: right; margin-right: 15px;">
                <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;               
                        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
                <img style="cursor: pointer; width: 40px; height: 40px; float: right;" alt="" onclick="exportToExcel()" src="../../img/Excel-icon.png" /></span>
        </div>

    </div>
    <br />
    <div class="row">
        <div class="col-sm-3" style="margin-bottom: 1rem;">
            <select id="ddlstate" style="width: 250px;" class="form-control">
                <option selected="selected">Select State</option>
            </select>
        </div>
        <div class="col-sm-3" style="margin-bottom: 1rem;">
            <select id="ddlclstr" style="width: 250px;" class="form-control">
                <option selected="selected">Select Cluster</option>
            </select>
        </div>
        <div class="col-sm-3" style="margin-bottom: 1rem;">
            <select id="ddldiv" style="width: 250px;" class="form-control">
                <option selected="selected">Select Division</option>
            </select>
        </div>
        <div class="col-sm-3" style="margin-bottom: 1rem;">
            <select id="ddlRptSf" style="width: 250px;" class="form-control">
                <option selected="selected">Select Manager</option>
            </select>
        </div>
        <div class="col-sm-3" style="margin-bottom: 1rem;">
            <select id="ddlSf" style="width: 250px;" class="form-control">
                <option selected="selected">Select Field Force</option>
            </select>
        </div>
    </div>
    <div class="card">
        <div class="card-body table-responsive" style="overflow: auto; max-height: 500px; padding: 0px !important">
            <table class="table table-bordered" id="tblPrdt_wise">
                <thead class="text-warning" style="position: sticky; top: 0;">
                    <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        var AllOrders = []; var Orders = []; var Invoice_details = []; var FDt = '', str = '';
        var TDt = ''; var Allstate = [], Allclustr = [], Allsf = [], AllHyrListSf = [], AllBrndName = [];
        var Brnd = [], items = [], Allsf_Suvdiv = [], GetBrndName = [], SubDiv_Brnd = [], clstr_MGR = [], st_MGR = [];
        $(document).ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDt = (id[2]).trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                TDt = id[5] + '-' + id[4] + '-' + (id[3]).trim() + ' 00:00:00';
                loadData()
                //$('#loadover').show();
                //setTimeout(function () {
                //    setTimeout();
                //}, 500);
            });
            AllHyrList();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/GetBrandName",
                dataType: "json",
                success: function (data) {
                    AllBrndName = JSON.parse(data.d) || [];
                }
            });
            Get_allbrnd();
            fillstate();
            GetDiv();
            GetMGR();
            $('#ddlstate').on('change', function () {
                var statecode = $('#ddlstate option:selected').val();
                if (statecode != '' || statecode != 'Select State') {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Rpt_Productwise_Secondary_Report_Qty.aspx/Getcluster",
                        data: "{'stateCode':'" + statecode + "'}",
                        dataType: "json",
                        success: function (data) {
                            Allclustr = JSON.parse(data.d) || [];
                            var st = $("#ddlclstr");
                            if (Allclustr.length > 0) {                                
                                st.empty().append('<option selected="selected" value="">Select Cluster</option>');
                                for (var i = 0; i < Allclustr.length; i++) {
                                    st.append($('<option value="' + Allclustr[i].Territory_code + '">' + Allclustr[i].Territory_name + '</option>'));
                                }
                            }
                            else {
                                st.empty().append('<option selected="selected" value="">No Cluster Found by State</option>');
                                alert('No Cluster Found');
                            }
                            Select_st = AllHyrListSf.filter(function (a) {
                                return a.State_Code == statecode;
                            });
                            ReloadTable(Select_st, GetBrndName);
                            st_MGR = AllMGR.filter(function (b) {
                                return b.State_Code == statecode;
                            });
                             GenerateMGR(st_MGR);
                        }
                    });
                }
                else {
                    alert("Select State");
                }
            });
            $('#ddlclstr').on('change', function () {
                var terr_code = $('#ddlclstr option:selected').val();
                if (terr_code == '' || terr_code == 'Select cluster') {
                    alert('Select the Clster');
                }
                else {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Rpt_Productwise_Secondary_Report_Qty.aspx/getsubDiv",
                        data: "{'terry_code':'" + terr_code + "'}",
                        dataType: "json",
                        success: function (data) {
                            Allsf = JSON.parse(data.d) || [];
                            var st = $("#ddldiv");
                            if (Allsf.length > 0) {
                                var filter_subdiv = [], filter_subdiv_det = [];
                                var filterval = '';
                                st.empty().append('<option selected="selected" value="">Select Division</option>');
                                for (var i = 0; i < Allsf.length; i++) {
                                    filterval = Allsf[i].subdivision_code.split(',');
                                    for (var k = 0; k < filterval.length; k++) {
                                        filter_subdiv = SubDivision.filter(function (c) {
                                            return c.subdivision_code == filterval[k];
                                        });
                                        for (var j = 0; j < filter_subdiv.length; j++) {
                                            filter_subdiv_det.push({
                                                subdivision_code: filter_subdiv[j].subdivision_code,
                                                BrandName: filter_subdiv[j].subdivision_name
                                            });
                                        }
                                    }
                                }
                                filter_subdiv_det.sort((a, b) => b.subdivision_code - a.subdivision_code);
                                var Chkstr = '';
                                for (var l = 0; l < filter_subdiv_det.length; l++) {
                                    if (filter_subdiv_det[l].BrandName != Chkstr) {
                                        Chkstr = filter_subdiv_det[l].BrandName;
                                        st.append($("<option  value=" + filter_subdiv_det[l].subdivision_code + ">" + filter_subdiv_det[l].BrandName + "</option>"));
                                    }
                                }
                            }
                            else {
                                st.empty().append('<option selected="selected" value="">No Division Found by Cluster</option>');
                                alert("No Division Found");
                            }
                            Select_trr = AllHyrListSf.filter(function (a) {
                                return a.Territory_Code == terr_code;
                            });
                            AppendHeader(GetBrndName);
                            ReloadTable(Select_trr, GetBrndName);
                            clstr_MGR = st_MGR.filter(function (a) {
                                return a.Territory_Code == terr_code;
                            });
                            GenerateMGR(clstr_MGR);
                        }
                    });
                }
            });
            $('#ddlSf').on('change', function () {
                var sfcode = $('#ddlSf option:selected').val();
                if (sfcode == '') {
                    alert("Select Field Force");
                }
                //else if (sfcode == 'All') {
                //    AllHyrList(); ReloadTable(AllHyrListSf);
                //    //ReloadTable(Orders);
                //}
                else {
                    FilterDetails(sfcode);
                }
            });
            $('#ddldiv').on('change', function () {
                var SubDiv_Code = $('#ddldiv option:selected').val();
                if (SubDiv_Code == '') {
                    alert('Select the Division');
                }
                else if (SubDiv_Code == 'All') {
                    AppendHeader(GetBrndName);
                    ReloadTable(AllHyrListSf, GetBrndName);
                }
                else {
                    //Allsf_Suvdiv = AllHyrListSf.filter(function (a) {
                    //    return a.subdivision_code.indexOf(SubDiv_Code) > -1;
                    //});
                    SubDiv_Brnd = AllBrndName.filter(function (b) {
                        return b.subdivision_code.indexOf(SubDiv_Code) > -1;
                    });
                    SubDivMGR = AllMGR.filter(function (c) {
                        return c.subdivision_code.indexOf(SubDiv_Code) > -1;
                    });
                    sub_det = AllHyrListSf.filter(function (a) {
                        return a.subdivision_code.indexOf(SubDiv_Code) > -1;
                    });
                    //var st = $("#ddlSf");
                    var str = $("#ddlRptSf");
                    str.empty().append('<option selected="selected" value="">Select Manager</option>');
                    if (SubDivMGR.length > 0) {
                        for (var i = 0; i < SubDivMGR.length; i++) {
                            str.append($('<option value="' + SubDivMGR[i].Sf_Code + '">' + SubDivMGR[i].SFNA + '</option>'));
                        }
                    }
                    else {
                        str.append('<option selected="selected" disable>No Record Found by Division</option>');
                        alert('No Data Found');
                    }
                    (SubDiv_Brnd.length > 0) ? AppendHeader(SubDiv_Brnd) : AppendHeader(GetBrndName);
                    ReloadTable(sub_det, (SubDiv_Brnd.length > 0) ? SubDiv_Brnd : GetBrndName);
                }
            });

        });
        $('#ddlRptSf').on('change', function () {
            var MGR = $('#ddlRptSf option:selected').val();
            if (MGR == '') {
                alert('Select the Manager');
            }
            else if (MGR == 'All') {
                AppendHeader(GetBrndName);
                ReloadTable(AllHyrListSf, (SubDiv_Brnd.length > 0) ? SubDiv_Brnd : GetBrndName);
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_Productwise_Secondary_Report_Qty.aspx/GetMGR_MR",
                    data: "{'SF':'" + MGR + "'}",
                    dataType: "json",
                    success: function (data) {
                        var Allsf_MGR = JSON.parse(data.d) || [];
                        var st = $("#ddlSf");                        
                        if (Allsf_MGR.length > 0) {
                            st.empty().append('<option selected="selected" value="">Select Field Force</option>');
                            for (var i = 0; i < Allsf_MGR.length; i++) {
                                st.append($('<option value="' + Allsf_MGR[i].Sf_Code + '">' + Allsf_MGR[i].SFNA + '</option>'));
                            }
                            ReloadTable(Allsf_MGR, (SubDiv_Brnd.length > 0) ? SubDiv_Brnd : GetBrndName);
                        }
                        else {
                            st.append('<option selected="selected" disable>No Record Found by Manager</option>');
                            ReloadTable('', '');
                            alert('No Data Found');
                        }
                    },
                    error: function (err) {
                        alert(err);
                    }
                });             
            }
        });
        function GetMGR() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/GetMGR",
                dataType: "json",
                success: function (data) {
                    AllMGR = JSON.parse(data.d) || [];
                    if (AllMGR.length > 0)
                        GenerateMGR(AllMGR)
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        function GenerateMGR(MGR) {
            if (MGR.length > 0) {
                var st = $("#ddlRptSf");
                st.empty().append('<option selected="selected" value="">Select Manager</option>');
                st.append('<option value="All">All</option>');
                for (var i = 0; i < MGR.length; i++) {
                    st.append($('<option value="' + MGR[i].Sf_Code + '">' + MGR[i].SFNA + '</option>'));
                }
            }
            else {
                st.append('<option selected="selected">No Record</option>')
            }
        }
        
        
        function Get_allbrnd() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/GetAllBrandName",
                dataType: "json",
                success: function (data) {
                    GetBrndName = JSON.parse(data.d) || [];

                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        function AllHyrList() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/Gethyr",
                dataType: "json",
                success: function (data) {
                    AllHyrListSf = JSON.parse(data.d) || [];
                }
            });
        }
        function FilterDetails(sf_code) {
            Filteritems = Orders.filter(function (a) {
                return a.Sf_Code == sf_code;
            });

            ReloadTable(Filteritems, (SubDiv_Brnd.length > 0) ? SubDiv_Brnd : GetBrndName);

        }
        function fillstate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/getStates",
                data: "{'divcode':'" +<%=Session["div_code"]%>+"'}",
                dataType: "json",
                success: function (data) {
                    Allstate = JSON.parse(data.d) || [];
                    var st = $("#ddlstate");
                    st.empty().append('<option selected="selected" value="">Select State</option>');
                    for (var i = 0; i < Allstate.length; i++) {
                        st.append($('<option value="' + Allstate[i].State_Code + '">' + Allstate[i].StateName + '</option>'));
                    };
                }
            });
        }
        function GetDiv() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/fillsubdivision",
                datatype: "json",
                success: function (data) {
                    SubDivision = JSON.parse(data.d) || []
                    if (SubDivision.length > 0) {
                        str = $('#ddldiv');
                        str.empty().append('<option selected="selected" value="">Select Division</option>');
                        str.append('<option value="All">All</option>');
                        for (var i = 0; i < SubDivision.length; i++) {
                            str.append($("<option  value=" + SubDivision[i].subdivision_code + ">" + SubDivision[i].subdivision_name + "</option>"));
                        }
                    }
                }
            });
        }
        function AppendHeader(BrndHeader) {
            $("#tblPrdt_wise THEAD").html("");
            Chkstr = '';
            //items = Orders.filter(function (a) {
            //    return a.BrndNo == 1;
            //});
            str = "<tr style='background-color: #37a4c6; color: #fff; white-space: nowrap;'><th style='text-align: left; color: #fff'>Field Force</th>";
            BrndHeader.sort((a, b) => b.ListedDrCode - a.ListedDrCode);
            for (var i = 0; i < BrndHeader.length; i++) {
                if (BrndHeader[i].BrandName != Chkstr) {
                    Chkstr = BrndHeader[i].BrandName;
                    str += "<th style='color: #fff'>" + BrndHeader[i].BrandName + "</th>";
                }
            }
            str += "</tr>";
            $('#tblPrdt_wise thead').append(str);
            str = '';
        }
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Productwise_Secondary_Report_Qty.aspx/GetProduct_Wise_Qty",
                data: "{'FDt':'" + FDt + "','TDt':'" + TDt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    AppendHeader(GetBrndName);
                    ReloadTable(AllHyrListSf, GetBrndName);
                },
                error: function (result) {
                }
            });
        }
        BrVal = 0
        function ReloadTable(AllHyrSf, BrndHeader) {
            slno = 0; str = ''; let Total = 0; var Reporting_to = ''; Chkstr = ''; var Chk = '';
            BrndHeader.sort((a, b) => b.ListedDrCode - a.ListedDrCode);
            $("#tblPrdt_wise TBODY").html("");
            if (AllHyrSf.length > 0) {
                AllHyrSf.sort((a, b) => b.SFNA - a.SFNA);
                for (var i = 0; i < AllHyrSf.length; i++) {
                    if (AllHyrSf[i].SFNA != Chk) {
                        Chk = AllHyrSf[i].SFNA;
                        str += "<tr  style='background-color: #" + AllHyrSf[i].Desig_Color + ";'><td>" + AllHyrSf[i].SFNA + "</td>";
                        BrndHeader.sort((a, b) => b.ListedDrCode - a.ListedDrCode);
                        for (var j = 0; j < BrndHeader.length; j++) {
                            if (BrndHeader[j].BrandName != Chkstr) {
                                Chkstr = BrndHeader[j].BrandName;
                                BrVal = 0;
                                var arr = AllHyrListSf.filter(function (itm) {
                                    return (itm.Reporting_To_SF == AllHyrSf[i].Sf_Code);
                                });
                                if (AllHyrSf.length > 1 && arr.length != 0) {
                                    Val = getValue(AllHyrSf[i].Sf_Code, BrndHeader[j].BrandName);
                                    str += "<td>" + Val + "</td>";
                                }
                                else {
                                    Brnd = Orders.filter(function (a) {
                                        return (a.Sf_Code == AllHyrSf[i].Sf_Code && a.ProductBrandName == BrndHeader[j].BrandName) //&& a.Reporting_to_Sf==AllHyrListSf[i].Reporting_To_SF);
                                    });
                                    str += ((Brnd.length > 0) ? "<td>" + Brnd[0].Quantity + "</td>" : "<td>" + 0 + "</td>");
                                }
                            }
                        }
                        str += "</tr>"
                    }
                }
                Chk = '';
            }
            else {
                str += "<td>No Data Found</td>";
            }
            $('#tblPrdt_wise TBODY').append(str);
            str = '';
        }

        function getValue(SF, BrndNm) {
            var arr = AllHyrListSf.filter(function (itm) {
                return (itm.Reporting_To_SF == SF);
            });
            for (var i = 0; i < arr.length; i++) {
                Brnd = Orders.filter(function (a) {
                    return (a.Sf_Code == arr[i].Sf_Code && a.ProductBrandName == BrndNm) //&& a.Reporting_to_Sf==AllHyrListSf[i].Reporting_To_SF);
                });
                if (Brnd.length > 0) {
                    console.log(Brnd[0].Quantity);
                    BrVal += (Brnd.length > 0) ? Brnd[0].Quantity : 0;
                }
                getValue(arr[i].Sf_Code, BrndNm);
            }
            return BrVal
        }
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                //$('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

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
        function exportToExcel() {
            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("tblPrdt_wise").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_PrdWise_Qty' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    </script>
</asp:Content>

