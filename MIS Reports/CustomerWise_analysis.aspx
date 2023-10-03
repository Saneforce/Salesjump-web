<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="CustomerWise_analysis.aspx.cs" Inherits="MIS_Reports_CustomerWise_analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-bottom: 10px">
        <div class="col-lg-12 sub-header">
            Customer Wise Analysis<span style="float: right; margin-right: 15px;">
                <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;               
                        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2  col-md-offset-3  control-label" style="margin-bottom: 1rem;">
            <label>Division :</label>
        </div>
        <div class="col-md-5" style="margin-bottom: 1rem;">
            <select id="ddldiv" style="width: 250px;" class="form-control">
                <option selected="selected">Select Division</option>
            </select>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2  col-md-offset-3  control-label" style="margin-bottom: 1rem;">
            <label>Manager :</label>
        </div>
        <div class="col-md-5" style="margin-bottom: 1rem;">
            <select id="ddlRptSf" style="width: 250px;" class="form-control">
                <option selected="selected">Select Manager</option>
            </select>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2  col-md-offset-3  control-label" style="margin-bottom: 1rem;">
            <label>Catagory :</label>
        </div>
        <div class="col-md-5" style="margin-bottom: 1rem;">
            <select id="ddlcat" style="width: 250px;" class="form-control" multiple>
            </select>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6" style="text-align: end;">
            <input type="button" value="View" class="btn btn-primary" id="vBtn" />
        </div>
    </div>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script type="text/javascript">
        
        $(document).ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDT = id[2].trim() + '-' + id[1] + '-' + id[0];
                TDT = id[5] + '-' + id[4] + '-' + id[3].trim();
                //$('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");                               
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "CustomerWise_analysis.aspx/GetDivision",
                dataType: "json",
                success: function (data) {
                    var sdiv = JSON.parse(data.d) || [];
                    var st = $("#ddldiv");
                    st.empty().append('<option selected="selected" value="">Nothing Selected</option>');
                    if (sdiv.length > 0) {
                        for (var i = 0; i < sdiv.length; i++) {
                            st.append($('<option value="' + sdiv[i].subdivision_code + '">' + sdiv[i].subdivision_name + '</option>'));
                        }
                    }
                    $("#ddldiv").selectpicker('reload');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "CustomerWise_analysis.aspx/GetMgr",
                dataType: "json",
                success: function (data) {
                    var Allsf_MGR = JSON.parse(data.d) || [];
                    var st = $("#ddlRptSf");
                    st.empty().append('<option selected="selected" value="">Nothing Selected</option>');
                    if (Allsf_MGR.length > 0) {
                        for (var i = 0; i < Allsf_MGR.length; i++) {
                            st.append($('<option value="' + Allsf_MGR[i].Sf_Code + '">' + Allsf_MGR[i].Sf_Name + '</option>'));
                        }
                    }
                    $("#ddlRptSf").selectpicker({
                        liveSearch: true
                    });
                    $("#ddlRptSf").selectpicker('reload');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "CustomerWise_analysis.aspx/GetCat",
                dataType: "json",
                success: function (data) {
                    var AllCat = JSON.parse(data.d) || [];
                    var st = $("#ddlcat");
                    st.empty().append('<option disabled value="">Nothing Selected</option>');
                    if (AllCat.length > 0) {
                        for (var i = 0; i < AllCat.length; i++) {
                            st.append($('<option value="' + AllCat[i].Product_Cat_Code + '">' + AllCat[i].Product_Cat_SName + '</option>'));
                        }
                    }
                    $("#ddlcat").selectpicker({
                        liveSearch: true
                    });
                    $("#ddlcat").selectpicker('reload');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $("#vBtn").on('click', function () {
                var Subdiv = $("#ddldiv").val();
                //var state = $("#ddlst").val();
                var MGR = $("#ddlRptSf").val();
                <%--var SF = $("#ddlso").val();
                var Prd_Nm = $("#ddlprd_nm").val();
                var Prd_Grp = $("#ddlprd_grp").val();--%>
                var Prd_Cat = $("#ddlcat").val();
                var frmdt = $("#frmdt").val(); var todt = $("#todt").val();
                var URL = "rptView_CustomerWise_analysis.aspx?RSf=" + MGR + "&Subdiv=" + Subdiv + "&PrdCat=" + Prd_Cat + "&Frm_Dt=" + FDT + "&To_Dt=" + TDT + "";
               
                window.open(URL, "ModalPopUp",
                    "toolbar=no," +
                    "scrollbars=yes," +
                    "location=no," +
                    "statusbar=no," +
                    "menubar=no," +
                    "addressbar=no," +
                    "resizable=yes," +
                    "width=900," +
                    "height=600," +
                    "left = 0," +
                    "top=0"
                );
                //, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
        });
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

