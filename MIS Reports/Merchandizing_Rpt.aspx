<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Merchandizing_Rpt.aspx.cs" Inherits="MIS_Reports_Merchandizing_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
            <style>
                .chosen{
                    height: 31px;
                }
            </style>
        </head>
        <body>
            <form id="form1" runat="server">
                <h3>Merchandising Activity</h3>
                <div  style="width:100%">
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
                            
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>            
            
            <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        var Fdate = '', Tdate = '';
        $(document).ready(function () {
             $('#<%=ddlFieldForce.ClientID%>').chosen();
                    $('#<%=subdiv.ClientID%>').chosen();
            
        });
        function NewWindow() {
            sf_code = $('#ddFieldf option:selected').val();
		var FO = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FO == "---Select Field Force---") { alert("Select FieldForce"); $('#salesforcelist').focus(); return false; }
            var Fdate = $('#fdatee').val();
                    if (Fdate == '') {
                        alert('Select the From date');
                        return false;
                    }
                    var Tdate = $('#tdatee').val();
                     if (Tdate == '') {
                        alert('Select the To date');
                        return false;
                    }
                
                var subdivt = $('#<%=subdiv.ClientID%> :selected').text();
                //if (subdivt == "--Select--") { alert("Select Division."); $('#subdiv').focus(); return false; }
                
                if (Fdate == '') { alert('Select From Date'); return false; }
                if (Tdate == '') { alert('Select To Date'); return false; }
                var frmdate = new Date($('#fdatee').val());
                var todate = new Date($('#tdatee').val());
            var sfcode = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
                var datediff = (todate - frmdate) / (1000 * 60 * 60 * 24);
                if (datediff > 180) {
                    alert('Selected Date With in 180days');
                }
                else {
                    $('#loadover').show();
                    window.open("Merchand_Inout.aspx?&Fdate=" + Fdate + "&Tdate=" + Tdate + "&subdiv=" + subdiv + "&sfCode=" + sfcode + "&sfname=" + FO ,
                        null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                }
        }
       </script>
        </body>
    </html>   
</asp:Content>

