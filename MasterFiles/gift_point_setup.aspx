<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="gift_point_setup.aspx.cs" Inherits="MasterFiles_gift_point_setup" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
        }

        th {
            position: sticky;
            top: 0;
            background: #6c7ae0;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        .a {
            line-height: 22px;
            padding: 3px 4px;
            border-radius: 7px;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }
       .gosubmit{
             display: inline-block;
            margin: 0;
            cursor: pointer;
            border: 1px solid #bbb;
            overflow: visible;
            font: bold 13px arial, helvetica, sans-serif;
            text-decoration: none;
            white-space: nowrap;
            color: #555;
            border-radius: 11px;
            background-color: #ddd;
            width: 5%;
        height: 26px;
           }
        input[type='text'], select {
            line-height: 22px;
            padding: 6px 21px;
            border: solid 1px #bbbaba;
            border-radius: 7px;
        }
    </style>
    <form id="formid" class="form-horizontal" runat="server">
           <div class="row">
            <div class="col-lg-12 sub-header">
               Gift Point Setup
            </div>
        </div>
         <center>
             <table class="auto-index" id="grid">
                 <thead>
                       <tr>
                        <th>S.No</th>
                         <th>Name</th>
                          <th>Points</th>
                         <th>Setup</th>
                           </tr>
                     </thead>
                <tbody>
               </tbody> </table>
         <br />
        <br />
        <input type="submit" name="btnsave" id="btnsave" class="btn btn-primary btnsave" value="Save" style="width: 100px;" />
              </center>
        </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">
  
        var prods = []; var arr = [];
        var ar = [{ Point_type_name: 'Retailer', Point_Type: '1' }, {Point_type_name: 'Distributor', Point_Type: '1' }];
        $(document).ready(function () {
            FillData();
           
        });
        function FillData() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "gift_point_setup.aspx/Filldata",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('#grid tbody').html('');
                    prods = JSON.parse(data.d);
                    var str = '';
                    if (prods.length > 0) {

                        for (var j = 0; j < prods.length; j++) {

                            str += "<tr><td>" + (j + 1) + "</td><td><input type='hidden' name='retnm' value='" + prods[j].Point_type_name + "'/><span>" + prods[j].Point_type_name + "</span></td><td><input type='hidden' name='rpoint' value='" + prods[j].Point_Type + "'/><span>" + prods[j].Point_Type + "</span></td><td><input type='text' name='resetup' value=" + prods[j].Point_Value + " class='form-control pqty' style='height: 25px;'/></td></tr>";
                        }
                    }
                      else {

                          for (var i = 0; i < ar.length; i++) {
                              str += "<tr><td>" + (i + 1) + "</td><td><input type='hidden' name='retnm' value='" + ar[i].Point_type_name + "'/><span>" + ar[i].Point_type_name + "</span></td><td><input type='hidden' name='rpoint' value='" + ar[i].Point_Type + "'/><span>" + ar[i].Point_Type + "</span></td><td><input type='text' name='resetup' value='' class='form-control pqty' style='height: 25px;'/></td></tr>";
                          }
                       
                        }
                        
                    $('#grid tbody').append(str);
                }
            });
        }
        $(document).on('click', '.btnsave', function () {
           
            $('#grid').find('tbody').find('tr').each(function () {
                arr.push({
                   
                    point: $(this).find('input[name="rpoint"]').val(),
                    pval: $(this).find('input[name="resetup"]').val(),
                    pnam: $(this).find('input[name="retnm"]').val(),
                });
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "gift_point_setup.aspx/Savepoint",
                data: "{'Data':'" + JSON.stringify(arr) + "','divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Success') {

                        alert('Gift Point Saved Successfully');

                    }
                    else {

                        alert(data.d);
                    }

                },
                error: function (exception) {
                    console.log(exception);
                }
            });
        });
        </script>
</asp:Content>
