<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>E-Reporting Sales & Analysis</title>
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }

            .loading {
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

            .padding {
                padding-left: 10px;
            }
        </style>
        <style type="text/css">
            table, td, th {
                border: 1px solid black;
            }
            
            table.gridtable {
                font-family: verdana,arial,sans-serif;
                font-size: 11px;
                border-width: 1px;
                border-color: #666666;
                border-collapse: collapse;
            }
            
            table.gridtable th {
                padding: 5px;
                border:1px solid;                
                border-color:  #666666;
                background-color: #A6A6D2;        

            }
            
            table.gridtable td {
                border-width: 1px;
                padding: 5px;
                border-style: solid;
                border-color: #666666;
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
                0% {
                    opacity: 1.0;
                }

                50% {
                    opacity: 0.0;
                }

                100% {
                    opacity: 1.0;
                }
            }

            @-webkit-keyframes blinker {
                0% {
                    opacity: 1.0;
                }

                50% {
                    opacity: 0.0;
                }

                100% {
                    opacity: 1.0;
                }
            }

            @keyframes blinker {
                0% {
                    opacity: 1.0;
                }

                50% {
                    opacity: 0.0;
                }

                100% {
                    opacity: 1.0;
                }
            }

            .exam #lblFN {
                padding: 2em 2em 0 2em;
            }

            #lblFN {
                width: 130px;
                height: 70px;
                background: Red;
                -moz-border-radius: 30px / 30px;
                -webkit-border-radius: 30px / 30px;
                border-radius: 30px / 30px;
                padding: 3px;
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
        
        <script type="text/javascript">

            function OpenNewWindow() {

                //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

                window.open('DoctorBirthday_View.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
                return false;
            }
        </script>
        
        <script type="text/javascript">

            function OpenWindow() {

                //window.open("NoticeBoard_design.aspx");
                //window.open('NoticeBoard_design.aspx', null, 'height=800, width=700,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
                //return false;

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
        
        <script type="text/javascript">

            function OpenNewWindow_delay() {

                //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

                window.open('Delayed_Status.aspx', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
                return false;
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <ucl:Menu ID="menu" runat="server" />
                <asp:Panel ID="pnldivi" runat="server" BorderWidth="0" CssClass="padding">
                    <table id="wrapper" width="99%" align="center" style="border-collapse: collapse;border: 1px solid black;">
                        <tr>
                            <td valign="top" width="20%" style="border: 1px solid black;">
                                <table id="tblDivision" style="width: 300px; border-collapse: collapse; height: 350px"
                                    border="0" cellspacing="0" cellpadding="0" align="center">
                                    <tr>
                                        <td align="center" style="height: 20px; width: 10%;">
                                            <asp:Label ID="Label8" runat="server" Text="Division Selection" ForeColor="#336277"
                                                Font-Size="Larger" Font-Bold="true" Width="230px">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 100%; width: 15%;" valign="top">
                                            <asp:ListBox ID="dd1division" Width="100%" Height="100%" runat="server" Style="border: solid 1px black;border-collapse: collapse;" AutoPostBack="false" 
                                                OnDataBound="ddldivision_DataBound" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged">
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%; height: 10px;" align="left" valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblenter" runat="server" Font-Size="Small" ForeColor="Black" Style="text-align: left" Text="Select division then click Enter" Width="220px" Font-Names="Verdana"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Button ID="btnSelect" runat="server" Text="Enter" BackColor="#336277" ForeColor="White" CssClass="BUTTON" Font-Bold="True"
                                                            OnClick="btnSelect_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="85%" align="center" valign="top" style="height: 280px;border-collapse: collapse; vertical-align: top">
                                <table align="center" style=" width: 100%; border-collapse: collapse; ">
                                    <tr>
                                        <td align="center" style="border-bottom: solid 1px; vertical-align:bottom">
                                            <span style="vertical-align: bottom">
                                                <asp:Label ID="lblimg" runat="server" Visible="false"><img src="Images/mail.gif" alt=""  /></asp:Label>
                                            </span>
                                            
                                            <a href="MasterFiles/MailBox.aspx" runat="server">
                                                <asp:Label ID="LnkNoMail" runat="server" Font-Size="14px" ForeColor="DarkRed" Font-Names="Verdana"
                                                    Visible="false"></asp:Label>
                                            </a>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblNoMail" runat="server" Visible="false" Font-Size="14px" ForeColor="DarkRed" Font-Names="Verdana" Text="---No Mails Received---"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom: solid 1px">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-bottom: solid 1px; " >
                                            <span style="vertical-align: bottom">
                                                <img src="Images/cake2.gif" alt="" width="26px" /></span>
                                            <asp:LinkButton ID="lnkDob" runat="server" Font-Size="14px" ForeColor="DarkViolet" Font-Names="Verdana"  
                                                Text="Click here to View Customer's DOB and DOW" OnClientClick="return OpenNewWindow() ;">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top: solid 1px; border-bottom: solid 1px">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom: solid 1px; " align="center">
                                            <span style="vertical-align: bottom;">
                                                <img src="Images/del.jpg" alt="" width="26px" />
                                            </span>
                                            <asp:LinkButton ID="lnkDelayed" runat="server" Font-Size="14px" ForeColor="DeepPink" Font-Names="Verdana"  
                                                Text="Delayed Report Status" OnClientClick="return OpenNewWindow_delay();">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-width: 1px; border-bottom: 1px; border-top: solid 1px;">
                                            <br />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="center" style="border-width: 1px; border-bottom: 1px; border-top: solid 1px;">
                                            <span style="vertical-align: bottom">
                                                <img src="Images/noticeboard.gif" alt="" width="26px" />
                                            </span>
                                            <asp:LinkButton ID="lnknotice" runat="server" Font-Size="14px" ForeColor="DarkGreen" Font-Names="Verdana" 
                                                Text="Notice Board" OnClientClick="return OpenWindow();">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-width: 1px; border-bottom: 1px; border-top: solid 1px;">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-width: 1px; border-bottom: 1px; border-top: solid 1px;">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-width: 1px; border-bottom: 1px; border-top: solid 1px;">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-width: 1px; border-bottom: 1px; border-top: solid 1px; text-align:center;" rowspan="2" colspan="2">
                                            <div style="vertical-align: middle">
                                                <img src="Images/rose_front.gif" alt="" width="100px" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <center>
                    <asp:Panel ID="Panel1" runat="server" BorderWidth="1" Width="98%" >
                        <table width="100%" border="1" >
                            <tr>
                                <td align="center" style="background-color: #336277; height: 20px">
                                    <span class="blink_me">
                                        <asp:Label ID="lblFN" runat="server" Text="Flash News" Font-Bold="true" Font-Size="12px"   
                                            Font-Italic="true" ForeColor="white"></asp:Label>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);">
                                        <%--<marquee onmouseover="this.stop();" onmouseout="this.start();" direction="left" scrolldelay="4" scrollamount="2" behavior="scroll">--%>
                                        <asp:Label ID="lblFlash" runat="Server"  style="margin-top:10px;" Width="100%" ForeColor="Brown" Font-Size="16px" Font-Names="Tahoma"  Text='<%# Eval("FN_Cont1") %>' />
                                    </marquee>
                                </td>
                            </tr>
                            <tr>
                                <td><br /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlhome" runat="server" BorderWidth="1" Width="98%">
                        <table >
                            <%--    <tr>
                                <td>
                                <marquee onmouseout="this.setAttribute('scrollamount', 6, 0);" 
                                onmouseover="this.setAttribute('scrollamount', 0, 0);">
                                <asp:Label ID="lblSupport" runat="Server" Font-Names="Tahoma" Font-Size="16px" 
                                ForeColor="Brown" style="margin-top:10px;" 
                                text="For Online Support Nos:- 9841737722, 9841316719, 9841266611, 9841633008 " 
                                Width="100%" />
                                </marquee>
                                </td>
                                </tr>--%>
                            <tr>
                                <td align="left" width="5%">
                                    <img src="Images/talk.gif" alt="" width="80px" />
                                    <%--<a href="">
                                        <asp:Label ID="lblNotice" runat="server" Text="Notice Board" ForeColor="Blue" Font-Size="Medium" Font-Bold="true"  Font-Italic="true" Font-Names="Verdana"/></a>--%>
                                </td>
                                <td style="vertical-align: middle;" align="left">
                                    <%--<asp:Label ID="lblsup" runat="server" ForeColor="Brown" Font-Size="14px" Font-Names="Tahoma"> ---> 9841737722 (Hindi,English,Gujarthi), 9841316719(Hindi,English,Tamil), 9841266611(Marathi,Hindi,English,Malayam,Telugu), 9841633008(Tamil,English)
                                        </asp:Label>--%>
                                    <%--<asp:Label ID="lblSupport" runat="Server" Font-Names="Tahoma" Font-Size="16px" ForeColor="Brown" style="margin-top:10px;" 
                                        Text="  ---> 9841737722 (Hindi,English,Gujarthi), 9841316719(Hindi,English,Tamil), 9841266611(Marathi,Hindi,English,Malayalam,Telugu), 9841633008(Tamil,English) " 
                                        Width="100%" />
                                        --%>
                                    <asp:Label ID="lblSupport" runat="Server"  style="margin-top:10px;" Width="100%" ForeColor="Brown" Font-Size="16px" Font-Names="Tahoma"  Text='<%# Eval("TalktoUs_Text") %>' />
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
