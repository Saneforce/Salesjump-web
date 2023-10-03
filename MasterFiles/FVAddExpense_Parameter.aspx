<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FVAddExpense_Parameter.aspx.cs"
    Inherits="MasterFiles_FVAddExpense_Parameter" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fixed / Variable Expense</title>
    <link type="text/css" rel="Stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').click(function () {
                if ($("#txtParameter").val() == "") {
                    alert("Enter Parameter Name.");
                    $('#txtParameter').focus();
                    return false;
                }
                if ($("#txtFixedAmount").val() == "") {
                    alert("Enter Fixed amount.");
                    $('#txtFixedAmount').focus();
                    return false;
                }
                var multiple = $('#<%=ddlExpenseType.ClientID%> :selected').text();
                if (multiple == "--Select--") { alert("Select Expense Type."); $('#ddlExpenseType').focus(); return false; }
                var month = $('#<%=ddlLevel.ClientID%> :selected').text();
                if (month == "--Select--") { alert("Select Level."); $('#ddlLevel').focus(); return false; }

            });
        });
    </script>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
            <table border="0" cellpadding="3" cellspacing="3" id="tblStateDtls" align="center"
                style="width: 24%">
                
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblParameter" runat="server" SkinID="lblMand" Width="100px" Height="18px"><span style="Color:Red">*</span>Parameter</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtParameter" runat="server" SkinID="MandTxtBox" Width="180px" onfocus="this.style.backgroundColor='#E0EE9D'"
                                    onblur="this.style.backgroundColor='White'" TabIndex="1" MaxLength="50">
                                </asp:TextBox>
                            </td>
                        </tr>
               
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblExpenseType" runat="server" SkinID="lblMand" Height="19px" Width="100px"><span style="Color:Red">*</span>Expense Type</asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlExpenseType" runat="server" AutoPostBack="true" Width="120px"
                                    SkinID="ddlRequired" OnSelectedIndexChanged="OnSelectedIndex_ddlExpenseType">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Fixed" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Variable" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="Variable with Limit" Value="L"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                  
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblFixedAmount" runat="server" SkinID="lblMand" Width="100px" Height="18px"><span style="Color:Red">*</span>Fixed Amount</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFixedAmount" runat="server" SkinID="MandTxtBox" Width="180px"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    TabIndex="1" MaxLength="50">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblLevel" runat="server" SkinID="lblMand" Height="19px" Width="100px"><span style="Color:Red">*</span>Level</asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLevel" runat="server" Width="120px" SkinID="ddlRequired">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Line Managers" Value="2"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                                            
              
            </table>
              </ContentTemplate>
                </asp:UpdatePanel>
                <center>  
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />

                </tr></center>
        </center>
        <%--<div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>--%>
    </form>
</body>
</html>
