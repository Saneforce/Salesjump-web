<%@ Page Title="Distance Entry" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Distance_Entry_New.aspx.cs" Inherits="MasterFiles_Distance_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="main_frm" runat="server">
    <div class="row">
        <div class="col-lg-12 sub-header">Distance Entry 
            <div style="float:right">
                <div class="input-group" style="width:350px"><span class="input-group-addon"><i class="glyphicon glyphicon-th-list"></i></span>
                <asp:DropDownList ID="ddlTerritoryName" runat="server" SkinID="ddlRequired" CssClass="form-control" Style="font-size: 17px;font-size:13px" Width="350">
                </asp:DropDownList></div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
            </div>
             <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>                          
                        <th style='text-align:left'>From Place</th>
                        <th style='text-align:left'>To Place</th>
                        <th style='text-align:left'>Type</th>
                        <th style='text-align:left'>Distance</th>                          
                        <th style='text-align:left'>Add / Remove</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>          
        </div>
    </div>
    <div style="display: block;position: fixed;bottom: 0px;padding: 5px;background: #fff;margin-left: -15px;box-shadow:1px 1px 1px 1px #000;width: 100%;"><i class='btn btn-primary btn-save' style="margin-left:30px;">Save</i> </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "HQ,Territory_Name,Allowance_Type,";
        var AlwTyps="<option value='HQ'>HQ</option><option value='EX'>EX</option><option value='OS'>OS</option><option value='OX'>OS-EX</option>"
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
        function loadPgNos() {
            $(".pagination").html("");
            TotalPg = (Orders.length / PgRecords).toFixed(0) //+ ((Orders.length % PgRecords) ? 1 : 0)
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>';
            for (il = 0; il < TotalPg; il++) {
                spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">Next</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = $(this).text(); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");TerrList = "";
            for ($i = 0; $i < Orders.length; $i++) {

                tr = $("<tr " + ((Orders[$i].AddRw != undefined && Orders[$i].AddRw == 1)?"class='subRow'":"") + "></tr>");
                if (Orders[$i].AddRw != undefined && Orders[$i].AddRw == 1 && Orders[$i].LRw != undefined && Orders[$i].LRw == 1)
                    $(tr).html("<td>" + Orders[$i].HQ + "</td><td><select class='ddltolist' data-sTr='" + Orders[$i].Territory_Code + "'></select></td><td class='altyp'><select class='Atyp' data-aTy='" + Orders[$i].Allowance_Type + "'>" + AlwTyps + "</select></td><td style='width:170px;'><input type='text' class='txtDist' value='' onkeyup='CalcDis(this)' /></td><td style='width:170px;'><i class='btn btn-primary btn-AddRw' onclick='AddRow(this)' style='min-width: 70px;'>Add</i>&nbsp;<i class='btn btn-danger  btn-update' onclick='DelRow(this)' style='min-width: 70px;'>Del</i></td>");
                else
                    $(tr).html("<td>" + Orders[$i].HQ + "</td><td>" + Orders[$i].Territory_Name + "</td><td><select class='Atyp' data-aTy='" + Orders[$i].Allowance_Type + "'>" + AlwTyps + "</select></td><td style='width:170px;'><input type='text' class='txtDist' value='" + ((Orders[$i].Dis != undefined) ? Orders[$i].Dis : '') + "' onkeyup='CalcDis(this)'/></td><td style='width:170px;'><i class='btn btn-primary btn-AddRw' onclick='AddRow(this)' style='min-width: 70px;'>Add</i>" + ((Orders[$i].AddRw != undefined && Orders[$i].AddRw == 1) ? "&nbsp;<i class='btn btn-danger  btn-update' onclick='DelRow(this)' style='min-width: 70px;'>Del</i>" : "") + "</td>");
                TerrList += "<option value='" + Orders[$i].Territory_Code + "' data-altyp='" + Orders[$i].Allowance_Type + "'>" + Orders[$i].Territory_Name + "</option>";
                $("#OrderList TBODY").append(tr);
            }
            $('.ddltolist').html("<option value=''>--select--</option>" + TerrList);
            $('.ddltolist').val($('.ddltolist').attr("data-sTr"));
            $('.Atyp').each(function(){
            $(this).val($(this).attr("data-aTy"))});
            $("#orders_info").html("Showing " + Orders.length + " entries")
        }

        function AddRow($x) {
            trc = $($x).closest('tr');
            $j = $(trc).index();
            if ($(trc).find('.ddltolist').length > 0) {
                if ($(trc).find("option:selected").val() =='') {
                    alert('Select the To Place');
                    $(trc).find('.ddltolist').focus(); return false;
                }
                $(trc).find('.ddltolist').closest('td').text($(trc).find("option:selected").text())
            }
            itm = {}
            itm.HQCd = Orders[$j].Territory_Code;
            itm.HQ = Orders[$j].Territory_Name;
            itm.Territory_Code = '';
            itm.Territory_Name = '';
            itm.Allowance_Type = '';
            itm.Dis = "0";
            itm.AddRw = 1;
            itm.LRw = 1;
            Orders.splice($j + 1, 0, itm);
            Orders[$j].LRw = 0;
            tr = $("<tr class='subRow'></tr>");
            $(tr).html("<td>" + itm.HQ + "</td><td><select class='ddltolist'><option value=''>--select--</option>" + TerrList + "</select></td><td class='altyp'><select class='Atyp' data-aTy='" + itm.Allowance_Type + "'>" + AlwTyps + "</select></td><td style='width:170px;'><input type='text' class='txtDist' value='' onkeyup='CalcDis(this)' /></td><td style='width:170px;'><i class='btn btn-primary btn-AddRw' onclick='AddRow(this)' style='min-width: 70px;'>Add</i>&nbsp;<i class='btn btn-danger  btn-update' onclick='DelRow(this)' style='min-width: 70px;'>Del</i></td>");
            
            $("#OrderList > TBODY > tr").eq( $j ).after(tr);
        }

        $(document).on("change", ".Atyp", function () {
            $indx = $(this).closest("tr").index();
            Alty = $(this).val();
            Orders[$indx].Allowance_Type = Alty;
        })
        $(document).on("change", ".ddltolist", function () {
            $indx = $(this).closest("tr").index();
            Alty = $(this).find("option:selected").attr("data-altyp");
            $(this).closest("tr").find(".Atyp").val(Alty);
            Orders[$indx].Allowance_Type = Alty;
            Orders[$indx].Territory_Code = $(this).find("option:selected").val();
            Orders[$indx].Territory_Name = $(this).find("option:selected").text();
            Orders[$indx].AddRw = 1;
        });
        $(document).on("click", ".btn-save", function () {
            var SF= $("#<%=ddlTerritoryName.ClientID%>").find('option:selected').val()
            var sXMl = ""; il = 1;
            for ($i = 0; $i < Orders.length; $i++) {
                if (Orders[$i].Dis > 0) {
                    sXMl += "<DS SL=\"" + il + "\" FPN=\"" + Orders[$i].HQ + "\" TPN=\"" + Orders[$i].Territory_Name + "\" FP=\"" + Orders[$i].HQCd + "\" TP=\"" + Orders[$i].Territory_Code + "\" D=\"" + Orders[$i].Dis + "\" PT=\"" + Orders[$i].Allowance_Type + "\" />"
                    il++;
                }
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "Distance_Entry_New.aspx/SaveDistance",
                data: "{'SF':'" + SF + "','xml':'" + sXMl + "'}",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        })

        function DelRow($x) {
            tr=$($x).closest('tr')
            $j = $(tr).index();
            Orders.splice($j , 1);
            $(tr).remove();
        }
        function CalcDis($x) {
            tr = $($x).closest('tr');
            $j = $(tr).index();
            Orders[$j].Dis = $($x).val();
        }
        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
            ReloadTable();
        })
        $("#<%=ddlTerritoryName.ClientID%>").on("change", function () { 
       sf= $(this).find('option:selected').val()
        loadData(sf); });
        function loadData(SF) {
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "Distance_Entry_New.aspx/GetRoutes",
                data: "{'SF':'"+SF+"'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            loadData('<%=Session["SF_Code"].ToString()%>');
        });

    </script>
</asp:Content>
