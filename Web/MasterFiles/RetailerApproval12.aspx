<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="RetailerApproval.aspx.cs" Inherits="MasterFiles_RetailerApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

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
                               str = '<td>' + (i + 1) + '</td><td>' + rtDtls[i].createDate + '</td>' + ((rtDtls[i].picture != '') ? '<td><img class="picc" src="/photos/' +rtDtls[i].picture + '" /></td>' :'<td></td>')+'<td><input type="hidden" name="custCode" value="' + rtDtls[i].cCode + '"/>' + rtDtls[i].cName + '</td>';
                                  str += '<td>' + rtDtls[i].Address + '</td><td>' + rtDtls[i].Area_Name + '</td><td>' + rtDtls[i].lat + '</td><td>' + rtDtls[i].longn + '</td><td>' + rtDtls[i].City_Name + '</td><td>' + rtDtls[i].Landmark + '</td><td>' + rtDtls[i].PIN_Code + '</td><td>' + rtDtls[i].Contact_Person + '</td><td>' + rtDtls[i].Designation + '</td><td>' + rtDtls[i].Phone_No + '</td><td>' + rtDtls[i].routeName + '</td><td>' + rtDtls[i].sfName + '</td>';
                                str += '<td style="text-align: center; width:200px;"><button name="btnUpdate" type="button" class="btn btn-primary btnUpdate" style="width: 80px">Approve</button> <button name="btnCancel" type="button" class="btn btn-danger btnCancel" style="width: 80px">Reject</button></td>';
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
            }
            getRetilers();

            $(document).on('click', '.btn-primary', function () {
                lCode = $(this).closest('tr').find('input[name="custCode"]').val();

                if (confirm('Are you sure you want to Approve this Retailer?')) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RetailerApproval.aspx/UpdateApprove",
                        data: "{'custCode':'" + lCode + "'}",
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
            $(document).on('click', '.btn-danger', function () {
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
    <div class="container" style="width: 100%">
        <div class="form-group">
            <div class="col-md-12">
                <table id="RetailerTable" class="table table-bordered newStly">
                    <thead>
                        <tr>
                            <th>
                                SlNo.
                            </th>
                            <th>
                                Created Date
                            </th>
<th>
                                Picture
                            </th>
                            <th>
                                Retailer Name
                            </th>
                            <th>
                                Address
                            </th>
                            <th>
                                Area Name
                            </th>
<th>
                                Latitude
                            </th>
                            <th>
                                Longitude
                            </th>
                            <th>
                                City Name
                            </th>
                            <th>
                                Landmark
                            </th>
                            <th>
                                PIN Code
                            </th>
                            <th>
                                Contact Person
                            </th>
                            <th>
                                Designation
                            </th>
                            <th>
                                Phone No.
                            </th>
                            <th>
                                Route Name
                            </th>
                            <th>
                                Field Force Name
                            </th>
						
                            <th>
                                Approval
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
