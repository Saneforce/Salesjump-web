<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Home.aspx.cs" Inherits="MasterFiles_MGR_MGR_Home" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager</title>
     <style type="text/css" xml:space="preserve" class="blink"> div.blink {text-decoration: blink;} </style>
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
<script runat="server">

</script>
  <style type="text/css">
        table, th, td {
    border: 1px solid black;
    border-collapse: collapse;
}
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: gray;
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
  
   .TextFont
        {
            text-align: center;
            margin-top: 6px;
        }
        .blink_me {
    -webkit-animation-name: blinker;
    -webkit-animation-duration: 1s;
    -webkit-animation-timing-function: linear;
    -webkit-animation-iteration-count: infinite;
    
    -moz-animation-name: blinker;
    -moz-animation-duration: 1s;
    -moz-animation-timing-function: linear;
    -moz-animation-iteration-count: infinite;
    
    animation-name: blinker;
    animation-duration: 1s;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
}

@-moz-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@-webkit-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}



.exam #lblFN  { padding: 2em 2em 0 2em; } 
#lblFN {
 width: 130px;
 height: 70px;
 background: Red;
 -moz-border-radius: 30px / 30px;
 -webkit-border-radius: 30px / 30px;
 border-radius: 30px / 30px;padding:3px;
}
.roundCorner
{
    border-radius: 25px;
    background-color: #4F81BD;

    text-align :center;
    font-family:arial, helvetica, sans-serif;
    padding: 5px 10px 10px 10px;
    font-weight:bold;
    width:100px;
    height:30px;
} 
    </style>   
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            noBack();
        }
        function noBack() {
            window.history.forward();
        }
</script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("marquee").hover(function () {
            this.stop();
        }, function () {
            this.start();            
        });
    });
  </script> 
   <script type="text/javascript">

       function OpenNewWindow() {

           window.open('DoctorBirthday_View.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

           return false;

       }

    </script>
     <script type="text/javascript">

         function OpenWindow() {

             var paramVal = "MRLink";
             window.open("NoticeBoard_design.aspx?id=" + paramVal,
              "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=700," +
    "height=800," +
    "left = 0," +
    "top=0"
    );

             return false;
         }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
          <table width="98%" style="margin-top:0px">
    <tr>
    <td style="width:55%" align="center">    
    <asp:Label ID="lblActivity" runat="server" Font-Size="Large" ForeColor="Green" Font-Italic="true" Font-Names="Verdana">Quick View</asp:Label> <br />
    <br />

<%--     <asp:Calendar ID="CalMgr" runat="server" BackColor="#FFFFCC" ForeColor="White" PrevMonthText="<img src='Images/PMonth.ico' border=0 align=top>" 
                DayNameFormat="Short" Font-Names="Book Antiqua" Font-Size="Small" Width="600px" NextMonthText="<img src='Images/NMonth.ico' border=0 align=top>" 
                 ShowGridLines="True"  Height="250px" ondayrender="CalMgr_DayRender"  WeekendDayStyle-BackColor="BlanchedAlmond"  
                BorderStyle="Solid">
                <DayHeaderStyle BackColor="#D9D9B3" ForeColor="Black" BorderStyle="Solid" Font-Italic="True"
                    Font-Size="Medium" CssClass="TextFont" BorderWidth="1" BorderColor="Black"  />
                <DayStyle BackColor="#FFFFFF" Font-Names="Adobe Kaiti Std R" BorderColor="SlateGray"
                    BorderWidth="1" Font-Bold="true" ForeColor="Red" CssClass="TextFont" />
                <NextPrevStyle Font-Italic="true" Font-Names="Arial CE" HorizontalAlign="Center" Width="15%" 
                    VerticalAlign="Middle" />
                <OtherMonthDayStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <SelectedDayStyle BackColor="LightBlue" BorderColor="SeaGreen" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
                <SelectorStyle BackColor="DarkSeaGreen" ForeColor="Snow" Font-Names="Times New Roman Greek"
                    Font-Size="Small" BorderColor="MediumSeaGreen" BorderWidth="1" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
                <TitleStyle BackColor="#8BA083" ForeColor="White" Height="35" Font-Size="Large" Font-Names="Courier New Baltic"
                    BorderColor="SlateGray" BorderWidth="1" CssClass="TextFont" />
                <TodayDayStyle Font-Size="Small" ForeColor="Red"/>
            </asp:Calendar>--%>
               <asp:Table ID="Table2" runat="server" BackColor="White" BorderStyle="Solid" Width="600px"
                        Height="230px" BorderWidth="1">
                        <asp:TableRow>
                            <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                <span style="vertical-align: bottom">
                                    <img src="Images/noticeboard.gif" alt="" width="28px" /></span>
                                <asp:LinkButton ID="LnkNotice" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red"
                                    Font-Italic="true" Font-Names="Verdana" Text="Notice Board" OnClientClick="return OpenWindow() ;"></asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                <span style="vertical-align: bottom">
                                    <img src="Images/cake2.gif" alt="" width="28px" /></span>
                                <asp:LinkButton ID="LnkDoctor" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red"
                                    Font-Italic="true" Font-Names="Verdana" Text="Customer's DOB and DOW View" OnClientClick="return OpenNewWindow() ;"></asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                <span style="vertical-align: bottom">
                                    <img src="Images/reject.png" alt="" width="28px" /></span>
                                <asp:LinkButton ID="lnkreject" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    Font-Italic="false" Font-Names="Verdana" Text="Rejection / ReEntries " OnClick="lnkreject_Click"></asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                <span style="vertical-align: bottom">
                                    <img src="Images/File.jpg" alt="" width="28px" /></span>
                                <asp:LinkButton ID="lnkfile" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    OnClick="lnkfile_Click" Font-Italic="false" Font-Names="Verdana" Text="File Upload "></asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                <span style="vertical-align: bottom;">
                                    <img src="Images/del.jpg" alt="" width="26px" /></span>
                                <asp:LinkButton ID="lnkdelay" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    Font-Italic="false" Font-Names="Verdana" Text="Delay Details "></asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                    </asp:Table>
    </td> 
 
    <td align="center"><asp:Label ID="lblSC" runat="server"  Font-Size="Large" ForeColor="Green" Font-Names="Verdana" Font-Italic="true">Short Cut</asp:Label> 
    <br />
   <br />
<asp:Table ID="Table1" runat="server" BorderStyle="Solid" Width="450px" BackColor="White" BorderWidth="1" >
 <asp:TableRow>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center" ColumnSpan="2">
                                <asp:Button ID="btnmain" runat="server" BackColor="HotPink" CssClass="roundCorner"
                                    ForeColor="White" Width="220px" Text="Goto Main Page" OnClick="btnmain_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
  <asp:TableRow>
    <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btntp" runat="server" BackColor="Chocolate" CssClass="roundCorner" ForeColor="White" Width="150px" Text="TP Entry" OnClick="btntp_Click" /></asp:TableCell>
    <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btnview" Width="150px" runat="server" BackColor="Chocolate" CssClass="roundCorner" ForeColor="White" Text="TP View" OnClick="btnview_Click"/></asp:TableCell>

</asp:TableRow>
 <asp:TableRow>
    <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btnDCR" runat="server" BackColor="BlanchedAlmond" CssClass="roundCorner" Width="150px" Text="DCR Entry" OnClick="btndcr_Click" /></asp:TableCell>
    <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btnDCRView" runat="server" BackColor="BlanchedAlmond" CssClass="roundCorner" Width="150px" Text="DCR View" OnClick="btndcrview_Click" /></asp:TableCell>

</asp:TableRow>
 <asp:TableRow>
    <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btnTerr" runat="server" BackColor="#F5C4F2"  CssClass="roundCorner" Width="150px" Text="Expense Entry" OnClick="btndExp_Click" /></asp:TableCell>
    <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btnlisteddr" runat="server" BackColor="#F5C4F2" CssClass="roundCorner" Width="150px" Text="Expense View"  OnClick="BtnExp1_Click" /></asp:TableCell>

</asp:TableRow>
<asp:TableRow>

 <asp:TableCell BorderWidth="1" Width="200px" HorizontalAlign="Center"><asp:Button ID="btnmail" runat="server" BackColor="#FFCCCC"  CssClass="roundCorner" Width="150px" Text="Internal Mail Box" OnClick="btnmail_Click" /></asp:TableCell>
 <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
<br />
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
<br />
</asp:TableCell>
<asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
<br />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
<br />
</asp:TableCell>
<asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
<br />
</asp:TableCell>
</asp:TableRow>


</asp:Table>
    
    </td>

    </tr>
    
    </table>
    <br />
    <br />
   
   <%-- <marquee scrollamount="15" direction="up" behavior="alternate"><marquee scrollamount="15" direction="right" behavior="alternate"></marquee></marquee>--%>
   <center>
    <asp:Panel ID="pnlhome" runat="server" BorderWidth="1" Width="90%" >
    <table width="100%" border="1">
 <tr>

 <td align="center" style="background-color:#8BA083; height:20px"> <span class="blink_me">
 <asp:Label ID="lblFN" runat="server" Text="Flash News"  Font-Bold="true" Font-Size="12px" Font-Italic="true" ForeColor="white" ></asp:Label></span>
 </td> 
 </tr>
 <tr>
 <td>
 <marquee onMouseOver="this.setAttribute('scrollamount', 0, 0);" OnMouseOut="this.setAttribute('scrollamount', 6, 0);">
 <%--<marquee onmouseover="this.stop();" onmouseout="this.start();" direction="left" scrolldelay="4" scrollamount="2" behavior="scroll">--%>
 <asp:Label ID="lblFlash" runat="Server"  style="margin-top:10px;" Width="100%" ForeColor="Brown" Font-Size="16px" Font-Names="Tahoma"  Text='<%# Eval("FN_Cont1") %>' /></marquee>


 
 </td>
 </tr>
 </table>
 </asp:Panel>
   <asp:Panel ID="Panel1" runat="server" BorderWidth="1" Width="90%" >
  <table>
 <tr>
<td align="left" width="5%">
 <img src="Images/talk.gif" alt="" width="80px" />
 <%--<a href="">
 <asp:Label ID="lblNotice" runat="server" Text="Notice Board" ForeColor="Blue" Font-Size="Medium" Font-Bold="true"  Font-Italic="true" Font-Names="Verdana"/></a>--%>
 </td>
 <td style="vertical-align:middle" align="left">
 <asp:Label ID="lblsup" runat="server" ForeColor="Brown"  Font-Size="14px" Font-Names="Tahoma" Text='<%# Eval("TalktoUs_Text") %>' > 
</asp:Label> 
 </td>
 </tr>
 </table>
 </asp:Panel>
 </center>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />
        </div>
    </div>
    
    </form>
</body>
</html>
