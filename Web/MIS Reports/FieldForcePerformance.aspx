<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FieldForcePerformance.aspx.cs" Inherits="MIS_Reports_FieldForcePerformance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        </head>
        <body>
            <form id="form1" runat="server">

                <div class="card" style="width:100%">
                    <div class="card-body">
                        <div class="row">
                            <label id="Label2" class="col-md-1  col-md-offset-4  control-label">
                                Division</label>
                            <div class="col-md-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    
                                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                                        Width="150" Visible="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <label for="ddlFF" class="col-md-1 col-md-offset-4 control-label">
                                Field Force</label>
                            <div class="col-md-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false"
                                        CssClass="form-control chosen" Width="350" Visible="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <label id="Label3" class="col-md-1 col-md-offset-4 control-label">
                                From</label>
                            <div class="col-sm-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input name="TextBox1" id="fdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                                    <asp:TextBox ID="txtfromdate" Text="" runat="server" CssClass="hidden"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                                To</label>
                            <div class="col-sm-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input name="TextBox1" id="tdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                                     <asp:TextBox ID="txttodate" Text="" runat="server" CssClass="hidden"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6 col-md-offset-5">
                                <a id="btnGo" class="btn btn-primary" onclick="NewWindow().this"
                                    style="vertical-align: middle; width:auto">
                                    <span>View</span></a>

                                <asp:Button ID="btnExcel" Text="Excel" CssClass="btn btn-primary" runat="server" OnClientClick="return CheckField()" OnClick="btnExcel_Click" />                                
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>            
            
            <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    
                    $('#<%=ddlFieldForce.ClientID%>').chosen();
                    $('#<%=subdiv.ClientID%>').chosen();

                     var Div = '<%=Session["div_code"]%>';

                    if (Div == "98") {
                        $('#<%=btnExcel.ClientID%>').show();
                    }
                    else { $('#<%=btnExcel.ClientID%>').hide(); } 

                 });

                function NewWindow() {
                   if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                        alert("Select FieldForce.");
                        $('#<%=ddlFieldForce.ClientID%>').focus();
                        return false;
                    }

                   
                    var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
                    var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();

                  
                    var fdate = $('#fdatee').val();
                    if (fdate == '') {
                        alert('Select the From date');
                        return false;
                    }
                    var tdate = $('#tdatee').val();
                     if (tdate == '') {
                        alert('Select the To date');
                        return false;
                    }
                    var sub_div_code = $('#<%=subdiv.ClientID%>').val();

                    var Div = '<%=Session["div_code"]%>';

                    if (Div == "98") {
                        window.open("Rpt_FieldPerformance_Aachi.aspx?&Fdate=" + fdate + "&Tdate=" + tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
                    }
                    else {
                        window.open("Rpt_FieldPerformance.aspx?&Fdate=" + fdate + "&Tdate=" + tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
                    }
                }

                function CheckField() {
                    if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                        alert("Select FieldForce.");
                        $('#<%=ddlFieldForce.ClientID%>').focus();
                        return false;
                    }

                    if ($('#<%=txtfromdate.ClientID%>').val().toString() == "") {
                        alert("Select From Date !!");
                        $('#fdatee').focus();
                         return false;
                    }

                    if ($('#<%=txttodate.ClientID%>').val().toString() == "") {
                        alert("Select To Date !!");
                        $('#tdatee').focus();
                        return false;
                    }
                }

                $("#fdatee").on("input", function (e) {
                    var input = $(this);
                    var fval = input.val();

                    $('#<%=txtfromdate.ClientID%>').val(fval);
                  
                });

                $("#tdatee").on("input", function (e) {
                    var input = $(this);
                    var fval = input.val();
                    
                    $('#<%=txttodate.ClientID%>').val(fval);

                });

            </script>
        </body>
    </html>   
</asp:Content>