<%@ Page Title="My Day Plan" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DCR_My_Date_Plan.aspx.cs" Inherits="Reports_DCR_My_Date_Plan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- Website CSS style -->
        <link href="../../css/StyleReg.css" rel="stylesheet" />
        <!-- Website Font style -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css">
        <!-- Google Fonts -->
        <link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css'>
        <link href='https://fonts.googleapis.com/css?family=Oxygen' rel='stylesheet' type='text/css'>

     

    </head>
    <body>
     
        <div class="container">
            <div class="row main">
                <div class="main-login main-center">
                    <h3>My Day Plan Entry</h3>
                    <form runat="server" class="" method="post" action="#">
                        <!-- Split button -->
                       
               
                        <div class="form-group">

                            <label for="name" class="cols-sm-2 control-label">Work Type</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-list fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList runat="server" CssClass="form-control selectpicker" name="name" ID="name" data-validation="required">
                                     
                                    </asp:DropDownList>
                                  
                                </div>
                                 <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" Display="Dynamic" 
                                    ValidationGroup="g1" runat="server" ControlToValidate="name"
                                    Text="*Please Select WorkType"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group">
                            <label for="email" class="cols-sm-2 control-label">Field Force</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-home fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList runat="server" CssClass="form-control" name="email" AutoPostBack="true" 
                                        ID="email" onselectedindexchanged="email_SelectedIndexChanged1">
                                       
                                    </asp:DropDownList>
                                </div>
                                 <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                                    ValidationGroup="g1" runat="server" ControlToValidate="email"
                                    Text="*Please Select Head Quarters"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="username" class="cols-sm-2 control-label">Distributors</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList runat="server" CssClass="form-control" name="username" 
                                        ID="username" onselectedindexchanged="username_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                               
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="password" class="cols-sm-2 control-label">Route</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-road fa-lg" aria-hidden="true"></i></span>
                                    <asp:DropDownList runat="server" CssClass="form-control" name="password" 
                                        ID="password">
                                    </asp:DropDownList>
                                </div>
                               
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="confirm" class="cols-sm-2 control-label">Remarks</label>
                            <div class="cols-sm-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-pencil fa-lg" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="Txt_Remark" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" placeholder="Enter your Remarks"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Button runat="server" Text="Submit" id="button" 
                                CssClass="btn btn-primary btn-lg btn-block login-button" ValidationGroup="g1" onclick="button_Click"></asp:Button>
                        </div>

                    </form>
                </div>
            </div>
        </div>

        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
       
        <!-- Include all compiled plugins (below), or include individual files as needed -->

        <script src="../../js/bootstrap.js" type="text/javascript"></script>
  
    </body>
    </html>

</asp:Content>
