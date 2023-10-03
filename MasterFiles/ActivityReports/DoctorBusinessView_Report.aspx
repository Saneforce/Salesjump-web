<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorBusinessView_Report.aspx.cs" Inherits="DoctorBusinessView_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Business View Report</title>
    <link type="text/css" href="css/Report.css" rel="Stylesheet" />
    <script type="text/javascript">

        function OpenReport(sfCode, monthYear) {

            window.open('DoctorBusinessViewProjects.aspx?sfCode=' + sfCode + '&monthYear=' + monthYear + '', null, 'height=400, width=300,left=0,top=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
   <asp:ToolkitScriptManager ID="scriptmanager1" runat="server" EnablePageMethods="true"> </asp:ToolkitScriptManager>
<%--<asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
<asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    <asp:Button ID="btnClose" runat="server" Text="Close" />

</asp:Panel>

<asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>--%>
 
<asp:TextBox ID="txtContactsSearch" runat="server"></asp:TextBox>
<asp:AutoCompleteExtender ServiceMethod="GetDoctorList"
    MinimumPrefixLength="1"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txtContactsSearch"
    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false"></asp:AutoCompleteExtender>
    <div>
        <asp:GridView ID="gvDoctorBusiness" runat="server" AutoGenerateColumns="true" 
            CssClass="mGrid" EmptyDataText="No Records Found" 
            onrowdatabound="gvDoctorBusiness_RowDataBound"></asp:GridView>
    </div>
    </form>
</body>
</html>
