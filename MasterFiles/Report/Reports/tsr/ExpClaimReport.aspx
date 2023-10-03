<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpClaimReport.aspx.cs" Inherits="MasterFiles_Reports_tsr_ExpClaimReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Expense</title>
        <link href="../../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="../../../css/style1.css" rel="stylesheet" type="text/css" />
        <link href="../../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="../../../css/Stockist.css" rel="stylesheet" type="text/css" />
        <link href="../../../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
        <link href="../../../css/Spinner2.css" rel="stylesheet" type="text/css" />
        <script src="../../../js/jquery.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="../../../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../../../js/plugins/datatables/dataTables.bootstrap.js"></script>	
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.24.0/moment.min.js"></script>
        <style type="text/css">
            #ExpenseEntry th{text-align:left;}
            .picc {
                width: 20px !important;
                height: 20px !important;
                color: blue !important;
            }
        @media print{
            .mheader{
                position:absolute !important;
            }
        }
    </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                 <center>
                     <asp:Panel ID="Panel1" runat="server" Visible="false">
                         <table width="100%">
                             <tr>
                                 <td width="80%"></td>
                                 <td align="right">
                                     <table>
                                         <tr>
                                             <td>
                                                 <asp:Button ID="Printbtn" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                                       />
                                             </td>
                                             <td>
                                                 <asp:Button ID="btnExcel1" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                                     OnClick="btnExcel_Click" />
                                             </td>
                                             <td>
                                                 <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                                     OnClick="btnPDF_Click" Visible="false" />
                                             </td>
                                             <td>
                                                 <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                                     OnClientClick="RefreshParent();" />
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>
                     <div class="row mheader" style="position: fixed;top: 0px;z-index: 1;background: #fff;display: block;width: 100%;padding: 5px;box-shadow: 0 0px 5px 0px black;">
                         <div class="col-lg-12 sub-header"><span style="float:right">
                              <a href="#" class="btn btn-primary btn-update btnExcel" id="btnExcel">Excel</a>&nbsp;
                             <a href="#" class="btn btn-primary btn-update" id="btnprint">Print</a>&nbsp;
                             <a href="#" id="cls" class="btn btn-danger btn-update">Close</a></span></div>
                     </div>
                     <div class="card" style="margin: 15px 0px 0px 0px;">
                         <div class="card-body table-responsive">
                             <table border="0" width="100%">
                             <tr align="center">
                                 <td>
                                     <asp:Label ID="Label1" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                                     <span style="color: Red"></span>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="center">
                                     <asp:Label ID="lblTitle" runat="server" Text="" Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                                 </td>
                             </tr>                    
                         </table>
                             <div class="row">
                                 <div  style="overflow:auto;">
                                     <table class="table table-hover ExpenseEntry" id="ExpenseEntry" style="width: 100% !important;">
                                         <thead class="text-warning" id="tHDExp" style="white-space: nowrap;">
                                             <tr>   
                                                 <th>S.No</th>
                                                 <th>State</th>
                                                 <th>Zone</th>
                                                 <th>Area</th>
                                                 <th>User</th>
                                                 <th>HQ</th>
                                                 <th>Designation</th>
                                                 <th>Claim Date</th>
                                                 <th style="padding:3px 0px;text-align:center" >
                                                     <span style="color: #12a2a9;">Additional Expense</span>
                                                     <table style="width: 100% !important;">
                                                         <tr>
                                                             <td style="text-align:left">Expense</td>
                                                             <td style="text-align:center">Image</td>
                                                             <td style="text-align:right">Amount</td>
                                                         </tr>
                                                     </table>
                                                 </th>     
                                                 <th style="text-align:right">Amount</th>
                                             </tr>
                                         </thead>                                         
                                         <tbody id="tBdyExp">

                                         </tbody>
                                         <tfoot>
                                             <tr>
                                                 <th></th>
                                                 <th></th>
                                                 <th></th>
                                                 <th></th>
                                                 <th></th>
                                                 <th></th>
                                                 <th></th>
                                                 <th style="text-align:right">Total</th>
                                                 <th></th>                                                                          
                                                 <th style="text-align:right" orgval="0" id="tTAmt">0.00</th>
                                                 <th class="hremarks"></th><th class="hremarks"></th></tr>

                                         </tfoot>
                                         
                                     </table>
                                 </div>
                             </div>
                         </div>
                     </div>

                     <asp:Panel ID="pnlContents" runat="server" Width="100%" Visible="false">
                         <table border="0" width="100%">
                             
                             <tr>
                                 <td>
                                     <asp:GridView ID="GridView1" runat="server" Width="100%"   ShowHeader="false" Font-Size="10pt"  
                                         HorizontalAlign="Center" OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated"  
                                         OnRowDataBound="GridView1_RowDataBound" BorderWidth="1px" CellPadding="2" 
                                         EmptyDataText="No Data found for View" AutoGenerateColumns="true" BackColor="#ffffe0" 
                                         RowStyleCssClass="gridpager" HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid" 
                                         Style="padding: 3px 6px; white-space: nowrap;" CssClass="mGrid"  ShowFooter="true">                               
                                         <Columns>

                                         </Columns>
                                         <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" 
                                             BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                     </asp:GridView>
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>
                     <br />
                 </center>
             </div>
            </form>
            <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js" type="text/javascript"></script>
            <script src="https://cdn.rawgit.com/rainabba/jquery-table2excel/1.1.0/dist/jquery.table2excel.min.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript">
                var ExpDets = []; DDetail = []; ExpRWs = []; SFDets = []; $UsrAlws = [];
                var $trTucl = []; $UsrMAlws = []; $ArApp = [];
                var $ExpSFMn = []; $UsrDAdd = [];
               
                var vars = [], hash;
                var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                console.log(hashes);
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[1]);
                    //vars[hash[0]] = hash[1];
                }
                console.log(vars);
                var div_code = '<%=Session["div_code"]%>';
                var sf_code = vars[0];
                //alert(sf_code);
                var Fdate = vars[4];
                var Tdate = vars[5];
                var stcode = vars[9];
                //var $fFR = '<=sf_code%>';
                var transExpmnth = [];
                var transExpTp = [];
                var $Dv = 0, $Plc = '', $RtTwns = '', $PTwn = '', $pTR = '', $Fl = 0, $ONo = 1, $RowNo = 0, $RvNo = 1, $chksfc = '',
                    $tTFa = 0, $tTAD = 0, $tTBD = 0, $tTDh = 0, $ptTAD = 0, $ptTBD = 0,
                    $DwsSl = 1, $pDwsSl = 1, $OSRT = 0, $StyPls = '', $SPls = '', $AlwAmt, $AlwDets = []
                var $FR = '', $Pdt, $fa = 2; $tWT = ''; $tTAlw = 0; $tTAmt = 0; $tTFare = 0; $tTAdyAlw = 0, $cWT = '', $Auto = 0, $rejenable = 0, $MAuto = 1;
                var HQC = 0, EXC = 0, OSC = 0, FWKC = 0, OTHC = 0;

                function ReloadTable() {
                    $("#tBdyExp").html("");
                   
                    ExpItem = {}; 

                    $tTAmt = 0;

                    $trTAdcol = [];
                    var vlength = -2;
                    
                    for ($i = 0; $i < ExpDets.length; $i++) {
                        $RowNo = ($i + 1);
                        //alert($RowNo);
                        ExpItem = ExpDets[$i];
                        //if (ExpItem.ClaimDate == undefined) {
                        //    ExpItem = ExpDets[$i];
                        //} else {
                        //    ExpItem.ClstrName += "," + ExpDets[$i].ClstrName
                        //}

                        NAdt = ""; nItm = null;
                        if (($i + 1) < ExpDets.length) { NAdt = ExpDets[$i + 1].ClaimDate; nItm = ExpDets[$i + 1] }
                        //if ($Auto == 0) {
                        //    CalcDis((($i == 0) ? null : ExpDets[$i - 1]), ExpDets[$i], nItm);
                        //}
                        //else {

                        //}

                        tr = $("<tr></tr>");
                        var filteredTp = transExpTp.filter(function (a) {
                            return a.TPdate == ExpItem.ClaimDate;
                        });

                      

                        $s = "<td class='SNo'>" + $RowNo + "</td><td class='StateName'>" + ExpItem.StateName + "</td><td class='Zone_name'>" + ExpItem.Zone_name + "</td><td class='Area_name'>" + ExpItem.Area_name + "</td><td class='SF_Name'>" + ExpItem.SF_Name + "</td><td class='SF_Hq'>" + ExpItem.SF_Hq + "</td><td class='Designation'>" + ExpItem.Designation + "</td><td class='E_Dt'>" + ExpItem.ClaimDate + "</td>";

                        $s += "<td><table style='width: 100%;'>";
                        $uAllws = $UsrAlws.filter(function (a) { return (a.eDate == ExpItem.ClaimDate); });
                        var $udAlw = 0; var $cTAmt = 0;
                        for ($j = 0; $j < $uAllws.length; $j++) {
                            var ToDate = $uAllws[$j].eDate;
                            var sfcode = $uAllws[$j].sf_code;
                            if (sfcode == ExpItem.SF_Code && ToDate == ExpItem.ClaimDate) {
                                $udAlw = parseFloat($uAllws[$j].Amt);
                                if (isNaN($udAlw)) $udAlw = 0;

                                if ($uAllws[$j].Image_Url != '') {
                                    //trimg = '<a href="' + $uAllws[$j].ImageUrl + '" target="_blank"><img alt="" id="img12" width="20" height="20" src="' + $uAllws[$j].ImageUrl + '" /></a>';
                                    trimg = '<img width="30" height="30" src="' + $uAllws[$j].ImageUrl + '" alt="" class="picc" />';
                                }
                                else {
                                    trimg = '';
                                }

                                $cTAmt += $udAlw; //$trTucl[$j] += $udAlw;

                                $s += "<tr class='mtbl'><td class='E_DyUExp' align=\"left\"> " + ($uAllws[$j].expName) + "</td><td class='E_DyUExp' align=\"center\"> " + trimg + "</td><td class='E_DyUExpAmt'  expDt='" + $uAllws[$j].eDate + "' expcode='" + $uAllws[$j].expCode + "'  align=\"right\">" + (($udAlw).toFixed(2)) + "</td></tr>";
                            }
                        }
                        $s += "</table></td><td class='tamnt'  orval='" + $cTAmt + "' align=\"right\">" + $cTAmt + "</td>";
                        $(tr).html($s);
                        $("#tBdyExp").append(tr);
                        $tTAmt += $cTAmt;
                        //if (ExpItem.ClaimDate != NAdt) {
                        //    if ((ExpItem.ClaimDate != frecord) && (ExpItem.ClaimDate != lrecord)) {

                        //       // $tTFare += (($Auto == 0) ? ($Dv * FareDet[0].Fare) : (Number(ExpItem.ExpFare)));
                        //        //$tTAlw += (($Auto == 0) ? ($AlwAmt) : Number(ExpItem.ExpDA))
                        //        //$tTAmt += $cTAmt;
                        //    }
                        //    //ExpItem = {}; $Dv = 0; $Plc = ''; $tTBD = 0; $tTAD = 0; $ptTAD = 0; $ptTBD = 0; $DwsSl = 1; $pDwsSl = 1; $StyPls = ''; $pDs = 0;
                        //}
                        
                    }
                    $("#tTAmt").html($tTAmt.toFixed(2));
                }

               
                function loadData() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        //   async: false,
                        url: "ExpClaimReport.aspx/GetSFDetails",
                        data: "{'div_code': '" + div_code + "' ,'sf_code':'" + sf_code + "','Fdate':'" + Fdate + "','Tdate':'" + Tdate + "','stcode':'" + stcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            ExpDets = JSON.parse(data.d) || [];

                            console.log(ExpDets);

                            ReloadTable(); 

                        },
                        error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                    });
                }

                function GetExpenseData() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ExpClaimReport.aspx/GetUserExpense",
                        data: "{'div_code': '" + div_code + "' ,'sf_code':'" + sf_code + "','Fdate':'" + Fdate + "','Tdate':'" + Tdate + "','stcode':'" + stcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            $UsrAlws = JSON.parse(data.d) || [];
                            console.log($UsrAlws);
                        },
                        error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                    });                   
                }


                function closew() {
                    $('#cphoto1').css("display", 'none');
                }
                function showERROR(jqXHR, exception) {
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }

                $('#btnprint').on('click', function () {
                    var body = $(document).find('body');
                    $(body).find('.sub-header').find('span').hide();
                    window.print(body);
                    $(body).find('.sub-header').find('span').show();
                });

                $('#cls').on('click', function () {
                    window.close();
                });

                $(document).ready(function () {

                    $(document).on('click', '.picc', function () {
                        var photo = $(this).attr("src");
                        //alert('hi');
                        $('#photo1').attr("src", $(this).attr("src"));
                        $('#cphoto1').css("display", 'block');
                    });

                    dv = $('<div style="z-index: 10000000;position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
                    $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
                    $("body").append(dv);

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ExpClaimReport.aspx/GetSFDetails",
                        data: "{'div_code': '" + div_code + "' ,'sf_code':'" + sf_code + "','Fdate':'" + Fdate + "','Tdate':'" + Tdate + "','stcode':'" + stcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            SFDets = JSON.parse(data.d) || [];
                            console.log(SFDets);
                        },
                        error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                    });

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ExpClaimReport.aspx/GetUserExpense",
                        data: "{'div_code': '" + div_code + "' ,'sf_code':'" + sf_code + "','Fdate':'" + Fdate + "','Tdate':'" + Tdate + "','stcode':'" + stcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            $UsrAlws = JSON.parse(data.d) || [];
                            console.log($UsrAlws);
                        },
                        error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                    });

                    loadData();
                    GetExpenseData();
                   
                    

                });

                $('.btnExcel').click(function () {
                    ExportToExcel('ExpenseEntry');
                    return false;
                });

                function ExportToExcel(Id) {

                    loadData();
                    GetExpenseData();

                    var htmltable = '';
                    var $totamunt = 0;
                    if (SFDets.length > 0 && $UsrAlws.length > 0) {

                        htmltable = "<html><head><title>Expense Claim Details</title></head><body>";

                        htmltable += "<table style='width: 100% !important;border=2px !important;' >";

                        htmltable += "<thead><tr><th>S.No</th><th>State</th><th>Zone</th><th>Area</th><th>User</th><th>HQ</th><th>Designation</th><th>Claim Date</th>";
                        htmltable += "<th style='padding:3px 0px; text-align:center'>";
                        htmltable += "<span style='color: #12a2a9;'>Additional Expense</span>";
                        htmltable += "<table style='width:100 %;'><tr>";
                        htmltable += "<td style='text-align:left'>Expense</td>";
                        htmltable += "<td style='text-align:left'>Image</td><td style='text-align:right'>Amount</td></tr></table></th>";
                        htmltable += "<th style='text-align:right'>Amount</th>";
                        htmltable += "</tr></thead>";

                        htmltable += "<tbody>";

                        ExpItem = {};
                        $trTAdcol = [];
                        var vlength = -2;
                        for ($i = 0; $i < ExpDets.length; $i++) {
                            $RowNo = ($i + 1);
                            //alert($RowNo);
                            ExpItem = ExpDets[$i];
                            
                            NAdt = ""; nItm = null;
                            if (($i + 1) < ExpDets.length) { NAdt = ExpDets[$i + 1].ClaimDate; nItm = ExpDets[$i + 1] }
                            
                            //tr = $("<tr></tr>");
                            htmltable += "<tr>";
                            htmltable += "<td class='SNo' align=\"center\">" + $RowNo + "</td><td class='StateName'>" + ExpItem.StateName + "</td>";
                            htmltable += "<td class='Zone_name'>" + ExpItem.Zone_name + "</td><td class='Area_name'>" + ExpItem.Area_name + "</td>";
                            htmltable += "<td class='SF_Name'>" + ExpItem.SF_Name + "</td><td class='SF_Hq'>" + ExpItem.SF_Hq + "</td>";
                            htmltable += "<td class='Designation'>" + ExpItem.Designation + "</td><td class='E_Dt'>" + ExpItem.ClaimDate + "</td>"

                          
                            htmltable += "<td><table style='width: 100%;'>";
                          
                            $uAllws = $UsrAlws.filter(function (a) { return (a.eDate == ExpItem.ClaimDate); });
                            var $udAlw = 0; var $cTAmt = 0; 
                            for ($j = 0; $j < $uAllws.length; $j++) {
                                var ToDate = $uAllws[$j].eDate;
                                var sfcode = $uAllws[$j].sf_code;
                                if (sfcode == ExpItem.SF_Code && ToDate == ExpItem.ClaimDate) {
                                    $udAlw = parseFloat($uAllws[$j].Amt);
                                    if (isNaN($udAlw)) $udAlw = 0;
                                   
                                    if ($uAllws[$j].Image_Url != '') {

                                        //imgurl = '<a href="' + $uAllws[$j].ImageUrl + '" target="_blank"><img  width="20" height="20" src="' + $uAllws[$j].ImageUrl + '" alt="" /></a>';

                                        //imgurl = '<a style="color: blue !important;" id="shrflink" href="' + $uAllws[$j].ImageUrl + '" />';
                                        trimg = '<a href="' + $uAllws[$j].ImageUrl + '" target="_blank"><img alt="" id="img12" width="20" height="20" src="' + $uAllws[$j].ImageUrl + '" /></a>';
                                        //trimg = '<img style="width:50px !important;height:50px !important;"  src="' + $uAllws[$j].ImageUrl + '" alt="" />';
                                    }
                                    else {
                                        imgurl = '';
                                        trimg = '';
                                    }

                                    $cTAmt += $udAlw; //$trTucl[$j] += $udAlw;
                                    htmltable += "<tr class='mtbl1'><td class='E_DyUExp1' align=\"left\">" + ($uAllws[$j].expName) + " </td><td class='E_DyUExp1' align=\"center\">" + trimg + " </td>";
                                    htmltable += "<td class='E_DyUExpAmt1' align=\"right\">" + (($udAlw).toFixed(2)) + "</td></tr>";
                                    
                            
                                }
                            }
                            htmltable += "</table></td><td class='tamnt1'  orval='" + $cTAmt + "' align=\"right\">" + $cTAmt + "</td>";
                            
                            htmltable += "</tr>";
                            $totamunt += $cTAmt;
                            console.log($totamunt);
                            $tTAmt = $cTAmt;
                        }
                        
                        htmltable += "</tbody>";
                        htmltable += "<tfoot><tr><th></th> <th></th><th></th><th></th><th></th><th></th><th></th> ";
                        htmltable += "<th style='text-align:right'>Total</th><th></th>";
                        htmltable += "<th style='text-align:right' orgval='0' id='tTAmt1'>" + $totamunt.toFixed(2) + " </th>";
                        htmltable += "<th class='hremarks'></th><th class='hremarks'></th></tr>";
                        htmltable += "</tfoot>";
                        htmltable += "</table>";

                        htmltable += "</body></html>";

                        var textRange;
                        var j = 0;
                        var tab = document.getElementById(Id);

                        console.log(htmltable);

                        var tab_text = htmltable; //= '2px'"<table border='2px'><tr>";

                        //var headerRow = $('[id*=ExpenseEntry] tr:first');
                        //tab_text += headerRow.html() + '</tr><tr>';
                        //var rows = $('[id*=ExpenseEntry] tr:not(:has(th))');
                        //var count = $('#tBdyExp').children('tr').length;

                        ////console.log(count);
                        //for (j = 0; j < count.length; j++) {
                        //    /*if ($(rows[j]).find('input').is(':checked')) {*/
                        //    tab_text = tab_text + count[j].innerHTML + "</tr>";
                        //    /*}*/
                        //}
                        //tab_text = tab_text + "</table>";

                        //tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, ""); //remove if u want links in your table
                        //tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
                        //tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
                        var ua = window.navigator.userAgent;
                        var msie = ua.indexOf("MSIE");
                        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
                        {
                            txtArea1.document.open("txt/html", "replace");
                            txtArea1.document.write(tab_text);
                            txtArea1.document.close();
                            txtArea1.focus();
                            sa = txtArea1.document.execCommand("SaveAs", true, "Expense_Claim_Detail.xls");
                            //sa = txtArea1.document.execCommand("SaveAs", true, Id + ".xls");
                        }
                        else {                 //other browser not tested on IE 11
                            sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
                        }
                        return (sa);

                    }                    
                    
                }            
                
            </script>
    </body>
</html>
