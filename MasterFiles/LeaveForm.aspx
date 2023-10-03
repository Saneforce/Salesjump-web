<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="LeaveForm.aspx.cs" Inherits="MasterFiles_LeaveForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
        input[type='text'], select, label, submit {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

        <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $('#<%=txtFrom.ClientID%>').val(today);
            $('#<%=txtTo.ClientID%>').val(today);


            leavedays();
            //leaveCheck();
            $(document).on('change', '#<%=txtFrom.ClientID%>', function () {
                // console.log($(this).val());
                if ($(this).val().length > 0) {
                    if (isValidDate($(this).val())) {
                        var fit_start_time = $(this).val();
                        var FromDate = fit_start_time.split("/").reverse().join("-");
                        var dts = new Date(FromDate);
                        var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                        $('#<%=txtTo.ClientID%>').datepicker("destroy");
                        $('#<%=txtTo.ClientID%>').datepicker({ dateFormat: 'dd/mm/yy', minDate: cr, defaultDate: cr });
                        $('#<%=txtTo.ClientID%>').val('');
                        $('#<%=txtCount.ClientID%>').val('');
                    }
                    else {
                        alert('Invalid Date Enter or Select Correct Date..!');
                        $(this).val('');
                        $(this).focus();

                    }
                }
                // leaveCheck();
            });
            $(document).on('focus', '#<%=txtTo.ClientID%>', function () {
                if ($('#<%=txtFrom.ClientID%>').val().length > 0) {
                    if ($('#<%=txtFrom.ClientID%>').val().length > 0) {
                        if (isValidDate($('#<%=txtFrom.ClientID%>').val())) {
                            var fit_start_time = $('#<%=txtFrom.ClientID%>').val();
                            var FromDate = fit_start_time.split("/").reverse().join("-");
                            var dts = new Date(FromDate);
                            var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                            $('#<%=txtTo.ClientID%>').datepicker("destroy");
                            $('#<%=txtTo.ClientID%>').datepicker({ dateFormat: 'dd/mm/yy', minDate: cr, defaultDate: cr });
                        }
                    }
                }
                //
                $('#<%=ddlLeaveType.ClientID%>').focus();
            });

            $(document).on('change', '#<%=txtTo.ClientID%>', function () {
                if ($(this).val().length > 0) {
                    if (isValidDate($(this).val())) {
                        ServerleaveCheck();
                        leavedays();
                    }
                    else {
                        alert('Invalid Date Enter or Select Correct Date..!');
                        $(this).val('');
                        $(this).focus();

                    }
                }
                // leaveCheck();

            });

            $(document).on('change', '#<%=ddlLeaveType.ClientID%>', function () {
                if ($(this).val() > 0) {
                    //  leaveCheck();
                    ServerleaveCheck();
                }
                else {
                    //  $('#btnSave').attr("disabled", false);
                    $('#<%=msglbl.ClientID%>').text('');
                }
            });


            function ServerleaveCheck() {
                var SFCode = $('#<%=ddlFieldForce.ClientID%>').val();
                var LType = $('#<%=ddlLeaveType.ClientID%>').val();
                var FDate = $('#<%=txtFrom.ClientID%>').val();
                var TDate = $('#<%=txtTo.ClientID%>').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveForm.aspx/GetLeave_Check",
                    data: "{'sfCode':'" + SFCode + "', 'fYear':'" + FDate + "','tYear':'" + TDate + "','lCode':'" + LType + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        if (data.d[0].msg == '') {

                        }
                        else {
                            alert(data.d[0].msg);
                            $('#<%=txtFrom.ClientID%>').val(today);
                            $('#<%=txtTo.ClientID%>').val(today);
                            return false;
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }


            function leaveCheck() {

                var LType = $('#<%=ddlLeaveType.ClientID%>').val();
                $('#btnSave').attr("disabled", false);
                $('#<%=msglbl.ClientID%>').text('');
                if ($('#<%=grdLeaves.ClientID%>').find('tr').length > 1) {
                    var ch = false;
                    $('#<%=grdLeaves.ClientID%>').find('tr').each(function (index) {
                        if (index > 0) {
                            if (LType == $(this).find('[id*=hdnLCode]').val()) {
                                ch = true;
                                if (Number($(this).find('[id*=lblavailability]').text()) < Number($('#<%=txtCount.ClientID%>').val())) {
                                    $('#btnSave').attr("disabled", true);
                                    $('#<%=msglbl.ClientID%>').text($(this).find('[id*=lblID]').text() + " Greater then  your Leave Allocate..!");
                                }
                            }
                        }
                    });

                    if (ch == false) {
                        $('#btnSave').attr("disabled", true);
                    }
                }
                else {
                    $('#btnSave').attr("disabled", true);
                    $('#<%=msglbl.ClientID%>').text("Leave Not Allocate..!");

                }
            }


            function leavedays() {
                var FDate = $('#<%=txtFrom.ClientID%>').val();
                var TDate = $('#<%=txtTo.ClientID%>').val();
                var oneDay = 24 * 60 * 60 * 1000;
                var f = new Date(FDate);
                var t = new Date(TDate);
                var FromDate = FDate.split("/").reverse().join("-");
                var dts = new Date(FromDate);
                var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                var ToDate = TDate.split("/").reverse().join("-");
                var dts1 = new Date(ToDate);
                var cr = dts1.getDate() + '/' + (dts1.getMonth() + 1) + '/' + dts1.getFullYear();
                var firstDate = new Date(dts.getFullYear(), (dts.getMonth() + 1), dts.getDate());
                var secondDate = new Date(dts1.getFullYear(), (dts1.getMonth() + 1), dts1.getDate());
                var diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
                $('#<%=txtCount.ClientID%>').val(Number(diffDays) + 1);
            }

            function isValidDate(s) {
                var bits = s.split('/');
                var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
                return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
            }


            $(document).on('click', '#btnSave', function () {



                var SFCode = $('#<%=ddlFieldForce.ClientID%>').val();
                if (SFCode == 0) { alert("Select Field Force."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
                var SFCode = $('#<%=ddlFieldForce.ClientID%>').val();
                if (SFCode == 0) { alert("Select Field Force."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }


                var LType = $('#<%=ddlLeaveType.ClientID%>').val();
                if (LType == 0) { alert("Select Leave Type."); $('#<%=ddlLeaveType.ClientID%>').focus(); return false; }


                var FDate = $('#<%=txtFrom.ClientID%>').val();
                if (FDate.length == 0) { alert("Select From Date."); $('#<%=txtFrom.ClientID%>').focus(); return false; }

                var TDate = $('#<%=txtTo.ClientID%>').val();
                if (TDate.length == 0) { alert("Select To Date."); $('#<%=txtTo.ClientID%>').focus(); return false; }

                var Reason = $('#<%=txtResaon.ClientID%>').val();
                if (Reason.length == 0) { alert("Enter Some Reason..!"); $('#<%=txtResaon.ClientID%>').focus(); return false; }

                //   ServerleaveCheck();



                var SFCode = $('#<%=ddlFieldForce.ClientID%>').val();
                var LType = $('#<%=ddlLeaveType.ClientID%>').val();
                var FDate = $('#<%=txtFrom.ClientID%>').val();
                var TDate = $('#<%=txtTo.ClientID%>').val();

                var lcount = $('#<%=txtCount.ClientID%>').val();
                var reson = $('#<%=txtResaon.ClientID%>').val();



                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveForm.aspx/GetLeave_Check",
                    data: "{'sfCode':'" + SFCode + "', 'fYear':'" + FDate + "','tYear':'" + TDate + "','lCode':'" + LType + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        if (data.d[0].msg == '') {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "LeaveForm.aspx/saveLeave",
                                data: "{'sfCode':'" + SFCode + "', 'fdate':'" + FDate + "','tdate':'" + TDate + "','lCode':'" + LType + "','reson':'" + reson + "','lcount':'" + lcount + "'}",
                                dataType: "json",
                                success: function (data) {                                    
                                    alert(data.d);
                                                                       
                                },
                                error: function (result) {
                                    alert(JSON.stringify(result));
                                }
                            });

                        }
                        else {
                            alert(data.d[0].msg);
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            });
        });

    </script>

    <form id="form1" runat="server">
        <div>


            <div class="container" style="width: 100%">

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="row">
                                <label id="Label1" class="col-md-2 col-md-offset-3  control-label">
                                    Division</label>
                                <div class="col-md-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                            Style="min-width: 100px" Width="150" AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                                    Field Force</label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                            CssClass="form-control" Style="min-width: 200px; width: 370px" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label3" class="col-md-2 col-md-offset-3  control-label">
                                    Leave Type</label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="ddlLeaveType" runat="server" SkinID="ddlRequired"
                                            CssClass="form-control" Style="min-width: 200px; width: 200px;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                                    From
                                </label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                        <asp:TextBox runat="server" ID="txtFrom" CssClass="form-control  datetimepicker" Style="min-width: 100px; width: 150px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label5" class="col-md-2 col-md-offset-3  control-label">
                                    To
                                </label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                        <asp:TextBox runat="server" ID="txtTo" CssClass="form-control  datetimepicker" Style="min-width: 100px; width: 150px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label7" class="col-md-2 col-md-offset-3  control-label">
                                    No. of Days
                                </label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>

                                        <asp:TextBox ID="txtCount" CssClass="form-control" ReadOnly="true" runat="server" Width="150px" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label id="Label6" class="col-md-2 col-md-offset-3  control-label">
                                    Reason
                                </label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                        <asp:TextBox ID="txtResaon" CssClass="form-control" TextMode="multiline" Columns="40" Rows="4" runat="server" Width="370px" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6 col-md-offset-5">
                                    <%--  <button id="btnSave" runat="server" onclick="NewWindow().this" class="btn btn-primary"
                                        style="width: 100px">
                                        <span>Save</span></button>     --%>
                                    <a id="btnSave"  class="btn btn-primary" >Save</a>
                                    <asp:Label ID="msglbl" runat="server" Text="" style="color:red; font-weight:bold"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:GridView ID="grdLeaves" runat="server" CssClass="newStly" Style="width: 100%" EmptyDataText="No Records Found" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Leave Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnLCode" runat="server" Value='<%#Eval("LeaveCode")%>' />
                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("Leave_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Leave Availability">
                                    <ItemTemplate>
                                        <asp:Label ID="lblavailability" runat="server" Text='<%#Eval("LeaveAvailability")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Leave Taken">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltaken" runat="server" Text='<%#Eval("LeaveTaken")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </form>
</asp:Content>

