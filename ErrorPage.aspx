<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<style type="text/css">

#errmsg
{
font-size:10pt;
font-family:Verdana;
background-color:lavender;
height:20pt;
}

</style>

</head>
<body>
    <form id="form1" runat="server">
     <div id="errmsg">
    <center>
        <table>
            <tr style="height:20">
                <td align="center" valign="middle">
                    <b>We are very sorry for the inconvenience caused to you...Kindly Re-Login</b>            
                </td>
            </tr>
        </table>
    </center>
    </div>
       </form>
</body>
</html>
