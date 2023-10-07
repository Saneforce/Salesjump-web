<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distance_fixation_view.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distance Fixation View</title>
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
    <div id="mainDiv" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
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
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"
                OnClick="btnSF_Click" />
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
      </div>
      <div id="divHqId" runat="server" visible="false">
      <center><u><font class="print" style="color:blue;cursor:pointer;font-weight:bold" size="2">SFC View For The HQ : </font><font style="color:red;cursor:pointer;font-weight:bold" size="3">-<span id="hqId" runat="server"></span></font></u></center>
      </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From">
                                        <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("Town_Cat")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distance" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Distance_In_Kms") %>'></asp:Label>
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
    </div>
    </form>
</body>
</html>
