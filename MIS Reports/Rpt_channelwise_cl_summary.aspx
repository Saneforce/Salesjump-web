<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Rpt_channelwise_cl_summary.aspx.cs" Inherits="MIS_Reports_Rpt_channelwise_cl_summary" %>


<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
         #OrderList {
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

        .a {
            line-height: 22px;
            padding: 3px 4px;
            border-radius: 7px;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
             text-align: center;
             font-size: 12px;
        }
    </style>
<body>
    <form id="form1" runat="server">
        <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" >
                <tr>
               <td width="70%" align="center" >
                    <asp:Label ID="lblHead" Text="" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server" Font-Size="Medium"></asp:Label>
				</td>
              </tr>
            </table> 
			
           <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Manager Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>	
         </asp:Panel><br /><br />
                 <center>  
                     <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                   <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
             <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                    </thead>
                    <tbody>
                    </tbody>
                  <tfoot></tfoot>
                </table>
        
            </div>
        </div>
                   </center>
    </div>
       
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var prodcl = []; var chnl = []; var orderval = [];
        var totcl = []; 
        
    
        function ReloadTable() {
           
            $("#OrderList TBODY").html("");
            var point = 0;
            var str = '';
            var sth = ''; var tt = []; var pc = []; var totorder = 0;
            sth += "<tr><th>S.NO</th><th>Employee Name</th>";
            for (c = 0; c < chnl.length; c++) {
                sth += "<th>" + chnl[c].Doc_Special_Name + "</th>";
            }
            sth += "<th>Total Call</th>";
            for (c = 0; c < chnl.length; c++) {
                sth += "<th>" + chnl[c].Doc_Special_Name + "</th>";
            }
            sth += "<th>Productive Call</th><th>OrderValue</th>";
            for (var i = 0; i < orderval.length; i++) {
                var prodcall = 0;  var totalcall = 0;
                str += "<tr><td>" + (i + 1) + "</td><td>" + orderval[i].SF_Name + "</td>";
           
            for (var k = 0; k < chnl.length; k++) {
                tt = totcl.filter(function (a) {
                    return a.Sf_Code == orderval[i].SF_Code && a.Doc_Special_Code == chnl[k].Doc_Special_Code;
                });
                if (tt.length > 0) {
                    totalcall += tt[0].TCall;
                }
                str += "<td>" + (tt.length > 0 ? tt[0].TCall : '') + "</td>"
                }
                str += "<td>" + totalcall + "</td>";
                
                for (var k = 0; k < chnl.length; k++) {
                    pc = prodcl.filter(function (a) {
                        return a.Sf_Code == orderval[i].SF_Code && a.Doc_Special_Code == chnl[k].Doc_Special_Code;
                    });
                    if (pc.length > 0) {
                        prodcall += pc[0].Retailer_Count;
                    }

                    str += "<td>" + (pc.length > 0 ? pc[0].Retailer_Count : '') + "</td>"
                }
              
                str += "<td>" + prodcall +"</td>";
                totorder += orderval[i].Order_Value;
                str += "<td>" + orderval[i].Order_Value.toFixed(2) + "</td></tr>"
            }
            col = chnl.length + chnl.length + 4;
            $("#OrderList TBODY").append(sth);
            $("#OrderList TBODY").append(str);
            $('#OrderList tfoot').html("<tr><td colspan=" + col + "  style='text-align: center;font-weight: bold'>Total  Value</td><td><label>" + totorder.toFixed(2) + "</label></td></tr>");

        }
   
        function Filltotcl() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Rpt_channelwise_cl_summary.aspx/Filltotcl",
                async: false,
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    totcl = AllOrders;
                }
            });
        } 
        function Fillprodcl() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Rpt_channelwise_cl_summary.aspx/Fillprodcl",
                async: false,
                dataType: "json",
                success: function (data) {
                    prodclr = JSON.parse(data.d) || [];
                    prodcl = prodclr; 
                }
            });
        } 
        function getchannel() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Rpt_channelwise_cl_summary.aspx/Fillchannel",
                async: false,
                dataType: "json",
                success: function (data) {
                 channel = JSON.parse(data.d) || [];
                 chnl = channel;
                }
            });
        } 
        function getorderval() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Rpt_channelwise_cl_summary.aspx/Fillorderval",
                async: false,
                dataType: "json",
                success: function (data) {
                    ordervals = JSON.parse(data.d) || [];
                    orderval = ordervals;
                }
            });
        } 
        $(document).ready(function () {
           getorderval();
           Filltotcl();
           Fillprodcl();
           getchannel();
          ReloadTable();
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
            htmls = document.getElementById("OrderList").innerHTML;


            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Channelwise_Summary_Report' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
    </script>
</body>
</asp:Content>