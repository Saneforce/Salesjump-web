<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Quiz_Result.aspx.cs" Inherits="MIS_Reports_Quiz_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Quiz Result</title>
        <%--        <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />--%>

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
             <div class="row">
                    
                        <asp:Label ID="Label5" runat="server" SkinID="lblMand" Text="From Date" CssClass="col-md-2  col-md-offset-3  control-label"></asp:Label>
                 <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                     <input id="txtFromDate" name="txtFrom1" type="date"  CssClass="TEXTAREA" MaxLength="5"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox" CssClass="form-control"/>
                       </div>
                </div>
            </div>
                  <div class="row">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="To Date" CssClass="col-md-2  col-md-offset-3  control-label"></asp:Label>
                    </td>
                  <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                     <input id="txtDate" name="txtFrom" type="date"  CssClass="TEXTAREA" MaxLength="5"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox" CssClass="form-control"/>
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

