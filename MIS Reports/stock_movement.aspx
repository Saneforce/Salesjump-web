<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup ="true" CodeFile="stock_movement.aspx.cs" Inherits="MIS_Reports_stock_movement" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <form id="formid" class="form-horizontal" runat="server">
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
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ff_SelectedIndexChanged"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
              <div class="row">
                <label for="ddlFF" class="col-md-1 col-md-offset-4 control-label">
                    Distributor</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddldist" runat="server" AutoPostBack="true"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
              <div class="row" style="display:none">
                <label id="Label3" class="col-md-1 col-md-offset-4 control-label">
                    From</label>
                <div class="col-sm-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="fdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                    </div>
                </div>

            </div>
            <div class="row"  style="display:none">
                <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                    To</label>
                <div class="col-sm-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="tdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                    </div>
                </div>

            </div><br />
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary" onclick="NewWindow().this"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>
            </div>
              </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var str = "";
        var arr = [];
        var prods = [];
        var clos = [];
       
        $(document).ready(function () {
          
          
        });
       
      
        function NewWindow() {
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                alert("Select FieldForce.");
                $('#<%=ddlFieldForce.ClientID%>').focus();
                return false;
            }
            var fieldforce = $('#<%=ddlFieldForce.ClientID%>').val();
            var disname = $('#<%=ddldist.ClientID%>').val();
            var stName = $('#<%=ddldist.ClientID%> :selected').text();
            var fdate = $('#fdatee').val();
            var tdate = $('#tdatee').val();
            window.open("view_stock_movement.aspx?&sfcode=" + fieldforce + "&stckcode=" + disname + "&stckName=" + stName + "&Fdate=" + fdate + "&Tdate=" + tdate, 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
          }
   
    </script>

    </asp:Content>