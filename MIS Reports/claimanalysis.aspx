<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="claimanalysis.aspx.cs" Inherits="MasterFiles_Reports_claimanalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).on('click','#btnSubmit', function () {
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") {
                alert("Select FieldForce.");
                $('#<%=ddlFieldForce.ClientID%>').focus();
                return false;
            }

            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") {
                alert("Select Year.");
                $('#ddlFYear').focus();
                return false;
            }
			 var slabyr = $('#<%=ddlslab.ClientID%> :selected').val();
            var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();

            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            window.open("Claim_Report.aspx?&YR=" + FYear + "&sfcode=" + ddlfo_Code + "&sfname=" + ddlfo_Name + "&subdiv=" + sub_div_code + "&syear=" + slabyr, 'DSRMonitor', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        });
    </script>
    <body>
        <form id="form1" runat="server">
            <div class="col-lg-12 sub-header">Claim Analysis Report</div>
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
                        <%--<asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>--%>
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
                        <label id="lblFYear" class="col-md-2  col-md-offset-3  control-label">
                            Year</label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:DropDownList ID="ddlFYear" runat="server"  OnSelectedIndexChanged="ddlFYear_SelectIndexchanged" AutoPostBack="true" CssClass="form-control" Width="100">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
					 <div class="row">
                        <label id="lblslab" class="col-md-2  col-md-offset-3  control-label">
                            Month</label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:DropDownList ID="ddlslab" runat="server" CssClass="form-control" Width="100">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-md-offset-5">
                            <button type="button" id="btnSubmit" class="btn btn-primary">View</button>
                        </div>
                    </div>



                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
