<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnLstDoctorList.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnLstDoctorList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UnListed Customer Details</title>
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
             $('#Btnsrc').click(function () {

                 var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                 var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                 if (divi1 == "---Select---") { alert("Select " +divi); $('#ddlSrc2').focus(); return false; }
                 if ($("#txtsearch").val() == "") { alert("Enter Doctor Name."); $('#txtsearch').focus(); return false; }

             });
         }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="Divid" runat="server">
        </div>
      
         <table width="90%">
            <tr>
                <td align="right" colspan="3">
               <%--     <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />--%>
                      <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div>    
                    </td>
            </tr>
            <tr>
                <td style="width: 8.2%">
                </td>
                <td>
                <asp:Panel ID="pnlAdmin" runat="server">
                <asp:Label ID="lblSalesforce" runat="server" SkinID="lblMand" Text="Field Force Name"></asp:Label>
                <asp:DropDownList ID="Alpha" runat="server" AutoPostBack="true" SkinID="ddlRequired" OnSelectedIndexChanged="Alpha_SelectedIndexChanged">     
                     <asp:ListItem Selected="true">---ALL---</asp:ListItem>
                        <asp:ListItem >A</asp:ListItem>
                        <asp:ListItem >B</asp:ListItem>
                        <asp:ListItem >C</asp:ListItem>    
                        <asp:ListItem >D</asp:ListItem>
                        <asp:ListItem >E</asp:ListItem>
                        <asp:ListItem >F</asp:ListItem>  
                        <asp:ListItem >G</asp:ListItem>
                        <asp:ListItem >H</asp:ListItem>
                        <asp:ListItem >I</asp:ListItem>    
                        <asp:ListItem >J</asp:ListItem>
                        <asp:ListItem >K</asp:ListItem>
                        <asp:ListItem >L</asp:ListItem> 
                        <asp:ListItem >M</asp:ListItem>
                        <asp:ListItem >N</asp:ListItem>
                        <asp:ListItem >O</asp:ListItem>    
                        <asp:ListItem >P</asp:ListItem>
                        <asp:ListItem >Q</asp:ListItem>
                        <asp:ListItem >R</asp:ListItem>  
                        <asp:ListItem >S</asp:ListItem>
                        <asp:ListItem >T</asp:ListItem>
                        <asp:ListItem >U</asp:ListItem>    
                        <asp:ListItem >V</asp:ListItem>
                        <asp:ListItem >W</asp:ListItem>
                        <asp:ListItem >X</asp:ListItem>     
                        <asp:ListItem >Y</asp:ListItem>
                        <asp:ListItem >Z</asp:ListItem>   
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlSFCode" runat="server"
                         SkinID="ddlRequired">
                    </asp:DropDownList>
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" onclick="btnSubmit_Click" />
                    </asp:Panel>
                </td>
                <td align="right" width="30%">
                <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
     <br />
    <table width="80%">
        <tr>
            <td style="width:9.2%" />
            <td>
                <asp:Button ID="btnQAdd" runat="server" CssClass="BUTTON" Text="Add UnListed Customer" Width="140px"
                    onclick="btnQAdd_Click"  /> &nbsp;&nbsp;
                <asp:Button ID="btnEdit" runat="server" CssClass="BUTTON" Text="Edit All UnListed Customer" Width="160px"
                    onclick="btnEdit_Click" /> &nbsp;&nbsp;
                <asp:Button ID="btnDeAc" runat="server" CssClass="BUTTON" Text="Deactivate UnListed Customer" Width="180px" 
                    onclick="btnDeAc_Click" />  &nbsp;&nbsp;
               <asp:Button ID="btnReAc" runat="server" CssClass="BUTTON" Text="Reactivate UnListed Customer" Width="180px"
                    onclick="btnReAc_Click" /> 
            </td>            
        </tr>
      </table>
      <br />
       <table width="100%">
        <tr>
            <td style="width: 4.5%" />
            <td>
                    <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By" ></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"    
                        TabIndex="1" onselectedindexchanged= "ddlSrch_SelectedIndexChanged" >                    
                                    <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Customer Speciality" Value="2" ></asp:ListItem>
                                    <asp:ListItem Text="Customer Category" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Customer Qualification" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Customer Class" Value="5"></asp:ListItem>
                                  <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                    <asp:ListItem Text="Customer Name" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox id="txtsearch" runat="server" CssClass="TEXTAREA" Visible= "false" ></asp:TextBox> 
                    <asp:DropDownList ID="ddlSrc2" runat="server" Visible ="false" onselectedindexchanged= "ddlSrc2_SelectedIndexChanged"  
                                     SkinID="ddlRequired" TabIndex="4">                    
                                </asp:DropDownList>       
                    <asp:Button ID="Btnsrc" runat="server" CssClass="BUTTON" 
                    Text=">>" onclick="Btnsrc_Click" Visible = "false" />
                </td>    
  
               
            </tr>
            <tr>
             <td width="60%" colspan="2" align="center">
                    <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                     runat="server" HorizontalAlign="Center">
                        <SeparatorTemplate></SeparatorTemplate>
                        <ItemTemplate>
                        &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha" Font-Names="Calibri" Font-Size="14px" ForeColor="Black" runat="server" CommandArgument = '<%#bind("UnListedDr_Name") %>' text = '<%#bind("UnListedDr_Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            
            </table>
      
       <table width="100%" align="center">
        <tbody>               
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"  
                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                        OnRowDataBound="grdDoctor_RowDataBound" onrowupdating="grdDoctor_RowUpdating" onrowediting="grdDoctor_RowEditing"                         
                            onrowcancelingedit="grdDoctor_RowCancelingEdit"
                        OnRowCreated="grdDoctor_RowCreated" onpageindexchanging="grdDoctor_PageIndexChanging"  onrowcommand="grdDoctor_RowCommand"                      
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDoctor_Sorting">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="gridview1"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDoctor.PageIndex * grdDoctor.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnListed Doctor Code" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("UnListedDrCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField SortExpression="UnListedDr_Name" ItemStyle-HorizontalAlign="Left" HeaderText="UnListed Customer Name" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("UnListedDr_Name")%>'></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox ID="txtDocName" SkinID="MandTxtBox" runat="server" Text='<%#Eval("UnListedDr_Name")%>'></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="rfvDoc" runat="server" ControlToValidate="txtDocName"  ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Channel" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                </ItemTemplate>
                                  <EditItemTemplate>
                                     <asp:DropDownList ID="ddlDocSpec" runat="server" SkinID="ddlRequired"  DataSource ="<%# Doc_Speciality() %>" DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">                                           
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDocSpec" ID="RequiredFieldValidator2"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>   
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Category" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:DropDownList ID="ddlDocCat" runat="server" SkinID="ddlRequired"  DataSource ="<%# Doc_Category() %>" DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">                                           
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDocCat" ID="RequiredFieldValidator3"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" SortExpression="Doc_QuaName" HeaderStyle-ForeColor="White" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:DropDownList ID="ddlDocQua" runat="server" SkinID="ddlRequired" DataSource ="<%# Doc_Qualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">                                           
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDocQua" ID="RequiredFieldValidator4"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate> 
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" SortExpression="Doc_ClsName" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDocClass" runat="server" SkinID="ddlRequired" DataSource ="<%# Doc_Class() %>" DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">                                           
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDocClass" ID="RequiredFieldValidator5"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left" SortExpression="territory_Name" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                       <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource ="<%# Doc_Territory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>    
                            </asp:TemplateField>
                                <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" HeaderStyle-Width="90px" 
                                    ShowEditButton="True">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle ForeColor="DarkBlue" 
                                    HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ItemStyle>
                            </asp:CommandField>
                            <asp:HyperLinkField HeaderText="View" Text="View" DataNavigateUrlFormatString="UnListedDr_View.aspx?type=1&UnListedDrCode={0}"
                                DataNavigateUrlFields="UnListedDrCode">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>            
                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="UnListedDr_View.aspx?type=2&UnListedDrCode={0}"
                                DataNavigateUrlFields="UnListedDrCode">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="Deactivate">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("UnListedDrCode") %>'
                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the UnListedDR');">Deactivate
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>                           
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
                    </asp:GridView>
                </td> 
            </tr> 
        </tbody>
    </table>
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
