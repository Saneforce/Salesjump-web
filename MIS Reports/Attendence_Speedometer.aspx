<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Attendence_Speedometer.aspx.cs" Inherits="MIS_Reports_Attendence_Speedometer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
		<div style="font-weight: bold;font-size: 16px;"> Attendance SpeedoMeter</div>
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-1  col-md-offset-4  control-label">
                    Division</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            Width="150">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="ddlFF" class="col-md-1 col-md-offset-4 control-label">
                    Field Force</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="Label3" class="col-md-1 col-md-offset-4 control-label">
                    From</label>
                <div class="col-sm-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="fdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                    </div>
                </div>

            </div>
            <div class="row">
                <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                    To</label>
                <div class="col-sm-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="tdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary" onclick="NewWindow().this"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function NewWindow() {
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                alert("Select FieldForce.");
                $('#<%=ddlFieldForce.ClientID%>').focus();
                return false;
            }
            var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var fdate = $('#fdatee').val();
            var tdate = $('#tdatee').val();
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            window.open("Attendence_Speedometer_view.aspx?&Fdate=" + fdate + "&Tdate=" + tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
    </script>
</asp:Content>
