<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rejection_ReEntries.aspx.cs" Inherits="MasterFiles_Rejection_ReEntries" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rejection/ReEntries</title>
        <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
     <style type="text/css">
        .div_fixed
        {
            position: fixed;
            top: 400px;
            right: 5px;
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
           .roundCorner
{
    border-radius: 25px;
    background-color: #4F81BD;

    text-align :center;
    font-family:arial, helvetica, sans-serif;
    padding: 5px 10px 10px 10px;
    font-weight:bold;
    width:100px;
    height:30px;
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
   <div>
        <center>
            <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                            font-size: 22px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnHome" runat="server" Width="150px" CssClass="roundCorner" Height="30px" Text="Goto Home Page" OnClick="btnHome_Click"
                            BackColor="Green" ForeColor="White"  />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnLogout" runat="server" Width="90px" CssClass="roundCorner" Height="30px" Text="Logout" OnClick="btnLogout_Click"
                            BackColor="Red" ForeColor="White" />
                    </td>
                </tr>
            </table>
            <br />
             
            <table cellpadding="0" cellspacing="0" id="Table2" align="center" style="width: 100%;
                background-color: Gray; color: white">
                <tr>
                    <td align="center" width="90%">                    
                        <asp:Label ID="lblApproval" runat="server" Text="Rejection/ReEntries" Font-Size="Medium"></asp:Label>                      
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <table width="100%" align="center">
              
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbldcr" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">DCR(Rejection)</asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td align="center" >
                       <asp:GridView ID="grdDCR" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            EmptyDataText="No Data Found" GridLines="None" CssClass="mGridImg"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DCR_Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDCRDate" runat="server" Text='<%#Eval("DCR_Activity_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                             <asp:TemplateField HeaderText="Work_Type" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Rejected_By" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Reason" >
                                <ItemTemplate>
                                    <asp:Label ID="lblReasonforRejection" runat="server" Text='<%#Eval("ReasonforRejection")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                         
                            
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                        </td>
                        </tr>
             </table>    
             </center> 
             <br />
             <br />
               <center>
            <table width="100%" align="center">
              
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbltp" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">Tour Plan</asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td align="center" >
                        <asp:GridView ID="grdTP" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            EmptyDataText="No Data Found" GridLines="None" CssClass="mGridImg"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected by">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRejectBy" runat="server" Text='<%#Eval("TP_Approval_MGR")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Rejection_Reason")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Click here" DataTextField="Month" DataNavigateUrlFormatString="MR/TourPlan.aspx?reject={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                        <br />    
                        <asp:GridView ID="grdTP_Calander" runat="server" Width="60%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Data Found" GridLines="None"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tour Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rejected by">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRejectBy" runat="server" Text='<%#Eval("TP_Approval_MGR")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Rejection_Reason")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Click here" DataTextField="Month" DataNavigateUrlFormatString="MGR/TourPlan_Calen.aspx?refer={0}"
                                    DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView> 
                        </td>
                        </tr>
             </table>    
             </center> 
             <br />
             <br />
             <center>
            <table width="100%" align="center">
              
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblcrm" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">Expense</asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td align="center" >
                        <asp:Label ID="lblcrm1" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True"  >No Data Found</asp:Label>
                        </td>
                        </tr>
             </table>    
             </center> 
             <br />
             <br />
        <center>
        <asp:Panel ID="pnldoc" runat="server">
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblAdd" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">Listed Customer Addition/Deactivation (Rejection)</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                        <td align="center">
                            <asp:GridView ID="grdListedDR" runat="server" Width="60%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" EmptyDataText="No Data Found" GridLines="None"
                                CssClass="mGridImg" PagerStyle-CssClass="pgr" RowStyle-Font-Size="Smaller" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" Font-Size="Smaller" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SF Code" Visible="false">
                                        <ItemTemplate>
                                       <%--     <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                              
                                 <asp:TemplateField HeaderText="No Of Listed Doctor's" ItemStyle-HorizontalAlign="Center">
                                 <ItemTemplate>
                                 <asp:Label ID="lblDrCount" runat="server" Text='<%#Eval("LSTCount")%>'></asp:Label>
                                 </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Rejected by">
                                 <ItemTemplate>
                                 <asp:Label ID="lblReporting" runat="server" Text='<%#Eval("Listeddr_App_Mgr")%>'></asp:Label>
                                 </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Mode">
                                 <ItemTemplate>
                                 <asp:Label ID="lblMode" runat="server" Text='<%#Eval("mode")%>'></asp:Label>
                                 </ItemTemplate>

                                 </asp:TemplateField>
                                   
                                </Columns>
                                  <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            </asp:Panel>
            </center>
            <br />
         
              <center>
            <table width="100%" align="center">
              
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblleave" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">Leave</asp:Label>
                        </td>
                    </tr>
                    <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdLeave" runat="server" Width="60%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Data Found"   
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                                  <asp:TemplateField HeaderText="Leave From" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("From_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave To" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbltodate" runat="server"  Text='<%#Eval("To_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                 <%--<asp:TemplateField HeaderText="Leave Days" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldays" runat="server" Text='<%#Eval("No_of_Days")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                          
                          <asp:TemplateField HeaderText="Rejected by" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                          <asp:Label ID="lblRejectedBy" runat="server" Text='<%#Eval("Leave_App_Mgr") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                            <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Rejected_Reason") %>'></asp:Label>
                            </ItemTemplate>                            
                            </asp:TemplateField>
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td> 
            </tr> 
             </table>   
             </center> 
               <!-- Included Secondary Sales Entry -->
            <br />
            <center>
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblSecSales" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana" ForeColor="Chocolate">Secondary Sales</asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td align="center" >
                          <asp:GridView ID="grdSecSales" runat="server" Width="60%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false"  EmptyDataText="No Data found for Rejection's" 
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SF Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FieldForce Name" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                             <asp:TemplateField HeaderText="Designation" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDes" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="HQ" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Month">
                                <ItemTemplate> 
                                    <asp:Label ID="lblSSMonth" runat="server" Text='<%#Eval("Cur_Month")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblSSYear" runat="server" Text='<%#Eval("Cur_Year")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:HyperLinkField HeaderText="Click here" DataTextField="Month" 
                                DataNavigateUrlFormatString="~/MasterFiles/MR/SecSales/SecSalesEntry.aspx"
                                DataNavigateUrlFields="key_field" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                                                                                   
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                        </td>
                        </tr>
                </table>                
            </center> 


            <!-- End  -->
                <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
