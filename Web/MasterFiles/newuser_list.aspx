<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="newuser_list.aspx.cs" Inherits="MasterFiles_newuser_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>User List</title>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        <style type="text/css">
            .grids th {
                padding-top: 4px;
                padding-bottom: 4px;
                padding-left: 3px;
                padding-right: 3px;
                text-align: center;
                background-color: #496a9a;
                color: white;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
                border: 1px solid #ddd;
            }

            .grids td {
                border: 1px solid #ddd;
                padding-top: 2px;
                padding-bottom: 2px;
                padding-left: 3px;
                padding-right: 3px;
                font-size: small;
                font-family: Calibri;
                background-color: white;
            }

            .selected {
                background-color: green;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="row" style="margin-top: 1rem;">
                <label class="col-md-2  col-md-offset-3  control-label">Company</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <select id="comdiv" data-dropup-auto="false" data-size="5"></select>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 1rem;">
                <label class="col-md-2  col-md-offset-3  control-label">Division</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <select id="ddldiv" data-dropup-auto="false" data-size="5"></select>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 1rem;">
                <label class="col-md-2  col-md-offset-3  control-label">Manager</label>
                <div class="col-md-5 inputGroupContainer">
                    <div class="input-group">
                        <select class="selectpicker" id="mgrli" data-dropup-auto="false" data-size="5">
                            <option value="">Select</option>
                        </select>
                    </div>
                </div>
            </div>
            <br />
            <center>
                <div>
                    <label style="color: Red;">
                        <input type="checkbox" id="wvcan" checked />
                        Without - Vacant</label>
                </div>
                <div>
                    <label style="color: Red;">
                        <input type="checkbox" id="ccoun" checked />
                        Without - Customer Count</label>
                </div>
            </center>
            <div class="row" style="margin-top: 5px">
                <div class="col-md-6  col-md-offset-5">
                    <button type="button" class="btn" id="btnview" style="background-color: #1a73e8; color: white;">View</button>
                    <img style="cursor: pointer; float: right;" src="../img/print.png" id="btnPrint" alt="" width="40" height="40">
                    <img style="cursor: pointer; float: right;" src="../limg/pdf.png" alt="" width="40" height="40" id="btnpdf">
                    <img style="cursor: pointer; float: right;" src="../img/excel.png" alt="" width="40" height="40" id="btnExport">
                </div>
            </div>
            <br />

            <div id="loading-image" style="display: none">
                <div id="sample" style="left: 50%; height: 100%; position: absolute; top: 50%;">
                    <img src="../Images/loading/loading17.gif" />
                </div>
            </div>

            <div id="tb" style="display: none;">
                <table class="grids" id="grid">
                    <thead>
                        <tr>
                            <th class='cn' style="display: none;">RTs Count</th>
                            <th>HQ</th>
                            <th>State Name</th>
                            <th>SF Code</th>
                            <th>Employee ID</th>
                            <th>FieldForce Name</th>
                            <th>Designation</th>
                            <th>Joining Date</th>
                            <th>Territory</th>
                            <th>Reporting</th>
                            <th>Mobile</th>
                            <th>Division</th>
                            <th>User Name</th>
                            <th>Password</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                    <tfoot></tfoot>
                </table>
            </div>

        </form>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
        <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
        <script type="text/javascript">
            var clos = [];
            var udtl = [];
            var ccnt = [];
            $(document).ready(function () {
                getDivision();
                loadsubDivision();

            });

            function getDivision() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "newuser_list.aspx/getcomDivision",
                    data: '{"divcode":"<%=Session["div_code"]%>"}',
                    dataType: "json",
                    success: function (data) {
                        AllDiv = JSON.parse(data.d) || [];
                        Div = AllDiv;
                        if (Div.length > 0) {
                            var dept = $("#comdiv");
                            dept.empty();
                            for (var i = 0; i < Div.length; i++) {
                                if (!(Div[i].Division_Name).includes('Select')) {
                                    dept.append($('<option value="' + Div[i].Division_Code + '">' + Div[i].Division_Name + '</option>'))
                                }
                            }
                        }
                    }
                });
                $('#comdiv').selectpicker({
                    liveSearch: true
                });
            }
            function loadsubDivision() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "newuser_list.aspx/getDivision",
                    data: '{"divcode":"<%=Session["div_code"]%>"}',
                    dataType: "json",
                    success: function (data) {
                        AllsDiv = JSON.parse(data.d) || [];
                        subDiv = AllsDiv;
                        if (subDiv.length > 0) {
                            var dept = $("#ddldiv");
                            dept.empty().append('<option selected="selected" value="">Select Division</option>');
                            for (var i = 0; i < subDiv.length; i++) {
                                dept.append($('<option value="' + subDiv[i].subdivision_code + '">' + subDiv[i].subdivision_name + '</option>'))
                            }
                        }
                    }
                });
                $('#ddldiv').selectpicker({
                    liveSearch: true
                });
                $('#ddldiv').selectpicker('refresh');
            }
            $('#ddldiv').change(function () {
                $('#mgrli').html('');
                fillddlmgr();
            });
            function fillddlmgr() {

                var sdiv = $('#ddldiv').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "newuser_list.aspx/fillddlmgr",
                    data: "{'divcode':'" + <%=Session["div_code"]%> +"','subdiv':'" + sdiv + "'}",
                    dataType: "json",
                    success: function (data) {
                        Allff = JSON.parse(data.d) || [];
                        mgrli = Allff;
                        if (mgrli.length > 0) {
                            var dept = $("#mgrli");
                            dept.empty().append('<option selected="selected" value="">Select Manager</option>');
                            for (var i = 0; i < mgrli.length; i++) {
                                dept.append($('<option value="' + mgrli[i].Sf_Code + '">' + mgrli[i].Sf_Name + '</option>'))
                            }
                        }
                    }
                });
                $('#mgrli').selectpicker({
                    liveSearch: true
                });

                $('#mgrli').selectpicker('refresh');
            }
            $('#btnview').click(function () {
                var subdiv = $('#ddldiv').val() || 0;
                if ("<%=sf_type%>" == "2" && subdiv == "0") {
                    alert("Select Division");
                    return false;
                }
                loadData();
            });
            async function loadData() {
                $('#tb').show();
                var cname = $("#comdiv").val();
                var diname = $("#ddldiv").val();
                var ff = $("#mgrli").val();
                str = '';
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "newuser_list.aspx/filldata",
                    async: false,
                    data: "{'comp':'" + cname + "','divi':'" + diname + "','fforce':'" + ff + "'}",
                    dataType: "json",
                    success: function (data) {

                        $('#grid tbody').html('');
                        Allorders = JSON.parse(data.d)
                        udtl = Allorders.filter(function (a) {
                            return a.rsfc == ff;
                        });
                        if (wvcan.checked == true) {
                            var udtl = Allorders.filter(function (a) {
                                return a.Active_Flag == 0 && a.rsfc == ff;
                            });
                        }
                        for (var i = 0; i < udtl.length; i++) {
                            str = $("<tr style='" + ((udtl[i].sf_type == 2) ? 'font-weight: bold;' : 'font-weight: normal;') + "'></tr>");
                            $(str).html("<td style='display:none' class='cn' id='" + udtl[i].Sf_Code + "'></td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Sf_HQ + "<input type='hidden' name='mty' value='" + udtl[i].sf_type + "'/></td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].StateName + "</td><td style='background-color:#" + udtl[i].Desig_Color + "' id='tb'>" + udtl[i].Sf_Code + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].sf_emp_id + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Sf_Name + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].sf_Designation_Short_Name + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Sf_Joining_Date + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Territory + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Reporting_To + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].SF_Mobile + "</td><td style='background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Division + "</td><td style='font-weight: bold;background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Sf_UserName + "</td><td style='font-weight: bold;background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Sf_Password + "</td><td style='" + ((udtl[i].Status == "Vacant" || udtl[i].Status == "Block") ? 'color: Red;font-weight: bold;font-size: 15pt;' : 'color: Black;') + "background-color:#" + udtl[i].Desig_Color + "'>" + udtl[i].Status + "</td>");
                            $('#grid tbody').append(str);
                            getReporting(udtl[i].Sf_Code);
                        }
                    }
                });
                if (ccoun.checked == false) {
                    $('#loading-image').show();
                    setTimeout(function () { getViewFields(); }, 3000);

                    var rtcnt = await new Promise((resolve) => setTimeout(() => resolve(fillcount())));
                    for (var i = 0; i < rtcnt.length; i++) {
                        $('#' + rtcnt[i].SF_Code).html(rtcnt[i].dr_count)
                    }
                    $('.cn').show();
                }
                else {
                    $('.cn').hide();
                }
            }
            function getReporting($sf_Code) {
                rpsfs = Allorders.filter(function (a) {
                    return a.rsfc == $sf_Code //|| a.Sf_Code == $sf_Code;
                });
                if (wvcan.checked == true) {
                    rpsfs = Allorders.filter(function (a) {
                        return a.Active_Flag == 0 && (a.rsfc == $sf_Code);// || a.Sf_Code == $sf_Code);
                    });
                }
                $(rpsfs).each(
                    function(){
                        let sstr = $("<tr style='" + ((this.sf_type == 2) ? 'font-weight: bold;' : 'font-weight: normal;') + "'></tr>");
                        $(sstr).html("<td style='display:none' class='cn' id='" + this.Sf_Code + "'></td><td style='background-color:#" + this.Desig_Color + "'>" + this.Sf_HQ + "<input type='hidden' name='mty' value='" + this.sf_type + "'/></td><td style='background-color:#" + this.Desig_Color + "'>" + this.StateName + "</td><td style='background-color:#" + this.Desig_Color + "' id='tb'>" + this.Sf_Code + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.sf_emp_id + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Sf_Name + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.sf_Designation_Short_Name + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Sf_Joining_Date + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Territory + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Reporting_To + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.SF_Mobile + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Division + "</td><td style='font-weight: bold;background-color:#" + this.Desig_Color + "'>" + this.Sf_UserName + "</td><td style='font-weight: bold;background-color:#" + this.Desig_Color + "'>" + this.Sf_Password + "</td><td style='" + ((this.Status == "Vacant" || this.Status == "Block") ? 'color: Red;font-weight: bold;font-size: 15pt;' : 'color: Black;') + "background-color:#" + this.Desig_Color + "'>" + this.Status + "</td>");
                        $('#grid tbody').append(sstr);
                        getReporting(this.Sf_Code);
                    }
                    );
                //for ($j = 0; $j < rpsfs.length; $j++) {
                   /* if ($('#grid').find(`#${this.Sf_Code}`).length < 1) {
                        let sstr = $("<tr style='" + ((this.sf_type == 2) ? 'font-weight: bold;' : 'font-weight: normal;') + "'></tr>");
                        $(sstr).html("<td style='display:none' class='cn' id='" + this.Sf_Code + "'></td><td style='background-color:#" + this.Desig_Color + "'>" + this.Sf_HQ + "<input type='hidden' name='mty' value='" + this.sf_type + "'/></td><td style='background-color:#" + this.Desig_Color + "'>" + this.StateName + "</td><td style='background-color:#" + this.Desig_Color + "' id='tb'>" + this.Sf_Code + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.sf_emp_id + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Sf_Name + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.sf_Designation_Short_Name + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Sf_Joining_Date + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Territory + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Reporting_To + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.SF_Mobile + "</td><td style='background-color:#" + this.Desig_Color + "'>" + this.Division + "</td><td style='font-weight: bold;background-color:#" + this.Desig_Color + "'>" + this.Sf_UserName + "</td><td style='font-weight: bold;background-color:#" + this.Desig_Color + "'>" + this.Sf_Password + "</td><td style='" + ((this.Status == "Vacant" || this.Status == "Block") ? 'color: Red;font-weight: bold;font-size: 15pt;' : 'color: Black;') + "background-color:#" + this.Desig_Color + "'>" + this.Status + "</td>");
                        $('#grid tbody').append(sstr);
                        getReporting(this.Sf_Code);
                    }*/
                //}
            }
            function fillcount() {
                var cname = $("#comdiv").val();
                var diname = $("#ddldiv").val();
                var ff = $("#mgrli").val();
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "newuser_list.aspx/fillccont",
                    async: false,
                    data: "{'comp':'" + cname + "','divi':'" + diname + "','fforce':'" + ff + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#loading-image').hide();
                        ccnt = JSON.parse(data.d);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                return ccnt;
            };
            $('#btnpdf').click(function () {

                var HTML_Width = $("#grid").width();
                var HTML_Height = $("#grid").height();
                var top_left_margin = 15;
                var PDF_Width = HTML_Width + (top_left_margin * 2);
                var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
                var canvas_image_width = HTML_Width;
                var canvas_image_height = HTML_Height;

                var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


                html2canvas($(".grids")[0], { allowTaint: true }).then(function (canvas) {
                    canvas.getContext('2d');

                    console.log(canvas.height + "  " + canvas.width);


                    var imgData = canvas.toDataURL("image/jpeg", 1.0);
                    var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                    pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                    for (var i = 1; i <= totalPDFPages; i++) {
                        pdf.addPage(PDF_Width, PDF_Height);
                        pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                    }

                    pdf.save("userlist.pdf");
                });
            });
            $('#btnExport').click(function () {

                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };
                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };
                htmls = document.getElementById("grid").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'userlist' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
            $(document).on('click', "#btnPrint", function () {
                var prtGrid = document.getElementById('grid');
                prtGrid.border = 1;
                var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=2,status=0,resizable=yes');
                prtwin.document.write(prtGrid.outerHTML);
                prtwin.document.close();
                prtwin.focus();
                prtwin.print();
                prtwin.close();

            });
        </script>
    </body>
    </html>
</asp:Content>