<%@ Page Title=""  Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FieldForceTrackingdaywise.aspx.cs" Inherits="MIS_Reports_FieldForceTrackingdaywise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>     
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $('#<%=txtFrom.ClientID%>').val(today);
            




            $(document).on('click', '.btnview', function (e) {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }

                var Fdt = $("#<%=txtFrom.ClientID%>").val();               
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();                            

                var str = 'rpt_FieldForceTracking.aspx?&SFCode=' + sf_code + '&FDate=' + Fdt + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                window.open(str, "_blank", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
            });

        });
    </script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px;" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server"
                            CssClass="form-control" Style="min-width: 100px; width: 350px;">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                    Date
                </label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:TextBox runat="server" ID="txtFrom" CssClass="form-control  datetimepicker" Style="min-width: 100px; width: 150px;"></asp:TextBox>
                    </div>
                </div>
            </div>            
            <div class="row">
                <div class="col-md-6  col-md-offset-5">
                    <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

