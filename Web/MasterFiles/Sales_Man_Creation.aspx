<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Sales_Man_Creation.aspx.cs" Inherits="MasterFiles_Sales_Man_Creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-show-password/1.0.3/bootstrap-show-password.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <form id="frm1" autocomplete="off" runat="server">
        <style type="text/css">
            input[type='text'], select {
                border-radius: 5px;
            }

            .ui-autocomplete {
                max-height: 300px;
                overflow-y: auto;
                overflow-x: hidden;
            }

            .ui-menu-item > a {
                display: block;
            }
        </style>
        <div class="row">
            <div class="col-lg-12 sub-header">
                SalesMan Creation
        <button type="button" class="btn btn-warning btn-circle btn-lg" data-toggle="modal" data-target="#exampleModal" style="display: none;">+</button>
            </div>
        </div>
        <div class="modal fade" id="exampleModal" tabindex="-1" style="z-index: 1000000" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnpls" class="btn btn-info btn-circle btn-lg" style="float: right">+</button>
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
                                            <input name="field" type="text" autocomplete="off" style="width: 100%;" />
                                        </div>
                                        <div class="col-sm-6">
                                            <input name="value" type="text" autocomplete="off" style="width: 100%;" />
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
        <div style="margin-top: 10px;">
            <div class="row" style="margin-left: 4px;">
                <label id="lbldname" class="col-lg-6">
                    DSM Name</label>
                <label id="lbldtype" class="col-lg-6">
                    Designation/Type</label>
            </div>
            <div class="row">
                <div class="col-xs-5" style="margin-left: 15px;">
                    <asp:HiddenField ID="hdsmcode" runat="server" />
                    <input type="text" class="form-control" id="txtdsmname" autocomplete="off" style="width: 283px;" />
                </div>
                <div class="col-xs-5" style="margin-left: 82px;">
                    <input list="designation" name="desig" class="form-control autoc ui-autocomplete-input" id="txttype" style="width: 189px;" />
                    <datalist id="designation">
                    </datalist>
                </div>
            </div>
            <div class="row" style="margin-top: 24px; margin-left: 4px;">
                <div class="col-lg-5">
                    <label id="lblstatus">
                        Status</label>
                </div>
                <div class="col-lg-5" style="margin-left: 88px;">
                    <label id="lblstype">
                        Sales Type</label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-5 col-xs-1" style="margin-left: 14px;">
                    <select id="ddlstatus" class="form-control" name="status" style="/* margin-left: 5px; */width: 282px; /* border-right-width: 4px; */">
                        <option value="0">Active</option>
                        <option value="1">Vacant/Block</option>
                        <option value="2">Deactivate</option>
                    </select>
                </div>
                <div class="col-lg-5 col-xs-1">
                    <select id="ddlstype" class="form-control" name="salestype" style="width: 187px; margin-left: 83px;">
                        <option value="1">Van</option>
                        <option value="0">Field</option>
                    </select>
                </div>
            </div>
            <div class="row" style="margin-top: 20px; margin-left: 4px;">
                <label id="lblusrname " class="col-lg-6 col-xs-1">
                    User Name</label>
                <label id="lblpass" class="col-lg-6 col-xs-1">
                    Password</label>
            </div>
            <div class="row" style="/* margin-top: 8px; */margin-left: 4px;">
                <div class="col-lg-5">
                    <label id="lbldusr"></label>
                    <input type="text" id="txtusrname" autocomplete="off" style="width: 279px;" />
                </div>
                <div class="col-xs-4" style="margin-left: 86px;">
                    <input type="password" class="form-control" id="password" data-toggle="password"
                        autocomplete="new-password" style="width: 194px;" />
                </div>
            </div>
            <div class="row" style="margin-top: 20px; margin-left: 4px;">
                <label id="lblemail" class="col-lg-6">
                    Email ID</label>
                <label id="lblmobile" class="col-lg-6">
                    Mobile</label>
            </div>
            <div class="row" style="/* margin-top: 8px; */margin-left: 4px;">
                <div class="col-lg-5">
                    <input type="text" id="txtemail" placeholder="loremipsum@gmail.com" autocomplete="off"
                        style="width: 279px;" />
                </div>
                <div class="col-xs-4" style="margin-left: 86px;">
                    <input type="text" class="form-control" id="txtmobile" maxlength="10" autocomplete="off"
                        style="width: 194px;" />
                </div>
            </div>
            <div class="row hdist" style="margin-top: 24px; margin-left: 4px;">
                <div class="col-lg-5">
                    <label id="lbldist">
                        Distributor</label>
                    <select id="ddldist" class="form-control hdist" name="distlist" style="width: 207px; height: 29px;">
                    </select>
                </div>
            </div>
            <center>
            <button style="margin-top: 20px;" type="button" id="btnsave" class="btn btn-primary">
                Save</button>
        </center>
        </div>
    </form>
    <script type="text/javascript">
        var stk = [];
        var stkc = [];
        //$(document).on('keyup', '.autoc', function () {
        //    var s = $(this).val();
        //    $(".autoc").autocomplete({
        //        source: function (request, response) {
        //            $.ajax({
        //                url: 'Sales_Man_Creation.aspx/GetDesignDetails',
        //                data: "{ 'prefix': '" + s + "'}",
        //                dataType: "json",
        //                type: "POST",
        //                contentType: "application/json; charset=utf-8",
        //                success: function (data) {
        //                    response($.map(data.d, function (item, id) {
        //                        return {
        //                            label: item,
        //                            val: item,
        //                        }
        //                    }))
        //                },
        //                error: function (response) {
        //                    alert(response.responseText);
        //                },
        //                failure: function (response) {
        //                    alert(response.responseText);
        //                }
        //            });
        //        },
        //        minLength: 1
        //    });

        //});

        $(document).on('keypress', '#txtmobile', function (e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        function getdatalist() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Sales_Man_Creation.aspx/GetDesgnName",
                dataType: "json",
                success: function (data) {
                    dts = data.d;
                    if (dts.length > 0) {
                        var st = $('#designation');
                        for (var i = 0; i < dts.length; i++) {
                            st.append($('<option value="' + dts[i].label + '">'));
                        }
                    }
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }

        //function filldata1(dts) {

        //    $(".autoc").autocomplete({
        //        source: dts
        //    });
        //}


        function filldata(divcode, dcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Sales_Man_Creation.aspx/fillData",
                data: "{'divcode':'" + divcode + "','dsmcode':'" + dcode + "'}",
                dataType: "json",
                success: function (data) {
                    stk = JSON.parse(data.d) || [];
                    $('#txtdsmname').val(stk[0].DSM_name);
                    $('#txttype').val(stk[0].Desig_type);
                    $('select[name="status"]').val(stk[0].DSM_Active_Flag);
                    //$('select[name="salestype"] :selected').text(stk[0].Salestype);
                    $('select[name="salestype"] option').each(function () {
                        if ($(this).text().toLowerCase() == (stk[0].Salestype).toLowerCase()) {
                            this.selected = true;
                            return;
                        }
                    });
                    var stkd = stkc.filter(function (obj) {
                        return obj.stockist_code == stk[0].Distributor_Code;
                    });
                    var arr = stk[0].UserName;
                    if (<%=Session["sf_type"]%>== 4) {
                        arr = arr.split('<%=Session["UserName"]%>');
                        $('#txtusrname').val(arr[1]);
                    }
                    else {
                        arr = arr.split(stkd[0].Username);
                        $('#lbldusr').text(stkd[0].Username);
                        $('#txtusrname').val(arr[1]);
                    }
                    $('#password').val(stk[0].Password);
                    $('select[name="distlist"]').val(stk[0].Distributor_Code);
                    $('#txtemail').val(stk[0].DSM_Email);
                    $('#txtmobile').val(stk[0].DSM_Phone_no);;
                },
                error: function (result) {
                    console.log(result);
                }
            });
        }

        function fillfield() {

        }

        function filldist(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Sales_Man_Creation.aspx/getstockist",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    stkc = JSON.parse(data.d) || [];
                    var dist = $('select[name="distlist"]');
                    dist.empty();
                    for (var i = 0; i < stkc.length; i++) {
                        dist.append($('<option value="' + stkc[i].stockist_code + '">' + stkc[i].Stockist_Name + '</option>'));
                    }
                },
                error: function (result) {
                    console.log(error);
                }
            });
        }

        function clear() {
            $('#txtdsmname').val('');
            $('#txttype').val('');
            $('select[name="status"]').val(0);
            $('select[name="salestype"]').val(1);
            $('#txtusrname').val('');
            $('#password').val('');
            $('select[name="distlist"]').val(0);
            $('#txtemail').val('');
            $('#txtmobile').val('');
        }
        $(document).ready(function () {
            $('.hdist').hide();
            var dcode = $('#<%=hdsmcode.ClientID%>').val();
            getdatalist();
            if (<%=Session["sf_type"]%>== 4) {
                divcode ='<%=Session["div_code"]%>';
                var divcode1 = divcode.split(',');
                divcode = divcode1[0];

                $('#lbldusr').text('<%=Session["UserName"]%>');
                if (dcode != null && dcode != '') {
                    filldata(divcode, dcode);
                }
            }
            else {
                divcode = Number(<%=Session["div_code"]%>);
                $('.hdist').show();
                filldist(divcode);
                if (dcode != null && dcode != '') {
                    filldata(divcode, dcode);
                }
            }

            var ctr = 1;
            $('#btnpls').on('click', function () {
                ctr++;
                var txtfield = "txtfield" + ctr;
                var txtval = "txtval" + ctr;
                var newTr = '<div class="row" style="padding-top: 11px;"><div class="col-sm-6 cls"><input type="text" name="field" style="width:100%" autocomplete="off" id=' + txtfield + ' /></div><div class="col-sm-6 cls"><input type="text" style="width:100%" name="value" autocomplete="off" id=' + txtval + ' /></div></div>';
                $('.scrldiv').append(newTr);
            });

            $('#ddldist').on('change', function () {
                var sle = $(this).val();
                var filt = stkc.filter(function (obj) {
                    return obj.stockist_code == sle;
                });
                $('#lbldusr').text(filt[0].Username);
            });
            $('#btnsave').on('click', function () {
                var dsmname = $('#txtdsmname').val();
                if (dsmname == null || dsmname == '') {
                    alert('Enter the DSM Name');
                    return false;
                    focus('#txtdsmname');
                }
                var dtype = $('#txttype').val();
                if (dtype == null || dtype == '') {
                    alert('Enter the Type');
                    return false;
                    focus('#txttype');
                }
                var statval = $('select[name="status"] :selected').val();
                var status = $('select[name="status"] :selected').text();
                var salval = $('select[name="salestype"] :selected').val();
                var saltype = $('select[name="salestype"] :selected').text();
                var email = $('#txtemail').val();
                if (email == null || email == '') {
                    alert('Enter the Email');
                    return false;
                    focus('#txtemail');
                }

                if (IsEmail(email) == false) {
                    alert('Invalid Email ID');
                    return false;
                }

                function IsEmail(email) {
                    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!regex.test(email)) {
                        return false;
                    } else {
                        return true;
                    }
                }

                var mobil = $('#txtmobile').val();
                var usrname = $('#lbldusr').text() + $('#txtusrname').val();
                var usr1 = $('#txtusrname').val();
                if (usr1 == null || usr1 == '') {
                    alert('Enter the Username');
                    return false;
                    focus('#txtusrname');
                }
                var passwd = $('#password').val();
                if (passwd == null || passwd == '') {
                    alert('Enter the Password');
                    return false;
                    focus('#password');
                }
                if (<%=Session["sf_type"]%>== 3 || <%=Session["sf_type"]%>== 2) {
                    var distname = $('select[name="distlist"] :selected').text();
                    var distcode = $('select[name="distlist"] :selected').val();
                    data = { "DSMCode": dcode, "DivCode": divcode, "DSMName": dsmname, "DType": dtype, "Status": statval, "Salestype": saltype, "UsrName": usrname, "PWD": passwd, "Dist": distcode, "Distname": distname, "Mobile": mobil, "Email": email }
                }
                else {
                    data = { "DSMCode": dcode, "DivCode": divcode, "DSMName": dsmname, "DType": dtype, "Status": statval, "Salestype": saltype, "UsrName": usrname, "PWD": passwd, "Dist": "<%=Session["Sf_Code"]%>", "distname": "<%=Session["sf_name"]%>", "Mobile": mobil, "Email": email }
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Sales_Man_Creation.aspx/savedsm",
                    data: "{'data':'" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        if (data.d == 'Success' || data.d == 'success') {
                            window.location.href = "SalesMan_List.aspx";
                        }
                    },
                    error: function (result) {
                        console.log(result);
                    }
                });
            });
        });
    </script>
    <%--   <script type="text/javascript">
        $("#password").password('toggle');
    </script>--%>
</asp:Content>
