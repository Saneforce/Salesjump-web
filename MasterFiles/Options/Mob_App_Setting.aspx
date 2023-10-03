<%@ Page Title="Apps Settings" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Mob_App_Setting.aspx.cs"
    Inherits="MasterFiles_Options_Mob_App_Setting" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Apps Settings</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <style type="text/css">
        .spc
        {
            padding-left: 5%;
        }
        .spc1
        {
            padding-left: 10%;
        }
        
        .box
        {
            background: #FFFFFF;
            border: 5px solid #427BD6;
            border-radius: 8px;
        }
        
        .tableHead
        {
            background: #e0f3ff;
            color: black;
            border-style: solid;
            border-width: 1px;
            border-color: #427BD6;
        }
        .break
        {
            height: 10px;
        }
        .style1
        {
            padding-left: 5%;
            height: 26px;
        }
        .style2
        {
            height: 26px;
        }
        
        <style type="text/css">
        .modalBackground
        {
            /* background-color: #999999;*/
            filter: alpha(opacity=80);
            opacity: 0.5;
            z-index: 10000;
            display: block;
            cursor: default;
            color: #000000;
            pointer-events: none;
        }
        #menu1
        {
            display: none;
        }
        
        .TextFont
        {
            text-align: center;
            margin-top: 6px;
        }
        
         .modalPopupNew
        {
            background-color: #FFFFFF;
            width: 350px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        
          .modalBackgroundNew
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
    </style>
    </style>
    <script type="text/javascript">
        function HidePanel() {
            var pnlpopup = document.getElementById('<%=pnlpopup.ClientID%>');
            pnlpopup.style.display = "none";
            pnlpopup.style.visibility = "hidden";
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:ScriptManager runat="server">
    </asp:ScriptManager>
        <%--<ucl:Menu ID="menu1" runat="server" />--%>
        <br />
        <br />
        <table border="1" width="80%" align="center" style="margin-left: 7%" class="box">
            <tr>
                <td width="48%" class="tableHead">
                    <br />
                    <table border="1" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblloca" runat="server" Text="LOCATION" Font-Underline="True" Width="70%"
                                    BackColor="SkyBlue" ForeColor="MediumVioletRed" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="14px"> </asp:Label>
                            </td>
                            <td>
                                <%--  <asp:UpdatePanel ID="updateP" runat="server">
                            <ContentTemplate>
                            <table >
                          <tr>
                          <td>--%>
                                <asp:LinkButton ID="linkgps" Text="GPS Setting" runat="server" 
                                 ></asp:LinkButton>
                                <%-- </td>
                                </tr>
                                  </table>
                                   </ContentTemplate>
                                         <Triggers>
                         
                           <asp:PostBackTrigger ControlID="linkgps" />
    
               </Triggers>
                        </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblmandat" runat="server" Text="Mandatory" Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdomandt" runat="server" RepeatDirection="Horizontal" Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblgeo" runat="server" Text="GEO Tag" Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdogeo" runat="server" RepeatDirection="Horizontal" Font-Bold="true">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblcover" runat="server" Text="Coverage" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtcover" runat="server" Width="60" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="break">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span runat="server" id="Span1" style="font-weight: bold; border-color: #FF0000;
                                    color: MediumVioletRed">
                                    <asp:Label ID="lblcap" runat="server" Text="CAPTIONS" Font-Underline="True" Font-Bold="True"
                                        Width="70%" BackColor="SkyBlue" Font-Names="Arial" Font-Size="14px"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblvist1" runat="server" Text="Visit Type 1" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtvisit1" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblvist2" runat="server" Text="Visit Type 2" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtvisit2" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblvist3" runat="server" Text="Visit Type 3" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtvisit3" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblvist4" runat="server" Text="Visit Type 4" Font-Names="Arial" Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtvisit4" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="break">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span runat="server" id="Span2" style="font-weight: bold; border-color: #FF0000;
                                    color: MediumVioletRed">
                                    <asp:Label ID="lblHalfday" runat="server" Text="HALFDAY WORK" Font-Underline="True"
                                        Font-Bold="True" Width="70%" BackColor="SkyBlue" Font-Names="Arial" Font-Size="14px"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc" align="center">
                                <div style="width: 250px; height: 300px; padding: 2px; overflow: auto; border: 1px solid Black;">
                                    <asp:CheckBoxList class="bor" ID="chkhaf_work" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                <%--   <asp:CheckBoxList ID="chkhaf_work" style="OVERFLOW-Y:scroll; WIDTH:200px; HEIGHT:200px" CssClass="chkboxLocation" RepeatDirection="vertical" RepeatColumns="1"
                                    runat="server">
                                </asp:CheckBoxList>--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tableHead">
                    <br />
                    <table border="1" width="80%">
                        <tr>
                            <td>
                                <span runat="server" id="Span5" style="font-weight: bold; border-color: #FF0000;
                                    color: MediumVioletRed">
                                    <asp:Label ID="lblslide" runat="server" Text="SLIDES" Font-Bold="true" Font-Underline="True"
                                        Font-Names="Arial" Font-Size="14px" Width="70%" BackColor="skyblue"> </asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="break">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbldr_ent" runat="server" Text="Doctor Entry" Font-Underline="True"
                                    Width="70%" BackColor="skyblue" ForeColor="MediumVioletRed" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="14px"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblprd_entry_doc" runat="server" Text="Product Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoprd_entry_doc" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblRx_Cap_doc" runat="server" Text="Rx Qty Caption" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRx_Cap_doc" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblSamQty_Cap_doc" runat="server" Text="Sample Qty Caption" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSamQty_Cap_doc" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblinput_Ent_doc" runat="server" Text="Input Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoinput_Ent_doc" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="break">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblChem" runat="server" Text="Chemist Entry" Font-Underline="True"
                                    Width="70%" BackColor="skyblue" ForeColor="MediumVioletRed" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="14px"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblNeed_chem" runat="server" Text="Needed" Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoNeed_chem" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblProduct_entr_chem" runat="server" Text="Product Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoProduct_entr_chem" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblqty_Cap_chem" runat="server" Text="Qty Caption" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtqty_Cap_chem" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblinpu_entry_chem" runat="server" Text="Input Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoinpu_entry_chem" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="break">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblstock" runat="server" Text="Stockist Entry" Font-Underline="True"
                                    Width="70%" BackColor="skyblue" ForeColor="MediumVioletRed" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="14px"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblNeed_stock" runat="server" Text="Needed" Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoNeed_stock" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblprdentry_stock" runat="server" Text="Product Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoprdentry_stock" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblQty_Cap_stock" runat="server" Text="Qty Caption" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQty_Cap_stock" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblinpu_entry_stock" runat="server" Text="Input Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoinpu_entry_stock" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="break">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblUnlisr_Dr" runat="server" Text="Unlisted Dr. Entry" Font-Underline="True"
                                    Width="70%" BackColor="skyblue" ForeColor="MediumVioletRed" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="14px"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblneed_unlistDr" runat="server" Text="Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoneed_unlistDr" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblprdentry_unlistDr" runat="server" Text="Product Entry Needed" Font-Names="Arial"
                                    Font-Size="12px"></asp:Label>
                            </td>
                            <td class="spc">
                                <asp:RadioButtonList ID="rdoprdentry_unlistDr" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblRxQty_Cap_unlistDr" runat="server" Text="Rx Qty Caption" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRxQty_Cap_unlistDr" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblSamQty_Cap_unlistDr" runat="server" Text="Sample Qty Caption" Font-Names="Arial"
                                    Font-Size="12px"> </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSamQty_Cap_unlistDr" runat="server" Width="120" SkinID="MandTxtBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="spc">
                                <asp:Label ID="lblinpuEnt_Need_unlistDr" runat="server" Text="Input Entry Needed"
                                    Font-Names="Arial" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoinpuEnt_Need_unlistDr" runat="server" RepeatDirection="Horizontal"
                                    Font-Bold="true">
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                PopupControlID="pnlpopup" TargetControlID="linkgps" BackgroundCssClass="modalBackgroundNew">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="#e0f3ff" Height="480px" Width="370px"
                class="ontop" Style="left: 450px; top: 200px; position: absolute; display: none">
                <table width="50%" style="border: Solid 3px #4682B4; width: 100%; height: 100%" cellpadding="0"
                    cellspacing="0">
                    <tr style="background-color: #4682B4;">
                        <td style="height: 10%; color: White; font-weight: bold; font-size: larger;" align="center">
                            <asp:Label ID="lblHead" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 50%; color: black; font-weight: bold; font-size: 20pt">
                            <asp:Label ID="lblgps" Font-Bold="true" Font-Size="20px" runat="server" Text="GPS Settings"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="width: 350px; height: 400px; padding: 2px; overflow: auto; border: 1px solid Black;">
                                                    <asp:GridView ID="grdgps" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center"
                                                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <HeaderStyle Font-Bold="False" />
                                                        <PagerStyle CssClass="gridview1"></PagerStyle>
                                                        <SelectedRowStyle BackColor="BurlyWood" />
                                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                        <Columns>
                                                            <%--  <asp:TemplateField HeaderText="S.No">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                            <%--   <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="300px" HeaderStyle-ForeColor="white">
                                                                <ControlStyle Width="90%"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GPS">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkId" runat="server" Font-Bold="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Width="90px" Height="25px"
                                CssClass="BUTTON" Text="Save Setting" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" Height="25px"
                                CssClass="BUTTON"  />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <br />
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" CssClass="BUTTON" Width="70px" Height="25px"
                Text="Save" OnClick="btnSubmit_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="BUTTON" Width="60px" Height="25px"
                Text="Clear" OnClick="btnClear_Click" />
        </center>
    </div>
    </form>
</body>
</html>
</asp:Content>