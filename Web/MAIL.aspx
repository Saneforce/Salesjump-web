<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/mail.master"CodeFile="MAIL.aspx.cs" Inherits="MIS_Reports_MAIL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
 <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

<style type="text/css">
   .popup{
    position:absolute;
    top:-111px;
    left:207px;
    margin:138px auto;
    width:572px;
    height:450px;
    font-family:verdana;
    font-size:13px;
    padding:10px;
    background-color:#f4f4f4;
    border:2px solid #14B1BB;
    z-index:100000000000000000;
    }
    .cancel{
    display:relative;
   cursor: pointer;
    margin: 0;
    float: right;
    height: 15px;
    width: 44px;
    padding: 4px 11px 14px 1px;
    background-color: #00a4ff;
    text-align: center;
    font-weight: bold;
    font-size: 8px;
    color: white;
    border-radius: 3px;
    z-index: 100000000000000000;
    border-style: solid;
    }

.cancel:hover{
    background:rgb(255,50,50);
    }
    
.regular-checkbox {
background-color: #FF0000;
}
    .ddl
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        background-image:url('css/download%20(2).png');
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
         .ddl1
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        
  
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
    .modalBackground
    {
       
           width:200px;
           height:500px;
      
    }
    .modalPopup
    {
        background-color: #FFFFFF;
       width:200px;
        height:500px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0;
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
            width: 40px;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
    
        .Grid        {

            background-color: #fff;

            margin: 5px 0 10px 0;

            border: solid 1px #525252;

            border-collapse: collapse;

            font-family: Calibri;

            color: #474747;

        }

        .Grid td{

            padding: 2px;

            border: solid 1px #c1c1c1;

        }

        .Grid th{

            padding: 4px 2px;

            color: #fff;

            background: #14B1BB;

            border-left: solid 1px #525252;

            font-size: 0.9em;
             Font-Names:-webkit-pictograph;
              

        }

    </style>
    <script type="text/javascript">

        var rw = $("#ctl00_ContentPlaceHolder1_GridView1").find("tr")[0];

        $(function () {
            $("[id*=ctl00_ContentPlaceHolder1_GridView1] td").click(function () {
               
                //                $find("ctl00_ContentPlaceHolder1_pnlPopup").show();
                //                ctl00_ContentPlaceHolder1_pnlPopup.show();

                dv = document.getElementById("popupdiv");
                dv.style = "width:572px;height:450px;"
                //  dv.attributes('style', 'width:500px;height:300px;');


                dv.className = 'popup';
                var cancel = document.createElement('div');
                cancel.id = "canl";
                cancel.className = 'cancel'; cancel.innerHTML = 'Close';
                cancel.onclick = function (e) { cdv = document.getElementById("canl"); dv.className = ''; cdv.parentNode.removeChild(cdv); $("#popupdiv").hide(); };
                dv.appendChild(cancel);



                 
                DisplayDetails($(this).closest("tr"));

            });
        });
        function auto_load() {
            $.ajax({
                url: "MAIL.aspx",
                cache: false,
                success: function (data) {
                  document.getElementById("popupdiv").html(data);
                }
            });
        }
    function DisplayDetails(row) {
        var message = "";
        document.getElementById("ctl00_ContentPlaceHolder1_subj").innerHTML = $("td", row).eq(0).text();
        document.getElementById("ctl00_ContentPlaceHolder1_from").innerHTML = $("td", row).eq(1).text();
        document.getElementById("ctl00_ContentPlaceHolder1_message").innerHTML = $("td", row).eq(2).text();
        document.getElementById("ctl00_ContentPlaceHolder1_Datetime").innerHTML = $("td", row).eq(3).text(); 
        message += "Id: " + $("td", row).eq(0).html();
        message += "\nName: " + $("td", row).eq(1).html();
        message += "\nDescription: " + $("td", row).eq(2).html();
      
        
    }
  
</script>
   
 


<script type="text/javascript">
   
        
        function tSpeedValue(button) {

            var discode = document.getElementById("<%=Searchtxt.ClientID %>");
               var gridid ='ctl00_ContentPlaceHolder1_GridView1';

                var selectValue = discode.value;

        alert(selectValue);
              $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "MAIL.aspx/GetData",
                     data:JSON.stringify({selectValue}),
                    dataType: "json",
                    success: function (data) {

                   
                    var rw=$("#ctl00_ContentPlaceHolder1_GridView1").find("tr")[0];
                    $("#ctl00_ContentPlaceHolder1_GridView1").empty();
                    if (data.d.length > 0) {
                         $("#ctl00_ContentPlaceHolder1_GridView1").append(rw);
                       
                        for (var i = 0; i < data.d.length; i++) {

                            $("#ctl00_ContentPlaceHolder1_GridView1").append("<tr><td>" + 
                            data.d[i].Mail_Subject + "</td> <td>" + 
                            data.d[i].Mail_SF_From + "</td> <td>" + 
                            data.d[i].Mail_Content + "</td> <td>" + 
                            data.d[i].Mail_Sent_Time + "</td></tr>");
                        }
                    }
                    },
                    error: function ajaxError(result) {
                        alert(result.status + ' : ' + result.statusText);
                    }
                });

     };

   
  

</script>
<script type="text/javascript">
    function rowb() {
    }
    $(function () {
        $(".table1").bind("mouseover", function () {
            $(this).css("background-color", "#d6e9c6");
        });
        $(".table1").bind("mouseout", function () {
            $(this).css("background-color", "transparent");

        });
    });
    </script>
   

    <meta charset="utf-8">

  
        <meta name="viewport" content="width=device-width, initial-scale=1">
   <%-- <link href="http://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet">--%>
    <style type="text/css">
        body{ margin-top:50px;}
.nav-tabs .glyphicon:not(.no-margin) { margin-right:10px; }
.tab-pane .list-group-item:first-child {border-top-right-radius: 0px;border-top-left-radius: 0px;}
.tab-pane .list-group-item:last-child {border-bottom-right-radius: 0px;border-bottom-left-radius: 0px;}
.tab-pane .list-group .checkbox { display: inline-block;margin: 0px; }
.tab-pane .list-group input[type="checkbox"]{ margin-top: 2px; }
.tab-pane .list-group .glyphicon { margin-right:5px; }
.tab-pane .list-group .glyphicon:hover { color:#FFBC00; }
a.list-group-item.read { color: #222;background-color: #F3F3F3; }
hr { margin-top: 5px;margin-bottom: 10px; }
.nav-pills>li>a {padding: 5px 10px;}

.ad { padding: 5px;background: #F5F5F5;color: #222;font-size: 80%;border: 1px solid #E5E5E5; }
.ad a.title {color: #15C;text-decoration: none;font-weight: bold;font-size: 110%;}
.ad a.url {color: #093;text-decoration: none;}
    </style>
    <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="http://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>

<body>
<form id="Form1" runat="server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
    </asp:ToolkitScriptManager>
<div class="container">
    <div class="row">
        <div class="col-sm-3 col-md-2">
            <div class="btn-group">
                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                    Mail <span class="caret"></span>
                </button>
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Mail</a></li>
                    <li><a href="#">Contacts</a></li>
                    <li><a href="#">Tasks</a></li>
                </ul>
            </div>
        </div>
        <div class="col-sm-9 col-md-10">
            <!-- Split button -->
            <div class="btn-group">
                <button type="button" class="btn btn-default">
                    <div class="checkbox" style="margin: 0;">
                        <label>
                            <input type="checkbox">
                        </label>
                    &nbsp;&nbsp;&nbsp;</div>
                </button>



              <%--  <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    <span class="caret"></span><span class="sr-only">Toggle Dropdown</span>
                </button>--%>
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#">All</a></li>
                    <li><a href="#">None</a></li>
                    <li><a href="#">Read</a></li>
                    <li><a href="#">Unread</a></li>
                    <li><a href="#">Starred</a></li>
                    <li><a href="#">Unstarred</a></li>
                </ul>
            </div>
            <button type="button" class="btn btn-default" data-toggle="tooltip" title="Refresh">
                &nbsp;&nbsp;&nbsp;<span class="glyphicon glyphicon-refresh"></span>&nbsp;&nbsp;&nbsp;</button>
            <!-- Single button -->
            <div class="btn-group">
                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    More <span class="caret"></span>
                </button>
                
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Mark all as read</a></li>
                    <li class="divider"></li>
                    <li class="text-center"><small class="text-muted">Select messages to see more actions</small></li>
                </ul>
            </div>
           
            <div class="pull-right">
             <asp:Label ID="Search" runat="server" Text="Search" Font-Bold="True" 
                    Font-Names="-webkit-pictograph" ForeColor="#136798"></asp:Label>&nbsp&nbsp<asp:TextBox ID="Searchtxt" onchange="tSpeedValue(this)"
                    runat="server" BorderStyle="Solid" BorderWidth="1px"  
                    BorderColor="#3399FF"  placeholder="Search Message" 
                    Width="300px" 
                    Height="30px" Font-Names="Pristina"></asp:TextBox>
                <span class="text-muted"><b>1</b>–<b>50</b> of <b>277</b></span>
                <div class="btn-group btn-group-sm">
                    <button type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                    </button>
                    <button type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                    </button>
                </div>
            </div>
        </div>
        
    </div>
    <hr />
    <div class="row">
        <div class="col-sm-3 col-md-2">
        <asp:Button ID="Button1" class="btn btn-danger btn-sm btn-block" runat="server" Text="COMPOSE" />
       
            <hr />
            <ul class="nav nav-pills nav-stacked">
                <li class="active"><a href="MIS Reports\MAIL.aspx"><span class="badge pull-right"><asp:Label ID="mailcount"  runat="server" ></asp:Label></span> Inbox </a>
                </li>
                <li><a href="http://www.jquery2dotnet.com">Starred</a></li>
                <li><a href="http://www.jquery2dotnet.com">Important</a></li>
                <li><a href="http://www.jquery2dotnet.com">Sent Mail</a></li>
                <li><a href="http://www.jquery2dotnet.com"><span class="badge pull-right">3</span>Drafts</a></li>
            </ul>
        </div>
        <div class="col-sm-9 col-md-10">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#home" data-toggle="tab"><span class="glyphicon glyphicon-inbox">
                </span>Primary</a></li>
             
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div class="tab-pane fade in active" id="home">
                 
                       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"   EnableViewState="false" GridLines="Horizontal"  HeaderStyle-BackColor="#14B1BB" HeaderStyle-Font-Names=" -webkit-pictograph;"
             Width="100%" EmptyDataText="No results found" EmptyDataRowStyle-Font-Size="Large" AllowPaging="true"
                          CssClass="table table-hover"        BorderStyle="None" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center">
                          <Columns>
                          <%-- <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>--%>
                        <%--   <asp:TemplateField ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox2" runat="server" CssClass="mycheckBig"/>
            </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" Width="60px" />
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Subject" ItemStyle-Height="30px">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server"   ForeColor="Black"><%# Eval("Mail_Subject")%></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="From" ItemStyle-Height="30px">
        <ItemTemplate>
          <asp:LinkButton ID="LinkButton2" runat="server"    ForeColor="Black"><%# Eval("Mail_SF_From")%></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Message" ItemStyle-Height="30px">
        <ItemTemplate>
         <asp:LinkButton ID="LinkButton3" runat="server"    ForeColor="Black"><%# Eval("Mail_Content")%></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>
<%--        <asp:BoundField DataField="Status" ItemStyle-Height="30px" HeaderStyle-Height="30px" >
                              <HeaderStyle Height="30px" />
                              <ItemStyle Height="30px" />
                              </asp:BoundField>--%>
        <%-- <asp:BoundField DataField="MessageId" ItemStyle-Height="30px" HeaderStyle-Height="30px">
       
                              <HeaderStyle Height="30px" />
                              <ItemStyle Height="30px" />
                              </asp:BoundField>--%>
       <asp:TemplateField HeaderText="Date" ItemStyle-Height="30px">
     <ItemTemplate>
       <asp:LinkButton ID="LinkButton4" runat="server"   ForeColor="Black"><%# Eval("Mail_Sent_Time")%></asp:LinkButton>
     </ItemTemplate>
       </asp:TemplateField>
           
           <%-- <asp:BoundField DataField="Mail_Attachement"  ItemStyle-Height="30px" HeaderStyle-Height="30px">
               
                              <HeaderStyle  />
                              <ItemStyle Height="30px" />
                              </asp:BoundField>--%>
               <%--<asp:TemplateField HeaderText="DateTime" ItemStyle-Height="30px">
     <ItemTemplate>
      <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup"  />
     </ItemTemplate>
       </asp:TemplateField>--%>
         <%--<asp:TemplateField ItemStyle-Width="30">
            <ItemTemplate>
                           
                <asp:Image ID="Image1" runat="server"  ImageUrl="../Images/paperclip.png"  />
            </ItemTemplate>
             <ItemStyle Width="30px" />
        </asp:TemplateField>--%>
                          </Columns>
                           <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" HorizontalAlign="Center" />
                         <%--  <HeaderStyle BackColor="#14B1BB" Font-Size="Small"  ForeColor="White" Height="30px" Font-Names="-webkit-pictograph" />--%>
                           
                          </asp:GridView>  
                          


                           <asp:GridView ID="grdDemo"    CssClass="Grid"  GridLines="Horizontal"   runat="server">
                              <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" HorizontalAlign="Center" />

                           <HeaderStyle BackColor="#14B1BB" Font-Size="Small"  ForeColor="White" Height="30px" Font-Names="-webkit-pictograph" />
    </asp:GridView>
                             <asp:modalpopupextender ID="mpe" runat="server" BehaviorID="pnlPopup" PopupControlID="pnlPopup" TargetControlID="Button1" 
     CancelControlID="btnClose"   
        BackgroundCssClass="modalBackground" >
</asp:modalpopupextender>



<%--
<asp:modalpopupextender ID="modal" runat="server" BehaviorID="panelmsg" PopupControlID="panelmsg" TargetControlID="btnShow" 
     CancelControlID="btnClose"   
        BackgroundCssClass="modalBackground" >
</asp:modalpopupextender>--%>

<asp:Panel ID="pnlPopup" runat="server"  CssClass="modalPopup" Style="display: none;width:650px; height:500px; overflow:scroll;   "  >

   <div class="col-md-9">
              <div class="box box-primary">
                <div class="box-header with-border">
                  <h3 class="box-title">Compose Your Mail!!</h3>
                  <div class="box-tools pull-right">
                    <div class="has-feedback">
                         
                      
                    </div>
                  </div><!-- /.box-tools -->
                </div><!-- /.box-header -->
                <div class="box-body no-padding">
                  
                  <div class="table-responsive mailbox-messages">
                   <%-- <table class="table table-hover table-striped">--%>
                         <div class="box-body">
                  <div class="form-group">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Fieldforce" ForeColor="#00C0EF" ControlToValidate="to"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="to" runat="server" class="form-control"  Width="800px"></asp:TextBox>
                  </div>
                  <div class="form-group">
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="subject" ForeColor="#00C0EF" ControlToValidate="subject"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="subject" runat="server" class="form-control" Width="800px"></asp:TextBox>
                  </div>
                  <div class="form-group">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Message" ForeColor="#00C0EF" ControlToValidate="message1"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="message1" runat="server" class="form-control"  
                          TextMode="MultiLine" Width="800px" Height="230px" 
                         ></asp:TextBox>
                    
                  </div>
                  <div>
                    <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <p class="help-block"></p>
                  </div>
                </div>   
                 <asp:Button ID="Button2" runat="server" Text="SEND" class="btn btn-primary" OnClick="send" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%--  </div>--%><asp:Button ID="btnClose" runat="server" Text="CANCEL" class="btn btn-primary"  />
                     
                          
                    <%--</table>--%><!-- /.table -->
                  </div><!-- /.mail-box-messages -->
                </div><!-- /.box-body -->
                
              </div><!-- /. box -->
            </div>
    
        
   
   

 
</asp:Panel>




            <div id="popupdiv" style="display:none;background-color:#FFFFFF; visibility:hidden;"  > 
        
                      
                           <table   style="border: 1px solid #999999; background-color: #33CCCC;width:552px; height: 20px;" ><tr>    
                               <td><asp:Label ID="sub" runat="server" Text="Subject:-" Font-Bold="True" ForeColor="#0073b7" BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC"></asp:Label></td><td><asp:Label ID="subj" runat="server"  style="font-family: Rockwell; align:center; font-size: large;"  ></asp:Label></td><td align="right">
                               <asp:Label ID="Datetime" runat="server"  
                                   style="background-color: rgba(250, 250, 250, 0.57);font-family: -webkit-pictograph;align:center;font-size: small;border-style: solid;border-bottom-width: 1px;border-color: #ddd;"></asp:Label></td></tr></table>
                                                     <%--   </h3>--%>

                          </br>
                           <div class="box-body">
                               <div class="form-group">
                               <table><tr><td>                <asp:Label ID="Label2" runat="server" Text="From:"></asp:Label>&nbsp&nbsp&nbsp</td><td><asp:Label ID="from" runat="server"  style="width:500px;font-family: -webkit-pictograph;"></asp:Label></td><td width="300px">&nbsp</td><td> <i class="fa fa-paperclip" align="right"></i>
                                   <asp:Label ID="att" runat="server" Text="Attachment" align="right"></asp:Label></td><td>  
                                         <asp:ImageButton ID="ImageButton2" runat="server" align="right"
            ImageUrl="~/download (3).png" width="40px" Height ="35px" /> </td></tr></table>
 
                                   
                                          
                                          
                                       
                                     
                               </div>
                              <%-- <div class="form-group">
                  Subject:
                                   <asp:Label ID="subject1" runat="server" class="form-control" style="width:500px"></asp:Label>
                               </div>--%>
                               <div class="form-group">
                                   <asp:Label ID="mg" runat="server" Text="Message" ForeColor="#3399FF" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="message" runat="server" class="form-control" style="display:inline-block;background-color: rgba(255, 255, 255, 0.15);height: 208px;width: 530px;"></asp:Label>
                               </div>
                               <asp:Panel ID="Panel2" runat="server">
                                   
                                      
                                       <p class="help-block">
                                       </p>
                                 
                               </asp:Panel>
                         
                      </div>          
                                  
                   </div>
                   <table id="divInboxList" style="border-top-width: thick; border-top-color: White;
                                                    width: 100.1%; margin: 0px">
                                                    <tr>
                                                        <td height="50%" valign="top" class="gvGrid">
                                                            <asp:GridView ID="gvInbox" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInbox_RowDataBound"
                                                                Width="100%" HeaderStyle-CssClass="Hprint" EmptyDataRowStyle-Font-Bold="true" OnRowCommand="gvInbox_RowCommand"
                                                                AllowPaging="True" PageSize="15" HeaderStyle-BackColor="#ededea" OnSelectedIndexChanged="OnSelectedIndexChanged"
                                                                CssClass="mGrid" HeaderStyle-HorizontalAlign="Left" OnPageIndexChanging="gvInbox_OnPageIndexChanging"
                                                                PagerStyle-CssClass="pgr" EmptyDataText="No Mail(s)..." GridLines="None">
                                                                <HeaderStyle BorderWidth="1" />
                                                                <RowStyle Height="20px" />
                                                                <PagerStyle CssClass="pgr" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First"
                                                                    LastPageText="Last" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="cbSelectAll_OnCheckedChanged"
                                                                                Style="margin: 2px" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkId" runat="server" AutoPostBack="true" OnCheckedChanged="chkId_OnCheckedChanged"
                                                                                Style="margin: 2px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="From_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="From" HeaderStyle-Width="280px" ItemStyle-CssClass="print" SortExpression="From_Name" />
                                                                    <%--<asp:BoundField DataField="Mail_subject" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderText="Subject" HeaderStyle-Width="20%" ItemStyle-CssClass="print" SortExpression="Mail_Subject" />--%>
                                                                    <%-- New changes done by saravanan start  --%>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Subject" HeaderStyle-Width='15%'
                                                                        ItemStyle-CssClass="print" SortExpression="Mail_Subject" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                           <asp:Label ID="lblMail_subject" runat="server" Text='<% #Eval("Mail_Subject") %>'  Visible ="false" />
                                                                           <asp:LinkButton ID = "lnk_MailSub" runat ="server" Text = '<% #Eval("Mail_Subject") %>' 
                                                                           CommandArgument='<%# Eval("Trans_sl_No") %>'  CommandName="ViewMail" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- New changes done by saravanan End  --%>
                                                                    <asp:BoundField DataField="Mail_Sent_Time" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="145px" ItemStyle-CssClass="print"
                                                                        HtmlEncode="false" DataFormatString="{0:d}" SortExpression="Mail_Sent_Time" />
                                                                    <asp:TemplateField HeaderImageUrl="~/Images/Attachment.gif">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Images/Attachment.gif" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblView" Visible="false" runat="server" Text='<% #Eval("Mail_Attachement") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Trans_sl_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width='30%'
                                                                        Visible="false" ItemStyle-CssClass="print" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblslNo" runat="server" Visible="false" Text='<% #Eval("Trans_sl_No") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnslNo" runat="server" Value='<% #Eval("Trans_sl_No") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle Font-Names="Verdana" ForeColor="Black" BackColor="AliceBlue" BorderColor="Black"
                                                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" CssClass="pgr1" />
                                                                <FooterStyle BackColor="#DDEEFF" />
                                                                <EmptyDataRowStyle Font-Size="9pt" ForeColor="Red" Font-Names="Verdana" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                   
                   
                   
                    </div>
                </div>


             
 






              
            </div>
          
            
        </div>
    </div>


</form>
</body>


</asp:Content>
 
