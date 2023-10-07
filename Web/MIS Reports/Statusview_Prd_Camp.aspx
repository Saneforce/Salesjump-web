<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statusview_Prd_Camp.aspx.cs" Inherits="MIS_Reports_Statusview_Prd_Camp" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Status View</title>
<link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
       <link type="text/css" rel="stylesheet" href="../../css/style.css" />
        <style>
  
        .NoRecord
        {
            font-size:10pt;
            font-weight:bold;
            color:Black;
            background-color:White;          
                       
        }
        
    </style>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, prod) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptsampleproduct_details.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&Prod=" + prod,
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
            $('#btnGo').click(function () {

                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }

             


            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <ucl:Menu ID="menu1" runat="server" />
       <br />
         <center >
           <table >

              <tr>
                   <td align="left" class="lblSpace">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                  <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true" 
                            >
                        </asp:DropDownList>
                    </td>
                </tr>

             <tr>
                <td align="left" class="stylespc" width="120px">
                    <asp:Label ID="lblmode" runat="server" SkinID="lblMand" Text="Select the Mode"></asp:Label>
                </td>

                <td align="left">
                     <asp:DropDownList ID="ddlmode" runat="server"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Listed Customer - Product Map"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Listed Customer - Campaign Map" ></asp:ListItem>
                     </asp:DropDownList>
                </td>            
             </tr>
             <tr>
                   <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Fieldforce Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" >
                            
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
               </tr>

           </table>
           <br />
            <asp:Button ID="btnGo" runat="server" Width="70px" CssClass="btnnew" Height="25px" Text="View" OnClick="btnGo_Click" />

          <%--  <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="BUTTON"
                OnClick="btnGo_Click" />--%>

         </center>
         <br />
         <br />
          <center>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="75%">
            </asp:Table>
            <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>
        </center>
        <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
       
    </div>
    </form>
</body>
</html>
