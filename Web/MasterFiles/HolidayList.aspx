<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayList.aspx.cs" Inherits="MasterFiles_HolidayList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday List</title>
  
        <link type="text/css" rel="stylesheet" href="../css/style.css" />     
        <style type="text/css">
        body
        {
        
        }
         .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: gray;
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
        
        </style>

    <%--<script type="text/javascript">
            var EmptyDataText = "No Records Found"
            function ShowEmptyDataHeader() {
                var Grid = document.getElementById("<%=grdHoliday.ClientID%>");
                var cell = Grid.getElementsByTagName("TD")[0];
                if (cell != null && cell.innerHTML == EmptyDataText) {
                    document.getElementById("dvHeader").style.display = "block";
                }
            }
            window.onload = ShowEmptyDataHeader;
    </script>--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
        <ucl:Menu ID="menu1" runat="server" /> 
          <br />

      <table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                <asp:Button ID="btnNew" runat="server" CssClass="BUTTON" Text="Add/Edit"  Width="90px" Height="25px" onClick="btnNew_Click" />&nbsp;
            <%-- <asp:Button ID="Button1" runat="server" CssClass="BUTTON" Text="old" onClick="btnold_Click" />&nbsp;--%>
                <asp:Button ID="btnView" runat="server" CssClass="BUTTON" Text="Calendar View"  Width="100px" Height="25px" OnClick="btnView_Click" />&nbsp;
                 <asp:Button ID="btnCons" runat="server" CssClass="BUTTON" 
                    Text="Consolidated View"  Width="130px" Height="25px" onclick="btnCons_Click" 
                      />
            </td>
            <td></td>
        </tr>
      </table>
       <br />
     <table  width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                    <asp:Label ID="lblsr" runat="server" Text="Select the State" ></asp:Label>
                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                        onselectedindexchanged="ddlState_SelectedIndexChanged">                    
                    </asp:DropDownList>                    
          
              <asp:Label ID="lblYear" runat="server" Text="Year "></asp:Label>
               <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                onselectedindexchanged="ddlYear_SelectedIndexChanged">
               </asp:DropDownList>
             </td>        
             </tr>
       </table>
       <br />
      <%-- <div id="dvHeader"></div> --%>
     <table align="center" style="width: 100%">
    <tbody>
        <tr>
            <td colspan="2" align="center">
               <asp:GridView ID="grdHoliday" runat="server" Width="85%" HorizontalAlign="Center" 
                     AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                     onrowupdating="grdHoliday_RowUpdating" onrowediting="grdHoliday_RowEditing"
                     onrowdeleting="grdHoliday_RowDeleting" EmptyDataText="No Records Found"
                     onpageindexchanging="grdHoliday_PageIndexChanging" OnRowCreated="grdHoliday_RowCreated"
                     onrowcancelingedit="grdHoliday_RowCancelingEdit"               
                     GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr"
                     AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdHoliday_Sorting"> 
                     <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="pgr"></PagerStyle>
                     <SelectedRowStyle BackColor="BurlyWood"/>
                     <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                     <Columns>                
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="HSlno" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHSlno" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="StateCode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField SortExpression="Academic_Year" HeaderStyle-ForeColor="white" HeaderText="Academic Year">
                            <ItemTemplate>
                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Academic_Year")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="State_Name" HeaderText="State" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%#Eval("State_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="Holiday_Name" HeaderStyle-ForeColor="white" HeaderText="Holiday Name" ItemStyle-HorizontalAlign="Left">
                           <%-- <EditItemTemplate>
                                <asp:TextBox ID="txtHolidayeName"  SkinID="TxtBxAllowSymb"  runat="server"  Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField  HeaderStyle-ForeColor="white" HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                           <%-- <EditItemTemplate>
                                <asp:TextBox ID="txtHolidayeName"  SkinID="TxtBxAllowSymb"  runat="server"  Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblmonth" runat="server" Text='<%# Bind("Month") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Holiday_Date" HeaderStyle-ForeColor="white" HeaderText="Holiday Date" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate> 
                             <asp:TextBox ID="txtDate" runat="server" onkeypress="Calendar_enter(event);" SkinID="TxtBxAllowSymb"  Text='<%# Bind("Holiday_Date") %>'></asp:TextBox>
                               <asp:CalendarExtender   
                                            ID="CalendarExtender1" Format="dd/MM/yyyy"  
                                            TargetControlID="txtDate"                                               
                                            runat="server" /> 
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Holiday_Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" 
                                ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                        </asp:CommandField>
                        <%-- <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="HolidayFixation_old.aspx?Sl_No={0}"
                                DataNavigateUrlFields="Sl_No">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>    --%>     
                        <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Sl_No") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Holiday');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        
                      </Columns>
                       <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
                </asp:GridView>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
              </td>
             </tr>
            </tbody>
          </table> 
            <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>
    </div>
    </form>
</body>
</html>
