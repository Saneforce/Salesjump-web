<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReasonAnalysisSFView.aspx.cs"
    Inherits="MIS_Reports_ReasonAnalysisSFView" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server" style="overflow-x: auto!important;">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../css/style.css" rel="stylesheet" />
    <title>Reason Analysis</title>
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        #Product_Table tr td
        {
            text-align:right;
        }
        #Product_Table tr td:nth-child(2)
        {
            text-align:left;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#Product_Table tr').remove();

            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=SubDivCode.ClientID%>").val();


            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ReasonAnalysisView.aspx/GetData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "'}",
                dataType: "json",
                success: function (data) {

                    ReasonArray = data.d;

                    if (data.d.length < 1) {
                        $("#noRec").css("display", "");
                    }
                    else {
                        $("#noRec").css("display", "none");
                    }


                    var SF = [];
                    for (var j = 0; j < data.d.length; j++) {
                        SF.push(data.d[j].sfName);
                    }

                    var dSF = SF.filter(function (itm, i, SF) {
                        return i == SF.indexOf(itm);
                    });



                    var result = ReasonArray.reduce(function (memo, e1) {
                        var matches = memo.filter(function (e2) {
                            return e1.sfCode == e2.sfCode
                        })
                        if (matches.length == 0)
                            memo.push(e1)
                        return memo;
                    }, [])




                    var kk = [];
                    for (var k = 0; k < data.d.length; k++) {
                        kk.push(data.d[k].remarks);
                    }

                    var unique = kk.filter(function (itm, i, kk) {
                        return i == kk.indexOf(itm);
                    });

                    str = '<th>SlNo.</th><th  style="min-width:250px;">Field Force</th>';

                    for (var i = 0; i < unique.length; i++) {
                        if (unique[i].length < 1) {
                            str += '<th style="min-width:150px" >Not specified </th>';
                        }
                        else {
                            str += '<th style="min-width:150px" >' + unique[i] + ' </th>';
                        }

                    }
                    $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th>TOTAL</th></tr>');


                    for (var i = 0; i < result.length; i++) {
                        var tot = 0;
                        str = '<td>' + (i + 1) + '</td><td> <input type="hidden" name="hdnSFCode" value="' + result[i].sfCode + '" /> ' + result[i].sfName + '</td>';
                        for (var j = 0; j < unique.length; j++) {
                            var q = "";
                            fP = ReasonArray.filter(function (a) { return (a.sfCode == result[i].sfCode && a.remarks == unique[j]); });
                            if (fP.length > 0) {
                                q = '<a class="AAA" href="ReasonAnalysisRetailerView.aspx?&SFCode=' + result[i].sfCode + '&FYear=' + Fyear + '&FMonth=' + FMonth + '&Remarks=' + fP[0].remarks + '&SFName=' + result[i].sfName + '">' + fP[0].cnt + '</a>';
                                tot += Number(fP[0].cnt);
                            }
                            str += '<td>' + q + '</td>';
                        }
                        $('#Product_Table tbody').append('<tr>' + str + '<td>' + tot + '</td></tr>');
                    }


                },
                error: function (jqXHR, exception) {
                    alert(JSON.stringify(result));
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }
            });


            if ($('#Product_Table  tr').length == 1) {
                console.log($('#Product_Table tr').length);
                $('#<%=Label3.ClientID%>').text('Records Not Found..!');

            }

            $('.mainhead th').each(function (i) {
                var remove = 0;

                var tds = $(this).parents('table').find('tr td:nth-child(' + (i + 1) + ')');
                tds.each(function (j) {
                    if (this.innerHTML == '') remove++;
                });
                if (remove == ($('#Product_Table tbody tr').length)) {
                    $(this).hide();
                    tds.hide();
                }
            });

            $(".AAA").click(function () {
                event.preventDefault();
                window.open($(this).attr("href"), "myWindow", "width=1100,height=600,scrollbars=yes");
            });

            $(document).on('click', "#btnClose", function () {
                window.close();
            });

               $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#content').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'ReasonAnalysis.xls';
                a.click();
                e.preventDefault();
            });

            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });


        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
<div class="container" id="content" style="max-width: 100%; width: 98%; margin: 0px;">
    <div class="row" style="max-width: 100%; width: 98%;margin: 0px;">
        <div class="col-md-8" style="margin: 0px;">
            <asp:Label ID="Label2" runat="server" Style="font-weight: bold; font-size: x-large"></asp:Label>
        </div>
        <div class="col-md-4" style="text-align: right">
            <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                    display: none" href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                        style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                            type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
        </div>
    </div> 
	
    
       <span style=" color:#1000ff; font-weight:bold" >Team Name : </span>  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>.
        <br />
        <table id="Product_Table" border="1" class="newStly" style="border-collapse: collapse;">
            <thead>
            </thead>
            <tbody>
            </tbody>
            <tfoot>
            </tfoot>
        </table>
        <asp:Label ID="Label3" runat="server" Style="font-weight: bold; font-size: x-large;color: red"></asp:Label>
    </div>
    </form>
</body>
</html>
