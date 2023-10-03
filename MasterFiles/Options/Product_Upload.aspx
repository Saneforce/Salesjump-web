<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Upload.aspx.cs" Inherits="MasterFiles_Options_Product_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Upload</title>
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
            width: 120px;
        }
        #detailcat
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
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
     
            #divBrd
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }
        #detailBrd
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
    </style>
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
    <br />
    <div>
   <asp:Panel ID="pnlProduct" Width="90%" runat="server">
        <table width="95%" style="margin-left: 10%">
            <tr>
                <td>
                    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color:White">
                      
                        <tr>
                            <td style="padding-left:80px">
                                <asp:Label ID="lblExcel" runat="server" SkinID="lblMand" Font-Size="Small" Font-Names="Verdana">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:80px">
                                <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                    Text="Deactivate Existing Product List ( if Yes then Check this Option )" 
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
                                <asp:Label ID="Label1" Font-Size="11px" Font-Names="Verdana" runat="server" Width="280px" Text="1) Sheet Name Must be 'UPL_Product_Master'"></asp:Label>
                                <br />
                                <asp:Label ID="Label2" Font-Size="11px" Font-Names="Verdana" runat="server" Text="2) Group, Category, Brand Name Must be Match Our Website"></asp:Label>                               
                                <br />
                                <asp:Label ID="Label4" Font-Size="11px" Font-Names="Verdana" runat="server" Text="3) Don't Do Any Special Formats in the Excel File"></asp:Label>
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
                <td style="width:10%">
                    <asp:Repeater ID="rptCat" runat="server">
                        <HeaderTemplate>
                            <div id="divcat" style="background-color:White;">
                                Category</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="detailcat" style="background-color:White;">
                                <div>
                                    <%#Eval("Product_Cat_Name")%></div>
                                <%--   <asp:Literal ID="litName" Text='<%#Eval("Doc_Special_Name") %>' runat="server"></asp:Literal>--%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                 <td style="width:7%">
                    <asp:Repeater ID="rptGrp" runat="server">
                        <HeaderTemplate>
                            <div id="divdr" style="background-color:White;">
                                Group</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="detail" style="background-color:White;">
                                <div> <%#Eval("Product_Grp_Name")%></div>                               
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
              
                  <td style="width:8%">
                    <asp:Repeater ID="rptBrd" runat="server" >
                        <HeaderTemplate>
                            <div id="divBrd" style="background-color:White;">
                               Brand</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="detailBrd" style="background-color:White;">
                                <div>
                                    <%#Eval("Product_Brd_Name")%></div>                               
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                
            </tr>
        </table>
    </asp:Panel>  
    </div>
    </form>
</body>
</html>
