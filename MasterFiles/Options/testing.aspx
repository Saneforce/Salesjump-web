<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testing.aspx.cs" Inherits="MasterFiles_Options_testing" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Master Details</title>
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
            background-color: #ffffff;
            
        }
  
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript">

        $(function () {
            var temprow = $('[id*=GridView2] table tbody').find(".emptyTd");

            $('[id*=cbCheck]').change(function () {
                var checkbox = $(this);
                if ($(this).is(":checked")) {
                    var row = $(this).parent().closest('tr');
                    var temp = $(row).clone(true);

                    $('[id*=GridView2] table tbody').find(".emptyTd").remove();
                    $(row).find("td:first").remove();
                    $('[id*=GridView2] table tbody').append(row);
                }
                else {
                    $('[id*=GridView2] table tbody tr').each(function () {
                        if ($(this).find("td:first").html() == $(checkbox).parent().closest('tr').find("td:nth-child(2)").html()) {
                            $(this).remove();
                            if ($('[id*=GridView2] table tbody').has('td').length == 0) {
                                $('[id*=GridView2] table tbody').append(temprow);
                            }
                        }
                    });
                }
            });
        
        });        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
    </div>
    <div>
    <center>
        <asp:Panel ID="pnlTrans" runat="server" BorderWidth="1" Width="90%" BackColor="White">
            <table style="border-bottom: none" width="100%">
                <tr style="border: none">
                    <th align="right" style="border: none;" width="30%">
                        <asp:RadioButtonList ID="rdotransfer" BorderStyle="None" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rdotransfer_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Listed Customer &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:ListItem>
                            <asp:ListItem Text="Chemist" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </th>
                    <th align="right" width="20%" >
                        <asp:Button ID="Button1" runat="server" Text="Transfer" ForeColor="White" 
           BackColor="#A6A6D2" onclick="btnTransfer_Click" /> 
            <asp:Button ID="Button2" runat="server" Text="Clear All" BackColor="#A6A6D2" ForeColor="White" 
            onclick="btnClear_Click" />
                    </th>

                </tr>
            </table>
        </asp:Panel>

    <table class="gridtable" width="90%">
        <tr>
            <td style="width: 45%">
                <table>
                    <tr>
                        <td style="border: none">
                            <asp:Label ID="lblFF" Width="160px" runat="server" SkinID="lblMand" Text="Transfer From"></asp:Label>
                        </td>
                        <td style="border: none">
                            <asp:DropDownList ID="ddlFromFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                OnSelectedIndexChanged="ddlFromFieldForce_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none">
                            <asp:Label ID="lblterrFrom" Width="160px" runat="server" SkinID="lblMand" Text="Transfer From Territory"></asp:Label>
                        </td>
                        <td style="border: none">
                            <asp:DropDownList ID="ddlFromTerr" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                OnSelectedIndexChanged="ddlFromTerr_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none" >
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none;" colspan="2">
                            <asp:GridView ID="GridView1"  GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                runat="server" AutoGenerateColumns="false" >
                                <Columns>
                                    <asp:TemplateField>
                                    
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCheck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Customer Name"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Speciality">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField SortExpression="Doc_QuaName" ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Qualification" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Doc_ClsName" ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Class" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                    </ItemTemplate>                               
                                </asp:TemplateField>--%>
                                    <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Territory">
                                        <ItemTemplate>
                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 45%">
                <table>
                    <tr>
                        <td style="border: none">
                            <asp:Label ID="lbltoFF" Width="160px" runat="server" SkinID="lblMand" Text="Transfer To"></asp:Label>
                        </td>
                        <td style="border: none">
                            <asp:DropDownList ID="ddlToFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                OnSelectedIndexChanged="ddlToFieldForce_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none">
                            <asp:Label ID="lblterrTo" Width="160px" runat="server" SkinID="lblMand" Text="Transfer To Territory"></asp:Label>
                        </td>
                        <td style="border: none">
                            <asp:DropDownList ID="ddlToTerr" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                OnSelectedIndexChanged="ddlToTerr_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none">
                            <asp:Label ID="lblCount" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                    </tr>
                    <tr style="border: none;">
                        <td style="border: none;" colspan="2">
                            <asp:GridView ID="GridView2" 
                                runat="server"  GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false">
                                
                                <EmptyDataTemplate>                                
                                    <table cellpadding="0" cellspacing="0" style="border:none; " >
                                        <tr style="border: none">
                                       
                                            <th style="width:130px">
                                                Listed Doctor Name
                                            </th>
                                            <th style="width:90px">
                                                Category
                                            </th>
                                            <th style="width:90px">
                                                Speciality
                                            </th>
                                            <th style="width:90px">
                                                Territory
                                            </th>
                                        </tr>
                                        <tr class="emptyTd">
                                           <td style="border: none">
                                            
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
            <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Visible="false"
            CssClass="BUTTON" onclick="btnTransfer_Click" /> &nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="BUTTON" Visible="false" 
            onclick="btnClear_Click" />
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
