<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListeddrCamp_View.aspx.cs" Inherits="MIS_Reports_ListeddrCamp_View" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Campaign Drs - View</title>
      <script type="text/javascript">
          var popUpObj;
        
          function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, campcode, campname) {
              
              //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
              popUpObj = window.open("rptListeddrCamp_View.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &camp_code=" + campcode + " &campaign=" + campname,
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
           
            var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "--Select--") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "--Select--") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

            var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
            if (TYear == "--Select--") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
            var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
            if (TMonth == "--Select--") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }

            var campaign = $('#<%=ddlCampaign.ClientID%> :selected').text();
            if (campaign == "---Select---") { alert("Select Campaign."); $('#ddlCampaign').focus(); return false; }

            var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
            var Frm_year = document.getElementById('<%=ddlFYear.ClientID%>').value;
            var Frm_Month = document.getElementById('<%=ddlFMonth.ClientID%>').value;
            var To_year = document.getElementById('<%=ddlTYear.ClientID%>').value;
            var To_Month = document.getElementById('<%=ddlTMonth.ClientID%>').value;

            var camp_code = document.getElementById('<%=ddlCampaign.ClientID%>').value;

            if (Frm_Month > To_Month && Frm_year == To_year) {

                alert("To Month must be greater than From Month"); return false;
            }
            else if (Frm_year > To_year) {
                alert("To Year must be greater than From Year"); return false;
            }
            else {

                showModalPopUp(sf_Code, Frm_Month, Frm_year, To_year, To_Month, sf_name, camp_code, campaign);
            }
        });
    }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="Divid" runat="server">
        </div>
    <br />
      <center>
            <table >
              <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                 
                </td>
             <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                </td>
            </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="From Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="From Year"></asp:Label>
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired"
                            Width="60">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTMonth" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text="To Year" Width="60"></asp:Label>
                        <asp:DropDownList ID="ddlTYear" runat="server"  SkinID="ddlRequired"
                            Width="60">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td align="left" class="stylespc">
                <asp:Label ID="lblCamp" runat="server" Text="Campaign" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                  <asp:DropDownList ID="ddlCampaign" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                           
                </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="BUTTON"  Width="70px" 
                Height="25px" 
                 />
        </center>
        <br />

    </div>
    </form>
</body>
</html>
