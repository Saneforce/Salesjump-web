<%@ Page Title="Purchase Trend Analysis" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Trend_Analysis.aspx.cs" Inherits="MIS_Reports_Trend_Analysis" %>
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Purchase Trend Analysis</title> 
      <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(fmon, subdiv, viewby, fyr) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_distributor_trend.aspx?FMonth=" + fmon + "&Subdivision=" + subdiv + "&viewby=" + viewby + "&FYear=" + fyr,
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
             $('#ctl00_ContentPlaceHolder1_btnGo').click(function () {

                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
               var viewby = $('#<%=viewdrop.ClientID%> :selected').text();
                if (viewby == "--- Select ---") { alert("Select viewby"); $('#viewdrop').focus(); return false; }
                  var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
                if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }
                




            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--  <ucl:menu ID="menu1" runat="server" />--%>

           

          
        
           <div class="container" style="width: 100%">
                    <div class="form-group">
                        <div class="row">
                          <label id="lblFMonth" class="col-md-1 col-md-offset-4 control-label">
                                From</label>
                       <%-- <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="From Month"></asp:Label>--%>
                    <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                 <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired"  CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
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
                        
                       
                        <asp:DropDownList ID="ddlFYear" runat="server"  SkinID="ddlRequired"  CssClass="form-control"
                                        Style="min-width: 100px" Width="100">
                        </asp:DropDownList>
                        </div>
                    </div>
                        </div>
                       <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label2" class="col-md-1 col-md-offset-4  control-label">
                                Division</label>
             <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                   <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired"  AutoPostBack="true" 
                       CssClass="form-control"
                                        Style="min-width: 100px" Width="130" >
                   </asp:DropDownList>
                 </div>
                            </div>
                        </div>
                   <div class="row">
                            <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                            <label id="Label1" class="col-md-1 col-md-offset-4  control-label">
                                View By</label>
                       
                                  <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="viewdrop" runat="server" AutoPostBack="true" SkinID="ddlRequired"    CssClass="form-control"
                                        Style="min-width: 100px" Width="130">
                             <asp:ListItem Value="0" Text="--- Select ---" Selected="True"></asp:ListItem>
                               <asp:ListItem Value="1" Text="Statewise"></asp:ListItem>
                            <asp:ListItem Value="2" Text="DistributorWise"></asp:ListItem>
                            <asp:ListItem Value="3" Text="BrandWise" ></asp:ListItem>
                              <asp:ListItem Value="4" Text="CategoryWise" ></asp:ListItem>
                                <asp:ListItem Value="5" Text="ProductWise" ></asp:ListItem>

                        </asp:DropDownList>
               
                 </div>
</div>
</div>
 <div class="row">
                            <div class="col-md-6 col-md-offset-5">    
               <button  ID="btnGo" runat="server"   OnServerClick="btnGo_Click"     class="btn btn-primary"    style="width: 100px"><span>View</span></button>
                 </div>
                        </div>

</div>
</div>
       

    </div>
    </form>
</body>
</html>
</asp:Content>