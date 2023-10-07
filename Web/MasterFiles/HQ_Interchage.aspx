<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HQ_Interchage.aspx.cs" Inherits="MasterFiles_HQ_Interchage" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Interchange Customers/Chemists/Hospitals</title>
       <style type="text/css">
        table.gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        table.gridtable th
        {
            padding: 5px;
        }
        table.gridtable td
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: #666666;
        }
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;

                    }
                }
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
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
    </div>
    <asp:Panel runat="server" HorizontalAlign="Right" >
    <asp:Button ID="btnclear" Text="Clear" runat="server" BackColor="LightBlue" Width="60px" Height="25px" onclick="btnclear_Click" />
    </asp:Panel>
    <center>
        <table class="gridtable" width="90%" style="background-color: White">
            <tr>
                <td style="width: 50%" align="center">
                    <table>
                        <tr>
                            <td style="border: none;" align="center">
                                <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="From Fieldforce"></asp:Label>
                                <%--<asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlFromFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                    OnSelectedIndexChanged="ddlFromFieldForce_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" BackColor="LightBlue" onclick="btnGo_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none">
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdTerritory" runat="server" Width="90%" HorizontalAlign="Center"
                                            EmptyDataText="No Records Found" AutoGenerateColumns="false" 
                                            GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdTerritory.PageIndex * grdTerritory.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="450px" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Listed Customers" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblListedDRCnt" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Chemists" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemistsCnt" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of UnListed Customers" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnListedDRCnt" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Hospitals" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHospitalCnt" runat="server" Text='<%# Bind("Hospital_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <%--    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true"
                                                            Text="Select" />--%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkTerritory" runat="server" Checked="true" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                          
                            </td>
                            <td  style="border: none;" >
                            <asp:Panel ID="pnlmove" runat="server" Visible="false">
                            <br />
                            <br />
                                 <br />
                        
                              
                                  <%--<img src="../Images/move.gif" />--%>
                                    <img src="../Images/in.gif" />
                                  </asp:Panel>
                                      
                                     <br />
                            <br />
                            <asp:Label ID="lblinterchange" Text="Interchange" Font-Size="Medium" Font-Bold="true" Font-Italic="true" ForeColor="Red" Visible="false" runat="server"></asp:Label>
                             <br />
                                 <br />
                            <asp:Panel ID="pnlmove1" runat="server" Visible="false">
                                   <%--<img src="../Images/move1.gif" />--%>
                                   <img src="../Images/in1.gif" />
                                   </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<img src="../Images/move1.gif" />--%>
                <td style="width: 45%" align="center">
                    <table>
                        <tr>
                            <td style="border: none" align="center">
                                <asp:Label ID="lbltoFF" runat="server" SkinID="lblMand" Text="To Fieldforce"></asp:Label>
                                <asp:DropDownList ID="ddlToFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                    OnSelectedIndexChanged="ddlToFieldForce_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:Button ID="btnGo1" runat="server" Width="30px" Height="25px" Text="Go" BackColor="LightBlue" onclick="btnGo1_Click" />
                                <br />
                                <asp:CheckBox ID="chkvacant" runat="server" AutoPostBack="true"  ForeColor="Red" 
                                    Text="Only Vacant Id's" oncheckedchanged="chkvacant_CheckedChanged" />
                            </td>

                        </tr>
                        <tr>
                            <td style="border: none;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdterritory1" runat="server" Width="85%" HorizontalAlign="Center"
                                            EmptyDataText="No Records Found" AutoGenerateColumns="false" 
                                            GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
                                            <HeaderStyle Font-Bold="False" />
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <%--     <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true"
                                                            Text="Select" />--%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkListedDR" runat="server" Checked="true" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdterritory1.PageIndex * grdterritory1.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="450px" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Listed Customers" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblListedDRCnt" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Chemists" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChemistsCnt" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of UnListed Customers" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnListedDRCnt" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Hospitals" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHospitalCnt" runat="server" Text='<%# Bind("Hospital_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%--   <td>
                    <table>
                        <tr>
                            <td style="border: none">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none" valign="middle">
                                <img src="../../Images/arr (1).gif" alt="" style="vertical-align: middle" />
                            </td>
                        </tr>
                    </table>
                </td>--%>
        <asp:Button ID="btnTransfer" runat="server" Text="Interchange" Visible="false" CssClass="BUTTON"
            Width="100px" Height="25px" OnClick="btnTransfer_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnRep" runat="server" Text="Replicate" CssClass="BUTTON" Visible="false"
            Width="100px" Height="25px" OnClick="btnRep_Click" />
    </center>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../../Images/loader.gif" alt="" />
    </div>
    </form>
</body>
</html>
