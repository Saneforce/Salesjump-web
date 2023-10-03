<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="CallCompliance.aspx.cs" Inherits="MIS_Reports_CallCompliance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="form1" runat="server">
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
                       <%-- <input name="TextBox1" id="fdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />--%>
                        <asp:TextBox ID="txtfdatee" TextMode="Date" runat="server" CssClass="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                    </div>
                </div>

            </div>
            <div class="row">
                <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                    To</label>
                <div class="col-sm-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        
                        <%--<input name="TextBox1" id="tdatee"  type="date" class="form-control" style="min-width: 200px; width: 250px" pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />--%>
                         <asp:TextBox ID="txttdatee" TextMode="Date" runat="server" CssClass="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}"  />

                    </div>
                </div>

            </div>
            <div class="row">
                <br />
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary btnview" onclick="NewWindow().this"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                    <div class="col-md-3  btnexcel">
                        <asp:Button runat="server" ID="exceldld" CssClass="btn btnexcel" BackColor="#1a73e8" ForeColor="White" Text="Excel" OnClientClick="return Validation();" OnClick="exceldld_Click" />
                    </div>
                </div>
                
            </div>
        </div>
    </form>
	
    <script type="text/javascript">
	    
		$(document).ready(function () {

            var divCode = '<%=Session["div_code"]%>';
            //alert(divCode);

            if (divCode == "173" || divCode == "160" || divCode == "159" || divCode == "178") {
                $('.btnexcel').show();
                $('.btnview').show();
            }
            else {
                $('.btnexcel').hide();
                $('.btnview').show();
            }

        });

      
        function Validation() {
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                alert("Select FieldForce  !!");
                $('#<%=ddlFieldForce.ClientID%>').focus();
                return false;
            }

            var fdate = "";
            fdate = $('#<%=txtfdatee.ClientID%>').val();
            //alert(fdate);
            //var fdate = $('#fdatee').val();

            if (fdate == "" || fdate == null) {
                alert("Select From Date !!");
                $('#<%=txtfdatee.ClientID%>').focus();
                return false;
            }



            var tdate = $('#<%=txttdatee.ClientID%>').val();

            if (tdate.toString() == "" || tdate == null) {
                alert("Select To Date !!");
                $('#<%=txttdatee.ClientID%>').focus();
                return false;
            }
        }
	    
	
	    
        function NewWindow() {
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                alert("Select FieldForce.");
                $('#<%=ddlFieldForce.ClientID%>').focus();
                return false;
            }
            var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();

            var fdate = "";
            
			  if ($('#<%=txtfdatee.ClientID%>').val().toString() == "") {
                alert("Select From Date !!");
                $('#<%=txtfdatee.ClientID%>').focus();
                return false;
            }
			
            //var fdate = $('#fdatee').val();

            fdate = $('#<%=txtfdatee.ClientID%>').val();
            //alert(fdate);

            var tdate = "";
            if ($('#<%=txttdatee.ClientID%>').val().toString() == "") {
                alert("Select To Date !!");
                 $('#<%=txttdatee.ClientID%>').focus();
                 return false;
             }
            //var tdate = $('#tdatee').val();

            tdate = $('#<%=txttdatee.ClientID%>').val();

            //var fdate = $('#fdatee').val();
            //var tdate = $('#tdatee').val();
			
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            window.open("RptCallCompliance.aspx?&Fdate=" + fdate + "&Tdate=" + tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
    </script>
</asp:Content>