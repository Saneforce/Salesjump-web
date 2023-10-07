<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Uotlet_Placement_Achived.aspx.cs" Inherits="MasterFiles_Uotlet_Placement_Achived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="../JsFiles/light.js" type="text/javascript"></script>
     <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function NewWindow() {
            var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (st == "---Select Manager---") { alert("Select Manager."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

            var st = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var str = $('#<%=ddlMR.ClientID%> :selected').val();
            var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
            if (MR_name == "---Select Base Level---") {
                str = st;
                MR_name = sf_name;
            }
            

            var year = $('#<%=ddlFYear.ClientID%> :selected').text();
            
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            if (FMonth==0) {
                alert("Please select Month...");
                $('#<%=ddlFMonth.ClientID%>').focus(); return false;
            }
            window.open("rpt_Uotlet_Placement_Achived.aspx?SF_Code=" + str + "&Month=" + FMonth + "&Year=" + year + "&SF_Name=" + MR_name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');

        }

        

    </script>    
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        
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
    <form id="form1" runat="server">
    <div class="container" style="width: 100%">
        <h3>Outlet Placement Target</h3>
        <br />
        <br />
        <div class="row">
            <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
                Division</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                        OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblFF" class="col-md-2 col-md-offset-3 control-label">
                Manager</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group" id="kk" runat="server">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="form-control"
                        Visible="false" Width="350">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired" CssClass="form-control"
                        Width="70">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                        CssClass="form-control" Width="350">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"
                        CssClass="form-control" Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblMR" runat="server" class="col-md-2 col-md-offset-3 control-label">
                Field Force</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlMR" runat="server" Visible="false" CssClass="form-control"
                        Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
             <label id="Label2" runat="server" class="col-md-2 col-md-offset-3 control-label">
                Month</label>
            	<div class="col-md-5 inputGroupContainer">
                	<div class="input-group">
                    	<span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>

                         <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired"  CssClass="form-control"
                                Style="font-size: 14px;" Width="120" >
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
             <label id="lblFYear" runat="server" class="col-md-2 col-md-offset-3 control-label">
                Year</label>
            	<div class="col-sm-5 inputGroupContainer">
                	<div class="input-group">
                    	<span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>

                          <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired"  CssClass="form-control"
                                Style="font-size: 14px;" Width="120" >
                            </asp:DropDownList>
              </div>
	            </div>
</div>
        	<div class="row">
            <div class="col-sm-12" style="text-align: center">                 

						<button id="btnView" class="btn btn-primary" runat="server" onclick="NewWindow().this" style="vertical-align: middle">
                            <span>View</span></button>
   </div>
</div>
        </div>
    </form>
</asp:Content>

