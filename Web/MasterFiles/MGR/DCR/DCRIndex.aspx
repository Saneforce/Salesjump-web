<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCRIndex.aspx.cs" Inherits="MasterFiles_MGR_DCR_DCRIndex" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Entry</title>

    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />

    <script type="text/javascript">
      function confirm_Save() {
          if (confirm('Do you want to save DCR?')) {
              if (confirm('Are you sure?')) {
                  return true;
              }
              else {
                  return false;
              }
          }
          else {
              return false;
          }
      }
      function confirm_Clear() {
          if (confirm('Do you want to Clear DCR?')) {
              if (confirm('Are you sure?')) {
                  ShowProgress();
                  return true;
              }
              else {
                  return false;
              }
          }
          else {
              return false;
          }
      }

      function confirm_Submit() {
          if (confirm('Do you want to Submit DCR?')) {
              if (confirm('Are you sure?')) {
                 return true;
              }
              else {
                  return false;
              }
          }
          else {
              return false;
          }
      }
</script>
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
 <script type="text/javascript">
     function LimtCharacters(txtMsg, CharLength, indicator) {
         chars = txtMsg.value.length;
         document.getElementById(indicator).innerHTML = CharLength - chars;
         if (chars > CharLength) {
             txtMsg.value = txtMsg.value.substring(0, CharLength);
         }
     }
    </script>
        <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajax:ToolkitScriptManager ID="ScriptManager1" runat="server" />  
           
<div>
   <asp:UpdatePanel ID="Uplworktype" runat="server">
    <ContentTemplate>
    <center>
     <asp:Panel ID="PnlInfo" runat="server" style="text-align:center; top:300px; width:100%; position:absolute;">
            <%--<input type="text" id="lblInfo" runat="server" style="border:0px;background-color:transparent; font-size:32px; font-family:TimesNewRoman;color:Green;" value="Select the WorkType..." />--%>
             <asp:Label id="lblInfo" runat="server" style="border: 0px; background-color: transparent;
                font-size: 32px; font-family: TimesNewRoman; color: Red;  " Text="Select the WorkType   ..." />
        </asp:Panel>
      </center> 
           <asp:Label ID="lblCurDate" runat="server" Visible="false"></asp:Label>
           <asp:Panel ID="pnlMGR" runat="server" BorderStyle="Solid" BorderWidth="1" BackColor="#E0E0E0">
           <table runat="server" width="100%"  cellpadding="3" cellspacing="3">
                <tr style="background-color:#E0E0E0;">
                    <td width="80%">                
                        &nbsp;&nbsp;
                        <asp:Label ID="lblText" runat="server" Font-Size="12px" 
                            Font-Names="TimesNewRoman"  Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblHeader" runat="server" Font-Size="Small" 
                            Font-Names="TimesNewRoman" BackColor="Yellow" ForeColor="#0033CC"></asp:Label>
                        <asp:Label ID="lblReject" runat="server" Font-Bold="true" ForeColor="Brown" Font-Names="TimesNewRoman"
                            Font-Size="14px"></asp:Label>
                    </td>
                        <td align="right">
                        <asp:Panel ID="Panel1" runat="server" Style="text-align: left;">
                        <asp:Label ID="lblReason" runat="server" Style="text-align: left" Font-Size="Small"
                            Font-Names="Verdana" Visible="false"></asp:Label>
                        </asp:Panel>
                        <ajax:BalloonPopupExtender ID="BalloonPopupExtender2" TargetControlID="lblNote" BalloonPopupControlID="Panel1"
                            runat="server" Position="TopLeft" DisplayOnMouseOver="true" BalloonSize="Small">
                        </ajax:BalloonPopupExtender>
                            <asp:Label ID="lblNote" runat="server" Style="text-decoration: underline; " ForeColor="Red" 
                                Font-Size="Small" Font-Names="Verdana" Text="Note" Visible="false"></asp:Label>

                    </td>
                     <td align="right">
                        <asp:HyperLink ID="hypHome" runat="server" Text="Home" NavigateUrl="~/Default_MGR.aspx"></asp:HyperLink>
                    </td>
                </tr>
             </table>
             <hr />
            <table id="Table1" runat="server" width="100%" style="border-bottom:1px solid ">
            <tr>
                    <td width="35%">
                        <asp:Label ID="lblWorkType" runat="server" 
                            Text="Work Type &nbsp;:-&nbsp;&nbsp;" CssClass="pnl_label" Font-Bold="True"></asp:Label>  
                        <asp:DropDownList ID="ddlWorkType" runat="server" Width="100px" SkinID="ddlRequired"   AutoPostBack="true" onselectedindexchanged="ddlWorkType_SelectedIndexChanged" ></asp:DropDownList> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                   
                    <td align="right">
                        <asp:Button ID="btnFieldWork" runat="server" Text="Goto Field Work Entry"  Width="150px" Height="25px" 
                            BackColor="Black" ForeColor="White" 
                            onclick="btnFieldWork_Click" />
                    </td>
                </tr>
           </table>
           </asp:Panel>
         
           <asp:Panel ID="pnlTerr" runat="server" BorderStyle="Solid" BorderWidth="1" BackColor="#E0E0E0" >
           <table runat="server" width="100%">
                <tr style="background-color:#E0E0E0;">
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTerrHQ" runat="server" Text="HQ"></asp:Label>
                        <asp:DropDownList ID="ddlTerrHQ" runat="server" SkinID="ddlRequired" Width="400" AutoPostBack="true"
                            onselectedindexchanged="ddlTerrHQ_SelectedIndexChanged"   ></asp:DropDownList> 
                    </td>
                    <td>
                        <asp:Label ID="lblSDP" runat="server" Text="Territory" CssClass="pnl_label"></asp:Label>  
                        <asp:DropDownList ID="ddlSDP" runat="server" Width="200" SkinID="ddlRequired"  AutoPostBack="true"
                        onselectedindexchanged="ddlSDP_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                   <td align="right"><asp:Button ID="btnBack" Text="Back" runat="server" BackColor="Yellow" Width="70px" Height="25px" 
                            onclick="btnBack_Click" /></td>
                </tr>
            </table>        
        </asp:Panel>
        <asp:Panel ID="pnlTree" runat="server" BorderStyle="Solid" BorderWidth="1" Visible ="false" >
           <%-- <asp:TreeView ID="trvSF" runat="server"  > 
            
            </asp:TreeView>--%>
            <table width="100%" cellpadding="3" cellspacing="3" >
              
          
          <tr>
          <td>
             <asp:GridView ID="grvWorkArea" runat="server"  AutoGenerateColumns="False"
                            ForeColor="#333333" Width="90%" GridLines="None" OnRowDataBound="grvWorkArea_RowDataBound" 
                             OnRowDeleting="grvWorkArea_RowDeleting" CellPadding ="5" CellSpacing ="5">
                            
                            <Columns>
                               
                                <asp:TemplateField HeaderStyle-Width="900">
                                
                                    <ItemTemplate >
                                      <asp:Label ID="lblWorkArea" runat="server"  Text='<%#DataBinder.Eval(Container. DataItem,"node") %>' ForeColor="#0101DF"  Font-Bold ="true"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField Visible = "false">
                                
                                    <ItemTemplate >
                                      <asp:Label ID="lblHQ" runat="server"  Text='<%#DataBinder.Eval(Container. DataItem,"hq") %>' ForeColor="#0101DF"  Font-Bold ="true"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField  Visible = "false">
                                
                                    <ItemTemplate >
                                      <asp:Label ID="lblisHQ" runat="server"  Text='<%#DataBinder.Eval(Container. DataItem,"ishq") %>' ForeColor="#0101DF"  Font-Bold ="true"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="sdp" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsdp" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"sdp") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"color") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:CommandField ShowDeleteButton="True"/>
                                <asp:TemplateField  >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtColor" runat="server"  BorderStyle ="None" Width ="10px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      
                            </Columns>

                        </asp:GridView>
                    </td>
                    </tr>
                    </table>
        </asp:Panel>
        <asp:Panel ID="PnlRemarks" runat="server" BorderStyle="Solid" BorderWidth="1" Visible = "false">
          <table id="tblRemarks" runat="server" border="1" style="width: 100%;  font-family:Tahoma; font-size:x-small;" cellspacing="0" cellpadding= "0">
          <tr>
                        <td style="height:15px;">
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDes" runat="server" Text="Description of Days work( Maximum charactors allowed:250 )" Font-Size="X-Small" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="txtRemarkDesc" runat="server" Width="1360" Height="350" BorderStyle="Groove" 
                                TextMode="MultiLine" onkeypress="AlphaNumeric(event);"
                                  onpaste="return LimtCharacters(this,500,'lblCount')"  onKeyDown="return LimtCharacters(this,500,'lblCount')" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
        </asp:Panel> 
        <asp:Panel ID="pnlbtn" runat="server" Width="95%" style="left:10px; top:500px; position:absolute;">
            <center>
                <asp:Button ID="btnGo" runat="server" Text="Goto Field Work Entry" Width="150px" Height="25px" 
                    BackColor="Black" ForeColor="White" 
                    onclick="btnGo_Click" />&nbsp;&nbsp;
           

                <asp:Button ID="btnSave" runat="server" Text="Save" Width="10px" Height="25px"  BackColor="Black" ForeColor="White"
                
                OnClientClick="return confirm_Save();" onclick="btnSave_Click" />    
            <asp:Button ID="btnSubmit" runat="server" Text="Final Submit" BackColor="Black" Width="100px" Height="25px"  ForeColor="White"
                 OnClientClick="return confirm_Submit();" onclick="btnSubmit_Click" />                        
            </center>   
        </asp:Panel>
</ContentTemplate>
 </asp:UpdatePanel>
        <asp:Panel ID="Panel2" runat="server" Width="95%" style="left:735px; top:500px; position:absolute;">
                     <asp:Button ID="NewbtnClear" runat="server" Text="Clear" Width="70px" Height="25px"  OnClientClick="return confirm_Clear()"  onclick="NewbtnClear_Click" 
                    BackColor="Black" ForeColor="White"  />
                    </asp:Panel> 
         </div>
   
    </form>
</body>
</html>
