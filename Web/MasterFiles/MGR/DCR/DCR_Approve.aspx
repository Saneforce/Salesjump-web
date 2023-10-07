<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Approve.aspx.cs" Inherits="MasterFiles_MGR_DCR_DCR_Approve" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>MR - DCR Approval</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
         <div>
         <center>
                 <table id="tblPreview_LstDoc" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                           <asp:Label ID="lblText" runat="server" Font-Size="12px" Font-Names="TimesNewRoman" text="Daily Calls Entry For :- "></asp:Label>
                           <asp:Label ID="lblHeader" runat="server" Font-Size="Small" Font-Names="TimesNewRoman" BackColor="SeaGreen" ForeColor="AntiqueWhite"></asp:Label>
                        </td>
                    </tr>                        
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
             
                    </table>
                <table id="Table1" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblld" runat="server" Text="Listed Customer Details" Font-Size="Small" Font-Names="Verdana"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%" >
                            </asp:Table>
                        </td>
                    </tr>
                </table>

                <br />
                 <table id="tblPreview_Chem" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                         <asp:Label ID="lblch" runat="server" Text="Chemists Details" Font-Size="Small" Font-Names="Verdana"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblChem" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>

                </table>
                <br />
                 <table id="tblPreview_Stock" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                         <asp:Label ID="lblst" runat="server" Text="Stockiests Details" Font-Size="Small" Font-Names="Verdana"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblstk" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                 <table id="tblPreview_UnLstDoc" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                         <asp:Label ID="lblunls" runat="server" Text="UnListed Customer Details" Font-Size="Small" Font-Names="Verdana"  Font-Underline="true"></asp:Label>
                            <asp:Table ID="tblunlst" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
               <br />
                 <table id="tblPreview_Hos" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td align="center">
                           <asp:Label ID="lblhos" runat="server" Text="Hospital Details" Font-Size="Small" Font-Names="Verdana"  Font-Underline="true" ></asp:Label>
                            <asp:Table ID="tblhos" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <br />
              
                 <br />
                 <table id="tblPreview_Remarks" runat="server" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Remarks" Font-Size="Small" Font-Names="Verdana" Font-Underline="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Font-Size="Small" Font-Names="Verdana">&nbsp;&nbsp;&nbsp;</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPreview_Remarks" runat="server" Font-Size="Small" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                </table>

                   <br />  
                   
                   </center>          
                <div style="margin-left:40%">
                    <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Text="Approve DCR" OnClick="btnSave_Click" />
                    &nbsp
                    <asp:Button ID="btnReject" CssClass="BUTTON" runat="server" Text="Reject DCR" OnClick="btnReject_Click" />
                    &nbsp                               
                    <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                    &nbsp
                     <asp:Button ID="btnSubmit" CssClass="BUTTON" runat="server" Visible="false" Text="Back to Field Force" OnClick="btnSubmit_Click" />
                </div>
    </div>
    </form>
</body>
</html>
