<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="UploadRouteNew.aspx.cs" Inherits="UploadRouteNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="from1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <div class="col-xs-3">
                    <a class="list-group-item active state">Excel Format & Fields</a>
                    <div style="overflow-y: scroll; height: 200px">
                        <asp:CheckBoxList runat="server" ID="chkFruits" class="list-group checked-list-box">
                        </asp:CheckBoxList>
                    </div>
                    <button runat="server" class="btn btn-primary col-xs-12" onserverclick="lnkDownload_Click" id="ExcelFormat">Download</button>
                    <pre id="display-json" style="display: none"></pre>
                </div>
                <div class="col-xs-9">
                    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color: White">
                        <tr>
                            <td style="padding-right: auto">
                                <asp:Label ID="lblExcel" runat="server" class="pad HDBg" Style="position: relative; color: #336277; font-family: Verdana; font-weight: bold; text-transform: capitalize; font-size: 14px;">Excel file</asp:Label>
                                <br />
                                <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">

                                <asp:Button ID="btnupload" runat="server" Width="100px" Height="30px" BackColor="#19a4c6"
                                    ForeColor="#ffffff" Font-Size="Medium" Text="Upload" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 80px;">
                                <asp:Label ID="Label4" runat="server" Text="Note:" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="Label6" Font-Size="11px" Font-Names="Verdana" runat="server" Text="1) Bill Date & Bill Due Date Must be in 'DD/MM/YYYY' Format"></asp:Label>
                                <br />
                                <asp:Label ID="Label7" Font-Size="11px" Font-Names="Verdana" runat="server" Text="2) Don't Do Any Special Formats in the Excel File"></asp:Label>
                                <br />
                                <div id="dvStatus" style="display: block; width: 80%; overflow: auto; height: 120px">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="http://code.jquery.com/jquery-3.4.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=btnupload.ClientID%>').click(function () {
                var st1 = $('#<%=FlUploadcsv.ClientID%>').val();
                if (st1 == "") { alert("No file selected"); $('#<%=FlUploadcsv.ClientID%>').focus(); return false; }
            });
        });
</script>
</asp:Content>

