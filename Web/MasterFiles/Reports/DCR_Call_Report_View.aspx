<%@ Page Title="Call Report View" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DCR_Call_Report_View.aspx.cs" Inherits="DCR_Call_Report_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>DCR View</title>
    <style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, Mode, sf_name) {

            if (Mode.trim() == "View All Remark(s)") {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rptRemarks.aspx?sf_code=" + sfcode + "&Month=" + fmon + "&Year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
					"ModalPopUp",
					"null," +
					"toolbar=no," +
					"scrollbars=yes," +
					"location=no," +
					"statusbar=no," +
					"menubar=no," +
					"addressbar=no," +
					"resizable=yes," +
					"width=800," +
					"height=600," +
					"left = 0," +
					"top=0"
					);
                popUpObj.focus();
                // LoadModalDiv();

            }

            else {

                popUpObj = window.open("Rpt_DCR_View.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
					"ModalPopUp",
					"null," +
					"toolbar=no," +
					"scrollbars=yes," +
					"location=no," +
					"statusbar=no," +
					"menubar=no," +
					"addressbar=no," +
					"resizable=yes," +
					"width=800," +
					"height=600," +
					"left = 0," +
					"top=0"
					);
                popUpObj.focus();
                // LoadModalDiv();
            }
        }

</script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
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
            $('#btnSubmit').click(function () {

                var ddlMRName = $('#<%=ddlMR.ClientID%> :selected').text();
                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }
                var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                if (ddlMRName != '') {

                    var ddlMR = document.getElementById('<%=ddlMR.ClientID%>').value;
                }

                var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;

                var selectedvalue = $('#<%= rbnList.ClientID %> input:checked').val();

                if (ddlMR != -1 && ddlMR != 0 && ddlMRName != '') {

                    showModalPopUp(ddlMR, ddlMonth, ddlYear, selectedvalue, ddlMRName)
                }
                else {

                    showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, selectedvalue, FieldForce)
                }



            });
        }); 
    </script>
    <script type="text/jscript">
        $(document).ready(function () {
 var date = $("#txtdate").val();
if (date == "") 
 			document.getElementById('txtdate').valueAsDate = new Date();
          
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">

        <br />
        </div>
        <center>
            <table cellpadding="0" cellspacing="5">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivision" Visible="false" runat="server" SkinID="lblMand" Text="Division "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server"  SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkVacant" Text=" Only Vacant Managers" AutoPostBack="true" 
                       OnCheckedChanged="chkVacant_CheckedChanged" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">

                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <br />
                <br />
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Date" Visible="false"></asp:Label>
                    </td>
                    <td align="left">
                       
                        
                             <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Date" Visible="true"></asp:Label>
                    </td>
                    <td align="left">
                     <input id="txtdate" name="txtFrom" type="date"  CssClass="TEXTAREA" MaxLength="5"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox" />
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                    <td colspan="2">
                        <asp:Label ID="lblmode" runat="server" SkinID="lblMand" ForeColor="Blue" Font-Size="Medium"
                            Font-Bold="true" Font-Names="Calibri" Text="Select the Mode" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server" Font-Names="Calibri"
                            RepeatDirection="Horizontal" RepeatColumns="3" Width="550px" Visible="false">
                      
  							<asp:ListItem Text="Call Report View" Selected="True"> Call Report View</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="60px" Height="25px" 
                Text="View" CssClass="BUTTON" onclick="btnSubmit_Click1"
                 />
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="60%">
            </asp:Table>
        </center>
       <%-- <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>--%>
    </div>
    </form>
</body>
</html>
</asp:Content>