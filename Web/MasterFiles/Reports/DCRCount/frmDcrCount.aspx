<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDcrCount.aspx.cs" Inherits="Reports_DCRCount_frmDcrCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Count</title>
  
     <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, sf_name, StrVacant, div_Code) {
            popUpObj = window.open("rptDCRCount.aspx?sf_code=" + sfcode + "&Month=" + fmon + "&Year=" + fyr + "&sf_name=" + sf_name + "&StrVacant=" + StrVacant + "&div_Code=" + div_Code,
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

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                var ImgTxT = "http://i.imgur.com/KUJoe.gifhttps://s10.postimg.org/g0v7h43wp/Text_1.gif";

               // var ImgSrc = "https://s10.postimg.org/4i4mt6p3t/loading_23_ook.gif"

                 var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;margin-left:40%">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:300px; height: 235px;position: fixed;top: 10%;left: 35%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }
</script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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
                if (FieldForce == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var grp = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (grp == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var grp = $('#<%=ddlYear.ClientID%> :selected').text();
                if (grp == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Div_Code = document.getElementById('<%=ddlDivision.ClientID%>').value;
                var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;
                var chkDetail = document.getElementById("chkWOVacant");

                var strValue = 1;
                if (chkDetail.checked) {
                    var strValue = 0;
                }

                showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, FieldForce, strValue, Div_Code)
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <ucl:Menu ID="Menu1" runat="server" />
        </div>
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblDivision"  runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
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
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="160px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMoth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblYear" runat="server" Text="To Year" SkinID="lblMand"></asp:Label>
                        <span style="margin-left: 14px"></span>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblWOVacant" Text="Without Vacants" runat="server" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:CheckBox ID="chkWOVacant" Checked="true" runat="server" />
                    </td>
                </tr>  
            </table>
            <br />
              <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                CssClass="btnnew"  />
               
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
