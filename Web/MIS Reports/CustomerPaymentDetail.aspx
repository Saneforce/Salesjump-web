<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="CustomerPaymentDetail.aspx.cs" Inherits="MIS_Reports_CustomerPaymentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        body {
            overflow-x: unset !important;
        }

        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            $('.datetimepicker').val(today);

            $('#<%=btnView.ClientID%>').click(function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == 0) { alert('Select Manager'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }

                var mr_Code = $("#<%=ddlMR.ClientID%>").val();
                if (mr_Code == 0) { alert('Select Field Force'); $("#<%=ddlMR.ClientID%>").focus(); return false; }

                var fromdt = $("#<%=txtFromDate.ClientID%>").val();

                if (fromdt.length == 0) { alert('Select From Date'); $("#<%=txtFromDate.ClientID%> ").focus(); return false; }

                var todt = $("#<%=txtToDate.ClientID%> ").val();
                if (todt.length == 0) { alert('Select To Date'); $("#<%=txtToDate.ClientID%> ").focus(); return false; }

                var url = `rptCustomerPaymentDetail.aspx?sfCode=${mr_Code}&SubDiv=0&FYear=${fromdt}&FMonth=${todt}&sfName=${$("#<%=ddlMR.ClientID%> :selected").text()}`;
                window.open(url, "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=200,left=300,width=1200,height=600");
            });

        });
    </script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="form-group">
                <div class="row">
                    <label id="ddLdiv" class="col-md-2 col-md-offset-3 control-label">
                        Division</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control"
                                Width="150" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label id="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                        Manager</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group" id="kk" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"  CssClass="form-control"
                                Width="350">
                            </asp:DropDownList>



                        </div>
                    </div>
                </div>

                <div class="row">
                    <label id="lblddlMR" class="col-md-2 col-md-offset-3 control-label">
                        Field Force</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group" id="Div3" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlMR" runat="server" CssClass="form-control"
                                Width="350">
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
                            <%-- <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datepicker"
                            style="min-width: 110px; width: 120px;" />--%>
                            <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker" autocomplete="off" style="min-width: 110px; width: 150px;" />

                        </div>
                    </div>
                </div>
                <div class="row">
                    <label for="txtToDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                        To Date</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group" id="Div2" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input type="text" name="txtToDate" runat="server" id="txtToDate" class="form-control datetimepicker"
                                style="min-width: 110px; width: 150px;" autocomplete="off" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-md-offset-5">
                        <a id="btnView" class="btn btn-primary" runat="server"
                            style="vertical-align: middle; width: 100px;">View</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

