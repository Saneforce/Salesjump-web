<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_distinct_skudetail.aspx.cs" Inherits="SuperStockist_Reports_Sales_distinct_skudetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <title>SKU Wise Product</title>

        <style type="text/css">
            .form-group {
                position: relative;
                margin-bottom: 30px;
            }

            label {
                position: absolute;
                top: 10;
                left: 0;
                font-size: 16px;
                color: #888;
                transform-origin: left top;
                /*pointer-events: none;*/
                transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
            }

            .form-controls {
                border: none !important;
                border-radius: 0 !important;
                box-shadow: none !important;
                background-color: transparent !important;
                font-size: 18px !important;
                padding: 0 !important;
                /*padding-bottom: 10px !important;*/
                margin-top: 30px !important;
                width: 100% !important;
                height: auto !important;
                transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1) !important;
            }

            .underline {
                position: absolute;
                bottom: 0;
                left: 0;
                height: 1px;
                width: 100%;
                background-color: #888;
                transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
            }

            .form-control:focus {
                outline: none;
            }

                .form-control:focus ~ label,
                .form-control:valid ~ label {
                    transform: translateY(-40%) scale(0.8);
                    color: #333;
                }

                .form-control:focus ~ .underline,
                .form-control:valid ~ .underline {
                    transform: scaleX(1);
                    background-color: #333;
                }

            /* Animation for label */
            label.active {
                transform: translateY(-40%) scale(0.8);
                color: #333;
            }

            /* Animation for underline */
            .underline.active {
                transform: scaleX(1);
                background-color: #333;
            }

            .rowdiv {
                border: 1px solid lightgray;
                border-radius: 10px;
                background: azure;
                padding-top: 9px;
            }

            .export-button {
                width: 35px;
                height: 42px;
                //background-color: #4CAF50; /* Green */
                border: none;
                color: transparent; /* Make the text color transparent */
                padding: 15px 32px;
                text-align: center;
                text-decoration: none;
                display: inline-block;
                font-size: 16px;
                margin: 4px 2px;
                cursor: pointer;
                float: right;
                margin-right: 10px;
                background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAACXBIWXMAAAsTAAALEwEAmpwYAAACcklEQVR4nGNgGAWjYBSMgmEJDOsNpfRqDX11aw0bdGsNN+vWGj5jGIzAvt6eRb9GX1uvxiBOt8Zwol6t0W69WsM3erVG/9HxQLuVQb3Uilev2thGt9YoDeRY3RrDI3o1ht+xOXbAPaBboSsIdmyNUb5ujdEi3Rqjq3o1Rn+JdeyAe0CPAofiwj7rA8jC3uv9jwxpD/isDyA99kY9UDsaA/8HXRJCBjCH4QOjHtBDCr3Obd0ooVO+qgpF3q7d6f+XH1/g8j07+gZXDBjWm/6/9eI23PC7r+7+N6gzgcsvPb4cLnfn5Z3/Rg1mg68YTZmfjhJCuUsKwOIevT7/f/7+CRb79+/f/+R5aYO3Hth7bT/cA5ceXwaLbbmwFS62+cKWwZ2Jvfp8//+AhjYIlKws///3318w+9P3T/8dO10Gtwf0ao3+zzowB27Jx28f4ez2LZ1Doxg1b7b+//LjSxTLrj+78d+gHpGpB20e0Ks1+m/VaofhgWfvn/03a7IaGh5YfXot3OGvP7+Bs2funz34k1Di3BRwUQkC3399/5+3tBBuIago9Z0QOHg9YNJo8f/uq3twC+YcnAcWP3r7GFzs2J0Tg9cDMw/MRil9bNscwOKhUyPgRSkIlK6sGHx5IGhy6P9ff37hbOvsvLwLJV+AMvqg8YBBncn/8w8vwB0IKoFMmyxRHTch4P+fv3/gahYfWzr4kpAehXjUAwMdA4OmItMb9UDAaAwMqSTkvd7/MMOIG9zFNbyuW2tkDJsLGNTD6+RMcOjVGnWAZmMG7QQHCYBRq95QRbfOOFS31rBNr8Zom16N4XNSDBgFo2AUjAKGIQEAkqNB3aFhJ4wAAAAASUVORK5CYII=);
                background-size: contain; /* Scale the icon to fit the button */
                background-repeat: no-repeat; /* Do not repeat the icon */
                background-position: center; /* Center the icon within the button */
                transition: transform 0.2s; /* Add a transition effect */
            }

                .export-button:hover {
                    transform: scale(1.2); /* Scale up the button on hover */
                }

            .spinner {
                margin: 100px auto;
                width: 50px;
                height: 40px;
                text-align: center;
                font-size: 10px;
            }

                .spinner > div {
                    background-color: #333;
                    height: 100%;
                    width: 6px;
                    display: inline-block;
                    -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                    animation: sk-stretchdelay 1.2s infinite ease-in-out;
                }

                .spinner .rect2 {
                    -webkit-animation-delay: -1.1s;
                    animation-delay: -1.1s;
                }

                .spinner .rect3 {
                    -webkit-animation-delay: -1.0s;
                    animation-delay: -1.0s;
                }

                .spinner .rect4 {
                    -webkit-animation-delay: -0.9s;
                    animation-delay: -0.9s;
                }

                .spinner .rect5 {
                    -webkit-animation-delay: -0.8s;
                    animation-delay: -0.8s;
                }

            @-webkit-keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    -webkit-transform: scaleY(1.0);
                }
            }

            @keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    transform: scaleY(0.4);
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    transform: scaleY(1.0);
                    -webkit-transform: scaleY(1.0);
                }
            }

            .spinnner_div {
                width: 1200px;
                height: 1000px;
                background: rgba(255, 255, 255, 0.1);
                backdrop-filter: blur(2px);
                position: absolute;
                z-index: 100;
                overflow-y: hidden;
            }

            .table-container {
                position: relative;
                overflow-x: scroll;
                overflow-y: auto;
            }

            table {
                /*border-collapse: collapse;*/
                width: 100%;
                border-collapse: separate
                /*table-layout: fixed;*/
            }

            thead {
                position: sticky;
                top: 0;
                background-color: #fff;
            }

            th:first-child {
                position: sticky;
                left: 0;
                z-index: 1;
                padding: 22px;
                height:68px;
                background-color: #f0ffff !important;
                border-color: #00c0ef !important;
                margin: 0;
                margin-top: 1px;
            }



            th {
                background-color: #f0ffff !important;
                border-bottom: 0.5px solid #19a4c6 !important;
                border-top: 0.5px solid #19a4c6 !important;
            }

            td {
                border-bottom: 0.5px solid #00c0ef;
            }

            tbody td:first-child {
                position: sticky;
                left: 0;
                border-right: 2px solid #00c0ef !important;
                z-index: 1;
                background-color: #f0ffff;
            }

            .green-row {
                background-color: #b0ff9c;
            }
        </style>
    </head>

    <body>
        <script language="javascript" type="text/javascript">
            var fdt = '';
            var tdt = '';
            var itms = [];
            sf = '';
            sf = '<%=Session["Sf_Code"]%>';
            sf_type = '<%=Session["sf_type"]%>';
            $(document).ready(function () {
                $('.form-control').on('focus', function () {
                    $(this).siblings('label').addClass('active');
                    $(this).siblings('.underline').addClass('active');
                }).on('blur', function () {
                    if (!this.value) {
                        $(this).siblings('label').removeClass('active');
                        $(this).siblings('.underline').removeClass('active');
                    }
                });
                if (sf_type != '4') {
                    $('#distselect').show();
                    loaddist();
                }
            });
            function chkinp() {
                var valid = true;
                var fdate = $('#fdate').val();
                if (fdate == '') {
                    alert('Select the From Date');
                    valid = false;
                    return false;
                }
                var tdate = $('#tdate').val();
                if (tdate == '') {
                    alert('Select the To Date');
                    valid = false;
                    return false;
                }
                if (sf_type != '4') {
                    sf = $('#ddl_dist').val();
                    if (sf == '0') {
                        alert("Select Distributor");
                        focus($('#ddl_dist'));
                        return false;
                    }
                }

                $('#<%=hfdt.ClientID%>').val(fdate);
                $('#<%=htdt.ClientID%>').val(tdate);
                $('#<%=stk_code.ClientID%>').val(sf);
                return valid;
            }
            function chkstckinp() {
                var valid = true;
                var fdate = $('#fdate').val();
                if (fdate == '') {
                    alert('Select the From Date');
                    valid = false;
                    return false;
                }
                var tdate = $('#tdate').val();
                if (tdate == '') {
                    alert('Select the To Date');
                    valid = false;
                    return false;
                }
                if (sf_type != '4') {
                    sf = $('#ddl_dist').val();
                    if (sf == '0') {
                        alert("Select Distributor");
                        focus($('#ddl_dist'));
                        return false;
                    }
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SS_distinct_skudetail.aspx/VIEW_STOCK",
                    data: "{'div_code':'<%=Session["Division_Code"]%>','sf_code':'<%=Session["Sf_Code"]%>','hfdt':'" + fdate + "','htdt':'" + tdate + "'}",
                    dataType: "json",
                    success: function (data) {
                        var Order = JSON.parse(data.d) || [];
                        if (Order.length > 0) {
                            var table = $('#skutbl');
                            table.find('tr').remove();
                            var headerRow = '<thead><tr>';
                            $.each(Order[0], function (key, value) {

                                if (key === Object.keys(Order[0])[0])
                                    headerRow += '<th class="card">' + key + '</th>';
                                else
                                    headerRow += '<th>' + key + '</th>';
                            });
                            headerRow += "</tr></thead>";
                            table.append(headerRow);
                            var row = '<tbody>';
                            for (var i = 0; i < Order.length; i++) {
                                row = '<tr>';
                                hasValue = false;
                                $.each(Order[i], function (key, value) {

                                    if (key === Object.keys(Order[i])[0])
                                        row += '<td class="card" style="margin: 3px; margin-left: 0px;">' + value + '</td>';
                                    else {
                                       
                                        if (value > 0) {
                                            hasValue = true;
                                            row += '<td>' + value + '</td>';
                                        }
                                        else {
                                            row += '<td>' + 'Nil' + '</td>';
                                        }
                                    }
                                });
                                row += "</tr>";
                                table.append(row);
                                if (hasValue) {
                                    table.find("tr:last").addClass("green-row");
                                }
                            }

                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            }
            function loaddist() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SS_distinct_skudetail.aspx/binddistributor",
                    data: "{'sf_code':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        itms = JSON.parse(data.d) || [];
                        for (var i = 0; i < itms.length; i++) {
                            $('#ddl_dist').append($("<option></option>").val(itms[i].Stockist_Code).html(itms[i].Stockist_Name + '-' + itms[i].ERP_Code)).trigger('chosen:updated').css("width", "100%");
                        }
                        //$('#ddl_dist').selectpicker({
                        //    liveSearch: true
                        //});
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

        </script>
        <script type="text/javascript">

            var Details_array = [];


            $(document).ready(function () {


            });

        </script>
        <script type="text/javascript">

</script>
        <%-- <div >
            <p> Product Wise Report </p>
        </div>--%>
        <%-- loading div --%>
        <form id="form1" runat="server">
            <asp:HiddenField runat="server" ID="hfdt" />
            <asp:HiddenField runat="server" ID="htdt" />
            <asp:HiddenField runat="server" ID="stk_code" />
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left: 438px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <%-- loading div --%>
            <fieldset>
                <div class="row rowdiv">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label for="inputField">From Date</label>
                            <input type="date" class="form-control form-controls" id="fdate" autocomplete="off" />
                            <div class="underline"></div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label for="inputField">To Date</label>
                            <input type="date" class="form-control form-controls" id="tdate" autocomplete="off" />
                            <div class="underline"></div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">

                        <input type="button" style="margin-top: 38px;" class="form-control btn-primary btnview" id="btn" value="Search" onclick="chkstckinp();" />
                        <%--<asp:Button runat="server" ID="exceldld" CssClass="form-control btn-primary" BackColor="#1a73e8" ForeColor="White" Text="Excel" OnClientClick="javascript: return chkinp()" OnClick="exceldld_Click" />--%>
                    </div> 
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">

                        <%--<input type="button" style="margin-top: 31px;" class="form-control btn-primary btnview" id="btn" value="Search" onclick="chkstckinp();" />--%>
                        <asp:Button runat="server"  style="margin-top: 38px;" ID="exceldld" CssClass="form-control btn-primary" BackColor="#1a73e8" ForeColor="White" Text="Excel" OnClientClick="javascript: return chkinp()" OnClick="exceldld_Click" />
                    </div>

                </div>
                <legend>SKU Wise Product </legend>
            </fieldset>
            <div class="row">
                <button class="export-button" onclick="exportToExcel()" style="display: none;">Export to Excel</button>
            </div>
            <div id="div_id" style="margin-right: -15px; margin-left: -15px; margin-top: 10px; background: white; border: 1px solid lightgrey; background: white; border-radius: 10px; padding: 5px;">
                <div class="table-container">
                    <table id="skutbl">
                    </table>
                </div>


            </div>
        </form>
    </body>
    </html>
</asp:Content>

