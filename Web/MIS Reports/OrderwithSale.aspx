<%@ Page Title="Order With Sale Report" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="OrderwithSale.aspx.cs" Inherits="MIS_Reports_OrderwithSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Order With Sale Report</title>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(subdiv, mon, year, sfcode, sfname) {

            popUpObj = window.open("rptorderwithsale.aspx?subdivision=" + subdiv + "&month=" + mon + "&year=" + year + "&sfcode=" + sfcode + "&sfname=" + sfname,
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
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
            height: 20px;
            width: 28px;
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

 var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
  var subdivision = $('#<%=subdiv.ClientID%> :selected').text();
                if (subdivision == "--Select--") { alert("Select subdivision"); $('#subdiv').focus(); return false; }
                var salesfrce = $('#<%= salesforcelist.ClientID%> :selected').text();
                if (salesfrce == "--Select--") { alert("Select Salesforce"); $('#salesforcelist').focus(); return false; }
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var Dist_Name = $('#<%=salesforcelist.ClientID%> :selected').text();
            var subdiv = $('#<%=subdiv.ClientID%> :selected').val();
            var Dist_Val = $('#<%=salesforcelist.ClientID%> :selected').val();
            window.open("rptorderwithsale.aspx?&subdivision="+subdiv+"&month=" + FMonth + "&year="+FYear+" &sfcode=" + Dist_Val + "&sfname=" + Dist_Name, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0'); 
        }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
   
<%--<table width="95%" cellpadding="0" cellspacing="0" align="center" frame="box">
            <tr>
                <td>
                    <table id="Table2" runat="server" width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                    text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" style="width: 45%">
                                <asp:Label ID="lblHeading" Text="Order With Sale Report" runat="server" CssClass="under" Style="text-transform: capitalize;
                                    font-size: 14px; text-align: center;" ForeColor="#336277" Font-Bold="True" Font-Names="Verdana">
                                </asp:Label>
                            </td>
                            <td align="right" class="style3" style="width: 55%">
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Visible="false" Height="25px" Width="60px"
                                    Text="Back"  />
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>--%>
            <br />

           <center >
        <asp:ScriptManager runat="server" ID="sm">
               </asp:ScriptManager>
                 <asp:updatepanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
           <br />
         
             <table >
               
                   
               
            
               
             <tr>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
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
                        <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Width="60" style="text-align:center"></asp:Label>
                       
                        <asp:DropDownList ID="ddlFYear" runat="server"  SkinID="ddlRequired" CssClass="ddl"
                            Width="60">
                        </asp:DropDownList>
                    </td>
               </tr>
                  <tr><td> <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Select Division"></asp:Label></td><td align="left">
                   <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired"  onselectedindexchanged="subdiv_SelectedIndexChanged"
                       AutoPostBack="true" Width="210px"
                       CssClass="ddl"  >
                   </asp:DropDownList>
               </td></tr>
                  
                     <tr><td> <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Select Sales Executive Officer"></asp:Label></td><td align="left">
                   <asp:DropDownList ID="salesforcelist" runat="server" SkinID="ddlRequired" CssClass="ddl" Width="210px">
                   </asp:DropDownList>
               </td></tr> 
              
               </tr>                   
             </table>
      
             <br />
             <br />
                  <button  ID="btnGo" class="button" runat="server"   onclick="NewWindow().this"  style="vertical-align:middle"><span>View</span></button>
              </ContentTemplate>
              </asp:updatepanel>
        </center>
       
    </div>
    </form>
</body>
</html>
</asp:Content>

