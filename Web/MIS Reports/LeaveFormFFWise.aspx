<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="LeaveFormFFWise.aspx.cs" Inherits="MIS_Reports_LeaveFormFFWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(document).on('click', '.btn', function () {
                //                var sf = $('#<%=ddlFieldForce.ClientID%> :selected').val();
                //                if (sf == "0") { alert("Select Manager..!"); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

                //                var str = $('#<%=ddlMR.ClientID%> :selected').val();
                //                if (str == "0") { alert("Select Field Force..!"); $('#<%=ddlMR.ClientID%>').focus(); return false; }
                var sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                var sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();


                if (sf_code == '0') {
                    alert('Select manager..!');
                    $("#<%=ddlFieldForce.ClientID%>").focus();
                    return false;
                }

                if ($('#mgrOnly').is(":checked")) {

                    sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                    sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                    if (sf_code == '0') {
                        alert('Select manager..!');
                        $("#<%=ddlFieldForce.ClientID%>").focus();
                        return false;
                    }
                }
                else {
                    sf_code = $("#<%=ddlMR.ClientID%> ").val();
                    sf_Name = $("#<%=ddlMR.ClientID%> :selected").text();
                    if (sf_code == '0' || sf_code == '') {
                        alert('Select Field Force..!');
                        $("#<%=ddlMR.ClientID%>").focus();
                        return false;
                    }
                }





                var fYear = $('#<%=ddlFYear.ClientID%> :selected').val();
                var url = 'rptLeaveFormFFWise.aspx?sfCode=' + sf_code + '&fYear=' + fYear;
                var win = window.open(url, 'LeaveFFWise', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1200,height=600,left = 0,top = 0');
                win.focus();

            });

        });
    </script>

    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 2px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>

    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%">
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
                          Manager Only
                    <input type="checkbox" id="mgrOnly" />
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
                <label for="txtYear" class="col-md-2 col-md-offset-3  control-label">
                    Year</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px" Width="120">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a name="btnView" class="btn btn-primary" style="width: 100px">View</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
