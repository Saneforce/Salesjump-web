<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAgainstDeact_App.aspx.cs"
    Inherits="MasterFiles_MGR_AddAgainstDeact_App" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Against Deactivation Approval</title>
     <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />

           <script type="text/javascript">
               function checkAll(objRef) {
                   var GridView = objRef.parentNode.parentNode.parentNode;
                   var inputList = GridView.getElementsByTagName("input");
                   for (var i = 0; i < inputList.length; i++) {
                       //Get the Cell To find out ColumnIndex
                       var row = inputList[i].parentNode.parentNode;
                       if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                           if (objRef.checked) {
             
                               inputList[i].checked = true;
                           }
                           else {             
                               inputList[i].checked = false;
                           }
                       }
                   }
               }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
       <br />
    <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div>    
        <br />
       
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                            CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdDoctor_RowDataBound" >
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" Text="  Select All" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkListedDR" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Customer Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <%--     <asp:TemplateField HeaderText="Class" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SLV No" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblslvno" runat="server" Text='<%# Bind("SLVNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mode" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmode" runat="server" Text='<%# Bind("mode") %>'></asp:Label>
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
        <br />
        <center>
            <asp:Button ID="btnApprove" runat="server" CssClass="BUTTON" Text="Approve - Add Lst Drs"
                Width="170px" onclick="btnApprove_Click" />&nbsp;
       <%--     <asp:Button ID="btnReject" runat="server" CssClass="BUTTON" Text="Reject" 
                Width="70px" onclick="btnReject_Click"
               />--%>
        </center>
    </div>
    </form>
</body>
</html>
