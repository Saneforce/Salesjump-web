<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SFWiseCalls.aspx.cs" Inherits="MIS_Reports_SFWiseCalls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }

        body {
            overflow-x: unset !important;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            $('.datetimepicker').datepicker('setDate', new Date());

            var start = moment().startOf('day'); // set to 12:00 am today
            var end = moment().endOf('day'); // set to 23

            var sTime = $("#<%=STimer.ClientID%>");
            var eTime = $("#<%=ETimer.ClientID%>");
            for (var k = 1; k <= 12; k++) {

                sTime.append('<option value=' + (k) + '>  ' + (k) + ' : 00 AM </option>');
                eTime.append('<option value=' + (k) + '> ' + (k) + ': 00 AM </option>');
            }
            for (var k = 1; k <= 12; k++) {
                sTime.append('<option value=' + (k + 12) + '>  ' + (k) + ': 00 PM </option>');
                eTime.append('<option value=' + (k + 12) + '> ' + (k) + ': 00 PM </option>');
            }

            var tSlot = $("#<%=TSlot.ClientID%>");

            tSlot.append('<option value=15>15 Minute</option>');
            tSlot.append('<option value=30>30 Minute</option>');
            tSlot.append('<option value=60>1 hour </option>');
            tSlot.append('<option value=120>2 hour</option>');
            tSlot.append('<option value=180>3 hour</option>');
            tSlot.append('<option value=240>4 hour</option>');

            $(document).on('click', '.btnview', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }

                var sT = $("#<%=STimer.ClientID%>").val();
                var eT = $("#<%=ETimer.ClientID%>").val();

                if (Number(sT) >= Number(eT)) { alert('End Time high to Start Time..!'); $("#<%=ETimer.ClientID%>").focus(); return false; }

                var dif = eT - sT;
                var tS = $("#<%=TSlot.ClientID%>").val();

                if (Math.floor(Number(tS) / 60) > dif) { alert('Time Slot High to Time..!'); $("#<%=TSlot.ClientID%>").focus(); return false; }


                var fDate = $("#<%=txtFromDate.ClientID%>").val();

                sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                //  var fDate = "2019/01/10";
                var SubDiv = $("#<%=subdiv.ClientID%>").val();
                var SFName = $("#<%=ddlFieldForce.ClientID%> option:selected").text();
                var str = 'rptSFWiseCalls.aspx?&SFCode=' + sf_code + '&SFName=' + SFName + '&SubDiv=' + SubDiv + '&FDate=' + fDate + '&sTime=' + sT + '&eTime=' + eT + '&tSlot=' + tS;
                window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
            });
        });
    </script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px;" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server"
                            CssClass="form-control" Style="min-width: 100px; width: 350PX">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtFromDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                    Date</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div1" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <%-- <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datepicker"
                            style="min-width: 110px; width: 120px;" />--%>
                        <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker" autocomplete="off" style="min-width: 110px; width: 120px;" />

                    </div>
                </div>
            </div>

            <div class="row">
                <label id="Label3" class="col-md-2  col-md-offset-3  control-label">
                    Start Time</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="STimer" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px;">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="Label4" class="col-md-2  col-md-offset-3  control-label">
                    Ent Time</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ETimer" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px;">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="Label5" class="col-md-2  col-md-offset-3  control-label">
                    Time Slot</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="TSlot" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px;">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6  col-md-offset-5">
                    <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

