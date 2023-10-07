<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RouteWise_transfer.aspx.cs" Inherits="MasterFiles_RouteWise_transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link href="../css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <form runat="server">

        <div class="row">

            <div class="col-md-6">

                <div>
                    <div class="form-group">
                        <label class="control-label " for="focusedInput">
                            Transfer From Field Force
                        </label>
                        <span style="color: Red">*</span>
                        <select class="form-control" id="ddlff" style="width: 100%;" data-size="5" tabindex="-98">
                            <option value="0">Select Field Force</option>
                        </select>
                    </div>
                </div>

                <div>
                    <div class="form-group">
                        <label class="control-label " for="focusedInput">
                            Transfer From Distributor
                        </label>
                        <span style="color: Red">*</span>
                        <select class="form-control" id="ddlDis" style="width: 100%;" data-size="5" tabindex="-98">
                            <option value="0">Select Distributor</option>
                        </select>
                    </div>
                </div>

                <div>
                    <div class="form-group" style="display: none;">
                        <label class="control-label " for="focusedInput">
                            Route (To be Transfer)
                        </label>
                        <span style="color: Red">*</span>
                        <select class="form-control" id="ddlroute" style="width: 100%;" data-size="5" tabindex="-98" multiple>
                        </select>
                    </div>
                </div>
            </div>

            <div class="col-md-6">

                <div>
                    <div class="form-group">
                        <label class="control-label " for="focusedInput">
                            Transfer To Field Force
                        </label>
                        <span style="color: Red">*</span>
                        <select class="form-control" id="ddltFFF" style="width: 100%;" data-size="5" tabindex="-98">
                            <option value="0">Select Field Force</option>
                        </select>
                    </div>
                </div>

                <div>
                    <div class="form-group">
                        <label class="control-label " for="focusedInput">
                            Transfer To Distributor
                        </label>
                        <span style="color: Red">*</span>
                        <select class="form-control" id="ddltDis" style="width: 100%;" data-size="5" tabindex="-98">
                            <option value="0">Select Distributor</option>
                        </select>
                    </div>
                </div>

                <div>
                    <div class="form-group" style="display: none;">
                        <label class="control-label " for="focusedInput">
                            Transfer To Route
                        </label>
                        <span style="color: Red">*</span>
                        <select class="form-control" id="ddltRoute" style="width: 100%;" data-size="5" tabindex="-98" multiple>
                        </select>
                    </div>
                </div>

            </div>

        </div>

        <div class="row">

            <div class="col-md-4 routeOnly" style="margin-left: 87px;">
                <a href="#" class="list-group-item active Route" style="width: 95%;">From Route<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                <div class="list-group" id="Route" style="border: 1px solid #ddd">
                </div>
            </div>

            <div class="col-md-2">
                <center>
                    <div class="form-group">
                        <input type="button" class="btn btn-primary btn-update" id="btnCopy" value="Copy" style="margin-top: 46px;" />
                    </div>
                    <div class="form-group">
                        <input type="button" class="btn btn-primary btn-update" id="btnleft" value=">>" style="margin-top: 46px;" />
                    </div>
                </center>
            </div>


            <div class="col-md-4 TrouteOnly">
                <a href="#" class="list-group-item active TRoute" style="width: 95%;">To Route</a><%--<input title="toggle all" type="checkbox" class="all pull-right" />--%>
                <div class="list-group" id="TRoute" style="border: 1px solid #ddd">
                </div>
            </div>

        </div>

        <div class="row">
            <div>
                <center>
                    <div class="form-group">
                        <input type="button" class="btn btn-primary btn-update" id="btnsub" value="Transfer" style="margin-top: 46px;" />
                    </div>
                </center>
            </div>
        </div>

    </form>

    <style>
        .list-group {
            max-height: 280px;
            min-height: 280px;
            width: 95%;
            margin-bottom: 10px;
            overflow: scroll;
            -webkit-overflow-scrolling: touch;
        }
    </style>

    <script type="text/javascript">

        var AllRoute = [], AllFF = []; var SFF = []; var RTE = []; var Dist_Route = []; var Dis_Details = []; var Terr_Details = [];

        $(document).ready(function () {

            loadFieldForce();

            $('.routeOnly .all').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#Route').find("[type=checkbox]").prop("checked", true);
                }
                else {
                    $('#Route').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                }
            });

            $('#ddlff').on('change', function () {

                var ff_Code = $(this).val();
                Bind_distributor(ff_Code);

                $('#ddlDis').empty();
                $('#ddlDis').selectpicker('destroy');
                $('#ddlDis').append('<option selected="selected" value="0">Select Distributor</option>');
                if (Dis_Details.length > 0) {
                    for (var i = 0; i < Dis_Details.length; i++) {
                        $('#ddlDis').append($('<option value="' + Dis_Details[i].Stockist_Code + '">' + Dis_Details[i].Stockist_Name + '</option>'))
                    }
                }
                $('#ddlDis').selectpicker({
                    liveSearch: true
                });
            });

            $('#ddltFFF').on('change', function () {

                var Selected_field_Code = $(this).val();

                Bind_distributor(Selected_field_Code);

                $("#ddltDis").empty();
                $('#ddltDis').selectpicker('destroy');
                $('#ddltDis').append('<option selected="selected" value="0">Select Distributor</option>');
                if (Dis_Details.length > 0) {
                    for (var i = 0; i < Dis_Details.length; i++) {
                        $('#ddltDis').append($('<option value="' + Dis_Details[i].Stockist_Code + '">' + Dis_Details[i].Stockist_Name + '</option>'))
                    }
                }

                $('#ddltDis').selectpicker({
                    liveSearch: true
                });
            });

            function Bind_distributor(Selected_field_Code) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RouteWise_transfer.aspx/Get_FieldWise_Dist",
                    data: "{'divcode':'<%=Session["div_code"]%>','DisCode':'" + Selected_field_Code + "'}",
                    dataType: "json",
                    success: function (data) {
                        Dis_Details = JSON.parse(data.d) || [];
                    }
                });
            }

            function Get_route(Selected_Dist_Code,Field_Code,typ) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
					async: false,
                    url: "RouteWise_transfer.aspx/bindDisWiseRoute",
                    data: "{'divcode':'<%=Session["div_code"]%>','DisCode':'" + Selected_Dist_Code + "','Field_Code':'" + Field_Code + "'}",
                    dataType: "json",
                    success: function (data) {
                        Dist_Route = JSON.parse(data.d) || [];
                        if (typ == 'F') {
                            FromRoute = Dist_Route;
                        }
                        else if (typ == 'T') {
                            ToRoute = Dist_Route;
                        }
                    }
                });
            }

            $('#ddlDis').on('change', function () {

                var Selected_Dist_Code = $(this).val();
				var selected_field_code = $('#ddlff option:selected').val();
                Get_route(Selected_Dist_Code,selected_field_code, 'F');
                bind_Froute(FromRoute);

            });

            function bind_Froute(FromRoute) {

                var div = $('#Route'); div.html('');
                if (FromRoute.length > 0) {
                    for (var i = 0; i < FromRoute.length; i++) {
                        str = '<a href="#" class="list-group-item">' + FromRoute[i].Territory_Name + '<input type="checkbox" name="' + FromRoute[i].Territory_Name + '" value=' + FromRoute[i].Territory_Code + ' class="chk pull-right"/></a>';
                        $(div).append(str);
                    }
                }
            }

            $('#ddltDis').on('change', function () {

                var Selected_Dist_Code = $(this).val();
				var selected_field_code = $('#ddltFFF option:selected').val();
                Get_route(Selected_Dist_Code,selected_field_code, 'T');
                bind_Troute(ToRoute);
            });

            function bind_Troute(ToRoute) {

                var div = $('#TRoute'); div.html('');
                if (ToRoute.length > 0) {
                    for (var i = 0; i < ToRoute.length; i++) {
                        str = '<a href="#" class="list-group-item" type="' + ToRoute[i].typ + '" name="' + ToRoute[i].Territory_Name + '" value=' + ToRoute[i].Territory_Code + ' >' + ToRoute[i].Territory_Name + '</a>';/*<input type="checkbox" name=' + Dist_Route[i].Territory_Code + ' class="chk pull-right"/>*/
                        $(div).append(str);
                    }
                }
            }

            $('#btnCopy').on('click', function () {

                var Rount_Count = $('#Route a .chk:checked').length;

                if (Rount_Count > 0) {

                    $('#Route a .chk').each(function (e) {

                        var $this = $(this);
                        var selected_route = $(this).val();

                        var filter_toroute = ToRoute.filter(function (q) {
                            return (q.Territory_Code == selected_route);
                        });

                        if (filter_toroute.length == 0) {

                            if ($this.is(":checked")) {
                                ToRoute.push({
                                    Territory_Code: $(this).attr('value'),
                                    Territory_Name: $(this).attr('name'),
                                    typ: 'Copy'
                                });
                            }
                        }
                    });
                    //   Array.prototype.push.apply(ToRoute, FromRoute);
                    bind_Froute(FromRoute); bind_Troute(ToRoute);
                }
                else {
                    alert('Please Select Route To Transfer');
                    return false;
                }
            });

            $('#btnleft').on('click', function () {


                var FDis = $('#ddltDis option:selected').val();

                if (FDis != '' && FDis != '0') {

                    $('#Route a .chk').each(function (e) {

                        var $this = $(this);
                        var selected_route = $(this).val();

                        if ($this.is(":checked")) {

                            var flt_toroute = ToRoute.filter(function (q) {
                                return (q.Territory_Code == selected_route);
                            });

                            if (flt_toroute.length == 0) {

                                ToRoute.push({
                                    Territory_Code: $(this).attr('value'),
                                    Territory_Name: $(this).attr('name'),
                                    typ: 'Transfer'
                                });

                                for (var i = 0; i < FromRoute.length; i++) {

                                    $('#Route a .chk').each(function (e) {

                                        var $this = $(this);
                                        var st_route = $(this).val();

                                        if ($this.is(":checked")) {

                                            if ($(this).attr('value') == FromRoute[i].Territory_Code) {
                                                FromRoute.splice(i, 1);
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    });
                    bind_Froute(FromRoute); bind_Troute(ToRoute);
                }
                else {
                    alert('Please Select Transfet To Distributor To Tranfer Route');
                    return false;
                }

            });


            $('#btnsub').on('click', function () {

                var FR_route_arr = []; var To_route_arr = []; var rt_ar = ''; var field_arr = []; var ff_ar = ''; var Dis_arr = []; var dt_ar = '';
                var Transfer_Route = []; var Copy_Route = [];

                var From_FF = $('#ddlff option:selected').val();
                if (From_FF == '0' || From_FF == '') { alert('Please Select Transfer From Field Force'); return false; }

                var From_Dis = $('#ddlDis option:selected').val();
                if (From_Dis == '0' || From_Dis == '') { alert('Please Select Transfer From Distributor'); return false; }

                //var rt = $("#Route input:checked");
                //if (rt.length == 0) {
                //    buttoncount = 0;
                //    alert('Select From Route..!');
                //    return false;
                //}

                FR = '';
                $("#Route a .chk").each(function (idx, item) {
                    FR += $(item).attr('value') + ',';
                    itm = {};
                    itm.RouteCode = $(item).attr('value');
                    FR_route_arr.push(itm);
                });


                var To_FF = $('#ddltFFF option:selected').val();
                if (To_FF == '0' || To_FF == '') { alert('Please Select Transfer To Field Force'); return false; }

                var To_Dis = $('#ddltDis option:selected').val();
                if (To_Dis == '0' || To_Dis == '') { alert('Please Select Transfer To Distributor'); return false; }

                //TR = '';
                //$("#TRoute a").each(function (idx, item) {

                //    if ($(item).attr('type') == 'Copy') {
                //        itm = {};
                //        itm.RouteCode = $(item).attr('value');
                //        Copy_Route.push(itm);
                //    }

                //    if ($(item).attr('type') == 'Transfer') {
                //        itm = {};
                //        itm.RouteCode = $(item).attr('value');
                //        Transfer_Route.push(itm);
                //    }

                //});

                TR = '';
                $("#TRoute a").each(function (idx, item) {

                    if ($(item).attr('type') != 'Original') {
                        itm = {};
                        itm.RouteCode = $(item).attr('value');
                        itm.Type = $(item).attr('type')
                        Copy_Route.push(itm);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RouteWise_transfer.aspx/Save_Transfer",
                    data: "{'divcode':'<%=Session["div_code"]%>','To_Route_Details':'" + JSON.stringify(To_route_arr) + "','ToFF':'" + To_FF + "','ToDis':'" + To_Dis + "','FromFF':'" + From_FF + "','FromDis':'" + From_Dis + "','FR_Route_Details':'" + JSON.stringify(FR_route_arr) + "','Cpy_Route':'" + JSON.stringify(Copy_Route) + "','Trf_Route':'" + JSON.stringify(Transfer_Route) + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Success') {
                            alert('Transfer Successfully');
                            //$('#ddlff').empty();
                            //$('#ddlff').selectpicker('destroy');
                            //loadFieldForce();
                            //bindRoute(0);
                        }
                        else {
                            alert('Transfer Failed');
                        }
                    },
                });
            });
        });

        function loadFieldForce() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RouteWise_transfer.aspx/getFieldForce",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    AllFF = JSON.parse(data.d) || [];

                    FmFF = AllFF.filter(function (g) {
                        return (g.SF_Status != 2);
                    });

                    TFF = AllFF.filter(function (g) {
                        return (g.SF_Status == 0 || g.SF_Status == 1);
                    });

                    if (FmFF.length > 0) {
                        var dept = $("#ddlff"); dept.html('');
                        dept.empty().append('<option selected="selected" value="0">Select FieldForce</option>');
                        for (var i = 0; i < FmFF.length; i++) {
                            dept.append($('<option value="' + FmFF[i].SF_Code + '">' + FmFF[i].SF_Nm + '</option>'))
                        }
                    }

                    if (TFF.length > 0) {
                        var multi_ff = $('#ddltFFF'); multi_ff.html('');
                        multi_ff.empty().append('<option selected="selected" value="0">Select FieldForce</option>');
                        for (var i = 0; i < TFF.length; i++) {
                            multi_ff.append($('<option value="' + TFF[i].SF_Code + '">' + TFF[i].SF_Nm + '</option>'))
                        }
                    }
                }
            });
            $('#ddlff').selectpicker({
                liveSearch: true
            });
            $('#ddltFFF').selectpicker({
                liveSearch: true
            });
        }

    </script>

</asp:Content>

