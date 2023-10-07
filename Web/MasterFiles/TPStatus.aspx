<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="TPStatus.aspx.cs" Inherits="MIS_Reports_TPStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 0px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
		.ddlDivision {
            height: 31px !important;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ('<%=Session["div_code"]%>' != '107') {
                $('.deporow').hide();
            }
            if ('<%=Session["div_code"]%>' == '107') {
                $('.deporow').show();
            }
			$('.export').hide();
            $(document).on('click', '.btnview', function () {

                var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
                if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }
				var sfdepot = $("#<%=ddldepo.ClientID%>").val();

                $('#Product_Table tr').remove();
                var len = 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "TPStatus.aspx/getdata",
                    dataType: "json",
                    success: function (data) {
                        len = data.d.length;
                        if (data.d.length > 0) {
                            str = '<th  style="min-width:300px; > <p style="margin: 0 0 0px;">Field Force</p> </th><th  style="min-width:200px;> <p style="margin: 0 0 0px;">TP Entry</p> </th> <th  style="min-width:200px;> <p style="margin: 0 0 0px;">TP Entry Date</p> </th>';
                            // str1 = '<th  style="min-width:200px; " " rowspan="2"> <p style="margin: 0 0 0px;">TP Entry</p> </th>'
                            str += '<th  style="min-width:200px; > <p style="margin: 0 0 0px;">TP Approvals</p> </th><th  style="min-width:200px; > <p style="margin: 0 0 0px;">TP Approvals Date</p> </th><th  style="min-width:200px;> <p style="margin: 0 0 0px;">TP Dates</p> </th>';
                            // str2 = '<th  style="min-width:200px; " " rowspan="2"> <p style="margin: 0 0 0px;">TP Dates</p> </th>'
                            str += '<th  style="min-width:200px;> <p style="margin: 0 0 0px;">Attendance</p> </th>';
                            //for (var i = 0; i < data.d.length; i++) {
                            //str += '<th style="min-width:150px" colspan="2"> <input type="hidden" name="pcode" value="' +  + '"/> <p name="pname" style="margin: 0 0 0px;">"'+"TP Entry" +'"</p> </th>';
                            //str1 += '<th>Quantity</th><th>Value</th>';
                            //strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';
                            // }
                            $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');
                            //$('#Product_Table thead').append('<tr class="mainhead">' + str1 + '</tr>');
                            //$('#Product_Table thead').append('<tr class="mainhead">' + strff + '</tr>');
                        }
                        else{
                            str = '<th  style="min-width:300px; > <p style="margin: 0 0 0px;">Field Force</p> </th><th  style="min-width:200px;> <p style="margin: 0 0 0px;">TP Entry</p> </th> <th  style="min-width:200px;> <p style="margin: 0 0 0px;">TP Entry Date</p> </th>';
                            // str1 = '<th  style="min-width:200px; " " rowspan="2"> <p style="margin: 0 0 0px;">TP Entry</p> </th>'
                            str += '<th  style="min-width:200px; > <p style="margin: 0 0 0px;">TP Approvals</p> </th><th  style="min-width:200px; > <p style="margin: 0 0 0px;">TP Approvals Date</p> </th><th  style="min-width:200px;> <p style="margin: 0 0 0px;">TP Dates</p> </th>';
                            // str2 = '<th  style="min-width:200px; " " rowspan="2"> <p style="margin: 0 0 0px;">TP Dates</p> </th>'
                            str += '<th  style="min-width:200px;> <p style="margin: 0 0 0px;">Attendance</p> </th>';
                            //for (var i = 0; i < data.d.length; i++) {
                            //str += '<th style="min-width:150px" colspan="2"> <input type="hidden" name="pcode" value="' +  + '"/> <p name="pname" style="margin: 0 0 0px;">"'+"TP Entry" +'"</p> </th>';
                            //str1 += '<th>Quantity</th><th>Value</th>';
                            //strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';
                            // }
                            $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');
                            //$('#Product_Table thead').append('<tr class="mainhead">' + str1 + '</tr>');
                            //$('#Product_Table thead').append('<tr class="mainhead">' + strff + '</tr>');
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });



                var Fyear = $("#<%=ddlFYear.ClientID%>").val();
                var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
                var SubDiv = $("#<%=ddlDivision.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "TPStatus.aspx/getIssuData",
                    data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "','SFDepot':'" + sfdepot + "','SubDiv':'" + SubDiv + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(JSON.stringify(data.d));
                        if (data.d.length > 0) {

                            $('#btnExcel').css('display', 'table-cell');

                            var sfCode = '';
                            for (var i = 0; i < data.d.length; i++) {

                                var str = '';
                                if (sfCode == data.d[i].sfCode) {

                                }
                                else {
                                    str = '<td><input class="AAA" type="hidden" name="sfcode" value="' + data.d[i].sfCode + '"/> <a class="AAA" href="Report/rptTPView.aspx?&sf_code=' + data.d[i].sfCode + '&cur_month=' + data.d[i].Month + '&cur_year=' + data.d[i].Year + '&sf_name=' + data.d[i].sfName + '&level=' + -1 + '" name="sfname" style="margin: 0 0 0px;">' + data.d[i].sfName + '</a> </td>';


                                    $('#Product_Table tbody').append('<tr>' + str + '<td></td><td>' + data.d[i].tDate + '</td><td></td><td>' + data.d[i].adate + '</td><td></td><td></td></tr>');
                                }
                                sfCode = data.d[i].sfCode;
                            }

                            function daysInMonth(month, year) {
                                return new Date(year, month, 0).getDate();
                            }


                            var dtls_tab = document.getElementById("Product_Table");
                            var nrows1 = dtls_tab.rows.length;
                            var Ncols = dtls_tab.rows[0].cells.length;
                            console.log(Ncols);
                            for (var i = 0; i < data.d.length; i++) {
                                $('#Product_Table tbody tr').each(function () {
                                    fd = 0;
                                    curr = 0;
                                    for (var col = 0; col < Ncols - 1; col++) {
                                        fd = col + 1;
                                        if ($(this).children('td').eq(0).find('input[name=sfcode]').val() == data.d[i].sfCode) {
                                            curr = i;
                                            //alert(data.d[i].caseRate);
                                            //if ($(this).closest('table').find('.mainhead').find('th').eq(fd).find('input[name=pcode]').val() == data.d[i].proCode) {
                                            if (data.d[i].caseRate > 0) {

                                                // $(this).children('td').eq(1).text('✔');

                                                $(this).children('td').eq(1).css('color', 'Green');
                                                $(this).children('td').eq(1).css('font-family', 'Wingdings');
                                                $(this).children('td').eq(1).css('font-size', '25px');
                                                $(this).children('td').eq(1).html('<span>&#61692;</span>');



                                                //                                                   <asp:Label ID="L5" runat="server" Style="font-family: Wingdings, Times, serif; color: Green;
                                                //                                                    font-size: 25px;" Visible="false">&#61692;</asp:Label>
                                                //                                                <asp:Label ID="L6" runat="server" Style="font-family: Wingdings, Times, serif; color: Red;
                                                //                                                    font-size: 25px;" Visible="false">&#61691;</asp:Label>
                                            }
                                            else {
                                                //  $(this).children('td').eq(1).text('✗');

                                                $(this).children('td').eq(1).css('color', 'Red');
                                                $(this).children('td').eq(1).css('font-family', 'Wingdings, Times, serif');
                                                $(this).children('td').eq(1).css('font-size', '25px');
                                                $(this).children('td').eq(1).html('&#61691;');
                                            }
                                            $(this).children('td').eq(5).text(data.d[i].amount + "/" + daysInMonth(data.d[i].Month, data.d[i].Year));
                                            if (data.d[i].TC_Count > 0) {
                                                $(this).children('td').eq(3).html('&#61692;');
                                                $(this).children('td').eq(1).html('&#61692;');
                                                // $(this).children('td').eq(1).text('✔');
                                                //  $(this).children('td').eq(2).text('✔');
                                                $(this).children('td').eq(3).css('color', 'Green');
                                                $(this).children('td').eq(1).css('color', 'Green');
                                                $(this).children('td').eq(1).css('font-family', 'Wingdings, Times, serif');
                                                $(this).children('td').eq(1).css('font-size', '25px');
                                                $(this).children('td').eq(3).css('font-size', '25px');
                                                $(this).children('td').eq(3).css('font-family', 'Wingdings, Times, serif');
                                            }
                                            else {

                                                $(this).children('td').eq(3).css('color', 'Red');
                                                $(this).children('td').eq(3).css('font-family', 'Wingdings, Times, serif');
                                                $(this).children('td').eq(3).css('font-size', '25px');
                                                $(this).children('td').eq(3).html('&#61691;');
                                                // $(this).children('td').eq(2).text('✗');
                                            }
                                            $(this).children('td').eq(6).text(data.d[i].EC_Count);

                                        }
                                    }
                                    fd++;
                                    if ($(this).children('td').eq(0).find('input[name=sfcode]').val() == data.d[i].sfCode) {
                                        //$(this).children('td').eq((fd * 2) - 1).text(data.d[i].TC_Count);
                                        //$(this).children('td').eq((fd * 2)).text(data.d[i].EC_Count);
                                    }

                                });
                            }


                        }
                        else {
                            $('#btnExcel').css('display', 'none');
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                //                var arr = [];
                //                $('#Product_Table tbody tr').each(function () {
                //                    var i = 0;
                //                    $(this).find('td').each(function () {
                //                        if (i != 0) {
                //                            arr[i - 1] = Number(arr[i - 1] || 0) + Number($(this).text() || 0);
                //                        }
                //                        i++;
                //                    });
                //                });
                //                // console.log(arr);

                //                var i = 0;
                //                $('.trfoot th').each(function () {
                //                    if (i != 0) {
                //                        //console.log($(this));

                //                        $(this).text(arr[i - 1]);

                //                    }
                //                    i++;
                //                });
                $(".AAA").click(function () {
                    event.preventDefault();
                    window.open($(this).attr("href"), "popupWindow", "width=800,height=600,scrollbars=yes");
                });

$('.export').show();


            });

            //            $(document).on('click', ".btnExcel", function () {
            //                var printableArea = $('#printableArea').html();
            //                window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#printableArea').html()));
            //                e.preventDefault();
            //            });


            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('printableArea');
                var table_html = "<html><head><meta http-equiv=Content-Type content='text/html; charset=UTF-8'/></head><body>"+table_div.outerHTML.replace(/ /g, '%20')+"</body></html>";
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'TP_Status_' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();

            });




        });
    </script>
    <form id="form1" runat="server">
    <div class="container" style="width: 100%;">
        <div class="row">
            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                Sub Division</label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group">
                   <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                   <asp:DropDownList ID="ddlDivision" SkinID="ddlRequired" runat="server" AutoPostBack="true" CssClass="form-control ddlDivision"  
                       OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                   </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                Field Force</label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="form-control" Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row deporow">
            <label for="ddldepo" class="col-md-2 col-md-offset-3 control-label">
                Depot</label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                    <asp:DropDownList ID="ddldepo" runat="server" CssClass="form-control" Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="txtMonth" class="col-md-2  col-md-offset-3  control-label">
                Month</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="form-control">
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label for="txtYear" class="col-md-2  col-md-offset-3  control-label">
                Year</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6  col-md-offset-5">
                <a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">
                    View</a>
            </div>
        </div>
		<div style="float:right;">
            <asp:LinkButton ID="exceldld" runat="server" class="btn btnExcel export" OnClick="exceldld_Click" />
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div id="printableArea" class="page">
                    <table id="Product_Table" class="newStly" style="width:90%;    margin: auto">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot>
                        </tfoot>
                    </table>
                </div>
            </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-6  col-md-offset-5">
            <a name="btnExcel" id="btnExcel" type="button" class="btn btn-primary"  style="width: 100px; display: none">
                Excel</a>
        </div>
    </div>
    </div>
    </form>
</asp:Content>
