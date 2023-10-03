<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Attendance_Rpt_1.aspx.cs" Inherits="MIS_Reports_Attendance_Rpt_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Attendance View</title>
    
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <%--<script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, prod) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptPurchas_Register_Distributor_wise.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&Prod=" + prod,
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
            popUpObj.focus();
            //LoadModalDiv();
        }


   
    </script>--%>
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
        var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
        if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
        var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
        if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }

        var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
        if (subdiv == "--- Select ---") { alert("Select Subdivision."); $('#subdiv').focus(); return false; }

        var type =' WorkTypewise';
        var statc = $('#<%=ddlstate.ClientID%> :selected').val();
        var statv = $('#<%=ddlstate.ClientID%> :selected').text();
        if (statv == "--Select--") { alert("Select State"); $('#ddlstate').focus(); return false; }
        var FO = $('#<%=ddlFieldForce.ClientID%> :selected').text();
        if (FO == "--Select--") { alert("Select salesforce"); $('#salesforcelist').focus(); return false; }




        var sfcode = $('#<%=ddlFieldForce.ClientID%> :selected').val();
        var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
        var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();

        var imgpth = $('#ctl00_ContentPlaceHolder1_lblpath').text();

        
            window.open("rptattendancewrktype_1.aspx?&FMonth=" + FMonth + "&FYear=" + FYear + "&subdiv=" + subdiv + "&sfCode=" + sfcode + "&sfname=" + FO + "&stcode=" + statc + "&stval=" + statv + "&type=" + type + "&imgpath=" + imgpth, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
      
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>


     
    <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
            <br />
            <div class="container" style="width:100%">
      
               <div class="row">
            <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                Division</label>
                  <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                        <asp:DropDownList ID="subdiv" runat="server" OnSelectedIndexChanged="subdiv_SelectedIndexChanged" CssClass="form-control"  Width="200px"
                       AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
</div>
</div>
 <div class="row">
                            <label id="st" class="col-md-2  col-md-offset-3  control-label">
                                State</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectIndexchanged" AutoPostBack="true"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>

			 <div class="row">
            <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                Field Force</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>

                   <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control"  AutoPostBack="true"
                       Width="360px" >   </asp:DropDownList>
               
                    </div>
</div>
</div>

				<%--<div class="row">
            <label id="Label3" class="col-md-2  col-md-offset-3  control-label" Visible="false">
                Type</label>
                  <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>


                 
                   <asp:DropDownList ID="view" runat="server" CssClass="form-control" Visible="false"  Width="200px" >
                          <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Maximised"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Minimised"></asp:ListItem>
  <asp:ListItem Value="3" Text="Status"></asp:ListItem>
 <asp:ListItem Value="4" Text="Typewise"></asp:ListItem>

  <asp:ListItem Value="5" Text="WorkTypewise"></asp:ListItem>

                   </asp:DropDownList>


                                  </div>
</div>
</div>--%>
  <div class="row">
            <label id="lblFMonth" class="col-md-2  col-md-offset-3  control-label">
                Month</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span> 
                        <asp:DropDownList ID="ddlFMonth" runat="server"  CssClass="form-control"  Width="100">
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
            <label id="lblFYear" class="col-md-2  col-md-offset-3  control-label">
                Year</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>                         
                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control"  Width="100">
                        </asp:DropDownList>
                   </div>
</div>
</div>

   <div class="row">
            <div class="col-md-6  col-md-offset-5">

          
           
            <button id="btnGo"  runat="server" onclick="NewWindow().this" class="btn btn-primary btnview" style="width: 100px">
                <span>View</span></button>
<asp:Label ID="lblpath" runat="server" ForeColor="#f1f2f7"  ></asp:Label>
 </div>
                 </div>
                    </div>

 </ContentTemplate>
              </asp:updatepanel>
       
    </div>
    </form>
</body>
</html>
</asp:Content>

