<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_target_setting.aspx.cs"
    EnableEventValidation="false" Inherits="MIS_Reports_rpt_target_settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DSR Monitoring Report</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script language="Javascript">
        function popUp(sfcode, type, sfname, vals) {

            if (sfcode != 'sub_tot') {

                if (sfcode != 'GRAND_TOTAL') {
                    if (Number(vals) > 0) {

                        fyear = $('#<%=hyear.ClientID%>').val();
                        fmonth = $('#<%=hmonth.ClientID%>').val();

                        strOpen = "rptDSRCallsDetails.aspx?SF_Code=" + sfcode + "&Year=" + fyear + "&SF_Name=" + sfname + "&months=" + fmonth + "&types=" + type
                        window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
                    }
                }
            }
        }
        $(document).ready(function () {

            $('#<%=Dgv_SKU.ClientID%> tr').each(function () {
                console.log($(this).find('[id*=hsfCode]').val());
                if ($(this).find('[id*=hsfCode]').val() == 'sub_tot') {
                    $(this).css('background', '#607d8b');
                    $(this).css('color', '#fff');
                }
                if ($(this).find('[id*=hsfCode]').val() == 'GRAND_TOTAL') {
                    $(this).css('background', '#496a9a');
                    $(this).css('color', '#fff');
                }
            });



            $(document).on('click', "#btnExcel", function (e) {
                //                var dt = new Date();
                //                var day = dt.getDate();
                //                var month = dt.getMonth() + 1;
                //                var year = dt.getFullYear();
                //                var postfix = day + "_" + month + "_" + year;
                //                //creating a temporary HTML link element (they support setting file names)
                //                var a = document.createElement('a');
                //                //getting data from our div that contains the HTML table
                //                var data_type = 'data:application/vnd.ms-excel';
                //                var table_div = document.getElementById('content');
                //                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                //                a.href = data_type + ', ' + table_html;
                //                //setting the file name
                //                a.download = 'DSR_Monitoring_Report_' + postfix + '.xls';
                //                //triggering the function
                //                a.click();
                //                //just in case, prevent default behaviour
                //                e.preventDefault();


                var a = document.createElement('a');
                var data_type = 'data:application/vnd.ms-excel';
                a.href = data_type + ', ' + encodeURIComponent($('div[id$=content]').html());
                document.body.appendChild(a);
                a.download = 'DSR_Monitoring_Report.xls';
                a.click();
                e.preventDefault();
            });



        });
        function RefreshParent() {
            window.close();
        }
       
    </script>
    <style type="text/css">
        #Dgv_SKU a
        {
            color: blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="max-width: 100%; width: 95%; margin: 0px auto;">
        <br />
        <div class="row" style="width: 100%; margin: 0px auto;">
            <div class="col-md-8">
                <asp:Image ID="logoo" runat="server" Style="border-width: 0px; height: 39px;"></asp:Image>
                <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large"></asp:Label>
            </div>
            <div class="col-md-4" style="text-align: right">
                <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                    OnClick="btnPrint_Click" />
                <a id="btnExport" runat="Server" style="padding: 0px 20px;" class="btn btnPdf" visible="false"
                    onclick="generate()"></a>
                <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
                <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                    class="btn btnClose"></a>
            </div>
        </div>
        <br />
        <div class="container" id="content" style="max-width: 100%; width: 100%; margin: 0px auto;">
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <div style="text-align: center; font-size: large; display: none">
                    DSR Monitoring Report For Month of <b>
                        <asp:Label ID="lblhead" runat="server"></asp:Label>
                        <asp:HiddenField ID="hyear" runat="server" />
                        <asp:HiddenField ID="hmonth" runat="server" />
                    </b>
                </div>
                <asp:GridView ID="Dgv_SKU" runat="server" Width="100%" HorizontalAlign="Center" BorderWidth="1"
                    GridLines="Both" CssClass="newStly" OnRowDataBound="Dgv_SKU_RowDataBound" AutoGenerateColumns="false">
                    <RowStyle HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="SF Name">
                            <ItemTemplate>
                                <asp:HiddenField ID="hsfCode" runat="server" Value='<%#Eval("SF Code")%>' />
                                <asp:Label ID="lblsf" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Working Days">
                            <ItemTemplate>
                                <asp:Label ID="lbltwd" runat="server" Text='<%#Eval("Target Working Days")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Working Days">
                            <ItemTemplate>
                                <asp:Label ID="lblawd" runat="server" Text='<%#Eval("Actual Working Days")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Actual vs Target Working Days">
                            <ItemTemplate>
                                <asp:Label ID="lblTWD" runat="server" Text='<%#Eval("% Actual vs Target Working Days")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Calls">
                            <ItemTemplate>
                                <asp:Label ID="LBLtc" runat="server" Text='<%#Eval("Target Calls")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Productive Calls">
                            <ItemTemplate>
                                <asp:Label ID="lbltpc" runat="server" Text='<%#Eval("Target Productive Calls")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Calls">
                            <ItemTemplate>
                                <a href="javascript:popUp('<%# Eval("SF Code") %>','TC','<%#Eval("Name")%>','<%# Eval("Actual Calls")%>')">
                                    <%# Eval("Actual Calls")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Call Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblcr" runat="server" Text='<%#Eval("Call Rate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Productive Calls">
                            <ItemTemplate>
                                <a href="javascript:popUp('<%# Eval("SF Code") %>','PC','<%#Eval("Name")%>','<%# Eval("Actual Productive Calls")%>')">
                                    <%# Eval("Actual Productive Calls")%></a>                              
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conversion Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblcra" runat="server" Text='<%#Eval("Conversion Rate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="New Doors">
                            <ItemTemplate>
                             <a href="javascript:popUp('<%# Eval("SF Code") %>','NR','<%#Eval("Name")%>','<%# Eval("New Doors")%>')">
                                    <%# Eval("New Doors")%></a>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Repeat Purchase">
                            <ItemTemplate>
                                <a href="javascript:popUp('<%# Eval("SF Code") %>','RP','<%#Eval("Name")%>', <%# Eval("Repeat Purchase")%>)">
                                    <%# Eval("Repeat Purchase")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Repeat Purchase Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblrprr" runat="server" Text='<%#Eval("Repeat Purchase Rate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Volume (strings)">
                            <ItemTemplate>
                                <asp:Label ID="lbltvs" runat="server" Text='<%#Eval("Total Volume Strings")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Vol Achivemnt">
                            <ItemTemplate>
                                <asp:Label ID="lbltva" runat="server" Text='<%#Eval("Target Vol Achivemnt")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total value">
                            <ItemTemplate>
                                <asp:Label ID="lbltv" runat="server" Text='<%#Eval("Total value")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value Achivement">
                            <ItemTemplate>
                                <asp:Label ID="lblva" runat="server" Text='<%#Eval("Value Achivement")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Average Daily Sales">
                            <ItemTemplate>
                                <asp:Label ID="lblads" runat="server" Text='<%#Eval("Average Daily Sales")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Average Drop size">
                            <ItemTemplate>
                                <asp:Label ID="lblads" runat="server" Text='<%#Eval("Average Drop size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Average Daily Productive Calls">
                            <ItemTemplate>
                                <asp:Label ID="lbladpc" runat="server" Text='<%#Eval("Average Daily Productive Calls")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <div style="text-align: right; padding: 2px 46px; font-size: small">
                    <span style="color: #1000ff; font-weight: bold">Taken by : </span><b>
                        <asp:Label ID="lbl_SFname" runat="server"></asp:Label>
                    </b>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script type="text/javascript">

        function generate() {

            var doc = new jsPDF('l', 'pt');

            var res = doc.autoTableHtmlToJson(document.getElementById("Dgv_SKU"));
            var header = function (data) {
                doc.setFontSize(10);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("DSR Monitoring Report", data.settings.margin.top, 70);
            };
            var options = {
                beforePageContent: header,
                margin: {
                    top: 80
                },
                startY: doc.autoTableEndPosY() + 20
            };
            doc.autoTable(res.columns, res.data, options);

            doc.save("DSRMonitoringReport.pdf");
        }
    </script>
    </form>
</body>
</html>
