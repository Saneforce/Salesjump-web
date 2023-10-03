<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RetailerWies_Visited_Nonvisited_Count.aspx.cs" Inherits="MIS_Reports_RetailerWies_Visited_Nonvisited_Count" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link href="../css/datepicker/datepicker3.css" rel="stylesheet" />
    <link href="../css/bs-datetimepicker/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../css/bs-datetimepicker/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <h3>Retailer Visit Status</h3>
    <br />
    <br />
    <br />
    <div class="form-group">
        <div class="row">
            <label class="col-md-2 col-md-offset-3 control-label">Division</label>
            <div class="col-md-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <select id="ddsubdiv" class="col-xs-2" style="width: 250px;"><option value="" disabled selected>Nothing  Select</option></select>
                </div>
            </div>
        </div>
        <div class="row">
            <label class="col-md-2 col-md-offset-3 control-label">Field Force</label>
            <div class="col-md-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <select id="ddFieldf" style="width: 250px;" class="col-xs-2"><option value="" disabled selected>Nothing  Select</option></select>
                </div>
            </div>
        </div>
         <div class="row" style="display:none;">
            <label class="col-md-2 col-md-offset-3 control-label">Visited Type</label>
            <div class="col-md-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-bookmark"></i></span>
                    <select id="ddlVisit_typ" style="width: 250px;" class="col-xs-2">
                        <option value="" disabled selected>Nothing  Select</option>
                        <option value="All" >All</option>
                        <option value="Non Visited" >Non Visited</option>
                        <option value="Billed" >Billed</option>
                        <option value="Not Billed" >Not Billed</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
            <label class="col-md-2 col-md-offset-3 control-label">From Date</label>&nbsp;&nbsp;
             <div class="col-md-6 inputGroupContainer">
                 <div class="input-group" id="Div2" runat="server">
                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                     <input id="txtFromDate" autocomplete="off" type="text" class="col-xs-2" style="width: 250px; background-color: white; line-height: 20px;font-size: 15px;" />
                 </div>
             </div>
        </div>
        <div class="row">
            <label class="col-md-2 col-md-offset-3 control-label">To Date</label>&nbsp;&nbsp;
                             <div class="col-md-6 inputGroupContainer">
                                 <div class="input-group" id="Div1" runat="server">
                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                     <input id="txtToDate" autocomplete="off" type="text" class=" col-xs-2" style="width: 250px; background-color: white; line-height: 20px;font-size: 15px;" />
                                 </div>
                             </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-md-6 col-md-offset-5">
                <input type="button" value="View" class="btn btn-primary" id="BtnView" />
            </div>
        </div>
    </div>
    <script src="../js/daterangepicker-3.0.5.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <script type="text/javascript">
        var SubDivision = [], FieldForce = []; var str = '', sub_div = '';
        var sf_code = '', Fdate = '', Tdate = '',Type='';
        $(document).ready(function () {
            $("#txtFromDate").datepicker({  maxDate: new Date() });
            $("#txtToDate").datepicker({  maxDate: new Date() });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "RetailerWies_Visited_Nonvisited_Count.aspx/fillsubdivision",
                datatype: "json",
                success: function (data) {
                    SubDivision = JSON.parse(data.d) || []
                    if (SubDivision.length > 0) {
                        str = '';
                        for (var i = 0; i < SubDivision.length; i++) {
                            str += "<option  value="+SubDivision[i].subdivision_code+">" + SubDivision[i].subdivision_name + "</option>";
                        }
                        $('#ddsubdiv').append(str);
                    }
                    else
                        alert(data.d);
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "RetailerWies_Visited_Nonvisited_Count.aspx/fillFF",
                datatype: "json",
                success: function (data) {
                    FieldForce = JSON.parse(data.d) || []
                    if (FieldForce.length > 0) {
                        str = '';
                        for (var $i = 0; $i < FieldForce.length; $i++) {
                            //if(FieldForce[$i].Sf_Code!='admin')
                            str += "<option value="+FieldForce[$i].Sf_Code+">" + FieldForce[$i].SFNA + "</option>";
                        }
                        $('#ddFieldf').append(str);
                    }
                    else
                        alert('No Field Force Found');
                },
                error: function (res) {
                    alert(res);
                }
            });
			$('#ddsubdiv').on('change',function(){
				var subdiv=$('#ddsubdiv').val();str='';
				$('#ddFieldf').html("");
				var fltrarr=FieldForce.filter(function (a) {
                    return a.subdivision_code.indexOf(subdiv)>-1;
				})
				str="<option value='' disabled selected>Nothing  Select</option>";
				str += "<option value='admin'>admin</option>";
				for (var $i = 0; $i < fltrarr.length; $i++) {					
                   str += "<option value="+fltrarr[$i].Sf_Code+">" + fltrarr[$i].SFNA + "</option>";
				}
				$('#ddFieldf').append(str);
			});			
        });
			
        $('#BtnView').on('click', function () {
            sub_div = $('#ddsubdiv option:selected').val();
            sf_code = $('#ddFieldf option:selected').val(); Fdate = $('#txtFromDate').val(), Tdate = $('#txtToDate').val();
            //Type = $('#ddlVisit_typ option:selected').val();
            if (sub_div == 'Nothing  Select' || sub_div == '') { alert('Select Division'); return false; }
            if (sf_code == 'Nothing  Select' || sf_code == '') { alert('Select Field Force'); return false; }
            if (Fdate == '') { alert('Select From Date'); return false; }
            if (Tdate == '') { alert('Select To Date'); return false; }
            //if (Type == 'Nothing  Select' || Type == '') { alert('Select VisitType'); return false; }
            var frmdate = new Date($('#txtFromDate').val());
            var todate = new Date($('#txtToDate').val());
            var datediff = (todate - frmdate) / (1000 * 60 * 60 * 24);
            if (datediff > 180) {
                alert('Selected Date With in 180days');
            }
            else {
                //var url = "RetailerWiseBilled_Unbilled.aspx?sf_code=" + sf_code + "&FDate=" + Fdate + "&Tdate=" + Tdate + "&Type=" + Type + "";                
                var url = "RetailerWiseBilled_Unbilled.aspx?sf_code=" + sf_code + "&FDate=" + Fdate + "&Tdate=" + Tdate + "";
                window.open(url, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            }
        })
    </script>
</asp:Content>

