<%@ Page Title="HO Creation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Menu_Hide.aspx.cs" Inherits="Reports_Menu_Hide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
  <%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- Website CSS style -->
        <link href="../css/StyleReg.css" rel="stylesheet" />
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
                    <h3>Menu Controller</h3>
                    <form runat="server" class="" method="post" action="#">
                        <!-- Split button -->
                       
               
                        <div class="form-group">

                            <label for="name" class="cols-sm-2 control-label">User Name</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-list fa" aria-hidden="true"></i></span>
                                 
                                    <asp:TextBox runat="server" CssClass="form-control selectpicker" name="name" ID="Txt_User_Name" data-validation="required"></asp:TextBox>
                                  
                                </div>
                                 <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" Display="Dynamic" 
                                    ValidationGroup="g1" runat="server" ControlToValidate="Txt_User_Name"
                                    Text="*Please Enter Username"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group">
                            <label for="email" class="cols-sm-2 control-label">Password</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-home fa" aria-hidden="true"></i></span>
                                         <asp:TextBox runat="server" CssClass="form-control selectpicker" name="Pass" ID="Txt_Password" TextMode="Password" data-validation="required"></asp:TextBox>
                                </div>
                                 <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic" 
                                    ValidationGroup="g1" runat="server" ControlToValidate="Txt_Password"
                                    Text="*Please Enter Password!!!"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                   

                        <div class="form-group">
                            <label for="confirm" class="cols-sm-2 control-label">Select Menu</label>
                            <div class="cols-sm-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-pencil fa-lg" aria-hidden="true"></i></span>
                                    <asp:CheckBoxList ID="Chk_Menu" runat="server" DataTextField="Menu_Name"
                            CssClass="form-control" DataValueField="Menu_code" Font-Names="Verdana"
                            Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="4"
                            RepeatDirection="vertical" Width="753px" TabIndex="29">
                        </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Button runat="server" Text="Control" id="button" 
                                CssClass="btn btn-primary btn-lg btn-block login-button" 
                                ValidationGroup="g1" onclick="button_Click" ></asp:Button>
                        </div>

                    </form>
                </div>
            </div>
        </div>

      

        <script src="../../js/bootstrap.js" type="text/javascript"></script>
  
    </body>
    </html>

</asp:Content>
