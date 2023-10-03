<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"  CodeFile="New_Territory_CustomDetail.aspx.cs" Inherits="MasterFiles_New_Territory_CustomDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>            
            <title>Route Add</title>
            <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
            <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />    
            
            <style type="text/css">
                #chkboxLocation label {
                    margin-bottom: 0px;
                }
                table td, table th {
                    margin-bottom: 0px;
                    padding: 3px;
                }
                select {
                    width: 100%;
                    border: 1px solid #D5D5D5 !important;
                    padding: 6px 6px 7px !important;
                }
                select:focus {
                    outline: none;
                    box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
                }
                .frmddl{
                    width:200px !important;
                    height:30px !important;

                }
                .form-control{
                    Height:34px !important;
                }
                .ChbControl{
                    height:200px !important;
                    overflow-x:scroll;
                    overflow-y:scroll;
                }
                /* Set a fixed scrollable wrapper */
                .tableWrap {
                    height: 250px;
                   /* border: 2px solid black;*/ 
                    overflow: auto;
                }
                /* Set header to stick to the top of the container. */
                thead tr th {
                    position: sticky;
                    top: 0;
                }

                /* If we use border,
                    we must use table-collapse to avoid
                    a slight movement of the header row */
                table {
                    border-collapse: collapse;
                }
                /* Because we must set sticky on th,
                    we have to apply background styles here
                    rather than on thead */
                th {
                    padding: 16px;
                    padding-left: 15px;
                    border-left: 1px dotted rgba(200, 209, 224, 0.6);
                    border-bottom: 1px solid #e8e8e8;
                    background: #ffc491;
                    text-align: left;
                    /* With border-collapse, we must use box-shadow or psuedo elements
                        for the header borders */
                    box-shadow: 0px 0px 0 2px #e8e8e8;
                }
                
                /* Basic Demo styling */
                table {
                    width: 100%;
                    font-family: sans-serif;
                }
                table td {
                    padding: 5px;
                }
                tbody tr {
                    /*border-bottom: 2px solid #e8e8e8;*/
                }
                thead {
                    font-weight: 500;
                    color: rgba(0, 0, 0, 0.85);
                }
                tbody tr:hover {
                    background: #e6f7ff;
                }
                .txtRoutecode {
                    background-color:#9b9797 !important;
                    color:white !important;
                    font-weight:bold !important;
                }
                    
            </style>

           
        </head>
        <body>
             <form id="Form1" runat="server">
                 <div class="card" style="margin-top:-10px !important">
                     <div class="card-header">
                         <div class="row">
                             
                             <div class="col-md-12 sub-header">
                                 <div class="col-md-6 sub-header" style="float:left">
                                     Route Add  
                                 </div>
                                 <div class="col-md-6 sub-header" style="float:right;margin-top:5px;">
                                     
                                     <div class="col-md-2" style="float: right;">
                                          <button type="reset" id="btnback" class="btn btn-primary" value="Back">Back</button>
                                         <%--<i class="fa fa-arrow-left btn btn-circle" id="btnback" style="color: #3f7b96; box-shadow: 1px 1px 6px 2px grey;"></i>   --%>                                                                                               
                                     </div>
                                      <div class="col-md-2" style="float: right;margin-top:5px;">
                                          <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                      </div>
                                      <div class="col-md-2" style="float: right;margin-top:5px;">
                                          <button type="button" class="btn btn-primary btnSave" id="btnSave">Save</button>
                                          <asp:Button ID="Button1"  CssClass="btn btn-primary" runat="server" Text="Save" Visible="false" />
                                      </div>
                                 </div>                                                        
                                 
                             </div>
                         </div>
                     </div>
                     
                     <div  class="card-body" id="AddRoute"  style="height:830px !important;overflow-y:scroll !important;margin-top:-10px !important;">
                         <br />
                         <div class="row">                 
                             <div class="col-lg-12">
                                 <div class="col-lg-7" style="float:left">
                                     <asp:Panel ID="pnlsf" runat="server" Visible="false" HorizontalAlign="Left" CssClass="marRight">
                                         <asp:Label ID="lblTerrritory" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
                                     </asp:Panel>

                                     <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" CssClass="marRight">
                                         <asp:Label ID="Lab_DSM" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
                                     </asp:Panel>
                                    
                                 </div>
                             </div>                                       
                         </div>
                         <br />
                         <div class="row">                             
                             <div class="col-lg-12">
                                 <div class="col-lg-2">
                                     <asp:Label ID="lblRoutecode" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Route Code</asp:Label>
                                     <asp:HiddenField runat="server" ID="hdnRoutecode" Value="" />
                                     <div class="form-group">
                                           <asp:TextBox ID="txtRoutecode" TabIndex="1" SkinID="MandTxtBox" CssClass="form-control txtRoutecode"  runat="server" MaxLength="10" onkeypress="AlphaNumeric_NoSpecialChars(event);" ReadOnly="true">
                                           </asp:TextBox>
                                     </div>
                                 </div>

                                 <div class="col-lg-4">
                                     <label><span style="Color:Red">*</span>Route Name</label>
                                     <%--<asp:Label ID="lblRouteName" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Route Name</asp:Label>--%>
                                     <div class="form-group">
                                         <asp:TextBox ID="txtRouteName" SkinID="MandTxtBox"  onfocus="this.style.backgroundColor='#E0EE9D'"   
                                             onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" CssClass="form-control"  
                                             onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                         </asp:TextBox>
                                     </div>
                                 </div>

                                 <div class="col-lg-3">
                                      <label><span style="Color:Red">*</span>Target</label>
                                     <%--<asp:Label ID="lblRoute_Target" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Target</asp:Label>--%>
                                     <div class="form-group">
                                         <asp:TextBox ID="txt_Target" runat="server" SkinID="MandTxtBox" MaxLength="50"  onfocus="this.style.backgroundColor='#E0EE9D'" 
                                             onblur="this.style.backgroundColor='White'"  TabIndex="3" CssClass="form-control" onkeypress="CheckNumeric(event);" Text="0">
                                         </asp:TextBox>
                                     </div>
                                 </div>

                                 <div class="col-lg-3">
                                     <label><span style="Color:Red">*</span>MinProd %</label>
                                     <%--<asp:Label ID="lblMinProd" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>MinProd %</asp:Label>--%>
                                     <div class="form-group">
                                         <asp:TextBox ID="txtMinProd" runat="server" SkinID="TxtBxNumOnly" MaxLength="50"  onfocus="this.style.backgroundColor='#E0EE9D'"  
                                             onblur="this.style.backgroundColor='White'"  CssClass="form-control" TabIndex="4"  onkeypress="CheckNumeric(event);" Text="0">
                                         </asp:TextBox>
                                     </div>
                                 </div>
                             </div>                             
                         </div>
                          <br />
                         <div class="row">
                             <div class="col-lg-12">
                                 <div class="col-lg-2">
                                      <label><span style="Color:Red"></span>Route Population</label>
                                     <%--<asp:Label ID="lblRoutePopulation" runat="server"  SkinID="lblMand"><span style="Color:red"></span>Route Population</asp:Label>--%>
                                     <div class="form-group">
                                         <asp:TextBox ID="txtRoutePopulation" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"  
                                             onfocus="this.style.backgroundColor='#E0EE9D'"  onblur="this.style.backgroundColor='White'"  
                                             TabIndex="4"  CssClass="form-control" onkeypress="CheckNumeric(event);" Text="0" Height="50">
                                         </asp:TextBox>
                                     </div>

                                 </div>
                                 <div class="col-lg-4">
                                     <%--<label id="Label1" style="Height:18px !important;Width:120px !important" runat="server" class="hide"><span style="Color:Red"></span>DSM Name</label>--%>
                                     <label><span style="Color:Red">*</span>Territory</label>
                                     <asp:Label ID="Label1" runat="server" SkinID="lblMand" Visible="false"><span style="Color:Red"></span>DSM Name</asp:Label>
                                     <%--<asp:Label ID="Label3" runat="server"  SkinID="lblMand"><span style="Color:red">*</span>Territory</asp:Label>--%>
                                     <div class="form-group">
                                         <asp:DropDownList ID="ddldsm" runat="server" SkinID="ddlRequired" Visible="false" CssClass="form-control" 
                                             data-live-search="true"></asp:DropDownList>
                                         
                                         <%--<asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="form-control DropDownList  frmddl" 
                                                 data-live-search="true"    Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);"  
                                                 OnSelectedIndexChanged="ddlTerritoryName_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                                             </asp:DropDownList> --%>     
                                         
                                         <asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="form-control DropDownList"
                                             data-live-search="true"  Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                         </asp:DropDownList>    
                                     </div>
                                 </div>
                                 <div class="col-lg-2">
                                     <asp:Label ID="Lbl_aw_type" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True"><span style="Color:Red">*</span>Allowance Type</asp:Label>
                                     <div class="form-group">
                                         <%--<asp:DropDownList ID="DDL_aw_Type" runat="server" SkinID="ddlRequired" Visible="true"  OnSelectedIndexChanged="DDL_aw_Type_SelectedIndexChanged"  
                                                 AutoPostBack="true" CssClass="form-control selectpicker frmddl" data-live-search="true">
                                                 <asp:ListItem Value="1">HQ</asp:ListItem>
                                                 <asp:ListItem Value="2">EX</asp:ListItem>
                                                 <asp:ListItem Value="3">OS</asp:ListItem>
                                                 <asp:ListItem Value="4">OS-EX</asp:ListItem>
                                             </asp:DropDownList>--%>
                                         
                                         <asp:DropDownList ID="DDL_aw_Type" runat="server" SkinID="ddlRequired" Visible="true"  
                                             CssClass="form-control" data-live-search="true">
                                             <%--<asp:ListItem Value="0">---Select---</asp:ListItem>--%>
                                             <asp:ListItem Value="1">HQ</asp:ListItem>
                                             <asp:ListItem Value="2">EX</asp:ListItem>
                                             <asp:ListItem Value="3">OS</asp:ListItem>
                                             <asp:ListItem Value="4">OS-EX</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="col-lg-2">
                                     <asp:Label ID="lblTown" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True"><span style="Color:Red"></span>Town</asp:Label>
                                     <div class="form-group">
                                         <%--<asp:DropDownList ID="ddl_town" runat="server" SkinID="ddlRequired" Visible="true"  AutoPostBack="true" 
                                                 CssClass="form-control selectpicker frmddl" data-live-search="true">
                                             </asp:DropDownList>--%>
                                         
                                         <asp:DropDownList ID="ddl_town" runat="server" SkinID="ddlRequired" Visible="true" 
                                             CssClass="form-control" data-live-search="true">
                                         </asp:DropDownList>

                                     </div>
                                 </div>
                                 <div class="col-lg-2">
                                     <asp:Label ID="lblstay" SkinID="lblMand" runat="server" Visible="false" Font-Bold="True"><span style="Color:Red">*</span>Stay Place</asp:Label>
                                     <div class="form-group">
                                         <asp:DropDownList ID="DDLStay" runat="server" SkinID="ddlRequired" Visible="false" CssClass="form-control" data-live-search="true">                        
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                             </div>
                         </div>
                          <br />
                         <div class="row">
                             <div class="col-lg-12">
                                 <div class="col-lg-6" style="float:left">
                                     <asp:Panel ID="Panel2" runat="server">
                                         <table border="0" cellpadding="0" cellspacing="0" id="Table3" style="width: 100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #19a4c6; color: white;font-weight:bold;">
                                                     &nbsp;Distributor&nbsp;
                                                     <asp:CheckBox ID="checkAll" runat="server" align="right" Text="Select All" OnClick="CheckAll(this);" />
                                                 </td>
                                             </tr>
                                         </table>
                                         <div class="tableWrap">
                                             <table border="0" cellpadding="0" id="chkboxLocation" cellspacing="0" style="width: 100%;">
                                                 <tbody></tbody>                                             
                                             </table>       

                                         </div>
                                                                          
                                     </asp:Panel>
                                 </div>
                                 <div class="col-lg-6" style="float:right;margin-top:-13px !important;">
                                     <asp:Panel ID="Panel4" runat="server">
                                         <br />
                                         <table border="0" cellpadding="0" cellspacing="0" id="Table2" style="width: 100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #19a4c6; color: white;font-weight:bold;height:30px">
                                                     &nbsp;Field Force&nbsp;
                                                 </td>
                                             </tr>
                                         </table>
                                         <div class="tableWrap">
                                              <table border="0" cellpadding="0" id="chkboxDDLFO" cellspacing="0" style="width: 100%;">
                                                  <tbody></tbody>                                             
                                              </table>
                                         </div>                                        
                                     </asp:Panel>
                                 </div>
                             </div>

                         </div>
                          <br />
                         <div class="row">
                             <div class="col-lg-12">
                                 <asp:Panel ID="Panel3" runat="server">
                                         <table border="0" cellpadding="0" cellspacing="0" id="tblad" style="width:  100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #19a4c6; color: white;font-weight:bold; padding: 6px;">
                                                     &nbsp;Additional Fields&nbsp;                            
                                                 </td>
                                             </tr>
                                         </table>
                                         <br />
                                         <div class="labelnames"></div>
                                         <br />
                                        <%-- <div class="afdata"></div>
                                         <br />--%>
                                         <div class="tableWrap">
                                             <table border="0" cellpadding="3" cellspacing="3" id="RouteadditionalField" style="width: 100%;margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                                 <tbody></tbody>    
                                             </table> 
                                         </div>              
                                     </asp:Panel>

                             </div>
                         </div>                         
                     </div>                      
                 </div>        
             </form>
          
            <script type="text/javascript">
                var Territory_Code = '';
                var MasFrms = []; var BindData = []; var CFBindData = []; var DistName = []; var sfon = [];
                var CFldType = ""; var SFldType = ""; var MSFldType = ""; var RBFlType = ""; var scontrolId;
                var mdcontrolId
                $(document).ready(function () {
                    $('#<%=txtRouteName.ClientID%>').focus();
                    
                    GetRouteTown(); 
                    FillTerritoryName(); 
                    GetCustomFormsFields(); 

                    if (localStorage.getItem('New_Territory_CustomDetail.aspx') === null) {
                        var item = { div: '<%=Session["div_code"]%>' };

                        namesArr = [];
                        namesArr.push(item);
                        window.localStorage.setItem('New_Territory_CustomDetail.aspx', JSON.stringify(namesArr));
                    }

                    console.log(<%=Territory_Code%>);

                    Territory_Code = '<%=Territory_Code%>';

                    //var pageURL = window.location.search.substring(2);
                    //if (pageURL != "") {
                    //    var urlQS = pageURL.split('&');
                    //    var Territory_Code = 0;
                    //    if (urlQS.length > 0) {
                    //        for (var i = 0; i < urlQS.length; i++) {
                    //            var paramName = urlQS[i].split('=');
                    //            Territory_Code = paramName[1];
                    //        }
                    //    }
                    //}
                    //else { Territory_Code = 0; }

                    //console.log(Territory_Code);

                    if ((Territory_Code > 0 || Territory_Code != '')) {

                        BindMainData(Territory_Code);

                        BindCustomFieldData(Territory_Code);
                    }                   
                });

                $('#btnSave').click(function () {
                    //alert('Hi');

                    SaveCustomFielsValues();
                    //window.location.href = "New_Territory_CustomDetail.aspx?&Territory_Code=" + route_C + "&state=" + state + "&HQ=" + HQ + "&SF=" + SF + "&HQNm=" + HQNm + "";
                });

                $('#btnback').on('click', function () {
                    window.location.href = 'Route_Master_New.aspx';
                });


                $('#<%=DDL_aw_Type.ClientID%>').on('change', function () {

                    var DDL_aw_Type = $('#<%=DDL_aw_Type.ClientID%>').val();
                    //alert(DDL_aw_Type);
                    if (DDL_aw_Type == "4") {
                        Load_staypln(DDL_aw_Type);
                        $('#<%=lblstay.ClientID%>').show();
                        $('#<%=DDLStay.ClientID%>').show();

                    }
                    else {
                        $('#<%=lblstay.ClientID%>').hide();
                        $('#<%=DDLStay.ClientID%>').hide();
                    }

                });

                function Load_staypln(DDL_aw_Type) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/Loadstaypln",
                        data: "{'terr_code':'" + DDL_aw_Type +"'}",
                        dataType: "json",
                        success: function (r) {
                             var ddltown = $('#<%=DDLStay.ClientID%>');
                             ddltown.empty().append('<option selected="selected" value="0">Please select</option>');
                             $.each(r.d, function () {
                                 ddltown.append($("<option></option>").val(this['Value']).html(this['Text']));
                             });
                         },
                         error: function (data) {
                             alert(JSON.stringify(data.d));
                         }
                     });
                }                           

                $('#<%=ddlTerritoryName.ClientID%>').on('change', function () {

                    var ddlTerritoryName = $('#<%=ddlTerritoryName.ClientID%>').val();
                    BindDitributor(ddlTerritoryName);
                    BindFOName(ddlTerritoryName);                  
                   
                });

                function BindDitributor(TerritoryNo) {
                    var DistbNames = [];
                    $.ajax({
                        type: "POST",
                        url: "New_Territory_CustomDetail.aspx/GetDistributorList",
                        data: "{'TerritoryName':'" + TerritoryNo + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            DistbNames = JSON.parse(data.d) || [];
                            console.log(DistbNames);
                            if (DistbNames.length > 0) {
                                $("#chkboxLocation TBODY").html("");
                                                           
                                var str = "<tr>"; 
                                j = 0;
                                for (var i = 0; i < DistbNames.length; i++) {

                                    var id = DistbNames[i].Stockist_Code;
                                    var txt = DistbNames[i].Stockist_Name;

                                    str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id=" + id + " name='country' class='chkRow' value='" + id + "' />&nbsp;&nbsp;<label class='text-wrap' for='" + id + "'>" + txt + " </label><td>";
                                    //str += "<td><label for=" + 'chk' + i + ">" + txt + "</label></td>";

                                    j++;
                                    if (j == 2) {
                                        str += "</tr><tr>";
                                        j = 0;
                                    }
                                }
                                str += "</tr>";

                                $("#chkboxLocation TBODY").append(str);

                                
                            }

                            if ((DistName.length > 0)) {

                                for (var l = 0; l < DistName.length; l++) {
                                    var aval = DistName[l];
                                    //alert(aval);
                                    $("#chkboxLocation TBODY").find('tr').each(function () {

                                        $(this).find("input[type='checkbox']").each(function () {
                                            var id = $(this).attr("id");
                                            if (aval != "") {
                                                if (id == aval) {
                                                    $(this).prop('checked', true);
                                                }
                                            }
                                        });
                                    });

                                }
                            }
                        }
                    });
                }

                function BindFOName(TerritoryCode) {
                    var FOName = [];
                    $.ajax({
                        type: "POST",
                        url: "New_Territory_CustomDetail.aspx/GetFieldForceList",
                        data: "{'TerritoryName':" + TerritoryCode + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            FOName = JSON.parse(data.d) || [];
                            $('#chkboxDDLFO TBODY').html("");
                            if (FOName.length > 0) {

                                var str = "<tr>";
                                j = 0;
                                for (var i = 0; i < FOName.length; i++) {

                                    var id = FOName[i].Sf_Code;
                                    var txt = FOName[i].Sf_Name;

                                    str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + id + "'' name='tfieldforce' class='chkfnRow' value='" + id + "' />&nbsp;&nbsp;<label class='text-wrap' for='" + id + "'>" + txt + " </label><td>";
                                    /* str += "<td><label for=" + 'chfnk' + i + ">" + txt + "</label></td>";*/

                                    j++;
                                    if (j == 2) {
                                        str += "</tr><tr>";
                                        j = 0;
                                    }
                                }
                                str += "</tr>";

                                $('#chkboxDDLFO TBODY').append(str);
                            }


                            if ((sfon.length > 0)) {

                                for (var l = 0; l < sfon.length; l++) {
                                    var aval = sfon[l];
                                    //alert(aval);
                                    $("#chkboxDDLFO TBODY").find('tr').each(function () {

                                        $(this).find("input[type='checkbox']").each(function () {
                                            var id = $(this).attr("id");
                                            if (aval != "") {
                                                if (id == aval) {
                                                    $(this).prop('checked', true);
                                                }
                                            }
                                        });
                                    });

                                }
                            }
                            
                        }
                    });
                }

                $('#<%=checkAll.ClientID%>').click(function () {
                    isChecked = $(this).prop("checked");
                    $('input[name=country]').each(function (index, item) {
                        $(this).closest('tr').find('input[type="checkbox"]').prop("checked", isChecked);
                    });                    
                });  

                function GetCustomFormsFields() {
                   
                    var sselectionType; var mselectionType; var cbselectionType; var rbselectionType; var ssmtablename; var ssmcolumnname; 
                    var smmtablename; var smmcolumnname;  var cmmtablename; var cmmcolumnname; var cmrmtablename; var cmrmcolumnname;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/GetCustomFormsFieldsList",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'4'}",
                        dataType: "json",
                        success: function (data) {
                            MasFrms = JSON.parse(data.d) || [];

                            if (MasFrms.length > 0) {
                                //var str = '<table id="RouteadditionalField" class="RouteadditionalField table-responsive">';
                                var str = "";
                                var str1 = "";
                                j = 0;                               
                                str += "<tr>";
                                var n = 0;
                                for (var k = 0; k < MasFrms.length; k++) {
                                    var FldType = MasFrms[k].Fld_Type;
                                    var Mandate = MasFrms[k].Mandate;
                                    if (FldType == "L") {
                                        str1 += "<label for=" + MasFrms[k].Field_Col + " value=" + MasFrms[k].Field_Name + "><span>" + MasFrms[k].Field_Name + "</span></label>";
                                    }
                                    else {
                                        str += "<td class='space' align='left'><label for=" + MasFrms[k].Field_Col + " value=" + MasFrms[k].Field_Name + ">" + ((Mandate == "Yes") ? "<span class='fldm' style='Color:Red'>*</span>" : "<span />") + MasFrms[k].Field_Name + "</label></td>";
                                        switch (FldType) {
                                            case 'TA':
                                                if (MasFrms[k].Mandate == "Yes") { str += "<td class='stylespc' align='left'><input type='text' id=" + MasFrms[k].Field_Col + "' name=" + MasFrms[k].Field_Col + " class='form-control required frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                else { str += "<td class='stylespc' align='left'><input type='text' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                break;
                                            case 'TAS':
                                                if (MasFrms[k].Mandate == "Yes") { str += "<td class='stylespc' align='left'><input type='text' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control required frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                else { str += "<td class='stylespc' align='left'><input type='text' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                break;
                                            case 'TAM':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><textarea type='text' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control required frmddl' maxLength=" + MasFrms[k].Fld_Length + "></textarea></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><textarea type='text' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired frmddl' maxLength=" + MasFrms[k].Fld_Length + "></textarea></td>"; }
                                                break;
                                            case 'NC':
                                                str += "<td class='stylespc' align='left'>";
                                                str += "<div class='row'>";
                                                str += "<div class='col-sm-6'>";
                                                str += "<div class='input-group input-group-sm mb-3' style='display: flex'>";
                                                str += "<div class='input-group-prepend'>";
                                                str += "<div class='input-group-text' style='width:50px; padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px;' id='NCS'>" + MasFrms[k].Fld_Symbol + "</div>";
                                                str += "</div>";
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<input type='number' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + "/>";
                                                }
                                                else {
                                                    str += "<input type='number' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired  frmddl' maxLength=" + MasFrms[k].Fld_Length + "/>";
                                                }
                                                str += "</div>";
                                                str += "</div>";
                                                str += "</div>";
                                                str += "</td>";
                                                break;
                                            case 'NP':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><input type='number' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + " class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='number' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                break;
                                            case 'N':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><input type='number' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + " class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'><input type='number' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + " class='form-control notrequired  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                break;
                                            case 'DR':

                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><input type='date' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='date' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                break;
                                            case 'D':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><input type='date' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + " class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='date' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired   frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>"; }
                                                break;
                                            case 'TR':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><input type='time' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + " class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><input type='time' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired  frmddl' maxLength=" + MasFrms[k].Fld_Length + "/></td>"; }
                                                break;
                                            case 'T':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><input type='time' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control required  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                else {
                                                    str += "<td class='stylespc' align='left'><input type='time' id=" + MasFrms[k].Field_Col + " name=" + MasFrms[k].Field_Col + "  class='form-control notrequired  frmddl' maxLength=" + MasFrms[k].Fld_Length + " /></td>";
                                                }
                                                break;
                                            case 'SSM':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><select name=" + MasFrms[k].Field_Col + " id=" + MasFrms[k].Field_Col + " class='form-control required   frmddl'></select></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><select name=" + MasFrms[k].Field_Col + " id=" + MasFrms[k].Field_Col + " class='form-control notrequired  frmddl'></select></td>"; }
                                                ssmtablename = MasFrms[k].Fld_Src_Name; ssmcolumnname = MasFrms[k].Fld_Src_Field;
                                                scontrolId = MasFrms[k].Field_Col;
                                                sselectionType = "SingleSelectionddl";
                                                break;
                                            case 'SMM':
                                                if (MasFrms[k].Mandate == "Yes") {
                                                    str += "<td class='stylespc' align='left'><select name=" + MasFrms[k].Field_Col + " id=" + MasFrms[k].Field_Col + "  class='form-control required multiddl'></select></td>";
                                                }
                                                else { str += "<td class='stylespc' align='left'><select name=" + MasFrms[k].Field_Col + "  id=" + MasFrms[k].Field_Col + " class='form-control notrequired  multiddl'></select></td>"; }
                                                smmtablename = MasFrms[k].Fld_Src_Name; smmcolumnname = MasFrms[k].Fld_Src_Field;
                                                mdcontrolId = MasFrms[k].Field_Col;
                                                mselectionType = "MultipleSelectionddl";
                                                break;
                                            case 'CM':
                                                str += "<td class='stylespc' align='left'><div name=" + MasFrms[k].Field_Col + " id=" + MasFrms[k].Field_Col + " class='notrequired ChbControl'></div></td>";
                                                cmmtablename = MasFrms[k].Fld_Src_Name; cmmcolumnname = MasFrms[k].Fld_Src_Field;
                                                cbselectionType = "CheckboxListControl";
                                                break;
                                            case 'RM':
                                                str += "<td class='stylespc' align='left'><div name=" + MasFrms[k].Field_Col + " id=" + MasFrms[k].Field_Col + " class='notrequired RbtnlControl'></div></td>";
                                                cmrmtablename = MasFrms[k].Fld_Src_Name; cmrmcolumnname = MasFrms[k].Fld_Src_Field;
                                                rbselectionType = "RadiobuttonListControl";
                                                break;
                                            case 'FS':
                                                str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[k].Field_Col + " name = " + MasFrms[k].Field_Col + "  accept='image/png, image/jpeg' class='form-control notrequired  frmddl' /></td>";
                                                break;
                                            case 'FSC':
                                                str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[k].Field_Col + " name = " + MasFrms[k].Field_Col + "  accept='image/png, image/jpeg' class='form-control notrequired  frmddl' /></td>";
                                                break;
                                            case 'FC':
                                                str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[k].Field_Col + " name = " + MasFrms[k].Field_Col + "  accept='image/png, image/jpeg' class='form-control notrequired  frmddl' /></td>";
                                            
                                                break;
                                            case 'F':
                                                str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[k].Field_Col + " name = " + MasFrms[k].Field_Col + "  accept='image/png, image/jpeg' class='form-control notrequired  frmddl' /></td>";
                                                break;
                                            default:
                                                break
                                        }
                                    }

                                    n++;
                                    if (n == 3) {
                                        str += "</tr><tr>";
                                        n = 0;
                                    }                                    
                                }
                                str += "</tr>";
                                //str += '</table>';
                                //$(".afdata").append(str);
                               
                                $("#RouteadditionalField tbody").append(str);
                                $(".labelnames").append("<div>" + str1 + "</div>");
                                //$("# tbody").append('<tr class="alcode">' + str + '</tr>');

                                if (sselectionType == "SingleSelectionddl") {
                                    BindDropdown(ssmtablename, ssmcolumnname, scontrolId);
                                }

                                if (mselectionType == "MultipleSelectionddl") {
                                    BindDropdown(smmtablename, smmcolumnname, mdcontrolId);
                                }

                                if (cbselectionType == "CheckboxListControl") {
                                    var filtcmgr = [];
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        async: false,
                                        url: "New_Territory_CustomDetail.aspx/GetCustomFormsSeclectionMastesList",
                                        data: "{'TableName':'" + cmmtablename + "','ColumnsName':'" + cmmcolumnname + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            filtcmgr = JSON.parse(data.d) || [];
                                            //console.log(filtcmgr);
                                            //alert('#' + ssmcontrolerId + '');
                                            var html = '<table id="tChblControl" style="width:200px;height:300px;" class="tChblControl table-responsive">';

                                            //var html = '';
                                            html += "<tr>";
                                            var m = 0;
                                            for (var k = 0; k < filtcmgr.length; k++) {

                                                html += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + filtcmgr[k].IDCol + "''";
                                                html += "name = 'tfieldforce' class='chkfnRow' value = '" + filtcmgr[k].IDCol + "' />";
                                                html += "<label class='text-wrap' for='" + filtcmgr[k].IDCol + "'>" + filtcmgr[k].TextVal + " </label></td>";

                                                //html += "<td><input type='radio' id='" + filtcmgr[k].IDCol + "' value='" + filtcmgr[k].IDCol + "' ></td>";
                                                //html += "<td><label class='text-wrap' for='" + filtcmgr[k].IDCol + "'>" + filtcmgr[k].TextVal + " </label></td>";
                                                m++;
                                                if (m == 4) {
                                                    html += "</tr><tr>";
                                                    m = 0;
                                                }
                                            }
                                            html += "</tr>";
                                            html += '</table>';
                                            $(".ChbControl").append(html);
                                        }
                                    });

                                    //BindDropdown(cmmtablename, cmmcolumnname, cmmcontrolerId, cbselectionType);
                                }

                                if (rbselectionType == "RadiobuttonListControl") {
                                    var filtcmgr = [];
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        async: false,
                                        url: "New_Territory_CustomDetail.aspx/GetCustomFormsSeclectionMastesList",
                                        data: "{'TableName':'" + cmrmtablename + "','ColumnsName':'" + cmrmcolumnname + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            filtcmgr = JSON.parse(data.d) || [];
                                            //console.log(filtcmgr);
                                            //alert('#' + ssmcontrolerId + '');
                                            var html = '<table id="tRbtnlControl" width="600" class="tRbtnlControl table-responsive">';

                                            //var html = '';
                                            html += "<tr>";
                                            var m = 0;
                                            for (var k = 0; k < filtcmgr.length; k++) {

                                                html += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id='" + filtcmgr[k].IDCol + "''";
                                                html += "name = 'tfieldforce' class='chkfnRow' value = '" + filtcmgr[k].IDCol + "' />";
                                                html += "&nbsp;& nbsp;<label class='text-wrap' for='" + filtcmgr[k].IDCol + "'>" + filtcmgr[k].TextVal + " </label><td>";

                                                //html += "<td><input type='radio' id='" + filtcmgr[k].IDCol + "' value='" + filtcmgr[k].IDCol + "' ></td>";
                                                //html += "<td><label class='text-wrap' for='" + filtcmgr[k].IDCol + "'>" + filtcmgr[k].TextVal + " </label></td>";

                                                m++;
                                                if (m == 4) {
                                                    html += "</tr><tr>";
                                                    m = 0;
                                                }
                                            }
                                            html += "</tr>";
                                            html += '</table>';
                                            $(".RbtnlControl").append(html);
                                        }
                                    });

                                    //BindDropdown(cmmtablename, cmmcolumnname, cmmcontrolerId, cbselectionType);
                                }
                            }

                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }

                    });

                }

                function BindDropdown(tablename, columnname, controlId) {
                    var filtmgr = []; 
                  
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/GetCustomFormsSeclectionMastesList",
                        data: "{'TableName':'" + tablename + "','ColumnsName':'" + columnname + "'}",
                        dataType: "json",
                        success: function (data) {
                            filtmgr = JSON.parse(data.d) || [];

                            $('#' + controlId + '').empty();
                            $('#' + controlId + '').append('<option value="0">Select</option>');
                            if (filtmgr.length > 0) {
                                for (var j = 0; j < filtmgr.length; j++) {
                                    $('#' + controlId + '').append('<option value="' + filtmgr[j].IDCol + '">' + filtmgr[j].TextVal + '</option>');
                                }
                            }
                        }
                    });                                       
                }

                function SaveCustomFielsValues() {

                    var routemainflds = new Array();

                    var addfields = new Array();
                    var vflag = true;

                    if ($('#<%=txtRouteName.ClientID%>').val() == "") {
                        alert('Please Enter Route Name !!');                       
                        vflag = false;
                        $('#<%=txtRouteName.ClientID%>').focus();
                        return vflag;
                        
                    }

                    if ($('#<%=txt_Target.ClientID%>').val() == "") {
                        alert('Please Enter Target !!');
                        vflag = false;
                        $('#<%=txt_Target.ClientID%>').focus();
                        return vflag;                       
                    }

                    if ($('#<%=txtMinProd.ClientID%>').val() == "") {
                        alert('Please Enter MinProd  !!');
                        vflag = false;
                        $('#<%=txtMinProd.ClientID%>').focus();
                        return vflag;                        
                    }

                    if (($('#<%=ddlTerritoryName.ClientID%>').val() == "" || $('#<%=ddlTerritoryName.ClientID%>').val() == "0")) {
                        alert('Please Enter Territory  !!');
                        vflag = false;
                        $('#<%=ddlTerritoryName.ClientID%>').focus();
                        return vflag;                       
                    }

                    if (($('#<%=DDL_aw_Type.ClientID%>').val() == "" || $('#<%=DDL_aw_Type.ClientID%>').val() == "0")) {
                        alert('Please Enter Allowance Type  !!');
                        vflag = false;
                        $('#<%=DDL_aw_Type.ClientID%>').focus();
                        return vflag;                        
                    }

                    var dist_name = "";
                   
                    var sf_code = "";
                    
                    $("#chkboxLocation TBODY").find('tr').each(function () {
                        $(this).find("input[type='checkbox']").each(function () {
                            if ($(this).is(":checked")) {
                                var id = $(this).val();
                                dist_name += id + ",";
                            }
                        });
                    });

                    $("#chkboxDDLFO TBODY").find('tr').each(function () {
                        $(this).find("input[type='checkbox']").each(function () {
                            if ($(this).is(":checked")) {
                                var id = $(this).val();                               
                                sf_code += id + ",";
                            }
                        });
                    });

                    console.log(dist_name);
                    console.log(sf_code);

                    if ((dist_name.length < 0 || dist_name == "" || dist_name == null)) {
                        alert('Please Select At least one distributor  !!')
                        vflag = false;
                        return vflag;
                    }

                    if ((sf_code.length < 0 || sf_code == "" || sf_code == null)) {
                        alert('Please Select At least one field force  !!')
                        vflag = false;
                        return vflag;
                    }

                    var values = ""; var fields = "";

                    $('.required').each(function () {
                        //alert('hi');
                        var adDetail = {};
                        var fval = $(this).val();
                        var fid = `${this.id}`;
                        values = ""; fields = "";
                        fields = fid;
                        var $label = $("label[for='" + fid + "']");
                        var msg = "Please Fill The " + $label.text() + "";
                        if ((fval == null || fval == "" || fval == "0")) {
                            alert(msg);
                            $(fid).focus();
                            vflag = false;
                            return vflag;
                        }

                        $(this).find('input[type="checkbox"]:checked').each(function () {
                            console.log(this.value);
                            fval += this.value + ",";
                        });
                        values = fval;
                        adDetail.Fields = fields;
                        adDetail.Values = values;

                        addfields.push(adDetail);
                    });

                    $('.notrequired').each(function () {

                        var adDetail = {};
                        var fval = $(this).val();
                        var fid = `${this.id}`;
                        fields = "";
                        fields = fid;

                        $(this).find('input[type="checkbox"]:checked').each(function () {
                            console.log(this.value);
                            fval += this.value + ",";
                        });
                        values = fval;
                        adDetail.Fields = fields;
                        adDetail.Values = values;
                        addfields.push(adDetail);
                    });

                    if (vflag) {
                        var Maindata = {};

                        Maindata.Route_code = $('#<%=txtRoutecode.ClientID%>').val();
                        Maindata.Route_Name = $('#<%=txtRouteName.ClientID%>').val();
                        Maindata.Target = $('#<%=txt_Target.ClientID%>').val();
                        Maindata.min_prod = $('#<%=txtMinProd.ClientID%>').val();
                        Maindata.Route_population = $('#<%=txtRoutePopulation.ClientID%>').val();
                        Maindata.territory_id = $('#<%=ddlTerritoryName.ClientID%>').val();

                        Maindata.dist_name = dist_name;
                        Maindata.sf_code = sf_code;

                        Maindata.route_town = $('#<%=ddl_town.ClientID%>').val();
                        Maindata.dstay = $('#<%=DDLStay.ClientID%>').val();
                        Maindata.DDL_aw_Type = $('#<%=DDL_aw_Type.ClientID%>').val();
                        Maindata.Additionsfld = addfields;

                        routemainflds.push(Maindata);
                        //var fdata = JSON.stringify({ routemainflds });
                        console.log(JSON.stringify({ routemainflds }));


                        var fdata = {
                            "Route_code": $('#<%=txtRoutecode.ClientID%>').val(),
                            "Route_Name": $('#<%=txtRouteName.ClientID%>').val(),
                            "Target": $('#<%=txt_Target.ClientID%>').val(),
                            "min_prod": $('#<%=txtMinProd.ClientID%>').val(),
                            "Route_population": $('#<%=txtRoutePopulation.ClientID%>').val(),
                            "territory_id": $('#<%=ddlTerritoryName.ClientID%>').val(),
                            "dist_name": dist_name,
                            "sf_code": sf_code,
                            "route_town": $('#<%=ddl_town.ClientID%>').val(),
                            "dstay": $('#<%=DDLStay.ClientID%>').val(),
                            "DDL_aw_Type": $('#<%=DDL_aw_Type.ClientID%>').val(),
                            "Additionsfld": addfields
                        };


                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "New_Territory_CustomDetail.aspx/SaveAdditionalField",
                            /*data: JSON.stringify({ routemainflds }),*/
                            data: "{'fdata':'" + JSON.stringify(fdata) + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == "Updated Successfully") {
                                    alert(data.d);
                                    window.location.href = "Route_Master_New.aspx";
                                    ClearControls();
                                }
                                else if (data.d == "Created Successfully") {
                                    alert(data.d);
                                    window.location.href = "Route_Master_New.aspx";
                                    ClearControls();
                                }
                                else {
                                    window.location.href = "New_Territory_CustomDetail.aspx";
                                }
                            }
                        });
                    }
                }

                function BindMainData(Territory_Code) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/GetBindData",
                        data: "{'TerritoryCode':'" + Territory_Code + "'}",
                        dataType: "json",
                        success: function (data) {
                            BindData = JSON.parse(data.d) || [];

                            if (BindData.length > 0) {
                                for (var j = 0; j < BindData.length; j++) {
                                    var sfcode = BindData[j].SF_Code;
                                    var Dist_Name = BindData[j].Dist_Name;
                                    var RouteTownCode = BindData[j].Route_Town_Code;
                                    console.log(RouteTownCode);
                                    $('#<%=ddl_town.ClientID%>').val(RouteTownCode);
                                    var Territory_Id = BindData[j].Territory_Sname;
                                    console.log(Territory_Id);
                                    $('#<%=ddlTerritoryName.ClientID%>').val(Territory_Id);

                                   

                                    if (Territory_Id > 0) {
                                        BindDitributor(Territory_Id);
                                        BindFOName(Territory_Id)
                                    }

                                    DistName = Dist_Name.split(',');
                                    console.log(DistName);

                                    sfon = sfcode.split(',');
                                    console.log(sfcode);

                                }
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }

                function BindCustomFieldData(Territory_Code) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/GetBindCustomFieldData",
                        data: "{'TerritoryCode':'" + Territory_Code + "'}",
                        dataType: "json",
                        success: function (data) {
                            CFBindData = JSON.parse(data.d) || [];

                            if ((CFBindData.length > 0 && MasFrms.length > 0)) {
                                for (var k = 0; k < MasFrms.length; k++) {

                                    var $fldnm = MasFrms[k].Field_Col;
                                    var fldtyp = MasFrms[k].Fld_Type;                                    
                                    for (var j = 0; j < CFBindData.length; j++) {

                                        var $cval = CFBindData[j][$fldnm];

                                        switch (fldtyp) {
                                            case 'TA':
                                                $("input[type='text']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        $(this).val($cval);
                                                    }
                                                });
                                                break;
                                            case 'TAS':
                                                $("input[type='text']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        $(this).val($cval);
                                                    }
                                                });
                                                break;
                                            case 'TAM':
                                                $("textarea").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        $(this).val($cval);
                                                        $(this).value = $cval;
                                                    }
                                                });
                                                break;
                                            case 'NC':
                                                $("input[type='number']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        //alert($cval);

                                                        $(this).val($cval);
                                                    }
                                                });
                                                break;
                                            case 'NP':
                                                $("input[type='number']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        //alert($cval);
                                                        $(this).val($cval);
                                                    }
                                                });
                                                break;
                                            case 'N':
                                                $("input[type='number']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        //alert($cval);
                                                        $(this).val($cval);
                                                    }
                                                });
                                                break;
                                            case 'D':
                                                $("input[type='date']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {

                                                        var date = new Date($cval);

                                                        var day = date.getDate();
                                                        var month = date.getMonth() + 1;
                                                        var year = date.getFullYear();

                                                        if (month < 10) month = "0" + month;
                                                        if (day < 10) day = "0" + day;

                                                        var today = year + "-" + month + "-" + day;
                                                        $(this).attr("value", today);

                                                    }
                                                });
                                                break;
                                            case 'DR':
                                                $("input[type='date']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        var date = new Date($cval);

                                                        var day = date.getDate();
                                                        var month = date.getMonth() + 1;
                                                        var year = date.getFullYear();

                                                        if (month < 10) month = "0" + month;
                                                        if (day < 10) day = "0" + day;

                                                        var today = year + "-" + month + "-" + day;
                                                        $(this).attr("value", today);

                                                    }
                                                });
                                                break;
                                            case 'T':
                                                $("input[type='time']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        var tv = getFormattedTime($cval);

                                                        $(this).val(tv);
                                                    }
                                                });
                                                break;
                                            case 'TR':
                                                $("input[type='time']").each(function () {
                                                    var id = $(this).attr("id");
                                                    if (id == $fldnm) {
                                                        var tv = getFormattedTime($cval);

                                                        $(this).val(tv);
                                                    }
                                                });
                                                break;
                                            case 'SSM': 
                                                //alert(scontrolId);
                                                //alert($('#' + scontrolerId + ''));
                                                if (scontrolId == $fldnm) {
                                                    //alert($cval);

                                                    var sddl = "#" + scontrolId;

                                                    $(sddl).val($cval);
                                                    //$('#' + scontrolerId + ' option:selected').val($cval);                                                    
                                                }
                                              
                                                break;
                                            case 'SMM':
                                                var arr = $cval.split(',');
                                                //console.log(arr);
                                                var sddl = "#" + scontrolId;
                                                if (mdcontrolId == $fldnm) {
                                                    var mddl = "#" + mdcontrolId;

                                                    for (var l = 0; l < arr.length; l++) {
                                                        var aval = arr[l];
                                                        $(mddl).val(aval);                                                        
                                                    }
                                                }
                                                
                                                break;
                                            case 'CM':
                                                var arr = $cval.split(',');
                                                //console.log(arr);
                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];
                                                    //alert(aval);
                                                    $("input[type='checkbox']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                                break;
                                            case 'RM':
                                                var arr = $cval.split(',');
                                                //console.log(arr);
                                                for (var l = 0; l < arr.length; l++) {
                                                    var aval = arr[l];
                                                    //alert(aval);
                                                    $("input[type='radio']").each(function () {
                                                        var id = $(this).attr("id");
                                                        if (aval != "") {
                                                            if (id == aval) {
                                                                $(this).prop('checked', true);
                                                            }
                                                        }
                                                    });
                                                }
                                                break;
                                            default:
                                                break;

                                        }
                                    }
                                }
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }

                function ClearControls() {
                    $('#<%=txtRoutecode.ClientID%>').val("");
                    $('#<%=txtRouteName.ClientID%>').val("");
                    $('#<%=txt_Target.ClientID%>').val("0");
                    $('#<%=txtMinProd.ClientID%>').val("0");
                    $('#<%=txtRoutePopulation.ClientID%>').val("0");
                    $('#<%=ddlTerritoryName.ClientID%>').val("0");
                    $('#<%=ddl_town.ClientID%>').val("0");

                    $('input[name=country]').each(function (index, item) {
                        if ($(this).is(":checked")) {
                            $(this).prop('checked', false);
                            var id = $(this).val();
                            //var country = $(this).closest('tr').find('label').html();
                            
                        }
                    });

                    $('input[name=country]').each(function (index, item) {
                        if ($(this).is(":checked")) {
                            $(this).prop('checked', false);
                            var id = $(this).val();
                            //var country = $(this).closest('tr').find('label').html();

                        }
                    });

                }

                function GetRouteTown() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/GetRouteTown",
                        data: "{'Division_Code':'<%=Session["div_code"]%>'}",
                        dataType: "json",
                        success: function (r) {
                            var ddltown = $('#<%=ddl_town.ClientID%>');
                            ddltown.empty().append('<option selected="selected" value="0">Please select</option>');
                            $.each(r.d, function () {
                                ddltown.append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                } 

                function FillTerritoryName() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_CustomDetail.aspx/FillTerritoryName",
                        data: "{'Division_Code':'<%=Session["div_code"]%>','sfcode':'<%=Session["sf_code"]%>'}",
                        dataType: "json",
                        success: function (r) {
                            var ddlTerritoryName = $('#<%=ddlTerritoryName.ClientID%>');                            
                            
                            ddlTerritoryName.empty().append('<option selected="selected" value="0">Please select</option>');
                          
                            $.each(r.d, function () {
                                ddlTerritoryName.append($("<option></option>").val(this['Value']).html(this['Text']));                          
                            });
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }

                function getFormattedDate(datev) {

                    var date = new Date(datev);

                    var day = date.getDate();
                    var month = date.getMonth() + 1;
                    var year = date.getFullYear();

                    if (month < 10) month = "0" + month;
                    if (day < 10) day = "0" + day;

                    var today = year + "-" + month + "-" + day;

                    return today;
                }

                function getFormattedTime(time) {

                    var dat = new Date(time);

                    var dd = dat.getDate();
                    var mm = 1 + dat.getMonth();
                    var yy = dat.getFullYear();

                    if (dd < 10) dd = '0' + dd;
                    if (mm < 10) mm = '0' + mm;

                    var dval = dd + '-' + mm + '-' + yy;

                    var h = dat.getHours();
                    var m = dat.getMinutes();
                    var s = dat.getSeconds();

                    if (h < 10) h = '0' + h;
                    if (m < 10) m = '0' + m;
                    if (s < 10) s = '0' + s;

                    var tval = h + ':' + m + ':' + s;

                    return tval;
                }
                
            </script>
        </body>
    </html>
</asp:Content>
