<%@ Page Title="Notification Message" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Notification_Message.aspx.cs" Inherits="MasterFiles_Notification_Message" %>
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Notification Message</title>
<script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
         <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
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
            background-color: White;
            z-index: 999;
        }
         td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
         .BUTTON
         {
             background-color:#0275d8;
             margin-top: 20px;
             Width:70px;
                 Height:25px;
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
        $(document).ready(function () {
            $('#<%=txtstartdate.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $('#<%=txtEndDate.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy'
            });
            if (($('#<%=hdnslno.ClientID%>').val()).length>0) {
                filldata();               
            }
            else {
            }
            divcode = '<%=Session["Division_Code"]%>';

         
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=txtmsg.ClientID%>').val() == "") {
                    alert("Enter Notification Message.");
                    $('#<%=txtmsg.ClientID%>').focus(); return false;
                }
                               
            });
            function filldata() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                     async: false,
                    url: "Notification_Message.aspx/fillData",                 
                    dataType: "json",
                    success: function (data) {
                        Notify = JSON.parse(data.d) || [];
                        fillfield();
                    },
                    error: function (result) {
                        console.log(result);
                    }
                });
            }
            function fillfield() {
                $('#<%=txtmsg.ClientID%>').val(Notify[0].Notification_Message);
                $('#<%=txtEndDate.ClientID%>').val(Notify[0].Notification_To_Date);
                $('#<%=txtstartdate.ClientID%>').val(Notify[0].Notification_From_Date);
                var notisf = [];
                var nottisfname = [];
                notisf = (Notify[0].SF_Code).split(',');
                nottisfname=(Notify[0].sf_)
                var grid = $('#<%=gvFF.ClientID%>');
                var row = $(grid).closest("tr").find("[id*=chkSf]");
                $(row, grid).each(function () {         
                    var sf = $(this).closest('tr').find("[id*=hdnsf]").val();
                    for ($i = 0; $i < notisf.length; $i++) {
                        if (sf == notisf[$i]) {
                            $(this).attr("checked", "checked");
                        }
                    }
                });            
              
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <%--<ucl:Menu ID="menu1" runat="server" />--%>
         <br />
         <br />
         <center >
       
     
         <ContentTemplate>  
            <table align="center">
            <tr align="center">
              <td align="right">
                 <asp:Label ID="lblSearch" CssClass="Hprint" runat="server" text="Search By" ForeColor="Navy" Font-Bold="true" ></asp:Label>
              </td>

              <td align="left">
                 <asp:RadioButtonList ID="rdoadr" runat="server" CssClass="Hprint" Height="25px" RepeatDirection="Horizontal" AutoPostBack="true"
                  OnSelectedIndexChanged="rdoadr_SelectedIndexChanged">
                    <asp:ListItem Value ="0" Text="Designation" Selected ="True" ></asp:ListItem> 
                    <asp:ListItem Value ="1" Text="Field Force" ></asp:ListItem>
                  </asp:RadioButtonList>
              </td>
            </tr>
         </table>

         <table align ="right" >
           <tr align="center"> 
             <td align ="center">
                <span runat="server" id="levelset" style="border-style:none; font-family:Verdana; font-size:14px; border-color:#E0E0E0; color :#8A2EE6"><asp:LinkButton ID="lnk" ForeColor="#e0f3ff" runat="server" Text="." onclick="lnk_Click" Font-Underline="false"></asp:LinkButton></span>
             </td>
           </tr>
          </table>
        

         <table border="0" class="Hprint">
           <tr>
           <td valign="top" align="left">
              <asp:Label ID="Label3" runat="server" Text="Designation" ForeColor="Navy" Font-Bold="true"></asp:Label>

               <asp:CheckBox ID="chkLevelAll" runat="server" Style="margin-left: 20px" Text="All"
                                AutoPostBack="true" OnCheckedChanged="chkLevelAll_CheckedChanged" />
           </td>
            
           <%--<td>
              <asp:CheckBox ID="chkMR" AutoPostBack="true" Visible="false" Text="MR" OnCheckedChanged="chkMR_OnCheckChanged"
                 runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal">
                 </asp:CheckBox>
           </td>--%>

           <td align="left">
                            <asp:CheckBoxList ID="chkDesgn" AutoPostBack="true" OnSelectedIndexChanged="chkDesgn_OnSelectedIndexChanged"
                                runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
           </tr>
         
         </table>
  <asp:HiddenField ID="hdnslno" runat="server" /> 
          <table width="50%" class="Hprint" align="center">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblFF" Visible="false" runat="server" Text="Field Force" CssClass="Hprint"
                                ForeColor="Navy" Font-Bold="true"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="ddlFFType" Visible="false" runat="server" Style="margin-left: 30px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Width="15%">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <%--<asp:ListItem Value="1" Text="Alphabetical"></asp:ListItem>--%>
                                <asp:ListItem Value="2" Text="Team" Selected="True"></asp:ListItem>
                               <%-- <asp:ListItem Value="3" Text="Division"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" Width="6%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFieldForce" Visible="false" runat="server" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                AutoPostBack="true" Width="40%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <%--<td align="right">
                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
                </td>--%>
                    </tr>
                    <tr style="height: 10px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSelectedCount" runat="server" Font-Bold="true" ForeColor="DarkRed" Text="SelectedValue"></asp:Label>
                        </td>
                    </tr>
                </table>
           <table width="50%" align="center" style="width: 50%; height: 350px; margin-top: 5px" cellpadding="0"
                    cellspacing="0" class="Hprint">
                    <tr>
                       <td valign="middle" style="vertical-align: middle;" align="left" class="stylespc">
                        <asp:Label ID="lblffname" runat="server" Text="Select The Fieldforce Name :" Font-Bold="true" ForeColor="MediumVioletRed"></asp:Label>
                    </td>
                    </tr>
                    <tr align="center">
                        <td >
                             <div class="card">
                            <div class="card-body table-responsive">
                                
                                <asp:CheckBoxList ID="chkFF" runat="server">
                                </asp:CheckBoxList>
                                <asp:GridView ID="gvFF" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                    GridLines="None" HeaderStyle-BackColor="#ededea" HeaderStyle-CssClass="Hprint" 
                                    HeaderStyle-HorizontalAlign="Left" Width="100%" CssClass="table  table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText = "&nbsp&nbsp✔">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSf" runat="server" AutoPostBack="true" OnCheckedChanged="gvFF_OnCheckedChanged"
                                                    Style="margin: 2px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="300px" HeaderText="FieldForce Name">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnsf" runat="server" Value='<% #Eval("sf_Code") %>'></asp:HiddenField>                                         
                                                <asp:Label ID="lblSf_Name" runat="server" Text='<% #Eval("sf_name") %>' Width="230px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Designation" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation_Short_Name" runat="server" Text='<% #Eval("Designation_Short_Name") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_mail" runat="server" Text='<% #Eval("sf_mail") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="HQ" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_HQ" runat="server" Text='<% #Eval("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBackcolor" runat="server" Text='<% #Eval("des_color") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_color" runat="server" Text='<% #Eval("sf_color") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation_Code" runat="server" Text='<% #Eval("Designation_Code") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_Code" Visible="false" runat="server" Text='<% #Eval("sf_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_Type" runat="server" Text='<% #Eval("sf_Type") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Red" />
                                </asp:GridView>
                            </>
                            <%--<asp:ListBox ID="chkFF" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>
</div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                      <td height="20px"></td>
                    </tr>
               <tr>
                   <td>
                      <span >  <asp:Label ID="lblstartdate" runat="server" Text="Notification From Date:"></asp:Label></span>
                   <asp:TextBox ID="txtstartdate" runat="server" DataFormatString="{dd/MM/yyyy}"></asp:TextBox>
                       </td>
               </tr>
                <tr>
                   <td>
                       <span> <asp:Label ID="lblEndDate" runat="server" Text=" Notification To  Date  : "></asp:Label></span>
                   <asp:TextBox ID="txtEndDate" runat="server" DataFormatString="{dd/MM/yyyy}"></asp:TextBox>
                       </td>
               </tr>
               
                     <tr>
                       <td valign="middle" style="vertical-align: middle;" align="left" class="stylespc">
                        <asp:Label ID="lblnotif" runat="server" Text="Notification Message :" Font-Bold="true" ForeColor="MediumVioletRed"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                         <td valign="top" align="left" class="stylespc">
                        <asp:TextBox ID="txtmsg" runat="server" TextMode="MultiLine" Height="100px" Width="100.5%"></asp:TextBox>
                    </td>
                    </tr>
                </table>
             </ContentTemplate> 
         
                <br />

                <asp:Button ID="btnSubmit" runat="server"  Text="Submit"   CssClass="BUTTON" OnClick="btnSubmit_Click" />
                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BUTTON" PostBackUrl="Notification_List.aspx" />
               </center>
                <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>