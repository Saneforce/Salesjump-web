<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Distributor_Tag.aspx.cs" Inherits="MasterFiles_Reports_Rpt_Distributor_Tag" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style>
        #gvMyDayPlan{
            margin:30px;
        }
    </style>
    <script type="text/javascript" src="../../js/plugins/table2excel.js"></script>

    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
        function openmodal($x,$typ) {
            sf = $x;
            $sfname = '<%=sfname%>';
			fdt = '<%=FDate%>';
            tdt ='<%=TDate%>';
           // strOpen = "Rpt_Retailer_Tag_Breakup.aspx?SF_Code=" + sf + "&SF_Name=" + $sfname + "&subDiv=" + <%=subdiv_code%> + "&TagType=" + $typ;
            strOpen = "Rpt_Distributor_Tag_BreakUp.aspx?SF_Code=" + sf + "&SF_Name=" + $sfname + "&subDiv=&TagType=" + $typ + "&FDate=" + fdt + "&TDate=" + tdt;
            window.open(strOpen, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
        }
        $(document).ready(function () {
            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
            $("#btnExcel").click(function () {
                $("#gvMyDayPlan").table2excel({
                    filename: "Distributor Tag Status.xls"
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: 12pt; padding: 0px 20px;">Distributor Tag List</asp:Label>
                </div>
                <div class="col-sm-4" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                        href="#" class="btn btnPrint"></a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" /><a name="btnClose"
                            id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
                </div>
                <asp:GridView ID="gvMyDayPlan" runat="server" Width="100%" HorizontalAlign="Center"
                                    BorderWidth="1" CellPadding="2" mar EmptyDataText="No Data found for View"
                                    AutoGenerateColumns="false" CssClass="newStly" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="sfn" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mapped Distributors" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <a href="#" sfc="<%#Eval("Sf_Code")%>" ttyp="Retailer_Count" onclick="openmodal('<%#Eval("Sf_Code")%>','Retailer_Count')" ><%#Eval("Retailer_Count")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tagged Distributors" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <a href="#" sfc="<%#Eval("Sf_Code")%>" ttyp="Tagged" onclick="openmodal('<%#Eval("Sf_Code")%>','Tagged')" ><%#Eval("Tagged")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Untagged Distributors" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <a href="#" sfc="<%#Eval("Sf_Code")%>" ttyp="Untagged" onclick="openmodal('<%#Eval("Sf_Code")%>','Untagged')" ><%#Eval("Untagged")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>