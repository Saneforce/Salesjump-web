<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vacant.aspx.cs" Inherits="Vacant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <br />
       <div>
        <div align="right">
            <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout"
                OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
        </div>
        <br />
        <br />
        <br />
        <center>
            <img src="Images/vacant.jpg" alt="" />
        </center>
        <center>
            <h2>
                <span style="color: Blue">This ID is in Vacant...</span></h2>
        </center>
    
    </div>
    </form>
</body>
</html>
