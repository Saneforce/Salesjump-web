<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Scheme_Master.aspx.cs" Inherits="MIS_Reports_Scheme_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../css/style.css" rel="stylesheet" />
     <link type="text/css" rel="stylesheet" href="../css/style1.css" />
<%--    <link href="../fonts/jquery.mobile-1.4.5.css" rel="stylesheet" type="text/css" />--%> 
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />

    <link href="../css/bootstrap-switch.CSS" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-slider/slider.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
  <script src="//code.jquery.com/jquery-1.10.2.min.js"></script>

  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        
    });
</script>




<%--  <script src="//code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>--%>
  <script type="text/javascript">
      $(document).ready(function () {
          $('.datetimepicker').datepicker({ format: "yyyy/mm/dd" });
      }); 
</script>
<style>
    .red .ajax__calendar_container
{
width:190px;
background-color: #ffffff; border:solid 1px #eeeeee;
-moz-border-radius-topleft: 8px/*{cornerRadius}*/; -webkit-border-top-left-radius: 8px/*{cornerRadius}*/; -khtml-border-top-left-radius: 8px/*{cornerRadius}*/; border-top-left-radius: 8px/*{cornerRadius}*/;
-moz-border-radius-topright: 8px/*{cornerRadius}*/; -webkit-border-top-right-radius: 8px/*{cornerRadius}*/; -khtml-border-top-right-radius: 8px/*{cornerRadius}*/; border-top-right-radius: 8px/*{cornerRadius}*/; 
-moz-border-radius-bottomleft: 8px/*{cornerRadius}*/; -webkit-border-bottom-left-radius: 8px/*{cornerRadius}*/; -khtml-border-bottom-left-radius: 8px/*{cornerRadius}*/; border-bottom-left-radius: 8px/*{cornerRadius}*/;
-moz-border-radius-bottomright: 8px/*{cornerRadius}*/; -webkit-border-bottom-right-radius: 8px/*{cornerRadius}*/; -khtml-border-bottom-right-radius: 8px/*{cornerRadius}*/; border-bottom-right-radius: 8px/*{cornerRadius}*/;
}
.red .ajax__calendar_body
{
width:180px;
height:150px;
background-color: #ffffff; border: solid 1px #eeeeee;
}
.red .ajax__calendar_header
{
background-color: #CC0505; margin-bottom: 8px;
-moz-border-radius-topleft: 4px/*{cornerRadius}*/; -webkit-border-top-left-radius: 4px/*{cornerRadius}*/; -khtml-border-top-left-radius: 4px/*{cornerRadius}*/; border-top-left-radius: 4px/*{cornerRadius}*/;
-moz-border-radius-topright: 4px/*{cornerRadius}*/; -webkit-border-top-right-radius: 4px/*{cornerRadius}*/; -khtml-border-top-right-radius: 4px/*{cornerRadius}*/; border-top-right-radius: 4px/*{cornerRadius}*/; 
-moz-border-radius-bottomleft: 4px/*{cornerRadius}*/; -webkit-border-bottom-left-radius: 4px/*{cornerRadius}*/; -khtml-border-bottom-left-radius: 4px/*{cornerRadius}*/; border-bottom-left-radius: 4px/*{cornerRadius}*/;
-moz-border-radius-bottomright: 4px/*{cornerRadius}*/; -webkit-border-bottom-right-radius: 4px/*{cornerRadius}*/; -khtml-border-bottom-right-radius: 4px/*{cornerRadius}*/; border-bottom-right-radius: 4px/*{cornerRadius}*/;
} 
.red .ajax__calendar_title
{
color: #ffffff; padding-top: 3px;
}
.red .ajax__calendar_next,
.red .ajax__calendar_prev
{
border:solid 2px #ffffff;
background-color: #ffffff;
-moz-border-radius-topleft: 18px/*{cornerRadius}*/; -webkit-border-top-left-radius: 18px/*{cornerRadius}*/; -khtml-border-top-left-radius: 18px/*{cornerRadius}*/; border-top-left-radius: 18px/*{cornerRadius}*/;
-moz-border-radius-topright: 18px/*{cornerRadius}*/; -webkit-border-top-right-radius: 18px/*{cornerRadius}*/; -khtml-border-top-right-radius: 18px/*{cornerRadius}*/; border-top-right-radius: 18px/*{cornerRadius}*/; 
-moz-border-radius-bottomleft: 18px/*{cornerRadius}*/; -webkit-border-bottom-left-radius: 18px/*{cornerRadius}*/; -khtml-border-bottom-left-radius: 18px/*{cornerRadius}*/; border-bottom-left-radius: 18px/*{cornerRadius}*/;
-moz-border-radius-bottomright: 18px/*{cornerRadius}*/; -webkit-border-bottom-right-radius: 18px/*{cornerRadius}*/; -khtml-border-bottom-right-radius: 18px/*{cornerRadius}*/; border-bottom-right-radius: 18px/*{cornerRadius}*/;
}
.red .ajax__calendar_hover .ajax__calendar_next,
.red .ajax__calendar_hover .ajax__calendar_prev
{
border:solid 2px #f7f7f7;
background-color: #ffffff;
-moz-border-radius-topleft: 4px/*{cornerRadius}*/; -webkit-border-top-left-radius: 4px/*{cornerRadius}*/; -khtml-border-top-left-radius: 4px/*{cornerRadius}*/; border-top-left-radius: 4px/*{cornerRadius}*/;
-moz-border-radius-topright: 4px/*{cornerRadius}*/; -webkit-border-top-right-radius: 4px/*{cornerRadius}*/; -khtml-border-top-right-radius: 4px/*{cornerRadius}*/; border-top-right-radius: 4px/*{cornerRadius}*/; 
-moz-border-radius-bottomleft: 4px/*{cornerRadius}*/; -webkit-border-bottom-left-radius: 4px/*{cornerRadius}*/; -khtml-border-bottom-left-radius: 4px/*{cornerRadius}*/; border-bottom-left-radius: 4px/*{cornerRadius}*/;
-moz-border-radius-bottomright: 4px/*{cornerRadius}*/; -webkit-border-bottom-right-radius: 4px/*{cornerRadius}*/; -khtml-border-bottom-right-radius: 4px/*{cornerRadius}*/; border-bottom-right-radius: 4px/*{cornerRadius}*/;
}
.red .ajax__calendar_dayname
{
text-align:center; margin-bottom: 4px; margin-top: 2px;
color:#000000;
} 
.red .ajax__calendar_day,
.red .ajax__calendar_month,
.red .ajax__calendar_year
{
margin:1px 1px 1px 1px;
text-align:center;
border:solid 1px #eeeeee;
color:#000000;
background-color: #f3f3f3;
}
.red .ajax__calendar_hover .ajax__calendar_day,
.red .ajax__calendar_hover .ajax__calendar_month,
.red .ajax__calendar_hover .ajax__calendar_year
{
color: #ffffff; font-weight:bold; background-color: #328BC8;border:solid 1px #328BC8;
}
.red .ajax__calendar_active .ajax__calendar_day,
.red .ajax__calendar_active .ajax__calendar_month,
.red .ajax__calendar_active .ajax__calendar_year
{
color: #ffffff; font-weight:bold; background-color: #F7B64A;	
}
.red .ajax__calendar_today .ajax__calendar_day
{
color: #CC0505; font-weight:bold; background-color: #ffffff;	
}
.red .ajax__calendar_other,
.red .ajax__calendar_hover .ajax__calendar_today
{
color: #ffffff;
font-weight:bold;
}
.ajax__calendar_days
{
background-color: #ffffff;
}
</style>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
     <script type="text/javascript" 
   src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script  type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>  

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=btsubmit]").bind("click", function () {

                var groupby = $('#<%= groupby.ClientID %>').find("option:selected").text();
                var state_code = $('#<%= ddlState.ClientID %>').find("option:selected").val();
                var date = $("#ctl00_ContentPlaceHolder1_Txt_Date").val();

                var stockist_code = $('#<%=Distributor.ClientID %>').find("option:selected").val();
              


                $.ajax({

                    type: 'POST',
                    url: 'Scheme_Master.aspx/deletedata',
                    async: false,
                    data: "{'state_code':'" + state_code + "','Effective_Date':'" + date + "','stockist_code':'" + stockist_code + "'}",
                    contentType: 'application/json; charset =utf-8',
                    success: function (data) {

                        var obj = data.d;
                        if (obj == 'true') {


                        }
                    },

                    error: function (result) {
                        alert("Error Occured, Try Again");
                    }
                });




                if (groupby == "Categorywise") {
                    var Customer = {};
                    $('[id*=gridcat]').find('tr:has(td)').each(function () {

                        var category_name = $(this).find("td:eq(1)").text();
                        var category_code = $(this).find("td:eq(0) :input").val();
                        var scheme = $(this).find("td:eq(2) :input").val();

                        var free = $(this).find("td:eq(3) :input").val();
                        var Discount = $(this).find("td:eq(4) :input").val();

                        var Nochk;

                        if (($(this).find("td:eq(5) :input").prop('checked'))) {


                            Nochk = 'Y';

                        }



                        if (scheme != "") {
                            $.ajax({
                                type: 'POST',
                                url: 'Scheme_Master.aspx/insertdatacategorywise',
                                async: false,
                                data: "{'category_name':'" + category_name + "','category_code':'" + category_code + "','Scheme':'" + scheme + "','Free':'" + free + "','Discount':'" + Discount + "','Package':'" + Nochk + "','state_code':'" + state_code + "','Eff_Date':'" + date + "','stockist_code':'" + stockist_code + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                  

                                    //window.location.reload();
                                }
                            });
                        }

                    });
                    alert("Saved successfully.!!");
                    return false;



                }



                else if (groupby == "Brandwise") {


                    var Customer = {};
                    $('[id*=gridbrand]').find('tr:has(td)').each(function () {

                        var Brand_name = $(this).find("td:eq(1)").text();
                        var Brand_code = $(this).find("td:eq(0) :input").val();
                        var scheme = $(this).find("td:eq(2) :input").val();

                        var free = $(this).find("td:eq(3) :input").val();
                        var Discount = $(this).find("td:eq(4) :input").val();


                        var Nochk;

                        if (($(this).find("td:eq(5) :input").prop('checked'))) {


                            Nochk = 'Y';

                        }




                        if (scheme != "") {
                            $.ajax({
                                type: 'POST',
                                url: 'Scheme_Master.aspx/insertdataBrandwise',
                                async: false,
                                data: "{'Brand_name':'" + Brand_name + "','Brand_code':'" + Brand_code + "','Scheme':'" + scheme + "','Free':'" + free + "','Discount':'" + Discount + "','Package':'" + Nochk + "','state_code':'" + state_code + "','Eff_Date':'" + date + "','stockist_code':'" + stockist_code + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    

                                    //window.location.reload();
                                }
                            });
                        }

                    });
                    alert("Saved successfully.");
                    return false;




                }

                else {

                    var Customer = {};
                    $('[id*=GridView1]').find('tr:has(td)').each(function () {

                        var Product_name = $(this).find("td:eq(1)").text();
                        var product_code = $(this).find("td:eq(0) :input").val();
                        var scheme = $(this).find("td:eq(2) :input").val();

                        var free = $(this).find("td:eq(3) :input").val();
                        var Discount = $(this).find("td:eq(4) :input").val();

                        //                        var gg = $(this).find("td:eq(5) :table tr td :input").html();



                        //                        $(this).closest("table").find("input").not(this).removeAttr("checked");
                        //                        alert(gg);
                        var Nochk;

                        if (($(this).find("td:eq(5) :input").prop('checked'))) {
                           
                            Nochk = 'Y';

                        }



                        if (scheme != "") {
                            $.ajax({
                                type: 'POST',
                                url: 'Scheme_Master.aspx/insertdata',
                                async: false,
                                data: "{'Product_name':'" + Product_name + "','Product_code':'" + product_code + "','Scheme':'" + scheme + "','Free':'" + free + "','Discount':'" + Discount + "','Package':'" + Nochk + "','state_code':'" + state_code + "','Eff_Date':'" + date + "','stockist_code':'" + stockist_code + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {


                                    //window.location.reload();
                                }
                            });
                        }

                    });
                    alert("Saved successfully.");
                    return false;
                }
            });


        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=GridView1] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
</script>
<style type="text/css">
    .onoffswitch {
    position: relative; width: 90px;
    -webkit-user-select:none; -moz-user-select:none; -ms-user-select: none;
}
.onoffswitch-checkbox {
    display: none;
}
.onoffswitch-label {
    display: block; overflow: hidden; cursor: pointer;
    border: 2px solid #999999; border-radius: 20px;
}
.onoffswitch-inner {
    display: block; width: 200%; margin-left: -100%;
    transition: margin 0.3s ease-in 0s;
}
.onoffswitch-inner:before, .onoffswitch-inner:after {
    display: block; float: left; width: 50%; height: 30px; padding: 0; line-height: 30px;
    font-size: 14px; color: white; font-family: Trebuchet, Arial, sans-serif; font-weight: bold;
    box-sizing: border-box;
}
.onoffswitch-inner:before {
    content: "Y";
    padding-left: 10px;
    background-color: #34A7C1; color: #FFFFFF;
}
.onoffswitch-inner:after {
    content: "N";
    padding-right: 10px;
    background-color: #EEEEEE; color: #999999;
    text-align: right;
}
.onoffswitch-switch {
    display: block; width: 18px; margin: 6px;
    background: #FFFFFF;
    position: absolute; top: 0; bottom: 0;
    right: 56px;
    border: 2px solid #999999; border-radius: 20px;
    transition: all 0.3s ease-in 0s; 
}
.onoffswitch-checkbox:checked + .onoffswitch-label .onoffswitch-inner {
    margin-left: 0;
}
.onoffswitch-checkbox:checked + .onoffswitch-label .onoffswitch-switch {
    right: 0px; 
}
</style>
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   

</head>
<body>
    <form id="form1" runat="server">
    <div>

    <br />
        <center>
<asp:ScriptManager ID="ScriptManager1" runat="server">
                  </asp:ScriptManager>
        <div class="container" style="width:100%">
        
        <div class="row">
            <label for="lblState" class="col-md-2 col-md-offset-3 control-label">
               State Name</label>
            <div class="col-md-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-flag"></i></span>
                   
                     <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired"  AutoPostBack="true"             
                        Style="font-size: 14px; max-width: 200px"  CssClass="form-control" 
                        onselectedindexchanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                </div>
            </div>
        </div>
        </BR>
        <div class="row">
            <label for="lblState" class="col-md-2 col-md-offset-3 control-label">
               Distributor</label>
            <div class="col-md-6 inputGroupContainer">    
                <div class="input-group">                              
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                 
                     <asp:DropDownList ID="Distributor" runat="server" SkinID="ddlRequired" Style="font-size: 14px; max-width: 200px"  CssClass="form-control">
                            </asp:DropDownList>
                </div>
            </div>
        </div>
        </BR>
        <div class="row">
            <label for="groupby" class="col-md-2 col-md-offset-3  control-label">
             Group By</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-th-list"></i></span>
                   
                    <asp:DropDownList ID="groupby" runat="server"  cssClass="form-control"
                        Style="font-size: 14px; min-width: 130px"  AutoPostBack="true"  OnSelectedIndexChanged="groupby_SelectedIndexChanged"  >
                        
                        <asp:ListItem Value="0">Select</asp:ListItem>
                          <asp:ListItem Value="1">Categorywise</asp:ListItem>
                            <asp:ListItem Value="2">Brandwise</asp:ListItem>
                         <asp:ListItem Value="3">SKUwise</asp:ListItem>

                        </asp:DropDownList>
                </div>
            </div>
        </div>
        </BR>
        <div class="row">
            <label for="groupby" class="col-md-2 col-md-offset-3  control-label">
            Effective From</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                     <asp:TextBox ID="Txt_Date" runat="server" style="padding:3px 15px;"  CssClass="form-control" type="text" Rows="3" placeholder="Date" onkeypress="return isNumberKey(event)" autocomplete="off" ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" TargetControlID="Txt_Date" Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                   
                </div>
            </div>
        </div>
        </BR>
        <div class="row">
            <div class="col-md-12 inputGroupContainer">
                <div class="input-group">
             <asp:Button ID="btnstate" runat="server" CssClass="button" Text="Go" 
                                Height="25px" OnClick="btnstate_Click" />
               <%-- <a name="btnview" id="btsubmit" type="button" class="btn btn-primary btnview"  OnClick="btnstate_Click" style="width: 100px">
                    View</a>--%>
            </div>
            </div>
        </div>
   


<%--
        <asp:Panel ID="pnlstate" runat="server" >
                <table border="0" cellpadding="3" cellspacing="3">
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblState" runat="server" SkinID="lblMand" Font-Bold="true">State Name</asp:Label>
                           
                         
                        </td><td>&nbsp;&nbsp;</td>
                           <td><asp:Label ID="Label1" runat="server" Text="Group By" Font-Bold="true"></asp:Label></td><td>   <asp:Button ID="btnstate" runat="server" CssClass="button" Text="Go" 
                                Height="25px" OnClick="btnstate_Click" /></td>

                     
                    </tr>
                </table>
            </asp:Panel>--%>
            </br>
            </br>
   <asp:GridView ID="GridView1" runat="server" GridLines="None" AutoGenerateColumns="false"  CssClass="newStly" ForeColor="Black"  Width="78%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" HeaderStyle-Height="40px">
                    <Columns>
                    

                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Product_detail_Code") %>' />
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="180" ItemStyle-Height="35"     HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet" >
                                        <ItemTemplate>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Product_detail_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheme" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" BorderColor="Aqua" class="form-control" Text='<%#Eval("Scheme")%>'  style="width:70px;height:19px;"></asp:TextBox>
                                           <%-- <asp:Label ID="SF_hq" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_HQ")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="fareeid"   runat="server"  BorderColor="Aqua" class="form-control"  Text='<%#Eval("free")%>'  style="width:70px;height:19px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Discount%" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="disid"   runat="server"  Text='<%#Eval("discount")%>'   BorderColor="Aqua" class="form-control"  style="width:70px;height:19px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Package Offer" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="CENTER">
                                        <ItemTemplate>
                                       <%-- <asp:CheckBoxList ID = "chkGender" runat="server" RepeatDirection = "Horizontal">
                    <asp:ListItem Text="Male" Value="M" />
                    <asp:ListItem Text="Female" Value="F" />
                </asp:CheckBoxList>--%>
 <asp:CheckBox ID="checkboxyes" runat="server"    Text ="Y" checked='<%# Eval("package").ToString().Equals("Y")%>' />

   </ItemTemplate>
                                    </asp:TemplateField>

                     
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

                    </asp:GridView>

                    <asp:GridView ID="gridcat" runat="server" GridLines="None" AutoGenerateColumns="false"  ForeColor="Black" CssClass="newStly" Width="78%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid"  HeaderStyle-Height="40px">
                    <Columns>
                    

                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Product_Cat_Code") %>' />
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category Name" ItemStyle-Width="180" ItemStyle-Height="35"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Cat_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheme" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" BorderColor="Aqua" class="form-control"  Text='<%#Eval("Scheme")%>' style="width:70px;height:19px;"></asp:TextBox>
                                           <%-- <asp:Label ID="SF_hq" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_HQ")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="fareeid"   runat="server"  BorderColor="Aqua" class="form-control"  Text='<%#Eval("free")%>' style="width:70px;height:19px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Discount%" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="disid"   runat="server"  BorderColor="Aqua" class="form-control" Text='<%#Eval("discount")%>'  style="width:70px;height:19px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Package Offer" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="CENTER">
                                        <ItemTemplate>
<asp:CheckBox ID="checkboxyes" runat="server"  Text ="Y" checked='<%# Eval("package").ToString().Equals("Y")%>' />
   </ItemTemplate>
                                    </asp:TemplateField>

                     
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

                    </asp:GridView>


                    <asp:GridView ID="gridbrand" runat="server" GridLines="None" AutoGenerateColumns="false"  ForeColor="Black" CssClass="newStly" Width="78%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" HeaderStyle-Height="40px">
                    <Columns>
                    

                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Product_Brd_Code") %>' />
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand Name" ItemStyle-Width="180" ItemStyle-Height="35"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Brd_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheme" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" BorderColor="Aqua" class="form-control" Text='<%#Eval("Scheme")%>'  style="width:70px;height:19px;"></asp:TextBox>
                                           <%-- <asp:Label ID="SF_hq" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_HQ")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="fareeid"   runat="server"  BorderColor="Aqua" class="form-control"  Text='<%#Eval("free")%>' style="width:70px;height:19px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Discount%" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="disid"   runat="server"  BorderColor="Aqua" class="form-control" Text='<%#Eval("discount")%>'  style="width:70px;height:19px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Package Offer" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="CENTER">
                                        <ItemTemplate>
<asp:CheckBox ID="checkboxyes" runat="server"   Text ="Y" checked='<%# Eval("package").ToString().Equals("Y")%>' />
   </ItemTemplate>
                                    </asp:TemplateField>

                     
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

                    </asp:GridView>
                    <br /><br< />
                     </div>

                    <table><tr><td></td><td></td><tr><td>     <asp:Button ID="btsubmit" runat="server" Text="Save"  
                BackColor="#68dff0" Width="80px"  Height="40px"  style="border-radius:5px;" 
              ></asp:Button></td><td>&nbsp;&nbsp;</td><td>   <asp:Button ID="btncancel" runat="server" Text="Cancel"    OnClientClick="function jj();"
              
                BackColor="#68dff0" Width="80px"    Height="40px" style="border-radius: 5px;"
              ></asp:Button></td></tr>
                    
                    </tr>
                    
                    </table>
               

             



        </center>
    </div>
    </form>
</body>
</html>

</asp:Content>

