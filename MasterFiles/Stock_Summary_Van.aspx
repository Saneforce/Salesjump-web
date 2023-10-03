<%@ Page Title="Stock Summary" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Stock_Summary_Van.aspx.cs" Inherits="MasterFiles_Stock_Summary_Van" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var StkDetail = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Stock_Summary_Van.aspx/GetDistributor",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddldistributor");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['disCode']).html(this['disName']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Stock_Summary_Van.aspx/GetProduct",
                dataType: "json",
                success: function (data) {
                    //console.log(data.d);
                    // var obj = JSON.stringify(data.d);                                 
                    //                    dtd = data.d.filter(function (a) {
                    //                        return (a.Dist_Code == $("#ddldistributor").val());
                    //                    });
                    StkDetail = data.d;
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


            var strW = "<td colspan='5'>No Record Found..!</td>";
            $('#ProductTable >tbody').append('<tr>' + strW + ' </tr>');

            $(document).on('change', '#ddldistributor', function () {
                dtd = StkDetail.filter(function (a) {
                    return (a.Dist_Code == $("#ddldistributor").val());
                });
                var str = "";
                $('#ProductTable >tbody >tr').remove();
                if (dtd.length > 0) {
                    for (var i = 0; i < dtd.length; i++) {
                        str = "<td>" + (i + 1) + "</td><td><input type='hidden' name='pcode'value='" + dtd[i].pCode + "'/>" + dtd[i].pName + "</td>";
                        str += "<td>" + dtd[i].BatchNo + "</td><td>" + dtd[i].GStock + "</td><td>" + dtd[i].DStock + "</td>";
                        $('#ProductTable >tbody').append('<tr>' + str + ' </tr>');
                    }
                }
                else {
                    str = "<td colspan='5'>No Record Found..!</td>";
                    $('#ProductTable >tbody').append('<tr>' + str + ' </tr>');
                }

            });


        });
    </script>
    <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <div class="form-group">
                <label for="location" class="control-label col-sm-1 col-sm-offset-4" style="font-weight: normal">
                    Field Force
                </label>
                <div class="col-sm-3">
                    <select id="ddldistributor" name="ddldistributor" class="form-control">
                    </select>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <table id="ProductTable" class="newStly table table-responsive">
                    <thead>
                        <tr>
                            <th>
                                SlNo.
                            </th>
                            <th>
                                Product Name
                            </th>
							<th>
                                Batch No.
                            </th>
                            <th>
                                Good
                            </th>
                            <th>
                                Damage
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
