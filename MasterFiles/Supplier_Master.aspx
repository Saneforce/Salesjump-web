<%@ Page Title="Supplier_Master" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Supplier_Master.aspx.cs" Inherits="MasterFiles_Supplier_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .col-form-label
        {
            padding-top: calc(.5rem - 1px * 2);
            padding-bottom: calc(.5rem - 1px * 2);
            margin-bottom: 0;
            line-height: 28px;
            font-weight: normal;
        }
        .col-sm-2
        {
            -webkit-box-flex: 0;
            -webkit-flex: 0 0 16.666667%;
            -ms-flex: 0 0 16.666667%;
            flex: 0 0 16.666667%;
            max-width: 16.666667%;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('click', '#btnSave', function () {

                var sname = $('#Sup_Name').val();
                if (sname.length <= 0) { alert('Enter Supplier Name!!!'); $('#Sup_Name').focus(); return false }
                var scontact = $('#Sup_Contact').val();
                if (scontact.length <= 0) { alert('Enter Contact Person !!!'); $('#Sup_Contact').focus(); return false }
                var smobile = $('#Sup_Mobile').val();
                if (smobile.length <= 0) { alert('Enter Mobile Number!!!'); $('#Sup_Mobile').focus(); return false }
                var serp = $('#Sup_ERP').val();
                if (serp.length <= 0) { alert('Enter ERP COde!!!'); $('#Sup_ERP').focus(); return false }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Supplier_Master.aspx/SaveDate",
                    data: "{'SName':'" + sname + "','SContact':'" + scontact + "','SMobile':'" + smobile + "','SERP':'" + serp + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert("Supplier Master has been updated successfully!!!");
                        clear();
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });


            function clear() {
                $('#Sup_Name').val("");
                $('#Sup_Contact').val("");
                $('#Sup_Mobile').val("");
                $('#Sup_ERP').val("");
                $('#Sup_Name').focus();
            }
        });
    </script>
    <div class="container" style="display: block; width: 60%">
        <div class="form-group row">
            <label for="Sup_Name" class="col-sm-2 col-form-label">
                Supplier Name</label>
            <div class="col-sm-4">
                <input class="form-control" type="text" id="Sup_Name" />
            </div>
            <label for="Sup_Contact" class="col-sm-2 col-form-label">
                Contact Person</label>
            <div class="col-sm-4">
                <input class="form-control" type="text" id="Sup_Contact" />
            </div>
        </div>
        <div class="form-group row">
            <label for="Sup_Mobile" class="col-sm-2 col-form-label">
                Mobile No.</label>
            <div class="col-sm-4">
                <input class="form-control" type="text" id="Sup_Mobile" />
            </div>
            <label for="Sup_ERP" class="col-sm-2 col-form-label">
                ERP Code</label>
            <div class="col-sm-4">
                <input class="form-control" type="text" id="Sup_ERP" />
            </div>
        </div>
        <br />
        <div class="form-group row" style="text-align: center">
            <div class="col-sm-12">
                <a id="btnSave" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                    <span>Save</span></a>
            </div>
        </div>
    </div>
</asp:Content>
