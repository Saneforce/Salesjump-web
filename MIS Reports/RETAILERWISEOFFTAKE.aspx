<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RETAILERWISEOFFTAKE.aspx.cs" Inherits="MIS_Reports_RETAILERWISEOFFTAKE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Retailer Potential</title>
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
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
    </script>--%>
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

        }); 
    </script>
  <script type="text/javascript">
      function NewWindow() {


          var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
          if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }

          var fieldforce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
          if (fieldforce == "--Select--") { alert("Select Fieldforce"); $('#ddlFieldForce').focus(); return false; }





          var fieldforcecode = $('#<%=ddlFieldForce.ClientID%> :selected').val();


          window.open("Rptretailerwiseofftake.aspx?&subdivision=" + subdivision + "&feild_code=" + fieldforcecode + "&feild_name=" + fieldforce, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');

      }
      $(function () {

          var viewState = $('#<%=HiddenField1.ClientID %>').val();
          $("#text").val(viewState);

      });
</script>
</head>
<body>
    <form id="form1" runat="server">    
<asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
    <div class="container" style="width:100%">
        <div class="row">
            <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                Division</label>
            <div class="col-sm-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                        Style=" min-width:100px;"  OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                Field Force</label>
            <div class="col-md-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>
					 <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                </div>
            </div>
        </div>

  <div class="row">
            <div class="col-md-6  col-md-offset-5">

                        <button id="btnGo" class="btn btn-primary btnview" runat="server" onclick="NewWindow().this" style="vertical-align: middle;width:100px">
                            <span>View</span></button>            
            
            </div>
        </div>
    </div>
        </form>
    </body>
    </html>
</asp:Content>

