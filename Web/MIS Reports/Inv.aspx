<%@ Page Title="Invoicing" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Inv.aspx.cs" Inherits="MIS_Reports_Inv" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
 <script src="../js/jquery.min.js" type="text/javascript"></script>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="../css/chosen.css" rel="stylesheet" type="text/css" />
 <link href = "https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css"     rel = "stylesheet">
     
      <script src = "https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
           <script type="text/javascript">
function myFunction() {
    window.print();
}
</script>

         <script type="text/javascript">
             $(document).ready(function () {

                 $('input:text:first').focus();


                 $('input:text').bind('keydown', function (e) {

                     if (e.keyCode == 13) {

                         e.preventDefault();

                         var nextIndex = $('input:text').index(this) + 1;

                         var maxIndex = $('input:text').length;

                         if (nextIndex < maxIndex) {
                             var ni = 2;
                             var afterindex = nextIndex - ni;
                             var beforeindex = nextIndex + ni;

                             $('input:text:eq(' + nextIndex + ')').focus();
                             $('input:text:eq(' + nextIndex + ')').parent().parent().addClass('hover_row');
                             $('input:text:eq(' + afterindex + ')').parent().parent().removeClass('hover_row');
                             $('input:text:eq(' + beforeindex + ')').parent().parent().removeClass('hover_row');

                         }
                     }
                 });
             });          

           
 
</script>

      <script type="text/javascript">
          var sum = 0;
          var selectedValue ;
          function FetchData(button) {
              
              var cell = button.parentNode.parentNode;
              var row = button.parentNode.parentNode;

              var label1 = GetChildControl(row, "txtquantity").value;
              var piecequan = GetChildControl(row, "txtpiece").value;

              var label = GetChildControl(cell, "productrate").value;
              var piecerate = GetChildControl(cell, "prtratepice").value;




              if (label1 != "" || piecequan != "" || piecequan == "" || label1 == "") {
                  if (label1 == "") {
                      label1='0';
                  }
                  if (piecequan == "") {
                      piecequan = '0';
                  }
                  if (label1 == "" && piecequan == "") {
                      label1 = '0';
                      piecequan = '0';
                  }

                  var label2 = GetChildControl(cell, "ffvalue").value;
                  if (label2 == "") {
                      var Multi = ((parseFloat(label) * parseFloat(label1)) + (parseFloat(piecequan) * parseFloat(piecerate)));

                      GetChildControl(row, "ffvalue").value = Multi;

                      if (!isNaN(Multi) && Multi.length != 0) {
                          sum += parseFloat(Multi);

                      }
                  }
                  else {
                      if (!isNaN(label2) && label2.length != 0) {
                          sum -= parseFloat(label2);
                      }
                      var Multi = ((parseFloat(label) * parseFloat(label1)) + (parseFloat(piecequan) * parseFloat(piecerate)));

                      GetChildControl(row, "ffvalue").value = Multi;
                      if (!isNaN(Multi) && Multi.length != 0) {
                          sum += parseFloat(Multi);
                      }

                  }
              }






              document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = sum;
              var ff = (sum * 0.09);
              document.getElementById("ctl00_ContentPlaceHolder1_TextBox3").value = ff;
              document.getElementById("ctl00_ContentPlaceHolder1_TextBox4").value = ff;
              var tots = (sum + ff + ff);
              document.getElementById("ctl00_ContentPlaceHolder1_Finaltot").value = tots;

              return false;


          };

          function fetc(button) {

              //           alert(obj.id);
              // This is the ID of the textbox clicked on
              var row = button.parentNode.parentNode;
              var label1 = GetChildControl(row, "fsum").value;
              label1 = sum;

          }
          function GetChildControl(element, id) {
              var child_elements = element.getElementsByTagName("*");
              for (var i = 0; i < child_elements.length; i++) {
                  if (child_elements[i].id.indexOf(id) != -1) {
                      return child_elements[i];
                      //                   alert(child_elements[i]);
                  }


              }


          };


          function CheckToochSelection(a) {
              //           alert(sum);
              var fv = sum;

              var all = new Array();

              //           alert(a);
              if (a > 8) {
                  i = a + 1;
                  var t = "Completed";
                  //        var u=   $('#DataList1 TR:nth-child(3)  input[id$=fsum]');

                  var f = parseInt(document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_fsum").value);
                  if (isNaN(f)) {
                      f = 0;
                      if (!isNaN(sum) && sum.length != 0) {
                          f += parseFloat(sum);
                      }
                  }
                  else {
                      if (!isNaN(sum) && sum.length != 0) {
                          f += parseFloat(sum);
                      }
                  }
                  document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_fsum").value = f;
                  document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_Label4").value = t;
                  //               $(this).css({ 'color': 'red' }); 
                  var id = document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_Label4");
                  id.style.backgroundColor = "yellow";





                  sum = 0;

              }
              else {
                  var tt = "Completed";
                  i = a + 1;
                  var ff = parseInt(document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_fsum").value);
                  if (isNaN(ff)) {
                      ff = 0;
                      if (!isNaN(sum) && sum.length != 0) {
                          ff += parseFloat(sum);
                      }
                  }
                  else {
                      if (!isNaN(sum) && sum.length != 0) {
                          ff += parseFloat(sum);
                      }
                  }
                  document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_fsum").value = ff;
                  document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_Label4").value = tt;
                  var idd = document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_Label4");
                  idd.style.backgroundColor = "yellow";

                  sum = 0;
              }




          }

           
          
  </script>

  <style type="text/css">
   .tb8 {
	width: 230px;
	height:20px;
		border: 1px solid rgba(0, 192, 239, 0.39);
		border-left: 4px solid rgba(0, 192, 239, 0.39);
}  
.hdr
{
   padding-top:5px; 
}
.button {
    background-color: #93E1D8;
    border: none;
    display: block;   
    cursor: pointer;
}   
</style>
       <script type="text/javascript">
           $(function () {

               $('#ctl00_ContentPlaceHolder1_submit1').click(function () {
                   var dist_code = '33';
                   var n = $("#ctl00_ContentPlaceHolder1_GridView1").find("tr").length;
                   var Invoice_no = document.getElementById("ctl00_ContentPlaceHolder1_Invoiceno").value;                 
                   var ff = selectedValue;                  
                   var Invoicedate = $("#datepicker").val();
                   var total_amount = document.getElementById("ctl00_ContentPlaceHolder1_Finaltot").value;
                   if (Invoicedate == '' && discode == '' && Invoice_no == '') {
                       alert('Please Select Date!!');
                   }
                   else {
                       $.ajax({

                           type: 'POST',
                           url: 'Inv.aspx/insertheaddata',
                           async: false,
                           data: "{'Invoice_no':'" + Invoice_no + "','Retailer_code':'" + selectedValue + "','Invoice_Date':'" + Invoicedate + "','Cust_code':'" + dist_code + "','Total_Amount':'" + total_amount + "'}",
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


                   }
                   var data = Array();
                   $("#ctl00_ContentPlaceHolder1_GridView1 tr").each(function () {
                       var Product_name = $(this).find("td:eq(1)").text();
                       var Product_code = $(this).find("td:eq(0) :input").val();

                       var Quantity = $(this).find("td:eq(3) :input").val();

                       var Pieces = $(this).find("td:eq(4) :input").val();
                       var value = $(this).find("td:eq(5) :input").val();

                       if (Quantity != "" && Pieces != "" && value != "") {
                           $.ajax({

                               type: 'POST',
                               url: 'Inv.aspx/insertdata',
                               async: false,
                               data: "{'Product_name':'" + Product_name + "','Product_code':'" + Product_code + "','Quantity':'" + Quantity + "','Pieces':'" + Pieces + "','Value':'" + value + "','Inv_no':'" + Invoice_no + "'}",
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
                       } else {
                     
                       }

                   });



               })
               alert("Saved Successfully");
               document.getElementById("ctl00_ContentPlaceHolder1_TextBox2").value = '';
               document.getElementById("ctl00_ContentPlaceHolder1_TextBox3").value = '';
               document.getElementById("ctl00_ContentPlaceHolder1_TextBox4").value = '';
               document.getElementById("ctl00_ContentPlaceHolder1_Finaltot").value = '';
               document.getElementById("ctl00_ContentPlaceHolder1_Invoiceno").value = '';
               
           });  
        </script>  
   <script type="text/javascript">
       function isNumberKey(evt, id) {
           try {
               var charCode = (evt.which) ? evt.which : event.keyCode;

               if (charCode == 46) {
                   var txt = document.getElementById(id).value;
                   if (!(txt.indexOf(".") > -1)) {

                       return true;
                   }
               }
               if (charCode > 31 && (charCode < 48 || charCode > 57))
                   return false;

               return true;
           } catch (w) {
               alert(w);
           }
       }
   </script>
    
    <script type="text/javascript">
        $(function () {
            $("[id*=ctl00_ContentPlaceHolder1_GridView1] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
</script>
<style type="text/css">
   .tb8 {
	width: 230px;
	height:20px;
		border: 1px solid rgba(0, 192, 239, 0.39);
		border-left: 4px solid rgba(0, 192, 239, 0.39);
}  
.hdr
{
   padding-top:5px; 
}
.button {
    background-color: #93E1D8;
    border: none;
    display: block;   
    cursor: pointer;
}   
.hover_row
    {
        background-color: #d6e9c6;
    }
</style>
 <script type="text/javascript">

     function UserOrEmailAvailability() { //This function call on text change.             
         $.ajax({
             type: "POST",
             url: "Inv.aspx/CheckEmail", // this for calling the web method function in cs code.  
             data: '{useroremail: "' + $("#<%=Invoiceno.ClientID%>")[0].value + '" }', // user name or email value  
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: OnSuccess,
             failure: function (response) {
                 alert(response);
             }
         });
     }

     // function OnSuccess  
     function OnSuccess(response) {
         var msg = $("#<%=lblStatus.ClientID%>")[0];
      
         switch (response.d) {
      
             case "true":
                 msg.style.display = "block";
                 msg.style.color = "red";
                 msg.innerHTML = "Invoice ID already exists.";
                 break;
             case "false":
                 msg.style.display = "block";
                 msg.style.color = "green";
                 msg.innerHTML = "";
                 break;

         }
         $('#datepicker').focus();
     }  
  
    </script>  

</head>
<body>
    <form id="form1" runat="server"  >
    <asp:ScriptManager
 ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label runat="server" ID="lblSelectedValue" Style="color: red"></asp:Label>
      <%--<asp:TextBox ID="TextBox1"  CssClass="tb8" runat="server"></asp:TextBox>--%>
<table><tr><td style="padding-left:165px; ">
    <asp:Label ID="Invono" runat="server" Text="Invoice No :" Font-Bold="True" ForeColor="Black" Font-Names="Andalus"></asp:Label></td><td>
        <asp:TextBox ID="Invoiceno" CssClass="tb8" runat="server" onchange="UserOrEmailAvailability();"></asp:TextBox> <div id="checkusernameoremail" runat="server">                             
                            <asp:Label ID="lblStatus" runat="server"></asp:Label> </div> </td> <td style="padding-left:30px;">
            <asp:Label ID="InvoDate" runat="server" Text="Invoice Date :" Font-Bold="True" ForeColor="Black" Font-Names="Andalus"></asp:Label></td><td>
               <input type="date"  style="border:1px solid rgba(0, 192, 239, 0.39);border-radius:5px;border-left: 4px solid rgba(0, 192, 239, 0.39);";   id = "datepicker" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}"></td></tr>
                </table>
                <br />
                <br />
       <div id="container">
                	<div class="side-by-side clearfix">         

<table>
<tr ><td  style="padding-left:250px;">
    
    <asp:Label ID="RetailerName" runat="server" Text="Retailer Name :" Font-Bold="True" ForeColor="Black" Font-Names="Andalus"></asp:Label></td><td>
       <asp:DropDownList     ID="Ret_name" runat="server"  style="font-family: -webkit-pictograph;"  class="chzn-select"  >
        </asp:DropDownList>


      
     
    </td></tr>


    </table>
     </div>
    </div>
    <br />
    <br />
<center>


      <asp:GridView ID="GridView1" runat="server" Width="90%" 
          AutoGenerateColumns="false" ShowFooter="true" GridLines="None" 
          RowStyle-Height="20px" BackColor=#f0f3f4 
        >
 <HeaderStyle BackColor="#93E1D8" BorderStyle="Solid"  BorderColor="White" ForeColor="BLACK"
         BorderWidth="1px" Width="75%" height="57px"  Font-Size="12pt"    Font-Names="Andalus" Font-Bold="True" />  
            
                    
                    <Columns> 
                                                       <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="20px"  ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Andalus">
                                <ItemTemplate>
                                  <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Product_Detail_Code") %>' />
                                    <asp:Label ID="lblSNo" runat="server" Width="15px" Height="30px" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Product Name"    ItemStyle-HorizontalAlign="Center"  FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" ItemStyle-Font-Names="Andalus"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <asp:HiddenField ID="productrate" runat="server" Value='<%# Eval("NSR_Price") %>' />  
                                                    <asp:Label ID="detailname" Height="30px"  runat="server" Width="195px" Text='<%# Eval("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                 </asp:TemplateField>
                                                 
                                                
                                             <asp:TemplateField HeaderText="Pack"  ItemStyle-HorizontalAlign="Center"  FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" ItemStyle-Font-Names="Andalus" ItemStyle-Width="6px"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                  <asp:HiddenField ID="prtratepice" runat="server" Value='<%# Eval("Retailor_Price") %>' />   
                                                    <asp:Label ID="pack" runat="server"  Height="28px" Text='<%# Eval("Product_Unit") %>' Width="95px"  ></asp:Label>
                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Quantity"  ItemStyle-Font-Names="Andalus" ItemStyle-HorizontalAlign="Center" 
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                
    <%--                                             <asp:Label ID="productrate" Height="20px" runat="server"  Width="45px" MaxLength="5"  Text='<%# Eval("NSR_Price") %>'  ForeColor="White"   ></asp:Label>--%>
                                                    <asp:TextBox ID="txtquantity" runat="server"  Width="65px" MaxLength="5" onkeypress="return isNumberKey(event,this.id)"   onkeyup="FetchData(this)"  CssClass="tb8"   BackColor="AliceBlue"></asp:TextBox>
                                                  <%--  <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtprice">
                                                    </asp:FilteredTextBoxExtender>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField    ItemStyle-HorizontalAlign="Center"  HeaderText="Pieces"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                                                                
<%--                                                    <asp:Label ID="prtratepice" Height="44px" runat="server"  Width="45px" MaxLength="5"  Text='<%# Eval("Retailor_Price") %>'   ForeColor="White"   ></asp:Label>--%>
                                                    <asp:TextBox ID="txtpiece"  runat="server"  Width="65px" MaxLength="5" onkeypress="return isNumberKey(event,this.id)"   onkeyup="FetchData(this)" CssClass="tb8"  ></asp:TextBox>
                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                           <asp:TemplateField   HeaderText="Value"  ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Andalus"
                                                HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle">
                                                <ItemTemplate> 
                                                
                                                 <asp:TextBox ID="ffvalue"   ReadOnly="true"  runat="server" class="txt" Width="79px" MaxLength="5"   CssClass="tb8"    BackColor="#f1f8e9"  ></asp:TextBox>
                                              
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                              
                                        
                                                    
                                                    
                                                   
                                                  
                                              
                                          
    </Columns> 
     
    <FooterStyle Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
       </center>
       <br />

      
    
      <table>    <tr><td width="280px">&nbsp</td><td></td><td  style="padding-left:496px;" align="right"><asp:Label ID="Label5" runat="server"  Font-Bold="True"  Text="Total:" ForeColor="Black" ></asp:Label></td><td><asp:TextBox ID="TextBox2" runat="server" CssClass="tb8" BackColor="#cffabd"  BorderColor="Red" BorderStyle="Dotted" BorderWidth="1.5px" Width="130px" ></asp:TextBox></td></tr>
      <tr><td width="288px">&nbsp</td><td></td><td  style="padding-left:490px;"><asp:Label ID="Label6" runat="server" Text="CGst 9% :" Font-Bold="True" ForeColor="Black" ></asp:Label></td><td><asp:TextBox ID="TextBox4" runat="server" CssClass="tb8" BackColor="#cffabd"  BorderColor="Red" BorderStyle="Dotted" BorderWidth="1.5px" Width="130px"></asp:TextBox></td></tr>
        <tr><td width="266px"></td><td></td> <td  style="padding-left:490px;"><asp:Label ID="Label3" runat="server" Text="SGst 9% :" Font-Bold="True" ForeColor="Black" ></asp:Label></td><td><asp:TextBox ID="TextBox3" runat="server"  Width="130px" CssClass="tb8" BackColor="#cffabd" BorderColor="Red" BorderStyle="Dotted" BorderWidth="1.5px"></asp:TextBox></td >
</tr></table>
        <table><tr><td style="padding-left:490px;"><asp:Button ID="submit1" runat="server" Text="Save" Width="95px" Height="30px"  CssClass="button" UseSubmitBehavior="false"
           Font-Names="Andalus" /> </td><td>&nbsp;&nbsp;<td><asp:Button ID="Button1" runat="server" Text="Print" Width="95px" Height="30px" CssClass="button" UseSubmitBehavior="false" onclientclick="myFunction()"
               Font-Names="Andalus" /></td>        <td  style="padding-left:65px;"><asp:Label ID="Label1" runat="server" Text="Final Total:" Font-Bold="True" ForeColor="Red" ></asp:Label></td><td><asp:TextBox ID="Finaltot" runat="server" CssClass="tb8"    ForeColor="Red" BackColor="#FFDAB9"  BorderColor="Red" BorderStyle="Dotted" BorderWidth="1.5px" Width="175px"></asp:TextBox></td>
             </tr>
            </table>
      <script src="../js/chosen.jquery.js" type="text/javascript"></script>
   	<script type="text/javascript">   	    $(".chzn-select").chosen();
   	    //   	    var myValues = $('.chzn-container chzn-container-single').chosen().val();
   	    $('.chzn-select').on('change', function (evt, params) {
   	        selectedValue = params.selected;
   	       

   	        alert(selectedValue);

   	    });

   	    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
   	  
         </script>
     
     </form>
</body>
</html>


</asp:Content>






