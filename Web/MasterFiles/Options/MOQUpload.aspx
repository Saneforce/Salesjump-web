<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="MOQUpload.aspx.cs" Inherits="MasterFiles_Options_MOQUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black;
        background-color: White">
     <%--   <tr>
            <td style="padding-right: auto">
                <asp:Label ID="Label5" runat="server" class="pad HDBg" Style="position: relative;
                    color: #336277; font-family: Verdana; font-weight: bold; text-transform: capitalize;
                    font-size: 14px;">Field Force</asp:Label>
                <br />
                <asp:DropDownList ID="DDLFO" runat="server" Width="350px" Style="padding: 5px 5px">
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td style="padding-right: auto">
                <asp:Label ID="lblExcel" runat="server" class="pad HDBg" Style="position: relative;
                    color: #336277; font-family: Verdana; font-weight: bold; text-transform: capitalize;
                    font-size: 14px;">Excel file</asp:Label>
                <br />
                <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px;" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <%--  <asp:ImageButton ID="Button1" runat="server" Width="180px" Height="30px" Text="Upload"
                    OnClick="btnUpload_Click" />--%>
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
                <asp:Label ID="Label7" Font-Size="11px" Font-Names="Verdana" runat="server" Text="1) Don't Do Any Special Formats in the Excel File"></asp:Label>
                <br />
                 <div id="dvStatus" style="display:block;width:80%;overflow:auto;height:120px">
                                
                                </div>
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
    </form>
</asp:Content>


