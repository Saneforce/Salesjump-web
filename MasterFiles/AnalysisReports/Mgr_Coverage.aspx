<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mgr_Coverage.aspx.cs" Inherits="MasterFiles_AnalysisReports_Mgr_Coverage" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Manager - HQ Wise Visit Coverage Analysis</title>
        <style type="text/css">
         .padding
        {
            padding:3px;
        }
          .chkboxLocation label 
{  
    padding-left: 5px; 
}
    td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
        <script type="text/javascript">
            var popUpObj;

            function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rptMgr_Coverage.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
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

            var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
            var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
            var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

            var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
            if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
            var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
            if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }


            var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
            var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
            var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
            var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
            var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;

            if (Month1 > Month2 && Year1 == Year2) {

                alert("To Month must be greater than From Month"); return false;
            }
            else if (Year1 > Year2) {
                alert("To Year must be greater than From Year"); return false;
            }
            else {
                showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name);
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
          <table>
               
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFieldForce" runat="server"  SkinID="ddlRequired">
                       <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
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
               
                
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                BackColor="LightBlue"  />
       <%--     &nbsp
            <asp:Button ID="btnClear" runat="server" Width="70px" Height="25px" Text="Clear"
                CssClass="btnnew" OnClick="btnClear_Click" />--%>
        </center>
  
    </div>
    </form>
</body>
</html>
