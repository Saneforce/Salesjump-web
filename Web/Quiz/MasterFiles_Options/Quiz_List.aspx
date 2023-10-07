<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Quiz_List.aspx.cs" Inherits="MasterFiles_Options_Quiz_List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quiz List</title>--%>
    <script language="javascript" type="text/javascript">
        function ViewQuestionPage(surveyId) {
            window.location.href = "AddQuizQuestions.aspx?surveyId=" + surveyId + "";
        }

    </script>
    <%-- <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />--%>
    <%--<link href="../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />--%>
    <%--  <link href="../../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../JScript/BootStrap/dist/css/GridTable.css" rel="stylesheet" type="text/css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style type="text/css">
        table{
            border:0 !important;
        }
        td,tr,th,tbody{
            border:none;
        }
        .NewBtn
        {
            background: #25A6E1;
            background: -moz-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#25A6E1),color-stop(100%,#188BC0));
            background: -webkit-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -o-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -ms-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#25A6E1',endColorstr='#188BC0',GradientType=0);
            padding: 5px 10px;
            color: #fff;
            font-family: 'Helvetica Neue' ,sans-serif;
            font-size: 15px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border: 1px solid #1A87B9;
            cursor: pointer;
        }
        .NewBtn:hover
        {
            color: Black;
        }
        .panelbtn
        {
            float: right;
            margin-right: 70px;
        }
    </style>
    <style type="text/css">
        .paging-nav
        {
            text-align: right;
            padding-top: 2px;
        }
        
        .paging-nav a
        {
            margin: auto 1px;
            text-decoration: none;
            display: inline-block;
            padding: 1px 7px;
            background: #91b9e6;
            color: white;
            border-radius: 3px;
        }
        
        .paging-nav .selected-page
        {
            background: #187ed5;
            font-weight: bold;
        }
        
        .paging-nav
        {
            width: 400px;
            font-family: Arial, sans-serif;
            margin-left: 50%;
        }
    </style>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray !important;
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
    </style>
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
<%--</head>
<body>--%>
    <form id="form1" runat="server">   
      <div class="row">
        <div class="col-lg-12 sub-header">
            Quiz List 
        <asp:Panel ID="pnl" runat="server" CssClass="panelbtn">
            <asp:Button ID="btnADD" runat="server" Text="Quiz Title Creation" CssClass="NewBtn" style="float:right;"
                OnClick="btnADD_Click" />
        </asp:Panel>
        </div>
    </div>
    <%--<ucl:Menu ID="menu1" runat="server" />--%>
    <br />
    <div>
        <br />
        <br />
        <br />
        <div class="card">
        <div class="card-body table-responsive">
            <asp:GridView ID="grdSurvey" runat="server" AutoGenerateColumns="False" 
                Font-Bold="False" Font-Size="Smaller" ForeColor="Black"
                CssClass="table table-hover" Width="100%" AllowSorting="true"
                CellPadding="2" CellSpacing="0" OnRowCommand="grdSurvey_RowCommand" OnRowDeleting="grdSurvey_RowDeleting"
                >
               <%-- OnPreRender="GridView1_PreRender"--%>
              <%--  <FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="false"
                    Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" />--%>
                <%--<RowStyle BackColor="#EFF3FB" Font-Names="Verdana" Font-Size="13px" ForeColor="#400000" />--%>
                <%--<EditRowStyle HorizontalAlign="Left" CssClass="gvedit" />--%>
                <%--<SelectedRowStyle BackColor="#ADAEB4" ForeColor="GhostWhite" />--%>
                <%--<AlternatingRowStyle BackColor="White" />--%>
                <Columns>
                    <asp:BoundField DataField="Survey_Id" HeaderText="Code" Visible="false">
                        <ControlStyle BackColor="#FFF" />
                        <ItemStyle  HorizontalAlign="Left"
                            VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Quiz Title">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" />
                        <ItemStyle  Font-Bold="False" />
                        <ItemTemplate>
                            <%-- <a href="#" onclick="ViewQuestionPage('<%# Eval("Survey_Id") %>')">
                                <%# Eval("Quiz_Title")%></a>--%>
                            <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Quiz_Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Created_Date" HeaderText="Created On">
                        <ItemStyle   HorizontalAlign="Left" />
                    </asp:BoundField>
                       <asp:BoundField DataField="From_date" HeaderText="Process From Date">
                        <ItemStyle   HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="To_Date" HeaderText="Process To Date">
                        <ItemStyle   HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NQues" HeaderText="No Of Questions">
                        <ItemStyle   HorizontalAlign="Left" />
                    </asp:BoundField>
                       <asp:TemplateField HeaderText="Uploaded File">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" />
                        <ItemStyle   Font-Bold="False" />
                        <ItemTemplate>
                          
                            <asp:Label ID="lblUpl" runat="server" Text='<%#Eval("FilePath") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Create Questions">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <a href="#" onclick="javascript:window.location.href='Quiz_Questions.aspx?Survey_Id=<%# Eval("Survey_Id") %>';">
                                Create Q/A</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upload Questions">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <a href="#" onclick="javascript:window.location.href='Quiz_Question_Upload.aspx?Survey_Id=<%# Eval("Survey_Id") %>';">
                                Upload Q/A</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preview Q/A">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <a href="#" onclick="javascript:window.location.href='AddQuizQuestions.aspx?surveyId=<%# Eval("Survey_Id") %>';">
                                Preview</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>                           
                            <a href="Quiz_Process.aspx?Survey_Id=<%# Eval("Survey_Id") %>&Month=<%#Eval("Month") %>&Year=<%#Eval("Year") %>"
                                onclick="ShowProgress();">Process</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Processed">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="Green" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <asp:Label ID="lblProcessed" runat="server" Text='<%#Eval("Processed") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <a href="#" onclick="javascript:window.location.href='Edit_Quiz_Questions.aspx?surveyId=<%# Eval("Survey_Id") %>';">
                                Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Deactivate">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle   Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Survey_Id") %>'
                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Quiz Title');">Deactivate
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
       </div>
    </div>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../../Images/loader.gif" alt="" />
    </div>
    </form>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/jquery-ui.min.js"></script>
    <%--<script src="../../JScript/js/paging.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#grdSurvey').paging(
            {
                limit: 5,
                rowDisplayStyle: 'block',
                activePage: 0,
                rows: []

            });
        });
    </script>--%>
<%--</body>
</html>--%>
</asp:Content>