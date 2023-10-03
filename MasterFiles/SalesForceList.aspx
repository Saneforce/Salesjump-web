<%@ Page Title="Field Force List" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="SalesForceList.aspx.cs" Inherits="MasterFiles_SalesForceList" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Field Force</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
            background-color: #666699;
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
    <script type="text/javascript" language="javascript">
        function confirm_DeActive() {
            if (confirm('Do you want to Deactivate the Fieldforce?')) {
                if (confirm('Are you sure?')) {
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
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFilter]');
            var $items = $('select[id$=ddlFilter] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JsFiles/jquery.tooltip.min.js"></script>
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
            $('#btnSearch').click(function () {

                var divi = $('#<%=ddlFields.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlSrc.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc').focus(); return false; }


            });
        }); 
    </script>
    <style type="text/css">
        #tooltip
        {
            position: absolute;
            z-index: 3000;
            border: 1px solid #111;
            background-color: #FEE18D;
            padding: 5px;
            opacity: 0.85;
        }
        #tooltip h3, #tooltip div
        {
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <center>
           
           
             <asp:Button ID="btnNew" runat="server" ToolTip="Click Here to Create the New ID's"
                    CssClass="btn btn-primary btn-md" Text="New ID Creation"  OnClick="btnNew_Click" />
            
             <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Height="40px" Width="40px" OnClick="ExportToExcel" />
          
            <%-- <asp:Table ID="Table1" runat="server" BorderStyle="Solid" Width="500px" BorderWidth="1"
                CellSpacing="3" CellPadding="3">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                     
                    </asp:TableCell>
                     <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnMulti_Divi" runat="server" Width="160px" Height="25px" CssClass="BUTTON" Text="Multi Division Selection"
                            OnClick="btnDivision_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnHo_Create" runat="server" Width="160px" Height="25px" CssClass="BUTTON" Text="Audit-ID Creation"
                            OnClick="btnHo_Create_Click" />
                    </asp:TableCell>
                      <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnPromoDePromo" runat="server" Width="160px" Height="25px" CssClass="BUTTON" Text="Promotion / De-Promotion"
                            OnClick="btnPromoDePromo_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnApproval" runat="server"  CssClass="BUTTON" Width="160px" Height="25px" Text="Approval Changes"
                            OnClick="btnApproval_Click" />
                    </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnReactivate" runat="server"  CssClass="BUTTON" Width="160px" Height="25px" Text="Reactivation"
                            OnClick="btnReactivate_Click" />
                    </asp:TableCell> 
  <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btninterchange" runat="server" CssClass="BUTTON" Width="180px" Height="25px" Text="Interchange/Transfer"
                         OnClick="btninterchange_Click"    />
                    </asp:TableCell>
                  
                 
                </asp:TableRow>
            <asp:TableRow>
                     <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnBkEd" runat="server" CssClass="BUTTON" Text="Bulk Edit" Width="160px" Height="25px"
                            OnClick="btnBkEd_Click" />
                    </asp:TableCell>
                       <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnBulk" runat="server" CssClass="BUTTON" Text="Edit - DCR Start Date" Height="25px" 
                            Width="160px" OnClick="btnBulk_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnBulkTP" runat="server" CssClass="BUTTON" Text="Edit - TP Start Date" Height="25px" 
                            Width="160px" OnClick="btnBulkTP_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnVac" runat="server" CssClass="BUTTON" Width="160px" Height="25px" Text="View Vacant ID's"
                            OnClick="btnVac_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnBlk" runat="server" CssClass="BUTTON" Width="160px" Height="25px" Text="View Blocked ID's"
                            OnClick="btnBlk_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnStatus" runat="server" CssClass="BUTTON" Width="160px" Height="25px" Text="Field Force Status"
                            OnClick="btnStatus_Click" />
                    </asp:TableCell>

                     <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnPromo" runat="server" CssClass="BUTTON" Width="180px" Height="25px" Text="BaseLevel-Manager Promotion"
                            OnClick="btnPromo_Click" />
                    </asp:TableCell>
                    
                
                </asp:TableRow>
            </asp:Table>--%>
            <br />
            <asp:UpdatePanel ID="updateP" runat="server">
                <ContentTemplate>
                    <table width="100%" align="center">
                        <tbody>
                            <tr>
                                <td style="width: 7.3%" />
                                <td align="left" style="width: 45%">
                                    <asp:Label ID="SearchBy" Font-Bold="true" runat="server" Text="SearchBy" ForeColor="Purple">
                                    </asp:Label>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                        <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                                        <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                        <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                        <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                                        <asp:ListItem Value="sf_emp_id">Employee Id</asp:ListItem>
                                        <asp:ListItem Value="StateName">State</asp:ListItem>
                                        <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" Width="150px" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        CssClass="TEXTAREA" Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'"
                                        OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged" SkinID="ddlRequired" TabIndex="4">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" 
                                        CssClass="btn btn-primary btn-md" Visible="false"></asp:Button>
                                </td>
                                <td align="right" style="margin-right: 30%">
                                    <asp:Label ID="lblFilter" runat="server" Font-Bold="true" ForeColor="Purple" Text="Filter By Manager"></asp:Label>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                        ToolTip="Enter Text Here" Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary btn-md"
                                        Text="Go" OnClick="btnGo_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnGo" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <table width="85%" align="center">
                <tbody>
                    <tr>
                        <%--<td style="width: 20%" />--%>
                        <td colspan="2" align="center">
                            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                runat="server" Width="40%" HorizontalAlign="center">
                                <SeparatorTemplate>
                                </SeparatorTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="15px"
                                        ForeColor="#8A2EE6" CommandArgument='<%#bind("sf_name") %>' Text='<%#bind("sf_name") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                OnRowUpdating="grdSalesForce_RowUpdating" OnRowEditing="grdSalesForce_RowEditing"
                                OnPageIndexChanging="grdSalesForce_PageIndexChanging" OnRowCreated="grdSalesForce_RowCreated"
                                OnRowCancelingEdit="grdSalesForce_RowCancelingEdit" OnRowCommand="grdSalesForce_RowCommand"
                                OnRowDataBound="grdSalesForce_RowDataBound" AllowSorting="True" OnSorting="grdSalesForce_Sorting"
                                GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="gridview1"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sf Code" Visible="true">
                                        <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="User Name" Visible="false"
                                        HeaderStyle-ForeColor="white">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUsrName" runat="server" SkinID="TxtBxAllowSymb" Text='<%# Bind("Sf_UserName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="Details">
                                      <ItemTemplate>
                                      <a href="#" class="gridViewToolTip" style="text-decoration:none"> <%# Eval("Sf_Name")%></a>
                                           <div id="tooltip" style="display: none;">
                                                <table>
                                                    <tr>
                                                        <td style="white-space: nowrap;">
                                                            <b>UserName:</b>&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("Sf_UserName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap;">
                                                            <b>Sf Name:</b>&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("Sf_Name")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                      </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField  HeaderText="FieldForce Name" HeaderStyle-Width="300px"
                                        HeaderStyle-ForeColor="white">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtsfName" SkinID="MandTxtBox" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvsf" runat="server" SetFocusOnError="true" ControlToValidate="txtsfName"
                                                ErrorMessage="*Enter Name"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="80px"
                                        HeaderStyle-ForeColor="white">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlDesign" runat="server" SkinID="ddlRequired" DataSource="<%# Fill_Design() %>"
                                                DataTextField="Designation_Name" DataValueField="Designation_Code">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvdes" runat="server" SetFocusOnError="true" ControlToValidate="ddlDesign"
                                                InitialValue="0" ErrorMessage="*Select Desigantion"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="HQ" HeaderStyle-ForeColor="white" Visible="true">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtHQ" SkinID="TxtBxAllowSymb" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvhq" runat="server" SetFocusOnError="true" ControlToValidate="txtHQ"
                                                ErrorMessage="Enter HQ"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="State" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired" DataSource="<%# FillState() %>"
                                                DataTextField="StateName" DataValueField="state_code">
                                            </asp:DropDownList>
										  <asp:RequiredFieldValidator ID="RFValidator1_ddl" InitialValue="0" runat="server" ErrorMessage="RequiredField" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                        ShowEditButton="True" Visible="false">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" HorizontalAlign="Center"
                                            Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}"
                                        DataNavigateUrlFields="SF_Code">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" HorizontalAlign="Center"
                                            Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="Vacant" Visible="false" Text="To Vacant" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}"
                                        DataNavigateUrlFields="SF_Code,Sf_HQ">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" HorizontalAlign="Center"
                                            Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="Block" Visible="false" ItemStyle-HorizontalAlign="Center"
                                        Text="Block" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_type={1}"
                                        DataNavigateUrlFields="SF_Code,SF_Type">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" Font-Bold="False">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"></HeaderStyle>
                                    </asp:HyperLinkField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" Font-Bold="False">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("SF_Code") %>'
                                                CommandName="Deactivate" OnClientClick="return confirm_DeActive();">Deactivate</asp:LinkButton>
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
        </center>
        <%-- <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>--%>
    </div>
    </form>
</body>
</html>
</asp:Content>