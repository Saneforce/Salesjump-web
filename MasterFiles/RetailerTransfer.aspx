<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RetailerTransfer.aspx.cs" Inherits="MasterFiles_RetailerTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
	<form id="frm1" runat="server" style="height: 550px;">
    <div class="row">
        <div class="col-lg-12 sub-header">
            Retailer Transfer
        </div>
    </div>
    <div class="col-md-offset-3">
        <div class="row" style="align-items: center;">
            <div class="col-xs-2">
                <label>From Route</label>
            </div>
            <div class="col-xs-8">
                <select id="ddlfrom" name="ddlsf" style="width: 250px;"></select>
            </div>
        </div>
        <div class="row" style="margin-top: 10px;">
            <div class="col-xs-2">
                <label>Retailers</label>
            </div>
            <div class="col-sm-8">
                <select id="mselectret" class="selectpicker" data-live-search="true" data-dropup-auto="false" multiple data-actions-box="true">
                </select>
            </div>
        </div>
        <div class="row" style="margin-top: 10px;">
            <div class="col-xs-2">
                <label>To Route</label>
            </div>
            <div class="col-sm-8">
                <select id="ddlto">
                </select>
            </div>
        </div>
        <button type="button" class="btn btn-primary col-md-offset-2" style="margin-top: 20px;" id="btnsave">Save</button>
    </div>
	</form>
    <script type="text/javascript">
        let divcode = '';
        let routeParsed = [];
        function fillRetailers(selroute) {
		$("#mselectret").val('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerTransfer.aspx/getRetailers",
                data: "{'divcode':'<%=Session["div_code"]%>','routecode':'" + selroute + "'}",
                dataType: "json",
                success: function (data) {
                    let retailerData = JSON.parse(data.d) || [];
                    filldata("mselectret", retailerData, "ListedDrCode", "ListedDr_Name", "Retailers")
                    //$('#mselectret').multiselect({
                    //    columns: 3,
                    //    placeholder: 'Select Retailer',
                    //    search: true,
                    //    searchOptions: {
                    //        'default': 'Search Retailer'
                    //    },
                    //    selectAll: true
                    //}).multiselect('reload');
                    //$('.ms-options ul').css('column-count', '3');
                }
            });
            $("#mselectret").selectpicker('refresh');
        }
        function filldata($id, $arr, $val, $name, $txt) {
            let ddldropdown = $(`#${$id}`);
            if ($txt != "Retailers") {
                ddldropdown.empty().append(`<option value="0">Select ${$txt}</option>`);
            }
            for (var i = 0; i < $arr.length; i++) {
                ddldropdown.append($('<option value="' + $arr[i][$val] + '">' + $arr[i][$name] + '</option>'));
            }
        }
        function fillRoute() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RetailerTransfer.aspx/GetRouteDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    routeParsed = JSON.parse(data.d);
                    filldata("ddlfrom", routeParsed, "Territory_Code", "Territory_Name", "Route");
                }
            });
            $("#ddlfrom").selectpicker({
                liveSearch: true
            });
            $("#ddlto").selectpicker({
                liveSearch: true
            });
        }
        $(document).ready(function () {
            divcode = Number('<%=Session["div_code"]%>');
            fillRoute();
            $('#ddlfrom').on('change', function () {
                let selRoute = $('#ddlfrom').val();
                let idx = routeParsed.findIndex(x => x.Territory_Code == selRoute);
                fillRetailers(selRoute);
                let filtrouteParsed = routeParsed.filter(function (a) {
                    return a.Territory_Code != routeParsed[idx].Territory_Code;
                })
                filldata("ddlto", filtrouteParsed, "Territory_Code", "Territory_Name", "Route");
                $("#ddlto").selectpicker('refresh');
            })
            $('#btnsave').on('click', function () {
                let selectedFromRoute = $('#ddlfrom').val();
                if (selectedFromRoute == "0") {
                    alert("Select From Route");
                    return false;
                }
                var selectedRetailers = '';
                var sdiv = $('#mselectret').val() || [];
                if (sdiv.length > 0) {
                    for (var i = 0; i < sdiv.length; i++) {
                        selectedRetailers += '\'' + sdiv[i] + '\',';
                    }
                }
                if (selectedRetailers == '') {
                    alert('Select Atleast One Retailer');
                    return false;
                }
                let selectedToRoute = $('#ddlto').val();
                if (selectedToRoute == "0" || selectedToRoute == selectedFromRoute || selectedToRoute == null) {
                    alert("Select To Route");
                    return false;
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerTransfer.aspx/saveTransfer",
                    data: '{"Divcode": "' + divcode + '", "fromRoute": "' + selectedFromRoute + '", "toRoute": "' + selectedToRoute + '", "retailerData": "' + selectedRetailers + '"}',
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        window.location.href = "RetailerTransfer.aspx";
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
        });
    </script>
</asp:Content>
