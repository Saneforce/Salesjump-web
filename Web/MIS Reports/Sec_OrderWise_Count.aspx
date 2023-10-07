<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Sec_OrderWise_Count.aspx.cs" Inherits="MIS_Reports_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>

    <head>
        <style type="text/css">
            input[type='text'], select, label {
                line-height: 22px;
                padding: 4px 6px;
                font-size: medium;
                border-radius: 7px;
                width: 100%;
                font-weight: normal;
            }
        </style>
        <link type="text/css" rel="stylesheet" href="../../css/style1.css" />


        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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


  
            
                    $("#<%=btnSubmit.ClientID %>").click(function () {

               <%--     var ddlMRName = $('#<%=ddlMR.ClientID%> :selected').text();--%>


                        var Sub_div = $('#<%=subdiv.ClientID%> :selected').val();
                        if (Sub_div == "0") { alert("Select Division Name."); $('#subdiv').focus(); return false; }


                        var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').val();
                        if (FieldForce == "0") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }


                 <%--   if (ddlMRName != '') {

                        var ddlMR = document.getElementById('<%=ddlMR.ClientID%>').value;
                    }--%>

                        var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;



                        var ddlYear = document.getElementById('<%=ddlFYear.ClientID%>').value;

                <%--    var selectedvalue = $('#<%= rbnList.ClientID %> input:checked').val();--%>

                        //if (ddlMR != -1 && ddlMR != 0 && ddlMRName != '') {

                     
                        //}
                        //else {

                        //    showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, selectedvalue, FieldForce)
                        //}



                    });
                
                });
            
        </script>
        <%--      <script type="text/jscript">
            $(document).ready(function () {
                document.getElementById('txtDate').valueAsDate = new Date();

            });
        </script>--%>
    </head>
    <body>
        <form id="frm" runat="server">
            <asp:ScriptManager runat="server" ID="sm">
            </asp:ScriptManager>

            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <div class="container" style="width: 100%">

                        <div class="row">
                            <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                                Division</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="subdiv_SelectedIndexChanged" Width="200px"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                                Field Force</label>
                            <div class="col-md-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control" AutoPostBack="true"
                                        Width="360px">
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
                                <asp:Button ID="btnSubmit"  class="btn btn-primary" OnClick="btnSubmit_Click" runat="server" Text="View" />

                                <asp:Label ID="lblpath" runat="server" ForeColor="#f1f2f7"></asp:Label>

                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <br />

        </form>
    </body>
    </html>

</asp:Content>

