<%@ Page Title="Allowance Entry" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Allowance_Entry.aspx.cs" Inherits="MasterFiles_Allowance_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .gvHeader th
        {
            padding: 4px 4px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }
        .gvRow td
        {
            padding: 4px 4px;
            background-color: #ffffff;
            border: 1px solid #bbb;
            text-align: left;
        }
        .gvAltRow td
        {
            padding: 4px 4px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
            text-align: left;
        }
        
        input[type='text']
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
          let edt = new Date();
            var dd = edt.getDate();
            var mm = edt.getMonth() + 1;
            var yyyy = edt.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            document.getElementById("recpt-dt").setAttribute("min", yyyy + '-' + mm + '-' + dd);
            alowanceLoad();
            function alowanceLoad() {
                var len = 0;
                var mnth_cnt = 0;
                var dly_cnt = 0;
                $('#Designation tr').remove();
                $("#FieldForce tr").remove();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry.aspx/GetAllType",
                    dataType: "json",
                    success: function (data) {

                        len = data.d.length;
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                if (data.d[i].ALW_type == 1) {
                                    dly_cnt++;
                                }
                                if (data.d[i].ALW_type == 2) {
                                    mnth_cnt++;
                                }
                            }
                        }
                        var str = "<tr><th rowspan='2' class='col-xs-2'><p>Designation</p></th><th colspan='" + (dly_cnt + 3) + "'>Daily</th>";

                        if (mnth_cnt > 0) str += "<th colspan='" + mnth_cnt + "'>Monthly</th>";
                        str += "</tr>";
                        $('#Designation thead').append(str);
                        str = "";
                        str += "<th><input type='hidden' name='Alw_Code' value='HQ1'/>HQ</th><th> <input type='hidden' name='Alw_Code' value='EX1'/>EX</th><th> <input type='hidden' name='Alw_Code' value='OS1'/>OS</th>";
                        for (var i = 0; i < data.d.length; i++) {
                            str += "<th> <input type='hidden' name='Alw_Code' value='" + data.d[i].ALW_code + "'/>" + data.d[i].ALW_name + "</th>";
                        }
                        $('#Designation thead').append('<tr class="alcode">' + str + '</tr>');

                        var str = "<tr><th rowspan='2' >S.No </th><th rowspan='2' class='col-xs-2'>FieldForce</th><th  rowspan='2' class='col-xs-2'>HeadQuarters</th><th  colspan='" + (dly_cnt + 3) + "'>Daily</th>";
                        if (mnth_cnt > 0) str += "<th colspan='" + mnth_cnt + "'>Monthly</th>";
                        str += "</tr>";
                        $('#FieldForce thead').append(str);
                        var str1 = "<th><input type='hidden' name='Alw_Code' value='HQ1'/>HQ</th><th><input type='hidden' name='Alw_Code' value='EX1'/>EX</th><th > <input type='hidden' name='Alw_Code' value='OS1'/>OS</th>";
                        for (var i = 0; i < data.d.length; i++) {
                            str1 += "<th > <input type='hidden' name='Alw_Code' value='" + data.d[i].ALW_code + "'/>" + data.d[i].ALW_name + "</th>";
                        }
                        $("#FieldForce thead").append('<tr class="alcode">' + str1 + '</tr>');

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                var strDs = "";
                $("#Designation").append(strDs);
                strDs = "";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry.aspx/Get_Details",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strDs = "<td class='col-xs-2'> <input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].Designation_Short_Name + "</td> <td > <input type='text' class='form-control' style='height:33px;' onkeyup='FetchData(this," + (1) + ")'/> </td><td ><input type='text' class='form-control' style='height:33px;' onkeyup='FetchData(this," + (2) + ")'/></td><td ><input type='text' class='form-control' style='height:33px;' onkeyup='FetchData(this," + (3) + ")'/></td>";
                            for (var j = 0; j < len; j++) {
                                strDs += "<td> <input type='text' class='form-control' style='height:33px;' onkeyup='FetchData(this," + (j + 4) + ")'/> </td>";
                            }
                            $("#Designation tbody").append("<tr class='gvRow'>" + strDs + "</tr>");
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                var strFF = "";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Allowance_Entry.aspx/Get_FieldForce",
                    dataType: "json",
                    success: function (data) {
                        for (var i = 0; i < data.d.length; i++) {
                            strFF = "<td>" + (i + 1) + " </td><td class='col-xs-2'>" + data.d[i].sf_name + "</td><td class='col-xs-2'><input type='hidden' name='sf_Code' value='" + data.d[i].sf_code + "'/><input type='hidden' name='Des_Code' value='" + data.d[i].Designation_Code + "'/>" + data.d[i].sf_HQ + "</td><td > <input type='text' class='form-control' style='height:33px;' name='Alw_value'/> </td><td ><input type='text' class='form-control' style='height:33px; 'name='Alw_value'/></td><td ><input type='text' class='form-control' style='height:33px; ' name='Alw_value'/></td>";
                            for (var j = 0; j < len; j++) {
                                strFF += "<td ><input type='text'  name='Alw_value' class='form-control' style='height:33px;'/></td>";
                            }
                            $("#FieldForce tbody").append("<tr class='gvRow'>" + strFF + "</tr>");
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
                    url: "Allowance_Entry.aspx/GetAllow_Values",
                    dataType: "json",
                    success: function (data) {
                        Filldata(data);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $("input").live("keypress", function (e) {
                    var num = e.keyCode;
                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                        return false;
                    }
                });

            }
            $(document).on('click', '.btnsave', function () {
                var dtls_tab = document.getElementById("FieldForce");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[2].cells.length;
                var arr = [];
				var btndate = $('#recpt-dt').val();
                var fnldate = btndate;
                $('#FieldForce tbody tr').each(function () {
                    var fd = 1;
                    for (var i = 3; i < Ncols; i++) {
                        console.log($(this).closest('table').find('.alcode').find('th'));
                        arr.push({
                            Des_code: $(this).closest('tr').find('input[name=Des_Code]').val().toLowerCase().toString(),
                            SF_Code: $(this).closest('tr').find('input[name=sf_Code]').val().toLowerCase().toString(),
                            Alw_code: $(this).closest('table').find('.alcode').find('th').eq(i - 3).find('input[name=Alw_Code]').val(),
                            values: (($(this).children('td').eq(i).find('input[name=Alw_value]').val())=="")?0:($(this).children('td').eq(i).find('input[name=Alw_value]').val())

                        });
                    }
                });
                // alert(JSON.stringify(arr));
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Entry.aspx/savedata",
                    data: "{'data':'" + JSON.stringify(arr) + "','date':'" + fnldate + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert("Allowance has been updated successfully!!!");
                        alowanceLoad();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
        });


        function Filldata(data) {

            var dtls_tab = document.getElementById("Designation");
            var nrows1 = dtls_tab.rows.length;
            var Ncols = dtls_tab.rows[2].cells.length;
            //alert(JSON.stringify(data.d));
            if (data.d.length > 0) {
			 document.getElementById("recpt-dt").setAttribute("min", data.d[0].EffDt);
                $("#recpt-dt").val(data.d[0].EffDt);
                for (var i = 0; i < data.d.length; i++) {
                    $('#Designation tbody tr').each(function () {
                        var fd = 1;
                        for (var col = 1; col < Ncols; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == data.d[i].Des_code) {
                                if ($(this).closest('table').find('.alcode').find('th').eq(col - 1).find('input[name=Alw_Code]').val() == data.d[i].Alw_code) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(data.d[i].values);
                                }
                            }
                        }

                    });
                }
            }




            var dtls_tab = document.getElementById("FieldForce");
            var nrows1 = dtls_tab.rows.length;
            var Ncols = dtls_tab.rows[2].cells.length;
            //  alert(JSON.stringify(data.d));
            if (data.d.length > 0) {
                $('#FieldForce tbody tr').each(function () {
                    var sfCode = $(this).closest('tr').find("[name='sf_Code']").val();
                    dtd = data.d.filter(function (a) {
                        return (a.SF_Code.toLowerCase() == sfCode.toLowerCase());
                    });
                    for (var i = 0; i < dtd.length; i++) {
                        var fd = 1;
                        for (var col = 3; col < Ncols; col++) {
                            if ($(this).closest('tr').find("[name='Des_Code']").val() == dtd[i].Des_code) {
                                if ($(this).closest('table').find('.alcode').find('th').eq(col - 3).find('input[name=Alw_Code]').val() == dtd[i].Alw_code) {
                                    $(this).find('td').eq(col).find('input[type="text"]').val(dtd[i].values);
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
                    $(this).find("td:eq(" + (idx + 2) + ") :input").val(fareval);
                }
            });
        }
    </script>
    <form id="Allowancefrm" runat="server">
	  <div class="row" style="margin-bottom: 1rem;">
            <div class="col-sm-6">
                <div class="col-sm-6">
                    <label style="float: right;">Effective From</label>
                </div>
                <div class="col-sm-6">
                    <input type="date" autocomplete="off" class="form-control" id="recpt-dt" />
                </div>
            </div>
        </div>
    <div class="container" style="width: 100%">
        <table id="Designation" class="gvHeader">
            <thead>
            </thead>
            <tbody>
            </tbody>
        </table>
        <br />
        <br />
        <br />
        <table id="FieldForce" class="gvHeader">
            <thead>
            </thead>
            <tbody>
            </tbody>
        </table>
        <br />
        <div class="row" style="text-align: center">
            <div class="col-sm-12 inputGroupContainer">
                <a name="btnsave" id="btnsave" class="btn btn-primary btnsave" style="width: 100px">
                    Save</a>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
