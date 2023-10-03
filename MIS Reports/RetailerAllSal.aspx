<%@ Page Title="Retailer Visit Analysis" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RetailerAllSal.aspx.cs" Inherits="MIS_Reports_RetailerAllSal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            $(document).on('click', '.btn', function () {

                var sfcode = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                <%--if (sfcode == "---Select Field Force---") { alert("select FieldForce"); $('#ddlFieldForce').focus(); return false; }--%>

                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }

          

                var mName = $('#<%=ddlFMonth.ClientID%>').val();

                if (sfcode == "---Select Field Force---") {
                    strOpen = "rptTotalRetailSal.aspx?sfCode=admin&fYear=" + FYear + "&fMonth=" + mName + "&SubDiv=0&sfName=admin&mName=" + FMonth + "";
                    window.open(strOpen, 'TotRetail', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
                }
                else {
                    strOpen = "rptTotalRetailSal.aspx?sfCode=" + $('#<%=ddlFieldForce.ClientID%> :selected').val() + "&fYear=" + FYear + "&fMonth=" + mName + "&SubDiv=0&sfName=" + sfcode+"&mName=" + FMonth + "";
                    window.open(strOpen, 'TotRetail', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
                }

            });
        });
    </script>

    <form id="retailersal" runat="server">
        <div class="container" style="width: 100%">
            <div class="form-group">
               <%-- <div class="row">
                    <label id="ddLdiv" class="col-md-2 col-md-offset-3 control-label">
                        Division</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control"
                                Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <label id="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                        Field Force</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group" id="kk" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control"
                                Width="350">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label for="txtMonth" class="col-md-2  col-md-offset-3  control-label">
                        Month</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="form-control"
                                Style="min-width: 100px" Width="120">
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
                        <a class="btn btn-primary">View</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

