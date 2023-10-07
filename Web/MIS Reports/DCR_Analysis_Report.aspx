<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Analysis_Report.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Coverage_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .gvHeader th
        {
            padding: 3px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }
        .gvRow td
        {
            padding: 3px;
            background-color: #ffffff;
            border: 1px solid #bbb;
        }
        .gvAltRow td
        {
            padding: 3px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
        }
        .gvHeader th:first-child
        {
            display: none;
        }
        .gvRow td:first-child
        {
            display: none;
        }
        .gvAltRow td:first-child
        {
            display: none;
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
        #loading
        {
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            position: fixed;
            display: block;
            opacity: 0.7; /*background-color: #fff;*/
            z-index: 99;
            text-align: center;
        }
        
        #loadingimage
        {
            position: absolute;
            top: 100px;
            left: 240px;
            z-index: 100;
        }
        
        
        .ball
        {
            background-color: rgba(0,0,0,0);
            border: 5px solid rgba(0,183,229,0.9);
            opacity: 0.9;
            border-top: 5px solid rgba(0,0,0,0);
            border-left: 5px solid rgba(0,0,0,0);
            border-radius: 50px;
            box-shadow: 0 0 35px #2187e7;
            width: 50px;
            height: 50px;
            margin: 0 auto;
            -moz-animation: spin .5s infinite linear;
            -webkit-animation: spin .5s infinite linear;
        }
        
        .ball1
        {
            background-color: rgba(0,0,0,0);
            border: 5px solid rgba(0,183,229,0.9);
            opacity: 0.9;
            border-top: 5px solid rgba(0,0,0,0);
            border-left: 5px solid rgba(0,0,0,0);
            border-radius: 50px;
            box-shadow: 0 0 15px #2187e7;
            width: 30px;
            height: 30px;
            margin: 0 auto;
            position: relative;
            top: -50px;
            -moz-animation: spinoff .5s infinite linear;
            -webkit-animation: spinoff .5s infinite linear;
        }
        
        .divProgress
        {
            margin-top: 40%;
        }
        @media print
        {
            tr.vendorListHeading
            {
                background-color: #1a4567 !important;
                -webkit-print-color-adjust: exact;
            }
        }
        @media print
        {
            .vendorListHeading th
            {
                color: white !important;
            }
        }
        tr td
        {
            font-size:smaller;
            font-family:Verdana;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <%--<div id="divLoader" visible="true" runat="server" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>--%>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td style="float: right;">
                    <table>
                        <tr style="float: right; margin-right: 10px;">
                            <td>
                                <asp:ImageButton ID="btnPrint" ImageUrl="~/Images/printer.png" runat="server" Width="35px"
                                    Height="30px" OnClientClick="return PrintPanel();" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:ImageButton ID="btnExcel" ImageUrl="~/Images/Excels.png" runat="server" Height="30px"
                                    Width="35px" />
                            </td>
                            &nbsp;&nbsp;&nbsp;
                            <td>
                                <asp:ImageButton ID="btnClose" ImageUrl="~/Images/closebtn.png" runat="server" Height="30px"
                                    Width="35px" OnClientClick="RefreshParent()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlContents" runat="server">
            <center>
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " Font-Underline="True"
                        Font-Bold="True" Font-Names="Verdana" Font-Size="11pt" Visible="false"></asp:Label>
                    <br />
                    <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="9pt" Visible="false"></asp:Label>
                </div>
            </center>
            <br />
            <center>
                <asp:Panel runat="server" ID="pnlTbl">
                </asp:Panel>                
            </center>
            <br />     
            <br />       
        </asp:Panel>
    </div>
    </form>
</body>
</html>
