<%@ Page Title="Productwise Sales Analysis" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_Sale.aspx.cs" Inherits="MIS_Reports_Product_Sale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Today_Order_View</title>
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
    <link type="text/css" rel="stylesheet" href="../../css/style1.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, Mode, sf_name) {

            if (Mode.trim() == "View All Remark(s)") {

                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rptRemarks.aspx?sf_code=" + sfcode + "&Month=" + fmon + "&Year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
    "ModalPopUp",
    "null," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
                popUpObj.focus();
                // LoadModalDiv();

            }

            else {

                popUpObj = window.open("Rpt_DCR_View.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&Mode=" + Mode + "&sf_name=" + sf_name,
    "ModalPopUp",
    "null," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
                popUpObj.focus();
                // LoadModalDiv();
            }
        }

</script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {

                var ddlMRName = $('#<%=ddlMR.ClientID%> :selected').text();
                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }
               
               
                if (ddlMRName != '') {

                    var ddlMR = document.getElementById('<%=ddlMR.ClientID%>').value;
                }

                var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

               

                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;

                var selectedvalue = $('#<%= rbnList.ClientID %> input:checked').val();

                if (ddlMR != -1 && ddlMR != 0 && ddlMRName != '') {

                    showModalPopUp(ddlMR, ddlMonth, ddlYear, selectedvalue, ddlMRName)
                }
                else {

                    showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, selectedvalue, FieldForce)
                }



            });
        }); 
    </script>
    <script type="text/jscript">
        $(document).ready(function () {
            document.getElementById('txtDate').valueAsDate = new Date();

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">

        <br />
        </div>
     
             <div class="container" style="width: 100%">
                    <div class="form-group">
                        <div class="row">

                         <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
                                Division</label>                    
                        <asp:Label ID="lblDivision"  runat="server" SkinID="lblMand" Text="Division "></asp:Label>                    
                     <div class="col-md-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150"  AutoPostBack="true"  onselectedindexchanged="subdiv_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                         <div class="row">

                         <label id="lblFF" runat="server" class="col-md-2 col-md-offset-3 control-label">
                                Field Force</label> 
                     
                <div class="col-md-5 inputGroupContainer">
                                <div class="input-group">
                     <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="form-control" Visible="false">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="350" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false" CssClass="form-control"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" >
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" >
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkVacant" Text=" Only Vacant Managers"  style="display:none"
                       OnCheckedChanged="chkVacant_CheckedChanged" runat="server" ForeColor="White" />

                   </div>
                   </div>
                            </div>
                      

                 <div class="row">

                         <label id="Label3" class="col-md-2 col-md-offset-3 control-label">
                                Year</label>                   

                        <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>

                   
                   
                        <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                   
                        <asp:Label ID="Label2" runat="server" SkinID="lblMand" Visible="false" Text="Year" ></asp:Label>
                       
                         <div class="col-md-5 inputGroupContainer">
                                <div class="input-group">
                                  <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                             <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired"  CssClass="form-control"
                                        Style="min-width: 100px"  Width="100" >
                        </asp:DropDownList>
                         <asp:Label ID="lblmode" runat="server" SkinID="lblMand" ForeColor="Blue" Font-Size="Medium"
                            Font-Bold="true" Font-Names="Calibri" Text="Select the Mode" Visible="false"></asp:Label>
                  
                        <asp:RadioButtonList ID="rbnList" CellSpacing="5" runat="server" Font-Names="Calibri"
                            RepeatDirection="Horizontal" RepeatColumns="3" Width="550px" Visible="false">
                      
  							<asp:ListItem Text="TPMyDayPlan" Selected="True"> TP MY Day Plan</asp:ListItem>
                        </asp:RadioButtonList>
                   </div>
                   </div>
                   </div>

                  
                       
                  
                     <div class="row">
                            <div class="col-md-6 col-md-offset-5">                             
                                      
                                       <asp:Button ID="btnSubmit" runat="server"   Text="View" class="btn btn-primary" style="width: 100px" onclick="btnSubmit_Click1"/>
                            </div>
                        </div>

           

           </div>
           </div>
           
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="60%">
            </asp:Table>      
        

        <div class="loading" align="center">
         
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>

