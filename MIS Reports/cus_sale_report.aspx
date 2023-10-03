<%@ Page Language="C#" MasterPageFile="~/Master.master"  AutoEventWireup="true" CodeFile="cus_sale_report.aspx.cs" Inherits="MIS_Reports_cus_sale_report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
           

            $(document).on('click', '#btnGo', function () {

                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var yrs = $("#<%=ddlFYear.ClientID%>").val();
                var sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                var sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
               

                if (sf_code == '0') {
                    alert('Select manager..!');
                    $("#<%=ddlFieldForce.ClientID%>").focus();
                    return false;
                }

                url = 'cus_sale_report_view.aspx?&yr=' + yrs + '&SubDiv=' + SubDivCode + '&SfCode=' + sf_code + '&sfName=' + sf_Name;
                window.open(url, 'cus_sale', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
            });
        });
    </script>
    <form id="form1" runat="server">
            <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <%-- <asp:Label ID="Label2" runat="server"  Text="Division" CssClass="col-sm-1 col-sm-offset-4 control-label"></asp:Label>--%>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="150"
                            AutoPostBack="true" OnSelectedIndexChanged="subdiv_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
                 <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Manager</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" CssClass="form-control"
                            AutoPostBack="true">
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
                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control" Width="100">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                  
            <div class="row">
                <div class="col-md-6  col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary" style="vertical-align: middle;"><span>View</span></a>
                    <asp:Button runat="server" ID="exceldld" CssClass="btn btn-primary" style="margin-left: 2rem;" ForeColor="White" Text="Excel" OnClick="exceldld_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
