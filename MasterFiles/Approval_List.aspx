<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval_List.aspx.cs" Inherits="MasterFiles_Approval_List" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approval - Changes (Fieldforce wise)</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .style1
        {
            width: 2.6%;
            height: 64px;
        }
        .style2
        {
            width: 50%;
            height: 64px;
        }
        
         .gridview1{
    background-color:#666699;
    border-style:none;
   padding:2px;
   margin:2% auto;
   
   
}

 .gridview1 a{
  margin:auto 1%;
  border-style:none;
    border-radius:50%;
      background-color:#444;
      padding:5px 7px 5px 7px;
      color:#fff;
      text-decoration:none;
      -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;
     
}
.gridview1 a:hover{
    background-color:#1e8d12;
    color:#fff;
}
.gridview1 td{
    border-style:none;
}
.gridview1 span{
    background-color:#ae2676;
    color:#fff;
     -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;

    border-radius:50%;
    padding:5px 7px 5px 7px;
}
      
    </style>
</head>
 <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(function () {
         var $txt = $('input[id$=txtNew]');
         var $ddl = $('select[id$=ddlFilter]');
         var $items = $('select[id$=ddlFilter] option');

         $txt.keyup(function () {
             searchDdl($txt.val());
         });

         function searchDdl(item) {
             $ddl.empty();
             var exp = new RegExp(item, "i");
             var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

             if (arr.length > 0) {
                 countItemsFound(arr.length);
                 $.each(arr, function () {
                     $ddl.append(this);
                     $ddl.get(0).selectedIndex = 0;
                 }
                    );
             }
             else {
                 countItemsFound(arr.length);
                 $ddl.append("<option>No Items Found</option>");
             }
         }

         function countItemsFound(num) {
             $("#para").empty();
             if ($txt.val().length) {
                 $("#para").html(num + " items found");
             }

         }
     });
    </script>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <table width="100%">
            <tr>
                <td style="width:3.5%" />
                <td >
                    <asp:Button ID="btnApproval" runat="server" CssClass="BUTTON" Width="120px" Height="25px" Text="Approval Edit"
                        OnClick="btnApproval_Click" />
                   <%-- <table border="0" id="tblMgrDtls" align="right" style="width: 43%">
                        <tr style="height: 30px">
                            <td>
                                <asp:Label ID="lblFilter" runat="server" Text="Select the Manager"></asp:Label>
                                &nbsp;
                                <asp:DropDownList ID="ddlFilter" TabIndex="1" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
                            </td>
                        </tr>
                    </table>--%>
                 </td>
                 </tr>
                 </table>
                 <br />

                 <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td style="width: 3.5%" />
                        <td align="left" style="width: 50%">
                            <asp:Label ID="SearchBy" Font-Bold="true" runat="server" Text="SearchBy" ForeColor="Purple">
                            </asp:Label>
                            &nbsp;  
                           <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                               <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                                 <asp:ListItem Value="sf_emp_id">Employee Id</asp:ListItem>
                                <%--<asp:ListItem Value="StateName">State</asp:ListItem>--%>
                                <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" Width="150px" CssClass="TEXTAREA"
                                Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'" OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="30px" Height="25px" CssClass="BUTTON" Visible="false">
                            </asp:Button>
                        </td>
                        <td width="50%">
                                   <asp:Label ID="lblFilter" runat="server" ForeColor="Purple" Font-Bold="true"  Text="Select the Manager"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
            ToolTip="Enter Text Here"></asp:TextBox>
                                   <asp:DropDownList ID="ddlFilter" TabIndex="1" runat="server"  Width="300px"  SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                     <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                      </asp:DropDownList>
                                   &nbsp;
                                   <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
                                   </td>
                    </tr>
                </tbody>
            </table>
                  <%--<table width="100%" align="center">
                    <tbody>
                    <tr>
                        <td class="style1" />
                        <td align="left" class="style2">
                            <asp:Label ID="SearchBy" Font-Bold="true" ForeColor="Purple"  runat="server" Text="SearchBy">
                            </asp:Label>
                            &nbsp;  
                            <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                               <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>--%>
                                <%--<asp:ListItem Value="StateName">State</asp:ListItem>
                                <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" Width="150px" CssClass="TEXTAREA"
                                Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'" OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="30px" Height="25px" CssClass="BUTTON" Visible="false">
                            </asp:Button>


                            <table border="0" id="tblMgrDtls" align="right" style="width: 43%">
                                 <tr style="height: 30px">
                                   <td>
                                   <asp:Label ID="lblFilter" runat="server" ForeColor="Purple" Text="Select the Manager"></asp:Label>
                                    &nbsp;
                                   <asp:DropDownList ID="ddlFilter" TabIndex="1" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                   &nbsp;
                                   <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
                                   </td>
                                </tr>
                         </table>

                        </td>
                       </tr>
                </tbody>
            </table>--%>
            <br />
                    <table width="100%" align="center">
                        <tbody>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="grdSalesForce_PageIndexChanging"
                                        OnPreRender="grdSalesForce_PreRender" OnRowCreated="grdSalesForce_RowCreated" OnRowEditing="grdSalesForce_RowEditing"
                                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                         OnRowUpdating="grdSalesForce_RowUpdating" OnRowCancelingEdit="grdSalesForce_RowCancelingEdit" OnRowDataBound="grdSalesForce_RowDataBound"
                                        ShowHeader="False">
                                        <HeaderStyle Font-Bold="False" />
                                        <PagerStyle CssClass="gridview1"></PagerStyle>
                                        <RowStyle Wrap="true" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" >
                                               
                                                <ItemStyle BorderStyle="Solid" Width="200px" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" >
                                               
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting To">
                                              
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="170px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting" runat="server" Text='<%# Bind("Reporting_To") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting" Visible="false" >
                                              
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="170px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReport" runat="server" Text='<%# Bind("Reporting") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCRReporting" runat="server" Text='<%# Bind("DCR_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDCR" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TP_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("TP_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTP" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLstReporting" runat="server" Text='<%# Bind("LstDr_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlLstDr" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLeaveReporting" runat="server" Text='<%# Bind("Leave_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlLeave" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SS_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSSReporting" runat="server" Text='<%# Bind("SS_AM") %>'></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlSS" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expense_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpReporting" runat="server" Text='<%# Bind("Expense_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlExp" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Otr_AM">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOtrReporting" runat="server" Text='<%# Bind("Otr_AM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlOtr" runat="server" SkinID="ddlRequired" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                     </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                        ShowEditButton="True">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"  HorizontalAlign="Center">
                                        </HeaderStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" 
                                            HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ItemStyle>
                                    </asp:CommandField>
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
