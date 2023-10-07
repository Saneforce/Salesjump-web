<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDr_Type_Map.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_ListedDr_Type_Map" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer - Type Map</title>
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
           $(document).ready(function () {
               //   $('input:text:first').focus();
               $('input:text').bind("keydown", function (e) {
                   var n = $("input:text").length;
                   if (e.which == 13) { //Enter key
                       e.preventDefault(); //to skip default behavior of the enter key
                       var curIndex = $('input:text').index(this);
                       if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                           $('input:text')[curIndex].focus();
                       }
                       else {
                           var nextIndex = $('input:text').index(this) + 1;

                           if (nextIndex < n) {
                               e.preventDefault();
                               $('input:text')[nextIndex].focus();
                           }
                           else {
                               $('input:text')[nextIndex - 1].blur();
                               $('#btnSubmit').focus();
                           }
                       }
                   }
               });
               $("input:text").on("keypress", function (e) {
                   if (e.which === 32 && !this.value.length)
                       e.preventDefault();
               });
               $('#btnOk').click(function () {

                   var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                   var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                   if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
                   if ($("#txtsearch").val() == "") { alert("Enter Customer Name."); $('#txtsearch').focus(); return false; }

               });
           }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="Divid" runat="server"></div>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
           <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>

          <table id="Table1" runat="server" width="90%">
       
            <tr>
                 <td align="right" width="30%">
                <%--    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
            </tr>
              <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>
            </tr>
            </table>  
          
        <center>
         <table>       
         <tr>
          <td style="width: 7.2%" />
            <td>
                    <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By" ></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"    
                        TabIndex="1" onselectedindexchanged= "ddlSrch_SelectedIndexChanged" >                    
                                    <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Customer Speciality" Value="2" ></asp:ListItem>
                                    <asp:ListItem Text="Customer Category" Value="3"></asp:ListItem>
                                   <%-- <asp:ListItem Text="Customer Qualification" Value="4" Enabled="false"></asp:ListItem>--%>
                                    <asp:ListItem Text="Customer Class" Value="5"></asp:ListItem>
                                   <%-- <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                    <asp:ListItem Text="Customer Name" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox id="txtsearch" runat="server" CssClass="TEXTAREA" Visible= "false" ></asp:TextBox> 
                    <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="true"  Visible ="false" onselectedindexchanged= "ddlSrc2_SelectedIndexChanged"  
                                    SkinID="ddlRequired" TabIndex="4">                    
                                </asp:DropDownList>       
                                   </td>
                <td align="left" style="vertical-align:bottom">
                    <asp:Button ID="btnOk" runat="server" CssClass="BUTTON" Width="35px" Height="25px" Text="Go" 
                        onclick="btnOk_Click" />
                
                </td>
            </tr>
        </table>
        </center>
        <br />
        <center>
        <table width="85%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="80%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="grdDoctor_RowDataBound" 
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Customer Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Qualification" HeaderStyle-ForeColor="White" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Channel" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Category" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Class" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Territory" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired">                                           
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <center>
        <asp:Button ID="btnUpdate" runat="server" CssClass="BUTTON" Text="Update" onclick="btnUpdate_Click"  Width="70px" Height="25px" 
                />
        </center>
        </center>

          <div class="div_fixed">
         <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="BUTTON" Width="70px" Height="25px"
                  onclick="btnSave_Click" />
    </div>    
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
