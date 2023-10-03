<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayFixation.aspx.cs"
    Inherits="MasterFiles_HolidayFixation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday Fixation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .divrpt
        {
            margin: 20px;
            font-family: Verdana;
            color: Maroon;
            font-size: 20px;
        }
        
        .rpt
        {
            border: 1px solid #A6A6D2;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            -khtml-border-radius: 8px;
            color: #071019;
            padding: 0px;
        }
        
        .rpttr
        {
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            -khtml-border-radius: 8px;
            background-color: #F0F8FF;
            padding: 0px;
        }
        
        .rptSpan
        {
            color: Black;
        }
        
        .rptmar
        {
            margin: 10px;
            color: Maroon;
            font-family: Verdana;
        }
        
        .rpta
        {
            color: Maroon;
            width: 200px;
            height: auto;
            text-decoration: underline;
        }
        
        .rpta:hover
        {
            color: #b70b6e;
            text-decoration: underline;
        }
        
        .rpttdWidth
        {
            width: 100px;
        }
        
        .rptTr
        {
            border-bottom: dashed 1px maroon;
            background-color: #F5FAEA;
        }
        #lblTitle_LocationDtls
        {
            margin-left: 200px;
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
            background-color: White;
            z-index: 999;
        }
        .padd
        {
            padding-left:10px;
        }
    </style>
    <%--<script type="text/javascript">
        function fnCheck() 
        {
            if ((document.getElementById("txtDate").value).length == 0)
             {
                alert("The textbox should not be empty");
            }
        }
    </script>--%>
     <script type="text/javascript">
         function ValidateCheckBoxList() {

             var listItems = document.getElementById("Chkstate").getElementsByTagName("input");
             var itemcount = listItems.length;
             var iCount = 0;
             var isItemSelected = false;
             for (iCount = 0; iCount < itemcount; iCount++) {
                 if (listItems[iCount].checked) {
                     isItemSelected = true;
                     break;
                 }
             }
             if (!isItemSelected) {
                 alert("Select State.");
             }
             else {
                 return true;
             }
             return false;
         }
    </script>
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
            $("button").click(function () {
                $("#txtDate").clone().appendTo("body");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
  
   
    <script type="text/javascript">

        function checkAll(obj1) {
            var Chkstate = document.getElementById('<%=Chkstate.ClientID %>').getElementsByTagName('input');
            for (var i = 0; i < Chkstate.length; i++) {
                if (Chkstate[i].type.toString().toLowerCase() == "checkbox") {

                    Chkstate[i].checked = obj1.checked;

                }
            }
        }  
      
    </script>
   
    <script type="text/javascript">

        $(function () {

            $("[id*=ChkAll]").bind("click", function () {

                if ($(this).is(":checked")) {

                    $("[id*=Chkstate] input").attr("checked", "checked");

                } else {

                    $("[id*=Chkstate] input").removeAttr("checked");

                }

            });

            $("[id*=Chkstate] input").bind("click", function () {

                if ($("[id*=Chkstate] input:checked").length == $("[id*=Chkstate] input").length) {

                    $("[id*=ChkAll]").attr("checked", "checked");

                } else {

                    $("[id*=ChkAll]").removeAttr("checked");

                }

            });

        });

    </script>
    <script type="text/javascript">

        var table = $('#tblholiday')[0];

        $(table).delegate('.tr_clone_add', 'click', function () {
            alert('hi')
            var thisRow = $(this).closest('tr')[0];
            $(thisRow).clone().insertAfter(thisRow).find('input:text').val('');
        });
    </script>
    <div>
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <br />
    <div align="center">
        <asp:Panel ID="pnl" runat="server">
            <asp:HiddenField ID="hidSlno" runat="server" />
            <table border="1" cellpadding="5" cellspacing="5" id="tblLocationDtls" align="center"
                width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblYear" SkinID="lblMand" runat="server" Text="Select Year "></asp:Label>

                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblTitle_LocationDtls" runat="server" Width="400px" Text="Select the State/Location to fix the Holiday"
                            TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="Small" ForeColor="#8A2EE6" style="margin-left:450px">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="1" style="width: 379px; height: 10px" align="left">
                        <asp:CheckBox ID="ChkAll" runat="server" Text="All" style="margin-left:450px" OnCheckedChanged="ChkAll_CheckedChanged"/>
                        <asp:CheckBoxList ID="Chkstate" runat="server" DataTextField="StateName" DataValueField="State_Code"
                             RepeatDirection="Vertical" RepeatColumns="4" Width="600px"
                            TabIndex="7" Style="font-size: x-small; color: black; font-family: Verdana; margin-left:450px;">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btngo" runat="server" OnClientClick="return ValidateCheckBoxList()" Width="60px" Height="25px" Text="Go >>>" CssClass="BUTTON" OnClick="btngo_onclick" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="margin-right: 30%">
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <%--<asp:Panel ID="pnlholiday" runat="server" Visible="false" >--%>
        <asp:Panel ID="pnlholiday" BorderStyle="Solid" runat="server" Visible="false" CssClass="padd" >
            <table id="tblholiday" border="1px solid"  cellpadding="5" cellspacing="5" style=" border-collapse:collapse; border-width:1;
                padding-right: 30px">
                <tr class="rpttr">
                <td style="background-color: LightBlue; color: Black; border: 1px solid black; text-align: center; width:50px;">
                Sl No
                </td>
                    <td style="background-color: LightBlue; color: Black; border: 1px solid black; text-align: center;"
                        width="170px">
                        Holiday Name
                    </td>
                    <td style="background-color: LightBlue; color: Black; border: 1px solid black; text-align: center">
                        Holiday Date
                    </td>
                    <asp:Repeater ID="rptYearHeader" runat="server" OnItemDataBound="rptYearHeader_OnItemDataBound">
                        <ItemTemplate>
                            <td align="center" style="background-color: LightBlue; color: White; width: 130px;
                                border: 1px solid black">
                              <span style="color:BlueViolet; font-family:Verdana; font-size:12px; width:140px ">       <asp:Literal ID="litYear" Text='<%#Eval("statename") %>' runat="server"></asp:Literal>
                              </span>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <asp:Repeater ID="rptName" runat="server" OnItemDataBound="rptName_ItemDataBound">
                    <ItemTemplate>
                        <tr class="rpttr">
                       
                            <td align="left" style="border: 1px solid black;"><%#Container.ItemIndex+1 %></td>

                       
                            <td align="left" style="border: 1px solid black;">
                                <div style="font-size:12px; font-family:Verdana; width:270px">&nbsp;
                                    <asp:Literal ID="litName" Text='<%#Eval("Holiday_Name") %>' runat="server"></asp:Literal>
                                </div>
                            </td>
                            <td width="50" style="border: 1px solid black;">
                                <asp:HiddenField ID="hdnHolidayID" runat="server" Value='<%#Eval("Holiday_Id") %>' />
                                <asp:TextBox ID="txtDate" Width="90px" runat="server" BackColor="Lightgray" CausesValidation="true"
                                    Text='<%#Eval("Holiday_Date") %>' />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDate">
                                </asp:CalendarExtender>
                                <asp:TextBox ID="txtAdd" runat="server" Width="90px" Visible="false" BackColor="Lightgray"
                                    CausesValidation="true" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="txtAdd">
                                </asp:CalendarExtender>
                                <asp:TextBox ID="txtAdd1" runat="server" Width="90px" BackColor="Lightgray" CausesValidation="true"
                                    Visible="false" />
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy" TargetControlID="txtAdd1">
                                </asp:CalendarExtender>
                                <asp:Label ID="lblMulti" runat="server" Visible="false" Text='<%#Eval("Multiple_Date") %>'></asp:Label>
                            </td>
                            <asp:Repeater ID="rptAmounts" runat="server" OnItemDataBound="rptCal_ItemDataBound">
                                <ItemTemplate>
                                    <td align="center" width="80" style="border: 1px solid black;">
                                      <asp:HiddenField ID="hdnseleted" runat="server" Value='<%#Eval("HolidaySeleted") %>' />
                                      <asp:HiddenField ID="hdnStateName" runat="server" Value='<%#Eval("statename") %>' />
                                      <asp:HiddenField ID="hdnState_code" runat="server" Value='<%#Eval("state_code") %>' />
                                        <asp:CheckBox ID="cbHolidaySelection" runat="server"  CssClass="rptmar" />                                   
                                   <div runat="server" id="TestDiv">
    </div>
                                        <asp:CheckBox ID="chkstate1" runat="server" CssClass="rptmar" Visible="false" />
                                <div runat="server" id="Div1">
    </div>
                                        <asp:CheckBox ID="chkstate2" runat="server" CssClass="rptmar" Visible="false" />
                                       
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <br />
            <center>
                <asp:Button ID="btnSave" runat="server" Width="60px" Height="25px" Text="Save" OnClick="btnSave_OnClick" CssClass="BUTTON" />
                <br />
                <asp:Label ID="lblValues" runat="server" Visible="false"></asp:Label>
            </center>
        </asp:Panel>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
