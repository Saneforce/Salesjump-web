<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailer_GeoTag_Status.aspx.cs" Inherits="MasterFiles_Reports_Retailer_GeoTag_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Retailer GeoTag Status</title>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    </head>
    <body>
        <form id="frm1" runat="server">
            <div>
                <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
            <br />
            <div class="row" style="display:none;">
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