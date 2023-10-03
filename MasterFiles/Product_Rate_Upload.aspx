<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_Rate_Upload.aspx.cs" Inherits="MasterFiles_Options_Product_Rate_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />

    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />



    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script type="text/javascript">
        var sDt = [];
        function genState() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Product_Rate_Upload.aspx/getState",
                dataType: "json",
                success: function (data) {
                    sDt = data.d;
                    //  console.log(sDt);
                    if (sDt.length > 0) {
                        var div = $('#state');
                        for (var i = 0; i < sDt.length; i++) {
                            str = '<a href="#" class="list-group-item" id="' + sDt[i].stName + '">' + sDt[i].stName + '<input type="checkbox" name=' + sDt[i].stCode + ' class="chk pull-right"/></a>';
                            $(div).append(str);
                        }
                    }
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });
        }

        function fillh() {
            var items = $("#state input:checked");
            var stc = '';
            for (var i = 0; i < items.length; i++) {
                stc += items[i].name + ',';
            }
            $('#<%=hstc.ClientID%>').val(stc);

            var items1 = $("#state input:checked").parent();
            var stc1 = '';
            for (var i = 0; i < items1.length; i++) {
                stc1 += items1[i].text + ',';
            }
            $('#<%=hstn.ClientID%>').val(stc1);
        }

        $(document).ready(function () {
            genState();

            $('.stOnly .all').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#state').find("[type=checkbox]").prop("checked", true);
                }
                else {
                    $('#state').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                }
                fillh();
            });
                $('.stOnly .list-group a .chk').click(function (e) {
                    e.stopPropagation();
                    var $this = $(this);
                    if ($this.is(":checked")) {
                        $this.prop("checked", true);
                    }
                    else {
                        $this.prop("checked", false);
                    }
                    if ($this.hasClass("all")) {
                        $this.trigger('click');
                    }
                    fillh();

                });
        });

    </script>
    <style type="text/css">
        .list-group {
            max-height: 250px;
            min-height: 250px;
            width: 95%;
            margin-bottom: 10px;
            overflow: scroll;
            -webkit-overflow-scrolling: touch;
        }
    </style>
    <form id="frm1" runat="server">
        <asp:HiddenField ID="hstc" runat="server" />
        <asp:HiddenField ID="hstn" runat="server" />
        <div class="col-md-5  stOnly">
            <a href="#" class="list-group-item active state" style="width: 95%;">States<input title="toggle all" type="checkbox" class="all pull-right" /></a>
            <div class="list-group" id="state" style="border: 1px solid #ddd">
            </div>
        </div>
        <div class="row">
            <asp:FileUpload ID="FlUploadcsv" runat="server" style="padding-left:20px;"  />
            <asp:Button ID="Upldbt" runat="server" Text="Excel File" OnClick="lnkDownload_Click" />
            <asp:Button ID="upbt" runat="server" Text="Upload" OnClick="upbt_Click" />
        </div>
    </form>
</asp:Content>

