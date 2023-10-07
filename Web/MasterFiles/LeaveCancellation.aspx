<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="LeaveCancellation.aspx.cs" Inherits="MasterFiles_LeaveCancellation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            getLeave = function () {
                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var sfCode = $("#<%=ddlFieldForce.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveCancellation.aspx/GetLeave",
                    data: "{'SFCode':'" + sfCode + "','FYear':'" + Fyear + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        CDtls = data.d;
                        var tbl = $('#tblLeave');
                        $(tbl).find('tbody tr').remove();
                        if (CDtls.length > 0) {
                            str = '';
                            for (var i = 0; i < CDtls.length; i++) {
                                str = '<td> <input name="lCode" type="hidden" value="' + CDtls[i].Leave_Id + '"/> ' + (i + 1) + '</td><td>' + CDtls[i].FieldForceName + '</td><td>' + CDtls[i].EmpCode + '</td><td>' + CDtls[i].sf_Designation_Short_Name + '</td><td>' + CDtls[i].HQ + '</td><td>' + CDtls[i].Leave_Name + '</td><td>' + CDtls[i].From_Date + '</td><td>' + CDtls[i].To_Date + '</td><td>' + CDtls[i].LeaveDays + '</td><td>' + CDtls[i].Reason + '</td><td style="text-align: center; width:200px;"><a class="btn btn-primary approve">Approve</a></td>';
                                $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                            }
                        }
                        else {
                            $(tbl).find('tbody').append('<tr><td colspan="11">No Records Found..!</td></tr>');
                        }
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });
            }
            $(document).on('click', '.btnGo', function () {
                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var sfCode = $("#<%=ddlFieldForce.ClientID%>").val();
                if (Number(sfCode) == 0) {
                    alert('Select Field Force..!');
                    $("#<%=ddlFieldForce.ClientID%>").focus();
                    return false;
                }
                getLeave();
            });
            $(document).on('click', '.approve', function () {
                lCode = $(this).closest('tr').find('input[name="lCode"]').val();
                if (confirm('Are you sure you want to Cancel this Leave?')) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "LeaveCancellation.aspx/CancelApprLeave",
                        data: "{'LeaveCode':'" + lCode + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data.d);
                            getLeave();
                        },
                        error: function (rs) {
                            console.log(rs);
                            getLeave();
                        }
                    });
                }
                else {
                    alert('Leave Not Cancel as You pressed Cancel !');
                }
            });
        });
    </script>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:dropdownlist id="subdiv" runat="server" cssclass="form-control" onselectedindexchanged="subdiv_SelectedIndexChanged" autopostback="true"
                            width="150">
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:dropdownlist id="ddlFieldForce" runat="server"
                            cssclass="form-control" width="350">
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                    Year</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:dropdownlist id="ddlFYear" runat="server" cssclass="form-control"
                            width="100">
                        </asp:dropdownlist>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-11" style="text-align: center">
                    <a id="btnGo" class="btn btn-primary btnGo" style="vertical-align: middle;">
                        <span>View</span></a>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12" style="text-align: center">
                    <br />
                    <table id="tblLeave" class="newStly" style="width: 100%">
                        <thead>
                            <tr>
                                <th>S.No</th>
                                <th>FieldForce Name	</th>
                                <th>Employee Code</th>
                                <th>Designation</th>
                                <th>HQ</th>
                                <th>Leave Type</th>
                                <th>From Date</th>
                                <th>To Date</th>
                                <th>No. of Days</th>
                                <th>Reason</th>
                                <th>Approve</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>

        </div>
    </form>
</asp:Content>

