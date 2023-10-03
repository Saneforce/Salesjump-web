<%@ Page Title="Top 10 Exception" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Top10_Exception.aspx.cs" Inherits="MIS_Reports_Top10_Exception" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>

<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Top 10 Exception</title>
    
    <style type="text/css">
        input[type='text'], select, label
        {
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
     <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type = "text/javascript">
        var popUpObj;
        function showModalPopUp(FYear,viewby, viewtop) {

            popUpObj = window.open("rpttop10_exception.aspx?FYear=" + FYear + "&viewby=" + viewby+"&viewtop=" + viewtop,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }

</script>
<script type="text/javascript">
        function NewWindow() {
 var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
 var viewtop = $('#<%=topdrop.ClientID%> :selected').text();
                if (viewtop == "--- Select ---") { alert("Select viewtop"); $('#topdrop').focus(); return false; }
                var viewby = $('#<%= viewdrop.ClientID%> :selected').text();
                if (viewby == "--- Select ---") { alert("Select viewby"); $('#viewdrop').focus(); return false; }
           
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var viewtop = $('#<%=topdrop.ClientID%> :selected').text();
            
            var viewby = $('#<%=viewdrop.ClientID%> :selected').text();
            window.open("rpttop10_exception.aspx?FYear=" + FYear + "&viewby=" + viewby + "&viewtop=" + viewtop, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0'); 
        }
</script>

 <%--   <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>--%>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
      
  
</head>
<body>
    <form id="form1" runat="server">
    
           <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
         <%--        <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>--%>
            
            
            <div class="container" style="width: 100%">
                    <div class="form-group">
                        <div class="row">
                        <label id="Top" class="col-md-1 col-md-offset-4 control-label">
                                Top</label>
                    <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                 <span class="input-group-addon"><i class="glyphicon glyphicon-asterisk	"></i></span>
                        <asp:DropDownList ID="topdrop" runat="server"  SkinID="ddlRequired"  CssClass="form-control"
                                        Style="min-width: 100px" Width="130"       >
                              <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
                          <asp:ListItem Value="1" >5</asp:ListItem>
                          <asp:ListItem Value="2" >10</asp:ListItem>
                            <asp:ListItem Value="3" >15</asp:ListItem>
                              <asp:ListItem Value="4" >20</asp:ListItem>
                                <asp:ListItem Value="5" >25</asp:ListItem>
                        </asp:DropDownList>
                     </div>
                    </div>
                    </div>
                    <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                                ViewBy</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-eye-open"></i></span>
                       
                   
                      <asp:DropDownList ID="viewdrop" runat="server" AutoPostBack="true" SkinID="ddlRequired" style="min-width: 100px" Width="130"  CssClass="form-control">
                             <asp:ListItem Value="0" Text="--- Select ---" Selected="True"></asp:ListItem>
                              <asp:ListItem Value="6" Text="AreaWise" ></asp:ListItem>
                              <asp:ListItem Value="2" Text="BrandWise" ></asp:ListItem>                                     
                              <asp:ListItem Value="3" Text="CategoryWise" ></asp:ListItem>
                            <asp:ListItem Value="1" Text="DistributorWise"></asp:ListItem>                                               
                             <asp:ListItem Value="4" Text="ProductWise" ></asp:ListItem>
                             <asp:ListItem Value="5" Text="StateWise"></asp:ListItem>
                              <asp:ListItem Value="8" Text="TerritoryWise" ></asp:ListItem>
                              <asp:ListItem Value="7" Text="ZoneWise" ></asp:ListItem>

                        </asp:DropDownList>
               
                    </div>
                            </div>
                        </div>
                <div class="row">
                 <label id="lblFYear" class="col-md-1 col-md-offset-4 control-label">
                                Year</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                      
                    
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" AutoPostBack="false" CssClass="form-control"
                        Style="min-width: 100px" Width="100">
                        </asp:DropDownList>
                    </div>
                            </div>
                        </div>
                
            
           
             <div class="row">
                            <div class="col-md-6 col-md-offset-5">
            <button  ID="btnGo"  runat="server"  class="btn btn-primary"   onclick="NewWindow().this"  style="width: 100px" ><span>View</span></button>
            </div>
            </div>
            </div></div>
  			<%--</ContentTemplate>
              </asp:updatepanel>--%>
    
            
          
    </form>
</body>
</html>
</asp:Content>