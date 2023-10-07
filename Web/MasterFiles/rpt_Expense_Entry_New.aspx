<%@ Page Title="Expense Report" Language="C#" AutoEventWireup="true" CodeFile="rpt_Expense_Entry_New.aspx.cs"
    Inherits="MasterFiles_rpt_Expense_Entry_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/style1.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/Stockist.css" rel="stylesheet" type="text/css" />
    <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
    <link href="../css/Spinner2.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.24.0/moment.min.js"></script>
	  <script src="../alertstyle/jquery-confirm.min.js"></script>
    <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <style type="text/css">
        #ExpenseEntry th {
            text-align: left;
        }

        .picc {
            width: 20px;
        }

        @media print {
            .mheader {
                position: absolute !important;
            }
        }
		#main div {
         width: 50px;
        height: 50px;
        margin: 2px;
        margin-left: 4px;
        }
        #main div:hover {
            border:2px solid #00bcd4;
        }
        
        .exmpl{
             border:2px solid #00bcd4;
        }
        .modal {
			position: fixed;
			top: 97px;
			left: 0;
			background-color: black;
			z-index: 99;
			opacity: 1;
			filter: alpha(opacity=80);
			-moz-opacity: 0.8;
			min-height: 100%;
			width: 100%;
			 overflow: initial;
			overflow-y: initial;
		}
    </style>
</head>
<body style="margin: 15px; margin-top: 50px">
    <form id="form1" runat="server">
        <div style="text-align: left; padding: 2px 50px;">
            <asp:HiddenField ID="sfname" runat="server" />
        </div>
        <div class="row mheader" style="position: fixed; top: 0px; z-index: 1; background: #fff; display: block; width: 100%; padding: 5px; box-shadow: 0 0px 5px 0px black;">
            <div class="col-lg-12 sub-header">Expense statement for the month of <%=new DateTime(int.Parse(Yr), int.Parse(Mn), 1).ToString("MMM")%> - <%=Yr%><span style="float: right">
                <button type="button" class="btn btn-success btn-update" id="btsav">Save Draft</button>&nbsp;
                <button type="button" class="btn btn-success btn-update" id="btsave" onclick="svExpense()">Approve</button>&nbsp;
                <button type="button" class="btn btn-success btn-danger btnrjt" id="rjctall" onclick="rejectexpense(this)">Reject All</button>&nbsp;<a href="#" class="btn btn-primary btn-update" id="btnprint">Print</a>&nbsp;<a href="#" id="cls" class="btn btn-danger btn-update">Close</a></span></div>
        </div>
        <div id="prtdiv" style="display: none;"></div>

        <div class="row FF">
            <div class="col-sm-10">
                <div class="card" style="margin: 15px 0px 0px 0px;">
                    <div class="card-body table-responsive">
                        <div class="row">
                            <div class="col-sm-6">
                                <table>
                                    <tr>
                                        <td>Name</td>
                                        <td>:</td>
                                        <td id="SFNm"></td>
                                    </tr>
                                    <tr>
                                        <td>Emp ID</td>
                                        <td>:</td>
                                        <td id="SFID"></td>
                                    </tr>
                                    <tr>
                                        <td>Designation</td>
                                        <td>:</td>
                                        <td id="SFDesig"></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-sm-3">
                                <table>
                                    <tr>
                                        <td>Headquarter</td>
                                        <td>:</td>
                                        <td id="SFHQ"></td>
                                    </tr>
                                    <tr>
                                        <td>Department</td>
                                        <td>:</td>
                                        <td id="SFDept"></td>
                                    </tr>
                                    <tr>
                                        <td>Manager</td>
                                        <td>:</td>
                                        <td id="SFMGR"></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-sm-3" style="text-align: end;">
                                <input type="button" onclick="shwTp()" id="showTp" style="display:none;" class="btn btn-primary" value="Tp Devation" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2">
                <div class="card" style="margin: 15px 0px 0px 0px;">
                    <table>
                        <tbody>
                            <tr>
                                <td style="background: #4697cf; border-top-left-radius: 5px; border-top-right-radius: 5px; color: #fff; font-size: 14px; font-weight: bold;">Period</td>
                            </tr>
                            <tr>
                                <td id="FDt" style="padding: 10px; text-align: center; font-weight: bold; letter-spacing: 0.9px;"></td>
                            </tr>
                            <tr>
                                <td id="TDt" style="padding: 10px; text-align: center; font-weight: bold; letter-spacing: 0.9px;"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="card" style="margin: 15px 0px 0px 0px;">
            <div class="card-body table-responsive">
                <div class="row">
                    <div style="overflow: auto;">
                        <table class="table table-hover" id="ExpenseEntry">
                            <thead class="text-warning" id="tHDExp" style="white-space: nowrap;">
                                <tr>
                                    <th>Date</th>
                                    <th>Work Type</th>
                                    <th class="phsfields">As Per TP</th>
                                    <th>Worked Place</th>
                                    <th class="phsfields">Joint Work</th>
                                    <th id="fplace">From Place</th>
                                    <th id="tplace">To Place</th>
                                    <th class="phsfields">Sec Sales</th>
                                    <%-- <th id="ttlCall_cnt" class="CallCnt" style="display:none;">No. of Calls</th>--%>
                                    <th>DA Type</th>
                                    <th class="startendkm">Start KM</th>
                                    <th class="startendkm">End KM</th>
                                    <th style="text-align: right">Distance</th>
                                    <th style="text-align: right">Fare</th>
                                    <th style="text-align: right">DA</th>
                                    <th style="padding: 3px 0px; text-align: center"><span style="color: #12a2a9;">Additional Expense</span><table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left">Expense</td>
                                            <td style="text-align: right">Amount</td>
                                        </tr>
                                    </table>
                                    </th>
                                    <th style="text-align: right">Amount</th>
                                    <th style="text-align: center" id="Remarks">Remarks</th>
                                    <th style="text-align: center" id="Deduct">Add/Deduct</th>
                                </tr>
                            </thead>
                            <tbody id="tBdyExp">
                            </tbody>
                            <tfoot class="text-warning">
                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th class="phsfields"></th>
                                    <th class="phsfields"></th>
                                    <th class="hremarks"></th>
                                    <th class="hremarks"></th>
                                    <th class="phsfields"></th>
                                    <th style="text-align: right">Total</th>
                                    <th></th>
                                    <th class="startendkm"></th>
                                    <th class="startendkm"></th>
                                    <th style="text-align: right"></th>
                                    <th style="text-align: right" id="tFare">0.00</th>
                                    <th style="text-align: right" id="tAlw">0.00</th>
                                    <th></th>
                                    <th style="text-align: right" orgval="0" id="tTAmt">0.00</th>
                                    <th class="hremarks"></th>
                                    <th class="hremarks"></th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6">
                <div class="col-sm-12">
                    <div class="card" style="margin: 15px 0px 0px 0px;">
                        <div class="card-body table-responsive">
                            <table class="table table-hover" id="tbAddDed">
                                <thead>
                                    <tr>
                                        <th>Description</th>
                                        <th style="text-align: right">Amount</th>
                                        <th style="text-align: center"><i class="fa fa-add-on" style="padding: 0px 4px; font-size: 14px; border: 1px solid; border-radius: 10px; line-height: 17px; text-align: center; color: green; font-weight: bold;" onclick="AddRw()">+</i></th>
                                    </tr>
                                </thead>
                                <tbody id="tBdyAddDed">
                                    <tr>
                                        <td style="width: 73%">
                                            <input class="txAddDesc" type="text" style="width: 100%" /></td>
                                        <td id="Td1" style="text-align: right; width: 26%">
                                            <select class="cbAlwTyp ispinner">
                                                <option value='1'>+</option>
                                                <option value='0'>-</option>
                                            </select>
                                            <div class="Spinner-Input">
                                                <div class="Spinner-Value">+</div>
                                                <div class="Spinner-Modal">
                                                    <ul>
                                                        <li val='1'>+</li>
                                                        <li val='0'>-</li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <input type="text" class="txAddDed" pval="0.00" value="" onkeyup="CalcMAddDed(this)" style="text-align: right; width: 70%;" /></td>
                                        <td><i class="fa fa-trash-o" style="padding: 6px 9px; font-size: 17px;" onclick='delRw(this)'></i></td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="font-size: 14px;">Total Amount</th>
                                        <th id="tfAddDedAmt" style="text-align: right; font-size: 14px;"></th>
                                        <th></th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="card" style="margin: 15px 0px 0px 0px;">
                        <div class="card-body table-responsive">
                            <table class="table table-hover" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Days Worked</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="width: 20%;">HQ</td>
                                        <td id="HQC"></td>
                                        <td style="text-align: right;">FieldWork</td>
                                        <td style="text-align: right;" id="FWKC"></td>
                                    </tr>
                                    <tr>
                                        <td>EX</td>
                                        <td id="EXC"></td>
                                        <td style="text-align: right;">Others</td>
                                        <td style="text-align: right;" id="OTHC"></td>
                                    </tr>
                                    <tr>
                                        <td>OS</td>
                                        <td id="OSC"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-1">
            </div>
            <div class="col-sm-5">
                <div class="card" style="margin: 15px 0px 0px 0px;">
                    <div class="card-body table-responsive">
                        <table class="table table-hover" id="gTotal">
                            <thead>
                                <tr>
                                    <th colspan="2" style="color: #12a2a9;">Expense Summary</th>
                                </tr>
                            </thead>
                            <tbody id="tTotSmry">
                                <tr>
                                    <td style="width: 90%">Total Daily Expense</td>
                                    <td id="tGDAmt" style="text-align: right">0.00</td>
                                </tr>
                                <tr>
                                    <td style="width: 90%">Addition ( + )</td>
                                    <td id="tAddAmt" style="text-align: right">0.00</td>
                                </tr>
                                <tr>
                                    <td style="width: 90%">Deduction ( - )</td>
                                    <td id="tDedAmt" style="text-align: right">0.00</td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th style="font-size: 16px;">Payable Amount</th>
                                    <th id="tNGTotAmt" style="text-align: right; font-size: 17px;"></th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
		<div class="modal fade" id="Htlbll" tabindex="-1" style="display: none; z-index: 10000000; background: transparent;" role="dialog" aria-labelledby="Htlbll" aria-hidden="false">
            <div class="modal-dialog" role="document" style="width: 50%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5>Hotel Bill</h5>
                        <button type="button" id="btntimesClose" class="close" style="margin-top: -30px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span></button>
                    </div>
                    <div class="modal-body Hotlbill">
                        <div style="width: 100%; height: 300px;" id="1stdiv"></div>
                        <div class="modal-footer" id="main" style="display: flex;padding-left:0px;flex-direction: row;" ></div>

                    </div>
                </div>
            </div>
        </div>
         <div class="modal" id="TpDv" tabindex="-1" style="display: none; z-index: 10000000; background: transparent;padding: 16px;"" role="dialog" aria-labelledby="TpDv" aria-hidden="false">
            <div class="modal-dialog" role="document" style="width: 100%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5>Tp Devation</h5>
                        <button type="button" id="btnClose" class="close" style="margin-top: -30px;" data-dismiss="modal" aria-label="Close"><span onclick="btnClose()" aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body TpDv">
                        <div class="card" style="overflow:scroll;height:250px;border-bottom:1px solid lightgray;">
                            <div class="card-body table-responsive">
                                <table class="table table-hover" id="Tptbl">
                                    <thead>
                                        <tr>
                                            <th>Sl.No.</th>
                                            <th>Devation Reason</th>
                                            <th>TP Area</th>
                                            <th>TP Date</th>
                                            <th>Tp Approved By</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script language="javascript" type="text/javascript">
            var ExpDets = []; DDetail = []; ExpRWs = []; SFDets = []; $UsrAlws = []; $trTucl = []; $UsrMAlws = []; $ArApp = []; $ExpSFMn = []; $UsrDAdd = []; var $fFR = '<%=SF_Code%>';
            var transExpmnth = [];
            var transExpTp = [];
            var transExpStEndKm = [];
            var $Dv = 0, $Plc = '', $RtTwns = '', $PTwn = '', $pTR = '', $Fl = 0, $ONo = 1, $RvNo = 1, $chksfc = '',
                $tTFa = 0, $tTAD = 0, $tTBD = 0, $tTDh = 0, $ptTAD = 0, $ptTBD = 0,
                $DwsSl = 1, $pDwsSl = 1, $OSRT = 0, $StyPls = '', $SPls = '', $AlwAmt, $AlwDets = []
            var $FR = '', $Pdt, $fa = 2; $tWT = ''; $tTAlw = 0; $tTAmt = 0; $tTFare = 0; $tTAdyAlw = 0, $cWT = '', $Auto = 0, $rejenable = 0, $MAuto = 1, $StartEndKM = 0;
            var HQC = 0, EXC = 0, OSC = 0, FWKC = 0, OTHC = 0;
			var HotelBill = [];
            var AlwTyps = "<option value='HQ'>HQ</option><option value='EX'>EX</option><option value='OS'>OS</option><option value='OX'>OS-EX</option>"
            function AddRw() {
                $trs = $("#tBdyAddDed").find('tr');
                $($trs).eq($($trs).length - 1).after("<tr><td style='width:73%'><input class='txAddDesc' type='text' style='width:100%'/></td><td style='text-align:right;width:26%'><select class='cbAlwTyp' onchange='CalcMAddDed(this)'><option value='1'>+</option><option value='0'>-</option></select> <input type='text' class='txAddDed' pval='0.00' value='' onkeyup='CalcMAddDed(this)' style='text-align:right;width: 70%;' /></td><td><i class='fa fa-trash-o' style='padding: 6px 9px;font-size: 17px;' onclick='delRw(this)'></i></td></tr>");
                $cmb = "<div class='Spinner-Input'><div class='Spinner-Value'>+</div><div class='Spinner-Modal'><ul><li val='1'>+</li><li val='0'>-</li></ul></div></div>";
                $trs = $("#tBdyAddDed").find('tr');
                $($trs[$($trs).length - 1]).find(".cbAlwTyp").addClass("ispinner").after($cmb);

            }
            function AddDRow() {

                for (var i = 0; i < $UsrDAdd.length; i++) {
                    $('#D' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).val($UsrDAdd[i].Descp);
                    $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).val($UsrDAdd[i].AltAmnt);
                    $ftd = $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td');
                    if ($UsrDAdd[i].Spinner_Value == '+') {
                        $($ftd).find('.cbAlwTyp').val(1);
                        $($ftd).find('.dSpinner-Value').text($UsrDAdd[i].Spinner_Value);
                    }
                    if ($UsrDAdd[i].Spinner_Value == '-') {
                        $($ftd).find('.cbAlwTyp').val(0);
                        $($ftd).find('.dSpinner-Value').text($UsrDAdd[i].Spinner_Value);
                    }
                    CalcDAddDed($('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))));
                    if ($ArApp[0].EnDis == 'Disable') {
                        $('#D' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td').html($UsrDAdd[i].Descp);
                        $('#D' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).hide();
                        if ($UsrDAdd[i].AltAmnt != "") {
                            $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td').addClass('dtxAddDed');
                            $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td').attr("mdeduct", $UsrDAdd[i].Spinner_Value + $UsrDAdd[i].AltAmnt);
                            $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td').attr("cbAlwTyp", ($UsrDAdd[i].Spinner_Value == '+') ? 1 : 0);
                            $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td').html($UsrDAdd[i].Spinner_Value + $UsrDAdd[i].AltAmnt);
                        }
                        else
                            $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).closest('td').html($UsrDAdd[i].AltAmnt);
                        $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).hide();
                    }
                    else {
                        $('#D' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).val($UsrDAdd[i].Descp);
                        $('#A' + ($UsrDAdd[i].Expense_Date.replace("T00:00:00", ""))).val($UsrDAdd[i].AltAmnt);
                    }
                }

                //   else {
                //     $('.dtxtdes').each(function () {
                //       $(this).closest('td').empty();
                //   });
                //  $('.dtxAddDed').each(function () {
                //     $(this).closest('td').empty();
                // })
                // }
            }
            function AddMRow() {
                $Mdtot = 0;
                $tAddAmt = $('#tAddAmt').text();
                $tDedAmt = $('#tDedAmt').text();
                $tNGTotAmt = $('#tNGTotAmt').text();
                if ($UsrMAlws.length > 0) {
                    for (var i = 0; i < $UsrMAlws.length; i++) {
                        $trs = $("#tBdyAddDed").find('tr');
                        $($trs).eq($($trs).length - 1).after("<tr><td style='width:73%'><input class='txAddDesc' value='" + $UsrMAlws[i].Exp_Desc + "' type='text' style='width:100%'/></td><td style='text-align:right;width:26%'><select class='cbAlwTyp' onchange='CalcMAddDed(this)'><option value='1'>+</option><option value='0'>-</option></select> <input type='text' class='txAddDed' pval='0.00' value='" + $UsrMAlws[i].Exp_Amnt + "' onkeyup='CalcMAddDed(this)' style='text-align:right;width: 70%;' /></td><td><i class='fa fa-trash-o' style='padding: 6px 9px;font-size: 17px;' onclick='delRw(this)'></i></td></tr>");
                        $cmb = "<div class='Spinner-Input'><div class='Spinner-Value'>" + $UsrMAlws[i].Add_Sub + "</div><div class='Spinner-Modal'><ul><li val='1'>+</li><li val='0'>-</li></ul></div></div>";
                        $trs = $("#tBdyAddDed").find('tr');
                        $($trs[$($trs).length - 1]).find(".cbAlwTyp").addClass("ispinner").after($cmb);
                        if ($UsrMAlws[i].Add_Sub == '+') {
                            $($trs[$($trs).length - 1]).find(".cbAlwTyp").val(1);
                            CalcMAddDed($($trs[$($trs).length - 1]));
                        }
                        if ($UsrMAlws[i].Add_Sub == '-') {
                            $($trs[$($trs).length - 1]).find(".cbAlwTyp").val(0);
                            CalcMAddDed($($trs[$($trs).length - 1]));
                        }
                    }
                    $tNGTotAmt = Number($tNGTotAmt) + Number($tAddAmt) - Number($tDedAmt);
                    $('#Td1').closest('tr').remove();
                }
                if ($ArApp[0].EnDis == 'Disable') {
                    var padd = 0;
                    $('#tBdyAddDed').empty();
                    $('.fa-add-on').closest('th').hide();
                    var addSTR = '';
                    for (var i = 0; i < $UsrMAlws.length; i++) {
                        addSTR += '<tr><td>' + $UsrMAlws[i].Exp_Desc + '</td><td style="text-align: right;">' + ($UsrMAlws[i].Add_Sub + $UsrMAlws[i].Exp_Amnt) + '</td><tr>';
                        padd += (Number($UsrMAlws[i].Add_Sub + $UsrMAlws[i].Exp_Amnt));
                    }
                    $('#tBdyAddDed').append(addSTR);
                }
            }
            function rejectexpense($rdt) {
                var rtype = 0;
                if ($($rdt).text() == 'Reject') {
                    rtype = 0;
                    var rejdt = $($rdt).attr('rejDt');
                    rejdt = rejdt.split('T');
                    console.log(rejdt[0]);
                }
                if ($($rdt).text() == 'Reject All') {
                    rtype = 1;
                    var rejdt = [];
                    rejdt[0] = '';
                }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry_New.aspx/rejectExpense",
                    data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>','RejDt':'" + rejdt[0] + "','Rtype':'" + rtype + "','sEmpID':'<%=EmpID%>'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        window.location.reload();
                    },
                    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                });
            }
            function delRw($x) {
                $trs = $("#tBdyAddDed").find('tr');
                if ($($trs).length > 1) {
                    if ($($x).closest('tr').find('.Spinner-Value').text() == '+') {
                        $('#tAddAmt').text(parseFloat(Number($('#tAddAmt').text()) - Number($($x).closest('tr').find('.txAddDed').val())).toFixed(2));
                        $('#tNGTotAmt').text(parseFloat(Number($('#tNGTotAmt').text()) - Number($($x).closest('tr').find('.txAddDed').val())).toFixed(2));
                        $('#tfAddDedAmt').text(parseFloat(Number($('#tfAddDedAmt').text()) - Number($($x).closest('tr').find('.txAddDed').val())).toFixed(2));
                    }
                    if ($($x).closest('tr').find('.Spinner-Value').text() == '-') {
                        $('#tDedAmt').text(Number($('#tDedAmt').text()) - Number($($x).closest('tr').find('.txAddDed').val()));
                        $('#tNGTotAmt').text(parseFloat(Number($('#tNGTotAmt').text()) + Number($($x).closest('tr').find('.txAddDed').val())).toFixed(2));
                        $('#tfAddDedAmt').text(parseFloat(Number($('#tfAddDedAmt').text()) + Number($($x).closest('tr').find('.txAddDed').val())).toFixed(2));
                    }
                    $($x).closest('tr').remove();
                    $("#tfAddDedAmt").attr('pval', $('#tfAddDedAmt').text());
                }
            }
            function CalcDAddDed($k) {
                var $cVal = 0, $taddv = 0, $tdedv = 0;
                var otmant = parseFloat($($k).closest('tr').find('.tamnt').attr('orval'));
                var ototamnt = parseFloat($("#tTAmt").attr('orgval'));
                var vtNGTotAmt = parseFloat($('#tNGTotAmt').attr('ongtamnt'));
                $taddv = parseFloat($('#tAddAmt').text());
                $tdedv = parseFloat($('#tDedAmt').text());
                var cval = parseFloat($($k).val());
                if (isNaN(cval)) cval = 0;
                vty = $($k).closest('tr').find('.cbAlwTyp').val();
                if (vty == '1') {
                    otmant += cval;
                } else {
					otmant = (isNaN(otmant)) ? 0 : otmant;
                    if (otmant >= cval) {
                        otmant -= cval
                    } else { <%--alert('Cannot Exceed the Expense Amount'); return false;--%> }
                }
                $(".dtxAddDed").each(function () {
                    $xv = parseFloat($(this).attr("mdeduct"));
                    $xv = Math.abs($xv);
                    if (isNaN($xv)) $xv = parseFloat($(this).val());
                    if (isNaN($xv)) $xv = 0;
                    vty = $(this).closest('tr').find('.cbAlwTyp').val();
                    if (vty == undefined) {
                        vty = $(this).attr("cbAlwTyp");
                    }
                    if (vty == '1') {
                        $cVal += $xv;
                    } else {
                        $cVal -= $xv;
                    }
                });
                ototamnt += $cVal;
                vtNGTotAmt = vtNGTotAmt + $taddv - $tdedv + $cVal;
                $($k).closest('tr').find('.tamnt').text(parseFloat(otmant).toFixed(2));
                $("#tTAmt").text(ototamnt.toFixed(2));
                $('#tGDAmt').text(ototamnt.toFixed(2));
                $('#tNGTotAmt').text(vtNGTotAmt.toFixed(2));
                CalcMAddDed($k);
            }
            function CalcMAddDed($x) {
                var $cVal = 0; $taddv = 0; $tdedv = 0;
                $(".txAddDed").each(function () {
                    $xv = parseFloat($(this).val());
                    if (isNaN($xv)) $xv = 0;
                    vty = $(this).closest('tr').find('.cbAlwTyp').val()
                    if (vty == '1') {
                        $taddv += $xv;
                        $cVal += $xv;
                    } else {
                        $tdedv += $xv;
                        $cVal -= $xv;
                    }
                });
                $("#tfAddDedAmt").attr('pval', $cVal);
                $("#tfAddDedAmt").html($cVal.toFixed(2));
                $(".dtxAddDed").each(function () {
                    $xv = parseFloat($(this).attr("mdeduct"));
                    $xv = Math.abs($xv);
                    if (isNaN($xv)) $xv = parseFloat($(this).val());
                    if (isNaN($xv)) $xv = 0;
                    vty = $(this).closest('tr').find('.cbAlwTyp').val();
                    if (vty == undefined) {
                        vty = $(this).attr("cbAlwTyp");
                    }
                    if (vty == '1') {
                        $taddv += $xv;
                        $cVal += $xv;
                    } else {
                        $tdedv += $xv;
                        $cVal -= $xv;
                    }
                });

                $("#tAddAmt").html($taddv.toFixed(2));
                $("#tDedAmt").html($tdedv.toFixed(2));
                var $pVal = parseFloat($("#tfAddDedAmt").attr('pval'));
                if (isNaN($pVal)) $pVal = 0;

                //var $pNTVal = parseFloat($("#tNGTotAmt").html());
                var $pNTVal = parseFloat($("#tNGTotAmt").attr('ongtamnt'));
                if (isNaN($pNTVal)) $pNTVal = 0;
                //$NTot = ($pNTVal - $pVal) + $cVal;
                $NTot = ($pNTVal) + $cVal;
                $("#tNGTotAmt").html($NTot.toFixed(2));
            }
            function calcTExp($x) {
                var $pNTVal = parseFloat($("#tNGTotAmt").html());
                if (isNaN($pNTVal)) $pNTVal = 0;
                var $cVal = parseFloat($($x).val());
                if (isNaN($cVal)) $cVal = 0;
                var $pVal = parseFloat($($x).attr('pval'));
                if (isNaN($pVal)) $pVal = 0;
                $NTot = ($pNTVal - $pVal) + $cVal;
                $("#tNGTotAmt").html($NTot.toFixed(2));
                $($x).attr('pval', $cVal);
            }
            $(document).on("click", ".Spinner-Input", function () {
                $ix = $(this).find(".Spinner-Modal");
                $dsp = $($ix).hasClass("open");
                $(".Spinner-Modal").removeClass("open");
                if ($dsp == false)
                    $($ix).addClass("open");
            })
            $(document).on("click", ".Spinner-Modal>ul>li", function () {
                $vl = $(this).attr('val');
                $(this).closest(".Spinner-Input").parent().find('.ispinner').val($vl);
                $(this).closest(".Spinner-Input").find(".Spinner-Value").html($(this).html())
                CalcMAddDed($(this).closest(".Spinner-Input").parent().find('.txAddDed'))
            })
            $(document).on("click", ".dSpinner-Input", function () {
                $ix = $(this).find(".dSpinner-Modal");
                $dsp = $($ix).hasClass("open");
                $(".dSpinner-Modal").removeClass("open");
                if ($dsp == false)
                    $($ix).addClass("open");
            })
            $(document).on("click", ".dSpinner-Modal>ul>li", function () {
                $vl = $(this).attr('val');
                $(this).closest(".dSpinner-Input").parent().find('.ispinner').val($vl);
                $(this).closest(".dSpinner-Input").find(".dSpinner-Value").html($(this).html())
                CalcDAddDed($(this).closest(".dSpinner-Input").parent().find('.dtxAddDed'))
            });
            function ReloadTable() {
                $("#tBdyExp").html("");
                if ($Auto == 1) {
                    ExpDets = $ExpSFMn;
                }
                ExpItem = {}; $AdDyAlw = $AlwDets.filter(function (a) {
                    return (a.AlwType == 1 && a.AlwUEntry == 0)
                });
                $AdMnthAlw = $AlwDets.filter(function (a) {
                    return (a.AlwType == 2)
                });
                var sfn = $('#<%=sfname.ClientID%>').val();
            $("#SFNm").html(sfn);
            $("#SFID").html((SFDets[0].EmpID == '') ? '-' : SFDets[0].EmpID);
            $("#SFDesig").html(SFDets[0].Desig);
            $("#SFHQ").html(SFDets[0].HQName);
            $("#SFDept").html((SFDets[0].DeptName == '') ? '-' : SFDets[0].DeptName);
            $("#SFMGR").html(SFDets[0].SFMgrNm);
            var frecord = ExpDets[0].ADate;
            var lrecord = ExpDets[ExpDets.length - 1].ADate;
            $("#FDt").html(ExpDets[1].ADate);
            $("#TDt").html(ExpDets[ExpDets.length - 2].ADate);
            $htr = $("#tHDExp").find('tr')[0];
            $trTAdcol = [];
            var vlength = -2;
            for ($j = 0; $j < $AdDyAlw.length; $j++) {
                $ths = $($htr).find('th');
                $($ths).eq($($ths).length - 2 + vlength).before("<th style=\"text-align: right;\">" + $AdDyAlw[$j].AlwSName + "</th>");
                $trTAdcol.push(0)
            }
            for ($i = 0; $i < ExpDets.length; $i++) {
                if (ExpItem.ADate == undefined) {
                    ExpItem = ExpDets[$i];
                } else {
                    ExpItem.ClstrName += "," + ExpDets[$i].ClstrName
                }
                NAdt = ""; nItm = null;
                if (($i + 1) < ExpDets.length) { NAdt = ExpDets[$i + 1].ADate; nItm = ExpDets[$i + 1] }

                var filteredStEndkm = transExpStEndKm.filter(function (a) {
                    return a.ExpDt == ExpItem.ADate;
                });
                let StartKmtag = '';
                let EndKmtag = '';
                if ($Auto == 0) {
                    CalcDis((($i == 0) ? null : ExpDets[$i - 1]), ExpDets[$i], nItm);
                }
                else {

                }
                if ($StartEndKM == 1) {
                    $Dv = (filteredStEndkm[0] != undefined) ? filteredStEndkm[0]["TotalKM"] : 0;
					ExpItem.ExpDist=$Dv;
                    ExpItem.ExpFare = Number((FareDet.length > 0) ? FareDet[0].Fare : 0) * $Dv;
                    StartKmtag = `<span>${(filteredStEndkm[0] != undefined) ? filteredStEndkm[0]["Start_km"] : ""}</span><img class="picc" style="width: 43px !important;" src="${(filteredStEndkm[0] != undefined) ? "../photos/" + filteredStEndkm[0]["Sf_Code"] + "_" + filteredStEndkm[0]["StartPic"] : ""}" />`;
                    EndKmtag = `<span>${(filteredStEndkm[0] != undefined) ? filteredStEndkm[0]["EndKm"] : ""}</span><img class="picc" style="width: 43px !important;" src="${(filteredStEndkm[0] != undefined) ? "../photos/" + filteredStEndkm[0]["Sf_Code"] + "_" + filteredStEndkm[0]["EndPic"] : ""}" />`;
                }
               // if (ExpItem.ADate != NAdt) {
                 //   if (ExpItem.Mn=='<%=Mn %>' && ExpItem.Yr=='<%=Yr%>') {				 
                if (ExpItem.ADate != NAdt) {
                    if ((ExpItem.ADate != frecord) && (ExpItem.ADate != lrecord)) {
                        tr = $("<tr></tr>");
                        let tpRoute = '', tpJointWork = '', secOrder = 0;
                        var $cTAmt = 0, $uAllws = [];
                        //if (ExpItem.WType != 'N/A') {                            
                            var filteredTp = transExpTp.filter(function (a) {
                                return a.TPdate == ExpItem.ADate;
                            });
                            tpRoute = '-';
                            tpJointWork = '-';
                            secOrder = 0;
                            if (filteredTp.length > 0) {
								tpRoute = filteredTp[0].Beat;
								secOrder = filteredTp[0].soval;
								for(var i=0;i<filteredTp.length;i++){									
									tpJointWork += (i != 0) ? ','+filteredTp[i].WorkedWith : filteredTp[i].WorkedWith;										
								}
                            }
                            switch (ExpItem.DAtype) {
                                case 'HQ':
                                    HQC++;
                                    break;
                                case 'EX':
                                    EXC++;
                                    break;
                                case 'OS': case 'OX':
                                    OSC++;
                                    break;
                            }
                            switch (ExpItem.WType) {
                                case 'Field Work':
                                    FWKC++;
                                    break;
                                case 'Not Claimed': case 'No Claim':
                                    break;
                                default:
                                    OTHC++;
                            }

                            alwd = $AlwDets.filter(function (a) {
                                return (a.AlwCd == ($cWT + '1'))
                            });
                            $AlwAmt = (alwd.length > 0) ? parseFloat(alwd[0].AlwAmt) : 0;
                            if (isNaN($AlwAmt)) $AlwAmt = 0;
                            ExpItem.ExpFare = Number(ExpItem.ExpFare);//((FareDet.length > 0) ? ((FareDet[0].Fare) * (ExpItem.ExpDist)) : 0);
                            //var $DAAlwAmt = $AlwDets.filter(function (a) {
                            //     return (a.AlwName == ExpItem.DAtype)
                            // });
                            // if ($DAAlwAmt.length > 0) {
                            //     ExpItem.ExpDA = Number($DAAlwAmt[0].AlwAmt);
                            // }
                            //else {
                            //   ExpItem.ExpDA = Number(ExpItem.ExpDA);
                            //}
                        $cTAmt = ($Auto == 0) ? ($AlwAmt + ($Dv * ((FareDet.length > 0) ? FareDet[0].Fare : 0))) : (Number(ExpItem.ExpFare) + Number(ExpItem.ExpDA) + (isNaN(Number(ExpItem.Hotel_Bill_Amt))?0:Number(ExpItem.Hotel_Bill_Amt)));                        //}
                        var wtypcolor = '#f16465';
                        if (ExpItem.WType == "Not Claimed") {
                            wtypcolor = '#4caf50';
                        }
                        else if (ExpItem.WType == "No Claim") {
                            wtypcolor = '#f16465';
                        }
                        else {
                            wtypcolor = '';
                        }
                        $s = "<td class='E_Dt'>" + ExpItem.ADate + "</td><td class='E_WT' style='color:" + wtypcolor + ";text-align:center;'>" + ExpItem.WType + "</td><td class='phsfields'>" + tpRoute + "</td><td class='E_MDayP'>" + ExpItem.ClstrName + "</td><td class='phsfields'>" + tpJointWork + "</td><td class='twnname'>" + ExpItem.Town_Name + "</td><td class='E_Twn'>" + ExpItem.WorkedPlace + "</td><td class='phsfields'>" + secOrder + "</td><%--<td class='CallCnt' style='text-align:center;display:none;'>" + ExpItem.Calls_Cnt + "</td>--%><td class='E_Typ' align=\"center\"><span>" + (($Auto == 0) ? $cWT : ExpItem.DAtype) + "</span><select class='Atyp' onchange=Atypchange(this) data-aTy='" + ExpItem.DAtype + "'>" + AlwTyps + "</select>" + ((ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed' && ExpItem.WType != 'Leave') ? "<a onclick=datyp(this)><i class='fa fa-pencil' style='float:right;'></i></a>" : '') + "</td><td class='startendkm'>" + ((ExpItem.WType != 'N/A') ? StartKmtag : 0) + "</td><td class='startendkm'>" + ((ExpItem.WType != 'N/A') ? EndKmtag : 0) + "</td><td class='E_Dis' align=\"center\"><span>" + (($Auto == 0) ? (($Dv == 0) ? "" : $Dv) : ((ExpItem.WType != 'N/A') ? ExpItem.ExpDist : 0)) + ((ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed' && ExpItem.WType != 'Leave') ? "</span><input class=\"eddist\" style=\"display:none\;width: 50px;\" type=\"number\" onchange=distchange(this) pdist=\"" + (($Auto == 0) ? (($Dv == 0) ? "" : $Dv) : ExpItem.ExpDist) + "\" value=\"" + (($Auto == 0) ? (($Dv == 0) ? "" : $Dv) : ((ExpItem.WType != 'N/A') ? ExpItem.ExpDist : 0)) + "\" /><a onclick=distyp(this)><i class='fa fa-pencil' style='float:right;'></i></a>" : '') + "</td><td class='E_Fare' align=\"right\">" + (($Auto == 0) ? (($Dv == 0) ? "" : ($Dv * ((FareDet.length > 0) ? FareDet[0].Fare : 0)).toFixed(2)) : ((ExpItem.WType != 'N/A') ? ExpItem.ExpFare : 0)) + "</td><td class='E_Alw' align=\"right\">" + (($Auto == 0) ? ((ExpItem.AlwType != '' || $cWT != '') ? ($AlwAmt).toFixed(2) : "") : ((ExpItem.WType != 'N/A') ? ExpItem.ExpDA : 0)) + "</td>\
						<input type='hidden' class='mot' value='" + ExpItem.Mot + "'/><input type='hidden' class='st_typ' value='" + ExpItem.Stay_Type + "'/><input type='hidden' class='H_Billamnt' value='" + ExpItem.Hotel_Bill_Amt + "'/><input type='hidden' class='mot_id'  value='" + ExpItem.mot_id + "'/><input type='hidden' class='StEndNeed' value='" + ExpItem.StEndNeed + "' /><input type='hidden' class='MaxKm' value='" + ExpItem.MaxKm + "' /><input type='hidden' class='Fuel_Charge' value='" + ExpItem.Fuel_Charge + "' /><input type='hidden' class='Expense_Km' value='" + ExpItem.Expense_Km + "' /><input type='hidden' class='Exp_Amount' value='" + ExpItem.Expense_Amount + "'/>";
                        for ($j = 0; $j < $AdDyAlw.length; $j++) {
                            var $adAlw = parseFloat($AdDyAlw[$j].AlwAmt);
                            if (isNaN($adAlw)) $adAlw = 0;
                            $s += "<td class='E_DyExp' Decd='" + $AdDyAlw[$j].AlwCd + "' Dedt='" + ExpItem.ExpDt + "'  align=\"right\">" + ((ExpItem.WType == 'No Claim' || ExpItem.WType == 'Not Claimed') ? "" : ($adAlw).toFixed(2)) + "</td>";
                            if (ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed') {
                                $cTAmt += $adAlw; $trTAdcol[$j] += $adAlw;
                            }
                        }
                        $s += "<td><table style='width: 100%;'>";
                        //ExpItem.WType != 'N/A' &&
                        (ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed') ? $uAllws = $UsrAlws.filter(function (a) { return (a.eDate == ExpItem.ExpDt); }) : $uAllws;
                        var $udAlw = 0;
                        for ($j = 0; $j < $uAllws.length; $j++) {
							if($uAllws[$j].Short_Name!='Hotel Bill'){
								var $udAlw = parseFloat($uAllws[$j].Amt);
								if (isNaN($udAlw)) $udAlw = 0;
								if ($uAllws[$j].Image_Url != '') {
										var img = $uAllws[$j].Image_Url != "" ? $uAllws[$j].ImageUrl.split(',') : [];
										trimg = '';
										for (var j = 0; j < img.length; j++) {
											if (img[j] != "") {
											   trimg += '<img src="' + img[j] + '" alt="" class="picc" style="/*height: 35px;width: 25px;*/margin-right: 3px;" />'
											}
										}
									//       trimg = '<img src="' + $uAllws[$j].ImageUrl + '" alt="" class="picc" />';
								}
								else {
								   trimg = '';
								}
								if(trimg!=''){
									$s += "<tr class='mtbl'><td class='E_DyUExp' align=\"left\">" + ((ExpItem.WType == 'No Claim' || ExpItem.WType == 'Not Claimed') ? "" : $uAllws[$j].expName) + "" + trimg + "</td><td class='E_DyUExpAmt'  expDt='" + $uAllws[$j].eDate + "' expcode='" + $uAllws[$j].expCode + "'  align=\"right\">" + ((ExpItem.WType == 'No Claim' || ExpItem.WType == 'Not Claimed') ? "" : ($udAlw).toFixed(2)) + "</td></tr>";
									if (ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed') {
									$cTAmt += $udAlw; $trTucl[$j] += $udAlw;
									}
								}
							}
                            /*$udAlw = parseFloat($uAllws[$j].Amt);
                            if (isNaN($udAlw)) $udAlw = 0;
                            if ($uAllws[$j].Image_Url != '') {
                                trimg = '<img src="' + $uAllws[$j].ImageUrl + '" alt="" class="picc" />';
                            }
                            else {
                                trimg = '';
                            }
                            if (parseInt($udAlw) != 0)
                                $s += "<tr class='mtbl'><td class='E_DyUExp' align=\"left\">" + ((ExpItem.WType == 'No Claim' || ExpItem.WType == 'Not Claimed') ? "" : $uAllws[$j].expName) + "" + trimg + "</td><td class='E_DyUExpAmt'  expDt='" + $uAllws[$j].eDate + "' expcode='" + $uAllws[$j].expCode + "'  align=\"right\">" + ((ExpItem.WType == 'No Claim' || ExpItem.WType == 'Not Claimed') ? "" : ($udAlw).toFixed(2)) + "</td></tr>";
                            if (ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed') {
                                $cTAmt += $udAlw; $trTucl[$j] += $udAlw;
                            }*/
                        }
                        var Htlnum = (isNaN(Number(ExpItem.Hotel_Bill_Amt)) ? 0 : Number(ExpItem.Hotel_Bill_Amt)).toFixed(2);
						if ( ExpItem.WType !='No Claim' && ExpItem.WType != 'Not Claimed' && parseInt(Htlnum) != 0) { //('<%=Session["div_code"]%>' == '162' || '<%=Session["div_code"]%>' == '109')
						var htl_img = HotelBill.filter(function (a) {
                                	return a.Dateandtime.indexOf(ExpItem.ExpDt)>-1;
	                            });        	                                    	                
                        	img = '';
                                if (htl_img.length > 0) {
									for (var i = 0; i < htl_img.length; i++)
		                                   img +='<img src="' + htl_img[i].Img_Url + '" alt="" style="margin-right:2px;" class="picc" />';
        	                }
                	        $s += "<tr><td expdt='" + ExpItem.ExpDt + "'>Hotel Bill<div>"+img+"</div></td><td>" + (Number(ExpItem.Hotel_Bill_Amt)).toFixed(2) + "</td></tr>";
                        	
                                //$s += "<tr><td><a  class='htlbill' expdt='" + ExpItem.ExpDt + "'>Hotel Bill</a></td><td>" + (Number(ExpItem.Hotel_Bill_Amt)).toFixed(2) + "</td></tr>";
                            }
							//else{
							//	$s += "</table></td>";
							//}
						    //$s += "<tr><td><a  class='htlbill' expdt='" + ExpItem.ExpDt + "'>Hotel Bill</a></td><td>" + Htlnum + "</td></tr>";
                         //}
                        if ($Auto == 1 && $rejenable == 0) {
                            tdreject = '<td><button type="button"  class="btn btn-danger" onclick="rejectexpense(this)" rejDt="' + ExpItem.ExpDt + '">Reject</button></td>'
                            if (ExpItem.WType == 'No Claim' || ExpItem.WType == 'Not Claimed') {
                                tdreject = '<td></td>';
                            }
                        }
                        else {
                            tdreject = '';
                        }
                        if (ExpItem.WType != 'No Claim' && $Auto == 1 && ExpItem.WType != 'Not Claimed') { //&& ExpItem.WType != 'N/A'
                            tdAdd = '<td><input class="dtxtdes" id="D' + ((ExpItem.ExpDt).replace("T00:00:00", "")) + '" type="text" /></td><td style="text-align:right;width:26%"><select class="cbAlwTyp ispinner"  onchange="CalcDAddDed(this)"><option value="1">+</option><option value="0">-</option></select><div class="dSpinner-Input"><div class="dSpinner-Value">+</div><div class="dSpinner-Modal"><ul><li val="1">+</li><li val="0">-</li></ul></div></div><input type="text" class="dtxAddDed" id="A' + ((ExpItem.ExpDt).replace("T00:00:00", "")) + '"  pval="0.00" value="" onkeyup="CalcDAddDed(this)" style="text-align:right;width: 70%;" /></td>';
                        }
                        else {
                            tdAdd = '<td class="hremarks"></td><td class="hremarks"></td>';
                        }
                        $s += "</table></td><td class='tamnt'  orval='" + $cTAmt + "' align=\"right\">" + ((ExpItem.WType != 'No Claim' && ExpItem.WType != 'Not Claimed') ? ($cTAmt).toFixed(2) : "") + "</td>" + tdAdd + tdreject + "";
                        $(tr).html($s);
                        $("#tBdyExp").append(tr);
                        //if (ExpItem.WType != 'N/A') {
                            $tTFare += (($Auto == 0) ? ($Dv * ((FareDet.length > 0) ? FareDet[0].Fare : 0)) : (Number(ExpItem.ExpFare)));
                            $tTAlw += (($Auto == 0) ? ($AlwAmt) : Number(ExpItem.ExpDA))
                            $tTAmt += $cTAmt;
                        //}
                        if ('<%=Session["div_code"]%>' != '162') {
                            $('#CallCnt').hide();
                            $('.CallCnt').closest('td').hide();
                        }
                    }
                    ExpItem = {}; $Dv = 0; $Plc = ''; $tTBD = 0; $tTAD = 0; $ptTAD = 0; $ptTBD = 0; $DwsSl = 1; $pDwsSl = 1; $StyPls = ''; $pDs = 0;

                }
            }

			 $('.htlbill').on('click', function () {
                    var rr = $(this).attr('expdt').split('T');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        //   async: false,
                        url: "rpt_Expense_Entry_New.aspx/Get_HotelBillExpense",
                        data: "{'SF':'<%=SF_Code%>'}",
                        dataType: "json",
                        success: function (data) {
                            HotelBill = JSON.parse(data.d) || [];
                            if (HotelBill.length > 0) {
                                $('#Htlbll').show();
                                str = '';
                                $('#main').html("");
                                $.each(HotelBill, function (key, val) {
                                    str = '<div><img style="width:50px;height:50px;" src="' + val.Img_Url + '" alt="" class="pics" /></div>';
                                    $('#main').append(str);
                                });
                                $('#main > div:first-child').find('.pics').addClass('exmpl');
                                var st = '<img id="imgsw" src="' + HotelBill[0].Img_Url + '" style="width:100%;height:300px;" />'                                
                                $('#1stdiv').html(st);
                            }
                            else {
                                rjalert('Alert', 'No Bills avaliable', 'error');
                            }
                        },
                        error: function (res) {

                        }
                    });
                });
                $("#btntimesClose").on('click', function () {
                    $('#Htlbll').hide();                    
                })
                $(document).on('click', '.pics', function () {                    
                    $('#imgsw').attr("src", $(this).attr("src"));
                    $('.pics').removeClass('exmpl');
                    $(this).addClass('exmpl');
                });
                $('#1stdiv').on('click', function () {
                    $('#photo1').attr("src", $(this).attr("src"));
                    $('#cphoto1').css("display", 'block');
                });

            $("#tFare").html($tTFare.toFixed(2));
            $("#tAlw").html($tTAlw.toFixed(2));
            $("#tTAmt").html($tTAmt.toFixed(2));
            $("#tTAmt").attr('orgval', $tTAmt.toFixed(2));
            $htr = $("#ExpenseEntry TFOOT").find('tr')[0];
            var vlength = -2;
            for ($j = 0; $j < $AdDyAlw.length; $j++) {
                $ths = $($htr).find('th');
                $($ths).eq($($ths).length - 2 + vlength).before("<th style=\"text-align: right;\">" + $trTAdcol[$j].toFixed(2) + "</th>");
                $trTAdcol.push(0)
            }

            $("#tGDAmt").html($tTAmt.toFixed(2));
            $htr = $("#tTotSmry");
            for ($j = 0; $j < $AdMnthAlw.length; $j++) {
                var filtexp = transExpmnth.filter(function (a) {
                    return a.ID == $AdMnthAlw[$j].AlwCd;
                });
                $ths = $($htr).find('tr');
                if (filtexp.length > 0) {
                    $udAlw = parseFloat(filtexp[0].Trans_Exp_Amt);
                }
                else {
                    $udAlw = parseFloat($AdMnthAlw[$j].AlwAmt);
                }
                if (isNaN($udAlw)) $udAlw = 0;
                $($ths).eq($($ths).length - 2).before("<tr class='alrow'><td class='E_MnExp'  alwc='" + $AdMnthAlw[$j].AlwCd + "' alwtyp='" + $AdMnthAlw[$j].AlwUEntry + "'   align=\"left\">" + $AdMnthAlw[$j].AlwName + "</td><td align=\"right\">" + (($AdMnthAlw[$j].AlwUEntry == '1') ? "<input type='text' style='text-align:right' onkeyup='calcTExp(this)' pval='" + ($udAlw).toFixed(2) + "' value='" + ($udAlw).toFixed(2) + "' class='mAllwAmt'/>" : ($udAlw).toFixed(2)) + "</td></tr>");
                $tTAmt += $udAlw;
            }

            $("#tNGTotAmt").html($tTAmt.toFixed(2));
            $("#tNGTotAmt").attr('oNGTAmnt', $tTAmt.toFixed(2));
            if ($Auto == 0) {
                $('.twnname').hide();
                $('.E_Twn').hide();
                $('.hremarks').hide();
                $('#fplace').hide();
                $('#tplace').hide();
                $('#Remarks').hide();
                $('#Deduct').hide();
                $(".fa-pencil").hide();
            }
            else if ('<%=Session["div_code"]%>' == '107') {
                $('#ttlCall_cnt').hide();
                $('.dtxAddDed').closest('td').hide();
            }
            $('.Atyp').each(function () {
                $(this).val($(this).attr("data-aTy"));
                $(this).hide();
            });
            if ($ArApp[0].EnDis == 'Disable' || '<%=Session["sf_code"]%>' =='<%=SF_Code%>') {
                    $(document).find('.fa-pencil').hide();
                }
                $('#HQC').text(HQC);
                $('#FWKC').text(FWKC);
                $('#EXC').text(EXC);
                $('#OTHC').text(OTHC);
                $('#OSC').text(OSC);
            }

            function goto(fl) {
                if (fl == 1)
                    if ($RtTwns.endsWith(',')) $RtTwns = $RtTwns.substring(0, $RtTwns.length - 1);

                if ($RtTwns.indexOf(',') > -1) {
                    $lst = $RtTwns.split(',');
                    if ($lst.length > 1)
                        $FTw = $lst[$lst.length - 1];
                } else {
                    $FTw = $RtTwns
                }
                $v = parseFloat(FILTER_DISTANCE($FTw, $TTw));
                if (isNaN($v)) $v = 0;
                itmd = {}
                itmd.SF = $fFR;
                itmd.Adt = $Adt
                itmd.FT = $TTw;
                itmd.TT = $FTw;
                itmd.MTR = -1;
                itmd.Dis = $v;
                itmd.ONo = $ONo;
                itmd.DwsSl = $DwsSl;
                ExpRWs.push(itmd);
                $Dv += $v;
                $ONo += 1;
                $CTW = $FTw;

                if ($OSRT == 0 && $fFR == $FTw) $OSRTFL = 1;

                if ($v > 100) {
                    $tTDh = $v - 100;
                    $tTAD = $tTAD + $tTDh;
                    $tTBD = $tTBD + ($v - $tTDh);
                } else
                    $tTBD = $tTBD + $v;

                if ($fFR == $FTw && $cWT != 'OS') {
                    if (($ptTAD + $ptTBD) < ($tTAD + $tTBD)) {
                        $ptTAD = $tTAD, $ptTBD = $tTBD, $pDs = $Dv, $pDwsSl = $DwsSl
                    }
                    $tTAD = 0, $tTBD = 0, $tTDh = 0, $Dv = 0, $DwsSl = $DwsSl + 1
                }
                $TTw = $FTw;
                if ($NTw == null || $NTw == '') $NTw = 's'
                if (FILTER_ROUTE($FTw, $NTw) == '' && $FTw != '' && $FTw != $NTw) { //&& $NTw!='' 
                    if ($RtTwns.indexOf(',') > -1) {
                        $RtTwns = $RtTwns.replace(',' + $FTw, '');
                        goto(0);
                    }
                }
                $FR = $FTw;
            }
			function rjalert(titles, contents, types) {
                if (types == 'error') {
                    var tp = 'red';
                    var icons = 'fa fa-warning';
                    var btn = 'btn-red';
                }
                else {
                    var tp = 'green';
                    var icons = 'fa fa-check fa-2x';
                    var btn = 'btn-green';
                }
                $.confirm({
                    title: '' + titles + '',
                    content: '' + contents + '',
                    type: '' + tp + '',
                    typeAnimated: true,
                    autoClose: 'action|8000',
                    icon: '' + icons + '',
                    buttons: {
                        tryAgain: {
                            text: 'OK',
                            btnClass: '' + btn + '',
                            action: function () {

                            }
                        }
                    }
                });
            }
            function FILTER_DISTANCE(fa, fb) {
                dtd = DDetail.d.filter(function (a) {
                    return (a.Frm_Plc_Code == fa && a.To_Plc_Code == fb);
                });
                $tWT = (dtd.length > 0) ? dtd[0].Place_Type : '';
                return (dtd.length > 0) ? dtd[0].Distance_KM : 0;
            }
            function FILTER_ROUTE(fa, fb) {
                dtd = DDetail.d.filter(function (a) {
                    return (a.Routes.indexOf(fa) > -1 && a.Routes.indexOf(fb, a.Routes.indexOf(fa)) > -1); //((a.Frm_Plc_Code == terr_code || a.Frm_Plc_Code == fa) && a.To_Plc_Code == fb);
                });

                return (dtd.length > 0) ? dtd[0].Routes.substring(('-' + dtd[0].Routes + '-').indexOf('-' + fa + '-'), ('-' + dtd[0].Routes + '-').indexOf('-' + fb + '-', ('-' + dtd[0].Routes + '-').indexOf('-' + fa + '-') + 1) + fb.length) + '-' : '';
                // dtd[0].Routes.substring(('-' + dtd[0].Routes).indexOf(fa) + 1, ('-' + dtd[0].Routes).indexOf(fb, ('-' + dtd[0].Routes).indexOf(fa) + 1) + fb.length) 
            }
            function CalcDis(p, c, n) {
                $v = 0, $OSRTFL = 0, $tWA = c.AlwType, $MTR = '';
                if ($FR == '') $FR = $fFR;
                if ($RtTwns == '') $RtTwns = $fFR + ',';
                $Adt = c.ExpDt;
                $Twn = c.route_code;
                var $d = new Date(c.ExpDt)
                if ($d.getDate() == 6) {
                    console.log('hai')
                }
                $Ntn = (n == undefined) ? '' : n.route_code;
                $nDt = (n == undefined) ? '' : n.ExpDt;

                $sTwns = FILTER_ROUTE($FR, $Twn);
                $sTwns1 = FILTER_ROUTE($Twn, $FR);
                if ($sTwns1.length > 0) {
                    if ($sTwns.length > $sTwns1.length || $sTwns.length < 1) {
                        $MTR = $RvNo; $sTwns = $sTwns1
                    }
                }

                $FTw = $sTwns.substring(0, $sTwns.indexOf('-'));
                $sTwns = $sTwns.substring($sTwns.indexOf('-') + 1, $sTwns.length);

                RTF: while (true) {
                    if ($MTR != '') $MTR = $RvNo;
                    if ($sTwns.indexOf('-') > -1) {
                        $TTw = $sTwns.substring(0, $sTwns.indexOf('-'));
                        $sTwns = $sTwns.substring($sTwns.indexOf('-') + 1, $sTwns.length);
                    } else $TTw = $sTwns;

                    $NTw = ($sTwns.indexOf('-') > -1) ? $sTwns.substring(0, $sTwns.indexOf('-')) : ($sTwns != '') ? $sTwns : $Ntn;
                    //$nDt = ($sTwns != '') ? $Adt : $Pdt;
                    ///$Pdt = $Adt
                    $v = 0
                    if ($Fl != 1) {
                        $v = parseFloat(FILTER_DISTANCE($FTw, $TTw));

                        $cWT = ($tWT == "") ? c.AlwType : $tWT;
                        if (isNaN($v)) $v = 0;
                        itmd = {}
                        itmd.SF = $fFR;
                        itmd.Adt = $Adt
                        itmd.FT = ($MTR == '') ? $FTw : $TTw;
                        itmd.TT = ($MTR == '') ? $TTw : $FTw;
                        itmd.MTR = $MTR;
                        itmd.Dis = $v;
                        itmd.ONo = $ONo;
                        itmd.DwsSl = $DwsSl;
                        ExpRWs.push(itmd);
                        $Dv += $v;
                        $ONo += ($MTR == '') ? 1 : 0;
                        $RvNo += ($MTR == '') ? 0 : 1;
                        $CTW = $TTw;
                        $StyPls += $FTw; $SPls = $FTw;
                        if ($v > 100) {
                            $tTDh = $v - 100;
                            $tTAD = $tTAD + $tTDh;
                            $tTBD = $tTBD + ($v - $tTDh);
                        } else
                            $tTBD = $tTBD + $v;
                    }
                    $Fl = 0;
                    if ($sTwns != '') {
                        if ($RtTwns.endsWith(',') == false && $RtTwns != '') $RtTwns = $RtTwns + ',';
                        $RtTwns = $RtTwns + $TTw + ',';
                        $FTw = $TTw;
                        continue RTF;
                    }
                    break;
                }
                $TTw = $Twn; $NTw = $Ntn;
                ///get Current AlTyp
                if ($Adt != $nDt && $cWT != 'OS') $NTw = ''   //and @sWT<>'OS'
                if ($RtTwns.endsWith(',') == false && $RtTwns != '') $RtTwns = $RtTwns + ','
                $RtTwns = $RtTwns + $TTw + ','

                if ((FILTER_ROUTE($TTw, $NTw) != '' || FILTER_ROUTE($NTw, $TTw) != '') && $TTw != '' && $NTw != '')
                    $FR = $TTw;
                else {
                    $RtTwns = FILTER_ROUTE($fFR, $TTw).replace(/-/g, ',');

                    dtd = DDetail.d.filter(function (a) {
                        return ($RtTwns.indexOf(',' + a.Frm_Plc_Code + ',') > -1
                            && a.To_Plc_Code == $NTw);
                    });
                    $tFR = ((dtd.length > 0) ? dtd[0].Frm_Plc_Code : '');
                    if ($tFR == '') {
                        dtd = DDetail.d.filter(function (a) {
                            return (a.Frm_Plc_Code == $fFR && a.To_Plc_Code == $TTw);
                        });
                        $tFR = ((dtd.length > 0) ? dtd[0].Frm_Plc_Code : '');
                    }
                    if ($tFR != '' && (',' + $RtTwns + ',').indexOf(',' + (($NTw == '') ? ' ' : $NTw) + ',') < 0 && (($NTw == '') ? 's' : $NTw) != (($TTw == '') ? 's' : $TTw)) {
                        dtd = DDetail.d.filter(function (a) {
                            return (
                                a.Frm_Plc_Code == $TTw
                                && a.To_Plc_Code == $tFR
                                && a.Distance_KM > 0
                            );
                        });
                        if (dtd.length > 0) {
                            $v = parseFloat(FILTER_DISTANCE($TTw, $tFR));
                            if (isNaN($v)) $v = 0;
                            itmd = {}
                            itmd.SF = $fFR;
                            itmd.Adt = $Adt
                            itmd.FT = $TTw;
                            itmd.TT = $tFR;
                            itmd.Dis = $v;
                            itmd.ONo = $ONo;
                            itmd.DwsSl = $DwsSl;
                            ExpRWs.push(itmd);
                            if ($OSRT == 0 && $tFR == $fFR) $OSRTFL = 1;
                            $Dv += $v; $ONo = $ONo + 1; $CTW = $tFR;

                            if ($v > 100) {
                                $tTDh = $v - 100;
                                $tTAD = $tTAD + $tTDh;
                                $tTBD = $tTBD + ($v - $tTDh);
                            } else
                                $tTBD = $tTBD + $v;

                            if ($fFR == $tFR && $WA != 'OS') {
                                if (($ptTAD + $ptTBD) < ($tTAD + $tTBD))
                                    $ptTAD = $tTAD; $ptTBD = $tTBD; $pDs = $Dv; $pDwsSl = $DwsSl;
                                $tTAD = 0; $tTBD = 0; $tTDh = 0; $Dv = 0; $DwsSl += 1
                            }

                            if ((',' + $RtTwns + ',').indexOf(',' + $tFR + ',') < 1)
                                $RtTwns = $tFR
                            else
                                $RtTwns = $RtTwns.substring(0, (',' + $RtTwns + ',').indexOf(',' + $tFR + ',') + $tFR.length);

                            $FR = $tFR
                        }
                        else {
                            goto(1)
                        }
                    }
                    else if ((($NTw == '') ? 's' : $NTw) != $TTw) {
                        goto(0)
                    } else {
                        $FR = $FTw;
                        $Fl = 1;
                    }
                }
                $OSRT++;
                if ($cWT != 'OS') $OSRT = 0;
                if (($ptTAD + $ptTBD) > ($tTAD + $tTBD)) {
                    $tTAD = $ptTAD; $tTBD = $ptTBD; $Dv = $pDs;
                }

                //delete from tbExpRoutDet where SF_code=@SF and Adt=@Adt and DtSl<>@pDwsSl  
                //declare @tmpWA varchar(20) 
                //set @tmpWA=@WA  

                //if @cWA='HQ' and @WA<>'HQ' and @OSRTFL=1 select @WA='EX',@OSRTFL=0,@OSRT=0  
                $tTFa = $Dv * $fa
                /*insert into tExpeDisDet select @SF,@Adt,isnull(@TWna,''),@WA,case when @tmpWA='OS' then 0 else round(isnull(@Dv,0),0) End,case when @tmpWA='OS' then round(isnull(@Dv,0),0) else @tTFa End  
                select @Dv=0,@Plc='',@tTBD=0,@tTAD=0,@ptTAD=0,@ptTBD=0,@DwsSl=1,@pDwsSl=1,@StyPls=''  */
            }
            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //   async: false,
                    url: "rpt_Expense_Entry_New.aspx/GetExpense",
                    data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>','SFEMP':'<%=EmpID%>'}",
                dataType: "json",
                success: function (data) {
                    ExpDets = JSON.parse(data.d) || [];
                    ReloadTable(); AddDRow(); AddMRow();
                    if ($StartEndKM == 1) {
                        $('.startendkm').show();
                    }
                    else {
                        $('.startendkm').hide();
                    }
                    if ('<%=Session["div_code"]%>' == '109' || '<%=Session["div_code"]%>' == '116') {
                        $('.phsfields').show();
                    }
                    else {
                        $('.phsfields').hide();
                    }
                    if ('<%=Session["div_code"]%>' == '162') {
                            $('.CallCnt').show();
                        }
                        else {
                            $('.CallCnt').hide();
                        }
                    },
                    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                });
            }
            function datyp($xda) {
                $($xda).closest('tr').find('.Atyp').show();
                $($xda).closest('tr').find('.E_Typ>span').hide();
                $($xda).hide();
            }
            function distyp($xd) {
                $($xd).closest('tr').find('.eddist').show();
                $($xd).closest('tr').find('.E_Dis>span').hide();
                $($xd).hide();
            }
            function distchange($chdist) {
                var cdist = parseFloat($($chdist).val());
                if (isNaN(cdist)) cdist = 0;
                var prdist = parseFloat($($chdist).attr("pdist"));
                if (isNaN(prdist)) prdist = 0;
                var chfare = cdist * parseFloat(((FareDet.length > 0) ? FareDet[0].Fare : 0));
                if (isNaN(chfare)) chfare = 0;
                var oldfare = parseFloat($($chdist).closest('tr').find('.E_Fare').text());
                if (isNaN(oldfare)) oldfare = 0;
                var predtmt = parseFloat($($chdist).closest('tr').find('.tamnt').text());
                if (isNaN(predtmt)) predtmt = 0;
                var pretfare = parseFloat($('#tFare').text());
                if (isNaN(pretfare)) pretfare = 0;
                var pretTAmt = parseFloat($('#tTAmt').text());
                if (isNaN(pretTAmt)) pretTAmt = 0;
                var pretGDAmt = parseFloat($('#tGDAmt').text());
                if (isNaN(pretGDAmt)) pretGDAmt = 0;
                var pretNGTotAmt = parseFloat($('#tNGTotAmt').text());
                if (isNaN(pretNGTotAmt)) pretNGTotAmt = 0;
                predtmt = (predtmt - oldfare + chfare);
                pretfare = (pretfare - oldfare + chfare);
                pretTAmt = (pretTAmt - oldfare + chfare);
                pretGDAmt = (pretGDAmt - oldfare + chfare);
                pretNGTotAmt = (pretNGTotAmt - oldfare + chfare);
                $($chdist).closest('tr').find('.E_Dis>span').text(cdist);
                $($chdist).closest('tr').find('.E_Fare').text(chfare);
                $($chdist).closest('tr').find('.tamnt').text(predtmt);
                $($chdist).closest('tr').find('.tamnt').attr("orval", predtmt);
                $('#tFare').text(pretfare);
                $('#tTAmt').text(pretTAmt);
                $('#tTAmt').attr("orgval", pretTAmt);
                $('#tGDAmt').text(pretGDAmt);
                $('#tNGTotAmt').text(pretNGTotAmt);
                $($chdist).closest('tr').find('.E_Dis>span').show();
                $($chdist).closest('.E_Dis').find('a').show();
                $($chdist).hide();
            }
            function Atypchange($typch) {
                var chdatyp = $($typch).val();
                var chdatext = $($typch).children('option:selected').text();
                var preddaamt = parseFloat($($typch).closest('tr').find('.E_Alw').text());
                if (isNaN(preddaamt)) preddaamt = 0;
                var predtmt = parseFloat($($typch).closest('tr').find('.tamnt').text());
                if (isNaN(predtmt)) predtmt = 0;
                var pretAlw = parseFloat($('#tAlw').text());
                if (isNaN(pretAlw)) pretAlw = 0;
                var pretTAmt = parseFloat($('#tTAmt').text());
                if (isNaN(pretTAmt)) pretTAmt = 0;
                var pretGDAmt = parseFloat($('#tGDAmt').text());
                if (isNaN(pretGDAmt)) pretGDAmt = 0;
                var pretNGTotAmt = parseFloat($('#tNGTotAmt').text());
                if (isNaN(pretNGTotAmt)) pretNGTotAmt = 0;
                var chalwd = $AlwDets.filter(function (a) {
                    return (a.AlwCd == (chdatyp + '1'))
                });
                $ChAlwAmt = (chalwd.length > 0) ? parseFloat(chalwd[0].AlwAmt) : 0;
                predtmt = (predtmt - preddaamt + $ChAlwAmt);
                pretAlw = (pretAlw - preddaamt + $ChAlwAmt);
                pretTAmt = (pretTAmt - preddaamt + $ChAlwAmt);
                pretGDAmt = (pretGDAmt - preddaamt + $ChAlwAmt);
                pretNGTotAmt = (pretNGTotAmt - preddaamt + $ChAlwAmt);
                $($typch).closest('tr').find('.E_Alw').text($ChAlwAmt);
                $($typch).closest('tr').find('.tamnt').text(predtmt);
                $($typch).closest('tr').find('.tamnt').attr("orval", predtmt);
                $('#tAlw').text(pretAlw);
                $('#tTAmt').text(pretTAmt);
                $('#tTAmt').attr("orgval", pretTAmt);
                $('#tGDAmt').text(pretGDAmt);
                $('#tNGTotAmt').text(pretNGTotAmt);
                $($typch).closest('tr').find('.E_Typ>span').text(chdatext);
                $($typch).closest('tr').find('.E_Typ>span').show();
                $($typch).closest('.E_Typ').find('a').show();
                $($typch).hide();
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
            $(document).ready(function () {
			 var item = { div: '<%=Session["div_code"]%>', sfcode: '<%=SF_Code%>', sfname: '<%=SFname%>', empid: '<%=EmpID%>', month: '<%=Mn%>', year:'<%=Yr%>'};
                if ('<%=Session["div_code"]%>' == '109')
                    $("#showTp").show();
                namesArr = [];
                namesArr.push(item);
                window.localStorage.setItem('Expense_Approval.aspx', JSON.stringify(namesArr));
                $('#cls').on('click', function () {
                    window.close();
                });
                $('#btnprint').on('click', function () {
                    if ('<%=Session["div_code"]%>' == '107') {
                    var totarr = [];
                    var prtdiv = $('#prtdiv').empty();
                    var printtable = $('<table id="prttable" style="border-collapse: collapse;"><thead id="prthead"></thead><tbody id="prtbody"></tbody></table>');
                    $(prtdiv).append(printtable);
                    var str = $('<tr><th colspan="9" style="text-align: center;border: 1px solid #000;">KTV Health Food PVT LTD</th></tr>');
                    $(prtdiv).find('#prthead').append(str);
                    str = $('<tr><th colspan="9" style="text-align: center;border: 1px solid #000;">Travelling & Daily Allowance Report</th></tr>');
                    $(prtdiv).find('#prthead').append(str);
                    str = $('<tr> <th colspan="3" style="text-align: left;border: 1px solid #000;">Name - ' + SFDets[0].Name + '</th><th colspan="3" style="text-align: left;border: 1px solid #000;">State - TamilNadu</th><th colspan="3"style="text-align: left;border: 1px solid #000;"></th></tr>');
                    $(prtdiv).find('#prthead').append(str);
                    str = $('<tr><th colspan="3" style="text-align: left;border: 1px solid #000;">Designation - ' + SFDets[0].Desig + '</th><th colspan="3" style="text-align: left;border: 1px solid #000;">HQ - ' + SFDets[0].HQName + '</th><th colspan="3" style="text-align: left;border: 1px solid #000;">' + ExpDets[1].ADate + ' to ' + ExpDets[ExpDets.length - 2].ADate + '</th></tr>');
                    $(prtdiv).find('#prthead').append(str);
                    str = '<tr><td rowspan="2" style="text-align: center;border: 1px solid #000;">Date</td><td rowspan="2" style="text-align: center;border: 1px solid #000;">Market Worked</td><td colspan="2" style="text-align: center;border: 1px solid #000;">TA</td>';
                    var str1 = '<tr><td style="text-align: center;border: 1px solid #000;">Local</td><td style="text-align: center;border: 1px solid #000;">Out Station</td>';
                    let taAlw = [];
                    taAlw = $AlwDets.filter(function (a) { return a.AlwType != "0" });
                    for (let i = 0; i < taAlw.length; i++) {
                        str += '<td style="text-align: center;border: 1px solid #000;">' + taAlw[i].AlwName + '</td>';
                        str1 += '<td style="text-align: center;border: 1px solid #000;">Rs.</td>';
                    }
                    str += '<td rowspan="2" style="text-align: center;border: 1px solid #000;">KM</td><td rowspan="2" style="text-align: center;border: 1px solid #000;">Total</td><td rowspan="2" style="text-align: center;border: 1px solid #000;">Remarks</td></tr>';
                    str1 += '</tr>';
                    $(prtdiv).find('#prthead').append(str);
                    $(prtdiv).find('#prthead').append(str1);
                    let bstr = '';
                    let fstr = '';
                    let gstr = '';
                    let emp = '';
                    let hqall = 0;
                    let osall = 0;
                    let Otot = 0;
                    ExpItem = {};
                    var curdt;
                    var curclstr = '';
                    for (let i = 1; i < ExpDets.length - 1; i++) {
                        if (curdt == undefined) {
                            curdt = ExpDets[i].ADate;
                            curclstr = ExpDets[i].ClstrName;
                        }
                        else {
                            curclstr += "," + ExpDets[i].ClstrName
                        }
                        NAdt = ""; nItm = null;
                        if ((i + 1) < ExpDets.length) { NAdt = ExpDets[i + 1].ADate; nItm = ExpDets[i + 1] }
                        if (curdt != NAdt) {
                            let ar = 0;
                            let Dtot = 0;
                            var clstr = '';
                            if (moment(new Date(ExpDets[i].ExpDt)).format('dddd') == "Sunday" && curclstr == "") {
                                clstr = "Sunday";
                            }
                            else if (curclstr == "" || curclstr == ",,") {
                                clstr = ExpDets[i].WType;
                            }
                            else {
                                clstr = curclstr;
                            }
                            bstr += '<tr><td style="text-align: left;border: 1px solid #000;">' + ExpDets[i].ADate + '</td><td style="text-align: left;border: 1px solid #000;">' + (clstr) + '</td>';
                            Dtot += Number(ExpDets[i].ExpDA);
                            if (ExpDets[i].DAtype == 'HQ') {
                                bstr += '<td style="text-align: right;border: 1px solid #000;">' + ExpDets[i].ExpDA + '</td><td style="text-align: right;border: 1px solid #000;"></td>';
                                hqall += Number(ExpDets[i].ExpDA);
                            }
                            else {
                                bstr += '<td style="text-align: right;border: 1px solid #000;"></td><td style="text-align: right;border: 1px solid #000;">' + ExpDets[i].ExpDA + '</td>';
                                osall += Number(ExpDets[i].ExpDA);
                            }
                            for (let j = 0; j < taAlw.length; j++) {
                                var filtarr = $UsrAlws.filter(function (a) { return a.expCode == taAlw[j].AlwCd && a.eDate == ExpDets[i].ExpDt });
                                let tamtAlw = 0;
                                tamtAlw = filtarr.reduce(function (prev, cur) {
                                    return prev + cur.Amt;
                                }, 0);
                                bstr += '<td style="text-align: right;border: 1px solid #000;">' + ((filtarr.length > 0) ? tamtAlw : emp) + '</td>';
                                let usralwamt = 0;
                                if (filtarr.length > 0) {
                                    for (var h = 0; h < filtarr.length; h++) {
                                        usralwamt += parseFloat(filtarr[h].Amt);
                                    }
                                    Dtot += usralwamt;
                                }
                                totarr[ar] = ((filtarr[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + usralwamt) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                                ar++;
                            }
                            var DAdd = $UsrDAdd.filter(function (a) { return a.Expense_Date == ExpDets[i].ExpDt });
                            if (ExpDets[i].WType != 'No Claim' && ExpDets[i].WType != 'Not Claimed') {
                                Otot += Dtot;
                            }
                            bstr += '<td style="text-align: right;border: 1px solid #000;">' + ExpDets[i].ExpDist + '</td><td style="text-align: right;border: 1px solid #000;">' + Dtot + '</td><td style="text-align: right;border: 1px solid #000;">' + ((DAdd.length > 0) ? DAdd[0].Descp : emp) + '</td>';
                            ExpItem = {}; curclstr = ''; curdt = undefined;
                        }
                    }
                    for (var i = 0; i < $UsrMAlws.length; i++) {
                        gstr += '<tr><td colspan="2" style="text-align: left;border: 1px solid #000;">' + $UsrMAlws[i].Exp_Desc + '</td><td style="text-align: right;border: 1px solid #000;"></td><td style="text-align: right;border: 1px solid #000;"></td><td style="text-align: right;border: 1px solid #000;"></td><td style="text-align: right;border: 1px solid #000;"></td><td style="text-align: right;border: 1px solid #000;"></td><td style="text-align: right;border: 1px solid #000;">' + ($UsrMAlws[i].Add_Sub + $UsrMAlws[i].Exp_Amnt) + '</td><td style="text-align: right;border: 1px solid #000;"></td></tr>';

                    }

                    fstr = '<tr><td colspan="2" style="text-align: left;border: 1px solid #000;">Total</td><td style="text-align: right;border: 1px solid #000;">' + hqall + '</td><td style="text-align: right;border: 1px solid #000;">' + osall + '</td>';
                    for (let j = 0; j < totarr.length; j++) {
                        fstr += '<td style="text-align: right;border: 1px solid #000;">' + totarr[j] + '</td>';
                    }
                    $('.txAddDed').each(function () {
                        let dedamt = $(this).val();
                        if (isNaN(Number(dedamt))) dedamt = 0;
                        dedamt = Number(dedamt);
                        let dedtyp = $(this).parent().find('.cbAlwTyp').val();
                        if (dedtyp == 1) {
                            Otot = Otot + parseFloat(dedamt)
                        }
                        if (dedtyp == 0) {
                            Otot = Otot - parseFloat(dedamt)
                        }
                    });
                    $('#tBdyAddDed>tr').each(function () {
                        let dedamt = $(this).find('td').eq(1).html();
                        if (isNaN(Number(dedamt))) dedamt = 0;
                        dedamt = Number(dedamt);
                        Otot = Otot + parseFloat(dedamt);
                    });
                    fstr += '<td></td><td style="text-align: right;border: 1px solid #000;">' + Otot + '</td><td style="text-align: right;border: 1px solid #000;"></td></tr>';
                    $(prtdiv).find('#prtbody').append(bstr);
                    $(prtdiv).find('#prtbody').append(gstr);
                    $(prtdiv).find('#prtbody').append(fstr);
                    fstr = '<tr><td rowspan="3" colspan="3" style="text-align: center;height:150px;">Signature</td><td style="text-align: center;" rowspan="3" colspan="2"></td><td rowspan="3" colspan="4">ASM/Manager\'s Signature</td></tr>';
                    $(prtdiv).find('#prtbody').append(fstr);
                    var contents = $(prtdiv).html();
                    var frame1 = $('<iframe />');
                    frame1[0].name = "frame1";
                    frame1.css({ "position": "absolute", "top": "-1000000px" });
                    $("body").append(frame1);
                    var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document;//? frame1[0].contentDocument.document : frame1[0].contentDocument;
                    frameDoc.document.open();
                    frameDoc.document.write('<html><head><title>Expense</title></head>');
                    frameDoc.document.write('<body>');
                    frameDoc.document.write(contents);
                    frameDoc.document.write('</body></html>');
                    frameDoc.document.close();
                    setTimeout(function () {
                        window.frames["frame1"].focus();
                        window.frames["frame1"].print();
                        frame1.remove();
                    }, 500);
                }
                else {
                    var body = $(document).find('body');
                    $(body).find('.sub-header').find('span').hide();
                    window.print(body);
                    $(body).find('.sub-header').find('span').show();
                }
            });
            $chksfc = '<%=Session["sf_code"]%>';
            $('#btsav').on('click', function () {
                $MAuto = 0;
                svExpense();
            });
            $(document).on('click', '.picc', function () {
                var photo = $(this).attr("src");
                $('#photo1').attr("src", $(this).attr("src"));
                $('#cphoto1').css("display", 'block');
            });
            dv = $('<div style="z-index: 10000000;position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
			$.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //   async: false,
                    url: "rpt_Expense_Entry_New.aspx/Get_HotelBillExpense",	                    
                    dataType: "json",
                    success: function (data) {
                        HotelBill = JSON.parse(data.d) || [];
                    },
                    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/GetSFDetails",
                data: "{'SF':'<%=SF_Code%>'}",
                dataType: "json",
                success: function (data) {
                    SFDets = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/GetDistanceDetails",
                data: "{'sf_code':'<%=SF_Code%>'}",
                dataType: "json",
                success: function (data) {
                    DDetail = data;
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/GetfAREDetails",
                data: "{'sf_code':'<%=SF_Code%>'}",
                dataType: "json",
                success: function (data) {
                    FareDet = data.d;
                    //if (FareDet.length < 1) alert("No Fare Details");
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/GetAllowanceDetails",
                data: "{'sf_code':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    $AlwDets = data.d;
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/GetUserExpense",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    $UsrAlws = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/UserDailyAdditional",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    $UsrDAdd = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/UserExpAdditional",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    $UsrMAlws = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/getTPDetails",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    transExpTp = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/getApproval",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    $ArApp = JSON.parse(data.d) || [];
                    if ($ArApp[0].EnDis == 'Enable') {
                        $('#btsave').prop('disabled', false);
                        $('#btsav').prop('disabled', false);
                        $('#rjctall').prop('disabled', false);
                        $('#btnprint').prop('disabled', true);
                        $rejenable = 0;
                    }
                    if ($ArApp[0].EnDis == 'Disable' || '<%=Session["sf_code"]%>' =='<%=SF_Code%>') {
                        $('#btsave').prop('disabled', true);
                        $('#btsav').prop('disabled', true);
                        $('#rjctall').prop('disabled', true);
                        $('#btnprint').prop('disabled', false);
                        $rejenable = 1;
                    }
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/AutoExpense",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    let $setups = JSON.parse(data.d);
                    if ($setups.length > 0) {
                        $Auto = $setups[0]["AutoExpense"];
                        $StartEndKM = $setups[0]["StartEndKM"];
                        if ($Auto == 0) {
                            $('#btsav').hide();
                            $('#rjctall').hide();
                        }
                    }
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/ManualSfExpense",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    $ExpSFMn = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/getTransMonthExp",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                dataType: "json",
                success: function (data) {
                    transExpmnth = JSON.parse(data.d) || [];
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/GetExpenseKm",
                data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                    dataType: "json",
                    success: function (data) {
                        transExpStEndKm = JSON.parse(data.d) || [];
                    },
                    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                });
                loadData();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Expense_Entry_New.aspx/getTPDeVation",
                    data: "{'SF':'<%=SF_Code%>','Mn':'<%=Mn %>','Yr':'<%=Yr%>'}",
                    dataType: "json",
                    success: function (data) {
                        transTpDevation = JSON.parse(data.d) || [];
                        if (transTpDevation.length > 0) {
                            LoadTpDevation();
                        }
                    },
                    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                });
            });
            function LoadTpDevation() {
                $("#Tptbl TBODY").html("");
                str = "";
                for (var i = 0; i < transTpDevation.length; i++) {
                    str += "<tr><td>" + (i + 1) + "</td><td>" + transTpDevation[i].Deviation_Reason + "</td><td>" + transTpDevation[i].TP_Area + "</td><td>" + transTpDevation[i].Activity_Date + "</td><td>" + transTpDevation[i].Approval_MGR_Name + "</td></tr>";
                }
                $("#Tptbl TBODY").append(str);
                //$("#TpDv").modal('toggle');
                //$("#TpDv").show();
            }
            function shwTp() {
                if (transTpDevation.length == 0) {
                    rjalert('Alert!!!', 'No Tp Devation Data for this month!!!', 'error');
                    return false;
                }
                $("#TpDv").show();
            }
            function btnClose() {
                $("#TpDv").hide();
            }
            function svExpense() {
                $tAv = parseFloat($("#tNGTotAmt").html());
                $Atvexp = parseFloat(Number($('#tNGTotAmt').attr('ongtamnt'))).toFixed(2);
                if (isNaN($tAv)) $tAv = 0;
                $ExpHead = [{ 'SF': '<%=SF_Code%>', 'Mn': '<%=Mn %>', 'Yr': '<%=Yr%>', 'md': '2', 'tamt': $tAv }];
            $tb = $('#tBdyExp');
            $trdy = $('#tBdyExp tr');
            dydata = [];
            for ($i = 0; $i < $trdy.length; $i++) {
			 //if ($($trdy[$i]).find('.E_WT').text() != 'N/A') {
                dy = {};
                dy.E_Dt = $($trdy[$i]).find('.E_Dt').text();
                dy.E_WT = $($trdy[$i]).find('.E_WT').text();
                dy.E_Twn = ($($trdy[$i]).find('.E_Twn').text() == "undefined") ? "" : ($($trdy[$i]).find('.E_Twn').text()).replace(/[^a-zA-Z ]/g, "");;
                dy.E_Typ = $($trdy[$i]).find('.E_Typ>span').text();
                dy.E_Twnn = $($trdy[$i]).find('.twnname').text().replace(/[^a-zA-Z ]/g, "");
                dy.E_MDP = $($trdy[$i]).find('.E_MDayP').text().replace(/[^a-zA-Z ]/g, "");
                dy.E_Dis = ($($trdy[$i]).find('.E_Dis>span').text() != "") ? $($trdy[$i]).find('.E_Dis>span').text() : '0';
                dy.E_Fare = ($($trdy[$i]).find('.E_Fare').text() != "") ? $($trdy[$i]).find('.E_Fare').text() : parseFloat('0').toFixed(2);
                dy.E_Alw = ($($trdy[$i]).find('.E_Alw').text() != "") ? $($trdy[$i]).find('.E_Alw').text() : parseFloat('0').toFixed(2);
                dy.E_TAmt = ($($trdy[$i]).find('.tamnt').text() != "") ? $($trdy[$i]).find('.tamnt').text() : parseFloat('0').toFixed(2);
				dy.E_mot = ($($trdy[$i]).find('.mot').val() != "") ? $($trdy[$i]).find('.mot').val() : parseFloat('0').toFixed(2);
                    dy.E_st_typ = ($($trdy[$i]).find('.st_typ').val() != "") ? $($trdy[$i]).find('.st_typ').val() : parseFloat('0').toFixed(2);
                    dy.E_H_Billamnt = ($($trdy[$i]).find('.H_Billamnt').val() != "") ? $($trdy[$i]).find('.H_Billamnt').val() : parseFloat('0').toFixed(2);
                    dy.E_mot_id = ($($trdy[$i]).find('.mot_id').val() != "") ? $($trdy[$i]).find('.mot_id').val() : parseFloat('0').toFixed(2);
                    dy.E_StEndNeed = ($($trdy[$i]).find('.StEndNeed').val() != "") ? $($trdy[$i]).find('.StEndNeed').val() : parseFloat('0').toFixed(2);
                    dy.E_MaxKm = ($($trdy[$i]).find('.MaxKm').val() != "") ? $($trdy[$i]).find('.MaxKm').val() : parseFloat('0').toFixed(2);
                    dy.E_Fuel_Charge = ($($trdy[$i]).find('.Fuel_Charge').val() != "") ? $($trdy[$i]).find('.Fuel_Charge').val() : parseFloat('0').toFixed(2);
                    dy.E_Expense_Km = ($($trdy[$i]).find('.Expense_Km').val() != "") ? $($trdy[$i]).find('.Expense_Km').val() : parseFloat('0').toFixed(2);
                    dy.E_Exp_Amount = ($($trdy[$i]).find('.Exp_Amount').val() != "") ? $($trdy[$i]).find('.Exp_Amount').val() : parseFloat('0').toFixed(2);
                if (dy.E_Dt != "" && dy.E_Typ != "") {
                    var dtt = dy.E_Dt.split('/')
                    dy.E_Dt = dtt[2] + '-' + dtt[1] + '-' + dtt[0];
                    dydata.push(dy);
               }
            //}
          }
            console.log(dydata);
            dyAdde = [];
            if ($Auto == 1) {
                for ($i = 0; $i < $trdy.length; $i++) {
                    dad = {};
                    dad.EAdt = $($trdy[$i]).find('.E_Dt').text();
                    dad.EAde = ($($trdy[$i]).find('.dtxtdes').val()) == undefined ? "" : $($trdy[$i]).find('.dtxtdes').val();
                    dad.EAdd = (isNaN($($trdy[$i]).find('.dtxAddDed').val()) ? 0 : $($trdy[$i]).find('.dtxAddDed').val());
                    dad.Dsub = $($trdy[$i]).find('.dSpinner-Value').text();
                    if ($($trdy[$i]).find('.E_WT').text() != "No Claim" && $($trdy[$i]).find('.E_WT').text() != "Not Claimed" && $($trdy[$i]).find('.E_Dt').text() != "") {
                        var dtt = dad.EAdt.split('/')
                        dad.EAdt = dtt[2] + '-' + dtt[1] + '-' + dtt[0];
                        dyAdde.push(dad);
                    }
                }
            }
            console.log(dyAdde);
            $mtbltr = $('.E_DyUExpAmt');
            mdydata = [];
            for ($i = 0; $i < $mtbltr.length; $i++) {
                md = {};
                md.E_CD = $($mtbltr[$i]).attr('expcode');
                md.E_EDT = $($mtbltr[$i]).attr('expDt').replace('T00:00:00', '');
                md.E_EAMT = $($mtbltr[$i]).closest('tr').find('.E_DyUExpAmt').text();
                if (md.E_EAMT != "") {
                    mdydata.push(md);
                }
            }
            console.log(mdydata);
            $mbtltd = $('.E_DyExp');
            for ($i = 0; $i < $mbtltd.length; $i++) {
                md = {};
                md.E_CD = $($mbtltd[$i]).attr('decd');
                md.E_EDT = $($mbtltd[$i]).attr('dedt').replace('T00:00:00', '');;
                md.E_EAMT = $($mbtltd[$i]).text();
                if (md.E_EAMT != "") {
                    mdydata.push(md);
                }
            }
            console.log(mdydata);
            $atbltr = $('.E_MnExp');
            mMtdata = [];
            for ($i = 0; $i < $atbltr.length; $i++) {
                mMd = {};
                mMd.E_MCD = $($atbltr[$i]).attr('alwc');
                mMd.E_MTYP = $($atbltr[$i]).attr('alwtyp');
                if ((($($atbltr[$i]).closest('tr').find('td').eq(1).html()).indexOf('input') > -1)) {
                    mMd.E_MAMT = $($atbltr[$i]).closest('tr').find('td').eq(1).find('.mAllwAmt').val();
                }
                else {
                    mMd.E_MAMT = $($atbltr[$i]).closest('tr').find('td').eq(1).html()
                }
                mMtdata.push(mMd);
            }
            console.log(mMtdata);
            $tBdyAddDed = $('#tBdyAddDed tr');
            Exp_Add = [];
            for ($i = 0; $i < $tBdyAddDed.length; $i++) {
                mMd = {};
                mMd.Desc = $($tBdyAddDed[$i]).find('.txAddDesc').val();
                mMd.Val = (isNaN($($tBdyAddDed[$i]).find('.txAddDed').val()) ? 0 : $($tBdyAddDed[$i]).find('.txAddDed').val());
                mMd.Asub = $($tBdyAddDed[$i]).find('.Spinner-Value').text();
                if (mMd.Val != "") {
                    Exp_Add.push(mMd);
                }
            }
            console.log(Exp_Add);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Expense_Entry_New.aspx/saveExpense",
                data: "{'EHead':'" + JSON.stringify($ExpHead) + "','ExDetails':'" + JSON.stringify(dydata) + "','DDetails':'" + JSON.stringify(mdydata) + "','MDetails':'" + JSON.stringify(mMtdata) + "','ExpAdd':'" + JSON.stringify(Exp_Add) + "','MAuto':'" + $MAuto + "','ApExp':'" + $Atvexp + "','ExpDAdd':'" + JSON.stringify(dyAdde) + "','AppSFC':'" + $chksfc + "','sEmpId':'<%=EmpID%>'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Error in Daily Expense Details') {
                            alert(data.d);
                        }
                        if (data.d == 'Expense Saved for Approval' || data.d == 'Expense Submitted for Approval') {
							 if ($MAuto != 0) {
                                alert('Expense Successfully Approved');
                            }
                            else {                                					
                                alert(data.d);
                            }
                            alert(data.d);
                            $('#btsave').prop('disabled', true);
                            $('#btsav').prop('disabled', true);
                            $('#rjctall').prop('disabled', true);
                            $rejenable = 1;
                            window.opener.location.reload();
                            window.close();
                        }
                        else {
                            alert(data.d);
                        }
                    },
                    error: function (exception) {
                        alert(exception.responseText);
                    }
                });
            }
        </script>
</body>
</form>
</html>
