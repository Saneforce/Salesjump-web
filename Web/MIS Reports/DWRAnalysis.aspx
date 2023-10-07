<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DWRAnalysis.aspx.cs" Inherits="MIS_Reports_DWRAnalysis" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Analysis</title>
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />

    <script type = "text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Fmon, FieldforceName, FYear) {

            popUpObj = window.open("rdlFile/rdlDWRAnalysis.aspx?sfcode=" + sfcode + "&FMonth=" + Fmon + "&SF_Name=" + FieldforceName + "&FYear=" + FYear,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }

</script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
              $('#btnGo').click(function () {
                  var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                  if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }


                  var ddlMR = document.getElementById('<%=ddlMR.ClientID%>').value;
                  var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                  var ddlFMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                  var ddlFYear = document.getElementById('<%=ddlFYear.ClientID%>').value;

                  if (ddlMR != 0) {
                      showModalPopUp(ddlMR, ddlFMonth, SName, ddlFYear);
                  }
                  else {
                      showModalPopUp(sf_Code, ddlFMonth, SName, ddlFYear);
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
        <center>
            <br />
            
            <table>
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
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
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" 
                CssClass="BUTTON" 
                 />
            <br />
            <br />
            <asp:Label ID="lblModelevel"  runat="server" style="text-decoration:underline;font-size:10pt;font-weight:bold" SkinID="lblMand"></asp:Label>
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
