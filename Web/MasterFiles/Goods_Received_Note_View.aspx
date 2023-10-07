<%@ Page Title="Goods Received Note View" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Goods_Received_Note_View.aspx.cs" Inherits="MasterFiles_Goods_Received_Note_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Bootstrap Example</title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="CustomStyles.css" rel="stylesheet" />
    <style type="text/css">
        #ProductTable input[type='text']
        {
            text-align: right;
        }
        .row
        {
            padding: 2px 2px;
        }
        #ProductTable
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #ProductTable td, #ProductTable th
        {
            border: 1px solid #ddd;
            padding: 2px 4px;
        }
        
        #ProductTable td
        {
            vertical-align: top;
        }
        
        #ProductTable tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #ProductTable tr:hover
        {
            background-color: #ddd;
        }
        
        #ProductTable th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #475677;
            color: white;
        }
        
        #ProductTable td:nth-child(10), #ProductTable th:nth-child(10)
        {
            display: none;
        }
        
        #ProductTable td:nth-child(11), #ProductTable td:nth-child(13)
        {
            text-align: right;
        }
        .control-label
        {
            font-weight: normal;
        }
        #ProductTable tfoot th label
        {
            margin-bottom: 0px;
        }
    </style>
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                $('#grnNo').attr('disabled', true);
                $('#grnDate').attr('disabled', true);
                $('#ddlsupplier').attr('disabled', true);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Goods_Received_Note_View.aspx/Get_AllValues",
                    data: "{'grnNo':'" + $('#<%=hdngrn_no.ClientID %>').val() + "','grnDate':'" + $('#<%=hdngrn_date.ClientID %>').val() + "','grnSuppcode':'" + $('#<%=hdnsupp_code.ClientID %>').val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(JSON.parse(data.d));
                        var obj = JSON.parse(data.d);

                        if (obj.TransH.length > 0) {
                            $('#grnNo').text(obj.TransH[0].GRN_No);
                            $('#grnDate').text(obj.TransH[0].GRN_Date);
                            $('#ddlsupplier').text(obj.TransH[0].Supp_Name);
                            $('#txtEdate').text(obj.TransH[0].Entry_Date);
                            $('#grnPono').text(obj.TransH[0].Po_No);
                            $('#challenNo').text(obj.TransH[0].Challan_No);
                            $('#grnDisDate').text(obj.TransH[0].Dispatch_Date);
                            $('#ddldistributor').text(obj.TransH[0].Receved_Name);
                            $('#receivedby').text(obj.TransH[0].Received_By);
                            $('#authorised').text(obj.TransH[0].Authorized_By);
                            $('#remarks').text(obj.TransH[0].remarks);
                            $('#gnd_Good_tot').text(obj.TransH[0].goodsTot);
                            $('#gnd_Txt_tot').text(obj.TransH[0].taxTot);
                            $('#gnd_Net_tot').text(obj.TransH[0].netTot);


                        }
                        var str = "";
                        for (var i = 0; i < obj.TransD.length; i++) {
                            str = "<td>" + (i + 1) + "</td><td>" + obj.TransD[i].PCode + "</td><td>" + obj.TransD[i].PDetails + "</td><td>" + obj.TransD[i].UOM_Name + "</td><td>" + obj.TransD[i].Batch_No + "</td>";
                            str += "<td>" + obj.TransD[i].mfgDate + "</td><td>" + obj.TransD[i].POQTY + "</td><td>" + obj.TransD[i].Price + "</td><td>" + obj.TransD[i].Good + "</td><td>" + obj.TransD[i].Damaged + "</td><td>" + obj.TransD[i].Gross_Value + "</td>";
                            str += "<td></td><td>" + obj.TransD[i].Net_Value + "</td>";
                            $('#ProductTable >tbody').append('<tr>' + str + ' </tr>');
                            var taxrow = "";
                            for (var j = 0; j < obj.TransD[i].taxDtls.length; j++) {
                                taxrow += "<tr style='width:100%'><td style='width:50%'>" + obj.TransD[i].taxDtls[j].Tax_Name + "</td><td style='width:50%; text-align: right'>" + obj.TransD[i].taxDtls[j].Tax_Value + "</td></tr>";
                            }
                            $('#ProductTable:first tbody tr:last td:eq(11)').append('<table name="addtable"><tbody>' + taxrow + '</tbody></table>');
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
        });
    </script>
    <form id="goodsfrm" runat="server">
    <asp:HiddenField ID="hdnmode" runat="server" />
    <asp:HiddenField ID="hdngrn_no" runat="server" />
    <asp:HiddenField ID="hdngrn_date" runat="server" />
    <asp:HiddenField ID="hdnsupp_code" runat="server" />
    <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <label for="grnno" class="control-label  col-md-2 col-sm-6 " style="text-align: left">
                GRN No.</label>
            <label id="grnNo" class="control-label col-md-2 col-sm-6" style="text-align: left">
            </label>
            <label for="grndate" class="control-label col-md-2 col-sm-6" style="text-align: left">
                GRN Date</label>
            <label id="grnDate" class="control-label col-md-2 col-sm-6" style="text-align: left">
            </label>
            <label for="grndate" class="control-label col-md-2 col-sm-6" style="text-align: left">
                Entry Date</label>
            <label id="txtEdate" class="control-label col-md-2 col-sm-6" style="text-align: left">
            </label>
            <label for="ddlsupplier" class="control-label col-md-2 col-sm-6" style="text-align: left">
                Supplier</label>
            <label id="ddlsupplier" class="control-label col-md-2 col-sm-6" style="text-align: left">
            </label>
            <label for="ddldistributor" class="control-label col-md-2 col-sm-6" style="text-align: left">
                PO No.</label>
            <label id="grnPono" class="control-label col-md-2 col-sm-6" style="text-align: left">
            </label>
        </div>
    </div>
    <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <label for="challenNo" class="control-label col-md-2" style="text-align: left">
                PI No.</label>
            <label id="challenNo" class="control-label col-md-2" style="text-align: left">
            </label>
            <label for="grnno" class="control-label col-md-2" style="text-align: left">
                Dispatch Date</label>
            <label id="grnDisDate" class="control-label col-md-2" style="text-align: left">
            </label>
        </div>
        <table id="ProductTable" class="gvHeader">
            <thead>
                <tr>
                    <th style="width: 40px">
                        SlNo.
                    </th>
                    <th style="width: 200px">
                        PCode
                    </th>
                    <th style="width: 350px">
                        Product Name
                    </th>
                    <th>
                        UOM
                    </th>
                    <th style="width: 150px">
                        Batch No.
                    </th>
                     <th style="width: 150px">
                        MFG Date
                    </th>
                    <th style="width: 150px">
                        PO QTY
                    </th>
                    <th style="width: 150px">
                        Price
                    </th>
                    <th style="width: 150px">
                        Good
                    </th>
                    <th style="width: 150px">
                        Damaged
                    </th>
                    <th style="width: 150px">
                        Gross Value
                    </th>
                    <th style="width: 150px">
                        TAX
                    </th>
                    <th style="width: 150px">
                        Net Value
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
            <tfoot>
                <tr>
                    <th colspan="9">
                        Total
                    </th>
                    <th style="text-align: right">
                        <label name="gnd_Good_tot" id="gnd_Good_tot">
                            0.00
                        </label>
                    </th>
                    <th style="text-align: right">
                        <label name="gnd_Txt_tot" id="gnd_Txt_tot">
                            0.00
                        </label>
                    </th>
                    <th style="text-align: right">
                        <label name="gnd_Net_tot" id="gnd_Net_tot">
                            0.00
                        </label>
                    </th>
                </tr>
            </tfoot>
        </table>
        <br />
        <div class="row">
            <div class="col-sm-6">
                <label for="grnno" class="control-label col-sm-4" style="text-align: left">
                    Remarks
                </label>
                <label id="remarks" class="control-label col-sm-8" style="text-align: left">
                </label>
            </div>
            <div class="col-sm-6">
                <div class="row">
                    <label for="grnno" class="control-label col-sm-3" style="text-align: left">
                        Received Location</label>
                    <label id="ddldistributor" class="control-label col-sm-8" style="text-align: left">
                    </label>
                </div>
                <div class="row">
                    <label for="grnno" class="control-label col-sm-3" style="text-align: left">
                        Received By</label>
                    <label id="receivedby" class="control-label col-sm-8" style="text-align: left">
                    </label>
                </div>
                <div class="row">
                    <label for="grnno" class="control-label col-sm-3" style="text-align: left">
                        Authorised By</label>
                    <label id="authorised" class="control-label col-sm-8" style="text-align: left">
                    </label>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
