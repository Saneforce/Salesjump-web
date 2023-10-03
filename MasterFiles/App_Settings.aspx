<%@ Page Title="App Settings" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="App_Settings.aspx.cs" Inherits="MasterFiles_Tabctrl" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>App Settings</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="../js/jQuery-2.2.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //  $('#OrderVal').attr('checked',true);
            $('.tpneed').css('display', 'none');
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $("#<%=TP_Remainder.ClientID%>").val(today);
            $("#<%=TP_Date.ClientID%> ").val(today);


            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "App_Settings.aspx/getdata",
                dataType: "json",
                success: function (result) {
                   // console.log(result.d);
                    var name = result.d.toString(); ;
                    var names = name.split("|");

                    if (names[0] == 1) {
                        $('#UNLNeed').attr('checked', true);
                        $('#Requ').css('display', 'table-row');
                    }
                    else {
                        $('#Requ').css('display', 'none');
                    }
                    if (names[1] == 1)
                        $('#NwRoute').attr('checked', true);
                    if (names[2] == 1)
                        $('#StkNeed').attr('checked', true);
                    if (names[3] == 1)
                        $('#NwDist').attr('checked', true);
                    if (names[4] == 1)
                        $('#jointwork').attr('checked', true);
                    if (names[5] == 1)
                        $('#template').attr('checked', true);
                    if (names[6] == 1)
                        $('#CusOrder').attr('checked', true);
                    if (names[7] == 1)
                        $('#OrderVal').attr('checked', true);
                    if (names[8] == 1)
                        $('#NetweightVal').attr('checked', true);
                    $('#sms').val(names[9]);
                    if (names[10] == 1)
                        $('#DrSmpQ').attr('checked', true);
                    if (names[11] == 1)
                        $('#CollectedAmount').attr('checked', true);
                    if (names[12] == 1)
                        $('#recv').attr('checked', true);
                    if (names[13] == 1)
                        $('#closing').attr('checked', true);
                    if (names[14] == 1)
                        $('#OrdAsPrim').attr('checked', true);
                    if (names[15] == 1)
                        $('#GEOTagNeed').attr('checked', true);
                    $('#DisRad').val(names[16]);
                    if (names[17] == 1)
                        $('#geoTrack').attr('checked', true);
                    if (names[18] == 1)
                        $('#rcstk').attr('checked', true);
                    if (names[19] == 1)
                        $('#sredit').attr('checked', true);
                    if (names[20] == 1)
                        $('#orentry').attr('checked', true);
                    if (names[21] == 1)
                        $('#dlyinven').attr('checked', true);
                    if (names[22] == 1)
                        $('#esubcall').attr('checked', true);
                    //Mano

                    
                    if (names[25] == 1)
                        $('#DTDNeed').attr('checked', true);
                    if (names[26] == 1)
                        $('#InshopND').attr('checked', true);
                    if (names[27] == 1)
                        $('#DistBased').attr('checked', true);
                    if (names[28] == 1)
                        $('#msDate').attr('checked', true);

                    if (names[29] == 1)
                        $('#opbal').attr('checked', true);
                    if (names[30] == 1)
                        $('#clbal').attr('checked', true);
                    if (names[31] == 1)
                        $('#freeqty').attr('checked', true);
                    if (names[32] == 1)
                        $('#callpreview').attr('checked', true);
                    if (names[33] == 1)
                        $('#phOrder').attr('checked', true);
                    if (names[34] == 1)
                        $('#BattorySt').attr('checked', true);
                    if (names[35] == 1)
                        $('#geoTrackprimary').attr('checked', true);
                    $('#priceCat').val(names[36]);
                    if (names[37] == 1)
                        $('#Explorerneed').attr('checked', true);
                    $("textarea#Expkey").val(names[38]);
                    var ned = names[23].split(',');
                    if (ned.length > 0) {
                        for (var i = 0; i < ned.length; i++) {
                            if (ned[i] == 'gs') {
                                $('#gstNoD').attr('checked', true);
                            }
                            if (ned[i] == 'ad') {
                                $('#addressD').attr('checked', true);
                            }
                            if (ned[i] == 'ar') {
                                $('#areaD').attr('checked', true);
                            }
                            if (ned[i] == 'ct') {
                                $('#cityD').attr('checked', true);
                            }
                            if (ned[i] == 'ln') {
                                $('#landmarkD').attr('checked', true);
                            }
                            if (ned[i] == 'pc') {
                                $('#pinD').attr('checked', true);
                            }
                            if (ned[i] == 'cp') {
                                $('#contactD').attr('checked', true);
                            }
                            if (ned[i] == 'cp2') {
                                $('#contactD2').attr('checked', true);
                            }
                            if (ned[i] == 'ph') {
                                $('#phoneD').attr('checked', true);
                            }
                            if (ned[i] == 'ph2') {
                                $('#phoneD2').attr('checked', true);
                            }

                            if (ned[i] == 'dn') {
                                $('#designationD').attr('checked', true);
                            }
                            if (ned[i] == 'dn2') {
                                $('#designationD2').attr('checked', true);
                            }
							if (ned[i] == 'dob') {
                                $('#dobd').attr('checked', true);
                            }
                            if (ned[i] == 'doa') {
                                $('#doad').attr('checked', true);
                            }
							 if (ned[i] == 'visit') {
                                $('#visd').attr('checked', true);
                            }
							 if (ned[i] == 'layer') {
                                $('#layd').attr('checked', true);
                            }
                            if (ned[i] == 'breed') {
                                $('#bred').attr('checked', true);
                            }
                            if (ned[i] == 'broil') {
                                $('#broid').attr('checked', true);
                            }
                        }
                    }
                    ned = '';
                    ned = names[24].split(',');
                    if (ned.length > 0) {
                        for (var i = 0; i < ned.length; i++) {
                            if (ned[i] == 'gs') {
                                $('#gstNoM').attr('checked', true);
                            }
                            if (ned[i] == 'ad') {
                                $('#addressM').attr('checked', true);
                            }
                            if (ned[i] == 'ar') {
                                $('#areaM').attr('checked', true);
                            }
                            if (ned[i] == 'ct') {
                                $('#cityM').attr('checked', true);
                            }
                            if (ned[i] == 'ln') {
                                $('#landmarkM').attr('checked', true);
                            }
                            if (ned[i] == 'pc') {
                                $('#pinM').attr('checked', true);
                            }
                            if (ned[i] == 'cp') {
                                $('#contactM').attr('checked', true);
                            }
                            if (ned[i] == 'cp2') {
                                $('#contactM2').attr('checked', true);
                            }
                            if (ned[i] == 'ph') {
                                $('#phoneM').attr('checked', true);
                            }
                            if (ned[i] == 'ph2') {
                                $('#phoneM2').attr('checked', true);
                            }

                            if (ned[i] == 'dn') {
                                $('#designationM').attr('checked', true);
                            }
                            if (ned[i] == 'dn2') {
                                $('#designationM2').attr('checked', true);
                            }
							if (ned[i] == 'dob') {
                                $('#dobM').attr('checked', true);
                            }
                            if (ned[i] == 'doa') {
                                $('#doaM').attr('checked', true);
                            }
							if (ned[i] == 'visit') {
                                $('#visM').attr('checked', true);
                            }
							 if (ned[i] == 'layer') {
                                $('#layM').attr('checked', true);
                            }
                            if (ned[i] == 'breed') {
                                $('#breM').attr('checked', true);
                            }
                            if (ned[i] == 'broil') {
                                $('#broiM').attr('checked', true);
                            }
                        }
                    }

                    $('#mandis tr ').not(':first').each(function () {
                        var ch = $(this).find('td').eq(1).find('input[type=checkbox]').attr("checked") ? 1 : 0;
                        if (ch == 0) {
                            $(this).closest('tr').find('td').eq(2).find('input[type=checkbox]').attr('checked', false);
                            $(this).closest('tr').find('td').eq(2).find('input[type=checkbox]').prop('disabled', true);
                        }
                        else {
                            $(this).closest('tr').find('td').eq(2).find('input[type=checkbox]').prop('disabled', false);
                        }
                    });


                },
                error: function ajaxError(result) {
                    alert(result.d);
                }

            });



            $(document).on('change', '#TP_Mandatory', function () {
                var ch = $('#TP_Mandatory').attr("checked") ? 1 : 0;
                if (ch == 0) {
                    $('.tpneed').css('display', 'none');
                }
                else {
                    $('.tpneed').css('display', 'table-row');
                }
            });


            $(document).on('change', '#UNLNeed', function () {
                var ch = $('#UNLNeed').attr("checked") ? 1 : 0;
                if (ch == 0) {
                    $('#Requ').css('display', 'none');
                }
                else {
                    $('#Requ').css('display', 'table-row');
                }
            });
            $(document).on('change', '#mandis input[type=checkbox]', function () {
                var ch = $(this).attr("checked") ? 1 : 0;
                if ($(this).closest('td').index() == 1) {
                    if (ch == 0) {
                        $(this).closest('tr').find('td').eq(2).find('input[type=checkbox]').attr('checked', false);
                        $(this).closest('tr').find('td').eq(2).find('input[type=checkbox]').prop('disabled', true);
                    }
                    else {
                        $(this).closest('tr').find('td').eq(2).find('input[type=checkbox]').prop('disabled', false);
                    }
                }
            });
            $("#DisRad").blur(function () {
                if ($('#DisRad').val().length < 1) {
                    $('#DisRad').val("0");
                }
            });
            $("#submit").click(function () {
                var arr = new Array();
                arr[0] = $('#UNLNeed').attr("checked") ? 1 : 0;
                arr[1] = $('#NwRoute').attr("checked") ? 1 : 0;
                arr[2] = $('#StkNeed').attr("checked") ? 1 : 0;
                arr[3] = $('#NwDist').attr("checked") ? 1 : 0;
                arr[4] = $('#jointwork').attr("checked") ? 1 : 0;
                arr[5] = $('#template').attr("checked") ? 1 : 0;
                arr[6] = $('#CusOrder').attr("checked") ? 1 : 0;
                arr[7] = $('#OrderVal').attr("checked") ? 1 : 0;
                arr[8] = $('#NetweightVal').attr("checked") ? 1 : 0;
                arr[9] = $('#sms').val().toString();
                arr[10] = $('#DrSmpQ').attr("checked") ? 1 : 0;
                arr[11] = $('#CollectedAmount').attr("checked") ? 1 : 0;
                arr[12] = $('#recv').attr("checked") ? 1 : 0;
                arr[13] = $('#closing').attr("checked") ? 1 : 0;
                arr[14] = $('#OrdAsPrim').attr("checked") ? 1 : 0;
                arr[15] = $('#GEOTagNeed').attr("checked") ? 1 : 0;
                arr[16] = $('#DisRad').val().toString();
                arr[17] = $('#geoTrack').attr("checked") ? 1 : 0;

                arr[18] = $('#rcstk').attr("checked") ? 1 : 0;
                arr[19] = $('#sredit').attr("checked") ? 1 : 0;
                arr[20] = $('#orentry').attr("checked") ? 1 : 0;
                arr[21] = $('#dlyinven').attr("checked") ? 1 : 0;
                arr[22] = $('#esubcall').attr("checked") ? 1 : 0;


                var ned = $('#gstNoD').attr("checked") ? 'gs#' : '';
                ned += $('#addressD').attr("checked") ? 'ad#' : '';
                ned += $('#areaD').attr("checked") ? 'ar#' : '';
                ned += $('#cityD').attr("checked") ? 'ct#' : '';
                ned += $('#landmarkD').attr("checked") ? 'ln#' : '';
                ned += $('#pinD').attr("checked") ? 'pc#' : '';
                ned += $('#contactD').attr("checked") ? 'cp#' : '';
                ned += $('#contactD2').attr("checked") ? 'cp2#' : '';
                ned += $('#phoneD').attr("checked") ? 'ph#' : '';
                ned += $('#phoneD2').attr("checked") ? 'ph2#' : '';
                ned += $('#designationD').attr("checked") ? 'dn#' : '';
                ned += $('#designationD2').attr("checked") ? 'dn2#' : '';
				ned += $('#dobd').attr("checked") ? 'dob#' : '';
                ned += $('#doad').attr("checked") ? 'doa#' : '';
				ned += $('#visd').attr("checked") ? 'visit#' : '';
                ned += $('#layd').attr("checked") ? 'layer#' : '';
                ned += $('#bred').attr("checked") ? 'breed#' : '';
                ned += $('#broid').attr("checked") ? 'broil#' : '';

                var mnd = $('#gstNoM').attr("checked") ? 'gs#' : '';
                mnd += $('#addressM').attr("checked") ? 'ad#' : '';
                mnd += $('#areaM').attr("checked") ? 'ar#' : '';
                mnd += $('#cityM').attr("checked") ? 'ct#' : '';
                mnd += $('#landmarkM').attr("checked") ? 'ln#' : '';
                mnd += $('#pinM').attr("checked") ? 'pc#' : '';
                mnd += $('#contactM').attr("checked") ? 'cp#' : '';
                mnd += $('#contactM2').attr("checked") ? 'cp2#' : '';
                mnd += $('#phoneM').attr("checked") ? 'ph#' : '';
                mnd += $('#phoneM2').attr("checked") ? 'ph2#' : '';
                mnd += $('#designationM').attr("checked") ? 'dn#' : '';
                mnd += $('#designationM2').attr("checked") ? 'dn2#' : '';
				mnd += $('#dobM').attr("checked") ? 'dob#' : '';
                mnd += $('#doaM').attr("checked") ? 'doa#' : '';
				mnd += $('#visM').attr("checked") ? 'visit#' : '';
				ned += $('#layM').attr("checked") ? 'layer#' : '';
                ned += $('#breM').attr("checked") ? 'breed#' : '';
                ned += $('#broiM').attr("checked") ? 'broil#' : '';
                arr[23] = ned;
                arr[24] = mnd;

                arr[25] = $('#DTDNeed').attr("checked") ? 1 : 0;
                arr[26] = $('#InshopND').attr("checked") ? 1 : 0;
                arr[27] = $('#DistBased').attr("checked") ? 1 : 0;
                arr[28] = $('#msDate').attr("checked") ? 1 : 0;
                arr[29] = $('#opbal').attr("checked") ? 1 : 0;
                arr[30] = $('#clbal').attr("checked") ? 1 : 0;
                arr[31] = $('#freeqty').attr("checked") ? 1 : 0;
                arr[32] = $('#callpreview').attr("checked") ? 1 : 0;
                arr[33] = $('#phOrder').attr("checked") ? 1 : 0;
                arr[34] = $('#BattorySt').attr("checked") ? 1 : 0;

                arr[35] = $('#geoTrackprimary').attr("checked") ? 1 : 0;

                arr[36] = $('#priceCat').val().toString();
                arr[37] = $('#Explorerneed').attr("checked") ? 1 : 0;

                arr[38] = $('textarea#Expkey').val().toString();
                //console.log(arr[13]);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "App_Settings.aspx/savedata",
                    data: "{'data':'" + arr + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "Sucess")
                            alert("Setup has been updated Successfully");
                        else
                            alert("Setup updation unSuccessfull");
                    },
                    error: function (result) {
                        // alert(JSON.stringify(result));
                        alert("Setup updation unSuccessfull");
                    }
                });


            });


        });
    </script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
        
      .ajax__tab_xp .ajax__tab_tab
        {
            height: 300px;
            min-height: 300px;
        }
        .ajax__tab_header
        {
            font-family: "Helvetica Neue" , Arial, Sans-Serif;
            font-size: 14px;
            font-weight: bold;
            display: block;
            
        }
        
        .ajax__tab_hover .ajax__tab_outer
        {
            background-color: #9c3;
        }
        .ajax__tab_hover .ajax__tab_inner
        {
            color:Blue;
            
        }
        .ajax__tab_active .ajax__tab_outer
        {
            border-bottom-color: #ffffff;
            background-color: #d7d7d7;
        }
        .ajax__tab_active .ajax__tab_inner
        {
            color: #000;
            border-color: #333;
        }
        .ajax__tab_body
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            background-color: #fff;
            border-top-width: 0;
            border: solid 1px #d7d7d7;
            border-top-color: #ffffff;
        }    
     /*style for new check box */
    


     

    .tab-content
    {
        border: 1px solid lightgray; 
margin-top: -1px;
    padding: 20px 8px 0px 20px;
    }
    
    
    .table>thead>tr>th, .table>tbody>tr>th, .table>tfoot>tr>th, .table>thead>tr>td, .table>tbody>tr>td, .table>tfoot>tr>td
    {   
    
        border-top: 0px solid;
    }   
    
    
    .table td, .table th, .table tr {
    border: none;
}


.tg-list-item {
  margin: 0 0em;
}


.tgl {
  display: none;
}
.tgl, .tgl:after, .tgl:before, .tgl *, .tgl *:after, .tgl *:before, .tgl + .tgl-btn {
  box-sizing: border-box;
}
.tgl::-moz-selection, .tgl:after::-moz-selection, .tgl:before::-moz-selection, .tgl *::-moz-selection, .tgl *:after::-moz-selection, .tgl *:before::-moz-selection, .tgl + .tgl-btn::-moz-selection {
  background: none;
}
.tgl::selection, .tgl:after::selection, .tgl:before::selection, .tgl *::selection, .tgl *:after::selection, .tgl *:before::selection, .tgl + .tgl-btn::selection {
  background: none;
}
.tgl + .tgl-btn {
  outline: 0;
  display: block;
  width: 4em;
  height: 2em;
  position: relative;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
}


.tgl-skewed + .tgl-btn {
  overflow: hidden;
  -webkit-transform: skew(-10deg);
          transform: skew(-10deg);
  -webkit-backface-visibility: hidden;
          backface-visibility: hidden;
  -webkit-transition: all .2s ease;
  transition: all .2s ease;
  font-family: sans-serif;
  background: #888;
}
.tgl-skewed + .tgl-btn:after, .tgl-skewed + .tgl-btn:before {
  -webkit-transform: skew(10deg);
          transform: skew(10deg);
  display: inline-block;
  -webkit-transition: all .2s ease;
  transition: all .2s ease;
  width: 100%;
  text-align: center;
  position: absolute;
  line-height: 2em;
  font-weight: bold;
  color: #fff;
  text-shadow: 0 1px 0 rgba(0, 0, 0, 0.4);
}
.tgl-skewed + .tgl-btn:after {
  left: 100%;
  content: attr(data-tg-on);
}
.tgl-skewed + .tgl-btn:before {
  left: 0;
  content: attr(data-tg-off);
}
.tgl-skewed + .tgl-btn:active {
  background: #888;
}
.tgl-skewed + .tgl-btn:active:before {
  left: -10%;
}
.tgl-skewed:checked + .tgl-btn {
  background: #86d993;
}
.tgl-skewed:checked + .tgl-btn:before {
  left: -100%;
}
.tgl-skewed:checked + .tgl-btn:after {
  left: 0;
}
.tgl-skewed:checked + .tgl-btn:active:after {
  left: 10%;
}
       select
    {
        
  padding: 0 4px;
    }


.table > tbody > tr > td {
    vertical-align: middle;
}
label {
     margin-bottom: 0px;
	margin:4px;
}



    </style>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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

        function number(val) {
            var charcode = event.keyCode ? event.keyCode : event.keyCode ? event.keyCode : 0
            if ((charcode != 46 || val.indexOf('.') != -1) && (charcode < 48 || charcode > 57)) {
              return false; 
            }
            else
               return true;
        }   

       

    </script>
</head>
<body>
    
   <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 
    <div style="text-align:center; width:100%">

     
    </div>   
   
                    
            
                      

    </form>


<div class="container" style="max-width:100%">
  
  <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#home">General</a></li>
    <li><a data-toggle="tab" href="#menu1">Order & Stock Updation</a></li>
    <li><a data-toggle="tab" href="#menu2">Location Settings</a></li>    
  </ul>

  <div class="tab-content">
    <div id="home" class="tab-pane fade in active">
      <div class="table-responsive">  
      <table class="table borderless">
    
    <tbody >
      <tr>
        <td >New Route Creation</td>
        <td class="col-xs-2" style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="NwRoute" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="NwRoute"></label></td>        
      </tr>      
         <tr>
        <td>New Retailer Entry</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="UNLNeed" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="UNLNeed"></label></td>        
      </tr>
      <tr id="Requ">
            <td colspan="2">
                <table class="table borderless newStly"  id="mandis">
                    <tr><th>Entry Field</th><th>Display</th><th>Mandatory</th></tr>
                    <tr><td>GST No. </td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="gstNoD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="gstNoD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="gstNoM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="gstNoM"></label></td>
                       </tr>


                    <tr><td>Address</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="addressD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="addressD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="addressM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="addressM"></label></td>
                       </tr>

                     <tr><td>Area Name</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="areaD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="areaD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="areaM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="areaM"></label></td>
                       </tr>

                      <tr><td>City Name</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="cityD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="cityD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="cityM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="cityM"></label></td>
                       </tr>

                       <tr><td>Landmark</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="landmarkD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="landmarkD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="landmarkM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="landmarkM"></label></td>
                       </tr>

                      <tr><td>PIN Code  </td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="pinD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="pinD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="pinM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="pinM"></label></td>
                       </tr>

                    
                      <tr><td>Contact Person   </td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="contactD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="contactD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="contactM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="contactM"></label></td>
                       </tr>
                    
                    
                      <tr><td>Contact Person 2 </td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="contactD2" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="contactD2"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="contactM2" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="contactM2"></label></td>
                       </tr>
                    <tr><td>Phone </td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="phoneD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="phoneD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="phoneM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="phoneM"></label></td>
                       </tr>

                     <tr><td>Phone 2</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="phoneD2" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="phoneD2"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="phoneM2" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="phoneM2"></label></td>
                       </tr>

                       <tr><td>Designation</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="designationD" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="designationD"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="designationM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="designationM"></label></td>
                       </tr>

                       <tr><td>Designation 2</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="designationD2" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="designationD2"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="designationM2" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="designationM2"></label></td>
                       </tr>
					   
					    <tr><td>DOB</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="dobd" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="dobd"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="dobM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="dobM"></label></td>
                       </tr>
					   
                     <tr><td>DOA</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="doad" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="doad"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="doaM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="doaM"></label></td>
                       </tr>
					   
					     <tr><td>Purpose of Visit</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="visd" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="visd"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="visM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="visM"></label></td>
                       </tr>
        <tr><td>Layer</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="layd" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="layd"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="layM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="layM"></label></td>
                       </tr>
                              <tr><td>Breed</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="bred" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="bred"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="breM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="breM"></label></td>
                       </tr>
                        <tr><td>Broil</td>
                   <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="broid" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="broid"></label></td>  
                      <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="broiM" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="broiM"></label></td>
                       </tr>
                </table>
            </td>
        </tr>
      <tr>
        <td>Primary Order Entry</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="StkNeed" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="StkNeed"></label></td>        
      </tr>
      <tr>
        <td>New Distributor Creation</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="NwDist" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="NwDist"></label></td>        
      </tr>
      <tr>
        <td>Jointwork Can Change</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="jointwork" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="jointwork"></label></td>        
      </tr>
      <tr>
        <td>Remark Template</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="template" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="template"></label></td>        
      </tr>
      <tr>
        <td>Walk-In Sequence</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="CusOrder" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="CusOrder"></label></td>        
      </tr>

		 <tr>
        <td>Edit Submitted Calls</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="esubcall" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="esubcall"></label></td>        
      </tr>
      <%--  Mano Last Update   --%>     
		
      
		 <tr>
        <td>Door To Door</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="DTDNeed" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="DTDNeed"></label></td>        
      </tr>

      
		 <tr>
        <td>Inshop Sales</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="InshopND" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="InshopND"></label></td>        
      </tr>

      
		
      
		 <tr>
        <td>Distributor Need</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="DistBased" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="DistBased"></label></td>        
      </tr>

      
		 <tr>
        <td>Missed Date</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="msDate" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="msDate"></label></td>        
      </tr>


      <tr>
        <td>TP Mandatory</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="TP_Mandatory" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="TP_Mandatory"></label></td>        
      </tr>

      
      <tr class="tpneed">
        <td>TP Remainder</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;">
         <input type="text" name="TP_Remainder" runat="server" id="TP_Remainder" class="form-control datetimepicker"
                        autocomplete="off" style="min-width: 110px; width: 120px;" />
         </td>        
      </tr>

      
      <tr class="tpneed">
        <td>TP Date</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> 
         <input type="text" name="TP_Date" runat="server" id="TP_Date" class="form-control datetimepicker"
                        autocomplete="off" style="min-width: 110px; width: 120px;" />
        </td>        
      </tr>
		
    </tbody>
  </table>
  </div>
    </div>
    <div id="menu1" class="tab-pane fade">
      <div class="table-responsive">  
      <table class="table borderless">
    
    <tbody>
      <tr>
        <td >Show Order Value</td>
        <td class="col-xs-2" style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="OrderVal" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="OrderVal"></label></td>        
      </tr>      
         <tr>
        <td>Show Netweight Values</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="NetweightVal" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="NetweightVal"></label></td>        
      </tr>
      <tr>
        <td>Order Infromation Send as</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> 
       <%-- <input class="tgl tgl-skewed" id="sms" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="sms"></label>  --%>    
        <select size="1"  id="sms" style=" height:24px"; >
             <option value="0">None</option>
             <option value="1">SMS</option>
             <option value="2">Whatsup</option>
         </select>

        </td> 
         
      </tr>
      <tr>
        <td>Show Product Value</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="DrSmpQ" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="DrSmpQ"></label></td>        
      </tr>
      <tr>
        <td>Collected Amount Entry</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="CollectedAmount" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="CollectedAmount"></label></td>        
      </tr>
      <tr>
        <td>Primary Stock Entrable</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="recv" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="recv"></label></td>        
      </tr>
      <tr>
        <td>Closing Stock Entrable</td>
        <td class="col-xs-2"style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="closing" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="closing"></label></td>        
      </tr>
       <tr>
        <td>Order as Next Day Primary</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="OrdAsPrim" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="OrdAsPrim"></label></td>        
      </tr>


		

      <tr>
        <td>Retailer Closing Stock</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="rcstk" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="rcstk"></label></td>        
      </tr>

      <tr>
        <td>Secondary Rate Editable</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="sredit" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="sredit"></label></td>        
      </tr>

      <tr>
        <td>Order Return Entry</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="orentry" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="orentry"></label></td>        
      </tr>

      <tr>
        <td>Daily Inventory</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="dlyinven" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="dlyinven"></label></td>        
      </tr>

      <%-- mano  --%>


      
      <tr>
        <td>Opening Balance</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="opbal" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="opbal"></label></td>        
      </tr>


      <tr>
        <td>Closing Balance</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="clbal" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="clbal"></label></td>        
      </tr>


      <tr>
        <td>Free</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="freeqty" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="freeqty"></label></td>        
      </tr>


      <tr>
        <td>Call Preview</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="callpreview" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="callpreview"></label></td>        
      </tr>


      <tr>
        <td>Phone Order </td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="phOrder" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="phOrder"></label></td>        
      </tr>



         <tr>
          <td>Price Category</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;">      
        <select size="1"  id="priceCat" style=" height:24px"; >
             <option value="0">None</option>
             <option value="1">Distributor</option>
             <option value="2">Retailer</option>
         </select>
        </td> 
      </tr>
    </tbody>
  </table>
  </div>
  </div>
  
    <div id="menu2" class="tab-pane fade">
       <div class="table-responsive">  
      <table class="table borderless">
    
    <tbody>
      <tr >
        <td >Geo fencing Secondary</td>
        <td class="col-xs-2" style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="GEOTagNeed" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="GEOTagNeed"></label></td>        
      </tr>   
          <td >Geo fencing Primary</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="geoTrackprimary" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="geoTrackprimary"></label></td>        
      </tr>   
       <tr >
        <td>Radius (In Km)</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input type="text" id="DisRad"  style=" height:24px; width:100px" CssClass="decimal"  onfocus="this.style.backgroundColor='#E0EE9D'"   onblur="this.style.backgroundColor='White'" onkeypress="return number(this.value);"  ;/> </td>        
      </tr> 
      <tr>
        <td >Geo Tracking </td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="geoTrack" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="geoTrack"></label></td>        
      </tr> 
      <tr>
    
       <tr>
        <td >Battery Status</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"> <input class="tgl tgl-skewed" id="BattorySt" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="BattorySt"></label></td>        
      </tr> 
          <tr>
        <td >Explorer Need</td>
        <td style="text-align: -webkit-right;padding: 4px 20px;"><input class="tgl tgl-skewed" id="Explorerneed" type="checkbox"/>
        <label class="tgl-btn" data-tg-off="OFF" data-tg-on="ON" for="Explorerneed"></label></td>        
      </tr> 
          <tr>
        <td >Explorer Keyword</td>
     <td>
        </td>    
      </tr> 
        <tr>
         <td> 
              <%--<input id="Explorerneed" type="text"/>--%>
             <textarea  id="Expkey" rows="4" cols="100"></textarea></td>
            </tr>
    </tbody>
  </table>
  </div>
    </div>
    
  </div>
  <div style="text-align:center">
  <br />
    <button type="button" class="btn btn-primary" id="submit">Submit</button>
    </div>
  </div>
  


</body>
</html>
</asp:Content>