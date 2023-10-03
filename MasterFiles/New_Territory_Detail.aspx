
<%@ Page Title="Route Add" Language="C#" AutoEventWireup="true" CodeFile="New_Territory_Detail.aspx.cs" MasterPageFile="~/Master.master" 
    Inherits="MasterFiles_New_Territory_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>            
            <title>Route Add</title>
            <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
            <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
            
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
                .ChbControl{
                    height:70px !important;
                    overflow-x:scroll;
                    overflow-y:scroll;
                }
            </style>

           
        </head>
        <body>
             <form id="Form1" runat="server">
                 <div class="card"  style="padding:10px !important; margin-top:0px !important;">

                     <div  class="card-body" id="AddRoute">
                         <div class="row" style="padding:1px !important;">
                             <div class="col-lg-12 sub-header">
                                 Route Add                                 
                                 <i class="fa fa-arrow-left btn btn-circle" id="btnback" style="float: right; color: #3f7b96; box-shadow: 1px 1px 6px 2px grey;"></i>                                                                                                  
                             </div>
                             <br />
                             <div class="col-lg-12">
                                 <div class="col-lg-7" style="float:left">
                                     <asp:Panel ID="pnlsf" runat="server" Visible="false" HorizontalAlign="Left" CssClass="marRight">
                                         <asp:Label ID="lblTerrritory" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
                                     </asp:Panel>
                                     <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" CssClass="marRight">
                                         <asp:Label ID="Lab_DSM" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
                                     </asp:Panel>
                                     <br />
                                     <asp:Panel ID="pnl1" runat="server">
                                         <table border="0" cellpadding="3" cellspacing="3" id="tblDocCatDtls">
                                             <tbody>
                                                 <tr>
                                                     <td align="left" class="stylespc">
                                                         <asp:Label ID="lblRoutecode" CssClass="hidden" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Route Code</asp:Label>
                                                         <asp:HiddenField runat="server" ID="hdnRoutecode" Value="" />
                                                     </td>
                                                     <td align="left" class="stylespc">
                                                         <asp:TextBox ID="txtRoutecode" TabIndex="1" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" CssClass="form-control frmddl hidden"  
                                                             onblur="this.style.backgroundColor='White'" runat="server" MaxLength="10" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                                         </asp:TextBox>
                                                     </td>
                                                 </tr>

                                                 <tr>
                                                     <td align="left" class="stylespc">
                                                         <label><span style="Color:Red">*</span>Route Name</label>
                                                         <%--<asp:Label ID="lblRouteName" runat="server" SkinID="lblMand" Height="18px" Width="120px"><span style="Color:Red">*</span>Route Name</asp:Label>--%>
                                                     </td>
                                                     <td align="left" class="stylespc">
                                                         <asp:TextBox ID="txtRouteName" SkinID="MandTxtBox"  onfocus="this.style.backgroundColor='#E0EE9D'"   
                                                             onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" CssClass="form-control frmddl"  
                                                             onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                                         </asp:TextBox>
                                                     </td>
                                                 </tr>

                                                 <tr>
                                                     <td class="space" align="left">
                                                         <label><span style="Color:Red">*</span>Target</label>
                                                         <%--<asp:Label ID="lblRoute_Target" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>Target</asp:Label>--%>
                                                     </td>
                                                     
                                                     <td class="space" align="left">
                                                         <asp:TextBox ID="txt_Target" runat="server" SkinID="MandTxtBox" MaxLength="50"  onfocus="this.style.backgroundColor='#E0EE9D'" 
                                                             onblur="this.style.backgroundColor='White'"  TabIndex="3" CssClass="form-control frmddl" onkeypress="CheckNumeric(event);" Text="0">
                                                         </asp:TextBox>
                                                     </td>
                                                     
                                                     <td align="left" class="stylespc">
                                                         <label><span style="Color:Red">*</span>MinProd %</label>
                                                         <%--<asp:Label ID="lblMinProd" runat="server" SkinID="lblMand"><span style="Color:Red">*</span>MinProd %</asp:Label>--%>
                                                     </td>
                                                     
                                                     <td class="space" align="left">
                                                         <asp:TextBox ID="txtMinProd" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"  onfocus="this.style.backgroundColor='#E0EE9D'"  
                                                             onblur="this.style.backgroundColor='White'"  CssClass="form-control  frmddl" TabIndex="4"  onkeypress="CheckNumeric(event);" Text="0">
                                                         </asp:TextBox>
                                                     </td>
                                                 </tr>

                                                 <tr>
                                                     <td class="space" align="left">
                                                         <label><span style="Color:Red"></span>Route Population</label>
                                                         <%--<asp:Label ID="lblRoutePopulation" runat="server"  SkinID="lblMand"><span style="Color:red"></span>Route Population</asp:Label>--%>
                                                     </td>
                                                     
                                                     <td class="space" align="left">
                                                         <asp:TextBox ID="txtRoutePopulation" runat="server" SkinID="TxtBxNumOnly" MaxLength="15"   
                                                             onfocus="this.style.backgroundColor='#E0EE9D'"  onblur="this.style.backgroundColor='White'"  
                                                             TabIndex="4"  CssClass="form-control frmddl" onkeypress="CheckNumeric(event);" Text="0">
                                                         </asp:TextBox>
                                                     </td>
                                                     
                                                     <td class="space" align="left">
                                                         <%--<label id="Label1" style="Height:18px !important;Width:120px !important" runat="server" class="hide"><span style="Color:Red"></span>DSM Name</label>--%>
                                                         <label><span style="Color:Red">*</span>Territory</label>
                                                         <asp:Label ID="Label1" runat="server" SkinID="lblMand" Visible="false"><span style="Color:Red"></span>DSM Name</asp:Label>
                                                         <%--<asp:Label ID="Label3" runat="server"  SkinID="lblMand"><span style="Color:red">*</span>Territory</asp:Label>--%>
                                                     </td>
                                                     
                                                     <td class="space" align="left">
                                                         <asp:DropDownList ID="ddldsm" runat="server" SkinID="ddlRequired" Visible="false" CssClass="form-control frmddl" 
                                                             data-live-search="true"></asp:DropDownList>

                                                         <%--<asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="form-control DropDownList  frmddl" 
                                                             data-live-search="true"    Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);"  
                                                             OnSelectedIndexChanged="ddlTerritoryName_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                                                         </asp:DropDownList> --%>     
                                                         
                                                          <asp:DropDownList ID="ddlTerritoryName" runat="server" EnableViewState="true" CssClass="form-control DropDownList  frmddl" 
                                                             data-live-search="true"  Enabled="true" SkinID="ddlRequired" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                                         </asp:DropDownList>      
                                                     </td>
                                                 </tr>
                                                 
                                                 <tr>
                                                     <td class="space" align="left">                                        
                                                         <asp:Label ID="Lbl_aw_type" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True"><span style="Color:Red">*</span>Allowance Type</asp:Label>
                                                     </td>
                                                     <td class="space" align="left">
                                                         <%--<asp:DropDownList ID="DDL_aw_Type" runat="server" SkinID="ddlRequired" Visible="true"  OnSelectedIndexChanged="DDL_aw_Type_SelectedIndexChanged"  
                                                             AutoPostBack="true" CssClass="form-control selectpicker frmddl" data-live-search="true">
                                                             
                                                             <asp:ListItem Value="1">HQ</asp:ListItem>
                                                             <asp:ListItem Value="2">EX</asp:ListItem>
                                                             <asp:ListItem Value="3">OS</asp:ListItem>
                                                             <asp:ListItem Value="4">OS-EX</asp:ListItem>
                                                         </asp:DropDownList>--%>

                                                         <asp:DropDownList ID="DDL_aw_Type" runat="server" SkinID="ddlRequired" Visible="true"  
                                                             CssClass="form-control frmddl" data-live-search="true">
                                                             <%--<asp:ListItem Value="0">---Select---</asp:ListItem>--%>
                                                             <asp:ListItem Value="1">HQ</asp:ListItem>
                                                             <asp:ListItem Value="2">EX</asp:ListItem>
                                                             <asp:ListItem Value="3">OS</asp:ListItem>
                                                             <asp:ListItem Value="4">OS-EX</asp:ListItem>
                                                         </asp:DropDownList>

                                                     </td>

                                                     <td class="space" align="left">
                                                         <asp:Label ID="lblTown" SkinID="lblMand" runat="server" Visible="true" Font-Bold="True"><span style="Color:Red"></span>Town</asp:Label>
                                                     </td>
                                             
                                                     <td class="space" align="left">
                                                       <%--  <asp:DropDownList ID="ddl_town" runat="server" SkinID="ddlRequired" Visible="true"  AutoPostBack="true" 
                                                             CssClass="form-control selectpicker frmddl" data-live-search="true"> 
                                                         </asp:DropDownList>--%>

                                                           <asp:DropDownList ID="ddl_town" runat="server" SkinID="ddlRequired" Visible="true" 
                                                               CssClass="form-control frmddl" data-live-search="true"> 
                                                         </asp:DropDownList>

                                                     </td>

                                                     <td class="space" align="left">
                                                         <asp:Label ID="lblstay" SkinID="lblMand" runat="server" Visible="false" Font-Bold="True"><span style="Color:Red">*</span>Stay Place</asp:Label>
                                                     </td>
                                                     
                                                     <td class="space" align="left">
                                                         <asp:DropDownList ID="DDLStay" runat="server" SkinID="ddlRequired" Visible="false" CssClass="form-control frmddl" data-live-search="true">                        

                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                             </tbody>
                                         </table>
                                     </asp:Panel>
                                     
                                     <br />
                                     <asp:Panel ID="Panel3" runat="server">
                                         <table border="0" cellpadding="0" cellspacing="0" id="tblad" style="width:  100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white; padding: 6px;">
                                                     &nbsp;Additional Fields&nbsp;                            
                                                 </td>
                                             </tr>
                                         </table>
                                         <table border="0" cellpadding="3" cellspacing="3" id="additionalField" style="width: 100%;margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                             <tbody></tbody>    
                                         </table>               
                                     </asp:Panel>
                                 </div>

                                 <div class="col-lg-5" style="float:right;">
                                     <br />
                                     <asp:Panel ID="Panel2" runat="server">
                                        <%-- <table border="0" cellpadding="0" cellspacing="0" id="Table1" style="width: 100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white; padding: 6px;">
                                                     &nbsp;Distributor&nbsp;
                                                     <asp:CheckBox ID="chkBox" runat="server" align="right" Text="Select All" OnClick="CheckAll(this);" />
                                                 </td>
                                             </tr>
                                         </table>
                                         <table border="1" cellpadding="0" id="tblchkboxLocation" cellspacing="0" style="width: 100%;margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                             <tr>
                                                 <td class="style71" align="left">
                                                     <asp:CheckBoxList ID="chkboxLocation" runat="server" CssClass="chkboxLocation" Font-Names="Verdana"
                                                         Font-Bold="true" ForeColor="BlueViolet" Font-Size="X-Small" RepeatColumns="3"  AutoPostBack="true" 
                                                         RepeatDirection="Horizontal" TabIndex="29" OnSelectedIndexChanged="chkboxLocation_SelectedIndexChanged">                                                      
                                                     </asp:CheckBoxList>                                                                                                                                 
                                                 </td>                                 
                                             </tr>
                                         </table>--%>

                                         <table border="0" cellpadding="0" cellspacing="0" id="Table3" style="width: 100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white; padding: 6px;">
                                                     &nbsp;Distributor&nbsp;
                                                     <asp:CheckBox ID="checkAll" runat="server" align="right" Text="Select All" OnClick="CheckAll(this);" />
                                                 </td>
                                             </tr>
                                         </table>
                                         <table border="0" cellpadding="0" id="chkboxLocation" cellspacing="0" style="width: 100%;margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                             <tbody></tbody>                                             
                                         </table>

                                         <br />
                                         <table border="0" cellpadding="0" cellspacing="0" id="Table2" style="width: 100%;">
                                             <tr>
                                                 <td rowspan="" class="style65" align="left" style="background-color: #A6A6D2; color: white; padding: 6px;">
                                                     &nbsp;Field Force&nbsp;
                                                 </td>
                                             </tr>
                                         </table>
                                         
                                         <table border="0" cellpadding="0" id="chkboxDDLFO" cellspacing="0" style="width: 100%;margin-bottom: 0px; margin-right: 0px; margin-top: 15px;">
                                             <tbody></tbody>                                             
                                         </table>
                                     </asp:Panel>
                                 </div>
                             </div>
                             <center>
                                 <table cellpadding="5" cellspacing="10" width="8%">
                                     <tr>
                                         <td>
                                             <button type="button" class="btn btn-primary btnSave" id="btnSave">Save</button>
                                             <asp:Button ID="btnSaved"  CssClass="btn btn-primary" runat="server" Text="Save" Visible="false" />
                                         </td>
                                         <td>
                                             <asp:Button ID="btnClear" CssClass="btn btn-primary" runat="server" 
                                                 Text="Clear" OnClick="btnClear_Click" />
                                         </td>
                                     </tr>
                                 </table>
                             </center>                             
                         </div>
                     </div>                    
                 </div>
             </form>
                        
            <script type="text/javascript" src="../css_new/1.12.1/jquery-ui.js"></script>
            <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
            <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>            
            <script type="text/javascript" src="../css_new/1.8.3/jquery.min.js"></script>

            <script type="text/javascript">

                var BindData = []; var CFBindData = []; var DistName = []; var sfon = [];
                var CFldType = ""; var SFldType = ""; var MSFldType = ""; var RBFlType = "";
                $(document).ready(function () {

                    GetRouteTown(); 
                    FillTerritoryName(); 
                    GetCustomFormsFields(); 

                    var pageURL = window.location.search.substring(2);
                    var urlQS = pageURL.split('&');
                    var Territory_Code = 0;
                    if (urlQS.length > 0) {
                        for (var i = 0; i < urlQS.length; i++) {
                            var paramName = urlQS[i].split('=');
                            Territory_Code = paramName[1];                           
                        }
                    }
                    
                    console.log(Territory_Code);

                    if (Territory_Code > 0) {
                        
                        BindMainData(Territory_Code);

                        BindCustomFieldData(Territory_Code);
                    }
                   
                });


                function BindMainData(Territory_Code) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_Detail.aspx/GetBindData",
                        data: "{'TerritoryCode':'" + Territory_Code + "'}",
                        dataType: "json",
                        success: function (data) {
                            BindData = JSON.parse(data.d) || [];

                            if (BindData.length > 0) {
                                for (var j = 0; j < BindData.length; j++) {
                                    var sfcode = BindData[j].SF_Code;
                                    var Dist_Name = BindData[j].Dist_Name;
                                    var Territory_Id = BindData[j].Territory_Sname;
                                    console.log(Territory_Id);
                                    $('#<%=ddlTerritoryName.ClientID%>').val(Territory_Id);

                                     if (Territory_Id.length > 0) {
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
                        url: "New_Territory_Detail.aspx/GetBindCustomFieldData",
                        data: "{'TerritoryCode':'" + Territory_Code + "'}",
                        dataType: "json",
                        success: function (data) {
                            CFBindData = JSON.parse(data.d) || [];

                            if (CFBindData.length > 0) {
                                for (var j = 0; j < CFBindData.length; j++) {
                                    var rowcount = $('#additionalField TBODY').find('tr').length;
                                    if (rowcount > 0) {
                                        $('.caustField').each(function () {
                                            var $fldnm = $(this).attr('id');  
                                            //alert($fldnm);
                                            var $cval = CFBindData[j][$fldnm];
                                            //alert($cval);

                                            if ($cval != "") {
                                                if (CFldType == "CheckboxesList") {

                                                }
                                                if ($(this).find('input[type="checkbox"]')) {
                                                    if (CFldType == "") {

                                                    }

                                                } else {

                                                }
                                                $(this).find('input[type="checkbox"]').each(function () {


                                                });
                                                $(this).val($cval);
                                            }
                                        });
                                        //$('#additionalField TBODY > tr').each(function () {
                                        //    $('td', this).each(function () {
                                                
                                        //        var $fldnm = $(this).attr('id');  
                                        //        alert($fldnm);
                                        //        var cval = CFBindData[j][$fldnm];
                                        //        $(this).val(cval);
                                        //    });
                                        //});                             
                                    }
                                 }
                             }
                         },
                         error: function (data) {
                             alert(JSON.stringify(data.d));
                         }
                     });       
                }

                $('#btnSave').click(function () {
                    //alert('Hi');

                    SaveCustomFielsValues();
                    //window.location.href = "New_Territory_Detail.aspx?&Territory_Code=" + route_C + "&state=" + state + "&HQ=" + HQ + "&SF=" + SF + "&HQNm=" + HQNm + "";
                });

                $('#btnback').on('click', function () {
                    window.location.href = 'Route_Master.aspx';
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
                        url: "New_Territory_Detail.aspx/Loadstaypln",
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
                        url: "New_Territory_Detail.aspx/GetDistributorList",
                        data: "{'TerritoryName':'" + TerritoryNo + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            DistbNames = JSON.parse(data.d) || [];

                            $("#chkboxLocation TBODY").html("");
                            var str = "<tr>";
                            j = 0;
                            for (var i = 0; i < DistbNames.length; i++) {

                                var id = DistbNames[i].Stockist_Code;
                                var txt = DistbNames[i].Stockist_Name;
                               
                                str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id=" + 'chk' + i + " name='country' class='chkRow' value='" + id + "' />&nbsp;&nbsp;" + txt +"<td>";
                                //str += "<td><label for=" + 'chk' + i + ">" + txt + "</label></td>";

                                j++;
                                if (j == 3) {
                                    str += "</tr><tr>";
                                    j = 0;
                                }
                            }
                            str += "</tr>";

                            $("#chkboxLocation TBODY").append(str);

                            if (DistName.length > 0) {
                                for (var d = 0; d < DistName.length; d++) {

                                    $("#chkboxLocation TBODY").find('input[type="checkbox"]').each(function () {
                                        var chkbval = $(this).closest('tr').find('input[type="checkbox"]').val();

                                        if ((DistName[d] == chkbval)) {
                                            $(this).closest('tr').find('input[type="checkbox"]').prop('checked', 'checked');
                                        }

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
                        url: "New_Territory_Detail.aspx/GetFieldForceList",
                        data: "{'TerritoryName':" + TerritoryCode + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            FOName = JSON.parse(data.d) || [];


                            $('#chkboxDDLFO TBODY').html("");
                            var str = "<tr>";
                            j = 0;
                            for (var i = 0; i < FOName.length; i++) {

                                var id = FOName[i].Sf_Code;
                                var txt = FOName[i].Sf_Name;

                                str += "<td style='font-weight: bold; font-size:10px;'><input type='checkbox' id=" + 'chfnk' + i + " name='tfieldforce' class='chkfnRow' value='" + id + "' />&nbsp;&nbsp;" + txt + "<td>";
                                /* str += "<td><label for=" + 'chfnk' + i + ">" + txt + "</label></td>";*/

                                j++;
                                if (j == 3) {
                                    str += "</tr><tr>";
                                    j = 0;
                                }
                            }
                            str += "</tr>";

                            $('#chkboxDDLFO TBODY').append(str);
                            console.log(sfon.length);
                            
                            if (sfon.length > 0) {

                                for (var n = 0; n < sfon.length; n++) {

                                    $("#chkboxDDLFO TBODY").find('input[type="checkbox"]').each(function () {
                                        var chkval = $(this).closest('tr').find('input[type="checkbox"]').val();

                                        if ((sfon[n] == chkval)) {
                                            $(this).closest('tr').find('input[type="checkbox"]').prop('checked', 'checked');
                                        }
                                        //alert(chkval);
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
                    var MasFrms = []; var sselectionType; var mselectionType; var cbselectionType; var ssmtablename; var ssmcolumnname; var ssmcontrolerId = '';
                    var smmtablename; var smmcolumnname; var smmcontrolerId = ''; var cmmtablename; var cmmcolumnname; var cmmcontrolerId = '';
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_Detail.aspx/GetCustomFormsFieldsList",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'4'}",
                        dataType: "json",
                        success: function (data) {
                            MasFrms = JSON.parse(data.d) || [];

                            if (MasFrms.length > 0) {
                                $("#additionalField TBODY").html("");
                                var str = "<tr>";
                                j = 0;
                                for (var i = 0; i < MasFrms.length; i++) {
                                    var FldType = MasFrms[i].Fld_Type;
                                    var Mandate = MasFrms[i].Mandate;
                                    str += "<td class='space' align='left'><label>" + ((Mandate == "Yes") ? "<span style='Color:Red'>*</span>" : "") + MasFrms[i].Field_Name + "</label></td>";

                                    switch (FldType) {
                                        case 'TA':
                                            str += "<td class='stylespc' align='left'><input type='text' id=" + MasFrms[i].Field_Col + "' name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + " /></td>";
                                            break;
                                        case 'TAS':
                                            str += "<td class='stylespc' align='left'><input type='text' id=" + MasFrms[i].Field_Col + "' name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + " /></td>";
                                            break;
                                        case 'TAM':
                                            str += "<td class='stylespc' align='left'><textarea type='text' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "></textarea></td>";
                                            break;
                                        case 'NC':
                                            str += "<td class='stylespc' align='left'>";
                                            str += "<div class='row'>";
                                            str += "<div class='col-sm-6'>";
                                            str += "<div class='input-group input-group-sm mb-3' style='display: flex'>";
                                            str += "<div class='input-group-prepend'>";
                                            str += "<div class='input-group-text' style='width:50px; padding: 5px 2px 5px 5px; background: #868383; color: white; border-radius: 4px 0px 0px 4px;' id='NCS'>" + MasFrms[i].Fld_Symbol + "</div>";
                                            str += "</div>";
                                            str += "<input type='number' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "/>";
                                            str += "</div>";
                                            str += "</div>";
                                            str += "</div>";
                                            str += "</td>";
                                            break;
                                        case 'NP':
                                            str += "<td class='stylespc' align='left'><input type='number' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + " /></td>";
                                            break;
                                        case 'N':
                                            str += "<td class='stylespc' align='left'><input type='number' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "/></td>";
                                            break;
                                        case 'DR':
                                            str += "<td class='stylespc' align='left'><input type='date' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "/></td>";
                                            break;
                                        case 'D':
                                            str += "<td class='stylespc' align='left'><input type='date' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "/></td>";
                                            break;
                                        case 'TR':
                                            str += "<td class='stylespc' align='left'><input type='time' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "/></td>";
                                            break;
                                        case 'T':
                                            str += "<td class='stylespc' align='left'><input type='time' id=" + MasFrms[i].Field_Col + " name=" + MasFrms[i].FldType + "  class='from-control caustField frmddl' maxLength=" + MasFrms[i].Fld_Length + "/></td>";
                                            break;
                                        case 'SSM':
                                            str += "<td class='stylespc' align='left'><select name=" + MasFrms[i].FldType + " id=" + MasFrms[i].Field_Col + " class='from-control selectpicker caustField frmddl' data-live-search='true' /></td>";
                                            ssmtablename = MasFrms[i].Fld_Src_Name; ssmcolumnname = MasFrms[i].Fld_Src_Field;
                                            ssmcontrolerId = MasFrms[i].Field_Col;
                                            sselectionType = "SingleSelectionddl"; SFldType = "SingleSelectiondd";
                                            break;
                                        case 'SMM':
                                            str += "<td class='stylespc' align='left'><select name=" + MasFrms[i].FldType + " data-dropup-auto='false' id=" + MasFrms[i].Field_Col + " class='from-control  selectpicker multiddl caustField' multiple='multiple' data-live-search='true' /></td>";
                                            smmtablename = MasFrms[i].Fld_Src_Name; smmcolumnname = MasFrms[i].Fld_Src_Field;
                                            smmcontrolerId = MasFrms[i].Field_Col;
                                            mselectionType = "MultipleSelectionddl"; MSFldType ="MultipleSelectiondd"
                                            break;
                                        case 'CM':
                                            str += "<td class='stylespc' align='left'><div name=" + MasFrms[i].FldType + " id=" + MasFrms[i].Field_Col + " class='caustField ChbControl frmddl'></div></td>";
                                            cmmtablename = MasFrms[i].Fld_Src_Name; cmmcolumnname = MasFrms[i].Fld_Src_Field;
                                            cmmcontrolerId = MasFrms[i].Field_Col;
                                            cbselectionType = "CheckboxListControl"; CFldType = "CheckboxesList";
                                            break;
                                        case 'RM':
                                            str += "<td class='stylespc' align='left'><div name=" + MasFrms[i].FldType + " id=" + MasFrms[i].Field_Col + " class='caustField ChbControl frmddl'></div></td>";
                                            cmmtablename = MasFrms[i].Fld_Src_Name; cmmcolumnname = MasFrms[i].Fld_Src_Field;
                                            cmmcontrolerId = MasFrms[i].Field_Col;
                                            cbselectionType = "RadiobuttonListControl"; RBFlType ="RadiobuttonsList"
                                            break;
                                        case 'FS':
                                            str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[i].Field_Col + " name = " + MasFrms[i].FldType + "  accept='image/png, image/jpeg' class='from-control caustField frmddl' /></td>";
                                            break;
                                        case 'FC':
                                            str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[i].Field_Col + " name = " + MasFrms[i].FldType + "  accept='image/png, image/jpeg' class='from-control caustField frmddl' /></td>";
                                            break;
                                        case 'F':
                                            str += "<td class='stylespc' align='left'><input type='file'  id = " + MasFrms[i].Field_Col + " name = " + MasFrms[i].FldType + "  accept='image/png, image/jpeg' class='from-control caustField frmddl' /></td>";
                                            break;
                                        default:
                                            break
                                    }
                                    j++;
                                    if (j == 2) {
                                        str += "</tr><tr>";
                                        j = 0;
                                    }
                                }
                                str += "</tr>";
                                //var tr = $(tr).html(str);
                                //$(tr).html(str);
                                $("#additionalField TBODY").append(str);
                                //$("#additionalField tbody").append('<tr class="alcode">' + str + '</tr>');

                                if (sselectionType == "SingleSelectionddl") {
                                    BindDropdown(ssmtablename, ssmcolumnname, ssmcontrolerId);
                                }

                                if (mselectionType == "MultipleSelectionddl") {
                                    BindDropdown(smmtablename, smmcolumnname, smmcontrolerId);
                                }

                                if (cbselectionType == "CheckboxListControl") {
                                    var filtcmgr = [];
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        async: false,
                                        url: "New_Territory_Detail.aspx/GetCustomFormsSeclectionMastesList",
                                        data: "{'TableName':'" + cmmtablename + "','ColumnsName':'" + cmmcolumnname + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            filtcmgr = JSON.parse(data.d) || [];
                                            console.log(filtcmgr); var html = '';
                                            //alert('#' + ssmcontrolerId + '');
                                            if (filtcmgr.length > 0) {
                                                html = '<table id="tChblControl" class="frmddl tChblControl">';

                                                //var html = '';
                                                html += "<tr>";
                                                var m = 0;
                                                for (var k = 0; k < filtcmgr.length; k++) {
                                                    html += "<td><input type='checkbox' id='cb" + k + "' value='" + filtcmgr[k].IDCol + "' name='customchblist' ></td>";
                                                    html += "<td><label for='cb" + k + "'>" + filtcmgr[k].TextVal + " </label></td>";
                                                    //str += "<td class='stylespc' align='left'><input type='checkbox' id='cb" + filtmgr[j].IDCol + "' value='" + filtmgr[j].IDCol + "' /></td>";

                                                    //str += "<td class='stylespc' align='left'><label for='cb" + filtmgr[j].IDCol + "'>" + filtmgr[j].TextVal + " </label></td>";
                                                    m++;
                                                    if (m == 2) {
                                                        html += "</tr><tr>";
                                                        m = 0;
                                                    }
                                                }
                                                html += "</tr>";
                                                html += '</table>';
                                            }
                                            $(".ChbControl").append(html);
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
                        url: "New_Territory_Detail.aspx/GetCustomFormsSeclectionMastesList",
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

                    $('.caustField').each(function (index, v) {

                        var adDetail = {};
                        var chblistval = "";
                        var fields = `${this.id}`;
                        var values = "";

                        $(this).find('input[type="checkbox"]').each(function () {
                          
                            if ($(this).is(":checked")) {
                                var id = $(this).val();
                                chblistval += id + ",";
                            }
                            else { chblistval = ""; }
                          
                        });

                        if (chblistval.length != "") {
                            values = chblistval;
                        }
                        else if (chblistval == "")
                        {
                            values = "";
                        }
                        else {
                            values = `${v.value}`;
                        }

                        adDetail.Fields = fields;
                        adDetail.Values = values;
                        
                        addfields.push(adDetail);

                    });                                       

                    var dist_name = "";
                    $('input[name=country]').each(function (index, item) {
                        if ($(this).is(":checked")) {
                            var id = $(this).val();
                            var country = $(this).closest('tr').find('label').html();
                            dist_name += id + ",";                            
                        }
                    });

                    var sf_code = '';

                    $('input[name=tfieldforce]').each(function (index, item) {
                        if ($(this).is(":checked")) {
                            var id = $(this).val();
                            var country = $(this).closest('tr').find('label').html();
                            sf_code += id + ",";
                        }
                    });

                    var Maindata = {};
                    $('#<%=txtRoutecode.ClientID%>').val();
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
                    var fdata = JSON.stringify({ routemainflds });
                    console.log(fdata);

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_Territory_Detail.aspx/SaveAdditionalField",
                        //data: "{'addfields':'" + JSON.stringify(addfields) + "','routemainflds':'" + JSON.stringify(routemainflds) + "'}",
                        data: JSON.stringify({ routemainflds }),
                        dataType: "json",               
                        success: function (msg) {
                            alert(msg.d);
                            window.location.href = "Route_Master.aspx";
                            ClearControls();
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
                        url: "New_Territory_Detail.aspx/GetRouteTown",
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
                        url: "New_Territory_Detail.aspx/FillTerritoryName",
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

               
                
            </script>
        </body>
    </html>
</asp:Content>


