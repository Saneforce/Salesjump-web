<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="dcrcalls.aspx.cs" Inherits="MIS_Reports_dcrcalls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
	</head>
	<body>
    <form runat="server" style="height:550px;">
      <div class="row">
            <div class="col-lg-12 sub-header">
               
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Division</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select id="ddldiv"  data-dropup-auto="false" data-size="5" ></select>
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
                    <input type="date" class="form-control" id="fdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="tdate"/>
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
       var AllDiv = [], AllState = [], AllFF = [];
       var Div = [], States = [], SFF = [];
       $(document).ready(function () {
           loadDivision();
           loadFieldForce();
           $('#ddldiv').on('change', function () {
               FFilter();
           });
           $('#ddlstates').on('change', function () {
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
               var url = '';

               url = "new_view_dcrcals.aspx?sf_code=" + sfcode + "&div_code=" + <%=Session["div_code"]%> + "&subdiv=" + subdiv +
                   "&Sf_Name=" + sfname + "&FromDate=" + fdate + "&ToDate=" + tdate;

               window.open(url, 'poprpExpense1', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
           });
       });
       $('#fdate').on('change', function () {
           fhandler();
       });
       $('#tdate').on('change', function () {
           handler();
       });
       function fhandler() {
           var fdate = $('#fdate').val();
           let today = new Date().toISOString().slice(0, 10)
           if (fdate > today) {
               alert("From Date should be less than current date");
               document.getElementById("fdate").value = "";
               return false;
           }
       }
       function handler() {
           var fdate = $('#fdate').val();
           var tdate = $('#tdate').val();
           let today = new Date().toISOString().slice(0, 10)
           if (fdate == "") {
               alert('Please Select From Date');
               document.getElementById("tdate").value = "";
               return false;
           }
           if (tdate < fdate ) {
               alert("To date should be Greater than From date");
               document.getElementById("tdate").value = "";
               return false;
           }
           if (tdate > today) {
               alert("To Date should be less than current date");
               document.getElementById("tdate").value = "";
               return false;
           }

       }
       function loadDivision(ssdiv) {
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               async: false,
               url: "dcrcalls.aspx/getDivision",
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
               url: "dcrcalls.aspx/getFieldForce",
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
       function FFilter() {
           var cstate = $("#ddlstates").val() || 0;
           var cdiv = $("#ddldiv").val() || 0;
           $('#ddlff').selectpicker('destroy');
           var filsf = [];
           if (cstate == 0) {
               filsf = AllFF.filter(function (a) {
                   return ((',' + (a.subdivision_code)).indexOf(',' + cdiv + ',')) > -1
               });
           }
           else if (cdiv == 0) {
               filsf = AllFF.filter(function (a) {
                   return a.State_Code == cstate;
               });
           }
           else if (cdiv != 0 && cstate != 0) {
               filsf = AllFF.filter(function (a) {
                   return (a.State_Code == cstate) && (((',' + (a.subdivision_code)).indexOf(',' + cdiv + ',')) > -1);
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
 </script>
</body>
</html>
</asp:Content>