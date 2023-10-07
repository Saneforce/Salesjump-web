<%@ Page Title="TP - Deviation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master"
    CodeFile="TP_Deviation.aspx.cs" Inherits="MIS_Reports_TP_Deviation" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>TP - Deviation</title>
        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
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
            .auto-style1 {
                width: 100%;
                margin-left: auto;
                margin-right: auto;
                padding-left: 15px;
                padding-right: 15px;
            }
        </style>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

        <script type="text/javascript">
            var popUpObj;
            function showModalPopUp(sfcode, Fromdate, Todate, sf_name) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rptTP_Deviation.aspx?sfcode=" + sfcode + "&FDate=" + Fromdate + "&TDate=" + Todate + "&sf_name=" + sf_name,
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
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            $('form').live("submit", function () {
                ShowProgress();
            });
        </script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
                $('.btnview').click(function () {                   
                    var FromDt = $("#<%=txtFromDate.ClientID%>").val();
                    var ToDt = $("#<%=txtToDate.ClientID%> ").val();
                    var sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                    var sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                    var mode = '';
                    if (sf_code == '0') {
                        alert('Select manager..!');
                        $("#<%=ddlFieldForce.ClientID%>").focus();
                        return false;
                    }


                    if ($('#mgrOnly').is(":checked")) {

                      sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                        sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                        mode = "1";
                     if (sf_code == '0') {
                        alert('Select manager..!');
                        $("#<%=ddlFieldForce.ClientID%>").focus();
                        return false;
                    }
                }
                else {
                    sf_code = $("#<%=ddlMR.ClientID%> ").val();
                    sf_Name = $("#<%=ddlMR.ClientID%> :selected").text();
                        mode = "0";
                        if (sf_code == '0' || sf_code == '') {
                            sf_code = $("#<%=ddlFieldForce.ClientID%> ").val();
                            sf_Name = $("#<%=ddlFieldForce.ClientID%> :selected").text();
                          }
                    }
                    
                    url = 'rptTP_Deviation.aspx?&FDate=' + FromDt + '&TDate=' + ToDt + '&sfcode=' + sf_code + '&sf_name=' + sf_Name + '&modes=' + mode;
                    window.open(url, 'SOB_POB', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');

                });
            }); 
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1;

                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }

                var today = mm + '/' + dd + '/' + yyyy;
                $('.datetimepicker').datepicker({ dateFormat: 'mm/dd/yy' });
                $('.datetimepicker').val(today);
            });
        </script>
        <script type="text/javascript">
            $(function () {
                var $txt = $('input[id$=txtNew]');
                var $ddl = $('select[id$=ddlFieldForce]');
                var $items = $('select[id$=ddlFieldForce] option');

                $txt.keyup(function () {
                    searchDdl($txt.val());
                });

                function searchDdl(item) {
                    $ddl.empty();
                    var exp = new RegExp(item, "i");
                    var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                    if (arr.length > 0) {
                        countItemsFound(arr.length);
                        $.each(arr, function () {
                            $ddl.append(this);
                            $ddl.get(0).selectedIndex = 0;
                        }
                    );
                    }
                    else {
                        countItemsFound(arr.length);
                        $ddl.append("<option>No Items Found</option>");
                    }
                }

                function countItemsFound(num) {
                    $("#para").empty();
                    if ($txt.val().length) {
                        $("#para").html(num + " items found");
                    }

                }
            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <br />
            <div class="auto-style1">
			     <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Manager</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        Manager Only
                    <input type="checkbox" id="mgrOnly" />
                    </div>
                </div>
            </div>
                <div class="row">
                  <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                            Field Force</label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlMR" runat="server" Width="300px" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                </asp:DropDownList>
                           </div>
                </div>
               </div>
    
                </div>
                <%--<div class="row">
                    <label for="txtMonth" class="col-md-2  col-md-offset-3  control-label">
                        Month</label>
                    <div class="col-md-2 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                Style="min-width: 100px; width: 150px">
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
                    <label for="txtYear" class="col-md-2 col-md-offset-3  control-label">
                        Year</label>
                    <div class="col-md-2 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                Style="min-width: 100px; width: 150px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <label for="txtFromDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                        From Date</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group" id="Div1" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <%-- <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datepicker"
                            style="min-width: 110px; width: 120px;" />--%>
                            <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker" autocomplete="off" style="min-width: 110px; width: 150px;" />

                        </div>
                    </div>
                </div>
                <div class="row">
                    <label for="txtToDate" class="col-sm-2 col-md-offset-3 control-label" style="font-weight: normal">
                        To Date</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group" id="Div2" runat="server">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input type="text" name="txtToDate" runat="server" id="txtToDate" class="form-control datetimepicker"
                                style="min-width: 110px; width: 150px;" autocomplete="off" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6  col-md-offset-5">
                        <a name="btnGo" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>
                    </div>
                </div>
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
