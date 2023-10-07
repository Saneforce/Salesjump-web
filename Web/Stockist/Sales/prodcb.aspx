<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="prodcb.aspx.cs" Inherits="Stockist_Sales_prodcb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
<script src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

        });
        function Showrpt() {

            id = $('#<%=dt.ClientID%>').val();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            prod = $('#<%=idpro.ClientID%>').val();

            $('#<%=fdt.ClientID%>').val(fdt);
            $('#<%=tdt.ClientID%>').val(tdt);
            $('#<%=prod.ClientID%>').val(prod);

        }
	function getPDF(){

		var HTML_Width = $(".canvas_div_pdf").width();
		var HTML_Height = $(".canvas_div_pdf").height();
		var top_left_margin = 15;
		var PDF_Width = HTML_Width+(top_left_margin*2);
		var PDF_Height = (PDF_Width*1.5)+(top_left_margin*2);
		var canvas_image_width = HTML_Width;
		var canvas_image_height = HTML_Height;
		
		var totalPDFPages = Math.ceil(HTML_Height/PDF_Height)-1;
		

		html2canvas($(".canvas_div_pdf")[0],{allowTaint:true}).then(function(canvas) {
			canvas.getContext('2d');
			
			console.log(canvas.height+"  "+canvas.width);
			
			
			var imgData = canvas.toDataURL("image/jpeg", 1.0);
			var pdf = new jsPDF('p', 'pt',  [PDF_Width, PDF_Height]);
		    pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin,canvas_image_width,canvas_image_height);
			
			
			for (var i = 1; i <= totalPDFPages; i++) { 
				pdf.addPage(PDF_Width, PDF_Height);
				pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height*i)+(top_left_margin*4),canvas_image_width,canvas_image_height);
			}
			
		    pdf.save("Misreports.pdf");
        });
	};
function exportToprint() {
 var divToPrint=document.getElementById("expex");
   newWin= window.open("");
   newWin.document.write(divToPrint.outerHTML);
   newWin.print();
   newWin.close();
          
      
            
        }
function exportToExcel() {
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

            htmls = document.getElementById("expex").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }


            var link = document.createElement("a");
            
            
            var tets = 'Misreports' + '.xls';   //create fname
            
            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
</script>

    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
		
    </style>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12 sub-header">
               Depot sales
                 <span style="float: right; margin-right: 15px;">
                     <asp:Button ID="btnSF" class="btn btn-primary btn" runat="server" Text="Go"  OnClientClick="Showrpt()" OnClick="btnSF_Click" />
                 </span>
                <span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
                            <span id="ordDate" runat="server"></span><i class="fa fa-caret-down"></i>
                    </div>
                </span>
                <span style="float: right; margin-right: 15px;">
                    <div>
                        <select class="form-control" clientidmode="Static" id="idpro" runat="server">
                        </select>
                    </div>
                </span>

            </div>
        </div>
        <asp:HiddenField ID="fdt" runat="server" Value="" />
        <asp:HiddenField ID="tdt" runat="server" Value="" />
        <asp:HiddenField ID="prod" runat="server" Value="" />
        <asp:HiddenField ID="dt" runat="server" Value="" />
       <div class="card" style="overflow:auto;">
            <div class="card-body" >
             <center><br /><br />
   <p align="left">
                <img onclick="exportToExcel()"
                     style="cursor: pointer;float:right;" src="../../img/Excel-icon.png"
                     alt="" width="40" height="40" id="btnExport" />
            </p>
 <p align="left">
                <img onclick="exportToprint()"
                     style="cursor: pointer;float:right;" src="../../img/print.png"
                     alt="" width="40" height="40" id="btnprint" />
            </p>
 <p align="left">
                <img onclick="getPDF()"
                     style="cursor: pointer;float:right;" src="../../img/pdf.png"
                     alt="" width="40" height="40" id="btnprint" />
            </p>
 <div id="expex" class="canvas_div_pdf">
                        <asp:Panel ID="Panel1" runat="server">
                           
                                    <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="Grid" EmptyDataText="No Records Found"
                                        GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdFixation_RowCreated"
                                         ShowHeader="False" Width="90%" Font-Names="calibri" OnRowDataBound="GrdFixation_RowDataBound" 
                                        Font-Size="Small" >
                                        <HeaderStyle Font-Bold="False" CssClass="VertiColumn" />
                                        <PagerStyle CssClass="pgr" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt" />
                                        <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                                        <Columns>
                                        </Columns>
                                        <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                            <br /><br />
                            <asp:GridView ID="GrdFixation1" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="Grid" EmptyDataText="No Records Found"
                                        GridLines="Both" HorizontalAlign="Center" BorderWidth="1" OnRowCreated="GrdFixation_RowCreated1"
                                         ShowHeader="False" Width="90%" Font-Names="calibri" OnRowDataBound="GrdFixation_RowDataBound1" 
                                        Font-Size="Small" >
                                        <HeaderStyle Font-Bold="False" CssClass="VertiColumn" />
                                        <PagerStyle CssClass="pgr" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt" />
                                        <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                                        <Columns>
                                        </Columns>
                                        <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                              
                           
                        </asp:Panel>
</div>
                    </center>
        </div>
            </div>
    </form>
    <script type="text/javascript">

        <%--var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
        var Div_Code = ("<%=Session["div_code"].ToString()%>");
        $(document).ready(function () {
            loadstk();
        });

        function loadstk() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "prodcb.aspx/GetDist",
                dataType: "json",
                success: function (data) {

                    var Ports = data.d;
                    var select = $('#<%=idpro.ClientID%>');
                    select.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(Ports, function (index, value) {
                        select.append("<option value='" + value.Prodcd + "'>" + value.Prodname + "</option>")
                        //select.find(":last").attr("Sequence", value.Type);
                    });

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }--%>
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                $('#<%=dt.ClientID%>').val(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
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
    </script>
</asp:Content>
