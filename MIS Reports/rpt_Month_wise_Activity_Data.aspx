<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Month_wise_Activity_Data.aspx.cs"
    EnableEventValidation="false" Inherits="MIS_Reports_rpt_Month_wise_Activity_Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Month_wise_Activity_Data</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script type="text/javascript">
        function RefreshParent() {
            //  window.opener.document.getElementById('form1').click();
            window.close();
        }
        </script>
    <style type="text/css">
        body
        {
            padding: 10px;
        }
        .mGrid td, .mGrid th
        {
            padding: 2px 8px;
        }
        .AAA
        {
            color: #f7f7f7;
            text-decoration: underline;
        }
        #GV_DATA td
        {
            text-align: right;
        }
        #GV_DATA td:nth-child(1), #GV_DATA td:nth-child(2)
        {
            text-align: left;
        }
        #GV_DATA tr td:first-child, #GV_DATA tr th:first-child
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="    width: 95%;    border-collapse: collapse;    margin: 0px auto;">
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="60%" align="center">
                        <asp:Label ID="lblHead" Text="Purchase Register-Distributor Wise" SkinID="lblMand"
                            Font-Bold="true" Visible="true" Font-Underline="true" runat="server" />
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                                        OnClick="btnPrint_Click" />
                                    <a id="btnExport" style="padding: 0px 20px;" class="btn btnPdf" onclick="generate()" />
                                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"
                                       />
                                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                        class="btn btnClose"></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div id="divExcel">
            <center>
               <div>
                    
                        <asp:Label ID="lblyear" runat="server" Visible="false"></asp:Label></div>
                <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidn_sf_code" runat="server" />
                        <asp:HiddenField ID="hidnYears" runat="server" />
                    </b>
                </div>
                <div>
                    <asp:GridView ID="GV_DATA" runat="server" Width="95%" class="newStly" OnRowDataBound="Dgv_SKU_RowDataBound">
                    </asp:GridView>
                </div>
            </center>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
