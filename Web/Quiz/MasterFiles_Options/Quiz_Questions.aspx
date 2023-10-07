<%@ Page Language="C#" MasterPageFile="~/Master.master"  AutoEventWireup="true" CodeFile="Quiz_Questions.aspx.cs" Inherits="MasterFiles_Options_Quiz_Questions" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Question Entry</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function QuestionTypeChange(dropdown) {
            var myindex = dropdown.selectedIndex
            var SelValue = dropdown.options[myindex].value
            if (SelValue == 3) {
                document.getElementById("trAnswer").style.display = 'none';
                document.getElementById("trCorrectAns").style.display = 'none';
                document.getElementById("trNoOfOption").style.display = 'none';
            }
            else {
                document.getElementById("trAnswer").style.display = '';
                document.getElementById("trCorrectAns").style.display = '';
                document.getElementById("trNoOfOption").style.display = '';
            }
        }

    </script>
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
    </style>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
            font-size: 9pt;
        }

        .label {
            display: inline-block;
            font-size: 9pt;
            color: black;
            font-family: Verdana;
        }

        .dropDown {
            height: 27px;
            width: 182px;
            font-size: 8pt;
            color: #000000;
            padding: 1px 3px 0.2em;
            height: 25px;
            border-top-style: groove;
            font-family: Verdana;
            border-right-style: groove;
            border-left-style: groove;
            border-bottom-style: groove;
        }

        .Textbox {
            font-size: 8pt;
            color: black;
            border-top-style: groove;
            border-right-style: groove;
            border-left-style: groove;
            height: 22px;
            padding-left: 4px;
            background-color: white;
            border-bottom-style: groove;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />

    <link href="../../JScript/css/RadioBtnCSS.css" rel="stylesheet" type="text/css" />
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
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/Quiz_JS/Quiz_QusCreate_JS.js" type="text/javascript"></script>
    <script type="text/javascript">

        function preventMultipleSubmissions() {

            $('#<%=btnAddQuestion.ClientID %>').prop('disabled', true);

        }

        window.onbeforeunload = preventMultipleSubmissions;

    </script>
    <script type="text/javascript">


        function check() {

            $('#tbl_outside table.tbl_Inside input:radio:checked').each(function () {

                var Option = $(this).parent().siblings().find('textarea').val();

                if (Option == "") {

                    alert("Please Enter Answer Option");
                    return false;
                }

                else {
                    $("#<%=txtAns.ClientID%>").val(Option);
                    return true;
                }

            });

        }

        function Validation() {

            //alert($("#ddlCategory").val());
            if ($("#<%=ddlQuestionType.ClientID%>").val() == 0) {

                alert("Please Select Question Type");
                return false;

            }
            if ($("#<%=txtQuestionText.ClientID%>").val() == "") {

                alert("Please Enter Question Text");
                return false;
            }

            if ($("#<%=txtNoOfOption.ClientID%>").val() == "") {

                alert("Please Enter No Of Option");
                return false;
            }
            if ($("#<%=txtAns.ClientID%>").val() == "") {

                alert("Please Enter Answer Choices and Select One Option");
                return false;
            }
            else {
                return true;
            }

        }

        function Clear() {

            $("#<%=txtQuestionText.ClientID%>").val("");

            var NOf = $("#<%=txtNoOfOption.ClientID%>").val("4");

            $('#OptionDiv').html("");

            for (i = 0; i < 4; i++) {
                $('#OptionDiv').append('<div id="OptDiv" style="width:400px;height:45px;float:left;margin-left:3%;margin-top:4%;"><div style="width:10%;height:43px;float:left;margin-top:4%"><input type="radio" id="RbtnOption_' + i + '" name="radio_name" class="radiogroup" value="' + i + '"  onclick=check(); /></div><div style="width:30%;height:43px;float:left"><textarea name="textarea"  id="txtOption_' + i + '" style="width:340px;height:43px;"></textarea></div></div>');
            }

            $("#<%=txtAns.ClientID%>").val("");

        }

        $(document).ready(function () {


            var NOf = $("#<%=txtNoOfOption.ClientID%>").val("4");

            for (i = 0; i < 4; i++) {
                $('#OptionDiv').append('<div id="OptDiv" style="width:400px;height:45px;float:left;margin-left:3%;margin-top:4%;"><div style="width:10%;height:43px;float:left;margin-top:4%"><input type="radio" id="RbtnOption_' + i + '" name="radio_name" class="radiogroup" value="' + i + '"  onclick=check(); /></div><div style="width:30%;height:43px;float:left"><textarea name="textarea"  id="txtOption_' + i + '" style="width:340px;height:43px;"></textarea></div></div>');
            }


            $("#<%=txtNoOfOption.ClientID%>").keyup(function () {

                $('#OptionDiv').html("");
                var N_Option = $("#<%=txtNoOfOption.ClientID%>").val();
                //alert(N_Option);

                for (i = 0; i < N_Option; i++) {
                    $('#OptionDiv').append('<div id="OptDiv" style="width:400px;height:45px;float:left;margin-left:3%;margin-top:4%;"><div style="width:10%;height:43px;float:left;margin-top:4%"><input type="radio" id="RbtnOption_' + i + '" name="radio_name" class="radiogroup" value="' + i + '"  onclick=check(); /></div><div style="width:30%;height:43px;float:left"><textarea name="textarea"  id="txtOption_' + i + '" style="width:340px;height:43px;"></textarea></div></div>');
                }

            });



            $("#<%=btnAddQuestion.ClientID%>").click(function (e) {

                if (Validation() === true) {
                    var AnsOpt = "";

                    $('#tbl_outside table.tbl_Inside input[type=radio]').each(function (i) {

                        AnsOpt += $(this).parent().siblings(i).find('textarea').val() + ",";

                    });

                    var AnswerOpt = AnsOpt.substr(0, AnsOpt.length - 1);

                    var QuestionType = $("#<%=ddlQuestionType.ClientID%>").val();
                    var QuestionText = $("#<%=txtQuestionText.ClientID%>").val();
                    var CorrectAns = $("#<%=txtAns.ClientID%>").val();

                    var Qus_Detail = {};

                    Qus_Detail.QuestionTypeId = QuestionType;
                    Qus_Detail.QuestionText = QuestionText;
                    Qus_Detail.InputOption = AnswerOpt;
                    Qus_Detail.CorrectAnswer = CorrectAns;

                    $.ajax({

                        type: "POST",
                        url: "Quiz_Questions.aspx/AddQuestionData",
                        async: false,
                        data: '{QueData:' + JSON.stringify(Qus_Detail) + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var txt = "Question has been added successfully";
                            alert(txt);

                            Clear();

                            window.location = 'Quiz_List.aspx';
                        },
                        error: function (result) {

                            var txt = "Error";
                            alert(txt);
                        }

                    });

                }

                e.preventDefault();
            });


        });

    </script>
<%--</head>
<body>--%>
    <form id="form1" runat="server">
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <br />
        <div>
            <asp:HiddenField ID="hidSurveyId" runat="server" />
            <div>
                <center>
                    <table align="center" id="tbl_outside">
                        <tr>
                            <td class="stylespc">
                                <table style="height: 38px" id="tblQuestion" class="tbl_Inside">
                                    <tr>

                                        <td class="stylespc">
                                            <asp:Label ID="lblQuestion_Type" runat="server" CssClass="label" Text="Question Type"></asp:Label>
                                        </td>
                                        <td class="stylespc">

                                            <asp:DropDownList ID="ddlQuestionType" CssClass="dropDown" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="stylespc" style="vertical-align: middle">
                                            <asp:Label ID="lblQuestionText" runat="server" CssClass="label" Text="Question Text"></asp:Label>
                                        </td>
                                        <td class="stylespc">
                                            <asp:TextBox ID="txtQuestionText" runat="server" CssClass="Textbox" Width="500px"
                                                Height="100px" TextMode="MultiLine">                                        
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trNoOfOption">
                                        <td class="stylespc">
                                            <asp:Label ID="lblNoofOption" runat="server" CssClass="label" Text="No Of Option"></asp:Label>
                                        </td>
                                        <td class="stylespc">
                                            <asp:TextBox ID="txtNoOfOption" runat="server" CssClass="Textbox" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trAnswer">
                                        <td class="stylespc" style="vertical-align: middle">
                                            <asp:Label ID="lblAnswer" runat="server" CssClass="label" Text="Answer Choices "></asp:Label>
                                        </td>
                                        <td class="stylespc">
                                            <asp:TextBox ID="txtInputOptions" runat="server" CssClass="Textbox" Width="500px"
                                                Height="30px" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            <div id="OptionDiv" style="width: 500px; height: 300px; border: 1px solid LightGray; background-color: White">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="trCorrectAns">
                                        <td class="stylespc">
                                            <asp:Label ID="lblCrctAns" runat="server" CssClass="label" Text=" Correct Answer"></asp:Label>
                                        </td>
                                        <td class="stylespc">
                                            <asp:TextBox ID="txtAns" runat="server" CssClass="Textbox" ReadOnly="true" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>
                <br />
                <center>
                    <asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" CssClass="BUTTON"
                        Style="height: 26px; width: 119px" />
                </center>
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../../Images/loader.gif" alt="" />
                </div>
            </div>
        </div>
    </form>
<%--</body>
</html>--%>
 </asp:Content>