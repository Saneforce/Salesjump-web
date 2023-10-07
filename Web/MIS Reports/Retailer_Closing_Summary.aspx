<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailer_Closing_Summary.aspx.cs" Inherits="MIS_Reports_Retailer_Closing_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Retailer Closing Summary</title>
        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <style type="text/css">
            input[type='text'], select, label {
                line-height: 22px;
                padding: 4px 6px;
                font-size: medium;
                border-radius: 7px;
                width: 100%;
                font-weight: normal;
            }
        </style>
        <script type="text/javascript">
            var popUpObj;
            function showModalPopUp(sfcode, subdiv_code,fmon, fyr, sf_name, mname, yname) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rpt_Retailer_Closing_Summary.aspx?sf_code=" + sfcode + "&subdiv=" + subdiv_code + "&Fmonth=" + fmon + "&Fyear=" + fyr + "&Sf_Name=" + sf_name + "&Mname=" + mname + "&Yname=" + yname,
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

                var divCode = 0; $('.btnexcel').hide();                

                divCode = '<%= Session["div_code"] %>';

                //if (divCode == "159" || divCode == "160" || divCode == "173" || divCode == "178") { $('.btnexcel').show(); }
                //else { $('.btnexcel').hide(); }

                if (divCode == "159") { $('.btnexcel').show(); }
                else if (divCode == "160") { $('.btnexcel').show(); }
                else if (divCode == "173") { $('.btnexcel').show(); }
                else if (divCode == "178") { $('.btnexcel').show(); }
                else if (divCode == "186") { $('.btnexcel').show(); }
                else if (divCode == "187") { $('.btnexcel').show(); }
                else { $('.btnexcel').hide(); }

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
                    var subdiv = $('#<%=subdiv.ClientID%> :selected').text();
                    if (subdiv == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }                
                    var SName = $('#<%=ddlFieldForce.ClientID%> :selecte d').text();
                    if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                    var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                    if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth')  .focus(); return false; }
                    var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                    if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                    var subdiv_code =  document.getElementById('<%=subdiv.ClientID%>').value;
                    var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                    var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                    var sf_name = document.getElementById('<%=ddlFieldForce.ClientID%>').text;
                    var month2 = $('#<%=ddlMonth.ClientID%> :selected').text();
                    var Year2 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    showModalPopUp(sfcode,subdiv_code, Month1, Year1, SName, month2, Year2);


                });
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

            function chkinp() {
                var valid = true;
              
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var sf_name = document.getElementById('<%=ddlFieldForce.ClientID%>').text;
                var month2 = $('#<%=ddlMonth.ClientID%> :selected').text();
                var Year2 = document.getElementById('<%=ddlYear.ClientID%>').value;

                
                return valid;
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div id="Divid" runat="server">
                    <div class="row">
                        <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                            Division</label>
                        <div class="col-sm-5 inputGroupContainer" style="width: 28%;" >
                            <div class="input-group" style="min-width: 25px">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                    Style="min-width: 75px" Width="75px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="container" style="width: 100%">
                    
                    <div class="row">
                        <div align="left" class="stylespc"  width="120px">
                            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                                Field Force</label></div>
                            <div class="col-md-5 inputGroupContainer" style="width: 28%;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" Width="300px" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                </div>
                            </div>
                    </div>
                   
                    <div class="row">
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
                    </div>
                    <div class="row" style="margin-top: 5px">
                        <div class="col-md-2  col-md-offset-5">
                            <a name="btnGo" type="button" class="btn btn-primary btnview" style="width: auto">View</a>
                        </div>
                        <div class="col-md-3  btnexcel">
                            <asp:Button runat="server" ID="exceldld" CssClass="btn btn-primary"  ForeColor="White" Text="Excel" 
                                OnClientClick="javascript: return chkinp()" OnClick="exceldld_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6  col-md-offset-5">
                            <%--<a name="btnGo" type="button" class="btn btn-primary btnview" style="width: 100px">View</a>--%>
                        </div>
                    </div> 

                </div>
                </div>
                <%-- <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>--%>
            
        </form>
    </body>
    </html>
</asp:Content>

