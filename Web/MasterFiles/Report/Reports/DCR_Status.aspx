<%@ Page Title="DCR Status" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DCR_Status.aspx.cs" Inherits="Reports_DCR_Status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>DCR Status</title>
    <style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDCRStatus.aspx?sf_code=" + sfcode + "&cmon=" + fmon + "&cyear=" + fyr + "&sf_name=" + sf_name,
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

        function showDCR_Status_DetailedPopUp(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptDCR_Status_Detailed.aspx?sf_code=" + sfcode + "&cmon=" + fmon + "&cyear=" + fyr + "&sf_name=" + sf_name,
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

        function showPeriodicRep(sfcode, fdate, todate, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("New_rpt_DCR_Status_Datewise.aspx?sf_code=" + sfcode + "&fdate=" + fdate + "&todate=" + todate + "&sf_name=" + sf_name,
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


        function showPeriodicRep_Det(sfcode, fdate, todate,sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("New_rpt_DCR_Status_Datewise_Det.aspx?sf_code=" + sfcode + "&fdate=" + fdate + "&todate=" + todate + "&sf_name=" + sf_name,
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
  
</head>
<body>
  <script type="text/javascript" id=" ">

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
              if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "1") {
                  var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                  if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
              }
              if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "2") {
                  if ($('[id$=txtEffFrom]').val().length == 0)
                  { alert("Select Effective From Date."); $('#txtEffFrom').focus(); return false; }
              }
              var SFName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
              if (SFName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
              var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
              if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "1") {


                  var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                  var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;

                  var chkDetail = document.getElementById("chkDetail");

                  if (chkDetail.checked) {

                      showDCR_Status_DetailedPopUp(ddlFieldForceValue, ddlMonth, ddlYear, SFName);

                  }
                  else {

                      showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, SFName)
                  }
              }
              if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "2") {
                  var txtEffFrom = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                  var txtEffTo = document.getElementById('<%=txtEffTo.ClientID%>').value;

                  var chkDetail = document.getElementById("chkDetail");

                  if (chkDetail.checked) {

                      showPeriodicRep_Det(ddlFieldForceValue, txtEffFrom, txtEffTo, SFName);

                  }
                  else {

                      showPeriodicRep(ddlFieldForceValue, txtEffFrom, txtEffTo, SFName)
                  }
              }
              return false;
          });
          $('[id$=txtEffFrom]').change(function () {
              if ($(this).val().length > 2) {
                  date = $(this).val();
                  var efftodate = '';
                  var emon = 0;
                  var edate = 0;
                  var eyear = 0;
                  var todate = $(this).val().split('/');
                  if (todate[0] == '01') {
                      if (todate[1] == '02')
                          edate = '28';
                      else
                          edate = '30';
                  }
                  else
                      edate = parseInt(todate[0]) - 1;

                  var evardate = edate.toString();

                  if (evardate.length == 1)
                      evardate = '0' + evardate;
                  if (todate[0] != '01') {
                      if (todate[1] != '12') {
                          emon = parseInt(todate[1]) + 1;

                          var evarmon = emon.toString();

                          if (evarmon.length == 1)
                              evarmon = '0' + evarmon;
                          efftodate = evardate + '/' + evarmon + '/' + todate[2];
                      }
                      else {
                          emon = 1;
                          var evarmon = emon.toString();

                          if (evarmon.length == 1)
                              evarmon = '0' + evarmon;
                          eyear = parseInt(todate[2]) + 1;
                          efftodate = evardate + '/' + evarmon + '/' + eyear;
                      }
                  }
                  else
                      efftodate = evardate + '/' + todate[1] + '/' + todate[2];
                  $('[id$=txtEffTo]').val(efftodate);

              }
          });
      }); 
    </script>

    <script type="text/javascript" language="javascript">
        function hideDiv() {
            
            if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() =="1") {
                var lblmon = document.getElementById('lblmon');
                lblmon.style.display = "block";
                lblmon.style.visibility = "visible";
                var ddlMonth = document.getElementById('ddlMonth');
                ddlMonth.style.display = "block";
                ddlMonth.style.visibility = "visible";
                var lblYear = document.getElementById('lblYear');
                lblYear.style.display = "block";
                lblYear.style.visibility = "visible";
                var ddlYear = document.getElementById('ddlYear');
                ddlYear.style.display = "block";
                ddlYear.style.visibility = "visible";

                var lblfrom = document.getElementById('lblfrom');
                lblfrom.style.display = "none";
                lblfrom.style.visibility = "hidden";
                var txtEffFrom = document.getElementById('txtEffFrom');
                txtEffFrom.style.display = "none";
                txtEffFrom.style.visibility = "hidden";
                var lblto = document.getElementById('lblto');
                lblto.style.display = "none";
                lblto.style.visibility = "hidden";
                var txtEffTo = document.getElementById('txtEffTo');
                txtEffTo.style.display = "none";
                txtEffTo.style.visibility = "hidden";
            }

            if ($('#<%=rdoType.ClientID %> input[type=radio]:checked').val() == "2") {
                var lblfrom = document.getElementById('lblfrom');
                lblfrom.style.display = "block";
                lblfrom.style.visibility = "visible";
                var txtEffFrom = document.getElementById('txtEffFrom');
                txtEffFrom.style.display = "block";
                txtEffFrom.style.visibility = "visible";
                var lblto = document.getElementById('lblto');
                lblto.style.display = "block";
                lblto.style.visibility = "visible";
                var txtEffTo = document.getElementById('txtEffTo');
                txtEffTo.style.display = "block";
                txtEffTo.style.visibility = "visible";

                var lblmon = document.getElementById('lblmon');
                lblmon.style.display = "none";
                lblmon.style.visibility = "hidden";
                var ddlMonth = document.getElementById('ddlMonth');
                ddlMonth.style.display = "none";
                ddlMonth.style.visibility = "hidden";
                var lblYear = document.getElementById('lblYear');
                lblYear.style.display = "none";
                lblYear.style.visibility = "hidden";
                var ddlYear = document.getElementById('ddlYear');
                ddlYear.style.display = "none";
                ddlYear.style.visibility = "hidden";
            }
        }    
    </script>

    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <div id="Divid" runat="server">
        </div>
        <center>

        <br />
            <br />
            <table >
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblDivision" Visible="false" runat="server" SkinID="lblMand" Text="Division "></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" Visible="false" runat="server" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" Width="250"
                            AutoPostBack="false" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblType" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" style="FONT-SIZE: 8pt;COLOR: black;FONT-FAMILY: Verdana;" onchange="hideDiv()">
                            <asp:ListItem Value="1" Text="Monthwise"  Selected = "True" ></asp:ListItem> 
                            <asp:ListItem Value="2" Text="Periodwise"></asp:ListItem> 
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%--<tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
               
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblmon" runat="server" SkinID="lblMand" Text="Month"  ></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" Width="100" >
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
                        <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Text="Year"  ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100" >
                            <%--     <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="2013"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2014"></asp:ListItem>
                        <asp:ListItem Value="3" Text="2015"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
               
              
                <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="lblfrom" runat="server" SkinID="lblMand" Text="From Date" style= "display : none" ></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffFrom" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);" style= "display : none" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc"> 
                        <asp:Label ID="lblto" runat="server" SkinID="lblMand" Text="To Date" style= "display : none" ></asp:Label>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffTo" runat="server" SkinID="MandTxtBox"  Enabled ="false" style= "display : none" ></asp:TextBox>
                       
                    </td>
                </tr>
             
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Without Vacants"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkVacant" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label4" runat="server" SkinID="lblMand" Text="Detailed"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkDetail" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Text="View" Width="70px"
                Height="25px" UseSubmitBehavior="false" onclick="btnSubmit_Click1"/>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="60%">
            </asp:Table>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </center>
    </div>
    </form>
</body>
</html>
</asp:Content>