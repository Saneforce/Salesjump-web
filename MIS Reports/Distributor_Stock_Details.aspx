<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Distributor_Stock_Details.aspx.cs" Inherits="MIS_Reports_Distributor_Stock_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Distributor_Stock_Details</title>
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
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
        var FDT = "";
        var TDT = "";
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
            $('#btnGo').click(function () {
                
                var FDT = $('#fdatee').val();
                if (FDT == "") { alert("Select From Date."); $('#fdatee').focus(); return false; }
                var TDT = $('#tdatee').val();
                if (TDT == "") { alert("Select To Date."); $('#tdatee').focus(); return false; }
                



            });
        }); 
    </script>
    <script type="text/javascript">
        function NewWindow() {
           
            var FDT = $('#fdatee').val();
            if (FDT == "") { alert("Select From Date."); $('#fdatee').focus(); return false; }
            //var TDT = $('#tdatee').val();
            //if (TDT == "") { alert("Select To Date."); $('#tdatee').focus(); return false; }

            var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
            if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }

            var fieldforce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (fieldforce == "--Select--") { alert("Select Fieldforce"); $('#ddlFieldForce').focus(); return false; }

            var sfCode=   $('#<%=ddlFieldForce.ClientID%>').val();

            var distri = $('#<%=Distributor.ClientID%> :selected').text();
            if (distri == "--Select--") { alert("Select Distributor"); $('#Distributor').focus(); return false; }

             var FDT = $('#fdatee').val();        
             //var TDT = $('#tdatee').val();
             var TDT = FDT;
            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            var Dist_Val = $('#<%=Distributor.ClientID%> :selected').val();

            window.open("Rpt_Distributor_stock_details.aspx?fdate=" + FDT + "&tdate=" + TDT + "&dist_code=" + Dist_Val + "&Distri_name=" + distri + "&SFCode=" + sfCode, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
        }
    </script>
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width:100%;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%-- <ucl:menu ID="menu1" runat="server" />--%>
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="container" style="width: 100%">
                    <div class="col-lg-12 sub-header">
            Distributor Stock Details <span style="float: right; margin-top: -20px;">               
                        </span>                   
                   </div>
                    <div class="form-group" style="margin-top:5%" >                    
                        <div class="row">
                            
                            <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                                Division</label>
                            <div class="col-sm-5 inputGroupContainer" >
                                <div class="input-group" style="min-width:25px">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 75px" Width="75px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="Label1" class="col-md-2 col-md-offset-3  control-label">
                                Field Force</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                        CssClass="form-control" Style="min-width: 200px" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="seldist" runat="server" class="col-md-2 col-md-offset-3  control-label">
                                Distributor</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="Distributor" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 200px; width: 250px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label id="Label3" class="col-md-2 col-md-offset-3 control-label">
                              From</label>
                                   <div class="col-sm-5 inputGroupContainer"style="width:15%">
                                    <div class="input-group">
                                   <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                 <input name="TextBox1" id="fdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                               </div>
                      </div>

                 </div>
                       <%--<div class="row">
                             <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
                              To</label>
                                <div class="col-sm-5 inputGroupContainer"style="width:15%">
                                  <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                  <input name="TextBox1" id="tdatee" type="date" class="form-control" style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                              </div>
                       </div>
                      </div>--%>
                        <div class="row">
                            <div class="col-md-6 col-md-offset-5">
                                <button id="Button1" runat="server" onclick="NewWindow()" class="btn btn-primary"
                                    style="width: 100px">
                                    <span>View</span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
</asp:Content>

