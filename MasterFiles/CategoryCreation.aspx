<%@ Page Title="Category Creation" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="CategoryCreation.aspx.cs" Inherits="MasterFiles_CategoryCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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

            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=txtDoc_Cat_SName.ClientID%>').val() == "") { alert("Enter Category Code."); $('#<%=txtDoc_Cat_SName.ClientID%>').focus(); return false; }
                if ($('#<%=txtDocCatName.ClientID%>').val() == "") { alert("Enter Category Name."); $('#<%=txtDocCatName.ClientID%>').focus(); return false; }
            });
        });
    </script>

    <form id="form1" runat="server">
    <div>
        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblDocSpeDtls" align="center"
                style="width: 30%">
                <tr>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblDoc_Cat_SName" runat="server" SkinID="lblMand" 
                            Height="19px" Width="100px"><span style="Color:Red">*</span>Category Code</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtDoc_Cat_SName" SkinID="MandTxtBox" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" MaxLength="6" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblDocCatName" runat="server" SkinID="lblMand" 
                            Height="18px" Width="100px"><span style="Color:Red">*</span>Category Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtDocCatName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                            MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" TabIndex="3" CssClass="BUTTON" Width="60px" Height="25px" Text="Save" OnClick="btnSubmit_Click" />
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</asp:Content>
