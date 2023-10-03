<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="purchaseview.aspx.cs" Inherits="purchaseview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<%--
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />--%>
      <link type="text/css" rel="stylesheet" href="../css/style1.css" />
  
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
  $('#ctl00_ContentPlaceHolder1_btnGo').click(function () {

 var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
                if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }
                var distri = $('#<%= Distributor.ClientID%> :selected').text();
                if (distri == "--Select--") { alert("Select Distributor"); $('#Distributor').focus(); return false; }

             });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
    </asp:ToolkitScriptManager>

        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
               <div class="container" style="width: 100%">
                        <div class="form-group">
                            <div class="row">

                          
            
                
             <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                                    Division</label>
                         <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>   
                  
                       <asp:DropDownList ID="subdiv" runat="server" AutoPostBack="true"  CssClass="form-control"
                                        Style="min-width: 100px" Width="150"
                           onselectedindexchanged="subdiv_SelectedIndexChanged" SkinID="ddlRequired">
                       </asp:DropDownList>
                </div>
                </div>
                </div>
             
                          <div class="row">
                                <label  id="Label1" runat="server" class="col-md-2 col-md-offset-3  control-label">
                                    Distributor</label>
                                <div class="col-sm-5 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                         <asp:DropDownList ID="Distributor" runat="server" CssClass="form-control"
                                        Style="min-width: 200px; width: 250px"
                             SkinID="ddlRequired">
                             <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                         </asp:DropDownList>
                                    </div>
                                    </div>
                </div>
                  
          <div class="row">
                            <label id="Label3" runat="server" class="col-md-2 col-md-offset-3  control-label">
                                Date</label>
                            <div class="col-sm-5 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input name="TextBox1" id="datee"  type="date"  class="form-control" Style="min-width: 200px; width: 250px" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                        
                     </div>
                        </div>
              
       </div>
     
       
       <div class="row">
                            <div class="col-md-6 col-md-offset-5">   
   <button  ID="btnGo"  runat="server"   OnServerClick="btnGo_Click"  class="btn btn-primary"  style="width: 100px"><span>View</span></button>
   </div>
                        </div>
                         
     </div>
       </div>
      
       
    </div>
     </ContentTemplate></asp:UpdatePanel>
    </form>
</body>
</html>
</asp:Content>

