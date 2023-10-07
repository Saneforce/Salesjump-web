<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmWorkTypeStatusView.aspx.cs"
    Inherits="Reports_frmWorkTypeStatusView" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work Type View Status</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
    <style>
         .padding
        {
            padding:3px;
        }
          .chkboxLocation label 
{  
    padding-left: 5px; 
}

    </style>
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

                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var FrmMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FrmMonth == "---Select---") { alert("Select Field From Month."); $('#ddlFrmMonth').focus(); return false; }
                var FrmYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FrmYear == "---Select---") { alert("Select Field From Year."); $('#ddlFrmYear').focus(); return false; }
                var ToMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (ToMonth == "---Select---") { alert("Select Field To Month."); $('#ddlToMonth').focus(); return false; }
                var ToMonth = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (ToMonth == "---Select---") { alert("Select Field To Year."); $('#ddlToYear').focus(); return false; }
                if ($('#chkWorkType input:checked').length > 0) { return true; } else { alert('Select WorkType'); return false; }
            });
        }); 
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
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, To_Month, To_Year, sf_name, div_Code, Mode) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptWrkTypeViewStatus.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + "&To_year=" + To_Year + "&To_Month=" + To_Month + "&sf_name=" + sf_name + "&div_Code=" + div_Code + "&ChkWorkType=" + Mode,
    "ModalPopUp",
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
</script>
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
            
            var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (FieldForce == "---Select---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
            var FrmMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
            if (FrmMonth == "---Select---") { alert("Select Field From Month."); $('#ddlFrmMonth').focus(); return false; }
            var FrmYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
            if (FrmYear == "---Select---") { alert("Select Field From Year."); $('#ddlFrmYear').focus(); return false; }
            var ToMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
            if (ToMonth == "---Select---") { alert("Select Field To Month."); $('#ddlToMonth').focus(); return false; }
            var ToMonth = $('#<%=ddlToYear.ClientID%> :selected').text();
            if (ToMonth == "---Select---") { alert("Select Field To Year."); $('#ddlToYear').focus(); return false; }
            if ($('#chkWorkType input:checked').length > 0) {
            }
            else {
                alert('Select WorkType'); return false;
            }

            var CHK = document.getElementById("<%=chkWorkType.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            var check;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {

                    check += "'" + label[i].innerHTML + "'" + ',';
                }
            }

            var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
            var ddlDivision = document.getElementById('<%=ddlDivision.ClientID%>').value;
            var ddlFrmMonth = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
            var ddlFrmYear = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
            var ddlToMonth = document.getElementById('<%=ddlToMonth.ClientID%>').value;
            var ddlToYear = document.getElementById('<%=ddlToYear.ClientID%>').value;

            showModalPopUp(ddlFieldForceValue, ddlFrmMonth, ddlFrmYear, ddlToMonth, ddlToYear, FieldForce, ddlDivision, check)

        });
    }); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div id="Divid" runat="server">
            </div>
            <%--<ucl:Menu1 ID="Menu1" runat="server" />--%>
        </div>
        <br />
        <center>
            <table>
                <tr align="center"  width="120px">
                    <td colspan="2" class="stylespc">
                        <asp:CheckBoxList ID="chkPeriodically" Visible="false" CssClass="Checkbox" runat="server">
                            <asp:ListItem Text="Periodically"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFrmMonth" runat="server" SkinID="ddlRequired">
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
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="From Year"></asp:Label>
                        <asp:DropDownList ID="ddlFrmYear" runat="server" Width="80px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label4" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlToMonth" runat="server" SkinID="ddlRequired">
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
                        <asp:Label ID="lblToYear" runat="server" Text="To Year" SkinID="lblMand"></asp:Label>
                        <span style="margin-left: 14px"></span>
                        <asp:DropDownList ID="ddlToYear" runat="server" Width="80px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="20px">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblWorkType" Style="color: Red; text-decoration: underline" SkinID="lblMand"
                            runat="server" Text="Work Type :"></asp:Label>
                    </td>
                </tr>
                <tr class="padding" >
                    <td colspan="2" align="left">
                        <asp:CheckBoxList ID="chkWorkType" CssClass="chkboxLocation" CellPadding="10" RepeatColumns="3" Font-Names="Verdana" Font-Size="11px"  RepeatDirection="vertical"                             Width="400px" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                CssClass="btnnew" />
            &nbsp
            <asp:Button ID="btnClear" runat="server" Width="70px" Height="25px" Text="Clear"
                CssClass="btnnew" OnClick="btnClear_Click" />
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
