<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="sundywrk_aprval.aspx.cs" Inherits="MIS_Reports_sundywrk_aprval" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
    <title></title>
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        th {
            position: sticky;
            top: 0;
            background: #177a9e;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
             text-align: center;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12 sub-header">
                Sunday Working Approval
            </div>
        </div><br />
        
        <div class="container"  style="max-width: 100%; width: 95%;padding: 34px 15px">
            <table class="grids" width="100%" id="grid">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>SF_Code</th>
                        <th>SF_Name</th>
                        <th>Date</th>
                       <th>Remarks</th>
                       <th>Action</th>
                    </tr>
                </thead>
                <tbody></tbody>
      
            </table>
        </div>
    </form>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            getdatas();
        });
        function getdatas() {
            $.ajax({
                contentType: "application/json; charset=utf-8",
                async: true,
                url: 'sundywrk_aprval.aspx/getData',
                type: "POST",
                dataType: "json",
                success: function (data) {
                    bDat = JSON.parse(data.d);
                    str = '';
                    var tbl = $('#grid');
                    $(tbl).find('tbody tr').remove();
                    if (bDat.length > 0) {
                        for (var i = 0; i < bDat.length; i++) {
                            str = '<td><input type="hidden" name="hlid" value="' + bDat[i].sdate + '" />' + (i + 1) + '</td><td><input type="hidden" name="hsf" value="' + bDat[i].Sf_Code + '" />' + bDat[i].Sf_Code + '</td><td>' + bDat[i].Sf_Name + '</td><td>' + bDat[i].sundaydate + '</td><td>' + bDat[i].Remarks + '</td>';
                            str += '<td style="text-align: center; width:200px;" ><a id="btnApp" class="btn btn-primary">Approval</a> <a id="btnRej" class="btn btn-danger">Reject</a></td>';
                            $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                        }
                    }
                    else {
                        $(tbl).find('tbody').append('<tr><td colspan="10" style="color:red">No Record Found..!</td></tr>');
                    }

                },
                error: function (erorr) {
                    console.log(erorr);
                }
            });
        }
        $(document).on('click', '[id*=btnApp]', function () {
            var mRow = $(this).closest('tr');
            if (confirm('Are you sure you want to Approval ?')) {
               sdate = $(mRow).find('input[name=hlid]').val();
               sfCode = $(mRow).find('input[name=hsf]').val();
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'sundywrk_aprval.aspx/Approvaldata',
                    type: "POST",
                    data: "{'SF_Code':'" + sfCode + "','sundate':'" + sdate + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        getdatas();
                    },
                    error: function (erorr) {
                        console.log(erorr);
                    }
                });
            }
            else {
            }
        });
        $(document).on('click', '[id*=btnRej]', function () {
            var mRow = $(this).closest('tr');
            if (confirm('Are you sure you want to Reject ?')) {
                 sdate = $(mRow).find('input[name=hlid]').val();
                sfCode = $(mRow).find('input[name=hsf]').val();
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'sundywrk_aprval.aspx/RejectData',
                    type: "POST",
                    data: "{'SF_Code':'" + sfCode + "','sundate':'" + sdate + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        getdatas();
                    },
                    error: function (erorr) {
                        console.log(erorr);
                    }
                });
            }
            else {
            }


        });
        </script>
</body>
</asp:Content>
