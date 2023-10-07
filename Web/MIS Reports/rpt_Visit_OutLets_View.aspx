<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="rpt_Visit_OutLets_View.aspx.cs"
    Inherits="MIS_Reports_rpt_Visit_OutLets_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Visit OutLets View</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function popUp(Stockist_Code, date, sf_Code, sf_name) {
            var dt = $('#<%=Hid_date.ClientID%>').val();
            console.log(dt);
            strOpen = "rptdailycallreport.aspx?DATE=" + dt + "&sfcode=" + sf_Code + "&sfname=" + sf_name + "&DistCode=" + Stockist_Code
            window.open(strOpen, "page", 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');


        }
    </script>
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
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
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
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        td
        {
            padding: 2px 5px;
        }
        .subTot
        {
            font-size: 11pt;
            font-weight: bold;
        }
        .GrndTot
        {
            font-size: 13pt;
            font-weight: bold;
        }
        .remove
        {
            text-decoration: none;
        }
        .TopButton
        {
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

                filename: "TodayOrderView",
                fileext: ".xls"
            });
        };
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $(document).on('click', "#btnClose", function () {
                window.close();
            });

            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('content');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'Primary_View' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
        });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="Hid_date" runat="server" />
    <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="80%" align="center">
                        <asp:Label ID="lblHead" Text="Retail Lost  Details" SkinID="lblMand" Font-Bold="true"
                            Font-Underline="true" runat="server" Font-Size="Medium"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btnPrint" OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <a id="btnPdf"  class="btn btnPdf" onclick="generate()"></a>
                                </td>
                                <td>
                                    <a id="btnExcel"  type="button"  onclick="tablesToExcel(array1, array2, 'myfile.xls')" class="btn btnExcel"></a>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnClose" runat="server" type="button" Style="padding: 0px 20px;"
                                        href="javascript:window.open('','_self').close();" class="btn btnClose" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%">
            <tr>
                 <td align="left" style="font-size: small; width:50% !important;  font-weight: bold; font-family: Andalus;">
                    FieldForce Name:
                    <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
                <td align="center" style="font-size: small;width:50% !important; height:20% !important; font-weight: bold; font-family: Andalus;">
                    Sub Division:
                    <asp:DropDownList ID="ddlsubdiv" runat="server" Width="200" Height="25" AutoPostBack="true"  OnSelectedIndexChanged="ddlsubdiv_SelectedIndexChanged">

                    </asp:DropDownList>
                    
                </td>
            </tr>
        </table>
        <table border="0" id='1' width="100%">
          
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvtotalorder" runat="server" Width="100%" HorizontalAlign="Center"
                        OnDataBound="OnDataBound"  BorderWidth="1px" CellPadding="2"
                        EmptyDataText="No Data found for View" AutoGenerateColumns="false" class="newStly"
                        Style="border-collapse: collapse;">
                         <%--OnRowCreated="OnRowCreated"--%>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Field Force" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="stockist_code" runat="server" Visible="false" Font-Size="9pt" Text='<%#Eval("stockist_code")%>'></asp:Label>
                                    <a style="white-space: nowrap;" font-size="9pt" href="javascript:popUp('<%# Eval("stockist_code") %>','<%# Eval("Productive") %>','<%# Eval("Sf_Code") %>','<%# Eval("Sf_Name") %>')">
                                        <%# Eval("Sf_Name")%></a>
                                    <%--  <asp:Label ID="stockist" Style="white-space: nowrap" runat="server" Font-Size="9pt"
                                        Text='<%#Eval("Sf_Name")%>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
 
                            <asp:TemplateField HeaderText="Reporting To" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("rsfname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Empployee Id" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="EmpId" runat="server" Font-Size="9pt" Text='<%#Eval("EmpId")%>'></asp:Label>
                                    <asp:HiddenField ID="hfsudiv" runat="server" Value='<%#Eval("subdivision_code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Distributor" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="stockist_name" runat="server" Font-Size="9pt" Text='<%#Eval("stockist_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Route" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Route_name" runat="server" Font-Size="9pt" Text='<%#Eval("Plan_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Call"  HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Total_Call" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Total_Call")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Productive Call"  HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Productive" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Productive")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
							   <asp:TemplateField HeaderText="Productive(%)"  HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Productiveper" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Productiveper")+"%"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
							
							
                            <asp:TemplateField HeaderText="Non Productive"  HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Nonproductive" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Nonproductive")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Order_Value" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_Value")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="Pay Type" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="toval" runat="server" Font-Size="9pt" Text='<%#Eval("Pay_Type")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Collected Amount" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="discountpri" runat="server" Font-Size="9pt" Text='<%#Eval("Collected_Amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="orderval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_Value", "{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                           
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        
        <table id='2'>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" width="50%">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Bold="True" Visible="false"
                        Font-Underline="true" Text="Categorywise Order"></asp:Label>
                    <asp:GridView ID="GridViewcat" runat="server" Width="100%" HorizontalAlign="Center"
                        Visible="false" BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                        AutoGenerateColumns="false" BackColor="#ffffe0" HeaderStyle-BackColor="#33CCCC"
                        HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Category" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Cat_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="stockist" runat="server" Font-Size="9pt" Text='<%#Eval("quantity")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="stval" runat="server" Font-Size="9pt" Text='<%#Eval("value")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    <div id="chartContainer" style="text-align: center; height: 180px; width: 95%;">
    </div>
    <script type="text/javascript">
        function generate() {
            var doc = new jsPDF('l', 'mm', [250,200]);
            var res = doc.autoTableHtmlToJson(document.getElementById("gvtotalorder"));
			doc.autoTable(res.columns, res.data, { 
				margin: {top: 15}, 
				addPageContent: function(data) { 
					doc.text("Visited Outlets View", 15, 10);
				}
			});
            doc.save("VisitedOutletsView.pdf");
        }</script>
    </form>
</body>
</html>
