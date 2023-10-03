<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_Prod_Map.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Listeddr_Prod_Map" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Customer Product Map</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[src*=plus]").live("click", function () {
                if ($(this).closest("tr").next().css('display') == "table-row") {
                    $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                    $(this).next().remove();
                } else
                    $(this).closest("tr").next().css('display', 'table-row');
                $(this).attr("src", "../../../Images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../../../Images/plus.png");
                $(this).closest("tr").next().css('display', 'none');
            });

        });
    </script>
    <script type="text/javascript">
        function ChkFn(x) {
            aid = x.id.split('_');
            y = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_chkCatName')
            z = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_ddlPriority');
            var pval = z.getAttribute('p');
            // alert('|'+pval+'|');
            z.setAttribute('p', ((z.value != '0') ? z.options[z.selectedIndex].text : ""));
            document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML = document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML.replace(y.parentNode.getElementsByTagName('label')[0].innerHTML + ((pval != '') ? ' ( ' + pval + ' )' : '') + ', ', '')
            if (y.checked == true)
                document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML += y.parentNode.getElementsByTagName('label')[0].innerHTML + ((z.value != '0') ? ' ( ' + z.options[z.selectedIndex].text + ' )' : '') + ', '


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
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div id="Divid" runat="server">
        </div>
        <br />
          <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <center>
            <asp:GridView ID="grdDoctor" runat="server" Width="90%" AutoGenerateColumns="false"
                CssClass="mGridImg" EmptyDataText="No Records Found" OnRowDataBound="OnRowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <%--  <img alt = "" style="cursor: pointer" src="../../Images/plus.png" title="Click here to tag the Campaign Dr" />--%>
                            <img alt="" src="../../../Images/plus.png" />
                            <asp:Panel ID="pnlDetails" runat="server" Style="display: none">
                                <asp:GridView ID="grdCampaign" runat="server" Width="100%" AutoGenerateColumns="false"
                                    CssClass="mGridImg">
                                    <Columns>
                                        <%--  <asp:BoundField ItemStyle-Width="150px" DataField="Doc_SubCatSName" HeaderText="Short Name 
                               " />
                                <asp:BoundField ItemStyle-Width="150px" DataField="Doc_SubCatName" HeaderText="Name" />--%>
                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-BackColor="AliceBlue" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatName" onclick="ChkFn(this)" Font-Names="Calibri" Font-Size="Small"
                                                    runat="server" Text='<%# Eval("Product_Detail_Name") %>' />
                                                <%--  <asp:HiddenField ID="cbSubCat" runat="server" value='<%# Eval("Doc_SubCatCode") %>' />--%>
                                                <asp:Label ID="cbSubCat" runat="server" Text='<%# Eval("Product_Code_SlNo") %>' Visible="false" />
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priority" HeaderStyle-BackColor="AliceBlue" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                              <asp:DropDownList ID="ddlPriority" runat="server" onChange="ChkFn(this)" SkinID="ddlRequired">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:BoundField DataField="ListedDrCode" Visible="false" HeaderText="ListedDr Code" />--%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDrcode" runat="server" Text='<%# Eval("ListedDrCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ListedCustomer Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="lblDrName" runat="server" Text='<%# Eval("ListedDr_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField  DataField="ListedDr_Name"  HeaderText="ListedDr Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White" />--%>
                    <asp:BoundField DataField="Doc_QuaName" HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px"
                        HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="Doc_Special_SName" HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" 
                        HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="Doc_Cat_SName" HeaderText="Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" 
                        HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="Doc_ClsSName" HeaderText="Class" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" 
                        HeaderStyle-ForeColor="White" />
                    <asp:BoundField DataField="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px" 
                        HeaderStyle-ForeColor="White" />
                    <%--  <asp:BoundField  DataField="Doc_SubCatName" HeaderText="Mapped Campaign" ItemStyle-HorizontalAlign="Left" />--%>
                    <asp:TemplateField HeaderText="Mapped Product" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Doc_SubCatName" runat="server" Text='<%#Eval("Product_Detail_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
        </center>
    </div>
    <br />
    <center>
        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="BUTTON" Width="70px"
            Height="25px" OnClick="btnSubmit_Click" />
    </center>
    <div class="div_fixed">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" Width="70px"
            Height="25px" onclick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
