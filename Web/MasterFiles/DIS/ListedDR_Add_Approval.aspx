<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDR_Add_Approval.aspx.cs" Inherits="MasterFiles_MGR_ListedDR_Add_Approval" %>
<%--<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer Addition - Approval</title>
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
                           //If the header checkbox is checked
                           //check all checkboxes
                           //and highlight all rows
                           //row.style.backgroundColor = "aqua";
                           inputList[i].checked = true;
                       }
                       else {
                           //If the header checkbox is checked
                           //uncheck all checkboxes
                           //and change rowcolor back to original 
                           //                    if (row.rowIndex % 2 == 0) {
                           //                        //Alternating Row Color
                           //                        row.style.backgroundColor = "#C2D69B";
                           //                    }
                           //                    else {
                           //                        row.style.backgroundColor = "white";
                           //                    }
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
   <%--  <ucl:Menu ID="menu1" runat="server" /> --%>
   <br />
    <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div>    
        <br />
       
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" 
                            OnRowCreated="grdDoctor_RowCreated"  OnRowDataBound="grdDoctor_RowDataBound" 
                            AllowSorting="True" OnSorting="grdDoctor_Sorting">
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
                                      <asp:TemplateField HeaderText="Deactivated Date" HeaderStyle-ForeColor="Black" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeact_Date" runat="server" Text='<%#Eval("ListedDr_Deactivate_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Class" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

  </div>
  <br />
  <center>
        <asp:Button ID="btnApprove" runat="server" CssClass="BUTTON" Text="Approve - Add Lst Drs" Width="170px" OnClick="btnApprove_Click" />&nbsp;
       <asp:Button ID="btnReject" runat="server" CssClass="BUTTON" Text="Reject" Width="70px" OnClick="btnReject_Click"  />
        </center>
  
    </form>
</body>
</html>