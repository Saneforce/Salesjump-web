<%@ Page Title="DCR View" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DCR_View.aspx.cs" Inherits="Reports_DCR_View" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

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

        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" type="text/css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            
			var sf_type = '<%=Session["sf_type"]%>';
            //alert(sf_type);
            if (sf_type == "2") {
                $('.chkVacant').hide();
            }
            else {
                $('.chkVacant').show();
            }
			
			
            var rbtnvalue = $('#<%=rbnList.ClientID %>').find('input[type=radio]:checked').val();
            

            if (rbtnvalue == "View_All_DCR_Date(s)") {
                $('#<%=exceldld.ClientID %>').show();
            }
            else {
                $('#<%=exceldld.ClientID %>').hide();
            }




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
			
			     $('#<%=TextBox1.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy'
            })
            $('#<%=TextBox2.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy'

            });
			
            $('#<%=btnSubmit.ClientID%>').click(function () {


                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
				if ($('#<%=TextBox1.ClientID%>').val() == "") { alert("Please select From Date."); $('#<%=TextBox1.ClientID%>').focus(); return false; }
                if ($('#<%=TextBox2.ClientID%>').val() == "") { alert("Please select To Date."); $('#<%=TextBox2.ClientID%>').focus(); return false; }
   

                var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }
                if (ddlMRName != '') {

                    var ddlMR = document.getElementById('#<%=ddlMR.ClientID%>').value;
                }

                var ddlFieldForceValue = document.getElementById('#<%=ddlFieldForce.ClientID%>').value;

                var ddlMonth = document.getElementById('#<%=ddlMonth.ClientID%>').value;

                var ddlYear = document.getElementById('#<%=ddlYear.ClientID%>').value;

                var selectedvalue = $('#<%= rbnList.ClientID %>input:checked').val();
                alert(selectedvalue);

                if (ddlMR != -1 && ddlMR != 0 && ddlMRName != '') {

                    showModalPopUp(ddlMR, ddlMonth, ddlYear, selectedvalue, ddlMRName)
                }
                else {

                    showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, selectedvalue, FieldForce)
                }

      


            });
			
        });

        function validation() {
            var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (FieldForce == "---Select Clear---")
            {
                alert("Select FieldForce Name.");
                $('#<%=ddlFieldForce.ClientID%>').focus();
                return false;
            }
            if ($('#<%=TextBox1.ClientID%>').val() == "")
            {
                alert("Please select From Date.");
                $('#<%=TextBox1.ClientID%>').focus();
                return false;
            }
            if ($('#<%=TextBox2.ClientID%>').val() == "")
            {
                alert("Please select To Date.");
                $('#<%=TextBox2.ClientID%>').focus();
                return false;
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
	
            <asp:HiddenField ID="HiddenField1" runat="server" />
             <asp:HiddenField ID="HiddenField2" runat="server" />
        
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <div>
        <div id="Divid" runat="server">

        <br />
        </div>
        <center>
            <table cellpadding="0" cellspacing="5">

             <tr>
                    <td colspan="2">
                        <asp:Label ID="lblmode" runat="server" SkinID="lblMand" ForeColor="Blue" Font-Size="Medium"
                            Font-Bold="true" Font-Names="Calibri" Text="Select the Mode"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server" Font-Names="Calibri"
                            RepeatDirection="Horizontal" RepeatColumns="3" Width="550px"
								onselectedindexchanged="rbnList_SelectedIndexChanged" AutoPostBack="true">
                           <%-- <asp:ListItem Text="DCRDoc">  View All DCR Customer(s)</asp:ListItem>--%>
                           <%-- <asp:ListItem Text="View All DCR Date(s)">  View All DCR Date(s)</asp:ListItem>--%>
                            <%--<asp:ListItem Text="Remarks">  View All Remark(s)</asp:ListItem>--%>
                            <asp:ListItem Selected="True" Text="DetailedView">  Detailed View</asp:ListItem>
                          <%--  <asp:ListItem Text="VALDRemark">  View All Listed Customer Remark(s)</asp:ListItem>
                            <asp:ListItem Text="NADDates">  Not Approved DCR Dates</asp:ListItem>--%>
                            <asp:ListItem Text="Closing Stock View">  Closing Stock View</asp:ListItem>
  							<%--<asp:ListItem Text="TPMyDayPlan" Enabled="false">  TP MY Day Plan</asp:ListItem>--%>
							<asp:ListItem Text="View_All_DCR_Date(s)">View_All_DCR_Date(s)</asp:ListItem>
                            <asp:ListItem Text="SKU Summary">SKU Summary</asp:ListItem>
                            <asp:ListItem Text="Sales Return">Sales Return</asp:ListItem>
                            <asp:ListItem Text="Primarydetailedview">Primary Detailed View</asp:ListItem>
                            <asp:ListItem Text="Primary SKU Summary">Primary SKU Summary</asp:ListItem>
                            <asp:ListItem Text="Promotional Value Summary">Incentives Summary</asp:ListItem>
                            <asp:ListItem Text="Primary Vs Secondary">Primary Vs Secondary</asp:ListItem>

                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivision" Visible="false" runat="server" SkinID="lblMand" Text="Division "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Team"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                            SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkVacant" Text=" Only Managers" AutoPostBack="true" CssClass="chkVacant"    
                       OnCheckedChanged="chkVacant_CheckedChanged" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMR" runat="server" Text="FieldForce" SkinID="lblMand" Visible="false"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" 
                            Visible="false" onselectedindexchanged="ddlMR_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Year" Visible="true"></asp:Label>
                        <asp:Label ID="Lbl3_Form" runat="server" Text="From Date :" Visible="false"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100">
                        </asp:DropDownList>			
						   <asp:TextBox ID="TextBox1" runat="server" autocomplete="off" DataFormatString="{dd/MM/yyyy}" CssClass="form-control" Width="120">
                                </asp:TextBox>
					
					
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Month" Visible="true"></asp:Label>
                        <asp:Label ID="Lbl4_To" runat="server" Text="To Date :" ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" Width="100">
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
						<asp:TextBox ID="TextBox2" runat="server" autocomplete="off" DataFormatString="{dd/MM/yyyy}" Width="120" CssClass="form-control">                                
                                </asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td>
                
                </td>
                </tr>
               
            </table>
            <br />
            <br />
            <div class="row col-md-offset-5" style="margin-top: 5px">
                <div class="col-md-2  ">
                    <asp:Button ID="btnSubmit" runat="server"  ValidationGroup="Date" BackColor="#1a73e8" Text="View" CssClass="btn btn-primary" onclick="btnSubmit_Click1" />
                </div>
                <div class="col-md-3">
                    <asp:Button runat="server" ID="exceldld" CssClass="btn btn-primary" BackColor="#1a73e8" ForeColor="White" Text="Excel" OnClientClick="return validation()" OnClick="exceldld_Click" />
                </div>
            </div>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="60%">
            </asp:Table>
        </center>
       
    </div>
    </form>
</body>
</html>
</asp:Content>