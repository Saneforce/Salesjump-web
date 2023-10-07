<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="TravelMod.aspx.cs" Inherits="MasterFiles_TravelMod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        .tg-list-item {
            margin: 0 2em;
        }


        .tgl {
            display: none;
        }

            .tgl, .tgl:after, .tgl:before, .tgl *, .tgl *:after, .tgl *:before, .tgl + .tgl-btn {
                box-sizing: border-box;
            }

                .tgl::-moz-selection, .tgl:after::-moz-selection, .tgl:before::-moz-selection, .tgl *::-moz-selection, .tgl *:after::-moz-selection, .tgl *:before::-moz-selection, .tgl + .tgl-btn::-moz-selection {
                    background: none;
                }

                .tgl::selection, .tgl:after::selection, .tgl:before::selection, .tgl *::selection, .tgl *:after::selection, .tgl *:before::selection, .tgl + .tgl-btn::selection {
                    background: none;
                }

                .tgl + .tgl-btn {
                    outline: 0;
                    display: block;
                    width: 4em;
                    height: 2em;
                    position: relative;
                    cursor: pointer;
                    -webkit-user-select: none;
                    -moz-user-select: none;
                    -ms-user-select: none;
                    user-select: none;
                }


        .tgl-skewed + .tgl-btn {
            overflow: hidden;
            -webkit-transform: skew(-10deg);
            transform: skew(-10deg);
            -webkit-backface-visibility: hidden;
            backface-visibility: hidden;
            -webkit-transition: all .2s ease;
            transition: all .2s ease;
            font-family: sans-serif;
            background: #888;
        }

            .tgl-skewed + .tgl-btn:after, .tgl-skewed + .tgl-btn:before {
                -webkit-transform: skew(10deg);
                transform: skew(10deg);
                display: inline-block;
                -webkit-transition: all .2s ease;
                transition: all .2s ease;
                width: 100%;
                text-align: center;
                position: absolute;
                line-height: 2em;
                font-weight: bold;
                color: #fff;
                text-shadow: 0 1px 0 rgba(0, 0, 0, 0.4);
            }


        .tgl-skewed:checked + .tgl-btn {
            background: #86d993;
        }

            .tgl-skewed:checked + .tgl-btn:before {
                left: -100%;
            }

            .tgl-skewed:checked + .tgl-btn:after {
                left: 0;
            }

            .tgl-skewed:checked + .tgl-btn:active:after {
                left: 10%;
            }

        .tgl-skewed + .tgl-btn:after {
            left: 100%;
            content: attr(data-tg-on);
        }

        .tgl-skewed + .tgl-btn:before {
            left: 0;
            content: attr(data-tg-off);
        }

        .tgl-skewed + .tgl-btn:active {
            background: #888;
        }

            .tgl-skewed + .tgl-btn:active:before {
                left: -10%;
            }

        .modal-body {
            position: relative;
            padding: 20px;
            overflow: auto;
        }

        .container {
            width: -moz-fit-content;
            width: fit-content;
            /*border: 2px solid #ccc;*/
            padding: 10px;
            width: 950px;
            height: 450px;
            margin-left: 464px;
            margin-top: -345px;
            overflow: auto;
        }

        .item {
            width: -moz-fit-content;
            width: fit-content;
            background-color: #ffffff;
            padding: 5px;
            margin-bottom: 1em;
            max-height: 350px;
            margin-top: 0px;
            margin-left: 0px;
            overflow: auto;
            min-height: 250px;
            /*max-width:800px;*/
        }
    </style>
    <form runat="server" id="frm1">
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png" Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            <div class="col-lg-12 sub-header">
                Travel Mode Master                
                <span style="float: right">
                    <a href="#" class="btn btn-primary btn-update" id="btnAdd">Add New</a></span>
                <div style="float: right; padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='Active' class="active">Active</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                    <label style="float: right">
                        Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
                            <th style="text-align: left">SlNO</th>
                            <th style="text-align: left;">MOT</th>
                            <th style="text-align: left;">Meter Reading</th>
                            <th style="text-align: left;">Driver</th>
                            <th style="text-align: left;">Allowance</th>
                            <th style="text-align: left;">Edit</th>
                            <th style="text-align: left;">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="row" style="padding: 5px 0px">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="AddFieldModal" tabindex="-1" style="z-index: 1000000; background: transparent; overflow: auto;" role="dialog" aria-labelledby="AddFieldModal" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 90%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>Add Travel Mode</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xs-4" style="padding-left: 30px;">
                                <table style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td rowspan="1">Mode Name
                                            </td>
                                            <td rowspan="1">
                                                <input type="text" name="mname" id="mname" autocomplete="off" />
                                            </td>
                                            <%--<td rowspan="5">
                                        <div id="stdiv" style="overflow: auto; height: 350px; width: 1100px; border: 1px solid; display: none;">
                                        </div>
                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td rowspan="1">Ref Code</td>
                                            <td rowspan="1">
                                                <input type="text" id="glc" autocomplete="off" /></td>
                                        </tr>
                                        <tr>
                                            <td rowspan="1">Meter Reading</td>
                                            <td rowspan="1">
                                                <input class='tgl tgl-skewed' id="meter" type='checkbox' name="meter" value="M" />
                                                <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="meter"></label>
                                            </td>

                                        </tr>
                                        <%-- <tr>
                                    <td rowspan="1">Effective Date
                                    </td>
                                    <td rowspan="1">
                                        <input type="date" id="effdate" />
                                    </td>
                                </tr>--%>
                                        <tr id="drivedi" style="display: none">
                                            <td rowspan="1">Driver</td>
                                            <td rowspan="1">
                                                <input class='tgl tgl-skewed' id="driver" type='checkbox' name="driver" value="D" />
                                                <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="driver"></label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td rowspan="1">Exception Entry</td>
                                            <td rowspan="1">
                                                <input class='tgl tgl-skewed' id="excep" type='checkbox' name="excpent" value="E" />
                                                <label class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="excep"></label>
                                            </td>
                                        </tr>
                                        <tr class="hidden">
                                            <td rowspan="1">Grade</td>
                                            <td colspan="3">
                                                <div class="col-sm-9" style="display: inline-flex;">
                                                    <select id="ddlgrade" multiple data-actions-box="true"></select>
                                                    <div style="display: inline-flex; padding-left: 3rem;">
                                                        <label>For All</label><input class='tgl tgl-skewed' id="forall" type="checkbox" name="all" value="A" />
                                                        <label style="margin-left: 3rem;" class='tgl-btn' data-tg-off='NO' data-tg-on='YES' for="forall"></label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label style="padding-top: 5px; padding-left: 15px; text-align: center; white-space: nowrap;">Allowance Eligible</label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class="col-xs-12 col-sm-20" style="border: 1px solid; width: 300px; margin-left: 31px;">
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex; margin-top: 10px;">
                                                        <input id="HQ" type='checkbox' name="allowance" value="HQ" /><label style="padding-left: 7px; white-space: nowrap; margin: 0px;" for="HQ">HQ</label>
                                                    </div>
                                                    <br />
                                                    <div class="col-xs-12 col-sm-8" style="display: inline-flex; margin-top: 10px;">
                                                        <input id="Ex-HQ" type='checkbox' name="allowance" value="EX" /><label style="padding-left: 7px; white-space: nowrap; margin: 0px;" for="Ex-HQ">EX</label><br />
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex; margin-top: 10px;">
                                                        <input id="OS" type='checkbox' name="allowance" value="OS" /><label style="padding-left: 7px; white-space: nowrap; margin: 0px;" for="Out">Out station</label><br />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="container col-xs-8 table-responsive">
                                <div class="col-xs-7 card table-responsive item" id="stdiv">
                                </div>
                                <div class="col-xs-7 item" id="bucharge" style="margin-top: 133px;">
                                    <label>Maximum amount</label>
                                    <input type="text" id="chargekm" autocomplete="off" />
                                </div>

                            </div>
                        </div>

                        <div class="hiddenSlNo" style="display: none;">
                            <div class="col-xs-12 col-sm-8">
                                <input type="text" name="echange" id="Echange" autocomplete="off" class="col-xs-12" />
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" onclick="closemodel()" data-dismiss="modal">Close</button>
                        <button type="button" style="background-color: #1a73e8;" class="btn btn-primary" id="svfields">Save</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="AddFieldsModal" tabindex="-1" style="z-index: 1000000; background: transparent; overflow: auto;" role="dialog" aria-labelledby="AddFieldsModal" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 70%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>Add Travel Mode</h4>
                    </div>
                    <div class="modal-body" style="overflow: auto;">
                        <div class="row" style="margin-bottom: 1rem!important;">
                        </div>
                        <div class="row">
                            <div class="col-sm-12 col-xs-12">
                                <div class="col-xs-12 col-sm-3">
                                    <input /><label style="padding-left: 7px; white-space: nowrap;" for="meter">Meter Reading</label>
                                </div>
                                <div class="Fuel col-xs-12 col-sm-5" style="display: none;">
                                    <div class="col-sm-6">
                                        <label for="fcharge" style="padding-left: 50px;">Fuel Charges</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <input type="number" name="fcharge" id="fcharge" autocomplete="off" class="form-control" />
                                    </div>
                                </div>
                                <%--<div class="Fuel col-sm-4 col-xs-12" style="display: none;">
                                    <label for="effdate">Effective Date</label>
                                    <input type="date" />
                                </div>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                    <input /><label style="padding-left: 7px; white-space: nowrap;" for="driver">Driver</label>
                                </div>
                            </div>
                        </div>
                        <div class="allow">
                            <div class="allow-body table-responsive" style="overflow-x: auto;">
                                <div class="col-xs-12 col-sm-10">
                                    <label for="mname" style="padding-top: 5px; padding-left: 15px; text-align: center">Allowance Eligible</label>
                                </div>
                                <div class="col-xs-12 col-sm-20" style="border: 1px solid; width: 300px; margin-left: 31px;">
                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                        <input id="HQ" type='checkbox' name="allowance" value="HQ" /><label style="padding-left: 7px; white-space: nowrap;" for="HQ">HQ</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8" style="display: inline-flex;">
                                        <input id="Ex-HQ" type='checkbox' name="allowance" value="Ex" /><label style="padding-left: 7px; white-space: nowrap;" for="Ex-HQ">EX</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-6" style="display: inline-flex;">
                                        <input id="OS" type='checkbox' name="allowance" value="OS" /><label style="padding-left: 7px; white-space: nowrap;" for="Out">Out station</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="button" style="background-color: #1a73e8;" class="btn btn-primary">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var mode = '', charge = '', meter = '', driver = '', allowance = '', data = '', echange = '', glcode = '', filtrkey = 'Active';
        var Orders = [], FrmsandFlds = [], FldArr = [], sfstates = [], pgNo = 1, PgRecords = 10;
        var cusxml = ''; let grdMaster = []; var stpricing = []; var sfdesigns = [];
        var searchKeys = "MOT,FuelAmt,StEndNeed,Alw_Eligibilty";
        optStatus = "<li><a href='#' value='0'>Active</a><a href='#' value='1'>Deactivate</a></li>"
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );
        $(document).ready(function () {
            onload();
		 $(".ffcharge").live("keypress", function (e) {
                    var num = e.keyCode;
                    if ((e.keyCode < 46 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                        return false;
                    }
                });
        });

        function onload() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TravelMod.aspx/getTravelModeFields",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    FrmsandFlds = JSON.parse(data.d) || [];
                    Orders = FrmsandFlds;
                    ReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getGradeMaster() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TravelMod.aspx/Getgrade",
                dataType: "json",
                success: function (data) {
                    grdMaster = JSON.parse(data.d);
                    $('#ddlgrade').empty();
                    let sfGrd = $("#ddlgrade");
                    for (var i = 0; i < grdMaster.length; i++) {
                        sfGrd.append($('<option value="' + grdMaster[i].Grade_ID + '">' + grdMaster[i].Group_Name + '</option>'));
                    }
                    $('#ddlgrade').selectpicker({
                        liveSearch: true
                    });
                    $('#ddlgrade').selectpicker('refresh');
                },
                error: function (result) {
                }
            });
        }

        function loadFields(FArr) {
            if ((FArr[0].StEndNeed) == 'Yes') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "TravelMod.aspx/getStatewisePrice",
                    data: "{'slno':'" + FArr[0].Sl_No + "','divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var stpricing = JSON.parse(data.d);
                        if (stpricing.length > 0) {
                            $('#effdate').val((stpricing[0].effdt).split('T')[0]);
                            for ($i = 0; $i < sfstates.length; $i++) {
                                var filtp = stpricing.filter(function (a) {
                                    return a.StateCode == sfstates[$i].scode;
                                });
                                for (i = 0; i < filtp.length; i++) {
                                    for (j = 0; j < sfdesigns.length; j++) {
                                        if (sfdesigns[j].Designation_Code == filtp[i].Design_code) {
                                            $('.' + filtp[i].StateCode).find('.' + filtp[i].Design_code).val(filtp[i].Fuel_Charge)
                                        }
                                    }
                                    //$('#stdiv').find('#st' + stpricing[$i].StateCode).find('.fcharge').val(stpricing[$i].Fuel_Charge)
                                    //$('#stdiv').find('#st' + stpricing[$i].StateCode).find('.nffcharge').val(stpricing[$i].NF_Fuel_Charge)
                                }
                            }
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            $('#mname').val(FArr[0].MOT);
            $('#Echange').val(FArr[0].Sl_No);
            $('#meter').prop('checked', ((FArr[0].StEndNeed) == 'Yes' ? true : false));
            $('#excep').prop('checked', ((FArr[0].Deviation) == 'Yes' ? true : false));

            $('#glc').val(FArr[0].GLCode);
            let sgrades = (FArr[0].GradeID).split(',');
            for ($j = 0; $j < sgrades.length; $j++) {
                $('#ddlgrade option[value=' + sgrades[$j] + ']').attr('selected', true);
            }
            $('#ddlgrade').selectpicker('refresh');
            if (FArr[0].GradeID == "") { $('#forall').prop("checked", true); };
            if ($('#meter').is(":checked") == true) {
                $('#stdiv').show();
                $('#drivedi').show();
                $('#driver').prop('checked', ((FArr[0].DriverNeed) == 'Yes' ? true : false));
                $('#bucharge').hide();
            }
            else {
                $('#bucharge').show();
                $('#drivedi').hide();
                $('#chargekm').val(FArr[0].MaxKm);
            }

            var multiopt = (FArr[0].Alw_Eligibilty).split(',');
            for ($j = 0; $j < multiopt.length; $j++) {
                if (multiopt[$j] == 'HQ')
                    $('#HQ').prop('checked', true);
                if (multiopt[$j] == 'EX')
                    $('#Ex-HQ').prop('checked', true);
                if (multiopt[$j] == 'OS')
                    $('#OS').prop('checked', true);
            }
            //var checkRadio = ($("#meter").is(":checked"))
            //if (checkRadio == false) {
            //   $('#bucharge').show()
            //} else {
            //   ($('#bucharge').hide());
            //}
        }

        function saveForms() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TravelMod.aspx/saveTravelMode",
                data: "{'data':'" + JSON.stringify(data) + "'}",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    alert(result);
                    clearfields();
                    $('#AddFieldModal').modal('toggle');
                    onload();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getdesign() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TravelMod.aspx/getdesign",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfdesigns = JSON.parse(data.d);
                    $('#stdiv').empty();
                    str = '';
                    for (var i = 0; i < sfdesigns.length; i++) {
                        //str += '<th ><label style="margin-left: 12px" class="heaser" value="' + sfdesigns[i].Designation_Code + '">' + sfdesigns[i].Designation_Short_Name + '</label><br><input style="width:50px" class="' + sfdesigns[i].Designation_Short_Name + 'header " onkeyup=getValue("' + sfdesigns[i].Designation_Short_Name + '") ></input></th>';
				str += '<th ><label style="margin-left: 12px" class="heaser" value="' + sfdesigns[i].Designation_Code + '">' + sfdesigns[i].Designation_Short_Name + '</label><br><input style="width:50px" class="' + sfdesigns[i].Designation_Code + 'header " onkeyup=getValue("' + sfdesigns[i].Designation_Code + '") ></input></th>';
                    }
                    $('#stdiv').append('<div style="margin-top:5px;margin-left:100px"><label for="effdate">Effective Date</label><input type="date"  id="effdate" style="margin-left: 25px;"/></div><table id="modtable" style="width:100%;border: 1px solid #80808000;"><thead style="border-bottom: 2px solid #333;position: sticky;top: -6px;background-color: #ddd;z-index: 5;"><tr  class="headernam"><th style="text-align:center"><label class="heaser" value="0" >State</label></th>' + str + '</tr></thead><tbody></tbody></table>');
                },
            });

        }
        function getValue(cls) {
            $('.' + cls).val($('.' + cls + 'header').val());
        }

        function getStates() {
            getdesign();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TravelMod.aspx/getStates",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfstates = JSON.parse(data.d);
                    for (var i = 0; i < sfstates.length; i++) {
                        if (sfstates[i].scode == 28) {
                            sfstates[i].sname = sfstates[i].sname + ' (Kolkata)';
                        }
                        else if (sfstates[i].scode == 14) {
                            sfstates[i].sname = sfstates[i].sname + ' (Indore)';
                        }
                        else if (sfstates[i].scode == 5) {
                            sfstates[i].sname = sfstates[i].sname + ' (Raipur)';
                        }

                        strl = '';
                        for (var j = 0; j < sfdesigns.length; j++) {
                            strl += '<td style="width: 75px;" ><input min ="0" style="width:50px" id="' + sfstates[i].scode + sfdesigns[j].Designation_Short_Name + '"  class="ffcharge form-control ' + sfdesigns[j].Designation_Short_Name + ' ' + sfdesigns[j].Designation_Code + '"  /></td>';
                        }
                        $('#modtable tbody').append('<tr class="states ' + sfstates[i].scode + '"><div id="st' + sfstates[i].scode + '" class="col-sm-12 states" scode="' + sfstates[i].scode + '" style="margin-top:1rem !important"><td style="text-align:center" scode="' + sfstates[i].scode + '"><label style="margin-right:1rem  !important" class="ffcharge" scode="' + sfstates[i].scode + '">' + sfstates[i].sname + '</label><input type="hidden" /></td>' + strl + '</div></tr>')

                    }
                },
                error: function (result) {
                }
            });
        }
        function closemodel() {
            clearfields();
        }
        function clearfields() {
            $('#mname').val('');
            $('#chargekm').val('');
            $('#fcharge').val('');
            $('.deactfrmfld').val('');
            $('#meter').prop('checked', false);
            $('#driver').prop('checked', false);
            $('#HQ').prop('checked', false);
            $('#Ex-HQ').prop('checked', false);
            $('#OS').prop('checked', false);
            allowance = '';
            $('.Fuel').hide();
            $('#excep').prop('checked', false);
            $('#forall').prop('checked', false);
            $('#glc').val('');
            $('#effdate').val('');
            $('#Echange').val('');
        }

        $("#meter").on("change", function () {
            ($('#meter').is(":checked") == true) ? ($('#stdiv').show()) : ($('#stdiv').hide());
            ($('#meter').is(":checked") == true) ? ($('#drivedi').show()) : ($('#drivedi').hide());
            ($('#meter').is(":checked") == false) ? ($('#bucharge').show()) : ($('#bucharge').hide());
        })

        $(document).on('click', '.frmedit', function () {
            clearfields();
            hefild = $(this).closest('tr').attr('id');
            getdesign();
            getStates();
            getGradeMaster();
            $('#stdiv').hide();
            $('#AddFieldModal').modal('toggle');

            loadFields(FrmsandFlds.filter(function (a) {
                return a.Sl_No == hefild;
            }));



        });

        $('#btnAdd').on('click', function () {
            clearfields();
            getStates();
            getStates();
            getGradeMaster();
            $('#AddFieldModal').modal('toggle');
            $('#stdiv').hide();
            $('#drivedi').hide();
            $('#bucharge').show()
        });




        $("#svfields").on('click', function () {
            mode = $('#mname').val();
            echange = $('#Echange').val();
            glcode = $('#glc').val();
            driver = ($('#driver').is(":checked") == true) ? 1 : 0;

            if (mode == '') {
                alert("Please enter Mode Name");
                return false;
            }
            //cusxml = '<ROOT>';
            //if ($('#meter').is(":checked")) {
            //    $('.states').each(function () {
            //        var stc = $(this).attr('scode');
            //        var fstnv = ($(this).find('.fcharge').val() == '') ? 0 : parseFloat($(this).find('.fcharge').val());
            //        var nfstnv = ($(this).find('.nffcharge').val() == '') ? 0 : parseFloat($(this).find('.nffcharge').val());
            //        cusxml += '<ASSD stcode="' + stc + '" fstn="' + fstnv + '" nfstn="' + nfstnv + '" />'
            //    });
            //}
            //cusxml += '</ROOT>';
            meter = ($('#meter').is(":checked") == true) ? 1 : 0;
            var effdate = $('#effdate').val();

            if (effdate == '' && meter == 1) {
                alert('Select the Effective Date');
                return false;
            }
            if ($('#meter').is(":checked")) {
                var fielarr = [];
                $('#modtable').find('tbody').find('tr').each(function (key, val) {
                    var snme = $(this).find('td').eq(0).attr('scode');
                    $(this).find('td').each(function (key, val) {
                        $ffstnv = ($(this).find('.ffcharge').val() == '') ? 'no': parseFloat($(this).find('.ffcharge').val());
                        var fieldof = $(this).closest('table').find('.headernam').find('th').eq(key).find('.heaser').attr('value');
                        if (fieldof != '' && $ffstnv >= 0 && $ffstnv !='no') {
                            fielarr.push({
                                scode: snme,
                                fieval: $ffstnv,
                                fieldhead: fieldof
                            })
                        }
                    });
                });
                var chargekm = ' ';
            }
            else {
                var chargekm = $('#chargekm').val();
                if (chargekm == '') {
                    chargekm = ' ';
                }
            }
            var excepND = ($('#excep').is(":checked")) == true ? 1 : 0;
            var sfs = 0;
            //var sfs = ($('#ddlgrade').val());
            //var gradeall = ($('#forall').is(":checked")) == true ? 1 : 0;
            //if (sfs == null && gradeall == 0) {
            //    alert("Select Atleast One Grade");
            //    return false;
            //}
            //else if (gradeall == 1) {
            //    sfs = "";
            //}
            //else {
            //    sfs = sfs.join(',');
            //}
            if ($('#HQ').is(":checked") || $('#Ex-HQ').is(":checked") || $('#OS').is(":checked")) {
                $('input[name=allowance]').each(function () {
                    if ($(this).is(":checked"))
                        allowance += ($(this).val()) + ',';
                });
            }
            else {
                alert("Please choose allowance");
                return false;
            }
            data = { "DivCode": "<%=Session["div_code"]%>", "Mode": mode, "chargekm": chargekm, "Meter": meter, "Driver": driver, "Echange": echange, "Allowance": allowance.trim(), "GLCode": glcode, "fuelEffDate": effdate, "Grade": sfs, "ExceptionNd": excepND, "fielarr": fielarr }
            saveForms();
        })

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            $("#tSearchOrd").val('');
            Orders = FrmsandFlds
            ReloadTable();
        });

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = FrmsandFlds.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = FrmsandFlds
            ReloadTable();
        });

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    slno = $i + 1;
                    tr = $("<tr class='" + Orders[$i].Sl_No + "' id='" + Orders[$i].Sl_No + "'></tr>");
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].MOT + '</td><td>' + Orders[$i].StEndNeed + '</td><td>' + Orders[$i].DriverNeed + '</td><td>' + Orders[$i].Alw_Eligibilty + '</td><td class="frmedit"><a href="#">Edit</a></td>' + '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState" data-val="' + Orders[$i].Flag + '">' + Orders[$i].Flag + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' + '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td>');
                    $("#OrderList TBODY").append(tr);
                }
            }

            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            $(".ddlStatus>li>a").on("click", function () {
                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("value");
                $indx = $(this).closest("tr").index();
                cStusNm = $(this).text();
                if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                    sf = Orders[$indx].Sl_No;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "TravelMod.aspx/SetTravelStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                        dataType: "json",
                        success: function (data) {
                            Orders[$indx].Flag = cStusNm;
                            Orders[$indx].Active_Flag = stus;
                            $(cStus).html(cStusNm);
                            alert('Status Changed Successfully...');
				    onload();
                            ReloadTable();
                        },
                        error: function (result) {
                        }
                    });
                }
            });
            ($('#meter').is(":checked") == false) ? ($('#bucharge').show()) : ($('#bucharge').hide());
            loadPgNos();
        }

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg)) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable();
            }
            );
        }
    </script>
</asp:Content>
