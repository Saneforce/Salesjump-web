<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="dailycallreport.aspx.cs"
    Inherits="MIS_Reports_dailycallreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head id="Head1">
            <title>DailyCallReport</title>
            <link type="text/css" rel="stylesheet" href="../css/style1.css" />
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <style type="text/css">
                .button {
                    display: inline-block;
                    border-radius: 4px;
                    background-color: #6495ED;
                    border: none;
                    color: #FFFFFF;
                    text-align: center;
                    font-bold: true;
                    width: 75px;
                    height: 29px;
                    transition: all 0.5s;
                    cursor: pointer;
                    margin: 5px;
                }
                
                .button span {
                    cursor: pointer;
                    display: inline-block;
                    position: relative;
                    transition: 0.5s;
                    top: 0px;
                    left: 0px;
                    height: 20px;
                    width: 28px;
                }
                
                .button span:after {
                    content: '»';
                    position: absolute;
                    opacity: 0;
                    top: 0;
                    right: -20px;
                    transition: 0.5s;
                }
                
                .button:hover span {
                    padding-right: 25px;
                }
                
                .button:hover span:after {
                    opacity: 1;
                    right: 0;
                }

                .ddl {
                    border: 1px solid #1E90FF;
                    border-radius: 4px;
                    margin: 2px;
                    font-family: Andalus;
                    background-image: url('css/download%20(2).png');
                    background-position: 88px;
                    background-position: 88px;
                    background-repeat: no-repeat;
                    text-indent: 0.01px; /*In Firefox*/
                }

                .ddl1 {
                    border: 1px solid #1E90FF;
                    border-radius: 4px;
                    margin: 2px;
                    background-position: 88px;
                    background-position: 88px;
                    background-repeat: no-repeat;
                    text-indent: 0.01px; /*In Firefox*/
                }
            </style>
            <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
            <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
            <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>   
            <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#<%=salesforcelist.ClientID%>').chosen();

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

                    var date = $("#txtdate").val();
                    if (date == "")
                        document.getElementById('txtdate').valueAsDate = new Date();
                });
       
                function NewWindow() {

                    var viewState = document.getElementById("txtdate").value;
                    $('#<%=HiddenField1.ClientID %>').val(viewState);


                    var date = $("#txtdate").val();
                    if (date == "") { alert("Select Date"); $('#txtdate').focus(); return false; }

                    var fieldforce = $('#<%=salesforcelist.ClientID%> :selected').text();
                    if (fieldforce == "--Select--") { alert("Select Sales Executive Officer	"); $('#salesforcelist').focus(); return false; }


                    var sfname = $('#<%=salesforcelist.ClientID%> :selected').text();
                    var sfcode = $('#<%=salesforcelist.ClientID%> :selected').val();
                    var DistCode = ""; var subdiv = "0";
                    if (<%=div_code%>== '98') {
                        window.open("rpt_Visit_OutLets_View.aspx?sf_code=" + sfcode +
                            "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sfname + "&Date=" + viewState + "&Type=" + <%=sf_type%> + "&subdiv=" + subdiv,
                            "ModalPopUp",
                            "toolbar=no," +
                            "scrollbars=yes," +
                            "location=no," +
                            "statusbar=no," +
                            "menubar=no," +
                            "addressbar=no," +
                            "resizable=yes," +
                            "width=900," +
                            "height=600," +
                            "left = 0," +
                            "top=0"
                        );
                    }
                    else {
                        window.open("rptdailycallreport.aspx?&DATE=" + viewState + "&sfcode=" + sfcode + "&sfname=" + sfname + "&DistCode=" + DistCode,
                            "ModalPopUp",
                            "toolbar=no," +
                            "scrollbars=yes," +
                            "location=no," +
                            "statusbar=no," +
                            "menubar=no," +
                            "addressbar=no," +
                            "resizable=yes," +
                            "width=900," +
                            "height=600," +
                            "left = 0," +
                            "top=0"
                        );
                    }
                    __doPostBack("<%=UniqueID%>");
                }

                $(function () {
                    var viewState = $('#<%=HiddenField1.ClientID %>').val();
                    $("#txtdate").val(viewState);

                });
            </script>
        </head>
        <body>
            <form id="form1" runat="server">
                <div>
                    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                    <table width="95%" cellpadding="0" cellspacing="0" align="center" frame="box">
                        <tr>
                            <td>
                                <table id="Table2" runat="server" width="100%">
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" 
                                                Style="font-size: 13px; text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 45%">
                                            <asp:Label ID="lblHeading" Text="SalesMan Call Tracking(Daily)" runat="server" CssClass="under"
                                                Style="text-transform: capitalize; font-size: 14px; text-align: center;" ForeColor="#336277"
                                                Font-Bold="True" Font-Names="Verdana">
                                            </asp:Label>
                                        </td>
                                        <td align="right" class="style3" style="width: 55%">
                                            <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Visible="false" Height="25px"  Width="60px" Text="Back" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="container" style="width: 100%">
                        <div class="row">
                            <label id="Label2" class="col-md-2  col-md-offset-3  control-label">Date</label>
                            <div class="col-md-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <input id="txtdate" name="txtFrom" type="date" class="form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                        tabindex="1" skinid="MandTxtBox" />
                                </div>
                            </div>
                        </div>
                        <br />                
                        <div class="row">
                            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">Field Force</label>
                            <div class="col-md-4 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="salesforcelist" runat="server" CssClass="form-control chosen" Width="300px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />       
                        <div class="row">
                            <div class="col-md-6  col-md-offset-5">
                                <button id="btnGo" class="btn btn-primary btnview" style="width: 100px;" onclick="NewWindow().this">
                                    <span>View</span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </body>
    </html>
</asp:Content>
