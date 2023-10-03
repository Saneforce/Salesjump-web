<%@ Page Title="Category Rate" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="CategoryRate.aspx.cs" Inherits="MasterFiles_CategoryRate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Product Rate</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
        .mGrid td:nth-child(9), .mGrid td:nth-child(10),.mGrid td:nth-child(11)
        {
           display: none;  
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
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
            $('#btnSubmit').click(function () {
                if ($("#txtEffFrom").val() == "") { alert("Please Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
            });
            $('#<%=btnGo.ClientID%>').click(function () {

                var SName = $('#<%=ddlState.ClientID%> :selected').text();
                if (SName == "") { alert("Please Select Category."); $('#<%=ddlState.ClientID%>').focus(); return false; }
            });

        }); 
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            $("input[type='text'], select").keydown(
function (event) {
    if ((event.keyCode == 39) || (event.keyCode == 9 && event.shiftKey == false)) {
        var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
        var idx = inputs.index(this);
        if (idx == inputs.length - 1) {
            inputs[0].select()
        } else {
            $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                $(this).attr("style", "BACKGROUND-COLOR: white; ");
            });

            inputs[idx + 1].focus();
        }
        return false;
    }
    if ((event.keyCode == 37) || (event.keyCode == 9 && event.shiftKey == true)) {
        var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
        var idx = inputs.index(this);
        if (idx > 0) {
            $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                $(this).attr("style", "BACKGROUND-COLOR: white; ");
            });

            inputs[idx - 1].focus();
        }
        return false;
    }
});
            //For navigating using up and down arrow of the keyboard
            $("input[type='text'], select").keydown(
function (event) {
    if ((event.keyCode == 40)) {
        if ($(this).parents("tr").next() != null) {
            var nextTr = $(this).parents("tr").next();
            var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
            var idx = inputs.index(this);
            nextTrinputs = nextTr.find("input[type='text'], select");
            if (nextTrinputs[idx] != null) {
                $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                    $(this).attr("style", "BACKGROUND-COLOR: white; ");
                });

                nextTrinputs[idx].focus();
            }
        }
        else {
            $(this).focus();
        }
    }
    if ((event.keyCode == 38)) {
        if ($(this).parents("tr").next() != null) {
            var nextTr = $(this).parents("tr").prev();
            var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
            var idx = inputs.index(this);
            nextTrinputs = nextTr.find("input[type='text'], select");
            if (nextTrinputs[idx] != null) {
                $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                    $(this).attr("style", "BACKGROUND-COLOR: white;");
                });

                nextTrinputs[idx].focus();
            }
            return false;
        }
        else {
            $(this).focus();
        }
    }
});
        });   
      
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

//        $("[id*=txtMRP]").live("change", function () {
//            alert('hi');
//        });
        $("[id*=txtMRP]").live("keyup", function () {
            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                   
                    var discount = (parseFloat($("[id*=txtdist_dsc]", row).val()) * parseFloat($(this).val()) / 100);
                    $("[id*=txtRP]", row).val(parseFloat($("[id*=txtdist_dsc]", row).val()) - parseFloat(discount));
                    var discount1 = (parseFloat($("[id*=txtretl_dsc]", row).val()) * parseFloat($(this).val()) / 100);
                    $("[id*=txtNSR]", row).val(parseFloat($("[id*=txtretl_dsc]", row).val()) - parseFloat(discount1));
                    var discount2 = (parseFloat($("[id*=txtDP]", row).val()) * parseFloat($(this).val()) / 100);
                    $("[id*=txtTarg]", row).val(parseFloat($("[id*=txtDP]", row).val()) - parseFloat(discount2));
                }
            } else {
                $(this).val('');

            }

        });
        $("[id*=txtRP]").live("keyup", function () {
            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=txtMRP]", row).val(0);
                     
                }
            } else {
                $(this).val('');

            }
        });
        $("[id*=txtNSR]").live("keyup", function () {

            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=txtMRP]", row).val(0);
                    
                }
            } else {
                $(this).val('');

            }

        });
        $("[id*=txtTarg]").live("keyup", function () {

            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=txtMRP]", row).val(0);
                   
                }
            } else {
                $(this).val('');

            }

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
	 <table width="100%">
        <tr>
            <td style="width:7.2%" />
            <td >
                <asp:Button ID="btn_Rate_Gen" runat="server" Visible="false" CssClass="BUTTON" Width="150px" 
              Height="25px" Text="Rate Statewise Generation" onclick="btn_Rate_Gen_Click"  />&nbsp;
               <asp:Button ID="btn_Rate_View" runat="server" Visible="false" CssClass="BUTTON"  Width="150px" 
            Height="25px" Text="Rate Statewise View" onclick="btn_Rate_View_Click"  />&nbsp;
            </td>            
        </tr>
      </table>
      <%--  <ucl:Menu ID="menu1" runat="server" />--%>

        <br />
 		
        <center>
            <table border="0" cellpadding="3" cellspacing="3">
            <tr>
            <td>
             <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Select Division" Visible="true"></asp:Label>
             </td>
             <td>
                          <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" 
                           CssClass="ddl" Visible="true" 
                         >
                         </asp:DropDownList>
                         </td>
            </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblState" runat="server" SkinID="lblMand" Width="100px"><span style="color:Red">*</span>Category Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
					  <asp:RequiredFieldValidator ID="RFValidator1_ddl" InitialValue="0" runat="server" ErrorMessage="RequiredField" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblEffFrom" runat="server" SkinID="lblMand" Width="100px" Visible="true"><span style="color:Red">*</span>Effective From</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffFrom" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            Width="106px" TabIndex="6" Visible="true"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                            runat="server" />
                    </td>
                </tr>
            </table>
  
 		

            <br />
            <center>
                <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary btn-md" Text="Go"
                    OnClick="btnGo_Click" />
            </center>
            <br />
            <center>
                <table id="tblRate" runat="server" width="100%" align="center">
                    <tbody>
                        <tr>
                            <td style="padding-left: 5%">
                                <asp:GridView ID="GrdDoctor" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                    CssClass="mGrid" EmptyDataText="No Records Found" GridLines="None" HorizontalAlign="Center"
                                    OnRowCreated="GVMissedCall_RowCreated" ShowHeader="false" Width="90%">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr" />
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="40px">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prod_Code" Visible="false">
                                            <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                            </ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProd_Code" runat="server" Text='<%#   Bind("Product_Detail_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="240px" HeaderText="Product Name" HeaderStyle-ForeColor="white"
                                            ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProdName" SkinID="MandTxtBox" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="120px">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("product_unit")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM Value" HeaderStyle-Width="90px">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Sample_Erp_Code")%>' CssClass="price"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Dis" HeaderStyle-Width="140px" Visible="true">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMRP" name="txtQuantity" Width="60px" Style="text-align: right;"
                                                    MaxLength="8" CssClass="ttlQty" Text='<%#(Eval("Discount"))%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Retailer Price" HeaderStyle-Width="120px">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                    Text='<%#(Eval("RP_Base_Rate"))%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NSR Price" HeaderStyle-Width="120px">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNSR" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                    Text='<%#(Eval("RP_Case_Rate"))%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Target Price" HeaderStyle-Width="120px">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTarg" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                    Text='<%#(Eval("MRP_Rate"))%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor Discount" HeaderStyle-Width="120px" Visible="true">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdist_dsc" Visible="true" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                    Text='<%#(Eval("RP_Base_Rate1"))%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Retailer Discount" HeaderStyle-Width="120px" Visible="true">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtretl_dsc" Visible="true" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                    Text='<%#(Eval("RP_Case_Rate1"))%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Distributor Price" HeaderStyle-Width="120px" Visible="true">
                                            <ControlStyle Width="60%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                            </ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="Total"
                                                    Text='<%#(Eval("MRP_Rate1"))%>' runat="server" name="lblPrice"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                                <%--<asp:GridView ID="grdProdRate" runat="server" Width="85%" HorizontalAlign="Center" 
                        AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" 
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                        AllowSorting="True"  OnSorting="grdProdRate_Sorting">
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>                
                           <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="40px">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prod_Code" Visible="false">
                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProd_Code" runat="server" Text='<%#   Bind("Product_Detail_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField    HeaderStyle-Width="240px" HeaderText="Product Name" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProdName" SkinID="MandTxtBox"  runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProdName"  runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="90px" >                                
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("Product_Sale_Unit")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="MRP"  HeaderStyle-Width="140px">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMRP" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("MRP_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Distributor Price" HeaderStyle-Width="120px">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDP" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA"  Text='<%#(Eval("Distributor_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Retailer Price" HeaderStyle-Width="120px">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRP" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("Retailor_Price"))%>'  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="NSR Price" HeaderStyle-Width="120px">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNSR" Width="60px" style="text-align:right;" MaxLength="8"
                                    CssClass="TEXTAREA" Text='<%#(Eval("NSR_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Target Price" HeaderStyle-Width="120px">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTarg" Width="60px" style="text-align:right;" MaxLength="8"
                                     CssClass="TEXTAREA" Text='<%#(Eval("Target_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP Rate" HeaderStyle-Width="120px">                                                                
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTarg" Width="60px" style="text-align:right;" MaxLength="8"
                                     CssClass="TEXTAREA" Text='<%#(Eval("Target_Price"))%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-md" runat="server" 
                            Text="Save" Visible="false" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>