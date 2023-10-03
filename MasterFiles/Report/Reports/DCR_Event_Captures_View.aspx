<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Event_Captures_View.aspx.cs" Inherits="MasterFiles_Reports_DCR_Event_Captures_View" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
        <%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
        <%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title>DCR View</title>
            <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
            <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
            <link type="text/css" href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
            <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
            <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet" />
            <link type="text/css" rel="stylesheet" href="../css/style1.css" />
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

            <style type="text/css">
                input[type='text'], select, label {
                    line-height: 22px;
                    padding: 4px 6px;
                    font-size: medium;
                    border-radius: 7px;
                    width: 100%;
                    font-weight: normal;
                }
                #tblDocRpt {
                    margin-left: 300px;
                }
                .ranges {
                    display: inline-flex;
                    height: 200px;
                    width: 420px;
                    overflow-x: scroll;
                }
                .daterangepicker .ranges {
                    width: 420px;
                    text-align: left;
                }
                .daterangepicker.dropdown-menu {
                    max-width: none;
                    z-index: 3000;
                    top: 306px;
                }
            </style>
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
            <script type="text/javascript">
                $(document).ready(function () {

                    var sf_type = '<%=Session["sf_type"]%>';
                    //alert(sf_type);
                    if (sf_type == "2") {
                        $('.chkVacant').hide();
                        $('.ddlAlpha').hide();
                    }
                    else {
                        $('.chkVacant').show();
                        $('.ddlAlpha').show();
                    }


                    <%--$('#<%=txtdate.ClientID%>').datepicker({
                        dateFormat: 'dd/mm/yy'
                    });
            
                    $('#<%=txttdate.ClientID%>').datepicker({
                        dateFormat: 'dd/mm/yy'

                    });--%>

                    //$('#txtdate').datepicker({
                    //    dateFormat: 'dd/mm/yy'
                    //});

                    //$('#txttdate').datepicker({
                    //    dateFormat: 'dd/mm/yy'

                    //});


                    //   $('input:text:first').focus();
                    <%--$('input:text').bind("keydown", function (e) {
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
                    });--%>
                                 
                });

                $('#<%=btnSubmit.ClientID%>').click(function () {
                    var mod = "Event Captures View";
                    var ddlMRName = $('#<%=ddlMR.ClientID%> :selected').text();
                    var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();

                    if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }

                    if ($('#<%=txtdate.ClientID%>').val() == "") { alert("Select From Date"); $('#<%=txtdate.ClientID%>').focus(); return false; }

                    if ($('#<%=txttdate.ClientID%>').val() == "") { alert("Select To Date"); $('#<%=txttdate.ClientID%>').focus(); return false; }
                });                   
            </script>  
        </head>
        <body>
            <form id="form1" runat="server">
                <div class="container" style="width: 100%">
                    <div class="row">
                        <asp:Label ID="lblDivision"  runat="server"  Text="Division" class="col-md-2  col-md-offset-3  control-label"></asp:Label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" CssClass="form-control" Style="min-width: 100px; width:150px" 
                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label ID="lblFF" runat="server"  Text="Field Force" class="col-md-2  col-md-offset-3  control-label"></asp:Label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="false"  
                                    Style="min-width: 100px; width:110px" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                    <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                                </asp:DropDownList> 
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="true" OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" 
                                    SkinID="ddlRequired"  CssClass="form-control ddlAlpha" Style="min-width:70px; width:70px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlFieldForce" runat="server"   CssClass="form-control" Style="min-width: 100px; width:350px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="form-control" Style="min-width: 100px;">
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkVacant" Text="Only Vacant Managers" Visible="false" AutoPostBack="true" 
                                    OnCheckedChanged="chkVacant_CheckedChanged" runat="server" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Date" Visible="true" class="col-md-2  col-md-offset-3  control-label"></asp:Label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                
                                <%--<input  id="txtdate" name="txtFrom" type="date" MaxLength="5"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'" 
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1"  class="form-control" style="min-width: 100px; width:150px" />--%>

                                <asp:TextBox ID="txtdate" runat="server" TextMode="Date" autocomplete="off" DataFormatString="{dd/MM/yyyy}" CssClass="form-control" Width="150">
                                </asp:TextBox>
                            
                                <%--<input  id="txttdate" name="txtto" type="date" MaxLength="5"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1"  class="form-control" style="min-width: 100px; width:150px" /> --%>
                                
                                <asp:TextBox ID="txttdate" runat="server" TextMode="Date" autocomplete="off"  DataFormatString="{dd/MM/yyyy}" Width="150" CssClass="form-control">                                
                                </asp:TextBox>
                                
                                <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6  col-md-offset-5">
                            <asp:LinkButton ID="btnSubmit" runat="server" Width="100px" Text="View" class="btn btn-primary btnview" onclick="btnSubmit_Click1" />
                            <button type="button" class="btn btn-primary hide" style="vertical-align: middle; width: 100px" id="btnsubmit">View</button>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td align="left" class="stylespc">
                                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="To Date :" Visible="false"></asp:Label>
                            </td>
                            <td align="left">
                                <%--<input name="txtto" type="date"  CssClass="TEXTAREA" MaxLength="5"
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox" />--%>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblmode" runat="server" SkinID="lblMand" ForeColor="Blue" Font-Size="Medium"  
                                    Font-Bold="true" Font-Names="Calibri" Text="Select the Mode" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server" Font-Names="Calibri"  
                                    RepeatDirection="Horizontal" RepeatColumns="3" Width="550px" Visible="false">
                                    <asp:ListItem Text="Event Captures View" Selected="True"> Event Captures View</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="60%">
                    </asp:Table>       
                </div>
            </form>
        </body>
    </html>
</asp:Content>