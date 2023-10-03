<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory.aspx.cs" Inherits="MasterFiles_MR_Territory" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<%@ Register Src="~/UserControl/DIS_Menu.ascx" TagName="Menu2" TagPrefix="ucl3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory List</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />
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
    <script type="text/javascript">
        function OpenNewWindow() {

            window.open('Territory_Help.aspx', null, 'height=400, width=300,top=0,left=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }
    </script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
                            $('btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Field Force Name."); $('ddlSFCode').focus(); return false; }

            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <table width="90%">
            <tr>
                <td align="right" colspan="3">
                    <%--     <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />--%>
                    <%--<div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div>    --%>
                </td>
            </tr>
            <tr>
                <td style="width: 8.2%">
                </td>
                <td>
                    <asp:Panel ID="pnlAdmin" runat="server">
                        <asp:Label ID="lblSalesforce" runat="server" Text="DSM Name"></asp:Label>
                        <asp:DropDownList ID="Alpha" Visible="false" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            OnSelectedIndexChanged="Alpha_SelectedIndexChanged">
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
                        <asp:DropDownList ID="ddlSFCode" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
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
        <table width="90%" cellpadding="5" cellspacing="5">
            <tr>
                <td style="width: 8.2%" />
                <td>
                <asp:Button ID="btnAdd" runat="server" CssClass="BUTTON" Width="120px" Height="25px"
                        Text="Add Route" onclick="btnAdd_Click"  />
                 &nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Text="Bulk Add Route" Width="150px"
                        Height="25px" OnClick="btnNew_Click" />
                    &nbsp;&nbsp;
                    <%--  <asp:Button ID="btnDetailAdd" runat="server" CssClass="BUTTON" Text="Detail Add" onClick="btnDetailAdd_Click" />&nbsp;--%>
                   <%-- <asp:Button ID="btnEdit" runat="server" CssClass="BUTTON" Text="Edit All Route"
                        Width="170px" Height="25px" OnClick="btnEdit_Click" />--%>
                    &nbsp;&nbsp;
                <%--    <asp:Button ID="btnTranfer" runat="server" CssClass="BUTTON" Text="Transfer Territory"
                        Width="170px" Height="25px" OnClick="btnTransfer_Click" />--%>
                    &nbsp;&nbsp;
                <%--    <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="BUTTON" Width="120px" Height="25px"
                        Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />&nbsp;&nbsp;--%>
                </td>
                <%--   <td align="right" >           
               
                        <asp:ImageButton ID="imgaudio" ImageUrl="~/Images/help5.png" runat="server" OnClientClick="OpenNewWindow();" Visible="false"/>
                           <asp:Image ID="btnImg" ImageUrl="~/Images/speaker.png" runat="server" Width="30px" Visible="false"/>
                       
                </td>--%>
            </tr>
        </table>
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdTerritory" runat="server" Width="85%" HorizontalAlign="Center"
                            AllowSorting="true" OnSorting="grdTerritory_Sorting" EmptyDataText="No Records Found"
                            OnRowUpdating="grdTerritory_RowUpdating" OnRowEditing="grdTerritory_RowEditing"
                            OnPageIndexChanging="grdTerritory_PageIndexChanging" OnRowCreated="grdTerritory_RowCreated"
                            OnRowCancelingEdit="grdTerritory_RowCancelingEdit" OnRowCommand="grdTerritory_RowCommand"
                            OnRowDataBound="grdTerritory_RowDataBound" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10 " GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="12px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdTerritory.PageIndex * grdTerritory.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritorys_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route Code" Visible="true">
                                   <%-- <EditItemTemplate>
                                        <asp:TextBox ID="txtTerritory_Code" runat="server" Width="450px" SkinID="TxtBxAllowSymb"
                                            MaxLength="150" Text='<%# Bind("Route_Code") %>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Route_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Route Name (Retailer Cnt) | (Distributor CNT)" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="White">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTerritory_Name" runat="server" Width="400px" SkinID="TxtBxAllowSymb"
                                            MaxLength="150" Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                        <asp:TableCell Width="30px">
                                            <asp:Label ID="lblListedDRCnt" runat="server" Visible="false" ForeColor="DarkBlue"
                                                Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="30px">
                                            <asp:Label ID="lblChemistsCnt" runat="server" Visible="false" ForeColor="DarkMagenta"
                                                Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                        </asp:TableCell>
                                       <%-- <asp:TableCell Width="30px">
                                            <asp:Label ID="lblUnListedDRCnt" runat="server" Visible="false" ForeColor="DarkGreen"
                                                Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                        </asp:TableCell>--%>
                                        <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_Name"
                                            ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Table ID="lblTerritory_Name" runat="server">
                                            <asp:TableRow>
                                                <asp:TableCell BorderStyle="None" Width="30px">
                                                    <asp:Label ID="LabTerritory_Name" runat="server" Width="400px" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="30px">
                                                    <asp:Label ID="lblListedDRCnt" runat="server" ForeColor="DarkBlue" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell Width="30px">
                                                    <asp:Label ID="lblChemistsCnt" runat="server" ForeColor="DarkMagenta" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                </asp:TableCell>
                                               <%-- <asp:TableCell Width="30px">
                                                    <asp:Label ID="lblUnListedDRCnt" runat="server" ForeColor="DarkGreen" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                </asp:TableCell>--%>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-Width="300px"  HeaderStyle-ForeColor="White"
                                    Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDist_Territory" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDist_Territory" ID="RequiredFieldValidator2"
                                            ErrorMessage="*Required" InitialValue="0" runat="server" SetFocusOnError="true"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDist_Territory" runat="server" Width="300px" Text='<%# Bind("Dist_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTerritory_Target" runat="server" SkinID="TxtBxAllowSymb"
                                            MaxLength="150" Text='<%# Bind("Target") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDoc1" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_Target"
                                            ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory_Target" runat="server" Text='<%# Bind("Target") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Prod %" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTerritory_minprod" runat="server" SkinID="TxtBxAllowSymb"
                                            MaxLength="150" Text='<%# Bind("Min_Prod") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDoc2" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_minprod"
                                            ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory_minprod" runat="server" Text='<%# Bind("Min_Prod") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Detail Add" Text="Detail Add" DataNavigateUrlFormatString="Territory_Detail.aspx?Territory_Code={0}"
                                    DataNavigateUrlFields="Territory_Code" Visible="false">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center" Width="90px">
                                    </ItemStyle>
                                </asp:HyperLinkField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" HeaderStyle-Width="90px" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>
                                 <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                    DataNavigateUrlFormatString="Territory_Detail.aspx?Territory_Code={0}" DataNavigateUrlFields="Territory_Code">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                               <%-- <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="90px" Visible="true">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Territory_Code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Route');">Deactivate
                                        </asp:LinkButton>
                                        <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../../../Images/deact1.png" alt="" width="50px" title="Kindly Transfer the Territory then deactivate" />
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
