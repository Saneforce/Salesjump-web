﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_Tax_Allocation.aspx.cs" Inherits="MIS_Reports_Product_Tax_Allocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
     <script type="text/javascript" 
   src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script  type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>  
      <style type="text/css">
         td
    {
        cursor: pointer;
    }
    .hover_row
    {
        background-color: #e8f5e9;
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
    <script type="text/javascript">
        function displaymessage() {
            $("select").each(function () { this.selectedIndex = 0 });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            
        });
        function Get_Selected_Value(obj) {
            var checked_checkboxes = obj.parentNode.parentNode.parentNode;
            var chk_box = checked_checkboxes.getElementsByTagName("input");
            var message = "";
            var tst = 0;
            for (var i = 0; i < chk_box.length; i++) {
                if (chk_box[i].type == "checkbox" && obj != chk_box[i]) {

                    if (chk_box[i].checked) {
                        tst = 1;
                        var row = chk_box[i].parentNode.parentNode;
                        message += row.innerText;
                        message += ",";
                        $('#' + chk_box[2].id).val(message);
                    }
                    if (tst == 0) {
                        $('#' + chk_box[2].id).val('');
                    }
                }
            }

        }
            function CheckAll(thisObject) {
                var listBox = thisObject.parentNode.parentNode.parentNode;    
                var inputItems = listBox.getElementsByTagName("input");
                var message = "";
                for (var i = 0; i < inputItems.length; i++) {

                    var row = inputItems[i].parentNode.parentNode;

                    if (inputItems[i].type == "checkbox" && thisObject != inputItems[i]) {
                        inputItems[i].checked = true;
                        if ($('#' + inputItems[3].id).checked) {
                            inputItems[i].checked = true;
                            message += row.innerText;
                            message += ",";
                            $('#' + inputItems[2].id).val(message);
                        }
                        else {
                            if (thisObject.checked) {
                                inputItems[i].checked = true;
                                message += row.innerText;
                                message += ",";
                                $('#' + inputItems[2].id).val(message);

                            }
                            else {

                                inputItems[i].checked = false;
                                $('#' + inputItems[2].id).val('');
                            }
                        }
                    }
                }
            }
        
        </script>
    <script type="text/javascript">
        $(function () {
            $(".ddlclass").change(function () {
                var cur = $(this).closest('td').find('select option:selected').val();
                var tt = ($(this).closest('td').find("input").val());
                var count = 0;
                $(this).closest('table').find('tr').each(function () {
                    var myString = $(this).find('td:eq(2)');
                    var sillyString = myString.find('input').val();
                    if (tt == sillyString) {
                        var countryTag = $(this).find('td:eq(2)'); // return the whole select option as string
                        var selCntry = countryTag.find('option:selected').val();
                        var selCntrytext = countryTag.find('option:selected').text();
                        if (cur == selCntry) {
                            count += 1;
                            if (count > 1) {
                                alert('Tax ' + selCntrytext + '  already selected');
                                countryTag.find('select').val('0');
                            }
                        }
                    }
                });
            });
        });
    </script>
    <script type="text/javascript" >
        $('a.btn.btn-primary').live('click', function () {
             if ($(this).text().toString() == "+") {
              var $this = $(this),
          $parentTR = $this.closest('tr');
              $dff = $parentTR.clone(true, true);
            $dff.find("td:first").empty();
            $dff.find("td:nth-child(2)").empty();
            $dff.insertAfter($parentTR);
                            $(this).text("-");
                    }
                    else {
                        var iDX = $(this).closest('tr').index();
                        $(this).closest('tr').next().remove();
                        $(this).text("+");
                    }
                    return false;
                });
    </script>
     
       <script >
           $(function () {
               var clientId = '<%= GridView1.ClientID%>'
               var tds = $('#' + clientId + ' tr ').find('td:nth(1)');

               var temp = []; j = 0;
               for (var i = 1; i <= tds.length; i++) {
                   //debugger;
                   
                   var prev = $(tds[i - 1]);
                   var curr = $(tds[i]);
                   if (j == 0)
                       temp.push(prev);
                       if (prev.text() == curr.text()) {
                       //curr.context.parentNode.cells[3].childNodes[3].id;//pannel id
                       //$('#' + prev.context.parentNode.cells[3].childNodes[3].children[2].id).val();//previous row hidden filed value
                       //$('#' + curr.context.parentNode.cells[3].childNodes[3].children[2].id).val();//current row hidden field value which is checked for same product in another row.
                       prev.closest("td").next().next().find('a').text('-');
                       temp.push(curr);
                       j++;
                   }
                   else {
                       for (var k = 1; k < temp.length; k++) {
                           temp[k].empty();
                       }
                       temp = [];
                       j = 0;
                   }
               }
           }); 

    </script>
       <script type="text/javascript">
           $(function () {

               $('#ctl00_ContentPlaceHolder1_submit1').click(function () {
                   var n = $("#ctl00_ContentPlaceHolder1_GridView1").find("tr").length;
var arr=[];
                   $("#ctl00_ContentPlaceHolder1_GridView1 tr").each(function () {
                       var Product_name = $(this).find("td:eq(1)").text();

                       if (Product_name != "") {
                           var obj = $(this);
                           var productcode = obj.find("td:eq(0) :input").val();
                           var taxid = obj.find("td:eq(2) option:selected").val();
                           var statename = obj.find("td:eq(3) :input").val();//'Pondicherry,Tamil Nadu,'
                           if (taxid != "" && taxid != "0" && statename != "") {
                               arr.push({
                                   PCode: productcode,
                                   taxcode: taxid,
                                   stnm: statename
                               });
                           }
                       }
                   });
$.ajax({
                       type: 'POST',
                       url: 'Product_Tax_Allocation.aspx/insertdata',
                       async: false,
                       data: "{'Data':'" + JSON.stringify(arr) + "'}",
                       contentType: 'application/json; charset =utf-8',
                       success: function (data) {
                           var obj = data.d;
                           if (obj == 'true') {

                           }
                       },
                       error: function (result) {
                           //alert("Error Occured, Try Again");
                       }
                   });
 alert("Submitted Successfully");
                   return false;
               })
               return false;
           });  
        </script>  
                        </head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
   <asp:GridView ID="GridView1" runat="server" GridLines="None" OnPreRender="gridView_PreRender"  
                AutoGenerateColumns="False"  ForeColor="Black" BackColor="white" 
                Width="78%"   HeaderStyle-BackColor="CornflowerBlue" 
                OnRowDataBound="GridView1_RowDataBound"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" AlternatingRowStyle-BackColor="#DEE4E7" 
                HeaderStyle-Height="40px">
                    <Columns>
                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                         <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Product_Detail_Code") %>' />
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="180" ItemStyle-Height="45"     HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Product_detail_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Detail" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="center" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                        <asp:DropDownList ID="drptax_detail" runat="server" SkinID="ddlRequired" ></asp:DropDownList>
                                            <asp:HiddenField ID="HiddenFieYY1" runat="server" Value='<%# Eval("Tax_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="State Name" ItemStyle-Width="150" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                <asp:TextBox ID="TextBox1" SkinID="TxtBxAllowSymb" runat="server" Width="150px" ReadOnly="true" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'"></asp:TextBox>
                        <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1" OffsetY="22" >
                </asp:PopupControlExtender>
                 <asp:Panel ID="Panel1" runat="server" Height="200px" Width="150px" BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"  ScrollBars="Auto" BackColor="#CCCCCC" >        
                     <asp:CheckBox ID="chkBox" runat="server" Text="Select All" OnClick="CheckAll(this)" />
                            <asp:CheckBoxList ID="ddlState1" runat="server"
                        DataTextField = "statename" DataValueField = "state_code" onclick="Get_Selected_Value(this);">
                        </asp:CheckBoxList>
                </asp:Panel>
                     </ItemTemplate>
                                    </asp:TemplateField>
         <asp:TemplateField HeaderText="Add"  ItemStyle-Width="160" ItemStyle-Height="35" HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>

               <a href="#" type="button" class="btn btn-primary" style="border: 1PX SOLID #000000; font-size: small; font-weight:bold; color:White; background-color: #6495ED;">+</a></td>                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

                    </asp:GridView>
                            </td>
                    </tr>
                </tbody>
                </table>
        
                    <br>
                    </br>
                    <br>
                    </br>
        <table align="center">
           
                    <tr><td >
                        <asp:Button ID="submit1" runat="server" Text="Save" Width="95px" Height="30px"  CssClass="button" style="border: 2px dotted #FF0000"
           Font-Names="Andalus" /> </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td><asp:Button ID="Button1" runat="server" Text="Cancel" UseSubmitBehavior="false" Width="95px" Height="30px" style="border: 2px dotted #FF0000" CssClass="button"  onclientclick="displaymessage()"
               Font-Names="Andalus" /></td>     
             </tr>
            </table>
         </div>
    </form>

</asp:Content>

