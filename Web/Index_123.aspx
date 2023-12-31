<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ereporting and sales analysis</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>online reporting</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
</head>

<body class="hold-transition login-page" background="images/back.png">
<form id="frm" runat="server">
    <div class="login-box">
        <div class="login-logo">
            <a href="../../index2.html"><b>Online</b>Reporting</a>
        </div>
        <div class="login-box-body">
            <img style="width: 100px; height: 50px;" src="data:image/jpeg;base64,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" />
            <br>
            <p class="login-box-msg">
                Sign in to start your session</p>
            <form action="../../index2.html" method="post"><%=Request.Url.Host.ToLower()%>
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="UserName"></asp:TextBox>
                <span class="fa fa-user form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtPassWord" runat="server" class="form-control" placeholder="Password" MaxLength="15" TextMode="Password"></asp:TextBox>
                <span class="fa fa-lock form-control-feedback"></span>
            </div>
            <div class="row">
                <div class="col-xs-4 col-xs-offset-4">
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="btn btn-primary btn-block btn-flat"
                        Style="width: 100%" OnClick="btnLogin_Click" />
                </div>
            </div>
            </form>
            <br>
            <br>
            <asp:Label ID="msg" runat="server" Width="100%" ForeColor="Red" Font-Bold="True"
                Font-Size="x-Small" Font-Names="Verdana"></asp:Label>
            <%-- <a href="#">I forgot my password</a><br>
            <a href="register.html" class="text-center">Register a new membership</a>--%>
        </div>
    </div>
    <script src="js/jQuery-2.2.0.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    </form>
</body>
<%--<body>
    <div style="margin-left: 43%; color: Fuchsia">
        <h1>
            FMCG</h1>
        <div>
            <div id="background">
                <img class="fullscreen" src="Images/fmcgb.jpg" />
            </div>
            <div id="login_border_one">
            </div>
            <div id="login_border">
            </div>
            <div id="head_form">
                <form id="login_form" runat="server">
                <table width="100%" style="vertical-align: top">
                    <tr>
                        <td>
                            <asp:DataList ID="DataList1" runat="server">
                                <ItemTemplate>
                                    <div id="float_bottom_left">
                                        <asp:Image ID="imgHome" ImageUrl='<%# Eval("FilePath") %>' Width="700px" ImageAlign="Middle"
                                            Height="400px" runat="server" />
                                        <img src="Images/Img_close1.jpg" id="close" width="30px">
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="Medium" Font-Bold="true"
                                ForeColor="Black" Text="User Name"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserName" Width="180px" EnableViewState="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="Medium" Font-Bold="true"
                                ForeColor="Black" Text="Password"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPassWord" runat="server" Width="180px" MaxLength="15" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnLogin" runat="server" OnClientClick="return validate()" Text="Login"
                                Width="70px" Height="25px" CssClass="BUTTON" OnClick="btnLogin_Click" />
                            &nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" Height="25px"
                                CssClass="BUTTON" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="msg" runat="server" Width="100%" ForeColor="Red" Font-Bold="True"
                                Font-Size="x-Small" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />
        </div>
        <div>
        </div>
    </div>
</body>--%>
</html>
