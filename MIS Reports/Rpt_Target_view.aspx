<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Target_view.aspx.cs"
    Inherits="Rpt_Target_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product Target Achievement</title>
    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script language="Javascript">
        function RefreshParent() {
            //window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        
        .remove
        {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 90%">
        <br />
        <div class="row" style="width: 100%">
            <div class="col-sm-8">
                <asp:Label ID="lblHead" Text="PRODUCT TARGET ACHIEVEMENT" runat="server" Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
            <div class="col-sm-4" style="text-align: right">
                <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                    OnClick="btnPrint_Click" />
                <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px; display: none"
                    class="btn btnPdf" OnClick="btnExport_Click" />
                <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                    OnClick="btnExcel_Click" />
                <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                    class="btn btnClose"></a>
            </div>
        </div>
    </div>
    <br />
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div>
            <div style="withd: 90%; padding: 0px 100px;">
                <asp:Label ID="lblIDMonth" Text="Fieldforce Name" runat="server" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblfldforce" runat="server" SkinID="lblMand"></asp:Label>
            </div>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl" runat="server" Style="border-collapse: collapse; border: solid 1px Black;"
                                GridLines="Both" Width="90%" CssClass="newStly">
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
