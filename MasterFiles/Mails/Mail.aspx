<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail.aspx.cs" Inherits="MasterFiles_Mails_Mail" %>
<%@ Register Src ="~/MasterFiles/Mails/cntrlMail.ascx" TagName ="Mail" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script language="javascript" type="text/javascript">
    function setView() {
        document.getElementById("Iframe1").style.display = "block";
        frames["Iframe1"].location.href = 'inbox.aspx?state=inbox&Flder="0"&strSf="0"&strDiv="0"&val="0"';
        //frames["Iframe1"].location.href = 'inbox.aspx?state=inbox&Flder="0"&strSf=' + $sf + '&strDiv=' + $div + '&val=' + $val + '';
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Mail ID="mail1" runat="server" /> 

        <table style="width: 100%" border="0">
            <tr>
                <td valign="top">
                    <asp:Panel ID="pnlCompose" runat="server">
                        <table style=" border:1px solid" align="center">
                            <tr>
                                <td colspan="2" align="center">
                                    <b>Send Mail using asp.net</b>
                                </td>
                            </tr>
                            <tr>
                            <td>
                                From:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Subject:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Body:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Columns="30" Rows="10" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" Text="Send" runat="server" 
                                    onclick="btnSubmit_Click" />
                            </td>
                            </tr>
                    </table>
                        
                    </asp:Panel>
                </td>
            </tr>
        </table>
           <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
