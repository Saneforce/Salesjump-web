<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Van_stock_ledger.aspx.cs" Inherits="MIS_Reports_Van_stock_ledger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
   
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    
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
       
.button {
  display: inline-block;
  border-radius: 4px;
  background-color: #6495ED;
  border: none;
  color: #FFFFFF;
  text-align: center;  
  font-bold:true;
  width: 75px;
  height:29px;
  transition: all 0.5s;
  cursor: pointer;
  margin: 5px;
}

.button span {
  cursor: pointer;
  display: inline-block;
  position: relative;
  transition: 0.5s;
            top: 0px;
            left: 0px;
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


    .ddl
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
             font-family:Andalus;         
          background-image:url('css/download%20(2).png');
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
         .ddl1
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        
  
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
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
          var fieldforceval = $('#<%=ddlFieldForce.ClientID%> :selected').val();
         
        

          <%-- %>var Product = $('#<%=product.ClientID%> :selected').text();
         if (Product == "--Select--") { alert("Select product"); $('#product').focus(); return false; }
         var productval = $('#<%=product.ClientID%> :selected').val();--%>



          var viewState = document.getElementById("text").value;
          $('#<%=HiddenField1.ClientID %>').val(viewState);


          var date = $("#text").val();
          var date1 = $("#Date1").val();

          if (date == "") { alert("Select Form Date"); $('#text').focus(); __doPostBack("<%=UniqueID%>"); return false; }
          if (date1 == "") { alert("Select To Date"); $('#text').focus(); __doPostBack("<%=UniqueID%>"); return false; }


        
          var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
        
          __doPostBack("<%=UniqueID%>");
          window.open("rpt_van_stock_ledger.aspx?&DATE=" + date + "&TODATE=" + date1 + " &subdivision=" + subdiv + "&fieldforceval= " + fieldforceval + "&Feildforce=" + fieldforce, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');

      }
      $(function () {

          var viewState = $('#<%=HiddenField1.ClientID %>').val();
          $("#text").val(viewState);
          $("#Date1").val(viewState);

      });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
      <%--- <ucl:menu ID="menu1" runat="server" />--%>

            <br />

           <center >
 <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
        
           <br />
           <%--<asp:UpdatePanel ID="UpdatePanelCalendar"  UpdateMode="Conditional" runat="server">
    <ContentTemplate>--%>
             <table >
               
           
             
               <%--<tr>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="From Month"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="ddl">
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
                        <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="From Year" Width="60"></asp:Label>
                       
                        <asp:DropDownList ID="ddlFYear" runat="server"  SkinID="ddlRequired" CssClass="ddl"
                            Width="60">
                        </asp:DropDownList>
                    </td>
               </tr>--%>
               <%--<tr>
                   <td align="left" class="stylespc">
                        <asp:Label ID="lblTMonth" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired" 
                            CssClass="ddl" Height="24px" Width="81px">
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
                        <asp:Label ID="lblTYear" runat="server" SkinID="lblMand" Text="To Year" Width="60"></asp:Label>
                       
                        <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired" CssClass="ddl"
                            Width="60">
                        </asp:DropDownList>
                    </td>
               </tr>--%>
               <tr><td> <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Select Division"></asp:Label></td><td align="left">
                   <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired"  
                       AutoPostBack="true" Width="210px"
                       CssClass="ddl" onselectedindexchanged="subdiv_SelectedIndexChanged" >
                   </asp:DropDownList>
               </td></tr>
                <tr><td> <asp:Label ID="Label5" runat="server" SkinID="lblMand" Text="Select Fieldforce"></asp:Label></td><td align="left">
                   <asp:DropDownList ID="ddlFieldForce" runat="server"  SkinID="ddlRequired"   AutoPostBack="true"   CssClass="ddl" Width="210px"
                        onselectedindexchanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
               </td></tr>
                
                       <%--<tr><td> <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Select Product Name"></asp:Label></td><td align="left">
                   <asp:DropDownList ID="product" runat="server" SkinID="ddlRequired" CssClass="ddl" Width="210px">
                   </asp:DropDownList>
               </td></tr>      --%>
              
               <td><asp:Label ID="Label4" runat="server" SkinID="lblMand" Text="Select From Date"></asp:Label></td>
 <td> <input id="text" name="TextBox1" type="date" class="ddl" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />
                                </td>
                            </tr>
                             <tr >
              
               <td><asp:Label ID="Label7" runat="server" SkinID="lblMand" Text="Select To Date"></asp:Label></td>
 <td> <input id="Date1" name="TextBox1" type="date" class="ddl" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}" />

                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <button id="btnGo" class="button" runat="server" onclick="NewWindow().this" style="vertical-align: middle">
                            <span>View</span></button>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
            </Triggers>
          </ContentTemplate>
</asp:UpdatePanel>--%>
        </div>
        </form>
    </body>
    </html>
</asp:Content>

