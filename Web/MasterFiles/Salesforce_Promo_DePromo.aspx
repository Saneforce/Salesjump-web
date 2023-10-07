<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Salesforce_Promo_DePromo.aspx.cs" Inherits="MasterFiles_Salesforce_Promo_DePromo" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Field Force Promotion / De-Promotion (Baselevel & Managers)</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>            
    <link type="text/css" rel="stylesheet" href="../css/style.css"/>
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
        <center>
      <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td style="width: 7.3%" />
                        <td align="left" style="width: 55%">
                            <asp:Label ID="SearchBy" SkinID="lblMand" runat="server" Text="SearchBy">
                            </asp:Label>
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
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" Width="150px" CssClass="TEXTAREA"
                                Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text=">" Visible="false">
                            </asp:Button>
                        </td>
                        <td align="right" style="margin-right: 20%">
                            <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server">
                            </asp:DropDownList>
                              <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                            <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table width="50%">
                <tbody>
                    <tr>
                        <td style="width: 20%" />
                        <td colspan="2">
                            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                runat="server" Width="70%" HorizontalAlign="left">
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
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                              
                                OnPageIndexChanging="grdSalesForce_PageIndexChanging" OnRowCreated="grdSalesForce_RowCreated"
                               
                                AllowSorting="True" OnSorting="grdSalesForce_Sorting"
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
                                    <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                        <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Sf_UserName" HeaderText="User Name" Visible="false"
                                        HeaderStyle-ForeColor="white">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             
                                    <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                        
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField SortExpression="Designation_Name" HeaderText="Designation" HeaderStyle-ForeColor="white">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ" HeaderStyle-ForeColor="white">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="StateName" HeaderText="State" HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                    </asp:TemplateField>
                                 
                                    <asp:HyperLinkField HeaderText="Promote" Text="Promote" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfusername={1}"
                                        DataNavigateUrlFields="SF_Code,Sf_UserName">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" 
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="De-Promote" Text="De-Promote" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;desgname={1}"
                                        DataNavigateUrlFields="SF_Code,Designation_Name">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px"
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                    </asp:HyperLinkField>

                                     <asp:HyperLinkField HeaderText="Promote" Text="Promote" Visible="false" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}&amp;Designation_Name={2}"
                                        DataNavigateUrlFields="SF_Code,Sf_HQ,Designation_Name">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" 
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                    </asp:HyperLinkField>

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
