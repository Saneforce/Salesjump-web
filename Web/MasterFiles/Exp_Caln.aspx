<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Exp_Caln.aspx.cs" Inherits="MasterFiles_Exp_Caln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title>Expense Calender</title>
        <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    </head>
        <body>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <form runat="server">
            <%--<div id="stdet" class="col-xs-8 tab-pane fade" style="display: none; padding: 0px">
                            <div <%-- style="width:650px;float: right"--%>
                                <%--<br />
                                <div class="" style="margin-top: 5px">
                                    <label for="map" style="font-size: medium" class="form-label">Retail Group Mapping</label>
                                </div>
                                <div class="table-responsive" style="width: 800px; margin-left: -15px">
                                    <table id="mapping" class="mapprod" border="2px" style="border-collapse: collapse;">
                                        <thead style="background-color: lightblue;">
                                            <tr>--%>

            <div class="col-lg-9">

                
                <%--<div class="card">--%>
                <br />
                
                <div class="" style="margin-top: 5px">
                                    <label for="map" style="font-size: large" class="form-label">Expense Calender</label>
                                </div>
                <div style="margin-bottom: 1rem;float:right;margin-right:-200px;">
                          
                    <label for="bday-month" style="font-size: medium">Effective From</label>
                           <input id="Caln" type="month" name="month-year" style="font-size: medium" />
                </div>
                <br />
                <br />
                
                    <div class="table-responsive" style="width: 800px; margin-left: 45px">
                     <table id="ExpCaln" class="mapprod" border="2px" style="border-collapse: collapse;">
                                        <thead style="background-color: lightblue;">
                                            <tr>
                                                <th rowspan="2" style="text-align: center">Name of the Period</th>
                                                <th rowspan="2" style="text-align: center">From Date</th>
                                                <th rowspan="2" style="text-align: center">To Date</th>
                                                <th rowspan="2" style="text-align: center">ADD
                                                    <i class="fa fa-plus-circle" style="font-size: 25px; margin-left: 2px; color: green" id="addmat" onclick="Addmat()"></i>
                                                </th>
                                                <%--<th align="left" style="width: 32px;">--%>
                                                
                                                
                                            
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input style="width: 400px; font-size: medium" class="Period" id="Period" /></td>
                                                <td>
                                                    <select style="width: 250px; font-size: medium" class="FDte" id="FDate">
                                                        <option value="0">select</option>
                                                        <option value="1">1</option>
                                                        <option value="2">2</option>
                                                        <option value="3">3</option>
                                                        <option value="4">4</option>
                                                        <option value="5">5</option>
                                                        <option value="6">6</option>      
                                                        <option value="7">7</option>
                                                        <option value="8">8</option>
                                                        <option value="9">9</option>  
                                                        <option value="10">10</option>
                                                        <option value="11">11</option>
                                                        <option value="12">12</option>
                                                        <option value="13">13</option>
                                                        <option value="14">14</option>
                                                        <option value="15">15</option>
                                                        <option value="16">16</option>
                                                        <option value="17">17</option>
                                                        <option value="18">18</option>
                                                        <option value="19">19</option>
                                                        <option value="20">20</option>
                                                        <option value="21">21</option>
                                                        <option value="22">22</option>
                                                        <option value="23">23</option>
                                                        <option value="24">24</option>
                                                        <option value="25">25</option>
                                                        <option value="26">26</option>
                                                        <option value="27">27</option>
                                                        <option value="28">28</option>
                                                        <option value="end of month">end of month</option>
                                                    </select></td>
                                                <td>
                                                    <select style="width: 250px; font-size: medium" class="TDte" id="TDate" onchange="Todatechg();">
                                                                <option value="0">select</option>
                                                                <option value="1">1</option>
                                                                <option value="2">2</option>
                                                                <option value="3">3</option>
                                                                <option value="4">4</option>
                                                                <option value="5">5</option>
                                                                <option value="6">6</option>
                                                                <option value="7">7</option>
                                                                <option value="8">8</option>
                                                                <option value="9">9</option>
                                                                <option value="10">10</option>
                                                                <option value="11">11</option>
                                                                <option value="12">12</option>
                                                                <option value="13">13</option>
                                                                <option value="14">14</option>
                                                                <option value="15">15</option>
                                                                <option value="16">16</option>
                                                                <option value="17">17</option>
                                                                <option value="18">18</option>
                                                                <option value="19">19</option>
                                                                <option value="20">20</option>
                                                                <option value="21">21</option>
                                                                <option value="22">22</option>
                                                                <option value="23">23</option>
                                                                <option value="24">24</option>
                                                                <option value="25">25</option>
                                                                <option value="26">26</option>
                                                                <option value="27">27</option>
                                                                <option value="28">28</option>
                                                                <option value="end of month">end of month</option>
                                                    </select></td>
                                                <td><img style='width:28px;margin-left: 2px;' src='https://img.icons8.com/ios-filled/512/delete.png' class='btnDeletion' /></td>
                                                <%--<td><i class="fa fa-plus-circle" style="font-size: 25px; margin-left: 10px; color: green" id="addmat" onclick="Addmat()"></i></td>--%>
                                            </tr>
                                        </tbody>
                     </table>
                                <br />
                        <div style="margin-bottom: 1rem;float:left;margin-left:10px;">
                          <br />
                    <button id="btnsave" type="button" style="width:100px;" class="btn btn-success">Save</button>

                </div>
                 </div>
                
                </div>
            <script type="text/javascript">
                $(document).ready(function () {
				getvalue();
                    //$('.FDte').val(1);
                    
                });
				function Addpreval() {
                    $tb = $('#ExpCaln tbody');
                    $tr = $($tb).find('tr')[0];
                    $nwRw = $($tr).clone();

                    $tb.append($nwRw);
					}
                function Addmat() {
                    var le = $('.FDte').length
                    for (var i = 0; i < le; i++) {
                        if ($(".TDte").eq(i).val() == '0') {
                            alert('Please select the To Date')
                            return false;
                        }
                        if ($(".TDte").eq(i).val() == '29') {
                            return false;
                        }

                    }
                    var tst = 0;
                    
                    $tb = $('#ExpCaln tbody');
                    $tr = $($tb).find('tr')[0];
                    $nwRw = $($tr).clone();

                    $tb.append($nwRw);
                    $nwRw.find($(".Period")).val('');
                    var len = $('.FDte').length
                    for (var i = 0; i < len; i++) {
                        var $T = parseInt($(".TDte").eq(i).val())
                        $(".FDte").eq(i + 1).val($T + 1)
                    }
                    /*$('#ExpCaln tbody tr').each(function () {
                        tr = $("<tr class='subRow'></tr>");
                        $(tr).html("<td><input style='width: 400px; font - size: medium' class='Period' id='Period' /></td><td><select style='width: 250px; font - size: medium' class='FDte' id='FDate' ><td><select style='width: 250px; font - size: medium' class='TDate' id='TDte'><td><img style='width:28px;margin-left: 2px;' src='https://img.icons8.com/ios-filled/512/delete.png' class='btnDeletion' /></td>");
                        //$(tr).html("<td><input style='width: 400px; font-size: medium' class='Period' id='Period" + Prd + "'/></td><td><select style='width: 250px; font-size: medium' class='FDate' id='FDate" + fdt + "'></select></td><td><select style='width: 250px; font-size: medium' class='TDate' id='TDate" + tdt + "'></select></td><td><img style='width:28px;margin-left: 2px;' src='https://img.icons8.com/ios-filled/512/delete.png' class='btnDeletion' /></td>");
//$(tr).html("<td><input style='width: 140px; font-size: medium' readonly class='matecode' id='mcode" + xn + "' /></td><td><select style='width: 140px; font-size: medium' class='mategrop' id='matgrp" + xn + "'>" + cusg + "</select></td><td><input style='width: 100px; font-size: medium' class='mrpp' id='mrp" + xn + "' /></td><td><input style='width: 100px; font-size: medium' class='ratee' id='rate" + xn + "' /></td><td style='max-width: 230px;'><select style='width: 150px' class='plangrp' multiple id='plgr" + xn + "'>" + plgrop + "</select></td><td><img style='width:28px;margin-left: 8px;' src='https://img.icons8.com/ios-filled/512/delete.png' class='btnDeletion' /></td>");
                            $("#ExpCaln > TBODY").append(tr);
                            
                            return false;
                    });*/
                    
                }
                $("#ExpCaln").on('click', '.btnDeletion', function () {
                    $(this).closest('tr').remove();
                    var len = $('.FDte').length
                    for (var i = 0; i < len; i++) {
                        var $T = parseInt($(".TDte").eq(i).val())
                        $(".FDte").eq(i + 1).val($T + 1)
                    }
                });
                function Todatechg() {
                    
                    var len = $('.FDte').length
                    for (var i = 0; i < len; i++) {
                        
                        var $T = parseInt($(".TDte").eq(i).val())
                        
                        var $F = parseInt($(".FDte").eq(i).val())
                        if ($T < $F) {
                            alert('To Date must be greater')
                            $(".TDte").eq(i).val($(".FDte").eq(i + 1).val() - 1);
                            var t=$(".FDte").eq(i + 1).val()-1

                            break;
                        }
                        //var $T = parseInt($(".TDte").eq(i).val())
                        var t = $(".FDte").eq(i + 1).val() - 1
                        if ($T > t) {
                            alert('To Date must be greater')
                            $(".TDte").eq(i).val($(".FDte").eq(i + 1).val() - 1);
                            var t = $(".FDte").eq(i + 1).val() - 1

                            break;
                        }
                        $(".FDte").eq(i + 1).val($T + 1)
                    }
                }
                
                 $(document).on('click', '#btnsave', function () {
                     var res_arr = [];
                     var dt = $('#Caln').val();
                     if ($('#Caln').val().length === 0) {
                         alert('Select the Month and year');
                         alt = 1;
                         return false;
                     }
                     var tst = [];
                     tst = dt.split('-');
                     var alt = 0;
                     $('#ExpCaln tbody tr').each(function () {
                         var row = $(this).closest('tr');
                         var itm = {};
                         itm.Period = row.find('#Period').val();
                         if (row.find('#Period').val().length === 0) {
                             alert('Enter the period name');
                             alt = 1;
                             return false;
                         }
                         itm.FDate = row.find('#FDate').val();
                         //itm.TDate = row.find('#TDate').text();
                         itm.TDate = row.find("#TDate option:selected").text();
                         if (row.find("#TDate option:selected").val() == 0) {
                             alert('Select the To Date');
                             alt = 1;
                             return false;
                         }
                         itm.month = tst[1];
                         itm.year = tst[0];
                         res_arr.push(itm);
                     });
                     if (alt == 0) {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         async: false,
                         data: "{'res_arr':'" + JSON.stringify(res_arr) + "'}",
                         url: "Exp_Caln.aspx/insertdata",
                         dataType: "json",
                         success: function (data) {
                             var obj = data.d;
                             if (obj == 'true') {
                                 alert("Submitted Successfully");
                                 return false;
                             }
                         },
                         error: function (result) {
                         }
                     });
                     }
                 });
				 function getvalue() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Exp_Caln.aspx/getprevalue",
                    data: "{'divcode': '<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var preval = JSON.parse(data.d);
                        if (preval.length > 0) {
                            $('#btnsave').hide();
                            for (var i = 1; i < preval.length; i++) {
                                Addpreval();
                                
                            }
                            if (preval[0].Eff_Month < 10)
                                $("#Caln").val(preval[0].Eff_Year + '-0' + preval[0].Eff_Month);
                            else
                                $("#Caln").val(preval[0].Eff_Year + '-' + preval[0].Eff_Month);
                            var index = 0, i = 0;
                            $('#ExpCaln tbody tr').each(function () {
                                
                                var row = $(this).closest('tr');
                                var cnt = 0;
                                var idx = $(row).index();

                                row.find("td").find("#Period").val(preval[idx].Period_Name);
                                row.find("td").find("#FDate").val(preval[idx].From_Date);
                                row.find("td").find("#TDate").val(preval[idx].To_Date);


                            });
                        }
                        else
                        {
                            $('.FDte').val(1);
                            $('#btnsave').show();
                        }
                    },
                });
                }
            </script>
            </form>
            </body>
        </html>
</asp:Content>

