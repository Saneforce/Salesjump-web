<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_Event_Captures_View.aspx.cs" Inherits="MasterFiles_Reports_Rpt_DCR_Event_Captures_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Captures Report</title>
  <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />    
   <script type="text/javascript" src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
   <script type="text/javascript" src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>   
   <link type="text/css" href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />    
   <link type="text/css" href="../css/style.css" rel="stylesheet" />    
   <script type="text/javascript" src="../../js/html2canvas.js"></script>
    
    <style type="text/css">
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
        .tbldetail_main
        {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }
        .tbldetail_Data
        {
            height: 18px;
        }

		table#GridView1 th{
			padding:  5px 10px;
			font-size: 15px;
		}
		table#GridView1 td {
			padding: 10px;
			font-size: 15px;
		}
        .Holiday
        {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
        .NoRecord
        {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
		
		/*.phimg:hover:before {
			display: block;
			content: attr(data-url);
			background-image: attr(data-url url);
			background-position: center center;
			background-repeat: no-repeat;
			background-size: contain;
			position: absolute;
			width: 60% !important;
			height: 60% !important;
			left: 0px;
			top: 0px;
		}*/

/*popup image             */

        .phimg img {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

        .phimg img:hover {
            opacity: 0.7;
        }

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
            width: 80%;
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
            from {
                -webkit-transform: scale(0)
            }

            to {
                -webkit-transform: scale(1)
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0)
            }

            to {
                transform: scale(1)
            }
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
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }

    </style>

    <script type="text/javascript">
        function generate() {
            var doc = new jsPDF();
            // Simple data example


            var head = ["ID", "Country", "Rank", "Capital"];
            var body = [
                [1, "Denmark", 7.526, "Copenhagen"],
                [2, "Switzerland", 7.509, "Bern"],
                [3, "Iceland", 7.501, "Reykjavík"],
                [3, "Iceland", 7.501, "Reykjavík"]
            ];
            doc.autoTable(head, body);
            // Simple html example
            //doc.autoTable({ html: '#table' });
            doc.save('table.pdf');
        }
            /*$("#GridView1 tbody").before("<thead>" + $($("#GridView1 tbody").find("tr")[0]).html() + "</thead>");
            var doc = new jsPDF();

            doc.autoTable({
                html: '#GridView1',
                bodyStyles: { minCellHeight: 15 }
            });

            doc.save("table.pdf");
        }
        */

        /*
          var doc = new jsPDF('l', 'pt');      
          var res = doc.autoTableHtmlToJson(document.getElementById("GridView1"));  
          var header = function (data) {          
              doc.setFontSize(15);              
              doc.setTextColor(40);      
              doc.setFontStyle('normal');    
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);   
              doc.text("Event Capture", data.settings.margin.top, 30);         
          };      
          var options = {       
           beforePageContent: header,     
           margin: {      
                  top: 40       
           },                
           startY: doc.autoTableEndPosY() + 50,     
            
       };
doc.autoTable(res.columns, res.data, options,
                didDrawCell: function (data) {
                    if (data.column.index === 6 && data.cell.section === 'body') {
                        var td = data.cell.raw;
                        // Assuming the td cells have an img element with a data url set (<td><img src="data:image/jpeg;base64,/9j/4AAQ..."></td>)
                        var img = td.getElementsByTagName('img')[0];
                        var dim = data.cell.height - data.cell.padding('vertical');
                        var textPos = data.cell.textPos;
                        doc.addImage(img.src, textPos.x, textPos.y, dim, dim);
                    }
                });  
          doc.save("DCR.pdf"); 
       }*/
    </script>

    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var indx = 1;
            var i = 0;
            function loadAddrss($tr) {
                var tbl = $('#GridView1 tr');
                var lat1 = $($tr).find('td')[5];
                var long1 = $($tr).find('td')[6];
                var lat2 = $(lat1).text().trim();
                var long2 = $(long1).text().trim();
                var addrs = '';

                //var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat2 + "," + long2 + "&key=sdvAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(long2) + '&lat=' + parseFloat(lat2) + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                    }
                });
                $($($tr).find('td')[7]).text(addrs); i++;
                setTimeout(function () { loadAddrss($('#GridView1 tr')[i]) }, 10); //loadAddrss(); 
                //                $.getJSON(geocodingAPI, function (json) {
                //                    if (json.status == "OK") {
                //                        var result = json.results[0];
                //                        addrs = result.formatted_address;
                //                        $x = $('#Addrss');
                //                       $($(tbl[indx]).find('td')[7]).text(result.formatted_address); indx++; loadAddrss(); 
                //                    }
                //                    else {
                //                        $x = $("#Addrss");
                //                        $($(tbl[indx]).find('td')[7]).text(json.status); indx++; loadAddrss();

                //                    }
                //                })
            }
            setTimeout(function () { loadAddrss($('#GridView1 tr')[0]) }, 10);
            /*indx++; 
            loadAddrss(); 
            indx++;
            loadAddrss(); 
            indx++;
            loadAddrss(); */
        });
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="80%">
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnPrint_Click" Visible="false"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnExcel_Click" />
                                    </td>
                                    <td style="display: none;">
                                        <input type="button" name="btnPDF" value="PDF" onclick="generate();return false;" id="Submit1" style="border-color:Black;border-width:1px;border-style:Solid;font-family:Verdana;font-size:10px;height:25px;width:60px;" />
                                        <%--<asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                           OnClientClick="generate();return false;" />--%>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="TP My Day Plan Report for" Font-Underline="True"
                                Font-Size="Small" Font-Bold="True" Visible="false"></asp:Label>
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1" runat="server" ForeColor="#333333"  AutoGenerateColumns="false" > 
								<AlternatingRowStyle BackColor="White" /> 
								<Columns> 
									<asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"  ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Fieldforce Name">
									   <ItemTemplate> 
										   <asp:Label ID="Label1" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label> 
									   </ItemTemplate> 
									</asp:TemplateField> 
									<asp:TemplateField HeaderText="Route Name" > 
									   <ItemTemplate> 
										   <asp:Label ID="Label2" runat="server" Text='<%#Eval("ClstrName")%>'></asp:Label> 
									   </ItemTemplate> 
									</asp:TemplateField> 
									<asp:TemplateField HeaderText="Customer Name" >                     
										<ItemTemplate> 
											<asp:Label ID="Label2" runat="server" Text='<%#Eval("retailor_name")%>'></asp:Label> 
										</ItemTemplate> 
									</asp:TemplateField> 
								   <asp:TemplateField HeaderText="Date" > 
									   <ItemTemplate> 
										   <asp:Label ID="Label2" runat="server" Text='<%#Eval("Insert_Date_Time")%>'></asp:Label> 
									   </ItemTemplate> 
								   </asp:TemplateField> 
								   <asp:TemplateField HeaderText="Lat">
                                       <ItemTemplate>
                                           <asp:Label ID="LAt" runat="server" Text='<%#Eval("lat") %>'></asp:Label>
                                       </ItemTemplate>
								   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Long">
                                       <ItemTemplate>
                                           <asp:Label ID='Long' runat="server" Text='<%#Eval("lon") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Address">
                                       <ItemTemplate>
                                           <asp:Label ID='Addrss' runat="server" Text=''></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
								   <asp:TemplateField HeaderText="Image"> 
									   <ItemTemplate> 
											<div class="phimg" data-url='<%# Eval("events","http://fmcg.sanfmcg.com/photos/{0}") %>'>
                                                <asp:Image ID="Image1" runat="server" Width="150" Height="100"  ImageUrl='<%# Eval("events","http://fmcg.sanfmcg.com/photos/{0}") %>'  onclick="imgPOP(this)" />
											</div>
									   </ItemTemplate> 
								   </asp:TemplateField> 
									<asp:TemplateField HeaderText="Remarks" > 
									   <ItemTemplate> 
										   <asp:Label ID="Label2" runat="server" Text='<%#Eval("remarks")%>'></asp:Label> 
									   </ItemTemplate> 
								   </asp:TemplateField>                                  
							   </Columns> 
							   <EditRowStyle BackColor="#2461BF" /> 
							   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /> 
							   <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /> 
							   <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /> 
							   <RowStyle BackColor="#EFF3FB" /> 
							   <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> 
							   <SortedAscendingCellStyle BackColor="#F5F7FB" /> 
							   <SortedAscendingHeaderStyle BackColor="#6D95E1" /> 
							   <SortedDescendingCellStyle BackColor="#E9EBEF" /> 
							   <SortedDescendingHeaderStyle BackColor="#4870BE" /> 
							</asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
			<!-- The Modal -->
			<div id="myModal" class="modal">

			  <!-- The Close Button -->
			  <span class="close">&times;</span>

			  <!-- Modal Content (The Image) -->
			  <img alt="" src="#" class="modal-content" id="img01" />

			  <!-- Modal Caption (Image Text) -->
			  <div id="caption"></div>
			</div>
        </center>
    </div>
	<script type="text/javascript">
        // Get the modal
        var modal = document.getElementById('myModal');

        // Get the image and insert it inside the modal - use its "alt" text as a caption
        var img = document.getElementById('myImg');
        var modalImg = document.getElementById("img01");
        var captionText = document.getElementById("caption");
        function imgPOP(x) {
            modal.style.display = "block";
            modalImg.src = x.src;
            captionText.innerHTML = x.alt;
        }

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }
    </script>
    </form>
</body>
</html>






<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_Event_Captures_View.aspx.cs" Inherits="Reports_Rpt_DCR_Event_Captures_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Captures Report</title>
  <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />    
   <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
   <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>   
   <link type="text/css" href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />    
   <link type="text/css" href="../css/style.css" rel="stylesheet" />    
   <script type="text/javascript" src="../../js/html2canvas.js"></script>
    
    <style>
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
        .tbldetail_main
        {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }
        .tbldetail_Data
        {
            height: 18px;
        }

		table#GridView1 th{
			padding:  5px 10px;
			font-size: 15px;
		}
		table#GridView1 td {
			padding: 10px;
			font-size: 15px;
		}
        .Holiday
        {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
        .NoRecord
        {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
		
		/*.phimg:hover:before {
			display: block;
			content: attr(data-url);
			background-image: attr(data-url url);
			background-position: center center;
			background-repeat: no-repeat;
			background-size: contain;
			position: absolute;
			width: 60% !important;
			height: 60% !important;
			left: 0px;
			top: 0px;
		}*/

/*popup image             */

.phimg img {
    border-radius: 5px;
    cursor: pointer;
    transition: 0.3s;
}

.phimg img:hover {opacity: 0.7;}

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
    width: 80%;
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

    <script type="text/javascript">
        function generate() {
            var doc = new jsPDF();
            // Simple data example


            var head = ["ID", "Country", "Rank", "Capital"];
            var body = [
            [1, "Denmark", 7.526, "Copenhagen"],
            [2, "Switzerland", 7.509, "Bern"],
            [3, "Iceland", 7.501, "Reykjavík"],
            [3, "Iceland", 7.501, "Reykjavík"]
        ];
            doc.autoTable(head, body);
            // Simple html example
            //doc.autoTable({ html: '#table' });
            doc.save('table.pdf');
        }
            /*$("#GridView1 tbody").before("<thead>" + $($("#GridView1 tbody").find("tr")[0]).html() + "</thead>");
                var doc = new jsPDF();

                doc.autoTable({
                    html: '#GridView1',
                    bodyStyles: { minCellHeight: 15 }
                });

                doc.save("table.pdf");
            }
            */

            /*
              var doc = new jsPDF('l', 'pt');      
              var res = doc.autoTableHtmlToJson(document.getElementById("GridView1"));  
              var header = function (data) {          
                  doc.setFontSize(15);              
                  doc.setTextColor(40);      
                  doc.setFontStyle('normal');    
                    //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);   
                  doc.text("Event Capture", data.settings.margin.top, 30);         
              };      
              var options = {       
               beforePageContent: header,     
               margin: {      
                      top: 40       
               },                
               startY: doc.autoTableEndPosY() + 50,     
                
           };
doc.autoTable(res.columns, res.data, options,
                    didDrawCell: function (data) {
                        if (data.column.index === 6 && data.cell.section === 'body') {
                            var td = data.cell.raw;
                            // Assuming the td cells have an img element with a data url set (<td><img src="data:image/jpeg;base64,/9j/4AAQ..."></td>)
                            var img = td.getElementsByTagName('img')[0];
                            var dim = data.cell.height - data.cell.padding('vertical');
                            var textPos = data.cell.textPos;
                            doc.addImage(img.src, textPos.x, textPos.y, dim, dim);
                        }
                    });  
              doc.save("DCR.pdf"); 
           }*/
           </script>

    <script language="Javascript">
        function RefreshParent() {
           // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var indx = 1;
            var i = 0;
            function loadAddrss($tr) {
                var tbl = $('#GridView1 tr');
                var lat1 = $($tr).find('td')[5];
                var long1 = $($tr).find('td')[6];
                var lat2 = $(lat1).text().trim();
                var long2 = $(long1).text().trim();
                var addrs = '';

                //var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat2 + "," + long2 + "&key=sdvAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(long2) + '&lat=' + parseFloat(lat2) + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                    }
                });
                $($($tr).find('td')[7]).text(addrs); i++;
                setTimeout(function () { loadAddrss($('#GridView1 tr')[i]) }, 10); //loadAddrss(); 
//                $.getJSON(geocodingAPI, function (json) {
//                    if (json.status == "OK") {
//                        var result = json.results[0];
//                        addrs = result.formatted_address;
//                        $x = $('#Addrss');
//                       $($(tbl[indx]).find('td')[7]).text(result.formatted_address); indx++; loadAddrss(); 
//                    }
//                    else {
//                        $x = $("#Addrss");
//                        $($(tbl[indx]).find('td')[7]).text(json.status); indx++; loadAddrss();

//                    }
//                })
            }
            setTimeout(function () { loadAddrss($('#GridView1 tr')[0]) }, 10);
            /*indx++; 
            loadAddrss(); 
            indx++;
            loadAddrss(); 
            indx++;
            loadAddrss(); */
        });
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="80%">
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <%--<asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnPrint_Click" Visible="false"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnExcel_Click" />
                                    </td>
                                    <td style="display: none;"><input type="button" name="btnPDF" value="PDF" onclick="generate();return false;" id="Submit1" style="border-color:Black;border-width:1px;border-style:Solid;font-family:Verdana;font-size:10px;height:25px;width:60px;">
                                        <%--<asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                           OnClientClick="generate();return false;" />
                                        </td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="TP My Day Plan Report for " Font-Underline="True"
                                Font-Size="Small" Font-Bold="True"></asp:Label>
<br/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1" runat="server" ForeColor="#333333"  AutoGenerateColumns="false" > 
								<AlternatingRowStyle BackColor="White" /> 
								<Columns> 
									<asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"  ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Fieldforce Name">
									   <ItemTemplate> 
										   <asp:Label ID="Label1" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label> 
									   </ItemTemplate> 
									</asp:TemplateField> 
									<asp:TemplateField HeaderText="Route Name" > 
									   <ItemTemplate> 
										   <asp:Label ID="Label2" runat="server" Text='<%#Eval("ClstrName")%>' Width="200"></asp:Label> 
									   </ItemTemplate> 
									</asp:TemplateField> 
									<asp:TemplateField HeaderText="Customer Name" >                     
										<ItemTemplate> 
											<asp:Label ID="Label2" runat="server" Text='<%#Eval("retailor_name")%>' Width="200"></asp:Label> 
										</ItemTemplate> 
									</asp:TemplateField> 
								   <asp:TemplateField HeaderText="Date" > 
									   <ItemTemplate> 
										   <asp:Label ID="Label2" runat="server" Text='<%#Eval("Insert_Date_Time")%>' Width="200"></asp:Label> 
									   </ItemTemplate> 
								   </asp:TemplateField> 
								   <asp:TemplateField HeaderText="Lat">
                                   <ItemTemplate>
                                   <asp:Label ID="LAt" runat="server" Text='<%#Eval("lat") %>' Width="80">
                                   </asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Long">
                                   <ItemTemplate>
                                   <asp:Label ID='Long' runat="server" Text='<%#Eval("lon") %>' Width="80"></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Address" HeaderStyle-Width="200px">
                                   <ItemTemplate>
                                   <asp:Label ID='Addrss' runat="server" Text='' Width="200"></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
								   <asp:TemplateField HeaderText="Image"> 
									   <ItemTemplate> 
											<div class="phimg" data-url='<%# Eval("events","http://fmcg.sanfmcg.com/photos/{0}") %>'>
										  <asp:Image ID="Image1" runat="server" Width="150" Height="100"  ImageUrl='<%# Eval("events","http://fmcg.sanfmcg.com/photos/{0}") %>'  onclick="imgPOP(this)" />
											</div>
									   </ItemTemplate> 
								   </asp:TemplateField> 
									<asp:TemplateField HeaderText="Remarks" > 
									   <ItemTemplate> 
										   <asp:Label ID="Label2" runat="server" Text='<%#Eval("remarks")%>' Width="200"></asp:Label> 
									   </ItemTemplate> 
								   </asp:TemplateField>                                  
							   </Columns> 
							   <EditRowStyle BackColor="#2461BF" /> 
							   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /> 
							   <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /> 
							   <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /> 
							   <RowStyle BackColor="#EFF3FB" /> 
							   <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> 
							   <SortedAscendingCellStyle BackColor="#F5F7FB" /> 
							   <SortedAscendingHeaderStyle BackColor="#6D95E1" /> 
							   <SortedDescendingCellStyle BackColor="#E9EBEF" /> 
							   <SortedDescendingHeaderStyle BackColor="#4870BE" /> 
							</asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
			<!-- The Modal -->
			<div id="myModal" class="modal">

			  <!-- The Close Button -->
			  <span class="close">&times;</span>

			  <!-- Modal Content (The Image) -->
			  <img class="modal-content" id="img01">

			  <!-- Modal Caption (Image Text) -->
			  <div id="caption"></div>
			</div>
        </center>
    </div>
	<script type="text/javascript">
			// Get the modal
			var modal = document.getElementById('myModal');

			// Get the image and insert it inside the modal - use its "alt" text as a caption
			var img = document.getElementById('myImg');
			var modalImg = document.getElementById("img01");
			var captionText = document.getElementById("caption");
			function imgPOP(x){
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
    </form>
</body>
</html>--%>