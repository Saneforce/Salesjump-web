<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"  CodeFile="gift_master.aspx.cs" Inherits="MasterFiles_gift_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>gift_master</title>
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
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }

        thead th {
            border: 1px solid #ececec;
        }


        tbody td {
            text-align: right;
        }


            tbody td:first-child {
                text-align: left;
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
   <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //  $('input:text:first').focus();
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
            $('#<%=efecdtf.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy'
            })
            $('#<%=efecdt.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy'
            })
            $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=gft_name.ClientID%>').val() == "") { alert("Enter Gift Name."); $('#<%=gft_name.ClientID%>').focus(); return false; }
                if ($('#<%=point_val.ClientID%>').val() == "") { alert("Enter Points Value."); $('#<%=point_val.ClientID%>').focus(); return false; }
                if ($('#<%=val_gft.ClientID%>').val() == "") { alert("Enter Value Of Gift."); $('#<%=val_gft.ClientID%>').focus(); return false; }
                if ($('#<%=efecdtf.ClientID%>').val() == "") { alert("Select Effective From Date."); $('#<%=efecdtf.ClientID%>').focus(); return false; }
                if ($('#<%=efecdt.ClientID%>').val() == "") { alert("Select Effective To Date."); $('#<%=efecdt.ClientID%>').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:HiddenField runat="server" ID="imgid"/>
        <br />
        <center>
            <table border="0" cellpadding="3" cellspacing="3" id="tblProBrdDtls" align="center">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblgft_name" runat="server" SkinID="lblMand" Height="19px"
                            Width="100px"><span style="color:Red">*</span>Gift Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="gft_name" SkinID="MandTxtBox" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" runat="server" MaxLength="20" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblpoint_val" runat="server" SkinID="lblMand" Height="18px" Width="100px"><span style="color:Red">*</span>Points Value</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                       
                        <asp:TextBox ID="point_val" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" 
                            MaxLength="20" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
             <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblval_gft" runat="server" SkinID="lblMand" Height="18px" Width="100px"><span style="color:Red">*</span>Value Of Gift</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                       
                        <asp:TextBox ID="val_gft" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" 
                            MaxLength="20" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td align="left" class="stylespc">
                      <asp:Label ID="lblefecdf" runat="server" SkinID="lblMand" Height="18px" Width="100px"><span style="color:Red">*</span>Effective From</asp:Label>
                    </td>
                    <td align="left">
                     		  <asp:TextBox ID="efecdtf" runat="server" autocomplete="off" DataFormatString="{dd/MM/yyyy}" CssClass="form-control" Width="120">
                                </asp:TextBox>
					 </td>
                </tr>
                          <tr>
                    <td align="left" class="stylespc">
                      <asp:Label ID="lblefecdt" runat="server" SkinID="lblMand" Height="18px" Width="100px"><span style="color:Red">*</span>Effective To</asp:Label>
                    </td>
                    <td align="left">
                     		  <asp:TextBox ID="efecdt" runat="server" autocomplete="off" DataFormatString="{dd/MM/yyyy}" CssClass="form-control" Width="120">
                                </asp:TextBox>
					 </td>
                </tr>
              <tr>
               
          <td align="left" class="stylespc" style="FONT-SIZE: 8pt;COLOR: black;FONT-FAMILY: Verdana;padding-bottom: 30px; width: 202px;white-space: nowrap;">Gift Image</td>
                                <td style="font-size:Medium;height:38px;width:301px;padding-top: 12px;">
                                    <asp:FileUpload ID="fileup2" runat="server" Font-Size="8pt" COLOR="black" FONT-FAMILY="Verdana" Height="38px" Width="301px"/>
                                </td>
      
        </tr> 
                     <tr><asp:Image ID="prodImg" runat="server" Style="width: 111px;height: 123px;" /></tr> 
            </table>
           
            <br />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-md"
                Text="Save" OnClick="btnSubmit_Click"/>
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