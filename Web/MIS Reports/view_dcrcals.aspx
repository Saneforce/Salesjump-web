<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view_dcrcals.aspx.cs" Inherits="MIS_Reports_view_dcrcals" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head runat="server">
    <title>viewdcrcalls</title>
     <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
     <link href="../css/style.css" rel="stylesheet" />  
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }
         #grid1 {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }
         #grid2 {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }
        th {
            position: sticky;
            top: 0;
            background: #177a9e;
           font-weight: normal;
            font-size: 15px;
            color: white;
        }

        .a {
            line-height: 22px;
            padding: 3px 4px;
            border-radius: 7px;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
            
        }
        
    </style>
    </head>
<body>
    <form id="form1" runat="server">
         <div style="text-align: left; padding: 2px 50px;">
                    <b>
                         <span style="font-family: Verdana">Field Force Name :</span>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidn_sf_code" runat="server" />
                         <asp:HiddenField ID="divcd" runat="server" />
                        <asp:HiddenField ID="fdat" runat="server" />
                        <asp:HiddenField ID="tdat" runat="server" />
                                     </b>
                </div>
        <div>
            <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
     <table class="grids" id="grid">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Field Force Name</th>
                        <th>Route</th>
                        <th>Distributor</th>
                        <th>Date</th>
                        <th>Channel Name</th>
                        <th>Values</th>
                    </tr>
                </thead>
                <tbody></tbody>
          </table></div><br />
         <div><table class="grids1" id="grid1">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Field Force Name</th>
                        <th>Route</th>
                        <th>Distributor</th>
                        <th>Date</th>
                        <th>Calls Name</th>
                        <th>Values</th>
                    </tr>
                </thead>
                <tbody></tbody>
          </table></div><br />
        <div><table class="grids2" id="grid2">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Field Force Name</th>
                        <th>Route</th>
                        <th>Distributor</th>
                        <th>Date</th>
                        <th>Retailer Name</th>
                        <th>Values</th>
                    </tr>
                </thead>
                <tbody></tbody>
          </table></div>
    </form>
</body>
         <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
      <script type="text/javascript">
          var chnl = []; var cl = [];
          $(document).ready(function () {
              fillchannel();
              fillcalls();
              fillretailer();
          });
          function fillchannel() {
              var sfCodes = $('#<%=hidn_sf_code.ClientID%>').val();
              var divcodes = $('#<%=divcd.ClientID%>').val();
              var fdts = $('#<%=fdat.ClientID%>').val();
              var tdts = $('#<%=tdat.ClientID%>').val();
              $.ajax({
                  type: "Post",
                  contentType: "application/json; charset=utf-8",
                  url: "view_dcrcals.aspx/fillchannel",
                  data: "{'div':'" + divcodes + "','sf_code':'" + sfCodes + "','fdt':'" + fdts + "','tdt':'" + tdts + "'}",
                  dataType: "json",
                  success: function (data) {
                      $('#grid tbody').html('');
                      chnl = JSON.parse(data.d);
                      str = '';
                      for (var i = 0; i < chnl.length; i++) {
                         
                          str += "<tr><td>" + (i + 1) + "</td><td>" + chnl[i].Sf_Name + "</td><td>" + chnl[i].ClstrName + "</td><td>" + chnl[i].dist_name + "</td><td>" + chnl[i].DCR_Date + "</td><td>" + chnl[i].ChannelName + "</td><td>" + chnl[i].ValuesC + "</td></tr >";
                      }
                      $('#grid tbody').append(str);

                  }
              });
          };
          function fillcalls() {
              var sfCodes = $('#<%=hidn_sf_code.ClientID%>').val();
              var divcodes = $('#<%=divcd.ClientID%>').val();
              var fdts = $('#<%=fdat.ClientID%>').val();
              var tdts = $('#<%=tdat.ClientID%>').val();
              $.ajax({
                  type: "Post",
                  contentType: "application/json; charset=utf-8",
                  url: "view_dcrcals.aspx/fillcall",
                  data: "{'div':'" + divcodes + "','sf_code':'" + sfCodes + "','fdt':'" + fdts + "','tdt':'" + tdts + "'}",
                  dataType: "json",
                  success: function (data) {
                      $('#grid1 tbody').html('');
                      cl = JSON.parse(data.d);
                      str = '';
                      for (var j = 0; j < cl.length; j++) {

                          str += "<tr><td>" + (j + 1) + "</td><td>" + cl[j].Sf_Name + "</td><td>" + cl[j].ClstrName + "</td><td>" + cl[j].dist_name + "</td><td>" + cl[j].DCR_Date + "</td><td>" + cl[j].CallslName + "</td><td>" + cl[j].ValuesC + "</td></tr >";
                      }
                      $('#grid1 tbody').append(str);

                  }
              });
          };
          function fillretailer() {
              var sfCodes = $('#<%=hidn_sf_code.ClientID%>').val();
               var divcodes = $('#<%=divcd.ClientID%>').val();
               var fdts = $('#<%=fdat.ClientID%>').val();
               var tdts = $('#<%=tdat.ClientID%>').val();
              $.ajax({
                  type: "Post",
                  contentType: "application/json; charset=utf-8",
                  url: "view_dcrcals.aspx/fillretailer",
                  data: "{'div':'" + divcodes + "','sf_code':'" + sfCodes + "','fdt':'" + fdts + "','tdt':'" + tdts + "'}",
                  dataType: "json",
                  success: function (data) {
                      $('#grid2 tbody').html('');
                      rt = JSON.parse(data.d);
                      str = '';
                      for (var k = 0; k < rt.length; k++) {

                          str += "<tr><td>" + (k + 1) + "</td><td>" + rt[k].Sf_Name + "</td><td>" + rt[k].ClstrName + "</td><td>" + rt[k].dist_name + "</td><td>" + rt[k].DcrDate + "</td><td>" + rt[k].RetailersName + "</td><td>" + rt[k].Order_values + "</td></tr >";
                      }
                      $('#grid2 tbody').append(str);

                  }
              });
          };
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

                  pdf.save("depotsales.pdf");
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
              var tets = 'depotsales' + '.xls';   //create fname

              link.download = tets;
              link.href = uri + base64(format(template, ctx));
              link.click();
          });
          </script>
</html>