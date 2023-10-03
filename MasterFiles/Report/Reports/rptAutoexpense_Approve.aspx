<%@ Page Title="Expense Statement Approval View" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="rptAutoexpense_Approve.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Expense Statement Approval View</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
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
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSubdiv" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlSubdiv" SkinID="ddlRequired" runat="server">
                        </asp:DropDownList>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc"><asp:dropdownlist ID="monthId" runat="server"></asp:dropdownlist></td>
                      <td align="left" class="stylespc">
                        <asp:Label ID="lblYr" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                    </td> 
                    <td align="left" class="stylespc"><asp:DropDownList ID="yearID" runat="server"></asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                   <td><asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
                OnClick="btnSF_Click" /></td>
                </tr>
            </table>
        </center>
        <br />
        <table align="right" style="margin-right: 5%">
            <tr>
                <td align="right">
                    <asp:Panel ID="pnlprint" runat="server" Visible="false">
                        <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                            onclick="PrintGridData()" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
      
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fieldforce Name">
                                        <ItemStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="sfNameHidden" runat="server" Value='<%#Eval("sf_name")%>' />
                                            <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%#Eval("SF_Code")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved BY" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfAll" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                          <%--<asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfFare" runat="server" Text='<%# Bind("fare") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                       <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label13" style="text-align:center;font-family:Calibri"  Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label14" style="text-align:center;font-family:Calibri"  Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label15" style="text-align:center;font-family:Calibri"  Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            
                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label16"  style="text-align:center;font-family:Calibri"  Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            
                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                     <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label17"  style="text-align:center;font-family:Calibri"  Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                         

                            <asp:TemplateField HeaderText="Miscellaneous" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmisamt" runat="server" Text='<%# Bind("mis_Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Claimed <br/>Amount(By MR)" ItemStyle-HorizontalAlign="Left">
                                                                              <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblClimedAmnt" runat="server" Text='<%# Bind("tot") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved <br/>Amount(By Admin)" ItemStyle-HorizontalAlign="Left">
                                                      
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppAmnt" runat="server" Text='<%# Bind("appAmnt") %>'></asp:Label>
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
      <%--   <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />--%>
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>