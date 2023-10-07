<%@ Page Title="Field Force Wise UOM Report" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FFWiseUOMReport.aspx.cs" Inherits="MIS_Reports_FFWiseUOMReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
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
            $("#<%=txtFromDate.ClientID%>").val(today);
            $("#<%=txtToDate.ClientID%> ").val(today);

            $(document).on('click', '.btn', function () {

                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var FromDt = $("#<%=txtFromDate.ClientID%>").val();
                var ToDt = $("#<%=txtToDate.ClientID%> ").val();

                var sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                var sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();


                if (sf_code == '0') {
                    alert('Select manager..!');
                    $("#<%=ddlFieldForce.ClientID%>").focus();
                    return false;
                }


                if ($('#mgrOnly').is(":checked")) {

                    var mgronly = 1;

                    sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                    sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                    if (sf_code == '0') {
                        alert('Select manager..!');
                        $("#<%=ddlFieldForce.ClientID%>").focus();
                        return false;
                    }
                }
                else {
                    var mgronly = 0;
                    sf_code = $("#<%=ddlMR.ClientID%> ").val();
                    sf_Name = $("#<%=ddlMR.ClientID%> :selected").text();
                    if (sf_code == '0' || sf_code == '') {
                        <%--alert('Select Field Force..!');
                        $("#<%=ddlMR.ClientID%>").focus();
                        return false;--%>
                        sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                        sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                    }
                }

                url = 'rptFFWiseUOMReport.aspx?&FYear=' + FromDt + '&FMonth=' + ToDt + '&SubDiv=' + SubDivCode + '&SfCode=' + sf_code + '&sfName=' + sf_Name + '&mgronly=' + mgronly;
                window.open(url, 'UOMReport', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
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
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="150"
                            AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Manager</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        Manager Only
                   
                        <input type="checkbox" id="mgrOnly" />
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="lblMR" runat="server" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlMR" runat="server" CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtFromDate" class="col-md-2 col-md-offset-3 control-label">
                    From Date
           
                </label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div1" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker"
                            autocomplete="off" style="min-width: 110px; width: 120px;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtToDate" class="col-md-2 col-md-offset-3 control-label">
                    To Date
           
                </label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div2" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="text" name="txtToDate" runat="server" id="txtToDate" class="form-control datetimepicker"
                            style="min-width: 110px; width: 120px;" autocomplete="off" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-11" style="text-align: center">
                    <a id="btnGo" class="btn btn-primary" style="vertical-align: middle;"><span>View</span></a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

