<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdBulkEdit.aspx.cs" Inherits="MasterFiles_ProdBulkEdit" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Product Detail</title>
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
     .collp
    {
        border-collapse:collapse;
     
    }
      .normal
{
    background-color:white;
}
.highlight_clr
{
    background-color:lightblue;
}
   .closeLoginPanel
        {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }
        
        .closeLoginPanel a
        {
            background-color: Yellow;
            cursor: pointer;
            color: Black;
            text-align: center;
            text-decoration: none;
            padding: 3px;
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
          function gvValidate() {

              var f = document.getElementById("grdProduct");
              if (f != null) {
                  var TargetChildPrdName = "txtProduct_Detail_Name";
                  var TargetChildPrdDescr = "txtProduct_Description";
                  var TargetChildPrdSale = "txtProduct_Sale_Unit";
                  var TargetChildState = "txtState";

                  var Inputs = f.getElementsByTagName("input");
                  for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                      if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdName, 0) >= 0) {
                          if (Inputs[i].value == "") {
                              alert("Enter Product Name");
                              f.getElementsByTagName("input").item(i).focus();
                              return false;
                          }
                      }

                      if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdDescr, 0) >= 0) {
                          if (Inputs[i].value == "") {
                              alert("Enter Description");
                              f.getElementsByTagName("input").item(i).focus();
                              return false;
                          }
                      }

                      if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildPrdSale, 0) >= 0) {
                          if (Inputs[i].value == "") {
                              alert("Enter Sales Unit");
                              f.getElementsByTagName("input").item(i).focus();
                              return false;
                          }
                      }

                      if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildState, 0) >= 0) {
                          if (Inputs[i].value == "---- Select ----") {
                              alert("Enter State");
                              f.getElementsByTagName("input").item(i).focus();
                              return false;
                          }
                      }

                  }

              }

          }

         
    </script>
    <script type="text/javascript">
        function HidePopup() {           
            var mpu = $find('txtstate_PopupControlExtender');
            mpu.hide();
        }
        </script>
         <script type="text/javascript">
             function HidePopup() {

                 var popup = $find('TextBox1_PopupControlExtender');
                 popup.hide();
             }
        </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" /> 
        <br />
        <center>
        <table border="0" cellpadding="0" cellspacing="0" id="tblLocationDtls" align="center" width ="80%">
        <tr>
            <td rowspan="">
                <asp:Label ID="lblTitle" runat="server" Width="210px" Text="Select the Fields to Edit"
                    TabIndex="6" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="Small" ForeColor="#8A2EE6">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" >
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBoxList ID="CblProdCode" CssClass="Checkbox" runat="server" style="margin-left:280px"  
                    RepeatColumns="4" RepeatDirection="Horizontal" Width="600px" Font-Names="Verdana" Font-Size="11px">
                    <asp:ListItem Value="Product_Detail_Code" Enabled="false">&nbsp;Product Code</asp:ListItem>
                        <asp:ListItem Value="Product_Detail_Name">&nbsp;Product Name</asp:ListItem>                        
                        <asp:ListItem Value="Product_Description">&nbsp;Product Description</asp:ListItem>
                        <asp:ListItem Value="Product_Cat_Code">&nbsp;Product Category</asp:ListItem>
                        <asp:ListItem Value="Product_Type_Code">&nbsp;Product Type</asp:ListItem>
                        <asp:ListItem Value="Product_Sale_Unit">&nbsp;UOM</asp:ListItem>
                        <asp:ListItem Value="Product_Sample_Unit_One">&nbsp;Base UOM</asp:ListItem>
                        <%--<asp:ListItem Value="Product_Sample_Unit_Two">&nbsp;Sample Unit2</asp:ListItem>
                        <asp:ListItem Value="Product_Sample_Unit_Three">&nbsp;Sample Unit3</asp:ListItem>--%>
                        <asp:ListItem Value="State_Code">&nbsp;State</asp:ListItem>
                        <asp:ListItem Value="subdivision_code">&nbsp;Sub Division</asp:ListItem>
                           <asp:ListItem Value="Sample_Erp_Code">&nbsp;Packet Size</asp:ListItem>
                        <asp:ListItem Value="Sale_Erp_Code">&nbsp;ERP Code</asp:ListItem>
                        <asp:ListItem Value="Product_Brd_Code">&nbsp;Product Brand</asp:ListItem>
                </asp:CheckBoxList>
            </td>           
        </tr>
     </table>
      <br />
      <center>
        <table>
         <tr >
             <td style ="width :15%" ></td>
              <td >
              
                <asp:Label ID ="lblGiftType" runat ="server" Text ="Search By"></asp:Label>
                <asp:DropDownList ID ="ddlSrch" runat ="server" SkinID ="ddlRequired" AutoPostBack ="true" 
                TabIndex ="1" OnSelectedIndexChanged ="ddlSrch_SelectedIndexChanged">
                <asp:ListItem Text ="All" Value ="1" Selected ="True" ></asp:ListItem>
                <asp:ListItem Text ="Product Name" Value ="2" ></asp:ListItem>
                <asp:ListItem Text ="Product Category" Value ="3"></asp:ListItem>
                <asp:ListItem Text ="Product Group" Value ="4" ></asp:ListItem>
                <asp:ListItem Text ="Product Brand" Value ="5"></asp:ListItem>
                <asp:ListItem Text ="Sub Division" Value ="6"></asp:ListItem>
                <asp:ListItem Text ="State" Value ="7"></asp:ListItem>
                

                </asp:DropDownList>
                <asp:TextBox ID ="TxtSrch" runat ="server" SkinID ="MandTxtBox" Width ="150px" CssClass ="TEXTAREA" Visible="false" ></asp:TextBox>
                <asp:DropDownList ID ="ddlProCatGrp" runat ="server" AutoPostBack ="false" OnSelectedIndexChanged ="ddlProCatGrp_SelectedIndexChanged" 
                SkinID ="ddlRequired" TabIndex ="4" Visible ="false" ></asp:DropDownList> 
              <asp:Button ID="Btnsrc" runat="server" CssClass="BUTTON" Width="30px" Height="25px"
                        Text="Go" OnClick="btnOk_Click"  />
                        </td>
            </tr>
      
      </table>
      </center>
      <br />
    <%--  <br />
        <table >
            
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" CssClass="BUTTON" Width="30px" Height="25px" Text="Go"  
                        onclick="btnOk_Click" />
                   
                    <asp:Button ID="btnClr" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" 
                         onclick="btnClr_Click" />
                </td>
            </tr>
        </table>
        <br />--%>
        <table runat="server" id="tblProduct" visible="false">
            <tr>
                <td>
                    <asp:GridView ID="grdProduct" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                        AutoGenerateColumns="false" onpageindexchanging="grdProduct_PageIndexChanging" OnRowCreated="grdProduct_RowCreated"
                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" OnRowDataBound="grdProduct_RowDataBound"
                        AlternatingRowStyle-CssClass="alt">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdCode" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" width="60px" MaxLength="5" Text='<%#Bind("Product_Detail_Code")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Product_Detail_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" width="120px" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Description" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Product_Description" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"  runat="server" Width="200px" Text='<%# Bind("Product_Description") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProduct_Detail_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" SkinID="TxtBxAllowSymb" width="150px" MaxLength="150" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Description" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProduct_Description" Width="200px" onkeypress="AlphaNumeric_NoSpecialChars(event);"  SkinID="TxtBxAllowSymb"  runat="server" Text='<%# Bind("Product_Description") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Product Category" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlProduct_Cat_Code" runat="server" SkinID="ddlRequired" MaxLength="150" DataSource ="<%# FillCategory() %>" DataTextField="Product_Cat_Name" DataValueField="Product_Cat_Code">                                           
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText ="Product Brand" HeaderStyle-HorizontalAlign="Center" >
                               <ItemTemplate >
                                   <asp:DropDownList ID="ddlProduct_Brd_Code" runat ="server" SkinID="ddlRequired" MaxLength="150" DataSource ="<%# FillBrand() %>" DataTextField="Product_Brd_Name" DataValueField="Product_Brd_Code">
                                   </asp:DropDownList>
                               </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="Product Type" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlProduct_Type_Code" runat="server" SkinID="ddlRequired">                                           
                                        <asp:ListItem Value="0" Selected="True">---Select Type---</asp:ListItem>
                                        <asp:ListItem Value="R">Regular Product</asp:ListItem>
                                        <asp:ListItem Value="N">New Product</asp:ListItem>                                        
                                        <asp:ListItem Value="O">Others</asp:ListItem>                                        
                                    </asp:DropDownList>                                   
                                </ItemTemplate>
                            </asp:TemplateField>     
                           <%-- <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Center">                          
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProduct_Sale_Unit" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"  runat="server" MaxLength="15" Text='<%# Bind("Product_Sale_Unit") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base UOM" HeaderStyle-HorizontalAlign="Center" >
                                <ItemStyle width="230px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProduct_Sample_Unit_One" Width="70px" onkeypress="CheckNumeric(event);"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="3" Text='<%# Bind("Product_Sample_Unit_One") %>'></asp:TextBox>
                                                           
                                    <asp:TextBox ID="txtProduct_Sample_Unit_Two" Width="70px" onkeypress="CheckNumeric(event);"  SkinID="TxtBxAllowSymb"  runat="server" MaxLength="3" Text='<%# Bind("Product_Sample_Unit_Two") %>'></asp:TextBox>
                                
                          
                                    <asp:TextBox ID="txtProduct_Sample_Unit_Three" Width="70px" onkeypress="CheckNumeric(event);" SkinID="TxtBxAllowSymb"  runat="server" MaxLength="3" Text='<%# Bind("Product_Sample_Unit_Three") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> --%>
                            <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                             <ItemStyle  Width="230px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updatepanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtState" runat="server" Width="220px" SkinID="MandTxtBox"></asp:TextBox>
                                                <asp:HiddenField ID="hdnStateId" runat="server"></asp:HiddenField>
                                                <asp:PopupControlExtender ID="txtState_PopupControlExtender" runat="server" Enabled="True"
                                                    ExtenderControlID="" TargetControlID="txtState" PopupControlID="Panel2" OffsetY="22">
                                                </asp:PopupControlExtender>
                                                <asp:Panel ID="Panel2" runat="server" Height="230px" Width="220px" BorderStyle="Solid"
                                                    BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                    Style="display: none">
                                                   <%--   <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="Button2" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color:Yellow; 
                                            color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>--%>
                                              <div style="height: 17px; position: relative;
 background-color: #4682B4; text-transform: capitalize;
  width: 100%; float: left" align="right">
    <div class="closeLoginPanel">
     <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
       title="Close">X</a>
  </div>
  </div>
                                                    <asp:CheckBoxList ID="chkstate" runat="server" width="200px" CssClass="collp"
                                                        DataTextField="StateName" DataValueField="State_Code" AutoPostBack="True"
                                                        OnSelectedIndexChanged="chkstate_SelectedIndexChanged">
                                                    </asp:CheckBoxList>
                                                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>             
                            <asp:TemplateField HeaderText="Sub Division" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle  Width="170px"></ItemStyle>
                                    <ItemTemplate>
                                      <%--  <asp:UpdatePanel ID="updatepanel1" runat="server">
                                            <ContentTemplate>--%>
                                                <asp:TextBox ID="TextBox1" runat="server"  Width="170px" SkinID="MandTxtBox"></asp:TextBox>
                                                <asp:HiddenField ID="hdnSubDivisionId" runat="server"></asp:HiddenField>
                                                <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True"
                                                    ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22">
                                                </asp:PopupControlExtender>
                                                <asp:Panel ID="Panel1" runat="server" Height="116px" Width="170px" BorderStyle="Solid"
                                                    BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                    Style="display: none">
                                                    <%--    <div style="height:15px; position:relative; background-color: #4682B4; 
                                        text-transform: capitalize; width:100%; float: left" align="right">
                                        <asp:Button ID="btnsubdiv" Style="font-family: Verdana; font-size: 7pt; font-weight:bold; width: 25px; background-color: Yellow; 
                                            Color: Black; margin-top: -1px;" Text="X" runat="server" OnClick="btnClose_Click"  OnClientClick="HidePopup();" />
                                        
                                            </div>--%>
                                              <div style="height: 17px; position: relative;
 background-color: #4682B4; text-transform: capitalize;
  width: 100%; float: left" align="right">
    <div class="closeLoginPanel">
     <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
       title="Close">X</a>
  </div>
  </div>
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server"  CssClass="collp" 
                                                        DataTextField="subdivision_name" DataValueField="subdivision_code" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" Width="170px">
                                                    </asp:CheckBoxList>
                                                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [subdivision_code],[subdivision_name] FROM [mas_subdivision]"></asp:SqlDataSource>--%>
                                                </asp:Panel>
                                            <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>       
                                  <asp:TemplateField HeaderText="Packet Size" HeaderStyle-HorizontalAlign="Center">                          
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSamp_Erp" Width="80px" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"  runat="server" MaxLength="15" Text='<%# Bind("Sample_Erp_Code") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>    
                               <asp:TemplateField HeaderText="ERP Code" HeaderStyle-HorizontalAlign="Center">                          
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSale_Erp" Width="80px" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"  runat="server" MaxLength="15" Text='<%# Bind("Sale_Erp_Code") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>              
                        </Columns>
                          <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
            <td>
            <br />
            </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnUpdate" CssClass="BUTTON" runat="server" Width="70px" Height="25px" Text="Update" 
                        onclick="btnUpdate_Click" OnClientClick="return gvValidate()" />
                </td>
            </tr>
        </table>
        </center>   
               <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>      
    </div>
    </form>
</body>
</html>
