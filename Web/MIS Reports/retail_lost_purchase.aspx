<%@ Page Title="Retail Lost-Purchase Details" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="retail_lost_purchase.aspx.cs" Inherits="MIS_Reports_retail_lost_purchase" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Retail Lost-Purchase Details</title>
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
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {

                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }




            });
        }); 
    </script>
    <script type="text/javascript">
        function NewWindow() {
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }

            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }

            var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
            if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }

            var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
            if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }

            var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
            if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }

            var fieldforce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (fieldforce == "--Select--") { alert("Select Fieldforce"); $('#ddlFieldForce').focus(); return false; }

            var sfCode=   $('#<%=ddlFieldForce.ClientID%>').val();

            var distri = $('#<%=Distributor.ClientID%> :selected').text();
            if (distri == "--Select--") { alert("Select Distributor"); $('#Distributor').focus(); return false; }

            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').val();
            var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();


            var Dist_Val = $('#<%=Distributor.ClientID%> :selected').val();

            window.open("rpt_retail_lost_purchase_sel_month.aspx?&FMonth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + TMonth + "&TYear=" + TYear + "&dist_code=" + Dist_Val + "&Distri_name=" + distri + "&fieldforce_name=" + fieldforce + "&SFCode=" + sfCode, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%-- <ucl:menu ID="menu1" runat="server" />--%>
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="container" style="width: 100%">
                    <div class="form-group">
                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="lblFMonth" class="col-md-2 col-md-offset-3 control-label">
                                From</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
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
                                    <%-- <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: center"
                                            Width="60"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label3" class="col-md-2 col-md-offset-3  control-label">
                                To</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
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
                                    <%-- <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text=" Year" Style="text-align: center"
                                Width="60"></asp:Label>--%>
                                    <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                                Division</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="Label1" class="col-md-2 col-md-offset-3  control-label">
                                Field Force</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                        CssClass="form-control" Style="min-width: 200px" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="seldist" runat="server" class="col-md-2 col-md-offset-3  control-label">
                                Distributor</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="Distributor" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 200px; width: 250px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-md-offset-5">
                                <button id="btnGo" runat="server" onclick="NewWindow().this" class="btn btn-primary"
                                    style="width: 100px">
                                    <span>View</span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
</asp:Content>