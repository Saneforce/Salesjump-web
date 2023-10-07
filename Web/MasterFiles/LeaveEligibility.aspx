<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="LeaveEligibility.aspx.cs" Inherits="MasterFiles_LeaveEligibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <form id="form1" runat="server">
 <div class="row">
            <label id="lblFYear" class="col-md-2  col-md-offset-3  control-label">
                Year</label>
                  <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>                         
                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control"  Width="100">
                        </asp:DropDownList>
                   </div>
</div>
</div>
        <div class="container" style="width: 100%; max-width: 100%">
            <table id="Designation" class="newStly" style="width: 90%; margin: 0px auto;">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
            <br />
            <br />
            <table id="FieldForce" class="newStly" style="width: 90%; margin: 0px auto;">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
            <br />
            <div class="row" style="text-align: center">
                <div class="col-sm-12 inputGroupContainer">
                    <a name="btnsave" id="btnsave" class="btn btn-primary btnsave" style="width: 100px">Save</a>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {

            LeaveEligibilityLoad();

            function LeaveEligibilityLoad() {
                var dTable = $('#Designation');
                var fTable = $('#FieldForce');
                var lType = [];
                var desg = [];


                $('#Designation').find('thead').find('tr').remove();
                $('#FieldForce').find('thead').find('tr').remove();
                $('#Designation').find('tbody').find('tr').remove();
                $('#FieldForce').find('tbody').find('tr').remove();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveEligibility.aspx/GetLeaveType",
                    dataType: "json",
                    success: function (data) {
                        lType = data.d;
                        //console.log(data.d);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveEligibility.aspx/Get_Details",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strDs = "<td class='col-xs-2'> <input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].Designation_Short_Name + "</td> ";
                            for (var j = 0; j < lType.length; j++) {
                                strDs += "<td> <input type='text' class='form-control' style='height:33px;' onkeyup='FetchData(this," + (j + 1) + ")'/> </td>";
                            }
                            $("#Designation tbody").append("<tr >" + strDs + "</tr>");
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });



                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveEligibility.aspx/Get_FieldForce",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strFF = "<td>" + (i + 1) + " </td><td class='col-xs-2'>" + data.d[i].sf_name + "</td><td class='col-xs-2'><input type='hidden' name='sf_Code' value='" + data.d[i].sf_code + "'/><input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].designation_name + "</td><td>" + data.d[i].sf_HQ + "</td><td>" + data.d[i].EmpCode + "</td><td>" + data.d[i].JoinDT + "</td>";
                            for (var j = 0; j < lType.length; j++) {
                                strFF += "<td ><input type='text'  name='Alw_value' class='form-control' style='height:33px;'/></td>";
                            }
                            $("#FieldForce tbody").append("<tr>" + strFF + "</tr>");
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                if (lType.length > 0) { 
                    var strDes = '<th>Designation</th>';
                    var strFF = '<th>SlNo.</th><th>Field Force Name</th><th>Designation</th><th>HeadQuarters</th><th>Employee Code</th><th>Joining Date</th> ';
                    for (var i = 0; i < lType.length; i++) {
                        strDes += '<th><input type="hidden" name="Alw_Code" value="' + lType[i].LCode + '"/>' + lType[i].LSName + '</th>';
                        strFF += '<th><input type="hidden" name="Alw_Code" value="' + lType[i].LCode + '"/>' + lType[i].LSName + '</th>';
                    }
                    $(dTable).find('thead').append('<tr class="alcode">' + strDes + '</tr>');
                    $(fTable).find('thead').append('<tr class="alcode">' + strFF + '</tr>');
                }

                $("input").live("keypress", function (e) {
                    var num = e.keyCode;
                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                        return false;
                    }
                });



                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "LeaveEligibility.aspx/GetLeave_Values",
                    dataType: "json",
                    success: function (data) {
                        // console.log(data.d);
                        Filldata(data);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });








                $(document).on('click', '.btnsave', function () {
				    var yr = $('#<%=ddlFYear.ClientID%> :selected').text();
                    var dtls_tab = document.getElementById("FieldForce");
                    var nrows1 = dtls_tab.rows.length;
                    var Ncols = dtls_tab.rows[1].cells.length;
                    var arr = [];
                    $(this).text('Wait..!');
                    $('#FieldForce tbody tr').each(function () {
                        var fd = 1;
                        for (var i = 6; i < Ncols; i++) {
                            //  console.log($(this).closest('table').find('.alcode').find('th').eq(5));
                            arr.push({
                                Des_code: $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString(),
                                SF_Code: $(this).closest('tr').find('input[name=sf_Code]').val().toLowerCase().toString(),
                                LeaveCode: $(this).closest('table').find('.alcode').find('th').eq(i).find('input[name=Alw_Code]').val(),
                                LeaveValues: $(this).children('td').eq(i).find('input[name=Alw_value]').val()

                            });
                        }
                    });
                    //   console.log(JSON.stringify(arr));
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "LeaveEligibility.aspx/savedata",
                        data: "{'data':'" + JSON.stringify(arr) + "','year':'"+yr+"'}",
                        dataType: "json",
                        success: function (data) {
                            alert("Leave Eligibility has been updated successfully!!!");
                            $('.btnsave').text('Save');
                            LeaveEligibilityLoad();
                           
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                });
            }
        });

        function Filldata(data) {

            var dtls_tab = document.getElementById("Designation");
            var nrows1 = dtls_tab.rows.length;
            var Ncols = dtls_tab.rows[1].cells.length;
            //alert(JSON.stringify(data.d));
            if (data.d.length > 0) {
                for (var i = 0; i < data.d.length; i++) {
                    $('#Designation tbody tr').each(function () {
                        var fd = 1;
                        for (var col = 1; col < Ncols ; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == data.d[i].Des_code) {

                                if ($(this).closest('table').find('.alcode').find('th').eq(col).find('input[name=Alw_Code]').val() == data.d[i].LeaveCode) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(data.d[i].LeaveValues);
                                }
                            }
                        }

                    });
                }
            }




            var dtls_tab = document.getElementById("FieldForce");
            var nrows1 = dtls_tab.rows.length;
            var Ncols = dtls_tab.rows[1].cells.length;
            //  alert(JSON.stringify(data.d));
            if (data.d.length > 0) {
                console.log(data.d);
                $('#FieldForce tbody tr').each(function () {
                    var sfCode = $(this).closest('tr').find("[name='sf_Code']").val();
                    dtd = data.d.filter(function (a) {
                        return (a.SF_Code.toLowerCase() == sfCode.toLowerCase());
                    });
                    for (var i = 0; i < dtd.length; i++) {
                        var fd = 1;
                        for (var col = 6; col < Ncols; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == dtd[i].Des_code) {
                                if ($(this).closest('table').find('.alcode').find('th').eq(col).find('input[name=Alw_Code]').val() == dtd[i].LeaveCode) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(dtd[i].LeaveValues);
                                    // console.log(data.d[i].Alw_code + ':' + data.d[i].values);
                                }
                            }
                        }
                    }
                });
            }
        }

        function FetchData(button, idx) {
            var fareval = $(button).val();
            var id = '#' + button;
            var ff = $(button).closest("tr").find("input:hidden").val();
            $('#FieldForce').find('tr:has(td)').each(function () {
                var sf_code = $(this).find("td:eq(2) :input[name=Des_Code]").val();
                if (sf_code == ff) {
                    //$(this).find("td:eq(" + (idx + 3) + ") :input").val(fareval);
					$(this).find("td:eq(" + (idx + 5) + ") :input[name=Alw_value]").val(fareval);
                }
            });
        }

    </script>
</asp:Content>
