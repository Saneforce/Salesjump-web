<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="View_Username_Password.aspx.cs" Inherits="MasterFiles_View_Username_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />

    <style>
        table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
        th{
            background-color: rgba(150, 212, 212, 0.4);
        }
        tr:nth-child(even) {
  background-color: rgba(150, 212, 212, 0.4);
}
    </style>
    

    <div class="row">
       <%-- <div class="col-lg-12 sub-header" >*</div>--%>
            <div class="col-lg-12 sub-header" style="color:forestgreen"><span style="color:red">**</span>
              To View UserName & Password Please Select CompanyName from DropDown!...
            </div>
        </div>
        <br />
        <br />
       <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-2  control-label">Company</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select id="ddlcomp"  data-dropup-auto="false" data-size="5"></select>
                </div>
            </div>
        </div>
        <br />
        <br />
      <%--  <div id="table" class="card">--%>
             <table class="table table-hover" id="CompList" style="font-size: 12px">
                                <thead>
                                    <tr>
                                        <th style="text-align: left">Sl.No</th>
                                        <th style="text-align: left">Client ID</th>
                                        <th style="text-align: left">Client UserName</th>
                                        <th style="text-align: left">Client Password</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
            <%--</div>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script>
         $(document).ready(function () {
             $('#CompList').hide();
             loadCompany();
             $('#ddlcomp').on('change', function () {
                 var compdiv = $('#ddlcomp').val();
                 var comname = $('#ddlcomp :selected').text();
                 var compdbnm = $('#ddlcomp :selected').attr('dbname');
                 $.ajax({
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     async: false,
                     url: "View_Username_Password.aspx/getUSNmPassWrd",
                     data: '{"div":"' + compdiv + '","dbnm":"' + compdbnm + '","compnm":"' + comname + '"}',
                     dataType: "json",
                     success: function (data) {
                         var comp = JSON.parse(data.d) || [];
                         $('#CompList').show();
                         var str = '', slno = 1;
                         $("#CompList tbody").html("");
                         for ($i = 0; $i < comp.length; $i++) {
                             slno = $i + 1;
                             tr = $("<tr></tr>");
                             $(tr).html('<td>' + slno + '</td><td>' + comp[$i].HO_ID + '</td><td>' + comp[$i].User_Name + '</td><td>' + comp[$i].Password + '</td>');
                             $("#CompList tbody").append(tr);
                         }
                     }
                 });
             });
         });

         function loadCompany() {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "View_Username_Password.aspx/getCompanyList",
                 data: '{}',
                 dataType: "json",
                 success: function (data) {
                     AllComp = JSON.parse(data.d) || [];
                     Comp = AllComp;
                     if (Comp.length > 0) {
                         var dept = $("#ddlcomp");
                         dept.empty().append('<option selected="selected" value="">Select CompanyName</option>');
                         for (var i = 0; i < Comp.length; i++) {
                             dept.append($('<option value="' + Comp[i].Cust_DivID + '" DBname="' + Comp[i].Cust_DBName + '">' + Comp[i].Cust_Name + '</option>'))
                         }
                     }
                 }
             });
             $('#ddlcomp').selectpicker({
                 liveSearch: true
             });
         }
    </script>

</asp:Content>

