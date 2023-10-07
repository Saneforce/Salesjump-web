<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferTerritory.aspx.cs" Inherits="MasterFiles_MR_Territory_TransferTerritory" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Territory</title>
     <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
              .marRight
        {
            margin-right:35px;
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
     <script type="text/javascript">
         function ValidateCheckBoxList() {
             var gridView = document.getElementById("<%=grdTerritory.ClientID %>");
             var checkBoxes = gridView.getElementsByTagName("input");
             for (var i = 0; i < checkBoxes.length; i++) {
                 if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                     var flag = true;
                     var dropdowns = new Array();
                     dropdowns = gridView.getElementsByTagName('select');
                     if (dropdowns.item(i).value == "-1") 
                         {                             
                             flag = false;                              
                         }
                    
                     if (!flag) {
                         alert("Select transfer to");
                     }
                     return flag;                    
                 }
             }
             alert("Select transfer from");
             return false;            
         }
   </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="Divid" runat="server">
        </div>
   <%-- <ucl:Menu ID="menu1" runat="server" /> --%>
           <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
       <table width="90%">
      
        <tr> 
          <td align="right" width="30%">
                  <%--  <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>                    
     </tr>
     </table>
     <table align="center" width="100%">            
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdTerritory" runat="server" Width="65%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                        AutoGenerateColumns="false" onrowdatabound="grdTerritory_RowDataBound" ShowFooter="true" 
                        GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                             <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                           
                                <ItemTemplate>                                    
                                    <asp:CheckBox ID="chkTerritory" Width="20px" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="290px" HeaderText="Transfer From" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name")%>'></asp:Label>
                                </ItemTemplate>
                                 <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="Total Count"></asp:Label>
                                </FooterTemplate>                                                             
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="No of Listed Customers" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="140px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblListedDRCnt" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                </ItemTemplate>
                                 <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                <FooterTemplate>
                                  <asp:Label ID="lblTotalqty" runat="server" />
                            </FooterTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField  HeaderText="No of Customer" ItemStyle-HorizontalAlign="Center">
                              <ItemStyle Width="140px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblChemistsCnt" runat="server" Text='<%# Bind("Chemists_Count") %>' ></asp:Label>
                                </ItemTemplate>
                                 <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                <FooterTemplate>
                                   <asp:Label ID="lblTotalChemists_Count" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField  HeaderText="No of UnListed Customers" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="140px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnListedDRCnt" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                </ItemTemplate>
                                 <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                <FooterTemplate>
                                  <asp:Label ID="lblTotalUnListedDR_Count" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="No of Hospitals" ItemStyle-HorizontalAlign="Center">
                               <ItemStyle Width="140px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHospitalCnt" runat="server" Text='<%# Bind("Hospital_Count") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                <FooterTemplate>                                                                
                                 <asp:Label ID="lblTotalHospital_Count" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Transfer To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                  <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTerritory" SkinID="ddlRequired" runat="server">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                        </Columns>
                         <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" Width="80%"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />  
                        <FooterStyle BackColor="#cccccc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                    </asp:GridView>                    
                </td> 
            </tr> 
       
    </table>
    <br />
    <center>
        <asp:Button ID="btnTransfer" runat="server" Text="Transfer" CssClass="BUTTON" Width="90px"
            onclick="btnTransfer_Click" OnClientClick="return ValidateCheckBoxList()"/>
    </center>
     <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
