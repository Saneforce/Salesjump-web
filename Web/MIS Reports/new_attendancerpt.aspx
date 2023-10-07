<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="new_attendancerpt.aspx.cs" Inherits="MIS_Reports_new_attendancerpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    <div>
   <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
            <br />
            <d class="container" style="width:100%">
        <div class="row">
         <label id="Label1" class="col-md-2  col-md-offset-3  control-label">Division</label>
         <div class="col-md-3 inputGroupContainer">
        <div class="input-group">
        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" 
             onselectedindexchanged="subdiv_SelectedIndexChanged" Width="200px" AutoPostBack="true" >
             </asp:DropDownList>
                    </div>
                    </div>
                    </div>
					<div class="row">
                            <label id="st" class="col-md-2  col-md-offset-3  control-label">
                                State</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
			   <div class="row">
             <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                Field Force</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                   <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control"  AutoPostBack="true"
                       Width="360px" >   </asp:DropDownList>
               
                    </div>
                            </div>
                            </div>

			<div class="row">
            <label id="lblFMonth" class="col-md-2  col-md-offset-3  control-label">
                Month</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span> 
                        <asp:DropDownList ID="ddlFMonth" runat="server"  CssClass="form-control"  Width="100">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>

                        </div>
                        </div>
                        </div>
						 <div class="row">
              <label id="lblFYear" class="col-md-2  col-md-offset-3  control-label">
                Year</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>                         
                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control"  Width="100">
                        </asp:DropDownList>
                   </div>
                    </div>
                    </div>

   <div class="row">
            <div class="col-md-6  col-md-offset-5">
         <button id="btnGo"  runat="server" onclick="NewWindow().this" class="btn btn-primary btnview" style="width: 100px">
                <span>View</span></button>
      <asp:Label ID="lblpath" runat="server" ForeColor="#f1f2f7"  ></asp:Label>
               </div>
                 </div>
                    

          </ContentTemplate>
              </asp:updatepanel>
       
    </div>
    </form>
    <script type="text/javascript">
        function NewWindow() {
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }

            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            if (subdiv == "--- Select ---") { alert("Select Subdivision."); $('#subdiv').focus(); return false; }

            var statc = $('#<%=ddlstate.ClientID%> :selected').val();
            var statv = $('#<%=ddlstate.ClientID%> :selected').text();


            var FO = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (FO == "--Select--") { alert("Select salesforce"); $('#salesforcelist').focus(); return false; }

            var sfcode = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();

            var imgpth = $('#ctl00_ContentPlaceHolder1_lblpath').text();


            window.location.href = "Monthly_Attendance_Report.aspx?&FMonth=" + FMonth + "&FYear=" + FYear + "&subdiv=" + subdiv + "&stcode=" + statc + "&stval=" + statv + "&sfCode=" + sfcode + "&sfname=" + FO + "";
       
    }
</script>
</body>
</html>
    </asp:Content>
