<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail_Folder_Trans.aspx.cs" Inherits="MasterFiles_Mail_Folder_Trans" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Mail Folder</title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
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
        .clp
        {
           border-collapse: collapse;
        }
    </style>
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
        $(document).ready(function () {
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $('#btnTransfer').click(function () {

                var From = $('#<%=ddlTrans_From.ClientID%> :selected').text();
                if (From == "--Select--") { alert("Select Transfer From Mail"); $('#ddlTrans_From').focus(); return false; }
                var To = $('#<%=ddlTrans_To.ClientID%> :selected').text();
                if (To == "--Select--") { alert("Select Transfer To Mail"); $('#ddlTrans_To').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript">

        function ConfirmMessage() {
            var selectedvalue = confirm("Do you want to Transfer Mail?") &&
         confirm('Are you sure want to Transfer?');

            if (selectedvalue) {

                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";

            } else {

                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";

            }
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" />
      <br />

      <center >
      <asp:UpdatePanel ID="updatepanel4" runat="server">
              <ContentTemplate>
      <table border ="0" cellpadding ="5" cellspacing ="5" id="tbltransfer" align="center" >
       <tr>
         <td align="left">
            <asp:Label ID="lblTrans_From" runat="server" SkinID="lblMand" Font-Bold="true" > <span style="Color:Red">*</span>Change From Folder </asp:Label>         
         </td>

         <td>
            <asp:DropDownList ID="ddlTrans_From" runat="server" onblur="this.style.backgroundColor='White'"
                       onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" AutoPostBack="true"
                       TabIndex="1" OnSelectedIndexChanged="ddlTrans_From_SelectedIndexChanged"></asp:DropDownList> 
         </td>
       </tr>

       <tr>
         <td align="left">
             <asp:Label ID="lblTrans_To" runat="server" SkinID="lblMand" Font-Bold="true"><span style="Color:Red">*</span>Change To Folder</asp:Label>         
         </td>
         <td>
            <asp:DropDownList ID="ddlTrans_To" runat="server" onblur="this.style.backgroundColor='White'"
                    onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired" AutoPostBack="true"
                    TabIndex="2">
            </asp:DropDownList>
         </td>
       </tr>    
      </table>
       </ContentTemplate>
           </asp:UpdatePanel>
      <br />

      <table >
         <tr>
            <td align="left" >
                  <asp:CheckBox ID="Chkdelete" runat="server" Font-Size="11px" Font-Names="Verdana" Font-Bold="true"
                            Text=" 'Delete' After Transfer" />
            </td>
         </tr>
      </table>
      
      </center>
      <br />
      <center>
         <asp:HiddenField ID="txtconformmessageValue" runat="server" />

         <asp:Button ID="btnTransfer" runat="server" CssClass="BUTTON" Width="130px" Text ="Transfer - Mail" OnClick="btnTransfer_Click" /> 
      </center>
     <br />
     <center >
        <asp:Panel ID="pnlCount" runat="server" Visible="false" >
           <asp:Table ID="Table1" runat="server" BorderStyle="Solid" Width="30%" BorderWidth="1" CssClass="clp" 
           CellSpacing="3" CellPadding ="3">
             <asp:TableHeaderRow >
               <asp:TableHeaderCell ColumnSpan="2">
                  <asp:Label ID="lbltrans" runat="server" Text ="Transaction Available" Font-Names="Verdana" Font-Bold="true" Font-Size="12px" ForeColor="Blue"></asp:Label>  
               </asp:TableHeaderCell>                
             </asp:TableHeaderRow>
             
             <asp:TableRow >
                <asp:TableCell BorderWidth="1" Width="150px" HorizontalAlign="Left">
                  <asp:Label ID="lblmovedmail" runat="server" Text="No. of moved mails available" Font-Bold="true"  SkinID="lblMand" ></asp:Label> 
                </asp:TableCell>
                <asp:TableCell BorderWidth="1" Width="80px" HorizontalAlign="center">
                   <asp:Label ID="lblmovedcount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>

             </asp:TableRow>
           </asp:Table> 
           <br />
           <asp:Button ID="btnConfirm" runat="server" Text="Confirm to Transfer" Width="130px" CssClass="BUTTON" OnClientClick="return ConfirmMessage()"
            OnClick="btnConfirm_Click" /> 
           </asp:Panel>
     </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
