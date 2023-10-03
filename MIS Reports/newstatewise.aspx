<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="newstatewise.aspx.cs" Inherits="MIS_Reports_newstatewise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head><meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' /></head>
    <form id="form1" runat="server">
        <div>
           <div class="row">
                            <label id="Label5" class="col-md-1 col-md-offset-4  control-label">
                                State</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
            <div class="row">
                <label id="lblFF" class="col-md-1 col-md-offset-4 control-label">
                Manager:</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group" id="kk" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlmgr" runat="server" OnSelectedIndexChanged="ddlmgr_SelectIndexchanged" AutoPostBack="true"   CssClass="form-control" Width="350"></asp:DropDownList> </div>
                </div>
            </div>
             <div class="row">
                <label id="lblMR" runat="server" class="col-md-1 col-md-offset-4 control-label">
                Field Force:</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlfieldforce" runat="server"  CssClass="form-control"
                            Width="350"></asp:DropDownList> </div>
                </div>
            </div>
          
           <div class="row">
            <label id="lblFYear" class="col-md-1 col-md-offset-4  control-label">
                Year</label>
                  <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>                         
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control" Style="min-width: 100px" Width="150">
                        </asp:DropDownList>
                   </div>
                  </div>
                   </div>
            <div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color:#1a73e8;color:white;">View</button>
            </div>
        </div>
        </div>
    </form>
 <script type="text/javascript">
     $(document).ready(function () {
         
     });
  
     $('#btnview').on('click', function () {
        
         var state = $('#<%=ddlstate.ClientID%>').val();
		  if (state == 0) { alert("Select State."); $('#ddlstate').focus(); return false; }
         var selstatev =$('#<%=ddlstate.ClientID%> :selected').text();
         var div =<%=Session["div_code"]%>;
         var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
         if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
         var mgrv = $('#<%=ddlmgr.ClientID%>').val();
         var mgrn = $('#<%=ddlmgr.ClientID%> :selected').text();
         var ffv = $('#<%=ddlfieldforce.ClientID%>').val();
         var ffn = $('#<%=ddlfieldforce.ClientID%> :selected').text();
         var url = '';
       
         url = "rpt_newstatewise.aspx?state=" + state + "&statev=" + selstatev + "&divcode=" + div + "&fyear=" + FYear + "&mgrval=" + mgrv + "&mgrnm=" + mgrn + "&ffval=" + ffv + "&ffnm=" + ffn;
                    window.open(url, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
              
      });
        
     </script>
</html>
</asp:Content>

