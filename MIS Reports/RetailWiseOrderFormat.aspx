<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RetailWiseOrderFormat.aspx.cs" Inherits="MIS_Reports_SampleReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <form runat="server">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <script type="text/javascript">
            $(function BUTTON() {
                debugger;
                <%-- $("#<%=Button1.ClientID%>").bind("click", function () {--%>
                $("#btn1").bind("click", function () {
                   $('#<%=HiddenField1.ClientID%>').val($('#FromDate').val())
                   $('#<%=HiddenField2.ClientID%>').val($('#EndDate').val())
                });
            });
        </script>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/jscript">
             $(document).ready(function () {
                 <%-- $('#<%=Button1.ClientID%>').on('click', function () {--%>
                 $('#btn1').on('click', function () {
                    <%--var sfc = $('#<%=ddlfieldforce.ClientID%>').val();--%>
                    var sfc = $('#ddlfieldforce').val();
                    if (sfc == 0) {
                        alert('Select FieldForce')
                        return false;
                    }
                    var route = $('#ddlRoute').val();
                    if (route == 0) {
                        alert('Select Route')
                        return false;
                    }
                    var retailer = $('#ddlRetailer').val();
                    if (retailer == 0) {
                        alert('Select Retailer')
                        return false;
                    }
                });
            });

            
        </script>                   
        <div align="center">
        <table border="0" cellpadding="3" cellspacing="3" id="tblStateDtls">
            <tr>
                <td align="left" class="stylespc">
                    <%--<asp:Label ID="lblFieldForce" runat="server" SkinID="lblMand" Width="90px"
                        Height="18px">Field Force</asp:Label>--%>
                    <label id="lblFieldForce">Field Force</label>
                </td>
                <td style="width: 52px">
                    <%--<asp:DropDownList ID="ddlfieldforce" runat="server" SkinID="ddlRequired"
                        TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlfieldforce_SelectedIndexChanged">
                    </asp:DropDownList><--%>
                    <select id="ddlfieldforce" data-live-search="true" data-dropup-auto="false"></select>
                </td>
            </tr>
            <tr>
                <td><label id="lblRoute">Route</label></td>
                <td style="width: 52px">
                    <%--<asp:DropDownList ID="ddlRoute" runat="server" SkinID="ddlRequired"
                        TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <select id="ddlRoute" data-live-search="true" data-dropup-auto="false"></select>
                </td>
            </tr>
            <tr>
                <td><label id="lblRetailer">Retailer</label></td>
                <td style="width: 52px"><select id="ddlRetailer" data-live-search="true" data-dropup-auto="false" />
                   </td>
            </tr>
            <tr>
                <td><label id="lblFromDate">From Date</label></td>
                <td style="width: 52px">
                    <input type="date" id="FromDate" name="trip-start"                        
                        min="2001-01-01" max="2020-12-31" /></td>
            </tr>
            <tr>
                <td><label id="lblToDate">ToDate</label></td>
                <td style="width: 52px">
                    <input type="date" id="EndDate" name="trip-start"                        
                        min="2001-01-01" max="2021-12-31" /></td>
            </tr>
            <tr>
                <td></td>
                <td style="width: 52px">
                    <%--<asp:Button ID="Button1" runat="server" Text="View" OnClientClick="BUTTON()" CssClass="btn btn-primary" OnClick="Button1_Click" />--%>
                    <input type="button" id="btn1" value="View" />
                </td>
            </tr>
        </table>
            </div>
    </form>
    <script type="text/javascript">
        function radiochange() {            
             $('#ddlfieldforce').html("");
            sf = '<%=Session["Sf_Code"]%>';
              $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SecondaryOrder_Summary.aspx/getMGR",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFStates = JSON.parse(data.d) || [];
                        if (SFStates.length > 0) {
                            var states = $("#ddlfieldforce");
                            states.empty().append('<option selected="selected" value="0">Select fieldforce</option>');
                            for (var i = 0; i < SFStates.length; i++) {
                                states.append($('<option value="' + SFStates[i].sf_code + '">' + SFStates[i].sf_Name + '</option>'))
                            }
                        }
                    }
              });                       
             $('#ddlfieldforce').selectpicker({
                liveSearch: true
            });
         }              
        function loadfieldforce(sf) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailWiseOrderFormat.aspx/getMGR",
                    data: "{'divcode':'<%=Session["div_code"]%>','sf_code':'" + sf + "'}",
                   <%-- data: "{'divcode':'<%=Session["div_code"]%>'}",--%>
                    dataType: "json",
                    success: function (data) {
                        SFDivi = JSON.parse(data.d) || [];
                        if (SFDivi.length > 0) {
                            var divi = $("#ddlfieldforce");
                            //divi.empty();
                            divi.empty().append('<option selected="selected" value="0">Select Field Force</option>');
                            for (var i = 0; i < SFDivi.length; i++) {
                                divi.append($('<option value="' + SFDivi[i].Sf_Code + '">' + SFDivi[i].Sf_Name + '</option>'));
                            }
                        }
                    }
                });
            $('#ddlfieldforce').selectpicker({
                    liveSearch: true
                });
         }
        function loadRoute(ssdiv) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailWiseOrderFormat.aspx/GetRoute",
                    data: "{'divcode':'<%=Session["div_code"]%>','sf_code':'" + ssdiv + "'}",
                    <%--data: "{'sf_code':'" + ssdiv + "','divcode':'<%=Session["div_code"]%>'}",--%>
                    dataType: "json",
                    success: function (data) {
                        var SFDepts = JSON.parse(data.d) || [];
                        if (SFDepts.length > 0) {
                            var dept = $("#ddlRoute");
                            dept.empty().append('<option selected="selected" value="0">Select Route</option>');                           
                            for (var i = 0; i < SFDepts.length; i++) {
                                dept.append($('<option value="' + SFDepts[i].Territory_Code + '">' + SFDepts[i].Territory_Name + '</option>'))
                            }
                        }
                    }
                });
            $('#ddlRoute').selectpicker('refresh');
        }
        function loadRetailer(ro,route) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailWiseOrderFormat.aspx/GetRetailer",
                    data: "{'sf_code':'"+ro+"','div_code':'<%=Session["div_code"]%>','routecode':'" + route + "'}",                    
                    dataType: "json",
                    success: function (data) {
                        var SFDepts = JSON.parse(data.d) || [];
                        if (SFDepts.length > 0) {
                            var dept = $("#ddlRetailer");
                            dept.empty().append('<option selected="selected" value="0">Select Retailer</option>');
                            for (var i = 0; i < SFDepts.length; i++) {
                                dept.append($('<option value="' + SFDepts[i].ListedDrCode + '">' + SFDepts[i].ListedDr_Name + '</option>'))
                            }
                        }
                    }
                });
             $('#ddlRetailer').selectpicker('refresh');
        }
        $(function () {
            $("#btn1").bind("click", function () {
                var url = "ReportFinal.aspx?FieldForce=" + encodeURIComponent($("#ddlfieldforce").val()) + "&Route=" + encodeURIComponent($("#ddlRoute").val()) + "&Retailer=" + encodeURIComponent($("#ddlRetailer").val()) + "&FromDate=" + encodeURIComponent($('#<%=HiddenField1.ClientID%>').val()) + "&EndDate="+encodeURIComponent($('#<%=HiddenField2.ClientID%>').val());
                window.location.href = url;
            });
        });
         $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';            
             loadfieldforce(sf);
             $('#ddlfieldforce').on('change', function () {
                 var sdiv = $(this).val() || [];
                 loadRoute(sdiv);
             });
             $('#ddlRoute').on('change', function () {
                 var route = $(this).val() || [];
                 var ro = $('#ddlfieldforce').val() || [];
                 loadRetailer(ro,route);
             });
           });
    </script>
</asp:Content>

