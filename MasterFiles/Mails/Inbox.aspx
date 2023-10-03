<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="MasterFiles_Mails_Inbox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ereporting and sales analysis</title>
   <link type="text/css" rel="stylesheet" href="../css/style.css" />
      <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>

    <script type="text/javascript" language="javascript">
    var val=""

function setVisble()
{
    with(window.parent.document)
    {   
        frm1=frames["Iframe1"].frmInbox.document;
        frm1.getElementById("hdnUrl").value=window.parent.aspnetForm.document.getElementById('ctl00_ContentPlaceHolder1_hdnVal').value;
                   
    }
    with(frmInbox)
    {
        hdnBcc.value='';
        hdnBccsf.value='';
        Loading.style.display='none';
        var qrStr=window.location.search; 
        var spQrStr=qrStr.substring(1);
        var arrQry =new Array();
        arr=spQrStr.split('&');
        arr2=arr[0].split('=');
        
        if(divInboxList.style.display=='')
        {
            if(arr2[1]=='inbox')
            {
               Panel1.disabled='disabled';
               tdDel.disabled='disabled';
               tdrplyAll.disabled='disabled';
            }
            else  if(arr2[1]=='sent')
            {
              Panel1.disabled='disabled';
              tdDel.disabled='';
              tdrplyAll.disabled='disabled';
            }
            else
            {
                Panel1.disabled='';
                tdDel.disabled='';
                tdrplyAll.disabled='disabled';
            }
           tdrply.disabled='disabled';
           tdFw.disabled='disabled';
      }
  }

  </script>

</head>

<body onload='setVisble()' style="background-color: White; height: 100%;">
    <div id="Loading" style="font-weight: bold;">
        <center>
            Loading Please Wait....</center>
    </div>
    <form id="frmInbox" runat="server" style="height: 100%; width: 100%;">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
        </asp:ScriptManager>
        <div class="DcrDispHPadFix" style="border-width: 0px; width: 100%">
            <table width="100%" id="tmp" align="Center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='print' nowrap>
                    </td>
                    <td class='print' id='tdDel' width="5%" nowrap style="cursor: hand; border-style: solid;
                        border-width: 0px;" valign='middle' title='Delete the Selected Mail(s)'>
                        <div id="divDel" onclick='return MoveFlder(0)'>
                            <span class='itemImage'>
                                <img alt='Delete the Selected Mail(s)' src="../../images/transparent.ico" style="height: 14px" /></span>&nbsp;Delete</div>
                    </td>
                    <td class='print' width="3%" nowrap style='text-align: center' valign='middle'>
                        |</td>
                    <td class='print' id='tdrply' width="5%" nowrap style="cursor: hand; border-style: solid;
                        border-width: 0px;" valign='middle' title='Reply to a Current Read Mail' onclick="val='rly';RlyFun();">
                        <span class='itemImage'>
                            <img alt='Send a Composed Mail' src="../../images/ReplyIcon.gif" style="height: 17px" /></span>Reply&nbsp;</td>
                    <td class='print' width="5%" style='text-align: center; display: none;' valign='middle'>
                        |</td>
                    <td class='print' id='tdrplyAll' width="3%" nowrap style='cursor: hand; border-style: solid;
                        display: none; border-width: 0px; width: 60px;' valign='middle' title='Send a Composed Mail'
                        onclick="val='rlyAll';RlyFun();">
                        <span class='itemImage'>
                            <img alt='Send a Composed Mail' src="../../images/ReplyIcon.gif" style="height: 17px" /></span>Reply&nbsp;All</td>
                    <td class='print' width="3%" nowrap style='text-align: center' valign='middle'>
                        |</td>
                    <td id='tdFw' class='print' width="5%" nowrap style="cursor: hand; border-style: solid;
                        border-width: 0px;" valign='middle' title='Forward  To a Current Read Mail' runat="server"
                        onclick="val='Fwd';RlyFun();">
                        <span class='itemImage' id="spanAttach" runat="server">
                            <img alt='Attach a file' src="../../images/ForwardIcon.jpg" style="width: 17px; height: 14px" /></span>&nbsp;
                        Forward&nbsp;</td>
                    <td class='print' width="3%" nowrap style='text-align: center' valign='middle'>
                        |</td>
                    <td class='print' id="Panel1" width="5%" runat="server"  style='cursor: hand;
                        border-style: solid; border-width: 0px; width: 70px;' valign='middle' title='Selected Mail(s) Move To Folder'>
                        <span class='itemImage'>
                            <img alt='Moved To' src="../../images/Movetofld.jpg" style='width: 16px; height: 16px' /></span>Moved
                        To
                    </td>
                    <td class='print' width="3%" id="tdCls0"   style='display: none;   text-align: center' valign='middle'>
                        |</td>
                    <td class='print'  style='cursor: hand; border-style: solid; display: none; border-width: 0px;
                        width: 70px;'  
                        valign='middle' onclick='tkPrn()' id='TdBtnPrint' title='Print Mail'>
                        <span class='itemImage'>
                            <img alt='Print Mail' src='../../images/print.gif' style='width: 16px; height: 16px' /></span>Print&nbsp;</td>
                    <td class='print' id="tdCls1" width="3%" nowrap style='display: none; text-align: center'
                        valign='middle'>
                        |</td>
                    <td class='print' id="tdClose" width="5%" runat="server" nowrap style='display: none;
                        cursor: hand; border-style: solid; border-width: 0px; width: 90px;' valign='middle'
                        title='Close The Current Window' onclick="val='Del';RlyFun();">
                        <span class='itemImage'>
                            <img alt='Close' src="../../images/New.gif" style='width: 16px; height: 16px' /></span>Close
                    </td>
                    <td class="print" style="width: 80%; text-align: right">
                        <div id="tdDate">
                            <asp:DropDownList ID="ddlMon" Width="75px" AutoPostBack="true" Style="background: #E2E1E0;" CssClass="DropDownList"
                                 runat="server">
                                <asp:ListItem Text="January" Value="1">
                                </asp:ListItem>
                                <asp:ListItem Text="February" Value="2">
                                </asp:ListItem>
                                <asp:ListItem Text="March" Value="3">
                                </asp:ListItem>
                                <asp:ListItem Text="April" Value="4">
                                </asp:ListItem>
                                <asp:ListItem Text="May" Value="5">
                                </asp:ListItem>
                                <asp:ListItem Text="June" Value="6">
                                </asp:ListItem>
                                <asp:ListItem Text="July" Value="7">
                                </asp:ListItem>
                                <asp:ListItem Text="Augest" Value="8">
                                </asp:ListItem>
                                <asp:ListItem Text="September" Value="9">
                                </asp:ListItem>
                                <asp:ListItem Text="October" Value="10">
                                </asp:ListItem>
                                <asp:ListItem Text="November" Value="11">
                                </asp:ListItem>
                                <asp:ListItem Text="December" Value="12">
                                </asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlYr" Width="75px" AutoPostBack="true" Style="background: #E2E1E0;" CssClass="DropDownList" runat="server">
                            </asp:DropDownList></div>
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
        </div>
        <table id="divInboxList" style="border-top-style: solid; border-top-width: thick;
            border-top-color: White; width: 100%;">
            <tr>
                <td height="50%" valign="top">
                    <asp:GridView ID="gv1" runat="server" BorderWidth="0px" BorderColor="white" BorderStyle="None"
                        CellPadding="0" HeaderStyle-HorizontalAlign="Left" SkinID="aa" CellSpacing="0"
                        AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true"
                        HeaderStyle-BackColor=" #ededea" EmptyDataText="No Mail(s)..." AllowPaging="true"
                        PageSize="15" PagerSettings-Position="Top" PagerSettings-Mode="NumericFirstLast"
                        PagerSettings-Visible="true" PagerStyle-CssClass="Hprint" PagerStyle-HorizontalAlign="Right"
                        PagerStyle-BackColor="Honeydew" AllowSorting="true" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width='1%' ItemStyle-HorizontalAlign="center">
                                <HeaderTemplate>
                                    <input id="cbSelectAll" type="checkbox" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="cbSelectAll" type="checkbox" runat="server" value="" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="From_Name" ItemStyle-HorizontalAlign="Left" HeaderText="From"
                                HeaderStyle-Width='30%' ItemStyle-CssClass="print" SortExpression="From_Name" />
                            <asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderText="Subject"
                                HeaderStyle-Width='30%' ItemStyle-CssClass="print" SortExpression="Mail_Subject" />
                            <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width='15%' ItemStyle-CssClass="print" SortExpression="Mail_Sent_Time" />
                            <asp:BoundField DataField="Mail_Attachement" ItemStyle-HorizontalAlign="center" HeaderStyle-Width='7%'
                                ItemStyle-CssClass="print" />
                            <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                ItemStyle-CssClass="print" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnslNo" runat="server"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#DDEEFF" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div id="DivView" runat="Server" style="display: none; border: solid 3 white;">
            <div style="width: 100%">
                <table style="width: 100%; border: solid 3 white;" class="DcrDispHPadFix print">
                    <tr>
                        <td style="width: 7%">
                            From
                        </td>
                        <td style="width: 2%" align="right">
                            :</td>
                        <td>
                            <asp:Label ID="lblFrm" runat="server" Text="Label"></asp:Label><input type="hidden"
                                value="" id="hdnFrm" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 7%">
                            To</td>
                        <td style="width: 2%" align="right">
                            :</td>
                        <td>
                            <asp:Label ID="lblTo" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trCC" runat="server">
                        <td style="width: 7%">
                            CC
                        </td>
                        <td style="width: 2%" align="right">
                            :</td>
                        <td>
                            <asp:Label ID="lblCC" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 7%">
                            Subject:</td>
                        <td style="width: 2%" align="right">
                            :</td>
                        <td>
                            <asp:Label ID="lblSub" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 7%">
                            Sent Date</td>
                        <td style="width: 2%" align="right">
                            :</td>
                        <td>
                            <asp:Label ID="lblSentDt" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <img id="ImgAttach" src="../../Images/Attachment.gif"><asp:LinkButton ID="LbFileName"
                                Font-Size="Larger" OnClientClick="return ViewDownLoad()" runat="server">LinkButton</asp:LinkButton></td>
                    </tr>
                </table>
                <div id="divContent" style="width: 99.5%; height: 500px; border: solid 2px white;
                    overflow: auto;">
                </div>
            </div>
        </div>
        <cc1:PopupControlExtender ID="popPnlFlder" Position="Bottom"  runat="server" TargetControlID="Panel1"
            PopupControlID="pnlFolder">
        </cc1:PopupControlExtender>
        <asp:Panel ID="pnlFolder" runat="server" Height="50px" Width="75px" BackColor="#DDEEFF"
            Style="display: none">
            <asp:GridView ID="gvMveFlder" BorderWidth="0" BorderColor="white" runat="server"
                Height="150px" ShowHeader="false" onmouseleave="CloseFld()" AutoGenerateColumns="false"
                BackColor="#DDEEFF" GridLines="none" CssClass="print" Width="126px">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="25px">
                        <ItemTemplate>
                            <img id="img1" alt="img" src="../../images/Closed.ICO" style="height: 20px;" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-HorizontalAlign="left" DataField="move_mailfolder_name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnCode" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:HiddenField ID="hdnMveFldrId" runat="server" />
            <asp:HiddenField ID="hdnState" runat="server" />
        </asp:Panel>
        <asp:HiddenField ID="hdnFileName" runat="server" />
        <input id="hdnfrmCode" type="hidden" />
        <input id="hdnBccsf" type="hidden" />
        <input id="hdnBcc" type="hidden" />
        <input id="hdnCC" type="hidden" />
        <asp:HiddenField ID="hdnSf" runat="server" />
        <asp:HiddenField ID="hdnDiv" runat="server" />
        <asp:HiddenField ID="hdnUrl" runat="server" />
        <asp:HiddenField ID="hdnMsgDisp" runat="server" Value="0" />
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
