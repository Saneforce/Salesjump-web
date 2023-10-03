<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="retailer_visit_status.aspx.cs" Inherits="MIS_Reports_retailer_visit_status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dashboard Details</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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
        <div class="card">
       <div class="card-body table-responsive" style="overflow-x: auto;">
             <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
          <asp:Label ID="lblHead" Text="Retail Lost  Details" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
        <div>
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            &nbsp;</td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                            <td align="left">
                             <asp:Label ID="Label1" Text="*Note:-All Active Retailer Only" ForeColor="Red" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                        
                    </tr>
                </table>
           </div>
            <br />
            <%--<div id="chartContainer" style="height: 300px; width: 100%;"></div>--%>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="98%">
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td align="center">
                        <asp:Label runat="server" id="lblResultMsg" ForeColor="Red" Visible="False" 
                            Width="750px" height="25px" BorderColor="#66CCFF" BorderWidth="1px" Font-Size="Medium"  BackColor="#d6e9c6"  /></td></tr>
                </tbody>
            </table>  
            </div>      
    </asp:Panel>

        <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                       </b>
                </div>
         <img  style="cursor: pointer;float:right;" src="/limg/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
            <asp:HiddenField ID="fdate" runat="server" />
            <asp:HiddenField ID="tdate" runat="server" />
            <asp:HiddenField ID="dv" runat="server" />
           <asp:HiddenField ID="md" runat="server" />
        <table id="grid" class="grids">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>SFcode</th>
                        <th>SF Name</th>
                        <th>Mapped Retailers</th>
                        <th>Total Retailers</th>
                        <th>Effective Retailer</th>
                        <th>Visited Retailer</th>
                        <th>Non- Visited Retailer</th>
                      </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table>
            <div class="overlay" id="loadover" style="display: none;">
            <div id="loader">Please Wait Loading...</div>
       </div></div>
       
    </form>
       <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
  
    
    <script type="text/javascript">
          var str = "";
        var usr = [];
        var visi = []; var map = []; var tot = []; let type = ''; var salesnl = [];
        var fdt = $('#<%=fdate.ClientID%>').val();
        var tdt = $('#<%=tdate.ClientID%>').val();
        var div = $('#<%=dv.ClientID%>').val();
        var mde = $('#<%=md.ClientID%>').val();
       
        $(document).ready(function () {
            $('#loadover').show();
            setTimeout(function () {
                $.when(Fillusers(), Fillvisiefec(), Fillmapped(), Filltot()).then(function () {
                    Reloadtable();
                });
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
        });
        function Fillusers() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "retailer_visit_status.aspx/Fillusers",
                async: false,
                dataType: "json",
                success: function (data) {
                    usr = JSON.parse(data.d);
                }

            })
        }
        function Fillvisiefec() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "retailer_visit_status.aspx/Fillvisiefec",
                async: false,
                dataType: "json",
                success: function (data) {
                    visi = JSON.parse(data.d);

                }

            })
        }
        function Fillmapped() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "retailer_visit_status.aspx/Fillmapped",
                async: false,
                dataType: "json",
                success: function (data) {
                    map = JSON.parse(data.d);

                }

            })
        }
        function Filltot() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "retailer_visit_status.aspx/Filltot",
                async: false,
                dataType: "json",
                success: function (data) {
                    tot = JSON.parse(data.d);

                }

            })
        }
        function Reloadtable() {
            var totret = 0; var efret = 0; var visret = 0; var nonvisret = 0;
            for (var i = 0; i < usr.length; i++) {
                var mapd = map.filter(function (a) {
                    return a.SF_Code == usr[i].Sf_Code;
                });
                var ttl = tot.filter(function (a) {
                    return a.sf_code == usr[i].Sf_Code;
                });
                var visit = visi.filter(function (a) {
                    return a.SF_Code == usr[i].Sf_Code;
                });
                var nonvis = (ttl.length > 0 ? ttl[0].tot : 0) - (visit.length > 0 ? visit[0].visicnt : 0);
                str += '<tr><td>' + (i + 1) + '</td><td>' + usr[i].Sf_Code + '</td><td>' + usr[i].Sf_Name + '</td><td>' + (mapd.length > 0 ? mapd[0].map : '') + '</td><td>' + (ttl.length > 0 ? ttl[0].tot : '') + '</td><td><a href="#"  sf=' + usr[i].Sf_Code + ' id="view_eff" style="color: #333333;">' + (visit.length > 0 ? visit[0].effec : '') + '</a></td><td><a href="#"  sf=' + usr[i].Sf_Code + ' id="view_vis" style="color: #333333;">' + (visit.length > 0 ? visit[0].visicnt : '') + '</a></td><td><a href="#"  sf=' + usr[i].Sf_Code + ' id="view_nvis" style="color: #333333;">' + nonvis + '</a></td></tr>';
                totret += (ttl.length > 0 ? ttl[0].tot : 0);
                efret += (visit.length > 0 ? visit[0].effec : 0);
                visret += (visit.length > 0 ? visit[0].visicnt : 0);
                nonvisret += nonvis;
            }

            $('#grid tbody').append(str);
            $('#grid tfoot').html("<tr><td colspan=4 style='text-align: center;font-weight: bold'>Total  Value</td><td><label>" + totret + "</label></td><td><label>" + efret + "</label></td><td><label>" + visret + "</label></td><td><label>" + nonvisret + "</label></td></tr>");

        } 
       
        $(document).on('click', '#view_eff', function () {
             let sfc = $(this).attr("sf");
            if (mde == "Month") {
                var sURL = "rptroutedeviationEffretview_mon.aspx?sf_code=" + sfc + "&Div_code=" + div + "&FDate=" + fdt + "&TDate=" + tdt;
            }
            else {
                var sURL = "rptroutedeviationEffretview.aspx?Division_code=" + div + "&sf_code=" + sfc + "&date=" + fdt;
            }
            window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
        });
        $(document).on('click', '#view_vis', function () {
            let sfc = $(this).attr("sf");
            if (mde == "Month") {
                var sURL = "rptroutedeviationretview_mon.aspx?sf_code=" + sfc + "&Div_code=" + div + "&FDate=" + fdt + "&TDate=" + tdt;
            }
            else {
                var sURL = "rptroutedeviationretview.aspx?Division_code=" + div + "&sf_code=" + sfc + "&date=" + fdt;
            }
            window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
        });
        $(document).on('click', '#view_nvis', function () {
            let sfc = $(this).attr("sf");
            if (mde == "Month") {
                var sURL = "rptroutedeviationnonretview_mon.aspx?sf_code=" + sfc + "&Division_code=" + div + "&FDate=" + fdt + "&TDate=" + tdt;
            }
            else {
                var sURL = "rptroutedeviationnonretview.aspx?Division_code=" + div + "&sf_code=" + sfc + "&date=" + fdt;
            }
            window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
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

                pdf.save("Retailer_Visit_Status.pdf");
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
            var tets = 'Retailer_Visit_Status' + '.xls';   //create fname

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
           
                var txt = type + '_Details';
           
            var tets = txt + '.xls';    //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
        </script>
    
    </body>

</html>

