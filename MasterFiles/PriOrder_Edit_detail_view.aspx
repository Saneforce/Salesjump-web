<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriOrder_Edit_detail_view.aspx.cs" Inherits="MasterFiles_PriOrder_Edit_detail_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <title>Primary Order</title>  
     <script type="text/jscript">        
         $(document).ready(function () {          

             $('.productcode').hide();
             $('.lblsscode').hide();
             $('.lblTrans_POrd_No').hide();
             $('.txtffc1').hide();
            
             $("[id*=txtcqnty]").on("change", function () {
                 // $('input[type = "text"]').blur(function () { 
                 var lblcqnty = $(this).closest("tr").find(".lblcqnty");
                 var txtcqnty = $(this).closest("tr").find(".txtcqnty");
                 var ddlsuper = $(this).closest("tr").find("[id*=ddldist]")[0];
                 var ddlact = $(this).closest("tr").find("[id*=ddlact]")[0];            
                 var grid = $(this).closest("table");
                 $(this, grid).each(function () {
                     if ($(lblcqnty).text() == $(txtcqnty).val()) {
                     
                         $(ddlsuper).prop("disabled", true);
                         $(ddlact).find('option[value="1"]').attr("selected", "selected");
                     }   
                     else if ($(lblcqnty).text() < $(txtcqnty).val()) {
                         $(this).val("");
                      //   alert("Sales Quantity must not greater than Order  Quantity");

                     }
                     else {
                         $(ddlsuper).prop("disabled", false);                         
                       //  $("ddlact").val(tran).prop('selected', true);
                         $(ddlact).find('option[value="3"]').attr("selected", "selected");
                         $(ddlact).prop("disable", true);                  
                     }  
                      
                 });                
             });           
             $("[id*=txtpqnty]").on("change", function () {
                 // $('input[type = "text"]').blur(function () { 
                 var lblpqnty = $(this).closest("tr").find(".lblpqnty");                   
                 var ddlsuper = $(this).closest("tr").find("[id*=ddldist]")[0];
                 var ddlact = $(this).closest("tr").find("[id*=ddlact]")[0]; 
                 var grid = $(this).closest("table");
                 $(this, grid).each(function () {
                     if ($(lblpqnty).val() == $(this).val()) {

                         $(ddlsuper).prop("disabled", true);
                         $(ddlact).find('option[value="1"]').attr("selected", "selected");
                        
                     }                    
                     else {

                         $(ddlsuper).prop("disabled", false);
                         $(ddlact).find('option[value="3"]').attr("selected", "selected");
                         $(ddlact).prop("disable", true);  
                     }
                 });
             }); 
             $('#btnSubmit').on('click', function () {
                 var grid = $('.newStly').closest("table");
                 var row = $(grid).find('tbody').find("tr");                 
                 if (row.length > 0) {
                     var i = 1;
                     $data = "[";                   
                     $(row, grid).each(function () {
                         var sfcode =$('#<%=txtffc.ClientID%>').val();
                          var ddlrow = $(this).find("[id*=ddlact]");
                         var ddlselected = $(ddlrow).children("option:selected").val();
                         var ddl = $(this).closest('tr').find("[id*=ddldist]")[0];
                         var txtSuperStock1 = $(ddl).children("option:selected").val();
                         var txtSuperStock = $('#lblsscode').text();
                         var lblTrans_POrd_No = $(this).find(".lblTrans_POrd_No").text();
                         if (txtSuperStock1 != txtSuperStock) {
                             var txtSuperStockist = txtSuperStock1;
                         }
                         else {
                             var txtSuperStockist = txtSuperStock;
                         }
                         if (ddlselected == 1) {  
                            // var txtSuperStockist = $('#lblsscode').text();
                             var txtorderno = $('#txtorderno').val();                            
                             var productcode = $(this).find(".productcode").text();
                             var productname = $(this).find(".productname").text();
                             var lblcqnty = $(this).find(".lblcqnty").text();
                             var txtcqnty = $(this).find(".txtcqnty").val();
                             var lblpqnty = $(this).find(".lblpqnty").text();
                             var txtpqnty = $(this).find(".txtpqnty").val();
                             var lblRate = $(this).find(".lblRate").text();
                             var lblValue = $(this).find(".lblValue").text();
                             var ordervalue = $(this).find(".lblValue").text();
                             var orderflag = 1;                               
                             if ($data != "[") $data += ",";
                           
                             $data += '{"orderflag":"' + orderflag + '","orderno":"' + txtorderno + '","ordervalue": "' + ordervalue + '","productcode":"' + productcode + '","productname":"' + productname + '","cqty":"' + lblcqnty + '","pQty":"' + lblpqnty + '","rate":"' + lblRate + '","values":"' + lblValue + '","casecnf":"' + txtcqnty + '","Pcscnf":"' + txtpqnty + '","superstockist":"' + txtSuperStockist + '","Trans_POrd_No":"' + lblTrans_POrd_No + '","newOrderno":"' + txtorderno + '","sf_code":"' + sfcode+'"}';
                         }
                         if (ddlselected == 2) {
                           //  var txtff = $('#txtff').val();
                             var txtorderno= $('#txtorderno').val();
                           //  var txtdis = $('#txtdis').val();
                            // var txtSuperStockist = $('#lblsscode').text();
                             var productcode = $(this).find(".productcode").text();
                             var productname = $(this).find(".productname").text();
                             var lblcqnty = $(this).find(".lblcqnty").text();
                             var txtcqnty = $(this).find(".txtcqnty").val();
                             var lblpqnty = $(this).find(".lblpqnty").text();
                             var txtpqnty = $(this).find(".txtpqnty").val();
                             var lblRate = $(this).find(".lblRate").text();
                             var lblValue = $(this).find(".lblValue").text();
                             var ordervalue = $(this).find(".lblValue").text();
                             var orderflag = 2;                            
                             if ($data != "[") $data += ",";
                            
                             $data += '{"orderflag":"' + orderflag + '","orderno":"' + txtorderno + '","ordervalue": "' + ordervalue + '","productcode":"' + productcode + '","productname":"' + productname + '","cqty":"' + lblcqnty + '","pQty":"' + lblpqnty + '","rate":"' + lblRate + '","values":"' + lblValue + '","casecnf":"' + txtcqnty + '","Pcscnf":"' + txtpqnty + '","superstockist":"' + txtSuperStockist + '","Trans_POrd_No":"' + lblTrans_POrd_No + '","newOrderno":"' + txtorderno + '","sf_code":"' + sfcode+'"}';
                         }
                         if (ddlselected == 3) {                               
                             
                             var productcode = $(this).find(".productcode").text();
                             var productname = $(this).find(".productname").text();               
                             
                             var txtorderno = $('#txtorderno').val();                                 
                                 var lblcqnty = $(this).find(".lblcqnty").text();
                                 var txtcqnty = $(this).find(".txtcqnty").val();
                                 var lblpqnty = $(this).find(".lblpqnty").text();
                                 var txtpqnty = $(this).find(".txtpqnty").val();                      
                                 var lblRate = $(this).find(".lblRate").text();
                             var lblValue = $(this).find(".lblValue").text();
                             var ordervalue = $(this).find(".lblValue").text();                        
                            if ((lblcqnty > txtcqnty) || (lblpqnty > txtpqnty)) {
                              //   var lblccqnty = lblcqnty - txtcqnty;
                                // var lblppqnty = lblpqnty - txtpqnty;
                                 var txtorder = txtorderno + '-' + i;
                                 //var orderflag = 0;
                              //   if ($data != "[") $data += ",";
                                // $data += '{"orderflag":"' + orderflag + '","orderno":"' + txtorderno + '","ordervalue": "' + ordervalue + '","productcode":"' + productcode + '","productname":"' + productname + '","cqty":"' + lblcqnty + '","pQty":"' + lblpqnty + '","rate":"' + lblRate + '","values":"' + lblValue + '","casecnf":"' + lblccqnty + '","Pcscnf":"' + lblppqnty + '","superstockist":"' + txtSuperStockist + '","Trans_POrd_No":"' + lblTrans_POrd_No + '"}';
                           //}   
                             //if ((lblcqnty > txtcqnty) || (lblpqnty > txtpqnty)) {
                               //  var txtorderno = txtorder;
                                 var orderflag = 3;
                                 if ($data != "[") $data += ",";
                                // $data += '{"orderflag":"' + orderflag + '","orderno":"' + txtorderno + '","sfcode":"' + txtff + '","Stockist":"' + txtdis + '","superstockist": "' + txtSuperStockist + '","ordervalue": "' + ordervalue + '","productcode":"' + productcode + '","productname":"' + productname + '","cqty":"' + lblcqnty + '","pQty":"' + lblpqnty + '","rate":"' + lblRate + '","values":"' + lblValue + '","casecnf":"' + txtcqnty + '","Pcscnf":"' + txtpqnty + '"}';
                                $data += '{"orderflag":"' + orderflag + '","orderno":"' + txtorderno + '","ordervalue": "' + ordervalue + '","productcode":"' + productcode + '","productname":"' + productname + '","cqty":"' + lblcqnty + '","pQty":"' + lblpqnty + '","rate":"' + lblRate + '","values":"' + lblValue + '","casecnf":"' + txtcqnty + '","Pcscnf":"' + txtpqnty + '","superstockist":"' + txtSuperStockist + '","Trans_POrd_No":"' + lblTrans_POrd_No + '","newOrderno":"' + txtorder + '","sf_code":"' + sfcode+'"}';
                             }
                         }
                         //i++;                         
                         });
                     $data += ']';
                     Xhsrdata = JSON.parse($data)
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         url : "priOrder_edit_detail_view.aspx/updatepriorderdetails",
                         data: "{'primary':'" + $data + "'}",
                         datatype: "json",
                         sucess: function (data) {
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
                          <asp:TextBox ID="txtffc" runat="server"  CssClass="txtffc1" ></asp:TextBox></td><td></td><td></td><td></td>
                       <td><asp:Label ID="lbloderno" Text="Order No" runat="server" ></asp:Label></td>
                       <td> <asp:TextBox ID="txtorderno" runat="server" Enabled="false"></asp:TextBox></td><td></td><td></td>
                       <td><asp:Label ID="lbldis" runat="server" Text="Distributer"  ></asp:Label></td>
                       <td><asp:TextBox runat="server" ID="txtdis" Enabled="false"></asp:TextBox></td><td></td><td></td>
                          <td><asp:Label ID="lblsuper" runat="server" Text="SuperStockist"  ></asp:Label></td>
                       <td><asp:TextBox runat="server" ID="txtSuperStockist" Enabled="false"></asp:TextBox>
                             <td><asp:Label ID="lblsscode" runat="server"  CssClass="lblsscode" ></asp:Label></td>
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
                                            <asp:Label ID="lblTrans_POrd_No"  style="white-space:nowrap" CssClass="lblTrans_POrd_No" runat="server" Font-Size="9pt" Text='<%#Eval("Trans_POrd_No")%>'></asp:Label>
                                               <asp:Label ID="Lblprocode" style="white-space:nowrap" CssClass="productcode" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Code")%>'></asp:Label>
                                            <asp:Label ID="lblProduct" style="white-space:nowrap" runat="server" CssClass="productname" Font-Size="9pt" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Quantity" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:Label ID="lblcqnty" runat="server" style="white-space:nowrap" CssClass="lblcqnty"  Text='<%#Eval("CQty")%>'  ></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Quantity" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="txtcqnty"   runat="server" style="white-space:nowrap" CssClass="txtcqnty"   Text='<%#Eval("CQty")%>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Piece Quantity" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                        <asp:Label ID="lblpqnty" runat="server" style="white-space:nowrap" CssClass="lblpqnty"  Text='<%#Eval("PQty")%>'  ></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Piece Quantity" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="txtpqnty"   runat="server" style="white-space:nowrap"  CssClass="txtpqnty"  Text='<%#Eval("PQty")%>'  ></asp:TextBox>
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
                                              <asp:DropDownList ID="ddldist" runat="server" BorderColor="Aqua" class="form-control"  style="width:190px;height:35px;" 
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
                         <asp:Button ID="btnclose" runat="server" CssClass="btn btn-primary btn-md" PostBackUrl="rpt_Primary_Order.aspx" Text="close" style="height:30px;width:50px;" ></asp:Button></a>
                         </center>
      </div>
    
              


    </div>
    </form>
</body>
        </html>
