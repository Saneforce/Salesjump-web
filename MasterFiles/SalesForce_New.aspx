<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SalesForce_New.aspx.cs" Inherits="MasterFiles_SalesForce_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title></title>
        <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        <style type="text/css">
            table {
                border-collapse: separate;
                border-spacing: 9px;
                width: 100%;
            }
            .is-invalid{
                outline: none;
                box-shadow: 0 0 0 3px rgba(255, 0, 0, 0.4);
            }
            .scrldiv, .scrldiv2 {
                width: 545px;
                height: 116px;
            }

                .scrldiv input {
                    margin-top: 10px;
                }

                .scrldiv2 input {
                    margin-top: 10px;
                }

            label {
                font-size: 13px;
                font-family: 'Open Sans','arial';
                font-weight: 700;
                margin: 0px;
            }

            .panel-default {
                border-color: #ddd !important;
                border: 1px solid transparent;
            }
            input {
                width: 100%;
                border: 1px solid #d8d8d8 !important;
                padding: 6px 6px 7px !important;
                border-radius: 3px !important;
                line-height: inherit !important;
                background: #fff;
                box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
                transition: all 0.3s ease;
                /*border: 1px solid #00809D;*/
            }
            input:focus{
                outline: none;
                box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
            }
            select {
                width: 100%;
                border: 1px solid #D5D5D5 !important;
                padding: 6px 6px 7px !important;
            }
            select:focus{
                outline: none;
                box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
            }
        </style>
    </head>
    <body>
		<div class="row">
        <div class="col-lg-12 sub-header">Employee Master <button style="float:right;" type="button" id="btnsubmit" class="btn btn-primary">Submit</button>
            </div>
        </div>
        <form style="background: #ffffff; box-shadow: 0px 3px 12px rgba(0, 0, 0, 0.25); border-radius: 8px;margin-top: 5px;">
            <div class="container">
                <div class="col-md-4">
                    <table id="upltbl" style="border-collapse: collapse;">
                        <tr>
                            <td colspan="2" align="center" style="padding-top: 57px; padding-right: 16px; /* padding-bottom: 57px; */padding-left: 16px;">
                                <img id="upimg" style="width: 80%; /* height: 0%; */" src="https://www.w3schools.com/w3css/img_avatar3.png" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="/* padding-top: 57px; */padding-right: 57px; padding-bottom: 16px; padding-left: 57px;">
                                <label for="uplfile" class="input-group-btn">
                                    <span class="btn btn-primary"><i class="fa fa-cloud-upload append-icon"></i>&nbsp Upload</span>
                                </label>
                                <!--<button for="uplfile" type="button" class="btn btn-primary">Upload</button>-->
                                <input accept=".jpg,.jpeg,.png" id="uplfile" class="sr-only" name="uplfile" type="file" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Status</label>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="spstat" class="label label-success">Active</span>
                                <a id="ffstatus" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <select id="fstat" style="width: 62% !important;display:none;">
                                    <option value="0">Active</option>
                                    <option value="2">Deactive</option>
                                    <option value="1">Vacant</option>
                                    <option value="3">Block</option>
                                </select>
                            </td>
                        </tr>
						<tr id="resroew" style="display:none;">
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Resignation Date</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="rffdttxt">Jan 01 2020</span>
                                <a id="rdate" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <input type="date" id="redate"  style="display:none;"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Type</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="fftyp">Non-Field</span>
                                <a id="fftype" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <select id="ddlftyp" style="width: 62% !important;display:none">
                                    <option value="FF">Field</option>
									 <option value="VS">Van Sales</option>
                                    <option value="NF">Non-Field</option>
                                    <option value="I">Inseminator</option>
                                </select>
                            </td>
                        </tr>
                        <tr id="sftyptr">
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Level</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="sfftyp">Base Level</span>
                                <a id="sfftype" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <select id="ddlsftyp" style="width: 62% !important;display:none">
                                    <option value="1">Base Level</option>
                                    <option value="2">Manager</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Since</label><span class="ffJdate" style="color:red;">*</span></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="ffdttxt">Jan 01 2020</span>
                                <a id="ffJdate" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <input type="date" id="fjdate" style="display:none;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;">
                                <label>Reporting To</label></td>
                            <td align="right" style="border-bottom: solid 1px #dcdcdc;">
                                <span id="ffrpttxt">Admin</span>
                                <a id="ffrpt" style="color: black;"><i class="fa fa-pencil" style="float: right; padding: 0px 10px; /* margin-top: 0px; */"></i></a>
                            </td>
                            <td align="left" style="border-bottom: solid 1px #dcdcdc;display:none;">
                                <select id="repto" data-dropup-auto="false"></select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-8">
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Account Setting</h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Email</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfemail" type="email" autocomplete="off" required />
                                <small id="EmailHelp" style="padding-left: 7px; display: none;" class="text-danger">Please provide a valid email address</small>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>UserName</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="input-group input-group-sm mb-3" style="display:flex">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text" style="padding: 5px 2px 5px 5px;background: #868383;color: white;border-radius: 4px 0px 0px 4px;" id="fixUsrn"></div>
                                            </div>
                                            <input type="text" class="form-control" id="sfusrname" style="border-radius: 0px 4px 4px 0px !important;" aria-label="Small" aria-describedby="inputGroup-sizing-sm" />
                                        </div>
                                    </div>
                                    <div class="col-sm-6" style="padding-left:30px">
                                        <input type="checkbox" id="usrauto" name="userauto" value="Yes" style="width: auto;margin: 8px 0px 8px 8px;" />
                                        <span style="font-size: 13px;">Auto-generate</span>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
							
                                <label>Password</label><span style="color:red;">*</span>
                            </td>
                            <td>
							<div class='image'>
                                    <span class='ab'><i class="fa fa-eye" onclick="myFunction()"></i></span>
                                <input class="col-xs-5" id="sfpwd" type="password" required />
                                 <small id="sfpwdHelp" style="padding-left: 7px; display: none;" class="text-danger">Password must contain: Min 6 and Max 12 Characters 1 UpperCase(A-Z),1 LowerCase(a-z) , 1 Number(0-9),1 Special Character(!@#$%^&*_=+-) Exp:PaSs@1</small>
								</div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>ConfirmPassword</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfcpwd" type="password" required />
                                <small id="passwordHelp" style="padding-left: 7px;display:none;" class="text-danger">Password doesn't Match</small>
                            </td>
                        </tr>
                    </table>
                    <div class="row">
                        <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Profile Setting &nbsp
                            <button type="button" class="btn btn-warning btn-circle btn-lg" data-toggle="modal" data-target="#exampleModal">+</button></h3>
                    </div>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>First Name</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" type="text" id="sffirstname" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Last Name</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sflastname" type="text" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Employee ID</label><span class="sfempid" style="color:red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-5" type="text" id="sfempid" autocomplete="off" required />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Date of Birth</label>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfdob" type="date" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Designation</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <select id="desg" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Division</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <select id="division" data-dropup-auto="false" multiple>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Department</label>
                            </td>
                            <td>
                                <select id="depts" class="selectpicker" data-live-search="true" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>State</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <select id="states" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>HeadQuarters</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <select id="hqs" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Territory</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <select id="routes" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>District</label>
                            </td>
                            <td>
                                <select id="ddldistrict" data-dropup-auto="false">
                                </select>
                            </td>
                        </tr>
                    </table>
                    <h3 style="font-weight: normal; font-family: 'Open Sans','arial';">Contact Setting &nbsp
                            <button type="button" class="btn btn-warning btn-circle btn-lg" data-toggle="modal" data-target="#modalcontact">+</button></h3>
                    <table>
                        <tr>
                            <td align="right" width="130px">
                                <label>Mobile Phone</label><span style="color:red;">*</span>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfmobil" autocomplete="off" type="text" maxlength="25" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Door No. & Street</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfaddr" type="text" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Area & Locality</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfarea" type="text" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>City</label>
                            </td>
                            <td>
                                <input class="col-xs-9" id="sfcity" type="text" autocomplete="off" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label>Pincode</label>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfpincode" type="text" maxlength="6" autocomplete="off" />
                            </td>
                        </tr>
                        <tr id="depo">
                            <td align="right">
                                <label>Depot</label>
                            </td>
                            <td>
                                <input class="col-xs-5" id="sfdepot" type="text" autocomplete="off" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="modal fade" id="exampleModal" tabindex="-1" style="z-index: 1000000;background-color:transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--<button type="button" id="btnpls" class="btn btn-info btn-circle btn-lg" style="float: right">+</button>--%>
                            <h5 class="modal-title" id="exampleModalLabel">Profile Fields</h5>
                        </div>
                        <div class="modal-body">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-6">Additional Fields</div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">Name</div>
                                        <div class="col-sm-6">Value</div>
                                    </div>
                                    <div class="scrldiv" style="height: 120px;">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <input name="field" type="text" autocomplete="off" />
                                            </div>
                                            <div class="col-sm-6">
                                                <input name="value" type="text" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-body -->
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modalcontact" tabindex="-1" style="z-index: 1000000;background-color:transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="bttnpls" class="btn btn-info btn-circle btn-lg" style="float: right">+</button>
                            <h5 class="modal-title" id="modalcontactLabel">Contact Fields</h5>
                        </div>
                        <div class="modal-body">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-6">Additional Fields</div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">Name</div>
                                        <div class="col-sm-6">Value</div>
                                    </div>
                                    <div class="scrldiv2" style="height: 120px;">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <input name="field" type="text" autocomplete="off" />
                                            </div>
                                            <div class="col-sm-6">
                                                <input name="value" type="text" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-body -->
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </form>
        <script type="text/javascript">
            var propic_name = ' ';
            var AddFields = [];
            var sfcode = '';
            var Itype = '1';
            var SfDetails = [];
            var usratuogenerate = false; var SFDesg = [];
            var SFStates = []; var SFDivi = [];
            var autogenusrname = ''; var SFDivisname = ''; var fstat1 = 0;
            function loadAdditionalFields() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getEmployeeFieldSettings",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        AddFields = JSON.parse(data.d) || [];
                        if (AddFields.length > 0) {
                            $('.scrldiv').empty();
                            for (var i = 0; i < AddFields.length; i++) {
                                if (AddFields[i].Field_Type == 'Text') {
                                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" mandatory="' + AddFields[i].Mandatory + '" value="' + AddFields[i].Field_name + '" autocomplete="off" /></div><div class="col-sm-6 cls"><input type="' + AddFields[i].Field_Type + '" name="value" id="' + AddFields[i].Field_Code + '" autocomplete="off" /></div></div>';
                                }
                                $('.scrldiv').append(newTr);
                            }
                        }
                    }
                });
            }
            function loaddivision() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getDivision",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFDivi = JSON.parse(data.d) || [];
                        if (SFDivi.length > 0) {
                            var divi = $("#division");
                            divi.empty();
                            for (var i = 0; i < SFDivi.length; i++) {
                                divi.append($('<option value="' + SFDivi[i].subdivision_code + '">' + SFDivi[i].subdivision_name + '</option>'));
                            }
                        }
                    }
                });
                $('#division').selectpicker({
                    liveSearch: true
                });
            }
            function getDivisname(fl) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getDivisionsname",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFDivisname = JSON.parse(data.d);

                        $('#fixUsrn').text(SFDivisname[0].Division_SName + "-");
                        if (fl == 1) $('#sfusrname').val(SFDivisname[0].uname);
                    }
                });
            }

            getDivisname(0);
            function loaddesignation() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getDesignation",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFDesg = JSON.parse(data.d) || [];
                        var sf_des = SFDesg.filter(function (a) {
                            return a.dtype == 1;
                        });
                        if (sf_des.length > 0) {
                            var desg = $("#desg");
                            desg.empty().append('<option selected="selected" value="0">Select Designation</option>');
                            for (var i = 0; i < sf_des.length; i++) {
                                desg.append($('<option value="' + sf_des[i].dcode + '">' + sf_des[i].dname + '</option>'));
                            }
                        }
                    }
                });
                $('#desg').selectpicker({
                    liveSearch: true
                });
            }
            function loadhq() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/GetHQDetails",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var SFHQs = JSON.parse(data.d) || [];
                        if (SFHQs.length > 0) {
                            var sfhq = $("#hqs");
                            sfhq.empty().append('<option selected="selected" value="0">Select HQ</option>');
                            for (var i = 0; i < SFHQs.length; i++) {
                                sfhq.append($('<option value="' + SFHQs[i].HQ_ID + '">' + SFHQs[i].HQ_Name + '</option>'));
                            }
                        }
                    }
                });
                $('#hqs').selectpicker({
                    liveSearch: true
                });
            }
            function loaddepts(ssdiv) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getDepts",
                    data: '{"divcode":"<%=Session["div_code"]%>","subdiv":"' + ssdiv + '"}',
                    dataType: "json",
                    success: function (data) {
                        var SFDepts = JSON.parse(data.d) || [];
                        if (SFDepts.length > 0) {
                            var dept = $("#depts");
                            dept.empty().append('<option selected="selected" value="0">Select Department</option>');
                            for (var i = 0; i < SFDepts.length; i++) {
                                dept.append($('<option value="' + SFDepts[i].DeptID + '">' + SFDepts[i].DeptName + '</option>'))
                            }
                        }
                    }
                });
                $('#depts').selectpicker('refresh');
            }
            function loadstates() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getStates",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFStates = JSON.parse(data.d) || [];
                        if (SFStates.length > 0) {
                            var states = $("#states");
                            states.empty().append('<option selected="selected" value="0">Select State</option>');
                            for (var i = 0; i < SFStates.length; i++) {
                                states.append($('<option value="' + SFStates[i].scode + '">' + SFStates[i].sname + '</option>'))
                            }
                        }
                    }
                });
                $('#states').selectpicker({
                    liveSearch: true
                });
            }
            function loadDistrict() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getDistrict",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        let sfdistrict = JSON.parse(data.d) || [];
                        if (sfdistrict.length > 0) {
                            let sdistrict = $("#ddldistrict");
                            sdistrict.empty().append('<option selected="selected" value="0">Select District</option>');
                            for (var i = 0; i < sfdistrict.length; i++) {
                                sdistrict.append($('<option value="' + sfdistrict[i].Dist_code + '">' + sfdistrict[i].Dist_name + '</option>'))
                            }
                        }
                    }
                });
                $('#ddldistrict').selectpicker({
                    liveSearch: true
                });
            }
            function loadReporting() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/GetReportingDetails",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var SFRep = JSON.parse(data.d) || [];
                        if (SFRep.length > 0) {
                            var sfrepto = $("#repto");
                            sfrepto.empty();
                             for (var i = 0; i < SFRep.length; i++) {
                                if (sfcode != SFRep[i].Sf_Code) {
                                    sfrepto.append($('<option value="' + SFRep[i].Sf_Code + '">' + SFRep[i].sf_name + '</option>'))
                                }
                            }
                        }
                    }
                });
                $('#repto').selectpicker({
                    liveSearch: true
                });
                //$('#repto').closest('td').find('button').hide();
            }
            function loadTerritory() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_New.aspx/getTerritory",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var SFTer = JSON.parse(data.d) || [];
                        if (SFTer.length > 0) {
                            var tert = $("#routes");
                            tert.empty().append('<option selected="selected" value="0">Select Territory</option>');
                            for (var i = 0; i < SFTer.length; i++) {
                                tert.append($('<option value="' + SFTer[i].tcode + '">' + SFTer[i].tname + '</option>'))
                            }
                        }
                    }
                });
                $('#routes').selectpicker({
                    liveSearch: true
                });
            }
            function filtDesg($de) {
                $desgfilt = SFDesg.filter(function (a) {
                    return a.dtype == $de;
                });
                if ($desgfilt.length > 0) {
                    var desg = $("#desg");
                    desg.empty().append('<option selected="selected" value="0">Select Designation</option>');
                    for (var i = 0; i < $desgfilt.length; i++) {
                        desg.append($('<option value="' + $desgfilt[i].dcode + '">' + $desgfilt[i].dname + '</option>'));
                    }
                }
                $('#desg').selectpicker('refresh');
            }
            function clearfields() {
                $(document).find('input[name="field"]').val('');
                $(document).find('input[name="value"]').val('');
                $(document).find('.cls').closest(".row").remove();
                $(document).find('.cls').closest(".row").remove();
                $('#sfemail').val('');
                $('#sfusrname').val('');
                $('#sfpwd').val('');
                $('#sfcpwd').val('');
                $('#sffirstname').val('');
                $('#sflastname').val('');
                $('#sfempid').val('');
                $('#sfdob').val('');
                $('#desg').val();
                $('#depts').val();
                $('#states').val();
                $('#hqs').val();
                $('#routes').val();
                $('#sfmobil').val('');
                $('#sfaddr').val('');
                $('#sfcity').val('');
                $('#sfarea').val('');
                $('#sfpincode').val('');
                $('#fstat').val();
                $('#ddlftyp').val();
                $('#fjdate').val('');
                $('#repto').val();
                $('#sfdepot').val('');
				$('#redate').val('');
            }
			function myFunction() {
                var x = document.getElementById("sfpwd");
                if (x.type === "password") {
                    x.type = "text";
                } else {
                    x.type = "password";
                }
            }
            $(document).ready(function () {
                if ('<%=Session["div_code"]%>' == '107') {
                    $('#depo').show();
                }
                else {
                    $('#depo').hide();
                }
                sfcode = '<%=sf_code%>';
                loadAdditionalFields();
                loaddivision();
                loadhq();
                loadstates();
                loadDistrict();
                loadTerritory();
                loaddesignation();
                loadReporting();
                if (sfcode == '') {
				    fstat1 = 0;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "SalesForce_New.aspx/getNewSfCode",
                        data: "{'typ':'2'}",
                        dataType: "json",
                        success: function (data) {
                            sfcode = data.d;
                            Itype = '0';
                            $('#ffstatus').closest('td').css('display', 'none');
                            $('#fftype').closest('td').css('display', 'none');
                            $('#sfftype').closest('td').css('display', 'none');
                            $('#ffJdate').closest('td').css('display', 'none');
                            $('#rdate').closest('td').css('display', 'none');
                            $('#ffrpt').closest('td').css('display', 'none');
                            $('#sftyptr').show();
                            $('#fstat').show();
                            $('#ddlftyp').show();
                            $('#ddlsftyp').show();
                            $('#fjdate').show();
                            $('#redate').show();
                            $('#repto').closest('td').show();
                        },
                        error: function (rs) {
                            alert(rs);
                        }
                    });
                }
                else {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "SalesForce_New.aspx/getSfDets",
                        data: "{'sfcode':'" + sfcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            SfDetails = data.d;
                            Itype = '1';
                            $('#sfemail').val(SfDetails[0].SfEmail);
                            if (SfDetails[0].sfusrname.indexOf("-") > -1) {
                                var sfausrname = SfDetails[0].sfusrname.split("-");
                                $('#fixUsrn').text(sfausrname[0] + "-");
                                $('#sfusrname').val(sfausrname[1]);
                            } else {
                                $('#fixUsrn').text("");
                                $('#sfusrname').val(SfDetails[0].sfusrname)
                            }
                            $('#sfpwd').val(SfDetails[0].sfpwd);
                            $('#sfcpwd').val(SfDetails[0].sfpwd); var splitsdiv = SfDetails[0].sfselsdiv.split(',') || [];
                            var csdiv = '';
                            if (splitsdiv.length > 0) {
                                for (var i = 0; i < splitsdiv.length; i++) {
                                    $('#division option[value=' + splitsdiv[i] + ']').attr('selected', true);
                                    csdiv += '\'' + splitsdiv[i] + '\',';
                                }
                                loaddepts(csdiv);
                            }
                            $('#division').selectpicker('refresh');
                            $('#sffirstname').val(SfDetails[0].SfName);
                            $('#sflastname').val('');
                            if (SfDetails[0].sfpimg != null && SfDetails[0].sfpimg != '' && SfDetails[0].sfpimg != ' ') {
                                $('#upimg').attr('src', 'http://fmcg.sanfmcg.com//SalesForce_Profile_Img/' + SfDetails[0].sfpimg);
                                propic_name = SfDetails[0].sfpimg;
                            }
                            else {
                                $('#upimg').attr('src', 'https://www.w3schools.com/howto/img_avatar.png')
                                propic_name = null;
                            }
                            $('#sfempid').val(SfDetails[0].sfempid);
                            $('#ddldistrict').val(SfDetails[0].sfdistrict);
                            $('#ddldistrict').selectpicker('refresh');
                            var sdob = (SfDetails[0].sfdob).split(' ');
                            sdob = sdob[0].split('/');
                            sdob = sdob[2] + '-' + sdob[1] + '-' + sdob[0];
                            $('#sfdob').val(sdob);
                            $('#depts').selectpicker('val', SfDetails[0].sfdept);
                            $('#states').selectpicker('val', SfDetails[0].sfstate);
                            $('#hqs').selectpicker('val', SfDetails[0].sfhq);
                            $('#routes').selectpicker('val', SfDetails[0].sfterr);
                            $('#sfmobil').val(SfDetails[0].sfmobile);
                            $('#sfaddr').val(SfDetails[0].sfaddr);
                            $('#usrauto').closest('td').find('span').hide();
                            $('#usrauto').hide();
                            var city = (SfDetails[0].sfcity).split('-');
                            $('#sfcity').val(city[0]);
                            $('#sfpincode').val(city[1]);
                            $('#sfarea').val(SfDetails[0].sfarea);
                            $('#fstat').val(SfDetails[0].sfstatus);
														
                            fstat1 = SfDetails[0].sfstatus;

                            //alert(fstat1);
							
                            $('#spstat').text($('#fstat :selected').text());
                            $('#ddlftyp').val(SfDetails[0].sftype);
                            $('#ddlsftyp').val(SfDetails[0].sflvl);
                            // $('#ddlsftyp').prop('disabled',true);
                            $('#sfftyp').text($('#ddlsftyp :selected').text());
                            // $('#sftyptr').hide();
                            filtDesg(SfDetails[0].sflvl);
                            $('#fftyp').text($('#ddlftyp :selected').text());
                            var sjdate = (SfDetails[0].sfjdate).split(' ');
                            sjdate = sjdate[0].split('/');
                            sjdate = sjdate[2] + '-' + sjdate[1] + '-' + sjdate[0];
                            var mdt = moment(sjdate);
                            var txtdt = mdt.format("MMM Do YYYY");
                            $('#ffdttxt').text(txtdt);
                            $('#fjdate').val(sjdate);
                            var redate = (SfDetails[0].resndate).split(' ');
                            redate = redate[0].split('/');
                            redate = redate[2] + '-' + redate[1] + '-' + redate[0];
                            var rmdt = moment(redate);
                            var rtxtdt = rmdt.format("MMM Do YYYY");
                            $('#rffdttxt').text(rtxtdt);
                            $('#redate').val(redate);
                            $('#repto').selectpicker('val', SfDetails[0].sfreport);
                            $('#ffrpttxt').text($('#repto :selected').text());
                            $('#sfdepot').val(SfDetails[0].sfdepot);
                            $('#desg').selectpicker('val', SfDetails[0].sfdesg);
                            //$(document).find('.scrldiv .row').closest(".row").remove();
                            $(document).find('.scrldiv2 .row').empty();
                            //$(document).find('input[name="field"]').remove();
                            //$(document).find('input[name="value"]').remove();
                            for (var i = 1; i < SfDetails.length; i++) {
                                var txtfield = "txtfield" + i;
                                var txtval = "txtval" + i;
                                if (SfDetails[i].pname != null) {
                                    $('#' + SfDetails[i].pname).val(SfDetails[i].pval);
                                }
                            }
                            for (var i = 1; i < SfDetails.length; i++) {
                                var txtfield = "txtfield" + i;
                                var txtval = "txtval" + i;
                                if (SfDetails[i].cname != null) {
                                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' value="' + SfDetails[i].cname + '" /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' value="' + SfDetails[i].cval + '" /></div></div>';
                                    $('.scrldiv2').append(newTr);
                                }
                            }
                        },
                        error: function (rs) {
                            alert(rs);
                        }
                    });
                }
                var ctr = 1;
                $('#btnpls').on('click', function () {
                    ctr++;
                    var txtfield = "txtfield" + ctr;
                    var txtval = "txtval" + ctr;
                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' /></div></div>';
                    $('.scrldiv').append(newTr);
                });
                var ctr2 = 1;
                $('#bttnpls').on('click', function () {
                    ctr++;
                    var txtfield = "txtfield" + ctr2;
                    var txtval = "txtval" + ctr2;
                    var newTr = '<div class="row"><div class="col-sm-6 cls"><input type="text" name="field" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" name="value" autocomplete="off" id=' + txtval + ' /></div></div>';
                    $('.scrldiv2').append(newTr);
                });
                $("#mobil").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#pin").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $('#uplfile').on('change', function (e) {
                    var img = URL.createObjectURL(e.target.files[0]);
                    $('#upimg').attr('src', img);
                    $.ajax({
                        url: 'SalesForce_Handler.ashx',
                        type: 'POST',
                        data: new FormData($('form')[0]),
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (file) {
                            propic_name = file.name;
                            alert(file.name + "has been uploaded.");
                        }
                    });
                });
                $('#ffstatus').on('click', function () {
                    $('#fstat').show();
                    $('#ffstatus').closest('td').css('display', 'none')
                });
                $('#fstat').on('change', function () {
                    $('#spstat').text($('#fstat :selected').text());
                    $('#fstat').hide();
                    $('#ffstatus').closest('td').css('display', 'table-cell');
                    var actval = $('#fstat').val();
                    if (actval == 2) {
                        $('#resroew').show();
                        //$('.sfempid').hide();
                        //$('.ffJdate').hide();
                    }
                    else if (actval == 1)
                    {
                        $('#resroew').show();
                        //$('.sfempid').show();
                        //$('.ffJdate').show();
                    }
                    else {
                        $('#resroew').hide();
                        //$('.sfempid').hide();
                        //$('.ffJdate').hide();
                    }
                })
                var actval = $('#fstat').val();
                if (actval == 2) {
                    $('#resroew').show();
                    //$('.sfempid').hide();
                    //$('.ffJdate').hide();
                }
                else if (actval == 1) {
                    $('#resroew').show();
                    //$('.sfempid').show();
                    //$('.ffJdate').show();
                }
                else {
                    $('#resroew').hide();
                    //$('.sfempid').hide();
                    //$('.ffJdate').hide();
                }
                $('#fftype').on('click', function () {
                    $('#ddlftyp').show();
                    $('#fftype').closest('td').css('display', 'none')
                });
                $('#division').on('change', function () {
                    var sdiv = $('#division').val() || [];
                    var ssdiv = '';
                    if (sdiv.length > 0) {
                        for (var i = 0; i < sdiv.length; i++) {
                            ssdiv += '\'' + sdiv[i] + '\',';
                        }
                        loaddepts(ssdiv);
                    }
                })
                $('#usrauto').on('change', function () {
                    var $this = $(this);
                    if ($this.is(":checked")) {
                        $('#sfusrname').prop('readonly', true)
                        usratuogenerate = true; getDivisname(1);
                    }
                    else {
                        $('#sfusrname').prop('readonly', false)
                        usratuogenerate = false;
                    }
                })
                $('#states').on('change', function () {
                    if (usratuogenerate == true) {
                        var sfilt = SFStates.filter(function (a) {
                            return a.scode == $('#states').val();
                        });

                        $('#fixUsrn').text(SFDivisname[0].Division_SName + "-");
                        if (autogenusrname == '') {
                            autogenusrname = sfilt[0].sshname + SFDivisname[0].uname;//SFDivisname[0].Division_SName +
                        }
                        else {
                            var des = ($('#sfusrname').attr('des') == undefined) ? '' : $('#sfusrname').attr('des')
                            autogenusrname = sfilt[0].sshname + des + SFDivisname[0].uname;//SFDivisname[0].Division_SName +
                        }
                        $('#sfusrname').attr('stt', sfilt[0].sshname);
                        $('#sfusrname').val(autogenusrname)
                    }
                })
                $('#desg').on('change', function () {
                    if (usratuogenerate == true) {
                        var dfilt = SFDesg.filter(function (a) {
                            return a.dcode == $('#desg').val();
                        });
                        $('#fixUsrn').val(SFDivisname[0].Division_SName + "-");
                        if (autogenusrname == '') {
                            autogenusrname = dfilt[0].dshname + SFDivisname[0].uname; //SFDivisname[0].Division_SName +
                        }
                        else {
                            var stt = ($('#sfusrname').attr('stt') == undefined) ? '' : $('#sfusrname').attr('stt')
                            autogenusrname = stt + dfilt[0].dshname + SFDivisname[0].uname;//SFDivisname[0].Division_SName +
                        }
                        $('#sfusrname').attr('des', dfilt[0].dshname);
                        $('#sfusrname').val(autogenusrname)
                    }
                })
                $('#ddlftyp').on('change', function () {
                    /*if ($('#ddlftyp').val() != 'FF') {
                        $('#sftyptr').hide();
                    }
                    else {
                        $('#sftyptr').show();
                    }*/
                    $('#fftyp').text($('#ddlftyp :selected').text());
                    $('#ddlftyp').hide();
                    $('#fftype').closest('td').css('display', 'table-cell')
                });
                $('#sfftype').on('click', function () {
                    $('#ddlsftyp').show();
                    $('#sfftype').closest('td').css('display', 'none')
                });
                $('#ddlsftyp').on('change', function () {
                    $('#sfftyp').text($('#ddlsftyp :selected').text());
                    $('#ddlsftyp').hide();
                    $('#sfftype').closest('td').css('display', 'table-cell');
                    $x = $('#ddlsftyp').val();
                    filtDesg($x);
                });
                $('#ffJdate').on('click', function () {
                    $('#fjdate').show();
                    $('#ffJdate').closest('td').css('display', 'none');
                });
                $('#fjdate').on('change', function () {
                    var mdt = moment($('#fjdate').val());
                    var txtdt = mdt.format("MMM Do YYYY");
                    $('#ffdttxt').text(txtdt);
                    //$('#fjdate').hide();
                    //$('#ffJdate').closest('td').css('display', 'table-cell');
                });
                $('#rdate').on('click', function () {
                    $('#redate').show();
                    $('#rdate').closest('td').css('display', 'none');
                });
                $('#redate').on('change', function () {
                    var rdt = moment($('#redate').val());
                    var txtrdt = rdt.format("MMM Do YYYY");
                    $('#rffdttxt').text(txtrdt);
                    $('#redate').hide();
                    $('#rdate').closest('td').css('display', 'table-cell');
                });
                $('#ffrpt').on('click', function () {
                    $('#repto').closest('td').show();
                    $('#ffrpt').closest('td').css('display', 'none');
                });
                $('#repto').on('changed.bs.select', function () {
                    $('#ffrpttxt').text($('#repto :selected').text());
                    $('#repto').closest('td').css('display', 'none');
                    $('#ffrpttxt').closest('td').css('display', 'table-cell');
                })

                 $("#sfemail").keyup(function () {
                        var email = $("#sfemail").val();
                        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                        if (!filter.test(email)) {
                            //alert('Please provide a valid email address');                       
                            //$("#EmailHelp").val(msg);
                            $('#sfemail').addClass('is-invalid');
                            $('#EmailHelp').css('display', 'inline')
                            email.focus;
                            return false;
                        }
                        else {
                            $('#sfemail').removeClass('is-invalid');
                            $('#EmailHelp').css('display', 'none')
                            return true;
                        }
                    });
                    
                     $("#sfpwd").keyup(function () {
                    var sfcpassword = $("#sfpwd").val();
                    //let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$/;

                    var filter = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$/;
                    if (!filter.test(sfcpassword)) {
                       
                        $('#sfpwd').addClass('is-invalid');
                        $('#sfpwdHelp').css('display', 'inline')
                        sfcpassword.focus;
                        return false;
                    }
                    else {
                        $('#sfpwd').removeClass('is-invalid');
                        $('#sfpwdHelp').css('display', 'none')
                        return true;
                    }
                });
                
                $('#sfcpwd').keyup(function () {
                    if ($('#sfcpwd').val() != $('#sfpwd').val()) {
                        $('#sfcpwd').addClass('is-invalid');
                        $('#passwordHelp').css('display', 'inline')
                        return false;
                    }
                    else {
                        $('#sfcpwd').removeClass('is-invalid');
                        $('#passwordHelp').css('display', 'none')
                        return true;
                    }
                })
                $('#btnsubmit').on('click', function () {
                    var sfemail = $('#sfemail').val().trim();
                    //if (sfemail == "") {
                    //     alert('Enter the Email');
                    //      $('#sfemail').focus();
                    //      return false;
                    //  }
                    var sfusrname = $('#sfusrname').val();
                    if (sfusrname == '') {
                        alert('Enter the Username');
                        $('#sfusrname').focus();
                        return false;
                    }
                    /*if((window.location.origin).indexOf('fmcg.sanfmcg')>-1){
                        sfusrname=sfusrname;
                    }
                    else{*/
                    sfusrname = $('#fixUsrn').text() + sfusrname;
                    //}
                    var sfpwd = $('#sfpwd').val();
                    if (sfpwd == '') {
                        alert('Enter the Password');
                        $('#sfpwd').focus();
                        return false;
                    }
                    var sfcpwd = $('#sfcpwd').val();
                    if (sfcpwd != sfpwd) {
                        alert("Password doesn't Match");
                        $('#sfcpwd').focus();
                        return false;
                    }
                    var sffirstname = $('#sffirstname').val().trim();
                    if (sffirstname == '') {
                        alert("Enter the First Name");
                        $('#sffirstname').focus();
                        return false;
                    }
                    var sflastname = $('#sflastname').val().trim();
                    var sfempid = $('#sfempid').val().trim();
                    //if (sfempid == '') {
                    //    alert("Enter the Employee ID");
                    //    $('#sfempid').focus();
                    //    return false;
                    //}
                    var sfdob = $('#sfdob').val();
                    var sfdesg = $('#desg').val().trim();
                    if (sfdesg == 0) {
                        alert("Select the Designation");
						$('#desg').focus();
                        return false;
                    }
                    var sfdept = $('#depts').val();
                    // var sfdept = $('#depts').val().trim();
                    if (sfdept == 0) {
                        alert("Select the Department");
						$('#depts').focus();
                        return false;
                    }
                    var sfstate = $('#states').val().trim();
                    if (sfstate == 0) {
                        alert("Select the State");
						$('#states').focus();
                        return false;
                    }
                    var sfhq = $('#hqs').val();
                    var sfhqname = $('#hqs :selected').text();
                    if (sfhq == 0 || sfhqname == '') {
                        alert("Select the HQ");
                        return false;
                    }
                    var sdiv = [];
                    sdiv = $('#division').val() || [];
                    //alert(sdiv);
                    if (sdiv.length == 0) {
                        alert('Select a Division');
                        return false;
                    }
                    var seldiv = '';
                    for (var i = 0; i < sdiv.length; i++) {
                        seldiv += sdiv[i] + ',';
                    }
                    var sfdept = $('#depts').val();
					//var sfdept = $('#depts').val().trim();
                    //if (sfdept == 0) {
                    //    alert("Select the Department");
                    //    return false;
                    //}
                    var sftype = $('#ddlftyp').val();
                    var sflevel = '2';
                    if (sftype == 'FF') {
                        sflevel = $('#ddlsftyp').val();
                    }
                    sflevel = SFDesg.filter(function (a) {
                        return a.dcode == sfdesg;
                    }).map(function (a) { return a.dtype }).toString();
                    var sfterr = $('#routes').val().trim();
                    if (sflevel == '1' && sfterr == "0") {
                        alert("Select the Territory");
                        $('#routes').focus();
                        return false;
                    }
                    var sfmobile = $('#sfmobil').val().trim();
                    if (sfmobile == '') {
                        alert("Enter the Mobile Number");
                        $('#sfmobil').focus();
                        return false;
                    }
                    var sfaddr = $('#sfaddr').val().trim();
                    var sfcity = $('#sfcity').val().trim();
                    var sfarea = $('#sfarea').val().trim();
                    var sfpincode = $('#sfpincode').val();
                    var sfstatus = $('#fstat').val();
                    var sfjdate = $('#fjdate').val();
                    var sfresdate = $('#redate').val();
                    var sfrepto = $('#repto').val();
                    var sfdepot = $('#sfdepot').val();
                    if (sfrepto == 0) {
                        alert("Select Reporting to");
						$('#repto').focus();
                        return false;
                    }

                    if (fstat1 == 0) {
                        if (sfstatus == 0 && sfempid == '') {

                            alert("Please enter the employee id");
                            $('#sfempid').focus();
                            return false;
                        }

                        if (sfstatus == 0 && sfjdate == '') {

                            alert("Please select the joining date");
                            $('#fjdate').focus();
                            return false;
                        }
                    }
                    else {

                        if ((fstat1 == 1 && sfstatus == 0 && sfempid == '')) {

                            alert("Please enter the employee id");
                            $('#sfempid').focus();
                            return false;
                        }

                        if ((fstat1 == 1 && sfstatus == 0 && sfjdate == '')) {

                            alert("Please select the joining date");
                            $('#fjdate').focus();
                            return false;
                        }
                    }

                    var psarr = [];
                    var a = true;
                    $('.scrldiv .row').each(function () {
                        if ($(this).find('input[name="field"]').attr('mandatory') == 'Mandatory') {
                            if ($(this).find('input[name="value"]').val() == '' || $(this).find('input[name="value"]').val() == undefined) {
                                alert('Enter the Value for ' + $(this).find('input[name="field"]').val());
                                $('#exampleModal').modal('toggle');
                                psarr = [];
                                $(this).find('input[name="value"]').focus();
                                a = false;
                            }
                            else {
                                psarr.push({
                                    pfield: $(this).find('input[name="value"]').attr('id'),
                                    pval: $(this).find('input[name="value"]').val()
                                }); a = true;
                            }
                        }
                        else {
                            psarr.push({
                                pfield: $(this).find('input[name="value"]').attr('id'),
                                pval: $(this).find('input[name="value"]').val()
                            }); a = true;
                        }
                        if (a == false) {
                            return false;
                        }
                    });
                    if (a == false) {
                        return false;
                    }
                    var csarr = [];
                    $('.scrldiv2 .row').each(function () {
                        csarr.push({
                            cfield: $(this).find('input[name="field"]').val(),
                            cval: $(this).find('input[name="value"]').val()
                        });
                    });
                    if (propic_name == null) {
                        propic_name = ' ';
                    }
                    let sfdist = $('#ddldistrict').val();
                    //data = { "DivCode": "<%=Session["div_code"]%>", "SfDiv": seldiv, "SFHQN":sfhqname, "SfCode": sfcode, "SF_Name": (sffirstname + sflastname), "SfEmail": sfemail, "SfUsername": sfusrname, "SfPwd": sfpwd, "SfEmpId": sfempid, "SfDOB": sfdob, "SfDesig": sfdesg, "SfDept": sfdept, "SfHQ": sfhq, "SfState": sfstate, "SfTerr": sfterr, "SfMobile": sfmobile, "SfAddr": sfaddr, "SfArea": sfarea, "SfCity": (sfcity + '-' + sfpincode), "SfPincode": sfpincode, "SfStatus": sfstatus,"Sfrdate": sfresdate, "SfTyp": sftype, "SfJDate": sfjdate, "SfReport": sfrepto, "SfPImg": propic_name, "PArr": psarr, "CArr": csarr, "SFlevel": sflevel, "SFDepot": sfdepot, "district": sfdist }

                    if (sflastname == "" || sflastname == null) {

                        data = { "DivCode": "<%=Session["div_code"]%>", "SfDiv": seldiv, "SFHQN": sfhqname, "SfCode": sfcode, "SF_Name": sffirstname, "SfEmail": sfemail, "SfUsername": sfusrname, "SfPwd": sfpwd, "SfEmpId": sfempid, "SfDOB": sfdob, "SfDesig": sfdesg, "SfDept": sfdept, "SfHQ": sfhq, "SfState": sfstate, "SfTerr": sfterr, "SfMobile": sfmobile, "SfAddr": sfaddr, "SfArea": sfarea, "SfCity": (sfcity + '-' + sfpincode), "SfPincode": sfpincode, "SfStatus": sfstatus, "Sfrdate": sfresdate, "SfTyp": sftype, "SfJDate": sfjdate, "SfReport": sfrepto, "SfPImg": propic_name, "PArr": psarr, "CArr": csarr, "SFlevel": sflevel, "SFDepot": sfdepot, "district": sfdist }

                     }
                     else {
                         data = { "DivCode": "<%=Session["div_code"]%>", "SfDiv": seldiv, "SFHQN": sfhqname, "SfCode": sfcode, "SF_Name": sffirstname + ' ' + sflastname, "SfEmail": sfemail, "SfUsername": sfusrname, "SfPwd": sfpwd, "SfEmpId": sfempid, "SfDOB": sfdob, "SfDesig": sfdesg, "SfDept": sfdept, "SfHQ": sfhq, "SfState": sfstate, "SfTerr": sfterr, "SfMobile": sfmobile, "SfAddr": sfaddr, "SfArea": sfarea, "SfCity": (sfcity + '-' + sfpincode), "SfPincode": sfpincode, "SfStatus": sfstatus, "Sfrdate": sfresdate, "SfTyp": sftype, "SfJDate": sfjdate, "SfReport": sfrepto, "SfPImg": propic_name, "PArr": psarr, "CArr": csarr, "SFlevel": sflevel, "SFDepot": sfdepot, "district": sfdist }
                    }

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "SalesForce_New.aspx/saveFieldForce",
                        data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                        dataType: "json",
                        success: function (data) {
                            var successtext = data.d;
                            if (successtext == "Dup") {
                                alert('UserName Already Exists');
                            }
                            else if (successtext == "SalesForce Created") {
                                alert('SalesForce Created');
                                window.location.href = 'SalesForce_List.aspx';
                                clearfields();
                            }
                            else if (successtext == "Update Success") {
                                alert(successtext);
                                window.location.href = 'SalesForce_List.aspx';
                                clearfields();
                            }
                            else if (successtext == "Employee ID Already Exist") {
                                alert(successtext);
                                $('#sfempid').focus();
                            }
                            else {
                                alert(successtext);
                            }
                        },
                        error: function (rs) {
                            alert(rs);
                        }
                    });
                })
            });
        </script>
    </body>
    </html>
</asp:Content>

