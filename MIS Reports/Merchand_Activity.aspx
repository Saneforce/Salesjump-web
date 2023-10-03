<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Merchand_Activity.aspx.cs" Inherits="MIS_Reports_Merchand_Activity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var slno = $("#<%=Sl_No.ClientID%>").val();
            var rtcd = $("#<%=RtCode.ClientID%>").val();
            fillActivity(rtcd,slno);
        });
        function fillActivity(route_C,slno) {
            $('#fielddets tbody').html('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Merchand_Activity.aspx/Act_Count",
                data: "{'rtcode':'" + route_C + "','Sl_No':'" + slno + "'}",
                dataType: "json",
                success: function (data) {
                    det = JSON.parse(data.d) || [];
                    if (det.length > 0) {
                        var slno = 0, str;
                        $('#fielddets TBODY').html("");
                        for (var i = 0; i < det.length; i++) {
                            slno = i + 1;
                            str += "<tr><td>" + slno + "</td><td>" + det[i].POP_Name + "</td><td>" + det[i].Qty + "</td></tr>";
                        }
                        $('#fielddets TBODY').append(str);
                    }
                    else {
                        //document.getElementById('ndf').innerHTML = "No Data Found for View...";
			$('#fielddets TBODY').html("");
                        str += "<tr><td>No Data Found for View...</td></tr>";
                        $('#fielddets TBODY').append(str);
                    }
                }
            });
        }
    </script>
     <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 100%;
            overflow: scroll;
        }

        th {
            position: sticky;
            top: 0;
            background-color: #496a9a;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        #grid td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="Sl_No" runat="server" />
    <asp:HiddenField ID="RtCode" runat="server" />
        <div>
            <table width="100%">
                <tr>
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead" Text="" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                    </tr>
                     </table>
            <div class="row m-0">
                    <div class="table-responsive col-10" style="max-width: 800px; max-height: 1000px; margin: auto;">
                        <table id="fielddets" class="table table-bordered table-hover grids">
                            </br>
                            </br>
                            <thead>
                                <tr>
                                    <th style="text-align: left">S.NO</th>
                                            <th style="text-align: left">POP Name</th>
                                            <th style="text-align: left">POP Qty</th>
                                </tr>
                            </thead>
                            <tbody>
                                 <tr id="ndf" align="center" valign="middle" style="color:Black;background-color:AliceBlue;border-color:Black;border-width:2px;border-style:Solid;font-weight:bold;height:5px;">
                                    <td></td>
                                </tr>
                            </tbody>
                            <tfoot></tfoot>
                        </table>
                    </div>
                </div>
        </div>
    </form>
</body>
</html>
