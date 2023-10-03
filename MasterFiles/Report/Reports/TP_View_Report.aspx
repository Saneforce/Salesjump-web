<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="TP_View_Report.aspx.cs" Inherits="Reports_TP_View_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TP View Report</title>
     <style type="text/css">
        #tblTpRpt
        {
            margin-left: 300px;
        }
       
    </style>
  
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript" language="javascript">

        function MyFunc(hypLevel,ddlFF,ddlMon,ddlYr)
        {
            var hypLevel = document.getElementById(hypLevel);
            var ddlFF = document.getElementById(ddlFF);
            var ddlMon = document.getElementById(ddlMon);
            var ddlYr = document.getElementById(ddlYr);
            //hypLevel.href =

        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
        <center>
        <br />
       <table id="tblTpRpt" cellpadding="3" cellspacing="3" width = "60%">
            <tr>
                <td>
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack = "true" SkinID="ddlRequired"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                </td>
            </tr>
            <tr >
                <td>
                    <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
            </tr>
            <tr >
                <td>
                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                </td>
               
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                        <asp:ListItem Value="2009" Text="2009"></asp:ListItem>
                        <asp:ListItem Value="2010" Text="2010"></asp:ListItem>
                        <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                        <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                        <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                        <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                        <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                        <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                        <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr >
                <td>
                    <asp:CheckBox id="chkConsolidate" runat="server" Text="  Consolidate" AutoPostBack="true"
                        oncheckedchanged="chkConsolidate_CheckedChanged" />
                </td>
                <td id="hypConsolidate" runat="server">
<%--                    <asp:HyperLink ID="hypLevel1" runat="server" Text="Level1" NavigateUrl="javascript:MyFunc();" ></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel2" runat="server" Text="Level2" NavigateUrl="#"></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel3" runat="server" Text="Level3" NavigateUrl="#"></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel4" runat="server" Text="Level4" NavigateUrl="#"></asp:HyperLink>
--%>            
                    <asp:RadioButtonList ID="rdoHypLevel" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Level1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Level2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Level3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Level4" Value="4"></asp:ListItem>
                    </asp:RadioButtonList>    
                </td>
            </tr>
        </table>
          <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View" 
                        onclick="btnSubmit_Click" />
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="80%">
        </asp:Table>
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
</asp:Content>