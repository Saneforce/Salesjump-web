<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProduct_Exp_Detail1.aspx.cs" Inherits="MIS_Reports_rptProduct_Exp_Detail1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../css/Report.css" />
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
                <asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand" ></asp:Label>
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
     <br />
    <center>
    <%--<asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand" ></asp:Label>--%>
    <br />
    <br />
    </center>

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
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdDr" runat="server" Width="70%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdDr_RowDataBound" 
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="ListedDrCode" ItemStyle-HorizontalAlign="Left" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrCode" runat="server" Width="120px" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblforce" runat="server" Width="120px" Text='<%#Eval("Fieldforce_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText ="Product Name" ItemStyle-HorizontalAlign="Left" >
                                   <ItemTemplate >
                                      <asp:Label ID="lblprod_name" runat="server" Width="120px" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                   </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" 
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrName" runat="server" Width="150px" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblquali" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Count" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ControlStyle Width="40%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblVisitCount" runat="server"></asp:Label>
                                </ItemTemplate>
                               </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Date of Visit" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblVisitDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Worked With" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="80%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblworked_with" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                               
                            </Columns>
                               <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
