<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="RetailerApproval.aspx.cs" Inherits="MasterFiles_RetailerApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
	<link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
        <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
        <link href="../../css/style.css" rel="stylesheet" />
    <script type="text/javascript">
		var masChannel = [];
        function updChannel(drcode,spcode,spname) {
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
            $(document).on('click','.elpencil', function () {
                $(this).closest('td').find('.hchannel').hide();
                var schannel = $(this).closest('td').find('.rchannel');
                schannel.empty().append('<option value="0">Select Channel</option>');
                for ($i = 0; $i < masChannel.length; $i++) {
                    schannel.append('<option value="' + masChannel[$i].Doc_Special_Code + '">' + masChannel[$i].Doc_Special_Name + '</option>');
                }
                schannel.show();
            })
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
            })
            getRetilers = function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RetailerApproval.aspx/getNewRetailer",
                    dataType: "json",
                    success: function (data) {
                        rtDtls = data.d;
                        dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
                        $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
                        $("body").append(dv);
                        var tbl = $('#RetailerTable');
                        $(tbl).find('tbody tr').remove();
                        if (rtDtls.length > 0) {
                            for (var i = 0; i < rtDtls.length; i++) {
                                str = '<td>' + (i + 1) + '</td>';
                                str += '<td>' + rtDtls[i].sfName + '</td><td style="text-align: center; width:200px;"><button name="btnUpdate" type="button" class="btn btn-primary btnUpdate" style="width: 80px">Approve</button> <button name="btnCancel" type="button" class="btn btn-danger btnCancel" style="width: 80px">Reject</button></td>';
                                str += '<td>' + rtDtls[i].createDate + '</td>' + ((rtDtls[i].picture != '') ? '<td class="hidetd"><img class="picc" src="/photos/' + rtDtls[i].picture + '" /></td>' : '<td class="hidetd"></td>') + '<td><input type="hidden" name="custCode" class="code"  value="' + rtDtls[i].cCode + '"/>' + rtDtls[i].cName + '</td><td style="width: 100px;" class="hidetd"><input type="textbox" class="erpcode" style="width: 100px;" name="ERP" onchange="updFlag(this)"  value="' + rtDtls[i].code + '"/>';
                                str += '<td>' + rtDtls[i].Address + '</td><td class="hidetd" style="width: 100px;"><input type="textbox" name="lat" style="width: 100px;" onchange="updFlag(this)" class="lati" value="' + rtDtls[i].lat + '"/></td><td style="width: 100px;" class="hidetd"><input type="textbox" style="width: 100px;" class="longi" onchange="updFlag(this)" name="long" value="' + rtDtls[i].longn + '"/></td><td>' + rtDtls[i].City_Name + '</td><td class="hidetd">' + rtDtls[i].Landmark + '</td><td>' + rtDtls[i].PIN_Code + '</td><td class="hidetd">' + rtDtls[i].Contact_Person + '</td><td class="hidetd">' + rtDtls[i].Designation + '</td><td>' + rtDtls[i].Phone_No + '</td><td>' + rtDtls[i].routeName + '</td><td><select class="rchannel" style="display:none;"></select><span class="hchannel channelfld">' + rtDtls[i].channel + '</span><a class="hchannel elpencil"><i class="fa fa-pencil" style="float: right;"></i></a></td>';
                                $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                            }
                        }
                        else {
                            $(tbl).find('tbody').append('<tr><td colspan="14" style="color:red;    font-weight: bold;">No Records Found..!</td></tr>');
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
            $('.picc').click(function () {
                var photo = $(this).attr("src");
                $('#photo1').attr("src", $(this).attr("src"));
                $('#cphoto1').css("display", 'block');
                // $(this).append('<div style="width: 100%" ><img src="' + photo + '"/></div>'

            });
        });
function closew() {
            $('#cphoto1').css("display", 'none');
        }

    </script>
    <form id="form1" runat="server">
	<asp:LinkButton ID="btnExcel"  runat="Server" Style="padding: 0px 20px;float:right;" class="btn btnExcel" OnClick="btnexl_Click" />
    <div class="container" style="width: 100%;overflow:scroll;">
        <div class="form-group">
            <div class="col-md-12">
                <table id="RetailerTable" class="table-bordered newStly">
                    <thead>
                        <tr>
                            <th>
                                SlNo.
                            </th>
							 <th>
                                Field Force Name
                            </th>
                            <th>
                                Approval
                            </th>
                            <th>
                                Created Date
                            </th>
<th class="hidetd">
                                Picture
                            </th>
                            <th>
                                Retailer Name
                            </th>
                             <th class="hidetd">
                                ERP Code
                            </th>
                            <th>
                                Address
                            </th>
                            <%--<th class="hidetd">
                                Area Name
                            </th>--%>
							<th class="hidetd">
                                Latitude
                            </th>
                            <th class="hidetd">
                                Longitude
                            </th>
                            <th>
                                City Name
                            </th>
                            <th class="hidetd">
                                Landmark
                            </th>
                            <th>
                                PIN Code
                            </th>
                            <th class="hidetd">
                                Contact Person
                            </th>
                            <th class="hidetd">
                                Designation
                            </th>
                            <th>
                                Phone No.
                            </th>
                            <th>
                                Route Name
                            </th>
                           
                            <th>
                                Channel
                            </th>
						
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div style="background: #dbdddf;position: fixed;width: 100%;bottom: 0px;padding: 5px 20px;margin-left: -30px;display:none">
            <button ID="btnSubmit" class="btn btn-primary btn-update">Update</button>
        </div>
    </div>
    </form>
</asp:Content>
