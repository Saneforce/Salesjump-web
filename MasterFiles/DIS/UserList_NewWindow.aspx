<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList_NewWindow.aspx.cs" Inherits="MasterFiles_UserList_NewWindow" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User List</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/AdminMenuStyle.css" />
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
         td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
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
            $('#btnGo').click(function () {

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var State = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (State == "---Select State---") { alert("Select State Name."); $('#ddlFieldForce').focus(); return false; }


            });
        }); 
    </script>
     
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <br />
       <center>
       <asp:Label ID="lbluser" runat="server" Text="User List" Font-Bold="true" Font-Underline="true" Font-Size="14px"></asp:Label>
       </center>
       <br />
        <center>
            <table cellpadding="1" cellspacing="1" >
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>&nbsp;
						<asp:Button ID="btnGo" runat="server" Text="Go" Width="35px" Height="25px" OnClick="btnGo_Click"
                            CssClass="btnnew" />
                        
                    </td>
                    
                </tr>             
                   
                   
            </table>
            <center>
             <asp:CheckBox ID="chkVacant" Text=" Without - Vacant" Font-Size="Medium" Font-Names="Calibri"
                            ForeColor="Red" Checked="true" runat="server" /> &nbsp;
                  <br />
                        
                            </center>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="95%" HorizontalAlign="Center"
                                EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowDataBound="grdSalesForce_RowDataBound"
                                GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
			                            <asp:TemplateField HeaderText="Drs Count" HeaderStyle-ForeColor="White" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
			                                <ItemTemplate>
			                                    <asp:Label ID="lblDrsCnt" runat="server" Font-Size="12px" Font-Bold="true" Width="10%" Font-Names="sans-serif" Forecolor="Red" Text='<%# Bind("Lst_drCount") %>' ></asp:Label>
			                                </ItemTemplate>
			                            </asp:TemplateField>
                            

                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlace" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="State Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="130px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldForce" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reporting" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTPReporting" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" Width="120px" runat="server" Font-Size="8pt" Font-Names="sans-serif" Forecolor="#483d8b" Style="font-weight:700" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Password" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassword" runat="server" Font-Size="8pt" Font-Names="sans-serif" Forecolor="#483d8b" Style="font-weight:700" Text='<%# Bind("sf_password") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblS" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFType" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("sf_type") %>'></asp:Label>
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
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>