<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUp.aspx.cs" Inherits="SecondarySales_SetUp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales Setup</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
      <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
   
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  
    <script type="text/javascript" language="javascript">
        function showhidetext(e) {            
            var cntrl_id = e.id;
            var qty = e.checked;            
            cntrl_id = cntrl_id.substring(0, cntrl_id.indexOf("chkSub"));
            var txt_sub = cntrl_id + 'txtSub';
            var txt_cntrl = document.getElementById(txt_sub);
            if (txt_cntrl != null) {
                var txt_cntrl_id = txt_cntrl.id;
                if (qty) {
                    txt_cntrl.setAttribute("writeOnly", true);                    
                }
                else {
                    txt_cntrl.value = '';
                    txt_cntrl.setAttribute("readOnly", true);
                }
            }
        }

        function GetCheckedCheckBox() {
            var checkedCheckBox;
            var dataGrid = document.all['GridViewRDR1_Hidden'];
            var rows = dataGrid.rows;
            for (var index = 1; index < rows.length; index++) {
                var checkBox = rows[index].cells[0].childNodes[0];
                if (checkBox.Checked)
                    checkedCheckBox = checkBox;
            }
            return checkedCheckBox;
        }
    </script>
        
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px;">
     <script type="text/javascript">
         $(document).ready(function () {
             $('#btnAddParam').click(function () {
                 if ($("#txtParamName").val() == "") { alert("Enter Parameter Name."); $('#txtParamName').focus(); return false; }
                 if ($("#txtShortName").val() == "") { alert("Enter Short Name."); $('#txtShortName').focus(); return false; }
                 var cat = $('#<%=ddlType.ClientID%> :selected').text();
                 if (cat == "Select") { alert("Select Type."); $('#ddlDesType').focus(); return false; }
             });
         });

    </script>
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <div>
        <center>
        <br />
        <table width="30%" align="center" >
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblParamName" runat="server" Text="Param Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtParamName" runat="server" SkinID="MandTxtBox" Width="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShortName" runat="server" Text="Short Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtShortName" runat="server" SkinID="MandTxtBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOpr" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="+" Text="+"></asp:ListItem>
                            <asp:ListItem Value="-" Text="-"></asp:ListItem>
                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnAddParam" runat="server"  Text="Add Param" 
                            onclick="btnAddParam_Click"/>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <br />

        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblPlus" runat="server" Text="Parameter Details (+)" ForeColor="Navy" Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>          
                     </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSecSales" runat="server" Width="95%" HorizontalAlign="Center" GridLines="None" OnRowDataBound="grdSecSales_RowDataBound" 
                            AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt"
                            onrowupdating="grdSecSales_RowUpdating" onrowediting="grdSecSales_RowEditing" onrowcancelingedit="grdSecSales_RowCancelingEdit">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Param Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSaleName"  SkinID="TxtBxAllowSymb" Width="160px" runat="server" MaxLength="50" Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisplay" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkValue" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalc" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcDis" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcSale" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFld" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);"/>
                                        <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                        ShowEditButton="True" >
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" 
                                            HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ItemStyle>
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

        <br />

        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblMinus" runat="server" Text="Parameter Details (-)" ForeColor="Navy" Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSecSalesMinus" runat="server" Width="95%" HorizontalAlign="Center" GridLines="None" OnRowDataBound="grdSecSalesMinus_RowDataBound" 
                            AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt"
                            onrowupdating="grdSecSalesMinus_RowUpdating" onrowediting="grdSecSalesMinus_RowEditing" onrowcancelingedit="grdSecSalesMinus_RowCancelingEdit">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Param Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSaleName"  SkinID="TxtBxAllowSymb" Width="160px" runat="server" MaxLength="50" Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisplay" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkValue" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalc" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcDis" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcSale" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFld" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);"/>
                                        <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                        ShowEditButton="True" >
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" 
                                            HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ItemStyle>
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

        <br />

        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:Label ID="LblOth" runat="server" Text="Parameter Details (Others)" ForeColor="Navy" Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSecSalesOthers" runat="server" Width="95%" HorizontalAlign="Center" GridLines="None" OnRowDataBound="grdSecSalesOthers_RowDataBound" 
                            AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt"
                            onrowupdating="grdSecSalesOthers_RowUpdating" onrowediting="grdSecSalesOthers_RowEditing" onrowcancelingedit="grdSecSalesOthers_RowCancelingEdit">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Param Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSaleName"  SkinID="TxtBxAllowSymb" Width="160px" runat="server" MaxLength="50" Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisplay" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkValue" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalc" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcDis" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcSale" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFld" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);"/>
                                        <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                        ShowEditButton="True" >
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" 
                                            HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ItemStyle>
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>


        <br />

        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCol" runat="server" Text="Parameter Details (User Defined Columns)" ForeColor="Navy" Font-Size="Small" Font-Bold="true" Font-Underline="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdCol" runat="server" Width="95%" HorizontalAlign="Center" GridLines="None" OnRowDataBound="grdCol_RowDataBound" 
                            AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt"
                            onrowupdating="grdCol_RowUpdating" onrowediting="grdCol_RowEditing" onrowcancelingedit="grdCol_RowCancelingEdit">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleCode" runat="server" Text='<%#Eval("Sec_Sale_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Param Name" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSaleName"  SkinID="TxtBxAllowSymb" Width="160px" runat="server" MaxLength="50" Text='<%# Bind("Sec_Sale_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("Sec_Sale_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisplay" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkValue" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFwd" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disable Mode" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDisable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalc" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculation with Disable" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcDis" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated as Sale" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCalcSale" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Forward Field" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCarryFld" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Needed" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSub" runat="server" onclick="showhidetext(this);"/>
                                        <asp:TextBox ID="txtSub" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                        <asp:TextBox ID="txtSub1" runat="server" Width="50" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrder" runat="server" Width="40" onkeypress="CheckNumeric(event);" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderStyle-ForeColor="white" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                        ShowEditButton="True" >
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle ForeColor="DarkBlue" 
                                            HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ItemStyle>
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

        <br />


        <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save" 
                CssClass="BUTTON" onclick="btnSubmit_Click"/>
        &nbsp;
        <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" 
                Height="25px" Text="Clear" onclick="btnClear_Click" />
     </center>         
   
    </div>
    </form>
</body>
</html>
