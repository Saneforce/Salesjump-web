<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="vansale_summary.aspx.cs" Inherits="MIS_Reports_vansale_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
     <form runat="server" style="height:550px;">
         <div class="row">
            <div class="col-lg-12 sub-header">
                Van S&C Report
            </div>
        </div>
    <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Manager</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" data-size="8"  id="ddlmgr"></select>
                </div>
            </div>
        </div>
      
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">FieldForce</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" data-size="8"  id="ddlff"></select>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control datePicker" id="fdate" />
                </div>
            </div>
        </div>
         <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control datePicker" id="tdate" />
                </div>
            </div>
        </div>
         <div class="row" style="margin-top: 5px">
            <div class="col-md-2  col-md-offset-5">
                <button type="button" class="btn" id="btnview" onclick="NewWindow()" style="background-color:#1a73e8;color:white;">View</button>
            </div>
          </div>
         </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({ dateFormat: 'dd/mm/yy' });
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = yyyy + '-' + mm + '-' + dd;
            $('#fdate').val(today);
            $('#tdate').val(today);
            loadMGR();
        });
        $("#ddlmgr").change(function () {
            var mgr = $('#ddlmgr').val();
            loadFieldForce(mgr);
        });
        function loadFieldForce(mgr) {
            var subdiv ='<%=Session["sub_division"]%>';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "vansale_summary.aspx/getFieldForce",
                data: '{"divcode":"<%=Session["div_code"]%>","sfcode":"' + mgr + '"}',
                dataType: "json",
                success: function (data) {
                    AllFF = JSON.parse(data.d) || [];
                    SFF = AllFF;
                    $("#ddlff").selectpicker("destroy");
                    var dept = $("#ddlff");
                    dept.empty().append('<option selected="selected" value="">Select FieldForce</option>');
                    if (SFF.length > 0) {
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
        function loadMGR() {
            var subdiv = '<%=Session["sub_division"]%>';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "vansale_summary.aspx/SalesForceListMgrGet_MgrOnly",
                data: '{}',
                dataType: "json",
                success: function (data) {
                    AllMGR = JSON.parse(data.d) || [];
                    MGR = AllMGR;
                    if (MGR.length > 0) {
                        var dept = $("#ddlmgr");
                        dept.empty().append('<option selected="selected" value="">Select Manager</option>');
                        for (var i = 0; i < MGR.length; i++) {
                            dept.append($('<option value="' + MGR[i].Sf_Code + '">' + MGR[i].Sf_Name + '</option>'))
                        }
                    }
                }
            });
            $('#ddlmgr').selectpicker({
                liveSearch: true
            });
            $("#ddlff").empty().append('<option selected="selected" value="">Select FieldForce</option>');
            $('#ddlff').selectpicker({
                liveSearch: true
            });
        }
        function NewWindow() {
            var str = $('#ddlff').val();
            var mgr = $('#ddlmgr').val();
            var mgrnm = $('#ddlmgr option:selected').text();
            var strnm = $('#ddlff option:selected').text();
            var fdate = $('#fdate').val();
            var tdate = $('#tdate').val();
            if (mgr == "") {
                alert("Please Select Manager...");
                return false;
            }

            if (str == "") {
                str = mgr;
                strnm = mgrnm;
            }
            window.open("rpt_vansale_summary.aspx?SF_Code=" + str + "&fdate=" + fdate + "&tdate=" + tdate + "&SF_Name=" + strnm, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
        </script>
</asp:Content>

