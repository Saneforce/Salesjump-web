<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="hqwisepob.aspx.cs" Inherits="MIS_Reports_hqwisepob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>hqwisepob</title>
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
             text-align: center;
        }
        </style>
</head>
<body>
    <form id="form1"  runat="server">
        <div>
               <div class="row">
                            <label id="Label5" class="col-md-1 col-md-offset-4  control-label">
                                State</label>
                            <div class="col-sm-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server"  AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
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
                
        <div style="margin-top: 12px;">
             <img  style="cursor: pointer;float:right;" src="/limg/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
            <div class="card">
        <div class="card-body table-responsive" id="cbd" style="display: none;">
        <table id="grid" class="grids" style="display: none;">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>HQ Name</th>
                        <th>Order Value</th>
                    </tr>
                  </thead>
                <tbody></tbody>
             </table>
        </div>
    </div>
        </div></center>
        </div>
    </form>
</body>
           <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
     <script type="text/javascript">
         var salesn = [];
         var sales = [];
     $(document).ready(function () {
         
     });
         
         $('#btnview').on('click', function () {
             $('#grid').show(); 
             $('#grid tbody').html('');
            var state = $('#<%=ddlstate.ClientID%>').val();
             var statev = $('#<%=ddlstate.ClientID%> :selected').text();
             var div =<%=Session["div_code"]%>;
             var Fdt = $('#fdate').val();
             var Tdt = $('#tdate').val();
             
             if (Fdt == 0) { alert("Select From Date."); $('#ddlcntry').focus(); return false; }
             if (Tdt == 0) { alert("Select To Date."); $('#ddlcntry').focus(); return false; }
         $.ajax({
             type: "Post",
             contentType: "application/json; charset=utf-8",
             url: "hqwisepob.aspx/getdata",
             data: "{'divc':'" + div + "','statev':'" + state + "','fdate':'" + Fdt + "','tdate':'" + Tdt + "'}",
             dataType: "json",
             success: function (data) {
                 sales = JSON.parse(data.d);
                 $('#cbd').show();
                 str = '';
                 strs=''
                 for (var i = 0; i < sales.length; i++) {

                    
                     str += "<tr><td>" + (i + 1) + "</td><td>" + sales[i].HQ + "</td><td>" + sales[i].Order_Value + "</td></tr>";
                   }
                 
                 $('#grid tbody').append(str);
              }
         });
       
         });

        
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

                 pdf.save("HQwise_POB.pdf");
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
             var tets = 'HQwise_POB' + '.xls';   //create fname

             link.download = tets;
             link.href = uri + base64(format(template, ctx));
             link.click();
         });
        
     </script>
</html>
</asp:Content>