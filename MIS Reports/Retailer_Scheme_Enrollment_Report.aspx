<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailer_Scheme_Enrollment_Report.aspx.cs" Inherits="MIS_Reports_Retailer_Scheme_Enrollment_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Retailer Business Summary Report</title>
        <%--        <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />--%>

        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/jscript">
            $(document).ready(function () {
                var date = $("#fromdate").val();
                var tdate = $('#todate').val();
                if (date == "")
                    document.getElementById('fromdate').valueAsDate = new Date();
                if (tdate == "")
                    document.getElementById('todate').valueAsDate = new Date();
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
                <asp:Label ID="Label1" CssClass="col-md-2  col-md-offset-3  control-label" Text="From Date" runat="server">
                </asp:Label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input id="fromdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            tabindex="1" skinid="MandTxtBox" />
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px">
                <asp:Label ID="Label2" CssClass="col-md-2  col-md-offset-3  control-label" Text="To Date" runat="server">
                </asp:Label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input id="todate" name="txtTo" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            tabindex="1" skinid="MandTxtBox" />
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

