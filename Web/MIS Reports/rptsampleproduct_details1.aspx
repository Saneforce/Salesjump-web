<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptsampleproduct_details1.aspx.cs" Inherits="MIS_Reports_rptsampleproduct_details1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Fieldforce_Name, Year, Month, sCurrentDate,Product_Code_SlNo) {
            popUpObj = window.open("rptsampleproduct_details2.aspx?sf_code=" + sfcode + "&Fieldforce_Name=" + Fieldforce_Name + "&Year=" + Year + "&Month=" + Month  + "&sCurrentDate=" + sCurrentDate + "&Product_Code_SlNo=" + Product_Code_SlNo,
     "_blank",
    "ModalPopUp," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=600," +
    "height=400," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
        }
    

   
    </script>
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
            <td width="20%"></td><td></td>
                <td width="80%" align="center" >
                <asp:Label ID="lblProd" runat="server" Text="Sample Details" SkinID="lblMand" ></asp:Label>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPrint_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnExcel_Click" />
                            </td>
                           
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
     <div>
     <br />
    <center>
    <%--<asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand" ></asp:Label>--%>
    <br />
    <br />
    </center>
    <center >
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%" align="center">
            <tbody>
            <tr>
            <%--<td ></td>--%>
              <td align="center" >
               <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:" ></asp:Label>
               <asp:Label ID="lblname" runat="server" SkinID="lblMand"></asp:Label>
              </td>
              
            </tr>
               <%-- <tr>
                    <td align="center">
                        <asp:GridView ID="grdDr" runat="server" Width="60%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"  
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  HeaderText="Product_Code_SlNo" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrCode" runat="server" Width="120px" Text='<%#Eval("Product_Code_SlNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblforce" runat="server" Width="120px" Text='<%#Eval("Fieldforce_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                
                                <asp:TemplateField  HeaderText="HQ" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblforce" runat="server" Width="120px" Text='<%#Eval("sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                           

                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblquali" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Sample" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblquali" runat="server" Text='<%# Bind("Sample") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                                                                                    
                           <%-- </Columns>
                               <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>--%>
            </tbody>
        </table>
         </asp:Panel>
        <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="70%" >
                            </asp:Table>
                        </td>
                    </tr>
                </tbody>
            </table>  
       
        </center>
    </div>
    </form>
</body>
</html>
