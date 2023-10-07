<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditTPStartDate.aspx.cs" Inherits="MasterFiles_BulkEditTPStartDate" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit TP Start Date</title>
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
    $(function () {
        var $txt = $('input[id$=txtNew]');
        var $ddl = $('select[id$=ddlFilter]');
        var $items = $('select[id$=ddlFilter] option');

        $txt.keyup(function () {
            searchDdl($txt.val());
        });

        function searchDdl(item) {
            $ddl.empty();
            var exp = new RegExp(item, "i");
            var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

            if (arr.length > 0) {
                countItemsFound(arr.length);
                $.each(arr, function () {
                    $ddl.append(this);
                    $ddl.get(0).selectedIndex = 0;
                }
                    );
            }
            else {
                countItemsFound(arr.length);
                $ddl.append("<option>No Items Found</option>");
            }
        }

        function countItemsFound(num) {
            $("#para").empty();
            if ($txt.val().length) {
                $("#para").html(num + " items found");
            }

        }
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <ucl:Menu ID="menu1" runat="server" /> 
        <br />
      <table  width ="100%" border ="0">
                 <tr>
                 <td style="width:5%" />
                    <td width="40%">                 
                        <asp:Label ID="lblTPStartDate" runat="server" Text="Starting Date" 
                                    ></asp:Label>
                         <asp:TextBox ID="txtTPStartDate" onkeypress="Calendar_enter(event);" TabIndex="1" runat="server" SkinID="MandTxtBox"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtTPStartDate" />&nbsp;
                     <asp:Button ID="btnGo" TabIndex="2" runat="server" Width="120px" CssClass="BUTTON" Text="Set Starting Date" onClick="btnGo_Click" />
                  </td>
                   <td  width="35%">
                        <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager"></asp:Label>
                        <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
            ToolTip="Enter Text Here"></asp:TextBox>
                        <asp:DropDownList ID="ddlFilter" TabIndex="3" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:Button ID="btnsrch" TabIndex="4" runat="server" Text="Go" Width="30px" Height="25px" onclick="btnsrch_Click" CssClass="BUTTON"/>
            </td>

              </tr>
        </table> 
       
            <br />
            <table width="100%">
            <tr>
            <td align="center">
            <asp:Label ID="lblSelect" Text="Select the Manager" runat="server" ForeColor="Red" Font-Size="Large" visible="false"></asp:Label>
            </td>
            </tr>
            </table>
           <table width="100%" align="center">
            <tbody>               
                <tr>
                    <td colspan="2" align="center">
                        
                        <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            onrowdatabound="grdSalesForce_RowDataBound" Visible="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>                
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" Forecolor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width ="300px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsfName"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDes" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last TP Date">                                    
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtLastTPStDt" runat="server" Text='<%# Bind("Last_TP_Date") %>'></asp:Label>        
                                    </ItemTemplate>
                                </asp:TemplateField>                                       
                                 <asp:TemplateField HeaderText="Existing TP Date">                                    
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtExtTPStDt" runat="server" Text='<%# Bind("Sf_TP_DCR_Active_Dt") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="New TP Date">                                    
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTPStDt" runat="server" onkeypress="Calendar_enter(event);" SkinID="MandTxtBox"></asp:TextBox>
                                        <asp:CalendarExtender   
                                            ID="CalendarExtender1"   
                                            TargetControlID="txtTPStDt"  Format="dd/MM/yyyy"                                             
                                            runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>

                            <%--      <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server"  Text='<%# Bind("Des_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                            </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                        </asp:GridView>
                    </td> 
                </tr> 
            </tbody>
        </table>
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" Text="Update" Width="80px" Height="25px" CssClass="BUTTON" Visible="false"
                onclick="btnSubmit_Click" />
        </center>

         <div class="div_fixed">  
              
            <asp:Button ID="btnSave" runat="server" Width="80px" Height="25px" CssClass="BUTTON" Visible="false"  Text="Update" OnClick="btnSave_Click" />
        </div>
      <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../Images/loader.gif" alt="" />
</div>
    </div>
    </form>
</body>
</html>
