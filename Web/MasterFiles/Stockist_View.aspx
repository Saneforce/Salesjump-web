<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_View.aspx.cs" Inherits="MasterFiles_Stockist_View" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stockist View</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    </div>
    <br /> 
    <table width="80%">
        <tbody>               
            <tr>
             <td style="width:30%" />
                <td colspan="2" align="center">
                    <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal"  OnItemCommand="dlAlpha_ItemCommand" runat="server"  HorizontalAlign="Center">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                        &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha" Font-Names="Calibri" Font-Size="14px" runat="server" ForeColor="#8A2EE6" CommandArgument = '<%#bind("Stockist_Name") %>' text = '<%#bind("Stockist_Name") %>'>
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
                
                    <asp:GridView ID="grdStockist" runat="server" Width="80%" HorizontalAlign="Center" 
                        AutoGenerateColumns="False" EmptyDataText="No Records Found"
                        onpageindexchanging="grdStockist_PageIndexChanging"                          
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt"  >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle Wrap="true" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns> 
                              
                            <asp:TemplateField HeaderText="S.No">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdStockist.PageIndex * grdStockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("Stockist_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Stockist Name">
                                
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="250px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStockistName"  runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField  HeaderText="Address">
                                
                              
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="250px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Address" runat="server" Text='<%# Bind("Stockist_Address") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField  HeaderText="Phone No">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>                  
                            <asp:TemplateField  HeaderText="FieldForce Name">                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                    <ItemTemplate>   
                                        
                                     <%--   <asp:Label ID="chkboxSalesforce" runat="server" Text='<%# ((string)Eval("SfName")).Replace("\n", "<br/>") %>' ></asp:Label>--%>
                                       
                                     <asp:Literal runat="server" id="Values" 
                Text='<%# string.Join("<br />", Eval("SfName").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
            </asp:Literal>

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
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
