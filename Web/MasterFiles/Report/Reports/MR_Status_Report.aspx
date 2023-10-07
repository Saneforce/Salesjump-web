<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MR_Status_Report.aspx.cs" Inherits="Reports_MR_Status_Report" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FieldForce Status Report</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        #tbl
        {
            border-collapse: collapse;
        }
        table, td, th
        {
            border: 1px solid black;
        }
        #tblSFRpt
        {
        }
        #tblLocationDtls
        {
            margin-left: 300px;
        }
        .style2
        {
            width: 50px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
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
            $('#btnSubmit').click(function () {

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var Field1 = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field1 == "") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var State = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (State == "---Select---") { alert("Select State Name."); $('#ddlFieldForce').focus(); return false; }


            });
        }); 
    </script>
    <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <center>
            <br />
            <table id="tblSFRpt" cellpadding="0" cellspacing="8">
                <tr>
                    <td align="left" class="lblSpace">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblView" runat="server" Text="View By" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rdoMGRState" runat="server" RepeatDirection="Horizontal"
                            Font-Names="Verdana" Font-Size="11px" AutoPostBack="true" OnSelectedIndexChanged="rdoMGRState_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="FieldForce-wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="1" Text="State-wise"></asp:ListItem>--%>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblState" runat="server" Text="State" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr >   
                <td height="5px"></td>             
                </tr>                
                <tr>
                    <td colspan="2" align="center">
                        <asp:CheckBox ID="chkDeactive" Checked="false" Text="Include Deactive List" SkinID="lblMand"
                         runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" CssClass="btnnew" Height="25px"
                Text="View" OnClick="btnSubmit_Click" />

                 <label id="lblwidth" runat="server" width="300px"></label>
               <asp:LinkButton ID="btnExcel"  runat="server" Text="Export to Excel"  Font-Names="Verdana" Font-Bold="true" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="0" Height="25px" Width="120px"
                                    OnClick="btnExcel_Click"></asp:LinkButton>
        </center>
        <br />
        <br />
        <center>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="80%">
            </asp:Table>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <asp:GridView ID="GrdDoctor" runat="server" Width="90%" HorizontalAlign="Center"
                AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found" GridLines="Both"
                CssClass="mGrid" OnRowDataBound="GrdDoctor_DataBound" ShowFooter="True" AlternatingRowStyle-CssClass="alt">
                <FooterStyle HorizontalAlign="Center" />
                <Columns>                
                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White" Visible="false">
                        <ControlStyle Width="20%"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSfCode" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Emp_Code" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="80px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lbUsrDfd_UserName" runat="server" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="400px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSf" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="80px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lbSf_HQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="80px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lbsf_Designation_Short_Name" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="State_Name" ItemStyle-HorizontalAlign="Left"  
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="150px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblState_Name" runat="server" Text='<%# Bind("State_Name") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="First Level Manager" ItemStyle-HorizontalAlign="Left" 
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="200px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblReporting_Manager1" runat="server" Text='<%# Bind("Reporting_Manager1") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Second Level Manager" ItemStyle-HorizontalAlign="Left" 
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="200px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblReporting_Manager2" runat="server" Text='<%# Bind("Reporting_Manager2") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" Visible="false" 
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDivision_Code" runat="server" Text='<%# Bind("Division_Code") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterDivision_Code" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active Territory" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblActive_Territory" Target="_blank" 
                            NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=1&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("Active_Territory") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:HyperLink ID="lblActTerrTotal"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=1&status=0")%>' Target="_blank" runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DeActive Territory" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblDeActive_Territory" Target="_blank" 
                             NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=1&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("DeActive_Territory") %>'></asp:HyperLink>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:HyperLink ID="lblDeActiveTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=1&status=1")%>'
                             runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active ListedDR" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblActive_ListedDR" Target="_blank" 
                             NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=2&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("Active_ListedDR") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:HyperLink ID="lblActiveLstDRTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=2&status=0")%>'
                             runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DeActive ListedDR" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblDeActive_ListedDR" Target="_blank" 
                             NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=2&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("DeActive_ListedDR") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:HyperLink ID="lblDeActiveLstDRTotal" Target="_blank"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=2&status=1")%>'
                             runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active UnListedDR" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblActive_UnListedDR" Target="_blank" 
                            NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=3&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("Active_UnListedDR") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:HyperLink ID="lblActiveUnLstDRTotal" Target="_blank"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=3&status=0")%>'
                             runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="DeActive UnListedDR" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblDeActive_UnListedDR" Target="_blank" 
                            NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=3&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("DeActive_UnListedDR") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:HyperLink ID="lblDeActiveUnLstDRTotal" Target="_blank"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=3&status=1")%>' 
                            runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active Chemists" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblActive_Chemists" Target="_blank" 
                            NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=4&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' 
                            runat="server" Text='<%# Bind("Active_Chemists") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:HyperLink ID="lblActiveChemistTotal" Target="_blank"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=4&status=0")%>' 
                            runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DeActive Chemists" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblDeActive_Chemists" Target="_blank" 
                            NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=4&status=1", Eval("Sf_Code"),Eval("sf_name"))%>'
                             runat="server" Text='<%# Bind("DeActive_Chemists") %>'></asp:HyperLink>
                        </ItemTemplate>
                          <FooterTemplate>
                            <asp:HyperLink ID="lblDeActiveChemistTotal" Target="_blank"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=4&status=1")%>' 
                            runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active Stockiest" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblActive_Stockiest" runat="server" Target="_blank"
                             NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=5&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' Text='<%# Bind("Active_Stockiest") %>'></asp:HyperLink>
                        </ItemTemplate>
                          <FooterTemplate>
                            <asp:HyperLink ID="lblActiveStockTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=5&status=0")%>' 
                            runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DeActive Stockiest" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="5%"></ControlStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lblDeActive_Stockiest" runat="server" Target="_blank" 
                            NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=5&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' Text='<%# Bind("DeActive_Stockiest") %>'></asp:HyperLink>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:HyperLink ID="lblDeActiveStockTotal" Target="_blank"  NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=5&status=1")%>' 
                            runat="server"></asp:HyperLink>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
            </asp:Panel>
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
