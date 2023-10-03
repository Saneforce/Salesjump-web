<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ListofSetup.aspx.cs" Inherits="MasterFiles_ListofSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>List of Setup</h3>
    <div class="col-lg-3">
        <body>
            <div class="overlay" id="loadover" style="display: none;">
                <div id="loader"></div>
            </div>
        </body>
        <div class="card card-body" style="padding: 0px 25px">
            <br />
            <div class="row">
                <label><span>Name</span></label>
                <select id="cmb_frmtyp" style="float: right;">
                    <option value="" disabled selected>--Select--</option>
                    <option value="Company">Company</option>
                    <option value="Employee">Employee</option>
                </select>
                <input type="text" autocomplete="off" class="form-control" id="StName" style="width: 250px;" />
            </div>
            <br />
            <div class="row Group_Type">
                <label><span>Group</span></label>
                <button type="button" id="btn_grppls" class="btn btn-info btn-circle btn-lg" style="float: right; color: #333;">+</button>
                <div class="modal-dialog " id="AddGrpModal" role="document" style="width: 100%; display: none;">
                    <div class="modal-content">
                        <div class="modal-body">
                            <h4>Enter the Group name</h4>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="finame" style="padding-top: 5px;">Name</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <input type="text" name="txt_Grpname" id="txt_Grpname" autocomplete="off" class="col-xs-12" />
                                    </div>

                                </div>
                            </div>
                            <button type="button" class="btn btn-primary col-md-offset-3" id="btn_AddGrp">ADD</button>
                            <button type="button" class="btn btn-danger col-md-offset-1" data-dismiss="modal" id="btncls">CLOSE</button>
                        </div>
                    </div>
                </div>
                <select class="form-control selected" id="cmb_grp" style="width: 250px">
                    <option value="" disabled selected>Nothing  Select</option>
                </select>
                <br />
            </div>

            <div class="row">
                <label><span>Field Name</span></label>
                <input type="text" autocomplete="off" style="width: 250px" class="form-control " id="StfieldName" />
            </div>
            <br />
            <div class="row">
                <label><span>Description</span></label>
                <textarea class="form-control" name="StDesc" id="StDesc" style="width: 250px; resize: none" maxlength="2000"></textarea>
            </div>
            <br />
            <div class="row">
                <label><span>Type of Setup</span></label>
                <select onchange="GetCmb()" class="form-control selected" id="ddSttype" style="width: 250px">
                    <option value="" disabled selected>--Select--</option>
                    <option value="Text">Text</option>
                    <option value="Number">Number</option>
                    <option value="Date">Date</option>
                    <option value="Selection">Selection</option>
                    <option value="Option">Option</option>
                    <option value="CheckBox">CheckBox</option>
                    <option value="Toggle">Toggle</option>
                </select>
                <select onchange="GetCmb()" class="form-control selected" id="ddlttype" style="width: 250px; display: none;">
                    <option value="" selected>--Select--</option>
                    <%--<option value="CheckBox">CheckBox</option> --%>
                    <option value="Toggle">Toggle</option>
                </select>
            </div>
            <br />
            <div class="row">
                <label><span>Value of Setup</span></label>
                <button type="button" id="bttnpls" class="btn btn-info btn-circle btn-lg" style="float: right; color: #333;">+</button>
                &nbsp;&nbsp;
                <table id="CntrlTbl" style="width: 100%;">
                    <tbody></tbody>
                </table>
                <%--<input type="text" id="stval" autocomplete="off" style="width: 250px; display: none;" class="text form-control" />--%>
                <%--<div class="modal"  tabindex="-1" style="display: none; z-index: 10000000; background: transparent;" role="dialog" aria-labelledby="Chklst" aria-hidden="false">--%>
                <div class="modal-dialog" id="Chklst" role="document" aria-labelledby="Chklst" aria-hidden="false" style="width: 100%; display: none; z-index: 10000000; background: transparent;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5>Select the Values</h5>
                            <button type="button" id="btntimesClose" class="close" style="margin-top: -30px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                        </div>
                        <div class="modal-body Chklst">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="finame" style="padding-top: 5px;">ON</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <input type="text" name="txtlist" id="tgl_ON" autocomplete="off" class="col-xs-12" />
                                    </div>

                                </div>
                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="finame" style="padding-top: 5px;">OFF</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <input type="text" name="listVal" id="tgl_OFF" autocomplete="off" class="col-xs-12" />
                                    </div>

                                </div>
                            </div>
                            <div style="text-align: center;">
                                <button type="button" class="btn btn-primary" id="tgl_btn_add">ADD</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--</div>--%>
                <div class="modal-dialog " id="AddSetUpModal" role="document" style="width: 100%; display: none;">
                    <div class="modal-content">
                        <div class="modal-body">
                            <h4>Enter the Values</h4>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="finame" style="padding-top: 5px;">Text</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <input type="text" name="txtlist" id="txtlist" autocomplete="off" class="col-xs-12" />
                                    </div>

                                </div>
                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="finame" style="padding-top: 5px;">Value</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <input type="text" name="listVal" id="listVal" autocomplete="off" class="col-xs-12" />
                                    </div>

                                </div>
                            </div>
                            <button type="button" class="btn btn-primary col-md-offset-3" id="btnadd">ADD</button>
                            <button type="button" class="btn btn-danger col-md-offset-1" data-dismiss="modal" id="btncls">CLOSE</button>
                        </div>
                    </div>
                </div>


                <a href="#" class="list-group-item active List_of_SetUp_Values" style="width: 100%;">List of SetUp-Values</a>
                <div class="list-group" id="ListValues" style="border: 2px solid #ddd; width: 100%; overflow: auto">
                </div>
            </div>

            <div style="text-align: center">
                <button type="button" class="btn btn-primary" id="btnsave">Save</button>
            </div>

        </div>
    </div>
    <div class="col-lg-9">
        <div class="card" style="height: 580px;">
            <div class="card-body table-responsive" style="overflow: auto">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                </div>
                <table class="table table-hover " id="SetUpList">
                    <thead class="text-warning" style="white-space: nowrap;position:sticky;top:0px;">
                        <tr>
                            <th style="text-align: left">SlNo</th>
                            <th style="text-align: left" class="CmpNm">Name</th>
                            <th style="text-align: left; display: none;" class="EmpName">Name</th>
                            <th style="text-align: left; display: none;" class="Field_Chk">Field Check</th>
                            <th style="text-align: left" class="Type">Type</th>
                            <th style="text-align: center" class="Default">Default</th>
                            <th style="text-align: left" class="EDIT">EDIT</th>
                            <th style="text-align: left" class="Delete">DELETE</th>

                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>

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

        .tableFixHead {
            overflow: auto;
            height: 600px;
        }

            .tableFixHead thead th {
                position: sticky;
                top: 0;
                z-index: 1;
            }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            padding: 8px 16px;
        }

        th {
            background: #eee;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var ListOfSetUp = [], ValueList = [], List = [], Grp_JsonValue = [], Grp_settings = [];
        var Listtxt_Val = [], TxtArr = [], FrmTypeArr = [];
        var fieldName = '', Setup_Name = '', Setup_Desc = '', SetUp_Type = '', field_defaultValue = '', SetUpValue = '', Group = '', form_type = '', Setup_Text = '', Setup_List = '', editId = '', delt_Id = '';
        var Inputstr = '';
        var ChngArr = ['CheckBox', 'Toggel Button'];
        searchKeys = "Setup_Name,Value,Setup_Type,Setup_Default";
        $(document).ready(function () {
            //$('#cmb_grp').attr('disabled', true);
            $('#loadover').show();
            LoadData(); cmbload();

            $('#StfieldName').on('keypress', function (e) {
                if (e.which == 32) {
                    console.log('Space Detected');
                    return false;
                }
            });
        });
        $(document).on('click', '.delete', function () {
            delt_Id = this.id;
            if (confirm('Are you sure you want to delete this data from the database?')) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ListofSetup.aspx/DeltListSetup",
                    data: "{'ID':'" + delt_Id + "'}",
                    datatype: "json",
                    success: function (data) {
                        if (data.d == 'Record Deleted Successfully') {
                            var FrmChk = $('#cmb_frmtyp option:selected').val();
                            (FrmChk != '') ? LoadFrmTyp(FrmChk) : LoadData();
                            alert(data.d);
                        }
                    }
                });
            }
        });
        $(document).on('click', '.editEmp', function () {
            editId = this.id; ValueList = [];
            Empfiltrdetails = FrmTypeArr.filter(function (a) {
                return (a.SetUp_ID == editId)
            });
            $('#StName').val(Empfiltrdetails[0].Setup_Name);
            $('#StfieldName').val(Empfiltrdetails[0].Field_Name);
            $('#StDesc').val(Empfiltrdetails[0].Setup_Desc);
            $('#ddlttype option:contains(' + Empfiltrdetails[0].Setup_Type + ')').attr('selected', 'selected');
            var valarr = Empfiltrdetails[0].Setup_Values;
            if (valarr != '') {
                var valuessetup = valarr.slice(0, -1).split(/,/);
                TxtArr = []; Listtxt_Val = [];
                $.each(valuessetup, function (key, val) {
                    Listtxt_Val = val.split('#');
                    TxtArr.push({
                        id: Listtxt_Val[1],
                        value: Listtxt_Val[0]
                    });
                })
                var div = $('#ListValues').html("");
                for (var i = 0; i < TxtArr.length; i++) {
                    ValueList.push({
                        push_value: valuessetup[i]
                    });
                    var str = ('<a href="#" class="list-group-item" id=' + TxtArr[i].id + '>' + TxtArr[i].value + '<input type="radio" ' + ((Empfiltrdetails[0].Value == TxtArr[i].id) ? 'checked=checked' : '') + ' name=btn_rd value=' + TxtArr[i].id + ' sid=' + TxtArr[i].id + ' class="chk pull-right"/><i class="fa fa-trash-o" style="position:absolute;right:2px;line-height: 20px"></a>');
                    $(div).append(str);
                }
            }
            $('#btnsave').text('Update');
        });
        $(document).on('click', '.edit', function () {
            editId = this.id;
            ValueList = [];
            //var edtitems = $(this).closest('tr').find('td:eq(3)').val();
            filldetails = ListOfSetUp.filter(function (a) {
                return (a.SetUp_ID == editId)
            })
            $('#cmb_grp').attr('disabled', false);
            $('#StName').val(filldetails[0].Setup_Name);
            GrpElement = Grp_JsonValue.filter(function (g) {
                return (g.ID == filldetails[0].Group_Setup)
            })
            //EditID = filldetails[0].Setup_ID;
            if (GrpElement.length > 0) $('#cmb_grp option:contains(' + GrpElement[0].Setting_Field_Name + ')').attr('selected', true);
            $('#cmb_frmtyp option:contains(' + filldetails[0].FormType + ')').attr('selected', 'selected');
            $('#StfieldName').val(filldetails[0].Field_Name);
            $('#StDesc').val(filldetails[0].Setup_Desc);
            $('#ddSttype option:contains(' + filldetails[0].Setup_Type + ')').attr('selected', 'selected');
            if (filldetails[0].Setup_Type == 'Selection' || filldetails[0].Setup_Type == 'Option' || filldetails[0].Setup_Type == 'CheckBox' || filldetails[0].Setup_Type == 'Toggle') {
                $('#stval').hide(); $('#ListValues').show(); $('.List_of_SetUp_Values').show();
                $('#bttnpls').show();
                var valarr = filldetails[0].Setup_Values;
                if (valarr != '') {
                    var valuessetup = valarr.slice(0, -1).split(/,/);
                    TxtArr = []; Listtxt_Val = [];
                    $.each(valuessetup, function (key, val) {
                        Listtxt_Val = val.split('#');
                        TxtArr.push({
                            id: Listtxt_Val[1],
                            value: Listtxt_Val[0]
                        });
                    })
                    var div = $('#ListValues').html("");
                    for (var i = 0; i < TxtArr.length; i++) {
                        ValueList.push({
                            push_value: valuessetup[i]
                        });
                        var str = ('<a href="#" class="list-group-item" id=' + TxtArr[i].value+TxtArr[i].id + '>' + TxtArr[i].value + '<input type="radio" ' + ((filldetails[0].Value == TxtArr[i].id) ? 'checked=checked' : '') + ' name=btn_rd value=' + TxtArr[i].id + ' sid=' + TxtArr[i].id + ' class="chk pull-right"/><i class="fa fa-trash-o" style="position:absolute;right:2px;line-height: 20px"></a>');
                        $(div).append(str);
                    }
                }
            }
            else {
                inputType(filldetails[0].Setup_Type);
                $('#stval').show(); $('#ListValues').hide(); $('.List_of_SetUp_Values').hide();
                $('#bttnpls').hide();
                $('#stval').val(filldetails[0].Value);
            }
            $('#btnsave').text('Update');
        });
        $('#bttnpls').on('click', function () {
			if($('#cmb_frmtyp option:selected').text()=='Employee')
				$('#Chklst').modal('toggle');
			else if($('#ddSttype option:selected').text()!='Toggle')
				$('#AddSetUpModal').modal('toggle');
			else
				$('#Chklst').modal('toggle');
            //$('#bttnpls').hide();
        });

        function GetCmb() {
            SetUp_Type = ($('#ddSttype option:selected').text() != '--Select--') ? $('#ddSttype option:selected').text() : $('#ddlttype option:selected').text();

        }

        $('#btnsave').on('click', function () {
            $('#loadover').show();
            fieldName = $('#StfieldName').val(), Setup_Name = $('#StName').val(), Setup_Desc = $('#StDesc').val();
            SetUp_Type = ($('#ddSttype option:selected').text() != '--Select--') ? $('#ddSttype option:selected').text() : $('#ddlttype option:selected').text();
            Group = $('#cmb_grp option:selected').val();
            form_type = $('#cmb_frmtyp option:selected').val();
            if (SetUp_Type == 'Selection' || SetUp_Type == 'Option' || SetUp_Type == 'CheckBox' || SetUp_Type == 'Toggle') {
                field_defaultValue = $('input[name=btn_rd]:checked').length == 0 ? '' : $("input[name=btn_rd]:checked").attr('sid');
            }
            else if (SetUp_Type == 'Date' || SetUp_Type == 'Number' || SetUp_Type == 'Text') {
                field_defaultValue = $('#stval').val();
            }
            if (fieldName == '') {
                alert('Enter the Field Name');
				$('#loadover').hide();
                return false;				
            }
            else if (Setup_Name == '') {
				$('#loadover').hide();
                alert('Enter the Name');
                return false;
            }
            //else if (Setup_Desc == '') {
            //    alert('Enter the Desription');
            //    return false;
            //}
            else if (SetUp_Type == '--Select--' && SetUp_Type != '') {
				$('#loadover').hide();
                alert('Select the SetUp Type');
                return false;
            }
            //else if (ValueList == '' && field_defaultValue == '') {				
            //    alert('Add the Value for SetUp');
            //    return false;
            //}
            else if ((Group == '' || Group == '--Select--') && form_type != 'Employee') {
				$('#loadover').hide();
                alert('Select the Group');
                return false;
            }
            else if (form_type == '') {
				$('#loadover').hide();
                alert('Select Form Type');
                $('#cmb_frmtyp').focus();
                return false;
            }

            for (var i = 0; i < ValueList.length; i++) {
                Setup_List += ValueList[i].push_value + ',';
            }
            ($('#btnsave').text()) == 'Update' ? update() : LoadData();
            Clear();            
            ($('#cmb_frmtyp option:selected').val != '') ? LoadFrmTyp($('#cmb_frmtyp option:selected').val) : LoadData();
			$('#cmb_frmtyp option:contains(--Select--)').attr('selected', 'selected');
        });
        $('#btn_AddGrp').on('click', function () {
            var Grpnm = $('#txt_Grpname').val();
            if (Grpnm == '') {
                alert('Fill the Group Name');
                $('#txt_Grpname').focus();
                return false;
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ListofSetup.aspx/InsertGroup",
                    datatype: "json",
                    data: "{'Name':'" + Grpnm + "'}",
                    success: function (data) {
                        var Grp_settings = data.d;
                        if (Grp_settings == 'Insert Successsfully') {
                            cmbload();
                            alert(data.d); $('#txt_Grpname').val('');
                        }
                    },
                    Error: function (res) {
                        alert(res);
                    }
                });
            }
        });
        $('#tgl_btn_add').on('click', function () {
            chk = []; str = ''; ValueList = [];
            var tglOnValue = $('#tgl_ON').val();
            var tglOffValue = $('#tgl_OFF').val();
            if (tglOnValue == '') { alert('Enter the ON Value'); }

            if (tglOffValue == '') { alert('Enter the OFF Value'); }
            else if (tglOnValue == tglOffValue) {
                alert('ON and OFF Values Can\'t be Same!!!');
            }
            else if (tglOnValue != '' && tglOffValue != '') {
                chk.push({
                    ON: tglOnValue,
                    OFF: tglOffValue
                });
                var fltered_nm = ValueList.filter(function (u) {
                    return (u.push_value == 'ON' + '#' + tglOnValue + ',OFF' + '#' + tglOffValue)
                });

                if (fltered_nm.length > 0) {
                    alert('The Value Already added to List!!!');
                    return false;
                }
                else {
                    ValueList.push({
                        push_value: 'ON' + '#' + tglOnValue + ',OFF' + '#' + tglOffValue
                    });
                    var div = $('#ListValues');
                    for (var i = 0; i < chk.length; i++) {
                        if (tglOnValue == chk[i].ON && tglOffValue == chk[i].OFF) {
                            str += ('<a href="#" class="list-group-item" id=' + 'ON'+tglOnValue + '>ON<input type="radio" name=btn_rd sid=' + tglOnValue + ' class="chk pull-right"/>\
                                <i class="fa fa-trash-o" style="position:absolute;right:2px;line-height: 20px"></i></a>\
                            <a href="#" class="list-group-item" id=' + 'OFF'+tglOffValue + '>OFF<input type="radio" name=btn_rd sid=' + tglOffValue + ' class="chk pull-right"/>\
                                <i class="fa fa-trash-o" style="position:absolute;right:2px;line-height: 20px"></i></a>');
                        }
                    }
                    $(div).append(str); $('#tgl_ON').val(''); $('#tgl_OFF').val('');
                }
            }
        });
        $('#btnadd').on('click', function () {
            var val = $('#listVal').val(); var txtval = $('#txtlist').val();
            if (val == '') { alert('Fill the Value'); $('#listVal').focus(); }
            else if (txtval == '') { alert('Fill the value'); $('#txtlist').focus(); }//txtlist
            else {

                var fltered_nm = ValueList.filter(function (u) {
                    return (u.push_value == txtval + '#' + val)
                });

                if (fltered_nm.length > 0) {
                    alert('The Value Already added to List!!!');
                    return false;
                }
                else {
                    ValueList.push({
                        push_value: txtval + '#' + val
                    });
                    var div = $('#ListValues');
                    var str = ('<a href="#" class="list-group-item" id=' + txtval+val + '>' + txtval + '<input type="radio" name=btn_rd sid=' + val + ' class="chk pull-right"/>\
                                <i class="fa fa-trash-o" style="position:absolute;right:2px;line-height: 20px"></a>');
                    $(div).append(str); $('#listVal').val(''); $('#txtlist').val('');
                }
            }

        });
        $(document).on('click', '#ListValues>a>i', function () {
            var GetID = $(this).closest('a').attr('id');
            $(this).closest('a').remove();
            $('.' + GetID).closest('a').remove();
            $('#' + GetID).css('display', 'block');
            for (var m = 0; m < ValueList.length; m++) {
                var values = ValueList[m].push_value.split('#');
                if (values[0]+values[1] == GetID) {
                    const val = ValueList[m].push_value.indexOf(ValueList[m].push_value)
                    if (val > -1) {
                        ValueList.splice(val, 1);
                    }
                }
            }
        });
        $('#btncls').on('click', function () {
            $('#bttnpls').show();
            $('#bttnpls').attr('disabled', false);
            $('#AddSetUpModal').hide();
        });
        $('#btn_grppls').on('click', function () {
            $('#AddGrpModal').modal('toggle');
            //$('#btn_grppls').attr('disabled', true);
            //cmbload();
            $('#cmb_grp').attr('disabled', false);
        });
        $("#tSearchOrd").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#SetUpList tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
        $('#ddSttype').on('change', function () {
            var ddsselval = $('#ddSttype').val(); /* CntrlTbl*/
            if (ddsselval == 'Text' || ddsselval == 'Number' || ddsselval == 'Date') {
                inputType(ddsselval);
                $('#stval').show(); $('#bttnpls').hide(); $('#ListValues').hide(); $('.List_of_SetUp_Values').hide();
            }
            else if (ddsselval == 'Toggle') {
                $('#Chklst').modal('toggle');
                $('#bttnpls').show(); $('#stval').hide(); $('#ListValues').show(); $('.List_of_SetUp_Values').show();
            }
            else {
                $('#AddSetUpModal').modal('toggle'); $('#bttnpls').show(); $('#stval').hide(); $('#ListValues').show(); $('.List_of_SetUp_Values').show();
            }
        });
        $('#cmb_frmtyp').on('change', function () {

            var FrmTyp = $('#cmb_frmtyp option:selected').text();
            if (FrmTyp == 'Employee') {
                $("#ddSttype").hide();
                $("#ddlttype").show();
                $('.Group_Type').hide(); $("#SetUpList TBODY").html("");
                $('.Type').hide(); $('.Default').hide();
                $('.Field_Chk').show(); $('.EmpName').show(); $('.CmpNm').hide();
                LoadFrmTyp(FrmTyp);
            }
            else if (FrmTyp == 'Company') {
                $("#ddSttype").show();
                $("#ddlttype").hide();
                $('.Group_Type').show();
                $('.Type').show(); $('.Default').show();
                $('.Field_Chk').hide(); $('.EmpName').hide(); $('.CmpNm').show();
                LoadFrmTyp(FrmTyp);
            }
            Clear();
        });
        function FrmHeaderChk(ID) {
            var Chk = $('#' + ID).attr("checked") ? 1 : 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ListofSetup.aspx/EmpHeaderChK",
                data: "{'Chk':'" + Chk + "','ID':'" + ID + "'}",
                datatype: "json",
                success: function (data) {

                },
                Error: function (Result) {
                    alert(Result);
                }
            });
        }

        function LoadFrmTyp(FrmChk) {
			$("#SetUpList TBODY").html("");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ListofSetup.aspx/GetEmp_Setting",
                data: "{'FrmTyp':'" + FrmChk + "'}",
                datatype: "json",
                success: function (data) {
                    FrmTypeArr = JSON.parse(data.d) || [];
                    if (FrmTypeArr.length > 0) {
                        if (FrmChk == 'Employee') {
                            slno = 0;
                            for (var $i = 0; $i < FrmTypeArr.length; $i++) {
                                tr = $("<tr></tr>");
                                slno += 1;
                                $(tr).html("<td>" + slno + "</td><td style='height: 46px;' class='EmpName'>" + FrmTypeArr[$i].Setup_Name + "<div colspan='3' style='position:absolute;font-size:11px;color:#878585;'>" + FrmTypeArr[$i].Setup_Desc + "</div></td>\
                                        <td class='Field_Chk'><input type='checkbox' id='" + FrmTypeArr[$i].SetUp_ID + "' name='btnchk_" + $i + "' \onclick=\(FrmHeaderChk('" + FrmTypeArr[$i].SetUp_ID + "')\)\ class='col-lg-3 Field_Chk' " + ((FrmTypeArr[$i].EmpChk == 1) ? 'checked=checked' : '') + " />\
                                        </td><td id='" + FrmTypeArr[$i].SetUp_ID + "'  class='editEmp'><a style='cursor:pointer'>Edit</a></td><td id='" + FrmTypeArr[$i].SetUp_ID + "'  class='delete'><a><i style='cursor:pointer' class='fa fa-trash-o'></i></a></td>");
                                $("#SetUpList TBODY").append(tr);
                            }
                        }
                        else {
                            ReloadTable(FrmTypeArr);
                        }
                    }
                }
            });
        }

        function inputType(inputtype) {
            $('#CntrlTbl').html("");
            Inputstr = '<td><input type=' + inputtype + ' id="stval" autocomplete="off" style="width: 250px; display: none;" class="text form-control" /></td>';
            $('#CntrlTbl').append(Inputstr);
        }
        function cmbload() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ListofSetup.aspx/Get_grp_data",
                datatype: "json",
                success: function (data) {
                    Grp_JsonValue = JSON.parse(data.d) || []
                    if (Grp_JsonValue.length > 0) {
                        var str = '';
                        $('#cmb_grp').html("");
                        str = "<option selected>--Select--</option>";
                        for (var $i = 0; $i < Grp_JsonValue.length; $i++) {
                            str += "<option value=" + Grp_JsonValue[$i].ID + ">" + Grp_JsonValue[$i].Setting_Field_Name + "</option>"
                        }
                        $('#cmb_grp').append(str);
                    }
                    else {
                        var str = "<option>No Data Found</option>"
                        $('#cmb_grp').append(str);
                    }
                },
                Error: function (res) {
                    alert(res);
                }
            })
        }
        function update() {
			var FrmChk = $('#cmb_frmtyp option:selected').val();            
            if (FrmChk == 'Employee') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ListofSetup.aspx/Update_Emp_Setup",
                    data: "{'Field_Name':'" + fieldName + "','Setup_Name':'" + Setup_Name + "','Setup_Desc':'" + Setup_Desc + "','Setup_Type':'" + SetUp_Type + "','field_Value':'" + Setup_List + "',\
                        'Value':'" + field_defaultValue + "','FormType':'" + form_type + "','EditID':'" + editId + "'}",
                    datatype: "json",
                    success: function (data) {
                        if (data.d == 'Update Successfully') {
							(FrmChk != '') ? LoadFrmTyp(FrmChk) : LoadData(); 
                            $('#btnsave').text('Save'); 
                        }
                        alert(data.d);
                    },
                    Error: function (result) {
                        alert(result);
                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ListofSetup.aspx/UpdateListSetup",
                    data: "{'Field_Name':'" + fieldName + "','Setup_Name':'" + Setup_Name + "','Setup_Desc':'" + Setup_Desc + "','Setup_Type':'" + SetUp_Type + "','field_Value':'" + Setup_List + "',\
                        'Value':'" + field_defaultValue + "','Group_Setup':'" + Group + "','FormType':'" + form_type + "','EditID':'" + editId + "'}",
                    datatype: "json",
                    success: function (data) {
                        if (data.d == 'Update Successfully') {
                            $('#btnsave').text('Save');
							LoadData();
                        }
                        alert(data.d);
                    }
                });
            }
            $('#loadover').hide();
        }
        function LoadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ListofSetup.aspx/SaveListofSetup",
                data: "{'Field_Name':'" + fieldName + "','Setup_Name':'" + Setup_Name + "','Setup_Desc':'" + Setup_Desc + "','Setup_Type':'" + SetUp_Type + "'\
                        ,'field_Value':'" + Setup_List + "','SetUpValue':'" + field_defaultValue + "','Group_Setup':'" + Group + "','FormType':'" + form_type + "'}",
                datatype: "json",
                success: function (data) {
                    ListOfSetUp = JSON.parse(data.d) || []
                    List = ListOfSetUp; ReloadTable(ListOfSetUp);
                    var chk = ListOfSetUp[0].Chk;
                    if (chk != "Y") {
                        alert(ListOfSetUp[0].Chk);
                    }
                },
                Error: function (result) {
                    alert(result);
                }
            })
            $('#loadover').hide();
        }
        function Clear() {
            fieldName = '', Setup_Name = '', Setup_Desc = '', SetUp_Type = '', field_defaultValue = '', SetUpValue = '', Group = '', form_type = '', Setup_Text = '', Setup_List = '';
            $('#StfieldName').val(''), $('#StName').val(''), $('#StDesc').val(''), $('#ddSttype option:contains(--Select--)').attr('selected', 'selected');
			$('#ddlttype option:contains(--Select--)').attr('selected', 'selected');
            $('#cmb_grp option:contains(--Select--)').attr('selected', 'selected');
            $('#ListValues').html("");
            for (var i = 0; i < TxtArr.length; i++) {
                $('#' + TxtArr[i].id).remove();
            }
            TxtArr = [];
            for (var i = 0; i < ValueList.length; i++) {
                $('#' + ValueList[i].id).remove();
            }
            ValueList = [];
        }
        function ReloadTable(SetUpList) {
            $('#loadover').show();
            $("#SetUpList TBODY").html("");
            slno = 0;
            for ($i = 0; $i < SetUpList.length; $i++) {
                if ($i < SetUpList.length) {
                    tr = $("<tr></tr>");
                    if (SetUpList[$i].FormType == 'Company') {
                        slno += 1;
                        $(tr).html("<td>" + slno + "</td><td style='height: 46px;'>" + SetUpList[$i].Setup_Name + "<div colspan='3' style='position:absolute;font-size:11px;color:#878585;'>\
                                " + SetUpList[$i].Setup_Desc + "</div></td><td>" + SetUpList[$i].Setup_Type + "</td><td style='text-align: center;'>" + SetUpList[$i].Value + "</td>\
                                <td id='" + SetUpList[$i].SetUp_ID + "'  class='edit'><a style='cursor:pointer'>Edit</a></td><td id='" + SetUpList[$i].SetUp_ID + "' class='delete'><a><i style='cursor:pointer' class='fa fa-trash-o'></i></a></td>");
                    }
                    $("#SetUpList TBODY").append(tr);
                }
            }
            $('#loadover').hide();
        }
    </script>
</asp:Content>

