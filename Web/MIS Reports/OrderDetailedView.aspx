<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="OrderDetailedView.aspx.cs" Inherits="MIS_Reports_OrderDetailedView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title>DCR View</title>
            <link type="text/css" rel="stylesheet" href="../../css/style.css" />

            

            <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
            <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
            <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    var div_code = '<%=Session["div_code"]%>';
                    //alert(div_code);

                    $('#<%=txtFrom.ClientID%>').datepicker({
                        dateFormat: 'dd/mm/yy'
                    })
                    $('#<%=ttxtdate.ClientID%>').datepicker({
                        dateFormat: 'dd/mm/yy'

                    });

                    $('.txtdate').show();
                     if (div_code == "98") {
                        $('.txtdate').show();
                        $('.txtMonth').hide();
                        $('.exceldld').show();
                    }
                    else {
                        $('.txtdate').show();
                        $('.txtMonth').hide();
                        $('.exceldld').hide();
                    }

                    $(document).on('click', '#btnView', function () {
                        var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                        if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                        var Fyear = ""; FMonth = "";
                         <%--Fyear = $("#<%=ddlFYear.ClientID%>").val();
                        FMonth = $("#<%=ddlFMonth.ClientID%>").val();--%>
                        var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                        var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();

                        if ($('#<%=txtFrom.ClientID%>').val() == "") {
                                alert("Please select From Date.");
                                $('#<%=txtFrom.ClientID%>').focus();
                                return false;
                            }
                            if ($('#<%=ttxtdate.ClientID%>').val() == "") {
                                alert("Please select To Date.");
                                $('#<%=ttxtdate.ClientID%>').focus();
                                return false;
                            }

                        if (div_code == "98") {

                            if ($('#<%=txtFrom.ClientID%>').val() == "") {
                                alert("Please select From Date.");
                                $('#<%=txtFrom.ClientID%>').focus();
                                return false;
                            }
                            if ($('#<%=ttxtdate.ClientID%>').val() == "") {
                                alert("Please select To Date.");
                                $('#<%=ttxtdate.ClientID%>').focus();
                                return false;
                            }

                            //var date = $("#txtdate").val();
                            //if (date == "") { alert("Select Date"); $('#txtdate').focus(); return false; }
                            //var tdate = $("#ttxtdate").val();
                            //if (tdate == "") { alert("Select Date"); $('#ttxtdate').focus(); return false; }
                        }

                        if (div_code == "98") {

                            Fyear = $('#<%=txtFrom.ClientID%>').val();
                            FMonth = $('#<%=ttxtdate.ClientID%>').val();

                            strOpen = 'rptOrderDetailedView.aspx?&SFCode=' + sf_code + '&FYear=' + Fyear + '&FMonth=' + FMonth + '&SFName=' + SFName + '&div_code=' + div_code + '&SubDiv=' + SubDivCode;
                        }
                        else {
                              Fyear = $('#<%=txtFrom.ClientID%>').val();
                            FMonth = $('#<%=ttxtdate.ClientID%>').val();
                            strOpen = 'rptOrderDetailedView.aspx?&SFCode=' + sf_code + '&FYear=' + Fyear + '&FMonth=' + FMonth + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                        }
                        //alert(strOpen);
                        window.open(strOpen, 'OrderDetailedView', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

                    });


                    $(function () {
                        var dtToday = new Date();

                        var month = dtToday.getMonth() + 1;
                        var day = dtToday.getDate();
                        var year = dtToday.getFullYear();

                        if (month < 10)
                            month = '0' + month.toString();
                        if (day < 10)
                            day = '0' + day.toString();

                        var maxDate = year + '-' + month + '-' + day;

                        $('#ttxtdate').attr('max', maxDate);

                        $('#txtdate').attr('max', maxDate);
                    });

                });
            </script>

        </head>
        <body>
            <form id="form1" runat="server">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <div>
                    <div id="Divid" runat="server">
                        <br />
                    </div>

                    <div class="container" style="width: 100%">
                        <center>
                             <table cellpadding="0" cellspacing="5">
                                 <tr>
                                     <td>
                                         <div class="row">
                                             <label id="Label2" class="col-md-3  col-md-offset-3  control-label">
                                                 Division</label>
                                             <div class="col-sm-2 inputGroupContainer">
                                                 <div class="input-group">
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                                     <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                                         Style="min-width: 100px;width:120px" OnSelectedIndexChanged="subdiv_SelectedIndexChanged" 
                                                         AutoPostBack="true">
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                         </div>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <div class="row">
                                             <label for="ddlFF" class="col-md-3 col-md-offset-3 control-label">
                                                 Field Force</label>
                                             <div class="col-md-5 inputGroupContainer">
                                                 <div class="input-group">
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                                     <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control" Width="350">
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                         </div>
                                     </td>
                                 </tr>
                                 <tr class="hidden">
                                     <td>
                                         <div class="row txtMonth">
                                             <label for="txtMonth" class="col-md-3  col-md-offset-3  control-label">
                                                 Month</label>
                                             <div class="col-md-2 inputGroupContainer">
                                                 <div class="input-group">
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                     <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                                         Style="min-width: 100px; width:120px">
                                                         <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                         <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                         <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                         <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                         <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                         <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                         <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                         <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                         <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                         <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                         <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                         <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                         </div>
                                     </td>
                                 </tr>
                                 <tr class="hidden">
                                     <td>
                                         <div class="row txtMonth">
                                             <label for="txtYear" class="col-md-3 col-md-offset-3  control-label">
                                                 Year</label>
                                             <div class="col-md-2 inputGroupContainer">
                                                 <div class="input-group">
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                     <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control" 
                                                         Style="min-width: 100px; width:120px">
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                         </div>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <div class="row txtdate stylespc">
                                             <label for="txtFromDate" class="col-md-3 col-md-offset-3  control-label">From Date</label>
                                             <div class="col-md-5 inputGroupContainer">
                                                 <div class="input-group">
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                     <asp:TextBox ID="txtFrom" runat="server" autocomplete="off" DataFormatString="{dd/MM/yyyy}" CssClass="form-control">
                                                     </asp:TextBox>
                                                    
                                                 </div>
                                             </div>
                                         </div>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <div class="row txtdate stylespc">
                                             <label for="txtFromDate" class="col-md-3 col-md-offset-3  control-label">To Date</label>
                                             <div class="col-md-5 inputGroupContainer">
                                                 <div class="input-group">
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                     <asp:TextBox ID="ttxtdate" runat="server" autocomplete="off" DataFormatString="{dd/MM/yyyy}" CssClass="form-control">                                
                                                     </asp:TextBox>
                                                     
                                                 </div>
                                             </div>
                                         </div>
                                     </td>
                                 </tr>
                             </table>
                            <br />
                            <br />
                            <div class="row col-md-offset-5" style="margin-top: 5px">
                                <div class="col-md-2">
                                     <a id="btnView" class="btn btn-primary" style="background-color:#1a73e8;">View</a>
                                    
                                </div>
                                <div class="col-md-3 exceldld">
                                    <asp:Button runat="server" ID="exceldld" CssClass="btn btn-primary" BackColor="#1a73e8" 
                                        ForeColor="White" Text="Excel" OnClientClick="return validation()" OnClick="exceldld_Click" />
                                </div>
                            </div>

                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="60%">
                            </asp:Table>
                        </center>
                    </div>

                </div>

            </form>
        </body>
    </html>
</asp:Content>





<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="OrderDetailedView.aspx.cs" Inherits="MIS_Reports_OrderDetailedView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('click', '#btnView', function () {
                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
                var SubDivCode = $("#<%=subdiv.ClientID%>").val();
                var SFName = $("#<%=ddlFieldForce.ClientID%>  option:selected").text();

                strOpen = 'rptOrderDetailedView.aspx?&SFCode=' + sf_code + '&FYear=' + Fyear + '&FMonth=' + FMonth + '&SFName=' + SFName + '&SubDiv=' + SubDivCode;
                window.open(strOpen, 'OrderDetailedView', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

            });
        });
    </script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%">
            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <div class="col-sm-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px;width:120px" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server"
                            CssClass="form-control" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtMonth" class="col-md-2  col-md-offset-3  control-label">
                    Month</label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px; width:120px">
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtYear" class="col-md-2 col-md-offset-3  control-label">
                    Year</label>
                <div class="col-md-2 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style="min-width: 100px; width:120px">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnView" class="btn btn-primary"
                        style="vertical-align: middle; width: 100px;">View</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>--%>

