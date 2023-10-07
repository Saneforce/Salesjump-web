<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail_Head.aspx.cs" Inherits="MasterFiles_Mails_Mail_Head"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail</title>
    <link type="text/css" rel="stylesheet" href="../../css/sfm_style.css" />
    
    <script type="text/javascript">

        function CCFunc(cc) {
            if (cc.id == "idCC") {

                //clearAddr(1);
                if (cc.childNodes[1].innerText == 'Remove Cc') {
                    cc.childNodes[1].innerText = 'Add Cc'; TrCC.style.display = 'none'; cc.childNodes[1].title = 'Add to CC'; return true;
                }
                if (cc.childNodes[1].innerText == 'Add Cc') {
                    cc.childNodes[1].innerText = 'Remove Cc'; cc.childNodes[1].title = 'Remove From CC'; TrCC.style.display = ''; return true;
                }
            }
            if (cc.id == "idBCC") {

                //clearAddr(2);
                if (cc.childNodes[1].innerText == 'Remove Bcc') {
                    cc.childNodes[1].innerText = 'Add Bcc'; TrBCC.style.display = 'none'; cc.childNodes[1].title = 'Add From BCC'; return true;
                }
                if (cc.childNodes[1].innerText == 'Add Bcc') {
                    cc.childNodes[1].innerText = 'Remove Bcc'; TrBCC.style.display = ''; cc.childNodes[1].title = 'Remove From BCC'; return true;
                }
            }
        }

        function ShowInbox() {

            var pnlInbox = document.getElementById('pnlInbox');
            pnlInbox.style.display = "block";
            pnlInbox.style.visibility = "visible";

            var pnlFolder = document.getElementById('pnlFolder');
            pnlFolder.style.display = "none";
            pnlFolder.style.visibility = "hidden";

            var btnInbox = document.getElementById('btnInbox');
            btnInbox.style.backgroundColor = "Gray";

            var tdInbox = document.getElementById('tdInbox');
            tdInbox.style.backgroundColor = "Gray";

            var GridView = document.getElementById('gvInbox');
            GridView.style.display = "block";
            GridView.visibility = "visible";

            var btnCompose = document.getElementById('btnCompose');
            btnCompose.style.backgroundColor = "Green";
            var pnlCompose = document.getElementById('pnlCompose');
            pnlCompose.style.display = "none";
            pnlCompose.style.visibility = "hidden";
            var tdCompose = document.getElementById('tdCompose');
            tdCompose.style.backgroundColor = "";

            var tdSent = document.getElementById('tdSent');
            tdSent.style.backgroundColor = "";
            var btnSentItem = document.getElementById('btnSentItem');
            btnSentItem.style.backgroundColor = "Green";
            var pnlSent = document.getElementById('pnlSent');
            pnlSent.style.display = "none";
            pnlSent.style.visibility = "hidden";

            var pnlViewMail = document.getElementById('pnlViewMail');
            pnlViewMail.style.display = "none";
            pnlViewMail.style.visibility = "hidden";
            var btnView = document.getElementById('btnView');
            btnView.style.backgroundColor = "Green";
            var tdViewed = document.getElementById('tdViewed');
            tdViewed.style.backgroundColor = "";


        }
        function Load() {
            ShowProgress();
            return true;
        }
        function valid() {
            with (frmAddr) {
                if (txtAddr.value == '') { alert('Select the Fieldforce to whom to send'); txtAddr.focus(); return false; }
                if (txtSub.value == '') { alert('Enter Subject'); txtSub.focus(); return false; }
                if (txtMsg.value == '') {

                    confrm = confirm("Send this subject without text in the body?");
                    if (confrm == true) {
                        return true;
                    }
                    else {
                        txtMsg.focus();
                        return false;
                    }
                }
            }
        }

    </script>    
    <script type="text/javascript">
        //Modified by Sridevi - Starts
        function SentMail() {

            var pnlCompose = document.getElementById('pnlCompose');
            var pnlInbox = document.getElementById('pnlInbox');
            var pnlViewMail = document.getElementById('pnlViewMail');
            var pnlSent = document.getElementById('pnlSent');
            var pnlFolder = document.getElementById('pnlFolder');

            var grdSent = document.getElementById('grdSent');
            var GridView = document.getElementById('gvInbox');

            var tdInbox = document.getElementById('tdInbox');
            var tdCompose = document.getElementById('tdCompose');
            var tdSent = document.getElementById('tdSent');
            var tdViewed = document.getElementById('tdViewed');

            var btnInbox = document.getElementById('btnInbox');
            var btnSentItem = document.getElementById('btnSentItem');
            var btnView = document.getElementById('btnView');
            var btnCompose = document.getElementById('btnCompose');

            btnView.style.backgroundColor = "Green";
            btnSentItem.style.backgroundColor = "Gray";
            btnInbox.style.backgroundColor = "Green";
            btnCompose.style.backgroundColor = "Green";

            tdInbox.style.backgroundColor = "";
            tdCompose.style.backgroundColor = "";
            tdSent.style.backgroundColor = "Gray";
            tdViewed.style.backgroundColor = "";


            pnlInbox.style.display = "block";
            pnlInbox.style.visibility = "visible";

            GridView.style.display = "none";
            GridView.style.visibility = "hidden";

            //            alert("hi22");
            //            if (grdSent.style.display == "none") {
            //                grdSent.style.display = "block";
            //                grdSent.style.visibility = "visible";
            //            }
            //            alert("hi23");

            pnlViewMail.style.display = "none";
            pnlViewMail.style.visibility = "hidden";

            pnlSent.style.display = "block";
            pnlSent.style.visibility = "visible";

            //            var pnlFolder = document.getElementById('pnlFolder');
            //            pnlFolder.style.display = "none";
            //            pnlFolder.style.visibility = "hidden";

        }

    </script>

    <script type="text/javascript">
        function ShowMail() {

            var pnlInbox = document.getElementById('pnlInbox');
            var pnlCompose = document.getElementById('pnlCompose');
            var pnlSent = document.getElementById('pnlSent');
            var tdInbox = document.getElementById('tdInbox');

            var tdCompose = document.getElementById('tdCompose');
            var tdSent = document.getElementById('tdSent');
            var tdViewed = document.getElementById('tdViewed');
            var btnInbox = document.getElementById('btnInbox');
            var btnSentItem = document.getElementById('btnSentItem');
            var btnView = document.getElementById('btnView');
            var btnCompose = document.getElementById('btnCompose');

            btnView.style.backgroundColor = "Green";
            btnSentItem.style.backgroundColor = "Green";
            btnInbox.style.backgroundColor = "Green";
            tdInbox.style.backgroundColor = "";
            tdCompose.style.backgroundColor = "Gray";
            btnCompose.style.backgroundColor = "Gray";
            tdSent.style.backgroundColor = "";
            tdViewed.style.backgroundColor = "";

            pnlCompose.style.display = "block";
            pnlCompose.style.visibility = "visible";
            pnlInbox.style.display = "none";
            pnlInbox.style.visibility = "hidden";
            pnlSent.style.display = "none";
            pnlSent.style.visibility = "hidden";

            //            var txtToAddr = document.getElementById('<%= txtAddr.ClientID %>');
            //            txtToAddr.value = '';
        }
    </script>
    <script type="text/javascript">
        function ViewedMail() {

            var pnlCompose = document.getElementById('pnlCompose');
            var pnlInbox = document.getElementById('pnlInbox');

            var pnlSent = document.getElementById('pnlSent');
            var btnCompose = document.getElementById('btnCompose');
            var GridView = document.getElementById('gvInbox');

            var tdInbox = document.getElementById('tdInbox');
            var tdCompose = document.getElementById('tdCompose');
            var tdSent = document.getElementById('tdSent');
            var tdViewed = document.getElementById('tdViewed');
            var btnInbox = document.getElementById('btnInbox');
            var btnSentItem = document.getElementById('btnSentItem');
            var btnView = document.getElementById('btnView');

            btnView.style.backgroundColor = "Gray";
            btnSentItem.style.backgroundColor = "Green";
            btnInbox.style.backgroundColor = "Green";
            tdInbox.style.backgroundColor = "";
            tdCompose.style.backgroundColor = "";
            tdSent.style.backgroundColor = "";
            tdViewed.style.backgroundColor = "Gray";
            btnCompose.style.backgroundColor = "Green";

            pnlInbox.style.display = "block";
            pnlInbox.style.visibility = "visible";

            GridView.style.display = "none";
            GridView.visibility = "hidden";

            pnlCompose.style.display = "none";
            pnlCompose.style.visibility = "hidden";
            pnlViewMail.style.display = "block";
            pnlViewMail.style.visibility = "visible";
            pnlSent.style.display = "none";
            pnlSent.style.visibility = "hidden";

        }
    </script>
    <script type="text/javascript">
        function ShowFolder() {

            var pnlInbox = document.getElementById('pnlInbox');
            var pnlCompose = document.getElementById('pnlCompose');
            var pnlSent = document.getElementById('pnlSent');
            var tdInbox = document.getElementById('tdInbox');
            var btnCompose = document.getElementById('btnCompose');


            var tdCompose = document.getElementById('tdCompose');
            var tdSent = document.getElementById('tdSent');
            var tdViewed = document.getElementById('tdViewed');
            var btnInbox = document.getElementById('btnInbox');
            var btnSentItem = document.getElementById('btnSentItem');
            var btnView = document.getElementById('btnView');

            var pnlViewMail = document.getElementById('pnlViewMail');
            var GridviewMail = document.getElementById('grdView');
            //GridviewMail.style.display = "none";
            //GridviewMail.visibility = "hidden";
            pnlViewMail.style.display = "none";
            pnlViewMail.style.visibility = "hidden";

            var GridView = document.getElementById('gvInbox');
            GridView.style.display = "none";
            GridView.visibility = "hidden";

            btnView.style.backgroundColor = "Green";
            btnSentItem.style.backgroundColor = "Green";
            btnInbox.style.backgroundColor = "Green";
            tdInbox.style.backgroundColor = "";
            tdCompose.style.backgroundColor = "Green";
            tdSent.style.backgroundColor = "";
            tdViewed.style.backgroundColor = "";
            btnCompose.style.backgroundColor = "Green";

            pnlInbox.style.display = "block";
            pnlInbox.style.visibility = "visible";
            pnlCompose.style.display = "none";
            pnlCompose.style.visibility = "hidden";
            pnlSent.style.display = "none";
            pnlSent.style.visibility = "hidden";
            //txtToAddr.value = '';

        }
        // Modified by Sridevi Ends
    </script>
    <script type="text/javascript">
        function LimtCharacters(txtMsg, CharLength, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
    </script>
    <script type="text/javascript">
        function generateTable() {
            var data = $('textarea[name=excel_data]').val();
            console.log(data);
            var rows = data.split("\n");

            var table = $('<table />');

            for (var y in rows) {
                var cells = rows[y].split("\t");
                var row = $('<tr />');
                for (var x in cells) {
                    row.append('<td>' + cells[x] + '</td>');
                }
                table.append(row);
            }

            // Insert into DOM
            $('#excel_table').html(table);
        }
    </script>
    <script type="text/javascript">
        function imgSearch(event) {
            alert('test');
            if (event.which == 13) {

                $('#imgSearch').trigger('click');
            }
        }
    </script>
    <style type="text/css">
        .img1
        {
            margin: 0px 0px 0px 0px;
            background: url("../../images/sendicon.gif") left center no-repeat;
            padding: 0em 1.2em;
            font: 8pt "tahoma";
            color: #336699;
            text-decoration: none;
            font-weight: normal;
            letter-spacing: 0px;
        }
        .gvBorder
        {
            border: 1px;
        }
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
        .pgr1
        {
            z-index: 1;
            left:140px; 
            top:525px;
            position: absolute;
            width: 89%;
        }
        .CursorPointer
        {
          cursor: default;
        }
        .mGrid .pgr1 {background: #A6A6D2; }
    .mGrid .pgr1 table { margin: 5px 0; }
    .mGrid .pgr1 td { border-width: 0; padding: 0 6px; text-align:left; border-left: solid 0px #666; font-weight: bold; color: Red; line-height: 12px; }   
    .mGrid .pgr1 th { background: #A6A6D2;}
    .mGrid .pgr1 a { color: Blue;  text-decoration: none; }
    .mGrid .pgr1 a:hover { color: White; text-decoration: none; }
    </style>
    <script type="text/javascript" src="../../JsFiles/common.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
                });

    </script>
    <script type="text/javascript">
        $(function () {
            $('#chkDesgn_OnSelectedIndexChanged').click(function () {
                var status = $('#chkDesgn').is(':checked');

            })
        });
    </script>
</head>
<body>
    <form id="frmAddr" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table id="tblLeft" height="200px" style="position: absolute; border-collapse: collapse;
            width: 100%;" border="0">
            <tr>
                <td valign="top" nowarp colspan="2">
                    <table border="0" style="border-collapse: collapse; width: 100%" class="DcrDispHPadFix">
                        <tr>
                            <td style="color: brown; width: 30%; font-weight: bold; height: 17px;" class="print"
                                nowarp>
                                Welcome&nbsp;
                                <asp:Label ID="lblSfName" runat="Server"></asp:Label>
                            </td>
                            <td style="height: 17px" align="center" width="40%">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Small" Text=""></asp:Label>
                            </td>
                            <td style="color: brown; width: 30%; font-weight: bold; height: 17px;" class="print"
                                nowarp align="Right">
                                <a href="../../index.aspx" title="Logout" class="Next">Logout&nbsp;</a>&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="tblFolder" border="0" style="border-collapse: collapse; width: 100%; height: 400px">
                        <tr>
                            <td style="height: 100%; width: 10%;" valign="top">
                                <table border="2" style="width: 100%; border-color: Gray; height: 100%;">
                                    <tr>
                                        <td class="print Header" style="width: 120; height: 18px;">
                                            <b>&nbsp; <span class="itemImage">
                                                <img alt="My Folders" src="../../Images/FOLDER.ICO" class="CursorPointer" style="width: 16px; height: 16px;" /></span>My<asp:LinkButton
                                                    ID="lnk" Style="text-decoration: none;" CssClass="CursorPointer" runat="server" Text=" " OnClick="OnClick_ShCut"></asp:LinkButton>Folders</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 100%;" width="100%" valign="top">
                                            <asp:Panel ID="pnlMoveFolder" runat="server" Height="500px">
                                                <%--<asp:Table ID="tbl" runat="server" Width="100%" Style="border-collapse: collapse;font-size:7pt;font-family:Verdana"
                                                    CellPadding="0" CellSpacing="0">--%>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grdClickFolder" runat="server" AutoGenerateColumns="False" OnRowCommand="grdClickFolder_RowCommand"
                                                                OnRowDataBound="grdClickFolder_RowDataBound" Font-Names="Verdana" Font-Size="8pt"
                                                                Width="100%" HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true"
                                                                AllowPaging="True" GridLines="None">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="imgFolder" ImageUrl="~/Images/Closed.ICO" Width="16px" Height="16px"
                                                                                runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnFolder" Style="text-decoration: none; color: Black; font-family: Times New Roman;"
                                                                                CommandName="Folder" runat="server" Text='<% #Eval("Move_MailFolder_Name") %>'>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Move_MailFolder_Name") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--</asp:Table>--%>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 100%;" valign="top">
                                <table border="2" style="width: 100%; height: 528px; border-collapse: collapse; border-color: Gray">
                                    <tr>
                                        <td valign="top" style="height: 20px; width: 90%;" nowrap class="print">
                                            <div style="overflow: hidden; width: 100%;">
                                                <table style="border-collapse: collapse;" class="Header">
                                                    <tr>
                                                        <td valign="middle" style="height: 18px; width: 10px;" nowrap class="print">
                                                            &nbsp;
                                                        </td>
                                                        <td valign="middle" id="tdInbox" style="height: 18px; width: 110px; border-style: solid;
                                                            border-width: 0px;" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap runat="server" class="print"
                                                            title="View a Incoming Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                            <asp:ImageButton ID="ImgInbox" runat="server" CssClass="BUTTON" Width="20px" Height="20px"
                                                                                    OnClick="btnInbox_Click" ImageUrl="../../images/InBox.ico" /></span>
                                                               <%-- <img alt="View a Incoming Mail(s)" src="../../Images/InBox.ico" runat="server" onclick ="btnInbox_Click" />--%>
                                                                <asp:Button ID="btnInbox" Text="Inbox" runat="server" OnClick="btnInbox_Click" Style="background-color: Green;
                                                                    border: 0; color: White; font-weight: bold; margin-right: 20px" />&nbsp
                                                            </span>
                                                        </td>
                                                        <td valign="middle" id="tdCompose" style="height: 18px; width: 110px; border-style: solid;
                                                            border-width: 0px;" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Write a Mail">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage" style="width: 20px;">
                                                             <asp:ImageButton ID="ImgCompose" runat="server" CssClass="BUTTON" Width="20px" Height="20px"
                                                                                    OnClick="btnCompose_Onclick" ImageUrl="../../images/Writemail.ico" /></span>
                                                           
                                                                <%--<img alt="Write a Mail" src="../../Images/Writemail.ico" /></span> --%>
                                                                <b><span style="margin-right: 20px">
                                                                    <asp:Button ID="btnCompose" runat="server" Text="Compose" Style="background-color: Green;
                                                                        border: 0; color: White; font-weight: bold; margin-right: 20px" OnClick="btnCompose_Onclick" />
                                                                </span></b></span>
                                                        </td>
                                                        <%--<td valign="middle" id="tdSent" style="height: 18px; border-style: solid;
                                                            border-width: 0px;" onclick="SentMail()" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" runat="server" class="print" title="Sent Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                                <img alt="Sent Mail(s)"  src="../../images/SendIconNew.png" /></span>
                                                                 <asp:Button ID="btnSentItem" Text="Sent Item" runat="server" 
                                                                onclick="btnSentItem_Click" style="background-color:Green;border:0;color:White;
                                                                font-weight:bold;" />                                                                
                                                                </span>
                                                        </td>--%>
                                                        <td valign="middle" id="tdSent" style="height: 18px; width: 120px; border-style: solid;
                                                            border-width: 0px;" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Sent Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage" style="width: 20px;">
                                                              <asp:ImageButton ID="ImgSent" runat="server" CssClass="BUTTON" Width="20px" Height="20px"
                                                                                    OnClick="btnSentItem_Click" ImageUrl="../../images/SendIconNew.png" /></span>
                                                                <%--<img alt="Sent Mail(s)" src="../../images/SendIconNew.png" /></span>--%>
                                                                 <b><span style="margin-right: 20px">
                                                                    <asp:Button ID="btnSentItem" runat="server" Text="Sent Item" Style="background-color: Green;
                                                                        border: 0; color: White; font-weight: bold; margin-right: 20px" OnClick="btnSentItem_Click" />
                                                                </span></b></span>
                                                        </td>
                                                        <td valign="middle" id="tdViewed" style="height: 18px; width: 95px; border-style: solid;
                                                            border-width: 0px;" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Viewed Incoming Mail(s)">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage">
                                                            
                                                              <asp:ImageButton ID="ImgView" runat="server" CssClass="BUTTON" Width="20px" Height="20px"
                                                                                    OnClick="btnView_Click" ImageUrl="../../images/sendicon.png" /></span>
                                                                <%--<img alt="Viewed Incoming Mail(s)" src="../../images/sendicon.png" /></span>--%>
                                                                <asp:Button ID="btnView" Text="Viewed" runat="server" OnClick="btnView_Click" Style="background-color: Green;
                                                                    border: 0; color: White; font-weight: bold; margin-right: 20px" />
                                                                <%-- <b>&nbsp;Viewed&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>--%>
                                                            </span>
                                                        </td>
                                                        <td valign="middle" id="tdHome" style="height: 18px; width: 95px; border-style: solid;
                                                            border-width: 0px;" onmouseover="this.style.borderWidth='0.5px';"
                                                            onmouseout="this.style.borderWidth='0px';" nowrap class="print" title="Goto Home">
                                                            <span style="cursor: hand; width: 100%"><span class="itemImage ">
                                                            <asp:ImageButton ID="imgHome" runat="server" CssClass="BUTTON" Width="20px" Height="20px"
                                                                                    OnClick="Button1_Click" ImageUrl="../../images/logoff.jpg" /></span>
                                                              
                                                                <%--<img id="imgHome" runat="server" visible="false" alt="Viewed Incoming Mail(s)" src="../../images/logoff.jpg" /></span>--%>
                                                                <asp:Button ID="btnHome" Visible="false" Text="Home" runat="server" Style="background-color: Green;
                                                                    border: 0; color: White; font-weight: bold; margin-right: 20px" OnClick="Button1_Click" /></span>
                                                        </td>
                                                        <td valign="middle" style="height: 18px; width: 90%;" nowrap class="print">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" runat="server">
                                                    <tr>
                                                        <td>
                                                            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UdpnlAddress">
                                                                        <ProgressTemplate>
                                                                            <img id="Img1" alt="" src="../../Images/loading/loading19.gif" runat="server" /><span
                                                                                style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>--%>
                                                            <asp:Panel ID="pnlInbox" runat="server" Width="90%" Style="top: 49px; position: absolute;
                                                                visibility: hidden; margin: 0px; margin-left: 0px; display: inline">
                                                                <div class="DcrDispHPadFix" style="border-width: 1px; width: 99.2%">
                                                                    <table width="100%" id="tmp" align="Center">
                                                                        <tr>
                                                                            <td class='print'>
                                                                            </td>
                                                                            <td class='print' id='tdDel' width="5%" nowrap style="cursor: hand; border-style: solid;
                                                                                border-width: 0px; border: 1" valign='middle' title='Delete the Selected Mail(s)'>
                                                                                <div id="divDel">
                                                                                    <span class='itemImage'>
                                                                                        <img alt='Delete the Selected Mail(s)' src="../../images/transparent.ico" style="height: 14px;" /></span>
                                                                                    <asp:Button ID="btnDelete" runat="server" BorderStyle="None" Enabled="false" ForeColor="Black"
                                                                                        Style="margin-left: 0px" OnClick="btnDelete_Onclick" Text="Delete" /></div>
                                                                            </td>
                                                                            <td class='print' width="3%" style='text-align: center' valign='middle'>
                                                                                |
                                                                            </td>
                                                                            <td class='print' id='tdrply' width="5%" nowrap style="cursor: hand; border-style: solid;
                                                                                border-width: 0px;" valign='middle' title='Reply to a Current Read Mail' onclick="val='rly';RlyFun();">
                                                                                <span class='itemImage'>
                                                                                    <img alt='Send a Composed Mail' src="../../images/ReplyIcon.gif" style="height: 17px" /></span>
                                                                                <asp:Button ID="btnReply" runat="server" BorderStyle="None" Enabled="false" ForeColor="Gray"
                                                                                    Style="margin-left: 0px" Text="Reply" OnClick="btnReply_Onclick" />
                                                                            </td>
                                                                            <td class='print' width="5%" style='text-align: center; display: none;' valign='middle'>
                                                                                |
                                                                            </td>
                                                                            <td class='print' id='tdrplyAll' width="3%" nowrap style='cursor: hand; border-style: solid;
                                                                                display: none; border-width: 0px; width: 60px;' valign='middle' title='Send a Composed Mail'
                                                                                onclick="val='rlyAll';RlyFun();">
                                                                                <span class='itemImage'>
                                                                                    <img alt='Send a Composed Mail' src="../../images/ReplyIcon.gif" style="height: 17px" /></span>Reply&nbsp;All
                                                                            </td>
                                                                            <td class='print' width="3%" style='text-align: center' valign='middle'>
                                                                                |
                                                                            </td>
                                                                            <td id='tdFw' class='print' width="5%" style="cursor: hand; border-style: solid;
                                                                                border-width: 0px;" valign='middle' title='Forward  To a Current Read Mail' runat="server">
                                                                                <span class='itemImage' id="span1" runat="server">
                                                                                    <img alt='Attach a file' src="../../images/ForwardIcon.jpg" style="width: 17px; height: 14px" /></span>
                                                                                <asp:Button ID="btnForward" runat="server" BorderStyle="None" Enabled="true" ForeColor="Gray"
                                                                                    Style="margin-left: 0px;" OnClick="btnForward_Onclick" Text="Forward" />
                                                                            </td>
                                                                            <td class='print' width="3%" style='text-align: center' valign='middle'>
                                                                                |
                                                                            </td>
                                                                            <td class='print' id="Td1" width="14%" runat="server" style='cursor: hand; border-style: solid;
                                                                                border-width: 0px;' valign='middle' title='Selected Mail(s) Move To Folder'>
                                                                                <span class='itemImage'>
                                                                                    <img alt='Moved To' src="../../images/Movetofld.jpg" style='width: 16px; height: 16px' /></span>Moved
                                                                                To
                                                                                <asp:DropDownList ID="ddlMoved" runat="server" Width="80px" Height="20px" Enabled="false" OnSelectedIndexChanged="ddlMoved_OnSelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class='print' width="3%" id="tdCls0" style='display: none; text-align: center'
                                                                                valign='middle'>
                                                                                |
                                                                            </td>
                                                                            <td class='print' style='cursor: hand; border-style: solid; display: none; border-width: 0px;
                                                                                width: 70px;' valign='middle' onclick='tkPrn()' id='TdBtnPrint' title='Print Mail'>
                                                                                <span class='itemImage'>
                                                                                    <img alt='Print Mail' src='../../images/print.gif' style='width: 16px; height: 16px' /></span>Print&nbsp;
                                                                            </td>
                                                                            <td class='print' id="tdCls1" width="3%" nowrap style='display: none; text-align: center'
                                                                                valign='middle'>
                                                                                |
                                                                            </td>
                                                                            <td class='print' id="tdClose" width="5%" runat="server" nowrap style='display: none;
                                                                                cursor: hand; border-style: solid; border-width: 0px; width: 90px;' valign='middle'
                                                                                title='Close The Current Window' onclick="val='Del';RlyFun();">
                                                                                <span class='itemImage'>
                                                                                    <img alt='Close' src="../../images/New.gif" style='width: 16px; height: 16px' /></span>Close
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;&nbsp;<asp:Label ID="lblSubjectSearch" Text="Search" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtboxSearch" runat="server" Width="80px" Height="14px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgSearch" runat="server" CssClass="BUTTON" Width="20px" Height="20px"
                                                                                    OnClick="imgSearch_Click" ImageUrl="../../images/Search.png" />
                                                                            </td>
                                                                            <td class="print" style="width: 80%; text-align: left">
                                                                                <div id="tdDate" style="margin-left: 15px">
                                                                                    <asp:DropDownList ID="ddlMon" OnSelectedIndexChanged="ddlMon_OnSelectedIndex" Width="80px" Height="20px" onchange = "Load();"
                                                                                        AutoPostBack="true" Style="background: #E2E1E0;" CssClass="DropDownList" runat="server">
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
                                                                                        <asp:ListItem Text="August" Value="8">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="September" Value="9">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="October" Value="10">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="November" Value="11">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="December" Value="12">
                                                                                        </asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:DropDownList ID="ddlYr" AutoPostBack="true" OnSelectedIndexChanged="ddlYr_OnSelectedIndex" onchange = "Load();"
                                                                                        Width="60px" Style="background: #E2E1E0;" CssClass="DropDownList" Height="20px" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--<asp:UpdatePanel ID="updPanelload" runat="server">
                                                    <ContentTemplate>--%>
                                                <table id="divInboxList" style="border-top-width: thick; border-top-color: White;
                                                    width: 100.1%; margin: 0px">
                                                    <tr>
                                                        <td height="50%" valign="top" class="gvGrid">
                                                            <asp:GridView ID="gvInbox" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInbox_RowDataBound"
                                                                Width="100%" HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true" OnRowCommand="gvInbox_RowCommand"
                                                                AllowPaging="True" PageSize="15" HeaderStyle-BackColor="#ededea" OnSelectedIndexChanged="OnSelectedIndexChanged"
                                                                CssClass="mGrid" HeaderStyle-HorizontalAlign="Left" OnPageIndexChanging="gvInbox_OnPageIndexChanging"
                                                                PagerStyle-CssClass="pgr" EmptyDataText="No Mail(s)..." GridLines="None">
                                                                <HeaderStyle BorderWidth="1" />
                                                                <RowStyle Height="20px" />
                                                                <PagerStyle CssClass="pgr" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First"
                                                                    LastPageText="Last" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="cbSelectAll_OnCheckedChanged"
                                                                                Style="margin: 2px" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkId" runat="server" AutoPostBack="true" OnCheckedChanged="chkId_OnCheckedChanged"
                                                                                Style="margin: 2px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="From_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="From" HeaderStyle-Width="280px" ItemStyle-CssClass="print" SortExpression="From_Name" />
                                                                    <%--<asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Subject" HeaderStyle-Width="20%" ItemStyle-CssClass="print" SortExpression="Mail_Subject" />--%>
                                                                    <%-- New changes done by saravanan start  --%>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" HeaderStyle-Width='15%'
                                                                        ItemStyle-CssClass="print" SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                           <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject") %>'  Visible ="false" />
                                                                           <asp:LinkButton ID = "lnk_MailSub" runat ="server" Text = '<% #Eval("Mail_subject") %>' 
                                                                           CommandArgument='<%# Eval("Trans_sl_No") %>'  CommandName="ViewMail" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- New changes done by saravanan End  --%>
                                                                    <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="145px" ItemStyle-CssClass="print"
                                                                        HtmlEncode="false" DataFormatString="{0:d}" SortExpression="Mail_Sent_Time" />
                                                                    <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblView" Visible="false" runat="server" Text='<% #Eval("Mail_Attachement") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                                                        Visible="false" ItemStyle-CssClass="print" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle Font-Names="Verdana" ForeColor="Black" BackColor="AliceBlue" BorderColor="Black"
                                                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" CssClass="pgr1" />
                                                                <FooterStyle BackColor="#DDEEFF" />
                                                                <EmptyDataRowStyle Font-Size="9pt" ForeColor="Red" Font-Names="Verdana" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlViewMail" runat="server" Width="90%" Style="top: 76px; margin-left: 2px;
                                    position: absolute; visibility: hidden;">
                                    <table id="Table1" style="width: 99.4%; margin: 0px">
                                        <tr>
                                            <td height="50%" valign="top">
                                                <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdView_RowDataBound"
                                                    HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true" AllowPaging="True" OnRowCommand="grdView_RowCommand"
                                                    PageSize="15" HeaderStyle-BackColor="#ededea" OnSelectedIndexChanged="OnSelectedIndexChanged"
                                                    CssClass="mGrid" OnPageIndexChanging="grdView_PageIndexChanging" HeaderStyle-HorizontalAlign="Left"
                                                    PagerStyle-CssClass="pgr" EmptyDataText="No Mail(s)..." GridLines="None">
                                                    <HeaderStyle BorderWidth="1" />
                                                    <PagerStyle CssClass="pgr" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First"
                                                        LastPageText="Last" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width='1%' ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="grdViewcbSelectAll_OnCheckedChanged"
                                                                    Style="margin: 2px" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkId" runat="server" AutoPostBack="true" OnCheckedChanged="grdViewchkId_OnCheckedChanged"
                                                                    Style="margin: 2px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="From_Name" ItemStyle-HorizontalAlign="Left" HeaderText="From"
                                                            HeaderStyle-Width="280px" ItemStyle-CssClass="print" SortExpression="From_Name"
                                                            HeaderStyle-HorizontalAlign="Left" />
                                                        <%--<asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderText="Subject"
                                                            HeaderStyle-Width='20%' ItemStyle-CssClass="print" SortExpression="Mail_Subject"
                                                            HeaderStyle-HorizontalAlign="Left" />--%>
                                                        <%-- New changes done by saravanan start  --%>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" HeaderStyle-Width='25%'
                                                            ItemStyle-CssClass="print" SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject") %>'  Visible ="false" />
                                                                   <asp:LinkButton ID = "lnk_ViewMailSub" runat ="server" Text = '<% #Eval("Mail_subject") %>' 
                                                                           CommandArgument='<%# Eval("Trans_sl_No") %>'  CommandName="ViewMail" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- New changes done by saravanan End  --%>
                                                        <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="145px" ItemStyle-CssClass="print"
                                                            HtmlEncode="false" DataFormatString="{0:d}" SortExpression="Mail_Sent_Time" />
                                                        <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                                            Visible="false" ItemStyle-CssClass="print" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle Font-Names="Verdana" ForeColor="Black" BackColor="AliceBlue" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" CssClass="pgr" />
                                                    <FooterStyle BackColor="#DDEEFF" />
                                                    <EmptyDataRowStyle Font-Size="9pt" ForeColor="Red" Font-Names="Verdana" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--<asp:UpdatePanel ID="UpdPanelSent" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>--%>
                                <asp:Panel ID="pnlSent" runat="server" Width="90%" Style="top: 76px; position: absolute;
                                    visibility: hidden;">
                                    <table id="Table2" style="border-top-width: thick; border-top-color: White; width: 99.4%;
                                        margin-left: 2px">
                                        <tr align="left">
                                            <td>
                                                <asp:GridView ID="grdSent" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdSent_RowDataBound"
                                                    HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true" AllowPaging="True" OnRowCommand="grdSent_RowCommand"
                                                    PageSize="15" CssClass="mGrid" HeaderStyle-BackColor=" #ededea" HeaderStyle-HorizontalAlign="Left"
                                                    EmptyDataText="No Mail(s)..." PagerStyle-CssClass="pgr" HeaderStyle-Font-Size="7pt"
                                                    HeaderStyle-Font-Names="Verdana" OnPageIndexChanging="grdSent_PageIndexChanging"
                                                    GridLines="None">
                                                    <HeaderStyle BorderWidth="1" />
                                                    <PagerStyle CssClass="pgr" Font-Size="6pt" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First"
                                                        LastPageText="Last" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width='1%' ItemStyle-HorizontalAlign="center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="grdcbSelectAll_OnCheckedChanged"
                                                                    Style="margin: 2px" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkId" runat="server" AutoPostBack="true" OnCheckedChanged="grdchkId_OnCheckedChanged"
                                                                    Style="margin: 2px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Mail_Sf_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="To" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-CssClass="print"
                                                            SortExpression="Mail_SF_To" Visible="false" />
                                                        <asp:TemplateField HeaderText="To" ItemStyle-CssClass="print" HeaderStyle-Width="250px"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <%--<ControlStyle  CssClass="TEXTAREA"></ControlStyle>--%>
                                                            <ItemTemplate>
                                                                <asp:Label Style="width: 250px;" CssClass="print" ID="lblSF_Code" runat="server"
                                                                    Text='<%#   Bind("To_SFName") %>'></asp:Label>\ &nbsp; &nbsp;&nbsp; &nbsp
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderText="Subject"
                                                                    HeaderStyle-Width='15%' ItemStyle-CssClass="print" SortExpression="Mail_Subject"
                                                                    HeaderStyle-HorizontalAlign="Left" />--%>
                                                        <%-- New changes done by saravanan start  --%>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" HeaderStyle-Width='25%'
                                                            ItemStyle-CssClass="print" SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject") %>'  Visible ="false" />
                                                                    <asp:LinkButton ID = "lnk_SentMailSub" runat ="server" Text = '<% #Eval("Mail_subject") %>' 
                                                                           CommandArgument='<%# Eval("Trans_sl_No") %>'  CommandName="ViewMail" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- New changes done by saravanan End  --%>
                                                        <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="145px" ItemStyle-CssClass="print"
                                                            HtmlEncode="false" DataFormatString="{0:d}" SortExpression="Mail_Sent_Time" />
                                                        <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"
                                                            Visible="false" ItemStyle-CssClass="print" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle Font-Names="Verdana" ForeColor="Black" BackColor="AliceBlue" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" CssClass="pgr" />
                                                    <FooterStyle BackColor="#DDEEFF" />
                                                    <EmptyDataRowStyle Font-Size="9pt" ForeColor="Red" Font-Names="Verdana" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%-- </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                               
                                               
                         
                                <asp:Panel ID="pnlCompose" runat="server" Style="top: 45px; width: 90%; position: absolute;
                                    visibility: hidden;">
                                    <div class="DcrDispHPadFix" style="border-width: 0px; margin: 0px; border-style: solid;
                                        border-color: #f7fff8;">
                                        <table align="Center">
                                            <tr>
                                                <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                                                    width: 95px; height: 20px;" valign='middle' title='Clear a New Mail'>
                                                    <%--<span class='itemImage'>
                                                        <img alt='Write a New Mail' src='../../images/Writemail.ico' /></span>Clear
                                                    Mail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                    <div>
                                                        <asp:LinkButton Style="cursor: hand; background: url('../../images/Writemail.ico') left center no-repeat;
                                                            text-decoration: none; padding: 0em 1.2em;" ID="lnkClear" Font-Names="Verdana"
                                                            ForeColor="#004000" Font-Size="10px" runat="server" OnClick="lnkClear_Click">&nbsp;&nbsp;Clear Mail</asp:LinkButton>
                                                    </div>
                                                </td>
                                                <td class='print' style="width: 10px; text-align: center; height: 20px;" valign='middle'>
                                                    |
                                                </td>
                                                <td nowrap style="border-style: solid; border-width: 0px; width: 90px; height: 20px;"
                                                    title='Send a Composed Mail'>
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImgBtnSend" runat="server" ImageUrl="../../images/sendicon.gif"
                                                                        OnClientClick="return valid()" OnClick="lnkButton_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="lnkButton" Text="Send" Style="padding: 0em 1.2em; border: 0; background-color: #F2F2F2"
                                                                        runat="server" Font-Names="Verdana" ForeColor="#004000" Font-Size="10px" OnClick="lnkButton_Click"
                                                                        OnClientClick="return valid()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td class='print' nowrap style="width: 20px; text-align: center; height: 20px;" valign='middle'>
                                                    |
                                                </td>
                                                <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                                                    width: 60px; height: 20px;" valign='middle' title='Attach a file ' runat="server"
                                                    id="tdAttach">
                                                    <div>
                                                        <asp:LinkButton Style="cursor: hand; background: url('../../images/Attachment.gif') left center no-repeat;
                                                            text-decoration: none; padding: 0em 1.2em;" ID="lnkAttach" Font-Names="Verdana"
                                                            ForeColor="#004000" Font-Size="10px" runat="server" OnClick="lnkAttach_Click"><span style="margin-left:10px">Attach</span></asp:LinkButton>
                                                    </div>
                                                    <%--<span class='itemImage' id="spanAttach" runat="server">
                                                        <img alt='Attach a file' src='../../images/Attachment.gif'  /></span>Attach&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                </td>
                                                <td class='print' nowrap style="width: 20px; text-align: center; height: 20px;" valign='middle'>
                                                    |
                                                </td>
                                                <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                                                    width: 90px; height: 20px;" valign='middle' title='Add to Cc'>
                                                    <div>
                                                        <asp:LinkButton Width="60" Height="20" Style="cursor: hand; background: url('../../images/CcIconNew.jpg') left center no-repeat;
                                                            text-decoration: none; display: inline; padding: 0em 1.2em;" ID="lnkRemoveCC"
                                                            Font-Names="Verdana" ForeColor="#004000" Font-Size="10px" runat="server" OnClick="lnkRemoveCC_Click"><span style="margin-left:20px;margin-top:2px;">Remove</span></asp:LinkButton>
                                                    </div>
                                                </td>
                                                <td class='print' nowrap style="width: 20px; text-align: center; height: 20px;" valign='middle'>
                                                    |
                                                </td>
                                                <td class='print' nowrap style="cursor: hand; border-style: solid; border-width: 0px;
                                                    width: 100px; height: 20px;" valign='middle' title='Add to Bcc'>
                                                    <asp:ImageButton ID="imgRemoveBCC" runat="server" ImageAlign="AbsMiddle" ImageUrl="../../images/BccIconNew.png"
                                                        OnClick="imgRemoveBCC_Click" />
                                                    <asp:Label ID="lblRemoveBCC" runat="server" Text="Remove Bcc"></asp:Label>
                                                </td>
                                                <td class='print' style="width: 90%; height: 20px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                     <asp:UpdatePanel ID="updPanelCompose" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                    <div style="border-width: 2px; border-style: solid; border-color: White;" class="DcrDispHPadFix">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td class="print">
                                                    To &nbsp; &nbsp; &nbsp; &nbsp;
                                                </td>
                                                <td align="right">
                                                    :
                                                </td>
                                                <td width="65px">
                                                    <asp:TextBox ID="txtAddr" runat="server" ReadOnly="true" Width="300px" CssClass="TextBox"
                                                        Height="15px"></asp:TextBox>
                                                </td>
                                                <%--<td onclick="ShowAddressBook();" >--%>
                                                <td>
                                                    <asp:Panel ID="lblAddr" runat="server" Width="50px">
                                                        <span class='itemImage1'>
                                                            <%--<asp:ImageButton ID="imgAddressBook" runat="server" ImageUrl="../../images/Address_Book_Icon.gif"
                                                                OnClick="imgAddressBook_Click" />--%>
                                                            <asp:Button ID="imgAddressBook" runat="server" Height="20px" Width="24px"  UseSubmitBehavior="false" style="background-image: url(../../images/Address_Book_Icon.gif);border:0" OnClick="imgAddressBook_Click" />
                                                        </span>
                                                    </asp:Panel>
                                                </td>
                                                <td width="50%">
                                                </td>
                                            </tr>
                                            <tr id="TrCC" runat="server">
                                                <td class="print">
                                                    Cc
                                                </td>
                                                <td align="Right">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddr1" ReadOnly="true" runat="server" Width="300px" CssClass="TextBox"
                                                        Height="15px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="lblAddr2" runat="server" Width="50px">
                                                        <span class='itemImage1'>
                                                        <%--    <asp:ImageButton ID="imgComposeCC" runat="server" ImageUrl="../../images/Address_Book_Icon.gif"
                                                                OnClick="imgComposeCC_Click" />--%>
                                                        <asp:Button ID="imgComposeCC" runat="server" Height="20px" Width="24px"  UseSubmitBehavior="false" 
                                                        style="background-image: url(../../images/Address_Book_Icon.gif);border:0" OnClick="imgComposeCC_Click" />
                                                        </span>
                                                    </asp:Panel>
                                                </td>
                                                <td width="50%">
                                                </td>
                                            </tr>
                                            <tr id="TrBCC" runat="server">
                                                <td class="print">
                                                    Bcc
                                                </td>
                                                <td align="Right">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddr2" ReadOnly="true" runat="server" Width="300px" CssClass="TextBox"
                                                        Height="15px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="lblAddr3" runat="server" Width="150px">
                                                        <span class='itemImage1'>
                                                            <%--<img alt='Address Book' src="../../images/Address_Book_Icon.gif" />--%>
                                                         <%--   <asp:ImageButton ID="imgComposeBCC" runat="server" ImageUrl="../../images/Address_Book_Icon.gif"
                                                                OnClick="imgComposeBCC_Click" />--%>
                                                         <asp:Button ID="imgComposeBCC" runat="server" Height="20px" Width="24px"  UseSubmitBehavior="false" 
                                                        style="background-image: url(../../images/Address_Book_Icon.gif);border:0" OnClick="imgComposeBCC_Click" />
                                                        </span>
                                                    </asp:Panel>
                                                </td>
                                                <td width="50%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="print">
                                                    Subject
                                                </td>
                                                <td align="Right">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSub" runat="server" MaxLength="150" Width="300px" Height="15px"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <div id="divAttach">
                                                        <asp:Image ID="imgAtt" ImageUrl="~/images/Attachment.gif" runat="server" /><asp:Label
                                                            ID="lblFileName" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblAttachment" runat="server"></asp:Label>
                                                        <asp:LinkButton CssClass="print" SkinID="dfs" ID="lbFileDel" runat="server" Text="Remove"
                                                            OnClick="lbFileDel_Onclick"></asp:LinkButton>
                                                        <%--<a id="linkFile" href="javascript:void(0);" onclick="ClearFileUpload();">Remove</a>--%>
                                                    </div>
                                                    <asp:HiddenField ID="hidAttPath" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                      </ContentTemplate>
                                                 <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnaddressclose" EventName="Click" />
     </Triggers>
                                                </asp:UpdatePanel>
                                    <table id="tblFont" runat="server">
                                        <tr style="height: 20px;">
                                            <td>
                                                <asp:DropDownList ID="ddlFontName" runat="server" Height="20" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlFontName_SelectedIndexChanged">
                                                    <asp:ListItem Text="Arial" Value="Arial" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Text="Courier" Value="Courier"></asp:ListItem>
                                                    <asp:ListItem Text="Tahoma" Value="Tahoma"></asp:ListItem>
                                                    <asp:ListItem Text="Times New Roman" Value="Times New Roman"></asp:ListItem>
                                                    <asp:ListItem Text="Verdana" Value="Verdana"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlFontSize" runat="server" Height="20" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlFontSize_SelectedIndexChanged">
                                                    <asp:ListItem Text="10" Value="10" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFontBold" runat="server" Text="B" OnClick="btnFontBold_Click" />
                                                <asp:Button ID="btnFontItalic" runat="server" Text="I" OnClick="btnFontItalic_Click" />
                                                <asp:Button ID="btnFontUnderline" runat="server" Text="U" OnClick="btnFontUnderline_Click" />
                                                <%-- <asp:Button ID="btnCount" runat="server" Text="Count" OnClientClick="javascript:generateTable()" 
                                                  OnClick="btnCount_Count" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center">
                                        <tr class='print'>
                                            <td style="width: 888px">
                                                <%--Maximum Number of characters is 5000--%>
                                            </td>
                                            <td style="background-color: Yellow;" width="10%">
                                                <span style="font-weight: bold">Left:&nbsp;&nbsp;</span><asp:Label ID="lblCount"
                                                    Text="5000" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtMsg" Style="border: 1px solid" SkinID="txtArea" OnTextChanged="txtMsg_TextChanged"
                                                    onDrag="return true;" onDrop="return true;" name="excel_data" TextMode="MultiLine"
                                                    BorderWidth="1" onpaste="return LimtCharacters(this,5000,'lblCount')" MaxLength="5000"
                                                    Height="343px" Width="100%" runat="server" onKeyDown="return LimtCharacters(this,5000,'lblCount')"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                
                                              
                                <asp:Panel ID="pnlFolder" runat="server" Width="90%" Style="margin-left: 0px; top: 68px;
                                    position: absolute; visibility: hidden;">
                                    <table id="Table4" style="border-top-style: solid; border-top-width: thick; border-top-color: White;
                                        width: 99.4%; margin-left: 4px;">
                                        <tr>
                                            <td height="50%" valign="top">
                                                <asp:GridView ID="grdFolder" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdFolder_RowDataBound"
                                                    Width="100%" HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true"  OnRowCommand="grdFolder_RowCommand"
                                                    AllowPaging="True" PageSize="20" HeaderStyle-BackColor="#ededea" OnSelectedIndexChanged="OnSelectedIndexChanged"
                                                    OnPageIndexChanging="grdView_PageIndexChanging" HeaderStyle-HorizontalAlign="Left"
                                                    EmptyDataText="No Mail(s)..." GridLines="None">
                                                    <HeaderStyle BorderWidth="1" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width='1%' ItemStyle-HorizontalAlign="center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="grdFoldercbSelected_OnCheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbChkId" AutoPostBack="true" runat="server" OnCheckedChanged="grdFoldercbChkId_OnCheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="From_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="From" HeaderStyle-Width="270px" ItemStyle-CssClass="print" SortExpression="From_Name" />
                                                        <%--<asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderText="Subject"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width='20%' ItemStyle-CssClass="print"
                                                            SortExpression="Mail_Subject" />--%>
                                                        <%-- New changes done by saravanan start  --%>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" HeaderStyle-Width='15%'
                                                            ItemStyle-CssClass="print" SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_subject") %>'  Visible = "false"/>
                                                                     <asp:LinkButton ID = "lnk_FldMailSub" runat ="server" Text = '<% #Eval("Mail_subject") %>' 
                                                                           CommandArgument='<%# Eval("Trans_sl_No") %>'  CommandName="ViewMail" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- New changes done by saravanan End  --%>
                                                        <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="145px" ItemStyle-CssClass="print"
                                                            HtmlEncode="false" DataFormatString="{0:d}" SortExpression="Mail_Sent_Time" />
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Images/Attachment.gif">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                                            Visible="false" ItemStyle-CssClass="print" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle Font-Names="Verdana" ForeColor="Black" BackColor="AliceBlue" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" CssClass="PageStyle" />
                                                    <FooterStyle BackColor="#DDEEFF" />
                                                    <EmptyDataRowStyle Font-Size="9pt" ForeColor="Red" Font-Names="Verdana" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnstate" runat="server" />
    <asp:HiddenField ID="hdnFlg" Value="" runat="server" />
    <asp:HiddenField ID="hdnDesign" runat="server" />
    <asp:HiddenField ID="hdnVal" runat="server" />   
    <asp:UpdatePanel ID="updPnlAddress" runat="server">
     
           <ContentTemplate>  
            <asp:Panel ID="pnlpopup" runat="server" BackColor="AliceBlue" Height="480px" Width="800px"
                Style="left: 230px; top: 70px; position: absolute; visibility: hidden;" BorderStyle="Solid"
                BorderWidth="1">
                <div style="border-collapse: collapse; width: 100%;" class="HeaderPrint">
                    <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                        <tr>
                            <td class='print Header' style='width: 100%'>
                                &nbsp;<span class='itemImage1'>
                                    <img alt='Address Book' src="../../Images/Address_Book_Icon.gif" /></span>Address
                                Book
                            </td>
                            <td class='print Header'>
                                <asp:Button ID="btnaddressclose" runat="server" Style="text-align: left" Height="20px"
                                    BackColor="Yellow" Font-Bold="true" Text="X" UseSubmitBehavior="false" OnClick="imgAddressClose_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <table align="center">
                    <tr align="center">
                        <td align="right">
                            <asp:Label ID="lblSearch" CssClass="Hprint" runat="server" Text="Search By" ForeColor="Navy"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdoadr" runat="server" CssClass="Hprint" Height="25px" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rdoadr_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Designation" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Field Force"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table border="0" class="Hprint">
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="Label3" runat="server" Text="Designation" ForeColor="Navy" Font-Bold="true"></asp:Label>
                            <asp:CheckBox ID="chkLevelAll" runat="server" Style="margin-left: 20px" Text="All"
                                AutoPostBack="true" OnCheckedChanged="chkLevelAll_CheckedChanged" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkMR" AutoPostBack="true" Visible="false" Text="MR" OnCheckedChanged="chkMR_OnCheckChanged"
                                runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal">
                            </asp:CheckBox>
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkDesgn" AutoPostBack="true" OnSelectedIndexChanged="chkDesgn_OnSelectedIndexChanged"
                                runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="Hprint">
                    <tr>
                        <td>
                            <asp:Label ID="lblFF" Visible="false" runat="server" Text="Field Force" CssClass="Hprint"
                                ForeColor="Navy" Font-Bold="true"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="ddlFFType" Visible="false" runat="server" Style="margin-left: 30px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Width="15%">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <%--<asp:ListItem Value="1" Text="Alphabetical"></asp:ListItem>--%>
                                <asp:ListItem Value="2" Text="Team" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Division"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" Width="6%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFieldForce" Visible="false" runat="server" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                AutoPostBack="true" Width="40%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <%--<td align="right">
                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
                </td>--%>
                    </tr>
                    <tr style="height: 10px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSelectedCount" runat="server" Text="SelectedValue"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" style="width: 100%; height: 350px; margin-top: 5px" cellpadding="0"
                    cellspacing="0" class="Hprint">
                    <tr>
                        <td style="border-top-style: solid; border-width: thin" valign="top">
                            <div style="height: 350px; overflow: auto;">
                                <asp:CheckBoxList ID="chkFF" runat="server">
                                </asp:CheckBoxList>
                                <asp:GridView ID="gvFF" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                    GridLines="None" HeaderStyle-BackColor="#ededea" HeaderStyle-CssClass="Hprint"
                                    HeaderStyle-HorizontalAlign="Left" OnRowDataBound="grdFF_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSf" runat="server" AutoPostBack="true" OnCheckedChanged="gvFF_OnCheckedChanged"
                                                    Style="margin: 2px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="230px" HeaderText="FieldForce Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_Name" runat="server" Text='<% #Eval("sf_name") %>' Width="230px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation_Short_Name" runat="server" Text='<% #Eval("Designation_Short_Name") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_mail" runat="server" Text='<% #Eval("sf_mail") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HQ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_HQ" runat="server" Text='<% #Eval("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBackcolor" runat="server" Text='<% #Eval("des_color") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_color" runat="server" Text='<% #Eval("sf_color") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation_Code" runat="server" Text='<% #Eval("Designation_Code") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_Code" Visible="false" runat="server" Text='<% #Eval("sf_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_Type" runat="server" Text='<% #Eval("sf_Type") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Red" />
                                </asp:GridView>
                            </div>
                            <%--<asp:ListBox ID="chkFF" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
           </ContentTemplate> 
           </asp:UpdatePanel>
            <asp:Panel ID="PnlAttachment" runat="server" BackColor="AliceBlue" Height="200px"
                Width="600px" Style="left: 230px; top: 80px; position: absolute; visibility: hidden;"
                BorderStyle="Solid" BorderWidth="1">
                <div style="border-collapse: collapse; width: 100%;" class="HeaderPrint">
                    <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                        <tr>
                            <td class='print Header' style='width: 100%'>
                                &nbsp;<span class='itemImage1'><img alt='Address Book' src="../../Images/Address_Book_Icon.gif" /></span>Address
                                Book
                            </td>
                            <td class='print Header'>
                                <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="../../images/close.gif"
                                    OnClick="ImgAttachment_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="border-collapse: collapse; width: 100%; margin-top: 20px">
                    <table border='1' style='border-collapse: collapse; width: 100%; height: 100%'>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <asp:Button ID="btnUpload" Text="Attachment" runat="server" OnClick="btn_Go" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlViewInbox" runat="server" BackColor="LightGrey" Height="500px"
                Width="99%" Style="top: 55px; margin-left: 2px; font-size: 7pt; font-family: Verdana;
                position: absolute; visibility: hidden;" BorderStyle="Solid" BorderWidth="1">
                <div style="border-collapse: collapse; width: 100%;" class="HeaderPrint">
                    <table border='0' style='border-collapse: collapse; width: 100%; height: 100%'>
                        <tr>
                            <td class='print Header' style='width: 100%'>
                                &nbsp;<span class='itemImage1'><img alt='Address Book' src="../../Images/Address_Book_Icon.gif" /></span>
                                View Mail Details
                            </td>
                            <td class='print Header'>
                                <%--asp:ImageButton ID="imgViewMail" runat="server" ImageUrl="../../images/close.gif"
                                            OnClick="imgViewMail_Click" />--%>
                                <asp:Button ID="btnimgViewMail" runat="server" Height="20px" Width="20px" Text="X"
                                    UseSubmitBehavior="false" OnClick="imgViewMail_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="DcrDispHPadFix" style="border-width: 0px; width: 100%; margin-left: 0px">
                    <table width="100%" height="20px" id="Table3" align="Center" border="0" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td class='print'>
                            </td>
                            <td class='print' id='td2' width="8%" style="cursor: hand; border-style: solid; border-width: 0px;"
                                valign='middle' title='Delete the Selected Mail(s)'>
                                <div id="divDelete" onclick='return MoveFlder(0)'>
                                    <asp:LinkButton Style="background: url('../../images/transparent.ico') left center no-repeat;
                                        text-decoration: none; padding: 0em 1.3em;" ID="imgbtnDeleteViewMail" Font-Names="Verdana"
                                        ForeColor="#004000" Font-Size="10px" runat="server" OnClick="imgbtnDeleteViewMail_Click">&nbsp;&nbsp; Delete</asp:LinkButton>
                                </div>
                            </td>
                            <td class='print' width="3%" style='text-align: center' valign='middle'>
                                |
                            </td>
                            <td class='print' id='td3' width="8%" style="border-style: solid; border-width: 0px;"
                                valign='middle' title='Reply to a Current Read Mail'>
                                <asp:LinkButton Style="cursor: hand; background: url('../../images/ReplyIcon.gif') left center no-repeat;
                                    text-decoration: none; padding: 0em 1.3em;" ID="imgbtnReplyViewMail" Font-Names="Verdana"
                                    ForeColor="#004000" Font-Size="10px" runat="server" OnClick="imgbtnReplyViewMail_Click">
                                                            &nbsp;&nbsp; Reply</asp:LinkButton>
                            </td>
                            <td class='print' width="3%" style='text-align: center' valign='middle'>
                                |
                            </td>
                            <td id='td5' class='print' width="7%" nowrap style="cursor: hand; border-style: solid;
                                border-width: 0px;" valign='middle' title='Forward  To a Current Read Mail'>
                                <asp:LinkButton Style="cursor: hand; background: url('../../images/ForwardIcon_New.jpg') left center no-repeat;
                                    text-decoration: none; padding: 0em 1.3em;" ID="imgbtnFwdViewMail" Font-Names="Verdana"
                                    ForeColor="#004000" Font-Size="10px" runat="server" OnClick="imgbtnFwdViewMail_Click">
                                                            &nbsp;&nbsp;Forward</asp:LinkButton>
                            </td>
                            <td class='print' width="3%" style='text-align: center' valign='middle'>
                                |
                            </td>
                            <td class='print' width="3%" id="td7" style='display: none; text-align: center' valign='middle'>
                                |
                            </td>
                            <td class='print' style='cursor: hand; border-style: solid; display: none; border-width: 0px;
                                width: 70px;' valign='middle' onclick='tkPrn()' id='Td8' title='Print Mail'>
                                <span class='itemImage'>
                                    <img alt='Print Mail' src='../../images/print.gif' style='width: 16px; height: 16px' />
                                </span>Print&nbsp;
                            </td>
                            <td class='print' id="td9" width="3%" style='display: none; text-align: center' valign='middle'>
                                |
                            </td>
                            <td class='print' id="td10" width="5%" runat="server" nowrap style='cursor: hand;
                                border-style: solid; border-width: 0px; width: 90px;' valign='middle' title='Close The Current Window'>
                                <asp:LinkButton Style="cursor: hand; background: url('../../images/New.gif') left center no-repeat;
                                    text-decoration: none; padding: 0em 1.3em;" ID="LinkButton1" Font-Names="Verdana"
                                    ForeColor="#004000" Font-Size="10px" runat="server" OnClick="lnkbtnClose_Click">
                                                            &nbsp;&nbsp; Close</asp:LinkButton>
                            </td>
                            <td class="print" style="width: 80%; text-align: left">
                                <div id="Div2">
                                    &nbsp;&nbsp;
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="pnlViewMailInbox" runat="server" BackColor="LightGrey" Height="500px"
                    Width="100%" Style="left: 0px; top: 40px; position: absolute; visibility: hidden;"
                    BorderStyle="Solid" BorderWidth="1">
                    <table width="100%" cellpadding="0" cellspacing="0" style="height: 85px">
                        <tr style="width: 50px">
                            <td style="width: 75px">
                                <asp:Label ID="lblFrom" runat="server" Text="&nbspFrom" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <asp:Label ID="lblViewFrom" runat="server" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 50px">
                            <td style="width: 25px">
                                <asp:Label ID="lblTo" runat="server" Text="&nbsp;To" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <asp:Label ID="lblViewTo" runat="server" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 50px">
                            <td style="width: 25px">
                                <asp:Label ID="lblCc" runat="server" Text="&nbsp;Cc " Font-Size="X-Small"></asp:Label>
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <asp:Label ID="lblViewCC" runat="server" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 50px">
                            <td style="width: 25px">
                                <asp:Label ID="lblSubject" runat="server" Text="&nbsp;Subject" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <asp:Label ID="lblViewSub" runat="server" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 50px">
                            <td style="width: 25px">
                                <asp:Label ID="lblSentDate" runat="server" Text="&nbsp;Sent Date" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <asp:Label ID="lblViewSent" runat="server" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Image ID="imgViewAttach" runat="server" Visible="false" ImageUrl="~/Images/Attachment.gif" />
                            </td>
                            <td><a id="aTagAttach" runat="server"> <asp:Label ID="lblViewAttach" runat="server"></asp:Label></a></td>
                        </tr>
                    </table>
                    <div>
                        <span style="white-space: pre-line">
                            <asp:TextBox ID="lblMailBody" SkinID="txtArea" onDrag="return false;" onDrop="return false;"
                                name="txtMsg" TextMode="MultiLine" onpaste="return MaxLenOnPaste(5000)" MaxLength="5000"
                                Height="400px" Width="100%" runat="server" onblur="javascript:generateTable()"></asp:TextBox>
                        </span>
                    </div>
                </asp:Panel>
            </asp:Panel>           
          
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../../Images/loader.gif" alt="" />
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
