<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="rpt_Primary_Order.aspx.cs" Inherits="MasterFiles_rpt_Primary_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    
       <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>      
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>     
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"> 

    <title>Primary Order</title>
     <script type="text/javascript">   
         //document.getElementById("ddlacthead").style.visibility = none;
         function popUp(Sf_Name, div_code, sf_code, StartDate, EndDate, Order_no, Stockist_Name) {
            
             // strOpen = "rpt_dis_order_value.aspx?SF_Code=" + Sf_Code + "&Year=" + Year + "&SF_Name=" + name
             strOpen = "PriOrder_Edit_detail_view.aspx?Sf_Name=" + Sf_Name + "&Order_no=" + Order_no + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&div_code=" + div_code + "&sf_code=" + sf_code + "&Stockist_Name=" + Stockist_Name
             window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
         }
       </script>
<script type="text/javascript">  
           
    $(document).ready(function () {
        var tdate = new Date();
                 var Tmonth = tdate.getMonth() + 1;
                 var Tday = tdate.getDate();
                 var year = tdate.getFullYear();
                if (Tmonth < 10) {
                    Tmonth = '0' + Tmonth
                }
        var CDate = Tday + '/' + Tmonth + '/' + year ;
        $('#txtFromDate').datepicker({
            dateformat: 'dd-mm-yy', maxDate: new Date(),
            onSelect: function (dateText, inst) {
                if (dateText == "NaN/NaN/NaN") {
                    var   datetextf = (isNaN(parseInt(inst.selectedDay)) ? (('0' + $(inst.selectedDay).text()).slice(-2)) : ('0' + (inst.selectedDay)).slice(-2)) + '/' + ('0' + ((inst.selectedMonth) + 1)).slice(-2) + '/' + inst.selectedYear;
                  //  var datetextf = (isNaN(parseInt(inst.selectedDay)) ? $(inst.selectedDay).text() : inst.selectedDay) + '/' + (parseInt(inst.selectedMonth) + 1) + '/' + inst.selectedYear;
                $("#txtFromDate").text(datetextf);
            //    datetext = (isNaN(parseInt(inst.selectedDay)) ? (('0' + $(inst.selectedDay).text()).slice(-2)) : ('0' + (inst.selectedDay)).slice(-2)) + '/' + ('0' + ((inst.selectedMonth) + 1)).slice(-2) + '/' + inst.selectedYear;

                $('#<%=hdnfdate.ClientID%>').val(datetextf);
                __doPostBack(datetextf, 'a');
            }
            else
                {
                    var da = dateText.split('/')
                    $('#<%=hdnfdate.ClientID%>').val(da[1] + '/' + da[0] + '/' + da[2]);
                    __doPostBack((da[1] + '/' + da[0] + '/' + da[2]), 'a');
            }
        }
                     });
        $('#txtDate').datepicker({
            dateformat: 'dd-mm-yy', maxDate: new Date() ,
                     onSelect: function (datetext, inst) {
                         if (datetext == "NaN/NaN/NaN") {
                           var  datetextf = (isNaN(parseInt(inst.selectedDay)) ? (('0' + $(inst.selectedDay).text()).slice(-2)) : ('0' + (inst.selectedDay)).slice(-2)) + '/' + ('0' + ((inst.selectedMonth) + 1)).slice(-2) + '/' + inst.selectedYear;
                           // var datetextf = (isNaN(parseInt(inst.selectedDay)) ? $(inst.selectedDay).text() : inst.selectedDay) + '/' + (parseInt(inst.selectedMonth) + 1) + '/' + inst.selectedYear;
                             $("#txtDate").text(datetextf);
                            
                             $('#<%=hdntdate.ClientID%>').val(datetextf);
                             __doPostBack(datetextf, 'b');
                         }
                         else {
                             var da = datetext.split('/')
                             $('#<%=hdntdate.ClientID%>').val(da[1]+'/'+da[0]+'/'+da[2]);
                             __doPostBack((da[1] + '/' + da[0] + '/' + da[2]), 'b');
                         }
                     } 
                     });



                 var hdnfdate = $('#<%=hdnfdate.ClientID%>').val();
                 var hdntdate = $('#<%=hdntdate.ClientID%>').val();
                 if (hdnfdate == "") hdnfdate = CDate;
                 if (hdntdate == "") hdntdate = CDate;
                 sp = hdnfdate.split("/")
        to = hdntdate.split("/")
        $('#txtFromDate').val(hdnfdate)
        $('#txtDate').val(hdntdate)
                // $('#txtFromDate').val(sp[1] + '/' + sp[0] + '/' + sp[2]);
                // $('#txtDate').val(to[1] + '/' + to[0] + '/' + to[2]);


               
                $('.sfcode').hide();
               $('.superno').hide();
               $('.stockist').hide();   
               
                 //check box header
                 $("[id*=checkhead]").live("click", function () {
                     var chkHeader = $(this);
                     var grid = $(this).closest("table");
                     $("[id*=checkrow]", grid).each(function () {
                         if (chkHeader.is(":checked")) {
                             $(this).attr("checked", "checked");
                             $("[id*=ddlacthead]").prop("disabled", false);
                             $("[id*=hydetail]").prop("disabled", false);

                         } else {
                             $(this).removeAttr("checked");
                             $("[id*=ddlacthead]").prop("disabled", true);
                             $("[id*=hydetail]").prop("disabled", true);
                         }
                     });
                 });

                 //drop down header
                 $("[id*=ddlacthead]").live("change", function () {
                     var ddlHeader = $(this).children("option:selected").val();
                     var grid = $(this).closest("table");
                     $("[id*=ddlactrow]", grid).each(function () {
                         var chkbx = $(this).closest('tr').find(" [Type=checkbox]")[0];
                         if ($(chkbx).is(":checked")) {
                             $(this).val(ddlHeader);
                         } else {
                             $(this).val("");
                         }
                     });
                 });


                 $("[id*=ddlactrow]").live("change", function () {
                     var ddlselected = $(this).children("option:selected").val();
                     var grid = $(this).closest("table");
                     $(this, grid).each(function () {
                         var lbl = $(this).closest('tr').find("[id*=lblsuper]")[0];
                         var ddl = $(this).closest('tr').find("[id*=ddlsuper]")[0];
                         if (ddlselected == 3) {

                             $(ddl).prop("disabled", false);

                         }
                     });
                 });
                 $(document).on('click','.save', function () {
                    // if (confirm("Are you sure do you want to upload?")) {                        
                     var grid = $('.newStly').closest("table");                   
                     var row = $(grid).find('tbody').find("tr");                    
                         if (row.length > 0) {
                             $data = "[";
                             $(row, grid).each(function () {  
                                 var ddlrow = $(this).find("[id*=ddlactrow]");
                                 var ddlselected = $(ddlrow).children("option:selected").val();                                                       
                                 if (ddlselected == 1) {
                                     var sfcode = $(this).find(".sfcode").text();
                                     var stockist = $(this).find(".stockist").text();
                                     var sno = $(this).find('.superno').text();
                                     var ddlsuper= $(this).find("[id*=ddlsuper]");
                                     var ddlsuperselect = $(ddlsuper).children("option:selected").val();
                                     if (ddlsuperselect == $(this).find('.superno').text) {
                                         var superno =sno;
                                     }
                                     else {
                                         var superno = ddlsuperselect;
                                     }                                   
                                     var Order_no = $(this).find('.Order_no').text();
                                     var Order_Value = $(this).find('.Order_Value').text();
                                     var Order_flag = 1;
                                     if ($data != "[") $data += ",";
                                    // $data = '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + s_no + '","order_value":"' + Order_Value + '"}'
                                     $data += '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + superno + '","order_value":"' + Order_Value + '"}'

                                 }                                 
                                 if (ddlselected == 2) {
                                     var sfcode = $(this).find(".sfcode").text();
                                     var stockist = $(this).find(".stockist").text();
                                     var superno = $(this).find('.superno').text();
                                     var Order_no = $(this).find('.Order_no').text();
                                     var Order_Value = $(this).find('.Order_Value').text();
                                     var Order_flag = 2;
                                     if ($data != "[") $data += ",";
                                     // $data = '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + s_no + '","order_value":"' + Order_Value + '"}'
                                     $data += '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + superno + '","order_value":"' + Order_Value + '"}'
                                 }                               
                                 if (ddlselected == 3) {
                                     var sfcode = $(this).find(".sfcode").text();
                                     var stockist = $(this).find(".stockist").text();
                                     var superno=$(this).find('.superno').text();
                                     var ddlsuper = $(this).find("[id*=ddlsuper]");
                                     var ddlsuperselect = $(ddlsuper).children("option:selected").val();
                                     if (ddlsuperselect == $(this).find('.superno').text) {
                                         var superno = sno;
                                     }
                                     else {
                                         var superno = ddlsuperselect;
                                     } 
                                     var Order_no = $(this).find('.Order_no').text();
                                     var Order_Value = $(this).find('.Order_Value').text();
                                     var Order_flag = 3; appr
                                     if ($data != "[") $data += ",";
                                     // $data = '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + s_no + '","order_value":"' + Order_Value + '"}'
                                     $data += '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + superno + '","order_value":"' + Order_Value + '"}'
                                 }
                             });
                             $data += ']';
                             Xhsrdata = JSON.parse($data)
                             $.ajax({
                                 type: "POST",
                                 contentType: "application/json; charset=utf-8",
                                 async: false,
                                 url: "rpt_Primary_Order.aspx/updatePriorder",
                                 data: "{'priorder':'" + $data + "'}",
                                 dataType: "json",
                                 success: function (data) {
                                     alert(data.d);  
                                    
                                 },
                                 error: function (rs) {
                                     console.log(rs);
                                 }
                             });                           
                         }                 
                 });

             /* $('#txtFromDate').on('change', function (e) {
              //  var d = $(this).datepicker("getDate");
                //var text = ((d.getMonth() + 1) + '/' + (d.getDate()) + '/' + (d.getFullYear()));
                var text = $("#txtFromDate").val();
                if ($("#txtFromDate").val() == null) {
                 
                }
                else {
                    $('#<=hdnfdate.ClientID%>').val(text);
                    __doPostBack(text, 'a');
                    this.autocomplete = false;
                }
            });
            $('#txtDate').on('change', function (e) {
                var text = $('#txtDate').val();
                if ($('#txtDate').val() == null) {
                 
                }
                else {
                    $('#<=hdntdate.ClientID%>').val(text);
                    __doPostBack(text, 'b');
                    this.autocomplete = false;
                }
            });*/


             /*  var from = $("#txtFromDate").datepicker({
                   dateFormat: "mm/dd/yy",
                   changeMonth: true
               })
                   .on("change", function () {
                       to.datepicker("option", "minDate", getDate(this));
                   }),
                   to = $("#txtDate").datepicker({
                       dateFormat: "mm/dd/yy",
                       changeMonth: true
                   })
                       .on("change", function () {
                           from.datepicker("option", "maxDate", getDate(this));
                       });

               function getDate(element) {
                   var date;
                   var dateFormat = "mm/dd/yy";
                   try {
                       date = $.datepicker.parseDate(dateFormat, element.value);
                   } catch (error) {
                       date = null;
                   }

                   return date;
               }*/
              /* $("#txtDate").on("change", function () {              
                   var fromdate = $('#txtFromDate').val();
                  // if (fromdate.length <= 0) { alert('Enter From Date..!'); $('#txtFromDate').focus(); return false; };
                   var todate = $('#txtDate').val();
                 //  if (todate.length <= 0) { alert('Enter To Date..!'); $('#toDate').txtDate(); return false; };               
          
                   $.ajax({
                       type: "POST",
                       contentType: "application/json; charset=utf-8",
                       async: false,
                       url: "rpt_Primary_Order.aspx/salesforcelist",
                      data: "{'fdate':'" + fromdate + "','tdate':'" + todate + "'}",
                       dataType: "json",
                       success: function (data) {               
                           //var ddlFieldFxorce = $("#ddlFieldForce");
                           $('.form-control').empty().append('<option selected="selected" value="0">--- Select ---</option>');
                           $.each(data.d, function (key, value) {
                               $('.form-control').append($("<option></option>").val(value.sfcode).html(value.sfname));
                       });
                   },
                   error: function (result) {
                       alert(JSON.stringify(result));
                   }                   
               });
               });
               $('.ddlff').on("change", function () {
                   var ddlffid = $('option:selected',  this).val();
                   var fromdate = $('#txtFromDate').val();
                   var todate = $('#txtDate').val();
                    
                 //  getpriorder = function () {
                       $.ajax({
                           Type: "POST",
                           contentType: "application/json; charset=utf-8",
                           async: false,
                           url: "rpt_Primary_Order.aspx/bindpriordersf",
                         //  data: "{'fdate':'" + fromdate + "','tdate':'" + todate + "'}",
                           //data: "{'sfccode':'" + ddlffid + "','fdate':'" + fromdate + "','tdate':'" + todate + "'}",
                           datatype: "JSON",
                           success: function (data) {
                                var priorder = data.d;
                               var gvdata = $("#GVData");
                               $(gvdata).find("thead th").remove();
                               str = '<th style="min-width:150px;"><input type="checkbox" name="checkhead" CssClass="chkhead"></th>';
                               str += '<th style="min-width:150px;">Slno</th><th>Field Force</th><th>Distributer</th><th>Super Stockist</th>';
                               str += '<th>Order No</th><th>Order Value</th><th><input type="text" name="action"><select disabled ><option value="0">Select</option><option value="1">Confirm</option><option value="2">Reject</option></select> </th>';
                               $(gvdata).find('thead').append(str);
                               $(gvdata).find('tbody tr').remove();
                               if (priorder.length > 0) {
                                   for (var i = 0; i < data.d.length; i++) {
                                       str = '<td> <input type="checkbox" name="checkrow" CssClass="chkrow" /></td><td>' + (i + 1) + '</td>'
                                       str += '<td><input type="hidden" name="scode" CssClass="sfcode" value="' + priorder[i].Sf_Code + '"/><input type="text" name="sfname"  value="' + priorder[i].Sf_Name + '"/></td><td><input type="hidden" name="lbldistr" CssClass="stockist" value="' + priorder[i].stockist_code + '"/><input type="text" name="lblDistri" value="' + priorder[i].Stockist_Name + '"/></td>';
                                       str += '<td><input type="hidden" name="lblsuperno" CssClass="superno" value="' + priorder[i].Super_Stockist_Code + '"/><input type="text" name="lblsuper" value="' + priorder[i].Super_name + '"/> </td> ';
                                       str += '<td><input type="text" name="lblOno" CssClass="Order_no" value="' + priorder[i].Order_no + '"/></td><td><input type="text" name="lblOv" CssClass="Order_Value" value="' + priorder[i].Order_Value + '"/></td>';
                                       str += ' <td><input type="text" name="ddlacthead" CssClass="ddlhead" ><select ><option value="0">Select</option><option value="1">Confirm</option><option value="2">Reject</option><option value="3">Transfer</option></select></td>';
                                       str += '<td><a  onClick="this.href=PriOrder_Edit_detail_view.aspx?Sf_Name="' + priorder[i].Sf_Name + '"&Order_no="' + priorder[i].Order_no + '" "&StartDate="' + fromdate + ' "&EndDate="' + todate + '"&sf_code=" ' + priorder[i].sf_code + ' "&Stockist_Name=" ' + priorder[i].Stockist_Name + '"&Superstockist="' + priorder[i].S_Name + '"&Stockist_code=" ' + priorder[i].stockist_code + '"&Superstockistcode="' + priorder[i].Super_Stockist_Code + '"">Detail</a></td>';
                                       $(gvdata).find("tbody").append('<tr>' + str + '</tr>');
                                   }
                             
                                   //if (priorder[i].s_no > 0) {
                                     //  for (j = 0; j < priorder[i].s_no; j++) {
                                      //     str += '<td><input type="text" name="ddlsuper" ><select disable><option value="' + priorder[i].s_no + '">"' + priorder[i].s_no + '"</option></select></td>';
                                     //  }
                                   //}
                                }
                               else {
                                   $(gvdata).find('tbody').append('<tr><td colspan="14" style="color""red" font-weight:bold;">No Record Found.....</td></tr>');
                               }
                           },
                           error: function (result) {
                               alert(JSON.stringify(result));
                           }
                           
                       });
                  // }
                   });      */           
              

             });
         </script>
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
     

</style>

</head>
<body>
    <form id="form1" runat="server">
      
     
         <div>
            <br />          
      
                 <div class="container"  >
                    <div class="form-group" >   

        <table>
            <tr>
                
           <td>
      
            <label id="Label1" class="col-md-2"  >
                 Date</label>
               </td>
                <td>
            <div class="col-md-2 inputGroupContainer">  
                <div class="input-group" >
                    
       
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <input id="txtFromDate" name="txtFrom1" type="text"  class="form-control  datetimepicker" MaxLength="5"
                       style="min-width: 110px ; width: 150px;"  onfocus="this.style.backgroundColor='#E0EE9D'"
                          onblur="this.style.backgroundColor='White'"  TabIndex="1" SkinID="MandTxtBox" autocomplete="off"  CssClass="TEXTFDATE"  />                       
                   
                           <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                      <input id="txtDate" name="txtFrom" type="text"  CssClass="TEXTAREA" MaxLength="5"  class="form-control datetimepicker"
                              style="min-width: 110px ;width: 150px;" onfocus="this.style.backgroundColor='#E0EE9D'"   autocomplete="off"  onblur="this.style.backgroundColor='White'"
                             TabIndex="2" SkinID="MandTxtBox"  />
                     </div>
                    </div> 
                   </td>
                <td>
                <label id="lblFF" class="col-md-2" >
                  FieldForce Name</label>
            </td>
                <td>
                <div class="col-md-3 inputGroupContainer" >
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control ddlff" style="width:100px"
                         AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div></td>
            <td>            
       <asp:HiddenField ID="hdnfdate" runat="server" />
                <asp:HiddenField ID="hdntdate" runat="server" />
                  <asp:HiddenField ID="hdnffdate" runat="server" />
                <asp:HiddenField ID="hdnttdate" runat="server" />
                </td></tr>
            </table>
            
         </div>
             </div>
              <div class="form-group">
            <div class="col-md-12">    
     <asp:GridView ID="GVData" runat="server" GridLines="None" AutoGenerateColumns="false"  CssClass="newStly" ForeColor="Black"  Width="78%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" HeaderStyle-Height="40px" EnableViewState="true" OnRowDataBound="GVData_OnRowDataBound" >
                    <Columns>
                          <asp:TemplateField HeaderText="Select" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="CENTER">
                              <HeaderTemplate>
                                  <asp:CheckBox ID="checkhead" runat="server"  CssClass="chkhead" /></HeaderTemplate>
                                        <ItemTemplate>
                                  
                                <asp:CheckBox ID="checkrow" runat="server" CssClass="chkrow"   />

  </ItemTemplate>
                                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>                                           
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Force" ItemStyle-Width="180" ItemStyle-Height="35"     HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet" >
                                        <ItemTemplate>
                                            <asp:Label ID="scode" style="white-space:nowrap" runat="server" Font-Size="9pt"  CssClass="sfcode" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributer" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>
                                             <asp:Label ID="lbldistr" runat="server" style="white-space:nowrap"  Font-Size="9pt" CssClass="stockist" Text='<%#Eval("stockist_code")%>'  ></asp:Label>
                                        <asp:Label ID="lblDistri" runat="server" style="white-space:nowrap"  Font-Size="9pt" Text='<%#Eval("Stockist_Name")%>'  ></asp:Label>
                                              
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Super Stockist" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>
                                              <asp:Label ID="lblsuperno" runat="server" style="white-space:nowrap"  CssClass="superno" Font-Size="9pt" Text='<%#Eval("Super_Stockist_Code")%>'  ></asp:Label>
                                        <asp:Label ID="lblsuper" runat="server" style="white-space:nowrap"  Font-Size="9pt" Text='<%#Eval("S_Name")%>'  ></asp:Label>
                                              
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>
                                          <asp:Label ID="lblOno"   runat="server" style="white-space:nowrap"  Font-Size="9pt" CssClass="Order_no"  Text='<%#Eval("Order_no")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:Label ID="lblOv"   runat="server"  Text='<%#Eval("Order_Value")%>' CssClass="Order_Value" style="white-space:nowrap"  Font-Size="9pt" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                             
                           <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                               <HeaderTemplate><asp:DropDownList ID="ddlacthead" runat="server" CssClass="ddlhead" BorderColor="Aqua" Enabled="false"  style="width:150px;height:35px;"
                                            >
                                           
                                           <asp:ListItem Text="Select" Value="0"></asp:ListItem>   
                                      <asp:ListItem Text="Confirm" Value="1"></asp:ListItem>                                     
                                     <asp:ListItem Text="Reject" Value="2"></asp:ListItem>                                    
                                     </asp:DropDownList></HeaderTemplate>
                                        <ItemTemplate>
                                          <asp:DropDownList ID="ddlactrow" runat="server" CssClass="ddlact" BorderColor="Aqua"   style="width:150px;height:35px;"
                                              >
                                           
                                           <asp:ListItem Text="Select" Value="0"></asp:ListItem>   
                                      <asp:ListItem Text="Confirm" Value="1"></asp:ListItem>                                     
                                     <asp:ListItem Text="Reject" Value="2"></asp:ListItem>   
                                         <asp:ListItem Text="Transfer" Value="3"></asp:ListItem> 
                                     </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Super Stockist" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>                               
                                              <asp:DropDownList ID="ddlsuper" runat="server" BorderColor="Aqua"  CssClass="form-control "  
                                                  style="width:150px;height:35px;" Enabled="false" >

                                              </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Detail Changes" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                               
                                              
                                        <asp:HyperLink ID="hydetail" Text="Detail" runat="server" NavigateUrl='<%# "PriOrder_Edit_detail_view.aspx?Sf_Name=" + Eval("Sf_Name") + "&Order_no=" + Eval("Order_no") + "&StartDate=" + hdnffdate.Value + "&EndDate=" + hdnttdate.Value + "&div_code=" + div_code + "&sf_code=" + Eval("Sf_Code") + "&Stockist_Name=" + Eval("Stockist_Name")+"&Superstockist=" + Eval("S_Name")+"&Stockist_code=" + Eval("stockist_code")+"&Superstockistcode=" + Eval("Super_Stockist_Code")%>'></asp:HyperLink>

                                            </ItemTemplate>
                             </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

         </asp:GridView>
                                       <br>
                        
     </br>
              <center >
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-md  save" Text="Save"   Visible="false"/>
                  </center>
    </div>
  
    </div>
          

    </form>
</body>
</html>
</asp:Content>

