<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="rpt_vantoursale.aspx.cs" Inherits="MIS_Reports_rpt_vantoursale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>vansales</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />


     <script language="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    
    <script type="text/javascript">
        var array1 = new Array();
        var array2 = new Array();
        var n = 2; //Total table
        for (var x = 1; x <= n; x++) {
            array1[x - 1] = x;
            array2[x - 1] = x + 'th';
        }

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
                , templateend = '</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'
                , body = '<body>'
                , tablevar = '<table>{table'
                , tablevarend = '}</table>'
                , bodyend = '</body></html>'
                , worksheet = '<x:ExcelWorksheet><x:Name>'
                , worksheetend = '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>'
                , worksheetvar = '{worksheet'
                , worksheetvarend = '}'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                , wstemplate = ''
                , tabletemplate = '';

            return function (table, name, filename) {
                var tables = table;

                for (var i = 0; i < tables.length; ++i) {
                    wstemplate += worksheet + worksheetvar + i + worksheetvarend + worksheetend;
                    tabletemplate += tablevar + i + tablevarend;
                }

                var allTemplate = template + wstemplate + templateend;
                var allWorksheet = body + tabletemplate + bodyend;
                var allOfIt = allTemplate + allWorksheet;

                var ctx = {};
                for (var j = 0; j < tables.length; ++j) {
                    ctx['worksheet' + j] = name[j];
                }

                for (var k = 0; k < tables.length; ++k) {
                    var exceltable;
                    if (!tables[k].nodeType) exceltable = document.getElementById(tables[k]);
                    ctx['table' + k] = exceltable.innerHTML;
                }

                //document.getElementById("dlink").href = uri + base64(format(template, ctx));
                //document.getElementById("dlink").download = filename;
                //document.getElementById("dlink").click();

                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })();

    </script>
 <script type="text/javascript">

     //        $(document).ready(function () { 
     //if($('HiddenField1').val!='35' )
     //        {           
     //   var remove = 0;
     //var col=$('.ff').prop("colSpan"); ;
     //           $('#gvtotalorder tr th').each(function (i) {
     //             
     //                //select all tds in this column
     //                var tds = $(this).parents('table')
     //              .find('tr td:nth-child(' + (i + 1) + ')');
     //                tds.each(function (j) {
     //                    if (this.innerHTML == '&nbsp;') this.innerHTML = ""; ;

     //                });
     //                //  alert(tds.innerHTML);
     //                //                tds.innerHTML = tds.innerHTML.replace(/&nbsp;/g, '');

     //                //check if all the cells in this column are empty
     //                if (tds.length == tds.filter(':empty').length) {


     //remove++;
     //var fcol=col-remove;

     //$('.ff').attr('colspan',remove);
     ////document.getElementById("myTd").colSpan = "1";

     //                    //hide header
     //                    $(this).hide();
     //                    //hide cells
     //                    tds.hide();
     //                }
     //$('.ff').attr('colspan',fcol);
     //            });
     //}
     //        });

    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };


    </script>

    <script src="../JsFiles/canvasjs.min.js"></script>
	<script type="text/javascript">

        function genChart(arrDta) {

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "theme2", //theme1
                title: {
                    text: "Categorywise Order"
                },
                animationEnabled: true,   // change to true
                data: [{
                    type: "pie",       // Change type to "bar", "area", "spline", "pie",etc.
                    dataPoints: arrDta
                }]
            });
            chart.render();
        }
	</script>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function getretdetails() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false, url: "samtotalorderview.aspx/GetRetDetails",
                dataType: "json",
                success: function (data) {
                    var gret = [];
                    gret = JSON.parse(data.d);
                    if (gret.length > 0) {
                        $('#totret').html('Total Retailers Visited ( TC ) : <b>' + gret[0].Total_call + "</b>")
                        $('#ordret').html('Ordered Retailers ( EC ) : <b>' + gret[0].Productive + "</b>")
                        $('#newret').html('New Outlets Created : <b>' + gret[0].drcount + "</b>")
                        $('#neworet').html('Newly Order Given Outlet : <b>' + gret[0].pdrcount + "</b>")
                        $('#totoval').html('Total Order Value : <b>' + gret[0].Order_Value + "</b>")
                    }
                    else {
                        $('#Rtsummary').hide();
                    }
                },

                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            getretdetails();
            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#pnlContents").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
        });

    </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        td{padding:2px 5px;}
		.subTot{Font-size:11pt;font-weight:bold;}
		.GrndTot{Font-size:13pt;font-weight:bold;}
        .remove  
  {
    text-decoration:none;
  }
  	.TopButton{
		border-color: Black;
	    border-width: 1px;
	    border-style: Solid;
	    font-family: Verdana;
	    font-size: 10px;
	    height: 25px;
	    width: 60px;
	}
    </style>
 <script type="text/javascript">
     function exportexcel() {
         $("#pnlContents").table2excel({

             filename: "PrimaryOrderView",
             fileext: ".xls"
         });
     };
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" >
                <tr>
              
                    <td width="70%" align="center" >
                    <asp:Label ID="lblHead" Text="Retail Lost  Details" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server" Font-Size="Medium"></asp:Label>
                    </td>
                    <td align="right">
                        
                                    <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" />
                                        <asp:LinkButton ID="btnExcel"  runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                            OnClick="btnExcel_Click" /> 
                               
                               
 <input id="pdfexport" type="linkbutton" style="padding: 0px 20px;" class="btn btnPdf" value="PDF" height="35px"   onclick="generate()"" >   



                                   <asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>


                                </td>

                            </tr>
            </table>
        </asp:Panel>
    </div>
      <asp:Panel id="pnlContents" EnableViewState="false"  runat="server" Width="100%">
                <table border="0" id='1'   width="90%" style="margin:auto;">                 
                                                   
          <tr align="right"><td align="center" style="font-size: small; font-weight: bold;font-family: Andalus;">FieldForce Name:
                   <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                         
            
                   
                    <tr> 
                        <td width="100%" colspan="2">
                    <asp:HiddenField ID="HiddenField1" runat="server"  />
                            <asp:GridView ID="gvtotalorder" runat="server" Width="80%"  class="newStly"  GridLines="Both"

 ShowHeader="false" Font-Size=8pt     HorizontalAlign="Center" OnDataBound = "OnDataBound" OnRowCreated = "OnRowCreated"   OnRowDataBound="GridView1_RowDataBound"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" BackColor="#ffffe0" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>                              
                                
                                     
                                   
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    </table>
 </asp:Panel>
           <div id="chartContainer" style="text-align:center;height:180px; width: 95%;"></div>
     
     
<script type="text/javascript">

    function generate() {



        // var doc = new jsPDF('l', 'pt');
        //var doc = new jsPDF(l, '', '', '');
        var doc = new jsPDF('l', 'mm', [1000, 1414]);
        var res = doc.autoTableHtmlToJson(document.getElementById("gvtotalorder"));
        var res1 = doc.autoTableHtmlToJson(document.getElementById("GridViewcat"));



        var header = function (data) {
            doc.setFontSize(10);
            doc.setTextColor(40);
            doc.setFontStyle('normal');
            //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
            doc.text("TotalOrderView", data.settings.margin.top, 30);
        };
        var options = {
            beforePageContent: header,
            margin: {
                top: 40
            },
            startY: doc.autoTableEndPosY() + 50
        };

        doc.autoTable(res.columns, res.data, options);

        var header1 = function (data) {
            doc.setFontSize(13);
            doc.setTextColor(40);
            doc.setFontStyle('normal');
            //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
            doc.text("Categorywise", data.settings.margin.left, doc.autoTableEndPosY() + 30);
        };
        var options1 = {
            beforePageContent: header1,
            margin: {
                top: 40
            },
            startY: doc.autoTableEndPosY() + 50
        };



        doc.autoTable(res1.columns, res1.data, options1);

        doc.save("TotalOrderView.pdf");



    }</script>
  </form>
</body>
</html>
