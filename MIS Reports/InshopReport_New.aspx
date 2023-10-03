<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="InshopReport_New.aspx.cs" Inherits="MIS_Reports_InshopReport_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
     <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript">

        $(document).ready(function () {

            $('.datePicker').datepicker({ dateFormat: 'dd/mm/yy' });
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
            $('#fromDate').val(today);
            $('#toDate').val(today);

        });

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function NewWindow() {
            var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (st == "---Select Field Force---") { alert("Select Field Force."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

            var st = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();


            var fromdate = $('#fromDate').val();

            if (fromdate.length <= 0) {
                alert('Enter From Date..!');
                $('#fromDate').focus();
                return false;
            }

            var todate = $('#toDate').val();

            if (todate.length <= 0) {
                alert('Enter To Date..!');
                $('#toDate').focus();
                return false;
            }

            var subDiv = $('#<%=subdiv.ClientID%> :selected').val();


            var str = 'rpt_inshopreport_new.aspx?&SFCode=' + st + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + sf_name + '&SubDiv=' + subDiv;
            window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');

        }



    </script>


    <form id="form1" runat="server">
        <h2>Inshop Sale Report</h2>
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label1" class="col-md-1 col-md-offset-3 control-label">
                    Division</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                            OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="lblFF" class="col-md-1 col-md-offset-3 control-label">
                    Manager</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group" id="kk" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="form-control"
                            Visible="false" Width="350">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired" CssClass="form-control"
                            Width="70">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="fromDate" class="col-md-1 col-md-offset-3 control-label">From Date</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="text" name="fromDate" id="fromDate" class="form-control datePicker" />
                    </div>
                </div>
            </div>

              <div class="row">
                                             <label for="fromDate" class="col-md-1 col-md-offset-3 control-label">To Date</label>
                                        <div class="col-md-3 inputGroupContainer">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                <input type="text" name="fromDate" id="toDate" class="form-control datePicker"/>
                                            </div>
                                        </div>                            
                            </div>
                          
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnView" class="btn btn-primary" onclick="NewWindow()"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

