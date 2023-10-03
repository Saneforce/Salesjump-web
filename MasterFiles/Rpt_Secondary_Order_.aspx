<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="Rpt_Secondary_Order_.aspx.cs" Inherits="MasterFiles_Rpt_Secondary_Order_" %>

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

    <script type="text/javascript">  
           
    $(document).ready(function () {
        var tdate = new Date();
                 var Tmonth = tdate.getMonth() + 1;
                 var Tday = tdate.getDate();
                 var year = tdate.getFullYear();
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
            dateformat: 'dd-mm-yy', maxDate: new Date(),
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
        $('.stockist').hide();
        $('.lblrecode').hide();
        $('.transslno').hide();
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
                var ddl = $(this).closest('tr').find('.ddlstockist');
                var retcode = $(this).closest('tr').find('.stockist').text();
                var retaname = $(this).closest('tr').find('.stockistname').text();
                var sfcode = $(this).closest('tr').find('.sfcode').text();
              //  $('.retailercode').empty().append('<option selected="selected" value="' + retcode + '">"' + retaname + '"</option>'); 
                if (ddlselected == 3) {
                    $(ddl).prop("disabled", false);

                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        async: false,
                        url: "Rpt_Secondary_Order_.aspx/getretail",
                        data: "{'sfcode':'" + sfcode + "'}",
                        datatype: "json",
                        success: function (data) {
                            var i = 0;
                            var data1=data.d;
                            //var ddlstockist = $(this).closest('tr').find("[id*=ddlstockist]");
                            if ((data.d.length) > 0) {                                
                                $.each(data.d, function () { 
                                    ddl.append($("<option></option>").val(data1[i].retailcode).html(data1[i].retailname));  
                                    i++;
                                });
                               
                            }
                            else {
                                ddl.append('<option  selected="selected" value="' + retcode + '">"' + retaname + '"</option>');
                                 
                            }
                        },
                        error: function (rs) {
                            alert(JSON.stringify(rs));

                        }
                    });
                }
            });
        });

        function onload() {
            var grid = $('.newStly').closest("table");
            var row = $(grid).find('tbody').find("tr");
            if (row.length > 0) {
                $(row, grid).each(function () {                    
                    var ddl = $(this).find("[id*=ddlstockist]");
                  //  var ddlselected = $(ddl).children("option:selected").val();
                    //if (ddlselected == "") {
                        var retcode = $(this).find('.stockist').text();
                        var retaname = $(this).find('.stockistname').text();
                        ddl.append('<option  selected="selected" value="' + retcode + '">"' + retaname + '"</option>');
                   // }
                });
            }
        }
        onload();
        $(document).on('click', '.save', function () {
            // if (confirm("Are you sure do you want to upload?")) {                        
            var grid = $('.newStly').closest("table");
            var row = $(grid).find('tbody').find("tr");
            if (row.length > 0) {
                $data = "[";
                $(row, grid).each(function () {
                    var ddlrow = $(this).find("[id*=ddlactrow]");                  
                    var ddlselected = $(ddlrow).children("option:selected").val();               
                 
                                   
                    if (ddlselected == 1) { 
                        var Order_flag = 1;
                        var stockistcode = $(this).find(".stockist").text();
                        var transslno = $(this).find(".transslno").text();                      
                        var ddlstockist = $(this).find("[id*=ddlstockist]");
                        var ddlstockistselect = $(ddlstockist).children("option:selected").val();
                        if (ddlstockistselect == stockistcode) {
                            var stockist = stockistcode;
                        }
                        else {
                            var stockist = ddlstockistselect;
                        }     
                        if ($data != "[") $data += ",";
                        // $data = '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + s_no + '","order_value":"' + Order_Value + '"}'
                        $data += '{"order_flag":"' + Order_flag + '","stockistcode":"' + stockist + '","trans_sl_no":"' + transslno + '"}'

                    }
                    if (ddlselected == 2) {                       
                        var Order_flag = 2;
                        var stockistcode = $(this).find(".stockist").text();
                        var transslno = $(this).find(".transslno").text();
                        var ddlstockist = $(this).find("[id*=ddlstockist]");
                        var ddlstockistselect = $(ddlstockist).children("option:selected").val();
                        if (ddlstockistselect == stockistcode) {
                            var stockist = stockistcode;
                        }
                        else {
                            var stockist = ddlstockistselect;
                        }   
                        if ($data != "[") $data += ",";
                        // $data = '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + s_no + '","order_value":"' + Order_Value + '"}'
                        $data += '{"order_flag":"' + Order_flag + '","stockistcode":"' + stockist + '","trans_sl_no":"' + transslno + '"}'
                    }
                    if (ddlselected == 3) {
                      
                        var Order_flag = 3;
                        var stockistcode = $(this).find(".stockist").text();
                        var transslno = $(this).find(".transslno").text();
                        var ddlstockist = $(this).find("[id*=ddlstockist]");
                        var ddlstockistselect = $(ddlstockist).children("option:selected").val();
                        if (ddlstockistselect == stockistcode) {
                            var stockist = stockistcode;
                        }
                        else {
                            var stockist = ddlstockistselect;
                        }   
                        if ($data != "[") $data += ",";
                        // $data = '{"order_flag":"' + Order_flag + '","order_no":"' + Order_no + '","sf_code":"' + sfcode + '","stockist":"' + stockist + '","S_No":"' + s_no + '","order_value":"' + Order_Value + '"}'
                        $data += '{"order_flag":"' + Order_flag + '","stockistcode":"' + stockist + '","trans_sl_no":"' + transslno + '"}'
                    }
                });
                $data += ']';
                Xhsrdata = JSON.parse($data)
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_Secondary_Order_.aspx/updateSecorder",
                    data: "{'secorder':'" + $data + "'}",
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
                             BorderStyle="Solid" HeaderStyle-Height="40px" EnableViewState="true">
 
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
                                            <asp:Label ID="transslno" style="white-space:nowrap" runat="server" Font-Size="9pt"  CssClass="transslno" Text='<%#Eval("Trans_Sl_No")%>'></asp:Label>
                                            <asp:Label ID="scode" style="white-space:nowrap" runat="server" Font-Size="9pt"  CssClass="sfcode" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributer" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>
                                             <asp:Label ID="lbldistr" runat="server" style="white-space:nowrap"  Font-Size="9pt" CssClass="stockist" Text='<%#Eval("stockist_code")%>'  ></asp:Label>
                                        <asp:Label ID="lblDistri" runat="server" style="white-space:nowrap" CssClass="stockistname" Font-Size="9pt" Text='<%#Eval("Stockist_Name")%>'  ></asp:Label>
                                              
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>
                                              <asp:Label ID="lblretailercode" runat="server" style="white-space:nowrap"  CssClass="lblrecode" Font-Size  ="9pt" Text='<%#Eval("Cust_Code")%>'  ></asp:Label>
                                        <asp:Label ID="lblretailer" runat="server" style="white-space:nowrap" CssClass="txtrename" Font-Size="9pt" Text='<%#Eval("ListedDr_Name")%>'  ></asp:Label>                                              
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
                                      <asp:ListItem Text="Confirm" Value="4"></asp:ListItem>        
                                      <asp:ListItem Text="Delivered" Value="1"></asp:ListItem>                                    
                                     <asp:ListItem Text="Reject" Value="2"></asp:ListItem>   
                                         <asp:ListItem Text="Transfer" Value="3"></asp:ListItem> 
                                     </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Distributer" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet">
                                        <ItemTemplate>                               
                                              <asp:DropDownList ID="ddlstockist" runat="server" BorderColor="Aqua"  CssClass="form-control ddlstockist "  
                                                  style="width:150px;height:35px;" Enabled="false"  >
                                                 
                                              </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Detail Changes" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                               
                                              
                                        <asp:HyperLink ID="hydetail" Text="Detail" runat="server"  NavigateUrl='<%# "SecOrder_Edit_detail_view.aspx?Sf_Name=" + Eval("Sf_Name") + "&Transslno=" + Eval("Trans_Sl_No") + "&StartDate=" + hdnffdate.Value + "&EndDate=" + hdnttdate.Value + "&div_code=" + div_code + "&sf_code=" + Eval("Sf_Code") + "&Stockist_Name=" + Eval("Stockist_Name")+"&RetailerName=" + Eval("ListedDr_Name")+"&Stockist_code=" + Eval("stockist_code")+"&Retailercode=" + Eval("Cust_Code")%>'></asp:HyperLink> 

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

