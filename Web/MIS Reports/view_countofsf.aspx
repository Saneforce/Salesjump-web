<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view_countofsf.aspx.cs" Inherits="MIS_Reports_view_countofsf" %>

 <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
      <head id="Head1" runat="server">
    <title>worktypedetails</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

      <style>
             #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
           
    overflow:scroll;
        }

    th {
        position: sticky;
        top: 0;
        background: #6c7ae0;
       
        font-weight: normal;
        font-size: 15px;
        color: white;
    }
            #grid td, table th {
                padding: 5px;
                border: 1px solid #ddd;
                
            }
        </style> 
          </head>
<body>
    <form id="form1" runat="server">
       <div class="container">
            <br />
        <asp:Label ID="lblHead" Text="Retailer Field Force Wise" SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 

        <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                        <asp:HiddenField ID="wt" runat="server" />
                         <asp:HiddenField ID="sc" runat="server" />
                        <asp:HiddenField ID="dat" runat="server" />
                        <asp:HiddenField ID="sdv" runat="server" />
						 <asp:HiddenField ID="ddd" runat="server" />
                    </b>
                </div>
          <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        <table id="grid" class="grids">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Activity Date</th>
                        <th>Employee Id</th>
                        <th>Field Force Name</th>
                        <th>State Name</th>
                        <th>Reporting Manager</th>
                        <th>Mobile No</th>
                        <th>Start Time</th>
                        <th>WorkType Name</th>
                        <th>Distributor Name</th>
                         <th>Route Name</th>
                         <th>Remarks</th>
                         <th>Joint Work With</th>
                         <th>Login</th>
                        <th>Log Out</th>
                        <th>App Version</th>
                      </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table></div>
       
    </form>
       <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">
          var str = "";
          var arr = [];
          var prods = [];
          $(document).ready(function () {
             
              FillList();

          });
        function FillList() {
            wt.Value = wtp;
            sc.Value = scode;
            dat.Value = dates;
            sdv.Value = subcode;
			ddd.Value = dts;
            var wtp = $('#<%=wt.ClientID%>').val();
            var scode = $('#<%=sc.ClientID%>').val();
            var dates = $('#<%=dat.ClientID%>').val();
            var subcode = $('#<%=sdv.ClientID%>').val();
            var div =<%=Session["div_code"]%>;
			 var dts = $('#<%=ddd.ClientID%>').val();
            if (wtp == "NL") {
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "view_countofsf.aspx/getdatanlsf",
                    data: "{'wtyp':'" + wtp + "','sfc':'" + scode + "','cdate':'" + dates + "','subc':'" + subcode + "','divc':'" + div + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#grid tbody').html('');
                        salesnl = JSON.parse(data.d);
                        var str = '';

                        for (var i = 0; i < salesnl.length; i++) {

                           str += '<tr><td>' + (i + 1) + '</td><td>' + dts + '</td><td>' + salesnl[i].sf_emp_id + '</td><td>' + salesnl[i].Sf_Name + '</td><td>' + salesnl[i].StateName + '</td><td>' + salesnl[i].rsf + '</td><td>' + salesnl[i].MOb + '</td><td>' + ((salesnl[i].Pln_Time == null) ? "" : salesnl[i].Pln_Time) + '</td><td>' + ((salesnl[i].Worktype_Name_B == null) ? "" : salesnl[i].Worktype_Name_B) + '</td><td>' + salesnl[i].dist_name + '</td><td>' + salesnl[i].ClstrName + '</td><td>' + salesnl[i].Remarks + '</td><td>' + salesnl[i].worked_with_name + '</td><td>' + ((salesnl[i].Start_Time == null) ? "" : salesnl[i].Start_Time) + '</td><td>' + ((salesnl[i].End_Time == null) ? "" : salesnl[i].End_Time) + '</td><td>' + salesnl[i].versions + '</td></tr>';
                        }

                        $('#grid tbody').append(str);

                    }
                })
            }
            else {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "view_countofsf.aspx/getdatasf",
                data: "{'wtyp':'" + wtp + "','sfc':'" + scode + "','cdate':'" + dates + "','subc':'" + subcode + "','divc':'" + div + "'}",
                dataType: "json",
                success: function (data) {
                    $('#grid tbody').html('');
                    sales = JSON.parse(data.d);
                    var str = ''; 
                   
                    for (var i = 0; i < sales.length; i++) {

                        str += '<tr><td>' + (i + 1) + '</td><td>' + ((sales[i].Pln_Date == null) ? "" : sales[i].Pln_Date) + '</td><td>' + sales[i].sf_emp_id + '</td><td>' + sales[i].Sf_Name + '</td><td>' + sales[i].StateName + '</td><td>' + sales[i].rsf + '</td><td>' + sales[i].MOb + '</td><td>' + sales[i].Pln_Time + '</td><td>' + sales[i].Worktype_Name_B + '</td><td>' + sales[i].dist_name + '</td><td>' + sales[i].ClstrName + '</td><td>' + sales[i].Remarks + '</td><td>' + sales[i].worked_with_name + '</td><td>' + ((sales[i].Start_Time == null) ? "" : sales[i].Start_Time) + '</td><td>' + ((sales[i].End_Time == null) ? "" :sales[i].End_Time)  + '</td><td>' + sales[i].versions + '</td></tr>';
                     }

                    $('#grid tbody').append(str);
                 
                }
            });
            }
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

                pdf.save("worktypedetails.pdf");
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
            var tets = 'worktypedetails' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
        </script>
    </body>

</html>
   
