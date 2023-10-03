<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportFinal.aspx.cs" Inherits="MIS_Reports_ReportFinal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="Stylesheet" href="../css/kendo.common.min.css" />
    <link rel="Stylesheet" href="../css/kendo.default.min.css" />
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' /> 
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 26px;
        }
    </style>
   <%-- <script type="text/javascript">
    var queryString = new Array();
    $(function () {
        if (queryString.length == 0) {
            if (window.location.search.split('?').length > 1) {
                var params = window.location.search.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var key = params[i].split('=')[0];
                    var value = decodeURIComponent(params[i].split('=')[1]);
                    queryString[key] = value;
                }
            }
        }
        if (queryString["StartDate"] != null && queryString["EndDate"] != null) {
            var data = "<u>Values from QueryString</u><br /><br />";
            data += "<b>From Date:</b> " + queryString["StartDate"] + " <b>To Date:</b> " + queryString["EndDate"];
            $("#lblData").html(data);
        }
    });
    </script>--%>     
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="Stylesheet" href="../css/kendo.common.min.css" />
    <link rel="Stylesheet" href="../css/kendo.default.min.css" />
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />  
    <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>    
    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
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
        });
    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="60%" align="center">
                            <asp:Label ID="lblHead" Text="Retailer Order View" Font-Bold="true" Style="color: #3F51B5; padding-right: 217PX;" Font-Size="Large" Font-Underline="true"
                                runat="server"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click" />
                                        <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf" Visible="false" />
                                        <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                            OnClick="btnExcel_Click" />
                                        <%--<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>--%>
                                        <asp:LinkButton ID="btnClose" runat="server" type="button" Style="padding: 0px 20px;"
                                        href="javascript:window.open('','_self').close();" class="btn btnClose"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
              <div class="card">            
             <div class="card-body table-responsive">
                  <table class="auto-style1" style="font-family:Arial">
                <tr>
                    <td><div style="white-space:nowrap"><b>RETAILER NAME:</b></div></td>
                    <td>
                        <asp:Label ID="lblRetailerName" runat="server" Text=""></asp:Label>
                    </td>
                    <td><div style="white-space:nowrap"><B>NAME OF PERSON:</B> </div>  </td>
                    <td>
                      <asp:Label ID="lblRetailerPerson" runat="server" Text=""></asp:Label>  
                    </td>
                     <td><div style="white-space:nowrap"><b>CONTACT NO:</b></div></td>
                    <td>
                          <asp:Label ID="lblRetailerContact" runat="server" Text=""></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td><div style="white-space:nowrap"><b>DELIVERY ADDRESS:</b></div></td>
                    <td>
                      <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                    </td>
                     <td class="auto-style2"><div style="white-space:nowrap"><b>From DATE:</b></div> </td>
                    <td class="auto-style2">
                       <asp:Label ID="lblFromDate" runat="server" Text=""></asp:Label>
                    </td>
                     <td class="auto-style2"><div style="white-space:nowrap"><b>To DATE:</b></div> </td>
                    <td class="auto-style2">
                       <asp:Label ID="lblToDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                
                                   
                <tr>
                   
                </tr>
                <tr>
                    
                </tr>
                <tr>
                   
                </tr>
                <tr>
                   
                </tr>
            </table>
           
                 </div>

        </div>
             <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%" Style="margin-left: 35px">
            <table border="0" id='1' style="margin: auto" width="100%">
                <tr>
                    <td>&nbsp;&nbsp;</td>
                </tr>
                <tr align="left">
                    <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus; padding-left: 180px;">
                   <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" Style="font-family: Andalus; color: Blue;"></asp:Label></td>
                </tr>
                <tr>
                    <td width="100%">
                        <asp:GridView ID="GridView1" runat="server" SkinID="GV" CssClass="newStly" ShowFooter="false">
                <Columns>
                    <asp:BoundField HeaderText="EAN _Code" DataField="Product_Detail_Code">
                        <ItemStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Brand Name" DataField="Product_Brd_Name">
                        <ItemStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Product Name" DataField="Product_Name">
                        <ItemStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="MRP" DataField="Retailor_Price">
                        <ItemStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Closing Stock" DataField="CLOSING">
                        <ItemStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Order Qty" DataField="Quantity">
                        <ItemStyle Font-Bold="True" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>            
        </div>         
    </form>
</body>
</html>
