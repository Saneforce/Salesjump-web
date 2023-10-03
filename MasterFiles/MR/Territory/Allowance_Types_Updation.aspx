<%@ Page Title="Allowance Types Updation" Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Allowance_Types_Updation.aspx.cs" MasterPageFile="~/Master.master"
    Inherits="MasterFiles_Allowance_Types_Updation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />
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
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
    </style>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
        function ValidateEmptyValue() {
            //            var grid = document.getElementById('<%= GrdRoute.ClientID %>');
            //            if (grid != null) {

            //                var isEmpty = false;
            //                var Inputs = grid.getElementsByTagName("input");
            //                var cnt = 0;
            //                var index = '';

            //                for (var k = 0; k < Inputs.length; k++) {
            //                    var i = k;

            //                    if (Inputs[i].type == 'text') {
            //                        i = i + 2;
            //                        if (i.toString().length == 1) {
            //                            index = cnt.toString() + i.toString();
            //                        }
            //                        else {
            //                            index = i.toString();
            //                        }
            //                        console.log(index);



            //                        var Name = document.getElementById('ctl00_ContentPlaceHolder1_GrdRoute_ctl' + index + '_Territory_Code'); // document.getElementById('GrdRoute_ctl' + index + '_Territory_Code');
            //                        var Type = document.getElementById('ctl00_ContentPlaceHolder1_GrdRoute_ctl' + index + '_Territory_Type'); // document.getElementById('GrdRoute_ctl' + index + '_Territory_Type');
            //                        var Stay = document.getElementById('ctl00_ContentPlaceHolder1_GrdRoute_ctl' + index + '_Stay_Place');

            //                        if (Name.value != '' || Type.value != '0') {
            //                            isEmpty = true;
            //                        }
            //                        if (Name.value != '') {
            //                            if (Type.value == '0') {
            //                                alert('Select Type');
            //                                Type.focus();
            //                                return false;
            //                            }

            //                        }
            //                        if (Type.value != '0') {
            //                            if (Name.value == '') {
            //                                alert('Enter Name')
            //                                Name.focus();
            //                                return false;
            //                            }
            //                        }
            //                        console.log(Type.value);
            //                        if (Type.value == 'OS-EX') {
            //                            if (Stay.value == '0' || Stay.value == '') {
            //                                alert('Select Stay Place');
            //                                Stay.focus();
            //                                return false;
            //                            }
            //                        }
            //                    }

            //                }
            //            }
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

        $(document).ready(function () {
            $('#<%=GrdRoute.ClientID %> tr:not(:first-child)').each(function () {

                $(this).find('[id$=Stay_Place]').val($(this).find('[id$=lblStay_Place]').text());
                if ($(this).find('[id$=Territory_Type]').val() == 'OS-EX') {
                    $(this).closest('tr').find('[id$=Stay_Place]').prop("disabled", false);
                }
            });
            $(document).on('change', '.territory_change', function () {
                //alert($(this).val());
                if ($(this).val() == 'OS-EX') {

                    $(this).closest('tr').find('[id$=Stay_Place]').prop("disabled", false);
                }
                else {
                    $(this).closest('tr').find('[id$=Stay_Place]').prop("disabled", true);
                }

                var arr = [];
                $('#<%=GrdRoute.ClientID %> tr:not(:first-child)').each(function () {
                    if ($(this).children('td').eq(4).find('[id$=Territory_Type]').val() == "OS") {
                        arr.push({ text: $(this).children('td').eq(2).find('[id$=lblTerritory_Name]').text(), value: $(this).children('td').eq(1).find('[id$=lblTerritorys_Code]').text() });
                    }
                });
                $('.sataypalace').each(function () {
                    var option = $(this).val();
                    console.log(option);
                    var ddlCustomers = $(this);
                    ddlCustomers.empty().append('<option value="0">--Select--</option>');
                    $.each(arr, function () {
                        if (option == this['value']) {
                            ddlCustomers.append($("<option selected></option>").val(this['value']).html(this['text']));
                        }
                        else {
                            ddlCustomers.append($("<option></option>").val(this['value']).html(this['text']));
                        }
                    });

                });



            });


            //            $(document).on('focus', '.sataypalace', function () {
            //                // alert('en');
            //            });
            //            $('.sataypalace').each(function () {

            //            });

            $(document).on('click', '#btnSave', function () {

                var grid = document.getElementById('<%= GrdRoute.ClientID %>');
                if (grid != null) {

                    var isEmpty = false;
                    var Inputs = grid.getElementsByTagName("input");
                    var cnt = 0;
                    var index = '';

                    for (var k = 0; k < Inputs.length; k++) {
                        var i = k;

                        if (Inputs[i].type == 'text') {
                            i = i + 2;
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }
                            console.log(index);



                            var Name = document.getElementById('ctl00_ContentPlaceHolder1_GrdRoute_ctl' + index + '_Territory_Code'); // document.getElementById('GrdRoute_ctl' + index + '_Territory_Code');
                            var Type = document.getElementById('ctl00_ContentPlaceHolder1_GrdRoute_ctl' + index + '_Territory_Type'); // document.getElementById('GrdRoute_ctl' + index + '_Territory_Type');
                            var Stay = document.getElementById('ctl00_ContentPlaceHolder1_GrdRoute_ctl' + index + '_Stay_Place');

                            if (Name.value != '' || Type.value != '0') {
                                isEmpty = true;
                            }
                            if (Name.value != '') {
                                if (Type.value == '0') {
                                    alert('Select Type');
                                    Type.focus();
                                    return false;
                                }

                            }
                            if (Type.value != '0') {
                                if (Name.value == '') {
                                    alert('Enter Name')
                                    Name.focus();
                                    return false;
                                }
                            }
                            console.log(Type.value);
                            if (Type.value == 'OS-EX') {
                                if (Stay.value == '0' || Stay.value == '') {
                                    alert('Select Stay Place');
                                    Stay.focus();
                                    return false;
                                }
                            }
                        }

                    }
                }

                var arra = [];
                $('#<%=GrdRoute.ClientID %> tr:not(:first-child)').each(function () {
                    arra.push({
                        Territory_Code: $(this).children('td').eq(1).find('[id$=lblTerritorys_Code]').text(),
                        Territory_Name: $(this).children('td').eq(3).find('[id$=Territory_Code]').val(),
                        Alow_Type: $(this).children('td').eq(4).find('[id$=Territory_Type]').val(),
                        stay_ply: $(this).children('td').eq(5).find('[id$=Stay_Place]').val()
                    });

                });

               // alert(JSON.stringify(arra));

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Types_Updation.aspx/SaveDate",
                    data: "{'data':'" + JSON.stringify(arra) + "'}",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                        alert("Allowance Types has been updated successfully!!!");
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });

            });





        });


    </script>
    <form id="form1" runat="server">
    <div class="container" style="width: 60%">
        <asp:Label ID="lblterr" runat="server" Style="font-size: large"></asp:Label>
        <asp:GridView ID="GrdRoute" runat="server" AllowSorting="true" EmptyDataText="No Records Found"
            AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt" Width="100%" OnRowDataBound="GrdRoute_RowDataBound">
            <HeaderStyle Font-Bold="False" />
            <PagerStyle CssClass="gridview1"></PagerStyle>
            <SelectedRowStyle BackColor="BurlyWood" />
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Route Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTerritorys_Code" runat="server" Text='<%#Eval("Territory_Code")%>'
                            Style="display: none"></asp:Label>
                        <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Route_Code")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Route Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTerritory_Name" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Route New Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="Territory_Code" runat="server" SkinID="TxtBxAllowSymb" MaxLength="10"
                            Width="100%" onkeypress="AlphaNumeric_NoSpecialChars(event);" Text='<%#Eval("Territory_Name")%>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Allowance Type" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <asp:Label ID="lblTerritory_Type" runat="server" Text='<%# Eval("Allowance_Type") %>'
                            Visible="false" />
                        <asp:DropDownList ID="Territory_Type" runat="server" SkinID="ddlRequired" Width="100%"
                            CssClass="territory_change">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="HQ" Value="HQ"></asp:ListItem>
                            <asp:ListItem Text="EX" Value="EX"></asp:ListItem>
                            <asp:ListItem Text="OS" Value="OS"></asp:ListItem>
                            <asp:ListItem Text="OS-EX" Value="OS-EX"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stay Place" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <asp:Label ID="lblStay_Place" runat="server" Text='<%# Eval("Territory_SNo") %>'
                            Style="display: none" />
                        <asp:DropDownList ID="Stay_Place" runat="server" SkinID="ddlRequired" Width="100%"
                            Enabled="false" CssClass="sataypalace">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <div style="width: 100%; text-align: center;">
            <a id="btnSave" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                <span>Save</span></a>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</asp:Content>
