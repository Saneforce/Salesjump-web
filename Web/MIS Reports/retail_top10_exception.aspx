<%@ Page Title="Retail Top 10 Exception" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="retail_top10_exception.aspx.cs" Inherits="MIS_Reports_retail_top10_exception" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Retail Top 10 Exception</title>
    <link type="text/css" rel="Stylesheet" href="../../css/style1.css" />
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <script type = "text/javascript">
        var popUpObj;
        function showModalPopUp(FYear, viewby, viewtop, route) {

            popUpObj = window.open("rpt_retail_top10_exception.aspx?FYear=" + FYear + "&viewtop=" + viewtop + " &route=" + route,
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


        var seltop = $('#<%=topdrop.ClientID%> :selected').text();
        if (seltop == "--Select--") { alert("Select Select Top"); $('#topdrop').focus(); return false; }


        var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
        if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }



        var distri = $('#<%=Distributor.ClientID%> :selected').text();
        if (distri == "--Select--") { alert("Select Distributor"); $('#Distributor').focus(); return false; }


        var route = $('#<%=route.ClientID%> :selected').text();
        if (route == "--Select--") { alert("Select Product"); $('#route').focus(); return false; }

        var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
        if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }



        var Dist_Name = $('#<%=Distributor.ClientID%> :selected').text();
        var Dist_Val = $('#<%=Distributor.ClientID%> :selected').val();

        var route_code = $('#<%=route.ClientID%> :selected').val();
        var route_name = $('#<%=route.ClientID%> :selected').text();

        var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();

        var viewtop = $('#<%=topdrop.ClientID%> :selected').text();

        window.open("rpt_retail_top10_exception.aspx?FYear=" + FYear + "&viewtop=" + viewtop + " &route=" + route_code + "&stockist_code=" + Dist_Val + "&stockist_name=" + Dist_Name + "&routee_name=" + route_name, "ModalPopUp", "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=0," + "width=900," + "height=600," + "left = 0," + "top=0");
    }
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
      
    
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <div id="Divid" runat="server">
        </div>
            <br />

 <div class="container" style="width: 100%">
                    <div class="form-group">
                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Top" class="col-md-1 col-md-offset-4 control-label">
                                Top</label>

                        <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>

                        <asp:DropDownList ID="topdrop" runat="server"   SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
                              <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                            <label id="Label4" class="col-md-1 col-md-offset-4  control-label">
                                Division</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                   <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired"  
                       AutoPostBack="true" CssClass="form-control" Style="min-width: 100px" Width="150"  onselectedindexchanged="subdiv_SelectedIndexChanged" >
                   </asp:DropDownList>
             
             </div>
             </div>
            </div>

             <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="dist_name" class="col-md-1 col-md-offset-4  control-label">
                                Distributor</label>
                                 <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                                 <asp:DropDownList ID="Distributor" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                       CssClass="form-control" Style="min-width: 150px" Width="210" 
                        onselectedindexchanged="Distributor_SelectedIndexChanged">
                   </asp:DropDownList>

                   </div>
                   </div>
                   </div>

                     <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label1" class="col-md-1 col-md-offset-4  control-label">
                                Route</label>
                                 <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                                <asp:DropDownList ID="route" runat="server" SkinID="ddlRequired" AutoPostBack="false" CssClass="form-control" Style="min-width: 150px" Width="210">
 <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
</div>
</div>
                          <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="lblFYear" class="col-md-1 col-md-offset-4  control-label">
                                Year</label>
                                 <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

               
                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" AutoPostBack="false" CssClass="form-control" Style="min-width: 150px" Width="110">
                        </asp:DropDownList>
                  </div>
</div>
</div>

                  <div class="row">
                            <div class="col-md-6 col-md-offset-5">                               
                                    
                                      <button  ID="btnGo" runat="server"   onclick="NewWindow().this" class="btn btn-primary"   style="width: 100px"><span>View</span></button>
                            </div>
                        </div>


</div>
</div>
</div>

            
         
    </form>
</body>
</html>
</asp:content>
