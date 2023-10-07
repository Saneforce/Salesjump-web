<%@ Page Title="Tax_Master" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Tax_Master.aspx.cs" Inherits="MasterFiles_Tax_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<head>
    
   <script type="text/javascript" 
   src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
<%--    <link href="../fonts/jquery.mobile.icons-1.4.5.min.css" rel="stylesheet" type="text/css" />--%>
<script type="text/javascript">
    function gg() {
        alert($("#flip-0").val());
        var Tax_name = document.getElementById("ctl00_ContentPlaceHolder1_txtltaxname").value;
        var tax_type = $("#flip-0").val();
alert(tax_type);
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
                    //                                        $('#TextBox1').val('');
                    //                                        $('#TextBox2').val('');
                    //                                        $('#TextBox3').val('');
                    //                                        $('#TextBox4').val('');
                    alert("Inserted Sucessfully");

                }
            },

            error: function (result) {
                alert("Error Occured, Try Again");
            }
        });


       return false;
       
    }

</script>
<script type="text/javascript">
     function cancel() {
document.getElementById("ctl00_ContentPlaceHolder1_txtltaxname").value='';
document.getElementById("ctl00_ContentPlaceHolder1_txtltaxvalue").value='';
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
  border-left: 5px solid #9b2;
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
form::after {
  content: "";
  display: block;
  clear: both;
}
label {
  position: absolute;
  left: -9999px;
}
input {
  position: relative;
  z-index: 10;
  margin: 0;
  padding: 0 5px;
  width: 225px;
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
}

 </style>
    <link href="../fonts/jquery.mobile-1.4.5.css" rel="stylesheet" type="text/css" /> 
       
  <script src="//code.jquery.com/jquery-1.10.2.min.js"></script>
  <script src="//code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
  </head>

  <body style="font:smaller;">
          
 <div class="box">
   <div class="form-header">
 <h2 align="center">      <img class="s-img service_178" src="http://ihorizon.co.uk/wp-content/uploads/2013/08/rd.png" alt="R&amp;D Tax credit application" height="25px">&nbsp &nbsp Tax Master</h2>
    </div>

  
  <form runat="server">    
   
    
    <div>
  
     <br >
      </br>
    
      <table align="center"><tr><td width="90px">
          <asp:Label ID="lbltaxnme" runat="server" Text="Tax Name" Font-Bold="true" Font-Names="ANDALUS" Font-Size="Medium"></asp:Label></td><td>  <asp:TextBox ID="txtltaxname" runat="server"></asp:TextBox></td></tr>
     <tr><td> <asp:Label ID="Label1" runat="server" Text="Tax Type" Font-Bold="true" Font-Names="ANDALUS" Font-Size="Medium"></asp:Label></td><td><label for="flip-0">Select slider:</label>
    <select name="flip-0" id="flip-0" data-role="slider">
      <option value="%">%</option>
      <option value="Rs">Rs</option>
    </select></td></tr> <tr><td>   <asp:Label ID="Lblvalue" runat="server" Text="Value" Font-Bold="true" Font-Names="ANDALUS" Font-Size="Medium"></asp:Label></td><td>  
          <asp:TextBox ID="txtltaxvalue" runat="server"></asp:TextBox></td></tr>
       
      </table>
      <br ></br>
   <table align="center"><tr ><td width="23px" ></td><td align="center">
      
       <asp:Button ID="Button1"    OnClientClick="return gg();"   runat="server" Text="Submit" style="background-color:#9b2;"   /> </td><td width="10px"></td><td>
           <asp:Button ID="btncancel" runat="server" OnClientClick="return cancel();" Text="Cancel" /></td></tr></table>
     
    
      
    </div> 
    
  </form>
</div>  
    
  </body>
</asp:Content>
