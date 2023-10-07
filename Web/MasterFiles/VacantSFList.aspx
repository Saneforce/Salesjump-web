<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VacantSFList.aspx.cs" Inherits="MasterFiles_VacantSFList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vacant - Fieldforce List</title>
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
        
         .gridview1{
    background-color:#666699;
    border-style:none;
   padding:2px;
   margin:2% auto;
   
   
}

 .gridview1 a{
  margin:auto 1%;
  border-style:none;
    border-radius:50%;
      background-color:#444;
      padding:5px 7px 5px 7px;
      color:#fff;
      text-decoration:none;
      -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;
     
}
.gridview1 a:hover{
    background-color:#1e8d12;
    color:#fff;
}
.gridview1 td{
    border-style:none;
}
.gridview1 span{
    background-color:#ae2676;
    color:#fff;
     -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;

    border-radius:50%;
    padding:5px 7px 5px 7px;
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
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td style="width: 7.2%" />

                     <td align="left" style="width: 50%">
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
                                   <asp:ListItem Value ="StateName">State</asp:ListItem>
                                <%--<asp:ListItem Value="StateName">State</asp:ListItem>--%>
                                <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" Width="150px" CssClass="TEXTAREA"
                                Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'" OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="btnGo" OnClick="btnGo_Click" runat="server" Text="Go" 
                                Width="29px" Height="25px" CssClass="BUTTON" Visible="false">
                            </asp:Button>
                        </td>
                        <td style="width: 13.2%" />
                    <td>
                        <asp:Label ID="lblFieldForceType" runat="server" Text="Filter by" Font-Bold="true" ForeColor="Purple" ></asp:Label>&nbsp;
                        <asp:DropDownList ID="ddlFieldForceType" runat="server" onblur="this.style.backgroundColor='White'"
                            TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'" SkinID="ddlRequired"
                            Font-Bold="False">
                            <%--  <asp:ListItem Text="---Select---" Value="" ></asp:ListItem> --%>
                            <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                            <asp:ListItem Text="MR" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" TabIndex="2" runat="server"
                            Text="Go" Width="30px" Height="25px" CssClass="BUTTON"></asp:Button>
                    </td>                   
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                   </tbody>
        </table>
        <table width="100%" align="center">
                <tbody>
                    <tr>
                        <%--<td style="width: 20%" />--%>
                        <td colspan="2" align="center" >
                            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                runat="server" Width="30%" HorizontalAlign="center">
                                <SeparatorTemplate>
                                </SeparatorTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px" ForeColor="#8A2EE6" CommandArgument='<%#bind("sf_name") %>'
                                        Text='<%#bind("sf_name") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </tbody>
            </table>

                <table width="100%" align="center">
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center" PagerStyle-CssClass="pgr"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowCommand="grdSalesForce_RowCommand"
                            EmptyDataText="No Records Found" GridLines="None" OnPageIndexChanging="grdSalesForce_PageIndexChanging"
                            CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                    <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDes" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSt" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Activate" Text="Activate" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfname={1}"
                                    DataNavigateUrlFields="SF_Code,Sf_Name">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                </asp:HyperLinkField>
                                 <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;state={1}"
                                    DataNavigateUrlFields="SF_Code,StateName">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                </asp:HyperLinkField>

                                  <asp:HyperLinkField HeaderText="Rejoin" Text="Rejoin" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfname={1}&amp;state_code={2}"
                                    DataNavigateUrlFields="SF_Code,Sf_Name,state_code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                </asp:HyperLinkField>
                                 <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px"
                                            Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center">
                                        </HeaderStyle>
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
                 </table>
            
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
