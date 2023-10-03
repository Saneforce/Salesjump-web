<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Busn Vs exp_Rpt.aspx.cs" Inherits="MasterFiles_Busn_Vs_exp_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        #FieldForce
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #FieldForce td
        {
            border: 1px solid #ddd;
            padding: 8px;
        }
        
        #FieldForce tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #FieldForce tr:hover
        {
            background-color: #ddd;
        }
        
        #FieldForce th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #496a9a;
            color: white;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval.aspx/Get_Year",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddl_year");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['years']).html(this['years']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $(document).on('click', '#btnGo', function () {
                var selyear = $("#ddl_year").val();
                if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                var selmonth = $("#dll_month").val();
                if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }

                $('#FieldForce tbody tr').remove();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Busn Vs exp_Rpt.aspx/Get_FieldForce",
                    data: "{'exp_years':'" + selyear + "','exp_month':'" + selmonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        details = JSON.parse(data.d) || [];
                        if (details.length > 0) {
                            var slno = 0, str;
                            $('#FieldForce TBODY').html("");
                            for (var i = 0; i < details.length; i++) {
                                slno = i + 1;
                                str += "<tr><td>" + slno +
                                    "</td><td><input type='hidden' class='pcod_hidd' value='" + details[i].Sf_Code + "'>"
                                    + details[i].SF_Name + "</td><td>" + details[i].Designation_Name + "</td><td>" + details[i].Sf_HQ +
                                    "</td><td>" + details[i].StateName + "</td><td>" + details[i].orValue + "</td><td>" + details[i].expense_amt + "</td><td>" + details[i].exp_percentage+'%'+"</td></tr>";
                            }
                            $('#FieldForce TBODY').append(str);
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });
        });
        function exportToExcel() {
            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("FieldForce").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Business_Expense Report' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    </script>
    <form id="Allowancefrm" runat="server">
    <div class="container" style="max-width:50%; width:20%" >
        <div class="row">
            <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Style="text-align: left;
                padding: 8px 4px;" Text="Year" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-sm-8 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <select id="ddl_year" name="txt_year" class="form-control" style="width: 130px">
                        <option>--- Select ---</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                Text="Month" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-sm-8 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <select id="dll_month" name="dll_month" class="form-control" style="width: 130px">
                        <option value="0" label="--- Select---"></option>
                        <option value="1" label="JAN"></option>
                        <option value="2" label="FEB"></option>
                        <option value="3" label="MAR"></option>
                        <option value="4" label="APR"></option>
                        <option value="5" label="MAY"></option>
                        <option value="6" label="JUN"></option>
                        <option value="7" label="JUL"></option>
                        <option value="8" label="AUG"></option>
                        <option value="9" label="SEP"></option>
                        <option value="10" label="OCT"></option>
                        <option value="11" label="NOV"></option>
                        <option value="12" label="DEC"></option>
                    </select>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="text-align: center">
            <div class="col-sm-10 inputGroupContainer">
                <a id="btnGo" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                    <span>Go</span></a></div>
        </div>
    </div>
        </br>
        <%--<div class="container" style="max-width:100%; width:40%" >--%>
         <div class="m-0">
                    <div style="margin-right:50px;position:inherit;" class="col-3">
                        <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" onclick="exportToExcel()" width="40" height="40" id="btnExport" />                        
                    </div>
                </div>
        <div class="row">
            <div class="col-sm-12 inputGroupContainer">
                <table id="FieldForce" class="gvHeader">
                    <thead>
                        <tr>
                            <th>Sl_No.</th>
                            <th>FIELD FORCE NAME</th>
                            <th>DESIGNATION</th>
                            <th>HQ</th>
                             <th>STATE</th>
                             <th>BUSINESS</th>
                            <th>EXPENSES CLAIMED</th>
                            <th>PERCENTAGE(%)</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    <%--</div>--%>
        </form>
</asp:Content>

