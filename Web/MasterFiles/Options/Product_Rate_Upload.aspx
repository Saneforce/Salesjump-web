<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_Rate_Upload.aspx.cs" Inherits="MasterFiles_Options_Product_Rate_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />


    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

       <%-- <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>--%>
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
                        var route = $("[id*=ddlrt]");
                        route.empty().append('<option selected="selected" value="0">Please select</option>');
                        for (var i = 0; i < data.d.length; i++) {
                            route.append($('<option value="' + data.d[i].stCode + '">' + data.d[i].stName + '</option>'));
                        };
                    }
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });
        }

        function fillh() {
            var stc = $("#ddlrt").find(":selected").val();
            var stn = $("#ddlrt").find(":selected").text();
            $('#<%=hstc.ClientID%>').val(stc);
            $('#<%=hstn.ClientID%>').val(stn);
        }

        $(document).ready(function () {
            genState();

            //$('.stOnly .all').click(function (e) {
            //    e.stopPropagation();
            //    var $this = $(this);
            //    if ($this.is(":checked")) {
            //        $('#state').find("[type=checkbox]").prop("checked", true);
            //    }
            //    else {
            //        $('#state').find("[type=checkbox]").prop("checked", false);
            //        $this.prop("checked", false);
            //    }
            //    fillh();
            //});
            //$('.stOnly .list-group a .chk').click(function (e) {
            //    e.stopPropagation();
            //    var $this = $(this);
            //    if ($this.is(":checked")) {
            //        $this.prop("checked", true);
            //    }
            //    else {
            //        $this.prop("checked", false);
            //    }
            //    if ($this.hasClass("all")) {
            //        $this.trigger('click');
            //    }
            $('select[name="ddlrt"]').change(function () {
                fillh();
            });
            });

    </script>
    <style type="text/css">
        .content{
            background-color:#ffffff;
        }
        .list-group {
            max-height: 250px;
            min-height: 250px;
            width: 95%;
            margin-bottom: 10px;
            overflow: scroll;
            -webkit-overflow-scrolling: touch;
        }

        input[type='text'], select, label {
            line-height: 22px;
            padding: 4px 6px;
            font-size: small;
            border-radius: 5px;
            width: 27%;
        }
    </style>
    <form id="frm1" runat="server">
        <asp:HiddenField ID="hstc" runat="server" />
        <asp:HiddenField ID="hstn" runat="server" />
        <%--<div class="col-md-5  stOnly">
            <a href="#" class="list-group-item active state" style="width: 95%;">States<input title="toggle all" type="checkbox" class="all pull-right" /></a>
            <div class="list-group" id="state" style="border: 1px solid #ddd">
            </div>
        </div>--%>
        <center>
            <div class="row">
                <asp:Label ID="lblst" runat="server" Text="State" style="margin-right: 20px;"></asp:Label>
                <select id="ddlrt" name="ddlrt"></select>
                <asp:DropDownList CssClass="selectpicker select" ID="ddlst" runat="server" Visible="false"></asp:DropDownList>
            </div>
            <div class="row" style="margin-top:10px;">
                <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px;position:absolute" />
                <asp:Button ID="Upldbt" CssClass="btn btn-primary" runat="server" Text="Excel File" OnClick="lnkDownload_Click" />
                <asp:Button ID="upbt" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="upbt_Click" />
            </div>
        </center>
    </form>
</asp:Content>

