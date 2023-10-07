<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Employee_wise_POrder.aspx.cs" Inherits="MIS_Reports_Employee_wise_POrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
        thead th
        {
            border: 1px solid #ececec;
        }
        
        
        tbody td
        {
            text-align: right;
        }
        
        
        tbody td:first-child
        {
            text-align: left;
        }
    </style>
     <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            $(document).on('click', '.btnview', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                var fromdate = $('#fromDate').val();
                if (fromdate.length <= 0) { alert('Enter From Date..!'); $('#fromDate').focus(); return false; };
                var todate = $('#toDate').val();
                if (todate.length <= 0) { alert('Enter From Date..!'); $('#toDate').focus(); return false; };

                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var type = $("#<%=ddlType.ClientID%>").val();
                if (type == "1") {

                    var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                    var str = 'rpt_employee_wise_primary_order.aspx?&SFCode=' + sf_code + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                    window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
                }
                else {
                    var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                    var str = 'rptEmployeewisePrimaryCategoryDetails.aspx?&SFCode=' + sf_code + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                    window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
                
                }
            });

        });
    </script>
    <form id="form1" runat="server">
    <div class="container" style="width: 100%">
        <div class="row">
            <label id="Label2" class="col-md-2 col-md-offset-3  control-label" style="font-weight: normal">
                Division</label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Style="min-width: 150px"
                        Width="150" OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label" style="font-weight: normal">
                Field Force</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFieldForce" runat="server"   Width="350" 
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
         <div class="row">
        <label for="fromDate" class="col-md-2 col-sm-offset-3 control-label" style="font-weight: normal">
                From Date</label>
            <div class="col-md-2 inputGroupContainer">
             <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                <input type="text" name="fromDate" id="fromDate" class="form-control datePicker" style="width:150px" />
                  </div>
            </div>
            </div>
            <div class="row">
            <label for="toDate" class="col-md-2 col-sm-offset-3 control-label" style="font-weight: normal">
                To Date</label>
            <div class="col-md-2 inputGroupContainer">
             <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                <input type="text" name="toDate" id="toDate" class="form-control datePicker" style="width:150px" />
            </div>
            </div>
            </div>
        <div class="row">
            <label for="txtMonth" class="col-md-2 col-md-offset-3  control-label">
                Type</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired" CssClass="form-control"
                         Style="min-width: 100px" Width="100">
                        <asp:ListItem Value="1" Text="Product"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Category"></asp:ListItem>                        
                    </asp:DropDownList>
                </div>
            </div>
        </div>
       <%-- <div class="row">
            <label for="txtYear" class="col-md-2  col-md-offset-3  control-label">
                Year</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                        Style=" min-width: 100px" Width="100">
                    </asp:DropDownList>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-md-6 col-md-offset-5">
                <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">
                    View</a>
            </div>
        </div>
        <br />
    </div>
    </form>
</asp:Content>