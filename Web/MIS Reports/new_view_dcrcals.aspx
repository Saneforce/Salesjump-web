﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_view_dcrcals.aspx.cs" Inherits="MIS_Reports_new_view_dcrcals" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>viewdcrcalls</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        #grid1 {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        #grid2 {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        th {
            top: 0;
            background: #177a9e;
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
</head>
<body>
    <form id="form1" runat="server">
    
        <div style="text-align: left; padding: 2px 50px;">
            <b><asp:Label ID="lblHead" Text="" SkinID="lblMand" Font-Bold="true" style="font-size:large"
           runat="server" />   <br />
                <span style="font-family: Verdana">Field Force Name :</span>
                <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                <asp:HiddenField ID="hidn_sf_code" runat="server" />
                <asp:HiddenField ID="divcd" runat="server" />
                <asp:HiddenField ID="fdat" runat="server" />
                <asp:HiddenField ID="tdat" runat="server" />
            </b>
        </div>
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport"><br /> 
			<div id="tex"></div>
            <div id="content">
                <table id="OrderList" class="newStly" style="border-collapse: collapse;">
                    <thead></thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <script type="text/javascript">
        var sfusers = []; var chnl = []; var call = []; var chnlvl = []; var clvl = [];
        var i = 0;

        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "new_view_dcrcals.aspx/getSFdets",
                dataType: "json",
                success: function (data) {
                    sfusers = JSON.parse(data.d) || [];
                    if (sfusers.length == 0) {
                        $('#tex').append('<p style="color: red;">NO RECORDS FOUND...</p>');
                    }
                    else {
                        ReloadTable();
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getchnlval() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "new_view_dcrcals.aspx/getchnlval",
                dataType: "json",
                success: function (data) {
                    chnlvl = JSON.parse(data.d) || [];

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getcallval() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "new_view_dcrcals.aspx/getcallval",
                dataType: "json",
                success: function (data) {
                    clvl = JSON.parse(data.d) || [];

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getchnl() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "new_view_dcrcals.aspx/getchnl",
                dataType: "json",
                success: function (data) {
                    chnl = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getcall() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "new_view_dcrcals.aspx/getcall",
                dataType: "json",
                success: function (data) {
                    call = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loadaddrs($tr) {
            var StartLat = parseFloat($($tr).find('.loginaddr').attr('lat'));
            var StartLong = parseFloat($($tr).find('.loginaddr').attr('long'));

            var EndLat = parseFloat($($tr).find('.logoutaddr').attr('lat'));
            var EndLong = parseFloat($($tr).find('.logoutaddr').attr('ling'));
            var addrs = '';
            var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + StartLong + '&lat=' + StartLat + "";
            $.ajax({
                url: url,
                async: false,
                dataType: 'json',
                success: function (data) {
                    addrs = data.display_name;
                }
            });
            $($tr).find('.loginaddr').html(addrs);
            url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + EndLong + '&lat=' + EndLat + "";
            $.ajax({
                url: url,
                async: false,
                dataType: 'json',
                success: function (data) {
                    addrs = data.display_name;
                }
            });
            $($tr).find('.logoutaddr').html(addrs);
            i++;
            if (i < $('#OrderList tbody tr').length) {
                setTimeout(function () { loadaddrs($('#OrderList tbody tr')[i]) }, 10);
            }
        }

        function ReloadTable() {
            $('#OrderList tbody').html('');
            $('#OrderList').show();
            var str = ''; var sth = ''; var stf = ''; var totch = 0; let selucsr = [];
            var totarr = [];
            sth += "<tr><th rowspan='2'>S.NO</th><th rowspan='2'>Activity Date</th><th rowspan='2'>Field Force Name</th>";
            sth += "<th rowspan='2'> State</th ><th rowspan='2'>Reporting Manager</th><th rowspan='2'>Working With/Joint Work</th><th rowspan='2'>Work Type</th>";
            sth += "<th rowspan='2'>Remarks</th><th rowspan='2'>Distributor Name/SS</th><th rowspan='2'>Route</th>";
            sth += "<th rowspan='2'>Login Time</th><th rowspan='2'>Login Address</th><th rowspan='2'>Logout Time</th><th rowspan='2'>Logout Address</th>";
            sth += "<th colspan=" + chnl.length + ">Channel Name</th><th colspan=" + call.length + ">Calls Summary</th></tr>";
            for (var c = 0; c < chnl.length; c++) {
                sth += "<th>" + chnl[c].ChannelName + "</th>";
            }
            for (var l = 0; l < call.length; l++) {
                sth += "<th>" + call[l].CallslName + "</th>";
            }

            let mymap = new Map();
            selucsr = sfusers.filter(function (el) {
                const val = mymap.get(el.Sf_Code + '_' + el.DCR_Date);
                if (val) {
                    return false;
                }
                else {
                    mymap.set(el.Sf_Code + '_' + el.DCR_Date, el.Sf_Code);
                    return true;
                }
             }).map(function (a) { return a; });

            console.log(selucsr);

            console.log(sfusers);
            for (var j = 0; j < selucsr.length; j++) {

                let sfdtl = sfusers.filter(function (a) {
                    return a.Sf_Code == selucsr[j].Sf_Code && a.DCR_Date == selucsr[j].DCR_Date;
                });


                let dmap = new Map();
                let ww = [];
                let rmr = [];
                let Dt = [];
                let rt = [];
                let workt = [];


                ww = sfdtl.filter(function (el) {
                    const val = dmap.get(el.worked_with_name);
                    if (val) {
                        return false;
                    }
                    else {
                        dmap.set(el.worked_with_name, el.worked_with_name);
                        return true;
                    }
                }).map(function (a) { return a; }).sort();


                workt = sfdtl.filter(function (el) {
                    const val = dmap.get(el.Wtype);
                    if (val) {
                        return false;
                    }
                    dmap.set(el.Wtype, el.Wtype);
                    return true;
                }).map(function (a) { return a; }).sort();


                rmr = sfdtl.filter(function (el) {
                    const val = dmap.get(el.remarks);
                    if (val) {
                        return false;
                    }
                    else {
                        dmap.set(el.remarks, el.remarks);
                        return true;
                    }
                }).map(function (a) { return a; }).sort();

                rt = sfdtl.filter(function (el) {
                    const val = dmap.get(el.ClstrName);
                    if (val) {
                        return false;
                    }
                    else {
                        dmap.set(el.ClstrName, el.ClstrName);
                        return true;
                    }
                }).map(function (a) { return a; }).sort();

                Dt = sfdtl.filter(function (el) {
                    const val = dmap.get(el.dist_name);
                    if (val) {
                        return false;
                    }
                    else {
                        dmap.set(el.dist_name, el.dist_name);
                        return true;
                    }
                }).map(function (a) { return a; }).sort();

                let jointwrk = ww.map(function (a) {
                    return a.worked_with_name
                }).join(',');

                let Wtype = workt.map(function (a) {
                    return a.Wtype
                }).join(',');
                
                let rmrks = rmr.map(function (a) {
                    return a.remarks
                }).join(',');

                let dist_name = Dt.map(function (a) {
                    return a.dist_name
                }).join(',');

                let ClstrName = rt.map(function (a) {
                    return a.ClstrName
                }).join(',');

                str += '<tr><td>' + (j + 1) + '</td><td>' + selucsr[j].DCR_Date + '</td><td>' + selucsr[j].Sf_Name + '</td>';
                str += ' <td>' + selucsr[j].StateName + '</td><td>' + selucsr[j].mgr + '</td>';
                str += ' <td>' + jointwrk + '</td><td>' + Wtype + '</td><td>' + rmrks + '</td><td>' + dist_name + '</td>';
                str += ' <td>' + ClstrName + '</td><td>' + selucsr[j].ST + '</td> ';
                str += '<td class="loginaddr" lat="' + selucsr[j].Start_Lat + '" long="' + selucsr[j].Start_Long + '"></td>';
                str += '<td>' + selucsr[j].ET + '</td><td class="logoutaddr" lat="' + selucsr[j].End_Lat + '" long="' + selucsr[j].End_Long + '"></td>';

                let ar = 0;
                for (var k = 0; k < chnl.length; k++) {
                    ch = chnlvl.filter(function (a) {
                        return a.Sf_Code == sfusers[j].Sf_Code && a.DCR_Date == sfusers[j].DCR_Date && a.ChannelId == chnl[k].ChannelId;
                    });

                    str += "<td>" + (ch.length > 0 ? ch[0].ValuesC : '') + "</td>";
                    totch += ch.length > 0 ? ch[0].ValuesC : 0;
                    totarr[ar] = ((ch[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + ch[0].ValuesC) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                    ar++;
                }
                for (var n = 0; n < call.length; n++) {
                    cl = clvl.filter(function (a) {
                        return a.Sf_Code == sfusers[j].Sf_Code && a.DCR_Date == sfusers[j].DCR_Date && a.CallsId == call[n].CallsId;
                    });
                    str += "<td>" + (cl.length > 0 ? cl[0].ValuesC : '') + "</td>";
                    totarr[ar] = ((cl[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + cl[0].ValuesC) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                    ar++;
                }
            }
            sth += "</tr>";
            str += "</tr>";
            stf += "<tr><td colspan=14 style='text-align: center;font-weight: bold'>Total  Value</td>";
            for (var i = 0; i < totarr.length; i++) {
                stf += '<td>' + totarr[i] + '</td>';
            }
            $('#OrderList thead').append(sth);
            $('#OrderList tbody').append(str);
            $('#OrderList tfoot').append(stf);

        }




        //function ReloadTable() {
        //    $('#OrderList tbody').html('');
        //    $('#OrderList').show();
        //    var str = ''; var sth = ''; var stf = ''; var totch = 0; let selucsr = [];
        //    var totarr = [];
        //    sth += "<tr><th rowspan='2'>S.NO</th><th rowspan='2'>Activity Date</th><th rowspan='2'>Field Force Name</th><th rowspan='2'>STATE</th><th rowspan='2'>REPORTING MGR</th><th rowspan='2'>WORKING WITH</th><th rowspan='2'>REMARKS</th><th rowspan='2'>Distributor Name/SS</th><th rowspan='2'>Route</th><th rowspan='2'>Login Time</th><th rowspan='2'>Login Address</th><th rowspan='2'>Logout Time</th><th rowspan='2'>Logout Address</th>" +
        //        "<th colspan=" + chnl.length + ">Channel Name</th><th colspan=" + call.length + ">Calls Summary</th></tr>";
        //    for (var c = 0; c < chnl.length; c++) {
        //        sth += "<th>" + chnl[c].ChannelName + "</th>";
        //    }
        //    for (var l = 0; l < call.length; l++) {
        //        sth += "<th>" + call[l].CallslName + "</th>";
        //    }

        //    let mymap = new Map();
        //     selucsr = sfusers.filter(function (el) {
        //        const val = mymap.get(el.Sf_Code + '_' + el.DCR_Date);
        //        if (val) {
        //            return false;
        //        }
        //        mymap.set(el.Sf_Code + '_' + el.DCR_Date, el.Sf_Code);
        //        return true;
        //    }).map(function (a) { return a; });
        //    console.log(selucsr);
        //    for (var j = 0; j < selucsr.length; j++) {

        //        let sfdtl = sfusers.filter(function (a) {
        //            return a.Sf_Code == selucsr[j].Sf_Code && a.DCR_Date == selucsr[j].DCR_Date;
        //        });
        //        let dmap = new Map();
        //        let ww = [];
        //        let rmr = [];
        //        let Dt = [];
        //        let rt = []; 
        //        ww = sfdtl.filter(function (el) {
        //            const val = dmap.get(el.worked_with_name);
        //            if (val) {
        //                return false;
        //            }
        //            dmap.set(el.worked_with_name, el.worked_with_name);
        //            return true;
        //        }).map(function (a) { return a; }).sort();
        //        rmr = sfdtl.filter(function (el) {
        //            const val = dmap.get(el.remarks);
        //            if (val) {
        //                return false;
        //            }
        //            dmap.set(el.remarks, el.remarks);
        //            return true;
        //        }).map(function (a) { return a; }).sort();
        //        rt = sfdtl.filter(function (el) {
        //            const val = dmap.get(el.ClstrName);
        //            if (val) {
        //                return false;
        //            }
        //            dmap.set(el.ClstrName, el.ClstrName);
        //            return true;
        //        }).map(function (a) { return a; }).sort();
        //        Dt = sfdtl.filter(function (el) {
        //            const val = dmap.get(el.dist_name);
        //            if (val) {
        //                return false;
        //            }
        //            dmap.set(el.dist_name, el.dist_name);
        //            return true;
        //        }).map(function (a) { return a; }).sort();
        //        let jointwrk = ww.map(function (a) {
        //            return a.worked_with_name
        //        }).join(',');

        //        let rmrks = rmr.map(function (a) {
        //            return a.remarks
        //        }).join(',');
        //        let dist_name = Dt.map(function (a) {
        //            return a.dist_name
        //        }).join(',');
        //        let ClstrName = rt.map(function (a) {
        //            return a.ClstrName
        //        }).join(',');

        //        str += '<tr><td>' + (j + 1) + '</td><td>' + selucsr[j].DCR_Date + '</td><td>' + selucsr[j].Sf_Name + '</td><td>' + selucsr[j].StateName + '</td><td>' + selucsr[j].mgr + '</td>' +
        //            '<td>' + jointwrk + '</td><td>' + rmrks + '</td><td>' + dist_name + '</td><td>' + ClstrName + '</td><td>' + selucsr[j].ST + '</td> ' +
        //            '<td class="loginaddr" lat="' + selucsr[j].Start_Lat + '" long="' + selucsr[j].Start_Long + '"></td><td>' + selucsr[j].ET + '</td><td class="logoutaddr" lat="' + selucsr[j].End_Lat + '" long="' + selucsr[j].End_Long + '"></td>';

        //        let ar = 0;
        //        for (var k = 0; k < chnl.length; k++) {
        //            ch = chnlvl.filter(function (a) {
        //                return a.Sf_Code == selucsr[j].Sf_Code && a.DCR_Date == selucsr[j].DCR_Date && a.ChannelId == chnl[k].ChannelId;
        //            });

        //            str += "<td>" + (ch.length > 0 ? ch[0].ValuesC : '') + "</td>";
        //            totch += ch.length > 0 ? ch[0].ValuesC : 0;
        //            totarr[ar] = ((ch[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + ch[0].ValuesC) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
        //            ar++;
        //        }
        //        for (var n = 0; n < call.length; n++) {
        //            cl = clvl.filter(function (a) {
        //                return a.Sf_Code == selucsr[j].Sf_Code && a.DCR_Date == selucsr[j].DCR_Date && a.CallsId == call[n].CallsId;
        //            });
        //            str += "<td>" + (cl.length > 0 ? cl[0].ValuesC : '') + "</td>";
        //            totarr[ar] = ((cl[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + cl[0].ValuesC) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
        //            ar++;
        //        }
        //    }
        //    sth += "</tr>";
        //    str += "</tr>";
        //    stf += "<tr><td colspan=13 style='text-align: center;font-weight: bold'>Total  Value</td>";
        //    for (var i = 0; i < totarr.length; i++) {
        //        stf += '<td>' + totarr[i] + '</td>';
        //    }
        //    $('#OrderList thead').append(sth);
        //    $('#OrderList tbody').append(str);
        //    $('#OrderList tfoot').append(stf);

        //}
        $(document).ready(function () {

            getchnl();
            getcall();
            getchnlval();
            getcallval();
            getUsers();
            setTimeout(function () { loadaddrs($('#OrderList tbody tr')[0]) }, 10);
        });

        $('#btnExport').click(function () {

            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("OrderList").innerHTML;


            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'channelDashboard' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
    </script>

</body>
</html>