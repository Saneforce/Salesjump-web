<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AllowanceMasterFFO.aspx.cs" Inherits="MasterFiles_AllowanceMasterFFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
                    Division</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                            OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label id="lblFF" class="col-md-2 col-md-offset-3 control-label">
                    Manager</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group" id="kk" runat="server">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control ffo" Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnView" class="btn btn-primary" style="vertical-align: middle; width: 100px">
                        <span>View</span></a>
                </div>
            </div>
        </div>
        <br />
        <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <div class="col-md-12">
                    <table id="FFTable" class="newStly" style="width: 100%;">
                        <thead></thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnSave" class="btn btn-primary" style="vertical-align: middle; width: 100px; display: none">
                        <span>Save</span></a>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var ffdata = [];
            $('#btnSave').css('display', 'none');


            $(document).on('change', '#<%=ddlFieldForce.ClientID%>', function () {
                var tbl = $('#FFTable');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();
                $('#btnSave').css('display', 'none');
            });


            var getTable = () => {
                var tbl = $('#FFTable');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();

                var str = `<th>SlNo</th><th>Field Force Name</th><th>Employee Code</th><th>Type</th>`;
                $(tbl).find('thead').append(`<tr>${str}</tr>`);
                if (ffdata.length > 0) {
                    $('#btnSave').css('display', 'table-cell');
                    ffdata.forEach((element, index) => {
                        var ty = '';
                        ty = element.allwType == '' ? 'Act' : element.allwType;
                        str = `<td style="padding: 1px;">${index + 1}</td><td style="padding: 1px;"><input type="hidden" name="sfcode" value="${element.sfCode}"/> <input type="hidden" name="mgrsf" value="${element.mgrsf}"/>${element.sfName}</td><td style="padding: 1px;">${element.empID}</td>`;
                        str += `<td style="padding: 1px;"><select class="form-control" name="allType" ><option  value="Act">Actual</option><option value="HQ">HQ</option>  <option value="EX">EX</option><option value="OS">OS</option></select></td>`;
                        $(tbl).find('tbody').append(`<tr>${str}</tr>`);
                        $(tbl).find('tbody tr').eq(index).find('select[name="allType"]').val(ty);
                        // console.log($(tbl).find('tbody tr').eq(index).find('select[name="allType"]').val(ty));
                    });
                }
                else {
                    str = `<td colspan="4" style="color:red">Field Force Not Found..!</td>`;
                    $(tbl).find('tbody').append(`<tr>${str}</tr>`);
                }
            }


            $(document).on('click', '#btnView', function () {
                var SubDiv = $('#<%=subdiv.ClientID%>').val();
                var sfCode = $('#<%=ddlFieldForce.ClientID%>').val();
                if (sfCode == '0') { alert('Select Manager..!'); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "AllowanceMasterFFO.aspx/GetFieldForce",
                    data: "{'sfCode':'" + sfCode + "','SubDiv':'" + SubDiv + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        ffdata = data.d;
                        getTable();
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });
            });

            $(document).on('click', '#btnSave', function () {
                var tbl = $('#FFTable');
                if ($(tbl).find('tbody tr').length > 0) {
                    var arr = [];
                    $(tbl).find('tbody tr').each((index, element) => {                       
                        arr.push({
                            sfCode: $(element).find('input[name="sfcode"]').val(),
                            mgrsf: $(element).find('input[name="mgrsf"]').val(),
                            allType: $(element).find('select[name="allType"]').val(),
                        });
                    });
                    console.log(arr);      

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "AllowanceMasterFFO.aspx/savedata",
                        data: "{'data':'" + JSON.stringify(arr) + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert("Allowance Type has been updated successfully!!!");
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>

