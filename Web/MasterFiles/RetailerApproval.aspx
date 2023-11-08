<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="RetailerApproval.aspx.cs" Inherits="MasterFiles_RetailerApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
            <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
            <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
            <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
            <link href="../../css/style.css" rel="stylesheet" />
            
        </head>
        <body>
            <form id="Retailerapproval" runat="server">
                <asp:LinkButton ID="btnExcel"  runat="Server" Style="padding: 0px 20px;float:right;" class="btn btnExcel" OnClick="btnexl_Click" />
                <div class="container" style="width: 100%;overflow:scroll;">
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="fieldsetting" style="width:30px;height:30px ;float: right;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:10px;">
                                <button type="button" class="btnsettings" id="btnsettings"><i class="fa fa-cog"></i></button>
                            </div>
                            <table id="RetailerTable" class="table-bordered newStly edit-table">
                                <thead>
                                    <tr>
                                        <th>SlNo.</th>
                                        <th>Field Force Name</th>
                                        <th>Approval</th>
                                        <th>Created Date</th>
                                        <th class="hidetd">Picture</th>
                                        <th>Retailer Name</th>
                                        <th class="hidetd">ERP Code</th>
                                        <th>Address</th>
                                        <%--<th class="hidetd">Area Name</th>--%>
                                        <th class="hidetd">Latitude</th>
                                        <th class="hidetd">Longitude</th>
                                        <th>City Name</th>
                                        <th class="hidetd">Landmark</th>
                                        <th>PIN Code</th>
                                        <th class="hidetd">Contact Person</th>
                                        <th class="hidetd">Designation</th>
                                        <th>Phone No.</th>
                                        <th>Route Name</th>
                                        <th>Channel</th>
                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
                        
                        <div class="modal fade" id="CustomFieldModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                            <div class="modal-dialog" role="document" style="width: 30% !important">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="CustomFieldModalLabel"></h5>
                                    </div>
                                    <div class="modal-body" style="padding-top: 10px">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="CustoFielddets" cellpadding="0" cellspacing="0" class="table" style="width: 100%; font-size: 12px;">
                                                    <thead class="text-warning">
                                                        <tr>
                                                            <th style="text-align: left">S.No</th>
                                                            <th style="text-align: left">Field Name</th>
                                                            <th class="hide" style="text-align: left">Field Column</th>
                                                            <th class="hide" style="text-align: left">Status</th>
                                                            <th style="text-align: left">
                                                                <input type="checkbox" name="checkAll" id="checkAll" class="checkAll" />
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody style="height:150px !important;overflow-x:scroll;"></tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" onclick="ApplyFields()" data-dismiss="modal">Apply</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="background: #dbdddf;position: fixed;width: 100%;bottom: 0px;padding: 5px 20px;margin-left: -30px;display:none">
                        <button id="btnSubmit" class="btn btn-primary btn-update">Update</button>
                    </div>
                </div>
                <div style="position:fixed;left:60%;top:50%;width:70%;height:70%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1">
                    <span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span>
                    <img alt="img" style="width:100%;height:100%;border-radius: 15px;" id="photo1" />
                </div>
            </form>

            <script type="text/javascript">
                var masChannel = []; var CFBindData = []; var MasFrms = []; var DisPlayF = []; var ARetailer = [];

                function updChannel(drcode, spcode, spname) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/updChannel",
                        data: "{'custCode':'" + drcode + "','spcode':'" + spcode + "','spname':'" + spname + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data.d);
                        },
                        error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                    });
                }

                function getChannel() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/getChannel",
                        dataType: "json",
                        success: function (data) {
                            masChannel = JSON.parse(data.d);
                        },
                        error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                    });
                }

                $(document).ready(function () {
                    updFlag = function (x) {
                        $(x).closest('TR').attr("data-ch", "1");
                    }

                    $(document).on('click', '.elpencil', function () {
                        $(this).closest('td').find('.hchannel').hide();
                        var schannel = $(this).closest('td').find('.rchannel');
                        schannel.empty().append('<option value="0">Select Channel</option>');
                        for (var $i = 0; $i < masChannel.length; $i++) {
                            schannel.append('<option value="' + masChannel[$i].Doc_Special_Code + '">' + masChannel[$i].Doc_Special_Name + '</option>');
                        }
                        schannel.show();
                    });

                    $(document).on('change', '.rchannel', function () {
                        $(this).hide();
                        var drcode = $(this).closest('tr').find('input[name="custCode"]').val();
                        var selChannel = $(this).val();
                        var selChannelNm = masChannel.filter(function (a) {
                            return a.Doc_Special_Code == selChannel;
                        }).map(function (el) {
                            return el.Doc_Special_Name
                        }).toString();
                        $(this).closest('td').find('.hchannel').hide();
                        var shchannel = $(this).closest('td').find('.hchannel');
                        var channelNm = $(this).closest('td').find('.channelfld');
                        $(channelNm).text(selChannelNm);
                        shchannel.show();
                        if (selChannel != "0") {
                            updChannel(drcode, selChannel, selChannelNm);
                        }
                    });

                    getRetilers();

                    getChannel();

                    getRetilers();

                    $(document).on('click', '.btnUpdate', function () {
                        lCode = $(this).closest('tr').find('input[name="custCode"]').val();
                        var erp = $(this).closest('tr').find('.erpcode').val();
                        var lat = $(this).closest('tr').find('.lati').val();
                        var longi = $(this).closest('tr').find('.longi').val();

                        if (confirm('Are you sure you want to Approve this Retailer?')) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "RetailerApproval.aspx/UpdateApprove",
                                data: "{'custCode':'" + lCode + "','erp':'" + erp + "','lat':'" + lat + "','longi':'" + longi + "'}",
                                dataType: "json",
                                success: function (data) {
                                    alert(data.d);
                                    getRetilers();
                                },
                                error: function (rs) {
                                    console.log(rs);
                                }
                            });
                        }
                        else {
                            alert('Retailer Not Approved as You pressed Cancel !');
                        }
                    });

                    $(document).on('click', '.btnCancel', function () {
                        lCode = $(this).closest('tr').find('input[name="custCode"]').val();
                        var cnf = prompt('Are you sure you want to Reject this Retailer?', 'Reason')
                        if (cnf != null) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "RetailerApproval.aspx/UpdateReject",
                                data: "{'custCode':'" + lCode + "','reasion':'" + cnf + "'}",
                                dataType: "json",
                                success: function (data) {
                                    alert(data.d);
                                    getRetilers();
                                },
                                error: function (rs) {
                                    console.log(rs);
                                }
                            });
                        }
                        else {
                            alert('Retailer Not Reject as You pressed Cancel!');
                        }

                    });

                    $(document).on('click', '.btn-update', function () {
                        if (confirm('Are you sure you want to Update ?')) {
                            var grid = $("#RetailerTable").closest('table');
                            var row = $(grid).find('tr');
                            if (row.length > 0) {
                                $data = '[';
                                $(row).each(function () {
                                    if ($(this).attr('data-ch') == "1") {
                                        var ccode = $(this).find('.code').val();
                                        var erp = $(this).find('.erpcode').val();
                                        var lat = $(this).find('.lati').val();
                                        var longi = $(this).find('.longi').val();
                                        if ($data != "[") $data += ",";
                                        $data += '{"custCode":"' + ccode + '","erp":"' + erp + '","lat":"' + lat + '","longi":"' + longi + '"}'
                                    }
                                });
                                $data += ']';
                                xhsrdata = JSON.parse($data)
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "RetailerApproval.aspx/Updatelatlong",
                                    data: "{'sCus':'" + $data + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        alert(data.d);
                                        getRetilers();
                                    },
                                    error: function (rs) {
                                        console.log(rs);
                                    }
                                });
                            }
                        }
                        else {
                            alert('Update canceled as You pressed Cancel!');
                        }
                    });

                    $(document).on('click', '.picc', function () {
                        //alert('hi');
                        var photo = $(this).attr("src");
                        $('#photo1').attr("src", $(this).attr("src"));
                        $('#cphoto1').css("display", 'block');
                        // $(this).append('<div style="width: 100%" ><img src="' + photo + '"/></div>'
                    });


                    //$('.picc').click(function () {
                    //    alert('hi');
                    //    var photo = $(this).attr("src");
                    //    $('#photo1').attr("src", $(this).attr("src"));
                    //    $('#cphoto1').css("display", 'block');
                    //    // $(this).append('<div style="width: 100%" ><img src="' + photo + '"/></div>'

                    //});

                    loadCustomFields();

                    ApplyFields();

                    loadDisPlayCustomFields();

                    $(document).on('click', '.btnsettings', function () {
                        $('#CustomFieldModal').modal('toggle');
                        $('#CustomFieldModalLabel').text("Addtional Fields for Route");
                        loadCustomFields();
                        CheckFields();
                    });

                    $('#checkAll').click(function () {
                        isChecked = $(this).prop("checked");
                        /*$('#CustoFielddets tbody').find('input[type="checkbox"]').prop('checked', isChecked);*/
                        //MasFrms[0].Field_Visible = isChecked
                        if (this.checked) {
                            $('#CustoFielddets tbody').find('input[type="checkbox"]').each(function () {
                                if ($(this).prop("checked") == false) {
                                    $(this).prop("checked", isChecked);
                                    var tblIndex = $(this).closest('tr').index();
                                    if (MasFrms.length > 0) {
                                        MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
                                    }
                                    var cbval = $(this).val();
                                    //alert(cbval);
                                    var ActiveView = "1";
                                    UpdateCutomRetailerData(cbval, ActiveView);
                                }
                            });
                        }
                        else {
                            $('#CustoFielddets tbody').find('input[type="checkbox"]').each(function () {

                                $(this).prop("checked", isChecked);
                                var tblIndex = $(this).closest('tr').index();
                                if (MasFrms.length > 0) {
                                    MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
                                }

                                var cbval = $(this).val();
                                //alert(cbval);
                                var ActiveView = "0";
                                UpdateCutomRetailerData(cbval, ActiveView);
                            });
                        }
                    });

                    $('#CustoFielddets tbody').on('click', 'input[type="checkbox"]', function (e) {

                        var isChecked = $(this).prop("checked");

                        var tblIndex = $(this).closest('tr').index();

                        if (isChecked == true) {
                            if (MasFrms.length > 0) {
                                MasFrms[tblIndex].Field_Visible = "1"
                            }
                            var cbval = $(this).val();
                            //alert(cbval);
                            var ActiveView = "1";
                            UpdateCutomRetailerData(cbval, ActiveView);
                        }
                        else {
                            var cbval = $(this).val();
                            //alert(cbval);
                            var ActiveView = "0";
                            UpdateCutomRetailerData(cbval, ActiveView);

                            if (MasFrms.length > 0) {
                                MasFrms[tblIndex].Field_Visible = "0";
                            }

                            var isHeaderChecked = $("#checkAll").prop("checked");

                            if (isChecked == false && isHeaderChecked) {
                                $("#checkAll").prop('checked', isChecked);
                            }
                        }

                    });
                });

                function closew() {
                    $('#cphoto1').css("display", 'none');
                }

                function loadDisPlayCustomFields() {
                    var ModuleId = "3";
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/DisPlayCutomFields",
                        data: "{'ModuleId':'" + ModuleId + "'}",
                        dataType: "json",
                        success: function (data) {

                            DisPlayF = JSON.parse(data.d) || [];

                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });

                }

                function fillAdditionalRetailer(ColumnName) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/GetAdditionalRetailer",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'3','ColumnName':'" + ColumnName + "'}",
                        dataType: "json",
                        success: function (data) {
                            ARetailer = JSON.parse(data.d) || [];

                        }
                    });
                }

                function loadCustomFields() {
                    var ModuleId = "3";
                    var Sf = 'admin';
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/GetCustomFormsFieldsColumns",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'" + ModuleId + "','Sf':'" + Sf + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#CustoFielddets TBODY').html("");
                            //Orders = JSON.parse(data.d) || [];
                            MasFrms = JSON.parse(data.d) || [];
                            //console.log(MasFrms);
                            if (MasFrms.length > 0) {
                                for (var i = 0; i < MasFrms.length; i++) {

                                    tr = $("<tr class='" + MasFrms[i].Field_Col + " row-select' id='" + MasFrms[i].Field_Col + "'></tr>");
                                    //var hq = filtered[$i].Sf_Name.split('-');
                                    slno = i + 1;
                                    var fldtype = MasFrms[i].Field_Type;
                                    var fldvisiable = MasFrms[i].Field_Visible;
                                    //alert(fldvisiable);
                                    //alert(fldtype);
                                    var checkbox = "";
                                    if (fldvisiable == "1") {
                                        checkbox = "<input type='checkbox' checked='checked' class='fldName' id='" + i + "' name='fldName' value='" + MasFrms[i].Field_Name + "' />";
                                    }
                                    else {
                                        checkbox = "<input type='checkbox' class='fldName' id='" + i + "' name='fldName' value='" + MasFrms[i].Field_Name + "' />";
                                    }

                                    $(tr).html('<td><i class="fa fa-ellipsis-v tbltrmove"></i></td><td class="fldsName">' + MasFrms[i].Field_Name +
                                        '</td><td class="hide fldcol">' + MasFrms[i].Field_Col +
                                        '</td><td class="hide fldtype"> ' + MasFrms[i].Field_Type +
                                        '</td><td class="hide fldvb"> ' + MasFrms[i].Field_Visible +
                                        '</td><td class="hide fldorder"> ' + MasFrms[i].Field_Order +
                                        '</td><td>' + checkbox + '</td>');
                                    $("#CustoFielddets TBODY").append(tr);

                                }
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });


                }

                function ApplyFields() {

                    var html_table_data = "";
                    var bRowStarted = true; var $tctd = ""; var colflg = true;

                    var tblheader = new Array();
                    $('#RetailerTable thead>tr').each(function () {
                        $('th', this).each(function () {
                            tblheader.push($(this).text());
                        });
                    });
                    var i = 0;
                    var rowcount = $("#CustoFielddets TBODY").find('tr').length;
                    if (rowcount > 0) {
                        $("#CustoFielddets TBODY").find('tr').each(function () {
                            $(this).find("input[type='checkbox']").each(function () {
                                if ($(this).is(":checked")) {
                                    var cbval = $(this).val();
                                    let CustItem = tblheader.filter(e => e.includes(cbval));
                                    console.log(CustItem);
                                    if (CustItem == 0) {
                                        if (cbval != "on") {
                                            $tctd += "<th style='text-align:left; color: #fff'>" + cbval + "</th>";
                                        }
                                    }
                                    else {
                                        if ($(this).prop('checked') == false) {
                                            $(this).prop('checked', true);
                                            $(this).prop('checked', 'checked');
                                        }
                                    }
                                }
                            });
                            i++;
                        });

                        $('#RetailerTable thead > tr').append($tctd);

                        getRetilers();
                    }
                }

                function CheckFields() {

                    var html_table_data = "";
                    var bRowStarted = true; var $td = ""; var colflg = true;

                    $('#RetailerTable thead>tr').each(function () {
                        $('th', this).each(function () {

                            //html_table_data += $(this).text();
                            var ortblhn = $(this).text();
                            $("#CustoFielddets TBODY").find('tr').each(function () {
                                $(this).find("input[type='checkbox']").each(function () {
                                    var cbval = $(this).val();
                                    //alert(id);
                                    if (ortblhn == cbval) {
                                        if ($(this).prop('checked') == false) {
                                            $(this).prop('checked', true);
                                            $(this).prop('checked', 'checked');
                                        }
                                    }
                                });
                            });
                        });
                    });

                }

                function UpdateCutomRetailerData(columnName, ActiveView) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/UpdateCutomRetailerData",
                        data: "{'columnName':'" + columnName + "','ActiveView':'" + ActiveView + "'}",
                        dataType: "json",
                        success: function (data) {

                            //CFBindData = JSON.parse(data.d) || [];
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }

                function GetCutomRetailerData(listeddrcode, columnName) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/GetBindCustomFieldData",
                        data: "{'listeddrcode':'" + listeddrcode + "','columnName':'" + columnName + "'}",
                        dataType: "json",
                        success: function (data) {
                            CFBindData = JSON.parse(data.d) || [];
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }

                function getRetilers() {                   
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/getNewRetailer",
                        dataType: "json",
                        success: function (data) {
                            rtDtls = data.d;
                            
                            var tbl = $('#RetailerTable');
                            $(tbl).find('tbody tr').remove();
                            if (rtDtls.length > 0) {
                                for (var i = 0; i < rtDtls.length; i++) {


                                    var cCode = rtDtls[i].cCode
                                    str = '<td>' + (i + 1) + '</td>';
                                    str += '<td>' + rtDtls[i].sfName + '</td><td style="text-align: center; width:200px;"><button name="btnUpdate" type="button" class="btn btn-primary btnUpdate" style="width: 80px">Approve</button> <button name="btnCancel" type="button" class="btn btn-danger btnCancel" style="width: 80px">Reject</button></td>';
                                    str += '<td>' + rtDtls[i].createDate + '</td>' + ((rtDtls[i].picture != '') ? '<td class="hidetd"><img class="picc" style="height:30px;width:30px" src="/photos/' + rtDtls[i].picture + '" /></td>' : '<td class="hidetd"></td>') + '<td><input type="hidden" name="custCode" class="code"  value="' + rtDtls[i].cCode + '"/>' + rtDtls[i].cName + '</td><td style="width: 100px;" class="hidetd"><input type="textbox" class="erpcode" style="width: 100px;" name="ERP" onchange="updFlag(this)"  value="' + rtDtls[i].code + '"/>';
                                    str += '<td>' + rtDtls[i].Address + '</td><td class="hidetd" style="width: 100px;"><input type="textbox" name="lat" style="width: 100px;" onchange="updFlag(this)" class="lati" value="' + rtDtls[i].lat + '"/></td><td style="width: 100px;" class="hidetd"><input type="textbox" style="width: 100px;" class="longi" onchange="updFlag(this)" name="long" value="' + rtDtls[i].longn + '"/></td><td>' + rtDtls[i].City_Name + '</td><td class="hidetd">' + rtDtls[i].Landmark + '</td><td>' + rtDtls[i].PIN_Code + '</td><td class="hidetd">' + rtDtls[i].Contact_Person + '</td><td class="hidetd">' + rtDtls[i].Designation + '</td><td>' + rtDtls[i].Phone_No + '</td><td>' + rtDtls[i].routeName + '</td><td><select class="rchannel" style="display:none;"></select><span class="hchannel channelfld">' + rtDtls[i].channel + '</span><a class="hchannel elpencil"><i class="fa fa-pencil" style="float: right;"></i></a></td>';

                                    loadDisPlayCustomFields();
                                    console.log(DisPlayF);
                                    if (DisPlayF.length > 0) {
                                        $td = "";

                                        for (var $k = 0; $k < DisPlayF.length; $k++) {
                                            $fldnm = DisPlayF[$k].DisPlayName;
                                            $fldnc = DisPlayF[$k].ColumnName;                                           
                                            $('#RetailerTable thead > tr').each(function () {
                                                $('th', this).each(function () {
                                                    var ortblhn = $(this).text();
                                                    console.log(ortblhn);
                                                    //alert(ortblhn);
                                                    if ($fldnm == ortblhn) {
                                                        
                                                        fillAdditionalRetailer($fldnc);
                                                        //console.log(ARetailer);
                                                        let CustItem = ARetailer.filter(function (a) { return rtDtls[i].cCode == a.Retail_code });
                                                        console.log(CustItem);
                                                        if (CustItem.length > 0) {
                                                            var $val = CustItem[0][$fldnc];
                                                            //console.log($val);
                                                            if (($val == null || $val == '' || $val == "")) {
                                                                                                                                
                                                                str += "<td></td>";
                                                            }
                                                            else {                                                               

                                                                var ans = $val.includes('.') ? "true" : "false";
                                                                if (ans == "true") {
                                                                    $.ajax({
                                                                        type: "POST",
                                                                        contentType: "application/json; charset=utf-8",
                                                                        async: false,
                                                                        url: "RetailerApproval.aspx/DownloadImageFromS3",
                                                                        data: "{'filename':'" + $val + "'}",
                                                                        dataType: "json",
                                                                        success: function (msg) {
                                                                            //$td = "<td>" + msg.d + "</td>";
                                                                            str += '<td>' + msg.d + '</td>';
                                                                        }
                                                                    });
                                                                    
                                                                }
                                                                else {

                                                                    //$td += "<td>" + $val + "</td>";

                                                                    str += "<td>" + $val + "</td>";

                                                                    // alert('text');
                                                                }
                                                            }
                                                        }
                                                    }
                                                   
                                                });
                                            });
                                        }
                                    }                                   

                                    $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                                }
                            }
                            else {
                                $(tbl).find('tbody').append('<tr><td colspan="14" style="color:red; font-weight: bold;">No Records Found..!</td></tr>');
                            }

                        },
                        error: function (jqXHR, exception) {
                            console.log(jqXHR);
                            console.log(exception);
                        }
                    });
                    if ('<%=Session["div_code"]%>' == '100') {
                        $('.hidetd').hide();
                    }
                }

            </script>

        </body>
    </html>

</asp:Content>
