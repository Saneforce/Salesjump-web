<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SummaryReport.aspx.cs" Inherits="MIS_Reports_SummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        $('form').live("submit", function () {
            ShowProgress();
        });


        function NewWindow() {
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") { alert("Select FieldForce."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
          <%--  var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }--%>
            var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
           <%-- FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            FYear = $('#<%=ddlFYear.ClientID%> :selected').text();--%>
            var fdate = $('#fdatee').val();
            var tdate = $('#tdatee').val();
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            window.open("rptSummaryReport.aspx?&Fdate=" + fdate + "&Tdate=" + tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, 'SummRpt', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
    </script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-1  col-md-offset-4  control-label">
                    Division</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
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
                        <input name="TextBox1" id="fdatee"  type="date"  class="form-control" Style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                        
                     </div>
                        </div>
              
       </div>
           <div class="row">
                            <label id="Label1" class="col-md-1 col-md-offset-4 control-label">
                                To</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="tdatee"  type="date"  class="form-control" Style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                        
                     </div>
                        </div>
              
       </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary" onclick="NewWindow().this"
                        style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

