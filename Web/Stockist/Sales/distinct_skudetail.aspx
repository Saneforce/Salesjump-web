<%@ Page Language="C#"  MasterPageFile="~/Master_DIS.master"  AutoEventWireup="true" CodeFile="distinct_skudetail.aspx.cs" Inherits="MIS_Reports_distinct_skudetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
	</head>
	<body>
    <form id="form1" runat="server">
         <asp:HiddenField runat="server" ID="hfdt" />
         <asp:HiddenField runat="server" ID="htdt" />
         <asp:HiddenField runat="server" ID="stk_code" />
         <div class="row">
            <div class="col-lg-12 sub-header">
                SKU Wise Product
            </div>
        </div>
          <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="fdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="tdate" />
                </div>
            </div>
        </div>       
        <div class="row" id="distselect" style="display:none;margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label"> Distibutor Name <span style="color:red"> *</span></label>            
                <div class="col-md-2 inputGroupContainer">
                <div class="input-group">                    
                    <select class="form-control" id="ddl_dist" style="width: 100%;">
                        <option value="0">SelectDistributor</option>
                    </select>
                </div>
            </div>
        </div>
         <div class="row" style="margin-top: 5px">
            <div class="ccol-md-2  col-md-offset-5">
                <asp:Button runat="server" ID="exceldld" CssClass="btn" BackColor="#1a73e8" ForeColor="White" Text="Excel" OnClientClick="javascript: return chkinp()" OnClick="exceldld_Click" />
            </div>
        </div>
    </form>
     <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />     
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script language="javascript" type="text/javascript">
        var fdt = '';
        var tdt = '';
        var itms = [];
        sf = '';
        sf = '<%=Session["Sf_Code"]%>';
        sf_type = '<%=Session["sf_type"]%>';
        $(document).ready(function () {
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
        function loaddist() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "distinct_skudetail.aspx/binddistributor",
                data: "{'sf_code':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    itms = JSON.parse(data.d) || [];
                    for (var i = 0; i < itms.length; i++) {
                        $('#ddl_dist').append($("<option></option>").val(itms[i].Stockist_Code).html(itms[i].Stockist_Name + '-' + itms[i].ERP_Code)).trigger('chosen:updated').css("width", "100%");
                    }
                    $('#ddl_dist').selectpicker({
                        liveSearch: true
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
       
        </script>
    
        </body>
        </html>
</asp:Content>
