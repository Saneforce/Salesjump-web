<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WrkTypeWise_Allowance.aspx.cs"
    Inherits="MasterFiles_WrkTypeWise_Allowance" EnableEventValidation="true" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", ".classAdd", function () { //
                var rowCount = $('.data-contact-person').length + 1;
                var contactdiv = '<tr class="data-contact-person">' +
                '<td><input type="text" name="f-name' + rowCount + '" class="form-control f-name01" /></td>' +
                '<td><select name="l-name' + rowCount + '" class="form-control l-name01"><option value="0">+</option><option value="1">-</option></select></td>' +
                '<td><input type="text" name="email' + rowCount + '" class="form-control email01" /></td>' +
                '<td><button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd">Add More</button>' +
                '<button type="button" id="btnDelete" class="deleteContact btn btn btn-danger btn-xs">Remove</button></td>' +
                '</tr>';
                $('#maintable').append(contactdiv); // Adding these controls to Main table class  
            });
            $(document).on("click", ".deleteContact", function () {
                $(this).closest("tr").remove(); // closest used to remove the respective 'tr' in which I have my controls   
            });
            //
            function getAllEmpData() {

                var data = [];
                $('tr.data-contact-person').each(function () {
                    var firstName = $(this).find('.f-name01').val();
                    //Bind to the first name with class f-name01  
                    var lastName = $(this).find('.l-name01').val(); //Bind to the last name with class l-name01  
                    var emailId = $(this).find('.email01').val(); //Bind to the emailId with class email01  
                    var alldata = {
                        'FName': firstName, //FName as per Employee class name in .cs  
                        'LName': lastName, //LName as per Employee class name in .cs  
                        'EmailId': emailId //EmailId as per Employee class name in .cs   
                    }
                    data.push(alldata);
                });
                console.log(data); //  
                return data;
            }
            $("#btnSubmit").click(function () {

                var data = JSON.stringify(getAllEmpData());
                //console.log(data);    

                $.ajax({

                    url: 'WrkTypeWise_Allowance.aspx/SaveData', //Home.aspx is the page   
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ 'empdata': data }),
                    success: function () {
                        alert("Data Added Successfully");
                        location.reload(true);
                    },
                    error: function () {
                        alert("Error while inserting data");
                        location.reload(true);

                    }
                });
            });
        });  
    </script>
    <style type="text/css">
        .mGrid1
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGrid1 td
        {
            border-color: Black;
            font-size: small;
            font-family: Calibri;
        }
        
        
        .mGrid1 th
        {
            padding: 4px 2px;
            color: white;
            background: #666699;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGrid1 .pgr
        {
            background: #666699;
        }
        .mGrid1 .pgr table
        {
            margin: 5px 0;
        }
        .mGrid1 .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            background: #666699;
            font-weight: bold;
            color: White;
            line-height: 12px;
        }
        
        .mGrid1 .pgr a
        {
            color: Red;
            text-decoration: none;
        }
        .mGrid1 .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        
        
        .mGrid1Img
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGrid1Img td
        {
            padding: 2px;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            background: F2F1ED;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGrid1Img th
        {
            padding: 4px 2px;
            color: white;
            background: #666699;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        .mGrid1Img .pgr
        {
            background: #666699;
        }
        .mGrid1Img .pgr table
        {
            margin: 5px 0;
        }
        .mGrid1Img .pgr td
        {
            border-width: 0;
            text-align: left;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: Red;
            line-height: 12px;
        }
        .mGrid1Img .pgr a
        {
            color: White;
            text-decoration: none;
        }
        .mGrid1Img .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        .gridview1
        {
            background-color: #666699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }
        
        .gridview1 a
        {
            margin: auto 1%;
            border-style: none;
            border-radius: 50%;
            background-color: #444;
            padding: 5px 7px 5px 7px;
            color: #fff;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }
        .gridview1 a:hover
        {
            background-color: #1e8d12;
            color: #fff;
        }
        .gridview1 td
        {
            border-style: none;
        }
        .gridview1 span
        {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 50%;
            padding: 5px 7px 5px 7px;
        }
        
        .grdView th
        {
            background-color: Yellow;
        }
        .grdView
        {
            border-collapse: collapse;
        }
        .grdView tr td
        {
            border: 2px;
            background-color: White;
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
        $('#btnSave').click(function () {
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

        $("[id*=txtFare]").live("change", function () {
            if (isNaN(parseInt($(this).val()))) {
                $(this).val('0');
            } else {
                $(this).val(parseInt($(this).val()).toString());
            }
        });
        $("[id*=txtFare]").live("keyup", function () {
            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=txtTotal]", row).val(parseFloat($(".price", row).val()) + parseFloat($(this).val()));
                }
            } else {
                $(this).val('');
            }
            var grandTotal = 0;
            $("[id*=txtTotal]").each(function () {
                grandTotal = grandTotal;
            });
            $("[id*=lblGrandTotal]").html(grandTotal.toString());
        });
        //
        $("[id*=txtAlw]").live("change", function () {
            if (isNaN(parseInt($(this).val()))) {
                $(this).val('0');
            } else {
                $(this).val(parseInt($(this).val()).toString());
            }
        });
        $("[id*=txtAlw]").live("keyup", function () {
            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=txtTotal]", row).val(parseFloat($(".price1", row).val()) + parseFloat($(this).val()));
                }
            } else {
                $(this).val('');
            }
            var grandTotal = 0;
            $("[id*=txtTotal]").each(function () {
                grandTotal = grandTotal;
            });
            $("[id*=lblGrandTotal]").html(grandTotal.toString());
        });

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table width="100%">
                <tr>
                    <td align="center" colspan="2">
                        <asp:GridView ID="grdWTAllowance" Width="80%" runat="server" AutoGenerateColumns="False"
                            BorderStyle="None" CssClass="mGrid" GridLines="Both" AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="grdWTAllowance_RowDataBound" ShowFooter="True" BackColor="White"
                            Style="border-collapse: collapse;" FooterStyle-HorizontalAlign="Right" Enabled="false">
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Width="4%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DCR Date" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDCR_Date" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Type" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorktype_Name" runat="server" Text='<%# Eval("WorkType_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Place Of Work" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Territory_Type" Width="200px" runat="server" Visible="false"
                                            Enabled="false" SkinID="ddlRequired" DataSource="<%# FillRou() %>" DataTextField="Plan_Name"
                                            DataValueField="Plan_No">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblWrkty_place" runat="server" Text='<%# Eval("Plan_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SF Name" Visible="false" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="SF_Name" Width="200px" runat="server" Enabled="false" SkinID="ddlRequired"
                                            DataSource="<%# Fillsf() %>" DataTextField="Sf_Name" DataValueField="Sf_Code">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allowance">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAlw" name="txtAllw" Style="text-align: right;" MaxLength="8"
                                            runat="server" Text='<%#(Eval("Expense_Allowance"))%>' CssClass="price"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblGrandTotal1" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDis" name="txtDis" Style="text-align: right;" MaxLength="8" runat="server"
                                            Text='<%#(Eval("Expense_Distance"))%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblGrandTotal2" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fare">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFare" name="txtFare" Style="text-align: right;" MaxLength="8"
                                            runat="server" Text='<%#(Eval("Expense_Fare"))%>' CssClass="price1"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblGrandTotal3" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" Style="text-align: right;" MaxLength="8" runat="server"
                                            Text='<%#(Eval("Expense_Total"))%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblGrandTotal" runat="server" Text="0" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                     <%--   <asp:Button ID="btnSave" Text="Save" Width="50px" Visible="false" CssClass="BUTTON" runat="server"
                            OnClick="btnSave_Click" />--%>
                    </td>
                </tr>
            </table>
        </center>
        <center>
            <table width="20%" align="left" style="margin-left: 10%">
                <tr>
                    <td align="center" colspan="2">
                        <asp:GridView ID="gvCustomers" Width="97%" CssClass="mGrid1" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" BorderWidth="1px" OnRowDataBound="gvCustomers_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Parameter" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                                    <ItemTemplate>
                                        <%# Eval("Parameter_Name")%>
                                        <asp:Label ID="lbl1" Visible="false" Text='<%# Eval("Parameter_Name")%>' runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Visible="true" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                                    <ItemTemplate>
                                        <%# Eval("Amount")%>
                                        <asp:Label ID="lbl2" Visible="false" Text='<%# Eval("Amount")%>' runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblAmountTotal" runat="server" Visible="true" CssClass="total" Text="0" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cal_type" Visible="false" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl3" Visible="false" Text='<%# Eval("Cal_Type")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cre_By" Visible="false" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_cre_by" Visible="false" Text='<%# Eval("Created_By")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--<table border="0" cellpadding="10" class="mGrid" cellspacing="0">
                            <tr>
                                <td style="width: 150px">
                                    <asp:Label ID="lblTot" runat="server" Visible="true" Text="Total"></asp:Label>
                                </td>
                                <td style="width: 150px">
                                    <asp:Label ID="lblAmountTot" runat="server" Visible="true" CssClass="total" Text="0" />
                                </td>
                            </tr>
                        </table>--%>
                        <br />
                        <div style="width: 250%">
                            <div>
                                <table class="table" id="maintable" width="250%">
                                    <thead>
                                        <tr>
                                            <th>
                                                Parameter
                                            </th>
                                            <th>
                                                Type
                                            </th>
                                            <th>
                                                Amount
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="data-contact-person">
                                            <td>
                                                <input type="text" name="f-name" class="form-control f-name01" />
                                            </td>
                                            <td>
                                                <select name="l-name" class="form-control l-name01">
                                                    <option value="0">+</option>
                                                    <option value="1">-</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" name="email" class="form-control email01" />
                                            </td>
                                            <td>
                                                <button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd">
                                                    Add More</button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <button type="button" id="btnSubmit" class="btn btn-primary btn-md pull-right btn-sm">
                                    Submit</button>
                            </div>
                        </div>
                        <br />
                        <asp:Button ID="Button2" runat="server" Text="Send Approval" OnClick="Button2_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" Text="Save" runat="server" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </center>
        <center>
            <table width="10%" align="right" style="margin-right: 10%">
                <tr>
                    <td style="width: 150px">
                        Grand Total:<br />
                        <asp:TextBox ID="Txt_OverallTotal" runat="server" Width="140" />
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
