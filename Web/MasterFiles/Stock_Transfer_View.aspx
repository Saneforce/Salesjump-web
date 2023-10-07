<%@ Page Title="Stock Transfer View" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Stock_Transfer_View.aspx.cs" Inherits="MasterFiles_Stock_Transfer_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
        #stockTable input[name='txtQty']
        {
            text-align: right;
        }
        .row
        {
            padding: 2px 2px;
        }
        #stockTable
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #stockTable td, #stockTable th
        {
            border: 1px solid #ddd;
            padding: 2px 4px;
        }
        
        #stockTable td
        {
            vertical-align: middle;
        }
        
        #stockTable tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #stockTable tr:hover
        {
            background-color: #ddd;
        }
        
        #stockTable th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #475677;
            color: white;
        }
        
        #stockTable td:nth-child(10), #stockTable td:nth-child(12)
        {
            text-align: right;
        }
        .control-label
        {
            font-weight: normal;
        }
        body
        {
            overflow-x: initial !important;
        }
    </style>
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                $('#transNo').attr('disabled', true);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Stock_Transfer_View.aspx/Get_AllValues",
                    data: "{'TransSlNo':'" + $('#<%=hdntransno.ClientID %>').val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(JSON.parse(data.d));

                        var obj = JSON.parse(data.d);
                        if (obj.TransH.length > 0) {
                            $('#transNo').text(obj.TransH[0].transNo);
                            $('#transDate').text(obj.TransH[0].transDate);
                            $('#stockistFrom').text(obj.TransH[0].stockistFrom_Nm);
                            $('#stockistTo').text(obj.TransH[0].stockistTo_Nm);
                        }

                        var str = "";
                        for (var i = 0; i < obj.TransD.length; i++) {
                            str = "<td>" + (i + 1) + "</td><td>" + obj.TransD[i].pName + "</td><td>" + obj.TransD[i].pType + "</td><td>" + obj.TransD[i].pqty + "</td><td>" + obj.TransD[i].preason + "</td>";
                            $('#stockTable >tbody').append('<tr>' + str + ' </tr>');
                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

        });
    </script>

 	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            $(document).on('click', "#btnprint", function () {
                var printableArea = $('#printableArea').html();  
                window.open('data:application/vnd.ms-excel,' + encodeURIComponent(
		$('#printableArea').html()
	))
                e.preventDefault();
            });
    </script>
	
    <form id="stockfrm" runat="server">
    <asp:HiddenField ID="hdnmode" runat="server" />
    <asp:HiddenField ID="hdntransno" runat="server" />
    <div class="container" style="max-width: 100%; width: 100%;">
    <a id="btnprint" class="btn btn-primary"  style="vertical-align: middle; font-size: 14px;">
            <span>Excel</span></a>
          <div id="printableArea" class="page">
        <div class="row">
            <div class="form-horizontal">
                <div class="form-group" style="text-align: left">
                    <label for="transNo" class="control-label col-sm-2 col-sm-offset-2 " style="text-align: left">
                        Transfer No.</label>
                    <label id="transNo" class="control-label col-sm-2 " style="text-align: left">
                    </label>
                    <label for="transDate" class="control-label col-sm-1 " style="text-align: left">
                        Date</label>
                    <label id="transDate" class="control-label col-sm-4" style="text-align: left">
                    </label>
                </div>
                <div class="form-group" style="text-align: left">
                    <label for="stockistFrom" class="control-label col-sm-2 col-sm-offset-2" style="text-align: left">
                        From</label>
                    <label id="stockistFrom" class="control-label col-sm-2 " style="text-align: left">
                    </label>
                    <label for="stockistTo" class="control-label col-sm-1" style="text-align: left">
                        To</label>
                    <label id="stockistTo" class="control-label col-sm-4 " style="text-align: left">
                    </label>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <table id="stockTable" class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 50px">
                                Sl.No.
                            </th>
                            <th>
                                Product Name
                            </th>
                            <th style="width: 150px">
                                Stock Type
                            </th>
                            <th style="width: 150px">
                                QTY
                            </th>
                            <th style="width: 250px">
                                Reason
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
    </form>
</asp:Content>
