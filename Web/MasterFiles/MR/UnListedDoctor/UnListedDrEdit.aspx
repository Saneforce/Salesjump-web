<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnListedDrEdit.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnListedDrEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - UnListed Customer</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="Divid" runat="server"></div> 
        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
           <table id="Table1" runat="server" width="90%">
       
            <tr>
                 <td align="right" width="30%">
                   <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
            </tr>
              <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Visible="false" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>
            </tr>
            </table>  
           
        <center>
        <table border="0" cellpadding="3" cellspacing="3" id="tblLocationDtls" align="center" width ="80%">
        <tr>
            <td rowspan="" align="center">
                <asp:Label ID="lblTitle" runat="server" Width="210px" Text="Select the Fields to Edit"
                    TabIndex="6" BorderColor="#E0E0E0" BorderStyle="None" Font-Bold="True" Font-Names="Verdana"
                    Font-Size="Small" ForeColor="BlueViolet">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" >
              
                <asp:CheckBoxList ID="CblDoctorCode" CssClass="Checkbox" runat="server"  
                    RepeatColumns="6" RepeatDirection="Horizontal" Width="700px" style="margin-left:200px">
                        <asp:ListItem Value="Territory_Code">&nbsp;Territory</asp:ListItem>
                        <asp:ListItem Value="Doc_Special_Code">&nbsp;Channel</asp:ListItem>
                        <asp:ListItem Value="Doc_Cat_Code">&nbsp;Category</asp:ListItem>
                      
                        <asp:ListItem Value="Doc_ClsCode">&nbsp;Class</asp:ListItem>
                        <asp:ListItem Value="UnListedDR_Address1">&nbsp;Address</asp:ListItem>
                        <asp:ListItem Value="UnListedDR_DOB">&nbsp;DOB</asp:ListItem>
                        <asp:ListItem Value="UnListedDR_DOW">&nbsp;DOW</asp:ListItem>
                        <asp:ListItem Value="No_of_Visit">&nbsp;No of Visit</asp:ListItem>
                        <asp:ListItem Value="UnListedDR_Mobile">&nbsp;Mobile No</asp:ListItem>
                        <asp:ListItem Value="UnListedDR_Phone">&nbsp;Telephone No</asp:ListItem>
                        <asp:ListItem Value="UnListedDR_EMail">&nbsp;EMail ID</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
     </table>
      <br />
        <table>       
         <tr>
            <td style="width:15%" />
            <td width="40%">
                    <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By" ></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"    
                        TabIndex="1" onselectedindexchanged= "ddlSrch_SelectedIndexChanged" >                    
                                    <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Doctor Speciality" Value="2" ></asp:ListItem>
                                    <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                  <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                    <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox id="txtsearch" runat="server" CssClass="TEXTAREA" Visible= "false" ></asp:TextBox> 
                    <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="true"  Visible ="false" onselectedindexchanged= "ddlSrc2_SelectedIndexChanged"  
                                     SkinID="ddlRequired" TabIndex="4">                    
                                </asp:DropDownList>       
                                   </td>
                <td align="left" width="40%" style="vertical-align:bottom">
                    <asp:Button ID="btnOk" runat="server" CssClass="BUTTON" Width="35px" Height="25px" Text="Go"  
                        onclick="btnOk_Click" />
                    &nbsp;
                    <asp:Button ID="btnClr" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" 
                       onclick="btnClr_Click" />
                </td>
            </tr>
        </table>
        <br />
        
       <table width="50%" align ="left">
       
    </table> 
   <br />
           <table runat="server" id="tblDoctor" visible="false" width="80%" align="center" style="margin-left:200px">
            <tr>
                <td>
                    <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="grdDoctor_RowDataBound" 
                        GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" 
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />                        
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoctorCode" runat="server" Text='<%#Eval("UnListedDrCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle Width="160px" />
                                <ItemTemplate>
                                    <asp:Label ID="ListedDr_Name" runat="server" SkinID="TxtBxAllowSymb" Width="160px" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Territory_Code" runat="server" Width="140px" SkinID="ddlRequired"  DataSource ="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="Channel" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Doc_Special_Code" runat="server" SkinID="ddlRequired" DataSource ="<%# FillSpeciality() %>" DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Doc_Cat_Code" runat="server" SkinID="ddlRequired" DataSource ="<%# FillCategory() %>" DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Doc_QuaCode" SkinID="ddlRequired" runat="server" DataSource="<%# FillQualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Doc_ClsCode" runat="server" SkinID="ddlRequired" DataSource ="<%# FillClass() %>" DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="UnListedDR_Address1"  SkinID="TxtBxAllowSymb"  runat="server" Width="200px" Text='<%# Bind("UnListedDR_Address1") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOB" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="UnListedDR_DOB"  SkinID="TxtBxAllowSymb" onkeypress="Calendar_enter(event);" runat="server" MaxLength="12" Text='<%# Bind("UnListedDR_DOB") %>'></asp:TextBox>
                                        <asp:CalendarExtender   
                                            ID="CalendarExtender1" Format="dd/MM/yyyy"  
                                            TargetControlID="UnListedDR_DOB"                                               
                                            runat="server" /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOW" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="UnListedDR_DOW" onkeypress="Calendar_enter(event);"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="12" Text='<%# Bind("UnListedDR_DOW") %>'></asp:TextBox>
                                    <asp:CalendarExtender   
                                            ID="CalendarExtender2" Format="dd/MM/yyyy"  
                                            TargetControlID="UnListedDR_DOW"                                               
                                            runat="server" /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Visit" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="No_of_Visit"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="5" Text='<%# Bind("No_of_Visit") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="UnListedDR_Mobile"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="12" Text='<%# Bind("UnListedDR_Mobile") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Telephone No" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="UnListedDR_Phone"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="12" Text='<%# Bind("UnListedDR_Phone") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMail ID" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="UnListedDR_EMail"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="25" Text='<%# Bind("UnListedDR_EMail") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            </tr>
            </table>
            <table align="center">
            <tr>
                <td align="center">
                    <asp:Button ID="btnUpdate" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Update" Visible="false"
                        onclick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
        </center>                 
        </div>
    <div class="div_fixed">
         <asp:Button ID="btnSave" runat="server" Text="Update" Visible="false" CssClass="BUTTON" 
            onclick="btnSave_Click" />
    </div>    
     <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
