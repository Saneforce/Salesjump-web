<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Dailyinv_viewNative.aspx.cs" Inherits="MIS_Reports_Dailyinv_viewNative" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Today_Order_View</title>
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

                    var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                    if (FieldForce == "---Select Field Force---") { alert("Select FieldForce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
                    if ($('#txtDate').val() == "") { alert("Please Select Date"); $('#txtDate').focus(); return false; }

                    if (ddlMR != -1 && ddlMR != 0 && ddlMRName != '') {

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
                document.getElementById('txtDate').valueAsDate = new Date();

            });
        </script>
        <style type="text/css">
            input[type='text'], select, label
            {
                line-height: 22px;
                padding: 4px 6px;
                font-size: medium;
                border-radius: 7px;
                width: 100%;
                font-weight: normal;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div id="Divid" runat="server">
                <br />
            </div>
            <div class="form-group">
                <div class="row">
                    <asp:Label ID="lblDivision" Visible="true" runat="server" SkinID="lblMand" Text="Division"
                        CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="subdiv" runat="server" Width="150" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="subdiv_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="false">
                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" Width="500" CssClass="form-control"
                                Style="width: 350px;">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkVacant" Text=" Only Vacant Managers" Style="display: none" OnCheckedChanged="chkVacant_CheckedChanged"
                                runat="server" ForeColor="White" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Date" Visible="true"
                        CssClass="col-md-2  col-md-offset-3 control-label"></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input id="txtDate" name="txtFrom" class="form-control" type="date" maxlength="5"
                                onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="1" style="width: 130px;" />
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
                <div class="row">
                    <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Date" Visible="false"></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" Width="100" Visible="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblmode" runat="server" SkinID="lblMand" ForeColor="Blue" Font-Size="Medium"
                        Font-Bold="true" Font-Names="Calibri" Text="Select the Mode" Visible="false"></asp:Label>
                    <div class="col-md-5 inputGroupContainer">
                        <div class="input-group">
                            <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server" Font-Names="Calibri"
                                RepeatDirection="Horizontal" RepeatColumns="3" Width="550px" Visible="false">
                                <asp:ListItem Text="TPMyDayPlan" Selected="True"> TP MY Day Plan</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <asp:Button ID="btnSubmit" runat="server" Text="View" class="btn btn-primary" OnClick="btnSubmit_Click1" />
                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                            Width="60%">
                        </asp:Table>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </body>
    </html>

</asp:Content>

