<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Monthly_POB_Summary.aspx.cs" Inherits="MIS_Reports_Monthly_POB_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <form id="form1" runat="server" style="height:550px;">
        
			<div class="row">
            <div class="col-lg-12 sub-header">
                Monthly POB Summary Report    
            </div>
        </div>
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport"/>
           
			<div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">Year</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select data-dropup-auto="false" id="ddlFYear"></select>
                </div>
            </div>
        </div>
            
		<div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color: #1a73e8; color: white;">View</button>
            </div>
        </div>
        <div class="tableclass" style="padding-top: 10px;">
                <table class="table table-hover orden" id="OrderEntry" style="border-top: inset;border-left:inset;border-bottom:inset;border-right:inset;display:none">
                    <thead class="text-warning">
                        <tr>                          
                            <th style="text-align:left;background-color:lightblue;">Month</th>                             
                            <th style="text-align:left;background-color:lightblue;">POB Value</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
    </form>
    <script language="javascript" type="text/javascript">
        var year = '';
        mnthlyPob = [];
        mntharr = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
		$(document).ready(function () {
            fillTpYR();
			$('#btnview').on('click', function () {
			 ViewData();
			 });
			});
			 function fillTpYR() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Monthly_POB_Summary.aspx/BindDate",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var tpyr = $("#ddlFYear");
                        tpyr.empty().append('<option value="0">Select Year</option>');
                        for (var i = 0; i < data.d.length; i++) {
                            tpyr.append($('<option value="' + data.d[i].value + '">' + data.d[i].text + '</option>'));
                        };
                    }
                });
                $('#ddlFYear').selectpicker({
                    liveSearch: true
                });
            }
			 
        function ViewData() {
            $('#OrderEntry').show();
            year = $('#ddlFYear option:selected').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Monthly_POB_Summary.aspx/GetMnthly_Pob",
                data: "{ 'Div':'<%=Session["Division_Code"]%>', 'Year':'" + year + "'}",
                dataType: "json",
                success: function (data) {
                    mnthlyPob = JSON.parse(data.d) || [];
                    ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $("#OrderEntry tbody").html("");
            
            var rwStr = ""; 
            
            for (j = 0; j < mntharr.length; j++) {
                pobval = "0";
                for (i = 0; i < mnthlyPob.length; i++) {
                    if (mntharr[j] == mnthlyPob[i].OrderMonth) {
                        pobval = mnthlyPob[i].POBValue;
                    }
                }
                rwStr += "<tr><td>" + mntharr[j] + "</td><td>" + pobval + "</td></tr>";
            }
            $("#OrderEntry tbody").append(rwStr);
        }
      
        $('#btnExport').click(function () {

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
              htmls = document.getElementById("OrderEntry").innerHTML;

              var ctx = {
                  worksheet: 'Worksheet',
                  table: htmls
              }
              var link = document.createElement("a");
              var tets = 'MonthlyPOBSummary_' + $('#ddlFYear option:selected').val()+ '.xls';   //create fname
              link.download = tets;
              link.href = uri + base64(format(template, ctx));
              link.click();
          });
    </script>
   
</asp:Content>
