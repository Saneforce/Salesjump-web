<%@ Page Title="Closing Stock Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Closing_Stock_Entry.aspx.cs" Inherits="MasterFiles_Closing_Stock_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Closing Stock Entry</title>
        <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
					
                    var SName = $('#<%=ddldis.ClientID%> :selected').text();
                    if (SName == "--Select--") { alert("Please Select Distributor Name."); $('#<%=ddldis.ClientID%>').focus(); return false; }
                    if ($("#txtEffFrom").val() == "") { alert("Please Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
                });

				 $('#btnGo').click(function () {
                   
                    var SName = $('#<%=ddldis.ClientID%> :selected').text();
                    if (SName == "--Select--") { alert("Please Select Distributor Name."); $('#<%=ddldis.ClientID%>').focus(); return false; }
                    if ($("#txtEffFrom").val() == "") { alert("Please Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
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
        <style type="text/css">
          
            .mGrid1
            {
                width: 100%;
                background-color: #fff;
                margin: 5px 0 10px 0;
                border: solid 1px #525252;
                border-collapse: collapse;
            }
            .mGrid1 td
            {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }
            .mGrid1 th
            {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }
            .mGrid1 .alt
            {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }
            .mGrid1 .pgr
            {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }
            .mGrid1 .pgr table
            {
                margin: 5px 0;
            }
            .mGrid1 .pgr td
            {
                border-width: 0;
                padding: 0 6px;
                border-left: solid 1px #666;
                font-weight: bold;
                color: #fff;
                line-height: 12px;
            }
            .mGrid1 .pgr a
            {
                color: #666;
                text-decoration: none;
            }
            .mGrid1 .pgr a:hover
            {
                color: #000;
                text-decoration: none;
            }
        </style>
    </head>

     
    <body>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <br />
            <center>
                <table border="0" cellpadding="3" cellspacing="3" class="form-group">
                   
                        <tr>
                        <td align="left">
                            <label class="control-label" style="">
                                Distributor Name</label>
                        </td>
                        <td align="left">
                            <div class="inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList runat="server" CssClass="form-control" name="ddldis" ID="ddldis">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:RequiredFieldValidator ID="RFValidator1_ddl" InitialValue="0" runat="server"
                                ErrorMessage="RequiredField" ControlToValidate="ddldis"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <label class="control-label" style="">
                                Date</label>
                        </td>
                        <td align="left" class="stylespc">
                            <div class="inputGroupContainer">
                                <div class="input-group date">
								 <span class="input-group-addon"><i
                                            class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtEffFrom" runat="server" CssClass="form-control" type="text" Rows="3"
                                        placeholder="Date" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                                        runat="server" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <asp:Button ID="btnGo" runat="server" class="btn btn-info" Text="Go" OnClick="btnGo_Click" />
                </center>
                <br />
                <center>
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="text-center">
                                    Select the Distributor Name & Date and Press the 'Go' Button</h4>
                            </div>
                            <div class="panel-body text-center">
                                <div id="grid">
                                    <table id="tblRate" runat="server" width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GrdDoctor" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        AllowPaging="false" CssClass="mGrid1" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        EmptyDataText="No Records Found" HorizontalAlign="Center" OnRowCreated="GVMissedCall_RowCreated"
                                                        ShowHeader="false" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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
                                                            <asp:TemplateField  HeaderStyle-Width="140px">
                                                                <ControlStyle Width="60%"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                                                </ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtMRP" name="txtQuantity" Width="60px" Style="text-align: right;"
                                                                        MaxLength="8" CssClass="ttlQty" Text='<%#(Eval("Distributer_Rate"))%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField  HeaderStyle-Width="120px">
                                                                <ControlStyle Width="60%"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                                                </ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="Total"
                                                                        Text='<%#(Eval("pieces"))%>' runat="server" name="lblPrice"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField  HeaderStyle-Width="120px">
                                                                <ControlStyle Width="60%"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                                                </ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRP" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                                         runat="server" Text='<%#(Eval("C_Case_Qty"))%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField  HeaderStyle-Width="120px">
                                                                <ControlStyle Width="60%"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                                                </ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNSR" Width="60px" Style="text-align: right;" MaxLength="8" CssClass="TEXTAREA"
                                                                         runat="server" Text='<%#(Eval("C_Pic_Qty"))%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </center>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSubmit" class="btn btn-info" runat="server" Text="Save" Visible="false"
                                OnClick="btnSubmit_Click" />

                        </td>
                    </tr>
                </table>
            
            </center>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
