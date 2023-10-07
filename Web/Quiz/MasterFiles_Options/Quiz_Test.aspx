<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quiz_Test.aspx.cs" Inherits="MasterFiles_Options_Quiz_Test" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quiz Test</title>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="../../JScript/QuizImg/QuizCSS.css" rel="stylesheet" />
    <link href="../../JScript/QuizImg/countdown.css" rel="stylesheet" />
    <script src="../../JScript/QuizImg/countdown.js" type="text/javascript"></script>  
  
    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/QuizCSS.css" rel="stylesheet"
        type="text/css" /> 
    <script src="../../JScript/Service_CRM/Quiz_JS/Quiz_Test_JS.js" type="text/javascript"></script>
    <script type="text/javascript">
        function preventMultipleSubmissions() {
            var btnVal = $('#next').html();

            if (btnVal == "Submit") {
                $('#next').prop('disabled', true);
            }
        }

        window.onbeforeunload = preventMultipleSubmissions;

    </script>
  <script src = "https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
      <script src = "https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">
            <asp:Button ID="btnHomepage" runat="server" CssClass="roundCorner" Width="150px"
                Height="30px" Text="Go to Home Page" BackColor="Green" ForeColor="White" OnClick="btnHomepage_Click" />
        </asp:Panel>
        <br />
        <div id='container'>
            <div id='title'>
                <%-- <h5>Quiz</h5>--%>
            </div>
            <div id='quiz'>
            </div>
            <br />
            <div id="demo">
            </div>
            <div style="width: 100%; height: 50px">
                <div style="float: right">
                    <button id="next" class="btn btn-1">
                        Next</button>
                    <button id="prev" class="btn btn-1">
                        Prev</button>
                    <%-- <button id="start" class="btn btn-1" style="width: 120px">Start Over</button>--%>
                </div>
            </div>
        </div>
    </div>
    
    </form>
</body>
      <script>
      $(document).ready(function() {
         function disablePrev() { window.history.forward() }
         window.onload = disablePrev();
         window.onpageshow = function(evt) { if (evt.persisted) disableBack() }
      });
   </script>
</html>
