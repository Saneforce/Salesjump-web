﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisitDetailsReport_Level4.aspx.cs" Inherits="MIS_Reports_VisitDetailsReport_Level4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Visit Details Report</title>
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

<script type = "text/javascript">
    var popUpObj;
    function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, cmode) {
        popUpObj = window.open("VisitDetailsReport_Level5.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&cMode=" + cmode,
    "ModalPopUp_Level2",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=400," +
    "height=300," +
    "left = 450," +
    "top=100"
    );
        popUpObj.focus();
        LoadModalDiv();
    }

    function showVisitDR_type(sfcode, fmon, fyr, cmode, modeval) {
        //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
        popUpObj = window.open("VisitDocList.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&cMode=" + cmode + "&vMode=" + modeval,
    "_blank," +
    "ModalPopUp", +
    "0," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=0," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
        popUpObj.focus();
        LoadModalDiv();
    }
</script>   

</head>
<body>
    <form id="form1" runat="server">
    <div>
       <center>
                 <table id="Heading" runat="server" style="width: 95%;  font-family:Bookman Old Style; font-size:Small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                       <td align="center" >
                             <asp:Label ID="lblText" runat="server" Font-Size="Medium" Font-Names="Bookman Old Style" Font-Underline ="true" 
                                text="Visit Details For "></asp:Label>
                        </td>
                    </tr>                        
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    </table>
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both">
        </asp:Table>
    
    </div>
    </form>
</body>
</html>
