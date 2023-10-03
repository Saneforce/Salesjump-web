<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cntrlMail.ascx.cs" Inherits="MasterFiles_Mails_cntrlMail" %>
<link href='<%=ResolveClientUrl("../../css/style.css")%>' rel="stylesheet" type="text/css" />
<link href='<%=ResolveClientUrl("../../css/sfm_style.css")%>' rel="stylesheet" type="text/css" />

    <div>
        <br />

        <table style="position: absolute; border-collapse: collapse; width: 100%;" border="0">
            <tr>
                <td valign="top" nowarp colspan="2">
                    <table border="0" style="border-collapse: collapse; width: 100%" class="DcrDispHPadFix">
                        <tr>
                            <td style="color: brown; width: 30%; font-weight: bold; height: 14px;" class="print"
                                nowarp>
                                Welcome&nbsp;
                                <asp:Label ID="lblSfName" runat="Server"></asp:Label></td>
                            <td style="height: 14px" align="center" width="40%">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Small" Text=""></asp:Label>
                            </td>
                            <td style="color: brown; width: 30%; font-weight: bold; height: 14px;" class="print"
                                nowarp align="Right">
                                <a href="../../index.aspx" title="Logout" class="Next">Logout&nbsp;</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 100%" valign="top">
                    <table border="0" style="border-collapse: collapse; width: 100%; height: 100%">
                        <tr>
                            <td style="height: 100%; width: 10%;" nowrap valign="top">
                                <table border="1" style="width: 100%; height: 100%;">
                                    <tr>
                                        <td class="print Header" style="width: 120; height: 18px;" nowrap >
                                            <b>&nbsp;<span class="itemImage"><img alt="My Folders" src="../../Images/FOLDER.ICO"
                                                style="width: 16px; height: 16px;" /></span>My <span id='Shcut' onclick="GotoHome(2)"
                                                    hpath="">F</span>olders</b></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 100%;" width=" 100%" valign="top">
                                            <asp:Panel ID="Panel1" runat="server" Height="700px" BorderColor="">
                                            <asp:Table ID="tbl" runat="server" width="100%" style="border-collapse: collapse;" cellpadding="0" cellspacing="0">
                                            </asp:Table>                
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 100%;" valign="top">
                                <table border="0" style="width: 100%; height: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td valign="top" style="height: 20px; width: 90%;" nowrap class="print">
                                            <div style="overflow: hidden; width: 100%;">
                                                <table style="border-collapse: collapse;" class="Header">
                                                    <tr>
                                                        <td valign="middle" style="height: 18px; width: 10px;" nowrap class="print">
                                                            &nbsp;</td>
                                                        <td valign="middle" id="tdInbox" style="height: 18px; width: 70px; border-style: solid;
                                                            border-width: 0px;" onclick="val='inbox';valid(this)" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="View a Incoming Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                                <img alt="View a Incoming Mail(s)" src="../../Images/InBox.ico" /></span><b>&nbsp;Inbox&nbsp;&nbsp;&nbsp;</b></span></td>
                                                        <td valign="middle" id="tdCompose" style="height: 18px; width: 80px; border-style: solid;
                                                            border-width: 0px;" onclick="val='compose';valid(this);" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Write a Mail">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                                <img alt="Write a Mail" src="../../Images/Writemail.ico" /></span><b>&nbsp;Compose&nbsp;&nbsp;&nbsp;</b></span></td>
                                                        <td valign="middle" id="tdSent" style="height: 18px; width: 100px; border-style: solid;
                                                            border-width: 0px;" onclick="val='sent';valid(this);" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Sent Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                                <img alt="Sent Mail(s)" src="../../images/Sendicon1.gif" /></span><b>&nbsp;Sent&nbsp;Item&nbsp;&nbsp;&nbsp;</b></span></td>
                                                        <td valign="middle" id="tdViewed" style="height: 18px; width: 80px; border-style: solid;
                                                            border-width: 0px;" onclick="val='view';valid(this);" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Viewed Incoming Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                                <img alt="Viewed Incoming Mail(s)" src="../../images/sendicon.png" /></span><b>&nbsp;Viewed&nbsp;&nbsp;&nbsp;</b></span></td>
                                                        <td valign="middle" id="tdHome" style="height: 18px; width: 80px; border-style: solid;
                                                            border-width: 0px;" onclick="GotoHome(1)" hpath="" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Goto Home">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage ">
                                                                <img alt="Viewed Incoming Mail(s)" src="../../images/logoff.jpg" /></span><b>&nbsp;Home</b></span></td>
                                                        <td valign="middle" style="height: 18px; width: 90%;" nowrap class="print">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
<%--                                    <tr>
                                        <td valign="top" width="50%" style="border-width: 0px;">
                                            <div id="divContent">
                                            Loading..
                                            </div>
                                            <asp:ContentPlaceHolder ID="ContentPlaceHolder1" runat="server">
                                            </asp:ContentPlaceHolder>
                                        </td>
                                    </tr>
--%>                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnstate" runat="server" />
        <asp:HiddenField ID="hdnFlg" Value="" runat="server" />
        <asp:HiddenField ID="hdnDesign" runat="server" />
        <asp:HiddenField ID="hdnVal" runat="server" />
    </form>

    <script type="text/javascript" language="javascript">
        PObj = tdInbox;
    </script>
    
    </div>
