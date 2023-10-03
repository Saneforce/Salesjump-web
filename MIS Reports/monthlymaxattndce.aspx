<%@ Page Language="C#" AutoEventWireup="true" CodeFile="monthlymaxattndce.aspx.cs" Inherits="MIS_Reports_monthlymaxattndce" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monthly Maximised</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
        }

        th {
            position: sticky;
            top: 0;
            background: #6c7ae0;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        .a {
            line-height: 22px;
            padding: 3px 4px;
            border-radius: 7px;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }
    </style>
    <script type="text/javascript">
        function RefreshParent() {
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(document).on('click', "#btnExcel", function (e) {
            var a = document.createElement('a');
            var fileName = 'Test file.xls';
            var blob = new Blob([$('div[id$=pnlContents]').html()], {
                type: "application/csv;charset=utf-8;"
            });
            document.body.appendChild(a);
            a.href = URL.createObjectURL(blob);
            a.download = 'AttendanceReport.xls';
            a.click();
            e.preventDefault();


        });
    </script>
    <script type="text/javascript">
        var calls = ["WT", "value", "TC", "EC","NC", "Sku"];
        var dates = []; var datewise = []; var prods = []; var proddtls = [];
		var newdts='';
        $(document).ready(function () {
            var tcData = [];

            var Fyear = $("#<%=hdnYear.ClientID%>").val();
            var FMonth = $("#<%=hdnMonth.ClientID%>").val();
            var hdnDiv_Code = $("#<%=hdnDiv.ClientID%>").val();
            var hty = $("#<%=htype.ClientID%>").val();
            filldetail();
            filldate();
            fillproduct();
            filldata();
        });
        function filldetail() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "monthlymaxattndce.aspx/Filldetail",
                dataType: "json",
                success: function (data) {
                    datewise = JSON.parse(data.d);

                }
            });
        }
        function filldate() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "monthlymaxattndce.aspx/Filldate",
                dataType: "json",
                success: function (data) {
                    dates = JSON.parse(data.d);
					newdts=data.d;
                }
            });
        }
        function fillproduct() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "monthlymaxattndce.aspx/Fillprod",
                dataType: "json",
                success: function (data) {
                    proddtls = JSON.parse(data.d);
                }
            });
        }
        function filldata() {
            $('#d1').html('<div style="padding-top: 55px;">Loading...</div>');
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "monthlymaxattndce.aspx/Filluser",
                async: false,
                dataType: "json",
                success: function (data) {
                    $('#d1').html('');
                    $('#grid tbody').html('');
                    prods = JSON.parse(data.d);

                    var str = '';
                    var str1 = '';
                    var sth = '';

                    var totapb = [];
                    sth += "<tr><th>SF Code</th><th>SF Name</th><th>Desig</th><th>HQ</th><th>Employee_Id</th><th>Calls</th>";


                    for (var i = 0; i < dates.length; i++) {
                        sth += "<th>" + dates[i].ExpDt + "</th>";
                    }
                    sth += "<th>Total</th>";
                    sth += "</tr>";

                    $('#grid thead').append(sth);

                    var hstr3 = '<tr>';
                    hstr3 += '<td colspan=6 rowspan=2>Total Value</td></tr>';
                    for (var i = 0; i < prods.length; i++) {
						dates=JSON.parse(newdts);
                        str = "<tr ><td rowspan='7'>" + prods[i].SF_Code + "</td><td rowspan='7'>" + prods[i].SF_Name + "</td><td rowspan='7'>" + prods[i].Desig + "</td><td rowspan='7'>" + prods[i].HQ + "</td><td rowspan='7'>" + prods[i].Employee_Id + "</td></tr>";
                        $('#grid tbody').append(str);
                        for (let j = 0; j < calls.length; j++) {
                            var vr = 0

                            str1 = '<tr><td>' + calls[j] + '</td>';
                            for (let k = 0; k < dates.length; k++) {
                                var filtarr = datewise.filter(function (a) {
                                    return a.dtt == dates[k].ExpDt && a.Sf_Code == prods[i].SF_Code;
                                });
                                var filtarrpr = proddtls.filter(function (b) {
                                    return b.Order_Date == dates[k].ExpDt && b.Sf_Code == prods[i].SF_Code;
                                });

                                
                                if (calls[j] == 'Sku') {
                                    if (filtarrpr.length > 0) {
                                        str1 += '<td>' + ((filtarrpr[0].Sku) != undefined ? filtarrpr[0].Sku : 0) + '</td>';
                                        vr = vr + Number((filtarrpr[0].Sku) != undefined ? filtarrpr[0].Sku : 0);
                                    }
                                    else {
                                        vr = vr + 0;
                                        str1 += '<td></td>';
                                    }
                                } else if (calls[j] == 'NC') {
                                    str1 += '<td>' + ((dates[k]['NC'] != undefined) ? dates[k]['NC'] : 0) + '</td>';
                                  //  ter = ter + Number((dates[k]['NC'] != undefined) ? dates[k]['NC'] : 0);
                                }
                                else {
                                    if (filtarr.length > 0) {
                                        str1 += '<td>' + filtarr[0][calls[j]] + '</td>';
                                        if (calls[j] == 'value') {
                                            dates[k]['val'] = (dates[k]['val'] != undefined) ? Number(dates[k]['val']) + Number(filtarr[0].value) : 0 + Number(filtarr[0].value);
                                        }

                                        dates[k]['NC'] = (Number((filtarr[0].TC) != undefined ? filtarr[0].TC : 0)) - (Number((filtarr[0].EC) != undefined ? filtarr[0].EC : 0));

                                        if (isNaN(Number(filtarr[0][calls[j]])))
                                            vr = vr + 0;
                                        else
                                            vr = vr + Number(filtarr[0][calls[j]]);
                                    }
                                    else {
                                        str1 += '<td></td>';
                                    }
                                  
                                }
                               
                            }
                          
                            str1 += '<td>' + vr.toFixed(2) + '</td>';
                            str1 += '</tr>';
                            $('#grid tbody').append(str1);
                        }
                    }
                    for (var k = 0; k < dates.length; k++) {
                        hstr3 += '<td>' + ((dates[k]['val'] != undefined) ? dates[k]['val'].toFixed(2) : 0) + '</td>';
                    }
                    hstr3 += '<td></td>';
                    hstr3 += '</tr>';

                    $('#grid tfoot').append(hstr3);

                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hdnYear" runat="server" />
            <asp:HiddenField ID="hdnMonth" runat="server" />
            <asp:HiddenField ID="hdnDiv" runat="server" />
            <asp:HiddenField ID="htype" runat="server" />
            <br />
        </div>
        <asp:Panel ID="pnlbutton" runat="server">
            <div class="container" style="max-width: 100%; width: 90%">
                <div class="row" style="text-align: right">
                    <asp:Label ID="lbltot" runat="server" />

                    <asp:LinkButton ID="btnExcel" runat="server" class="btn btnExcel" />
                    <asp:LinkButton ID="btnClose" runat="server" href="javascript:window.open('','_self').close();"
                        class="btn btnClose" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%" Style="margin-left: 35px">
            <div class="container" style="max-width: 100%; width: 90%">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblHead" SkinID="lblMand" Style="font-weight: bold; font-size: 16pt; color: black; font-family: Arial; float: left; padding: 5px;"
                            runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true" Font-Underline="true"
                            ForeColor="#476eec" runat="server"></asp:Label>
                        <asp:Label ID="lblsf_name" runat="server" Font-Underline="true" SkinID="lblMand"></asp:Label>
                        <asp:Image ID="logoo" runat="server" Style="width: 28%; border-width: 0px; height: 39px; float: right"></asp:Image>
                    </div>
                </div>
            </div>
            <div class="container" style="max-width: 100%; width: 90%">
                <div id="d1"></div>
                <table class="auto-index" id="grid">
                    <thead>
                    </thead>
                    <tbody></tbody>
                    <tfoot></tfoot>
                </table>

            </div>
        </asp:Panel>
        <asp:Label ID="lbl12" runat="server"></asp:Label>
    </form>
</body>
</html>
