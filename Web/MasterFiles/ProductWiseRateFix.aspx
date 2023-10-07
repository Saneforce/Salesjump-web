<%@ Page Title="Product Wise Target Fixation" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ProductWiseRateFix.aspx.cs" Inherits="MasterFiles_ProductWiseRateFix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";


            $('[id*=btnsave]').css('display', 'none');
            $('[id*=totTable]').css('display', 'none');


            $(document).on('click', '[id*=btnview]', function () {

                var Team = $('[id*=ddlFieldForce]').val();
                if (Team == "0") { alert('Select Team..!'); $('[id*=ddlFieldForce]').focus(); return false; }
                var sF = $('[id*=ddlMR]').val();
                if (sF == "0") { alert('Select Field Force..1'); $('[id*=ddlMR]').focus(); return false; }
                $('[id*=ProductTable] tr').remove();
                $('[id*=btnview]').text('Please Wait..!');
                loadRetailer();


            });
            $(document).on('change', '[id*=ddlFieldForce]', function () {
                $('[id*=btnsave]').css('display', 'none');
                $('[id*=totTable]').css('display', 'none');
                $('[id*=ProductTable] tr').remove();
            });
            $(document).on('change', '[id*=ddlMR]', function () {
                $('[id*=btnsave]').css('display', 'none');
                $('[id*=totTable]').css('display', 'none');
                $('[id*=ProductTable] tr').remove();
            });

            function loadRetailer() {

                $('[id*=ProductTable] tr').remove();
                var sF = $('[id*=ddlMR]').val();
                var subDiv = $('[id*=subdiv]').val();

                FYear = $('[id*=ddlFYear]').val();
                FMonth = $('[id*=ddlFMonth]').val();

                var rDts = [];
                var sDts = [];

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductWiseRateFix.aspx/getrates",
                    data: "{'data':'" + subDiv + "'}",
                    dataType: "json",
                    success: function (data) {
                        rDts = data.d;
                    },
                    error: function (result) {
                        console.log(rs);
                        alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductWiseRateFix.aspx/getfo",
                    data: "{'term':'" + sF + "'}",
                    dataType: "json",
                    success: function (data) {
                        sDts = data.d;
                    },
                    error: function (result) {
                        console.log(rs);
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ProductWiseRateFix.aspx/GetProduct",
                    data: "{'subDiv':'" + subDiv + "'}",
                    dataType: "json",
                    success: function (data) {

                        var now = new Date(FYear + '/' + FMonth + '/1');
                        var dt = new Date(FYear + '/' + FMonth + '/1');

                        var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                        str = '';
                        len = 0;
                        while (sixMonthsFromNow < dt) {
                            str += '<th>SAL-QTY-' + month[sixMonthsFromNow.getMonth()] + ' - ' + sixMonthsFromNow.getFullYear() + '</th>';
                            sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                            len++;
                        }


                        dtls = data.d;
                        $('[id*=ProductTable]').append('<tr><th>SlNo.</th><th>Product Name</th>' + str + '<th>Target</th><tr>');
                        var rtName = '';
                        var cnt = 1;

                        for (var i = 0; i < dtls.length; i++) {
                            stk = '<td>' + (cnt++) + '</td><td> <input type="hidden" name="pcode" value="' + dtls[i].product_id + '"/> ' + dtls[i].product_name + '</td>';
                            var now = new Date(FYear + '/' + FMonth + '/1');
                            var dt = new Date(FYear + '/' + FMonth + '/1');
                            var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                            while (sixMonthsFromNow < dt) {
                                stk += '<td class="moncls"><span  name="oldAmt" mNum=' + (sixMonthsFromNow.getMonth() + 1) + '  yNum=' + sixMonthsFromNow.getFullYear() + '  ></span> </td> ';
                                sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                            }

                            rr = rDts.filter(function (obj) {
                                return (obj.stCode === sDts[0].stCode) && (obj.pCode === dtls[i].product_id);
                            });
                            var rt = 0;
                            if (rr.length > 0) {
                                rt = rr[0].mRate;
                            }

                            stk += '<td><input type="hidden" name="mRate" value="' + rt + '"/><input type="text" style="text-align:right;" name="target" maxlength="6"  Class="form-control" /></td>';
                            $('[id*=ProductTable]').append('<tr>' + stk + '</tr>');

                        }
                        stf = '<td colspan="2" >Total</td>';
                        var now = new Date(FYear + '/' + FMonth + '/1');
                        var dt = new Date(FYear + '/' + FMonth + '/1');
                        var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                        while (sixMonthsFromNow < dt) {
                            stf += '  <td style="text-align:right;font-weight: bold;"><span class="ovOldtot">0</span></td> ';
                            sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                        }

                        stf += '<td style="text-align:right;font-weight: bold;"><span id="ovtot">0</span></td>';
                        $('[id*=ProductTable]').append('<tr style="background: #496a9a; color:#fff">' + stf + '</tr>');
                        $('[id*=btnsave]').css('display', '');
                        $('[id*=totTable]').css('display', '');
                        $('[id*=btnview]').text('View');
                        loadData();
                    },
                    error: function (data) {
                        console.log(rs);
                        alert(JSON.stringify(data));

                    }

                });
                // $('[id*=btnview]').text('View');



            }

            function loadData() {
                $('[id*=ovtot]').text(0);
                $('[id*=lbltopTot]').text(0);
                var sF = $('[id*=ddlMR]').val();
                FYear = $('[id*=ddlFYear]').val();
                FMonth = $('[id*=ddlFMonth]').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductWiseRateFix.aspx/gettarget",
                    data: "{'sf_code':'" + sF + "','years':'" + FYear + "','months':'" + FMonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        if (data.d.length > 0) {
                            var sum = 0;
                            $('#ProductTable tbody tr').each(function (index) {
                                for (var i = 0; i < data.d.length; i++) {
                                    if ($(this).children('td').eq(1).find('input[name=pcode]').val() == data.d[i].pcode) {
                                        $(this).children('td').eq(8).find('input[name=target]').val(data.d[i].target);
                                        $(this).children('td').eq(8).find('input[name=target]').attr('pv', data.d[i].target);
                                        sum += Number($(this).children('td').eq(8).find('input[name=mRate]').val()) * Number(data.d[i].target);
                                    }
                                }
                            });
                            $('[id*=ovtot]').text(sum.toFixed(0));
                            $('[id*=lbltopTot]').text(sum.toFixed(0));
                        }

                    },
                    error: function (result) {
                        console.log(rs);
                        alert(JSON.stringify(result));
                    }
                });


                var now = new Date(FYear + '/' + FMonth + '/1');
                var dt = new Date(FYear + '/' + FMonth + '/1');

                var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "ProductWiseRateFix.aspx/getProductSales",
                    data: "{'SF_Code':'" + sF + "', 'FYear':'" + now.getFullYear() + "', 'FMonth':'" + (now.getMonth() + 1) + "', 'TYear':'" + dt.getFullYear() + "', 'TMonth':'" + (dt.getMonth() + 1) + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        sdt = data.d;
                        sum = [];
                        if (sdt.length > 0) {
                            //var sum = 0;
                            $('[id*=ProductTable] tr').each(function () {
                                pCode = $(this).find('[name*=pcode]').val();
                                pAmt = Number($(this).children('td').eq(8).find('input[name=mRate]').val());
                                i = 0;
                                $(this).find('.moncls').each(function () {
                                    cM = $(this).find('span:first').attr('mNum');
                                    cY = $(this).find('span:first').attr('yNum');
                                    var fA = sdt.filter(function (obj) {
                                        return (obj.pCode === pCode && obj.cMonth == cM && obj.cYear == cY);
                                    });
                                    cVl = 0;
                                    if (fA.length > 0) {
                                        $(this).find('[name*=oldAmt]').text(fA[0].qty);
                                        cVl = pAmt * Number(fA[0].qty);
                                        sss = Number(cVl);
                                        sum[i] = Number(sum[i] || 0) + Number(sss);
                                    }
                                    else {
                                        sum[i] = Number(sum[i] || 0) + Number(cVl);
                                    }
                                    i++;
                                });
                            });
                            $('.ovOldtot').each(function (index) {
                                $(this).text(Number(sum[index]).toFixed(0));
                            });
                        }
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
            }

            $(document).on('keypress', '[name*=target]', function (event) {
                return isNumberOnly(event, this)
            });

            $(document).on('keyup', 'input[name=target]', function () {
                var idx = $(this).closest('tr').index();
                var rat = $(this).closest('td').find('input[name=mRate]').val();
                var cTxt = $(this).val();
                var t = parseFloat($(this).val());
                if (isNaN(t)) t = 0;
                var pv = parseFloat($(this).attr('pv'));
                if (isNaN(pv)) pv = 0;
                var tot = parseFloat($('[id*=ovtot]').text());
                if (isNaN(tot)) tot = 0;
                tot = (tot - (pv * rat)) + Number(t * rat);
                $('[id*=ovtot]').text(tot.toFixed(0));
                $('[id*=lbltopTot]').text(tot.toFixed(0));
                $(this).attr('pv', t);
            });


            $(document).on('click', '#btnsave', function () {

                $('#btnsave').text('Please Wait..!');

                var Team = $('[id*=ddlFieldForce]').val();
                if (Team == "0") { alert('Select Team..!'); $('[id*=ddlFieldForce]').focus(); return false; }
                var sF = $('[id*=ddlMR]').val();
                if (sF == "0") { alert('Select Field Force..1'); $('[id*=ddlMR]').focus(); return false; }
                var arr = [];
                $('#ProductTable tbody tr').each(function () {
                    if (Number($(this).children('td').eq(8).find('input[name=target]').val()) > 0) {
                        arr.push({
                            sf_code: sF,
                            target: $(this).children('td').eq(8).find('input[name=target]').val(),
                            rate: $(this).children('td').eq(8).find('input[name=mRate]').val(),
                            pcode: $(this).children('td').eq(1).find('input[name=pcode]').val(),
                            months: $('#<%=ddlFMonth.ClientID%>').val(),
                            years: $('#<%=ddlFYear.ClientID%>').val(),
                            rsf: Team
                        });
                    }

                });

                if (arr.length > 0) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ProductWiseRateFix.aspx/savedata",
                        data: "{'data':'" + JSON.stringify(arr) + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#btnsave').text('Save');
                            alert("Product Target has been updated successfully!!!");
                            loadRetailer();
                        },
                        error: function (data) {
                            $('#btnsave').text('Save');
                            alert(JSON.stringify(data));
                        }
                    });
                }
                else {
                    $('#btnsave').text('Save');
                    alert("No Row Enter Values..!");
                }
            });
        });
        function isNumberOnly(evt, element) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <style type="text/css">
        .newStly td {
            padding: 0px 5px !important;
        }

        .moncls {
            text-align: right;
        }
    </style>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Division" Style="text-align: right; padding: 8px 4px;"
                        CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                                OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Team" Style="text-align: right; padding: 8px 4px;"
                        CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                Width="350" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Field Force" Style="text-align: right; padding: 8px 4px;"
                        CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlMR" runat="server" Visible="true" CssClass="form-control"
                                Width="350">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: right; padding: 8px 4px;"
                        CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-sm-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control" Width="120">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month" Style="text-align: right; padding: 8px 4px;"
                        CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-sm-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFMonth" runat="server" Width="120" CssClass="form-control">
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
                    <div class="col-sm-12" style="text-align: center">
                        <button id="btnview" type="button" class="btn btn-primary" style="vertical-align: middle">
                            View</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="container" style="max-width: 90%">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <table id="totTable" style="width: 100%; background: #19a4c6; color: #fff;">
                            <tr>
                                <td style="width: 70%">
                                    <label id="Label3">
                                        Total</label>
                                </td>
                                <td style="width: 30%; text-align: right">
                                    <label id="lbltopTot">
                                        0.00</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <table id="ProductTable" style="width: 100%" class="newStly">
                        </table>
                    </div>
                </div>
                <br />
                <div class="row" style="text-align: center">
                    <div class="col-md-12 inputGroupContainer">
                        <a id="btnsave" class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;">
                            <span>Save</span></a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
