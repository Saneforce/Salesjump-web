<%@ Page Language="C#"  MasterPageFile="~/Master.master"  AutoEventWireup="true" CodeFile="Quiz_Question_Upload.aspx.cs"
    Inherits="MasterFiles_Options_Quiz_Question_Upload" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quiz Question Upload</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        #divdr
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 130px;
        }
        #detail
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 130px;
            padding: 2px;
        }
        #divcat
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 90px;
        }
        #detailcat
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 90px;
            padding: 2px;
        }
        #divTerr
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }
        #detailTerr
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
    </style>
    <style type="text/css">
        *
        {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }
        input[type=file], input[type=file] + input
        {
            display: inline-block;
            background-color: #eee;
            border: 1px solid gray;
            font-size: 15px;
            padding: 4px;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }
        input[type=file] + input
        {
            padding: 12px;
            background-color: #00b7cd;
            color: White;
        }
        ::-webkit-file-upload-button
        {
            -webkit-appearance: none;
            background-color: #00b7cd;
            border: 1px solid gray;
            font-size: 15px;
            padding: 8px;
            color: White;
        }
        ::-ms-browse
        {
            background-color: #00b7cd;
            border: 1px solid gray;
            font-size: 15px;
            padding: 8px;
        }
        input[type=file]::-ms-value
        {
            border: none;
        }
        
        .panel
        {
            border: 1px solid lightgray;
            height: 200px;
            margin-left: 33%;
            width: 40%;
            background-color: White;
        }
        
        .DownLable
        {
            font-family: @Dotum;
            font-size: 16px;
            color: #e845c4;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        #blinkeffect {
            -webkit-animation-name: blink;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;

            -moz-animation-name: blink;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;

            animation-name: blink;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            font-size:16px;
        }

        @-moz-keyframes blink {  
            0% { opacity: 1.0; color: blue; }
            50% { opacity: 0.0; }
            100% { opacity: 1.0; color: red; }
        }

        @-webkit-keyframes blink {  
            0% { opacity: 1.0; color: blue;}
            50% { opacity: 0.0; }
            100% { opacity: 1.0; color: red; }
        }

        @keyframes blink {  
            0% { opacity: 1.0; color: blue; }
            50% { opacity: 0.0; }
            100% { opacity: 1.0; color: red; }
        }
</style>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <%--  <script type="text/javascript">
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
    </script>--%>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function blink() {
            $('#blinkeffect').fadeOut(500).fadeIn(500);
        }
        setInterval(blink, 1000);
    </script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateFile() {

            if ($("#FlUploadcsv").val() == "") {
                createCustomAlert("Please Browse the File");
                return false;
            }
            else {
                return true;
            }

        }

    </script>

    <style type="text/css">
        a.imagelink
        {
            background: url(../../JScript/ButtonIcon/ExcelIcon.png) no-repeat left top;
            padding-left: 25px;
        }
    </style>

<%--</head>
<body>--%>
    <form id="form1" runat="server">
    <%--<ucl:Menu ID="menu1" runat="server" />--%>
    <br />
    <asp:Panel ID="pnlQuiz_Question" runat="server" CssClass="panel">
        <div style="font-family: @DotumChe; font-size: 16px; width: 150px; margin-left: 35%;
            margin-top: 4%;">
            <asp:Label ID="lblExcel" runat="server" CssClass="DownLable">Excel File</asp:Label>
        </div>
        <div style="margin-top: 4%; margin-left: 15%">
            <asp:FileUpload ID="FlUploadcsv" runat="server" />
            <asp:Button ID="Upload" runat="server" Text="Upload" OnClick="btnUpload_Click" OnClientClick="if(!ValidateFile()) return false;" />
            <div style="margin-left: 25%; margin-top: 6%; width: 200px; height: 50px;">
                <%--  <asp:Button ID="btnDownLoad" runat="server" Text="DownLoad" 
                    onclick="btnDownLoad_Click" />--%>
                <div style="background-color: White;">
                    <div style="float: left; width: 100%">
                       <%-- <asp:ImageButton ID="Img" runat="server" Width="50px" Height="50px" ImageUrl="../../JScript/ButtonIcon/ExcelIcon.png"
                            OnClick="Img_Click" />--%>
                       <asp:LinkButton ID="Img" runat="server" Text="Download File"  OnClick="Img_Click"></asp:LinkButton>
                    </div>
                    <%--<div style="float: left; margin-top: 15px">
                        <h2 id="blinkeffect" align="center">
                            Download File
                        </h2>
                    </div>--%>
                </div>
            </div>
        </div>
      
    </asp:Panel>
   <%-- <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../../Images/loader.gif" alt="" />
    </div>--%>
    </form>
<%--</body>
</html>--%>
</asp:Content>