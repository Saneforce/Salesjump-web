
<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailer_rpt_Outlet.aspx.cs" Inherits="MIS_Reports_Retailer_rpt_Outlet" %>

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

        table {
            table-layout: fixed;
            width: 20em;
        }

        td {
            width: 4em;
            height: 1.5em;
            text-align: center;
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
    <div class="overlay" id="loadover" style="display: none;">
        <div id="loader"></div>
    </div>
    <div class="row">
        <div class="col-lg-12 sub-header">
           Outlet / Retailer Report<span style="float: right; margin-right: 15px;">
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
            <table class="table table-bordered" border="1" id="tblRet_wise">
                <thead class="text-warning" style="position: sticky; top: 0;">
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        var AllHyrListSf = [], AllOrders = [], Orders = [];
        var Allclustr = [], Allsf = [], Allsf_Suvdiv = [], Allstate = [], SubDivision = [], AllMGR = [];
        var RetCnt = []; var styl = '', trstyl = '', str = '', MnVal = 0;
        const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        $(document).ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDt = (id[2]).trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                TDt = id[5] + '-' + id[4] + '-' + (id[3]).trim() + ' 00:00:00';
                FMn = id[1];
                TMn = id[4];
                $('#loadover').show();
                setTimeout(function () {
                    setTimeout(loadData(), 500);
                }, 500);
            });
            AllHyrList();
            fillstate();GetDiv();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_rpt_Outlet.aspx/Retailer_Cnt",
                dataType: "json",
                success: function (data) {
                    RetCnt = JSON.parse(data.d) || [];
                },
                error: function (res) {
                    alert(res);
                }
            });
            $('#ddlstate').on('change', function () {
                var statecode = $('#ddlstate option:selected').val();
                if (statecode != '' || statecode != 'Select State') {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_rpt_Outlet.aspx/Getcluster",
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
                                st.empty().append('<option selected="selected" >No Cluster Found By State</option>');
                                 alert("No Cluster Found By State");
                            }
                            Select_st = AllHyrListSf.filter(function (a) {
                                return a.State_Code == statecode;
                            });
                            ReloadTable(Select_st);
                        },
                        error: function (Res) {
                            alert(Res);
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
                        url: "Retailer_rpt_Outlet.aspx/getsubDiv",
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
                                                subdivision_name: filter_subdiv[j].subdivision_name
                                            });
                                        }
                                    }
                                }
                                filter_subdiv_det.sort((a, b) => b.subdivision_code - a.subdivision_code);
                                var Chkstr = '';
                                for (var l = 0; l < filter_subdiv_det.length; l++) {
                                    if (filter_subdiv_det[l].subdivision_name != Chkstr) {
                                        Chkstr = filter_subdiv_det[l].subdivision_name;
                                        st.append($("<option  value=" + filter_subdiv_det[l].subdivision_code + ">" + filter_subdiv_det[l].subdivision_name + "</option>"));
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
                            ReloadTable(Select_trr);
                        },
                        error: function (Res) {
                            alert(Res);
                        }
                    })
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
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_rpt_Outlet.aspx/GetMGR",
                dataType: "json",
                success: function (data) {
                    AllMGR = JSON.parse(data.d) || [];
                    var st = $("#ddlRptSf");
                    if (AllMGR.length > 0) {
                        st.empty().append('<option selected="selected" value="">Select Manager</option>');
                        //st.append('<option value="All">All</option>');
                        for (var i = 0; i < AllMGR.length; i++) {
                            st.append($('<option value="' + AllMGR[i].Sf_Code + '">' + AllMGR[i].SFNA + '</option>'));
                        }
                    }
                    else {
                        st.empty().append('<option selected="selected">No Record Found</option>');
                        alert("No Data Found");
                    }
                },
                error: function (err) {
                    alert(err);
                }
            });           

        });
        $('#ddlRptSf').on('change', function () {
            var MGR = $('#ddlRptSf option:selected').val();
            if (MGR == '') {
                alert('Select the Manager');
            }
            else {
                Allsf_MGR = AllHyrListSf.filter(function (a) {
                    return a.Reporting_To_SF == MGR;
                });
                var st = $("#ddlSf");
                st.empty().append('<option selected="selected" value="">Select Field Force</option>');
                if (Allsf_MGR.length > 0) {
                    //st.append('<option value="All">All</option>');
                    for (var i = 0; i < Allsf_MGR.length; i++) {
                        st.append($('<option value="' + Allsf_MGR[i].Sf_Code + '">' + Allsf_MGR[i].Sf_Name + '</option>'));
                    }
                }
                else {
                    st.empty().append('<option selected="selected" disable>No Record Found By Manager</option>');
                    alert('No Record Found');
                }
                ReloadTable(Allsf_MGR);
            }
        });
        $('#ddldiv').on('change', function () {
            var SubDiv_Code = $('#ddldiv option:selected').val();
            if (SubDiv_Code == '') {
                alert('Select the Division');
            }
            else {
                SubDivMGR = AllMGR.filter(function (c) {
                    return c.subdivision_code.indexOf(SubDiv_Code) > -1;
                });

                var str = $("#ddlRptSf");
                str.empty().append('<option selected="selected" value="">Select Manager</option>');
                if (SubDivMGR.length > 0) {
                    for (var i = 0; i < SubDivMGR.length; i++) {
                        str.append($('<option value="' + SubDivMGR[i].Sf_Code + '">' + SubDivMGR[i].SFNA + '</option>'));
                    }
                }
                else {
                    str.append('<option selected="selected" disable>No Record Found by Division</option>');
                    alert('No Record Found');
                }
                sub_det = AllHyrListSf.filter(function (a) {
                    return a.subdivision_code.indexOf(SubDiv_Code) > -1;
                });
                ReloadTable(sub_det);
            }
        });
        function FilterDetails(sf_code) {
            Filteritems = AllHyrListSf.filter(function (a) {
                return a.Sf_Code == sf_code;
            });
            ReloadTable(Filteritems);
        }
        function fillstate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_rpt_Outlet.aspx/getStates",
                data: "{'divcode':'" +<%=Session["div_code"]%>+"'}",
                dataType: "json",
                success: function (data) {
                    Allstate = JSON.parse(data.d) || [];
                    var st = $("#ddlstate");
                    st.empty().append('<option selected="selected" value="">Select State</option>');
                    for (var i = 0; i < Allstate.length; i++) {
                        st.append($('<option value="' + Allstate[i].State_Code + '">' + Allstate[i].StateName + '</option>'));
                    };
                },
                error: function (Res) {
                    alert(Res);
                }
            });
        }
        function GetDiv() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Retailer_rpt_Outlet.aspx/fillsubdivision",
                datatype: "json",
                success: function (data) {
                    SubDivision = JSON.parse(data.d) || []
                    if (SubDivision.length > 0) {
                        str = $('#ddldiv');
                        str.empty().append('<option selected="selected" value="">Select Division</option>');
                        for (var i = 0; i < SubDivision.length; i++) {
                            str.append($("<option  value=" + SubDivision[i].subdivision_code + ">" + SubDivision[i].subdivision_name + "</option>"));
                        }
                    }
                    else {
                        $('#ddldiv').append('<option selected="selected" >No Division Found</option>');
                    }
                },
                error: function (Res) {
                    alert(Res);
                }
            });
        }
        function loadData() {
            str = '';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_rpt_Outlet.aspx/GetRetailerUPC_Cnt",
                data: "{'Fdt':'" + FDt + "','Tdt':'" + TDt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    $("#tblRet_wise THEAD").html("");
                    var strt = monthNames[parseInt(FMn, 10) - 1];
                    var end = monthNames[parseInt(TMn, 10) - 1];
                    styl = "style='color: #fff'";
                    trstyl = "style='background-color: #37a4c6; color: #fff; white-space: nowrap;'";
                    str = "<tr " + trstyl + " ><th " + styl + " rowspan='2'>Field Force</th><th colspan='2' " + styl + ">" + ((strt != end) ? strt + "  -  " + end : strt) + "</th>";
                    str += "<tr><th " + trstyl + styl + ">Total Outlet</th><th " + trstyl + styl + ">UPC Count</th></tr>";
                    str += "</tr>";
                    $('#tblRet_wise thead').append(str);
                    str = '';                    
                    ReloadTable(AllHyrListSf);
                    $('#loadover').hide();
                },
                error: function (result) {
                    alert(result);
                }
            });
        }
        function AllHyrList() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_rpt_Outlet.aspx/Gethyr",
                dataType: "json",
                success: function (data) {
                    AllHyrListSf = JSON.parse(data.d) || [];
                },
                error: function (Res) {
                    alert(Res);
                }
            });
        }
        function ReloadTable(AllHyrSf) {
            str = ''; var Chkstr = '', Val = 0, Grdtotl_Outlet = 0, Grdtotl_UPC = 0;
            if (AllHyrSf.length > 0) {
                $("#tblRet_wise TBODY").html("");
                if (AllHyrSf[0].SFNA == 'Select Field Force') {
                    str = "<tr><td colspan='4' style='font-size: 25px;color: grey;'>Select Field Force</td></tr>";
                    $('#tblRet_wise TBODY').append(str);
                    return false;
                }
                for (var i = 0; i < AllHyrSf.length; i++) {
                    Val = 0, MnVal = 0;
                    str += "<tr style='background-color: #" + AllHyrSf[i].Desig_Color + ";'><td> " + AllHyrSf[i].SFNA + "</td>";
                    Sf_Retcnt = RetCnt.filter(function (a) {
                        return a.Sf_Code == AllHyrSf[i].Sf_Code;
                    })

                    $.each(Sf_Retcnt, function (key, value) {
                        Val += value.OutletCount;
                    });
                    Grdtotl_Outlet += Val;
                    str += (Val > 0) ? "<td>" + Val + "</td>" : "<td>" + 0 + "</td>";
                    MnRetCnt = Orders.filter(function (b) {
                        return b.Sf_Code == AllHyrSf[i].Sf_Code;
                    });
                    $.each(MnRetCnt, function (key, value) {
                        MnVal += value.DrCnt;
                    });
                    Grdtotl_UPC += MnVal;
                    str += (MnVal > 0) ? "<td>" + MnVal + "</td>" : "<td>" + 0 + "</td>";
                }
            }
            else {
                str = "<tr><td colspan='4' style='font-size: 25px;color: grey;'>Select Field Force</td></tr>";
            }
            str += "</tr>";
            str += "<tr><td style='background-color: black;color: #fff;'>Grand Total : </td><td>" + Grdtotl_Outlet + "</td><td>" + Grdtotl_UPC + "</td></tr>";
            $('#tblRet_wise TBODY').append(str);
            str = '';
        }
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
            htmls = document.getElementById("tblRet_wise").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_RetWise_Qty' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
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
    </script>
</asp:Content>

