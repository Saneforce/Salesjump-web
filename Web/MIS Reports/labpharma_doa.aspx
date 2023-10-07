<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="labpharma_doa.aspx.cs" Inherits="MIS_Reports_labpharma_doa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {


            $(document).on('click', '.btn', function () {

                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
                if (FMonth == "--- Select ---") { alert("Select From Month."); $('#<%=ddlFMonth.ClientID%>').focus(); return false; }
                var sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                var sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                var type = $("#<%=view.ClientID%> :selected").text();
                
                if (sf_code == '0') {
                    alert('Select manager..!');
                    $("#<%=ddlFieldForce.ClientID%>").focus();
                    return false;
                }

                url = 'labpharma_doa_view.aspx?&month=' + FMonth + '&SubDiv=' + SubDivCode + '&SfCode=' + sf_code + '&typ=' + type + '&sfName=' + sf_Name;
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
                    <label id="lblFMonth" class="col-md-2  col-md-offset-3  control-label">
                        Month</label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="form-control" Style="min-width: 100px;
                                width: 120px">
                                <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
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
               <label id="Label3" class="col-md-2  col-md-offset-3  control-label">
                Type</label>
                <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                 <asp:DropDownList ID="view" runat="server" CssClass="form-control" Visible="true"  Width="200px" >
                          <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="DOB"></asp:ListItem>
                            <asp:ListItem Value="2" Text="DOA"></asp:ListItem>
               </asp:DropDownList>
               </div>
                </div>
                </div>
                  <div class="row">
                <div class="col-md-11" style="text-align: center">
                    <a id="btnGo" class="btn btn-primary" style="vertical-align: middle;"><span>View</span></a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>



