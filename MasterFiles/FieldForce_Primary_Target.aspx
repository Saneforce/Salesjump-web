<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FieldForce_Primary_Target.aspx.cs" Inherits="MasterFiles_FieldForce_Primary_Target" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .card-body {
            overflow: auto;
        }
    </style>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.4.1.js"></script>
    <form id="form1" runat="server"><div class="row">
        <div class="col-lg-12 sub-header">FieldForcewise Primary Target</div>
        </div>
        <div style="padding: 5px;">
            <div class="form-group">
                <div class="row">

                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Division" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>

                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                                OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Team" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true"
                                Width="350" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-sm-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control" Width="120">
                            </asp:DropDownList>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <button id="btnview" type="button" class="btn btn-primary" style="vertical-align: middle">
                            View</button>
                    </div>
                </div>

            </div>


            <div class="card">
                <div class="card-body table-responsive">
                    <div style="white-space: nowrap">
                        Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                        <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                    </div>
                    <table class="table table-hover" id="FFTarget">
                        <thead class="text-warning" style="white-space: nowrap;">
                            <tr>
                                <th style="text-align: left;" id="month1">FieldForce</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="1" />Jan</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="2" />Feb</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="3" />Mar</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="4" />Apr</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="5" />May</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="6" />Jun</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="7" />Jul</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="8" />Aug</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="9" />Sep</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="10" />Oct</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="11" />Nov</th>
                                <th style="text-align: left;">
                                    <input type="hidden" class="monid" name="month" value="12" />Dec</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot></tfoot>
                    </table>
                </div>
                <div class="row" style="text-align: center">
                    <div class="col-md-12 inputGroupContainer">
                        <a id="btnsave" class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;"><span>Save</span></a>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" type="text/javascript">
        var AllOrders = [];
        var divcode;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Name,Sf_Code";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            });
        function loadPgNos() {
            $(".pagination").html("");
            TotalPg = (Orders.length / PgRecords).toFixed(0) //+ ((Orders.length % PgRecords) ? 1 : 0)
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>';
            for (il = 0; il < TotalPg; il++) {
                spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">Next</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = $(this).text(); ReloadTable();
            });
        }
        function ReloadTable() {
            $("#FFTarget TBODY").html("");
            var j = 0;
            var month = '';
            var rr = '';
            var rt = '';
            var rt1 = '';
            for ($i = 0; $i < Orders.length; $i++) {
                if ($i < Orders.length) {
                    tr = "<tr>";
                    tr += '<td  class="celda_normal" style="min-width: 100px;" > <input type="hidden" name="FFcode" value="' + Orders[$i].Sf_Code + '"/> <p class="phh" name="sfname">' + Orders[$i].Sf_Name + '</p></td>';
                    for (j = 1; j < 13; j++) {
                        slno = $i + 1;
                        rr = rDts.filter(function (obj) {
                            return (obj.SF_Code === Orders[$i].Sf_Code) && (obj.month === $('#FFTarget').find('thead').find('th').eq(j).find("input[name='month']").val());
                        });
                        if (rr.length > 0) {
                            rt = rr[0].Target_Value
                            rt1 = rr[0].Trans_target_detail
                        }
                        if (rDts.length > 0) {
                            tr += '<td  class="celda_normal" style="min-width: 100px; "><input type="hidden" name="detailno" value="' + rt1 + '"/><input type="text" class="proqnty" name="target" value="' + rt + '" style="width:60px"/> </td>';
                            rt = '';
                            rt1 = '';
                        }
                        else {
                            tr += '<td><input type="text" class="proqnty" name="target" style="width:60px"/></td>';
                        }
                    }
                    tr += "</tr>";
                    $("#FFTarget TBODY").append(tr);
                    tr = "";
                }
            }
            loadPgNos();
        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
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
                Orders = AllOrders
            ReloadTable();
        });
        function loadRetailer() {
            FYear = $('[id*=ddlFYear]').val();
            ddlFF = $('[id*=ddlFieldForce]').val();
            var Subdiv = $('[id*=subdiv]').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "FieldForce_Primary_Target.aspx/GetFF",
                data: "{'Stockist_Code':'" + ddlFF + "','ddlFieldForce':'" + ddlFF + "','subdiv':'" + Subdiv + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
            var div_code = ("<%=Session["div_code"].ToString()%>");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "FieldForce_Primary_Target.aspx/getqnty",
                data: "{'div':'" + div_code + "','sfcode':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "','year':'" + $('#<%=ddlFYear.ClientID%>').val() + "'}",
                dataType: "json",
                success: function (data) {
                    rDts = data.d; ReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        $(document).on('click', '#btnsave', function () {
            var t_month = '';
            var t_qty = 0;
            var FF = '';
            var v = 2;
            var arr = [];
            $('#FFTarget').each(function () {
                if (Orders.length > 0) {
                    for (var i = 0; i < Orders.length; i++) {
                        for (var j = 1; j < 13; j++) {
                            if (Number($(this).find('tbody').find('tr').eq(i).find('td').eq(j).find("input[name='target']").val() || 0) > 0) {
                                arr.push({
                                    month: $(this).find('thead').find('th').eq(j).find("input[name='month']").val(),
                                    FF_code: $(this).find('tbody').find('tr').eq(i).find('td').eq(0).find("input[name='FFcode']").val(),
                                    T_Qnty: $(this).find('tbody').find('tr').eq(i).find('td').eq(j).find("input[name='target']").val()
                                });
                                v = v + 1;
                            }
                        }
                    }
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "FieldForce_Primary_Target.aspx/inserttarget",
                data: "{'target':'" + JSON.stringify(arr) + "','div':'" + ("<%=Session["div_code"].ToString()%>") + "', 'Sf_code':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "','year':'" + $('#<%=ddlFYear.ClientID%>').val() + "','subdiv':'" + $('#<%=subdiv.ClientID%>').val() + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d = "updated") {
                        alert(" Target updated Successfully");
                    }
                    else {
                        alert(" Target updation Unsuccesful");
                    }

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        });

        $(document).on('keypress', 'input[name=target]', function (event) {
            return isNumberOnly(event, this)
        });

        function isNumberOnly(evt, element) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        $(document).ready(function () {
            $(document).on('click', '[id*=btnview]', function () {
                var Team = $('[id*=ddlFieldForce]').val();
                if (Team == "0") { alert('Select Team..!'); $('[id*=ddlFieldForce]').focus(); return false; }
                loadRetailer();
            });
        });

    </script>
</asp:Content>
