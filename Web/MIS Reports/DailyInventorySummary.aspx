<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DailyInventorySummary.aspx.cs" Inherits="MIS_Reports_DailyInventorySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />

    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            $('.datetimepicker').datepicker('setDate', new Date());


            $(document).on('click', '.btnview', function () {

                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                var fDate = $("#<%=txtFromDate.ClientID%>").val();
                var tDate = $("#<%=txtToDate.ClientID%>").val();
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                var str = 'rptDailyInventorySummary.aspx?&SFCode=' + sf_code + '&FDate=' + fDate + '&TDate=' + tDate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                window.open(str, "popupWindow", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
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
                    From Date</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div1" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker" autocomplete="off" style="min-width: 110px; width: 120px;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtToDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                    To Date</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div2" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="text" name="txtToDate" runat="server" id="txtToDate" class="form-control datetimepicker" autocomplete="off" style="min-width: 110px; width: 120px;" />
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

