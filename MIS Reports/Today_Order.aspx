<%@ Page Title="Order Confirmation" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Today_Order.aspx.cs" Inherits="MIS_Reports_Today_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Bootstrap custom collapse table</title>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
        <script src="../js/jQuery-2.2.0.min.js" type="text/javascript"></script>
        <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            $(function () {
                var viewState = $('#<%=Hdn_Date.ClientID %>').val();
                $("#txtDate").val(viewState);
            });

            function myFunction() {
                var viewState = document.getElementById("txtDate").value;
                $('#<%=Hdn_Date.ClientID %>').val(viewState);
                <%--__doPostBack("<%=UniqueID%>");--%>
            }


            $(document).ready(function () {
                console.log($("#<%=Repeater1.ClientID %> ").find('tr:gt(0)').length);

                $("button[name = 'btncc']").click(function () {
                    if ($(this).text() == "Order Confirm") {
                        var arr = [];
                        var itm = {};
                        var slvals = []
                         $("#tblCustomers [id*=chkRow]:checked").each(function () {
                            slvals.push($(this).closest('tr').children('td.rowVal').text().trim());

                        })

                        itm.flag = $('#submit').val().toString();
                        itm.custid = $(this).closest("div").find("[id$='hfCustomerId']").val();
                        itm.ordno = $(this).closest("div").find("[id$='hforderno']").val();
                        arr.push(itm);
                        if (slvals.length > 0) {
						if (confirm("Do You Want to confirm this Order..."))
						{
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "Today_Order.aspx/savedata",
                                data: "{'data':'" + JSON.stringify(arr) + "','prod':'" + slvals+"'}",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d == "Sucess")
                                        alert("Order has been updated Successfully");
                                    else
                                        alert("Order updation Successfull");
										
									 location.reload();	
										
                                },
                                error: function (result) {
                                    alert("Order updation unSuccessfull");
                                }
                            });
						}
						else {
                            alert("You have Cancelled");
                        }
                        }
                        else {
                            alert('Plz Select Checkbox values !!');

                        }
                       
                    }
                    else {
					var arr1 = new Array();
                        var ckbxval = []
                        $("#tblCustomers [id*=chkRow]:checked").each(function () {
                            ckbxval.push($(this).closest('tr').children('td.rowVal').text().trim());

                        });
                        if (ckbxval.length > 0) {
                            $(this).popover({
                                placement: 'top',
                                title: '<span class="text-info"><strong>title</strong></span>' +
                                    '<button type="button" id="close" class="close" onclick="$(&quot;#example&quot;).popover(&quot;hide&quot;);">&times;</button>',
                                html: true,
                                content: function () {
                                    
                                    return $('#myForm').html();

                                }
                                // $(this).popover('show')

                            }).on('click', function () {
                                // had to put it within the on click action so it grabs the correct info on submit
                                //$(this).popover('show');
                                arr1[1] = $(this).closest('div').find("[id$='HiddenField1']").val();
                                arr1[2] = $(this).closest("div").find("[id$='hidodrnum']").val();
                                $("[id$='send']").click(function () {


                                    arr1[0] = $(".popover #about").val();

                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        url: "Today_Order.aspx/saveData1",
                                        data: "{'data':'" + arr1 + "','prod':'" + ckbxval +"'}",
                                        dataType: "json",
                                        success: function (data) {
                                            if (data.d == "Sucess")
                                                alert("Order Reject has been updated Successfully");

                                            else
                                                alert("Order Reject updation Successfull");
                                            location.reload();
                                        },
                                        error: function (result) {
                                            alert("Order Reject updation unSuccessfull");
                                        }
                                    });
                                    $.post('/echo/html/', {
                                        email: $('#about').val(),
                                        name: $('#name').val(),
                                        gender: $('#gender').val()

                                    }, function (r) {
                                        $('#pops').popover('hide')

                                    })
                                });
                            });
                        }
                        else {
                            alert("Please Select Checkboxes");
                        }
                        
                    }
                });
            });
			$(document).on('click', '#btntran', function (e) {
                    if ($(this).text().trim() == "Transferred") {
                        var arr1 = new Array();
                        var slvals = [];
                        var txt = "";
                        var pSelect = "";
                        $("#tblCustomers [id*=chkRow]:checked").each(function() {

                            slvals.push($(this).closest('tr').children('td.rowVal').text());
                            //txt = $(this).closest('tr').children('td.rowVal1').text()
                            //pSelect = $('#Select1').val();
                        });
						txt = $("#ordno").val();
                        pSelect = $('#Select1').val();
                        cuscode = $("#custId").val();
						//if (slvals.length>0) {

                        $.ajax({

                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Today_Order.aspx/savedata2",
                            data: "{'data':'" + cuscode + "','orno':'" + txt + "','dd':'" + pSelect + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == "Sucess")
								{
                                    alert("Order has been Transferred Successfully");
									
								}
                                else
                                    alert("Order Transferred Successfull");
									location.reload();
                            },
                            error: function (result) {
                                alert("Order Transferred Successfull");
                            }
                        });
						//}
						//else {
                        //    alert("Please Tick Checkbox...");
                        //    location.reload();
                        //}

                    }
                });
        </script>
        <script language="JavaScript">
            function ToggleDisplay(id) {
                var elem = document.getElementById('d' + id);
                var divsToHide = document.getElementsByClassName("fa fa-plus");
                var divsToshow = document.getElementsByClassName("fa fa-minus");
                if (elem) {
                    if (elem.style.display != 'block') {
                        elem.style.display = 'block';
                        elem.style.visibility = 'visible';

                        for (var i = 0; i < divsToHide.length; i++) {
                            divsToHide[i].style.visibility = "hidden";
                            divsToshow[i].style.visibility = "visible";
                        }
                    }
                    else {
                        elem.style.display = 'none';
                        elem.style.visibility = 'hidden';
                        for (var i = 0; i < divsToHide.length; i++) {
                            divsToHide[i].style.visibility = "visible";
                            divsToshow[i].style.visibility = "hidden";
                        }
                    }
                }
            }
        </script>
        <style type="text/css">
            .header
            {
                cursor: hand;
                cursor: pointer;
            }
            .details
            {
                display: none;
                visibility: hidden;
                margin-top: 16px;
            }
            @media only screen and (max-width: 768px)
            {
                .col-sm-4
                {
                    border-top: 2px solid #d2c8c8;
                }
            }
            @media only screen and (min-width: 768px)
            {
                .col-sm-4
                {
                    border-left: 2px solid #d2c8c8;
                }
            }
            /* Only affects 1600px width and higher */
            @media only screen and (min-width: 1600px)
            {
                .table table-bordered
                {
                    font-size: 128px;
                }
            }
            /* Only affects 1200px-1600px width */
            @media only screen and (max-width: 1600px)
            {
                .table table-bordered
                {
                    font-size: 72px;
                }
            }
            /* Only affects 900px-1200px width */
            @media only screen and (max-width: 1200px)
            {
                .table table-bordered
                {
                    font-size: 48px;
                }
            }
            /* Only affects 600px-900px width */
            @media only screen and (max-width: 900px)
            {
                .table table-bordered
                {
                    font-size: 36px;
                }
            }
            /* Only affects 400-600px width */
            @media only screen and (max-width: 600px)
            {
                .table table-bordered
                {
                    font-size: 24px;
                }
            }
            /* Only affects 400px width and lower */
            @media only screen and (max-width: 400px)
            {
                .table table-bordered
                {
                    font-size: 12px;
                }
            }
            @media only screen and (max-width: 200px)
            {
                .table table-bordered
                {
                    font-size: 6px;
                }
            }
            @media only screen and (max-width: 000px)
            {
                .table table-bordered
                {
                    font-size: 3px;
                }
            }
            .body
            {
                padding-top: 100px;
            }
        </style>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
        <script type="text/javascript">
            $(function () {

                $('input[name="btnDelete"]').click(function (e) {
                    var arr1 = new Array();
                    var slvals = [];
                    var txt = "";
                    var pSelect = "";
                    $("#tblCustomers [id*=chkRow]:checked").each(function() {

                        slvals.push($(this).closest('tr').children('td.rowVal').text());
                        txt = $(this).closest('tr').children('td.rowVal1').text()
                        pSelect = $(this).closest('tr').children('td.orno').text();


                    });
					
					 if (slvals.length > 0) {
					 if (confirm("Are You sure delete this Order?")) {
                    $.ajax({

                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Today_Order.aspx/savedel",
                        data: "{'data':'" + slvals + "','orno':'" + txt + "','dd':'" + pSelect + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == "Sucess") {
                                alert("Order has been Delete Successfully");
                                var row = $(this).closest('tr');
                                row.remove();
                            }
                            else {
                                alert("Order Delete Successfully");
                            }
							location.reload();

                        },
                        error: function (result) {
                            alert("Order Delete unSuccessfully");
                        }
                    });
					}
					else {
                            alert("You have Cancelled");
                        }
					}
					else {
                        alert("Please Tick Checkbox");
                    }


                })
            })
        </script>
        <script type="text/jscript">
            $(document).ready(function () {

                var date = $("#txtDate").val();
                if (date == "")
                    document.getElementById('txtDate').valueAsDate = new Date();

            });
        </script>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
        <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
        <script type="text/javascript">
            function chkAllProducts(x) {
                $tb = $(x).closest('table').find('tbody');
                $($tb[0]).find('input[type="checkbox"]').each(function () { 
				if ($(x).attr('checked')) {
                        if (!($(this).is('[disabled=disabled]'))) 
                        $(this).attr('checked', 'checked');
                    } 
				else 
				$(this).removeAttr('checked'); });

            }

            $(function () {
                $("#tblCustomers [id*=chkHeader]").click(function () {
                    if ($(this).is(":checked")) {
                        $("#tblCustomers [id*=chkRow]").attr("checked", "checked");
                    } else {
                        $("#tblCustomers [id*=chkRow]").removeAttr("checked");
                    }
                });
                $("#tblCustomers [id*=chkRow]").click(function () {
                    if ($("#tblCustomers [id*=chkRow]").length == $("#tblCustomers [id*=chkRow]:checked").length) {
                        $("#tblCustomers [id*=chkHeader]").attr("checked", "checked");
                    } else {
                        $("#tblCustomers [id*=chkHeader]").removeAttr("checked");
                    }
                });
            });
        </script>
        <style type="text/css">
/* The Modal (background) */
.modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
    position: relative;
    background-color: #fbfbfc;
    margin: auto;
    padding: 0;
    border: 1px solid #888;
    width: 50%;
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
}

/* Add Animation */
@-webkit-keyframes animatetop {
    from {top:-300px; opacity:0} 
    to {top:0; opacity:1}
}

@keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}

/* The Close Button */
.close {
    color: white;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: #000;
    text-decoration: none;
    cursor: pointer;
}

.modal-header {
    padding: 2px 16px;
    background-color: #5cb85c;
    color: white;
}

.modal-body {padding: 2px 16px;}

.modal-footer {
    padding: 2px 16px;
    background-color: #5cb85c;
    color: white;
}
</style>
        <script type="text/jscript">
            $(document).ready(function () {

            });
        </script>
        <style type="text/css">
            .col-md-4
            {
                padding: 0px 3px 6px 4px;
            }
        </style>
        <script type="text/javascript">

            //function pleasewait() {
            $(document).ready(function () {

                $('#<%=btnSubmit.ClientID%>').click(function () {
                    if ($('#txtDate').val() == "") { alert("Please Select Date."); $('#txtDate').focus(); return false; }
                });

                $(document).on('click', '.pleasewait', function () {


                    window.location = "../Invoice_Entry.aspx?Or_Date=" + encodeURIComponent($("#txtDate").val()) + "&Dis_code=" + encodeURIComponent($('#<%=ddl_dis.ClientID%> option:selected').val()) + "&Cus_code=" + encodeURIComponent($(this).parent().find('[id$=Label3]').text());

                });
            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="Hdn_Date" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
            <br />
            <br />
            <center>
                <table>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Distributor Name" Width="106px"
                                CssClass="col-md-4 control-label"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddl_dis" runat="server" SkinID="ddlRequired" Style="font-size: 17px;"
                                        Width="500">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Date" Visible="true"
                                CssClass="col-md-4 control-label"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input id="txtDate" name="txtFrom" type="date" cssclass="TEXTAREA" maxlength="5"
                                        onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                        onkeypress="AlphaNumeric_NoSpecialChars(event);" tabindex="1" skinid="MandTxtBox"
                                        style="font-size: 18px; padding: 0px 6px;" />
                                </div>
                            </div>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <asp:Button ID="btnSubmit" runat="server" Text="View" class="btn btn-primary" OnClientClick="myFunction()"
                            OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </center>
            <br />
        </div>
        <div id="allDiv">
            <!-- ROW 1 -->
            <asp:Repeater ID='Repeater1' runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <nav role="navigation"  >
    <div  id="bs-example-navbar-collapse-1" >
      <ul >
     <div class="panel panel-default">
    <div class="panel-body" id='h<%# DataBinder.Eval(Container, "ItemIndex") %>' class="header"
          onclick='ToggleDisplay(<%# DataBinder.Eval(Container, "ItemIndex") %>);' ></h4> <img alt="" style="cursor: pointer" src="../Images/plus.png" /></i><span style="color: Green; font-weight: bold;font-size:medium;padding-left:10px""><%# Eval("retailername")%></span><asp:Image ID="Image1" runat="server" ImageUrl="../img/aprr.png"  style="height:50px; width:100px;"></asp:Image><asp:Image ID="Image2" runat="server" ImageUrl="../img/rej.png"  style="height:50px; width:100px;"></asp:Image><asp:Image ID="Image3" runat="server" ImageUrl="../img/partial.png"  style="height:50px; width:100px;"></asp:Image><span style="color: Black; font-weight: bold;font-size:x-large;padding: 8px;float: right;"><%#DataBinder.Eval(Container.DataItem, "Order_value", "{0:N2}")%></span><asp:Label ID="Label3" runat="server" Visible="true" style="display:none;" Text='<%# Eval("Cust_Code")%>' /></div>
    <div class="panel-heading">
     <div class="row">
        <div class="col-sm-3">
            <div class="center-block" ><center>Order Taken By</center>
           
         <span style="color: Black; font-weight: bold;font-size:medium;font-size: 11px"><center><%# Eval("Sf_Name")%></center></span>  ,
         </div>
        </div>
        <div class="col-sm-4">
            <div class="center-block" ><center>Route</center>
           
         <span style="color: Black; font-weight: bold;font-size:medium;font-size: 11px"><center><%# Eval("routename")%></center></span>
            </div>
        </div>
         <div class="col-sm-4">
            <div class="center-block" ><center>Net Weight</center>
            
         <span style="color: Black; font-weight: bold;font-size:medium;font-size: 11px"><center><%#DataBinder.Eval(Container.DataItem, "net_weight_value")%></center></span>
          <asp:HiddenField ID="HiddenField2"  runat="server" Value='<%# Eval("Order_Flag") %>' />
            </div>
        </div>
 	</div>
  <div id='d<%# DataBinder.Eval(Container, "ItemIndex") %>' class="details" class="collapse" data-parent="bs-example-navbar-collapse-1" >
    <div class="panel-group" style="padding-left:20px;padding-right:0px;">
     <table id="Table1" class="table table-bordered">
     <tr>
     <td>
         <asp:Panel ID="pnlOrders" runat="server" >
        
                    <asp:Repeater ID="rptOrders" runat="server" >
                        <HeaderTemplate>
                          <table id="tblCustomers" class="table table-bordered" cellspacing="0" rules="all" border="1">
                          <thead>
                                <tr>
                               
                              <th align="left" scope="col" style="width: 30px">
                                        <input id="chkall" type="checkbox" onchange="chkAllProducts(this)" />
                                    </th>
                                <th scope="col" style="width: 50px">
                                        S.No
                                    </th>
                                   
                                    <th scope="col" style="width: 150px">
                                        Product Name
                                    </th>
                                    <th scope="col" style="width: 150px">
                                       Quantity
                                    </th>
                                    <th scope="col" style="width: 150px">
                                       Net Weight
                                    </th>
                                    <th scope="col" style="width: 100px">
                                       Discount(%)
                                    </th>
                                      <th scope="col" style="width: 120px">
                                       Free
                                    </th>
                                   <th scope="col" style="width: 150px">
                                       Value
                                    </th>
                                   
                                     <%--<th scope="col" style="width: 30px">
                                       
                                    </th>--%>
                                </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                             
                      <td class="singleCheckbox">
                  <%--<input id="chkRow" type="checkbox"  ('<%# Eval("Order_Flag")%>'=='2') ? 'disabled' : ''/>--%>
                          <asp:Checkbox ID="chkRow" name="chkcountry" runat="server" />
            </td>
                               <td>
                                <asp:Label ID="Label2" runat="server" Text='<%# Container.ItemIndex + 1 %>' />
                               </td>
                                <td class="rowVal1" style="display:none;">
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Cust_Code")%>' />
                               </td>
							   <td class="orno" style="display:none;">
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("trans_sl_no")%>' />
                               </td>
                            <td  class="rowVal" style="display:none;">
                            <asp:Label ID="pro_code" runat="server" Text='<%# Eval("Product_Code")%>'  />
                            </td>
                                <td >
                                    <asp:Label ID="lbl_Prod_name" runat="server" Text='<%# Eval("Product_Name")%>' />
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Quantity")%>' />
                                </td>
                                 <td class="text-right">
                                    <asp:Label ID="Lab_net" runat="server" Text='<%# Eval("net_weight")%>' />
                                </td>
                                 <td class="text-right">
                                    <asp:Label ID="Lab_dis" runat="server" Text='<%# Eval("discount")%>' />
                                </td>
                                  <td class="text-right">
                                    <asp:Label ID="Lab_free" runat="server" Text='<%# Eval("free")%>' />
                                </td>
                                 <td class="text-right">
                                    <asp:Label ID="Lab_val" runat="server" Text='<%# Eval("value")%>' />
                                </td>
                                
                                <%-- <td class="text-right">
                                <input id="btnDelete" name="btnDelete" type="image" src="../img/del.png" width="50" height="50">
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>                        
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                 <tr><input id="btnDelete" name="btnDelete" type="image" runat="server" src="../img/del.png" width="50" height="50" align="right"></tr>
                </asp:Panel>
                
        </td>
         
        </tr>
      </table>
     
      <div class="row text-right">
       
  <div class="col-xs-2 col-xs-offset-8">
    <p>
      <strong>
        Net Weight Total : <br>
        Total : <br>
      </strong>
    </p>
  </div>
  <div class="col-xs-2">
    <strong>
      <%#DataBinder.Eval(Container.DataItem, "net_weight_value")%><br>
      <%#DataBinder.Eval(Container.DataItem, "Order_value")%><br>
    </strong>
  

  </div>
 <div class="mainc">
  
  <asp:Panel ID="hide" runat="server">
   <div class="row">
  
   
     
     <div id="div1" runat="server" class="col-md-10">
      <a href="#" id="myBtn" class="btn btn-success"><span class="glyphicon glyphicon-retweet"></span>Transferred</a>
               <asp:HiddenField ID="hfCustomerId"  runat="server" Value='<%# Eval("Cust_Code") %>' />
			   <asp:HiddenField ID="hforderno"  runat="server" Value='<%# Eval("Trans_sl_no") %>' />
			   <asp:HiddenField ID="hfstkcode"  runat="server" Value='<%# Eval("Stockist_Code") %>' />
         <asp:HiddenField ID="hfstkname"  runat="server" Value='<%# Eval("Stockist_Name") %>' />
                 <button id="submit"  type="button"  name="btncc" class="btn btn-primary" Value="1" >Order Confirm</button></div><%--<button type="button" class="btn btn-primary">Order Confirm</button>--%>
     <div id="div2" runat="server"  class="col-md-1">
         
           <div id="myForm" class="hide">
     
    <form  id="popForm" method="get">
        <div>
             <asp:HiddenField ID="HiddenField1"  runat="server" Value='<%# Eval("Cust_Code") %>' />
            <asp:HiddenField ID="hidodrnum"  runat="server" Value='<%# Eval("Trans_sl_no") %>' />
            <label for="about">Remarks:</label> <button onclick="$('.popover').hide();" class="btn btn-small btn-primary pull-right">Close</button>
              </a>
            <textarea rows="3" name="about" id="about" class="form-control input-md"></textarea>
            <br />
            <button id="send" type="button" class="btn btn-primary" data-loading-text="Sending info.." Value='<%# Eval("Cust_Code") %>'><em class="icon-ok"></em>Save</button>
        </div>
    </form>
</div>
         <button id="Cancel" name="btncc" type="button" class="btn btn-danger" data-container="body" data-toggle="popover" data-placement="top" title="Header" data-content=''>Order Reject</button><%--<button type="button" class="btn btn-danger">Order Reject</button>--%>
    
   </div>
   </div>
   </asp:Panel>

</div>
 
  <%--//----%>
   </ul>
       
    </div>
  </nav>
                </ItemTemplate>
            </asp:Repeater>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript">
                $("body").on("click", "[src*=plus]", function () {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
                    $(this).attr("src", "../Images/minus.png");
                });
                $("body").on("click", "[src*=minus]", function () {
                    $(this).attr("src", "../Images/plus.png");
                    $(this).closest("tr").next().remove();
                });
            </script>
            <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
            <script src='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
            <script src="../js/index.js" type="text/javascript"></script>
        </div>
        </form>
        <!-- Trigger/Open The Modal -->
        <!-- The Modal -->
        <div id="myModal" class="modal">
            <!-- Modal content -->
			<input type="hidden" id="custId" name="custId">
            <input type="hidden" id="ordno" name="ordno" >
            <input type="hidden" id="stkcd" name="stkcd" >
            <div class="modal-content">
                <div class="modal-header">
                    <span class="close">&times;</span>
                    <h3>
                        Order Transfer</h3>
                </div>
                <div>
                    <select id="DropDownList1" class="form-control">
                    </select>
                    <div align="center">
                        <input type="image" src="../img/iphone.png" style="width: 50px; height: 50px;" />
                    </div>
                    <select id="Select1" class="form-control">
                    </select>
                </div>
                <br />
                <div align="center">
                    <a href="#" class="btn btn-success" name="btntrans" id="btntran"><span class="glyphicon glyphicon-retweet">
                    </span>Transferred</a>
                </div>
            </div>
        </div>
        <script type="text/jscript">
            $(document).ready(function () {
                $(document).on('click', '#myBtn', function (e) {
				var slvals = [];
                    $("#tblCustomers [id*=chkRow]:checked").each(function () {
                        slvals.push($(this).closest('tr').children('td.rowVal').text());
                    });
					if (confirm("Do You Want Transfer this order to someother distributor")) {
                    modal.style.display = "block";
					var stkcd = $(this).closest("div").find("[id$='hfstkcode']").val();
                    var stknm = $(this).closest("div").find("[id$='hfstkname']").val();
					var ordno = $(this).closest("div").find("[id$='hforderno']").val();
                     var custid = $(this).closest("div").find("[id$='hfCustomerId']").val();
                    $("#DropDownList1").empty();
                    $("#DropDownList1").append($("<option></option>").val(stkcd).html(stknm));
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Today_Order.aspx/filldropdown",
                        data: "{}",
                        dataType: "json",
                        success: function (data) {
                            var dis = JSON.parse(data.d) || [];
                            var dist = $("#Select1").empty();
                            for (var i = 0; i < dis.length; i++) {
                                if (dis[i].Stockist_code != stkcd)
                                    dist.append($('<option value="' + dis[i].Stockist_code + '">' + dis[i].Stockist_Name + '</option>'));
                            }
                        }
                    });
					$("#custId").val(custid);
                        $("#ordno").val(ordno);
                        $("#stkcd").val(stkcd);
					}
					else {
                        alert("You have Cancelled.");
                    }
                });
            });
            // Get the modal
            var modal = document.getElementById('myModal');

            // Get the button that opens the modal
            var btn = document.getElementById("myBtn");

            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];

            // When the user clicks the button, open the modal 
            // btn.onclick = function () {
            //  modal.style.display = "block";
            // }

            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }

            // When the user clicks anywhere outside of the modal, close it
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        </script>
    </body>
    </html>
</asp:Content>
