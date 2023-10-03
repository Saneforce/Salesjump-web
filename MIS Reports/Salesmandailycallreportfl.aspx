<%@ Page Title="SalesManDaywise Report" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Salesmandailycallreportfl.aspx.cs" Inherits="Salesmandailycallreportfl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
            <script src="../JsFiles/serial.js" type="text/javascript"></script>
            <script src="../JsFiles/light.js" type="text/javascript"></script>
            <script type="text/javascript">
                function ShowProgress() {
                    setTimeout(function () {
                        var modal = $('<div />');
                        modal.addClass("modal");
                        $('body').append(modal);
                        var loading = $(".loading");
                        loading.show();
                        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                        loading.css({ top: top, left: left });
                    }, 200);
                }

                function NewWindow() {
                    var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                    if (st == "---Select Field Force---") { alert("Select Field Force."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

                    var st = $('#<%=ddlFieldForce.ClientID%> :selected').val();
                    var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text().trim();
                    var str;

                    var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
                    if (subdiv == "--- Select ---") { alert("Select Subdivision."); $('#subdiv').focus(); return false; }

                    if (sf_name == 'admin' || sf_name == 'admin - Admin -') {
                        var str = $('#<%=ddlMR.ClientID%> :selected').val();
                        MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
                        if (str == "---Select Base Level---") { var str = $('#<%=ddlMR.ClientID%> :selected').val(); alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }

                    }
                    else {
                        <%--var str = $('#<%=ddlMR.ClientID%> :selected').text();
                        if (str == "---Select Base Level---") { var str = $('#<%=ddlMR.ClientID%> :selected').val(); alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }--%>

                        str = $('#<%=ddlMR.ClientID%> :selected').text();
                        if (str == "") { alert("Select Base Level."); $('#<%=ddlMR.ClientID%>').focus(); return false; }
                        var str = $('#<%=ddlMR.ClientID%> :selected').val();
                        var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();
                    }

                    var date = $("#txtdate").val();
                    if (date == "") { alert("Select Date"); $('#txtdate').focus(); return false; }
                    var tdate = $("#ttxtdate").val();
                    if (tdate == "") { alert("Select Date"); $('#ttxtdate').focus(); return false; }

                    var DistCode = "";

                    if (str == 0) {
                        window.open("rptsalesmandailycallreportfl.aspx?SF_Code=" + st + "&date=" + date + "&tdate=" + tdate + "&DistCode=" + DistCode + "&subdiv=" + subdiv + "&SF_Name=" + sf_name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
                    }
                    if (str != 0) {
                        window.open("rptsalesmandailycallreportfl.aspx?SF_Code=" + str + "&date=" + date + "&tdate=" + tdate + "&DistCode=" + DistCode + "&subdiv=" + subdiv + "&SF_Name=" + MR_name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
                    }
                }

                $(function () {
                    var dtToday = new Date();

                    var month = dtToday.getMonth() + 1;
                    var day = dtToday.getDate();
                    var year = dtToday.getFullYear();

                    if (month < 10)
                        month = '0' + month.toString();
                    if (day < 10)
                        day = '0' + day.toString();

                    var maxDate = year + '-' + month + '-' + day;

                    $('#ttxtdate').attr('max', maxDate);
					 $('#ttxtdate').val(maxDate);

                    $('#txtdate').attr('max', maxDate);
					$('#txtdate').val(year + '-' + month+'-01');
                });
            </script>
            <style type="text/css">
                .modal {
                    position: fixed;
                    top: 0;
                    left: 0;
                    background-color: black;
                    z-index: 99;
                    opacity: 0.8;
                    filter: alpha(opacity=80);
                    -moz-opacity: 0.8;
                    min-height: 100%;
                    width: 100%;
                }
                .loading {
                    font-family: Arial;
                    font-size: 10pt;
                    border: 5px solid #67CFF5;
                    width: 200px;
                    height: 100px;
                    display: none;
                    position: fixed;
                    background-color: White;
                    z-index: 999;
                }

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
                    margin: 2px;
                    border-radius: 4px;
                    font-family: Andalus;
                    background-image: url('css/download%20(2).png');
                    background-position: 88px;
                    background-position: 88px;
                    background-repeat: no-repeat;
                    text-indent: 0.01px; /*In Firefox*/
                }
                
                .ddl1 {
                    border: 1px solid #1E90FF;
                    margin: 2px;
                    border-radius: 4px;
                    background-position: 88px;
                    background-position: 88px;
                    background-repeat: no-repeat;
                    text-indent: 0.01px;
                }

                .col-sm-6 {
                    padding: 0px 3px 6px 4px;
                }
            </style>
        </head>
        <body>
            <form id="Form2" runat="server">
                <div class="row">
                    <div class="row">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: right;padding: 8px 4px;" Text="Select Division" 
                            CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-sm-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="subdiv" runat="server"  CssClass="form-control"
                                    Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name" Style="text-align: right;padding: 8px 4px;" 
                            CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-sm-6 inputGroupContainer">
                            <div class="input-group" id="kk" runat="server">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true"  
                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    SkinID="ddlRequired" CssClass="form-control" Style="font-size: 14px;" Width="500">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" 
                            Style="text-align: right; padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-sm-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" 
                                    CssClass="form-control" Style="font-size: 14px;" Width="400">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="FromDate" Style="text-align: right; padding: 8px 4px;" 
                            CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-sm-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <input id="txtdate" name="txtFrom" type="date" class="TEXTAREA form-control" onfocus="this.style.backgroundColor='#E0EE9D'"
                                    onblur="this.style.backgroundColor='White'" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                                    tabindex="1" skinid="MandTxtBox" style="font-size: medium" />
                            </div>
                        </div>
                    </div>  
                    
                    <div class="row">
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="ToDate"  Style="text-align: right; padding: 8px 4px;"
                            CssClass="col-md-4 control-label"></asp:Label>
                        <div class="col-sm-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <input id="ttxtdate" name="txtFrom" type="date" class="TEXTAREA form-control"  
                                    onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"  
                                    onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="1"  
                                    skinid="MandTxtBox" style="font-size: medium" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12" style="text-align: center">                 

						<button id="btnView" class="btn btn-primary" runat="server" onclick="NewWindow().this" style="vertical-align: middle">
                            <span>View</span></button>
                        </div>
                    </div>

                </div>

            </form>
        </body>
    </html>
</asp:Content>

