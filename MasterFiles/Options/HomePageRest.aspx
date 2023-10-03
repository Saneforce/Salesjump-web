<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePageRest.aspx.cs" Inherits="MasterFiles_Options_HomePageRest" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval Mandatory Setup</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
        <style type="text/css">
        #tblDivisionDtls
        {
            margin-left: 300px;
        }
        #tblLocationDtls
        {
            margin-left: 300px;
        }
        .style2
        {
            width: 92px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
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
        .padding
        {
            padding:3px;
        }
        td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
           .chkNew label 
{  
    padding-left: 5px; 
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <ucl:Menu ID="menu1" runat="server" />
    <br />
    <center>
     <table cellpadding="4" cellspacing="4" align="center" style="border: solid 1px #347C17;
                border-collapse: collapse" >
       <tr>
                    <td align="left">
                        <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="chkNew"
                            BackColor="#FEFCFF" ForeColor="#7F525D" CellPadding="2" CellSpacing="5" Width="600px"
                            Font-Names="Calibri">
                            <asp:ListItem Value="0" Text="DCR"></asp:ListItem>
                            <asp:ListItem Value="1" Text="TP"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Leave"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Expense"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Listed dr Addition"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Listed dr Deactivation"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Listed dr Addition against Deactivation"></asp:ListItem>
                            <asp:ListItem Value="7" Text="SS Entry"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Doctor Service Form"></asp:ListItem>

                        </asp:CheckBoxList>
                        
                    </td>
                </tr>
    </table>
    </center>
    <br />
    <center>
       <asp:Button ID="btnSave" runat="server" Text="Update"  CssClass="BUTTON" Width="80px" Height="25px"
            onclick="btnSave_Click"/>
    </center>
    </div>
    </form>
</body>
</html>
