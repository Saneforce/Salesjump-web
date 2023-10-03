<%@ Page Title="Allowance Master" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Allowance_Master.aspx.cs" Inherits="MasterFiles_Allowance_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .col-sm-7, .col-sm-3
        {
            padding: 0px 3px 6px 4px;
        }
        .checkbox
        {
            padding-left: 20px;
        }
        .checkbox label
        {
            display: inline-block;
            position: relative;
            padding-left: 5px;
        }
        .checkbox label::before
        {
            content: "";
            display: inline-block;
            position: absolute;
            width: 17px;
            height: 17px;
            left: 0;
            margin-left: -20px;
            border: 1px solid #cccccc;
            border-radius: 3px;
            background-color: #fff;
            -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
        }
        .checkbox label::after
        {
            display: inline-block;
            position: absolute;
            width: 16px;
            height: 16px;
            left: 0;
            top: 0;
            margin-left: -20px;
            padding-left: 3px;
            padding-top: 1px;
            font-size: 11px;
            color: #555555;
        }
        .checkbox input[type="checkbox"]
        {
            opacity: 0;
        }
        .checkbox input[type="checkbox"]:focus + label::before
        {
            outline: thin dotted;
            outline: 5px auto -webkit-focus-ring-color;
            outline-offset: -2px;
        }
        .checkbox input[type="checkbox"]:checked + label::after
        {
            font-family: 'FontAwesome';
            content: "\f00c";
        }
        .checkbox input[type="checkbox"]:disabled + label
        {
            opacity: 0.65;
        }
        .checkbox input[type="checkbox"]:disabled + label::before
        {
            background-color: #eeeeee;
            cursor: not-allowed;
        }
        .checkbox.checkbox-circle label::before
        {
            border-radius: 50%;
        }
        .checkbox.checkbox-inline
        {
            margin-top: 0;
        }
        .checkbox-primary input[type="checkbox"]:checked + label::before
        {
            background-color: #428bca;
            border-color: #428bca;
        }
        .checkbox-primary input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
        .checkbox-danger input[type="checkbox"]:checked + label::before
        {
            background-color: #d9534f;
            border-color: #d9534f;
        }
        .checkbox-danger input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
        .checkbox-info input[type="checkbox"]:checked + label::before
        {
            background-color: #5bc0de;
            border-color: #5bc0de;
        }
        .checkbox-info input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
        .checkbox-warning input[type="checkbox"]:checked + label::before
        {
            background-color: #f0ad4e;
            border-color: #f0ad4e;
        }
        .checkbox-warning input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
        .checkbox-success input[type="checkbox"]:checked + label::before
        {
            background-color: #5cb85c;
            border-color: #5cb85c;
        }
        .checkbox-success input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
        
        
        table.blueTable
        {
            border: 1px solid #1C6EA4;
            background-color: #EEEEEE;
            width: 100%;
            text-align: left;
            border-collapse: collapse;
        }
        table.blueTable td, table.blueTable th
        {
            border: 1px solid #AAAAAA;
            padding: 4px 4px;
        }
        table.blueTable tbody td
        {
            font-size: 13px;
        }
        table.blueTable tr:nth-child(even)
        {
            background: #D0E4F5;
        }
        table.blueTable thead
        {
            background: #1C6EA4;
            background: -moz-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
            background: -webkit-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
            background: linear-gradient(to bottom, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
            border-bottom: 2px solid #444444;
        }
        table.blueTable thead th
        {
            font-size: 15px;
            font-weight: bold;
            color: #FFFFFF;
            border-left: 2px solid #D0E4F5;
        }
        table.blueTable thead th:first-child
        {
            border-left: none;
        }
        
        table.blueTable tfoot
        {
            font-size: 14px;
            font-weight: bold;
            color: #FFFFFF;
            background: #D0E4F5;
            background: -moz-linear-gradient(top, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
            background: -webkit-linear-gradient(top, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
            background: linear-gradient(to bottom, #dcebf7 0%, #d4e6f6 66%, #D0E4F5 100%);
            border-top: 2px solid #444444;
        }
        table.blueTable tfoot td
        {
            font-size: 14px;
        }
        table.blueTable tfoot .links
        {
            text-align: right;
        }
        table.blueTable tfoot .links a
        {
            display: inline-block;
            background: #1C6EA4;
            color: #FFFFFF;
            padding: 2px 8px;
            border-radius: 5px;
        }
        table.blueTable th
        {
            background: #1C6EA4;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            getdata();
            $(document).on('click', '#btnSave', function () {

                var AllName = $('#<%=txtAllName.ClientID%>').val();
                if (AllName.length <= 0) { alert("Type Allowance Name"); $('#<%=txtAllName.ClientID%>').focus(); return false; }
                var AllShName = $('#<%=txtAllShName.ClientID%>').val();
                if (AllShName <= 0) { alert("Type Short Name"); $('#<%=txtAllShName.ClientID%>').focus(); return false; }
                var AllType = $('#<%=DDLAllType.ClientID%> :selected').text();
                if (AllType == "---Select---") { alert("Select Type "); $('#<%=DDLAllType.ClientID%>').focus(); return false; }
                var Alltype_Val = $('#<%=DDLAllType.ClientID%> :selected').val();

                var userent = $("#userent").attr("checked") ? 1 : 0;
                var alw_code = $('#<%=hdnawlcode.ClientID%>').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Master.aspx/SaveData",
                    data: "{'alName':'" + AllName + "','aShName':'" + AllShName + "','alType':'" + Alltype_Val + "','uentr':'" + userent + "','alw_code':'" + alw_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert("Record has been Added successfully!!!");
                        cleardata();
                        getdata();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });

            });

            function getdata() {
                $('#daily tbody tr').remove();
                $('#monthly tbody tr').remove();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Master.aspx/GetData",
                    dataType: "json",
                    success: function (data) {
                        var m = 1;
                        var n = 1;
                        console.log(JSON.stringify(data.d));
                        for (var i = 0; data.d.length; i++) {
                            if (data.d[i].ALW_type == 1) {
                                var str = "<td><input type='hidden' name='alw_code' value='" + data.d[i].ALW_code + "'/><input type='hidden' name='alw_type' value='" + data.d[i].ALW_type + "'/><input type='hidden' name='user_mode' value='" + data.d[i].user_enter + "'/>" + (m++) + "</td><td>" + data.d[i].ALW_name + "</td><td>" + data.d[i].ALW_shname + "</td><td>" + (data.d[i].user_enter == 1 ? 'Yes' : 'No') + "</td><td><a href='#' class='aedit'>Edit</a></td>";

                                // str += "<td><img src='../Images/deact1.png' alt='' width='50px' /></td>";
                                str += "<td><a href='#' class='Deactivate'>Deactivate</a></td>";

                                $('#daily').append('<tr>' + str + '</tr>');
                            }
                            else if (data.d[i].ALW_type == 2) {
                                var str = "<td><input type='hidden' name='alw_code' value='" + data.d[i].ALW_code + "'/><input type='hidden' name='alw_type' value='" + data.d[i].ALW_type + "'/><input type='hidden' name='user_mode' value='" + data.d[i].user_enter + "'/>" + (n++) + "</td><td>" + data.d[i].ALW_name + "</td><td>" + data.d[i].ALW_shname + "</td><td>" + (data.d[i].user_enter == 1 ? 'Yes' : 'No') + "</td><td><a href='#' class='aedit'>Edit</a></td>";
                                // str += "<td><img src='../Images/deact1.png' alt='' width='50px' /></td>";
                                str += "<td><a href='#' class='Deactivate'>Deactivate</a></td>";
                                $('#monthly').append('<tr>' + str + '</tr>');
                            }
                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            }

            $(document).on('click', '.Deactivate', function () {
                var alw_code = $(this).closest('tr').find('td').eq('0').find('input[name=alw_code]').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Allowance_Master.aspx/DeactivateAllow",
                    data: "{'alw_code':'" + alw_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        getdata();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });


            $(document).on('click', '.aedit', function () {
                var alw_code = $(this).closest('tr').find('td').eq('0').find('input[name=alw_code]').val();
                $('#<%=hdnawlcode.ClientID%>').val(alw_code);
                $('#<%=txtAllName.ClientID%>').val($(this).closest('tr').find('td').eq('1').text());
                $('#<%=txtAllShName.ClientID%>').val($(this).closest('tr').find('td').eq('2').text());
                $('#<%=DDLAllType.ClientID%>').val($(this).closest('tr').find('td').eq('0').find('input[name=alw_type]').val());
                var entmode = $(this).closest('tr').find('td').eq('0').find('input[name=user_mode]').val();
                if (entmode == 1) {
                    $('#userent').attr('checked', true);
                }
                else {
                    $('#userent').attr('checked', false);
                }
            });

            function cleardata() {
                $('#<%=hdnawlcode.ClientID%>').val("");
                $('#<%=txtAllName.ClientID%>').val("");
                $('#<%=txtAllShName.ClientID%>').val("");
                $('#<%=DDLAllType.ClientID%>').val(0);
                $('#userent').attr('checked', false);
            };

        });        

    </script>
    <form id="Allowancefrm" runat="server">
    <div class="container" style="width: 40%">
        <asp:HiddenField ID='hdnawlcode' runat="server" />
        <div class="form-group">
            <div class="row">
                <asp:Label ID="Label1" runat="server" Text="Allowance Name" CssClass="col-sm-3 control-label"></asp:Label>
                <div class="col-sm-7 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="txtAllName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="Label2" runat="server" Text="Short Name" CssClass="col-sm-3 control-label"></asp:Label>
                <div class="col-sm-7 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="txtAllShName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="Label3" runat="server" Text="Type" CssClass="col-sm-3 control-label"></asp:Label>
                <div class="col-sm-7 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                        <asp:DropDownList ID="DDLAllType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Daily" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="Label4" runat="server" Text="User Enterable" CssClass="col-sm-3 control-label"></asp:Label>
                <div class="col-sm-7 inputGroupContainer">
                    <div class="checkbox checkbox-primary">
                        <input id="userent" type="checkbox" />
                        <label for="userent">
                        </label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-10" style="text-align: center">
                    <a id="btnSave" type="button" class="btn btn-primary" style="vertical-align: middle">
                        <span>Save</span></a>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="container" style="width: 100%">
        <div class="row">
            <div class="col-sm-6" style="text-align: center">
                <table id="monthly" class="blueTable">
                    <thead>
                        <tr>
                            <th colspan="6">
                                MonthWise
                            </th>
                        </tr>
                        <tr>
                            <th>
                                Sl.No.
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Short Name
                            </th>
                            <th>
                                Entrable Mode
                            </th>
                            <th>
                            </th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-6" style="text-align: center">
                <table id="daily" class="blueTable">
                    <thead>
                        <tr>
                            <th colspan="6">
                                DayWise
                            </th>
                        </tr>
                        <tr>
                            <th>
                                Sl.No.
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Short Name
                            </th>
                            <th>
                                Entrable Mode
                            </th>
                            <th>
                            </th>
                            <th>
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
