<%@ Page Title="Expense Statement Approval View" Language="C#" AutoEventWireup="true"  CodeFile="rptAutoexpense_Approve_View.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>
<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Expense Statement Approval View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid ;
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
        }

    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
            $('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div id="Divid" runat="server"></div>
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSubdiv" runat="server" Visible="false" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlSubdiv" onselectedindexchanged="ddlSubdiv_SelectedIndexChanged" SkinID="ddlRequired" runat="server">
                        </asp:DropDownList>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc"><asp:dropdownlist ID="monthId" runat="server"></asp:dropdownlist></td>
                      <td align="left" class="stylespc">
                        <asp:Label ID="lblYr" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                    </td> 
                    <td align="left" class="stylespc"><asp:DropDownList ID="yearID" runat="server"></asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                   <td><asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
                OnClick="btnSF_Click" /></td>
                </tr>
            </table>
        </center>
        <br />
        <table align="right" style="margin-right: 5%">
            <tr>
                <td align="right">
                    <asp:Panel ID="pnlprint" runat="server" Visible="false">
                        <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                            onclick="PrintGridData()" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
      
       
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>