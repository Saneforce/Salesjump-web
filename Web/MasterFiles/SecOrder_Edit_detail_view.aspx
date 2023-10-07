<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecOrder_Edit_detail_view.aspx.cs" Inherits="MasterFiles_SecOrder_Edit_detail_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <title>Secondary Order</title> 
      
     <script type="text/jscript">        
         $(document).ready(function () {        

             $('.productcode').hide();
             $('.lblretailcode').hide();
             $('.lblTrans_POrd_No').hide();
             $('.txtffc1').hide();
             $('.lblrecode').hide();
             $('.txttransslno').hide();
             $('.Order_Date').hide();            
            
             $("[id*=txtqnty]").on("change", function () {                
                 var lblqnty = $(this).closest("tr").find(".lblqnty");
                 var txtqnty = $(this).closest("tr").find(".txtqnty");
                 var txtfree = $(this).closest("tr").find(".txtfree");
                 var lblValue = $(this).closest("tr").find(".lblValue").text();              
                 var txtdiscount = $(this).closest("tr").find(".txtdiscount");                
                 var txtdisprice = $(this).closest("tr").find(".txtdisprice");
                 var ddlstockist = $(this).closest("tr").find("[id*=ddlstockist]")[0];
                 var ddlact = $(this).closest("tr").find("[id*=ddlact]")[0];            
                 var grid = $(this).closest("table");
                 var productcode = $(this).closest("tr").find(".productcode").text();
                 var Order_Date = $(this).closest("tr").find(".Order_Date").text();
                 var orderdate = Order_Date.split(' ');
                 var splitdate = orderdate[0].split('/');
                 var O_date = (splitdate[2] + '/' + splitdate[1] + '/' + splitdate[0]);
                 var lblrecode = $('#<%=lbldiscode.ClientID%>').text();
                 var qnty =( ($(lblqnty).text()) - ($(txtqnty).val()));
                 $(this, grid).each(function () {                    
                     if ($(lblqnty).text() > $(txtqnty).val()) {
                         $(ddlstockist).prop("disabled", false);
                         //  $("ddlact").val(tran).prop('selected', true);
                         $(ddlact).find('option[value="3"]').attr("selected", "selected");
                         $(ddlact).prop("disable", true);     
                     }   
                 
                     else {
                         $(ddlstockist).prop("disabled", true);
                         $(ddlact).find('option[value="1"]').attr("selected", "selected");       
                     }                      
                 
                   
                 $.ajax({
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     async: false,
                     url: "SecOrder_Edit_detail_view.aspx/schemedetail",                 
                     data: "{'dis':'" + lblrecode + "','prod':'" + productcode + "','date':'" + O_date + "','qnty':'" + qnty +"'}",
                     datatype: "json",
                     success: function (data) {
                         var datad = data.d;
                         if ((datad.length)> 0) {
                               $(txtfree).val(datad[0].Free);
                             $(txtdiscount).val(datad[0].Discount);
                             var totaldisprice = (lblValue * ((datad[0].Discount) / 100));
                             $(txtdisprice.text(totaldisprice))
                         }
                     },
                     error: function (rs) {
                         alert(rs);
                     }
                 });            
                 }); 
             });    
             $("[id*=txtDiscount]").on("change", function () {
                 var txtdiscount = $(this).val();
                 var txtvalue = $(this).closest("tr").find(".lblValue").text();
                 var txtdisprice = $(this).closest("tr").find(".txtdisprice");
                 var totaldisprice = (txtvalue * (txtdiscount / 100));
                 $(this).closest("tr").find(".txtdisprice").text(totaldisprice);
             });

             $("[id*=txtfree]").on("change", function () {
                 // $('input[type = "text"]').blur(function () { 
                 var lblfree = $(this).closest("tr").find(".lblfree");                   
                 var ddlstockist = $(this).closest("tr").find("[id*=ddlstockist]")[0];
                 var ddlact = $(this).closest("tr").find("[id*=ddlact]")[0]; 
                 var grid = $(this).closest("table");
                 $(this, grid).each(function () {
                     if ($(lblfree).text() > $(this).val()) {
                         $(ddlstockist).prop("disabled", false);
                         $(ddlact).find('option[value="3"]').attr("selected", "selected");
                         $(ddlact).prop("disable", true);                       
                        
                     }
                    
                     else {
                         $(ddlstockist).prop("disabled", true);
                         $(ddlact).find('option[value="1"]').attr("selected", "selected");
                     }
                 });
             }); 
             $('#btnSubmit').on('click', function () {
                 var grid = $('.newStly').closest("table");
                 var row = $(grid).find('tbody').find("tr");                 
                 if (row.length > 0) {
                     var i = 1;
                     $data = "[";                   
                     $(row,grid).each(function () {
                         var ddlrow = $(this).find("[id*=ddlact]");
                         var txttransslno = $(this).find(".lblTrans_Sl_No").text(); 
                         var ddlselected = $(ddlrow).children("option:selected").val();
                         var ddl = $(this).find("[id*=ddlstockist]");
                         var ddlstockist = $(ddl).children("option:selected").val();                         
                         var lblrecode = $('#<%=lbldiscode.ClientID%>').text();
                         var lblTrans_POrd_No = $(this).find(".lblTrans_POrd_No").text();                      
                         if (ddlstockist != lblrecode) {
                             var txtSuperStockist = ddlstockist;
                         }
                         else {
                             var txtSuperStockist = lblrecode;
                         }
                         if (ddlselected == 1) {                                                        
                             var productcode = $(this).find(".productcode").text();                          
                             var lblqnty = $(this).find(".lblqnty").text();
                             var txtqnty = $(this).find(".txtqnty").val();
                             var lblfree = $(this).find(".lblfree").text();
                             var txtfree = $(this).find(".txtfree").val();
                             var lblRate = $(this).find(".lblRate").text();     
                             var txtdiscount = $(this).find(".txtdiscount").val();
                             var txtdisprice = $(this).find(".txtdisprice").text(); 
                             var orderflag = 1;                               
                             if ($data != "[") $data += ",";
                           
                             $data += '{"orderflag":"' + orderflag + '","Transslno":"' + txttransslno + '","productcode": "' + productcode + '","qty":"' + lblqnty + '","qtycnf":"' + txtqnty + '","free":"' + lblfree + '","rate":"' + lblRate + '","freecnf":"' + txtfree + '","trans_order_no":"' + lblTrans_POrd_No + '","neworderno":"' + txttransslno + '","stockistcode":"' + txtSuperStockist + '","txtdiscount":"' + txtdiscount+ '","txtdisprice":"' + txtdisprice+'"}';
                         }
                         if (ddlselected == 2) {                         
                                                   
                             var productcode = $(this).find(".productcode").text();                          
                             var lblqnty = $(this).find(".lblqnty").text();
                             var txtqnty = $(this).find(".txtqnty").val();
                             var lblfree = $(this).find(".lblfree").text();
                             var txtfree = $(this).find(".txtfree").val();
                             var lblRate = $(this).find(".lblRate").text();    
                             var txtdiscount = $(this).find(".txtdiscount").val();
                             var txtdisprice = $(this).find(".txtdisprice").text(); 
                             var orderflag = 2;                            
                             if ($data != "[") $data += ",";
                            
                             $data += '{"orderflag":"' + orderflag + '","Transslno":"' + txttransslno + '","productcode": "' + productcode + '","qty":"' + lblqnty + '","qtycnf":"' + txtqnty + '","free":"' + lblfree + '","rate":"' + lblRate + '","freecnf":"' + txtfree + '","trans_order_no":"' + lblTrans_POrd_No + '","neworderno":"' + txttransslno + '","stockistcode":"' + txtSuperStockist + '","txtdiscount":"' + txtdiscount + '","txtdisprice":"' + txtdisprice +'"}';
                         }
                         if (ddlselected == 3) {                             
                             
                             var productcode = $(this).find(".productcode").text();                                                          
                                 var lblqnty = $(this).find(".lblqnty").text();
                                 var txtqnty = $(this).find(".txtqnty").val();
                                var lblfree = $(this).find(".lblfree").text();
                                 var txtfree = $(this).find(".txtfree").val();                      
                             var lblRate = $(this).find(".lblRate").text();
                             var txtdiscount = $(this).find(".txtdiscount").val();
                             var txtdisprice = $(this).find(".txtdisprice").text(); 
                             if ((lblqnty > txtqnty) || (lblfree > txtfree)) {                              
                                      var neworderno = txttransslno + '-' + i;                               
                                 var orderflag = 3;
                                 if ($data != "[") $data += ",";
                                 // $data += '{"orderflag":"' + orderflag + '","orderno":"' + txtorderno + '","sfcode":"' + txtff + '","Stockist":"' + txtdis + '","superstockist": "' + txtSuperStockist + '","ordervalue": "' + ordervalue + '","productcode":"' + productcode + '","productname":"' + productname + '","cqty":"' + lblcqnty + '","pQty":"' + lblpqnty + '","rate":"' + lblRate + '","values":"' + lblValue + '","casecnf":"' + txtcqnty + '","Pcscnf":"' + txtpqnty + '"}';
                                 $data += '{"orderflag":"' + orderflag + '","Transslno":"' + txttransslno + '","productcode": "' + productcode + '","qty":"' + lblqnty + '","qtycnf":"' + txtqnty + '","free":"' + lblfree + '","rate":"' + lblRate + '","freecnf":"' + txtfree + '","trans_order_no":"' + lblTrans_POrd_No + '","neworderno":"' + neworderno + '","stockistcode":"' + txtSuperStockist + '","txtdiscount":"' + txtdiscount + '","txtdisprice":"' + txtdisprice +'"}';
                             }
                         }                                          
                         });
                     $data += ']';
                     Xhsrdata = JSON.parse($data)                   
                    
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         url: "SecOrder_Edit_detail_view.aspx/updatesecorderdetails",
                         data: "{'sec':'" + $data + "'}",
                         datatype: "json",
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

           
          
           <br />
               <table>
                  <tr><td><asp:Label ID="lblff" Text="Field Force" runat="server"></asp:Label></td>
                       <td> <asp:TextBox ID="txtff" runat="server" Enabled="false" CssClass="txtff"></asp:TextBox></td><td>
                          <asp:TextBox ID="txtffc" runat="server"  CssClass="txtffc1" Enabled ="false" ></asp:TextBox></td><td></td><td></td><td></td>                       
                       <td> <asp:TextBox ID="txttransslno" runat="server" CssClass="txttransslno" Enabled="false"></asp:TextBox></td><td></td><td></td>
                       <td><asp:Label ID="lbldis" runat="server" Text="Distributer"  ></asp:Label></td> 
                      <td><asp:Label ID="lbldiscode" runat="server"  CssClass="lblrecode" ></asp:Label></td>
                       <td><asp:TextBox runat="server" ID="txtdis" Enabled="false"></asp:TextBox></td><td></td><td></td>
                          <td><asp:Label ID="lblsuper" runat="server" Text="Retailer"  ></asp:Label></td>
                       <td><asp:TextBox runat="server" ID="txtretailer" Enabled="false"></asp:TextBox>
                           
<asp:HiddenField ID="hdnfdate" runat="server" />
                           <asp:HiddenField ID="hdntdate" runat="server" />


                       </td>
                   </tr>
               </table>
               
                 <div>  
            
          <asp:GridView ID="GVData" runat="server" GridLines="None" AutoGenerateColumns="false"  CssClass="newStly" ForeColor="Black"  Width="78%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" HeaderStyle-Height="40px" OnRowDataBound="GVData_OnRowDataBound">
                    <Columns>
                        
                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>                                           
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product" ItemStyle-Width="180" ItemStyle-Height="35"     HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="NotSet" >
                                        <ItemTemplate>
                                              <asp:Label ID="lblOrderDate"  style="white-space:nowrap" CssClass="Order_Date" runat="server" Font-Size="9pt" Text='<%#Eval("Order_Date")%>'></asp:Label>
                                            <asp:Label ID="lblTrans_Order_No"  style="white-space:nowrap" CssClass="lblTrans_POrd_No" runat="server" Font-Size="9pt" Text='<%#Eval("Trans_Order_No")%>'></asp:Label>
                                             <asp:Label ID="lblTrans_Sl_No"  style="white-space:nowrap" CssClass="lblTrans_Sl_No" runat="server" Font-Size="9pt" Text='<%#Eval("Trans_Sl_No")%>'></asp:Label>
                                               <asp:Label ID="Lblprocode" style="white-space:nowrap" CssClass="productcode" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Code")%>'></asp:Label>
                                            <asp:Label ID="lblProduct" style="white-space:nowrap" runat="server" CssClass="productname" Font-Size="9pt" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Quantity" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:Label ID="lblqnty" runat="server" style="white-space:nowrap" CssClass="lblqnty"  Text='<%#Eval("Quantity")%>'  ></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Quantity" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="txtqnty"   runat="server" style="white-space:nowrap" CssClass="txtqnty"   Text='<%#Eval("Quantity")%>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Free" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:Label ID="lblfree" runat="server" style="white-space:nowrap" CssClass="lblfree"  Text='<%#Eval("free")%>'  ></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offer Free" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="txtfree"   runat="server" style="white-space:nowrap"  CssClass="txtfree"  Text='<%#Eval("free")%>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Rate" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:Label ID="lblRate"   runat="server" style="white-space:nowrap"  CssClass="lblRate"   Text='<%#Eval("Rate")%>'  ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Value" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:Label ID="lblValue"   runat="server" style="white-space:nowrap" CssClass="lblValue"    Text='<%#Eval("value")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                         <asp:TemplateField HeaderText="Discount" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:Label ID="lblDiscount"   runat="server" style="white-space:nowrap" CssClass="lbldiscount"    Text='<%#Eval("discount")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                           <asp:TemplateField HeaderText="Discount Offer" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="txtDiscount"   runat="server" style="white-space:nowrap" CssClass="txtdiscount"    Text='<%#Eval("discount")%>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Discount Price" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:Label ID="txtDisprice"   runat="server" style="white-space:nowrap" CssClass="txtdisprice"    Text='<%#Eval("discount_price")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                           <asp:TemplateField HeaderText="Action" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:DropDownList ID="ddlact" runat="server" BorderColor="Aqua" style="width:150px;height:30px;" 
                                             >                                             
                                      
                                    <asp:ListItem Text="Confirm" Value="1"></asp:ListItem>                                    
                                     <asp:ListItem Text="Reject" Value="2"></asp:ListItem>                                    
                                     <asp:ListItem Text="Transfer" Value="3"></asp:ListItem>
                                     </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Detail Changes" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                              <asp:DropDownList ID="ddlstockist" runat="server" BorderColor="Aqua" class="form-control"  style="width:190px;height:35px;" 
                                            Enabled="false"></asp:DropDownList>                                            
                                         
                                            </ItemTemplate>
                             </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

         </asp:GridView>  
          </br>
                     <center>
             
                          <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-md" Text="Save" style="height:30px;width:50px;"   />
                         <a>
                         <asp:Button ID="btnclose" runat="server" CssClass="btn btn-primary btn-md" PostBackUrl="Rpt_Secondary_Order_.aspx" Text="close" style="height:30px;width:50px;" ></asp:Button></a>
                         </center>
      </div>
    
              


    </div>
    </form>
</body> 
</html>
