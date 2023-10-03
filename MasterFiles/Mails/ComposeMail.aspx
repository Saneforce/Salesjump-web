<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComposeMail.aspx.cs" Inherits="MasterFiles_Mails_ComposeMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

            <div class="DcrDispHPadFix" style="border-width: 1px; border-style: solid; border-color: #f7fff8;">
                <table width="100%" align="Center" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class='print' nowrap style="height: 27px">
                        </td>
                        <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                            width: 100px; height: 27px;" valign='middle' title='Clear a New Mail' onclick='clearComp();'>
                            <span class='itemImage'>
                                <img alt='Write a New Mail' src='../../images/Writemail.ico' /></span>Clear
                            Mail</td>
                        <td class='print' nowrap style="width: 20px; text-align: center; height: 27px;" valign='middle'>
                            |</td>
                        <td nowrap style="border-style: solid; border-width: 0px; width: 60px; height: 27px;"
                            title='Send a Composed Mail'>
                            <div>
                                <asp:LinkButton Style="cursor: hand; background: url('../../images/sendicon.gif') left center no-repeat;
                                    text-decoration: none; padding: 0em 1.2em;" ID="lnkButton" OnClientClick="return valid()"
                                    Font-Names="Verdana" ForeColor="#004000" Font-Size="10px" runat="server">&nbsp;&nbsp;Send</asp:LinkButton>
                            </div>
                        </td>
                        <td class='print' nowrap style="width: 20px; text-align: center; height: 27px;" valign='middle'>
                            |</td>
                        <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                            width: 60px; height: 27px;" valign='middle' title='Attach a file ' runat="server"
                            id="tdAttach">
                            <span class='itemImage' id="spanAttach" runat="server">
                                <img alt='Attach a file' src='../../images/Attachment.gif' /></span>Attach&nbsp;</td>
                        <td class='print' nowrap style="width: 20px; text-align: center; height: 27px;" valign='middle'>
                            |</td>
                        <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                            width: 90px; height: 27px;" valign='middle' title='Add to Cc' id='idCC' onclick="return CCFunc(this);">
                            <span class='itemImage'>
                                <img alt='Add to Cc' src='../../images/CcIcon.jpg' style='width: 16px; height: 16px' /></span><span>Remove
                                    Cc</span>
                        </td>
                        <td class='print' nowrap style="width: 20px; text-align: center; height: 27px;" valign='middle'>
                            |</td>
                        <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                            width: 90px; height: 27px;" valign='middle' title='Add to Bcc' id='idBCC' onclick="return CCFunc(this);">
                            <span class='itemImage'>
                                <img alt='Add to Bcc' src='../../images/BccIcon.jpg' style='width: 20px; height: 20px' /></span><span>Remove
                                    Bcc</span>
                        </td>
                        <td class='print' style="width: 90%; height: 27px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border-width: 2px; border-style: solid; border-color: White;" class="DcrDispHPadFix">
                <table width="100%" align="center">
                    <tr>
                        <td class="print">
                            To &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td width="35px" align="Right">
                            :</td>
                        <td width="65px">
                            <asp:TextBox ID="txtAddr" runat="server" ReadOnly="true" Width="300px" CssClass="TextBox"></asp:TextBox></td>
                        <td>
                            <asp:Panel ID="lblAddr" runat="server" Width="50px">
                                <span class='itemImage'>
                                    <img alt='Address Book' src="../../images/Address_Book_Icon.gif" /></span>
                            </asp:Panel>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr id="TrCC">
                        <td class="print">
                            Cc
                        </td>
                        <td width="35px" align="Right">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtAddr1" ReadOnly="true" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Panel ID="lblAddr2" runat="server" Width="50px">
                                <span class='itemImage'>
                                    <img alt='Address Book' src="../../images/Address_Book_Icon.gif" /></span>
                            </asp:Panel>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr id="TrBCC">
                        <td class="print">
                            Bcc
                        </td>
                        <td width="35px" align="Right">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtAddr2" ReadOnly="true" runat="server" Width="300px" CssClass="TextBox"  ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Panel ID="lblAddr3" runat="server" Width="50px">
                                <span class='itemImage'>
                                    <img alt='Address Book' src="../../images/Address_Book_Icon.gif" /></span>
                            </asp:Panel>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td class="print">
                            Subject</td>
                        <td width="35px" align="Right">
                            :</td>
                        <td>
                            <asp:TextBox ID="txtSub" runat="server" MaxLength="42" onkeypress="_fNvALIDeNTRY('-o-!!~~^%*;`~@#.\\,$&_[]|=<>{}?',50);" Width="300px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <div id="divAttach">
                                <asp:Image ID="imgAtt" ImageUrl="~/images/Attachment.gif" runat="server" /><asp:Label
                                    ID="lblFileName" runat="server" Text=""></asp:Label>
                                <asp:LinkButton CssClass="print" SkinID="dfs" ID="lbFileDel"
                                    runat="server" OnClientClick="ClearFileUpload();" Text="Remove"></asp:LinkButton>
                                <%--<a id="linkFile" href="javascript:void(0);" onclick="ClearFileUpload();">Remove</a>--%>
                            </div>
                            <asp:HiddenField ID="hidAttPath" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%" align="center">
                <tr class='print'>
                    <td style="width: 888px">
                        Maximum Number of characters is 5000</td>
                    <td style="background-color: Yellow;" width="10%">
                        Left:&nbsp;&nbsp;<span id="myCounter">5000</span></td>
                </tr>
                <tr>
                    <td colspan="2"  style =" border:1px">
                        <asp:TextBox ID="txtMsg" SkinID="txtArea" BorderWidth="1" onDrag="return false;" onDrop="return false;" name="txtMsg" TextMode="MultiLine" onKeyPress="return taLimit(this)"
                            onpaste="return MaxLenOnPaste(5000)" MaxLength="5000" onKeyUp="return taCount(this,'myCounter')" 
                            Height="200px" Width="98%" runat="server"></asp:TextBox>
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
