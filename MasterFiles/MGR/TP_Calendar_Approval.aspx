<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Calendar_Approval.aspx.cs"
    Inherits="MasterFiles_MGR_TP_Calendar_Approval" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tour Plan</title>
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        .modalBackground
        {
            /* background-color: #999999;*/
            filter: alpha(opacity=80);
            opacity: 0.5;
            z-index: 10000;
            display: block;
            cursor: default;
            color: #000000;
            pointer-events: none;
        }
        #menu1
        {
            display: none;
        }
        
        .TextFont
        {
            text-align: center;
            margin-top: 6px;
        }
    </style>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('PopupControlExtender3');
            popup.hidePopup();
        }
    </script>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('PopupControlExtender4');
            popup.hidePopup();
        }
    </script>
    <script type="text/javascript">

        var popUpObj;
        function showModalPopUp(dateValue) {

        }

        function hidepanel(txtcntrl, pnlcntrl) {


            var pnl = document.getElementById(pnlcntrl);
            var txtMGR = document.getElementById(txtcntrl);

            if (txtMGR.value != '') {
                pnl.style.display = 'none';
                return false;
            }
        }

        function LoadFieldForce() {
            var chkBox = document.getElementById("<%=chkFieldForce.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtFieldForce.ClientID%>");
            var counter = 0;
            objTextBox.value = "";
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        objTextBox.value = chkBoxText[0].innerHTML;
                    }
                    else {
                        objTextBox.value = objTextBox.value + ", " + chkBoxText[0].innerHTML;
                    }
                }
            }

        }

        function LoadTerritory() {
            var chkBox = document.getElementById("<%=chkTerritory.ClientID%>");
            var checkbox = chkBox.getElementsByTagName("input");
            var objTextBox = document.getElementById("<%=txtTerritory.ClientID%>");
            //var objTextBoxcode = document.getElementById("<%=txtTerr.ClientID%>");
            var counter = 0;
            objTextBox.value = "";
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    var chkBoxText = checkbox[i].parentNode.getElementsByTagName('label');
                    if (objTextBox.value == "") {
                        objTextBox.value = chkBoxText[0].innerHTML;
                        //objTextBoxcode.value = chkBoxText[0].value;
                    }
                    else {
                        objTextBox.value = objTextBox.value + ", " + chkBoxText[0].innerHTML;
                        //objTextBoxcode.value = objTextBoxcode.value + ", " + chkBoxText[0].value;
                    }
                }
            }

        }

     


    </script>
    <script type="text/javascript">
        function ValidateEmptyValue() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approved TP Save Successfully ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
    <script type="text/javascript">
        function validate() {
            var txtReason = document.getElementById('<%=txtReason.ClientID %>').value;
            if (txtReason == "") {
                alert("Please Enter the Reason");
                document.getElementById('<%=txtReason.ClientID %>').focus();
                return false;
            }

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Rejecte TP ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        function validation() {
            var result = document.getElementById("<%=ddlWorkType.ClientID%>");

            var ddlText = result.options[result.selectedIndex].text;
            var Value = result.options[result.selectedIndex].value;

            if (Value == "0") {
                alert("Select WorkType ");
                result.focus();
                return false;
            }
            if (ddlText == "Field Work") {
                if (document.getElementById("<%=txtFieldForce.ClientID%>").value == "") {
                    alert("Select Joint Work ");
                    document.getElementById("<%=txtFieldForce.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtTerritory.ClientID%>").value == "") {
                    alert("Select Territorty ");
                    document.getElementById("<%=txtTerritory.ClientID%>").focus();
                    return false;
                }
            }

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
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Panel ID="pnlCalendar" runat="server" Style="margin-top: 0px;">
        <div id="Divid" runat="server">
        </div>
        <table id="tblTitle" runat="server" width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" Font-Names="Verdana"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="margin-left: 90%">
            <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
        </div>
        <br />
        <div style="margin-left: 75%">
            <asp:Label ID="lblStatingDate" Font-Size="Small" Visible="false" Font-Names="Verdana"
                runat="server"></asp:Label>
        </div>
        <center>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" ForeColor="White"
                DayNameFormat="Full" Font-Names="Book Antiqua" Font-Size="Medium" Width="1020px"
                ShowGridLines="True" ShowNextPrevMonth="False" Height="388px" OnDayRender="Calendar1_DayRender"
                OnSelectionChanged="Calendar1_SelectionChanged" BorderStyle="Solid">
                <DayHeaderStyle BackColor="#D9D9B3" ForeColor="Black" BorderStyle="Solid" Font-Italic="True"
                    Font-Size="Large" CssClass="TextFont" />
                <DayStyle BackColor="#FFFFFF" Font-Names="Adobe Kaiti Std R" BorderColor="SlateGray"
                    BorderWidth="1" Font-Bold="true" ForeColor="Red" CssClass="TextFont" />
                <NextPrevStyle Font-Italic="true" Font-Names="Arial CE" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
                <OtherMonthDayStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <SelectedDayStyle BackColor="LightBlue" BorderColor="SeaGreen" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
                <SelectorStyle BackColor="DarkSeaGreen" ForeColor="Snow" Font-Names="Times New Roman Greek"
                    Font-Size="Small" BorderColor="MediumSeaGreen" BorderWidth="1" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
                <TitleStyle BackColor="#8BA083" ForeColor="Black" Height="35" Font-Size="Large" Font-Names="Courier New Baltic"
                    BorderColor="SlateGray" BorderWidth="1" CssClass="TextFont" />
                <TodayDayStyle Font-Size="Large" />
            </asp:Calendar>
            <br />
          
            
                  <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="90px" Text="Approve TP"
                OnClientClick="return ValidateEmptyValue()" OnClick="btnSave_Click" />
            &nbsp
                    <asp:Button ID="btnReject" CssClass="BUTTON" runat="server" Text="Reject TP" Width="90px"
                        OnClick="btnReject_Click" />
            
            &nbsp
            <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                runat="server"></asp:TextBox>
            &nbsp
            <asp:Button ID="btnSubmit" CssClass="BUTTON" Width="140px" runat="server" Visible="false"
                Text="Send for ReEntry" OnClick="btnSubmit_Click" OnClientClick="return validate();" />
                    
        </center>
    </asp:Panel>
    <div>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="950px"
            class="ontop" Style="left: 250px; top: 200px; position: absolute;" Visible="false">
            <table width="100%" style="border: Solid 3px #4682B4; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr width="800px" style="background-color: #4682B4;">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger;"
                        align="center">
                        <asp:Label ID="lblHead" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                        align="center">
                        &nbsp;&nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="right">
                        <asp:Label ID="lblWorkType" SkinID="lblMand" runat="server" Text="Work Type &nbsp;&nbsp;&nbsp;"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlWorkType" Font-Names="Verdana" Font-Size="8pt" runat="server"
                                                AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddlWorkType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                                                    <img id="Img1" alt="" src="../../Images/loading/loading19.gif" runat="server" /><span
                                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="height: 50px">
                    <td style="width: 30%;" align="right">
                        <asp:Label ID="lblFieldForce" SkinID="lblMand" runat="server" Text="Joint Work (Workedwith) &nbsp;&nbsp;&nbsp;"></asp:Label>
                    </td>
                    <td style="width: 65%">
                        <asp:UpdatePanel ID="updatepanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtFieldForce" SkinID="MandTxtBox" runat="server" Width="300px"></asp:TextBox>
                                            <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="txtFieldForce" PopupControlID="pnlFieldForce"
                                                OffsetY="22"></asp:PopupControlExtender>
                                            <asp:Panel ID="pnlFieldForce" runat="server" Height="116px" Width="300px" BorderStyle="Solid"
                                                BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                Style="display: none; text-transform: capitalize">
                                                <div style="height: 15px; position: relative; background-color: #4682B4; overflow-y: scroll;
                                                    text-transform: capitalize; width: 100%; float: left" align="right">
                                                    <asp:Button ID="Button2" Style="font-family: Verdana; font-size: 5pt; width: 20px;
                                                        color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"
                                                        OnClientClick="HidePopup();" /></div>
                                                <br />
                                                <asp:CheckBoxList ID="chkFieldForce" Font-Names="Verdana" Font-Size="8pt" runat="server"
                                                    AutoPostBack="true" OnClick="LoadFieldForce();" OnSelectedIndexChanged="chkFieldForce_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSFCode" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblMRTerritory" Font-Names="Verdana" Font-Size="7pt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="right">
                        <asp:Label ID="lblTerritory" SkinID="lblMand" runat="server" Text="Territory &nbsp;&nbsp;&nbsp;"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="updatepanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtTerritory" SkinID="MandTxtBox" runat="server" Width="300px"></asp:TextBox>
                                <asp:PopupControlExtender ID="PopupControlExtender4" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="txtTerritory" PopupControlID="pnlTerritory"
                                    OffsetY="22"></asp:PopupControlExtender>
                                <asp:Panel ID="pnlTerritory" runat="server" Height="116px" Width="300px" BorderStyle="Solid"
                                    BorderWidth="2px" Direction="LeftToRight" Font-Bold="true" ScrollBars="Auto"
                                    BackColor="#FCFCFC" Style="display: none">
                                    <div style="height: 15px; position: relative; background-color: #4682B4; overflow-y: scroll;
                                        text-transform: capitalize; width: 100%; float: left" align="right">
                                        <asp:Button ID="Button1" Style="font-family: Verdana; font-size: 5pt; width: 20px;
                                            color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"
                                            OnClientClick="HidePopup();" /></div>
                                    <br />
                                    <asp:CheckBoxList ID="chkTerritory" Font-Names="Verdana" Font-Size="7pt" runat="server"
                                        OnClick="LoadTerritory();">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <asp:TextBox ID="txtTerr" runat="server" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="right">
                        <asp:Label ID="lblObjective" SkinID="lblMand" runat="server" Text="Objective &nbsp;&nbsp;&nbsp;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtObjective" SkinID="MandTxtBox" runat="server" Width="300">                                           
                        </asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Width="70px" Height="25px"
                            Text="Update" OnClick="btnUpdate_Click" OnClientClick="return validation();" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" Height="25px"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    <asp:Panel ID="pnlHidden" runat="server">
        <asp:HiddenField ID="hidDate" runat="server" />
        <br />
        <center>
        </center>
    </asp:Panel>
    </form>
</body>
</html>
