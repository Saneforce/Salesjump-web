<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TourPlan_Entry.aspx.cs" Inherits="MasterFiles_MR_TPEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TP Entry</title>

    
    
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <style type="text/css">
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
        
      .c1
        {
            width: 280px;
            height: 240px;            
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

    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Save Successfully ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    

</head>
<body>

    <script type="text/javascript">
    function ValidateEmptyValue() {
        var grid = document.getElementById('<%= grdTP.ClientID %>');
        if (grid != null) {
            
            var isEmpty = false;
            var Inputs = grid.getElementsByTagName("input");
            var Incre = Inputs.length ;
            var cnt = 0;
            var index = '';

            for (i = 2; i < Incre; i++) 
            {
                if (Inputs[i].type != '') 
                {

                    if (Inputs[i].type == 'text') 
                    {
                        if (i.toString().length == 1) 
                        {
                            index = cnt.toString() + i.toString();
                        }
                        else 
                        {
                            index = i.toString();
                        }

                        var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlWT');
                        var ddlTerr = document.getElementById('grdTP_ctl' + index + '_ddlTerr');
                        var drpDownListValue = ddlWT.options[ddlWT.selectedIndex].innerHTML;

                        if (ddlWT.value == '0') 
                        {
                            //isEmpty = true;
                            alert('Select Work Type')
                            ddlWT.focus();
                            return false;
                        }
                        if (drpDownListValue == 'Field Work') {
                            if (ddlTerr.value == '0') {
                                alert('Select Territory')
                                ddlTerr.focus();
                                return false;
                            }
                        }
                    
                    }
                }
            }

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Save Successfully ?")) 
            {
                confirm_value.value = "Yes";
            } 
            else 
            {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);

        }
    }
</script>

 <script type="text/javascript">
     function DraftValidateEmptyValue() {
         var grid = document.getElementById('<%= grdTP.ClientID %>');
         if (grid != null) {

             var isEmpty = false;
             var Inputs = grid.getElementsByTagName("input");
             var Incre = Inputs.length;
             var cnt = 0;
             var index = '';

             
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to Draft Save Successfully ?")) {
                 confirm_value.value = "Yes";
             }
             else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);

         }
     }
</script>

    <form id="form1" runat="server">
    
    <div>   
        <ucl:Menu ID="menu1" runat="server" />
       
        <center>
            <table id="tblMargin" runat="server" width="100%">
                <tr valign="bottom">
                    <td colspan="5" align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of " Font-Size="Medium"
                            Font-Names="Verdana"></asp:Label>                       
                        <asp:Label ID="lblmon" runat="server" Font-Size="Medium" Font-Names="Verdana"
                            ForeColor="Green"></asp:Label>
                             
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblLink" runat="server" Font-Size="Small" Font-Names="Verdana"
                            ForeColor="Black"></asp:Label>
                        <asp:HyperLink ID="hylEdit" runat="server" NavigateUrl="~/MasterFiles/MR/TPEdit.aspx"
                            Font-Size="Small" Font-Names="Verdana" ForeColor="Blue"></asp:HyperLink>
                            
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <asp:Panel ID="Panel1" runat="server" Style="text-align: left;">
                    <asp:Label ID="lblReason" runat="server" Style="text-align: left" Font-Size="Small"
                        Font-Names="Verdana" Visible="false"></asp:Label>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender2" TargetControlID="lblNote" BalloonPopupControlID="Panel1"
                    runat="server" Position="TopLeft" DisplayOnMouseOver="true" BalloonSize="Small">
                </asp:BalloonPopupExtender>
                <tr>
               
                    <td align="right">
                    <asp:Label ID="lblStatingDate" Visible="false" Font-Names="Verdana" runat="server"></asp:Label>
                    &nbsp;&nbsp
                        <asp:Label ID="lblNote" runat="server" Style="text-decoration: underline;" ForeColor="Red"
                            Font-Size="Small" Font-Names="Verdana" Text="Rejection Reason" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdTP" runat="server" Width="85%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" CssClass="mGridImg" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                         <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("TP_Date") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay" runat="server" Text='<%#  Eval("TP_Day") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWT" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                            DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" AutoPostBack="true" OnSelectedIndexChanged="GrdTP_ddlWTSelectedIndexChanged">
                                        </asp:DropDownList>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route Plan 1" HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTerr" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled = "false" >
                                        </asp:DropDownList>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWT1" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                            DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" OnSelectedIndexChanged="GrdTP1_ddlWT1SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route Plan 2" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTerr1" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled ="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWT2" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                            DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" OnSelectedIndexChanged="GrdTP2_ddlWT1SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route Plan 3" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTerr2" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled ="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObjective" runat="server" SkinID="MandTxtBox" Width="250">                                           
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </ContentTemplate>                    
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="80px" Text="Draft Save" OnClick="btnSave_Click" OnClientClick="return DraftValidateEmptyValue()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" CssClass="BUTTON" Width="170px" runat="server" Text="Send to Manager Approval"
                            OnClick="btnSubmit_Click" OnClientClick="return ValidateEmptyValue()" />
                        &nbsp;&nbsp;&nbsp;
                       
                    </td>
                </tr>
            </table>
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
