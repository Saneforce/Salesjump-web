<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="prod_brand_cat_upload.aspx.cs" Inherits="MasterFiles_Options_prod_brand_cat_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <link href="/css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link href="/css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sDt = [];

        function genState() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "prod_brand_cat_upload.aspx/getDivision",
                dataType: "json",
                success: function (data) {
                    sDt = data.d;
                    if (sDt.length > 0) {
                        var route = $("[id*=ddlrt]");
                        route.empty();
                        for (var i = 0; i < data.d.length; i++) {
                            route.append($('<option value="' + data.d[i].stCode + '">' + data.d[i].stName + '</option>'));
                        };
                        //$('#ddlrt').multiselect({
                        //    columns: 4,
                        //    placeholder: 'Select Division IDs',
                        //    searchOptions: {
                        //        'default': 'Search Division IDs'
                        //    },
                        //    selectAll: true,
                        //    maxHeight: 20,
                        //}).multiselect("reload");

                        //$('.ms-options').css("width", "500px");
                        //$('.ms-options').css("height", "200px");
                        //$('.ms-options-wrap').css("width", "500px");

                    }
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });
        }

        function fillh() {
            var divc = $("#ddlrt").find(":selected").val();
            var divn = $("#ddlrt").find(":selected").text();
            $('#<%=hdivc.ClientID%>').val(divc);
            $('#<%=hdivn.ClientID%>').val(divn);
        }


        $(document).ready(function () {

            genState();

            var Ste = '';
            $("#<%=upbt.ClientID %>").click(function () {

                var selected_state_length = $("select#ddlrt option:selected").length;

                if (selected_state_length != '0') {

                    <%--$('#ddlrt option:selected').each(function () {
                        Ste += $(this).val() + ',';
                    });--%>
                    $('#<%=hdivc.ClientID%>').val($("#ddlrt").find(":selected").val());
                }
                else {
                    alert('Please Select State to Upload');
                    return false;
                }

                if (document.getElementById('<%= FlUploadcsv.ClientID %>').files.length == 0) {
                    alert('Please Select File to Upload');
                    return false;
                }
            });

            $('select[name="ddlrt"]').change(function () {
                fillh();
            });
        });
    </script>
     <style type="text/css">
        .content {
            background-color: #ffffff;
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
         <asp:HiddenField ID="hdivc" runat="server" />
        <asp:HiddenField ID="hdivn" runat="server" />
          <center>
            <div class="row">
                <asp:Label ID="lblst" runat="server" Text="Division" style="margin-right: 20px;"></asp:Label>
                <select id="ddlrt" name="ddlrt"></select>
            </div>
            <div class="row" style="margin-top:10px;">
                <asp:FileUpload ID="FlUploadcsv" runat="server" Style="padding-left: 20px;position:absolute" />
                <asp:Button ID="Upldbt" CssClass="btn btn-primary" runat="server" Text="Excel File" OnClick="lnkDownload_Click" />
                <asp:Button ID="upbt" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="upbt_Click" />
            </div>
        </center>
    </form>
</asp:Content>

