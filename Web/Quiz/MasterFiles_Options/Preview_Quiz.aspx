<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Preview_Quiz.aspx.cs" Inherits="MasterFiles_Options_Preview_Quiz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview Quiz</title>
    <style type="text/css">
        .boxshadow
        {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }
        .roundbox
        {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }
        .grd
        {
            border: 1;
            border-color: Black;
        }
        .roundbox-top
        {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }
        .roundbox-bottom
        {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }
        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
        {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }
        .gridheaderleft
        {
            text-align: left;
        }
        .gridheaderright
        {
            text-align: right;
        }
        .gridheaderbig
        {
            font-size: 135%;
        }
        
        
        .Space label
        {
            margin-left: 5px;
            margin-right: 10px;
        }
     .modal
    {
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
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: Black;
        z-index: 999;
    }
     .timelft
     {
         font-weight:bold;
         font-family:Arial;
         font-size:x-large;
         color:#6600CC;
     }
    </style>
      
          <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

    function preventMultipleSubmissions() {

        $('#<%=btnFinalSubmit.ClientID %>').prop('disabled', true);

    }

    window.onbeforeunload = preventMultipleSubmissions;

    </script>

<script type = "text/javascript" >

    function preventBack() { window.history.forward(); }

    setTimeout("preventBack()", 0);

    window.onunload = function () { null };

</script>

    <link href="../../JScript/ClassicGridView.css" rel="stylesheet" type="text/css" />
       <link href="../../JScript/AlertCSS.css" rel="stylesheet" type="text/css" />

       <script type="text/javascript">
           $(document).ready(function () {
               window.setInterval(function () {
                   var timeLeft = $("#timeLeft").html();
                   var timeSplit = timeLeft.split(':');
                   var ttlSec = timeSplit[0] * 60 + timeSplit[1] * 1;
                   if (eval(ttlSec) == 0) {
                       window.location = ("http://saneffa.info/");
                   } else {
                       //$("#timeLeft").html(eval(ttlSec) - eval(1));
                       var time = eval(ttlSec) - eval(1);
                       var mins = Math.floor(time / 60);
                       var sec = time - (mins * 60);
                       var minsORsec;
                       if (mins === 0) 
                       {
                           minsORsec = " Seconds";
                       }
                       else 
                       {
                           minsORsec = " Minutes";
                       }
                       $("#timeLeft").html(mins + ':' + sec + " : " + minsORsec);
                   }
               }, 1000);
           });  
    </script>

    <style type="text/css">
    .rbl input[type="radio"]
{
   margin-left: 10px;
   margin-right: 1px;
}
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <br />
    <div>
    <asp:Panel ID="pnlnot" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
            <tr>
                <td>
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="Red" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="Red" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
            </tr>
              <tr>
                <td colspan="2">
                    <hr style="border-style:dashed; border-width:2px" />
                </td>
            </tr>
            </table>
           </asp:Panel>

    <center>
        <br />
           <img src="../../Images/blue.png" alt="" />
        <br />
        <div class="roundbox boxshadow" style="width: 1000px;  border: solid 2px steelblue;">
            <div class="gridheaderleft">
                Tick the correct options</div>
            <div class="boxcontenttext" style="background: khaki;">
                <div id="pnlPreviewSurveyData">
                    <asp:HiddenField ID="hidSurveyId" runat="server" />
                    <asp:GridView ID="grdPreviewQuestion" GridLines="None" HorizontalAlign="Center" BorderStyle="None"
                        runat="server" Width="95%" AutoGenerateColumns="false" CellPadding="0" OnRowCreated="grdPreviewSurvey_RowCreated"
                        OnPreRender="GridView1_PreRender">
                        <HeaderStyle BorderStyle="None" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblqusid" runat="server" Text='<%#Eval("Question_Id")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Question_type_id" Visible="false">
                                <ItemStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Question_Text" Visible="false">
                                <ItemStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left"> 
                           <ItemTemplate>
                              <asp:Label ID="lblRowNo" runat="server" ></asp:Label>
                       
                             <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                        <br />
                     <br />
                                  <%--  <asp:Panel ID="pnlAnswerOptions" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# (grdPreviewQuestion.PageIndex * grdPreviewQuestion.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                                                </td>
                                                <td>--%>
                                                    <asp:RadioButtonList ID="rblans" RepeatDirection="Horizontal"  CssClass="rbl"
                                                    runat="server">
                                                    </asp:RadioButtonList>
                                                      <br />
              
                                              <%--  </td>
                                            </tr>
                                        </table>
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                    </asp:Panel>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle BorderStyle="None" />
                                <ItemTemplate>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--     <asp:ImageButton ID="imgprev" runat="server"  ImageUrl="~/Images/PrevHand.gif" 
                        ImageAlign="Left" onclick="imgprev_Click" />
                  &nbsp;&nbsp;
                  <asp:ImageButton ID="imgNext" runat="server" ImageUrl="~/Images/NextHand.gif" 
                        ImageAlign="Right" onclick="imgNext_Click" />--%>
                    <%--  <asp:Button ID="btnNext" runat="server" Text="Next" />--%>
                    <center>
                        <div>
                            <asp:ScriptManager ID="SM1" runat="server">
                            </asp:ScriptManager>
                           
                        </div>
                        <asp:Label CssClass="timelft" Text="Time Left   " runat="server" /><span class="timelft" id="timeLeft">10:01</span>
    <%--                    <div>
                            <asp:UpdatePanel ID="updPnl" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblTimer" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large"
                                        ForeColor="#6600CC"></asp:Label>
                                         <asp:Timer ID="timer1" runat="server" Interval="1000" OnTick="timer1_tick">
                            </asp:Timer>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="timer1" EventName="tick" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>--%>
                        <div style="margin-left: 80%; padding-bottom: 20px">
                            <%--  <asp:Button ID="btnFinalSubmit" runat="server" Text="Final Submit"/>--%>
                            <asp:ImageButton ID="btnFinalSubmit" runat="server" ImageUrl="~/Images/submit.png" OnClick="btnFinalSubmit_Click" />
                        </div>
                    </center>
                </div>
            </div>
        </div>
         <ul id="UlPaging" class="pagination">
        </ul>
          <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
        <script src="../../JScript/jquery.simplePagination.js" type="text/javascript"></script>
        <link href="../../JScript/simplePagination.css" rel="stylesheet" type="text/css" />
        <link href="../../JScript/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <script src="../../JScript/Paging.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () 
            {
               $("#btnFinalSubmit").css('display', 'none');
               PagingCommon("#grdPreviewQuestion");
            });
        </script>
    </center>
       <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../../Images/loader.gif" alt="" />
</div> 
</div>
    </form>
</body>
</html>
