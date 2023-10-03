<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Approval.aspx.cs" Inherits="MasterFiles_MGR_TP_Approval" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tour Plan Approval</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
        function validate() {
            var txtReason = document.getElementById('<%=txtReason.ClientID %>').value;
            if (txtReason == "") {
                alert("Please Enter the Reason");
                document.getElementById('<%=txtReason.ClientID %>').focus();
                return false;
            }

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Rejecte TP ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div style="margin-left: 90%">
            <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
        </div>
        <br />
        <center>
         <table align="center">
        <tr><td>
                <asp:Label ID="lblDeactivate" Font-Bold="true" ForeColor="Red" Font-Names="Verdana" Visible="false" Font-Size="8" runat="server"></asp:Label>
                </td></tr>
        </table>
        
       
            <table  align="center">
                <tbody>               
                    <tr valign="bottom">
                        <td colspan="5" align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of " Font-Size="Medium"
                                Font-Names="Verdana"></asp:Label>
                            <%-- <asp:Label ID="lblHead" runat="server" Text="Month Tour Plan - "
                        Font-Size="Medium" Font-Names="Lucida Calligraphy"></asp:Label>--%>
                            <asp:Label ID="lblmon" runat="server" Font-Size="Small" Font-Names="Verdana"
                                ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                     
                    <tr>
                        <td align="right">
                        <asp:Label ID="lblStatingDate" Visible="false" Font-Names="Verdana" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                            <asp:GridView ID="grdTP" runat="server" Width="85%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                OnRowDataBound="grdTP_RowDataBound" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="grdTP_SelectedIndexChanged">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
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
                                                DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" OnTextChanged="ddlWT_TextChanged"
                                                OnSelectedIndexChanged="ddlWT_OnSelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Plan" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTerr" runat="server" SkinID="ddlRequired" Width="250px"
                                                DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlWT1" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlWT1_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Plan" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTerr1" runat="server" SkinID="ddlRequired" Width="250px"
                                                AutoPostBack="true" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name"
                                                DataValueField="Territory_Code">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlWT2" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlWT2_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Plan" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTerr2" runat="server" SkinID="ddlRequired" Width="250px"
                                                DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObjective" runat="server" SkinID="MandTxtBox" Width="300">                                           
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </center>
        <br />
        <div style="margin-left: 40%">
            <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="90px" Text="Approve TP" OnClick="btnSave_Click" 
                OnClientClick="return ValidateEmptyValue()" />
            &nbsp
            
                    <asp:Button ID="btnReject" CssClass="BUTTON" runat="server" Text="Reject TP" Width="90px" OnClick="btnReject_Click"/>
              
            &nbsp
            <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand" runat="server"></asp:Label>
            &nbsp
            <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                runat="server"></asp:TextBox>
            &nbsp
            <asp:Button ID="btnSubmit" CssClass="BUTTON" Width="140px" runat="server" Visible="false" OnClientClick="return validate();"
                Text="Send for ReEntry" OnClick="btnSubmit_Click"  />
        </div>
     
    </div>
    </form>
</body>
</html>
