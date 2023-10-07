<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="QuizTitleCreation.aspx.cs"
    Inherits="MasterFiles_Options_QuizTitleCreation" EnableEventValidation="false" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "//www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="//www.w3.org/1999/xhtml">
<head id="Head1" runat="server">--%>
    <%--<title>Online Quiz - Title Creation</title>--%>
    <style type="text/css">
        .Textbox
        {
            width: 120px;
            height: 20px;
        }
        
        #txttitle
        {
            width: 250px;
            height: 28px;
        }
        
        .Lable
        {
            width: 150px;
            height: 20px;
        }
        
        .dropDown
        {
            height: 24px;
            width: 80px;
        }
        
        #ddlCategory
        {
            height: 24px;
            width: 180px;
        }
        
        #tblTitleCreate td
        {
            padding-top: 5px;
            padding-bottom: 5px;
        }
        
        .file
        {
            color: Blue;
            font-family: Verdana;
            font-weight: 600;
        }
    </style>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <script src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="../../JScript/DateJs/dist/jquery-clockpicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../JScript/DateJs/assets/css/github.min.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">

        //var j = jQuery.noConflict();
        $(document).ready(function () {
            $('#<%=txtEffFrom.ClientID%>').datepicker({
                defaultDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeYear: true,
                changeMonth: true
            });

            $("#txtEffFrom").datepicker("setDate", new Date());

            var currentMonth = (new Date).getMonth() + 1;

            $("#ddlMonth option[value='" + currentMonth + "']").prop('selected', true);

            // var currentYear = (new Date).getFullYear();

            //            $("#ddlMonth option[value='" + currentMonth + "']").prop('selected', true);
            // $("#ddlYear option[value='" + currentYear + "']").prop('selected', true);

            //var noofyears = 2; // Change to whatever you want
            //var thisYear = (new Date()).getFullYear();
            //for (var i = 0; i <= noofyears; i++)
            //{
            //    var year = thisYear - i;
            //    $('<option>', { value: year, text: year }).appendTo("#ddlYear");
            //}

            var currentYear = (new Date).getFullYear();
            var PreviousYear = currentYear - 1;
            $('#<%=ddlYear.ClientID%>').append($("<option></option>").val(PreviousYear).html(PreviousYear));
            $('#<%=ddlYear.ClientID%>').append($("<option></option>").val(currentYear).html(currentYear));
            //$('#<%=ddlYear.ClientID%> option[value='" + currentYear + "']').prop('selected', true);

            $('#<%=hidYear.ClientID%>').val($('#<%=ddlYear.ClientID%>').val());

            //$('#<%=hidYear.ClientID%>').val();

            $("#txttitle").val("");

            $('#<%=ddlYear.ClientID%>').change(function () {
                $('#<%=hidYear.ClientID%>').val($('#<%=ddlYear.ClientID%>').val());
                console.log($('#<%=ddlYear.ClientID%>').val());
            });

        });
    
    </script>
   
    <%--<link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />--%>
<%--    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        function Validation() {

            //alert($("#ddlCategory").val());

            if ($('#<%=txttitle.ClientID%>').val() == "") {

                alert("Please Enter Quiz Title");
                return false;
            }
            if ($('#<%=ddlCategory.ClientID%>').val() == 0) {
                alert("Please Select Category");
                return false;
            }
            if ($('#<%=txtEffFrom.ClientID%>').val() == "") {
                alert("Please Enter Effective Date");
                return false;
            }
            if ($('#<%=ddlYear.ClientID%>').val() == "") {
                alert("Please Select Month");
                return false;
            }
            if ($('#<%=ddlYear.ClientID%>').val() == "") {
                alert("Please Select Year");
                return false;
            }
            else {
                return true;
            }

        }

    </script>
    <script type="text/javascript">

        function preventMultipleSubmissions() {
            $('#btnSurvey').prop('disabled', true);
        }
        window.onbeforeunload = preventMultipleSubmissions;
    </script>
    <link href="../Quiz_ProcessCSS.css" rel="stylesheet" type="text/css" />
<%--</head>
<body>--%>
    <form id="form1" runat="server">
    <%--<ucl:Menu ID="menu1" runat="server" />--%>
    <br />
    <div>
        <asp:HiddenField ID="hidSurveyID" runat="server" />

        <asp:HiddenField ID="hidYear" runat="server" />

        <center>
            <div style="width: 70%">
                <table id="tblTitleCreate" width="50%" style="margin-left: 20%">
                    <tr>
                        <td>
                            <asp:Label ID="lbltitle" runat="server" CssClass="Lable" Text="Quiz Title"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttitle" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcategory" runat="server" CssClass="Lable" Text="Quiz Category"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" CssClass="dropDown" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Effective Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEffFrom" runat="server" CssClass="Textbox" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropDown">
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropDown">
                              <%--  <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpload1" runat="server" CssClass="file" /><br />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSurvey" runat="server" Text="Save" OnClick="btnadd_Click" CssClass="BUTTON"
                OnClientClick="if (!Validation()) return false;" Width="70px" Height="24px" />
        </center>

    </div>

    </form>
<%--</body>
</html>--%>
</asp:Content>