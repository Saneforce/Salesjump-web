<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Productwise_SuperStockist_Order.aspx.cs" Inherits="MIS_Reports_Productwise_SuperStockist_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <div class="col-lg-12 sub-header">
            Productwise SuperStockist Order <span style="float: right; margin-right: 15px;"></span>
        </div>
        <div class="container" style="min-width:100%; font-size: 18px";>
            <div class="row"style="margin-top:80px">
                <label for="ddlFF" class="col-md-1 col-md-offset-4 control-label" >
                    Field Force</label>
                <div class="col-md-6 inputGroupContainer"style="width:25%">
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
                <div class="col-sm-5 inputGroupContainer"style="width:15%">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="fdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                    </div>
                </div>

            </div>
            <div class="row">
                <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                    To</label>
                <div class="col-sm-5 inputGroupContainer"style="width:15%">
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
            if (fdate == "") { alert("Select From Date.");$('#fdatee').focus(); return false;}
            var tdate = $('#tdatee').val();
            if (tdate == "") { alert("Select To Date.");$('#tdatee').focus(); return false;}
            window.open("rpt_Productwise_SuperStockist_Order.aspx?&Fdate=" + fdate + "&Tdate=" + tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "", 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
    </script>
</asp:Content>

              