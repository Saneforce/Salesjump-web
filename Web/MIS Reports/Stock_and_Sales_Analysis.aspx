﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Stock_and_Sales_Analysis.aspx.cs" Inherits="MIS_Reports_Stock_and_Sales_Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(fmon, fyr, dis_name, subdiv, distributor) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_stock_second_sales.aspx?FMonth=" + fmon + "&FYear=" + fyr + "&Dist_Name=" + dis_name + "&subdivision=" + subdiv + "&Distributor=" + distributor,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }


   
    </script>
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

        }); 
    </script>
    <script type="text/javascript">
        function NewWindow() {
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
            var TMonth = $('#<%=ddltmnth.ClientID%> :selected').text();
            if (TMonth == "---Select---") { alert("Select To Month."); $('#ddltmnth').focus(); return false; }
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
            var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
            if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }

            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var TMonth = $('#<%=ddltmnth.ClientID%> :selected').val();
            var FMonthN = $('#<%=ddlFMonth.ClientID%> :selected').text();
            var TMonthN = $('#<%=ddltmnth.ClientID%> :selected').text();
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var Dist_Name = $('#<%=Distributor.ClientID%> :selected').text();
            var ffd_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var ffd_code = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            var Dist_Code = $('#<%=Distributor.ClientID%> :selected').val();
            window.open("Rpt_Stock_Sales_Progressive.aspx?&FMonth=" + FMonth + "&TMonth=" + TMonth + "&FMnthName=" + FMonthN + "&TMnthName=" + TMonthN + "&FYear=" + FYear + "&Dist_Name=" + Dist_Name + "&SFName=" + ffd_Name + "&SFCode=" + ffd_code + "&subdivision=" + subdiv + "&Dist_Code=" + Dist_Code, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
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
    <div>
        <%--  <ucl:menu ID="menu1" runat="server" />--%>
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <div class="container" style="width: 100%">
                    <div class="form-group">
                        <div class="row">
                            <label id="lblFMonth" style="white-space:nowrap;" class="col-md-2 col-md-offset-3 control-label">
                                From Month</label>
                                <div class="input-group col-md-3 ">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="form-control" Style="min-width: 100px"
                                        Width="100">
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
                                    <%-- <asp:Label ID="lblFYear" runat="server" SkinID="lblMand"
                                Text="Year" Width="60"></asp:Label>--%>
                                </div>
                        </div>
                        <div class="row">
                            <label id="lblTMonth" style="white-space:nowrap;" class="col-md-2 col-md-offset-3 control-label">
                                To Month</label>
                                <div class="input-group col-md-3 ">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:DropDownList ID="ddltmnth" runat="server" CssClass="form-control" Style="min-width: 100px"
                                        Width="100">
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
                                    <%-- <asp:Label ID="lblFYear" runat="server" SkinID="lblMand"
                                Text="Year" Width="60"></asp:Label>--%>
                                </div>
                        </div>
                        <div class="row">
                            <label class="col-md-2 col-md-offset-3 control-label">
                                Year</label>
                                <div class="input-group col-md-3 ">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control" Style="min-width: 100px"
                                        Width="100">
                                    </asp:DropDownList></div></div>
                        <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="lbldiv" class="col-md-2 col-md-offset-3  control-label">
                                Division</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="subdiv" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="subdiv_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                                FieldForce</label>
                            <div class="col-md-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                            </asp:DropDownList>
                            <label id="Label1" class="col-md-2 col-md-offset-3  control-label">
                                Distributor</label>
                            <div class="col-md-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="Distributor" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-6 col-md-offset-4">
                                <button id="btnGo" class="btn btn-primary" runat="server" onclick="NewWindow().this"
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
</asp:Content>

