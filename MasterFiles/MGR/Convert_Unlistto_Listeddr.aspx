<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Convert_Unlistto_Listeddr.aspx.cs"
    Inherits="MasterFiles_MGR_Convert_Unlistto_Listeddr" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unlisted Customers Convert to Listed Customers</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
      <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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
          <div id="Divid" runat="server">
        </div>
        <br />
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>&nbsp;
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="true">
                        </asp:DropDownList>&nbsp;
                         <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                     <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
                    </td>
                </tr>
            </table>
            <br />
           
        </center>
        <center>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" OnRowDataBound="grdDoctor_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="  Select All" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkListedDR" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="UnListed Doctor Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("UnListedDrCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UnListed Customer Name" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="80%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("UnListedDr_Address1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="80%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="80%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="80%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                  <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
        </center>
        <br />
        <center>
        <asp:Button ID="btnConvert" runat="server" Text="Convert to Listed Doctor" OnClientClick="return confirm('Do you want to Convert the Doctor(s)?') &&  confirm('Are you sure want to Convert?');" Visible="false"
                CssClass="BUTTON" onclick="btnConvert_Click" />
        </center>
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
