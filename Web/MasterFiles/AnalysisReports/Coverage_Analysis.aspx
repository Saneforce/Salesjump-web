<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Coverage_Analysis.aspx.cs" Inherits="MasterFiles_AnalysisReports_Coverage_Analysis" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coverage Analysis</title>
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

         function showModalPopUp(sfcode, fmon, fyr, sf_name) {
             //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
             popUpObj = window.open("rptCoverage_Analysis.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name,
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
            var Year = $('#<%=ddlYear.ClientID%> :selected').text();
            if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
            var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
            if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

            var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
            var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
            var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;

            showModalPopUp(sf_Code, Month1, Year1, Name);
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
                      <asp:Label ID="lblToYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                    </td>
                 <td align="left" class="stylespc">                      
                     
                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
               
                
            </table>
            <br />
            <asp:Button ID="btnSubmit"  runat="server" Width="70px" Height="25px" Text="View"
                BackColor="LightBlue"  />
           
        </center>
    </div>
  <%--  <script type="text/javascript">
        function openWin()
        {
            x=document.getElementById("<%=ddlFieldForce.ClientID%>");
            mn=document.getElementById("<%=ddlMonth.ClientID%>").value;
            yr = document.getElementById("<%=ddlYear.ClientID%>").value;
            sf=x.value;
            sfn=x.options[x.selectedIndex].text;
            window.open('rptCoverage_Analysis.aspx?sf_code='+sf+'&FMonth=' + mn +'&Fyear=' + Yr+ '&sf_name=' + sfn,null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');
        
        }
    </script>--%>
    </form>
</body>
</html>
