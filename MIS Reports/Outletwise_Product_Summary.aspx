<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Outletwise_Product_Summary.aspx.cs" Inherits="MIS_Reports_Outletwise_Product_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Expense Report</title>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/jscript">
            $(document).ready(function () {
                $('#<%=btnview.ClientID%>').on('click', function () {
                    var sfc = $('#<%=ddldist.ClientID%>').val();
                    if (sfc == 0) {
                        alert('Select FieldForce')
                        return false;
                    }
                    var mnth = $('#<%=ddlmnth.ClientID%>').val();
                    if (mnth == 0) {
                        alert('Select Month')
                        return false;
                    }
                });
            });
        </script>
    </head>
    <body>
        <form id="frm1" runat="server">
            <div>
                <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
            <br />
            <div class="row">
                <asp:Label ID="lbldiv" runat="server" Text="Division" CssClass="col-md-2  col-md-offset-3  control-label"></asp:Label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                        <asp:DropDownList ID="ddldiv" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px">
                <asp:Label ID="lbldist" runat="server" Text="FieldForce" CssClass="col-md-2  col-md-offset-3  control-label"></asp:Label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddldist" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px">
                <asp:Label ID="Label1" CssClass="col-md-2  col-md-offset-3  control-label" Text="Month" runat="server">
                </asp:Label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlmnth" CssClass="form-control"  runat="server">
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
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px">
                <asp:Label ID="Label2" CssClass="col-md-2  col-md-offset-3  control-label" Text="Year" runat="server">
                </asp:Label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlyr" CssClass="form-control"  runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
                    </ContentTemplate>
                     </asp:updatepanel>
            <div class="row" style="margin-top: 5px">
                <div class="col-md-6  col-md-offset-5">
                    <asp:Button ID="btnview" CssClass="btn btn-primary" runat="server" Text="View" OnClick="btnview_Click" />
                </div>
            </div>
                </div>
        </form>
    </body>
    </html>
</asp:Content>

