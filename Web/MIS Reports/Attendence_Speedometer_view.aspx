<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Attendence_Speedometer_view.aspx.cs" Inherits="MIS_Reports_Attendence_Speedometer_view" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Productivity Calls Data</title>
   <link type="text/css" href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />    
   <link type="text/css" href="../css/style.css" rel="stylesheet" />
    <%--<link type="text/css" rel="Stylesheet" href="../css/Report.css" />--%>
    <%--<link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />--%>
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }
    /*popup image             */

        .phimg img {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

        .phimg img:hover {opacity: 1.0;}

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (Image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 60%;
            height: 80%;
        }

        /* Caption of Modal Image (Image Text) - Same Width as the Image */
        #caption {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
            text-align: center;
            color: #ccc;
            padding: 10px 0;
            height: 150px;
        }

        /* Add Animation - Zoom in the Modal */
        .modal-content, #caption { 
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {-webkit-transform:scale(0)} 
            to {-webkit-transform:scale(1)}
        }

        @keyframes zoom {
            from {transform:scale(0)} 
            to {transform:scale(1)}
        }

        /* The Close Button */
        .close {
            position: absolute;
            top: 15px;
            right: 35px;
            color: #f1f1f1;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

        .close:hover,
        .close:focus {
            color: #bbb;
            text-decoration: none;
            cursor: pointer;
        }

        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px){
            .modal-content {
                width: 100%;
            }
        }	

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        </div>

        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="lblHead" runat="server" Text="" Style="margin-left: 50px; font-size: x-large"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;display:none">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">               
                <table id="OrderList" class="newStly" style="border-collapse: collapse;">
                   <thead></thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                  </table>
            </div>
            <div id="content1">               
                <table id="OrderList1" class="newStly" style="border-collapse: collapse;display:none;">
                   <thead></thead>
                    <tbody></tbody>
                    <tfoot>
                    </tfoot>
                  </table>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
       <div id="myModal" class="modal" style="opacity: 1;">
		    <!-- The Close Button -->
		    <span class="close" style="opacity: 1;">&times;</span>
					
		    <!-- Modal Content (The Image) -->
		    <img class="modal-content" id="img01">
					
		    <!-- Modal Caption (Image Text) -->
		    <div id="caption"></div>
	    </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script>
        sfatt = []; sfImg = []; imgfiltr = [];
        function getsfdata() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Attendence_Speedometer_view.aspx/getSFdata",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    sfatt = JSON.parse(data.d) || [];
                    
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getsfimg() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Attendence_Speedometer_view.aspx/getSFimg",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    sfImg = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $('#OrderList thead').html('');
            $('#OrderList tbody').html('');
            $('#OrderList').show();
            PgRecords = $("#pglim").val();
          
            var startimg = '';
            var Endimg = '';
            var travelledkm = '';
            var startkm = 0;
            var endkm = 0;
            var sth = '';
            var str = '';
            var strexcel = '';
           var base_url = window.location.origin;
           //alert(base_url);
            sth += "<tr><th>Sl.No</th><th >EmpID</th><th >FieldForce Name</th><th >HQ</th><th >State</th><th >Reporting To</th><th>Mode Of Travel</th><th >Start Date & Time</th><th >Start KM</th>";
            sth += "<th >Start KM Image</th><th >End Date & Time</th><th >End KM</th><th >End KM Image</th><th >Travelled Kelometer</th></tr>";

            for (i = 0; i < sfatt.length; i++) {
                if (sfatt[i].day_start_km == "") startkm = 0; else startkm = sfatt[i].day_start_km;
                if (sfatt[i].day_end_km == "") endkm = 0; else endkm = sfatt[i].day_end_km;
                travelledkm = endkm - startkm;
                if (travelledkm < 0) travelledkm = 0;

                if (sfImg.length > 0) {
                    imgfiltr = sfImg.filter(function (a) {
                        return a.Activity_Report_Code == sfatt[i].SF_Code && a.Insert_Date_Time == sfatt[i].login_date;
                    });
                }

                startimgdwnld = ""; Endimgdwnld = ""; startimg = ""; Endimg = "";

                //if (imgfiltr.length < 1) {
                //startimgdwnld = "http://fmcg.salesjump.in/photos/" + imgfiltr[k].Activity_Report_Code + "_" + imgfiltr[k].imgurl;
                if (sfatt[i].SSlfyPath.indexOf(".") > -1) { 
                    startimgdwnld = base_url + "/photos/" + sfatt[i].SSlfyPath;
                    startimg = "<img width=30 height=30 class='phimg' onclick='imgPOP(this)' src='" + startimgdwnld + "'>";
                }
                if (sfatt[i].ESlfyPath.indexOf(".") > -1) {
                    Endimgdwnld = base_url + "/photos/" + sfatt[i].ESlfyPath;
                    Endimg = "<img width=30 height=30 class='phimg' onclick='imgPOP(this)' src='" + Endimgdwnld + "'>";
                }
                //} else {
                    for (k = 0; k < imgfiltr.length; k++) {
                        if (imgfiltr[k].Identification == "Day Start km Pic") {
                            //startimgdwnld = "http://fmcg.salesjump.in/photos/" + imgfiltr[k].Activity_Report_Code + "_" + imgfiltr[k].imgurl;
                            startimgdwnld = base_url + "/photos/" + imgfiltr[k].Activity_Report_Code + "_" + imgfiltr[k].imgurl;
                            startimg = "<img width=30 height=30 class='phimg' onclick='imgPOP(this)' src='" + startimgdwnld + "'>";
                        }
                        if (imgfiltr[k].Identification == "Day End km Pic") {
                            //Endimgdwnld = "http://fmcg.salesjump.in/photos/" + imgfiltr[k].Activity_Report_Code + "_" + imgfiltr[k].imgurl;
                            Endimgdwnld = base_url + "/photos/" + imgfiltr[k].Activity_Report_Code + "_" + imgfiltr[k].imgurl;
                            Endimg = "<img width=30 height=30 class='phimg' onclick='imgPOP(this)' src='" + Endimgdwnld + "'>";
                            //"<img Width='150' Height='100' src=http://fmcg.sanfmcg.com/photos/" + imgfiltr[k].imgurl + ">";
                        }
                    }
               // }
                //Identification
                /*u.sf_emp_id,u.SF_Code,u.SF_Name,u.sf_hq,u.StateName,u.Reporting_To_SF,tp.Start_Time,tp.End_Time,CONVERT(date,tp.login_date)login_date,isnull(tp.day_start_km,0) day_start_km,isnull(tp.day_end_km,0) day_end_km*/
                str += '<tr><td>' + (i + 1) + '</td><td>' + sfatt[i].sf_emp_id + '</td><td>' + sfatt[i].SF_Name + '</td><td>' + sfatt[i].sf_hq + '</td>';
                str += '<td>' + sfatt[i].StateName + '</td><td>' + sfatt[i].Reporting_To_SF + '</td><td>' + sfatt[i].MOT_Name + '</td><td>' + sfatt[i].Start_Time + '</td><td>' + sfatt[i].day_start_km + '</td>';
                str += '<td>' + startimg + '</td><td>' + sfatt[i].End_Time + '</td><td>' + sfatt[i].day_end_km + '</td><td>' +  Endimg + '</td><td>' + travelledkm + '</td></tr>';

                strexcel += '<tr><td>' + (i + 1) + '</td><td>' + sfatt[i].sf_emp_id + '</td><td>' + sfatt[i].SF_Name + '</td><td>' + sfatt[i].sf_hq + '</td>';
                strexcel += '<td>' + sfatt[i].StateName + '</td><td>' + sfatt[i].Reporting_To_SF + '</td><td>' + sfatt[i].Start_Time + '</td><td>' + sfatt[i].day_start_km + '</td>';
                strexcel += '<td><a href="#">' + startimgdwnld + '</a></td><td>' + sfatt[i].End_Time + '</td><td>' + sfatt[i].day_end_km + '</td><td><a href="#">' +  Endimgdwnld + '</a></td><td>' + travelledkm + '</td></tr>';


            }
            $('#OrderList thead').append(sth);
            $('#OrderList tbody').append(str);
            $('#OrderList1 thead').append(sth);
            $('#OrderList1 tbody').append(strexcel);
        }
        $(document).ready(function () {
            //getsfdata();
            ////getsfimg();
            ////ReloadTable();
        $.when(getsfdata(), getsfimg())
            .then(ReloadTable(), function () { console.log('Error Found') });
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
               // htmls = document.getElementById("content").innerHTML;
                htmls = document.getElementById("content1").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'AttendanceSpeedoMeter_Data' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
           
        });
    </script>
    <script type="text/javascript">
			// Get the modal
			var modal = document.getElementById('myModal');
			
			function imgPOP(x){
				// Get the image and insert it inside the modal - use its "alt" text as a caption
				var img = document.getElementById('myImg');
				var modalImg = document.getElementById("img01");
				var captionText = document.getElementById("caption");
				modal.style.display = "block";
				modalImg.src = x.src;
				captionText.innerHTML = x.alt;
			}

			// Get the <span> element that closes the modal
			var span = document.getElementsByClassName("close")[0];

			// When the user clicks on <span> (x), close the modal
			span.onclick = function() { 
			  modal.style.display = "none";
			}
	</script>
</body>
</html>

