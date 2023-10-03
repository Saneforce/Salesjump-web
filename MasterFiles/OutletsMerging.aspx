<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="OutletsMerging.aspx.cs" Inherits="MasterFiles_OutletsMerging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title>Outlets Merging</title>
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        </head>
        <body>
            <form id="form1" runat="server">
                <div class="card" style="width:100%">
                    <div class="card-body">
                        <div class="row">
                             <div class="col-md-6" style="float:left;">
                                 <div class="col-md-3" style="padding-left:35px;">
                                     <label>Field Force</label>
                                 </div>
                                 <div class="col-md-3" style="width:70%">
                                     <select id="ddlfieldforce" class="form-control ddlfieldforce"></select>
                                 </div>
                             </div>
                             <div class="col-md-6" style="float:right;">
                                 <div class="col-md-3" style="padding-left:35px;">
                                     <label>Route</label>
                                 </div>
                                 <div class="col-md-3" style="width:70%">
                                     <select id="ddlroute" class="form-control ddlroute"></select>
                                 </div>
                             </div>                            
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-6" style="float:left;">
                                <div class="col-md-3" style="padding-left:35px;">
                                    <label>From Retailers</label>
                                </div>
                                <div class="col-md-3" style="width:70%">                                
                                    <select id="ddlfromretailer" class="form-control ddlfromretailer"></select>
                                </div>  
                            </div>
                            <div class="col-md-6" style="float:right;">
                                <div class="col-md-3" style="padding-left:50px;" >
                                    <label>To Retailers</label>
                                </div>
                                <div class="col-md-3" style="width:70%;">                                
                                    <select id="ddltoretailer" class="form-control ddltoretailer"></select>
                                </div>    
                            </div>
                        </div>
                        <br />                        
                    </div>
                    <br />
                    <div class="col-md-12" style="width:100%">
                         <div class="col-md-6" style="float:left;width:50%">
                            <table id="fromretailertbl" class="table table-responsive">
                                <thead>
                                    <tr>
                                        
                                        <th>Retailer Code</th>
                                        <th>Retailer Name</th>
                                        <th>Address</th>
                                        <th>Created Date</th>                                     
                                       
                                        <th>Status</th>
                                        <th>OrderCount</th>
                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
                        <div class="col-md-6" style="float:right;width:50%">
                            <table id="toretailertbl" class="table table-responsive">
                                <thead>
                                    <tr>
                                        
                                        <th>Retailer Code</th>
                                        <th>Retailer Name</th>
                                        <th>Address</th>
                                        <th>Created Date</th>                                        
                                        <th>Status</th>
                                        <th>OrderCount</th>
                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
                    </div>
                    
                    <div class="card-footer">
                        <div class="row">
                            <div class="col-md-6 col-md-offset-5">
                                <button type="button" class="btn btn-primary col-md-offset-2" style="margin-top: 20px; background-color:#19a4c6;font-weight:bold;" id="btnsave">Save</button>
                            </div>
                        </div>                        
                    </div>
                        
                </div>
            </form>

            <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script> 
            <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

            <script type="text/javascript">
                let divcode = '';
                let routeParsed = []; //var fromretaile = new Array();   
                divcode = Number('<%=Session["div_code"]%>');

                function fillFieldForce() {

                    $('#ddlroute').empty();
                    $('#ddlroute').append('<option value="0">--Select--</option>');

                    $('#ddlfromretailer').empty();
                    $('#ddlfromretailer').append('<option value="0">--Select--</option>');

                    $('#ddltoretailer').empty();
                    $('#ddltoretailer').append('<option value="0">--Select--</option>');

                    var filtmgr = []; 
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "OutletsMerging.aspx/GetSalesForceDetails",
                        data: "{'divcode':'" + <%=Session["div_code"]%> + "'}",
                        dataType: "json",
                        success: function (data) {
                            filtmgr = JSON.parse(data.d) || [];

                            $('#ddlfieldforce').empty();
                            $('#ddlfieldforce').append('<option value="0">--Select--</option>');
                            if (filtmgr.length > 0) {
                                for (var j = 0; j < filtmgr.length; j++) {
                                    $('#ddlfieldforce').append('<option value="' + filtmgr[j].Sf_Code + '">' + filtmgr[j].Sf_Name + '</option>');
                                }
                            }
                        }
                    });      
                    $('#ddlfieldforce').chosen();
                }

                function BindRoute(ddlfieldforce) {
                    var FOName = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "OutletsMerging.aspx/GetRouteDetails",
                        data: "{'SfCode':'" + ddlfieldforce + "','divcode':'" + <%=Session["div_code"]%> + "'}",
                        dataType: "json",
                        success: function (data) {
                            FOName = JSON.parse(data.d) || [];
                            console.log(FOName);

                            $('#ddlroute').empty();
                            $('#ddlroute').append('<option value="0">--Select--</option>');

                            if (FOName.length > 0) {
                                for (var j = 0; j < FOName.length; j++) {

                                    $('#ddlroute').append('<option value="' + FOName[j].Territory_Code + '">' + FOName[j].Territory_Name + '</option>');                                    
                                }
                            }
                        }
                    });

                    //$('#ddlfromretailer').chosen();
                    //$('#ddltoretailer').chosen();
                }

                function BindRetailer(ddlroute) {
                    var FOName = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "OutletsMerging.aspx/GetRetailerList",
                        data: "{'Territory_Code':'" + ddlroute + "','divcode':'" + <%=Session["div_code"]%> + "'}",
                        dataType: "json",
                        success: function (data) {
                            FOName = JSON.parse(data.d) || [];
                            console.log(FOName);

                            $('#ddlfromretailer').empty();
                            $('#ddlfromretailer').append('<option value="0">--Select--</option>');

                            $('#ddltoretailer').empty();
                            $('#ddltoretailer').append('<option value="0">--Select--</option>');

                            if (FOName.length > 0) {
                                for (var j = 0; j < FOName.length; j++) {

                                    $('#ddlfromretailer').append('<option value="' + FOName[j].ListedDrCode + '">' + FOName[j].ListedDr_Name + '</option>');
                                    $('#ddltoretailer').append('<option value="' + FOName[j].ListedDrCode + '">' + FOName[j].ListedDr_Name + '</option>');
                                }
                            }
                        }
                    });

                    //$('#ddlfromretailer').chosen();
                    //$('#ddltoretailer').chosen();
                }

                function BindFromRetailerDetails(ddlfromretailer) {
                    var FOName = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "OutletsMerging.aspx/GetRetailerDetails",
                        data: "{'DivCode':'" + divcode + "','ListedDrCode':'" + ddlfromretailer + "'}",
                        dataType: "json",
                        success: function (data) {
                            FOName = JSON.parse(data.d) || [];
                            console.log(FOName);
                            $("#fromretailertbl TBODY").html("");

                            if (FOName.length > 0) {
                                tr = $("<tr></tr>");
                                for (var j = 0; j < FOName.length; j++) {
                                    tr = $("<tr></tr>");
                                    $(tr).html("<td>" + FOName[j].ListedDrCode + "</td><td>" + FOName[j].ListedDr_Name + "</td><td>" + FOName[j].ListedDr_Address1 + "</td><td>" + FOName[j].ListedDr_Created_Date + "</td><td>" + FOName[j].ListedDr_Active_Flag + "</td><td>" + FOName[j].OrderCount + "</td>");
                                    $("#fromretailertbl TBODY").append(tr);
                                }
                            }
                        }
                    });
                }

                function BindToRetailerDetails(ddltoretailer) {
                    var FOName = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "OutletsMerging.aspx/GetRetailerDetails",
                        data: "{'DivCode':'" + divcode + "','ListedDrCode':'" + ddltoretailer + "'}",
                        dataType: "json",
                        success: function (data) {
                            FOName = JSON.parse(data.d) || [];
                            console.log(FOName);

                            $("#toretailertbl TBODY").html("");

                            if (FOName.length > 0) {
                                tr = $("<tr></tr>");
                                for (var j = 0; j < FOName.length; j++) {
                                    tr = $("<tr></tr>");
                                    $(tr).html("<td>" + FOName[j].ListedDrCode + "</td><td>" + FOName[j].ListedDr_Name + "</td><td>" + FOName[j].ListedDr_Address1 + "</td><td>" + FOName[j].ListedDr_Created_Date + "</td><td>" + FOName[j].ListedDr_Active_Flag + "</td><td>" + FOName[j].OrderCount + "</td>");
                                    $("#toretailertbl TBODY").append(tr);
                                }
                            }
                        }
                    });
                }

                $(document).ready(function () {
                    divcode = Number('<%=Session["div_code"]%>');

                    fillFieldForce(); //fillRoute();                 
                   
                    var ddlfieldforce = $('#ddlfieldforce').val();

                    if ((ddlfieldforce == "0" || ddlfieldforce == null))
                    { ddlfieldforce = "0"; }

                    if (ddlfieldforce != "0") { BindRoute(ddlfieldforce); }

                    $('#ddlfieldforce').on('change', function () {
                        
                        var ddlfieldforce = $('#ddlfieldforce').val();

                        if ((ddlfieldforce == "" || ddlfieldforce == null))
                        { ddlfieldforce = "0"; }

                        $('#ddlroute').empty();
                        $('#ddlroute').append('<option value="0">--Select--</option>');
                                              

                        $('#ddlroute').chosen('destroy');  
                       
                        if (ddlfieldforce != "0")
                        {                     
                            BindRoute(ddlfieldforce);
                            
                            $('#ddlroute').chosen('refresh');
                           
                        }
                    });   

                    $('#ddlroute').on('change', function () {
                        
                        var ddlroute = $('#ddlroute').val();

                        if ((ddlroute == "" || ddlroute == null)) { ddlroute = "0"; }

                        $('#ddlfromretailer').empty();
                        $('#ddlfromretailer').append('<option value="0">--Select--</option>');

                        $('#ddltoretailer').empty();
                        $('#ddltoretailer').append('<option value="0">--Select--</option>');

                        $("#fromretailertbl TBODY").html("");
                        $("#toretailertbl TBODY").html("");

                        $('#ddlfromretailer').chosen('destroy');
                        $('#ddltoretailer').chosen('destroy');

                        if (ddlroute != "0") {
                            BindRetailer(ddlroute);

                            $('#ddlfromretailer').chosen('refresh');
                            $('#ddltoretailer').chosen('refresh');
                        }
                    });

                    $('#ddlfromretailer').on('change', function () {

                        var ddlfromretailer = $('#ddlfromretailer').val();

                        if ((ddlfromretailer == "" || ddlfromretailer == null)) { ddlfromretailer = "0"; }

                        if (ddlfromretailer != "0") { BindFromRetailerDetails(ddlfromretailer); }
                    });

                    $('#ddltoretailer').on('change', function () {

                        var ddltoretailer = $('#ddltoretailer').val();

                        if ((ddltoretailer == "" || ddltoretailer == null)) { ddltoretailer = "0"; }

                        if (ddltoretailer != "0") { BindToRetailerDetails(ddltoretailer); }
                    });

                    $('#btnsave').on('click', function () {
                        var SfCode = $('#ddlfieldforce').val();
                        if (SfCode == "0") {
                            alert("Select From Field Force");
                            $('#ddlfieldforce').focus();
                            return false;
                        }

                        var SfRoute = $('#ddlroute').val();
                        if (SfRoute == "0") {
                            alert("Select Route");
                            $('#ddlroute').focus();
                            return false;
                        }

                        var fromretailer = $('#ddlfromretailer').val();
                        if (fromretailer == "0") {
                            alert("Select From Retailers !!");
                            $('#ddlfromretailer').focus();
                            return false;
                        }

                        var toretailer = $('#ddltoretailer').val();
                        if (toretailer == "0") {
                            alert("Select To Retailers !!");
                            $('#ddltoretailer').focus();
                            return false;
                        }

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "OutletsMerging.aspx/saveOutletMerging",
                            data: "{'divcode':'" + divcode + "','SfCode':'" + SfCode + "','SfRoute':'" + SfRoute + "','fromretailer':'" + fromretailer + "','toretailers':'" + toretailer + "'}",
                            dataType: "json",
                            success: function (data) {
                                alert(data.d);
                                window.location.href = "OutletsMerging.aspx";
                            },
                            error: function (data) {
                                alert(JSON.stringify(data));
                            }
                        });
                    });
                });
            </script>
        </body>
    </html>
</asp:Content>


