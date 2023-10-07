<%@ Page Title="Tax Master" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Tax_mfinal.aspx.cs" Inherits="MasterFiles_Tax_mfinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<head>
    <link href="../css/bootstrap-slider/Switch.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript" 
   src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
   <script type="text/javascript">
       var dt = [];
       function fillslot(scode) {
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               async: false,
               url: "Tax_mfinal.aspx/getTax1",
               data: "{'divcode':'" + divcode + "','scode':'" + scode + "'}",
               dataType: "json",
               success: function (data) {
                   var dts = JSON.parse(data.d) || [];
                   $('#hscode').val(dts[0].Tax_Id);
                   $('input#ctl00_ContentPlaceHolder1_txtltaxname').val(dts[0].Tax_Name);
                   var tx_tp = dts[0].Tax_Type;
                   if (tx_tp == 'Rs') {
                       $("#VwToClient").prop('checked',true);
                   }
                   else {
                       $("#VwToClient").prop('checked',false);
                   }
                   $('input#ctl00_ContentPlaceHolder1_txtltaxvalue').val(dts[0].Value);
               },
               error: function (data) {
                   alert(JSON.stringify(data));
               }
           });
       }
       function gg1() {
           var tax_code = $('#hscode').val();
           var Tax_name = document.getElementById("ctl00_ContentPlaceHolder1_txtltaxname").value;

           //var tax_tp = $("#VwToClient").attr('checked');
           //if (tax_tp == "checked") {
               //var tax_type  = 'Rs';
           //}
           //else {
               var tax_type = '%';
           //}

           //var tax_tp = document.getElementById("ct100_ContentPlaceHolder1_DropDownList1").value;
     
           var tax_value = document.getElementById("ctl00_ContentPlaceHolder1_txtltaxvalue").value;
           if (Tax_name == '' || Tax_name == null) {
               alert('Enter the Tax Name');
               return false;
           }
           //if (tax_tp == '' || tax_tp == null) {
           //    alert('Select tax type');
           //    return false;
           //}
           if (tax_value == '' || tax_value == null) {
               alert('Enter the Tax Value');
               return false;
           }
		    $.ajax({
               type: 'POST',
               url: 'Tax_mfinal.aspx/selecttaxdata',
               async: false,
                data: "{'Tax_Name':'" + Tax_name + "','Tax_Type':'" + tax_type + "','Value':'" + tax_value + "'}",
               contentType: 'application/json; charset =utf-8',
               success: function (data) {
                   dt =JSON.parse(data.d) || [];
               }
           });
           if (dt.length > 0) {
               alert('TaxDetails Already Exists...');
           }
           else {
           $.ajax({

               type: 'POST',
               url: 'Tax_mfinal.aspx/inserttaxdata',
               async: false,
               data: "{'Tax_Name':'" + Tax_name + "','Tax_Type':'" + tax_type + "','Value':'" + tax_value + "','tax_code':'" + tax_code+"'}",
               contentType: 'application/json; charset =utf-8',
               success: function (data) {
                   var obj = data.d;
                   if (obj == 'true') {
                          document.getElementById("ctl00_ContentPlaceHolder1_txtltaxname").value = '';
                          document.getElementById("ctl00_ContentPlaceHolder1_txtltaxvalue").value = '';
                          alert("Inserted Sucessfully");
                   }
               },
               error: function (result) {
                   alert("Error Occured, Try Again");
               }
           });
               }
           return false;

       }
       $(document).ready(function () {
           divcode =<%=Session["div_code"]%>;
           $('#hscode').val(<%=tax_code%>);
           fillslot($('#hscode').val());
       });
   </script>
<%--    <link href="../fonts/jquery.mobile.icons-1.4.5.min.css" rel="stylesheet" type="text/css" />--%>
<script type="text/javascript">
    
    function gg() {
        alert($("#flip-0").val());
        var Tax_name = document.getElementById("ctl00_ContentPlaceHolder1_txtltaxname").value;
        var tax_type = $("#flip-0").val();
        var tax_value = document.getElementById("ctl00_ContentPlaceHolder1_txtltaxvalue").value;

        $.ajax({

            type: 'POST',
            url: 'Tax_master.aspx/inserttaxdata',
            async: false,
            data: "{'Tax_Name':'" + Tax_name + "','Tax_Type':'" + tax_type + "','Value':'" + tax_value + "'}",
            contentType: 'application/json; charset =utf-8',
            success: function (data) {

                var obj = data.d;
                if (obj == 'true') {
                    //$('#TextBox1').val('');
                    //$('#TextBox2').val('');
                    //$('#TextBox3').val('');
                    //$('#TextBox4').val('');
                    alert("Inserted Sucessfully");
                }
            },

            error: function (result) {
                alert("Error Occured, Try Again");
            }
        });


        return false;

    }
  function character(e) {
           isIE = document.all ? 1 : 0
           keyEntry = !isIE ? e.which : event.keyCode;
		    if (event.keyCode == 32) {
                    event.returnValue = false;
                    return false;
                } 
           if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
               return true;
           else {
               return false;
           }
       }
	    function AvoidSpace() {
                if (event.keyCode == 32) {
                    event.returnValue = false;
                    return false;
                }
            } 
			 function IsOneDecimalPoint(evt, element) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
            return false;
        else {
            var len = $(element).val().length;
            var index = $(element).val().indexOf('.');
            if (index > 0 && charCode == 46) {
                return false;
            }
            if (index > 0) {
                var CharAfterdot = (len + 1) - index;
                if (CharAfterdot > 3) {
                    return false;
                }
            }

        }
        return true;
    }
</script>
<script type="text/javascript">
    function cancel() {
        document.getElementById("ctl00_ContentPlaceHolder1_txtltaxname").value = '';
        document.getElementById("ctl00_ContentPlaceHolder1_txtltaxvalue").value = '';
        return false;
    }
</script>
 <style type="text/css">

     .box {
  margin: auto;
  padding: 25px 50px;
  width: 750px;
  min-height: 115px;
  background: #fff;
  box-shadow: 0 0 20px rgba(0, 0, 0, .15);
  font: 12px/15px Arial, Helvetica, sans-serif;
  color: #666;
}
h2 {
  margin: 0 10px;
  line-height: 40px;
}
form {
  padding: 0 10px 10px;
}


input {
  position: relative;
  z-index: 10;
  margin: 0;
  padding: 0 5px;
  width: 85px;
  height: 30px;
  border: 1px solid #ccc;
}

#submit {
  float: right;
  padding: 0;
  width: 75px;
  background: #9b2;
  color: #140;
  border-color: #471;
}
#submit:hover {
  color: #025;
  background: #28e;
  border-color: #16c;
}
#submit ~ a {
  display: block;
  float: left;
  width: 120px;
  text-decoration: none;
  color: #9b2;
}
#submit ~ a:hover {
  text-decoration: underline;
}

 .form-header {
  background-color: #9b2;;
  border-radius: 4px 4px 0 0;

}

.form-header h2 {
  color: #fff; 
  font-size: 1.5em;
  margin: 0;
  background-color: #19a4c6;
}

 </style>
<%--    <link href="../fonts/jquery.mobile-1.4.5.css" rel="stylesheet" type="text/css" /> 
       
  <script src="//code.jquery.com/jquery-1.10.2.min.js"></script>
  <script src="//code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>--%>
  </head>

  <body style="font:smaller;">
       <a href="Tax_Master_grid.aspx" class="btn btn-primary btn-update" id="newsf" style="float: right;">Back</a>     
 <div class="box">
     <input type="hidden" id="hscode" /> 
   <div class="form-header">
 <h2 align="center">TAX CREATION</h2>
    </div>
  <form id="Form1" runat="server">   
    <div>
      <table align="center"><tr><td width="90px">
          <asp:Label ID="lbltaxnme" runat="server" Text="Tax Name" Font-Bold="true" Font-Names="ANDALUS" Font-Size="Medium"></asp:Label></td><td>  <asp:TextBox ID="txtltaxname"  runat="server" onkeypress="return character(event)" MaxLength="10"></asp:TextBox></td></tr>
     <%--<tr><td> <asp:Label ID="Label1" runat="server" Text="Tax Type" Font-Bold="true" Font-Names="ANDALUS" Font-Size="Medium"></asp:Label></td><td>--%>
	 <%--><label for="flip-0">Select slider:</label>--%>
    <%--<select name="flip-0" id="flip-0" data-role="slider">
      <option value="%">%</option>
      <option value="Rs">Rs</option>
    </select>--%>
   <%--  <label class="switch" style="float: center;align:center; padding-top:4px; color:Green;">
                                        <input type="checkbox" class="switch-input" id="VwToClient">
                                        <span class="switch-label" data-on="Rs" data-off="%"></span>
                                        <span class="switch-handle"></span>
                                    </label> --%>
        <%-- <asp:DropDownList ID="DropDownList1" runat="server">
             <asp:ListItem Value="">---Select---</asp:ListItem>  
            <asp:ListItem>Rs </asp:ListItem>  
            <asp:ListItem>% </asp:ListItem>  
            <asp:ListItem>GST </asp:ListItem>  
            <asp:ListItem>SGST </asp:ListItem>  
            <asp:ListItem>CGST </asp:ListItem>
         </asp:DropDownList>--%>
   <%--  </td></tr> --%>
	<tr><td>   <asp:Label ID="Lblvalue" runat="server" Text="Value" Font-Bold="true" Font-Names="ANDALUS" Font-Size="Medium"></asp:Label></td><td>  
          <asp:TextBox ID="txtltaxvalue" runat="server" onkeypress="return AvoidSpace(),IsOneDecimalPoint(event,this);"  MaxLength="10"></asp:TextBox></td></tr>
      </table>
         </br>
      <br ></br>
   <table align="center"><tr ><td width="23px" ></td><td align="center">
      
       <asp:Button ID="Button1"    OnClientClick="return gg1();"   runat="server" Text="Submit"/> </td><td width="10px"></td><td>
           <asp:Button ID="btncancel" runat="server" OnClientClick="return cancel();" Text="Cancel" /></td></tr></table>
    </div> 
  </form>
</div>  
    
  </body>
</asp:Content>
