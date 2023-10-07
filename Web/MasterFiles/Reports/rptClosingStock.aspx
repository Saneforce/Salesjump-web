<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptClosingStock.aspx.cs" Inherits="MasterFiles_Reports_rptClosingStock" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Stock </title>
    <link href="../../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
	<script type="text/javascript" src="../../js/xlsx.full.min.js"></script>
    <script type="text/javascript">
		var prod = [];
        function fnExcelReport() {

             var droptblname = 'Closing Stock Report';
            var ws = XLSX.utils.json_to_sheet(prod);
            var wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, droptblname);
            XLSX.writeFile(wb, "Closing Stock Report.xlsx");
        }
        function closew() {
            $('#cphoto1').css("display", 'none');
        }  
        jQuery(document).ready(function ($) {

            var sfcode = $("#<%=hsfcode.ClientID%>").val();
           // var fyear = $("#<%=hfyear.ClientID%>").val();
           // var fmonth = $("#<%=hfmonth.ClientID%>").val();
            var fdt = $("#<%=hfyear.ClientID%>").val();
            var tdt = $("#<%=hfmonth.ClientID%>").val();
            var subdiv = $("#<%=hsubdiv.ClientID%>").val();
            dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
            $(document).on('click', '.picc', function () {
                // alert("hi");
                var photo = $(this).attr("src");
                $('#photo1').attr("src", $(this).attr("src"));
                $('#cphoto1').css("display", 'block');
                // $(this).append('<div style="width: 100%" ><img src="' + photo + '"/></div>'

            });
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptClosingStock.aspx/GetAllData",
                data: "{'SF_Code':'" + sfcode + "','fdate':'" + fdt + "','tdate':'" + tdt + "','SubDiv':'" + subdiv + "' }",
                dataType: "json",
                success: function (data) {
                    prod = JSON.parse(data.d);



                    ////var Dist = CDtls.reduce((acc, current) => {
                    ////    var x = acc.find(item => item.distCode === current.distCode);
                    ////    if (!x) {
                    ////        return acc.concat([current]);
                    ////    }
                    ////    else {
                    ////        return acc;
                    ////    }
                    ////},
                    ////    []);

                    ////var prod = CDtls.reduce((acc, current) => {
                    ////    var x = acc.find(item => item.pCode === current.pCode);
                    ////    if (!x) {
                    ////        return acc.concat([current]);                        
                    ////    }
                    ////    else {
                    ////        return acc;
                    ////    }
                    ////},
                    ////    []);


                   if (prod.length > 0) {
                        var tbl = $('#tblFF');
                        var str = '<th>Submitted Date</th><th>Submitted Time </th><th>State</th><th>H.Q</th><th>Field Force</th><th>Distributor Code</th><th>Distributor ERP Code</th><th>Distributor Name</th><th>Product Name </th><th class="hide">Case</th><th class="hide">Piece</th><th>Total Units</th><th style="height: 5%;width: 2%;">Pictures</th>';
                        //var str = '<th>Submitted Date</th><th>State</th><th>H.Q</th><th>Field Force</th><th>Distributor Code</th><th>Distributor Name</th><th>Product Name </th><th>Case</th><th>Piece</th><th>Total Units</th>';
                        // var stk = '';
                        //for (var i = 0; i < Dist.length; i++) {
                        //    str += '<th colspan="3" >' + Dist[i].distName + '</th>';
                        //    stk += '<th>Case</th><th>Piece</th><th>Total Units</th>';
                        //}
                        $(tbl).append('<tr>' + str + '</tr>');
                        //$(tbl).append('<tr>' + stk + '</tr>');

                        for (var j = 0; j < prod.length; j++) {
                            $imgtg = '<img  class="picc" style="height:50px;" src=\"http://fmcg.sanfmcg.com/photos/' + prod[j].Imgurl + '\" />';
                            //str = '<td>' + prod[j].Pln_Date + '</td><td>' + prod[j].StateName + '</td><td>' + prod[j].Sf_HQ + '</td><td>' + prod[j].SF_Name + '</td><td>' + prod[j].St_code + '</td><td>' + prod[j].Stockist_Name + '</td><td>' + prod[j].pName + '</td><td>' + prod[j].cQTY + '</td><td>' + prod[j].pQTY + '</td><td>' + prod[j].TPieces + '</td>';
                            str = '<td>' + prod[j].SubmittedDate + '</td><td>' + prod[j].SubmittedTime + '</td><td>' + prod[j].State + '</td><td>' + prod[j].HQ + '</td><td>' + prod[j].FieldForce + '</td><td>' + prod[j].DistributorCode + '</td><td>' + prod[j].Erp_Code + '</td><td>' + prod[j].DistributorName + '</td><td>' + prod[j].ProductName + '</td><td class="hide">' + prod[j].CaseQty + '</td><td class="hide">' + prod[j].PieceQty + '</td><td>' + prod[j].TotalUnits + '</td><td>' + ((prod[j].Imgurl != '') ? $imgtg : '') + '</td>';

                            //for (var i = 0; i < Dist.length; i++) {
                            //    var cQ = '';
                            //    var pQ = '';
                            //    var Dts = '';
                            //    fL = CDtls.filter(function (obj) {
                            //        return (obj.pCode == prod[j].pCode && obj.distCode == Dist[i].distCode);
                            //    });
                            //    if (fL.length > 0) {
                            //cQ = fL[0].cQTY;
                            //pQ = fL[0].pQTY;
                            //Dts = fL[0].TPieces;
                            //}
                            // str += '<td>' + prod[j].cQTY + '</td><td>' + prod[j].pQTY + '</td><td>' + prod[j].TPieces + '</td>';
                            //}
                            $(tbl).append('<tr>' + str + '</tr>');

                        }
                    }
                },
                error: function (rs) {
                    alert(JSON.stringify(rs));
                }
            });
			if('<%=Session["div_code"]%>'=='100'){
				$('.hide').hide();
			}
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="max-width: 100%; width: 95%; text-align: right"> 
         <%--<asp:Label ID="lblMon" runat="server"  Font-Size="Large"></asp:Label>
        <asp:DropDownList ID="ddlMonth" CssClass="ddlMonth" runat="server" SkinID="ddlRequired" Width="100">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                        </asp:DropDownList>--%>
        <a class="btn btnExcel" onclick="fnExcelReport()"></a><a class="btn btnClose" href="javascript:window.open('','_self').close();">
        </a>
    </div>
        
    <div id="excelDiv" class="container" style="max-width: 100%; width: 95%;">
        <asp:HiddenField ID="hsfcode" runat="server" />
        <asp:HiddenField ID="hfmonth" runat="server" />
        <asp:HiddenField ID="hfyear" runat="server" />
        <asp:HiddenField ID="hsubdiv" runat="server" />
        <asp:Label ID="lblhead" runat="server" Style="font-size: x-large"></asp:Label>
        <br /><b>
        <asp:Label ID="subhead" runat="server"  Font-Size="Large"></asp:Label></b>

        <table id="tblFF" class="table newStly" style="width: 100%;">
        </table>
    </div>
    </form>
</body>
</html>
