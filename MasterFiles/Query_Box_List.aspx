<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Query_Box_List.aspx.cs" Inherits="MasterFiles_Query_Box_List"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>"); $(this).next().remove();
                }
            });
        }); 
    </script>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .mGrid
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGrid td
        {
            padding: 2px;
            border: solid 1px Black;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            font-size: small;
            font-family: Calibri;
        }
        
        
        .mGrid th
        {
            padding: 4px 2px;
            color: white;
            background: #A6A6D2;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGrid .pgr
        {
            background: #A6A6D2;
        }
        .mGrid .pgr table
        {
            margin: 5px 0;
        }
        .mGrid .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            text-align: left;
            border-left: solid 1px #666;
            font-weight: bold;
            color: White;
            line-height: 12px;
        }
        .mGrid .pgr th
        {
            background: #A6A6D2;
        }
        .mGrid .pgr a
        {
            color: #666;
            text-decoration: none;
        }
        .mGrid .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <title>Query Box List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <center>
            <h2>
                Query Box List</h2>
        </center>
        <asp:Panel ID="pnlcompl" runat="server" HorizontalAlign="Right" style="margin-right:20px">
        <table >
            <tr>
                <td>
                    <asp:Button ID="btnComp" runat="server" Text="Completed Queries"  
                        BackColor="Pink" onclick="btnComp_Click"/>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLog" runat="server" Text="Logout" BackColor="Pink" 
                        onclick="btnLog_Click" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        <br />
        <center>
            <table cellpadding="8" cellspacing="8">
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
            </table>
        </center>
        <br />
           <center>
        <asp:Button ID="btngo" runat="server" Text ="Go" Width="30px" Height="25px" BackColor="LightBlue" onclick="btngo_Click"/>
        </center>
        <br />
        <table align="center" style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdQuery" runat="server" Width="95%" HorizontalAlign="Center" OnRowCommand="grdQuery_RowCommand" 
                            AutoGenerateColumns="false"  EmptyDataText="No Records Found" 
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="7%" />
                                    <ItemTemplate>
                                        <%--  <img alt = "" style="cursor: pointer" src="../../Images/plus.png" title="Click here to tag the Campaign Dr" />--%>
                                        <asp:ImageButton ID="imgOrdersShow" title="" runat="server" OnClick="Show_Hide_OrdersGrid"
                                            ImageUrl="~/Images/plus.png" CommandArgument="Show" />
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView ID="grdreporting" runat="server" Width="60%" AutoGenerateColumns="false"
                                                CssClass="mGridImg">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Reporting to" HeaderStyle-BackColor="AliceBlue" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <%--    <asp:CheckBox ID="chkCatName" OnCheckedChanged="chkCatName_OnCheckedChanged" Font-Names="Calibri" Font-Size="Small" AutoPostBack="true"  runat="server" Text='<%# Eval("Doc_SubCatName") %>' />--%>
                                                            <%--  <asp:HiddenField ID="cbSubCat" runat="server" value='<%# Eval("Doc_SubCatCode") %>' />--%>
                                                            <asp:Label ID="cbSubCat" Width="140px" runat="server" Text='<%# Eval("Reporting_To_SF") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name" HeaderStyle-BackColor="AliceBlue" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluser" Width="130px" runat="server" Text='<%# Eval("Reporting_User") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Password" HeaderStyle-BackColor="AliceBlue" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpass" Width="130px" runat="server" Text='<%# Eval("Reporting_Pass") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdQuery.PageIndex * grdQuery.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="sf_code" HeaderStyle-ForeColor="White" Visible="true"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsf_code" runat="server" Text='<%#Eval("sf_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query_Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuery" runat="server" Text='<%#Eval("Query_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="280px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldivision_name" runat="server" Text='<%# Bind("division_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="280px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfname" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfuser" runat="server" Text='<%# Eval("Sf_UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfpass" runat="server" Text='<%# Eval("Sf_Password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="7%">
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbltype" runat="server" Text='<%# Bind("Query_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="260px">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStateName" SkinID="TxtBxAllowSymb" Width="160px" onkeypress="CharactersOnly(event);"
                                            runat="server" MaxLength="120" Text='<%# Bind("StateName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("Query_Text") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" Width="120px" runat="server" Text='<%# Bind("Created_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="white" HeaderStyle-Width="100px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnkbutPending" runat="server" CommandArgument='<%# Eval("Query_Id") %>'
                                            CommandName="Status" >Completed
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
    </div>
    </form>
</body>
</html>
