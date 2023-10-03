<%@ Page Title="Distance Entry" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Distance_Entry.aspx.cs" Inherits="MasterFiles_Distance_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.16/angular.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('change', '#<%=ddlTerritoryName.ClientID%>', function () {

                var selval = $("#<%=ddlTerritoryName.ClientID%>").val();
                var seltxt = $("#<%=ddlTerritoryName.ClientID%> option:selected").text();
                $('#table1 tbody tr').remove();
                if (selval != 0) {
                    var str1 = "";
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Distance_Entry.aspx/Get_Details",
                        data: "{'terr_code':'" + selval + "'}",
                        dataType: "json",
                        success: function (data) {
                            var k = 0;
                            console.log(data.d);
                            for (var i = 0; i < data.d.length; i++) {
                                if (data.d[i].alw_type != "OS-EX") {
                                    str1 += "<tr class='newhead'><td ><input type='hidden' name='from_code' value='" + $("#<%=ddlTerritoryName.ClientID%>").val() + "'/>" + seltxt + "</td> <td> <input type='hidden' name='to_code' value='" + data.d[i].terr_code + "'/> <input type='hidden' name='dis_type' value='" + data.d[i].alw_type + "'/>" + data.d[i].terr_Name + "</td><td>" + data.d[i].alw_type + "</td><td><input type='text' name='txtdis' value='0' /></td></tr>";
                                    k = i;
                                }
                                else {
                                    str1 += "<tr class='clrow'><td><input type='hidden' name='from_code' value='" + data.d[k].terr_code + "'/>" + data.d[k].terr_Name + "</td> <td><input type='hidden' name='to_code' value='" + data.d[i].terr_code + "'/><input type='hidden' name='dis_type' value='" + data.d[i].alw_type + "'/>" + data.d[i].terr_Name + "</td><td>" + data.d[i].alw_type + "</td><td><input type='text'  name='txtdis' value='0'/></td></tr>";

                                }
                            }
                            $("#table1 tbody").append(str1);
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });



                    var dtls_tab = document.getElementById("table1");
                    var nrows1 = dtls_tab.rows.length;
                    var Ncols = dtls_tab.rows[0].cells.length;


                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Distance_Entry.aspx/getDistanceValue",
                        dataType: "json",
                        data: "{'data':'" + selval + "'}",
                        success: function (data) {
                            if (data.d.length > 0) {
                                for (var i = 0; i < data.d.length; i++) {
                                    $('#table1 tbody tr').each(function () {
                                        if ($(this).children('td').eq(0).find("[name=from_code]").val().toString() == data.d[i].fromc.toString()) {
                                            if ($(this).children('td').eq(1).find("[name=to_code]").val().toString() == data.d[i].toc.toString()) {
                                                $(this).children('td').eq(3).find('input[name=txtdis]').val(data.d[i].distance.toString());
                                            }
                                        }
                                    });
                                }
                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
                $(".clrow").prev().addClass('header');
            });

            $(document).on('click', '.header', function () {
                $(this).toggleClass("active", "").nextUntil('.newhead').css('display', function (i, v) {
                    return this.style.display === 'table-row' ? 'none' : 'table-row';
                });
            });

            $(document).on('click', '#btnsave', function () {

                var selval = $("#<%=ddlTerritoryName.ClientID%>").val();
                var seltxt = $("#<%=ddlTerritoryName.ClientID%> option:selected").text();

                if (selval == 0) {
                    alert('Select Territory HQ');
                    $("#<%=ddlTerritoryName.ClientID%>").focus();
                    return;

                }

                var dtls_tab = document.getElementById("table1");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;

                var ch = true;
                var arr = [];
                $('#table1 tbody tr').each(function () {
                    arr.push({
                        distance: $(this).children('td').eq(3).find('input[name=txtdis]').val().toLowerCase().toString(),
                        fromc: $(this).children('td').eq(0).find('input[name=from_code]').val(),
                        Place_Type: $(this).children('td').eq(1).find('input[name=dis_type]').val(),
                        toc: $(this).children('td').eq(1).find('input[name=to_code]').val(),
                        terrhq: $("#<%=ddlTerritoryName.ClientID%>").val()
                    });
                });


                if (arr.length <= 0) {
                    alert('No Record Found');
                    return;
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Distance_Entry.aspx/savedata",
                    data: "{'data':'" + JSON.stringify(arr) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert("Distance has been updated successfully!!!");
                        loaddata();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });

            $("input").live("keypress", function (e) {
                var num = e.keyCode;
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                    return false;
                }
            });

            $("input").live("blur", function (e) {
                if ($(this).val().length == 0) {
                    $(this).val("0");
                }
            });


        });

    </script>
    <style type="text/css">
        input[type='text']
        {
            text-align: right;
        }
        input[type='text'], select
        {
            line-height: 22px;
            padding: 5px 5px;
            border-radius: 5px;
        }
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            padding: 4px;
            vertical-align: middle;
            overflow: hidden;
            text-overflow: ellipsis;
            font-weight:normal;
        }
        .table > tbody > tr.active > td, .table > tbody > tr.active > th, .table > tbody > tr > td.active, .table > tbody > tr > th.active, .table > tfoot > tr.active > td, .table > tfoot > tr.active > th, .table > tfoot > tr > td.active, .table > tfoot > tr > th.active, .table > thead > tr.active > td, .table > thead > tr.active > th, .table > thead > tr > td.active, .table > thead > tr > th.active
        {
            background-color: #ccd1d6;
        }
        
        .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th
        {
            border-color: #e4e5e7;
        }
        
        .table thead tr
        {
            background-color: #055079;
            color: #fff;
        }
        
        
        .table tr.header
        {
            font-weight: bold;
            background-color: #ccd1d6;
            color: #000;
            cursor: pointer;
            -webkit-user-select: none; /* Chrome all / Safari all */
            -moz-user-select: none; /* Firefox all */
            -ms-user-select: none; /* IE 10+ */
            user-select: none; /* Likely future */
        }
        
        
        .table tr.newhead
        {
            font-weight: bold;
            background-color: #ccd1d6;
            color: #000;
            cursor: pointer;
            -webkit-user-select: none; /* Chrome all / Safari all */
            -moz-user-select: none; /* Firefox all */
            -ms-user-select: none; /* IE 10+ */
            user-select: none; /* Likely future */
        }
        
        .table .header td:nth-child(2):after
        {
            content: "\002b";
            position: relative;
            top: 1px;
            display: inline-block;
            font-family: 'Glyphicons Halflings';
            font-style: normal;
            font-weight: 400;
            line-height: 1;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            float: right;
            color: #fff;
            text-align: center;
            padding: 3px;
            transition: transform .25s linear;
            -webkit-transition: -webkit-transform .25s linear;
        }
        .table tr:.clrow
        {
            display: none;
        }
        
        .table .header.active td:nth-child(2):after
        {
            content: "\2212";
        }
        --% ></style>
    <form id="main_frm" runat="server">
    <div class="container">
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Territory HQ" CssClass="col-md-4 control-label"
                Style="text-align: right; font-weight: bold; padding: 8px 4px;"></asp:Label>
            <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-th-list"></i></span>
                    <asp:DropDownList ID="ddlTerritoryName" runat="server" SkinID="ddlRequired" CssClass="form-control"
                        Style="font-size: 17px;" Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="container" style="width: 100%; max-width: 100%">
        <table id="table1" class="table table-bordered" style="width: 100%">
            <thead>
                <tr>
                    <th class="col-md-2">
                        From
                    </th>
                    <th class="col-md-5">
                        To
                    </th>
					<th class="col-md-1">
                        Type
                    </th>
                    <th class="col-md-1">
                        Distance
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

<div class="row">
                <div class="col-sm-12" style="text-align: center">
            <a name="btnsave" id="btnsave" class="btn btn-primary" style="width: 100px;vertical-align: middle">
                Save</a>                   
                </div>
            </div>




    </div>
    </form>
</asp:Content>
