<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HospitalList.aspx.cs" Inherits="MasterFiles_MR_Hospital_HospitalList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Details</title>
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

                    var divi = $('#<%=ddSrch.ClientID%> :selected').text();
                    var divi1 = $('#<%=ddSrc2.ClientID%> :selected').text();
                    if (divi1 == "---Select---") { alert(divi + " Name."); $('#ddSrc2').focus(); return false; }
                    if ($("#txtsearch").val() == "") { alert("Hospital  Name."); $('#txtsearch').focus(); return false; }


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
                        <asp:Label ID="lblSalesforce" runat="server" Text="Field Force Name"></asp:Label>
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
                        <asp:DropDownList ID="ddlSFCode" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" OnClick="btnSubmit_Click" />
                    </asp:Panel>
                </td>
                <td align="right" width="30%">
                    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
    <br />
        <table width="80%">
            <tr>
                <td style="width: 9.2%" />
                <td>
                    <asp:Button ID="btnQAdd" runat="server" CssClass="BUTTON" Text="Add Hospital" OnClick="btnQAdd_Click" Width="100px" /> &nbsp;&nbsp;
                    <asp:Button ID="btnEdit" runat="server" CssClass="BUTTON" Text="Edit All Hospital" OnClick="btnEdit_Click" Width="120px" /> &nbsp;&nbsp;
                    <asp:Button ID="btnDeAc" runat="server" CssClass="BUTTON" Text="Deactivate Hospital" Width="130px"
                        OnClick="btnDeAc_Click" /> &nbsp;&nbsp;
                    <asp:Button ID="btnReAc" runat="server" CssClass="BUTTON" Text="Reactivate Hospital" Width="130px" OnClick="btnReAc_Click" />
                </td>
            </tr>
        </table>
        <br />
     
        <table width ="100%">
          <tr>
              <td style ="width :7.5%"></td>
                <td>
                  <asp:Label ID ="lblType" runat ="server" SkinID ="lblMand" Text ="Search By" ></asp:Label>
                  <asp:DropDownList ID ="ddSrch" runat ="server" SkinID ="ddlRequired" AutoPostBack ="true" 
                  TabIndex ="1" OnSelectedIndexChanged ="ddSrch_OnSelectedIndexChanged" >
                  <asp:ListItem Text ="All" Value ="1" Selected ="True" ></asp:ListItem>
                  <asp:ListItem Text ="Hospital Name" Value ="2"></asp:ListItem>
                  <%--<asp:ListItem Text ="Territory" Value ="3"></asp:ListItem>--%>
                  </asp:DropDownList>
                  <asp:TextBox ID ="txtsearch" runat ="server" CssClass ="TEXTAREA" 
                  Visible ="false" MaxLength ="50" Height ="15px" ></asp:TextBox>
                  <asp:DropDownList ID ="ddSrc2" runat ="server" Visible ="false" 
                  SkinID ="ddlRequired" TabIndex ="4" OnSelectedIndexChanged ="ddSrc2_OnSelectedIndexChanged">
                  </asp:DropDownList> 
                  <asp:Button ID ="Btnsrc" runat ="server" CssClass ="BUTTON" Width ="30px" Height ="25px" 
                  Text ="Go" OnClick ="Btnsrc_Click" Visible ="false" />
                </td>
          
          </tr> 
        
        </table>
        <br />
        <table width="80%">
            <tbody>
                <tr>
                    <td style="width: 30%" />
                    <td colspan="2" align="center">
                        <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                            runat="server" HorizontalAlign="Center">
                            <SeparatorTemplate>
                            </SeparatorTemplate>
                            <ItemTemplate>
                            &nbsp
                                <asp:LinkButton ID="lnkbtnAlpha" ForeColor="Black" Font-Names="Calibri" Font-Size="14px" runat="server" CommandArgument='<%#bind("Hospital_Name") %>'
                                    Text='<%#bind("Hospital_Name") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdHospital" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"  
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnPageIndexChanging="grdHospital_PageIndexChanging"
                            OnRowCreated="grdHospital_RowCreated"
                            onrowupdating="grdHospital_RowUpdating" onrowediting="grdHospital_RowEditing"                         
                            onrowcancelingedit="grdHospital_RowCancelingEdit" OnRowDataBound="grdHospital_RowDataBound"
                            OnRowCommand="grdHospital_RowCommand" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdHospital_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdHospital.PageIndex * grdHospital.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hospital Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Hospital_Code" runat="server" Text='<%#Eval("Hospital_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                  <%--  <EditItemTemplate>
                                        <asp:TextBox ID="txtHospitalCode" runat="server" Text='<%#Eval("Hospital_Code")%>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Hospital_Name" HeaderText="Hospital Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHospitalName" runat="server" Text='<%#Eval("Hospital_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtHospitalName" runat="server" Width="200px" SkinID="MandTxtBox" Text='<%#Eval("Hospital_Name")%>'></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfvHos" runat="server" ControlToValidate="txtHospitalName" ErrorMessage="*Required"></asp:RequiredFieldValidator>

                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Hospital_Contact" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Hospital_Contact")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtContact" runat="server" Width="170px" SkinID="MandTxtBox" Text='<%#Eval("Hospital_Contact")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                     <EditItemTemplate>                                                                                                
                                    <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource ="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
                                    </asp:DropDownList> 
                                     <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                            ErrorMessage="Select Territory" InitialValue="0" runat="server"
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
                                <asp:HyperLinkField HeaderText="View" Text="View" DataNavigateUrlFormatString="Hospital_View.aspx?Hospital_Code={0}"
                                    DataNavigateUrlFields="Hospital_Code" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Hospital_Code") %>'
                                            CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Hospital');">Deactivate
                                        </asp:LinkButton>
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
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
