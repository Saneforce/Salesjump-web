<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="UploadExcelFile.aspx.cs" Inherits="MasterFiles_UploadExcelFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="from1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <div class="col-xs-3">
                    <a class="list-group-item active state">Excel Format & Fields</a>
                    <div style="overflow-y: scroll; height: 200px">
                        <div class="form-group">
                            <label for="ddlType">Choose table</label>
                            <select id="tblName1" style="box-shadow: none; width: 200px;">
                            </select>
                        </div>

                        <div class="list-group" id="chkAliceName" style="border: 1px solid #ddd">
                        </div>

                    </div>
                    <div class='box-footer'>
                        <div class='btn btn-primary col-xs-12'>
                            <button type='button' id='btnDownload' style="min-width: 100% !important;" class='btn btn-primary'>Download</button>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div id="btn">
                    </div>
                </div>
                <div class="col-xs-9">
                    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black;">
                        <tr>
                            <td style="padding-right: auto;">
                                <asp:Label ID="lblExcel" runat="server" Style="position: relative; color: #336277; font-family: Verdana; font-weight: bold; text-transform: capitalize; font-size: 14px; margin-top: 20px;">Excel file</asp:Label>
                                <br />
                                <div>
                                    <input type="file" id="fileid" style="padding-left: 20px; padding-top: 20px" />
                                </div>
                            </td>
                            <td>
                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="ddlType1">Sheet Name:</label>
                                        <select id="sheetid" class="form-control">
                                            <option selected="selected">Select Sheet</option>
                                        </select>
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="form-group">
                                    <label for="ddlType">Choose table</label>
                                    <select id="tblName" style="box-shadow: none; width: 200px;">
                                        <option selected="selected">Select table</option>
                                    </select>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="pglimit"></div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <img onclick="exceldwnld()" src="../../img/Excel-icon.png" style="cursor: pointer; float: right; display: none;" id="btnExport" width="40" height="40" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="form-group">
                                    <div id="ExcelRecords">
                                        <table>
                                            <tr>
                                                <td>Success :&nbsp<span id="SucessRecord"></span></td>
                                            </tr>
                                            <tr>
                                                <td>Error :&nbsp<span id="ErrorRecord"></span></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table id="exceltable" style="text-align: center">
                                        <thead></thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="pgNo"></div>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <%--<script type="text/javascript" src="../js/jhxlsx.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.min.js" 
        integrity="sha384-tsQFqpEReu7ZLhBV2VZlAu7zcOV+rXbYlF2cqB8txI/8aZajjp4Bqd+V6D5IgvKT" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/alasql/0.3/alasql.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.7.12/xlsx.core.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/xlsx.core.min.js"></script>
    <script type="text/javascript" src="../js/FileSaver.min.js"></script>--%>
    <script type="text/javascript">
        var columnSet = []; var excelRows = []; var dvExcel = '';
        var tbl = '';
        var columns = '';
        var SFStates = [];
        var str = '';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "";
        var mUpl = [];

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                BindTable(excelRows, dvExcel)
                loadAliseName($('#tblName :selected').text());
                loadAliseName1($('#tblName1 :selected').text());
            }
        );
        function getUploadSettings(toolname) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "UploadExcelFile.aspx/getUploadSettings",
                data: "{divcode:'<%=Session["div_code"]%>','toolname':'" + toolname + "'}",
                dataType: "json",
                success: function (data) {
                    mUpl = JSON.parse(data.d) || [];
                },
                error: function (rs) {
                    alert(rs);
                }
            });
        }
        function ExcelDateToJSDate(serial) {
            //var utc_days  = Math.floor(serial - 25569);
            //var utc_value = utc_days * 86400;                                        
            //var date_info = new Date(utc_value * 1000);

            //var fractional_day = serial - Math.floor(serial) + 0.0000001;

            //var total_seconds = Math.floor(86400 * fractional_day);

            //var seconds = total_seconds % 60;

            //total_seconds -= seconds;

            //var hours = Math.floor(total_seconds / (60 * 60));
            //var minutes = Math.floor(total_seconds / 60) % 60;
            var dt = new Date((serial - (25569)) * 86400 * 1000);
            return dt.getFullYear() + '-' + (parseInt(dt.getMonth()) + 1) + '-' + dt.getDate();
        }
        function exceldwnld() {
            var errexl = excelRows.filter(function (a) {
                return a.Success != "Success";
            });
            var droptblname = $("#sheetid option:selected").text();
            var ws = XLSX.utils.json_to_sheet(errexl);
            var wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, droptblname);
            XLSX.writeFile(wb, "Uploaded_Excel.xlsx");
        }
        $(document).ready(function () {
            loadData1();
            $('#ExcelRecords').hide();
            $('#tblName').on('change', function () {
                tbl = $('#tblName').val();
                if (tbl != '0') {
                    loadAliseName($('#tblName :selected').text());
                }
                else
                    alert("Select a Tool Name");
            });
            $('#tblName1').on('change', function () {
                tbl = $('#tblName1').val();
                if (tbl != '0')
                    loadAliseName1($('#tblName1 :selected').text());
                else
                    alert("Select a Tool Name");
            });
            $(document).on('change', '.ddlAlise', function () {
                var cthis = this;
                var thiss = $(this).find('option:selected').text();
                var sField_Name = '';
                $('.ddlAlise').each(function () {
                    var ethis = this.isSameNode(cthis);
                    sField_Name += (((($(this).find('option:selected').text()) == thiss) && (!ethis)) ? ($(this).find('option:selected').text() + ',') : '');
                });
                if (sField_Name != '') {
                    alert('This Field is Already Selected');
                    $(this).val(0);
                }
            });
        });
        $(document).on('change', '#fileid', function () {
            Upload();
        });

        
        $(document).on('click', '#btnDownload', function () {
            if ($('#tblName1').val() != '0') {
                if ($("input:checkbox[name=fields]:checked").length > 0) {
                    var ExcelName = $('#tblName1 :selected').text();
                    if (ExcelName == "Tax") {
                        var array = [];
                        var arr = [];
                        item1 = {};
                        var dataa = [],dataa1=[];
                        $("input:checkbox[name=fields]:checked").each(function () {
                            var colName = $(this).closest('a').text();
                            arr.push({
                                [colName]: colName,
                            });
                            item1[colName] = '';
                        });
                        array.push(item1);
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "UploadExcelFile.aspx/getStates",
                            data: "{}",
                            dataType: "json",
                            success: function (data) {
                                dataa = JSON.parse(data.d);
                            }
                        });
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "UploadExcelFile.aspx/getTaxmas",
                            data: "{}",
                            dataType: "json",
                            success: function (data) {
                                dataa1 = JSON.parse(data.d);
                            }
                        });
                        //var opts = [{ sheetid: 'Sheet1', header: true }, { sheetid: 'StateName', header: true }, { sheetid: 'TaxName', header: true }];
                        //var res = alasql('SELECT INTO XLSX("' + ExcelName +'.xlsx",?) FROM ?',
                         //   [opts, [array, dataa, dataa1]]);
						 var ws = XLSX.utils.json_to_sheet(array);
                        var wb = XLSX.utils.book_new();
                        XLSX.utils.book_append_sheet(wb, ws, ExcelName);
                        XLSX.writeFile(wb, ExcelName + ".xlsx");
						 
                    }
                    else {
                        var array = [];
                        $("input:checkbox[name=fields]:checked").each(function () {
                            var colName = $(this).closest('a').text()
                            array.push({
                                [colName]: ""
                            });
                        });
                        var ws = XLSX.utils.json_to_sheet(array);
                        var wb = XLSX.utils.book_new();
                        XLSX.utils.book_append_sheet(wb, ws, ExcelName);
                        XLSX.writeFile(wb, ExcelName + ".xlsx");
                        //var array = [];
                        //$("input:checkbox[name=fields]:checked").each(function () {
                        //    var colName = $(this).closest('a').text();
                        //    array.push({
                        //        [colName]: ""
                        //    });
                        //});
                        //var opts = [{ sheetid: 'Sheet1', header: true }];
                        //var res = alasql('SELECT INTO XLSX("' + ExcelName + '.xlsx",?) FROM ?',
                        //    [opts, [array]]);
                    }
                }
                else {
                    alert("Please select a Field");
                }
            }
        });

        $(document).on('click', '#btnUpload', function () {
            var tb = $('#tblName').val();
            $('#ExcelRecords').show();
            var toolnm = $('#tblName :selected').text();
            var Field_Name = '';
            $('.ddlAlise').each(function () {
                Field_Name += $(this).find('option:selected').text() + ',';
            });
            var srecords = 0;
            var erecords = 0;
            for (var i = 0; i < excelRows.length; i++) { //excelRows.length
                var newObj = {};
                try {
                    for (var j = 0; j < columnSet.length; j++) {
                        if (excelRows[i][columnSet[j]] == undefined) {
                            excelRows[i][columnSet[j]] = "";
                        }
                        else if (((excelRows[i][columnSet[j]]).toString()) == null) {
                            excelRows[i][columnSet[j]] = "";
                        }
                        if (((excelRows[i][columnSet[j]]).toString()).indexOf("\'") > -1) {
                            excelRows[i][columnSet[j]] = (excelRows[i][columnSet[j]]).replaceAll("\'", "u0027");
                        }
                        if ((columnSet[j]).toString().toLowerCase().indexOf("date") > -1) {
                            newObj[columnSet[j]] = ExcelDateToJSDate(excelRows[i][columnSet[j]]);
                        }
                        else {
                            newObj[columnSet[j]] = excelRows[i][columnSet[j]];
                        }
                    }
                    var newArr = [];
                    newArr.push(newObj);
                }
                catch (err) {
                    excelRows[i]["Success"] = err.message;
                }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "UploadExcelFile.aspx/InsertExcelData",
                    data: `{'data':'${(JSON.stringify(newArr[0]))}','Table_Name':'${tb}','column_name':'${Field_Name}','div':'${<%=Session["div_code"]%>}','ToolNm':'${toolnm}'}`,
                    dataType: "json",
                    success: function (data) {
                        try {
                            var repText = JSON.parse(data.d);
                            excelRows[i]["Success"] = repText.success;
                            console.log(repText.Qry);
                            if (repText.success != "Success") {
                                erecords += 1;
                            }
                            else {
                                srecords += 1;
                            }
                            setTimeout(function () {
                                $('#ErrorRecord').html(erecords);
                                $('#SucessRecord').html(srecords);
                            }, 500);
                        }
                        catch (err) {
                            excelRows[i]["Success"] = err.message;
                            erecords += 1;
                            setTimeout(function () {
                                $('#ErrorRecord').html(erecords);
                                $('#SucessRecord').html(srecords);
                            }, 500);
                        }
                    },
                    error: function (rs) {
                        excelRows[i]["Success"] = (JSON.parse(rs.responseText).Message);
                        erecords += 1;
                        setTimeout(function () {
                            $('#ErrorRecord').html(erecords);
                            $('#SucessRecord').html(srecords);
                        }, 500);
                    }
                });
            }
            BindTable(excelRows, dvExcel)
            $('#btnExport').show();
        });
        $(document).on('change', '#sheetid', function () {
            //Reference the FileUpload element.
            var fileUpload = document.getElementById("fileid");

            //Validate whether File is valid Excel file.
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
            if (regex.test(fileUpload.value.replace(/ *\([^)]*\) */g, "").toLowerCase())) {
                if (typeof (FileReader) != "undefined") {
                    var reader = new FileReader();

                    //For Browsers other than IE.
                    if (reader.readAsBinaryString) {
                        reader.onload = function (e) {
                            BindExcel(e.target.result, $("#sheetid>option:selected").text());
                            loadData();
                        };
                        reader.readAsBinaryString(fileUpload.files[0]);
                        //loadData();
                    } else {
                        //For IE Browser.
                        reader.onload = function (e) {
                            var data = "";
                            var bytes = new Uint8Array(e.target.result);
                            for (var i = 0; i < bytes.byteLength; i++) {
                                data += String.fromCharCode(bytes[i]);
                            }
                            BindExcel(data, $("#sheetid>option:selected").text());
                        };
                        reader.readAsArrayBuffer(fileUpload.files[0]);
                    }
                } else {
                    alert("This browser does not support HTML5.");
                }
            }
            else {
                alert("Please upload a valid Excel file.");
            }

        });

        function Upload() {
            //Reference the FileUpload element.
            var fileUpload = document.getElementById("fileid");

            //Validate whether File is valid Excel file.
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
            //if (regex.test(fileUpload.value.toLowerCase())) {
            if (regex.test(fileUpload.value.replace(/ *\([^)]*\) */g, "").toLowerCase())) {
                if (typeof (FileReader) != "undefined") {
                    var reader = new FileReader();

                    //For Browsers other than IE.
                    if (reader.readAsBinaryString) {
                        reader.onload = function (e) {
                            ProcessExcel(e.target.result);
                        };
                        reader.readAsBinaryString(fileUpload.files[0]);
                    } else {
                        //For IE Browser.
                        reader.onload = function (e) {
                            var data = "";
                            var bytes = new Uint8Array(e.target.result);
                            for (var i = 0; i < bytes.byteLength; i++) {
                                data += String.fromCharCode(bytes[i]);
                            }
                            ProcessExcel(data);
                        };
                        reader.readAsArrayBuffer(fileUpload.files[0]);
                    }
                } else {
                    alert("This browser does not support HTML5.");
                }
            } else {
                alert("Please upload a valid Excel file.");
            }
        };
        function ProcessExcel(data) {
            //Read the Excel File data.
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            //Fetch the name of First Sheet.
            var firstSheet = workbook.SheetNames;
            if (firstSheet.length > 0) {
                var states = $("#sheetid");
                //states.empty()
                states.empty().append('<option selected="selected" value="0">Select Sheet</option>');
                for (var i = 0; i < firstSheet.length; i++) {
                    states.append($('<option value="' + firstSheet[i] + '">' + firstSheet[i] + '</option>'))
                }
            }
        };

        function BindExcel(data, sheetname) {
            //Read the Excel File data.
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            //Fetch the name of First Sheet.
            //var firstSheet = workbook.SheetNames[0];
            //Read all rows from First Sheet into an JSON array.
            excelRows = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetname]);

            //var dvExcel = document.getElementById("dvExcel");            
            dvExcel = document.getElementById("exceltable");
            dvExcel.border = "1";
            dvExcel.width = "100%"; BindTable(excelRows, dvExcel)

        };
        function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/
            $('#exceltable thead').html('')
            $('#exceltable tbody').html('')
            st = PgRecords * (pgNo - 1);
            columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/
            var tr = $("<tr></tr>");
            for (var i = 0; i < columns.length; i++) {
                if (columns[i] != 'Success') {
                    var td1 = $("<td></td>");
                    var combo = $("<select class='form-control ddlAlise'><option selected='0'>Select Alias Name</option></select>");
                    td1.append(combo);
                    tr.append(td1);
                }
            }
            tr.append("<td></td>");
            $('#exceltable thead').append(tr);
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < jsondata.length) {
                    var row$ = $('<tr/>');
                    for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                        var cellValue = jsondata[$i][columns[colIndex]];
                        if (columns[i] != 'Success') {
                            if (cellValue == null)
                                cellValue = "";
                            row$.append($('<td colName="' + (columns[colIndex]).trim() + '"></td>').html((cellValue).toString().trim()));
                        }
                        else {
                            row$.append($('<td colName="' + (columns[colIndex]).trim() + '"></td>').html((cellValue).toString().trim()));
                        }
                    }
                    if (excelRows[0]["Success"] == undefined)
                        row$.append("<td></td>");
                    $('#exceltable tbody').append(row$);
                }
            }
            var uplbtn = $("#btn");
            uplbtn.html("");
            var btn = $("<div class='box-footer'><div class='btn btn-primary col-xs-12'><button type='button' id='btnUpload' style='min-width: 100% !important;' class='btn btn-primary'>Upload</button></div></div></div>");
            uplbtn.append(btn);

            var pg = $("#pgNo");
            pg.html("");
            var div = $('<div class="row" style="padding:5px 0px"><div class="col-sm-5"><div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div></div><div class="col-sm-7"><div class="dataTables_paginate paging_simple_numbers" id="example2_paginate"><ul class="pagination" style="float:right;margin:-11px 0px"><li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li><li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li></ul></div></div></div>')// $("<div class='row' style='padding: 5px 0px'><div class='col-sm-5'><div class='dataTables_info' id='orders_info' role='status' aria-live='polite'>Showing 0 to 0 of 0 entries</div></div><div class='col-sm-7'><div class='dataTables_paginate paging_simple_numbers' id='example2_paginate'><ul class='pagination' style='float: right; margin: -11px 0px'><li class='paginate_button previous disabled' id='example2_previous'><a href='#' aria-controls='example2' data-dt-idx='0' tabindex='0'>Previous</a></li> <li class='paginate_button next' id='example2_next'><a href='#' aria-controls='example2' data-dt-idx='7' tabindex='0'>Next</a></li>  </ul></div></div></div>")
            pg.append(div);
            $(".paginate_button.next>a").attr("data-dt-idx", parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)))

            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < jsondata.length) ? (st + PgRecords) : jsondata.length) + " of " + jsondata.length + " entries")
            Orders = jsondata;
            loadPgNos();



        }

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg)) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx"));
                BindTable(excelRows, dvExcel)
                loadAliseName($('#tblName :selected').text());
                loadAliseName1($('#tblName1 :selected').text());
            }
            );
        }

        function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/
            columnSet = [];
            var headerTr$ = $('<tr/>');
            for (var i = 0; i < jsondata.length; i++) {
                var rowHash = jsondata[i];
                for (var key in rowHash) {
                    if (rowHash.hasOwnProperty(key)) {
                        //columnSet = [];
                        if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/
                            columnSet.push(key);
                            headerTr$.append($('<th/>').html(key));
                        }
                    }
                }
            }
            if (excelRows[0]["Success"] == undefined)
                headerTr$.append('<th>Success</th>');
            $('#exceltable thead').append(headerTr$);
            return columnSet;
        }
        function loadData() {
            $('#tblName').html("");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "UploadExcelFile.aspx/getTable",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFStates1 = JSON.parse(data.d) || [];
                    if (SFStates1.length > 0) {
                        var states = $("#tblName");
                        var droptblname = $("#sheetid option:selected").text();
                        states.empty().append('<option selected="selected" value="0">Select table</option>');
                        for (var i = 0; i < SFStates1.length; i++) {
                            states.append($('<option value="' + SFStates1[i].Master_Table_Name + '">' + SFStates1[i].Tool_Name + '</option>'))
                        }
                        $('#tblName option:contains(' + droptblname + ')').prop('selected', true);
                        loadAliseName(droptblname);
                    }
                }
            });
        }

        function loadData1() {
            $('#tblName1').html("");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "UploadExcelFile.aspx/getTable",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFStates = JSON.parse(data.d) || [];
                    if (SFStates.length > 0) {
                        var states = $("#tblName1");
                        states.empty().append('<option selected="selected" value="0">Select table</option>');
                        for (var i = 0; i < SFStates.length; i++) {
                            states.append($('<option value="' + SFStates[i].Tool_Name + '">' + SFStates[i].Tool_Name + '</option>'))
                        }
                    }
                }
            });
        }

        function loadAliseName(tbl) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "UploadExcelFile.aspx/getAliseName",
                data: "{'divcode':'<%=Session["div_code"]%>','toolname':'" + tbl + "'}",
                dataType: "json",
                success: function (data) {
                    SFStates = JSON.parse(data.d) || [];
                    if (SFStates.length > 0) {
                        var states = $(".ddlAlise");
                        //states.empty();
                        //  if (tbl == "Route") {
                        states.empty().append('<option selected="selected" value="0">Select Alias Name</option>');
                        for (var i = 0; i < SFStates.length; i++) {
                            states.append($('<option value="' + SFStates[i].Field_Name + '">' + SFStates[i].Alise_Name + '</option>'))

                        }
                        for (var i = 0; i < columnSet.length; i++) {
                            $($(states)[i]).find('option').each(function () {
                                if (this.innerHTML == columnSet[i]) {
                                    //var sel = this.innerHTML;
                                    $(this).prop("selected", true);

                                }
                            });
                        }
                    }
                }
            });

        }

        function loadAliseName1(tbl) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "UploadExcelFile.aspx/getAliseName",
                data: "{'divcode':'<%=Session["div_code"]%>','toolname':'" + tbl + "'}",
                dataType: "json",
                success: function (data) {
                    SFStates = JSON.parse(data.d) || [];
                    if (SFStates.length > 0) {
                        //                        SFStates.sort(function (a, b) {
                        //                          if (a["Order_By"].toLowerCase() < b["Order_By"].toLowerCase() && asc == 'true') return -1;
                        //                        if (a["Order_By"].toLowerCase() > b["Order_By"].toLowerCase() && asc == 'true') return 1;
                        //
                        //                          if (b["Order_By"].toLowerCase() < a["Order_By"].toLowerCase() && asc == 'false') return -1;
                        //                        if (b["Order_By"].toLowerCase() > a["Order_By"].toLowerCase() && asc == 'false') return 1;
                        //                      return 0;
                        //                });
                        var states = $("#chkAliceName");
                        //if (tbl == "Route") {
                        states.empty();
                        for (var i = 0; i < SFStates.length; i++) {
                            str = '<a href="#" class="list-group-item">' + SFStates[i].Alise_Name + '<input type="checkbox" id="chk_' + SFStates[i].Field_Name + '" name="fields" class="chk pull-right"' + ((SFStates[i].Mantatory == "1") ? "checked='true' disabled='true'" : "") + '/></a>';
                            $(states).append(str);
                        }
                    }

                }
            });
        }



    </script>
</asp:Content>
