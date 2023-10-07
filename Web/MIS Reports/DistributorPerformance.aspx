<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DistributorPerformance.aspx.cs" Inherits="MIS_Reports_DistributorPerformance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
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
             var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var fdate = $('#txtfrdate').val();
            if (fdate == '') {
                alert('Select the From Date');
                return false;
            }
            var Tdate = $('#ttxtodate').val();
            if (Tdate == '') {
                alert('Select the To Date');
                return false;
            }
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            window.open("rptDistributorPerformance.aspx?&Fdates=" + fdate + "&Tdates=" + Tdate + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
    </script>
    <form id="target_form" runat="server">
        <div class="container" style="width: 100%">
            <div class="form-group">
                <div class="row">
                    <label id="ddlDivision" class="col-md-2  col-md-offset-3  control-label">
                        Division</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                Style="min-width: 150px" Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                        Field Force</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false"
                                CssClass="form-control" Width="500">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                 <div class="row">
                                <label id="Label6" class="col-md-2 col-md-offset-3  control-label">
                                    From Date
                                </label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                       <input id="txtfrdate" name="txtFrom" type="date" cssclass="TEXTAREA" onfocus="this.style.backgroundColor='#E0EE9D'"
                    onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                    tabindex="1" skinid="MandTxtBox" style="font-size: medium" />                               
                                </div>
                                </div>
                            </div>
                 <div class="row">
                                <label id="Label5" class="col-md-2 col-md-offset-3  control-label">
                                    To Date
                                </label>
                                <div class="col-md-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                       <input id="ttxtodate" name="txtFrom" type="date" cssclass="TEXTAREA" 
                                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="1" 
                            skinid="MandTxtBox" style="font-size: medium" />                                 
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
        </div>
    </form>
</asp:Content>

