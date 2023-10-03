<%@ Page Title="Product Target Fixation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Product_Target_Achive.aspx.cs" Inherits="MasterFiles_Product_Target_Achive"
    EnableEventValidation="false" %>

<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <link href="../css/ScrollTabla.css" rel="stylesheet" media="screen" />
        <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <style type="text/css">
            .modal
            {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }
            .loading
            {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }
            
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
            
            
            .ddl
            {
                border: 1px solid #1E90FF;
                border-radius: 4px;
                margin: 2px;
                font-family: Andalus;
                background-image: url('css/download%20(2).png');
                background-position: 88px;
                background-position: 88px;
                background-repeat: no-repeat;
                text-indent: 0.01px; /*In Firefox*/
            }
            .ddl1
            {
                border: 1px solid #1E90FF;
                border-radius: 4px;
                margin: 2px;
                background-position: 88px;
                background-position: 88px;
                background-repeat: no-repeat;
                text-indent: 0.01px; /*In Firefox*/
            }
            input[type=text]
            {
                width: 100%;
                padding: 5px 5px;
                margin: 0px 0;
                display: inline-block;
                border: 1px solid #ccc;
                border-radius: 4px;
                box-sizing: border-box;
                font-size: medium;
                text-align: right;
            }
            .week_table
            {
                width: 100%;
                background-color: #F0F8FF;
                margin: 0px 0px 0px 0px;
                border: solid 1px #525252;
                border-collapse: collapse;
            }
        </style>
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
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Division" Style="text-align: right;
                        padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                                OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force" Style="text-align: right;
                        padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                Width="350" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Style="text-align: right;
                        padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control" Width="120">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month" Style="text-align: right;
                        padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:DropDownList ID="ddlFMonth" runat="server" Width="120" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <button id="btnview" type="button" class="btn btn-primary" style="vertical-align: middle">
                            View</button>
                    </div>
                </div>
            </div>
        </div>
        <main class="main">
		<div class="ContenedorTabla" style="max-width:100%; width:95%">
                       
                    </div>
                    	</main>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <button name="Button1" type="button" id="btnsave" class="btn btn-primary">
                            Save</button>
                    </div>
                </div>
            </div>
        </div>
        </form>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script type="text/javascript" src="../js/jquery.CongelarFilaColumna.js"></script>
        <script type="text/javascript">

            jQuery(document).ready(function ($) {
                $('#btnsave').css('visibility', 'hidden');

                $('#<%=ddlFieldForce.ClientID%>').change(function () {
                    var di = $('.ContenedorTabla');
                    //                    $(di).find('tabel').remove();
                    $(di).empty();
                    $('#btnsave').css('visibility', 'hidden');
                });


                $(document).on('click', '#btnview', function () {
                    var rDts = [];
                    var pDts = [];
                    if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") { alert("Select FieldForce."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
                    if ($('#<%=ddlFieldForce.ClientID%>').val().toString() != "---Select Field Force---") {
                        var di = $('.ContenedorTabla');
                        $(di).empty();
                        var htm = '<table id="Product_Table"> <thead></thead><tbody></tbody> </table>';

                        $(di).append(htm);

                        var len = 0;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Product_Target_Achive.aspx/getdata",
                            data: "{'data':'" + $('#<%=subdiv.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                len = data.d.length;
                                pDts = data.d;

                                if (data.d.length > 0) {
                                    h1 = $('#fht-table  thead th').height();

                                    str = '<th class="celda_encabezado_general" style="min-width: 100px; min-height:' + h1 + '" > <p class="phh">Field Force </p> </th><th class="celda_encabezado_general" style="min-width: 100px;">Target</th>';
                                    for (var i = 0; i < data.d.length; i++) {
                                        str += '<th class="celda_encabezado_general" style="min-width: 100px; min-height:49px"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p class="phh" name="pname">' + data.d[i].product_name + '</p> </th>';
                                    }

                                    $('#Product_Table thead').append('<tr>' + str + '</tr>');
                                    // console.log($('#Product_Table thead tr').length);
                                }
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });



                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Product_Target_Achive.aspx/getrates",
                            data: "{'data':'" + $('#<%=subdiv.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                rDts = data.d;
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Product_Target_Achive.aspx/getfo",
                            data: "{'term':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    //   console.log(pDts);
                                    for (var i = 0; i < data.d.length; i++) {
                                        str = "";
                                        str = '<td  class="celda_normal" style="min-width: 100px;" > <input type="hidden" name="sfcode" value="' + data.d[i].sf_code + '"/> <p class="phh" name="sfname">' + data.d[i].sf_name + '</p> </td><td  class="celda_normal" style="min-width: 100px; "></td>';
                                        for (var j = 0; j < pDts.length; j++) {

                                            rr = rDts.filter(function (obj) {
                                                return (obj.stCode === data.d[i].stCode) && (obj.pCode === pDts[j].product_id);
                                            });

                                            var rt = 0;
                                            if (rr.length > 0) {
                                                rt = rr[0].mRate;
                                            }

                                            str += '<td  class="celda_normal" style="min-width: 100px; "><input type="hidden" name="mRate" value="' + rt + '"/><input type="text" name="target"/> </td>';
                                        }

                                        $('#Product_Table tbody').append('<tr>' + str + '</tr>');

                                    }
                                    $('#btnsave').css('visibility', 'visible');
                                }
                                $("#Product_Table").CongelarFilaColumna({ lboHoverTr: true });



                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                        //data: "{'term':'" + txt + "','mtable':'" + mtab + "','mfield':'" + mfie + "'}",

                        var dtls_tab = document.getElementById("Product_Table");
                        var nrows1 = dtls_tab.rows.length;
                        var Ncols = dtls_tab.rows[0].cells.length;

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Product_Target_Achive.aspx/gettarget",
                            data: "{'sf_code':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "','years':'" + $('#<%=ddlFYear.ClientID%>').val() + "','months':'" + $('#<%=ddlFMonth.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    $('#Product_Table tbody tr').each(function (index) {
                                        var fd = 1;
                                        var sum = 0;

                                        for (var i = 0; i < data.d.length; i++) {
                                            for (var col = 0; col < Ncols - 2; col++) {
                                                fd = col + 2;
                                                if ($(this).find('input[name=sfcode]').val().toLowerCase().toString() == data.d[i].sf_code.toLowerCase().toString()) {
                                                    if ($(this).closest('table').find('th').eq(fd).find('input[name=pcode]').val().toLowerCase().toString() == data.d[i].pcode.toLowerCase().toString()) {
                                                        $(this).children('td').eq(fd).find('input[name=target]').val(data.d[i].target);
                                                        $(this).children('td').eq(fd).find('input[name=target]').attr('pv', data.d[i].target);
                                                        sum += Number($(this).children('td').eq(fd).find('input[name=mRate]').val()) * Number(data.d[i].target);

                                                        //  console.log($(this).parent('.ContenedorTabla'));

                                                        //  console.log($('.fht-fixed-column').children('.fht-table').find('tr'));
                                                    }
                                                }
                                            }
                                        }
                                        // console.log($(this).children('td').eq(1));
                                        $(this).children('td').eq(1).text(sum.toFixed(2));
                                       // console.log(index);
                                        $('.fht-fixed-column').children('.fht-tbody').children('.fht-table').find('tr').eq(index).children('td').eq(1).text(sum.toFixed(2));
                                    });
                                }

                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                    }


                });
                //                $("input").live("keypress", function (e) {
                //                    var num = e.keyCode;
                //                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                //                        return false;
                //                    }
                //                });


                $(document).on('keypress', 'input[name=target]', function (e) {
                    var num = e.keyCode;
                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                        return false;
                    }
                });

                $(document).on('keyup', 'input[name=target]', function () {
                    var idx = $(this).closest('tr').index();

                    var rat = $(this).closest('td').find('input[name=mRate]').val();
                    var oldTot = $(this).closest('tr').children('td').eq(1).text();

                    $('.fht-fixed-column').children('.fht-table').find('tr:nth-child(' + idx + ')').children('td').eq(1).text(rat);
                    //                    $('.fht-fixed-column').children('.fht-tbody').children('.fht-table').find('tr').eq(idx).children('td').eq(1).text(rat)
                    var cTxt = $(this).val();
                    var t = parseFloat($(this).val());
                    if (isNaN(t)) t = 0;
                    var pv = parseFloat($(this).attr('pv'));
                    if (isNaN(pv)) pv = 0;
                    var tot = parseFloat($(this).closest('tr').children('td').eq(1).text());

                    if (isNaN(tot)) tot = 0;
                    tot = (tot - (pv * rat)) + Number(t * rat);
                    $(this).closest('tr').children('td').eq(1).text(tot.toFixed(2));
                    $('.fht-fixed-column').children('.fht-tbody').children('.fht-table').find('tr').eq(idx).children('td').eq(1).text(tot.toFixed(2));
                    $(this).attr('pv', t);
                });



                $(document).on('click', '#btnsave', function () {


                    var dtls_tab = document.getElementById("Product_Table");
                    var nrows1 = dtls_tab.rows.length;
                    var Ncols = dtls_tab.rows[0].cells.length;

                    if (nrows1 > 1) {
                        var ch = true;
                        var arr = [];
                        $('#Product_Table tbody tr').each(function () {
                            var fd = 1;
                            for (var i = 0; i < Ncols - 1; i++) {
                                fd = i + 1;
                                if (Number($(this).children('td').eq(fd).find('input[name=target]').val()) > 0) {
                                    arr.push({
                                        sf_code: $(this).children('td').eq(0).find('input[name=sfcode]').val(),
                                        target: $(this).children('td').eq(fd).find('input[name=target]').val(),
                                        rate: $(this).children('td').eq(fd).find('input[name=mRate]').val(),
                                        pcode: $(this).closest('table').find('th').eq(fd).find('input[name=pcode]').val(),
                                        months: $('#<%=ddlFMonth.ClientID%>').val(),
                                        years: $('#<%=ddlFYear.ClientID%>').val(),
                                        rsf: $('#<%=ddlFieldForce.ClientID%>').val()
                                    });
                                }
                            }
                        });

                        if (arr.length > 0) {

                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "Product_Target_Achive.aspx/savedata",
                                data: "{'data':'" + JSON.stringify(arr) + "'}",
                                dataType: "json",
                                success: function (data) {
                                    // alert(data.d);
                                    alert("Product Target has been updated successfully!!!");
                                    loaddata();
                                },
                                error: function (data) {
                                    alert(JSON.stringify(data));
                                }
                            });
                        }
                        else {
                            alert("No Row Enter Values..!");
                        }
                    }
                    else {
                        alert("Select New Field Force");
                    }

                });
            });
            function NewWindow() {
                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Field Force---") { alert("Select FieldForce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
                var ddlFieldForceValue = $('#<%=ddlFieldForce.ClientID%> :selected').val();
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
                var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();

            }

        </script>
    </body>
    </html>
</asp:Content>
