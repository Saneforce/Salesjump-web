<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Analysis.aspx.cs" Inherits="MIS_Reports_DWRAnalysis" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Analysis</title>
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
        var popUpObj;
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("DCR_Analysis_Report.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&sf_name=" + sf_name,
    "ModalPopUp"+randomnumber,
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=900," +
    "height=600," +
    "left = 100," +
    "top=100"
    );
            popUpObj.focus();
            //
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                //var ImgSrc = "https://s4.postimg.org/l4xbni8jx/loading13.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }

        function showMissedDR(sfcode, fmon, fyr, cmode) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("VisitDetList.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&cMode=" + cmode,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=1100," +
    "height=1000," +
    "left = 100," +
    "top=100"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }

    </script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGo').click(function () {
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "Select") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
                
                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                var FMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                var ddMRindex = $('#ddlMR').find(":selected").index();                
                
                var frmYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                var frmMonth = $('#ddlFMonth').find(":selected").index();                    
                    
                if (ddMRindex != 0) {
                    sf_Code = document.getElementById('<%=ddlMR.ClientID%>').value;
                }
                showModalPopUp(sf_Code, frmMonth, frmYear, SName);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <center>
            <br />
            <table>
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired" Visible="false">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                            SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
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
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
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
                        <asp:Label ID="lblFYear" Width="70px" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFYear" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
                 />
            <br />
            <br />
            <asp:Label ID="lblModelevel" runat="server" Style="text-decoration: underline; font-size: 10pt;
                font-weight: bold" SkinID="lblMand"></asp:Label>
            <br />
            <br />
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" Style="border-collapse: collapse;
                border: solid 0px #999999;" GridLines="Both" Width="95%">
            </asp:Table>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </center>
    </div>
    </form>
</body>
</html>
