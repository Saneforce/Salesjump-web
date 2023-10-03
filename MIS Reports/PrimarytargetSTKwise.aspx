﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="PrimarytargetSTKwise.aspx.cs" Inherits="MIS_Reports_PrimarytargetSTKwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="../JsFiles/light.js" type="text/javascript"></script>
    
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
            if (st == "---Select Field Force---") { alert("Select Field Force."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

            var st = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();

            
            var str = $('#<%=ddlMR.ClientID%> :selected').val();
            var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
            var year = $('#<%=ddlFYear.ClientID%> :selected').text();       
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').val();
            var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
            var dt1 = new Date(FYear + '/' + FMonth + '/' + 01);
            var dt2 = new Date(TYear + '/' + TMonth + '/' + 01);
  var type = $('#<%=ddlType.ClientID%> :selected').val();  
            var subDiv = $('#<%=subdiv.ClientID%> :selected').val();

            function monthDiff(d1, d2) {
                var months;
                months = (d2.getFullYear() - d1.getFullYear()) * 12;
                months -= d1.getMonth();
                months += d2.getMonth();
                return months <= 0 ? 0 : months;

            }
            if (monthDiff(dt1, dt2) > 0 || (FMonth == TMonth && FYear <= TYear)) {
  if (type == 1) {
                    if (str == "0") {
                        str = st;
                        MR_name = sf_name;
                    }
                window.open("rpt_primarySTKwise_targetVSsale.aspx?code=" + str + "&FYear=" + FYear + "&SF_Name=" + MR_name
                    + "&fmonth=" + FMonth
                    + "&TYear=" + TYear
                    + "&Tmonth=" + TMonth
                    + "&subDiv=" + subDiv

                    , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
            }
            else if (type == 2) {
			
            
			var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
                    if (str == "---Select Base Level---") { alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }
                    var str = $('#<%=ddlMR.ClientID%> :selected').val();
                    //if (str == "") { alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }
                    if (str == "0") {
                        str = st;
                        MR_name = sf_name;
                    }

                    window.open("rpt_Distributor_target_VS_sales_analysis.aspx?SF_Code=" + str + "&FYear=" + FYear + "&SF_Name=" + MR_name
                        + "&FMonth=" + FMonth
                        + "&TYear=" + TYear
                        + "&TMonth=" + TMonth
                        + "&subDiv=" + subDiv

                        , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');

                }
                else if (type == 3) {
				if (str == "0") {
                        str = st;
                        MR_name = sf_name;
                    }

                    window.open("rpt_primaryPDTwise_targetVSsale.aspx?SF_Code=" + str + "&FYear=" + FYear + "&SF_Name=" + MR_name
                        + "&FMonth=" + FMonth
                        + "&TYear=" + TYear
                        + "&TMonth=" + TMonth
                        + "&subDiv=" + subDiv

                        , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');

                }
				else if (type == 4) {
				var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
                    if (str == "---Select Base Level---") { alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }
                     var str = $('#<%=ddlMR.ClientID%> :selected').val();
                    //if (str == "") { alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }
                    if (str == "0") {
                        str = st;
                        MR_name = sf_name;
                    }

      window.open("rpt_primaryPDTwise_FO_Target.aspx?SF_Code=" + str + "&FYear=" + FYear + "&SF_Name=" + MR_name
          + "&FMonth=" + FMonth
          + "&TYear=" + TYear
          + "&TMonth=" + TMonth
          + "&subDiv=" + subDiv

          , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');

  }
  else if (type == 5) {
  if (str == "0") {
                        str = st;
                        MR_name = sf_name;
                    }
      window.open("rpt_PrimaryFO_valuewise.aspx?code=" + str + "&FYear=" + FYear + "&SF_Name=" + MR_name
          + "&fmonth=" + FMonth
          + "&TYear=" + TYear
          + "&Tmonth=" + TMonth
          + "&subDiv=" + subDiv

          , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
  }

        }
        }

    </script>
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        .modal {
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

        .loading {
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

        input[type='text'], select, label {
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
            <div class="row">
                <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
                  Division</label>
                <div class="col-md-3 inputGroupContainer">
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
                       
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
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
                        <asp:DropDownList ID="ddlMR" runat="server" CssClass="form-control"
                            Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">             
                <label id="lblFMonth" class="col-md-2 col-md-offset-3  control-label">
                    From</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px" Width="100">
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
                   
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px" Width="100">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
              
                <label id="Label3" class="col-md-2 col-md-offset-3  control-label">
                    To</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px" Width="100">
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
                        <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px" Width="100">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                   Type </label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-tasks"></i></span>
                        <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px" Width="100">                           
                             <asp:ListItem Value="1" Text="Distributor-Valuewise"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Distributor-Productwise"></asp:ListItem> 
                             <asp:ListItem Value="4" Text="FO-Productwise"></asp:ListItem> 	
                             <asp:ListItem Value="5" Text="FO-Valuewise"></asp:ListItem> 							 
                            
                        </asp:DropDownList>                       
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnView" class="btn btn-primary" onclick="NewWindow()"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>
            </div>
        </div>
    </form>


</asp:Content>

