<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_Prod_Map_New.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_Listeddr_Prod_Map_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer - Product Map</title>
 <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[src*=plus]").live("click", function () {
                if ($(this).closest("tr").next().css('display') == "table-row") {
                    $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                    $(this).next().remove();
                } else
                    $(this).closest("tr").next().css('display', 'table-row');
                $(this).attr("src", "../../../Images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../../../Images/plus.png");
                $(this).closest("tr").next().css('display', 'none');
            });

        });
    </script>
    <script type="text/javascript">
        function ChkFn(x) {
            aid = x.id.split('_');
            y = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_chkCatName')
            z = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_ddlPriority');
            var pval = z.getAttribute('p');
            // alert('|'+pval+'|');
            z.setAttribute('p', ((z.value != '0') ? z.options[z.selectedIndex].text : ""));
            document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML = document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML.replace(y.parentNode.getElementsByTagName('label')[0].innerHTML + ((pval != '') ? ' ( ' + pval + ' )' : '') + ', ', '')
            if (y.checked == true)
                document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML += y.parentNode.getElementsByTagName('label')[0].innerHTML + ((z.value != '0') ? ' ( ' + z.options[z.selectedIndex].text + ' )' : '') + ', '


        }

    </script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        .textalign
        {
            text-align :center ;
            font-weight :bold ;
        }
      
        .mycheckbox input[type="checkbox"] 
{ 
         margin-right: 7px; 
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

         function confirm_Save() {

             if (confirm('Do you want to Tag the Product?')) {
                 if (confirm('Are you sure?')) {
                     ShowProgress();

                     return true;

                 }
                 else {
                     return false;
                 }
             }
             else {
                 return false;
             }
         }
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
             $('#btnGo').click(function () {
                 var type = $('#<%=ddldr.ClientID%> :selected').text();
                 if (type == "---Select---") { alert("Select Listed Customer."); $('#ddldr').focus(); return false; }
             });
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div id="Divid" runat="server">
        </div>
        <br />
        <center >
         <table align="center" >
            <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDr" runat="server" Text="Listed Customer Name" SkinID="lblMand" ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddldr" runat="server" SkinID="ddlRequired"  
                            Height="24px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                     <asp:Button ID="btnGo"  Width="40px" Height="25px" Text="GO" CssClass="BUTTON" runat="server" 
                    OnClick="btnGo_Click" />
                       <%--<asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="GO" CssClass="BUTTON"
                    OnClick="btnGo_Click" />--%>
                    </td>
                    <td>
                     <asp:Button id="btnclr" onclick="btnClear_Click" runat="server" Width="60px" Height="25px" Text="Clear" 
                     CssClass="BUTTON"></asp:Button>
                    </td>
                </tr>
                
        </table>
        <br />
        <br />
        <table width="100%">
            <tr>
            <td align="center">
               <asp:Label ID="lblSelect" Text="Select the Product" Font-Bold="true"  runat="server" ForeColor="#A52A2A" Font-Size="Medium" Font-Underline ="true" visible="false"></asp:Label>
            </td>
            </tr>
            </table>

          <br />

           <table align="center" width="85%" border="1" cellpadding ="1" cellspacing ="1" style="border-collapse:collapse ">
        <tbody>               
            <tr>          
                    <td align="center" >

                    <asp:DataList ID="DataList1"   Font-Size="9pt" HeaderStyle-BackColor="#666699" Width="85%"
            runat="server" RepeatDirection="Vertical" 
            RepeatColumns="2">
            <HeaderStyle  ForeColor="White"  />
            <HeaderTemplate>
              <asp:Label ID="lblsln" Text="SL No" Font-Bold="true" Width="90px"  runat="server"></asp:Label>
                <asp:Label ID="lblDocVisit" Text="Product Name" Font-Bold="true" Width ="180px"  runat="server"></asp:Label>
                <asp:Label ID="lblunit" Text="Pack" Font-Bold="true" Width="210px"  runat="server"></asp:Label>

                  <asp:Label ID="lbl1" Text="SL No" Font-Bold="true" Width="90px"  runat="server"></asp:Label>
                <asp:Label ID="lbl2" Text="Product Name" Font-Bold="true" Width ="180px"  runat="server"></asp:Label>
                <asp:Label ID="lbl3" Text="Pack" Font-Bold="true"  runat="server"></asp:Label>
                </HeaderTemplate>
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="1px" />
            <AlternatingItemStyle  />
            <ItemStyle  />        
            <ItemTemplate >

            <b></b>
                <asp:Label id="lblSLNO" runat="server" Width="50px" Text='<%# Container.ItemIndex+1 %>' ></asp:Label>
            <asp:Label ID="lblPrdCode" runat="server" Text='<%#Eval("Product_Code_SlNo")%>' Visible="false" ></asp:Label>&nbsp&nbsp
               <asp:CheckBox ID="chkCatName" onclick="ChkFn(this)" Font-Names="Calibri" Width="200px" CssClass="mycheckbox"
               runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product_Detail_Name")%>' />&nbsp&nbsp&nbsp&nbsp&nbsp

                <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("Product_Sale_Unit")%>'  ></asp:Label>
            </ItemTemplate>
        </asp:DataList>

                  <%--  </asp:DataList>--%>
                </td>

            </tr> 
        </tbody>
    </table>
             
                </center>
                <br />
                <br />

                <center >
                <%--<table align ="center" >
                   <tr>
                     <td colspan="2" align="center">
                        <asp:CheckBoxList ID="chkprd" CssClass="chkboxLocation" CellPadding="10" Visible="false"  RepeatColumns="3" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"  RepeatDirection="vertical"  Width="300px" runat="server">
                        </asp:CheckBoxList>
                    </td>
                   </tr>
                </table>
--%>
                  <br />
           <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="BUTTON" Width="70px" Visible ="false" 
            Height="25px" OnClick="btnSubmit_Click" OnClientClick="return confirm_Save();" />
              </center>
    </div>
    </form>
</body>
</html>
