<%@ Page Title="Fare Fixation"  Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Fare_fixation_ft.aspx.cs" Inherits="Fare_fixation_ft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head>
  
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />

    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            function jj() {

                //            $("[id*=btncancel]").bind("click", function () {
                $('#<%= GridView1.ClientID %> input[type=text]').val("");
                //         });
            }
        });
    </script>
<script  type="text/javascript">
        function FetchData(button) {
           
            var fareval = $(button).val();
            var id = '#' + button;
            //   var name = $(this).closest('tr').find('.contact_name').text();
            var char = $(button).children().eq(0);
            var ff = $(button).closest("tr").find("input:hidden").val();
          
            $('[id*=GridView1]').find('tr:has(td)').each(function () {

                var sf_name = $(this).find("td:eq(1)").text();
                var sf_code = $(this).find("td:eq(0) :input").val();
              
                if(sf_code == ff) {


                    $(this).find("td:eq(3) :input").val(fareval);

                }
            });

            //iterate through each textboxes and add keyup
            //handler to trigger sum event
            //   var row = button.parentNode.parentNode;
            //           $("#ffvalue").each(function () {

            //               $(this).keyup(function () {
            //                   calculateSum();
            //               });
            //           });

        };
       </script>
    <script type="text/javascript">
	$(document).ready(function () {
           var div_code = <%=Session["div_code"]%>;
            if (div_code == "109") {
                
                var dd = 01;
                var mm = 08;
                var yyyy = 2021;
                if (dd < 10) {
                    dd = '0' + dd
                }
                if (mm < 10) {
                    mm = '0' + mm
                }
                document.getElementById("recpt-dt").setAttribute("min", yyyy + '-' + mm + '-' + dd);
            }
            else {
                let edt = new Date();
                var dd = edt.getDate();
                var mm = edt.getMonth() + 1;
                var yyyy = edt.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd
                }
                if (mm < 10) {
                    mm = '0' + mm
                }
                document.getElementById("recpt-dt").setAttribute("min", yyyy + '-' + mm + '-' + dd);
            }
            if (div_code == "109") {
                $("#recpt-dt").val('2021-08-01');
            }
			 else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Fare_fixation_ft.aspx/GetFare_fixation_ftValues",
                dataType: "json",
                success: function (data) {
                    Filldata(data);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
              }
            function Filldata(data) {

                if (data.d.length > 0) {
                    document.getElementById("recpt-dt").setAttribute("min", data.d[0].EffDt);
                    $("#recpt-dt").val(data.d[0].EffDt);
                }
             }
        });
        $(function () {
            $("[id*=btsubmit]").bind("click", function () {
                var Customer = {};
				 var btndate = $('#recpt-dt').val();
                var fnldate = btndate;
                $('[id*=GridView1]').find('tr:has(td)').each(function () {
               
                     var sf_name = $(this).find("td:eq(1) :input").val();

                    var sf_code = $(this).find("td:eq(2) :input").val();
                    var sf_hq = $(this).find("td:eq(2)").text();
                    var Quantity = $(this).find("td:eq(3) :input").val();
                   
                    if (Quantity != "" ) {
                        $.ajax({
                            type: 'POST',
                            url: 'Fare_fixation_ft.aspx/insertdata',
                            async: false,
                            data: "{'sf_name':'" + sf_name + "','sf_code':'" + sf_code + "','Quantity':'" + Quantity + "','date':'" + fnldate + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {


                                //window.location.reload();
                            }
                        });
                    }
                    else {
                      
                    }
                });
                alert("Submitted successfully.");
                return false;
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   

</head>
<body>
    <form id="form1" runat="server">
	  <div class="row" style="margin-bottom: 1rem;">
            <div class="col-sm-6">
                <div class="col-sm-6">
                    <label style="float: right;">Effective From</label>
                </div>
                <div class="col-sm-6">
                    <input type="date" autocomplete="off" class="form-control" id="recpt-dt" />
                </div>
            </div>
        </div>
   <div class="row">
    <div class="col-md-6" align="left">
    <br />

        <center>
<asp:GridView ID="Griddesig" runat="server" GridLines="None" AutoGenerateColumns="false"  ForeColor="Black" BackColor="#cffabd " Width="50%"   HeaderStyle-BackColor="#2196f3"  HeaderStyle-ForeColor="White" Font-Names="Andalus" 
                 AlternatingRowStyle-BackColor="#fffce2"            BorderStyle="Solid"  HeaderStyle-Height="24px">


<Columns>
 <asp:TemplateField  ItemStyle-Width="10" ItemStyle-Height="24"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                         <input type="hidden" name="reply-id" Value='<%# Eval("Designation_Code") %>'>
                                          <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%# Eval("Designation_Code") %>' />
                                           <%-- <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Designation" ItemStyle-Width="180" ItemStyle-Height="24"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="desigshrtname" style="white-space:nowrap; padding-left:14px;" runat="server" Font-Size="10pt" Text='<%#Eval("Designation_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField  HeaderText="Fare" ItemStyle-Width="180" ItemStyle-Height="24"    HeaderStyle-Width="10" 
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                        <asp:TextBox ID="desigid" runat="server"  BorderColor="Aqua" class="form-control"   onkeyup="FetchData(this)"         style="width:70px;height:24px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="FieldForce" ItemStyle-Width="180" ItemStyle-Height="35"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

</Columns>


</asp:GridView>
 <table><tr><td></td><td></td><tr><td>     <asp:Button ID="btsubmit" runat="server" Text="Save"  
                BackColor="#68dff0" Width="80px"  Height="40px"  style="border-radius:5px;" onclick="btsubmit_Click"
              ></asp:Button></td><td>&nbsp;&nbsp;</td><td>   <asp:Button ID="btncancel" runat="server" Text="Cancel"    OnClientClick="function jj();"
              
                BackColor="#68dff0" Width="80px"    Height="40px" style="border-radius: 5px;"
              ></asp:Button></td></tr>
                    
                    </tr>
                    
                    </table>
</div>
<div class="col-md-6" >
   <asp:GridView ID="GridView1" runat="server" GridLines="None" AutoGenerateColumns="false"  ForeColor="Black" BackColor="#cffabd " Width="60%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" AlternatingRowStyle-BackColor="#b9f6ca" HeaderStyle-Height="40px">
                    <Columns>
                    

                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"  HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                               <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Designation_Code") %>' />
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce" ItemStyle-Width="180" ItemStyle-Height="35"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("Sf_Name") %>' />
                                  
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                            <asp:Label ID="Label1" style="white-space:nowrap;font-size: 8pt;display: block;color: #6f6565;" runat="server" Font-Size="9pt" Text='<%#Eval("designation_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HeadQuarters" ItemStyle-Width="130" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
 <asp:HiddenField ID="HiddenSF" runat="server" Value='<%# Eval("SF_Code") %>' />
                                            <asp:Label ID="SF_hq" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fare" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                          <asp:TextBox ID="fareeid"   runat="server"  BorderColor="Aqua" class="form-control" Text='<%#Eval("fare")%>'   style="width:70px;height:24px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                     
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

                    </asp:GridView>

                   
             </div>
</div>
                   

                   
               

             



        </center>
    </div>
    </form>
</body>
</html>

</asp:Content>

