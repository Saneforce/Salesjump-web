<%@ Page Title="Customer Rate Fixation" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="CustomerRateFixation.aspx.cs" Inherits="MasterFiles_CustomerRateFixation" %>

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
                $('[id*=customerTable] tr').remove();
                loadRetailer();


            });
            $(document).on('change', '[id*=ddlFieldForce]', function () {
                $('[id*=btnsave]').css('display', 'none');
                $('[id*=totTable]').css('display', 'none');
                $('[id*=customerTable] tr').remove();
            });
            $(document).on('change', '[id*=ddlMR]', function () {
                $('[id*=btnsave]').css('display', 'none');
                $('[id*=totTable]').css('display', 'none');
                $('[id*=customerTable] tr').remove();
            });

            function loadRetailer() {

                $('[id*=customerTable] tr').remove();
                var sF = $('[id*=ddlMR]').val();
                FYear = $('[id*=ddlFYear]').val();
                FMonth = $('[id*=ddlFMonth]').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CustomerRateFixation.aspx/GetRetailers",
                    data: "{'sf_code':'" + sF + "'}",
                    dataType: "json",
                    success: function (data) {

                        var now = new Date(FYear + '/' + FMonth + '/1');
                        var dt = new Date(FYear + '/' + FMonth + '/1');

                        var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                        str = '';
                        len = 0;
                        while (sixMonthsFromNow < dt) {
                            str += '<th>Sales on ' + month[sixMonthsFromNow.getMonth()] + ' - ' + sixMonthsFromNow.getFullYear() + '</th>';
                            sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                            len++;
                        }


                        dtls = data.d;
                        $('[id*=customerTable]').append('<tr><th>Slno</th><th>Retailer Name</th><th>Category</th>' + str + '<th>Target</th><tr>');
                        var rtName = '';
                        var cnt = 1;
                        var rtCnt = 0;
                        for (var i = 0; i < dtls.length; i++) {
                            if (rtName != dtls[i].RoName) {
                                rtCnt++;
                                stk = '<td colspan="2" >Route Name : ' + dtls[i].RoName + '</td><td></td>';

                                var now = new Date(FYear + '/' + FMonth + '/1');
                                var dt = new Date(FYear + '/' + FMonth + '/1');

                                var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                                while (sixMonthsFromNow < dt) {
                                    stk += '<td style="text-align:right;"><span id="Route' + rtCnt + '' + month[sixMonthsFromNow.getMonth()] + '">0</span></td>';
                                    sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                                }
                                $('[id*=customerTable]').append('<tr style="background: #19a4c6; color:#fff">' + stk + ' <td style="text-align:right;"><span id="Route' + rtCnt + '">0</span></td> </tr>');
                                cnt = 1;

                            }
                            stk = '<td>' + (cnt++) + '</td><td> <input type="hidden" name="custCode" value="' + dtls[i].custCode + '"/> ' + dtls[i].custName + '</td> <td>' + dtls[i].custClass + '</td>';

                            var now = new Date(FYear + '/' + FMonth + '/1');
                            var dt = new Date(FYear + '/' + FMonth + '/1');

                            var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                            while (sixMonthsFromNow < dt) {

                                stk += '<td class="moncls"><span  name="oldAmt" mNum=' + (sixMonthsFromNow.getMonth() + 1) + '  yNum=' + sixMonthsFromNow.getFullYear() + '  ></span> </td> ';
                                sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                            }
                            stk += '<td><input type="text" style="text-align:right;" name="txtqty" maxlength="6"  Class="form-control" rName="Route' + rtCnt + '"/></td>';
                            $('[id*=customerTable]').append('<tr>' + stk + '</tr>');
                            rtName = dtls[i].RoName
                        }


                        stf = '<td colspan="3" >Total</td>';

                        var now = new Date(FYear + '/' + FMonth + '/1');
                        var dt = new Date(FYear + '/' + FMonth + '/1');

                        var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                        while (sixMonthsFromNow < dt) {

                            stf += '  <td style="text-align:right;font-weight: bold;"><span class="ovOldtot">0</span></td> ';
                            sixMonthsFromNow.setMonth(sixMonthsFromNow.getMonth() + 1)
                        }

                        stf += '<td style="text-align:right;font-weight: bold;"><span id="ovtot">0</span></td>';
                        $('[id*=customerTable]').append('<tr style="background: #496a9a; color:#fff">' + stf + '</tr>');
                        $('[id*=btnsave]').css('display', '');
                        $('[id*=totTable]').css('display', '');
                        loadData();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });



            }


            function loadData() {
                var sF = $('[id*=ddlMR]').val();
                FYear = $('[id*=ddlFYear]').val();
                FMonth = $('[id*=ddlFMonth]').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CustomerRateFixation.aspx/GetCustTargets",
                    data: "{'sf_code':'" + sF + "', 'FYear':'" + FYear + "','FMonth':'" + FMonth + "'}",
                    dataType: "json",
                    success: function (data) {
                        sdt = data.d;
                        if (sdt.length > 0) {
                            var sum = 0;
                            $('[id*=customerTable] tr').each(function () {
                                custCode = $(this).find('[name*=custCode]').val();
                                var fA = sdt.filter(function (obj) {
                                    return (obj.custCode == custCode);
                                });
                                if (fA.length > 0) {
                                    $(this).find('[name*=txtqty]').val(fA[0].cAmount);
                                    $(this).find('[name*=txtqty]').attr('pv', fA[0].cAmount);
                                    sum += Number(fA[0].cAmount);

                                    var rNa = $(this).find('[name*=txtqty]').attr('rName');
                                    var rTo = parseFloat($('[id~=' + rNa + ']').text());
                                    rTo = rTo + Number(fA[0].cAmount);
                                    $('[id~=' + rNa + ']').text(rTo);

                                }
                            });
                            $('[id*=ovtot]').text(sum);
                            $('[id*=lbltopTot]').text(sum);
                        }

                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });


                var now = new Date(FYear + '/' + FMonth + '/1');
                var dt = new Date(FYear + '/' + FMonth + '/1');

                var sixMonthsFromNow = new Date(now.setMonth(now.getMonth() - 6));
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "CustomerRateFixation.aspx/getCustomerSales",
                    data: "{'SF_Code':'" + sF + "', 'FYear':'" + now.getFullYear() + "', 'FMonth':'" + (now.getMonth() + 1) + "', 'TYear':'" + dt.getFullYear() + "', 'TMonth':'" + (dt.getMonth() + 1) + "'}",
                    dataType: "json",
                    success: function (data) {
                        sdt = data.d;
                        sum = [];
                        if (sdt.length > 0) {
                            //var sum = 0;
                            $('[id*=customerTable] tr').each(function () {
                                custCode = $(this).find('[name*=custCode]').val();
                                rNa = $(this).find('[name*=txtqty]').attr('rname');

                                i = 0;
                                $(this).find('.moncls').each(function () {
                                    cM = $(this).find('span:first').attr('mNum');
                                    cY = $(this).find('span:first').attr('yNum');
                                    var fA = sdt.filter(function (obj) {
                                        return (obj.cCode === custCode && obj.cMonth == cM && obj.cYear == cY);
                                    });

                                    sss = 0;
                                    if (fA.length > 0) {
                                        $(this).find('[name*=oldAmt]').text(fA[0].ordVal);
                                        sss = Number(fA[0].ordVal);
                                        sum[i] = Number(sum[i] || 0) + Number(sss);


                                        var rTo = parseFloat($('[id~=' + rNa + '' + month[cM - 1] + ']').text() || 0);
                                        //  console.log(rTo);
                                        rTo = rTo + Number(fA[0].ordVal || 0);
                                        //                                        console.log($('[id~=' + rNa + '' + month[cM] + ']').text());
                                        //                                        console.log($('[id~=' + rNa + '' + month[cM] + ']'));
                                        $('[id~=' + rNa + '' + month[cM - 1] + ']').text(rTo);

                                    }
                                    else {
                                        sum[i] = Number(sum[i] || 0) + Number(sss);
                                    }
                                    i++;
                                });
                            });
                            $('.ovOldtot').each(function (index) {
                                $(this).text(Number(sum[index]));
                            });
                        }
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });

                //                $.ajax({
                //                    type: "POST",
                //                    contentType: "application/json; charset=utf-8",
                //                    url: "CustomerRateFixation.aspx/GetCustTargets",
                //                    data: "{'sf_code':'" + sF + "', 'FYear':'" + FYear + "','FMonth':'" + (FMonth - 1) + "'}",
                //                    dataType: "json",
                //                    success: function (data) {
                //                        sdt = data.d;
                //                        if (sdt.length > 0) {
                //                            var sum = 0;
                //                            $('[id*=customerTable] tr').each(function () {
                //                                custCode = $(this).find('[name*=custCode]').val();
                //                                var fA = sdt.filter(function (obj) {
                //                                    return (obj.custCode == custCode);
                //                                });
                //                                if (fA.length > 0) {
                //                                    $(this).find('[name*=oldAmt]').text(fA[0].cAmount);
                //                                    sum += Number(fA[0].cAmount);
                //                                }
                //                            });
                //                            $('[id*=ovOldtot]').text(sum);
                //                        }
                //                    },
                //                    error: function (data) {
                //                        alert(JSON.stringify(data));
                //                    }
                //                });
            }



            $(document).on('keypress', '[name*=txtqty]', function (event) {
                return isNumberOnly(event, this)
            });

            $(document).on('keyup', 'input[name=txtqty]', function () {
                var t = parseFloat($(this).val());
                var rNa = $(this).attr('rName');
                if (isNaN(t)) t = 0;
                var pv = parseFloat($(this).attr('pv'));
                if (isNaN(pv)) pv = 0;
                var tot = parseFloat($('[id*=ovtot]').text());
                var rTo = parseFloat($('[id~=' + rNa + ']').text());
                if (isNaN(tot)) tot = 0;
                if (isNaN(rTo)) rTo = 0;
                tot = (tot - pv) + Number(t);
                rTo = (rTo - pv) + Number(t);
                $('[id*=ovtot]').text(tot);
                $('[id*=lbltopTot]').text(tot);
                $('[id~=' + rNa + ']').text(rTo);

                $(this).attr('pv', t);
            });


            $(document).on('click', '[id*=btnsave]', function (event) {

                var Team = $('[id*=ddlFieldForce]').val();
                if (Team == "0") { alert('Select Team..!'); $('[id*=ddlFieldForce]').focus(); return false; }
                var sF = $('[id*=ddlMR]').val();
                if (sF == "0") { alert('Select Field Force..1'); $('[id*=ddlMR]').focus(); return false; }

                var chcon = false;


                var str = '<ROOT>';
                $('[id*=customerTable] tr').each(function () {
                    if ($(this).index() > 1) {
                        if (Number($(this).find('[name*=txtqty]').val()) > 0) {
                            chcon = true;
                            custCode = $(this).find('[name*=custCode]').val();
                            cAmount = $(this).find('[name*=txtqty]').val();
                            custSFCode = $('[id*=ddlMR]').val();
                            FYear = $('[id*=ddlFYear]').val();
                            FMonth = $('[id*=ddlFMonth]').val();
                            diVcode = '<%= Session["div_code"] %>';
                            str += '<Cus sfCode=\"' + custSFCode + '\" custCode=\"' + custCode + '\" FYear=\"' + FYear + '\" FMonth=\"' + FMonth + '\" cAmount=\"' + cAmount + '\"  divCode=\"' + diVcode + '\" />';
                        }
                    }
                });
                str += '</ROOT>';

                if (chcon) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "CustomerRateFixation.aspx/UploadXML",
                        data: "{'data':'" + str + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data.d);
                            loadRetailer();
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
                else {
                    alert('No Retailer in the List..!');
                    return false;
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
        .newStly td
        {
            padding: 0px 5px !important;
        }
        .moncls
        {
            text-align: right;
        }
    </style>
    <form id="form1" runat="server">
    <div class="container">
        <div class="form-group">
            <div class="row">
                <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Division" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
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
                <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Team" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
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
                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Field Force" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
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
                <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                <div class="col-sm-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control" Width="120">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
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
                            <td style="width:70%">
                                <label id="Label3">
                                    Total</label>
                            </td>
                            <td style="width:30%; text-align:right"  >
                                <label id="lbltopTot">
                                    0.00</label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="customerTable" runat="server" style="width: 100%" class="newStly">
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
