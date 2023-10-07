<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Rpt_My_Day_Plan_View.aspx.cs"
    Inherits="Reports_Rpt_My_Day_Plan_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Day Plan View Report</title>
    <link href="../../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-daterangepicker/3.0.5/daterangepicker.css" />
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type="text/css" />
    <link href="../../css/jquery.multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.24.0/moment.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-daterangepicker/3.0.5/daterangepicker.min.js"></script>
    <script src="<%=Page.ResolveUrl("~/js/lib/bootstrap-select.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.multiselect.js")%>" type="text/javascript"></script>
	    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
    <script type="text/javascript">
        var dt = new Date();
        var sf = '';
        $(document).ready(function () {
            var indx = 1;
            var i = 0;
            function loadAddrss($tr) {
                var tbl = $('#gvMyDayPlan tr');
                var lat1 = $($tr).find('td')[16];
                var long1 = $($tr).find('td')[17];
                var lat2 = $(lat1).text().trim();
                var long2 = $(long1).text().trim();
                var addrs = '';

                //var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat2 + "," + long2 + "&key=sdvAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(long2) + '&lat=' + parseFloat(lat2) + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                    }
                });
				if(parseFloat(long2)=='0' && parseFloat(lat2)=='0'){
				$($($tr).find('td')[18]).text('Address Not Fetched');
				}
				else{
                $($($tr).find('td')[18]).text(addrs);
                }i++;
                setTimeout(function () { loadAddrss($('#gvMyDayPlan tr')[i]) }, 25); //loadAddrss(); 

            }
            setTimeout(function () { loadAddrss($('#gvMyDayPlan tr')[0]) }, 25);
        });
        function getManagerHQ($rsf) {
            if ($rsf.toString().indexOf("MGR") > -1) {
                $("#ddlrephq").show();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_My_Day_Plan_View.aspx/getRepHQs",
                    data: "{'repsf':'" + $rsf + "'}",
                    dataType: "json",
                    success: function (data) {
                        let repHqs = JSON.parse(data.d) || [];
                        let ddlhqs = $("#ddlrephq");
                        ddlhqs.empty().append("<option value=''>Select HQ</option>");
                        for (var i = 0; i < repHqs.length; i++) {
                            ddlhqs.append($('<option value="' + repHqs[i].Sf_Code + '">' + repHqs[i].HqName + '</option>'));
                        }
                        $('#ddlrephq').selectpicker({
                            liveSearch: true
                        });
                        $('#ddlrephq').selectpicker('refresh');
                    },
                    error: function (rs) {

                    }
                });
            }
            else {
                $("#ddlrephq").hide();
            }
        }
        function getTPWorkWith(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_My_Day_Plan_View.aspx/getSFWorkedWith",
                data: "{'sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPWtyps = JSON.parse(data.d) || [];
                    var tpr = $("#eWork");
                    tpr.empty();
                    for (var i = 0; i < TPWtyps.length; i++) {
                        tpr.append($('<option value="' + TPWtyps[i].id + '">' + TPWtyps[i].name + '</option>'));
                    }
                    $('#eWork').multiselect({
                        columns: 3,
                        placeholder: 'Select Worked with',
                        search: true,
                        searchOptions: {
                            'default': 'Search Worked with'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                },
                error: function (rs) {

                }
            });
        }
        function getTPWtypes(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_My_Day_Plan_View.aspx/getSFTPWtypes",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPWtyps = JSON.parse(data.d) || [];
                    var tpr = $("#eWorktyp");
                    tpr.empty().append($('<option value="0">Select Worktype</option>'));
                    for (var i = 0; i < TPWtyps.length; i++) {
                        tpr.append($('<option value="' + TPWtyps[i].WCode + '">' + TPWtyps[i].WName + '</option>'));
                    }
                    $('#eWorktyp').selectpicker({
                        liveSearch: true
                    });
                    $('#eWorktyp').selectpicker('refresh');
                },
                error: function (rs) {

                }
            });
        }
        function getDayplan(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_My_Day_Plan_View.aspx/getTBPlan",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "','Dt':'<%=Date%>'}",
                dataType: "json",
                success: function (data) {
                    var TBPlan = JSON.parse(data.d) || [];
                    $('#eWorktyp').selectpicker('val', TBPlan[0].wtype);
                    $('#edist').selectpicker('val', TBPlan[0].stockist);
                    $('#eroute').selectpicker('val', TBPlan[0].cluster);
                    $('#eremarks').val(TBPlan[0].remarks);
                    var workwith = TBPlan[0].worked_with_code;
                    workwith = workwith.split('$$');
                    $('#eWork  > option').each(function () {
                        $v = $(this).val(); $xa = workwith.filter(function (a) { return a == $v });
                        if ($xa.length > 0) $(this).prop('selected', true);
                    });
                    $('#eWork').multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                    if (TBPlan[0].FWFlg == 'W' || TBPlan[0].FWFlg == 'H' || TBPlan[0].FWFlg == 'L' || TBPlan[0].FWFlg == 'N') {
                        $('.hidedist').hide();
                        $('.hideroute').hide();
                        $('.hideww').hide();
                    }
                    if (TBPlan[0].FWFlg == 'F') {
                        $('.hidedist').show();
                        $('.hideroute').show();
                        $('.hideww').show();
                    }
                    if (TBPlan[0].FWFlg == 'DH') {
                        $('.hidedist').hide();
                        $('.hideroute').show();
                        $('.hideww').show();
                    }
                },
                error: function (rs) {

                }
            });
        }
        function getTPRoutes(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_My_Day_Plan_View.aspx/getSFTPRoute",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPRoutes = JSON.parse(data.d) || [];
                    var tpr = $("#eroute");
                    tpr.empty().append($('<option value="0">Select Route</option>'));
                    for (var i = 0; i < TPRoutes.length; i++) {
                        tpr.append($('<option value="' + TPRoutes[i].Territory_Code + '">' + TPRoutes[i].Territory_Name + '</option>'));
                    }
                    $('#eroute').selectpicker({
                        liveSearch: true
                    });
                    $('#eroute').selectpicker('refresh');
                },
                error: function (rs) {

                }
            });
        }
        function getTPDist(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_My_Day_Plan_View.aspx/getSFTPDist",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPDist = JSON.parse(data.d) || [];
                    var tpr = $("#edist");
                    tpr.empty().append($('<option value="0">Select Distributor</option>'));
                    for (var i = 0; i < TPDist.length; i++) {
                        tpr.append($('<option value="' + TPDist[i].Stockist_Code + '">' + TPDist[i].Stockist_Name + '</option>'));
                    }
                    $('#edist').selectpicker({
                        liveSearch: true
                    });
                    $('#edist').selectpicker('refresh');
                },
                error: function (rs) {

                }
            });
        }
        function openmodal($x) {
            sf = $x; clear();
            $('#sDate').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                minDate: new Date(Date.parse('<%=Date%>')),
                maxDate: new Date(Date.parse('<%=Date%>' + ' 23:59')),
                timePicker24Hour: true,
                timePickerSeconds: true,
                locale: {
                    format: 'DD-MM-YYYY H:mm:ss'
                }
            });
            $('#eDate').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                minDate: new Date(Date.parse('<%=Date%>')),
                maxDate: new Date(Date.parse('<%=Date%>' + ' 23:59')),
                timePicker24Hour: true,
                timePickerSeconds: true,
                locale: {
                    format: 'DD-MM-YYYY H:mm:ss'
                }
            }); getTPWtypes($x); getTPRoutes($x); getTPDist($x); getTPWorkWith($x); getManagerHQ($x);
            getDayplan($x);
        }
        function clear() {
            $('#sDate').val('');
            $('#eDate').val('');
            $('#edist').empty();
            $('#eroute').empty();
            $('#eWork').empty();
            $('#eWorktyp').empty();
            $('#eremarks').empty();
        }
        $(document).ready(function () {
            $('.hidecll').closest('td').hide();
            $('.hidecell').hide();
            $('.hidecell').hide();
            <%--if (('<%=div_code%>'== '100' || '<%=div_code%>'== '107' || '<%=div_code%>'== '91') && '<%=sf_code%>'=='admin') {
                $('.hidecell').show();
                $('.hidecll').closest('td').show();
            }--%>
            if ('<%=DBase_EReport.Global.AttanceEdit%>'=='0') {
                $('.hidecell').show();
                $('.hidecll').closest('td').show();
            }
            $("#ddlrephq").on("change", function () {
                let selsf = $("#ddlrephq").val();
                getTPDist(selsf); getTPRoutes(selsf);
            });
            $('#eWorktyp').on('change', function () {
                var sworktyp = $('#eWorktyp :selected').text();
                if (sworktyp == 'Weekly Off' || sworktyp == 'Holiday' || sworktyp == 'Leave' || sworktyp == 'Meeting' || sworktyp == 'Meeting') {
                    $('.hidedist').hide();
                    $('.hideroute').hide();
                    $('.hideww').hide();
                }
                if (sworktyp == 'Field Work' || sworktyp == 'INDIVIDUAL WORK' || sworktyp == 'Group work') {
                    $('.hidedist').show();
                    $('.hideroute').show();
                    $('.hideww').show();
                }
                if (sworktyp == 'New Distributor Hunting') {
                    $('.hidedist').hide();
                    $('.hideroute').show();
                    $('.hideww').show();
                }
            });
        })
        function updateDayplan() {
            var starttime = $('#sDate').val();
            var endtime = $('#eDate').val();
            var stdate = starttime.split(' ');
            var strdate = stdate[0].split('-');
            var fnlsdate = strdate[2] + '-' + strdate[1] + '-' + strdate[0] + ' ' + stdate[1];
            var etdate = endtime.split(' ');
            var etrdate = etdate[0].split('-');
            var fnledate = etrdate[2] + '-' + etrdate[1] + '-' + etrdate[0] + ' ' + etdate[1];
            var worktyp = $('#eWorktyp').val();
            var worktypname = $('#eWorktyp :selected').text();
            var workedwithc = $("#ddlrephq").val();
            var workedwithn = $("#ddlrephq :selected").text();
            if (workedwithc == "" || workedwithc == null) {
                workedwithc = "";
                workedwithn = "";
            }
            if (worktyp == '0') {
                alert('Select the Worktype');
                return false;
            }
            var distn = ($('#edist :selected').text()).replaceAll("&", "and");
            var distc = $('#edist').val();
            if ((worktypname == 'Field Work') && distc == '0') {
                alert('Select the Distributor');
                return false;
            }
            var routen = ($('#eroute :selected').text() == 'Select Route') ? '' : $('#eroute :selected').text();
            var routec = $('#eroute').val();
            if ((worktypname == 'Field Work' || worktypname == 'Van Sales' || worktypname == 'New Distributor Hunting') && routec == '0') {
                alert('Select the Route');
                return false;
            }
            var workwithn = '';
            var workwithc = '';
            $('#eWork  > option:selected').each(function () {
                workwithc += $(this).val() + '$$';
                workwithn += $(this).text() + ',';
            });
            //if (workwithc == '') {
            //    alert('Select the Worked With');
            //    return false;
            //}
			var remarks = $('#eremarks').val();
            var sXml = "<DYP sfc=\"" + sf + "\" wrkwtc=\"" + workedwithc + "\" wtyc=\"" + worktyp + "\" wtyn=\"" + worktypname + "\" distn=\"" + distn + "\" distc=\"" + distc + "\" routen=\"" + routen + "\" routec=\"" + routec + "\" workwithn=\"" + workwithn + "\" workwithc=\"" + workwithc + "\" fnlsdate=\"" + fnlsdate + "\" fnledate=\"" + fnledate + "\" remarks=\"" + remarks+"\" />";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_My_Day_Plan_View.aspx/svDayplan",
                data: "{'sfcode':'" + sf + "','PlnDate':'" + fnlsdate + "','sxml':'" + sXml + "'}",
                dataType: "json",
                success: function (data) {
                    alert("DayPlan Edited");
                    $('#editmodal').modal('hide');
                    window.location.reload();
                },
                error: function (result) {
                    alert(result.responseText);
                }
            })
        }
    </script>
    <style type="text/css">
        p.serif
        {
            font-family: "Wingdings" , Times, serif;
        }
      /*<%--.tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }--%>*/
        .tbldetail_main
        {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }
        .tbldetail_Data
        {
            height: 18px;
        }
        .Holiday
        {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
            .daterangepicker {
                z-index: 10000000;
            }
        .NoRecord
        {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
        div.dropdown.bootstrap-select.bs3{
            width:300px !important;
        }
    </style>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
	<style type="text/css">
		/*popup image*/

	    .phimg img {
	        border-radius: 5px;
	        cursor: pointer;
	        transition: 0.3s;
	    }
        .phimg img:hover {
            opacity: 0.7;
        }
		
		/* The Modal (background) */
	    .modal {
	        
	        display: none; /* Hidden by default */
	        position: fixed; /* Stay in place */
	        z-index: 1; /* Sit on top */
            padding-top: 7%;
	        /*padding-top: 100px;*/ /* Location of the box */
	        left: 0;
	        top: 0;
	        margin-left: 25%;
            width: 40%;
            height: 100%;
	        /*width: 50%;*/ /* Full width */
	        /*height: 100%;*/ /* Full height */
	        overflow: auto; /* Enable scroll if needed */
	        background-color: rgb(0,0,0); /* Fallback color */
	        background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
	    }
		
		/* Modal Content (Image) */
		.modal-content {
			margin: auto;
			display: block;
			width: 80%;
			height: 80%;
		}
		
		/* Caption of Modal Image (Image Text) - Same Width as the Image */
		#caption {
			margin: auto;
			display: block;
			width: 80%;
			max-width: 700px;
			text-align: center;
			color: #ccc;
			padding: 10px 0;
			height: 150px;
		}
		
		/* Add Animation - Zoom in the Modal */
		.modal-content, #caption { 
			-webkit-animation-name: zoom;
			-webkit-animation-duration: 0.6s;
			animation-name: zoom;
			animation-duration: 0.6s;
		}
		
		@-webkit-keyframes zoom {
			from {-webkit-transform:scale(0)} 
			to {-webkit-transform:scale(1)}
		}
		
		@keyframes zoom {
			from {transform:scale(0)} 
			to {transform:scale(1)}
		}
		
		/* The Close Button */
		.close {
			position: absolute;
			top: 15px;
			right: 35px;
			color: #f1f1f1;
			font-size: 40px;
			font-weight: bold;
			transition: 0.3s;
		}
		
		.close:hover,
		.close:focus {
			color: #bbb;
			text-decoration: none;
			cursor: pointer;
		}
		
		/* 100% Image Width on Smaller Screens */
		@media only screen and (max-width: 700px){
			.modal-content {
				width: 100%;
			}
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="modal fade" id="editmodal" tabindex="-1" style="z-index: 1000000; background-color: transparent;"
        role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document" style="width: 100%;position: fixed;top:0;right:0;left:0;bottom:0">
            <div class="modal-content" style="width: 30%;height:80%">
                <div class="modal-header">
                    <div class="row">
                        <div class="col-sm-10">
                            <h5 class="modal-title">
                                DayPlan Edit</h5>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                </div>
                <div class="modal-body" style="padding-top: 10px">
                    <div class="row">
                        <div class="col-sm-12">
                            <table width="100%" style="table-layout: fixed">
                                <tr>
                                    <td>
                                        Start Time
                                    </td>
                                    <td>
                                        End Time
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 16px;">
                                        <input type="text" class="form-control" id="sDate" />
                                    </td>
                                    <td style="padding-right: 16px;">
                                        <input type="text" class="form-control" id="eDate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>HQ
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <select id="ddlrephq"></select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Work Type
                                    </td>
									</tr>
                                <tr>
								<td>
                                        <select id="eWorktyp">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="hidedist">
                                        Distributor
                                    </td>
									</tr>
                                <tr>
                                    <td class="hidedist">
                                        <select id="edist" data-size="13">
                                        </select>
                                    </td>
                                </tr>
                                <tr  class="hideroute">
                                    <td>
                                        Route
                                    </td>
                                </tr>
                                <tr  class="hideroute">
                                    <td>
                                        <select data-size="13" id="eroute">
                                        </select>
                                    </td>
                                </tr>
                                <tr class="hideww">
                                    <td colspan="2">
                                        Worked With
                                    </td>
                                </tr>
                                <tr class="hideww">
                                    <td colspan="2">
                                        <select id="eWork" multiple>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Remarks
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <input class="form-control" type="text" id="eremarks" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">
                        Close</button>
                    <button type="button" class="btn btn-primary" id="btnupdate" onclick="updateDayplan()">
                        Update</button>
                </div>
            </div>
        </div>
    </div>
    <div>
        <center>
            <asp:Panel ID="pnlbutton" runat="server">
                <br />
                <table width="95%">
                    <tr>
                        <td width="70%">
                        </td>
                        <td width="30%" align="right">
                            <table>
                                <tr>
                                    <td style="padding-left: 5px">
                                        <asp:LinkButton ID="btnPDF" runat="server" class="btn btnPdf" OnClick="btnPDF_Click" />
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:LinkButton ID="btnPrint" runat="server" class="btn btnPrint" OnClick="btnPrint_Click" />
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:LinkButton ID="btnExcel" runat="server" class="btn btnExcel" OnClick="btnExcel_Click" />
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:LinkButton ID="btnClose" runat="server" href="javascript:window.open('','_self').close();"
                                            class="btn btnClose" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <table border="0" width="95%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="X-Large" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="Lest">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblHead" runat="server" Font-Bold="True" Style="float: right"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%--<td align="center">
                            <span style="font-family: Verdana">Field Force Name :</span>
                            <asp:Label ID="lblFieldForceName" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <div id="dvPrintXLFull" runat="server">
                                <asp:GridView ID="gvMyDayPlan" runat="server" Width="100%" HorizontalAlign="Center"
                                    OnRowDataBound="GridView1_RowDataBound" BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View"
                                    AutoGenerateColumns="false" CssClass="newStly">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Pln_Date" runat="server" Font-Size="9pt" Text='<%#Eval("Pln_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Id" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="empid" runat="server" Font-Size="9pt" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Force Name" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Zone" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Zone" runat="server" Font-Size="9pt" Text='<%#Eval("Zone")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State Name" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="StateName" runat="server" Font-Size="9pt" Text='<%#Eval("StateName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
										 <asp:TemplateField HeaderText="Division Name" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="dv_name" runat="server" Font-Size="9pt" Text='<%#Eval("subdivision_code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting Manager " ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="SF_name12" runat="server" Font-Size="9pt" Text='<%#Eval("rsf")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Mob" runat="server" Font-Size="9pt" Text='<%#Eval("MOb")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Time" ItemStyle-Width="80" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Pln_Time" runat="server" Font-Size="9pt" Text='<%#Eval("Pln_Time")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WorkType Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtWorkTypeName" runat="server" Font-Size="9pt" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor Name" ItemStyle-Width="150" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtWorkTypeddd" runat="server" Font-Size="9pt" Text='<%#Eval("dist_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
										   <asp:TemplateField HeaderText="Distributor Address" ItemStyle-Width="150" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtstkadrs" runat="server" Font-Size="9pt" Text='<%#Eval("Stockist_Address")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                            HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtClustername" runat="server" Font-Size="9pt" Text='<%#Eval("ClstrName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtremarks" runat="server" Font-Size="9pt" Text='<%#Eval("remarks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joint Work With" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtjointwrk" runat="server" Font-Size="9pt" Text='<%#Eval("worked_with_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
										  <asp:TemplateField HeaderText="Lat">
                                   <ItemTemplate>
                                   <asp:Label ID="LAt" runat="server" Text='<%#Eval("Start_Lat") %>' Width="80">
                                   </asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Long">
                                   <ItemTemplate>
                                   <asp:Label ID='Long' runat="server" Text='<%#Eval("Start_Long") %>' Width="80"></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Address">
                                   <ItemTemplate>
                                   <asp:Label ID='Addrss' runat="server" Text='' Width="200"></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Login" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtlogin" runat="server" Font-Size="9pt" Text='<%#Eval("Start_Time")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Log Out" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtlogout" runat="server" Font-Size="9pt" Text='<%#Eval("End_Time")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
										    <asp:TemplateField HeaderText="Day End Remark" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtrmrk" runat="server" Font-Size="9pt" Text='<%#Eval("Rmks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Today New Retailers" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="txtre" runat="server" Font-Size="9pt" Text='<%#Eval("newDoor")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Version" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                            ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="txtvers" runat="server" Font-Size="9pt" Text='<%#Eval("versions")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attendance" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="L1" runat="server" Style="font-family: Wingdings, Times, serif; color: Green;
                                                    font-size: 25px;" Visible="false">&#61692;</asp:Label>
                                                <asp:Label ID="L2" runat="server" Style="font-family: Wingdings, Times, serif; color: Red;
                                                    font-size: 25px;" Visible="false">&#61691;</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("order_status")%>' />
                                                <asp:Label ID="L5" runat="server" Style="font-family: Wingdings, Times, serif; color: Green;
                                                    font-size: 25px;" Visible="false">&#61692;</asp:Label>
                                                <asp:Label ID="L6" runat="server" Style="font-family: Wingdings, Times, serif; color: Red;
                                                    font-size: 25px;" Visible="false">&#61691;</asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DailyInventory" ItemStyle-HorizontalAlign="center"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("inventry_status")%>' />
                                                <asp:Label ID="L3" runat="server" Style="font-family: Wingdings, Times, serif; color: Green;
                                                    font-size: 25px;" Visible="false">&#61692;</asp:Label>
                                                <asp:Label ID="L4" runat="server" Style="font-family: Wingdings, Times, serif; color: Red;
                                                    font-size: 25px;" Visible="false">&#61691;</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TL Closingstock" ItemStyle-HorizontalAlign="center"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("Endinvent_status")%>' />
                                                <asp:Label ID="L7" runat="server" Style="font-family: Wingdings, Times, serif; color: Green;
                                                    font-size: 25px;" Visible="false">&#61692;</asp:Label>
                                                <asp:Label ID="L8" runat="server" Style="font-family: Wingdings, Times, serif; color: Red;
                                                    font-size: 25px;" Visible="false">&#61691;</asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image">
                                            <ItemTemplate>
                                                <div class="phimg" data-url='<%# Eval("events") %>'>
												<img alt="img" style="width:30px ;height:30px" class='phimg' onclick='imgPOP(this)' src='<%# Eval("events") %>' />
                                                    <%-- <a href="<%# Eval("events") %>">
                                                        <asp:Image ID="Image1" runat="server" Width="150" Height="100" ImageUrl='<%# Eval("events","appurl") %>'
                                                            onclick="imgPOP(this)" />--%>
                                                    <%--</a>--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image" Visible="false">
                                            <ItemTemplate>
                                                <div class="phimg" data-url='<%# Eval("logoutImg") %>'>
												<img alt="img" style="width:30px ;height:30px" class='phimg' onclick='imgPOP(this)' src='<%# Eval("logoutImg") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="hidecell" HeaderText="Edit Attendance" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <a href="#" class="hidecll" sfc="<%#Eval("Sf_Code")%>" onclick="openmodal('<%#Eval("Sf_Code")%>')"
                                                    data-toggle="modal" data-target="#editmodal"><i class="fa fa-edit"></i>EDIT</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
        </center>
		<div id="myModal" class="modal" style="opacity: 1;">
			<!-- The Close Button -->
			<span class="close" style="opacity: 1;">&times;</span>
			
			<!-- Modal Content (The Image) -->
			<img class="modal-content" alt="" id="img01" />
			
			<!-- Modal Caption (Image Text) -->
			<div id="caption"></div>
		</div>
    </div>
    </form>
	
	<script type="text/javascript">

        function imgPOP(x) {
            // Get the modal
            var modal1 = document.getElementById('myModal');

            // Get the image and insert it inside the modal - use its "alt" text as a caption
            var img = document.getElementById('myImg');
            var modalImg = document.getElementById("img01");
            var captionText = document.getElementById("caption");
            //modal1.style.display = "block";
            $('#myModal').css("display", "block");
            modalImg.src = x.src;
            captionText.innerHTML = x.alt;
        }

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            $('#myModal').css("display", "none");
            //modal1.style.display = "none";
        }
    </script>
</body>

</html>
