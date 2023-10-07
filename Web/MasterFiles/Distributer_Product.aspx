<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Distributer_Product.aspx.cs" Inherits="MasterFiles_Distributer_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <link href="../css/ScrollTabla.css" rel="stylesheet" media="screen" />
        <link type="text/css" rel="stylesheet" href="../css/style.css" />
		
		<style type="text/css">
            #Product_Table th {
                position: sticky;
                top: 0;
            }
        </style>
          <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script type="text/javascript" src="../js/jquery.CongelarFilaColumna.js"></script>
        <script type="text/javascript">

            jQuery(document).ready(function ($) {
                $('#btnsave').css('visibility', 'hidden');				
                if(<%=sf_type%>=="2"){
                    $('.subdivrow').hide();
                }
                $('#<%=ddlFieldForce.ClientID%>').change(function () {
                    var di = $('.ContenedorTabla');
                    //                    $(di).find('tabel').remove();
                    $(di).empty();
                    $('#btnsave').css('visibility', 'hidden');
                });
             
                $(document).on('click', '#btnview', function () {
                    var rDts = [];
                    var pDts = [];
                    var dis = "";
                    var distr = ""; 
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
                            url: "Distributer_Product.aspx/getdata",
                            data: "{'data':'" + $('#<%=subdiv.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                len = data.d.length;
                                pDts = data.d;
                                var str = 0;

                                if (data.d.length > 0) {
                                    str = '<th class="celda_encabezado_general" style="min-width: 100px; min-height:100px;" > <p class="phh">Distributer </p> </th>';
                                    for (var i = 0; i < data.d.length; i++) {
                                        str += '<th class="celda_encabezado_general" style="min-width: 100px; min-height:49px"> <input type="hidden" class="proid" name="pcode" value="' + data.d[i].product_id + '"/> <p class="phh" name="pname">' + data.d[i].product_name + '</p> </th>';
                                    }
                                    $('#Product_Table thead').append('<tr>' + str + '</tr>');
                                    // console.log($('#Product_Table thead tr').length);
                                }
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                        var div_code = ($('#<%=subdiv.ClientID%>').val() == 0) ? ("<%=Session["div_code"].ToString()%>") : $('#<%=subdiv.ClientID%>').val();

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Distributer_Product.aspx/getqnty",
                            data: "{'div':'<%=Session["div_code"].ToString()%>','sfcode':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "','Mon':'" + $('#<%=ddlFMonth.ClientID%>').val() + "','year':'" + $('#<%=ddlFYear.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                rDts = data.d;
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                        var rt1 = 0;
                        var rt = 0;
                        var str1 = "";
                      
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Distributer_Product.aspx/getfo",
                            data: "{'subdiv':'" + $('#<%=subdiv.ClientID%>').val() + "','sfcode':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "'}",
                            dataType: "json",
                            success: function (data) {

                                distr = data.d.filter(function (obj) {
                                    if (("," + dis + ",").indexOf("," + obj.Stockist_Name + ",") < 0) {
                                        dis += obj.Stockist_Name + ",";
                                        return true;
                                    }
                                });

                                if (dis.length > 0) {
                                    for (var i = 0; i < distr.length; i++) {
                                        str1 += '<td  class="celda_normal" style="min-width: 100px;" > <input type="hidden" name="stcode" value="' + distr[i].Stockist_code + '"/> <p class="phh" name="sfname">' + distr[i].Stockist_Name + '</p> </td>';
                                        //str1 += '<td>' + data.d[i].Stockist_Name + '</td><td><input type="hidden" id="stcode" value="' + data.d[i].Stockist_code + '"></td>';
                                        for (var j = 0; j < pDts.length; j++) {
                                            rt = "";
                                            rt1 = ""
                                            rr = rDts.filter(function (obj) {
                                                return (obj.Stockist_code === data.d[i].Stockist_code) && (obj.Product_code === pDts[j].product_id);

                                            });
                                            if (rr.length > 0) {
                                                rt = rr[0].Target_Qnty
                                                rt1 = rr[0].Trans_target_detail
                                            }
                                            //  str1 += '<td  class="celda_normal" style="min-width: 100px; "><input type="hidden" name="mRate" value="' + rt + '"/><input type="text" name="target"/> </td>';
                                            str1 += '<td  class="celda_normal" style="min-width: 100px; "><input type="hidden" name="detailno" value="' + rt1 + '"/><input type="text" class="proqnty" name="target" value="' + rt + '" /> </td>';
                                        }
                                        $('#Product_Table tbody').append('<tr>' + str1 + '</tr>');
                                        str1 = "";
                                    }
                                    //$('#Product_Table tbody').append('<tr>' + str1 + '</tr>');               
                                }
                                $('#Product_Table tbody').css('visibility', 'visible');
                                //$("#Product_Table").CongelarFilaColumna({ lboHoverTr: true });
                                $('#btnsave').css('visibility', 'visible');
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                
                        $(document).on('click', '#btnsave', function () {
                            var p_code = '';
                            var t_qty = 0;
                            var distri = '';
                            var arr = [];
                            //  var div_code = ("<%=Session["div_code"].ToString()%>");\

                            $('#Product_Table').each(function () {
                                if (dis.length > 0) {
                                    for (var i = 0; i < distr.length; i++) {
                                        for (var j = 1; j < pDts.length; j++) {
                                            if (Number($(this).find('tbody').find('tr').eq(i).find('td').eq(j).find("input[name='target']").val() || 0) > 0) {
                                                arr.push({
                                                    P_code: $(this).find('thead').find('th').eq(j).find("input[name='pcode']").val(),
                                                    St_code: $(this).find('tbody').find('tr').eq(i).find('td').eq(0).find("input[name='stcode']").val(),
                                                    T_Qnty: $(this).find('tbody').find('tr').eq(i).find('td').eq(j).find("input[name='target']").val(),
                                                    // T_detail_no:Number($(this).find('tbody').find('tr').eq(i).find('td').eq(j).find("input[name='detailno']").val()||0),
                                                    //  div: ($('#<%=subdiv.ClientID%>').val() == 0) ? div_code : $('#<%=ddlFieldForce.ClientID%>').val(),
                                                    // Sf_code: $('#<%=ddlFieldForce.ClientID%>').val(),
                                                    // year: $('#<%=ddlFYear.ClientID%>').val(),
                                                    // Mon: $('#<%=ddlFMonth.ClientID%>').val(),
                                                });
                                            }
                                        }
                                    }
                                }
                            });
                      
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "Distributer_Product.aspx/inserttarget",
                                data: "{'target':'" + JSON.stringify(arr) + "','div':'" + ("<%=Session["div_code"].ToString()%>") + "', 'Sf_code':'" + $('#<%=ddlFieldForce.ClientID%>').val() + "','year':'" + $('#<%=ddlFYear.ClientID%>').val() + "','Mon':'" + $('#<%=ddlFMonth.ClientID%>').val() + "'}",
                              //  data: "{'target':'" + JSON.stringify(arr) + "','div':'"+ $('#<%=subdiv.ClientID%>').val()+"', 'Sf_code':'"+ $('#<%=ddlFieldForce.ClientID%>').val()+"','year':'"+ $('#<%=ddlFYear.ClientID%>').val()+"','Mon':'"+$('#<%=ddlFMonth.ClientID%>').val()+"''}",
                                dataType: "json",
                                success: function (data) {
                                  if (data.d = "updated") {
                                        alert("Primary Target updated Successfully");
                                    }
                                    else {
                                        alert("Primary Target updation Unsuccesful");
                                    }
                                   
                                },
                                error: function (result) {
                                    alert(JSON.stringify(result));
                                }
                            });
                        });
                    
                    }
                });

                $(document).on('keypress', 'input[name=target]', function (e) {
                    var num = e.keyCode;
                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
                        return false;
                    }
                });
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
            });

                          
        </script>
         </head>
           <body>
         <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <div class="subdivrow row">
                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Sub Division" Style="text-align: right;
                        padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                               AutoPostBack="true">
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
		<div class="ContenedorTabla" style="max-width:100%; width:95%"></div></main>
            
                    
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
                 

                  </body>
    </html>
</asp:Content>

