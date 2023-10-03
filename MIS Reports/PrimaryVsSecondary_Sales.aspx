<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="PrimaryVsSecondary_Sales.aspx.cs" Inherits="MIS_Reports_PrimaryVsSecondary_Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <script runat="server">
    </script>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Primary Vs Secondary Sales</title>

        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/jscript">
            $(document).ready(function () {

                $('#btn').on("click", function () {

                    var Sub_Div_Code = $("#<%=ddldiv.ClientID%>  option:selected").val();
                    var field = $("#<%=ddlFieldForce.ClientID%>  option:selected").val();
                    var field_name = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();
                    var year = $("#<%=ddlFYear.ClientID%>  option:selected").val();
                    var Type=$("#<%=ddlType.ClientID%>  option:selected").val();


                    var str = 'rpt_PrimaryVsSecondary.aspx?&Sub_Div_Code=' + Sub_Div_Code + '&Field_force=' + field + '&Year=' + year + '&Type=' + Type + '&Field_name=' + field_name;
                    window.open(str, "EmpOrder", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');

                });

            });
        </script>
    </head>
    <body>
        <form id="frm1" runat="server">
            <div>
                <br />
                <div class="row">
<%--                    <asp:Label ID="lbldiv" runat="server" Text="Division" CssClass="col-md-2  col-md-offset-3  control-label"></asp:Label>--%>
                     <label id="lbldiv" class="col-md-2  col-md-offset-3  control-label">Division</label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                            <asp:DropDownList ID="ddldiv" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged"></asp:DropDownList>
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
                    <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <label id="Label3" class="col-md-2 col-md-offset-3  control-label">
                        Type
                    </label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-tasks"></i></span>
                            <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                Style="min-width: 100px" Width="100">
                                <asp:ListItem Value="0" Text="HQ"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Distributor"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 5px">
                    <div class="col-md-6  col-md-offset-5">
                        <%--  <asp:Button ID="btnview" CssClass="btn btn-primary" runat="server" Text="View" OnClick="btnview_Click1" />--%>
                        <input type="button" id="btn" value="View" class="btn btn-primary" />

                    </div>
                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>

