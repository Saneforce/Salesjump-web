<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailerwise_distsales.aspx.cs" Inherits="MIS_Reports_Retailerwise_distsales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <style type="text/css">
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
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <script type="text/javascript">
            $(document).ready(function () {
                $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!

                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                var today = dd + '/' + mm + '/' + yyyy;
                $("#<%=txtFromDate.ClientID%>").val(today);
                $("#<%=txtToDate.ClientID%> ").val(today);
            });
        </script>
          <script type="text/javascript">
              function NewWindow() {
                  var FMonth = $('#<%=txtFromDate.ClientID%> ').val();
            if (FMonth == "--Select--") { alert("Select From Month."); $('#<%=txtFromDate.ClientID%>').focus(); return false; }
            var FYear = $('#<%=txtToDate.ClientID%>').val();
            if (FYear == "--Select--") { alert("Select From Year."); $('#<%=txtToDate.ClientID%>').focus(); return false; }
            var ddlFF = $('#<%=ddlstockist.ClientID%> :selected').text();
                  if (ddlFF == "--Select--") { alert("Select Stockist."); $('#<%=ddlstockist.ClientID%>').focus(); return false; }

            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            if (subdiv == "--Select--") { alert("Select Subdivision."); $('#<%=subdiv.ClientID%>').focus(); return false; }


            var ddlFF = $('#<%=ddlstockist.ClientID%> :selected').val();
            var ddlFF_Name = $('#<%=ddlstockist.ClientID%> :selected').text();
            var FMonth = $('#<%=txtFromDate.ClientID%>').val();
            var FYear = $('#<%=txtToDate.ClientID%>').val();

                  window.open("rpt_Retailerwise_distsales.aspx?&Month=" + FMonth + "&Year=" + FYear + "&subdiv=" + subdiv + "&sf_code=" + ddlFF + "&Sf_Name=" + ddlFF_Name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');

              }
        </script>
         <script type="text/javascript">
             function Window() {
                 var FMonth = $('#<%=txtFromDate.ClientID%> ').val();
                  if (FMonth == "--Select--") { alert("Select From Month."); $('#<%=txtFromDate.ClientID%>').focus(); return false; }
                  var FYear = $('#<%=txtToDate.ClientID%>').val();
                  if (FYear == "--Select--") { alert("Select From Year."); $('#<%=txtToDate.ClientID%>').focus(); return false; }
                  var ddlFF = $('#<%=ddlstockist.ClientID%> :selected').text();
                 if (ddlFF == "--Select--") { alert("Select Stockist."); $('#<%=ddlstockist.ClientID%>').focus(); return false; }

                  var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
                  if (subdiv == "--Select--") { alert("Select Subdivision."); $('#<%=subdiv.ClientID%>').focus(); return false; }


                  var ddlFF = $('#<%=ddlstockist.ClientID%> :selected').val();
                  var ddlFF_Name = $('#<%=ddlstockist.ClientID%> :selected').text();
                  var FMonth = $('#<%=txtFromDate.ClientID%>').val();
                  var FYear = $('#<%=txtToDate.ClientID%>').val();

                 window.open("rpt_itemwise_distsales.aspx?&Month=" + FMonth + "&Year=" + FYear + "&subdiv=" + subdiv + "&sf_code=" + ddlFF + "&Sf_Name=" + ddlFF_Name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');

             }
        </script>
        </head>
    <body>
        <form id="form1" runat="server">
            <h3>Distributor Sales</h3>
        <div>
            <div class="container" style="width: 100%">
                <div class="row">
                    <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                        Division</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Style="min-width: 100px;"
                                 AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                        Stockist</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlstockist" runat="server" CssClass="form-control" Style="min-width: 100px;"
                                 AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br/>
                <div class="row">
            <label for="txtFromDate" class="col-md-2 col-md-offset-3 control-label">From</label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group" id="Div1" runat="server">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <input type="text" name="txtFromDate" runat="server" id="txtFromDate" class="form-control datetimepicker"
                        autocomplete="off" />
                </div>
            </div>
        </div>
                <br />
        <div class="row">
            <label for="txtToDate" class="col-md-2 col-md-offset-3 control-label">To</label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group" id="Div2" runat="server">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <input type="text" name="txtToDate" runat="server" id="txtToDate" class="form-control datetimepicker"
                        autocomplete="off" />
                </div>
            </div>
        </div>
                <br />
                <div class="row">
                    <div class="col-md-6  col-md-offset-5">
                        <a id="btnGo" runat="server" onclick="NewWindow().this" class="btn btn-primary btnview"
                            style="width: 100px">
                            <span>RetailerWise</span></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        <a id="A1" runat="server" onclick="Window().this" class="btn btn-primary btnview"
                            style="width: 100px">
                            <span>ItemWise</span></a>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>

