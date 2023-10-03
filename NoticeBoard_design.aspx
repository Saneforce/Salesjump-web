<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoticeBoard_design.aspx.cs"
    Inherits="NoticeBoard_design" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .post-it
        {
            background-image: url('http://alternatewrites.com/wp-content/uploads/2012/06/post-it-note-with-a-pin.jpg');
            background-repeat: no-repeat;
            width: 500px;
            height: 300px;
        }
        .note
        {
            position: absolute;
            top: 90px;
            right: 30px;
            bottom: 30px;
            left: 60px;
            overflow: auto;
        }
        
        #pnlmar
        {
            background-repeat: no-repeat;
            height: 800px;
        }
        .word_wrap
        {
            word-wrap: break-word;
        }
        
        body
        {
            text-align: center;
            margin: 0 auto;
        }
        
        #box
        {
            position: absolute;
            width: 90%;
            height: 40%;
            left: 25%;
            top: 30%;
            border: #000;
        }
        .roundCorner
        {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 150px;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlnot" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
            <tr>
                <td>
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                        OnClick="btnHome_Click" BackColor="LightPink" ForeColor="Black" CssClass="roundCorner" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="roundCorner"
                        Text="Direct to Home Page" OnClick="btnHomepage_Click" BackColor="Green" ForeColor="White" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="roundCorner"
                        Text="Logout" OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <br />
        <center>
            <asp:Label ID="lblHead" Font-Size="X-Large" ForeColor="Blue" Font-Bold="true" Font-Italic="true"
                Font-Names="Verdana" runat="server">Notice Board</asp:Label>
        </center>
        <br />
        <br />
        <center>
            <asp:Panel ID="pnldivi" runat="server" Visible="false">
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnGo" runat="server" BackColor="LightBlue" Width="30px" Height="25px"
                                Text="Go" OnClick="btnGo_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </center>
        <br />
        <br />
        <br />
        <center>
            <div id="box">
                <asp:Panel ID="pnlmar" runat="server" BackImageUrl="Images/Notice1.jpg" HorizontalAlign="Center">
                    <font style="font-family: Verdana; font-style: inherit; font-size: 13px; color: Red;
                        vertical-align: middle">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);"
                            scrolldelay="200" direction="up" style="width: 80%">
<asp:Label ID="lblCon1" runat="Server" Text='<%# Eval("NB_Cont1") %>' Font-Italic="true" ForeColor="Red" Width="220px"></asp:Label>
<br />    
<span style="text-align:center">***</span>
<br />
     <asp:Label ID="lblCon2" runat="Server" Text='<%# Eval("NB_Cont2") %>' width="220px"  Font-Italic="true" ForeColor="Red"></asp:Label>
     <br />
     <span style="text-align:center" >***</span>
     <br />
      <asp:Label ID="lblCon3" runat="Server" Text='<%# Eval("NB_Cont3") %>' Width="220px" Font-Italic="true" ForeColor="Red"></asp:Label>     
     
<asp:Label ID = "lblnorecords" runat="Server" Visible="false">No Data Found </asp:Label>

</marquee>
                    </font>
                </asp:Panel>
            </div>
        </center>
    </asp:Panel>
    </form>
</body>
</html>
