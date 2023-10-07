<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="cntrywise_wtype.aspx.cs" Inherits="MIS_Reports_cntrywise_wtype" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">

    <title></title>
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        th {
            position: sticky;
            top: 0;
            background: #177a9e;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

      table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
             text-align: left;
        }
        </style>
            <head>
    <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                            <label id="Label5" class="col-md-1 col-md-offset-4  control-label">
                                Country</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlcntry" runat="server" OnSelectedIndexChanged="ddlcntry_SelectIndexchanged"  AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
             <div class="row">
                            <label id="lblst" class="col-md-1 col-md-offset-4  control-label">
                                State</label>
                            <div class="col-md-2  inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
            <div class="row">
            <label id="lbldesig" class="col-md-1 col-md-offset-4  control-label">
                Designation</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                        <asp:DropDownList ID="Dropdesignation" runat="server"  CssClass="form-control" >
                        </asp:DropDownList>
                    </div>
                    </div>
                    </div>
            <div class="row">
            <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <input id="fdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                tabindex="1" skinid="MandTxtBox" />
                   </div>
                    </div>
                    </div>
            <div class="row">
            <label id="Label2" class="col-md-1 col-md-offset-4 control-label">
                To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <input id="tdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                tabindex="1" skinid="MandTxtBox" />
                   </div>
                    </div>
                    </div>
            <div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color:#1a73e8;color:white;">View</button>
            </div>
        </div>
            <center>
                 <div class="col-md-6  col-md-offset-5">
                      <div class="huge">
                            <asp:Label ID="Lbl_Tot_User" runat="server" Text="0"></asp:Label></div>
                        <div id="auser">
                            Active Users / Vacant</div>
                    </div>
        <div style="margin-top: 12px;">
             <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
            <div class="card">
        <div class="card-body table-responsive" id="cbd">
        <table id="grid" class="grids" style="display: none;">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Work Type Name</th>
                        <th>Count</th>
                    </tr>
                  </thead>
                <tbody></tbody>
             </table>
        </div>
    </div>
        </div>
                  </center>
        </div>
                 <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 98% !important">
                <div class="modal-content">
                    <div class="modal-header">
                            <h5 class="modal-title" id="leaveModalLabel"></h5>
                           <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal"  data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                   
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                  <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExportmd">
                                <table id="leavedets" style="width: 100%;font-size:12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">S.No</th>
                                            <th style="text-align: left">SF_Code</th>
                                            <th style="text-align: left">FieldForce Name</th>
                                            <th style="text-align: left">Division</th>
                                            <th style="text-align: left">Designation</th>
                                            <th style="text-align: left">HQ To</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
          
    </form>
</body>
          <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
     <script type="text/javascript">
         var salesn = [];
         var sales = [];
         var wtype = ''; var dtname = [];



         $(document).ready(function () {

         });

         $('#btnview').on('click', function () {
             $('#grid').show();

             var cntry = $('#<%=ddlcntry.ClientID%>').val();
             var cntryv = $('#<%=ddlcntry.ClientID%> :selected').text();
             var div =<%=Session["div_code"]%>;
             var desi = $('#<%=Dropdesignation.ClientID%>').val();
             var st = $('#<%=ddlstate.ClientID%>').val();
             var Fdt = $('#fdate').val();
             var Tdt = $('#tdate').val();
             if (cntry == 0) { alert("Select Country."); $('#ddlcntry').focus(); return false; }
             if (Fdt == 0) { alert("Select From Date."); $('#ddlcntry').focus(); return false; }
             if (Tdt == 0) { alert("Select To Date."); $('#ddlcntry').focus(); return false; }
             getdatanl();
             $.ajax({
                 type: "Post",
                 contentType: "application/json; charset=utf-8",
                 url: "cntrywise_wtype.aspx/getdata",
                 data: "{'divc':'" + div + "','contry':'" + cntry + "','desgn':'" + desi + "','fdate':'" + Fdt + "','tdate':'" + Tdt + "','state':'" + st + "'}",
                 dataType: "json",
                 success: function (data) {
                     $('#grid tbody').html('');
                     sales = JSON.parse(data.d);
                     var str = '';
                     var strs = '';
                     for (var i = 0; i < sales.length; i++) {
                        

                         str += "<tr><td>" + (i + 1) + "</td><td>" + sales[i].wtype + "</td><td><a href='#' onclick='sfleave(\"" + sales[i].wtype + "\")'>" + sales[i].cnt + "<a/></td></tr>";

                     }
                     strs = "<tr><td></td><td>Not Login</td><td><a href='#' onclick='sfleave(\"" + 0 + "\")'>" + salesn[0].notlogin + "<a/></td></tr >";

                     $('#grid tbody').append(str);
                     $('#grid tbody').append(strs);

                 }
             });

         });



         function getdatanl() {
             var cntry = $('#<%=ddlcntry.ClientID%>').val();
             var div =<%=Session["div_code"]%>;
             var desi = $('#<%=Dropdesignation.ClientID%>').val();
             var st = $('#<%=ddlstate.ClientID%>').val();
             var Fdt = $('#fdate').val();
             var Tdt = $('#tdate').val();
             $.ajax({
                 type: "Post",
                 contentType: "application/json; charset=utf-8",
                 url: "cntrywise_wtype.aspx/getdatanl",
                 data: "{'divc':'" + div + "','contry':'" + cntry + "','desgn':'" + desi + "','fdate':'" + Fdt + "','tdate':'" + Tdt + "','state':'" + st + "'}",
                 dataType: "json",
                 success: function (data) {
                     salesn = JSON.parse(data.d);
                 }
             })
         }
         function sfleave(lsfc) {
             dtname = lsfc ;
             $('#summaryModal').modal('hide');
             var cntr = $('#<%=ddlcntry.ClientID%>').val();
             var div =<%=Session["div_code"]%>;
             var desn = $('#<%=Dropdesignation.ClientID%>').val();
             var st = $('#<%=ddlstate.ClientID%>').val();
             var Fdt = $('#fdate').val();
             var Tdt = $('#tdate').val();
             $("#leaveModal .modal-title").html("Details for " + lsfc + ' from ' + Fdt + ' to ' + Tdt);
             $('#leaveModal').modal('toggle');
             $('#leavedets TBODY').html("<tr><td colspan=10>Loading please wait...</td></tr>");
             setTimeout(function () {
                 if (lsfc == "0") {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         url: "cntrywise_wtype.aspx/getdatanlsf",
                         data: "{'divc':'" + div + "','contry':'" + cntr + "','desgn':'" + desn + "','fdate':'" + Fdt + "','tdate':'" + Tdt + "','wtps':'" + lsfc + "','state':'" + st + "'}",
                         dataType: "json",
                         success: function (data) {
                             AllOrders3 = JSON.parse(data.d) || [];
                             $('#leavedets TBODY').html("");
                             var slno = 0;
                             for ($j = 0; $j < AllOrders3.length; $j++) {
                                 if (AllOrders3.length > 0) {
                                     slno += 1;
                                     tr = $("<tr></tr>");
                                     $(tr).html("<td>" + slno + "</td><td>" + AllOrders3[$j].sf_code + "</td><td>" + AllOrders3[$j].Sf_Name + "</td><td>" + AllOrders3[$j].subdivision_name + "</td><td>" + AllOrders3[$j].sf_Designation_Short_Name + "</td><td>" + AllOrders3[$j].sf_hq + "</td>");
                                     $("#leavedets TBODY").append(tr);

                                 }
                             }
                         },
                         error: function (result) {
                             $('#leavedets TBODY').html("<tr><td colspan=14>Something went wrong. Try again.</td></tr>");
                             //alert(JSON.stringify(result));
                         }
                     })
                 }
                 else {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         url: "cntrywise_wtype.aspx/getdatasf",
                         data: "{'divc':'" + div + "','contry':'" + cntr + "','desgn':'" + desn + "','fdate':'" + Fdt + "','tdate':'" + Tdt + "','wtps':'" + lsfc + "','state':'" + st + "'}",
                         dataType: "json",
                         success: function (data) {
                             AllOrders2 = JSON.parse(data.d) || [];
                             $('#leavedets TBODY').html("");
                             var slno = 0;
                             for ($i = 0; $i < AllOrders2.length; $i++) {
                                 if (AllOrders2.length > 0) {
                                     slno += 1;
                                     tr = $("<tr></tr>");
                                     $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].sf_code + "</td><td>" + AllOrders2[$i].Sf_Name + "</td><td>" + AllOrders2[$i].subdivision_name + "</td><td>" + AllOrders2[$i].sf_Designation_Short_Name + "</td><td>" + AllOrders2[$i].sf_hq + "</td>");
                                     $("#leavedets TBODY").append(tr);

                                 }
                             }
                         },
                         error: function (result) {
                             $('#leavedets TBODY').html("<tr><td colspan=14>Something went wrong. Try again.</td></tr>");
                             //alert(JSON.stringify(result));
                         }
                     })
                 }
             }, 500);
         }
         $('#btnpdf').click(function () {

             var HTML_Width = $(".grids").width();
             var HTML_Height = $(".grids").height();
             var top_left_margin = 15;
             var PDF_Width = HTML_Width + (top_left_margin * 2);
             var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
             var canvas_image_width = HTML_Width;
             var canvas_image_height = HTML_Height;

             var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


             html2canvas($(".grids")[0], { allowTaint: true }).then(function (canvas) {
                 canvas.getContext('2d');

                 console.log(canvas.height + "  " + canvas.width);


                 var imgData = canvas.toDataURL("image/jpeg", 1.0);
                 var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                 pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                 for (var i = 1; i <= totalPDFPages; i++) {
                     pdf.addPage(PDF_Width, PDF_Height);
                     pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                 }

                 pdf.save("Worktypecount.pdf");
             });
         });
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
             htmls = document.getElementById("grid").innerHTML;


             var ctx = {
                 worksheet: 'Worksheet',
                 table: htmls
             }
             var link = document.createElement("a");
             var tets = 'Worktypecount' + '.xls';   //create fname 

             link.download = tets;
             link.href = uri + base64(format(template, ctx));
             link.click();
         });
         $('#btnExportmd').click(function () {

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
             htmls = document.getElementById("leavedets").innerHTML;


             var ctx = {
                 worksheet: 'Worksheet',
                 table: htmls
             }
             var link = document.createElement("a");
             if (dtname == 0) {
                 var txt = 'NotLogin_Details';
             }
             else {
                 var txt = dtname + '_Details';
             }
            var tets = txt + '.xls';    //create fname

             link.download = tets;
             link.href = uri + base64(format(template, ctx));
             link.click();
         });
     </script>
</html>
    </asp:Content>
