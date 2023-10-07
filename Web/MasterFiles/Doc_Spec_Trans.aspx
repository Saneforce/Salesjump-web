<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doc_Spec_Trans.aspx.cs" Inherits="MasterFiles_Doc_Spec_Trans" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Customer Channel</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
            $('#btnTransfer').click(function () {
                var From = $('#<%=ddlTrans_From.ClientID%> :selected').text();
                if (From == "--Select--") { alert("Select Transfer From"); $('#ddlTrans_From').focus(); return false; }
                var To = $('#<%=ddlTrans_To.ClientID%> :selected').text();
                if (To == "--Select--") { alert("Select Transfer To"); $('#ddlTrans_To').focus(); return false; }

            });
        });
    </script>
         <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        .clp
        {
           border-collapse: collapse;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table border="0" cellpadding="5" cellspacing="5" id="tblspectransfer" align="center">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblTrans_From" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Transfer From</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTrans_From" runat="server" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" AutoPostBack="true"
                            TabIndex="1" onselectedindexchanged="ddlTrans_From_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td align="left">
                        <asp:Label ID="lblTrans_To" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Transfer To</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTrans_To" runat="server" Height="20px" onblur="this.style.backgroundColor='White'"
                            onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" AutoPostBack="true"
                            TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="Chkdelete" runat="server" Font-Size="11px" Font-Names="Verdana"
                             Text=" 'Delete' After Transfer"  />
                    </td>
                </tr>
            </table>
        </center>
        <br />      
        <center>
            <asp:HiddenField ID="txtconformmessageValue" runat="server" />
            <asp:Button ID="btnTransfer" runat="server" CssClass="BUTTON" Text="Transfer - Channel" OnClick="btnTransfer_Click" />
        </center>
        <br />
        <center>
            <asp:Panel ID="pnlCount" runat="server" Visible="false">
                <asp:Table ID="Table1" runat="server" BorderStyle="Solid" Width="300px" BorderWidth="1" CssClass="clp"
                    CellSpacing="3" CellPadding="3">
                          <asp:TableHeaderRow>
                    <asp:TableHeaderCell ColumnSpan="2">
                    <asp:Label ID="lbltrans" runat="server" Text="Transaction Available" Font-Names="Verdana" Font-Bold="true" Font-Size="12px" ForeColor="Blue"></asp:Label>
                    </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell BorderWidth="1" Width="80px" HorizontalAlign="Left">
                            <asp:Label ID="lblListed" runat="server" Text="Listed Customer Count" SkinID="lblMand"></asp:Label></asp:TableCell>
                        <asp:TableCell BorderWidth="1" Width="80px" HorizontalAlign="Center">
                            <asp:Label ID="lblDrcount" runat="server" Font-Bold="true"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell BorderWidth="1" Width="80px" HorizontalAlign="Left">
                            <asp:Label ID="lblUnListed" runat="server" Text="UnListed Customer Count" SkinID="lblMand"></asp:Label></asp:TableCell>
                        <asp:TableCell BorderWidth="1" Width="80px" HorizontalAlign="Center">
                            <asp:Label ID="lblUndrcount" runat="server" Font-Bold="true"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm to Transfer" CssClass="BUTTON" OnClientClick="return confirm('Do you want to Transfer Speciality?') &&  confirm('Are you sure want to Transfer?');" 
                    OnClick="btnConfirm_Click" />
            </asp:Panel>
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
