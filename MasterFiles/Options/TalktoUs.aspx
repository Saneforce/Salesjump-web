<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="TalktoUs.aspx.cs" Inherits="MasterFiles_Options_TalktoUs" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Talk to Us</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div>
       <%-- <ucl:Menu ID="menu1" runat="server" />--%>
        <br /> 
        <center>
        <table>
            <tr style="height:25px;">
                <td valign="middle" style="vertical-align:middle;">
                    <asp:Label ID="lbltalk" runat="server" Text="Talk to Us"  ></asp:Label>                                        
                </td>
                <td valign="top">
                    <asp:TextBox ID="txttalk" runat="server" TextMode="MultiLine" Height="100px" 
                        Width="600px"></asp:TextBox>
                </td>      
             </tr>
             <tr>
             <td></td>
            <%-- <td align="left">
                  <asp:CheckBox ID="chkback" runat="server" Text="Set as Home Page" />
                  </td>--%>
             </tr>
        </table> 
      
        <br />
        <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="Save" onclick="btnSubmit_Click" />
      <%--  &nbsp;&nbsp;        
        <asp:Button ID="btnDelete" runat="server" CssClass="BUTTON" Text="Delete-Add Quote" onclick="btnDelete_Click" />--%>
        </center>     
    </div>
    </form>
</body>
</html>
</asp:Content>