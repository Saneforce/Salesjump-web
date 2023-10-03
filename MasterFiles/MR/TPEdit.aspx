<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TPEdit.aspx.cs" Inherits="MasterFiles_MR_TPEdit" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Edit</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />
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
                            $('#btnGo').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {

                var FieldForce = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FieldForce == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

            });
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
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';

                for (i = 2; i < Incre; i++) {
                    if (Inputs[i].type != '') {

                        if (Inputs[i].type == 'text') {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }

                            var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlWT');
                            var ddlTerr = document.getElementById('grdTP_ctl' + index + '_ddlTerr');
                            var drpDownListValue = ddlWT.options[ddlWT.selectedIndex].innerHTML;

                            if (ddlWT.value == '0') {
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
                if (confirm("Do you want to Save Successfully ?")) {
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
        <br />
        <center>
            <table border="0" align="center">
                <tr style="height: 25px;">
                    <td align="left" class="stylespc" width="90px">
                        <asp:Label ID="lblYear" SkinID="lblMand" runat="server" Text="Year"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="height: 25px;" align="left">
                    <td>
                        <asp:Label ID="lblMonth" SkinID="lblMand" runat="server" Text="Month"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                          
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="75%" align="center">
                <tr>
                    <td align="center">
                      <span style="font-weight:bold;font-family:Verdana;font-size:8pt"> Field Force Name : </span> <asp:Label ID="lblFieldForce" SkinID="lblMand"  runat="server"></asp:Label>
                    </td>
                </tr>

               <%-- <tr style="height: 30px">
                    <td colspan="2" align="center">
                        <asp:Label ID="lblComment" Width="250px" Font-Size="Small" ForeColor="Black" BackColor="AliceBlue"
                            Height="20px" BorderWidth="2" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                            HorizontalAlign="Center" VerticalAlign="Middle" runat="server"></asp:Label>
                    </td>
                </tr>--%>

                <tr>
                    <td colspan="2" align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdTP" runat="server" Width="85%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                    OnRowDataBound="grdTP_RowDataBound" EmptyDataText="TP No found for Edit" GridLines="None"
                                    CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
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
                                                <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("TP_Date") %>' Width="90"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDay" runat="server" Text='<%#  Eval("TP_Day") %>' Width="90"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Type1" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlWT" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" OnSelectedIndexChanged="ddlWT_OnSelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route Plan 1" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                                    DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Type2" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlWT1" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" OnSelectedIndexChanged="ddlWT1_OnSelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route Plan 2" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr1" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                                    DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Type3" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlWT2" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" OnSelectedIndexChanged="ddlWT2_OnSelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route Plan 3" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr2" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                                    DataTextField="Territory_Name" DataValueField="Territory_Code">
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
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
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
                        <asp:Button ID="btnSave" CssClass="BUTTON" Width="170px" runat="server" Text="Send to Manager Approval"
                            OnClick="btnSave_Click" Visible="False" OnClientClick="return ValidateEmptyValue()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px"
                            Text="Clear" Visible="False" OnClientClick="return ValidateEmptyValue()" />
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
