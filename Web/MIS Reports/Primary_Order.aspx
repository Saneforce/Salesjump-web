<%@ Page  Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Primary_Order.aspx.cs" Inherits="MIS_Reports_Primary_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Bootstrap custom collapse table</title>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>--%>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
        <%--  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>
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
                __doPostBack("<%=UniqueID%>");
            }


            $(document).ready(function () {





                $("a[name = 'btntrans']").click(function () {


                  
                    location.reload();
                    var pSelect = "";
                    var pSelect_name = "";
                    var order_no = "";
                    var arr = new Array();
                    arr[0] = $('#submit').val().toString()
                    arr[1] = $('#<%= ddl_dis.ClientID %>').val();
                    pSelect_name = $('#<%= Select1.ClientID %>').text();
                    pSelect = $('#<%= Select1.ClientID %>').val();
                    order_no = $("#Lab_order_no").text();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Primary_Order.aspx/savedata",
                        data: "{'data':'" + arr + "','dd':'" + pSelect + "','ddN':'" + pSelect_name + "','order_no':'" + order_no + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == "Sucess")
                                alert("Order has been updated Successfully");
                            else
                                alert("Order updation Successfull");
                        },
                        error: function (result) {
                            // alert(JSON.stringify(result));
                            alert("Order updation Successfull");
                        }
                    });

                });
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
        <script type="text/jscript">
            $(document).ready(function () {
                var date = $("#txtDate").val();
                if (date == "")
                    document.getElementById('txtDate').valueAsDate = new Date();

            });
        </script>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
        <script type="text/javascript">
            $(function () {
                $('input[name="btnDelete"]').click(function (e) {
                    var row = $(this).closest('tr');
                    row.remove()
                })
            })
        </script>
        <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
        <script type="text/javascript">
            function chkAllProducts(x) {
                $tb = $(x).closest('table').find('tbody');
                $($tb[0]).find('input[type="checkbox"]').each(function () { if ($(x).attr('checked')) $(this).attr('checked', 'checked'); else $(this).removeAttr('checked'); });



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
                //  document.getElementById('txtDate').valueAsDate = new Date();

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

                    window.location = "../Pri_Invoice_Entry.aspx?Or_Date=" + encodeURIComponent($("#txtDate").val()) + "&Dis_code=" + encodeURIComponent($('#<%=ddl_dis.ClientID%> option:selected').val()) + "&Ord_code=" + encodeURIComponent($(this).parent('div').next('[id$=Label3]').text());

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
        <div>
            <!-- ROW 1 -->
            <asp:Repeater ID='Repeater1' runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <nav role="navigation">
    <div  id="bs-example-navbar-collapse-1" >
      <ul >
     <div class="panel panel-default">
    <div class="panel-body" id='h<%# DataBinder.Eval(Container, "ItemIndex") %>' class="header"
                    onclick='ToggleDisplay(<%# DataBinder.Eval(Container, "ItemIndex") %>);' ></h4> <img alt="" style="cursor: pointer" src="../Images/plus.png" /><span style="color: Green; font-weight: bold;font-size:medium;padding-left:10px""><%# Eval("Stockist_Name")%></span><span style="color: Red; font-weight: bold;font-size:medium;padding-left:8px"">(Order No :<%# Eval("Order_No") %>)</span><asp:Image ID="Image1" runat="server" ImageUrl="../img/cf.png"  style="height:50px; width:100px;"></asp:Image><button type="button"  class="btn btn-success pleasewait" style="float: right;padding:6px 21px;">Convert To Invoice</button><span style="color: Black; font-weight: bold;font-size:x-large;padding: 8px;float: right;"><%#DataBinder.Eval(Container.DataItem, "Order_value", "{0:N2}")%></span></div><asp:Label ID="Label3" runat="server" style="display:none;"  Visible="true"  Text='<%# Eval("Order_No") %>'></asp:Label>
    <div class="panel-heading">
     <div class="row">
        <div class="col-sm-3">
            <div class="center-block" ><center>Order Taken By</center>
           
         <span style="color: Black; font-weight: bold;font-size:medium;font-size: 11px"><center><%# Eval("Sf_Name")%></center></span> 

         </div>
        </div>
        <div class="col-sm-4">
            <div class="center-block" ><center>Payment Type</center>
           
         <span style="color: Black; font-weight: bold;font-size:medium;font-size: 11px"><center><%# Eval("Pay_Type")%></center></span>
            </div>
        </div>
         <div class="col-sm-4">
            <div class="center-block" ><center>Collected Amount</center>
            
         <span style="color: Black; font-weight: bold;font-size:medium;font-size: 11px"><center><%#DataBinder.Eval(Container.DataItem, "Collected_Amount")%></center></span>
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
                               
                             
                                <th scope="col" style="width: 50px">
                                        S.No
                                    </th>
                                   
                                    <th scope="col" style="width: 150px">
                                        Product Name
                                    </th>
                                    <th scope="col" style="width: 150px">
                                       CQty
                                    </th>
                                    <th scope="col" style="width: 150px">
                                       PQty
                                    </th>

                                   <th scope="col" style="width: 150px">
                                       Value
                                    </th>
                                   
                                    
                                </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                             
                    
                               <td>
                                <asp:Label ID="Label2" runat="server" Text='<%# Container.ItemIndex + 1 %>' />
                               </td>
                                <td class="rowVal1" style="display:none;">
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Stockist_Code")%>' />
                               </td>
                            <td  class="rowVal" style="display:none;">
                            <asp:Label ID="pro_code" runat="server" Text='<%# Eval("Product_Code")%>'  />
                            </td>
                                <td >
                                    <asp:Label ID="lbl_Prod_name" runat="server" Text='<%# Eval("Product_Name")%>' />
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("CQty")%>' />
                                </td>
                                 <td class="text-right">
                                    <asp:Label ID="Lab_net" runat="server" Text='<%# Eval("PQty")%>' />
                                </td>
                              
                                 <td class="text-right">
                                    <asp:Label ID="Lab_val" runat="server" Text='<%# Eval("value")%>' />
                                </td>
                                
                              
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>                        
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                 
                </asp:Panel>
                
        </td>
         
        </tr>
      </table>
    <%--<asp:Image ID="Image1" runat="server" ImageUrl="../img/cf.png"  style="height:80px; width:150px;"></asp:Image>--%>
     
      <div class="row text-right">
       
  <div class="col-xs-2 col-xs-offset-8">
    <p>
      <strong>
        Collected Amount : <br>
        Total : <br>
      </strong>
    </p>
  </div>
  <div class="col-xs-2">
    <strong>
      <%#DataBinder.Eval(Container.DataItem, "Collected_Amount")%><br>
      <%#DataBinder.Eval(Container.DataItem, "Order_value")%><br>
    </strong>
  

  </div>
 <div class="mainc">
  
  <asp:Panel ID="hide" runat="server">
   <div class="row">
  
   
     
     <div id="div1" runat="server" class="col-md-10">
     
                <asp:HiddenField ID="hfCustomerId"  runat="server" Value='<%# Eval("Stockist_Code") %>' />
                 <button id="submit"  type="button"  name="btncc" class="btn btn-primary" Value="1" >Order Confirm</button></div><%--<button type="button" class="btn btn-primary">Order Confirm</button>--%>
    
   
   </div>
   </div>
   </asp:Panel>
   <div id="myForm" class="hide">
     
    <form  id="popForm" method="get">
        <div>
             
            <label for="about">Remarks:</label> <button onclick="$('#meddelanden').popover('hide');" class="btn btn-small btn-primary pull-right">Close</button>&times;
              </a>
            <textarea rows="3" name="about" id="about" class="form-control input-md"></textarea>
            <br />
            <button id="send" type="button" class="btn btn-primary" data-loading-text="Sending info.." ><em class="icon-ok"></em>Save</button>
        </div>
    </form>
</div>
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
        <%--<button id="myBtn">Open Modal</button>--%>
        <!-- The Modal -->
        <div id="myModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header">
                    <span class="close">&times;</span>
                    <h3>
                        Supplier Master</h3>
                </div>
                <div>
                    <select id="DropDownList1" runat="server" class="form-control" visible="false">
                    </select>
                    <div align="center">
                        <input type="image" src="../img/iphone.png" style="width: 50px; height: 50px;" />
                    </div>
                    <select id="Select1" runat="server" class="form-control">
                    </select>
                </div>
                <br />
                <div align="center">
                    <a href="#" class="btn btn-success" name="btntrans"><span class="glyphicon glyphicon-retweet">
                    </span>Order Confirm</a>
                </div>
            </div>
        </div>
        <script type="text/jscript">
            $(document).ready(function () {
                $(document).on('click', '#submit', function (e) {
                    modal.style.display = "block";
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
