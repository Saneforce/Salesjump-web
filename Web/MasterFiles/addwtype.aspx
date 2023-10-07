<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="addwtype.aspx.cs" Inherits="MasterFiles_addwtype" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style type="text/css">
        table {
            border-collapse: separate;
            border-spacing: 9px;
            width: 100%;
        }

        .is-invalid {
            outline: none;
            box-shadow: 0 0 0 3px rgba(255, 0, 0, 0.4);
        }

        .scrldiv, .scrldiv2 {
            width: 545px;
            height: 116px;
        }

            .scrldiv input {
                margin-top: 10px;
            }

            .scrldiv2 input {
                margin-top: 10px;
            }

        label {
            font-size: 13px;
            font-family: 'Open Sans','arial';
            font-weight: 700;
            margin: 0px;
        }

        .panel-default {
            border-color: #ddd !important;
            border: 1px solid transparent;
        }

        input {
            width: 100%;
            border: 1px solid #d8d8d8 !important;
            padding: 6px 6px 7px !important;
            border-radius: 3px !important;
            line-height: inherit !important;
            background: #fff;
            box-shadow: 0px 3px 5px rgba(0, 0, 0, 0.1);
            transition: all 0.3s ease;
            /*border: 1px solid #00809D;*/
        }

            input:focus {
                outline: none;
                box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
            }

        select {
            width: 100%;
            border: 1px solid #D5D5D5 !important;
            padding: 6px 6px 7px !important;
        }

            select:focus {
                outline: none;
                box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
            }
    </style>
    <form id="form" runat="server">
        <div class="row">
            <div class="col-lg-12 sub-header">
                Work Type Creation
            </div>

        </div>
        <br /><br /><br /><br />

        <div class="row">
            <input type="hidden" id="wtcode" />
            <div class="col-sm-3">
                <label>Worktype Name</label>
            </div>
            <div class="col-sm-4">
                <input type="text" class="form-control autoc ui-autocomplete-input" id="txtwname" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <label>Work Type Shortname</label>
            </div>
            <div class="col-sm-4">
                <input type="text"id="txtsname"  style="width:250px"/>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <label>Field work Indicator</label>
            </div>
            <div class="col-sm-4">
                <input type="text"  id="fldname" style="width:250px" />
            </div>
        </div>
        </br>
        <div class="row">
            <div class="col-sm-3">
                <label>Access</label>
            </div>
            <div class="col-sm-2">
                <input type="checkbox" id="tp" name="tPD" value="T" style="width: 18px">
                <label for="Tourplan">TP</label>
                <input type="checkbox" id="dcr" name="tPD" value="D" style="width: 18px">
                <label for="Dcr">DCR</label><br>
                <br>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-3">
                <label>Place Involved</label>
            </div>
            <div class="col-sm-2">
                <input type="radio" id="yes" name="selection" value="Y" style="width: 18px" />
                <label for="yes">YES</label>
                <input type="radio" id="no" name="selection" value="N" style="width: 18px" />
                <label for="no">NO</label>
                <br>

                <br>
            </div>


        </div>


        <br />
        <div class="row">
            <button type="button" class="btn btn-primary" style="margin-top: 65px; margin-left: 259px;" id="btnsave">Save</button>
            <a href="worktypemaster.aspx" class="btn" style="margin-top: 65px; margin-left: 259px; background-color: #2f7db2; color: white;">Back</a>
        </div>
    </form>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

    <script type="text/javascript">
        var divcode;
        var ComDets = [];
        var wcode = '<%=wcode%>';


        function clearFields() {
            $('#txtwname').val('');
            $('#txtsname').val('');

        }
        function loaddetails() {
            var wcode = '<%=wcode%>';
            if (wcode != '') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "addwtype.aspx/getwtypdets",
                    data: "{'wcode':'" + wcode + "'}",
                    dataType: "json",
                    success: function (data) {
                        ComDets = data.d;
                        if (ComDets.length > 0) {


                            $('#txtwname').val(ComDets[0].wname);
                            $('#txtsname').val(ComDets[0].wsname);
                            $('#fldname').val(ComDets[0].fldi);
                            var tpdcr = (ComDets[0].races);
                            const aces = tpdcr.replaceAll(',', '');
                            const myArray = aces.split("");
                            for (var i = 0; i < myArray.length; i++) {
                                if ($('#tp').val() == myArray[i]) {
                                    $('#tp').attr('checked', true);
                                }
                                else if ($('#dcr').val() == myArray[i]) {
                                    $('#dcr').attr('checked', true);
                                }
                            }
                            var plces = (ComDets[0].rplac);
                            if ($('#yes').val() == plces) {
                                $('#yes').attr('checked', true);
                            }
                            else {
                                $('#no').attr('checked', true);
                            }
                        }
                    }
                });
            }
        }


        $(document).ready(function () {

            loaddetails();
            $('#btnsave').on('click', function () {

                var wnam = $('#txtwname').val();
                if (wnam == '') {
                    alert('Enter the Worktype Name');
                    return false;
                }
                var wsnam = $('#txtsname').val();
                if (wsnam == '') {
                    alert('Enter the Worktype Short Name');
                    return false;
                }
                var feldnam = $('#fldname').val();
                if (feldnam == '') {
                    alert('Enter the Field work indicator');
                    return false;
                }

                var checkboxes = document.querySelectorAll('input[name="tPD"]');
                var values = [];
                for (var i = 0; i < checkboxes.length; i++) {
                    if (checkboxes[i].checked == true) {
                        values.push(checkboxes[i].value);
                    }
                }
                let access = values.toString();
                if (access == '') {
                    alert('Select The Access');
                    return false;
                }
               var place= $('input[name="selection"]:checked').length
                if ($('input[name="selection"]:checked').length == 0) {
                    alert('Select the Place Involved Option');
                    return false;
                }
              var plac = document.querySelector('input[name="selection"]:checked').value;
                  

                divcode = <%=Session["div_code"]%>;

               

                if (wcode == '') {
                    data = { "Divcode": divcode, "wname": wnam, "wshnam": wsnam, "races": access, "rplac": plac,"fldi":feldnam }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "addwtype.aspx/savewtype",
                        data: "{'data':'" + JSON.stringify(data) + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
                            alert(data.d);
                            window.location.href = "worktypemaster.aspx";

                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
                else {
                    data = { "wcode": wcode, "Divcode": divcode, "wname": wnam, "wshnam": wsnam, "races": access, "rplac": plac,"fldi":feldnam }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "addwtype.aspx/updwtype",
                        data: "{'data':'" + JSON.stringify(data) + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
                            alert(data.d);
                            window.location.href = "worktypemaster.aspx";

                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
            });





        });
    </script>
</asp:Content>
