<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_Sale.aspx.cs" Inherits="MasterFiles_Stockist_Sale" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fieldforce Stockist Entry Map</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        #btnSubmit
        {
            margin-right: 30px;
        }
        
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <%--<table width="50%">  
         <tr>
         <td style="width:15%" />
               <td colspan="2" align="center">  
               <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="70%" HorizontalAlign="left">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAlpha" runat="server" CommandArgument = '<%#bind("stockist_name") %>' text = '<%#bind("stockist_name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                   
                </td>
            </tr>   
            </table> --%>
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblddlDetails">
                <tr style="height: 30px">
                    <td align="left">
                        <asp:Label ID="lblStockistName" runat="server" SkinID="lblMand" Font-Bold="true"  Text="Stockist Name "></asp:Label>
                    </td>
                    <%-- <td>
                <asp:DropDownList ID="dlAlpha" runat="server" AutoPostBack="true" Width="160px" DataNavigateUrlFormatString="Stockist_Sale.aspx?stockist_code={0}" 
                       DataNavigateUrlFields="stockist_code" onselectedindexchanged="ddlStockist_SelectedIndexChanged">                    
                    </asp:DropDownList>
                </td>--%>
                    <td align="left">
                        <asp:DropDownList ID="dlAlpha" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            OnSelectedIndexChanged="dlAlpha_SelectedIndexChanged">
                            <asp:ListItem Selected="true">---ALL---</asp:ListItem>
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                            <asp:ListItem>E</asp:ListItem>
                            <asp:ListItem>F</asp:ListItem>
                            <asp:ListItem>G</asp:ListItem>
                            <asp:ListItem>H</asp:ListItem>
                            <asp:ListItem>I</asp:ListItem>
                            <asp:ListItem>J</asp:ListItem>
                            <asp:ListItem>K</asp:ListItem>
                            <asp:ListItem>L</asp:ListItem>
                            <asp:ListItem>M</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                            <asp:ListItem>O</asp:ListItem>
                            <asp:ListItem>P</asp:ListItem>
                            <asp:ListItem>Q</asp:ListItem>
                            <asp:ListItem>R</asp:ListItem>
                            <asp:ListItem>S</asp:ListItem>
                            <asp:ListItem>T</asp:ListItem>
                            <asp:ListItem>U</asp:ListItem>
                            <asp:ListItem>V</asp:ListItem>
                            <asp:ListItem>W</asp:ListItem>
                            <asp:ListItem>X</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>Z</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlStockist" runat="server" AutoPostBack="true" DataNavigateUrlFormatString="Stockist_Sale.aspx?stockist_code={0}"
                            DataNavigateUrlFields="stockist_code" SkinID="ddlRequired" OnSelectedIndexChanged="ddlStockist_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
        </center>
        <center>
        <table  border="0" cellpadding="3" cellspacing="3" id="Table1" align="center">
            <tr>
                <td style="width: 92px" align="left">
                    <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Stockist Name" Width="86px"
                        Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStockist_Name" runat="server" Width="161px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                        onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                        SkinID="TxtBxAllowSymb" Visible="False"></asp:TextBox>
                </td>
                      <td align="left">
                    <asp:Label ID="lblStockist_Address" runat="server" SkinID="lblMand" Text="Address"
                        Width="99px" Visible="False"></asp:Label>
                </td>
                     <td align="left">
                    <asp:TextBox ID="txtStockist_Address" runat="server" SkinID="TxtBxAllowSymb" Width="297px"
                        TabIndex="2" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White' "
                        MaxLength="150" onkeypress="AlphaNumeric(event);" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 92px" align="left">
                    <asp:Label ID="lblStockist_ContactPerson" runat="server" SkinID="lblMand" Text="Contact Person"
                        Width="91px" Visible="False"></asp:Label>
                </td>
                      <td align="left">
                    <asp:TextBox ID="txtStockist_ContactPerson" runat="server" Width="224px" TabIndex="3"
                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                        MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"
                        Visible="False"></asp:TextBox>
                </td>
                    <td align="left">
                    <asp:Label ID="lblStockist_Designation" runat="server" SkinID="lblMand" Text="ERP Code"
                        Width="91px" Visible="False"></asp:Label>
                </td>
                      <td align="left">
                    <asp:TextBox ID="txtStockist_Desingation" runat="server" SkinID="TxtBxAllowSymb"
                        Width="150px" TabIndex="4" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White' "
                        MaxLength="15" onkeypress="AlphaNumeric_NoSpecialCharshq(event);" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 92px" align="left">
                    <asp:Label ID="lblStockist_Mobile" runat="server" SkinID="lblMand" Text="Mobile No"
                        Width="87px" Visible="False"></asp:Label>
                </td>
                     <td align="left">
                    <asp:TextBox ID="txtStockist_Mobile" runat="server" Width="224px" TabIndex="5" SkinID="TxtBxNumOnly"
                        onfocus="this.style.backgroundColor = '#E0EE9D'" onblur="this.style.backgroundColor='White'"
                        MaxLength="100" onkeypress="CheckNumeric(event);" Visible="False"></asp:TextBox>
                </td>
                     <td align="left" visible="false">
                    <asp:Label ID="lblTerritory" runat="server" SkinID="lblMand" Text="Territory" Width="91px"
                        Visible="False"></asp:Label>
                </td>
                      <td align="left" visible="false">
                    <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxAllowSymb" Width="210px"
                        TabIndex="6" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White' "
                        MaxLength="150" onkeypress="AlphaNumeric_NoSpecialChars(event);" Visible="False">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table border="1" cellpadding="3" cellspacing="3" id="tblSalesforceDtls" align="center">
            <tr>
                <td rowspan="">
                    <asp:Label ID="lblTitle_SalesforceDtls" runat="server" Width="755px" Text="Select the Fieldforce Name"
                        TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="small" ForeColor="#8B0000" Visible="False"></asp:Label>
                </td>
            </tr>
            <%-- <table width="80%" align="center" border="0"> --%>
            <tr>
                <td colspan="2" align="right">
                    <asp:Label ID="lblFilter" runat="server" SkinID="lblMand" Text="Filter By" Font-Bold="true" Visible="False"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlFilter" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"
                        Visible="False">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click"
                        CssClass="BUTTON" Visible="False" />
                </td>
            </tr>
            <tr>
               <%-- <td rowspan="1" align="left" style="width: 379px; height: 10px">
                    <asp:CheckBoxList ID="chkboxSalesforce" runat="server" DataTextField="Sf_Name" DataValueField="sf_code"
                        RepeatColumns="2" RepeatDirection="Vertical" Width="753px" TabIndex=" 8" Style="font-size: x-small;
                        color: black; font-family: Verdana;">
                    </asp:CheckBoxList>
                    <asp:HiddenField ID="HidStockistCode" runat="server" />
                </td>--%>

               
                <td rowspan="1" align="left" style="width: 379px; height: 10px">
                    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                        RepeatLayout="Flow">
                        <ItemTemplate>
                            <h3>
                                <asp:Label ID="lblsf_Name" ForeColor="#3333cc" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Eval("HQ") %>' /></h3>
                                <%--<br />--%>
                             <asp:CheckBox ID="chkCategoryNameLabel"   runat="server" Font-Size="10px" Text='<%# Eval("sf_Name") %>'></asp:CheckBox>
                            <%-- <br />--%>
           <asp:HiddenField ID="cbTestID" runat="server" value='<%# Eval("SF_Code") %>' />
           <%--<br />--%>

        <asp:Label ID="lbltest" runat="server"></asp:Label>
                            <asp:HiddenField ID="HidStockistCode" runat="server" />
                        </ItemTemplate>
                    </asp:DataList> 
                </td>
            </tr>
        </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px"
                Text="Update" OnClick="btnSubmit_Click" Visible="False" />
        </center>
    </div>
    <div class="div_fixed">
        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="BUTTON" Visible="false"
            OnClick="btnSave_Click" />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
