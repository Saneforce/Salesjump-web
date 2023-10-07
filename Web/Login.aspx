<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <title></title>
    <style type="text/css">
        .hcontent
        {
            margin-top:50px;
        }
        .mcontent {
            border-top:2px solid #a5a5a5;
            font-size:larger;
        }
        li{
            margin: 0px auto;
            padding: 5px 5px;
            
        }
        i{
            color:red;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">            
            <div class="row hcontent">
                <h1>Session Expired</h1>
            </div>
            <div class="row mcontent">
                <ul class="fa-ul">
                    <li><i class="fa fa-exclamation-triangle" aria-hidden="true"></i> Your session has been expired. kindly log in <a href="Index.aspx"><b>Click here</b></a></li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
