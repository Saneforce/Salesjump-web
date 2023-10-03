<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_detailing_report.aspx.cs" Inherits="MIS_Reports_Product_detailing_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">
     <style type="text/css">
                /*popup image*/

                .phimg img {
                    border-radius: 5px;
                    cursor: pointer;
                    transition: 0.3s;
                }
                
                .phimg img:hover {
                    opacity: 0.7;
                }
		
		      /* Caption of Modal Image (Image Text) - Same Width as the Image */
                #caption {
                    margin: auto;
                    display: block;
                    width: 80%;
                    max-width: 700px;
                    text-align: center;
                    color: #ccc;
                    padding: 10px 0;
                    height: 150px;
                }
		
		        /* Add Animation - Zoom in the Modal */
                .modal-content, #caption {
                    -webkit-animation-name: zoom;
                    -webkit-animation-duration: 0.6s;
                    animation-name: zoom;
                    animation-duration: 0.6s;
                }
                .close {
                    position: absolute;
                    top: 0px;
                    right: 3px;
                    font-size: 50px;
                    font-weight: bold;
                    transition: 0.3s;
                }

                .close:hover, .close:focus {
                    color: #bbb;
                    text-decoration: none;
                    cursor: pointer;
                }
         </style>
     <form id="form1" runat="server">
    <div class="row">
        <div class="col-sm-3">
             <label for="dt">Date:  </label>&nbsp
             </div>
        <div class="col-sm-3">
             <label for="ftname">FieldForce:  </label>&nbsp
             </div>
        <div class="col-sm-3">
              <label for="pname">Product Name:  </label>&nbsp
             </div>
    </div>
   <div class="row">
       <div class="col-sm-3">
             <%--<input type="date" id="ddldate" />--%>
           <span style="float: left; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>
            </span>
        </div>
       <div class="col-sm-3" style="margin-bottom: 1rem;">
            
                <select id="ddlff"> 
                    <option value="">Select FieldForce</option>
                </select>
        </div>
         <div class="col-sm-3" style="margin-bottom: 1rem;">
               
                <select id="ddlpn">
                    <option value="">Select Product</option>
                </select>
            </div>
        <div class="col-sm-3" style="margin-bottom: 1rem;">
            <%--<button id="btnview" type="button" class="btn btn-primary" style="vertical-align: middle; background-color: #496a9a">View</button>--%>
             <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png" Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; " OnClick="ExportToExcel" /> 
            </div>
      
       </div>
    <br />
          <div class="modal fade" id="RouteModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 80% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="RouteModalLabel"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="bindpopup"></div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    <asp:Panel ID="pnlbutton" runat="server">
    <main class="main">
       <div class="bindcells"></div>
		</main>
         </asp:Panel>
    </form>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
     <script type="text/javascript">
         var fdt = ''; var tdt = ''; var prodetailing = []; var alldata = [];
         $(document).ready(function () {
             fillsf();
             fillprodnm();
             $('#ddlff').on("change", function () {
                 var dt = $('#ddldate').val();
                 Detailing();
             });
             $('#ddlpn').on("change", function () {
                 var pd = $('#ddlpn').val();
                 Detailing();
             });
             $(document).on('click', '.view', function () {
                 $('#RouteModal').modal('toggle');
                 var route_C = $(this).closest('div').attr('grpcode');
                 fillViews(route_C);
             });
         });

         $("#reportrange").on("DOMSubtreeModified", function () {
             id = $('#ordDate').text();
             id = id.split('-');
             fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
             tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
             var Fdate = new Date(fdt);
             var fyear = Fdate.getFullYear();
             var Tdate = new Date(tdt);
             var tyear = Tdate.getFullYear();
             Detailing();
         });

         $(function () {

             var start = moment();
             var end = moment();

             function cb(start, end) {
                 $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));

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

         function Detailing() {
             var ff = $('#ddlff').val();
             var pd = $('#ddlpn').val();
             if (fdt == '' && tdt == '') {
                 alert("Select Date");
                 return false;
             }
             if (ff != '') {
                 $.ajax({
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     async: false,
                     url: "Product_detailing_report.aspx/getDetailing",
                     data: "{'fdate':'" + fdt + "','tdate':'" + tdt + "','ffnam':'" + ff + "','pnam':'" + pd + "'}",
                     dataType: "json",
                     success: function (data) {
                         prodetailing = JSON.parse(data.d) || [];
                         if (pd != '') {
                             prodetailing = prodetailing.filter(function (a) {
                                 return a.GroupID == pd;
                             });
                         }
                         var di = $('.bindcells'), str = "", str1 = "", slno = 0;
                         $(di).empty();
                         if (prodetailing.length > 0) {
                             for (var i = 0; i < prodetailing.length; i++) {

                                 if (prodetailing[i].row_num == '1') {

                                     var htm = '<div class="row"><div class="column"><div class="view" grpcode=' + prodetailing[i].GroupID + ' sfcode=' + prodetailing[i].SF_Code + ' dat=' + prodetailing[i].ActivityDate + '><strong>' + prodetailing[i].GroupName + '</strong> - <a href=#>View</a></div><div class="card"><table class="table table-hover" id="detailingtbl' + (++slno) + '"> <thead class="text-warning"></thead><tbody></tbody></table></div></div></div>';
                                     $(di).append(htm);
                                     str = '<th>Date</th><th>FieldForce Name</th><th>Route</th><th>Retailer Name</th><th>StartTime</th><th>EndTime</th><th>TotalTime</th><th>Remarks</th><th>OrderValue</th>';
                                     $('#detailingtbl' + (slno) + ' thead').append('<tr>' + str + '</tr>');
                                     str = "";
                                     str1 = '<td>' + prodetailing[i].ActivityDate + '</td><td>' + prodetailing[i].Sf_Name1 + '</td><td>' + prodetailing[i].Territory_Name + '</td><td>' + prodetailing[i].RetailerName + '</td><td>' + prodetailing[i].GPStartTime + '</td><td>' + prodetailing[i].GPEndTime + '</td><td>' + prodetailing[i].GPSpendTime + '</td><td>' + prodetailing[i].GPFeedbk + '</td><td>' + prodetailing[i].value + '</td>';
                                     $('#detailingtbl' + (slno) + ' tbody').append('<tr>' + str1 + '</tr>');
                                     str1 = "";
                                 }
                                 if (prodetailing[i].row_num != '1') {
                                     str1 = '<td>' + prodetailing[i].ActivityDate + '</td><td>' + prodetailing[i].Sf_Name1 + '</td><td>' + prodetailing[i].Territory_Name + '</td><td>' + prodetailing[i].RetailerName + '</td><td>' + prodetailing[i].GPStartTime + '</td><td>' + prodetailing[i].GPEndTime + '</td><td>' + prodetailing[i].GPSpendTime + '</td><td>' + prodetailing[i].GPFeedbk + '</td><td>' + prodetailing[i].value + '</td>';
                                     $('#detailingtbl' + (slno) + ' tbody').append('<tr>' + str1 + '</tr>');
                                     str1 = "";
                                 }

                             }

                         }
                         else {
                             alert("No Records Found...");
                         }
                     },
                     error: function (result) {
                     }
                 });
             }
         }
        
         function fillViews(grpcd) {
             var dt = $('#ddldate').val();
             var ff = $('#ddlff').val();
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Product_detailing_report.aspx/detailing_view",
                 data: "{'grpcode':'" + grpcd + "','fdat':'" + fdt + "','tdat':'" + tdt + "','sfcode':'" + ff+"'}",
                        dataType: "json",
                        success: function (data) {
                            var det = JSON.parse(data.d) || [];
                            var di = $('.bindpopup'), str = "", str1 = "";
                            $(di).empty();
                            var htm = '<table id="viw" style="width: 100%; font-size: 12px;"> <thead class="text-warning"></thead><tbody></tbody></table>';
                            $(di).append(htm);
                            str = '<th>File</th><th>SlideName</th><th>FieldForce Name</th><th>Date</th><th>StartTime</th><th>EndTime</th><th>TotalTime</th>';
                            $('#viw thead').append('<tr>' + str + '</tr>');
                            for (var i = 0; i < det.length; i++) {
                                if (det[i].row_num == '1') {
                                    str1 = ""; 
                                    str1 = '<td rowspan="' + det[i].max_value +'"><div id="fileViewer' + i + '"></div></td><td>' + det[i].SlideName + '</td><td>' + det[i].Sf_Name +'</td><td>' + det[i].ActivityDate +'</td><td>' + det[i].StartTime + '</td><td>' + det[i].EndTime + '</td><td>' + det[i].SpendTime + '</td>';
                                    $('#viw tbody').append('<tr>' + str1 + '</tr>');
                                    openFileInModal(getFileType(det[i].SlideName), "../../../../MasterFiles/Files/Detailing/" + det[i].SlideName + "",i);
                                }
                                else {
                                    str1 = "";
                                    str1 = '<td>' + det[i].SlideName + '</td><td>' + det[i].Sf_Name +'</td><td>' + det[i].ActivityDate +'</td><td>' + det[i].StartTime + '</td><td>' + det[i].EndTime + '</td><td>' + det[i].SpendTime + '</td>';
                                    $('#viw tbody').append('<tr>' + str1 + '</tr>');
                                }
                            }
                        }
                    });
         }
         function getFileType(fileName) {
             var extension = fileName.split('.').pop().toLowerCase();

             switch (extension) {
                 case 'jpg':
                 case 'jpeg':
                 case 'png':
                 case 'gif':
                     return 'image';
                 case 'mp4':
                 case 'avi':
                 case 'mkv':
                     return 'video';
                 case 'mp3':
                 case 'wav':
                 case 'ogg':
                     return 'audio';
                 case 'pdf':
                     return 'pdf';
                 case 'doc':
                 case 'docx':
                 case 'txt':
                     return 'doc';
                 default:
                     return 'unknown';
             }
         }
         function openFileInModal(fileType, filePath,id) {
             var content = "";
             var fileViewer = document.getElementById("fileViewer"+id);
             if (fileType === "image") {
                 content = `<img width="160" height="130" src="${filePath}" alt="Image" />`;
             } else if (fileType === "video") {
                 content = `<video width="180" height="120" controls><source src="${filePath}" type="video/mp4">Your browser does not support the video tag.</video>`;
             } else if (fileType === "audio") {
                 content = `<audio width="320" height="240" controls><source src="${filePath}" type="audio/mp3">Your browser does not support the audio tag.</audio>`;
             } else if (fileType === "pdf") {
                 content = `<iframe width="320" height="240" src="${filePath}" frameborder="0" width="100%" height="100%"></iframe>`;
             } else if (fileType === "doc") {
                 content = `<iframe width="320" height="240" src="https://view.officeapps.live.com/op/embed.aspx?src=${encodeURIComponent(filePath)}" frameborder="0" width="100%" height="100%"></iframe>`;
             }

             fileViewer.innerHTML = content;
         }
         function fillsf() {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Product_detailing_report.aspx/getFieldForce",
                 data: "{'Div':'<%=Session["div_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var sfdets = JSON.parse(data.d) || [];
                    var ddsf = $('#ddlff');
                    $('#ddlff').selectpicker('destroy');
                    ddsf.empty().append('<option value="">Select Employee</option>');
                    for (var i = 0; i < sfdets.length; i++) {
                        ddsf.append($('<option value="' + sfdets[i].Sf_Code + '">' + sfdets[i].Sf_Name + '</option>'));
                    }
                },
                error: function (result) {
                }
             });
             $('#ddlff').selectpicker({
                 liveSearch: true
             });
         }
         function fillprodnm() {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Product_detailing_report.aspx/getProductMaster",
                 data: "{'Div':'<%=Session["div_Code"]%>'}",
                 dataType: "json",
                 success: function (data) {
                     var prod = JSON.parse(data.d) || [];
                     var ddpd = $('#ddlpn');
                     $('#ddlpn').selectpicker('destroy');
                     ddpd.empty().append('<option value="">Select Product</option>');
                     for (var i = 0; i < prod.length; i++) {
                         ddpd.append($('<option value="' + prod[i].Product_Detail_Code + '">' + prod[i].Product_Detail_Name + '</option>'));
                     }
                 },
                 error: function (result) {
                 }
             });
             $('#ddlpn').selectpicker({
                 liveSearch: true
             });
         }
         </script>
</asp:Content>

