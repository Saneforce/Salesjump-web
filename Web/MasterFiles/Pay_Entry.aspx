<%@ Page Title="PayMent Entry" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Pay_Entry.aspx.cs" Inherits="Pay_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
     
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title></title>
       


  <script type="text/jscript">
      $(document).ready(function () {
          $('.datetimepicker').datepicker({ format: "yyyy/mm/dd" });
          $('#button').click(function () {
              alert("hi");
              var type = $('#<%=Dis_name.ClientID%> :selected').text();
              if (type == "--Select--") { alert("Select Distributor Name."); $('#Dis_name').focus(); return false; }

          });
      });
       </script>
    </head>
    <body>
        <div class="container">
            <div class="col-lg-9">
                <form class="form-horizontal" action=" " method="post" id="reg_form" runat="server">
                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                  </asp:ScriptManager>
                <fieldset>
                    <!-- Form Name -->
                    <legend></legend>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Field Force Name</label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList  CssClass="form-control" name="sf_Name" AutoPostBack="true" 
                                        ID="sf_Name" runat="server" onselectedindexchanged="sf_Name_SelectedIndexChanged">
                                       
                                    </asp:DropDownList>
                               
                                
                            </div>
                        </div>
                    </div>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Distributor Name</label>0
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                               <asp:DropDownList runat="server" CssClass="form-control" name="Dis_name" 
                                        ID="Dis_name" onselectedindexchanged="Dis_name_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Route</label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-road"></i></span>
                              <asp:DropDownList runat="server" CssClass="form-control" name="Rou" 
                                        ID="Rou" onselectedindexchanged="Cus_name_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Customer Name</label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                  <asp:DropDownList runat="server" CssClass="form-control" name="cus" 
                                   ID="cus">
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Amount</label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-rupee" style="font-size:20px;padding-left:5px;"></i></span>
                               
                                 <asp:TextBox ID="Txt_Amout" runat="server" style="padding:3px 15px;" TextMode="Number" MaxLength="10"  CssClass="form-control" type="text" Rows="3" placeholder="Amount" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!-- Select Basic -->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Mode Of Payment</label>
                        <div class="col-md-6 selectContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                                 <asp:DropDownList  CssClass="form-control" name="DDL_type"  
                                        ID="DDL_type" runat="server">
                                       
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Payment Date</label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group date">
<span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                                <asp:TextBox ID="Txt_Date" runat="server" style="padding:3px 15px;"   CssClass="form-control" type="text" Rows="3" TextMode="Date" placeholder="Date" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                             
                            </div>
                        </div>
                    </div>
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Ref.No</label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-home"></i></span>
                                
                                <asp:TextBox ID="Txt_ref_No" runat="server" style="padding:3px 15px;" MaxLength="20"  CssClass="form-control" type="text"  placeholder="Ref.No"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!-- Text area -->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Remarks
                        </label>
                        <div class="col-md-6  inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-pencil"></i></span>
                               
                                <asp:TextBox ID="Txt_Remark" runat="server" style="padding:3px 15px;"  MaxLength="50"  CssClass="form-control" type="text" placeholder="Remarks"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
                    <!-- Button -->
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                        </label>
                        <div class="col-md-4">
                          
                            <asp:Button runat="server" Text="Submit" id="button" type="submit" class="btn btn-info"
                                CssClass="btn btn-info" ValidationGroup="g1" onclick="button_Click"></asp:Button>
                        </div>
                        <div class="col-md-4">
                            <asp:Button runat="server" Text="Clear" id="button1" type="Clear" class="btn btn-info"
                                CssClass="btn btn-info" onclick="button1_Click" ></asp:Button>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label">
                        </label>
                        
                    </div>
                </fieldset>
                </form>
            </div>
        </div>
      <%--  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
        <script src="js/jQuery-2.2.0.min.js" type="text/javascript"></script>
        <link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/jquery.bootstrapvalidator/0.5.3/css/bootstrapValidator.min.css" />
        <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jquery.bootstrapvalidator/0.5.3/js/bootstrapValidator.min.js" />--%>
   
  
    </body>
    </html>
</asp:Content>
