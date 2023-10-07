<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_BulkUpload.aspx.cs" Inherits="MasterFiles_Options_Listeddr_BulkUpload" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Customer Upload - Bulk</title>
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
        #divdr
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 130px;
        }
        #detail
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 130px;
            padding: 2px;
        }
          #divcat
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 90px;
        }
        #detailcat
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 90px;
            padding: 2px;
        }
            #divTerr
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }
        #detailTerr
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
     
            #divcls
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }
        #detailCls
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <ucl:Menu ID="menu1" runat="server" />
    <br />
    <asp:Panel ID="pnlDoctor" Width="80%" runat="server" >
        <table width="100%" style="margin-left: 10%; " >
            <tr>
                <td>
                    <table width="90%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black;background-color:White">
                   
                        <tr>
                            <td style="padding-left:80px">
                                <asp:Label ID="lblExcel" runat="server" SkinID="lblMand" Font-Size="Small" Font-Names="Verdana">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:80px">
                                <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                    Text="Deactivate Existing Customer List ( if Yes then Check this Option )" 
                                    oncheckedchanged="chkDeact_CheckedChanged" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button1" runat="server" Width="70px" Height="25px" BackColor="BurlyWood" Font-Size="Medium"
                                    Text="Upload" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid Black; padding-left:80px; ">
                                <asp:Label ID="lblIns" runat="server" Text="Note:" ForeColor="Red"></asp:Label>
                           <br />
                                <asp:Label ID="Label1" runat="server" Width="280px" Font-Size="11px" Font-Names="Verdana" Text="1) Sheet Name Must be 'UPL_Customer_Bulk'"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblExc" runat="server" Text="Excel Format File" Font-Size="Medium"></asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana"
                                    Text="Download Here" OnClick="lnkDownload_Click"> 
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                 <td style="width:2%">
                                <br />
                            </td>
                <td style="width:10%; ">
                    <asp:Repeater ID="rptSpec" runat="server" >
                        <HeaderTemplate>
                        
                            <div id="divdr" style="background-color:White; text-align:center">
                                Speciality</div>
                        </HeaderTemplate>
                        <ItemTemplate >
                            <div id="detail" style="background-color:White">
                                <div style="background-color:White">
                                    <%#Eval("Doc_Special_Name") %></div>
                                <%--   <asp:Literal ID="litName" Text='<%#Eval("Doc_Special_Name") %>' runat="server"></asp:Literal>--%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                 <td style="width:7%; ">
                    <asp:Repeater ID="rptCat" runat="server">
                        <HeaderTemplate>
                            <div id="divcat" style="background-color:White; text-align:center">
                                Category</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="detailcat" style="background-color:White;">
                                <div> <%#Eval("Doc_Cat_Name")%></div>                               
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
              
                  <td style="width:8%">
                    <asp:Repeater ID="rptcls" runat="server" >
                        <HeaderTemplate>
                            <div id="divcls" style="background-color:White; text-align:center">
                               Class</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="detailCls" style="background-color:White;">
                                <div>
                                    <%#Eval("Doc_ClsName")%></div>                               
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                  <td style="width:8%">
                    <asp:Repeater ID="rptTerr" runat="server" >
                        <HeaderTemplate>
                            <div id="divTerr" style="background-color:White; text-align:center">
                                Territory</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="detailTerr" style="background-color:White;">
                                <div>
                                    <%#Eval("Territory_Name")%></div>                               
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </asp:Panel>
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
