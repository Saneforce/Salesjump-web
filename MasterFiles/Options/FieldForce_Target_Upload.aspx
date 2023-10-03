<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FieldForce_Target_Upload.aspx.cs" Inherits="MasterFiles_Options_FieldForce_Target_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=btnupload.ClientID%>').click(function () {
                var st1 = $('#<%=FlUploadcsv.ClientID%>').val();
                if (st1 == "") { alert("No file selected"); $('#<%=FlUploadcsv.ClientID%>').focus(); return false; }
            });
        });
    </script>
      <form id="mainfrm" runat="server">
        <asp:Panel ID="pnlDoctor" Width="90%" runat="server">
        <table width="100%" style="margin-left: 10%">
            <tr>
                <td>
    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black;background-color: White">
        <tr>
            <td style="padding-right: auto">
                 <asp:Label ID="lblExcel" runat="server" class="pad HDBg" style="position:relative;color: #336277;font-family: Verdana;font-weight: bold;text-transform: capitalize;font-size: 14px;">Excel file</asp:Label>
                <br />
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
            <td>
                <asp:Label ID="lblExc" runat="server" Text="Excel Format File" Style="position: relative;
                    color: #336277; font-family: Verdana; font-weight: bold; text-transform: capitalize;
                    font-size: 12px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="lnkDownload" runat="server" ImageUrl="~/Images/button_download-here.png"
                    OnClick="lnkDownload_Click" />
            </td>
        </tr>
    </table>
                    </td>
              
            </tr>
        </table>
         </asp:Panel>
            </form>
</asp:Content>

