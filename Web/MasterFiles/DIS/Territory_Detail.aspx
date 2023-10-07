<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Detail.aspx.cs"
    Inherits="MasterFiles_Territory_Detail" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/DIS_Menu.ascx" TagName="Menu2" TagPrefix="ucl3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route Add</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <style type="text/css">
        
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSave').click(function () {
                if ($("#txtRoutecode").val() == "") { alert("Enter Route code."); $('#txtRoutecode').focus(); return false; }
                if ($("#txtRouteName").val() == "") { alert("Enter Route Name."); $('#txtRouteName').focus(); return false; }
                if ($("#txt_Target").val() == "") { alert("Enter Target."); $('#txt_Target').focus(); return false; }
                if ($("#txtMinProd").val() == "") { alert("Enter MinProd."); $('#txtMinProd').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <br />
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Left" CssClass="marRight">
            <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Right" CssClass="marRight">
            <asp:Label ID="Lab_DSM" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <%--  <center>
        <table>
          <tr>
                <td>
                    <asp:Label ID="lblN" runat="server" Font-Size="Small" Font-Bold="true" Text="Name:" ></asp:Label>                   
                    <asp:Label ID="lblName" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="true" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                  &nbsp;&nbsp;</td>
             
           
                <td>
                <asp:Label ID="lblty" runat="server" Font-Size="Small" Font-Bold="true" Text="Type:" ></asp:Label>               
                    <asp:Label ID="lblType" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="true" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                </td>
               
            </tr>
          </table>
         </center>--%>
        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDocCatDtls">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblRoutecode" runat="server" SkinID="lblMand" Height="19px" Width="120px"><span style="Color:Red">*</span>Route Code</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtRoutecode" TabIndex="1" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" MaxLength="5" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblRouteName" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Route Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtRouteName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="50" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td class="stylespc" align="left">
                        <asp:Label ID="lblRoute_Target" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Target</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txt_Target" runat="server" SkinID="MandTxtBox" Width="144px" MaxLength="50"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            TabIndex="3" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);"
                            Height="16px"></asp:TextBox>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMinProd" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>MinProd %</asp:Label>
                    </td>
                    <td class="stylespc" align="left">
                        <asp:TextBox ID="txtMinProd" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="163px" TabIndex="4" CssClass="TEXTAREA" onkeypress="CheckNumeric(event);"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <table cellpadding="3" cellspacing="3" width="8%">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Save" OnClick="btnSave_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
