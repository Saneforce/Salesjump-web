<%@ Page Title="Leave Approval" Language="C#" MasterPageFile="~/Master_MGR.master"
    AutoEventWireup="true" CodeFile="Leave_Admin_Approval.aspx.cs" Inherits="MasterFiles_Leave_Admin_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            getLave = function () {
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'Leave_Admin_Approval.aspx/getData',
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        bDat = data.d;
                        var tbl = $('#tblLeave');
                        $(tbl).find('tbody tr').remove();
                        if (bDat.length > 0) {
                            for (var i = 0; i < bDat.length; i++) {
				if (bDat[i].First_or_Second == 1) {
                                    var session = 'First Half';
                                } else if (bDat[i].First_or_Second == 2) {
                                    var session = 'Second Half';
                                } else if (bDat[i].First_or_Second == 3 ||bDat[i].First_or_Second == 0 ) {
                                    var session = 'Full Day';
                                }
                                str = '<td><input type="hidden" name="hlid" value="' + bDat[i].Leave_Id + '" />' + (i + 1) + '</td><td> <input type="hidden" name="hsf" value="' + bDat[i].Sf_Code + '" />' + bDat[i].FieldForceName + '</td><td>' + bDat[i].Designation + '</td><td>' + bDat[i].HQ + '</td><td>' + bDat[i].EmpCode + '</td><td>' + bDat[i].From_Date + '</td><td>' + bDat[i].To_Date + '</td><td>' + bDat[i].LeaveDays + '</td><td>'+session+'</td><td>' + bDat[i].Reason + '</td>';
                                str += '<td style="text-align: center; width:200px;" ><a id="btnApp" class="btn btn-primary">Approval</a> <a id="btnRej" class="btn btn-danger">Reject</a></td>';
                                $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                            }
                        }
                        else {
                            $(tbl).find('tbody').append('<tr><td colspan="10" style="color:red">No Record Found..!</td></tr>');
                        }

                    },
                    error: function (erorr) {
                        console.log(erorr);
                    }
                });
            }

            getLave();
            $(document).on('click', '[id*=btnApp]', function () {
                var mRow = $(this).closest('tr');
                if (confirm('Are you sure you want to Approval ?')) {
                    lCode = $(mRow).find('input[name=hlid]').val();
                    sfCode = $(mRow).find('input[name=hsf]').val();
                    $.ajax({
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: 'Leave_Admin_Approval.aspx/Approvaldata',
                        type: "POST",
                        data: "{'SF_Code':'" + sfCode + "', 'LeaveCode':'" + lCode + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
							alert(data.d);
                            getLave();
                        },
                        error: function (erorr) {
                            console.log(erorr);
                        }
                    });
                }
                else {
                }
            });
            $(document).on('click', '[id*=btnRej]', function () {
                var mRow = $(this).closest('tr');
                if (confirm('Are you sure you want to Reject ?')) {
                    var reasonTxt = prompt('Enter Reject Reason here');
                    lCode = $(mRow).find('input[name=hlid]').val();
                    sfCode = $(mRow).find('input[name=hsf]').val();
					if(reasonTxt!=null)
					{
                    $.ajax({
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        url: 'Leave_Admin_Approval.aspx/RejectData',
                        type: "POST",
                        data: "{'SF_Code':'" + sfCode + "', 'LeaveCode':'" + lCode + "', 'Reason':'" + reasonTxt + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
							alert(data.d);
                            getLave();
                        },
                        error: function (erorr) {
                            console.log(erorr);
                        }
                    });
					}
					else
					{
					alert("You Cancelled Leave Rejection.")
					}
                }
                else {
                }


            });
        });
    </script>
    <form id="form1" runat="server">
    <div class="container" style="max-width: 100%; width: 95%">
        <table id="tblLeave" width="100%" class="newStly">
            <thead>
                <tr>
                    <th>
                        S.No
                    </th>
                    <th>
                        FieldForce Name
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        HQ
                    </th>
                    <th>
                        Emp.Code
                    </th>
                    <th>
                        From Date
                    </th>
                    <th>
                        To Date
                    </th>
                    <th>
                        Leave Days
                    </th>
		    <th>
                        Session
                    </th>
                    <th>
                        Reason
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    </form>
</asp:Content>
