<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubordinateDetails.aspx.cs"
    Inherits="MasterFiles_MGR_SubordinateDetails" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Subordinate Details</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
            $('#btnGo').click(function () {

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }




            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <center>
                <asp:Button ID="btnGo" runat="server" Text="Go" Width="35px" Height="25px" CssClass="BUTTON"
                    OnClick="btnGo_Click" />
            </center>
            <br />
            <center>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSelect" Text="Please Select the Division Name" runat="server" ForeColor="DarkGreen"
                                Font-Size="Large" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                EmptyDataText="No Records Found" AutoGenerateColumns="false" GridLines="None"
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="DCR Count">
                                <ItemTemplate>
                                    <asp:Label ID="lblDCRCount" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drs(SDP)" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDRSDP" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Terr" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTerr" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="DCR St.Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDCRStDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldForce" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlace" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PassWord" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Bind("sf_password") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TP Reporting" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server" Text='<%# Bind("sf_color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFType" runat="server" Text='<%# Bind("sf_type") %>'></asp:Label>
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
