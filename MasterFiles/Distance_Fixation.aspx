<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distance_Fixation.aspx.cs"
    Inherits="MasterFiles_Distance_Fixation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SFC Updation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    
    <script type="text/javascript">
        function GridViewRepeatColumns(grdHQ, repeatColumns) {
            //Created By: Brij Mohan(http://techbrij.com)
            GridViewRepeatColumns("<%=grdHQ.ClientID %>", 2);
            if (repeatColumns < 2) {
                alert('Invalid repeatColumns value');
                return;
            }
            var $gridview = $('#' + grdHQ);
            var $newTable = $('<table></table>');

            //Append first row in table
            var $firstRow = $gridview.find('tr:eq(0)'),
            firstRowHTML = $firstRow.html(),
            colLength = $firstRow.children().length;

            $newTable.append($firstRow);

            //Append first row cells n times
            for (var i = 0; i < repeatColumns - 1; i++) {
                $newTable.find('tr:eq(0)').append(firstRowHTML);
            }

            while ($gridview.find('tr').length > 0) {
                var $gridRow = $gridview.find('tr:eq(0)');
                $newTable.append($gridRow);
                for (var i = 0; i < repeatColumns - 1; i++) {
                    if ($gridview.find('tr').length > 0) {
                        $gridRow.append($gridview.find('tr:eq(0)').html());
                        $gridview.find('tr:eq(0)').remove();
                    }
                    else {
                        for (var j = 0; j < colLength; j++) {
                            $gridRow.append('<td></td>');
                        }
                    }
                }
            }
            //update existing GridView
            $gridview.html($newTable.html());
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
    </div>
    <center>
        <table>
            <tr>
                <td align="center">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
                      OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        
    </center>
    <table width="100%">
        <tr>
            <td style="width: 5%">
            </td>
            <td align="left">
                <asp:Label ID="lblFieldName" runat="server" Font-Size="12px" Font-Names="Verdana"
                    Visible="true"></asp:Label>
            </td>
            <td align="right">
                <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana"
                    Visible="true"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblSelect" Text="Please Select the Field Force Name" runat="server"
                        ForeColor="Red" Font-Size="Large"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <asp:Panel ID="pnlDist" runat="server" Visible="false">
            <table width="70%">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblHQSta" runat="server" Text="Head Quarter" Font-Bold="true" Font-Size="Small"
                            ForeColor="Black"></asp:Label>
                        <span style="color: Blue; font-weight: bold">(No Fare Only Allowance)</span>
                    </td>
                    <%-- <td><asp:Label ID="lblFare" runat="server" Text="No Fare Only Allowance" ForeColor="Red"></asp:Label></td>--%>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdHQ" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                         OnRowDataBound="grdHQ_RowDataBound"
                            GridLines="None" BorderStyle="Solid" CssClass="mGrid" EmptyDataText="No Head Quarter Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQDoctor" runat="server" Text='<%# Eval("Territory_Name")%>'></asp:Label>
                                      <asp:Label ID="Label4" runat="server" Text='<%# Eval("ListedDR_Count")%>'></asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label2" runat="server" Text="EX Station" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdEX" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" BorderStyle="Solid" CssClass="mGrid" EmptyDataText="No EX Station Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromEX" runat="server" Text='<%#Eval("Sf_HQ")%>'>
                                        </asp:Label>
                                        <asp:HiddenField ID="hdnFrmTerrCode" runat="server" Value='<%#Eval("sf_code")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToEX" runat="server" Text='<%#Eval("Territory_Name")%>'>
                                        </asp:Label>
                                        <asp:HiddenField ID="hdnToTerrCode" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                        <asp:HiddenField ID="hidcat" runat="server" Value='<%#Eval("Territory_Cat")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance(One Way in Kms)">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtKms" runat="server" Text='<%#Eval("Distance") %>' SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="Out Station" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdOS" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" BorderStyle="Solid" CssClass="mGrid" EmptyDataText="No Out Station Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromOs" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnOSFrmTerrCode" runat="server" Value='<%#Eval("sf_code")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToOs" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnOSToTerrCode" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                        <asp:HiddenField ID="oSHidCat" runat="server" Value='<%#Eval("Territory_Cat")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance(One Way in Kms)" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOsKms" runat="server" SkinID="MandTxtBox" Text='<%#Eval("Distance") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label3" runat="server" Text="OS-EX" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdOSEX" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" BorderStyle="Solid" CssClass="mGrid" EmptyDataText="No OS-EX Station Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromOSEX" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnOSEXFrmTerrCode" runat="server" Value='<%#Eval("FCode")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToOSEX" runat="server" Text='<%#Eval("TName")%>'></asp:Label>
                                         <asp:HiddenField ID="hdnOSEXToTerrCode" runat="server" Value='<%#Eval("TCode")%>' />
                                        <asp:HiddenField ID="oSEXHidCat" runat="server" Value='<%#Eval("TCat")%>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance(One Way in Kms)" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOsExKms" runat="server" Text='<%#Eval("Distance")%>' SkinID="MandTxtBox"></asp:TextBox>
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
        </asp:Panel>
    </center>
    <center>
        <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
            Text="Save SFC" Visible="false" OnClick="btnSave_Click" />
    </center>
    </form>
</body>
</html>
