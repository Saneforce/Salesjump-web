<%@ Page Title="TP View Report" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master" CodeFile="TP_View_Report.aspx.cs" Inherits="MasterFiles_Temp_TP_View_Report" %>

<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>TP View Report</title>
     
    
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

     <script type="text/javascript">
         var popUpObj;
         function showModalPopUp(sfcode, fmon, fyr, level, sf_name) {
             //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
             popUpObj = window.open("rptTPView.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&level=-1" + "&sf_name=" + sf_name,
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

    <script type="text/javascript" language="javascript">

        function MyFunc(hypLevel, ddlFF, ddlMon, ddlYr) {
            var hypLevel = document.getElementById(hypLevel);
            var ddlFF = document.getElementById(ddlFF);
            var ddlMon = document.getElementById(ddlMon);
            var ddlYr = document.getElementById(ddlYr);
            //hypLevel.href =

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
                            $('#<%=btnSubmit.ClientID%>').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#<%=btnSubmit.ClientID%>').click(function () {

                var Sf = $('#<%=ddlFilterby.ClientID%> :selected').text();
                if (Sf == "---Select---") { alert("Select Filter by."); $('#<%=ddlFilterby.ClientID%>').focus(); return false; }
                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#<%=ddlYear.ClientID%>').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;

                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;

                showModalPopUp(sf_Code, Month1, Year1, -1, Name);
            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server"></div>
        <center>

        <br />
        <br />
       <table id="tblTpRpt" cellpadding="3" cellspacing="3">
           <tr>
               <td align="left" class="stylespc" width="120px">
                   <asp:Label ID="lblFilterby" runat="server" SkinID="lblMand" Text="Filter by"></asp:Label>
               </td>
               <td align="left">
                   <asp:DropDownList ID="ddlFilterby" Width="240px" AutoPostBack="true" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilterBy_SelectedChange" runat="server">
                   </asp:DropDownList>
                  <%-- <asp:DropDownList ID="ddlFilterbyColor" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>--%>
               </td>
           </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="---Select---" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Alphabetical" ></asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">                         
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" Width="250px" AutoPostBack="false" SkinID="ddlRequired">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>
                </td>
            </tr>

            <%--<tr>
               <td>
                   <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
               </td>
               <td align="left">
                   <asp:DropDownList ID="ddlDivision" Width="110px" SkinID="ddlRequired" runat="server">
                   </asp:DropDownList>
               </td>
           </tr>--%>

           
            <tr >
                <td align="left" class="stylespc">
                    <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="80px" SkinID="ddlRequired">
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
            <tr >
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                </td>
               
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                     <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>                
                    </asp:DropDownList>
                </td>               
            </tr>
            <tr >
                <td align="left" class="stylespc">
                    <asp:CheckBox id="chkConsolidate" runat="server" Text="Consolidate" Font-Size="11px"  Font-Names="Verdana" AutoPostBack="true"
                        oncheckedchanged="chkConsolidate_CheckedChanged" />
                </td>
                <td id="hypConsolidate" runat="server" align="left">

              <%--  <asp:HyperLink ID="hypLevel1" runat="server" Text="Level1" NavigateUrl="javascript:MyFunc();" ></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel2" runat="server" Text="Level2" NavigateUrl="#"></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel3" runat="server" Text="Level3" NavigateUrl="#"></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel4" runat="server" Text="Level4" NavigateUrl="#"></asp:HyperLink>--%>
                                
                    <asp:RadioButtonList ID="rdoHypLevel" runat="server"  RepeatDirection="Horizontal">
                        <asp:ListItem Text="Level1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Level2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Level3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Level4" Value="4"></asp:ListItem>
                    </asp:RadioButtonList>    
                </td>
            </tr>
        </table>
          <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View" CssClass="BUTTON"
             onclick="btnSubmit_Click"/>
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="80%">
        </asp:Table>
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
</asp:Content>