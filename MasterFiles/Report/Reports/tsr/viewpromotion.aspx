<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewpromotion.aspx.cs" Inherits="MasterFiles_Reports_tsr_viewpromotion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
     #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 100%;
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
            text-align: center;
        }
    </style>
    <script type="text/javascript">

        //$(document).on('click', "#btnExport", function (e) {
        //    var dt = new Date();
        //    var day = dt.getDate();
        //    var month = dt.getMonth() + 1;
        //    var year = dt.getFullYear();
        //    var postfix = day + "_" + month + "_" + year;
        //    //creating a temporary HTML link element (they support setting file names)
        //    var a = document.createElement('a');
        //    //getting data from our div that contains the HTML table
        //    var data_type = 'data:application/vnd.ms-excel';
        //    var table_div = document.getElementById('grid');

         
        //    var table_html = table_div.outerHTML.replace(/ /g, '%20');
                  
        //    table_html = table_html.replace(/<A[^>]*>|<\/A>/g, "");             //remove if u want links in your table

        //    table_html = table_html.replace(/<img[^>]*>/gi, "");                 // remove if u want images in your table

        //    table_html = table_html.replace(/<input[^>]*>|<\/input>/gi, ""); 

        //    var tab_text = '<table  style="border:1.5px solid black">';

        //    var textRange;

        //    var j = 0;

        //    //var tab = document.getElementById('grid'); // id of table

        //    var lines = table_div.rows.length;

        //    // the first headline of the table

        //    if (lines > 0) {

        //        tab_text = tab_text + '<tr bgcolor="#19a4c6">' + table_div.rows[0].innerHTML + '</tr>';

        //    }

        //    // table data lines, loop starting from 1

        //    for (j = 1; j < lines; j++) {

        //        tab_text = tab_text + "<tr>" + table_div.rows[j].innerHTML + "</tr>";

        //    }

        //    tab_text = tab_text + "</table>";

        //    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");             //remove if u want links in your table

        //    tab_text = tab_text.replace(/<img[^>]*>/gi, "");                 // remove if u want images in your table

        //    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); 

        //    console.log(tab_text);


        //    a.href = data_type + ' ' + tab_text;
        //    //setting the file name
        //    a.download = 'DirectingMarketing_' + postfix + '.xls';
        //    //triggering the function
        //    a.click();
        //    //just in case, prevent default behaviour
        //    e.preventDefault();
        //});


        $(function () {
            $('[id*=btnExport]').on('click', function () {



                //alert('Hi');
                ExportToExcel1('grid');
                //location.reload();
            });
        });

       
        function ExportToExcel1(lP_No) {

            var dt = new Date();
            var day = dt.getDate();
            var month = dt.getMonth() + 1;
            var year = dt.getFullYear();
            var postfix = day + "_" + month + "_" + year;


            var tab_text = '<table border="1px">';

            var textRange;

            var j = 0;

            var tab = document.getElementById('grid'); // id of table

            var lines = tab.rows.length;

            // the first headline of the table

            if (lines > 0) {

                tab_text = tab_text + '<tr bgcolor="#19a4c6">' + tab.rows[0].innerHTML + '</tr>';

            }

            // table data lines, loop starting from 1

            for (j = 1; j < lines; j++) {

                tab_text = tab_text + "<tr>" + tab.rows[j].innerHTML + "</tr>";

            }

            tab_text = tab_text + "</table>";

            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");             //remove if u want links in your table

            tab_text = tab_text.replace(/<img[^>]*>/gi, "");                 // remove if u want images in your table

            tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, "");    // reomves input params

            var ua = window.navigator.userAgent;

            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {

                exportIF.document.open("txt/html", "replace");

                exportIF.document.write(tab_text);

                exportIF.document.close();

                exportIF.focus();

                var filename = 'DirectingMarketing';

                sa = exportIF.document.execCommand("SaveAs", true, 'DirectingMarketing_' + postfix + '.xls');                

            }

            else // other browser not tested on IE 11

                //sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

            return (sa);    
        }

        function ExportToExcel(Id) {

            var dt = new Date();
            var day = dt.getDate();
            var month = dt.getMonth() + 1;
            var year = dt.getFullYear();
            var postfix = day + "_" + month + "_" + year;

            var tab_text = "<table border='2px'><tr>";
            var textRange;
            var j = 0;
            tab = document.getElementById(Id);
            var headerRow = $('[id*=grid] tr:first');
            tab_text += headerRow.html() + '</tr><tr>';
            var rows = $('[id*=grid] tr:not(:has(th))');

            var lines = tab.rows.length;



            // the first headline of the table

            if (lines > 0) {

                tab_text = tab_text + '<tr bgcolor="#19a4c6">' + tab.rows[0].innerHTML + '</tr>';

            }

            for (j = 0; j < rows.length; j++) {
                if ($(rows[j]).css('display') != 'none') {
                    tab_text = tab_text + rows[j].innerHTML + "</tr>";
                }
            }
            tab_text = tab_text + "</table>";
            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, ""); //remove if u want links in your table
            tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
            tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                
                sa = txtArea1.document.document.execCommand("SaveAs", true, 'DirectingMarketing_' + postfix + '.xls');
            }
            else {                 //other browser not tested on IE 11

               
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
            }
            return (sa);
        }
    </script>
</head>
    <body>
        <form id="form1" runat="server">
            <asp:HiddenField ID="hidn_sf_code" runat="server" />
            <asp:HiddenField ID="fdate" runat="server" />
            <asp:HiddenField ID="tdate" runat="server" />
            <asp:HiddenField ID="divc" runat="server" />
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="80%">
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                   
                                    <td>
                                        <asp:Button ID="btnExport" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            
                                            />
                                    </td>
                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Brandwise Sales" Style=" margin-left: 10px; font-size: x-large "></asp:Label>

            </div>

        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
             <asp:Label ID="Label3" Text="State Name :" runat="server" Style="font-size: larger"></asp:Label>
             <asp:Label ID="Label4" Text="Zone :" runat="server" Style="font-size: larger"></asp:Label>             
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div>
            <table class="auto-index" id="grid">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Date</th>
						<th>Time</th>
                        <th>State</th>
                        <th>Zone</th>
                        <th>Area</th>
                        <th>User </th>
                        <th>HQ</th>
                        <th>DB</th>
                        <th>Town</th>
                        <th>Outlet / Venue</th>
						<th>Route</th>
						<th>Mode</th>
                        <th style="display:none;" class="hidden">FC Time</th>
                        <th style="display:none;" class="hidden">LC Time</th>
                        <th>Brand</th>
                        <th>SKU</th>
                        <th>Sales</th>
                        <th>Free</th>
                        <th>Image</th>                        
                     </tr>
                 </thead>
                <tbody style="text-align:left;"></tbody>
              
            </table>
        </div>
            <iframe id="exportIF" style="display:none"></iframe>
    </form>
    
    <script type="text/javascript">
        var prods = [];
        $(document).ready(function () {
            promotiondtl();

        });
        function closew() {
            $('#cphoto1').css("display", 'none');
        }
        $(document).ready(function () {
            dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
        });
        $(document).on('click', '.view_image', function () {
            var photo = $(this).attr("src");
            $('#photo1').attr("src", $(this).attr("src"));
            $('#cphoto1').css("display", 'block');
        });
        function promotiondtl() {


            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "viewpromotion.aspx/promotiondtl",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('#grid tbody').html('');
                    prods = JSON.parse(data.d);
                    str = '';
                    for (var i = 0; i < prods.length; i++) {


                      str += "<tr><td>" + (i + 1) + "</td><td style='text-align:left;'>" + prods[i].Date + "</td><td style='text-align:left;'>" + prods[i].Time + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].StateName + "</td><td style='text-align:left;'>" + prods[i].Zone_name + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].Area_name + "</td><td style='text-align:left;'>" + prods[i].Sf_Name + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].Territory_name + "</td><td style='text-align:left;'>" + prods[i].DB + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].Territory + "</td><td style='text-align:left;'>" + prods[i].Venue + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].RouteNm + "</td><td style='text-align:left;'>" + prods[i].Mode + "</td>";
                        str += "<td class='hidden' style='text-align:left;display:none'>" + prods[i].FC + "</td><td class='hidden' style='text-align:left;display:none'>" + prods[i].LC + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].Brand + "</td><td style='text-align:left;'>" + prods[i].SKU + "</td>";
                        str += "<td style='text-align:left;'>" + prods[i].Sales + "</td><td style='text-align:left;'>" + prods[i].free + "</td>";
                        //str += "<td style='text-align:left;'><a href='/MasterFiles/Reports/tsr/viewpromotion_image.aspx?Ekey=" + prods[i].Ekey + "&sfn=" + prods[i].Sf_Name + "'>View</a></td>";
                        str += "<td style='text-align:left;'> <img id='img1' width='140' height='140' src='" + prods[i].ImgUrl + "' /> </td></tr>";
                        //str += "<td style='text-align:left;'>" + prods[i].ImgUrl + "</td><td style='text-align:left;'>" + prods[i].Area_name + "</td><td style='text-align:left;'>" + prods[i].Sf_Name + "</td><td style='text-align:left;'>" + prods[i].HQ_Name + "</td><td style='text-align:left;'>" + prods[i].stockist_name + "</td><td style='text-align:left;'>" + prods[i].MinTime + "</td><td style='text-align:left;'>" + prods[i].MaxTime + "</td><td style='text-align:left;'><a href='/MasterFiles/Reports/tsr/viewpromotion_image.aspx?Ekey=" + prods[i].Ekey + "&sfn=" + prods[i].Sf_Name + "'>View</a></td></tr>";
                    }
                    $('#grid tbody').append(str);
                }
            });
        };
    </script>
</body>
</html>
