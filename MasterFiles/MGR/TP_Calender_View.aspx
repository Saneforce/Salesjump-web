<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Calender_View.aspx.cs" Inherits="MasterFiles_MGR_TP_Calender_View" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Entry</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript" language="javascript">
        function open_popup() {
            alert('ok');
            return true;
        }

        function SetDate(dateValue) {
            window.open('TourPlan_Entry.aspx', 'TP Entry', 'height=500,width=1000,top=100,left=200,toolbar=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,modal=no;display:block;position:absolute;');

            //ctl = window.location.search.substr(s1).substring(4);i
            //thisForm = window.opener.document.forms[0].elements[ctl].value = dateVae;
            //self.close();
        }
    </script>

<script type = "text/javascript">
    var popUpObj;
    function showModalPopUp(dateValue) {
        popUpObj = window.open("TourPlan_Entry.aspx?TP_Date=" + dateValue,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=1000," +
    "height=500," +
    "left = 200," +
    "top=100"
    );
        popUpObj.focus();
        LoadModalDiv();
    }
      
</script>
<script type = "text/javascript">
    function LoadModalDiv() {
        var bcgDiv = document.getElementById("divBackground");
        bcgDiv.style.display = "block";
        if (bcgDiv != null) {
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            bcgDiv.style.width = "100%";
            bcgDiv.style.height = "100%";
        }
    }
</script>
 <script type = "text/javascript">
     function HideModalDiv() {
         var bcgDiv = document.getElementById("divBackground");
         bcgDiv.style.display = "none";
     }
</script> 
 <script type = "text/javascript">
     function OnUnload() {
         if (false == popUpObj.closed) {
             popUpObj.close();
         }
     }
     window.onunload = OnUnload;
 </script> 

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
        <br />   
        <center>
            <table  width="70%">
            <tr>
                <td colspan="4" align ="center">
                    <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of "
                        Font-Size="Medium" Font-Names="Lucida Calligraphy" ></asp:Label>
                    <asp:Label ID="lblmon" runat="server" 
                        Font-Size="Medium" Font-Names="Lucida Calligraphy" ForeColor="#FF3300" ></asp:Label>
                </td>
            </tr>
        </table> 
        <br />
        <table>
            <tr>
                <td>
                         <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnPreRender="Calendar1_PreRender"
                              OnVisibleMonthChanged="Calendar1_VisibleMonthChanged1">
                         </asp:Calendar>
                    </td>    
                </tr>
            </table>
        </center>
    </div>
    <div   id = "divBackground" style=" position:absolute; top:0px; left:0px;background-color:black; z-index:100;opacity: 0.8;filter:alpha(opacity=60); -moz-opacity: 0.8; overflow:hidden; display:none; text-align:center;">
    </div> 
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
