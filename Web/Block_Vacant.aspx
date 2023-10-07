<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Block_Vacant.aspx.cs" Inherits="Block_Vacant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script type="text/javascript" src="https://code.jquery.com/jquery-1.7.1.min.js"></script>
</head>
<body>
<script type="text/javascript">
          $(document).ready(function () {
            if ('<%=type%>' == '2' || '<%=type%>' == '1') {
                $("#Blk").hide()
                $("#Dactv").show()
            }
            else if ('<%=type%>' == '3') {
                $("#Dactv").hide()
                $("#Blk").show()
            }
        })
    </script>
    <form id="form1" runat="server">
    <div>
        <br />
        <div align="right">
            <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout"
                OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
        </div>
        <br />
        <br />
        <center>
            <img src="Images/block1.jpg" alt="" />
        </center>
        <center>
        </center>
        <center>
            <h2>
                <span id="Blk" style="color: Blue">Your Id is Blocked... Kindly Contact Your Line Manager...</span></h2>
               <h2> <span id="Dactv" style="color: Blue">Your Id is Deactivate... Kindly Contact Your Line Manager...</span></h2>
            <br />
            <table style="display:none;">
                <tr>
                    <td>
                        <asp:Label ID="lblimg" runat="server"><img src="Images/hand.gif" alt=""  /></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="lblblo" runat="server" Text="Blocked Reason:" Font-Bold="true" Font-Size="18px"></asp:Label>
                        <asp:Label ID="lblreason" runat="Server" Style="margin-top: 10px;" Width="100%" ForeColor="Blue"
                            Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" Text='<%# Eval("sf_blkreason") %>' />
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <br />
        <br />
        <%--  <asp:Label ID="lblreason" runat="Server"  style="margin-top:10px;" Width="100%" ForeColor="Blue" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma"  Text='<%# Eval("sf_blkreason") %>' />--%>
    </div>
    </form>
</body>
</html>
