<%@ Page Title="Retailer Details List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="LstDoctorList.aspx.cs" Inherits="MasterFiles_ListedDoctor_LstDoctorList"  EnableEventValidation="false" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>--%>
<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Retailer Details</title>
   <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    
   <%--  <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
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
        .gridview1
        {
            background-color: #336699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }
        
        .gridview1 a
        {
            margin: auto 1%;
            border-style: none;
            border-radius: 50%;
            background-color: #444;
            padding: 5px 7px 5px 7px;
            color: #fff;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }
        .gridview1 a:hover
        {
            background-color: #1e8d12;
            color: #fff;
        }
        .gridview1 td
        {
            border-style: none;
        }
        .gridview1 span
        {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 50%;
            padding: 5px 7px 5px 7px;
        }
        .mGridImg1
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGridImg1 td
        {
            padding: 2px;
            border-color: Black;
            background: F2F1ED;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGridImg1 th
        {
            padding: 4px 2px;
            color: white;
            background: #336699;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        .mGridImg1 .pgr
        {
            background: #336699;
        }
        .mGridImg1 .pgr table
        {
            margin: 5px 0;
        }
        .mGridImg1 .pgr td
        {
            border-width: 0;
            text-align: left;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: Red;
            line-height: 12px;
        }
        .mGridImg1 .pgr a
        {
            color: White;
            text-decoration: none;
        }
        .mGridImg1 .pgr a:hover
        {
            color: #000;
            text-decoration: none;
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
            $('#<%=Btnsrc.ClientID%>').click(function () {

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#<%=ddlSrc2.ClientID%>').focus(); return false; }
                if ($('#<%=txtsearch.ClientID%>').val() == "") { alert("Enter Retailer Name."); $('#<%=txtsearch.ClientID%>').focus(); return false; }

            });
            $('#<%=btnGo.ClientID%>').click(function () {
                   var st = $('#<%=DDL_div.ClientID%> :selected').text();
                   if (st == "--Select--") { alert("Select Division."); $('#<%=DDL_div.ClientID%>').focus(); return false; }
				   var st = $('#<%=salesforcelist.ClientID%> :selected').text();
                  if (st == "--Select--") { alert("Select SalesForce."); $('#<%=salesforcelist.ClientID%>').focus(); return false; }
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Distributor Name."); $('#<%=ddlSFCode.ClientID%>').focus(); return false; }
                var st = $('#<%=ddlroutecode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Route."); $('#<%=ddlroutecode.ClientID%>').focus(); return false; }

            });
            $('#<%=btnQAdd.ClientID%>').click(function () {
                var st = $('#<%=DDL_div.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Division."); $('#<%=DDL_div.ClientID%>').focus(); return false; }
 				var st = $('#<%=salesforcelist.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select SalesForce."); $('#<%=salesforcelist.ClientID%>').focus(); return false; }
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Distributor Name."); $('#<%=ddlSFCode.ClientID%>').focus(); return false; }
                var st = $('#<%=ddlroutecode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Route."); $('#<%=ddlroutecode.ClientID%>').focus(); return false; }

            });
            $('#<%=btnDAdd.ClientID%>').click(function () {
                var st = $('#<%=DDL_div.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Division."); $('#<%=DDL_div.ClientID%>').focus(); return false; }
 				var st = $('#<%=salesforcelist.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select SalesForce."); $('#<%=salesforcelist.ClientID%>').focus(); return false; }
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Distributor Name."); $('#<%=ddlSFCode.ClientID%>').focus(); return false; }
                var st = $('#<%=ddlroutecode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Route."); $('#<%=ddlroutecode.ClientID%>').focus(); return false; }

            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
<table width="95%" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td>
                    <table id="Table2" runat="server" width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                    text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" style="width: 45%">
                                <asp:Label ID="lblHeading" Text="Retailer Details List" runat="server" Visible="false" CssClass="under" Style="text-transform: capitalize;
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
        </table>
    <div>
        <div id="Divid" runat="server">
        </div>
        <br />
        <table width="90%">
            <tr>
				 <td align="right" colspan="3" style="margin-right: 30%">
                <asp:Label ID="lblFilter" runat="server" Font-Bold="true" ForeColor="Purple" Text="Filter By Manager"></asp:Label>&nbsp;&nbsp;
                                  
                                    <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                    <asp:Button ID="Button1" runat="server" CssClass="BUTTON" 
                        Width="30px" Height="25px"
                                        Text="Go" onclick="Button1_Click"  />
                </td>
                <td align="right" colspan="3">
                    <%--     <asp:Button ID="btnBack1" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />--%>
                    <div style="margin-left: 90%">
                        <%--<asp:ImageButton ID="btnBack1" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />--%>
 					<asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Height="40px" Width="40px" OnClick="ExportToExcel" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 8.2%">
                </td>
                <td>
                    <asp:Panel ID="pnlAdmin" runat="server">
                        <asp:Label ID="lblSalesforce" runat="server" SkinID="lblMand" 
                            Text="Division"></asp:Label>
                        
                       <asp:DropDownList ID="DDL_div" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            Font-Bold="True" onselectedindexchanged="DDL_div_SelectedIndexChanged">
                        </asp:DropDownList>
						<asp:DropDownList ID="Alpha" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            OnSelectedIndexChanged="Alpha_SelectedIndexChanged" Height="24px" Visible="false">
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
						 <asp:DropDownList ID="salesforcelist" runat="server" SkinID="ddlRequired" 
                            onselectedindexchanged="salesforcelist_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSFCode" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlSFCode_SelectedIndexChanged" Font-Bold="True" Visible="false">
                        </asp:DropDownList>
                        <asp:DropDownList ID="Territory" runat="server" SkinID="ddlRequired" AutoPostBack="true"  OnSelectedIndexChanged="Territory_SelectedIndexChanged" CssClass="ddl" Visible="true" >                         
                         </asp:DropDownList>
                        <asp:DropDownList ID="ddlroutecode" runat="server" SkinID="ddlRequired" Visible="false" Font-Bold="True">
                        </asp:DropDownList>
                         
                        <asp:Button ID="btnGo" runat="server" Width="35px" Height="25px" Text="Go" CssClass="BUTTON"
                            OnClick="btnSubmit_Click" />
                    </asp:Panel>
                </td>
                <td align="right" width="30%">
                    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana"
                        Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <center>
            <table width="91%">
                <tr>
                    <td style="width: 3.2%" />
                    <td>
                        <asp:Button ID="btnQAdd" runat="server" CssClass="BUTTON" Text="Add Listed Retailer" ForeColor="White" BackColor="HotPink" 
                            Width="130px" OnClick="btnQAdd_Click" />
                        &nbsp;
                       
                        <%--<asp:Button ID="btnEdit" runat="server" CssClass="BUTTON" ForeColor="White" BackColor="DarkGreen" Text="Edit All Listed Customer"
                            Width="160px" OnClick="btnEdit_Click" />--%>
                        &nbsp;
<%--                        <asp:Button ID="btnDeAc" runat="server" CssClass="BUTTON" ForeColor="White"  BackColor="Chocolate" Text="Deactivate Listed Customer"
                            Width="160px" OnClick="btnDeAc_Click" />--%>
                        &nbsp;
                         <asp:Button ID="btnDAdd" runat="server" CssClass="BUTTON" Text="Bulk Add Listed Retailer"
                            Width="160px" OnClick="btnDAdd_Click" Visible="false"  />
                        &nbsp;
                        <%--<asp:Button ID="btnSlNoChg" runat="server" CssClass="BUTTON" Text="Change Sl.No"
                            Width="120px" OnClick="btnSlNoChg_Click" />--%>
                        &nbsp;
                        <asp:Button ID="btnReAc" runat="server" CssClass="BUTTON" Text="Reactivate Listed Retailer"
                            Width="160px" OnClick="btnReAc_Click" Height="26px" Visible="false" />
                        &nbsp;
                        <%--<asp:Button ID="btntypemap" runat="server" CssClass="BUTTON" Width="150px" Visible="false"
                            Text="ListedCustomer-Type Map" OnClick="btntypemap_Click" />--%>
                        &nbsp;
                        <%--<asp:Button ID="btnpromap" runat="server" CssClass="BUTTON" Width="180px" Text="ListedCustomer-Product Map"
                            OnClick="btnpromap_Click" />--%>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 3.6%" />
                <td>
                    <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged" Visible="false">
                        <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Customer Channel" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Customer Category" Value="3"></asp:ListItem>
                       <%-- <asp:ListItem Text="Customer Qualification" Value="4"></asp:ListItem>--%>
                        <asp:ListItem Text="Customer Class" Value="5"></asp:ListItem>
                        <%--   <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                        <asp:ListItem Text="Customer Name" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" CssClass="TEXTAREA"
                        Visible="false"></asp:TextBox>
                    <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" SkinID="ddlRequired"
                        TabIndex="4">
                    </asp:DropDownList>
                    <asp:Button ID="Btnsrc" runat="server" CssClass="BUTTON" Width="30px" Height="25px"
                        Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 50%">
                    <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                        runat="server" HorizontalAlign="Center" AlternatingItemStyle-ForeColor="Red">
                        <SeparatorTemplate>
                        </SeparatorTemplate>
                        <ItemTemplate>
                            &nbsp
<%--                            <asp:LinkButton ID="lnkbtnAlpha" ForeColor="Black" Font-Names="Calibri" Font-Size="14px"
                                runat="server" CommandArgument='<%#bind("ListedDr_Name") %>' Text='<%#bind("ListedDr_Name") %>'>
                            </asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
          <br />
        <asp:Panel ID="pnlselect" runat="server"  Visible="false">
        
        <table width="100%">
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblSelect" runat="server" Font-Size="Large" ForeColor="Red"  Visible="false"
                        Text="Select the Division and Ex.Name & Distributor & Route and Press the 'Go' Button"></asp:Label>

                          <asp:Label ID="lblSelec1" runat="server" Font-Size="Large" ForeColor="Red"  Visible="false"
                        Text="Click the 'ALL' Link"></asp:Label>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10" OnPageIndexChanging="grdDoctor_PageIndexChanging" OnRowCreated="grdDoctor_RowCreated"
                            OnRowUpdating="grdDoctor_RowUpdating" OnRowEditing="grdDoctor_RowEditing" OnRowCancelingEdit="grdDoctor_RowCancelingEdit"
                            OnRowDataBound="grdDoctor_RowDataBound" GridLines="None" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                            OnSorting="grdDoctor_Sorting" onrowcommand="grdDoctor_RowCommand">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Listed Customer Name"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDocName" SkinID="MandTxtBox" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDoc" runat="server"  setfocusonerror="true" ControlToValidate="txtDocName"
                                            ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField S ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Category" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDocCat" AutoPostBack="false" runat="server"  SkinID="ddlRequired" DataSource="<%# Doc_Category() %>"
                                            DataTextField="Doc_Cat_SName"  DataValueField="Doc_Cat_Code">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDocCat" ID="RequiredFieldValidator2"
                                             ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Mobile No" HeaderStyle-ForeColor="White" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDocSpec" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Speciality() %>"
                                            DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">
                                        </asp:DropDownList>
                                             <asp:RequiredFieldValidator ControlToValidate="ddlDocSpec" ID="RequiredFieldValidator3"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:TemplateField  ItemStyle-HorizontalAlign="Left"
                                    HeaderText="EX.Name" HeaderStyle-ForeColor="White" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Field_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDocQua" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Qualification() %>"
                                            DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ControlToValidate="ddlDocQua" ID="RequiredFieldValidator4"
                                             ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField  ItemStyle-HorizontalAlign="Left"
                                    HeaderText="DistributorName" HeaderStyle-ForeColor="White" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDocClass" runat="server" Visible="false" SkinID="ddlRequired" DataSource="<%# Doc_Class() %>"
                                            DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">
                                        </asp:DropDownList>
                                              <asp:RequiredFieldValidator ControlToValidate="ddlDocClass" ID="RequiredFieldValidator5"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Left" 
                                    HeaderText="Route" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Territory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code">
                                        </asp:DropDownList>
                                       <%--  <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" HeaderStyle-Width="90px" ShowEditButton="True" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                              <%--  <asp:HyperLinkField HeaderText="View" Text="View" DataNavigateUrlFormatString="ListedDr_DetailAdd.aspx?type=1&ListedDrCode={0}"
                                    DataNavigateUrlFields="ListedDrCode" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>--%>
                                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="ListedDr_DetailAdd.aspx?type=2&ListedDrCode={0}"
                                    DataNavigateUrlFields="ListedDrCode"  ItemStyle-HorizontalAlign="Center"  Visible="true">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
								 
                                 <asp:TemplateField HeaderText="Deactivate">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("ListedDrCode") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate');">Deactivate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                        
                    </td>
                </tr>
            </tbody>
        </table>
       <%-- <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>--%>
    </div>
    </form>
</body>
</html>
</asp:Content>