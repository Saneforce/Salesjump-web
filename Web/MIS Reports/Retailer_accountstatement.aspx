<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Retailer_accountstatement.aspx.cs" Inherits="MIS_Reports_Retailer_accountstatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Retailer Potential</title>
        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <%--<script type="text/javascript">
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
    </script>--%>
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

            }); 
        </script>
        <script type="text/javascript">
            function NewWindow() {


                var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
                if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }

                var fieldforce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (fieldforce == "--Select--") { alert("Select Fieldforce"); $('#ddlFieldForce').focus(); return false; }

                var distri = $('#<%=Distributor.ClientID%> :selected').text();
                if (distri == "--Select--") { alert("Select Distributor"); $('#Distributor').focus(); return false; }

                var Routee = $('#<%=product.ClientID%> :selected').text();
                if (Routee == "--Select--") { alert("Select Route"); $('#product').focus(); return false; }



                var viewState = document.getElementById("text").value;
                $('#<%=HiddenField1.ClientID %>').val(viewState);


                var date = $("#text").val();
                var date1 = $("#Date1").val();

                if (date == "") { alert("Select Date"); $('#product').focus(); __doPostBack("<%=UniqueID%>"); return false; }

                var Route_code = $('#<%=product.ClientID%> :selected').val();
                var Route_name = $('#<%=product.ClientID%> :selected').text();
                var Retailer_code = $('#<%=ddretailer.ClientID%> :selected').val();
                var Retailer_name = $('#<%=ddretailer.ClientID%> :selected').text();
                var Dist_Name = $('#<%=Distributor.ClientID%> :selected').text();
                var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
                var Dist_Val = $('#<%=Distributor.ClientID%> :selected').val();
                __doPostBack("<%=UniqueID%>");
                window.open("rptretaileraccountstatement.aspx?&DATE=" + date + "&TODATE=" + date1 + " &subdivision=" + subdiv + "&Route_code=" + Route_code + "&Route_name=" + Route_name + "&Retailer_code=" + Retailer_code + "&Retailer_name=" + Retailer_name + "&stockist_code= " + Dist_Val + "&stockist_name=" + Dist_Name + "&Feildforce=" + fieldforce, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');

            }
            $(function () {

                var viewState = $('#<%=HiddenField1.ClientID %>').val();
                $("#text").val(viewState);

            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
            <%--- <ucl:menu ID="menu1" runat="server" />--%>
            <br />
            <asp:ScriptManager runat="server" ID="sm">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <div class="container" style="width: 100%">
                        <div class="form-group">
                            <div class="row">
                                <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                                <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                                    Division</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Style="min-width: 150px"
                                            Width="150" OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label5" class="col-md-2 col-md-offset-3  control-label">
                                    Field Force</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                            Style="min-width: 150px" CssClass="form-control"  OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label1" class="col-md-2 col-md-offset-3  control-label">
                                    Distributor</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="Distributor" runat="server" SkinID="ddlRequired" Style="min-width: 150px"
                                            CssClass="form-control" Width="210px" AutoPostBack="true" OnSelectedIndexChanged="Distributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label id="Label8" class="col-md-2 col-md-offset-3  control-label">
                                    Route</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="product" runat="server" SkinID="ddlRequired" Style="min-width: 150px"
                                            CssClass="form-control" Width="210px" AutoPostBack="true" OnSelectedIndexChanged="route_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <label id="Label3" class="col-md-2 col-md-offset-3  control-label">
                                    Retailer</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:DropDownList ID="ddretailer" runat="server" SkinID="ddlRequired" Style="min-width: 150px"
                                            CssClass="form-control" Width="210px" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                             <div class="row">
                                <label id="Label6" class="col-md-2 col-md-offset-3  control-label">
                                    From Date</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                        <input id="text" name="TextBox1" type="date" Style="min-width: 150px; Width:150px"  class="ddl form-control" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                                        
                                    </div>
                                </div>
                            </div>

                             <div class="row">
                                <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                                    To Date</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                        <input id="Date1" name="TextBox1" type="date" Style="min-width: 150px; Width:150px" class="ddl form-control" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                                        
                                    </div>
                                </div>
                            </div>

                             <div class="row">
                    <div class="col-md-6 col-md-offset-5">
                         <button id="btnGo" runat="server" onclick="NewWindow().this" class="btn btn-primary" style="width: 100px">
                        <span>View</span></button>
                    </div>
                </div>

                        </div>
                    </div>
                   
                   
                   
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
            </Triggers>
          </ContentTemplate>
</asp:UpdatePanel>--%>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
