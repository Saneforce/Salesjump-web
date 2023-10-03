<%@ Page Title="Product Target Achievement" Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Master.master" CodeFile="Product_Target_Achivement.aspx.cs"
    Inherits="MasterFiles_Product_Target_Achivement" EnableEventValidation="false" %>

<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Product Target Achievement</title>
        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <style type="text/css">
            input[type='text'], select, label
            {
                line-height: 22px;
                padding: 4px 6px;
                font-size: medium;
                border-radius: 7px;
                width: 100%;
                font-weight: normal;
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
            
            .button
            {
                display: inline-block;
                border-radius: 4px;
                background-color: #6495ED;
                border: none;
                color: #FFFFFF;
                text-align: center;
                font-bold: true;
                width: 75px;
                height: 29px;
                transition: all 0.5s;
                cursor: pointer;
                margin: 5px;
            }
            
            .button span
            {
                cursor: pointer;
                display: inline-block;
                position: relative;
                transition: 0.5s;
            }
            
            .button span:after
            {
                content: '»';
                position: absolute;
                opacity: 0;
                top: 0;
                right: -20px;
                transition: 0.5s;
            }
            
            .button:hover span
            {
                padding-right: 25px;
            }
            
            .button:hover span:after
            {
                opacity: 1;
                right: 0;
            }
            
            
            .ddl
            {
                border: 1px solid #1E90FF;
                border-radius: 4px;
                margin: 2px;
                font-family: Andalus;
                background-image: url('css/download%20(2).png');
                background-position: 88px;
                background-position: 88px;
                background-repeat: no-repeat;
                text-indent: 0.01px; /*In Firefox*/
            }
            .ddl1
            {
                border: 1px solid #1E90FF;
                border-radius: 4px;
                margin: 2px;
                background-position: 88px;
                background-position: 88px;
                background-repeat: no-repeat;
                text-indent: 0.01px; /*In Firefox*/
            }
            .col-sm-6
            {
                padding: 0px 3px 6px 4px;
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

            $(document).ready(function () {
                $('#<%=Mode.ClientID%>').change(function () {

                    if ($(this).val().toString() == "2") {
                        console.log("2");
                        $('#<%=rbtnqty.ClientID%>').attr('disabled', false);
                    }
                    else {
                        console.log("1");
                        $('#<%=rbtnval.ClientID%>').prop('checked', true);
                        $('#<%=rbtnqty.ClientID%>').attr('disabled', true);
                    }
                });

            });


            $(document).on('click', '#btnview', function () {

                if ($('#<%=ddlFieldForce.ClientID%>').val().toString() != "---Select Field Force---") {
                    var isChecked = "1";
                    if ($('#<%=rbtnqty.ClientID%>').is(':checked')) {
                        isChecked = "1";
                    }
                    if ($('#<%=rbtnval.ClientID%>').is(':checked')) {
                        isChecked = "2";
                    }
                    var sub_div_code = $('#<%=subdiv.ClientID%>').val();
                    //  alert(isChecked);

                    window.open("Rpt_Target_view.aspx?Year=" + $('#<%=ddlFYear.ClientID%>').val() + "&Month=" + $('#<%=ddlFMonth.ClientID%>').val() + "&SF_Name=" + $('#<%=ddlFieldForce.ClientID%> :selected').text() + "&SF_Code=" + $('#<%=ddlFieldForce.ClientID%>').val() + "&Mode=" + $('#<%=Mode.ClientID%>').val() + "&type=" + isChecked + "&Sub_Div=" + sub_div_code, "ModalPopUp", "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=900," + "height=600," + "left = 0," + "top=0");
                }
                else {
                    alert('Please select Field Force');
                    $('#<%=ddlFieldForce.ClientID%>').focus();
                }
            });


        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div class="container" style="width:100%">
            <div class="form-group">
                <div class="row">
                    <%--<asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Division" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <label id="Label3" class="col-md-2 col-md-offset-3  control-label">
                        Division</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server"  CssClass="form-control"
                                Style="min-width: 150px" Width="150" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <%-- <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <label id="lblFF" class="col-md-2 col-md-offset-3  control-label">
                        Field Force</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                               CssClass="form-control"  Width="350">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label id="lblFMonth" class="col-md-2 col-md-offset-3  control-label">
                        Month</label>
                    <%--<asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFMonth" runat="server"  CssClass="form-control"
                                Style=" min-width: 100px" Width="120">
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label id="lblFYear" class="col-md-2 col-md-offset-3  control-label">
                        Year</label>
                    <%--<asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFYear" runat="server"  CssClass="form-control"
                                Width="120" Style=" min-width: 100px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label id="Label1" class="col-md-2 col-md-offset-3  control-label">
                        Mode</label>
                    <%--<asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Mode" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-transfer"></i></span>
                            <asp:DropDownList ID="Mode" runat="server" CssClass="form-control"
                                Style=" min-width: 100px" Width="150">
                                <asp:ListItem Value="1" Text="Field Force"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Product" Selected></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label id="Label2" class="col-md-2 col-md-offset-3  control-label">
                        Type</label>
                    <%-- <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Type" CssClass="col-md-2 col-md-offset-3 control-label"></asp:Label>--%>
                    <div class="col-sm-6 inputGroupContainer">
                        <asp:RadioButton ID="rbtnqty" runat="server" GroupName="type" Text="Quantity" Checked="true"
                            class="radio-inline" Style="margin-top: 8px;" />
                        <asp:RadioButton ID="rbtnval" runat="server" GroupName="type" Text="Value" Enabled="true"
                            class="radio-inline" Style="margin-top: 8px;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-md-offset-5">
                        <button name="btnview" id="btnview" type="button" class="btn btn-primary" style="width: 100px">
                            View</button>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </body>
    </html
</asp:Content>