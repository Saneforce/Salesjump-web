<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Spec_List.aspx.cs" Inherits="MasterFiles_MR_Doctor_Spec_List" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl1" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Customer-Speciality List</title>
       <link type="text/css" rel="stylesheet" href="../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
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
    
     .gridview1{
    background-color:#99B7B7;
    border-style:none;
   padding:2px;
   margin:2% auto;
   
   
}

 .gridview1 a{
  margin:auto 1%;
  border-style:none;
    border-radius:50%;
      background-color:#444;
      padding:5px 7px 5px 7px;
      color:#fff;
      text-decoration:none;
      -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;
     
}
.gridview1 a:hover{
    background-color:#1e8d12;
    color:#fff;
}
.gridview1 td{
    border-style:none;
}
.gridview1 span{
    background-color:#ae2676;
    color:#fff;
     -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;

    border-radius:50%;
    padding:5px 7px 5px 7px;
}
   </style>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
<table width="95%" cellpadding="0" cellspacing="0" align="center" frame="box">
            <tr>
                <td>
                    <table id="Table2" runat="server" width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                    text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" style="width: 45%">
                                <asp:Label ID="lblHeading" Text="Company List" runat="server" CssClass="under" Style="text-transform: capitalize;
                                    font-size: 14px; text-align: center;" ForeColor="#336277" Font-Bold="True" Font-Names="Verdana">
                                </asp:Label>
                            </td>
                            <td align="right" class="style3" style="width: 55%">
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Visible="false" Height="25px" Width="60px"
                                    Text="Back"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <div>
      <div id="Divid" runat="server"></div>
     <br />
     <center>
          <table align="center" style="width: 50%">
    <tbody>
        <tr>
               <td colspan="2" align="center">
               <asp:GridView ID="grdDocSpe" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" 
                     EmptyDataText="No Records Found"  onpageindexchanging="grdDocSpe_PageIndexChanging"                                    
                     GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                       AlternatingRowStyle-CssClass="alt">
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="gridview1"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Speciality Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocSpeCode" runat="server" Text='<%#Eval("Doc_Special_Code")%>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Doc_Special_SName"  HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                                <asp:TextBox ID="txtDoc_Spe_SName" runat="server" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Doc_Special_SName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Spe_SName" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Doc_Special_Name"  ItemStyle-HorizontalAlign="Left" HeaderText="Speciality Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDocSpeName"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="100" Text='<%# Bind("Doc_Special_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocSpeName" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                      
                   </Columns>
                     <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
              </td>
             </tr>
            </tbody>
          </table> 
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
