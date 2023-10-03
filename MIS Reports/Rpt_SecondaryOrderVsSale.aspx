<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_SecondaryOrderVsSale.aspx.cs" Inherits="MIS_Reports_Rpt_SecondaryOrderVsSale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Order Vs sale</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript">
        var SFdet = [];
        function popUp(Sf_Name, Stockist_Name, Sf_Code, Trans_Sl_No, order_value, div) {
            var url = "Rpt_SecondaryOrderSFdetail.aspx?Sf_Code=" + Sf_Code + "&Sf_Name=" + Sf_Name + "&Stockist_Name=" + Stockist_Name + "&Trans_Sl_No=" + Trans_Sl_No + "&order_value=" + order_value + "&div=" + div
            window.open(url, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
        }

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_SecondaryOrderVsSale.aspx/gettotaldetSF",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFdet = JSON.parse(data.d) || [];
                    $('#saletable tbody').html('');
                    var slno = 0;
                    for ($i = 0; $i < SFdet.length; $i++) {
                        if (SFdet.length > 0) {
                            slno += 1;
                            tr = $("<tr Sf_code='" + SFdet[$i].Sf_Code + "' month='" + SFdet[$i].monthn + "' year='" + SFdet[$i].yearn + "' ></tr>");
                            var salepe = (SFdet[$i].Sale_value / SFdet[$i].Order_Value) * 100;
                            var saleper = Math.round(salepe);
                            $(tr).html("<td>" + slno + "</td><td class='month' style='display:none' >" + SFdet[$i].monthn + "</td><td class='year' style='display:none' >" + SFdet[$i].yearn + "</td><td>" + SFdet[$i].Sf_Name + "</td><td>" + SFdet[$i].ordercount + "</td><td>" + SFdet[$i].Order_Value + "</td><td class='salcount' style='cursor:pointer'><a>" + SFdet[$i].Sale_count + "</a></td><td>" + SFdet[$i].Sale_value + "</td ><td class='Rejcount' style='cursor:pointer'><a>" + SFdet[$i].Reject_count + "</a></td><td>" + SFdet[$i].Reject_value + "</td><td>" + saleper + "%</td>");
                        }
                        $('#saletable tbody').append(tr);
                    }

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });

            $("#btnExcel").on('click', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divexcel').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'SecondaryOrderVSsale xls';
                a.click();
                e.preventDefault();

            });
        });
        $(document).on('click', '.salcount', function () {
            var Sf_code = $(this).closest('tr').attr('Sf_code');
            var month = parseInt($(this).closest('tr').attr('month'));
            var year = parseInt($(this).closest('tr').attr('year'));
            var flag = 1;
            window.open("Rpt_SecondaryOrderSFdetail.aspx?sf_code=" + Sf_code + "&month=" + month + "&year=" + year + "&flag=" + flag, "ModalPopUp", "null," + "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=800," + "height=600," + "left = 0," + "top=0");

        });
        $(document).on('click', '.Rejcount', function () {
            var Sf_code = $(this).closest('tr').attr('Sf_code');
            var month = parseInt($(this).closest('tr').attr('month'));
            var year = parseInt($(this).closest('tr').attr('year'));
            var flag = 2;
            window.open("Rpt_SecondaryOrderSFdetail.aspx?sf_code=" + Sf_code + "&month=" + month + "&year=" + year + "&flag=" + flag, "ModalPopUp", "null," + "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=800," + "height=600," + "left = 0," + "top=0");

        });

           </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div> <asp:Label ID="lblHead" Text="Report"  Font-Names="Andalus" Font-Bold="true"     margin-left="71px;" Font-Underline="true"
                runat="server" Font-Size="Large"></asp:Label></div>
            <div style="text-align: right">            
                <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
            </div>
            <div id="divexcel">
                <div class="row" style="margin-left:50px">
                    <asp:Label ID="sfname" Text="Field Force" runat="server"></asp:Label>
                    <asp:TextBox ID="txtsfname" runat="server"></asp:TextBox>
                </div>
                <table id="saletable" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                        <th>Sl.No</th>
                        <th>Field force Name</th>
                        <th>Total order</th>
                        <th>Order value</th>
                        <th>Sale Count</th>
                        <th>Sale value</th>
                        <th>Reject count</th>
                        <th>Reject value</th>
                        <th>Orders vs Sales</th>
                    </thead>

                    <tbody>
                    </tbody>
                </table>
                <br />
                <br />
                <%--    
            <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="false" class="newStly"  >
                <Columns>
                     <asp:TemplateField HeaderText="Sl No">
                        <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Date" >
                        <ItemTemplate>
                        <%#Eval("Order_Date")%>
                            </ItemTemplate>
                    </asp:TemplateField>              
                         <asp:TemplateField HeaderText=" Field Force">
                             <ItemTemplate>                                 
                                 <%#Eval("Sf_Name")%>
                             </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Distributer">
                        <ItemTemplate>
                            <%#Eval("Stockist_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="sfcode">
                        <ItemTemplate>
                            <%#Eval("Sf_Code") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Retailer">
                        <ItemTemplate>
                            <%#Eval("ListedDr_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Order No" >
                        <ItemTemplate>
                            <a href="javascript:popUp('<%# Eval("Sf_Name") %>','<%# Eval("Stockist_Name")%>','<%#Eval("Sf_Code") %>', '<%#Eval("Trans_Sl_No") %>','<%#Eval("order_value") %>','<%=div%>')">
                            <%#Eval("Trans_Sl_No") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Order Value">
                        <ItemTemplate>
                            <%#Eval("order_value") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="OrderVsSale">
                        <ItemTemplate>
                            <%#Eval("order_flag") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>              
            </asp:GridView>--%>
            </div>
        </div>
    </form>
</body>
</html>
