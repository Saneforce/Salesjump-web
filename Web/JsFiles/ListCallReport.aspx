<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListCallReport.aspx.cs" Inherits="MIS_Reports_ListCallReport" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missed Call Report</title>
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
        .Nested_ChildGrid td
        {
            background-color: #fff !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .Nested_ChildGrid th
        {
            background-color: #2B579A !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <center>
            <br />
            <table width="40%">
                <tr>
                    <td>
                        <asp:Label ID="lblFF" runat="server" Text="Field Force"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFMonth" runat="server" Text="From Month"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
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
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblFYear" runat="server" Text="From Year"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="2011"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2012"></asp:ListItem>
                            <asp:ListItem Value="3" Text="2013"></asp:ListItem>
                            <asp:ListItem Value="4" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="5" Text="2015"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTMonth" runat="server" Text="To Month"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
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
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTYear" runat="server" Text="To Year"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="2011"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2012"></asp:ListItem>
                            <asp:ListItem Value="3" Text="2013"></asp:ListItem>
                            <asp:ListItem Value="4" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="5" Text="2015"></asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMode" runat="server" Text="Mode"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMode" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Listed Doctor"></asp:ListItem>
                            <asp:ListItem Value="2" Text="SDP"></asp:ListItem>
                            <asp:ListItem Value="3" Text="HQ/EX/OS"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Category"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Speciality"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Class"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" 
                onclick="btnGo_Click"/>
            <br />

            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="50%">
            </asp:Table>
            
    <asp:Panel ID="pnl" runat="server" BorderStyle="Solid" Visible="false">
    <table width="50%" align="center" border="1">
        <tbody> 
            <tr>
                <td>
                    <asp:TreeView ID="TreeView1" runat="server" BorderStyle="Solid" ExpandDepth="0">
                        <LeafNodeStyle BorderStyle="Solid" />
                        <NodeStyle BorderStyle="Solid" ChildNodesPadding="3px" HorizontalPadding="2px" 
                            NodeSpacing="2px" VerticalPadding="2px" />
                    </asp:TreeView>
                </td> 
            </tr> 
        </tbody>
    </table>
    </asp:Panel>
        </center>         
    </div>
    </form>
</body>
</html>
