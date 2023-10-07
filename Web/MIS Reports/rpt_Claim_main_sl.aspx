<%@ Page Title="" Language="C#" MasterPageFile="~/Master_MGR.master" AutoEventWireup="true" CodeFile="rpt_Claim_main_sl.aspx.cs" Inherits="MIS_Reports_rpt_Claim_main_sl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <form runat="server">
        <div class="container" style="width: 990px">
            <asp:ScriptManager runat="server" ID="sm">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Division" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-md-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-md-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" Width="350" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Gift Type" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlgift" runat="server" Width="350" CssClass="form-control">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Display Claim"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Billed Claim"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Slab Claim"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
			  <div class="row">
                   <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                           <div class="col-sm-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:DropDownList ID="ddlFYear" runat="server" OnSelectedIndexChanged="ddlFYear_SelectIndexchanged" AutoPostBack="true" CssClass="form-control" Width="120">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
            <div class="row">
                <asp:Label ID="lbldes" runat="server" SkinID="lblMand" Text="Description" Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-sm-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddldes" runat="server" CssClass="form-control" Width="120">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align: center">
                    <button id="btnview" type="button" class="btn btn-primary"> <%--style="vertical-align: middle"--%>
                        View</button>
                </div>
            </div>

        </div>
    </form>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('click', '[id*=btnview]', function () {
                var Team = $('[id*=ddlFieldForce]').val();
                var Gifttype = $('[id*=ddlgift]').val();
                if (Team == "0") { alert('Select Team..!'); $('[id*=ddlFieldForce]').focus(); return false; }
                if (Gifttype == "0") { alert('Select Gift Type..!'); $('[id*=ddlgift]').focus(); return false; }
                var sF = $('[id*=ddlMR]').val();
                //{ alert('Select Field Force..1'); $('[id*=ddlMR]').focus(); return false; }
                var ddldes = $('#<%=ddldes.ClientID%>').val();
                var ddldescription = $('#<%=ddldes.ClientID%> :selected').text();
                    strOpen = "rpt_claim_status_Nsm.aspx?sf_code=" + Team + "&ddldes=" + ddldes + "&ddldescription=" + ddldescription + "&giftype=" + Gifttype + "";
                window.open(strOpen, '_self');

            });
        });
    </script>
</asp:Content>