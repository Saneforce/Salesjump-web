<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateLocationList.aspx.cs" Inherits="MasterFiles_StateLocationList" %>
<%@ Register Src ="~/UserControl/pnlMenu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State-Location</title>
   
    <style type="text/css">
     .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: gray;
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
    </style>
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    <br />
      <table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Text="Add"  Width="60px" Height="25px" onClick="btnNew_Click" />
           &nbsp;
               <asp:Button ID="btnReactivate" runat="server" CssClass="BUTTON" Width="90px" Height="25px"
                    Text= "Reactivation" onclick="btnReactivate_Click" />
            </td>
        </tr>
      </table>
 
    <table width="90%">
        <tbody>               
            <tr>
             <td style="width:20%" />
                <td colspan="2" align="center">
                    <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand" runat="server" Width="45%" HorizontalAlign="Center">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px" ForeColor="#336277" CommandArgument = '<%#bind("StateName") %>' text = '<%#bind("StateName") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            </tbody>
            </table>
    <table align="center" style="width: 100%">
    <tbody>
        <tr>
            <td colspan="2" align="center">
               <asp:GridView ID="grdState" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found" 
                     onrowupdating="grdState_RowUpdating" onrowediting="grdState_RowEditing"
                     OnRowCommand="grdState_RowCommand"
                     onpageindexchanging="grdState_PageIndexChanging" OnRowCreated="grdState_RowCreated"
                     onrowcancelingedit="grdState_RowCancelingEdit"                  
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="gridview1" 
                    AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdState_Sorting">
                     <HeaderStyle Font-Bold="False" />
                  
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdState.PageIndex * grdState.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="ShortName" HeaderText="Short Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                           <%-- <EditItemTemplate> 
                                <asp:TextBox ID="txtShortName" runat="server" SkinID="TxtBxAllowSymb" MaxLength="2" Text='<%# Bind("ShortName") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <HeaderStyle Width="7%" />
                            <ItemTemplate>
                            
                                <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("ShortName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StateName" HeaderText="State Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="260px">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStateName"  SkinID="TxtBxAllowSymb" width="160px" onkeypress="CharactersOnly(event);" runat="server" MaxLength="120" Text='<%# Bind("StateName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Division Alias Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="280px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lbldivision_name" runat="server" Text='<%# Bind("division_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <%--    <asp:TemplateField HeaderText="FieldForce Count" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="330px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblsf_count" runat="server" Text='<%# Bind("sf_count") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white"  HeaderStyle-Width="100px" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>                        
                         <asp:HyperLinkField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="80px" Text="Edit" DataNavigateUrlFormatString="StateLocationCreation.aspx?State_Code={0}"
                                DataNavigateUrlFields="State_Code" HeaderStyle-ForeColor="white">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>
                       <%-- <asp:TemplateField HeaderText="Delete" HeaderStyle-ForeColor="white">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("State_Code") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the State');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                          <asp:TemplateField HeaderText="Deactivate" HeaderStyle-ForeColor="white" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("State_Code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the State');">Deactivate
                                        </asp:LinkButton>
                                           <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../Images/deact1.png" alt="" width="55px" title="Taged in Division" />
                                        </asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                      </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
              </td>
             </tr>
            </tbody>
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
