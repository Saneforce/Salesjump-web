<%@ Page Title="Product Target Achievement" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Product_Target_Achivement.aspx.cs" Inherits="MasterFiles_Product_Target_Achivement"
    EnableEventValidation="false" %>

<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Product Target Achievement</title>
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
         

            $(document).on('click', '.btnview', function () {

                if ($('#<%=ddlFieldForce.ClientID%>').val().toString() != "---Select Field Force---") {
                    var isChecked = "1";
                    if ( $('#<%=rbtnqty.ClientID%>').is(':checked')) {     
                        isChecked = "1";
                    }
                    if ($('#<%=rbtnval.ClientID%>').is(':checked')) {                     
                        isChecked = "2";
                    }

                  //  alert(isChecked);

                    window.open("Rpt_Target_view.aspx?Year=" + $('#<%=ddlFYear.ClientID%>').val() + "&Month=" + $('#<%=ddlFMonth.ClientID%>').val() + "&SF_Name=" + $('#<%=ddlFieldForce.ClientID%> :selected').text() + "&SF_Code=" + $('#<%=ddlFieldForce.ClientID%>').val() + "&Mode=" + $('#<%=Mode.ClientID%>').val() + "&type=" + isChecked, "ModalPopUp", "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=900," + "height=600," + "left = 0," + "top=0");
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
        <div style="width:100%";>
            <center>
                <table cellpadding="0" cellspacing="5">
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                SkinID="ddlRequired">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="From Month"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="ddl">
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
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: center"
                                Width="60"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="ddl"
                                Width="60" >
                            </asp:DropDownList>
                           
                        </td>
                    </tr>
                     <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Slect Mode"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="Mode" runat="server" SkinID="ddlRequired" CssClass="ddl">
                                <asp:ListItem Value="1" Text="Field Force"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Product" Selected></asp:ListItem>                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                    <td align="left" class="stylespc">
                     <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Slect Type"></asp:Label>
                    </td>
                    <td align="left">
                     <asp:RadioButton ID="rbtnqty" runat="server" GroupName="type" Text="Quantity" Checked="true"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    <asp:RadioButton ID="rbtnval" runat="server" GroupName="type" Text="Value" Enabled="true"/>
                    </td>
                    </tr>
                </table>
                
                  <button name="btnview" type="button" class="btnview" style="width:100px" >
                                    View</button>
                <br />
            </center>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
