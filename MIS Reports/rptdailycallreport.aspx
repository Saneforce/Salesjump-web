<%@  Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="rptdailycallreport.aspx.cs" Inherits="MIS_Reports_rptdailycallreport" %>


<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Sales Man Call Tracking(Daily)</title>
        <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
        <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" type="text/css" />
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
        <script  type="text/javascript">
            function RefreshParent() {
                //  window.opener.document.getElementById('form1').click();
                window.close();
            }
        </script>
        <style type="text/css">
            .rptCellBorder {
                border: 1px solid;
                border-color: #999999;
            }

            .remove {
                text-decoration: none;
            }

            #myImg {
                border-radius: 5px;
                cursor: pointer;
                transition: 0.3s;
            }

             #myImg:hover {opacity: 0.7;}

            /* The Modal (background) */
            .modal {
                display: none; /* Hidden by default */
                position: fixed; /* Stay in place */
                z-index: 1; /* Sit on top */
                padding-top: -10px; /* Location of the box */
                left: 240px;
                top: 50px;
                width: 40%; /* Full width */
                height: 10%; /* Full height */
                /* Enable scroll if needed */
                min-height:81% !important;
            }

             /* Modal Content (image) */
            .modal-content {
                margin: auto;
                display: block;
                width: 80%;
                max-width: 700px;
            }

            /* Caption of Modal Image */
            #caption {
                margin: auto;
                display: block;
                width: 80%;
                max-width: 700px;
                text-align: center;
                color: #181515;
                padding: 10px 0;
                height: 150px;
                font-weight:bold;
            }

             /* Add Animation */
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
                color: #272222;
                font-size: 50px;
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
            /*.rpthCellBorder 
            {
                background-color:antiquewhite !important;
                color:black !important;
                font-weight:bold !important;
            }*/
            
        </style>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
        <script type="text/javascript">
            removecol = function () {
                for (i = 0; i < tbl.length; i++) {
                }
            }
            $(document).ready(function () {
                var indx = 2;
                function loadAddrss() {
                    $(".geoaddr").css("display", "none");
                    var tbl = $('#tbl tr');
                    var lat1 = $(tbl[indx]).find('td')[2];
                    var long1 = $(tbl[indx]).find('td')[3];
                    var lat2 = $(lat1).text().trim();
                    var long2 = $(long1).text().trim();
                    var addrs = '';

                    var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat2 + "," + long2 + "&key=asknAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                    //$.getJSON(geocodingAPI, function (json) {
                    //if (json.status == "OK") {
                    //var result = json.results[0];
                    //addrs = result.formatted_address;
                    //$x = $('#Addr');
                    //$($(tbl[indx]).find('.Addr')).text(result.formatted_address); indx++; loadAddrss();
                    //}
                    //else {
                    //$x = $("#Addr");
                    //$($(tbl[indx]).find('.Addr')).text(json.status); indx++; loadAddrss();
                    //}
                    //})
                }

                loadAddrss();

                //$(document).on('click', "#btnPrint", function () {
                //    var originalContents = $("body").html();
                //    var printContents = $("#content").html();
                //    $("body").html(printContents);
                //    window.print();
                //    $("body").html(originalContents);
                //    return false;
                //});
            });

            function imgPOP(x) {
                // Get the modal
                //var modal1 = document.getElementById('myModal');

                //// Get the image and insert it inside the modal - use its "alt" text as a caption
                //var img = document.getElementById('myImg');
                //var modalImg = document.getElementById("img01");
                //var captionText = document.getElementById("caption");
                ////modal1.style.display = "block";
                //$('#myModal').css("display", "block");
                //modalImg.src = x.src;
                //captionText.innerHTML = x.alt;

                var modal = document.getElementById("mymmodal");

                // Get the image and insert it inside the modal - use its "alt" text as a caption
                var img = document.getElementById("myImg");
                var modalImg = document.getElementById("im01");
                var captionText = document.getElementById("caption");

                modal.style.display = "block";
                //modalImg.src = x.src;
                modalImg.src = x.src;
                //captionText.innerHTML = x.alt;


                // Get the <span> element that closes the modal
                var span = document.getElementsByClassName("close")[0];

                // When the user clicks on <span> (x), close the modal
                span.onclick = function () {
                    modal.style.display = "none";
                }
            }

            // Get the <span> element that closes the modal

            //var span = document.getElementsByClassName("close")[0];

            // When the user clicks on <span> (x), close the modal

            //$(document).on('click', ".close", function () {
            //    $('#myModal').css("display", "none");
            //});


            // convert list to an array and loop over each item


            //$('.close').onClick(function () {
            //    $('#myModal').css("display", "none");
            //    //modal1.style.display = "none";
            //});

            //span.onclick = function () {
            //    $('#myModal').css("display", "none");
            //    //modal1.style.display = "none";
            //}
        </script>
        <script type="text/javascript">
            function PrintGridData() {

                var prtGrid = document.getElementById('<%=tbl.ClientID %>');

                prtGrid.border = 1;
                var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
                prtwin.document.write(prtGrid.outerHTML);
                prtwin.document.close();
                prtwin.focus();
                prtwin.print();
                prtwin.close();
            }

    </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:Panel ID="pnlbutton" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td style="width:60%;" align="center">
                                <asp:Label ID="lblHead" Text="Sales Man Call Tracking(Daily)" SkinID="lblMand" Font-Bold="true"  Font-Underline="true" runat="server"></asp:Label>
                            </td>
                            <td  style="width:40%;" align="right">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnPrint" Visible="false" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"></asp:LinkButton>
                                             <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                                                 <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Print.GIF" ToolTip="Print"
                                                     Width="20px" Style="border-width: 0px;" />
                                             </asp:LinkButton>
                                            <asp:LinkButton ID="btnExport"  runat="Server" Visible="false" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
                                            <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
                                            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>                
            </div>
            <br />
            <div>
                <asp:Panel ID="pnlContents" runat="server" Width="100%">
                    <div>
                        <table width="100%" align="center">
                            <tr>
                                <td width="2.5%"></td>
                                <td align="left">&nbsp;</td>
                                <td align="left">
                                    <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" align="center">
                            <tr>
                                <td width="2.5%"></td>
                                <td align="left">&nbsp;</td>
                                <td align="center">
                                    <asp:Label ID="Label1" Text="SalesMan Name  :" runat="server"  ForeColor="#0099CC" Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                                    <asp:Label ID="distname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp</td>                     
                            </tr>
                        </table>
                    </div>
                    <br /><%--<br />--%>
                    <div id="norecordfound" runat="server" align="center" style="border: 1px solid #99FFCC; background-color: #CCFFFF;">No Record Found</div>
                    <asp:Table ID="tbl"  runat="server" GridLines="Both">

                    </asp:Table>
                    <table width="100%" align="center">
                        <tbody>
                            <tr>
                                <td align="center">
                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>  
                    <br /><%--<br /><br />--%>
                    <div id="detail" runat="server" style="padding-left:100px;">
                        <table align="center">
                            <tbody style="border:1px solid black;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server"  Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Calls :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="callcount" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Productive Calls :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="productive" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Drop Size :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="drop_size" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Call Average :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="call_average" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Hours Worked :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="tot_hours" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Andalus" Visible="true" Font-Underline="True" ForeColor="#0099CC" Text="Total New Retailers :"></asp:Label>  
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="Total_new_rt" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr> 
                                    <td>
                                        <asp:Label ID="Label8" Visible="true" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Visited New Retailers :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="Tot_new_ret" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
								<tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" Visible="false" ForeColor="#0099CC" Text="Closing Time :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="closingtime" runat="server" Font-Bold="True" Visible="false" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                            </tbody> 
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <br />

           

            <!-- The Modal -->
            <div class="modal fade in" id="mymmodal" role="dialog" style="z-index: 10000000;  background-color: rgb(1 2 12 / 12%);" aria-hidden="false">
               <div class="modal-dialog">
                   <div class="modal-content">
                        <span class="close"  id="clos" style="font-weight:bold;  font-size:58px;color:red;margin-right:-65px;margin-top:-25px">&times;</span>
                       
                       <img id="im01" style="width: 100%;height:600px;">
                   </div>
                   

               </div>
           </div>

            <%--<div id="myModal" class="modal" style="opacity: 1;">
                <!-- The Close Button -->
                <%--<span class="close" style="opacity: 1;">&times;</span>--%>
                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>&nbsp;
                <span class="close">&times;</span>
                <!-- Modal Content (The Image) -->
                <img  class="modal-content" alt="" id="img01" />
                <!-- Modal Caption (Image Text) -->
                <div id="caption"></div>
            </div>--%>
        </form>
    </body>
</html>

