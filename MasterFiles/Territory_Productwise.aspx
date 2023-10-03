<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Productwise.aspx.cs" Inherits="MasterFiles_Territory_Productwise" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory - Productwise View</title>
  <link type="text/css" rel="stylesheet" href="../css/style.css" />
  <script type="text/javascript">
      function PrintGridData() {

          var prtGrid = document.getElementById('<%=grdProduct.ClientID %>');
   
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
                            $('#btnProduct').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnProduct').click(function () {
                var Prod = $('#<%=ddlProduct.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Sub Division Name."); $('#ddlProduct').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <ucl:Menu ID="menu1" runat="server" />
        <br />

        <center>
      <table>
    <tr>
    <td align="left" class="stylespc">
    <asp:Label ID="lblProduct" runat="server" SkinID="lblMand" Text="Territory Name"></asp:Label></td>
    <td align="left" class="stylespc">
    <asp:DropDownList ID="ddlProduct" SkinID="ddlRequired" runat="server" ></asp:DropDownList>
    </td>

    </tr>
    </table>
    </center>
    <br />
    <center>
    <asp:Button ID="btnProduct" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON"  
            onclick="btnProduct_Click" /> 
    </center>
    <br />
    <table align="right" style="margin-right:7.5%">
    <tr>
    <td align="right">
      <asp:Panel ID="pnlprint" runat="server" Visible="false"> 
    <input type="button" id="btnPrint" value="Print" style="width:60px;height:25px; background-color:LightBlue; "   onclick="PrintGridData()"  />
    
  </asp:Panel>
    </td>
    </tr>
    </table>
  
         <table width="100%" align="center">
        <tbody>    
            <tr>
         
                <td colspan="2" align="center">
                    <asp:GridView ID="grdProduct" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Records Found"         
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdProduct.PageIndex * grdProduct.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                               
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Code" SortExpression="Product_Detail_Code" Visible="false" HeaderStyle-ForeColor="white">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdCode" runat="server" Text='<%#Eval("Product_Detail_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                               
                                <ItemTemplate>
                                    <asp:Label ID="lblProName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">                              
                                <ItemTemplate>
                                    <asp:Label ID="lblProDesc" runat="server" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left">                              
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleUn" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">                              
                                <ItemTemplate>
                                    <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Product_Cat_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Group" ItemStyle-HorizontalAlign="Left">                              
                                <ItemTemplate>
                                    <asp:Label ID="lblGroup" runat="server" Text='<%# Bind("Product_Grp_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                               
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
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
