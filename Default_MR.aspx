<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_MR.aspx.cs" Inherits="Default_MR" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Reporting Sales & Analysis</title>
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
            height:25px;
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
    <style type="text/css" xml:space="preserve" class="blink">
        div.blink
        {
            text-decoration: blink;
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
    <script language="C#" runat="server">

        void DayRender(Object source, DayRenderEventArgs e)
        {

            if (!e.Day.IsOtherMonth && !e.Day.IsWeekend)
                e.Cell.BackColor = System.Drawing.Color.Yellow;

        }

    </script>
    <script type="text/javascript">

        function OpenNewWindow() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('DoctorBirthday_View.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>
    <script type="text/javascript">

        function OpenWindow() {

            //          //  window.open("NoticeBoard_design.aspx");
            //            window.open('NoticeBoard_design.aspx', null, 'height=800, width=700,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');

            //            return false;
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

            //window.open('NoticeBoard_design.aspx', null, 'height=500, width=900, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px;">
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <div>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdProduct" runat="server" Width="85%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProDes" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Unit1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsam1" runat="server" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Unit2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsam2" runat="server" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Unit3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsam3" runat="server" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgr" runat="server" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcar" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="98%" style="margin-top: 0px">
            <tr>
                <td style="width: 55%" align="center">
                    <asp:Label ID="lblActivity" runat="server" Font-Size="Large" ForeColor="Blue" Font-Italic="true"
                        Font-Names="Verdana">Quick View</asp:Label>
                    <br />
                    <br />
              <%--      <asp:Calendar ID="Calendar1" runat="server" BackColor="Blue" ForeColor="White" PrevMonthText="<img src='images/PMonth.ICO' border=0 align=top>"
                        DayNameFormat="Short" Font-Names="Book Antiqua" Font-Size="Small" Width="600px" 
                        NextMonthText="<img src='Images/NMonth.ICO' border=0 align=top>" ShowGridLines="True"
                        Height="250px" OnDayRender="Calendar1_DayRender" WeekendDayStyle-BackColor="Lavender"
                        BorderStyle="Solid">
                        <DayHeaderStyle BackColor="#e0f3ff" ForeColor="Black" BorderStyle="Solid" Font-Italic="True"
                            Font-Size="14px" CssClass="TextFont" BorderWidth="1" BorderColor="Black"  />
                        <DayStyle BackColor="#FFFFFF" Font-Names="Adobe Kaiti Std R" BorderColor="SlateGray" VerticalAlign="Top" 
                            BorderWidth="1" Font-Bold="true" ForeColor="#666666" CssClass="TextFont" />
                        <NextPrevStyle Font-Italic="true" Font-Names="Arial CE" Width="15%" />
                        <OtherMonthDayStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                        <SelectedDayStyle BackColor="LightBlue" Font-Size="Small" BorderColor="SeaGreen"
                            HorizontalAlign="Center" VerticalAlign="Top" CssClass="TextFont" />
                        <SelectorStyle BackColor="DarkSeaGreen" ForeColor="Snow" Font-Names="Times New Roman Greek" 
                            Font-Size="Small" BorderColor="MediumSeaGreen" BorderWidth="1" HorizontalAlign="Center"
                            VerticalAlign="Middle" CssClass="TextFont" />
                        <TitleStyle BackColor="#7AA3CC" Height="20px" Font-Size="Large" Font-Names="Courier New Baltic"
                            BorderColor="SlateGray" BorderWidth="1" CssClass="TextFont" />
                        <TodayDayStyle Font-Size="Small" ForeColor="Blue" CssClass="TextFont" />
                    </asp:Calendar>--%>
                            <asp:Table ID="Table2" runat="server" BackColor="White" BorderStyle="Solid" Width="600px" Height="220px"
                        BorderWidth="1">
                        <asp:TableRow>
                            <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                       <span style="vertical-align: bottom">
                             <img src="Images/noticeboard.gif" alt="" width="28px" /></span>
                                <asp:LinkButton ID="LnkNotice" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    Font-Italic="false" Font-Names="Verdana" Text="Notice Board" OnClientClick="return OpenWindow() ;"></asp:LinkButton>
                             </asp:TableCell>
                          
                        </asp:TableRow>
                        <asp:TableRow>
                           <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                       <span style="vertical-align: bottom">
                                <img src="Images/cake2.gif" alt="" width="28px" /></span>
                                <asp:LinkButton ID="LnkDoctor" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    Font-Italic="false" Font-Names="Verdana" Text="Customer's DOB and DOW View" OnClientClick="return OpenNewWindow() ;"></asp:LinkButton>
                             </asp:TableCell>
                          
                        </asp:TableRow>
                        <asp:TableRow>
                          <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                   <span style="vertical-align: bottom">
                                <img src="Images/reject.png" alt="" width="28px" /></span>
                      <asp:LinkButton ID="lnkreject" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    Font-Italic="false" Font-Names="Verdana" Text="Rejection / ReEntries " OnClick="lnkreject_Click" ></asp:LinkButton>
                             </asp:TableCell>
                          
                        </asp:TableRow>
                        <asp:TableRow>
                           <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                                <span style="vertical-align: bottom">
                                <img src="Images/File.jpg" alt="" width="28px" /></span>
                      <asp:LinkButton ID="lnkfile" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true" OnClick="lnkfile_Click"
                                    Font-Italic="false" Font-Names="Verdana" Text="File Upload " ></asp:LinkButton>
                             </asp:TableCell>
                          
                        </asp:TableRow>
                        <asp:TableRow>
                             <asp:TableCell Width="300px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                               <span style="vertical-align: bottom;">
                            <img src="Images/del.jpg" alt="" width="26px" /></span>
                         <asp:LinkButton ID="lnkdelay" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"
                                    Font-Italic="false" Font-Names="Verdana" Text="Delay Details " ></asp:LinkButton>
                             </asp:TableCell>
                          
                        </asp:TableRow>
                        <asp:TableRow>
                         <asp:TableCell Width="200px" BorderWidth="1" ColumnSpan="2" HorizontalAlign="Center">
                     
                             </asp:TableCell>
                          
                        </asp:TableRow>
                      
                   
                    </asp:Table>
                </td>
                 <td align="center">
                    <asp:Label ID="lblSC" runat="server" Font-Size="Large" ForeColor="Blue" Font-Names="Verdana"
                        Font-Italic="true">Short Cut</asp:Label>
                    <br />
                    <br />
                    <asp:Table ID="Table1" runat="server" BackColor="White" BorderStyle="Solid" Width="450px" Height="100px"
                        BorderWidth="1">
                        <asp:TableRow>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                     
                                <asp:Button ID="btntp" runat="server" BackColor="Chocolate" ForeColor="White" Width="160px" CssClass="roundCorner" 
                                    Text="TP Entry" OnClick="btntp_Click" /></asp:TableCell>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnview" Width="160px" runat="server" BackColor="Chocolate" ForeColor="White" CssClass="roundCorner" 
                                    Text="TP View" OnClick="btnview_Click" /></asp:TableCell>
                        </asp:TableRow>
                  <%--<asp:TableRow>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnDCR" runat="server" BackColor="BlanchedAlmond" Width="160px" Text="DCR Entry" CssClass="roundCorner" 
                                    OnClick="btndcr_Click" /></asp:TableCell>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnDCRView" runat="server" BackColor="BlanchedAlmond" Width="160px" CssClass="roundCorner" 
                                    Text="DCR View" OnClick="btndcrview_Click" /></asp:TableCell>
                        </asp:TableRow>
                     <asp:TableRow>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnTerr" runat="server" BackColor="#F5C4F2" Width="160px" Text="Territory Entry" CssClass="roundCorner" 
                                    OnClick="btnTerr_Click" />
                            </asp:TableCell>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnlisteddr" runat="server" BackColor="#F5C4F2" Width="160px" Text="Listed Dr Entry" CssClass="roundCorner" 
                                    OnClick="btnlisteddr_Click" /></asp:TableCell>
                        </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnEx_entry" runat="server" BackColor="LightBlue" Width="160px" CssClass="roundCorner" Text="Expense Entry" OnClick="btnEx_entry_Click" /></asp:TableCell>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnEx_view" runat="server" BackColor="LightBlue" Width="160px" CssClass="roundCorner" Text="Expense View" OnClick="btnEx_view_Click" /></asp:TableCell>
                        </asp:TableRow>
                       <%-- <asp:TableRow>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnSS_entry" runat="server" BackColor="#CCCCCC" Width="160px" CssClass="roundCorner" Text="SS Entry" OnClick="btnSS_entry_Click" /></asp:TableCell>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
                                <asp:Button ID="btnSS_view" runat="server" BackColor="#CCCCCC" Width="160px" CssClass="roundCorner" Text="SS View" OnClick="btnSS_view_Click" /></asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow>
                           <asp:TableCell Width="200px" HorizontalAlign="Center">
                                <asp:Button ID="btnmail" runat="server" BackColor="#FFCCCC" CssClass="roundCorner" Width="160px" Text="Internal Mail Box"
                                    OnClick="btnmail_Click" /></asp:TableCell>
                            <asp:TableCell Width="200px" BorderWidth="1" HorizontalAlign="Center">
<br />
                            </asp:TableCell>
                        </asp:TableRow>--%>                
                                                     
                    </asp:Table>
                </td>
            </tr>
        </table>
        <br />
        <%-- <marquee scrollamount="15" direction="up" behavior="alternate"><marquee scrollamount="15" direction="right" behavior="alternate"></marquee></marquee>--%>
        <center>
            <asp:Panel ID="pnlhome" runat="server" BorderWidth="1" Width="90%">
                <table width="100%" border="1" style="border-width: 1">
                    <tr>
                        <td align="center" style="background-color: #7AA3CC; height: 20px">
                            <span class="blink_me">
                                <asp:Label ID="lblFN" runat="server" Text="Flash News" Font-Bold="true" Font-Size="12px"
                                    Font-Italic="true" ForeColor="white"></asp:Label></span>
                        </td>
                    </tr>
                    <tr>
                        <td >
                          
                            <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);">
 <%--<marquee onmouseover="this.stop();" onmouseout="this.start();" direction="left" scrolldelay="4" scrollamount="2" behavior="scroll">--%>
 <asp:Label ID="lblFlash" runat="Server"  style="margin-top:10px;" Width="100%" ForeColor="Brown" Font-Size="16px" Font-Names="Tahoma"  Text='<%# Eval("FN_Cont1") %>' /></marquee>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1" Width="90%">
                <table>
                    <tr>
                        <td align="left" width="5%">
                            <img src="Images/talk.gif" alt="" width="80px" />
                            <%--<a href="">
 <asp:Label ID="lblNotice" runat="server" Text="Notice Board" ForeColor="Blue" Font-Size="Medium" Font-Bold="true"  Font-Italic="true" Font-Names="Verdana"/></a>--%>
                        </td>
                        <td style="vertical-align: middle;" align="left">
                            <asp:Label ID="lblsup" runat="server" ForeColor="Brown" Font-Size="14px" Font-Names="Tahoma" Text='<%# Eval("TalktoUs_Text") %>'> 
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
