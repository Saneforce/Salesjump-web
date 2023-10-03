<%@ Page Title="Lost-Products Details" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Lost_Purchase.aspx.cs" Inherits="MIS_Reports_Lost_Purchase" %>
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Lost-Products Details</title>
   
    <link type="text/css" rel="stylesheet" href="../css/Style1.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, subdiv) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_lost_purchase_prac.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&Subdivision=" + subdiv,
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
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }
   var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
                if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }


              var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
              var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
              var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').val();
              var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();

              var subdiv = $('#<%=subdiv.ClientID%> :selected').val();

              window.open("rpt_purchase_lost_selmonth.aspx?&FMonth=" + FMonth + "&FYear=" + FYear + "&subdivision=" + subdiv + "&TMonth=" + TMonth + "&TYear=" + TYear, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
          }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <%-- <ucl:menu ID="<a href="../fonts/">../fonts/</a>menu1" runat="server" />--%>

            

         
            <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
           
          <div class="container" style="width: 100%">
                        <div class="form-group">
                            <div class="row">
                        <label id="lblFMonth" class="col-md-2 col-md-offset-3 control-label">
                                    From</label>
                    <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
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
                        <%--<asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Width="60"></asp:Label>--%>
                       
                        <asp:DropDownList ID="ddlFYear" runat="server"  SkinID="ddlRequired" CssClass="form-control"
                                            Style="min-width: 100px" Width="100">
                            
                        </asp:DropDownList>
                        </div>
                                </div>
                                </div>
          <div class="row">
                             
                                <label id="Label4" class="col-md-2 col-md-offset-3  control-label">
                                    To</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                       <%-- <asp:Label ID="lblTMonth" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>--%>
                  
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
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
                       <%-- <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text=" Year" Width="60"></asp:Label>
                       --%>
                        <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                            Style="min-width: 100px" Width="100">
                        </asp:DropDownList>
                       </div>
                                </div>
                            </div>
                      <div class="row">
                               

                                <label id="Label6" class="col-md-2 col-md-offset-3  control-label">
                                    Division</label>
                                <div class="col-sm-6 inputGroupContainer">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span> 
                                       <%-- <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Select Division"></asp:Label></td><td align="left">--%>
                   <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired"  
                       AutoPostBack="true" CssClass="form-control"
                                            Style="min-width: 100px" Width="100"  >
                   </asp:DropDownList>
                  
                                    </div>
                                </div>
                            </div>
                                       
             <div class="row">
                            <div class="col-md-6 col-md-offset-5"> 
                <button  ID="btnGo"  runat="server"    onclick="NewWindow().this"  class="btn btn-primary" style="width: 100px"><span>View</span></button>
                 </div>
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