<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="mgr_daysummary.aspx.cs" Inherits="MIS_Reports_mgr_daysummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head><meta charset="utf-8" />
        <name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' /></head>
    <form id="form1" runat="server">
        <div>
             <div class="row">
                <label id="lblFF" class="col-md-2 col-md-offset-3 control-label">
                Manager:</label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group" id="kk" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:DropDownList ID="ddlmgr" runat="server"  AutoPostBack="true"   CssClass="form-control" Width="350"></asp:DropDownList> </div>
                </div>
            </div>
                 <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="fdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="tdate" />
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
        
         var div =<%=Session["div_code"]%>;
         var mgrv = $('#<%=ddlmgr.ClientID%>').val();
         var mgrn = $('#<%=ddlmgr.ClientID%> :selected').text();
         var fdate = $('#fdate').val();
         if (fdate == '') {
             alert('Select the From Date');
             valid = false;
             return false;
         }
         var tdate = $('#tdate').val();
         if (tdate == '') {
             alert('Select the To Date');
             valid = false;
             return false;
         }
         var url = '';
       
         url = "view_mgr_daysummary.aspx?divcode=" + div + "&mgrval=" + mgrv + "&mgrnm=" + mgrn + "&fdt=" + fdate + "&tdt=" + tdate;
                    window.open(url, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
              
      });
        
     </script>
        </html>
</asp:Content>