<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Product_Sequence.aspx.cs" Inherits="MasterFiles_rpt_Product_Sequence" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Sequence</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {

            var sfCode = $('#<%=subdiv_code.ClientID%>').val();
            var fYear = $('#<%=hFYear.ClientID%>').val();
            var fMonth = $('#<%=hFMonth.ClientID%>').val();
            var tYear = $('#<%=hTYear.ClientID%>').val();
            var tMonth = $('#<%=hTMonth.ClientID%>').val();
            var rDtls = [];
            var cDtls = [];



            genReport = function () {
                if (rDtls.length > 0 && cDtls.length > 0) {
                    var tbl = $('[id*=cTable]');
                    $(tbl).find('thead').find('tr').remove();
                    $(tbl).find('tbody').find('tr').remove();
                    var len = 1;
                    var str = '<th rowspan="1" style="width:70%">Name</th><th rowspan="1" style="width:30%">Sequence No</th><th rowspan="1" style="width:30%">Print Sequence</th>';
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + str + '</tr>');


                    for (var i = 0; i < rDtls.length; i++) {
                        var str = '<td >' + rDtls[i].catName + '</td><td></td><td></td> ';
                        $(tbl).find('tbody').append('<tr style="background-color: #39435C;color: #fff;font-weight: bold;">' + str + '</tr>');

                        vR = cDtls.filter(function (obj) {
                            return (obj.cCode === rDtls[i].catCode);
                        });

                        //                          var lR = vR.filter((arr, index, self) =>
                        //                            index === self.findIndex((t) => (t.cCode === arr.cCode )))


                        if (vR.length > 0) {
                            for (var k = 0; k < vR.length; k++) {
                                var slno = (k + 1);
                                str = '<td><input type="hidden" name="pCode" value="' + vR[k].pCode + '"/> ' + vR[k].pName + '</td><td><input type="text" name="txtsequence" oldVal="' + vR[k].sequenceNo + '" class="' + rDtls[i].catCode + '" value="' + vR[k].sequenceNo + '"/></td><td><input type="text" name="txtprintsequence" oldVal="' + vR[k].printsequence + '" class="' + rDtls[i].catCode + '" value="' + vR[k].printsequence + '"/></td>';
                                $(tbl).find('tbody').append('<tr class="rtclass">' + str + '</tr>');
                            }
                        }
                        //$(tbl).find('tbody').append('<tr style="color:blue;background-color: #99FFFF;"><td  colspan="5">Total</td>' + stk + ' <td>' + Number(ovTTot)+ '</td>  <td>' + Number(ovSTot)+ '</td> </tr>');
                    }

                }
            }


            function loadData() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Product_Sequence.aspx/getCategory",
                    data: "{'subdivcode':'" + sfCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        rDtls = data.d;
                        genReport();
                        // console.log(rDtls.length);
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Product_Sequence.aspx/getProductSequence",
                    data: "{'subdivcode':'" + sfCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        cDtls = data.d;
                        genReport();
                        //console.log(data.d);
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
            }
            loadData();

            $(document).on('keypress', 'input[name="txtsequance"]', function (event) {
                var keycode = event.which;
                if (!(event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {
                    event.preventDefault();
                }
            });




            $(document).on('blur', 'input[name="txtsequence"]', function () {



                var cVal = $(this).val();
                var oVal = $(this).attr('oldVal');
                var cls = '.' + $(this).attr('class');
                var cl = $(this).attr('class');
                $(this).removeClass(cl);

                console.log($('[id*=cTable]').find(cls).length);

                if (Number(cVal || 0) > 0) {

                    $('[id*=cTable]').find(cls).each(function () {
                        var cV = $(this).val();
                        if (Number(cV) >= Number(cVal) && Number(cV) < Number(oVal)) {
                            $(this).val(Number(cV) + 1);
                            $(this).attr('oldVal', Number(cV) + 1);

                        }
                        else if (Number(cV) <= Number(cVal) && Number(cV) > Number(oVal)) {
                            $(this).val(Number(cV) - 1);
                            $(this).attr('oldVal', Number(cV) - 1);

                        }
                    });
                    $(this).val(cVal);
                    $(this).attr('oldVal', cVal);


                    //                    if (cVal > oVal) {
                    //                        $('[id*=cTable]').find(cls).each(function () {
                    //                            //if (Number($(this).attr('oldVal')) == cVal) {
                    //                            $(this).attr('oldVal', oVal);
                    //                            $(this).val(oVal);
                    //                            //}
                    //                        });
                    //                        $(this).attr('oldVal', cVal);
                    //                    }
                    //                    else {
                    //                        $('[id*=cTable]').find(cls).each(function () {
                    //                            idx = $(this).index;
                    //                            //if (Number($(this).attr('oldVal')) == cVal) {
                    //                            $(this).attr('oldVal', $(this).val()  - idx);
                    //                            $(this).val($(this).val() - idx);
                    //                            //}
                    //                        });
                    //                        $(this).attr('oldVal', cVal);
                    //                    }
                }
                else {

                    $(this).val(oVal);
                }
                $(this).addClass(cl);
            });

            $(document).on('click', '#btnSave', function (e) {
                var arr = [];
                $('[id*=cTable] tbody .rtclass').each(function () {
                    arr.push({
                        pCode: $(this).find('input[name="pCode"]').val(),
                        sequenceNo: $(this).find('input[name="txtsequence"]').val(),
                        printsequence: $(this).find('input[name="txtprintsequence"]').val()

                    });
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Product_Sequence.aspx/SaveSequence",
                    data: "{'Data':'" + JSON.stringify(arr) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        loadData();
                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
            });
            $(document).on('click', '#btnExcel', function (e) {


                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                var a = document.createElement('a');
                var blob = new Blob([$('div[id$=excelDiv]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'Product Sequence.xls';
                a.click();
                e.preventDefault();

            });

            $(document).on('click', '#btnPrint', function (e) {
                var css = '<link href="../css/style.css" rel="stylesheet" />';
                var printContent = document.getElementById("excelDiv");
                var WinPrint = window.open('', '', 'width=900,height=650');
                WinPrint.document.write(printContent.innerHTML);
                WinPrint.document.head.innerHTML = css;
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                WinPrint.close();




            });

        });

        function monthDiff(d1, d2) {
            var months;
            months = (d2.getFullYear() - d1.getFullYear()) * 12;
            months -= d1.getMonth();
            months += d2.getMonth();
            return months <= 0 ? 0 : months;
        }


    </script>
    <style type="text/css">
        @media print
        {
            thead
            {
                display: table-header-group;
            }
        
            tfoot
            {
                display: table-footer-group;
            }
        }
        .odd
        {
            background-color: #b1adae;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div class="container" style="max-width: 100%; width: 100%; text-align: right; padding-right: 50px">
        <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" />
        <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px; display: none"
            class="btn btnPdf" />
        <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
        <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
            class="btn btnClose"></a>
    </div>
    <div id="excelDiv" class="container" style="max-width: 100%; width: 70%">
        <div style="text-align: left; padding: 2px 50px; font-size: large"> <b>            
                <asp:Label ID="lblyear" runat="server"></asp:Label></b>
        </div>
        <div style="text-align: left; padding: 2px 50px;">
            <b>
                <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                <asp:HiddenField ID="subdiv_code" runat="server" />
                <asp:HiddenField ID="hFYear" runat="server" />
                <asp:HiddenField ID="hTYear" runat="server" />
                <asp:HiddenField ID="hFMonth" runat="server" />
                <asp:HiddenField ID="hTMonth" runat="server" />
            </b>
        </div>
        <div>
        </div>
        <div style="width: 100%; margin: 0px auto;">
            <table id="cTable" class="newStly" border="1" style="border-collapse: collapse;">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <br />
        <div style="width: 100%; margin: 0px auto; text-align: center">
            <a name="btnSave" id="btnSave" type="button" class="btn btn-primary ">Save </a>
        </div>
    </div>
    <br />
    </form>
</body>
</html>
