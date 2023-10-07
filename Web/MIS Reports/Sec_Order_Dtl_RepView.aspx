<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Sec_Order_Dtl_RepView.aspx.cs" Inherits="MasterFiles_Sec_Order_Dtl_RepView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    

    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('click', '.btnview', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                console.log(sf_code);
                if (sf_code == "0") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                var fromdt = $("#txtFromDate").val();
                if (fromdt.length == 0) { alert('Select From Date'); $("#txtFromDate").focus(); return false; }
                var todt = $("#txtToDate").val();
                if (todt.length == 0) { alert('Select To Date'); $("#txtToDate").focus(); return false; }
                var SubDivCode = $("#<%=ddlsubdiv.ClientID%>").val();
                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                var FromDt = $("#txtFromDate").val();
                var ToDt = $("#txtToDate").val();
                var Fyear = "";
                var FMonth = "";
                var str = 'rptSec_Order_Dtl_RepView.aspx?&SFCode=' + sf_code + '&FYear=' + Fyear + '&FMonth=' + FMonth + '&SFName=' + SFName + '&SubDiv=' + SubDivCode + "&FromDt=" + FromDt + "&ToDt=" + ToDt;
                window.open(str, "Sec_Order_Dtl", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
            });

        });
        function setDates() {
            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            if (sf_code == "0") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
            var fromdt = $("#txtFromDate").val();
            if (fromdt.length == 0) { alert('Select From Date'); $("#txtFromDate").focus(); return false; }
            var todt = $("#txtToDate").val();
            if (todt.length == 0) { alert('Select To Date'); $("#txtToDate").focus(); return false; }
            var SubDivCode = $("#<%=ddlsubdiv.ClientID%>").val();
            var FromDt = $("#txtFromDate").val();
            var ToDt = $("#txtToDate").val();
            $('#<%=HiddenField1.ClientID%>').val(sf_code);
            $('#<%=HiddenField2.ClientID%>').val(SubDivCode);
            $('#<%=HiddenField3.ClientID%>').val(FromDt);
            $('#<%=HiddenField4.ClientID%>').val(ToDt);
        }
    </script>

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
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="HiddenField1" />
        <asp:HiddenField runat="server" ID="HiddenField2" />
        <asp:HiddenField runat="server" ID="HiddenField3" />
        <asp:HiddenField runat="server" ID="HiddenField4" />
    <div class="container" style="width: 100%">
        <div class="form-group">
            <div class="row">
                <label id="ddLdiv" class="col-md-2 col-md-offset-3 control-label">
                    Division</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlsubdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="font-size: 17px;" Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="kk" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="font-size: 17px;" Width="500">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtFromDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                   From Date</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div1" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                       <%-- <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datepicker"
                            style="min-width: 110px; width: 120px;" />--%>
                               <input type="date" name="txtFromDate" id="txtFromDate" class="form-control" autocomplete="off"  style="min-width: 110px; width: 120px;" />

                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtToDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                    To Date</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group" id="Div2" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                       <%-- <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datepicker"
                            style="min-width: 110px; width: 120px;" />--%>
                               <input type="date" name="txtToDate" id="txtToDate" class="form-control" autocomplete="off"  style="min-width: 110px; width: 120px;" />

                    </div>
                </div>
            </div>
          
            <div class="row">
                  <div class="col-md-6 col-md-offset-5">
					<a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>
					<asp:Button Text="Excel" runat="server" CssClass="btn btn-primary" style="margin-left :2rem;" ID="btnexl" OnClick="btnexl_Click" OnClientClick="setDates()" />
            </div>
            </div>
        </div>
    </div>
    <br />
    
    </form>
</asp:Content>
