<%@ Page Title="Class Transfer" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Class_Transfer.aspx.cs" Inherits="MasterFiles_Class_Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

$(document).ready(function () {

  $('#<%=btnchange.ClientID%>').click(function () {
				 var spec = $('#<%=DDL_CHANNEL1.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Class From."); $('#<%=DDL_CHANNEL1.ClientID%>').focus(); return false; }
 var spec = $('#<%=DDL_CHANNEL2.ClientID%> :selected').text();
                if (spec == "---Select---") { alert("Select Class To."); $('#<%=DDL_CHANNEL2.ClientID%>').focus(); return false; }
            });

 });
    </script>
   <form id="form1" runat="server">
    <div>
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Class From"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDL_CHANNEL1" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            CssClass="ddl" onselectedindexchanged="DDL_CHANNEL1_Change" >
                        </asp:DropDownList>
                    </td>
                   <%--  <td>
                        <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Retailer Count : "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_cus_cnt" runat="server" SkinID="lblMand" Text="0"></asp:Label>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Class To"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDL_CHANNEL2" runat="server" SkinID="ddlRequired" AutoPostBack="false"
                            CssClass="ddl">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />            
           <%-- <button id="btnGo" class="button" runat="server"  style="vertical-align: middle">
                <span>Change</span></button>--%>
                <asp:Button ID="btnchange" runat="server"  style="vertical-align: middle" 
                Font-Bold="True" Text="Change" onclick="btnchange_Click"  />
        </center>
    </div>
    </form>
</asp:Content>
