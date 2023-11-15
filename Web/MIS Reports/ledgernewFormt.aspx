<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="ledgernewFormt.aspx.cs" Inherits="MIS_Reports_ledgernewFormt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>           
    
            <link rel="stylesheet" type="text/css" media="all" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
            <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

            <style type="text/css">
                input[type='text'], select, label {
                    line-height: 22px;
                    padding: 4px 6px;
                    font-size: medium;
                    border-radius: 7px;
                    width: 100%;
                }

                thead th {
                    border: 1px solid #ececec;
                }

                tbody td {
                    text-align: right;
                }
                
                tbody td:first-child {
                    text-align: left;
                }
				
            </style>
			<style>
			.open>.dropdown-menu
				{
				min-height: 110px !important;;
				}
			</style>
            
            <script type="text/javascript">

                function FFilter() {
                    var cstate = $("#<%=ddlState.ClientID%>").val() || 0;
                    var cdiv = $("#<%=subdiv.ClientID%>").val() || 0;
                    $('#<%=ddlFieldForce.ClientID%>').selectpicker('destroy');
                    var filsf = [];
                    if (cstate == 0) {
                        filsf = AllFF.filter(function (a) {
                            return ((',' + (a.subdivision_code)).indexOf(',' + cdiv + ',')) > -1
                        });
                    }
                    else if (cdiv == 0) {
                        filsf = AllFF.filter(function (a) {
                            return a.State_Code == cstate;
                        });
                    }
                    else if (cdiv != 0 && cstate != 0) {
                        filsf = AllFF.filter(function (a) {
                            return (a.State_Code == cstate) && (((',' + (a.subdivision_code)).indexOf(',' + cdiv + ',')) > -1);
                        });
                    }
                    else {
                        filsf = AllFF.filter(function (a) {
                            return (a.Sf_Code != 'admin');
                        });
                    }
                    var dept = $("#<%=ddlFieldForce.ClientID%>");
                    dept.empty().append('<option selected="selected" value="">Select FieldForce</option>');
                    if ("<%=sf_type%>" != "2") {
                        dept.append('<option value="admin">Admin</option>');
                    }
                    if (filsf.length > 0) {
                        for (var i = 0; i < filsf.length; i++) {
                            dept.append($('<option value="' + filsf[i].Sf_Code + '">' + filsf[i].Sf_Name + '</option>'))
                        }
                    }
                    $("#<%=ddlFieldForce.ClientID%>").selectpicker({
                        liveSearch: true
                    });
                }

                function loadStates() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ledgernewFormt.aspx/getStates",
                        data: '{"divcode":"<%=Session["div_code"]%>"}',
                        dataType: "json",
                        success: function (data) {
                            AllState = JSON.parse(data.d) || [];
                            States = AllState;
                            if (States.length > 0) {
                                var dept = $("#<%=ddlState.ClientID%>");
                                dept.empty().append('<option selected="selected" value="0">Select State</option>');
                                for (var i = 0; i < States.length; i++) {
                                    dept.append($('<option value="' + States[i].State_code + '">' + States[i].StateName + '</option>'))
                                }
                            }
                        }
                    });
                    $("#<%=ddlState.ClientID%>").selectpicker({
                        liveSearch: true
                    });
                }

                function loadDivision(ssdiv) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ledgernewFormt.aspx/getDivision",
                        data: '{"divcode":"<%=Session["div_code"]%>"}',
                        dataType: "json",
                        success: function (data) {
                            AllDiv = JSON.parse(data.d) || [];
                            Div = AllDiv;
                            if (Div.length > 0) {
                                var dept = $("#<%=subdiv.ClientID%>");
                                dept.empty().append('<option selected="selected" value="0">Select Division</option>');
                                for (var i = 0; i < Div.length; i++) {
                                    dept.append($('<option value="' + Div[i].subdivision_code + '">' + Div[i].subdivision_name + '</option>'))
                                }
                            }
                        }
                    });

                    $("#<%=subdiv.ClientID%>").selectpicker({
                        liveSearch: true
                    });
                }

                function loadFieldForce() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "ledgernewFormt.aspx/getFieldForce",
                        data: '{"divcode":"<%=Session["div_code"]%>","sfcode":"<%=Session["Sf_Code"]%>"}',
                        dataType: "json",
                        success: function (data) {
                            AllFF = JSON.parse(data.d) || [];
                            SFF = AllFF;
                            if (SFF.length > 0) {
                                var dept = $("#<%=ddlFieldForce.ClientID%>");
                                dept.empty().append('<option selected="selected" value="0">Select FieldForce</option>');
                                for (var i = 0; i < SFF.length; i++) {
                                    dept.append($('<option value="' + SFF[i].Sf_Code + '">' + SFF[i].Sf_Name + '</option>'))
                                }
                            }
                        }
                    });

                    $("#<%=ddlFieldForce.ClientID%>").selectpicker({
                        liveSearch: true
                    });
                }

                $(document).ready(function () {
                    loadDivision();
                    loadStates();
                    loadFieldForce();

                     $("#<%=ddlType.ClientID%>").selectpicker({
                        liveSearch: true
                    });

                    $('#<%=subdiv.ClientID%>').on('change', function () {
                        FFilter();
                    });

                    $('#<%=ddlState.ClientID%>').on('change', function () {
                        FFilter();
                    });

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

                    var divcode = <%=Session["div_code"]%>;

                    $(document).on('click', '.btnview', function () {

                        if (divcode == "150") {
                            var stcode = $("#<%=ddlState.ClientID%>").val();
                            if (stcode == "" || stcode == "0") {
                                alert('Select State');
                                $("#<%=ddlState.ClientID%>").focus();
                                return false;
                            }
                            var StName = $("#<%=ddlState.ClientID%>  option:selected").text();
                            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                            var sf_Name = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();

                            if ((sf_Name == "---Select Field Force---" || sf_Name == "Select FieldForce")) {
                                alert('Select Field Force');
                                $("#<%=ddlFieldForce.ClientID%>").focus();
                                return false;
                            }

                            var fromdate = $('#fromDate').val();

                            if (fromdate.length <= 0) {
                                alert('Enter From Date..!');
                                $('#fromDate').focus();
                                return false;
                            }

                            var todate = $('#toDate').val();

                            if (todate.length <= 0) {
                                alert('Enter From Date..!');
                                $('#toDate').focus();
                                return false;
                            }

                            var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                            var type = $("#<%=ddlType.ClientID%>").val();
                                    
                            if (type == "1") {

                                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                                var str = 'rptEmployeewiseorderDetails.aspx?&SFCode=' + sf_code + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode + '&stcode=' + stcode + '&StName=' + StName;
                                window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
                            }
                            else {
                                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                                var str = 'rptEmployeewiseCategoryDetails.aspx?&SFCode=' + sf_code + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode + '&stcode=' + stcode + '&StName=' + StName;
                                window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');

                            }
                        }
                        else {

                            var stcode = $("#<%=ddlState.ClientID%>").val();
                            <%--if (stcode == "" || stcode == "0") {
                                alert('Select State');
                                $("#<%=ddlState.ClientID%>").focus();
                                return false;
                            }--%>
                            var StName = $("#<%=ddlState.ClientID%>  option:selected").text();
                            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                            var sf_Name = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();

                            if ((sf_Name == "---Select Field Force---" || sf_Name == "Select FieldForce")) {
                                alert('Select Field Force');
                                $("#<%=ddlFieldForce.ClientID%>").focus();
                                return false;
                            }

                            var fromdate = $('#fromDate').val();

                            if (fromdate.length <= 0) {
                                alert('Enter From Date..!');
                                $('#fromDate').focus();
                                return false;
                            }

                            var todate = $('#toDate').val();

                            if (todate.length <= 0) {
                                alert('Enter From Date..!');
                                $('#toDate').focus();
                                return false;
                            }

                            var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                            var type = $("#<%=ddlType.ClientID%>").val();
                                    
                            if (type == "1") {

                                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                                var str = 'rptEmployeewiseorderDetails.aspx?&SFCode=' + sf_code + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode + '&stcode=' + stcode + '&StName=' + StName;
                                window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
                            }
                            else {
                                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                                var str = 'rptEmployeewiseCategoryDetails.aspx?&SFCode=' + sf_code + '&FYear=' + fromdate + '&FMonth=' + todate + '&SFName=' + SFName + '&SubDiv=' + SubDivCode + '&stcode=' + stcode + '&StName=' + StName;
                                window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');

                            }

                        }
                    });

                });
            </script>
        </head>
        <body>
            <form id="form1" runat="server">
                
                        <div class="container" style="width: 100%">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-8">
                                         <div class="col-lg-2 col-lg-offset-4" style="float:left;width:150px !important;">
                                             <label>Division</label>
                                         </div>
                                        <div class="col-lg-6 inputGroupContainer" style="float:right;margin-top:-40px !important;">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                                <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          
                            <br />
                             <div class="row">
                                 <div class="col-lg-12">
                                    <div class="col-lg-8">
                                         <div class="col-lg-2 col-lg-offset-4" style="float:left;width:150px !important;">
                                             <label>State</label>
                                         </div>
                                        <div class="col-lg-6 inputGroupContainer" style="float:right;margin-top:-40px !important;">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                                <asp:DropDownList ID="ddlState" SkinID="ddlRequired" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                            </div>                        
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-8">
                                         <div class="col-lg-2 col-lg-offset-4" style="float:left;width:150px !important;">
                                             <label for="ddlFieldForce">Field Force</label>
                                         </div>
                                        <div class="col-lg-6 inputGroupContainer" style="float:right;margin-top:-40px !important;">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                                <asp:DropDownList ID="ddlFieldForce" SkinID="ddlFieldForce" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                      
                            </div>
                          
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-8">
                                         <div class="col-lg-2 col-lg-offset-4" style="float:left;width:150px !important;">
                                             <label for="fromDate">From Date</label>
                                         </div>
                                        <div class="col-lg-6 inputGroupContainer" style="float:right;margin-top:-40px !important;">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                <input type="text" name="fromDate" id="fromDate" class="form-control datePicker"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                     
                            </div>
                          
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-8">
                                         <div class="col-lg-2 col-lg-offset-4" style="float:left;width:150px !important;">
                                             <label for="toDate">To Date</label>
                                         </div>
                                        <div class="col-lg-6 inputGroupContainer" style="float:right;margin-top:-40px !important;">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                <input type="text" name="toDate" id="toDate" class="form-control datePicker" />
                                            </div>
                                        </div>
                                    </div>
                                </div>                                       
                            </div>                         
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-8">
                                         <div class="col-lg-2 col-lg-offset-4" style="float:left;width:150px !important;">
                                             <label for="ddlType">Type</label>
                                         </div>
                                        <div class="col-lg-6 inputGroupContainer" style="float:right;margin-top:-40px !important;">
                                            <div class="input-group">
                                                 <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Product"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Category"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                       
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6 col-md-offset-5">
                                    <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>
                                </div>
                            </div>
                            <br />
                        </div>
                             
            </form>
        </body>
    </html>    
</asp:Content>
