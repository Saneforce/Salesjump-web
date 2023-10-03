<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="DoctorBusinessView.aspx.cs" Inherits="DoctorBusinessView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <style type="text/css">
      td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
  </style>
<link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <script type="text/javascript">
        function OpenReport(sfCode, monthYear) {

            window.open('DoctorBusinessViewProjects.aspx?sfCode=' + sfCode + '&monthYear=' + monthYear + '', null, 'height=500, width=400,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

        function OpenNewWindow() {
            var elemField = document.getElementById("<%= ddlFieldForce.ClientID %>");
            var ddlFromMonth = document.getElementById("<%= ddlFromMonth.ClientID %>");
            var ddlFromYear = document.getElementById("<%= ddlFromYear.ClientID %>");
            var ddlToMonth = document.getElementById("<%= ddlToMonth.ClientID %>");
            var ddlToYear = document.getElementById("<%= ddlToYear.ClientID %>");

            var sf = elemField.selectedIndex;
            var fmSI = ddlFromMonth.selectedIndex;
            var fySI = ddlFromYear.selectedIndex;
            var tmSI = ddlToMonth.selectedIndex;
            var tySI = ddlToYear.selectedIndex;

            var sfCode = elemField.options[sf].value;
            var fmCode = ddlFromMonth.options[fmSI].value;
            var fyCode = ddlFromYear.options[fySI].value;
            var tmCode = ddlToMonth.options[tmSI].value;
            var tyCode = ddlToYear.options[tySI].value;

            window.open('DoctorBusinessView_Report.aspx?sf_Code=' + sfCode + '&fromMonth=' + fmCode + '&fromYear=' + fyCode + '&toMonth=' + tmCode + '&toYear=' + tyCode + '', null, 'height=800, width=600,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>
    <ucl:Menu ID="menu" runat="server" />
    <center>
        <table>
            <tr>
                <td align="center" style="color:#8A2EE6;font-family:Verdana;font-weight:bold;text-transform: capitalize; font-size: 14px;
                                        text-align: center;">
                    <asp:Label ID="lblHead" runat="server" Text="Customer Business View" Font-Underline="True"
                        Font-Bold="True" ></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="Field Force Name" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFromMonth" runat="server" Text="From Month" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFromMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFromYear" runat="server" Text="From Year" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFromYear" runat="server" SkinID="ddlRequired">
                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblToMonth" runat="server" Text="To Month" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlToMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblToYear" runat="server" Text="To Year" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlToYear" runat="server" SkinID="ddlRequired">
                     
                    </asp:DropDownList>
                </td>
            </tr>
            </table>
            <center>
                    <asp:Button ID="CmdView" runat="server" Text="View" Width="60px" Height="25px" CssClass="BUTTON" OnClick="CmdView_Click" />
                </center>
        
        <table width="95%">
            <tr>
                <td>
                    <asp:GridView ID="gvDoctorBusiness" runat="server" AutoGenerateColumns="true" CssClass="mGrid"
                        EmptyDataText="No Records Found" 
                        OnRowDataBound="gvDoctorBusiness_RowDataBound" HeaderStyle-ForeColor="White" 
                        onrowcreated="gvDoctorBusiness_RowCreated" ShowFooter="true">
                        
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
