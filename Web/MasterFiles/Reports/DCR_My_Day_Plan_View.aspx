<%@ Page Title="My Day Plan View" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DCR_My_Day_Plan_View.aspx.cs" Inherits="DCR_My_Day_Plan_View" %>
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
 		input[type='text'], select, label
        {
            line-height: 22px;
            padding: 0px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/style1.css" />

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
            $('#<%=btnSubmit.ClientID%>').click(function () {
			
				var	subdiv =$('#<%=subdiv.ClientID%> :selected').text();
                if (subdiv == "--Select--") { alert("Select Sub Division."); $('#<%=subdiv.ClientID%>').focus(); return false; }
                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Field Force---") { alert("Select FieldForce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
               
				  if ($('#txtdate').val() == "") { alert("Please Select Date."); $('#txtdate').focus(); return false; }

                if (subdiv != -1 && FieldForce != 0 && FieldForce != '') {

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
         <div class="container" style="width:100%">

           <div class="row">
            <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                Division</label>
                  <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" 
                             onselectedindexchanged="subdiv_SelectedIndexChanged" Width="200px"
                       AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
</div>
</div>
<div class="row">
                            <label id="st" class="col-md-2  col-md-offset-3  control-label">
                                State</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
			<div class="row">
            <label id="lblFF" class="col-md-2  col-md-offset-3  control-label">
                Field Force</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:Label ID="Label3" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>

                        <asp:DropDownList ID="DropDownList1" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>

                        <asp:Label ID="Label4" runat="server" SkinID="lblMand" Text="Date" Visible="false"></asp:Label>
                        
                             <asp:DropDownList ID="DropDownList2" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                        </asp:DropDownList>
                   
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" width="350" CssClass="form-control">
                        </asp:DropDownList>
                   </div>
                    </div>
                    </div>
			<div class="row">
            <label id="lbldesig" class="col-md-2  col-md-offset-3  control-label">
                Designation</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>

                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                   
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                   
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Date" Visible="false"></asp:Label>
                       
                        
                             <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                        </asp:DropDownList>
                 
	<asp:DropDownList ID="Dropdesignation" runat="server"  CssClass="form-control" >
                        </asp:DropDownList>
  </div>
                    </div>
                    </div>
                  <div class="row">
            <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <input id="txtdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                tabindex="1" skinid="MandTxtBox" />
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
                   </div>
                    </div>
                    </div>
                   
                        <asp:Label ID="lblmode" runat="server" SkinID="lblMand" ForeColor="Blue" Font-Size="Medium"
                            Font-Bold="true" Font-Names="Calibri" Text="Select the Mode" Visible="false"></asp:Label>
                  
                        <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server" Font-Names="Calibri"
                            RepeatDirection="Horizontal" RepeatColumns="3" Width="550px" Visible="false">
                      
  							<asp:ListItem Text="TPMyDayPlan" Selected="True"> TP MY Day Plan</asp:ListItem>
                        </asp:RadioButtonList>
                    
  <div class="row">
            <div class="col-md-6  col-md-offset-5">

            <asp:Button ID="btnSubmit" runat="server" Cssclass="btn btn-primary btnview" style="width: 100px"
                Text="View"  onclick="btnSubmit_Click1"
                 />
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="60%">
            </asp:Table>

 </div>
       
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