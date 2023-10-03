<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptCallCompliance.aspx.cs" Inherits="MIS_Reports_RptCallCompliance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Call Compliance Report</title>
<link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFDT" runat="server" />
        <asp:HiddenField ID="hTDT" runat="server" />
        <asp:HiddenField ID="hsubDiv" runat="server" />
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
             <asp:ImageButton ID="btnimgbuttonExcel" ImageUrl="~/img/excel.png" runat="server" 
                Width="40" Height="40" CssClass="form-control" OnClick="btnimgbuttonExcel_Click" />
            <img class="hidden" style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        </div>

        <div class="row" style="margin: 0px 0px 0px 5px;">
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="Label1" runat="server" Text="Call Compliance Report" Style="margin-left: 10px; font-size: x-large"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 6px 0px 0px 11px;">
                <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
                <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
            </div>
            <br />
            <br />
            <div id="content">
			   
			   <asp:GridView ID="GridView1" runat="server" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                   CellPadding="4" ForeColor="#333333"  class="newStly"  GridLines="Both"  AutoGenerateColumns="false" 
                   PageSize="500" >
                   <AlternatingRowStyle BackColor="White" />
                   <Columns>
                       <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1" 
                           HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                           <ItemTemplate>
                               <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          <%-- <ItemTemplate>
                               <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                           </ItemTemplate>--%>
                           <HeaderStyle BorderWidth="1px" Font-Size="8pt"></HeaderStyle>
                           <ItemStyle HorizontalAlign="Center" BorderWidth="1px" Width="50px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Territory">
                           <ItemTemplate>
                               <asp:Label ID="lblTerritory" runat="server" Text='<%#Eval("Territory")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="SF Code">
                           <ItemTemplate>
                               <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="SF Name">
                           <ItemTemplate>
                               <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("SF_Name")%>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Date">
                           <ItemTemplate>
                               <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Route Code">
                           <ItemTemplate>
                               <asp:Label ID="lblRouteCode" runat="server" Text='<%#Eval("RouteCode")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Route Name">
                           <ItemTemplate>
                               <asp:Label ID="lblRouteName" runat="server" Text='<%#Eval("RouteName")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Retailer Code">
                           <ItemTemplate>
                               <asp:Label ID="lblRetailerCode" runat="server" Text='<%#Eval("RetailerCode")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Retailer Name">
                           <ItemTemplate>
                               <asp:Label ID="lblRetailerName" runat="server" Text='<%#Eval("RetailerName")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Mobile">
                           <ItemTemplate>
                               <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Start Time">
                           <ItemTemplate>
                               <asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("StartTime")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="End Time">
                           <ItemTemplate>
                               <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="In-Store Time">
                           <ItemTemplate>
                               <asp:Label ID="lblInStoreTime" runat="server" Text='<%#Eval("InStoreTime")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Class">
                           <ItemTemplate>
                               <asp:Label ID="lblClass" runat="server" Text='<%#Eval("Class")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Channel">
                           <ItemTemplate>
                               <asp:Label ID="lblChannel" runat="server" Text='<%#Eval("Channel")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Distributor">
                           <ItemTemplate>
                               <asp:Label ID="lblDistributor" runat="server" Text='<%#Eval("DistributorName")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Submitted Lat">
                           <ItemTemplate>
                               <asp:Label ID="lblSubmittedLat" runat="server" Text='<%#Eval("Call_lat")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Submitted Long">
                           <ItemTemplate>
                               <asp:Label ID="lblSubmittedLong" runat="server" Text='<%#Eval("Call_lng")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Actual Lat">
                           <ItemTemplate>
                               <asp:Label ID="lblActualLat" runat="server" Text='<%#Eval("Retlat")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Actual Long">
                           <ItemTemplate>
                               <asp:Label ID="lblActualLong" runat="server" Text='<%#Eval("RetLng")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Distance">
                           <ItemTemplate>
                               <asp:Label ID="lblDistance" runat="server" Text='<%#Eval("Distance")%>' ></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Order Value">
                           <ItemTemplate>
                               <asp:Label ID="lblOrderValue" runat="server" Text='<%#Eval("OrderValue")%>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Reason">
                           <ItemTemplate>
                               <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                   </Columns>
                   <EditRowStyle BackColor="#2461BF" />
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                   <PagerSettings FirstPageText="First" PageButtonCount="4" LastPageText="Last" Mode="NumericFirstLast" Visible="true" />
                   <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle BackColor="#EFF3FB" />
                   <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                   <SortedAscendingCellStyle BackColor="#F5F7FB" />
                   <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                   <SortedDescendingCellStyle BackColor="#E9EBEF" />
                   <SortedDescendingHeaderStyle BackColor="#4870BE" />
               </asp:GridView>

			
			
                <table id="FFTbl" class="newStly hidden" style="border-collapse: collapse;">
                    <thead>
                        <tr>
                            <th>SLNo.</th>
                            <th>Territory</th>
                            <th>SF Code</th>
                            <th>SF Name</th>
                            <th>Date</th>
                            <th>Route Code</th>
                            <th>Route Name</th>
                            <th>Retailer Code</th>
                            <th>Retailer Name</th>
                            <th>Mobile</th>
                            <th>Start Time</th>
                            <th>End Time</th>
                            <th>In-Store Time</th>
                            <th>Class</th>
                            <th>Channel</th>
                           
                            <th>Distributor</th>
                            <th>Submitted Lat</th>
                            <th>Submitted Long</th>
                            <th>Actual Lat</th>
                            <th>Actual Long</th>
                            <th>Distance</th>
                            <th>Order Value</th>
                            <th>Reason</th>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        let jsonData = [];
        function getSKU() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RptCallCompliance.aspx/getDetails",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    jsonData = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
            $.when(getSKU()).then(function () {
                //ReloadData();
            });
        }
        function ReloadData() {
            $("#FFTbl>tbody").html('');
            let strArr = [];
            for (let i = 0; i < jsonData.length; i++) {
                 //let str = `<tr><td align="right">${i + 1}</td><td>${jsonData[i]["Activity_Date"]}</td><td>${jsonData[i]["StartTime"]}</td><td>${jsonData[i]["EndTime"]}</td><td>${jsonData[i]["SF_Name"]}</td><td>${jsonData[i]["RetailerName"]}</td><td>${jsonData[i]["Route"]}</td><td>${jsonData[i]["Class"]}</td><td>${jsonData[i]["Channel"]}</td><td>${jsonData[i]["Mobile"]}</td><td>${jsonData[i]["DistributorName"]}</td><td>${jsonData[i]["Call_lat"]}</td><td>${jsonData[i]["Call_lng"]}</td><td>${jsonData[i]["Retlat"]}</td><td>${jsonData[i]["RetLng"]}</td><td>${jsonData[i]["Distance"]}</td><td>${jsonData[i]["OrderValue"]}</td><td>${jsonData[i]["Remarks"]}</td></tr>`;

                let str = `<tr><td align="right">${i + 1}</td><td>${jsonData[i]["Territory"]}</td><td>${jsonData[i]["Sf_Code"]}</td><td>${jsonData[i]["SF_Name"]}</td><td>${jsonData[i]["Activity_Date"]}</td><td>${jsonData[i]["RouteCode"]}</td><td>${jsonData[i]["RouteName"]}</td><td>${jsonData[i]["RetailerCode"]}</td><td>${jsonData[i]["RetailerName"]}</td><td>${jsonData[i]["Mobile"]}</td><td>${jsonData[i]["StartTime"]}</td><td>${jsonData[i]["EndTime"]}</td><td>${jsonData[i]["InStoreTime"]}</td><td>${jsonData[i]["Class"]}</td><td>${jsonData[i]["Channel"]}</td><td>${jsonData[i]["DistributorName"]}</td><td>${jsonData[i]["Call_lat"]}</td><td>${jsonData[i]["Call_lng"]}</td><td>${jsonData[i]["Retlat"]}</td><td>${jsonData[i]["RetLng"]}</td><td>${jsonData[i]["Distance"]}</td><td>${jsonData[i]["OrderValue"]}</td><td>${jsonData[i]["Remarks"]}</td></tr>`;

                strArr[i] = str;
            }
            $("#FFTbl>tbody").append(strArr.join(''));
        }
        $(document).ready(function () {
            //$('#loadover').show();
            setTimeout(function () {
                //loadData();
            }, 1000);
            $(document).ajaxStop(function () {
                //$('#loadover').hide();
            });
            $('#btnExport').click(function () {
                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };
                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };
                htmls = document.getElementById("content").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'Call Compliance Report' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
        });
    </script>
</body>
</html>