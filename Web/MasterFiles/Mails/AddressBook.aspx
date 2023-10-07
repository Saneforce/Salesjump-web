<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddressBook.aspx.cs" Inherits="MasterFiles_Mails_AddressBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ereporting and sales analysis</title>
    <link href="../../css/PrintStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/sfm_style.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .img1
    {margin:0px 0px 0px 0px;
    background:url("../../images/sendicon.gif") left center no-repeat;
    padding: 0em 1.2em; 
    font: 8pt "tahoma"; 
    color: #336699; 
    text-decoration: none; 
    font-weight: normal; 
    letter-spacing: 0px; 
    }  
    </style>
</head>
<body>
    <form id="frmAddr" runat="server" onsubmit="AddFlag()">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="divAddrBook">
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
                    <td colspan="2">
                        <asp:TextBox ID="txtMsg" SkinID="txtArea" onDrag="return false;" onDrop="return false;" name="txtMsg" TextMode="MultiLine" onKeyPress="return taLimit(this)"
                            onpaste="return MaxLenOnPaste(5000)" MaxLength="5000" onKeyUp="return taCount(this,'myCounter')"
                            Height="200px" Width="98%" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div id="Div1">
                <cc1:PopupControlExtender ID="PopupControlExtender1" Position="Center" TargetControlID="lblAddr"
                    PopupControlID="pnl" runat="server">
                </cc1:PopupControlExtender>
                <cc1:PopupControlExtender ID="PopupControlExtender2" Position="Center" TargetControlID="lblAddr2"
                    PopupControlID="pnl2" runat="server">
                </cc1:PopupControlExtender>
                <cc1:PopupControlExtender ID="PopupControlExtender3" Position="Top" TargetControlID="lblAddr3"
                    PopupControlID="pnl3" runat="server">
                </cc1:PopupControlExtender>
                <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" Position="Bottom"
                    TargetControlID="tdAttach" PopupControlID="PnlAttach">
                </cc1:PopupControlExtender>
                <asp:Panel ID="Pnl" BackColor="white" BorderColor="ActiveBorder" BorderWidth="1"
                    runat="server" Width="280px" Height="280px" Style="display: none;">
                    <div style="border-collapse: collapse; width: 280px;" class="HeaderPrint">
                        <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                            <tr>
                                <td class='print Header' style='width: 280px'>
                                    &nbsp;<span class='itemImage'><img alt='Address Book' src="../../Images/Address_Book_Icon.gif" /></span>Address
                                    Book</td>
                                <td class='print Header'>
                                    <img alt='Close' src='../../images/close.gif' style='width: 16px; height: 16px;'
                                        onclick="closeAddr()" /></td>
                            </tr>
                        </table>
                    </div>
                    <div class='print DcrDispHPadFixNB' style="border: 1; height: 85px; overflow-y: auto;
                        border-color: white; background-color: #E2E1DB">
                        <asp:CheckBoxList ID="CblDesign" Width="280px" CssClass="print" BorderWidth="1" BorderColor="white"
                            BackColor="#E2E1DB" Font-Bold="true" runat="server" RepeatColumns="8" onclick="addSelAdd(this,txtAddr,CblSSCode,ddlsf,lblCount);add(this,txtAddr,CblSSCode,ddlsf,ddlDiv,lblCount,ddlspan)">
                        </asp:CheckBoxList>
                        <table style="visibility: hidden; width: 280px;" cellpadding="5" id="ddlDiv">
                            <tr>
                                <td nowrap >
                                    <span id="ddlspan" class="print">Managers:</span>
                                    <asp:DropDownList ID="ddlsf" Width="225px" runat="server" CssClass="DROPDOWNLIST"
                                        onchange="ddlsel(this,txtAddr,CblSSCode,CblDesign,lblCount)">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblCount" runat="server" CssClass="print" Text="No .of Field Force selected:"></asp:Label><br />
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="Name" BorderWidth="1" BorderColor="white"
                        BackColor="#E2E1DB" Width="280px" CssClass='print DcrDispHPadFixNB'></asp:Label>
                    <div style="overflow: scroll; height: 170px; border: 1; border-color: Gray">
                        <asp:CheckBoxList ID="CblSSCode" CssClass="checklist" runat="server" RepeatColumns="1"
                            Width="300px" onclick="addSf(this,txtAddr,lblCount);">
                        </asp:CheckBoxList>.
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnl2" BackColor="white" BorderColor="ActiveBorder" BorderWidth="1"
                    runat="server" Width="280px" Height="280px" Style="display: none;">
                    <div style="border-collapse: collapse; width: 280px;" class="HeaderPrint">
                        <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                            <tr>
                                <td class='print Header' style='width: 280px'>
                                    &nbsp;<span class='itemImage'><img alt='Address Book' src="../../Images/Address_Book_Icon.gif" /></span>Address
                                    Book</td>
                                <td class='print Header'>
                                    <img alt='Close' src='../../images/close.gif' style='width: 16px; height: 16px;'
                                        onclick="closeAddr()" /></td>
                            </tr>
                        </table>
                    </div>
                    <div class='print DcrDispHPadFixNB' style="border: 1; width: 280px; height: 85px;
                        overflow-y: auto; border-color: white; background-color: #E2E1DB">
                        <asp:CheckBoxList ID="CblDesign1" Width="280px" CssClass="print" BorderWidth="1"
                            BorderColor="white" BackColor="#E2E1DB" Font-Bold="true" runat="server" RepeatColumns="8"
                            onclick="addSelAdd(this,txtAddr1,CblSSCode1,ddlsf1,lblCount1);add(this,txtAddr1,CblSSCode1,ddlsf1,ddlDiv1,lblCount1,ddlspan1)">
                        </asp:CheckBoxList>
                        <table style="visibility: hidden; width: 280px;" cellpadding="5" id="ddlDiv1">
                            <tr>
                                <td nowrap >
                                    <span id="ddlspan1" class="print">Managers:</span>
                                    <asp:DropDownList ID="ddlsf1" Width="225px" runat="server" CssClass="DROPDOWNLIST"
                                        onchange="ddlsel(this,txtAddr1,CblSSCode1,CblDesign1,lblCount1)">
                                    </asp:DropDownList><br />
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblCount1" runat="server" CssClass="print" Text="No .of Field Force selected:"></asp:Label> 
                    </div>
                    <asp:Label ID="Label7" runat="server" Text="Name" Width="280px" CssClass='print DcrDispHPadFixNB'></asp:Label>
                    <div style="overflow: scroll; height: 170px; border: 1; border-color: Gray">
                        <asp:CheckBoxList ID="CblSSCode1" CssClass="checklist" runat="server" RepeatColumns="1"
                            Width="400px" onclick="addSf(this,txtAddr1,lblCount1);">
                        </asp:CheckBoxList></div>
                </asp:Panel>
                <asp:Panel ID="Pnl3" BackColor="white" BorderColor="ActiveBorder" BorderWidth="1"
                    runat="server" Width="280px" Height="280px" Style="display: none;">
                    <div style="border-collapse: collapse; width: 280px;" class="HeaderPrint">
                        <table border='0' style='border-collapse: collapse; width: 280px; height: 100%'>
                            <tr>
                                <td class='print Header' style='width: 280px'>
                                    &nbsp;<span class='itemImage'><img alt='Address Book' src="../../Images/Address_Book_Icon.gif" /></span>Address
                                    Book</td>
                                <td class='print Header'>
                                    <img alt='Close' src='../../images/close.gif' style='width: 16px; height: 16px;'
                                        onclick="closeAddr()" /></td>
                            </tr>
                        </table>
                    </div>
                    <div class='print DcrDispHPadFixNB' style="border: 1; width: 280px; height: 85px;
                        overflow-y: auto; border-color: white; background-color: #E2E1DB;">
                        <asp:CheckBoxList ID="CblDesign2" Width="280px" CssClass="print" BorderWidth="1"
                            BorderColor="white" BackColor="#E2E1DB" Font-Bold="true" runat="server" RepeatColumns="15"
                            onclick="addSelAdd(this,txtAddr2,CblSSCode2,ddlsf2,lblCount2);add(this,txtAddr2,CblSSCode2,ddlsf2,ddlDiv2,lblCount2,ddlspan2)">
                        </asp:CheckBoxList>
                        <table style="visibility: hidden; width: 300px;" cellpadding="5" id="ddlDiv2">
                            <tr>
                                <td nowrap >
                                    <span id="ddlspan2" class="print">Managers:</span>
                                    <asp:DropDownList ID="ddlsf2" Width="225px" runat="server" CssClass="DROPDOWNLIST"
                                        onchange="ddlsel(this,txtAddr2,CblSSCode2,CblDesign2,lblCount2)">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblCount2" runat="server" CssClass="print" Text="No .of Field Force selected:"></asp:Label>
                    </div>
                    <asp:Label ID="Label3" runat="server" Text="Name" Width="280px" CssClass='print DcrDispHPadFixNB'></asp:Label>
                    <div style="overflow: scroll; height: 170px; border: 1; border-color: Gray">
                        <asp:CheckBoxList ID="CblSSCode2" CssClass="checklist" runat="server" RepeatColumns="1"
                            Width="280px" onclick="addSf(this,txtAddr2,lblCount2);">
                        </asp:CheckBoxList></div>
                </asp:Panel>
                <input id="hdnSutunlar" type="hidden" />&nbsp;
                <br />
                <asp:Panel ID="PnlAttach" runat="server" Height="179px" Width="500px" Style="display: none;">
                    <div style="border-collapse: collapse; width: 500px;">
                        <table>
                            <tr>
                                <td class="HeaderPrint" width="485px">
                                    Attachment
                                </td>
                                <td>
                                    <img alt='Close' src='../../images/close.gif' style='width: 16px; height: 16px;'
                                        onclick="closeAttach()" /></td>
                            </tr>
                        </table>
                    </div>
                    <iframe id="ifram3" src="attachView.aspx" style="position: absolute; width: 500px;
                        height: 179px" scrolling="no" runat="server"></iframe>
                </asp:Panel>
                <asp:HiddenField ID="hdn" runat="server" />
                <asp:HiddenField ID="hdnBCC" runat="server" />
                <asp:HiddenField ID="hdnval" runat="server" />
                <asp:HiddenField ID="Flag" runat="server" />
                <asp:HiddenField ID="hdnSf" runat="server" />
                <asp:HiddenField ID="hdnDiv" runat="server" />
                <input id="hdnDesig" type="hidden" />
            </div>

      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
