<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Rpt_Route.aspx.cs" Inherits="MIS_Reports_Rpt_Route" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head >
        <title>Route Report</title>
        <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
        <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">


        <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js"></script>--%>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/base/jquery-ui.css">


        <script type="text/jscript">  
            $(document).ready(function () {
                $('.date-picker').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'MM yy',
                    showButtonPanel: true,
                    onClose: function (dateText, inst)  {

                        var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();

                        var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();

                        $(this).datepicker('setDate', new Date(iYear, iMonth, 1));
                        var sdate =((inst.selectedYear)+'/'+ (inst.selectedMonth+1)+'/'+ 1);           

                        $('#txtFromDate').val(sdate)

                    },
                    beforeShow: function () {
                        if ((selDate = $(this).val()).length > 0) {

                            iYear = selDate.substring(selDate.length - 4, selDate.length);

                            iMonth = jQuery.inArray(selDate.substring(0, selDate.length - 5), $(this).datepicker('option', 'monthNames'));
                            $(this).datepicker('option', 'defaultDate', new Date(iYear, iMonth, 1));
                            $(this).datepicker('setDate', new Date(iYear, iMonth, 1));
                        }
                    }
                        });
                $(document).on('click', '#btnview', function () {
                    if ($('[id$=ddlff]').children("option:selected").val() == '0') {
                        alert("Pls Select field Force ");
                    }
                    else if ($('#txtFromDate').val() == "") {
                        alert("Pls Select  Date");
                    }
                    else {
                        var url = "rpt_route_availvisi.aspx?&sfcode=" + $('[id$=ddlff]').children("option:selected").val() + "&startDate=" + $('#txtFromDate').val() + "&sfName=" + $('[id$=ddlff]').children("option:selected").text() + "&monyear=" + $('#startDate').val()
                        window.open(url, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
                    }
                });
            });
        </script>
        <style>
            .ui-datepicker-calendar {
                display: none;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
               	<input type="hidden" id="txtFromDate" />
                <div align="center" style="height:50px">
                    FieldForce Name:
                <asp:DropDownList ID="ddlff" runat="server" AutoPostBack="false"></asp:DropDownList>
                </div>

                <div align="center" style="height:50px">
                    Month Year:
                <input name="startDate" id="startDate" autocomplete="off" class="date-picker" />
                </div>
                <div align="center"  style="height:50px">
              <button type="button" id="btnview">View</button>
                </div>

            </div>
        </form>
    </body>
    </html>
</asp:Content>
