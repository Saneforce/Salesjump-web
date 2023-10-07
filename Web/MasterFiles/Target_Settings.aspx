<%@ Page Title="Target Setting" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Target_Settings.aspx.cs" EnableEventValidation="false" Inherits="MasterFiles_Target_Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="../css/style1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        $(document).ready(function () {
           
       

            $("input").live("keypress", function (e) {
                var num = e.keyCode;
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                    return false;
                }
            });

            $("input").blur(function (e) {
                if ($(this).val().length == 0) {
                    $(this).val("0");
                }
            });




            $(document).on('click', '#btnsave', function () {
                var dtls_tab = document.getElementById("table1");
                var nrows1 = dtls_tab.rows.length;
                var Ncols = dtls_tab.rows[0].cells.length;
                if (nrows1 > 1) {
                    var ch = true;
                    var arr = [];
                    $('#table1 tbody tr').each(function () {
                        var fd = 2;

                        for (var i = 2; i < Ncols; i++) {
                            fd = i + 0;
                            console.log(fd + " : " + Ncols);
                            if ($(this).attr('class') != "thh") {
                                arr.push({
                                    Code: $(this).children('td').eq(1).find('input[name=code]').val().toLowerCase().toString(),
                                    values: $(this).find('td').eq(fd).find('input[type="text"]').val(),
                                    months: $(this).closest('table').find('th').eq(fd).find('input[name=months]').val(),
                                    years: $('#<%=ddlFYear.ClientID%> :selected').val(),
                                    state: $('#<%=ddlstate.ClientID%> :selected').val(),
                                });
                            }
                        }
                    });
                    // alert(JSON.stringify(arr));
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Target_Settings.aspx/savedata",
                        data: "{'data':'" + JSON.stringify(arr) + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert("Target has been updated successfully!!!");
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
                else {
                    alert('Please Create Master Target Settings!!!');
                }
            });





        });    

        $(document).on('click', '#btnGo', function () {
           
            var selyear = $('#<%=ddlFYear.ClientID%> :selected').val();

           $('#table1 tbody').html('');
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Target_Settings.aspx/Get_Target_Name",
                 dataType: "json",
                 success: function (data) {
                     var lcategory = "";
                     for (var i = 0; i < data.d.length; i++) {
                         //if (lcategory != data.d[i].Category) {
                         //    $('#table1 tbody').append("<tr class='thh'><th>" + data.d[i].Category + " </th></tr>");
                         //}
                         var str = "<tr><td style='min-width: 50px;'>" + (i + 1) + "</td> <td style='min-width: 350px;' ><input type='hidden' name='categ' value=" + data.d[i].Category + "/><input type='hidden' name='code' value=" + data.d[i].Code + "><label name='tname' for='tname'>" + data.d[i].Name + "</label></td> ";
                         str += "<td><input type='text' name='values' value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values' value='0'   class='form-control'/></td>";
                         str += "<td><input type='text' name='values' value='0'   class='form-control'/></td>";
                         str += "<td><input type='text' name='values' value='0'   class='form-control'/></td>";
                         str += "<td><input type='text' name='values'  value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values' value='0'   class='form-control'/></td>";
                         str += "<td><input type='text' name='values'  value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values'  value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values'  value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values'  value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values'  value='0'  class='form-control'/></td>";
                         str += "<td><input type='text' name='values' value='0'   class='form-control'/></td>";
                         str += "</tr>";
                         $('#table1 tbody').append(str);
                         lcategory = data.d[i].Category;
                     }
                 },
                 error: function (result) {
                     alert(JSON.stringify(result));
                 }
             });

             var dtls_tab = document.getElementById("table1");
             var nrows1 = dtls_tab.rows.length;
             var Ncols = dtls_tab.rows[0].cells.length;

            var val = $('#<%=ddlFYear.ClientID%> :selected').val();
            var st = $('#<%=ddlstate.ClientID%> :selected').val();
            
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: false,
                 url: "Target_Settings.aspx/gettargetsetting",
                 dataType: "json",
                 data: "{'data':'" + val + "','state':'" + st + "'}",
                 success: function (data) {
                     if (data.d.length > 0) {
                         for (var i = 0; i < data.d.length; i++) {
                             $('#table1 tbody tr').each(function () {
                                 var fd = 1;
                                 for (var col = 0; col < Ncols - 1; col++) {
                                     fd = col + 1;
                                     if ($(this).attr('class') != "thh") {
                                         if ($(this).children('td').eq(1).find("[name='code']").val().toString() == data.d[i].Code.toString()) {
                                             if ($(this).closest('table').find('th').eq(fd).find('input[name=months]').val() == data.d[i].months) {
                                                 $(this).find('td').eq(fd).find('input[type="text"]').val(data.d[i].tvalues);
                                             }
                                         }
                                     }
                                 }

                             });
                         }
                     }
                 },
                 error: function (result) {
                     alert(JSON.stringify(result));
                 }
             });

         });



    </script>
    <style type="text/css">
        .button
        {
            display: inline-block;
            border-radius: 4px;
            background-color: #6495ED;
            border: none;
            color: #FFFFFF;
            text-align: center;
            font-bold: true;
            width: 75px;
            height: 29px;
            transition: all 0.5s;
            cursor: pointer;
            margin: 5px;
        }
        
        .button span
        {
            cursor: pointer;
            display: inline-block;
            position: relative;
            transition: 0.5s;
        }
        
        .button span:after
        {
            content: '»';
            position: absolute;
            opacity: 0;
            top: 0;
            right: -20px;
            transition: 0.5s;
        }
        
        .button:hover span
        {
            padding-right: 25px;
        }
        
        .button:hover span:after
        {
            opacity: 1;
            right: 0;
        }
        
       #table1 th {
            position: sticky;
            top: 0;
            background: #6c7ae0;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }
        .form-control{
            width: 50px;
        }
        #table1 td,th {
            padding: 5px;
            border: 1px solid #ddd;
        }
        input
        {
            text-align: right;
        }
        .pad {
    display: block;
    padding: 5px;
    height: 32px;
}
    </style>
    <form runat="server">
    <center>
        <br />
        
        <div class="row">
                            <label id="Label5" class="col-md-1 col-md-offset-4  control-label">
                                State</label>
                            <div class="col-sm-6 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:DropDownList ID="ddlstate" runat="server"  SkinID="ddlRequired" CssClass="form-control"
                                        Style="min-width: 100px" Width="150">
                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
            <div class="row">
            <label id="lblFYear" class="col-md-1 col-md-offset-4  control-label">
                Year</label>
                  <div class="col-sm-6 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>                         
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control" Style="min-width: 100px" Width="150">
                        </asp:DropDownList>
                   </div>
                  </div>
                   </div>
        <br />
       
           <button type="button" class="btn" id="btnGo" style="background-color:#1a73e8;color:white;">View</button>
        <br />
    </center>
    <br />
    <center>
        <div class="container" style="width: 100%;">
            <div class="table-responsive" style="width: 93%">
                <table id="table1" class="table">
                    <thead>
                        <tr>
                           <th>
                                Sl.No.
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                JAN
                                <input type="hidden" name="months" value="1" />
                            </th>
                            <th>
                                FEB
                                <input type="hidden" name="months" value="2" />
                            </th>
                            <th>
                                MAR
                                <input type="hidden" name="months" value="3" />
                            </th>
                            <th>
                                APR
                                <input type="hidden" name="months" value="4" />
                            </th>
                            <th>
                                MAY
                                <input type="hidden" name="months" value="5" />
                            </th>
                            <th>
                                JUN
                                <input type="hidden" name="months" value="6" />
                            </th>
                            <th>
                                JUL
                                <input type="hidden" name="months" value="7" />
                            </th>
                            <th>
                                AUG
                                <input type="hidden" name="months" value="8" />
                            </th>
                            <th>
                                SEP
                                <input type="hidden" name="months" value="9" />
                            </th>
                            <th>
                                OCT
                                <input type="hidden" name="months" value="10" />
                            </th>
                            <th>
                                NOV
                                <input type="hidden" name="months" value="11" />
                            </th>
                            <th>
                                DEC
                                <input type="hidden" name="months"  value="12" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <button type="button" id="btnsave" class="btn btn-primary" style="width: 100px;">
            Save
        </button>
        </div>
        
    </center>
        </form>
</asp:Content>
